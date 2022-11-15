import {  saveByteArray, base64ToArrayBuffer, IsNullOrEmptyOrUndefinedLooseTyped, RemoveClassIfPresent, AddModelLoadingIndicator, RemoveModelLoadingIndicator, GetCookie, ToastrAlert, GetLastReferrer, AddClassIfAbsent, GetDiscussionNotesCount, GetAttachmentViewUtility, GetAttachmentIconUtility, AttachmentTemplateUtility, IsNullOrEmptyOrUndefined, ConvertByteToMegaBytes} from "../common/utilities.js"
import { MessageCateory_General, MessasgeCategory_PurchaseOrder, MessasgeCategory_Inspection, MessasgeCategory_InspectionFinding, MessasgeCategory_Certificate, FileAttachmentLimit, FileAttachmentLimitErrorMessage, MessasgeCategory_Crew, MessasgeCategory_HazOcc, MessasgeCategory_SeaPassage, MessasgeCategory_PortCallLocationEvent, MessasgeCategory_DefectWorkOrder, MessasgeCategory_PlannedMaintenance, MessasgeCategory_JSA } from "../common/constants.js"
import moment from "moment";

var maxLengthNotesTitle = 22;
var SelectedNotesVesselId = '';
var IsNotedVesselHeaderAdded = false;
var ReminderDateTime = null;
// Notes Attachments
window.NotesAttachedFiles = [];
//
export function GetAreaList(selectedCategory) {
    var selAreaSelection = document.getElementById('selArea');
    var selAreaSelectionLength = selAreaSelection.options.length;
    for (var i = selAreaSelectionLength - 1; i >= 0; i--) {
        selAreaSelection.options[i] = null;
    }

    $.ajax({
        url: "/Notes/GetNotesAreaList",
        type: "POST",
        "datatype": "JSON",
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('#divNotesEditSection');
        },
        success: function (data) {
            var select = document.getElementById('selArea');

            for (var i = 0; i < data.length; i++) {
                var obj = data[i];
                var opt = document.createElement('option');
                if (!IsNullOrEmptyOrUndefinedLooseTyped(selectedCategory) && obj.catId == selectedCategory) {
                    opt.selected = true;
                }
                else if (IsNullOrEmptyOrUndefinedLooseTyped(selectedCategory) && obj.catId == MessageCateory_General) {
                    opt.selected = true;
                }
                else {
                    opt.selected = false;
                }
                opt.value = obj.catId;
                opt.innerHTML = obj.categoryName;
                select.appendChild(opt);
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divNotesEditSection');
        }
    });
}

export function RecordLevelNote(messageDetailsJSON) {
    if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
        RemoveClassIfPresent("#aBaseRecordLevelNote", "d-none");
        $("#aBaseRecordLevelNote").click(function () {
            var messageDetailsObj = JSON.parse(messageDetailsJSON);

            var categoryId = messageDetailsObj.CategoryId;
            if (!IsNullOrEmptyOrUndefinedLooseTyped(categoryId) && categoryId != MessageCateory_General) {
                $("#selArea").prop("disabled", true);
            }
            GetAreaList(categoryId);

            $('#txtNoteTitle').val(messageDetailsObj.DefaultMessage);
            $('#hdnNotesContextParams').val(messageDetailsObj.ContextPayload);
            $('#hdnNotesReferenceId').val(messageDetailsObj.ReferenceIdentifier);

            var vesselId = messageDetailsObj.VesselId;
            var vesselName = messageDetailsObj.VesselName;
            if (!(IsNullOrEmptyOrUndefinedLooseTyped(vesselId) && IsNullOrEmptyOrUndefinedLooseTyped(vesselName))) {
                var option = new Option(vesselName, vesselId, true, true);
                $('#cboNotesVesselSearch').append(option).trigger('change');
                $("#cboNotesVesselSearch").prop("disabled", true);
            }
        });
    }
}

function CreateNoteView(result) {
    if (result != null) {
        let vesselName = result.vesselName;
        let module = result.catId == MessageCateory_General ? '' : result.categoryName;
        let reminderTimeStamp = result.expectedExecutionDateTime;
        let dateCreatedModified = moment(result.shortDate, "DD-MM-YYYY").format("DD/MM/YY");
        let noteName = result.noteTitle;
        let isNotesModuleHidden = IsNullOrEmptyOrUndefinedLooseTyped(vesselName)
            && IsNullOrEmptyOrUndefinedLooseTyped(module)
            && IsNullOrEmptyOrUndefinedLooseTyped(reminderTimeStamp);
        let notesModuleVisiblity = isNotesModuleHidden ? 'd-none' : '';
        let reminderIconPath = result.isReminderExpired ? '/images/notes-reminder-red.png' : '/images/notes-reminder.png';
        let localRow = '<li>\
							<div class="notes-box">\
								<div class="row mx-auto no-gutters notename cursor-pointer" data-id="'+ result.noteId + '">\
									<div class="col-8 col-md-8 col-lg-8 col-xl-9 my-auto p-0">\
										<div class="notes-box-title " >\
											<h1 class="clickEventDisable">'+ noteName + '</h1>\
										</div>\
									</div>\
									<div class="col-4 col-md-4 col-lg-4 col-xl-3 my-auto p-0">\
										<div class="collapse-date d-block">\
											<h1>'+ dateCreatedModified + '</h1>\
										</div>\
									</div>\
								</div>\
								<div class="notes-module '+ notesModuleVisiblity + '">\
									<div class="moduledata '+ GetViewVisiblity(vesselName) + '">\
										<img src="/images/notes-vessel-name.png" />'+ vesselName + '\
									</div>\
									<div class="moduledata navigateToDetails cursor-pointer '+ GetViewVisiblity(module) + '" data-noteid="' + result.noteId + '" data-vesselid="' + result.vesselId + '">\
										'+ GetNotesModuleIcon(result.catId) + module + '\
									</div>\
									<div class="moduledata '+ GetViewVisiblity(reminderTimeStamp) + '">\
										<img src="'+ reminderIconPath + '" />' + reminderTimeStamp + '\
									</div>\
								</div>\
							</div>\
                        </li>';
        return localRow;
    }
}

function GetViewVisiblity(data) {
    if (IsNullOrEmptyOrUndefinedLooseTyped(data)) {
        return 'd-none';
    } else {
        return '';
    }
}

function CreateNoteList(data) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let note = data[i];
            var newRow = CreateNoteView(note);
            $('#noteListTab').append(newRow);
        }
    }
}

function CreateReminderList(data) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let reminder = data[i];
            var reminderRow = CreateReminderView(reminder);
            $('#reminderAlertsTab').append(reminderRow);
        }
    }
}

function CreateReminderView(result) {
    if (result != null) {
        let reminderTimeStamp = result.expectedExecutionTime;
        let dateCreatedModified = moment(result.noteCreatedDate, "DD-MM-YYYY").format("DD/MM/YY");
        let noteName = result.noteTitle;
        let reminderIconPath = result.isReminderExpired ? '/images/notes-reminder-red.png' : '/images/notes-reminder.png';
        let localRow = '<li>\
							<div class="notes-box">\
								<div class="row mx-auto no-gutters notename cursor-pointer" data-id="'+ result.noteId + '">\
									<div class="col-8 col-md-8 col-lg-8 col-xl-9 p-0">\
										<div class="notes-box-title " >\
											<h1 class="clickEventDisable">'+ noteName + '</h1>\
										</div>\
									</div>\
								</div>\
								<div class="notes-module clearfix">\
									<div class="moduledata '+ GetViewVisiblity(reminderTimeStamp) + '">\
										<img src="'+ reminderIconPath + '" />' + reminderTimeStamp + '\
									</div>\
                                    <div class="pull-right close-notes">\
                                        <div class="dismissReminder" data-id="'+ result.encryptedReminderId + '"> Dismiss </div>\
                                    </div>\
								</div>\
							</div>\
                        </li>';
        return localRow;
    }
}


export function GetNotesListOnDashboardPage(isScrolled) {

    var data = {
        'SearchText': '',
        'StatusIds': 1,
        'IsPageScroled': isScrolled,
        'PageNumber': parseInt($('#hdnNotesTabCurrentPageNumber').val())
    }

    $.ajax({
        url: "/Notes/GetNotesList",
        type: "Post",
        dataType: "JSON",
        data: data,
        beforeSend: function (xhr) {
            if (!isScrolled) {
                AddModelLoadingIndicator('#divNotesTab');
                $('#noteListTab').empty();
            }
        },
        success: function (data) {
            CreateNoteList(data.data);
            $('#hdnNotesTabHasNextPage').val(data.hasNextScroll);
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divNotesTab');
        }
    });
}


function NoteRowCreated(result) {

    if (result != null) {
        let isChecked = result.status == 2 ? 'checked' : ''; //completed
        let isVesselAvailable = IsNullOrEmptyOrUndefinedLooseTyped(result.vesselId) ? 'd-none' : '';
        let isCategoryAvailable = result.catId == MessageCateory_General ? 'd-none' : '';
        let isReminderDateAvailable = IsNullOrEmptyOrUndefinedLooseTyped(result.expectedExecutionDateTime) ? 'd-none' : '';
        let reminderIconPath = result.isReminderExpired ? '/images/notes-reminder-red.png' : '/images/notes-reminder.png';
        let localRow = '<li>\
                            <div class="notes-box" data-id="' + result.noteId + '">\
                                <div class="row mx-auto no-gutters">\
                                    <div class="col-7 col-md-8 col-lg-8 col-xl-8 my-auto">\
                                        <div class="notes-box-title cursor-pointer editNote">\
                                            <h1>' + ShortNoteTitle(result.noteTitle) + '</h1>\
                                        </div>\
                                    </div>\
                                    <div class="col-5 col-md-4 col-lg-4 col-xl-4 my-auto">\
                                        <div class="icons-right">\
                                            <a href="javascript:void(0);" id="" data-toggle="tooltip" data-placement="bottom" title="Mark as Completed">\
                                                <div class="markascomplete notesmarkascomplete">\
                                                    <div class="custom-control custom-switch" >\
                                                        <input type="checkbox" class="custom-control-input" id="markascomplete'+ result.noteId + '" data-id="' + result.noteId + '" ' + isChecked + '>\
                                                        <label class="custom-control-label" for="markascomplete'+ result.noteId + '"></label>\
                                                    </div>\
                                                </div >\
                                            </a>\
                                            <div class="notesdropdown">\
                                                <div class="dropdown" >\
                                                    <a class="btn p-0 m-0 dropdown-toggle notesListDropdown" href="#" role="button" id="notesdropdown'+ result.noteId + '" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">\
                                                        <img src="/images/notesdrop.png" />\
                                                    </a>\
                                                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="notesdropdown'+ result.noteId + '">\
                                                        <a href="javascript:void(0);" class="editNote" title="Edit">\
                                                            <div> <img src="/images/edit-notes.svg" /></div> Edit\
                                                        </a>\
                                                        <a href="javascript:void(0);" id="' + result.noteId + '" class="deleteNote" title="Delete">\
                                                            <div> <img src="/images/delete-notes.svg" /></div> Delete\
                                                        </a>\
                                                        <a href="javascript:void(0);" class="sendMail" title="Email">\
                                                            <div> <img src="/images/email-notes.svg" /></div> Email\
                                                        </a>\
                                                        <a href="javascript:void(0);" id="' + result.noteId + '" class="createVChat" title="Create Chat">\
                                                            <img src="/images/chat-headericon-notes.svg" width="18" class="chatnotes"/>Create Chat\
                                                        </a>\
                                                    </div>\
                                                </div>\
                                            </div>\
                                        </div>\
                                        <div class="collapse-date">\
                                            <h1>' + result.shortDate + '</h1>\
                                        </div>\
                                    </div>\
                                </div>\
                                <div class="cursor-pointer editNote">\
                                    <div class="notes-desc scroller" >\
                                        <p>' + result.noteDescription + '</p>\
                                    </div>\
                                    <div class="notes-date">\
                                        <h1>' + result.noteDateUTC + '</h1>\
                                    </div>\
                                </div>\
                                <div class="notes-module">\
                                    <div class="moduledata ' + isVesselAvailable + '">\
                                        <img src="/images/notes-vessel-name.png" />' + result.vesselName + '\
                                    </div>\
                                <div class="moduledata navigateToDetails cursor-pointer ' + isCategoryAvailable + '" data-noteid="' + result.noteId + '" data-vesselid="' + result.vesselId + '">\
                                    ' + GetNotesModuleIcon(result.catId) + result.categoryName + '\
                                </div>\
                                <div class="moduledata ' + isReminderDateAvailable + '" >\
                                    <img src="'+ reminderIconPath + '" />' + result.expectedExecutionDateTime + '\
                                </div >\
                            </div>\
                        </div>\
                        </li >';
        return $(localRow);
    }
}

function AppendAttachments(localRow, result) {
    if (result.isAttachment == true && result.attachmentDetails != null) {
        let attachments = result.attachmentDetails;
        for (var i = 0; i < attachments.length; i++) {
            let attachment = attachments[i];
            $(localRow).find('.notes-box').append(GetAttachmentViewUtility(attachment));
        }
    }
}


export function SaveNoteDetails() {
    var isReminderDateEmpty = IsNullOrEmptyOrUndefinedLooseTyped($('#reminderDate').val());
    var ReminderDateTimeUTCFormatted = null;
    if (!isReminderDateEmpty) {
        let localReminderDate = new Date($('#reminderDate').val());
        let newLocalReminderDate = moment(localReminderDate).format("DD MMM YYYY HH:mm");
        if (newLocalReminderDate != null) {
            let ReminderDateTimeUTC = new Date(new Date(newLocalReminderDate).toUTCString().substr(0, 25));
            ReminderDateTimeUTCFormatted = moment(ReminderDateTimeUTC).format("DD MMM YYYY HH:mm");
        }
    }

    var NoteDescription = $('#txtNoteDescription').val().trim();
    var NoteTitle = $('#txtNoteTitle').val().trim();
    NoteTitle = IsNullOrEmptyOrUndefinedLooseTyped(NoteTitle) ? NoteDescription.substring(0, 50) : NoteTitle;

    let IsSelectedReminderExpired = false; 
    if ($('#hdnIsReminderIsExpired').val().toLowerCase() == "true") {
        IsSelectedReminderExpired = true;
    }

    let SelectedReminderId = +$('#hdnNVReminderId').val();

    let IsReminderAlertDismissed = false;
    if ($('#hdnIsReminderAlertDismissed').val().toLowerCase() == "true") {
        IsReminderAlertDismissed = true;
    }

    let SelectedNoteStatusId = +$('#hdnNoteStatus').val();
    
    let request = {
        "NoteId": $("#hdnNoteId").val(),
        "NoteTitle": NoteTitle,
        "NoteDescription": NoteDescription,
        "VesselId": SelectedNotesVesselId,
        "AttachmentList": window.NotesAttachedFiles,
        "Status": SelectedNoteStatusId,
        "CatId": IsNullOrEmptyOrUndefinedLooseTyped($('#selArea').val()) ? MessageCateory_General : $('#selArea').val(),
        "ContextParams": $('#hdnNotesContextParams').val(),       
        "NotesReminders": [{ ReminderId: SelectedReminderId, ExpectedExecutionTime: ReminderDateTimeUTCFormatted, IsReminderExpired: IsSelectedReminderExpired, IsAlertDismissed: IsReminderAlertDismissed }],
        "ReferenceIdentifier": $('#hdnNotesReferenceId').val()
    }

    $.ajax({
        url: "/Notes/SaveNoteDetails",
        type: "POST",
        dataType: "JSON",
        data: request,
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('#divNotesEditSection')
        },
        success: function (data) {
            if (data > 0) {
                //ToastrAlert("success", "Note added successfully.");
            }
            else {
                //ToastrAlert("error", "Something went wrong!");
            }
        },
        complete: function () {
            //refresh the list
            RemoveModelLoadingIndicator('#divNotesEditSection');
            ShowNoteListSection();
            $("#hdnCurrentStatusPageNumber").val(1);
            UpdateDashboardNoteList();
            UpdateDashboardReminderAlertTab();
            UpdateNotesTab();
            let messageDetailsJSON = $("#MessageDetailsJSON").val();
            if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
                GetDiscussionNotesCount(messageDetailsJSON);
            }
            ResetNotesAttachment();
            $('#cboNotesVesselSearch').val(null).trigger('change');
        }
    });
}

export function UpdateDashboardNoteList() {
    let lastReferrer = GetLastReferrer();
    if (lastReferrer.includes('Dashboard')) {
        GetNotesListOnDashboardPage(false);
    }
}

export function UpdateDashboardReminderAlertTab() {
    let lastReferrer = GetLastReferrer();
    if (lastReferrer.includes('Dashboard')) {
        CreateReminderAlertOnDashboard();
    }
}

export function ShowNoteListSection() {
    $('.notes-sidebar-open .theme-settings__inner').show();
    $('.my-note').show();
    $('.add-note').hide();
    $('.search-filter-n').show();
    ResetNotesAttachment();
}

export function BindSelectedNoteDetails(noteId) {

    $("#hdnNoteId").val(noteId);

    $.ajax({
        url: "/Notes/NoteDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedNoteId": noteId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('#divNotesEditSection');
            ShowNoteDetailsSection();            
        },
        success: function (data) {
            if (data != null) {
                $('#txtNoteTitle').val(data.noteTitle);

                $('#txtNoteDescription').val(data.noteDescription);                
                if (!(IsNullOrEmptyOrUndefinedLooseTyped(data.vesselId) && IsNullOrEmptyOrUndefinedLooseTyped(data.vesselName))) {                 
                    var option = new Option(data.vesselName, data.vesselId, true, true);
                    $('#cboNotesVesselSearch').append(option).trigger('change');
                }
                else {                    
                    var option = new Option('', '', true, true);
                    $('#cboNotesVesselSearch').append(option).trigger('change');
                }
                $('#hdnNoteStatus').val(data.status);                
                if (data.status == 1) {
                    $('#chkMarkAsCompleteEdit').prop('checked', false);
                }
                else if (data.status == 2) {
                    $('#chkMarkAsCompleteEdit').prop('checked', true);
                }
                
                var categoryId = data.catId;
                var categoryName = data.categoryName;
                if (!IsNullOrEmptyOrUndefinedLooseTyped(categoryId) && categoryId != MessageCateory_General) {
                    RemoveClassIfPresent('#divAreaDropdown', 'd-none');
                    var option = new Option(categoryName, categoryId, true, true);
                    $('#selArea').append(option).trigger('change');
                    $("#selArea").prop("disabled", true);
                    $("#cboNotesVesselSearch").prop("disabled", true);
                }

                //Reminder Changes
                if (data.notesReminders != null && data.notesReminders[0] != null) {
                    let reminder = data.notesReminders[0];                    
                    $('#hdnNVReminderId').val(reminder.reminderId);                                        
                    $('#hdnIsReminderIsExpired').val(reminder.isReminderExpired);
                    $('#hdnIsReminderAlertDismissed').val(reminder.isAlertDismissed);
                    if (!IsNullOrEmptyOrUndefinedLooseTyped(reminder.expectedExecutionTime)) {
                        cb(new moment(reminder.expectedExecutionTime));
                    }
                }
                if (data.isAttachment == true && data.attachmentDetails != null && data.attachmentDetails.length > 0) {
                    let attachments = data.attachmentDetails;
                    for (var i = 0; i < attachments.length; i++) {
                        let attachment = attachments[i];
                        var row = AttachmentTemplateUtility(GetAttachmentIconUtility(attachment.fileExtension), attachment.description, attachment.sequence, '',attachment.ettId);
                        $("#divNotesMessageAttachemnts").append(row);
                        window.NotesAttachedFiles.push(attachment);
                    }
                }
            }
        },
        complete: function () {
            //ClearNoteDetailsSection();
            RemoveModelLoadingIndicator('#divNotesEditSection');

        },
    });
}



export function ShowNoteDetailsSection() {
    $('.notes-sidebar-open .theme-settings__inner').hide();
    $('.my-note').hide();
    $('.add-note').show();
    if ($(window).width() < 767) {
        $('body').addClass('overflow-hidden');
    }
    $('.search-filter-n').hide();
}

export function EditNote(noteId) {
    ClearNoteDetailsSection();
    BindSelectedNoteDetails(noteId);
    HideNoteFilter();
    $('#spanNoteDetailHeader').text('Edit Note');    
    ShowHideEditActions(true);
}

export function DeleteNote(noteId) {
    DeleteSelectedNote(noteId);
}

export function LoadNotesVesselSearch(IsMobile) {
    $("#cboNotesVesselSearch").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search vessel",
        minimumInputLength: 0,
        allowClear: true,
        ajax: {
            url: '/Dashboard/GetVesselLookup',
            dataType: 'json'
        },
        templateResult: formatNotesVesselResult,
        templateSelection: formatNotesVesselRepoSelection
    });

    $('#cboNotesVesselSearch').on('select2:open', function (e) {
        if (!IsMobile) {
            if (!IsNotedVesselHeaderAdded) {
                var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <tbody>\
		              </tbody>\
		              </table>';
                $('.select2-search').append(html);
                $('.select2-results').addClass('stock');
                $('.select2-results').addClass('vessel-drop');
                IsNotedVesselHeaderAdded = true;
            }
        }
    });

    function formatNotesVesselResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (!IsMobile) {
            if (result != undefined) {
                $result = $('<div class="select2-row">' +
                    '<div>' + result.vesselName + '</div>'
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

    function formatNotesVesselRepoSelection(repo) {       
        SelectedNotesVesselId = repo.vesselId || repo.id;
        return repo.text;
    }
}

export function ClearNoteDetailsSection() {
    $("#hdnNoteId").val('');
    $('#reminderDate').val('');
    $('#txtNoteTitle').val('');
    $('#txtNoteDescription').val('');
    $('#hdnNotesContextParams').val(null);
    $('#hdnNotesReferenceId').val(null)
    $('#cboNotesVesselSearch').val(null).trigger('change');
    $("#cboNotesVesselSearch").prop("disabled", false);
    AddClassIfAbsent('#divAreaDropdown', 'd-none');
    $("#selArea").prop("disabled", false);
    var selAreaSelection = document.getElementById('selArea');
    var selAreaSelectionLength = selAreaSelection.options.length;
    for (var i = selAreaSelectionLength - 1; i >= 0; i--) {
        selAreaSelection.options[i] = null;
    }
    SelectedNotesVesselId = '';
    ReminderDateTime = null;   
    $('#hdnNVReminderId').val(0);
    $('#hdnIsReminderIsExpired').val(false);
    $('#hdnIsReminderAlertDismissed').val(false);
    $('#hdnNoteStatus').val(1);   
    $("#divNotesMessageAttachemnts").empty();
    ResetNotesAttachment();
}

export function DeleteSelectedNote(noteId) {
    $.ajax({
        url: "/Notes/DeleteNote",
        type: "GET",
        dataType: "JSON",
        data: {
            "encryptedNoteId": noteId
        },
        success: function (data) {
            if (data != null) {
                if (data == true) {
                    ToastrAlert("success", "Note Deleted successfully.");
                    UpdateDashboardNoteList();
                    UpdateDashboardReminderAlertTab();
                    UpdateNotesTab();
                    let messageDetailsJSON = $("#MessageDetailsJSON").val();
                    if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
                        GetDiscussionNotesCount(messageDetailsJSON);
                    }
                }
                else {
                    ToastrAlert("error", "Something went wrong!");
                }
            }
            else {
                ToastrAlert("error", "Something went wrong!");
            }
        }
    });
}

export function OpenAddNotes(selectedCategory) {
    ClearNoteDetailsSection();
    ShowNoteDetailsSection();    
    $('#hdnNVReminderId').val(0);
    $('#hdnIsReminderIsExpired').val(false);
    $('#hdnIsReminderAlertDismissed').val(false);
    $('#hdnNoteStatus').val(1);    
    $('#spanNoteDetailHeader').text('Add Note');    
    ShowHideEditActions(false);
    if (!IsNullOrEmptyOrUndefinedLooseTyped(selectedCategory)) {
        GetAreaList(selectedCategory);
    }
    ClearNotesAttachment();
}

export function ClearNotesAttachment() {
    $("#divNotesMessageAttachemnts").empty();
}

export function SaveNote(isShowToastr) {
    let isValidate = NoteValidation(isShowToastr);

    if (isValidate) {   
        SaveNoteDetails();
    }
}

function NoteValidation(isShowToastr) {
    let isValidate = true;
    var NoteTitle = $('#txtNoteTitle').val().trim();
    var NoteDescription = $('#txtNoteDescription').val().trim();

    if (IsNullOrEmptyOrUndefinedLooseTyped(NoteTitle) && IsNullOrEmptyOrUndefinedLooseTyped(NoteDescription)) {
        isValidate = false;
    }

    if (!isValidate && isShowToastr) {
        ToastrAlert("validate", "Please enter note title/description.");
    }

    var ReminderDate = $('#reminderDate').val().trim();
    var date = Date.parse(ReminderDate);
    if (!IsNullOrEmptyOrUndefinedLooseTyped(ReminderDate) && isNaN(date)) {
        isValidate = false;
    }

    if (!isValidate && isShowToastr) {
        ToastrAlert("error", 'Please enter date in proper format.');
    }
   
    return isValidate;
}

export function CompleteNote(noteId, checkbox) {
    $.ajax({
        url: "/Notes/UpdateNoteStatus",
        type: "POST",
        dataType: "JSON",
        data: { "encryptedNoteId": noteId, "status": 2 },
        success: function (data) {
            if (data == true) {
                $('#hdnCurrentStatusPageNumber').val(1);
                ToastrAlert("success", "Note marked as Completed.");
                UpdateDashboardNoteList();
                UpdateNotesTab();
            }
            else {
                $(checkbox).prop('checked', false);
                ToastrAlert("error", "Something went wrong!");
            }
        },
        complete: function () {
            $(checkbox).prop('checked', false);
        }
    });
}

export function CompleteNoteFromEdit() {
    $.ajax({
        url: "/Notes/UpdateNoteStatus",
        type: "POST",
        dataType: "JSON",
        data: { "encryptedNoteId": $("#hdnNoteId").val(), "status": 2 },
        beforeSend: function (xhr) {            
            AddModelLoadingIndicator('#divNotesEditSection');
        },
        success: function (data) {
            if (data == true) {                
                ToastrAlert("success", "Note marked as Completed.");               
            }
            else {              
                ToastrAlert("error", "Something went wrong!");
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divNotesEditSection');
            ShowNoteListSection();
            $("#hdnCurrentStatusPageNumber").val(1);
            UpdateDashboardNoteList();
            UpdateDashboardReminderAlertTab();
            UpdateNotesTab();
            let messageDetailsJSON = $("#MessageDetailsJSON").val();
            if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
                GetDiscussionNotesCount(messageDetailsJSON);
            }
            ResetNotesAttachment();
        }
    });
}

export function MoveToCurrentFromEdit() {
    $.ajax({
        url: "/Notes/UpdateNoteStatus",
        type: "POST",
        dataType: "JSON",
        data: { "encryptedNoteId": $("#hdnNoteId").val(), "status": 1 },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('#divNotesEditSection');
        },
        success: function (data) {
            if (data == true) {
                ToastrAlert("success", "Note moved to current.");
            }
            else {
                ToastrAlert("error", "Something went wrong!");
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divNotesEditSection');
            ShowNoteListSection();
            $("#hdnCurrentStatusPageNumber").val(1);
            UpdateDashboardNoteList();
            UpdateDashboardReminderAlertTab();
            UpdateNotesTab();
            let messageDetailsJSON = $("#MessageDetailsJSON").val();
            if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
                GetDiscussionNotesCount(messageDetailsJSON);
            }
            ResetNotesAttachment();
        }
    });
}

export function DeleteNoteFromEdit() {
    $.ajax({
        url: "/Notes/DeleteNote",
        type: "GET",
        dataType: "JSON",
        data: {
            "encryptedNoteId": $("#hdnNoteId").val()
        },
        success: function (data) {
            if (data != null) {
                if (data == true) {
                    ToastrAlert("success", "Note Deleted successfully.");                   
                    let messageDetailsJSON = $("#MessageDetailsJSON").val();
                    if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
                        GetDiscussionNotesCount(messageDetailsJSON);
                    }
                }
                else {
                    ToastrAlert("error", "Something went wrong!");
                }
            }
            else {
                ToastrAlert("error", "Something went wrong!");
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divNotesEditSection');
            ShowNoteListSection();
            //$("#hdnCurrentStatusPageNumber").val(1);
           // UpdateDashboardNoteList();
            UpdateDashboardReminderAlertTab();
            UpdateNotesTab();
            ResetNotesAttachment();            
        }
    });
}

export function MoveToCurrent(noteId, checkbox) {
    $.ajax({
        url: "/Notes/UpdateNoteStatus",
        type: "POST",
        dataType: "JSON",
        data: { "encryptedNoteId": noteId, "status": 1 },
        success: function (data) {
            if (data == true) {
                $('#hdnCurrentStatusPageNumber').val(1);
                ToastrAlert("success", "Note moved to current.");
                UpdateDashboardNoteList();
                UpdateNotesTab();
            }
            else {
                $(checkbox).prop('checked', true);
                ToastrAlert("error", "Something went wrong!");
            }
        }
    });
}

function GetNotesModuleIcon(categoryId) {

    if (categoryId == MessasgeCategory_PurchaseOrder) {
        return '<img src="/images/notes-procurement.png" />';
    }
    else if (categoryId == MessasgeCategory_Inspection || categoryId == MessasgeCategory_InspectionFinding) {
        return '<img src="/images/notes-inspection.png" />';
    }
    else if (categoryId == MessasgeCategory_Certificate) {
        return '<img src="/images/notes-certificate.png" />';
    }
    else if (categoryId == MessasgeCategory_Crew) {
        return '<img src="/images/notes-crew.png" />';
    }
    else if (categoryId == MessasgeCategory_HazOcc) {
        return '<img src="/images/notes-hazoc.png" />';
    }
    else if (categoryId == MessasgeCategory_SeaPassage || categoryId == MessasgeCategory_PortCallLocationEvent) {
        return '<img src="/images/notes-commercial.png" />';
    }
    else if (categoryId == MessasgeCategory_DefectWorkOrder) {
        return '<img src="/images/notes-defect.png" />';
    }
    else if (categoryId == MessasgeCategory_PlannedMaintenance) {
        return '<img src="/images/notes-pms.png" />';
    } 
    else if (categoryId == MessasgeCategory_JSA) {
        return '<img src="/images/notes-jsa.png" />';
    }
    else {
        return '';
    }
}

export function NavigateToNoteRecordDetails(encryptedNoteId, encryptedVesselId) {
    $.ajax({
        url: "/Notes/NavigateToNoteRecordDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedNoteId": encryptedNoteId,
            "encryptedVesselId": encryptedVesselId
        },
        success: function (data) {
            window.location.href = window.location.protocol + "//" + window.location.host + '/' + data;
        }
    });
}

function HideNoteFilter() {
    $(".search-filter-n").hide();
}

export function cb(start) {
    if (start.format("DD MMM YYYY HH:mm") != 'Invalid date') {
        ReminderDateTime = start.format("DD MMM YYYY HH:mm");
        $("#reminderDate").val(ReminderDateTime);
    }
}

export function CancelReminder() {
    ReminderDateTime = null;
}

function GetNotesListTab(request, successCallback) {
    $.ajax({
        url: "/Notes/GetNotesList",
        type: "Post",
        dataType: "JSON",
        data: request,
        success: function (data) {
            if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                successCallback(data);
            }
        },
        complete: function () {
            $(".notes-sidebar-open .collapse-date").hide();
            RemoveModelLoadingIndicator('#divNotesLoader');
            $('.notesListDropdown').dropdown();
        }
    });
}

function CreateNoteTemplateParameterised(data, container) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let note = data[i];
            var newRow = NoteRowCreated(note);
            AppendAttachments(newRow, note);
            $(container).append(newRow);
        }
    }
}

export function LoadCurrentTabNotes(isScrolled) {
    var data = {
        'SearchText': $("#notesSearchText").val(),
        'StatusIds': 1, //current,
        'IsPageScroled': isScrolled,
        'PageNumber': parseInt($('#hdnCurrentStatusPageNumber').val()),
        'MessageDetailsJSON': $("#hdnNotesMessageDetails").val()
    }

    RemoveClassIfPresent('#spanCurrentNotesCount', 'd-none');
    AddClassIfAbsent('#spanCompletedNotesCount', 'd-none');
    AddClassIfAbsent('#spanAllNotesCount', 'd-none');

    if (!isScrolled) {
        AddModelLoadingIndicator('#divNotesLoader');
        $('#currentnotessection').empty();
    }

    GetNotesListTab(data, function (data) {
        CreateNoteTemplateParameterised(data.data, '#currentnotessection');
        $('#hdnCurrentHasNextPage').val(data.hasNextScroll);
        if (data.hasNextScroll) {
            $('#spanCurrentNotesCount').text('(' + data.totalCount + ')');
        }
    });
}

export function LoadCompletedTabNotes(isScrolled) {
    var data = {
        'SearchText': $("#notesSearchText").val(),
        'StatusIds': 2, //completed,
        'IsPageScroled': isScrolled,
        'PageNumber': parseInt($('#hdnCompletedStatusPageNumber').val()),
        'MessageDetailsJSON': $("#hdnNotesMessageDetails").val()
    }

    RemoveClassIfPresent('#spanCompletedNotesCount', 'd-none');
    AddClassIfAbsent('#spanCurrentNotesCount', 'd-none');
    AddClassIfAbsent('#spanAllNotesCount', 'd-none');

    if (!isScrolled) {
        AddModelLoadingIndicator('#divNotesLoader');
        $('#completednotessection').empty();
    }

    GetNotesListTab(data, function (data) {
        CreateNoteTemplateParameterised(data.data, '#completednotessection');
        $('#hdnCompletedHasNextPage').val(data.hasNextScroll);
        $('#spanCompletedNotesCount').text('(' + data.totalCount + ')');
    });
}

export function LoadAllTabNotes(isScrolled) {
    var data = {
        'SearchText': $("#notesSearchText").val(),
        'StatusIds': '1,2', //completed,
        'IsPageScroled': isScrolled,
        'PageNumber': parseInt($('#hdnAllStatusPageNumber').val()),
        'MessageDetailsJSON': $("#hdnNotesMessageDetails").val()
    }

    RemoveClassIfPresent('#spanAllNotesCount', 'd-none');
    AddClassIfAbsent('#spanCurrentNotesCount', 'd-none');
    AddClassIfAbsent('#spanCompletedNotesCount', 'd-none');

    if (!isScrolled) {
        AddModelLoadingIndicator('#divNotesLoader');
        $('#allnotessection').empty();
    }

    GetNotesListTab(data, function (data) {
        CreateNoteTemplateParameterised(data.data, '#allnotessection');
        $('#hdnAllHasNextPage').val(data.hasNextScroll);
        $('#spanAllNotesCount').text('(' + data.totalCount + ')');
    });
}

export function UpdateNotesTab() {
    if ($('.search-filter-n').is(':visible')) {
        let activeTab = $('.search-filter-n').find('div.active').attr('id');
        if (activeTab == 'tabs1') {
            $('#hdnAllStatusPageNumber').val(1);
            LoadAllTabNotes(false);
        }
        if (activeTab == 'tabs2') {
            $('#hdnCurrentStatusPageNumber').val(1);
            LoadCurrentTabNotes(false);
        }
        else if (activeTab == 'tabs3') {
            $('#hdnCompletedStatusPageNumber').val(1);
            LoadCompletedTabNotes(false);
        }
    }
}

function ShortNoteTitle(NoteTitle) {
    if (!IsNullOrEmptyOrUndefinedLooseTyped(NoteTitle)) {
        NoteTitle = NoteTitle.length > maxLengthNotesTitle ? NoteTitle.substring(0, maxLengthNotesTitle) + '...' : NoteTitle;
        return NoteTitle;
    }

    return "";
}

export function CreateReminderAlertOnDashboard() {
    $.ajax({
        url: "/Dashboard/GetReminderAlert",
        type: "Get",
        dataType: "JSON",
        beforeSend: function (xhr) {
            $('#reminderAlertsTab').empty();
            AddModelLoadingIndicator('#divReminderAlertTab');
        },
        success: function (data) {
            if (data != null && data != '') {
                CreateReminderList(data);
            }            
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divReminderAlertTab');
        }
    });
}

export function DeleteNoteAttachment(ele) {
    let ettId = $(ele).parent().data('ettid');
    if ($('.add-note').is(':visible')) {
        var filteredFiles = window.NotesAttachedFiles.filter(x => {
            return x.ettId != ettId;
        });
        window.NotesAttachedFiles = filteredFiles;
        $(ele).parent().remove();
    }
}

export function UploadAttachmentEvent() {
    $("#inputNotesMessageAttachments").on('change', function sub(obj) {
        var input = document.getElementById('inputNotesMessageAttachments');
        var newInput = [];      
        var megaBytes = 0;
        if (input.files != null) {
            for (let i = 0; i < input.files.length; ++i) {
                megaBytes = ConvertByteToMegaBytes(input.files[i].size);
                if (megaBytes < 2) {
                    newInput.push(input.files[i]);
                }
                else {
                    ToastrAlert("validate", "File should not be greater than 2 MB.");
                }
            }                      
        }
               
        var docsCount = window.NotesAttachedFiles.length + newInput.length;
        if (CheckFilesAttachedCountForNotes(docsCount)) {
            for (var i = 0; i < newInput.length; ++i) {
                UploadNotesAttachmentFile(newInput[i], GetSequenceNumberFromView(), null, "#divNotesMessageAttachemnts", null);
            }
        }
    });
}

function GetSequenceNumberFromView() {
    let children = $("#divNotesMessageAttachemnts").find('.attached-files');
    if (children.length > 0) {
        let seq = $(children).last().data("sequence");
        return seq + 1;
    } else {
        return 1;
    }
}

function ResetNotesAttachment() {
    window.NotesAttachedFiles.splice(0);
}

export function CheckFilesAttachedCountForNotes(docsCount) {
    if (docsCount > FileAttachmentLimit) {
        ToastrAlert("validate", FileAttachmentLimitErrorMessage);
        return false;
    } else {
        return true;
    }
}

function UploadNotesAttachmentFile(file, sequence, attachedTo, divHtmlElement, onSuccessCallBack) {    
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        let fileName = file.name;
        let fileBase64String = reader.result.split('base64,')[1];

        var fileDetails = {
            fileName: fileName,
            fileBase64String: fileBase64String,
            sequence: sequence
        };

        $.ajax({
            url: "/Notes/UploadFile",
            type: "POST",
            dataType: "JSON",
            data: fileDetails,
            beforeSend: function (xhr) {
                AddModelLoadingIndicator(divHtmlElement);
            },
            success: function (data) {
                if (!data.response) {
                    ToastrAlert("validate", "Failed to upload " + fileName + ".");
                }
                else {
                    var row = AttachmentTemplateUtility(GetAttachmentIconUtility(data.data.fileExtension), fileName, sequence, attachedTo, data.data.ettId);
                    $(divHtmlElement).append(row);
                    window.NotesAttachedFiles.push(data.data);
                    RemoveModelLoadingIndicator(divHtmlElement);
                    if (!IsNullOrEmptyOrUndefined(onSuccessCallBack)) {
                        onSuccessCallBack();
                    }
                }
            },
            complete: function () {
                RemoveModelLoadingIndicator(divHtmlElement);
            }
        });
    };
    reader.onerror = function (error) {
        ToastrAlert("error", error);
    };
}

export function ReminderDismissed(encryptedReminderId) {
    $.ajax({
        url: "/Notes/ReminderAlertDismissed",
        type: "POST",
        dataType: "JSON",
        data: { "encryptedReminderId": encryptedReminderId },
        success: function (data) {
            if (data == true) {
                CreateReminderAlertOnDashboard();
                ToastrAlert("success", "Reminder has been dismissed");                
            }
            else {                
                ToastrAlert("error", "Something went wrong!");
            }
        }
    });
}

export function DownloadAttachment(input) {
    $.ajax({
        url: "/Notes/DownloadAttachment",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": input
        },
        success: function (data) {
            if (data != null) {
                if (data.bytes != null) {
                    var array = base64ToArrayBuffer(data.bytes);
                    saveByteArray(data.filename, array, data.fileType);
                    ToastrAlert("success", "File Downloaded \"" + data.filename + "\"");
                } else {
                    if (data.status != null) {
                        ToastrAlert("error", data.status + " for \"" + data.filename + "\"");
                    } else {
                        ToastrAlert("validate", "File Not Found for \"" + data.filename + "\"");
                    }
                }
            }
        }
    });
}

export function DeleteDocument(ettId) {
    var request = {
        ettId: ettId
    }
    $.ajax({
        url: "/Notes/DeleteAttachment",
        type: "POST",
        dataType: "JSON",
        data: request,
        success: function (data) {
        }
    });
}

function ShowHideEditActions(isVisible) {
    if (isVisible) {
        RemoveClassIfPresent('#divNotesEditActions', 'd-none');
    }
    else {
        AddClassIfAbsent('#divNotesEditActions', 'd-none');
    }
}