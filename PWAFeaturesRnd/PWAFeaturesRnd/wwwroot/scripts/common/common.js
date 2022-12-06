import "bootstrap";
import "select2/dist/js/select2.full.js";
import "@chenfengyuan/datepicker";
import "daterangepicker";
import moment from "moment";
import * as JSZip from "jszip";
window.JSZip = JSZip;
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { AjaxError, GetNoticationUnreadChannelCountDashboard, GetCookie, ToastrAlert, saveByteArray, base64ToArrayBuffer, GetRoleRightsAsync, AddClassIfAbsent, AddModelLoadingIndicator, RemoveModelLoadingIndicator, RemoveClassIfPresent, ReplaceClass, IsNullOrEmptyOrUndefinedLooseTyped, GetLastReferrer, SetHeight, IsNullOrEmptyOrUndefined, GetDiscussionNotesCount, GetStringNullOrWhiteSpace, UpdateUnreadMessagesOnDashboard, NavigateToNotification, IsJsonString, ErrorLog, NavigateToNotificationDashboard } from "../common/utilities.js"
import { GetCellData, GetExportCellData, GetExportData, GetExportFormattedDate, GetFormattedDateTime, GetFormattedDate } from "../common/datatablefunctions.js"
import 'signalR'
import { MobileScreenSize, TabScreenSize, NoteCompleted, NotificationMobileChatDetailKey, NotificationPageKey, NotificationMobileInfoKey, NotificationMobileDiscussionKey, NotificationChatPageKey } from "../common/constants.js"
import "../lib/dataTables.checkboxes.min.js"
import { DownloadAttachment, DeleteDocument, ShowNoteListSection, EditNote, DeleteNote, LoadNotesVesselSearch, cb, CancelReminder, OpenAddNotes, SaveNote, CompleteNote, NavigateToNoteRecordDetails, LoadCompletedTabNotes, LoadCurrentTabNotes, UpdateNotesTab, LoadAllTabNotes, MoveToCurrent, UpdateDashboardReminderAlertTab, DeleteNoteAttachment, UploadAttachmentEvent, CompleteNoteFromEdit, MoveToCurrentFromEdit, DeleteNoteFromEdit } from "../common/notesUtilities"
import "block-ui";

var headerIsAppend = false;
var IsMobile = true;
var IscboNotesVesselSearchSelectEventOccured = false;
var IsNotificationDraftSelectEventOccured = false;

var dashboardVesselDetails = {
    VesselId: "",
    VesselName: "",
};
var PreSelectedUsers = [];
var FilesAttached = 0;
var AttachedFiles = [];
var SelectedUser = [];

var gridAvailableVesselList, gridUserFleetList, dtInspectionPlanningDetails, gridMappedVesselList, dtTaskNotifications;
var AddUserFleet = "AD167786-2F89-433A-9500-481EBEEA2C2A";
var EditUserFleet = "E238AA19-1BD4-4AC2-9FFE-393ABD1EE4CF";
var DemoControlId = '346F8CA4-AF18-4AF0-9454-5A949048EECB';
var ChatControlId = '33FCEC99-1DC4-4C0A-94C4-FCC59123C116';


var validationMesssage = {
    MappedVesselValidationMesssage: "At least one vessel should be mapped.",
}

//This mouseup event for notes or notification.
$(document).mouseup(function (e) {
    var container = $(".form-control,.notes-sidebar-open, .select2-container, .daterangepicker, .toast, #CreateDraftChannel, #searchparticipant, #notificationVesselChangeConfirmationModal,#VChatModalDraftDiscussion, #notesComfirmAsDeleteModal ");

    if (!container.is(e.target) && container.has(e.target).length === 0) {

        if ($(".notes-sidebar-open").hasClass('settings-open') && !IscboNotesVesselSearchSelectEventOccured) {
            if ($('.add-note:visible').is(':visible')) {
                if (!$(e.target).hasClass('clickEventDisable')) {
                    SaveNote(false);
                }
            }
        }

        if ($("#modalDraftChannelBody").data("createdraft") == "1" && !IsNotificationDraftSelectEventOccured) {
            CreateChannel(true);
            $('#modalDraftChannelBody').data("createdraft", "0");
        }

    }
    else {
        if ($('#VChatModalDraftDiscussion').is(':visible')) {
            $('#VChatModalDraftDiscussion').data("createdraft", "1");
        }
    }
    IsNotificationDraftSelectEventOccured = false;
    IscboNotesVesselSearchSelectEventOccured = false;
    $('#NotificationCboSelected').val("0");
});



$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        AddClassIfAbsent('#addlistcircle', 'addfixedMobile');
    } else {
        RemoveClassIfPresent('#addlistcircle', 'addfixedMobile');
    }
});

$(document).on('click', '.preferenceHeaderRight', function () {
    $("#btnSaveUserPreferences").prop("disabled", true);
    BindUserPreferences();
});

$(document).on('click', '#btnOffline', OpenOfflineModal);

$(document).on('click', '#btnSaveOffline', fn_TakeAppOffline);


$(document).on('click', '.showFromAgentDetails1', function () {
    let urlRequest = $(this).data('encryptedposid');
    let portname = decodeURIComponent($(this).data('portname'));
    CurrentVoyageAgentDetails(urlRequest, portname);
});


$(document).ready(function () {

    $.ajax({
        url: "/Common/GetAccessDetails",
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                if (data.token != null) {
                    $("#hdnAPIToken").val(data.token);
                }
                if (data.emailId != null) {
                    $("#hndUserEmail").val(data.emailId);
                }
            }
        }
    });

    if (($(window).width() < MobileScreenSize)) {
        $('#modalSendEmail').on('shown.bs.modal', function (e) {
            var windowheight3 = $(window).height();
            var modalheader3 = $('#modalSendEmail .modal-header').outerHeight();
            $(".vesselselectscroll").css({
                "max-height": windowheight3 - modalheader3 - 130,
                "height": windowheight3 - modalheader3 - 130
            });
        });
    }

    if (($(window).width() < MobileScreenSize)) {
        $('#modalManageUserFleet').on('shown.bs.modal', function (e) {
            var windowheight = $(window).height();
            var modalheader = $('.mobilescrollerheightmodal .modal-header').outerHeight();
            $(".mobilescrollerheightmodal .modal-body .scrollermobile").css({
                "max-height": windowheight - modalheader - 15,
                "height": windowheight - modalheader - 15
            });
        });
    }

    if (($(window).width() < MobileScreenSize)) {
        $('#modalAddEditUserFleet').on('shown.bs.modal', function (e) {
            var windowheight2 = $(window).height();
            var modalheader2 = $('.mobilescrollerheightmodal2 .modal-header').outerHeight();
            var modalfooter2 = $('.mobilescrollerheightmodal2 .modal-footer').outerHeight();
            $(".mobilescrollerheightmodal2 .modal-body .scrollermobile").css({
                "max-height": windowheight2 - modalheader2 - modalfooter2 - 15,
                "height": windowheight2 - modalheader2 - modalfooter2 - 15
            });
        });
    }

    if (($(window).width() < MobileScreenSize)) {
        $('#addvesselavaiablemodal').on('shown.bs.modal', function (e) {
            var windowheight3 = $(window).height();
            var modalheader3 = $('.mobilescrollerheightmodal3 .modal-header').outerHeight();
            var modalfooter3 = $('.mobilescrollerheightmodal3 .modal-footer').outerHeight();
            $(".mobilescrollerheightmodal3 .modal-body .scrollermobile").css({
                "max-height": windowheight3 - modalheader3 - modalfooter3 - 15,
                "height": windowheight3 - modalheader3 - modalfooter3 - 15
            });
        });
    }

    if ($('#IsAllowSelectFleetParent').val() === 'true') {
        AddClassIfAbsent('#isAllowSelectFleetMessage', 'd-none');
    }
    else {
        RemoveClassIfPresent('#isAllowSelectFleetMessage', 'd-none');
    }

});




$(document).on('click', '.notesmarkascomplete input[type="checkbox"]', function () {
    let checkbox = $(this);
    let id = $(checkbox).data('id');
    if ($(checkbox).is(":checked")) {
        CompleteNote(id, checkbox);
    } else {
        MoveToCurrent(id, checkbox);
    }
});

$(document).on('hidden.bs.modal', '#modalVChat,#modalCreateNewChannel', function (e) {
    UpdateUnreadMessagesOnDashboard();
    GetNoticationUnreadChannelCountDashboard();
    //sessionStorage.removeItem(NotificationPageKey);
    //sessionStorage.removeItem(NotificationChatPageKey);
});

$(document).on('hidden.bs.modal', '#modalVChat', function (e) {
    $(".modal-backdrop").remove();
    $('#iframeNotification').empty();
    window.parent.updateChatAndNotesCount();
    var channelNodes = (document.getElementsByClassName('baseDiscussionCountCommon'));
    var referenceIds = []
    for (var i = 0; i < channelNodes.length; i++) {
        let messageDetailsJSONstr = $(channelNodes[i]).parent().data('messagejson');
        let messageDetailsJSON = JSON.parse(decodeURIComponent(messageDetailsJSONstr));
        let messageDetailsObj;

        if (IsJsonString(messageDetailsJSON)) {
            messageDetailsObj = JSON.parse(messageDetailsJSON);
        }
        else {
            messageDetailsObj = JSON.parse(JSON.stringify(messageDetailsJSON));
        }
        referenceIds.push(messageDetailsObj)
    }
    GetChannelAndNotesCountByReferenceId(referenceIds);
});

function RoleRightForDemo(rolerights) {
    var demoAccess = rolerights.find(x => x.controlId.toLowerCase() === DemoControlId.toLowerCase());
    if (demoAccess != null && demoAccess != 'undefined' && demoAccess != '' && demoAccess.permission) {
        RemoveClassIfPresent(".btnToggleDemoMode", "d-none");
    }
    else {
        AddClassIfAbsent(".btnToggleDemoMode", "d-none");
    }
    var chatAccess = rolerights.find(x => x.controlId.toLowerCase() === ChatControlId.toLowerCase());
    if (chatAccess != null && chatAccess != 'undefined' && chatAccess != '' && chatAccess.permission) {
        RemoveClassIfPresent(".aBaseNotification", "d-none");
    }
    else {
        AddClassIfAbsent(".aBaseNotification", "d-none");
    }
}

function GetChannelAndNotesCountByReferenceId(input) {
    $.ajax({
        url: "/Dashboard/GetListLevelRecordDiscussionCountByReferenceId",
        type: "POST",
        dataType: "JSON",
        data: {
            "input": input
        },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var channelCount = isNaN(data[i].channelCount) ? 0 : parseInt(data[i].channelCount);
                if (channelCount > 0) {
                    $('#baseChannelCount' + data[i].referenceIdentifier).text(data[i].channelCount)
                }
                else {
                    $('#baseChannelCount' + data[i].referenceIdentifier).parent().remove()
                }
            }
        }
    });
}

function BindUserPreferences() {
    $.ajax({
        url: "/Dashboard/GetUserPreferences",
        type: "GET",
        dataType: "JSON",
        beforeSend: function (xhr) {
            AddModelLoadingIndicator("#preference .card-body");
        },
        success: function (data) {
            SetCheckbox('#CertificatePrefCheckbox', data.isCertificateEnabled, data.certificate);
            SetCheckbox('#CommercialPrefCheckbox', data.isCommercialEnabled, data.commercial);
            SetCheckbox('#CrewPrefCheckbox', data.isCrewEnabled, data.crew);
            SetCheckbox('#DefectPrefCheckbox', data.isDefectEnabled, data.defect);
            SetCheckbox('#EnvironmentPrefCheckbox', data.isEnvironmentEnabled, data.environment);
            SetCheckbox('#FinancialPrefCheckbox', data.isFinancialEnabled, data.financial);
            SetCheckbox('#HazoccPrefCheckbox', data.isHazoccEnabled, data.hazocc);
            SetCheckbox('#PMSPrefCheckbox', data.isPMSEnabled, data.pms);
            SetCheckbox('#OpenOrdersPrefCheckbox', data.isProcurementEnabled, data.procurement);
            SetCheckbox('#InspectionPrefCheckbox', data.isInspectionEnabled, data.inspection);
        },
        complete: function () {
            RemoveModelLoadingIndicator("#preference .card-body");
        }
    });
}

$(document).on('click', '#btnSaveUserPreferences', function () {

    $("#btnSaveUserPreferences").prop("disabled", true);

    let checkboxes = $("#preference input:checkbox").map(function () {
        return {
            isPreferred: this.checked,
            PreferenceKey: $(this).prop('name'),
        }
    }).get();
    SaveUserPreferences(checkboxes);
});

function SaveUserPreferences(input) {

    $.ajax({
        url: "/Dashboard/SaveUserPreferences",
        type: "POST",
        "data": {
            "userPreferences": input,
        },
        dataType: "JSON",
        beforeSend: function (xhr) {
            AddModelLoadingIndicator("#preference .card-body");
        },
        success: function (data) {
            if (data != null) {
                if (data.res == true) {
                    $('#preference').modal('hide');
                    ToastrAlert("success", data.msg);
                    let lastReferrer = GetLastReferrer();
                    if (lastReferrer.includes('Dashboard') || lastReferrer.includes('VesselDetailsMobile')) {
                        setTimeout(function () {
                            location.reload();
                        }, 1500);
                    }
                } else {
                    ToastrAlert("error", data.msg);
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator("#preference .card-body");
            $("#btnSaveUserPreferences").prop("disabled", false);
        }
    });
}

function SetCheckbox(jqueryid, condition, key) {
    $(jqueryid).prop('name', key);
    if (condition == true) {
        $(jqueryid).prop('checked', true);
        ReplaceClass($(jqueryid).parent(), 'off', 'on');
    } else {
        $(jqueryid).prop('checked', false);
        ReplaceClass($(jqueryid).parent(), 'on', 'off');
    }
}

$(document).on('click', '#dtFleetList tbody .addEditFleet', function () {
    var data = gridUserFleetList.row($(this).parents('tr')).data();
    $('#modalAddEditUserFleet').modal('show');
    $('#addbuttonmobile').show();
    $('.mobilemappedremovebox ').hide();
    OpenAddEditFleetModal(data.fleetId, data.fleetName, data.isFleetActive);
});

$(document).on('click', '#dtTaskNotifications .downloadDocument', function () {
    var data = dtTaskNotifications.row($(this).parents('tr')).data();
    DownloadTaskMessageDocument(data);
});

$(document).on('click', '.dashboardInspectionPlanningDetils', function () {
    fn_dashboardVesselDetails(this);
});

$(document).on('show.bs.modal', '.modal', function () {
    $(".modal-backdrop").addClass("d-none");
});

$(document).on("change", ".available-master-checkbox", function () {
    if ($(this).prop('checked') == false) {
        $("#dtAvailableVesselList tbody tr").removeClass("row_selected");
    }
    else {
        $("#dtAvailableVesselList tbody tr").addClass('row_selected');
    }
    fn_EnabledDisabledFleetGridButton(gridAvailableVesselList, 'btnAddtoMapped');
});

$(document).on("change", ".mapped-master-checkbox", function () {
    if ($(this).prop('checked') == false) {
        $("#dtMappedVesselList tbody tr").removeClass('row_selected');
    }
    else {
        $("#dtMappedVesselList tbody tr").addClass('row_selected');
    }
    fn_EnabledDisabledFleetGridButton(gridMappedVesselList, 'btnRemoveFromMapped');
});

$(document).on('hidden.bs.modal', '.modal', function (e) {

    var container = $("#modalCreateNewChannel");
    if (container.is(e.target) == true) {
        $("#modalDraftChannelBody").data("createdraft", "0");
    }

    var divCount = $(".modal-backdrop.d-none").length;
    if (divCount > 2) {
        $(".modal-backdrop:gt(1)").removeClass("d-none");
    }
    else {
        $(".modal-backdrop").removeClass("d-none");
    }
});

$(document).on('click', '#collapsenotes', function () {
    ToggleNotesDetails(this);
});

function HideNotesAdditionalDetails() {
    AddClassIfAbsent($(document).find(".activetab li .notes-desc"), 'notes-desc-hide');
    AddClassIfAbsent($(document).find(".activetab li .notes-date"), 'notes-date-hide');
    AddClassIfAbsent($(document).find(".activetab li .notes-box"), 'icons-hide');
    RemoveClassIfPresent($(document).find(".activetab li .notes-sidebar-open .collapse-date"), 'collapse-date-show');
}

function ShowNotesAdditionalDetails() {
    RemoveClassIfPresent($(document).find(".activetab li .notes-desc"), 'notes-desc-hide');
    RemoveClassIfPresent($(document).find(".activetab li .notes-date"), 'notes-date-hide');
    RemoveClassIfPresent($(document).find(".activetab li .notes-box"), 'icons-hide');
    AddClassIfAbsent($(document).find(".activetab li .notes-sidebar-open .collapse-date"), 'collapse-date-show');
}

function ToggleNotesDetails(expander) {
    if ($(expander).hasClass('expanded')) {
        RemoveClassIfPresent(expander, 'expanded');
        ShowNotesAdditionalDetails();
    } else {
        AddClassIfAbsent(expander, 'expanded');
        HideNotesAdditionalDetails();
    }
}


$(document).on('click', '.editNote', function () {
    var parent = $(this).parents('.notes-box')[0];
    var noteId = $(parent).data('id');
    EditNote(noteId);
});

$(document).on('click', '.deleteNote', function () {
    var noteId = $(this).attr('id');

    $("#notesComfirmAsDeleteModal").modal('show');
    $('#yesConfirmAsDeleteButton').off();
    $('#yesConfirmAsDeleteButton').on('click', function () {
        DeleteNote(noteId);
        $("#notesComfirmAsDeleteModal").modal('hide');
    });

    $('#noConfirmAsDeleteButton').off();
    $('#noConfirmAsDeleteButton').on('click', function () {
        $("#notesComfirmAsDeleteModal").modal('hide');
    });
});

$(document).on('click', '#deleteNoteForEditMode', function () {
    $("#notesComfirmAsDeleteModal").modal('show');
    $('#yesConfirmAsDeleteButton').off();
    $('#yesConfirmAsDeleteButton').on('click', function () {
        DeleteNoteFromEdit();
        $("#notesComfirmAsDeleteModal").modal('hide');
    });

    $('#noConfirmAsDeleteButton').off();
    $('#noConfirmAsDeleteButton').on('click', function () {
        $("#notesComfirmAsDeleteModal").modal('hide');
    });
});

//To create a chat from notes
$(document).on('click', '.createVChat', function () {
    var noteId = $(this).attr('id');
    CreateDiscussionRequest(noteId);
});

$(document).on('click', '.navigateToDetails', function () {
    let encryptedNoteId = $(this).data('noteid');
    let encryptedVesselId = $(this).data('vesselid');
    NavigateToNoteRecordDetails(encryptedNoteId, encryptedVesselId)
});

$(document).on('click', '.attachment-close', function () {
    let sequence = $(this).parent().data('sequence');
    if (!$('.add-note').is(':visible')) {
        var filteredFiles = AttachedFiles.filter(x => {
            return x.sequence != sequence;
        });
        AttachedFiles = filteredFiles;
    }
    $(this).parent().remove();
});

$(document).on('click', '#noteSearch', function () {
    UpdateNotesTab();
});

$(document).on('click', '#noteSearchCancel', function () {
    $("#notesSearchText").val('');
    UpdateNotesTab();
});

$(document).on('keypress', function (e) {
    //if user presses enter with shiftkey follow natural behaviour 
    //if user presses only enter follow the below logic

    if (screen.width < MobileScreenSize) {
        if ($("#notesSearchText").is(":focus")) {
            if (e.which == 13) {
                UpdateNotesTab();
            }
        }
        else if ($(".notesFocusable").is(":focus")) {
            if (e.which == 13) {
                e.preventDefault();
            }
        }
    }
});

$(document).on('click', '.sendMail', function () {
    //Adelate, Check Balance, PO, 344 - 90999
    let parent = $(this).parents('.notes-box')[0];
    let noteId = $(parent).data('id');

    GetNoteDetails(noteId, function (data) {
        let vesselName = data.vesselName;
        let noteTitle = data.noteTitle;
        let area = data.categoryName;
        let record = '';
        let subjectLst = [];
        if (!IsNullOrEmptyOrUndefinedLooseTyped(vesselName)) {
            subjectLst.push(vesselName);
        }
        if (!IsNullOrEmptyOrUndefinedLooseTyped(noteTitle)) {
            subjectLst.push(noteTitle);
        }
        if (!IsNullOrEmptyOrUndefinedLooseTyped(area)) {
            subjectLst.push(area);
        }
        if (!IsNullOrEmptyOrUndefinedLooseTyped(record)) {
            subjectLst.push(record);
        }
        let msgBody = '';
        if (!IsNullOrEmptyOrUndefinedLooseTyped(data.noteDescription)) {
            msgBody = data.noteDescription;
        }

        var link = "mailto:"
            + "?"
            + "subject=" + encodeURIComponent(subjectLst.join(', '))
            + "&body=" + encodeURIComponent(msgBody);
        window.location.href = link;
    });
});

$(document).on('click', '.communicationmode', function () {
    let email = $(this).data('number').trim()
    var link = "mailto:"
        + email;
    window.location.href = link;
});

$(document).on('click', '.vesselcommunicationlinkA', function () {

    $("#modalVesselCommunication").modal('show');

    $('#dtVesselCommunication').DataTable().destroy();
    var dtVesselCommunication = $('#dtVesselCommunication').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": true,
        "paging": false,
        "scrollY": "150px",
        "order": [],
        "language": {
            "emptyTable": "No Vessel Communication.",
        },
        "ajax": {
            "url": "/Dashboard/GetVesselCommunication",
            "type": "POST",
            "data": { "VesselId": $(this).find(".vesselcommunicationlink").data('id') },
            "datatype": "json"
        },
        "columns": [
            {
                className: "tdblock td-row-header font-weight-600 data-text-align d-sm-none",
                data: "comNumber",
                width: "80px",
                render: function (data, type, full, meta) {
                    var communicationhtml = ""
                    if (full.isEmail == true || full.isEmail == 'true' || full.isEmail == 'True') {
                        communicationhtml = '<a href="javascript:void(0);" class="communicationmode" data-number="' + DefaultCellData(data) + '">' + DefaultCellData(data) + '</a>';
                    }
                    else {
                        if (($(window).width() < MobileScreenSize)) {
                            communicationhtml = '<a href="tel:' + DefaultCellData(data).trim() + '">' + DefaultCellData(data) + '</a>';
                        }
                        else {
                            communicationhtml = '<a href="javascript:void(0);">' + DefaultCellData(data) + '</a>';
                        }
                    }
                    return GetCellData('', communicationhtml);
                }
            },
            {
                className: "data-text-align",
                data: "primaryContact",
                width: "40px",
                render: function (data, type, full, meta) { return GetCellData('Primary', DefaultCellData(data)); }
            },
            {
                className: "data-text-align",
                data: "ctyName",
                width: "60px",
                render: function (data, type, full, meta) { return GetCellData('Type', DefaultCellData(data)); }
            },
            {
                className: "d-none d-sm-table-cell data-text-align",
                data: "comNumber",
                width: "80px",
                render: function (data, type, full, meta) {
                    var communicationhtml = ""
                    if (full.isEmail == true || full.isEmail == 'true' || full.isEmail == 'True') {
                        communicationhtml = '<a href="javascript:void(0);" class="communicationmode" data-number="' + DefaultCellData(data) + '">' + DefaultCellData(data) + '</a>';
                    }
                    else {
                        if (($(window).width() < MobileScreenSize)) {
                            communicationhtml = '<a href="tel:' + DefaultCellData(data).trim() + '">' + DefaultCellData(data) + '</a>';
                        }
                        else {
                            communicationhtml = '<a href="javascript:void(0);">' + DefaultCellData(data) + '</a>';
                        }
                    }
                    return GetCellData('Number/Email', communicationhtml);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "comDesc",
                width: "110px",
                render: function (data, type, full, meta) { return GetCellData('Comments', DefaultCellData(data)); }
            },
            {
                className: "data-text-align",
                data: "cmpName",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Provider', DefaultCellData(data)); }
            },
            {
                className: "data-datetime-align",
                data: "comStartDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    if (($(window).width() < MobileScreenSize)) {
                        if (data == null || data == '' || data == undefined) {
                            data = '-';
                            return GetCellData('Contract Start Date', data);
                        }
                        else {
                            return GetFormattedDate(type, 'Start Date', data);
                        }
                    }
                    else {
                        return GetFormattedDate(type, 'Start Date', DefaultCellData(data));
                    }
                }
            },
            {
                className: "data-datetime-align",
                data: "comExpiryDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    if (($(window).width() < MobileScreenSize)) {
                        if (data == null || data == '' || data == undefined) {
                            data = '-';
                            return GetCellData('Contract Expiry Date', data);
                        }
                        else {
                            return GetFormattedDate(type, 'Contract Expiry Date', data);
                        }
                    }
                    else {
                        return GetFormattedDate(type, 'Contract Expiry Date', DefaultCellData(data));
                    }
                }
            }
        ],
        "initComplete": function (settings) {

        }
    });
});


function DefaultCellData(data) {
    if (($(window).width() < MobileScreenSize)) {
        if (data == null || data == '' || data == undefined) {
            data = '-';
            return data;
        }
    }
    return data;
}

$(document).on('click', '.aReminderCompleteNote', function () {
    let checkbox = $(this);
    let parent = $(this).parents('.repeat-notes')[0];
    let latestReminderIdObject = $(parent).find('.notesReminder')[0];
    let reminderId = $(latestReminderIdObject).data("reminderid");
    let noteId = $(latestReminderIdObject).data("noteid");
    if ($(checkbox).is(":checked")) {
        let completeNoteRequest = {
            "ReminderId": reminderId,
            "NoteId": noteId,
            "ReminderFeatureStatus": 2
        }
        UpdateReminderStatus(completeNoteRequest, function () {
            AddClassIfAbsent(parent, 'd-none');
            UpdateReminderView();
            ToastrAlert("success", NoteCompleted);
        });
    }
});

//$(document).on('click', '.reminderAlert > .remindersectionhead > .aReminderClose', function () {
//    UpdateNotesTab();
//    AddClassIfAbsent(parent, 'd-none');
//    UpdateReminderView();
//    UpdateDashboardReminderAlertTab();
//});

$(document).on('click', '.dismissReminderAction', function () {
    let parent = $(this).parents('.repeat-notes')[0];
    let latestReminderIdObject = $(parent).find('.notesReminder')[0];
    let reminderId = $(latestReminderIdObject).data("reminderid");
    let dismissReminderObject = {
        "ReminderId": reminderId
    }
    UpdateReminderToDismiss(dismissReminderObject, function () {
        AddClassIfAbsent(parent, 'd-none');
        UpdateReminderView();
    });
});


$(document).on('click', '.aReminderSnooze', function () {
    let parent = $(this).parents('.repeat-notes')[0];
    let latestReminderIdObject = $(parent).find('.notesReminder')[0];
    let minuteInterval = $(parent).find('.selMinuteInternal option:selected')[0];
    let reminderId = $(latestReminderIdObject).data("reminderid");
    let noteId = $(latestReminderIdObject).data("noteid");

    let selectedMinuteValue = $(minuteInterval).val();
    let intervalInText = $(minuteInterval).text();
    let minuteIntervalConverted = parseInt(selectedMinuteValue);
    var localDate = moment(new Date()).add(minuteIntervalConverted, 'm').toDate();
    let ReminderDateTimeUTC = new Date(new Date(localDate).toUTCString().substr(0, 25));
    let ReminderDateTimeUTCFormatted = moment(ReminderDateTimeUTC).format("DD MMM YYYY HH:mm");
    let snoozeRequest = {
        "ReminderId": reminderId,
        "NoteId": noteId,
        "ReminderFeatureStatus": 1,
        "ExpectedExecutionTime": ReminderDateTimeUTCFormatted
    }
    UpdateReminderStatus(snoozeRequest, function () {
        AddClassIfAbsent(parent, 'd-none');
        UpdateReminderView();
        let reminderSnoozedText = 'Reminder has been snoozed for ' + intervalInText + '.';
        ToastrAlert("success", reminderSnoozedText);
    });
});

$(document).on('click', '.attachmentClick', function () {
    let id = $(this).data('id');
    let desc = $(this).data('desc');
    let type = $(this).data('type');
    let request = {
        'EttId': id,
        'Description': desc,
        'FileExtension': type
    };
    DownloadAttachment(request);
});

var connection;


$(document).on('click', '.attachment-close', function () {
    DeleteNoteAttachment(this);
});

//notificationclosemodal
$(document).on('click', '#notificationclosemodal', function () {
    var response = document.getElementById('iframeCreateChannel').contentWindow.closeNotificationModalAction();
    if (response == true) {
        $("#modalCreateNewChannel").modal('hide');
    }
    else {
        $("#notificationDraft").modal('show');
    }
});

//modalVChatClose
$(document).on('click', '#modalVChatClose', function () {
    var response = document.getElementById('NotificationContainerFrame').contentWindow.closeNotificationModalAction();
    if (response == true) {
        $("#modalVChat").modal('hide');
    }
    else {
        $("#notificationDraft").modal('show');
    }
});

//noNotificationDraft
$(document).on('click', '#noNotificationDraft', function () {
    var response = true;
    try {
        response = document.getElementById('iframeCreateChannel').contentWindow.noNotificationDraftAction();
    }
    catch (err) {
    }

    try {
        response = document.getElementById('NotificationContainerFrame').contentWindow.noNotificationDraftAction();
    }
    catch (err) {
    }

    if (response == true) {
        $("#modalVChat").modal('hide');
        $("#modalCreateNewChannel").modal('hide');
    }
});

//yesNotificationDraft
$(document).on('click', '#yesNotificationDraft', function () {
    var response = true;

    try {
        response = document.getElementById('iframeCreateChannel').contentWindow.yesNotificationDraftAction();
    }
    catch (err) {
    }

    try {
        response = document.getElementById('NotificationContainerFrame').contentWindow.yesNotificationDraftAction();
    }
    catch (err) {
    }

    if (response == true) {
        $("#modalVChat").modal('hide');
        $("#modalCreateNewChannel").modal('hide');
    }
});






$(document).ready(function () {
    if (screen.width < 767) {
        var header = $('.app-header').outerWidth();
        var menu = $('.menumobile').outerWidth();
        var home = $('.app-header__mobile-menu .header-dots').outerWidth();
        var vchat = $('.header-dots .aBaseNotification').width();
        var more = $('.mobile-toggle-header-nav').outerWidth();
        $(".vesseldropdownmobile").css({
            "width": header - menu - home - vchat - more - 30
        });
    }

    AjaxError();
    $(".userDropdownClose").click(function () {
        $('#headerdropdown').toggleClass('show');
        $('#headerdropdown .user-logout').toggleClass('show');
    });

    //Get role right
    GetRoleRightsAsync([DemoControlId, ChatControlId], function (rolerights) {
        RoleRightForDemo(rolerights);
    });

    $.blockUI.defaults = {
        /*timeout: 10000,*/
        fadeIn: 200,
        fadeOut: 400,
    };

    if (screen.width > 1600) {
        var windowwidth = $(window).width();
        $(".creatediscussiframe").css({
            "width": windowwidth - ((windowwidth / 100) * 48)
        });
        $(".creatediscussiframemodal").css({
            "max-width": windowwidth - ((windowwidth / 100) * 48)
        });
    }

    if (screen.width < 1599) {
        var windowwidth = $(window).width();
        $(".creatediscussiframe").css({
            "width": windowwidth - ((windowwidth / 100) * 25)
        });
        $(".creatediscussiframemodal").css({
            "max-width": windowwidth - ((windowwidth / 100) * 25)
        });
    }


    //notification iframe
    $(".notificationiframe").css({
        "width": windowwidth
    });

    $(".notificationiframemodal").css({
        "max-width": windowwidth
    });


    $.ajax({
        url: "/Dashboard/GetDefaultParameterNotification",
        type: "post",
        datatype: "json",
        data: {
            channelId: 0
        },
        success: function (data) {
            $('#hdnDefaultNotificationJSON').val(data)
        }
    });

    $.ajax({
        url: "/Dashboard/GetDefaultParameterCreateChannel",
        type: "post",
        datatype: "json",
        success: function (data) {
            $('#hdnGeneralMessageJSON').val(data)
        }
    });

    UploadAttachmentEvent();

    $('#cboNotesVesselSearch').on('select2:selecting', function (e) {
        IscboNotesVesselSearchSelectEventOccured = true;
    });

    $('#selectNCMParticipant, #cboNCMVesselSearch').on('select2:selecting', function (e) {
        IsNotificationDraftSelectEventOccured = true;
    });

    if ($(window).width() > MobileScreenSize) {
        SetDynamicTextAreaHeight();
    }

    $('.search-filter-n a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        //$('.search-filter-n #tabs1 a').on('click', function (e) {
        //e.preventDefault()
        $('.notessection').removeClass('activetab');
        //$(e.target).text();
        if ($(e.target).text() == 'All') {
            $("#allnotessection").addClass("activetab");
            $('#hdnAllStatusPageNumber').val(1);
            LoadAllTabNotes(false);
        }
        if ($(e.target).text() == 'Current') {
            $("#currentnotessection").addClass("activetab");
            $('#hdnCurrentStatusPageNumber').val(1);
            LoadCurrentTabNotes(false);
        }
        else if ($(e.target).text() == 'Completed') {
            $("#completednotessection").addClass("activetab");
            $('#hdnCompletedStatusPageNumber').val(1);
            LoadCompletedTabNotes(false);
        }
    });

    $('.add-note').hide();
    $("#plus-sign-note").click(function () {
        OpenAddNotes(null);
        $('.search-filter-n').hide();
    });

    $('.aReminderClose').click(function () {
        UpdateNotesTab();
        AddClassIfAbsent('.reminderAlert', 'd-none');
        $(".notesReminderAlertContainer").empty();
        UpdateDashboardReminderAlertTab();
    });

    $("#notes-filter").click(function () {
        $("#notesSearchText").toggleClass('d-none');
    });

    $("#btnCancelNote").click(function () {
        //clear add/edit section
        ShowNoteListSection();
        $('#hdnCurrentStatusPageNumber').val(1);
        UpdateNotesTab();
    });


    $(".add-notesclick").click(function () {
        OpenAddNotes(null);
        $('.notes-sidebar-open .theme-settings__inner').hide();
        $('.my-note').hide();
        $('.add-note').show();
        AddClassIfAbsent(".notes-sidebar-open", 'settings-open');
        if ($(".notes-sidebar-open").hasClass('settings-open')) {
            $('.addfixed').addClass('addfixedleft');
            $('.add-list').addClass('addfixedleft');
        }
        else {
            $('.addfixed').removeClass('addfixedleft');
            $('.add-list').removeClass('addfixedleft');
        }
    });

    $(".view-notesclick").click(function () {
        ShowNoteListSection();
        $('#hdnCurrentStatusPageNumber').val(1);
        UpdateNotesTab();
        AddClassIfAbsent(".notes-sidebar-open", 'settings-open');
        if ($(".notes-sidebar-open").hasClass('settings-open')) {
            $('.addfixed').addClass('addfixedleft');
            $('.add-list').addClass('addfixedleft');
        }
        else {
            $('.addfixed').removeClass('addfixedleft');
            $('.add-list').removeClass('addfixedleft');
        }
    });

    $("#btnSaveNote").click(function () {
        SaveNote(true);
    });

    $('.markNoteAsCompletedForEditMode input[type="checkbox"]').click(function () {
        let checkbox = $(this);
        if ($(checkbox).is(":checked")) {
            CompleteNoteFromEdit();
        } else {
            MoveToCurrentFromEdit();
        }
    });

    $("#aBaseGeneralNote").click(function () {
        $('.add-list').hide();
        OpenAddNotes(null);
    });

    $("#aBaseRecordLevelNote").click(function () {
        $('.add-list').hide();
        OpenAddNotes(null);
        RemoveClassIfPresent('#divAreaDropdown', 'd-none');
    });

    var today = new Date();

    $('.reminderDatePicker').daterangepicker({
        "singleDatePicker": true,
        "timePicker": true,
        "timePicker24Hour": true,
        "timePickerIncrement": 15,
        "autoApply": true,
        opens: "left",
        drops: "up",
        minDate: today,
        "locale": {
            "format": "MM/DD/YYYY HH:mm",
            "applyLabel": "Apply",
            "cancelLabel": "Cancel",
        }
    }, cb);

    $('.reminderDatePicker').on('apply.daterangepicker', function (ev, picker) {
        var currentDate = picker.startDate;
        var currentMinutePart = GetCurrentMinute(currentDate.minute());
        currentDate.minute(currentMinutePart);
        var ReminderDateTime = moment(currentDate).format("DD MMM YYYY HH:mm");
        $("#reminderDate").val(ReminderDateTime);
    });


    $('.reminderDatePicker').on('cancel.daterangepicker', function (ev, picker) {
        $("#reminderDate").val('');
        CancelReminder()
    });

    //Do not delete this code
    //Reminder related 
    //$.ajax({
    //    url: "/Notes/GetNotesReminder",
    //    type: "POST",
    //    dataType: "JSON",
    //    beforeSend: function (xhr) {
    //        xhr.setRequestHeader('Authorization', 'Bearer ' + GetCookie('ClientWebToken'));
    //    },
    //    success: function (data) {
    //        console.log(data);
    //        data.forEach(function (reminderAlert) {
    //            console.log('reminderAlert ', reminderAlert)
    //            SetReminderAlertFromDb(reminderAlert.meetingText, reminderAlert.timeOut)
    //        });
    //    }
    //});


    //let messageObject = {
    //    NoteId: 10332,
    //    NoteReminderId: 10140,
    //    NoteTitle: "new note test",
    //    NoteDescription: "new reminder",
    //    NoteStatus: 2
    //};
    //var newRow = CreateReminderAlertRow(messageObject);
    //$('.notesReminderAlertContainer').append(newRow);

    //setTimeout(function () {
    //    RemoveClassIfPresent('.reminderAlert', 'd-none');
    //    let messageObject1 = {
    //        NoteId: 10253,
    //        NoteReminderId: 10094,
    //        NoteTitle: null,
    //        NoteDescription: null,
    //        NoteStatus: 1
    //    };
    //    var newRow1 = CreateReminderAlertRow(messageObject1);
    //    $('.notesReminderAlertContainer').append(newRow1);
    //    console.log('event triggered')
    //}, 9000);


    GetNoticationUnreadChannelCountDashboard();
    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
        RemoveModelLoadingIndicator("#modalAddEditUserFleet .busy-indicator");
    };

    var key = CryptoJS.enc.Utf8.parse('8080808080808080')
    var iv = CryptoJS.enc.Utf8.parse('8080808080808080')
    let code = (function () {

        return {
            encryptMessage: function (messageToencrypt = '', secretkey = '') {
                var encryptedMessage = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(messageToencrypt), key, { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
                return encryptedMessage.toString();
            },
            decryptMessage: function (encryptedMessage = '', secretkey = '') {
                var decryptedBytes = CryptoJS.AES.decrypt(encryptedMessage, key, { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
                var decryptedMessage = decryptedBytes.toString(CryptoJS.enc.Utf8);
                return decryptedMessage;
            }
        }
    })();

    $(document).on('click', '.discussionListAnchorOnClick', function () {
        let messageDetailsJSONstr = $(this).data("messagejson");
        let messageDetailsJSON = JSON.parse(decodeURIComponent(messageDetailsJSONstr));
        NavigateToNotification(messageDetailsJSON);
    });

    $(document).on('click', '.discussionAnchorOnClick', function () {
        var messageDetailsJSON = $("#MessageDetailsJSON").val()
        NavigateToNotification(messageDetailsJSON);
    });

    $(document).on('click', '.discussionListAnchorOnClickVoyageList', function () {
        let messageDetailsJSONstr = $(this).data("messagejson");
        let messageDetailsJSON = JSON.parse(decodeURIComponent(messageDetailsJSONstr));
        NavigateToNotification(JSON.stringify(messageDetailsJSON));
    });


    connection = $.hubConnection();
    connection.logging = true;
    var contosoChatHubProxy = connection.createHubProxy('MessageHub');

    var ar = GetCookie('Roles').split(",");
    for (var i = ar.length; i--;) {
        ar[i] = ar[i].replace(/['"]/g, "");
    }
    connection.url = GetCookie('SignalRURL');
    //var connection = $.hubConnection('https://srv-glas925:9705/SignalRHubApplication/');
    //var contosoChatHubProxy = connection.createHubProxy('MessageHub');
    //contosoChatHubProxy.on('BroadcastTaskResult', (sender, messageObject) => {		
    //});
    contosoChatHubProxy.on('BroadcastReportResult', (sender, messageObject) => {
        if (messageObject.Success) {
            $.ajax({
                url: "/Permission/ReportResponse",
                type: "POST",
                dataType: "JSON",
                global: false,
                data: {
                    "response": messageObject
                },
                success: function (data) {
                    if (data.bytes != null && data.bytes != '') {
                        var array = base64ToArrayBuffer(data.bytes);
                        saveByteArray(data.filename, array, data.fileType);
                        ToastrAlert("success", messageObject.MessageContent);
                    } else {
                        ToastrAlert("validate", "File Not Found for \" " + data.filename + "\"");
                    }
                }
            });
        }
        else {
            ToastrAlert("error", messageObject.MessageContent.replace(/['"]+/g, ''));
        }
    });

    contosoChatHubProxy.on('BroadcastTaskResult', (sender, messageObject) => {
        $.ajax({
            url: "/Permission/TaskResponse",
            type: "POST",
            dataType: "JSON",
            global: false,
            data: {
                "response": messageObject
            },
            success: function (data) {
                if (data != null) {
                    if (data.isFileType) {
                        if (data.bytes != null && data.bytes != '') {
                            var array = base64ToArrayBuffer(data.bytes);
                            saveByteArray(data.filename, array, data.fileType);
                            ToastrAlert("success", messageObject.MessageContent);
                        }
                        else {
                            ToastrAlert("validate", "File Not Found for \" " + data.filename + "\"");
                        }
                    }
                    else {
                        if (data.isSuccess) {
                            ToastrAlert("success", data.message);
                        }
                        else {
                            ErrorLog(xhr, status, error);
                        }
                    }
                }
            }
        });
    });

    contosoChatHubProxy.on('BroadcastNotificationResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("BroadcastNotificationResult", messageObject)
            GetNoticationUnreadChannelCountDashboard();
            UpdateUnreadMessagesOnDashboard();
        }
    });

    contosoChatHubProxy.on('BroadcastCreateChannelResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("BroadcastCreateChannelResult", messageObject)
            //works in both mobile and web because the page is same for channel list
            GetNoticationUnreadChannelCountDashboard();
            UpdateUnreadMessagesOnDashboard();
        }
    });

    contosoChatHubProxy.on('BroadcastChannelOpenedResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("broadcastChannelOpenedResult", messageObject)
            GetNoticationUnreadChannelCountDashboard();
            UpdateUnreadMessagesOnDashboard();
        }
    });

    contosoChatHubProxy.on('broadcastRefreshUnreadCountResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("broadcastRefreshUnreadCountResult", messageObject)
            GetNoticationUnreadChannelCountDashboard();
        }
    });

    contosoChatHubProxy.on('broadcastMessageDeletedResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("broadcastMessageDeletedResult", messageObject)
            GetNoticationUnreadChannelCountDashboard();
            UpdateUnreadMessagesOnDashboard();
        }
    });

    contosoChatHubProxy.on('BroadcastNotesReminderResult', (sender, messageObject) => {
        if (messageObject != null && messageObject.MessageTarget != null) {
            console.log("BroadcastNotesReminderResult", messageObject);
            let userId = messageObject.MessageTarget.UserIds[0];
            var newRow = CreateReminderAlertRow(messageObject);
            $('.notesReminderAlertContainer').append(newRow);
            let loggedInUser = GetCookie('UserId');
            if (userId == loggedInUser) {
                RemoveClassIfPresent('.reminderAlert', 'd-none');
                $('.reminderAlert').css('position', 'fixed');
                $('.reminderAlert').fadeIn();
            }
        }
    });

    connection.start({ transport: ['webSockets', 'longPolling'] }).done(function () {
        console.log('Now connected, connection ID=' + connection.id);
        contosoChatHubProxy.invoke('RegisterUserEmailConnection', GetCookie('EmailId'), '5d62c6a512464f7f930f96e080dfcb23', ar);
        console.log('Invoke');
    }).fail(function (e) { console.error('Could not connect ' + e); });

    //mobile tabs
    $(".tab-1").click(function () {
        Tab_List();
    });
    $(".tab-2").click(function () {
        Tab_Overview();
    });

    //mobile filter search
    $("#search-mobile").click(function () {
        $(".search-dropdown-mobile").toggleClass('dropdown-show');
    });

    //mobile tabs dropdown
    $('#dropdowntabbutton').click(function () {
        //$('.mobile-tab .dropdown-menu').toggleClass('show');
        $(".mobile-dropdown-tab").toggleClass('dropdown-show');
    });


    var $lis = $('.mobile-tab.mobile-dropdown-tab li').not('.d-none');
    if ($lis.length > 3) {
        $("#dropdowntablist").addClass("tab-4-text");
    }
    else {
        $("#dropdowntablist").removeClass("tab-4-text");
    }

    var $lis1 = $('.mobile-tab.mobile-dropdown-tab li').not('.d-none');
    if ($lis1.length > 4) {
        $("#dropdowntablist").removeClass("tab-4-text");
        $("#dropdowntablist").addClass("tab-5-text");
    }
    else {
        $("#dropdowntablist").removeClass("tab-5-text");
    }

    $('.dropdown-toggle').dropdown();
    //$('#mobilebottomddropdown').click(function (e) {
    //	e.preventDefault();
    //	$(this).parent().addClass("show");
    //	$(this).attr("aria-expanded", "true");
    //	$(this).next().addClass("show");
    //	$('.mobile-tab .dropdown-menu').toggleClass('show');
    //});
    $(".mobile-tab .dropdown-menu a").click(function () {
        $(document.body).click();
    });
    $(".mobile-tab .dropdown-menu a").click(function () {
        if (!$(this).hasClass('modal-click')) {
            $(".dropdowntabtitle").html($(this).text());
        }
        $(".dropdowntabtitle").addClass('active');
        $(".mobile-dropdown-tab").removeClass('dropdown-show');
    });

    $('#dropdowntablist a').click(function (e) {
        var $this = $(this);
        if (!$this.hasClass('modal-click')) {
            $this.parent().siblings().removeClass('active').end().addClass('active');
        }
        e.preventDefault();
    });

    $(document).click(function () {
        if ($("#dropdowntablist").hasClass('show')) {
            $("#dropdowntablist").removeClass('show');
        }
    });


    $(".dropdown-tab-1").on("click", function () {
        $(".tab-box-1").show();
        $(".tab-box-2").hide();
        $(".tab-box-3").hide();
        $(".tab-box-4").hide();
        $(".tab-box-5").hide();
        $("#tabdivhide").show();
        $(window).scrollTop(0);
    });
    $(".dropdown-tab-2").on("click", function () {
        $(".tab-box-1").hide();
        $(".tab-box-2").show();
        $(".tab-box-3").hide();
        $(".tab-box-4").hide();
        $(".tab-box-5").hide();
        $("#tabdivhide").show();
        $(window).scrollTop(0);
    });
    $(".dropdown-tab-3").on("click", function () {
        $(".tab-box-1").hide();
        $(".tab-box-2").hide();
        $(".tab-box-3").show();
        $(".tab-box-4").hide();
        $(".tab-box-5").hide();
        $("#tabdivhide").show();
        $(window).scrollTop(0);
    });
    $(".dropdown-tab-4").on("click", function () {
        $(".tab-box-1").hide();
        $(".tab-box-2").hide();
        $(".tab-box-3").hide();
        $(".tab-box-4").show();
        $(".tab-box-5").hide();
        $("#tabdivhide").show();
        $(window).scrollTop(0);
    });
    $(".dropdown-tab-5").on("click", function () {
        $(".tab-box-1").hide();
        $(".tab-box-2").hide();
        $(".tab-box-3").hide();
        $(".tab-box-4").hide();
        $(".tab-box-5").show();
        $("#tabdivhide").show();
        $(window).scrollTop(0);
    });

    //search filter sidebar
    $(".search-filter-sidebar").click(function () {
        $(".search-sidebar-div").toggleClass("search-sidebar-open");
        if (($(window).width() < MobileScreenSize)) {
            $("body").addClass('overflow-hidden');
        }
    });

    $(".clear-btn").click(function () {
        $(".search-sidebar-div").removeClass("search-sidebar-open");
        if (($(window).width() < MobileScreenSize)) {
            $("body").removeClass('overflow-hidden');
        }
    });
    $(".search-btn").click(function () {
        $(".search-sidebar-div").removeClass("search-sidebar-open");
        if (($(window).width() < MobileScreenSize)) {
            $("body").removeClass('overflow-hidden');
        }
    });
    $(".close-search-panel").click(function () {
        $(".search-sidebar-div").removeClass("search-sidebar-open");
        if (($(window).width() < MobileScreenSize)) {
            $("body").removeClass('overflow-hidden');
        }
    });



    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        IsMobile = true;
    } else {
        IsMobile = false;
    }

    //hide datepicker for mobile on tabs 
    $(".mobile-tab").on("click", function () {
        Mobile_Tab();
    });


    //Sidebar back
    //$('.back').click(function () {
    //	//window.history.back();		
    //	//window.location.replace(document.referrer);
    //	window.close();
    //});

    //vessel search
    $("#cbFromVesselSearch").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search vessel",
        minimumInputLength: 0,
        ajax: {
            url: '/Dashboard/GetVesselLookup',
            dataType: 'json'
        },
        templateResult: formatResult,
        templateSelection: formatRepoSelection
    });

    $('#cbFromVesselSearch').on('select2:open', function (e) {
        if (!IsMobile) {
            if (!headerIsAppend) {
                var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <tbody>\
		              </tbody>\
		              </table>';
                $('.select2-search').append(html);
                $('.select2-results').addClass('stock');
                headerIsAppend = true;
            }
        }
    });

    function formatResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (IsMobile) {
            if (result != undefined) {
                $result = $('<div class="row select2-row">' +
                    //'<div class="col-sm-4"><b>Coy Id : </b>' + result.accountingCompanyId + '</div>' +
                    '<div class="col-sm-8">' + result.vesselName + '</div>'
                    + '</div>');
            }
        }
        else {
            if (result != undefined) {
                $result = $('<table class="table table-bordered p-0 m-0">\
								 <tbody>\
									<tr class="m-0">\
										<td class="m-0" width="45%">' + result.vesselName + '</td>\
									</tr>\
								</tbody>\
							</table>');
            }
        }
        return $result;
    }

    function formatRepoSelection(repo) {
        let vesselURL = repo.vesselURL;
        let id = repo.id;
        if (id != "") {
            let pathname = window.location.pathname
            let refererWithoutParameterRequest = pathname.split("?")[0];
            let referers = refererWithoutParameterRequest.split("/");
            let lastReferer = '';
            let upper = referers.map(element => {
                return element.toUpperCase();
            });
            if (referers.length > 0) {
                lastReferer = referers[referers.length - 1];
                if (lastReferer === '') {
                    lastReferer = referers[referers.length - 2];
                }
            }
            if (lastReferer.includes("Dashboard") || upper.includes("APPROVAL")) {

                let vesselId = vesselURL;
                var searchParams = new URLSearchParams(window.location.search);
                var obj = { "fleetId": "", "menuType": "", "vesselId": vesselId, "title": repo.text.split('-')[0], "isFleetSelection": true }

                $.ajax({
                    url: "/Dashboard/GetEncryptedFleetRequest",
                    type: "post",
                    datatype: "json",
                    data: obj,
                    success: function (data) {
                        if (searchParams.has("fleetRequest")) {
                            searchParams.set("fleetRequest", data);
                        }
                        else if (searchParams.has("FleetRequest")) {
                            searchParams.set("FleetRequest", data);
                        }
                        else {
                            searchParams.set("fleetRequest", data);
                        }

                        window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + searchParams.toString();
                    }
                });

            }
            else {
                var searchParams = new URLSearchParams(window.location.search);
                if (searchParams.has("vesselId")) {
                    searchParams.set("vesselId", vesselURL);
                }
                else if (searchParams.has("VesselId")) {
                    searchParams.set("VesselId", vesselURL);
                }
                else {
                    searchParams.set("VesselId", vesselURL);
                }

                if (!searchParams.has("IsVesselChanged")) {
                    searchParams.set("IsVesselChanged", true);
                }
                if (searchParams.has("fleetRequest")) {
                    searchParams.delete("fleetRequest");
                }
                else if (searchParams.has("FleetRequest")) {
                    searchParams.delete("FleetRequest");
                }
                else {
                    searchParams.delete("fleetRequest");
                }

                window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + searchParams.toString();
            }
        }
        return repo.text;
    }

    //vessel search selection in My notes section

    LoadNotesVesselSearch(IsMobile);

    $(".fixed-header").addClass("closed-sidebar");

    $(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));

    $(window).resize(function () {
        $(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));
    });


    $('.btnInspectionPlanning').click(function () {
        OpenModalInspectionPlanning(true, true, true, false);
    });

    $('#btnInspectionPlanningDue').click(function () {
        OpenModalInspectionPlanning(false, false, true, false);
    });

    $('#btnInspectionPlanningOverDueDue').click(function () {
        OpenModalInspectionPlanning(true, false, false, false);
    });

    $('#btnInspectionPlanningNeverDone').click(function () {
        OpenModalInspectionPlanning(false, true, false, false);
    });

    $("#selInspectionPlanningDue").change(function () {
        OnChangeInspectionPlanningDue();
    });

    $("#chkOverdue").change(function () {
        OnChangeInspectionChkOverDue();
    });

    $("#chkNeverDone").change(function () {
        OnChangeInspectionchkNeverDone();
    });

    $('#btnExportInspectionPlanning').click(() => {
        var searchValue = dtInspectionPlanningDetails.search();
        dtInspectionPlanningDetails.search("").draw();

        $('#dtInspectionPlanningDetails.cardview thead').addClass("export-grid-show");
        $('#dtInspectionPlanningDetails').DataTable().buttons(0, 2).trigger();
        $('#dtInspectionPlanningDetails.cardview thead').removeClass("export-grid-show");

        dtInspectionPlanningDetails.search(searchValue).draw();
    });




    $('#btnOpenManageUserFleet').click(function () {
        OpenManageUserFleetModal();
    });

    $('#btnAddUserFleet').click(function () {
        OpenAddUserFleetModal();
    });

    $('#example-select-all').on('click', function () {
        var rows = gridAvailableVesselList.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    $('#dtAvailableVesselList tbody').on('change', 'input[type="checkbox"]', function () {

        if (!this.checked) {
            var el = $('#example-select-all').get(0);
            if (el && el.checked && ('indeterminate' in el)) {
                el.indeterminate = true;
            }
        }
    });

    $('#btnSaveUserFleetDetails').click(function () {
        if (!($("#btnSaveUserFleetDetails").hasClass("disabled"))) {
            var selectedVesIds = [];
            var rows_selected = gridMappedVesselList.column(0).data();
            var fleetId = $("#hdnFleetId").val();
            var fleetName = $("#fleetName").val();

            if (fleetName.trim() == "") {
                $("#fleetNameReqValidation").removeClass("d-none");
            }
            else if (rows_selected.length > 0) {
                $.each(rows_selected, function (index, rowId) {
                    selectedVesIds.push(rowId);
                });

                SaveAndUpdateFleetDetails(fleetId, selectedVesIds);
            }
            else {
                ToastrAlert("validate", validationMesssage.MappedVesselValidationMesssage);
            }
        }
    });

    $('#btnAddtoMapped, #btnAddtoMappedmobile').click(function () {
        if (($(window).width() < MobileScreenSize)) {
            $("#addvesselavaiablemodal").modal('hide');
            $('#addbuttonmobile').show();
            $('.mobilemappedremovebox').hide();
        }
        var aTrs = gridAvailableVesselList.rows('.row_selected').data();
        for (var i = 0; i < aTrs.length; i++) {
            gridMappedVesselList.row.add(aTrs[i]);
            gridAvailableVesselList.row('.row_selected').remove();
        }
        gridMappedVesselList.draw();
        gridAvailableVesselList.draw();

        $('#avaiablevesselrowcount, #avaiablevesselrowcountmobile').text(gridAvailableVesselList.data().count());
        $('#mappedvesselrowcount').text(gridMappedVesselList.data().count());

        fn_EnabledDisabledFleetGridButton(gridAvailableVesselList, 'btnAddtoMapped');
        $("#btnSaveUserFleetDetails").removeClass('disabled').removeAttr('aria-disabled');
        ResetMasterCheckBox();

    });

    $('#addbuttonmobile').click(function () {
        if (($(window).width() < MobileScreenSize)) {
            $("#addvesselavaiablemodal").modal('show');
        }
    });


    $('#btnRemoveFromMapped').click(function () {
        var aTrs = gridMappedVesselList.rows('.row_selected').data();
        for (var i = 0; i < aTrs.length; i++) {
            gridAvailableVesselList.row.add(aTrs[i]);
            gridMappedVesselList.row('.row_selected').remove();
        }
        gridMappedVesselList.draw();
        gridAvailableVesselList.draw();

        $('#avaiablevesselrowcount, #avaiablevesselrowcountmobile').text(gridAvailableVesselList.data().count());
        $('#mappedvesselrowcount').text(gridMappedVesselList.data().count());

        fn_EnabledDisabledFleetGridButton(gridMappedVesselList, 'btnRemoveFromMapped');
        $("#btnSaveUserFleetDetails").removeClass('disabled').removeAttr('aria-disabled');
        ResetMasterCheckBox();
    });

    $("#navigationTree").fancytree({
        checkbox: false,
        selectMode: 3,
        icon: false,

        source: $.ajax({
            url: "/Dashboard/GetNavigationTreeTopLevel",
            dataType: "json",
            data: { treeType: "VesselTree", AllowFleetSelection: $("#IsAllowSelectFleetParent").val() }
        }),

        lazyLoad: function (event, data) {
            console.log(event);
            console.log(data);
            data.result =
                $.ajax({
                    url: "/Dashboard/GetNavigationTreeLevel",
                    dataType: "json",
                    data: {
                        key: data.node.key,
                        userMenuItemType: data.node.data.userMenuItemType,
                        parentUserMenuTypeShortCode: data.node.data.userMenuTypeShortCode,
                        AllowFleetSelection: $("#IsAllowSelectFleetParent").val()
                    }
                });
        },

        activate: function (event, data) {
            var fleetId = "";
            var menuType = "F";
            var vesselId = '';
            var title = '';
            if (data.node.data.isVessel == true && data.node.data.allowFleetSelection == false) {
                var searchParams = new URLSearchParams(window.location.search);
                if (searchParams.has("vesselId")) {
                    searchParams.set("vesselId", data.node.key);
                }
                else if (searchParams.has("VesselId")) {
                    searchParams.set("VesselId", data.node.key);
                }
                else {
                    searchParams.set("VesselId", data.node.key);
                }

                if (!searchParams.has("IsVesselChanged")) {
                    searchParams.set("IsVesselChanged", true);
                }

                window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + searchParams.toString();
            }
            else if (data.node.data.allowFleetSelection == true) {

                fleetId = data.node.data.identifier;
                menuType = data.node.data.userMenuTypeShortCode;

                if (data.node.data.isVessel == true) {
                    vesselId = data.node.key;
                    title = data.node.title.split('-')[0];
                }
                else {
                    title = data.node.title;
                }

                var searchParams = new URLSearchParams(window.location.search);
                var obj = { "fleetId": fleetId, "menuType": menuType, "vesselId": vesselId, "title": title, "isFleetSelection": true }

                $.ajax({
                    url: "/Dashboard/GetEncryptedFleetRequest",
                    type: "post",
                    datatype: "json",
                    data: obj,
                    success: function (data) {
                        if (searchParams.has("fleetRequest")) {
                            searchParams.set("fleetRequest", data);
                        }
                        else if (searchParams.has("FleetRequest")) {
                            searchParams.set("FleetRequest", data);
                        }
                        else {
                            searchParams.set("fleetRequest", data);
                        }

                        window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + searchParams.toString();
                    }
                });
            }
        }
    });

    // Sample button
    $("#button1").click(function () {
        console.log('button1 click');
        var tree = $.ui.fancytree.getTree(),
            node = tree.findFirst(function (n) {
                return n.title === "The Hobbit";
            });

        node.toggleSelected();
    });

    //this for mobile
    $('#dtMappedVesselList').on('click', '.select-favfleet-cross', function () {
        var thisRow = $(this).closest("tr"); //.addClass('row_selected');
        var aTrs = gridMappedVesselList.rows(thisRow).data();

        gridAvailableVesselList.row.add(aTrs[0]).draw();
        gridMappedVesselList.row(thisRow).remove().draw();

        $('#avaiablevesselrowcount, #avaiablevesselrowcountmobile').text(gridAvailableVesselList.data().count());
        $('#mappedvesselrowcount').text(gridMappedVesselList.data().count());

        fn_EnabledDisabledFleetGridButton(gridMappedVesselList, 'btnRemoveFromMapped');
        $("#btnSaveUserFleetDetails").removeClass('disabled').removeAttr('aria-disabled');

    });


    $('#dtMappedVesselList').on('change', '.select-favfleet-checkbox', function () {
        if ($(this).prop('checked') == false) {
            $(this).closest("tr").removeClass('row_selected');
        }
        else {
            $(this).closest("tr").addClass('row_selected');
        }
        fn_EnabledDisabledFleetGridButton(gridMappedVesselList, 'btnRemoveFromMapped');
        fn_HideShowFleetGridMobileButton(gridMappedVesselList, '.mobilemappedremovebox');
    });

    $('#chkIsActive').change(function () {
        var fleetId = $("#hdnFleetId").val();
        if (chkIsActive.checked == true && $("#hdnFleetId").val() != "") {
            fnCanCreateFleet(fleetId);
        }
        $("#btnSaveUserFleetDetails").removeClass('disabled').removeAttr('aria-disabled');
    });

    $('#modalAddEditUserFleet').on('shown.bs.modal', function () {
        if (($(window).width() < MobileScreenSize)) {
            $(".desktopavaiablevessel").removeAttr("id");
        }
    });

    $('#addvesselavaiablemodal').on('shown.bs.modal', function () {
        if (($(window).width() < MobileScreenSize)) {
            gridAvailableVesselList.columns().checkboxes.deselect(true);
            //$("#btnAddtoMappedmobile").hide();
            if (!$.fn.DataTable.isDataTable('#dtAvailableVesselList')) {
                let fleetid = $('#hdnFleetId').val();
                if (IsNullOrEmptyOrUndefinedLooseTyped(fleetid)) {
                    LoadAvailableVesselList();
                }
                else {
                    LoadAvailableVesselList(fleetid);
                }
            }

        }
    });

    if (($(window).width() < MobileScreenSize)) {
        $('#addvesselavaiablemodal').on('hidden.bs.modal', function (e) {
            if ($('#modalAddEditUserFleet').hasClass('show')) {
                $('body').addClass('modal-open');
            }
        });
        $('#modalAddEditUserFleet').on('hidden.bs.modal', function (e) {
            if ($('#modalManageUserFleet').hasClass('show')) {
                $('body').addClass('modal-open');
            }
        });
        $('#modalManageUserFleet').on('hidden.bs.modal', function (e) {
            if ($('#modalLabelSendEmail').hasClass('show')) {
                $('body').addClass('modal-open');
            }
        });
    }

    $('#modalAddEditUserFleet').on('hidden.bs.modal', function () {
        $("#fleetNameReqValidation").addClass("d-none");
        $("#UserFleetHeader").text('Add Favourite Fleet');
        $('#fleetName').val('');
        chkIsActive.checked = true;
        LoadUserFleet();
        $("#btnSaveUserFleetDetails").addClass('disabled').attr('aria-disabled');
    });

    $("#fleetName").keyup(function () {
        $("#btnSaveUserFleetDetails").removeClass('disabled').removeAttr('aria-disabled');
    });

    $(".btnTaskNotifications").click(function () {
        GetTaskNotifications();
    });

    $(".btnToggleDemoMode").click(function () {
        ToggleDemoMode();
    });

    $("#btnRefreshTaskNotifications").click(function () {
        GetTaskNotifications();
    });

    $("#preference input:checkbox").on('change', function () {
        $("#btnSaveUserPreferences").prop("disabled", false);
    });

    $("#aBaseGeneralMessage").click(function () {

        $.ajax({
            url: "/Dashboard/GetEncodedBase64String",
            type: "post",
            datatype: "json",
            data: {
                "request": $('#hdnGeneralMessageJSON').val()
            },
            success: function (data) {
                $('#createRequest').val(data);
                $("#notificationcreatechannelform").submit()
                OpenNewChannelModal();
            }
        });
    });

    $("#aBaseRecordLevelMessage").click(function () {
        $.ajax({
            url: "/Dashboard/GetEncodedBase64String",
            type: "post",
            datatype: "json",
            data: {
                "request": $('#hdnmessageDetailsJSON').val()
            },
            success: function (data) {
                $('#createRequest').val(data);
                $("#notificationcreatechannelform").submit()
                OpenNewChannelModal();
            }
        });
    });


    $(".modal-draggable .modal-drag-holder").on("mousedown", function (mousedownEvt) {
        var $draggable = $(this);
        var x = mousedownEvt.pageX - $draggable.offset().left,
            y = mousedownEvt.pageY - $draggable.offset().top;
        $("body").on("mousemove.draggable", function (mousemoveEvt) {
            $draggable.closest(".modal-dialog").offset({
                "left": mousemoveEvt.pageX - x,
                "top": mousemoveEvt.pageY - y
            });
        });
        $("body").one("mouseup", function () {
            IsNotificationDraftSelectEventOccured = true;
            $("body").off("mousemove.draggable");
        });
        $draggable.closest(".modal").one("bs.modal.hide", function () {
            $("body").off("mousemove.draggable");
        });
    });

    $("#btnNCMCreateChannel").click(function () {
        CreateChannel(false);
    });

    $("#btnNCMSaveAsDraft").click(function () {
        CreateChannel(true);
    });


    $("#btnNCMCancel").click(function () {
        $("#modalDraftChannelBody").data("createdraft", "0");
        $("#modalCreateNewChannel").modal('hide');
    });

    $(".btn-open-options").click(function () {
        $("#hdnNotesMessageDetails").val('');
        UpdateNotesTab();
        $(".notes-sidebar-open").toggleClass("settings-open");
    });

    if ($(window).width() < 767) {
        $(".btn-open-options").click(function () {
            if ($(".notes-sidebar-open").hasClass('settings-open')) {
                $('body').addClass('overflow-hidden');
            }
            else {
                $('body').removeClass('overflow-hidden');
            }
        });
    }



    if ($(window).width() > 767) {
        $(".btn-open-options").click(function () {
            if ($(".notes-sidebar-open").hasClass('settings-open')) {
                $('.addfixed').addClass('addfixedleft');
                $('.add-list').addClass('addfixedleft');
            }
            else {
                $('.addfixed').removeClass('addfixedleft');
                $('.add-list').removeClass('addfixedleft');
            }
        });
        $(".close-notes-panel").click(function () {
            if ($(".notes-sidebar-open").hasClass('settings-open')) {
                $('.addfixed').removeClass('addfixedleft');
                $('.add-list').removeClass('addfixedleft');
            }
        });
    }


    //if ($(".notes-sidebar-open").hasClass('settings-open')) {
    //    $('.addfixed').addClass('addfixedleft');
    //}
    //else {
    //    $('.addfixed').removeClass('addfixedleft');
    //}

    $(".close-notes-panel").click(function () {
        if ($(".notes-sidebar-open").hasClass('settings-open') && $('.add-note:visible').is(':visible')) {
            SaveNote(false);
        }
        $(".notes-sidebar-open").removeClass("settings-open");
        $('body').removeClass('overflow-hidden');
    });

    $('#divCurrentNotesSection').on('scroll', function () {
        let moreThanHalfHeight = ($(this)[0].scrollHeight) - (($(this)[0].scrollHeight) / 3);
        if (($(this).scrollTop() + $(this).innerHeight()) >= moreThanHalfHeight) {
            let PageNumber = parseInt($('#hdnCurrentStatusPageNumber').val());
            PageNumber++;
            $('#hdnCurrentStatusPageNumber').val(PageNumber);
            if ($('#hdnCurrentHasNextPage').val() == "true") {
                LoadCurrentTabNotes(true);
            }
            else {
                $('#hdnCurrentStatusPageNumber').val(1);
            }
        }
    });

    $('#divCompleteNotesSection').on('scroll', function () {
        let moreThanHalfHeight = ($(this)[0].scrollHeight) - (($(this)[0].scrollHeight) / 3);
        if (($(this).scrollTop() + $(this).innerHeight()) >= moreThanHalfHeight) {
            let PageNumber = parseInt($('#hdnCompletedStatusPageNumber').val());
            PageNumber++;
            $('#hdnCompletedStatusPageNumber').val(PageNumber);
            if ($('#hdnCompletedHasNextPage').val() == "true") {
                LoadCompletedTabNotes(true);
            }
            else {
                $('#hdnCompletedStatusPageNumber').val(1);
            }
        }
    });

    $('#divAllNotesSection').on('scroll', function () {
        let moreThanHalfHeight = ($(this)[0].scrollHeight) - (($(this)[0].scrollHeight) / 3);
        if (($(this).scrollTop() + $(this).innerHeight()) >= moreThanHalfHeight) {
            let PageNumber = parseInt($('#hdnAllStatusPageNumber').val());
            PageNumber++;
            $('#hdnAllStatusPageNumber').val(PageNumber);
            if ($('#hdnAllHasNextPage').val() == "true") {
                LoadAllTabNotes(true);
            }
            else {
                $('#hdnAllStatusPageNumber').val(1);
            }
        }
    });

    $(".aBaseNotification").click(function () {
        //sessionStorage.removeItem(NotificationPageKey);
        //sessionStorage.removeItem(NotificationChatPageKey);
        //sessionStorage.removeItem(NotificationMobileChatDetailKey);
        //sessionStorage.removeItem(NotificationMobileDiscussionKey);
        //sessionStorage.removeItem(NotificationMobileInfoKey);
        NavigateToNotification($('#hdnDefaultNotificationJSON').val());
    });

    $("#unreadMessageListViewMessageBtn").on('click', function () {
        //sessionStorage.removeItem(NotificationPageKey);
        //sessionStorage.removeItem(NotificationChatPageKey);
        //sessionStorage.removeItem(NotificationMobileChatDetailKey);
        //sessionStorage.removeItem(NotificationMobileDiscussionKey);
        //sessionStorage.removeItem(NotificationMobileInfoKey);
        NavigateToNotification($('#hdnDefaultNotificationJSON').val());
    });

    $(document).on('click', '.messageChannelOnClick', function () {
        let channelId = $(this).data('chid');
        let isDraft = $('#hdnisdraft_' + channelId).val();
        var obj = null;
        $.ajax({
            url: "/Dashboard/GetDefaultParameterNotification",
            type: "post",
            datatype: "json",
            success: function (data) {
                NavigateToNotificationDashboard(data, channelId, isDraft);
            }
        });

    });

    SetVesselNameToHeaderDD();

    if (($(window).width() < TabScreenSize)) {
        $(document).click(function () {
            $(".app-header__content").removeClass('header-mobile-open');
        });
    }
    if (($(window).width() < TabScreenSize)) {
        $(document).click(function () {
            $(".fixed-sidebar").removeClass('sidebar-mobile-open');
            $(".mobile-toggle-nav").removeClass('is-active');
        });
    }
    if (($(window).width() < TabScreenSize)) {
        $(".mobile-toggle-header-nav").click(function () {
            $(".fixed-sidebar").removeClass('sidebar-mobile-open');
            $(".mobile-toggle-nav").removeClass('is-active');
        });
    }

    if (($(window).width() < TabScreenSize)) {
        $(".mobile-toggle-nav").click(function () {
            $(".app-header__content").removeClass('header-mobile-open');
        });
    }

});


$(window).on('resize', function () {
    if (screen.width < 767) {
        var header = $('.app-header').outerWidth();
        var menu = $('.menumobile').outerWidth();
        var home = $('.app-header__mobile-menu .header-dots').outerWidth();
        var vchat = $('.header-dots .aBaseNotification').width();
        var more = $('.mobile-toggle-header-nav').outerWidth();
        $(".vesseldropdownmobile").css({
            "width": header - menu - home - vchat - more - 30
        });
    }
});

function fn_dashboardVesselDetails(_this) {

    dashboardVesselDetails.VesselId = $(_this).data('vesselid');
    dashboardVesselDetails.VesselName = decodeURIComponent($(_this).data('vesselname'));

    OpenModalInspectionPlanning(true, false, false, false);
}

function fn_EnabledDisabledFleetGridButton(dtTableList, selector) {
    var rows_selected = dtTableList.column(0).checkboxes.selected();

    if (rows_selected.length > 0) {
        $("#btnAddtoMappedmobile").removeClass('disabled').removeAttr('aria-disabled');
        $("#" + selector).removeClass('disabled').removeAttr('aria-disabled');
    }
    else {
        $("#btnAddtoMappedmobile").addClass('disabled').attr('aria-disabled');
        $("#" + selector).addClass('disabled').attr('aria-disabled');
    }
}

function fn_HideShowFleetGridMobileButton(dtTableList, selector) {
    var rows_selected = dtTableList.column(0).checkboxes.selected();

    if (rows_selected.length > 0) {
        $(selector).show();
        $('#addbuttonmobile').hide();
    }
    else {
        $(selector).hide();
        $('#addbuttonmobile').show();
    }
}

//$(window).scroll(function () {
//	if ($(this).scrollTop() > 0) {
//		$('.scrollup').fadeIn();
//	} else {
//		$('.scrollup').fadeOut();
//	}
//});

//$('.scrollup').click(function () {
//	$("html, body").animate({
//		scrollTop: 0
//	}, 600);
//	return false;
//});

$('#addlistcircle').click(function () {
    $('.add-list').show();
});
$('#closed').click(function () {
    $('.add-list').hide();
});

function Tab_Overview() {
    $(".tab-box-1").hide();
    $(".tab-box-2").show();
    $(".tab-1").removeClass("active");
    $(".tab-2").addClass("active");
    $(window).scrollTop(0);
}

function Tab_List() {
    $(".tab-box-1").show();
    $(".tab-box-2").hide();
    $(".tab-2").removeClass("active");
    $(".tab-1").addClass("active");
    $(window).scrollTop(0);
}

function Mobile_Tab() {
    if ($(".tab-1").hasClass('active')) {
        $('#divDatePickermobilehide').removeClass('d-inline-block');
        $('#divDatePickermobilehide').addClass('d-none');
        $('#divfilterhide').removeClass('d-inline-block');
        $('#divfilterhide').addClass('d-none');
        $('#divlegendhide').removeClass('d-inline-block');
        $('#divlegendhide').addClass('d-none');

        $("#search-mobile").removeClass('search-filter-top');
    }
    else {
        $('#divDatePickermobilehide').removeClass('d-none');
        $('#divDatePickermobilehide').addClass('d-inline-block');
        $('#divfilterhide').removeClass('d-none');
        $('#divfilterhide').addClass('d-inline-block');
        $('#divlegendhide').removeClass('d-none');
        $('#divlegendhide').addClass('d-inline-block');

        $("#search-mobile").addClass('search-filter-top');
    }
}

function OpenManageUserFleetModal() {
    //load user fleet here
    LoadUserFleet();
}

function OpenAddUserFleetModal() {
    $("#UserFleetHeader").text('Add Favourite Fleet');
    $('#fleetName').val('');
    $("#fleetNameReqValidation").addClass('d-none');
    chkIsActive.checked = true;
    LoadAvailableVesselList();
    LoadMappedVesselList();
}

function LoadAvailableVesselList(fleetId) {
    AddModelLoadingIndicator("#loaderAvailableVessel");

    $('#dtAvailableVesselList').DataTable().destroy();
    var masterCheckBox = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
    masterCheckBox += '<input type = "checkbox" class="available-master-checkbox custom-control-input" id="AvailableMasterCheckbox">';
    masterCheckBox += '<label class="custom-control-label d-block" for="AvailableMasterCheckbox"></label></div >';
    gridAvailableVesselList = $('#dtAvailableVesselList').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-12 offset-lg-0 col-xl-12 offset-xl-0 dt-infomation"i><"col-12 col-md-5 mt-0 mt-md-2"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": false,
        "scrollY": true,
        "scrollCollapse": true,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No Available Vessels.",
            "info": "Showing _MAX_ entries",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        'columnDefs': [
            {
                'orderable': false,
                'targets': 0,
                'visible': true,
                'render': function (data, type, row, meta) {
                    var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
                    uielement += '<input type = "checkbox" class="select-favfleet-checkbox custom-control-input dt-checkboxes" id= "' + row.rowId + '">';
                    uielement += '<label class="custom-control-label d-block" for= "' + row.rowId + '"></label></div >';
                    data = uielement;
                    return data;
                },
                'createdCell': function (td, cellData, rowData, row, col) {
                    if (rowData.isVesselMapped == true) {
                        //this.api().cell(td).checkboxes.selected();
                        this.api().cell(td).checkboxes.select(true)
                    }
                },
                'checkboxes': {
                    'selectRow': true,
                    'selectAllRender': masterCheckBox
                }
            }
        ],
        'select': {
            'style': 'multi'
        },
        "ajax": {
            "url": "/Dashboard/GetUserManagementAvailableVesselList",
            "type": "POST",
            "data": { "fleetId": fleetId },
        },
        "columns": [
            {
                className: "mobile-popover-attachments tdblock data-icon-align checkbox-design-table",
                width: "20px",
                'orderable': false,
                'searchable': false,
                'data': "vesselManagementId",
                //'render': function (data, type, full, meta) {
                //	return '<div class="custom-checkbox custom-control custom-control-inline mr-0"><input type = "checkbox" name="VesselId[]" value="' + $('<div/>').text(data).html() + '" class="select-checkbox custom-control-input" id = "CLKA00000001" ><label class="custom-control-label d-block" for="CLKA00000001"></label></div>';
                //}
            },
            {
                className: "data-text-align width-87",
                data: "vesselName",
                width: "270px",
                name: "VesselName",
                render: function (data, type, full, meta) {
                    return full.vesselName;
                }
            }
        ],
        "initComplete": function (settings) {
            $('#avaiablevesselrowcount').text(this.api().data().length);
            $('#avaiablevesselrowcountmobile').text(this.api().data().length);
            RemoveModelLoadingIndicator("#loaderAvailableVessel");

            if (($(window).width() > 767)) {
                var fleetwindow2 = $(window).height();
                var fleetheader2 = $('.fleetscrolladduser .modal-header').outerHeight();
                $(".fleetscrolladduser .dataTables_scrollBody").css({
                    "height": fleetwindow2 - fleetheader2 - 375
                });
                $('.fleetscrolladduser .dataTables_scrollBody').addClass('scroller p-0');
            }
        }
    });
    $('#dtAvailableVesselList').on('change', '.select-favfleet-checkbox', function () {
        if ($(this).prop('checked') == false) {
            $(this).closest("tr").removeClass('row_selected');
        }
        else {
            // oTableSource.$('tr.row_selected').removeClass('row_selected');
            $(this).closest("tr").addClass('row_selected');
        }
        fn_EnabledDisabledFleetGridButton(gridAvailableVesselList, 'btnAddtoMapped');
    });
}

function LoadMappedVesselList(fleetId) {
    AddModelLoadingIndicator("#loaderMappedVessel");

    $('#dtMappedVesselList').DataTable().destroy();
    var masterCheckBox = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
    masterCheckBox += '<input type = "checkbox" class="mapped-master-checkbox custom-control-input" id="mappedMasterCheckbox">';
    masterCheckBox += '<label class="custom-control-label d-block" for="mappedMasterCheckbox"></label></div >';

    gridMappedVesselList = $('#dtMappedVesselList').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-12 offset-lg-0 col-xl-12 offset-xl-0 dt-infomation"i><"col-12 col-md-5 mt-0 mt-md-2"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": false,
        "scrollY": true,
        "scrollCollapse": true,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No Mapped Vessels.",
            "info": "Showing _MAX_ entries",
            //"infoFiltered": "<div>(filtered from _MAX_ total entries)<div>",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        'columnDefs': [
            {
                'orderable': false,
                'targets': 0,
                'visible': true,
                'render': function (data, type, row, meta) {
                    var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0 hide-mobile">';
                    uielement += '<input type = "checkbox" class="select-favfleet-checkbox custom-control-input dt-checkboxes" id= "' + row.rowId + '">';
                    uielement += '<label class="custom-control-label d-block" for= "' + row.rowId + '"></label></div >';
                    data = uielement;
                    return data;
                },
                'createdCell': function (td, cellData, rowData, row, col) {
                    if (rowData.isVesselMapped == true) {
                        //this.api().cell(td).checkboxes.selected();
                        this.api().cell(td).checkboxes.select(true)
                    }
                },
                'checkboxes': {
                    'selectRow': true,
                    'selectAllRender': masterCheckBox
                }
            }
            //,
            //{
            //	'orderable': false,
            //	'targets': 1,
            //}
        ],
        'select': {
            'style': 'multi'
        },
        "ajax": {
            "url": "/Dashboard/GetMappedVesselList",
            "type": "POST",
            "data": { "fleetId": fleetId },
        },
        "columns": [
            {
                className: "mobile-popover-attachments tdblock data-icon-align checkbox-design-table hide-mobile",
                width: "20px",
                'orderable': false,
                'searchable': false,
                'data': "vesselManagementId",
            },
            {
                className: "d-inline-block d-md-none selectfleetcross",
                width: "20px",
                'orderable': false,
                'searchable': false,
                'data': "vesselManagementId",
                render: function (data, type, full, meta) {
                    return '<img src="/images/fleetcross.svg" class="select-favfleet-cross"/>';
                }
            },
            {
                className: "data-text-align width-87",
                data: "vesselName",
                width: "270px",
                name: "VesselName",
                width: "444px",
                render: function (data, type, full, meta) {
                    return full.vesselName;
                }
            }
        ],
        "initComplete": function (settings) {
            $('#mappedvesselrowcount').text(this.api().data().length);
            RemoveModelLoadingIndicator("#loaderMappedVessel");

            if (($(window).width() > 767)) {
                var fleetwindow2 = $(window).height();
                var fleetheader2 = $('.fleetscrolladduser .modal-header').outerHeight();
                $(".fleetscrolladduser .dataTables_scrollBody").css({
                    "height": fleetwindow2 - fleetheader2 - 375
                });
                $('.fleetscrolladduser .dataTables_scrollBody').addClass('scroller p-0');
            }
        }
    });
}

function LoadUserFleet() {

    AddModelLoadingIndicator("#modalManageUserFleet .busy-indicator");
    if (($(window).width() < MobileScreenSize)) {
        $("#btnAddUserFleet").hide();
    }

    $('#dtFleetList').DataTable().destroy();

    gridUserFleetList = $('#dtFleetList').DataTable({
        "dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-10 offset-lg-2 col-xl-7 offset-xl-2 dt-infomation"i><"col-12 col-md-5 "f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": false,
        "scrollY": "auto",
        "scrollCollapse": true,
        "autoWidth": false,
        "paging": false,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No Favourites.",
            "info": "Showing _MAX_ entries",
            "search": "_INPUT_",
            "searchPlaceholder": "Search",
        },
        "ajax": {
            "url": "/Dashboard/GetFleetList",
            "type": "POST"
        },
        "columns": [
            {
                className: "d-none d-sm-table-cell data-icon-align",
                width: "20px",
                'orderable': false,
                'searchable': false,
                'render': function (data, type, full, meta) {
                    return '<button class="btn btn-icon fsize-1 px-1 py-0"> <a class="text-black" href="javascript:void(0);" > <img src="/images/edit.svg" class="addEditFleet cursor-pointer" title="Edit" data-placement="bottom" aria-hidden="true" width="18"> </a> </button>';
                }
            },
            {
                className: "top-margin-0 tdblock data-text-align headinghidemobile",
                data: "fleetName",
                width: "380px",
                render: function (data, type, full, meta) {
                    var htmlContent = "";
                    htmlContent = '<button class="btn btn-icon p-0 d-inline-block d-sm-none editfleetbtn"> <a class="text-black" href="javascript:void(0);" ><span class="material-symbols-rounded addEditFleet cursor-pointer"  title="Edit" data-placement="bottom" aria-hidden="true" >edit</span></a> </button>';
                    return GetCellData("Fleet Name", htmlContent + full.fleetName);
                }
            },
            {
                className: "data-text-align tdblock mt-2 mt-md-0",
                data: "isActive",
                render: function (data, type, full, meta) {
                    //return GetCellData("Active", full.isActive);
                    if (($(window).width() < MobileScreenSize)) {
                        if (full.isActive == "No") {
                            return "<span class='inactivefleet'><span class='material-symbols-rounded'>do_not_disturb_on</span><span class='align-middle'>Inactive</span></span>";
                        }
                        else {
                            return "<span class='activefleet'><span class='material-symbols-rounded'>check_circle</span><span class='align-middle'>Active</span></span>";
                        }
                    }
                    else {
                        return full.isActive;
                    }
                }
            }
        ],
        "initComplete": function (settings) {
            $('#favouriterowcount').text(this.api().data().length);
            RemoveModelLoadingIndicator("#modalManageUserFleet .busy-indicator");
            if (($(window).width() < MobileScreenSize)) {
                $("#btnAddUserFleet").show();
            }


            if (($(window).width() > 767)) {
                var fleetwindow = $(window).height();
                var fleetheader = $('.fleetscrollmanage .modal-header').outerHeight();
                $(".fleetscrollmanage .dataTables_scrollBody").css({
                    "height": fleetwindow - fleetheader - 250
                });
                $('.fleetscrollmanage .dataTables_scrollBody').addClass('scroller p-0');
            }
        }
    });
}

function OpenAddEditFleetModal(fleetId, fleetName, isFleetActive) {
    $('#UserFleetHeader').text('Edit Favourite Fleet');
    $('#hdnFleetId').val(fleetId);
    LoadAvailableVesselList(fleetId);
    LoadMappedVesselList(fleetId);
    GetFleetDetails(fleetId);
}

function GetFleetDetails(fleetId) {
    $.ajax({
        url: "/Dashboard/GetFleetDetailsByFleetId",
        type: "POST",
        data: { "fleetId": fleetId },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            AddModelLoadingIndicator("#modalManageUserFleet .busy-indicator");
        },
        success: function (result) {
            $('#fleetName').val(result.fleetName);
            if (result.isFleetActive) {
                chkIsActive.checked = true;
            }
            else {
                chkIsActive.checked = false;
            }
        }
    });
}

function SaveAndUpdateFleetDetails(fleetId, selectedVesIds) {
    AddModelLoadingIndicator("#modalAddEditUserFleet .busy-indicator");
    var request =
    {
        "FleetId": fleetId,
        "FleetName": $('#fleetName').val().trim(),
        "IsFleetActive": chkIsActive.checked,
    }

    $.ajax({
        url: "/Dashboard/SaveUserFleet",
        type: "POST",
        data: { "request": request, "vesselManagementIds": selectedVesIds },
        "datatype": "JSON",
        success: function (result) {
            if (result.response == true) {
                $('#modalAddEditUserFleet').modal('hide');
                $('.modal-backdrop:gt(1)').remove();
                ToastrAlert("success", result.message);
            }
            else {
                if (result.message != "") {
                    var msg = result.message.replace(/\n/g, "<br />");
                    ToastrAlert("validate", msg);
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator("#modalAddEditUserFleet .busy-indicator");
        }
    });
}

function fnCanCreateFleet(fleetId) {

    var request =
    {
        "FleetId": fleetId,
        "FleetName": $('#fleetName').val(),
        "IsFleetActive": chkIsActive.checked,
    }

    $.ajax({
        url: "/Dashboard/CanCreateUserFleet",
        type: "POST",
        data: { "request": request },
        "datatype": "JSON",
        success: function (result) {
            if (result.response == true) {
                if (result.message != "" && result.message != null) {
                    ToastrAlert("success", result.message);
                }
            }
            else {
                ToastrAlert("error", result.message);
            }
        },
        Compelete: function (result) {
        }
    });
}

function OpenModalInspectionPlanning(IsOverDueSelected, IsNeverDoneSelected, IsDueSelected, IsTypeIdRequired) {

    if (IsOverDueSelected) {
        $("#chkOverdue").prop('checked', true);
    }
    else {
        $("#chkOverdue").prop('checked', false);
    }

    if (IsNeverDoneSelected) {
        $("#chkNeverDone").prop('checked', true);
    }
    else {
        $("#chkNeverDone").prop('checked', false);
    }
    if (IsTypeIdRequired) {
        $("#IsTypeIdRequired").val(true);
    }
    else {
        $("#IsTypeIdRequired").val(false);
    }

    LoadInspectionManagerDueDropdown(IsDueSelected);


    //first time loaded after the drop down is loaded
    //LoadInspectionPlanningList();
}

function LoadInspectionManagerDueDropdown(isDueSelected) {

    var selInspectionPlanningDue = document.getElementById("selInspectionPlanningDue");
    var selInspectionPlanningDueLength = selInspectionPlanningDue.options.length;
    for (var i = selInspectionPlanningDueLength - 1; i >= 0; i--) {
        selInspectionPlanningDue.options[i] = null;
    }

    $.ajax({
        url: "/Inspection/GetInspectionManagerDueInDays",
        type: "POST",
        "datatype": "JSON",
        success: function (result) {
            var data = result.data;
            var select = document.getElementById('selInspectionPlanningDue');

            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
                var opt = document.createElement('option');
                if (!isDueSelected) {
                    if (obj.value == "0") {
                        opt.selected = true;
                    }
                    else {
                        opt.selected = false;
                    }
                }
                else {
                    opt.selected = obj.isDefault;
                }
                opt.value = obj.value;
                opt.innerHTML = obj.description;
                select.appendChild(opt);
            }
            LoadInspectionPlanningList();
        }
    });

}

function OnChangeInspectionPlanningDue() {
    LoadInspectionPlanningList();
}

function OnChangeInspectionChkOverDue() {
    LoadInspectionPlanningList();
}

function OnChangeInspectionchkNeverDone() {
    LoadInspectionPlanningList();
}

function LoadInspectionPlanningList() {
    var vesselName = dashboardVesselDetails.VesselName != "" ? dashboardVesselDetails.VesselName : $("#VesselName").val();
    var vesselId = dashboardVesselDetails.VesselId != "" ? dashboardVesselDetails.VesselId : $("#VesselId").val();

    $("#vesselNameModalInspectionPlanning").text(vesselName);
    $("#vesselNameModalInspectionPlanning").attr('href', "/Dashboard/?VesselId=" + vesselId);

    var inDays = $('#selInspectionPlanningDue').val();
    var isOverdue = $('#chkOverdue').is(':checked');
    var isNeverDone = $('#chkNeverDone').is(':checked');

    var typeIds = "";
    var isTypeRequired = $('#IsTypeIdRequired').val();

    if (isTypeRequired == "true") {
        typeIds = $('#strInspectionTypeIds').val();
    }
    else {
        typeIds = null;
    }

    var inspectionOverviewPlanningRequest;

    if (inDays > 0 && isOverdue && isNeverDone) {

        inspectionOverviewPlanningRequest = {
            "inDays": inDays,
            "isDue": false,
            "isOverdue": false,
            "isNeverDone": false,
            "vesselId": vesselId,
            "typeIds": typeIds
        }
    }
    else {
        inspectionOverviewPlanningRequest = {
            "inDays": inDays,
            "isDue": inDays > 0,
            "isOverdue": isOverdue,
            "isNeverDone": isNeverDone,
            "vesselId": vesselId,
            "typeIds": typeIds
        }
    }

    $('#dtInspectionPlanningDetails').DataTable().destroy();
    dtInspectionPlanningDetails = $('#dtInspectionPlanningDetails').DataTable({
        //"dom": '<<"row "<"detailsinfo"><"col-12 col-md-7 col-lg-7 col-xl-7"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "dom": '<<"row mb-3" <"detailsinfo"><"col-12 col-md-7 offset-md-1 col-lg-7 offset-lg-1 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "scrollY": "235px",
        "scrollCollapse": true,
        "info": true,
        "autoWidth": true,
        "paging": false,
        "ajax": {
            "url": "/Inspection/GetPlanningList",
            "type": "POST",
            "data":
            {
                "inspectionOverviewPlanningRequest": inspectionOverviewPlanningRequest,
            },
            "datatype": "json",
        },
        "language": {
            "emptyTable": "No data available.",
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
                title: "Inspection Planning Details",
                messageTop: function () {
                    return 'Vessel : ' + $('#VesselName').val();
                }
            },
            'pdf', 'print'
        ],
        "columns": [
            {
                className: "top-margin-0 data-text-align tdblock mobile-heading-large",
                data: "inspectionType",
                width: "300px",
                render: function (data, type, full, meta) {
                    //return GetExportCellData('Inspection Type', data);
                    return '<span class="export-Data">' + data + '</span>';
                }
            },
            {
                className: "data-datetime-align",
                data: "occuredDateType",
                width: "95px",
                render: function (data, type, full, meta) { return GetExportFormattedDate(type, 'Occured Date', data); }
            },
            {
                className: "data-datetime-align",
                data: "nextDueDateType",
                width: "95px",
                render: function (data, type, full, meta) { return GetExportFormattedDate(type, 'Next Due', data); }
            },
            {
                className: "data-text-align",
                data: "interval",
                width: "60px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetExportCellData('Default <br/>Interval', '<span class="export-Data d-block">' + data + '</span>');
                    }
                    return full.defaultInterval;
                }
            },
            {
                className: "data-datetime-align",
                data: "managementStartDateType",
                width: "95px",
                render: function (data, type, full, meta) { return GetExportFormattedDate(type, 'Management <br/> Start Date', data); }
            },
            {
                className: "data-text-align tdblock",
                data: "inspectionStatus",
                width: "70px",
                render: function (data, type, full, meta) {
                    var text = data;
                    if (type === "display") {
                        if (data == 'Due') {
                            text = '<span class="txt-orange">' + data + '<span>';
                        }
                        else if (data == 'Overdue') {
                            text = '<span class="txt-red">' + data + '<span>';
                        }
                        else if (data == 'Never Done') {
                            text = '<span class="txt-red">' + data + '<span>';
                        }
                        return GetCellData('Status', GetExportData(text));
                    }
                    return data;
                }
            }
        ]
    });
    //$("div.detailsinfo").html('<span class="counters-heading border-none pr-4">Details</span>');
    //$("div.detailsinfo").html('<span class="counters-heading border-none">Details</span>');
}

function GetTaskNotifications() {

    AddModelLoadingIndicator('#modalTaskMessageNotifications .common-custom-card')

    $('#dtTaskNotifications').DataTable().destroy();

    dtTaskNotifications = $('#dtTaskNotifications').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "order": [],
        "ajax": {
            "url": "/Dashboard/GetUserTaskMessages",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-datetime-align tdblock top-margin-0",
                data: "messageDate",
                width: "40px",
                render: function (data, type, full, meta) {
                    if (full.isFile) {
                        return GetFormattedDateTime(type, 'Date Time', data) + '<a href="javascript:void(0);" class="downloadDocument float-right d-sm-none"> <img src="/images/Download-doc-active.png" class="m-0 align-top"> </a>';
                    } else {
                        return GetFormattedDateTime(type, 'Date Time', data);
                    }
                }
            },
            {
                className: "data-text-align tdblock",
                data: "messageContent",
                width: "150px",
                render: function (data, type, full, meta) {
                    if (full.success) {
                        return GetCellData('Message', data);
                    }
                    else {
                        return GetCellData('Message', '<span class="txt-red">' + data + '</span>');
                    }
                }
            },
            {
                className: "data-icon-align px-0 d-none d-sm-table-cell",
                data: "isFile",
                width: "5px",
                render: function (data, type, full, meta) {
                    if (data) {
                        return '<a href="javascript:void(0);" class="downloadDocument"> <img src="/images/Download-doc-active.png" class="m-0 align-top"> </a>';
                    } else {
                        return '';
                    }
                }
            }
        ],
        "initComplete": function (settings) {
            //$('.spanalertscount').text(dtTaskNotifications.data().count());
            RemoveModelLoadingIndicator('#modalTaskMessageNotifications .common-custom-card')
        }
    });
}

function ToggleDemoMode() {
    $.ajax({
        url: "/Dashboard/UpdateDemoModeSetting",
        type: "GET",
        dataType: "JSON",
        global: false,
        success: function (result) {
            if (result == true || result == 'true' || result == 'True') {
                window.location.replace("/Dashboard");
            }
        }
    });
}

function DownloadTaskMessageDocument(data) {
    $.ajax({
        url: "/Dashboard/DownloadDocument",
        type: "POST",
        dataType: "JSON",
        global: false,
        data: {
            "input": data.downloadDocumentUrl
        },
        success: function (result) {
            if (result != null) {
                if (result.bytes != null && result.bytes != '') {
                    var array = base64ToArrayBuffer(result.bytes);
                    saveByteArray(result.filename, array, result.fileType);
                    ToastrAlert("success", data.messageContent);
                }
                else {
                    ToastrAlert("validate", "File Not Found for \" " + data.filename + "\"");
                }
            }
            else {
                ToastrAlert("validate", "Generated file can only be downloaded once. File is either already downloaded or has failed to generate.");
            }
        }
    });
}

$(window).on('beforeunload', function () {
    DisconnectSignalR();
});

function DisconnectSignalR() {
    if (connection != null && connection.id != "") {
        connection.hub.stop().done();
    }
}

function SetReminderAlertFromDb(alertText, alertTimer) {
    setTimeout(function () {
        RemoveClassIfPresent('.reminderAlert', 'd-none');
        $('.span-reminder-text').text(alertText);
        $('.reminderAlert').css('position', 'fixed');
        $('.reminderAlert').fadeIn();
    }, alertTimer);
}


//ADd class doesnot work because it is used as toggle
function HideShortDateFormat() {
    AddClassIfAbsent('.notes-sidebar-open .collapse-date', 'd-none');
}

function ResetAttachedFilesVariables() {
    AttachedFiles = [];
    FilesAttached = 0;
}

function ResetNCMMandatoryFields() {
    $("#txtNCMSubject").css("border", "2px solid #e5e5e5");
    $("#txtNCMMessage").css("border", "2px solid #e5e5e5");
    $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", '#e5e5e5');
}

function OpenNewChannelModal() {
    $('.add-list').hide();
    $("#modalDraftChannelBody").data("createdraft", "1");
    $("#modalCreateNewChannel").modal('show');
}

function UpdateReminderStatus(request, onCompleteCallBack) {
    $.ajax({
        url: "/Notes/UpdateReminderStatus",
        type: "POST",
        dataType: "JSON",
        data: request,
        complete: function () {
            $("#hdnCurrentStatusPageNumber").val(1);
            UpdateNotesTab();
            UpdateDashboardReminderAlertTab();
            if (!IsNullOrEmptyOrUndefinedLooseTyped(onCompleteCallBack)) {
                onCompleteCallBack();
            }
        }
    });
}


function UpdateReminderToDismiss(request, onCompleteCallBack) {
    $.ajax({
        url: "/Notes/DismissReminderWithoutEncryptedKey",
        type: "POST",
        dataType: "JSON",
        data: request,
        success: function (data) {
            if (data == true) {
                ToastrAlert("success", "Reminder has been dismissed");
            }
            else {
                ToastrAlert("error", "Something went wrong!");
            }
        },
        complete: function () {
            $("#hdnCurrentStatusPageNumber").val(1);
            UpdateNotesTab();
            UpdateDashboardReminderAlertTab();
            if (!IsNullOrEmptyOrUndefinedLooseTyped(onCompleteCallBack)) {
                onCompleteCallBack();
            }
        }
    });
}


function GetNoteDetails(noteId, successCallback) {
    $.ajax({
        url: "/Notes/NoteDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedNoteId": noteId
        },
        success: function (data) {
            if (data != null) {

                if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                    successCallback(data);
                }

            }
        }
    });
}

function SetDynamicTextAreaHeight() {
    var textarea = document.getElementById("txtNoteDescription");
    var limit = 240; //height limit

    textarea.oninput = function () {
        textarea.style.height = "";
        textarea.style.height = Math.min(textarea.scrollHeight, limit) + "px";
    };
}



function CreateReminderAlertRow(data) {
    if (data != null) {
        var NoteTitle = IsNullOrEmptyOrUndefinedLooseTyped(data.NoteTitle) ? '' : data.NoteTitle;
        var NoteDescription = IsNullOrEmptyOrUndefinedLooseTyped(data.NoteDescription) ? '' : data.NoteDescription;
        var isChecked = data.NoteStatus == 2 ? ' checked ' : ''; //completed checked 
        var isDisabled = data.NoteStatus == 2 ? ' disabled ' : ''; //completed disabled   
        let localRow = '<div class="repeat-notes">\
                        <div class="">\
                             <div class="remindersection clearfix">\
                                 <div class="span-reminder-title">' + NoteTitle + '</div>\
                                 <div class="span-reminder-description">' + NoteDescription + '</div>\
                                    <div class="close-notes">\
										<div class="dismissReminderAction">Dismiss Reminder</div>\
									</div>\
                             </div>\
                             <div class="notes-module notesReminder text-right" data-noteid="' + data.NoteId + '" + data-reminderid="' + data.NoteReminderId + '">\
                                 <div class="markascomplete">\
                                     <div class="marktext"> Mark note as completed </div>\
                                     <div class="custom-control custom-switch">\
                                         <input type="checkbox" class="custom-control-input aReminderCompleteNote" id="markascomplete'+ data.NoteId + '" ' + isChecked + isDisabled + '>\
                                         <label class="custom-control-label" for="markascomplete'+ data.NoteId + '"></label>\
                                     </div>\
                                 </div>\
                                 <div class="snooze">\
                                     <div class="snoozetext"> Snooze reminder for </div>\
                                     <div class="minutessnooze">\
                                         <select name="" class="form-control selMinuteInternal" id="">\
                                             <option value="5">5 Minutes</option>\
                                             <option value="10">10 Minutes</option>\
                                             <option value="15" selected>15 Minutes</option>\
                                             <option value="30">30 Minutes</option>\
                                             <option value="60">1 Hour</option>\
                                             <option value="1440">1 Day</option>\
                                         </select>\
                                     </div>\
                                     <a href="javascript:void(0);" class="aReminderSnooze">Snooze</a>\
                                 </div>\
                             </div>\
                         </div>\
                   </div>';
        return localRow;
    }
}


function GetCurrentMinute(currentMin) {
    if (0 < currentMin && currentMin <= 15) {
        return 15;
    }
    if (15 < currentMin && currentMin <= 30) {
        return 30;
    }
    if (30 < currentMin && currentMin <= 45) {
        return 45;
    }
    else {
        return currentMin;
    }
}

function UpdateReminderView() {
    let chidlrens = $(".notesReminderAlertContainer").find('.repeat-notes');
    let visibleChilds = $(chidlrens).not('.d-none');
    if (visibleChilds.length == 0) {
        AddClassIfAbsent('.reminderAlert', 'd-none');
    }
    else {
        RemoveClassIfPresent('.reminderAlert', 'd-none');
    }
}

function SetVesselNameToHeaderDD() {
    $.ajax({
        url: "/Common/GetVesselNameFromUrl",
        type: "post",
        datatype: "json",
        data: {
            url: window.location.href
        },
        success: function (data) {
            if (!IsNullOrEmptyOrUndefinedLooseTyped(data) && $('#IsAllowSelectFleetParent').val() !== 'true') {
                $(".spanfleetSelectionTitle").text(data);
            }
        },
    });
}

function CreateDiscussionRequest(noteId) {
    $.ajax({
        url: "/Dashboard/CreateParameterFromNotes",
        type: "post",
        datatype: "json",
        data: {
            "request": noteId
        },
        success: function (data) {
            OpenCreateDiscussionModalFromNotes(data);
        }
    });
}

function OpenCreateDiscussionModalFromNotes(jsonMessage) {
    $.ajax({
        url: "/Dashboard/GetEncodedBase64String",
        type: "post",
        datatype: "json",
        data: {
            "request": jsonMessage
        },
        success: function (data) {
            $('#createRequest').val(data);
            $("#notificationcreatechannelform").submit();
            OpenNewChannelModal();
        }
    });
}


function ResetMasterCheckBox() {

    if ($('.available-master-checkbox').prop('checked') == true) {
        $('.available-master-checkbox').prop('checked', false);
    }


    if ($('.mapped-master-checkbox').prop('checked') == true) {
        $('.mapped-master-checkbox').prop('checked', false);
    }
}

function CurrentVoyageAgentDetails(urlRequest, portname) {
    $('#VoyageAgentContentsHtml').html("");
    $('#VoyageAgentCount').html("0");
    $('#VoyagePortName').text("");
    $('#ModalVoyageAgentDetails').modal("show");

    AddModelLoadingIndicator("#ModalVoyageAgentDetails");
    $.ajax({
        url: "/Dashboard/GetAgentDetails",
        type: "POST",
        "data": {
            "input": urlRequest,
        },
        "datatype": "JSON",
        success: function (result) {
            if (result != null && result.length > 0) {

                $('#VoyageAgentCount').text(result.length);
                $('#VoyagePortName').text(portname);

                for (var index = 0; index < result.length; index++) {
                    result.length;
                    var obj = result[index];
                    var tabContentsHtml = '';
                    var copyAgentDetails = '';
                    var agentType = obj.typeOfCompany
                    var agentNumber = index + 1;
                    //cmpname + agent type
                    tabContentsHtml += '<div class="agentlist" >';

                    tabContentsHtml += ' <div class="body-box" id=""><div class="agentheadtitle">Agent ' + agentNumber + '</div ></div > ';
                    tabContentsHtml += '<div class="body-box" id=""><div><div class="main-title title" id="">' + obj.companyName + ' (' + agentType + ')</div> </div> </div>';

                    //location
                    tabContentsHtml += '<div class="body-box" id="">';
                    tabContentsHtml += '<div class="image align-top"><img id="" class="" src="/images/location-supplier.png"></div>';
                    tabContentsHtml += '<div>';

                    if (!IsNullOrEmptyOrUndefined(obj.address)) {
                        tabContentsHtml += '<div class="title">';
                        tabContentsHtml += '<div id="">' + obj.address + '</div>';
                        tabContentsHtml += '</div>';
                    }

                    if (!IsNullOrEmptyOrUndefined(obj.town) || !IsNullOrEmptyOrUndefined(obj.state) || !IsNullOrEmptyOrUndefined(obj.postalCode)) {
                        tabContentsHtml += '<div class="title" id="">';

                        if (!IsNullOrEmptyOrUndefined(obj.town)) {
                            tabContentsHtml += '<span>' + obj.town + '&nbsp;</span>';
                        }
                        if (!IsNullOrEmptyOrUndefined(obj.state)) {
                            tabContentsHtml += '<span>' + obj.state + '&nbsp;</span>';
                        }

                        if (!IsNullOrEmptyOrUndefined(obj.postalCode)) {
                            tabContentsHtml += '<span>' + obj.postalCode + '&nbsp;</span>';
                        }
                        tabContentsHtml += '</div>';
                    }

                    if (!IsNullOrEmptyOrUndefined(obj.country)) {
                        tabContentsHtml += '<div class="title" id="">' + obj.country + '</div>';
                    }
                    tabContentsHtml += '</div></div>';

                    //end location

                    //telephone
                    tabContentsHtml += '<div class="body-box" id="">';
                    tabContentsHtml += '<div class="image align-top"><img id="" class="" src="/images/telephone-supplier.png" width="19"></div>';

                    tabContentsHtml += '<div>';

                    if (!IsNullOrEmptyOrUndefined(obj.mobile)) {
                        tabContentsHtml += '<div class="title" id="">';
                        tabContentsHtml += '<a id="" class="email position-relative" href="tel:' + obj.mobile + '">';
                        tabContentsHtml += '<div id="">' + obj.mobile + '</div></a> </div>';
                    }

                    if (!IsNullOrEmptyOrUndefined(obj.telephone)) {
                        tabContentsHtml += '<div class="title" id="">';
                        tabContentsHtml += '<a id="" class="email position-relative" href="tel:' + obj.telephone + '">';
                        tabContentsHtml += '<div id="">' + obj.telephone + '</div></a> </div>';
                    }

                    if (!IsNullOrEmptyOrUndefined(obj.telex)) {
                        tabContentsHtml += '<div class="title" id="">';
                        tabContentsHtml += '<a id="" class="email position-relative" href="tel:' + obj.telex + '">';
                        tabContentsHtml += '<div id="">' + obj.telex + '</div></a> </div>';
                    }
                    tabContentsHtml += '</div></div>';
                    //end telephone

                    //fax
                    if (!IsNullOrEmptyOrUndefined(obj.fax)) {
                        tabContentsHtml += ' <div class="body-box" id="">';
                        tabContentsHtml += '<div class="image"><img id="" class="" src="/images/fax-suppliernew.png"></div>';

                        tabContentsHtml += '<div>';

                        tabContentsHtml += '<div class="title" id="spanFax">' + obj.fax + '</div>';

                        tabContentsHtml += '</div ></div >';
                    }

                    //email
                    if (!IsNullOrEmptyOrUndefined(obj.email)) {
                        tabContentsHtml += '<div class="body-box" id="">';
                        tabContentsHtml += '<div class="image"> <img id="" class="" src="/images/email-supplier.png" width="20"> </div>';

                        tabContentsHtml += '<div>';

                        tabContentsHtml += '<div class="title">';
                        tabContentsHtml += '<a id="" class="email position-relative" href="mailto:' + obj.email + '">';
                        tabContentsHtml += '<div id="">' + obj.email + '</div></a></div>';

                        tabContentsHtml += '</div></div>';
                    }

                    //website
                    if (!IsNullOrEmptyOrUndefined(obj.website)) {
                        tabContentsHtml += '<div class="body-box" id="">';
                        tabContentsHtml += '<div class="image"> <img id="" class="" src="/images/web-supplier.png"> </div>';

                        tabContentsHtml += '<div>';

                        tabContentsHtml += '<div class="title" id="">';
                        let cmpWww = obj.website;
                        if (!obj.website.match(/^http([s]?):\/\/.*/)) {
                            cmpWww = 'http://' + obj.website;
                        }

                        tabContentsHtml += '<a href="' + cmpWww + '" target="_blank" class="email position-relative">';
                        tabContentsHtml += '<div id="">' + cmpWww + '</div></a></div>';

                        tabContentsHtml += '</div></div>';
                    }

                    //notes // d-none
                    if (!IsNullOrEmptyOrUndefined(obj.notes)) {
                        tabContentsHtml += '<div class="body-box mb-0 d-none" id="">';
                        tabContentsHtml += '<div class="image"> <img id="" class="" src="/images/web-supplier.png"> </div>';

                        tabContentsHtml += '<div>';

                        tabContentsHtml += '<div class="title" id="">';
                        tabContentsHtml += '<div id="">' + obj.notes + '</div></div>';

                        tabContentsHtml += '</div></div>';
                    }

                    tabContentsHtml += '</div>';

                    $('#VoyageAgentContentsHtml').append(tabContentsHtml);


                }
            }
        },
        complete: function (setting) {
            RemoveModelLoadingIndicator("#ModalVoyageAgentDetails");
            var windowheightagent = $(window).height();
            var modalheaderagent = $('#ModalVoyageAgentDetails .modal-header').outerHeight();
            $("#ModalVoyageAgentDetails .scroller").css({
                "max-height": windowheightagent - modalheaderagent - 100
            });
        }
    });
}

function OpenOfflineModal() {
    $.ajax({
        url: '/OfflineAccess/GetOfflineDetailsModal',
        success: function (response) {
            fn_InitializeOfflineModal(response.view, 'divOfflineModalPopPup')
        }
    })
}

function fn_InitializeOfflineModal(modalHtml, modalId) {
    $('body').append(modalHtml);
    setTimeout(function () {
        $('#' + modalId).modal('show');
        $('#' + modalId).on('hidden.bs.modal', function (e) {
            $(this).remove();
        })
    }, 200)
}

async function fn_getOfflineData(list) {
    $.ajax({
        url: '/OfflineAccess/GetAppOfflineURLs',
        method: 'POST',
        data: { data: list },
        success: async function (response) {
            if (response) {
                if (vshipDb) {
                    let offlineData = await vshipDb.getAll('appMetaData');
                    if (offlineData && offlineData.length > 0) {
                        offlineData.forEach(function (d, idx) {
                            vshipDb.delete('appMetaData', idx);
                        });
                    }
                    response.urls.forEach(function (d, idx) {
                        vshipDb.put('appMetaData', idx, d);
                    })
                }
                fn_TakeDataOffline();
                setInterval(fn_TakeDataOffline,600000)
            }
        }
    });
}

async function fn_TakeDataOffline() {
    /*window.location.reload();*/
    let offlineData = await vshipDb.getAll('appMetaData');
    if (offlineData.length > 0)
    {
        for (let url of offlineData) {
            $.ajax({
                url: url.url,
                success: function (response)
                {
                    console.log(response);
                }
            })
        }
    }
}

async function fn_TakeAppOffline() {
    let data = [];

    new Promise(function (resolve) {
        $('#divOfflineModalPopPup').modal('hide');
        $('.input-offline-modules:checkbox:checked').each(function () { data.push({ viewid: $(this).data('viewid'), moduleid: $(this).data('moduleid') }); });
        resolve();
    }).then(function () {
        fn_getOfflineData(data);
    })
}


$(document).on('change', '.input-offline-modules', function () {
    const viewId = $(this).data('viewid');
    const moduleId = $(this).data('moduleid');

    let this_obj = $(this);

    if (viewId != "" && viewId != 0) {
        $(`.input-offline-modules[data-viewid="${viewId}"]`).prop('checked', this_obj.prop('checked'));
    }
    if (moduleId != "" && moduleId != 0) {
        $(`.input-offline-modules[data-moduleid="${moduleId}"]`).prop('checked', this_obj.prop('checked'));
    }
})