import "bootstrap";
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import * as JSZip from "jszip";
import moment from "moment";
import { DateFormat, DateRangePickerCancelText, DateRangePickerLabelText, FinanceGeneralLedgerTransactionPageKey, MobileScreenSize } from "../common/constants.js";
import { CustomizedExcelHeader, GetExportCellData, GetExportFormattedDate, GetExportFormattedDecimal, GetExportFormattedDecimalKPI, GetExportData } from "../common/datatablefunctions.js";
import { AjaxError, AddLoadingIndicator, createFlatRadioButton, GetCookie, RemoveLoadingIndicator, ToastrAlert, BackButton, IsNullOrEmptyOrUndefined, datepickerheightinmobile, RegisterTabSelectionEvent } from "../common/utilities.js";
import { ClearSelectedLookUp, GetSelectedLookUpDetails, SetSelectedLookUp } from "../master/lookup/accountcodeLookUp";
window.JSZip = JSZip;
var selectedStartDate, selectedEndDate;
var generalLedgerFilterCount = 0;
var financialPeriodsArray = new Map();
var dtGLTransaction;
const NotSelected = 'Not Selected';

$(document).on('click', '#aRemoveFilter', function () {
    clearForm();
});

$(document).ready(function () {
    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();
    BackButton(FinanceGeneralLedgerTransactionPageKey, false);
    //Mobile ui changes
    $(document).click(function () {
        if ($("#mobileActiondropdown").hasClass('show')) {
            $("#mobileActiondropdown").removeClass('show');
        }
    });
    RegisterTabSelectionEvent('.mobileTabClick', FinanceGeneralLedgerTransactionPageKey);
    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }
    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });

    //Datepicker section -
    selectedStartDate = $('#FromDate').val();
    selectedEndDate = $('#ToDate').val();

    setFinancialYearDate();
    GetFinancialYears();

    $('#btnSearch').click(function () {
        Search();
    });

    //AccounntCode
    SetSelectedLookUp($('#AccountName').val(), $('#AccountCode').val());

    $('#spanAccountCodeName').text($('#AccountNameDescription').val());

    //Summary section
    BindLedgerSummary();

    //Load Data table
    GetGeneralLedgerTransactionList();

    $('#btnClear').click(function () {
        clearForm();
    });

    $('.btnExport').click(() => {
        $('#dtGLTransaction.cardview thead').addClass("export-grid-show");
        $('#dtGLTransaction').DataTable().buttons(0, 2).trigger();
        $('#dtGLTransaction.cardview thead').removeClass("export-grid-show");
    });

    $('#filterdata').on('show.bs.modal', function (e) {
        if ($('#filterFinancialPeriodRow').css('display') == 'none'
            && $('#filterDateRangeRow').css('display') == 'none') {
            $("#filterCard1").hide();
        }
        else {
            $("#filterCard1").show();
        }
    });
});

function GetGeneralLedgerTransactionList() {
    let input = GetInputRequest();
    let BaseCoyCurr = $('#BaseCoyCurr').val();
    var localStartDate = moment(selectedStartDate, 'DD-MM-YYYY');
    if (localStartDate.isValid() == false) {
        localStartDate = moment(selectedStartDate, 'DD MMM YYYY');
    }
    var formatedStartDate = localStartDate.format("DD MMM YYYY");

    var localEndDate = moment(selectedEndDate, 'DD-MM-YYYY');
    if (localEndDate.isValid() == false) {
        localEndDate = moment(selectedEndDate, 'DD MMM YYYY');
    }
    var formatedEndDate = localEndDate.format("DD MMM YYYY");
    $('#dtGLTransaction').DataTable().destroy();

    dtGLTransaction = $('#dtGLTransaction').DataTable({
        //"dom": '<<"row mb-3"<"col-12 col-md-6 offset-md-0 col-lg-2 offset-lg-1 col-xl-2 offset-xl-1 dt-infomation"i><"col-md-3 col-lg-5 col-xl-5 filters-data"><"col-12 col-md-6 col-lg-4 col-xl-4"f>><rt><"clearfix"<"float-left"l><""p>>>',
        //"dom": '<"row"<"col-12 col-md-6 col-lg-6 col-xl-6"i><"col-12 col-md-6 col-lg-6 col-xl-6 search-filter"f>>' +
        //    '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
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
        "order": [],
        "language": {
            "emptyTable": "No transaction available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/Finance/GetGeneralLedgerTransactionList",
            "type": "POST",
            "data":
            {
                "filters": input
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
                title: "Transaction List",
                customize: function (xlsx) {
                    CustomizedExcelHeader(xlsx, 2);
                },
                messageTop: function () {
                    return 'Vessel : ' + $('#VesselName').val() + '\nFrom Date : ' + formatedStartDate + '\nTo Date : ' + formatedEndDate + '\nAccount Code : ' + $('#AccountName').val();
                }
            },
            'pdf', 'print'
        ],
        "columns": [
            {
                className: "tdblock hide-desktop-tr data-text-align",
                "data": "voucherNo",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetExportData(data);
                }
            },
            {
                className: "data-datetime-align",
                "data": "tranxDate",
                width: "55px",
                render: function (data, type, full, meta) {
                    return GetExportFormattedDate(type, "Date", data);
                }
            },
            {
                className: "hide-mobile-tr data-text-align",
                "data": "voucherNo",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetExportCellData("Voucher No.", data);
                }
            },
            {
                className: "data-text-align",
                "data": "journalType",
                width: "35px",
                render: function (data, type, full, meta) {
                    return GetExportCellData("Type", data);
                }
            },
            {
                className: "tdblock data-text-align",
                "data": "text",
                width: "230px",
                render: function (data, type, full, meta) {
                    return GetExportCellData("Text", data);
                }
            },
            {
                className: "data-text-align",
                "data": "reference",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetExportCellData("Reference", data);
                }
            },
            {
                className: "data-text-align",
                "data": "currency",
                width: "25px",
                render: function (data, type, full, meta) {
                    return GetExportCellData("Cur", data);
                }
            },
            {
                className: "data-number-align",
                "data": "amountOriginal",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetExportFormattedDecimalKPI(type, 'Original </br> Amount', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetExportFormattedDecimal(type, 'Original </br> Amount', data, 2, '0.00');
                    }
                }
            },
            {
                className: "data-number-align",
                "data": "amount",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetExportFormattedDecimalKPI(type, 'Functional Amount (' + BaseCoyCurr + ')', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetExportFormattedDecimal(type, 'Functional Amount (' + BaseCoyCurr + ')', data, 2, '0.00');
                    }
                }
            },
            {
                className: "data-number-align",
                "data": "runningBalance",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetExportFormattedDecimalKPI(type, 'Running Balance (' + BaseCoyCurr + ')', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetExportFormattedDecimal(type, 'Running Balance (' + BaseCoyCurr + ')', data, 2, '0.00');
                    }
                }
            },
            {
                className: "data-text-align",
                "data": "isOpen",
                width: "25px",
                render: function (data, type, full, meta) {
                    return '<label> Is Open </label><br />' + '<span class="d-block mobile-top-20 export-Data">' + data + '</span>';
                }
            },
        ],
        "initComplete": function (settings, data) {
            BindAppliedFilter();
        }
    });
    //$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata"  class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');
}

function GetInputRequest() {
    let inputReq = {
        "ToDate": selectedEndDate,
        "FromDate": selectedStartDate,
        "AccountCode": $('#AccountCode').val(),
        "AccountingCompanyId": $('#AccountingCompanyId').val(),
        "FinancialYearStartDate": $('#FinancialYearStartDate').val(),
        "ChhId": $('#ChhId').val()
    }
    return inputReq;
}

//Search click
function Search() {
    var selectedAccountCodeLookup = GetSelectedLookUpDetails();
    $('#AccountCode').val(selectedAccountCodeLookup.id);
    $('#AccountName').val(selectedAccountCodeLookup.description);

    if (IsStringEmpty(selectedAccountCodeLookup.name)) {
        $('#spanAccountCodeName').text(NotSelected);
        $('#AccountNameDescription').val(NotSelected);
    }
    else {
        $('#spanAccountCodeName').text(selectedAccountCodeLookup.name);
        $('#AccountNameDescription').val(selectedAccountCodeLookup.name);
    }

    let input = {
        "ToDate": selectedEndDate,
        "FromDate": selectedStartDate,
        "AccountCode": $('#AccountCode').val(),
        "AccountingCompanyId": $('#AccountingCompanyId').val(),
        "FinancialYearStartDate": $('#FinancialYearStartDate').val(),
        "ChhId": $('#ChhId').val(),
        "AccountName": $('#AccountName').val(),
        "finPeriod": $("#finPeriod").val(),
        "AccountNameDescription": $('#AccountNameDescription').val(),
    }

    $.ajax({
        url: "/Finance/SetGeneralLedgerTransactionPageParameter",
        type: "POST",
        data: input,
        success: function (data) {
            GetGeneralLedgerTransactionList();
        }
    });
}

function IsStringEmpty(inputText) {
    if (inputText != '' && inputText != 'undefined' && inputText != null) {
        return false;
    }
    return true;
}

//Summary
function BindLedgerSummary() {
    $.ajax({
        url: "/Finance/GetSummaryDetails",
        type: "GET",
        dataType: "JSON",
        data: {
            "VesselId": $('#EncryptedVesselId').val()
        },
        success: function (data) {
            data = data.data;

            $("#spanOwner").text(data.vmdOwner);
            $("#spanFuncCurr").text(data.baseCurrency);
            $("#spanFinancialYearRange").text(data.financialYearStartDate + " - " + data.financialYearEndDate);
            $("#spanMgmtStartDate").text(data.managementStartDate);
            $("#spanLedgerCutOffDate").text(data.generalLedgerCutOffDate);
        }
    });
}

//Date dropdown in Search Filter 
//Doc ready 
function GetFinancialYears() {
    $.ajax({
        url: "/Finance/GetFinancialYears",
        type: "GET",
        dataType: "JSON",
        data: {
            "accountingCompanyId": $("#AccountingCompanyId").val()
        },
        success: function (data) {
            $("#FinancialYearRadiosContainer").html("");
            var response = data.accountingFinancialYears;

            for (let i = 0; i < response.length; i++) {
                let opt = response[i];
                let financialYearRadio = createFlatRadioButton("financialYears" + i, "financialYears", opt.period, opt.dateRange);
                $("#FinancialYearRadiosContainer").append(financialYearRadio);

                let financialYearDateRange = { StartDate: opt.startDate, EndDate: opt.endDate }
                financialPeriodsArray.set(opt.period, financialYearDateRange);
            }

            let financialYearRadio = createFlatRadioButton("financialYears00", "financialYears", '0', 'Custom');
            $("#FinancialYearRadiosContainer").append(financialYearRadio);

            var localStartDate = moment($("#MinStartDate").val(), 'DD-MM-YYYY');
            var localEndDate = moment($("#MaxEndDate").val(), 'DD-MM-YYYY');

            let financialYearDateRange = { StartDate: localStartDate, EndDate: localEndDate }
            financialPeriodsArray.set(0, financialYearDateRange);
        },
        complete: function () {
            $('input[type=radio][name=financialYears]').on('change', function () {
                financialPeriodYearChange(this);
            });

            if ($("#finPeriod").val() != "" && $("#finPeriod").val() != undefined) {
                financialPeriodYearChange(null);
            }
        }
    });
};

function financialPeriodYearChange($this) {
    var selected = 0;
    if ($this == null || $this == undefined) {
        selected = Number($("#finPeriod").val());
        $('input[type=radio][name=financialYears]').prop('checked', false);

        var $selRadio = $('input[type=radio][name=financialYears]');
        if (selected != "" && selected != undefined) {
            $selRadio.filter('[value=' + selected + ']').prop('checked', true);
        }
    }
    else {
        selected = Number($this.value);
    }
    $("#finPeriod").val(selected);

    //Custom radio buttion click
    if (selected == 0) {

        var localStartDate = moment($("#MinStartDate").val(), 'DD-MM-YYYY');
        var localEndDate = moment($("#MaxEndDate").val(), 'DD-MM-YYYY');

        var localFinStartDate = moment($("#CoyFinStartDate").val(), 'DD-MM-YYYY');
        var localFinEndDate = moment($("#CoyFinEndDate").val(), 'DD-MM-YYYY');

        var formatedFinStartDate = localFinStartDate.format("DD MMM YYYY");
        var formatedFinEndDate = localFinEndDate.format("DD MMM YYYY");
        DaterangePicker(localStartDate, localEndDate, localFinStartDate, localFinEndDate, true);

        $('#dateFinancePeriod').val(formatedFinStartDate + ' - ' + formatedFinEndDate);
    }
    else {

        var localStartDate = moment(financialPeriodsArray.get(selected).StartDate);
        var formatedStartDate = localStartDate.format("DD MMM YYYY");

        var localEndDate = moment(financialPeriodsArray.get(selected).EndDate);
        var formatedEndDate = localEndDate.format("DD MMM YYYY");

        selectedStartDate = localStartDate.format('DD-MM-YYYY');
        selectedEndDate = localEndDate.format('DD-MM-YYYY');

        DaterangePicker(localStartDate, localEndDate, localStartDate, localEndDate, true);

        $('#dateFinancePeriod').val(formatedStartDate + ' - ' + formatedEndDate);
    }

    $("#spnDateRangeValidationMsg").addClass('d-none');
    $("#btnSearch").prop('disabled', false);
}

function DaterangePicker(minDate, maxDate, startDate, endDate, isHideOutofRange) {

    $("#dateFinancePeriod").caleran(
        {
            showButtons: true,
            hideOutOfRange: isHideOutofRange,
            showOn: "top",
            arrowOn: "right",
            startEmpty: true,
            cancelLabel: "Clear",
            format: "DD MMM YYYY",
            minDate: minDate,
            maxDate: maxDate,
            startDate: startDate,
            endDate: endDate,
            showFooter: false,
            onafterselect: function (caleran, startDate, endDate) {
                setDateDetails(startDate, endDate);
            },
            oncancel: function (caleran, start, end) {
                setDateDetailonCancel();
                return true;
            }
        }
    );

}

function setDateDetails(startDate, endDate) {
    $('#dateFinancePeriod').val(startDate.format(DateFormat) + ' - ' + endDate.format(DateFormat));
    selectedStartDate = startDate.format(DateFormat);
    selectedEndDate = endDate.format(DateFormat);
    validateFinancialDateRange();
}

function setDateDetailonCancel() {
    selectedStartDate = '';
    selectedEndDate = '';
    $('#dateFinancePeriod').val(DateRangePickerLabelText);
    setFinancialYearDate();

}


//Clear and doc ready
function setFinancialYearDate() {

    var startDate = moment($('#FromDate').val(), 'DD-MM-YYYY');
    var endDate = moment($('#ToDate').val(), 'DD-MM-YYYY');

    var formatedStartDate = startDate.format("DD MMM YYYY");
    var formatedEndDate = endDate.format("DD MMM YYYY");

    var localStartDate = moment($("#MinStartDate").val(), 'DD-MM-YYYY');
    var localEndDate = moment($("#MaxEndDate").val(), 'DD-MM-YYYY');

    DaterangePicker(localStartDate, localEndDate, startDate, endDate, false);

    $('#dateFinancePeriod').val(formatedStartDate + ' - ' + formatedEndDate);

    $("#spnDateRangeValidationMsg").addClass('d-none');
    $("#btnSearch").prop('disabled', false);
}


function validateFinancialDateRange() {
    var stratFinYear = moment(selectedStartDate).format("YYYY");
    var endFinYear = moment(selectedEndDate).format("YYYY");

    var stratFinMonth = moment(selectedStartDate).format("MM");
    var endFinMonth = moment(selectedEndDate).format("MM");

    var stratFinDate = moment(selectedStartDate).format("DD");
    var endFinDate = moment(selectedEndDate).format("DD");

    var finStartDate = $("#finStartDate").val();
    var finEndDate = $("#finEndDate").val();
    var finStartMonth = $("#finStartMonth").val();
    var finEndMonth = $("#finEndMonth").val();

    var finStartfullDate = moment([finStartDate, finStartMonth, stratFinYear], 'DD-MM-YYYY');
    var finEndfullDate = moment([finEndDate, finEndMonth, endFinYear], 'DD-MM-YYYY');

    var dpStartDate = moment([stratFinDate, stratFinMonth, stratFinYear], 'DD-MM-YYYY');
    var dpEndDate = moment([endFinDate, endFinMonth, endFinYear], 'DD-MM-YYYY');

    var localFinStartDate = moment($("#CoyFinStartDate").val(), 'DD-MM-YYYY');
    var localFinEndDate = moment($("#CoyFinEndDate").val(), 'DD-MM-YYYY');
    let isStartDateInFinYear = moment(dpStartDate).isBetween(localFinStartDate, localFinEndDate);

    if (isStartDateInFinYear) {
        if (!$("#spnDateRangeValidationMsg").hasClass('d-none')) {
            $("#spnDateRangeValidationMsg").addClass('d-none');
        }
        $("#btnSearch").prop('disabled', false);
    }
    else if (finEndfullDate.diff(finStartfullDate, 'days') > 365) {
        $("#spnDateRangeValidationMsg").removeClass('d-none');
        $("#btnSearch").prop('disabled', true);
    }
    else {
        if ((moment(finStartfullDate).isSameOrBefore(dpStartDate)) && (moment(dpEndDate).isSameOrBefore(finEndfullDate))) {
            $("#spnDateRangeValidationMsg").addClass('d-none');
            $("#btnSearch").prop('disabled', false);
        }
        else {
            $("#spnDateRangeValidationMsg").removeClass('d-none');
            $("#btnSearch").prop('disabled', true);
        }
    }
}

function BindAppliedFilter() {
    generalLedgerFilterCount = 0;

    var fyPeriodVal = $('input[name="financialYears"]:checked').data("description");
    AppendTextFilterDataInModel(fyPeriodVal, "#filterFinancialPeriod", "#filterFinancialPeriodRow");

    var dateFinancePeriodVal = $("#dateFinancePeriod").val();
    dateFinancePeriodVal = dateFinancePeriodVal == DateRangePickerLabelText ? null : dateFinancePeriodVal;
    AppendTextFilterDataInModel(dateFinancePeriodVal, "#filterDateRange", "#filterDateRangeRow");

    var searchLookUpVal = $('#AccountCode').val() + " - " + $('#AccountName').val();
    searchLookUpVal = searchLookUpVal === " - " ? null : searchLookUpVal;
    AppendTextFilterDataInModel(searchLookUpVal, "#filterAccountSearch", "#filterCard2");
}

function AppendTextFilterDataInModel(filteredValue, filterId, filterCardId) {

    if (!IsNullOrEmptyOrUndefined(filteredValue) && filteredValue !== undefined) {
        generalLedgerFilterCount++;
        $(filterId).text(filteredValue);
        $('#appliedFilterCount').text(generalLedgerFilterCount);
        $(filterCardId).show();
    }
    else {
        $(filterId).text("");
        $(filterCardId).hide();
    }
    hideShowFilterDesign();
}

function hideShowFilterDesign() {
    if (generalLedgerFilterCount > 0) {
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

function clearForm() {
    selectedStartDate = $('#FromDate').val();
    selectedEndDate = $('#ToDate').val();
    $('#AccountCode').val("");
    $('#AccountNameDescription').val("");
    $('#AccountName').val("");
    $("#finPeriod").val("0");
    setFinancialYearDate();
    ClearSelectedLookUp();
    $('input[type=radio][name=financialYears]').prop('checked', false);
    GetGeneralLedgerTransactionList();
    $('#spanAccountCodeName').text(NotSelected);
}