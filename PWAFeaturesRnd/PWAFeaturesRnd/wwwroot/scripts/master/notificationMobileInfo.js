import "select2/dist/js/select2.full.js";
import { RemoveClassIfPresent, AddModelLoadingIndicator, RemoveModelLoadingIndicator, AddClassIfAbsent, ToastrAlert, GetCookie} from "../common/utilities.js";
import { NotificationMobileInfoKey, PleaseSelectUser } from "../common/constants";
import { Participants, ClearMoreUserPopUp, OpenAddParticipantsToChannel, AddNewParticipanttoChannel, InitialiseSearchMoreUserDropdown, SelectedParticipantTemplate, GetRecordLevelDetails, NavigateToChannelRecordDetails, SetRecordAndParticipantHeight, CloseVChatApplication, DisableExistingMembers, BackButtonAction, BackButtonForNotification, GetChannelRecordDetails } from "../common/notificationutilities.js";
require('bootstrap');

var SelectedUser = [];

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

$(document).ready(function () {
    $('body').addClass('backgroundcolorheight');

    $('.close').click(function () {
        $(".modal-backdrop").remove();
    });
    if (sessionStorage.getItem(NotificationMobileInfoKey) != null) {
        //fill filter fields from sessionStorage
        $('#hdnSessionStorageDetails').val(sessionStorage.getItem(NotificationMobileInfoKey))
    }
    else {
        //fill sessionStorage from fields
        sessionStorage.setItem(NotificationMobileInfoKey, $('#hdnSessionStorageDetails').val());
    }

    if (GetCookie('NotificationApplicationId') != '2') {
        RemoveClassIfPresent('.backclose', 'd-none');
        $('.app-header').css("height", "60px");
    }
    else {
        $('body').addClass("hideleftmenuheader");
    }
    $('.backclose').click(function () {
        sessionStorage.removeItem(NotificationMobileInfoKey);
        CloseVChatApplication();
    });

    BackButtonForNotification(NotificationMobileInfoKey, false, function () {
        AddModelLoadingIndicator('.notification-mobile-info');
    }, function () {
        RemoveModelLoadingIndicator('.notification-mobile-info');
    });

    var channelId = $("#ChannelId").val();
    var vesselId = $("#VesselId").val();
    var vesselName = $("#VesselName").val();

    Participants(channelId);


    if ($("#IsGeneralCat").val() == 'false' || $("#IsGeneralCat").val() == 'False') {
        RemoveClassIfPresent(".recordLevelDetails", 'd-none');
        GetRecordLevelDetails(channelId);
    } else {
        AddClassIfAbsent(".recordLevelDetails", 'd-none');
    }


    //this code is duplicated from notfication.js
    $("#btnAddParticipant").click(function () {
        $("#searchparticipant").modal('show');
        $('.discussion-form .participant-design .select2-container--bootstrap4 .select2-selection').css("border-color", '#e5e5e5');
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
            if (SelectedUser.length > 0) {
                $("#addParticipantConfirmationDialog").modal('show');

                $("#yesConfirmationButton").off();
                $("#yesConfirmationButton").on('click', function () {
                    AddNewParticipanttoChannel(channelId, SelectedUser);
                    $("#searchparticipant .close")[0].click();

                });
            } else {
                //AddNewParticipanttoChannel(channelId, SelectedUser);
                $("#searchparticipant .close")[0].click();
                ToastrAlert("error", PleaseSelectUser);
            }
        });
    });

    InitialiseSearchMoreUserDropdown(vesselId);

    //this code is duplicated from notfication.js
    $("#cboSearchMoreUsers").on('select2:select', function (selection) {
        //add user to array
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

    var h1 = ($(".app-main").outerHeight());
    var h2 = ($(".chat-header").outerHeight());
    $(".record-section").css({
        "height": h1 - h2
    });

    $("#aViewRecord").click(function () {
        var channelId = $("#ChannelId").val();
        var vesselId = $("#VesselId").val();

        NavigateToChannelRecordDetails(channelId, vesselId);
    });

    let recordSectionHeight = $(".record-section").height();
    SetRecordAndParticipantHeight(recordSectionHeight);
});

window.backAction = function () {
    BackFunction();
}
function BackFunction() {

    BackButtonAction(NotificationMobileInfoKey, function () {
        AddModelLoadingIndicator('.notification-mobile-info');
    }, function () {
        RemoveModelLoadingIndicator('.notification-mobile-info');
    });
}

window.chatScrollLastRow = function () {
}