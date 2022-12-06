

import "select2/dist/js/select2.full.js";
require('bootstrap');

import { AjaxError, GetCookie, ToastrAlert, IsNullOrEmptyOrUndefined, RemoveClassIfPresent, AddClassIfAbsent, GetStringNullOrWhiteSpace, AddModelLoadingIndicator, RemoveModelLoadingIndicator, BackButton, IsNullOrEmptyOrUndefinedLooseTyped, IsNullOrEmpty } from "../common/utilities.js";
import { PleaseSelectUser, MessageCateory_General, MobileScreenSize, DeleteMessageTemplate, NotificationPageKey, NotificationMobileChatDetailKey, NotificationMobileDiscussionKey } from "../common/constants";
import { RowCreatedChannel, LoadChatMessageScreen, ExpandNotificationDetail, HideNewDiscussionSlot, ChannelMessage, GetReadParticipants, SendMessage, setHeight, DownloadAttachment, GetAttachmentIcon, ClearMoreUserPopUp, OpenAddParticipantsToChannel, AddNewParticipanttoChannel, GetVesselResponsibilities, InitialiseSearchMoreUserDropdown, SelectedParticipantTemplate, UploadFile, CheckFilesAttachedCount, ScrollToLastRow, DeleteDocument, DeleteAllDocument, InitialiseVesselSearch, GetAreaList, ValidationBeforeCreateChannel, DisplaySearchMoreUserSection, UpdateSelectedVesselDropdown, GetRequestPropertiesFromDiv, CallSendMessage, NavigateToChannelRecordDetails, GetCurrentUserDetails, ResetMandatoryFields, GetAttachedFilesVariables, LoadDiscussionScreen, ShortChatTitle, AttachmentTemplateSet, ResetDraftAttachedFilesVariables, saveasDraftDetails, SetSelectedFileCount, SignalRConnect, DeleteMessage, DeleteChannel, EditChannelMessage, GetAttachmentViewForEditMessage, CloseVChatApplication, DisableExistingMembers, ShowNotificationDraftDiscardConfBox, ValidationBeforeCreateDraft, NewChannelDetailSelect } from "../common/notificationutilities.js";

//any changes made here should also be made in 
//notificationMobileChat, notificationMobileInfo, notificationMobileDiscussion accordingly

var IsMobile = true;
var headerIsAppend = false;
var SelectedVesselId = "";
var SelectedVesselName = "";
var SelectedUser = [];
var PreSelectedUsers = [];
var FilesAttached = 0;
var AttachedFiles = [];
var AttachedFilesCol = new Map();
var EditMsg = { EditMessageId: 0 };
var isChannelSearchClicked = false;


var idb = function (e) { "use strict"; let t, n; const r = new WeakMap, o = new WeakMap, s = new WeakMap, a = new WeakMap, i = new WeakMap; let c = { get(e, t, n) { if (e instanceof IDBTransaction) { if ("done" === t) return o.get(e); if ("objectStoreNames" === t) return e.objectStoreNames || s.get(e); if ("store" === t) return n.objectStoreNames[1] ? void 0 : n.objectStore(n.objectStoreNames[0]) } return p(e[t]) }, set: (e, t, n) => (e[t] = n, !0), has: (e, t) => e instanceof IDBTransaction && ("done" === t || "store" === t) || t in e }; function u(e) { return e !== IDBDatabase.prototype.transaction || "objectStoreNames" in IDBTransaction.prototype ? (n || (n = [IDBCursor.prototype.advance, IDBCursor.prototype.continue, IDBCursor.prototype.continuePrimaryKey])).includes(e) ? function (...t) { return e.apply(f(this), t), p(r.get(this)) } : function (...t) { return p(e.apply(f(this), t)) } : function (t, ...n) { const r = e.call(f(this), t, ...n); return s.set(r, t.sort ? t.sort() : [t]), p(r) } } function d(e) { return "function" == typeof e ? u(e) : (e instanceof IDBTransaction && function (e) { if (o.has(e)) return; const t = new Promise((t, n) => { const r = () => { e.removeEventListener("complete", o), e.removeEventListener("error", s), e.removeEventListener("abort", s) }, o = () => { t(), r() }, s = () => { n(e.error || new DOMException("AbortError", "AbortError")), r() }; e.addEventListener("complete", o), e.addEventListener("error", s), e.addEventListener("abort", s) }); o.set(e, t) }(e), n = e, (t || (t = [IDBDatabase, IDBObjectStore, IDBIndex, IDBCursor, IDBTransaction])).some(e => n instanceof e) ? new Proxy(e, c) : e); var n } function p(e) { if (e instanceof IDBRequest) return function (e) { const t = new Promise((t, n) => { const r = () => { e.removeEventListener("success", o), e.removeEventListener("error", s) }, o = () => { t(p(e.result)), r() }, s = () => { n(e.error), r() }; e.addEventListener("success", o), e.addEventListener("error", s) }); return t.then(t => { t instanceof IDBCursor && r.set(t, e) }).catch(() => { }), i.set(t, e), t }(e); if (a.has(e)) return a.get(e); const t = d(e); return t !== e && (a.set(e, t), i.set(t, e)), t } const f = e => i.get(e); const l = ["get", "getKey", "getAll", "getAllKeys", "count"], D = ["put", "add", "delete", "clear"], v = new Map; function b(e, t) { if (!(e instanceof IDBDatabase) || t in e || "string" != typeof t) return; if (v.get(t)) return v.get(t); const n = t.replace(/FromIndex$/, ""), r = t !== n, o = D.includes(n); if (!(n in (r ? IDBIndex : IDBObjectStore).prototype) || !o && !l.includes(n)) return; const s = async function (e, ...t) { const s = this.transaction(e, o ? "readwrite" : "readonly"); let a = s.store; r && (a = a.index(t.shift())); const i = a[n](...t); return o && await s.done, i }; return v.set(t, s), s } return c = (e => ({ ...e, get: (t, n, r) => b(t, n) || e.get(t, n, r), has: (t, n) => !!b(t, n) || e.has(t, n) }))(c), e.deleteDB = function (e, { blocked: t } = {}) { const n = indexedDB.deleteDatabase(e); return t && n.addEventListener("blocked", () => t()), p(n).then(() => { }) }, e.openDB = function (e, t, { blocked: n, upgrade: r, blocking: o, terminated: s } = {}) { const a = indexedDB.open(e, t), i = p(a); return r && a.addEventListener("upgradeneeded", e => { r(p(a.result), e.oldVersion, e.newVersion, p(a.transaction)) }), n && a.addEventListener("blocked", () => n()), i.then(e => { s && e.addEventListener("close", () => s()), o && e.addEventListener("versionchange", () => o()) }).catch(() => { }), i }, e.unwrap = f, e.wrap = p, e }({});
var vshipDb;
async function createDB() {
    const db = idb.openDB("Test", 1, {
        upgrade(db) {
            db.createObjectStore("htmlCachedData");
            db.createObjectStore("modelCachedData");
            db.createObjectStore("PSCDeficiency");
            db.createObjectStore("ChatNotificationList");
            db.createObjectStore("ChatNotificationDetails");
            db.createObjectStore('ChatChannelsDeleted');
        },
    });

    vshipDb = {
        get: async (storeName, key) => (await db).transaction(storeName).store.get(key),
        getAll: async (storeName) => (await db).transaction(storeName).store.getAll(),
        getFirstFromIndex: async (storeName, indexName, direction) => {
            const cursor = await (await db).transaction(storeName).store.index(indexName).openCursor(null, direction);
            return (cursor && cursor.value) || null;
        },
        put: async (storeName, key, value) => (await db).transaction(storeName, 'readwrite').store.put(value, key === null ? undefined : key),
        putAllFromJson: async (storeName, json) => {
            const store = (await db).transaction(storeName, 'readwrite').store;
            JSON.parse(json).forEach(item => store.put(item));
        },
        delete: async (storeName, key) => (await db).transaction(storeName, 'readwrite').store.delete(key),
        autocompleteKeys: async (storeName, text, maxResults) => {
            const results = [];
            let cursor = await (await db).transaction(storeName).store.openCursor(IDBKeyRange.bound(text, text + '\uffff'));
            while (cursor && results.length < maxResults) {
                results.push(cursor.key);
                cursor = await cursor.continue();
            }
            return results;
        }
    };
    return Promise.resolve();
}

$(document).ready(function () {
    fn_GetOfflineData();
})

$(document).on('click', '.expandIcon', ShowNotificationDraftDiscardConfBox);

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

        SetAttachedFilesCollection(messageId, tempFilesAttached, tempAttachedFiles);
    }
});

$(document).on('click', '.retry', function () {
    AddClassIfAbsent($(this), 'd-none');
    let retryDiv = $(this).parents('li')[0];
    let request = GetRequestPropertiesFromDiv(retryDiv);
    CallSendMessage(request, retryDiv, ResetChatMessage, true);
});

$(document).on('click', '.readByButton', function () {
    let id = $(this).parent().parent().data('msgid');
    let button = this;
    $(button).next('ul').empty();

    let participantsCount = parseInt($("#hdnParticipantsCount_" + $('#hdnSelectedChannelId').val()).val());
    let IsOneToOneChat = $("#hdnIsOneToOneChat_" + $("#hdnSelectedChannelId").val()).val();
    let readByTotalParticipants = participantsCount - 1;

    GetReadParticipants(IsOneToOneChat, readByTotalParticipants, id, button);

});

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

    let localAddedUser = { userShortName: userShortName.trim(), text: userName.trim(), id: anchorId.trim(), description: userRoleDescription.trim() };
    let isUserAdded = SelectedUser.some(x => x.id === anchorId);
    if (!isUserAdded) {
        SelectedUser.push(localAddedUser);
        $('#divSelectedParticipants').prepend(sibling);
        var selectedParticipantsCount = parseInt($("#spanSelectedParticipantsCount").text());
        $("#spanSelectedParticipantsCount").text(++selectedParticipantsCount);
    }
});

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

$(document).on('click', '.replyPrivately', function () {
    LoadDiscussionScreen();
    ClearCreateDiscussionControls();
    ShowNewDiscussionSlot();
    $('#DraftTitle').text("New Discussion");
    let userId = $(this).data('usrid');
    let userShortName = $(this).data('usershortname');
    let text = $(this).data('username');
    let channelId = $(this).data('channelid');
    let vesselId = $('#hdnVesselId_' + channelId).val();
    let vesselName = $('#hdnVesselName_' + channelId).val();
    $('#hdnSelectedChannelId').val('');

    UpdateSelectedVesselDropdown(vesselId, vesselName, '#cboNotificationVesselSearch', '#btnSearchParticipants');

    $.ajax({
        url: "/Notification/GetUserPrimaryRole",
        type: "POST",
        dataType: "JSON",
        data: {
            userIds: userId
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
            };
            var option = new Option(obj.text, obj.id, true, true);
            $(option).data('raw', obj);
            $('#participantdropdown').append(option).trigger('change');
        }
    });
});

$(document).on('click', '.attachment-close', function () {
    let messageId = $(this).parent().data('messageid');
    if (!IsNullOrEmptyOrUndefinedLooseTyped(messageId)) {
        AttchmentCloseForEditMessage(messageId, this);
    }
    else {
        let sequence = $(this).parent().data('sequence');
        let attachedTo = $(this).parent().data('attachedto');
        $(this).parent().remove();

        updateAttachedFilesArray("");

        var fileObject = AttachedFiles.filter(x => {
            return x.sequence == sequence;
        });

        DeleteDocument(fileObject[0].ettId);

        var filteredFiles = AttachedFiles.filter(x => {
            return x.sequence != sequence;
        });
        AttachedFiles = filteredFiles;

        if (attachedTo == "Message") {
            AdjustElementsOnAttachmentChanges();

            if (AttachedFiles.length == 0) {
                RemoveClassIfPresent("#meessageText", "add-attachment-div");
                AddClassIfAbsent('#divMessageAttachemnts', 'd-none');
            }
        }
    }
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

    SetAttachedFilesCollection(messageId, 0, tempAttachedFiles);
}

$(document).on('click', '.deleteChannel', function (e) {
    e.preventDefault();
    let channelId = $(this).data('channelid');
    let isSaveAsDraft = $('#hdnIsSaveAsDraft_' + channelId).val();

    if (isSaveAsDraft === 'true') {
        $('#pDeleteChannelMessage').text('Do you want to delete the draft?');
    }
    else {
        $('#pDeleteChannelMessage').text('Are you sure you want to delete the discussion? This will also remove you from the chat and you will no longer receive messages.');
    }

    $("#modalDeleteChannelConfirmationDialog").modal('show');
    $('#btnDeleteChannelYes').off();
    $('#btnDeleteChannelYes').on('click', function () {
        DeleteChannel(channelId, isSaveAsDraft, ShowWelcomeMessage);
        $("#modalDeleteChannelConfirmationDialog").modal('hide');
    });

    $('#btnDeleteChannelNo').off();
    $('#btnDeleteChannelNo').on('click', function () {
        $("#modalDeleteChannelConfirmationDialog").modal('hide');
    });
});

var key = CryptoJS.enc.Utf8.parse('8080808080808080');
var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
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
    };
})();

//deleteMessage
$(document).on('click', '.deleteMessage', function () {
    var parent = $(this).parents('li')[0];
    var desc = $(parent).find('.chat-desc');
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
        };

        //deleting message
        DeleteMessage(request, function () {
            let messageDeletedMessage = "<i>" + DeleteMessageTemplate + "</i>";
            $(desc).find('p').html(messageDeletedMessage);
            AddClassIfAbsent($(parent).find('.message-options'), 'd-none');
            AddClassIfAbsent($(parent).find('.seen'), 'd-none');
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

$(document).on('click', '.editMessage', function () {
    let parent = $(this).parents('li')[0];
    let desc = $(parent).find('.chat-desc');
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
    };
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
                GetAttachmentViewForEditMessage(attachmentList, messageId);
            }
        }
    });
}

function GetSessionStorageFilterForList() {
    $.ajax({
        url: "/Notification/GetSessionStorageFilterForList",
        type: "POST",
        dataType: "JSON",
        data: {
            "sessionDetails": $('#hdnSessionStorageDetails').val()
        },
        beforeSend: function (xhr) {
            $('#hdnChatCurrentPageNumber').val("1");
        },
        success: function (data) {
            if (data != null) {
                $('#IsSearchClicked').val(data.isSearchClicked)
                $('#SearchText').val(data.searchText)
                DisplaySearchBox();
                let request = {
                    searchText: GetStringNullOrWhiteSpace($('#inputSearchChannel').val()),
                    isSearchClicked: false,
                    'PageNumber': parseInt($('#hdnChatCurrentPageNumber').val())
                };
                GetChannelList(request);
                ShowWelcomeMessage(false);
            }
        }

    });
}

$(document).ready(function () {
    AjaxError();
    var clearSessionStorage = $('#hdnClearSessionStorage').val();
    if ($('#OpenCreateNewChannel').val() != "True") {
        if (sessionStorage.getItem(NotificationPageKey) != null && (clearSessionStorage == false || clearSessionStorage == 'false' || clearSessionStorage == "False")) {
            if ($('#hdnIsFilterChange').val() == "True" || $('#hdnIsFilterChange').val() == "true" || $('#hdnIsFilterChange').val() == true) {
                $.ajax({
                    url: "/Notification/SetSessionStorageFilter",
                    type: "POST",
                    dataType: "JSON",
                    data: {
                        "sessionDetails": sessionStorage.getItem(NotificationPageKey),
                        "newSessionDetails": $('#hdnSessionStorageDetails').val(),
                    },
                    success: function (data) {
                        if (data != null) {
                            sessionStorage.setItem(NotificationPageKey, data);
                            $('#hdnSessionStorageDetails').val(data);
                            GetSessionStorageFilterForList();
                        }
                    }

                });
            }
            else {
                //fill filter fields from sessionStorage        
                $('#hdnSessionStorageDetails').val(sessionStorage.getItem(NotificationPageKey))
                GetSessionStorageFilterForList();
            }
        }
        else {
            //fill sessionStorage from fields
            sessionStorage.setItem(NotificationPageKey, $('#hdnSessionStorageDetails').val());
            GetSessionStorageFilterForList();
            if (sessionStorage.getItem(NotificationMobileChatDetailKey) != null || sessionStorage.getItem(NotificationMobileDiscussionKey) != null) {
                let sessionStorageValue = "";
                if (sessionStorage.getItem(NotificationMobileChatDetailKey) != null) {
                    sessionStorageValue = sessionStorage.getItem(NotificationMobileChatDetailKey);
                }
                else {
                    sessionStorageValue = sessionStorage.getItem(NotificationMobileDiscussionKey)
                }

                $.ajax({
                    url: "/Notification/SetSessionStorageSourceURL",
                    type: "POST",
                    dataType: "JSON",
                    data: {
                        "sessionDetails": sessionStorageValue,
                        "newSessionDetails": $('#hdnSessionStorageDetails').val(),
                    },
                    success: function (data) {
                        if (data != null) {
                            sessionStorage.setItem(NotificationPageKey, data);
                            $('#hdnSessionStorageDetails').val(data);

                        }
                    }

                });
            }
        }
    }

    $('#divChatContainerSection').on('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
            console.log('reached scroller');
            let PageNumber = parseInt($('#hdnChatCurrentPageNumber').val());
            PageNumber++;
            $('#hdnChatCurrentPageNumber').val(PageNumber);
            if ($('#hdnChatHasNextPage').val() == "true" || $('#hdnChatHasNextPage').val() == true) {
                let request = {
                    searchText: GetStringNullOrWhiteSpace($('#inputSearchChannel').val()),
                    isSearchClicked: isChannelSearchClicked,
                    'PageNumber': parseInt($('#hdnChatCurrentPageNumber').val()),
                };
                GetChannelListScrolled(request);
            }
        }
    });


    if (!IsNullOrEmptyOrUndefinedLooseTyped($("#EncryptedNoteId").val())) {
        BindNotesDetailsInVChat()
    }

    AddClassIfAbsent('.new-discussion-layout', 'd-none');
    AddMessagingUserIfNotExists();
    SignalRConnect();
    if ($(window).width() > MobileScreenSize) {
        $('body').addClass("hideleftmenuheader");
        $('body').addClass("toastrshiftleft");
    }
    else {
        if (GetCookie('NotificationApplicationId') != '2') {
            $('.mobile-header-back').hide();
            $('.menumobile').addClass("chatbackcolorhide");
            $('.app-header').css("height", "60px");
            RemoveClassIfPresent('.backclose', 'd-none');
        }
        else {
            $('body').addClass("hideleftmenuheader");
        }
    }
    if (GetCookie('NotificationApplicationId') == '1') {
        $(".hideleftmenuheader .notification-box").css("padding", "0px");
    }

    $('.backclose').click(function () {
        sessionStorage.removeItem(NotificationPageKey);
        CloseVChatApplication();
    });

    $(document).on("click", ".notifynewpopup .notification-box img.collapse", function () {
        $('.notifynewpopup .notification-box .chat-layout').addClass('right-hide');
        $('.notifynewpopup .notification-box .chat-header').addClass('chat-relative');
        $(this).hide();
        $('.notifynewpopup img.collapse-show').show();
    });
    $(document).on("click", ".notifynewpopup .notification-box img.collapse-show", function () {
        $('.notifynewpopup .notification-box .chat-layout').removeClass('right-hide');
        $('.notifynewpopup .notification-box .chat-header').removeClass('chat-relative');
        $(this).hide();
        $('.notifynewpopup img.collapse').show();
    });

    //BackButton(NotificationPageKey, true, function () {
    //	AddModelLoadingIndicator('.notification-box');
    //}, function () {
    //	RemoveModelLoadingIndicator('.notification-box');
    //});

    $('.back').click(function () {
        BackFunction();
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

    if (($(window).width() > MobileScreenSize)) {
        $('.search-panel').matchHeight({
            byRow: 0
        });
    }
    $('body').addClass('tooltipdesign');
    $('body').addClass('listbackcolor');
    $('[data-toggle="tooltip"]').tooltip();

    //to open search more user
    //used in CreateMessage
    $("#btnSearchParticipants").off();
    $("#btnSearchParticipants").click(function () {
        $("#searchparticipant").modal('show');

        $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", '#e5e5e5');
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

    $("#cboAreaSelection").select2({
        theme: "bootstrap4",
        placeholder: "Select",
        dropdownCssClass: 'vessel-drop area-drop',
        dropdownAutoWidth: true,
    });

    //auto expand textarea
    setTimeout(function () {
        if ($(window).width() > 766 && $(window).width() < 1100) {
            var h1 = ($(".notification-box").height());
            var h2 = ($(".fixed-mesage-box").outerHeight());
            $(".chat-list").css({
                "height": h1 - h2 - 1
            });
        }
    }, 1000);


    if (($(window).width() > 1200)) {
        var h1 = ($(".notification-box").height());
        var h2 = ($(".fixed-mesage-box").outerHeight());
        $(".chat-list").css({
            "height": h1 - h2 - 2
        });


    }

    if (($(window).width() > 1439)) {
        var h1 = ($(".notification-box").height());
        var h2 = ($(".fixed-mesage-box").outerHeight());
        $(".chat-list").css({
            "height": h1 - h2 - 3
        });
    }

    if (($(window).width() > 1900)) {
        var h1 = ($(".notification-box").height());
        var h2 = ($(".fixed-mesage-box").outerHeight());
        $(".chat-list").css({
            "height": h1 - h2 - 1
        });
    }

    if ($(window).width() > MobileScreenSize) {
        var h1 = ($(".discussion").outerHeight());
        var h2 = ($('.discussion-list-name').outerHeight());
        var h3 = ($(".app-main").height());

        $(".discussion-list").css({
            "height": h3 - h1 - h2
        });
    }

    if ($(window).width() < MobileScreenSize) {
        var h1 = ($(".app-main").height());
        var h2 = ($('.discussion').height());

        $(".discussion-list").css({
            "height": h1 - h2 - 80
        });
    }

    var val = $('#createChannelView').val();
    if (val != '1') {
        const textarea = document.getElementById("meessageText");

        textarea.addEventListener("input", (e) => {
            setHeight(e.target);
            AdjustElementsOnAttachmentChanges();
        });

        setHeight(textarea);
    }

    //auto expand textarea end
    //TODO:
    //Need to check with rashid
    //$(".expandIcon").click(ExpandNotificationDetail);
    //$(".expandIcon").click(function () {
    //    let channelId = $('#hdnSelectedChannelId').val();
    //    let isSaveAsDraft = $("#hdnIsSaveAsDraft_" + channelId).val();

    //    if (isSaveAsDraft == "false" || isSaveAsDraft == "False" || isSaveAsDraft == false) {
    //        $("#meessageText").val('');
    //        $('#meessageText').attr('rows', 2);
    //        updateAttachedFilesArray("");
    //        DeleteAllDocument(AttachedFiles);
    //        ResetChatMessage();
    //        if (!IsNullOrEmptyOrUndefinedLooseTyped($("#hdnChannelDraftMessage_" + channelId).val())) {
    //            $("#meessageText").val($("#hdnChannelDraftMessage_" + channelId).val());
    //        }
    //    }
    //});

    $("#sendMessage").click(function () {
        SendChatMessage();
    });

    $('#channelSearch').click(function () {
        ShowChannelSearchBox();
    });

    $('#btnSeachChannel').click(function () {

        var searchText = GetStringNullOrWhiteSpace($('#inputSearchChannel').val());
        if (!IsNullOrEmptyOrUndefinedLooseTyped(searchText)) {
            let request = {
                searchText: searchText,
                isSearchClicked: true
            };
            SetSessionStorageFilterForChannelList(request);
        }
    });

    //search close
    $('#btnCloseSearch').click(function () {
        RemoveClassIfPresent('#divChannelHeader', 'd-none');
        AddClassIfAbsent('#divSearchChannel', 'd-none');

        $('#inputSearchChannel').val('');
        let request = {
            searchText: GetStringNullOrWhiteSpace($('#inputSearchChannel').val()),
            isSearchClicked: false
        };
        SetSessionStorageFilterForChannelList(request);
        ShowWelcomeMessage(true);
    });

    $("#btnAddParticipant").off();
    $("#btnAddParticipant").click(function () {

        $("#searchparticipant").modal('show');

        $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", '#e5e5e5');
        let channelId = $('#hdnSelectedChannelId').val();
        let vesselId = $('#hdnVesselId_' + channelId).val();
        let vesselName = $('#hdnVesselName_' + channelId).val();
        ClearMoreUserPopUp(SelectedUser);
        OpenAddParticipantsToChannel(vesselId, vesselName, channelId, SelectedUser);
        let documentLoaded = false;
        $(document).ajaxStop(function () {
            if (!documentLoaded) {
                DisableExistingMembers();
                documentLoaded = true;
            }
        });
        $("#btnAddToMessage").off('click');
        $('#btnAddToMessage').click(function () {
            if (!fn_CommonOfflineMessage()) {
                return true;
            }
            if (SelectedUser.length > 0) {
                $("#addParticipantConfirmationDialog").modal('show');

                $("#yesConfirmationButton").off();
                $("#yesConfirmationButton").on('click', function () {
                    $("#searchparticipant").modal('hide');
                    AddNewParticipanttoChannel(channelId, SelectedUser);
                });
            } else {
                $("#searchparticipant").modal('hide');
                //discuss this with kavita for validations
                //AddNewParticipanttoChannel(channelId, SelectedUser);
                ToastrAlert("error", PleaseSelectUser);
            }
        });
    });

    //Chat message scrolling
    $('#divChatMessages').on('scroll', async function () {
        var pos = $('#divChatMessages').scrollTop();
        if (pos == 0) {
            let PageNumber = parseInt($('#hdnCurrentPageNumber').val());
            PageNumber++;
            $('#hdnCurrentPageNumber').val(PageNumber);
            if ($('#hdnHasNextPage').val() == "true" && $('#hdnIsNewChannelSelected').val() == "false") {
                if (!vshipDb) {
                    await createDB()
                }
                ChannelMessage($('#hdnSelectedChannelId').val(), true, vshipDb);
            }
        }
    });

    $(document).on('keypress', function (e) {
        //if user presses enter with shiftkey follow natural behaviour 
        //if user presses only enter follow the below logic
        if (e.which == 13 && !e.shiftKey) {
            e.preventDefault();
            if ($("#meessageText").is(":focus")) {
                if ($.trim($("#meessageText").val()) != "") {
                    SendChatMessage();
                }
            }

            //if ($(".editMessageTextcls").is(":focus")) {
            //	if ($.trim($(".editMessageTextcls").val()) != "") {
            //		$(this).closest('#editMessageConfirm').click();
            //	}
            //}

            if ($("#inputSearchChannel").is(":focus")) {
                if ($.trim($("#inputSearchChannel").val()) != "") {
                    var searchText = GetStringNullOrWhiteSpace($('#inputSearchChannel').val());
                    if (!IsNullOrEmptyOrUndefinedLooseTyped(searchText)) {
                        let request = {
                            searchText: searchText,
                            isSearchClicked: true
                        };
                        SetSessionStorageFilterForChannelList(request);
                        ShowWelcomeMessage(true);
                    }
                }
            }
        }
    });

    //chat message scroll query
    if (($(window).width() > MobileScreenSize)) {
        var newWidth = ($(".chat-list").width());
        $(".fixed-mesage-box").css({
            "maxWidth": newWidth + 10
        });
    }
    if (($(window).width() > 1440)) {
        var newWidth = ($(".chat-list").width());
        $(".fixed-mesage-box").css({
            "maxWidth": newWidth
        });
    }

    // discussion list query
    if (($(window).width() > MobileScreenSize)) {
        var newWidth1 = ($(".discussion").outerWidth());
        $(".discussion-list-name").css({
            "maxWidth": newWidth1 + 18
        });
    }
    if (($(window).width() > 993)) {
        var newWidth1 = ($(".discussion").outerWidth());
        $(".discussion-list-name").css({
            "maxWidth": newWidth1 + 4
        });
    }
    if (($(window).width() > 1440)) {
        var newWidth1 = ($(".discussion").outerWidth());
        $(".discussion-list-name").css({
            "maxWidth": newWidth1 + 5
        });
    }

    //chat header query
    if (($(window).width() > MobileScreenSize)) {
        var newWidth2 = ($(".record-section").outerWidth());
        var newWidth1 = ($(".chat-list").outerWidth());
        $(".chat-header").css({
            "maxWidth": newWidth1 + newWidth2
        });
    }
    if (($(window).width() > 992)) {
        var newWidth2 = ($(".record-section").outerWidth());
        var newWidth1 = ($(".chat-list").outerWidth());
        $(".chat-header").css({
            "maxWidth": newWidth1 + newWidth2
        });
    }
    if (($(window).width() > 1025)) {
        var newWidth2 = ($(".record-section").outerWidth());
        var newWidth1 = ($(".chat-list").outerWidth());
        $(".chat-header").css({
            "maxWidth": newWidth1 + newWidth2
        });
    }
    if (($(window).width() > 1440)) {
        var newWidth2 = ($(".record-section").outerWidth());
        var newWidth1 = ($(".chat-list").outerWidth());
        $(".chat-header").css({
            "maxWidth": newWidth1 + newWidth2 + 17
        });
    }

    // plus query


    $("#plus-sign").click(function () {
        ShowWelcomeMessage(false);
        $('#hdnSelectedChannelId').val('');
        ResetMandatoryFields();
        LoadDiscussionScreen();
        ClearCreateDiscussionControls();
        AddClassIfAbsent('#li_0', 'd-md-block');
        ShowNewDiscussionSlot();
        $('#DraftTitle').text("New Discussion");
        $('#hdnIsAllowDrafrtConfBox').val("Yes");
        AddModelLoadingIndicator('.new-discussion-layout');
        GetAreaList(null, 'cboAreaSelection', function () {
            RemoveModelLoadingIndicator('.new-discussion-layout');
        });
    });

    $("#cancel").click(function () {
        $('#hdnIsAllowDrafrtConfBox').val("No");
        var val = $('#IsStandaloneCreateChannel').val();
        if (val == 'True' || val == 'true' || val == true) {
            if (GetCookie('NotificationApplicationId') == '2') {
                AfterSaveChannelSuccess();
            }
            else if (GetCookie('NotificationApplicationId') == '1') {
                AfterCreateChannelAction();
            }
        }
        else {
            if (isChannelListPresentForCurrentUser()) {
                RemoveClassIfPresent('#li_0', 'd-md-block');
            }
            let channelId = $('.currentdiscussion').attr('id') + '';
            let isSaveAsDraft = $("#hdnIsSaveAsDraft_" + channelId).val();

            if (isSaveAsDraft == true || isSaveAsDraft == 'true' || isSaveAsDraft == 'True') {
                $('#hdnSelectedChannelId').val(channelId);
                saveasDraftDetails(channelId);
            }
            else {
                LoadChatMessageScreen();
                ClearCreateDiscussionControls();
                HideNewDiscussionSlot();
            }
            ShowWelcomeMessage(true);
        }
    });

    $("#btn-discussion").click(function () {
        if (!fn_CommonOfflineMessage()) {
            return true;
        }
        CreateChannel(false);
    });

    $("#btn-SaveAsDraft").click(function () {
        if (!fn_CommonOfflineMessage()) {
            return true;
        }
        CreateChannel(true);
    });

    //subject textbox
    $('#txtSubject').focusout(function () {
        if ($(this).val().length > 0) {
            $("#txtSubject").css("border", "2px solid #e5e5e5");
        }
    });

    $('#txtSubject').keyup(function () {
        if ($(this).val().length > 0) {
            $("#txtSubject").css("border", "2px solid #e5e5e5");
            $('#hdnIsAllowDrafrtConfBox').val("Yes");
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
            $('#hdnIsAllowDrafrtConfBox').val("Yes");
        }
    });

    //participants drop down
    InitialiseParticipantDropdown();

    InitialiseSearchMoreUserDropdown(SelectedVesselId);

    $("#cboSearchMoreUsers").on('select2:select', function (selection) {
        //add user to array
        let isUserAdded = SelectedUser.some(x => x.id === selection.params.data.id);
        if (!isUserAdded) {
            let localAddedUser = { userShortName: selection.params.data.userShortName, text: selection.params.data.text, id: selection.params.data.id, description: selection.params.data.description };
            SelectedUser.push(localAddedUser);
            var row = SelectedParticipantTemplate(selection.params.data, true);
            $('#divSelectedParticipants').prepend(row);
            var selectedParticipantsCount = parseInt($("#spanSelectedParticipantsCount").text());
            $("#spanSelectedParticipantsCount").text(++selectedParticipantsCount);
        }
    });

    //Vessel search

    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        IsMobile = true;
    } else {
        IsMobile = false;
    }

    InitialiseVesselSearch("#cboNotificationVesselSearch", formatResult, formatRepoSelection);

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

    //new message alert scroll 
    //$("#divNewMessageAlert").click(function () {
    //    ScrollToLastRow();
    //    HideNewMessageAlert();
    //});
    //TODO:
    //Need to check with rashid
    //if ($(window).width() > MobileScreenSize) {
    //	var notificationExpander = $('.expandIcon')[0];
    //	if (notificationExpander != null && notificationExpander != undefined) {
    //		notificationExpander.click();
    //	}
    //}

    if (($(window).width() > MobileScreenSize) & ($(window).width() < 991)) {
        $('body').addClass('block-divs-ipad');
    }

    $("#inputChannelAttachments").on('change', function sub(obj) {
        var input = document.getElementById('inputChannelAttachments');
        updateAttachedFilesArray("");
        if (FilesAttached == 0) {
            FilesAttached = AttachedFiles.length;
        }
        var docsCount = AttachedFiles.length + input.files.length;
        if (CheckFilesAttachedCount(docsCount)) {
            SetSelectedFileCount(input.files.length);
            for (var i = 0; i < input.files.length; ++i) {
                let fileSequenceNumber = FilesAttached + i + 1;
                UploadFile(input.files[i], fileSequenceNumber, "Channel", "#divNewChannelAttachemnts", AttachedFiles, null);
            }
            FilesAttached = FilesAttached + input.files.length;
        }
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


    if ($('#OpenCreateNewChannel').val() == "True") {
        $('#OpenCreateNewChannel').val("False");
        ResetMandatoryFields();
        LoadDiscussionScreen();
        ClearCreateDiscussionControls();
        ShowNewDiscussionSlot();
        var vesselId = $('#VesselId').val();
        if (!IsNullOrEmptyOrUndefinedLooseTyped(vesselId)) {
            $("#cboNotificationVesselSearch").prop("disabled", true);
        }
        UpdateSelectedVesselDropdown(vesselId, $('#VesselName').val(), '#cboNotificationVesselSearch', '#btnSearchParticipants');
        if (!IsNullOrEmptyOrUndefinedLooseTyped($('#DefaultMessage').val())) {
            let defaultMessage = $('#DefaultMessage').val();
            $('#txtSubject').val(defaultMessage);
        }
        AddModelLoadingIndicator('.new-discussion-layout');
        var catId = $('#CategoryId').val();
        if ($('#CategoryId').val() == '0' || $('#CategoryId').val() == 0) {
            catId = MessageCateory_General;
        }
        GetAreaList(catId, 'cboAreaSelection', function () {
            RemoveModelLoadingIndicator('.new-discussion-layout');
        });

        if (!IsNullOrEmptyOrUndefinedLooseTyped($('#CategoryId').val()) || catId == MessageCateory_General) {
            if (catId == MessageCateory_General) {
                AddClassIfAbsent('#divAreaDropdown', 'd-none');
            }
            else {
                RemoveClassIfPresent('#divAreaDropdown', 'd-none');
            }
            $('#CategoryId').val('');
        }
    }

    $("#aViewRecord").click(function () {
        let channelId = $('#hdnSelectedChannelId').val();
        let vesselId = $('#hdnVesselId_' + channelId).val();
        NavigateToChannelRecordDetails(channelId, vesselId);
    });

    $("#cancel").click(function () {

        let channelId = $('#hdnSelectedChannelId').val();

        if (isChannelListPresentForCurrentUser()) {
            if (IsNullOrEmptyOrUndefinedLooseTyped(channelId) || channelId == '0') {
                channelId = $('.currentdiscussion').attr('id') + '';
            }
            let isSaveAsDraft = $("#hdnIsSaveAsDraft_" + channelId).val();

            if (isSaveAsDraft == true || isSaveAsDraft == 'true' || isSaveAsDraft == 'True') {
                $('#hdnSelectedChannelId').val(channelId);
                saveasDraftDetails(channelId);
            }
            else {
                LoadChatMessageScreen();
                ClearCreateDraftDiscussionControls();
                HideNewDiscussionSlot();
            }
        }
        else {
            ShowWelcomeMessage(true);
        }

    });

    $(document).on('focusout', function (e) {

        e.preventDefault();
        var channelId = $('#hdnSelectedChannelId').val();

        if ($("#meessageText").is(e.target) && !IsNullOrEmptyOrUndefinedLooseTyped($("#meessageText").val())) {
            $("#hdnChannelDraftMessage_" + channelId).val($("#meessageText").val());
        }
    });


    $('#yesNotificationDraft').off();
    $('#yesNotificationDraft').on('click', function () {
        CreateChannel(true);
    });

    $('#noNotificationDraft').off();
    $('#noNotificationDraft').on('click', function () {
        var notificationDraftChannelId = $("#hdnNotificationDraftChannelId").val();
        ExpandNotificationDetail(notificationDraftChannelId);
    });


});

window.yesNotificationDraftAction = function () {
    CreateChannel(true);
    return true;
}

window.noNotificationDraftAction = function () {
    return true;
}

window.closeNotificationModalAction = function () {
    return onCloseNotificationModalAction();
}

window.chatScrollLastRow = function () {
}

function onCloseNotificationModalAction() {
    var isValid = ValidationBeforeCreateDraft('#txtSubject', '#txtMessage', '#participantdropdown');
    if ($('#hdnIsAllowDrafrtConfBox').val() == "Yes" && isValid) {
        return false;
    }
    else {
        return true;
    }
}

window.backAction = function () {
    BackFunction();
}
function BackFunction() {
    updateAttachedFilesArray("");
    DeleteAllDocument(AttachedFiles);
    sessionStorage.removeItem(NotificationPageKey);
    chatmobileBackNotification();
}
function SetSessionStorageFilterForChannelList(request) {
    $.ajax({
        url: "/Notification/SetSessionStorageFilterForNotification",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelRequest": request,
            "sessionDetails": $('#hdnSessionStorageDetails').val()
        },
        success: function (data) {
            if (data != null) {
                $('#hdnSessionStorageDetails').val(data)
                sessionStorage.setItem(NotificationPageKey, $('#hdnSessionStorageDetails').val());
                GetChannelList(request);
            }
        },
        error: function (jqXHR, exception) {
            GetChannelList(request);
        }
    });
}

function ShowNewDiscussionSlot() {
    $(".discussion-list li.new-discussion-highlight").show();
    $('.discussion-list li').find('.nav-link.active').addClass("currentdiscussion");
    $('.discussion-list li').find('.nav-link').removeClass("active");
}

function CreateChannel(isSaveAsDraft) {
    $('#hdnIsAllowDrafrtConfBox').val("No");
    let isValidate = null;
    if (!isSaveAsDraft) {
        isValidate = ValidationBeforeCreateChannel('#txtSubject', '#txtMessage', '#participantdropdown');
    } else {
        isValidate = ValidationBeforeCreateDraft('#txtSubject', '#txtMessage', '#participantdropdown', true);
    }

    if (isValidate) {
        updateAttachedFilesArray("");

        let participantsArray = $('#participantdropdown').select2('data');
        var participantsList = participantsArray.map(item => {
            return {
                id: item.id,
                text: item.text,
            };
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
            "CategoryId": IsNullOrEmptyOrUndefinedLooseTyped($('#cboAreaSelection').val()) ? 1 : $('#cboAreaSelection').val(),
            "ContextPaylod": IsNullOrEmptyOrUndefinedLooseTyped($('#ContextPayload').val()) ? "" : $('#ContextPayload').val(),
            "IsSaveAsDraft": isSaveAsDraft,
            "ChannelId": IsNullOrEmptyOrUndefinedLooseTyped($('#hdnSelectedChannelId').val()) ? 0 : $('#hdnSelectedChannelId').val(),
            "ReferenceIdentifier": $('#ReferenceIdentifier').val()
        };

        $.ajax({
            url: "/Notification/CreateNotificationChannel",
            type: "POST",
            dataType: "JSON",
            data: request,
            beforeSend: function (xhr) {
                AddModelLoadingIndicator('.notification-createchat');
            },
            success: function (data) {
                if (data != 0) {

                    if (isSaveAsDraft == 'True' || isSaveAsDraft == true || isSaveAsDraft == "true") {
                        ToastrAlert("success", "Discussion saved as draft.");
                    } else {
                        ToastrAlert("success", "Discussion created successfully.");
                    }

                    ResetAttachedFilesVariables();
                    var val = $('#IsStandaloneCreateChannel').val();
                    if (val == 'True' || val == 'true' || val == true) {

                        setTimeout(function () {
                            if (GetCookie('NotificationApplicationId') == '2') {
                                updateChatAndNotesCount();
                                AfterSaveChannelSuccess();
                            }
                            else if (GetCookie('NotificationApplicationId') == '1') {
                                AfterCreateChannelAction();
                            }
                        }, 1000);
                    }
                    else {
                        var channelCount = parseInt($('#hdnChannelListCount').val());
                        channelCount = channelCount + 1;
                        $('#hdnChannelListCount').val(channelCount);
                        HideNewDiscussionSlot();
                        LoadChatMessageScreen();
                        NewChannelDetailCreated(data, isSaveAsDraft);
                    }
                }
            },
            complete: function () {
                RemoveModelLoadingIndicator(".notification-createchat");
            }
        });
    }
}
function AfterSaveChannelSuccess() {
    window.parent.chatAfterSaveChannelSuccess();

}
function updateChatAndNotesCount() {
    window.parent.updateChatAndNotesCount();
}

function chatmobileBackNotification() {
    window.parent.chatmobileBack();
}

function ClearCreateDiscussionControls() {
    $('#txtSubject').val('');
    $('#txtMessage').val('');
    updateAttachedFilesArray("");
    DeleteAllDocument(AttachedFiles);
    ResetAttachedFilesVariables();
    $('#inputChannelAttachments').val('');
    $("#divNewChannelAttachemnts").empty();
    $('#participantdropdown').val(null).trigger('change');
    $('#cboNotificationVesselSearch').val(null).trigger('change');
    //$('#cboAreaSelection').prop('selectedIndex', 0)
    //$('#cboAreaSelection option').remove()
    $('#cboAreaSelection option[value="0"]').attr("selected", true);
    $("#cboNotificationVesselSearch").prop("disabled", false);
    AddClassIfAbsent('#divAreaDropdown', 'd-none');

    SelectedVesselId = "";
    SelectedVesselName = "";
    PreSelectedUsers.length = 0;
    AddClassIfAbsent('.divAreaDropdowncls', 'd-none');
    //$('#cboAreaSelection').prop('selectedIndex', -1);
}

function SendChatMessage() {
    let message = $("#meessageText").val();
    ResetChatMessageUI();
    let id = $('#hdnSelectedChannelId').val();
    if (!IsNullOrEmptyOrUndefined(message) || AttachedFiles.length > 0) {
        SendMessage(message, id, AttachedFiles, ResetChatMessage);
    }
}

export function NewChannelDetailCreated(channelId, isSaveAsDraft) {
    $.ajax({
        url: "/Notification/GetChannelDetail",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },
        success: async function (data) {
            if (data != null) {
                data = typeof (data) == "string" ? JSON.parse(data) : data;
                if (!vshipDb) {
                    await createDb()
                }
                const length = (await vshipDb.getAll('ChatNotificationList')).length
                vshipDb.put('ChatNotificationList', length, data);
                ShowWelcomeMessage(true);
                var ul = $('#chatsection');
                var liclone = ul.find('#li_' + channelId);
                if (liclone != null && liclone.length > 0) {

                    $('#li_0').after(liclone);
                    $('#modifiedDate_' + data.channelId).html(data.recievedDate);
                    $('#hdnTitle_' + data.channelId).val(data.title);
                    $('#hdnVesselId_' + data.channelId).val(data.vesselId);
                    $('#hdnVesselName_' + data.channelId).val(data.vesselName);
                    $('#hdnIMONumber_' + data.channelId).val(data.vesselIMONumber);
                    $('#hdnIsOneToOneChat_' + data.channelId).val(data.isOneToOneChat);
                    $('#hdnIsSaveAsDraft_' + data.channelId).val(isSaveAsDraft);
                    $('#hdnCategoryId_' + data.channelId).val(data.categoryId);
                    $('#hdnIsGeneralCat_' + data.channelId).val(data.isGeneralCat);
                    $('#Title_' + data.channelId).html(ShortChatTitle(data.title));
                    $('#VesselName_' + data.channelId).html(data.vesselName);

                    if (isSaveAsDraft == "true" || isSaveAsDraft == "True" || isSaveAsDraft == true) {
                        $('#divDraft_' + data.channelId).show();
                    }
                    else {
                        $('#divDraft_' + data.channelId).hide();
                    }

                    liclone.find('.expandIcon')[0].click();
                }
                else {
                    $('#li_0').after(RowCreatedChannel(data, '', isSaveAsDraft));
                    var ul = $('#chatsection');
                    var liclone = ul.find('#li_' + channelId);
                    if (isSaveAsDraft == "true" || isSaveAsDraft == "True" || isSaveAsDraft == true) {
                        liclone.find('.expandIcon')[0].click();
                    }
                    else {
                        liclone.find('.expandIcon')[0].click();
                    }
                }
            }
        }
    });
}

function SetSearchFilter(searchText, isSearchClicked) {
    var searchParams = new URLSearchParams(window.location.search);
    var obj = { "searchText": searchText, "isSearchClicked": isSearchClicked };
    //var obj = { "searchText": searchText, "isSearchClicked": isSearchClicked }
    var searchReq = JSON.stringify(obj);
    if (searchParams.has("urlParameter")) {
        searchParams.set("urlParameter", searchReq);
    }
    else if (searchParams.has("UrlParameter")) {
        searchParams.set("UrlParameter", searchReq);
    }
    else {
        searchParams.set("urlParameter", searchReq);
    }

    window.location.href = window.location.protocol + "//" + window.location.host + window.location.pathname + '?' + searchParams.toString();
}

function DisplaySearchBox() {
    SetSearchBoxText();
    if ($('#IsSearchClicked').val() == "True" || $('#IsSearchClicked').val() == "true" || $('#IsSearchClicked').val() == true) {
        ShowChannelSearchBox();
    }
    else {
        HideChannelSearchBox();
    }
}

function ShowChannelSearchBox() {
    AddClassIfAbsent('#divChannelHeader', 'd-none');
    RemoveClassIfPresent('#divSearchChannel', 'd-none');
}

function HideChannelSearchBox() {
    AddClassIfAbsent('#divSearchChannel', 'd-none');
    RemoveClassIfPresent('#divChannelHeader', 'd-none');
}

function SetSearchBoxText() {
    let searchText = GetStringNullOrWhiteSpace($('#SearchText').val());
    $('#inputSearchChannel').val(searchText);
}

function OpenSearchMoreUsers() {
    var vesselObj = $('#cboNotificationVesselSearch').select2('data')[0];
    var selectedVesselName = vesselObj.vesselName || vesselObj.text;
    var selectedVesselId = vesselObj.vesselId || vesselObj.id;
    $('#spanResponsibleVesselName').text(selectedVesselName);
    GetVesselResponsibilities(selectedVesselId);
    InitialiseSearchMoreUserDropdown(selectedVesselId);
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

    $result.find('.initialname').text(repo.userShortName || raw.userShortName);
    $result.find('.type').text(repo.description || raw.description);

    let userName = repo.text;
    let userShortName = repo.userShortName || raw.userShortName;
    let userRoleDescription = repo.description || raw.description;
    let userId = repo.id;
    let user = { userShortName: userShortName.trim(), text: userName.trim(), id: userId.trim(), description: userRoleDescription.trim() };
    PreSelectedUsers.push(user);
    return $result;
}

function ResetAttachedFilesVariables() {
    AttachedFiles = [];
    FilesAttached = 0;
}

function ResetChatMessageUI() {
    $("#meessageText").val('');
    $('#meessageText').attr('rows', 2);
    $('#meessageText').css("height", 'auto');
    $('#inputMessageAttachments').val('');
    $("#divMessageAttachemnts").empty();
    RemoveClassIfPresent("#meessageText", "add-attachment-div");
    AddClassIfAbsent('#divMessageAttachemnts', 'd-none');
    AdjustElementsOnAttachmentChanges();
}

function ResetChatMessage() {
    ResetAttachedFilesVariables();
    ResetChatMessageUI();
}

function AddAttachmentToChat() {
    AddClassIfAbsent("#meessageText", "add-attachment-div");
    RemoveClassIfPresent('#divMessageAttachemnts', 'd-none');
    AdjustElementsOnAttachmentChanges();
}

function AddAttachmentToEditChat() {
    AddClassIfAbsent("#editMessageText" + EditMsg.EditMessageId, "add-attachment-div");
    RemoveClassIfPresent("#divEditMessageAttachemnts" + EditMsg.EditMessageId, 'd-none');
    //AdjustElementsOnAttachmentChanges();
}

function AdjustElementsOnAttachmentChanges() {
    let divMessageAttachemntsHeight = ($('#divMessageAttachemnts').height());
    let textAreaBottom = divMessageAttachemntsHeight + 21;
    $("#meessageText").css("bottom", textAreaBottom + 'px');

    var h1 = ($(".notification-box").height());
    var h2 = ($('#divMessageAttachemnts').height());
    var h3 = ($("#meessageText").outerHeight());
    var addHeight = 0;

    if (($(window).width() > 1200)) {
        addHeight = 27;
    }

    if (($(window).width() > 1439)) {
        addHeight = 33;
    }

    if (($(window).width() > 1900)) {
        addHeight = 60;
    }

    $(".chat-list").css({
        "height": h1 - h2 - h3 - addHeight
    });

    $(".fixed-mesage-box").css({
        "height": h2 + h3 + addHeight
    });
    ScrollToLastRow();
}

function formatRepoSelection(repo) {
    SelectedVesselName = repo.vesselName || repo.text;
    SelectedVesselId = repo.vesselId || repo.id;
    let isVesselSelected = IsNullOrEmptyOrUndefinedLooseTyped(SelectedVesselId) ? false : true;
    DisplaySearchMoreUserSection(isVesselSelected, '#btnSearchParticipants');

    var channelId = $('#hdn' + 'SelectedChannelId').val();
    let isSaveAsDraft = $("#hdn" + "IsSaveAsDraft_" + channelId).val();
    if (isSaveAsDraft != "true" && isSaveAsDraft != "True" && isSaveAsDraft != true) {
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

function ShowWelcomeMessage(isShowWelcome) {

    var userShortName = $('#currentUserShortName').text();
    var username = $('#currentUserName').text();

    if (isShowWelcome && !isChannelListPresentForCurrentUser()) {

        var messageDescription = 'Welcome to V.Chat, use this to communicate with your vessel contacts, ' +
            'or with others in the company.<br/>Use the add button to create a new discussion ' +
            'or click the filter button to search the discussions.';

        $('#messageTitle').text("Welcome to V.Chat");
        $(".newdiscussion").text("Welcome to V.Chat");
        ShowNewDiscussionSlot();
        $('.fixed-mesage-box').hide();

        var localRow = '<li><div class="row mx-auto no-gutters">' +
            '<div class="col-1 col-md-1 col-lg-1 col-xl-1">' +
            '<div class=" readyByButton1 initialname green-name">' + userShortName + '</div></div>' +
            '<div class="col-11 col-md-11 col-lg-11 col-xl-11">' +
            '<div class="right-chat"><div class="chat-name">' + username + '</div>' +
            '<div class="chat-date"></div><div class="chat-desc"><p class="text-break">' + messageDescription + '</p></div></div>' +
            '</div></div></li>';

        $('#messageSection').html(localRow);
        AddClassIfAbsent('#notificationVesselFlag', 'd-none');
        if (!isChannelListPresentForCurrentUser()) {
            $(".chat-layout").css(
                "background", "white"
            );
        }
        if (($(window).width() > MobileScreenSize)) {
            console.log('add right screen filter')
            AddClassIfAbsent('.record-section', 'd-none');
            RemoveClassIfPresent('.record-section', 'd-md-block');
            AddClassIfAbsent('#messageVesselName', 'd-none');
            RemoveClassIfPresent('#messageVesselName', 'd-md-block');
        }
        //else {
        //    if ($(window).width() <= MobileScreenSize) {
        //        var divHtmlElement = '#chatsection';
        //        $(divHtmlElement).empty();
        //        var localRow = $('<li class="nav-item new-discussion-highlight d-block d-md-none welcommessagechat" style="display:block"><div class= "row mx-auto no-gutters"><div class="col-12 col-md-12 col-lg-12"><h1 class="currentchat">Welcome to V.Chat</h1><div class="clearfix"><div class="vessel-name float-left">Welcome to V.Chat, use this to communicate with your vessel contacts, or with others in the company.<br />Use the add button to create a new discussion or click the filter button to search the discussions.</div></div></div></div ></li >');
        //        $(divHtmlElement).append(localRow);
        //    }


        //}


    }
    else {

        RemoveClassIfPresent('#notificationVesselFlag', 'd-none');
        $('.fixed-mesage-box').show();
        $(".newdiscussion").text("New Discussion");
        if (($(window).width() > MobileScreenSize)) {
            console.log('remove right screen filter')
            AddClassIfAbsent('.record-section', 'd-none');
            AddClassIfAbsent('.record-section', 'd-md-block');
            RemoveClassIfPresent('#messageVesselName', 'd-none');
            AddClassIfAbsent('#messageVesselName', 'd-md-block');
        }
    }

}

function isChannelListPresentForCurrentUser() {
    let channelCount = parseInt($("#hdnChannelListCount").val());

    if (channelCount > 0) {
        return true;
    }
    else {
        return channelCount != 0;
    }
}


function UpdatesParticipants(channelId) {
    $.ajax({
        url: "/Notification/GetChannelParticipants",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('.new-discussion-layout');
        },
        success: function (data) {
            var CoockieUserId = GetCookie('NotificationUserId');

            if (data != null && data != '') {
                for (var i = 0; i < data.length; i++) {
                    let userDetails = data[i];
                    if (CoockieUserId !== userDetails.ssUserId) {
                        var obj = {
                            id: userDetails.ssUserId,
                            description: userDetails.userRoleDescription,
                            userShortName: userDetails.userShortName,
                            text: userDetails.username
                        };
                        var option = new Option(obj.text, obj.id, true, true);
                        $(option).data('raw', obj);
                        $('#participantdropdown').append(option).trigger('change');
                    }
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('.new-discussion-layout');
        }
    });
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
    else {

        if (objAttachedFiles.AttachedFiles.length > 0) {
            AttachedFiles = objAttachedFiles.AttachedFiles.slice();
            ResetDraftAttachedFilesVariables("");
        }
    }
}

function AddMessagingUserIfNotExists() {
    $.ajax({
        url: "/Notification/AddMessagingUserIfNotExists",
        type: "POST",
        dataType: "JSON",
        success: function (userDetails) {
        },
        complete: function () {
            GetCurrentUserDetails(function (userDetails) {
                $('#currentUserShortName').text(userDetails.userShortName);
                $('#currentUserName').text(userDetails.username);
                $('#currentUserPrimaryRoleName').text(userDetails.primaryRoleName);

                if (!isChannelListPresentForCurrentUser()) {
                    ShowWelcomeMessage(true);
                }
                else {
                    if ($(window).width() > MobileScreenSize) {
                        RemoveClassIfPresent('#li_0', 'd-md-block');
                    }
                }
            });
        }
    });
}

function AfterCreateChannelAction() {
    window.parent.chatCreateDiscussionEvent();
}


function GetChannelList(request) {

    var divHtmlElement = '#chatsection';
    $(divHtmlElement).empty();

    $.ajax({
        url: "/Notification/GetChannelList",
        type: "POST",
        dataType: "JSON",

        data: {
            "channelRequest": request,
            "sessionDetails": $('#hdnSessionStorageDetails').val()
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('.discussion-list');
        },
        success: function (data) {
            fn_BindChannelList(data, divHtmlElement);
        },

        error: async function () {
            let data = await fn_GetOfflineChannelList(request);
            fn_BindChannelList(data, divHtmlElement);
        },
        complete: function () {
            RemoveModelLoadingIndicator('.discussion-list');
            $.ajax({
                url: "/Notification/GetSessionStorageFilterForList",
                type: "POST",
                dataType: "JSON",
                data: {
                    "sessionDetails": $('#hdnSessionStorageDetails').val()
                },
                success: function (data) {
                    if (data != null) {
                        if (($(window).width() > MobileScreenSize)) {
                            if (data.selectedChannelId != null && data.selectedChannelId != 0) {
                                var ul = $('#chatsection');
                                var liclone = ul.find('#li_' + data.selectedChannelId);
                                if (liclone != null && liclone.length > 0) {
                                    liclone.find('.expandIcon')[0].click();
                                    liclone.find('.expandIcon')[0].scrollIntoView(true);
                                }
                                else {
                                    NewChannelDetailSelect(data.selectedChannelId)
                                }
                            }
                            else {
                                //default open first channel
                                //var li = $('#chatsection li:nth-child(2)');
                                //li.find('.expandIcon')[0].click();
                                //var notificationExpander = $('.expandIcon')[0];
                                //if (notificationExpander != null && notificationExpander != undefined) {
                                //    notificationExpander.click();
                                //}
                            }
                        }
                        else {
                            if (data.selectedChannelId != null && data.selectedChannelId != 0) {
                                var ul = $('#chatsection');
                                var liclone = ul.find('#li_' + data.selectedChannelId);
                                if (liclone != null && liclone.length > 0) {
                                    liclone.find('.expandIcon')[0].click();
                                    liclone.find('.expandIcon')[0].scrollIntoView(true);
                                }
                                else {
                                    NewChannelDetailSelect(data.selectedChannelId)
                                }
                            }
                        }
                    }
                }

            });
        }
    });
}

function fn_BindChannelList(data, divHtmlElement) {
    let channelData = data != null ? data.data : null;
    if (data != null && channelData != null && channelData.length > 0) {
        $('#hdnChannelListCount').val(data.totalCount);
        var newDiscussionRow = GetDiscussionRow();
        $(divHtmlElement).append(newDiscussionRow);
        ShowWelcomeMessage(false);
        $('.fixed-mesage-box').show();
        for (var i = 0; i < channelData.length; i++) {
            var row = RowCreatedChannel(channelData[i], channelData[i].className, channelData[i].isSaveAsDraft);
            $(divHtmlElement).append(row);
        }
    }
    else {
        //appending default new discussion row
        $('#hdnChannelListCount').val(0);
        var newDiscussionRow = GetDiscussionRow();
        $(divHtmlElement).append(newDiscussionRow);
        ShowWelcomeMessage(true);
    }

    //TODO:
    //Need to check with rashid
    //$(".expandIcon").click(ExpandNotificationDetail);
    $(".expandIcon").click(function () {

        let channelId = $('#hdnSelectedChannelId').val();
        let isSaveAsDraft = $("#hdnIsSaveAsDraft_" + channelId).val();

        if (isSaveAsDraft == "false" || isSaveAsDraft == "False" || isSaveAsDraft == false) {
            $("#meessageText").val('');
            $('#meessageText').attr('rows', 2);
            updateAttachedFilesArray("");
            DeleteAllDocument(AttachedFiles);
            ResetChatMessage();
            if (!IsNullOrEmptyOrUndefinedLooseTyped($("#hdnChannelDraftMessage_" + channelId).val())) {
                $("#meessageText").val($("#hdnChannelDraftMessage_" + channelId).val());
            }
        }
    });

    if (($(window).width() > MobileScreenSize)) {
        //default open first channel
        var notificationExpander = $('.expandIcon')[0];
        if (notificationExpander != null && notificationExpander != undefined) {
            notificationExpander.click();
        }
    }
}

function GetChannelListScrolled(request) {
    $.ajax({
        url: "/Notification/GetChannelList",
        type: "POST",
        dataType: "JSON",

        data: {
            "channelRequest": request,
            "sessionDetails": $('#hdnSessionStorageDetails').val()
        },
        beforeSend: function (xhr) {
            //RemoveClassIfPresent('.chat-paged-loader', 'd-none');
            //AddModelLoadingIndicator('.chat-paged-loader');
        },
        success: function (data) {
            fn_BindChannelListOnScroll(data);
        },

        error: async function (jqXHR, exception) {
            let data = await fn_GetOfflineChannelList(request);
            fn_BindChannelListOnScroll(data);
        },
        complete: function () {
        }
    });
}

async function fn_GetOfflineChannelList(request) {
    if (!vshipDb) {
        await createDB()
    }
    const pageLength = 100;
    const start = (pageLength * ((request.PageNumber || 1) - 1));
    let offlinedata = await vshipDb.getAll('ChatNotificationList');
    let finalData = offlinedata.filter(function (e) {
        return !convertStringToBool(request.isSearchClicked) ||
            e.title.includes(request.searchText)
    });
    let data = { hasNextScroll: finalData.length > (start + pageLength + 1), data: finalData.slice(start, start + pageLength), totalCount: finalData.length }
    return Promise.resolve(data);
}

function fn_BindChannelListOnScroll(data) {
    $('#hdnChatHasNextPage').val(data.hasNextScroll);
    let channelListData = data != null ? data.data : null;
    if (data != null && channelListData != null && channelListData.length > 0) {
        $('.fixed-mesage-box').show();
        for (var i = 0; i < channelListData.length; i++) {
            var row = RowCreatedChannel(channelListData[i], channelListData[i].className, channelListData[i].isSaveAsDraft);
            $('#chatsection').append(row);
        }
    }
}

function GetDiscussionRow() {
    var localRow = '';

    if ($(window).width() <= 767) {//mobile
        if (!isChannelListPresentForCurrentUser()) {//empty
            localRow = $('<li class="nav-item new-discussion-highlight d-block welcommessagechat" style="display:block" id="li_0"><div class= "row mx-auto no-gutters"><div class="col-12 col-md-12 col-lg-12"><h1 class="currentchat">Welcome to V.Chat</h1><div class="clearfix"><div class="vessel-name float-left">Welcome to V.Chat, use this to communicate with your vessel contacts, or with others in the company.<br />Use the add button to create a new discussion or click the filter button to search the discussions.</div></div></div></div ></li >');
        }
        else {//there
            localRow = $('<li class="nav-item new-discussion-highlight d-none welcommessagechat" style="display:block" id="li_0"><div class= "row mx-auto no-gutters"><div class="col-12 col-md-12 col-lg-12"><h1 class="currentchat">Welcome to V.Chat</h1><div class="clearfix"><div class="vessel-name float-left">Welcome to V.Chat, use this to communicate with your vessel contacts, or with others in the company.<br />Use the add button to create a new discussion or click the filter button to search the discussions.</div></div></div></div ></li >');
        }
    }
    else {//web
        if (isChannelListPresentForCurrentUser()) {//channel there
            localRow = $('<li class="nav-item new-discussion-highlight d-none" id="li_0" ><div class="row mx-auto no-gutters"><div class="col-md-12"><h1 class="currentchat newdiscussion">New Discussion</h1></div></div></li>');
        }
        else {// empty
            localRow = $('<li class="nav-item new-discussion-highlight d-none d-md-block" id="li_0" ><div class="row mx-auto no-gutters"><div class="col-md-12"><h1 class="currentchat newdiscussion">New Discussion</h1></div></div></li>');
        }
    }

    if (!$('#divSearchChannel').hasClass('d-none')) {
        return '';
    }
    return localRow;
}

function SetAttachedFilesCollection(messageId, FilesAttached, AttachedFiles) {
    let obj = {
        FilesAttached: FilesAttached,
        AttachedFiles: AttachedFiles
    };
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
        };

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


function BindNotesDetailsInVChat() {
    $.ajax({
        url: "/Notification/NoteDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "encryptedNoteId": $('#EncryptedNoteId').val()
        },
        success: function (data) {
            if (data != null) {
                $('#ReferenceIdentifier').val(data.referenceIdentifier);
                $('#ContextPayload').val(data.contextParams);
                $('#CategoryId').val(data.catId);
                $('#txtSubject').val(data.noteTitle);
                $('#txtMessage').val(data.noteDescription);

                if (!(IsNullOrEmptyOrUndefinedLooseTyped(data.vesselId) && IsNullOrEmptyOrUndefinedLooseTyped(data.vesselName))) {
                    UpdateSelectedVesselDropdown(data.vesselId, data.vesselName, '#cboNotificationVesselSearch', '#btnSearchParticipants');
                }

                if (!IsNullOrEmptyOrUndefinedLooseTyped($('#CategoryId').val()) && $('#CategoryId').val() != MessageCateory_General) {
                    RemoveClassIfPresent('#divAreaDropdown', 'd-none');
                    GetAreaList($('#CategoryId').val(), 'cboAreaSelection', function () {
                        RemoveModelLoadingIndicator('.new-discussion-layout');
                    });
                }
            }
        }
    });
}

function fn_CommonOfflineMessage() {
    if (!navigator.onLine) {
        alert('This feature is not allowed, app is Offline');
        return false;
    }
    return true;
}


function fn_GetOfflineData() {
    let request = {
        'PageNumber': 1,
    };
    $.ajax({
        url: "/Notification/GetChannelListForOfflineServe",
        type: "POST",
        dataType: "JSON",

        data: {
            "channelRequest": request,
            "sessionDetails": $('#hdnSessionStorageDetails').val()
        },
        success: async function (response) {
            if (!vshipDb) {
                await createDB();
            }
            let offlineData = await vshipDb.getAll('ChatNotificationList');
            offlineData.forEach(function (data, idx) {
                vshipDb.delete('ChatNotificationList', idx);
            });

            response.data.forEach(function (data, idx) {
                vshipDb.put('ChatNotificationList', idx, data);
            })
        }
    });

    $.ajax({
        url: "/Notification/GetChannelMessagesForOfflineServe",
        type: "POST",
        dataType: "JSON",

        data: {
            "sessionDetails": $('#hdnSessionStorageDetails').val()
        },
        success: async function (response) {
            if (!vshipDb) {
                await createDB();
            }
            let offlineData = await vshipDb.getAll('ChatNotificationDetails');
            offlineData.forEach(function (data, idx) {
                vshipDb.delete('ChatNotificationDetails', idx);
            });

            response.data.forEach(function (data, idx) {
                vshipDb.put('ChatNotificationDetails', idx, data);
            })
        }
    });
}

window.addEventListener('online', function (event) {
    fn_DeleteOfflineDeletedChannels();
});

function convertStringToBool(inputString) {
    inputString = inputString || "";
    if (typeof (inputString) == "string")
        inputString = inputString.toLowerCase();

    switch (inputString) {
        case "1":
        case "true":
        case "yes":
        case "y":
        case 1:
        case true:
            return true;
            break;

        default: return false;
    }
}

async function fn_DeleteOfflineDeletedChannels() {
    if (!vshipDb) {
        await createDB();
    }
    let offlineDeleteddata = await vshipDb.getAll('ChatChannelsDeleted');
    offlineDeleteddata.forEach(function (d, idx) {
        $.ajax({
            url: "/Notification/DeleteChannelById",
            type: "GET",
            dataType: "JSON",
            data: {
                "channelId": channelId
            },
            success: function (response) { }
        });
    })
    Promise.resolve();
}
