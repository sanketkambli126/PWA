import { createTree } from "jquery.fancytree";
import "@chenfengyuan/datepicker";
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import "daterangepicker";
import moment from "moment";
require('bootstrap');

import { GetCookie, AjaxError, ReplaceClass, AddLoadingIndicator, RemoveLoadingIndicator, IsNullOrEmptyOrUndefined, BackButton, MobileTab_Overview, Mobile_Tabs, MobileTab_List, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, AddClassIfAbsent, RemoveClassIfPresent, RegisterTabSelectionEvent } from '../common/utilities.js';
import { JSAListPageKey, MobileScreenSize, Tab2, Tab1, ApprovedJSAStatus } from '../common/constants.js';
import { GetExportFormattedDate, GetExportCellData, CustomizedExcelHeader, GetExportData } from '../common/datatableFunctions.js';

var isSearchClicked = false;
var jsaFilterCount = 0;
var dtjsalist;
var ExportStageName = '';
var SelectedStatus = '';
var SelectedSystemArea = '';
var SelectedRisk = '';
var SelectedOther = '';

var statusColorMap = new Map();
statusColorMap.set(0, { textColor: "txt-orange" });
statusColorMap.set(1, { textColor: "txt-green" });
statusColorMap.set(5, { textColor: "text-grey" });
statusColorMap.set(6, { textColor: "txt-red" });

var riskColorMap = new Map();
riskColorMap.set(0, { textColor: "text-yellow" });
riskColorMap.set(1, { textColor: "txt-green" });
riskColorMap.set(7, { textColor: "txt-red" });
riskColorMap.set(6, { textColor: "txt-orange" });

$(document).on('click', '#aRemoveFilter', function () {
    ClearSelection();
    $("#aTotalNav")[0].click();
    $('.popover').popover('hide');
});

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
    AddLoadingIndicator();
    RemoveLoadingIndicator();
    InitializeListDiscussionAndNoteClickEvents(code);

    ExportStageName = $("#GridSubTitle").val();

    $('#chckOverdueForClosure').click(function () {
        if (this.checked) {
            $('#SelectedStatusIds').val(ApprovedJSAStatus);
            FilterCountSet(1, "#otherFilterCount");
            SetStatusTree();
        }
        else {
            FilterCountSet(0, "#otherFilterCount");
        }
    });

    //Sidebar back
    BackButton(JSAListPageKey, true);

    //daterangepicker
    //var start = moment();
    //var end = moment();


    //$("#dtrjsalist").daterangepicker(
    //    {
    //        startDate: start,
    //        endDate: end,
    //        opens: "right",
    //        ranges: {
    //            Today: [moment(), moment()],
    //            Yesterday: [moment().subtract(1, "days"), moment().subtract(1, "days")],
    //            "Last 7 Days": [moment().subtract(6, "days"), moment()],
    //            "Last 30 Days": [moment().subtract(29, "days"), moment()],
    //            "This Month": [moment().startOf("month"), moment().endOf("month")],
    //            "Last Month": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")],
    //            "Last 6 Month": [moment().subtract(6, "month"), moment()]
    //        },
    //        locale: {
    //            format: "DD MMM YYYY"
    //        }
    //    },
    //    cb
    //);

    //cb(start, end);

    $('.height-equal').matchHeight();

    BindSummary();

    MaintainFilters();

    LoadStatusTree();
    LoadSystemAreaTree();
    LoadRiskFilterTree();
    SetOverdueForClosure();
    RegisterTabSelectionEvent('.mobileTabClick', JSAListPageKey);
    //search
    $('#btnSearch').click(function () {
        SetPageParameter();
        $('.popover').popover('hide');
    });

    //clear
    $('#btnClear').click(function () {
        ClearSelection();
        $("#aTotalNav")[0].click();
        $('.popover').popover('hide');
    });

    //Export To Excel
    $('.btnExport').click(() => {
        var searchValue = dtjsalist.search();
        dtjsalist.search("").draw();
        let chatColumn = 0;
        let noteColumn = 1;
        let mobileChatNoteColumn = 2;

        dtjsalist.column(mobileChatNoteColumn).visible(false);
        dtjsalist.column(chatColumn).visible(false);
        dtjsalist.column(noteColumn).visible(false);

        $('#dtjsalist.cardview thead').addClass("export-grid-show");
        $('#dtjsalist').DataTable().buttons(0, 2).trigger();
        $('#dtjsalist.cardview thead').removeClass("export-grid-show");

        dtjsalist.column(mobileChatNoteColumn).visible(true);
        dtjsalist.column(chatColumn).visible(true);
        dtjsalist.column(noteColumn).visible(true);

        dtjsalist.search(searchValue).draw();
    });

    if (($(window).width() < MobileScreenSize)) {
        if ($(".tab-2").hasClass('active')) {
            $('.btnExportShowHideMobile').show();
        }
        else {
            $('.btnExportShowHideMobile').hide();
        }
    }
});

$('#StatusTree').click(function () {
    FilterCountSet(GetTreeNodeLength("#StatusTree"), "#statusFilterCount");
});

$('#SystemAreaTree').click(function () {
    FilterCountSet(GetTreeNodeLength("#SystemAreaTree"), "#systemAreaFilterCount");
});

$('#RiskFilterTree').click(function () {
    FilterCountSet(GetTreeNodeLength("#RiskFilterTree"), "#riskFilterCount");
});

//function cb(start, end) {
//    startDate = start.format("DD MMM YYYY");
//    endDate = end.format("DD MMM YYYY HH:mm:ss");
//    $("#dtrjsalist span").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
//    /*$("#tblspndtrinspectionlist").html(", " + start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));*/
//}

function ClearSelection() {

    jsaFilterCount = 0;
    $('#SelectedSystemAreaIds').val('');
    $('#SelectedStatusIds').val('');
    $('#SelectedRiskFilterIds').val('');
    $('#OverdueForClosure').val(false);

    SelectedStatus = '';
    SelectedSystemArea = '';
    SelectedRisk = '';
    SelectedOther = '';

    SetStatusTree();
    SetSystemAreaTree();
    SetRiskFilterTree();
    SetOverdueForClosure();

    isSearchClicked = false;
    FilterCountSet(0, ".filtercount");
}

//Will be called only in search click
//This will be called from search click which stores latest temp data when clicked on search button
function SetPageParameter() {
    ExportStageName = '';
    GetSystemAreaTree();
    GetStatusTree();
    GetRiskFilterTree();
    GetOverdueForClosure();

    var input = {
        "IsSearchClicked": true,
        "EncryptedVesselId": $('#EncryptedVesselId').val(),

        "SelectedStatus": $('#SelectedStatusIds').val(),
        "SelectedSystemArea": $('#SelectedSystemAreaIds').val(),
        "SelectedRiskFilter": $('#SelectedRiskFilterIds').val(),
        "OverdueForClosure": $("#OverdueForClosure").val(),
        "StageName": $('#StageName').val()
    }

    $.ajax({
        url: "/JSA/SetPageParameter",
        type: "POST",
        data: input,
        success: function (data) {
            UpdateHiddenField(data.data);

            jsaFilterCount = 0;
            AppendTreeFilterDataInModel("#StatusTree", "#filterStatus", "#filterCard1");
            AppendTreeFilterDataInModel("#SystemAreaTree", "#filterSystemArea", "#filterCard2");
            AppendTreeFilterDataInModel("#RiskFilterTree", "#filterRisk", "#filterCard3");

            var OverdueForClosure = $('input[name="OverdueForClosure"]:checked').next('label').text();
            AppendTextFilterDataInModel(OverdueForClosure, "#filterOther", "#filterCard4");
        },
        complete: function () {
            CallJSAList(true);
        }
    });
}

//To load filter with pre selected value
//To maintain tab in mobile ui
function SetFilters(data) {
    jsaFilterCount = 0;
    SetMobileTabWindow();
    SetStatusTree();
    SetSystemAreaTree();
    SetRiskFilterTree();
    SetOverdueForClosure();
}

function LoadGraph(dataset) {

    let data = {
        datasets: [{
            data: dataset,
            backgroundColor: ["#22B573", "#FA8E2E"]
        }],

        // These labels appear in the legend and in the tooltips when hovering different arcs
        labels: [
            'Low',
            'Medium & High',
        ]
    };

    if (document.getElementById("jsagraph")) {

        var type;

        type = "pie";

        var ctx6 = document.getElementById("jsagraph").getContext("2d");
        window.myHorizontalBar = new Chart(ctx6, {
            showTooltips: true,
            type: type,
            data: data,
            options: {
                responsive: true,
                title: {
                    display: false,
                    fontsize: 18,
                    fontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",
                    color: '#000000',
                    text: "Severity (Open)",
                    Position: 'right'
                },
                maintainAspectRatio: false,
                datasetFill: false,
                legend: {
                    display: true,
                    position: "right",
                    labels: {
                        fontColor: '#666666',
                        usePointStyle: true,
                        boxWidth: 6,
                        fontSize: 13,
                        generateLabels: (chart) => {
                            const datasets = chart.data.datasets;
                            return datasets[0].data.map((data, i) => ({
                                text: `${chart.data.labels[i]} ${data}`,
                                fillStyle: datasets[0].backgroundColor[i],
                            }));
                        }
                    },
                },
                hover: {
                    animationDuration: 1
                },

            },
        });
    }
}

function CallJSAList(IsSearchClicked) {
    isSearchClicked = IsSearchClicked;
    let data;
    if (IsSearchClicked) {
        data = SetSearchInput();
    }
    else {
        data = {
            "EncryptedVesselId": $("#EncryptedVesselId").val(),
            "StageName": $("#StageName").val()
        }
    }
    LoadJSAList(data);
}

function SetSearchInput() {
    var input = {
        "IsSearchClicked": true,
        "SelectedStatus": $('#SelectedStatusIds').val(),
        "SelectedSystemArea": $('#SelectedSystemAreaIds').val(),
        "SelectedRiskFilter": $('#SelectedRiskFilterIds').val(),
        "OverdueForClosure": $("#OverdueForClosure").val(),
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
    }
    return input;
}

function LoadJSAList(request) {

    $('#dtjsalist').DataTable().destroy();
    dtjsalist = $('#dtjsalist').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No data available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        //"data": jsadata,
        "ajax": {
            "url": "/JSA/GetJSAList",
            "type": "POST",
            "data": request,
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
                title: "Job Safety Analysis List",
                customize: function (xlsx) {
                    CustomizedExcelHeader(xlsx, 7);
                },

                messageTop: function () {

                    let filterText = '';

                    if (!IsNullOrEmptyOrUndefined(ExportStageName)) {
                        filterText += '\nStage : ' + ExportStageName;
                    }
                    else {
                        if (!IsNullOrEmptyOrUndefined(SelectedStatus)) {
                            if (SelectedStatus.includes("All", 0)) {
                                SelectedStatus = "All";
                            }
                            filterText += '\nStatus : ' + SelectedStatus;
                        }
                        if (!IsNullOrEmptyOrUndefined(SelectedSystemArea)) {
                            if (SelectedSystemArea.includes("All", 0)) {
                                SelectedSystemArea = "All";
                            }
                            filterText += '\nSystem Area : ' + SelectedSystemArea;
                        }
                        if (!IsNullOrEmptyOrUndefined(SelectedRisk)) {
                            if (SelectedRisk.includes("All", 0)) {
                                SelectedRisk = "All";
                            }
                            filterText += '\nRisk : ' + SelectedRisk;
                        }
                        if (!IsNullOrEmptyOrUndefined(SelectedOther)) {
                            filterText += '\nOther : ' + SelectedOther;
                        }
                    }

                    return 'Vessel : ' + $('#VesselName').val()
                        //+ '\nFrom Date : ' + startDate
                        //+ '\nTo Date : ' + moment(endDate, 'DD MMM YYYY HH:mm:ss').format("DD MMM YYYY")
                        + filterText
                        + '\n';
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
                        return GetChatBaseIcons(full.jobId, full.channelCount, full.messageDetailsJSON);
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
                        return GetNotesBaseIcons(full.jobId, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "top-margin-0 width-auto mr-2 data-text-align d-md-none d-lg-none d-xl-none",
                data: "refNo",
                width: "52px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetExportData(data);
                    }
                    return data;
                }
            },
            {
                className: "data-text-align d-md-none d-lg-none d-xl-none",
                data: "title",
                width: "280px",
                render: function (data, type, full, meta) {
                    let anchor = '<a href="/JSA/Details/?Source=1&JSADetails=' + full.jsaDetails + '&VesselId=' + $("#EncryptedVesselId").val() + '">' + GetExportData(data) + '</a>';
                    return anchor;
                }
            },
            {
                className: "data-icon-align tdblock d-md-none d-lg-none d-xl-none",
                orderable: false,
                width: "70px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0 || full.notesCount > 0) {
                        return GetChatNotesBaseIcons(full.jobId, full.channelCount, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "top-margin-0 width-auto mr-2 data-text-align d-none d-md-table-cell",
                data: "refNo",
                width: "52px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetExportData(data);
                    }
                    return data;
                }
            },
            {
                className: "data-text-align d-none d-md-table-cell",
                data: "title",
                width: "280px",
                render: function (data, type, full, meta) {
                    let anchor = '<a href="/JSA/Details/?Source=1&JSADetails=' + full.jsaDetails + '&VesselId=' + $("#EncryptedVesselId").val() + '">' + GetExportData(data) + '</a>';
                    return anchor;
                }
            },
            {
                className: "data-datetime-align",
                data: "startDate",
                width: "75px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDate(type, 'Start Date', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "endDate",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDate(type, 'End Date', data);
                }
            },
            {
                className: "data-text-align",
                data: "status",
                width: "140px",
                render: function (data, type, full, meta) {
                    let finalData = '';
                    if (statusColorMap.has(full.statusKPI)) {
                        finalData = '<span class=' + statusColorMap.get(full.statusKPI).textColor + '>' + data + '<span/>';
                    } else {
                        finalData = data;
                    }
                    return GetExportCellData('Status', finalData);
                }
            },
            {
                className: "data-text-align",
                data: "maxRisk",
                width: "95px",
                render: function (data, type, full, meta) {
                    let finalData = '';
                    if (riskColorMap.has(full.riskKPI)) {
                        finalData = '<span class=' + riskColorMap.get(full.riskKPI).textColor + '>' + data + '<span/>';
                    } else {
                        finalData = data;
                    }
                    return GetExportCellData('Max. Risk', finalData);
                }
            },
            {
                className: "tdblock data-text-align",
                data: "systemArea",
                width: "250px",
                render: function (data, type, full, meta) { return GetExportCellData('System Area', data); }
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
}

function BindSummary() {
    $.ajax({
        url: "/JSA/GetJSASummaryAndGraph",
        type: "POST",
        datatype: "JSON",
        data: {
            "vesselId": $("#EncryptedVesselId").val()
        },
        success: function (summary) {
            if (summary != null) {

                $("#totalCount").text(summary.totalCount);
                if (summary.totalCount == "0") {
                    ReplaceClass("#totalCount", 'txt-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalCount", 'txt-color-gray', 'txt-blue');
                }

                $("#lowCount").text(summary.residualRiskLowCount);
                if (summary.residualRiskLowCount == "0") {
                    ReplaceClass("#lowCount", 'txt-green', 'txt-color-gray');
                } else {
                    ReplaceClass("#lowCount", 'txt-color-gray', 'txt-green');
                }

                $("#midHighCount").text(summary.residualRiskMediumAndHighCount);
                if (summary.residualRiskMediumAndHighCount == "0") {
                    ReplaceClass("#midHighCount", 'text-yellow', 'txt-color-gray');
                } else {
                    ReplaceClass("#midHighCount", 'txt-color-gray', 'text-yellow');
                }

                $("#completedCount").text(summary.completedCount);
                if (summary.completedCount == "0") {
                    ReplaceClass("#completedCount", 'txt-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#completedCount", 'txt-color-gray', 'txt-blue');
                }

                $("#overdueClosureCount").text(summary.overdueForClosureCount);
                if (summary.overdueForClosureCount == "0") {
                    ReplaceClass("#overdueClosureCount", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#overdueClosureCount", 'txt-color-gray', 'txt-red');
                }

                $("#approvalPendingCount").text(summary.pendingOfficeApprovalCount);
                if (summary.pendingOfficeApprovalCount == "0") {
                    ReplaceClass("#approvalPendingCount", 'text-yellow', 'txt-color-gray');
                } else {
                    ReplaceClass("#approvalPendingCount", 'txt-color-gray', 'text-yellow');
                }

                $("#aTotalNav").click(function () {
                    SetSummaryFilterInTempData(summary.totalUrl)
                });

                $("#aLowNav").click(function () {
                    SetSummaryFilterInTempData(summary.lowUrl)
                });

                $("#aMidHighNav").click(function () {
                    SetSummaryFilterInTempData(summary.midHighUrl)
                });

                $("#aCompletedNav").click(function () {
                    SetSummaryFilterInTempData(summary.completedUrl)
                });

                $("#aOverdueNav").click(function () {
                    SetSummaryFilterInTempData(summary.overdueForClosureUrl)
                });

                $("#aPendOffAppNav").click(function () {
                    SetSummaryFilterInTempData(summary.pendingOfficeApprovalUrl)
                });

                let dataset = [summary.residualRiskLowCount, summary.residualRiskMediumAndHighCount]
                LoadGraph(dataset);
            }
        }
    });
}

//To maintain Summary selection in Session
function SetSummaryFilterInTempData(data) {
    $.ajax({
        url: "/JSA/SetSummaryFilterInTempData",
        type: "POST",
        dataType: "JSON",
        data: {
            "JSAUrl": data,
            "vesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data != null) {
                ClearSelection();
                UpdateHiddenField(data.data);
                SetFilters(data.data);
                SetMobileTabWindow();
                ExportStageName = $("#GridSubTitle").val();
            }
        },
        complete: function () {
            CallJSAList(false);
        }
    });
}

//To update hidden field value which are - VesselId, SelectedStageNAme
function UpdateHiddenField(data) {
    $('#ActiveMobileTabClass').val(data.activeMobileTabClass);
    $('#StageName').val(data.stageName);
    $('#GridSubTitle').val(data.gridSubTitle);


    if (data.selectedSystemArea != null) {
        $('#SelectedSystemAreaIds').val(data.selectedSystemArea.join(","));
    }

    if (data.selectedStatus != null) {
        $('#SelectedStatusIds').val(data.selectedStatus.join(","));
    }

    if (data.selectedRiskFilter != null) {
        $('#SelectedRiskFilterIds').val(data.selectedRiskFilter.join(","));
    }

    $("#OverdueForClosure").val(data.overdueForClosure);
}

function SetMobileTabWindow() {
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }
}

function MaintainFilters() {
    $.ajax({
        url: "/JSA/MaintainFilterParameters",
        type: "POST",
        success: function (data) {
            if (data.isTempDataExist) {
                var response = data.data;
                UpdateHiddenField(response);
                SetFilters(response);
                isSearchClicked = response.isSearchClicked;
            }
            else {
                isSearchClicked = false;

                var OverdueForClosure = $('input[name="OverdueForClosure"]:checked').next('label').text();
                AppendTextFilterDataInModel(OverdueForClosure, "#filterOther", "#filterCard4");
            }
        },
        complete: function () {
            CallJSAList(isSearchClicked);
        }
    });
}

function LoadStatusTree() {
    $("#StatusTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/JSA/GetJSAStatus",
            dataType: "json"
        }),
        init: function (event, data) {
            SetStatusTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function LoadRiskFilterTree() {
    $("#RiskFilterTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/JSA/GetRiskFilter",
            dataType: "json"
        }),
        init: function (event, data) {
            SetRiskFilterTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function SetStatusTree() {
    $("#StatusTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedStatusIds').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                SelectedStatus += node.title + ', ';
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });
    AppendTreeFilterDataInModel("#StatusTree", "#filterStatus", "#filterCard1");
}

function SetRiskFilterTree() {
    $("#RiskFilterTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedRiskFilterIds').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                SelectedRisk += node.title + ', ';
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });
    AppendTreeFilterDataInModel("#RiskFilterTree", "#filterRisk", "#filterCard3");
}

function LoadSystemAreaTree() {
    $("#SystemAreaTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/JSA/GetSystemArea",
            dataType: "json",
            data: {
                "encVesselId": $("#EncryptedVesselId").val()
            }
        }),
        init: function (event, data) {
            SetSystemAreaTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function SetSystemAreaTree() {
    $("#SystemAreaTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedSystemAreaIds').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                SelectedSystemArea += node.title + ', ';
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });
    AppendTreeFilterDataInModel("#SystemAreaTree", "#filterSystemArea", "#filterCard2");
}

function GetSystemAreaTree() {
    var tree = $('#SystemAreaTree');
    var nodes = tree.fancytree('getTree').getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    if (selectednodes.length > 0) {
        $('#SelectedSystemAreaIds').val(selectednodes.join(","));
    } else {
        $('#SelectedSystemAreaIds').val('');
    }
    SelectedSystemArea = nodes.map(x => x.title).join(", ");
}

function GetStatusTree() {
    var tree = $('#StatusTree');
    var nodes = tree.fancytree('getTree').getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    let ischecked = $('#chckOverdueForClosure').is(':checked');
    if (selectednodes.length > 1 && ischecked) {
        $('#chckOverdueForClosure').prop('checked', false);
    }
    $('#SelectedStatusIds').val(selectednodes.join(","));
    SelectedStatus = nodes.map(x => x.title).join(", ");
}

function GetRiskFilterTree() {
    var tree = $('#RiskFilterTree');
    var nodes = tree.fancytree('getTree').getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedRiskFilterIds').val(selectednodes.join(","));
    SelectedRisk = nodes.map(x => x.title).join(", ");
}

function SetOverdueForClosure() {
    if ($("#OverdueForClosure").val() == 'True' || $("#OverdueForClosure").val() == 'true' || $("#OverdueForClosure").val() == true) {
        $("#chckOverdueForClosure").prop('checked', true);
        SelectedOther = "Overdue For Closure";
    } else {
        $("#chckOverdueForClosure").prop('checked', false);
    }
    var OverdueForClosure = $('input[name="OverdueForClosure"]:checked').next('label').text();
    AppendTextFilterDataInModel(OverdueForClosure, "#filterOther", "#filterCard4");
}

function GetOverdueForClosure() {
    let ischecked = $('#chckOverdueForClosure').is(':checked');
    $("#OverdueForClosure").val(ischecked);
    if (ischecked) {
        SelectedOther = "Overdue For Closure";
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
    $('#appliedFilterCount').text(jsaFilterCount);
    $("#tableSubTitle").hide();
    var subtitle = $("#GridSubTitle").val();
    if (!IsNullOrEmptyOrUndefined(subtitle) && subtitle != "All") {
        //$("#tblspndtrjsalist").hide();
        $("#tableSubTitle").show();
        $("#tableSubTitle").text(" - " + subtitle);
    }
    else {
        //    $("#tblspndtrjsalist").show();
    }
}

function AppendTreeFilterDataInModel(treeSelector, filterId, filterCardId) {
    var StatusTree = $(treeSelector).fancytree('getTree').getSelectedNodes();
    var StatusTreeArray = GetUniqueChildArr(StatusTree);
    var StatusHtmlElement = GetFilterHtmlElement(StatusTreeArray);
    
    if (StatusTreeArray.size > 0) {
        $(filterId).html(StatusHtmlElement);
        jsaFilterCount = jsaFilterCount + StatusTreeArray.size;
        $('#appliedFilterCount').text(jsaFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).html("");
        $(filterCardId).hide();
    }

    if (treeSelector == "#StatusTree" ) {
        FilterCountSet(StatusTreeArray.size, "#statusFilterCount");
    }
    else if (treeSelector == "#SystemAreaTree" ) {
        FilterCountSet(StatusTreeArray.size, "#systemAreaFilterCount");
    }
    else if (treeSelector == "#RiskFilterTree" ) {
        FilterCountSet(StatusTreeArray.size, "#riskFilterCount");
    }

    hideShowFilterDesign();
}

function AppendTextFilterDataInModel(filteredValue, filterId, filterCardId) {

    if (!IsNullOrEmptyOrUndefined(filteredValue) && filteredValue !== undefined) {
        jsaFilterCount++;
        var htmlElement = "";
        htmlElement += '<div class="col-12 col-md-6 col-lg-4 col-xl-4">';
        htmlElement += '<div class="dashboard-counters-label"><span id="">' + filteredValue + '</span></div></div>';
        $(filterId).html(htmlElement);
        $('#appliedFilterCount').text(jsaFilterCount);
        $(filterCardId).show();
        FilterCountSet(1, "#otherFilterCount");
    }
    else {
        $(filterId).text("");
        $(filterCardId).hide();
        FilterCountSet(0, "#otherFilterCount");
    }
    hideShowFilterDesign();
}

function hideShowFilterDesign() {
    if (jsaFilterCount > 0) {
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
