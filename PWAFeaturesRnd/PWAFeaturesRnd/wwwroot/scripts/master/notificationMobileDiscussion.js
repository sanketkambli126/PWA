
import "select2/dist/js/select2.full.js";
require('bootstrap');

import { AjaxError, GetCookie, GetStringNullOrWhiteSpace, IsNullOrEmptyOrUndefined, RemoveClassIfPresent, AddClassIfAbsent, AddModelLoadingIndicator, RemoveModelLoadingIndicator, BackButton, IsNullOrEmptyOrUndefinedLooseTyped } from "../common/utilities.js";
import { NotificationMobileDiscussionKey, MobileScreenSize} from "../common/constants";
import { InitialiseVesselSearch, GetAreaList, UploadFile, ValidationBeforeCreateChannel, ClearMoreUserPopUp, SelectedParticipantTemplate, GetVesselResponsibilities, InitialiseSearchMoreUserDropdown, setHeight, CheckFilesAttachedCount, DisplaySearchMoreUserSection, UpdateSelectedVesselDropdown, GetChannelDetailDraft, GetNotificationObject, ResetDraftAttachedFilesVariables, SetSelectedFileCount, CloseVChatApplication } from "../common/notificationutilities.js";

var IsMobile = true;
var headerIsAppend = false;
var SelectedVesselName = "";
var SelectedVesselId = "";
var PreSelectedUsers = [];
var FilesAttached = 0;
var AttachedFiles = [];
var SelectedUser = [];
var IsDraftSelectEventOccured = false;

//this code is duplicated from notfication.js
$(document).on('click', '.attachment-close', function () {
    let sequence = $(this).parent().data('sequence');
    updateAttachedFilesArray();
    var filteredFiles = AttachedFiles.filter(x => {
        return x.sequence != sequence;
    });
    AttachedFiles = filteredFiles;
    $(this).parent().remove();
});

//this code is duplicated from notfication.js
$(document).on('click', '.removeSelectedParticipants', function () {
    let anchorId = $(this).attr('id');
    let userId = anchorId.trim();
    var values = $('#cboSearchMoreUsers').val();
    $(".addParticipants#" + anchorId).prop('disabled', false);
    $(".addParticipants#" + anchorId).find('img').prop('src', '/images/add-partici.png');

    if (values) {
        var i = values.indexOf(userId);
        if (i >= 0) {
            values.splice(i, 1);
            $('#cboSearchMoreUsers').val(values).change();
        }
    }
    var filteredSelectedUser = SelectedUser.filter(x => {
        return x.id != userId;
    });
    SelectedUser = filteredSelectedUser;

    var selectedParticipantsCount = parseInt($("#spanSelectedParticipantsCount").text());
    $("#spanSelectedParticipantsCount").text(--selectedParticipantsCount);
    $(this).parent().remove();
});

//this code is duplicated from notfication.js
$(document).on('click', '.addParticipants', function () {
    let anchorId = $(this).attr('id');
    var sibling = $(this).find('.selectedParticipant').clone(true);
    RemoveClassIfPresent(sibling.find('.remove'), 'd-none');
    $(this).prop('disabled', true);
    $(this).find('img').prop('src', '/images/add-partici-disabled.png');

    let userData = $(this).find('.participantsRow')

    let userName = userData.data('username');
    let userShortName = userData.data('usershortname');
    let userRoleDescription = userData.data('description');

    let localAddedUser = { userShortName: userShortName.trim(), text: userName.trim(), id: anchorId.trim(), description: userRoleDescription.trim() }
    let isUserAdded = SelectedUser.some(x => x.id === anchorId);
    if (!isUserAdded) {
        SelectedUser.push(localAddedUser);
        $('#divSelectedParticipants').prepend(sibling);
        var selectedParticipantsCount = parseInt($("#spanSelectedParticipantsCount").text());
        $("#spanSelectedParticipantsCount").text(++selectedParticipantsCount);
    }
});

function BackButtonNotificationMobileDiscussionAction(keyName, beforeCompleteCallback, afterCompleteCallback) {
    $.ajax({
        url: "/Notification/GetSourceURLForNotification",
        type: "POST",
        dataType: "JSON",
        data: {
            "sessionDetails": GetStringNullOrWhiteSpace($('#hdnSessionStorageDetails').val())
        },
        beforeSend: function (xhr) {
            if (beforeCompleteCallback != null && beforeCompleteCallback != 'undefined') {
                beforeCompleteCallback();
            }
        },
        success: function (data) {
            if (data != null) {
                if (isDraft() && $('#IsAlreadyTriggerEvent').val() == '0') {
                    CreateChannel(true, function () {
                        if ($('#hdnIsFromOtherSource').val() == true || $('#hdnIsFromOtherSource').val() == 'true' || $('#hdnIsFromOtherSource').val() == 'True') {
                            sessionStorage.removeItem(keyName);
                            chatmobileBackNotification();
                        }
                        else {
                            sessionStorage.removeItem(keyName);
                            window.location.replace(data);
                        }                        
                    });
                } else {
                    if ($('#hdnIsFromOtherSource').val() == true || $('#hdnIsFromOtherSource').val() == 'true' || $('#hdnIsFromOtherSource').val() == 'True') {
                        sessionStorage.removeItem(keyName);
                        chatmobileBackNotification();
                    }
                    else {
                        sessionStorage.removeItem(keyName);
                        window.location.replace(data);
                    }
                }
            }
        },
        complete: function () {
            if (afterCompleteCallback != null && afterCompleteCallback != 'undefined') {
                afterCompleteCallback();
            }
        },
        error: function (xhr, status, error) {
            //var data = JSON.parse(xhr.responseText);
            ErrorLog(xhr, status, error);
        }
    });
}

//created a copy from utilities and added logic for creating drafts
function BackButtonNotificationMobileDiscussion(keyName, beforeCompleteCallback, afterCompleteCallback) {
    $('.back').click(function () {
        BackButtonNotificationMobileDiscussionAction(keyName, beforeCompleteCallback, afterCompleteCallback);
    });
}

function isDraft() {
    let subjectText = $('#txtSubject').val();
    let messageText = $('#txtMessage').val();
    let participants = $('#participantdropdown').val();
    let vesselSelected = $('#cboNotificationVesselSearch').val();

    let isValidate = false;
    if (!IsNullOrEmptyOrUndefined(subjectText)) {
        isValidate = true;
    }
    else if ((messageText != null && messageText.trim().length != 0)) {
        isValidate = true;
    }
    else if (participants.length != 0) {
        isValidate = true;
    }
    else if (vesselSelected.length != 0) {
        isValidate = true;
    }

    return isValidate;
}

$(document).ready(function () {
    AjaxError();

    if (sessionStorage.getItem(NotificationMobileDiscussionKey) != null) {
        if ($('#hdnIsFilterChange').val() == "True" || $('#hdnIsFilterChange').val() == "true" || $('#hdnIsFilterChange').val() == true) {
            $('#hdnSessionStorageDetails').val(sessionStorage.getItem(NotificationMobileDiscussionKey));            
        }
        else {
            //fill filter fields from sessionStorage
            sessionStorage.setItem(NotificationMobileDiscussionKey, $('#hdnSessionStorageDetails').val());
        }
    }
    else {
        //fill sessionStorage from fields
        sessionStorage.setItem(NotificationMobileDiscussionKey, $('#hdnSessionStorageDetails').val());
    }
    BackButtonNotificationMobileDiscussion(NotificationMobileDiscussionKey, function () {
        AddModelLoadingIndicator('.notification-mobile-info');
    }, function () {
        RemoveModelLoadingIndicator('.notification-mobile-info');
    });

    InitialiseVesselSearch("#cboNotificationVesselSearch", formatResult, formatRepoSelection);

    if (GetCookie('NotificationApplicationId') != '2') {
        RemoveClassIfPresent('.backclose', 'd-none');
        $('.app-header').css("height", "60px");
    }
    else {
        $('body').addClass("hideleftmenuheader");
    }
    $('.backclose').click(function () {
        sessionStorage.removeItem(NotificationMobileDiscussionKey);
        CloseVChatApplication();
    });

    //this code is duplicated from notfication.js
    $('#cboNotificationVesselSearch').on('select2:open', function (e) {
        if (!IsMobile) {
            if (!headerIsAppend) {
                var html = '<table class="table" style="margin-top: 0px;margin-bottom: 0px; width: 99%">\
		              <tbody>\
		              </tbody>\
		              </table>';
                $('.select2-search').append(html);
                $('.select2-results').addClass('stock');

                headerIsAppend = true;
            }
        }
    });

    AddModelLoadingIndicator('.vessel-list-patch');
    GetAreaList($('#CategoryId').val(), 'cboAreaSelection', function () {
        RemoveModelLoadingIndicator('.vessel-list-patch');
    });

    if (!IsNullOrEmptyOrUndefinedLooseTyped($('#CategoryId').val())) {
        RemoveClassIfPresent('#divAreaDropdown', 'd-none');
    }

    $('#txtSubject').focusout(function () {
        if ($(this).val().length > 0) {
            $("#txtSubject").css("border", "2px solid #e5e5e5");
        }
    });

    $('#txtSubject').keyup(function () {
        if ($(this).val().length > 0) {
            $("#txtSubject").css("border", "2px solid #e5e5e5");
        }
    });

    $('#txtMessage').focusout(function () {
        if ($(this).val().length > 0) {
            $("#txtMessage").css("border", "2px solid #e5e5e5");
        }
    });
    $('#txtMessage').keyup(function () {
        if ($(this).val().length > 0) {
            $("#txtMessage").css("border", "2px solid #e5e5e5");
        }
    });

    InitialiseParticipantDropdown();

    if (!IsNullOrEmptyOrUndefined($("#replyPrivateParticipant_Id").val()) && $("#replyPrivateParticipant_Id").val() != undefined) {
        let userId = $("#replyPrivateParticipant_Id").val();
        let userShortName = $("#replyPrivateParticipant_UserShortName").val();
        let text = $("#replyPrivateParticipant_Text").val();
        LoadParticipantFromViewModel(userId, userShortName, text);
    }

    //this code is duplicated from notfication.js
    $("#inputChannelAttachments").on('change', function sub(obj) {
        var input = document.getElementById('inputChannelAttachments');
        updateAttachedFilesArray();
        if (FilesAttached == 0) {
            FilesAttached = AttachedFiles.length;
        }
        var docsCount = AttachedFiles.length + input.files.length;
        if (CheckFilesAttachedCount(docsCount)) {
            SetSelectedFileCount(input.files.length);
            for (var i = 0; i < input.files.length; ++i) {
                let fileSequenceNumber = FilesAttached + i + 1;
                UploadFile(input.files[i], fileSequenceNumber, "Channel", "#divNewChannelAttachemnts", AttachedFiles, null, '');
            }
            FilesAttached = FilesAttached + input.files.length;
        }
    });

    $("#btn-discussion").click(function () {
        CreateChannel(false);
    });

    $("#btn-SaveAsDraft").click(function () {
        CreateChannel(true);
    });

    $("#cancel").click(function () {
        $('#IsAlreadyTriggerEvent').val('1');
        $('.back')[0].click();
    });

    //this code is duplicated from notfication.js
    $("#btnSearchParticipants").click(function () {
        ClearMoreUserPopUp(SelectedUser);
        OpenSearchMoreUsers();
        //Add to message from user control
        $("#btnAddToMessage").off('click');
        $('#btnAddToMessage').click(function () {
            let existingUserArray = $('#participantdropdown').val();
            PreSelectedUsers = PreSelectedUsers.filter(({ id }) => existingUserArray.includes(id));
            var mergedArray = $.merge(SelectedUser, PreSelectedUsers);
            var finalArray = [];
            mergedArray.filter(function (item) {
                var i = finalArray.findIndex(x => (x.id == item.id));
                if (i <= -1) {
                    finalArray.push(item);
                }
                return null;
            });

            $('#participantdropdown').val(null).trigger('change');

            finalArray.forEach(function (user) {
                var option = new Option(user.text, user.id, true, true);
                $(option).data('raw', user);
                $('#participantdropdown').append(option).trigger('change');
            });

            $("#searchparticipant").modal('hide');
            $(".modal-backdrop").remove();
        });
    });

    InitialiseSearchMoreUserDropdown(SelectedVesselId);

    //this code is duplicated from notfication.js
    $("#cboSearchMoreUsers").on('select2:select', function (selection) {
        //add user to array
        IsDraftSelectEventOccured = true;
        let isUserAdded = SelectedUser.some(x => x.id === selection.params.data.id);
        if (!isUserAdded) {
            let localAddedUser = { userShortName: selection.params.data.userShortName, text: selection.params.data.text, id: selection.params.data.id, description: selection.params.data.description }
            SelectedUser.push(localAddedUser);
            var row = SelectedParticipantTemplate(selection.params.data, true);
            $('#divSelectedParticipants').prepend(row);
            var selectedParticipantsCount = parseInt($("#spanSelectedParticipantsCount").text());
            $("#spanSelectedParticipantsCount").text(++selectedParticipantsCount);
        }
    });

    $('#cboSearchMoreUsers').on('select2:selecting', function (e) {
        IsDraftSelectEventOccured = true;
    });

    const textarea = document.getElementById("txtMessage");

    textarea.addEventListener("input", (e) => {
        setHeight(e.target);
    });

    setHeight(textarea);

    var h1 = ($(".app-main").outerHeight());
    var h2 = ($(".create-message-mobile").outerHeight());
    $(".heightfix").css({
        "height": h1 - h2 - 15
    });

    let vesselId = $('#VesselId').val() || $('#SelectedVesselId').val()
    let vesselName = $('#VesselName').val() || $('#SelectedVesselName').val()

    if (!IsNullOrEmptyOrUndefinedLooseTyped(vesselId)) {
        $("#cboNotificationVesselSearch").prop("disabled", true);
    }

    UpdateSelectedVesselDropdown(vesselId, vesselName, '#cboNotificationVesselSearch', '#btnSearchParticipants');
    if (!IsNullOrEmptyOrUndefinedLooseTyped($('#DefaultMessage').val())) {
        let defaultMessage = $('#DefaultMessage').val();
        $('#txtSubject').val(defaultMessage);
    }

    var isSaveAsDraft = $('#IsSaveAsDraft').val();
    if (isSaveAsDraft == "true" || isSaveAsDraft == "True" || isSaveAsDraft == true) {
        var IsGeneralCat = $('#IsGeneralCat').val();
        var CategoryId = $('#CategoryId').val();

        if (CategoryId > 0 && (IsGeneralCat == "false" || IsGeneralCat == "False")) {
            RemoveClassIfPresent('.divAreaDropdowncls', 'd-none');
            $("#cboNotificationVesselSearch").prop("disabled", true);
        }
        else {
            AddClassIfAbsent('.divAreaDropdowncls', 'd-none');
            $("#cboNotificationVesselSearch").prop("disabled", false);
        }

        var ChannelId = $('#ChannelId').val();
        GetChannelDetailDraft(ChannelId,'.notification-mobile-info','');
    }

    $('#cboNotificationVesselSearch, #participantdropdown').on('select2:selecting', function (e) {
        IsDraftSelectEventOccured = true;
        console.log("On select2 ", IsDraftSelectEventOccured);
    });
});

window.backAction = function () {
    BackFunction();
}
function BackFunction() {

    BackButtonNotificationMobileDiscussionAction(NotificationMobileDiscussionKey, function () {
        AddModelLoadingIndicator('.notification-mobile-info');
    }, function () {
        RemoveModelLoadingIndicator('.notification-mobile-info');
    });
}
window.chatScrollLastRow = function () {    
}
//this code is duplicated from notfication.js
function formatRepoSelection(repo) {
    SelectedVesselName = repo.vesselName || repo.text;
    SelectedVesselId = repo.vesselId || repo.id;
    let isVesselSelected = IsNullOrEmptyOrUndefinedLooseTyped(SelectedVesselId) ? false : true;
    DisplaySearchMoreUserSection(isVesselSelected, '#btnSearchParticipants');

    var isSaveAsDraft = $('#IsSaveAsDraft').val();
    if (isSaveAsDraft == "false" || isSaveAsDraft == "False" || isSaveAsDraft == false) {
        let participantsArray = $('#participantdropdown').select2('data');
        if (typeof participantsArray !== 'undefined' && participantsArray.length > 0) {
            $("#notificationVesselChangeConfirmationModal").modal('show');
            $('#yesNotificationVesselChangeConfirmationModal').off();
            $('#yesNotificationVesselChangeConfirmationModal').on('click', function () {
                $("#notificationVesselChangeConfirmationModal").modal('hide');
            });

            $('#noNotificationVesselChangeConfirmationModal').off();
            $('#noNotificationVesselChangeConfirmationModal').on('click', function () {
                $('#participantdropdown').val(null).trigger('change');
                PreSelectedUsers.length = 0;
                $("#notificationVesselChangeConfirmationModal").modal('hide');
            });
        }
    }
    return repo.text;
}

//this code is duplicated from notfication.js
function formatResult(result) {
    if (result.loading)
        return "Searching...";

    var $result;

    if (IsMobile) {
        if (result != undefined) {
            $result = $('<div class="row select2-row">' +
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

function InitialiseParticipantDropdown() {
    $("#participantdropdown").select2({
        theme: "bootstrap4",
        placeholder: "Search users by name or role",
        minimumInputLength: 0,
        dropdownCssClass: 'dropdown-outline participant-drop-deisgn',
        ajax: {
            url: '/Notification/GetParticipantsListPaged',
            dataType: 'json',
            data: function (params) {
                let participantsArray = $('#participantdropdown').select2('data');
                var participantsList = participantsArray.map(item => {
                    return item.id;
                });
                return {
                    term: params.term,
                    page: params.page || 1,
                    vesselId: SelectedVesselId,
                    tempSelectedUsers: String(participantsList)
                };
            },
        },
        templateResult: participantsResult,
        templateSelection: participantsRepoSelection
    });
}

//this code is duplicated from notfication.js
function participantsResult(result) {
    if (result.loading)
        return "Searching...";

    var $result;
    if (result != undefined) {
        $result = $('<table class="table table-bordered p-0 m-0">\
							<tbody>\
						 	    <tr>\
                                    <td rowspan="2" class="participant-design">\
                                        <div class="participantsRow">\
                                            <div class="initialname green-name particiapntsCol">' + result.userShortName + '</div>\
                                            <div class="particiapntsCol">\
                                                <div class="name">' + result.text + '</div>\
                                                <div class="type">' + result.description + '</div>\
                                            </div>\
                                        </div>\
                                    </td>\
						 	    </tr>\
						    </tbody>\
						 </table>');
    }

    return $result;
}

//this code is duplicated from notfication.js
function participantsRepoSelection(repo) {
    var raw = $(repo.element).data('raw');
    $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", '#e5e5e5');
    var $result;
    $result = $('<div class="participantsRow">\
                        <div class="initialname green-name particiapntsCol">' + repo.userShortName + '</div>\
                        <div class="particiapntsCol">\
                            <div class="name">' + repo.text + '</div>\
                            <div class="type">' + repo.description + '</div>\
                        </div>\
                    </div>');

    $result.find('.initialname').text(repo.userShortName || raw.userShortName)
    $result.find('.type').text(repo.description || raw.description)

    let userName = repo.text;
    let userShortName = repo.userShortName || raw.userShortName;
    let userRoleDescription = repo.description || raw.description;
    let userId = repo.id;
    let user = { userShortName: userShortName.trim(), text: userName.trim(), id: userId.trim(), description: userRoleDescription.trim() }
    PreSelectedUsers.push(user);
    return $result;
}

function CreateChannel(isSaveAsDraft, successCallback) {
    let isValidate = null;
    if (!isSaveAsDraft) {
        isValidate = ValidationBeforeCreateChannel('#txtSubject', '#txtMessage', '#participantdropdown');
    } else {
        isValidate = true;
    }

    if (isValidate) {
        updateAttachedFilesArray();

        let participantsArray = $('#participantdropdown').select2('data');
        var participantsList = participantsArray.map(item => {
            return {
                id: item.id,
                text: item.text,
            }
        });

        var InitialMsg = $('#txtMessage').val().trim();
        var Title = $('#txtSubject').val().trim();
        Title = IsNullOrEmptyOrUndefinedLooseTyped(Title) ? InitialMsg.substring(0, 100) : Title;

        let request = {
            "InitialMsg": InitialMsg,
            "Title": Title,
            "Subscribers": participantsList,
            "VesselId": SelectedVesselId,
            "AttachmentList": AttachedFiles,
            "CategoryId": $('#cboAreaSelection').val(),
            "ContextPaylod": $('#ContextPayload').val(),
            "IsSaveAsDraft": isSaveAsDraft,
            "ChannelId": IsNullOrEmptyOrUndefinedLooseTyped($('#ChannelId').val()) ? 0 : $('#ChannelId').val()
        }

        $.ajax({
            url: "/Notification/CreateNotificationChannel",
            type: "POST",
            dataType: "JSON",
            data: request,
            beforeSend: function (xhr) {
                AddModelLoadingIndicator('.notification-mobile-info');
            },
            success: function (data) {
                if (data != 0) {
                    AttachedFiles = [];
                    if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                        successCallback();
                    } else {
                        $('#IsAlreadyTriggerEvent').val('1');
                        $('.back')[0].click();
                    }
                }
            },
            complete: function () {
                RemoveModelLoadingIndicator(".notification-mobile-info");
            }
        });
    }
}

//this code is duplicated from notfication.js
function OpenSearchMoreUsers() {
    $('#spanResponsibleVesselName').text(SelectedVesselName);
    GetVesselResponsibilities(SelectedVesselId);
}

function LoadParticipantFromViewModel(userId, userShortName, text) {

    $.ajax({
        url: "/Notification/GetUserPrimaryRole",
        type: "POST",
        dataType: "JSON",
        data: {
            userIds: userId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('.notification-mobile-info');
        },
        success: function (data) {
            var roleName;
            if (data != null) {
                roleName = data.find(x => x.userId == userId).roleName;
            }
            var obj = {
                id: userId,
                description: roleName,
                userShortName: userShortName,
                text: text
            }
            var option = new Option(obj.text, obj.id, true, true);
            $(option).data('raw', obj);
            $('#participantdropdown').append(option).trigger('change');
        },
        complete: function () {
            RemoveModelLoadingIndicator('.notification-mobile-info');
        }
    });
}

function updateAttachedFilesArray() {
    var objAttachedFiles = GetNotificationObject();

    if (objAttachedFiles.AttachedFiles.length > 0) {
        AttachedFiles = objAttachedFiles.AttachedFiles.slice();
        ResetDraftAttachedFilesVariables("");
    }
}

function chatmobileBackNotification() {
    window.parent.chatmobileBack();
}