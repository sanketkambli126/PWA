import { createTree } from "jquery.fancytree";
import "bootstrap";
import moment from "moment";
import "select2/dist/js/select2.full.js";

import toastr from "toastr";
import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader, GetExportCellData } from "../common/datatablefunctions.js";
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, ToastrAlert, GetCookie, ConvertDecimalNumberToString, SetHeaderMargin, BackButton, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetNotesBaseIcons, GetChatBaseIcons, AddClassIfAbsent, RemoveClassIfPresent, datepickerheightinmobile, RegisterTabSelectionEvent, SummaryColorCode } from "../common/utilities.js";
import { DateRangePickerCancelText, DateRangePickerLabelText, PlannedMaintenanceListPageKey, Tab1, Tab2, MobileScreenSize } from "../common/constants.js";

var IsMobile = false;
var gridWorkBasketList;
const DateFormat = "DD MMM YYYY";
var selectedStartDate;
var selectedEndDate;

var StatusTitles, PriorityTitles, ResponsibilityTitles, RescheduleTitles, JobTypeTitles, OtherFilterTitles;

var IsSearchClickedForExport = false;

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

$(window).on('resize', SetHeaderMargin);

$(document).on('click', '#aRemoveFilter', function () {
	clearFrom();
});

$(document).ready(function () {
	

	AjaxError();
	if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
		IsMobile = true;
	} else {
		IsMobile = false;
	}

	

	$('.height-equal').matchHeight();

	$(".mobile-tab").on("click", function () {
		SetHeaderMargin();
	});

	AddLoadingIndicator();
	RemoveLoadingIndicator();
	DaterangePicker();

	InitializeListDiscussionAndNoteClickEvents(code);

	LoadWBStatusTree();
	LoadWBPriorityTree();
	LoadWBResponsibilityTree();
	LoadWBRescheduledTree();
	LoadWBJobTypeTree();
	LoadWBOtherFiltersTree();
	LoadWBHierarchyTree();

	$('.btnMaintenanceHistory').click(function () {
		CallMaintenanceHistory();
	});


	$('#mobileactiontoggle').click(function () {
		$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
	});
	//$(".dropdown-toggle").dropdown();

	$(document).click(function () {
		if ($("#mobileActiondropdown").hasClass('show')) {
			$("#mobileActiondropdown").removeClass('show');
		}
	});


	$('#btnSearch').click(function () {
		IsSearchClickedForExport = true;
		SetPageParameter(true);
		//LoadWorkBasketDetailsList(true);
	});

	$('#btnClear').click(function () {
		//Default filter
		clearFrom();
	});

	SetMobileTab();

	IsSearchClickedForExport = $("#isSearchedClick").val() == "True" ? true : false;

	LoadWorkBasketDetailsList($("#isSearchedClick").val() == "True" ? true : false);

	BindSummary();

	$('#dtspares').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip();
	});

	$(document).on('click', '.requiredSpareDetails', (function () {
		$("#requiredSpareModal").modal('toggle');
		$("#vesselNameRequiredSpareModal").text($('#VesselName').val());
		$("#vesselNameRequiredSpareModal").attr('href', "/Dashboard/?VesselId=" + $('#EncryptedVesselId').val());
		var request = $(this).data('request');
		var componentName = $(this).data('component');
		$("#componentNameModal").text(componentName);
		LoadRequiredSpares(request);
	}));

	//Sidebar back
	BackButton(PlannedMaintenanceListPageKey, true)

	SetHeaderMargin();

	RegisterTabSelectionEvent('.mobileTabClick', PlannedMaintenanceListPageKey);

	$('.btnExport').click(() => {

		var input = GetInputForSearch(IsSearchClickedForExport);

		$.ajax({
			url: "/PlannedMaintenance/ExportToExcelPMSList",
			type: "POST",
			xhrFields: {
				responseType: 'blob'
			},
			"data": {
				"pmsRequest": input
			},
			success: function (data, textStatus, xhr) {
				var filename = "";
				var disposition = xhr.getResponseHeader('Content-Disposition');
				if (disposition && disposition.indexOf('attachment') !== -1) {
					var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
					var matches = filenameRegex.exec(disposition);
					if (matches != null && matches[1])
						filename = matches[1].replace(/['"]/g, '');
					var a = document.createElement('a');
					var url = window.URL.createObjectURL(data);
					a.href = url;
					a.download = filename;
					document.body.append(a);
					a.click();
					a.remove();
					window.URL.revokeObjectURL(url);
				}
			}
		});
	});

	
});




$('#WBStatusTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBStatusTree"), "#statusFilterCount");
});

$('#WBPriorityTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBPriorityTree"), "#priorityFilterCount");
});

$('#WBResponsibilityTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBResponsibilityTree"), "#responsibilityFilterCount");
});

$('#WBRescheduledTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBRescheduledTree"), "#rescheduleFilterCount");
});

$('#WBJobTypeTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBJobTypeTree"), "#jobtypeFilterCount");
});

$('#WBOtherFiltersTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBOtherFiltersTree"), "#otherFilterCount");
});

$('#WBHierarchyTree').click(function () {
	var node = $.ui.fancytree.getTree("#WBHierarchyTree").getActiveNode();
	if (node != null) {
		FilterCountSet(1, "#componentFilterCount");
	}
	else {
		FilterCountSet(0, "#componentFilterCount");
	}
});


function LoadRequiredSpares(request) {
	$('#dtspares').DataTable().destroy();
	$('#dtspares').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"scrollCollapse": true,
		"autoWidth": false,
		"paging": false,
		"pageLength": 10,
		"language": {
			"emptyTable": "No spares available.",
			"loadingRecords": "&nbsp;"
		},
		"ajax": {
			"url": "/PlannedMaintenance/SparePartListWithEncryptedUrl",
			"type": "POST",
			"data":
			{
				"request": request
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-text-align tdblock",
				data: "partName",
				width: "150px",
				render: function (data, type, full, meta) {
					return data;
				}
			},
			{
				className: "data-text-align",
				data: "makerReferenceNumber",
				width: "120px",
				render: function (data, type, full, meta) {
					return GetCellData('Makers REF No.', data);
				}
			},
			{
				className: "data-text-align",
				data: "plateSheetNumber",
				width: "40px",
				render: function (data, type, full, meta) {
					return GetCellData('Plate/Sheet No.', data);
				}
			},
			{
				className: "data-text-align",
				data: "drawingPosition",
				width: "65px",
				render: function (data, type, full, meta) {
					return GetCellData('Drawing Position', data);
				}
			},
			{
				className: "data-number-align",
				data: "pendingOrderCount",
				width: "50px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = "";
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
					return data;
				}
			},
			{
				className: "data-text-align",
				data: "isRenewSpares",
				width: "45px",
				render: function (data, type, full, meta) {
					return GetCellData('Renew Spare', data);
				}
			},
			{
				className: "data-number-align",
				data: "quantityRequired",
				width: "40px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = ConvertDecimalNumberToString(data, 2, 1);
						return GetCellData('QTY REQD', numberText);
					}
					return data;
				}
			},
			{
				className: "data-text-align",
				data: "isMarkedForReorder",
				width: "35px",
				render: function (data, type, full, meta) {
					return GetCellData('Mark For Order', data);
				}
			},
			{
				className: "data-number-align",
				type: "html-num",
				data: "reorderQuantity",
				width: "50px",
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
				className: "data-text-align",
				data: "remarks",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetCellData('Remarks', data);
				}
			}
		]
	});
}

function GetInputForSearch(isSearchedClick) {

	var StatusIds = GetStringToArray($("#SelectedWBStatusIds").val());
	var PriorityIds = GetStringToArray($("#SelectedWBPriorityIds").val());
	var RescheduledIds = GetStringToArray($('#SelectedWBRescheduledIds').val());
	var ResponsibilityIds = GetStringToArray($('#SelectedWBResponsibilityIds').val());
	var JobTypeIds = GetStringToArray($('#SelectedWBJobTypeIds').val());
	var OtherFilters = GetStringToArray($('#SelectedOtherFilters').val());

	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate,
		"StageName": $('#StageName').val(),
		"StatusIds": StatusIds,
		"PriorityIds": PriorityIds,
		"RescheduledIds": RescheduledIds,
		"ResponsibilityIds": ResponsibilityIds,
		"JobTypeIds": JobTypeIds,
		"OtherFilters": OtherFilters,
		"isSearchedClick": isSearchedClick,

		//for export
		"ComponentTitle": $("#ComponentTitle").val(),
		"StatusTitles": StatusTitles,
		"PriorityTitles": PriorityTitles,
		"ResponsiblityTitles": ResponsibilityTitles,
		"RescheduleTitles": RescheduleTitles,
		"JobTypeTitles": JobTypeTitles,
		"OtherFilterTitles": OtherFilterTitles,
	};

	input = Object.assign(input, GetHierarchyTreeFromHidden());
	return input;
}

function LoadWorkBasketDetailsList(isSearchedClick) {

	var input = GetInputForSearch(isSearchedClick);

	$('#dtPMSWorkBasketGrid').DataTable().destroy();
	gridWorkBasketList = $('#dtPMSWorkBasketGrid').DataTable({
		//"dom": '<<"row mb-3"<"col-12 col-md-4 offset-md-0 col-lg-3 offset-lg-5 col-xl-3 offset-xl-4 dt-infomation"i><"col-md-3 col-lg-2 col-xl-2 filters-data"><"col-12 col-md-5 col-lg-3 col-xl-3"f>><rt><"clearfix"<"float-left"l><""p>>>',
		//"dom": '<"row"<"col-12 col-md-6 col-lg-6 col-xl-6"i><"col-12 col-md-6 col-lg-6 col-xl-6 search-filter"f>>' +
		//	'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false,
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		"order": [],
		"language": {
			"emptyTable": "No details available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/PlannedMaintenance/GetWorkBasketDetailsList",
			"type": "POST",
			"data":
			{
				"input": input
			},
			"datatype": "json"
		},
		buttons: [
			'copy', 'csv',
			{
				extend: 'excelHtml5',
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
				filename: function () {
					return 'Planned Maintenance Report';
				},
				title: "Planned Maintenance Report",
				customize: function (xlsx) {
					CustomizedExcelHeader(xlsx, 2);
				},
				messageTop: function () {
					return 'Vessel : ' + $('#VesselName').val() + '\nMonth : ' + moment(endDate).format(DateFormat) + ' | ' + GetSelectedStage($('#StageName').val());
				}
			},
			'pdf', 'print'
		],
		"createdRow": function (row, data, dataIndex) {
			if (data.isCritical == true) {
				$(row).addClass('text-danger');
			}
		},
		"columns": [
			{
				className: "data-icon-align d-none d-md-table-cell",
				orderable: false,
				width: "10px",
				render: function (data, type, full, meta) {
					if (full.channelCount > 0) {
						return GetChatBaseIcons(full.identifier, full.channelCount, full.messageDetailsJSON);
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
						return GetNotesBaseIcons(full.identifier, full.notesCount, full.messageDetailsJSON);
					} else {
						return '';
					}
				}
			},
			{
				className: "data-icon-align tdblock d-md-none d-lg-none d-xl-none",
				orderable: false,
				width: "70px",
				render: function (data, type, full, meta) {
					if (full.channelCount > 0 || full.notesCount > 0) {
						return GetChatNotesBaseIcons(full.identifier, full.channelCount, full.notesCount, full.messageDetailsJSON);
					} else {
						return '';
					}
				}
			},
            {
				className: "tdblock td-row-header font-weight-600 data-text-align d-sm-none",
				"data": "job",
				width: "110px",
				render: function (data, type, full, meta) {
					let wolink = '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + GetActualCellData(data) + '</a > '

					let defectWoLink = '<a href = "/Defect/Details/?DefectDetails=' + full.defectDetailsUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + GetActualCellData(data) + '</a > '

					if (full.isDefectWorkOrder == true) {
						return GetActualCellData(defectWoLink);
					} else {
						return GetActualCellData(wolink);
					}
				}
			},
			{
				className: "data-text-align",
				"data": "type",
				width: "10px",
				render: function (data, type, full, meta) {
					return GetCellData('Job Type', data);
				}
			},
			{
				className: "data-datetime-align position-relative",
				"data": "dueDate",
				type: "date",
				width: "50px",
				render: function (data, type, full, meta) {
					var date = GetDateToString(data, DateFormat);
					if (type === "display") {
						var ShowStatus = GetActualCellData(date);
						if (full.isOverDueVisible) {
							ShowStatus += "<img src='/images/overdue.png' class='ml-1 d-sm-block d-md-none' width='' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Overdue job.'>";
							ShowStatus += "<i class='fa fa-caret-right over-due icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='OverDue Job.'></i>";
						}
						else if (full.isOverduePeriodVisible) {
							ShowStatus += "<img src='/images/overdue.png' class='ml-1 d-sm-block d-md-none' width='' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Overdue job prior from current month.'>";
							ShowStatus += "<i class='fa fa-caret-right over-due-period icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Overdue job prior from current month.'></i>";
						}
						return GetCellDataWithOutExportClass('Due Date', ShowStatus);
					}
					return date;

				}
			},
			{
				className: "d-none d-sm-table-cell data-text-align",
				"data": "job",
				width: "160px",
				render: function (data, type, full, meta) {
					let wolink = '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + GetActualCellData(data) + '</a > '

					let defectWoLink = '<a href = "/Defect/Details/?DefectDetails=' + full.defectDetailsUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + GetActualCellData(data) + '</a > '

					if (full.isDefectWorkOrder == true) {
						return GetCellData('Job Name',defectWoLink);
					} else {
						return GetCellData('Job Name',wolink);
					}
				}
			},
			{
				className: "data-text-align tdblock",
				"data": "componentName",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetCellData('Component Name', data);
				}
			},
			{
				className: "data-icon-align",
				width: "30px",
				render: function (data, type, full, meta) {
					var MappedJSAIcon = "";
					if (full.hasMappedJSA) {
						//Green JobSafetyAnalysisGeometry Geometry
						//tooltip - JsaTooltip
						MappedJSAIcon = "<img src='/images/JobSafetyGreen.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.jsaTooltip + "'>";
					}
					else {
						MappedJSAIcon = "";
					}

					//this property refers to IsJSAToBeMapped in Shipsure
					var PermitJSAIcon = "";
					if (full.hasPermitJSA) {
						//orange JobSafetyAnalysisGeometry Geometry
						//tooltip - JsaTooltip
						PermitJSAIcon = "<img src='/images/JobSafetyOrange.png' class='mr-2' width='' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.jsaTooltip + "'>";
					}
					else {
						PermitJSAIcon = "";
					}

					var IsPermitRequired = "";
					if (full.isJSAPermitRequired) {
						//orange JobSafetyAnalysisGeometry Geometry
						//tooltip - JsaTooltip
						IsPermitRequired = "<img src='/images/permit.png' class='' width='' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.isJSAPermitRequiredTooltip + "'>";
					}

					var RoundJobIcon = "";
					if (full.hasRoundsJobIcon) {
						//Gray RoundsJobGeometry Geometry
						//tooltip - Round Job
						RoundJobIcon = "<img src='/images/RoundJob.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Round Job.'>";
					}
					else {
						RoundJobIcon = "";
					}

					var RobLessThanReqIcon = "";
					if (full.isRobLessThanReq) {
						//Red PartsGeometry
						//tooltip - ROB is less than required.
						RobLessThanReqIcon = "<img src='/images/PartsGeometryRed.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='ROB is less than required.'>";
					}
					else {
						RobLessThanReqIcon = "";
					}

					var CriticalIcon = "";
					if (full.isCritical) {
						CriticalIcon = "<img src='/images/critical.png' class='mr-2' width='' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Critical.'>";
					}
					else {
						CriticalIcon = "";
					}

					var FinalUIElement = CriticalIcon + MappedJSAIcon + PermitJSAIcon + IsPermitRequired + RoundJobIcon + RobLessThanReqIcon

					return GetCellData('Specifics', FinalUIElement);
				}
			},
			{
				className: "data-text-align",
				"data": "status",
				width: "13px",
				render: function (data, type, full, meta) {
					return GetCellData('Status', data);
				}
			},
			{
				className: "data-text-align",
				"data": "interval",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetCellData('Interval', data);
				}
			},
			{
				className: "data-text-align",
				"data": "resp",
				width: "20px",
				render: function (data, type, full, meta) {
					let result = "<span data-toggle='tooltip' data-placement='bottom' title='" + full.respDescription + "'>";
					result += data;
					result += '</span>'
					return GetCellData('Resp.', result);
				}
			},
			{
				className: "data-number-align",
				type: "html-num",
				"data": "leftHours",
				width: "15px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = "";
						if (data != null && data != 'undefined' && data != '') {
							numberText = data;
							return GetCellData('Left Hours', numberText);
						}
						else {
							return GetCellData('Left Hours', numberText);
						}
					}
					return data;
				}
			},
			{
				className: "data-number-align",
				"data": "requiredSpareCount",
				width: "15px",
				render: function (data, type, full, meta) {
					var finalData = data;
					if (data != null && data != 'undefined' && (data != 0 || data != '0')) {
						finalData = '<a href="javascript:void(0)" class="requiredSpareDetails" data-component="' + full.componentName + '" data-request="' + full.plannedMaintenanceDetailsRequestURL + '">' + data + '</a>';
					}
					return GetExportCellData('Req. Spares', finalData);
				}
			},
		],
		"initComplete": function (settings, data) {
			SetAppliedFilterAndCount();
		}
	});
	//$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata"  class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');

	$('#dtPMSWorkBasketGrid').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});

}

function BindSummary() {

	var request =
	{
		"VesselId": $('#EncryptedVesselId').val()
	}
	$.ajax({
		url: "/PlannedMaintenance/GetSummaryDetails",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		success: function (data) {
			if (data != null) {
				$('#spanSchedule').text(data.due);
				SummaryColorCode('#spanSchedule', data.due, 'txt-blue');

				$('#spanPlannedFor').text(data.plannedFor);
				SummaryColorCode('#spanPlannedFor', data.plannedFor, 'txt-blue');

				$('#spanReqResch').text(data.reqReschedule);
				SummaryColorCode('#spanReqResch', data.reqReschedule, 'txt-blue');

				$('#spanOverdue').text(data.overdue);
				SummaryColorCode('#spanOverdue', data.overdue, 'txt-red');

				$('#spanCriticalOverdue').text(data.criticalOverdue);
				SummaryColorCode('#spanCriticalOverdue', data.criticalOverdue, 'txt-red');

				$('#spanCritical').text(data.critical);
				SummaryColorCode('#spanCritical', data.critical, 'txt-blue');

				$('#spanCompletedWO').text(data.completedWO);
				SummaryColorCode('#spanCompletedWO', data.completedWO, 'txt-blue');

				//setting url
				//Remove all click event
				$(".click-event-off").off('click');
				$("#aScheduleNav").click(function () {
					GetPMSSummaryDetails(data.dueURL);
				});

				$("#aPlannedForNav").click(function () {
					GetPMSSummaryDetails(data.plannedForURL);
				});

				$("#aReqReschNav").click(function () {
					GetPMSSummaryDetails(data.reqRescheduleURL);
				});

				$("#aOverdueNav").click(function () {
					GetPMSSummaryDetails(data.overdueURL);
				});

				$("#aCriticalOverdueNav").click(function () {
					GetPMSSummaryDetails(data.criticalOverdueURL);
				});

				$("#aCriticalNav").click(function () {
					GetPMSSummaryDetails(data.criticalURL);
				});

				$("#aCompletedWONav").click(function () {
					GetPMSSummaryDetails(data.completedUrl);
				});
			}
		},
		complete: function () {
			$(parent).find('.pms-panel').unblock();
		}
	});

}

function GetPMSSummaryDetails(pmsUrl) {
	$.ajax({
		url: "/PlannedMaintenance/GetPMSSummaryDetails",
		type: "POST",
		"data": { "plannedMaintenanceUrl": pmsUrl, "encryptedVesselId": $('#EncryptedVesselId').val() },
		success: function (data) {
			var responce = data.data;
			SetHiddenFields(responce);
			SetFilters();

			SetMobileTab();
			IsSearchClickedForExport = false;
			LoadWorkBasketDetailsList(false);

		}
	});
}

////-------Selections -------------//

function LoadWBStatusTree() {
	$("#WBStatusTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/PlannedMaintenance/GetWorkBasketStatusTreeList",
			dataType: "json"
		}),
		init: function (event, data) {
			SetWBStatusTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetWBStatusTree() {
	StatusTitles = '';
	console.log("$('#SelectedWBStatusIds').val()", $('#SelectedWBStatusIds').val());

	$("#WBStatusTree").fancytree("getTree").visit(function (node) {
		var treeIdsList = $('#SelectedWBStatusIds').val().split(',');
		treeIdsList.forEach(function () {
			if (treeIdsList.includes(node.key)) {
				node.setSelected(true);
				StatusTitles += node.title + ",";
			}
			else {
				node.setSelected(false);
			}
		});
	});
}

function GetWBStatusTree() {
	var tree = $('#WBStatusTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	StatusTitles = nodes.map(x => x.title).join(",");
	//$('#SelectedWBStatusIds').val(selectednodes.join(","));
	return selectednodes;
}

function LoadWBPriorityTree() {
	$("#WBPriorityTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/PlannedMaintenance/GetWorkBasketPriorityTreeList",
			dataType: "json"
		}),
		init: function (event, data) {
			SetPriorityTreeTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetPriorityTreeTree() {
	console.log("$('#SelectedWBPriorityIds').val()", $('#SelectedWBPriorityIds').val());

	PriorityTitles = '';
	$("#WBPriorityTree").fancytree("getTree").visit(function (node) {
		var treeIdsList = $('#SelectedWBPriorityIds').val().split(',');
		treeIdsList.forEach(function () {
			if (treeIdsList.includes(node.key)) {
				PriorityTitles += node.title + ",";
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
}

function GetWBPriorityTree() {
	var tree = $('#WBPriorityTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	PriorityTitles = nodes.map(x => x.title).join(",");
	//$('#SelectedWBStatusIds').val(selectednodes.join(","));
	return selectednodes;
}

function LoadWBResponsibilityTree() {
	$("#WBResponsibilityTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/PlannedMaintenance/GetWorkBasketResponsibilityTreeList",
			data: { input: $('#EncryptedVesselId').val() },
			dataType: "json"
		}),
		init: function (event, data) {
			SetResponsibilityTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetResponsibilityTree() {
	ResponsibilityTitles = '';
	$("#WBResponsibilityTree").fancytree("getTree").visit(function (node) {
		var treeIdsList = $('#SelectedWBResponsibilityIds').val().split(',');
		treeIdsList.forEach(function () {
			if (treeIdsList.includes(node.key)) {
				ResponsibilityTitles += node.title + ",";
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
}

function GetWBResponsibilityTree() {
	var tree = $('#WBResponsibilityTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	ResponsibilityTitles = nodes.map(x => x.title).join(",");
	//$('#SelectedWBStatusIds').val(selectednodes.join(","));
	return selectednodes;
}

function LoadWBRescheduledTree() {
	$("#WBRescheduledTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/PlannedMaintenance/GetWorkBasketRescheduledTreeList",
			dataType: "json"
		}),
		init: function (event, data) {
			SetRescheduledTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetRescheduledTree() {
	RescheduleTitles = '';
	$("#WBRescheduledTree").fancytree("getTree").visit(function (node) {
		var treeIdsList = $('#SelectedWBRescheduledIds').val().split(',');
		treeIdsList.forEach(function () {
			if (treeIdsList.includes(node.key)) {
				RescheduleTitles += node.title + ",";
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
}

function GetWBRescheduledTree() {
	var tree = $('#WBRescheduledTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	RescheduleTitles = nodes.map(x => x.title).join(",");
	//$('#SelectedWBStatusIds').val(selectednodes.join(","));
	return selectednodes;
}

function LoadWBJobTypeTree() {
	$("#WBJobTypeTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/PlannedMaintenance/GetJobType",
			dataType: "json"
		}),
		init: function (event, data) {
			SetJobTypeTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetJobTypeTree() {
	JobTypeTitles = '';
	$("#WBJobTypeTree").fancytree("getTree").visit(function (node) {
		var treeIdsList = $('#SelectedWBJobTypeIds').val().split(',');
		treeIdsList.forEach(function () {
			if (treeIdsList.includes(node.key)) {
				JobTypeTitles += node.title + ",";
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
}

function GetWBJobTypeTree() {
	var tree = $('#WBJobTypeTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	JobTypeTitles = nodes.map(x => x.title).join(",");
	return selectednodes;
}

function LoadWBOtherFiltersTree() {
	$("#WBOtherFiltersTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/PlannedMaintenance/GetOtherFilters",
			dataType: "json"
		}),
		init: function (event, data) {
			SetOtherFiltersTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetOtherFiltersTree() {
	OtherFilterTitles = '';
	$("#WBOtherFiltersTree").fancytree("getTree").visit(function (node) {
		var treeIdsList = $('#SelectedOtherFilters').val().split(',');
		treeIdsList.forEach(function () {
			if (treeIdsList.includes(node.key)) {
				OtherFilterTitles += node.title + ",";
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
}

function GetWBOtherFiltersTree() {
	var tree = $('#WBOtherFiltersTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	OtherFilterTitles = nodes.map(x => x.title).join(",");
	return selectednodes;
}

function LoadWBHierarchyTree() {
	$("#WBHierarchyTree").fancytree({
		checkbox: false,
		selectMode: 1,
		icon: false,
		source:
			$.ajax({
				url: "/PlannedMaintenance/GetHierarchyTree",
				dataType: "json",
				data: { VesselId: $('#EncryptedVesselId').val() }
			}),
		lazyLoad: function (event, data) {
			var input = {
				VesselId: $('#EncryptedVesselId').val()
			}
			if (!$.isEmptyObject(data.node.data)) {
				let additionalData = data.node.data.additionalData;

				input["SyaId"] = additionalData.syaId;
				input["CategoryId"] = additionalData.categoryId;
				input["ModuleId"] = additionalData.moduleId;
				input["AlternateTemplateId"] = additionalData.alternateTemplateId;
				input["ParentComponentId"] = additionalData.parentComponentId;
				input["CanAddComponent"] = additionalData.canAddComponent;
				input["ComponentId"] = additionalData.componentId;

				if (additionalData.componentId != null && additionalData.componentId != 'undefined') {
					input["RequestComponentId"] = additionalData.componentId;
					input["IsComponentClick"] = true;
				}
				if (additionalData.parentComponentId != null && additionalData.parentComponentId != 'undefined') {
					input["RequestComponentId"] = additionalData.parentComponentId;
				}
			}
			data.result =
				$.ajax({
					url: "/PlannedMaintenance/GetHierarchyTree",
					type: "POST",
					dataType: "json",
					data: input
				});
		},
	});
}


function GetHierarchyTree() {
	var node = $.ui.fancytree.getTree("#WBHierarchyTree").getActiveNode();
	if (node != null) {
		$("#ComponentTitle").val(node.title);
	}
	
	var result = {};
	if (!$.isEmptyObject(node) && !$.isEmptyObject(node.data)) {
		let additionalData = node.data.additionalData;
		if (additionalData.syaId != null && additionalData.syaId != 'undefined') {
			result["TopSystemAreaId"] = additionalData.syaId;
		}
		else if (additionalData.componentId != null && additionalData.componentId != 'undefined') {
			result["ComponentId"] = additionalData.componentId;
		}
		else {
			result["ParentComponentId"] = additionalData.parentComponentId;
			result["CategoryId"] = additionalData.categoryId;
		}
	}
	return result;
}

function GetHierarchyTreeFromHidden() {

	var topSystemAreaId = $("#TopSystemAreaId").val();
	var componentId = $("#ComponentId").val();
	var parentComponentId = $("#ParentComponentId").val();
	var categoryId = $("#CategoryId").val();

	var result = {};
	if (topSystemAreaId != "" && topSystemAreaId != 'undefined') {
		result["TopSystemAreaId"] = topSystemAreaId;
	}
	else if (componentId != "" && componentId != 'undefined') {
		result["ComponentId"] = componentId;
	}
	else {
		if (parentComponentId != "" && parentComponentId != 'undefined')
			result["ParentComponentId"] = parentComponentId;
		if (categoryId != "" && categoryId != 'undefined')
			result["CategoryId"] = categoryId;
	}

	return result;
}

function ClearHierarchySelection() {
	$("#ComponentTitle").val('');
	$.ui.fancytree.getTree("#WBHierarchyTree").expandAll(false);
	$.ui.fancytree.getTree("#WBHierarchyTree").activateKey(false);
}


//----- Common functions-----//

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

function GetDateToString(date, format) {
	var formattedDate = "";
	if (date != null) {
		var dateString = new Date(date);
		formattedDate = moment(dateString).format(format);
	}
	return formattedDate;
}

function DaterangePicker() {

	var start = moment($('#FromDate').val(), 'DD-MM-YYYY');
	var end = moment($('#ToDate').val(), 'DD-MM-YYYY');

	$("#dtrppmlist").caleran(
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


	$('#dtrppmlist').val(DateRangePickerLabelText);

}


function setDateDetails(startDate, endDate, isApplyClicked) {
	$('#dtrppmlist').html(startDate.format(DateFormat) + ' - ' + endDate.format(DateFormat));
	$("#tblspndtrinspectionlist").html(startDate.format(DateFormat) + "-" + endDate.format(DateFormat));
	selectedStartDate = startDate.format(DateFormat);
	selectedEndDate = endDate.format(DateFormat);
	if (isApplyClicked) {
		IsSearchClickedForExport = true;
		SetPageParameter(true);
		//LoadWorkBasketDetailsList();
	}
}

function GetCellDataWithOutExportClass(label, data) {
	return '<label>' + label + '</label> <br />' + data;
}


function CallMaintenanceHistory() {
	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val()
	};
	$.ajax({
		url: "/PlannedMaintenance/LoadMaintenanceHistoryList",
		type: "POST",
		data: input,
		success: function (data) {
			window.location.href = data;
		}
	});
}

function SetPageParameter(isSearchClicked) {

	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate,
		"StageName": $('#StageName').val(),
		"StatusIds": GetWBStatusTree(),
		"PriorityIds": GetWBPriorityTree(),
		"RescheduledIds": GetWBRescheduledTree(),
		"ResponsibilityIds": GetWBResponsibilityTree(),
		"JobTypeIds": GetWBJobTypeTree(),
		"OtherFilters": GetWBOtherFiltersTree(),
		"isSearchedClick": isSearchClicked,
	};
	input = Object.assign(input, GetHierarchyTree());
	input["ComponentTitle"]= $("#ComponentTitle").val();

	$.ajax({
		url: "/PlannedMaintenance/SetPageParameter",
		type: "POST",
		data: input,
		success: function (data) {
			if (data != null) {

				SetHiddenFields(data.data);
			}
		},
		complete: function () {
			LoadWorkBasketDetailsList(isSearchClicked);
		}
	});
}

function SetHiddenFields(response) {

	$("#SelectedWBStatusIds").val(response.selectedWBStatusIds);
	$("#SelectedWBPriorityIds").val(response.selectedWBPriorityIds);
	$("#SelectedWBResponsibilityIds").val(response.selectedWBResponsibilityIds);
	$("#SelectedWBRescheduledIds").val(response.selectedWBRescheduledIds);
	$("#SelectedWBJobTypeIds").val(response.selectedWBJobTypeIds);
	$("#SelectedOtherFilters").val(response.selectedOtherFilters);
	$("#FromDate").val(response.fromDate);
	$("#ToDate").val(response.toDate);
	$("#EncryptedVesselId").val(response.encryptedVesselId);
	$('#StageName').val(response.stageName);
	$('#isSearchedClick').val(response.isSearchedClick);
	$("#ActiveMobileTabClass").val(response.activeMobileTabClass);
	$("#TopSystemAreaId").val(response.topSystemAreaId);
	$("#ComponentId").val(response.componentId);
	$("#ParentComponentId").val(response.parentComponentId);
	$("#CategoryId").val(response.categoryId);
	$("#ComponentTitle").val(response.componentTitle);
	$("#GridSubTitle").val(response.gridSubTitle);
}

function GetStringToArray(strData) {

	if (strData != null && strData != "" && strData != undefined) {
		return strData.split(',');
	}
	else {
		var result = [];
		return result;
	}
}

function SetMobileTab() {
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
	}
}

function SetFilters() {
	SetWBStatusTree();
	SetPriorityTreeTree();
	SetRescheduledTree();
	SetResponsibilityTree();
	SetJobTypeTree();
	SetOtherFiltersTree();
}

function SetAppliedFilterAndCount() {

	var filterCount = 0;
	var ComponentTitle = $("#ComponentTitle").val();

	var WBStatusTreeNode = $('#WBStatusTree').fancytree('getTree').getSelectedNodes();
	var WBStatusArray = GetUniqueChildArr(WBStatusTreeNode);
	var WBStatusHtmlElement = GetFilterHtmlElement(WBStatusArray);

	var WBPriorityNode = $('#WBPriorityTree').fancytree('getTree').getSelectedNodes();
	var WBPriorityArray = GetUniqueChildArr(WBPriorityNode);
	var WBPriorityHtmlElement = GetFilterHtmlElement(WBPriorityArray);

	var WBResponsibilityNode = $('#WBResponsibilityTree').fancytree('getTree').getSelectedNodes();
	var WBResponsibilityArray = GetUniqueChildArr(WBResponsibilityNode);
	var WBResponsibilityHtmlElement = GetFilterHtmlElement(WBResponsibilityArray);

	var WBRescheduledNode = $('#WBRescheduledTree').fancytree('getTree').getSelectedNodes();
	var WBRescheduledArray = GetUniqueChildArr(WBRescheduledNode);
	var WBRescheduledHtmlElement = GetFilterHtmlElement(WBRescheduledArray);

	var WBJobTypeNode = $('#WBJobTypeTree').fancytree('getTree').getSelectedNodes();
	var WBJobTypeArray = GetUniqueChildArr(WBJobTypeNode);
	var WBJobTypeHtmlElement = GetFilterHtmlElement(WBJobTypeArray);

	var WBOtherNode = $('#WBOtherFiltersTree').fancytree('getTree').getSelectedNodes();
	var WBOtherArray = GetUniqueChildArr(WBOtherNode);
	var WBOtherHtmlElement = GetFilterHtmlElement(WBOtherArray);

	if (WBStatusArray.size > 0) {
		$('#filterStatus').html(WBStatusHtmlElement);
		filterCount = filterCount + WBStatusArray.size;
		$("#filterCard2").show();

		FilterCountSet(WBStatusArray.size, "#statusFilterCount");
	}
	else {
		$('#filterStatus').html("");
		$("#filterCard2").hide();
		FilterCountSet(0, "#statusFilterCount");
	}

	if (WBPriorityArray.size > 0) {
		$('#filterPriority').html(WBPriorityHtmlElement);
		filterCount = filterCount + WBPriorityArray.size;
		$("#filterCard3").show();
		FilterCountSet(WBPriorityArray.size, "#priorityFilterCount");
	}
	else {
		$("#filterCard3").hide();
		$('#filterPriority').html("");
		FilterCountSet(0, "#priorityFilterCount");
	}

	if (WBResponsibilityArray.size > 0) {
		$('#filterResponsibility').html(WBResponsibilityHtmlElement);
		filterCount = filterCount + WBResponsibilityArray.size;
		$("#filterCard4").show();
		FilterCountSet(WBResponsibilityArray.size, "#responsibilityFilterCount");
	}
	else {
		$('#filterResponsibility').html("");
		$("#filterCard4").hide();
		FilterCountSet(0, "#responsibilityFilterCount");
	}
	
	if (WBRescheduledArray.size > 0) {
		$('#filterReschedule').html(WBRescheduledHtmlElement);
		filterCount = filterCount + WBRescheduledArray.size;
		$("#filterCard5").show();
		FilterCountSet(WBRescheduledArray.size, "#rescheduleFilterCount");
	}
	else {
		$('#filterReschedule').html("");
		$("#filterCard5").hide();
		FilterCountSet(0, "#rescheduleFilterCount");
	}

	if (WBJobTypeArray.size > 0) {
		$('#filterJobType').html(WBJobTypeHtmlElement);
		filterCount = filterCount + WBJobTypeArray.size;
		$("#filterCard6").show();
		FilterCountSet(WBJobTypeArray.size, "#jobtypeFilterCount");
	}
	else {
		$('#filterJobType').html("");
		$("#filterCard6").hide();
		FilterCountSet(0, "#jobtypeFilterCount");
	}

	if (WBOtherArray.size > 0) {
		$('#filterOthers').html(WBOtherHtmlElement);
		filterCount = filterCount + WBOtherArray.size;
		$("#filterCard7").show();
		FilterCountSet(WBOtherArray.size, "#otherFilterCount");
	}
	else {
		$('#filterOthers').html("");
		$("#filterCard7").hide();
		FilterCountSet(0, "#otherFilterCount");
	}

	if (!IsNullOrEmptyOrUndefined(ComponentTitle)) {
		$('#filterComponents').html(ComponentTitle);
		filterCount++;
		$("#filterCard1").show();
	}
	else {
		$('#filterComponents').html("");
		$("#filterCard1").hide();
	}

	if (filterCount > 0) {
		$(".filter-design, .clear-filter").show();

		$("#divfilterhide").removeClass('btn-dark-grey');
		$("#divfilterhide").addClass('btn-info');
	}
	else {
		$(".filter-design, .clear-filter").hide();

		$("#divfilterhide").removeClass('btn-info');
		$("#divfilterhide").addClass('btn-dark-grey');
	}

	$('#appliedFilterCount').text(filterCount);
	AppendGridTitle();
}

function GetUniqueChildArr(nodedata) {
	var parentArray = new Map();
	var statusArray = new Map();

	nodedata.forEach(function (node) {
		statusArray.set(node.title, node.title);
		parentArray.set(node.parent.title, node.parent.title);
	});
	parentArray.forEach((value, key) => {
		if (statusArray.has(key)) {
			statusArray.delete(key);
		}
	});

	return statusArray;
}

function GetFilterHtmlElement(statusArray) {
	var htmlElement = "";

	statusArray.forEach((value, key) => {
		htmlElement += '<div class="col-12 col-md-6 col-lg-4 col-xl-4">';
		htmlElement += '<div class="sub-section">';
		htmlElement += '<div class="dashboard-counters-label"><span id="">' + value + '</span></div></div></div>';
	});

	return htmlElement;
}

function AppendGridTitle() {

	$("#tableSubTitle").hide();
	var subtitle = $("#GridSubTitle").val();
	if (!IsNullOrEmptyOrUndefined(subtitle) && subtitle != "All")
	{
		$("#tblspndtrinspectionlist").hide();
		$("#tableSubTitle").show();
		$("#tableSubTitle").text(" - " + subtitle);
	}
	else {
		$("#tblspndtrinspectionlist").show();
	}
}

function clearFrom() {
	$('#StageName').val("All");
	var start = moment().startOf('month');
	var end = moment().endOf('month');

	selectedStartDate = start.format(DateFormat);
	selectedEndDate = end.format(DateFormat);
	$("#dtrppmlist").html(start.format(DateFormat) + " - " + end.format(DateFormat));
	$("#tblspndtrinspectionlist").html(start.format(DateFormat) + " - " + end.format(DateFormat));
	$("#SelectedWBStatusIds").val("");
	$("#SelectedWBPriorityIds").val("");
	$("#SelectedWBResponsibilityIds").val("");
	$("#SelectedWBRescheduledIds").val("");
	$("#SelectedWBJobTypeIds").val("");
	$("#SelectedOtherFilters").val("");
	$("#isSearchedClick").val("");

	$("#TopSystemAreaId").val("");
	$("#ComponentId").val("");
	$("#ParentComponentId").val("");
	$("#CategoryId").val("");

	SetWBStatusTree();
	SetPriorityTreeTree();
	SetRescheduledTree();
	SetResponsibilityTree();
	SetJobTypeTree();
	SetOtherFiltersTree();
	ClearHierarchySelection();
	//LoadWorkBasketDetailsList(false);
	IsSearchClickedForExport = false;
	SetPageParameter(false);

	FilterCountSet(0,".filtercount");
}

function GetTreeNodeLength(element) {
	var treeData = $(element);
	var selectedNodes = treeData.fancytree('getTree').getSelectedNodes();
	var TreeNodeArray = GetUniqueChildArr(selectedNodes);
	return TreeNodeArray.size;
}

function FilterCountSet(nodeLength , elementCount) {
	$(elementCount).text(nodeLength);
	if (nodeLength > 0)
		AddClassIfAbsent($(elementCount).parent('div'), 'active');
	else
		RemoveClassIfPresent($(elementCount).parent('div'), 'active');
}
