import GMaps from "gmaps";
import moment from "moment";
require('bootstrap');

import { AjaxError, ConvertDecimalNumberToString, ConvertValueToPercentage, GetCookie, ToastrAlert, GetDashboardColorMap, GetStringNullOrWhiteSpace, BackButton, NavigateToNotification, datepickerheightinmobile } from "../common/utilities.js";
import { GetCellData, GetFormattedDecimal, GetFormattedDateTime } from "../common/datatablefunctions.js";
import { OpenModalAgentDetails, OpenModalPortServices, LoadBadWeather, LoadOffHireDetails, LoadPortBadWeather, LoadPortDelay } from "../master/voyageReportingModal.js";
import { VoyageReportingListPageKey, MobileScreenSize } from "../common/constants.js"
import { UpdateNotesTab } from "../common/notesUtilities"
var startDate, endDate;
var dtVoyageActivitiesList, weatherDetailsGrid, dtDelayList, dtCharterRequirementList, dtOffHireList;
var columnIndexBallast = 1, columnIndexLoaded = 2;

var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
	'<div class="loader  mx-auto">' +
	'<div class="ball-clip-rotate">' +
	'<div></div>' +
	'</div>' +
	'</div>' +
	'</div>';

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
})
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

$(document).on('click', '.noteListAnchorOnClick', function () {
	let messageDetailsJSONstr = $(this).data("messagejson");
	var messageDetailsJSON = JSON.parse(decodeURIComponent(messageDetailsJSONstr));
	$("#hdnNotesMessageDetails").val(JSON.stringify(messageDetailsJSON));
	UpdateNotesTab();
	$(".notes-sidebar-open").toggleClass("settings-open");
	$("#tabs a[href='#tabs1']")[0].click();
});

$(document).ready(function () {
	AjaxError();
	//daterangepicker
	var start = moment($('#FromDate').val(), 'DD-MM-YYYY');
	var end = moment($('#ToDate').val(), 'DD-MM-YYYY');

	//Sidebar back
	BackButton(VoyageReportingListPageKey, true);


	$("#dtrvesselpositionlist").caleran(
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
				setDateDetails(startDate, endDate, true);
			}
		}
	);


	setDateDetails(start, end, false);


	let currentActivity = document.getElementsByClassName("current-activity-active")[0];
	currentActivity.scrollIntoView(false);

	$("div[data-type='Port'][data-hasPortAlert='True']").each(function () {
		let portId = $(this).data("portid");
		BindPortAlerts(this, portId, $('#EncryptedVesselDetail').val());
	});

	$("div[data-type='SeaPassage']").each(function () {
		let posId = $(this).data("activity");
		BindSeaPassage(this, posId, $('#EncryptedVesselDetail').val());
		BindPerformanceSummary($('#EncryptedVesselDetail').val(), posId, this)
	});
});


function setDateDetails(start, end, isSearch) {
	startDate = start.format("DD MMM YYYY");
	endDate = end.format("DD MMM YYYY");
	$("#dtrvesselpositionlist").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
	if (isSearch) {
		SetPageParameter();
	}
}


function BindSeaPassage(parent, posId, vesselId) {
	$.ajax({
		url: "/VoyageReporting/GetSeaPassageGraphDetails",
		type: "POST",
		data: {
			"posId": posId,
			"vesselId": vesselId
		},
		datatype: "JSON",
		beforeSend: function (xhr) {
			$(parent).find('.port-activity-detail').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			if (data != null) {
				if (data.fromPortName != null) {
					$(parent).find('.voyage-divFromSection').show();
					$(parent).find('.spanFoap').text(data.fromEventType);
					let fullFromPortName = GetStringNullOrWhiteSpace(data.fromPortName) + ', ' + GetStringNullOrWhiteSpace(data.fromCountryCode);
					$(parent).find('.spanFromPortName').text(fullFromPortName);
					if (data.fromDate != null) {
						$(parent).find('.spanSeaPassageFromDate').text(moment(new Date(data.fromDate)).format("D MMM YYYY, HH:mm"));
					}

				} else {
					$(parent).find('.voyage-divFromSection').hide();
				}

				if (data.toPortName != null) {
					$(parent).find('.voyage-divToSection').show();
					$(parent).find('.spanToEosp').text(data.toEventType);
					let fullToPortName = GetStringNullOrWhiteSpace(data.toPortName) + ', ' + GetStringNullOrWhiteSpace(data.toCountryCode);
					$(parent).find('.spanToPortName').text(fullToPortName);
					if (data.toDate != null) {
						$(parent).find('.spanSeaPassageToDate').text(moment(new Date(data.toDate)).format("D MMM YYYY, HH:mm"));
					}
				} else {
					$(parent).find('.voyage-divToSection').hide();
				}

				LoadProgressBar(data, parent);
			}
		},
		complete: function () {
			if (screen.width < MobileScreenSize) {
				$('.equalheightport').matchHeight();
			}
			$(parent).find('.port-activity-detail').unblock();
		}
	});
}

function BindPortAlerts(parent, portId, vesselId) {
	$.ajax({
		url: "/VoyageReporting/GetVesselActivitiesPortAlert",
		type: "POST",
		data: {
			"vesselId": vesselId,
			"portId": portId
		},
		datatype: "JSON",
		beforeSend: function (xhr) {
			$(parent).find('.portAlertSection').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			if (data != null) {
				$(parent).find('.spanAlertCount').text(data.length);
				for (var i = 0; i < data.length; i++) {
					var obj = data[i];
					$(parent).find('.port-message-red').append(obj.title + '<br class="d-block d-lg-block d-xl-block" />');
				}
			}
		},
		complete: function () {
			$(parent).find('.portAlertSection').unblock();
		}
	});
}

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
		$(parent).find('.at-location').attr('title', progressBarEndContent);
		$(parent).find('[data-toggle="tooltip"]').tooltip();
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

function BindPerformanceSummary(vesselId, posId, parent) {

	var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
	var endDate = moment().format("DD MMM YYYY");

	var request =
	{
		"VesselId": vesselId,
		"PosId": posId,
		"StartDate": null,
		"EndDate": null,
	}

	$.ajax({
		url: "/Dashboard/GetPerformanceSummary",
		type: "POST",
		"data": {
			"request": request,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(parent).find(".performance-section").block({
				message: $(" " + loadercontent),
			});
		},
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
				//$(parent).find(".cpOrdersSpeed").addClass(colorMap.get(data.cpOrdersSpeedPriority).textColor);
				//$(parent).find(".cpOrdersSpeed").addClass(colorMap.get(data.cpOrdersSpeedBackgroundPriority).color);

				$(parent).find(".cpOrdersConsumption-text").text(data.cpOrdersConsumption);
				//$(parent).find(".cpOrdersConsumption").addClass(colorMap.get(data.cpOrdersConsumptionPriority).textColor);
				//$(parent).find(".cpOrdersConsumption").addClass(colorMap.get(data.cpOrdersConsumptionBackgroundPriority).color);  
			}
		},
		complete: function () {
			$(parent).find(".performance-section").unblock();
		}
	});
}

function OpenModalOffHireDetails(input) {

	$('#dtOffHireList').DataTable().destroy();
	dtOffHireList = $('#dtOffHireList').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"order": [],
		"ajax": {
			"url": "/VoyageReporting/GetOffHireByPosId",
			"type": "POST",
			"data":
			{
				"input": input
			},
			"datatype": "json"
		},
		"language": {
			"emptyTable": "No data available.",
		},
		"columns": [
			{
				className: "tdblock",
				data: "activity",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Activity', data); }
			},
			{
				className: "text-center",
				data: "offHireDuration",
				orderable: false,
				render: function (data, type, full, meta) {
					if (type === "display") {
						if (data != null && data.hasValue == true) {
							return GetCellData('Offhire Duration', full.offHireDurationHours + ":" + full.offHireDurationMinutes);
						}
						return GetCellData('Offhire Duration', "");
					}
					return data;
				}
			},
			{
				className: "text-center",
				data: "dateFrom",
				orderable: false,
				type: "date",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetFormattedDateTime(type, 'From', data);
					}
					return data;
				}
			},
			{
				className: "text-center tdblock",
				data: "dateTo",
				orderable: false,
				type: "date",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetFormattedDateTime(type, 'To', data);
					}
					return data;
				}
			},
		]
	});

}

function OpenModalDelayDetails(input) {

	$('#dtDelayList').DataTable().destroy();
	dtDelayList = $('#dtDelayList').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"order": [],
		"ajax": {
			"url": "/VoyageReporting/GetDelays",
			"type": "POST",
			"data":
			{
				"input": input
			},
			"datatype": "json"
		},
		"language": {
			"emptyTable": "No delays available.",
		},
		"columns": [
			{
				className: "tdblock",
				data: "activityDescription",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Activity', data); }
			},
			{
				className: "text-center",
				data: "delayDuration",
				orderable: false,
				render: function (data, type, full, meta) {
					if (type === "display") {
						if (data != null && data.hasValue == true) {
							return GetCellData('Delay Duration', full.delayDurationHours + ":" + full.delayDurationMinutes);
						}
						return GetCellData('Delay Duration', "");
					}
					return data;
				}
			},
			{
				className: "text-center",
				data: "dateFrom",
				orderable: false,
				type: "date",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetFormattedDateTime(type, 'From', data);
					}
					return data;
				}
			},
			{
				className: "text-center tdblock",
				data: "dateTo",
				orderable: false,
				type: "date",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetFormattedDateTime(type, 'To', data);
					}
					return data;
				}
			},
		]
	});

}

function OpenModalWeatherDetails(data) {

	$('#spanSwellLength').text('');
	$('#spanWindForce').text('');
	$('#dtWeatherList').DataTable().destroy();

	$.ajax({
		url: "/VoyageReporting/GetBadWeatherDetail",
		type: "POST",
		"data": {
			"input": data.requestURL,
		},
		"datatype": "JSON",
		success: function (result) {
			var data = result.data;
			$('#spanSwellLength').text(data.charterSwellLength);
			$('#spanWindForce').text(data.charterWindForce);
			LoadWeatherDetailsList(data.badWeatherList);
		}
	});
}

function LoadWeatherDetailsList(weatherdata) {

	weatherDetailsGrid = $('#dtWeatherList').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"order": [],
		"data": weatherdata,
		"language": {
			"emptyTable": "No weather details available.",
		},
		"columns": [
			{
				className: "text-left",
				data: "eventName",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Event Name', data); }
			},
			{
				className: "text-center",
				data: "eventDate",
				orderable: false,
				type: "date",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetFormattedDateTime(type, 'Date', data);
					}
					return data;
				}
			},
			{
				className: "text-left",
				data: "maxSwellLengthDscription",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Swell Length', data); }
			},
			{
				className: "text-left",
				data: "maxWindForce",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Wind Force', data); }
			},
		]
	});

}

function OpenModalCharterDetails(input) {

	$('#spanChartererName').text('');
	$('#spanVoyageNumber').text('');
	$('#spanChartererNumber').text('');
	$('#spanTrade').text('');
	$('#dtCharterRequirementList').DataTable().destroy();

	if (input.isSeaPassageEvent == true) {

		document.getElementById('divCharterRequirement').style.display = "block";

		$.ajax({
			url: "/VoyageReporting/GetSeaPassageDetails",
			type: "POST",
			"data": {
				"input": input.requestURL,
			},
			"datatype": "JSON",
			success: function (result) {
				var data = result.data;
				$('#spanChartererName').text(data.charterName);
				$('#spanVoyageNumber').text(data.voyageNumber);
				$('#spanChartererNumber').text(data.charterNumber);
				$('#spanTrade').text(data.trade);
				LoadCharterRequiremnets(data.charterRequirementsList, input.isVesselLoadedFlag);
			}
		});
	}
	else if (input.isSeaPassageEvent == false) {

		document.getElementById('divCharterRequirement').style.display = "none";

		$.ajax({
			url: "/VoyageReporting/GetPortCallDetail",
			type: "POST",
			"data": {
				"input": input.requestURL,
			},
			"datatype": "JSON",
			success: function (result) {
				var data = result.data;
				$('#spanChartererName').text(data.charterName);
				$('#spanVoyageNumber').text(data.voyageNumber);
				$('#spanChartererNumber').text(data.charterNumber);
				$('#spanTrade').text(data.trade);
			}
		});
	}
}

function LoadCharterRequiremnets(charterdata, isVesselLoadedFlag) {

	dtCharterRequirementList = $('#dtCharterRequirementList').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"order": [],
		"data": charterdata,
		"language": {
			"emptyTable": "No requirements available.",
		},
		'columnDefs': [
			{ 'visible': false, 'targets': [columnIndexBallast, columnIndexLoaded] }
		],
		"columns": [
			{
				className: "tdblock",
				data: "fuelType",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Type', data); }
			},
			{
				className: "text-right",
				data: "ballastValue",
				type: "html-num",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetFormattedDecimal(type, 'Cht. Ballast', data, 2, '0.00');
				}
			},
			{
				className: "text-right",
				data: "loadedValue",
				type: "html-num",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetFormattedDecimal(type, 'Cht. Loaded', data, 2, '0.00');
				}
			},
			{
				className: "text-right",
				data: "actualValue",
				type: "html-num",
				orderable: false,
				render: function (data, type, full, meta) {
					if (full.isCritical == true) {
						return GetCellData('Actual', "<span class='text-danger'>" + GetFormattedDecimal(null, null, data, 2, '0.00') + "</span>");
					}
					else {
						return GetCellData('Actual', GetFormattedDecimal(null, null, data, 2, '0.00'));
					}
				}
			}
		]
	});

	if (isVesselLoadedFlag == false) {
		dtCharterRequirementList.columns([columnIndexBallast]).visible(true);
	}
	else if (isVesselLoadedFlag == true) {
		dtCharterRequirementList.columns([columnIndexLoaded]).visible(true);
	}
}

function GetPortName(type, label, portName, hasPortAlert, portClass, alertClass) {
	var data = portName;
	if (type === "display") {
		if (portName != null && portName != "" && portName != undefined) {
			data = "<a href='javascript: void(0);' class='" + portClass + "'>" + portName + "</a>";
		}
		if (hasPortAlert == true) {
			data = data + "<a href='javascript: void(0);' class='icon-hover-underline-none " + alertClass + "'><i class='fa fa-anchor icon-red ml-2'></i><sup class='icon-red'>&#9733;</sup></a>";
		}
		return GetCellData(label, data);
	}
	return data;
}

function SetPageParameter() {
	var request =
	{
		"menuType": $('#MenuType').val(),
		"fromDate": startDate,
		"toDate": endDate,
		"encryptedVesselDetail": $('#EncryptedVesselDetail').val(),
		"positionListId": $('#PositionListId').val()
	};

	$.ajax({
		url: "/VoyageReporting/SetPageParameter",
		type: "POST",
		"data": request,
		success: function (data) {
			window.location.href = data;			
		}
	});
}
