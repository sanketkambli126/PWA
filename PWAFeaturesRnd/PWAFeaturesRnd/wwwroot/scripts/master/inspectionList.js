import "select2/dist/js/select2.full.js";
import { createTree } from "jquery.fancytree";
//import "@chenfengyuan/datepicker";
//import "daterangepicker";
import moment from "moment";
import toastr from "toastr";
import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader } from "../common/datatablefunctions.js"
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, GetCookie, ToastrAlert, MobileTab_Overview, MobileTab_List, Mobile_Tabs, BackButton, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, AddClassIfAbsent, RemoveClassIfPresent, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js"
import { GetSelectedCompanyDetails, SetSelectedCompay, ClearSelectedCompany } from "../master/lookup/companyLookUp"
import { InspectionListPageKey, MobileScreenSize } from '../common/constants.js'
var previous, current;
var startDate, endDate, datesRangeInExcel;
var dtgrid;
var selectedTab;
var ispageLoad;
var statusTree;
var isTypeChanged;
var isStatusChanged;
var InspectionFilterCount = 0;

$(window).on('resize', OnScreenResize);

$(document).on('click', '#aRemoveFilter', function () {
    ClearSelection();
});

function OnScreenResize() {
    SetHeaderMargin();
    MatchDivHeights();
}

$(window).on('resize', SetHeaderMargin);

function MatchDivHeights() {
    $('.equal-height-planning').matchHeight();

}

function SetHeaderMargin() {
    $(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));
}

var headerIsAppend = false;
var IsMobile = false;

var key = CryptoJS.enc.Utf8.parse('8080808080808080')
var iv = CryptoJS.enc.Utf8.parse('8080808080808080')
var code = (function () {

    return {
        encryptMessage: function (messageToencrypt = '', secretkey = '') {
            var encryptedMessage = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(messageToencrypt), key, { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
            return encryptedMessage.toString();
        },
        decryptMessage: function (encryptedMessage = '', secretkey = '') {
            var decryptedBytes = CryptoJS.AES.decrypt(encryptedMessage, secretkey);
            var decryptedMessage = decryptedBytes.toString(CryptoJS.enc.Utf8);
            return decryptedMessage;
        }
    }
})();

$(document).ready(function () {
    AjaxError();
    InitializeListDiscussionAndNoteClickEvents(code);
    AddLoadingIndicator();
    RemoveLoadingIndicator();
    ispageLoad = true;
    var start = moment($('#FromDate').val(), 'DD-MM-YYYY');
    var end = moment($('#ToDate').val(), 'DD-MM-YYYY');

    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        IsMobile = true;
    } else {
        IsMobile = false;
    }

    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });
    //$(".dropdown-toggle").dropdown();
    RegisterTabSelectionEvent('.mobileTabClick', InspectionListPageKey);
    $(document).click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }
    });

    $("#dtrinspectionlist").caleran(
        {
            showButtons: true,
            hideOutOfRange: true,
            showOn: "top",
            arrowOn: "right",
            startDate: start,
            endDate: end,
            format: "DD MMM YYYY",
            ranges: [
                {
                    title: "Last 3 Months",
                    startDate: moment().subtract(3, "month"),
                    endDate: moment()
                },
                {
                    title: "Last 6 Months",
                    startDate: moment().subtract(6, "month"),
                    endDate: moment()
                },
                {
                    title: "Last 12 Months",
                    startDate: moment().subtract(12, "month"),
                    endDate: moment()
                },
            ],
            rangeOrientation: "vertical",
            onafterselect: function (caleran, startDate, endDate) {
                console.log('onafterselect Event fire');
                setDateDetails(startDate, endDate);
            },
            oncancel: function (caleran, start, end) {
                console.log('onCancel Event fire');
                  setDateDetailonCancel(start, end);
            }
        }
    );

    setDateDetails(start, end);

    $('.height-equal').matchHeight();

    BackButton(InspectionListPageKey, true);

  
    $("#statustree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: [
            {
                "title": "All", "key": "AllInspections", "folder": false, "expanded": true, "children": [
                    {
                        "title": "Inspection with Open Findings", "key": "Outstanding", "folder": false, "children": false,
                    },
                    {
                        "title": "Inspection with Overdue Findings", "key": "Overdue", "folder": false, "children": false,
                    },
                    {
                        "title": "Pending Closure", "key": "Complete", "folder": false, "children": false,
                    },
                    {
                        "title": "Closed", "key": "Closed", "folder": false, "children": false,
                    },
                ]
            },
        ],
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });

    $('#btnStatus').click(onStatusClick);

    $("#typetree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/Inspection/GetInspectionTypeTree",
            data: { Ves_Id: $("#VesselId").val(), IsManualCreation: false },
            dataType: "json"
        }),
        init: function (event, data) {
            $("#typetree").fancytree("getTree").visit(function (node) {
                var typeIdList = $('#strInspectionTypeIds').val().split(',');
                typeIdList.forEach(function () {
                    if (typeIdList.includes(node.key)) {
                        node.setSelected(true);
                    }
                    else {
                        node.setSelected(false);
                    }
                });
            });
        },
        click: function (event, data) {
            if (data.targetType != 'expander') {
                $("#formTxtInspectionType").val("");
            }
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        }
    });

    $('#btnType').click(onTypeClick);



    $(".tab-2").click(function () {
        $('#btnExportMobile').show();
        $('#btnInspectionPlanningMobile').hide();
    });

    $(".tab-1").click(function () {
        $('#btnExportMobile').hide();
        $('#btnInspectionPlanningMobile').show();
    });

    $(".mobile-tab").on("click", function () {
        SetHeaderMargin();
    });
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }

    $('#btnClear').click(ClearSelection);
    $('#btnExport, #btnExportMobile').click(() => {
        var findingColumn = 7;
        var chatColumn = 0;
        var NotesColumn = 1;
        let mobileChatNoteColumn = 2;
        var searchValue = dtgrid.search();
        dtgrid.search("").draw();

        var isFindingVisible = dtgrid.column(findingColumn).visible();

        if (isFindingVisible) {
            dtgrid.column(findingColumn).visible(false);
        }

        dtgrid.column(chatColumn).visible(false);
        dtgrid.column(NotesColumn).visible(false);
        dtgrid.column(mobileChatNoteColumn).visible(false);

        $('#dtGrid.cardview thead').addClass("export-grid-show");
        $('#dtGrid').DataTable().buttons(0, 2).trigger();
        $('#dtGrid.cardview thead').removeClass("export-grid-show");

        if (isFindingVisible) {
            dtgrid.column(findingColumn).visible(true);
        }

        dtgrid.column(chatColumn).visible(true);
        dtgrid.column(NotesColumn).visible(true);
        dtgrid.column(mobileChatNoteColumn).visible(true);

        dtgrid.search(searchValue).draw();
    });

    $("#btnSearch").click(function () {
        var selectedCompany = GetSelectedCompanyDetails();
        $('#CompanyId').val(selectedCompany.id);
        $('#Company').val(selectedCompany.name);
        SetPageParameter(isStatusChanged, isTypeChanged, false);
    });


    $("#formTxtInspectionType").on('input', function () {
        $("#typetree").fancytree("getTree").visit(function (node) {
            node.setSelected(false);
        });
        FilterCountSet(1, "#typePeriodFilterCount");
    });

    MaintainFilterParameters();

    $('#filterdata').on('show.bs.modal', function (e) {
        if ($('#filterType').css('display') == 'none'
            && $('#filterTypeTextRow').css('display') == 'none') {
            $("#filterCard2").hide();
        }
        else {
            $("#filterCard2").show();
        }
    });
});

$('#statustree').click(function () {
    FilterCountSet(GetTreeNodeLength("#statustree"), "#statusPeriodFilterCount");
});

$('#typetree').click(function () {
    FilterCountSet(GetTreeNodeLength("#typetree"), "#typePeriodFilterCount");
});

$(document).on('click', '#divSearchCompanyLookup .rowTemplate', function () {
    FilterCountSet(1, "#companyPeriodFilterCount");
});

$(document).on('click', '#divSearchCompanyLookup .removeSelection', function () {
    FilterCountSet(0, "#companyPeriodFilterCount");
});

$("#formTxtInspector").keyup(function () {
    let count = 0;
    let textInspector = $("#formTxtInspector").val();
    if (!IsNullOrEmptyOrUndefined(textInspector)) {
        count = count + 1;
    }
    FilterCountSet(count, "#inspectorPeriodFilterCount");
});

function onStatusClick() {
    isStatusChanged = true;
}
function onTypeClick() {
    isTypeChanged = true;
}

function setDateDetails(start, end) {
    startDate = start.format("DD MMM YYYY");
    endDate = end.format("DD MMM YYYY");
    datesRangeInExcel = start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY");
    $("#dtrinspectionlist").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
    $("#tblspndtrinspectionlist").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));

    if (ispageLoad == true) {
        ispageLoad = false;
    }
    else {
        SetPageParameter(false, false, false);
    }
}

function InitializedtGrid() {
    $('#dtGrid').DataTable().destroy();
    $('#dtGrid').append('<caption style="caption-side: top"><div class="row"><div class="col-md-10 dt-hd-part1"><strong>Details</strong></div> <div class="col-md-2 dt-hd-part2"><strong>Findings</strong></div></div></caption>');
}

function LoadGrid(track) {

    console.log("startDate", startDate);
    console.log("endDate", endDate);

    var data = {
        "inspectionType": $('#InspectionType').val(),
        "fromDate": startDate,
        "toDate": endDate,
        "vesselId": $('#VesselId').val(),
        "isFindingOutstanding": $('#IsFindingOutstanding').val(),
        "isFindingOverdue": $('#IsFindingOverdue').val(),
        "isPendingClosure": $('#IsPendingClosure').val(),
        "isClosed": $('#IsClosed').val(),
        "isAllSelected": $('#IsAllSelected').val(),
        "inspectionTypeIds": $('#InspectionTypeIds').val(),
        "isShowDetained": $('#IsShowDetained').val(),
        "isDue": $('#IsDue').val(),
        "isOverdue": $('#IsOverdue').val(),
        "strInspectionTypeIds": $('#strInspectionTypeIds').val(),
        "inspectionFilter": $('#InspectionFilter').val(),
        "inDays": $('#InDays').val(),
        "isInspection": $('#IsInspection').val(),
        "company": $("#Company").val(),
        "companyId": $("#CompanyId").val(),
        "inspector": $("#Inspector").val(),
        "inspectionTypeTextField": $("#InspectionTypeTextField").val(),
        "isDetention": $('#IsDetention').val(),
        "isOMVRejection": $('#IsOMVRejection').val(),
        "isPSCDeficency": $('#IsPSCDeficency').val(),
    };

    dtgrid = $('#dtGrid').DataTable({
        //"dom": '<<"row mb-3"<"col-12 col-md-4 offset-md-0 col-lg-3 offset-lg-5 col-xl-2 offset-xl-4 dt-infomation  dt-infomationhed"i><"col-md-3 col-lg-2 col-xl-3 //filters-data"><"col-12 col-md-5 col-lg-3 col-xl-3"f>><rt><"clearfix"<"float-left"l><""p>>>',
        //"dom": '<"row"<"col-12 col-md-6 col-lg-6 col-xl-6"i><"col-12 col-md-6 col-lg-6 col-xl-6 search-filter"f>>' +
        //       '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "pageLength": 10,
        "destroy": true,
        "order": [],
        "language": {
            "emptyTable": "No inspections available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/Inspection/GetInspectionList",
            "type": "POST",
            "data": data,
            "datatype": "json"
        },
        buttons: [
            'copy', 'csv',
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible',
                    format: {
                        body: function (data, row, column, node) {
                            var child = node.querySelectorAll('.export-Data');

                            if (child != undefined && child.length > 0) {
                                var actualData = child[0];
                                if (actualData != undefined) {
                                    return actualData.innerText;
                                }
                            }
                        }
                    }
                },
                customize: function (xlsx) {
                    CustomizedExcelHeader(xlsx, 2);
                },
                title: "Inspection",
                messageTop: function () {
                    var headerDetails = 'Vessel : ' + $('#VesselName').val() + '\nDate : ' + datesRangeInExcel;
                    if ($('#IsAllSelected').val() == "true") {
                        headerDetails = headerDetails + '\nStatus : All';
                    }
                    else {
                        var selectedStatusNodes = $('#statustree').fancytree('getTree').getSelectedNodes();
                        var selectedStatus = selectedStatusNodes.map(x => x.title);
                        headerDetails = headerDetails + '\nStatus : ' + selectedStatus.join();
                    }
                    //var inspectionTypeTextField = $("#formTxtInspectionType").val();
                    //if (inspectionTypeTextField != null && inspectionTypeTextField != "" && inspectionTypeTextField != "undefined") {
                    //    headerDetails = headerDetails + '\nInspectionTypes : ' + inspectionTypeTextField;
                    //} else {
                    //    var selectedTypeNodes = $("#typetree").fancytree('getTree').getSelectedNodes();
                    //    var selectedTypes = selectedTypeNodes.map(x => x.title);
                    //    headerDetails = headerDetails + '\nInspectionTypes : ' + selectedTypes.join();
                    //}

                    return headerDetails;
                }
            },
            'pdf', 'print'
        ],
        "columns": [
            {
                className: "data-icon-align d-none d-md-table-cell",
                orderable: false,
                width: "10px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0) {
                        return GetChatBaseIcons(full.inspectionId, full.channelCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align d-none d-md-table-cell",
                orderable: false,
                width: "10px",
                render: function (data, type, full, meta) {
                    if (full.notesCount > 0) {
                        return GetNotesBaseIcons(full.inspectionId, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align tdblock d-md-none d-lg-none d-xl-none",
                orderable: false,
                width: "70px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0 || full.notesCount > 0) {
                        return GetChatNotesBaseIcons(full.inspectionId, full.channelCount, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "tdblock td-row-header font-weight-600 data-text-align rowheadermargin",
                name: "InspectionTypeName",
                data: "inspectionTypeName",
                width: "167px",
                render: function (data, type, full, meta) {
                    if ($('#InspectionType').val() == 'InspectionDueType' || $('#InspectionType').val() == 'InspectionOverdueType') {
                        return GetActualCellData(data);
                    }
                    else {
                        return '<a href="/Inspection/Findings/?finding=' + full.findingURL + '&vesselId=' + full.vesselId + '">' + GetActualCellData(data) + '</a>';
                    }
                }
            },
            {
                className: "tdblock data-text-align",
                name: "Status",
                data: "status",
                width: "90px",
                render: function (data, type, full, meta) {
                    var color = '';
                    if (data == "Overdue Findings") {
                        color = "txt-red";
                    }
                    else if (data == "Open Findings") {
                        color = "text-yellow";
                    }
                    else if (data == "Pending Closure") {
                        color = "txt-blue";
                    }
                    else if (data == "Closed") {
                        color = "txt-color-green";
                    }

                    //return GetCellData('Status', '<span class="' + color + '">' + data + '</span>');
                    return '<span class="export-Data ' + color + '">' + data + '</span>';
                }
            },
            {
                className: "data-datetime-align",
                name: "InspectionDate",
                data: "inspectionDate",
                width: "62px",
                type: "date",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null && data != '') {
                        date = new Date(data);
                        formattedDate = moment(date).format("DD MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Occured', formattedDate);
                        return formattedDate;
                    }
                    return date;
                }
            },
            {
                className: "data-datetime-align",
                name: "NextDueDate",
                data: "nextDueDate",
                width: "64px",
                type: "date",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null && data != '') {
                        date = new Date(data);
                        formattedDate = moment(date).format("DD MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Next Due', formattedDate);
                    }
                    return date;
                }
            },
            {
                className: "tdblock data-text-align",
                data: "location",
                name: "Location",
                width: "140px",
                render: function (data, type, full, meta) {
                    if (data != null) {
                        return GetCellData('Location', data);
                    }
                    else {
                        return GetCellData('Location', '<span class="d-sm-none">-</span>');
                    }
                }
            },
            {
                className: "tdblock data-text-align",
                name: "CompanyName",
                data: "companyName",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Company', data); }
            },
            {
                className: "tdblock data-number-align",
                name: "DaysDetained",
                data: "daysDetained",
                width: "51px",
                render: function (data, type, full, meta) {
                    var color = '';
                    if (data != null && full.isPSCDetention) {
                        color = "txt-red font-weight-600";
                        return GetCellData('Days Detained', '<span class="' + color + '">' + data + '</span>');
                    }
                    else {
                        return GetCellData('Days Detained', '<span></span>');
                    }
                }
            },
            {
                className: "tdblock td-row-header d-inline-block d-sm-none finding-mobile-text",
                "defaultContent": 'FINDINGS',
                orderable: false
            },
            {
                className: "data-number-align td-w-auto border-xs-0 text-dark-grey-span mobile-bottom-0-label",
                name: "TotalFindingCount",
                data: "totalFindingCount",
                width: "51px",

                render: function (data, type, full, meta) {
                    var color = '';
                    if (data == 0) {
                        color = "txt-color-green font-weight-600";
                    }

                    return GetCellData('Total', '<span class="' + color + '">' + data + '</span>');
                }
            },
            {
                className: "data-number-align td-w-auto border-xs-0 mobile-bottom-0-label",
                name: "OutStandingFindingCount",
                data: "outStandingFindingCount",
                width: "30px",
                render: function (data, type, full, meta) {
                    var color = '';
                    if (data > 0) {
                        color = "text-yellow font-weight-600";
                    }
                    else {
                        color = "txt-color-gray";
                    }

                    return GetCellData('Open', '<span class="' + color + '">' + data + '</span>');
                }
            },
            {
                className: "data-number-align td-w-auto border-xs-0 mobile-bottom-0-label",
                name: "OverdueFindingCount",
                data: "overdueFindingCount",
                width: "30px",
                render: function (data, type, full, meta) {
                    var color = '';
                    if (data > 0) {
                        color = "txt-red font-weight-600";
                    }
                    else {
                        color = "txt-color-gray";
                    }
                    return GetCellData('Overdue', '<span class="' + color + '">' + data + '</span>');
                }
            }
        ],
        "fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
            let child = $(nRow).find('td:eq(2)').children();
            if (screen.width < MobileScreenSize && $(child).length == 0) {
                $(nRow).find('td:eq(2)').addClass('d-none');
            }
        },
        "initComplete": function (settings, data) {
            AppendGridTitle();
        }
    });
    //$("div.filters-data").html('');

    $.fn.DataTable.ext.pager.numbers_length = 4;

    $("#divDtinfo").html($('.dt-infomation').innerText)

    $('#dtGrid').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });
}

//Inspection Summary Call
function BindInspectionSummary() {

    var request =
    {
        "EncryptedVesselId": $('#VesselId').val(),
        "ToDate": endDate,
        "FromDate": startDate,
        "IsFromDashboard": false
    }

    $.ajax({
        url: "/Inspection/GetInspectionDashboardDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        success: function (data) {
            if (data != null) {

                //inspection & audit
                $('#spanInspectionDueCount').text(data.inspectionDueCount);
                SummaryColorCode('#spanInspectionDueCount', data.inspectionDueCount, 'txt-blue');
                $('#spanInspectionOverdueCount').text(data.inspectionOverdueCount);
                SummaryColorCode('#spanInspectionOverdueCount', data.inspectionOverdueCount, 'txt-red');
                $('#spanInspectionNeverDoneCount').text(data.inspectionNeverDoneCount);
                SummaryColorCode('#spanInspectionNeverDoneCount', data.inspectionNeverDoneCount, 'txt-red');

                //inspection
                $('#spanInspectionFindingOutstandingCount').text(data.inspectionFindingOutstandingCount);
                SummaryColorCode('#spanInspectionFindingOutstandingCount', data.inspectionFindingOutstandingCount, 'text-yellow');
                $('#spanInspectionFindingOverdueCount').text(data.inspectionFindingOverdueCount);
                SummaryColorCode('#spanInspectionFindingOverdueCount', data.inspectionFindingOverdueCount, 'txt-red');
                $('#spanPendingClosureCount').text(data.pendingClosureCount);
                SummaryColorCode('#spanPendingClosureCount', data.pendingClosureCount, 'txt-blue');

                //inspection type
                $('#spanInspectionTypePscCount').text(data.openPSCInspectionCount);
                SummaryColorCode('#spanInspectionTypePscCount', data.openPSCInspectionCount, 'txt-blue');
                $('#spanInspectionTypeOmvCount').text(data.openOMVInspectionCount);
                SummaryColorCode('#spanInspectionTypeOmvCount', data.openOMVInspectionCount, 'txt-blue');

                //audit
                $('#spanInspectionAuditFindingOutstandingCount').text(data.inspectionAuditFindingOutstandingCount);
                SummaryColorCode('#spanInspectionAuditFindingOutstandingCount', data.inspectionAuditFindingOutstandingCount, 'text-yellow');
                $('#spanInspectionAuditFindingOverdueCount').text(data.inspectionAuditFindingOverdueCount);
                SummaryColorCode('#spanInspectionAuditFindingOverdueCount', data.inspectionAuditFindingOverdueCount, 'txt-red');
                $('#spanAuditPendingClosureCount').text(data.auditPendingClosureCount);
                SummaryColorCode('#spanAuditPendingClosureCount', data.auditPendingClosureCount, 'txt-blue');

                //findings
                $('#spanTotalOutstandingFindingCount').text(data.totalOutstandingFindingCount);
                SummaryColorCode('#spanTotalOutstandingFindingCount', data.totalOutstandingFindingCount, 'text-yellow');
                $('#spanTotalOverdueFindingCount').text(data.totalOverdueFindingCount);
                SummaryColorCode('#spanTotalOverdueFindingCount', data.totalOverdueFindingCount, 'txt-red');

                //settingh url
                //Remove all click event
                $(".click-event-off").off('click');

                $("#InspectionFindingOutstanding").click(function () {
                    BindInspectionDashboardDetails(data.inspectionFindingOutstandingTypeURL);
                });
                $("#InspectionFindingOverdue").click(function () {
                    BindInspectionDashboardDetails(data.inspectionFindingOverdueTypeURL);
                });

                $("#PendingClosure").click(function () {
                    BindInspectionDashboardDetails(data.pendingClosureByOfficeTypeURL);
                });

                $("#InspectionAuditFindingOutstanding").click(function () {
                    BindInspectionDashboardDetails(data.auditInspectionFindingOutstandingTypeURL);
                });

                $("#InspectionAuditFindingOverdue").click(function () {
                    BindInspectionDashboardDetails(data.auditInspectionFindingOverdueTypeURL);
                });

                $("#AuditPendingClosure").click(function () {
                    BindInspectionDashboardDetails(data.auditPendingClosureByOfficeTypeURL);
                });

                $("#inspectionAll").click(function () {
                    BindInspectionDashboardDetails(data.allInspectionURL);
                });

                $("#TotalOutstandingFinding").click(function () {
                    BindInspectionDashboardDetails(data.findingsOutstandingUrl);
                });

                $("#TotalOverdueFinding").click(function () {
                    BindInspectionDashboardDetails(data.findingsOverdueUrl);
                });

                $("#aInspectionTypePsc").click(function () {
                    BindInspectionDashboardDetails(data.inspectionTypePscURL);
                });

                $("#aInspectionTypeOmv").click(function () {
                    BindInspectionDashboardDetails(data.inspectionTypeOmvURL);
                });

                //height adjustment
                MatchDivHeights();
            }
        },
        compelet: function () {

        }
    });
}

function SummaryColorCode(span, data, colorClass) {
    if (data == 0) {
        $(span).addClass("txt-color-gray");
    }
    else {
        $(span).addClass(colorClass);
    }
}

function BindInspectionDashboardDetails(InspectionUrl) {
    $.ajax({
        url: "/Inspection/BindInspectionDashboardDetails",
        type: "POST",
        "data": { "inspection": InspectionUrl, "vesselId": $('#VesselId').val() },
        success: function (data) {
            var responce = data.data;
            SetHiddenFields(responce, data.vesselId);
            SetFilterData();
            LoadGrid();

            if (($(window).width() < MobileScreenSize)) {
                var MobilTabCls = $("#ActiveMobileTabClass").val();
                $('.' + MobilTabCls)[0].click();
            }
        }
    });
}

function GetCellData(label, data) {
    return '<label>' + label + '</label><br />' + GetActualCellData(data);
}

function GetActualCellData(data) {
    return '<span class="export-Data">' + data + '</span>';
}

function ClearSelection() {
    FilterCountSet(0, ".filtercount");
    InspectionFilterCount = 0;
    ClearSelectedCompany();
    var statusTree = $('#statustree');
    var typeTree = $('#typetree');
    var selectedStatusNodes = statusTree.fancytree('getTree').getSelectedNodes();
    var selectedStatus = selectedStatusNodes.map(x => x.key);
    var selectedTypeNodes = typeTree.fancytree('getTree').getSelectedNodes();
    var selectedType = selectedTypeNodes.map(x => x.key);
    var input = {
        "inspectionType": $('#InspectionType').val(),
        "fromDate": startDate,
        "toDate": endDate,
        "vesselId": $('#VesselId').val(),
        "statusList": selectedStatus,
        "isSummaryClicked": false,
        "inspectionTypeIds": selectedType,
    }

    $.ajax({
        url: "/Inspection/SetDefaultValue",
        type: "POST",
        "data": input,
        success: function (data) {
            //window.location.href = data;
            var res = data.data;
            SetHiddenFields(res, data.vesselId);
            SetFilterData();
            LoadGrid();
            BindInspectionSummary();
        }
    });
}

function SetPageParameter(isStatusChanged, isTypeChanged, isSummaryClicked) {
    var statusTree = $('#statustree');
    var typeTree = $('#typetree');
    var selectedStatusNodes = statusTree.fancytree('getTree').getSelectedNodes();
    var selectedStatus = selectedStatusNodes.map(x => x.key);
    var selectedTypeNodes = typeTree.fancytree('getTree').getSelectedNodes();
    var selectedType = selectedTypeNodes.map(x => x.key);
    var inspectorName = $("#formTxtInspector").val();
    var inspectionTypeTextField = $("#formTxtInspectionType").val();

    var input = {
        "fromDate": startDate,
        "toDate": endDate,
        "vesselId": $('#VesselId').val(),
        "statusList": selectedStatus,
        "isStatusChanged": isStatusChanged,
        "isSummaryClicked": isSummaryClicked,
        "isTypeChanged": isTypeChanged,
        "company": $("#Company").val(),
        "companyId": $("#CompanyId").val(),
        "inspector": inspectorName
    }

    if (inspectionTypeTextField != null && inspectionTypeTextField != "" && inspectionTypeTextField != 'undefined') {
        input["inspectionTypeTextField"] = inspectionTypeTextField;
    } else {
        input["inspectionTypeIds"] = selectedType;
        input["inspectionType"] = $('#InspectionType').val();
    }

    $.ajax({
        url: "/Inspection/SetPageParameter",
        type: "POST",
        "data": input,
        success: function (data) {
            // window.location.href = data;
            var res = data.data;
            SetHiddenFields(res, data.vesselId);

            InspectionFilterCount = 0;
            AppendTreeFilterDataInModel("#statustree", "#filterStatus", "#filterCard1");
            AppendTreeFilterDataInModel("#typetree", "#filterType", "#filterType");

            var companyVal = $('#Company').val();
            var inspectorVal = $('#Inspector').val();
            companyVal = companyVal === '' ? null : companyVal;
            inspectorVal = inspectorVal === '' ? null : inspectorVal;

            AppendTextFilterDataInModel($('#Company').val(), "#filterCompany", "#filterCard3");
            AppendTextFilterDataInModel($('#Inspector').val(), "#filterInspector", "#filterCard4");
            AppendTextFilterDataInModel($('#formTxtInspectionType').val(), "#filterTypeText", "#filterTypeTextRow");

            LoadGrid();
            BindInspectionSummary();
        }
    });
}

function MaintainFilterParameters() {

    $.ajax({
        url: "/Inspection/MaintainFilter",
        type: "POST",
        success: function (data) {

            if (data.isTempDataExist) {
                var responce = data.data;
                SetHiddenFields(responce, data.vesselId);
            }
            SetHeaderMargin();
        },
        complete: function () {
            SetFilterData();
            InitializedtGrid();
            LoadGrid();
            BindInspectionSummary();
        }
    });
}

function SetHiddenFields(responce, vesselId) {

    startDate = responce.fromDate;
    endDate = responce.toDate;

    $('#InspectionType').val(responce.inspectionType);
    $('#VesselId').val(vesselId);
    $('#IsFindingOutstanding').val(responce.isFindingOutstanding);
    $('#IsFindingOverdue').val(responce.isFindingOverdue);
    $('#IsPendingClosure').val(responce.isPendingClosure);
    $('#IsClosed').val(responce.isClosed);
    $('#IsAllSelected').val(responce.isAllSelected);
    $('#InspectionTypeIds').val(responce.inspectionTypeIds);
    $('#IsShowDetained').val(responce.isShowDetained);
    $('#IsDue').val(responce.isDue);
    $('#IsOverdue').val(responce.isOverdue);
    $('#strInspectionTypeIds').val(responce.strInspectionTypeIds);
    $('#InspectionFilter').val(responce.inspectionFilter);
    $('#InDays').val(responce.inDays);
    $('#IsInspection').val(responce.isInspection);
    $("#Company").val(responce.company);
    $("#CompanyId").val(responce.companyId);
    $("#Inspector").val(responce.inspector);
    $("#InspectionTypeTextField").val(responce.inspectionTypeTextField);
    $('#GridSubTitle').val(responce.gridSubTitle);
    $('#IsDetention').val(responce.isDetention);
    $('#IsOMVRejection').val(responce.isOMVRejection);
    $('#IsPSCDeficency').val(responce.isPSCDeficency);
    $("#ActiveMobileTabClass").val(responce.activeMobileTabClass);
}

function SetFilterData() {
    InspectionFilterCount = 0;

    var start = moment(startDate, 'DD-MM-YYYY').format("DD MMM YYYY");
    var end = moment(endDate, 'DD-MM-YYYY').format("DD MMM YYYY");

    if (start != "Invalid date" && end != "Invalid date") {
        $("#dtrinspectionlist").html(start + "-" + end);
        $("#tblspndtrinspectionlist").html(start + "-" + end);
    }

    $("#statustree").fancytree("getTree").visit(function (node) {

        node.setSelected(false);
        if (node.key == 'AllInspections' && $('#IsAllSelected').val().toLowerCase() == "true") {
            node.setSelected(true);
        }

        if (node.key == 'Outstanding' && $('#IsFindingOutstanding').val().toLowerCase() == "true") {
            node.setSelected(true);
        }
        if (node.key == 'Overdue' && $('#IsFindingOverdue').val().toLowerCase() == "true") {
            node.setSelected(true);
        }
        if (node.key == 'Complete' && $('#IsPendingClosure').val().toLowerCase() == "true") {
            node.setSelected(true);
        }
        if (node.key == 'Closed' && $('#IsClosed').val().toLowerCase() == "true") {
            node.setSelected(true);
        }
    });

    $("#typetree").fancytree("getTree").visit(function (node) {
        var typeIdList = $('#strInspectionTypeIds').val().split(',');
        node.setSelected(false);
        typeIdList.forEach(function () {
            if (typeIdList.includes(node.key)) {
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });

    $('#formTxtInspector').val($('#Inspector').val());

    $("#formTxtInspectionType").val($("#InspectionTypeTextField").val());

    if ($('#CompanyId').val() != null && $('#CompanyId').val() != '' && $('#CompanyId').val() != 'undefined') {
        SetSelectedCompay($('#Company').val(), $('#CompanyId').val());
    }

    AppendTreeFilterDataInModel("#statustree", "#filterStatus", "#filterCard1");
    AppendTreeFilterDataInModel("#typetree", "#filterType", "#filterType");

    var companyVal = $('#Company').val();
    var inspectorVal = $('#Inspector').val();
    var inspectionTypeVal = $("#formTxtInspectionType").val();

    AppendTextFilterDataInModel(companyVal, "#filterCompany", "#filterCard3");
    AppendTextFilterDataInModel(inspectorVal, "#filterInspector", "#filterCard4");
    AppendTextFilterDataInModel(inspectionTypeVal, "#filterTypeText", "#filterTypeTextRow");

    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();

        if (MobilTabCls == "tab-2") {
            $('#btnExportMobile').show();
            $('#btnInspectionPlanningMobile').hide();
        }
        else {
            $('#btnExportMobile').hide();
            $('#btnInspectionPlanningMobile').show();
        }
    }
}

function GetUniqueChildArr(nodedata) {
    var parentArray = new Map();
    var statusArray = new Map();

    nodedata.forEach(function (node) {
        statusArray.set(node.title, node.title);
        parentArray.set(node.parent.title, node.parent.title);
    });
    parentArray.forEach((value, key) => {
        if (statusArray.has(key)) {
            statusArray.delete(key);
        }
    });

    return statusArray;
}

function GetFilterHtmlElement(statusArray) {
    var htmlElement = "";

    statusArray.forEach((value, key) => {
        htmlElement += '<div class="col-12 col-md-6 col-lg-4 col-xl-4">';
        htmlElement += '<div class="dashboard-counters-label"><span id="">' + value + '</span></div></div>';
    });

    return htmlElement;
}

function AppendGridTitle() {

    hideShowFilterDesign();
    $('#appliedFilterCount').text(InspectionFilterCount);

    //$("#tableSubTitle").hide();
    var subtitle = $("#GridSubTitle").val();
    if (!IsNullOrEmptyOrUndefined(subtitle) && subtitle != "All") {
        $("#tblspndtrinspectionlist").hide();
        //$("#tableSubTitle").show();
        $("#tableSubTitle").text(subtitle);
    }
    else {
        $("#tableSubTitle").text("Inspections & Audits");
        $("#tblspndtrinspectionlist").show();
    }
}

function AppendTreeFilterDataInModel(treeSelector, filterId, filterCardId) {
    var StatusTree = $(treeSelector).fancytree('getTree').getSelectedNodes();
    var StatusTreeArray = GetUniqueChildArr(StatusTree);
    var StatusHtmlElement = GetFilterHtmlElement(StatusTreeArray);

    if (StatusTreeArray.size > 0) {
        $(filterId).html(StatusHtmlElement);
        InspectionFilterCount = InspectionFilterCount + StatusTreeArray.size;
        $('#appliedFilterCount').text(InspectionFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).html("");
        $(filterCardId).hide();
    }

    console.log("treeSelector", StatusTreeArray.size);
    if (treeSelector == "#statustree") {
        FilterCountSet(StatusTreeArray.size, "#statusPeriodFilterCount");
    }
    else if (treeSelector == "#typetree" && StatusTreeArray.size > 0) {
        FilterCountSet(StatusTreeArray.size, "#typePeriodFilterCount");
    }

    hideShowFilterDesign();
}

function AppendTextFilterDataInModel(filteredValue, filterId, filterCardId) {
    if (!IsNullOrEmptyOrUndefined(filteredValue) && filteredValue !== undefined) {
        InspectionFilterCount++;
        $(filterId).text(filteredValue);
        $('#appliedFilterCount').text(InspectionFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).text("");
        $(filterCardId).hide();
    }

    var count = 0;
    if (filterId == "#filterCompany") {
        if (!IsNullOrEmptyOrUndefined(filteredValue)) {
            count = 1;
        }
        FilterCountSet(count, "#companyPeriodFilterCount");
    }
    else if (filterId == "#filterInspector") {
        if (!IsNullOrEmptyOrUndefined(filteredValue)) {
            count = 1;
        }
        FilterCountSet(count, "#inspectorPeriodFilterCount");
    }
    else if (filterId == "#filterTypeText") {
        console.log("filterTypeText", filteredValue);
        if (!IsNullOrEmptyOrUndefined(filteredValue)) {
            count = 1;
            FilterCountSet(count, "#typePeriodFilterCount");
        }
    }

    hideShowFilterDesign();
}

function hideShowFilterDesign() {
    if (InspectionFilterCount > 0) {
        $(".filter-design, .clear-filter").show();
        $("#divfilterhide").removeClass('btn-dark-grey');
        $("#divfilterhide").addClass('btn-info');
    }
    else {
        $(".filter-design, .clear-filter").hide();
        $("#divfilterhide").removeClass('btn-info');
        $("#divfilterhide").addClass('btn-dark-grey');
    }
}

function GetTreeNodeLength(element) {
    var treeData = $(element);
    var selectedNodes = treeData.fancytree('getTree').getSelectedNodes();
    var TreeNodeArray = GetUniqueChildArr(selectedNodes);
    return TreeNodeArray.size;
}

function FilterCountSet(nodeLength, elementCount) {
    $(elementCount).text(nodeLength);
    if (nodeLength > 0)
        AddClassIfAbsent($(elementCount).parent('div'), 'active');
    else
        RemoveClassIfPresent($(elementCount).parent('div'), 'active');
}
