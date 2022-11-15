import "select2/dist/js/select2.full.js";
import { createTree } from "jquery.fancytree";
import moment from "moment";
import toastr from "toastr";
import * as JSZip from "jszip";

window.JSZip = JSZip;

require('bootstrap');

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, AddClassIfAbsent, BackButton, IsNullOrEmptyOrUndefined, GetChatNotesBaseIcons, InitializeListDiscussionAndNoteClickEvents, GetChatBaseIcons, GetNotesBaseIcons, IsNullOrEmptyOrUndefinedLooseTyped, GetValueOrDefaultDT, RegisterTabSelectionEvent, datepickerheightinmobile } from "../common/utilities.js"
import { DateRangePickerLabelText, DateRangePickerCancelText, ProcurementListPageKey, MobileScreenSize } from "../common/constants.js"
import { GetFormattedDate } from "../common/datatablefunctions.js"
import { GetSelectedCompanyDetails, SetSelectedCompay, ClearSelectedCompany } from "../master/lookup/supplierLookUp"

var filter;
var gridPurchaseOrder;
var ispageLoad;

var headerIsAppend = false;
var IsMobile = false;
var selectedStartDate, selectedEndDate;
var isOrderStatusChanged;
var statusMap;
var defaultDateFormatCalendarDisplay = 'DD MMM YYYY'

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
    clearFilter();
});

$(document).ready(function () {
    AjaxError();
    BackButton(ProcurementListPageKey, true);
    InitializeListDiscussionAndNoteClickEvents(code);

    $('.height-equal').matchHeight();
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        IsMobile = true;
    } else {
        IsMobile = false;
    }
    ispageLoad = true;

    AddLoadingIndicator();
    RemoveLoadingIndicator();

    RegisterTabSelectionEvent('.mobileTabClick', ProcurementListPageKey);

    $('#btnSearch').click(function () {

        SetPageParameter(true);
    });

    $('#btnClear').click(function () {
        clearFilter();
    });

    $('#btnOnHoldActivity').click(function () {
        $('#holdOrderConfirmation').modal("show");
    });

    $('#btnChangeDeliveryStatus').click(function () {
        $('#convertOrderConfirmation').modal("show");
    });

    $("#dtrOrderlist").caleran(
        {
            showButtons: true,
            hideOutOfRange: true,
            showOn: "top",
            arrowOn: "right",
            startEmpty: true,
            format: "DD MMM YYYY",
            cancelLabel: "Clear",
            ranges: [
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
                    title: "This Month",
                    startDate: moment().startOf("month"),
                    endDate: moment().endOf("month")
                },
                {
                    title: "Last Month",
                    startDate: moment().subtract(1, "month").startOf("month"),
                    endDate: moment().subtract(1, "month").endOf("month")
                },
                {
                    title: "Last 6 Months",
                    startDate: moment().subtract(6, "month"),
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


    function setDateDetails(startDate, endDate) {
        $('#dtrOrderlist').html(startDate.format("DD MMM YYYY") + ' - ' + endDate.format("DD MMM YYYY"));

        $('#tblspndtrOrderlist').html(', ' + startDate.format("DD MMM YYYY") + ' - ' + endDate.format("DD MMM YYYY"));

        selectedStartDate = startDate.format("DD MMM YYYY");
        selectedEndDate = endDate.format("DD MMM YYYY");
        SetPageParameter(true);
    }


    function setDateDetailonCancel() {
        $('#dtrOrderlist').html(DateRangePickerLabelText);
        ResetTblheading();
        if (doesSearchBoxHasValues()) {
            selectedStartDate = ''
            selectedEndDate = ''
            SetPageParameter(true);
        } else {
            $('#btnClear').click();
        }
    }

    $("#orderName").on('input', function () {
        SetSearchButtonAccess();
    });

    $("#orderNumber").on('input', function () {
        SetSearchButtonAccess();
    });

    $(document).keyup(function (e) {
        if (e.keyCode == 13) {
            var isButtonEnabled = $('#btnSearch').prop('disabled')
            if ($(".search-sidebar-div").hasClass("search-sidebar-open") && !isButtonEnabled) {
                SetPageParameter(true);
            }
        }
    });

    $('#btnExport').click(() => {

        var purchaseRequest = {
            "pOStage": $('#POStage').val(),
            "fromDate": selectedStartDate,
            "toDate": selectedEndDate,
            "vesselId": $('#VesselId').val(),
            "searchOrderNumber": $('#orderNumber').val(),
            "purchaseOrderTypes": $('#PurchaseOrderTypes').val(),
            "IsSearchClicked": $('#IsSearchClicked').val(),
            "SelectedOrderStage": $('#SelectedOrderStage').val(),
            "Title": $('#orderName').val(),
            "SupplierId": $('#SupplierId').val(),
            "SupplierName": $('#SupplierName').val(),
            "SearchFilter": filter,
            "SelectedStatus": $("#SelectedStatus").val(),
            "ShowOnlyAuthRequired": $("#ShowOnlyAuthRequired").val(),
            "strOrderStatusIds": $('#strOrderStatusIds').val(),
        }

        $.ajax({
            url: "/PurchaseOrder/ExportToExcelPurchaseOrderList",
            type: "POST",
            xhrFields: {
                responseType: 'blob'
            },
            "data": {
                "purchaseRequest": purchaseRequest
            },
            success: function (data, textStatus, xhr) {
                var filename = "";
                var disposition = xhr.getResponseHeader('Content-Disposition');
                if (disposition && disposition.indexOf('attachment') !== -1) {
                    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    var matches = filenameRegex.exec(disposition);
                    if (matches != null && matches[1])
                        filename = matches[1].replace(/['"]/g, '');
                    var a = document.createElement('a');
                    var url = window.URL.createObjectURL(data);
                    a.href = url;
                    a.download = filename;
                    document.body.append(a);
                    a.click();
                    a.remove();
                    window.URL.revokeObjectURL(url);
                }
            }
        });
    });

    LoadOrderStatusTree();
    SetFilters();
    LoadGrid();
    BindSummary();

    $('#aShowFilterData').click(function () {
        ShowFilterDetails();
    });
});

function SetFilters() {
    $('#collapseSearchFilter').removeClass('collapse');
    $('#orderNumber').val($('#SearchOrderNumber').val());
    $('#orderName').val($('#Title').val());

    if ($('#SupplierId').val() != null && $('#SupplierId').val() != '' && $('#SupplierId').val() != 'undefined') {
        SetSelectedCompay($('#SupplierName').val(), $('#SupplierId').val());
    }

    let apiStartDate = moment($('#FromDate').val(), 'DD-MM-YYYY');
    let apiEndDate = moment($('#ToDate').val(), 'DD-MM-YYYY');
    SetDateFilters(apiStartDate, apiEndDate);

    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }

    SetSearchButtonAccess();
}

function SetDateFilters(localStartDate, localEndDate) {
    $("#tblspndtrOrderlist").html("");
    if ($('#FromDate').val() != null && $('#FromDate').val() != 'undefined' && $('#FromDate').val() != '') {
        selectedStartDate = localStartDate.format("DD MMM YYYY");
    }
    else {
        selectedStartDate = '';
    }
    if ($('#ToDate').val() != null && $('#ToDate').val() != 'undefined' && $('#ToDate').val() != '') {
        selectedEndDate = localEndDate.format("DD MMM YYYY");
    }
    else {
        selectedEndDate = '';
    }

    if (selectedStartDate == '' && selectedEndDate == '') {
        $('#dtrOrderlist').html(DateRangePickerLabelText);
    } else if (selectedStartDate != 'Invalid date' && selectedStartDate != 'Invalid date') {
        $('#dtrOrderlist').html(selectedStartDate + ' - ' + selectedEndDate);
        $('#tblspndtrOrderlist').html(', ' + selectedStartDate + ' - ' + selectedEndDate);
    }
}

function LoadGrid() {
    if ($('#POStage').val() == 'InProcess') {
        filter = "InProcess";
        AppendGridTitle(" - In Progress");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'Ordered') {
        filter = "Ordered";
        AppendGridTitle(" - Ordered");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'Dispatched') {
        filter = "Dispatched";
        AppendGridTitle(" - Dispatched");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'AuthenticationRequired') {
        filter = "AuthenticationRequired";
        AppendGridTitle(" - Auth. Enq.");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'Received30Days') {
        filter = "Received30Days";
        AppendGridTitle(" - Received (30 Days)");
        $("#tableSubTitle").text("- Received (30 Days)");
        $("#tableSubTitle").show();
        $("#tblspndtrOrderlist").hide();

        if ($('#IsSearchClicked').val().toLowerCase() == 'true') {
            $("#tableSubTitle").hide();
            $("#tableSubTitle").text("");
            $("#tblspndtrOrderlist").show();
        }

        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'AuthorisationRequired') {
        filter = "AuthorisationRequired";
        AppendGridTitle(" - Awaiting Snr. Auth.");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'TenderAwaitingAuthorization') {
        filter = "TenderAwaitingAuthorization";
        AppendGridTitle(" - Tender Awaiting Auth.");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'Requisitions') {
        filter = "Requisitions";
        AppendGridTitle(" - Requisitions");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'Enquiries') {
        filter = "Enquiries";
        AppendGridTitle(" - Enquiries");
        LoadPurchaseOrdersGrid(filter);
    }
    else if ($('#POStage').val() == 'OnHold') {
        filter = "OnHold";
        AppendGridTitle(" - On Hold");
        LoadPurchaseOrdersGrid(filter);
    }
}

function SetHiddenFields(response) {
    $("#FromDate").val(response.fromDate);
    $("#ToDate").val(response.toDate);
    $("#VesselId").val(response.vesselId);
    $("#AccountCompanyId").val(response.accountCompanyId);
    $("#POStage").val(response.poStage);
    $("#PurchaseOrderTypes").val(response.purchaseOrderTypes);
    $("#SearchOrderNumber").val(response.searchOrderNumber);
    $("#SelectedOrderStage").val(response.selectedOrderStage);
    $("#IsSearchClicked").val(response.isSearchClicked);
    $("#Title").val(response.title);
    $("#SupplierId").val(response.supplierId);
    $("#SupplierName").val(response.supplierName);
    $("#SelectedStatus").val(response.selectedStatus);
    $("#ActiveMobileTabClass").val(response.activeMobileTabClass);
    $("#ShowOnlyAuthRequired").val(response.showOnlyAuthRequired);
    $('#strOrderStatusIds').val(response.strOrderStatusIds);
}

function SetSearchButtonAccess() {
    if (doesSearchBoxHasValues()) {
        $("#btnSearch").prop('disabled', false);
    } else {
        $("#btnSearch").prop('disabled', true);
    }
}

function NotNullAndNotEmpty(data) {
    return data != null && data != '';
}
function doesSearchBoxHasValues() {
    let orderNumber = $("#orderNumber").val();
    let orderName = $("#orderName").val();
    let selectedSupplierId = $('#SupplierId').val();
    let isorderStatusTree = false;

    var orderStatusTree = $('#orderStatusTree');
    var selectedOrderStatusNodes = orderStatusTree.fancytree('getTree').getSelectedNodes();

    if (selectedOrderStatusNodes != undefined && selectedOrderStatusNodes != null && selectedOrderStatusNodes != "") {
        isorderStatusTree = true;
    }
    else {
        isorderStatusTree = false;
    }

    return NotNullAndNotEmpty(orderName) || NotNullAndNotEmpty(orderNumber) || NotNullAndNotEmpty(selectedSupplierId) || isorderStatusTree;
}

function ResetTblheading() {
    $('#tblspndtrOrderlist').html('');
}

$("#orderNumber").keyup(function () {
    OrderStatus($(this).val());
});

$("#orderName").keyup(function () {
    OrderStatus($(this).val());
});

$(document).on('click', '#divSupplierSearch .rowTemplate', function () {
    $('#supplierFilterCount').text('1');
    AddClassIfAbsent($("#supplierFilterCount").parent('div'), 'active');
});

$(document).on('click', '#divSupplierSearch .removeSelection', function () {
    $('#supplierFilterCount').text('0');
    AddClassIfAbsent($("#supplierFilterCount").parent('div'), 'active');
});

function OrderStatus(Data) {
    if (!IsNullOrEmptyOrUndefined(Data)) {
        $('#orderFilterCount').text('1');
        AddClassIfAbsent($("#orderFilterCount").parent('div'), 'active');
    }
    else {
        $('#orderFilterCount').text('0');
        RemoveClassIfPresent($("#orderFilterCount").parent('div'), 'active');
    }
}

$('#orderStatusTree').click(function () {

    var orderStatusTree = $('#orderStatusTree');
    var selectedOrderStatusNodes = orderStatusTree.fancytree('getTree').getSelectedNodes();
    var selectedOrderStatus = selectedOrderStatusNodes.map(x => x.key);
    var SelectedArry = selectedOrderStatus.toString().split(",");
    SelectedArry = SelectedArry.filter(function (v) { return v !== '' && v !== 'All' });
    $('#statusFilterCount').text(SelectedArry.length);
    if (SelectedArry.length > 0)
        AddClassIfAbsent($('#statusFilterCount').parent('div'), 'active');
    else
        RemoveClassIfPresent($('#statusFilterCount').parent('div'), 'active');
});


function SetPageParameter(isSearchClicked) {

    var selectedSupplierLookup = GetSelectedCompanyDetails();
    $('#SupplierId').val(selectedSupplierLookup.id);
    $('#SupplierName').val(selectedSupplierLookup.name);

    var orderStatusTree = $('#orderStatusTree');
    var selectedOrderStatusNodes = orderStatusTree.fancytree('getTree').getSelectedNodes();
    var selectedOrderStatus = selectedOrderStatusNodes.map(x => x.key);

    var input = {
        "pOStage": $('#POStage').val(),
        "fromDate": selectedStartDate,
        "toDate": selectedEndDate,
        "vesselId": $('#VesselId').val(),
        "searchOrderNumber": $('#orderNumber').val(),
        "purchaseOrderTypes": $('#selOrderType').val(),
        "IsSearchClicked": isSearchClicked,
        "SupplierId": $('#SupplierId').val(),
        "SupplierName": $('#SupplierName').val(),
        "Title": $('#orderName').val(),
    }
    input["OrderStatusIds"] = selectedOrderStatus;

    $.ajax({
        url: "/PurchaseOrder/SetPageParameter",
        type: "POST",
        data: input,
        success: function (data) {
            if (data != null) {
                SetHiddenFields(data.data);
            }
        },
        complete: function () {
            LoadGrid();
        }
    });
}

function GetCellData(label, data) {
    return '<label>' + label + '</label><br />' + data;
}

function SetSummaryFilterInTempData(data) {
    $.ajax({
        url: "/PurchaseOrder/SetSummaryFilterInTempData",
        type: "POST",
        dataType: "JSON",
        data: {
            "purchaseOrderUrl": data,
            "vesselId": $("#VesselId").val()
        },

        success: function (data) {
            if (data != null) {
                SetHiddenFields(data.data);
                SetFilters();
                SetOrderStatusTree();
                SetSearchButtonAccess();
                LoadGrid();
            }
        }
    });
}

function BindSummary() {
    var request =
    {
        "vesselId": $('#VesselId').val()
    }
    $.ajax({
        url: "/PurchaseOrder/GetOrderSummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },

        success: function (data) {

            $('#spanPoInProcess').text(data.orderInProcessCount);
            $('#spanPoOrdered').text(data.orderedCount);
            $('#spanPoDispatched').text(data.orderDeliveryOnTheWayCount);
            $('#spanPoAuthEnq').text(data.authorisationCount);
            $('#spanPoRecieved').text(data.recievedIn30DaysCount);
            $('#spanTenderAwaitAuth').text(data.awaitingAuthorisationCount);
            $('#spanAuthRequired').text(data.authRequiredCount);
            $('#spanRequisitions').text(data.requisitionCount);
            $('#spanEnquiries').text(data.enquiriesCount);
            $('#spanOnHold').text(data.onHoldCount);

            if (data.orderInProcessCount == 0) {
                SetDefaultEmptyColor('#spanPoInProcess');
            } else {
                SetUrgentElseDefault(data.isOrderInProcessUrgent, '#spanPoInProcess');
            }

            if (data.orderedCount == 0) {
                SetDefaultEmptyColor('#spanPoOrdered');
            } else {
                SetUrgentElseDefault(data.isOrderedUrgent, '#spanPoOrdered');
            }

            if (data.orderDeliveryOnTheWayCount == 0) {
                SetDefaultEmptyColor('#spanPoDispatched');
            } else {
                SetUrgentElseDefault(data.isOrderDeliveryOnTheWayUrgent, '#spanPoDispatched');
            }

            if (data.authorisationCount == 0) {
                SetDefaultEmptyColor('#spanPoAuthEnq');
            } else {
                SetUrgentElseDefault(data.isAuthorisationUrgent, '#spanPoAuthEnq');
            }

            if (data.recievedIn30DaysCount == 0) {
                SetDefaultEmptyColor('#spanPoRecieved');
            } else {
                SetUrgentElseDefault(data.isRecievedIn30DaysUrgent, '#spanPoRecieved');
            }

            if (data.awaitingAuthorisationCount == 0) {
                SetDefaultEmptyColor('#spanTenderAwaitAuth');
            } else {
                SetUrgentElseDefault(data.isAwaitingAuthorisationUrgent, '#spanTenderAwaitAuth');
            }

            if (data.authRequiredCount == 0) {
                SetDefaultEmptyColor('#spanAuthRequired');
            } else {
                SetUrgentElseDefault(data.isAuthRequiredUrgent, '#spanAuthRequired');
            }

            if (data.requisitionCount == 0) {
                SetDefaultEmptyColor('#spanRequisitions');
            } else {
                SetUrgentElseDefault(data.isRequisitionUrgent, '#spanRequisitions');
            }

            if (data.enquiriesCount == 0) {
                SetDefaultEmptyColor('#spanEnquiries');
            } else {
                SetUrgentElseDefault(data.isEnquiriesUrgent, '#spanEnquiries');
            }

            if (data.onHoldCount == 0) {
                SetDefaultEmptyColor('#spanOnHold');
            } else {
                SetUrgentElseDefault(data.isOnHoldUrgent, '#spanOnHold');
            }

            $("#aPOInProcess").click(function () {
                SetSummaryFilterInTempData(data.inProcessURL)
            });

            $("#aPOOrdered").click(function () {
                SetSummaryFilterInTempData(data.orderURL)
            });

            $("#aPODispateched").click(function () {
                SetSummaryFilterInTempData(data.dispatchedURL)
            });

            $("#aPOAuthEnq").click(function () {
                SetSummaryFilterInTempData(data.authEnqURL)
            });

            $("#aPOReceieved").click(function () {
                SetSummaryFilterInTempData(data.recievedURL)
            });

            $("#aTenderAwaitAuth").click(function () {
                SetSummaryFilterInTempData(data.tenderAwaitingAuthURL)
            });

            $("#aAuthRequired").click(function () {
                SetSummaryFilterInTempData(data.authRequiredURL)
            });

            $("#aRequisitions").click(function () {
                SetSummaryFilterInTempData(data.requisitionsURL)
            });

            $("#aEnquiries").click(function () {
                SetSummaryFilterInTempData(data.enquiriesURL);
            });

            $("#aOnHold").click(function () {
                SetSummaryFilterInTempData(data.onHoldUrl);
            });
        },
        complete: function () {
            $('.height-equal').matchHeight();
        }
    });
}

function SetDefaultEmptyColor(jqueryObject) {
    AddClassIfAbsent($(jqueryObject), 'txt-color-gray');
}
function SetUrgentElseDefault(data, jqueryObject) {
    if (data) {
        AddClassIfAbsent($(jqueryObject), 'txt-red');
    } else {
        AddClassIfAbsent($(jqueryObject), 'txt-blue');
    }
}

function LoadPurchaseOrdersGrid(filter) {
    let Yes = "Yes";
    let No = "No";
    var input = {
        "pOStage": $('#POStage').val(),
        "fromDate": selectedStartDate,
        "toDate": selectedEndDate,
        "vesselId": $('#VesselId').val(),
        "searchOrderNumber": $('#orderNumber').val(),
        "purchaseOrderTypes": $('#PurchaseOrderTypes').val(),
        "IsSearchClicked": $('#IsSearchClicked').val(),
        "SelectedOrderStage": $('#SelectedOrderStage').val(),
        "Title": $('#orderName').val(),
        "SupplierId": $('#SupplierId').val(),
        "SupplierName": $('#SupplierName').val(),
        "SearchFilter": filter,
        "SelectedStatus": $("#SelectedStatus").val(),
        "ShowOnlyAuthRequired": $("#ShowOnlyAuthRequired").val(),
        "strOrderStatusIds": $('#strOrderStatusIds').val(),
    }

    $('#dtPurchaseOrder').DataTable().destroy();
    var orderByColumn = $('#dtPurchaseOrder').find("th:contains('Order No.')")[0].cellIndex;

    //$('#dtPurchaseOrder').append('<caption style="caption-side: top"></caption>');

    gridPurchaseOrder = $('#dtPurchaseOrder').DataTable({
        // "dom": '<<"row mb-3"<"col-12 col-md-3 offset-md-0 col-lg-3 col-xl-2 offset-lg-2 offset-xl-1 dt-infomation dt-infomationhed "i><"col-md-3 col-lg-2 col-xl-5 filters-data"><"col-12 col-md-6 col-lg-3 col-xl-4"f>><"table-horizontal-scroll"rt><"clearfix"<"float-left"l><""p>>>',
        "dom": '<"row"<"col-12 col-md-12 col-lg-12 col-xl-12 dtPurchaseOrder_customSearch search-filter"f><"col-12 col-md-6 col-lg-6 col-xl-6"i>>' +
            '<"table-responsive" rt><"clearfix"<"float-left"l><""p>>',
        "processing": false,
        "serverSide": true,
        "lengthChange": true,
        "searching": true,
        "info": true,
        search: {
            return: true,
        },
        "autoWidth": false,
        "paging": true,
        "pageLength": 25,
        //"order": [[4, 'asc']],
        order: [[orderByColumn, 'asc']],
        "language": {
            "emptyTable": "No orders available.",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/PurchaseOrder/GetPurchaseOrderList",
            "type": "POST",
            "data":
            {
                "input": input
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "width-auto  data-icon-align d-none d-md-table-cell",
                orderable: false,
                width: "16px",
                render: function (data, type, full, meta) {
                    if (full.isDamagedItem == Yes || full.isPoorQuality == Yes || full.isPoorPackaging == Yes || full.isIncorrectItem == Yes || full.isCertificateReceived == No || (full.isHazMaterial == Yes || full.isAnyHazardousMaterialInOrderLines == true)) {
                        var displayElement = "<i class='fa fa-exclamation-triangle txt-red extraDetails cursor-pointer mr-1 sm-mr-0' data-toggle='modal' data-target='#extraDetailsModal'></i>";
                        return displayElement;
                    }
                    else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align d-none d-md-table-cell",
                orderable: false,
                width: "17px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0) {
                        return GetChatBaseIcons(full.orderNumber, full.channelCount, full.messageDetailsJSON);
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
                        return GetNotesBaseIcons(full.orderId, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "data-icon-align tdblock d-block d-md-none d-lg-none d-xl-none",
                orderable: false,
                width: "70px",
                render: function (data, type, full, meta) {
                    if (full.channelCount > 0 || full.notesCount > 0) {
                        return GetChatNotesBaseIcons(full.orderNumber, full.channelCount, full.notesCount, full.messageDetailsJSON);
                    } else {
                        return '';
                    }
                }
            },
            {
                className: "width-auto d-md-none d-lg-none d-xl-none data-icon-align",
                orderable: false,
                width: "16px",
                render: function (data, type, full, meta) {
                    if (full.isDamagedItem == Yes || full.isPoorQuality == Yes || full.isPoorPackaging == Yes || full.isIncorrectItem == Yes || full.isCertificateReceived == No || (full.isHazMaterial == Yes || full.isAnyHazardousMaterialInOrderLines == true)) {
                        var displayElement = "<i class='fa fa-exclamation-triangle txt-red extraDetails cursor-pointer mr-1 sm-mr-0' data-toggle='modal' data-target='#extraDetailsModal'></i>";
                        return displayElement;
                    }
                    else {
                        return '';
                    }
                }
            },
            {
                className: "data-text-align",
                width: "90px",
                name: 'OrderNumber',
                render: function (data, type, full, meta) {
                    return '<a href = "/PurchaseOrder/Detail/?PurchaseOrderRequest=' + full.purchaseOrderUrl + '&VesselId=' + full.vesselId + '"> ' + full.accountingCompanyId + ' - ' + full.orderNumber + '</a > ';
                }
            },
            {
                className: "tdblock data-text-align",
                "data": "title",
                width: "240px",
                name: 'OrderName',
                render: function (data, type, full, meta) {
                    var uiElement = data;
                    return GetCellData('Order Name', GetValueOrDefaultDT(uiElement));
                }
            },
            {
                className: "data-icon-align-right",
                "data": "status",
                width: "50px",
                name: 'OrderStatus',
                render: function (data, type, full, meta) {
                    var status = '';
                    if (full.isFurtherOrderAuthorisationRequired) {
                        status += '<span class="d-none d-md-inline-block d-xl-inline-block d-lg-inline-block badge badge-pill badge-amber-color purchase-order-status-badge mr-1" data-toggle="tooltip" title="Authorisation Required"> AR </span><span class="d-md-none d-lg-none d-xl-none mr-1 text-yellow"> Authorisation Required <br/></span>';
                    }
                    if (full.isHighPriority == false) {
                        status += '<span class="d-none d-md-inline-block d-xl-inline-block d-lg-inline-block badge badge-pill purchase-order-status-badge badge-success" data-toggle="tooltip" title="' + full.statusDescription + '">' + data + '</span><span class="text-green d-md-none d-lg-none d-xl-none">' + full.statusDescription + '</span>';
                    }
                    else {
                        status += '<span class="d-none d-md-inline-block d-xl-inline-block d-lg-inline-block badge badge-pill purchase-order-status-badge badge-danger" data-toggle="tooltip" title="' + full.statusDescription + '">' + data + '</span><span class="txt-red d-md-none d-lg-none d-xl-none">' + full.statusDescription + '</span>';
                    }
                    return GetCellData('Status', status);
                }
            },
            {
                className: "d-md-none d-lg-none d-xl-none data-number-align",
                "data": "cost",
                width: "70px",
                name: 'TotalAmount',
                render: function (data, type, full, meta) {
                    return GetFormattedDecimalWithText(type, 'Amount Total', data, 2, '0.00', full.currency);
                }
            },
            {
                className: "data-datetime-align",
                "data": "dateEntered",
                width: "65px",
                name: 'RequestDate',
                render: function (data, type, full, meta) {
                    if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
                        return GetCellData('Requested Date', GetValueOrDefaultDT(data));
                    } else {
                        return GetFormattedDate(type, 'Requested Date', data);
                    }
                }
            },
            {
                className: "data-datetime-align",
                "data": "dateOrdered",
                width: "65px",
                name: 'OrderDate',
                render: function (data, type, full, meta) {
                    if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
                        return GetCellData('Ordered Date', GetValueOrDefaultDT(data));
                    } else {
                        return GetFormattedDate(type, 'Ordered Date', data);
                    }
                }
            },
            {
                className: "data-text-align",
                "data": "expectedRecPort",
                width: "90px",
                name: 'ExpectedReceivedPort',
                render: function (data, type, full, meta) { return GetCellData("Exp'd / Rec'd Port", GetValueOrDefaultDT(data)); }
            },
            {
                className: "data-datetime-align",
                "data": "expctedRecDate",
                width: "80px",
                name: 'ExpectedReceivedDate',
                render: function (data, type, full, meta) {
                    if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
                        return GetCellData("Exp'd / Rec'd Date", GetValueOrDefaultDT(data));
                    } else {
                        return GetFormattedDate(type, "Exp'd / Rec'd Date", data);
                    }
                }
            },
            {
                className: "tdblock data-text-align",
                "data": "supplier",
                width: "150px",
                name: 'SupplierName',
                render: function (data, type, full, meta) {
                    var supplierData = data;
                    if (full.isSupplierAdditionalDetailsVisible) {
                        supplierData = "<a href='#' class='supplierDetails' data-toggle='modal' data-target='#supplierModal'>" + data + "</a>"
                    }

                    return GetCellData('Supplier Name', GetValueOrDefaultDT(supplierData));
                }
            },
            {
                className: "tdblock data-text-align",
                "data": "agent",
                width: "150px",
                name: 'AgentName',
                render: function (data, type, full, meta) {
                    var agentData = data;
                    if (full.isAgentAdditionalDetailsVisible) {
                        agentData = "<a href='#' class='agentDetails' data-toggle='modal' data-target='#supplierModal'>" + data + "</a>"
                    }

                    return GetCellData('Agent', GetValueOrDefaultDT(agentData));
                }
            },
            {
                className: "d-none d-md-table-cell data-text-align",
                "data": "warehouse",
                width: "150px",
                name: 'WarehouseName',
                render: function (data, type, full, meta) {
                    var warehouseData = data;
                    if (full.isWarehouseAdditionalDetailsVisible) {
                        warehouseData = "<a href='#' class='warehouseDetails' data-toggle='modal' data-target='#supplierModal'>" + data + "</a>"
                    }
                    return GetCellData('Warehouse', GetValueOrDefaultDT(warehouseData));
                }
            },
            {
                className: "d-none d-md-table-cell data-text-align",
                "data": "isHazMaterial",
                width: "70px",
                name: 'IsHazardousMaterial',
                render: function (data, type, full, meta) { return GetCellData('Haz Materials', data); }
            },
            {
                className: "d-none d-md-table-cell data-number-align",
                "data": "cost",
                width: "70px",
                name: 'TotalAmount',
                render: function (data, type, full, meta) {
                    return GetFormattedDecimalWithText(type, 'Amount Total', data, 2, '0.00', full.currency);

                }
            },
            {
                className: "data-number-align d-none d-md-table-cell",
                "data": "osCost",
                width: "70px",
                name: 'OutstandingAmount',
                render: function (data, type, full, meta) {
                    return GetFormattedDecimalWithText(type, 'Amount O/S', data, 2, '0.00', full.currency);
                }
            }

        ],
        "fnRowCallback": function (nRow, full, iDisplayIndex, iDisplayIndexFull) {
            let child = $(nRow).find('td:eq(3)').children();
            if (screen.width < MobileScreenSize && $(child).length == 0) {
                $(nRow).find('td:eq(3)').addClass('d-none');
            }

            let childHazardous = $(nRow).find('td:eq(0)').children();
            if (screen.width < MobileScreenSize && $(childHazardous).length == 0) {
                $(nRow).find('td:eq(0)').addClass('d-none');
            }
        },
        initComplete: function (setting) {
            SetFilterCount();
            $(".dtPurchaseOrder_customSearch .dataTables_filter input").unbind();
            $(".dtPurchaseOrder_customSearch .dataTables_filter input").bind('keyup input', function (e) {
                var value = this.value.trim();
                if (e.keyCode == 13) {
                    if (value.length > 1) {
                        gridPurchaseOrder.search(value).draw();
                    }
                }
                else if (value == "") {
                    gridPurchaseOrder.search("").draw();
                }
            });
        },
    });
    //$("div.filters-data").html('<a href="javascript:void(0)" data-toggle="modal" data-target="#filterdata" class="filter-design"><i class="fa fa-filter" aria-hidden="true" title=""></i><span id="appliedFilterCount">0</span> Filters applied</a><a href="javascript:void(0)" class="clear-filter" id="aRemoveFilter"><i class="fa fa-fw" aria-hidden="true" title=""></i></a>');
    $.fn.DataTable.ext.pager.numbers_length = 4;

    //table scroll
    var newWidth = ($(".table-scroll-width").width());
    $(".table-common-design .table-horizontal-scroll").css({
        "maxWidth": newWidth - 20
    });

    if ((selectedStartDate != null && selectedStartDate != "") || !IsNullOrEmptyOrUndefined($("#tableSubTitle").text())) {
        $('.dt-infomationhed').removeClass('offset-md-0 col-lg-6 col-xl-2 offset-lg-2 offset-xl-1');
        $('.dt-infomationhed').addClass('offset-md-0 col-lg-3 col-xl-2 offset-lg-4 offset-xl-3');
        $('.filters-data').removeClass('col-md-3 col-lg-2 col-xl-5');
        $('.filters-data').addClass('col-md-3 col-lg-2 col-xl-3');
    }

    $('#dtPurchaseOrder tbody').on('click', 'a.agentDetails', function () {
        var data = gridPurchaseOrder.row($(this).parents('tr')).data();
        LoadCompanyDetails('Agent', data.encryptedAgentId)
    });

    $('#dtPurchaseOrder tbody').on('click', 'a.supplierDetails', function () {
        var data = gridPurchaseOrder.row($(this).parents('tr')).data();
        LoadCompanyDetails('Supplier', data.encryptedSupplierId)
    });

    $('#dtPurchaseOrder tbody').on('click', 'a.warehouseDetails', function () {
        var data = gridPurchaseOrder.row($(this).parents('tr')).data();
        LoadCompanyDetails('Warehouse', data.encryptedWarehouseId)
    });

    $('#dtPurchaseOrder tbody').on('click', 'i.extraDetails', function () {
        var data = gridPurchaseOrder.row($(this).parents('tr')).data();
        LoadExtraDetails(data)
    });

    $('#dtPurchaseOrder').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip({
            trigger: 'hover'
        })
    });

}

function LoadExtraDetails(data) {
    if (data != null && data != 'undefined' && data != '') {
        $('#spanOrderNumber').text(data.accountingCompanyId + ' - ' + data.orderNumber);
        $('#spanOrderName').text(data.title);
        $('#spanDamagedItems').text(data.isDamagedItem);
        $('#spanPoorQuality').text(data.isPoorQuality);
        $('#spanPoorPackaging').text(data.isPoorPackaging);
        $('#spanIncorrectItems').text(data.isIncorrectItem);
        $('#spanCertificateRecieved').text(data.isCertificateReceived);
        $('#spanIsHazMat').text(data.isAnyHazardousMaterialInOrderLines == true ? "Yes" : data.isHazMaterial);
    }
}

function LoadCompanyDetails(selection, data) {
    $.ajax({
        url: "/PurchaseOrder/GetAddressDetails",
        type: "POST",
        "data": {
            "selectedStatus": selection,
            "encryptedId": data
        },
        success: function (data) {
            if (selection == 'Agent') {
                $('#modalHeader').text('Agent Details');
                $('#modalimage').attr("src", "/images/agent.png");
            }
            else if (selection == 'Warehouse') {
                $('#modalHeader').text('Warehouse Details');
                $('#modalimage').attr("src", "/images/warehouse.png");
            }
            else if (selection == 'Supplier') {
                $('#modalHeader').text('Supplier Details');
                $('#modalimage').attr("src", "/images/supplier.png");
            }

            $("#companyName").text(data.companyName);

            if (data.isCompanyAddressVisible) {
                $("#divCompanyAddress").removeClass('d-none');
                $('#spanCompanyAddress').text(data.companyAddress);
            }

            $('#spanLocalAddres').text(data.companyLocalAddress);

            if (data.isCountryVisible) {
                $("#divCountry").removeClass('d-none');
                $('#spanCountryName').text(data.companyCountryDesc);
                $("#imgCountryFlag").attr("src", "/images/Flags/" + data.countryCode + ".png");
            }

            if (data.isTelephoneVisible) {
                $("#divPOTelephone").removeClass('d-none');
                $('#spanTelephone').text(data.telephoneNumber);
            }

            if (data.isMobileVisible) {
                $("#divPOMobile").removeClass('d-none');
                $('#spanMobile').text(data.mobileNumber);
            }

            if (data.isFaxVisible) {
                $("#divPOFax").removeClass('d-none');
                $('#spanFax').text(data.faxNumber);
            }

            if (data.isEmailVisible) {
                $("#divPOEmail").removeClass('d-none');
                $('#spanEmail').text(data.email);
                $("#aEmail").attr("href", 'mailto:' + data.email);
            }

            if (data.isWebAddressVisible) {
                $("#divPOWeb").removeClass('d-none');
                $('#spanWeb').text(data.webAddress);
                $("#aWebAddr").attr("href", 'http://' + data.webAddress);
                $("#aWebAddr").attr('target', '_blank')
            }

            if (data.isProcurmentVisible) {
                $("#divPOProcurment").removeClass('d-none');
                $('#spanProcurment').text(data.procurmentType);
            }

            if (data.isAuditDateVisible) {
                $("#spanAuditDate").removeClass('d-none');
                $('#spanAudit').text(data.isAuditCompleted);
                $('#spanAuditDate').text(data.auditCompletedDate);
            }

        }
    });
}

function getCookie(name) {
    var cookieArr = document.cookie.split(";");

    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");

        if (name == cookiePair[0].trim()) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}

function tosterAlert(type, message) {
    if (type == "success") {

        toastr.success(message);
    }
    else if (type == "error") {
        toastr.options = {
            "closeButton": true,
            "timeOut": "0",
            "extendedTimeOut": "0"
        };
        toastr.error(message);
    }
}

function GetFormattedDecimalWithText(type, label, data, decimalPlaces, defaultValue, currency) {
    var formattedDecimal = defaultValue;
    if (data != null && data != '' && data != 'undefined') {
        formattedDecimal = Number(parseFloat(data).toFixed(decimalPlaces)).toLocaleString('en',
            {
                minimumFractionDigits: decimalPlaces,
                maximumFractionDigits: decimalPlaces
            });
    }
    if (type === "display") {
        if (formattedDecimal != defaultValue) {
            formattedDecimal += " (" + currency + ")";
        }
        return GetCellData(label, formattedDecimal);
    }
    return formattedDecimal;
}

function LoadOrderStatusTree() {

    $("#orderStatusTree").fancytree({
        checkbox: true,
        selectMode: 3,
        icon: false,
        source: $.ajax({
            url: "/PurchaseOrder/GetOrderStatusTree",
            dataType: "json"
        }),
        init: function (event, data) {
            SetOrderStatusTree();
        },
        select: function (event, data) {
            SetSearchButtonAccess();

        },
        click: function (e, data) {
            if (data.targetType === 'title') {
                data.node.toggleSelected();
            }
        },
    });
}

function SetOrderStatusTree() {
    $("#orderStatusTree").fancytree("getTree").visit(function (node) {
        var typeIdList = $('#strOrderStatusIds').val().split(',');
        typeIdList.forEach(function () {
            if (typeIdList.includes(node.key)) {
                node.setSelected(true);
            }
            else {
                node.setSelected(false);
            }
        });
    });
}

function AppendGridTitle(subtitle) {

    if (isFilterExist()) {
        $("#divfilterhide").removeClass('btn-dark-grey');
        $("#divfilterhide").addClass('btn-info');
    }
    else {
        $("#divfilterhide").removeClass('btn-info');
        $("#divfilterhide").addClass('btn-dark-grey');
    }

    if (!(selectedStartDate != null && selectedStartDate != "")) {
        $("#tableSubTitle").text(subtitle);
        $("#tableSubTitle").show();
        $("#tblspndtrOrderlist").html("");
    }

    if ($('#IsSearchClicked').val().toLowerCase() == 'true') {
        $("#tableSubTitle").hide();
        $("#tableSubTitle").text("");
    }
}

function isFilterExist() {
    var strOrderStatusIds = $('#strOrderStatusIds').val();
    var orderNumber = $('#orderNumber').val();
    var SupplierId = $('#SupplierId').val();
    var SupplierName = $('#SupplierName').val();
    var Title = $('#Title').val();

    return (!IsNullOrEmptyOrUndefined(orderNumber) || !IsNullOrEmptyOrUndefined(strOrderStatusIds)
        || !IsNullOrEmptyOrUndefined(SupplierId) || !IsNullOrEmptyOrUndefined(SupplierName)
        || !IsNullOrEmptyOrUndefined(Title))
}


function ShowFilterDetails() {
    SetFilterCount();
    $('#modelFilterData').modal("show");
}

function SetFilterCount() {

    var orderNumber = $('#orderNumber').val();
    var orderName = $('#orderName').val();
    var SupplierName = $('#SupplierName').val();
    var Title = $('#Title').val();
    var filterCount = 0;
    var filterStatusCount = 0;
    var htmlElement = "";
    var parentArray = new Map();
    var statusArray = new Map();

    var nodedata = $('#orderStatusTree').fancytree('getTree').getSelectedNodes();
    nodedata.forEach(function (node) {
        statusArray.set(node.title, node.title);
        parentArray.set(node.parent.title, node.parent.title);
    });

    parentArray.forEach((value, key) => {
        if (statusArray.has(key)) {
            statusArray.delete(key);
        }
    });

    statusArray.forEach((value, key) => {
        filterStatusCount++;

        htmlElement += '<div class="col-12 col-md-6 col-lg-4 col-xl-4">';
        htmlElement += '<div class="sub-section">';
        htmlElement += '<div class="dashboard-counters-label"><span id="">' + value + '</span></div></div></div>';
    });
    if (filterStatusCount > 0) {
        $('#statusFilterCount').text(filterStatusCount);
        AddClassIfAbsent($('#statusFilterCount').parent('div'), 'active');
    }
    else {
        $('#statusFilterCount').text('0');
        RemoveClassIfPresent($('#statusFilterCount').parent('div'), 'active');
    }

    if (!IsNullOrEmptyOrUndefined(orderNumber) || !IsNullOrEmptyOrUndefined(orderName)) {
        $('#orderFilterCount').text('1');
        AddClassIfAbsent($('#orderFilterCount').parent('div'), 'active');
    }

    if (!IsNullOrEmptyOrUndefined(orderNumber)) {
        $('#filterOrderNumber').text(orderNumber);
        $('#filterOrderNumberRow').show();
        filterCount++;
    }
    else {
        $('#filterOrderNumber').text('-');
        $('#filterOrderNumberRow').hide();
    }

    if (!IsNullOrEmptyOrUndefined(Title)) {
        $('#filterOrderName').text(Title);
        $('#filterOrderNameRow').show();
        filterCount++;
    }
    else {
        $('#filterOrderName').text('-');
        $('#filterOrderNameRow').hide();
    }

    if (!IsNullOrEmptyOrUndefined(Title) || !IsNullOrEmptyOrUndefined(orderNumber)) {
        $("#filterCard1").show();
    }
    else {
        $("#filterCard1").hide();
    }

    if (filterStatusCount > 0) {
        $('#filterStatus').html(htmlElement);
        filterCount = filterCount + filterStatusCount;
        $("#filterCard2").show();
    }
    else {
        $('#filterStatus').html("");
        $("#filterCard2").hide();
    }

    if (!IsNullOrEmptyOrUndefined(SupplierName)) {
        $('#filterSupplier').text(SupplierName);
        filterCount++;
        $("#filterCard3").show();
    }
    else {
        $('#filterSupplier').text('-');
        $("#filterCard3").hide();
    }

    $('#appliedFilterCount').text(filterCount);
}

function clearFilter() {
    $('#orderNumber').val('');
    $('#orderName').val('');
    $('#dtrOrderlist').html('-');
    ResetTblheading();
    $('#SupplierId').val('');
    $('#SupplierName').val('');
    //clear date parameter
    $('#FromDate').val();
    $('#ToDate').val();
    selectedEndDate = '';
    selectedStartDate = '';
    $('#strOrderStatusIds').val("");
    SetOrderStatusTree();
    ClearSelectedCompany();
    $("#aPOInProcess")[0].click();
}
