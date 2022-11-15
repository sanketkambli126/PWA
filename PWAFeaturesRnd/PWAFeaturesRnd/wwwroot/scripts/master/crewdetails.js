import "bootstrap";
import { AjaxError, ErrorLog, AddLoadingIndicator, base64ToArrayBuffer, GetCookie, FormatDate, RemoveLoadingIndicator, saveByteArray, ToastrAlert, saveOpenByteArray, BackButton, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, ReplaceClass, RegisterTabSelectionEvent, RemoveClassIfPresent  } from "../common/utilities.js";
import { GetFormattedDecimal } from "../common/datatablefunctions.js";
import { MobileScreenSize, CrewDetailsPageKey, DropdownTab3 } from "../common/constants.js";
import { RecordLevelNote } from "../common/notesUtilities.js"

var crewId, crewName, dtCertificatesAndDocs, crewRank, reportFormatType;

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
    $('.height-equal').matchHeight();
    crewId = $("#hdnCrewId").val();

    AddLoadingIndicator();
    RemoveLoadingIndicator();
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
    //Sidebar back
    BackButton(CrewDetailsPageKey, false)

    fetchCrewDetails(crewId);
    fetchCrewCurrentDetails(crewId);
    fetchCrewImage(crewId);
    fetchServiceHistory(crewId, false);

    $("#dropdowntablist .dropdown-tab-3, #tabs .certificatetablink").click(function () {
        fetchCertificatesAndDocs(crewId);
        $("#dropdowntablist .dropdown-tab-3, #tabs .certificatetablink").off('click');
    });

    $.fn.dataTable.ext.search.push(
        function (settings, searchData, index, rowData, counter) {
            if (settings.nTable.id == 'dtcertificates') {
                var allowed = false;
                if ($("#isValid").is(':checked')) {
                    allowed |= rowData.isValid;
                }
                if ($("#isExpired").is(':checked')) {
                    allowed |= rowData.isExpired;
                }
                if ($("#isExpiring").is(':checked')) {
                    allowed |= rowData.isExpiring;
                }
                return allowed;
            }
            return true;
        }
    );

    $.fn.dataTable.ext.oSort["exp-asc"] = function (a, b) {
        var aDays = a.split(",")[1].split("D")[0].trim();
        var aMonths = a.split(",")[0].split("M")[0].trim();
        var aDaysInt = parseInt(aDays);
        var aMonthsInt = parseInt(aMonths);

        var bDays = b.split(",")[1].split("D")[0].trim();
        var bMonths = b.split(",")[0].split("M")[0].trim();
        var bDaysInt = parseInt(bDays);
        var bMonthsInt = parseInt(bMonths);

        if ((aMonthsInt - bMonthsInt) == 0) {
            return aDaysInt - bDaysInt;
        } else {
            return aMonthsInt - bMonthsInt;
        }
    }

    $.fn.dataTable.ext.oSort["exp-desc"] = function (a, b) {
        var aDays = a.split(",")[1].split("D")[0].trim();
        var aMonths = a.split(",")[0].split("M")[0].trim();
        var aDaysInt = parseInt(aDays);
        var aMonthsInt = parseInt(aMonths);

        var bDays = b.split(",")[1].split("D")[0].trim();
        var bMonths = b.split(",")[0].split("M")[0].trim();
        var bDaysInt = parseInt(bDays);
        var bMonthsInt = parseInt(bMonths);

        if ((aMonthsInt - bMonthsInt) == 0) {
            return bDaysInt - aDaysInt;
        } else {
            return bMonthsInt - aMonthsInt;
        }
    }

    $('#isValid, #isExpiring, #isExpired').on('click', function () {
        dtCertificatesAndDocs.draw();
    });

    $('#chkIncludeShore').change(function () {
        if (this.checked) {
            fetchServiceHistory(crewId, true);
        }
        else {
            fetchServiceHistory(crewId, false);
        }
    });

    $('#btnAdvDownload').on('click', function () {
        showAdvDowloadGrid();
    });

    $('#btnAdvCancel').on('click', function () {
        hideAdvDowloadGrid();
    });

    $('#btnDownloadSelection').on('click', function () {
        fetchAttachments();
    });

    $('#AdvDownSelectAll').on('click', function (e) {

        if (this.checked) {
            $('input[type="checkbox"].select-checkbox-all:not(:checked)').trigger('click');
        } else {
            $('input[type="checkbox"].select-checkbox-all').trigger('click');
        }
    });

    $('#isGrouping').on('click', function () {

        var inputValue = $(this).attr("value");
        $("." + inputValue).toggle();
        dtCertificatesAndDocs.draw();

        //if (this.checked) {
        //    var inputValue = $(this).attr("value");
        //    $("." + inputValue).toggle();
        //    dtCertificatesAndDocs.draw();
        //} else {
        //    //$('#dtcertificates > tbody  > tr.group-start').each(function (index, tr) {
        //    //    var name = $(this).data('name');
        //    //    var statusId = $(this).data('id');
        //    //    $('*[data-name="' + name + '_toggle"]').show();
        //    //    $('#togglestatus_' + statusId).text('-');
        //    //});

        //    //var inputValue = $(this).attr("value");
        //    //$("." + inputValue).toggle();
        //    dtCertificatesAndDocs.draw();
        //}

    });

    RegisterTabSelectionEvent('.mobileTabClick', CrewDetailsPageKey);
    var MobilTabCls = $("#ActiveMobileTabClass").val();

    if (($(window).width() < MobileScreenSize)) {
        $('#dropdowntablist .' + MobilTabCls)[0].click();
        var selectedTabElement = $('#dropdowntablist .' + MobilTabCls)[0]
        if (!($(selectedTabElement).hasClass('modal-click'))) {
            $(".dropdowntabtitle").html($(selectedTabElement).text());
            $('#dropdowntablist li').removeClass("active");
            $(selectedTabElement).parent().addClass('active')
        }
    }
    else {
        if (MobilTabCls == DropdownTab3) {
            $('#tabs .certificatetablink')[0].click();
        }
    }

    LoadCvDefaultDetails();

    $('.btnOpenCreateCv').click(function () {
        $('#modalCreateCv').modal('show');
    });

    $('input[type=radio][name=reportType]').change(function () {
        var reportType = $('input[name="reportType"]:checked').val();
        if (reportType == 'Default') {
            $('.chkCvOption').attr("disabled", true);
            $("#chkIsIncludeAddress").prop('checked', false);
            $("#chkIsIncludeContact").prop('checked', false);
            $("#chkIsIncludeBioData").prop('checked', true);
            $("#chkIsIncludePicture").prop('checked', true);    
            $("#chkIsIncludeSummaryOfCompetence").prop('checked', true);
            $("#chkIsIncludeOnShoreHistory").prop('checked', false);
            $("#chkIsIncludeServiceDetails").prop('checked', true);
            $("#chkIsIncludeDocuments").prop('checked', true);
            $("#chkIsIncludeNotes").prop('checked', false);
            $("#chkIsIncludeMedicalRecords").prop('checked', false);
        }
        else if (reportType == 'Custom') {
            $('.chkCvOption').attr("disabled", false);
        }
    });

    $('#btnCreateCv').click(function () {
        var input = {
            "EncryptedCrewId": crewId,
            "ReportFormatType": reportFormatType,
            "CrewRank": crewRank,
            "CrewName": crewName,
            "ReportFormat": $('#selFormat').val(),
            "IsIncludeAddress": $('#chkIsIncludeAddress').is(':checked'),
            "IsIncludeContact": $('#chkIsIncludeContact').is(':checked'),
            "IsIncludePicture": $('#chkIsIncludePicture').is(':checked'),
            "IsIncludeSummaryOfCompetence": $('#chkIsIncludeSummaryOfCompetence').is(':checked'),
            "IsIncludeOnShoreHistory": $('#chkIsIncludeOnShoreHistory').is(':checked'),
            "IsIncludeServiceDetails": $('#chkIsIncludeServiceDetails').is(':checked'),
            "IsIncludeDocuments": $('#chkIsIncludeDocuments').is(':checked'),
            "IsIncludeNotes": $('#chkIsIncludeNotes').is(':checked'),
            "IsIncludeMedicalRecords": $('#chkIsIncludeMedicalRecords').is(':checked')
        }

        $.ajax({
            url: "/Crew/CreateCV",
            type: "POST",
            data: {
                "input": input
            },
            success: function (data) {
                if (data.success) {
                    ToastrAlert("success", data.message);
                    $('#modalCreateCv').modal('hide');
                }
                else {
                    ToastrAlert("error", data.message);
                }
            }
        });
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);

    GetDiscussionNotesCount(messageDetailsJSON, function () {
        var selectedMobileDropdown = $('.mobile-tab.mobile-dropdown-tab li').not('.d-none');
        if (selectedMobileDropdown.length == 5) {
            ReplaceClass("#dropdowntablist", "tab-4-text", "tab-5-text");
        }
        if (selectedMobileDropdown.length == 6) {
            ReplaceClass("#dropdowntablist", "tab-5-text", "tab-6-text");
        }
    });
    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);
    AjaxError();
});

function fetchServiceHistory(input, includeShoreFlag) {
    $('#dtservicehistory').DataTable().destroy();
    var dtServiceHistory = $('#dtservicehistory').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-3 col-xl-7 offset-xl-2 dt-infomation"i><"col-12 col-md-5"f>><t><"clearfix"<"float-left"l><""p>>>',
        "processing": true,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "pageLength": 10,
        "ajax": {
            "url": "/Crew/GetServiceHistory",
            "type": "POST",
            "data":
            {
                "identifier": input,
                "canIncludeShore": includeShoreFlag
            },
            "datatype": "json",
        },
        "order": [[6, "desc"]],
        "columns": [
            {
                className: "tdblock data-text-align",
                width: "200px",
                "data": "vesselName",
                render: function (data, type, full, meta) {
                    //return GetCellData('Vessel Name', data);
                    return '<span class="export-Data">' + data + '</span>';
                }
            },
            {
                className: "data-icon-align onboard-bg",
                "data": "status",
                width: "75px",
                render: function (data, type, full, meta) {
                    if (full.serviceActiveStatusId == 3) {
                        return GetCellData('Status', '<span class="badge " style="background-color:' + full.planningBackGroundColor + ';color:' + full.planningForegroundColor + ';" data-toggle="tooltip" data-placement="bottom" title="' + full.planningStatusDescription + '">' + full.planningStatusShortCode + '</span>');
                    }
                    else {
                        return GetCellData('Status', '<span data-toggle="tooltip" data-placement="bottom" title="' + data + '">' + data + '</span>')
                    }
                }
            },
            {
                className: "data-text-align",
                width: "75px",
                "data": "rank",
                render: function (data, type, full, meta) {
                    return GetCellData('Rank', data);
                }
            },
            {
                className: "data-text-align",
                width: "200px",
                "data": "vesselType",
                render: function (data, type, full, meta) {
                    return GetCellData('Vessel Type', data);
                }

            },
            {
                className: "data-text-align",
                width: "50px",
                "data": "flag",
                render: function (data, type, full, meta) {
                    return GetCellData('Flag', data);
                }
            },
            {
                className: "data-number-align",
                width: "50px",
                "data": "dwt",
                render: function (data, type, full, meta) {
                    return GetCellData('DWT', new Intl.NumberFormat('en', {
                        style: 'decimal',
                        useGrouping: true
                    }).format(data));
                }
            },
            {
                className: "data-datetime-align",
                width: "100px",
                "data": "startDate",
                "type": "date",
                render: function (data, type, full, meta) {
                    return GetCellData('Start Date', FormatDate(data));
                }
            },
            {
                className: "data-datetime-align",
                width: "100px",
                "data": "endDate",
                "type": "date",
                render: function (data, type, full, meta) {
                    return GetCellData('End Date', FormatDate(data));
                }
            },
            {
                className: "data-number-align",
                width: "50px",
                "data": "days",
                render: function (data, type, full, meta) {
                    return GetCellData('Days', data);
                }
            },
            {
                className: "tdblock data-text-align",
                width: "100px",
                "data": "engine",
                render: function (data, type, full, meta) {
                    return GetCellData('Engine', data);
                }
            },
            {
                className: "data-number-align ",
                width: "75px",
                "data": "power",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Power (KW)', data, 0, '');
                }
            }],
        "fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
            if (full.serviceActiveStatusId == 1 || full.isFutureOnshoreRecord == true) {
                $(nRow).find('td span.export-Data:eq(1)').css('background-color', full.shortCodeBackGroundColor);
                $(nRow).find('td span.export-Data:eq(1)').css('color', full.shortCodeForegroundColor);
                $(nRow).find('td span.export-Data:eq(1)').css('padding', '2px 10px');
            }
        },
        "initComplete": function () {
            $('#crewServiceHistoryCount').text(this.api().data().length)
        }
    });
    $.fn.DataTable.ext.pager.numbers_length = 4;
}

function fetchCertificatesAndDocs(input) {

    var masterCheckBox = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
    masterCheckBox += '<input type = "checkbox" class="select-checkbox-all custom-control-input" id="masterCheckboxAll">';
    masterCheckBox += '<label class="custom-control-label d-block" for="masterCheckboxAll"></label></div >';

    dtCertificatesAndDocs = $('#dtcertificates').DataTable({
        "dom": '<<"row row mb-3 mb-md-3 mb-xl-4"<"col-12 col-md-12 offset-md-0 col-lg-12 offset-lg-0 col-xl-7 offset-xl-3 dt-infomation"i>><t><"clearfix"<"float-left"l><""p>>>',
        "processing": true,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "pageLength": 10,
        "aaSorting": [],
        "language": {
            "emptyTable": "No certificates available.",
            "infoFiltered": "<div>(filtered from _MAX_ total entries)<div>",
        },
        'columnDefs': [
            {
                'orderable': false,
                'targets': 1,
                'visible': false,
                'render': function (data, type, row, meta) {
                    var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
                    uielement += '<input type = "checkbox" class="select-checkbox custom-control-input dt-checkboxes" id= "' + row.crdId + '">';
                    uielement += '<label class="custom-control-label d-block" for= "' + row.crdId + '"></label></div >';
                    data = uielement;
                    return data;
                },
                'createdCell': function (td, cellData, rowData, row, col) {
                    if (rowData.documentCount == 0) {
                        let child = $(td).children()[0];
                        $(child).addClass('invisible');
                        $(td).removeClass('dt-checkboxes-cell');
                        this.api().cell(td).checkboxes.disable();
                    }
                },
                'checkboxes': {
                    'selectRow': true,
                    'selectAllRender': masterCheckBox
                }
            },
            {
                'orderable': false,
                'targets': 2,
            }
        ],
        'select': {
            'style': 'multi'
        },
        "ajax": {
            "url": "/Crew/GetCertificatesAndDocuments",
            "type": "POST",
            "data":
            {
                "identifier": input
            },
            "datatype": "json",
        },
        "columns": [{
            className: "d-none",
            "data": "cdtDescription",
            width: "75px",
            render: function (data, type, full, meta) {
                return data;
            }
        }, {
            "data": "crdId",
            className: "mobile-popover-attachments tdblock data-icon-align checkbox-design-table",
            width: "17px",
        }, {

            className: "mobile-popover-attachments tdblock data-icon-align",
            width: "17px",
            render: function (data, type, full, meta) {

                if (full.documentCount == 0) {
                    return '';
                }
                else {
                    var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';
                    var uniqueId = full.crdId + '_' + crewId;
                    var element = '';

                    element = '<a class="text-black documentPopup cursor-pointer stock_p_' + uniqueId + '" target="_blank"><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
                    return element;
                }
            }
        }, {
            className: "tdblock data-text-align mobile-heading-record",
            "data": "name",
            width: "75px",
            render: function (data, type, full, meta) {

                var uiElement = '';
                if (full.isCovidVaccineTypeDocument) {
                    uiElement = '<span data-toggle="tooltip" data-placement="bottom" class="pr-1" title="' + data + '">' + data + '</span> <img src="/images/CovidVaccine.png" width="15" data-html="true" data-toggle="tooltip" data-placement="bottom" title="Covid Vaccine"/>'
                }
                else {
                    uiElement = '<span data-toggle="tooltip" data-placement="bottom" title="' + data + '">' + data + '</span>';
                }
                //return GetCellData('Name',uiElement);
                return uiElement;
            }
        }, {
            className: "data-text-align tdblock",
            "data": "ref",
            width: "75px",
            render: function (data, type, full, meta) {
                return GetCellData('Ref', '<span data-toggle="tooltip" data-placement="bottom" title="' + data + '">' + data + '</span>');
            }
        }, {
            className: "data-text-align",
            "data": "number",
            width: "40px",
            render: function (data, type, full, meta) {
                return GetCellData('Number', '<span data-toggle="tooltip" data-placement="bottom" title="' + data + '">' + data + '</span>');
            }
        }, {
            className: "data-text-align",
            "data": "cnt",
            width: "19px",
            render: function (data, type, full, meta) {
                return GetCellData('CNT', data);
            }
        }, {
            className: "data-datetime-align",
            "data": "issuedOn",
            "type": "date",
            width: "30px",
            render: function (data, type, full, meta) {
                return GetCellData('Issued On', FormatDate(data));
            }
        }, {
            className: "data-datetime-align",
            "data": "expiry",
            "type": "date",
            width: "30px",
            render: function (data, type, full, meta) {
                return GetCellData('Expiry', FormatDate(data));
            }
        }, {
            className: "data-text-align tdblock",
            "data": "authority",
            width: "40px",
            render: function (data, type, full, meta) {
                return GetCellData('Authority', '<span data-toggle="tooltip" data-placement="bottom" title="' + data + '">' + data + '</span>');
            }
        }],
        "fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
            if (full.isExpired || full.isExpiring) {
                var colorclass;
                if (full.isExpired) {
                    colorclass = 'txt-red';
                }
                else if (full.isExpiring) {
                    colorclass = 'txt-orange';
                }
                $(nRow).find('td').addClass(colorclass);
            }
        },
        "initComplete": function (settings, json) {
            $('#crewCertAndDocsCount').text(this.api().data().length);
        },
        "drawCallback": function (settings) {

            if ($('#isGrouping').is(":checked")) {
                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;

                api.column(0, { page: 'current' }).data().each(function (group, i) {
                    if (last !== group) {
                        $(rows).eq(i).before(
                            '<tr data-name="' + group + '" data-id="' + i + '" class="group group-start font-weight-bolder"><td colspan="8"><span id="togglestatus_' + i + '">-</span>&nbsp;' + group + '</td></tr>'
                        )
                        last = group;
                    }
                    $(rows).eq(i).attr('data-name', group + '_toggle');
                });
            }
            else {
                var api = this.api();
                var rows = api.rows().nodes();
                var last = null;

                api.column(0, { page: 'current' }).data().each(function (group, i) {
                    $('*[data-name="' + group + '_toggle"]').show();
                    $('#togglestatus_' + i).text('-');
                    $(rows).eq(i).removeAttr('data-name', group + '_toggle');
                });
            }
        }
    });
    $.fn.DataTable.ext.pager.numbers_length = 4;

    //configuring popupover
    $('#dtcertificates tbody').on('click', 'a.documentPopup', function () {
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
        $('body').addClass('popover-design');
        $('body').addClass('popover-design-crew');
        var data = dtCertificatesAndDocs.row($(this).parents('tr')).data();
        $("#spanModalAttachmentName").text(data.name);
        $("#spanModalCrewName").text(crewName);
        $("#spanModalREF").text(data.ref != null ? data.ref : "-");

        var uniqueClsSel = 'stock_p_' + data.crdId + '_' + crewId;
        if (data.documentCount == 1) {
            fetchAttachments(true, data.crdId);
        }
        else if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {

            var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
                '<div class="loader  mx-auto">' +
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
                url: "/Crew/GetAttachments",
                type: "POST",
                dataType: "JSON",
                global: false,
                data: {
                    "identifier": crewId,
                    "matchedIds": data.crdId
                },
                beforeSend: function (xhr) {
                    $('.' + uniqueClsSel).popover('show');
                    $(".elementLoader").block({
                        message: $(" " + loadercontent),
                    });
                },
                complete: function () {
                    $(".elementLoader").unblock();
                },
                success: function (data) {
                    var jsonArray = data.data;

                    var html_content = "<div class='elementLoader scroller'><table class='table table-condensed table-borderless mb-0'><tbody>";
                    var attachCount = jsonArray.length;
                    for (var i = 0; i < jsonArray.length; i++) {
                        html_content += "<tr>";
                        html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='/images/Download-doc-active.png' class='m-0' width='18' title='Download'/>";
                        html_content += "<span class='documentName' > " + jsonArray[i].name + " </span >";
                        html_content += "<span class='documentId d-none'> " + jsonArray[i].documentId + " </span >";
                        html_content += "<span class='documentfileName d-none' > " + jsonArray[i].fileName + " </span ></a></td > ";
                        html_content += "<td>" + (jsonArray[i].documentSize / (1024 * 1024)).toFixed(2) + " MB </td>";
                        html_content += "<td class='text-right'> " + FormatDate(jsonArray[i].uploadedOn) + "</td>";
                        html_content += "</tr>"
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
            })
        }
        else {
            $('.' + uniqueClsSel).popover('show');
        }

    });

    $(document).off('click', 'a.documentDownload', function () { });

    $(document).on('click', 'a.documentDownload', function () {

        var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
            '<div class="loader">' +
            '<div class="ball-clip-rotate">' +
            '<div></div>' +
            '</div>' +
            '</div>' +
            '</div>';

        var documentName = $(this).children('span.documentName').text();
        var documentId = $(this).children('span.documentId').text();
        var docfileName = $(this).children('span.documentfileName').text();

        var fileName = ''
        var input = {
            "identifier": documentId.trim(),
            "fileName": docfileName.trim()
        };
        fileName = documentName;

        $.ajax({
            url: "/Crew/DownloadDocument",
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
                    //saveByteArray(fileName, array, data.fileType);
                    saveOpenByteArray(fileName, array, data.fileType);
                } else {
                    ToastrAlert("validate", "File Not Found for \" " + fileName + "\"");
                }
            }
        });
    });

    $(document).on("click", ".close-popover", function () {
        $('.popover').popover('hide');
        $('body').removeClass('popover-design');
        $('body').removeClass('popover-design-crew');
    });

    $(document).on('click', 'body', function (e) {
        $('[data-toggle=popover]').each(function () {
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');

                //if ($('body').hasClass('popover-design')) {
                //    $('body').removeClass('popover-design')
                //}
            }
        });
    });

    $('#dtcertificates').on('change', '.select-checkbox, .select-checkbox-all', function () {

        var rows_selected = dtCertificatesAndDocs.column(1).checkboxes.selected();

        if (rows_selected.length == 0) {
            $("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
        }
        else {
            $("#btnDownloadSelection").removeClass('disabled').removeAttr('aria-disabled');
        }

        $('#AdvDocSelected').text(rows_selected.length);
    });

    $('#dtcertificates').on('click', 'tbody tr.group-start', function () {
        var name = $(this).data('name');
        var statusId = $(this).data('id');
        $('*[data-name="' + name + '_toggle"]').toggle();
        if ($('#togglestatus_' + statusId).text() == '-')
            $('#togglestatus_' + statusId).text('+');
        else
            $('#togglestatus_' + statusId).text('-');
    });
}

function fetchCrewDetails(crewIdentifier) {
    $.ajax({
        url: "/Crew/GetCrewDetails",
        dataType: "JSON",
        type: "POST",
        data: {
            "identifier": crewIdentifier
        },
        success: function (data) {
            $("#crewStatus").text(data.crewStatus);
            $("#crewName").text(data.name);
            $("#crewRank").text(data.rank);
            $("#divCvCrewName").text(data.name);
            $("#divCvCrewRank").text(data.rank);
            $("#crewPCN").text(data.pcn);
            $("#crewAirport").text(data.airport);
            $("#crewJoinDate").text(FormatDate(data.joinDate));
            $("#crewRankExperience").text(data.rankDetails.experienceInMonths + 'M, ' + data.rankDetails.experienceRemainingDays + 'D');
            var expList = [];

            $.each(data.experiences, function (index, value) {
                expList.push({
                    "vesselType": value.experienceType,
                    "experience": value.experienceInMonths + 'M, ' + value.experienceRemainingDays + 'D'
                });
            });

            loadExperienceTable(expList);
            crewName = data.name;
            crewRank = data.rank;
        },
        complete: function (data) {
            if (($(window).width() > MobileScreenSize)) {
                $('.height-equal').matchHeight({
                    byRow: 0
                });
            }
        }
    })
}
function loadExperienceTable(data) {
    $("#dtExperience").DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "scrollCollapse": true,
        "scrollY": "145px",
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [[1, "desc"]],
        "pageLength": 10,
        "language": {
            "emptyTable": "No data available.",
            "loadingRecords": "&nbsp;"
        },
        "data": data,
        "columns": [
            {
                className: "text-left",
                name: "VesselType",
                data: "vesselType",
                orderable: false,
                render: function (data, type, full, meta) {
                    return data;
                }
            },
            {
                className: "text-right",
                name: "Experience",
                type: "exp",
                data: "experience",
                render: function (data, type, full, meta) {
                    return data;
                }
            }
        ],
    });
}

function fetchCrewCurrentDetails(crewIdentifier) {
    $.ajax({
        url: "/Crew/GetCrewCurrentDetails",
        dataType: "JSON",
        type: "POST",
        data: {
            "identifier": crewIdentifier
        },
        success: function (data) {
            $("#crewCurrentServiceRank").text(data.rank);
            $("#crewCurrentServiceVessel").text(data.vessel);
            $("#crewCurrentServiceVessel").prop('href', "/Dashboard/?VesselId=" + $("#VesselId").val());
            $("#crewCurrentServiceService").text(FormatDate(data.serviceStart) + ' to ' + FormatDate(data.serviceEnd));
            $("#crewCurrentServiceClient").text(data.client);
            $("#crewCurrentServiceDaysRem").text(data.daysRem);
            if (data.contractLength == 0) {
                data.contractLength = "-";
            }
            if (data.contractLengthUnit == null) {
                data.contractLengthUnit = "-";
            }
            $("#crewCurrentServiceContractLen").text(data.contractLength + ' ' + data.contractLengthUnit);
        },
        complete: function (data) {
            if (($(window).width() > MobileScreenSize)) {
                $('.height-equal').matchHeight({
                    byRow: 0
                });
            }
        }
    })
}

function fetchCrewImage(identifier) {
    $.ajax({
        url: "/Crew/PostGetCrewImage",
        dataType: "JSON",
        type: "POST",
        data: {
            "identifier": identifier
        },
        success: function (data) {
            if (data != null) {
                $("#crewImage").attr("src", "data:image/png;base64," + data + "");
            }
            else {
                $("#crewImage").attr("src", "/images/NoImageAvailable.png");
            }
        }
    })
}
function GetCellData(label, data) {
    return '<label>' + label + '</label><br />' + GetActualCellData(data);
}

function GetActualCellData(data) {
    return '<span class="export-Data">' + data + '</span>';
}

function fetchAttachments(isCalledfromCerficationList, matchedId) {

    var matchedIds = [];
    var rows_selected = dtCertificatesAndDocs.column(1).checkboxes.selected();

    if (rows_selected.length > 0) {
        $.each(rows_selected, function (index, rowId) {
            matchedIds.push(rowId);
        });
    }
    else {
        matchedIds = matchedId;
    }

    if (matchedIds.length > 0) {
        var map_crewAttachmentData = new Map();
        $.ajax({
            url: "/Crew/GetAttachments",
            type: "POST",
            dataType: "JSON",
            data: {
                "identifier": crewId,
                "matchedIds": matchedIds
            },
            complete: function () {
                //$(".elementLoader").unblock();
            },
            success: function (data) {
                var jsonArray = data.data;

                for (var i = 0; i < jsonArray.length; i++) {
                    map_crewAttachmentData.set(i, {
                        documentName: jsonArray[i].name,
                        documentId: jsonArray[i].documentId,
                        documentFileName: jsonArray[i].fileName
                    });
                }
                DownloadSelectedAttachment(isCalledfromCerficationList, map_crewAttachmentData);
            }
        });
    }

}

function DownloadSelectedAttachment(isCalledfromCerficationList, map_crewAttachmentData) {

    var fileName = '';
    var nextAttach = 0;
    var totalAttachment = map_crewAttachmentData.size;

    DownloadNextAttachment();

    function DownloadNextAttachment() {

        // map_crewAttachmentData.forEach((value, key) => {

        var input = {
            "identifier": map_crewAttachmentData.get(nextAttach).documentId.trim(), //value.documentId.trim(), 
            "fileName": map_crewAttachmentData.get(nextAttach).documentFileName.trim() //value.documentFileName.trim()
        };
        fileName = map_crewAttachmentData.get(nextAttach).documentName; //value.documentName; 

        $.ajax({
            url: "/Crew/DownloadDocument",
            type: "POST",
            dataType: "JSON",
            data: {
                "input": JSON.stringify(input)
            },
            success: function (data) {

                if (data.bytes != null) {

                    var array = base64ToArrayBuffer(data.bytes);
                    if (isCalledfromCerficationList) {
                        saveOpenByteArray(fileName, array, data.fileType);
                    }
                    else {
                        saveByteArray(fileName, array, data.fileType);
                    }

                } else {
                    ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
                }

                nextAttach++;
                // Not implemented in error due to avoid call for multipule time 
                if (totalAttachment > nextAttach) {

                    DownloadNextAttachment();
                }
            }
        });
        //}); //loop

    }

}

function showAdvDowloadGrid() {
    $("#btnAdvDownload").hide();
    $(".grid-action-panel").show();
    $('.app-main__outer .background-padding').addClass('download-attachment-margin');
    // Get the column API object
    var ChkBoxColumn = dtCertificatesAndDocs.column(1);
    var IconColumn = dtCertificatesAndDocs.column(2);
    // Toggle the visibility
    ChkBoxColumn.visible(true);
    IconColumn.visible(false);

    if ($('#isGrouping').is(":checked")) {
        $('#isGrouping').trigger('click');
    }

    $('.isGroupCheckBox').hide();
    $('.isSelectAllDocument').show();
}

function hideAdvDowloadGrid() {
    $("#btnAdvDownload").show();
    $(".grid-action-panel").hide();
    $('.app-main__outer .background-padding').removeClass('download-attachment-margin');
    // Get the column API object
    $('.select-checkbox-all').prop("checked", false);
    var ChkBoxColumn = dtCertificatesAndDocs.column(1);
    var IconColumn = dtCertificatesAndDocs.column(2);
    ChkBoxColumn.visible(false);
    IconColumn.visible(true);
    dtCertificatesAndDocs.column(1).checkboxes.deselectAll();

    $('#AdvDocSelected').text(0);
    $('.isGroupCheckBox').show();
    $('.isSelectAllDocument').hide();
}

function LoadCvDefaultDetails() {
    $.ajax({
        url: "/Crew/GetCrewCvDetails",
        type: "POST",
        dataType: "JSON",
        success: function (result) {
            $("#divReportFormatType").text(result.reportFormatTypeDesc);
            reportFormatType = result.reportFormatType;
            var formatList = result.formatList;
            var select = document.getElementById('selFormat');
            for (var i = 0; i < formatList.length; i++) {
                var obj = formatList[i];
                var opt = document.createElement('option');
                opt.value = obj.identifier;
                opt.innerHTML = obj.description;
                if (opt.value == result.defaultFormat ) {
                    opt.selected = true;
                }
                select.appendChild(opt);
            }
        }
    });
}