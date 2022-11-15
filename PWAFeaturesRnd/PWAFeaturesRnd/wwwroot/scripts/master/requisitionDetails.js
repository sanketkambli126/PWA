var gridSummary;

$(document).ready(function () {

	$('#OrderLineHeader').html('Order Lines(0)');

	if ($(".step-anchor li:last-child").hasClass('active')) {
		$("body").addClass("finish");
	}
	else {
		$("body").removeClass("finish");
	}

	var $radio = $('input[name="radio-position"]');
	var $listSelect = $('#list');
	var $portSelect = $("#port");

	$portSelect.prop('disabled', true);
	$radio.on('change', function (e) {
		if ($(this).val() === '1') {
			$portSelect.prop('disabled', true);
			$listSelect.prop('disabled', false)
		} else {
			$portSelect.prop('disabled', false);
			$listSelect.prop('disabled', true);
		}
	})

	$('.checkbox-sort').removeClass("sorting_asc");

	var data = [
		{
			"Notes": "",
			"ComponentName": "Main Engine",
			"PartName": "Compensator",
			"MakersReference": "98810-0039-085",
			"DrawingPosition": "Yes",
			"UOM": "PCS",
			"REC": "0",
			"ROB": "0",
			"REQ": "1",
			"QTY": "1",
			"LastPurchaseCost": "0"
		},
		{
			"Notes": "",
			"ComponentName": "Main Engine",
			"PartName": "Gasket",
			"MakersReference": "98810-0039-085",
			"DrawingPosition": "Yes",
			"UOM": "PCS",
			"REC": "0",
			"ROB": "0",
			"REQ": "1",
			"QTY": "1",
			"LastPurchaseCost": "0"
		},
		{
			"Notes": "",
			"ComponentName": "Main Engine",
			"PartName": "Butterfly Valve",
			"MakersReference": "98810-0039-085",
			"DrawingPosition": "Yes",
			"UOM": "PCS",
			"REC": "0",
			"ROB": "0",
			"REQ": "1",
			"QTY": "1",
			"LastPurchaseCost": "0"
		}
	]

	var dtInProc = $('#dtAddOrderLine').DataTable({
		"dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><t><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 50,
		"data": data,
		"order": [[2, "asc"]],
		"columnDefs": [
			{ "targets": [0], "orderable": false, "visible": true },
			{ "orderable": false, "width": "20px", "targets": 0 },
			{ "orderable": false, "width": "40px", "targets": 1 },
			{ "width": "150px", "targets": 2 },
			{ "width": "150px", "targets": 3 },
			{ "orderable": false, "width": "70px", "targets": 4 },
			{ "width": "40px", "targets": 5 },
			{ "width": "40px", "targets": 6 },
			{ "width": "40px", "targets": 7 },
			{ "width": "40px", "targets": 8 },
			{ "orderable": false, "width": "60px", "targets": 9 },
			{ "orderable": false, "width": "105px", "targets": 10 },
			{ "width": "110px", "targets": 11 },
		],
		"columns": [
			{
				className: "tdblock text-left",
				render: function (data, type, full, meta) { return GetCellData('', '<div class="position-relative form-check"><input name="check" id="exampleCheck" type="checkbox" class="form-check-input inside-checkbox" checked><label for="exampleCheck" class="form-check-label"></label></div>') }
			},
			{
				className: "tdblock text-center",
				render: function (data, type, full, meta) { return GetCellData('Notes', '<a href="#"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>'); }
			},
			{
				className: "tdblock",
				render: function (data, type, full, meta) { return GetCellData('Component Name', full.ComponentName + '<a href="#" data-toggle="modal" data-target="#component"><i class="fa fa-fw pull-right mt-1" aria-hidden="true" title=""></i></a>'); }
			},
			{
				className: "tdblock",
				render: function (data, type, full, meta) {
					return GetCellData('Part Name', '<div>' + full.PartName + '</div>' + full.MakersReference + '<i class="fa fa-fw red pull-right" aria-hidden="true" title=""></i>');
				}
			},
			{
				className: "tdblock",
				data: "DrawingPosition",
				render: function (data, type, full, meta) { return GetCellData('Drawing Position', data); }
			},
			{
				className: "text-center td-w-19",
				data: "UOM",
				render: function (data, type, full, meta) { return GetCellData('UOM', data); }
			},
			{
				className: "text-center td-w-19",
				data: "REC",
				render: function (data, type, full, meta) { return GetCellData('REC', data); }
			},
			{
				className: "text-center td-w-19",
				data: "ROB",
				render: function (data, type, full, meta) { return GetCellData('ROB', data); }
			},
			{
				className: "text-center td-w-19",
				data: "REQ",
				render: function (data, type, full, meta) { return GetCellData('REQ', data); }
			},
			{
				className: "text-center",
				render: function (data, type, full, meta) { return GetCellData('Pending Orders', '<a href="#" data-toggle="modal" data-target="#pendingorders"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>'); }
			},
			{
				render: function (data, type, full, meta) { return GetCellData('QTY', '<div class="input-group input-group-sm d-input"><div class="input-group-prepend"><span class="input-group-text btn btn-secondary">-</span></div><input placeholder="" type="text" class="form-control text-center" value="1"><div class="input-group-append"><span class="input-group-text btn btn-secondary">+</span></div></div>'); }
			},
			{
				className: "text-center d-none d-sm-table-cell",
				data: "LastPurchaseCost",
				render: function (data, type, full, meta) { return GetCellData('Last Purchase Cost (USD)', data); }
			}
		]
	});

	function GetCellData(label, data) {
		return '<label>' + label + '</label> <br />' + data;
	}

	gridSummary = $('#summary').DataTable({
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"language": {
			"infoEmpty": "No order line added"
		},
		"order": [[2, "asc"]],
		"columnDefs": [
			{ "targets": [0], "orderable": false, "visible": true },
		],
		"columns": [
			{
				className: "tdblock text-left",
				orderable: false,
				width: "20px",
				render: function (data, type, full, meta) { return GetCellData('', '<div class="position-relative form-check"><input name="check" id="exampleCheck" type="checkbox" class="form-check-input inside-checkbox" checked><label for="exampleCheck" class="form-check-label"></label></div>') }
			},
			{
				className: "tdblock text-center",
				orderable: false,
				width: "40px",
				render: function (data, type, full, meta) { return GetCellData('Notes', '<a href="#"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>'); }
			},
			{
				className: "tdblock",
				width: "150px",
				render: function (data, type, full, meta) { return GetCellData('Component Name', full.ComponentName + '<a href="#" data-toggle="modal" data-target="#component"><i class="fa fa-fw mt-1" aria-hidden="true" title=""></i></a>'); }
			},
			{
				className: "tdblock",
				width: "150px",
				render: function (data, type, full, meta) { return GetCellData('Part Name', '<div>' + full.PartName + '<i class="fa fa-fw red" aria-hidden="true" title=""></i></div> <label class="mt-3">Makers Reference</label> <br />' + full.MakersReference); }
			},
			{
				className: "tdblock",
				data: "DrawingPosition",
				width: "70px",
				render: function (data, type, full, meta) { return GetCellData('Drawing Position', data); }
			},
			{
				className: "text-center td-w-19",
				data: "UOM",
				width: "40px",
				render: function (data, type, full, meta) { return GetCellData('UOM', data); }
			},
			{
				className: "text-center td-w-19",
				data: "REC",
				width: "40px",
				render: function (data, type, full, meta) { return GetCellData('REC', data); }
			},
			{
				className: "text-center td-w-19",
				data: "ROB",
				width: "40px",
				render: function (data, type, full, meta) { return GetCellData('ROB', data); }
			},
			{
				className: "text-center td-w-19",
				data: "REQ",
				width: "40px",
				render: function (data, type, full, meta) { return GetCellData('REQ', data); }
			},
			{
				className: "text-center",
				width: "60px",
				render: function (data, type, full, meta) { return GetCellData('Pending Orders', '<a href="#" data-toggle="modal" data-target="#pendingorders"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>'); }
			},
			{
				width: "110px",
				render: function (data, type, full, meta) { return GetCellData('QTY', '<div class="input-group input-group-sm d-input"><div class="input-group-prepend"><span class="input-group-text btn btn-secondary">-</span></div><input placeholder="" type="text" class="form-control text-center" value="1"><div class="input-group-append"><span class="input-group-text btn btn-secondary">+</span></div></div>'); }
			},
			{
				className: "text-center d-none d-sm-table-cell",
				data: "LastPurchaseCost",
				width: "110px",
				render: function (data, type, full, meta) { return GetCellData('Last Purchase Cost (USD)', data); }
				//render: function (data, type, full, meta) { return '<span class="d-none d-sm-block">' + GetCellData('Last Purchase Cost (USD)', data); + '</span>' }
			}
		]
	});

	$('#btnAddOrderLine').click(function () {
		gridSummary.rows.add(data).draw();

		$('#OrderLineHeader').html('Order Lines(' + gridSummary.data().count() + ')');
	});
});