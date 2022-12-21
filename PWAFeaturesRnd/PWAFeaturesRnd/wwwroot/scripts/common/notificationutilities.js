import { remove } from "toastr";
import moment from "moment";
import { ErrorLog, GetCookie, ToastrAlert, GetStringNullOrWhiteSpace, IsNullOrEmptyOrUndefined, RemoveClassIfPresent, AddClassIfAbsent, AddModelLoadingIndicator, RemoveModelLoadingIndicator, IsNullOrEmpty, base64ToArrayBuffer, saveByteArray, IsNullOrEmptyOrUndefinedLooseTyped } from "../common/utilities.js";
import { FileAttachmentLimit, FileAttachmentLimitErrorMessage, MessageCateory_General, DeleteMessageTemplate, MessageStatus_UserLeftDiscussion, MessageStatus_UserAddedToTheDiscussion, MobileScreenSize, NotificationPageKey } from "../common/constants.js";
import 'signalR'
var currentUser;
var recordSectionHeight; //height is being changed due to some unknown reason, hence caching the value on first load
var selectedFileCount = 0;
var processedFileCount = 0;

var DraftAttachmentObj = {
    AttachedFiles: [],
    FilesAttached: 0
}

var NotificationObj = {
    SelectedVesselName: '',
    SelectedVesselId: '',
    PreSelectedUsers: [],
    SelectedUser: [],
    AttachedFiles: [],
    FilesAttached: 0,
    headerIsAppend: false,
    IsMobile: false,
    AttachedFilesCol: new Map()
}

var idb = function (e) { "use strict"; let t, n; const r = new WeakMap, o = new WeakMap, s = new WeakMap, a = new WeakMap, i = new WeakMap; let c = { get(e, t, n) { if (e instanceof IDBTransaction) { if ("done" === t) return o.get(e); if ("objectStoreNames" === t) return e.objectStoreNames || s.get(e); if ("store" === t) return n.objectStoreNames[1] ? void 0 : n.objectStore(n.objectStoreNames[0]) } return p(e[t]) }, set: (e, t, n) => (e[t] = n, !0), has: (e, t) => e instanceof IDBTransaction && ("done" === t || "store" === t) || t in e }; function u(e) { return e !== IDBDatabase.prototype.transaction || "objectStoreNames" in IDBTransaction.prototype ? (n || (n = [IDBCursor.prototype.advance, IDBCursor.prototype.continue, IDBCursor.prototype.continuePrimaryKey])).includes(e) ? function (...t) { return e.apply(f(this), t), p(r.get(this)) } : function (...t) { return p(e.apply(f(this), t)) } : function (t, ...n) { const r = e.call(f(this), t, ...n); return s.set(r, t.sort ? t.sort() : [t]), p(r) } } function d(e) { return "function" == typeof e ? u(e) : (e instanceof IDBTransaction && function (e) { if (o.has(e)) return; const t = new Promise((t, n) => { const r = () => { e.removeEventListener("complete", o), e.removeEventListener("error", s), e.removeEventListener("abort", s) }, o = () => { t(), r() }, s = () => { n(e.error || new DOMException("AbortError", "AbortError")), r() }; e.addEventListener("complete", o), e.addEventListener("error", s), e.addEventListener("abort", s) }); o.set(e, t) }(e), n = e, (t || (t = [IDBDatabase, IDBObjectStore, IDBIndex, IDBCursor, IDBTransaction])).some(e => n instanceof e) ? new Proxy(e, c) : e); var n } function p(e) { if (e instanceof IDBRequest) return function (e) { const t = new Promise((t, n) => { const r = () => { e.removeEventListener("success", o), e.removeEventListener("error", s) }, o = () => { t(p(e.result)), r() }, s = () => { n(e.error), r() }; e.addEventListener("success", o), e.addEventListener("error", s) }); return t.then(t => { t instanceof IDBCursor && r.set(t, e) }).catch(() => { }), i.set(t, e), t }(e); if (a.has(e)) return a.get(e); const t = d(e); return t !== e && (a.set(e, t), i.set(t, e)), t } const f = e => i.get(e); const l = ["get", "getKey", "getAll", "getAllKeys", "count"], D = ["put", "add", "delete", "clear"], v = new Map; function b(e, t) { if (!(e instanceof IDBDatabase) || t in e || "string" != typeof t) return; if (v.get(t)) return v.get(t); const n = t.replace(/FromIndex$/, ""), r = t !== n, o = D.includes(n); if (!(n in (r ? IDBIndex : IDBObjectStore).prototype) || !o && !l.includes(n)) return; const s = async function (e, ...t) { const s = this.transaction(e, o ? "readwrite" : "readonly"); let a = s.store; r && (a = a.index(t.shift())); const i = a[n](...t); return o && await s.done, i }; return v.set(t, s), s } return c = (e => ({ ...e, get: (t, n, r) => b(t, n) || e.get(t, n, r), has: (t, n) => !!b(t, n) || e.has(t, n) }))(c), e.deleteDB = function (e, { blocked: t } = {}) { const n = indexedDB.deleteDatabase(e); return t && n.addEventListener("blocked", () => t()), p(n).then(() => { }) }, e.openDB = function (e, t, { blocked: n, upgrade: r, blocking: o, terminated: s } = {}) { const a = indexedDB.open(e, t), i = p(a); return r && a.addEventListener("upgradeneeded", e => { r(p(a.result), e.oldVersion, e.newVersion, p(a.transaction)) }), n && a.addEventListener("blocked", () => n()), i.then(e => { s && e.addEventListener("close", () => s()), o && e.addEventListener("versionchange", () => o()) }).catch(() => { }), i }, e.unwrap = f, e.wrap = p, e }({});
var vshipDb;
async function createDB() {
    const db = idb.openDB("Test", 2, {
        upgrade(db) {
            db.createObjectStore("modelCachedData");
            db.createObjectStore("appMetaData")
            db.createObjectStore("ChatNotificationList");
            db.createObjectStore("ChatNotificationDetails");
            db.createObjectStore('ChatChannelsDeleted');
            db.createObjectStore('POSTRequests');
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
        },
        getAllKeys: async (storeName) => (await db).transaction(storeName).store.getAllKeys()
    };
    return Promise.resolve();
}


export function GetNotificationObject() {
    return NotificationObj;
}

export function NewChannelMessage(channelId) {
    $.ajax({
        url: "/Notification/GetNewChannelMessages",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },

        success: function (data) {
            if (data != null) {
                $('#messageSection .lastRow').removeClass('lastRow');
                let msgs = CreateTemplateNewMessage(data, channelId);
                for (var i = 0; i < msgs.length; i++) {
                    let msg = msgs[i];
                    if (data[i].ssUserId == GetCookie('NotificationUserId')) {
                        let searchResults = $(".chat-desc[data-msgid='" + data[i].messageId + "']");
                        if (searchResults != null && searchResults.length > 0) {
                            let existingDiv = searchResults[0];
                            let parent = $(existingDiv).parents('li');
                            $(parent).replaceWith(msg);
                        } else {
                            $('#messageSection').append(msg);
                        }
                    } else {
                        $('#messageSection').append(msg);
                    }
                }
            }
        },
        complete: function () {
            var ulScroll = $('#messageSection .lastRow')[0];
            if (ulScroll != null && ulScroll != undefined) {
                ulScroll.scrollIntoView(true);
            }
            $('.dropdown-toggle').dropdown();

            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

export function NewChannelDetailActive(channelId) {
    $.ajax({
        url: "/Notification/GetChannelDetail",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },

        success: function (data) {
            if (data != null) {
                var ul = $('#chatsection');
                var liclone = ul.find('#li_' + channelId).clone(true);
                ul.find('#li_' + channelId).remove();
                $('#li_0').after(liclone);
                $('#modifiedDate_' + data.channelId).html(data.recievedDate);
            }
        }
    });
}

export function NewChannelDetailUnread(channelId) {

    $.ajax({
        url: "/Notification/GetChannelDetailById",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },

        success: function (data) {
            if (data != null) {
                var ul = $('#chatsection');
                var liclone = ul.find('#li_' + channelId);
                if (liclone != null && liclone.length > 0) {
                    if (($(window).width() > 767)) {
                        AddClassIfAbsent(liclone.find('.expandIcon'), 'unread')
                        AddClassIfAbsent(liclone.find('a:eq(1)'), 'mobile-link-active')
                        $('#li_0').after(liclone);
                        $('#modifiedDate_' + data.channelId).html(data.recievedDate);
                    }
                    else {
                        liclone.remove();
                        $('#li_0').after(RowCreatedChannel(data, 'unread'));
                        var ul = $('#chatsection');
                        var liclone = ul.find('#li_' + channelId);
                        liclone.find('.expandIcon').click(ExpandNotificationDetail);
                    }
                }
                else {
                    $('#li_0').after(RowCreatedChannel(data, 'unread'));
                    var ul = $('#chatsection');
                    var liclone = ul.find('#li_' + channelId);
                    liclone.find('.expandIcon').click(ExpandNotificationDetail);
                }
            }
        },
        complete: function () {
            GetNoticationUnreadChannelCount();
        }
    });
}

export function NewChannelDetailSelect(channelId) {

    $.ajax({
        url: "/Notification/GetChannelDetailById",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },

        success: function (data) {
            if (data != null) {

                $('#li_0').after(RowCreatedChannel(data, ''));
                var ul = $('#chatsection');
                var liclone = ul.find('#li_' + channelId);
                liclone.find('.expandIcon').click(ExpandNotificationDetail);
                liclone.find('.expandIcon')[0].click();
            }
        }
    });
}

function CreateTemplateNewMessage(data, channelId) {
    let msgs = [];
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let userDetails = data[i];
            var lastRowclass = ''
            if (i == data.length - 1) {
                lastRowclass = lastRowclass + 'class="lastRow"'
            }
            var $newRow = RowCreated(userDetails, lastRowclass);
            msgs.push($newRow);
        }
    }
    return msgs;
}

//function RowCreatedNewMessage(result, lastRowclass) {
//    var $localRow = $('<li ' + lastRowclass + '><div class="row mx-auto no-gutters"><div class="col-1 col-md-1 col-lg-1 col-xl-1"><div class="initialname green-name">' + result.userShortName + '</div>' +
//        '</div><div class="col-11 col-md-11 col-lg-11 col-xl-11"><div class="right-chat"><div class="chat-name">' + result.username + '</div>' +
//        '<div class="chat-date">' + result.createdOnUTCDateFormat + '</div><div class="chat-desc"><p>' + result.messageDescription + '</p></div>' +
//        '</div></div></div></li>');

//    return $localRow;
//}

function RowCreatedParticipants(result) {
    var localRow = $('<li><div class="row mx-auto no-gutters">' +
        '<div class="col-2 col-md-4 col-lg-3 col-xl-3">' +
        '<div class="initialname green-name">' + result.userShortName + '</div></div>' +
        '<div class="col-10 col-md-8 col-lg-9 col-xl-9 my-auto">' +
        '<div class="right-chat"><div class="chat-name">' + result.username + '</div><div class="type">' + result.userRoleDescription + '</div ><span class="online d-none"></span><span class="offline d-none"></span></div > ' +
        '</div ></div ></li >');

    return localRow;
}

export function RowCreatedChannel(result, className, isSaveAsDraft) {

    let SaveAsDraft = '';
    if (result.isSaveAsDraft == true || result.isSaveAsDraft == 'true') {
        SaveAsDraft = '<div class="clearfix" id="divDraft_' + result.channelId + '" ><div class="vessel-name float-left"><i>Draft</i></div></div>'
    }

    let chatDetailMobileNavigation = '';
    if ($(window).width() <= MobileScreenSize) {
        if (result.isSaveAsDraft == true || result.isSaveAsDraft == 'true') {
            chatDetailMobileNavigation = '<a href="/Notification/NotificationMobileDiscussion/?request=' + result.notificationMobileDiscussionUrl + '" class="d-block d-md-none">'
        }
        else {
            chatDetailMobileNavigation = '<a href="/Notification/NotificationMobileChatDetail/?request=' + result.notificationMobileChatDetailURL + '" class="d-block d-md-none nav-link expandIcon ' + className + '">'
        }
    }
    else {
        chatDetailMobileNavigation = '<a id="' + result.channelId + '" class="nav-link d-none d-md-block expandIcon ' + className + '" data-toggle="tab" href="#one-tab" role="tab">';
    }

    let chatTitle = '<h1 class="currentchat float-left" id="Title_' + result.channelId + '">' + ShortChatTitle(result.title) + '&nbsp;</h1>';

    let vesselRow = '';
    if (!IsNullOrEmptyOrUndefinedLooseTyped(result.vesselName)) {
        vesselRow = '<div class="clearfix mobilemargin"> <div class="vessel-name float-left" id="VesselName_' + result.channelId + '"> <img class="mr-1" src="/images/vesseltopicon.svg" style="height:15px"> <span class="align-middle"> ' + result.vesselName + '</span>' + ' </div></div>';
    }

    let participantsInitials = '';
    if (!IsNullOrEmptyOrUndefinedLooseTyped(result.participantsInitials)) {
        participantsInitials = '<h1 class="currentchat initialchat">'
            + '<img class="initialchat" src="/images/initialsuser.svg" height="14"> '
            + '<span>' + result.participantsInitials + '</span>'
            + '</h1>'
    }

    let showLastSentMessage = '';
    let strLastMessageDetails = '';
    let SenderName = IsNullOrEmptyOrUndefinedLooseTyped(result.lastSenderName) ? '' : result.lastSenderName.split(" ")[0];
    if (result.isAttachment) {
        strLastMessageDetails = '<i class="fa fa-paperclip mr-1 text-teal"></i>';
    }
    if (!IsNullOrEmptyOrUndefinedLooseTyped(result.lastSentMessageInChannel) || result.isAttachment) {
        showLastSentMessage = '<div class="vessel-name float-left messagediv"> <img class="mr-1" src="/images/messagetabs.svg" width="14"/> ' + SenderName + ' : ' + strLastMessageDetails + result.lastSentMessageInChannel + '</div>';
    }

    let unreadMessageCount = ''
    if (result.unreadMessageCount > 0) {
        unreadMessageCount = '<div class= "discussionlistcount">' + result.unreadMessageCount + '</div>'
    }

    let isSyncIconToShow = !convertStringToBool(result.isPendingToSync) ? "d-none" : "";

    var localRow =
        $('<li class="nav-item" id="li_' + result.channelId + '">'
            + '<input type="hidden" id="hdnTitle_' + result.channelId + '" value="' + result.title + '" />'
            + '<input type="hidden" id="hdnVesselId_' + result.channelId + '" value="' + result.vesselId + '" />'
            + '<input type="hidden" id="hdnVesselName_' + result.channelId + '" value="' + result.vesselName + '" />'
            + '<input type="hidden" id="hdnIMONumber_' + result.channelId + '" value="' + result.vesselIMONumber + '" />'
            + '<input type="hidden" id="hdnIsOneToOneChat_' + result.channelId + '" value="' + result.isOneToOneChat + '" />'
            + '<input type="hidden" id="hdnParticipantsCount_' + result.channelId + '" value="" />'
            + '<input type="hidden" id="hdnIsSaveAsDraft_' + result.channelId + '" value="' + result.isSaveAsDraft + '" />'
            + '<input type="hidden" id="hdnCategoryId_' + result.channelId + '" value="' + result.categoryId + '" />'
            + '<input type="hidden" id="hdnIsGeneralCat_' + result.channelId + '" value="' + result.isGeneralCat + '" />'
            + '<input type="hidden" id="hdnChannelDraftMessage_' + result.channelId + '" value="" class="ChannelDraftMessage" />'
            //+ chatDetailMobileNavigation
            //	+ '<div class="row mx-auto no-gutters">'
            //		+ '<div class="col-10 col-md-10 col-lg-10">'
            //			+ '<div class="clearfix">'
            //				+ chatTitle
            //			+ '</div>'
            //			+ vesselRow
            //			+ participantsInitials
            //			+ '<div class="clearfix">'
            //				+ showLastSentMessage
            //			+ '</div>'
            //			+ SaveAsDraft
            //		+ '</div>'
            //		+ '<div class="col-2 col-md-2 col-lg-2 mt-auto mx-auto">'
            //			+ '<div class="date">'
            //				+ result.recievedDate
            //			+ '</div>'
            //		+ '</div>'
            //	+ '</div>'
            //+ '</a>'
            //+ '<button type="button" class="float-right deleteChannel btn p-0" data-channelid="' + result.channelId + '">'
            //+ '<i class="fa fa-trash-alt color-delete-grey"></i>'
            //+ '</button>'
            + chatDetailMobileNavigation
            + '<div class="row mx-auto no-gutters">'
            + '<div class="col-10 col-md-10 col-lg-10">'
            + '<div class="clearfix">'
            + chatTitle
            + '</div>'
            + vesselRow
            + participantsInitials
            + '<div class="clearfix">'
            + showLastSentMessage
            + '</div>'
            + SaveAsDraft
            + '</div>'
            + '<div class="col-2 col-md-2 col-lg-2">'
            + '<div class="row mx-auto no-gutters height-100-percent">'
            + '<div class="col-12">'
            + unreadMessageCount
            + '</div>'
            + '<div class="col-12 justify-content-end mt-auto">'
            + '<div id="divSyncPending_' + result.channelId + '" class="sync-pending ' + isSyncIconToShow + '">'
            + '<i class="fa fa-refresh" title="sync pending"></i>'
            + '</div>'
            + '<div class="date"> '
            + result.recievedDate
            + ' </div>'
            + '</div>'
            + '</div>'
            + '</div>'
            + '</div>'
            + '</a >'
            + '<button type="button" class="float-right deleteChannel btn p-0" data-channelid="' + result.channelId + '">'
            + '<i class="fa fa-trash-alt color-delete-grey"></i>'
            + '</button>'
            + '</li > ');
    return localRow;
}

//it is only used in notification js for loading data on web when clicked on channel.
//wehn clicked in mobile device it has anchor tag and on the basis of that screen is loaded so had to add the condition of screen size
export function ShowNotificationDraftDiscardConfBox() {
    if (($(window).width() > 767)) {
        var channelId = $(this).attr('id');
        $("#hdnNotificationDraftChannelId").val(channelId);
        $('.discussion-list li').find('.nav-link').removeClass("currentdiscussion");
        var isValid = ValidationBeforeCreateDraft('#txtSubject', '#txtMessage', '#participantdropdown');
        if ($('#hdnIsAllowDrafrtConfBox').val() == "Yes" && isValid) {
            $("#notificationDraft").modal('show');
        }
        else {
            ExpandNotificationDetail(channelId);
        }
    }
}

function SetSessionStorageFilter(request) {
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
            }
        }
    });
}


export function ExpandNotificationDetail(_channelId = null) {

    var searchText = GetStringNullOrWhiteSpace($('#inputSearchChannel').val());
    var _isSearch = false;
    if (!IsNullOrEmptyOrUndefinedLooseTyped(searchText)) {
        _isSearch = true;
    }
    let request = {
        searchText: searchText,
        isSearchClicked: _isSearch,
        selectedChannelId: _channelId,
    };
    SetSessionStorageFilter(request);

    $('#hdnIsAllowDrafrtConfBox').val("No");
    RemoveClassIfPresent('#li_0', 'd-md-block')
    var chId = $(this).attr('id');
    var channelId = IsNullOrEmptyOrUndefinedLooseTyped(chId) ? _channelId : chId;
    let isSaveAsDraft = false;
    if (!isNaN(channelId)) {
        let idSelector = "#hdnIsSaveAsDraft_" + channelId;
        isSaveAsDraft = $(idSelector).val();
    }

    $('#hdnSelectedChannelId').val(channelId);
    RemoveClassIfPresent('.addparticipant', 'd-none');

    if (recordSectionHeight == 'undefined' || recordSectionHeight == undefined) {
        recordSectionHeight = $(".discussion-list, .hideleftmenuheader iframe").height();
    }
    HideNewMessageAlert();
    RemoveClassIfPresent($("#" + channelId), 'unread');
    if (isSaveAsDraft == "false" || isSaveAsDraft == "False" || isSaveAsDraft == false) {

        $('#hdnCurrentPageNumber').val(1);
        $('#hdnHasNextPage').val(true);
        $('#hdnIsNewChannelSelected').val(true);

        ChannelMessage(channelId, false);
        if ($('#hdnIsGeneralCat_' + channelId).val() == 'false') {
            RemoveClassIfPresent(".recordLevelDetails", 'd-none');
            SetRecordAndParticipantHeight(recordSectionHeight);
            GetRecordLevelDetails(channelId);
            //GetChannelRecordDetails(channelId, $('#hdnVesselId_' + channelId).val());
        } else {
            AddClassIfAbsent(".recordLevelDetails", 'd-none');
            SetRecordAndParticipantHeight(recordSectionHeight);
        }
        Participants(channelId)
    }
    else {
        saveasDraftDetails(channelId);
    }
}

export function HideNewMessageAlert() {
    AddClassIfAbsent('#divNewMessageAlert', 'd-none');
}

export function ChannelMessage(channelId, isScrolled) {

    LoadChatMessageScreen();
    HideNewDiscussionSlot();
    $('#messageTitle').text($('#hdnTitle_' + channelId).val())
    $('#messageVesselName').html($('#hdnVesselName_' + channelId).val() + " <span>" + $('#hdnIMONumber_' + channelId).val() + "</span>");
    let vesselname = $('#hdnVesselName_' + channelId).val();
    if (!IsNullOrEmptyOrUndefinedLooseTyped(vesselname)) {
        RemoveClassIfPresent('#notificationVesselFlag', 'd-none');
    } else {
        AddClassIfAbsent('#notificationVesselFlag', 'd-none');
    }
    var ul = $('#chatsection');
    var litag = ul.find('#li_' + channelId);
    litag.removeClass('unread');
    $.ajax({
        url: "/Notification/GetChannelMessages",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId,
            "IsScrolled": isScrolled,
            "PageNumber": $('#hdnCurrentPageNumber').val()
        },
        beforeSend: function (xhr) {
            if (!isScrolled) {
                AddModelLoadingIndicator('#divChatMessages');
                //RemoveClassIfPresent($('#dummyMessages'), 'd-none');
                $('#messageSection').empty();
            }
        },
        success: function (data) {
            //AddClassIfAbsent($('#dummyMessages'), 'd-none');
            fn_BindChannelMessages(data, isScrolled);
        },
        error: async function (jqXHR, exception) {
            const pageNumber = $('#hdnCurrentPageNumber').val() || 1
            let data = await fn_GetOfflinedChannelMessaged(channelId, pageNumber)
            fn_BindChannelMessages(data, isScrolled);
        },
        complete: function () {
            setTimeout(function () {
                RemoveModelLoadingIndicator('#divChatMessages');
                GetNoticationUnreadChannelCount();
                if (!isScrolled) {
                    var ulScroll = $('#messageSection .lastRow')[0];
                    if (ulScroll != null && ulScroll != undefined) {
                        ulScroll.scrollIntoView(true);
                    }
                }
                else {
                    var ulScroll = $('#messageSection .latestRow')[0];
                    if (ulScroll != null && ulScroll != undefined) {
                        ulScroll.scrollIntoView(true);
                    }
                }
                $('.dropdown-toggle').dropdown();
                $('[data-toggle="tooltip"]').tooltip();
                $('#hdnIsNewChannelSelected').val(false);
                $("#messageSection").css("margin-top", $(".chat-header").outerHeight(true));
            }, 5000)
        }
    });
}

async function fn_GetOfflinedChannelMessaged(channelId, PageNumber) {
    if (!vshipDb) {
        await createDB();
    }
    const pageLength = 10;
    const start = (pageLength * ((PageNumber || 1) - 1));
    let offlinedata = await vshipDb.getAll('ChatNotificationDetails');
    let finalData = offlinedata.filter(function (e) {
        return e.channelId == channelId
            && convertStringToBool(e['isDeleted']) == false
    });
    let data = { hasNextScroll: finalData.length > (start + pageLength + 1), data: finalData.slice(start, start + pageLength) }
    return Promise.resolve(data);
}

function fn_BindChannelMessages(data, isScrolled) {
    if (data != null && data.data != null) {
        if (!isScrolled) {
            CreateTemplate(data.data);
        }
        else {
            LoadMoreMessagesCreateTemplate(data.data);
        }
        $('#hdnHasNextPage').val(data.hasNextScroll);
    }
}

//not being used at the moment
export function GetSeenOrDeliveredMsgs(channelId) {
    $.ajax({
        url: "/Notification/GetLastReadDeliveredMessageForChannel",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": channelId
        },
        success: function (data) {
            if (!IsNullOrEmpty(data)) {
                for (var i = 0; i < data.length; i++) {
                    let messageDetails = data[i];
                    if (messageDetails.isSeen == true) {
                        RemoveClassIfPresent($('.seen' + '.' + messageDetails.messageId), 'd-none');
                    }
                    else if (messageDetails.isDelivered == true) {
                        RemoveClassIfPresent($('.sent' + '.' + messageDetails.messageId), 'd-none');
                    }
                }
            }
        },
        complete: function () {

        }
    });
}

//not used
export function GetMyLastAllReadMsgForChannel(channelId) {
    $.ajax({
        url: "/Notification/GetMyLastAllReadMsgForChannel",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": channelId
        },
        success: function (data) {
            console.table(data);
            if (!IsNullOrEmpty(data)) {
                let messageDetails = data;
                if (GetCookie('NotificationUserId') == messageDetails.ssUserId) {
                    //HideAllSeen();
                    RemoveClassIfPresent($('.seen' + '.' + messageDetails.id), 'd-none');
                    AddClassIfAbsent($('.sent' + '.' + messageDetails.id), 'd-none');
                }
            }
        },
        complete: function () {

        }
    });
}

export function HideAllSeen() {
    let seenElements = $('.seen');
    for (var i = 0; i < seenElements.length; i++) {
        let el = seenElements[i];
        AddClassIfAbsent(el, 'd-none');
    }
}

export function HideAllSent() {
    let sentElements = $('.sent');
    for (var i = 0; i < sentElements.length; i++) {
        let el = sentElements[i];
        AddClassIfAbsent(el, 'd-none');
    }
}

export function SetLastMessageSentStatus() {
    RemoveClassIfPresent($('.lastRow:last').find('.sent'), 'd-none');
}

export function SetSeenForChannelId(channelId) {
    //set all the messages for channelId to seen
    let sentElements = $('.seen' + '.' + channelId);
    for (var i = 0; i < sentElements.length; i++) {
        let el = sentElements[i];
        let wholeMessageDiv = $(el).parents('li')[0];
        let retryElement = $(wholeMessageDiv).find('.retry')[0];

        //if retry is visible dont do anything because the retry div is dummy
        if ($(retryElement).hasClass('d-none')) {
            let parentChatDesc = $(el).parents('.chat-desc')[0];
            let messageId = $(parentChatDesc).data('msgid');
            SetReadParticipantsCount(messageId, el);
        }
    }
}

export function LoadChatMessageScreen() {
    $(".chat-layout").show();
    AddClassIfAbsent(".new-discussion-layout", 'd-none');
}

export function HideNewDiscussionSlot() {
    $(".discussion-list li.new-discussion-highlight").hide();
    $('.discussion-list li').find('.nav-link.currentdiscussion').addClass("active");
    $('.discussion-list li').find('.nav-link').removeClass("currentdiscussion");
}

function CreateTemplate(data) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let userDetails = data[i];
            var lastRowclass = ''
            if (i == data.length - 1) {
                lastRowclass = lastRowclass + 'class="lastRow"'
            }
            var newRow = RowCreated(userDetails, lastRowclass);
            $('#messageSection').append(newRow);
        }
    }
}

function RowCreated(result, lastRowclass) {
    let isretryVisible = !convertStringToBool(result.isRetry) ? "d-none" : "";
    let isSeenClass = result.isSeen == true ? '' : 'd-none';
    let isSentClass = result.isSent == true ? '' : 'd-none';
    let canReplyPrivatelyClass = result.ssUserId == GetCookie('NotificationUserId') ? 'd-none ' : '';
    let isMessageDescription = result.messageDescription == null ? ' d-none' : '';
    let MessageOptionsVisiblity = result.ssUserId == GetCookie('NotificationUserId') ? '' : 'd-none';
    //data - toggle="dropdown" -- this is causing the issue of button not working for the 2nd time, this is removed from button tag

    let isCurrentUserSenderOfMessage = result.ssUserId == GetCookie('NotificationUserId') ? ' usercurrentchat ' : ' otheruserchat ';
    var localRow;
    if (result.status === MessageStatus_UserLeftDiscussion) {
        localRow = $('<li ' + lastRowclass + '>'
            + '<div class="row mx-auto no-gutters leftdiscussiondiv">'
            + '<div class="col-1 col-md-1 col-lg-1 col-xl-1">'
            + ''
            + '</div>'
            + '<div class="col-11 col-md-11 col-lg-11 col-xl-11">'
            + '<div class="right-chat"><div class="chat-date">' + result.createdOnUTCDateFormat + '</div>'
            + '<span class="chat-desc"><p><i class="fa fa-user-minus font-size-sm mt-1 mr-2"></i><i>' + result.messageDescription + '</i></p></span>'
            + '</div></div>'
            + '</div>'
            + '</li>');
    }
    else if (result.status === MessageStatus_UserAddedToTheDiscussion) {
        localRow = $('<li ' + lastRowclass + '>'
            + '<div class="row mx-auto no-gutters addedrow useradddiv">'
            + '<div class="col-1 col-md-1 col-lg-1 col-xl-1">'
            + ''
            + '</div>'
            + '<div class="col-11 col-md-11 col-lg-11 col-xl-11">'
            + '<div class="right-chat"><div class="chat-date">' + result.createdOnUTCDateFormat + '</div>'
            + '<span class="chat-desc"><p><i class="fa fa-user-plus font-size-sm mt-1 mr-2"></i><i>' + result.messageDescription + '</i></p></span>'
            + '</div></div>'
            + '</div>'
            + '</li>');
    }
    else {

        let editedLable = '';
        if (result.isMessageEdited == true) {
            editedLable = '<span class="chat-date edited-style font-italic">Edited</span>';
        }
        else {
            editedLable = '<span class="chat-date edited-style font-italic d-none">Edited</span>';
        }

        localRow = $('<li ' + lastRowclass + '><div class="row mx-auto no-gutters' + isCurrentUserSenderOfMessage + '"><div class="col-1 col-md-1 col-lg-1 col-xl-1"><div class=" readyByButton1 initialname green-name">' + result.userShortName + '</div>' +
            '</div><div class="col-11 col-md-11 col-lg-11 col-xl-11"><div class="right-chat editchat' + result.messageId + '"><div class="chat-name">' + result.username + '<a href="javascript:void(0);" class="ml-2 ' + canReplyPrivatelyClass + 'replyPrivately" data-usrid="' + result.ssUserId + '" data-userShortName="' + result.userShortName + '" data-username="' + result.username + '" data-channelId="' + result.channelId + '" data-toggle="tooltip" data-placement="bottom" title="Create new chat with this participant"><i class="fa fa-reply"></i></a>' + '</div>' +
            '<div class="chat-date d-inline-block mr-3">' + result.createdOnUTCDateFormat + '</div>' + editedLable + '<div class="chat-desc" data-isunread="' + result.isUnreadMessage + '" data-msgid="' + result.messageId + '" data-channelid="' + result.channelId + '"><p class="text-break ' + isMessageDescription + '">' + result.messageDescription + '</p><i class="fa fa-fw sent ' + result.messageId + ' ' + isSentClass + ' ' + result.channelId + '" data-toggle="tooltip" data-placement="bottom" aria-hidden="true" title="Sent"></i><i class="fa fa-redo retry ' + isretryVisible + '" aria-hidden="true" data-toggle="tooltip" data-placement="bottom" title="Retry"></i> <div class="dropdown-notify btn-group ' + isSeenClass + ' "> <button type="button"  aria-haspopup="true" aria-expanded="false" class="btn p-0 readByButton" ><img class="seen ' + result.messageId + ' ' + isSeenClass + ' ' + result.channelId + '" data-toggle="tooltip" data-placement="bottom" title="Seen" src="/images/mobilechatseen.svg" width="17" height="13px"/><span class="d-inline-block d-md-none seencount" id="seencount_' + result.messageId + '" >' + result.readParticipantCount + '</span></button><ul class="dropdown-menu dropdown-menu-right dropdown-list readByDropdown" role="menu"></ul></div>' +
            '<div class="dropdown-notify message-options btn-group ' + MessageOptionsVisiblity + '">\
            <button type="button" class="btn dropdown-toggle p-0" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-fw" aria-hidden="true" title=""></i></button>\
            <ul class="dropdown-menu dropdown-list message-options-list" role="menu">\
                <li class="cursor-pointer editMessage"><img src="/images/edit-notes.svg"> Edit</li>\
                <li class="cursor-pointer deleteMessage"><img src="/images/delete-notes.svg"> Delete</li>\
            </ul>\
         </div>'
            + ' </div>' +
            '</div></div></div></li>');

        if (result.isAttachment == true && result.attachments != null) {
            let attachments = result.attachments;
            $(localRow).find('.right-chat').append('<div class="attachmentsdiv">');
            for (var i = 0; i < attachments.length; i++) {
                let attachment = attachments[i];
                $(localRow).find('.attachmentsdiv').append(GetAttachmentView(attachment, result.messageId));
            }
            $(localRow).find('.attachmentsdiv').append('</div>');
        }
    }

    return localRow;
}

function GetAttachmentView(obj, MessageId) {
    let ettId = obj.ettId;
    let fileName = obj.description + obj.fileExtension;
    let row = '<div class="attached-files mt-2 attachmentClick attachmentView' + MessageId + ' " data-seq="' + obj.sequence + '" data-id="' + ettId + '" data-desc="' + obj.description + '" data-type="' + obj.fileExtension + '">' + GetAttachmentIcon(obj.fileExtension) + '<div class="attachment-text">' + fileName + '</div></div >';
    return row;
}

export function GetAttachmentViewForEditMessage(attachments, MessageId) {
    $(".attachmentView" + MessageId).remove();
    if (attachments != null) {
        for (var i = 0; i < attachments.length; i++) {
            let attachment = attachments[i];
            $('.editchat' + MessageId).append(GetAttachmentView(attachment, MessageId));
        }
    }
}

export function GetAttachmentIcon(extension) {
    extension = extension.toLowerCase();
    let result = '';

    let text = '<img src="/images/atatched-files.png">';
    let word = '<img src="/images/word.png">';
    let excel = '<img src="/images/excel.png">';
    let pdf = '<img src="/images/pdf.png">';
    let image = '<img src="/images/image.png">';


    if (extension.includes(".pdf")) {
        result = pdf;
    }
    else if (extension.includes(".xls")
        || extension.includes(".xlsx")
        || extension.includes(".xlsm")) {

        result = excel;
    }
    else if (extension.includes(".doc")
        || extension.includes(".docx")
        || extension.includes(".odt")
        || extension.includes(".ppt")
        || extension.includes(".pot")
        || extension.includes(".ppsx")
        || extension.includes(".pptx")
        || extension.includes(".pps")) {

        result = word;
    }
    else if (extension.includes(".txt")
        || extension.includes(".csv")
        || extension.includes(".ptx")
        || extension.includes(".rtf")) {
        result = text;
    }
    else if (extension.includes(".tif")
        || extension.includes(".png")
        || extension.includes(".jpg")
        || extension.includes(".jpeg")
        || extension.includes(".gif")
        || extension.includes(".bmp")) {
        result = image
    }
    else {
        result = text;
    }
    return result;
}

function LoadMoreMessagesCreateTemplate(data) {
    RemoveClassIfPresent($('#messageSection .latestRow'), 'latestRow');
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let userDetails = data[i];

            var lastRowclass = ''
            if (i == 0) {
                lastRowclass = lastRowclass + 'class="latestRow"'
            }
            var newRow = RowCreated(userDetails, lastRowclass);
            $('#messageSection').prepend(newRow);
        }
    }
}

export function Participants(channelId) {
    $.ajax({
        url: "/Notification/GetChannelParticipants",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },
        beforeSend: function (xhr) {
            $('#participantSection').empty();
            AddModelLoadingIndicator('#divParticipantSection');
        },
        success: function (data) {
            $('#participantSection').empty();
            if (data != null) {
                $("#hdnParticipantsCount_" + channelId).val(data.length);
                $("#spanParticipantsCount").text(data.length);
                CreateTemplateParticipants(data)
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divParticipantSection');
        }
    });
}

function CreateTemplateParticipants(data) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let userDetails = data[i];
            var newRow = RowCreatedParticipants(userDetails);
            $('#participantSection').append(newRow);
        }
    }
}

export function GetNoticationUnreadChannelCount() {
    $.ajax({
        url: "/Notification/GetUnreadChannelCount",
        type: "POST",
        dataType: "JSON",
        global: false,
        success: function (data) {
            if (parseInt(data) != 0) {
                $(".notificationAlert").text(data);
                $(".notificationAlert").show();
            }
            else {
                $(".notificationAlert").text(0);
                $(".notificationAlert").hide();
            }
        }
    });
}

export function AppendMessageIntoList(message) {
    let newMessage = '<p>' + message + '</p>';
    $("#messageSection").append(newMessage);
}


function SetReadParticipantsCount(messageId, messageElement) {
    $.ajax({
        url: "/Notification/GetReadMessageParticipants",
        type: "POST",
        dataType: "JSON",
        data: {
            "messageId": messageId
        },
        success: function (data) {
            if (data != null) {
                let parentChatDesc = $(messageElement).parents('.chat-desc')[0];
                $('#seencount_' + messageId).text(data.readby);
                RemoveClassIfPresent(messageElement, 'd-none');
                RemoveClassIfPresent($(messageElement).parents('.dropdown-notify')[0], 'd-none');
                let sent = $(parentChatDesc).find('.sent')[0];
                AddClassIfAbsent($(sent), 'd-none'); //sent

            }
        }
    });

}

export function GetReadParticipants(IsOneToOneChat, readByTotalParticipants, id, button) {
    $.ajax({
        url: "/Notification/GetReadMessageParticipants",
        type: "POST",
        dataType: "JSON",
        data: {
            "messageId": id
        },
        success: function (data) {
            let liparent = $('<li class="dropdown dropdown-submenu dropdown-notify-submenu head-read"><span >Read by ' + data.readby + '</span> </li>');

            let participants = data.data;

            if (IsOneToOneChat == 'false') {
                $(button).next('ul').append(liparent);
            }

            for (var i = 0; i < participants.length; i++) {
                let userDetails = participants[i];
                let username = userDetails.username;
                let li = '<li>' + username + '</li>';
                $(button).next('ul').append(li);
            }
        },
        complete: function () {
            $(button).next('ul').addClass('show');
            $(document).one('click', function () {
                $(button).next('ul').removeClass('show');
            });
        }
    });
}

export function SendMessage(message, channelId, attachedFiles, onSuccessCallBack) {
    var request =
    {
        "MessageDescription": message,
        "ChannelId": channelId,
        "AttachmentList": attachedFiles
    }

    let currentMsg = [{
        isSeen: false,
        isSent: false,
        messageDescription: message,
        userShortName: currentUser.userShortName,
        username: currentUser.username,
        createdOnUTCDateFormat: moment().format("DD MMM YYYY HH:mm"),
        isUnreadMessage: true,
        isAttachment: attachedFiles.length > 0,
        attachments: attachedFiles,
        ssUserId: GetCookie('NotificationUserId'),
        channelId: $('#hdnSelectedChannelId').val()
    }];

    $('#messageSection .lastRow').removeClass('lastRow');

    let msgs = CreateTemplateNewMessage(currentMsg);
    let newDiv = msgs[0];
    let retryDiv = $(newDiv).appendTo('#messageSection');

    ScrollToLastRow();

    CallSendMessage(request, retryDiv, onSuccessCallBack, false);
}

export function GetRequestPropertiesFromDiv(div) {
    let message = $(div).find('.text-break').text();
    let channelId = $(div).find('.chat-desc').data('channelid');
    let request = {
        "MessageDescription": message,
        "ChannelId": channelId,
    };

    let attachmentDivs = $(div).find(".attached-files");
    if (attachmentDivs != null && attachmentDivs.length > 0) {
        let attachments = [];
        for (let i = 0; i < attachmentDivs.length; i++) {
            let el = attachmentDivs[i];
            let attachment = {
                "description": $(el).data("desc"),
                "errorMessage": null,
                "ettId": $(el).data("id"),
                "fileExtension": $(el).data("type"),
                "isOperationSuccess": true,
                "sequence": $(el).data("seq")
            }
            attachments.push(attachment);
        }
        request["AttachmentList"] = attachments;
    }

    return request;
}

export function CallSendMessage(request, retryDiv, onCompleteCallback, isCalledThroughRetry) {
    $.ajax({
        url: "/Notification/SendNotification",
        type: "POST",
        dataType: "JSON",
        data: request,
        success: function (data) {
            if (parseInt(data) != 0) {
                RemoveClassIfPresent($(retryDiv).find(".sent")[0], 'd-none');
                $("#hdnChannelDraftMessage_" + request.ChannelId).val("");
                if (isCalledThroughRetry == true) {
                    $(retryDiv).remove();
                } else {
                    let el = $(retryDiv).find('.chat-desc')[0];
                    $(el).attr('data-msgid', data);
                }
                let newMessage = {
                    isRetry: false,
                    messageId: parseInt(data),
                    isSeen: true,
                    isSent: true,
                    messageDescription: request.MessageDescription,
                    userShortName: currentUser.userShortName,
                    username: currentUser.username,
                    createdOnUTCDateFormat: moment().format("DD MMM YYYY HH:mm"),
                    isUnreadMessage: false,
                    isAttachment: request.AttachmentList && request.AttachmentList.length > 0,
                    attachments: request.AttachmentList,
                    ssUserId: GetCookie('NotificationUserId'),
                    channelId: request.ChannelId,
                    isMessageEdited: false
                }
                $('#divSyncPending_' + newMessage.channelId).addClass('d-none')
                addMessageToDb(newMessage)
                NewChannelMessage(request.ChannelId);
                NewChannelDetailActive(request.ChannelId);
            }
        },
        complete: function () {
            if (!IsNullOrEmptyOrUndefined(onCompleteCallback)) {
                onCompleteCallback();
            }
        },
        error: async function (jqXHR, exception) {
            let newMessage = {
                isRetry: true,
                messageId: 0,
                isSeen: false,
                isSent: false,
                messageDescription: request.MessageDescription,
                userShortName: currentUser.userShortName,
                username: currentUser.username,
                createdOnUTCDateFormat: moment().format("DD MMM YYYY HH:mm"),
                isUnreadMessage: false,
                isAttachment: request.AttachmentList && request.AttachmentList.length > 0,
                attachments: request.AttachmentList,
                ssUserId: GetCookie('NotificationUserId'),
                channelId: request.ChannelId,
                isMessageEdited: false,
                isPendingToSync: true
            }
            addMessageToDb(newMessage)
            $(retryDiv).find('.retry').removeClass('d-none');
            $('#divSyncPending_' + newMessage.channelId).removeClass('d-none')
        }
    });
}
async function addMessageToDb(newMessage) {
    if (!vshipDb) {
        await createDB();
    }
    let allkeyofAppMetaData = await vshipDb.getAllKeys('ChatNotificationDetails')
    let offlineData = await vshipDb.getAll('ChatNotificationDetails');
    let finalData = offlineData.map(function (data, idx) { return { key: allkeyofAppMetaData[idx], data: data } });
    let exisistingDetails = finalData.filter(function (d) { return d.data.messageDescription == newMessage.messageDescription && d.data.messageId == 0 })
    if (exisistingDetails.length == 1) {
        vshipDb.put('ChatNotificationDetails', exisistingDetails[0].key, newMessage);
    }
    else {
        const count_chats = allkeyofAppMetaData.reduce(function (val1, val2) { return Math.max(val1, val2); }) + 1;
        vshipDb.put('ChatNotificationDetails', count_chats, newMessage);
    }
}

export function setHeight(elem) {
    const style = getComputedStyle(elem, null);
    const verticalBorders = Math.round(parseFloat(style.borderTopWidth) + parseFloat(style.borderBottomWidth));
    const maxHeight = parseFloat(style.maxHeight) || 120;

    elem.style.height = "auto";

    const newHeight = elem.scrollHeight + verticalBorders;

    elem.style.overflowY = newHeight > maxHeight ? "auto" : "hidden";
    elem.style.height = Math.min(newHeight, maxHeight) + "px";
}

export function DownloadAttachment(input) {
    $.ajax({
        url: "/Notification/DownloadAttachment",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": input
        },
        success: function (data) {
            if (data != null) {
                if (data.bytes != null) {
                    $("#hndFileName").val(data.filename);
                    $("#hdnFileByteString").val(data.bytes);
                    $("#hdnFileExtension").val(data.fileType);
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

export function ClearMoreUserPopUp(selectedUser) {
    selectedUser.length = 0
    $("#divResponsibleUsers").empty();
    $("#divSelectedParticipants").empty();
    $('#cboSearchMoreUsers').val(null).trigger('change');
    $("#spanSelectedParticipantsCount").text(0);
}

export function OpenAddParticipantsToChannel(vesselId, vesselName, channelId, selectedUser) {
    $('#spanResponsibleVesselName').text(vesselName);
    ClearMoreUserPopUp(selectedUser);
    if (IsNullOrEmptyOrUndefined(vesselId)) {
        AddClassIfAbsent('#divResponsibleUsersSection', 'd-none');
    }
    else {
        RemoveClassIfPresent('#divResponsibleUsersSection', 'd-none');
        GetVesselResponsibilities(vesselId);
    }
    GetExistingParticipants(channelId);
}

export function GetVesselResponsibilities(vesselId) {
    $.ajax({
        url: "/Notification/GetVesselResponsibilities",
        dataType: "JSON",
        data: {
            "vesselId": vesselId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('#divResponsibleUsers');
        },
        success: function (data) {
            $("#spanResponsibleUserCount").text(data.length);
            CreateResponsibleUserTemplate(data);
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divResponsibleUsers');
        }
    });
}

export function GetExistingParticipants(channelId) {
    $.ajax({
        url: "/Notification/GetChannelParticipants",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('#divSelectedParticipants');
        },
        success: function (data) {
            if (data != null && data != '') {
                $("#spanSelectedParticipantsCount").text(data.length);
                for (var i = 0; i < data.length; i++) {
                    let existingParticipantsDetail = data[i];
                    var existingUser = {
                        userShortName: existingParticipantsDetail.userShortName,
                        text: existingParticipantsDetail.username,
                        description: existingParticipantsDetail.userRoleDescription,
                        id: existingParticipantsDetail.ssUserId
                    }
                    var newRow = SelectedParticipantTemplate(existingUser, false);
                    $('#divSelectedParticipants').append(newRow);
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#divSelectedParticipants');
        }
    });
}

export function CreateResponsibleUserTemplate(data) {
    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            let userDetails = data[i];
            var newRow = ResponsibleUserRow(userDetails);
            $('#divResponsibleUsers').append(newRow);
        }
    }
}

export function ResponsibleUserRow(userDetails) {
    let row = '<div id="' + userDetails.id + '" class="col-12 col-md-12 col-lg-6 col-xl-6 bottom addParticipants">\
                    <div class="participantsRow d-inline-flex selectedParticipant" data-username="' + userDetails.text + '" data-description="' + userDetails.description + '" data-usershortname="' + userDetails.userShortName + '">\
                        <div class="initialname green-name particiapntsCol">' + userDetails.userShortName + '</div>\
                        <div class="particiapntsCol">\
                            <div class="name">' + userDetails.text + '</div>\
                            <div class="type"> '+ userDetails.description + '</div>\
                        </div>\
                        <span id="' + userDetails.id + ' " class="remove removeSelectedParticipants d-none">×</span>\
                    </div>\
                     <button id="' + userDetails.id + '"class="btn d-none d-inline-flex float-right add-p p-0"><img src="/images/add-partici.png"/></button>\
                </div>'
    return row;
}

export function AddNewParticipanttoChannel(channelId, selectedUser) {
    //added participant message code 
    //var addedUsersList = "";
    //for (var i = 0; i < SelectedUser.length; i++) {
    //	if (i == 0) {
    //		addedUsersList = addedUsersList + SelectedUser[i].text;
    //	}
    //	else if (i == (SelectedUser.length - 1)) {
    //		addedUsersList = addedUsersList + ' and ' + SelectedUser[i].text;
    //	}
    //	else {
    //		addedUsersList = addedUsersList + ', ' + SelectedUser[i].text;
    //	}
    //}
    //var addedUserContent = $('#currentusername').text() + ' added ' + addedUsersList + ' to the chat.';
    //var newRow = '<li><div class="row mx-auto no-gutters">' + addedUserContent +'</div></li>'
    //$('#messageSection').append(newRow);

    let isValidate = true;  //ValidationBeforeCreateChannel();
    if (isValidate) {

        let request =
        {
            "Subscribers": selectedUser,
            "ChannelId": channelId
        }
        $.ajax({
            url: "/Notification/AddNewParticipantsToChannel",
            type: "POST",
            dataType: "JSON",
            data: request,
            success: function (data) {
                Participants(channelId);
                NewChannelMessage(channelId);
                NewChannelDetailActive(channelId)
                ToastrAlert(data.response, data.message);
            }
        });
    }
}

export function SelectedParticipantTemplate(data, isRemovable) {
    var canRemoveParticipant = isRemovable ? '' : ' d-none';
    var $result;
    $result = $('<div class="participantsRow">\
                        <div class="initialname green-name particiapntsCol">' + data.userShortName + '</div>\
                        <div class="particiapntsCol">\
                            <div class="name">' + data.text + '</div>\
                            <div class="type">' + data.description + '</div>\
                        </div>\
                        <span id="' + data.id + ' " class="remove removeSelectedParticipants cboSelected' + canRemoveParticipant + '">×</span>\
                    </div>');

    return $result;
}

export function InitialiseSearchMoreUserDropdown(selectedVesselId) {
    $("#cboSearchMoreUsers").select2({
        theme: "bootstrap4",
        placeholder: "Search users by name or role",
        minimumInputLength: 0,
        dropdownCssClass: 'dropdown-outline participant-drop-deisgn',
        ajax: {
            url: '/Notification/GetParticipantsListPaged',
            dataType: 'json',
            data: function (params) {
                let selectedUsers = $("#divSelectedParticipants").find("span");
                let selectedUsersId = new Array();
                $(selectedUsers).each(function () {
                    selectedUsersId.push($(this).attr('id').trim());
                });
                return {
                    term: params.term,
                    page: params.page || 1,
                    vesselId: selectedVesselId,
                    tempSelectedUsers: String(selectedUsersId)
                };
            },
        },
        templateResult: searchMoreResult
    });
}

function searchMoreResult(result) {
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

export function UploadFile(file, sequence, attachedTo, divHtmlElement, attachedFiles, onSuccessCallBack) {
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
            url: "/Notification/UploadFile",
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
                    var row = AttachmentTemplate(GetAttachmentIcon(data.data.fileExtension), fileName, sequence, attachedTo, "");
                    $(divHtmlElement).append(row);
                    attachedFiles.push(data.data);
                    if (!IsNullOrEmptyOrUndefined(onSuccessCallBack)) {
                        onSuccessCallBack();
                    }
                }
            },
            complete: function () {
                processedFileCount++;
                if (selectedFileCount == processedFileCount) {
                    RemoveModelLoadingIndicator(divHtmlElement);
                    processedFileCount = 0;
                    selectedFileCount = 0;
                }
            }
        });
    };
    reader.onerror = function (error) {
        ToastrAlert("error", error);
    };
}

function AttachmentTemplate(attachemtIcon, fileName, sequence, attachedTo, messageId) {
    var $row;
    $row = $('<div class="attached-files" data-sequence="' + sequence + '" data-attachedTo="' + attachedTo + '"  data-messageid="' + messageId + '">'
        + attachemtIcon +
        '<div class="attachment-text">' + fileName + '</div >\
				<div class="attachment-close">×</div>\
			</div >');

    return $row;
}

export function CheckFilesAttachedCount(docsCount) {
    if (docsCount > FileAttachmentLimit) {
        ToastrAlert("validate", FileAttachmentLimitErrorMessage);
        return false;
    } else {
        return true;
    }
}

export function ScrollToLastRow() {
    let totalli = $('#messageSection').children('li').length;
    if (totalli > 6) {
        var ulScroll = $('#messageSection .lastRow')[0];
        if (ulScroll != null && ulScroll != undefined) {
            ulScroll.scrollIntoView(true);
        }
    }

}

export function DeleteDocument(ettId) {
    var request = {
        ettId: ettId
    }
    $.ajax({
        url: "/Notification/DeleteAttachment",
        type: "POST",
        dataType: "JSON",
        data: request,
        success: function (data) {
        }
    });
}

export function DeleteAllDocument(attachedFiles) {
    for (var i = 0; i < attachedFiles.length; ++i) {
        DeleteDocument(attachedFiles[i].ettId)
    }
}

export function InitialiseVesselSearch(id, formatResult, formatRepoSelection) {
    $(id).select2({
        theme: "bootstrap4",
        placeholder: "Type here to search vessel",
        minimumInputLength: 0,
        dropdownCssClass: 'vessel-drop',
        allowClear: true,
        ajax: {
            url: '/Notification/GetVesselLookup',
            dataType: 'json'
        },
        templateResult: formatResult,
        templateSelection: formatRepoSelection
    });
}

export function GetAreaList(selectedCategory, areaSelection, afterCompleteCallback) {
    var selAreaSelection = document.getElementById(areaSelection);
    var selAreaSelectionLength = selAreaSelection.options.length;
    for (var i = selAreaSelectionLength - 1; i >= 0; i--) {
        selAreaSelection.options[i] = null;
    }

    $.ajax({
        url: "/Notification/GetMessageCategories",
        type: "POST",
        "datatype": "JSON",
        success: function (data) {
            var select = document.getElementById(areaSelection);

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
            if (afterCompleteCallback != null && afterCompleteCallback != 'undefined') {
                afterCompleteCallback();
            }
        }
    });
}

export function ValidationBeforeCreateChannel(subjectElement, messageElement, participantsElement) {
    let subjectText = $(subjectElement).val();
    let messageText = $(messageElement).val();
    let participants = $(participantsElement).val();

    let isValidate = true;
    if (IsNullOrEmptyOrUndefined(subjectText)) {
        isValidate = false;
        $(subjectElement).css({ "border-color": 'red' });
    }
    if (($(messageElement).val().trim().length === 0)) {
        isValidate = false;
        $(messageElement).css("border-color", 'red');
    }

    if (participants.length === 0) {
        isValidate = false;
        $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", 'red');
    }

    if (!isValidate) {
        ToastrAlert("validate", "Please enter mandatory details.");
    }
    return isValidate;
}

export function ValidationBeforeCreateDraft(subjectElement, messageElement, participantsElement, isShowErrorMsg) {
    let subjectText = $(subjectElement).val();
    let participants = $(participantsElement).val();

    let isValidate = true;
    if (IsNullOrEmptyOrUndefined(subjectText) && ($(messageElement).val().trim().length === 0) && participants.length === 0) {
        isValidate = false;
    }

    if (!isValidate && isShowErrorMsg) {
        ToastrAlert("validate", "Please enter any one of the mandatory details.");
    }
    return isValidate;
}

export function DisplaySearchMoreUserSection(isVesselSelected, htmlSearchMoreUserElement) {
    if (isVesselSelected) {
        RemoveClassIfPresent(htmlSearchMoreUserElement, 'd-none');
        $('.generalmsgpopup .participant-design,.discussion-form .participant-design').removeClass('col-xl-12');
        $('.generalmsgpopup .participant-design').addClass('col-md-9 col-lg-9 col-xl-9');
        $('.discussion-form .participant-design').addClass('col-md-12 col-lg-12 col-xl-9');
        $('.discussion-form .participant-design').removeClass('col-lg-12');
        $('.discussion-form .participant-design').addClass('col-lg-9');
        $('.discussion-form .participant-design').removeClass('col-md-12');
        $('.discussion-form .participant-design').addClass('col-md-7');
    }
    else {
        AddClassIfAbsent(htmlSearchMoreUserElement, 'd-none');
        $('.generalmsgpopup .participant-design,.discussion-form .participant-design').addClass('col-xl-12');
        $('.discussion-form .participant-design').addClass('col-lg-12');
        $('.generalmsgpopup .participant-design').removeClass('col-md-9 col-lg-9 col-xl-9');
        $('.discussion-form .participant-design').addClass('col-md-7 col-lg-12 col-xl-9');
        $('.discussion-form .participant-design').addClass('col-md-12');
    }
}

export function UpdateSelectedVesselDropdown(vesselId, vesselName, htmlVesselElememt, htmlSearchMoreUserElement) {
    if (!IsNullOrEmptyOrUndefinedLooseTyped(vesselId) && !IsNullOrEmptyOrUndefinedLooseTyped(vesselName)) {
        var option = new Option(vesselName, vesselId, true, true);
        $(htmlVesselElememt).append(option).trigger('change');
        DisplaySearchMoreUserSection(true, htmlSearchMoreUserElement);
    }
    else {
        DisplaySearchMoreUserSection(false, htmlSearchMoreUserElement);
    }
}

export function GetRecordLevelDetails(channelId) {
    $.ajax({
        url: "/Notification/GetRecordLevelDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": channelId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('.recordLevelDetails');
        },
        success: function (data) {
            if (data != null) {
                if (data.isSuccess == true) {
                    $("#divRecordSection").empty();
                    $("#divRecordSection").append('<h1>Record</h1>');
                    if (data.details != null && data.details.length > 0) {

                        for (var i = 0; i < data.details.length; i++) {
                            let pair = data.details[i];
                            let key = '<p>' + pair.key + '</p>';
                            let value = '<h2>' + pair.value + '</h2>';
                            $("#divRecordSection").append(key);
                            $("#divRecordSection").append(value);
                        }
                    }
                } else {
                    ToastrAlert("error", data.errorMessage);
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('.recordLevelDetails');
        }
    });
}

export function NavigateToChannelRecordDetails(channelId, vesselId) {
    $.ajax({
        url: "/Notification/NavigateToChannelRecordDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": channelId,
            "vesselId": vesselId
        },
        success: function (data) {
            if (data != null) {
                if (data.type == 'RedirectParent' && GetCookie('NotificationApplicationId') == '2') {
                    parent.goTo(data.navigateURL);
                    //parent.window.location.href = data.navigateURL;
                }
                else {
                    ExternalObjectCall(data);
                }
            }
        }
    });
}

export function GetChannelRecordDetails(channelId, vesselId) {
    $.ajax({
        url: "/Notification/NavigateToChannelRecordDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "channelId": channelId,
            "vesselId": vesselId
        },
        success: function (data) {
            if (data != null) {
                if (data.type == "WindowOpen" && GetCookie('NotificationApplicationId') == '2') {
                    $("#aViewRecord").attr('href', data.navigateURL);
                    $("#aViewRecord").attr('target', '_blank');

                    //if (window.matchMedia('(display-mode: standalone)').matches) { // standalone app
                    //	alert("This is running as standalone.");
                    //	window.open(data.navigateURL + "", "_self");
                    //}
                    //else { // browser
                    //	alert("This is running as web.");
                    //	window.open(data.navigateURL + "", "_blank");
                    //}
                }
            }
        }
    });
}

async function ExternalObjectCall(data) {
    await CefSharp.BindObjectAsync("chatViewRecord");
    window.chatViewRecord.viewRecordInApplication(data.navigateURL, data.contextPayload);
}

export function GetCurrentUserDetails(successCallback) {
    $.ajax({
        url: "/Notification/GetCurrentUserDetails",
        type: "POST",
        dataType: "JSON",
        success: function (userDetails) {
            userDetails = typeof (userDetails) == 'string' ? JSON.parse(userDetails) : userDetails;
            if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                successCallback(userDetails);
            }
            currentUser = userDetails;
        }
    });
}

export function SetRecordAndParticipantHeight(recordSectionHeight) {

    let participantsHeight = $("#spanParticipantsCount").parent('h1').outerHeight(true); //h1
    let AddParticipantButtonHeight = $("#btnAddParticipant").parent('div').outerHeight(true); //participant button height
    let viewRecordHeight = $("#aViewRecord").outerHeight(true); //view record button height

    let remain = recordSectionHeight - (viewRecordHeight + participantsHeight + AddParticipantButtonHeight);

    if ($("#divRecordSection").hasClass('d-none')) {
        $(".participant-list-dynamic").height(remain);
    } else {
        $(".participant-list-dynamic").height(remain / 2);
        $("#divRecordSection").height(remain / 2);
    }
}

export function ShortChatTitle(title) {
    var maxLengthTitle = 32;
    if (!IsNullOrEmptyOrUndefinedLooseTyped(title)) {
        title = title.length > maxLengthTitle ? title.substring(0, maxLengthTitle) + '...' : title;
        return title;
    }
    return "";
}

export function saveasDraftDetails(channelId) {

    $('#messageSection').html("");
    ResetMandatoryFields();
    DeleteAllDocument(NotificationObj.AttachedFiles);
    ResetDraftAttachedFilesVariables();
    LoadDiscussionScreen();
    ClearCreateDraftDiscussionControls();
    HideNewDiscussionSlot();

    var CategoryId = $("#hdn" + "CategoryId_" + channelId).val();
    var IsGeneralCat = $("#hdn" + "IsGeneralCat_" + channelId).val();

    if (CategoryId > 0 && IsGeneralCat == "false") {
        RemoveClassIfPresent('#divAreaDropdown', 'd-none');
        $("#cboNotificationVesselSearch").prop("disabled", true);

        AddModelLoadingIndicator('.new-discussion-layout');
        GetAreaList(CategoryId, 'cboAreaSelection', function () {
            null
        });
        //RemoveModelLoadingIndicator('.new-discussion-layout');
    }
    else {
        AddClassIfAbsent('#divAreaDropdown', 'd-none');
        $("#cboNotificationVesselSearch").prop("disabled", false);
    }
    GetChannelDetailDraft(channelId, '.new-discussion-layout');
}

export function UpdateParticipants(data) {
    var CoockieUserId = GetCookie('NotificationUserId');

    $('#participantdropdown').html(null).trigger('change');
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let userDetails = data[i];
            if (CoockieUserId !== userDetails.ssUserId) {
                var obj = {
                    id: userDetails.ssUserId,
                    description: userDetails.userRoleDescription,
                    userShortName: userDetails.userShortName,
                    text: userDetails.username
                }
                var option = new Option(obj.text, obj.id, true, true);
                $(option).data('raw', obj);
                $('#participantdropdown').append(option).trigger('change');
            }
        }
    }
}

export function UpdateAttachments(result, isAttachment) {
    if (result != null) {
        NotificationObj.AttachedFiles = [];
        $("#divNewChannelAttachemnts").html("");
        if (isAttachment == true && result != null) {
            for (var i = 0; i < result.length; i++) {
                var row = AttachmentTemplate(GetAttachmentIcon(result[i].fileExtension), result[i].description, result[i].sequence, "Channel", "");
                $("#divNewChannelAttachemnts").append(row);
                var obj = {
                    description: result[i].description,
                    errorMessage: null,
                    ettId: result[i].ettId,
                    fileExtension: result[i].fileExtension,
                    isOperationSuccess: true,
                    sequence: result[i].sequence
                }
                NotificationObj.AttachedFiles.push(obj);
            }
        }
    }
}

export function UpdateAttachmentsForEditMessage(result) {

    let messageId = result.messageId;
    let isAttachment = result.isAttachment;
    let attachments = result.attachments;

    if (attachments != null && isAttachment == true) {
        var tempAttachedFiles = [];
        $("#divEditMessageAttachemnts" + messageId).html("");
        if (isAttachment == true && attachments != null) {
            for (var i = 0; i < attachments.length; i++) {
                var row = AttachmentTemplate(GetAttachmentIcon(attachments[i].fileExtension), attachments[i].description, attachments[i].sequence, "Channel", messageId);
                $("#divEditMessageAttachemnts" + messageId).append(row);
                var obj = {
                    description: attachments[i].description,
                    errorMessage: null,
                    ettId: attachments[i].ettId,
                    fileExtension: attachments[i].fileExtension,
                    isOperationSuccess: true,
                    sequence: attachments[i].sequence,
                    isOldFile: true
                }
                tempAttachedFiles.push(obj);
            }
        }

        var objAttachment = {
            FilesAttached: 0,
            AttachedFiles: tempAttachedFiles
        }
        NotificationObj.AttachedFilesCol.set(messageId, objAttachment);
    }
}

export function SetChannelMessage(channelId, isScrolled) {
    $.ajax({
        url: "/Notification/GetChannelMessages",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId,
            "IsScrolled": isScrolled,
            "PageNumber": 1
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator('.new-discussion-layout');
        },
        success: function (data) {
            if (data != null && data.data != null) {
                var result = data.data[0];

                $('#txtMessage').val(result.messageDescription);
                $("#divNewChannelAttachemnts").html("");
                if (result.isAttachment == true && result.attachments != null) {
                    for (var i = 0; i < result.attachments.length; i++) {
                        var row = AttachmentTemplateSet(GetAttachmentIcon(result.attachments[i].fileExtension), result.attachments[i].description, result.attachments[i].sequence, "Channel");
                        $("#divNewChannelAttachemnts").append(row);
                        var obj = {
                            description: result.attachments[i].description,
                            errorMessage: null,
                            ettId: result.attachments[i].ettId,
                            fileExtension: result.attachments[i].fileExtension,
                            isOperationSuccess: true,
                            sequence: result.attachments[i].sequence
                        }
                        NotificationObj.AttachedFiles.push(obj);
                    }
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('.new-discussion-layout');
        }
    });
}

export function ResetMandatoryFields() {
    $("#txtSubject").css("border", "2px solid #e5e5e5");
    $("#txtMessage").css("border", "2px solid #e5e5e5");
    $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", '#e5e5e5');
}

export function ResetDraftAttachedFilesVariables(messageId) {
    if (NotificationObj.AttachedFilesCol.has(messageId)) {
        NotificationObj.AttachedFilesCol.delete(messageId);
    }
    else {
        NotificationObj.AttachedFiles = [];
        NotificationObj.FilesAttached = 0;
    }
}

export function GetAttachedFilesVariables() {
    return NotificationObj;
}

export function LoadDiscussionScreen() {
    $(".chat-layout").hide();
    RemoveClassIfPresent('.new-discussion-layout', 'd-none');

}

export function ClearCreateDraftDiscussionControls() {
    $('#txtSubject').val('');
    $('#txtMessage').val('');
    $('#inputChannelAttachments').val('');
    $("#divNewChannelAttachemnts").empty();
    $('#participantdropdown').val(null).trigger('change');
    $('#cboNotificationVesselSearch').val(null).trigger('change');
    $('#cboAreaSelection option[value="0"]').attr("selected", true);
    $("#cboNotificationVesselSearch").prop("disabled", false);
    AddClassIfAbsent('#divAreaDropdown', 'd-none');
    AddClassIfAbsent('.divAreaDropdowncls', 'd-none');
}

export function AttachmentTemplateSet(attachemtIcon, fileName, sequence, attachedTo) {
    var $row;
    $row = $('<div class="attached-files" data-sequence="' + sequence + '" data-attachedTo="' + attachedTo + '">'
        + attachemtIcon +
        '<div class="attachment-text">' + fileName + '</div >\
				<div class="attachment-close">×</div>\
			</div >');

    return $row;
}

export function GetChannelDetailDraft(channelId, IndicatorSelecter) {
    $.ajax({
        url: "/Notification/GetChannelDetailDraft",
        type: "POST",
        dataType: "JSON",
        data: {
            "ChannelId": channelId
        },
        beforeSend: function (xhr) {
            AddModelLoadingIndicator(IndicatorSelecter);
        },
        success: function (data) {
            let vesselId = data.vesselId;
            let vesselName = data.vesselName;
            NotificationObj.SelectedVesselId = vesselId;
            NotificationObj.SelectedVesselName = vesselName;
            UpdateSelectedVesselDropdown(vesselId, vesselName, '#cboNotificationVesselSearch', '#btnSearchParticipants');
            UpdateParticipants(data.participants);
            UpdateAttachments(data.attachments, data.isAttachment);
            $('#txtSubject').val(data.title);
            $('#txtMessage').val(data.messageDescription);
            if (IsNullOrEmptyOrUndefinedLooseTyped(data.title)) {
                $('#DraftTitle').text("Draft");
            }
            else {
                $('#DraftTitle').text(data.title);
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator(IndicatorSelecter);
        }
    });
}


export function toggleIframe() {
    console.log("Toggle calling");
    if (!IsNullOrEmptyOrUndefinedLooseTyped(($("#modalCreateNewChannel").data('bs.modal') || {})._isShown)) {
        console.log("Toggle if calling");
        $("#modalCreateNewChannel").modal('hide');
    }
}

export function SetSelectedFileCount(value) {
    selectedFileCount = value;
}

export function SignalRConnect() {
    var connection1;
    connection1 = $.hubConnection();
    connection1.logging = true;
    var contosoChatHubProxy1 = connection1.createHubProxy('MessageHub');
    //ToDo : Need to assign roles from notification controller
    var ar1 = ''
    if (GetCookie('NotificationRoles') != null) {
        ar1 = GetCookie('NotificationRoles').split(",");
        for (var i = ar1.length; i--;) {
            ar1[i] = ar1[i].replace(/['"]/g, "");
        }
    }
    connection1.url = GetCookie('SignalRURL');

    contosoChatHubProxy1.on('BroadcastNotificationResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("BroadcastNotificationResult utilities", messageObject)
            if (parseInt($('#hdnSelectedChannelId').val()) == messageObject.ChannelId) {
                NewChannelMessage($('#hdnSelectedChannelId').val(), '');
                NewChannelDetailActive($('#hdnSelectedChannelId').val(), '')
            }
            else {
                NewChannelDetailUnread(messageObject.ChannelId, '')
            }
        }
    });

    contosoChatHubProxy1.on('BroadcastSystemGeneratedNotificationResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("BroadcastSystemGeneratedNotificationResult utilities", messageObject)
            if (parseInt($('#hdnSelectedChannelId').val()) == messageObject.ChannelId) {
                NewChannelMessage($('#hdnSelectedChannelId').val(), '');
                Participants(messageObject.ChannelId);
            }
        }
    });

    contosoChatHubProxy1.on('BroadcastCreateChannelResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("BroadcastCreateChannelResult utilities", messageObject)
            //works in both mobile and web because the page is same for channel list			
            if (parseInt($('#hdnSelectedChannelId').val()) != messageObject.ChannelId) {
                NewChannelDetailUnread(messageObject.ChannelId, '')
            }
            else {
                NewChannelMessage($('#hdnSelectedChannelId').val(), '');
                NewChannelDetailActive($('#hdnSelectedChannelId').val(), '')
            }
        }
    });

    contosoChatHubProxy1.on('BroadcastChannelOpenedResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("broadcastChannelOpenedResult utilities", messageObject)
            //needs to be called in both web and mobile page for message seen
            console.table(messageObject);
            if (parseInt($('#hdnSelectedChannelId').val()) == messageObject.ChannelId) {
                SetSeenForChannelId(messageObject.ChannelId, '');

            }
        }
    });

    contosoChatHubProxy1.on('broadcastMessageDeletedResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("broadcastMessageDeletedResult utilities", messageObject)
            //needs to be called in both web and mobile page for message delete
            console.table(messageObject);
            if (parseInt($('#hdnSelectedChannelId').val()) == messageObject.ChannelId) {
                DeleteMessageThroughSignalR(messageObject.NotificationId);
            }
        }
    });

    contosoChatHubProxy1.on('broadcastUpdatedNotificationResult', (sender, messageObject) => {
        if (messageObject != null) {
            console.log("broadcastUpdatedNotificationResult utilities", messageObject)
            //needs to be called in both web and mobile page for message delete
            console.table(messageObject);

            if (parseInt($('#hdnSelectedChannelId').val()) == messageObject.ChannelId) {
                UpdateMessageThroughSignalR(messageObject.NotificationId);
            }
        }
    });

    connection1.start({ transport: ['webSockets', 'longPolling'] }).done(function () {
        console.log('Now connected,utilities connection ID=' + connection1.id);
        contosoChatHubProxy1.invoke('RegisterUserEmailConnection', GetCookie('NotificationUserEmailId'), '5d62c6a512464f7f930f96e080dfcb23', ar1);
        console.log('Invoke utilities');
    })

}

function DeleteMessageThroughSignalR(messageId) {
    let messageDiv = $('#messageSection li .chat-desc[data-msgid="' + messageId + '"]')
    let parent = $(messageDiv).parents('li')[0];
    if (messageDiv.length > 0) {
        $(messageDiv).find('p').html("<i>" + DeleteMessageTemplate + "</i>");
        AddClassIfAbsent($(messageDiv).find('.message-options'), 'd-none');
        AddClassIfAbsent($(messageDiv).find('.seen'), 'd-none');
        AddClassIfAbsent($(messageDiv).find('.sent'), 'd-none');
        AddClassIfAbsent($(parent).find('.attached-files'), 'd-none');
    }
}


function UpdateMessageThroughSignalR(messageId) {
    let messageDiv = $('#messageSection li .chat-desc[data-msgid="' + messageId + '"]')
    if (messageDiv.length > 0) {

    }
}

export function DeleteMessage(request, successCallback) {
    $.ajax({
        url: "/Notification/DeleteMessage",
        type: "POST",
        dataType: "JSON",
        data: request,
        success: async function (data) {
            if (data == true) {
                if (!vshipDb) {
                    await createDB();
                }
                let allkeyofAppMetaData = await vshipDb.getAllKeys('ChatNotificationDetails')
                let offlineData = (await vshipDb.getAll('ChatNotificationDetails')).map(function (d, idx) {
                    return { data: d, key: allkeyofAppMetaData[idx] }
                }).filter(function (e) {
                    return e.data.messageId == request["Id"]
                });

                offlineData.forEach(function (e) {
                    let newData = e.data;
                    newData.isDeleted = true;
                    newData.isPendingToSync = true;
                    vshipDb.delete('ChatNotificationDetails', e.key);
                })
                if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                    successCallback();
                }
            } else {
                ToastrAlert("error", "Some error occured:" + data);
            }
        },
        error: async function (jqXHR, exception) {
            if (!vshipDb) {
                await createDB();
            }
            let allkeyofAppMetaData = await vshipDb.getAllKeys('ChatNotificationDetails')

            let data = (await vshipDb.getAll('ChatNotificationDetails')).map(function (d, idx) {
                return { data: d, key: allkeyofAppMetaData[idx] }
            }).filter(function (e) {
                return e.data.messageId == request["Id"]
            });

            data.forEach(function (e) {
                let newData = e.data;
                newData.isDeleted = true;
                newData.isPendingToSync = true;
                $('#divSyncPending_' + e.data.channelId).removeClass('d-none');
                vshipDb.put('ChatNotificationDetails', e.key, newData);
            })

            if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                successCallback();
            }
        }
    });
}

export function DeleteChannel(channelId, isSaveAsDraft, allChannelsDeleted) {
    $.ajax({
        url: "/Notification/DeleteChannelById",
        type: "GET",
        dataType: "JSON",
        data: {
            "channelId": channelId
        },
        success: function (data) {
            data = typeof (data) == "string" ? JSON.parse(data) : data;
            if (data != null) {

                if (isSaveAsDraft === 'true') {
                    ToastrAlert("success", "Draft deleted successfully.");
                    LoadChatMessageScreen();
                }
                else {
                    ToastrAlert("success", "You left the discussion successfully.");
                }

                var ul = $('#chatsection');

                var currentElement = ul.find('#li_' + channelId);

                var nextElement = ul.find('#li_' + channelId).next('li');
                var nextElementId = nextElement.attr('id');

                var prevElement = ul.find('#li_' + channelId).prev('li');
                var prevElementId = prevElement.attr('id');

                let channelCount = parseInt($("#hdnChannelListCount").val());
                let currentChannelCount = channelCount - 1;
                $("#hdnChannelListCount").val(currentChannelCount)

                if (!IsNullOrEmptyOrUndefinedLooseTyped(nextElementId)) {
                    if ($(window).width() > MobileScreenSize) {
                        nextElement.find('.expandIcon')[0].click();
                    }
                    currentElement.remove();
                }
                else if (!IsNullOrEmptyOrUndefinedLooseTyped(prevElementId) && prevElementId != 'li_0') {
                    if ($(window).width() > MobileScreenSize) {
                        prevElement.find('.expandIcon')[0].click();
                    }
                    currentElement.remove();
                }
                else {
                    currentElement.remove();
                    if ($(window).width() <= MobileScreenSize) {
                        AddClassIfAbsent('#li_0', 'd-block');
                    }
                    else {
                        AddClassIfAbsent('#li_0', 'd-md-block');
                    }
                    $('#messageSection').empty();
                    $('#messageVesselName').empty();
                    $('#participantSection').empty();
                    $("#hdnSelectedChannelId").val(0);
                    $("#hdnParticipantsCount_" + channelId).val(0);
                    $("#spanParticipantsCount").text(0);
                    AddClassIfAbsent(".recordLevelDetails", 'd-none');
                    AddClassIfAbsent('#notificationVesselFlag', 'd-none');
                    AddClassIfAbsent('.addparticipant', 'd-none');

                    if (!IsNullOrEmptyOrUndefinedLooseTyped(allChannelsDeleted)) {
                        allChannelsDeleted(true);
                    }
                }
            }
            else {
                ToastrAlert("error", "Something went wrong!");
            }
        }
    });
}

export function EditChannelMessage(MessageId, chatElement) {
    console.log("EditChannelMessage", chatElement);
    $.ajax({
        url: "/Notification/GetChannelMessageById",
        type: "POST",
        dataType: "JSON",
        data: {
            "MessageId": MessageId
        },
        success: function (data) {
            SetEdiMessage(data, chatElement);
        },
        error: async function (jqXHR, exception) {
            let data = await fn_GetOfflineMessageFromMessageId(MessageId)
            SetEdiMessage(data, chatElement)
        },
        complete: function () {

        }
    });
}

async function fn_GetOfflineMessageFromMessageId(messageId) {
    if (!vshipDb) {
        await createDB();
    }

    let data = (await vshipDb.getAll('ChatNotificationDetails')).filter(function (e) { return e.messageId == messageId });
    if (data.length == 1) {
        let message = data[0]
        let response = {
            channelId: message.channelId,
            messageId: message.messageId,
            messageDescription: message.messageDescription,
            isAttachment: message.isAttachment,
            attachments: message.attachments
        }
        return Promise.resolve(response)
    }
    return Promise.resolve({});
}

function SetEdiMessage(result, chatElement) {
    let messageId = result.messageId;
    let message = IsNullOrEmptyOrUndefinedLooseTyped(result.messageDescription) ? "" : result.messageDescription;
    let editDiv = '<div class="fixed-mesage-box edit-message">\
							<div class="btn-edit-message"><button id="editMessageConfirm" class="btn p-0">\
								<img src="/images/message-mobile-image.svg" class="d-none d-md-block">\
								<img src = "/images/message-mobile-image.svg" class="d-inline-block d-md-none" >\
                            </button >\
							<button id="editMessageCancel" class="btn p-0 pe-7s-close mr-2">\
                            </button >\
							<button id="btnAttachEditMessage'+ messageId + '" class="btn p-0 mr-2 attachedit" >\
								<div class= "file-upload">\
								<label for="inputEditMessageAttachments'+ messageId + '">\
									<img src="/images/attach-message-mobile.png">\
								</label>\
								<input id="inputEditMessageAttachments'+ messageId + '" class="inputEditMessageAttachmentsCls" type="file" multiple="" data-messageid="' + messageId + '">\
								</div>\
							</button></div>\
							<textarea class="form-control scroller editMessageTextcls" rows="2" id="editMessageText'+ messageId + '"></textarea>\
							<div id="divEditMessageAttachemnts'+ messageId + '" class="message-attachments apply-loader loader-height" style=""></div>\
						</div>';
    $(chatElement).append(editDiv);
    UpdateAttachmentsForEditMessage(result);
    $(chatElement).find('#editMessageText' + messageId).val(message).focus();
}

export function CloseVChatApplication() {
    CloseVChatApplicationExternal();
}

async function CloseVChatApplicationExternal() {
    await CefSharp.BindObjectAsync("closeVChat");
    window.closeVChat.closeVChatApplication();
}

export function DisableExistingMembers() {
    let selectedUsers = $("#divSelectedParticipants").find("span");
    $(selectedUsers).each(function () {
        let id = $(this).attr('id');
        $("button#" + id).prop('disabled', true);
        $("button#" + id).find('img').prop('src', '/images/add-partici-disabled.png');
    });
}

export function BackButtonForNotification(keyName, isFromList, beforeCompleteCallback, afterCompleteCallback) {

    $('.back').click(function () {
        BackButtonAction(keyName, beforeCompleteCallback, afterCompleteCallback);
    });
}

export function BackButtonAction(keyName, beforeCompleteCallback, afterCompleteCallback) {
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
                sessionStorage.removeItem(keyName);
                window.location.replace(data);
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