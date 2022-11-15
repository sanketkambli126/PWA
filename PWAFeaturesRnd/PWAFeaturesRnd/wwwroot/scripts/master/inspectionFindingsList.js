import "bootstrap";
import toastr from "toastr";
import moment from "moment";
import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader } from "../common/datatablefunctions.js";
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, FormatDate, txtReadMore, headerReadMoreFindings, SetHeaderMargin, GetCookie, ToastrAlert, BackButton, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, InitializeListDiscussionAndNoteClickEvents, GetChatNotesBaseIcons, GetNotesBaseIcons, GetChatBaseIcons, IsNullOrEmptyOrUndefinedLooseTyped, GetRoleRightsAsync, GlobalAjaxCall, IsJsonString, RemoveClassIfPresent, RegisterTabSelectionEvent  } from "../common/utilities.js";
import { InspectionFindingPageKey, MobileScreenSize } from '../common/constants.js';
import { RecordLevelNote } from "../common/notesUtilities.js";

var dtgrid;
var InspectionFindingFilter;
var documentLoaded = false;

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

var CloseInspectionControlId = "244C0560-E9E4-4146-A41E-859826CC9EBA";

$(document).ready(function () {
    AjaxError();
    var val = $('#IsFromViewRecord').val();
    if (val == 'True' || val == 'true' || val == true) {
        if ($(window).width() > 767) {
            $('body').addClass("hideleftmenuheader");
        }
        else {
            $('.app-container .logo-src, .app-container .aBaseNotification, .app-container .mobile-toggle-header-nav, .mobile-header-back, .vesseldropdownmobile').hide();
            $('.app-header__mobile-menu .header-dots').css("visibility", "hidden");
            RemoveClassIfPresent('.backclose', 'd-none');
        }
    }

    $('.backclose').click(function () {
        window.close();
    });
    BackButton(InspectionFindingPageKey, false);

    //Inspection closure
    InspectionDetails();

    //role right
    ActionBasedOnRoleRight();
    RegisterTabSelectionEvent('.mobileTabClick', InspectionFindingPageKey);
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }
    //Inspection closure actions
    //validations on button click

    $('.btnInspectionClosure').click(function () {

        $("#inspectionClosureActionModal").modal('show');
        $('#yesInspectionClosureAction').off();
        $('#yesInspectionClosureAction').on('click', function () {
            IsVesselInManagement();
            $("#inspectionClosureActionModal").modal('hide');
        });

        $('#noInspectionClosureAction').off();
        $('#noInspectionClosureAction').on('click', function () {
            $("#inspectionClosureActionModal").modal('hide');
        });
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);

    GetDiscussionNotesCount(messageDetailsJSON);

    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);

    InitializeListDiscussionAndNoteClickEvents(code);

    $(document).click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }
    });

    $('#mobiletoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });

    AddLoadingIndicator();
    RemoveLoadingIndicator();
    var inspectionUrl = $('#InspectionUrl').val();
    fetchInspectionAndInspectorDetails(inspectionUrl);

    BindSummary();

    InspectionFindingFilter = $('#InspectionFindingFilter').val() == undefined ? "AllFindings" : $('#InspectionFindingFilter').val();

    $('#csall, #cscleared, #csoutstanding, #csover').prop('checked', false);

    if (InspectionFindingFilter == "AllFindings") {
        $('#csall').prop('checked', true);
    }
    else if (InspectionFindingFilter == "Cleared") {
        $('#cscleared').prop('checked', true);
    }
    else if (InspectionFindingFilter == "Outstanding") {
        $('#csoutstanding').prop('checked', true);
    }
    else if (InspectionFindingFilter == "Overdue") {
        $('#csover').prop('checked', true);
    }

    LoadGrid(InspectionFindingFilter);

    $('input[type=radio][name=selFindingStatus]').change(function () {
        var FindingStatusval = $('input[name="selFindingStatus"]:checked').val();
        $('#SelectedStageFilter').val(FindingStatusval);
        LoadGrid(FindingStatusval);
    });

    $('.btnExport').click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }

        ExportToExcel();
        ////var searchValue = dtgrid.search();
        ////dtgrid.search("").draw();

        ////$('#dtfindings.cardview thead').addClass("export-grid-show");
        ////$('#dtfindings').DataTable().buttons(0, 2).trigger();
        ////$('#dtfindings.cardview thead').removeClass("export-grid-show");

        ////dtgrid.search(searchValue).draw();
    });


    $(document).ajaxStop(function () {
        if (!documentLoaded) {
            if (screen.width < MobileScreenSize) {
                headerReadMoreFindings('headershowmorewrapper', 'header');
            }
            SetHeaderMargin();
            documentLoaded = true;
        }
    });

    $(document).on('click', "#inspectionHeader", function () {
        CallInspectionListPageFromSession();
    });

    LoadDownloadReportDefaultDetails();

    $('.btnDownloadReportOpen').click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }

        $('#modalDownloadInspectionReport').modal('show');
    });

    $('input[type=radio][name=reportType]').change(function () {
        var reportType = $('input[name="reportType"]:checked').val();
        if (reportType == 'QaDetail') {
            $('#selFormat').attr("disabled", true);
            var select = document.getElementById('selFormat');
            select.selectedIndex = 0;
        }
        else {
            $('#selFormat').attr("disabled", false);
        }
    });

    $('#btnDownloadReport').click(function () {
        DownloadReport();
    });

});

function LoadGrid(inspectionFindingFilter) {
    var data = {
        "inspectionFindingFilter": inspectionFindingFilter,
        "inspectionUrl": $('#InspectionUrl').val(),
        "VesselId": $('#VesselId').val(),
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
        "inspectionType": $('#InspectionType').val(),
    }
    var IsPscVisible = $('#IsPscVisible').val();
    var IsOMVType = $('#IsOMVType').val();
    var IsCausesSectionVisible = $('#IsCausesSectionVisible').val();

    $('#dtfindings').DataTable().destroy();
    dtgrid = $('#dtfindings').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-5 offset-lg-2 col-xl-5 offset-xl-1 dt-infomation"i><""f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": true,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": true,
        "paging": false,
        "order": [],
        "language": {
            "emptyTable": "No findings available.",
        },
        "ajax": {
            "url": "/Inspection/GetInspectionFindingsByInspectionId",
            "type": "POST",
            "data": data,
            "datatype": "json"
        },
        "initComplete": function (settings, json) {
            CreateChildRow();
            txtReadMore('div-read-more');
        },
        buttons: [
            'copy', 'csv',
            {
                extend: 'excel',
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
                title: "Inspection Finding",
                messageTop: function () {
                    return 'Vessel : ' + $('#VesselName').val() + '\n' + $('#InspectionName').val() + ' - ' + $('#OccuredDate').val();
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
                        return GetChatBaseIcons(full.inspectionFindingId, full.channelCount, full.messageDetailsJSON);
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
                        return GetNotesBaseIcons(full.inspectionFindingId, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align tdblock d-block d-md-none d-lg-none d-xl-none",
                orderable: false,
                width: "70px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0 || full.notesCount > 0) {
                        return GetChatNotesBaseIcons(full.inspectionFindingId, full.channelCount, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "top-margin-0 tdblock data-text-align",
                name: "VesselReferenceNo",
                data: "vesselReferenceNo",
                widht: "60px",
                render: function (data, type, full, meta) {
                    return GetCellData('Ves Ref', '<a href="/Inspection/Actions/?inspectionAction=' + full.actionUrl + '&vesselId=' + full.vesselId + '">' + data + '</a>');
                    //return '<a href="javascript:void(0)" onclick="javascript:window.open(\'/Inspection/Actions/?inspectionAction=' + full.actionUrl + '&vesselId=' + full.vesselId + '\')">' + data + '</a>';
                }
            },
            {
                className: "font-bold color-black tdblock data-number-align chapter-no",
                name: "InspectionReferenceNo",
                data: "inspectionReferenceNo",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Chapter / Insp.Ref No', data); }
            },
            {
                className: "font-bold color-black tdblock data-text-align",
                name: "InspectionFindingTypeName",
                data: "inspectionFindingTypeName",
                width: "140px",
                render: function (data, type, full, meta) { return GetCellData('Type or Category', data); }
            },
            //{
            //    className: "description tdblock",
            //    name: "Description",
            //    data: "description",
            //    render: function (data, type, full, meta) { return GetCellData('Description', data); }
            //},
            {
                className: "font-bold color-black tdblock data-text-align",
                name: "RiskAssessmentCategoryName",
                data: "riskAssessmentCategoryName",
                render: function (data, type, full, meta) { return GetCellData('Correction Action Assigned To', data); },
                "visible": (IsPscVisible == "True" ? false : true)
            },
            {
                className: "font-bold color-black tdblock data-text-align",
                name: "RiskAssessmentAreaName",
                data: "riskAssessmentAreaName",
                width: "215px",
                render: function (data, type, full, meta) { return GetCellData('System / Area', data); }
            },
            {
                className: "font-bold color-red data-datetime-align",
                name: "DateDue",
                data: "dateDue",
                width: "90px",
                //render: function (data, type, full, meta) { return GetCellData('Due Date', data); }

                type: "date",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null && data != '') {
                        date = new Date(data);
                        formattedDate = moment(date).format("DD MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Due Date', formattedDate);
                    }
                    return date;
                }
            },
            {
                className: "data-datetime-align",
                name: "DateCleared",
                data: "dateCleared",
                width: "90px",
                //render: function (data, type, full, meta) { return GetCellData('Date Cleared', data); }

                type: "date",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null && data != '') {
                        date = new Date(data);
                        formattedDate = moment(date).format("DD MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Date Cleared', formattedDate);
                    }
                    return date;
                }
            }
        ],
        "fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
            let child = $(nRow).find('td:eq(0)').children();
            if (screen.width < MobileScreenSize && $(child).length == 0) {
                $(nRow).find('td:eq(0)').addClass('d-none');
            }
        },
    });
    $.fn.DataTable.ext.pager.numbers_length = 4;

    $('#dtfindings').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });
}

function CreateChildRow() {
    dtgrid.rows().every(function () {
        // If row has details collapsed
        if (!this.child.isShown()) {
            // Open this row
            this.child(formatChildRow(this.data()), 'tbl-description').show();
            $(this.node()).addClass('shown');
        }
    });
}

function formatChildRow(d) {
    if (($(window).width() < MobileScreenSize)) {
        return '<div class="div-read-more">' + lntobr(d.description) + '</div>';
    }
    else {
        return '<div class="row"> <div class="col-12"><strong>Description</strong><div> ' + lntobr(d.description) + '</div>';
    }
}

function BindSummary() {

    var data = {
        "inspectionUrl": $('#InspectionUrl').val()
    }

    $.ajax({
        "url": "/Inspection/GetInspectionFindingsCountByInspectionId",
        "type": "POST",
        "dataType": "json",
        "data": data,
        "success": function (data) {
            $('#AllFindingLabelCount').text(data.allFindingCount);
            $('#ClearedLabelCount').text(data.clearedCount);
            $('#OutstandingLabelCount').text(data.outstandingCount);
            $('#OverdueLabelCount').text(data.overdueCount);
        }
    });
}

function tosterAlert(type, message) {
    if (type == "success") {

        toastr.success(message);
    }
    else if (type == "error") {
        toastr.options = {
            "closeButton": true,
            "timeOut": "0",
            "extendedTimeOut": "0"
        };
        toastr.error(message);
    }
}

function GetCellData(label, data) {
    return '<label>' + label + '</label><br />' + GetActualCellData(data);
}

function GetActualCellData(data) {
    return '<span class="export-Data">' + data + '</span>';
}

function getCookie(name) {
    var cookieArr = document.cookie.split(";");

    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");

        if (name == cookiePair[0].trim()) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}

function fetchInspectionAndInspectorDetails(inspectionUrl) {
    $.ajax({
        "url": "/Inspection/PostGetInspectionAndInspectorDetails",
        "type": "POST",
        "dataType": "json",
        "data": {
            "inspectionUrl": inspectionUrl
        },
        "success": function (data) {
            $("#location").text(getValueOrBlank(data.location));
            $("#endDate").text(FormatDate(data.endDate));
            $("#where").text(getValueOrBlank(data.where));
            $("#nextDue").text(FormatDate(data.nextDue));
            $("#entity").text(getValueOrBlank(data.entity));
            $("#inspectorName").text(getValueOrBlank(data.inspector));
            $("#rank").text(getValueOrBlank(data.rank));
            $("#company").text(getValueOrBlank(data.company));
            $("#department").text(getValueOrBlank(data.department));
            $("#startDate").text(FormatDate(data.startDate));

            $("#HeadingDetainedDay").text(getDetainedDayValueOrBlank(data.detainedDays));
            $("#HeadingCompany").text(getValueOrBlank(data.company));
            var locationtext = "(" + getValueOrBlank(data.location) + ") " + getValueOrBlank(data.where);
            $("#HeadingLocation").text(locationtext);
        },
        "complete": function () {
            $('.height-equal').matchHeight();
        }
    });
}

function getValueOrBlank(data) {
    if (data == null || data == "") {
        return "-";
    }
    return data;
}


function getDetainedDayValueOrBlank(data) {
    if (data == null || data == "") {
        $('.detained').removeClass('d-md-inline-block');
        $('.detained').hide();
        return "-";
    }
    else {
        if (data > 1) {
            return "Detained " + data + " Days";
        }
        else {
            return "Detained " + data + " Day";
        }
    }
}


function ExportToExcel() {

    $.ajax({
        url: "/Inspection/ExportToExcelSummeryReport",
        type: "POST",
        "data": {
            "inspectionUrl": $('#InspectionUrl').val(), "vesselId": $('#VesselId').val()
        },
        success: function (data) {
            if (data.success) {
                ToastrAlert("success", data.message);
            }
            else {
                ToastrAlert("error", data.message);
            }
        }
    });
}

function CallInspectionListPageFromSession() {
    $.ajax({
        url: "/Inspection/RouteToInspectionUsingSession",
        type: "POST",
        "data": {
            "encryptedVesselId": $('#VesselId').val()
        },
        success: function (data) {
            window.location.href = data;
        }
    });
}

function LoadDownloadReportDefaultDetails() {
    $.ajax({
        url: "/Inspection/GetFindingsReportDetails",
        type: "POST",
        dataType: "JSON",
        success: function (result) {
            var formatList = result.formatList;
            var select = document.getElementById('selFormat');
            for (var i = 0; i < formatList.length; i++) {
                var obj = formatList[i];
                var opt = document.createElement('option');
                opt.value = obj.identifier;
                opt.innerHTML = obj.description;
                if (i == 0) {
                    opt.selected = true;
                }
                select.appendChild(opt);
            }
        }
    });
}

function DownloadReport() {
    var input = {
        "EncryptedVesselId": $('#VesselId').val(),
        "InspectionUrl": $('#InspectionUrl').val(),
        "ReportType": $('input[name="reportType"]:checked').val(),
        "ReportFormat": $('#selFormat').val(),
    }

    $.ajax({
        url: "/Inspection/DownloadInspectionFindingReport",
        type: "POST",
        data: {
            "input": input
        },
        success: function (data) {
            if (data.success) {
                ToastrAlert("success", data.message);
                $('#modalDownloadInspectionReport').modal('hide');
            }
            else {
                ToastrAlert("error", data.message);
            }
        }
    });
}

function lntobr(str) {
    if (str != "" && str != null) {
        str = getValueOrBlank(str)
        return str.replace(/(?:\r\n|\r|\n)/g, '<br>');
    }
    else {
        return "";
    }
}

function IsVesselInManagement() {
    $.ajax({
        "url": "/Inspection/VesselHeaderDetails",
        "type": "GET",
        "data":
        {
            "encryptedVesselId": $('#EncryptedVesselId').val(),
        },
        "datatype": "json",
        "success": function (data) {
            if (data != null) {
                if (data.isVesselInManagement) {
                    InspectionClosureChecks()
                    return true;
                }
                else {
                    $("#modelInspectionClosureActionAlert").modal('show');
                    $('#spanAlertMessage').text("To close inspection report, vessel management should be in full management or technical management and in active state.");
                    return true;
                }
            }
        }
    });
}

function InspectionClosureChecks() {
    var request =
    {
        "EncryptedInspectionId": $('#EncryptedInspectionId').val(),
        "EncryptedVesselId": $('#VesselId').val()
    }
    $.ajax({
        "url": "/Inspection/GetInspectionDetailsByInspectionId",
        "type": "GET",
        "data": request,
        "datatype": "json",
        "success": function (data) {
            if (data != null) {
                //Report tab condition
                if (!IsNullOrEmptyOrUndefinedLooseTyped(data.tplId)) {
                    $("#modelInspectionClosureActionAlert").modal('show');
                    $('#spanAlertMessage').text("This type of inspection can only be closed in Shipsure application.");
                    return true;
                }

                //Finding should be cleared
                if (!data.allFindingsCleared) {
                    $("#modelInspectionClosureActionAlert").modal('show');
                    $('#spanAlertMessage').text("All findings should be cleared before closing inspection.");
                    return true;
                }

                //All mandatory questions are not filled
                if (!data.isAllQuestionAndAnsValid) {
                    $("#modelInspectionClosureActionAlert").modal('show');
                    $('#spanAlertMessage').text("This type of inspection can only be closed in Shipsure application.");
                    return true;
                }

                //Close inspection
                CloseInspection();
                return true;
            }
        }
    });
}

function CloseInspection() {
    var data = {
        'encryptedInspectionId': $('#EncryptedInspectionId').val()
    }

    GlobalAjaxCall('/Inspection/CloseInspection/', 'POST', 'application/x-www-form-urlencoded; charset=UTF-8', 'json', data, null, SuccessCallBackOfCloseInspection, null, errorCallBackFunctionValidation);
}

function errorCallBackFunctionValidation(xhr, status, error) {    
    if (xhr != null) {
        if (IsJsonString(xhr.responseText)) {
            if (xhr.status == 403) {
                //forbidden request for business exception
                $("#modelInspectionClosureActionAlert").modal('show');
                var validationMessage = JSON.parse(xhr.responseText);
                $('#spanAlertMessage').text(validationMessage.message);
                return false;
            }
        }
    }
}

function SuccessCallBackOfCloseInspection(data) {
    if (data != null) {
        if (data.operationResult) {
            $("#inspectionSuccessModal").modal('show');
            $('#btnCloseInspectionSuccessOk').off();
            $('#btnCloseInspectionSuccessOk').on('click', function () {
                $("#inspectionSuccessModal").modal('hide');
                GetInspectionClosureSuccesUrl(InspectionFindingPageKey);
            });
        }
        else {
            ToastrAlert("validate", data.message);
        }
    }
}

function GetInspectionClosureSuccesUrl(keyName) {
    $.ajax({
        url: "/Inspection/GetInspectionClosureSuccesUrl",
        type: "POST",
        dataType: "JSON",
        data: {
            "pageKey": keyName,
            "encryptedVesselId": $('#EncryptedVesselId').val()
        },
        success: function (data) {
            if (data != null) {
                window.location.replace(data);
            }
        }
    });
}

function ActionBasedOnRoleRight() {
    //Role Right
    GetRoleRightsAsync([CloseInspectionControlId], function (rolerights) {
        var inspectionClosureAccess = rolerights.find(x => x.controlId.toLowerCase() === CloseInspectionControlId.toLowerCase());
        if (!inspectionClosureAccess.permission) {
            $(".btnInspectionClosure").remove();
        }
    });
}

function InspectionDetails() {

    $.ajax({
        "url": "/Inspection/GetInspectionDetailsOnFinding",
        "type": "GET",
        data: {
            "encryptedInspectionId": $('#EncryptedInspectionId').val()
        },
        "datatype": "json",
        "success": function (data) {
            if (data != null) {
                if (data.isClosed) {
                    $(".btnInspectionClosure").remove();
                }

                if (data.isDeleted) {
                    $(".btnInspectionClosure").remove();
                }
            }
        }
    });
}