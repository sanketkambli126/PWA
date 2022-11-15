import { createTree } from "jquery.fancytree";
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import moment from "moment";
require('bootstrap');

var startDate, endDate;
import { GetExportFormattedDate, GetExportCellData, CustomizedExcelHeader } from "../common/datatablefunctions.js";
import { AjaxError, GetCookie, AddLoadingIndicator, RemoveLoadingIndicator, MobileTab_Overview, ToastrAlert, Mobile_Tabs, MobileTab_List, ReplaceClass, BackButton, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, AddClassIfAbsent, RemoveClassIfPresent, datepickerheightinmobile, RegisterTabSelectionEvent } from '../common/utilities.js';
import { Tab1, Tab2, MobileScreenSize, LaptopScreenSize, HazOccListPageKey } from '../common/constants.js';
var dtHazOccList;


var statusTree;
var isSearchClicked = false;
var SelectedTypes = '';
var SelectedStatus = '';
var SelectedSeverity = '';
var ExportStageName = '';
var hazOccFilterCount = 0;

var colorMap = new Map();
colorMap.set(0, { textColor: "text-yellow" });
colorMap.set(1, { textColor: "txt-orange" });
colorMap.set(2, { textColor: "txt-green" });
colorMap.set(3, { textColor: "" });
colorMap.set(4, { textColor: "" });
colorMap.set(5, { textColor: "text-purple" });
colorMap.set(6, { textColor: "text-grey" });
colorMap.set(7, { textColor: "text-grey" });
colorMap.set(8, { textColor: "" });

var key = CryptoJS.enc.Utf8.parse('8080808080808080');
var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
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
    };
})();


function cb(start, end) {
    startDate = start.format("DD MMM YYYY");
    endDate = end.format("DD MMM YYYY HH:mm:ss");
    $("#dtrhazocclist").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
    $("#tblspndtrinspectionlist").html(", " + start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
}

$(document).on('click', '#aRemoveFilter', function () {
    ClearFilters();
    SetSummaryFilterInTempData($("#ClearButtonUrl").val(), BindHazoccSummary);
});

InitializeListDiscussionAndNoteClickEvents(code);

$(document).ready(function () {
    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();
    RegisterTabSelectionEvent('.mobileTabClick', HazOccListPageKey);
    LoadTypeTree();
    LoadStatusTree();
    LoadSeverityTree();

    ExportStageName = $("#StageDescription").val();
    isSearchClicked = $('#IsSearchedClick').val();


    //Sidebar back
    BackButton(HazOccListPageKey, true);

    //this sorting type considers time aswell , please refer to Date Constructor for acceptable datetime formats
    //not being used at the moment due to change in the requirement
    $.fn.dataTable.ext.oSort["datetime1-asc"] = function (a, b) {
        a = removeTags(a);
        b = removeTags(b);

        var first = moment(new Date(a));
        var second = moment(new Date(b));

        if (first < second) {
            return -1;
        } else {
            return 1;
        }
    };

    $.fn.dataTable.ext.oSort["datetime1-desc"] = function (a, b) {
        a = removeTags(a);
        b = removeTags(b);
        var first = moment(new Date(a));
        var second = moment(new Date(b));

        if (first < second) {
            return 1;
        } else {
            return -1;
        }
    };

    //daterangepicker hazocclist

    var start = moment($('#StartDate').val(), 'DD-MM-YYYY');
    var end = moment($('#EndDate').val(), 'DD-MM-YYYY HH:mm:ss');

    $("#dtrhazocclist").caleran(
        {
            showButtons: true,
            hideOutOfRange: true,
            showOn: "top",
            arrowOn: "right",
            startEmpty: false,
            startDate: start,
            endDate: end,
            format: "DD MMM YYYY",
            ranges: [
                {
                    title: "Today",
                    startDate: moment(),
                    endDate: moment()
                },
                {
                    title: "Yesterday",
                    startDate: moment().subtract(1, "day"),
                    endDate: moment()
                },
                {
                    title: "Last 7 Days",
                    startDate: moment().subtract(6, "day"),
                    endDate: moment()
                },
                {
                    title: "Last 30 Days",
                    startDate: moment().subtract(29, "day"),
                    endDate: moment()
                },
                {
                    title: "This Month",
                    startDate: moment().startOf("month"),
                    endDate: moment().endOf("month")
                },
                {
                    title: "Last Month",
                    startDate: moment().subtract(1, "month").startOf("month"),
                    endDate: moment().subtract(1, "month").endOf("month")
                },
                {
                    title: "Last 6 Months",
                    startDate: moment().subtract(6, "month"),
                    endDate: moment()
                },

            ],
            rangeOrientation: "vertical",
            onafterselect: function (caleran, startDate, endDate) {
                setDateDetails(startDate, endDate);
            },
            oncancel: function (caleran, start, end) {
                setDateDetailonCancel(start, end);
            }
        }
    );

    cb(start, end);

    function setDateDetails(SelectedstartDate, SelectedendDate) {

        startDate = SelectedstartDate.format("DD MMM YYYY");
        endDate = SelectedendDate.format("DD MMM YYYY HH:mm:ss");
        $("#dtrhazocclist").html(SelectedstartDate.format("DD MMM YYYY") + " - " + SelectedendDate.format("DD MMM YYYY"));
        $("#tblspndtrinspectionlist").html(", " + SelectedstartDate.format("DD MMM YYYY") + " - " + SelectedendDate.format("DD MMM YYYY"));

        BindHazoccSummary();
        SetPageParameter();

    }

    MaintainFilters();

    GetNearMissGraphData();
    $('.height-equal').matchHeight();

    $("#btnSearch").click(function () {
        SetPageParameter();
    });

    $("#btnClear").click(function () {
        ClearFilters();
        SetSummaryFilterInTempData($("#ClearButtonUrl").val(), BindHazoccSummary);
    });

    SetMobileTabWindow();

    $('.btnExport').click(() => {
        var searchValue = dtHazOccList.search();
        dtHazOccList.search("").draw();
        let chatColumn = 0;
        let noteColumn = 1;
        let mobileChatNoteColumn = 2;

        dtHazOccList.column(mobileChatNoteColumn).visible(false);
        dtHazOccList.column(chatColumn).visible(false);
        dtHazOccList.column(noteColumn).visible(false);

        $('#dtHazOccList.cardview thead').addClass("export-grid-show");
        $('#dtHazOccList').DataTable().buttons(0, 2).trigger();
        $('#dtHazOccList.cardview thead').removeClass("export-grid-show");

        dtHazOccList.column(mobileChatNoteColumn).visible(true);
        dtHazOccList.column(chatColumn).visible(true);
        dtHazOccList.column(noteColumn).visible(true);

        dtHazOccList.search(searchValue).draw();
    });
});

$('#StatusTree').click(function () {
    FilterCountSet(GetTreeNodeLength("#StatusTree"), "#statusFilterCount");
});

$('#TypeTree').click(function () {
    FilterCountSet(GetTreeNodeLength("#TypeTree"), "#typeFilterCount");
});

$('#SeverityTree').click(function () {
    FilterCountSet(GetTreeNodeLength("#SeverityTree"), "#severityFilterCount");
});

//To update HiddenField, LoadGrid, SetSelectedFilterValue
function MaintainFilters() {
    $.ajax({
        url: "/HazOcc/MaintainFilterParameters",
        type: "POST",
        success: function (data) {
            if (data.isTempDataExist) {
                var response = data.data;
                UpdateHiddenField(response);
                hazOccFilterCount = 0;
                SetTypeTree();
                SetSeverityTree();
                SetStatusTree();

                isSearchClicked = response.isSearchedClick;
            }
            else {
                isSearchClicked = false;
            }
        },
        complete: function () {
            LoadHazOccList(isSearchClicked);
            BindHazoccSummary();
        }
    });
}

//To update hidden field value which are - VesselId, SelectedStageNAme
function UpdateHiddenField(data) {
    $("#ActiveMobileTabClass").val(data.activeMobileTabClass);
    $('#StageName').val(data.stageName);
    $('#StageDescription').val(data.stageDescription);
    $('#GridSubTitle').val(data.gridSubTitle);
    $('#StartDate').val(data.startDate);
    $('#EndDate').val(data.endDate);
    $('#IsSearchedClicked').val(data.isSearchedClicked);

    if (data.selectedIncidentTypes != null) {
        $('#SelectedIncidentTypes').val(data.selectedIncidentTypes);
    }

    if (data.selectedIncidentSeveritys != null) {
        $('#SelectedIncidentSeveritys').val(data.selectedIncidentSeveritys);
    }

    if (data.selectedIncidentStatus != null) {
        $('#SelectedIncidentStatus').val(data.selectedIncidentStatus);
    }

    //updating date local variables since the list is using those instead of hidden fields
    cb(moment(new Date(data.startDate)), moment(new Date(data.endDate)));
}

//Will be called only in search click
//This will be called from search click which stores latest temp data when clicked on search button
function SetPageParameter() {
    ExportStageName = '';
    GetTypeTree();
    GetSeverityTree();
    GetStatusTree();

    var input = {
        "EncryptedVesselId": $("#EncryptedVesselId").val(),
        "StartDate": startDate,
        "EndDate": endDate,
        "StageName": $("#StageName").val(),
        "SelectedIncidentTypes": $('#SelectedIncidentTypes').val(),
        "SelectedIncidentSeveritys": $('#SelectedIncidentSeveritys').val(),
        "SelectedIncidentStatus": $('#SelectedIncidentStatus').val(),
        "IsSearchedClick": true
    };

    $.ajax({
        url: "/HazOcc/SetPageParameter",
        type: "POST",
        data: input,
        success: function (data) {
            UpdateHiddenField(data.data);

            hazOccFilterCount = 0;
            AppendTreeFilterDataInModel("#SeverityTree", "#filterSeverity", "#filterCard3");
            AppendTreeFilterDataInModel("#TypeTree", "#filterType", "#filterCard2");
            AppendTreeFilterDataInModel("#StatusTree", "#filterStatus", "#filterCard1");

            //SetMobileTabWindow();
        },
        complete: function () {
            LoadHazOccList(true);
        }
    });
}

//To maintain Summary selection in Session
function SetSummaryFilterInTempData(data, callAfterSuccess) {
    $.ajax({
        url: "/HazOcc/SetSummaryFilterInTempData",
        type: "POST",
        dataType: "JSON",
        data: {
            "hazOccUrl": data,
            "vesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data != null) {
                ClearFilters();
                UpdateHiddenField(data.data);
                SetFilters();

                SetMobileTabWindow();
                if (callAfterSuccess != null && callAfterSuccess != 'undefined') {
                    callAfterSuccess();
                }
                ExportStageName = $("#StageDescription").val();
            }
        },
        complete: function () {
            LoadHazOccList(false);
        }
    });
}

function GetNearMissGraphData() {
    $.ajax({
        url: "/HazOcc/GetNearMissLtmData",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedVesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data != null) {

                GetSafeActGraphData(data);
            }
        }
    });
}

function GetSafeActGraphData(nearMissData) {
    $.ajax({
        url: "/HazOcc/GetSafeActAndConditionLtmData",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedVesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data != null) {
                GetUnSafeActGraphData(nearMissData, data);
            }
        }
    });
}

function GetUnSafeActGraphData(nearMissData, safeActData) {
    $.ajax({
        url: "/HazOcc/GetUnsafeActAndConditionLtmData",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedVesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data != null) {
                LoadGraph(nearMissData, safeActData, data);
            }
        }
    });
}

function LoadGraph(nearMissData, safeActData, unSafeActData) {

    var nearMiss = [], safeAct = [], unSafeAct = [], months = [];

    for (var i = 0; i < nearMissData[0].monthTotal.length; i++) {
        months.push(nearMissData[0].monthTotal[i].month);
        nearMiss.push(parseInt(nearMissData[0].monthTotal[i].count));
        safeAct.push(parseInt(safeActData[0].monthTotal[i].count));
        unSafeAct.push(parseInt(unSafeActData[0].monthTotal[i].count));
    }

    if (screen.width < LaptopScreenSize) {
        $("#hazoccreportchart")[0].height = 150;
    }

    if (document.getElementById("hazoccreportchart")) {


        var BarChartData = {
            labels: months,
            datasets: [
                {
                    label: "Safe Act/Cond",
                    backgroundColor: "rgb(50, 177, 110)",
                    data: safeAct,
                },
                {
                    label: "Near Miss",
                    backgroundColor: "rgb(247, 129, 41)",
                    data: nearMiss,
                },
                {
                    label: "Unsafe Act/Cond",
                    backgroundColor: "rgb(245, 69, 69)",
                    data: unSafeAct,
                },

            ],
        };
        var type;

        type = "bar";

        var ticksvar = {
            display: true,
            autoSkip: true,
            maxRotation: 0,
            minRotation: 0,
        };
        if (screen.width < MobileScreenSize) {
            ticksvar = {
                display: true,
                autoSkip: true,
                maxRotation: 90,
                minRotation: 90
            };
        };

        var ctx6 = document.getElementById("hazoccreportchart").getContext("2d");
        window.myHorizontalBar = new Chart(ctx6, {
            showTooltips: false,
            type: type,
            data: BarChartData,
            options: {
                elements: {
                    rectangle: {
                        borderWidth: 0,
                    },
                },
                responsive: true,
                maintainAspectRatio: false,
                datasetFill: false,
                legend: {
                    position: "right",
                    labels: {
                        boxWidth: 8,
                    },
                },
                scales: {
                    xAxes: [{
                        gridLines: {
                            display: false,
                        },
                        ticks: ticksvar,
                        maxBarThickness: 12,
                    }],
                    yAxes: [{
                        gridLines: {
                            display: true,
                        },
                        ticks: {
                            display: true,
                            autoSkip: true,
                            stepSize: 10,
                            max: 30
                        },
                    }]
                },
                tooltips: {
                    enabled: true,
                    mode: 'nearest',
                    callbacks: {
                        title: function (tooltipItems, data) {
                            return data.datasets[tooltipItems[0].datasetIndex].label;
                        },
                        label: function (tooltipItems, data) {
                            let val = '';
                            val = tooltipItems.xLabel;
                            return val + " : " + tooltipItems.value;
                        }
                    }
                },
                hover: {
                    animationDuration: 1
                },

            },
        });
    }
}

function removeTags(str) {
    if ((str === null) || (str === ''))
        return false;
    else
        str = str.toString();
    return str.replace(/(<([^>]+)>)/ig, '');
}
function LoadHazOccList(isSearchClicked = false) {

    let input = {
        "EncryptedVesselId": $("#EncryptedVesselId").val(),
        "StartDate": startDate,
        "EndDate": endDate,
        "StageName": $("#StageName").val(),
        "SelectedIncidentTypes": $('#SelectedIncidentTypes').val(),
        "SelectedIncidentSeveritys": $('#SelectedIncidentSeveritys').val(),
        "SelectedIncidentStatus": $('#SelectedIncidentStatus').val(),
        "IsSearchedClick": isSearchClicked
    };
    $('#dtHazOccList').DataTable().destroy();
    dtHazOccList = $('#dtHazOccList').DataTable({
        //"dom": '<"row"<"col-12 col-md-6 col-lg-6 col-xl-6"i><"col-12 col-md-6 col-lg-6 col-xl-6 search-filter"f>>' +
        //        '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "order": [],
        "pageLength": 25,
        "language": {
            "emptyTable": "No orders available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/HazOcc/GetHazOccs",
            "type": "POST",
            "data": {
                "request": input
            },
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
                title: "HazOcc List",
                customize: function (xlsx) {
                    CustomizedExcelHeader(xlsx, 10);
                },

                messageTop: function () {

                    let filterText = '';

                    if (!IsNullOrEmptyOrUndefined(ExportStageName)) {
                        filterText += '\nStage :' + ExportStageName;
                    } else {
                        if (!IsNullOrEmptyOrUndefined(SelectedTypes)) {
                            if (SelectedTypes.includes("All", 0)) {
                                SelectedTypes = "All";
                            }
                            filterText += '\nTypes :' + SelectedTypes;
                        }
                        if (!IsNullOrEmptyOrUndefined(SelectedStatus)) {
                            if (SelectedStatus.includes("All", 0)) {
                                SelectedStatus = "All";
                            }
                            filterText += '\nStatus :' + SelectedStatus;
                        }
                        if (!IsNullOrEmptyOrUndefined(SelectedSeverity)) {
                            if (SelectedSeverity.includes("All", 0)) {
                                SelectedSeverity = "All";
                            }
                            filterText += '\nSeverity :' + SelectedSeverity;
                        }
                    }

                    return 'Vessel : ' + $('#VesselName').val()
                        + '\nFrom Date : ' + startDate
                        + '\nTo Date : ' + moment(endDate, 'DD MMM YYYY HH:mm:ss').format("DD MMM YYYY")
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
                        return GetChatBaseIcons(full.identifier, full.channelCount, full.messageDetailsJSON);
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
                        return GetNotesBaseIcons(full.identifier, full.notesCount, full.messageDetailsJSON);
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
                        return GetChatNotesBaseIcons(full.identifier, full.channelCount, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "top-margin-0 tdblock data-datetime-align",
                data: "incidentDate",
                type: "date",
                width: "75px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDate(type, 'Occurrence Date', data);
                }
            },
            {
                className: "data-text-align",
                data: "shipReferenceNumber",
                width: "65px",
                render: function (data, type, full, meta) {
                    var HazOccDetailsurl = "/HazOcc/Details/?HazOccDetails=" + full.hazOccDetailsUrlData + '&VesselId=' + $("#EncryptedVesselId").val();
                    var cellData = '<a href="' + HazOccDetailsurl + '" class="align-bottom"> ' + GetExportCellData('Report Number', data) + '</a>'

                    if (full.hasParent) {
                        cellData = cellData + '<span class="ml-1 badge badge-pill status-badge badge-yellow-text-white" title="" data-toggle="tooltip" data-placement="bottom" data-original-title="Child">C</span>'
                    }
                    else if (full.hasChildReports) {
                        cellData = cellData + '<span class="ml-1 badge badge-pill status-badge badge-purple-text-white" title="" data-toggle="tooltip" data-placement="bottom" data-original-title="Parent">P</span>'
                    }

                    if (full.mappedDefectCount > 0) {
                        cellData = cellData + '<img src="/images/hazOcc-defect.png" class="ml-1" width="15" title="" data-toggle="tooltip" data-placement="bottom" data-original-title="Mapped defect">'
                    }

                    return cellData;
                }

            },
            {
                className: "data-text-align",
                data: "status",
                width: "100px",
                render: function (data, type, full, meta) {
                    let finalData = '';
                    if (colorMap.has(full.statusKPI)) {
                        finalData = '<span class=' + colorMap.get(full.statusKPI).textColor + '>' + data + '<span/>';
                    } else {
                        finalData = data;
                    }
                    return GetExportCellData('Status', finalData);
                }
            },
            {
                className: "data-text-align",
                data: "severity",
                width: "70px",
                render: function (data, type, full, meta) {
                    let finalData = '';
                    if (data == "Serious" || data == "Very Serious") {
                        finalData = '<span class="txt-red">' + data + '<span/>';
                    } else {
                        finalData = data;
                    }
                    return GetExportCellData('Severity <img src="/images/hazoc-info.png" class=" ml-1" title="" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="Actual Severity for Accidents & Incidents. Potential Severity for Near Miss."/>', finalData);
                }
            },
            {
                className: "data-text-align",
                data: "type",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportCellData('Type', data);
                }
            },
            {
                className: "data-text-align",
                data: "category",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetExportCellData('Category', data);
                }
            },
            {
                className: "data-text-align",
                data: "class",
                width: "155px",
                render: function (data, type, full, meta) {
                    return GetExportCellData('Class', data);
                }
            },
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
    $('#dtHazOccList').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip(
            {
                container: 'body',
                trigger: 'hover'
            }
        );
    });
}

function LoadStatusTree() {
    $("#StatusTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/HazOcc/GetStatusFilter",
            dataType: "json",
            success: function (data) {
                statusTree = data;
            }
        }),
        select: function (event, data) {
            if (data.node.key === "deleted") {
                let node = data.node.getNextSibling();
                if (data.node.isSelected() == true) {
                    node.setSelected(false);
                } else {
                    node.setSelected(true);
                }
            }
            else if (data.node.key === "") {
                let node = data.node.getPrevSibling();
                if (data.node.isSelected() == true) {
                    node.setSelected(false);
                }
            }
        },
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

function LoadTypeTree() {
    $("#TypeTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/HazOcc/GetTypeFilter",
            dataType: "json",
            type: "POST",
            data: {
                encryptedVesselId: $("#EncryptedVesselId").val()
            }
        }),
        init: function (event, data) {
            SetTypeTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function LoadSeverityTree() {
    $("#SeverityTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/HazOcc/GetSeverityFilter",
            dataType: "json"
        }),
        init: function (event, data) {
            SetSeverityTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function GetTypeTree() {
    var nodes = $.ui.fancytree.getTree("#TypeTree").getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedIncidentTypes').val(selectednodes.join(","));
    SelectedTypes = nodes.map(x => x.title).join(",");
}

function GetStatusTree() {
    var nodes = $.ui.fancytree.getTree("#StatusTree").getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedIncidentStatus').val(selectednodes.join(","));
    SelectedStatus = nodes.map(x => x.title).join(",");
}

function GetSeverityTree() {
    var nodes = $.ui.fancytree.getTree("#SeverityTree").getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedIncidentSeveritys').val(selectednodes.join(","));
    SelectedSeverity = nodes.map(x => x.title).join(",");
}

function ClearTypeTree() {
    $.ui.fancytree.getTree("#TypeTree").visit(function (node) {
        node.setSelected(false);
    });
    SelectedTypes = '';
}

function ClearStatusTree() {
    $('#StatusTree').fancytree('option', 'source', statusTree);
    SelectedStatus = '';
}

function ClearSeverityTree() {
    $.ui.fancytree.getTree("#SeverityTree").visit(function (node) {
        node.setSelected(false);
    });
    SelectedSeverity = '';
}

function ClearFilters() {
    $('#SelectedIncidentTypes').val('');
    $('#SelectedIncidentStatus').val('');
    $('#SelectedIncidentSeveritys').val('');
    ClearSeverityTree();
    ClearStatusTree();
    ClearTypeTree();
    FilterCountSet(0, ".filtercount");
}

function BindHazoccSummary() {

    var start = startDate;
    var end = endDate;


    var request =
    {
        "VesselId": $('#EncryptedVesselId').val(),
        "StartDate": start,
        "EndDate": end
    };
    $.ajax({
        url: "/HazOcc/HazOccSummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            if (data != null) {

                $("#totalCrewAccidents").text(data.crewAccidentsCount);
                if (data.crewAccidentsCount == "0") {
                    ReplaceClass("#totalCrewAccidents", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalCrewAccidents", 'txt-color-gray', 'txt-red');
                }

                $("#totalIncidents").text(data.incidentCount);
                if (data.incidentCount == "0") {
                    ReplaceClass("#totalIncidents", 'text-yellow', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalIncidents", 'txt-color-gray', 'text-yellow');
                }

                $("#totalNMSO").text(data.nearMissObservationCount);
                if (data.nearMissObservationCount == "0") {
                    ReplaceClass("#totalNMSO", 'txt-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalNMSO", 'txt-color-gray', 'txt-blue');
                }

                $("#totalPassengerAccidents").text(data.passengerAccidentsCount);
                if (data.passengerAccidentsCount == "0") {
                    ReplaceClass("#totalPassengerAccidents", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalPassengerAccidents", 'txt-color-gray', 'txt-red');
                }

                $("#totalCount").text(data.totalCount);
                if (data.totalCount == "0") {
                    ReplaceClass("#totalCount", 'txt-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalCount", 'txt-color-gray', 'txt-blue');
                }

                $("#ltiCount").text(data.ltiCount);
                if (data.ltiCount == "0") {
                    ReplaceClass("#ltiCount", 'text-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#ltiCount", 'txt-color-gray', 'text-blue');
                }


                $("#trcCount").text(data.trcCount);
                if (data.trcCount == "0") {
                    ReplaceClass("#trcCount", 'text-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#trcCount", 'txt-color-gray', 'text-blue');
                }

                $("#mexphsCount").text(data.mExpHrsCrw);
                if (data.mExpHrsCrw == "0") {
                    ReplaceClass("#mexphsCount", 'txt-blue', 'txt-color-gray');
                } else {
                    ReplaceClass("#mexphsCount", 'txt-color-gray', 'txt-blue');
                }

                $("#fatality").text(data.fatalityCount);
                if (data.fatalityCount == "0") {
                    ReplaceClass("#fatality", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#fatality", 'txt-color-gray', 'txt-red');
                }

                $("#verySerious").text(data.verySeriousCount);
                if (data.verySeriousCount == "0") {
                    ReplaceClass("#verySerious", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#verySerious", 'txt-color-gray', 'txt-red');
                }

                $("#totalThirdPartyAccidents").text(data.thirdPartyAccidentsCount);
                if (data.thirdPartyAccidentsCount == "0") {
                    ReplaceClass("#totalThirdPartyAccidents", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalThirdPartyAccidents", 'txt-color-gray', 'txt-red');
                }

                $("#totalIllness").text(data.illnessCount);
                if (data.illnessCount == "0") {
                    ReplaceClass("#totalIllness", 'txt-red', 'txt-color-gray');
                } else {
                    ReplaceClass("#totalIllness", 'txt-color-gray', 'txt-red');
                }

                $("#aTotalCount").off();
                $("#aTotalCount").click(function () {
                    SetSummaryFilterInTempData(data.totalCountUrl);
                });

                $("#aTotalPassengerAccidents").off();
                $("#aTotalPassengerAccidents").click(function () {
                    SetSummaryFilterInTempData(data.totalPassengerAccidentUrl);
                });

                $("#aTotalCrewAccidents").off();
                $("#aTotalCrewAccidents").click(function () {
                    SetSummaryFilterInTempData(data.totalCrewAccidentsUrl);
                });

                $("#aTotalIncidents").off();
                $("#aTotalIncidents").click(function () {
                    SetSummaryFilterInTempData(data.totalIncidentsUrl);
                });

                $("#aTotalNMSO").off();
                $("#aTotalNMSO").click(function () {
                    SetSummaryFilterInTempData(data.totalNearMissObservationsUrl);
                });

                $("#aFatality").off();
                $("#aFatality").click(function () {
                    SetSummaryFilterInTempData(data.totalFatalitiesURL);
                });

                $("#aVerySerious").off();
                $("#aVerySerious").click(function () {
                    SetSummaryFilterInTempData(data.totalVerySeriousURL);
                });

                $("#aLTI").off();
                $("#aLTI").click(function () {
                    SetSummaryFilterInTempData(data.ltiUrl);
                });

                $("#aTRC").off();
                $("#aTRC").click(function () {
                    SetSummaryFilterInTempData(data.trcUrl);
                });

                $("#aThirdPartyAccidents").off();
                $("#aThirdPartyAccidents").click(function () {
                    SetSummaryFilterInTempData(data.thirdPartyAccidentsUrl);
                });

                $("#aIllness").off();
                $("#aIllness").click(function () {
                    SetSummaryFilterInTempData(data.illnessUrl);
                });
            }
        }
    });
}

function SetSeverityTree() {
    $("#SeverityTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedIncidentSeveritys').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                node.setSelected(true);
                SelectedSeverity += node.title + ',';
            }
            else {
                node.setSelected(false);
            }
        });
    });

    AppendTreeFilterDataInModel("#SeverityTree", "#filterSeverity", "#filterCard3");
}

function SetTypeTree() {

    var treeIdsList = $('#SelectedIncidentTypes').val().split(',');

    $("#TypeTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedIncidentTypes').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                SelectedTypes += node.title + ',';
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });

    AppendTreeFilterDataInModel("#TypeTree", "#filterType", "#filterCard2");
}

function SetStatusTree() {
    $("#StatusTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedIncidentStatus').val().split(',');
        treeIdsList.forEach(function () {
            console.table("SetStatusTree == ", treeIdsList);
            if (treeIdsList.includes(node.key) && node.key !== "") {
                SelectedStatus += node.title + ',';
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });

    AppendTreeFilterDataInModel("#StatusTree", "#filterStatus", "#filterCard1");
}

function SetMobileTabWindow() {
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }
}

function SetFilters() {
    hazOccFilterCount = 0;
    SetSeverityTree();
    SetStatusTree();
    SetTypeTree();
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
        htmlElement += '<div class="col-12 col-md-12 col-lg-12 col-xl-12">';
        htmlElement += '<div class="dashboard-counters-label"><span id="">' + value + '</span></div></div></div>';
    });

    return htmlElement;
}

function AppendGridTitle() {

    hideShowFilterDesign();

    $("#tableSubTitle").hide();
    var subtitle = $("#GridSubTitle").val();
    if (!IsNullOrEmptyOrUndefined(subtitle) && subtitle != "All") {
        $("#tblspndtrinspectionlist").hide();
        $("#tableSubTitle").show();
        $("#tableSubTitle").text(" - " + subtitle);
    }
    else {
        $("#tblspndtrinspectionlist").show();
    }
}

function AppendTreeFilterDataInModel(treeSelector, filterId, filterCardId) {
    var StatusTree = $(treeSelector).fancytree('getTree').getSelectedNodes();
    var StatusTreeArray = GetUniqueChildArr(StatusTree);
    var StatusHtmlElement = GetFilterHtmlElement(StatusTreeArray);

    if (StatusTreeArray.size > 0) {
        $(filterId).html(StatusHtmlElement);
        hazOccFilterCount = hazOccFilterCount + StatusTreeArray.size;
        $('#appliedFilterCount').text(hazOccFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).html("");
        $(filterCardId).hide();
    }

    if (treeSelector == "#StatusTree") {
        FilterCountSet(StatusTreeArray.size, "#statusFilterCount");
    } else if (treeSelector == "#TypeTree") {
        FilterCountSet(StatusTreeArray.size, "#typeFilterCount");
    } else if (treeSelector == "#SeverityTree") {
        FilterCountSet(StatusTreeArray.size, "#severityFilterCount");
    }

    hideShowFilterDesign();
}

function hideShowFilterDesign() {
    if (hazOccFilterCount > 0) {
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
