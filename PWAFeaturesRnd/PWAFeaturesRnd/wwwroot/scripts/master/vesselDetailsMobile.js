import moment from "moment";
import "select2/dist/js/select2.full.js";
import { AjaxError, ConvertDecimalNumberToString, ConvertValueToPercentage, GetCookie, ToastrAlert, GetDashboardColorMap, IsNullOrEmpty, SetValueElseDefault, IsNullOrEmptyOrUndefinedLooseTyped, IsNullOrEmptyOrUndefined, RemoveModelLoadingIndicator } from "../common/utilities.js";
import { OpenModalPortServices, LoadBadWeather, LoadOffHireDetails, LoadPortBadWeather, LoadPortDelay } from "../master/voyageReportingModal.js";
import { GetCellData, GetFormattedDateTime, GetFormattedDecimal } from "../common/datatablefunctions.js";
require('bootstrap');
import { IMO_Prefix, MobileScreenSize, EmailTypeCategoryId, PhoneTypeCategoryId, OtherTypeCategoryId, Amber, Green, Red } from "../common/constants.js";

var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
	'<div class="loader  mx-auto">' +
	'<div class="ball-clip-rotate">' +
	'<div></div>' +
	'</div>' +
	'</div>' +
	'</div>';

var colorMap = GetDashboardColorMap();

$(document).on('click', '.showFromAgentDetails, .showToAgentDetails ', function () {
	let urlRequest = $(this).data('encryptedposid');
	let portname = decodeURIComponent($(this).data('portname'));
	CurrentVoyageAgentDetails(urlRequest, portname);
});

$(document).on('click', '.fromAnchorPortAlertCls, .toAnchorPortAlertCls', function () {
	var requestUrl = $(this).data('url');
	OpenModalPortServices(requestUrl);
});

$(document).on('click', '.vesselDetailsMobileBadWeather', function () {
	var urlRequest = $(this).attr('id');
	LoadBadWeather(urlRequest);
});

$(document).on('click', '.vesselDetailsMobileoffhire', function () {
	var urlRequest = $(this).attr('id');
	LoadOffHireDetails(urlRequest);
});

$(document).on('click', '.vesselDetailsMobileportBadWeather', function () {
	var urlRequest = $(this).attr('id');
	LoadPortBadWeather(urlRequest);
});

$(document).on('click', '.vesselDetailsMobileportDelay', function () {
	var urlRequest = $(this).attr('id');
	LoadPortDelay(urlRequest);
});

$(document).on('click', '.agentbtncall', function () {
	var parent = $(this).siblings('.agentmenudetail').find('.agentlisting');
	parent.empty();
	$.ajax({
		url: "/Dashboard/GetVesselCommunication",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": $(this).find(".vesselcommunicationlink").data('id'),
			"typeCategoryId": PhoneTypeCategoryId
		},
		beforeSend: function (xhr) {
			AddCommunicationLoadingIndicator('.communicationcall');
		},
		success: function (data) {

			if (data != null && data.length > 0) {
				for (var i = 0; i < data.length; i++) {
					var divHtmlElement = '<div class="listagentdiv"><div class="row no-gutters mx-auto"><div class="col-1 p-0"><img src="/images/agentcallblue.svg" /></div>' +
						'<div class="col-11 p-0"><h1><a href="tel:' + data[i].comNumber.trim() + '">' + data[i].comNumber + '</a>'
					if (data[i].primaryContact != '') {
						divHtmlElement = divHtmlElement + '<button class="btn agentprimary">PRIMARY</button>'
					}
					divHtmlElement = divHtmlElement + '</h1><h2><span>Type </span>' + data[i].ctyName + '</h2><h3>' + data[i].comDesc + '</h3></div></div></div>'

					parent.append(divHtmlElement);
				}
			} else {
				parent.append('<Label>No data found.</Label>');
			}
		},
		complete: function () {
			RemoveModelLoadingIndicator('.communicationcall');
		}
	});
	$(this).siblings('.communicationcall').show();
	$(this).parent().siblings('.agentemaildropdown').find('.communicationemail').hide();
	$(this).parent().siblings('.agentoptiondropdown').find('.communicationother').hide();
});

$(document).on('click', '.agentbtnemail', function () {
	var parent = $(this).siblings('.agentmenudetail').find('.agentlisting');
	parent.empty();
	$.ajax({
		url: "/Dashboard/GetVesselCommunication",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": $(this).find(".vesselcommunicationlink").data('id'),
			"typeCategoryId": EmailTypeCategoryId
		},
		beforeSend: function (xhr) {
			AddCommunicationLoadingIndicator('.communicationemail');
		},
		success: function (data) {

			if (data != null && data.length > 0) {
				for (var i = 0; i < data.length; i++) {
					var divHtmlElement = '<div class="listagentdiv"><div class="row no-gutters mx-auto"><div class="col-1 p-0"><img src="/images/agentemailblue.svg" /></div>' +
						'<div class="col-11 p-0"><h1><a href="javascript:void(0);" class="communicationmode" data-number="' + data[i].comNumber.trim() + '">' + data[i].comNumber + '</a>'
					if (data[i].primaryContact != '') {
						divHtmlElement = divHtmlElement + '<button class="btn agentprimary">PRIMARY</button>'
					}
					divHtmlElement = divHtmlElement + '</h1><h2><span>Type </span>' + data[i].ctyName + '</h2><h3>' + data[i].comDesc + '</h3></div></div></div>'

					parent.append(divHtmlElement);
				}
			} else {
				parent.append('<Label>No data found.</Label>');
			}
		},
		complete: function () {
			RemoveModelLoadingIndicator('.communicationemail');
		}
	});
	$(this).siblings('.communicationemail').show();
	$(this).parent().siblings('.agentcalldropdown').find('.communicationcall').hide();
	$(this).parent().siblings('.agentoptiondropdown').find('.communicationother').hide();
});

$(document).on('click', '.agentbtnoptions', function () {
	var parent = $(this).siblings('.agentmenudetail').find('.agentlisting');
	parent.empty();
	$.ajax({
		url: "/Dashboard/GetVesselCommunication",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": $(this).find(".vesselcommunicationlink").data('id'),
			"typeCategoryId": OtherTypeCategoryId
		},
		beforeSend: function (xhr) {
			AddCommunicationLoadingIndicator('.communicationother');
		},
		success: function (data) {

			if (data != null && data.length > 0) {
				for (var i = 0; i < data.length; i++) {
					var divHtmlElement = '<div class="listagentdiv"><div class="row no-gutters mx-auto">' +
						'<div class="col-12 p-0"><h1 class="ml-0"><a href="javascript:void(0);">' + data[i].comNumber + '</a>'
					if (data[i].primaryContact != '') {
						divHtmlElement = divHtmlElement + '<button class="btn agentprimary">PRIMARY</button>'
					}
					divHtmlElement = divHtmlElement + '</h1><h2 class="ml-0"><span class="ml-0">Type </span>' + data[i].ctyName + '</h2><h3 class="ml-0">' + data[i].comDesc + '</h3></div></div></div>'

					parent.append(divHtmlElement);
				}
			} else {
				parent.append('<Label>No data found.</Label>');
			}
		},
		complete: function () {
			RemoveModelLoadingIndicator('.communicationother');
		}
	});
	$(this).siblings('.communicationother').show();
	$(this).parent().siblings('.agentemaildropdown').find('.communicationemail').hide();
	$(this).parent().siblings('.agentcalldropdown').find('.communicationcall').hide();
});

$(document).click(function (e) {
	var container = $('.communicationcall,.communicationemail,.communicationother,.agentbtnoptions,.agentbtnemail,.agentbtncall,.agentcalldropdown,.agentemaildropdown,.agentoptiondropdown');
	if (!container.is(e.target) && container.has(e.target).length === 0) {
		var elements = $('.communicationemail,.communicationcall,.communicationother');
		$.each(elements, function (index, item) {
			if ($(item).is(':visible')) $(item).hide();
		})
	} else {
		return true;
	}
});

$(document).on('click', '.closeagentcall', function () {
	$(this).parent().parent('.communicationcall').hide();
});
$(document).on('click', '.closeagentemail', function () {
	$(this).parent().parent('.communicationemail').hide();
});
$(document).on('click', '.closeagentother', function () {
	$(this).parent().parent('.communicationother').hide();
});


var vesselId = '';
var parent = '';

$(document).ready(function () {

	$('.back').click(function () {
		window.location.replace("/Dashboard");
	});

	vesselId = $("#EncryptedVesselId").val();
	parent = $(".vessel-name-details");

	$(".spanvesselCount").text($("#VesselName").val());

	if (screen.width < 767) {
		var divId;
		$(' .vessel-name-details-mobile .vessel-name-list span a').click(function () {
			divId = $(this).attr('href');
			$('html, body').animate({
				scrollTop: $(divId).offset().top - 220
			}, 100);
		});
	}

	$(window).scroll(function () {
		if (($(window).width() < 767)) {
			if ($(this).scrollTop() > 60) {
				$('#icons-box').addClass("iconssticky");
			} else {
				$('#icons-box').removeClass("iconssticky");
			}
		}
	});

	$('.PredictedBadWeatherCls').on('click', function () {
		let count = parseInt($(this).data('predictedbadweather'))
		if (count > 0) {
			let VesselId = $(this).data('vesselid');
			let VesselName = decodeURIComponent($(this).data('vesselname'));
			OpenModalPredictedBadWeather(VesselId, VesselName);
			$('#modalPredictedBadWeather').modal('show');
		}
	});

	AjaxError();

	BindVesselDetailsSummary(vesselId, parent);
	BindingVesselOfficerDetails(vesselId, parent);
	BindSentinelValue(vesselId, parent);
	BindVoyageReporting(vesselId, parent);
	BindPerformanceSummary(vesselId, parent);
	BindVesselHeaderDetails(vesselId);
	BindVesselRightShip(vesselId, parent);	

	if (window.location.hash != '') {
		let currentActivity = document.getElementById(window.location.hash.substring(1));
		currentActivity.scrollIntoView({ behavior: 'smooth', block: 'center' });
	}
	$(parent).find('.vesselcommunicationlink').attr('data-id', vesselId);
});

function BindJSASummary(vesselId, parent) {
	$.ajax({
		url: "/Dashboard/GetJSASummary",
		type: "POST",
		dataType: "JSON",
		data: {
			"vesselId": vesselId
		},
		beforeSend: function (xhr) {
			$(parent).find('.jsa-panel').block({
				message: $(" " + loadercontent),
			})
		},

		success: function (data) {
			if (data != null) {

				//Navigation
				var viewMoreURL = "/JSA/List/?JsaRequest=" + data.totalUrl + "&VesselId=" + vesselId;
				$(parent).find('.jsaViewMore').attr("href", viewMoreURL);

				var highRiskUrl = "/JSA/List/?JsaRequest=" + data.midHighUrl + "&VesselId=" + vesselId;
				$(parent).find('.jsaMidHighRiskUrl').attr("href", highRiskUrl);
								
				$(parent).find('.jsaOpenReportURL').attr("href", viewMoreURL);

				var overdueUrl = "/JSA/List/?JsaRequest=" + data.overdueForClosureUrl + "&VesselId=" + vesselId;
				$(parent).find('.jsaOverdueForClosureUrl').attr("href", overdueUrl);

				var pendOffApprUrl = "/JSA/List/?JsaRequest=" + data.pendingOfficeApprovalUrl + "&VesselId=" + vesselId;
				$(parent).find('.jsaPendOffAppUrl').attr("href", pendOffApprUrl);

				//Count
				$(parent).find(".jsaMidHighRiskCount").text(data.residualRiskMediumAndHighCount);
				$(parent).find(".jsaOpenCount").text(data.totalCount);
				$(parent).find(".jsaOverdueClosureCount").text(data.overdueForClosureCount);
				$(parent).find(".jsaPendingOffAppCount").text(data.pendingOfficeApprovalCount);

				//Priority 
				$(parent).find('.jsaMedHighPanel').addClass(colorMap.get(data.residualRiskMediumAndHighPriority).color);
				$(parent).find('.jsaMidHighRiskCount').addClass(colorMap.get(data.residualRiskMediumAndHighPriority).textColor);

				$(parent).find('.jsaOpenPanel').addClass(colorMap.get(data.openPriority).color);
				$(parent).find('.jsaOpenCount').addClass(colorMap.get(data.openPriority).textColor);

				$(parent).find('.jsaOverduePanel').addClass(colorMap.get(data.overdueForClosurePriority).color);
				$(parent).find('.jsaOverdueClosureCount').addClass(colorMap.get(data.overdueForClosurePriority).textColor);

				$(parent).find('.jsaPendingOfficeApprovalPanel').addClass(colorMap.get(data.pendingOfficeApprovalPriority).color);
				$(parent).find('.jsaPendingOffAppCount').addClass(colorMap.get(data.pendingOfficeApprovalPriority).textColor);
			}
		},
		complete: function () {
			$(parent).find('.jsa-panel').unblock();
		}
	});

}

function BindVesselDetailsSummary(vesselid, parent) {
	var request =
	{
		"VesselId": vesselid,
		"VesselIdentifier": $("#VesselIdentifier").val(),
		"IsFleetSelection": false
	}
	$.ajax({
		url: "/Dashboard/GetVesselDetails",
		type: "POST",
		dataType: "JSON",
		data: request,
		beforeSend: function (xhr) {
			$(parent).find('.vessel-summary').block({
				message: $(" " + loadercontent),
			});
		},

		success: function (data) {
			if (data != null) {
				//$(panelId+' .spanVesselName').text(data.name);
				//$(panelId +' .spanVesselFlag').text(data.flag);
				SetValueElseDefault($(parent).find('.spanVesselImo'), IMO_Prefix + ' ' + data.imo);
				SetValueElseDefault($(parent).find('.spanVesselType'), data.type);
				SetValueElseDefault($(parent).find('.spanVesselBuiltDate'), data.vesselBuiltDate);
				SetValueElseDefault($(parent).find('.spanVesselAge'), data.vesselAge);
				
				if (data.image != null) {
					$(parent).find(".imgvesselpicture").attr("src", "data:image/png;base64," + data.image + "");
				}
				
			}
		},
		complete: function () {
			$('.height-equal-vessel').matchHeight();
			$(parent).find('.vessel-summary').unblock();
		}
	});
}

function BindingVesselOfficerDetails(vesselId, parent) {
	$.ajax({
		url: "/Dashboard/GetVesselOfficeDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": vesselId
		},
		beforeSend: function (xhr) {
			$(parent).find('.officer-detail').block({
				message: $(" " + loadercontent),
			});
		},

		success: function (data) {
			if (data != null) {
				SetValueElseDefault($(parent).find('.spanVesselChiefEngg'), data.vesselChiefEnggName);
				SetValueElseDefault($(parent).find('.spanVesselMaster'), data.vesselMasterName);
			}
		},
		complete: function () {
			$('.height-equal-vessel').matchHeight();
			$(parent).find('.officer-detail').unblock();
		}
	});
}

function BindDefectManager(vesselId, parent) {
	var date = new Date();
	var start = 1 + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
	var end = moment().format("DD MMM YYYY");

	var request =
	{
		"EncryptedVesselId": vesselId,
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
		beforeSend: function (xhr) {
			$(parent).find('.defect-panel').block({
				message: $(" " + loadercontent),
			})
		},

		success: function (data) {
			if (data != null) {
				$(parent).find('.dueDefectsCount').text(data.openDefectCount);
				$(parent).find('.dueDefectsCount-panel').addClass(colorMap.get(data.openDefectPriority).color);
				$(parent).find('.dueDefectsCount').addClass(colorMap.get(data.openDefectPriority).textColor);

				$(parent).find('.overDueDefectsCount').text(data.overdueCount);
				$(parent).find('.overDueDefectsCount-panel').addClass(colorMap.get(data.overduePriority).color);
				$(parent).find('.overDueDefectsCount').addClass(colorMap.get(data.overduePriority).textColor);

				$(parent).find('.offHireRequiredCount').text(data.offHireCount);
				$(parent).find('.offHireRequiredCount-panel').addClass(colorMap.get(data.offHirePriority).color);
				$(parent).find('.offHireRequiredCount').addClass(colorMap.get(data.offHirePriority).textColor);

				$(parent).find('.awaitingSparesCount').text(data.awaitingSparesCount);
				$(parent).find('.awaitingSparesCount-panel').addClass(colorMap.get(data.awaitingSparesPriority).color);
				$(parent).find('.awaitingSparesCount').addClass(colorMap.get(data.awaitingSparesPriority).textColor);

				//setting up url
				var viewMoreNav = "/Defect/List/?DefectRequest=" + data.openDefectNavigation + "&VesselId=" + vesselId;
				$(parent).find('.defectViewMore').attr("href", viewMoreNav);

				var dueNavigation = "/Defect/List/?DefectRequest=" + data.openDefectNavigation + "&VesselId=" + vesselId;
				$(parent).find(".dueDefectsUrl").attr("href", dueNavigation);

				var overdueNavigation = "/Defect/List/?DefectRequest=" + data.overdueDefectNavigation + "&VesselId=" + vesselId;
				$(parent).find(".overDueDefectsUrl").attr("href", overdueNavigation);

				var offHireNavigation = "/Defect/List/?DefectRequest=" + data.offHireDefectNavigation + "&VesselId=" + vesselId;
				$(parent).find(".offHireRequiredUrl").attr("href", offHireNavigation);

				var awaitingSparesNavigation = "/Defect/List/?DefectRequest=" + data.awaitingSparesNavigation + "&VesselId=" + vesselId;
				$(parent).find(".awaitingSparesUrl").attr("href", awaitingSparesNavigation);

			}
		},
		complete: function () {
			$(parent).find('.defect-panel').unblock();
		}
	});
}

function BindCrewSummary(vesselId, parent) {
	$.ajax({
		url: "/Dashboard/GetCrewSummary",
		type: "POST",
		"data": {
			"input": vesselId,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(parent).find('.crew-panel').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {

			$(parent).find(".overDueCount").text(data.overdueCount);
			$(parent).find('.overdue-panel').addClass(colorMap.get(data.overduePriority).color);
			$(parent).find('.overDueCount').addClass(colorMap.get(data.overduePriority).textColor);


			$(parent).find(".unplannedBirthCount").text(data.unplannedBerthCount);
			$(parent).find('.unplanned-panel').addClass(colorMap.get(data.unplannedBerthPriority).color);
			$(parent).find('.unplannedBirthCount').addClass(colorMap.get(data.unplannedBerthPriority).textColor);


			$(parent).find(".medicalCount").text(data.medicalSignOffCount);
			$(parent).find('.medicalSignOff-panel').addClass(colorMap.get(data.medicalSignOffPriority).color);
			$(parent).find('.medicalCount').addClass(colorMap.get(data.medicalSignOffPriority).textColor);

			if (data.crewChangeCount != 'undefined' || data.crewChangeCount != null) {
				$(parent).find(".crewChangesCount").text(data.crewChangeCount);
			}
			$(parent).find('.crewChanges-panel').addClass(colorMap.get(data.crewChangePriority).color);
			$(parent).find('.crewChangesCount').addClass(colorMap.get(data.crewChangePriority).textColor);


			//settig up url			
			var viewMoreNav = "/Crew/List/?CrewList=" + data.viewMoreURL + "&VesselId=" + vesselId;
			$(parent).find('.crewViewMore').attr("href", viewMoreNav);

			var overDueNav = "/Crew/List/?CrewList=" + data.overdueURL + "&VesselId=" + vesselId;
			var unPlannedBerth = "/Crew/List/?CrewList=" + data.unplannedBerthURL + "&VesselId=" + vesselId;
			var crewChangeNav = "/Crew/List/?CrewList=" + data.crewChangeUrl + "&VesselId=" + vesselId;
			var medicalSignOffNav = "/Crew/MedicalSignOffList/?CrewList=" + data.medicalSignOffURL + "&VesselId=" + vesselId;

			$(parent).find(".overDueUrl").attr("href", overDueNav);
			$(parent).find(".unplannedBirthUrl").attr("href", unPlannedBerth);
			$(parent).find(".medicalUrl").attr("href", medicalSignOffNav);
			$(parent).find(".crewChangesUrl").attr("href", crewChangeNav);

		},
		complete: function () {
			$(parent).find('.crew-panel').unblock();
		}
	});
}

function BindOpexSummary(vesselId, parent) {
	var request =
	{
		"VesselId": vesselId,
	}

	$.ajax({
		url: "/Dashboard/GetOpexDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		beforeSend: function (xhr) {
			$(parent).find('.Opex-panel').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			$(parent).find('.budgetYTDCount').text(data.budgetStr);
			$(parent).find('.budget-panel').addClass(colorMap.get(data.budgetKPIPriority).color);
			$(parent).find('.budgetYTDCount').addClass(colorMap.get(data.budgetKPIPriority).textColor);

			$(parent).find('.actualSpendCount').text(data.actualStr);
			$(parent).find('.actual-panel').addClass(colorMap.get(data.actualKPIPriority).color);
			$(parent).find('.actualSpendCount').addClass(colorMap.get(data.actualKPIPriority).textColor);

			$(parent).find('.accrualsCount').text(data.accuralsStr);
			$(parent).find('.accrual-panel').addClass(colorMap.get(data.accrualKPIPriority).color);
			$(parent).find('.accrualsCount').addClass(colorMap.get(data.accrualKPIPriority).textColor);

			$(parent).find('.varianceCount').text(data.variancePercent);
			$(parent).find('.variance-panel').addClass(colorMap.get(data.varianceKPIPriority).color);
			$(parent).find('.varianceCount').addClass(colorMap.get(data.varianceKPIPriority).textColor);

			var OpexURL = "/Finance/List/?OperationCostRequestUrl=" + data.opexDashboardUrl + "&VesselId=" + vesselId;
			$(parent).find('.financeViewMore').attr("href", OpexURL);
			$(parent).find('.varianceURL').attr("href", OpexURL);
			$(parent).find('.actualSpendUrl').attr("href", OpexURL);
			$(parent).find('.accrualsUrl').attr("href", OpexURL);
			$(parent).find('.budgetYTDUrl').attr("href", OpexURL);

		},
		complete: function () {
			$(parent).find('.Opex-panel').unblock();
		}
	});
}

function BindPurchaseOrderSummary(vesselId, parent) {
	//date is not used in api	
	var postartDate = moment().subtract(6, "month");
	var poendDate = moment();
	var localStartDate = postartDate.format("DD MMM YYYY");
	var localEndDate = poendDate.format("DD MMM YYYY");

	var request =
	{
		"vesselId": vesselId,
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
		beforeSend: function (xhr) {
			$(parent).find('.order-panel').block({
				message: $(" " + loadercontent),
			})
		},

		success: function (data) {
			$(parent).find('.requestedCount').text(data.requisitionCount);
			$(parent).find('.orderedCount').text(data.orderedCount);
			$(parent).find('.awaitingSnrCount').text(data.authRequiredCount);
			$(parent).find('.awaitingCount').text(data.awaitingAuthorisationCount);

			var requisitionsURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.requisitionsURL + "&VesselId=" + vesselId;
			$(parent).find('.requestedUrl').attr("href", requisitionsURL);

			var overViewURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.overViewURL + "&VesselId=" + vesselId;
			$(parent).find('.orderViewMore').attr("href", overViewURL);

			var OrderedURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.orderURL + "&VesselId=" + vesselId;
			$(parent).find(".orderedUrl").attr("href", OrderedURL);

			var authRequiredUrl = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.authRequiredURL + "&VesselId=" + vesselId;
			$(parent).find('.awaitingSnrUrl').attr("href", authRequiredUrl);

			var tenderAwaitingAuthURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.tenderAwaitingAuthURL + "&VesselId=" + vesselId;
			$(parent).find('.awaitingUrl').attr("href", tenderAwaitingAuthURL);

			//color
			$(parent).find('.requested-panel').addClass(colorMap.get(data.requisitionPriority).color);
			$(parent).find('.requestedCount').addClass(colorMap.get(data.requisitionPriority).textColor);

			$(parent).find('.ordered-panel').addClass(colorMap.get(data.orderedPriority).color);
			$(parent).find('.orderedCount').addClass(colorMap.get(data.orderedPriority).textColor);

			$(parent).find('.awaitingSnrAuth-panel').addClass(colorMap.get(data.awaitingSnrAuthPriority).color);
			$(parent).find('.awaitingSnrCount').addClass(colorMap.get(data.awaitingSnrAuthPriority).textColor);

			$(parent).find('.tenderAwaitingAuth-panel').addClass(colorMap.get(data.tenderAwaitingAuthPriority).color);
			$(parent).find('.awaitingCount').addClass(colorMap.get(data.tenderAwaitingAuthPriority).textColor);
		},
		complete: function () {
			$(parent).find('.order-panel').unblock();
		}
	});
}

function BindCertificateSummary(vesselId, parent) {
	var request =
	{
		"vesselId": vesselId
	}
	$.ajax({
		url: "/Dashboard/GetCertificateDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"input": request
		},
		beforeSend: function (xhr) {
			$(parent).find('.certificates-panel').block({
				message: $(" " + loadercontent),
			})
		},

		success: function (data) {
			if (data != null && data != 'undefined') {
				$(parent).find('.overdueCount').text(data.overDueCertificateCount);
				$(parent).find('.expiringWithin30Count').text(data.expires30DaysCertificateCount);
				$(parent).find('.withinSurveyRangeCount').text(data.surveyRangeCertificateCount);
				$(parent).find('.stopSailingExpiring30Count').text(data.stopSailingTradingExpiringIn30DaysCount);

				//color
				$(parent).find('.overdueCertificate-panel').addClass(colorMap.get(data.overDueCertificatePriority).color);
				$(parent).find('.overdueCount').addClass(colorMap.get(data.overDueCertificatePriority).textColor);

				$(parent).find('.expiringWithin-panel').addClass(colorMap.get(data.expiringXDaysCertificatePriority).color);
				$(parent).find('.expiringWithin30Count').addClass(colorMap.get(data.expiringXDaysCertificatePriority).textColor);

				$(parent).find('.withingSurvey-panel').addClass(colorMap.get(data.surveyRangeCertificatePriority).color);
				$(parent).find('.withinSurveyRangeCount').addClass(colorMap.get(data.surveyRangeCertificatePriority).textColor);

				$(parent).find('.stopSailingExpiring30-panel').addClass(colorMap.get(data.stopSailingTradingExpiringIn30DaysKPI).color);
				$(parent).find('.stopSailingExpiring30Count').addClass(colorMap.get(data.stopSailingTradingExpiringIn30DaysKPI).textColor);

				//navigation
				var certificateBaseURL = "/Certificate/List?VesselId=" + vesselId + "&CertificateRequestUrl=";

				$(parent).find(".overdueUrl").attr("href", certificateBaseURL + data.overDueCertificateCountURL + "&IsViewMore=false");

				$(parent).find(".expiringWithin30Url").attr("href", certificateBaseURL + data.expires30DaysCertificateCountURL + "&IsViewMore=false");

				$(parent).find(".withinSurveyRangeUrl").attr("href", certificateBaseURL + data.surveyRangeCertificateCountURL + "&IsViewMore=false");

				$(parent).find(".stopSailingExpiring30Url").attr("href", certificateBaseURL + data.stopSailingTradingExpiringIn30DaysUrl + "&IsViewMore=false");

				$(parent).find('.certificateViewMore').attr("href", certificateBaseURL + data.allActiveCertificateCountURL + "&IsViewMore=true");
			}
		},
		complete: function () {
			$(parent).find('.certificates-panel').unblock();
		}
	});
}

function BindInspectionSummary(vesselId, parent) {
	var request =
	{
		"EncryptedVesselId": vesselId,

		"ToDate": moment().format("DD MMM YYYY"),
		"FromDate": moment().subtract(6, "month").format("DD MMM YYYY"),

		"DeficienciesPerPSCToDate": moment().format("DD MMM YYYY"),
		"DeficienciesPerPSCFromDate": moment().subtract(6, "month").format("DD MMM YYYY"),

		"PSCDetentionToDate": moment().format("DD MMM YYYY"),
		"PSCDetentionFromDate": moment().subtract(3, "month").format("DD MMM YYYY"),

		"PscDeficiencyToDate": moment().format("DD MMM YYYY"),
		"PscDeficiencyFromDate": moment().subtract(3, "month").format("DD MMM YYYY"),

		"OmvRejectionToDate": moment().format("DD MMM YYYY"),
		"OmvRejectionFromDate": moment().subtract(3, "month").format("DD MMM YYYY"),

		"DeficienciesPerPscPriorityHighLimit": 3,
		"DeficienciesPerPscPriorityMidLimit": 2.99,
		"DeficienciesPerPscPriorityLowLimit": 1,

		"DeficienciesPerOmvPriorityHighLimit": 3,
		"DeficienciesPerOmvPriorityMidLimit": 2.99,
		"DeficienciesPerOmvPriorityLowLimit": 1,

		"OverdueFindingsPriorityLimit": 0,
		"OverdueInspectionsPriorityLimit": 0,

		"IsFromDashboard": true
	}

	$.ajax({
		url: "/Dashboard/GetInspectionManagerSummary",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		beforeSend: function (xhr) {
			$(parent).find('.inspection-panel').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			if (data != null) {

				$(parent).find(".deficienciesPerOmvCount").text(data.omvDefectRate);
				$(parent).find('.deficienciesPerOmv-panel').addClass(colorMap.get(data.deficienciesPerOMVPriority).color);
				$(parent).find('.deficienciesPerOmvCount').addClass(colorMap.get(data.deficienciesPerOMVPriority).textColor);

				$(parent).find(".deficienciesPerPscCount").text(data.pscDefectRate);
				$(parent).find('.deficienciesPerPsc-panel').addClass(colorMap.get(data.deficienciesPerPSCPriority).color);
				$(parent).find('.deficienciesPerPscCount').addClass(colorMap.get(data.deficienciesPerPSCPriority).textColor);

				$(parent).find(".overdueFindingsCount").text(data.totalOverdueFindingCount);
				$(parent).find('.overdueFindings-panel').addClass(colorMap.get(data.overdueFindingsPriority).color);
				$(parent).find('.overdueFindingsCount').addClass(colorMap.get(data.overdueFindingsPriority).textColor);

				$(parent).find(".overdueInspectionCount").text(data.inspectionOverdueCount);
				$(parent).find('.overdueInspection-panel').addClass(colorMap.get(data.overdueInspectionPriority).color);
				$(parent).find('.overdueInspectionCount').addClass(colorMap.get(data.overdueInspectionPriority).textColor);

				$(parent).find(".dashboardInspectionPlanningDetils").attr('data-vesselid', data.vesselId);
				$(parent).find(".dashboardInspectionPlanningDetils").attr('data-vesselname', encodeURIComponent(data.vesselName));

				$(parent).find(".pscDeficenCount").text(data.totalPSCFindingCount);
				$(parent).find('.pscDeficen-panel').addClass(colorMap.get(data.pscDeficenPriority).color);
				$(parent).find('.pscDeficenCount').addClass(colorMap.get(data.pscDeficenPriority).textColor);

				$(parent).find(".pscDetCount").text(data.pscDetaintionCount);
				$(parent).find('.pscDetCount').addClass(colorMap.get(data.pscDetentionPriority).textColor);
				
				$(parent).find(".omvRejCount").text(data.omvRejCount);
				$(parent).find('.omvRejCount').addClass(colorMap.get(data.omvRejPriority).textColor);

				if (data.omvRejPriority == 2 || data.pscDetentionPriority == 2)
					$(parent).find('.pscomv-panel').addClass(colorMap.get(2).color); // Red
				else
					$(parent).find('.pscomv-panel').addClass(colorMap.get(0).color); // Green

				//settig up url			
				var viewMoreNav = "/Inspection/List/?Inspection=" + data.overviewInspectionUrl + "&VesselId=" + vesselId + "&IsViewMore=true";
				var overdueFindingsNav = "/Inspection/List/?Inspection=" + data.overdueFindingsUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
				var pscDetNav = "/Inspection/List/?Inspection=" + data.pscDetentionUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
				var pscDeficiencyNav = "/Inspection/List/?Inspection=" + data.pscDeficiencyUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
				var omvRejectionNav = "/Inspection/List/?Inspection=" + data.omvRejectionUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
				var deficienciesPerOmvNav = "/Inspection/List/?Inspection=" + data.deficienciesPerOmvURL + "&VesselId=" + vesselId + "&IsViewMore=false";
				var deficienciesPerPscNav = "/Inspection/List/?Inspection=" + data.deficienciesPerPscURL + "&VesselId=" + vesselId + "&IsViewMore=false";

				if (data.omvRejCount == 'N/A') {
					$(parent).find('.omvRejCount').addClass('inspection-na');
					$(parent).find('.omvRejUrl').attr("href", 'javascript: void(0);');
				}
				else {
					$(parent).find('.omvRejCount').removeClass('inspection-na');
					$(parent).find('.omvRejUrl').attr("href", omvRejectionNav);
				}

				if (data.omvDefectRate == 'N/A') {
					$(parent).find('.deficienciesPerOmvCount').addClass('inspection-na');
					$(parent).find('.deficienciesPerOmvUrl').attr("href", 'javascript: void(0);');
				}
				else {
					$(parent).find('.deficienciesPerOmvCount').removeClass('inspection-na');
					$(parent).find('.deficienciesPerOmvUrl').attr("href", deficienciesPerOmvNav);
				}

				$(parent).find('.overdueInspectionUrl').attr("href", 'javascript: void(0);');
				$(parent).find('.deficienciesPerPscUrl').attr("href", deficienciesPerPscNav);

				$(parent).find('.overdueFindingsUrl').attr("href", overdueFindingsNav);
				$(parent).find('.pscDetUrl').attr("href", pscDetNav);
				$(parent).find('.pscDeficenUrl').attr("href", pscDeficiencyNav);
				$(parent).find('.inspectionViewMore').attr("href", viewMoreNav);
			}
		},
		complete: function () {
			$(parent).find('.inspection-panel').unblock();
		}
	});
}

function BindPMSSummary(vesselId, parent) {

	var request =
	{
		"VesselId": vesselId
	}
	$.ajax({
		url: "/Dashboard/PMSDashboardSummary",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		beforeSend: function (xhr) {
			$(parent).find('.pms-panel').block({
				message: $(" " + loadercontent),
			})
		},

		success: function (data) {
			if (data != null) {
				$(parent).find('.pmsCriticalOverdueCount').text(data.criticalOverdue);
				$(parent).find('.pms-critical-overdue-panel').addClass(colorMap.get(data.criticalOverduePriority).color);
				$(parent).find('.pmsCriticalOverdueCount').addClass(colorMap.get(data.criticalOverduePriority).textColor);

				$(parent).find('.pmsCriticalDueCount').text(data.criticalDue);
				$(parent).find('.pms-critical-due-panel').addClass(colorMap.get(data.criticalDuePriority).color);
				$(parent).find('.pmsCriticalDueCount').addClass(colorMap.get(data.criticalDuePriority).textColor);

				$(parent).find('.pmsOverdueCount').text(data.overdue);
				$(parent).find('.pms-overdue-panel').addClass(colorMap.get(data.overduePriority).color);
				$(parent).find('.pmsOverdueCount').addClass(colorMap.get(data.overduePriority).textColor);

				$(parent).find('.pmsDueCount').text(data.due);
				$(parent).find('.pms-due-panel').addClass(colorMap.get(data.duePriority).color);
				$(parent).find('.pmsDueCount').addClass(colorMap.get(data.duePriority).textColor);

				var viewMoreURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.allRequestURL + "&VesselId=" + vesselId + "&IsViewMore=true";
				$(parent).find('.pmsViewMore').attr("href", viewMoreURL);

				var dueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.dueURL + "&VesselId=" + vesselId + "&IsViewMore=false";
				$(parent).find('.pmsDueURL').attr("href", dueURL);

				var criticalDueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalDueURL + "&VesselId=" + vesselId + "&IsViewMore=false";
				$(parent).find('.pmsCriticalDueURL').attr("href", criticalDueURL);

				var overdueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.overdueURL + "&VesselId=" + vesselId + "&IsViewMore=false";
				$(parent).find('.pmsOverdueURL').attr("href", overdueURL);

				var criticalOverdueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalOverdueURL + "&VesselId=" + vesselId + "&IsViewMore=false";
				$(parent).find('.pmsCriticalOverdueURL').attr("href", criticalOverdueURL);

			}
		},
		complete: function () {
			$(parent).find('.pms-panel').unblock();
		}
	});
}

function BindHazoccSummary(vesselId, parent) {

	var request =
	{
		"VesselId": vesselId,
		"StartDate": moment().subtract(12, 'months').add(1, 'days').format('DD MMM YYYY'),
		"EndDate": moment().endOf('day').format('DD MMM YYYY HH:mm:ss')
	}
	$.ajax({
		url: "/Dashboard/HazOccSummary",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		beforeSend: function (xhr) {
			$(parent).find('.hazoc-panel').block({
				message: $(" " + loadercontent),
			})
		},

		success: function (data) {
			if (data != null) {
				$(parent).find('.seriousAccidentsCount').text(data.seriousAccidentCount);
				$(parent).find('.seriousAccidentsPanel').addClass(colorMap.get(data.seriousAccidentPriority).color);
				$(parent).find('.seriousAccidentsCount').addClass(colorMap.get(data.seriousAccidentPriority).textColor);

				$(parent).find('.seriousIncidentsCount').text(data.seriousIncidentCount);
				$(parent).find('.seiousIncidentsPanel').addClass(colorMap.get(data.seriousIncidentPriority).color);
				$(parent).find('.seriousIncidentsCount').addClass(colorMap.get(data.seriousIncidentPriority).textColor);

				$(parent).find('.ltiFreeDaysCount').text(data.ltiFreeDaysCount);
				$(parent).find('.ltiFreeDaysPanel').addClass(colorMap.get(data.ltiFreeDaysPriority).color);
				$(parent).find('.ltiFreeDaysCount').addClass(colorMap.get(data.ltiFreeDaysPriority).textColor);

				$(parent).find('.unsafeCount').text(ConvertDecimalNumberToString(data.unsafeRate, 2, 1, 2));
				$(parent).find('.unsafePanel').addClass(colorMap.get(data.unsafePriority).color);
				$(parent).find('.unsafeCount').addClass(colorMap.get(data.unsafePriority).textColor);

				let viewMoreNav = '/HazOcc/List?request=' + data.hazOccListRequestUrl + '&VesselId=' + vesselId ;
				$(parent).find('.hazocViewMore').attr("href", viewMoreNav);

				let seriousAccidentURL = '/HazOcc/List?request=' + data.seriousAccidentURL + '&VesselId=' + vesselId;
				$(parent).find('.seriousAccidentsUrl').attr("href", seriousAccidentURL);

				let seriousIncidentURL = '/HazOcc/List?request=' + data.seriousIncidentURL + '&VesselId=' + vesselId;
				$(parent).find('.seriousIncidentsUrl').attr("href", seriousIncidentURL);

				let uaucRateURL = '/HazOcc/List?request=' + data.unsafeActAndUnsafeConditionURL + '&VesselId=' + vesselId;
				$(parent).find('.unsafeUrl').attr("href", uaucRateURL);
			}
		},
		complete: function () {
			$(parent).find('.hazoc-panel').unblock();
		}
	});
}

function BindCommercialSummary(vesselId, parent) {
	$.ajax({
		url: "/Dashboard/GetCommercialSummary",
		type: "POST",
		"data": {
			"input": vesselId,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(parent).find('.commercial-panel').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {

			$(parent).find(".unplannedOffHireHrs").text(data.unplannedOffHireHrs);
			$(parent).find('.unplannedOffHireHrs-panel').addClass(colorMap.get(data.unplannedOffHireHrsPriority).color);
			$(parent).find('.unplannedOffHireHrs').addClass(colorMap.get(data.unplannedOffHireHrsPriority).textColor);

			$(parent).find(".fuelUnderPerformance").text(data.fuelUnderPerformance);
			$(parent).find('.fuelUnderPerformance-panel').addClass(colorMap.get(data.fuelUnderPerformancePriority).color);
			$(parent).find('.fuelUnderPerformance').addClass(colorMap.get(data.fuelUnderPerformancePriority).textColor);

			$(parent).find(".speedUnderPerformance").text(data.speedUnderPerformance);
			$(parent).find('.speedUnderPerformance-panel').addClass(colorMap.get(data.speedUnderPerformancePriority).color);
			$(parent).find('.speedUnderPerformance').addClass(colorMap.get(data.speedUnderPerformancePriority).textColor);

			$(parent).find(".predictedBadWeather").text(data.predictedBadWeather);
			$(parent).find('.predictedBadWeather-panel').addClass(colorMap.get(data.predictedBadWeatherPriority).color);
			$(parent).find('.predictedBadWeather').addClass(colorMap.get(data.predictedBadWeatherPriority).textColor);
			$(parent).find(".PredictedBadWeatherCls").attr('data-vesselid', data.vesselId);
			$(parent).find(".PredictedBadWeatherCls").attr('data-vesselname', encodeURIComponent(data.vesselName));
			$(parent).find(".PredictedBadWeatherCls").attr('data-predictedbadweather', data.predictedBadWeather);

			//settig up url			
			var viewMoreNav = "/VoyageReporting/VesselActivityList/?VoyageReportingRequestUrl=" + data.viewMoreURL + "&VesselId=" + vesselId;
			$(parent).find('.commercialViewMoreUrl').attr("href", viewMoreNav);
			$(parent).find(".unplannedOffHireHrsUrl").attr("href", viewMoreNav);
			$(parent).find(".fuelUnderPerformanceUrl").attr("href", viewMoreNav);
			$(parent).find(".speedUnderPerformanceUrl").attr("href", viewMoreNav);
		},
		complete: function () {
			$(parent).find('.commercial-panel').unblock();
		}
	});
}

function BindEnvironmentSummary(vesselId, parent) {

	var request =
	{
		"VesselId": vesselId,
		"StartDate": moment().subtract(30, "day").format("DD MMM YYYY"),
		"EndDate": moment().format("DD MMM YYYY"),
		"OilSpillStartDate": moment().subtract(3, "month").format("DD MMM YYYY"),
		"OilSpillEndDate": moment().format("DD MMM YYYY"),
		"BilgeStartDate": moment().subtract(1, "year").format("DD MMM YYYY"),
		"BilgeEndDate": moment().format("DD MMM YYYY"),
		"AEUtilisationStartDate": moment().subtract(1, "year").format("DD MMM YYYY"),
		"AEUtilisationEndDate": moment().format("DD MMM YYYY")
	}

	$.ajax({
		url: "/Dashboard/GetEnvironmentSummary",
		type: "POST",
		"data": {
			"request": request,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(parent).find('.environment-panel').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			$(parent).find(".EEOICount").text(data.eeoi);
			$(parent).find('.EEOI-panel').addClass(colorMap.get(0).color);
			$(parent).find('.EEOICount').addClass(colorMap.get(0).textColor);

			$(parent).find(".accidentalSpillsCount").text(data.accidentalOilSpillsCount);
			$(parent).find('.accidentalSpills-panel').addClass(colorMap.get(0).color);
			$(parent).find('.accidentalSpillsCount').addClass(colorMap.get(0).textColor);

			$(parent).find(".oilBilgeRetentionCount").text(data.oilBilgeRetention + "%");
			$(parent).find('.oilBilgeRetention-panel').addClass(colorMap.get(0).color);
			$(parent).find('.oilBilgeRetentionCount').addClass(colorMap.get(0).textColor);

			$(parent).find(".aeUtilisationCount").text(data.aeUtilisation);
			$(parent).find('.aeUtilisation-panel').addClass(colorMap.get(0).color);
			$(parent).find('.aeUtilisationCount').addClass(colorMap.get(0).textColor);
		},
		complete: function () {
			$(parent).find('.environment-panel').unblock();
		}
	});
}

function BindVoyageReporting(vesselId, parent) {
	$.ajax({
		url: "/Dashboard/GetVoyageLandingPageDetail",
		type: "POST",
		dataType: "JSON",
		data: {
			"VesselId": vesselId
		},
		beforeSend: function (xhr) {

			$(parent).find('.route-graph-loader').block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			if (data != null) {
				if (data.isFromToVisible) {
					$(parent).find(".clsActivityName").text(data.activityName);
					$(parent).find(".voyage-divFromSection").show();
					$(parent).find(".voyage-divToSection").show();

					$(parent).find('.spanFoap').text(data.fromFAOPValue);
					$(parent).find('.spanFromPortName').text(data.fromPortName);
					$(parent).find('.spanSeaPassageFromDate').text(data.fromDate);

					$(parent).find('.spanToEosp').text(data.toEOSPValue);
					$(parent).find('.spanToPortName').text(data.toPortName);
					$(parent).find('.spanSeaPassageToDate').text(data.toPortDate);

					var viewMoreNav = "/VoyageReporting/VesselActivityList/?VoyageReportingRequestUrl=" + data.requestURL + "&VesselId=" + vesselId;
					$(parent).find('.currentVoyageURL').attr("href", viewMoreNav);
					$(parent).find('.chartername').text(data.charterName);
					$(parent).find('.charternumber').text(data.charterNumber);

					$(parent).find('.showFromAgentDetails').hide();
					$(parent).find('.showToAgentDetails').hide();

					if (!IsNullOrEmptyOrUndefinedLooseTyped(data.previousActivityId) || data.isAgentAvailable) {
						$(parent).find('.showFromAgentDetails').show();
						$(parent).find('.showFromAgentDetails').data('encryptedposid', data.fromAgentRequestURL);
						$(parent).find('.showFromAgentDetails').data('portname', encodeURIComponent(data.fromPortName));
					}

					if (!IsNullOrEmptyOrUndefinedLooseTyped(data.nextActivityId)) {
						$(parent).find('.showToAgentDetails').show();
						$(parent).find('.showToAgentDetails').data('encryptedposid', data.toAgentRequestURL);
						$(parent).find('.showToAgentDetails').data('portname', encodeURIComponent(data.toPortName));
					}

					$(parent).find('.fromAnchorPortAlertCls').addClass('d-none').removeClass('d-inline-block');
					$(parent).find('.toAnchorPortAlertCls').addClass('d-none').removeClass('d-inline-block');

					if (data.hasFromPortAlert) {
						$(parent).find('.fromAnchorPortAlertCls').removeClass('d-none').addClass('d-inline-block');
						$(parent).find('.fromAnchorPortAlertCls').data('url', data.fromRequestURL);
					}

					if (data.isToPortAlertVisible) {
						$(parent).find('.toAnchorPortAlertCls').removeClass('d-none').addClass('d-inline-block');
						$(parent).find('.toAnchorPortAlertCls').data('url', data.toRequestURL);
					}

					if (data.plA_ID != 'SP') {
						$(parent).find(".HeadingPreformanceLeft").text("Latest Voyage Performance");
						$(parent).find(".HeadingPreformanceRight").text("Previous Voyage Performance");
					}
					else {
						$(parent).find(".HeadingPreformanceLeft").text("Current Voyage Performance");
						$(parent).find(".HeadingPreformanceRight").text("Previous Voyage Performance");
					}
				}
				else {
					$(parent).find(".voyage-divFromSection").hide();
					$(parent).find(".voyage-divToSection").hide();
				}

				if (data.plA_ID != 'SP') {
					LoadPortCallDetails(data, parent);
					$(parent).find(".infoportdiv").removeClass('d-none');
					$(parent).find(".infoseaPassagediv").addClass('d-none');
				}
				else {
					LoadProgressBar(data, parent);
					$(parent).find(".infoportdiv").addClass('d-none');
					$(parent).find(".infoseaPassagediv").removeClass('d-none');
				}
			}
		},
		complete: function () {
			$('.height-equal-vessel').matchHeight();
			$(parent).find('.route-graph-loader').unblock();
		}
	});
}

function BindPerformanceSummary(vesselId, parent) {

	var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
	var endDate = moment().format("DD MMM YYYY");

	var request =
	{
		"VesselId": vesselId,
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

			for (let i = 0; i < result.length; i++) {
				if (result.length == 1) {
					$(".previousectionmobile").hide();
				}
				else {
					$(".previousectionmobile").show();
                }

				let obj = result[i];
				let selector = '';
				if (obj.rankId == 1) {
					selector = ".currentsectionmobile";
				}
				else {
					selector = ".previousectionmobile";
				}
				SetPerformanceSummary(parent, selector, obj);
			}
		},
		complete: function () {
			$('.height-equal-vessel').matchHeight();
			$(parent).find(".performance-section").unblock();
		}
	});
}

function SetPerformanceSummary(parent, selector, data) {
	$(parent).find(selector).find(".last24HourSpeed-text").text(data.last24HourSpeed);
	$(parent).find(selector).find(".last24HourSpeed").addClass(colorMap.get(data.last24HourSpeedPriority).textColor);
	$(parent).find(selector).find(".last24HourSpeed").addClass(colorMap.get(data.last24HourSpeedBackgroundPriority).color);

	$(parent).find(selector).find(".last24HourConsumption-text").text(data.last24HourConsumption);
	$(parent).find(selector).find(".last24HourConsumption").addClass(colorMap.get(data.last24HourConsumptionPriority).textColor);
	$(parent).find(selector).find(".last24HourConsumption").addClass(colorMap.get(data.last24HourConsumptionBackgroundPriority).color);

	$(parent).find(selector).find(".voyageAverageSpeed-text").text(data.voyageAverageSpeed);
	$(parent).find(selector).find(".voyageAverageSpeed").addClass(colorMap.get(data.voyageAverageSpeedPriority).textColor);
	$(parent).find(selector).find(".voyageAverageSpeed").addClass(colorMap.get(data.voyageAverageSpeedBackgroundPriority).color);

	$(parent).find(selector).find(".voyageAverageConsumption-text").text(data.voyageAverageConsumption);
	$(parent).find(selector).find(".voyageAverageConsumption").addClass(colorMap.get(data.voyageAverageConsumptionPriority).textColor);
	$(parent).find(selector).find(".voyageAverageConsumption").addClass(colorMap.get(data.voyageAverageConsumptionBackgroundPriority).color);

	$(parent).find(selector).find(".cpAdjustedSpeed-text").text(data.cpAdjustedSpeed);
	$(parent).find(selector).find(".cpAdjustedSpeed").addClass(colorMap.get(data.cpAdjustedSpeedPriority).textColor);
	$(parent).find(selector).find(".cpAdjustedSpeed").addClass(colorMap.get(data.cpAdjustedSpeedBackgroundPriority).color);

	$(parent).find(selector).find(".cpAdjustedConsumption-text").text(data.cpAdjustedConsumption);
	$(parent).find(selector).find(".cpAdjustedConsumption").addClass(colorMap.get(data.cpAdjustedConsumptionPriority).textColor);
	$(parent).find(selector).find(".cpAdjustedConsumption").addClass(colorMap.get(data.cpAdjustedConsumptionBackgroundPriority).color);

	$(parent).find(selector).find(".cpOrdersSpeed-text").text(data.cpOrdersSpeed);
	//$(parent).find(".cpOrdersSpeed").addClass(colorMap.get(data.cpOrdersSpeedPriority).textColor);
	//$(parent).find(".cpOrdersSpeed").addClass(colorMap.get(data.cpOrdersSpeedBackgroundPriority).color);

	$(parent).find(selector).find(".cpOrdersConsumption-text").text(data.cpOrdersConsumption);
			//$(parent).find(".cpOrdersConsumption").addClass(colorMap.get(data.cpOrdersConsumptionPriority).textColor);
			//$(parent).find(".cpOrdersConsumption").addClass(colorMap.get(data.cpOrdersConsumptionBackgroundPriority).color);  
}

function LoadPortCallDetails(data, parent) {
	$(parent).find(".portCountryCls").text(data.countryName);
	$(parent).find(".portLocCodeCls").text(data.unlocode);
	$(parent).find(".portKeyHubCls").text(data.isKeyHubPort);
	$(parent).find(".portLatitudeCls").text(data.fullLatitude);
	$(parent).find(".portLongitudeCls").text(data.fullLongitude);
	$(parent).find(".portEospDateHeaderCls").text(data.eospDateHeader);
	$(parent).find(".portEospDateCls").text(data.eospDate);
	$(parent).find(".portBerthDateHeaderCls").text(data.berthDateHeader);
	$(parent).find(".portBerthDateCls").text(data.berthDate);
	$(parent).find(".PortUnBerthDateHeaderCls").text(data.unBerthDateHeader);
	$(parent).find(".PortUnBerthDateCLs").text(data.unBerthDate);
	$(parent).find(".portFaopDateHeaderCls").text(data.faopDateHeader);
	$(parent).find(".portFaopDateCls").text(data.faopDate);
	$(parent).find(".portVoyageNoCls").text(data.voyageNumber);
	$(parent).find(".portLastReportedEventCls").text(data.lastUpdatedEventDate);
}


function LoadProgressBar(data, parent) {
	$(parent).find('.spanSPTotalDistance').text(ConvertDecimalNumberToString(data.totalDistance, 2, 1, 2) + " nm");
	$(parent).find('.spanSPDistanceCovered').text(ConvertDecimalNumberToString(data.distanceTravelled, 2, 1, 2) + " nm");

	if (data.isSeaPassageEvent == true) {
		var endPositionOfVessel = ConvertValueToPercentage(data.distanceTravelled, data.totalDistance);
		var progressBarEndContent = "AT SEA" + "<br/>" + data.lastEventPosition + "<br/>" + data.remainingValue + " nm remaining";
		$(parent).find('.divProgressBarFlow').css('width', endPositionOfVessel + '%');
		$(parent).find('.at-location').css('left', endPositionOfVessel + '%');
		$(parent).find('.at-location').attr('title', progressBarEndContent);
		//TODO fix this
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
					$(parent).find(".divProgressBar").append('<a class="vesselDetailsMobileBadWeather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
						'<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
						'style="left:' + badWeatherPosition + '%;top: -23px;" data-html="true"></div></a>');
				}
				else {
					$(parent).find(".divProgressBar").append('<a class="vesselDetailsMobileBadWeather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
						'<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
						'style="left:' + badWeatherPosition + '%;" data-html="true"></div></a>');
				}
			}

			//this is for off hire
			//IsOnlyBreakAlert || IsBreakAndBadWeatherAlert uses - BreakDetailsTemplate
			if (data.badWeatherDetails[i].isOnlyBreakAlert == true || data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
				var offHirePosition = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);
				$(parent).find(".divProgressBar").append('<a class="vesselDetailsMobileoffhire" href="javascript:void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
					'<div class="graph-position-default offhire-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
					'style="left:' + offHirePosition + '%;" data-html="true"></div></a>');
			}

			//IsOnlyPortBadWeatherAlert - uses PortBadWeathersDetailsTemplate
			if (data.badWeatherDetails[i].isOnlyPortBadWeatherAlert == true) {
				var PortBadWeathersDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);
				$(parent).find(".divProgressBar").append('<a class="vesselDetailsMobileportBadWeather" href="javascript:void(0);" id=' + data.requestURL + '>' +
					'<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
					'style="left:' + PortBadWeathersDetail + '%;" data-html="true"></div></a>');
			}

			//IsOnlyDelayAlert - use DelaysDetailsTemplate
			if (data.badWeatherDetails[i].isOnlyDelayAlert == true) {
				var DelaysDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

				$(parent).find(".divProgressBar").append('<a class="vesselDetailsMobileportDelay" href="javascript:void(0);" id=' + data.requestURL + '>' +
					'<div class="graph-position-default delayed-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
					'style="left:' + DelaysDetail + '%;" data-html="true"></div></a>');
			}
		}
	}
}

function BindVesselHeaderDetails(vesselId) {

	var request =
	{
		"VesselIds": vesselId,
	}

	$.ajax({
		url: "/Dashboard/GetDashboardVesselHeader",
		type: "POST",
		"data": {
			"input": request,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(document).find(".vessel-name-list").block({
				message: $(" " + loadercontent),
			});
			$(document).find(".moduleList").block({
				message: $(" " + loadercontent),
			});
		},
		success: function (data) {

			if (data[0].canShowCertificates == true) {
				BindCertificateSummary(vesselId, parent);
				$("#certificateHeaderImage img").attr('src', data[0].certificateColor);
				
			} else {
				$("#certificateHeaderImage").hide();
				$('.certificates-panel').hide();
			}

			if (data[0].canShowCommercial == true) {
				$("#commercialHeaderImage img").attr('src', data[0].commercialColor);
				BindCommercialSummary(vesselId, parent);
			} else {
				$("#commercialHeaderImage").hide();
				$('.commercial-panel').hide();
			}

			if (data[0].canShowCrewing == true) {
				$("#crewHeaderImage img").attr('src', data[0].crewColor);
				BindCrewSummary(vesselId, parent);
			} else {
				$("#crewHeaderImage").hide();
				$('.crew-panel').hide();
			}

			if (data[0].canShowDefects == true) {
				BindDefectManager(vesselId, parent);
				$("#defectsHeaderImage img").attr('src', data[0].defectsColor);
			} else {
				$("#defectsHeaderImage").hide();
				$('.defect-panel').hide();
			}

			if (data[0].canShowEnvironment == true) {
				$("#environmentHeaderImage img").attr('src', data[0].environmentColor);
				BindEnvironmentSummary(vesselId, parent);
			}
			else {
				$("#environmentHeaderImage").hide();
				$('.environment-panel').hide();
			}
			if (data[0].canShowFinancials == true) {
				$("#opexHeader").text(data[0].opexOverSpend);
				$("#opexHeader").addClass(data[0].opexColor);
				BindOpexSummary(vesselId, parent);
			} else {
				$("#opexHeader").hide();
				$('.Opex-panel').hide();
			}

			if (data[0].canShowHazOcc == true) {
				$("#hazocHeaderImage img").attr('src', data[0].hazOccColor);
				BindHazoccSummary(vesselId, parent);
			} else {
				$("#hazocHeaderImage").hide();
				$('.hazoc-panel').hide();
			}

			if (data[0].canShowInspectionsAndRatings == true) {
				$("#inspectionHeaderImage img").attr('src', data[0].inspectionColor);
				BindInspectionSummary(vesselId, parent);
			} else {
				$("#inspectionHeaderImage").hide();
				$('.inspection-panel').hide();
			}

			if (data[0].canShowPMS == true) {				
				$("#pmsHeaderImage img").attr('src', data[0].pmsColor);
				BindPMSSummary(vesselId, parent);
			} else {
				$("#pmsHeaderImage").hide();
				$('.pms-panel').hide();
			}

			if (data[0].canShowJSA == true) {
				$("#jsaHeaderImage img").attr('src', data[0].jsaColor);
				BindJSASummary(vesselId, parent);
			} else {
				$("#jsaHeaderImage").hide();
				$('.jsa-panel').hide();
			}

			if (data[0].canShowProcurement == true) {
				$("#ordersHeaderImage img").attr('src', data[0].purchaseOrderColor);
				BindPurchaseOrderSummary(vesselId, parent);
			} else {
				$("#ordersHeaderImage").hide();
				$('.order-panel').hide();
			}
		},
		complete: function () {
			$('.height-equal-vessel').matchHeight();
			$(document).find(".vessel-name-list").unblock();
			$(document).find(".moduleList").unblock();
		}
	});
}

function OpenModalPredictedBadWeather(vesselId, vesselName) {

	$('#vesselNameBadWeatherModal').text(vesselName);

	$('#dtbadweather').DataTable().destroy();
	var dtbadweather = $('#dtbadweather').DataTable({
		"dom": '<<"row mb-3" <"detailsinfo"><"col-12 col-md-7 offset-md-1 col-lg-7 offset-lg-1 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"scrollY": "235px",
		"scrollCollapse": true,
		"info": true,
		"autoWidth": false,
		"paging": false,
		"language": {
			"emptyTable": "No Bad Weather data present.",
		},
		"ajax": {
			"url": "/Dashboard/GetPredictedBadWeather",
			"type": "POST",
			"data": {
				"input": vesselId,
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-datetime-align",
				data: "weatherDate",
				width: "60px",
				render: function (data, type, full, meta) { return GetFormattedDateTime(type, 'Date', data); }
			},
			{
				className: "data-text-align",
				data: "locationStr",
				width: "100px",
				render: function (data, type, full, meta) { return GetCellData('Location', data); }
			},
			{
				className: "data-text-align",
				data: "speedBeaufort",
				width: "40px",
				render: function (data, type, full, meta) {
					return GetCellData('Beaufort', '<span style="color:' + full.beaufortColour + '">F' + data + '</span');
				}
			},
			{
				className: "data-number-align",
				data: "speedMS",
				width: "45px",
				render: function (data, type, full, meta) { return GetCellData('Speed m/s', data); }
			},
			{
				className: "data-number-align",
				data: "direction",
				width: "45px",
				render: function (data, type, full, meta) { return GetCellData('Direction', data == null ? "" : data); }
			}
		]
	});
}

function BindVesselRightShip(vesselid, parent) {
	let input = {
		"VesselId": vesselid
	}
	$.ajax({
		url: "/Dashboard/GetRightShipGHGRatingDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": input
		},
		beforeSend: function (xhr) {
			$(parent).find('.rightship-details').block({
				message: $(" " + loadercontent),
			});
		},

		success: function (data) {
			if (data != null && data.data != null) {
				let rightShipDetails = data.data;
				if (!IsNullOrEmpty(rightShipDetails.rightShipScore)) {
					let score = GetFormattedDecimal('', '', rightShipDetails.rightShipScore, 2, '0.00');
					$(parent).find('.spanRightShipScore').text(score);
				}
				if (!IsNullOrEmpty(rightShipDetails.ghgRating)) {
					$(parent).find('.spanRightShipRating').text(rightShipDetails.ghgRating);
				}

			}
		},
		complete: function () {
			$('.height-equal-vessel').matchHeight();
			$(parent).find('.rightship-details').unblock();
		}
	});
}

function CurrentVoyageAgentDetails(urlRequest, portname ) {
	$('#VoyageAgentContentsHtml').html("");
	$('#VoyageAgentCount').html("0");
	$('#VoyagePortName').text("");
	$('#ModalVoyageAgentDetails').modal("show");
	$('#ModalVoyageAgentDetails').block({
		message: $(" " + loadercontent),
	});

	$.ajax({
		url: "/Dashboard/GetAgentDetails",
		type: "POST",
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		success: function (result) {
			
			if (result != null && result.length > 0) {

				$('#VoyageAgentCount').text(result.length);
				$('#VoyagePortName').text(portname);

				for (var index = 0; index < result.length; index++) {
					var obj = result[index];
					var tabContentsHtml = '';
					var copyAgentDetails = '';
					var agentType = obj.typeOfCompany
					var agentNumber = index + 1;
					//cmpname + agent type
					tabContentsHtml += '<div class="agentlist" >';

					tabContentsHtml += ' <div class="body-box" id=""><div class="agentheadtitle">Agent ' + agentNumber + '</div ></div > ';
					tabContentsHtml += '<div class="body-box" id=""><div><div class="main-title title" id="">' + obj.companyName + ' (' + agentType + ')</div> </div> </div>';

					//location
					tabContentsHtml += '<div class="body-box" id="">';
					tabContentsHtml += '<div class="image align-top"><img id="" class="" src="/images/location-supplier.png"></div>';
					tabContentsHtml += '<div>';

					if (!IsNullOrEmptyOrUndefined(obj.address)) {
						tabContentsHtml += '<div class="title">';
						tabContentsHtml += '<div id="">' + obj.address + '</div>';
						tabContentsHtml += '</div>';
					}

					if (!IsNullOrEmptyOrUndefined(obj.town) || !IsNullOrEmptyOrUndefined(obj.state) || !IsNullOrEmptyOrUndefined(obj.postalCode)) {
						tabContentsHtml += '<div class="title" id="">';

						if (!IsNullOrEmptyOrUndefined(obj.town)) {
							tabContentsHtml += '<span>' + obj.town + '&nbsp;</span>';
						}
						if (!IsNullOrEmptyOrUndefined(obj.state)) {
							tabContentsHtml += '<span>' + obj.state + '&nbsp;</span>';
						}

						if (!IsNullOrEmptyOrUndefined(obj.postalCode)) {
							tabContentsHtml += '<span>' + obj.postalCode + '&nbsp;</span>';
						}
						tabContentsHtml += '</div>';
					}

					if (!IsNullOrEmptyOrUndefined(obj.country)) {
						tabContentsHtml += '<div class="title" id="">' + obj.country + '</div>';
					}
					tabContentsHtml += '</div></div>';

					//end location

					//telephone
					tabContentsHtml += '<div class="body-box" id="">';
					tabContentsHtml += '<div class="image align-top"><img id="" class="" src="/images/telephone-supplier.png" width="19"></div>';

					tabContentsHtml += '<div>';

					if (!IsNullOrEmptyOrUndefined(obj.mobile)) {
						tabContentsHtml += '<div class="title" id="">';
						tabContentsHtml += '<a id="" class="email position-relative" href="tel:' + obj.mobile + '">';
						tabContentsHtml += '<div id="">' + obj.mobile + '</div></a> </div>';
					}

					if (!IsNullOrEmptyOrUndefined(obj.telephone)) {
						tabContentsHtml += '<div class="title" id="">';
						tabContentsHtml += '<a id="" class="email position-relative" href="tel:' + obj.telephone + '">';
						tabContentsHtml += '<div id="">' + obj.telephone + '</div></a> </div>';
					}

					if (!IsNullOrEmptyOrUndefined(obj.telex)) {
						tabContentsHtml += '<div class="title" id="">';
						tabContentsHtml += '<a id="" class="email position-relative" href="tel:' + obj.telex + '">';
						tabContentsHtml += '<div id="">' + obj.telex + '</div></a> </div>';
					}
					tabContentsHtml += '</div></div>';
					//end telephone

					//fax
					if (!IsNullOrEmptyOrUndefined(obj.fax)) {
						tabContentsHtml += ' <div class="body-box" id="">';
						tabContentsHtml += '<div class="image"><img id="" class="" src="/images/fax-suppliernew.png"></div>';

						tabContentsHtml += '<div>';

						tabContentsHtml += '<div class="title" id="spanFax">' + obj.fax + '</div>';

						tabContentsHtml += '</div ></div >';
					}

					//email
					if (!IsNullOrEmptyOrUndefined(obj.email)) {
						tabContentsHtml += '<div class="body-box" id="">';
						tabContentsHtml += '<div class="image"> <img id="" class="" src="/images/email-supplier.png" width="20"> </div>';

						tabContentsHtml += '<div>';

						tabContentsHtml += '<div class="title">';
						tabContentsHtml += '<a id="" class="email position-relative" href="mailto:' + obj.email + '">';
						tabContentsHtml += '<div id="">' + obj.email + '</div></a></div>';

						tabContentsHtml += '</div></div>';
					}

					//website
					if (!IsNullOrEmptyOrUndefined(obj.website)) {
						tabContentsHtml += '<div class="body-box" id="">';
						tabContentsHtml += '<div class="image"> <img id="" class="" src="/images/web-supplier.png"> </div>';

						tabContentsHtml += '<div>';

						let cmpWww = obj.website;
						if (!obj.website.match(/^http([s]?):\/\/.*/)) {
							cmpWww = 'http://' + obj.website;
						}

						tabContentsHtml += '<div class="title" id="">';
						tabContentsHtml += '<a href="' + cmpWww + '" target="_blank" class="email position-relative">';
						tabContentsHtml += '<div id="">' + cmpWww + '</div></a></div>';

						tabContentsHtml += '</div></div>';
					}

					//notes // d-none
					if (!IsNullOrEmptyOrUndefined(obj.notes)) {
						tabContentsHtml += '<div class="body-box mb-0 d-none" id="">';
						tabContentsHtml += '<div class="image"> <img id="" class="" src="/images/web-supplier.png"> </div>';

						tabContentsHtml += '<div>';

						tabContentsHtml += '<div class="title" id="">';
						tabContentsHtml += '<div id="">' + obj.notes + '</div></div>';

						tabContentsHtml += '</div></div>';
					}

					tabContentsHtml += '</div>';

					$('#VoyageAgentContentsHtml').append(tabContentsHtml);
				}
			}
		}
		,complete: function (setting) {
			$('#ModalVoyageAgentDetails').unblock();

			if (($(window).width() < MobileScreenSize)) {
				$('body').addClass('fixedmodalbodyagent');
				$('#ModalVoyageAgentDetails').addClass('fixedmodalbodyagent');
				var windowheightagent = $(window).height();
				var modalheaderagent = $('#ModalVoyageAgentDetails .modal-header').outerHeight();
				$("#ModalVoyageAgentDetails .scroller").css({
					"max-height": windowheightagent - modalheaderagent - 60
				});

				$("#ModalVoyageAgentDetails").on('hidden.bs.modal', function (e) {
					$('body').removeClass('fixedmodalbodyagent');
					$('#ModalVoyageAgentDetails').removeClass('fixedmodalbodyagent');
				});
			}
		}
	});
}

function AddCommunicationLoadingIndicator(selector) {
	$(selector).block({
		message: '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
			'<div class="loader  mx-auto">' +
			'<div class="ball-clip-rotate">' +
			'<div></div>' +
			'</div>' +
			'</div>' +
			'</div>',

		onBlock: function () {
			$(".blockElement").addClass("blockMsg");
			$(".blockElement").removeClass("undefined");
		}
	});
}

function BindSentinelValue(vesselid, parent) {
	$.ajax({
		url: "/Dashboard/GetSentinelValue",
		type: "GET",
		dataType: "JSON",
		data: {
			"VesselId": vesselid
		},
		success: function (data) {
			if (data != null) {
				var state = 1;
				if (data.sentinelTotalValueColor == Amber) {
					$(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-orange.png");
				}
				else if (data.sentinelTotalValueColor == Green) {
					$(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-green.png");
				}
				else if (data.sentinelTotalValueColor == Red) {
					$(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-red.png");
				}
				else {
					$(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-grey.png");
					$(parent).find('.aSentinelValue').removeAttr("href");
					state = 2;
				}
				$(parent).find('.pSentinelScore').text(ConvertDecimalNumberToString(data.sentinelTotalValue, 1, state, 1));
			}
		}
	});
}