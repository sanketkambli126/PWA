import "bootstrap";
import moment from "moment";

import toastr from "toastr";
import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader, GetFormattedDate } from "../common/datatablefunctions.js"
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, base64ToArrayBuffer, saveByteArray, MobileTab_Overview, MobileTab_List, Mobile_Tabs, createFlatRadioButton, SetHeaderMargin, ToastrAlert, GetCookie, BackButton, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, GetValueOrDefaultDT, IsNullOrEmptyOrUndefinedLooseTyped, RemoveClassIfPresent, AddClassIfAbsent, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js"
import { DateRangePickerCancelText, DateRangePickerLabelText, CertificateListPageKey, Tab1, Tab2, MobileScreenSize } from "../common/constants.js"
var gridCertificateList;
var gridChangelogsList;
var gridRenewalHistoryList;
var gridRequisitionOrderList;
var PMSCertificateList;

var columnIndex_Doc = 1, columnIndex_DiscussionChat = 2, columnIndex_DiscussionNotes = 3, columnIndex_checkBox = 0;
const ColumnHeader_EOW = "EOW Date"; var gridDocumentList;
const DateFormat = "DD MMM YYYY";
var selectedStartDate;
var selectedEndDate;
var noOfEnabledCheckboxes;
var data;
var selected = [];
var ispageLoad;

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
	$("#aCertificateTotalActive").trigger('click');
});

$(document).on('change', 'input[type=radio][name=selStatus]', function () {
	StatusFilterCountSet(this.value);
});

$(document).on('change', 'input[type=radio][name=selImpact]', function () {
	ImpactFilterCountSet(this.value);
});

$(document).on('change', 'input[type=radio][name=selType]', function () {
	TypeFilterCountSet(this.value);
});

$(document).on('change', '#chckIncludeWindow', function () {
	ExpiryFilterCountSet();
});



$(document).ready(function () {
	
	InitializeListDiscussionAndNoteClickEvents(code);

	AddLoadingIndicator();
	RemoveLoadingIndicator();
	$("#btnAdvDownloadMobile").hide();

	$(document).click(function () {
		if ($("#mobileActiondropdown").hasClass('show')) {
			$("#mobileActiondropdown").removeClass('show');
		}
	});

	$('#mobiletoggle').click(function () {
		$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
	});

	$("#dateExpiringBtwn").caleran(
		{
			showButtons: true,
			hideOutOfRange: true,
			showOn: "top",
			arrowOn: "right",
			startEmpty: true,
			format: "DD MMM YYYY",
			cancelLabel: "Clear",
			ranges: [
				{
					title: "3 Months",
					startDate: moment().subtract(3, "month"),
					endDate: moment()
				},
				{
					title: "6 Months",
					startDate: moment().subtract(6, "month"),
					endDate: moment()
				},
				{
					title: "9 Months",
					startDate: moment().subtract(9, "month"),
					endDate: moment()
				},
				{
					title: "12 Months",
					startDate: moment().subtract(12, "month"),
					endDate: moment()
				},

			],
			rangeOrientation: "vertical",
			onafterselect: function (caleran, startDate, endDate) {
				setDateDetails(startDate, endDate);
			},
			onCancel: function (caleran, start, end) {
				setDateDetailonCancel();
				return true;
			}
		}
	);


	$('#dateExpiringBtwn').val(DateRangePickerLabelText);

	if ($('#dateExpiringBtwn').val() == "" || $('#dateExpiringBtwn').val() == DateRangePickerLabelText || $('#dateExpiringBtwn').val() == undefined) {
		var fromDate = $('#FromDate').val();
		var toDate = $('#ToDate').val();
		if (fromDate != "" && toDate != "") {
			var start = moment(fromDate, 'DD-MM-YYYY');
			var end = moment(toDate, 'DD-MM-YYYY');
			selectedStartDate = start.format(DateFormat);
			selectedEndDate = end.format(DateFormat);
			$('#dateExpiringBtwn').val(selectedStartDate + ' - ' + selectedEndDate);
		}
	}

	LoadImpactDropdown();
	LoadStatusDropdown();
	LoadTypeDropdown();

	BindSummary();
	$('#btnSendEmail').attr("disabled", true);

	$("#btnClear").click(function () {
		$("#aCertificateTotalActive").trigger('click');
	})

	$("#btnSearch").click(function () {
		$('#StageName').val("");
		SetPageParameter();
		hideAdvDowloadGrid();
	});

	var input = {
		"vesselId": $('#VesselId').val(),
		"stageName": $('#StageName').val(),
		"vesselName": $('#VesselName').val(),
		"CertificateImpact": $('#CertificateImpact').val(),
		"CertificateStatus": $('#CertificateStatus').val(),
		"CertificateType": $('#CertificateType').val(),
		"includeWindow": $('#chckIncludeWindow').is(':checked'),
		"ToDate": selectedEndDate,
		"FromDate": selectedStartDate,
		"SearchKeyword": $("#searchInput").val(),
		"stageName": $('#StageName').val()
	};
	GetCertificateList(input);

	$('.btnAdvDownload').on('click', function () {
		showAdvDowloadGrid();
	});

	$('#btnAdvCancel').on('click', function () {
		hideAdvDowloadGrid();
	});

	$('#btnDownloadSelection').on('click', function () {
		fetchAttachments("", gridCertificateList);
	});

	$('#AdvDownSelectAll').on('click', function (e) {

		if (this.checked) {
			$('input[type="checkbox"].select-checkbox-all:not(:checked)').trigger('click');
		} else {
			$('input[type="checkbox"].select-checkbox-all').trigger('click');
		}
	});

	$('#dtCertificateList').on('change', '.select-checkbox, .select-checkbox-all', function () {
		var rows_selected = gridCertificateList.column(0).checkboxes.selected();
		if (rows_selected.length == 0) {
			$("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
		}
		else {
			$("#btnDownloadSelection").removeClass('disabled').removeAttr('aria-disabled');
		}
		if (rows_selected.length > 0) {
			let rows = gridCertificateList.rows().nodes();
			noOfEnabledCheckboxes = $('input[type="checkbox"].select-checkbox', rows).not(":disabled").length;

			if (noOfEnabledCheckboxes == rows_selected.length) {
				$("#AdvDownSelectAll").prop({
					checked: true,
				});
			} else {
				$("#AdvDownSelectAll").prop({
					indeterminate: true,
				});
			}
		} else {
			$("#AdvDownSelectAll").prop({
				indeterminate: false,
				checked: false
			});
		}

		$('#AdvDocSelected').text(rows_selected.length);
	});

	$(document).on("click", ".close-popover", function () {
		$('.popover').popover('hide');
		$('body').removeClass('popover-design');
	});

	$(document).on('click', 'body', function (e) {
		$('[data-toggle=popover]').each(function () {
			if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
				$(this).popover('hide');
			}
		});
	});

	$(".btn-export-modal").click(function () {
		$('#chckIncludePMSCertificate').prop("checked", false);
		$('#chckIncludeVesselCertificate').prop("checked", true);
		$('#modalExportExcel').modal('show');
	});

	$('.btnExportExcel').click(() => {
		var len = $("#divChkboxExportExcel input[name='checkboxExport']:checked").length;
		if (len > 0) {

			if ($('#chckIncludePMSCertificate').is(':checked')) {
				GetPMSCertificates();
			}
			else {
				configExportToExcel();
			}

			$('#modalExportExcel').modal('hide');
		}
	});

	$('#dtCertificateList tbody').on('click', 'a.certificateDetails', function () {
		var data = gridCertificateList.row($(this).parents('tr')).data();
		$('#issuedDateModel').text("-");
		$('#certifcateNameModel').text("-");
		$('#issuedByModel').text("-");
		$('#notesModel').text("-");

		if (data.issuedBy != null && data.issuedBy != "") {
			$('#issuedByModel').text(data.issuedBy);
		}
		if (data.notes != null && data.notes != "") {
			$('#notesModel').text(data.notes);
		}
		if (data.dateFrom != null && data.dateFrom != "") {
			$('#issuedDateModel').text(GetFormattedOnlyDate(data.dateFrom));
		}
		if (data.certificateFullName != null && data.certificateFullName != "") {
			$('#certifcateNameModel').text(data.certificateFullName);
		}

		$.ajax({
			url: "/Certificate/GetCertificateRenewalHistory",
			type: "POST",
			dataType: "JSON",
			data: {
				"VesselCertificateId": data.vesselCertificateId
			},
			success: function (data) {
				$('#dtRenewalHistory').DataTable().destroy();
				gridRenewalHistoryList = $('#dtRenewalHistory').DataTable({
					"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-3 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
					"processing": false,
					"serverSide": false,
					"lengthChange": true,
					"searching": false,
					"info": true,
					"scrollY": "150px",
					"scrollCollapse": true,
					"autoWidth": false,
					"paging": false,
					"pageLength": 10,
					"order": [[1, "asc"]],
					"language": {
						"emptyTable": "No renewal history available.",
						"loadingRecords": "&nbsp;"
					},
					"data": data.data,
					"columns": [
						{
							className: "data-icon-align mobile-popover-attachments tdblock",
							//data: "certificateLogId",
							width: "15px",
							orderable: false,
							render: function (data, type, full, meta) {
								if (!full.isDocumentsAvailable) {
									return '<a class="text-black" target="_blank"><img src="/images/Download-doc-inactive.png" class="m-0 align-top" width="18" title="Download"/></a>';
								}
								else {
									var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';
									var uniqueId = full.certificateLogId;
									var element = '';

									element = '<a class="text-black documentPopup renewal-history-doc-popup cursor-pointer universalIdentifier_' + uniqueId + '" target="_blank" ><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
									return element;
								}
							}
						},
						{
							className: "data-text-align tdblock top-margin-0 ",
							data: "issuedBy",
							width: "30px",
							render: function (data, type, full, meta) { return GetCellData('Issued By', data); }
						},
						{
							className: "data-datetime-align",
							data: "issueDate",
							width: "20px",
							render: function (data, type, full, meta) {
								var date = "";
								var formattedDate = "";
								if (data != null) {
									date = new Date(data);
									formattedDate = moment(date).format("D MMM YYYY");
								}
								if (type === "display") {
									return GetCellData('Issued', formattedDate);
								}
								return date;
							}
						},
						{
							className: "data-datetime-align",
							data: "expiryDate",
							width: "20px",
							render: function (data, type, full, meta) {
								var date = "";
								var formattedDate = "";
								if (data != null) {
									date = new Date(data);
									formattedDate = moment(date).format("D MMM YYYY");
								}
								if (type === "display") {
									return GetCellData('Expiry', formattedDate);
								}
								return date;
							}
						},
						{
							className: "data-text-align",
							data: "location",
							width: "30px",
							render: function (data, type, full, meta) { return GetCellData('Location', data); }
						},
						{
							className: "data-text-align",
							data: "updatedBy",
							width: "30px",
							render: function (data, type, full, meta) { return GetCellData('Updated By', data); }
						},
						{
							className: "data-datetime-align",
							data: "updatedOn",
							width: "20px",
							render: function (data, type, full, meta) {
								var date = "";
								var formattedDate = "";
								if (data != null) {
									date = new Date(data);
									formattedDate = moment(date).format("D MMM YYYY");
								}
								if (type === "display") {
									return GetCellData('Updated', formattedDate);
								}
								return date;
							}
						}
					]
				});
			}
		});

		$.ajax({
			url: "/Certificate/GetCertificateAuditLog",
			type: "POST",
			dataType: "JSON",
			data: {
				"VesselCertificateId": data.vesselCertificateId,
				"VesselId": $('#VesselId').val(),
			},
			success: function (data) {
				$('#dtChangelogs').DataTable().destroy();
				gridChangelogsList = $('#dtChangelogs').DataTable({
					"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-3 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
					"processing": false,
					"serverSide": false,
					"lengthChange": true,
					"searching": false,
					"info": true,
					"scrollY": "130px",
					"scrollCollapse": true,
					"autoWidth": false,
					"paging": false,
					"pageLength": 10,
					"order": [[0, "desc"]],
					"language": {
						"emptyTable": "No change logs available.",
						"loadingRecords": "&nbsp;"
					},
					"data": data.data,
					"columns": [
						{
							className: "data-datetime-align top-margin-0",
							data: "logDateLocal",
							width: "20px",
							render: function (data, type, full, meta) {
								var date = "";
								var formattedDate = "";
								if (data != null) {
									date = new Date(data);
									formattedDate = moment(date).format("D MMM YYYY");
								}
								if (type === "display") {
									return GetCellData('Local Date', formattedDate);
								}
								return date;
							}
						},
						{
							className: "data-text-align  top-margin-0",
							data: "updatedByName",
							width: "50px",
							render: function (data, type, full, meta) { return GetCellData('Updated By', data); }
						},
						{
							className: "data-text-align tdblock",
							data: "event",
							width: "40px",
							render: function (data, type, full, meta) { return GetCellData('Action', data); }
						},
						{
							className: "data-text-align tdblock",
							data: "remarks",
							width: "150px",
							render: function (data, type, full, meta) { return GetCellData('Remarks', data); }
						},

					]
				});
			}
		});
	});

	ConfigurePopover();

	ConfigureDownloadPopup();

	$(".modal").on("hidden.bs.modal", function () {
		//renewal history modal details
		$("#issuedByModel").text("-");
		$("#certifcateNameModel").text("-");
		$("#issuedDateModel").text("-");
		$("#notesModel").text("-");

		//requisition modal details
		$('#certifcateNameModelReq').text("-");
		$('#velidityModelReq').text("-");
		$('#issuedDateModelReq').text("-");
		$('#expiryDateModelReq').text("-");

		try {
			if (gridChangelogsList != undefined) { gridChangelogsList.clear().draw(); }
			if (gridChangelogsList != undefined) { gridRenewalHistoryList.clear().draw(); }
			if (gridChangelogsList != undefined) { gridRequisitionOrderList.clear().draw(); }
		} catch (e) {
			console.log(e.message);
		}
	});

	$('#dtCertificateList tbody').on('click', 'a.RequisitionLinkDetails', function () {
		var data = gridCertificateList.row($(this).parents('tr')).data();
		BindRequisitionLinkDetails(data);
	});

	BackButton(CertificateListPageKey, true)
	RegisterTabSelectionEvent('.mobileTabClick', CertificateListPageKey);
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
		SetHeaderMargin();
	}
	
	$("#btnPMSCertificate").click(function () {
		CallPlannedMaintenance();
	});

	AjaxError();

	$("#searchInput").keyup(function () {
		KeywordFilterCountSet($(this).val());
	});

	$('#dateExpiringBtwn').on('input', function () {
		ExpiryFilterCountSet();
	});

});



function setDateDetails(startDate, endDate) {
	$('#dateExpiringBtwn').val(startDate.format(DateFormat) + ' - ' + endDate.format(DateFormat));
	selectedStartDate = startDate.format(DateFormat);
	selectedEndDate = endDate.format(DateFormat);
	ExpiryFilterCountSet();
}


function setDateDetailonCancel() {
	selectedStartDate = '';
	selectedEndDate = '';
	$('#dateExpiringBtwn').val(DateRangePickerLabelText);
}


function BindSummary() {
	var request =
	{
		"menuType": $('#MenuType').val(),
		"vesselId": $('#VesselId').val()
	}
	$.ajax({
		url: "/Certificate/GetHeaderSummary",
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

				$(".click-event-off").off('click');

				$("#aCertificateOverdue").click(function () {
					BindCertificateDashboardDetails(data.overDueCertificateCountURL);
				});

				$("#aCertificateExpiringIn30Days").click(function () {
					BindCertificateDashboardDetails(data.expires30DaysCertificateCountURL);
				});

				$("#aCertificateWithinSurveyRange").click(function () {
					BindCertificateDashboardDetails(data.surveyRangeCertificateCountURL);
				});

				$("#aCertificateTotalActive").click(function () {
					BindCertificateDashboardDetails(data.allActiveCertificateCountURL);
				});

			}
		},
		complete: function () {
			$('.height-equal').matchHeight();
		}
	});
}

function BindCertificateDashboardDetails(CertificateUrl) {

	var $selStatusRadios = $('input[type=radio][name=selStatus]');
    setRadioButtonValue($selStatusRadios, "", "All");

	var $selImpactRadios = $('input[type=radio][name=selImpact]');
	setRadioButtonValue($selImpactRadios, "", "All");

	var $selTypeRadios = $('input[type=radio][name=selType]');
	setRadioButtonValue($selTypeRadios, "", "");
	
	$('#dateExpiringBtwn').val(DateRangePickerLabelText);
	$("#searchInput").val('');
	$('#chckIncludeWindow').prop('checked', false);
	selectedStartDate = '';
	selectedEndDate = '';
	KeywordFilterCountSet('');

	$.ajax({
		url: "/Certificate/BindCertificateDashboardDetails",
		type: "POST",
		dataType: "JSON",
		data: { "certificateUrl": CertificateUrl, "vesselId": $('#VesselId').val() },
		success: function (data) {

			
			SetHiddenFields(data.data);
			SetFilterData();
			if (($(window).width() < MobileScreenSize)) {
				var MobilTabCls = $("#ActiveMobileTabClass").val();
				$('.' + MobilTabCls)[0].click();
			}
			var responce = data.data;
			var input = {
				"vesselId": responce.vesselId,
				"stageName": responce.stageName,
				"vesselName": responce.vesselName,
			};

			GetCertificateList(input);
		}
	});
}

function GetFormattedOnlyDate(data) {
	if (data == null) return "";
	var date = new Date(data);
	return moment(date).format("D MMM YYYY");
}

function GetCertificateList(input) {
	var rowCount = 0;
	var masterCheckBox = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
	masterCheckBox += '<input type = "checkbox" class="select-checkbox-all custom-control-input" id="masterCheckboxAll">';
	masterCheckBox += '<label class="custom-control-label d-block" for="masterCheckboxAll"></label></div >';

	$('#dtCertificateList').DataTable().destroy();
	gridCertificateList = $('#dtCertificateList').DataTable({
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
		"pageLength": 10,
		"order": [[5, "asc"]],
		"language": {
			"emptyTable": "No certificates available.",
			"loadingRecords": "&nbsp;",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		'columnDefs': [
			{
				'orderable': false,
				'targets': 0,
				'visible': false,
				'render': function (data, type, row, meta) {
					var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
					uielement += '<input type = "checkbox" class="select-checkbox custom-control-input dt-checkboxes" id= "' + row.vesselCertificateLogId + '">';
					uielement += '<label class="custom-control-label d-block" for= "' + row.vesselCertificateLogId + '"></label></div >';
					data = uielement;
					return data;
				},
				'createdCell': function (td, cellData, rowData, row, col) {
					if (rowData.documentCount == 0) {

						let child = $(td).children()[0];
						$(child).addClass('invisible');
						$(td).removeClass('dt-checkboxes-cell');
						this.api().cell(td).checkboxes.disable();
					}
				},
				'checkboxes': {
					'selectRow': true,
					'selectAllRender': masterCheckBox
				}
			},
		],
		'select': {
			'style': 'multi'
		},
		"ajax": {
			"url": "/Certificate/GetCertificateList",
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
				title: "Certificate Report",
				messageTop: function () {
					let FilterText = '';
					rowCount = 4;
					var SearchInput = $("#searchInput").val();
					var CertificateStatus = $('input[name="selStatus"]:checked').val();
					var CertificateType = $('input[name="selType"]:checked').val();
					var dateExpiringBtwn = $('#dateExpiringBtwn').val();
					var IncludeWindow = $('#chckIncludeWindow').is(':checked');
					var impactVal = $('input[name="selImpact"]:checked').data("description");

					if (!IsNullOrEmptyOrUndefined(SearchInput)) {
						FilterText += '\nCertificate Name / Number / Issued By : ' + SearchInput;
						rowCount++;
					}

					if (!IsNullOrEmptyOrUndefined(CertificateStatus)) {
						FilterText += '\nCertificate Status : ' + CertificateStatus;
						rowCount++;
					}

					if (!IsNullOrEmptyOrUndefined(CertificateType)) {
						FilterText += '\nCertificate Type : ' + CertificateType;
						rowCount++;
					}

					if (!IsNullOrEmptyOrUndefined(impactVal)) {
						FilterText += '\nImpact Value : ' + impactVal;
						rowCount++;
					}

					if (!IsNullOrEmptyOrUndefined(dateExpiringBtwn) && !(dateExpiringBtwn === DateRangePickerLabelText)) {
						FilterText += '\nExpiry Date : ' + dateExpiringBtwn;
						rowCount++;
					}

					if (IncludeWindow) {
						FilterText += '\nInclude Window : Yes';
						rowCount++;
					}

					return 'Vessel : ' + $('#VesselName').val()						//Vessel Name
						+ '\nDate : ' + moment(new Date()).format("D MMM YYYY")		//Date
						+ FilterText												//Filters Applied
						+ '\n';
				},
				customize: function (xlsx) {

					if ($('#chckIncludePMSCertificate').is(':checked') && $('#chckIncludeVesselCertificate').is(':checked')) {
						CustomizedExcelHeader(xlsx, rowCount);
						setSheetName(xlsx, 'Vessel_Certificate');
						addSheet(xlsx, '', 'PMS Certificates', 'PMS_Certificates', '2');
					}
					else if ($('#chckIncludePMSCertificate').is(':checked') && !$('#chckIncludeVesselCertificate').is(':checked')) {
						addSheet(xlsx, '', 'PMS Certificates', 'Sheet2', '0');
						setSheetName(xlsx, 'PMS_Certificates');
					}
					else {
						CustomizedExcelHeader(xlsx, rowCount);
						setSheetName(xlsx, 'Vessel_Certificate');
					}
				}				
			},
			'pdf', 'print'
		],
		"createdRow": function (row, data, dataIndex) {
			if (data.isActive) {
				if (data.rangeType == "Overdue") {
					$(row).find('td').addClass('txt-red');
				}
				else if (data.rangeType == "WithinSurveyRange") {
					$(row).find('td').addClass('txt-orange');
				}
				else if (data.rangeType == "DueNow") {
					$(row).find('td').addClass('txt-purple');
				}
			}
		},
		"columns": [

			{
				"data": "vesselCertificateLogId",
				orderable: false,
				className: "mobile-popover-attachments tdblock data-icon-align checkbox-design-table",
				width: "30px",
			}, {
				className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
				orderable: false,
				width: "30px",
				render: function (data, type, full, meta) {
					if (full.documentCount == 0) {
						return '';
					}
					else {
						var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';

						var uniqueId = full.vesselCertificateLogId;
						var element = '';

						element = '<a class="text-black documentPopup cursor-pointer universalIdentifier_' + uniqueId + '" target="_blank" ><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
						return element;
					}
				}
			},
			{
				className: "data-icon-align d-none d-md-table-cell",
				orderable: false,
				width: "10px",
				render: function (data, type, full, meta) {
					if (full.channelCount > 0) {
						return GetChatBaseIcons(full.vesselCertificateId, full.channelCount, full.messageDetailsJSON);
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
						return GetNotesBaseIcons(full.vesselCertificateId, full.notesCount, full.messageDetailsJSON);
					} else {
						return '';
					}
				}
			},
			{
				className: "data-icon-align tdblock tdblock d-md-none d-lg-none d-xl-none",
				orderable: false,
				width: "70px",
				render: function (data, type, full, meta) {
					if (full.channelCount > 0 || full.notesCount > 0) {
						return GetChatNotesBaseIcons(full.vesselCertificateId, full.channelCount, full.notesCount, full.messageDetailsJSON);
					} else {
						return '';
					}
				}
			},
			{
				className: "data-number-align mr-2 width-auto",
				data: "certificateNumber",
				width: "30px",
				render: function (data, type, full, meta) {
					return GetActualCellData(GetValueOrDefaultDT(full.certificateNumber));
				}
			},
			{
				className: "data-text-align width-65",
				data: "certificateFullName",
				width: "200px",
				render: function (data, type, full, meta) {
					var UIElements = "";
					let CertificateIncompleteIcon = "";
					let CertificateDeletedIcon = "";
					let CertificareInActiveIcon = "";

					var RequisitionLink = "";

					if (full.mappedOrderCount > 0) {
						var RequisitionIcon = '<i class="fa fa-fw" aria-hidden="true" data-trigger="hover" data-placement="bottom" title="Mapped Requisition"></i>';
						RequisitionLink = '<a class="RequisitionLinkDetails cursor-pointer float-none float-md-right" href="javascript:void(0)"  data-toggle="modal" data-target="#requisitionLinkModel">' + RequisitionIcon + '</a>';
					}

					if (full.isCertificateIncomplete) {

					}
					else {
						CertificateIncompleteIcon = "";
					}

					if (full.isCertificateDeleted) {
						CertificateDeletedIcon = "<img src='/images/DeletedCertificate.png' class='float-none float-md-right ml-1 mt-sm-0 mt-md-1' width='15' data-html='true' title='Deleted certificate' data-toggle='tooltip' data-placement='bottom'>";
					}
					else {
						CertificateDeletedIcon = "";
					}

					if (full.isCertificateInActive) {
						CertificareInActiveIcon = "<img src='/images/DeactiveCertificate.png' class='float-none float-md-right ml-1 mt-sm-0 mt-md-1' width='15' data-html='true' title='Inactive certificate' data-toggle='tooltip' data-placement='bottom'>";
					}
					else {
						CertificareInActiveIcon = "";
					}

					var certificateName = '<a class="cursor-pointer" href="/Certificate/Details/?CertificateRequest=' + full.certificateDetailsUrl + '&VesselId=' + $('#VesselId').val() + '"> ' +data+'</a > ';
					UIElements = CertificateIncompleteIcon + CertificateDeletedIcon + CertificareInActiveIcon + RequisitionLink;
					return GetActualCellData(GetValueOrDefaultDT( certificateName + '  ' + UIElements));
				}

			},
			{
				className: "data-number-align",
				data: "validity",
				width: "20px",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Validity',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-datetime-align",
				"data": "dateFrom",
				type: "date",
				width: "75px",
				render: function (data, type, full, meta) {
					if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
						return GetCellData('Issue Date', GetValueOrDefaultDT(data));
					} else {
						return GetFormattedDate(type, 'Issue Date', data);
                    }
				}
			},
			{
				className: "data-datetime-align",
				"data": "dateTo",
				width: "65px",
				type: "date",
				render: function (data, type, full, meta) {
					if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
						return GetCellData('Expiry Date', GetValueOrDefaultDT(data));
					} else {
						return GetFormattedDate(type, 'Expiry Date', data);
					}
					
				}
			},
			{
				className: "data-datetime-align",
				"data": "endOfWindowDate",
				width: "65px",
				type: "date",
				render: function (data, type, full, meta) {
					if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
						return GetCellData('End of Window', GetValueOrDefaultDT(data));
					} else {
						return GetFormattedDate(type, 'End of Window', data);
					}
				}
			},
			{
				className: "data-number-align",
				"data": "windowFormattedString",
				width: "50px",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Window',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-text-align",
				"data": "isMandatoryCertificate",
				width: "20px",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Mandatory',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-text-align",
				"data": "certificateType",
				width: "45px",
				render: function (data, type, full, meta) {
					return GetCellData('Type',GetValueOrDefaultDT(data));
				}
			},
			{
				className: "data-text-align",
				"data": "certificateImpact",
				width: "75px",
				render: function (data, type, full, meta) {
					return GetCellData('Impact',GetValueOrDefaultDT(data));
				}
			},
			{
				className: " data-text-align",
				"data": "issuedBy",
				width: "50px",
				render: function (data, type, full, meta) {
					return GetCellData('Issued By',GetValueOrDefaultDT(data));
				}
			},
			{
				className: " data-text-align",
				"data": "isActiveText",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetCellData('In Active',GetValueOrDefaultDT(data));
				}
			},
		],
		"fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
			let child = $(nRow).find('td:eq(3)').children();
			if (screen.width < MobileScreenSize && $(child).length == 0) {
				$(nRow).find('td:eq(3)').addClass('d-none');
			}
		},
		"initComplete": function (settings, data) {
			var subTitle = '';
			if (data.data != null && data.data != undefined && data.data.length > 0) {
				subTitle = data.data[0].gridTitle;
			}
			AppendGridTitle(subTitle);
			SetAppliedFilterAndCount();

			$('#dtCertificateList thead th').each(function () {
				var $td = $(this);
				if (ColumnHeader_EOW == $td.text()) {
					$td.attr('title', "End Of Window Date");
				}
			});
		}
	});
	if ($('#StageName').val() === "StopSailingAndTradingExpiring30Days") {
		$('.dt-infomationhed').removeClass('col-lg-3 offset-lg-4 col-xl-2 offset-xl-3');
		$('.dt-infomationhed').addClass('col-lg-3 offset-lg-4 col-xl-2 offset-xl-4');
		$('.filters-data').removeClass('col-md-3 col-lg-2 col-xl-3');
		$('.filters-data').addClass('col-md-3 col-lg-2 col-xl-2');
	}

	$.fn.DataTable.ext.pager.numbers_length = 4;
	$('#dtCertificateList').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});

}

function InitializePopoverConstructor() {
	$.fn.popover.Constructor.Default.whiteList.table = [];
	$.fn.popover.Constructor.Default.whiteList.tr = [];
	$.fn.popover.Constructor.Default.whiteList.td = [];
	$.fn.popover.Constructor.Default.whiteList.th = [];
	$.fn.popover.Constructor.Default.whiteList.div = [];
	$.fn.popover.Constructor.Default.whiteList.tbody = [];
	$.fn.popover.Constructor.Default.whiteList.thead = [];
	$.fn.popover.Constructor.Default.whiteList.a = [];
	$.fn.popover.Constructor.Default.whiteList.i = [];
	$.fn.popover.Constructor.Default.whiteList.span = [];
}

function ConfigurePopover() {
	//configuring popupover
	$('#dtCertificateList tbody').on('click', 'a.documentPopup', function () {
		InitializePopoverConstructor();
		$('body').addClass('popover-design');
		var data = gridCertificateList.row($(this).parents('tr')).data();
		var uniqueClsSel = 'universalIdentifier_' + data.vesselCertificateLogId;

		//if (data.documentCount == 1) {
		//	fetchAttachments(data.vesselCertificateLogId, gridCertificateList);
		//}
		//else

		if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
			CertificateDocumentsPopover(uniqueClsSel, data.vesselCertificateLogId);
		}
		else {
			$('.' + uniqueClsSel).popover('show');
		}
	});

	$(document).on('click', 'a.renewal-history-doc-popup', function () {
		InitializePopoverConstructor();
		$('body').addClass('popover-design');
		var data = gridRenewalHistoryList.row($(this).parents('tr')).data();
		var uniqueClsSel = 'universalIdentifier_' + data.certificateLogId;

		//if (data.documentCount == 1) {
		//	fetchAttachments(data.vesselCertificateLogId, gridRenewalHistoryList);
		//}
		//else

		if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
			CertificateDocumentsPopover(uniqueClsSel, data.certificateLogId);
		}
		else {
			$('.' + uniqueClsSel).popover('show');
		}

	});
}

function ConfigureDownloadPopup() {

	$(document).on('click', 'a.documentDownload', function () {

		var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
			'<div class="loader">' +
			'<div class="ball-clip-rotate">' +
			'<div></div>' +
			'</div>' +
			'</div>' +
			'</div>';

		var documentName = $(this).children('span.documentName').text();
		var documentId = $(this).children('span.documentId').text();
		var docfileName = $(this).children('span.documentfileName').text();
		var isWebAddressEditable = $(this).children('span.isWebAddressEditable').text();
		var webAddress = $(this).children('span.webAddress').text();

		if (isWebAddressEditable.trim().toLowerCase() == 'true') {
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
				url: "/Certificate/DownloadDocument",
				type: "POST",
				dataType: "JSON",
				global: false,
				data: {
					"input": JSON.stringify(input)
				},
				beforeSend: function (xhr) {
					$(".popover").block({
						message: $(" " + loadercontent),
					});
				},
				complete: function () {
					$(".popover").unblock();
				},
				success: function (data) {
					if (data.bytes != null) {
						var array = base64ToArrayBuffer(data.bytes);
						saveByteArray(fileName, array, data.fileType);
					} else {
						ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
					}
				}
			});
		}

	});

}

function GetCellData(label, data) {
	data = data == null ? "" : data;
	return '<label>' + label + '</label> <br />' + GetActualCellData(data);
}

function GetActualCellData(data) {
	return '<span class="export-Data">' + data + '</span>';
}

function SelectedStage(stageName) {
	if (stageName == "All") {
		return "All Active";
	}
	else if (stageName == "Overdue") {
		return "Overdue";
	}
	else if (stageName == "Expire30Days") {
		return "Expiring in 30 Days";
	}
	else if (stageName == "SurveyRange") {
		return "Within Survey Range";
	}
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

function LoadStatusDropdown() {

	$.ajax({
		url: "/Certificate/GetCertificateStatuses",
		type: "GET",
		dataType: "JSON",
		success: function (data) {
			$("#StatusRadiosContainer").html("");

			for (let i = 0; i < data.length; i++) {
				let opt = data[i];

				let statusRd = createFlatRadioButton("statusRd" + i, "selStatus", opt.identifier, opt.description);

				$("#StatusRadiosContainer").append(statusRd);
			}

			var $selRadio = $('input[type=radio][name=selStatus]');
			var $value = $("#CertificateStatus").val();
			setRadioButtonValue($selRadio, $value, "All");

			StatusFilterCountSet($value);
			
		}
	})
};

function LoadImpactDropdown() {
	$.ajax({
		url: "/Certificate/GetImpactValues",
		type: "GET",
		dataType: "JSON",
		success: function (data) {
			$("#ImpactRadiosContainer").html("");
			for (let i = 0; i < data.length; i++) {
				let opt = data[i];

				let impactRd = createFlatRadioButton("impactRd" + i, "selImpact", opt.identifier, opt.description);

				$("#ImpactRadiosContainer").append(impactRd);
			}

			var $selRadio = $('input[type=radio][name=selImpact]');
			var $value = $("#CertificateImpact").val();
			if (!IsNullOrEmptyOrUndefined($value)) {
				setRadioButtonValue($selRadio, $value, "All");
			}
			else {
				$selRadio.filter('[value=""]').prop('checked', true);
			}

			ImpactFilterCountSet($value);
			
		}
	})
}
function LoadTypeDropdown() {
	$.ajax({
		url: "/Certificate/GetCertificateTypes",
		type: "GET",
		dataType: "JSON",
		success: function (data) {
			$("#TypeRadiosContainer").html("");
			for (let i = 0; i < data.length; i++) {
				let opt = data[i];

				let selTypeRd = createFlatRadioButton("selTypeRd" + i, "selType", opt.identifier, opt.description);

				$("#TypeRadiosContainer").append(selTypeRd);
			}

			var $selRadio = $('input[type=radio][name=selType]');
			var $value = $("#CertificateType").val();
			setRadioButtonValue($selRadio, $value, "");

			TypeFilterCountSet($value);

		}
	})
}
function showAdvDowloadGrid() {
	$(".btnAdvDownload").hide();
	if ($("#mobileActiondropdown").hasClass('show')) {
		$("#mobileActiondropdown").removeClass('show');
	}
	$(".grid-action-panel").show();
	$('.app-main__outer .background-padding').addClass('download-attachment-margin');
	//Get the column API object
	var ChkBoxColumn = gridCertificateList.column(0);
	var IconColumn = gridCertificateList.column(1);
	$('.select-checkbox-all').prop({
		indeterminate: false,
		checked: false
	});
	$('#AdvDownSelectAll').prop({
		indeterminate: false,
		checked: false
	});
	ChkBoxColumn.visible(true);
	IconColumn.visible(false);
	$('.isSelectAllDocument').show();
	$("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
}

function hideAdvDowloadGrid() {
	if (($(window).width() < MobileScreenSize)) {
		$("#btnAdvDownloadMobile").show();
		$("#btnAdvDownloadDesktop").hide();
	} else {
		$("#btnAdvDownloadDesktop").show();
	}

	$(".grid-action-panel").hide();
	$('.app-main__outer .background-padding').removeClass('download-attachment-margin');
	//Get the column API object
	$('.select-checkbox-all').prop("checked", false);
	$("#AdvDownSelectAll").prop("checked", false);

	var ChkBoxColumn = gridCertificateList.column(0);
	var IconColumn = gridCertificateList.column(1);
	ChkBoxColumn.visible(false);
	IconColumn.visible(true);
	gridCertificateList.column(0).checkboxes.deselectAll();

	$('#AdvDocSelected').text(0);
	$('.isSelectAllDocument').hide();
}

function fetchAttachments(vesselCertificateLogId, gridList) {

	var vesselCertificateLogIds = [];
	var rows_selected = gridList.column(0).checkboxes.selected();

	if (rows_selected.length > 0) {
		$.each(rows_selected, function (index, rowId) {
			vesselCertificateLogIds.push(rowId);
		});
	}
	else {
		vesselCertificateLogIds = vesselCertificateLogId;
	}

	if (vesselCertificateLogIds.length > 0) {
		var certificatesDocMap = new Map();
		$.ajax({
			url: "/Certificate/GetCertificateDocuments",
			type: "POST",
			dataType: "JSON",
			data: {
				"VesselCertificateLogIds": vesselCertificateLogIds
			},
			success: function (data) {
				var jsonArray = data.data;
				for (var i = 0; i < jsonArray.length; i++) {
					if (!jsonArray[i].isWebAddressEditable) {
						certificatesDocMap.set(i, {
							documentName: jsonArray[i].title,
							documentId: jsonArray[i].ettId,
							documentFileName: jsonArray[i].cloudFileName
						});
					}
				}
				DownloadSelectedAttachment(certificatesDocMap, true);
			}
		});
	}

}

function DownloadSelectedAttachment(certificatesDocMap, globalFlag) {

	var fileName = '';
	var nextAttach = 0;
	var totalAttachment = certificatesDocMap.size;

	DownloadNextAttachment();

	function DownloadNextAttachment() {

		var input = {
			"identifier": certificatesDocMap.get(nextAttach).documentId.trim(),
			"fileName": certificatesDocMap.get(nextAttach).documentFileName.trim()
		};
		fileName = certificatesDocMap.get(nextAttach).documentName.trim();

		$.ajax({
			url: "/Certificate/DownloadDocument",
			type: "POST",
			dataType: "JSON",
			global: globalFlag,
			data: {
				"input": JSON.stringify(input)
			},
			success: function (data) {

				if (data.bytes != null) {
					var array = base64ToArrayBuffer(data.bytes);
					saveByteArray(fileName, array, data.fileType);
				} else {
					ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
				}

				nextAttach++;
				// Not implemented in error due to avoid call for multipule time 
				if (totalAttachment > nextAttach) {
					DownloadNextAttachment();
				}
			}
		});


	}

}

function CertificateDocumentsPopover(uniqueClsSel, VesselCertificateLogIds) {
	var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
		'<div class="loader  mx-auto">' +
		'<div class="ball-clip-rotate">' +
		'<div></div>' +
		'</div>' +
		'</div>' +
		'</div>';

	$('.' + uniqueClsSel).attr('title', 'Attachments <a href = "#" class= "close-popover cursor-pointer pull-right"><img src="/images/popover-close.png" /></a>');
	$('.' + uniqueClsSel).attr('data-placement', 'bottom');
	$('.' + uniqueClsSel).attr('data-trigger', 'focus');
	$('.' + uniqueClsSel).attr('data-toggle', 'popover');
	$('.' + uniqueClsSel).attr('data-html', true);
	$('.' + uniqueClsSel).attr('data-content', '<div class="elementLoader p-2"></div>');

	$.ajax({
		url: "/Certificate/GetCertificateDocuments",
		type: "POST",
		dataType: "JSON",
		global: false,
		data: {
			"VesselCertificateLogIds": VesselCertificateLogIds
		},
		beforeSend: function (xhr) {
			$('.' + uniqueClsSel).popover('show');
			$(".elementLoader").block({
				message: $(" " + loadercontent),
			});
		},
		success: function (data) {
			var jsonArray = data.data;
			var attachCount = jsonArray.length;
			if (attachCount > 1 || (attachCount == 1 && jsonArray[0].isWebAddressEditable == true)) {
				var html_content = "<div class='elementLoader scroller'><table class='table table-condensed table-borderless mb-0'><tbody>";
				for (var i = 0; i < attachCount; i++) {
					var iconPath = jsonArray[i].isWebAddressEditable == true ? '/images/Download-doc-inactive.png' : '/images/Download-doc-active.png';
					html_content += "<tr>";
					html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='" + iconPath + "' class='m-0' width='18' title='Download'/>";
					html_content += "<span class='documentName' > " + jsonArray[i].title + " </span >";
					html_content += "<span class='documentId d-none'> " + jsonArray[i].ettId + " </span >";
					html_content += "<span class='webAddress d-none'> " + jsonArray[i].webAddress + " </span >";
					html_content += "<span class='isWebAddressEditable d-none'> " + jsonArray[i].isWebAddressEditable + " </span >";
					html_content += "<span class='documentfileName d-none' > " + jsonArray[i].cloudFileName + " </span ></a></td > ";
					html_content += "<td class='data-datetime-align'> " + GetFormattedOnlyDate(jsonArray[i].createdOn) + "</td>";
					html_content += "</tr>";
				}
				html_content += "</tbody></table></div>";
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('title', 'Attachments (' + attachCount + ') <a href="#" class="close-popover cursor-pointer pull-right"><img src="/images/popover-close.png" /></a>');
				$('.' + uniqueClsSel).attr('data-content', html_content);
				$('.' + uniqueClsSel).attr('data-placement', 'bottom');
				$('.' + uniqueClsSel).attr('data-trigger', 'focus');
				$('.' + uniqueClsSel).attr('data-toggle', 'popover');
				$('.' + uniqueClsSel).attr('data-html', true);
				//$('.' + uniqueClsSel).attr('data-container', ''); popover({ 'container', ".modal" });
				$('.' + uniqueClsSel).popover('show');
				$('.' + uniqueClsSel).removeAttr('title');
				//$('.' + uniqueClsSel).popover({ container: ".renewal-history-doc-popup" });

			}
			else if (attachCount == 1) {
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('data-content', '');
				var certificatesDocMap = new Map();
				certificatesDocMap.set(0, {
					documentName: jsonArray[0].title,
					documentId: jsonArray[0].ettId,
					documentFileName: jsonArray[0].cloudFileName
				});
				DownloadSelectedAttachment(certificatesDocMap, false);
			}
		},
		complete: function () {
			$(".elementLoader").unblock();
		},
	});
}

function GetPMSCertificates() {

	$.ajax({
		url: "/Certificate/GetPMSCertificates",
		type: "POST",
		dataType: "JSON",
		data: {
			"vesselId": $('#VesselId').val()
		},
		success: function (data) {
			PMSCertificateList = data.data;
		},
		complete: function () {
			configExportToExcel();
		}
	});
}

function configExportToExcel() {

	var searchValue = gridCertificateList.search();
	gridCertificateList.search("").draw();
	hideAdvDowloadGrid();

	gridCertificateList.columns([columnIndex_Doc]).visible(false);
	gridCertificateList.columns([columnIndex_DiscussionNotes]).visible(false);
	gridCertificateList.columns([columnIndex_DiscussionChat]).visible(false);

	$('#dtCertificateList.cardview thead').addClass("export-grid-show");
	$('#dtCertificateList').DataTable().buttons(0, 2).trigger();
	$('#dtCertificateList.cardview thead').removeClass("export-grid-show");

	gridCertificateList.columns([columnIndex_Doc]).visible(true);
	gridCertificateList.columns([columnIndex_DiscussionNotes]).visible(true);
	gridCertificateList.columns([columnIndex_DiscussionChat]).visible(true);
	
	gridCertificateList.search(searchValue).draw();
}

function getHeaderNames(table) {
	// Gets header names.
	//params:
	//  table: table ID.
	//Returns:
	//  Array of column header names.

	var names = ["CERTIFICATE NAME", "VALIDITY", "ISSUE DATE", "EXPIRY DATE"];

	return names;
}

function buildCols(data) {
	// Builds cols XML.
	//To do: deifne widths for each column.
	//Params:
	//  data: row data.
	//Returns:
	//  String of XML formatted column widths.

	var cols = '<cols>';
	var colNum = 1;
	cols += '<col min="' + colNum + '" max="' + colNum + '" width="50" customWidth="1"/>';
	for (var i = 1; i < data.length; i++) {
		colNum = i + 1;
		cols += '<col min="' + colNum + '" max="' + colNum + '" width="20" customWidth="1"/>';
	}

	cols += '</cols>';

	return cols;
}

function buildRow(data, rowNum, styleNum, isbody) {
	// Builds row XML.
	//Params:
	//  data: Row data.
	//  rowNum: Excel row number.
	//  styleNum: style number or empty string for no style.
	//Returns:
	//  String of XML formatted row.

	var style = styleNum ? ' s="' + styleNum + '"' : '';
	var colItrate = 10;
	var row = '<row r="' + rowNum + '">';
	var colNum;

	if (isbody == false) {
		for (var i = 0; i < data.length; i++) {
			colNum = (i + colItrate).toString(36).toUpperCase();  // Convert to alpha
			var cr = colNum + rowNum;
			row += '<c t="inlineStr" r="' + cr + '"' + style + '>' +
				'<is>' +
				'<t>' + data[i] + '</t>' +
				'</is>' +
				'</c>';
		}
	}
	else {
		colNum = (colItrate++).toString(36).toUpperCase();  // Convert to alpha
		var cr = colNum + rowNum;
		row += '<c t="inlineStr" r="' + cr + '"' + style + '>' +
			'<is>' +
			'<t>' + data['jobName'] + '</t>' +
			'</is>' +
			'</c>';

		colNum = (colItrate++).toString(36).toUpperCase();  // Convert to alpha
		cr = colNum + rowNum;
		row += '<c t="inlineStr" r="' + cr + '"' + style + '>' +
			'<is>' +
			'<t>' + data['interval'] + '</t>' +
			'</is>' +
			'</c>';

		colNum = (colItrate++).toString(36).toUpperCase();  // Convert to alpha
		cr = colNum + rowNum;
		row += '<c t="inlineStr" r="' + cr + '"' + style + '>' +
			'<is>' +
			'<t>' + GetFormattedOnlyDate(data['dueDate']) + '</t>' +
			'</is>' +
			'</c>';

		colNum = (colItrate++).toString(36).toUpperCase();  // Convert to alpha
		cr = colNum + rowNum;
		row += '<c t="inlineStr" r="' + cr + '"' + style + '>' +
			'<is>' +
			'<t>' + GetFormattedOnlyDate(data['lastCompletedDate']) + '</t>' +
			'</is>' +
			'</c>';
	}

	row += '</row>';

	return row;
}

function getTableData(table, title) {
	// Processes Datatable row data to build sheet.
	//Params:
	//  table: table ID.
	//  title: Title displayed at top of SS or empty str for no title.
	//Returns:
	//  String of XML formatted worksheet.

	var header = getHeaderNames();
	var table = PMSCertificateList;   //$(table).DataTable();
	var rowNum = 1;
	var mergeCells = '';
	var ws = '';

	ws += buildCols(header);
	ws += '<sheetData>';

	if (title.length > 0) {
		ws += buildRow([title], rowNum, 51, false);
		rowNum++;

		var mergeCol = ((header.length - 1) + 10).toString(36).toUpperCase();

		mergeCells = '<mergeCells count="1">' +
			'<mergeCell ref="A1:' + mergeCol + '1"/>' +
			'</mergeCells>';
	}

	var topHeadingDetails = 'Vessel : ' + $('#VesselName').val() + '\nDate : ' + moment(new Date()).format("D MMM YYYY");

	ws += buildRow([topHeadingDetails], rowNum, 51, false);
	rowNum++;
	var mergeCol = ((header.length - 1) + 10).toString(36).toUpperCase();

	mergeCells += '<mergeCells count="1">' +
		'<mergeCell ref="A1:' + mergeCol + '1"/>' +
		'</mergeCells>';

	//Header
	ws += buildRow(header, rowNum, 2, false);
	rowNum++;

	$.each(table, function (index, jsonObject) {
		var data = jsonObject;
		ws += buildRow(data, rowNum, '', true);
		rowNum++;
	});

	ws += '</sheetData>' + mergeCells;

	return ws;
}

function setSheetName(xlsx, name) {
	// Changes tab title for sheet.
	//Params:
	//  xlsx: xlxs worksheet object.
	//  name: name for sheet.

	if (name.length > 0) {
		var source = xlsx.xl['workbook.xml'].getElementsByTagName('sheet')[0];
		source.setAttribute('name', name);
	}
}

function addSheet(xlsx, table, title, name, sheetId) {
	console.log("sheetId", sheetId);
	//Clones sheet from Sheet1 to build new sheet.
	//Params:
	//  xlsx: xlsx object.
	//  table: table ID.
	//  title: Title for top row or blank if no title.
	//  name: Name of new sheet.
	//  sheetId: string containing sheetId for new sheet.
	//Returns:
	//  Updated sheet object.

	//Add sheet2 to [Content_Types].xml => <Types>
	//============================================
	var source = xlsx['[Content_Types].xml'].getElementsByTagName('Override')[1];
	var clone = source.cloneNode(true);
	clone.setAttribute('PartName', '/xl/worksheets/sheet' + sheetId + '.xml');
	xlsx['[Content_Types].xml'].getElementsByTagName('Types')[0].appendChild(clone);

	//Add sheet relationship to xl/_rels/workbook.xml.rels => Relationships
	//=====================================================================
	var source = xlsx.xl._rels['workbook.xml.rels'].getElementsByTagName('Relationship')[0];
	var clone = source.cloneNode(true);
	clone.setAttribute('Id', 'rId' + sheetId + 1);
	clone.setAttribute('Target', 'worksheets/sheet' + sheetId + '.xml');
	xlsx.xl._rels['workbook.xml.rels'].getElementsByTagName('Relationships')[0].appendChild(clone);

	//Add second sheet to xl/workbook.xml => <workbook><sheets>
	//=========================================================
	var source = xlsx.xl['workbook.xml'].getElementsByTagName('sheet')[0];
	var clone = source.cloneNode(true);
	clone.setAttribute('name', name);
	clone.setAttribute('sheetId', sheetId);
	clone.setAttribute('r:id', 'rId' + sheetId + 1);
	xlsx.xl['workbook.xml'].getElementsByTagName('sheets')[0].appendChild(clone);

	//Add sheet2.xml to xl/worksheets
	//===============================
	var newSheet = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' +
		'<worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:x14ac="http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac" mc:Ignorable="x14ac">' +
		getTableData(table, title) +

		'</worksheet>';

	if ($('#chckIncludePMSCertificate').is(':checked') && !$('#chckIncludeVesselCertificate').is(':checked')) {
		xlsx.xl.worksheets['sheet1.xml'] = $.parseXML(newSheet);
	}
	else {
		xlsx.xl.worksheets['sheet' + sheetId + '.xml'] = $.parseXML(newSheet);
	}
}

$(document).on("change", ".checkbox-ExportExcel", function () {

	var len = $("#divChkboxExportExcel input[name='checkboxExport']:checked").length;

	if (len > 0) {
		$(".btnExportExcel").removeClass('disabled').removeAttr('aria-disabled');
	}
	else {
		$(".btnExportExcel").addClass('disabled').attr('aria-disabled', 'true');
	}
});

function BindRequisitionLinkDetails(data) {

	$('#certifcateNameModelReq').text("-");
	$('#velidityModelReq').text("-");
	$('#issuedDateModelReq').text("-");
	$('#expiryDateModelReq').text("-");

	if (data.validity != null && data.validity != "") {
		$('#velidityModelReq').text(data.validity);
	}
	if (data.dateFrom != null && data.dateFrom != "") {
		$('#issuedDateModelReq').text(GetFormattedOnlyDate(data.dateFrom));
	}
	if (data.dateTo != null && data.dateTo != "") {
		$('#expiryDateModelReq').text(GetFormattedOnlyDate(data.dateTo));
	}
	if (data.certificateFullName != null && data.certificateFullName != "") {
		$('#certifcateNameModelReq').text(data.certificateFullName);
	}

	$.ajax({
		url: "/Certificate/GetRequisitionMappedOrders",
		type: "POST",
		dataType: "JSON",
		data: {
			"vesselId": $('#VesselId').val(),
			"vesselCertificateLogId": data.vesselCertificateLogId
		},
		complete: function () {

		},
		success: function (data) {
			$('#dtRequisition').DataTable().destroy();
			gridRequisitionOrderList = $('#dtRequisition').DataTable({
				"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-1 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
				"processing": false,
				"serverSide": false,
				"lengthChange": true,
				"searching": false,
				"info": true,
				"scrollY": "275px",
				"scrollCollapse": true,
				"autoWidth": false,
				"paging": false,
				"pageLength": 10,
				"order": [[1, "asc"]],
				"language": {
					"emptyTable": "No requisition available.",
					"loadingRecords": "&nbsp;"
				},
				"data": data.data,
				"columns": [
					{
						className: "data-text-align",
						data: "orderNumber",
						width: "30px",
						render: function (data, type, full, meta) {
							console.log(full);
							var OrderNo = full.coyId + " - " + full.orderNumber;
							var OrderDetailsLink = '<a href = "/PurchaseOrder/Detail/?PurchaseOrderRequest=' + full.purchaseOrderUrl + '&VesselId=' + $('#VesselId').val() + '"> ' + OrderNo + '</a >';
							return OrderDetailsLink; //GetCellData('Order No.', OrderDetailsLink);
						}
					},
					{
						className: "data-text-align tdblock",
						data: "orderName",
						width: "45px",
						render: function (data, type, full, meta) { return GetCellData('Name', data); }
					},
					{
						className: "data-icon-align tdblock",
						data: "purchaseOrderStatus",
						width: "20px",
						render: function (data, type, full, meta) {
							var status = "";
							if (full.isStatusVisible) {
								if (full.orderStatusColor == "Good") {
									status = '<span class="badge badge-pill purchase-order-status-badge badge-success" data-placement="bottom" data-toggle="tooltip" title="' + full.statusDescription + '">' + full.purchaseOrderStatus + '</span>';
								}
								else if (full.orderStatusColor == "PreWarning") {
									status = '<span class="badge badge-pill purchase-order-status-badge BrushOrange" data-placement="bottom" data-toggle="tooltip" title="' + full.statusDescription + '">' + full.purchaseOrderStatus + '</span>';
								}
								else if (full.orderStatusColor == "Critical") {
									status = '<span class="badge badge-pill purchase-order-status-badge BrushRed" data-placement="bottom" data-toggle="tooltip" title="' + full.statusDescription + '">' + full.purchaseOrderStatus + '</span>';
								}
							}
							return GetCellData('Status', status);
						}
					},
					{
						className: "data-datetime-align",
						data: "requestedDate",
						width: "25px",
						render: function (data, type, full, meta) {
							var formattedDate = GetFormattedOnlyDate(data);
							return GetCellData('Requested Date', formattedDate);
						}
					},
					{
						className: "data-datetime-align",
						data: "orderDate",
						width: "20px",
						render: function (data, type, full, meta) {
							var formattedDate = GetFormattedOnlyDate(data);
							return GetCellData('Ordered Date', formattedDate);
						}
					},
					{
						className: "data-text-align tdblock",
						data: "expectedDeliveryPort",
						width: "25px",
						render: function (data, type, full, meta) {
							return GetCellData('Expected Port', data);
						}
					},
					{
						className: "data-datetime-align",
						data: "expectDeliveryDate",
						width: "25px",
						render: function (data, type, full, meta) {
							var formattedDate = GetFormattedOnlyDate(data);
							return GetCellData('Expected Delivered Date', formattedDate);
						}
					},
					{
						className: "data-datetime-align",
						data: "orderReceivedDate",
						width: "25px",
						render: function (data, type, full, meta) {
							var formattedDate = GetFormattedOnlyDate(data);
							return GetCellData('Received <br/> Date', formattedDate);
						}
					},

				]
			});
		}
	});
	$('#dtRequisition').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip({
			trigger: 'hover'
		})
	});
}

function SetPageParameter() {
	let includeWindow = false;
	if ($('#chckIncludeWindow').is(':checked')) {
		includeWindow = true;
	}
	var input = {
		"vesselId": $('#VesselId').val(),
		"vesselName": $('#VesselName').val(),
		"CertificateImpact": $('input[name="selImpact"]:checked').val(),
		"CertificateStatus": $('input[name="selStatus"]:checked').val(),
		"CertificateType": $('input[name="selType"]:checked').val(),
		"includeWindow": includeWindow,
		"ToDate": selectedEndDate,
		"FromDate": selectedStartDate,
		"SearchKeyword": $("#searchInput").val(),
		"stageName": $('#StageName').val()
	};

	$.ajax({
		url: "/Certificate/SetPageParameter",
		type: "POST",
		data: input,
		success: function (data) {
			if (data != null) {
				SetHiddenFields(data.data);
				GetCertificateList(data.data);
			}
		}
	});
}

function SetHiddenFields(response) {
	$('#VesselId').val(response.vesselId);
	$('#vesselName').val(response.vesselName);
	$("#CertificateImpact").val(response.certificateImpact);
	$("#CertificateType").val(response.certificateType);
	$("#CertificateStatus").val(response.certificateStatus);
	$("#FromDate").val(response.fromDate);
	$("#ToDate").val(response.toDate);
	$("#ActiveMobileTabClass").val(response.activeMobileTabClass);
	$('#StageName').val(response.stageName);
	$("#searchInput").val(response.searchKeyword);
	KeywordFilterCountSet(response.searchKeyword);
	if (response.includeWindow == true) {
		$("#chckIncludeWindow").prop('checked', true);
	}
	else {
		$("#chckIncludeWindow").prop('checked', false);
	}
	ExpiryFilterCountSet();
}

function CallPlannedMaintenance() {
	var input = {
		"VesselId": $('#VesselId').val()
	}
	$.ajax({
		url: "/Certificate/LoadPlannedMaintenance",
		type: "POST",
		"data": input,
		success: function (data) {
			window.location.href = data;
		}
	});
}

function AppendGridTitle(subtitle) {

	$("#tableSubTitle").hide();

	if (isFilterExist()) {
		$("#divfilterhide").removeClass('btn-dark-grey');
		$("#divfilterhide").addClass('btn-info');
	}
	else {
		$("#divfilterhide").removeClass('btn-info');
		$("#divfilterhide").addClass('btn-dark-grey');
	}

	if (!IsNullOrEmptyOrUndefined(subtitle)) {
		$("#tableSubTitle").text(" - " + subtitle);
		$("#tableSubTitle").show();
	}
}

function isFilterExist() {
	var SearchInput = $("#searchInput").val();
	var CertificateImpact = $('input[name="selImpact"]:checked').val();
	var CertificateStatus = $('input[name="selStatus"]:checked').val();
	var CertificateType = $('input[name="selType"]:checked').val();
	var dateExpiringBtwn = $('#dateExpiringBtwn').val();
	var IncludeWindow = $('#chckIncludeWindow').is(':checked');

	return (
		(!IsNullOrEmptyOrUndefined(dateExpiringBtwn) && !(dateExpiringBtwn === DateRangePickerLabelText))
		|| IncludeWindow || !IsNullOrEmptyOrUndefined(SearchInput)
		|| !IsNullOrEmptyOrUndefined(CertificateType)
		|| (!IsNullOrEmptyOrUndefined(CertificateImpact) && !(CertificateImpact === 'All'))
		|| (!IsNullOrEmptyOrUndefined(CertificateStatus) && !(CertificateStatus === 'All'))
	)
}


function SetAppliedFilterAndCount() {

	var SearchInput = $("#searchInput").val();
	var CertificateStatus = $('input[name="selStatus"]:checked').val();
	var CertificateType = $('input[name="selType"]:checked').val();
	var dateExpiringBtwn = $('#dateExpiringBtwn').val();
	var IncludeWindow = $('#chckIncludeWindow').is(':checked');
	var filterCount = 0;

	if (!IsNullOrEmptyOrUndefined(SearchInput)) {
		$('#filterCertificateName').text(SearchInput);
		$("#filterCard1").show();
		filterCount++;
	}
	else {
		$('#filterCertificateName').text('-');
		$("#filterCard1").hide();
	}

	if (!IsNullOrEmptyOrUndefined(CertificateStatus) && !(CertificateStatus === 'All')) {
		$('#filterStatus').text(CertificateStatus);
		$("#filterCard2").show();
		filterCount++;
	}
	else {
		$('#filterStatus').text('-');
		$("#filterCard2").hide();
	}

	if (!IsNullOrEmptyOrUndefined(CertificateType)) {
		$('#filterType').text(CertificateType);
		$("#filterCard3").show();
		filterCount++;
	}
	else {
		$('#filterType').text('-');
		$("#filterCard3").hide();
	}

	var impactVal = $('input[name="selImpact"]:checked').data("description");
	if (!IsNullOrEmptyOrUndefined(impactVal) && !(impactVal === 'All')) {
		
		$('#filterImpact').text(impactVal);
		$("#filterCard4").show();
		filterCount++;
	}
	else {
		$('#filterImpact').text('-');
		$("#filterCard4").hide();
	}

	if (!IsNullOrEmptyOrUndefined(dateExpiringBtwn) && !(dateExpiringBtwn === DateRangePickerLabelText )) {
		$('#filterExpiry').text(dateExpiringBtwn);
		$("#filterExpiryRow").show();
		filterCount++;
	}
	else {
		$('#filterExpiry').text('Select Date Range');
		$("#filterExpiryRow").hide();
	}

	if (IncludeWindow) {
		$('#filterIncWindow').text('Yes');
		$("#filterIncWindowRow").show();
		filterCount++;
	}
	else {
		$('#filterIncWindow').text('No');
		$("#filterIncWindowRow").hide();
	}

	if ((!IsNullOrEmptyOrUndefined(dateExpiringBtwn) && !(dateExpiringBtwn === DateRangePickerLabelText)) || IncludeWindow) {
		$("#filterCard5").show();
	}
	else {
		$("#filterCard5").hide();
	}

	if (filterCount > 0) {
		$(".filter-design, .clear-filter").show();
	}
	else {
		$(".filter-design, .clear-filter").hide();
	}

	$('#appliedFilterCount').text(filterCount);
}

function SetFilterData() {

	var $selRadioStatus = $('input[type=radio][name=selStatus]');
	var $valueStatus = $("#CertificateStatus").val();
	setRadioButtonValue($selRadioStatus, $valueStatus, "All");
	StatusFilterCountSet($valueStatus);

	var $selRadioImpac = $('input[type=radio][name=selImpact]');
	var $valueImpac = $("#CertificateImpact").val();
	setRadioButtonValue($selRadioImpac, $valueImpac, "All");
	ImpactFilterCountSet($valueImpac);

	var $selRadioType = $('input[type=radio][name=selType]');
	var $valueType = $("#CertificateType").val();
	setRadioButtonValue($selRadioType, $valueType, "");
	TypeFilterCountSet($valueType);

	$('#dateExpiringBtwn').val(DateRangePickerLabelText);
	
	if ($('#dateExpiringBtwn').val() == "" || $('#dateExpiringBtwn').val() == DateRangePickerLabelText || $('#dateExpiringBtwn').val() == undefined) {
		var fromDate = $('#FromDate').val();
		var toDate = $('#ToDate').val();
		if (fromDate != "" && toDate != "") {
			let start = moment($('#FromDate').val(), 'DD-MM-YYYY');
			let end = moment($('#ToDate').val(), 'DD-MM-YYYY');
			selectedStartDate = start.format(DateFormat);
			selectedEndDate = end.format(DateFormat);
			$('#dateExpiringBtwn').val(selectedStartDate + ' - ' + selectedEndDate);
		}
	}

	ExpiryFilterCountSet();
}

function setRadioButtonValue($selRadio, $value,$default) {

	if (!IsNullOrEmptyOrUndefined($value)) {
		$selRadio.filter('[value=' + $value + ']').prop('checked', true);
	}
	else {
		if (!IsNullOrEmptyOrUndefined($default)) {
			$selRadio.filter('[value=' + $default + ']').prop('checked', true);
		}
		else {
			$selRadio.filter('[value=""]').prop('checked', true);
		}
	}
}

function KeywordFilterCountSet(Data) {
	if (!IsNullOrEmptyOrUndefined(Data)) {
		FilterCountSet(1, "#keywordFilterCount");
	}
	else {
		FilterCountSet(0, "#keywordFilterCount");
	}
}

function StatusFilterCountSet(Data) {
	if (!IsNullOrEmptyOrUndefined(Data) && Data != "All") {
		FilterCountSet(1, "#statusFilterCount");
	}
	else {
		FilterCountSet(0, "#statusFilterCount");
	}
}

function ImpactFilterCountSet(Data) {
	if (!IsNullOrEmptyOrUndefined(Data) && Data != "All") {
		FilterCountSet(1, "#impactFilterCount");
	}
	else {
		FilterCountSet(0, "#impactFilterCount");
	}
}

function TypeFilterCountSet(Data) {
	if (!IsNullOrEmptyOrUndefined(Data) && Data != " ") {
		FilterCountSet(1, "#typeFilterCount");
	}
	else {
		FilterCountSet(0, "#typeFilterCount");
	}
}

function ExpiryFilterCountSet() {

	var count = 0;
	var Data = $("#dateExpiringBtwn").val();
	count = $('#chckIncludeWindow').is(':checked')? 1 : 0;
	if (!IsNullOrEmptyOrUndefined(Data) && Data != DateRangePickerLabelText) {
		count = count + 1;
	}

	FilterCountSet(count, "#expiryFilterCount");
}

function FilterCountSet(nodeLength, elementCount) {
	$(elementCount).text(nodeLength);
	if (nodeLength > 0)
		AddClassIfAbsent($(elementCount).parent('div'), 'active');
	else
		RemoveClassIfPresent($(elementCount).parent('div'), 'active');
}