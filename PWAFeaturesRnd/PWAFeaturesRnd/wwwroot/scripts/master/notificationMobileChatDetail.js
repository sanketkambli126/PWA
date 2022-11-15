require('bootstrap');
import { ChannelMessage, GetReadParticipants, SendMessage, setHeight, DownloadAttachment, UploadFile, CheckFilesAttachedCount, ScrollToLastRow, DeleteDocument, DeleteAllDocument, GetRequestPropertiesFromDiv, CallSendMessage, GetCurrentUserDetails, SetSelectedFileCount, SignalRConnect, DeleteMessage, EditChannelMessage, GetAttachedFilesVariables, ResetDraftAttachedFilesVariables, GetAttachmentViewForEditMessage, CloseVChatApplication, BackButtonAction, BackButtonForNotification, GetChannelRecordDetails, NavigateToChannelRecordDetails } from "../common/notificationutilities.js";

import { AjaxError, ErrorLog, IsNullOrEmptyOrUndefined, RemoveClassIfPresent, AddClassIfAbsent, GetStringNullOrWhiteSpace, AddModelLoadingIndicator, RemoveModelLoadingIndicator, IsNullOrEmptyOrUndefinedLooseTyped, GetCookie } from "../common/utilities.js";
import { NotificationMobileChatDetailKey, NotificationPageKey, DeleteMessageTemplate, MobileScreenSize } from "../common/constants";

var participantsCount = parseInt($("#ParticipantCount").val());
var FilesAttached = 0;
var AttachedFiles = [];
var AttachedFilesCol = new Map();
var EditMsg = { EditMessageId: 0 };

$(document).on('click', '.editMessage', function () {
    let parent = $(this).parents('li')[0];
    let desc = $(parent).find('.chat-desc')
    let messageTag = $(desc).find('p')[0];
    let messageId = $(desc).data('msgid');
    let editMessageDiv = $(desc).find('.edit-message')[0];

    if (IsNullOrEmptyOrUndefinedLooseTyped(editMessageDiv)) {
        AddClassIfAbsent(messageTag, 'd-none');
        AddClassIfAbsent(".attachmentView" + messageId, 'd-none');
        EditChannelMessage(messageId, desc);
    }
});

$(document).on('click', '#editMessageCancel', function () {
    let parent = $(this).parents('li')[0];
    let desc = $(parent).find('.chat-desc')[0];
    let messageTag = $(desc).find('p')[0];
    let messageId = $(desc).data('msgid');

    RemoveClassIfPresent(messageTag, 'd-none');
    let editMessageDiv = $(desc).find('.edit-message')[0];
    $(editMessageDiv).empty();
    $(desc).children("div:last").remove();
    $(desc).remove(editMessageDiv);
    RemoveClassIfPresent(".attachmentView" + messageId, 'd-none');

    updateAttachedFilesArray(messageId);
    let objAttach = GetAttachedFilesCollection(messageId);
    //let deleteAttachedFiles = objAttach.AttachedFiles.myArray.filter((x) => { return x.isOldFile != true });
    DeleteAllDocument(objAttach.AttachedFiles);
    //let tempArr = objAttach.AttachedFiles.myArray.filter((x) => { return x.isOldFile == true });
    //SetAttachedFilesCollection(messageId, 0, tempArr);
});

$(document).on('click', '#editMessageConfirm', function () {
    console.log("msgId == ");
    EditMessageConfirm(this);
});

function EditMessageConfirm(e) {

    let parent = $(e).parents('li')[0];
    let desc = $(parent).find('.chat-desc')[0];
    let messageTag = $(desc).find('p')[0];
    RemoveClassIfPresent(messageTag, 'd-none');

    var channelId = $(desc).data('channelid');
    let msgId = $(desc).data("msgid");
    let newMessage = $("#editMessageText" + msgId).val();
    updateAttachedFilesArray(msgId);
    let objAttach = GetAttachedFilesCollection(msgId);
    let request = {
        "MessageDescription": newMessage,
        "Id": msgId,
        "ChannelId": channelId,
        "AttachmentList": objAttach.AttachedFiles
    }
    if (!IsNullOrEmptyOrUndefined(newMessage) || objAttach.AttachedFiles.length > 0) {
        EditMessage(request, messageTag);
    }

    let editMessageDiv = $(desc).find('.edit-message')[0];
    $(editMessageDiv).empty(); //didnt work
    $(desc).children("div:last").remove(); //didnt work
    $(desc).remove(editMessageDiv);
}

function EditMessage(request, messageTag) {
    $.ajax({
        url: "/Notification/EditMessage",
        type: "POST",
        dataType: "JSON",
        data: request,
        success: function (data) {
            if (data == true) {
                let newMessage = IsNullOrEmptyOrUndefinedLooseTyped(request['MessageDescription']) ? "" : request['MessageDescription'];
                let messageId = request['Id'];
                let attachmentList = request['AttachmentList'];
                $(messageTag).text(newMessage);
                let parent = $(messageTag).parents('li')[0];
                let editedStyle = $(parent).find('.edited-style')[0];
                RemoveClassIfPresent(editedStyle, 'd-none');
                GetAttachmentViewForEditMessage(attachmentList, messageId)
            }
        }
    });
}

$(document).on('click', '.retry', function () {
    AddClassIfAbsent($(this), 'd-none');
    let retryDiv = $(this).parents('li')[0];
    let request = GetRequestPropertiesFromDiv(retryDiv);
    CallSendMessage(request, retryDiv, null, true);
});

$(document).on('click', '.readByButton', function () {
    let id = $(this).parent().parent().data('msgid');
    let button = this;
    $(button).next('ul').empty();
    let IsOneToOneChat = $("#IsOneToOneChat").val().toLowerCase();
    let readByTotalParticipants = participantsCount - 1;
    GetReadParticipants(IsOneToOneChat, readByTotalParticipants, id, button);
});

$(document).on('click', '.replyPrivately', function () {

    let userId = $(this).data('usrid');
    let userShortName = $(this).data('usershortname');
    let text = $(this).data('username');
    let vesselId = $('#VesselId').val()
    let vesselName = $('#VesselName').val()

    let replyPrivateParticipant = {
        "Id": userId,
        "UserShortName": userShortName,
        "Text": text
    };

    let request = {
        "replyPrivateParticipant": replyPrivateParticipant,
        "selectedVesselId": vesselId,
        "selectedVesselName": vesselName
    }

    $.ajax({
        url: "/Notification/NavigateToCreateDiscussion",
        type: "POST",
        dataType: "JSON",
        data: {
            details: request
        },
        success: function (response) {
            window.location.href = response;
        }
    });
});

$(document).on('change', '.inputEditMessageAttachmentsCls', function sub(obj) {
    let messageId = $(this).data('messageid');
    updateAttachedFilesArray(messageId);
    let tempAttachObj = GetAttachedFilesCollection(messageId);
    var tempAttachedFiles = tempAttachObj.AttachedFiles;
    var tempFilesAttached = tempAttachObj.FilesAttached;

    console.log("before tempFilesAttached == ", tempFilesAttached);
    if (tempFilesAttached == 0) {
        tempFilesAttached = tempAttachedFiles.length;
    }

    console.log("tempAttachedFiles.length == ", tempAttachedFiles.length);
    console.log("after tempFilesAttached == ", tempFilesAttached);

    var input = document.getElementById('inputEditMessageAttachments' + messageId);
    var docsCount = tempAttachedFiles.length + input.files.length;

    if (CheckFilesAttachedCount(docsCount)) {
        SetSelectedFileCount(input.files.length);
        EditMsg.EditMessageId = messageId;
        //AddAttachmentToEditChat();
        for (var i = 0; i < input.files.length; ++i) {
            let fileSequenceNumber = tempFilesAttached + i + 1;
            UploadFile(input.files[i], fileSequenceNumber, "Message", "#divEditMessageAttachemnts" + messageId, tempAttachedFiles, null);
        }
        tempFilesAttached = tempFilesAttached + input.files.length;

        SetAttachedFilesCollection(messageId, tempFilesAttached, tempAttachedFiles)
    }
});

$(document).on('click', '.attachment-close', function () {
    let messageId = $(this).parent().data('messageid');
    if (!IsNullOrEmptyOrUndefinedLooseTyped(messageId)) {
        AttchmentCloseForEditMessage(messageId, this);
    }
    else {
        let sequence = $(this).parent().data('sequence');

        $(this).parent().remove();

        var fileObject = AttachedFiles.filter(x => {
            return x.sequence == sequence;
        });

        DeleteDocument(fileObject[0].ettId);

        var filteredFiles = AttachedFiles.filter(x => {
            return x.sequence != sequence;
        });
        AttachedFiles = filteredFiles;

        AdjustElementsOnAttachmentChanges();

        if (AttachedFiles.length == 0) {
            AddClassIfAbsent('#divMessageAttachemnts', 'd-none');
        }
    }
});

//deleteMessage
$(document).on('click', '.deleteMessage', function () {
    var parent = $(this).parents('li')[0];
    var desc = $(parent).find('.chat-desc')
    var messageId = $(desc).data('msgid');
    var channelId = $(desc).data('channelid');
    var message = $(desc).find('p').text();

    $("#notificationMessageDelete").modal('show');
    $('#yesNotificationMessageDelete').off();
    $('#yesNotificationMessageDelete').on('click', function () {

        let request = {
            "Id": messageId,
            "ChannelId": channelId,
            "MessageDescription": message
        }

        //deleting message
        DeleteMessage(request, function () {
            let messageDeletedMessage = "<i>" + DeleteMessageTemplate + "</i>";
            $(desc).find('p').html(messageDeletedMessage);
            AddClassIfAbsent($(parent).find('.message-options'), 'd-none');
            AddClassIfAbsent($(parent).find('.sent'), 'd-none');
            AddClassIfAbsent($(parent).find('.attached-files'), 'd-none');
        });

        $("#notificationMessageDelete").modal('hide');
    });

    $('#noNotificationMessageDelete').off();
    $('#noNotificationMessageDelete').on('click', function () {
        $("#notificationMessageDelete").modal('hide');
    });

});


function AttchmentCloseForEditMessage(messageId, elementObj) {

    let sequence = $(elementObj).parent().data('sequence');
    let attachedTo = $(elementObj).parent().data('attachedto');
    $(elementObj).parent().remove();
    updateAttachedFilesArray(messageId);
    let tempAttachedFiles = GetAttachedFilesCollection(messageId).AttachedFiles;

    var fileObject = tempAttachedFiles.filter(x => {
        return x.sequence == sequence;
    });

    DeleteDocument(fileObject[0].ettId);

    var filteredFiles = tempAttachedFiles.filter(x => {
        return x.sequence != sequence;
    });
    tempAttachedFiles = filteredFiles;

    if (attachedTo == "Message") {
        //AdjustElementsOnAttachmentChanges();

        if (tempAttachedFiles.length == 0) {
            RemoveClassIfPresent("#editMessageText" + messageId, "add-attachment-div");
            AddClassIfAbsent("#divEditMessageAttachemnts" + messageId, 'd-none');
        }
    }

    SetAttachedFilesCollection(messageId, 0, tempAttachedFiles)
}


$(document).ready(function () {
    AjaxError();
    if (sessionStorage.getItem(NotificationMobileChatDetailKey) != null) {
        if ($('#isFilterChange').val() == "True" || $('#isFilterChange').val() == "true" || $('#isFilterChange').val() == true) {            
            sessionStorage.setItem(NotificationMobileChatDetailKey, $('#hdnSessionStorageDetails').val());
        }
        else {
            //fill filter fields from sessionStorage            
            $('#hdnSessionStorageDetails').val(sessionStorage.getItem(NotificationMobileChatDetailKey));
        }        
    }
    else {
        //fill sessionStorage from fields
        sessionStorage.setItem(NotificationMobileChatDetailKey, $('#hdnSessionStorageDetails').val());
    }

    if ($('#hdnIsFromOtherSource').val() == true || $('#hdnIsFromOtherSource').val() == 'true' || $('#hdnIsFromOtherSource').val() == 'True') {
        $.ajax({
            url: "/Notification/GetSessionStorageFilterForChatDetail",
            type: "POST",
            dataType: "JSON",
            data: {
                "sessionDetails": $('#hdnSessionStorageDetails').val()
            },
            success: function (data) {
                if (data != null) {
                    GetChannelDetails(data.channelId);
                    GetCurrentUserDetails();

                    $('#hdnCurrentPageNumber').val(1);
                    $('#hdnHasNextPage').val(true);
                    $("#hdnSelectedChannelId").val(data.channelId);
                    ChannelMessage(data.channelId, false);
                }
            }
        });
    }
    else {
        var channelId = $("#ChannelId").val();
        GetCurrentUserDetails();

        $('#hdnCurrentPageNumber').val(1);
        $('#hdnHasNextPage').val(true);
        $("#hdnSelectedChannelId").val(channelId);
        ChannelMessage(channelId, false);
        GetChannelHeaderDetails();
        if (sessionStorage.getItem(NotificationPageKey) != null) {
            let request = {
                selectedChannelId: channelId,
            };
            $.ajax({
                url: "/Notification/SetSessionStorageFilterSelectedChannel",
                type: "POST",
                dataType: "JSON",
                data: {
                    "channelRequest": request,
                    "sessionDetails": sessionStorage.getItem(NotificationPageKey)
                },
                success: function (data) {
                    if (data != null) {
                        sessionStorage.setItem(NotificationPageKey, data);
                    }
                }
            });
        }        
    }

    if (GetCookie('NotificationApplicationId') != '2') {
        RemoveClassIfPresent('.backclose', 'd-none');
        $('.app-header').css("height", "60px");
    }
    else {
        $('body').addClass("hideleftmenuheader");
    }

    if (GetCookie('NotificationApplicationId') == '1') {
        AddClassIfAbsent('.mobileInfoIcon', 'd-none');
    }
    SignalRConnect();

    $(window).resize(function () {
        AdjustElementsOnAttachmentChanges();
    });

    $("#messageSection").css("margin-top", $(".chat-header").outerHeight(true));    

    $('.backclose').click(function () {
        sessionStorage.removeItem(NotificationMobileChatDetailKey);
        CloseVChatApplication();
    });

    BackButtonForNotification(NotificationMobileChatDetailKey, false, function () {
        AddModelLoadingIndicator('.notification-mobile-chat');
        let request = {
            selectedChannelId: 0,
        };
        SetSessionStorageFilterSelectedChannel(request);
    }, function () {
        RemoveModelLoadingIndicator('.notification-mobile-chat');
    });

    $('.back').click(function () {
        DeleteAllDocument(AttachedFiles);        
    });    

    $("#sendMessage").click(function () {
        let message = $("#messageText").val();
        $("#messageText").val('');
        ResetChatMessageUI();
        if (!IsNullOrEmptyOrUndefined(message) || AttachedFiles.length > 0) {
            SendMessage(message, $("#hdnSelectedChannelId").val(), AttachedFiles, ResetChatMessage);
        }
    });

    //$("#messageText").focus(function (e) {
    //    if (screen.width < MobileScreenSize) {
    //        e.preventDefault(); e.stopPropagation();
    //        window.scrollTo(0, 0)
    //    }
    //});

    $(document).on('keypress', function (e) {
        //if user presses enter with shiftkey follow natural behaviour 
        //if user presses only enter follow the below logic

        if (screen.width < MobileScreenSize) {            
            if ($("#messageText").is(":focus")) {
                if (e.which == 13) {
                    e.preventDefault();
                }
            }
        }
        else {
            if (e.which == 13 && !e.shiftKey) {
                e.preventDefault();
                if ($("#messageText").is(":focus")) {
                    if ($.trim($("#messageText").val()) != "") {
                        let message = $("#messageText").val();
                        $("#messageText").val('');
                        ResetChatMessageUI();
                        if (!IsNullOrEmptyOrUndefined(message) || AttachedFiles.length > 0) {
                            SendMessage(message, $("#hdnSelectedChannelId").val(), AttachedFiles, ResetChatMessage);
                        }
                    }
                }
            }
        }

        
    });

    const textarea = document.getElementById("messageText");

    textarea.addEventListener("input", (e) => {
        setHeight(e.target);
        AdjustElementsOnAttachmentChanges();
    });

    setHeight(textarea);

    $('#divChatMessages').on('scroll', function () {
        var pos = $('#divChatMessages').scrollTop();
        if (pos == 0) {
            let PageNumber = parseInt($('#hdnCurrentPageNumber').val());
            PageNumber++;
            $('#hdnCurrentPageNumber').val(PageNumber);
            if ($('#hdnHasNextPage').val() == "true") {
                ChannelMessage(channelId, true);
            }
        }
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

    $("#inputMessageAttachments").on('change', function sub(obj) {
        var input = document.getElementById('inputMessageAttachments');
        var docsCount = AttachedFiles.length + input.files.length;
        if (CheckFilesAttachedCount(docsCount)) {
            SetSelectedFileCount(input.files.length);
            AddAttachmentToChat();
            for (var i = 0; i < input.files.length; ++i) {
                let fileSequenceNumber = FilesAttached + i + 1;
                UploadFile(input.files[i], fileSequenceNumber, "Message", "#divMessageAttachemnts", AttachedFiles, AddAttachmentToChat);
            }
            FilesAttached = FilesAttached + input.files.length;
        }
    });

    $("#aViewRecord").click(function () {
        let channelId = $('#ChannelId').val();
        let vesselId = $('#VesselId').val();
        NavigateToChannelRecordDetails(channelId, vesselId);
    });
    
});
window.backAction = function () {
    BackFunction();
}

window.chatScrollLastRow = function () {
    AdjustElementsOnAttachmentChangesKeyPress();
}

function SetSessionStorageFilterSelectedChannel(request) {
    $.ajax({
        url: "/Notification/SetSessionStorageFilterSelectedChannel",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelRequest": request,
            "sessionDetails": sessionStorage.getItem(NotificationPageKey)
        },
        success: function(data) {
            if (data != null) {
                sessionStorage.setItem(NotificationPageKey, data);
            }
        }
    });
}

function BackFunction() {
    DeleteAllDocument(AttachedFiles);    
    if ($('#hdnIsFromOtherSource').val() == true || $('#hdnIsFromOtherSource').val() == 'true' || $('#hdnIsFromOtherSource').val() == 'True') {
        sessionStorage.removeItem(NotificationMobileChatDetailKey);
        chatmobileBackNotification();
    }
    else {        
        
        BackButtonAction(NotificationMobileChatDetailKey, function () {
            AddModelLoadingIndicator('.notification-mobile-chat');
            let request = {
                selectedChannelId: 0,
            };
            SetSessionStorageFilterSelectedChannel(request);
        }, function () {
            RemoveModelLoadingIndicator('.notification-mobile-chat');
        });
    }    
}

function chatmobileBackNotification() {
    window.parent.chatmobileBack();
}

function GetChannelDetails(channelId) {
    $.ajax({
        url: "/Notification/GetChannelDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": channelId
        },
        success: function (data) {
            if (data != null) {
                $('#ChannelId').val(data.channelId);
                $('#IsOneToOneChat').text(data.isOneToOneChat);
                $('#ParticipantCount').text(data.participantCount);
                $('#VesselId').val(data.vesselId);
                $('#VesselName').text(data.vesselName);
                $('.activeParticipantCount').text(data.activeParticipantsCount);
                $('.title').text(data.title);
                $('.mobilevesselname').html(data.vesselName);
                $('.vesselname').html(data.vesselName + '<span>' + data.vesselIMONumber + '</span>');
                $('.chat-participant').attr("href", '/Notification/NotificationMobileInfo/?request='+ data.notificationMobileParticipantURL);
                if (data.isGeneralCat) {
                    AddClassIfAbsent('.mobileInfoIcon', 'd-none');
                }
                else {
                    GetChannelRecordDetails($("#ChannelId").val(), data.vesselId)
                }
                
            }
            $("#messageSection").css("margin-top", $(".chat-header").outerHeight(true));
        }
    });
}


function GetChannelHeaderDetails() {
    $.ajax({
        url: "/Notification/GetChannelHeaderDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": $("#ChannelId").val()
        },
        success: function (data) {
            if (data != null) {
                $('.activeParticipantCount').text(data.activeParticipantsCount);
                if (data.isGeneralCat) {
                    AddClassIfAbsent('.mobileInfoIcon', 'd-none');
                }
                else {
                    GetChannelRecordDetails($("#ChannelId").val(), $('#VesselId').val())
                }
            }
            $("#messageSection").css("margin-top", $(".chat-header").outerHeight(true));
        }
    });
}

function ResetAttachedFilesVariables() {
    AttachedFiles = [];
    FilesAttached = 0;
}

function ResetChatMessageUI() {
    $("#messageText").val('');
    $('#messageText').attr('rows', 2);
    //$('#messageText').css("height", 'auto');
    $('#inputMessageAttachments').val('');
    $("#divMessageAttachemnts").empty();
    AddClassIfAbsent('#divMessageAttachemnts', 'd-none');
    AdjustElementsOnAttachmentChanges();
}

function ResetChatMessage() {
    ResetAttachedFilesVariables();
    ResetChatMessageUI();
}

function AddAttachmentToChat() {
    RemoveClassIfPresent('#divMessageAttachemnts', 'd-none');
    AdjustElementsOnAttachmentChanges();
}

function AdjustElementsOnAttachmentChanges() {
    let divMessageAttachemntsHeight = ($('#divMessageAttachemnts').height());
    let textAreaBottom = divMessageAttachemntsHeight + 8;

    var h1 = ($(".app-main").height());
    var h2 = ($('#divMessageAttachemnts').height());
    var h3 = ($("#messageText").outerHeight());

    $(".chat-list").css({
        "height": h1 - h2 - h3 - 20
    });
    //console.log($(".chat-list").height());

    ScrollToLastRow();
}
function AdjustElementsOnAttachmentChangesKeyPress() {
    let divMessageAttachemntsHeight = ($('#divMessageAttachemnts').height());
    let textAreaBottom = divMessageAttachemntsHeight + 8;

    var h1 = ($(".app-main").height());
    var h2 = ($('#divMessageAttachemnts').height());
    var h3 = ($("#messageText").outerHeight());

    $(".chat-list").css({
        "height": h1 - h2 - h3 - 20,
        "padding-bottom":"80px"
    });

    ScrollToLastRowOnKeyPress();
}
function ScrollToLastRowOnKeyPress() {
    var ulScroll = $('#messageSection .lastRow')[0];
    if (ulScroll != null && ulScroll != undefined) {
        ulScroll.scrollIntoView(true);
    }
}

function updateAttachedFilesArray(messageId) {
    var objAttachedFiles = GetAttachedFilesVariables();

    if (!IsNullOrEmptyOrUndefinedLooseTyped(messageId)) {
        if (objAttachedFiles.AttachedFilesCol.has(messageId)) {
            let tempObjArr = objAttachedFiles.AttachedFilesCol.get(messageId).AttachedFiles;
            let tempAttachedFiles = GetAttachedFilesCollection(messageId).AttachedFiles;

            if (tempObjArr.length > 0) {
                tempAttachedFiles = tempObjArr.slice();
                SetAttachedFilesCollection(messageId, 0, tempAttachedFiles);

                ResetDraftAttachedFilesVariables(messageId);
            }
        }
    }
}

function SetAttachedFilesCollection(messageId, FilesAttached, AttachedFiles) {
    let obj = {
        FilesAttached: FilesAttached,
        AttachedFiles: AttachedFiles
    }
    AttachedFilesCol.set(messageId, obj);
}

function GetAttachedFilesCollection(MessageId) {
    if (AttachedFilesCol.has(MessageId)) {
        return AttachedFilesCol.get(MessageId);
    }
    else {
        let obj = {
            FilesAttached: 0,
            AttachedFiles: []
        }

        return obj;
    }
}

function DeleteAllDocumentEditMessage() {
    AttachedFilesCol.forEach((value, key) => {
        updateAttachedFilesArray(key);
        var tempArr = GetAttachedFilesCollection(key).AttachedFiles;
        DeleteAllDocument(tempArr);
    });
}
