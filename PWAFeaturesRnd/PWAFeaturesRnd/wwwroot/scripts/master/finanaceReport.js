import "@chenfengyuan/datepicker";
import "daterangepicker";
import toastr from "toastr";
import moment from "moment";
import * as JSZip from "jszip";
window.JSZip = JSZip;
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { CustomizedExcelHeader } from "../common/datatablefunctions.js"
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, ToastrAlert, GetCookie, IsNullOrEmptyOrUndefined, headerReadMoreFindings, RegisterTabSelectionEvent } from "../common/utilities.js";
import { MobileScreenSize, FinanceListPageKey } from "../common/constants.js"

var ispageLoad;
var CurrencyForExportToExcel;
var gridOpexDrillDown

$(window).on('resize', SetHeaderMargin);

function SetHeaderMargin() {
	$(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));
}

$(document).ready(function () {

	AddLoadingIndicator();
	RemoveLoadingIndicator();

	$('.back').click(function () {
		$.ajax({
			url: "/Finance/GetFinanceSourceURL",
			type: "POST",
			dataType: "JSON",
			data: {
				"pageKey": $("#PreviousStageName").val()
			},
			success: function (data) {
				if (data != null) {
					var isMobileScreen = false
					if (screen.width < MobileScreenSize) {
						isMobileScreen = true
					}
					if (IsNullOrEmptyOrUndefined($("#PreviousStageName").val()) && isMobileScreen) {
						window.location.replace("/Dashboard/VesselDetailsMobile/");
					}
					else {
						window.location.replace(data);
					}						
				}
			}
		});
	});
	RegisterTabSelectionEvent('.mobileTabClick', FinanceListPageKey);
	if (($(window).width() < MobileScreenSize)) {
		var MobilTabCls = $("#ActiveMobileTabClass").val();
		$('.' + MobilTabCls)[0].click();
	}
	$("#graphExpander").click(function () {
		if (screen.width < MobileScreenSize) {
			$('#FinanceReportChart').height(600);
		} else {
			$('#FinanceReportChart').height(300);
		}
	});

	$(".tab-2").click(function () {
		if (screen.width < MobileScreenSize) {
			$('#FinanceReportChart').height(600);
		}
	});

	$('#generalledger').click(function () {
		location.href = "/Finance/GeneralLedger/?VesselId=" + $('#VesselId').val();
	});

	ispageLoad = true;
	$('#IsTransactionLevel').val(false);

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

	GetOperationCostList();

	$(document).ajaxStop(function () {
		if (screen.width < 760) {
			headerReadMoreFindings('headershowmorewrapper', 'header');
		}
		SetHeaderMargin();
	});
	AjaxError();
});

function SetPageParameter() {
	var requestObject = {
		"SelectedYear": $('#SelectedYear').val(),
		"SelectedMonth": $('#SelectedMonth').val(),
		"IsTransactionLevel": $('#IsTransactionLevel').val(),
		"ToDate": $('#ToDate').val(),
		"CoyId": $('#CoyId').val(),
		"AccountLevel": $('#AccountLevel').val(),
		"ReportDefinitionType": $('#ReportDefinitionType').val(),
		"AccountId": $('#AccountId').val(),
		"Parent1AccAndDesc": $('#Parent1AccAndDesc').val(),
		"Parent2AccAndDesc": $('#Parent2AccAndDesc').val(),
		"Parent3AccAndDesc": $('#Parent3AccAndDesc').val(),
		"VesselId": $('#VesselId').val(),
		"VesselName": $('#VesselName').val(),
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

function GetOperationCostList() {
	var requestObject = {
		"ToDate": $('#ToDate').val(),
		"CoyId": $('#CoyId').val(),
		"AccountLevel": $('#AccountLevel').val(),
		"ReportDefinitionType": $('#ReportDefinitionType').val(),
		"AccountId": $('#AccountId').val(),
		"Parent1AccAndDesc": $('#Parent1AccAndDesc').val(),
		"Parent2AccAndDesc": $('#Parent2AccAndDesc').val(),
		"Parent3AccAndDesc": $('#Parent3AccAndDesc').val(),
		"VesselId": $('#VesselId').val(),
		"VesselName": $('#VesselName').val(),
	}

	$.ajax({
		url: "/Finance/GetOperationCostList",
		type: "POST",
		"data": requestObject,
		"datatype": "JSON",
		success: function (data) {
			if (data != null) {

				var budgetValue = NumberToString(data.budget, 2, 1)
				$('#spanBudget').text(budgetValue);

				var varianceText = NumberToString(data.variance, 2, 1);
				if (data.variance < 0) {
					if ($('#spanVariance').hasClass('txt-color-green')) {
						$('#spanVariance').removeClass('txt-color-green');
					}
					$('#spanVariance').addClass('txt-red');
				}
				else {
					if ($('#spanVariance').hasClass('txt-red')) {
						$('#spanVariance').removeClass('txt-red');
					}
					$('#spanVariance').addClass('txt-color-green');
				}
				$('#spanVariance').text(varianceText);

				var totalValue = NumberToString(data.total, 2, 1);
				$('#spanTotal').text(totalValue);

				var totalActual = NumberToString(data.actual, 2, 1);
				$('#spanActual').text(totalActual);

				var totalAccural = NumberToString(data.accurals, 2, 1);
				$('#spanAccural').text(totalAccural);

				LoadOperatingCostList(data.operatingCostList, budgetValue, totalAccural, totalActual, totalValue, varianceText);
				LoadBudgetHeader(data.operatingCostList);
			}

			if ($('#AccountLevel').val() != -1) {

				$('.breadcrum').html('<a class="txt-blue" href="/Finance/List/?OperationCostRequestUrl=' + $('#PreviousStageUrl').val() + '&VesselId=' + $('#VesselId').val() + '"> << ' + $('#PreviousStageName').val() + '</a>');
			}
		}
	});
}

function LoadBudgetHeader(opexList) {
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
				$('#currencyInfodt').text(data.currency);
				$('#spanSubHeadingFromDate').text(data.rcFromDateTime);
				$('#spanSubHeadingToDate').text(data.rcToDateTime);
				$('#spanSubHeadingNoOfDays').text(data.numberOfDays);
				//set margin here
				SetHeaderMargin();

				if ($('#AccountLevel').val() == -1) {
					LoadGraph(opexList, data.currency);
				} else {
					if (!$("#accordion").hasClass('d-none')) {
						$("#accordion").addClass('d-none');
					}
				}
			}
		}
	});
}

function LoadOperatingCostList(OperatingCostList, budgetValue, totalAccural, totalActual, totalValue, varianceText) {

	$('#dtFinanceReport').DataTable().destroy();
	gridOpexDrillDown = $('#dtFinanceReport').DataTable({
		/*"dom": '<<"row mb-3"<"col-12 col-md-6 offset-md-0 col-lg-6 offset-lg-2 col-xl-6 offset-xl-2 dt-infomation"i><"col-12 col-md-6 col-lg-4 col-xl-4"f>><rt><"clearfix"<"float-left"l><""p>>>',*/
		"dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
			'<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": false,
		"autoWidth": false,
		"searching": true,
		"paging": false,
		"info": true,
		"pageLength": 25,
		"order": [],
		"language": {
			"emptyTable": "No data available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		data: OperatingCostList,
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
				customize: function (xlsx) {
					if ($('#AccountLevel').val() == -1) {
						CustomizedExcelHeader(xlsx, 3);
					}
					else {
						CustomizedExcelHeader(xlsx, 4);
					}
				},
				title: "Opex Drill Down Report",
				messageTop: function () {
					if ($('#AccountLevel').val() == -1) {
						return 'Vessel : ' + $('#VesselName').val() + '\nDate : ' + moment(new Date()).format("D MMM YYYY") + ' | Budget : ' + budgetValue + ' | Accrual : ' + totalAccural + ' | Actual : ' + totalActual + '\nTotal : ' + totalValue + ' | Variance : ' + varianceText + ' | Currency : ' + CurrencyForExportToExcel
					}
					else {
						return 'Vessel : ' + $('#VesselName').val() + "\nAccount : " + $('#CurrentStageTitle').val() + '\nDate : ' + moment(new Date()).format("D MMM YYYY") + ' | Budget : ' + budgetValue + ' | Accrual : ' + totalAccural + ' | Actual : ' + totalActual + '\nTotal : ' + totalValue + ' | Variance : ' + varianceText + ' | Currency : ' + CurrencyForExportToExcel
					}
				}
			},
			'pdf', 'print'
		],
		"columns": [
			{
				className: "tdblock data-text-align",
				render: function (data, type, full, meta) {
					if (full.drillDownLevel != 0) {
						return '<a class="txt-blue" href = "/Finance/List/?OperationCostRequestUrl=' + full.operatingCostUrl + '&VesselId=' + full.operatingCostVesselId + '"> ' + GetActualCellData(full.accountDescription) + '</a > ';
					}
					else if (full.drillDownLevel == 0) {
						return '<a class="txt-blue" href = "/Finance/Transaction/?PreviousRequest=' + full.previousOfTransactionRequestUrl + '&TransactionUrl=' + full.transactionRequestUrl + '&VesselId=' + full.operatingCostVesselId + '"> ' + GetActualCellData(full.accountDescription) + '</a > ';
					}
				}
			},
			{
				className: "data-number-align",
				type: "html-num",
				"data": "budget",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = NumberToString(data, 2, 1);
						if (data < 0) {
							var kpi = '<span class="txt-red">' + numberText + '</span>'
							return GetCellData('Budget', kpi);
						}
						return GetCellData('Budget', numberText);
					}
					return data;
				}
			},
			{
				className: "data-number-align",
				type: "html-num",
				data: "actual",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = NumberToString(data, 2, 1);
						if (data < 0) {
							var kpi = '<span class="txt-red">' + numberText + '</span>'
							return GetCellData('Actual', kpi);
						}
						return GetCellData('Actual', numberText);
					}
					return data;
				}
			},
			{
				className: "data-number-align",
				type: "html-num",
				data: "accurals",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = NumberToString(data, 2, 1);
						if (data < 0) {
							var kpi = '<span class="txt-red">' + numberText + '</span>'
							return GetCellData('Accrual', kpi);
						}
						return GetCellData('Accrual', numberText);
					}
					return data;
				}
			},
			{
				className: "data-number-align",
				type: "html-num",
				data: "total",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = NumberToString(data, 2, 1);
						if (data < 0) {
							var kpi = '<span class="txt-red">' + numberText + '</span>'
							return GetCellData('Total', kpi);
						}
						return GetCellData('Total', numberText);
					}
					return data;
				}
			},
			{
				className: "data-number-align tdblock",
				type: "html-num",
				"data": "variance",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var numberText = NumberToString(data, 2, 1);
						if (data < 0) {
							var kpi = '<span class="txt-red">' + numberText + '</span>'
							return GetCellData('Variance', kpi);
						}
						return GetCellData('Variance', numberText);
					}
					return data;
				}
			},
			{
				className: "data-number-align tdblock",
				"data": "variancePercent",
				width: "40px",
				render: function (data, type, full, meta) {
					let finalData = '';
					if (type === "display") {
						let decimalData = NumberToString(data, 2, 1);
						if (full.isVariancePercentNegative) {
							finalData = '<span class="txt-red">' + decimalData + '<span/>';
						} else {
							finalData = '<span class="txt-color-green">' + decimalData + '<span/>';
						}
						return GetCellData('Variance %', finalData);
					}
					return data;
				}
			}
		]
	});

	//$('.btnExportExcel').click(() => {
	//	var searchValue = gridOpexDrillDown.search();
	//	gridOpexDrillDown.search("").draw();

	//	$('#dtFinanceReport.cardview thead').addClass("export-grid-show");
	//	$('#dtFinanceReport').DataTable().buttons(0, 2).trigger();
	//	$('#dtFinanceReport.cardview thead').removeClass("export-grid-show");

	//	gridOpexDrillDown.search(searchValue).draw();
	//});

	$('.btnExportExcel').click(function () {
		ExportToExcel();
	})

}

function GetCustomCellData(displayText, type, data, htmlElement) {
	if (type === "display") {
		GetCellData(displayText, htmlElement)
	} else {
		return data;
	}
}

function reverseArray(arr) {
	if (arr.length === 0) {
		return []
	}
	return [arr.pop()].concat(reverseArray(arr))
}

function LoadGraph(opexDataList, currency) {
	var opexList = reverseArray(opexDataList);
	var budget = [], accountName = [];
	var actual = [], accurals = [];
	for (var i in opexList) {
		budget.push(parseInt(opexList[i].budget));
		accountName.push(opexList[i].label);
		accurals.push(opexList[i].accurals);
		actual.push(opexList[i].actual);
	}
	if (document.getElementById("FinanceReportChart")) {
		var horizontalBarChartData = {
			labels: accountName,
			datasets: [
				{
					label: "Actual",
					backgroundColor: "rgb(64, 178, 170)",
					data: actual,
					stack: "0"
				},
				{
					label: "Accrual",
					backgroundColor: "rgb(254, 206, 0)",
					data: accurals,
					stack: "0"
				},
				{
					label: "Budget",
					backgroundColor: "rgb(25, 131, 191)",
					data: budget,
					stack: "1"
				}
			],
		};
		var type;
		var xaxis = false;
		var yaxis = false;
		if (screen.width > MobileScreenSize) {
			type = "horizontalBar";
			$('#FinanceReportChart').height(300);
			xaxis = true;
		}
		else {
			type = "bar"
			$('#FinanceReportChart').height(600);
			yaxis = true;
		}

		var ctx6 = document.getElementById("FinanceReportChart").getContext("2d");
		window.myHorizontalBar = new Chart(ctx6, {
			showTooltips: false,
			type: type,
			data: horizontalBarChartData,
			options: {
				elements: {
					rectangle: {
						borderWidth: 0,
					},
				},
				responsive: true,
				maintainAspectRatio: false,
				datasetFill: false,
				legend: {
					position: "top",
				},
				title: {
					display: true,
					text: "OPERATING COSTS YTD MONTH END",
				},
				scales: {
					xAxes: [{
						gridLines: {
							display: xaxis,
						},
						ticks: {
							display: true,
							callback: function (value, index, values) {
								if (Number.isInteger(value)) {
									return NumberToString(value, 2, 1, 0);
								} else {
									return value;
								}
							},
							precision: 0
						},
						maxBarThickness: 13,
						barThickness: 13
					}],
					yAxes: [{
						gridLines: {
							display: yaxis,
						},
						ticks: {
							display: true,
							callback: function (value, index, values) {
								if (Number.isInteger(value)) {
									return NumberToString(value, 2, 1, 0);
								} else {
									return value;
								}
							},
							precision: 0
						},
						maxBarThickness: 13,
						barThickness: 13
					}]
				},
				tooltips: {
					enabled: true,
					mode: 'nearest',
					callbacks: {
						title: function (tooltipItems, data) {
							return data.datasets[tooltipItems[0].datasetIndex].label;
						},
						label: function (tooltipItems, data) {
							let val = '';
							if (screen.width > 760) {
								val = tooltipItems.xLabel;
							}
							else {
								val = tooltipItems.yLabel;
							}
							return NumberToString(val, 2, 1);
						}
					}
				},
				hover: {
					animationDuration: 1
				},

			},
		});
	}
}

function NumberToString(number, toFixedValue, state, decimalDigits = 2) {
	var numToString = "";
	if (number != null && number != '' && number != 'undefined') {
		numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
			{
				minimumFractionDigits: decimalDigits
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

function ExportToExcel() {

	var requestObject = {
		"SelectedYear": $('#SelectedYear').val(),
		"SelectedMonth": $('#SelectedMonth').val(),
		"ToDate": $('#ToDate').val(),
		"CoyId": $('#CoyId').val(),
		"ReportDefinitionType": $('#ReportDefinitionType').val(),
		"VesselId": $('#VesselId').val(),
	}

	$.ajax({
		url: "/Finance/ExportToExcelReport",
		type: "POST",
		"data": {
			"input": requestObject
		},
		success: function (data) {
			if (data.success) {
				ToastrAlert("success", data.message);
			}
			else {
				ToastrAlert("error", data.message);
			}
		}
	});
}

