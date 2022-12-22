import { NotificationMobileChatDetailKey, NotificationPageKey, NotificationMobileInfoKey, NotificationMobileDiscussionKey, NotificationChatPageKey } from "../common/constants";
import { ErrorLog, GetStringNullOrWhiteSpace } from "../common/utilities.js";

$(document).ready(function () {
    $('#hdnSessionStorageDetails').val(sessionStorage.getItem('hdnSessionStorageDetails'));
    if (sessionStorage.getItem(NotificationChatPageKey) != null) {
        if ($('#isFilterChange').val() == "True" || $('#isFilterChange').val() == "true" || $('#isFilterChange').val() == true) {
            $.ajax({
                url: "/Dashboard/SetSessionStorageFilterForChat",
                type: "POST",
                dataType: "JSON",
                data: {
                    "sessionDetails": sessionStorage.getItem(NotificationChatPageKey),
                    "urlParameter": $('#urlParameter').val()
                },
                success: function (data) {
                    if (data != null) {
                        sessionStorage.setItem(NotificationChatPageKey, data);
                        $('#hdnSessionStorageDetails').val(data);
                    }
                    $('.vesseldropdowntopheader').addClass('d-none');
                    $('body').addClass('hidemobilepatch');
                    document.getElementById("notificationchatform").submit()
                    IFrameResize();
                },
                error: function () {
                    $('.vesseldropdowntopheader').addClass('d-none');
                    $('body').addClass('hidemobilepatch');
                    document.getElementById("notificationchatform").submit()
                    IFrameResize();
                }

            });
        }
        else {

            //fill filter fields from sessionStorage
            if (sessionStorage.getItem(NotificationChatPageKey)) {
                $('#hdnSessionStorageDetails').val(sessionStorage.getItem(NotificationChatPageKey))
            }
            $.ajax({
                url: "/Dashboard/GetSessionStorageFilterForChat",
                type: "POST",
                dataType: "JSON",
                data: {
                    "sessionDetails": $('#hdnSessionStorageDetails').val()
                },
                success: function (data) {
                    if (data != null) {
                        $('#urlParameter').val(data.urlParameter)
                    }
                    $('.vesseldropdowntopheader').addClass('d-none');
                    $('body').addClass('hidemobilepatch');
                    document.getElementById("notificationchatform").submit()
                    IFrameResize();
                },
                error: function (xhjr) {
                    $('.vesseldropdowntopheader').addClass('d-none');
                    $('body').addClass('hidemobilepatch');
                    document.getElementById("notificationchatform").submit()
                    IFrameResize();
                }

            });
        }

    }
    else {
        //sessionStorage.removeItem(NotificationPageKey);
        //fill sessionStorage from fields
        sessionStorage.setItem(NotificationChatPageKey, $('#hdnSessionStorageDetails').val());
        $('.vesseldropdowntopheader').addClass('d-none');
        $('body').addClass('hidemobilepatch');
        document.getElementById("notificationchatform").submit()
        IFrameResize();
    }


    $('#chatclosebutton').click(function () {
        $.ajax({
            url: "/Dashboard/GetSourceURLForNotification",
            type: "POST",
            dataType: "JSON",
            data: {
                "sessionDetails": GetStringNullOrWhiteSpace(sessionStorage.getItem(NotificationChatPageKey))
            },
            success: function (data) {
                if (data != null) {
                    sessionStorage.removeItem(NotificationPageKey);
                    sessionStorage.removeItem(NotificationChatPageKey);
                    sessionStorage.removeItem(NotificationMobileChatDetailKey);
                    sessionStorage.removeItem(NotificationMobileDiscussionKey);
                    sessionStorage.removeItem(NotificationMobileInfoKey);
                    window.location.replace(data);
                }
            }
        });
    });

    $(window).resize(function () {
        IFrameResize();
        document.getElementById('iFrameNotificationChatFrame').contentWindow.chatScrollLastRow();
    });

    $('.back').click(function () {
        document.getElementById('iFrameNotificationChatFrame').contentWindow.backAction();
    });

});

function IFrameResize() {
    var windowwidth = $(document).width();
    var windowheight = $(window).height();
    var headerheight = $('.app-header').height();
    $(".notificationiframe").css({
        "width": windowwidth,
        "height": windowheight - headerheight
    });
}
