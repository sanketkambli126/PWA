import Chart from "chart.js";
import ApexCharts from 'apexcharts';

$(document).ready(function () {

	var data = [
		{
			"datetime": "23 Apr 2020 08:00",
			"event": "FAP",
			"position": "F.A.O.P",
			"course": "0",
			"timesailed": "",
			"breakstime": "",
			"dist": "0",
			"avgspeed": "0.00",
			"rpm": "0.00",
			"trueslip": "0.00",
			"windforce": "",
			"seastate": "",
			"windspeed": "",
			"winddir": "",
			"fo": "0.00",
			"isfo": "0.00",
			"do": "0.00",
			"go": "0.00",
			"lng": "0.00",
			"fwdom": "0.00",
			"fwtech": "0.00",
			"clo": "0.00",
			"crank": "0.00",
			"aux": "0.00",
			"general": "0.00",
		},
		{
			"datetime": "23 Apr 2020 12:00",
			"event": "NN",
			"position": "36&deg;, 14'S 73&deg;,23' W ",
			"course": "343",
			"timesailed": "04:00",
			"breakstime": "",
			"dist": "29",
			"avgspeed": "7.25 <i class='fa fa-caret-right icon' data-toggle='tooltip' data-placement='bottom' title='Avg. speed less than charter avg. speed' data-html='true'></i>",
			"rpm": "52.68",
			"trueslip": "-0.04",
			"windforce": "05 Fresh Breeze",
			"seastate": "Slight",
			"windspeed": "10",
			"winddir": "08 Ahead",
			"fo": "0.00",
			"isfo": "2.40",
			"do": "0.00",
			"go": "0.00",
			"lng": "0.00",
			"fwdom": "0.00",
			"fwtech": "0.00",
			"clo": "0.00",
			"crank": "0.00",
			"aux": "0.00",
			"general": "0.00",
		},

	]

	var portcalllocation = $('#dtportcalllocation').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": true,
		"paging": true,
		"pageLength": 50,
		"data": data,
		"columns": [
			{
				className: "text-left d-none d-md-table-cell",
				data: "datetime",
				width: "180px",
				render: function (data, type, full, meta) { return GetCellData('Date/Time', data); }
			},
			{
				className: "text-left tdblock td-row-header",
				data: "event",
				width: "115px",
				render: function (data, type, full, meta) { return GetCellData('Event', data); }
			},
			{
				className: "text-left d-md-none d-lg-none d-xl-none td-w-43",
				data: "datetime",
				width: "180px",
				render: function (data, type, full, meta) { return GetCellData('Date/Time', data); }
			},
			{
				className: "text-left td-w-25 pr-0",
				data: "position",
				width: "110px",
				render: function (data, type, full, meta) { return GetCellData('Position', data); }
			},
			{
				className: "text-left td-w-18 pr-0",
				data: "course",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Course', data); }
			},
			{
				className: "text-left td-w-43",
				"data": "timesailed",
				width: "50px",
				render: function (data, type, full, meta) { return GetCellData('Time Sailed', data); }
			},
			{
				className: "text-left td-w-25 pr-0",
				"data": "breakstime",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Breaks Time', data); }
			},
			{
				className: "text-left td-w-18 pr-0",
				"data": "dist",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Dist', data); }
			},
			{
				className: "text-left td-w-43 pr avg-speed",
				"data": "avgspeed",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Avg Speed', data); }
			},
			{
				className: "text-left td-w-25 pr-0",
				"data": "rpm",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('RPM', data); }
			},
			{
				className: "text-left td-w-18 pr-0",
				"data": "trueslip",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('True Slip', data); }
			},
			{
				className: "text-left",
				"data": "windforce",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Wind Force', data); }
			},
			{
				className: "text-left",
				"data": "seastate",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Sea State', data); }
			},
			{
				className: "text-left",
				"data": "windspeed",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Wind Speed', data); }
			},
			{
				className: "text-left",
				"data": "winddir",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Wind Dir', data); }
			},

			{
				className: "text-left td-w-19",
				"data": "fo",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('FO', data); }
			},
			{
				className: "text-left td-w-20",
				"data": "isfo",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('ISFO', data); }
			},
			{
				className: "text-left td-w-22",
				"data": "do",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('DO', data); }
			},
			{
				className: "text-left row-5-columns",
				"data": "go",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('GO', data); }
			},
			{
				className: "text-left td-w-19",
				"data": "lng",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('LNG', data); }
			},
			{
				className: "text-left td-w-20",
				"data": "fwdom",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('FW DOM', data); }
			},
			{
				className: "text-left td-w-43",
				"data": "fwtech",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('FW TECH', data); }
			},
			{
				className: "text-left td-w-19",
				"data": "clo",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('CLO', data); }
			},
			{
				className: "text-left td-w-20",
				"data": "crank",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('CRANK', data); }
			},
			{
				className: "text-left td-w-22",
				"data": "aux",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('AUX', data); }
			},
			{
				className: "text-left row-5-columns",
				"data": "general",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('General', data); }
			},
		]


	});

	//port service attachment list modal data

	var servicedata = [
		{
			"createddate": "27 OCT 2020",
			"title": "Covid Guidelines",
			"description": "Covid Guidelines",
		},
	]
	var portservicelist = $('#dtportservicelist').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": servicedata,
		"columns": [
			{
				className: "text-left",
				data: "createddate",
				render: function (data, type, full, meta) { return GetCellData('Created Date', data); }
			},
			{
				className: "text-left",
				data: "title",
				render: function (data, type, full, meta) { return GetCellData('Title', data); }
			},
			{
				className: "text-left tdblock",
				data: "description",
				render: function (data, type, full, meta) { return GetCellData('Description', data); }
			}
		]
	});

	//bad weather list modal data

	var badweatherdata = [
		{
			"length": "Swell Length",
			"charter": "Short &nbsp;&nbsp;&nbsp;0-100",
			"max": "Short &nbsp;&nbsp;&nbsp;0-100",
		},
		{
			"length": "Wind Force",
			"charter": "04 Moderate breeze",
			"max": "05 Fresh breeze",
		},
	]
	var badweatherlist = $('#dtbadweatherlist').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": badweatherdata,
		"columns": [
			{
				className: "data-text-align tdblock",
				data: "length",
				"orderable": false,
				//render: function (data, type, full, meta) { return data }
			},
			{
				className: "data-text-align",
				data: "charter",
				render: function (data, type, full, meta) { return GetCellData('Charter', data); }
			},
			{
				className: "data-text-align",
				data: "max",
				render: function (data, type, full, meta) { return GetCellData('Max', data); }
			}
		]
	});

	//offhire list modal data

	var offhiredata = [
		{
			"reason": "Commercial Reason",
			"delay": "12:15",
			"offhire": "No",
			"from": "25 Apr 2020 12:00",
			"to": "26 Apr 2020 00:15",
		},
		{
			"reason": "Canal Transit",
			"delay": "16:30",
			"offhire": "Yes",
			"from": "25 Apr 2020 12:00",
			"to": "26 Apr 2020 04:30",
		},
	]
	var offhirelist = $('#dtOffHireList').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": offhiredata,
		"columns": [
			{
				className: "text-left tdblock",
				data: "reason",
				render: function (data, type, full, meta) { return GetCellData('Reason', data); }
			},
			{
				className: "text-left",
				data: "delay",
				render: function (data, type, full, meta) { return GetCellData('Delay', data); }
			},
			{
				className: "text-left",
				data: "offhire",
				render: function (data, type, full, meta) { return GetCellData('Off - Hire', data); }
			},
			{
				className: "text-left",
				data: "from",
				render: function (data, type, full, meta) { return GetCellData('From', data); }
			},
			{
				className: "text-left",
				data: "to",
				render: function (data, type, full, meta) { return GetCellData('To', data); }
			}
		]
	});

	function GetCellData(label, data) {
		return '<label>' + label + '</label> <br />' + data;
	}

	//graph js charter order fuel-consuption
	$(function () {
		//get the bar chart canvas
		var ctx = $("#fuel-consuption");
		//bar chart data
		var data = {
			labels: ["LSFO", "DO", "GO", "LNG"],
			datasets: [
				{
					label: "Cht. Ballast",
					data: [32.0, 10, 10.5, 15],
					backgroundColor: ["#d38646", "#d38646", "#d38646", "#d38646"],
					barPercentage: 0.5,
					barThickness: 20,
					maxBarThickness: 20,
					minBarLength: 2,
				},
				{
					label: "Actual",
					data: [27.54, 0.06, 15, 13],
					backgroundColor: ["#775b5f", "#775b5f", "#775b5f", "#775b5f"],
					barPercentage: 0.5,
					barThickness: 20,
					maxBarThickness: 20,
					minBarLength: 2,
				}
			]
		};
		//options
		var options = {
			tooltips: {
				enabled: true
			},
			hover: {
				animationDuration: 1
			},
			animation: {
				duration: 1,
				onComplete: function () {
					var chartInstance = this.chart,
						ctx = chartInstance.ctx;
					ctx.textAlign = 'center';
					ctx.fillStyle = "rgba(0, 0, 0, 1)";
					ctx.textBaseline = 'bottom';
					this.data.datasets.forEach(function (dataset, i) {
						var meta = chartInstance.controller.getDatasetMeta(i);
						meta.data.forEach(function (bar, index) {
							var data = dataset.data[index];
							ctx.fillText(data, bar._model.x, bar._model.y - 5);
						});
					});
				}
			},
			responsive: true,
			title: {
				display: true,
				position: "top",
				text: "Fuel Consuption (Daily Avg)",
				fontSize: 12
			},
			legend: {
				display: true,
				position: "bottom",
				labels: {
					fontColor: "#333",
					fontSize: 14
				}
			},
			scales: {
				yAxes: [{
					ticks: {
						min: 0
					}
				}]
			},
			plugins: {
				datalabels: {
					display: true,
					align: 'center',
					anchor: 'center'
				}
			},
		};
		//create Chart class object
		var chart = new Chart(ctx, {
			type: "bar",
			data: data,
			options: options
		});
	});


	RenderRadialChartForSeaPassageSpeed();
});

const max = 30
function valueToPercent(value) {
	return parseFloat((value * 100) / max).toFixed(2);
}

function PercentageToValue(value) {
	return parseFloat((max * value) / 100).toFixed(2);
}

function RenderRadialChartForSeaPassageSpeed() {
	var options = {
		series: [valueToPercent(19), valueToPercent(12.1), valueToPercent(5)],
		chart: {
			height: 400,
			type: 'radialBar',
		},
		plotOptions: {
			radialBar: {
				offsetY: -5,
				offsetX: 0,
				startAngle: -135,
				endAngle: 135,
				hollow: {
					margin: 3,
					size: '30%',
					background: 'transparent',
				},
				dataLabels: {
					name: {
						show: false,
					},
					value: {
						show: false,
					}
				}
			}
		},
		colors: ['#39539E', '#1ab7ea', '#0084ff'],
		labels: ['Charter Speed', 'Cht. Ballast', 'Actual'],
		legend: {
			show: true,
			floating: false,
			fontSize: '1px',
			position: 'bottom',
			horizontalAlign: 'center',
			fontFamily: 'Helvetica, Arial',
			offsetX: 0,
			offsetY: 30,
			labels: {
				useSeriesColors: true,
			},
			markers: {
				size: 0
			},
			formatter: function (seriesName, opts) {
				return seriesName + ":  " + PercentageToValue(opts.w.globals.series[opts.seriesIndex])
			},
			itemMargin: {
				vertical: 1,
				horizontal: 2
			}
		},
		title: {
			text: 'Speed',
			align: 'center',
			offsetX: 0,
			offsetY: 40,
			style: {
				fontSize: '11px',
				fontWeight: 'bold',
				color: '#263238'
			},
		},
		responsive: [{
			breakpoint: 480,
			options: {
				legend: {
					show: true,
					offsetY: 30,
					floating: false,
					fontSize: '1px',
					fontFamily: 'Helvetica, Arial',
					fontWeight: 20,
				},
				chart: {
					//offsetX: 150,
					offsetY: 5,
					//height: 110
				},
			}
		},
		{
			breakpoint: 0,
			options: {
				legend: {
					show: true,
					offsetX: 0,
					offsetY: 30,
					floating: false,
					fontSize: '1px',
					fontFamily: 'Helvetica, Arial',
					fontWeight: 20,
				},
				chart: {
					offsetX: 23//,
					//width: 140,
					//height: 150,
				}
			}
		}]
	};

	var chart = new ApexCharts(document.querySelector("#seaPassageSpeedRadialChart"), options);
	chart.render();
}