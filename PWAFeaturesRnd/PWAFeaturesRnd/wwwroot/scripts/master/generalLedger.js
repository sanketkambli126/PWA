import "bootstrap";
import moment from "moment";
import "select2/dist/js/select2.full.js";

import toastr from "toastr";
import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader } from "../common/datatablefunctions.js"
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, base64ToArrayBuffer, saveByteArray, MobileTab_Overview, MobileTab_List, Mobile_Tabs, createFlatRadioButton, BackButton, IsNullOrEmptyOrUndefined, RemoveClassIfPresent, AddClassIfAbsent, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js"

import { DateRangePickerCancelText, DateRangePickerLabelText, Tab1, Tab2, MobileScreenSize, FinanceGeneralLedgerPageKey } from "../common/constants.js"
import { GetSelectedLookUpDetails, SetSelectedLookUp, ClearSelectedLookUp } from "../master/lookup/accountcodeLookUp"

var IsMobile = false;
var generalLedgeList;
const DateFormat = "DD MMM YYYY";
var selectedStartDate;
var selectedEndDate;
var financialPeriodsArray = new Map();
var generalLedgerFilterCount = 0;

$(document).on('click', '#aRemoveFilter', function () {
	clearForm();
});

$(document).on('change', 'input[type=radio][name=financialYears]', function () {
	updateFinancialLedgerFilterCount();
});

$('#dateFinancePeriod').on('input', function () {
	updateFinancialLedgerFilterCount();
});

function updateFinancialLedgerFilterCount() {
	let count = 0;
	if ($('input[type=radio][name=financialYears]').is(":checked")) {
		count++;
	}
	let dateFinancePeriod = $("#dateFinancePeriod").val();
	if (!IsNullOrEmptyOrUndefined(dateFinancePeriod)) {
		count++;
	}
	FilterCountSet(count, "#financialPeriodFilterCount");
}

$(document).on('click', '#divAccountSearch .rowTemplate', function () {
	FilterCountSet(1, "#accountSearchFilterCount");
});

$(document).on('click', '#divAccountSearch .removeSelection', function () {
	FilterCountSet(0, "#accountSearchFilterCount");
});


$(document).ready(function () {
	AjaxError();
	BackButton(FinanceGeneralLedgerPageKey,false);

	if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
		IsMobile = true;
	} else {
		IsMobile = false;
	}
	RegisterTabSelectionEvent('.mobileTabClick', FinanceGeneralLedgerPageKey);
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
	}
	selectedStartDate = $('#FromDate').val();
	selectedEndDate = $('#ToDate').val();

	AddLoadingIndicator();
	RemoveLoadingIndicator();

	//AccounntCode
	SetSelectedLookUp($('#AccountName').val(), $('#AccountId').val());

	initialSetFinancialYearDate();

	$('#btnSearch').click(function () {
		SetPageParameter(true);
	});

	$('#btnClear').click(function () {
		clearForm();
	});

	GetFinancialYears();
	BindLedgerSummary();
	GetGeneralLedgerList();

	$("#btnExport").on('click', function () {
		ExportToExcel();
	});
	
	
	$('#filterdata').on('show.bs.modal', function (e) {
		if ($('#filterFinancialPeriodRow').css('display') == 'none'
			&& $('#filterDateRangeRow').css('display') == 'none')
		{
			$("#filterCard1").hide();
		}
		else {
			$("#filterCard1").show();
		}
	});
});

function ExportToExcel() {
	var BaseCoyCurr = $('#BaseCoyCurr').val();

	var input = GetRequest(BaseCoyCurr);

	input["AccountName"] = $('#AccountName').val();

	$.ajax({
		url: "/Finance/ExportToExcelGeneralLedger",
		type: "POST",
		xhrFields: {
			responseType: 'blob'
		},
		"data": {
			"input": input
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
}

function GetRequest(BaseCoyCurr) {
	return {
		"AccountCode": $('#AccountId').val(),
		"VesselId": $('#VesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate,
		"BaseCoyCurr": BaseCoyCurr,
		"AccountingCompanyChhId": $('#ChhId').val(),
		"FinancialYearStartDate": $('#FromDate').val()
	};
}

function GetGeneralLedgerList() {

	var BaseCoyCurr = $('#BaseCoyCurr').val();

	var input = GetRequest(BaseCoyCurr);

	$('#dtGeneralLedgerGrid').DataTable().destroy();
	generalLedgeList = $('#dtGeneralLedgerGrid').DataTable({
		//"dom": '<<"row mb-3"<"col-12 col-md-6 offset-md-0 col-lg-2 offset-lg-2 col-xl-2 offset-xl-2 dt-infomation"i><"col-md-3 col-lg-4 col-xl-4 filters-data"><"col-12 col-md-6 col-lg-4 col-xl-4"f>><rt><"clearfix"<"float-left"l><""p>>>',
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
			"emptyTable": "No account balance information available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Finance/GetAccountBalanceForAccCompany",
			"type": "POST",
			"data": input,
			"datatype": "json"
		},
		"columns": [
			{
				className: "d-none d-sm-table-cell data-text-align",
				data: "accountCode",
				name: "AccountCode",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetCellData('Account Code', data);
				}
			},
			{
				className: "width-85 data-text-align",
				data: "description",
				name: "Description",
				width: "300px",
				render: function (data, type, full, meta) {
					return '<a href = "/Finance/GeneralLedgerTransaction/?transactionURL=' + full.transactionURL + '&VesselId=' + $('#VesselId').val() + '"> <span class="d-inline-block d-sm-none"> ' + full.accountCode + ' - </span>  <span>' + data + '</span></a>';
				}
			},
			{
				className: "data-text-align tdblock",
				data: "accountType",
				name: "AccountType",
				width: "30px",
				render: function (data, type, full, meta) {
					return GetCellData('Account Type', data);
				}
			},
			{
				className: "data-number-align",
				data: "auxiliaries",
				name: "Auxiliaries",
				width: "15px",
				render: function (data, type, full, meta) {
					return GetCellData('AUX', data);
				}
			},
			{
				className: "data-text-align",
				data: "currencyType",
				name: "CurrencyType",
				width: "15px",
				render: function (data, type, full, meta) {
					return GetCellData('CUR', data);
				}
			},
			{
				className: "data-number-align",
				data: "originalBalance",
				name: "OriginalBalance",
				width: "40px",
				render: function (data, type, full, meta) {
					var originalBalance = "";

					if (parseFloat(data.replace(/,/g, ''), 10) < 0) {
						originalBalance = '<span  class="txt-red" >' + data + '</span>';
					}
					else {
						originalBalance = '<span class="">' + data + '</span>';
					}

					return GetCellData('Original <br/> Balance', originalBalance);
				}
			},

			{
				className: "data-number-align",
				data: "baseBalanceUSD",
				name: "BaseBalanceUSD",
				width: "40px",
				render: function (data, type, full, meta) {
					var baseBalanceUSD = "";
					if (parseFloat(data.replace(/,/g, ''), 10) < 0) {
						baseBalanceUSD = '<span class="txt-red">' + data + '</span>';
					}
					else {
						baseBalanceUSD = '<span class="">' + data + '</span>';
					}

					return GetCellData('Functional <br/> Balance (' + BaseCoyCurr + ')', baseBalanceUSD);
				}
			},
		],
		"initComplete": function (settings, data) {
			BindAppliedFilter();
		}
		
	});
	//$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata"  class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');

}

function NumberToString(number, toFixedValue, state) {
	var numToString = "";
	if (number != null && number != '' && number != 'undefined') {
		numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
			{
				minimumFractionDigits: 2
			});
	}
	else {
		if (state == 1) {
			//when expected return is 0.00
			numToString = "0.00";
		}
		else if (state == 2) {
			//when blank space is expected
			numToString = "";
		}
	}
	return numToString;
}

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

function GetFinancialYears() {

	$.ajax({
		url: "/Finance/GetFinancialYears",
		type: "GET",
		dataType: "JSON",
		data: {
			"accountingCompanyId": $("#CoyId").val()
		},
		success: function (data) {
			$("#FinancialYearRadiosContainer").html("");
			var response = data.accountingFinancialYears;

			for (let i = 0; i < response.length; i++) {
				let opt = response[i];
				let financialYearRadio = createFlatRadioButton("financialYears" + i, "financialYears", opt.period, opt.dateRange);
				$("#FinancialYearRadiosContainer").append(financialYearRadio);

				let financialYearDateRange = { StartDate: opt.startDate, EndDate: opt.endDate }
				financialPeriodsArray.set(opt.period, financialYearDateRange);
			}

			let financialYearRadio = createFlatRadioButton("financialYears00", "financialYears", '0', 'Custom');
			$("#FinancialYearRadiosContainer").append(financialYearRadio);

			var localStartDate = moment($("#MinStartDate").val(), 'DD-MM-YYYY');
			var localEndDate = moment($("#MaxEndDate").val(), 'DD-MM-YYYY');

			let financialYearDateRange = { StartDate: localStartDate, EndDate: localEndDate }
			financialPeriodsArray.set(0, financialYearDateRange);

		},
		complete: function () {
			$('input[type=radio][name=financialYears]').on('change', function () {
				financialPeriodYearChange(this);
			});

			if ($("#finPeriod").val() != "" && $("#finPeriod").val() != undefined) {
				financialPeriodYearChange(null);
			}
		}
	})

};

function DaterangePicker(minDate,maxDate,startDate,endDate,isHideOutofRange) {

	$("#dateFinancePeriod").caleran(
		{
			showButtons: true,
			hideOutOfRange: isHideOutofRange,
			showOn: "right",
			arrowOn: "right",
			startEmpty: true,
			cancelLabel: "Clear",
			format: "DD MMM YYYY",
			minDate: minDate,
			maxDate: maxDate,
			startDate: startDate,
			endDate: endDate,
			showFooter: false,
			onafterselect: function (caleran, startDate, endDate) {
				setDateDetails(startDate, endDate);
			},
			oncancel: function (caleran, start, end) {
				setDateDetailonCancel();
				return true;
			}
		}
	);

}


function setDateDetails(startDate, endDate)
{
	$('#dateFinancePeriod').val(startDate.format(DateFormat) + ' - ' + endDate.format(DateFormat));
	selectedStartDate = startDate.format(DateFormat);
	selectedEndDate = endDate.format(DateFormat);
	validateFinancialDateRange();

	updateFinancialLedgerFilterCount();
}

function setDateDetailonCancel() {
	selectedStartDate = '';
	selectedEndDate = '';
	$('#dateFinancePeriod').val(DateRangePickerLabelText);
	initialSetFinancialYearDate();

	FilterCountSet(1, "#financialPeriodFilterCount");

}


function SetPageParameter(isSearchClicked) {

	var selectedAccountCodeLookup = GetSelectedLookUpDetails();
	$('#AccountId').val(selectedAccountCodeLookup.id);
	$('#AccountName').val(selectedAccountCodeLookup.description);

	var input = {
		"AccountId": $('#AccountId').val(),
		"AccountName": $('#AccountName').val(),
		"VesselId": $('#VesselId').val(),
		"FromDate": selectedStartDate,
		"ToDate": selectedEndDate,
		"BaseCoyCurr": $('#BaseCoyCurr').val(),
		"ChhId": $('#ChhId').val(),
		"FinancialYearStartDate": $('#FromDate').val(),
		"finPeriod" : $("#finPeriod").val()
	}

	$.ajax({
		url: "/Finance/SetGeneralLedgerPageParameter",
		type: "POST",
		data: input,
		success: function (data) {
			GetGeneralLedgerList();
		}
	});
}

function financialPeriodYearChange($this) {
	var selected = 0;
	if ($this == null || $this == undefined) {
		selected = Number($("#finPeriod").val());
		$('input[type=radio][name=financialYears]').prop('checked', false);

		var $selRadio = $('input[type=radio][name=financialYears]');
		if (selected != "" && selected != undefined) {
			$selRadio.filter('[value=' + selected + ']').prop('checked', true);
		}
	}
	else {
		selected = Number($this.value);
	}
	$("#finPeriod").val(selected);

	//Custom radio buttion click
	if (selected == 0) {

		var localStartDate = moment($("#MinStartDate").val(), 'DD-MM-YYYY');
		var localEndDate = moment($("#MaxEndDate").val(), 'DD-MM-YYYY');

		var localFinStartDate = moment($("#CoyFinStartDate").val(), 'DD-MM-YYYY');
		var localFinEndDate = moment($("#CoyFinEndDate").val(), 'DD-MM-YYYY');

		DaterangePicker(localStartDate, localEndDate, localFinStartDate, localFinEndDate, true);

		var formatedFinStartDate = localFinStartDate.format("DD MMM YYYY");
		var formatedFinEndDate = localFinEndDate.format("DD MMM YYYY");

		$('#dateFinancePeriod').val(formatedFinStartDate + ' - ' + formatedFinEndDate);
	}
	else {

		selectedStartDate = financialPeriodsArray.get(selected).StartDate;
		selectedEndDate = financialPeriodsArray.get(selected).EndDate;

		var localStartDate = moment(selectedStartDate);
		var formatedStartDate = localStartDate.format("DD MMM YYYY");

		var localEndDate = moment(selectedEndDate);
		var formatedEndDate = localEndDate.format("DD MMM YYYY");

		DaterangePicker(localStartDate, localEndDate, localStartDate, localEndDate, true);
		$('#dateFinancePeriod').val(formatedStartDate + ' - ' + formatedEndDate);


	}

	$("#spnDateRangeValidationMsg").addClass('d-none');
	$("#btnSearch").prop('disabled', false);
}

function initialSetFinancialYearDate() {

	var startDate = moment($('#FromDate').val(), 'DD-MM-YYYY');
	var endDate = moment($('#ToDate').val(), 'DD-MM-YYYY');

	var formatedStartDate = startDate.format("DD MMM YYYY");
	var formatedEndDate = endDate.format("DD MMM YYYY");

	var localStartDate = moment($("#MinStartDate").val(), 'DD-MM-YYYY');
	var localEndDate = moment($("#MaxEndDate").val(), 'DD-MM-YYYY');


	DaterangePicker(localStartDate, localEndDate, startDate, endDate, false);

	$('#dateFinancePeriod').val(formatedStartDate + ' - ' + formatedEndDate);

	$("#spnDateRangeValidationMsg").addClass('d-none');
	$("#btnSearch").prop('disabled', false);
}

function BindLedgerSummary() {
	$.ajax({
		url: "/Finance/GetSummaryDetails",
		type: "GET",
		dataType: "JSON",
		data: {
			"VesselId": $('#VesselId').val()
		},
		success: function (data) {
			data = data.data;
			//$("#spanType").text(data.type);
			$("#spanOwner").text(data.vmdOwner);
			$("#spanFuncCurr").text(data.baseCurrency);
			$("#spanFinancialYearRange").text(data.financialYearStartDate + " - " + data.financialYearEndDate);
			$("#spanMgmtStartDate").text(data.managementStartDate);
			$("#spanLedgerCutOffDate").text(data.generalLedgerCutOffDate);
		}
	});
}

function validateFinancialDateRange() {
	var stratFinYear = moment(selectedStartDate).format("YYYY");
	var endFinYear = moment(selectedEndDate).format("YYYY");

	var stratFinMonth = moment(selectedStartDate).format("MM");
	var endFinMonth = moment(selectedEndDate).format("MM");

	var stratFinDate = moment(selectedStartDate).format("DD");
	var endFinDate = moment(selectedEndDate).format("DD");

	var finStartDate = $("#finStartDate").val();
	var finEndDate = $("#finEndDate").val();
	var finStartMonth = $("#finStartMonth").val();
	var finEndMonth = $("#finEndMonth").val();

	var finStartfullDate = moment([finStartDate, finStartMonth, stratFinYear], 'DD-MM-YYYY');
	var finEndfullDate = moment([finEndDate, finEndMonth, endFinYear], 'DD-MM-YYYY');

	var dpStartDate = moment([stratFinDate, stratFinMonth, stratFinYear], 'DD-MM-YYYY');
	var dpEndDate = moment([endFinDate, endFinMonth, endFinYear], 'DD-MM-YYYY');

	var localFinStartDate = moment($("#CoyFinStartDate").val(), 'DD-MM-YYYY');
	var localFinEndDate = moment($("#CoyFinEndDate").val(), 'DD-MM-YYYY');
	let isStartDateInFinYear = moment(dpStartDate).isBetween(localFinStartDate, localFinEndDate);

	if (isStartDateInFinYear) {
		if (!$("#spnDateRangeValidationMsg").hasClass('d-none')) {
			$("#spnDateRangeValidationMsg").addClass('d-none');
		}
		$("#btnSearch").prop('disabled', false);
	}
	else if (finEndfullDate.diff(finStartfullDate, 'days') > 365) {
		$("#spnDateRangeValidationMsg").removeClass('d-none');
		$("#btnSearch").prop('disabled', true);
	}
	else {
		if ((moment(finStartfullDate).isSameOrBefore(dpStartDate)) && (moment(dpEndDate).isSameOrBefore(finEndfullDate))) {
			$("#spnDateRangeValidationMsg").addClass('d-none');
			$("#btnSearch").prop('disabled', false);
		}
		else {
			$("#spnDateRangeValidationMsg").removeClass('d-none');
			$("#btnSearch").prop('disabled', true);
		}
	}
}

function BindAppliedFilter() {
	generalLedgerFilterCount = 0;
	var fyPeriodVal = $('input[name="financialYears"]:checked').data("description");
	AppendTextFilterDataInModel(fyPeriodVal, "#filterFinancialPeriod", "#filterFinancialPeriodRow");
	
	var dateFinancePeriodVal = $("#dateFinancePeriod").val();
	dateFinancePeriodVal = dateFinancePeriodVal == DateRangePickerLabelText ? null : dateFinancePeriodVal;
	AppendTextFilterDataInModel(dateFinancePeriodVal, "#filterDateRange", "#filterDateRangeRow");
	
	var searchLookUpVal = $('#AccountId').val() + " - " + $('#AccountName').val();
	searchLookUpVal = searchLookUpVal === " - " ? null : searchLookUpVal; 
	AppendTextFilterDataInModel(searchLookUpVal, "#filterAccountSearch", "#filterCard2");
	
	updateFinancialLedgerFilterCount();

	if (!IsNullOrEmptyOrUndefined(searchLookUpVal) && searchLookUpVal != " - " )  {
		FilterCountSet(1, "#accountSearchFilterCount");
	}
	else {
		FilterCountSet(0, "#accountSearchFilterCount");
	}
}

function AppendTextFilterDataInModel(filteredValue, filterId, filterCardId) {

	if (!IsNullOrEmptyOrUndefined(filteredValue) && filteredValue !== undefined) {
		generalLedgerFilterCount++;
		$(filterId).text(filteredValue);
		$('#appliedFilterCount').text(generalLedgerFilterCount);
		$(filterCardId).show();
	}
	else {
		$(filterId).text("");
		$(filterCardId).hide();
	}
	hideShowFilterDesign();
}

function hideShowFilterDesign() {
	if (generalLedgerFilterCount > 0) {
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

function clearForm() {
	FilterCountSet(0, ".filtercount");
	selectedStartDate = $('#CoyFinStartDate').val();
	selectedEndDate = $("#CoyFinEndDate").val();
	$('#FromDate').val(selectedStartDate);
	$('#ToDate').val(selectedEndDate);

	$('#AccountId').val("");
	$("#finPeriod").val("0");
	initialSetFinancialYearDate();
	ClearSelectedLookUp();
	$('input[type=radio][name=financialYears]').prop('checked', false);
	SetPageParameter(false);


}

function FilterCountSet(nodeLength, elementCount) {
	$(elementCount).text(nodeLength);
	if (nodeLength > 0)
		AddClassIfAbsent($(elementCount).parent('div'), 'active');
	else
		RemoveClassIfPresent($(elementCount).parent('div'), 'active');
}