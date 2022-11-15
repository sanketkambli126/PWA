import "bootstrap";
import toastr from "toastr";
import moment from "moment";
import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader } from "../common/datatablefunctions.js"
import { AjaxError, GetCookie, FormatDate, txtReadMore, headerReadMore, SetHeaderMargin, BackButton, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, RemoveClassIfPresent, AddClassIfAbsent, RegisterTabSelectionEvent } from "../common/utilities.js";
import { InspectionActionPageKey, MobileScreenSize } from '../common/constants.js'
import { RecordLevelNote } from "../common/notesUtilities.js";

var dtgrid
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

    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });

    $(document).click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }
    });

    BackButton(InspectionActionPageKey, false);

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);
    RegisterTabSelectionEvent('.mobileTabClick', InspectionActionPageKey);
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }
    GetDiscussionNotesCount(messageDetailsJSON);

    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON, code);

    var inspectionUrl = $('#InspectionUrl').val();
    fetchInspectionAndInspectorDetails(inspectionUrl);

    $('.height-equal').matchHeight();
    fetchInspectionFindingsAndCausation(inspectionUrl);

    LoadGrid();

    $('.btnExport').click(() => {
        var searchValue = dtgrid.search();
        dtgrid.search("").draw();

        $('#dtactions.cardview thead').addClass("export-grid-show");
        $('#dtactions').DataTable().buttons(0, 2).trigger();
        $('#dtactions.cardview thead').removeClass("export-grid-show");

        dtgrid.search(searchValue).draw();
    });

    $(document).ajaxStop(function () {
        if (!documentLoaded) {
            if (screen.width < MobileScreenSize) {
                headerReadMore('headershowmorewrapper', 'header');
                $('.mobile-tab').click();
            }
            SetHeaderMargin();
            documentLoaded = true;
        }
    });

    $('.mobile-tab').click(function () {
        if (($(window).width() < MobileScreenSize)) {
            if ($(".tab-1").hasClass('active')) {
                AddClassIfAbsent('#btnExportShowHideMobile', 'd-none');
            }
            else {
                RemoveClassIfPresent('#btnExportShowHideMobile', 'd-none');
            }
            let mobileParentElement = $("#btnExportShowHideMobile").parents('.dropdown');
            let chidlrens = mobileParentElement.find('li');
            let visibleChilds = $(chidlrens ).not('.d-none');
            if (visibleChilds.length == 0) {
                RemoveClassIfPresent(mobileParentElement, 'd-block');
                AddClassIfAbsent(mobileParentElement, 'd-none');
            } else {
                AddClassIfAbsent(mobileParentElement, 'd-block');
                RemoveClassIfPresent(mobileParentElement, 'd-none');
            }
        }
    });

     

    $(document).on('click', "#findingsHeader", function () {
        CallFindingsPageFromSession();
    });

    $(document).on('click', "#inspectionHeader", function () {
        CallInspectionListPageFromSession();
    });

});

function LoadGrid() {
    var data = {
        "inspectionUrl": $('#InspectionUrl').val()
    }

    $('#dtactions').DataTable().destroy();
    dtgrid = $('#dtactions').DataTable({
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
            "emptyTable": "No actions available.",
        },
        "ajax": {
            "url": "/Inspection/GetFindingActionsByFindingId",
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
                    CustomizedExcelHeader(xlsx, 3);
                },
                title: "Inspection Actions",
                messageTop: function () {
                    return 'Vessel : ' + $('#VesselName').val() + '\n' + $('#InspectionName').val() + ' - ' + $('#OccuredDate').val() + '\nActions for - ' + $('#VesselReferenceNo').val();
                }
            },
            'pdf', 'print'
        ],
        "columns": [
            {
                className: "font-bold color-black top-margin-0 tdblock data-datetime-align",
                name: "ActionDate",
                data: "actionDate",
                width: "75px",
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
                        return GetCellData('Date', formattedDate);
                    }
                    return date;
                }
            },
            {
                className: "font-bold color-black tdblock data-text-align",
                name: "ReportedBy",
                data: "reportedBy",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Reported By', data); }
            },
            {
                className: "font-bold color-black tdblock data-text-align",
                name: "IsClear",
                data: "isClear",
              
                render: function (data, type, full, meta) { return GetCellData('Cleared', data); }
            }
        ]
    });
    $.fn.DataTable.ext.pager.numbers_length = 4;
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
    if (($(window).width() < 767)) {
        $("#findingdescription").addClass('div-read-more');
        return '<div class="div-read-more">' + lntobr(d.actionDescription) + '</div>';
    }
    else {
        return '<div class="row"> <div class="col-12"><strong>Description</strong><div> ' + lntobr(d.actionDescription) + '</div>';
    }
}



function GetCellData(label, data) {
    return '<label>' + label + '</label><br />' + GetActualCellData(data);
}

function GetActualCellData(data) {
    return '<span class="export-Data">' + data + '</span>';
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

function fetchInspectionFindingsAndCausation(data) {
    $.ajax({
        "url": "/Inspection/PostGetInspectionFindingDetails",
        "type": "POST",
        "dataType": "json",
        "data": {
            "inspectionUrl": data
        },
        "success": function (data) {
            $("#vesselRef").text(data.vesRef);
            $("#inspectionRef").text(data.refNo);
            $("#typeCatergory").text(data.type);
            $("#correctionAction").text(data.correctionActionAssignedTo);
            $("#systemArea").text(data.systemArea);
            $("#dueDate").text(FormatDate(data.dueDate));
            $("#dateCleared").text(FormatDate(data.dateCleared));
            $("#findingdescription").html(lntobr(data.description));
            $("#substandardActs").text(getValueOrBlank(data.substandardActs));
            $("#substandardConditions").text(getValueOrBlank(data.substandardConditions));
            $("#humanFactor").text(getValueOrBlank(data.humanFactors));
            $("#jobFactors").text(getValueOrBlank(data.jobFactors));
            $("#controlManagementFailure").text(getValueOrBlank(data.controlManagementFailure));
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

function fetchInspectionAndInspectorDetails(inspectionUrl) {
    $.ajax({
        "url": "/Inspection/PostGetInspectionAndInspectorDetails",
        "type": "POST",
        "dataType": "json",
        "data": {
            "inspectionUrl": inspectionUrl
        },
        "success": function (data) {
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

function CallFindingsPageFromSession() {
    $.ajax({
        url: "/Base/GetSourceURL",
        type: "POST",
        dataType: "JSON",
        data: {
            "pageKey": InspectionActionPageKey
        },
        success: function (data) {
            if (data != null) {
                window.location.replace(data);
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

function lntobr(str) {
    if (str != "" && str != null) {
        str = getValueOrBlank(str)
        return str.replace(/(?:\r\n|\r|\n)/g, '<br />');
    }
    else {
        return "";
    }
}