import moment from "moment";

import { GetCookie, AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, MobileTab_Overview, MobileTab_List, Mobile_Tabs, AddClassIfAbsent, RemoveClassIfPresent, IsNullOrEmptyOrUndefinedLooseTyped, BackButton, RegisterTabSelectionEvent, FormatDate, SetHeaderMargin } from "../common/utilities.js";
import { GetCellData, GetExportData, GetFormattedDate } from "../common/datatablefunctions.js";
import { ApprovalListPageKey, MobileScreenSize, No, Tab2, Yes } from "../common/constants.js";

require('bootstrap');
const DateFormat = "DD MMM YYYY";
var dtPurchaseOrderPendingApproval, dtPoTenderAwaitingAuthorization, dtDefectsPendingClosure, dtPMSPendingClosure, dtPMSPendingRescheduleRequests, dtInspectionPendingClosure, dtAuditPendingClosure, dtJSAPendingApproval;

var statusColorMap = new Map();
statusColorMap.set(0, { textColor: "txt-orange" });
statusColorMap.set(1, { textColor: "txt-green" });
statusColorMap.set(5, { textColor: "text-grey" });
statusColorMap.set(6, { textColor: "txt-red" });

var riskColorMap = new Map();
riskColorMap.set(0, { textColor: "text-yellow" });
riskColorMap.set(1, { textColor: "txt-green" });
riskColorMap.set(7, { textColor: "txt-red" });
riskColorMap.set(6, { textColor: "txt-orange" });

$(document).on('click', '.approvalNode', function () {
	//add Active Class to header
	$('.approval-list').find('.list').removeClass("approval-active");
	$(this).parents('.list').addClass("approval-active");

	let headerNode = $(this).data('headernode');
	let nodeShortCode = $(this).data('node');

	//show hide section
	$('#divApprovalList').find('.approvalListSection').addClass("d-none")
	RemoveClassIfPresent('#table_' + headerNode + nodeShortCode, 'd-none');
	SetMobileTab();
	
	GetApprovalNode(headerNode, nodeShortCode);
	SetApprovalFilters(headerNode, nodeShortCode);
});

$("#dtPurchaseOrderPendingApproval").on('click', 'tbody tr', function () {
	var purchaseOrderUrl = dtPurchaseOrderPendingApproval.row(this).data().purchaseOrderUrl;
	var encryptedVesselId = dtPurchaseOrderPendingApproval.row(this).data().encryptedVesselId;
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/PurchaseOrder/Detail/?PurchaseOrderRequest=' + purchaseOrderUrl + '&VesselId=' + encryptedVesselId;
	}
});

$("#dtPoTenderAwaitingAuthorization").on('click', 'tbody tr', function () {
	var purchaseOrderUrl = dtPoTenderAwaitingAuthorization.row(this).data().purchaseOrderUrl;
	var encryptedVesselId = dtPoTenderAwaitingAuthorization.row(this).data().encryptedVesselId;
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/PurchaseOrder/Detail/?PurchaseOrderRequest=' + purchaseOrderUrl + '&VesselId=' + encryptedVesselId;
	}
});

$("#dtDefectsPendingClosure").on('click', 'tbody tr', function () {
	var defectDetailURL = dtDefectsPendingClosure.row(this).data().defectDetailURL;
	var encryptedVesselId = dtDefectsPendingClosure.row(this).data().encryptedVesselId;
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/Defect/Details/?DefectDetails=' + defectDetailURL + '&VesselId=' + encryptedVesselId;
	}
});

$("#dtPMSPendingClosure").on('click', 'tbody tr', function () {
	var plannedMaintenanceDetailsRequestURL = dtPMSPendingClosure.row(this).data().plannedMaintenanceDetailsRequestURL;
	var defectDetailsUrl = dtPMSPendingClosure.row(this).data().defectDetailsUrl;
	var encryptedVesselId = dtPMSPendingClosure.row(this).data().encryptedVesselId;
	var isDefectWorkOrder = dtPMSPendingClosure.row(this).data().isDefectWorkOrder;
	
	if ($(window).width() > MobileScreenSize) {
		
		if (isDefectWorkOrder == true) {
			window.location.href = window.location.protocol + "//" + window.location.host + '/Defect/Details/?DefectDetails=' + defectDetailsUrl + '&VesselId=' + encryptedVesselId;
		}
		else {
			window.location.href = window.location.protocol + "//" + window.location.host + '/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + plannedMaintenanceDetailsRequestURL + '&VesselId=' + encryptedVesselId;
		}
	}
});

$("#dtPMSPendingRescheduleRequests").on('click', 'tbody tr', function () {
	var plannedMaintenanceDetailsRequestURL = dtPMSPendingRescheduleRequests.row(this).data().plannedMaintenanceDetailsRequestURL;
	var encryptedVesselId = dtPMSPendingRescheduleRequests.row(this).data().encryptedVesselId;
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + plannedMaintenanceDetailsRequestURL + '&VesselId=' + encryptedVesselId;
	}
});

$("#dtInspectionPendingClosure").on('click', 'tbody tr', function () {
	var findingURL = dtInspectionPendingClosure.row(this).data().findingURL;
	var encryptedVesselId = dtInspectionPendingClosure.row(this).data().vesselId;
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/Inspection/Findings/?finding=' + findingURL + '&VesselId=' + encryptedVesselId;
	}
});

$("#dtAuditPendingClosure").on('click', 'tbody tr', function () {
	var findingURL = dtAuditPendingClosure.row(this).data().findingURL;
	var encryptedVesselId = dtAuditPendingClosure.row(this).data().vesselId;
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/Inspection/Findings/?finding=' + findingURL + '&VesselId=' + encryptedVesselId;
	}
});

$("#dtJSAPendingApproval").on('click', 'tbody tr', function () {
	var jsaDetails = dtJSAPendingApproval.row(this).data().jsaDetails;
	var encryptedVesselId = $('#hdnVesselId').val();
	if ($(window).width() > MobileScreenSize) {
		window.location.href = window.location.protocol + "//" + window.location.host + '/JSA/Details/?Source=1&JSADetails=' + jsaDetails + '&VesselId=' + encryptedVesselId;
	}
});

function SetMobileTab() {
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
	}
}

$(document).ready(function () {
	RegisterTabSelectionEvent('.mobileTabClick', ApprovalListPageKey);
	//collapse function
	if (($(window).width() > MobileScreenSize)) {
		$('.collapseleftarrow').on('click', function (e) {
			$('.approvalcollapse').toggleClass("expanded");
		});
	}

	//Common Functions
	AddLoadingIndicator();
	RemoveLoadingIndicator();
	SetMobileTab();
	AjaxError();

	//Sidebar back
	BackButton(ApprovalListPageKey, true)
	$(".spanfleetSelectionTitle").html("<em>Search Vessel / Fleet</em>");
	$('#fleetSelectionTitle').text('Viewing '+$('#hdnTitle').val())

	var headerNodeShortCode = $('#hdnHeaderNodeShortCode').val();
	var nodeShortCode = $('#hdnNodeShortCode').val();
	GetApprovalsList(headerNodeShortCode, nodeShortCode);
	SetHeaderMargin();
});

function GetApprovalsList(headerNodeShortCode, nodeShortCode) {

	var input = {
		FleetId: $('#hdnFleetId').val(),
		MenuType: $('#hdnMenuType').val(),
		VesselId: $('#hdnVesselId').val(),
		Title: $('#hdnTitle').val()
	}

	$.ajax({
		url: "/Approval/GetApprovalSummary",
		type: "POST",
		data: input,
		dataType: "JSON",
		beforeSend: function (xhr) {
			$('.approval-list').empty();
		},
		success: function (response) {
			if (!IsNullOrEmptyOrUndefinedLooseTyped(response) && !IsNullOrEmptyOrUndefinedLooseTyped(response.data)) {
				var result = response.data;
				for (var i = 0; i < result.length; i++) {
					var newRow = CreateHeaderNodeTemplate(result[i]);
					$('.approval-list').append(newRow);
				}

				if (!IsNullOrEmptyOrUndefinedLooseTyped(headerNodeShortCode) && !IsNullOrEmptyOrUndefinedLooseTyped(nodeShortCode)) {
					var approvalSummaryList = $('.approval-list');
					var headerNodeId = approvalSummaryList.find('#headerNode_' + headerNodeShortCode + nodeShortCode);
					headerNodeId.click();
				}
				else {
					var headerNode = $('.approvalheader')[0];
					if (headerNode != null && headerNode != undefined) {
						var node = $(headerNode).find('.approvalNode');
						node.click();
					}
				}

			}
		}
	});
}

function CreateHeaderNodeTemplate(item) {
	var element = '';
	element += '<div class="list approvalheader">\
					<div class="row no-gutters">\
						<div class="col-12 col-md-12 col-lg-12 col-xl-12">\
							<div class="approvalhead">\
								'+ item.headerNode + '\
							</div>';
	for (var i = 0; i < item.children.length; i++) {
		element += '<div id="headerNode_' + item.children[i].headerNodeShortCode + item.children[i].nodeShortCode + '" class="row cursor-pointer approvalNode" data-headerNode="' + item.children[i].headerNodeShortCode + '" data-node="' + item.children[i].nodeShortCode + '">\
								<span class="col-10 col-md-10 col-lg-10 col-xl-10"><div class="approvalsubhead">' + item.children[i].node + '</div></span>\
								<div class="col-2 col-md-2 col-lg-2 col-xl-2 text-left">\
									<a href="javascript:void(0);" class="approvalcount" id="node_'+ item.children[i].headerNodeShortCode + '_' + item.children[i].nodeShortCode + '">' + item.children[i].count + '</a>\
								</div>\
							</div>';
	}
	element += '</div>\
					</div>\
				</div>';
	return element;
}

function GetApprovalNode(HeaderNodeShortCode, NodeShortCode) {
	if (HeaderNodeShortCode == "PurchaseOrder" && NodeShortCode == "AwaitingSnrOrClientAuth") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			VesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetPurchseOrderPendingApproval(input);
	}
	else if (HeaderNodeShortCode == "PurchaseOrder" && NodeShortCode == "TenderAwaitingAuthorization") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			VesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetPurchseOrderTenderAwaitingAuthorization(input);
	}
	else if (HeaderNodeShortCode == "Defect" && NodeShortCode == "PendingClosure") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			EncryptedVesselId: $('#hdnVesselId').val() 
		}
		GetDefectPendingClosure(input);
	}
	else if (HeaderNodeShortCode == "PMS" && NodeShortCode == "PendingClosure") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			EncryptedVesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetPMSPendingClosure(input);
	}
	else if (HeaderNodeShortCode == "PMS" && NodeShortCode == "PendingRescheduleRequests") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			EncryptedVesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetPMSPendingRescheduleRequests(input);
	}
	else if (HeaderNodeShortCode == "InspectionsAndAudits" && NodeShortCode == "InspectionPendingClosure") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			EncryptedVesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetInspectionPendingClosure(input);
	}
	else if (HeaderNodeShortCode == "InspectionsAndAudits" && NodeShortCode == "AuditPendingClosure") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			EncryptedVesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetAuditPendingClosure(input);
	}
	else if (HeaderNodeShortCode == "JSA" && NodeShortCode == "PendingApproval") {
		var input = {
			FleetId: $('#hdnFleetId').val(),
			MenuType: $('#hdnMenuType').val(),
			EncryptedVesselId: $('#hdnVesselId').val(),
			NodeType: NodeShortCode
		}
		GetJSAPendingApproval(input);
	}
}

//To maintain page Summary in Session
function SetApprovalFilters(HeaderNodeShortCode, NodeShortCode) {
	$.ajax({
		url: "/Approval/SetApprovalFilters",
		type: "POST",
		dataType: "JSON",
		data: {
			"headerNode": HeaderNodeShortCode,
			"shortCode": NodeShortCode
		},
		success: function (data) {
			if (data != null) {
				$("#ActiveMobileTabClass").val(data.data.activeMobileTabClass);
			}
		}
	});
}

function GetPurchseOrderPendingApproval(input) {
	$('#dtPurchaseOrderPendingApproval').DataTable().destroy();
	var orderByColumn = $('#dtPurchaseOrderPendingApproval').find("th:contains('Vessel')")[0].cellIndex;

	dtPurchaseOrderPendingApproval = $('#dtPurchaseOrderPendingApproval').DataTable({
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 dtPurchaseOrderPendingApproval_customSearch search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false,
		"serverSide": true,
		"lengthChange": true,
		"searching": true,
		"info": true,
		search: {
			return: true,
		},
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		order: [[orderByColumn, 'asc']],
		"language": {
			"emptyTable": "No orders available.",
			//"infoFiltered": "(filtered from _MAX_ total entries)",
			//"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetApprovalPurchaseOrderList",
			"type": "POST",
			"data":
			{
				"input": input
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				orderable: false, 
				width: "70px",
				render: function (data, type, full, meta) {
					return " <a href = '/PurchaseOrder/Detail/?PurchaseOrderRequest=" + full.purchaseOrderUrl + "&VesselId=" + full.encryptedVesselId + "'> " + full.accountingCompanyId + " - " + full.orderNumber + "</a > ";
				}
			},
			{
				className: "data-text-align",
				"data": "vesselName",
				width: "130px",
				name: 'VesselName',
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "data-text-align d-none d-md-table-cell",
				width: "70px",
				name: 'OrderNumber',
				render: function (data, type, full, meta) {
					return GetCellData('Order No.', '<a href = "/PurchaseOrder/Detail/?PurchaseOrderRequest=' + full.purchaseOrderUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + full.accountingCompanyId + ' - ' + full.orderNumber + '</a > ');
				}
			},
			{
				className: "data-text-align",
				"data": "status",
				width: "45px",
				name: 'Status',
				render: function (data, type, full, meta) {
					return GetCellData('Status', data);
				}
			},
			{
				className: "tdblock data-text-align",
				"data": "title",
				width: "225px",
				name: 'Title',
				render: function (data, type, full, meta) {
					return GetCellData('Title', data);
				}
			},
			{
				className: "data-text-align",
				"data": "priorityDescription",
				width: "50px",
				name: 'PriorityDescription',
				render: function (data, type, full, meta) {
					return GetCellData("Priority", data);
				}
			},
			{
				className: "data-text-align",
				"data": "type",
				width: "70px",
				name: 'Type',
				render: function (data, type, full, meta) {
					return GetCellData('Type', data);
				}
			},
			{
				className: "data-datetime-align",
				"data": "dateOriginated",
				width: "75px",
				name: 'DateOriginated',
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Date Originated', data);
				}
			},
			{
				className: "data-text-align",
				"data": "authLevel",
				width: "35px",
				name: 'AuthLevel',
				render: function (data, type, full, meta) {
					var result = "";
					var output = "";
					if (full.authLevel == "1") {
						if (!IsNullOrEmptyOrUndefinedLooseTyped(full.authOfficeLimit)) {
							result = result + 'Office Limit of ' + full.authOfficeLimit + '(USD) was exceeded';
						}
						if (!IsNullOrEmptyOrUndefinedLooseTyped(result)) {
							result = result + '<br/>'
						}
						if (!IsNullOrEmptyOrUndefinedLooseTyped(full.authVesselLimit)) {
							result = result + 'Vessel Limit of ' + full.authVesselLimit + '(USD) was exceeded';
						}
					}
					if (full.authLevel == "2") {
						result = result + 'Level 1 Limit of ' + full.authLevel1Limit + '(USD) was exceeded';
					}
					if (!IsNullOrEmptyOrUndefinedLooseTyped(result)) {
						output = '<img src="/images/outline-i.png" class="ml-1 float-none float-md-right mt-md-1" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="' + result + '" title="">';
					}
					return GetCellData('Auth Level', data + output);
				}
			},
			{
				className: "data-text-align",
				"data": "hasClientAuthorised",
				width: "40px",
				name: 'HasClientAuthorised',
				render: function (data, type, full, meta) {
					var result = "";
					if (full.hasClientAuthorised) {
						result = '<img src="/images/outline-i.png" class="ml-1 float-none float-md-right mt-md-1" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="Client Limit of ' + full.authClientLimit + '(USD) was exceeded" title="">';
					}
					return GetCellData('Client Auth Req\'d', (data ? Yes : No) + result);
				}
			},
		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
			$(".dtPurchaseOrderPendingApproval_customSearch .dataTables_filter input").unbind();
			$(".dtPurchaseOrderPendingApproval_customSearch .dataTables_filter input").bind('keyup input', function (e) {
				var value = this.value.trim();
				if (e.keyCode == 13) {
					if (value.length > 1) {
						dtPurchaseOrderPendingApproval.search(value).draw();
					}
				}
				else if (value == "") {
					dtPurchaseOrderPendingApproval.search("").draw();
				}
			});
		}
	});

	$('#dtPurchaseOrderPendingApproval').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover',
			boundary:'window'
		})
	});
}

function GetPurchseOrderTenderAwaitingAuthorization(input) {
	$('#dtPoTenderAwaitingAuthorization').DataTable().destroy();
	var orderByColumn = $('#dtPoTenderAwaitingAuthorization').find("th:contains('Vessel')")[0].cellIndex;

	dtPoTenderAwaitingAuthorization = $('#dtPoTenderAwaitingAuthorization').DataTable({
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 dtPoTenderAwaitingAuthorization_customSearch search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false, 
		"serverSide": true,
		"lengthChange": true,
		"searching": true,
		"info": true,
		search: {
			return: true,
		},
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		order: [[orderByColumn, 'asc']],
		"language": {
			"emptyTable": "No orders available.",
			//"infoFiltered": "(filtered from _MAX_ total entries)",
			//"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetApprovalPurchaseOrderList",
			"type": "POST",
			"data": input,
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				orderable: false, 
				width: "70px",
				render: function (data, type, full, meta) {
					return " <a href = '/PurchaseOrder/Detail/?PurchaseOrderRequest=" + full.purchaseOrderUrl + "&VesselId=" + full.encryptedVesselId + "'> " + full.accountingCompanyId + " - " + full.orderNumber + "</a > ";
				}
			},
			{
				className: "data-text-align",
				"data": "vesselName",
				width: "130px",
				name: 'VesselName',
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "data-text-align d-none d-md-table-cell",
				width: "70px",
				name: 'OrderNumber',
				render: function (data, type, full, meta) {
					return GetCellData('Order No.', '<a href = "/PurchaseOrder/Detail/?PurchaseOrderRequest=' + full.purchaseOrderUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + full.accountingCompanyId + ' - ' + full.orderNumber + '</a > ');
				}
			},
			{
				className: "data-text-align",
				"data": "status",
				width: "45px",
				name: 'Status',
				render: function (data, type, full, meta) {
					return GetCellData('Status', data);
				}
			},
			{
				className: "tdblock data-text-align",
				"data": "title",
				width: "225px",
				name: 'Title',
				render: function (data, type, full, meta) {
					return GetCellData('Title', data);
				}
			},
			{
				className: "data-text-align",
				"data": "priorityDescription",
				width: "50px",
				name: 'PriorityDescription',
				render: function (data, type, full, meta) {
					return GetCellData("Priority", data);
				}
			},
			{
				className: "data-text-align",
				"data": "type",
				width: "70px",
				name: 'Type',
				render: function (data, type, full, meta) {
					return GetCellData('Type', data);
				}
			},
			{
				className: "data-datetime-align",
				"data": "dateOriginated",
				width: "75px",
				name: 'DateOriginated',
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Date Originated', data);
				}
			}
		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
			
			$(".dtPoTenderAwaitingAuthorization_customSearch .dataTables_filter input").unbind();
			$(".dtPoTenderAwaitingAuthorization_customSearch .dataTables_filter input").bind('keyup input', function (e) {
				var value = this.value.trim();
				if (e.keyCode == 13) {
					if (value.length > 1) {
						dtPoTenderAwaitingAuthorization.search(value).draw();
					}
				}
				else if (value == "") {
					dtPoTenderAwaitingAuthorization.search("").draw();
				}
			});
		}
	});

	$('#dtPoTenderAwaitingAuthorization').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});
}

function GetDefectPendingClosure(data) {
	$('#dtDefectsPendingClosure').DataTable().destroy();
	dtDefectsPendingClosure = $('#dtDefectsPendingClosure').DataTable({
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
			"emptyTable": "No defects available.",
			"infoFiltered": "(filtered from _MAX_ total entries)",
			"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetDefectPendingClosures",
			"type": "POST",
			"data": data,
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				data: "defectNo",
				width: "70px",
				render: function (data, type, full, meta) {
					return "<a href = '/Defect/Details/?DefectDetails=" + full.defectDetailURL + "&VesselId=" + full.encryptedVesselId + "'> " + full.defectNo + "</a > ";
				}
			},
			{
				className: "data-text-align",
				"data": "vesselName",
				width: "130px",
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "d-none d-sm-table-cell data-text-align",
				data: "defectNo",
				width: "72px",
				render: function (data, type, full, meta) {
					return GetCellData('Defect No.', "<a href = '/Defect/Details/?DefectDetails=" + full.defectDetailURL + "&VesselId=" + full.encryptedVesselId + "'> " + full.defectNo + "</a > ");
				}
			},
			{
				className: "data-text-align",
				data: "category",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetCellData('Category', data);
				}
			},
			{
				className: "width-85 data-text-align",
				data: "title",
				width: "200px",
				render: function (data, type, full, meta) {
					return GetCellData('Title', data);
				}
			},
			{
				className: "data-text-align",
				data: "status",
				width: "50px",
				render: function (data, type, full, meta) {
					var ShowStatus = "";
					ShowStatus += "<span class='d-sm-block d-md-none' data-toggle='tooltip' title='" + full.statusDescription + "' > " + data + "</span>";
					ShowStatus += "<span class='d-none d-sm-none d-md-block' data-placement='bottom' data-toggle='tooltip' title='" + full.statusDescription + "'>" + data + "</span>";
					return GetCellData('Status', ShowStatus);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "systemArea",
				width: "180px",
				render: function (data, type, full, meta) {
					var ShowElement = data;
					if (full.subSystemArea != null && full.subSystemArea != '' && full.subSystemArea != 'undefined') {
						ShowElement += "<span class='d-none d-sm-none d-md-block' data-placement='bottom' data-toggle='tooltip' title='" + full.subSystemArea + "'>" + full.subSystemArea + "</span>";
					}
					return GetCellData('System Area', ShowElement);
				}
			},
			{
				className: "data-datetime-align",
				data: "estimatedCompleteDate",
				width: "85px",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Due Date', data);
				}
			}
		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
		}
	});

	$('#dtDefectsPendingClosure').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});

}

function GetPMSPendingClosure(data) {
	$('#dtPMSPendingClosure').DataTable().destroy();
	dtPMSPendingClosure = $('#dtPMSPendingClosure').DataTable({
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
			"emptyTable": "No pms pending closure available.",
			"infoFiltered": "(filtered from _MAX_ total entries)",
			"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetPMSPendingClosures",
			"type": "POST",
			"data": data,
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				data: "componentName",
				width: "200px",
				render: function (data, type, full, meta) {

					let wolink = '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + full.componentName + '</a > '

					let defectWoLink = '<a href = "/Defect/Details/?DefectDetails=' + full.defectDetailsUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + full.componentName + '</a > '

					if (full.isDefectWorkOrder == true) {
						return defectWoLink;
					} else {
						return wolink;
					}

				}
			},
			{
				className: "data-text-align",
				"data": "vesselName",
				width: "90px",
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "data-datetime-align",
				data: "dueDate",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Due Date', data);
				}
			},
			{
				className: "d-none d-sm-table-cell data-text-align",
				data: "componentName",
				width: "200px",
				render: function (data, type, full, meta) {

					let wolink = '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + full.componentName + '</a > '

					let defectWoLink = '<a href = "/Defect/Details/?DefectDetails=' + full.defectDetailsUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + full.componentName + '</a > '

					if (full.isDefectWorkOrder == true) {
						return GetCellData('Component Name', defectWoLink);
					} else {
						return GetCellData('Component Name', wolink);
					}

				}
			},
			{
				className: "width-85 data-text-align",
				data: "jobName",
				width: "200px",
				render: function (data, type, full, meta) {
					return GetCellData('Job Name', data);
				}
			},
			{
				className: "data-text-align",
				data: "statusCode",
				width: "50px",
				render: function (data, type, full, meta) {
					return GetCellData('Status', data);
				}
			},
			{
				className: "data-text-align",
				data: "jobTypeShortCode",
				width: "50px",
				render: function (data, type, full, meta) {
					return GetCellData('Job Type', data);
				}
			},
			{
				className: "data-text-align",
				data: "responsibleRankShortCode",
				width: "80px",
				render: function (data, type, full, meta) {
					return GetCellData('Responsibility', data);
				}
			},
			{
				className: "data-text-align",
				data: "interval",
				width: "60px",
				render: function (data, type, full, meta) {
					return GetCellData('Interval', data);
				}
			}
		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
		}
	});

	$('#dtPMSPendingClosure').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});

}

function GetPMSPendingRescheduleRequests(data) {
	$('#dtPMSPendingRescheduleRequests').DataTable().destroy();
	dtPMSPendingRescheduleRequests = $('#dtPMSPendingRescheduleRequests').DataTable({
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
			"emptyTable": "No reschedule requests available.",
			"infoFiltered": "(filtered from _MAX_ total entries)",
			"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetPMSRescheduleRequests",
			"type": "POST",
			"data": data,
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				data: "componentName",
				width: "200px",
				render: function (data, type, full, meta) {
					return '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + full.requestNumber + '</a > ';
				}
			},
			{
				className: "tdblock data-text-align",
				"data": "vesselName",
				width: "130px",
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "data-text-align",
				data: "rescheduleType",
				width: "90px",
				render: function (data, type, full, meta) {
					let wolink = '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + data + '</a > '
					return GetCellData('Request Type', wolink);
				}
			},
			{
				className: "data-text-align",
				data: "jobType",
				width: "35px",
				render: function (data, type, full, meta) {
					return GetCellData('Job Type', data);
				}
			},
			{
				className: "d-none d-sm-table-cell data-text-align",
				data: "requestNumber",
				width: "60px",
				render: function (data, type, full, meta) {
					let wolink = '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + data + '</a > '
					return GetCellData('Request No', wolink);
				}
			},
			{
				className: "data-datetime-align position-relative",
				data: "previousDueDate",
				type: "date",
				width: "70px",
				render: function (data, type, full, meta) {
					var date = FormatDate(data);
					if (type === "display") {
						var ShowStatus = date;
						if (full.isOverDueVisible) {
							ShowStatus += "<img src='/images/overdue.png' class='ml-1 d-sm-block d-md-none' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Overdue job.'>";
							ShowStatus += "<i class='fa fa-caret-right over-due icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='OverDue Job.'></i>";
						}
						else if (full.isOverduePeriodVisible) {
							ShowStatus += "<img src='/images/overdue.png' class='ml-1 d-sm-block d-md-none' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Overdue job prior from current month.'>";
							ShowStatus += "<i class='fa fa-caret-right over-due-period icon d-none d-sm-none d-md-block' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Overdue job prior from current month.'></i>";
						}
						return GetCellData('Due Date', ShowStatus);
					}
					return date;
				}
			},
			{
				className: "data-datetime-align",
				data: "currentDueDate",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Next Due Date', data);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "componentName",
				width: "150px",
				render: function (data, type, full, meta) {
					return GetCellData('Component Name', data);
				}
			},
			{
				className: "width-85 data-text-align",
				data: "jobName",
				width: "170px",
				render: function (data, type, full, meta) {
					return GetCellData('Job Name', data);
				}
			},
			{
				className: "data-text-align",
				data: "statusShortCode",
				width: "45px",
				render: function (data, type, full, meta) {
					return GetCellData('Status', data);
				}
			},
			{
				className: "data-text-align",
				data: "originalInterval" ,
				width: "55px",
				render: function (data, type, full, meta) {
					return GetCellData('Original Interval', data);
				}
			},
			{
				className: "data-text-align",
				data: "requestedInterval",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetCellData('Requested Interval', data);
				}
			},
			{
				className: "data-text-align",
				data: "responsibleRank",
				width: "110px",
				render: function (data, type, full, meta) {
					return GetCellData('Resp', data);
				}
			},
			{
				className: "data-text-align",
				data: "rescheduleStatusName",
				width: "50px",
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
				data: "comment",
				width: "75px",
				render: function (data, type, full, meta) {
					var result = '<img src="/images/outline-i.png" class="ml-1 float-none mt-md-1" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="Requestor\'s Comments: ' + full.comment + '" title="">';
					return GetCellData('Comments', result);

				}
			}
		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
		}
	});

	$('#dtPMSPendingRescheduleRequests').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover',
			boundary: 'window'
		})
	});

}

function onDataTableComplete() {
	$.fn.DataTable.ext.pager.numbers_length = 4;
	var newWidth = ($(".table-scroll-width").width());
	$(".table-common-design .table-horizontal-scroll").css({
		"maxWidth": newWidth - 20
	});
}

function GetInspectionPendingClosure(data) {
	$('#dtInspectionPendingClosure').DataTable().destroy();
	dtInspectionPendingClosure = $('#dtInspectionPendingClosure').DataTable({
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
			"emptyTable": "No inspections available.",
			"infoFiltered": "(filtered from _MAX_ total entries)",
			"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetInspectionAuditPendingClosure",
			"type": "POST",
			"data": data,
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				name: "InspectionTypeName",
				data: "inspectionTypeName",
				width: "175px",
				render: function (data, type, full, meta) {
					if ($('#InspectionType').val() == 'InspectionDueType' || $('#InspectionType').val() == 'InspectionOverdueType') {
						return GetCellData(data);
					}
					else {
						return '<a href="/Inspection/Findings/?finding=' + full.findingURL + '&vesselId=' + full.vesselId + '">' + data + '</a>';
					}
				}
			},
			{
				className: "data-text-align",
				"data": "vesselName",
				width: "130px",
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "d-none d-sm-table-cell td-row-header font-weight-600 data-text-align rowheadermargin",
				name: "InspectionTypeName",
				data: "inspectionTypeName",
				width: "175px",
				render: function (data, type, full, meta) {
					if ($('#InspectionType').val() == 'InspectionDueType' || $('#InspectionType').val() == 'InspectionOverdueType') {
						return GetCellData(data);
					}
					else {
						return '<a href="/Inspection/Findings/?finding=' + full.findingURL + '&vesselId=' + full.vesselId + '">' + data + '</a>';
					}
				}
			},
			{
				className: "data-text-align",
				name: "Status",
				data: "status",
				width: "100px",
				render: function (data, type, full, meta) {
					var color = '';
					if (data == "Overdue Findings") {
						color = "txt-red";
					}
					else if (data == "Open Findings") {
						color = "text-yellow";
					}
					else if (data == "Pending Closure") {
						color = "txt-blue";
					}
					else if (data == "Closed") {
						color = "txt-color-green";
					}

					return GetCellData('Status', '<span class="' + color + '">' + data + '</span>');
				}
			},
			{
				className: "data-datetime-align",
				name: "InspectionDate",
				data: "inspectionDate",
				width: "64px",
				type: "date",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Occured Date', data);
				}
			},
			{
				className: "data-datetime-align",
				name: "NextDueDate",
				data: "nextDueDate",
				width: "64px",
				type: "date",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Next Due', data);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "location",
				name: "Location",
				width: "100px",
				render: function (data, type, full, meta) {
					if (data != null) {
						return GetCellData('Location', data);
					}
					else {
						return GetCellData('Location', '<span class="d-sm-none">-</span>');
					}
				}
			},
			{
				className: "tdblock data-text-align",
				name: "CompanyName",
				data: "companyName",
				width: "130px",
				render: function (data, type, full, meta) { return GetCellData('Company', data); }
			},
			{
				className: "tdblock td-row-header d-inline-block d-sm-none finding-mobile-text",
				"defaultContent": 'FINDINGS',
				orderable: false
			},
			{
				className: "data-number-align td-w-auto border-xs-0 text-dark-grey-span mobile-bottom-0-label",
				name: "TotalFindingCount",
				data: "totalFindingCount",
				width: "51px",

				render: function (data, type, full, meta) {
					var color = '';
					if (data == 0) {
						color = "txt-color-green font-weight-600";
					}

					return GetCellData('Total', '<span class="' + color + '">' + data + '</span>');
				}
			},
			{
				className: "data-number-align td-w-auto border-xs-0 mobile-bottom-0-label",
				name: "OutStandingFindingCount",
				data: "outStandingFindingCount",
				width: "30px",
				render: function (data, type, full, meta) {
					var color = '';
					if (data > 0) {
						color = "text-yellow font-weight-600";
					}
					else {
						color = "txt-color-gray";
					}

					return GetCellData('O/S', '<span class="' + color + '">' + data + '</span>');
				}
			},
			{
				className: "data-number-align td-w-auto border-xs-0 mobile-bottom-0-label",
				name: "OverdueFindingCount",
				data: "overdueFindingCount",
				width: "30px",
				render: function (data, type, full, meta) {
					var color = '';
					if (data > 0) {
						color = "txt-red font-weight-600";
					}
					else {
						color = "txt-color-gray";
					}
					return GetCellData('O/D', '<span class="' + color + '">' + data + '</span>');
				}
			}
		],
		"initComplete" : function (settings, data) {
			onDataTableComplete();
		},
	});

	$('#dtInspectionPendingClosure').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});

}

function GetAuditPendingClosure(data) {

	$('#dtAuditPendingClosure').DataTable().destroy();
	dtAuditPendingClosure = $('#dtAuditPendingClosure').DataTable({
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
			"emptyTable": "No Audits available.",
			"infoFiltered": "(filtered from _MAX_ total entries)",
			"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetInspectionAuditPendingClosure",
			"type": "POST",
			"data": data,
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-md-none",
				name: "InspectionTypeName",
				data: "inspectionTypeName",
				width: "175px",
				render: function (data, type, full, meta) {
					if ($('#InspectionType').val() == 'InspectionDueType' || $('#InspectionType').val() == 'InspectionOverdueType') {
						return GetCellData(data);
					}
					else {
						return '<a href="/Inspection/Findings/?finding=' + full.findingURL + '&vesselId=' + full.vesselId + '">' + data + '</a>';
					}
				}
			},
			{
				className: "data-text-align",
				"data": "vesselName",
				width: "130px",
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "d-none d-sm-table-cell td-row-header font-weight-600 data-text-align rowheadermargin",
				name: "InspectionTypeName",
				data: "inspectionTypeName",
				width: "175px",
				render: function (data, type, full, meta) {
					if ($('#InspectionType').val() == 'InspectionDueType' || $('#InspectionType').val() == 'InspectionOverdueType') {
						return GetCellData(data);
					}
					else {
						return '<a href="/Inspection/Findings/?finding=' + full.findingURL + '&vesselId=' + full.vesselId + '">' + data + '</a>';
					}
				}
			},
			{
				className: "data-text-align",
				name: "Status",
				data: "status",
				width: "100px",
				render: function (data, type, full, meta) {
					var color = '';
					if (data == "Overdue Findings") {
						color = "txt-red";
					}
					else if (data == "Open Findings") {
						color = "text-yellow";
					}
					else if (data == "Pending Closure") {
						color = "txt-blue";
					}
					else if (data == "Closed") {
						color = "txt-color-green";
					}

					return GetCellData('Status', '<span class="' + color + '">' + data + '</span>');
				}
			},
			{
				className: "data-datetime-align",
				name: "InspectionDate",
				data: "inspectionDate",
				width: "64px",
				type: "date",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Occured Date', data);
				}
			},
			{
				className: "data-datetime-align",
				name: "NextDueDate",
				data: "nextDueDate",
				width: "64px",
				type: "date",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Next Due', data);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "location",
				name: "Location",
				width: "100px",
				render: function (data, type, full, meta) {
					if (data != null) {
						return GetCellData('Location', data);
					}
					else {
						return GetCellData('Location', '<span class="d-sm-none">-</span>');
					}
				}
			},
			{
				className: "tdblock data-text-align",
				name: "CompanyName",
				data: "companyName",
				width: "130px",
				render: function (data, type, full, meta) { return GetCellData('Company', data); }
			},
			{
				className: "tdblock td-row-header d-inline-block d-sm-none finding-mobile-text",
				"defaultContent": 'FINDINGS',
				orderable: false
			},
			{
				className: "data-number-align td-w-auto border-xs-0 text-dark-grey-span mobile-bottom-0-label",
				name: "TotalFindingCount",
				data: "totalFindingCount",
				width: "51px",

				render: function (data, type, full, meta) {
					var color = '';
					if (data == 0) {
						color = "txt-color-green font-weight-600";
					}

					return GetCellData('Total', '<span class="' + color + '">' + data + '</span>');
				}
			},
			{
				className: "data-number-align td-w-auto border-xs-0 mobile-bottom-0-label",
				name: "OutStandingFindingCount",
				data: "outStandingFindingCount",
				width: "30px",
				render: function (data, type, full, meta) {
					var color = '';
					if (data > 0) {
						color = "text-yellow font-weight-600";
					}
					else {
						color = "txt-color-gray";
					}

					return GetCellData('O/S', '<span class="' + color + '">' + data + '</span>');
				}
			},
			{
				className: "data-number-align td-w-auto border-xs-0 mobile-bottom-0-label",
				name: "OverdueFindingCount",
				data: "overdueFindingCount",
				width: "30px",
				render: function (data, type, full, meta) {
					var color = '';
					if (data > 0) {
						color = "txt-red font-weight-600";
					}
					else {
						color = "txt-color-gray";
					}
					return GetCellData('O/D', '<span class="' + color + '">' + data + '</span>');
				}
			}
		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
		},
	});

	$('#dtAuditPendingClosure').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});

}

function GetJSAPendingApproval(input) {

	$('#dtJSAPendingApproval').DataTable().destroy();
	var orderByColumn = $('#dtJSAPendingApproval').find("th:contains('Vessel')")[0].cellIndex;
	dtJSAPendingApproval = $('#dtJSAPendingApproval').DataTable({
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 dtJSAPendingApproval_customSearch search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false,
		"serverSide": true,
		"lengthChange": true,
		"searching": true,
		"info": true,
		search: {
			return: true,
		},
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		order: [[orderByColumn, 'asc']],
		"language": {
			"emptyTable": "No Approvals available.",
			//"infoFiltered": "(filtered from _MAX_ total entries)",
			//"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Approval/GetJSAPendingApprovalsList",
			"type": "POST",
			"data":
			{
				"input": input
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "tdblock data-text-align d-table-cell d-sm-none",
				data: "title",
				width: "200px",
				name: 'Title',
				render: function (data, type, full, meta) {
					let anchor = '<span class="d-inline-block d-sm-none"> ' + full.refNo + '</span> <a href="/JSA/Details/?Source=1&JSADetails=' + full.jsaDetails + '&VesselId=' + $("#hdnVesselId").val() + '">' + data + '</a>';
					return anchor;
				}
			},
			{
				className: "tdblock data-text-align",
				"data": "vesselName",
				width: "130px",
				name: 'VesselName',
				render: function (data, type, full, meta) {
					return GetCellData('Vessel', data);
				}
			},
			{
				className: "d-none d-sm-table-cell data-text-align",
				data: "refNo",
				width: "72px",
				name: 'RefNo',
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetExportData(data);
					}
					return data;
				}
			},
			{
				className: "width-85 d-none d-sm-table-cell td-row-header font-weight-600 data-text-align rowheadermargin",
				data: "title",
				width: "200px",
				name: 'Title',
				render: function (data, type, full, meta) {
					let anchor = '<span class="d-inline-block d-sm-none"> ' + full.refNo + '</span> <a href="/JSA/Details/?Source=1&JSADetails=' + full.jsaDetails + '&VesselId=' + $("#hdnVesselId").val() + '">' + GetExportData(data) + '</a>';
					return anchor;
				}
			},
			{
				className: "data-datetime-align",
				data: "startDate",
				width: "75px",
				name: 'StartDate',
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Start Date', data);
				}
			},
			{
				className: "data-datetime-align",
				data: "endDate",
				width: "70px",
				name: 'EndDate',
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'End Date', data);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "status",
				width: "140px",
				name: 'Status',
				render: function (data, type, full, meta) {
					let finalData = '';
					if (statusColorMap.has(full.statusKPI)) {
						finalData = '<span class=' + statusColorMap.get(full.statusKPI).textColor + '>' + data + '<span/>';
					} else {
						finalData = data;
					}
					return GetCellData('Status', finalData);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "maxRisk",
				width: "95px",
				name: 'MaxRiskFactor',
				render: function (data, type, full, meta) {
					let finalData = '';
					if (riskColorMap.has(full.riskKPI)) {
						finalData = '<span class=' + riskColorMap.get(full.riskKPI).textColor + '>' + data + '<span/>';
					} else {
						finalData = data;
					}
					return GetCellData('Max. Risk', finalData);
				}
			},
			{
				className: "tdblock data-text-align",
				data: "systemArea",
				width: "250px",
				name: 'SystemArea',
				render: function (data, type, full, meta) { return GetCellData('System Area', data); }
			}

		],
		"initComplete": function (settings, data) {
			onDataTableComplete();
			$(".dtJSAPendingApproval_customSearch .dataTables_filter input").unbind();
			$(".dtJSAPendingApproval_customSearch .dataTables_filter input").bind('keyup input', function (e) {
				var value = this.value.trim();
				if (e.keyCode == 13) {
					if (value.length > 1) {
						dtJSAPendingApproval.search(value).draw();
					}
				}
				else if (value == "") {
					dtJSAPendingApproval.search("").draw();
				}
			});
		}
	});

	$('#dtJSAPendingApproval').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});
}