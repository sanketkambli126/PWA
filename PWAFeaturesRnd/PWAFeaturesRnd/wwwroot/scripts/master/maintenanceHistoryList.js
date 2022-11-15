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
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, ToastrAlert, GetCookie, ConvertDecimalNumberToString, headerReadMore, SetHeaderMargin, MobileTab_Overview, MobileTab_List, Mobile_Tabs, IsNullOrEmptyOrUndefined, AddClassIfAbsent, RemoveClassIfPresent, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js";
import { DateRangePickerCancelText, DateRangePickerLabelText, MaintenanceHistoryListPageKey, Tab1, Tab2, MobileScreenSize } from "../common/constants.js";

var IsMobile = false;
var gridWorkBasketList;
const DateFormat = "DD MMM YYYY";
var selectedStartDate;
var selectedEndDate;
var ActiveMobileTabClass;

var IsSearchClickedForExport = false;
var ResponsibilityTitles, RescheduleTitles, JobTypeTitles;

$(document).on('click', '#aRemoveFilter', function () {
	fnClearFilter();
	SetPageParameter(false);
});

$(document).ready(function () {
	AjaxError();
	if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
		IsMobile = true;
	} else {
		IsMobile = false;
	}


	$('#mobileactiontoggle').click(function () {
		$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
	});
	//$(".dropdown-toggle").dropdown();

	$(document).click(function () {
		if ($("#mobileActiondropdown").hasClass('show')) {
			$("#mobileActiondropdown").removeClass('show');
		}
	});

	if (screen.width < 760) {
		headerReadMore('headershowmorewrapper', 'header');
		SetHeaderMargin();
	}

	$('.height-equal').matchHeight();
	$('.height-equal2').matchHeight();
	RegisterTabSelectionEvent('.mobileTabClick', MaintenanceHistoryListPageKey);
	AddLoadingIndicator();
	RemoveLoadingIndicator();
	DaterangePicker();
	LoadWBJobTypeTree();
	LoadWBResponsibilityTree();
	LoadWBRescheduledTree();
	LoadWBHierarchyTree();

	$('#btnSearch').click(function () {
		SetPageParameter(true);
	});

	$('#btnClear').click(function () {
		fnClearFilter();
		SetPageParameter(false);
	});

	SetMobileTab();
	loadMaintenanceHistoryList($("#isSearchedClick").val() == "True" ? true : false);

	BindSummary();

	//Sidebar back
	$('.back ,.btnPlannedMaintenance').click(function () {
		$.ajax({
			url: "/Base/GetSourceURL",
			type: "POST",
			dataType: "JSON",
			data: {
				"pageKey": MaintenanceHistoryListPageKey
			},
			beforeSend: function (xhr) {
			},
			success: function (data) {
				if (data != null) {
					window.location.replace(data);
				}
			},
			compelet: function () {

			}
		});
	});

	$('.btnExport').click(() => {

		var input = GetInputForSearch(IsSearchClickedForExport);

		$.ajax({
			url: "/PlannedMaintenance/ExportToExcelMaintenanceHistoryList",
			type: "POST",
			xhrFields: {
				responseType: 'blob'
			},
			"data": {
				"maintenanceHistoryRequest": input
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


$('#WBJobTypeTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBJobTypeTree"), "#jobtypeFilterCount");
});

$('#WBResponsibilityTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBResponsibilityTree"), "#responsibilityFilterCount");
});

$('#WBRescheduledTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#WBRescheduledTree"), "#rescheduleFilterCount");
});


$('#chckCritical').change(function () {
	if (this.checked) {
		FilterCountSet(1, "#criticalFilterCount");
	}
	else {
		FilterCountSet(0, "#criticalFilterCount");
    }
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

function GetInputForSearch(isSearchedClick) {
	let IsCritical = null;
	if ($('#chckCritical').is(':checked')) {
		IsCritical = true
	}

	var syaId = $("#TopSystemAreaId").val()
	var componentId = $("#ComponentId").val();
	var parentComponentId = $("#ParentComponentId").val();
	var categoryId = $("#CategoryId").val();

	var RescheduledIds = GetStringToArray($('#SelectedWBRescheduledIds').val());
	var ResponsibilityIds = GetStringToArray($('#SelectedWBResponsibilityIds').val());
	var JobTypeIds = GetStringToArray($('#SelectedWBJobTypeIds').val());

	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate,
		"JobTypeIds": JobTypeIds,
		"RescheduledIds": RescheduledIds,
		"ResponsibilityIds": ResponsibilityIds,
		"IsCritical": IsCritical,
		"isSearchedClick": isSearchedClick,
		"TopSystemAreaId": syaId,
		"ComponentId": componentId,
		"CategoryId": categoryId,
		"ParentComponentId": parentComponentId,
		"StageName": $('#StageName').val(),

		//export
		"ComponentTitle": $("#ComponentTitle").val(),
		"ResponsiblityTitles": ResponsibilityTitles,
		"RescheduleTitles": RescheduleTitles,
		"JobTypeTitles": JobTypeTitles,
	};
	return input;
}

function fnClearFilter() {
	var start = moment().subtract(30, 'days'); 
	var end = moment();

	selectedStartDate = start.format(DateFormat);
	selectedEndDate = end.format(DateFormat);
	$("#dtrmaintenancehistory").html(start.format(DateFormat) + " - " + end.format(DateFormat));
	$("#tblspndtrinspectionlist").html(start.format(DateFormat) + " - " + end.format(DateFormat));
	$('#SelectedWBJobTypeIds').val('');
	$('#SelectedWBRescheduledIds').val('');
	$('#SelectedWBResponsibilityIds').val('');


	$("#TopSystemAreaId").val('');
	$("#ComponentId").val('');
	$("#ParentComponentId").val('');
	$("#CategoryId").val('');

	SetJobTypeTree();
	SetRescheduledTree();
	SetResponsibilityTree();
	$('#chckCritical').prop("checked", false);
	ClearHierarchySelection();
	FilterCountSet(0, ".filtercount");
}

function loadMaintenanceHistoryList(isSearchedClick) {
	IsSearchClickedForExport = isSearchedClick;
	var input = GetInputForSearch(isSearchedClick);

	$('#maintenancehistorygrid').DataTable().destroy();
	gridWorkBasketList = $('#maintenancehistorygrid').DataTable({
		//"dom": '<<"row mb-3"<"col-12 col-md-4 offset-md-0 col-lg-3 offset-lg-5 col-xl-3 offset-xl-4 dt-infomation"i><"col-md-3 col-lg-2 col-xl-2 filters-data"><"col-12 col-md-5 col-lg-3 col-xl-3"f>><rt><"clearfix"<"float-left"l><""p>>>',
		//"dom": '<"row"<"col-12 col-md-6 col-lg-6 col-xl-6"i><"col-12 col-md-6 col-lg-6 col-xl-6 search-filter"f>>' +
		//	'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
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
			"emptyTable": "No data available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/PlannedMaintenance/GetWorkBasketHistoryDetailsList",
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
		"columns": [
			{
				className: "tdblock data-text-align d-sm-none",
				data: "jobName",
				width: "265px",
				render: function (data, type, full, meta) {

					var txtresult = "";
					if (full.isCritical) {
						txtresult = "<a href ='/PlannedMaintenance/MaintenanceHistoryDetails/?PlannedMaintenanceDetails=" + full.plannedMaintenanceDetailsRequestURL + "&VesselId=" + full.encryptedVesselId + "'><span>" + full.jobName + "  <i class='fa fa-exclamation-circle txt-red' data-html='true' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Critical'></i> </span></a>";
					}
					else {
						txtresult = "<a href = '/PlannedMaintenance/MaintenanceHistoryDetails/?PlannedMaintenanceDetails=" + full.plannedMaintenanceDetailsRequestURL + "&VesselId=" + full.encryptedVesselId + "'>" + full.jobName + "</a>";
					}

					return GetActualCellData(txtresult);
				}
			},
			{
				className: "tdblock data-text-align d-sm-none",
				data: "componentName",
				width: "265px",
				render: function (data, type, full, meta) { return GetCellData('Component', data); }
			},
			{
				className: "data-datetime-align d-sm-none",
				data: "doneDate",
				width: "55px",
				render: function (data, type, full, meta) {
					return GetCellData('Done Date', data);
				}
			},
			{
				className: "data-datetime-align d-none d-sm-table-cell",
				data: "doneDate",
				width: "55px",
				render: function (data, type, full, meta) {
					return GetActualCellData("<a href = '/PlannedMaintenance/MaintenanceHistoryDetails/?PlannedMaintenanceDetails=" + full.plannedMaintenanceDetailsRequestURL + "&VesselId=" + full.encryptedVesselId + "'>" + full.doneDate + "</a>");
				}
			},
			{
				className: "tdblock data-text-align d-none d-sm-table-cell",
				data: "componentName",
				width: "265px",
				render: function (data, type, full, meta) { return GetCellData('Component', data); }
			},
			{
				className: "tdblock data-text-align d-none d-sm-table-cell",
				data: "jobName",
				width: "265px",
				render: function (data, type, full, meta) {
					var txtresult = "";
					if (full.isCritical) {
						txtresult = "<span>" + full.jobName + "  <i class='fa fa-exclamation-circle txt-red' data-html='true' data-toggle='tooltip' data-placement='bottom' title='' data-original-title='Critical'></i> </span>";
					}
					else {
						txtresult = "<span>" + full.jobName + "</span>";
					}
					return GetActualCellData(txtresult);
				}
			},
			{
				className: "data-text-align",
				data: "dept",
				width: "25px",
				render: function (data, type, full, meta) { return GetCellData('Dept', data); }
			},
			{
				className: "data-text-align",
				data: "resp",
				width: "35px",
				render: function (data, type, full, meta) { return GetCellData('Resp', data); }
			},
			{
				className: "data-text-align",
				data: "type",
				width: "30px",
				render: function (data, type, full, meta) { return GetCellData('Type', data); }
			},
			{
				className: "data-text-align",
				data: "interval",
				width: "50px",
				render: function (data, type, full, meta) { return GetCellData('Interval', data); }
			}
		],
		"initComplete": function (settings, data) {
			SetAppliedFilterAndCount();
		}
	});
	//$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata"  class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');
	$('#maintenancehistorygrid').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip();
	});
}

function BindSummary() {

	var request =
	{
		"VesselId": $('#EncryptedVesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate
	};

	$.ajax({
		url: "/PlannedMaintenance/GetMaintenanceHistorySummary",
		type: "POST",
		dataType: "JSON",
		data: {
			"request": request
		},
		success: function (data) {
			if (data != null) {
				$('#spanOverhaul').text(data.overhaulCount);
				$('#spanRescheduled').text(data.rescheduledCount);

				//Remove all click event
				$(".click-event-off").off('click');

				$("#aOverhaul").click(function () {
					GetMaintenanceHistorySummaryUrlData(data.overhaulURL);
				});

				$("#aRescheduled").click(function () {
					GetMaintenanceHistorySummaryUrlData(data.rescheduledURL);
				});
			}
		}
	});
}

function GetMaintenanceHistorySummaryUrlData(protectedUrl) {
	$.ajax({
		url: "/PlannedMaintenance/GetMaintenanceHistorySummaryDetails",
		type: "POST",
		dataType: "JSON",
		data: { "protectedUrl": protectedUrl, "encryptedVesselId": $('#EncryptedVesselId').val() },
		success: function (data) {
			var responce = data.data;
			SetHiddenFields(responce);
			SetFilters();
			SetMobileTab();
			loadMaintenanceHistoryList(false);
		}
	});
}

////-------Selections -------------//

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
	if (!$.isEmptyObject(node) && !$.isEmptyObject(node.data)) {
		return node.data.additionalData;
	}
	return null;
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

function DaterangePicker() {

	var start = moment($('#FromDate').val(), 'DD-MM-YYYY');
	var end = moment($('#ToDate').val(), 'DD-MM-YYYY');

	$("#dtrmaintenancehistory").caleran(
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

	$('#dtrmaintenancehistory').val(DateRangePickerLabelText);

}

function setDateDetails(startDate, endDate, isSearch) {
	$('#dtrmaintenancehistory').html(startDate.format(DateFormat) + ' - ' + endDate.format(DateFormat));
	$("#tblspndtrinspectionlist").html(startDate.format(DateFormat) + ' - ' + endDate.format(DateFormat));
	selectedStartDate = startDate.format(DateFormat);
	selectedEndDate = endDate.format(DateFormat);
	if (isSearch) {
		SetPageParameter(true);
	}
}

function SetPageParameter(isSearchClicked) {
	IsSearchClickedForExport = isSearchClicked;
	let IsCritical = null;
	if ($('#chckCritical').is(':checked')) {
		IsCritical = true
	}

	let syaId = null, componentId = null, parentComponentId = null, categoryId = null;

	var additionalData = GetHierarchyTree();

	if (additionalData != null) {
		syaId = additionalData.syaId;
		componentId = additionalData.componentId;
		parentComponentId = additionalData.parentComponentId;
		categoryId = additionalData.categoryId;
	}

	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate,
		"JobTypeIds": GetWBJobTypeTree(),
		"RescheduledIds": GetWBRescheduledTree(),
		"ResponsibilityIds": GetWBResponsibilityTree(),
		"IsCritical": IsCritical,
		"isSearchedClick": isSearchClicked,
		"TopSystemAreaId": syaId,
		"ComponentId": componentId,
		"CategoryId": categoryId,
		"ParentComponentId": parentComponentId,
		"StageName": $('#StageName').val()
	};

	input["ComponentTitle"] = $("#ComponentTitle").val();

	$.ajax({
		url: "/PlannedMaintenance/SetPageParameterPMSHistory",
		type: "POST",
		data: input,
		success: function (data) {
			if (data != null) {
				SetHiddenFields(data.data);
			}
		},
		complete: function () {
			loadMaintenanceHistoryList(isSearchClicked);
			BindSummary();
		}
	});
}

function SetHiddenFields(response) {
	$("#SelectedWBResponsibilityIds").val(response.selectedWBResponsibilityIds);
	$("#SelectedWBRescheduledIds").val(response.selectedWBRescheduledIds);
	$("#SelectedWBJobTypeIds").val(response.selectedWBJobTypeIds);
	$("#SelectedOtherFilters").val(response.selectedOtherFilters);
	$("#FromDate").val(response.fromDate);
	$("#ToDate").val(response.toDate);
	$("#EncryptedVesselId").val(response.encryptedVesselId);
	$('#StageName').val(response.stageName);
	$('#isSearchedClick').val(response.isSearchedClick);
	$("#TopSystemAreaId").val(response.topSystemAreaId);
	$("#ComponentId").val(response.componentId);
	$("#ParentComponentId").val(response.parentComponentId);
	$("#CategoryId").val(response.categoryId);
	$("#GridSubTitle").val(response.gridSubTitle);
	if (response.isCritical == true) {
		$("#chckCritical").prop('checked', true);
		FilterCountSet(1, "#criticalFilterCount");
	}
	else {
		$("#chckCritical").prop('checked', false);
		FilterCountSet(0, "#criticalFilterCount");
	}
	$("#ComponentTitle").val(response.componentTitle);
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
	SetRescheduledTree();
	SetResponsibilityTree();
	SetJobTypeTree();
}

function SetAppliedFilterAndCount() {

	var filterCount = 0;
	var ComponentTitle = $("#ComponentTitle").val();

	var WBResponsibilityNode = $('#WBResponsibilityTree').fancytree('getTree').getSelectedNodes();
	var WBResponsibilityArray = GetUniqueChildArr(WBResponsibilityNode);
	var WBResponsibilityHtmlElement = GetFilterHtmlElement(WBResponsibilityArray);

	var WBRescheduledNode = $('#WBRescheduledTree').fancytree('getTree').getSelectedNodes();
	var WBRescheduledArray = GetUniqueChildArr(WBRescheduledNode);
	var WBRescheduledHtmlElement = GetFilterHtmlElement(WBRescheduledArray);

	var WBJobTypeNode = $('#WBJobTypeTree').fancytree('getTree').getSelectedNodes();
	var WBJobTypeArray = GetUniqueChildArr(WBJobTypeNode);
	var WBJobTypeHtmlElement = GetFilterHtmlElement(WBJobTypeArray);
	
	if (WBResponsibilityArray.size > 0) {
		$('#filterResponsibility').html(WBResponsibilityHtmlElement);
		filterCount = filterCount + WBResponsibilityArray.size;
		$("#filterCard4").show();
	}
	else {
		$('#filterResponsibility').html("");
		$("#filterCard4").hide();
	}

	if (WBRescheduledArray.size > 0) {
		$('#filterReschedule').html(WBRescheduledHtmlElement);
		filterCount = filterCount + WBRescheduledArray.size;
		$("#filterCard5").show();
	}
	else {
		$('#filterReschedule').html("");
		$("#filterCard5").hide();
	}

	if (WBJobTypeArray.size > 0) {
		$('#filterJobType').html(WBJobTypeHtmlElement);
		filterCount = filterCount + WBJobTypeArray.size;
		$("#filterCard6").show();
	}
	else {
		$('#filterJobType').html("");
		$("#filterCard6").hide();
	}

	if ($('#chckCritical').is(':checked')) {
		filterCount++;
		$("#filterCritical").text("Yes");
		$("#filterCard2").show();
	}
	else {
		$("#filterCard2").hide();
		$("#filterCritical").text("No");
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
	if (!IsNullOrEmptyOrUndefined(subtitle)) {
		$("#tblspndtrinspectionlist").hide();
		$("#tableSubTitle").show();
		$("#tableSubTitle").text(" - " + subtitle);
	}
	else {
		$("#tblspndtrinspectionlist").show();
	}
}

function GetTreeNodeLength(element) {
	var treeData = $(element);
	var selectedNodes = treeData.fancytree('getTree').getSelectedNodes();
	var TreeNodeArray = GetUniqueChildArr(selectedNodes);
	return TreeNodeArray.size;
}

function FilterCountSet(nodeLength, elementCount) {
	$(elementCount).text(nodeLength);
	if (nodeLength > 0)
		AddClassIfAbsent($(elementCount).parent('div'), 'active');
	else
		RemoveClassIfPresent($(elementCount).parent('div'), 'active');
}
