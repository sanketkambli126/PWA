import "bootstrap";
import moment from "moment";
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, headerReadMore, GetCookie, ToastrAlert, SetHeaderMargin, base64ToArrayBuffer, saveByteArray, BackButton, ReplaceClass, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, GetRoleRightsAsync, IsNullOrEmptyOrUndefinedLooseTyped, RemoveClassIfPresent, AddClassIfAbsent } from "../common/utilities.js"
import { GetCellData, GetFormattedDate, GetFormattedDecimal } from "../common/datatablefunctions.js"
import { MobileScreenSize, DefectDetailsPageKey } from "../common/constants.js"
import { RecordLevelNote } from "../common/notesUtilities.js"

var CloseDefectGauranteeClaimControlId = "431CDCDE-5274-4266-A719-0245E568AD06";

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

$(window).on('resize', SetHeaderMargin);
var DownloadAttachment;

$(document).ready(function () {

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

    $("#txtInputDay").keyup(function (event) {
        let text = $(this).val();
        if (!CheckIfNumberIsInteger(text)) {
            $('#txtInputDay').val("");
        }
    });

    $("#txtInputDay").focusout(function () {
        if (!CheckIfNumberIsInteger($('#txtInputDay').val())) {
            $('#txtInputDay').val("");
        }
    });

    $("#txtInputHours").keyup(function (event) {
        let text = $(this).val();
        if (!CheckIfNumberIsInteger(text)) {
            $('#txtInputHours').val("");
        }
    });

    $("#txtInputHours").focusout(function () {
        if (!CheckIfNumberIsInteger($('#txtInputHours').val())) {
            $('#txtInputHours').val("");
        }
    });

    $("#txtInputMins").keyup(function (event) {
        let text = $(this).val();
        if (!CheckIfNumberIsInteger(text)) {
            $('#txtInputMins').val("");
        }
    });

    $("#txtInputMins").focusout(function () {
        if (!CheckIfNumberIsInteger($('#txtInputMins').val())) {
            $('#txtInputMins').val("");
        }
    });
    
    $('#mobileactiontoggle').click(function () {
        $('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
    });
    if (screen.width > MobileScreenSize) {
        MatchHeightOfDefectClosureSection();
    }

    if (screen.width < 760) {
        headerReadMore('headershowmorewrapper', 'header');
    }

    //Role Right
    GetRoleRightsAsync([CloseDefectGauranteeClaimControlId], function (rolerights) {
        var defectClosureAccess = rolerights.find(x => x.controlId.toLowerCase() === CloseDefectGauranteeClaimControlId.toLowerCase());
        if (defectClosureAccess.permission) {
            if ($('#IsStatusCompleted').val() == "True" || $('#IsStatusCompleted').val() == "true") {
                if (($(window).width() < MobileScreenSize)) {
                    $('.btnDefectCloseActionMobile').show();
                    $('.btnDefectCloseActionDesktop').hide();
                }
                else {
                    $('.btnDefectCloseActionDesktop').show();
                    $('.btnDefectCloseActionMobile').hide();
                }
            }
            else {
                $('.btnDefectCloseActionDesktop').hide();
                $('.btnDefectCloseActionMobile').hide();
            }

        }
        else {
            $(".btnDefectCloseAction").remove();
        }
    });

    $('.btnDefectCloseAction').click(function () {
        $("#closureDefectModal").modal('show');
        DefectClosureDataLoad();
    });

    $('#btnCloseDefectWO').click(function () {
        if (ValidationBeforeCloseDefect()) {
            CloseDefect();
        }

    });

    //Closure offhire radio button

    $("#divOffhire input[name='offhire']").click(function () {
        if ($('input:radio[name=offhire]:checked').val() == "Yes") {
            ClearInptTextField()
            $("#txtInputDay").removeAttr("disabled");
            $("#txtInputHours").removeAttr("disabled");
            $("#txtInputMins").removeAttr("disabled");
            $('#selectOffHireDD').prop('disabled', false);
            RemoveClassIfPresent('.validationDefectClosure', 'd-none');
        }
        else if ($('input:radio[name=offhire]:checked').val() == "No") {
            $("#txtInputDay").val("");
            $("#txtInputHours").val("");
            $("#txtInputMins").val("");
            $("#txtInputDay").attr("disabled", "disabled");
            $("#txtInputHours").attr("disabled", "disabled");
            $("#txtInputMins").attr("disabled", "disabled");
            $('#selectOffHireDD').val("0");
            $('#selectOffHireDD').prop('disabled', 'disabled');
            AddClassIfAbsent('.validationDefectClosure', 'd-none');
        }
    });

    $("#divRegulatory input[name='regular']").click(function () {
        if ($('input:radio[name=regular]:checked').val() == "Yes") {
            $('input[name="dispensation"]').removeAttr('disabled');
        }
        else if ($('input:radio[name=regular]:checked').val() == "No") {
            $("#disp2").prop("checked", true);
            $('input[name="dispensation"]').attr('disabled', 'disabled');
        }
    });

    AddLoadingIndicator();
    RemoveLoadingIndicator();

    //Sidebar back
    BackButton(DefectDetailsPageKey, false)

    DefectWorkOrderDetails();

    if ($('#IsGuaranteeClaimCode').val() == "True") {
        $('#divRemarksAndFinding').hide();
        $('#divSpareParts').hide();
        $('#divRankAndHours').hide();
        $('#divRequisition').hide();
        $('#divRescheduleHistory').hide();
    }
    else {
        DefectReportWOSummary();
        DefectRescheduleLog();
    }

    $(".btnDefectReport").click(function () {
        DownloadDefectDetailReport();
    });

    DefectRequisition();

    $('.btnDownloadAttachment').click(function () {
        //console.log('btn clicked')
        //$('#modalDownloadAttachment').show();
        LoadDownloadAttachment();
    });

    var messageDetailsJSON = $("#MessageDetailsJSON").val()
    RecordLevelMessage(messageDetailsJSON);
    RecordLevelNote(messageDetailsJSON);
    GetDiscussionNotesCount(messageDetailsJSON);
    InitializeDiscussionAndNoteClickEvents(messageDetailsJSON, code);
});

function DefectWorkOrderDetails() {
    $.ajax({
        url: "/Defect/GetDefectWorkOrderForEdit",
        type: "POST",
        "data": {
            "EncryptedDWOId": $('#EncryptedDWOId').val(),
        },
        "datatype": "JSON",
        success: function (data) {
            if (data != null) {
                //header
                $('#spanDefectName').text(data.defectName);
                $('#spanDefectNumber').text(data.defectNumber);
                $('#spanDefectDueDate').text(data.estimatedCompletionDate);
                let due = moment(data.estimatedCompletionDate, 'DD MMM YYYY').startOf('day');
                let todaysDate = moment().startOf('day');
                if (due < todaysDate) {
                    //AddClassIfAbsent("#clockIcon", 'common-sub-heading-red');
                    ReplaceClass("#spanDefectDueDate", 'common-small-heading', 'common-sub-heading-red');
                };

                //description
                $('#spanDefectDescription').text(data.defectDescription);
                $('#spanRepairSpecification').text(data.repairSpecification);

                //details
                $('#spanDetailsDefectName').text(data.defectName);
                $('#spanDetailsDefectNo').text(data.defectNumber);
                $('#spanCategory').text(data.category);
                $('#spanSystemArea').text(data.defectSystemArea);
                $('#spanEffect').text(data.defectSubSystemArea);
                $('#spanResponsibility').text(data.rankDescription);
                $('#spanPriority').text(data.priority);
                $('#spanPlannedFor').text(data.siteType);
                $('#spanStatus').text(data.defectStatusDescription);


                //Load Action taken
                LoadDefectActionTaken(data.actionList);

                //TimeLine
                $('#spanTimeLineDueDate').text(data.estimatedCompletionDate);
                $('#spanReportedDate').text(data.foundDate);
                $('#spanEstimatedStartDate').text(data.proposedStartDate);
                $('#spanEstimatedCompletion').text(data.estimatedCompletionDate);

                //Defect Attributes
                $('#spanOwnerSpecific').text(data.ownerSpecificId);
                $('#spanSpecificNo').text(data.specificAttributeValue);

                if (data.isCritical) {
                    $('#divCritical').show();
                }
                else {
                    $('#divCritical').hide();
                }

                if (data.includeInDamageForm) {
                    $('#divTechnicalDefect').show();
                }
                else {
                    $('#divTechnicalDefect').hide();
                }

                if ($('#IsGuaranteeClaimCode').val() == "False") {
                    if (data.isJSARequired) {
                        $('#divJSARequired').show();
                    }
                    else {
                        $('#divJSARequired').hide();
                    }

                    if (data.isMOCRequired) {
                        $('#divMOCRequired').show();
                    }
                    else {
                        $('#divMOCRequired').hide();
                    }
                }

                if (data.isOffHire) {
                    $('#spanOffHireEstimatedPeriod').text(data.estimatedPeriod);
                }
                else {
                    $('#spanOffHireEstimatedPeriod').text("-");
                }

                if (data.isImpact) {
                    var impactText = " - " + data.impact;
                    $('#spanImpact').text(impactText);
                    $('#divImpact').show();
                }
                else {
                    $('#divImpact').hide();
                }

                if (data.isRegulatoryAuthority) {
                    $('#divClassReq').show();
                    $('#divDispensionInPlace').show();
                }
                else {
                    $('#divClassReq').hide();
                    $('#divDispensionInPlace').hide();
                }

                if (data.isGasFree) {
                    $('#divGasFree').show();
                }
                else {
                    $('#divGasFree').hide();
                }

                //header margin
                SetHeaderMargin();

                if (data.isCompletedOrClosed == true || $('#IsCompletedOrClosed').val() == "True") {
                    DefectReportWOSummary();
                }
                else {
                    $('#divRemarksAndFinding').hide();
                    $('#divSpareParts').hide();
                    $('#divRankAndHours').hide();
                }
            }
        }
    });
}

function LoadDefectActionTaken(data) {
    $('#dtActionTaken').DataTable().destroy();
    var ActionTaken = $('#dtActionTaken').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "scrollY": "235px",
        "scrollCollapse": true,
        "scrollX": false,
        "paging": false,
        "language": {
            "emptyTable": "No action taken.",
        },
        "data": data,
        "columns": [
            {
                className: "top-margin-0 data-datetime-align tdblock",
                data: "date",
                width: "15px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Date', data);
                }
            },
            {
                className: "tdblock data-text-align tdblock text-break",
                data: "reportedBy",
                width: "45px",
                render: function (data, type, full, meta) {
                    return GetCellData('Reported By', data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "action",
                width: "200px",
                render: function (data, type, full, meta) {
                    return GetCellData('Details', data);
                }
            }
        ],
        "initComplete": function () {
            $('#spanActionTakenCount').text(this.api().data().length);
        }
    });
}

//will be used for closed and completed
function DefectReportWOSummary() {
    $.ajax({
        url: "/Defect/GetDefectReportWOSummary",
        type: "POST",
        "data": {
            "EncryptedDWOId": $('#EncryptedDWOId').val(),
        },
        "datatype": "JSON",
        success: function (data) {
            if (data != null) {

                LoadSparePartList(data.spareParts);
                LoadShipStaffList(data.shipStaff);

                if (data.isShoreStaff) {
                    LoadShoreStaffList(data.shoreStaff);
                }
                else {
                    $('#divShoreStaff').hide();
                }
                $('#spanRemarksAndFindings').text(data.remark);
            }
        }
    });
}

function LoadSparePartList(data) {
    $('#dtSpareParts').DataTable().destroy();
    var SpareParts = $('#dtSpareParts').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": true,
        "paging": false,
        "language": {
            "emptyTable": "No spare part usage reported.",
        },
        "data": data,
        "columns": [
            {
                className: "top-margin-0 data-text-align tdblock",
                data: "partName",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetCellData('Part Name', data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "makerReferenceNumber",
                width: "115px",
                render: function (data, type, full, meta) {
                    return GetCellData("Maker's Ref No.", data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "plateSheetNumber",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetCellData("Plate/Sheet No.", data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "drawingPositionNumber",
                width: "120px",
                render: function (data, type, full, meta) {
                    return GetCellData("Drawing Position", data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "location",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetCellData("Location", data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "condition",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetCellData("Condition", data);
                }
            },
            {
                className: "data-number-align tdblock",
                data: "quantityUsed",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetCellData("Qty Used", data);
                }
            }
        ]
    });
}

function LoadShipStaffList(data) {
    $('#dtShipStaff').DataTable().destroy();
    var ShipStaff = $('#dtShipStaff').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No ship staff.",
        },
        "data": data,
        "columns": [
            {
                className: "top-margin-0 data-text-align",
                data: "shipStaffRank",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Rank', data);
                }
            },
            {
                className: "top-margin-0 data-number-align",
                data: "hours",
                width: "5px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Hours', data, 2, "0.00");
                }
            }
        ]
    });
}

function LoadShoreStaffList(data) {
    $('#dtShoreStaff').DataTable().destroy();
    var ShoreStaff = $('#dtShoreStaff').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No shore staff.",
        },
        "data": data,
        "columns": [
            {
                className: "top-margin-0 data-text-align",
                data: "shoreStaffRank",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Rank', data);
                }
            },
            {
                className: "top-margin-0 data-number-align",
                data: "hours",
                width: "5px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Hours', data, 2, "0.00");
                }
            }
        ]
    });
}

function DefectRescheduleLog() {
    $('#dtRescheduleHistory').DataTable().destroy();
    var RescheduleHistory = $('#dtRescheduleHistory').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "scrollY": "235px",
        "scrollCollapse": true,
        "scrollX": true,
        "autoWidth": true,
        "paging": false,
        "language": {
            "emptyTable": "No reschedule history.",
        },
        "ajax": {
            "url": "/Defect/GetDefectRescheduleLog",
            "type": "POST",
            "data": {
                "EncryptedDWOId": $('#EncryptedDWOId').val(),
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "top-margin-0 data-datetime-align",
                data: "previousDueDate",
                width: "65px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Original<br> Due Date', data);
                }
            },
            {
                className: "top-margin-0  data-datetime-align",
                data: "requestedDueDate",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Requested<br> Due Date', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "newDueDate",
                width: "70px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Approved Due Date', data);
                }
            },
            {
                className: "data-text-align",
                data: "workOrderReasonDescription",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetCellData('Reason', '<span class="export-Data d-block">' + data + '</span>');
                }
            },
            {
                className: "data-text-align",
                data: "requestedBy",
                width: "115px",
                render: function (data, type, full, meta) {
                    return GetCellData('Requester <br>Name', data);
                }
            },
            {
                className: "data-text-align",
                data: "requesterRole",
                width: "65px",
                render: function (data, type, full, meta) {
                    return GetCellData('Requester <br/> Rank', data);
                }
            },
            {
                className: "data-text-align",
                data: "approvedBy",
                width: "110px",
                render: function (data, type, full, meta) {
                    return GetCellData('Approver Name', data);
                }
            },
            {
                className: "data-text-align",
                data: "approverRole",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Approver Rank', data);
                }
            },
            {
                className: "data-icon-align",
                data: "statusShortCode",
                width: "50px",
                render: function (data, type, full, meta) {
                    var status = "";
                    if (full.isStatusShortCode) {
                        if (full.statusColour == "Normal") {
                            status = '<span class="badge badge-pill purchase-order-status-badge badge-success" data-toggle="tooltip" title="' + full.rescheduleWorkOrderStatus + '">' + full.statusShortCode + '</span>';
                        }
                        else if (full.statusColour == "PreWarning") {
                            status = '<span class="badge badge-pill purchase-order-status-badge badge-amber-color" data-toggle="tooltip" title="' + full.rescheduleWorkOrderStatus + '">' + full.statusShortCode + '</span>';
                        }
                        else if (full.statusColour == "Critical") {
                            status = '<span class="badge badge-pill purchase-order-status-badge badge-danger" data-toggle="tooltip" title="' + full.rescheduleWorkOrderStatus + '">' + full.statusShortCode + '</span>';
                        }
                    }
                    return GetCellData('Status', status);
                }
            }
        ],
        "initComplete": function () {
            $('#spanRescheduleHistoryCount').text(this.api().data().length);

        }
    });
    $('#dtRescheduleHistory').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
    });
}

function DefectRequisition() {
    $('#dtRequisition').DataTable().destroy();
    var Requisition = $('#dtRequisition').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "scrollY": "235px",
        "scrollCollapse": true,
        "scrollX": true,
        "info": false,
        "autoWidth": true,
        "paging": false,
        "language": {
            "emptyTable": "No requisitions mapped.",
        },
        "ajax": {
            "url": "/Defect/GetRequisition",
            "type": "POST",
            "data": {
                "EncryptedDWOId": $('#EncryptedDWOId').val(),
                "EncryptedVesselId": $('#EncryptedVesselId').val()
            },
            "datatype": "json"
        },
        columns: [
            {
                className: "tdblock data-text-align",
                width: "90px",
                render: function (data, type, full, meta) {
                    return '<a href = "/PurchaseOrder/Detail/?PurchaseOrderRequest=' + full.purchaseOrderUrl + '&VesselId=' + full.encryptedVesselId + '"> ' + full.coyId + ' - ' + full.orderNumber + '</a > ';
                }
            },
            {
                className: "data-text-align",
                data: "priority",
                width: "90px",
                render: function (data, type, full, meta) {
                    var priorityUIElement = "";
                    if (full.orderStatusColor == "Good") {
                        priorityUIElement = '<span class="txt-color-green">' + data + '</span>';
                    }
                    else if (full.orderStatusColor == "PreWarning") {
                        priorityUIElement = '<span class="BrushOrange">' + data + '</span>';
                    }
                    else if (full.orderStatusColor == "Critical") {
                        priorityUIElement = '<span class="BrushRed">' + data + '</span>';
                    }
                    return GetCellData('Priority', priorityUIElement);
                }
            },
            {
                className: "data-icon-align",
                width: "50px",
                render: function (data, type, full, meta) {
                    var status = "";
                    if (full.isStatusVisible) {
                        if (full.orderStatusColor == "Good") {
                            status = '<span class="badge badge-pill purchase-order-status-badge badge-success" data-toggle="tooltip" title="' + full.statusDescription + '">' + full.statusShortCode + '</span>';
                        }
                        else if (full.orderStatusColor == "PreWarning") {
                            status = '<span class="badge badge-pill purchase-order-status-badge badge-amber-color" data-toggle="tooltip" title="' + full.statusDescription + '">' + full.statusShortCode + '</span>';
                        }
                        else if (full.orderStatusColor == "Critical") {
                            status = '<span class="badge badge-pill purchase-order-status-badge badge-danger" data-toggle="tooltip" title="' + full.statusDescription + '">' + full.statusShortCode + '</span>';
                        }
                    }

                    return GetCellData('Status', status);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "orderName",
                width: "150px",
                render: function (data, type, full, meta) {
                    return GetCellData("Name", data);
                }
            },
            {
                className: "data-text-align",
                data: "orderType",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Type', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "requestedDate",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Requested Date', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "orderDate",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Ordered Date', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "expectedDeliveryDate",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Expected Delivered Date', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "receivedDate",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Received Date', data);
                }
            },
        ],
        "initComplete": function () {
            $('#spanRequisitionCount').text(this.api().data().length);
        }

    });
}

function DownloadDefectDetailReport() {
    $.ajax({
        url: '/Defect/DownloadDefectDetailReport',
        type: 'POST',
        dataType: "JSON",
        data: {
            "dwoId": $("#EncryptedDWOId").val(),
            "vesselId": $("#EncryptedVesselId").val()
        },
        success: function (data) {
            if (data.success) {
                ToastrAlert("success", data.message);
            }
            else {
                ToastrAlert("error", data.message);
            }
        }
    })
}

//Download attachment -
function LoadDownloadAttachment() {
    $.ajax({
        url: "/Defect/GetDefectDocumentsDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "DefectWorkOrderId": $("#EncryptedDWOId").val(),
        },
        success: function (data) {
            var documentList = data.data;
            console.table(documentList);
            $("#vesselNameModal").text($("#VesselName").val());
            $("#vesselNameModal").attr('href', "/Dashboard/?VesselId=" + $("#EncryptedVesselId").val());

            //load data table
            $('#dtDownloadAttachment').DataTable().destroy();
            DownloadAttachment = $('#dtDownloadAttachment').DataTable({
                "dom": '<<"row mb-3" <"detailsinfo"><"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-3 col-xl-9 offset-xl-3 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
                "processing": false,
                "serverSide": false,
                "lengthChange": true,
                "searching": false,
                "scrollY": "235px",
                "scrollCollapse": true,
                "info": true,
                "autoWidth": false,
                "paging": false,
                "order": [],
                "language": {
                    "emptyTable": "No attachments.",
                },
                "data": documentList,
                "columns": [
                    {
                        className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
                        orderable: false,
                        width: "25px",
                        render: function (data, type, full, meta) {
                            if (full.isWebAddressEditable) {
                                return ("<a target='_blank' href='" + full.webAddress + "' class='cursor-pointer d-none d-sm-none d-md-block'><img src='/images/AttachmentLinkIcon.png' class='m-0' width='18' title='View Link'/>")
                            }
                            else {
                                return ("<a class='documentDownload cursor-pointer'><img src='/images/Download-doc-active.png' class='m-0' width='18' title='View Attachment'/>")
                            }
                        }
                    },
                    {
                        className: "mt-0 width-85 top-margin-0 text-break data-text-align",
                        data: "title",
                        width: "300px",
                        render: function (data, type, full, meta) {
                            if (full.isWebAddressEditable) {
                                if (($(window).width() < MobileScreenSize)) {
                                    return ("<a target='_blank' href='" + full.webAddress + "' class='cursor-pointer'>" + data + "</a>");
                                }
                                else {
                                    return data;
                                }

                            }
                            else {
                                return data;
                            }
                        }
                    },
                    {
                        className: "data-text-align",
                        data: "type",
                        width: "90px",
                        render: function (data, type, full, meta) {
                            return GetCellData('Type', data);
                        }
                    },
                    {
                        className: "data-datetime-align",
                        data: "createdOn",
                        width: "100px",
                        render: function (data, type, full, meta) {
                            return GetFormattedDate(type, 'Created Date', data);
                        }
                    }
                ]
            });

            $('#dtDownloadAttachment tbody').on('click', 'a.documentDownload', function () {
                var data = DownloadAttachment.row($(this).parents('tr')).data();
                DownloadSelectedAttachment(data);
            });

        }
    });
}

function DownloadSelectedAttachment(selectedItem) {
    console.table(selectedItem);
    var documentId = (selectedItem.ettId != null && selectedItem.ettId != 'undefined') ? selectedItem.ettId.trim() : '';
    var documentFileName = (selectedItem.cloudFileName != null && selectedItem.cloudFileName != 'undefined') ? selectedItem.cloudFileName.trim() : '';
    var documentCategory = (selectedItem.documentCategory != null && selectedItem.documentCategory != 'undefined') ? selectedItem.documentCategory : '';
    var input = {
        "identifier": documentId,
        "fileName": documentFileName,
        "documentCategory": documentCategory
    };
    var fileName = selectedItem.title.trim();

    $.ajax({
        url: "/Defect/DownloadDocument",
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

function CloseDefect() {
    let IsOffHireRequired = $('input[name="offhire"]:checked').val() == "Yes" ? true : false;
    let ActualTimeDay = IsOffHireRequired ? $('#txtInputDay').val() : null;
    let OffHireHours = IsOffHireRequired ? $('#txtInputHours').val() : null;
    let OffHireMins = IsOffHireRequired ? $('#txtInputMins').val() : null;
    let OffhireTypeId = !IsOffHireRequired && $('#selectOffHireDD').val() == "0" ? null : $('#selectOffHireDD').val();
    let ImpactId = $('#selectImpactDD').val() == "0" ? null : $('#selectImpactDD').val();
    let IsRegulatoryAuthority = $('input[name="regular"]:checked').val() == "Yes" ? true : false;
    let DispensationInPlace = $('input[name="dispensation"]:checked').val() == "Yes" ? true : false;
    let IsGasFree = $('input[name="gas"]:checked').val() == "Yes" ? true : false;
    let OffHireReason = $('#txtReasonForOffHire').val().trim();

    //validationbefore
    let inputRequest = {
        "IsOffHireRequired": IsOffHireRequired,
        "ActualTimeDay": ActualTimeDay,
        "OffHireHours": OffHireHours,
        "OffHireMins": OffHireMins,
        "OffhireTypeId": OffhireTypeId,
        "ImpactId": ImpactId,
        "IsRegulatoryAuthority": IsRegulatoryAuthority,
        "DispensationInPlace": DispensationInPlace,
        "IsGasFree": IsGasFree,
        "OffHireReason": OffHireReason,
        "EncryptedDWOId": $('#EncryptedDWOId').val()
    }

    $.ajax({
        url: "/Defect/CloseSelectedDefect",
        type: "POST",
        "data": {
            "inputRequest": inputRequest
        },
        "datatype": "JSON",
        success: function (data) {
            $("#closureDefectModal").modal('hide');
            if (data != null) {
                if (data == true) {
                    $("#defectCloseWOSuccessModal").modal('show');
                    $('#btnCloseDefectWOSuccessOk').off();
                    $('#btnCloseDefectWOSuccessOk').on('click', function () {
                        $("#defectCloseWOSuccessModal").modal('hide');
                        ApprovalSuccess(DefectDetailsPageKey);
                    });
                }
                else {
                    ToastrAlert("validate", "Work order update failed.")
                }
            }
        }
    });
}

function ApprovalSuccess(keyName) {
    $.ajax({
        url: "/Defect/GetDefectApprovalSuccesUrl",
        type: "POST",
        dataType: "JSON",
        data: {
            "pageKey": keyName
        },
        success: function (data) {
            if (data != null) {
                window.location.replace(data);
            }
        }
    });
}

function DefectClosureDataLoad() {
    $('#spanClosureVesselName').text($('#VesselName').val());
    GetWODetailForReportDefect();
    PostGetReportedDefectWOForPreview();
    GetDefectReportWOForEdit();
}

function GetWODetailForReportDefect() {
    $.ajax({
        url: "/Defect/GetWODetailForReportDefect",
        type: "GET",
        dataType: "JSON",
        data: {
            "encryptedDWOId": $('#EncryptedDWOId').val()
        },
        success: function (data) {
            if (data != null) {
                PostGetComponentHierarchy(data.componentId, data.systemAreaId);
                $('#spanClosureDefectName').text(data.defectName);
                $('#spanClosureDefectNo').text(data.defectNumber);
                $('#spanClosureCategory').text(data.category);
                $('#spanClosureAssignedTo').text(data.staffType);
                $('#spanClosureSiteType').text(data.siteType);
                $('#spanClosurePriority').text(data.priority);
                $('#spanClosureDueDate').text(data.dueDate);
            }
        }
    });
}

function PostGetReportedDefectWOForPreview() {
    $.ajax({
        url: "/Defect/PostGetReportedDefectWOForPreview",
        type: "Post",
        dataType: "JSON",
        data: {
            "encryptedVesselId": $("#EncryptedVesselId").val(),
            "encryptedDWOId": $('#EncryptedDWOId').val()
        },
        success: function (data) {
            if (data != null) {
                $('#spanClosureDefectTitle').text(data.defectName);
                $('#spanClosureSystemArea').text(data.systemAreaName);
                $('#spanClosureCompletedOn').text(data.completedOn);
                $('#spanClosureDetailsDueDate').text(data.dueDate);
                $('#spanClosureCondBef').text(data.conditionBeforeWorkDone);
                $('#spanClosureCondAfter').text(data.conditionAfterWorkDone);
                $('#spanClosureSymptoms').text(data.symptomsObeserved);
                $('#spanClosureAdditionalJobs').text(data.isAdditionalJobReported);
                $('#spanClosureRemarksAndFindings').text(data.remarkAndFindings);
                LoadPartsUsedInClosureAction(data.partsUsed)
            }
        },
        complete: function () {
            MatchHeightOfDefectClosureSection();
        }
    });
}

function PostGetComponentHierarchy(componentId, systemAreaId) {
    var request = {
        "VesselId": $('#EncryptedVesselId').val(),
        "ComponentId": componentId,
        "SystemAreaId": systemAreaId
    }
    $.ajax({
        url: "/Defect/PostGetComponentHierarchy",
        type: "Post",
        dataType: "JSON",
        data: {
            "requestVM": request
        },
        success: function (data) {
            if (data != null) {
                $('#spanClosureSystemAreaPath').text(data);
            }
        }
    });
}

function GetDropDownDetails(impactId) {
    $.ajax({
        url: "/Defect/GetDropDownDetails",
        type: "Post",
        dataType: "JSON",
        success: function (data) {
            //clear select drop downOffHireHours
            ClearSelectDropDown('selectImpactDD');
            if (data != null) {
                if (data.impactList != null) {
                    let impactList = data.impactList;
                    var selectImpact = document.getElementById('selectImpactDD');

                    for (var i = 0; i < impactList.length; i++) {
                        var obj = impactList[i];
                        var opt = document.createElement('option');
                        if (!IsNullOrEmptyOrUndefinedLooseTyped(impactId) && obj.dalId == impactId) {
                            opt.selected = true;
                        }
                        else {
                            opt.selected = false;
                        }
                        opt.value = obj.dalId;
                        opt.innerHTML = obj.attributeName;
                        selectImpact.appendChild(opt);
                    }
                }
            }
        }
    });
}

function ClearSelectDropDown(selectId) {
    var selDropDown = document.getElementById(selectId);
    var selDropDownLength = selDropDown.options.length;
    for (var i = selDropDownLength - 1; i >= 0; i--) {
        selDropDown.options[i] = null;
    }
    var opt = document.createElement('option');
    opt.selected = true;
    opt.value = '0';
    opt.innerHTML = 'Select';
    selDropDown.appendChild(opt);
}

function GetDefectReportWOForEdit() {
    $.ajax({
        url: "/Defect/GetDefectReportWOForEdit",
        type: "Get",
        dataType: "JSON",
        data: {
            "encryptedDWOId": $('#EncryptedDWOId').val()
        },
        success: function (data) {
            if (data != null) {
                if (data.isOffHireRequired) {
                    $("#offhireYes").prop("checked", true);
                    $("#offhireYes").click();
                }
                else {
                    $("#offhireNo").prop("checked", true);
                    $("#offhireNo").click();
                }

                if (data.isRegulatoryAuthority) {
                    $("#regular1").prop("checked", true);
                    $("#regular1").click();
                }
                else {
                    $("#regular2").prop("checked", true);
                    $("#regular2").click();
                }

                if (data.dispensationInPlace) {
                    $("#disp1").prop("checked", true);
                }
                else {
                    $("#disp2").prop("checked", true);
                }

                if (data.isGasFree) {
                    $("#gas1").prop("checked", true);
                }
                else {
                    $("#gas2").prop("checked", true);
                }

                $('#txtReasonForOffHire').val(data.offHireReason);
                $('#txtInputDay').val(data.actualTime);
                $('#txtInputHours').val(data.offHireHours);
                $('#txtInputMins').val(data.offHireMins);


                PostGetPosAttributeLookup(data.offhireTypeId);
                GetDropDownDetails(data.impactId);
            }
        },
        complete: function () {
            MatchHeightOfDefectClosureSection();
        }
    });
}

function PostGetPosAttributeLookup(offhireTypeId) {
    $.ajax({
        url: "/Defect/PostGetPosAttributeLookup",
        type: "Post",
        dataType: "JSON",
        success: function (data) {
            ClearSelectDropDown('selectOffHireDD')
            if (data != null) {
                let offHireTypeList = data;
                var selectOffHire = document.getElementById('selectOffHireDD');

                for (var i = 0; i < offHireTypeList.length; i++) {
                    var obj = offHireTypeList[i];
                    var opt = document.createElement('option');
                    if (!IsNullOrEmptyOrUndefinedLooseTyped(offhireTypeId) && obj.identifier == offhireTypeId) {
                        opt.selected = true;
                    }
                    else {
                        opt.selected = false;
                    }
                    opt.value = obj.identifier;
                    opt.innerHTML = obj.description;
                    selectOffHire.appendChild(opt);
                }

            }
        }
    });
}

function ValidationBeforeCloseDefect() {
    if ($('#offhireYes').is(':checked')) {
        let Day = $('#txtInputDay').val();
        let Hours = $('#txtInputHours').val();
        let Mins = $('#txtInputMins').val();
        if (Day > 999) {
            ToastrAlert("validate", "Actual Time Days cannot be greater than 999.");
            return false;
        }
        if (Hours > 23) {
            ToastrAlert("validate", "Actual Time Hours cannot be greater than 23.");
            return false;
        }
        if (Mins > 59) {
            ToastrAlert("validate", "Actual Time Minutes cannot be greater than 59.");
            return false;
        }
        if (Day == 0 && Hours == 0 && Mins == 0) {
            ToastrAlert("validate", "Actual Time can not be blank or zero.");
            return false;
        }
        let selectedOffHireType = $('#selectOffHireDD').val();
        if (selectedOffHireType == "0") {
            ToastrAlert("validate", "Off hire type cannot be blank.");
            return false;
        }
    }

    return true;
}

function LoadPartsUsedInClosureAction(data) {
    $('#dtpartused').DataTable().destroy();
    var SpareParts = $('#dtpartused').DataTable({
        "dom": '<<"row"<"col-12 col-md-12 offset-md-0 col-lg-8 offset-lg-2 col-xl-8 offset-xl-2 dt-infomation "i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "order": [],
        "language": {
            "emptyTable": "No parts found.",
        },
        "data": data,
        "columns": [
            {
                className: "top-margin-0 data-text-align tdblock",
                data: "partName",
                width: "55px",
                render: function (data, type, full, meta) {
                    return GetCellData('Part Name', data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "makerReferenceNumber",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetCellData("Maker's Reference No.", data);
                }
            },
            {
                className: "data-number-align tdblock",
                data: "quantityUsed",
                width: "25px",
                render: function (data, type, full, meta) {
                    return GetCellData("Qty", data);
                }
            }
        ],
        "initComplete": function () {
            $('#spanPartsUsed').text(this.api().data().length);
            if (screen.width > MobileScreenSize) {
                $('.height-equal-boxes-two').matchHeight({
                    byRow: 0
                });
            }
        }
    });
}

function MatchHeightOfDefectClosureSection() {
    if (screen.width > MobileScreenSize) {
        //$('.height-equal-boxes').matchHeight({
        //    byRow: 0
        //});
        var height = $('.height-equal-boxes-defect').outerHeight();
        $(".height-equal-boxes-remark").css({
            "height": height
        });
        var height = $('.height-equal-boxes-remark').height();
        var height2 = $('#remarks').height();
        $(".height-equal-boxes-remark .details-common").css({
            "height": height - height2
        });
        $('.height-equal-boxes-two').matchHeight({
            byRow: 0
        });
    }
}

function ClearInptTextField() {
    let day = $("#txtInputDay").val();
    let hours = $("#txtInputHours").val();
    let mins = $("#txtInputMins").val();

    if (day == "" || day == 0) {
        $("#txtInputDay").val("");
    }
    if (hours == "" || hours == 0) {
        $("#txtInputHours").val("");
    }
    if (mins == "" || mins == 0) {
        $("#txtInputMins").val("");
    }
}

function CheckIfNumberIsInteger(input) {
    var intRegex = /^\d+$/;    
    if (intRegex.test(input)) {
        return true;
    }
    return false;
}