$(document).ready(function () {

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
    
    var dtInProc = $('#dtorderlines').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><t><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": true,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "pageLength": 50,
        "order": [[2, "asc"]],
        columnDefs: [
            { "targets": [0], "orderable": false, "visible": true },
            { "orderable": false, "width": "5px", "targets": 0 },
            { "orderable": false, "width": "40px", "targets": 1 },
            { "width": "150px", "targets": 2 },
            { "width": "150px", "targets": 3 },
            { "width": "90px", "targets": 4 },
            { "orderable": false, "width": "70px", "targets": 5 },
            { "width": "40px", "targets": 6 },
            { "width": "40px", "targets": 7 },
            { "width": "40px", "targets": 8 },
            { "width": "40px", "targets": 9 },
            { "orderable": false, "width": "60px", "targets": 10 },
            { "orderable": false, "width": "105px", "targets": 11 },
            { "width": "110px", "targets": 12 },
        ],
        
    });

    var dtInProc = $('#summary').DataTable({
        "processing": true,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [[2, "asc"]],
        columnDefs: [
            { "targets": [0], "orderable": false, "visible": true },
            { "orderable": false, "width": "5px", "targets": 0 },
            { "orderable": false, "width": "40px", "targets": 1 },
            { "width": "150px", "targets": 2 },
            { "width": "150px", "targets": 3 },
            { "width": "90px", "targets": 4 },
            { "orderable": false, "width": "70px", "targets": 5 },
            { "width": "40px", "targets": 6 },
            { "width": "40px", "targets": 7 },
            { "width": "40px", "targets": 8 },
            { "width": "40px", "targets": 9 },
            { "orderable": false, "width": "60px", "targets": 10 },
            { "orderable": false, "width": "105px", "targets": 11 },
            { "width": "110px", "targets": 12 },
        ],

    });
});
