import "@chenfengyuan/datepicker";
import "daterangepicker";
import moment from "moment";
import ApexCharts from 'apexcharts';
import Chart from "chart.js";

import * as JSZip from "jszip";
window.JSZip = JSZip;
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";

import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, GetCookie, ToastrAlert, ConvertDecimalNumberToString } from "../common/utilities.js"
var startDate, endDate;
var inspectionEndDate, inspectionStartDate;

require('bootstrap');

var maxSpeed = 60;

$(document).ready(function () {
	AjaxError();
	var vesselDetailHeight = $("#cdVesselDetail").height();
	var recentActivityHeight = $("#cdRecentActivity").height();
	var certificateHeight = $("#cdCertificate").height();
	var charteresOrderHeight = $("#cdCharteresOrder").height();

	AddLoadingIndicator();
	RemoveLoadingIndicator();

	$(window).on('load', function () {
		if (($(this).width() > 1024)) {
			certificateHeight = $("#cdCertificate").height();
			$("#cdCharteresOrder").height(certificateHeight - 32);
		}

		if ($(this).width() > 1024) {
			vesselDetailHeight = $("#cdVesselDetail").height();
			$("#cdRecentActivity").height(vesselDetailHeight);
		}
	});

	if (($(this).width() > 1024) && (vesselDetailHeight != recentActivityHeight)) {
		$("#cdRecentActivity").height(vesselDetailHeight);
	}

	if (($(this).width() > 1024)) {
		certificateHeight = $("#cdCertificate").height();

		$("#cdCharteresOrder").height(certificateHeight - 32);
	}

	$('.height-equal').matchHeight();
	$('.height-equal-open-order-pms').matchHeight();

	$('[data-toggle="datepicker-icon-managementStart"]').datepicker({
		trigger: ".datepicker-trigger-managementStart",
	});

	$('[data-toggle="datepicker-icon-managementEnd"]').datepicker({
		trigger: ".datepicker-trigger-managementEnd",
	});

	$(".btn.toggle-on").click(function () {
		var parentDiv = $(this).parents('div.card-header');
		var siblingDiv = parentDiv.siblings('div.widget-content');
		siblingDiv.filter(".widget-content.blur").addClass("blur-active");
		parentDiv.find(".active-link").addClass("inactive-link");
		parentDiv.find(".charterer-dropdown").addClass("d-none");
		parentDiv.find(".common-date-white").addClass("d-none");
	});
	$(".btn.toggle-off").click(function () {
		var parentDiv = $(this).parents('div.card-header');
		var siblingDiv = parentDiv.siblings('div.widget-content');
		siblingDiv.filter(".widget-content.blur").removeClass("blur-active");
		parentDiv.find(".active-link").removeClass("inactive-link");
		parentDiv.find(".charterer-dropdown").removeClass("d-none");
		parentDiv.find(".common-date-white").removeClass("d-none");
	});
	$('.close-new').click(function () {
		$(this).closest('.col-xl-6').hide();
		$(this).closest('.col-lg-6').hide();
		$(this).closest('.col-lg-12').hide();
		return false;
	});

	//port service attachment list modal data

	var servicedata = [
		{
			"createddate": "27 OCT 2020",
			"title": "Covid Guidelines",
			"description": "Covid Guidelines",
		},
	]
	var portservicelist = $('#dtportservicelist').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": servicedata,
		"columns": [
			{
				className: "text-left",
				data: "createddate",
				render: function (data, type, full, meta) { return GetCellData('Created Date', data); }
			},
			{
				className: "text-left",
				data: "title",
				render: function (data, type, full, meta) { return GetCellData('Title', data); }
			},
			{
				className: "text-left tdblock",
				data: "description",
				render: function (data, type, full, meta) { return GetCellData('Description', data); }
			}
		]
	});

	//bad weather list modal data

	//var badweatherdata = [
	//	{
	//		"length": "Swell Length",
	//		"charter": "Short &nbsp;&nbsp;&nbsp;0-100",
	//		"max": "Short &nbsp;&nbsp;&nbsp;0-100",
	//	},
	//	{
	//		"length": "Wind Force",
	//		"charter": "04 Moderate breeze",
	//		"max": "05 Fresh breeze",
	//	},
	//]
	//var badweatherlist = $('#dtbadweatherlist').DataTable({
	//	"processing": true,
	//	"serverSide": false,
	//	"lengthChange": true,
	//	"searching": false,
	//	"info": false,
	//	"autoWidth": true,
	//	"paging": false,
	//	"data": badweatherdata,
	//	"columns": [
	//		{
	//			className: "text-left tdblock td-row-header",
	//			data: "length",
	//			"orderable": false,
	//			//render: function (data, type, full, meta) { return data }
	//		},
	//		{
	//			className: "text-left",
	//			data: "charter",
	//			render: function (data, type, full, meta) { return GetCellData('Charter', data); }
	//		},
	//		{
	//			className: "text-left",
	//			data: "max",
	//			render: function (data, type, full, meta) { return GetCellData('Max', data); }
	//		}
	//	]
	//});

	//offhire list modal data

	var offhiredata = [
		{
			"reason": "Commercial Reason",
			"delay": "12:15",
			"offhire": "No",
			"from": "25 Apr 2020 12:00",
			"to": "26 Apr 2020 00:15",
		},
		{
			"reason": "Canal Transit",
			"delay": "16:30",
			"offhire": "Yes",
			"from": "25 Apr 2020 12:00",
			"to": "26 Apr 2020 04:30",
		},
	]
	var offhirelist = $('#dtOffHireList').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": offhiredata,
		"columns": [
			{
				className: "text-left tdblock",
				data: "reason",
				render: function (data, type, full, meta) { return GetCellData('Reason', data); }
			},
			{
				className: "text-left",
				data: "delay",
				render: function (data, type, full, meta) { return GetCellData('Delay', data); }
			},
			{
				className: "text-left",
				data: "offhire",
				render: function (data, type, full, meta) { return GetCellData('Off - Hire', data); }
			},
			{
				className: "text-left",
				data: "from",
				render: function (data, type, full, meta) { return GetCellData('From', data); }
			},
			{
				className: "text-left",
				data: "to",
				render: function (data, type, full, meta) { return GetCellData('To', data); }
			}
		]
	});

	function GetCellData(label, data) {
		return '<label>' + label + '</label> <br />' + data;
	}

	//daterangepicker offhire
	var start = moment().subtract(6, "month");
	var end = moment();

	function cb(start, end) {
		startDate = start.format("DD MMM YYYY");
		endDate = end.format("DD MMM YYYY");
		$("#dtroffhire span").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
	}

	$("#dtroffhire").daterangepicker(
		{
			startDate: start,
			endDate: end,
			opens: "right",
			ranges: {
				Today: [moment(), moment()],
				Yesterday: [moment().subtract(1, "days"), moment().subtract(1, "days")],
				"Last 7 Days": [moment().subtract(6, "days"), moment()],
				"Last 30 Days": [moment().subtract(29, "days"), moment()],
				"This Month": [moment().startOf("month"), moment().endOf("month")],
				"Last Month": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")],
				"Last 6 Month": [moment().subtract(6, "month"), moment()]
			},
			locale: {
				format: "DD MMM YYYY"
			}
		},
		cb
	);

	//inspection manager datepicker
	var inspStart = moment().subtract(6, "month");
	var inspEnd = moment();

	$("#inspectionDatepicker").daterangepicker(
		{
			startDate: inspStart,
			endDate: inspEnd,
			opens: "right",
			ranges: {
				"Last 3 Months": [moment().subtract(3, "month"), moment()],
				"Last 6 Months": [moment().subtract(6, "month"), moment()],
				"Last 12 Months": [moment().subtract(12, "month"), moment()],
				"Year To Date": [new Date(new Date().getFullYear(), 0, 1), moment()]
			},
			locale: {
				format: "DD MMM YYYY"
			}
		},
		InspectionDatePickerChange
	);

	function InspectionDatePickerChange(start, end) {

		inspectionStartDate = start.format("DD MMM YYYY");
		inspectionEndDate = end.format("DD MMM YYYY");
		var localStartDate = new Date(start);
		var localEndDate = new Date(end);
		var monthDifference = monthDiff(localStartDate, localEndDate);

		var globalConvertedDate = (end).toISOString().split('T')[0];
		var currentConvertedDate = (new Date()).toISOString().split('T')[0];

		if (monthDifference == 3) {
			if (globalConvertedDate == currentConvertedDate) {
				$('#spanInspectionCurrentDateFilter').html("Last 3 Months");
			}
		}
		else if (monthDifference == 6) {
			if (globalConvertedDate == currentConvertedDate) {
				$('#spanInspectionCurrentDateFilter').html("Last 6 Months");
			}
		}
		else if (monthDifference == 12) {
			if (globalConvertedDate == currentConvertedDate) {
				$('#spanInspectionCurrentDateFilter').html("Last 12 Months");
			}
		}
		else {
			$('#spanInspectionCurrentDateFilter').html("Custom Range");
		}

		$("#spanInspectionFromDate").html(start.format("DD MMM YYYY"));
		$("#spanInspectionToDate").html(end.format("DD MMM YYYY"));

		BindInspectionSummary();
		BindCertificateSummary();
	}

	InspectionDatePickerChange(inspStart, inspEnd)

	cb(start, end);
	BindPurchaseOrderSummary();
	BindOpexSummary();
	BindVesselDetailsSummary();
	BindingVesselOfficerDetails();
	BindVoyageReporting();

	BindHazoccSummary();
	BindCrewSummary();
	BindDefectManager();
	BindPMSSummary();
	BindRightShip();
});

function monthDiff(d1, d2) {
	var months;
	months = (d2.getFullYear() - d1.getFullYear()) * 12;
	months -= d1.getMonth();
	months += d2.getMonth();
	return months <= 0 ? 0 : months;
}

//Header Service Section

//Inspection Summary Call
function BindInspectionSummary() {

	var request =
	{
		"EncryptedVesselId": $('#VesselId').val(),
		"ToDate": inspectionEndDate,
		"FromDate": inspectionStartDate
	}
	$.ajax({
		url: "/Dashboard/GetInspectionDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},

		success: function (data) {
			if (data != null) {

				//psc
				$('#spanPSCDefectRate').text(data.pscDefectRate);
				$('#spanPSCDetaintionCount').text(data.pscDetaintionCount);

				//omv
				$('#spanTotalOmvInspectionCount').text(data.totalOmvInspectionCount);
				$('#spanOMVDefectRate').text(data.omvDefectRate);
				$('#spanOMVInspectionAverageRisk').text(data.omvInspectionAverageRisk);

				//inspection & audit
				$('#spanInspectionDueCount').text(data.inspectionDueCount);
				$('#spanInspectionOverdueCount').text(data.inspectionOverdueCount);

				//inspection
				$('#spanInspectionFindingOutstandingCount').text(data.inspectionFindingOutstandingCount);
				$('#spanInspectionFindingOverdueCount').text(data.inspectionFindingOverdueCount);
				$('#spanPendingClosureCount').text(data.pendingClosureCount);

				//audit
				$('#spanInspectionAuditFindingOutstandingCount').text(data.inspectionAuditFindingOutstandingCount);
				$('#spanInspectionAuditFindingOverdueCount').text(data.inspectionAuditFindingOverdueCount);
				$('#spanAuditPendingClosureCount').text(data.auditPendingClosureCount);

				//findings
				$('#spanTotalOutstandingFindingCount').text(data.totalOutstandingFindingCount);
				$('#spanTotalOverdueFindingCount').text(data.totalOverdueFindingCount);

				$('#spanTotalOutstandingFindingCount1').text(data.totalOutstandingFindingCount);
				$('#spanTotalOverdueFindingCount1').text(data.totalOverdueFindingCount);


				//settingh url

				var pscURL = "/Inspection/List/?Inspection=" + data.detentionTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#PSCDetaintion").attr("href", pscURL);

				var omvURL = "/Inspection/List/?Inspection=" + data.inspectionOMVTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#TotalOmvInspection").attr("href", omvURL);

				//var InspectionDueTypeURL = "/Inspection/List/?Inspection=" + data.inspectionDueTypeURL + "&VesselId=" + $('#VesselId').val()+"&IsViewMore=false";
				//$("#inspectionDue").attr("href", InspectionDueTypeURL);

				//var InspectionOverdueTypeURL = "/Inspection/List/?Inspection=" + data.inspectionOverdueTypeURL + "&VesselId=" + $('#VesselId').val()+"&IsViewMore=false";
				//$("#inspectionOverDue").attr("href", InspectionOverdueTypeURL);

				var inspectionFindingOutstandingTypeURL = "/Inspection/List/?Inspection=" + data.inspectionFindingOutstandingTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#InspectionFindingOutstanding").attr("href", inspectionFindingOutstandingTypeURL);

				var InspectionFindingOverdueTypeURL = "/Inspection/List/?Inspection=" + data.inspectionFindingOverdueTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#InspectionFindingOverdue").attr("href", InspectionFindingOverdueTypeURL);

				var PendingClosureByOfficeTypeURL = "/Inspection/List/?Inspection=" + data.pendingClosureByOfficeTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#PendingClosure").attr("href", PendingClosureByOfficeTypeURL);

				var AuditInspectionFindingOutstandingTypeURL = "/Inspection/List/?Inspection=" + data.auditInspectionFindingOutstandingTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#InspectionAuditFindingOutstanding").attr("href", AuditInspectionFindingOutstandingTypeURL);

				var AuditInspectionFindingOverdueTypeURL = "/Inspection/List/?Inspection=" + data.auditInspectionFindingOverdueTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#InspectionAuditFindingOverdue").attr("href", AuditInspectionFindingOverdueTypeURL);

				var AuditPendingClosureByOfficeTypeURL = "/Inspection/List/?Inspection=" + data.auditPendingClosureByOfficeTypeURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#AuditPendingClosure").attr("href", AuditPendingClosureByOfficeTypeURL);

				var AllInspectionURL = "/Inspection/List/?Inspection=" + data.allInspectionURL + "&VesselId=" + $('#VesselId').val() +"&IsViewMore=true";
				$("#inspectionAll").attr("href", AllInspectionURL);

				var aInspectionTitle = AllInspectionURL;
				$("#aInspectionTitle").attr("href", aInspectionTitle);

			}
		}
	});
}

function BindHazoccSummary() {

	var start = moment().subtract(12, "month").format("DD MMM YYYY");
	var end = moment().format("DD MMM YYYY");


	var request =
	{
		"VesselId": $('#VesselId').val(),
		"StartDate": start,
		"EndDate": end
	}
	$.ajax({
		url: "/Dashboard/GetHazoccDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},

		success: function (data) {
			if (data != null) {

				//Open Items
				$('#spanOpenItems').text(data.openItems);
				$('#spanOfficeRev').text(data.officeRev);

				//Open Incident
				$('#spanIncidentVerySerious').text(data.incidentVerySerious);
				$('#spanIncidentSerious').text(data.incidentSerious);
				$('#spanIncidentModerate').text(data.incidentModerate);
				$('#spanIncidentMinor').text(data.incidentMinor);

				//Crew Accidents
				$('#spanCrewAccidentFatal').text(data.crewAccidentFatal);
				$('#spanCrewAccidentLWC').text(data.crewAccidentLWC);
				$('#spanCrewAccidentRWC').text(data.crewAccidentRWC);
				$('#spanCrewAccidentMTC').text(data.crewAccidentMTC);
				$('#spanCrewAccidentFAC').text(data.crewAccidentFAC);

				//Statistics
				$('#spanStatisticsLTI').text(data.statisticsLTI);
				$('#spanStatisticsTRC').text(data.statisticsTRC);
				$('#spanStatisticsMEXPHS').text(data.statisticsMEXPHS);

				//Passenger Accidents
				$('#spanPassengerFatal').text(data.passengerFatal);
				$('#spanPassengerFAC').text(data.passengerFAC);
				$('#spanPassengerMTC').text(data.passengerMTC);

				//Near miss and Seafty observation
				$('#spanNearMissSafeActs').text(data.nearMissSafeActs);
				$('#spanNearMissCount').text(data.nearMissCount);
				$('#spanNearMissUnsafeActs').text(data.nearMissUnsafeActs);
				$('#spanNearMissUnsafeCond').text(data.nearMissUnsafeCond);


				//settingh url

				var openItemsURL = "/HazOcc/List/?HazOcc=" + data.openItemsUrl + "&VesselId=" + $('#VesselId').val();
				$("#OpenItems").attr("href", openItemsURL);

				var officeRevUrl = "/HazOcc/List/?HazOcc=" + data.officeRevUrl + "&VesselId=" + $('#VesselId').val();
				$("#OfficeRev").attr("href", officeRevUrl);

				var incidentVerySeriousUrl = "/HazOcc/List/?HazOcc=" + data.incidentVerySeriousUrl + "&VesselId=" + $('#VesselId').val();
				$("#IncidentVerySerious").attr("href", incidentVerySeriousUrl);

				var incidentSeriousUrl = "/HazOcc/List/?HazOcc=" + data.incidentSeriousUrl + "&VesselId=" + $('#VesselId').val();
				$("#IncidentSerious").attr("href", incidentSeriousUrl);

				var incidentModerateUrl = "/HazOcc/List/?HazOcc=" + data.incidentModerateUrl + "&VesselId=" + $('#VesselId').val();
				$("#IncidentModerate").attr("href", incidentModerateUrl);

				var incidentMinorUrl = "/HazOcc/List/?HazOcc=" + data.incidentMinorUrl + "&VesselId=" + $('#VesselId').val();
				$("#IncidentMinor").attr("href", incidentMinorUrl);

				var crewAccidentFatalUrl = "/HazOcc/List/?HazOcc=" + data.crewAccidentFatalUrl + "&VesselId=" + $('#VesselId').val();
				$("#CrewAccidentFatal").attr("href", crewAccidentFatalUrl);

				var crewAccidentLWCUrl = "/HazOcc/List/?HazOcc=" + data.crewAccidentLWCUrl + "&VesselId=" + $('#VesselId').val();
				$("#CrewAccidentLWC").attr("href", crewAccidentLWCUrl);

				var crewAccidentRWCUrl = "/HazOcc/List/?HazOcc=" + data.crewAccidentRWCUrl + "&VesselId=" + $('#VesselId').val();
				$("#CrewAccidentRWC").attr("href", crewAccidentRWCUrl);

				var crewAccidentMTCUrl = "/HazOcc/List/?HazOcc=" + data.crewAccidentMTCUrl + "&VesselId=" + $('#VesselId').val();
				$("#CrewAccidentMTC").attr("href", crewAccidentMTCUrl);

				var crewAccidentFACUrl = "/HazOcc/List/?HazOcc=" + data.crewAccidentFACUrl + "&VesselId=" + $('#VesselId').val();
				$("#CrewAccidentFAC").attr("href", crewAccidentFACUrl);

				var passengerFatalUrl = "/HazOcc/List/?HazOcc=" + data.passengerFatalUrl + "&VesselId=" + $('#VesselId').val();
				$("#PassengerFatal").attr("href", passengerFatalUrl);

				var passengerFACUrl = "/HazOcc/List/?HazOcc=" + data.passengerFACUrl + "&VesselId=" + $('#VesselId').val();
				$("#PassengerFAC").attr("href", passengerFACUrl);

				var passengerMTCUrl = "/HazOcc/List/?HazOcc=" + data.passengerMTCUrl + "&VesselId=" + $('#VesselId').val();
				$("#passengerMTC").attr("href", passengerMTCUrl);

				var nearMissSafeActsUrl = "/HazOcc/List/?HazOcc=" + data.nearMissSafeActsUrl + "&VesselId=" + $('#VesselId').val();
				$("#NearMissSafeActs").attr("href", nearMissSafeActsUrl);

				var nearMissCountUrl = "/HazOcc/List/?HazOcc=" + data.nearMissCountUrl + "&VesselId=" + $('#VesselId').val();
				$("#nearMissCount").attr("href", nearMissCountUrl);

				var nearMissUnsafeActsUrl = "/HazOcc/List/?HazOcc=" + data.nearMissUnsafeActsUrl + "&VesselId=" + $('#VesselId').val();
				$("#NearMissUnsafeActs").attr("href", nearMissUnsafeActsUrl);

				var nearMissUnsafeCondUrl = "/HazOcc/List/?HazOcc=" + data.nearMissUnsafeCondUrl + "&VesselId=" + $('#VesselId').val();
				$("#NearMissUnsafeCond").attr("href", nearMissUnsafeCondUrl);
			}
		}
	});
}

function BindPMSSummary() {

	var start = moment().clone().startOf('month').format('DD MMM YYYY');
	var end = moment().clone().endOf('month').format('DD MMM YYYY');;

	console.log(start);
	console.log(end);
	var request =
	{
		"VesselId": $('#VesselId').val(),
		"FromDate": start,
		"ToDate": end
	}
	$.ajax({
		url: "/Dashboard/GetPMSDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},

		success: function (data) {
			if (data != null) {

				//Critical WO
				$('#spanCriticalDone').text(data.criticalDoneCount);
				$('#spanCriticalDue').text(data.criticalDueCount);
				$('#spanCriticalOverDue').text(data.criticalOverDueCount);
				$('#spanCriticalOverDuePrior').text(data.criticalOverDuePriorCount);

				//Non Critical WO
				$('#spanNonCriticalDone').text(data.nonCriticalDoneCount);
				$('#spanNonCriticalDue').text(data.nonCriticalDueCount);
				$('#spanNonCriticalOverDue').text(data.nonCriticalOverDueCount);
				$('#spanNonCriticalOverDuePrior').text(data.nonCriticalOverDuePriorCount);

				//Ships WO
				$('#spanShipsWOPlanned').text(data.shipsWOPlannedCount);
				$('#spanShipsWODone').text(data.shipsWODoneCount);

				//RESC. WO
				$('#spanRescWOApproved').text(data.rescWOApprovedCount);

				//Spares Below
				$('#spanSparesBelowTechMin').text(data.sparesBelowTechMinCount);
				$('#spanSparesBelowOprMin').text(data.sparesBelowOprMinCount);


				//settingh url

				var criticalDoneUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalDoneUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#CriticalDone").attr("href", criticalDoneUrl);

				var criticalDueUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalDueUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#CriticalDue").attr("href", criticalDueUrl);

				var criticalOverDueUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalOverDueUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#CriticalOverDue").attr("href", criticalOverDueUrl);

				var criticalOverDuePriorUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalOverDuePriorUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#CriticalOverDuePrior").attr("href", criticalOverDuePriorUrl);

				var nonCriticalDoneUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.nonCriticalDoneUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#NonCriticalDone").attr("href", nonCriticalDoneUrl);

				var nonCriticalDueUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.nonCriticalDueUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#NonCriticalDue").attr("href", nonCriticalDueUrl);

				var nonCriticalOverDueUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.nonCriticalOverDueUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#NonCriticalOverDue").attr("href", nonCriticalOverDueUrl);

				var nonCriticalOverDuePriorUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.nonCriticalOverDuePriorUrl + "&VesselId=" + $('#VesselId').val();
				$("#NonCriticalOverDuePrior").attr("href", nonCriticalOverDuePriorUrl);

				var shipsWOPlannedUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.shipsWOPlannedUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#ShipsWOPlanned").attr("href", shipsWOPlannedUrl);

				var shipsWODoneUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.shipsWODoneUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#ShipsWODone").attr("href", shipsWODoneUrl);

				var rescWOApprovedUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.rescWOApprovedUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#RescWOApproved").attr("href", rescWOApprovedUrl);

				var sparesBelowTechMinUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.sparesBelowTechMinUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#SparesBelowTechMin").attr("href", sparesBelowTechMinUrl);

				var sparesBelowOprMinUrl = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.sparesBelowOprMinUrl + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=false";
				$("#SparesBelowOprMin").attr("href", sparesBelowOprMinUrl);
			}
		}
	});
}

function BindRightShip() {

	var request =
	{
		"VesselId": $('#VesselId').val()
	}
	$.ajax({
		url: "/Dashboard/GetOltVesselPerformance",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		success: function (data) {
			$('#spanRightship').text(data);

			if (isNaN(data)) {
				$('#spanRightship').addClass('text-black');

			} else if (parseInt(data) < 3) {
				$('#spanRightship').addClass('text-red');

			} else {
				$('#spanRightship').addClass('text-green');

			}

		}
	});
}

function BindCertificateSummary() {
	var request =
	{
		"vesselId": $('#VesselId').val()
	}
	$.ajax({
		url: "/Dashboard/GetCertificateDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"input": request
		},

		success: function (data) {
			if (data != null && data != 'undefined') {
				$('#spanCertificateTotalActive').text(data.allActiveCertificateCount);
				$('#spanCertificateOverdue').text(data.overDueCertificateCount);
				$('#spanCertificateExpiringIn30Days').text(data.expires30DaysCertificateCount);
				$('#spanCertificateWithinSurveyRange').text(data.surveyRangeCertificateCount);

				document.getElementById('prgBarCertificateOverdue').style.width = (data.overDueCertificateCount * 100) / (data.allActiveCertificateCount) + '%';
				document.getElementById('prgBarCertificateExpiringIn30Days').style.width = (data.expires30DaysCertificateCount * 100) / (data.allActiveCertificateCount) + '%';
				document.getElementById('prgBarCertificateWithinSurveyRange').style.width = (data.surveyRangeCertificateCount * 100) / (data.allActiveCertificateCount) + '%';
				//document.getElementById('prgBarCertificateTotalActive').style.width = (data.allActiveCertificateCount * 100) / (data.allActiveCertificateCount) + '%';

				var certificateBaseURL = "/Certificate/List?VesselId=" + $('#VesselId').val() + "&CertificateRequestUrl=";

				$("#aCertificateOverdue").attr("href", certificateBaseURL + data.overDueCertificateCountURL + "&IsViewMore=false");
				$("#aCertificateExpiringIn30Days").attr("href", certificateBaseURL + data.expires30DaysCertificateCountURL + "&IsViewMore=false");
				$("#aCertificateWithinSurveyRange").attr("href", certificateBaseURL + data.surveyRangeCertificateCountURL + "&IsViewMore=false");
				$("#aCertificateTotalActive").attr("href", certificateBaseURL + data.allActiveCertificateCountURL + "&IsViewMore=false");
				$("#aCertificateExternal").attr("href", certificateBaseURL + data.allActiveCertificateCountURL + "&IsViewMore=true");
				$("#aCertificateTitle").attr("href", certificateBaseURL + data.allActiveCertificateCountURL + "&IsViewMore=true");


			}
		}
	});
}

function BindPurchaseOrderSummary() {
	//date is not used in api	
	var postartDate = moment().subtract(6, "month");
	var poendDate = moment();
	var localStartDate = postartDate.format("DD MMM YYYY");
	var localEndDate = poendDate.format("DD MMM YYYY");

	var request =
	{
		"vesselId": $('#VesselId').val(),
		"orderToDate": localEndDate,
		"orderFromDate": localStartDate
	}
	$.ajax({
		url: "/Dashboard/GetOrderSummary",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},

		success: function (data) {
			$('#spanPoInProcess').text(data.orderInProcessCount);
			$('#spanPoOrdered').text(data.orderedCount);
			$('#spanPoDispatched').text(data.orderDeliveryOnTheWayCount);
			$('#spanPoAuthEnq').text(data.authorisationCount);
			$('#spanPoRecieved').text(data.recievedIn30DaysCount);

			if (data.isOrderInProcessUrgent) {
				$('#aPOInProcess').addClass('text-red');
			}
			if (data.isOrderedUrgent) {
				$('#aPOOrdered').addClass('text-red');
			}
			if (data.isOrderDeliveryOnTheWayUrgent) {
				$('#aPODispateched').addClass('text-red');
			}
			if (data.isAuthorisationUrgent) {
				$('#aPOAuthEnq').addClass('text-red');
			}
			if (data.isRecievedIn30DaysUrgent) {
				$('#aPOReieved').addClass('text-red');
			}

			//url set up
			var InProcessURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.inProcessURL + "&VesselId=" + $('#VesselId').val();
			$("#aPOInProcess").attr("href", InProcessURL);
			$("#aPOViewMore").attr("href", InProcessURL + "&IsViewMore=true");
			$("#aPOTitle").attr("href", InProcessURL + "&IsViewMore=true");

			var OrderedURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.orderURL + "&VesselId=" + $('#VesselId').val();
			$("#aPOOrdered").attr("href", OrderedURL);

			var DispatchedURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.dispatchedURL + "&VesselId=" + $('#VesselId').val();
			$("#aPODispateched").attr("href", DispatchedURL);

			var AuthEnqURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.authEnqURL + "&VesselId=" + $('#VesselId').val();
			$("#aPOAuthEnq").attr("href", AuthEnqURL);

			var RecievedURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.recievedURL + "&VesselId=" + $('#VesselId').val();
			$("#aPOReieved").attr("href", RecievedURL);

		}
	});
}

function BindOpexSummary() {
	var request =
	{
		"VesselId": $('#VesselId').val(),
	}

	$.ajax({
		url: "/Dashboard/GetOpexDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},

		success: function (data) {
			$('#spanOpexBudget').text(NumberToString(data.budget, 0, 1));
			$('#spanOpexActual').text(NumberToString(data.actual, 0, 1));
			$('#spanOpexAccrual').text(NumberToString(data.accurals, 0, 1));
			$('#spanOpexTotal').text(NumberToString(data.total, 0, 1));
			$('#spanOpexVariance').text(NumberToString(data.variance, 0, 1));
			$('#spanDashboardCostCurrency').text(data.dashboardCostCurrency);
			$('#spanDashboardDrillDownDate').text(data.dashboardDrillDownPeriod);

			if (data.variance < 0) {
				$('#divOpexVariance').addClass('text-red');
			}
			else {
				$('#divOpexVariance').addClass('text-success');
			}
			//url here
			var OpexURL = "/Finance/List/?OperationCostRequestUrl=" + data.opexDashboardUrl + "&VesselId=" + $('#VesselId').val();
			$("#aOpexViewMore").attr("href", OpexURL);

			var aOpexTitle = OpexURL;
			$("#aOpexTitle").attr("href", aOpexTitle);

			var barChartOpexCanvas = $('canvas[name = "barChartOpex"]').get(0).getContext('2d')
			var barChartOpexData = {
				labels: ["Budget", "Actuals", "Accrual"],
				datasets: [{
					label: '',
					data: [data.budget, data.actual, data.accurals],
					backgroundColor: ["#2e6e84", "#79cce0", "#4596b2"],
					barThickness: 10
				}]
			}

			var barChartOpexOptions = {
				responsive: true,
				maintainAspectRatio: false,
				datasetFill: false,
				title: {
					display: true,
					text: '',
					padding: 0,
					fontColor: '#495057',
					fontSize: 16,
					fontStyle: 'lighter',
				},
				tooltips: {
					enabled: false
				},
				legend: {
					display: false,
					margin: 0,
					padding: 0,
					labels: {
						boxWidth: 20,
					}
				},
				scales: {
					xAxes: [{
						stacked: true,
						gridLines: {
							display: false,
						},
						ticks: {
							display: false,
						},
					}],
					yAxes: [{
						stacked: true,
						gridLines: {
							display: false,
						},
						ticks: {
							display: false,
						},
					}]
				},
			}
			var barChartOpex = new Chart(barChartOpexCanvas, {
				type: 'horizontalBar',
				data: barChartOpexData,
				options: barChartOpexOptions
			})
		}
	});
}

function BindVesselDetailsSummary() {
	$.ajax({
		url: "/Dashboard/GetVesselDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": $('#VesselId').val()
		},

		success: function (data) {
			if (data != null) {
				$('#spanVesselName').text(data.name);
				$('#spanVesselImo').text(data.imo);
				$('#spanVesselFlag').text(data.flag);
				$('#spanVesselType').text(data.type);
				$('#spanVesselBuiltDate').text(data.vesselBuiltDate);
				$('#spanVesselAge').text(data.vesselAge);
				if (data.image != null) {
					$("#imgvesselpicture").attr("src", "data:image/png;base64," + data.image + "");
				}
				else {
					$("#imgvesselpicture").attr("src", "/images/NoImageAvailable.png");
				}
			}
		}
	});
}

function BindingVesselOfficerDetails() {
	$.ajax({
		url: "/Dashboard/GetVesselOfficeDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": $('#VesselId').val()
		},

		success: function (data) {
			if (data != null) {
				$('#spanVesselChiefEngg').text(data.vesselChiefEnggName);
				$('#spanVesselMaster').text(data.vesselMasterName);
			}
		}
	});
}

function NumberToString(number, toFixedValue, state) {
	var numToString = "";
	if (number != null && number != '' && number != 'undefined') {
		numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
			{
				minimumFractionDigits: 0
			});
	}
	else {
		if (state == 1) {
			//when expected return is 0.00
			numToString = "0";
		}
		else if (state == 2) {
			//when blank space is expected
			numToString = "";
		}
	}
	return numToString;
}

function BindVoyageReporting() {
	$.ajax({
		url: "/Dashboard/GetVoyageLandingPageDetail",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": $('#VesselId').val()
		},

		success: function (data) {
			if (data != null) {
				if (data.isFromToVisible) {
					$("#divFromSection").show();
					$("#divToSection").show();

					$('#spanFoap').text(data.fromFAOPValue);
					$('#spanFromPortName').text(data.fromPortName);
					$('#spanFromHeaderSection').text(data.fromHeaderSection);
					$('#spanSeaPassageFromDate').text(data.fromDate);

					$('#spanToHeaderSection').text(data.toHeaderSection);
					$('#spanToEosp').text(data.toEOSPValue);
					$('#spanToPortName').text(data.toPortName);
					$('#spanSeaPassageToDate').text(data.toPortDate);

				}
				else {
					$("#divFromSection").hide();
					$("#divToSection").hide();
				}

				GetCharterHeaderDetails(data.isSeaPassageEvent, data.requestURL, data.isNoCharter);
				LoadProgressBar(data);
				LoadCharterersOrder(data);
				LoadPortServiceModal(data);
				//only for port call
				if (!data.isSeaPassageEvent) {
					LoadVoyageVesselStatus(data);
				}

				//load agent details
				var AgentDetailElement = document.getElementById('PortAgentDetails');
				if (data.isAgentAvailable) {
					AgentDetailElement.style.display = "";
					$('#VoyageReportingRequestUrl').val(data.requestURL);
				}
				else {
					AgentDetailElement.style.display = "none";
				}

				//setting up url				
				if (data.isSeaPassageEvent) {
					var seaPassageNavigation = "/VoyageReporting/SeaPassageEvent/?VoyageReportingRequestUrl=" + data.requestURL + "&VesselId=" + $('#VesselId').val();
					$("#aCharterNavigation").attr("href", seaPassageNavigation);
				}
				else {
					var portCallNavigation = "/VoyageReporting/PortCallLocationEvent/?VoyageReportingRequestUrl=" + data.requestURL + "&VesselId=" + $('#VesselId').val();
					$("#aCharterNavigation").attr("href", portCallNavigation);
				}
				var viewMoreNavigation = "/VoyageReporting/VesselActivityList/?VoyageReportingRequestUrl=" + data.requestURL + "&VesselId=" + $('#VesselId').val();

				$("#aCurrentVoyageViewMore").attr("href", viewMoreNavigation);

			}
		}
	});
}

function LoadVoyageVesselStatus(data) {
	$("#spanEospDate").text(data.eospDate);
	$("#spanEospDateHeader").text(data.eospDateHeader);
	$("#spanBerthDate").text(data.berthDate);
	$("#spanBerthDateHeader").text(data.berthDateHeader);
	$("#spanUnBerthDate").text(data.unBerthDate);
	$("#spanUnBerthDateHeader").text(data.unBerthDateHeader);
	$("#spanFaopDateHeader").text(data.faopDateHeader);
	$("#spanFaopDate").text(data.faopDate);
}

function LoadCharterersOrder(data) {
	if (!data.isSeaPassageEvent) {
		$("#divCharterersOrder").hide();

		if ($("#cdCertificate").hasClass('col-xl-6')) {
			$("#cdCertificate").removeClass('col-xl-6');
		}
		$("#cdCertificate").addClass('col-xl-12');

		if ($("#colCertificate").hasClass('col-md-12')) {
			$("#colCertificate").removeClass('col-md-12');
		}
		$("#colCertificate").addClass('col-md-6 col-lg-12 col-xl-6 pr-10 vessel-details-new-padding');

		if ($("#colOpenOrder").hasClass('col-md-12')) {
			$("#colOpenOrder").removeClass('col-md-12');
		}
		$("#colOpenOrder").addClass('col-md-6 col-lg-12 col-xl-6');

		$('.equal-height-CertificateOpenOrder').matchHeight();
	}
	else {

		$("#aCharterersOrderExternal").attr("href", "/VoyageReporting/SeaPassageEvent?VesselId=" + $('#VesselId').val() + "&VoyageReportingRequestUrl=" + data.requestURL);
		SeaPassageEventDetails(data.requestURL);
	}
}

function ConvertValueToPercentage(value, max) {
	return parseFloat((value * 100) / max).toFixed(2);
}

function LoadPortServiceModal(data) {
	var FromAnchorElement = document.getElementById('FromAnchorPortAlert');
	if (data.hasFromPortAlert) {
		FromAnchorElement.style.display = "";
		$("#FromAnchorPortAlert").attr("url-data", data.fromRequestURL);
	}
	else {
		FromAnchorElement.style.display = "none";
	}

	var ToAnchorPortElement = document.getElementById('ToAnchorPortAlert');
	if (data.isToPortAlertVisible) {
		ToAnchorPortElement.style.display = "";
		$("#ToAnchorPortAlert").attr("url-data", data.toRequestURL);
	}
	else {
		ToAnchorPortElement.style.display = "none";
	}

}

function LoadProgressBar(data) {
	$('#spanSPTotalDistance').text(ConvertDecimalNumberToString(data.totalDistance, 2, 1, 2) + " nm");
	$('#spanSPDistanceCovered').text(ConvertDecimalNumberToString(data.distanceTravelled, 2, 1, 2) + " nm");

	if (data.isSeaPassageEvent == true) {
		var endPositionOfVessel = ConvertValueToPercentage(data.distanceTravelled, data.totalDistance);
		var progressBarEndContent = "AT SEA" + "<br/>" + data.lastEventPosition + "<br/>" + data.remainingValue + " nm remaining";
		$('.divProgressBarFlow').css('width', endPositionOfVessel + '%');
		$("#divProgressBar").append('<div class="progress-bar bg-default" role="progressbar" aria-valuenow="7" aria-valuemin="0" aria-valuemax="100" style="left: ' + endPositionOfVessel + '%;" data-toggle="tooltip" data-placement="bottom" title="' + progressBarEndContent + '" data-html="true"></div>');
		$('[data-toggle="tooltip"]').tooltip();
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
					$("#divProgressBar").append('<a class="badweather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '> <div class="progress-bar bg-default bg-purple-speed" role="progressbar" aria-valuenow="7" aria-valuemin="0" aria-valuemax="100" style="left: ' + badWeatherPosition + '%;top:-63% "></div> </a>');
				}
				else {
					$("#divProgressBar").append('<a class="badweather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '> <div class="progress-bar bg-default bg-purple-speed" role="progressbar" aria-valuenow="7" aria-valuemin="0" aria-valuemax="100" style="left: ' + badWeatherPosition + '%"></div> </a>');
				}
			}

			//this is for off hire
			//IsOnlyBreakAlert || IsBreakAndBadWeatherAlert uses - BreakDetailsTemplate
			if (data.badWeatherDetails[i].isOnlyBreakAlert == true || data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
				var offHirePosition = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

				$("#divProgressBar").append('<a class="offhire" href="javascript:void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '> <div class="progress-bar bg-default bg-stop" role="progressbar" aria-valuenow="7" aria-valuemin="0" aria-valuemax="100" style="left: ' + offHirePosition + '%;"></div> </a>');
			}

			//IsOnlyPortBadWeatherAlert - uses PortBadWeathersDetailsTemplate
			if (data.badWeatherDetails[i].isOnlyPortBadWeatherAlert == true) {
				var PortBadWeathersDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

				$("#divProgressBar").append('<a class="portBadWeather" href="javascript:void(0);" id=' + data.requestURL + '> <div class="progress-bar bg-default bg-purple-speed" role="progressbar" aria-valuenow="7" aria-valuemin="0" aria-valuemax="100" style="left: ' + PortBadWeathersDetail + '%"></div> </a>');
			}

			//IsOnlyDelayAlert - use DelaysDetailsTemplate
			if (data.badWeatherDetails[i].isOnlyDelayAlert == true) {
				var DelaysDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

				$("#divProgressBar").append('<a class="portDelay" href="javascript:void(0);" id=' + data.requestURL + '> <div class="progress-bar bg-default bg-stop" role="progressbar" aria-valuenow="7" aria-valuemin="0" aria-valuemax="100" style="left: ' + DelaysDetail + '%;"></div> </a>');
			}
		}

	}
}

function GetCharterHeaderDetails(isSeaPassageEvent, requestURL, isNoCharter) {
	if (isSeaPassageEvent == true) {
		$.ajax({
			url: "/Dashboard/GetSeaPassageDetails",
			type: "POST",
			"data": {
				"input": requestURL,
			},
			"datatype": "JSON",
			success: function (result) {
				var data = result.data;
				$('#spanCharterName').text(data.charterName);
				$('#spanVoyageNumber').text(data.voyageNumber);

				if (isNoCharter) {
					$('#spanCharterNo').text('No Charter');
				}
				else {
					$('#spanCharterNo').text(data.charterNumber);
				}
			}
		});
	}
	else if (isSeaPassageEvent == false) {
		$.ajax({
			url: "/Dashboard/GetPortCallDetail",
			type: "POST",
			"data": {
				"input": requestURL,
			},
			"datatype": "JSON",
			success: function (result) {
				var data = result.data;
				$('#spanCharterName').text(data.charterName);
				$('#spanVoyageNumber').text(data.voyageNumber);

				if (isNoCharter) {
					$('#spanCharterNo').text('No Charter');
				}
				else {
					$('#spanCharterNo').text(data.charterNumber);
				}
			}
		});
	}
}

function SeaPassageEventDetails(requestURL) {
	$.ajax({
		url: "/Dashboard/SeaPassageEventDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"input": requestURL
		},
		success: function (data) {
			if (data != null) {
				maxSpeed = data.speedStatistics.maxSpeed;
				RenderRadialChartForSeaPassageSpeed(data.speedStatistics.firstCount, data.speedStatistics.secondCount, data.speedStatistics.charterName);
				RenderBarChart(data.barChartStats);
			}
		}
	});
}

function valueToPercent(value) {
	return parseFloat((value * 100) / maxSpeed).toFixed(2);
}

function PercentageToValue(value) {
	return parseFloat((maxSpeed * value) / 100).toFixed(2);
}

function RenderRadialChartForSeaPassageSpeed(charterSpeed, actualSpeed, charterName) {
	var options = {
		series: [valueToPercent(charterSpeed), valueToPercent(actualSpeed)],
		chart: {
			height: 325,
			type: 'radialBar',
		},
		plotOptions: {
			radialBar: {
				offsetY: -45,
				offsetX: 0,
				startAngle: -135,
				endAngle: 135,
				hollow: {
					margin: 3,
					size: '30%',
					background: 'transparent',
				},
				dataLabels: {
					name: {
						show: false,
					},
					value: {
						show: false,
					}
				}
			}
		},
		colors: ['#1ab7ea', '#0084ff'],
		labels: [charterName, 'Actual'],
		legend: {
			show: true,
			floating: false,
			fontSize: '1px',
			position: 'bottom',
			horizontalAlign: 'center',
			fontFamily: 'Open Sans',
			offsetX: -25,
			offsetY: 10,
			labels: {
				useSeriesColors: true,
			},
			markers: {
				size: 0
			},
			formatter: function (seriesName, opts) {
				return seriesName + ":  " + PercentageToValue(opts.w.globals.series[opts.seriesIndex])
			},
			itemMargin: {
				vertical: 1,
				horizontal: 2
			}
		},
		title: {
			text: 'Speed',
			align: 'center',
			offsetX: 0,
			offsetY: 5,
			style: {
				fontSize: '11px',
				fontWeight: 'bold',
				color: '#263238'
			},
		},
		responsive: [{
			breakpoint: 480,
			options: {
				legend: {
					show: true,
					offsetY: 20,
					floating: false,
					fontSize: '1px',
					fontFamily: 'Helvetica, Arial',
					fontWeight: 20,
				},
				chart: {
					offsetX: 0,
					offsetY: 5,
					height: 210
				},
			}
		}]
	};

	var chart = new ApexCharts(document.querySelector("#seaPassageSpeedRadialChart"), options);
	chart.render();
}


//graph js charter order fuel-consuption
function RenderBarChart(barChartStats) {
	//get the bar chart canvas
	var ctx = $("#fuel-consuption");
	//bar chart data
	var data = {
		labels: [barChartStats.firstItemLabelName, barChartStats.secondItemLabelName],
		datasets: [
			{
				label: barChartStats.charterName,
				data: [barChartStats.firstItemCharterValue, barChartStats.secondItemCharterValue],
				backgroundColor: ["#d38646", "#d38646"],
				barPercentage: 0.5,
				barThickness: 20,
				maxBarThickness: 20,
				minBarLength: 2,
			},
			{
				label: "Actual",
				data: [barChartStats.firstItemActualValue, barChartStats.secondItemActualValue],
				backgroundColor: ["#775b5f", "#775b5f"],
				barPercentage: 0.5,
				barThickness: 20,
				maxBarThickness: 20,
				minBarLength: 2,
			}
		]
	};
	//options
	var options = {
		tooltips: {
			enabled: false
		},
		layout: {
			padding: {
				left: 10,
				right: 10,
				top: 0,
				bottom: 0
			}
		},
		hover: {
			animationDuration: 1
		},
		animation: {
			duration: 1,
			onComplete: function () {
				var chartInstance = this.chart,
					ctx = chartInstance.ctx;
				ctx.textAlign = 'center';
				ctx.fillStyle = "rgba(0, 0, 0, 1)";
				ctx.textBaseline = 'bottom';
				this.data.datasets.forEach(function (dataset, i) {
					var meta = chartInstance.controller.getDatasetMeta(i);
					meta.data.forEach(function (bar, index) {
						var data = dataset.data[index];
						ctx.fillText(data, bar._model.x, bar._model.y - 5);
					});
				});
			}
		},
		responsive: true,
		maintainAspectRatio: false,
		title: {
			display: true,
			position: "top",
			text: "Fuel Consuption (Daily Avg)",
			fontSize: 10
		},
		legend: {
			display: true,
			position: "bottom",
			labels: {
				fontColor: "#333",
				fontSize: 12
			}
		},
		scales: {
			yAxes: [{
				ticks: {
					max: 40,
					min: 0,
					stepSize: 10
				}
			}]
		},
		plugins: {
			datalabels: {
				display: true,
				align: 'center',
				anchor: 'center'
			}
		},
	};
	//create Chart class object
	var chart = new Chart(ctx, {
		type: "bar",
		data: data,
		options: options
	});
}


function BindCrewSummary() {
	$.ajax({
		url: "/Dashboard/GetCrewSummary",
		type: "POST",
		"data": {
			"input": $('#VesselId').val(),
		},
		"datatype": "JSON",
		success: function (data) {

			$("#spanCrewOnboard").text(data.onboardCount);
			$("#spanCrewOverdue").text(data.overdueCount);
			$("#spanCrewUnplannedBerth").text(data.unplannedBerthCount);
			$("#spanCrewTop4SignOn").text(data.top4SignOnCount);
			$("#spanCrewMedicalSignOff").text(data.medicalSignOffCount);

			//settig up url			
			var viewMoreNav = "/Crew/List/?CrewList=" + data.viewMoreURL + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=true";
			$("#aCrewViewMore").attr("href", viewMoreNav);

			$("#aCrewTitle").attr("href", viewMoreNav);

			var onBoardNav = "/Crew/List/?CrewList=" + data.onboardURL + "&VesselId=" + $('#VesselId').val();
			var overDueNav = "/Crew/List/?CrewList=" + data.overdueURL + "&VesselId=" + $('#VesselId').val();
			var unPlannedBerth = "/Crew/List/?CrewList=" + data.unplannedBerthURL + "&VesselId=" + $('#VesselId').val();
			var top4SignNav = "/Crew/List/?CrewList=" + data.top4SignOnURL + "&VesselId=" + $('#VesselId').val();
			var medicalSignOffNav = "/Crew/MedicalSignOffList/?CrewList=" + data.medicalSignOffURL + "&VesselId=" + $('#VesselId').val();

			$("#aOnboardURL").attr("href", onBoardNav);
			$("#aOverDueURL").attr("href", overDueNav);
			$("#aUnplannedBerth").attr("href", unPlannedBerth);
			$("#aTop4SignOn").attr("href", top4SignNav);
			$("#aMedicalSignOff").attr("href", medicalSignOffNav);

			$('.height-equal').matchHeight();
		}
	});
}

function BindDefectManager() {
	var date = new Date();
	var start = 1 + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
	var end = moment().format("DD MMM YYYY");

	var request =
	{
		"EncryptedVesselId": $('#VesselId').val(),
		"ToDate": end,
		"FromDate": start
	}
	$.ajax({
		url: "/Dashboard/GetDefectDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},

		success: function (data) {
			if (data != null) {
				$('#spanDefectDue').text(data.dueCount);
				$('#spanDefectOD').text(data.overdueCount);
				$('#spanDefectOpen').text(data.openDefectCount);
				$('#spanDefectClosed').text(data.closedDefectCount);
				$('#spanDefectOffHireReq').text(data.offHireRequiredCount);
				$('#spanDefectOpenOrders').text(data.orderCount);

				//setting up url
				var viewMoreNav = "/Defect/List/?DefectRequest=" + data.allNavigation + "&VesselId=" + $('#VesselId').val() + "&IsViewMore=true"
				$("#aDefectViewMore").attr("href", viewMoreNav);

				var titleNav = viewMoreNav;
				$("#aDefectTitle").attr("href", titleNav);

				var dueNavigation = "/Defect/List/?DefectRequest=" + data.dueNavigation + "&VesselId=" + $('#VesselId').val();
				$("#aDefectDue").attr("href", dueNavigation);

				var overdueNavigation = "/Defect/List/?DefectRequest=" + data.overdueNavigation + "&VesselId=" + $('#VesselId').val();
				$("#aDefectOD").attr("href", overdueNavigation);

				var openDefectNavigation = "/Defect/List/?DefectRequest=" + data.openDefectNavigation + "&VesselId=" + $('#VesselId').val();
				$("#aDefectOpen").attr("href", openDefectNavigation);

				var closedDefectNavigation = "/Defect/List/?DefectRequest=" + data.closedDefectNavigation + "&VesselId=" + $('#VesselId').val();
				$("#aDefectClosed").attr("href", closedDefectNavigation);

				var offHireNavigation = "/Defect/List/?DefectRequest=" + data.offHireNavigation + "&VesselId=" + $('#VesselId').val();
				$("#aDefectOffHireReq").attr("href", offHireNavigation);

				var orderNavigation = "/Defect/List/?DefectRequest=" + data.orderNavigation + "&VesselId=" + $('#VesselId').val();
				$("#aDefectOpenOrders").attr("href", orderNavigation);
			}
		}
	});
}