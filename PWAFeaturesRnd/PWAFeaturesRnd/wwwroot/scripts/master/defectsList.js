import { createTree } from "jquery.fancytree";
import moment from "moment";
import "bootstrap";
import "datatables.net-colreorder";
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, GetCookie, ToastrAlert, base64ToArrayBuffer, saveByteArray, MobileTab_Overview, Mobile_Tabs, MobileTab_List, createFlatRadioButton, BackButton, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, AddClassIfAbsent, RemoveClassIfPresent, IsNullOrEmptyOrUndefinedLooseTyped, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js"
import { GetCellData, GetFormattedDate, GetExportData } from "../common/datatablefunctions.js"
import { Tab1, Tab2, MobileScreenSize, DateRangePickerLabelText, DefectListPageKey } from "../common/constants.js"

var startDate, endDate;
var dtDefectsList;
var selectedStartDate, selectedEndDate;
var noOfEnabledCheckboxes;
var isSearchClicked = false;
var defectFilterCount = 0;

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

$(document).on('click', '#aRemoveFilter', function () {
    ClearSelection();
    $("#aOpenDefectNav")[0].click();
    $('.popover').popover('hide');
});

$('#SystemAreaTree').click(function () {
    SystemAreaFilterCountSet(GetTreeNodeLength("#SystemAreaTree"));
});

$('#PlannedForTree').click(function () {
    PlannedForFilterCountSet(GetTreeNodeLength("#PlannedForTree"));
});

$('#StatusTree').click(function () {
    StatusFilterCountSet();
});

$(document).on('change', 'input[type=radio][name=criticalStatus]', function () {
    StatusFilterCountSet();
});

$(document).on('change', 'input[type=radio][name=defectDueStatus]', function () {
    StatusFilterCountSet();
});


$(document).ready(function () {
    isSearchClicked = false;
    AddLoadingIndicator();
    RemoveLoadingIndicator();

    InitializeListDiscussionAndNoteClickEvents(code);

    //Sidebar back
    BackButton(DefectListPageKey, true)

    //Show/hide button for Legend
    $('.btnHideLegend').on('click', function () {
        $('.divLegendSection').toggle('show');
        $(this).text(function (i, text) {
            return text === "Show Legend" ? "Hide Legend" : "Show Legend";
        });
    });

    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });

    RegisterTabSelectionEvent('.mobileTabClick', DefectListPageKey);

    $(document).click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }
    });

    $(window).scroll(function () {
        if (($(window).width() > 991)) {
            if ($(this).scrollTop() > 500) {
                $('#sticky-box').addClass("stickybox");
                var newWidth = ($(".table-scroll-width").outerWidth());
                $("#sticky-box").css({
                    "maxWidth": newWidth
                });
            } else {
                $('#sticky-box').removeClass("stickybox");
                var newWidth = ($(".table-scroll-width").outerWidth());
                $("#sticky-box").css({
                    "maxWidth": 'inherit'
                });
            }
        }
    });

    $('.height-equal').matchHeight();

    LoadSystemAreaTree();
    LoadPlannedForTree();
    LoadStatusTree();

    ///load dropdowns
    LoadAllDropDown();

    //Datepicker section -
    $("#dtRangeDefectList").caleran(
        {
            showButtons: true,
            hideOutOfRange: true,
            showOn: "top",
            arrowOn: "right",
            startEmpty: true,
            cancelLabel: "Clear",
            format: "DD MMM YYYY",
            ranges: [
                {
                    title: "Today",
                    startDate: moment(),
                    endDate: moment()
                },
                {
                    title: "Yesterday",
                    startDate: moment().subtract(1, "day"),
                    endDate: moment()
                },
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
                    title: "Last 3 Months",
                    startDate: moment().subtract(3, "month"),
                    endDate: moment()
                },
                {
                    title: "Last 6 Months",
                    startDate: moment().subtract(6, "month"),
                    endDate: moment()
                },
                {
                    title: "Last 12 Months",
                    startDate: moment().subtract(12, "month"),
                    endDate: moment()
                },
            ],
            rangeOrientation: "vertical",
            onafterselect: function (caleran, startDate, endDate) {
                setDateDetails(startDate, endDate);
            },
            onCancel: function (caleran, start, end) {
                setDateDetailonCancel();
                return true;
            }
        }
    );


    SetDatePickerValues();

    //to load stats section
    LoadDefectSummary();

    //search
    $('#btnSearch').click(function () {
        SetPageParameter();
        ResetOffSet();
        hideAdvDowloadGrid();
        $('.popover').popover('hide');
    });

    //clear
    $('#btnClear').click(function () {
        ClearSelection();
        $("#aOpenDefectNav")[0].click();
        $('.popover').popover('hide');
    });

    $('.btnExport').click(function () {
        ExportToExcel();
    });

    ResetOffSet();

    //Download Attachment methods starts ---------
    $('.btnAdvDownload').on('click', function () {
        showAdvDowloadGrid();
        $('.popover').popover('hide');
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

    $('#dtDefectsList').on('change', '.select-checkbox, .select-checkbox-all', function () {
        var rows_selected = dtDefectsList.column(0).checkboxes.selected();
        if (rows_selected.length == 0) {
            $("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
        }
        else {
            $("#btnDownloadSelection").removeClass('disabled').removeAttr('aria-disabled');
        }
        if (rows_selected.length > 0) {
            let rows = dtDefectsList.rows().nodes();
            noOfEnabledCheckboxes = $('input[type="checkbox"].select-checkbox', rows).not(":disabled").length;

            if (noOfEnabledCheckboxes == rows_selected.length) {
                $("#AdvDownSelectAll").prop({
                    checked: true,
                });
            } else {
                $("#AdvDownSelectAll").prop({
                    indeterminate: true,
                });
            }
        } else {
            $("#AdvDownSelectAll").prop({
                indeterminate: false,
                checked: false
            });
        }

        $('#AdvDocSelected').text(rows_selected.length);
    });

    $(document).on("click", ".close-popover", function () {
        $('.popover').popover('hide');
        $('body').removeClass('popover-design');
        $('body').removeAttr('class');
    });

    $(document).on('click', 'body', function (e) {
        $('[data-toggle=popover]').each(function () {
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });

    ConfigurePopover();

    ConfigureDownloadPopup();

    $('#filterdata').on('show.bs.modal', function (e) {

        if ($('#filterStatus').css('display') == 'none'
            && $('#filterCriticalRow').css('display') == 'none'
            && $('#filterCriticalDueRow').css('display') == 'none'){
            $("#filterCard4").hide();
        }
        else {
            $("#filterCard4").show();
        }
    });

    $("#txtDefectTitle").keyup(function () {
        KeywordFilterCountSet($(this).val());
    });

    AjaxError();
});


function setDateDetails(startDate, endDate) {

    $('#dtRangeDefectList').html(startDate.format("DD MMM YYYY") + ' - ' + endDate.format("DD MMM YYYY"));
    $('#tblspndtrdefectslist').html(startDate.format("DD MMM YYYY") + ' - ' + endDate.format("DD MMM YYYY"));

    selectedStartDate = startDate.format("DD MMM YYYY");
    selectedEndDate = endDate.format("DD MMM YYYY");
    SetPageParameter();
}

function setDateDetailonCancel() {
    $('#dtRangeDefectList').html(DateRangePickerLabelText);
    selectedStartDate = '';
    selectedEndDate = '';
    ResetTblheading();

}

function LoadAllDropDown() {
    LoadDefectDueList(function (criticalList) {
        LoadDefectCriticalList(function (dueList) {
            MaintainFilters();
        });
    });
    SetMobileTabWindow();
}

function SetDatePickerValues() {
    var localStartDate = '';
    var localEndDate = '';

    if ($('#FromDate').val() != null && $('#FromDate').val() != 'undefined' && $('#FromDate').val() != '') {
        localStartDate = moment($('#FromDate').val(), 'DD-MM-YYYY');
        selectedStartDate = localStartDate.format("DD MMM YYYY");
    }
    else {
        selectedStartDate = '';
    }
    if ($('#ToDate').val() != null && $('#ToDate').val() != 'undefined' && $('#ToDate').val() != '') {
        localEndDate = moment($('#ToDate').val(), 'DD-MM-YYYY');
        selectedEndDate = localEndDate.format("DD MMM YYYY");
    }
    else {
        selectedEndDate = '';
    }
    ResetOffSet();
}

function ResetOffSet() {
    if (selectedStartDate == '' && selectedEndDate == '') {
        $('#dtRangeDefectList').html(DateRangePickerLabelText);
        $('#tblspndtrdefectslist').html('');
    } else {
        $('#dtRangeDefectList').html(selectedStartDate + ' - ' + selectedEndDate);
        $('#tblspndtrdefectslist').html(selectedStartDate + ' - ' + selectedEndDate);
    }
}

function ResetTblheading() {
    $('#tblspndtrdefectslist').html('');
    //  $('.dt-infomationhed').removeClass('offset-md-5 offset-lg-4 offset-xl-3');
    // $('.dt-infomationhed').addClass('offset-md-1 offset-lg-1 offset-xl-1');
}

function ClearSelection() {
    defectFilterCount = 0;
    $('#txtDefectTitle').val('');
    $('input[type=radio][name=defectDueStatus]').prop('checked', false);
    var $selRadios = $('input[type=radio][name=defectDueStatus]');
    $selRadios.filter('[value=All]').prop('checked', true);

    $('input[type=radio][name=criticalStatus]').prop('checked', false);
    var $selRadios = $('input[type=radio][name=criticalStatus]');
    $selRadios.filter('[value=All]').prop('checked', true);

    $('#SelectedSystemAreaIds').val('');
    $('#SelectedPlannedForIds').val('');
    $('#SelectedStatusIds').val('');

    SetPlannedForTree();
    SetStatusTree();
    SetSystemAreaTree();

    isSearchClicked = false;
    ResetTblheading();
    $('#FromDate').val('');
    $('#ToDate').val('');
    $('#dtRangeDefectList').html('-');
    SetDatePickerValues();
}

function SetSearchInput() {

    var input = {
        "IsSearchClicked": true,
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
        "ToDate": selectedEndDate,
        "FromDate": selectedStartDate,
        "SelectedPlannedFor": $('#SelectedPlannedForIds').val(),
        "SelectedStatus": $('#SelectedStatusIds').val(),
        "SelectedSystemArea": $('#SelectedSystemAreaIds').val(),
        "DefectTitle": $('#txtDefectTitle').val(),
        "SelectedCriticalStatus": $('input[name="criticalStatus"]:checked').val(),
        "SelectedDueStatus": $('input[name="defectDueStatus"]:checked').val(),
    }
    return input;
}

//wrapper for main listing call- LoadDefectWorkBasket
function CallDefectWorkBasket(IsSearchClicked) {
    isSearchClicked = IsSearchClicked;
    let data;
    if (IsSearchClicked) {
        data = SetSearchInput();
    }
    else {
        data = {
            "EncryptedVesselId": $('#EncryptedVesselId').val(),
            "ToDate": selectedEndDate,
            "FromDate": selectedStartDate,
            "StageName": $('#StageName').val(),
        }
    }
    LoadDefectWorkBasket(data);
}

//Load Defect List
function LoadDefectWorkBasket(data) {
    if (dtDefectsList != undefined && dtDefectsList != 'undefined') {
        dtDefectsList.colReorder.reset();
    }

    var masterCheckBox = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
    masterCheckBox += '<input type = "checkbox" class="select-checkbox-all custom-control-input" id="masterCheckboxAll">';
    masterCheckBox += '<label class="custom-control-label d-block" for="masterCheckboxAll"></label></div >';

    $('#dtDefectsList').DataTable().destroy();
    dtDefectsList = $('#dtDefectsList').DataTable({
        //"dom": '<<"row mb-3"<"col-12 col-md-4 offset-md-0 col-lg-3 offset-lg-5 col-xl-2 offset-xl-3 dt-infomation  dt-infomationhed"i><"col-md-3 col-lg-2 col-xl-4 filters-data"><"col-12 col-md-5 col-lg-3 col-xl-3"f>><rt><"clearfix"<"float-left"l><""p>>>',
        //"dom": '<"row"<"col-12 col-md-6 col-lg-6 col-xl-6"i><"col-12 col-md-6 col-lg-6 col-xl-6 search-filter"f>>' +
        //    '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        colReorder: {
            fixedColumnsLeft: 3
        },
        //drawCallback: function (settings) {
        //    var pagination = $(this).closest('.dataTables_wrapper').find('.dataTables_paginate');
        //    if (this.api().page.info().pages > 1) {
        //        $('.dataTables_length').show();
        //        pagination.show();
        //    } else {
        //        $('.dataTables_length').hide();
        //        pagination.hide();
        //    }
        //},
        "autoWidth": false,
        "paging": true,
        "pageLength": 10,
        "order": [],
        "language": {
            "emptyTable": "No defects available.",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "loadingRecords": "&nbsp;",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        'columnDefs': [
            {
                'orderable': false,
                'targets': 0,
                'visible': false,
                'render': function (data, type, row, meta) {
                    if (row.documentCount == 0) {
                        var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
                        uielement += '<input type = "checkbox" class="select-checkbox custom-control-input dt-checkboxes" id= "' + row.defectWorkOrderId + '" disabled="disabled">';
                        uielement += '<label class="custom-control-label d-block" for= "' + row.defectWorkOrderId + '"></label></div >';
                        data = uielement;
                        return data;
                    } else {
                        var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
                        uielement += '<input type = "checkbox" class="select-checkbox custom-control-input dt-checkboxes" id= "' + row.defectWorkOrderId + '">';
                        uielement += '<label class="custom-control-label d-block" for= "' + row.defectWorkOrderId + '"></label></div >';
                        data = uielement;
                        return data;
                    }
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
        ],
        'select': {
            'style': 'multi'
        },
        "ajax": {
            "url": "/Defect/GetDefectWorkBasketList",
            "type": "POST",
            "data": data,
            "datatype": "json"
        },
        "columns": [
            {
                "data": "defectWorkOrderId",
                orderable: false,
                className: "mobile-popover-attachments tdblock data-icon-align checkbox-design-table",
                width: "30px",
            },
            {
                className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
                orderable: false,
                width: "30px",
                render: function (data, type, full, meta) {
                    if (full.documentCount == 0) {
                        return '';
                    }
                    else {
                        var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';
                        var uniqueId = full.defectWorkOrderId;
                        var element = '';

                        element = '<a class="text-black documentPopup cursor-pointer universalIdentifier_' + uniqueId + '" target="_blank" ><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
                        return element;
                    }
                }
            },
            {
                className: "data-icon-align d-none d-md-table-cell",
                orderable: false,
                width: "18px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0) {
                        return GetChatBaseIcons(full.defectWorkOrderId, full.channelCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align d-none d-md-table-cell",
                orderable: false,
                width: "10px",
                render: function (data, type, full, meta) {
                    if (full.notesCount > 0) {
                        return GetNotesBaseIcons(full.defectWorkOrderId, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align tdblock d-md-none d-lg-none d-xl-none",
                orderable: false,
                width: "70px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0 || full.notesCount > 0) {
                        return GetChatNotesBaseIcons(full.defectWorkOrderId, full.channelCount, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "d-none d-sm-table-cell data-text-align",
                data: "defectNo",
                width: "72px",
                render: function (data, type, full, meta) {
                    return GetCellData('Defect No.', data);
                }
            },
            {
                className: "width-85 data-text-align",
                data: "title",
                width: "200px",
                render: function (data, type, full, meta) {
                    var criticalElement = "";
                    if (full.isCritical) {
                        criticalElement += "<i class='fa fa-exclamation-circle ml-1 txt-red' data-html='true' data-toggle='tooltip' data-placement='bottom' title='Critical'></i>";
                    }

                    return '<span class="d-inline-block d-sm-none"> ' + full.defectNo + '</span> <a href="/Defect/Details/?DefectDetails=' + full.defectDetailURL + '&VesselId=' + $("#EncryptedVesselId").val() + '"> <span>' + data + '</span></a>' + criticalElement;
                }
            },
            {
                className: "data-datetime-align",
                data: "estimatedCompleteDate",
                width: "85px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var localDate = LocalGetFormattedDate(data);
                        var rescheduleElement = "";
                        if (full.isRescheduleCount) {
                            rescheduleElement += "<img src='/images/Rescheduled.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom'>";
                        }
                        if (full.isOverDueVisible) {
                            rescheduleElement += "<span class='txt-red'>" + localDate + " </span>";
                        }
                        else {
                            rescheduleElement += localDate;
                        }

                        return GetCellData('Current Due Date', rescheduleElement);
                    }
                    else {
                        var date = "";
                        if (data != null && data != "" && data != undefined) {
                            date = new Date(data);
                        }
                        return date;
                    }

                }
            },
            {
                className: "data-datetime-align",
                data: "actualCompleteDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Date Completed', data);

                }
            },
            {
                className: "data-text-align",
                data: "status",
                width: "50px",
                render: function (data, type, full, meta) {
                    var ShowStatus = "";
                    ShowStatus += "<span class='d-sm-block d-md-none'" + GetExportData(data) + "<i class='fa fa-info-circle ml-1 d-sm-block d-md-none' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.statusDescription + "'></i></span>";
                    ShowStatus += "<span class='d-none d-sm-none d-md-block' data-placement='bottom' data-toggle='tooltip' title='" + full.statusDescription + "'>" + GetExportData(data) + "</span>";
                    return GetCellData('Status', ShowStatus);
                }
            },
            {
                className: "data-icon-align",
                data: "",
                width: "65px",
                render: function (data, type, full, meta) {
                    var techDefectElement = "";
                    if (full.techDefect) {
                        techDefectElement = "<img src='/images/TechDefect.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.defectDamageFormNumber + "'>";
                    }
                    else {
                        techDefectElement = "";
                    }

                    var guaranteeClaimCodeElement = "";
                    if (full.guaranteeClaimCode) {
                        guaranteeClaimCodeElement = "<img src='/images/GuaranteedClaim.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.guaranteeClaimNumber + "'>";
                    }
                    else {
                        guaranteeClaimCodeElement = "";
                    }

                    var JSARequiredElement = "";
                    if (full.isJSARequired) {
                        JSARequiredElement = "<img src='/images/JSA.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='JSA Required'>";
                    }
                    else {
                        JSARequiredElement = "";
                    }

                    var MOCRequiredElement = "";
                    if (full.isMOCRequired) {
                        MOCRequiredElement = "<img src='/images/MOC.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='MOC Required'>";
                    }
                    else {
                        MOCRequiredElement = "";
                    }

                    var RobLessThanReqIcon = "";
                    if (full.isRobLessThanReq) {
                        RobLessThanReqIcon = "<img src='/images/PartsGeometryRed.png' class='mr-1' width='15' data-html='true' data-toggle='tooltip' data-placement='bottom' title='ROB is less than required.'>";
                    }
                    else {
                        RobLessThanReqIcon = "";
                    }

                    var finalUIElement = techDefectElement + guaranteeClaimCodeElement + JSARequiredElement + MOCRequiredElement + RobLessThanReqIcon

                    return GetCellData('Specifics', finalUIElement);

                }
            },
            {
                className: "data-text-align",
                data: "offHire",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetCellData('Off Hire', data);
                }
            },
            {
                className: "data-text-align",
                data: "impact",
                width: "160px",
                render: function (data, type, full, meta) {
                    return GetCellData('Impact', data);
                }
            },
            {
                className: "data-text-align",
                data: "siteTypeDescription",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetCellData('Planned For', data);
                }
            },
            {
                className: "data-text-align",
                data: "category",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetCellData('Category', data);
                }
            },
            {
                className: "data-text-align",
                data: "priority",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Priority', data);
                }
            },
            {
                className: "data-number-align",
                data: "requisitionCount",
                width: "35px",
                render: function (data, type, full, meta) {
                    var color = '';
                    if (data > 0) {
                        color = "txt-color-green font-weight-600";
                    }
                    else {
                        color = "txt-color-gray";
                    }

                    return GetCellData('Req.', '<span class="' + color + '">' + data + '</span>');
                }
            },
            {
                className: "tdblock data-text-align",
                data: "systemArea",
                width: "180px",
                render: function (data, type, full, meta) {
                    var ShowElement = GetExportData(data);
                    if (full.subSystemArea != null && full.subSystemArea != '' && full.subSystemArea != 'undefined') {
                        ShowElement += "<i class='fa fa-info-circle' data-html='true' data-toggle='tooltip' data-placement='bottom' title='" + full.subSystemArea + "'></i>";
                    }
                    return GetCellData('System Area', ShowElement);
                }
            },
        ],
        "initComplete": function (settings, data) {
            AppendGridTitle();
        }
    });
    //$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata"  class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');

    $.fn.DataTable.ext.pager.numbers_length = 4;
    $('#dtDefectsList').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });

    //table scroll
    var newWidth = ($(".table-scroll-width").width());
    $(".table-common-design .table-horizontal-scroll").css({
        "maxWidth": newWidth - 20
    });
}

//local methods
function LocalGetFormattedDate(data) {
    var date = "";
    var formattedDate = "";
    if (data != null && data != "" && data != undefined) {
        date = new Date(data);
        formattedDate = moment(date).format("DD MMM YYYY");
    }
    return formattedDate;
}

function LoadDefectSummary() {
    $.ajax({
        url: "/Defect/GetDefectManagerSummaryDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "EncryptedVesselId": $('#EncryptedVesselId').val()
        },
        success: function (data) {

            if (data != null) {
                //Remove all click event
                $(".click-event-off").off('click');

                $('#spanCloseDefect').text(data.closeDefectCount);
                SummaryColorCode('#spanCloseDefect', data.closeDefectCount, 'txt-blue');
                $("#aCloseDefectNav").click(function () {
                    SetSummaryFilterInTempData(data.closedDefectNavigation)
                });

                $('#spanOpenDefect').text(data.openDefectCount);
                SummaryColorCode('#spanOpenDefect', data.openDefectCount, 'txt-blue');
                $("#aOpenDefectNav").click(function () {
                    SetSummaryFilterInTempData(data.openDefectNavigation)
                });

                $('#spanOverdueDefect').text(data.overdueCount);
                SummaryColorCode('#spanOverdueDefect', data.overdueCount, 'txt-red');
                $("#aOverdueNav").click(function () {
                    SetSummaryFilterInTempData(data.overdueDefectNavigation)
                });

                $('#spanOffhireDefect').text(data.offHireCount);
                SummaryColorCode('#spanOffhireDefect', data.offHireCount, 'txt-red');
                $("#aOffHireNav").click(function () {
                    SetSummaryFilterInTempData(data.offHireDefectNavigation)
                });

                $('#spanLayoverDefect').text(data.layoverCount);
                SummaryColorCode('#spanLayoverDefect', data.layoverCount, 'txt-red');
                $("#aLayoverNav").click(function () {
                    SetSummaryFilterInTempData(data.layOverDefectNavigation)
                });

                $('#spanDrydockDefect').text(data.drydockCount);
                SummaryColorCode('#spanDrydockDefect', data.drydockCount, 'txt-blue');
                $("#aDrydockNav").click(function () {
                    SetSummaryFilterInTempData(data.drydockDefectNavigation)
                });

                $('#spanTechDefect').text(data.technicalDefectCount);
                SummaryColorCode('#spanTechDefect', data.technicalDefectCount, 'txt-blue');
                $("#aTechDefectNav").click(function () {
                    SetSummaryFilterInTempData(data.technicalDefectNavigation)
                });

                $('#spanGuaranteeClaimDefect').text(data.guaranteeClaimCount);
                SummaryColorCode('#spanGuaranteeClaimDefect', data.guaranteeClaimCount, 'txt-blue');
                $("#aGuaranteeClaimNav").click(function () {
                    SetSummaryFilterInTempData(data.guaranteeClaimDefectNavigation)
                });

                $('#spanCompletedDefects').text(data.completedDefectsCount);
                SummaryColorCode('#spanCompletedDefects', data.completedDefectsCount, 'txt-blue');
                $("#aCompletedDefectsNav").click(function () {
                    SetSummaryFilterInTempData(data.completedDefectsNavigation)
                });
            }

        }
    });
}

function SummaryColorCode(span, data, colorClass) {
    if (data == 0) {
        $(span).addClass("txt-color-gray");
    }
    else {
        $(span).addClass(colorClass);
    }
}

//Called from LoadAllDropDown
function LoadDefectDueList(resolve) {

    $.ajax({
        url: "/Defect/GetDefectDueList",
        type: "POST",
        "datatype": "JSON",
        success: function (data) {

            $("#defectDueRadiosContainer").html("");

            for (let i = 0; i < data.length; i++) {
                let opt = data[i];
                let statusRd = createFlatRadioButton("dueStatusRd" + i, "defectDueStatus", opt.identifier, opt.description);
                $("#defectDueRadiosContainer").append(statusRd);
            }

            var selectedDueStatus = $('#SelectedDueStatus').val();
            var $flatRadioButton = $('input[type=radio][name=defectDueStatus]');
            if ($flatRadioButton.is(':checked') === false) {
                $flatRadioButton.filter('[value=All]').prop('checked', true);
            }
            resolve(true);
        }
    });
}

function SetSelectedDefectDue(selectedValue) {
    var $flatRadioButton = $('input[type=radio][name=defectDueStatus]');
    $flatRadioButton.filter('[value=' + selectedValue + ']').prop('checked', true);

    var dueStatusFilterVal = $('input[name="defectDueStatus"]:checked').data("description");
    dueStatusFilterVal = dueStatusFilterVal === 'All' ? null : dueStatusFilterVal;
    AppendTextFilterDataInModel(dueStatusFilterVal, "#filterCriticalDue", "#filterCriticalDueRow");
}

//Called from LoadAllDropDown
function LoadDefectCriticalList(resolve) {

    $.ajax({
        url: "/Defect/GetDefectCriticalList",
        type: "POST",
        "datatype": "JSON",
        success: function (data) {

            $("#criticalRadiosContainer").html("");

            for (let i = 0; i < data.length; i++) {
                let opt = data[i];
                let statusRd = createFlatRadioButton("criticalRd" + i, "criticalStatus", opt.identifier, opt.description);
                $("#criticalRadiosContainer").append(statusRd);
            }

            var selectedCriticalStatus = $('#SelectedCriticalStatus').val();
            var $flatRadioButton = $('input[type=radio][name=criticalStatus]');
            if ($flatRadioButton.is(':checked') === false) {
                $flatRadioButton.filter('[value=All]').prop('checked', true);
            }
            resolve(true);
        }
    });
}

function SetSelectedDefectCriticalDropDown(selectedValue) {
    var $flatRadioButton = $('input[type=radio][name=criticalStatus]');
    $flatRadioButton.filter('[value=' + selectedValue + ']').prop('checked', true);

    var criticalStatusFilterVal = $('input[name="criticalStatus"]:checked').data("description");
    criticalStatusFilterVal = criticalStatusFilterVal === 'All' ? null : criticalStatusFilterVal;
    AppendTextFilterDataInModel(criticalStatusFilterVal, "#filterCritical", "#filterCriticalRow");
}

//------------- Maintain filter changes

//To update HiddenField, LoadGrid, SetSelectedFilterValue
function MaintainFilters() {
    $.ajax({
        url: "/Defect/MaintainFilterParameters",
        type: "POST",
        success: function (data) {
            if (data.isTempDataExist) {
                var response = data.data;
                UpdateHiddenField(response);
                SetFilters(response);
                isSearchClicked = response.isSearchClicked;
            }
            else {
                isSearchClicked = false;

                var criticalStatusFilterVal = $('input[name="criticalStatus"]:checked').data("description");
                criticalStatusFilterVal = criticalStatusFilterVal === 'All' ? null : criticalStatusFilterVal;
                AppendTextFilterDataInModel(criticalStatusFilterVal, "#filterCritical", "#filterCriticalRow");

                var dueStatusFilterVal = $('input[name="defectDueStatus"]:checked').data("description");
                dueStatusFilterVal = dueStatusFilterVal === 'All' ? null : dueStatusFilterVal;
                AppendTextFilterDataInModel(dueStatusFilterVal, "#filterCriticalDue", "#filterCriticalDueRow");

                var DefectTitleVal = $('#txtDefectTitle').val();
                AppendTextFilterDataInModel(DefectTitleVal, "#filterKeyword", "#filterCard1");
            }
        },
        complete: function () {
            CallDefectWorkBasket(isSearchClicked);
        }
    });
}

//To load filter with pre selected value
//To maintain tab in mobile ui
function SetFilters(data) {
    defectFilterCount = 0;
    $('#txtDefectTitle').val(data.defectTitle);
    SetSelectedDefectDue(data.selectedDueStatus);
    SetSelectedDefectCriticalDropDown(data.selectedCriticalStatus);
    SetDatePickerValues();
    SetMobileTabWindow();
    SetStatusTree();
    SetPlannedForTree();
    SetSystemAreaTree();
    var DefectTitleVal = $('#txtDefectTitle').val();
    AppendTextFilterDataInModel(DefectTitleVal, "#filterKeyword", "#filterCard1");

    KeywordFilterCountSet(data.defectTitle);
    StatusFilterCountSet();
}

function SetMobileTabWindow() {
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }
}

//To update hidden field value which are - VesselId, SelectedStageNAme
function UpdateHiddenField(data) {
    $('#ActiveMobileTabClass').val(data.activeMobileTabClass);
    $('#StageName').val(data.stageName);
    $('#FromDate').val(data.fromDate);
    $('#ToDate').val(data.toDate);
    $('#GridSubTitle').val(data.gridSubTitle);


    if (data.selectedSystemArea != null) {
        $('#SelectedSystemAreaIds').val(data.selectedSystemArea.join(","));
    }

    if (data.selectedPlannedFor != null) {
        $('#SelectedPlannedForIds').val(data.selectedPlannedFor.join(","));
    }

    if (data.selectedStatus != null) {
        $('#SelectedStatusIds').val(data.selectedStatus.join(","));
    }
}

//Will be called only in search click
//This will be called from search click which stores latest temp data when clicked on search button
function SetPageParameter() {
    GetSystemAreaTree();
    GetStatusTree();
    GetPlannedForTree();

    var input = {
        "IsSearchClicked": true,
        "EncryptedVesselId": $('#EncryptedVesselId').val(),
        "ToDate": selectedEndDate,
        "FromDate": selectedStartDate,
        "ActiveMobileTabClass": $('#ActiveMobileTabClass').val(),
        "SelectedPlannedFor": $('#SelectedPlannedForIds').val(),
        "SelectedStatus": $('#SelectedStatusIds').val(),
        "SelectedSystemArea": $('#SelectedSystemAreaIds').val(),

        "DefectTitle": $('#txtDefectTitle').val(),
        "SelectedCriticalStatus": $('input[name="criticalStatus"]:checked').val(),
        "SelectedDueStatus": $('input[name="defectDueStatus"]:checked').val(),
        "StageName": $('#StageName').val()
    }

    $.ajax({
        url: "/Defect/SetPageParameter",
        type: "POST",
        data: input,
        success: function (data) {
            UpdateHiddenField(data.data);

            defectFilterCount = 0;
            AppendTreeFilterDataInModel("#SystemAreaTree", "#filterSystemArea", "#filterCard2");
            AppendTreeFilterDataInModel("#PlannedForTree", "#filterPlannedFor", "#filterCard3");
            AppendTreeFilterDataInModel("#StatusTree", "#filterStatus", "#filterStatus");

            var criticalStatusFilterVal = $('input[name="criticalStatus"]:checked').data("description");
            criticalStatusFilterVal = criticalStatusFilterVal === 'All' ? null : criticalStatusFilterVal;
            AppendTextFilterDataInModel(criticalStatusFilterVal, "#filterCritical", "#filterCriticalRow");

            var dueStatusFilterVal = $('input[name="defectDueStatus"]:checked').data("description");
            dueStatusFilterVal = dueStatusFilterVal === 'All' ? null : dueStatusFilterVal;
            AppendTextFilterDataInModel(dueStatusFilterVal, "#filterCriticalDue", "#filterCriticalDueRow");

            var DefectTitleVal = $('#txtDefectTitle').val();
            AppendTextFilterDataInModel(DefectTitleVal, "#filterKeyword", "#filterCard1");
        },
        complete: function () {
            CallDefectWorkBasket(true);
        }
    });
}

//To maintain Summary selection in Session
function SetSummaryFilterInTempData(data) {
    $.ajax({
        url: "/Defect/SetSummaryFilterInTempData",
        type: "POST",
        dataType: "JSON",
        data: {
            "defectUrl": data,
            "vesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data != null) {
                ClearSelection();
                UpdateHiddenField(data.data);
                SetFilters(data.data);
                SetMobileTabWindow();
            }
        },
        complete: function () {
            CallDefectWorkBasket(false);
        }
    });
}

//------ Export to excel
function ExportToExcel() {
    let data;
    if (isSearchClicked) {
        data = SetSearchInput();
    }
    else {
        data = {
            "EncryptedVesselId": $('#EncryptedVesselId').val(),
            "ToDate": selectedEndDate,
            "FromDate": selectedStartDate,
            "StageName": $('#StageName').val(),
        }
    }

    $.ajax({
        url: "/Defect/ExportToExcelDefectList",
        type: "POST",
        data: {
            "input": data
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


//--- Download attachmnet
function showAdvDowloadGrid() {
    $(".btnAdvDownload").hide();
    if ($("#mobileActiondropdown").hasClass('show')) {
        $("#mobileActiondropdown").removeClass('show');
    }
    $(".grid-action-panel").show();
    $('.app-main__outer .background-padding').addClass('download-attachment-margin');
    //Get the column API object
    var ChkBoxColumn = dtDefectsList.column(0);
    var IconColumn = dtDefectsList.column(1);
    $('.select-checkbox-all').prop({
        indeterminate: false,
        checked: false
    });
    $('#AdvDownSelectAll').prop({
        indeterminate: false,
        checked: false
    });
    ChkBoxColumn.visible(true);
    IconColumn.visible(false);
    $('.isSelectAllDocument').show();
    $("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
}

function hideAdvDowloadGrid() {
    if (($(window).width() < MobileScreenSize)) {
        $("#btnAdvDownloadMobile").show();
        $("#btnAdvDownloadDesktop").hide();
    } else {
        $("#btnAdvDownloadDesktop").show();
    }

    $(".grid-action-panel").hide();
    $('.app-main__outer .background-padding').removeClass('download-attachment-margin');
    //Get the column API object
    $('.select-checkbox-all').prop("checked", false);
    $("#AdvDownSelectAll").prop("checked", false);

    var ChkBoxColumn = dtDefectsList.column(0);
    var IconColumn = dtDefectsList.column(1);
    ChkBoxColumn.visible(false);
    IconColumn.visible(true);
    dtDefectsList.column(0).checkboxes.deselectAll();

    $('#AdvDocSelected').text(0);
    $('.isSelectAllDocument').hide();
}

function fetchAttachments(defectWorkOrderId) {
    var defectWorkOrderIds = [];
    var rows_selected = dtDefectsList.column(0).checkboxes.selected();

    if (rows_selected.length > 0) {
        $.each(rows_selected, function (index, rowId) {
            defectWorkOrderIds.push(rowId);
        });
    }
    else {
        defectWorkOrderIds = defectWorkOrderId;
    }

    if (defectWorkOrderIds.length > 0) {
        var defectDocMap = new Map();
        $.ajax({
            url: "/Defect/GetDefectDocuments",
            type: "POST",
            dataType: "JSON",
            data: {
                "DefectWorkOrderIds": defectWorkOrderIds
            },
            success: function (data) {
                var jsonArray = data.data;
                var counter = 0;
                for (var i = 0; i < jsonArray.length; i++) {
                    if (!jsonArray[i].isWebAddressEditable) {
                        defectDocMap.set(counter++, {
                            documentName: jsonArray[i].title,
                            documentId: jsonArray[i].ettId,
                            documentFileName: jsonArray[i].cloudFileName,
                            documentCategory: jsonArray[i].documentCategory
                        });
                    }
                }
                DownloadSelectedAttachment(defectDocMap, true);
            }
        });
    }
}

function DownloadSelectedAttachment(defectDocMap, globalFlag) {

    var fileName = '';
    var nextAttach = 0;
    var totalAttachment = defectDocMap.size;
    DownloadNextAttachment();

    function DownloadNextAttachment() {
        var documentId = (defectDocMap.get(nextAttach).documentId != null && defectDocMap.get(nextAttach).documentId != 'undefined') ? defectDocMap.get(nextAttach).documentId.trim() : '';
        var documentFileName = (defectDocMap.get(nextAttach).documentFileName != null && defectDocMap.get(nextAttach).documentFileName != 'undefined') ? defectDocMap.get(nextAttach).documentFileName.trim() : '';
        var documentCategory = (defectDocMap.get(nextAttach).documentCategory != null && defectDocMap.get(nextAttach).documentCategory != 'undefined') ? defectDocMap.get(nextAttach).documentCategory : '';
        var input = {
            "identifier": documentId,
            "fileName": documentFileName,
            "documentCategory": documentCategory
        };
        fileName = defectDocMap.get(nextAttach).documentName.trim();

        $.ajax({
            url: "/Defect/DownloadDocument",
            type: "POST",
            dataType: "JSON",
            global: globalFlag,
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

                nextAttach++;
                if (totalAttachment > nextAttach) {
                    DownloadNextAttachment();
                }
            }
        });
    }
}

function ConfigurePopover() {
    $('#dtDefectsList').on('click', 'a.documentPopup', function () {
        InitializePopoverConstructor();
        $('body').addClass('popover-design');
        var data = dtDefectsList.row($(this).parents('tr')).data();
        var uniqueClsSel = 'universalIdentifier_' + data.defectWorkOrderId;
        if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
            DefectDocumentsPopover(uniqueClsSel, data.defectWorkOrderId);
        }
        else {
            $('.' + uniqueClsSel).popover('show');
        }
    });
}

function InitializePopoverConstructor() {
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
}

function GetFormattedOnlyDate(data) {
    if (data == null) return "";
    var date = new Date(data);
    return moment(date).format("D MMM YYYY");
}

function ConfigureDownloadPopup() {
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
        var isWebAddressEditable = $(this).children('span.isWebAddressEditable').text();
        var webAddress = $(this).children('span.webAddress').text();
        var documentCategory = $(this).children('span.documentCategory').text();

        if (isWebAddressEditable != null && isWebAddressEditable != 'undefined' && isWebAddressEditable.trim().toLowerCase() == 'true') {
            window.open(webAddress, '_blank').focus();
        }
        else {
            var fileName = ''
            var input = {
                "identifier": documentId != null && documentId != 'undefined' ? documentId.trim() : '',
                "fileName": docfileName != null && docfileName != 'undefined' ? docfileName.trim() : '',
                "documentCategory": documentCategory != null && documentCategory != 'undefined' ? documentCategory.trim() : ''
            };
            fileName = documentName != null && documentName != 'undefined' ? documentName.trim() : '';

            $.ajax({
                url: "/Defect/DownloadDocument",
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
                        saveByteArray(fileName, array, data.fileType);
                    } else {
                        ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
                    }
                }
            });
        }
    });
}

function DefectDocumentsPopover(uniqueClsSel, defectWorkOrderId) {
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
        url: "/Defect/GetDefectDocuments",
        type: "POST",
        dataType: "JSON",
        global: false,
        data: {
            "DefectWorkOrderIds": defectWorkOrderId
        },
        beforeSend: function (xhr) {
            $('.' + uniqueClsSel).popover('show');
            $(".elementLoader").block({
                message: $(" " + loadercontent),
            });
        },
        success: function (data) {
            var jsonArray = data.data;
            var attachCount = jsonArray.length;
            if (attachCount > 1 || (attachCount == 1 && jsonArray[0].isWebAddressEditable == true)) {
                var html_content = "<div class='elementLoader scroller'><table class='table table-condensed table-borderless mb-0'><tbody>";
                for (var i = 0; i < attachCount; i++) {
                    html_content += "<tr>";
                    if (jsonArray[i].isWebAddressEditable) {
                        html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='/images/AttachmentLinkIcon.png' class='mt-2' width='18' title='View link'/>";
                    }
                    else {
                        html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='/images/Download-doc-active.png' class='m-0' width='18' title='Download'/>";
                    }
                    html_content += "<span class='documentName' > " + jsonArray[i].title + " </span >";
                    html_content += "<span class='documentId d-none'> " + jsonArray[i].ettId + " </span >";
                    html_content += "<span class='webAddress d-none'> " + jsonArray[i].webAddress + " </span >";
                    html_content += "<span class='documentCategory d-none'> " + jsonArray[i].documentCategory + " </span >";
                    html_content += "<span class='isWebAddressEditable d-none'> " + jsonArray[i].isWebAddressEditable + " </span >";
                    html_content += "<span class='documentfileName d-none' > " + jsonArray[i].cloudFileName + " </span ></a></td > ";
                    html_content += "<td class='data-datetime-align'> " + GetFormattedOnlyDate(jsonArray[i].createdOn) + "</td>";
                    html_content += "</tr>";
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
            else if (attachCount == 1) {
                $('.' + uniqueClsSel).popover('dispose');
                $('.' + uniqueClsSel).attr('data-content', '');
                var defectDocMap = new Map();
                defectDocMap.set(0, {
                    documentName: jsonArray[0].title,
                    documentId: jsonArray[0].ettId,
                    documentFileName: jsonArray[0].cloudFileName,
                    documentCategory: jsonArray[0].documentCategory
                });
                DownloadSelectedAttachment(defectDocMap, false);
            }
        },
        complete: function () {
            $(".elementLoader").unblock();
        },
    });
}

function LoadSystemAreaTree() {
    $("#SystemAreaTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/Defect/GetSystemAreaTreeList",
            dataType: "json"
        }),
        init: function (event, data) {
            SetSystemAreaTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function SetSystemAreaTree() {
    $("#SystemAreaTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedSystemAreaIds').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });

    AppendTreeFilterDataInModel("#SystemAreaTree", "#filterSystemArea","#filterCard2");
}

function LoadPlannedForTree() {
    $("#PlannedForTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/Defect/GetDefectWorkOrderPlannedForTreeList",
            dataType: "json"
        }),
        init: function (event, data) {
            SetPlannedForTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function SetPlannedForTree() {
    $("#PlannedForTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedPlannedForIds').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });

    AppendTreeFilterDataInModel("#PlannedForTree", "#filterPlannedFor", "#filterCard3");
}

function LoadStatusTree() {
    $("#StatusTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/Defect/GetDefectWorkOrderStatusTreeList",
            dataType: "json"
        }),
        init: function (event, data) {
            SetStatusTree();
        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function SetStatusTree() {
    $("#StatusTree").fancytree("getTree").visit(function (node) {
        var treeIdsList = $('#SelectedStatusIds').val().split(',');
        treeIdsList.forEach(function () {
            if (treeIdsList.includes(node.key)) {
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });
    AppendTreeFilterDataInModel("#StatusTree", "#filterStatus", "#filterStatus");

}

function GetSystemAreaTree() {
    var tree = $('#SystemAreaTree');
    var nodes = tree.fancytree('getTree').getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedSystemAreaIds').val(selectednodes.join(","));
}

function GetPlannedForTree() {
    var tree = $('#PlannedForTree');
    var nodes = tree.fancytree('getTree').getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedPlannedForIds').val(selectednodes.join(","));
}

function GetStatusTree() {
    var tree = $('#StatusTree');
    var nodes = tree.fancytree('getTree').getSelectedNodes();
    var selectednodes = nodes.map(x => x.key);
    $('#SelectedStatusIds').val(selectednodes.join(","));
}

function GetUniqueChildArr(nodedata) {
    var parentArray = new Map();
    var statusArray = new Map();

    nodedata.forEach(function (node) {
        statusArray.set(node.title, node.title);
        parentArray.set(node.parent.title, node.parent.title);
    });
    parentArray.forEach((value, key) => {
        if (statusArray.has(key)) {
            statusArray.delete(key);
        }
    });

    return statusArray;
}

function GetFilterHtmlElement(statusArray) {
    var htmlElement = "";

    statusArray.forEach((value, key) => {
        htmlElement += '<div class="col-12 col-md-6 col-lg-4 col-xl-4">';
        htmlElement += '<div class="dashboard-counters-label"><span id="">' + value + '</span></div></div>';
    });

    return htmlElement;
}

function AppendGridTitle() {

    hideShowFilterDesign();
    $('#appliedFilterCount').text(defectFilterCount);

    $("#tableSubTitle").hide();
    var subtitle = $("#GridSubTitle").val();
    if (!IsNullOrEmptyOrUndefined(subtitle) && subtitle != "All") {
        $("#tblspndtrdefectslist").hide();
        $("#tableSubTitle").show();
        $("#tableSubTitle").text(" - " + subtitle);
    }
    else {
        $("#tblspndtrdefectslist").show();
    }
}

function AppendTreeFilterDataInModel(treeSelector, filterId, filterCardId) {
    var StatusTree = $(treeSelector).fancytree('getTree').getSelectedNodes();
    var StatusTreeArray = GetUniqueChildArr(StatusTree);
    var StatusHtmlElement = GetFilterHtmlElement(StatusTreeArray);

    if (StatusTreeArray.size > 0) {
        $(filterId).html(StatusHtmlElement);
        defectFilterCount = defectFilterCount + StatusTreeArray.size;
        $('#appliedFilterCount').text(defectFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).html("");
        $(filterCardId).hide();
    }

    if (treeSelector == "#SystemAreaTree") {
        SystemAreaFilterCountSet(StatusTreeArray.size);
    }
    else if (treeSelector == "#PlannedForTree") {
        PlannedForFilterCountSet(StatusTreeArray.size);
    }
    else if (treeSelector == "#StatusTree") {
        StatusFilterCountSet();
    }

    hideShowFilterDesign();
}

function AppendTextFilterDataInModel(filteredValue, filterId, filterCardId) {

    if (!IsNullOrEmptyOrUndefined(filteredValue) && filteredValue !== undefined) {
        defectFilterCount++;
        $(filterId).text(filteredValue);
        $('#appliedFilterCount').text(defectFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).text("");
        $(filterCardId).hide();
    }
    hideShowFilterDesign();
}

function hideShowFilterDesign() {
    if (defectFilterCount > 0) {
        $(".filter-design, .clear-filter").show();
        $("#divfilterhide").removeClass('btn-dark-grey');
        $("#divfilterhide").addClass('btn-info');
    }
    else {
        $(".filter-design, .clear-filter").hide();
        $("#divfilterhide").removeClass('btn-info');
        $("#divfilterhide").addClass('btn-dark-grey');
    }
}

function KeywordFilterCountSet(Data) {
    if (!IsNullOrEmptyOrUndefined(Data)) {
        $('#keywordFilterCount').text('1');
        AddClassIfAbsent($("#keywordFilterCount").parent('div'), 'active');
    }
    else {
        $('#keywordFilterCount').text('0');
        RemoveClassIfPresent($("#keywordFilterCount").parent('div'), 'active');
    }
}

function GetTreeNodeLength(element) {
    var treeData = $(element);
    var selectedNodes = treeData.fancytree('getTree').getSelectedNodes();
    var TreeNodeArray = GetUniqueChildArr(selectedNodes);
    return TreeNodeArray.size;
}

function SystemAreaFilterCountSet(nodeLength) {
    $('#systemAreaFilterCount').text(nodeLength);
    if (nodeLength > 0)
        AddClassIfAbsent($('#systemAreaFilterCount').parent('div'), 'active');
    else
        RemoveClassIfPresent($('#systemAreaFilterCount').parent('div'), 'active');
}

function PlannedForFilterCountSet(nodeLength) {
    $('#plannedForFilterCount').text(nodeLength);
    if (nodeLength > 0)
        AddClassIfAbsent($('#plannedForFilterCount').parent('div'), 'active');
    else
        RemoveClassIfPresent($('#plannedForFilterCount').parent('div'), 'active');
}

function StatusFilterCountSet() {

    var count = 0;
    var defectDueStatusVal = $('input[type = radio][name = defectDueStatus]:checked').val();
    var criticalStatusVal = $('input[type = radio][name = criticalStatus]:checked').val();
    count = GetTreeNodeLength("#StatusTree");
    //if (!IsNullOrEmptyOrUndefinedLooseTyped(StatusTreeLen) && StatusTreeLen != undefined) {
    //    count = StatusTreeLen;
    //}
    //else {
    //    count = GetTreeNodeLength("#StatusTree");
    //}

    if (!IsNullOrEmptyOrUndefined(defectDueStatusVal) && defectDueStatusVal != "All") {
        count ++;
    }

    if (!IsNullOrEmptyOrUndefined(criticalStatusVal) && criticalStatusVal != "All") {
        count ++;
    }
    FilterCountSet(count, "#statusFilterCount");
}

function FilterCountSet(nodeLength, elementCount) {
    $(elementCount).text(nodeLength);
    if (nodeLength > 0)
        AddClassIfAbsent($(elementCount).parent('div'), 'active');
    else
        RemoveClassIfPresent($(elementCount).parent('div'), 'active');
}