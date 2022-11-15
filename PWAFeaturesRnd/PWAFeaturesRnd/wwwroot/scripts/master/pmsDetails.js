import toastr from "toastr";
import moment from "moment";
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, headerReadMore, SetHeaderMargin, BackButton, SetValueElseDefault, IsNullOrEmptyOrUndefined, RemoveClassIfPresent, AddClassIfAbsent, ReplaceClass, GetCookie, ToastrAlert, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, GetRoleRightsAsync, SetTooltip, base64ToArrayBuffer, saveByteArray, IsNullOrEmptyOrUndefinedLooseTyped } from "../common/utilities.js";
import { PlannedMaintenanceDetailsPageKey, MobileScreenSize } from "../common/constants.js";
require('bootstrap')
import { RecordLevelNote } from "../common/notesUtilities.js"
import { GetFormattedDate } from "../common/datatablefunctions.js";
import { CloseWOControlId, ApproveRescheduleWorkOrder } from "../common/RoleRightsControlIds";
import { CustomizeAtleast4Characters, CustomizeCannotBeBlank } from "../common/messages.js";

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

var encryptedPwoId, encryptedPwhId;
const Staus_Round = "Round", Status_Certificate = "Certificate", Status_Other = "Other", Status_Defect = "Defect"
var EncryptedSystemAreaId;
var IsCounterBased, processRescheduleWoUrl, maximumCounterExtensionValue, maximumIntervalDays, isBtnProcessWoClicked;

const ColumnIndex_Originalnterval = 11, ColumnIndex_RequestedInterval = 12, ColumnIndex_ApprovedInterval = 13
const ReqSparesDom = '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><"table-horizontal-scroll"rt><"clearfix"<"float-left"l><""p>>>';

const RankDom = '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-4 col-xl-7 offset-xl-3 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>';

const RescheduleHistoryDom = '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><"table-horizontal-scroll"rt><"clearfix"<"float-left"l><""p>>>';

$(document).on("click", function () {
    $('.popover').popover('hide');
    $('body').removeClass('workOrder-reschedue-popover');
});

$(document).on('hidden.bs.modal', '#resceduledrequest', function (e) {
    isBtnProcessWoClicked = false;
    $('#txtExtendedBy').css({ "border-color": '' });
    $('#txtExtendedBy').val(0);
    $('#txtareaApproverComments').css({ "border-color": '' });
    $('#txtareaApproverComments').val('');
    $('#txtareaRiskAssessment').css({ "border-color": '' });
    $('#txtareaRiskAssessment').val('');
    $('#txtareaCurrentConditions').css({ "border-color": '' });
    $('#txtareaCurrentConditions').val('');
    $('#txtareaAdditionalControlMeasuress').css({ "border-color": '' });
    $('#txtareaAdditionalControlMeasuress').val('');
});

$(document).ready(function () {
    AjaxError();
    //$("#pmsCloseWOSuccessModal").modal('show');
    AddLoadingIndicator();
    RemoveLoadingIndicator();

    $("#resceduledrequest").on('shown.bs.modal', function (e) {
        $('.requestext').click(function () {
            $('#btnProcessWO').html($(this).val());
        });
        if (($(window).width() > 1024)) {
            $('.rescehulerheight').matchHeight();
            $('.rescehulerheightreview').matchHeight();
            $('.rescehulerheightreview2').matchHeight();
        }
        var windowheight = $(window).height();
        var modalheader = $('.scrollerheightmodal .modal-header').outerHeight();
        //var modalfooter = $('.scrollerheightmodal .modal-footer').outerHeight();
        var calculateheight = windowheight - modalheader - 75;
        $(".scrollerheightmodal .modal-body .scroller").css({
            "max-height": calculateheight + 'px'
        });
    });

    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
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
    //Sidebar back
    BackButton(PlannedMaintenanceDetailsPageKey, false)

    $('.more').click(function () {
        var sibling = $(this).parents('p,div').siblings('.moretext');
        sibling.slideToggle();
        $(this).hide();
        sibling.find(".less").show();
    });

    $('.less').click(function () {
        var parent = $(this).parents('.moretext');
        parent.slideUp();
        parent.siblings('p,div').find(".more").show();
        $(this).hide();
    });

    var listNavigationLink = "/PlannedMaintenance/List/?PlannedMaintenance=" + $('#PlannedMaintainanceListUrl').val() + "&VesselId=" + $('#EncryptedVesselId').val() + "&IsViewMore=false";
    $("#viewMobileList").attr("href", listNavigationLink);
    $("#viewList").attr("href", listNavigationLink);

    VesselHeaderSummary();
    if ($('#IsNavigatedFromDone').val() == "True") {
        $('#divScheuleTask').hide();
        NavigateDoneDetails();
    }
    else {
        //here navigated from due method will be called		

        $('#divDoneRoundVesselJobDescription').hide();
        $('#divDoneCertificateStatus').hide();
        $('#divDoneRoundStatus').hide();
        $('#divDoneOtherJobDescription').hide()
        $('#divDoneOther').hide();
        $('#divSWOCurrentRWDSparePart').hide();

        ComponentHeirarchyDetails();
        ComponentDetails();
        GetWorkOrderDetails();

        if ($('#IsSWO').val() == "True") {

            $('#divScheuleTask').hide();
            $('#divSparePart').hide();
            $('#divSWOCurrentRWDSparePart').show();
            $('#divShipsWorkOrderDetails').show();
            UnplannedWorkOrderSpecifications();


        } else {

            $('#divScheuleTask').show();
            $('#divSparePart').show();
            $('#divShipsWorkOrderDetails').hide();
            WorkOrderSpecifications();
            SparePartList();
        }

    }
    $("#spanRescheduleHistoryDue a").click(function () {
        RescheduleHistoryLogApiCall();
    });
    $("#spanRescheduleHistoryCertificate a").click(function () {
        RescheduleHistoryLogApiCall();
    });
    $("#spanRescheduleHistoryOther a").click(function () {
        RescheduleHistoryLogApiCall();
    });

    $(document).ajaxStop(function () {
        matchHeight();
        setScrollerHeight();
        if (screen.width < 760) {
            headerReadMore('headershowmorewrapper', 'header');
        }
        SetHeaderMargin();
    });



    var rankslist = $('#dtranks').DataTable({
        "dom": RankDom,
        "searching": false,
        "info": true,
        "language": {
            "emptyTable": "No rank available.",
            "zeroRecords": ""
        },
    });

    $('#SWOCurrentRWDSparePartDt').DataTable({
        "dom": ReqSparesDom,
        "searching": false,
        "info": true,
        "language": {
            "emptyTable": "No spares available.",
        },
    });

    var rescheduleHistory = $('#dtresceduledhistory').DataTable({
        "dom": RescheduleHistoryDom,
        "searching": false,
        "info": true,
        "language": {
            "emptyTable": "No reschedule history available.",
        },
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);
    GetDiscussionNotesCount(messageDetailsJSON);
    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON, code);

    const txtExtendedBy = document.getElementById("txtExtendedBy");
    txtExtendedBy.addEventListener("input", (e) => {
        let approvedValidated = ValidateApprovedExtendedBy($('#txtExtendedBy').val());
        if (approvedValidated.IsSuccess) {
            $('#txtExtendedBy').css({ "border-color": '' });
        }
        else {
            $('#txtExtendedBy').css({ "border-color": 'red' });
        }
    });

    const txtareaApproverComments = document.getElementById("txtareaApproverComments");
    txtareaApproverComments.addEventListener("input", (e) => {

        let inputRequest = {
            "IsApprove": $('input[name="processType"]:checked').val() == "Approve" ? true : false,
            "IsRevise": $('input[name="processType"]:checked').val() == "Revise" ? true : false,
            "IsReject": $('input[name="processType"]:checked').val() == "Reject" ? true : false,
            "Comment": $('#txtareaApproverComments').val()
        }

        if (isBtnProcessWoClicked) {
            let commentValidated = ValidateComment(inputRequest);
            if (commentValidated.IsSuccess) {
                $('#txtareaApproverComments').css({ "border-color": '' });
            }
            else {
                $('#txtareaApproverComments').css({ "border-color": 'red' });
            }
        }
    });

    const txtareaRiskAssessment = document.getElementById("txtareaRiskAssessment");
    txtareaRiskAssessment.addEventListener("input", (e) => {
        TriggerRiskAssessmentValidation();
    });

    const txtareaCurrentConditions = document.getElementById("txtareaCurrentConditions");
    txtareaCurrentConditions.addEventListener("input", (e) => {
        TriggerCurrentConditionsValidation();
    });

    const txtareaAdditionalControlMeasuress = document.getElementById("txtareaAdditionalControlMeasuress");
    txtareaAdditionalControlMeasuress.addEventListener("input", (e) => {
        TriggerAdditionalControlMeasureValidation();
    });

    $("#divRadioriskAssessment input[name='riskAssessment']").click(function () {
        if ($('input:radio[name=riskAssessment]:checked').val() == "Yes") {
            AddClassIfAbsent('#spanRiskAssessmentMandatory', 'd-none');
        }
        else if ($('input:radio[name=riskAssessment]:checked').val() == "No") {
            RemoveClassIfPresent('#spanRiskAssessmentMandatory', 'd-none');
        }
        TriggerRiskAssessmentValidation();
    });

    $("#divRadioCurrentCondition input[name='currentCondition']").click(function () {
        if ($('input:radio[name=currentCondition]:checked').val() == "Yes") {
            AddClassIfAbsent('#spanCurrentConditionMandatory', 'd-none');
        }
        else if ($('input:radio[name=currentCondition]:checked').val() == "No") {
            RemoveClassIfPresent('#spanCurrentConditionMandatory', 'd-none');
        }
        TriggerCurrentConditionsValidation();
    });

    $("#divRadioAdditionalControlMeasures input[name='additionalControlMeasures']").click(function () {
        if ($('input:radio[name=additionalControlMeasures]:checked').val() == "Yes") {
            AddClassIfAbsent('#spanAdditionalControlMeasuresMandatory', 'd-none');
        }
        else if ($('input:radio[name=additionalControlMeasures]:checked').val() == "No") {
            RemoveClassIfPresent('#spanAdditionalControlMeasuresMandatory', 'd-none');
        }
        TriggerAdditionalControlMeasureValidation();
    });

});

function VesselHeaderSummary() {
    var request = {
        "EncryptedVesselId": $('#EncryptedVesselId').val()
    };
    $.ajax({
        url: "/PlannedMaintenance/VesselHeaderDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            if (data != null) {

                $('#spanImoNumber').text(data.imo);
                var totalToolTip = 'Type: ' + data.type + '<br/>' + 'Build Date: ' + data.vesselBuiltDate + '<br/>' + 'Age: ' + data.vesselAge
                $('#vesselDetails').prop('title', totalToolTip);
                //$('[data-toggle="tooltip"]').tooltip();
            }
        }
    });
}

function ComponentHeirarchyDetails() {
    var request = {
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };
    $.ajax({
        url: "/PlannedMaintenance/ComponentHeirarchyDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            $(".componentHeirarchy").empty();
            if (data != null) {
                var i;
                for (i = 0; i < data.length; i++) {
                    var componentName = "<span>" + data[i].componentName + "</span>";
                    $(".componentHeirarchy").append(componentName);
                }
            }
        }
    });
}

function ComponentDetails() {
    var request = {
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };
    $.ajax({
        url: "/PlannedMaintenance/ComponentDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            if (data != null) {
                if ($('#IsNavigatedFromDone').val() == "True") {
                    $('#divSecondHeader').hide();
                    $('#spanComponentSecondHeaderCertificate').text(data.componentCode + " " + data.componentName);
                }
                else {
                    SetValueElseDefault("#spanComponentCode", data.componentCode);
                    SetValueElseDefault("#spanSchedulesTaskComponentName", data.componentName);
                    $('#divSecondHeaderDoneDetails').hide();
                }
                SetValueElseDefault("#spanPosition", data.componentPosition);
                SetValueElseDefault("#spanSchedulesTaskMakerName", data.makerName);
                SetValueElseDefault("#spanSchedulesTaskModel", data.model);
                SetValueElseDefault("#spanWarrantyDate", data.warrantyDate);
                SetValueElseDefault('#spanSchedulesTaskDesigner', data.designer);

                SetValueElseDefault('#spanSchedulesTaskAlternateNumber', data.alternativeNumber);
                if (!IsNullOrEmptyOrUndefined(data.alternativeNumberType)) {
                    SetValueElseDefault('#spanAlternateType', " (" + data.alternativeNumberType + ")");
                }

            }
        }
    });
}

function GetWorkOrderDetails() {
    var request = {
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };
    $.ajax({
        url: "/PlannedMaintenance/WorkOrderDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            if (data != null) {
                $('#spanJobName, #spanPRWJobName').text(data.jobName);
                $('#spanJobTypeDescription').text(data.jobTypeDescription);
                $('#spanWorkOrderStatusCode').text(data.workOrderStatusCode);
                $('#spanExtendedDaysNote').text(data.extendedDaysNote);
                maximumCounterExtensionValue = data.maximumCounterExtensionValue;
                maximumIntervalDays = data.maximumIntervalDays;

                GetRoleRightsAsync([CloseWOControlId], function (rolerights) {
                    var pmsClosureAccess = rolerights.find(x => x.controlId.toLowerCase() === CloseWOControlId.toLowerCase());
                    if (pmsClosureAccess.permission) {
                        if (data.isStatusCompleted == true) {
                            if (($(window).width() < MobileScreenSize)) {
                                RemoveClassIfPresent('.btnPmsCloseActionMobile', 'd-none')
                                AddClassIfAbsent('.btnPmsCloseActionDesktop', 'd-none')
                            }
                            else {
                                RemoveClassIfPresent('.btnPmsCloseActionDesktop', 'd-none')
                                AddClassIfAbsent('.btnPmsCloseActionMobile', 'd-none')
                            }
                        }
                        else {
                            AddClassIfAbsent('.btnPmsCloseActionMobile', 'd-none')
                            AddClassIfAbsent('.btnPmsCloseActionDesktop', 'd-none')
                        }

                    }
                    else {
                        $(".btnPmsCloseAction").remove();
                    }
                });

                GetRoleRightsAsync([ApproveRescheduleWorkOrder], function (rolerights) {
                    var pmsProcessRescheduleWO = rolerights.find(x => x.controlId.toLowerCase() === ApproveRescheduleWorkOrder.toLowerCase());

                    if (pmsProcessRescheduleWO.permission) {
                        if (data.canProcessRescheduleWO == true) {
                            if (($(window).width() < MobileScreenSize)) {
                                RemoveClassIfPresent('.btnPmsProcessRescheduleWOMobile', 'd-none')
                                AddClassIfAbsent('.btnPmsProcessRescheduleWODesktop', 'd-none')
                            }
                            else {
                                RemoveClassIfPresent('.btnPmsProcessRescheduleWODesktop', 'd-none')
                                AddClassIfAbsent('.btnPmsProcessRescheduleWOMobile', 'd-none')
                            }

                            $('#btnProcessWO').off();
                            isBtnProcessWoClicked = false
                            $('#btnProcessWO').click(function () {
                                isBtnProcessWoClicked = true;
                                let IsApprove = $('input[name="processType"]:checked').val() == "Approve" ? true : false;
                                let IsRevise = $('input[name="processType"]:checked').val() == "Revise" ? true : false;
                                let IsReject = $('input[name="processType"]:checked').val() == "Reject" ? true : false;
                                let IsRiskAssessmentMapped = $('input[name="riskAssessment"]:checked').val() == "Yes" ? true : false;
                                let RiskAssessmentMappedComment = $('#txtareaRiskAssessment').val().trim();
                                let IsJobHistoryLinked = $('input[name="currentCondition"]:checked').val() == "Yes" ? true : false;
                                let JobHistoryLinkedComment = $('#txtareaCurrentConditions').val().trim();
                                let IsSupportingWOCreated = $('input[name="additionalControlMeasures"]:checked').val() == "Yes" ? true : false;
                                let SupportingWOCreatedComment = $('#txtareaAdditionalControlMeasuress').val().trim();
                                let ApprovedExtendedBy = $('#txtExtendedBy').val();
                                let Comment = $('#txtareaApproverComments').val();

                                //validationbefore
                                let inputRequest = {
                                    "IsApprove": IsApprove,
                                    "IsRevise": IsRevise,
                                    "IsReject": IsReject,
                                    "IsRiskAssessmentMapped": IsRiskAssessmentMapped,
                                    "RiskAssessmentMappedComment": RiskAssessmentMappedComment,
                                    "IsJobHistoryLinked": IsJobHistoryLinked,
                                    "JobHistoryLinkedComment": JobHistoryLinkedComment,
                                    "IsSupportingWOCreated": IsSupportingWOCreated,
                                    "SupportingWOCreatedComment": SupportingWOCreatedComment,
                                    "ApprovedExtendedBy": ApprovedExtendedBy,
                                    "Comment": Comment
                                }

                                if (ValidationBeforeProcessWO(inputRequest)) {
                                    ProcessWO(inputRequest);
                                }
                            });
                        }
                        else {
                            AddClassIfAbsent('.btnPmsProcessRescheduleWOMobile', 'd-none')
                            AddClassIfAbsent('.btnPmsProcessRescheduleWODesktop', 'd-none')
                        }

                    }
                    else {
                        $(".btnPmsProcessRescheduleWOAction").remove();
                    }
                });

                $('.btnPmsCloseAction').click(function () {
                    $("#pmsClosureActionModal").modal('show');
                    $('#yesPmsClosureAction').off();
                    $('#yesPmsClosureAction').on('click', function () {
                        ClosePms();
                        $("#pmsClosureActionModal").modal('hide');
                    });

                    $('#noPmsClosureAction').off();
                    $('#noPmsClosureAction').on('click', function () {
                        $("#pmsClosureActionModal").modal('hide');
                    });
                });

                $('.btnPmsProcessRescheduleWOAction').click(function () {
                    $('#resceduledrequest').modal('show');
                    LoadProcessRescheduleWO();
                });

                if (!data.isInRangeWorkOrder) {
                    $('#spanIntervalHeader').show();
                    $('#spanInterval').text(data.interval);
                }
                else {
                    $('#spanIntervalHeader').hide();
                }

                if (data.isRunningHrsRangeWorkOrder) {
                    $('#spanRunningHoursHeader').show();
                    $('#spanRunningHours').text(data.runningHoursRange);
                }
                else {
                    $('#spanRunningHoursHeader').hide();
                }

                if (data.isCalendarRangeWorkOrder) {
                    $('#spanCalendarRangeHeader').show();
                    $('#spanCalendarRange').text(data.calendarRange);
                }
                else {
                    $('#spanCalendarRangeHeader').hide();
                }

                if (!data.isInRangeWorkOrder) {
                    $('#spanCurrentDueDateHeader').show();
                    $('#spanCurrentDueDate').text(data.dueDate);
                    let due = moment(data.dueDate, 'DD MMM YYYY').startOf('day');
                    let todaysDate = moment().startOf('day');
                    if (due < todaysDate) {
                        AddClassIfAbsent("#clockIcon", 'common-sub-heading-red');
                        ReplaceClass("#spanCurrentDueDate", 'common-small-heading', 'common-sub-heading-red');
                    };
                }
                else {
                    $('#spanCurrentDueDateHeader').hide();
                }

                if (data.isInRangeWorkOrder) {
                    $('#spanCurrentDueDateRangeHeader').show();
                    $('#spanCurrentDueDateRange').text(data.currentDueDateRange);
                }
                else {
                    $('#spanCurrentDueDateRangeHeader').hide();
                }



                //stored for done - Round.
                EncryptedSystemAreaId = data.encryptedSystemAreaId;
            }
        }
    });
}

function WorkOrderSpecifications() {
    var request = {
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };
    $.ajax({
        url: "/PlannedMaintenance/WorkOrderSpecifications",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            if (data != null) {
                $('#spanSchedulesTaskResponsibleDepartment').text(data.responsibleDepartmentShortCode);
                $('#spanSchedulesTaskResponsibility').text(data.responsibilityRankShortCode);
                $('#spanSchedulesTaskApprover').text(data.approverRequired);
                $('#spanSchedulesTaskOfficeApprover').text(data.officeApprovalRequired);
                $('#spanSchedulesTaskDueDate').text(data.dueDate);
                $('#spanJSARequired').text(data.jsaRequired ? "Yes" : "No");
                $('#spanPermitRequired').text(data.permitRequired ? "Yes" : "No");
                $('#spanCritical').text(data.critical ? "Yes" : "No");


                if (data.isLeftHoursVisible) {
                    if ($('#divSingleJobDescription').hasClass('d-none')) {
                        $('#divSingleJobDescription').removeClass('d-none');
                    }
                    $('#spanSchedulesTaskLeftHrs').text(data.leftHours);
                }
                else {
                    if (!$('#divSingleJobDescription').hasClass('d-none')) {
                        $('#divScheduleLeftHours').addClass('d-none');
                    }
                }

                //job description
                VesselJobDescription(data.jobDescriptionCheck, data.jobDescriptionPart1, data.jobDescriptionPart2, data.jobDescription);

                //vesselGuideLines
                VesselGuideLines(data.pjbId);

                //Job And Rank
                WorkOrderJobAndRank(data.orderRankList, data.totalManHours);

                //Rechedule Work Order History Log				
                encryptedPwoId = data.pwoId;
                encryptedPwhId = data.pwhId;
                IsCounterBased = data.isCounterBased;

                RescheduleHistoryLogApiCall();
            }
        }
    });
}

function UnplannedWorkOrderSpecifications() {
    var request = {
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };
    $.ajax({
        url: "/PlannedMaintenance/GetUnplannedWOSpecification",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            SetValueElseDefault("#spanSWOApprover", data.approver);
            SetValueElseDefault("#spanSWOHodApproval", data.hodApproval);
            SetValueElseDefault("#spanSWOShoreContractor", data.shoreContractorInvolved);
            SetValueElseDefault("#spanSWOResponsibleDepartment", data.responsibleDepartment);
            SetValueElseDefault("#spanSWOResponsibility", data.responsibility);
            var htmlJobDescription = $.parseHTML(data.description);
            $('#spanWholeJobDescription').append(htmlJobDescription);

            if (data.showCurrentRWD == true) {
                GetWorkOrderHistoryDetails(data.workOrderHistoryId, null);

            }
        }
    });
}

function GetWorkOrderHistoryDetails(workOrderHistoryId, scheduleTaskId) {

    let request = {
        "workOrderHistoryId": workOrderHistoryId
    }
    if (!IsNullOrEmptyOrUndefined(scheduleTaskId)) {
        request["scheduleTaskId"] = scheduleTaskId
    }

    $.ajax({
        url: "/PlannedMaintenance/GetWorkOrderHistoryDetails",
        type: "POST",
        dataType: "JSON",
        data: request,

        success: function (data) {
            WorkOrderJobAndRank(data.orderRankList, data.totalManHours);
            BindSparePartList(data.partsUsed);
            encryptedPwhId = data.pwhId;
            RescheduleHistoryLogApiCall();
        }
    });
}

function VesselJobDescription(jobDescriptionCheck, jobDescriptionPart1, jobDescriptionPart2, jobDescription) {

    var htmlJobDescription = $.parseHTML(jobDescription);
    $('#spanWholeJobDescription').append(htmlJobDescription);
}

function VesselGuideLines(request) {
    $.ajax({
        url: "/PlannedMaintenance/VesselGuideLines",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        success: function (data) {
            if (data != null) {
                var html = $.parseHTML(data);

                var $iframe = $('#iframe');
                $iframe.ready(function () {
                    $iframe.contents().find("body").append(html);
                });
            }
        }
    });

}

function WorkOrderJobAndRank(orderRankList, totalManHours) {
    $('#dtranks').DataTable().destroy();
    var rankslist = $('#dtranks').DataTable({
        "dom": RankDom,
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": true,
        "paging": true,
        "data": orderRankList,
        "language": {
            "emptyTable": "No rank available.",
            "zeroRecords": ""
        },
        "columns": [
            {
                className: "top-margin-0 data-text-align",
                data: "rankShortCode",
                orderable: false,
                render: function (data, type, full, meta) {
                    return GetCellData('Rank', '<span data-toggle="tooltip" title="' + full.rankDescription + '">' + data + '</span>');
                }
            },
            {
                className: "top-margin-0  data-number-align",
                data: "manHours",
                render: function (data, type, full, meta) {
                    return GetCellData('Hours', data);
                }
            }
        ],
        "initComplete": function (settings, json) {
            matchHeight();
            setScrollerHeight();
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();
            $(api.column(0).footer()).html('<span class="font-weight-bold">Total</span>');
            $(api.column(1).footer()).html(totalManHours);
        },
    });

}

function RescheduleHistoryLogApiCall() {
    var request = {
        "PwoId": encryptedPwoId,
        "PwhId": encryptedPwhId,
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };

    $.ajax({
        url: "/PlannedMaintenance/GetRescheduleHistoryDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        success: function (data) {
            if (data != null) {
                RescheduleHistoryLog(data.data)
            }
        }
    });
}


function RescheduleHistoryLog(historyList) {
    //show hide of 11 to 13 column based on IsCounter true
    //$("#resceduledhistory").modal("show");

    $('#dtresceduledhistory').DataTable().destroy();
    var rescheduleHistory = $('#dtresceduledhistory').DataTable({
        "dom": RescheduleHistoryDom,
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "language": {
            "emptyTable": "No reschedule history available.",
        },
        'columnDefs': [
            { 'visible': false, 'targets': [ColumnIndex_Originalnterval, ColumnIndex_RequestedInterval, ColumnIndex_ApprovedInterval] }
        ],
        data: historyList,
        "columns": [
            {
                className: "top-margin-0 data-text-align",
                data: "rescheduleRequestType",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Request Type', data);
                }
            },
            {
                className: "top-margin-0 data-datetime-align",
                type: "date",
                data: "originalDueDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null) {
                        date = new Date(data);
                        formattedDate = moment(date).format("D MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Original Due Date', formattedDate);
                    }
                    return date;
                }
            },
            {
                className: "data-datetime-align",
                type: "date",
                data: "requestedDueDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null) {
                        date = new Date(data);
                        formattedDate = moment(date).format("D MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Requested Due Date', formattedDate);
                    }
                    return date;
                }
            },
            {
                className: "data-datetime-align",
                type: "date",
                data: "newDueDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    var date = "";
                    var formattedDate = "";
                    if (data != null) {
                        date = new Date(data);
                        formattedDate = moment(date).format("D MMM YYYY");
                    }
                    if (type === "display") {
                        return GetCellData('Approved Due Date', formattedDate);
                    }
                    return date;
                }
            },
            {
                className: "data-text-align",
                data: "workOrderReasonDescription",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Reason', data);
                }
            },
            {
                className: "data-text-align",
                data: "rescheduleStatusDescription",
                width: "80px",
                render: function (data, type, full, meta) {
                    var kpi = "";
                    if (full.status == "Normal") {
                        kpi = '<span class="BrushOrange">' + data + '</span>'
                        return GetCellData('Status', kpi);
                    }
                    else if (full.status == "Good") {
                        kpi = '<span class="BrushPurple">' + data + '</span>'
                        return GetCellData('Status', kpi);
                    }
                    else if (full.status == "Excellent") {
                        kpi = '<span class="BrushGreen">' + data + '</span>'
                        return GetCellData('Status', kpi);
                    }
                    else if (full.status == "Critical") {
                        kpi = '<span class="BrushRed">' + data + '</span>'
                        return GetCellData('Status', kpi);
                    }
                    else {
                        kpi = '<span class="BrushOrange">' + data + '</span>'
                        return GetCellData('Status', kpi);
                    }
                }
            },
            {
                className: "data-icon-align",
                width: "50px",
                render: function (data, type, full, meta) {
                    var txtresult = "<a href='javascript: void(0);' class='rescheduleHistoryDetails cursor-pointer' id='document_'><img src='/images/outline-i.svg' class='m-0' width='18' /></a>";
                    return GetCellData('Details', txtresult);
                }
            },
            {
                className: "data-text-align tdnowrap",
                data: "requestedBy",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Requester Name', data);
                }
            },
            {
                className: "data-text-align",
                data: "requesterRoleDescription",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Requester Rank', data);
                }
            },
            {
                className: "data-text-align",
                data: "approvedBy",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Approver Name', data);
                }
            },
            {
                className: "data-text-align",
                data: "approverRoleDescription",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Approver Rank', data);
                }
            },
            {
                className: "data-text-align",
                data: "originalInterval",
                width: "80px",
                render: function (data, type, full, meta) {
                    var numberText = "";
                    if (data != null && data != 'undefined' && data != '') {
                        numberText = data;
                        return GetCellData('Original Interval', numberText);
                    }
                    else {
                        return GetCellData('Original Interval', numberText);
                    }
                }
            },
            {
                className: "data-text-align",
                data: "requestedInterval",
                width: "80px",
                render: function (data, type, full, meta) {
                    var numberText = "";
                    if (data != null && data != 'undefined' && data != '') {
                        numberText = data;
                        return GetCellData('Requested Interval', numberText);
                    }
                    else {
                        return GetCellData('Requested Interval', numberText);
                    }
                }
            },
            {
                className: "data-text-align",
                data: "rescheduledInterval",
                width: "80px",
                render: function (data, type, full, meta) {
                    var numberText = "";
                    if (data != null && data != 'undefined' && data != '') {
                        numberText = data;
                        return GetCellData('Approved Interval', numberText);
                    }
                    else {
                        return GetCellData('Approved Interval', numberText);
                    }
                }
            }
        ]
    });

    if (IsCounterBased == true) {
        rescheduleHistory.columns([ColumnIndex_Originalnterval, ColumnIndex_RequestedInterval, ColumnIndex_ApprovedInterval]).visible(true);
    }


    //table scroll
    var newWidth = ($(".table-scroll-width").width());
    $(".table-common-design .table-horizontal-scroll").css({
        "maxWidth": newWidth
    });

    $('#dtresceduledhistory tbody').on('click', 'a.rescheduleHistoryDetails', function () {
        var $this = this;
        var data = rescheduleHistory.row($(this).parents('tr')).data();
        GetRescheduleHistoryDetail(data, $this);
    });
}

function GetRescheduleHistoryDetail(woData, thisSelector) {

    $(thisSelector).popover('dispose');
    $.ajax({
        url: "/PlannedMaintenance/GetWorkOrderRescheduleDetail",
        type: "POST",
        dataType: "JSON",
        global: false,
        data: {
            "workOrderRescheduleId": woData.workOrderRescheduleId,
            "rescheduleRequestTypeId": woData.rescheduleRequestTypeId
        },
        success: function (data) {
            var popOverSettings = woReschedulePopover(data);

            $(thisSelector).popover(popOverSettings);
            $('body').addClass('workOrder-reschedue-popover');
            $(thisSelector).popover('show');
        }
    });

}

function woReschedulePopover(data) {


    var htmlContent = "<span><h5>Requester Details</h5></span>" +
        "<span>" + data.requesterRoleDescription + "</span> </br>" +
        "<span>" + data.requestedBy + "</span></br>" +
        "<span>" + data.requestedOn + "</span></br>";

    let approvedBlock = "<span><h5> Approver Details </h5></span>" +
        "<span>" + data.approverRoleDescription + "</span> </br>" +
        "<span>" + data.approvedBy + "</span></br>" +
        "<span>" + data.approveOn + "</span>";

    if (data.isApprovedRowVisible) {
        htmlContent += '<hr>';
        htmlContent += approvedBlock;
    }

    var popOverSettings = {
        placement: 'auto',
        container: 'body',
        html: true,
        trigger: 'focus',
        selector: '[rel="popover"]',
        content: function () {
            return htmlContent;
        }
    }

    return popOverSettings;
}

function BindSparePartList(data) {
    $('#SWOCurrentRWDSparePartDt').DataTable().destroy();
    $('#SWOCurrentRWDSparePartDt').DataTable({
        "dom": ReqSparesDom,
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "order": [],
        "language": {
            "emptyTable": "No spares available.",
        },
        "data": data,
        "columns": [
            {
                className: "data-text-align tdblock mobile-heading-large",
                data: "partName",
                width: "200px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-text-align",
                data: "makerReferenceNumber",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetCellData('Maker\'s REF No.', data);
                }
            },
            {
                className: "data-text-align",
                data: "plateSheetNumber",
                width: "105px",
                render: function (data, type, full, meta) {
                    return GetCellData('Plate/Sheet No.', data);
                }
            },
            {
                className: "data-text-align",
                data: "drawingPosition",
                width: "120px",
                render: function (data, type, full, meta) {
                    return GetCellData('Drawing Position', data);
                }
            },
            {
                className: "data-text-align",
                data: "location",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetCellData('Location', data);
                }
            },
            {
                className: "data-text-align",
                data: "condition",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetCellData('Condition', data);
                }
            },
            {
                className: "data-number-align",
                data: "quantityUsed",
                width: "60px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var numberText = 0;
                        if (data != null && data != 'undefined' && data != '') {
                            numberText = data;
                            return GetCellData('Qty Used', numberText);
                        }
                        else {
                            return GetCellData('Qty Used', numberText);
                        }
                    }
                    return data;
                }
            },
        ]
    });

    $('#dtspares').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    //table scroll
    var newWidth = ($(".table-scroll-width").width());
    $(".table-common-design .table-horizontal-scroll").css({
        "maxWidth": newWidth
    });
}

function SparePartList() {
    var request = {
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };
    $('#dtspares').DataTable().destroy();
    var sparePart = $('#dtspares').DataTable({
        "dom": ReqSparesDom,
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "order": [],
        "language": {
            "emptyTable": "No spares available.",
        },
        "ajax": {
            "url": "/PlannedMaintenance/SparePartList",
            "type": "POST",
            "data":
            {
                "request": request
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock mobile-heading-large",
                data: "partName",
                width: "130px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "data-text-align",
                data: "makerReferenceNumber",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetCellData('Maker\'s REF No.', data);
                }
            },
            {
                className: "data-text-align",
                data: "plateSheetNumber",
                width: "105px",
                render: function (data, type, full, meta) {
                    return GetCellData('Plate/Sheet No.', data);
                }
            },
            {
                className: "data-text-align",
                data: "drawingPosition",
                width: "120px",
                render: function (data, type, full, meta) {
                    return GetCellData('Drawing Position', data);
                }
            },
            {
                className: "data-number-align",
                data: "pendingOrderCount",
                width: "60px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var numberText = 0;
                        if (data != null && data != 'undefined' && data != '') {
                            numberText = data;
                            return GetCellData('Pending Orders', numberText);
                        }
                        else {
                            return GetCellData('Pending Orders', numberText);
                        }
                    }
                    return data;
                }
            },
            {
                className: "data-number-align pr",
                data: "quantityROB",
                width: "30px",
                render: function (data, type, full, meta) {

                    if (type === "display") {
                        var numberText = 0;
                        if (typeof data !== 'undefined') {
                            numberText = data;
                            if (full.isQuantityRequiredGreaterThanROB) {
                                var robLessThan = "";
                                robLessThan += "<span class='d-sm-block d-md-none'" + '<span>' + numberText + '</span>' + "<i class='fa fa-exclamation-circle ml-1 BrushOrange d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='ROB is less than required quantity.'></i>" + "</span>";

                                robLessThan += "<span class='d-none d-sm-none d-md-block'" + '<span>' + numberText + '</span>' + "<i class='fa fa-caret-right over-due icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='ROB is less than required quantity.'></i>" + "</span>";
                                return GetCellData('ROB', robLessThan);
                            }
                        }
                        else {
                            return GetCellData('ROB', numberText);
                        }
                    }
                    return GetCellData('ROB', data);
                }
            },
            {
                className: "data-text-align",
                data: "isRenewSpares",
                width: "40px",
                render: function (data, type, full, meta) {
                    return GetCellData('Renew Spare', data);
                }
            },
            {
                className: "data-number-align",
                data: "quantityRequired",
                width: "50px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var numberText = NumberToString(data, 2, 1);
                        return GetCellData('QTY REQD', numberText);
                    }
                    return data;
                }
            },
            {
                className: "data-text-align",
                data: "isMarkedForReorder",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetCellData('Mark For Order', data);
                }
            },
            {
                className: "data-number-align",
                type: "html-num",
                data: "reorderQuantity",
                width: "40px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var numberText = "";
                        if (data != null && data != 'undefined' && data != '') {
                            numberText = data;
                            return GetCellData('Order Qty', numberText);
                        }
                        else {
                            return GetCellData('Order Qty', numberText);
                        }
                    }
                    return data;
                }
            },
            {
                className: "data-text-align tdblock",
                data: "remarks",
                width: "95px",
                render: function (data, type, full, meta) {
                    return GetCellData('Remarks', data);
                }
            }
        ]
    });

    $('#dtspares').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    //table scroll
    var newWidth = ($(".table-scroll-width").width());
    $(".table-common-design .table-horizontal-scroll").css({
        "maxWidth": newWidth
    });
}

function NumberToString(number, toFixedValue, state) {
    var numToString = "";
    if (number != null && number != '' && number != 'undefined') {
        numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
            {
                minimumFractionDigits: 2
            });
    }
    else {
        if (state == 1) {
            //when expected return is 0.00
            numToString = "0.00";
        }
        else if (state == 2) {
            //when blank space is expected
            numToString = "";
        }
    }
    return numToString;
}


// -- Done
function NavigateDoneDetails() {
    var request = {
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };

    $.ajax({
        url: "/PlannedMaintenance/GetMaintainaceHistoryDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            if (data.status == Staus_Round) {
                if (data.roundWorkOrderViewModel != null) {
                    GetWorkOrderDetails();
                    ComponentDetails();
                    ComponentHeirarchyDetailsForDoneRound();

                    //bindnig of this data.roundWorkOrderViewModel
                    $('#spanDoneRoundReportWorkDoneDate').text(data.roundWorkOrderViewModel.reportWorkDoneDate);
                    $('#spanDoneRoundRWDDueDate').text(data.roundWorkOrderViewModel.rwdDueDate);
                    $('#spanDoneRoundResponsibilityRank').text(data.roundWorkOrderViewModel.responsibilityRank);
                    var html = $.parseHTML(data.roundWorkOrderViewModel.jobDescription);
                    var $iframe = $('#iframeJobDescription');
                    $iframe.ready(function () {
                        $iframe.contents().find("body").append(html);
                    });

                    //hide spare part section
                    $('#divSparePart').hide();
                    $('#divOfficeJobDescription').hide();
                    $('#divVesselGuideLines').hide();
                    $('#divDoneCertificateStatus').hide();
                    $('#divDoneOther').hide();
                }
            }
            else if (data.status == Status_Certificate) {
                if (data.certificateReportVM != null) {
                    ComponentHeirarchyDetails();
                    ComponentDetails();
                    GetWorkOrderDetails();

                    //bindin of this data.certificateReportVM
                    $('#spanDoneCertificateWorkDoneDate').text(data.certificateReportVM.workDoneDate);
                    $('#spanDoneCertificateOriginalDueDate').text(data.certificateReportVM.originalDueDate);
                    $('#spanDoneCertificateResponsibleRank').text(data.certificateReportVM.responsibleRank);

                    //for job description
                    VesselJobDescription(data.certificateReportVM.jobDescriptionCheck, data.certificateReportVM.jobDescriptionPart1, data.certificateReportVM.jobDescriptionPart2, data.certificateReportVM.officeJobDescription)

                    //for reschedule log
                    encryptedPwhId = data.certificateReportVM.pwhId;
                    IsCounterBased = data.isCounterBased;

                    //hiding divs for certificate
                    $('#divDoneRoundVesselJobDescription').hide();
                    $('#divVesselGuideLines').hide();
                    $('#divSparePart').hide();
                    $('#divDoneRoundStatus').hide();
                    $('#divDoneOther').hide();
                    $('#divDoneOtherJobDescription').hide();
                    $('#divJobRankAndHour').hide();
                    $('#divJobDescription').removeClass('border-right');
                }
            }
            else if (data.status == Status_Defect) {

            }
            else if (data.status == Status_Other) {
                if (data.otherWorkOrderViewModel != null) {
                    ComponentDetails();
                    GetWorkOrderDetails();
                    ComponentHeirarchyDetailsForDoneRound();

                    //bindin of this data.otherWorkOrderViewModel
                    $('#spanDoneOtherReportWorkDoneDate').text(data.otherWorkOrderViewModel.reportWorkDoneDate);
                    $('#spanDoneOtherRWDDueDate').text(data.otherWorkOrderViewModel.rwdDueDate);

                    if (data.otherWorkOrderViewModel.isCbtId) {
                        $('#spanDoneOtherBeforeCondition').text(data.otherWorkOrderViewModel.beforeCondition);
                        $('#spanDoneOtherAfterCondition').text(data.otherWorkOrderViewModel.afterCondition);
                        $('#spanDoneOtherSymptomsObserved').text(data.otherWorkOrderViewModel.symptomsObserved);
                    }
                    else {
                        $('#divDoneOtherAfterCondition').hide();
                        $('#divDoneOtherBeforeCondition').hide();
                        $('#divDoneOtherSymptomsObserved').hide();
                    }

                    if (data.otherWorkOrderViewModel.showCounterRunningHours) {
                        $('#spanDoneOtherCounterReading').text(data.otherWorkOrderViewModel.counterReading);
                    }
                    else {
                        $('#divDoneOtherCounterReading').hide();
                    }

                    if (data.otherWorkOrderViewModel.showCounterRevolutions) {
                        $('#spanDoneOtherCounterRevolutionsReading').text(data.otherWorkOrderViewModel.counterRevolutionsReading);
                    }
                    else {
                        $('#divDoneOtherCounterRevolutionsReading').hide();
                    }

                    if (data.otherWorkOrderViewModel.showCounterEvents) {
                        $('#spanDoneOtherCounterEventsReading').text(data.otherWorkOrderViewModel.counterEventsReading);
                    }
                    else {
                        $('#divDoneOtherCounterEventsReading').hide();
                    }

                    if (data.otherWorkOrderViewModel.isCommentForReason) {
                        $('#spanDoneOtherReason').text(data.otherWorkOrderViewModel.reason);
                        //$('#spanReasonForChangeInTimeLine').prop('title', data.otherWorkOrderViewModel.reason);
                        $('#spanReasonForChangeInTimeLine').prop('title', data.otherWorkOrderViewModel.reason);
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                    else {
                        $('#divDoneOtherReason').hide();
                    }

                    if (data.otherWorkOrderViewModel.showOtherSymptom) {
                        $('#spanDoneOtherOtherSymptoms').text(data.otherWorkOrderViewModel.otherSymptoms);
                    }
                    else {
                        $('#divDoneOtherOtherSymptoms').hide();
                    }

                    //Job Rank & hours
                    WorkOrderJobAndRank(data.otherWorkOrderViewModel.orderRankList, data.otherWorkOrderViewModel.totalManHours);

                    //vessel Job Description
                    SetIframeValue(data.otherWorkOrderViewModel.jobDescription, 'iframeDoneOtherJobDescription');

                    //Vessel Guide Lines
                    SetIframeValue(data.otherWorkOrderViewModel.vesselJobDescription, 'iframe');

                    //for reschedule log
                    encryptedPwhId = data.otherWorkOrderViewModel.pwhId;
                    IsCounterBased = data.otherWorkOrderViewModel.isCounterBased;

                    //hide div
                    $('#divDoneRoundVesselJobDescription').hide();
                    $('#divSparePart').hide();
                    $('#divDoneRoundStatus').hide();
                    $('#divDoneCertificateStatus').hide();
                    $('#divJobDescription').hide();
                }
            }
        }
    });
}

function SetIframeValue(text, iFrameId) {
    var html = $.parseHTML(text);
    var $iframe = $('#' + iFrameId);
    $iframe.ready(function () {
        $iframe.contents().find("body").append(html);
    });
}

function ComponentHeirarchyDetailsForDoneRound() {
    var request = {
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val(),
        "EncryptedSystemAreaId": EncryptedSystemAreaId
    };
    $.ajax({
        url: "/PlannedMaintenance/GetComponentHeirarchyDoneRound",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {
            $(".componentHeirarchy").empty();
            if (data != null) {
                var i;
                for (i = 0; i < data.length; i++) {
                    var componentName = "<span>" + data[i].componentName + "</span>";
                    $(".componentHeirarchy").append(componentName);
                }
            }
        }
    });
}

function GetCellData(label, data) {
    return '<label>' + label + '</label> <br />' + data;
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

function matchHeight() {
    $('.height-equal').matchHeight();
}
function setScrollerHeight() {
    let columnHeight = $('#leftColumn').height();
    if (screen.width < MobileScreenSize) {
        $("#divSingleJobDescription").height('200px');
    }
    else {
        let vesselGuideHeight = $("#divVesselGuideLines").height();
        $("#divSingleJobDescription").height(columnHeight - vesselGuideHeight - 113);
    }


}

function ClosePms() {
    var input = {
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };

    $.ajax({
        url: "/PlannedMaintenance/CloseWorkOrder",
        type: "POST",
        "data": {
            "input": input,
        },
        "datatype": "JSON",
        success: function (data) {
            if (data != null) {
                if (data == true) {
                    $("#pmsCloseWOSuccessModal").modal('show');
                    $('#btnCloseWOSuccessYes').off();
                    $('#btnCloseWOSuccessYes').on('click', function () {
                        $("#pmsCloseWOSuccessModal").modal('hide');
                        ApprovalSuccessYes(PlannedMaintenanceDetailsPageKey);
                    });
                }
                else {
                    ToastrAlert("validate", "Work order update failed.")
                }
            }
        }
    });
}

function ApprovalSuccessYes(keyName) {
    $.ajax({
        url: "/PlannedMaintenance/GetPmsSourceUrl",
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

function LoadProcessRescheduleWO() {
    GetWORescheduleHeaderDetail();
}

function GetWORescheduleHeaderDetail() {
    var request = {
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
        "PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
    };

    $.ajax({
        url: "/PlannedMaintenance/GetWORescheduleHeaderDetail",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        success: function (data) {
            $('#spanPRWRescheduleRequest').text(data.rescheduleStatus);
            $('#spanPRWCurrentInterval').text(data.interval);
            if (IsCounterBased) {
                $('.spanExtendBy').text('Extend By(' + data.intervalTypeShortName + ')');
                $('.spanRequestExtensionType').text('Requested Extension(' + data.intervalTypeShortName + ')');
            }
            else {
                $('.spanExtendBy').text('Extend By(Days)');
                $('.spanRequestExtensionType').text('Requested Extension(Days)');
            }


            //NormalBrush = "{StaticResource BrushOrange}"
            //GoodBrush = "{StaticResource BrushGreen}"
            //PreWarningBrush = "{StaticResource BrushPurple}"
            //CrticalBrush = "{StaticResource BrushRed}"

            var kpi = data.rescheduleStatusKPI;
            if (kpi == "Normal") {
                AddClassIfAbsent('#spanPRWRescheduleRequest', 'ShipsureBrushOrange')
            }
            else if (kpi == "Good") {
                AddClassIfAbsent('#spanPRWRescheduleRequest', 'ShipsureBrushGreen')
            }
            else if (kpi == "PreWarning") {
                AddClassIfAbsent('#spanPRWRescheduleRequest', 'ShipsureBrushPurple')
            }
            else if (kpi == "Critical") {
                AddClassIfAbsent('#spanPRWRescheduleRequest', 'ShipsureBrushRed')
            }

            processRescheduleWoUrl = data.processRescheduleWoUrl
            GetRescheduleWorkOrderForEdit(data.processRescheduleWoUrl);
            GetMappedRiskAssessmentDetail(data.processRescheduleWoUrl);
            GetProcessRescheduleWoAttachments(data.processRescheduleWoUrl);
        }
    });
}

function GetRescheduleWorkOrderForEdit(input) {
    $.ajax({
        url: "/PlannedMaintenance/GetRescheduleWorkOrderForEdit",
        type: "POST",
        "data": {
            "input": input,
        },
        "datatype": "JSON",
        success: function (data) {

            $('#spanPRWreason').text(data.workOrderReasonDescription);
            $('#spanPRWExtendBy, #spanPRWRequestedExtension').text(data.extendedBy);
            $('#txtExtendedBy').val(data.extendedBy);

            $('#spanPRWrequestedBy').text(data.requestedBy);
            var requesterTooltip = 'Requested On: ' + data.requestedOn + '<br/> Responsibility: ' + data.requesterRoleDescription;
            SetTooltip('#imgRequesterInfo', requesterTooltip);
            $('#spanPRWreasonForReschedule').text(data.reasonForReschedule);

            if (data.isRiskAssessmentMapped) {
                $("#riskAssessmentYes").prop("checked", true);
                $("#riskAssessmentYes").click();
            }
            else {
                $("#riskAssessmentNo").prop("checked", true);
                $("#riskAssessmentNo").click();
            }
            $('#txtareaRiskAssessment').val(data.riskAssessmentMappedComment);

            if (data.isJobHistoryLinked) {
                $("#currentConditionYes").prop("checked", true);
                $("#currentConditionYes").click();
            }
            else {
                $("#currentConditionNo").prop("checked", true);
                $("#currentConditionNo").click();
            }
            $('#txtareaCurrentConditions').val(data.jobHistoryLinkedComment);

            if (data.isSupportingWOCreated) {
                $("#additionalControlMeasuresYes").prop("checked", true);
                $("#additionalControlMeasuresYes").click();
            }
            else {
                $("#additionalControlMeasuresNo").prop("checked", true);
                $("#additionalControlMeasuresNo").click();
            }
            $('#txtareaAdditionalControlMeasuress').val(data.supportingWOCreatedComment);

            $("#processTypeApprove").prop("checked", true);
            GetAssociatedJobs(data.associatedJobs);
            GetSupportingJobsHistory(data.supportingJobsHistory)
            GetSupportingWorkOrders(data.supportingWorkOrders)

        }
    });
}

function GetAssociatedJobs(data) {
    $('#dtAssociatedJobs').DataTable().destroy();
    var dtAssociatedJobs = $('#dtAssociatedJobs').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "ordering": true,
        //"pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No associated jobs available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": data,
        "columns": [
            {
                className: "data-text-align",
                data: "type",
                width: "50px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            },
            {
                className: "data-date-align",
                data: "dueDate",
                width: "80px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Due Date', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "componentName",
                width: "220px",
                render: function (data, type, full, meta) { return GetCellData('Component Name', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "jobName",
                width: "280px",
                render: function (data, type, full, meta) {
                    return GetCellData('Job Name', data);
                }
            },
            {
                className: "data-text-align",
                data: "status",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetCellData('Status', data);
                }
            },
            {
                className: "data-text-align",
                data: "interval",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetCellData('Interval', data);
                }
            },
            {
                className: "data-text-align",
                data: "responsibility",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Responsibility', data);
                }
            }
        ]
    });

    $('#spanPRWassociatedJobCount').text(dtAssociatedJobs.data().count());
    if (dtAssociatedJobs.data().count() == 0) {
        AddClassIfAbsent($('#dtAssociatedJobs').parent('div'), 'text-center');
        $('#dtAssociatedJobs').parent('div').text('No records available.');
        $('#dtAssociatedJobs').hide();
    }
}

function GetSupportingJobsHistory(data) {

    $('#dtSupportingJobsHistory').DataTable().destroy();
    var dtSupportingJobsHistory = $('#dtSupportingJobsHistory').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "ordering": true,
        //"pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No job history available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": data,
        "columns": [
            {
                className: "data-text-align",
                data: "doneDate",
                width: "70px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Done Date', data); }
            },
            {
                className: "data-date-align",
                data: "closedDate",
                width: "80px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Closed Date', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "componentName",
                width: "220px",
                render: function (data, type, full, meta) { return GetCellData('Component Name', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "name",
                width: "250px",
                render: function (data, type, full, meta) { return GetCellData('Name', data); }
            },
            {
                className: "data-text-align",
                data: "type",
                width: "30px",
                render: function (data, type, full, meta) {
                    return GetCellData('Type', data);
                }
            },
            {
                className: "data-text-align",
                data: "interval",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetCellData('Interval', data);
                }
            },
            {
                className: "data-text-align",
                data: "department",
                width: "120px",
                render: function (data, type, full, meta) {
                    return GetCellData('Department', data);
                }
            },
            {
                className: "data-text-align",
                data: "responsibility",
                width: "130px",
                render: function (data, type, full, meta) {
                    return GetCellData('Responsibility', data);
                }
            }
        ]
    });

    $('#spanPRWsupportingJobsHistory').text(dtSupportingJobsHistory.data().count());
    if (dtSupportingJobsHistory.data().count() == 0) {
        AddClassIfAbsent($('#dtSupportingJobsHistory').parent('div'), 'text-center');
        $('#dtSupportingJobsHistory').parent('div').text('No records available.');
        $('#dtSupportingJobsHistory').hide();
    }
}

function GetSupportingWorkOrders(data) {
    $('#dtSupportWorkOrders').DataTable().destroy();
    var dtSupportWorkOrders = $('#dtSupportWorkOrders').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "ordering": true,
        //"pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No support orders available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": data,
        "columns": [
            {
                className: "tdblock data-text-align",
                data: "jobName",
                width: "280px",
                render: function (data, type, full, meta) { return GetCellData('Job Name', data); }
            },
            {
                className: "data-text-align",
                data: "type",
                width: "120px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            },
            {
                className: "data-date-align",
                data: "dueDate",
                width: "200px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Due Date', data); }
            },
            {
                className: "tdblock data-text-align",
                data: "responsibility",
                width: "300px",
                render: function (data, type, full, meta) {
                    return GetCellData('Responsibility', data);
                }
            }
        ]
    });

    $('#spanPRWsupportingWorkOrders').text(dtSupportWorkOrders.data().count());

    if (dtSupportWorkOrders.data().count() == 0) {
        AddClassIfAbsent($('#dtSupportWorkOrders').parent('div'), 'text-center');
        $('#dtSupportWorkOrders').parent('div').text('No records available.');
        $('#dtSupportWorkOrders').hide();
    }
}

function GetMappedRiskAssessmentDetail(input) {
    $.ajax({
        url: "/PlannedMaintenance/GetMappedRiskAssessmentDetail",
        type: "POST",
        "data": {
            "input": input,
        },
        "datatype": "JSON",
        success: function (data) {
            GetHazardsList(data.data);
        }
    });
}

function GetHazardsList(data) {
    $('#dthazards').DataTable().destroy();
    var dthazards = $('#dthazards').DataTable({
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "ordering": true,
        //"pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No data available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "data": data,
        "columns": [
            {
                className: "data-text-align d-none d-md-table-cell",
                data: "hazardNumber",
                width: "20px",
                render: function (data, type, full, meta) { return GetCellData('No.', data); }
            },
            {
                className: "tdblock data-text-align",
                "data": "riskArea",
                width: "175px",
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "tdblock data-text-align d-md-none",
                "data": "description",
                width: "175px",
                render: function (data, type, full, meta) {
                    return GetCellData('Hazards Description', full.hazardNumber + '. ' + full.description);
                }
            },
            {
                className: "data-text-align d-none d-md-table-cell",
                data: "description",
                width: "175px",
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
                    let kpiColor = RiskKPIConverter(full.riskFactorColor);
                    RiskFactorUIElement = '<span class="' + kpiColor + ' semibold">' + data + '</span>';
                    if (full.isInitialRiskVisible) {
                        RiskFactorUIElement += "<i class='fa fa-exclamation-circle ml-2 BrushOrange d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.initialRiskFactorDescription + " <br/> Further control measures added.'></i>";
                        RiskFactorUIElement += "<i class='fa fa-caret-right orange-error-triangle icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.initialRiskFactorDescription + " <br/> Further control measures added.'></i>";
                    }
                    return GetCellData('Risk Factor', RiskFactorUIElement);
                }
            }
        ],
        "initComplete": function () {
            $('#spanPRWhazardsCount').text(this.api().data().length)
        }
    });

    if (dthazards.data().count() == 0) {
        AddClassIfAbsent($('#dthazards').parent('div'), 'text-center');
        $('#dthazards').parent('div').text('No records available.');
        $('#dthazards').hide();
    }

}

function RiskKPIConverter(kpi) {
    if (kpi == 'Good') {
        return 'ShipsureBrushGreen';
    }
    else if (kpi == 'Warning') {
        return 'ShipsureBrushOrange';
    }
    else if (kpi == 'Critical') {
        return 'ShipsureBrushRed';
    }
}

function GetProcessRescheduleWoAttachments(input) {

	$('#dtPWAattachments').DataTable().destroy();
	var dtPWAattachments = $('#dtPWAattachments').DataTable({
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"order": [],
		"language": {
			"emptyTable": "No attachment available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/PlannedMaintenance/GetProcessRescheduleWoAttachments",
			"type": "POST",
			"data":
			{
				"input": input
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align pr-0 processresattach",
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
				className: "width-85 mt-0 data-text-align",
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
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Created On', data); }
            },
            {
                className: "data-text-align tdblock",
                data: "description",
                width: "400px",
                render: function (data, type, full, meta) { return GetCellData('Description', data); }
            }
        ],
        "initComplete": function () {
            $('#spanPRWattachmentsCount').text(this.api().data().length)
            if (this.api().data().length == 0) {
                AddClassIfAbsent($('#dtPWAattachments').parent('div'), 'text-center');
                $('#dtPWAattachments').parent('div').text('No records available.');
                $('#dtPWAattachments').hide();
            }
        }
    });

    $('#dtPWAattachments tbody').on('click', 'a.documentDownload', function () {
        var data = dtPWAattachments.row($(this).parents('tr')).data();
        DownloadSelectedAttachment(data);
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
        url: "/Common/DownloadDocument",
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
                ToastrAlert("validate", "File not found for \"" + fileName + "\"");
            }
        }
    });
}

function ValidationBeforeProcessWO(inputrequest) {

    let approvedValidated = ValidateApprovedExtendedBy(inputrequest.ApprovedExtendedBy);
    if (approvedValidated.IsSuccess) {
        $('#txtExtendedBy').css({ "border-color": '' });
    }
    else {
        $('#txtExtendedBy').css({ "border-color": 'red' });
        ToastrAlert("validate", approvedValidated.Message);
    }

    let commentValidated = ValidateComment(inputrequest);
    if (commentValidated.IsSuccess) {
        $('#txtareaApproverComments').css({ "border-color": '' });
    }
    else {
        $('#txtareaApproverComments').css({ "border-color": 'red' });
        ToastrAlert("validate", commentValidated.Message);
    }

    let riskAssessmentValidated = ValidateRiskAssessment(inputrequest);
    if (riskAssessmentValidated.IsSuccess) {
        $('#txtareaRiskAssessment').css({ "border-color": '' });
    }
    else {
        $('#txtareaRiskAssessment').css({ "border-color": 'red' });
        ToastrAlert("validate", riskAssessmentValidated.Message);
    }

    let currentConditionsValidated = ValidateCurrentConditions(inputrequest);
    if (currentConditionsValidated.IsSuccess) {
        $('#txtareaCurrentConditions').css({ "border-color": '' });
    }
    else {
        $('#txtareaCurrentConditions').css({ "border-color": 'red' });
        ToastrAlert("validate", currentConditionsValidated.Message);
    }

    let additionalControlMeasuressValidated = ValidateAdditionalControlMeasures(inputrequest);
    if (additionalControlMeasuressValidated.IsSuccess) {
        $('#txtareaAdditionalControlMeasuress').css({ "border-color": '' });
    }
    else {
        $('#txtareaAdditionalControlMeasuress').css({ "border-color": 'red' });
        ToastrAlert("validate", additionalControlMeasuressValidated.Message);
    }

    return approvedValidated.IsSuccess && commentValidated.IsSuccess && riskAssessmentValidated.IsSuccess && currentConditionsValidated.IsSuccess && additionalControlMeasuressValidated.IsSuccess;
}

function ValidateApprovedExtendedBy(approvedExtendedBy) {
    let response = {
        "IsSuccess": true,
        "Message": ""
    }

    if (approvedExtendedBy == null) {
        response.IsSuccess = false;
        response.Message = "Extend by value cannot be blank.";
        return response;
    }
    else if (approvedExtendedBy == 0) {
        response.IsSuccess = false;
        response.Message = "Extend by value should be greater than zero.";
        return response;
    }
    else if (IsCounterBased && approvedExtendedBy > maximumCounterExtensionValue) {
        response.IsSuccess = false;
        response.Message = "You cannot extend by more than " + maxCounterExtensionPercentage + "% of current interval.";
        return response;
    }
    else if (!IsCounterBased && approvedExtendedBy > maximumIntervalDays) {
        response.IsSuccess = false;
        response.Message = "Extended days cannot be greater than " + maximumIntervalDays + " (days)."
        return response;
    }
    return response
}

function ValidateComment(inputRequest) {
    let response = {
        "IsSuccess": true,
        "Message": ""
    }

    if (IsNullOrEmptyOrUndefinedLooseTyped(inputRequest.Comment)) {
        response.IsSuccess = false;
        if (inputRequest.IsRevise) {
            response.Message = "Revise comments" + CustomizeCannotBeBlank;
        }
        else {
            response.Message = "Approver comments" + CustomizeCannotBeBlank;
        }
        return response;
    }

    if (inputRequest.Comment.trim().length < 4) {
        response.IsSuccess = false;
        if (inputRequest.IsRevise) {
            response.Message = "Revise comments" + CustomizeAtleast4Characters;
        }
        else {
            response.Message = "Approver comments" + CustomizeAtleast4Characters;
        }
        return response;
    }
    return response;
}

function ValidateRiskAssessment(inputRequest) {
    let response = {
        "IsSuccess": true,
        "Message": ""
    }

    if (!inputRequest.IsRiskAssessmentMapped) {
        if (IsNullOrEmptyOrUndefinedLooseTyped(inputRequest.RiskAssessmentMappedComment)) {
            response.IsSuccess = false;
            response.Message = "Risk Assessment comments" + CustomizeCannotBeBlank;
            return response;
        }

        if (inputRequest.RiskAssessmentMappedComment.trim().length < 4) {
            response.IsSuccess = false;
            response.Message = "Risk Assessment comments" + CustomizeAtleast4Characters;
            return response;
        }
    } 
    return response;
}

function TriggerRiskAssessmentValidation() {
    let IsRiskAssessmentMapped = $('input[name="riskAssessment"]:checked').val() == "Yes" ? true : false;
    let RiskAssessmentMappedComment = $('#txtareaRiskAssessment').val().trim();

    let inputRequest = {
        "IsRiskAssessmentMapped": IsRiskAssessmentMapped,
        "RiskAssessmentMappedComment": RiskAssessmentMappedComment,
    }

    if (isBtnProcessWoClicked) {
        let riskAssessmentValidated = ValidateRiskAssessment(inputRequest);
        if (riskAssessmentValidated.IsSuccess) {
            $('#txtareaRiskAssessment').css({ "border-color": '' });
        }
        else {
            $('#txtareaRiskAssessment').css({ "border-color": 'red' });
        }
    }
}

function ValidateCurrentConditions(inputRequest) {
    let response = {
        "IsSuccess": true,
        "Message": ""
    }

    if (!inputRequest.IsJobHistoryLinked) {
        if (IsNullOrEmptyOrUndefinedLooseTyped(inputRequest.JobHistoryLinkedComment)) {
            response.IsSuccess = false;
            response.Message = "Current Conditions comments" + CustomizeCannotBeBlank;
            return response;
        }

        if (inputRequest.JobHistoryLinkedComment.trim().length < 4) {
            response.IsSuccess = false;
            response.Message = "Current Conditions comments" + CustomizeAtleast4Characters;
            return response;
        }
    } 
    return response;
}

function TriggerCurrentConditionsValidation() {
    let IsJobHistoryLinked = $('input[name="currentCondition"]:checked').val() == "Yes" ? true : false;
    let JobHistoryLinkedComment = $('#txtareaCurrentConditions').val().trim();

    let inputRequest = {
        "IsJobHistoryLinked": IsJobHistoryLinked,
        "JobHistoryLinkedComment": JobHistoryLinkedComment,
    }

    if (isBtnProcessWoClicked) {
        let currentConditionsValidated = ValidateCurrentConditions(inputRequest);
        if (currentConditionsValidated.IsSuccess) {
            $('#txtareaCurrentConditions').css({ "border-color": '' });
        }
        else {
            $('#txtareaCurrentConditions').css({ "border-color": 'red' });
        }
    }
}

function ValidateAdditionalControlMeasures(inputRequest) {
    let response = {
        "IsSuccess": true,
        "Message": ""
    }

    if (!inputRequest.IsSupportingWOCreated) {
        if (IsNullOrEmptyOrUndefinedLooseTyped(inputRequest.SupportingWOCreatedComment)) {
            response.IsSuccess = false;
            response.Message = "Additional Control Measures comments" + CustomizeCannotBeBlank;
        }

        if (inputRequest.SupportingWOCreatedComment.trim().length < 4) {
            response.IsSuccess = false;
            response.Message = "Additional Control Measures comments" + CustomizeAtleast4Characters;
        }
    } 
    return response
}

function TriggerAdditionalControlMeasureValidation() {
    let IsSupportingWOCreated = $('input[name="additionalControlMeasures"]:checked').val() == "Yes" ? true : false;
    let SupportingWOCreatedComment = $('#txtareaAdditionalControlMeasuress').val().trim();

    let inputRequest = {
        "IsSupportingWOCreated": IsSupportingWOCreated,
        "SupportingWOCreatedComment": SupportingWOCreatedComment,
    }

    if (isBtnProcessWoClicked) {
        let additionalControlMeasuressValidated = ValidateAdditionalControlMeasures(inputRequest);
        if (additionalControlMeasuressValidated.IsSuccess) {
            $('#txtareaAdditionalControlMeasuress').css({ "border-color": '' });
        }
        else {
            $('#txtareaAdditionalControlMeasuress').css({ "border-color": 'red' });
        }
    }
}

function ProcessWO(inputrequest) {
    $.ajax({
        url: "/PlannedMaintenance/ProcessRescheduleRequest",
        type: "POST",
        "data": {
            "input": processRescheduleWoUrl,
            "inputRequest": inputrequest,
        },
        "datatype": "JSON",
        success: function (data) {
            $('#resceduledrequest').modal('hide');

            if (data != null && data == true) {

                if (inputrequest.IsApprove) {
                    $('#h1ProcessReschduleWoMessage').text('Reschedule request approved successfully.');
                }
                else if (inputrequest.IsRevise) {
                    $('#h1ProcessReschduleWoMessage').text('Reschedule request revised successfully.');
                }
                else if (inputrequest.IsReject) {
                    $('#h1ProcessReschduleWoMessage').text('Reschedule request rejected successfully.');
                }
                else {
                    $('#h1ProcessReschduleWoMessage').text('Reschedule request processed successfully.');
                }

                $("#pmsProcessRescheduleSuccessModal").modal('show');
                $('#btnProcessRescheduleWOSuccessOK').off();
                $('#btnProcessRescheduleWOSuccessOK').on('click', function () {

                    $("#pmsProcessRescheduleSuccessModal").modal('hide');
                    ProcessRescheduleWoSuccess(PlannedMaintenanceDetailsPageKey);
                });
            }
            else {
                ToastrAlert("validate", "Work order update failed.")
            }
        }
    });
}

function ProcessRescheduleWoSuccess(keyName) {
    $.ajax({
        url: "/PlannedMaintenance/GetProcessRescheduleRequestSourceUrl",
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

