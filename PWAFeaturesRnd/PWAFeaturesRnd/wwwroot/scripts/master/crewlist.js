import "bootstrap";
import "select2/dist/js/select2.full.js";
import { createTree } from "jquery.fancytree";
import moment from "moment";
import * as JSZip from "jszip";
window.JSZip = JSZip;
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, GetCookie, ToastrAlert, MobileTab_Overview, MobileTab_List, Mobile_Tabs, BackButton, IsModuleAccessible, BlockLinkAccess, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, GetValueOrDefaultDT, IsNullOrEmptyOrUndefinedLooseTyped, AddClassIfAbsent, RemoveClassIfPresent, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js"
import { MobileScreenSize, CrewListPageKey, Tab1, Tab2, PMSModuleKey } from "../common/constants.js"
import { GetCellData, CustomizedExcelHeader, GetExportData, GetExportCellData, GetExportFormattedDate, GetExportDataForHiddenElement } from "../common/datatablefunctions.js"
var startDate, endDate;
var dtCrewList;
var crewFilterCount = 0;

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

$(document).on('click', '#aRemoveFilter', function () {
	clearSearchFilter();
});

$(document).ready(function () {
	AddLoadingIndicator();
	RemoveLoadingIndicator();

	InitializeListDiscussionAndNoteClickEvents(code);

	//Sidebar back
	BackButton(CrewListPageKey, true)

	$('#mobileactiontoggle').click(function () {
		$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
	});
	//$(".dropdown-toggle").dropdown();
	RegisterTabSelectionEvent('.mobileTabClick', CrewListPageKey);

	$(document).click(function () {
		if ($("#mobileActiondropdown").hasClass('show')) {
			$("#mobileActiondropdown").removeClass('show');
		}
	});

	$('input[type="checkbox"]').click(function () {
		var inputValue = $(this).attr("value");
		$("." + inputValue).toggle();
	});

	var start = moment($('#FromDate').val(), 'DD-MM-YYYY');
	var end = moment($('#ToDate').val(), 'DD-MM-YYYY');
	
	$("#dtrcrewlist").caleran(
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

	$('.btnMedicalSignOff').click(function () {
		CallMedicalSignOff();
	});

	$('#btnSearch').click(function () {
		search();
	});

	$('#btnClear').click(function () {
		clearSearchFilter();
	});

	$('.btnExportCrewList').click(() => {
		var searchValue = dtCrewList.search();
		dtCrewList.search("").draw();

		let chatColumn = 0;
		let noteColumn = 1;
		let mobileChatNoteColumn = 2;
		
		dtCrewList.column(mobileChatNoteColumn).visible(false);
		dtCrewList.column(chatColumn).visible(false);
		dtCrewList.column(noteColumn).visible(false);
		

		$('#dtcrewlistonboard.cardview thead').addClass("export-grid-show");
		$('#dtcrewlistonboard').DataTable().buttons(0, 2).trigger();
		$('#dtcrewlistonboard.cardview thead').removeClass("export-grid-show");

		dtCrewList.column(mobileChatNoteColumn).visible(true);
		dtCrewList.column(chatColumn).visible(true);
		dtCrewList.column(noteColumn).visible(true);

		

		dtCrewList.search(searchValue).draw();
	});

	//drop down change event for crew status
	$('input[type=radio][name=selCrewStatus]').change(function () {
		var CrewStatusval = $('input[name="selCrewStatus"]:checked').val();
		$('#SelectedStageFilter').val(CrewStatusval);
		if (CrewStatusval == 'Onboard') {
			$('#dtrcrewlist').prop('disabled', false);
		}
		else {
			var currentDate = moment();
			var start = moment(currentDate, 'DD-MM-YYYY');
			var end = moment(currentDate, 'DD-MM-YYYY');
			setDateDetails(start, end, false);
			$('#dtrcrewlist').prop('disabled', true);
		}
	});
	SetMobileTabWindow();
	BindCrewSummary();
	LoadDepartmentTree();
	LoadRankCategoryTree();
	MaintainFilters();
	AjaxError();
});


$('#rankCategoryTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#rankCategoryTree"), "#rankFilterCount");
});

$('#departmentTree').click(function () {
	FilterCountSet(GetTreeNodeLength("#departmentTree"), "#departmentFilterCount");
});

function setDateDetails(start, end, isSearch) {
	//if (start.format("DD MMM YYYY") != 'Invalid date' && end.format("DD MMM YYYY") != 'Invalid date') {
		startDate = start.format("DD MMM YYYY");
		endDate = end.format("DD MMM YYYY");
		$("#dtrcrewlist").html(startDate + " - " + endDate);
		$("#tblspndtrcrewlist").html(startDate + " - " + endDate);
	//}
	if (isSearch) {
		search();
	}

}

function LoadCrewListDataTable() {
	var input = {
		"fromDate": startDate,
		"toDate": endDate,
		"EncryptedVesselId": $('#EncryptedVesselId').val(),
		"SelectedStageFilter": $('#SelectedStageFilter').val(),
		"SelectedRankIds": $('#SelectedRankIds').val(),
		"SelectedRankDescriptions": $('#SelectedRankDescriptions').val(),
		"SelectedDepartmentIds": $('#SelectedDepartmentIds').val(),
		"SelectedDepartmentDescriptions": $('#SelectedDepartmentDescriptions').val(),
		"CrewChangeDate": $('#CrewChangeDate').val(),
		"isSearchClicked": $('#IsSearchClicked').val()
	}
	$('#dtcrewlistonboard').DataTable().destroy();
	dtCrewList = $('#dtcrewlistonboard').DataTable({
		//"dom": '<<"row mb-3"<"col-12 col-md-4 offset-md-0 col-lg-3 offset-lg-5 col-xl-3 offset-xl-3 dt-infomation"i><"col-md-3 col-lg-2 col-xl-3 filters-data"><"col-12 col-md-5 col-lg-3 col-xl-3"f>><rt><"clearfix"<"float-left"l><""p>>>',
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
		"pageLength": 50,
		"aaSorting": [],
		"language": {
			"emptyTable": "No crew records available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Crew/GetCrewList",
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
				title: "Crew List",
				customize: function (xlsx) {
					CustomizedExcelHeader(xlsx, 7);
				},
				messageTop: function () {

					let filterText = '';
					let status = $('#SelectedStageName').val();
					let ranks = $('#SelectedRankDescriptions').val();
					let departments = $('#SelectedDepartmentDescriptions').val();

					if (!IsNullOrEmptyOrUndefined(status)) {
						filterText += '\nStatus :' + status
					}
					if (!IsNullOrEmptyOrUndefined(ranks)) {
						if (ranks.includes("All", 0)) {
							ranks = "All";
						}
						filterText += '\nRank :' + ranks
					}
					if (!IsNullOrEmptyOrUndefined(departments)) {
						if (departments.includes("All", 0)) {
							departments = "All";
						}
						filterText += '\nDepartment :' + departments
					}
					

					return 'Vessel : ' + $('#VesselName').val()
						+ '\nFrom Date : ' + startDate
						+ '\nTo Date : ' + endDate
						+ filterText
						+ '\n';
				}
			},
			'pdf', 'print'
		],
		//"createdRow": function (row, data, dataIndex) {
		//	$('td:eq(1)', row).css('min-width', '150px');
		//	$('td:eq(1)', row).css('max-width', '300px');

		//	$('td:eq(2)', row).css('min-width', '90px');
		//	$('td:eq(2)', row).css('max-width', '120px');

		//	$('td:eq(6)', row).css('min-width', '90px');
		//	$('td:eq(6)', row).css('max-width', '100px');

		//	$('td:eq(7)', row).css('min-width', '90px');
		//	$('td:eq(7)', row).css('max-width', '100px');

		//	$('td:eq(11)', row).css('min-width', '150px');
		//	$('td:eq(11)', row).css('max-width', '300px');

		//},
		"columns": [
			{
				className: "data-icon-align d-none d-md-table-cell",
				orderable: false,
				width: "10px",
				render: function (data, type, full, meta) {
					if (full.channelCount > 0) {
						return GetChatBaseIcons(full.crewId, full.channelCount, full.messageDetailsJSON);
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
						return GetNotesBaseIcons(full.crewId, full.notesCount, full.messageDetailsJSON);
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
						return GetChatNotesBaseIcons(full.crewId, full.channelCount, full.notesCount, full.messageDetailsJSON);
					} else {
						return '';
					}
				}
			},
			{
				className: "top-margin-0 width-auto mr-2 data-number-align",
				"data": "serialNumber",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetExportData(data + '<span class="d-sm-none">.</span>');
					}
					return data;
				}
			},
			{
				className: "top-margin-0 width-auto mr-2 data-icon-align",
				"data": "berthTypeShortCode",
				width: "75px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						if (($(window).width() < MobileScreenSize)) {
							return '<div class="badge badge-pill status-badge" data-toggle="tooltip" title="" data-original-title="' + full.berthTypeDescription + '" style="border: 2px solid ' + full.berthColorCode + ';color: ' + full.berthColorCode + ';padding: 4px 0px !important;"><span class="export-Data">' + data + '</span></div>';
						}
						else {
							return '<div class="badge badge-pill status-badge" data-toggle="tooltip" title="" data-original-title="' + full.berthTypeDescription + '" style="border: 2px solid ' + full.berthColorCode + ';color: ' + full.berthColorCode + ';padding: 3px 0px !important;"><span class="export-Data">' + data + '</span></div>';
						}
					}
					return data;
				}
			},
			{
				className: "data-text-align width-78",
				"data": "crewFullName",
				width: "270px",
				render: function (data, type, full, meta) {
					if (full.isCrewNameVisible) {

						var uiElement = '';
						if (full.isCrewSignedOff) {
							uiElement = '<div> <a class="text-uppercase blockCrewDetail" href = "/Crew/Details/?Source=1&CrewId=' + full.encryptedCrewId + '&VesselId=' + $("#EncryptedVesselId").val() + '"> ' + GetExportData(full.crewFullName) + '</a > <img src="/images/SignOff.png" width="15" data-html="true" data-toggle="tooltip" data-placement="bottom" title="Signed Off"></div>'
						}
						else {
							uiElement = '<div> <a class="text-uppercase blockCrewDetail" href = "/Crew/Details/?Source=1&CrewId=' + full.encryptedCrewId + '&VesselId=' + $("#EncryptedVesselId").val() + '"> ' + GetExportData(full.crewFullName) + '</a > </div>';
						}
						return GetExportData(uiElement);
					}
					else {
						return GetExportData(GetValueOrDefaultDT(''));
					}
				}
			},
			{
				className: "data-text-align",
				"data": "rankDescription",
				width: "150px",
				render: function (data, type, full, meta) {
					let elipsedData = data;
					if (data.length > 20 && !($(window).width() < MobileScreenSize)) {
						elipsedData = data.substring(0, 14) + '...';
					}
					return GetExportCellData('Rank', GetValueOrDefaultDT('<span data-toggle="tooltip" data-placement="bottom" title="' + data + '">' + elipsedData + '</span>'));
				}
			},
			{
				className: "data-text-align",
				"data": "departmentShortName",
				width: "50px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Dept.',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-text-align",
				"data": "nationality",
				width: "30px",
				render: function (data, type, full, meta) {
					return GetExportCellData('NAT',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-text-align",
				"data": "lengthOfContract",
				width: "50px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Contract',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-datetime-align",
				"data": "signOn",
				width: "160px",
				render: function (data, type, full, meta) {
					if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
						return GetExportCellData('Sign On',GetValueOrDefaultDT(data))
                    }
					return GetExportFormattedDate(type, 'Sign On', data);
				}
			},
			{
				className: "data-datetime-align",
				"data": "dueRelief",
				width: "140px",
				render: function (data, type, full, meta) {
					if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
						return GetExportCellData('Due Relief', GetValueOrDefaultDT(data))
					}
					return GetExportFormattedDate(type, 'Due Relief', data);
				}
			},
			{
				className: "data-text-align",
				"data": "extension",
				width: "50px",
				render: function (data, type, full, meta) {
					return GetExportCellData('EXT', GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-number-align",
				"data": "left",
				width: "30px",
				render: function (data, type, full, meta) {
					if (data != null && data != '' && data != 'undefined') {
						var KPIElement = '';
						if (data < 0) {
							KPIElement += "<span class='txt-red'>" + data + "</span>";
							return GetExportCellData('Left', KPIElement);
						}
						else {
							return GetExportCellData('Left', data);
						}

					}
					else {
						return GetExportCellData('Left', 0);
					}

				}
			},
			{
				className: "data-icon-align",
				width: "80px",
				render: function (data, type, full, meta) {
					var status = '';
					var color = '';

					if (full.relieverId != '' && full.relieverId != null && full.relieverId != 'undefined') {
						color = full.planningStatusColour;
						status = '';
						status += GetExportDataForHiddenElement(full.planningStatusShortCode);
						status += '<span style="color:' + color + '">' + full.planningStatusShortCode + '</span>';
					}

					if (full.planningStatusId == 'OV') {
						color = "#FF544A";
						status = '';
						status += GetExportDataForHiddenElement('Overlap');
						status += '<span style="color:' + color + '"> Overlap </span>';
					}

					return GetCellData('Status', GetValueOrDefaultDT(status));
				}
			},
			{
				className: "tdblock data-text-align",
				"data": "relieverName",
				width: "270px",
				render: function (data, type, full, meta) {
					if (full.isRelieverNameVisible) {
						return GetCellData('Reliever', '<a class="text-uppercase" href = "/Crew/Details/?Source=1&CrewId=' + full.encryptedReliverId + '"> ' + GetExportData(full.relieverName) + '</a > ');
					}
					else {
						return GetExportCellData('Reliever', GetValueOrDefaultDT(''));
					}
				}
			},
		],
		"fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
			let child = $(nRow).find('td:eq(2)').children();
			if (screen.width < MobileScreenSize && $(child).length == 0) {
				$(nRow).find('td:eq(2)').addClass('d-none');
			}
		},
		"initComplete": function (settings, data) {
			AppendGridTitle();
		}
	});
	$.fn.DataTable.ext.pager.numbers_length = 4;
	//$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata"  class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');

	$('#dtcrewlistonboard').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});
}

function search() {
	GetSelectedRank();
	GetSelectedDeartment();

	var input = {
		"fromDate": startDate,
		"toDate": endDate,
		"EncryptedVesselId": $('#EncryptedVesselId').val(),
		"SelectedStageFilter": $('#SelectedStageFilter').val(),
		"SelectedRankIds": $('#SelectedRankIds').val(),
		"SelectedRankDescriptions": $('#SelectedRankDescriptions').val(),
		"SelectedDepartmentIds": $('#SelectedDepartmentIds').val(),
		"SelectedDepartmentDescriptions": $('#SelectedDepartmentDescriptions').val(),
		"IsSearchClicked": true,
		"ActiveMobileTabClass": $('#ActiveMobileTabClass').val()
	}

	$.ajax({
		url: "/Crew/SetPageParameter",
		type: "POST",
		"data": input,
		success: function (data) {
			SetHiddenFields(data.data);
			crewFilterCount = 0;
			AppendTreeFilterDataInModel("#departmentTree", "#filterDepartment", "#filterCard2");
			AppendTreeFilterDataInModel("#rankCategoryTree", "#filterRank", "#filterCard1");
			var viewFilterVal = $('input[name="selCrewStatus"]:checked').data("description");
			AppendTextFilterDataInModel(viewFilterVal,"#filterView" ,"#filterCard3");
		},
		complete: function () {
			LoadCrewListDataTable(true);
		}
	});
}

function CallMedicalSignOff() {
	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val()
	}
	$.ajax({
		url: "/Crew/LoadMedicalSignOff",
		type: "POST",
		"data": input,
		success: function (data) {
			window.location.href = data;
		}
	});
}

function BindCrewSummary() {
	$.ajax({
		url: "/Crew/GetCrewSummary",
		type: "POST",
		"data": {
			"input": $('#EncryptedVesselId').val(),
		},
		"datatype": "JSON",
		success: function (data) {
			if (data != null && data != 'undefined' && data != '') {
				$("#spanCrewOnboard").text(data.onboardCount);
				$("#spanCrewOverdue").text(data.overdueCount);
				$("#spanCrewUnplannedBerth").text(data.unplannedBerthCount);
				$("#spanCrewOfficerProm").text(data.officerPromNewHireCount);
				$("#spanCrewMedicalSignOff").text(data.medicalSignOffCount);


				var medicalSignOffNav = "/Crew/MedicalSignOffList/?CrewList=" + data.medicalSignOffURL + "&VesselId=" + $('#EncryptedVesselId').val();

				//Remove all click event
				$(".click-event-off").off('click');

				$("#aOnboardURL").click(function () {
					SetSummaryFiltersInTempData(data.onboardURL)
				})
				$("#aOverDueURL").click(function () {
					SetSummaryFiltersInTempData(data.overdueURL)
				})
				$("#aUnplannedBerth").click(function () {
					SetSummaryFiltersInTempData(data.unplannedBerthURL)
				})

				$("#aMedicalSignOff").attr("href", medicalSignOffNav);
			}
			$('.height-equal').matchHeight();
			
		}
	});
}

function SetFilters() {
	crewFilterCount = 0;

	SetViewRdioButtonFilter();

	var from = moment($('#FromDate').val(), 'DD-MM-YYYY');
	var to = moment($('#ToDate').val(), 'DD-MM-YYYY');

	setDateDetails(from, to, false);

	if ($('#SelectedStageFilter').val() == 'Onboard') {
		$('#dtrcrewlist').prop('disabled', false);
	}
	else {
		var currentDate = moment();
		var start = moment(currentDate, 'DD-MM-YYYY');
		var end = moment(currentDate, 'DD-MM-YYYY');
		setDateDetails(start, end, false);
		$('#dtrcrewlist').prop('disabled', true);
	}

	SetDepartmentTree();
	SetRankCategoryTree();

	SetMobileTabWindow();
}

function SetViewRdioButtonFilter() {
	$('#csOnboard, #csOverdue, #csUnplannedBerth').prop('checked', false);

	if ($('#SelectedStageFilter').val() == "Onboard") {
		$('#csOnboard').prop('checked', true);
	}
	else if ($('#SelectedStageFilter').val() == "Overdue") {
		$('#csOverdue').prop('checked', true);
	}
	else if ($('#SelectedStageFilter').val() == "UnplannedBerth") {
		$('#csUnplannedBerth').prop('checked', true);
	}

	var viewFilterVal = $('input[name="selCrewStatus"]:checked').data("description");
	AppendTextFilterDataInModel(viewFilterVal, "#filterView", "#filterCard3");
}

function SetHiddenFields(data) {
	$("#EncryptedVesselId").val(data.encryptedVesselId);
	$("#FromDate").val(data.fromDate);
	$("#ToDate").val(data.toDate);
	$("#SelectedStageFilter").val(data.selectedStageFilter);
	$("#SelectedStageName").val(data.selectedStageName);
	$("#SelectedFilter").val(data.selectedFilter);
	$("#VesselName").val(data.vesselName);
	$("#SelectedRankIds").val(data.selectedRankIds);
	$("#SelectedRankDescriptions").val(data.selectedRankDescriptions);
	$("#SelectedDepartmentIds").val(data.selectedDepartmentIds);
	$("#SelectedDepartmentDescriptions").val(data.selectedDepartmentDescriptions);
	$("#ActiveMobileTabClass").val(data.activeMobileTabClass);
	$("#CrewChangeDate").val(data.crewChangeDate);
	$("#IsSearchClicked").val(data.isSearchClicked);
	$("#GridSubTitle").val(data.gridSubTitle);
}

function MaintainFilters() {
	$.ajax({
		url: "/Crew/MaintainFilter",
		type: "POST",
		success: function (data) {
			if (data.isTempDataExist) {
				var response = data.data;
				SetHiddenFields(response);
				SetFilters();
			}
			else {
				SetViewRdioButtonFilter();
			}
		},
		complete: function () {
			LoadCrewListDataTable(false);
		}
	});
}

function SetSummaryFiltersInTempData(data) {
	$.ajax({
		url: "/Crew/SetSummaryFilters",
		type: "POST",
		data: { "crewUrl": data, "vesselId": $('#EncryptedVesselId').val() },
		success: function (data) {
			var response = data.data;
			SetHiddenFields(response);
			SetFilters();
			LoadCrewListDataTable(false);

			SetMobileTabWindow();
		}
	});
}

function LoadDepartmentTree() {

	$("#departmentTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/Crew/GetDepartmentTreeList",
			dataType: "json"
		}),
		init: function (event, data) {
			SetDepartmentTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function LoadRankCategoryTree() {

	$("#rankCategoryTree").fancytree({
		checkbox: true,
		selectMode: 3,
		icon: false,
		source: $.ajax({
			url: "/Crew/GetRankCategoryTreeList",
			dataType: "json"
		}),
		init: function (event, data) {
			SetRankCategoryTree();
		},
		click: function (e, data) {
			if (data.targetType === 'title') {
				data.node.toggleSelected();
			}
		},
	});
}

function SetDepartmentTree() {
	$("#departmentTree").fancytree("getTree").visit(function (node) {
		var departmentIdList = $('#SelectedDepartmentIds').val().split(',');
		departmentIdList.forEach(function () {
			if (departmentIdList.includes(node.key)) {
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
	AppendTreeFilterDataInModel("#departmentTree", "#filterDepartment", "#filterCard2");
}

function SetRankCategoryTree() {
	$("#rankCategoryTree").fancytree("getTree").visit(function (node) {
		var rankCategoryIdList = $('#SelectedRankIds').val().split(',');
		rankCategoryIdList.forEach(function () {
			if (rankCategoryIdList.includes(node.key)) {
				node.setSelected(true);
			}
			else {
				node.setSelected(false);
			}
		});
	});
	AppendTreeFilterDataInModel("#rankCategoryTree", "#filterRank", "#filterCard1");
}

function GetSelectedDeartment() {
	var tree = $('#departmentTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	var selectednodestitle = nodes.map(x => x.title);

	$('#SelectedDepartmentDescriptions').val(selectednodestitle.join(","));
	$('#SelectedDepartmentIds').val(selectednodes.join(","));
}

function GetSelectedRank() {
	var tree = $('#rankCategoryTree');
	var nodes = tree.fancytree('getTree').getSelectedNodes();
	var selectednodes = nodes.map(x => x.key);
	var selectednodestitle = nodes.map(x => x.title);

	$('#SelectedRankDescriptions').val(selectednodestitle.join(","));
	$('#SelectedRankIds').val(selectednodes.join(","));
}

function SetMobileTabWindow() {
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
	}
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
		htmlElement += '<div class="col-12 col-md-12 col-lg-12 col-xl-12">';
		htmlElement += '<div class="dashboard-counters-label"><span id="">' + value + '</span></div></div></div>';
	});

	return htmlElement;
}

function AppendGridTitle() {

	hideShowFilterDesign();
	$('#appliedFilterCount').text(crewFilterCount);

	$("#tableSubTitle").hide();
	var subtitle = $("#GridSubTitle").val();
	if (!IsNullOrEmptyOrUndefined(subtitle) && subtitle != "All") {
		$("#tblspndtrcrewlist").hide();
		$("#tableSubTitle").show();
		$("#tableSubTitle").text(" - " + subtitle);
	}
	else {
		$("#tblspndtrcrewlist").show();
	}
}

function AppendTreeFilterDataInModel(treeSelector, filterId, filterCardId) {
	var StatusTree = $(treeSelector).fancytree('getTree').getSelectedNodes();
	var StatusTreeArray = GetUniqueChildArr(StatusTree);
	var StatusHtmlElement = GetFilterHtmlElement(StatusTreeArray);

	if (StatusTreeArray.size > 0) {
		$(filterId).html(StatusHtmlElement);
		crewFilterCount = crewFilterCount + StatusTreeArray.size;
		$('#appliedFilterCount').text(crewFilterCount);
		$(filterCardId).show();
	}
	else {
		$(filterId).html("");
		$(filterCardId).hide();
	}

	if (treeSelector == "#rankCategoryTree") {
		FilterCountSet(StatusTreeArray.size, "#rankFilterCount");
	} else if (treeSelector == "#departmentTree") {
		FilterCountSet(StatusTreeArray.size, "#departmentFilterCount");
	}


	hideShowFilterDesign();
}

function AppendTextFilterDataInModel(filteredValue, filterId, filterCardId) {

	if (!IsNullOrEmptyOrUndefined(filteredValue) && filteredValue !== undefined) {
		crewFilterCount++;
		$(filterId).text(filteredValue);
		$('#appliedFilterCount').text(crewFilterCount);
		$(filterCardId).show();
	}
	else {
		$(filterId).text("");
		$(filterCardId).hide();
	}
	hideShowFilterDesign();
}

function hideShowFilterDesign() {
	if (crewFilterCount > 0) {
		$(".filter-design, .clear-filter").show();
		$("#divfilterhide").removeClass('btn-dark-grey');
		$("#divfilterhide").addClass('btn-info');
	}
	else {
		$(".filter-design, .clear-filter").hide();
		$("#divfilterhide").removeClass('btn-info');
		$("#divfilterhide").addClass('btn-dark-grey');
	}
}

function clearSearchFilter() {
	crewFilterCount = 0;
	//clear search filter here
	//date range will be todays date and will be enabled
	var currentDate = moment();
	var start = moment(currentDate, 'DD-MM-YYYY');
	var end = moment(currentDate, 'DD-MM-YYYY');
	setDateDetails(start, end, false);
	$('#dtrcrewlist').prop('disabled', false);

	//setting crew status drop down to onboard
	$('#csOnboard').prop('checked', true);
	$('#SelectedStageFilter').val('Onboard');

	//clearing hidden parameters
	$('#SelectedDepartmentIds').val('');
	$('#SelectedDepartmentDescriptions').val('');

	//clearing hidden parameters
	$('#SelectedRankDescriptions').val('');
	$('#SelectedRankIds').val('');

	SetDepartmentTree();
	SetRankCategoryTree();
	var viewFilterVal = $('input[name="selCrewStatus"]:checked').data("description");
	AppendTextFilterDataInModel(viewFilterVal, "#filterView", "#filterCard3");

	$('#IsSearchClicked').val('');
	//add call to onboardlink
	$("#aOnboardURL")[0].click();
	FilterCountSet(0, ".filtercount");
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
