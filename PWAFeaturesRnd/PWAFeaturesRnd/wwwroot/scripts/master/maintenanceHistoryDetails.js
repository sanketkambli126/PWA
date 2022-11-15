import toastr from "toastr";
import moment from "moment";
require('bootstrap');
import { AjaxError, ToastrAlert, AddLoadingIndicator, RemoveLoadingIndicator, headerReadMore, SetHeaderMargin, base64ToArrayBuffer, saveByteArray, BackButton } from "../common/utilities.js";
import { MaintenanceHistoryDetailsPageKey } from "../common/constants.js";
var encryptedPwoId, encryptedPwhId;
var rescheduleHistory;
var documentHistoryList;

$(document).on("click", ".popover-body", function () {
	$('.popover').popover('hide');
	$('body').removeClass('workOrder-reschedue-popover');
});

$(document).ready(function () {
	AjaxError();
	AddLoadingIndicator();
	RemoveLoadingIndicator();
	
	//Sidebar back
	BackButton(MaintenanceHistoryDetailsPageKey, false)

	GetWorkOrderDetails();
	ComponentHeirarchyDetails();
	ComponentDetails();
	JobDetails();
	GetWorkorderHistoryDocuments();

	$('#aResceduledHistory').on('click', function () {
		RescheduleHistory();
	});

	$(document).ajaxStop(function () {
		matchHeight();
		setScrollerHeight();
		if (screen.width < 760) {
			headerReadMore('headershowmorewrapper', 'header');
		}
		SetHeaderMargin();
	});

	$('#dtspareparts').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-3 col-xl-7 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"searching": false,
	});

});

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
				$('#spanJobName').text(data.jobName);
				$('#spanJobTypeDescription').text(data.jobTypeDescription);
				$('#spanWorkOrderStatusCode').text(data.workOrderStatusCode);
				$('#spanCurrentDueDate').text(data.dueDate);

				//stored for done - Round.
				//EncryptedSystemAreaId = data.encryptedSystemAreaId;
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

				$('#spanSchedulesTaskComponentName').text(data.componentCode + " " + data.componentName);
				$('#spanSchedulesTaskMakerName').text(data.makerName);
				$('#spanSchedulesTaskModel').text(data.model);
				$('#spanSchedulesTaskPosition').text(data.componentPosition);
				$('#spanSchedulesTaskAlternateNumber').text(data.alternativeNumber);
				$('#spanSchedulesTaskWarantyDate').text(data.warrantyDate);
			}
		}
	});
}

function JobDetails() {
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
			data = data.otherWorkOrderViewModel;
			if (data != null) {
				encryptedPwoId = data.pwoId;
				encryptedPwhId = data.pwhId;

				$('#spanWorkDoneDate').text(data.reportWorkDoneDate);
				$('#spanWorkDueDate').text(data.rwdDueDate);
				$('#spanSymptonsObserved').text(data.symptomsObserved);
				$('#spanConditionAfterWorkDone').text(data.afterCondition);
				$('#spanConditionBeforeWorkDone').text(data.beforeCondition);
				$('#spanRemarksFindings').text(data.remarks);

				if (data.isCommentForReason) {
					$('#spanReason').text(data.reason);
					$("#aReasonTooltip").attr("title", data.commentForReason);
					$('#aReasonTooltip').tooltip();
				}
				else {
					$('#divDoneOtherReason').hide();
				}

				//Job And Rank
				WorkOrderJobAndRank(data.orderRankList, data.totalManHours);

				//Parts And Usage
				SparePartList(data.partsUsed);
			}
		}
	});
}

function WorkOrderJobAndRank(orderRankList, totalManHours) {
	var rankslist = $('#dtranksjobs').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-4 col-xl-7 offset-xl-3 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": orderRankList,
		"language": {
			"emptyTable": "No rank available.",
			"zeroRecords": ""
		},
		"columns": [
			{
				className: "data-text-align",
				data: "rankShortCode",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Rank', '<span data-toggle="tooltip" title="' + full.rankDescription + '">' + data + '</span>');
				}
			},
			{
				className: "data-number-align",
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

function SparePartList(partsUsed) {
	var request = {
		"PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
	};

	$('#dtspareparts').DataTable().destroy();
	var sparePart = $('#dtspareparts').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-3 col-xl-7 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": true,
		"paging": true,
		"language": {
			"emptyTable": "No spares available.",
		},
		"data": partsUsed,
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
				width: "70px",
				render: function (data, type, full, meta) {
					return GetCellData('Makers Ref No.', data);
				}
			},
			{
				className: "data-text-align",
				data: "plateSheetNumber",
				width: "80px",
				render: function (data, type, full, meta) {
					return GetCellData('Plate/Sheet No.', data);
				}
			},
			{
				className: "data-text-align",
				data: "drawingPosition",
				width: "60px",
				render: function (data, type, full, meta) {
					return GetCellData('Drawing Position', data);
				}
			},
			{
				className: "data-text-align",
				data: "location",
				width: "60px",
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
				className: "data-text-align",
				data: "quantityUsed",
				width: "60px",
				render: function (data, type, full, meta) {
					return GetCellData('Qty Used', data);
				}
			},
		]
	});

	$('#dtspareparts').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip();
	});
}

function RescheduleHistory() {

	//$("#resceduledhistory").modal("show");
	var request = {
		"PwoId": encryptedPwoId,
		"PwhId": encryptedPwhId,
		"PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
	};

	$('#dtresceduledhistory').DataTable().destroy();
	rescheduleHistory = $('#dtresceduledhistory').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": false,
		"scrollY": "235px",
		"scrollCollapse": true,
		"paging": true,
		"language": {
			"emptyTable": "No reschedule history available.",
		},
		"ajax": {
			"url": "/PlannedMaintenance/GetRescheduleHistoryDetails",
			"type": "POST",
			"data":
			{
				"request": request
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-text-align",
				data: "rescheduleRequestType",
				width: "80px",
				render: function (data, type, full, meta) {
					return GetCellData('Request Type', data);
				}
			},
			{
				className: "data-datetime-align",
				type: "date",
				data: "originalDueDate",
				width: "82px",
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
				width: "82px",
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
		]
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
		data: {
			"workOrderRescheduleId": woData.workOrderRescheduleId,
			"rescheduleRequestTypeId": woData.rescheduleRequestTypeId
		},
		success: function (data) {
			var popOverSettings = woReschedulePopover(data);

			$(thisSelector).popover(popOverSettings);
			$('body').addClass('workOrder-reschedue-popover');
			$(thisSelector).popover('show');
			//$('.popover-body').popover('hide');
		}
	});

}

function woReschedulePopover(data) {

	//$('.popover-dismiss').popover({
	//	trigger: 'focus'
	//});

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
	//IsRequester
	var popOverSettings = {
		placement: 'auto',
		container: '#resceduledhistory',
		html: true,
		trigger: 'focus',
		selector: '[rel="popover"]',
		content: function () {
			return htmlContent;
		}
	}

	return popOverSettings;
}

function GetWorkorderHistoryDocuments() {
	var request = {
		"PlannedMaintenanceRequestDetailsURL": $('#PlannedMaintenanceRequestDetailsURL').val()
	};

	documentHistoryList = $('#dtattachments').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-3 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"ajax": {
			"url": "/PlannedMaintenance/GetWorkorderHistoryDocuments",
			"type": "POST",
			"data":
			{
				"request": request
			},
			"datatype": "json"
		},
		"language": {
			"emptyTable": "No attachment available.",
			"zeroRecords": ""
		},
		"columns": [
			{
				className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
				data: "rankShortCode",
				width: "10px",
				orderable: false,
				render: function (data, type, full, meta) {
					var txtresult = "<a href='#' class='documentDownload cursor-pointer' id='document_'><img src='/images/Download-doc-active.png' class='m-0' width='18' title='Download' /></a>";
					return GetActualCellData(txtresult);
				}
			},
			{
				className: "data-text-align tdblock mobile-heading-large",
				width: "235px",
				data: "title",
				render: function (data, type, full, meta) {
					return GetActualCellData(data);
				}
			},
			{
				className: "data-text-align tdblock",
				data: "description",
				width: "295px",
				render: function (data, type, full, meta) {
					return GetCellData('Description', data);
				}
			},
			{
				className: "data-datetime-align",
				data: "createdOn",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetCellData('Created Date', GetFormattedOnlyDate(data));
				}
			},
			{
				className: "data-text-align",
				data: "type",
				width: "102px",
				render: function (data, type, full, meta) {
					return GetCellData('Type', data);
				}
			}
		],
		"initComplete": function (settings, json) {
			matchHeight();
			setScrollerHeight();
		},
	});
}

$(document).on('click', 'a.documentDownload', function () {
	
	var data = documentHistoryList.row($(this).parents('tr')).data();
	console.log("ROW DATA", data);

	var documentName = data.title;
	var documentId = data.ettId;
	var docfileName = data.cloudFileName
	var isWebAddressEditable = data.isWebAddressEditable
	var webAddress = data.webAddress

	if (isWebAddressEditable == true) {
		window.open(webAddress, '_blank').focus();
	}
	else {
		var fileName = ''
		var input = {
			"identifier": documentId.trim(),
			"fileName": docfileName.trim()
		};
		fileName = documentName.trim();

		$.ajax({
			url: "/PlannedMaintenance/DownloadDocument",
			type: "POST",
			dataType: "JSON",
			data: {
				"input": JSON.stringify(input)
			},
			success: function (data) {
				if (data.bytes != null) {
					var array = base64ToArrayBuffer(data.bytes);
					saveByteArray(fileName, array, data.fileType);
				} else {
					ToastrAlert("error", "File Not Found for \"" + fileName + "\"");
				}
			}
		});
	}
});


/////Common Functions///

function GetCellData(label, data) {
	data = data == null ? "" : data;
	return '<label>' + label + '</label> <br />' + GetActualCellData(data);
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
	let vesselGuideHeight = $("#divVesselGuideLines").height();

	//$("#divSingleJobDescription").height(columnHeight * 0.5);
	$("#divSingleJobDescription").height(columnHeight - vesselGuideHeight - 113);
}

function GetFormattedOnlyDate(data) {
	if (data == null) return "";
	var date = new Date(data);
	return moment(date).format("D MMM YYYY");
}

function GetActualCellData(data) {
	return '<span class="export-Data">' + data + '</span>';
}