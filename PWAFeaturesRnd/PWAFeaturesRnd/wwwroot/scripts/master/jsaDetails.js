import moment from "moment";

require('bootstrap');

import { AjaxError, SetHeaderMargin, headerReadMore, GetCookie, AddClassIfAbsent, RemoveClassIfPresent, BackButton, AddLoadingIndicator, RemoveLoadingIndicator, base64ToArrayBuffer, saveByteArray, IsNullOrEmptyOrUndefined, IsNullOrEmptyOrUndefinedLooseTyped, ToastrAlert, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, RegisterTabSelectionEvent, FormatDateCustom } from "../common/utilities.js";
import { GetCellData, GetFormattedDateTime, GetFormattedDate } from "../common/datatablefunctions.js";
import { JSADetailsPageKey, MobileScreenSize } from "../common/constants.js"
import { RecordLevelNote } from "../common/notesUtilities.js"

var JobId;
var jsaRiskAssessmentHazard;
var DownloadAttachment;

var statusColorMap = new Map();
statusColorMap.set(0, { textColor: "txt-orange" });
statusColorMap.set(1, { textColor: "text-green" });
statusColorMap.set(5, { textColor: "text-grey" });
statusColorMap.set(6, { textColor: "text-red" });

var riskColorMap = new Map();
riskColorMap.set(0, { textColor: "text-yellow" });
riskColorMap.set(1, { textColor: "text-green" });
riskColorMap.set(7, { textColor: "text-red" });
riskColorMap.set(6, { textColor: "txt-orange" });
var tabs = { SummaryTab: 1, RiskAssessmentTab: 2, AttachmentTab: 3 };
var dataLoad = new Set();

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

$('#mobileactiontoggle').click(function () {
    $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
});

$(document).click(function () {
    if ($("#mobileActiondropdown").hasClass('show')) {
        $("#mobileActiondropdown").removeClass('show');
    }
});

$('#authorizetoggle').click(function () {
    $('.authorizejsadropdown .dropdown .dropdown-menu').toggleClass('show');
});

$(document).click(function () {
    if ($("#authorizedrop").hasClass('show')) {
        $("#authorizedrop").removeClass('show');
    }
});

$(document).ready(function () {
    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();

    $(".offjsaclear").click(function () {
        $('.offccommentstext').show();
        $('#commentssectionjsa').hide();
    });
    $(".offjsasave").click(function () {
        $('.offccommentstext').show();
        $('#commentssectionjsa').hide();
    });

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

    if (screen.width < 760) {
        headerReadMore('headershowmorewrapper', 'header');
    }

    if (screen.width > 767) {
        $('.height-equal-jsa').matchHeight({
            byRow: 0
        });
    }

    $('#componentsclick').click(function () {
        $("#safetyprecautiondiv").hide();
        $("#componentsdiv").show();
        $("#safetyclick").removeClass("active");
        $("#componentsclick").addClass("active");
    });
    $('#safetyclick').click(function () {
        $("#safetyprecautiondiv").show();
        $("#componentsdiv").hide();
        $("#safetyclick").addClass("active");
        $("#componentsclick").removeClass("active");
    });

    $('#crewlist').click(function () {
        $("#crewlistdiv").show();
        $("#othermeetingdiv").hide();
        $("#guidelinesdiv").hide();
        $("#crewlist").addClass("active");
        $("#otherclick").removeClass("active");
        $("#guideclick").removeClass("active");
    });
    $('#otherclick').click(function () {
        $("#othermeetingdiv").show();
        $("#crewlistdiv").hide();
        $("#guidelinesdiv").hide();
        $("#otherclick").addClass("active");
        $("#crewlist").removeClass("active");
        $("#guideclick").removeClass("active");
    });
    $('#guideclick').click(function () {
        $("#guidelinesdiv").show();
        $("#othermeetingdiv").hide();
        $("#crewlistdiv").hide();
        $("#guideclick").addClass("active");
        $("#crewlist").removeClass("active");
        $("#otherclick").removeClass("active");
    });

    JobId = $('#EncryptedJobId').val();

    //Sidebar back
    BackButton(JSADetailsPageKey, false)

    LoadSummaryDetails(JobId);
    RegisterTabSelectionEvent('.mobileTabClick', JSADetailsPageKey);


    $(".dropdown-tab-2").on('click', function () {
        if (!dataLoad.has(tabs.RiskAssessmentTab)) {
            GetJSARiskTabHazardSummary(JobId);
            dataLoad.add(tabs.RiskAssessmentTab);
        }
    });

    //JSA Summary
    $('.dropdown-tab-1').click(function () {
        if (!dataLoad.has(tabs.SummaryTab)) {
            LoadSummaryDetails(JobId);
        }
    });

    $('#dtHazards').on('click', 'tbody tr.group-start', function () {
        var name = $(this).data('name');
        var statusId = $(this).data('id');
        $('*[data-name="' + name + '_toggle"]').toggle();
        if ($('#togglestatus_' + statusId).text() == '-')
            $('#togglestatus_' + statusId).text('+');
        else
            $('#togglestatus_' + statusId).text('-');
    });

    $('#dthazardsRiskTab').on('click', 'tbody tr.group-start', function () {
        var name = $(this).data('name');
        var statusId = $(this).data('id');
        $('*[data-name="' + name + '_toggle"]').toggle();
        if ($('#togglestatusRiskTab_' + statusId).text() == '-')
            $('#togglestatusRiskTab_' + statusId).text('+');
        else
            $('#togglestatusRiskTab_' + statusId).text('-');
    });


    $("#dthazardsRiskTab").on('click', 'tbody tr', function () {
        var hazardId = jsaRiskAssessmentHazard.row(this).data().jahId;
        if (!IsNullOrEmptyOrUndefined(hazardId)) {
            GetJSAHazardAdditionalDetails(hazardId);
        }
    });

    $('.dropdown-tab-3').click(function () {
        if (!dataLoad.has(tabs.AttachmentTab)) {
            GetAttachmentList(JobId);
            dataLoad.add(tabs.AttachmentTab);
        }
    });

    $("#btnSimultaneousJobs, #btnSimultaneousJobsHeader").on('click', function () {
        BindSimultaneousJobsSummary(JobId);
    });
    var MobilTabCls = $("#ActiveMobileTabClass").val();
    $('.' + MobilTabCls)[0].click();
    //dropdown tab element
    if (($(window).width() < MobileScreenSize)) {
        var selectedTabElement = $('#dropdowntablist .' + MobilTabCls)[0]
        if (!($(selectedTabElement).hasClass('modal-click'))) {
            $(".dropdowntabtitle").html($(selectedTabElement).text());
            $('#dropdowntablist li').removeClass("active");
            $(selectedTabElement).parent().addClass('active')
        }
    }
    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);
    GetDiscussionNotesCount(messageDetailsJSON);
    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON, code);

    const remarkapprovejsa = document.getElementById("remarkapprovejsa");
    remarkapprovejsa.addEventListener("input", (e) => {
        var remark = $('#remarkapprovejsa').val();
        if (!IsNullOrEmptyOrUndefinedLooseTyped(remark.trim())) {

            $('#remarkapprovejsa').css({ "border-color": '' });
        }
        else {
            $('#remarkapprovejsa').css({ "border-color": 'red' });
        }
    });
    const remarkrejectjsa = document.getElementById("remarkrejectjsa");
    remarkrejectjsa.addEventListener("input", (e) => {
        var remark = $('#remarkrejectjsa').val();
        if (!IsNullOrEmptyOrUndefinedLooseTyped(remark.trim())) {
            $('#remarkrejectjsa').css({ "border-color": '' });
        }
        else {
            $('#remarkrejectjsa').css({ "border-color": 'red' });
        }
    });
    const remarkreopenjsa = document.getElementById("remarkreopenjsa");
    remarkreopenjsa.addEventListener("input", (e) => {
        var remark = $('#remarkreopenjsa').val();
        if (!IsNullOrEmptyOrUndefinedLooseTyped(remark.trim())) {
            $('#remarkreopenjsa').css({ "border-color": '' });
        }
        else {
            $('#remarkreopenjsa').css({ "border-color": 'red' });
        }
    });
    const spanOfficeCommentstextarea = document.getElementById("spanOfficeCommentstextarea");
    spanOfficeCommentstextarea.addEventListener("input", (e) => {
        var officecomments = $('#spanOfficeCommentstextarea').val();
        if (!IsNullOrEmptyOrUndefinedLooseTyped(officecomments.trim())) {
            $('#spanOfficeCommentstextarea').css({ "border-color": '' });
        }
        else {
            $('#spanOfficeCommentstextarea').css({ "border-color": 'red' });
        }
    });
    $("#btnoffcsave").on('click', function () {
        HandleOfficecommentsSaveClick();
    });

    -    $("#btnscenario").on('click', function () {
        GetAllJsaWorkflow();
    });

    GetJsaWorkflow();
});

function LoadSummaryDetails(JobId) {
    GetJSADetailsHeaderSummary(JobId);
    ShowAuthorizeActionsBasedOnStatus();
    GetJsaJobDetails(JobId);
    GetJSASummarytHazardsDetails(JobId);
    GetJsaCrewList(JobId);
    GetJsaAttributeSummary(JobId);
    GetJsaTaskBreakDownSummary(JobId);
    GetJSAComponentSummary(JobId);
    GetJSAMeetingGuidelines();
    dataLoad.add(tabs.SummaryTab);
    BindControlActionSaveEvent();
}

function GetJsaJobDetails(encryptedJobId) {
    $.ajax({
        url: "/JSA/GetJSASummaryDetails",
        dataType: "JSON",
        type: "GET",
        data: {
            "encryptedJobId": encryptedJobId
        },
        success: function (data) {
            if (data != null) {

                $('#spanJsaName').text(data.jsaName);
                $('#spanResponsibility').text(data.responsibility);
                $('#spanSystemArea').text(data.systemArea);
                $('#spanCreatedBy').text(data.createdBy);
                $('#spanMeetingDate').text(data.meetingDate);
                $('#spanProposedStartDate').text(data.proposedStartDate);
                $('#spanApproveStartDate').text(data.proposedStartDate);
                $('#spanRejectStartDate').text(data.proposedStartDate);
                $('#spanReopenStartDate').text(data.proposedStartDate);

                $('#spanApproveEstEndDate').text(data.estimatedEndDate);
                $('#spanRejectEstEndDate').text(data.estimatedEndDate);
                $('#spanReopenEstEndDate').text(data.estimatedEndDate);
                $('#spanEstimatedEndDate').text(data.estimatedEndDate);

                $('#spanReason').html(stringToHtmlFormat(data.reasons));
                $('#spanSummaryDetails').html(stringToHtmlFormat(data.details));
                $('#spanOfficeComments').html(stringToHtmlFormat(data.officeComments));
                $('#spanOfficeCommentstextarea').html(stringToHtmlFormat(data.officeComments));
            }
        }
    });
}


function GetJSASummarytHazardsDetails(encryptedJobId) {
    $('#dtHazards').DataTable().destroy();
    var jsahazourds = $('#dtHazards').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "scrollY": "275px",
        "scrollCollapse": true,
        "paging": false,
        "pageLength": 25,
        "ordering": false,
        "order": [],
        "language": {
            "emptyTable": "No data available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/JSA/GeJSASummarytHazardsDetails",
            "type": "POST",
            "data":
            {
                "encryptedJobId": encryptedJobId,
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "d-none",
                "data": "workActivityDescription",
                width: "75px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-number-align",
                data: "hazardNumber",
                width: "20px",
                render: function (data, type, full, meta) { return GetCellData('No.', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "description",
                width: "240px",
                render: function (data, type, full, meta) {
                    return GetCellData('Hazards Description', data);
                }
            },
            {
                className: "data-text-align",
                data: "likelihoodDescription",
                width: "75px",
                render: function (data, type, full, meta) {
                    let kpiColor = RiskKPIConverter(full.likelihoodColor);
                    let coloredData = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    return GetCellData('Likelihood', coloredData);
                }
            },
            {
                className: "data-text-align",
                data: "severityDescription",
                width: "60px",
                render: function (data, type, full, meta) {
                    let kpiColor = RiskKPIConverter(full.severityColor);
                    let coloredData = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    return GetCellData('Severity', coloredData);
                }

            },
            {
                className: "data-text-align position-relative",
                data: "riskFactorDescription",
                width: "75px",
                render: function (data, type, full, meta) {
                    let RiskFactorUIElement = '';
                    let kpiColor = RiskKPIConverter(full.riskColor);
                    RiskFactorUIElement = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    if (full.isInitialRiskVisible) {
                        RiskFactorUIElement += "<i class='fa fa-exclamation-circle ml-2 BrushOrange d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.initialRiskFactorDescription + " <br/> Further control measures added.'></i>";
                        RiskFactorUIElement += "<i class='fa fa-caret-right orange-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.initialRiskFactorDescription + " <br/> Further control measures added.'></i>";
                    }

                    return GetCellData('Risk Factor', RiskFactorUIElement);
                }
            }
        ],
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(0, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    let localWorkDescriptionArray = group.split('+');
                    let localDisplayName = '';
                    if (IsNullOrEmptyOrUndefinedLooseTyped(localWorkDescriptionArray[1])) {
                        localDisplayName = localWorkDescriptionArray[0];
                    }
                    else {
                        localDisplayName = localWorkDescriptionArray[0] + ' \\ ' + localWorkDescriptionArray[1] + ' ' + localWorkDescriptionArray[2];
                    }
                    $(rows).eq(i).before(
                        '<tr data-name="' + group + '" data-id="' + i + '" class="group group-start font-weight-bolder"><th colspan="5"><span id="togglestatus_' + i + '">-</span>&nbsp;' + localDisplayName + '</th></tr>'
                    )
                    last = group;
                }
                $(rows).eq(i).attr('data-name', group + '_toggle');
            });
        },
        "initComplete": function () {
            $('#spanHazardCount').text(this.api().data().length);

        }
    });

    $('#dtHazards').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });

}

function BindCrewDetails(data) {
    $('#dtcrewlist').DataTable().destroy();
    var crewList = $('#dtcrewlist').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": true,
        "scrollY": "235px",
        "scrollX": true,
        "paging": false,
        "pageLength": 25,
        "language": {
            "emptyTable": "No crew details available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": data,
        "columns": [
            {
                className: "tdblock data-text-align",
                data: "crewFullName",
                width: "110px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-text-align",
                data: "rank",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetCellData('Rank', data);
                }
            },
            {
                className: "data-text-align",
                data: "department",
                width: "101px",
                render: function (data, type, full, meta) {
                    return GetCellData('Dept', data);
                }
            },
            {
                className: "data-icon-align",
                data: "hasMeetingAttended",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var uiElement = "";
                        if (data != null) {
                            if (data == true) {
                                uiElement = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
                            }
                            else {
                                uiElement = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
                            }
                        }
                        return GetCellData('Meeting Attended', uiElement);
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align",
                data: "hasWorkInstructionUnderstood",
                width: "80px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var uiElement = "";
                        if (data != null) {
                            if (data == true) {
                                uiElement = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
                            }
                            else {
                                uiElement = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
                            }
                        }
                        return GetCellData('Work Instruction Understood', uiElement);
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align",
                data: "hasSatisfiedWithPrecaution",
                width: "80px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var uiElement = "";
                        if (data != null) {
                            if (data == true) {
                                uiElement = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
                            }
                            else {
                                uiElement = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
                            }
                        }
                        return GetCellData('Satisfied With Precautionsd', uiElement);
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align",
                data: "notes",
                width: "50px",
                render: function (data, type, full, meta) {
                    var uiElement = "";
                    if (full.isNotesVisible) {
                        uiElement = '<img src="/images/notes.svg" width="12" data-placement="left" data-toggle="tooltip" title="' + data + '"/>';
                    }
                    return GetCellData('Notes', uiElement);
                }
            }
        ],
        "initComplete": function () {
            $('#spanCrewListCount').text(this.api().data().length);
        }
    });
    $('#dtcrewlist').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
}


function GetJsaAttributeSummary(encryptedJobId) {
    $.ajax({
        url: "/JSA/GetJsaAttributeSummary",
        dataType: "JSON",
        type: "GET",
        data: {
            "encryptedJobId": encryptedJobId
        },
        success: function (data) {
            if (data != null) {
                BindPermitList(data.officeApprovalCollection);
                BindSafetyPrecautions(data.safetyPrecautionList, data.safetyPrecautionTotalCount);
                BindEmergencyCommunicationProtocol(data.communicationProtocol, data.communicationProtocolDescription);
                BindOtherSafetyPrecautions(data.isOtherSafetyAvailable, data.otherSafetyPrecaution);
            }
        }
    });
}

function BindOtherSafetyPrecautions(isOtherSafetyAvailable, otherSafetyPrecaution) {
    if (isOtherSafetyAvailable) {
        let localRow = LocalSectionOfRowHTML('Others');
        $('.jsaSafetyPrecautionSection').append(localRow);
        RemoveClassIfPresent('#divOtherSafetyPrecautionAvailable', 'd-none');
        $('#spanOtherSafetyPrecautionAvailable').text(otherSafetyPrecaution);
    }
}

function BindEmergencyCommunicationProtocol(communicationProtocol, communicationProtocolDescription) {
    $('#emergencyProtocol').prop('title', communicationProtocolDescription);
    $('#spanEmergencyCommunicationProtocpol').text(communicationProtocol);
}

function BindSafetyPrecautions(safetyPrecautionCollection, safetyPrecautionTotalCount) {
    $('.jsaSafetyPrecautionSection').empty();
    $('#spanSafetyPrecautionCount').text(safetyPrecautionTotalCount);
    if (safetyPrecautionCollection != null && safetyPrecautionCollection.length > 0) {
        for (let i = 0; i < safetyPrecautionCollection.length; i++) {
            let localRow = LocalSectionOfRowHTML(safetyPrecautionCollection[i].attributeName);
            $('.jsaSafetyPrecautionSection').append(localRow);
        }
    }
}

function BindPermitList(officeApprovalCollection) {
    $('#dtpermit').DataTable().destroy();
    var permitList = $('#dtpermit').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "language": {
            "emptyTable": "No permit available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": officeApprovalCollection,
        "columns": [
            {
                className: "tdblock data-text-align",
                data: "attributeName",
                width: "75px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-text-align",
                data: "other",
                width: "135px",
                render: function (data, type, full, meta) {
                    return GetCellData('Description', data);
                }
            },
            {
                className: "data-text-align",
                data: "permitNumber",
                width: "72px",
                render: function (data, type, full, meta) {
                    return GetCellData('Permit No.', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "permitRequestDateTime",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateTime(type, 'Permit Request Date & Time', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "validityFromDateTime",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateTime(type, 'Valdity From Date & Time', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "validityToDateTime",
                width: "135px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateTime(type, 'Validity To Date & Time', data);
                }
            }
        ],
        "initComplete": function () {
            $('#spanPermitCount').text(this.api().data().length)
        }
    });
}

function GetJsaTaskBreakDownSummary(encryptedJobId) {
    $('#dtTaskBreakDown').DataTable().destroy();
    var taskBreakdownList = $('#dtTaskBreakDown').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "scrollY": "300px",
        "scrollX": true,
        "paging": false,
        "pageLength": 25,
        "language": {
            "emptyTable": "No breakdown details available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/JSA/GetJsaTaskBreakDownSummary",
            "type": "POST",
            "data":
            {
                "encryptedJobId": encryptedJobId,
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "tdblock d-table-cell d-md-none data-text-align",
                data: "taskDescription",
                width: "280px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-number-align",
                data: "seqNo",
                width: "30px",
                render: function (data, type, full, meta) {
                    return GetCellData('Seq.No.', data);
                }
            },
            {
                className: "data-text-align",
                data: "stepDescription",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Steps', data);
                }
            },
            {
                className: " d-none d-md-table-cell data-text-align",
                data: "taskDescription",
                width: "150px",
                render: function (data, type, full, meta) {
                    return GetCellData('Task Description', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "estCompletionDateTime",
                width: "130px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateTime(type, 'Est. Completion Date & Time', data);
                }
            },
            {
                className: "data-text-align",
                data: "responsiblePerson",
                width: "200px",
                render: function (data, type, full, meta) {
                    return GetCellData('Name', data);
                }
            },
            {
                className: "data-text-align",
                data: "rank",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetCellData('Rank', data);
                }
            }
        ],
        "initComplete": function () {
            $('#spanTaskBreakDownCount').text(this.api().data().length)
        }
    });

    //table scroll
    if (($(window).width() > 767)) {
        var newWidth = ($(".table-scroll-width").width());
        $(".table-common-design #dtTaskBreakDown_wrapper .table").css({
            "width": newWidth + 220
        });
    }
}

function GetJSADetailsHeaderSummary(encryptedJobId) {
    $.ajax({
        url: "/JSA/GetJSADetailsHeaderSummary",
        dataType: "JSON",
        type: "GET",
        data: {
            "encryptedJobId": encryptedJobId
        },
        success: function (data) {
            if (data != null) {
                let JobName = data.refNo + ' ' + data.title;
                $('#spanHeaderJobName').text(JobName);
                $('#spanScenarioHeaderJobName').text(JobName);
                $('#spanRejectJobName').text(JobName);
                $('#spanReopenJobName').text(JobName);
                $('#spanApproveJobName').text(JobName);
                $('#spanHeaderStatus').text(data.status);
                $('#spanScenarioHeaderStatus').text(data.status);
                IsAddingCommentsAllowed(data.status);
                $('#spanHeaderMaxRisk').text(data.maxRisk);
                AddClassIfAbsent('#spanHeaderStatus', statusColorMap.get(data.statusKPI).textColor);
                AddClassIfAbsent('#spanScenarioHeaderStatus', statusColorMap.get(data.statusKPI).textColor);
                AddClassIfAbsent('#spanHeaderMaxRisk', riskColorMap.get(data.riskKPI).textColor);
                if (data.simultaneousJobVisible == true) {
                    RemoveClassIfPresent('#btnSimultaneousJobsHeader', 'd-none');
                    RemoveClassIfPresent('#btnSimultaneousJobs', 'd-none');
                }
            }
        },
        complete: function () {
            SetHeaderMargin();
        }
    })
}

function RiskKPIConverter(kpi) {
    if (kpi == 'Good') {
        return 'txt-green';
    }
    else if (kpi == 'Warning') {
        return 'text-amber';
    }
    else if (kpi == 'Critical') {
        return 'BrushRed';
    }
}

function getValueOrBlank(data) {
    if (data == null || data == "") {
        return "-";
    }
    return data;
}

function stringToHtmlFormat(str) {
    str = getValueOrBlank(str)
    return str.replace(/(?:\r\n|\r|\n)/g, '<br>');
}

function LocalSectionOfRowHTML(attributeName) {
    let uiComponent = '<div class="col-12 col-md-4 col-lg-6 col-xl-4 my-auto"><span class="dashboard-counters-label mr-0"><img src="/images/tickgreen.svg" class="mr-1" />' + attributeName + '</span></div>';
    return uiComponent;
}

function GetJSARiskTabHazardSummary(encryptedJobId) {
    $('#dthazardsRiskTab').DataTable().destroy();
    jsaRiskAssessmentHazard = $('#dthazardsRiskTab').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "scrollY": "500px",
        "scrollX": false,
        "scrollCollapse": true,
        "ordering": false,
        "order": [],
        "language": {
            "emptyTable": "No risk assessment available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/JSA/GeJSASummarytHazardsDetails",
            "type": "POST",
            "data":
            {
                "encryptedJobId": encryptedJobId,
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "d-none",
                "data": "workActivityDescription",
                width: "75px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-number-align",
                data: "hazardNumber",
                width: "20px",
                render: function (data, type, full, meta) { return GetCellData('No.', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "description",
                width: "240px",
                render: function (data, type, full, meta) {
                    return GetCellData('Hazards Description', data);
                }
            },
            {
                className: "data-text-align",
                data: "likelihoodDescription",
                width: "75px",
                render: function (data, type, full, meta) {
                    let kpiColor = RiskKPIConverter(full.likelihoodColor);
                    let coloredData = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    return GetCellData('Likelihood', coloredData);
                }
            },
            {
                className: "data-text-align",
                data: "severityDescription",
                width: "60px",
                render: function (data, type, full, meta) {
                    let kpiColor = RiskKPIConverter(full.severityColor);
                    let coloredData = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    return GetCellData('Severity', coloredData);
                }

            },
            {
                className: "data-text-align position-relative",
                data: "riskFactorDescription",
                width: "75px",
                render: function (data, type, full, meta) {
                    let RiskFactorUIElement = '';
                    let kpiColor = RiskKPIConverter(full.riskColor);
                    RiskFactorUIElement = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    if (full.isInitialRiskVisible) {
                        RiskFactorUIElement += "<i class='fa fa-exclamation-circle ml-2 BrushOrange d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.initialRiskFactorDescription + " <br/> Further control measures added.'></i>";
                        RiskFactorUIElement += "<i class='fa fa-caret-right orange-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.initialRiskFactorDescription + " <br/> Further control measures added.'></i>";
                    }
                    return GetCellData('Risk Factor', RiskFactorUIElement);
                }
            }
        ],
        "drawCallback": function (settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(0, { page: 'current' }).data().each(function (group, i) {
                if (last !== group) {
                    let localWorkDescriptionArray = group.split('+');
                    let localDisplayName = '';
                    if (IsNullOrEmptyOrUndefinedLooseTyped(localWorkDescriptionArray[1])) {
                        localDisplayName = localWorkDescriptionArray[0];
                    }
                    else {
                        localDisplayName = localWorkDescriptionArray[0] + ' \\ ' + localWorkDescriptionArray[1] + ' ' + localWorkDescriptionArray[2];
                    }
                    $(rows).eq(i).before(
                        '<tr data-name="' + group + '" data-id="' + i + '" class="group group-start font-weight-bolder"><th colspan="5"><span id="togglestatusRiskTab_' + i + '">-</span>&nbsp;' + localDisplayName + '</th></tr>'
                    )
                    last = group;
                }
                $(rows).eq(i).attr('data-name', group + '_toggle');
            });
        },
        "initComplete": function () {
            $('#spanHazardCountRiskTab').text(this.api().data().length);
            if (this.api().data().length > 0) {
                $('#dthazardsRiskTab td:first').click();
            }
        }
    });

    $('#dthazardsRiskTab').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });

}

function GetAttachmentList(encryptedJobId) {
    $.ajax({
        url: "/JSA/GetJSAAttachments",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedJobId": encryptedJobId
        },
        success: function (data) {
            var documentList = data.data;
            //load data table
            $('#dtattachments').DataTable().destroy();
            DownloadAttachment = $('#dtattachments').DataTable({
                "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
                    '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
                "processing": false,
                "serverSide": false,
                "lengthChange": true,
                "searching": false,
                "info": false,
                "autoWidth": false,
                "paging": false,
                "pageLength": 25,
                "ordering": false,
                "order": [],
                "language": {
                    "emptyTable": "No attachment available.",
                    "search": "_INPUT_",
                    "searchPlaceholder": "Search",
                },
                "data": documentList,
                "columns": [
                    {
                        className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
                        data: "ettId",
                        width: "20px",
                        render: function (data, type, full, meta) {
                            if (full.isWebAddressEditable) {
                                return ("<a target='_blank' href='" + full.webAddress + "' class='cursor-pointer d-none d-sm-none d-md-block'><img src='/images/AttachmentLinkIcon.png' class='m-0' width='18' title='View Link'/>")
                            }
                            else {
                                return ("<a class='documentDownload cursor-pointer'><img src='/images/Download-doc-active.png' class='m-0' width='18' title='View Attachment'/>")
                            }
                        }
                    },
                    {
                        className: "mt-0 width-85 top-margin-0 text-break top-margin-0 data-text-align",
                        data: "title",
                        width: "250px",
                        render: function (data, type, full, meta) {
                            if (full.isWebAddressEditable) {
                                if (($(window).width() < MobileScreenSize)) {
                                    return ("<a target='_blank' href='" + full.webAddress + "' class='cursor-pointer'>" + data + "</a>");
                                }
                                else {
                                    return data;
                                }

                            }
                            else {
                                return data;
                            }
                        }
                    },
                    {
                        className: "data-date-align",
                        data: "createdOn",
                        width: "80px",
                        render: function (data, type, full, meta) { return GetFormattedDate(type, 'Created Date', data); }
                    },
                    {
                        className: "data-text-align",
                        data: "type",
                        width: "140px",
                        render: function (data, type, full, meta) { return GetCellData('Type', data); }

                    },
                    {
                        className: "data-text-align tdblock",
                        data: "description",
                        width: "400px",
                        render: function (data, type, full, meta) { return GetCellData('Description', data); }
                    }
                ],
                "initComplete": function () {
                    $('#attachmentsCount').text(this.api().data().length)
                }
            });

            $('#dtattachments tbody').on('click', 'a.documentDownload', function () {
                var data = DownloadAttachment.row($(this).parents('tr')).data();
                DownloadSelectedAttachment(data);
            });
        }
    });
}

function DownloadSelectedAttachment(selectedItem) {

    var documentId = (selectedItem.ettId != null && selectedItem.ettId != 'undefined') ? selectedItem.ettId.trim() : '';
    var documentFileName = (selectedItem.cloudFileName != null && selectedItem.cloudFileName != 'undefined') ? selectedItem.cloudFileName.trim() : '';
    var documentCategory = (selectedItem.documentCategory != null && selectedItem.documentCategory != 'undefined') ? selectedItem.documentCategory : '';
    var input = {
        "identifier": documentId,
        "fileName": documentFileName,
        "documentCategory": documentCategory
    };
    var fileName = selectedItem.title.trim();

    $.ajax({
        url: "/Defect/DownloadDocument",
        type: "POST",
        dataType: "JSON",
        global: false,
        data: {
            "input": JSON.stringify(input)
        },
        success: function (data) {
            if (data.bytes != null) {
                var array = base64ToArrayBuffer(data.bytes);
                saveByteArray(fileName, array, data.fileType);
            } else {
                ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
            }
        }
    });
}

function GetJSAHazardAdditionalDetails(hazardId) {
    $.ajax({
        url: "/JSA/GetJSAHazardAdditionalDetails",
        dataType: "JSON",
        type: "GET",
        data: {
            "hazardId": hazardId
        },
        success: function (response) {
            if (response != null) {
                var data = response.data;
                $("#hazardDescription").text(data.description);
                $("#likelihoodDescription").removeClass();
                $("#likelihoodDescription").addClass(RiskKPIConverter(data.likelihoodColor));
                $("#likelihoodDescription").text(data.likelihoodDescription);
                $("#likelihoodDefinition").attr('data-original-title', data.likelihoodDefinition);
                $("#severityDescription").removeClass();
                $("#severityDescription").addClass(RiskKPIConverter(data.severityColor));
                $("#severityDescription").text(data.severityDescription);
                $("#severityDefinition").attr('data-original-title', data.severityDefinition);
                $("#riskFactorDescription").removeClass();
                $("#riskFactorDescription").addClass(RiskKPIConverter(data.riskColor));
                $("#riskFactorDescription").text(data.riskFactorDescription);
                $("#riskFactorDefinition").attr('data-original-title', data.riskFactorDefinition);
                $("#reportedDate").text(data.reportedDate);
                var controlMeasure = data.controlMeasures.split("\r\n");
                var consequences = data.consequences.split("\r\n");
                $("#hazardConsequences").html(GetHtmlElement(consequences));
                $("#hazardExistingControlMeasures").html(GetHtmlElement(controlMeasure));
                $("#spanFurtherControlMeasuresCount").text(data.furtherControlMeasureCount);
                $("#hazardFurtherControlMeasures").text(data.furtherControlMeasure);
            }
        },
        complete: function () {
            $('[data-toggle="tooltip"]').tooltip();
        }
    })
}

function GetHtmlElement(array) {
    var htmlElement = "";
    $.each(array, function (index, value) {
        htmlElement += "<p> " + value + "</p>";
    });
    return htmlElement;
}

function GetJsaCrewList(encryptedJobId) {
    $.ajax({
        url: "/JSA/GetJsaCrewSummary",
        dataType: "JSON",
        type: "GET",
        data: {
            "encryptedJobId": encryptedJobId
        },
        success: function (data) {
            if (data != null) {
                BindCrewDetails(data.crewMembers);
                BindOtherMembersAttendingList(data.otherMembers);
            }
        }
    });
}

function BindOtherMembersAttendingList(data) {
    $('#dtothermeeting').DataTable().destroy();
    var jsaother = $('#dtothermeeting').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No data available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": data,
        "columns": [
            {
                className: "data-text-align",
                data: "otherIdentityNo",
                width: "50px",
                render: function (data, type, full, meta) { return GetCellData('Identity No', data); }
            },
            {
                className: "data-text-align",
                data: "otherCrewName",
                width: "135px",
                render: function (data, type, full, meta) { return GetCellData('Name', data); }
            },
            {
                className: "data-text-align",
                data: "otherPosition",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Position', data); }
            },
            {
                className: "data-text-align",
                data: "otherCompany",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Company', data); }
            },
            {
                className: "data-icon-align",
                data: "hasMeetingAttended",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var uiElement = "";
                        if (data != null) {
                            if (data == true) {
                                uiElement = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
                            }
                            else {
                                uiElement = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
                            }
                        }
                        return GetCellData('Meeting Attended', uiElement);
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align",
                data: "hasWorkInstructionUnderstood",
                width: "80px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var uiElement = "";
                        if (data != null) {
                            if (data == true) {
                                uiElement = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
                            }
                            else {
                                uiElement = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
                            }
                        }
                        return GetCellData('Work Instruction Understood', uiElement);
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align",
                data: "hasSatisfiedWithPrecaution",
                width: "80px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var uiElement = "";
                        if (data != null) {
                            if (data == true) {
                                uiElement = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
                            }
                            else {
                                uiElement = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
                            }
                        }
                        return GetCellData('Satisfied With Precautionsd', uiElement);
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align",
                data: "notes",
                width: "50px",
                render: function (data, type, full, meta) {
                    var uiElement = "";
                    if (full.isNotesVisible) {
                        uiElement = '<img src="/images/notes.svg" width="12" data-placement="left" data-toggle="tooltip" title="' + data + '"/>';
                    }
                    return GetCellData('Notes', uiElement);
                }
            }
        ],
        "initComplete": function () {
            $('#spanOtherMembersListCount').text(this.api().data().length);
        }
    });
}

function GetJSAComponentSummary(encryptedJobId) {
    $('#dtcomponents').DataTable().destroy();
    var jsapermit = $('#dtcomponents').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No components available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/JSA/GetJsaComponentSummary",
            "type": "GET",
            "data":
            {
                "encryptedJobId": encryptedJobId,
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "tdblock data-text-align",
                data: "componentName",
                width: "75px",
                render: function (data, type, full, meta) {

                    var title = 'Position: ' + full.position + '\nMaker: ' + full.maker + '\nModel: ' + full.model + '\nClass Code: ' + full.classCode;
                    var infoIcon = '<i class="fa fa-exclamation-circle ml-2" data-html="true" data-toggle="tooltip" data-placement="right" title="' + title + '"></i>';
                    var htmlElement = full.componentName + infoIcon;
                    return htmlElement;
                }
            },
            {
                className: "data-text-align",
                data: "jobName",
                width: "70px",
                render: function (data, type, full, meta) { return GetCellData('Job Name', data); }
            },
            {
                className: "data-text-align",
                data: "workOrderStatusDescription",
                width: "60px",
                render: function (data, type, full, meta) { return GetCellData('Status', data); }
            },
            {
                className: "data-date-align",
                data: "reportWorkDoneDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Work done date', data);
                }
            }
        ],
        "initComplete": function () {
            $('#spanComponentCount').text(this.api().data().length);
        }
    });
    $('#dtcomponents').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });
}

function BindSimultaneousJobsSummary(JobId) {
    $('#dtoperation').DataTable().destroy();
    var jsaopr = $('#dtoperation').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No simultaneous jobs available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/JSA/GetJsaSimultaneousJobs",
            "type": "POST",
            "data":
            {
                "JobId": JobId,
                "VesselId": $("#VesselId").val(),
                "StartDateFromUI": $("#spanProposedStartDate").text(),
                "EndDateFromUI": $("#spanEstimatedEndDate").text()
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "d-none d-sm-table-cell text-left data-text-align",
                data: "jsaNo",
                width: "20px",
                render: function (data, type, full, meta) { return GetCellData('JSA No', data); }
            },
            {
                className: "tdblock td-row-header data-text-align",
                data: "jobName",
                width: "135px",
                render: function (data, type, full, meta) {
                    let JobNameUIElement = '<span class="d-inline-block d-sm-none">' + full.jsaNo + '&nbsp; - &nbsp;' + '</span>'
                    JobNameUIElement += full.jobName;
                    JobNameUIElement += '<i class="fa fa-exclamation-circle ml-2" data-html="true" data-toggle="tooltip" data-placement="right" title="' + full.jobDetail + '"></i>';

                    return JobNameUIElement;
                }
            },
            {
                className: "tdblock data-text-align",
                data: "systemArea",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('System Area', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "responsibility",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Responsibility', data); }
            },
            {
                className: "data-datetime-align text-left width-auto ",
                data: "startDateUI",
                width: "40px",
                render: function (data, type, full, meta) {
                    return GetCellData('Proposed Start Date', data);
                }
            },
            {
                className: "data-datetime-align text-left width-auto ",
                data: "endDateUI",
                width: "40px",
                render: function (data, type, full, meta) {
                    return GetCellData('Estimated End Date', data);
                }
            }
        ],
        "initComplete": function () {
            $('#spanSimultaneousOpsCount').text(this.api().data().length);
        }
    });
    $('#dtoperation').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });
}

function GetJSAMeetingGuidelines() {
    $.ajax({
        url: "/JSA/GetJSAMeetingGuidelines",
        dataType: "JSON",
        type: "GET",
        success: function (response) {
            $("#meetingGuidelines").html(response.meetingGuidelines);
        }
    })
}

function ShowAuthorizeActionsBasedOnStatus() {
    var riskIndicator = $('#MaxRisk').val();
    var isApproved = $('#IsApproved').val();
    var approvelink = $('#lnkapprovejsa');
    var hadNoActionItem = true;

    if (isApproved != "True") {
        approvelink.removeAttr('data-target');
        approvelink.css('pointer-events', 'none');
    }
    else {
        if (isHighRiskJob(riskIndicator)) {
            approvelink.attr('data-target', '#confirmjsa');
        }
        else {
            approvelink.attr('data-target', '#approvejsa');
        }
        approvelink.css('pointer-events', 'inherit');
        hadNoActionItem = false;
    }

    var isRejected = $('#IsRejected').val();
    var rejectlink = $('#lnkrejectjsa');

    if (isRejected != "True") {
        rejectlink.removeAttr('data-target');
        rejectlink.css('pointer-events', 'none');
    }
    else {

        rejectlink.attr('data-target', '#rejectjsa');
        rejectlink.css('pointer-events', 'inherit');
        hadNoActionItem = false;
    }

    var isReopened = $('#IsReopened').val();
    var reopenedlink = $('#lnkreopenjsa');

    if (isReopened != "True") {
        reopenedlink.removeAttr('data-target');
        reopenedlink.css('pointer-events', 'none');
    }
    else {
        reopenedlink.attr('data-target', '#reopenjsa');
        reopenedlink.css('pointer-events', 'inherit');
        hadNoActionItem = false;
    }

    if (hadNoActionItem != false) {
        $('#authorizetoggle').attr("disabled", "disabled");
    }
}

function isHighRiskJob(riskIndicator) {
    if (riskIndicator == '2. Low Risk' || riskIndicator == '1. Very Low Risk') {
        return false;
    }
    else {
        return true;
    }
}

function BindControlActionSaveEvent() {
    $('#authorizetoggle').click(function () {
        $("#reopenjsabutton").prop("checked", false);
        $("#continuejsabutton").prop("checked", false);
        EnableDisableConfirmApprove();
    });
    $('#confirmApproveJsa').click(function () {
        HandleConfirmApproveClick();
    });
    $('#btnRejectSave').click(function () {
        HandleRejectSaveClick();
    });
    $('#btnReopenSave').click(function () {
        HandleReopenSaveClick();
    });
    $('#btnApproveSave').click(function () {
        HandleApproveSaveClick();
    });
    $('#reopenjsabutton').change(function () {
        EnableDisableConfirmApprove();
    });
    $('#continuejsabutton').change(function () {
        EnableDisableConfirmApprove();
    });
}

function HandleConfirmApproveClick() {
    if ($("#continuejsabutton").prop("checked")) {
        $("#approvejsa").modal('show');
    }
    else if ($("#reopenjsabutton").prop("checked")) {
        $("#reopenjsa").modal('show');
    }
    else {
        ToastrAlert("validate", "Please select an action.");
    }
}

function HandleRejectSaveClick() {
    var jobId = $('#EncryptedJobId').val();
    var remark = $('#remarkrejectjsa').val();
    var jsaStatus = $('#hdnRejectStatus').val();
    if (remark == "") {
        ToastrAlert("validate", "Please enter remarks.");
        $('#remarkrejectjsa').css({ "border-color": 'red' });
        return;
    }
    else {
        $('#remarkrejectjsa').css({ "border-color": '' });
    }
    $.ajax({
        url: "/JSA/ChangeJSAStatus",
        dataType: "JSON",
        type: "POST",
        data: {
            "jobId": jobId,
            "remark": remark,
            "jsaStatus": jsaStatus
        },
        success: function (data) {
            var modal = $("#rejectjsa");
            HideModal(modal);
            if (data != null && data.isJSAStatusChanged.toString().toLowerCase() == "true") {
                $("#successModalMsg").text("Job rejected successfully");
                HandleSuccessModal();
            } else {
                ToastrAlert("validate", "Reject Job failed.")
            }
        }
    });
}

function HandleReopenSaveClick() {
    var jobId = $('#EncryptedJobId').val();
    var remark = $('#remarkreopenjsa').val();
    var jsaStatus = $('#hdnReopenStatus').val();
    if (remark == "") {
        ToastrAlert("validate", "Please enter remarks.");
        $("#remarkreopenjsa").css({ "border-color": 'red' });
        return;
    }
    else {
        $("#remarkreopenjsa").css({ "border-color": '' });
    }
    $.ajax({
        url: "/JSA/ChangeJSAStatus",
        dataType: "JSON",
        type: "POST",
        data: {
            "jobId": jobId,
            "remark": remark,
            "jsaStatus": jsaStatus
        },
        success: function (data) {
            var modal = $("#reopenjsa");
            HideModal(modal);
            if (data != null && data.isJSAStatusChanged.toString().toLowerCase() == "true") {
                $("#successModalMsg").text("Job reopen successfully");
                HandleSuccessModal();
            } else {
                ToastrAlert("validate", "Reopen Job failed.");
            }
        }
    });
}

function HandleApproveSaveClick() {
    var jobId = $('#EncryptedJobId').val();
    var remark = $('#remarkapprovejsa').val();
    var jsaStatus = $('#hdnApproveStatus').val();
    if (remark == "") {
        ToastrAlert("validate", "Please enter remarks.");
        $("#remarkapprovejsa").css({ "border-color": 'red' });
        return;
    }
    else {
        $("#remarkapprovejsa").css({ "border-color": '' });
        $.ajax({
            url: "/JSA/ChangeJSAStatus",
            dataType: "JSON",
            type: "POST",
            data: {
                "jobId": jobId,
                "remark": remark,
                "jsaStatus": jsaStatus
            },
            success: function (data) {
                var modal = $("#approvejsa");
                HideModal(modal);
                if (data != null && data.isJSAStatusChanged.toString().toLowerCase() == "true") {
                    $("#successModalMsg").text("Job approved successfully");
                    HandleSuccessModal();
                } else {
                    ToastrAlert("validate", "Approve Job failed.");
                }
            }
        });
    }
}

function HandleSuccessModal() {
    $("#authorizeActionSuccessModal").modal('show');
    $('#btnSuccessOk').off();
    $('#btnSuccessOk').on('click', function () {
        $("#authorizeActionSuccessModal").modal('hide');
        ApprovalSuccess(JSADetailsPageKey);
    });

}

function ApprovalSuccess(keyName) {
    AddLoadingIndicator();
    $.ajax({
        url: "/JSA/GetJSAApprovalSuccesUrl",
        type: "POST",
        dataType: "JSON",
        data: {
            "pageKey": keyName
        },
        success: function (data) {
            if (data != null) {
                window.location.replace(data);
            }
        }
    });
}

function EnableDisableConfirmApprove() {
    if ($("#continuejsabutton").prop("checked") || $("#reopenjsabutton").prop("checked")) {
        $('#confirmApproveJsa').removeAttr("disabled");
    }
    else {
        $('#confirmApproveJsa').attr("disabled", "disabled");
    }
}

function HideModal(modal) {
    modal.removeClass("in");
    $(".modal-backdrop").remove();
    modal.hide();
}

function GetJsaWorkflow() {
    $.ajax({
        url: "/JSA/GetJsaTopWorkflow",
        dataType: "JSON",
        type: "POST",
        data: {
            "jobId": $('#EncryptedJobId').val()
        },
        success: function (data) {
            $('#divWorkFlow').empty();
            if (data != null) {
                var previousLogsDetails = data.previousLogsDetails;
                var possibleWorkflowDetails = data.possibleWorkflowDetails;
                var workflowGroupCount = data.workflowGroupCount;

                if (workflowGroupCount > 1) {
                    RemoveClassIfPresent('#scenarioSection', 'd-none');
                }
                if (!IsNullOrEmptyOrUndefinedLooseTyped(previousLogsDetails)) {
                    for (var i = 0; i < previousLogsDetails.length; i++) {

                        let isLastActiveStep = (i + 1) == previousLogsDetails.length && !IsNullOrEmptyOrUndefinedLooseTyped(possibleWorkflowDetails) && possibleWorkflowDetails.length > 0
                        let isLastStep = (i + 1) == previousLogsDetails.length && (IsNullOrEmptyOrUndefinedLooseTyped(possibleWorkflowDetails) || possibleWorkflowDetails.length == 0)

                        var newRow = CreateJsaWorkflowTemplate(previousLogsDetails[i], isLastActiveStep, isLastStep);
                        $('#divWorkFlow').append(newRow);
                    }
                }

                if (!IsNullOrEmptyOrUndefinedLooseTyped(possibleWorkflowDetails)) {
                    for (var i = 0; i < possibleWorkflowDetails.length; i++) {

                        let isLastStep = (i + 1) == possibleWorkflowDetails.length

                        var newRow = CreateJsaWorkflowTemplate(possibleWorkflowDetails[i], false, isLastStep);
                        $('#divWorkFlow').append(newRow);
                    }
                }
            }
        }
    });
}

function GetAllJsaWorkflow() {
    $.ajax({
        url: "/JSA/GetAllJsaWorkflows",
        dataType: "JSON",
        type: "POST",
        data: {
            "jobId": $('#EncryptedJobId').val()
        },
        success: function (data) {
            if (data != null) {
                BindDynamicTable("#tblWorkflowDetailsThead", "#tblWorkflowDetailsTbody", data.data, data.activityList);
            }
        }
    });
}

function CreateJsaWorkflowTemplate(data, isLastActiveStep, isLastStep) {

    let image = '';
    let stepsClass = '';
    let performedBy = '';
    let performedOn = '';
    let performedByRole = '';
    let performedByRoleElement = '';

    if (!IsNullOrEmptyOrUndefined(data.performedByRoleName)) {
        performedByRole = data.performedByRoleName.split(",");
    }

    if (data.isDone) {

        stepsClass = 'activestep'
        performedBy = '<div class="stepscategory">' + data.performedByName + '</div>'
        performedOn = '<div class="stepsdatetime">' + FormatDateCustom(data.perfomedUTCDate, "DD.MM.YYYY HH:mm") + '</div>'

        if (data.isApplicableToVessel) {
            image = '<img src="/images/jsavessel-active.svg" class="float-left" />'
        }
        else {
            image = '<img src="/images/jsaoffice-active.svg" class="float-left offc" />'
        }
    }
    else {

        stepsClass = 'inactivestep'

        if (data.isApplicableToVessel) {
            image = '<img src="/images/jsavessel-inactive.svg" class="float-left" />'
        }
        else {
            image = '<img src="/images/jsaoffice-inactive.svg" class="float-left offc" />'
        }
    }

    if (isLastActiveStep) {
        stepsClass = stepsClass + ' nextstep'
    }
    else if (isLastStep) {
        stepsClass = stepsClass + ' laststep'
    }

    if (performedByRole.length == 1) {
        performedByRoleElement = '<div class="stepssubtitle">' + data.performedByRoleName + '</div>';
    }
    else {
        var performedByRoleSkipFirstList = performedByRole.slice(1);
        var performedByRoleSkipFirstListNewLine = performedByRoleSkipFirstList.join().replaceAll(',', ',<br/>');
        var infoTagElement = '<img src="/images/outline-i.png" class="i-outline-icon w-auto" id="performedByRole" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="' + performedByRoleSkipFirstListNewLine + '" title="">';
        performedByRoleElement = '<div class="stepssubtitle">' + performedByRole[0] + infoTagElement + '  </div>';

    }

    var result = '<div class="steps ' + stepsClass + '">'
        + '<div class="clearfix textposition">'
        + image
        + '<div class="lefttext float-left">'
        + '<div class="stepstitle">' + data.activityName + '</div>'
        + performedByRoleElement
        + performedBy
        + performedOn
        + '</div>'
        + '</div >'
        + '</div >'

    return result;
}

function BindDynamicTable(theadSelector, tbodySelector, result, nameList) {
    if (!IsNullOrEmptyOrUndefined(result) && result.length > 0) {
        var rows = result.length;
        var cols = result[0].possibleWorkflowDetails.length;

        $(theadSelector).html("");
        $(tbodySelector).html("");

        var row = '<tr>';

        row += '<th> Title </th>';

        for (var inner = 0; inner < nameList.length; inner++) {
            row += '<th>' + nameList[inner] + '</th>';
        }
        row += '</tr>';
        $(theadSelector).append(row);
        for (var outter = 0; outter < rows; outter++) {
            var row = '<tr>';

            row += '<td class="tdblock">' + result[outter].workflowName + '</td>';
            cols = result[outter].possibleWorkflowDetails.length

            for (var colheaderindex = 0; colheaderindex < nameList.length; colheaderindex++) {
                var hasRoleName = false;
                var td = '';
                for (var inner = 0; inner < cols; inner++) {
                    if (nameList[colheaderindex] == result[outter].possibleWorkflowDetails[inner].activityName) {
                        var names = result[outter].possibleWorkflowDetails[inner].performedByRoleName;
                        td = '<td><ul><li><span>' + names.split(',').join('</span></li><li><span>') + '</span></li></ul></td>';
                        hasRoleName = true;
                        break;
                    }

                }
                if (!hasRoleName) {
                    td = '<td> N/A </td>';
                }
                row += td;
            }

            row += '</tr>';
            $(tbodySelector).append(row);
        }
    }
}

function HandleOfficecommentsSaveClick() {
    var jobId = $('#EncryptedJobId').val();
    var officecomments = $('#spanOfficeCommentstextarea').val();
    if (IsNullOrEmptyOrUndefinedLooseTyped(officecomments)) {
        ToastrAlert("validate", "Please enter comments.");
        $("#spanOfficeCommentstextarea").css({ "border-color": 'red' });
        return;
    }
    else {
        $("#spanOfficeCommentstextarea").css({ "border-color": '' });
        $.ajax({
            url: "/JSA/ChangeJSAOfficeComments",
            dataType: "JSON",
            type: "POST",
            data: {
                "jobId": jobId,
                "officecomments": officecomments
            },
            success: function (data) {
                if (data != null && data.isCommentsAdded.toString().toLowerCase() == "true") {
                    window.location.reload();
                } else {
                    ToastrAlert("error", "Unable to add comments.");
                }
            }
        });
    }
}

function IsAddingCommentsAllowed(status) {
    if (status == "Planned" || status == "Approval Pending" || status == "Office Approval Pending" || status == "Reopened") {
        $("#offceditlinkjsa").attr("href", "javascript:void(0);");
        $("#offceditjsa").click(function () {
            $('.offccommentstext').hide();
            $('#spanOfficeCommentstextarea').val($('#spanOfficeComments').text());
            $('#commentssectionjsa').show();
        });
    }
    else {
        $("#offceditlinkjsa").removeAttr("href");
    }
}