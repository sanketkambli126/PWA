import "@chenfengyuan/datepicker";
import "daterangepicker";
import moment from "moment";
var startDate, endDate;

import GMaps from "gmaps";
import { createTree } from "jquery.fancytree";

var data;
var gridPositionList;

$(document).ready(function () {

	var map = new GMaps({
		el: "#positionlistMap",
		lat: 11.774276,
		lng: 73.207846,
		width: "100%",
		height: "600px",
		zoom: 3,
		markerClusterer: function (map) {
			return new MarkerClusterer(map);
		}
	});

	var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';

	var features = [
		{
			position: new google.maps.LatLng(-9.889369, 81.645346),
			details: { id: "101", Name: "Sichem Amethyst", Status: "In Management", VesselType: "Chemical Tanker", Destination: "", Speed: "9.8 Knots", ETA: "", Heading: "270.3&deg;", ReplicationType: "Web Service", LastReported: "", LastReplicated: "" }
		},
		{
			position: new google.maps.LatLng(-14.866810, 75.317221),
			details: { id: "102", Name: "Sicehm Eagle" }
		},
		{
			position: new google.maps.LatLng(11.774276, 73.207846),
			details: { id: "103", Name: "Queen Esther" }
		},
		{
			position: new google.maps.LatLng(10.221350, 70.922690),
			details: { id: "104", Name: "Sichem Mumbai" }
		},
		{
			position: new google.maps.LatLng(9.528647, 74.262533),
			details: { id: "201", Name: "Sichem Etsher" }
		}
	];

	var markers = [];
	features.forEach(function (feature) {

		var marker = map.addMarker({
			position: feature.position,
			details: feature.details,
			label: {
				color: '#ffffff',
				fontWeight: 'bold',
				text: feature.details.Name,
				scaledSize: new google.maps.Size(32, 38),
				labelOrigin: new google.maps.Point(9, 9)
			},
			icon: {
				labelOrigin: new google.maps.Point(11, 50),
				url: '/images/icons/marker_red.png',
				size: new google.maps.Size(22, 40),
				origin: new google.maps.Point(0, 0),
				anchor: new google.maps.Point(11, 40),
			},
			click: function (e) {
				var infoWindow = new google.maps.InfoWindow({
					content: ('<div class="card d-none d-sm-block"> <div class="map-info"> <div class="pl-2"> <div class="card-title"><a href="/Vessel/VesselPositionList" class="text-teal-hover-black">Sichem Amethyst</a></div> <span class="card-subtitle"> In Management <span class="ml-2 border-left pl-2">Chemical Tanker (IMO I & II)</span> </span> </div> <div class="card-body p-0"> <ul class="list-group list-group-flush"> <li class="p-2 list-group-item">  <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Destination </td> <td>  </td> <td class="font-weight-bold" width="150px"> Speed (Reported) </td> <td> 9.8 Knots </td> </tr> <tr> <td class="font-weight-bold"> ETA </td> <td>  </td> <td class="font-weight-bold"> Heading (Reported) </td> <td> 270.3&deg; </td> </tr> <tr> <td class="font-weight-bold"> Last Reported </td> <td> <span> 30 Jul 2020 13:50 <i class="fa fa-info-circle ml-2" title="Last known Position information provided by polestar" data-toggle="tooltip" data-placement="bottom"></i> </span> </td> </tr> </table>  </li> <li class="p-2 list-group-item"> <div class="row"> <div class="col-6"> <div id="accordionCharter" class="mt-2"> <div id="charter" data-toggle="collapse" data-target="#collapseCharter" aria-expanded="true" aria-controls="collapseOne" class="text-left cursor-pointer mb-2"> <span class="card-title"> <i class="fa fa-chevron-up mr-3"></i> Charter </span> </div> <div data-parent="#accordionCharter" id="collapseCharter" aria-labelledby="charter" class="collapse show"> <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Average Speed </td> <td width="150px"> 2.07 knots </td> <td> </td> </tr> <tr> <td class="font-weight-bold"> Charter Speed </td> <td> <span>13.5 knots</span> </td> </tr> <tr> <td class="font-weight-bold"> Charter Fo </td> <td> <span>15 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Charter Go </td> <td> <span>15 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Average FO </td> <td> <span>8.5 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Average GO </td> <td> <span>1.29 mt</span> </td> </tr> </table> </div> </div> </div> <div class="col-6 border-left"> <div id="accordionWeather" class="mt-2"> <div id="weather" data-toggle="collapse" data-target="#collapseWeather" aria-expanded="true" aria-controls="collapseOne" class="text-left cursor-pointer mb-2"> <span class="card-title"> <i class="fa fa-chevron-up mr-3"></i> Weather </span> </div> <div data-parent="#accordionWeather" id="collapseWeather" aria-labelledby="weather" class="collapse show"> <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Air Pressure </td> <td width="150px"> 1005 mbar </td> <td> </td> </tr> <tr> <td class="font-weight-bold"> Wind Direction </td> <td> <span>223&deg;</span> </td> </tr> <tr> <td class="font-weight-bold"> Wind Speed </td> <td> <span>17.3 knots</span> </td> </tr> <tr> <td class="font-weight-bold"> Wave Direction </td> <td> <span>202&deg;</span> </td> </tr> <tr> <td class="font-weight-bold"> Wave Height </td> <td> <span>3 m</span> </td> </tr> <tr> <td class="font-weight-bold"> Forecast Time </td> <td> <span>30 Jul 2020 05:30 PM</span> </td> </tr> </table> </div> </div> </div> </div> </li> </ul> </div> </div> </div>' + '<div class="d-block d-sm-none"> <b> ' + e.details.Name + ' </b> <br /> ' + e.position.lat() + ', ' + e.position.lng() + ' </div>')
				});
				infoWindow.open(map, e);
			}
		});
	});

	map.markerClusterer = new MarkerClusterer(map, markers, { imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m' });

	data = [
		{
			"vessel": "Queen Esther",
			"country": "Turkey",
			"port": "Fasta, TUR",
			"activity": "Await Orders",
			"fromStatus": "EST",
			"fromDate": "27 Aug 2020 10:00",
			"toStatus": "EST",
			"toDate": "",
			"agentDetails": "",
			"expDeliveries": "2",
			"od": "2",
			"dueNow": "1",
			"signOn": "5",
			"signOff": "2",
			"portActivities": "3",
		},
		{
			"vessel": "Sichem Eagle",
			"country": "Bangladesh",
			"port": "Chittagong, BGD",
			"activity": "Await Orders",
			"fromStatus": "EST",
			"fromDate": "21 Aug 2020 10:00",
			"toStatus": "EST",
			"toDate": "",
			"agentDetails": "",
			"expDeliveries": "9",
			"od": "1",
			"dueNow": "3",
			"signOn": "4",
			"signOff": "5",
			"portActivities": "8",
		},
		{
			"vessel": "Sichem Esther",
			"country": "Turkey",
			"port": "Fasta, TUR",
			"activity": "Await Orders",
			"fromStatus": "EST",
			"fromDate": "15 Aug 2020 10:00",
			"toStatus": "EST",
			"toDate": "",
			"agentDetails": "",
			"expDeliveries": "2",
			"od": "2",
			"dueNow": "1",
			"signOn": "5",
			"signOff": "2",
			"portActivities": "3",
		},
		{
			"vessel": "Sichem Amethyst",
			"country": "Vietnam",
			"port": "Ho Chi Minh, VNM",
			"activity": "Await Orders",
			"fromStatus": "EST",
			"fromDate": "27 Aug 2020 10:00",
			"toStatus": "EST",
			"toDate": "",
			"agentDetails": "",
			"expDeliveries": "2",
			"od": "2",
			"dueNow": "1",
			"signOn": "5",
			"signOff": "2",
			"portActivities": "3",
		},
	]

	gridPositionList = $('#dtPositionList').DataTable({
		"dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><t><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
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
				className: "tdblock td-row-header",
				data: "vessel",
				render: function (data, type, full, meta) {
					return '<a href="/Vessel/VesselPositionList">' + full.vessel + '</a>';
				}
			},
			{
				className: "d-none d-sm-table-cell",
				data: "country",
				render: function (data, type, full, meta) { return GetCellData('Country', data); }
			},
			{
				className: "",
				data: "port",
				render: function (data, type, full, meta) { return GetCellData('Port', data); }
			},
			{
				className: "",
				data: "activity",
				render: function (data, type, full, meta) { return GetCellData('Activity', data); }
			},
			{
				className: "",
				"data": "fromStatus",
				render: function (data, type, full, meta) { return GetCellData('From Status', data); }
			},
			{
				className: "text-center",
				"data": "fromDate",
				render: function (data, type, full, meta) { return GetCellData('From Date', data); }
			},
			{
				className: "",
				"data": "toStatus",
				render: function (data, type, full, meta) { return GetCellData('To Status', data); }
			},
			{
				className: "text-center",
				"data": "toDate",
				render: function (data, type, full, meta) { return GetCellData('To Date', data); }
			},
			{
				className: "text-center td-w-27",
				"data": "agentDetails",
				render: function (data, type, full, meta) { return GetCellData('Agent Details', '<i class="fas fa-user-tie" data-toggle="modal" data-target="#agentDetails"></i>'); }
			},
			{
				className: "text-right td-w-27",
				"data": "expDeliveries",
				render: function (data, type, full, meta) { return GetCellData('Exp Deliveries', data); }
			},
			{
				className: "text-right td-w-27",
				"data": "portActivities",
				render: function (data, type, full, meta) {
					return GetCellData('Port Activities', '<a href="javascript:void(0);">' + full.portActivities + '</i></a>');
				}
			},
			{
				className: "tdblock d-block d-sm-none opacity-7 text-uppercase font-weight-bold",
				defaultContent: 'Certificates',
			},
			{
				className: "text-right remove-top-border",
				"data": "od",
				render: function (data, type, full, meta) {
					return GetCellData('OD', '<a href="javascript:void(0);">' + full.od + '</i></a>');
				}
			},
			{
				className: "text-right remove-top-border",
				"data": "dueNow",
				render: function (data, type, full, meta) {
					return GetCellData('Due Now', '<a href="javascript:void(0);">' + full.dueNow + '</i></a>');
				}
			},
			{
				className: "tdblock d-block d-sm-none opacity-7 text-uppercase font-weight-bold",
				defaultContent: 'Crew Lineup',
			},
			{
				className: "text-right remove-top-border",
				"data": "signOn",
				render: function (data, type, full, meta) { return GetCellData('Sign On', data); }
			},
			{
				className: "text-right remove-top-border",
				"data": "signOff",
				render: function (data, type, full, meta) { return GetCellData('Sign Off', data); }
			},

		]
	});

	function GetCellData(label, data) {
		return '<label>' + label + '</label> <br />' + data;
	}

	if (($(this).width() <= 767)) {
		$("#tab-list").addClass('active');
		$("#tab-map").removeClass('active');
	}

	$("#fleetTree").fancytree({
		"icon": false,
		source: [
			{
				"title": "Fleet", "folder": false, "children": [
					{ "title": "V.Ships glasgow" },
					{ "title": "Zodiac maritime Ltd." }
				]
			},
			{
				"title": "Favourites", "children": [
					{ "title": "Cargo" },
					{ "title": "Oil Tankers" }
				]
			},

		],
	});

	//daterangepicker
	var start = moment().subtract(6, "month");
	var end = moment();

	function cb(start, end) {
		startDate = start.format("DD MMM YYYY");
		endDate = end.format("DD MMM YYYY");
		$("#dtrpositionlist span").html(start.format("DD MMM YYYY") + " - " + end.format("DD MMM YYYY"));
	}

	$("#dtrpositionlist").daterangepicker(
		{
			startDate: start,
			endDate: end,
			opens: "right",
			locale: {
				format: 'DD MMM YYYY'
			},
			ranges: {
				Today: [moment(), moment()],
				Yesterday: [moment().subtract(1, "days"), moment().subtract(1, "days")],
				"Last 7 Days": [moment().subtract(6, "days"), moment()],
				"Last 30 Days": [moment().subtract(29, "days"), moment()],
				"This Month": [moment().startOf("month"), moment().endOf("month")],
				"Last Month": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")],
				"Last 6 Month": [moment().subtract(6, "month"), moment()]
			},
			locale: {
				format: "DD MMM YYYY"
			}
		},
		cb
	);

	cb(start, end);
});