import moment from "moment";
import * as JSZip from "jszip";
window.JSZip = JSZip;
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, GetCookie, ToastrAlert, BackButton, datepickerheightinmobile } from "../common/utilities.js"
import { GetCellData, GetExportData, GetExportCellData, GetExportFormattedDate, CustomizedExcelHeader } from "../common/datatablefunctions.js"
import { MedicalSignOffListPageKey } from "../common/constants.js"

var startDate, endDate;
var medicalSignOffList;
$(document).ready(function () {

	AddLoadingIndicator();
	RemoveLoadingIndicator();
	AjaxError();

	$('input[type="checkbox"]').click(function () {
		var inputValue = $(this).attr("value");
		$("." + inputValue).toggle();
	});

	//Sidebar back
	BackButton(MedicalSignOffListPageKey, false)

	$('#mobileactiontoggle').click(function () {
		$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
	});

	$(document).click(function () {
		if ($("#mobileActiondropdown").hasClass('show')) {
			$("#mobileActiondropdown").removeClass('show');
		}
	});

	var ispageLoad = true;
	var start = moment($('#FromMedicalSignOff').val(), 'DD-MM-YYYY');
	var end = moment($('#ToMedicalSignOff').val(), 'DD-MM-YYYY');


	$("#dateRangeMSO").caleran(
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

	LoadMedicalSignOff();

	$('.btnCrewList').click(function () {
		CallCrewList();
	});

	$('#btnSearch').click(function () {
		$('#SelectedStageFilter').val('All');
		Search();
	});

	//$('#btnClear').click(function () {
	//	var currentDate = moment();
	//	var startDate = moment().subtract(3, "month");
	//	var start = moment(startDate, 'DD-MM-YYYY');
	//	var end = moment(currentDate, 'DD-MM-YYYY');
	//	setDateDetails(start, end, false);
	//});

	$('.btnExportMSO').click(() => {
		var searchValue = medicalSignOffList.search();
		medicalSignOffList.search("").draw();

		$('#dtMedicalSignOff.cardview thead').addClass("export-grid-show");
		$('#dtMedicalSignOff').DataTable().buttons(0, 2).trigger();
		$('#dtMedicalSignOff.cardview thead').removeClass("export-grid-show");

		medicalSignOffList.search(searchValue).draw();
	});
});


function setDateDetails(start, end, isSearch) {
	startDate = start.format("DD MMM YYYY");
	endDate = end.format("DD MMM YYYY");
	$("#dateRangeMSO").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
	$("#tblspndtrmso").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));

	if (isSearch) {
		$('#SelectedStageFilter').val('All');
		Search();
	}
}


function Search() {
	var input = {
		"FromMedicalSignOff": startDate,
		"ToMedicalSignOff": endDate,
		"EncryptedVesselId": $('#EncryptedVesselId').val()
	}

	$.ajax({
		url: "/Crew/SetPageParameterMSO",
		type: "POST",
		"data": input,
		success: function (data) {
			window.location.href = data;
		}
	});
}

function CallCrewList() {
	var input = {
		"EncryptedVesselId": $('#EncryptedVesselId').val()
	}
	$.ajax({
		url: "/Crew/LoadCrewList",
		type: "POST",
		"data": input,
		success: function (data) {
			window.location.href = data;
		}
	});
}

function LoadMedicalSignOff() {
	var input = {
		"FromMedicalSignOff": startDate,
		"ToMedicalSignOff": endDate,
		"EncryptedVesselId": $('#EncryptedVesselId').val()
	}

	$('#dtMedicalSignOff').DataTable().destroy();
	medicalSignOffList = $('#dtMedicalSignOff').DataTable({
		//"dom": '<<"row mb-3"<"col-12 col-md-6 offset-md-0 col-lg-3 offset-lg-5 col-xl-5 offset-xl-3 dt-infomation"i><"col-12 col-md-6 col-lg-4 col-//xl-4"f>><""rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
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
		"order": [[0, "asc"]],
		"language": {
			"emptyTable": "No crew records available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/Crew/GetMedicalSignOffList",
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
				title: "Medical Sign Off List",
				customize: function (xlsx) {
					CustomizedExcelHeader(xlsx, 2);
				},
				messageTop: function () {
					return 'Vessel : ' + $('#VesselName').val() + '\nFrom Date : ' + startDate + ' | ' + ' To Date ' + endDate;
				}
			},
			'pdf', 'print'
		],
		"columns": [
			{
				className: "data-text-align tdblock td-row-header",
				"data": "crewFullName",
				width: "270px",
				render: function (data, type, full, meta) {
					if (full.isCrewNameVisible) {
						return '<a class="text-uppercase" href = "/Crew/Details/?Source=2&CrewId=' + full.encryptedCrewId + '"> ' + GetExportData(full.crewFullName) + '</a >';
					}
					else {
						return GetExportCellData('', '');
					}
				}
			},
			{
				className: "data-text-align",
				"data": "rankDescription",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Rank', data);
				}
			},
			{
				className: "data-text-align",
				"data": "nationality",
				width: "80px",
				render: function (data, type, full, meta) {
					return GetExportCellData('NAT', data);
				}
			},
			{
				className: "data-text-align",
				"data": "onBoardDaysDisplay",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Onboard Days', data);
				}
			},
			{
				className: "data-datetime-align",
				"data": "signOn",
				width: "90px",
				render: function (data, type, full, meta) {
					return GetExportFormattedDate(type, 'Sign On', data);
				}
			},
			{
				className: "data-datetime-align",
				"data": "signOff",
				width: "90px",
				render: function (data, type, full, meta) {
					return GetExportFormattedDate(type, 'Sign Off', data);
				}
			},
			{
				className: "data-text-align",
				"data": "reason",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Reason', data);
				}
			},
			{
				className: "data-text-align",
				"data": "portOff",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Port Off', data);
				}
			},
			{
				className: "text-left",
				"data": "countryOff",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Country Off', data);
				}
			},

			{
				className: "data-text-align",
				"data": "currentStatusDescription",
				width: "80px",
				render: function (data, type, full, meta) {
					return GetExportCellData('Current Status', data);
				}
			},
			{
				className: "data-datetime-align",
				"data": "statusEndDate",
				width: "110px",
				render: function (data, type, full, meta) {
					return GetExportFormattedDate(type, 'Status End Date', data);
				}
			},
		],
	});
	$.fn.DataTable.ext.pager.numbers_length = 4;
}