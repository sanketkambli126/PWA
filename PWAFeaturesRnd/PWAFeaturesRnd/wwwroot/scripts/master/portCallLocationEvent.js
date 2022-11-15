import * as JSZip from "jszip";
window.JSZip = JSZip;
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import moment from "moment";

require('bootstrap');

import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, headerReadMore, GetCookie, ToastrAlert, SetHeaderMargin, BackButton, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, RegisterTabSelectionEvent, RemoveClassIfPresent, IsNullOrEmptyOrUndefinedLooseTyped, AddClassIfAbsent, IsNullOrEmptyOrUndefined, base64ToArrayBuffer, saveByteArray, IsNullOrEmpty, ConvertDecimalNumberToString, headerReadMoreMulti } from "../common/utilities.js"
import { GetCellData, GetExportCellData, GetExportData, GetExportFormattedDecimal, GetFormattedDate, GetExportFormattedDateTime, GetFormattedDecimal } from "../common/datatablefunctions.js"
import { PortCallEventPageKey, MobileScreenSize, Yes, No, InBound, OutBound } from "../common/constants.js"
import { RecordLevelNote } from "../common/notesUtilities.js"

var encryptedVesselDetail, vesselName, listURL, dtEventsList, dtEventsDocuments, dtPopEventsDocuments;
var specificsColumn = 4, delayColumn = 5, offHireColumn = 6, lopColumn = 7, badWeatherColumn = 8, distanceColumn = 9, fOColumn = 10, lsfoColumn = 11, doColumn = 12, goColumn = 13, lngColumn = 14, fwDomColumn = 16, fwTechColumn = 17, docsIconColumn = 18, docsColumn = 19, commentsIconColumn = 20, commentsColumn = 21,lngCargoColumn = 15;

$(window).on('resize', SetHeaderMargin);
var defaultValue = "0.000";
var key = CryptoJS.enc.Utf8.parse('8080808080808080')
var iv = CryptoJS.enc.Utf8.parse('8080808080808080')
var code = (function () {

    return {
        encryptMessage: function (messageToencrypt = '', secretkey = '') {
            var encryptedMessage = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(messageToencrypt), key, { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
            return encryptedMessage.toString();
        },
        decryptMessage: function (encryptedMessage = '', secretkey = '') {
            var decryptedBytes = CryptoJS.AES.decrypt(encryptedMessage, secretkey);
            var decryptedMessage = decryptedBytes.toString(CryptoJS.enc.Utf8);
            return decryptedMessage;
        }
    }
})();

$(document).ready(function () {
    AjaxError();

    var val = $('#IsFromViewRecord').val();
    if (val == 'True' || val == 'true' || val == true) {
        if ($(window).width() > 767) {
            $('body').addClass("hideleftmenuheader");
        }
        else {
            $('.app-container .logo-src, .app-container .aBaseNotification, .app-container .mobile-toggle-header-nav, .mobile-header-back, .vesseldropdownmobile').hide();
            $('.app-header__mobile-menu .header-dots').css("visibility", "hidden");
            RemoveClassIfPresent('.backclose', 'd-none');
        }
    }
    $('.backclose').click(function () {
        window.close();
    });

    if (($(window).width() > 767)) {
        $('.height-equal-box').matchHeight({
            byRow: 0
        });

    }
    if (screen.width > 767 && screen.width < 1025 ) {
        $('.height-equal5').matchHeight({
            byRow: 0
        });
    }


    $('#modelDelay').on('shown.bs.modal', function (e) {
        if (screen.width < 760) {
            headerReadMoreMulti('headerDelayedshowmorewrapper', 'headerDelayed');
        }
        $(".modal-body .scroller").scrollTop(0);
    });

    $('#modelManoeuvring').on('shown.bs.modal', function (e) {
        if (screen.width < 760) {
            headerReadMoreMulti('headerManoeuvringShowmorewrapper', 'headerManoeuvring');
        }
        $(".modal-body .scroller").scrollTop(0);
    });

    $('#modelEospView').on('shown.bs.modal', function (e) {
        if (screen.width < 760) {
            headerReadMoreMulti('headerEospShowmorewrapper', 'headerEosp');
        }
        $(".modal-body .scroller").scrollTop(0);
    });



    AddLoadingIndicator();
    RemoveLoadingIndicator();

    encryptedVesselDetail = $('#EncryptedVesselDetail').val();
    listURL = $('#ListURL').val();
    BackButton(PortCallEventPageKey, false);
    LoadVesselPreview();
    LoadPortCallDetails();
    LoadVoyageActivityDetail();
    LoadPortCallHeader();
    LoadPortCallLocationEvents();
    RegisterTabSelectionEvent('.mobileTabClick', PortCallEventPageKey);
    //allvesseldata list data
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }


    var allspecficsdata = [
        {
            "type": '<img src="/images/DashboardIcons/first-aid-voyage.png"/> <span class="medical-specifics align-middle"> Medical Incident</span>',
            "from": 'Sun, Apr 26, 23:46:00',
            "to": "Mon, Apr 27, 00:30:00",
            "reason": "Reason 1",
            "offhire": "-",
            "offhiretype": "-",
            "comments": "Captain hit his head and sufferred brief blackout. First aid was administered and he has now recovered",
        },
        {
            "type": '<img src="/images/DashboardIcons/clock-orange.png"/> <span class="align-middle"> Delay</span>',
            "from": 'Sun, Apr 26, 23:47:00',
            "to": "Mon, Apr 27, 23:59:00",
            "reason": "Reason 2",
            "offhire": "-",
            "offhiretype": "-",
            "comments": "Captain hit his head and sufferred brief blackout. First aid was administered and he has now recovered",
        }
    ];
    var specifics = $('#dtspecifics').DataTable({
        "processing": true,
        "order": [[0, "desc"]],
        "serverSide": false,
        "lengthChange": false,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "data": allspecficsdata,
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "type",
                width: "116px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-text-align",
                data: "from",
                width: "90px",
                render: function (data, type, full, meta) { return GetCellData('From', data); }
            },
            {
                className: "data-text-align",
                data: "to",
                width: "94px",
                render: function (data, type, full, meta) { return GetCellData('To', data); }
            },
            {
                className: "data-text-align",
                data: "reason",
                width: "50px",
                render: function (data, type, full, meta) { return GetCellData('Reason', data); }
            },
            {
                className: "data-text-align",
                data: "offhire",
                width: "44px",
                render: function (data, type, full, meta) { return GetCellData('Off Hire', data); }
            },
            {
                className: "data-text-align",
                data: "offhiretype",
                width: "73px",
                render: function (data, type, full, meta) { return GetCellData('Off Hire Type', data); }
            },
            {
                className: "data-text-align tdblock",
                data: "comments",
                width: "245px",
                render: function (data, type, full, meta) { return GetCellData('Comments', data); }
            }
        ]
    });

    var data = [
        {
            "docs": "<img src='/images/Download-doc-active.png' width='13' title='Download'>",
            "cdate": "25 May 2022",
            "title": "user-test",
            "type": "General Documentation",
            "desc": ""
        }
    ];

    var attach = $('#dtattach').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        'orderable': false,
        "paging": false,
        "data": data,
        "order": [],
        "columns": [

            {
                className: "data-icon-align",
                'orderable': false,
                'targets': 0,
                "data": "docs",
                "width": "35px",
                render: function (data, type, full, meta) { return GetCellData('Docs', data); }
            },
            {
                className: "data-datetime-align",
                "data": "cdate",
                'orderable': true,
                "width": "70px",
                render: function (data, type, full, meta) { return GetCellData('Created Date', data); }
            },
            {
                className: "data-text-align",
                "data": "title",
                "width": "55px",
                render: function (data, type, full, meta) { return GetCellData('Title', data); }

            },
            {
                className: "data-text-align",
                "data": "type",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            },
            {
                className: "data-text-align",
                "data": "desc",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Description', data); }
            },
        ],
        "initComplete": function (settings, json) {
            if (($(window).width() > 767)) {
                $('.height-equal2').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    var data = [
        {
            "oil": "Prod. Qty.",
            "clo": "0.000",
            "case": "0.000",
            "aux": "0.000",
            "general": "0.000"
        },
        {
            "oil": "Consumption	 Qty.",
            "clo": "0.000",
            "case": "0.000",
            "aux": "0.000",
            "general": "0.000"
        }
    ];



    var data = [
        {
            "fuelc": "ME Consumption",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lng": "0.000",
            "cargo": "0.000"
        },
        {
            "fuelc": "Cargo heat/ cool",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lng": "0.000",
            "cargo": "0.000"
        },
        {
            "fuelc": "Boiler",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lng": "0.000",
            "cargo": "0.000"
        },
        {
            "fuelc": "DG Cosnumption",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lng": "0.000",
            "cargo": "0.000"
        },
        {
            "fuelc": "Other",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lng": "0.000",
            "cargo": "0.000"
        },
        {
            "fuelc": "Total Cosnumption",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lng": "0.000",
            "cargo": "0.000"
        }
    ];



    var data = [
        {
            "oil": "Prod. Qty.",
            "clo": "0.000",
            "case": "0.000",
            "aux": "0.000",
            "general": "0.000"
        },
        {
            "oil": "Consumption	 Qty.",
            "clo": "0.000",
            "case": "0.000",
            "aux": "0.000",
            "general": "0.000"
        }
    ];

    var datac = [
        {
            "docs": "<img src='/images/Download-doc-active.png' width='13' title='Download'>",
            "cdate": "25 May 2022",
            "title": "user-test",
            "type": "General Documentation",
            "desc": ""
        },
        {
            "docs": "<img src='/images/Download-doc-active.png' width='13' title='Download'>",
            "cdate": "26 May 2022",
            "title": "user-test5",
            "type": "LOP",
            "desc": ""
        },
        {
            "docs": "<img src='/images/Download-doc-active.png' width='13' title='Download'>",
            "cdate": "27 May 2022",
            "title": "user-test4",
            "type": "General Documentation",
            "desc": ""
        },
        {
            "docs": "<img src='/images/Download-doc-active.png' width='13' title='Download'>",
            "cdate": "28 May 2022",
            "title": "user-test3",
            "type": "General Documentation",
            "desc": ""
        },
        {
            "docs": "<img src='/images/Download-doc-active.png' width='13' title='Download'>",
            "cdate": "29 May 2022",
            "title": "user-test2",
            "type": "LOP",
            "desc": ""
        }
    ];

    var attachcharter = $('#dtattachcharter').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        'orderable': false,
        "paging": false,
        "data": datac,
        "order": [],
        "columns": [

            {
                className: "data-icon-align",
                'orderable': false,
                'targets': 0,
                "data": "docs",
                "width": "15px",
                render: function (data, type, full, meta) { return GetCellData('Docs', data); }
            },
            {
                className: "data-datetime-align",
                "data": "cdate",
                'orderable': true,
                "width": "45px",
                render: function (data, type, full, meta) { return GetCellData('Created Date', data); }
            },
            {
                className: "data-text-align",
                "data": "title",
                "width": "55px",
                render: function (data, type, full, meta) { return GetCellData('Title', data); }

            },
            {
                className: "data-text-align",
                "data": "type",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            },
            {
                className: "data-text-align",
                "data": "desc",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Description', data); }
            },
        ],
        "initComplete": function (settings, json) {
            if (($(window).width() > 767)) {
                $('.height-equal3').matchHeight({
                    byRow: 0
                });
            }
        },
    });


    var data = [
        {
            "fuel": "",
            "hfo": "0.000",
            "lfo": "0.000",
            "do": "0.000",
            "go": "0.000",
            "lngbunker": "0.000",
        }

    ];

    var fuels = $('#dtfuel').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": data,
        "columns": [
            {
                className: "data-text-align boldheader tdblock",
                'orderable': false,
                "data": "fuel",
                'targets': 0,
                "width": "45px",
                render: function (data, type, full, meta) { return GetCellData('Fuel', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "hfo",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('HFO(mt)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "lfo",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('LFO (mt)', data); }

            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "do",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('DO (mt)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "go",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('GO (mt)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "lngbunker",
                "width": "90px",
                render: function (data, type, full, meta) { return GetCellData('LNG Bunker (mt)', data); }
            },
        ],
        "initComplete": function (settings, json) {
            if (screen.width > MobileScreenSize) {
                $('.height-equal4').matchHeight({
                    byRow: 0
                });
            }
        },
    });


    var data2 = [
        {
            "title": "",
            "robclo": "0.000",
            "crank": "0.000",
            "aux": "0.000",
            "general": "0.000",
        },

    ];

    var lubeoils = $('#dtlubeoil2').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": data2,
        "columns": [
            {
                className: "data-text-align boldheader tdblock",
                'orderable': false,
                "data": "title",
                'targets': 0,
                "width": "56px",
                render: function (data, type, full, meta) { return GetCellData('Lube Oil', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "robclo",
                "width": "64px",
                render: function (data, type, full, meta) { return GetCellData('CLO (m3)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "crank",
                "width": "72px",
                render: function (data, type, full, meta) { return GetCellData('Crank Case (m3)', data); }

            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "aux",
                "width": "62px",
                render: function (data, type, full, meta) { return GetCellData('Aux (m3)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "general",
                "width": "70px",
                render: function (data, type, full, meta) { return GetCellData('General (m3)', data); }
            },
        ],
        "initComplete": function (settings, json) {
            if (screen.width > MobileScreenSize) {
                $('.height-equal4').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    var data = [
        {
            "title": "",
            "sludge": "0.000",
            "bilge": "0.000",
            "slops": "0.000",
            "sewage": "0.000",
        },

    ];

    var waste = $('#dtwaste').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": data,
        "columns": [
            {
                className: "data-text-align boldheader tdblock",
                'orderable': false,
                "data": "title",
                'targets': 0,
                "width": "45px",
                render: function (data, type, full, meta) { return GetCellData('Waste', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "sludge",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Sludge (m3)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "bilge",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Bilge (m3)', data); }

            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "slops",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Slops (m3)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "sewage",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Sewage (m3)', data); }
            },
        ],
        "initComplete": function (settings, json) {
            if (screen.width > MobileScreenSize) {
                $('.height-equal4').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    var data = [
        {
            "title": "",
            "domestic": "0.000",
            "tech": "0.000",
        },

    ];

    var fresh = $('#dtFreshWater').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": data,
        "columns": [
            {
                className: "data-text-align boldheader tdblock",
                'orderable': false,
                "data": "title",
                'targets': 0,
                "width": "65px",
                render: function (data, type, full, meta) { return GetCellData('Fresh Water', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "domestic",
                "width": "81px",
                render: function (data, type, full, meta) { return GetCellData('Domestic (mt)', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "tech",
                "width": "80px",
                render: function (data, type, full, meta) { return GetCellData('Technical (mt)', data); }

            },
        ],
        "initComplete": function (settings, json) {
            if (screen.width > MobileScreenSize) {
                $('.height-equal4').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    var data = [
        {
            "cargo": "Clean",
            "qty": "0.000 (mt)"
        },
        {
            "cargo": "Dry",
            "qty": "0.000 (mt)"
        },
    ];

    var fresh = $('#dtcargo').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "data": data,
        "columns": [
            {
                className: "data-text-align boldheader tdblock",
                'orderable': false,
                "data": "cargo",
                'targets': 0,
                "width": "70px",
                render: function (data, type, full, meta) { return GetCellData('Ballast Water', data); }
            },
            {
                className: "data-number-align",
                'orderable': false,
                "data": "qty",
                "width": "70px",
                render: function (data, type, full, meta) { return GetCellData('Quantity', data); }
            },
        ],
        "initComplete": function (settings, json) {
            if (screen.width > MobileScreenSize) {
                $('.height-equal4').matchHeight({
                    byRow: 0
                });
            }
        },
    });



    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);
    GetDiscussionNotesCount(messageDetailsJSON);
    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);
});

function LoadVesselPreview() {

    $.ajax({
        url: "/VoyageReporting/GetVesselPreview",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedVesselDetail": encryptedVesselDetail
        },
        success: function (data) {
            if (data != null) {
                $('#spanVesselName').text(data.name);
                vesselName = data.name;
                $('#spanImoNumber').text(data.imo);
                var vesselToolTip = 'Type: ' + data.type + '<br/>Build Date: ' + data.vesselBuiltDate + '<br/>Age: ' + data.vesselAge;
                $('#infoVesselDetails').attr("data-original-title", vesselToolTip);
            }
        }
    });
}

function LoadPortCallDetails() {

    $.ajax({
        url: "/VoyageReporting/GetPortCallDetail",
        type: "POST",
        "data": {
            "input": listURL
        },
        "datatype": "JSON",
        success: function (result) {
            var data = result.data;
            $('#spanPort').text(data.port);
            $('#spanChartererName').text(data.charterName);
            if (data.chtCompanyCode != null) {
                $('#spanCharterNumber').text(data.charterNumber);
            }
            else {
                $('#spanCharterNumber').hide();
            }
            $('#spanVoyageNumber').text(data.voyageNumber);

            $('#spanCargoOperationTime').text(data.cargoOperationTime);
            $('#spanTimeOnBerth').text(data.berthTime);
            $('#spanOffHire').text(data.outofserviceTime);
            $('#spanTimeInPort').text(data.totalTime);

            $('#labelEosp').text(data.eospDateHeader);
            $('#spanEosp').text(data.eosp);
            $('#labelBerthed').text(data.berthDateHeader);
            $('#spanBerthed').text(data.berthed);
            $('#labelUnBerthed').text(data.unBerthDateHeader);
            $('#spanUnBerthed').text(data.unBerthed);
            $('#labelFaop').text(data.faopDateHeader);
            $('#spanFaop').text(data.faop);

            if (($(window).width() > 767)) {
                $('.height-equal-box').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function LoadVoyageActivityDetail() {

    $.ajax({
        url: "/VoyageReporting/GetVoyageActivityDetail",
        type: "POST",
        "data": {
            "input": listURL
        },
        "datatype": "JSON",
        success: function (result) {
            var data = result.data;
            $('#spanActivityDescription').text(data.activityDescription);
            if (data.hasPortAgent == false) {
                $('#spanAgentDetails').hide();
            }
        },
        complete: function () {
            SetHeaderMargin();
        }
    });
}

function LoadPortCallHeader() {

    $.ajax({
        url: "/VoyageReporting/GetPortCallHeader",
        type: "POST",
        "data": {
            "input": listURL
        },
        "datatype": "JSON",
        success: function (result) {
            var data = result.data;
            $('#spanLastReportedEvent').text(data.lastUpdatedDate);
        }
    });
}

function LoadPortCallLocationEvents() {

    $('#dtEventsList').DataTable().destroy();
    dtEventsList = $('#dtEventsList').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-11 offset-md-1 col-lg-10 offset-lg-1 col-xl-10 offset-xl-12 dt-infomation"i><""f>><"table-horizontal-scroll"rt><""<""l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": true,
        "paging": false,
        "order": [],
        "pageLength": 10,
        "language": {
            "emptyTable": "No events available.",
        },
        "ajax": {
            "url": "/VoyageReporting/GetPortCallLocationEvents",
            "type": "POST",
            "data":
            {
                "input": listURL
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "tdblock",
                data: "eventName",
                width: "180px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var value = data;

                        value = GetExportData(value);

                        if (full.isInComplete) {
                            value += "<i class='ml-1 fa fa-exclamation-circle text-danger' data-toggle='tooltip' title='' data-placement='bottom' data-original-title='Incomplete " + data + "'></i>"
                        }
                        if (full.viewName == "AddEditPortEventDelayDetailsView" || full.viewName == "AddEditEospView" || full.viewName == "AddEditManoeuvringView")
                            return '<a class="showReportPopup" href="javascript:void(0)"><span class="eventDetails">' + value + '</span></a>';
                        else
                            return value;
                    }

                    if (full.viewName == "AddEditPortEventDelayDetailsView" || full.viewName == "AddEditEospView" || full.viewName == "AddEditManoeuvringView")
                        return '<a class="showReportPopup" href="javascript:void(0)"><span class="eventDetails" >' + data + '</span></a>';
                    else
                        return data;
                }
            },
            {
                className: "data-datetime-align total",
                data: "fromDate",
                width: "115px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDateTime(type, 'From', data);
                }
            },
            {
                className: "data-datetime-align hide-footer-column",
                data: "toDate",
                width: "115px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDateTime(type, 'To', data);
                }
            },
            {
                className: "data-datetime-align hide-footer-column",
                data: "elapsedTime",
                width: "100px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        if (data != null) {

                            return GetExportCellData('Elapsed Time', full.totalElapsedHours + ":" + full.totalElapsedMinutes);
                        }
                        return GetCellData('Elapsed Time', "");
                    }
                    return data;
                }
            },
            {
                className: "data-icon-align hide-footer-column",
                "data": "isLop",
                width: "80px",
                orderable: false,
                render: function (data, type, full, meta) {
                    var value = '';
                    if (full.isLop) {
                        value += '<span class="material-symbols-sharp text-grey text-grey desciconportcall mr-1" aria-hidden="true" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="LOP" title="LOP" >description</span>';
                    }
                    if (full.isOffHire) {
                        value += '<img src="../images/vessel-black-icon.png" width="15" class="mr-1 float-none" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="Off-Hire" title="Off-Hire">';
                    }
                    if (full.isBadWeather) {
                        value += '<img src="../images/DashboardIcons/cloud-black.png" class="mr-1 float-none" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="Bad Weather" title="Bad Weather">';
                    }
                    if (full.isDelay) {
                        value += '<img src="../images/DashboardIcons/clock-black.png" class="mr-1 float-none" data-html="true" data-toggle="tooltip" data-placement="bottom" data-original-title="Delay" title="Delay">';
                    }
                    return GetCellData('Specifics', value);
                }
            },
            {
                data: "isDelay",
                render: function (data, type, full, meta) {
                    var result = '';
                    if (data == true) {
                        result = 'Yes';
                    }
                    return GetExportData(result);
                }
            },
            {
                data: "isOffHire",
                render: function (data, type, full, meta) {
                    var result = '';
                    if (data == true) {
                        result = 'Yes';
                    }
                    return GetExportData(result);
                }
            },
            {
                data: "isLop",
                render: function (data, type, full, meta) {
                    var result = '';
                    if (data == true) {
                        result = 'Yes';
                    }
                    return GetExportData(result);
                }
            },
            {
                data: "isBadWeather",
                render: function (data, type, full, meta) {
                    var result = '';
                    if (data == true) {
                        result = 'Yes';
                    }
                    return GetExportData(result);
                }
            },
            {
                className: "data-number-align",
                "data": "distance",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'Distance', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "totalFo",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'HFO', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "totalLsfo",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'LFO', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "totalDo",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'DO', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "totalGo",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'GO', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "totalLng",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'LNG BUNKER', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "totalLngCargo",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'CARGO BOIL OFF', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "fwDomestic",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'FW DOM', data, 2, '0.00');
                }
            },
            {
                className: "data-number-align",
                "data": "fwTechnical",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDecimal(type, 'FW TECH', data, 2, '0.00');
                }
            },
            {
                className: "data-icon-align hide-footer-column",
                "data": "hasDocuments",
                width: "80px",
                orderable: false,
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        if (data == true) {

                            return GetCellData('DOCS', "<i class='fa fa-paperclip eventsDocuments cursor-pointer' aria-hidden='true' data-toggle='modal' data-target='#modalEventsDocument'></i>");
                        }
                        else {
                            return GetCellData('DOCS', '');
                        }
                    }
                    return data;
                }
            },
            {
                "data": "hasDocuments",
                width: "80px",
                orderable: false,
                render: function (data, type, full, meta) {
                    return GetExportData('');
                }
            },
            {
                className: "data-icon-align hide-footer-column",
                "data": "comments",
                width: "80px",
                orderable: false,
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        if (data != null) {
                            return GetCellData('Comments', "<img src='/images/notes.svg' width='12' class='eventsComments cursor-pointer'  data-toggle='modal' data-target='#modalEventsComments'>");
                        }
                        else {
                            return GetCellData('Comments', "");
                        }
                    }
                    return data;
                }
            },
            {
                "data": "comments",
                width: "80px",
                render: function (data, type, full, meta) {
                    var value = ''
                    if (data != null) {
                        value = data;
                    }
                    return GetExportData(value);
                }
            },
        ],
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
                    var sheet = xlsx.xl.worksheets['sheet1.xml'];
                    $('row c[r*="2"]', sheet).attr('s', '55'); //Wrap test style set to 2nd row. Need to set \n for new lilne in messageTop.

                    // height set to 2nd row. need to change the height as per the content in header.
                    $('row* ', sheet).each(function (index) {
                        if (index == 1) {
                            $(this).attr('ht', 15);
                            $(this).attr('customHeight', 1);
                        }
                    });
                },
                title: "Port Call Events Details",
                messageTop: function () {
                    return 'Vessel : ' + vesselName;
                }
            },
            'pdf', 'print'
        ],
        "initComplete": function (settings, json) {
            $('[data-toggle="tooltip"]').tooltip();
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api(), data;

            // converting to interger to find total
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            // computing column Total of the complete result 
            var distanceTotal = api
                .column(distanceColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var foTotal = api
                .column(fOColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var lsfoTotal = api
                .column(lsfoColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var doTotal = api
                .column(doColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var goTotal = api
                .column(goColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var lngTotal = api
                .column(lngColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            var lngCargoTotal = api
                .column(lngCargoColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var fwDomTotal = api
                .column(fwDomColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            var fwTechTotal = api
                .column(fwTechColumn)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);

            // Update footer by showing the total with the reference of the column index 
            $(api.column(1).footer()).html('Total');
            $(api.column(distanceColumn).footer()).html(GetExportFormattedDecimal('display', 'Distance', distanceTotal, 2, '0.00'));
            $(api.column(fOColumn).footer()).html(GetExportFormattedDecimal('display', 'HFO', foTotal, 2, '0.00'));
            $(api.column(lsfoColumn).footer()).html(GetExportFormattedDecimal('display', 'LFO', lsfoTotal, 2, '0.00'));
            $(api.column(doColumn).footer()).html(GetExportFormattedDecimal('display', 'DO', doTotal, 2, '0.00'));
            $(api.column(goColumn).footer()).html(GetExportFormattedDecimal('display', 'GO', goTotal, 2, '0.00'));
            $(api.column(lngColumn).footer()).html(GetExportFormattedDecimal('display', 'LNG', lngTotal, 2, '0.00'));
            $(api.column(lngCargoColumn).footer()).html(GetExportFormattedDecimal('display', 'LNG CARGO', lngCargoTotal, 2, '0.00'));
            $(api.column(fwDomColumn).footer()).html(GetExportFormattedDecimal('display', 'FW DOM', fwDomTotal, 2, '0.00'));
            $(api.column(fwTechColumn).footer()).html(GetExportFormattedDecimal('display', 'FW TECH', fwTechTotal, 2, '0.00')); 
        },
    });


    //table scroll
    if (($(window).width() > 767)) {
        var newWidth = ($(".table-scroll-width").width());
        $(".table-common-design .table-horizontal-scroll").css({
            "maxWidth": newWidth
        });
    }

    $('.table-footer-design tfoot tr td.total').each(function () {
        if ($('td:contains("Total")')) {
            $('.table-footer-design tfoot tr td.total').addClass('total-common-block');
        }
    });

    dtEventsList.columns([delayColumn, offHireColumn, lopColumn, badWeatherColumn, docsColumn, commentsColumn]).visible(false);

    $("div.ExportToExcel").html('<button data-toggle="tooltip" id="btnExportEventsList" title="Export to excel" data-placement="bottom" class="btn btn-dark btn-shadow font-size-lg p-1 hover-blue float-right"> <i class="fa fa-fw" aria-hidden="true"></i></button>');

    $('#btnExportEventsList').click(() => {
        var searchValue = dtEventsList.search();
        dtEventsList.search("").draw();

        dtEventsList.columns([delayColumn, offHireColumn, lopColumn, badWeatherColumn, docsColumn, commentsColumn]).visible(true);
        dtEventsList.columns([specificsColumn, docsIconColumn, commentsIconColumn]).visible(false);

        $('#dtEventsList.cardview thead').addClass("export-grid-show");
        $('#dtEventsList').DataTable().buttons(0, 2).trigger();
        $('#dtEventsList.cardview thead').removeClass("export-grid-show");

        dtEventsList.columns([delayColumn, offHireColumn, lopColumn, badWeatherColumn, docsColumn, commentsColumn]).visible(false);
        dtEventsList.columns([specificsColumn, docsIconColumn, commentsIconColumn]).visible(true);

        dtEventsList.search(searchValue).draw();
    });

    $('#dtEventsList tbody').on('click', 'i.eventsDocuments', function () {
        var data = dtEventsList.row($(this).parents('tr')).data();
        OpenModalEventsDocuments(data);
    });

    $('#dtEventsList tbody').on('click', 'img.eventsComments', function () {
        var data = dtEventsList.row($(this).parents('tr')).data();
        $('#spanEventsTitle').text('Comments');
        $('#spanEventsComments').text(data.comments);
    });
}

function OpenModalEventsDocuments(input) {

    $('#spanEventsName').text(input.eventName);

    var fromDate = input.fromDate;
    var formattedFromDate = "";
    if (fromDate != null && fromDate != '' && fromDate != undefined) {
        var date = new Date(fromDate);
        formattedFromDate = moment(date).format("D MMM YYYY HH:mm");
    }
    $('#spanFromDate').text(formattedFromDate);

    var toDate = input.toDate;
    var formattedToDate = "";
    if (toDate != null && toDate != '' && toDate != undefined) {
        var date = new Date(toDate);
        formattedToDate = moment(date).format("D MMM YYYY HH:mm");
    }
    $('#spanToDate').text(formattedToDate);

    $('#dtEventsDocuments').DataTable().destroy();
    dtEventsDocuments = $('#dtEventsDocuments').DataTable({
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "scrollY": "150px",
        "paging": false,
        "order": [],
        "language": {
            "emptyTable": "No documents available.",
        },
        "ajax": {
            "url": "/VoyageReporting/GetDocumentDetails",
            "type": "POST",
            "data":
            {
                "input": input.documentRequestUrl
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-icon-align top-margin-0",
                data: "docs",
                width: "60px",
                orderable: false,
                render: function (data, type, full, meta) { return GetCellData('Docs', '<a class="documentDownload cursor - pointer"><img src="/images/Download-doc-active.png" class="m-0 align-top" width="13" title="Download Attachment"/>'); }
            },
            {
                className: "data-datetime-align top-margin-0",
                data: "createdOn",
                width: "120px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Created Date', data); }
            },
            {
                className: "data-text-align tdblock",
                data: "title",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Title', data); }
            },
            {
                className: "data-text-align",
                data: "type",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            },
            {
                className: "data-icon-align",
                data: "description",
                width: "60px",
                orderable: false,
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        if (data) {
                            return GetCellData('Description', "<i class='fa fa-file-alt docsDescription cursor-pointer' aria-hidden='true' data-toggle='modal' data-target='#modalDocumentsDescription'></i>");
                        }
                        else {
                            return GetCellData('Description', "");
                        }
                    }
                    return data;
                }
            }
        ],
        "initComplete": function (settings, json) {
            $('#spanAttachmentsCount').text(dtEventsDocuments.data().count());
        },
    });

    $('#dtEventsDocuments tbody').on('click', 'a.documentDownload', function () {
        var data = dtEventsDocuments.row($(this).parents('tr')).data();
        DownloadSelectedAttachment(data);
    });

    $('#dtEventsDocuments tbody').on('click', 'i.docsDescription', function () {
        var data = dtEventsDocuments.row($(this).parents('tr')).data();
        $('#spanDocumentsDescription').text(data.description);
        $("#modalDocumentsDescription .modal-content").addClass("modalcommentsborder");
    });
    $(document).on('click', '#modalDocumentsDescription .close', function () {
        $("#modalDocumentsDescription .modal-content").removeClass("modalcommentsborder");
    });
    $('#modalDocumentsDescription').on('hidden.bs.modal', function () {
        $('body').addClass('modal-open');
    })
}




$("#dtEventsList").on('click', 'tbody tr span.eventDetails', function () {
    var data = dtEventsList.row($(this).parents('tr')).data();
    var input = {
        PsfId: data.psfId,
        PosId: data.posId,
        PpfId: data.ppfId,
        encryptedVesselDetail: $('#EncryptedVesselDetail').val()
    }

    if (data.viewName == "AddEditPortEventDelayDetailsView") {
        LoadPortEventAndRobDetails(input, data.eventName);// EventId or psfId
        LoadEventsDocuments(data);
        $('#modelDelay').modal('show');
    }
    else if (data.viewName == "AddEditEospView") {
        LoadPortEospEventAndRobDetails(input, data.eventName);
        $('#modelEospView').modal('show');
    }
    else if (data.viewName == "AddEditManoeuvringView") {
        LoadPortEventManoeuvringDetails(input, data.eventName);
        $('#modelManoeuvring').modal('show');
    }
});


function LoadPortEventAndRobDetails(input, EventName) {

    $.ajax({
        url: "/VoyageReporting/GetPortEventAndRobDetails",
        type: "POST",
        "data": {
            "input": input
        },
        "datatype": "JSON",
        success: function (result) {
            var data = result.data;
            if (!IsNullOrEmptyOrUndefined(data)) {
                $('#popEventName').text(EventName);
                $('#popVesselName').text(vesselName);
                var fromDate = data.fromDate;
                var formattedFromDate = "";
                if (fromDate != null && fromDate != '' && fromDate != undefined) {
                    var date = new Date(fromDate);
                    formattedFromDate = moment(date).format("D MMM YYYY HH:mm");
                }
                $('#popFromDate').text(formattedFromDate);
                var toDate = data.toDate;
                var formattedToDate = "";
                if (toDate != null && toDate != '' && toDate != undefined) {
                    var date = new Date(toDate);
                    formattedToDate = moment(date).format("D MMM YYYY HH:mm");
                }
                $('#popToDate').text(formattedToDate);

                $('#popDistance').text(data.totalDistance);
                $('#popSecurityLevel').text(data.securityLevel);
                $('#popOffHire').text(data.isOffHire == true ? Yes : No);
                $('#popOffHireType').text(data.offHireType);
                $('#popLOP').text(data.isLopIssued == "1" ? Yes : No);
                $('#popHours').text(data.duration);
                $('#popComments').text(data.comment);

                BindDynamicTable("#tblLubeOilThead", "#tblLubeOilTbody", data.lubOilBreakDown, data.lubOilBreakDownNameList, "", true)
                BindDynamicTable("#tblFuelConsumptionThead", "#tblFuelConsumptionTbody", data.fuelRobBreakDown, data.fuelRobBreakDownNameList, "", true)

                if (screen.width > 1025) {
                    $('.height-equal1').matchHeight({
                        byRow: 0
                    });
                }
            }
        }
    });
}

function LoadPortEospEventAndRobDetails(input, EventName) {

    $.ajax({
        url: "/VoyageReporting/GetPortEospEventAndRobDetails",
        type: "POST",
        "data": {
            "input": input
        },
        "datatype": "JSON",
        success: function (response) {
            if (!IsNullOrEmptyOrUndefined(response)) {

                $('#EospEventName').text(EventName);
                $('#EospVesselName').text(vesselName);
                var fromDate = response.fromDate;
                var formattedFromDate = "";
                if (fromDate != null && fromDate != '' && fromDate != undefined) {
                    var date = new Date(fromDate);
                    formattedFromDate = moment(date).format("D MMM YYYY HH:mm");
                }
                $('#EospFromDate').text(formattedFromDate);
                $('#EospSecurityLevel').text(response.securityLevel);
                $('#EospLOP').text(response.isLopIssued == "1" ? Yes : No);
                $('#EospComment').text(response.comment);

                $('#spanEospForeDraft').text(ConvertDecimalNumberToString(response.forwardDraft, 3, 1, 3));
                $('#spanEospMidDraft').text(ConvertDecimalNumberToString(response.midDraft, 3, 1, 3) );
                $('#spanEospAftDraft').text(ConvertDecimalNumberToString(response.aftDraft, 3, 1, 3) );
                $('#spanEospMeanDraft').text(ConvertDecimalNumberToString(response.meanDraft, 3, 1, 3) );

                BindDynamicTable("#tblEospFuelConsumptionThead", "#tblEospFuelConsumptionTbody", response.fuelRobBreakDown, response.fuelRobBreakDownNameList, "", false);
                BindDynamicTable("#tblEospLubeOilThead", "#tblEospLubeOilTbody", response.lubOilBreakDown, response.lubOilBreakDownNameList, "", false);
                BindDynamicTable("#tblEospWasteThead", "#tblEospWasteTbody", response.wasteRobBreakDown, response.wasteRobBreakDownNameList, "", false);
                BindDynamicTable("#tblEospFreshWaterThead", "#tblEospFreshWaterTbody", response.freshWaterRobBreakDown, response.freshWaterBreakDownNameList, "", false);
                BindBlastRobDynamicTable("#tblEospBlastThead", "#tblEospBlastTbody", response.ballastDetails, null);
                LoadEventRunningHours(response.engineRunningHours, "dtEosprunninghours", "spanEospRunnigpowerCount");

                if (screen.width > 1025) {
                    var height = $('#runninghours').outerHeight();
                    var height2 = $('#vesseldraft').outerHeight();
                    $("#comments").css({
                        "height": height - height2 - 15
                    });
                }
            }
        }
    });
}

function LoadEventsDocuments(input) {

    $('#dtPopEventsDocuments').DataTable().destroy();
    dtPopEventsDocuments = $('#dtPopEventsDocuments').DataTable({
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "scrollY": "95px",
        "scrollX": true,
        "scrollCollapse": true,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "language": {
            "emptyTable": "No documents available.",
        },
        "ajax": {
            "url": "/VoyageReporting/GetDocumentDetails",
            "type": "POST",
            "data":
            {
                "input": input.documentRequestUrl
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-icon-align top-margin-0",
                data: "docs",
                width: "25px",
                orderable: false,
                render: function (data, type, full, meta) { return GetCellData('Docs', '<a class="documentDownload cursor - pointer"><img src="/images/Download-doc-active.png" class="m-0" width="13" title="Download Attachment"/>'); }
            },
            {
                className: "data-datetime-align top-margin-0",
                data: "createdOn",
                width: "60px",
                render: function (data, type, full, meta) { return GetFormattedDate(type, 'Created Date', data); }
            },
            {
                className: "data-text-align tdblock",
                data: "title",
                width: "80px",
                render: function (data, type, full, meta) { return GetCellData('Title', data); }
            },
            {
                className: "data-text-align",
                data: "type",
                width: "80px",
                render: function (data, type, full, meta) { return GetCellData('Type', data); }
            },
            {
                className: "data-icon-align",
                data: "description",
                width: "70px",
                orderable: false,
                render: function (data, type, full, meta) {
                    var result = "<img src='/images/notes.svg' width='12' class='DownloadDecription cursor-pointer' data-toggle='modal' data-target='#modalEventsComments'>";
                    return GetCellData('Description', result);
                }
            }
        ],
        "initComplete": function (settings, json) {
            $('#popSpanAttachCount').text(dtPopEventsDocuments.data().count());
            if (($(window).width() > 767)) {
                $('.height-equal2').matchHeight({
                    byRow: 0
                });
            }
        },
    });

    $('#dtPopEventsDocuments tbody').on('click', 'a.documentDownload', function () {
        var data = dtPopEventsDocuments.row($(this).parents('tr')).data();
        DownloadSelectedAttachment(data);
    });
    $('#dtPopEventsDocuments tbody').on('click', 'img.DownloadDecription', function () {
        var data = dtPopEventsDocuments.row($(this).parents('tr')).data();
        $('#spanEventsTitle').text('Description');
        $('#spanEventsComments').text(data.description);
        $("#modalEventsComments .modal-content").addClass("modalcommentsborder");
    });
    $(document).on('click', '#modalEventsComments .close', function () {
        $("#modalEventsComments .modal-content").removeClass("modalcommentsborder");
    });
}

function DownloadSelectedAttachment(selectedItem) {

    var documentId = (!IsNullOrEmptyOrUndefined(selectedItem.ettId) && selectedItem.ettId != undefined) ? selectedItem.ettId.trim() : '';
    var documentFileName = (!IsNullOrEmptyOrUndefined(selectedItem.cloudFileName) && selectedItem.cloudFileName != undefined) ? selectedItem.cloudFileName.trim() : '';
    var documentCategory = (!IsNullOrEmptyOrUndefined(selectedItem.documentCategory) && selectedItem.documentCategory != undefined) ? selectedItem.documentCategory : '';
    var input = {
        "identifier": documentId,
        "fileName": documentFileName,
        "documentCategory": documentCategory
    };
  //  var fileName = documentFileName;
    var fileName = selectedItem.title.trim();
    $.ajax({
        url: "/VoyageReporting/DownloadDocument",
        type: "POST",
        dataType: "JSON",
        global: false,
        data: {
            "input": JSON.stringify(input)
        },
        success: function (data) {
            if (data.bytes != null) {
                var array = base64ToArrayBuffer(data.bytes);
                saveByteArray(fileName, array, data.fileType);
            } else {
                ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
            }
        }
    });
}

function BindDynamicTable(theadSelector, tbodySelector, result, nameList, firstThValue, isBindNamList) {

    if (!IsNullOrEmpty(result) && result.length > 0) {
        var rows = result[0].robDetails.length;
        var cols = result.length;

        $(theadSelector).html("");
        $(tbodySelector).html("");

        var row = '<tr>';

        if (isBindNamList) {
            row += '<th>' + firstThValue + '</th>';
        }

        for (var inner = 0; inner < cols; inner++) {
            row += '<th>' + result[inner].title + '</th>';
        }
        row += '</tr>';
        $(theadSelector).append(row);

        for (var outter = 0; outter < rows; outter++) {
            var row = '<tr>';

            if (!IsNullOrEmpty(nameList) && nameList.length > 0 && isBindNamList) {
                row += '<td class="tdblock">' + nameList[outter + 1] + '</td>';
            }
            //else {
            //    row += '<td class="tdblock">' + GetCellData(firstThValue, "") + '</td>';
            //}

            for (var inner = 0; inner < cols; inner++) {
                row += '<td class="data-number-align">' + GetFormattedDecimal("display", result[inner].title, result[inner].robDetails[outter].value, 3, "0.000") + '</td>';
            }
            row += '</tr>';
            $(tbodySelector).append(row);
        }
    }
}

function BindBlastRobDynamicTable(theadSelector, tbodySelector, result, nameList) {

    if (!IsNullOrEmpty(result) && result.length > 0) {
        var rows = result[0].robDetails.length;
        var cols = result.length;

        $(tbodySelector).html("");

        if (screen.width > MobileScreenSize) {
            for (var inner = 0; inner < cols; inner++) {
                var row = '<tr>';
                row += '<td class="tdblock d-none d-md-block">' + result[inner].title + '</td>';
                row += '<td class="data-number-align">' + GetFormattedDecimal("display", result[inner].title, result[inner].robDetails.value, 3, "-") + '</td>';
                row += '</tr>';
                $(tbodySelector).append(row);
            }
        }
        else {
            //this is for mobile
            var row = '<tr>';
            for (var inner = 0; inner < cols; inner++) {
                row += '<td class="data-number-align">' + GetFormattedDecimal("display", result[inner].title, result[inner].robDetails.value, 3, "-") + '</td>';
            }
            row += '</tr>';
            $(tbodySelector).append(row);
        }
    }
}

function LoadEventRunningHours(data, dtTableName, runningCount) {
    $('#' + dtTableName).DataTable().destroy();
    var runninghours = $('#' + dtTableName).DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "400px",
        "scrollX": true,
        "scrollCollapse": true,
        "order": [],
        "orderable": false,
        "data": data,
        "columns": [
            {
                className: "data-text-align",
                "data": "partName",
                "width": "60px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetCellData('Components', IsNullOrEmpty(data) ? "" : data); }
            },
            {
                className: "data-number-align",
                "data": "previousHours",
                "width": "40px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Past Hrs', data, 1, "0"); }

            },
            {
                className: "data-number-align",
                "data": "total",
                "width": "40px",
                "orderable": false,
                render: function (data, type, full, meta) { return GetFormattedDecimal(type, 'Current Reading', data, 1, "0"); }
            },
        ],
        "initComplete": function (settings, json) {
            $('#' + runningCount).text(this.api().data().length);
            if (screen.width > MobileScreenSize) {
                $('.height-equal4').matchHeight({
                    byRow: 0
                });
            }
        },
    });
}

function LoadPortEventManoeuvringDetails(input, EventName) {

    $.ajax({
        url: "/VoyageReporting/GetPortEventManoeuvringDetails",
        type: "POST",
        "data": {
            "input": input
        },
        "datatype": "JSON",
        success: function (result) {
            var data = result;
            if (!IsNullOrEmptyOrUndefined(data)) {
                $('#manoEventName').text(EventName);
                $('#manoVesselName').text(vesselName);

                var fromDate = data.fromDate;
                var formattedFromDate = "";
                if (fromDate != null && fromDate != '' && fromDate != undefined) {
                    var date = new Date(fromDate);
                    formattedFromDate = moment(date).format("D MMM YYYY HH:mm");
                }
                $('#manoFromDate').text(formattedFromDate);
                var toDate = data.toDate;
                var formattedToDate = "";
                if (toDate != null && toDate != '' && toDate != undefined) {
                    var date = new Date(toDate);
                    formattedToDate = moment(date).format("D MMM YYYY HH:mm");
                }
                $('#manoToDate').text(formattedToDate);

                $('#manoDistance').text(ConvertDecimalNumberToString(data.totalDistance, 2, 1, 2) );
                $('#manoeuvring').text(data.isInBound == true ? InBound : OutBound);
                $('#manoSecurityLevel').text(data.securityLevel);
                $('#manoOffHire').text(data.isOffHire == true ? Yes : No);
                $('#manoOffHireType').text(data.offHireType);
                $('#manoLOP').text(data.isLopIssued == "1" ? Yes : No);
                //  $('#manoHours').text(data.duration);
                $('#manoComments').text(data.comment);
                $('#spanManoForeDraft').text(ConvertDecimalNumberToString(data.forwardDraft, 3, 1, 3));
                $('#spanManoMidDraft').text(ConvertDecimalNumberToString(data.midDraft, 3, 1, 3));
                $('#spanManoAftDraft').text(ConvertDecimalNumberToString(data.aftDraft, 3, 1, 3));
                $('#spanManoMeanDraft').text(ConvertDecimalNumberToString(data.meanDraft, 3, 1, 3));


                BindDynamicTable("#tblManoLubeOilThead", "#tblManoLubeOilTbody", data.lubOilBreakDown, data.lubOilBreakDownNameList, "", true);
                BindDynamicTable("#tblManoFuelConsumptionThead", "#tblManoFuelConsumptionTbody", data.fuelRobBreakDown, data.fuelRobBreakDownNameList, "", true);
                BindDynamicTable("#tblManoWasteThead", "#tblManoWasteTbody", data.wasteRobBreakDown, data.wasteRobBreakDownNameList, "", true);
                BindDynamicTable("#tblManoFreshWaterThead", "#tblManoFreshWaterTbody", data.freshWaterRobBreakDown, data.freshWaterBreakDownNameList, "", true);
                LoadEventRunningHours(data.engineRunningHours, "dtManorunninghours", "spanManoRunnigpowerCount");

                if (screen.width > MobileScreenSize) {
                    $('.rowSection1').matchHeight({
                        byRow: 0
                    });
                    $('.rowSection2').matchHeight({
                        byRow: 0
                    });
                    $('.rowSection3').matchHeight({
                        byRow: 0
                    });
                }

            }
        }
    });
}




