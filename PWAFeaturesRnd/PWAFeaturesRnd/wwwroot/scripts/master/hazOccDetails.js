import toastr from "toastr";
import moment from "moment";
var tabs = { summary: 1, Events: 2, Finding: 3, Action: 4,HierarchyExplorer: 5};

require('bootstrap');
import { GetCellData, GetFormattedDateTimeAMPMFormat, GetFormattedDate, GetExportData } from "../common/datatablefunctions.js";
import { AjaxError, GetCookie, AddLoadingIndicator, RemoveLoadingIndicator, headerReadMore, SetHeaderMargin, ToastrAlert, BackButton, AddClassIfAbsent, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, RegisterTabSelectionEvent, RemoveClassIfPresent } from "../common/utilities.js";
import { HazOccDetailsPage, MobileScreenSize } from "../common/constants.js"
import { RecordLevelNote } from "../common/notesUtilities.js"
var dataLoad = new Set();

var colorMap = new Map();
colorMap.set(0, { color: "badge-grey" });
colorMap.set(1, { color: "badge-green" });
colorMap.set(5, { color: "badge-amber-color" });
colorMap.set(6, { color: "badge-orange" });

var statusColorMap = new Map();
statusColorMap.set(0, { textColor: "text-yellow" });
statusColorMap.set(1, { textColor: "txt-orange" });
statusColorMap.set(2, { textColor: "text-green" });
statusColorMap.set(3, { textColor: "" });
statusColorMap.set(4, { textColor: "" });
statusColorMap.set(5, { textColor: "text-purple" });
statusColorMap.set(6, { textColor: "text-grey" });
statusColorMap.set(7, { textColor: "text-grey" });
statusColorMap.set(8, { textColor: "" });

const No = 'No';
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

$(document).ready(function () {
    AddLoadingIndicator();
    RemoveLoadingIndicator();
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
    //Sidebar back
    BackButton(HazOccDetailsPage, false)

    if (screen.width < 760) {
        headerReadMore('headershowmorewrapper', 'header');
    }
    SetHeaderMargin();

    fnSummaryTab();
    $("#insuranceclaimsdiv").hide();

    $(".dropdown-tab-1").click(function () {
        if (!dataLoad.has(tabs.summary)) {
            fnSummaryTab();
        }
    });

    $(".dropdown-tab-2").click(function () {
        if (!dataLoad.has(tabs.Events)) {
            if ($('#IsAccident').val().toLowerCase() == "true") {
                GetHazOccAccidentEvent();
            }
            else if ($('#IsIncident').val().toLowerCase() == "true") {
                GetHazOccIncidentEvent();
            }
            else if ($('#IsNearMiss').val().toLowerCase() == "true") {
                GetHazOccNearMissEvent();
            }
            else if ($('#IsPassengerIllness').val().toLowerCase() == "true") {
                GetHazOccPassengerDetails();
            }
            else if ($('#IsCrewIllness').val().toLowerCase() == "true") {
                GetHazOccCrewIllnessDetail();
            }
            else if ($('#IsThirdPartyIllness').val().toLowerCase() == "true") {
                GetHazOccThirdPartyIllnessDetail();
            }

            dataLoad.add(tabs.Events);
        }
    });

    $(".dropdown-tab-3").click(function () {
        if (!dataLoad.has(tabs.Finding)) {
            if ($('#IsAccident').val().toLowerCase() == "true" || $('#IsIncident').val().toLowerCase() == "true" || $('#IsNearMiss').val().toLowerCase() == "true") {
                GetHazOccShipFinding();
                GetHazOccInvestigationFinding();
                GetHazOccDirectCauses();
                GetHazOccRootCauses();
            }
            else if ($('#IsPassengerIllness').val().toLowerCase() == "true") {
                GetHazOccPassengerTreatmentDetails();
            }
            else if ($('#IsCrewIllness').val().toLowerCase() == "true") {
                GetHazOccCrewTreatmentDetail();
            }
        }
        dataLoad.add(tabs.Finding);
    });

    $(".dropdown-tab-4").click(function () {
        if (!dataLoad.has(tabs.Action)) {
            if ($('#IsAccident').val().toLowerCase() == "true" || $('#IsIncident').val().toLowerCase() == "true" || $('#IsNearMiss').val().toLowerCase() == "true") {
                BindRescheduleHistory();
                GetHazOccCausation();
            }
            else if ($('#IsPassengerIllness').val().toLowerCase() == "true") {
                GetHazOccPassengerDoctorsReport();
            }
            else if ($('#IsCrewIllness').val().toLowerCase() == "true") {
                GetHazOccCrewDoctorsReport();
                }
            }
            dataLoad.add(tabs.Action);
    });

    RegisterTabSelectionEvent('.mobileTabClick', HazOccDetailsPage);
    var MobilTabCls = $("#ActiveMobileTabClass").val();
    if (($('#IsObservation').val().toLowerCase() == "false") || ($('#IsIllness').val().toLowerCase() == "true")) {
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
    }

    if (statusColorMap.has(Number($("#StatusKPI").val()))) {
        console.log("color", statusColorMap.get(Number($("#StatusKPI").val())).textColor);
        var StatusColor = statusColorMap.get(Number($("#StatusKPI").val())).textColor;
        $("#spanHeadStatus").addClass(StatusColor);
    }

    $("#aHierarchyExplorerMapping").click(function () {
        if (!dataLoad.has(tabs.HierarchyExplorer)) {
            GetHierarchyExplorerMapping();
        }
        dataLoad.add(tabs.HierarchyExplorer);
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);

    GetDiscussionNotesCount(messageDetailsJSON);

    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);

    $('#insuranceClaims').click(function () {
        $("#defectsdetailsdiv").hide();
        $("#insuranceclaimsdiv").show();
        $("#defectDetails").removeClass("active");
        $("#insuranceClaims").addClass("active");
    });
    $('#defectDetails').click(function () {
        $("#defectsdetailsdiv").show();
        $("#insuranceclaimsdiv").hide();
        $("#defectDetails").addClass("active");
        $("#insuranceClaims").removeClass("active");
    });
});

function fnSummaryTab() {
    if ($('#IsAccident').val().toLowerCase() == "true") {
        GetHazOccAccidentSummary();
        BindDefectDetils();
        BindInsuranceClaimsList();
        dataLoad.add(tabs.summary);
    }
    else if ($('#IsIncident').val().toLowerCase() == "true") {
        GetHazOccIncidentSummary();
        BindDefectDetils();
        BindInsuranceClaimsList();
        dataLoad.add(tabs.summary);
    }
    else if ($('#IsObservation').val().toLowerCase() == "true") {
        GetHazOccObservationSummary();
        BindDefectDetils();
        BindInsuranceClaimsList();
        dataLoad.add(tabs.summary);
    }
    else if ($('#IsNearMiss').val().toLowerCase() == "true") {
        GetNearMissSummary();
        BindDefectDetils();
        BindInsuranceClaimsList();
        dataLoad.add(tabs.summary);
    }
    else if ($('#IsIllness').val().toLowerCase() == "true") {
        GetIllnessSummary();
        dataLoad.add(tabs.summary);
    }

}

function BindDefectDetils() {

    var defectdetails = $('#dtdefectdetails').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 col-lg-8 col-xl-8 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": false,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No defect details available.",
        },
        "ajax": {
            "url": "/HazOcc/GetHazOccDefectDetails",
            "type": "POST",
            "data": {
                "encryptedIncidentId": $("#EncryptedIdentifier").val(),
                "encryptedVeselId": $("#EncryptedVesselId").val()
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "tdblock td-row-header font-weight-600 data-text-align",
                "data": "defectNo",
                "width": "80px",
                render: function (data, type, full, meta) { return GetExportData(data); }
            },
            {
                className: "data-text-align tdblock",
                "data": "defectTitle",
                "width": "150px",
                render: function (data, type, full, meta) { return GetCellData('Title', data); }

            },
            {
                className: "data-datetime-align",
                "data": "currentDueDate",
                "width": "100px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Current <br/> Due Date', data); }
            },
            {
                className: "data-datetime-align",
                "data": "datecompleted",
                "width": "100px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Date <br/> Completed', data); }
            },
            {
                className: "data-text-align",
                "data": "plannedFor",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Planned For', data); }
            },
            {
                className: "data-text-align",
                "data": "requisitionCount",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Requisitions', data); }
            },
            {
                className: "data-text-align",
                "data": "defectStatus",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Status', data); }
            },
        ],
        "initComplete": function () {
            $('#spanDefectDetailsCount').text(this.api().data().length);
        }
    });
}

function GetHazOccAccidentSummary() {
    var request = {
        "EncryptedIncidentId": $("#EncryptedIdentifier").val(),
        "VesselType": $("#VesselType").val(),
        "CategoryId": $("#CategoryId").val()
    }

    $.ajax({
        url: "/HazOcc/GetHazOccAccidentSummary",
        type: "POST",
        "data": request,
        "datatype": "JSON",
        success: function (data) {

            $("#hdnParentReportId").val(data.parentReportId);
            $("#spnReportDate").text(getValueOrBlank(data.reportDate));
            $("#spanAccidentDateTime").text(getValueOrBlank(data.accidentDateTime));

            if (data.closedDate != null) {
                $("#spanClosedDate").text(getValueOrBlank(data.closedDate));
            }
            else {
                $("#isVisibleClosedDate").hide();
            }

            $("#spanSafetyOfficer").text(getValueOrBlank(data.safetyOfficer));
            $("#spanClassification").text(getValueOrBlank(data.classification));
            $("#spanInvestigationDate").text(getValueOrBlank(data.investigateDate));
            $("#spanInvestigatorName").text(getValueOrBlank(data.investigateName));
            $("#spanInvestigatorRank").text(getValueOrBlank(data.investigateRankName));

            $("#spanLastChanged").text(getValueOrBlank(data.updatedDate));
            $("#spanCreatedBy").text(getValueOrBlank(data.createdBy));
            $("#spanParentReport").text(getValueOrBlank(data.parentReport));
            $("#spanMaster").text(getValueOrBlank(data.masterName));

            $("#spanActualSeverity").text(getValueOrBlank(data.actualSeverity));
            $("#spanLogBookEntryDate").text(getValueOrBlank(data.logBookDate));
            $("#spanWorkRelated").text(getValueOrBlank(data.isAccidentWorkRelated));

            if (data.isPassengerAccident == true) {
                $("#spanCruiseNo").text(getValueOrBlank(data.cruiseNo));
                $("#spanPaxNo").text(getValueOrBlank(data.paxNumber));
                $(".isPassengerAccidentVisible").show();
            }
            else {
                $(".isPassengerAccidentVisible").hide();
            }

            $("#spanVesselLocation").text(getValueOrBlank(data.vesselLocation));
            if (data.showManeuvering == true) {
                $("#spanManeuvering").text(getValueOrBlank(data.manoeuvring));
            } else {
                $(".isManoeuvringVisible ").hide();
            }

            if (data.showPort == true) {
                $("#spanPort").text(getValueOrBlank(data.portName));
                $("#spanDockName").text(getValueOrBlank(data.dockName));
            }
            else {
                $(".isPortVisible").hide();
            }

            $("#spanWhere").text(getValueOrBlank(data.where));
            $("#divShipLocationLabel").text(getValueOrBlank(data.shipLocationLabel));
            $("#spanLocationOnboard").text(getValueOrBlank(data.shipLocation));
            $("#spanLight").text(getValueOrBlank(data.light));

            $("#spanDescription").html(lntobr(data.description));

            if (data.isClosureCommentVisible == true) {
                $("#spanClosureComment").html(lntobr(data.closureComments));
            }
            else {
                $(".isClosureCommentVisible").hide();
            }

            if (data.isFleetManagerCommentsVisible == true) {
                $("#spanFleetManagerComments").html(lntobr(data.fleetManagerComments));
            }
            else {
                $(".isFleetManagerCommentsVisible").hide();
            }

            if (data.isFleetManagerCommentsVisible == true) {
                $("#spanHSEQManagerComments").html(lntobr(data.msqComments));
            }
            else {
                $(".isHSEQManagerCommentsVisible").hide();
            }

            if (data.showLongLat == true) {
                $("#spanLatitude").text(getValueOrBlank(data.latitudeDegrees));
                $("#spanLongitude").text(getValueOrBlank(data.longDegrees));
            }
            else {
                $(".isLongLatVisible").hide();
            }

            if (data.locationLookupLabel != null && data.locationLookupLabel != "") {
                $("#divLocationLookupLabel").text(getValueOrBlank(data.locationLookupLabel));
                $("#spanLocationName").text(getValueOrBlank(data.locationName));
            }
            else {
                $(".isLocationLookupLabelVisible").hide();
            }

            if (data.showShipsOpsRelated == true) {
                $("#spanAccidentShipOperation").text(getValueOrBlank(data.showShipsOpsRelated));
            }
            else {
                $(".isShowShipsOpsRelatedVisible").hide();
            }

            if (data.showOtherShipLocation == true) {
                $("#spanSpecify").text(getValueOrBlank(data.otherShipLocation));
                $(".isShowOtherShipLocationVisible").show();
            }
            else {
                $(".isShowOtherShipLocationVisible").hide();
            }
        },
        complete: function () {

        }
    });
}

function GetHazOccIncidentSummary() {

    var request = {
        "EncryptedIncidentId": $("#EncryptedIdentifier").val(),
        "VesselType": $("#VesselType").val(),
        "CategoryId": $("#CategoryId").val()
    }

    $.ajax({
        url: "/HazOcc/GetHazOccIncidentSummary",
        type: "POST",
        "data": request,
        "datatype": "JSON",
        success: function (data) {
            $("#hdnParentReportId").val(data.parentReportId);

            $("#spnReportDate").text(getValueOrBlank(data.reportDate));
            $("#spanIncidentDateTime").text(getValueOrBlank(data.incidentDateTime));

            if (data.closedDate != null) {
                $("#spanClosedDate").text(getValueOrBlank(data.closedDate));
            }
            else {
                $("#isVisibleClosedDate").hide();
            }

            $("#spanSafetyOfficer").text(getValueOrBlank(data.safetyOfficer));
            $("#spanClassification").text(getValueOrBlank(data.classification));
            $("#spanLastChanged").text(getValueOrBlank(data.updatedDate));
            $("#spanCreatedBy").text(getValueOrBlank(data.createdBy));
            $("#spanParentReport").text(getValueOrBlank(data.parentReport));
            $("#spanMaster").text(getValueOrBlank(data.masterName));
            $("#spanActualSeverity").text(getValueOrBlank(data.actualSeverity));
            $("#spanEquipmentCategory").text(getValueOrBlank(data.equipmentType));
            $("#spanEquipmentFailureCausingIncident").text('-');
            $("#spanVesselLocation").text(getValueOrBlank(data.vesselLocation));

            if (data.showManeuvering == true) {
                $("#spanManeuvering").text(getValueOrBlank(data.manoeuvring));
            }
            else {
                $(".isManoeuvringVisible ").hide();
            }

            if (data.showPort == true) {
                $("#spanPort").text(getValueOrBlank(data.portName));
                $("#spanDockName").text(getValueOrBlank(data.dockName));
            }
            else {
                $(".isPortVisible").hide();
            }

            $("#spanWhere").text(getValueOrBlank(data.where));
            $("#divShipLocationLabel").text(getValueOrBlank(data.shipLocationLabel));
            $("#spanLocationOnboard").text(getValueOrBlank(data.shipLocation));
            $("#spanLight").text(getValueOrBlank(data.light));
            $("#spanDescription").html(lntobr(data.description));

            if (data.showLongLat == true) {
                $("#spanLatitude").text(getValueOrBlank(data.latitudeDegrees));
                $("#spanLongitude").text(getValueOrBlank(data.longDegrees));
            }
            else {
                $(".isLongLatVisible").hide();
            }

            if (data.locationLookupLabel != null && data.locationLookupLabel != "") {
                $("#divLocationLookupLabel").text(getValueOrBlank(data.locationLookupLabel));
                $("#spanLocationName").text(getValueOrBlank(data.locationName));
            }
            else {
                $(".isLocationLookupLabelVisible").hide();
            }

            if (data.showShipsOpsRelated == true) {
                $("#spanAccidentShipOperation").text(getValueOrBlank(data.showShipsOpsRelated));
            }
            else {
                $(".isShowShipsOpsRelatedVisible").hide();
            }

            if (data.showOtherShipLocation == true) {
                $("#spanSpecify").text(getValueOrBlank(data.otherShipLocation));
                $(".isShowOtherShipLocationVisible").show();
            }
            else {
                $(".isShowOtherShipLocationVisible").hide();
            }

            if (data.showCollisionStationary == true) {
                $("#spanCollisionStationary").text(getValueOrBlank(data.collisionStationary));
            }
            else {
                $(".showCollisionStationary").hide();
            }

            $("#spanEquipmentFailure").text(getValueOrBlank(data.isEquipmentFailure));
            $("#spanDescription").html(lntobr(data.description));

            if (data.isClosureCommentVisible == true) {
                $("#spanClosureComment").html(lntobr(data.closureComments));
            }
            else {
                $(".isClosureCommentVisible").hide();
            }

            if (data.isFleetManagerCommentsVisible == true) {
                $("#spanFleetManagerComments").html(lntobr(data.fleetManagerComments));
            }
            else {
                $(".isFleetManagerCommentsVisible").hide();
            }

            if (data.isFleetManagerCommentsVisible == true) {
                $("#spanHSEQManagerComments").html(lntobr(data.msqComments));
            }
            else {
                $(".isHSEQManagerCommentsVisible").hide();
            }

            var classificationTooltip = data.classificationDescription;
            $('#classificationTitle').prop('title', classificationTooltip);
            $('[data-toggle="tooltip"]').tooltip();
        },
      
        complete: function () {

        }
    });
}

function GetHazOccObservationSummary() {

    var request = {
        "EncryptedIncidentId": $("#EncryptedIdentifier").val(),
        "VesselType": $("#VesselType").val(),
        "CategoryId": $("#CategoryId").val()
    }

    $.ajax({
        url: "/HazOcc/GetHazOccObservationSummary",
        type: "POST",
        "data": request,
        "datatype": "JSON",
        success: function (data) {
            $("#hdnParentReportId").val(data.parentReportId);
            $("#spnReportDate").text(getValueOrBlank(data.reportDate));
            $("#spanObservationDateTime").text(getValueOrBlank(data.observationDateTime));

            if (data.closedDate != null) {
                $("#spanClosedDate").text(getValueOrBlank(data.closedDate));
            }
            else {
                $("#isVisibleClosedDate").hide();
            }
            $("#spanSafetyOfficer").text(getValueOrBlank(data.safetyOfficer));
            $("#spanClassification").text(getValueOrBlank(data.classification));
            $("#spanShipOperations").text(getValueOrBlank(data.shipOperation));
            $("#spanLastChanged").text(getValueOrBlank(data.updatedDate));
            $("#spanCreatedBy").text(getValueOrBlank(data.createdBy));
            $("#spanMaster").text(getValueOrBlank(data.masterName));
            $("#spanObservationRaisedBy").text(getValueOrBlank(data.observationRaisedByName));
            $("#spanReporterRank").text(getValueOrBlank(data.rank));
            $("#spanPotentialConsequences").text(getValueOrBlank(data.possibleConsequence));
            $("#spanImmediateActionTaken").html(lntobr(data.immediateActionTaken));
            $("#spanDescription").html(lntobr(data.description));
            $("#spanComments").html(lntobr(data.comments));

            if (data.isClosureCommentVisible == true) {
                $("#spanClosureComment").html(lntobr(data.closureComments));
            } else {
                $(".isClosureCommentVisible").hide();
            }

            if (data.isUnsafeObs == true) {
                $("#spanWasWorkStopped").text(getValueOrBlank(data.hasWorkStopped));
                if (data.hasWorkStopped === No) {
                    AddClassIfAbsent("#spanWasWorkStopped", 'text-red');
                }
                $("#spanPotentialSeverity").text(getValueOrBlank(data.potentialSeverity));
                $("#isVisibleParentReport").hide();
            } else {
                $(".isUnsafeObservation").hide();
                $("#spanParentReport").text(getValueOrBlank(data.parentReport));
            }

            $("#divActsLabel").text(getValueOrBlank(data.classification));
            $("#spanSafeAct").text(getValueOrBlank(data.actsLabelValue));
        },
        complete: function () {
            if (screen.width > 767) {
                var total = $("#observationLeftcolumn").height();

                $("#rightcolumnDescription").css({ height: (Math.round(total / 3) + 12) + "px" });
                $("#rightcolumnClosureComments").css({ height: (Math.round(total / 3) - 20) + "px" });
                $("#rightcolumnComments").css({ height: (Math.round(total / 3) - 20) + "px" });
            }
        }
    });
}

function GetHazOccAccidentEvent() {
    $.ajax({
        url: "/HazOcc/GetHazOccAccidentEventDetails",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "categoryId": $("#CategoryId").val()
        },
        "datatype": "JSON",
        success: function (data) {
            $("#spanDutyStatus").text(getValueOrBlank(data.dutyStatus));
            $("#spanDepartment").text(getValueOrBlank(data.department));
            $("#spanTypeInjury").text(getValueOrBlank(data.typeInjury));
            $("#spanTypeAccident").text(getValueOrBlank(data.typeAccident));
            $("#spanBodyAreasAffected").text(getValueOrBlank(data.bodyAreasAffected));
            $("#divImmediateActionTaken").html(lntobr(data.immediateActionTaken));
            $("#spanDateOfAccident").text(getValueOrBlank(data.accidentDate));
            $("#spanDateReported").text(getValueOrBlank(data.reportedDate));
            $("#spanAccidentReportedTo").text(getValueOrBlank(data.reportedToName));
            $("#spanAreaSupervisor").text(getValueOrBlank(data.areaSupervisorName));
            $("#spanAreaSupervisorRank").text(getValueOrBlank(data.areaSupervisorRankName));
            $("#divComment").html(lntobr(data.comments));
            $("#spanWorkHoursBeforeAccident").text(getValueOrBlank(data.workHours));
            $("#spanRestHoursBeforeAccident").text(getValueOrBlank(data.restHours));
            $("#spanDaysOnboardShip").text(getValueOrBlank(data.daysOnboardShip));
            $("#spanTimeWithCompany").text(getValueOrBlank(data.crewTimeWithCompany));
            $("#spanTimeRank").text(getValueOrBlank(data.crewtimeInRank));
            $("#PPEWorn").text(getValueOrBlank(data.ppeWornDescription));
            $("#spanNormallySpectacles").text(getValueOrBlank(data.hasSpectacle));
            $("#spanInjuredPersonsCompanyName").text(getValueOrBlank(data.timeWithCompany));
            $("#spanThirdPartyDaysOnboardShip").text(getValueOrBlank(data.daysOnboardShip));
            $("#spanThirdPartyPPEWorn").text(getValueOrBlank(data.ppeWornDescription));
            $("#spanOccupation").text(getValueOrBlank(data.occupation));
            $("#spanThirdPartyNormallySpectacles").text(getValueOrBlank(data.hasSpectacle));
            $("#spanThirdPartyWereSpectaclesWorn").text(getValueOrBlank(data.spectaclesWorn));
            $("#spanWereSpectaclesWorn").text(getValueOrBlank(data.spectaclesWorn));

            console.log("DATA", data);

            if (data.isCrewOrThirtyPrtyAccident == true) {
                $("#isVisibleDutyStatus").show();
            }
            else {
                $("#isVisibleDutyStatus").hide();
            }

            if (data.isCrewAccident == true) {
                $("#isVisibleDepartment").show();
                $("#isVisibleCrewInjuredPerson").show();
            }
            else {
                $("#isVisibleDepartment").hide();
                $("#isVisibleCrewInjuredPerson").hide();
            }

            if (data.hasSpectacle.toLowerCase() == "yes") {
                $("#isVisibleCrewSpectacle").show();
                $("#isVisibleThirdPartySpectacle").show();
            }
            else {
                $("#isVisibleCrewSpectacle").hide();
                $("#isVisibleThirdPartySpectacle").hide();
            }

            if (data.isThirdPartyAccident == true) {
                $("#isVisibleThirdPartyInjuredPerson").show();
            }
            else {
                $("#isVisibleThirdPartyInjuredPerson").hide();
            }
        },
        complete: function () {
            if (screen.width > 767) {
                $('.event-height-equal').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccIncidentEvent() {
    $.ajax({
        url: "/HazOcc/GetHazOccIncidentEventDetails",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
        },
        "datatype": "JSON",
        success: function (data) {
            $("#spanShipOperations").text(getValueOrBlank(data.shipOptions));
            $("#spanDamageVesselEquip").text(getValueOrBlank(data.damageToVesselOrEquip));
            $("#spanDrugsAlcohalFactor").text(getValueOrBlank(data.isDrugOrAlcoholFactor));
            $("#spanProtestIssued").text(getValueOrBlank(data.isProtestNoteIssued));
            $("#spanPollutionCategoryDeck").text(getValueOrBlank(data.polutionCategoryDeck));
            $("#spanSpillsContainedDeck").text(getValueOrBlank(data.oilLtrsDeck));
            $("#spanSpillsContainedDeckOtherDetails").html(lntobr(data.deckDetails));
            $("#spanPollutionCtegorySea").text(getValueOrBlank(data.polutionCategorySea));
            $("#spanSpillsSea").text(getValueOrBlank(data.oilLtrsSea));
            $("#spanSpillsSeaOtherDetails").html(lntobr(data.seaDetails));
            $("#spanDamage3rdParty").text(getValueOrBlank(data.damageToThirdParty));
            $("#spanDamage3rdPartOtherDetails").html(lntobr(data.damageToThirdPartyDesc));
            $("#divImmediateActionTaken").html(lntobr(data.immediateActionTaken));
            $("#divDamageDetails").html(lntobr(data.damageDescription));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.event-height-equal').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccShipFinding() {
    $.ajax({
        url: "/HazOcc/GetHazOccShipFinding",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "encryptedVesselId": $("#EncryptedVesselId").val()
        },
        "datatype": "JSON",
        success: function (data) {
            $("#divShipsFindingAnalysis").html(lntobr(data.analysis));
            $("#divShipsFindingEvaluations").html(lntobr(data.risk));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.height-equal-findings').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccInvestigationFinding() {
    $.ajax({
        url: "/HazOcc/GetHazOccInvestigationFinding",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "encryptedVesselId": $("#EncryptedVesselId").val()
        },
        "datatype": "JSON",
        success: function (data) {

            $("#divInvestigationAnalysis").html(lntobr(data.analysis));
            $("#divInvestigationEveluation").html(lntobr(data.risk));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.height-equal-findings').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccDirectCauses() {
    $.ajax({
        url: "/HazOcc/GetHazOccDirectCauses",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
        },
        "datatype": "JSON",
        success: function (data) {
            var title = "", count = "";
            $("#divFindingDirectCases").html("");
            $.each(data, function (index, jsonObject) {
                $.each(jsonObject, function (key, val) {

                    if (title != val.subStandardName) {
                        title = val.subStandardName
                        count = jsonObject.filter(function (o) { return o.subStandardName == title }).length;

                        $("#divFindingDirectCases").append(GetHtmlFormatForCausessTitle(title, count));
                    }

                    $("#divFindingDirectCases").append(GetHtmlFormatForCausess(val.longDescription));
                });
            });

        },
        complete: function () {
            if (screen.width > 767) {
                $('.height-equal-findings').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccRootCauses() {
    $.ajax({
        url: "/HazOcc/GetHazOccRootCauses",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
        },
        "datatype": "JSON",
        success: function (data) {
            var title = "", count = "";

            $("#divFindingRootCases").html("");
            $.each(data, function (index, jsonObject) {
                $.each(jsonObject, function (key, val) {

                    if (title != val.subStandardName) {
                        title = val.subStandardName
                        count = jsonObject.filter(function (o) { return o.subStandardName == title }).length;

                        $("#divFindingRootCases").append(GetHtmlFormatForCausessTitle(title, count));
                    }

                    $("#divFindingRootCases").append(GetHtmlFormatForCausess(val.longDescription));
                });
            });
        },
        complete: function () {
            if (screen.width > 767) {
                $('.height-equal-findings').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHierarchyExplorerMapping() {
    $.ajax({
        url: "/HazOcc/GetHierarchyExplorerMapping",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val()
        },
        "datatype": "JSON",
        success: function (data) {

            BindHierarchyExplorerMapping(data);

        },
        complete: function () {

        }
    });
}

function BindHierarchyExplorerMapping(response) {

    var hierarchyExplorerList = $('#dtHierarchyExplorerMapping').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-12 offset-lg-0 col-xl-12 offset-xl-0 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "language": {
            "emptyTable": "No data available.",
        },
        "data": response.data,
        "columns": [
            {
                className: "tdblock td-row-header font-weight-600 data-text-align",
                "data": "componentName",
                "width": "170px",
                render: function (data, type, full, meta) { return GetExportData(getValueOrBlankReturnEmpty(data)); }
            },
            {
                className: "data-text-align tdblock",
                "data": "systemArea",
                "width": "170px",
                render: function (data, type, full, meta) { return GetCellData('Hierarchy', getValueOrBlankReturnEmpty(data)); }

            },
            {
                className: "data-text-align",
                "data": "maker",
                "width": "95px",
                render: function (data, type, full, meta) { return GetCellData('Maker', getValueOrBlankReturnEmpty(data)); }
            },
            {
                className: "data-text-align",
                "data": "model",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Model', getValueOrBlankReturnEmpty(data)); }
            },
            {
                className: "data-text-align",
                "data": "designer",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Designer', getValueOrBlankReturnEmpty(data)); }
            },
            {
                className: "data-text-align",
                "data": "position",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Position', getValueOrBlankReturnEmpty(data)); }
            }
        ]
    });
}

function BindRescheduleHistory() {

    var defectdetails = $('#dtactionsrescedule').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No reschedule history available.",
        },
        "ajax": {
            "url": "/HazOcc/GetIncidentActionsAll",
            "type": "POST",
            "data": {
                "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            },
            "datatype": "json"
        },
        "initComplete": function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip();
            if (screen.width > 767) {
                $('.height-equal-actions').matchHeight({
                    byRow: 0
                });
            }
        },
        "columns": [
            {
                className: "tdblock td-row-header font-weight-600 data-icon-align",
                data: "status",
                width: "40px",
                render: function (data, type, full, meta) {
                    var StatusColor = colorMap.get(0).color;
                    if (colorMap.has(full.statusColor)) {
                        StatusColor = colorMap.get(full.statusColor).color;
                    }

                    var htmlElement = '<span class="badge badge-circle ' + StatusColor + '" data-toggle="tooltip" data-placement="bottom" title="' + full.statusDesc + '">' + data + '</span>';
                    return GetExportData(htmlElement);
                }
            },
            {
                className: "top-margin-0 data-datetime-align tdblock",
                "data": "actionDate",
                "width": "125px",
                render: function (data, type, full, meta) { return GetFormattedDateTimeAMPMFormat(type, 'Date Created', data); }

            },
            {
                className: "data-text-align tdblock",
                "data": "actionToBeTaken",
                "width": "125px",
                render: function (data, type, full, meta) { return GetCellData('Actions to be Taken', data); }
            },
            {
                className: "data-datetime-align tdblock",
                "data": "deadline",
                "width": "125px",
                render: function (data, type, full, meta) { return GetFormattedDateTimeAMPMFormat(type, 'Due Date', data); }
            },
            {
                className: "data-datetime-align tdblock",
                "data": "closureDate",
                "width": "136px",
                render: function (data, type, full, meta) { return GetFormattedDateTimeAMPMFormat(type, 'Closure Date Of Action', data); }
            },
            {
                className: "data-text-align tdblock",
                "data": "actionTaken",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Action<Br> Taken', data); }
            },
        ]
    });
}

function GetHazOccCausation() {
    $.ajax({
        url: "/HazOcc/GetHazOccCausation",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            var title = "", count = "";
            $("#divActionRootCases").html("");
            $.each(data.rootCauses, function (index, jsonObject) {

                if (title != jsonObject.name) {
                    title = jsonObject.name;
                    count = data.rootCauses.filter(function (o) { return o.name == title }).length;
                    $("#divActionRootCases").append(GetHtmlFormatForCausessTitle(title, count));
                }

                $("#divActionRootCases").append(GetHtmlFormatForCausess(jsonObject.longDescription));
            });

            title = ""; count = "";
            $("#divActionDirectCases").html("");
            $.each(data.directCauses, function (index, jsonObject) {

                if (title != jsonObject.name) {
                    title = jsonObject.name;
                    count = data.directCauses.filter(function (o) { return o.name == title }).length;
                    $("#divActionDirectCases").append(GetHtmlFormatForCausessTitle(title, count));
                }

                $("#divActionDirectCases").append(GetHtmlFormatForCausess(jsonObject.longDescription));
            });

        },
        complete: function () {
            if (screen.width > 767) {
                $('.height-equal-actions').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetNearMissSummary() {
    $.ajax({
        url: "/HazOcc/GetNearMissSummary",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            $("#spnReportDate").text(getValueOrBlank(data.reportDate));
            $("#spanNearMissDateTime").text(getValueOrBlank(data.nearMissDateTime));

            if (data.closedDate != null) {
                $("#spanClosedDate").text(getValueOrBlank(data.closedDate));
            }
            else {
                $("#isVisibleClosedDate").hide();
            }

            $("#spanSafetyOfficer").text(getValueOrBlank(data.safetyOfficer));
            $("#spanClassification").text(getValueOrBlank(data.classification));
            $("#spanObservationRaisedBy").text(getValueOrBlank(data.observationRaisedByName));
            $("#spanReporterRank").text(getValueOrBlank(data.rank));

            $("#spanLastChanged").text(getValueOrBlank(data.updatedDate));
            $("#spanCreatedBy").text(getValueOrBlank(data.createdBy));
            $("#spanMaster").text(getValueOrBlank(data.masterName));
            $("#spanPotentialSeverity").text(getValueOrBlank(data.potentialSeverity));
            $("#spanDescription").html(lntobr(data.description));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.nearmiss-height-equal').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccNearMissEvent() {
    $.ajax({
        url: "/HazOcc/GetHazOccNearMissEventDetails",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
        },
        "datatype": "JSON",
        success: function (data) {
            $("#spanOperation").text(getValueOrBlank(data.operation));
            $("#spanPotentialCosequences").text(getValueOrBlank(data.possibleConsequence));
            $("#divImmediateActionTaken").html(lntobr(data.immediateActionTaken));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.event-height-equal').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetIllnessSummary() {
    $.ajax({
        url: "/HazOcc/GetHazOccIllnessSummary",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            $("#spnReportDate").text(getValueOrBlank(data.reportDate));
            $("#spanIllnessDateTime").text(getValueOrBlank(data.accidentDateTime));
            $("#spanSafetyOfficer").text(getValueOrBlank(data.safetyOfficer));
            $("#spanClassification").text(getValueOrBlank(data.classification));
            $("#spanInvestigationDate").text(getValueOrBlank(data.investigateDate));
            $("#spanInvestigatorName").text(getValueOrBlank(data.investigateName));
            $("#spanInvestigatorRank").text(getValueOrBlank(data.investigateRankName));

            $("#spanLastChanged").text(getValueOrBlank(data.updatedDate));
            $("#spanCreatedBy").text(getValueOrBlank(data.createdBy));
            $("#spanParentReport").text(getValueOrBlank(data.parentReport));
            $("#spanMaster").text(getValueOrBlank(data.masterName));
            $("#spanActualSeverity").text(getValueOrBlank(data.actualSeverity));

            $("#spanDescription").html(lntobr(data.description));
            $("#spanClosureComment").html(lntobr(data.closureComments));

            if (data.closedDate != null) {
                $("#spnClosedDate").text(getValueOrBlank(data.closedDate));
            }
            else {
                $("#isVisibleClosedDate").hide();
            }

        },
        complete: function () {
            if (screen.width > 767) {
                $('.passengerillnesssummary-height').matchHeight({
                    byRow: 0
                });
                var total = $("#leftcolumn").height();
                $("#rightcolumnDescription").css({ height: (Math.round(total / 2) - 0) + "px" });
                $("#rightcolumnComments").css({ height: (Math.round(total / 2) - 14) + "px" });

                var newWidth = ($("#rightcolumnDescription").outerHeight());
                $("#rightcolumnDescription p").css({
                    "height": newWidth - 60
                });
            }
        }
    });
}

function GetHazOccPassengerDetails() {
    $.ajax({
        url: "/HazOcc/GetHazOccPassengerDetails",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            $("#spnLastName").text(getValueOrBlank(data.lastName));
            $("#spnFirstName").text(getValueOrBlank(data.firstName));
            $("#spnNationality").text(getValueOrBlank(data.nationality));
            $("#spnDateofBirth").text(getValueOrBlank(data.dateOfBirth));
            $("#spnAddress").html(lntobr(data.address));
            $("#spnGender").text(getValueOrBlank(data.gender));
            $("#spnOccupation").text(getValueOrBlank(data.occupation));
            $("#spanMaritalStatus").text(getValueOrBlank(data.maritalStatus));
            $("#spanTelephoneNo").text(getValueOrBlank(data.telephoneNumber));

            $("#spnCabinNo").text(getValueOrBlank(data.cabinNumber));
            $("#spnBookingPassageNo").text(getValueOrBlank(data.bookingNumber));
            $("#spnEmbarkedIn").text(getValueOrBlank(data.embarkedInCountryCode) + "-" + getValueOrBlank(data.embarkedInPortName));
            $("#spanDueDisembrk").text(getValueOrBlank(data.disembarkedAtCountryCode) + " - " + getValueOrBlank(data.disembarkedInPortName));
            $("#spnEmbarkedDate").text(getValueOrBlank(data.embarkedDate));
            $("#spanDisembarkDate").text(getValueOrBlank(data.disembarkedDate));
            $("#spnTravelAgent").text(getValueOrBlank(data.travelAgent));

            $("#spnCompanionRelationship").text(getValueOrBlank(data.relationship));
            $("#spnCompanionLastName").text(getValueOrBlank(data.companionLastName));
            $("#spnCompanionFirstName").text(getValueOrBlank(data.companionFirstName));
            $("#spnCompanionNationality").text(getValueOrBlank(data.companionNationality));
            $("#spnCompanionTelephoneNo").text(getValueOrBlank(data.companionTelephoneNumber));
            $("#spnCompanionAddess").html(lntobr(data.companionAddress));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.passengerillnespersonal-height').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccPassengerTreatmentDetails() {
    $.ajax({
        url: "/HazOcc/GetHazOccPassengerTreatmentDetails",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (result) {
            console.log("tre result", result);

            var data = result.response;
            $("#divDetailsIllness").html(lntobr(data.injuryDetail));
            $("#divDetailsTreatement").text(getValueOrBlank(data.injuryTreatment));
            $("#spanPort").text(getValueOrBlank(data.disembarkedCountryCode) + " - " + getValueOrBlank(data.disembarkedPortName));
            $("#spanDate").text(getValueOrBlank(data.disembarkedDate));
            $("#spanOnshoreDoctor").text(getValueOrBlank(data.sentToShoreDoctor));
            $("#spanTestasRecommended").text(getValueOrBlank(data.xRaysRecommended));
            $("#divResultTests").html(lntobr(data.testResult));
            $("#divNameAddressHospitalDoctor").html(lntobr(data.doctorNameAndAddress));
            $("#divRemarks").html(lntobr(data.remarks));
            BindMedicalVisists(result.data);
        },
        complete: function () {
            if (screen.width > 767) {
                $('.passengerillnestreat-height').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function BindMedicalVisists(data) {
    console.log("BindMedicalVisists", data);
    var medicalVisistsList = $('#dtMedicalVisits').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-5 col-xl-8 offset-xl-4 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": false,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No medical visits available.",
        },
        "data": data,
        "columns": [
            {
                className: "data-datetime-align",
                "data": "visitOn",
                "width": "95px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Date', data); }
            },
            {
                className: "data-number-align",
                "data": "visitNo",
                //"width": "80px",
                render: function (data, type, full, meta) { return GetCellData('No. Of Visists', data); }
            }
        ]
    });
}

function GetHazOccPassengerDoctorsReport() {
    $.ajax({
        url: "/HazOcc/GetHazOccPassengerDoctorsReport",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {
            $("#spanFirstAidAdministered").text(getValueOrBlank(data.isFirstAidAdministered));

            var firstAidAdministeredBy = "";
            if (getValueOrBlankReturnEmpty(data.firstAidAdministeredByForename) != "") {
                firstAidAdministeredBy = getValueOrBlankReturnEmpty(data.firstAidAdministeredBySurname) + ", " + getValueOrBlankReturnEmpty(data.firstAidAdministeredByForename);
            }
            else {
                firstAidAdministeredBy = getValueOrBlankReturnEmpty(data.firstAidAdministeredBySurname);
            }

            $("#spanAdministeredBy").text(getValueOrBlank(firstAidAdministeredBy));

            $("#spanResucitationEquipmentAvailable").text(getValueOrBlank(data.isResuscitationEquipmentAvailable));
            $("#spanLocationEquipment").text(getValueOrBlank(data.equipmentLocation));

            $("#spanInjuredPersonAlcohol").text(getValueOrBlank(data.hasConsumedDrugOrAlcohol));
            $("#spanAmountAlcoholConsumed").text(getValueOrBlank(data.alcoholConsumed));

            $("#spanDoesInjuredPerson").text(getValueOrBlank(data.hasSpectacle));
            $("#spanWereTheyWornAccident").text(getValueOrBlank(data.spectaclesWorn));

            $("#spanAuthorisedPlaceDuringTheAccident").text(getValueOrBlank(data.atAuthorisedPlace));
            $("#spanDescriptionShoesWornInjuredPerson").text(getValueOrBlank(data.shoesDescription));

            $("#divDiagnosisDetailsIllness").html(lntobr(data.doctorsDiagnosis));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.passengerillnesdoctor-height').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccCrewIllnessDetail() {
    $.ajax({
        url: "/HazOcc/GetHazOccCrewIllnessDetail",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            $("#spanInCrewList").text(getValueOrBlank(data.crewNotFound));
            $("#spanCrewList").text("-");

            if (data.crewNotFound === "Yes") {
                $("#spanCrewList").text(getValueOrBlank(data.crewLastName) + ", " + getValueOrBlank(data.crewFirstName));
            }

            $("#spnLastName").text(getValueOrBlank(data.crewLastName));
            $("#spnFirstName").text(getValueOrBlank(data.crewFirstName));

            $("#spanRank").text(getValueOrBlank(data.rankDescription));
            $("#spnNationality").text(getValueOrBlank(data.nationality));

            $("#spnDateofBirth").text(getValueOrBlank(data.dateOfBirth));
            $("#spnAddress").html(lntobr(data.address));
            $("#spnGender").text(getValueOrBlank(data.gender));
            $("#spanMaritalStatus").text(getValueOrBlank(data.maritalStatus));

            $("#spanPsassportNo").text(getValueOrBlank(data.passportNumber));
            $("#spanSeamanNo").text(getValueOrBlank(data.bookNumber));
            $("#spanPCN").text(getValueOrBlank(data.pcn));

            $("#spnReportDate").text(getValueOrBlank(data.reportedDate));
            $("#spnNatureIllness").text(getValueOrBlank(data.illnessTypes));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.crewillnespersonal-height').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHazOccCrewTreatmentDetail() {
    $.ajax({
        url: "/HazOcc/GetHazOccCrewTreatmentDetail",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            $("#spanSignedOffResult").text(getValueOrBlank(data.isCrewOffSigned));
            $("#spanSignedOffResultDate").text(getValueOrBlank(data.offSignedDate));
            $("#spanSignedOffPort").text(getValueOrBlank(data.offSignedCountryCode) + " - " + getValueOrBlank(data.offSignedPortName));

            $("#spanTreatedAshore").text(getValueOrBlank(data.isCrewTreatedAshore));
            $("#spanTreatedAshoreDate").text(getValueOrBlank(data.treatmentDate));
            $("#spnTreatedPort").text(getValueOrBlank(data.treatmentCountryCode) + " - " + getValueOrBlank(data.treatmentPortName));

            $("#spanDrugsAlcoholTest").text(getValueOrBlank(data.drugAlcoholTested));
            $("#spanAlcoholTestAvailable").text(getValueOrBlank(data.testResult));
            $("#spnWithNoRestrictions").text(getValueOrBlank(data.hasCrewResumedWork));
            $("#spnDaysIncapacitated").text(getValueOrBlank(data.numberOfDaysOff));
            $("#spanDutiesResumedon").text(getValueOrBlank(data.resumedDate));
            $("#spanCompliantHours").text(getValueOrBlank(data.hoursComplaint));

            $("#spanFirstAidOnBoard").text(getValueOrBlank(data.firstAidGivenOnBoard));
            $("#spanDetailsIllness").html(lntobr(data.injuryDetail));
            $("#spanDetailsTreatment").html(lntobr(data.injuryTreatment));
            $("#spanDependentDetail").html(lntobr(data.dependentDetail));
            $("#spanComments").html(lntobr(data.comments));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.crewillnestreat-height').matchHeight({
                    byRow: 0
                });
                var total = $("#leftcolumntreament").height();
                console.log('total', total);
                $("#rightcolumnillness").css({ height: (Math.round(total / 4) - 0) + "px" });
                $("#rightcolumntreatment").css({ height: (Math.round(total / 4) - 0) + "px" });
                $("#rightcolumndeath").css({ height: (Math.round(total / 4) - 16) + "px" });
                $("#rightcolumncomments").css({ height: (Math.round(total / 4) - 14) + "px" });
                $(".crewillnestreat-height").css({ height: (Math.round(total / 1) + 10) + "px" });
            }
        }
    });
}

function GetHazOccCrewDoctorsReport() {
    $.ajax({
        url: "/HazOcc/GetHazOccCrewDoctorsReport",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (result) {

            var data = result.response;
            $("#spanDoctorName").text(getValueOrBlank(data.doctorName));
            $("#spanRestrictionApplyDays").text(getValueOrBlank(data.restrictionDays));
            $("#spanDisabilityDays").text(getValueOrBlank(data.disabilityDays));
            $("#spnHospitalisationRequired").text(getValueOrBlank(data.hospitalisationRequired));

            $("#spanDetailsSymptoms").html(lntobr(data.symptomDescription));
            $("#spanAddressHospital").html(lntobr(data.hospitalDetail));
            $("#spanDoctorAddress").html(lntobr(data.doctorAddress));

            BindDoctorVisists(result.data);
        },
        complete: function () {
            if (screen.width > 767) {
                $('.crewillnesdoctor-height').matchHeight({
                    byRow: 0
                });
                var total = $("#leftcolumndoctor").height();
                console.log('total', total);
                $("#rightcolumnmedical").css({ height: (Math.round(total / 3) - 5) + "px" });
                $("#rightcolumnhospital").css({ height: (Math.round(total / 3) - 5) + "px" });
                $("#rightcolumnaddress").css({ height: (Math.round(total / 3) - 19) + "px" });
            }
        }
    });
}

function BindDoctorVisists(data) {

    var doctorVisistsList = $('#dtDoctorVisits').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-4 offset-xl-8 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": false,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No doctor visits available.",
        },
        "data": data,
        "columns": [
            {
                className: "top-margin-0 data-text-align",
                "data": "limitation",
                "width": "150px",
                render: function (data, type, full, meta) { return GetCellData('Limitations', data); }
            },
            {
                className: "top-margin-0 data-text-align",
                "data": "type",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            }
        ]
    });
}

function GetHazOccThirdPartyIllnessDetail() {
    $.ajax({
        url: "/HazOcc/GetHazOccThirdPartyAccidentDetail",
        type: "POST",
        "data": {
            "encryptedIncidentId": $("#EncryptedIdentifier").val(),
            "vesselType": $("#VesselType").val(),
        },
        "datatype": "JSON",
        success: function (data) {

            $("#spnLastName").text(getValueOrBlank(data.lastName));
            $("#spnFirstName").text(getValueOrBlank(data.firstName));
            $("#spnNationality").text(getValueOrBlank(data.nationality));
            $("#spnDateofBirth").text(getValueOrBlank(data.dateOfBirth));
            $("#spnAddress").html(lntobr(data.address));
            $("#spanPsassportNo").text(getValueOrBlank(data.passportNumber));
            $("#spanSeamanNo").text(getValueOrBlank(data.bookNumber));

            $("#spanDetailsIllness").html(lntobr(data.injuryDetail));
        },
        complete: function () {
            if (screen.width > 767) {
                $('.thirdpartypersonal-height').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function GetHtmlFormatForCausessTitle(title, count) {
    var container = '<div class="row">' +
        '<div class="col-12 col-md-12 col-lg-12 col-xl-12">' +
        '<div class="dashboard-counters">' +
        '<span>' + title + ' (' + count + ')</span>' +
        '</div>' +
        '</div>' +
        '</div>';
    return container;
}

function GetHtmlFormatForCausess(val) {
    var container = '<div class="row">' +
        '<div class="col-12 col-md-12 col-lg-12 col-xl-12">' +
        '<div class="dashboard-counters-label">' +
        '<span> - </span><span id="">' + val + '</span>' +
        '</div>' +
        '</div>' +
        '</div >';
    return container;
}

function getValueOrBlank(data) {
    if (data == null || data == "") {
        return "-";
    }
    return data;
}

function getValueOrBlankReturnEmpty(data) {
    if (data == null || data == "") {
        return "";
    }
    return data;
}

function lntobr(str) {
    str = getValueOrBlank(str)
    return str.replace(/(?:\r\n|\r|\n)/g, '<br>');
}

function BindInsuranceClaimsList() {

    var insuranceClaims = $('#dtInsuranceClaims').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 col-lg-8 col-xl-8 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": false,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No Insurance claims available.",
        },
        "ajax": {
            "url": "/HazOcc/GetHazOccLinkedInsuranceClaims",
            "type": "POST",
            "data": {
                "encryptedIncidentId": $("#EncryptedIdentifier").val(),
                "encryptedVeselId": $("#EncryptedVesselId").val()
            },
            "datatype": "json"
        },
        "order": [],
        "columns": [
            {
                className: "d-none d-sm-table-cell text-left data-text-align",
                "data": "claimNumber",
                "width": "60px",
                render: function (data, type, full, meta) { return GetCellData('Claim Number', data); }
            },
            {
                className: "tdblock td-row-header data-text-align",
                "data": "name",
                "width": "200px",
                render: function (data, type, full, meta) {
                    let ClaimNameUIElement = '<span class="d-inline-block d-sm-none">' + full.claimNumber + '&nbsp; - &nbsp;' + '</span>'
                    ClaimNameUIElement += full.name;

                    return ClaimNameUIElement;
                }

            },
            {
                className: "data-text-align",
                "data": "claimType",
                "width": "60px",
                render: function (data, type, full, meta) { return GetCellData('Claim Type', data); }
            },
            {
                className: "data-datetime-align",
                "data": "reportDate",
                "width": "55px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Report Date', data); }
            },
            {
                className: "data-datetime-align",
                "data": "openDate",
                "width": "55px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Open Date', data); }
            },
            {
                className: "data-datetime-align",
                "data": "closeDate",
                "width": "55px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Close Date', data); }
            },
            {
                className: "data-text-align tdblock",
                "data": "systemArea",
                "width": "115px",
                render: function (data, type, full, meta) { return GetCellData('System Area', data); }

            },
            {
                className: "data-number-align",
                "data": "cost",
                "width": "60px",
                render: function (data, type, full, meta) { return GetCellData('Cost (USD)', data); }
            },
        ],
        "initComplete": function () {
            $('#spanInsuranceClaimsCount').text(this.api().data().length);
        }
    });
}


