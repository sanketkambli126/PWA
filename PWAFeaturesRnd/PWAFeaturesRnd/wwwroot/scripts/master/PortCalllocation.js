$(document).ready(function () {
	var data = [
		{
			"event": "End Of Sea Passage (EOSP)",
			"from": "23 Apr 2020 02:30",
			"to": "23 Apr 2020 04:42",
			"elapsedtime": "02:12",
			"specifics": "0",
			"distance": "0.00",
			"fo": "0.00",
			"isfo": "0.00",
			"do": "0.00",
			"go": "0.00",
			"lng": "0.00",
			"fwdom": "0.00",
			"fwtech": "0.00",
			"docs": "",
			"comments": "",
		},
		{
			"event": "Other Event",
			"from": "23 Apr 2020 02:30",
			"to": "23 Apr 2020 04:42",
			"elapsedtime": "02:12",
			"specifics": "0",
			"distance": "0.00",
			"fo": "0.00",
			"isfo": "0.00",
			"do": "0.00",
			"go": "0.00",
			"lng": "0.00",
			"fwdom": "0.00",
			"fwtech": "0.00",
			"docs": "",
			"comments": "",
		},
		{
			"event": "NOR Tendered",
			"from": "23 Apr 2020 02:30",
			"to": "23 Apr 2020 04:42",
			"elapsedtime": "02:12",
			"specifics": "0",
			"distance": "0.00",
			"fo": "0.00",
			"isfo": "0.00",
			"do": "0.00",
			"go": "0.00",
			"lng": "0.00",
			"fwdom": "0.00",
			"fwtech": "0.00",
			"docs": "<a href='javascript:void(0);' class='text-black' data-toggle='modal' data-target='#eventsdoc'><i class='fa fa-fw' aria-hidden='true'></i></a>",
			"comments": "<a href='javascript:void(0);' class='text-black' data-toggle='tooltip' data-placement='bottom' title='NOR RE-TENDERED.' data-html='true'><i class='fa fa-fw' aria-hidden='true'></i></a>",
		},
		
	]

	var portcalllocation = $('#dtportcalllocation').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 50,
		"data": data,
		"columns": [
			{
				className: "text-left tdblock td-row-header",
				data: "event",
				width: "180px",
				//render: function (data, type, full, meta) { return GetCellData('Event', data); }
			},
			{
				className: "text-left",
				data: "from",
				width: "115px",
				render: function (data, type, full, meta) { return GetCellData('From', data); }
			},
			{
				className: "text-left",
				data: "to",
				width: "115px",
				render: function (data, type, full, meta) { return GetCellData('To', data); }
			},
			{
				className: "text-left td-w-43",
				data: "elapsedtime",
				width: "100px",
				render: function (data, type, full, meta) { return GetCellData('ElapsedTime', data); }
			},
			{
				className: "text-left td-w-22",
				"data": "specifics",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Specifics', data); }
			},
			{
				className: "text-left td-w-19",
				"data": "distance",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Distance', data); }
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
				className: "text-center",
				"data": "docs",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('DOCS', data); }
			},
			{
				className: "text-center",
				"data": "comments",
				width: "80px",
				render: function (data, type, full, meta) { return GetCellData('Comments', data); }
			},
		]
	});

	//events docs list modal data

	var docsdata = [
		{
			"docs": "",
			"createdate": "26 Oct 2020",
			"title": "Test File",
			"createddate": "27 Mar 2020",
			"type": "General Documentation",
			"description": "",
		},
	]
	var docslist = $('#dtdocs').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"data": docsdata,
		"columns": [
			{
				className: "text-left",
				data: "docs",
				render: function (data, type, full, meta) { return GetCellData('Docs','<i class="fa fa-fw" aria-hidden="true"></i>'); }
			},
			{
				className: "text-left",
				data: "createdate",
				render: function (data, type, full, meta) { return GetCellData('Created Date', data); }
			},
			{
				className: "text-left",
				data: "title",
				render: function (data, type, full, meta) { return GetCellData('Title', data); }
			},
			{
				className: "text-left",
				data: "type",
				render: function (data, type, full, meta) { return GetCellData('Type', data); }
			},
			{
				className: "text-left tdblock",
				data: "description",
				render: function (data, type, full, meta) { return GetCellData('Description', data); }
			}
		]
	});

	function GetCellData(label, data) {
		return '<label>' + label + '</label> <br />' + data;
	}
});