// Form Wizard

import "smartwizard";

$(document).ready(() => {
    setTimeout(function () {
        
        $("#smartwizardChangeOrderStatus").smartWizard({
            selected: 0,
            transitionEffect: "slide",
            toolbarSettings: {
                toolbarPosition: "none",
            },
        });

        // External Button Events
        $("#reset-btnChangeOrderStatus").on("click", function () {
            // Reset wizard
            $("#smartwizardChangeOrderStatus").smartWizard("reset");
            return true;
        });

        $("#prev-btnChangeOrderStatus").on("click", function () {
            // Navigate previous
            $("#smartwizardChangeOrderStatus").smartWizard("prev");
            return true;
        });

        $("#next-btnChangeOrderStatus").on("click", function () {
            // Navigate next
            $("#smartwizardChangeOrderStatus").smartWizard("next");
            return true;
        });

    }, 2000);

	function GetCellData(label, data) {
		return '<label>' + label + '</label><br />' + data;
	}

	var data = [
		{
			"companyName": "A &Z Shipping Agency",
			"address": "Gemlik Office: Hisar Mah.Yalova Yolu No.20 Gemlik/Bursa/Turkiye AOH: +90 530 876 0197 gemlik Turkey ",
			"telephone": "+90 224 512 3375",
			"fax": "+90 224 512 3375 ",
			"email": "gemlik@azshipping.com",
		},
	]

	var gridPositionList = $('#dtAvailableDeliveryAddress').DataTable({
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
				data: "companyName",
			},
			{
				className: "tdblock",
				data: "address",
				render: function (data, type, full, meta) { return GetCellData('Address', data); }
			},
			{
				className: "",
				data: "telephone",
				render: function (data, type, full, meta) { return GetCellData('Telephone', data); }
			},
			{
				className: "",
				data: "fax",
				render: function (data, type, full, meta) { return GetCellData('Fax', data); }
			},
			{
				className: "tdblock",
				"data": "email",
				render: function (data, type, full, meta) { return GetCellData('Email', data); }
			},
		]
	});

	$('[data-toggle="datepicker-icon-deliveryDate"]').datepicker({
		trigger: ".datepicker-trigger-deliveryDate",
	});

});
