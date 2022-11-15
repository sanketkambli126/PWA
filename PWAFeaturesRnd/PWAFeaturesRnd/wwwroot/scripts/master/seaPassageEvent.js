import moment from "moment";
require('bootstrap');

import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, headerReadMore, ConvertDecimalNumberToString, ConvertValueToPercentage, GetCookie, ToastrAlert, GetDashboardColorMap, GetStringNullOrWhiteSpace, SetHeaderMargin, IsNullOrEmpty, BackButton, SetValueElseDefault, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, RegisterTabSelectionEvent, RemoveClassIfPresent } from "../common/utilities.js";
import { GetCellData, GetFormattedDecimal, GetFormattedDateTime, GetFormattedTimeFromDate } from "../common/datatablefunctions.js";
import { OpenModalAgentDetails, OpenModalPortServices, LoadBadWeather, LoadOffHireDetails, LoadPortBadWeather, LoadPortDelay } from "../master/voyageReportingModal.js";
import { SeaPassageEventPageKey, MobileScreenSize } from "../common/constants.js"
import { RecordLevelNote } from "../common/notesUtilities.js"

var encryptedVesselDetail, vesselName, listURL;
const columnTimeSailed = 6, columnDist = 8, columnFO = 17, columnLSFO = 18, columnDO = 19, columnGO = 20, columnLNG = 21, columnFwDom = 22,
    columnFwTech = 23, columnCLO = 24, columnCRANK = 25, columnAUX = 26, columnGeneral = 27;

var colorMap = GetDashboardColorMap();

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



$(document).on('click', '.anchorPortAlertCls', function () {
    var requestUrl = $(this).data('url');
    OpenModalPortServices(requestUrl);
});
$(document).on('click', '.vesselActivityAgentDetails', function () {
    var data = $(this).data('url');
    OpenModalAgentDetails(data, $('#EncryptedVesselDetail').val());
});

$(document).on('click', '.vesselActivityBadWeather', function () {
    var urlRequest = $(this).attr('id');
    LoadBadWeather(urlRequest);
});

$(document).on('click', '.vesselActivityoffhire', function () {
    var urlRequest = $(this).attr('id');
    LoadOffHireDetails(urlRequest);
});

$(document).on('click', '.vesselActivityportBadWeather', function () {
    var urlRequest = $(this).attr('id');
    LoadPortBadWeather(urlRequest);
});

$(document).on('click', '.vesselActivityportDelay', function () {
    var urlRequest = $(this).attr('id');
    LoadPortDelay(urlRequest);
});

$(window).on('resize', SetHeaderMargin);

$(document).on('click', 'a.showReportPopup', function () {

    var objSeaPassage = JSON.parse(decodeURIComponent($(this).data("full")));
    var activitytype = objSeaPassage.positionListActivityType;

    if (activitytype == "NN") {
        GetSeaPassageNoonReport(objSeaPassage);
    }
    else if (activitytype == "FAP") {
        var spaId = objSeaPassage.seaPostionActivityId;
        GetSeaPassageFAOPDetailsReport(spaId);
    }
    else if (activitytype == "CD") {
        var spaId = objSeaPassage.seaPostionActivityId;
        GetChangeInDestinationReport(spaId);
    }
    else if (activitytype == "ESP") {
        GetSeaPassageNoonReport(objSeaPassage);
    }
});

$(document).ready(function () {


    SectionMatchHights();

    AddLoadingIndicator();
    RemoveLoadingIndicator();
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

    BackButton(SeaPassageEventPageKey, false);
    encryptedVesselDetail = $('#EncryptedVesselDetail').val();
    listURL = $('#ListURL').val();

    LoadPortCallDetails();
    LoadVoyageActivityDetail();
    LoadSeaPassageLastReported();
    SeaPassageEventDetails();
    RegisterTabSelectionEvent('.mobileTabClick', SeaPassageEventPageKey);
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }

    //graph call
    var posId = $('#PositionListId').val();

    BindSeaPassage(posId, encryptedVesselDetail);

    BindPerformanceSummary(encryptedVesselDetail, posId);

    $("#expandseapassagemap").on('click', function () {
        ShowFullMap();
    });

    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);
    GetDiscussionNotesCount(messageDetailsJSON);
    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);

    AjaxError();

    
});

//header details
function LoadPortCallDetails() {
    $.ajax({
        url: "/VoyageReporting/GetPortCallDetail",
        type: "POST",
        "data": {
            "input": listURL
        },
        "datatype": "JSON",
        success: function (result) {
            if (result != null) {
                var data = result.data;
                if (!IsNullOrEmpty(data.charterName)) {
                    $('#spanChartererName').text(data.charterName);
                }
                if (!IsNullOrEmpty(data.charterNumber)) {
                    $('#spanChartererNumber').text(data.charterNumber);
                }
                if (!IsNullOrEmpty(data.voyageNumber)) {
                    $('#spanVoyageNumber').text(data.voyageNumber);
                }
            }
        }
    });
}

function LoadVoyageActivityDetail() {
    $.ajax({
        url: "/VoyageReporting/GetVoyageActivityDetail",
        type: "POST",
        "data": {
            "input": listURL
        },
        "datatype": "JSON",
        success: function (result) {
            var data = result.data;
            $('#spanActivityName').text(data.activityName);
        }
    });
}

function LoadSeaPassageLastReported() {
    $.ajax({
        url: "/VoyageReporting/SeaPassageHeader",
        type: "POST",
        "data": {
            "input": listURL
        },
        "datatype": "JSON",
        success: function (data) {
            if (data != null) {
                if (!IsNullOrEmpty(data.lastUpdatedEventDate)) {
                    $('#spanLastReportedEvent').text(data.lastUpdatedEventDate);
                }
            }
        }
    });
}

function SeaPassageEventDetails() {
    $.ajax({
        url: "/VoyageReporting/SeaPassageEventDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "input": listURL
        },
        success: function (data) {
            if (data != null) {
                LoadSeaPassageEventList(data);
            }
        }
    });
}

function LoadSeaPassageEventList(model) {
    $('#dtEventsList').DataTable().destroy();
    var dtEventsList = $('#dtEventsList').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-11 offset-md-1 col-lg-10 offset-lg-1 col-xl-10 offset-xl-12 dt-infomation"i><""f>><"table-horizontal-scroll"rt><""<""l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": true,
        "paging": false,
        "order": [],
        "pageLength": 10,
        "language": {
            "emptyTable": "No event details.",
        },
        "data": model.activityDetails,
        "columns": [
            {
                className: "tdblock mobile-popover-attachments d-none data-icon-align",
                data: "attachments",
                width: "10px",
                "orderable": false,
                render: function (data, type, full, meta) { return '<img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>'; }
            },
            {
                className: "data-text-align d-none d-md-table-cell total position-relative",
                data: "date",
                width: "180px",
                render: function (data, type, full, meta) {
                    var Element = GetFormattedDateTime(type, '', data);

                    if (full.isNoonIncomplete) {
                        Element += " <i class='fa fa-exclamation-circle ml-2 BrushRed' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Incomplete Noon.'></i>";
                    }
                    if (full.isNoonSynopsisAdded) {
                        Element += " <i class='fa fa-paper-plane' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Navigation Synopsis Recorded.'></i>";
                    }
                    if (full.isIDL) {

                        Element += "<i class='fa fa-exclamation-circle ml-2 BrushOrange d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='IDL Marked.'></i>";

                        Element += "<i class='fa fa-caret-right orange-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='IDL Marked.'></i>";
                    }
                    return GetCellData('Date/Time', Element);
                }
            },
            {
                className: "data-text-align hide-footer-column tdblock",
                data: "positionListActivityType",
                width: "115px",
                render: function (data, type, full, meta) {
                    var fullstr = encodeURIComponent(JSON.stringify(full));

                    var htmlElement = '<a class="showReportPopup" href="javascript:void(0)" data-full="' + fullstr +'">' + data + '</a>';
                    return GetCellData('Event', htmlElement);
                }
            },
            {
                className: "top-margin-0 width-85 data-text-align d-md-none d-lg-none d-xl-none",
                data: "date",
                width: "180px",
                render: function (data, type, full, meta) {
                    var Element = GetFormattedDateTime('', '', data);

                    if (full.isNoonIncomplete) {
                        Element += " <i class='fa fa-exclamation-circle ml-2 BrushRed' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Incomplete Noon.'></i>";
                    }
                    if (full.isNoonSynopsisAdded) {
                        Element += " <i class='fa fa-paper-plane' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Navigation Synopsis Recorded.'></i>";
                    }
                    if (full.isIDL) {

                        Element += "<i class='fa fa-exclamation-circle ml-2 BrushOrange d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='IDL Marked.'></i>";

                        Element += "<i class='fa fa-caret-right orange-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='IDL Marked.'></i>";
                    }
                    return GetCellData('Date/Time', Element);
                }
            },
            {
                className: "data-text-align hide-footer-column",
                data: "activity",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetCellData('Position', data);
                }
            },
            {
                className: "data-number-align hide-footer-column",
                data: "course",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Course', data);
                }
            },
            {
                className: "data-number-align",
                "data": "timeSailed",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetCellData('Time Sailed', data);
                }
            },
            {
                className: "data-number-align hide-footer-column",
                "data": "breakHours",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Breaks Time', data);
                }
            },
            {
                className: "data-number-align",
                "data": "distance",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Dist', data);
                }
            },
            {
                className: "data-number-align hide-footer-column pr",
                "data": "speed",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = GetFormattedDecimal('', '', data, 2, '0.00');
                    if (full.isAverageSpeedError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Avg. speed less than charter avg. speed.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Avg. speed less than charter avg. speed.'></i>";
                    }
                    return GetCellData('Avg Speed', uiElement);
                }
            },
            {
                className: "data-number-align hide-footer-column",
                "data": "rpm",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'RPM', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align hide-footer-column",
                "data": "trueSlip",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'True Slip', data, 2, '0.00');
                }
            },
            {
                className: "data-icon-align hide-footer-column",
                "data": "specifics",
                width: "80px",
                render: function (data, type, full, meta) {
                    let medical = '<img src="/images/DashboardIcons/first-aid-voyage.png" class="mr-1"/>';
                    let delay = '<img src="/images/DashboardIcons/clock-orange.png" class="mr-1" />';
                    let offhire = '<img src="/images/DashboardIcons/vessel-orange.png" class="mr-1" />';
                    let result = '<span class="specificsClicked d-inline-block cursor-pointer" data-spa="' + full.seaPostionActivityId + '" data-date="' + full.date + '" data-course="' + full.course + '" data-time="' + full.timeSailed + '" data-position="' + full.activity + '" data-toggle="modal" data-target="#specifics">'
                    let closetag = '</span>';

                    if (full.isDelayBreakInPassage == true) {
                        result += delay;
                    }
                    if (full.isMedicalBreakInPassage == true) {
                        result += medical;
                    }
                    if (full.isOffHireBreakInPassage == true) {
                        result += offhire;
                    }
                    result += closetag;

                    return GetCellData('Specifics', result);
                }
            },
            {
                className: "data-text-align hide-footer-column",
                "data": "windForce",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = data;
                    if (full.isWindForceError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Wind force greater than charter wind force.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Wind force greater than charter wind force.'></i>";
                    }
                    return GetCellData('Wind Force', uiElement);
                }
            },
            {
                className: "data-text-align hide-footer-column",
                "data": "seaState",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Sea State', data);
                }
            },
            {
                className: "data-number-align hide-footer-column",
                "data": "windSpeed",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Wind Speed', data, 2, '');
                }
            },
            {
                className: "data-text-align hide-footer-column",
                "data": "windDirection",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Wind Dir', data);
                }
            },

            {
                className: "data-number-align pr",
                "data": "fo",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = GetFormattedDecimal('', '', data, 2, '0.00');
                    if (full.isFoError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='FO greater than charter FO.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='FO greater than charter FO.'></i>";
                    }
                    return GetCellData('FO', uiElement);
                }
            },
            {
                className: "data-number-align pr",
                "data": "lsfo",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = GetFormattedDecimal('', '', data, 2, '0.00');
                    if (full.isLsfoError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='LSFO greater than charter LSFO.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='LSFO greater than charter LSFO.'></i>";
                    }
                    return GetCellData('LSFO', uiElement);
                }
            },
            {
                className: "data-number-align pr",
                "data": "do",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = GetFormattedDecimal('', '', data, 2, '0.00');
                    if (full.isDoError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='DO greater than charter DO.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='DO greater than charter DO.'></i>";
                    }
                    return GetCellData('DO', uiElement);
                }
            },
            {
                className: "data-number-align pr",
                "data": "go",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = GetFormattedDecimal('', '', data, 2, '0.00');
                    if (full.isGoError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='GO greater than charter GO.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='GO greater than charter GO.'></i>";
                    }
                    return GetCellData('GO', uiElement);
                }
            },
            {
                className: "data-number-align pr",
                "data": "lng",
                width: "80px",
                render: function (data, type, full, meta) {
                    var uiElement = GetFormattedDecimal('', '', data, 2, '0.00');
                    if (full.isLngError) {
                        uiElement += "<i class='fa fa-exclamation-circle ml-2 BrushRed d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='LNG greater than charter LNG.'></i>";

                        uiElement += "<i class='fa fa-caret-right red-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='LNG greater than charter LNG.'></i>";
                    }
                    return GetCellData('LNG', uiElement);
                }
            },
            {
                className: "data-number-align pr",
                "data": "freshWaterConsumptionDomestic",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'FW DOM.', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align pr",
                "data": "freshWaterConsumptionTechnial",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'FW TECH.', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align pr",
                "data": "cloLubOilConsumption",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'CLO', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "crankLubOilConsumption",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'CRANK', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align pr",
                "data": "auxLubOilConsumption",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'AUX', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align pr",
                "data": "generalLubOilConsumption",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'General', data, 2, '0.00');
                }
            },
        ],
        "initComplete": function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip();
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // Update footer by showing the total with the reference of the column index 

            $(api.column(1).footer()).html('Total');
            $(api.column(columnTimeSailed).footer()).html(GetCellData('Time Sailed', model.totalTime));
            $(api.column(columnDist).footer()).html(GetFormattedDecimal('display', 'Dist', model.totalDistance, 2, '0.00'));
            $(api.column(columnFO).footer()).html(GetFormattedDecimal('display', 'FO', model.totalFo, 2, '0.00'));
            $(api.column(columnLSFO).footer()).html(GetFormattedDecimal('display', 'LSFO', model.totalLsfo, 2, '0.00'));
            $(api.column(columnDO).footer()).html(GetFormattedDecimal('display', 'DO', model.totalDo, 2, '0.00'));
            $(api.column(columnGO).footer()).html(GetFormattedDecimal('display', 'GO', model.totalGo, 2, '0.00'));
            $(api.column(columnLNG).footer()).html(GetFormattedDecimal('display', 'LNG', model.totalLNG, 2, '0.00'));
            $(api.column(columnFwDom).footer()).html(GetFormattedDecimal('display', 'FW DOM.', model.totalFreshWaterConsumptionDomestic, 2, '0.00'));
            $(api.column(columnFwTech).footer()).html(GetFormattedDecimal('display', 'FW TECH.', model.totalFreshWaterConsumptionTechnial, 2, '0.00'));
            $(api.column(columnCLO).footer()).html(GetFormattedDecimal('display', 'CLO', model.totalLubeOilConsumptionClo, 2, '0.00'));
            $(api.column(columnCRANK).footer()).html(GetFormattedDecimal('display', 'CRANK', model.totalLubeOilConsumptionCrankCase, 2, '0.00'));
            $(api.column(columnAUX).footer()).html(GetFormattedDecimal('display', 'AUX', model.totalLubeOilConsumptionAux, 2, '0.00'));
            $(api.column(columnGeneral).footer()).html(GetFormattedDecimal('display', 'General', model.totalGeneralLubOilConsumption, 2, '0.00'));
        },

    });

    //table scroll
    if (($(window).width() > 767)) {
        var newWidth = ($(".table-scroll-width").width());
        $(".table-common-design .table-horizontal-scroll").css({
            "maxWidth": newWidth
        });
    }

    $('.table-footer-design tfoot tr td.total').each(function () {
        if ($('td:contains("Total")')) {
            $('.table-footer-design tfoot tr td.total').addClass('total-common-block');
        }
    });

    $(".specificsClicked").click(function () {
        let date = $(this).data('date');
        let course = $(this).data('course');
        let time = $(this).data('time');
        let position = $(this).data('position');
        SetValueElseDefault("#specificsCourse", course);
        SetValueElseDefault("#specificsDate", GetFormattedDateTime('', '', date));
        SetValueElseDefault("#specficsTime", time);
        SetValueElseDefault("#specificsPosition", position);

        $('#dtspecifics').DataTable().destroy();
        var specifics = $('#dtspecifics').DataTable({
            "processing": true,
            "order": [[0, "desc"]],
            "serverSide": false,
            "lengthChange": true,
            "searching": false,
            "info": false,
            "autoWidth": false,
            "paging": false,
            "ajax": {
                "url": "/VoyageReporting/GetSeaPassageBreaks",
                "type": "POST",
                "data":
                {
                    "posId": $("#PositionListId").val(),
                    "spaId": $(this).data('spa')
                },
                "datatype": "json"
            },
            "columns": [
                {
                    className: "data-text-align tdblock",
                    data: "type",
                    width: "125px",
                    render: function (data, type, full, meta) {
                        let medical = '<img src="/images/DashboardIcons/first-aid-voyage.png" class="mr-1"/>';
                        let delay = '<img src="/images/DashboardIcons/clock-orange.png" class="mr-1" />';
                        let offhire = '<img src="/images/DashboardIcons/vessel-orange.png" class="mr-1" />';
                        let result = '';
                        if (full.isDelay == true) {
                            result += delay;
                        }
                        if (full.isMedical == true) {
                            result += medical;
                        }
                        if (full.offHire == true) {
                            result += offhire;
                        }
                        result += data;
                        return result;
                    }
                },
                {
                    className: "data-text-align",
                    data: "from",
                    width: "90px",
                    render: function (data, type, full, meta) { return GetFormattedDateTime(type, 'From', data); }
                },
                {
                    className: "data-text-align",
                    data: "to",
                    width: "94px",
                    render: function (data, type, full, meta) { return GetFormattedDateTime(type, 'From', data); }
                },
                {
                    className: "data-text-align",
                    data: "reason",
                    width: "50px",
                    render: function (data, type, full, meta) { return GetCellData('Reason', data); }
                },
                {
                    className: "data-text-align",
                    data: "offHire",
                    width: "44px",
                    render: function (data, type, full, meta) {
                        let result = data == true ? "Yes" : "No"
                        return GetCellData('Off Hire', result);
                    }
                },
                {
                    className: "data-text-align",
                    data: "offHireType",
                    width: "73px",
                    render: function (data, type, full, meta) { return GetCellData('Off Hire Type', data); }
                },
                {
                    className: "tdblock data-text-align",
                    data: "comments",
                    width: "245px",
                    render: function (data, type, full, meta) { return GetCellData('Comments', data); }
                }
            ]
        });

    });

}

function BindSeaPassage(posId, vesselId) {
    let parent = $('#divSeapassageParent');
    $.ajax({
        url: "/VoyageReporting/GetSeaPassageGraphDetails",
        type: "POST",
        data: {
            "posId": posId,
            "vesselId": vesselId
        },
        datatype: "JSON",
        success: function (data) {
            if (data != null) {
                if (data.fromPortName != null) {
                    $(parent).find('.voyage-divFromSection').show();
                    $(parent).find('.spanFoap').text(data.fromEventType);
                    $('#spanFromDestination').text(data.fromPortName);
                    let fullFromPortName = GetStringNullOrWhiteSpace(data.fromPortName) + ', ' + GetStringNullOrWhiteSpace(data.fromCountryCode);
                    $(parent).find('.spanFromPortName').text(fullFromPortName);
                    if (data.fromDate != null) {
                        $(parent).find('.spanSeaPassageFromDate').text(moment(new Date(data.fromDate)).format("D MMM YYYY, HH:mm"));
                    }
                    $(".spanFromPortName").data("url", data.fromPortURL);
                    //$('.spanFromPortName').css('cursor', 'pointer');

                    if (data.hasFromPortAlert == true) {
                        $('#divFromPortAlert').addClass('d-inline');
                        $(".fromportalert").data("url", data.fromPortURL);
                    }
                    else {
                        $('#divFromPortAlert').addClass('d-none')
                    }
                } else {
                    $(parent).find('.voyage-divFromSection').hide();
                }

                if (data.toPortName != null) {
                    $(parent).find('.voyage-divToSection').show();
                    $(parent).find('.spanToEosp').text(data.toEventType);
                    $('#spanToDestination').text(data.toPortName);
                    let fullToPortName = GetStringNullOrWhiteSpace(data.toPortName) + ', ' + GetStringNullOrWhiteSpace(data.toCountryCode);
                    $(parent).find('.spanToPortName').text(fullToPortName);
                    if (data.toDate != null) {
                        $(parent).find('.spanSeaPassageToDate').text(moment(new Date(data.toDate)).format("D MMM YYYY, HH:mm"));
                    }

                    $(".spanToPortName").data("url", data.toPortURL);
                    //$('.spanToPortName').css('cursor', 'pointer');

                    if (data.hasToPortAlert == true) {
                        $('#divToPortAlert').addClass('d-inline');
                        $(".toportalert").data("url", data.toPortURL);
                    }
                    else {
                        $('#divToPortAlert').addClass('d-none')
                    }
                }
                else {
                    $(parent).find('.voyage-divToSection').hide();
                }

                LoadProgressBar(data, parent);
            }
        },
        complete: function () {
            SetHeaderMargin();
            if (screen.width > MobileScreenSize) {
                $('.height-equal').matchHeight({
                    byRow: 0
                });
            }
            if (screen.width < MobileScreenSize) {
                $('.equalheightport').matchHeight();
            }
        }
    });
}

//under progress bar
function LoadProgressBar(data, parent) {

    var remainDistance = data.totalDistance - data.sailedDistance;
    $(parent).find('.spanSPTotalDistance').text(ConvertDecimalNumberToString(remainDistance, 2, 1, 2) + " nm");
    $(parent).find('.spanSPDistanceCovered').text(ConvertDecimalNumberToString(data.sailedDistance, 2, 1, 2) + " nm");

    if (data.isSeaPassageEvent == true) {
        var endPositionOfVessel = ConvertValueToPercentage(data.sailedDistance, data.totalDistance);
        var progressBarEndContent = "AT SEA" +
            "<br/>" + data.lastEventPosition + "<br/>" +
            (data.totalDistance - data.sailedDistance) + " nm remaining";
        $(parent).find('.divProgressBarFlow').css('width', endPositionOfVessel + '%');
        $(parent).find('.at-location').css('left', endPositionOfVessel + '%');

        $('.at-location').attr("data-original-title", progressBarEndContent);
    }
    if (data != null && data.badWeatherDetails != null && data.badWeatherDetails.length > 0) {
        var length = data.badWeatherDetails.length;
        var i = 0;
        for (i; i < length; i++) {

            //this is for bad weather
            //IsOnlyBadWeatherAlert || IsBreakAndBadWeatherAlert - BadWeatherDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyBadWeatherAlert == true || data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
                var badWeatherPosition = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

                if (data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
                    $(parent).find(".divProgressBar").append('<a class="vesselActivityBadWeather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
                        '<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                        'style="left:' + badWeatherPosition + '%;top: -23px;" data-html="true"></div></a>');
                }
                else {
                    $(parent).find(".divProgressBar").append('<a class="vesselActivityBadWeather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
                        '<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                        'style="left:' + badWeatherPosition + '%;" data-html="true"></div></a>');
                }
            }

            //this is for off hire
            //IsOnlyBreakAlert || IsBreakAndBadWeatherAlert uses - BreakDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyBreakAlert == true || data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
                var offHirePosition = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);
                $(parent).find(".divProgressBar").append('<a class="vesselActivityoffhire" href="javascript:void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
                    '<div class="graph-position-default offhire-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                    'style="left:' + offHirePosition + '%;" data-html="true"></div></a>');
            }

            //IsOnlyPortBadWeatherAlert - uses PortBadWeathersDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyPortBadWeatherAlert == true) {
                var PortBadWeathersDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);
                $(parent).find(".divProgressBar").append('<a class="vesselActivityportBadWeather" href="javascript:void(0);" id=' + data.requestURL + '>' +
                    '<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                    'style="left:' + PortBadWeathersDetail + '%;" data-html="true"></div></a>');
            }

            //IsOnlyDelayAlert - use DelaysDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyDelayAlert == true) {
                var DelaysDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

                $(parent).find(".divProgressBar").append('<a class="vesselActivityportDelay" href="javascript:void(0);" id=' + data.requestURL + '>' +
                    '<div class="graph-position-default delayed-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                    'style="left:' + DelaysDetail + '%;" data-html="true"></div></a>');
            }
        }
    }
}

function BindPerformanceSummary(vesselId, posId) {
    let parent = $('#divSeapassageParent');

    var request =
    {
        "VesselId": vesselId,
        "PosId": posId
    }

    $.ajax({
        url: "/Dashboard/GetPerformanceSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        success: function (result) {

            if (result != null && result.length > 0) {
                var data = result[0];

                $(parent).find(".last24HourSpeed-text").text(data.last24HourSpeed);
                $(parent).find(".last24HourSpeed").addClass(colorMap.get(data.last24HourSpeedPriority).textColor);
                $(parent).find(".last24HourSpeed").addClass(colorMap.get(data.last24HourSpeedBackgroundPriority).color);

                $(parent).find(".last24HourConsumption-text").text(data.last24HourConsumption);
                $(parent).find(".last24HourConsumption").addClass(colorMap.get(data.last24HourConsumptionPriority).textColor);
                $(parent).find(".last24HourConsumption").addClass(colorMap.get(data.last24HourConsumptionBackgroundPriority).color);

                $(parent).find(".voyageAverageSpeed-text").text(data.voyageAverageSpeed);
                $(parent).find(".voyageAverageSpeed").addClass(colorMap.get(data.voyageAverageSpeedPriority).textColor);
                $(parent).find(".voyageAverageSpeed").addClass(colorMap.get(data.voyageAverageSpeedBackgroundPriority).color);

                $(parent).find(".voyageAverageConsumption-text").text(data.voyageAverageConsumption);
                $(parent).find(".voyageAverageConsumption").addClass(colorMap.get(data.voyageAverageConsumptionPriority).textColor);
                $(parent).find(".voyageAverageConsumption").addClass(colorMap.get(data.voyageAverageConsumptionBackgroundPriority).color);

                $(parent).find(".cpAdjustedSpeed-text").text(data.cpAdjustedSpeed);
                $(parent).find(".cpAdjustedSpeed").addClass(colorMap.get(data.cpAdjustedSpeedPriority).textColor);
                $(parent).find(".cpAdjustedSpeed").addClass(colorMap.get(data.cpAdjustedSpeedBackgroundPriority).color);

                $(parent).find(".cpAdjustedConsumption-text").text(data.cpAdjustedConsumption);
                $(parent).find(".cpAdjustedConsumption").addClass(colorMap.get(data.cpAdjustedConsumptionPriority).textColor);
                $(parent).find(".cpAdjustedConsumption").addClass(colorMap.get(data.cpAdjustedConsumptionBackgroundPriority).color);

                $(parent).find(".cpOrdersSpeed-text").text(data.cpOrdersSpeed);

                $(parent).find(".cpOrdersConsumption-text").text(data.cpOrdersConsumption);
            }
        }

    });
}

function ShowFullMap() {
    $.ajax({
        url: "/VoyageReporting/GetDashboardFullMapUrl",
        dataType: "JSON",
        type: "POST",
        data: {
            "vesselId": encryptedVesselDetail
        },
        success: function (data) {
            var fullMapUrl = "/Dashboard/DashboardMapFullScreen/?mapurl=" + data;
            window.location.href = fullMapUrl;
        }
    })
}

function GetSeaPassageNoonReport(objSeaPassage) {

    var spaId = objSeaPassage.seaPostionActivityId;
    var isIdl = objSeaPassage.isIDL;
    var isNoonIncomplete = objSeaPassage.isNoonIncomplete;

    $.ajax({
        url: "/VoyageReporting/GetSeaPassageNoonReport",
        type: "POST",
        data: {
            "PosId": $("#PositionListId").val(),
            "SpaId": spaId,
            "VesselId": encryptedVesselDetail,
            "IsIdl": isIdl,
            "IsNoonIncomplete": isNoonIncomplete
        },
        "datatype": "html",
        success: function (result) {
            $('#noonreport').modal('show');
            $('#divNoonReport').empty();
            $('#divNoonReport').html(result);

            BindNooreportTable();
            SectionMatchHights();

            if (objSeaPassage.positionListActivityType == "ESP") {
                $('#divHeaderNoon').html("EOSP REPORT");
            }
            else {
                $('#divHeaderNoon').html("NOON REPORT");
            }

            $('.mobile-dropdown-tab').removeClass('d-none');
            if (screen.width < 760) {
                headerReadMore('noonheadershowmorewrapper', 'header');
                $('.noonreport').on('shown.bs.modal', function (e) {
                    var windowh = $('.modal-events').height();
                    var modalheader = $('.modal-events.noonreport .modal-header').outerHeight();
                    $(".modal-events.noonreport .scroller").css({
                        "max-height": windowh - modalheader - 80
                    });
                });
            }
        }
        
    });
}

function GetSeaPassageFAOPDetailsReport(spaId) {
    $.ajax({
        url: "/VoyageReporting/GetSeaPassageFAOPDetailsReport",
        type: "POST",
        data: {
            "posId": $("#PositionListId").val(),
            "spaId": spaId,
            "vesselId": encryptedVesselDetail
        },
        "datatype": "html",
        success: function (result) {
            $('#divFAOPReport').empty();
            $('#divFAOPReport').html(result);
            $('#faop').modal('show');

            BindFAOPreportTable();
            //SectionMatchHights();
            if (screen.width < 760) {
                headerReadMore('faopheadershowmorewrapper', 'header');
                $('.faop').on('shown.bs.modal', function (e) {
                    var windowh = $('.modal-events').height();
                    var modalheader = $('.modal-events.faop .modal-header').outerHeight();
                    $(".modal-events.faop .scroller").css({
                        "max-height": windowh - modalheader - 40
                    });
                });
            }
            
        }
        
    });
}

function BindNooreportTable() {
    var noonsynopsisJson = $('#divNavigationArr').data('noonsynopsis');
    var seapassagebreaks = $('#divBreakInPassegeArr').data('seapassagebreaks');
    var runnighrspower = $('#divRunnigpowerArr').data('runnigpower');
    var nooncomments = $('#divEngineArr').data('nooncomments');
    var noonreportweather = $('#divDeckLogWeatherArr').data('noonreportweather');

    if ($.fn.DataTable.isDataTable('#dtnavigation')) {
        $('#dtnavigation').DataTable().destroy();
    }
    var navigarion = $('#dtnavigation').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><"table-horizontal-scroll table-horizontal-scroll-modal"rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "scrollX": true,
        "scrollCollapse": true,
        "order": [],
        "orderable": false,
        "data": noonsynopsisJson,
        "columns": [
            {
                className: "data-datetime-align",
                "data": "FromDate",
                "width": "125px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDateTime(type, 'From Date', data); }
            },
            {
                className: "data-datetime-align",
                "data": "ToDate",
                "width": "125px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDateTime(type, 'To Date', data); }

            },
            {
                className: "data-number-align",
                "data": "TimeDifference",
                "width": "75px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Tme (hrs)', data, 2, ""); }
            },
            {
                className: "data-text-align",
                "data": "SelectedSynopsisType",
                "width": "140px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Reason', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "AvgSpeed",
                "width": "126px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'AVG Speed (knots)', data, 2, ""); }
            },
            {
                className: "data-text-align",
                "data": "PlkIdSignificantCurrent",
                "width": "124px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Significant current', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Effect",
                "width": "100px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Effect', data, 2, ""); }
            },
            {
                className: "tdblock data-text-align",
                "data": "Comment",
                "width": "266px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Comments', IsNullOrEmpty(data) ? "" : data); }
            },
        ],
        "initComplete": function (settings, json) {
            $('#spanNavigationCount').text(this.api().data().length);
            if (($(window).width() > 767)) {
                $('.height-equal-boxes-two').matchHeight({
                    byRow: 0
                });
            }
            $('#noonreport').on('shown.bs.modal', function () {
                if (($(window).width() > 767)) {
                    var newWidth = ($(".table-scroll-width-modal").width());
                    $(".table-common-design .table-horizontal-scroll-modal").css({
                        "maxWidth": newWidth
                    });
                    var newWidth2 = ($(".table-scroll-width-modal").width());
                    $(".table-scroll-width-modal table").css({
                        "width": newWidth2
                    });
                }
            });
        },
    });

    if ($.fn.DataTable.isDataTable('#dtbreakpassage')) {
        $('#dtbreakpassage').DataTable().destroy();
    }
    var breakinp = $('#dtbreakpassage').DataTable({
        "dom": '<<"row"<"col-12 col-md-11 offset-md-1 col-lg-10 offset-lg-1 col-xl-10 offset-xl-12 dt-infomation"i><""f>><"table-horizontal-scroll table-horizontal-scroll2"rt><""<""l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "orderable": false,
        "data": seapassagebreaks,
        "columns": [
            {
                className: "data-text-align",
                "data": "Type",
                "width": "100px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Type', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "tdblock data-datetime-align",
                "data": "From",
                "width": "80px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDateTime(type, "From", data) }

            },
            {
                className: "tdblock data-datetime-align",
                "data": "To",
                "width": "80px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDateTime(type, "To", data) }
            },
            {
                className: "data-datetime-align",
                "data": "BreakTimeD",
                "width": "40px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Time (HRS)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-text-align",
                "data": "Reason",
                "width": "120px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Reason', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Fo",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('HFO (mt)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Lsfo",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('LFO (mt)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Do",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('DO (mt)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Go",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('GO (mt)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Lng",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('LNG Bunker (mt)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "SpbLngcargo",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Cargo Boil Off(mt)', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "OutDistance",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Drifted', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-text-align",
                "data": "IsOutOfService",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    var offHire = data == 1 ? "Yes" : "No";
                    return GetCellData('Off Hire', offHire);
                }
            },
            {
                className: "tdblock data-text-align",
                "data": "OffHireType",
                "width": "150px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Off Hire Type', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "tdblock data-text-align",
                "data": "Comments",
                "width": "150px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Comments', IsNullOrEmpty(data) ? "" : data); }
            },
        ],
        "initComplete": function () {
            $('#spanBreakpassageCount').text(this.api().data().length);
        }
    });

    $('#noonreport').on('shown.bs.modal', function () {
        if (($(window).width() > 767)) {
            var newWidth = ($(".table-scroll-width2").width());
            $(".table-common-design .table-horizontal-scroll2").css({
                "maxWidth": newWidth
            });
        }
    });

    if ($.fn.DataTable.isDataTable('#dtrunnigpower')) {
        $('#dtrunnigpower').DataTable().destroy();
    }
    var runninghours2 = $('#dtrunnigpower').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "order": [],
        "orderable": false,
        "paging": false,
        "data": runnighrspower,
        "columns": [
            {
                className: "data-text-align",
                "data": "PartName",
                "width": "190px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Component', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "Previous",
                "width": "80px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Past Hrs', data, 2, '0.00');
                }

            },
            {
                className: "data-number-align",
                "data": "Daily",
                "width": "80px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Current Hrs', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "Total",
                "width": "80px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Total Hrs', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "PowerOutput",
                "width": "85px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Power (kW/h)', data, 2, '0.00');
                }
            },
        ],
        "initComplete": function (settings, json) {
            $('#spanRunnigpowerCount').text(this.api().data().length);
            if (($(window).width() > 1200)) {
                $('.height-equal-boxestable').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    if ($.fn.DataTable.isDataTable('#dtengine')) {
        $('#dtengine').DataTable().destroy();
    }
    var engines = $('#dtengine').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "data": nooncomments,
        "columns": [
            {
                className: "tdblock data-text-align",
                "data": "Type",
                "width": "300px",
                render: function (data, type, full, meta) { return GetCellData('Type', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-text-align",
                "data": "Reason",
                "width": "180px",
                render: function (data, type, full, meta) { return GetCellData('Reason', IsNullOrEmpty(data) ? "" : data); }

            },
            {
                className: "data-text-align",
                "data": "Comment",
                "width": "150px",
                render: function (data, type, full, meta) { return GetCellData('Comment', IsNullOrEmpty(data) ? "" : data); }
            }
        ],
        "initComplete": function (settings, json) {
            $('#spanEngineCount').text(this.api().data().length);
            if (($(window).width() > 1200)) {
                $('.height-equal-boxestable').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    if ($.fn.DataTable.isDataTable('#dtDeckLogWeather')) {
        $('#dtDeckLogWeather').DataTable().destroy();
    }
    var decklogweather = $('#dtDeckLogWeather').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><"table-horizontal-scroll table-horizontal-scroll-modal2"rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "scrollX": true,
        "scrollCollapse": true,
        "order": [],
        "orderable": false,
        "data": noonreportweather,
        "columns": [
            {
                className: "data-datetime-align",
                "data": "PhwRecordedFrom",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedTimeFromDate(type, 'From', data); }
            },
            {
                className: "data-datetime-align",
                "data": "PhwRecordedAt",
                "width": "50px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedTimeFromDate(type, 'To', data); }

            },
            {
                className: "data-number-align",
                "data": "PhwWindSpeed",
                "width": "165px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Wind Speed (m/s)', data, 2, ""); }
            },
            {
                className: "data-text-align",
                "data": "PhwWindForce",
                "width": "180px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Wind Force', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-text-align",
                "data": "PhwWindDir",
                "width": "180px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Wind Direction', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-text-align",
                "data": "PdrIdSwellDirection",
                "width": "180px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Swell Direction', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-text-align",
                "data": "WavId",
                "width": "190px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Current Wave Direction', IsNullOrEmpty(data) ? "" : data); }
            }
        ],
        "initComplete": function (settings, json) {
            $('#spanDeckLogWeatherCount').text(this.api().data().length);
            if (($(window).width() > 767)) {
                $('.height-equal-boxes-two').matchHeight({
                    byRow: 0
                });
            }
            $('#noonreport').on('shown.bs.modal', function () {
                if (($(window).width() > 767)) {
                    var newWidth = ($(".table-scroll-width-modal2").width());
                    $(".table-common-design .table-horizontal-scroll-modal2").css({
                        "maxWidth": newWidth
                    });
                    var newWidth2 = ($(".table-scroll-width-modal2").width());
                    $(".table-scroll-width-modal2 table").css({
                        "width": newWidth2
                    });
                }
            });
        },
    });
}

function BindFAOPreportTable() {
    var defaultValue = "0.000";

    var runninghoursjson = $('#divRunningHoursArr').data('runninghours');
    var fueljson = $('#divROBFuelArr').data('fuellist');
    var wastejson = $('#divROBWasteArr').data('wastelist');
    var freshwatejson = $('#divFreshWaterArr').data('freshwaterlist');
    var lubeoiljson = $('#divLubeOilArr').data('lubeoillist');
    var cargoballastjson = $('#divCargoBallastArr').data('cargoballastlist');

    if ($.fn.DataTable.isDataTable('#dtrunninghours')) {
        $('#dtrunninghours').DataTable().destroy();
    }
    var runninghours = $('#dtrunninghours').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><"table-horizontal-scroll table-horizontal-scroll-modal"rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "100px",
        "scrollX": true,
        "scrollCollapse": true,
        "order": [],
        "orderable": false,
        "data": runninghoursjson,
        "columns": [
            {
                className: "data-text-align",
                "data": "PartName",
                "width": "39px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetCellData('Component', IsNullOrEmpty(data) ? "-" : data);
                }
            },
            {
                className: "data-number-align",
                "data": "Previous",
                "width": "110px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Part Hrs', data, 2, "0.00");
                }
            },
            {
                className: "data-number-align",
                "data": "Total",
                "width": "110px",
                "orderable": false,
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Total Hrs', data, 2, "0.00");
                }
            },
        ],

        "initComplete": function (settings, json) {
            $('#spanRunninghoursCount').text(this.api().data().length);
            if (($(window).width() > 767)) {
                $('.height-equal-box').matchHeight({
                    byRow: 0
                });
            }

            $('#faop').on('shown.bs.modal', function () {
                if (($(window).width() > 767)) {
                    var newWidth = ($(".table-scroll-width-modal").width());
                    $(".table-common-design .table-horizontal-scroll-modal").css({
                        "maxWidth": newWidth
                    });
                    var newWidth2 = ($(".table-scroll-width-modal").width());
                    $(".table-scroll-width-modal table").css({
                        "width": newWidth2
                    });
                }
            });

        },
    });

    if ($.fn.DataTable.isDataTable('#fuel')) {
        $('#fuel').DataTable().destroy();
    }
    var fuel = $('#fuel').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "order": [],
        "autoWidth": false,
        "paging": false,
        "data": fueljson,
        "columns": [
            {
                className: "data-text-align boldheader",
                'orderable': false,
                'targets': 0,
                "data": "Title",
                "width": "45px",
                render: function (data, type, full, meta) {
                    return GetCellData('Fuel', IsNullOrEmpty(data) ? "-" : data);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobFo",
                "width": "60px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'HFO (mt)', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobLsfo",
                "width": "60px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'LFO (mt)', data, 3, defaultValue);
                }

            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobDo",
                "width": "60px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'DO (mt)', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobGo",
                "width": "60px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'GO (mt)', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobLng",
                "width": "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'LNG Bunker (mt)', data, 3, defaultValue);
                }
            },
        ],
        "initComplete": function (settings, json) {
            $('#spanFuelCount').text(this.api().data().length);
        },
    });

    if ($.fn.DataTable.isDataTable('#dtwaste')) {
        $('#dtwaste').DataTable().destroy();
    }
    var waste = $('#dtwaste').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": wastejson,
        "columns": [
            {
                className: "data-text-align boldheader",
                'orderable': false,
                "data": "Title",
                "width": "45px",
                render: function (data, type, full, meta) { return GetCellData('Waste', IsNullOrEmpty(data) ? "-" : data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobSludge",
                "width": "80px",
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Sludge', data, 3, defaultValue); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobBilge",
                "width": "80px",
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Bilge', data, 3, defaultValue); }

            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobSlops",
                "width": "80px",
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Slops', data, 3, defaultValue); }

            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobSewage",
                "width": "80px",
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Sewage', data, 3, defaultValue); }
            },
        ],
        "initComplete": function (settings, json) {
            $('#spanWasteCount').text(this.api().data().length);
        },
    });

    if ($.fn.DataTable.isDataTable('#dtcargo')) {
        $('#dtcargo').DataTable().destroy();
    }
    var cargoballast = $('#dtcargo').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": cargoballastjson,
        "columns": [
            {
                className: "data-text-align boldheader",
                'orderable': false,
                "data": "Title",
                "width": "114px",
                render: function (data, type, full, meta) { return GetCellData('Cargo & Ballast', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "SpaCargoBallastQty",
                "width": "70px",
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Quantity', data, 2, "0.00"); }
            }
        ],
        "initComplete": function (settings, json) {

        },
    });

    if ($.fn.DataTable.isDataTable('#dtFreshWater')) {
        $('#dtFreshWater').DataTable().destroy();
    }
    var freshWater = $('#dtFreshWater').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": freshwatejson,
        "columns": [
            {
                className: "data-text-align boldheader",
                'orderable': false,
                'targets': 0,
                "data": "Title",
                "width": "60px",
                render: function (data, type, full, meta) { return GetCellData('Fresh Water', IsNullOrEmpty(data) ? "-" : data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobDomestic",
                "width": "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Domestic (mt)', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobTechnical",
                "width": "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Technical (mt)', data, 3, defaultValue);
                }

            },
        ],
        "initComplete": function (settings, json) {

        },
    });

    if ($.fn.DataTable.isDataTable('#dtLubeOil')) {
        $('#dtLubeOil').DataTable().destroy();
    }
    var lubeOil = $('#dtLubeOil').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        'targets': 0,
        "paging": false,
        "order": [],
        "data": lubeoiljson,
        "columns": [
            {
                className: "data-text-align boldheader",
                'orderable': false,
                "data": "Title",
                "width": "56px",
                render: function (data, type, full, meta) {
                    return GetCellData('Lube Oil', IsNullOrEmpty(data) ? "-" : data);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobClo",
                "width": "64px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'CLO', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobCrankCase",
                "width": "72px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Crank Case', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "RobAuxLubeOil",
                "width": "62px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Auxilaries', data, 3, defaultValue);
                }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "SpaGeneralLubeOilRob",
                "width": "70px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'General', data, 3, defaultValue);
                }
            },
        ],
        "initComplete": function (settings, json) {

        },
    });
}

function SectionMatchHights() {
    if (screen.width > MobileScreenSize) {
        $('.height-equal-box').matchHeight({
            byRow: 0
        });
        $('.height-equal').matchHeight({
            byRow: 0
        });
        $('.height-equal-boxes').matchHeight({
            byRow: 0
        });
        $('.height-equal-boxes-two').matchHeight({
            byRow: 0
        });
        $('.height-equal-boxes-weather').matchHeight({
            byRow: 0
        });
    }

    if (screen.width > 992) {
        $('.height-equal-boxes-log').matchHeight({
            byRow: 0
        });
        $('.height-equal-boxes-engine').matchHeight({
            byRow: 0
        });
    }

    if (screen.width > 1200) {
        $('.height-equal-boxes-enginetwo').matchHeight({
            byRow: 0
        });
        $('.height-equal-boxestable').matchHeight({
            byRow: 0
        });
    }

}


function GetChangeInDestinationReport(spaId) {
    $.ajax({
        url: "/VoyageReporting/GetChangeInDestinationReport",
        type: "POST",
        data: {
            "posId": $("#PositionListId").val(),
            "spaId": spaId,
            "vesselId": encryptedVesselDetail
        },
        "datatype": "html",
        success: function (result) {
            $('#divDestinationReport').empty();
            $('#divDestinationReport').html(result);
            $('#destination').modal('show');
            SectionMatchHights();

            if (screen.width < 760) {
                headerReadMore('destinationheadershowmorewrapper', 'header');
              
            }

        }
    });
}

