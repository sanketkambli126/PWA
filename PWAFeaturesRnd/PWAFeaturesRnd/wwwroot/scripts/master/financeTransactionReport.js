import toastr from "toastr";
import "bootstrap";
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, GetCookie, ToastrAlert, base64ToArrayBuffer, saveByteArray, BackButton, RegisterTabSelectionEvent } from "../common/utilities.js"
import { MobileScreenSize, FinanceTransactionPageKey } from "../common/constants.js"
import { GetFormattedDecimal } from "../common/datatablefunctions.js";

var CurrencyForExportToExcel;
var transactionList;
$(window).on('resize', SetHeaderMargin);
function SetHeaderMargin() {
	$(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));
}

$(document).ready(function () {

	AddLoadingIndicator();
	RemoveLoadingIndicator();
	AjaxError();

	BackButton(FinanceTransactionPageKey, false);

	$('#IsTransactionLevel').val(true);

	RegisterTabSelectionEvent('.mobileTabClick', FinanceTransactionPageKey);
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
	}

	$('#cboMonthList').on('change', function () {
		if ($(this).val() != '0' && $(this).val() != "") {
			$('#SelectedMonth').val($(this).val());
			SetPageParameter();
		}
	});

	$('#cboYearList').on('change', function () {
		if ($(this).val() != '0' && $(this).val() != "") {
			$('#SelectedYear').val($(this).val());
			SetPageParameter();
		}
	});

	$('.breadcrum').html('<a class="txt-blue" href="/Finance/List/?OperationCostRequestUrl=' + $('#PreviousStageUrl').val() + '&VesselId=' + $('#VesselId').val() + '"> << ' + $('#PreviousStageName').val() + '</a>');
	GetHeaderSummary();
	GetOpexTransationList();

	$('#generalledger').click(function () {
		location.href = "/Finance/GeneralLedger/?VesselId=" + $('#VesselId').val();
	});

	//Download Attachment Methods
	$(document).on("click", ".close-popover", function () {
		$('.popover').popover('hide');
		$('body').removeClass('popover-design');
		$('body').removeAttr('class');
	});

	$(document).on('click', 'body', function (e) {
		$('[data-toggle=popover]').each(function () {
			if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
				$(this).popover('hide');
			}
		});
	});

	ConfigurePopover();

	ConfigureDownloadPopup();
});

function GetHeaderSummary() {

	var budgetValue = NumberToString($('#Budget').val(), 2, 1);

	$('#spanBudget').text(budgetValue);

	var varianceText = NumberToString($('#Variance').val(), 2, 1);

	if ($('#Variance').val() < 0) {
		$('#spanVariance').addClass('txt-red');
	}
	else {
		$('#spanVariance').addClass('txt-color-green');
	}

	$('#spanVariance').text(varianceText);

	var totalValue = NumberToString($('#Total').val(), 2, 1);
	var actualValue = NumberToString($('#Actual').val(), 2, 1);
	var accuralValue = NumberToString($('#Accurals').val(), 2, 1);

	$('#spanTotal').text(totalValue);
	$('#spanActual').text(actualValue);
	$('#spanAccural').text(accuralValue);

	GetCurrencyDetails();
}

function GetCurrencyDetails() {
	var requestObject = {
		"ToDate": $('#ToDate').val(),
		"CoyId": $('#CoyId').val(),
		"ReportDefinitionType": $('#ReportDefinitionType').val()
	}

	$.ajax({
		url: "/Finance/GetOperationCostHeaderDetails",
		type: "POST",
		"data": requestObject,
		"datatype": "JSON",
		success: function (data) {
			if (data != null) {
				CurrencyForExportToExcel = data.currency;
				$('#spanCurrency').text(data.currency);

				$('#spanSubHeadingFromDate').text(data.rcFromDateTime);
				$('#spanSubHeadingToDate').text(data.rcToDateTime);
				$('#spanSubHeadingNoOfDays').text(data.numberOfDays);
				//set margin height
				SetHeaderMargin();
			}
		}
		
	});
}

function GetOpexTransationList() {
	$('#dtTransaction').DataTable().destroy();
	transactionList = $('#dtTransaction').DataTable({
		//"dom": '<<"row mb-3"<"col-12 col-md-6 offset-md-0 col-lg-6 offset-lg-2 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-6 col-lg-4 col-xl-4"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 50,
		"scrolX": true,
		"scrollCollapse": true,
		"aaSorting": [],
		"language": {
			"emptyTable": "No transactions available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Finance/GetVeselTransactionDetails",
			"type": "POST",
			"data": {
				"TransactionCostRequestUrl": $('#TransactionRequestUrl').val()
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
				width: "15px",
				orderable: false,
				render: function (data, type, full, meta) {
					if (full.documentCount == 0) {
						return '';
					}
					else {
						var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';
						var uniqueId = full.counter;
						var element = '';

						element = '<a class="text-black documentPopup cursor-pointer universalIdentifier_' + uniqueId + '" target="_blank" ><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
						return element;
					}
				}
			},
			{
				className: "tdblock td-row-header font-weight-600 data-text-align",
				data: "voucherNo",
				name: "VoucherNo",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetActualCellData(data);
				}
			},
			{
				className: "data-text-align",
				data: "type",
				name: "Type",
				width: "10px",
				render: function (data, type, full, meta) {
					return GetCellData('Type', data);
				}
			},
			{
				className: "data-datetime-align",
				data: "transactionDate",
				name: "TransactionDate",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetCellData('Date', data);
				}
			},
			{
				className: "data-text-align",
				data: "currency",
				name: "Currency",
				width: "15px",
				render: function (data, type, full, meta) {
					return GetCellData('Currency', data);
				}
			},
			{
				className: "data-number-align",
				data: "amount",
				name: "Amount",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetFormattedDecimal(type, 'Amount', data, 2, '0.00');
				}
			},
			{
				className: "data-number-align tdblock",
				data: "amountBase",
				name: "AmountBase",
				width: "20px",
				render: function (data, type, full, meta) {
					return GetFormattedDecimal(type, 'Amount Base', data, 2, '0.00');
				}
			},
			{
				className: "data-text-align tdblock",
				data: "order",
				name: "Order",
				width: "460px",
				render: function (data, type, full, meta) {
					return GetCellData('Order', data);
				}
			}
		]
	});

	$('.btnExport').click(() => {
		$.ajax({
			url: "/Finance/GetVeselTransactionDetailsExportToExcel",
			type: "POST",
			xhrFields: {
				responseType: 'blob'
			},
			"data": {
				"TransactionCostRequestUrl": $('#TransactionRequestUrl').val(),
				"CurrencyForExportToExcel": CurrencyForExportToExcel,
				"CurrentStageTitle": $('#CurrentStageTitle').val()
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
}

function SetPageParameter() {
	var requestObject = {
		"SelectedYear": $('#SelectedYear').val(),
		"SelectedMonth": $('#SelectedMonth').val(),
		"IsTransactionLevel": $('#IsTransactionLevel').val(),
		"ToDate": $('#ToDate').val(),
		"PreviousStageUrl": $('#PreviousStageUrl').val(),
		"TransactionRequestUrl": $('#TransactionRequestUrl').val(),
		"VesselId": $('#VesselId').val()
	}

	$.ajax({
		url: "/Finance/SetPageParameter",
		type: "POST",
		"data": requestObject,
		success: function (data) {
			window.location.href = data;
		}
		
	});
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
	return '<label>' + label + '</label> <br />' + GetActualCellData(data);
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

//Download Methods
function ConfigurePopover() {
	$('#dtTransaction tbody').on('click', 'a.documentPopup', function () {
		InitializePopoverConstructor();
		$('body').addClass('popover-design');
		var data = transactionList.row($(this).parents('tr')).data();
		var uniqueClsSel = 'universalIdentifier_' + data.counter;
		console.log("data", data);
		if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
			DocumentPopOver(uniqueClsSel, data.invoiceDocumentId, data.accountingCompanyId, data.voucherNo, data.documentCount);
		}
		else {
			$('.' + uniqueClsSel).popover('show');
		}
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

function DocumentPopOver(uniqueClsSel, invoiceDocumentId, accountingCompanyId, voucherNo, documentCount) {
	var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
		'<div class="loader mx-auto">' +
		'<div class="ball-clip-rotate">' +
		'<div></div>' +
		'</div>' +
		'</div>' +
		'</div>';

	$('.' + uniqueClsSel).attr('title', 'Attachments <a href = "#" class= "close close-popover cursor-pointer"><img src="/images/popover-close.png" /></a>');
	$('.' + uniqueClsSel).attr('data-placement', 'bottom');
	$('.' + uniqueClsSel).attr('data-trigger', 'focus');
	$('.' + uniqueClsSel).attr('data-toggle', 'popover');
	$('.' + uniqueClsSel).attr('data-html', true);
	$('.' + uniqueClsSel).attr('data-content', '<div class="elementLoader p-2"></div>');

	$.ajax({
		url: "/Finance/GetDocumentList",
		type: "POST",
		dataType: "JSON",
		global: false,
		data: {
			"invoiceDocumentId": invoiceDocumentId,
			"accountingCompanyId": accountingCompanyId,
			"voucherNo": voucherNo,
			"documentCount": documentCount
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

			if (attachCount > 1) {
				var html_content = "<div class='elementLoader scroller'><table class='table table-condensed table-borderless mb-0'><tbody>";
				for (var i = 0; i < attachCount; i++) {
					var iconPath = '/images/Download-doc-active.png';
					html_content += "<tr>";
					html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='" + iconPath + "' class='m-0' width='18' title='Download'/>";
					html_content += "<span class='documentDescription'> " + jsonArray[i].documentDescription + " </span>";
					html_content += "<span class='documentCategory d-none'> " + jsonArray[i].documentCategory + " </span>";
					html_content += "<span class='documentfileName d-none'> " + jsonArray[i].documentFilename + " </span> ";
					html_content += "<span class='documentSizeInBytes d-none'> " + jsonArray[i].documentSizeInBytes + " </span>";
					html_content += "<span class='matchedId d-none'> " + jsonArray[i].matchedId + " </span>";
					html_content += "<span class='fileType d-none'> " + jsonArray[i].fileType + " </span>";
					html_content += "<span class='cloudDocumentIdentifier d-none'> " + jsonArray[i].cloudDocumentIdentifier + " </span ></a></td> ";
					html_content += "</tr>";
				}
				html_content += "</tbody></table></div>";
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('title', 'Attachments (' + attachCount + ') <a href="#" class="close close-popover"><img src="/images/popover-close.png" /></a>');
				$('.' + uniqueClsSel).attr('data-content', html_content);
				$('.' + uniqueClsSel).attr('data-placement', 'bottom');
				$('.' + uniqueClsSel).attr('data-trigger', 'focus');
				$('.' + uniqueClsSel).attr('data-toggle', 'popover');
				$('.' + uniqueClsSel).attr('data-html', true);
				$('.' + uniqueClsSel).popover('show');
				$('.' + uniqueClsSel).removeAttr('title');
			}
			else if (attachCount == 1) {
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('data-content', '');
				var DocumentMapping = new Map();
				DocumentMapping.set(0, {
					documentDescription: jsonArray[0].documentDescription,
					documentCategory: jsonArray[0].documentCategory,
					documentFilename: jsonArray[0].documentFilename,
					documentSizeInBytes: jsonArray[0].documentSizeInBytes,
					fileType: jsonArray[0].fileType,
					cloudDocumentIdentifier: jsonArray[0].cloudDocumentIdentifier,
					matchedId: jsonArray[0].matchedId

				});
				DownloadSelectedAttachment(DocumentMapping);
			}
		},
		complete: function () {
			$(".elementLoader").unblock();
		},
	});
}

function DownloadSelectedAttachment(DocumentMapping) {
	var nextAttach = 0;
	var totalAttachment = DocumentMapping.size;
	DownloadNextAttachment();

	function DownloadNextAttachment() {
		var documentDescription = (DocumentMapping.get(nextAttach).documentDescription != null && DocumentMapping.get(nextAttach).documentDescription != 'undefined') ? DocumentMapping.get(nextAttach).documentDescription.trim() : '';
		var documentCategory = (DocumentMapping.get(nextAttach).documentCategory != '' && DocumentMapping.get(nextAttach).documentCategory != null && DocumentMapping.get(nextAttach).documentCategory != 'undefined') ? DocumentMapping.get(nextAttach).documentCategory : null;
		var documentFilename = (DocumentMapping.get(nextAttach).documentFilename != null && DocumentMapping.get(nextAttach).documentFilename != 'undefined') ? DocumentMapping.get(nextAttach).documentFilename : '';
		var documentSizeInBytes = (DocumentMapping.get(nextAttach).documentSizeInBytes != null && DocumentMapping.get(nextAttach).documentSizeInBytes != 'undefined' && DocumentMapping.get(nextAttach).documentSizeInBytes != '') ? DocumentMapping.get(nextAttach).documentSizeInBytes : 0;
		var fileType = (DocumentMapping.get(nextAttach).fileType != '' && DocumentMapping.get(nextAttach).fileType != null && DocumentMapping.get(nextAttach).fileType != 'undefined') ? DocumentMapping.get(nextAttach).fileType : null;
		var cloudDocumentIdentifier = (DocumentMapping.get(nextAttach).cloudDocumentIdentifier != null && DocumentMapping.get(nextAttach).cloudDocumentIdentifier != 'undefined') ? DocumentMapping.get(nextAttach).cloudDocumentIdentifier : '';
		var matchedId = (DocumentMapping.get(nextAttach).matchedId != null && DocumentMapping.get(nextAttach).matchedId != 'undefined') ? DocumentMapping.get(nextAttach).matchedId : '';
		var input = {
			"documentDescription": documentDescription,
			"documentFilename": documentFilename,
			"documentCategory": documentCategory,
			"documentSizeInBytes": documentSizeInBytes,
			"fileType": fileType,
			"cloudDocumentIdentifier": cloudDocumentIdentifier,
			"matchedId": matchedId
		};

		$.ajax({
			url: "/Finance/DownloadDocument",
			type: "POST",
			dataType: "JSON",
			global: false,
			data: {
				"input": JSON.stringify(input)
			},
			success: function (data) {

				if (data.bytes != null) {
					var array = base64ToArrayBuffer(data.bytes);
					saveByteArray(data.filename, array, data.fileType);
				} else {
					ToastrAlert("validate", "File Not Found for \"" + data.filename + "\"");
				}

				nextAttach++;
				if (totalAttachment > nextAttach) {
					DownloadNextAttachment();
				}
			}
			
		});
	}
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

		var documentDescription = $(this).children('span.documentDescription').text();
		var documentFilename = $(this).children('span.documentFilename').text();
		var documentCategory = $(this).children('span.documentCategory').text();
		var documentSizeInBytes = $(this).children('span.documentSizeInBytes').text();
		var fileType = $(this).children('span.fileType').text();
		var cloudDocumentIdentifier = $(this).children('span.cloudDocumentIdentifier').text();
		var matchedId = $(this).children('span.matchedId').text();

		var input = {
			"documentDescription": (documentDescription != null && documentDescription != 'undefined' && documentDescription != '') ? documentDescription.trim() : '',
			"documentFilename": (documentFilename != '' && documentFilename != null && documentFilename != 'undefined') ? documentFilename.trim() : '',
			"documentCategory": (documentCategory != '' && documentCategory != null && documentCategory != 'undefined') ? documentCategory.trim() : null,
			"documentSizeInBytes": (documentSizeInBytes != null && documentSizeInBytes != 'undefined' && documentSizeInBytes != '') ? documentSizeInBytes.trim() : 0,
			"fileType": (fileType != null && fileType != 'undefined' && fileType != '') ? fileType.trim() : null,
			"cloudDocumentIdentifier": (cloudDocumentIdentifier != null && cloudDocumentIdentifier != 'undefined' && cloudDocumentIdentifier != '') ? cloudDocumentIdentifier.trim() : '',
			"matchedId": (matchedId != null && matchedId != 'undefined' && matchedId != '') ? matchedId.trim() : ''
		};

		$.ajax({
			url: "/Finance/DownloadDocument",
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
					saveByteArray(data.filename, array, data.fileType);
				} else {
					ToastrAlert("validate", "File Not Found for \"" + data.filename + "\"");
				}
			}
		});

	});
}
