import ApexCharts from 'apexcharts';
import Chart from "chart.js";
import GMaps from "gmaps";
import moment from "moment";
import "select2/dist/js/select2.full.js";
import toastr from "toastr";
import { ErrorLog, AjaxError, ConvertDecimalNumberToString, ConvertValueToPercentage, GetCookie, ToastrAlert, GetDashboardColorMap, IsNullOrEmpty, ReplaceClass, SetSessionDetailsForTimeZone, IsNullOrEmptyOrUndefinedLooseTyped, AddModelLoadingIndicator, RemoveModelLoadingIndicator, AddClassIfAbsent, GetLastReferrer, UpdateUnreadMessagesOnDashboard, RegisterTabSelectionEvent, MobileTab_Overview, Mobile_Tabs, IsNullOrEmptyOrUndefined } from "../common/utilities.js";
import { GetCellData, GetFormattedDate, GetFormattedDateTimeWithSeconds, GetFormattedDateTime, GetFormattedDecimal, GetFormattedDecimalKPI } from "../common/datatablefunctions.js";
import { LTIFreeDaysExcessCondition, DaysInYear, MobileScreenSize, TabScreenSize, DashboardFilterKey, IMO_Prefix, EmailTypeCategoryId, PhoneTypeCategoryId, OtherTypeCategoryId, NotificationMobileChatDetailKey, NotificationPageKey, NotificationMobileInfoKey, NotificationMobileDiscussionKey, NotificationChatPageKey, Amber, Green, Red } from "../common/constants";
import { OpenModalPortServices } from "../master/voyageReportingModal.js";
import { GetNotesListOnDashboardPage, EditNote, NavigateToNoteRecordDetails, CreateReminderAlertOnDashboard, ReminderDismissed } from "../common/notesUtilities";
import { createTree } from "jquery.fancytree";

require('bootstrap');

var map;
var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
    '<div class="loader  mx-auto">' +
    '<div class="ball-clip-rotate">' +
    '<div></div>' +
    '</div>' +
    '</div>' +
    '</div>';
var dataLoad = new Set();

var colorMap = GetDashboardColorMap();

var fleetId = "";
var menuType = "F";
var vesselId = "";
var IsMobile = false;

var allowedModules = {
    CanShowCertificates: 'True',
    CanShowCommercial: 'True',
    CanShowCrewing: 'True',
    CanShowDefects: 'True',
    CanShowEnvironment: 'True',
    CanShowFinancials: 'True',
    CanShowHazOcc: 'True',
    CanShowInspectionsAndRatings: 'True',
    CanShowPMS: 'True',
    CanShowProcurement: 'True'
};

$(document).on('click', '.fromAnchorPortAlertCls , .toAnchorPortAlertCls', function () {
    var requestUrl = $(this).data('url');
    OpenModalPortServices(requestUrl);
})

$(document).on('click', '.deleteChannelDashboard', function (e) {
    e.preventDefault();
    let channelId = $(this).data('channelid');
    let isSaveAsDraft = $(this).data('isdraft');
    if (isSaveAsDraft) {
        $('#pDashboardDeleteChannelMsg').text('Do you want to delete the draft?');
    }
    else {
        $('#pDashboardDeleteChannelMsg').text('Are you sure you want to delete the discussion? This will also remove you from the chat and you will no longer receive messages.');
    }

    $("#modalDashboardDeleteChannelConfirmation").modal('show');
    $('#btnDashboardDeleteChannelYes').off();
    $('#btnDashboardDeleteChannelYes').on('click', function () {
        DeleteChannelById(channelId, isSaveAsDraft);
        $("#modalDashboardDeleteChannelConfirmation").modal('hide');
    });

    $('#btnDashboardDeleteChannelNo').off();
    $('#btnDashboardDeleteChannelNo').on('click', function () {
        $("#modalDashboardDeleteChannelConfirmation").modal('hide');
    });
});


var PageNumber = 1;

function LoadMoreVessels() {
    PageNumber++;
    fleetId = $('#fleetId').val()
    menuType = $('#menuType').val()
    vesselId = $('#vesselId').val()
    var request =
    {
        "FleetId": fleetId,
        "MenuType": menuType,
        "VesselIds": vesselId,
        "PageNumber": PageNumber
    }

    $.ajax({
        url: "/Dashboard/LoadMoreVessels",
        type: "POST",
        dataType: "html",
        data: request,
        beforeSend: function (xhr) {
            //Enable loading here
            AddModelLoadingIndicator('.dashboardvessel-panel-loadmore');
            $('#btnLoadMoreVessels').toggle();
            //$('#divLoadMore').toggle();
        },
        success: function (data) {
            if (data != null) {
                $('#divVesselResult').append(data);
                SetOnVesselOnClick();
                //Hide "Load More" button when last page loaded 
                //Page size is hardcoded here
                if (parseInt($("#hdnTotalPages").val()) == PageNumber) {
                    $('#btnLoadMoreVessels').toggle();
                }
                $(".expandIcon").click(ExpandVesselDetail);
            }
        },
        complete: function () {
            //Remove loading here
            console.log("remove dashboardvessel-panel-loadmore from complete");
            RemoveModelLoadingIndicator('.dashboardvessel-panel-loadmore');
            $('#btnLoadMoreVessels').toggle();
        }
    });
}

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

$(document).on('click', '.agentbtncall', function () {
    var parent = $(this).siblings('.agentmenudetail').find('.agentlisting');
    parent.empty();
    $.ajax({
        url: "/Dashboard/GetVesselCommunication",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselId": $(this).find(".vesselcommunicationlink").data('id'),
            "typeCategoryId": PhoneTypeCategoryId
        },
        beforeSend: function (xhr) {
            AddCommunicationLoadingIndicator('.communicationcall');
        },
        success: function (data) {
            
            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var divHtmlElement = '<div class="listagentdiv"><div class="row no-gutters mx-auto"><div class="col-md-1 col-lg-1 col-xl-1 p-0"><img src="/images/agentcallblue.svg" /></div>' +
                        '<div class="col-md-11 col-lg-11 col-xl-11 p-0"><h1><a href="javascript:void(0);">' + data[i].comNumber + '</a>'
                    if (data[i].primaryContact != '') {
                        divHtmlElement = divHtmlElement + '<button class="btn agentprimary">PRIMARY</button>'
                    }
                    divHtmlElement = divHtmlElement + '</h1><h2><span>Type </span>' + data[i].ctyName + '</h2><h3>' + data[i].comDesc + '</h3></div></div></div>'
                    
                    parent.append(divHtmlElement);
                }
            } else {
                parent.append('<Label>No data found.</Label>');
            }            
        },
        complete: function () {
            RemoveModelLoadingIndicator('.communicationcall');
        }
    });
    $(this).siblings('.communicationcall').show();
    $(this).parent().siblings('.agentemaildropdown').find('.communicationemail').hide();
    $(this).parent().siblings('.agentoptiondropdown').find('.communicationother').hide();
});

$(document).on('click', '.agentbtnemail', function () {
    var parent = $(this).siblings('.agentmenudetail').find('.agentlisting');
    parent.empty();
    $.ajax({
        url: "/Dashboard/GetVesselCommunication",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselId": $(this).find(".vesselcommunicationlink").data('id'),
            "typeCategoryId": EmailTypeCategoryId
        },
        beforeSend: function (xhr) {
            AddCommunicationLoadingIndicator('.communicationemail');
        },
        success: function (data) {

            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var divHtmlElement = '<div class="listagentdiv"><div class="row no-gutters mx-auto"><div class="col-md-1 col-lg-1 col-xl-1 p-0"><img src="/images/agentemailblue.svg" /></div>' +
                        '<div class="col-md-11 col-lg-11 col-xl-11 p-0"><h1><a href="javascript:void(0);" class="communicationmode" data-number="' + data[i].comNumber.trim() + '">' + data[i].comNumber + '</a>'
                    if (data[i].primaryContact != '') {
                        divHtmlElement = divHtmlElement + '<button class="btn agentprimary">PRIMARY</button>'
                    }
                    divHtmlElement = divHtmlElement + '</h1><h2><span>Type </span>' + data[i].ctyName + '</h2><h3>' + data[i].comDesc + '</h3></div></div></div>'
                    
                    parent.append(divHtmlElement);
                }
            } else {
                parent.append('<Label>No data found.</Label>');
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('.communicationemail');
        }
    });
    $(this).siblings('.communicationemail').show();
    $(this).parent().siblings('.agentcalldropdown').find('.communicationcall').hide();
    $(this).parent().siblings('.agentoptiondropdown').find('.communicationother').hide();
});

$(document).on('click', '.agentbtnoptions', function () {
    var parent = $(this).siblings('.agentmenudetail').find('.agentlisting');
    parent.empty();
    $.ajax({
        url: "/Dashboard/GetVesselCommunication",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselId": $(this).find(".vesselcommunicationlink").data('id'),
            "typeCategoryId": OtherTypeCategoryId
        },
        beforeSend: function (xhr) {
            AddCommunicationLoadingIndicator('.communicationother');
        },
        success: function (data) {

            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    var divHtmlElement = '<div class="listagentdiv"><div class="row no-gutters mx-auto">' +
                        '<div class="col-md-12 col-lg-12 col-xl-12 p-0"><h1 class="ml-0"><a href="javascript:void(0);">' + data[i].comNumber + '</a>'
                    if (data[i].primaryContact != '') {
                        divHtmlElement = divHtmlElement + '<button class="btn agentprimary">PRIMARY</button>'
                    }
                    divHtmlElement = divHtmlElement + '</h1><h2><span class="ml-0">Type </span>' + data[i].ctyName + '</h2><h3 class="ml-0">' + data[i].comDesc + '</h3></div></div></div>'

                    parent.append(divHtmlElement);
                }
            } else {
                parent.append('<Label>No data found.</Label>');
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('.communicationother');
        }
    });
    $(this).siblings('.communicationother').show();
    $(this).parent().siblings('.agentemaildropdown').find('.communicationemail').hide();
    $(this).parent().siblings('.agentcalldropdown').find('.communicationcall').hide();
});

$(document).click(function (e) {
    var container = $('.communicationcall,.communicationemail,.communicationother,.agentbtnoptions,.agentbtnemail,.agentbtncall,.agentcalldropdown,.agentemaildropdown,.agentoptiondropdown');
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        var elements = $('.communicationemail,.communicationcall,.communicationother');
        $.each(elements, function (index, item) {
            if ($(item).is(':visible')) $(item).hide();
        })
    } else {
        return true;
    }
});

$(document).on('click', '.closeagentcall', function () {
    $(this).parent().parent('.communicationcall').hide();
});
$(document).on('click', '.closeagentemail', function () {
    $(this).parent().parent('.communicationemail').hide();
});
$(document).on('click', '.closeagentother', function () {
    $(this).parent().parent('.communicationother').hide();
});

$(document).ready(function () {

    sessionStorage.removeItem(NotificationPageKey);
    sessionStorage.removeItem(NotificationChatPageKey);
    sessionStorage.removeItem(NotificationMobileChatDetailKey);
    sessionStorage.removeItem(NotificationMobileDiscussionKey);
    sessionStorage.removeItem(NotificationMobileInfoKey);
    $('body').addClass('daterangepickermobile');

    UpdateUnreadMessagesOnDashboard();
    SetSessionDetailsForTimeZone();
    fetchClientLogo();
    $('.PredictedBadWeatherCls').on('click', function () {
        let count = parseInt($(this).data('predictedbadweather'))
        if (count > 0) {
            let VesselId = $(this).data('vesselid');
            let VesselName = decodeURIComponent($(this).data('vesselname'));
            OpenModalPredictedBadWeather(VesselId, VesselName);
            $('#modalPredictedBadWeather').modal('show');
        }
    });
    AddMessagingUserIfNotExists();

    //$('.map-dashboard').click(function () {
    //    $('.map-dashboard iframe').css("pointer-events", "auto");
    //});

    //$(".map-dashboard").mouseleave(function () {
    //    $('.map-dashboard iframe').css("pointer-events", "none");
    //});

    fleetId = $('#fleetId').val()
    menuType = $('#menuType').val()
    vesselId = $('#vesselId').val()

    //BindFleetSummary(fleetId, menuType, vesselId, $('#selectedTitle').val());
    $("#fleetSelectionTitle").text("Viewing " + $('#selectedTitle').val());
    $(".spanfleetSelectionTitle").html("<em>Search Vessel / Fleet</em>");
    $("#fleetSelectionVesselTitle").text("Viewing "+$('#selectedTitle').val());

    BindCrewFleetSummary(fleetId, menuType, vesselId);
    BindOpexFleetSummary(fleetId, menuType, vesselId);
    BindInspectionFleetSummary(fleetId, menuType, vesselId);
    BindHazOccFleetSummary(fleetId, menuType, vesselId);
    BindCommercialFleetSummary(fleetId, menuType, vesselId);
    BindRightshipFleetSummary(fleetId, menuType, vesselId);
    BindPMSFleetSummary(fleetId, menuType, vesselId);

    BindDashboardVesselList(fleetId, menuType, vesselId)

    $('#btnLoadMoreVessels').on('click', LoadMoreVessels);

    //Fleet Summary Methods
    $(".list #divSeriousIncidents").click(function () {
        let count = parseInt($("#seriousIncidentCountId").val())
        if (count > 0) {
            $("#modalSeriousIncident").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId
            }
            GetSeriousIncidents(request);
        }
    });

    //RightShip Data
    $(".list #divRightShip").click(function () {
        let count = parseFloat($("#hdnRightShipCount").val())
        if (count > 0.00) {
            $("#modalRightShip").modal('show');
            fleetId = $('#fleetId').val();
            menuType = $('#menuType').val();
            vesselId = $('#vesselId').val();
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId
            }
            GetRightShipDetils(request);
        }
    });

    $(".list #divExperienceMatrix").click(function () {
        let count = parseInt($("#hdnExperienceMatrixCount").val())
        if (count > 0) {
            $("#modalExperienceMatrix").modal('show');
            fleetId = $('#fleetId').val();
            menuType = $('#menuType').val();
            vesselId = $('#vesselId').val();
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId
            }
            GetExperienceMatrixDetils(request);
        }
    });

    $(".list #divPscDetention").click(function () {
        let count = parseInt($("#pscDetentionCountId").val())
        if (count > 0) {
            $("#modalPscDetention").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId
            }
            GetPSCDetentionDetails(request);
        }
    });

    $(".list #divPscDefeciency").click(function () {
        let count = parseFloat($("#hdnPscDeficiencyRate").val())
        if (count > 0.00) {
            $("#modalPscDeficiency").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselIds": vesselId
            }
            GetPSCDeficiencyDetails(request);
        }
    });

    $(".list #divOverdueInspection").click(function () {
        let count = parseInt($("#hdnOverdueInspection").val())
        if (count > 0) {
            $("#modalOverdueInspection").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "EncryptedVesselId": vesselId
            }
            GetOverdueInspectionDetails(request);
        }
    });

    $(".list #divCriticalPMS").click(function () {
        let count = parseInt($("#hdnCriticalPMSCountId").val())
        if (count > 0) {
            $("#modalPMSOverdue").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId
            }
            GetCriticalPMSDetails(request);
        }
    });

    $(".list #divOilSpillsToWater").click(function () {
        let count = parseInt($("#hdnOilSpillToWaterCount").val())
        if (count > 0) {
            $("#modalOilSpill").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId
            }
            GetOilSpillsToWater(request);
        }
    });

    $(".list #divOffHireSummary").click(function () {
        let count = $("#offHireCountId").val()
        if (count != "0") {
            $("#modalOffHireSummary").modal('show');
            var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
            var endDate = moment().format("DD MMM YYYY");
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "VesselId": vesselId,
                "OffHireStartDate": startDate,
                "OffHireEndDate": endDate,
            }
            GetOffHireSummary(request);
        }
    });

    $(".list #divOmvFindingsSummary").click(function () {
        let count = parseInt($("#hdnOmvFindingRate").val())
        if (count > 0) {
            $("#modalOmvFindingsSummary").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "EncryptedVesselId": vesselId
            }
            GetOmvFindingsSummary(request);
        }
    });

    $(".list #divOverBudgetSummary").click(function () {
        let count = parseFloat($("#hdnOverBudgetCount").val())
        if (count != 0.00 && !isNaN(count)) {
            $("#modalOverBudgetSummary").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "EncryptedVesselId": vesselId
            }
            GetOverBudgetSummary(request);
        }
    });

    $(".list #divFuelEfficiencySummary").click(function () {
        let count = parseFloat($("#hdnFuelEfficiencyCount").val())
        if (count != 0.00) {
            $("#modalFuelEfficiencySummary").modal('show');
            fleetId = $('#fleetId').val()
            menuType = $('#menuType').val()
            vesselId = $('#vesselId').val()
            var request =
            {
                "FleetId": fleetId,
                "MenuType": menuType,
                "EncryptedVesselId": vesselId
            }
            GetFuelEfficiencySummary(request);
        }
    });

    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
        console.log(message);
        $('#modalSeriousIncident').unblock();
        $('#modalPscDetention').unblock();
        $('#modalPscDeficiency').unblock();
        $('#modalOmvFindingsSummary').unblock();
        $('#modalOverBudgetSummary').unblock();
        $('#modalPMSOverdue').unblock();
        $('#modalOilSpill').unblock();
        $('#modalFuelEfficiencySummary').unblock();
        $('#modalRightShip').unblock();
        $('#modalOverdueInspection').unblock();
        $('#modalExperienceMatrix').unblock();
    };


    if ($(".home-left-right-padding").is(":visible")) {
        $('body').addClass('back-disabled');
    }
    else {
        $('body').removeClass('back-disabled');
    }
    AjaxError();

    $("#vesseldropdown").select2({
        theme: "bootstrap4",
        placeholder: "Select",
        dropdownCssClass: 'dropdown-outline',
        dropdownAutoWidth: true,
    });

    //Option 1 Load More Script
    $("div.Vessel-box").slice(0, 3).show();
    $("#loadMore").on('click', function (e) {
        e.preventDefault();
        $("div.Vessel-box:hidden").slice(0, 3).slideDown();
        if ($("div.Vessel-box:hidden").length == 0) {
            $("#loadMore").fadeOut('slow');
        }
        $('html,body').animate({
            scrollTop: $(this).offset().top
        }, 1500);
    });


    //Option 2 On Scroll Script
    $("div.Vessel-box").slice(0, 3).show();
    $("#loadMore2").on('click', function (e) {
        e.preventDefault();
        $("div.Vessel-box:hidden").slice(0, 3).slideDown();
        if ($("div.Vessel-box:hidden").length == 0) {
            $("#loadMore2").fadeOut('slow');
        }
        //$('html,body').animate({
        //	scrollTop: $(this).offset().top
        //}, 1500);
    });




    //dashboard dynamic height
    var allvesselheight = $("#dashboard-section-2").height();
    var newscrollerheight = $(".dashboard-section-2-scroller").height();
    var carouselheight = $("#carouselControlsdashboard").height();
    var navheight = $(".dashboard-section-2 .card-header").height();
    var approvalOuter = $(".dashboard-section-2 .card-body .tab-pane").outerHeight();
    var approval = $(".dashboard-section-2 .card-body .tab-pane").height();

    var $window = $('#dashboard-section-2').on('resize', function () {
        SetPurchaseOrderScrollerHeight(this);
    }).trigger('resize');

    //$("#fleetOverviewTree").fancytree({
    //	checkbox: false,
    //	selectMode: 3,
    //	icon: false,

    //	source: $.ajax({
    //		url: "/Dashboard/GetNavigationTreeTopLevel",
    //		dataType: "json",
    //		data: { treeType: "FleetOverviewTree" }
    //	}),

    //	lazyLoad: function (event, data) {
    //		data.result =
    //			$.ajax({
    //				url: "/Dashboard/GetNavigationTreeLevel",
    //				dataType: "json",
    //				data: { key: data.node.key, userMenuItemType: data.node.data.userMenuItemType }
    //			});
    //	},

    //	activate: function (event, data) {
    //		OnSideBarMenuSelection(data.node.data.userMenuItemType, data.node.key, false)
    //	},
    //	select: function (event, data) {
    //		$("#statusLine").text(
    //			event.type + ": " + data.node.isSelected() + " " + data.node
    //		);
    //	},
    //});

    var gridHeight = $("#grdActions").height()
    var carouselHeight = $("#carouselExampleControls2").height();

    //$(window).resize(function () {
    //	if (($(this).width() >= 1024) && (gridHeight != carouselHeight)) {

    //		$("#carInner").height(gridHeight);
    //		$("#carImg1").height(gridHeight);
    //		$("#carImg2").height(gridHeight);
    //		$("#carImg3").height(gridHeight);
    //	}
    //});

    //if (($(this).width() >= 1024) && (gridHeight != carouselHeight)) {

    //	$("#carInner").height(gridHeight);
    //	$("#carImg1").height(gridHeight);
    //	$("#carImg2").height(gridHeight);
    //	$("#carImg3").height(gridHeight);
    //}

    RegisterTabSelectionEvent('.mobileTabClick', DashboardFilterKey);

    $('#tab-2').click(function () {
        $('.app-main__inner').addClass("fixed-top-margin-legends");
        $('.app-main__inner').removeClass("mapmargin0");
    });
    $('#tab-1').click(function () {
        $('.app-main__inner').removeClass("fixed-top-margin-legends");
        $('.app-main__inner').removeClass("mapmargin0");
    });
    $('#tab-3').click(function () {
        $('.app-main__inner').removeClass("fixed-top-margin-legends");
        $('.app-main__inner').addClass("mapmargin0");
    });
    $('#tab-3').click(function () {
        $(".tab-box-1").hide();
        $(".tab-box-2").hide();
        $(".tab-box-3-map").show();
        $(".tab-1").removeClass("active");
        $(".tab-2").removeClass("active");
        $(".tab-3").addClass("active");
        $(window).scrollTop(0);

        if (($(window).width() < MobileScreenSize)) {
            var windowheightmap = $(window).height();
            var headertop = $(".app-header").outerHeight();
            var tabfooter = $(".mobile-tab-threedefault").outerHeight();
            $("#generalMap").css({
                "height": windowheightmap - headertop - tabfooter
            });
        }
    });

    $("#homediv").click(function () {
        if (($(window).width() < MobileScreenSize)) {
            var windowheightmap = $(window).height();
            var headertop = $(".app-header").outerHeight();
            var tabfooter = $(".mobile-tab-threedefault").outerHeight();
            $("#generalMap").css({
                "height": windowheightmap - headertop - tabfooter
            });
        }
    });

    $('#tab-1').click(function () {
        $(".tab-box-3-map").hide();
        $(".tab-3").removeClass("active");
    });
    $('#tab-2').click(function () {
        $(".tab-box-3-map").hide();
        $(".tab-3").removeClass("active");
    });

    if (($(window).width() < MobileScreenSize)) {
        var MobilTabCls = $("#ActiveMobileTabClass").val();
        $('.' + MobilTabCls)[0].click();
    }

    $('.height-equal-text').matchHeight()
    $('.height-equal').matchHeight();

    if (document.getElementById("generalMapdesktop")) {
        LoadMapdesktop();
    }

    //OnSideBarMenuSelection('UserAllVessel', '', true);

    $('.fleet-option').click(function () {
        var fleetOption = $(this)[0].innerText;
        $('#fleetTitle').text(fleetOption + ' Overview');

        if (fleetOption == "All") {
            $('#fleetTitle').text('All Vessels Overview');
        }
    });


    //---------------------------------------------RSS FEED-----------------------------------------------------------------------
    //var url = "https://vgrouplimited.com/our-group/news-and-press-releases/feed";
    //$.ajax({
    //	url: url,
    //	datatype: 'jsonp',
    //	success: function (data) {

    //		$(data).find("item").each(function () {
    //			var item = $(this);
    //			var div = document.createElement("div");
    //			var titletext = item.find("title").text();
    //			var pubDate = item.find("pubDate").text();
    //			var descriptiontext = item.find("description")[0].childNodes[0].data;


    //			var h4 = document.createElement("h4");
    //			h4.appendChild(document.createTextNode(titletext));


    //			var em = document.createElement("em");
    //			em.appendChild(document.createTextNode(pubDate));


    //			div.appendChild(h4);
    //			div.appendChild(em);
    //			div.innerHTML = div.innerHTML + descriptiontext;
    //			div.setAttribute("class", "carousel-item");

    //			$("#carInner").append(div);

    //		})
    //	},
    //	error: function (data) {
    //		console.log(data);
    //	}
    //});

    //if ($('#ActiveMobileTabClass').val() == "tab-2" && screen.width < MobileScreenSize) {
    //    MobileTab_Overview();
    //    Mobile_Tabs();
    //    $('.app-main__inner').addClass("fixed-top-margin-legends");
    //}
    $('#hideDeficiencyPSC').on('click', function () {
        $('#dtPscDeficiency').DataTable().draw();
    });
    $('#hideDeficiencyOMV').on('click', function () {
        $('#dtOmvFindings').DataTable().draw();
    });

    $.fn.dataTable.ext.search.push(
        function (settings, searchData, index, rowData, counter) {
            if (settings.nTable.id == 'dtPscDeficiency') {
                if ($("#hideDeficiencyPSC").is(':checked')) {
                    if (rowData.findingCount === 0) {
                        return false;
                    }
                }
            }
            if (settings.nTable.id == 'dtOmvFindings') {
                if ($("#hideDeficiencyOMV").is(':checked')) {
                    if (rowData.findingCount === 0) {
                        return false;
                    }
                }
            }
            return true;
        }
    );

    $("#expanddashboardmap").on('click', function () {
        ShowFullMap();
    });

    GetNotesListOnDashboardPage(false);

    //Notes on scroll
    $('#divNotesTab').on('scroll', function () {
        if ($(this).scrollTop() + Math.round($(this).innerHeight()) >= $(this)[0].scrollHeight) {
            let PageNumber = parseInt($('#hdnNotesTabCurrentPageNumber').val());
            PageNumber++;
            $('#hdnNotesTabCurrentPageNumber').val(PageNumber);
            if ($('#hdnNotesTabHasNextPage').val() == "true") {
                GetNotesListOnDashboardPage(true);
            }
            else {
                $('#hdnNotesTabCurrentPageNumber').val(1);
            }
        }
    });

    CreateReminderAlertOnDashboard();
    GetApprovalsList();
    if (screen.width < MobileScreenSize) {
        $("#divOmvFindingsSummary, #divPscDefeciency").attr('data-original-title', "");
    }
    if (($(window).width() < MobileScreenSize)) {
        $(".fleetkpimodal").on('shown.bs.modal', function (e) {
            $('body').addClass('fixedmodalbody');
        });
        $(".fleetkpimodal").on('hidden.bs.modal', function (e) {
            $('body').removeClass('fixedmodalbody');
            $('html,body').animate({ scrollTop: ($('html').height()) - ($(window).height()) }, 10);
        });
    }

    if (($(window).width() < MobileScreenSize)) {
        $("#cboDashboardMobileVesselSearch").select2({
            theme: "bootstrap4",
            placeholder: "Search for a vessel...",
            minimumInputLength: 0,
            allowClear: false,
            ajax: {
                url: '/Dashboard/GetVesselLookup',
                dataType: 'json'
            },
            templateResult: formatMobileVesselSearchResult,
            templateSelection: formatMobileVesselSearchRepoSelection
        });

        $('#cboDashboardMobileVesselSearch').on('select2:open', function (e) {
            $('.select2-results__options').addClass('overflow-x-hidden');
            $('.select2-container--bootstrap4 .select2-results>.select2-results__options').css("max-height", "153px");
            $('.select2-container--bootstrap4 .select2-results>.select2-results__options').css("height", "auto");
        });
        $('#cboDashboardMobileVesselSearch').on('select2:select', function (e) {
            var repo = e.params.data;
            let vesselURL = repo.vesselURL;
            let id = repo.id;

            if (!IsNullOrEmptyOrUndefinedLooseTyped(id)) {
                window.location.href = window.location.protocol + "//" + window.location.host + '/Dashboard/VesselDetailsMobile/?VesselId=' + vesselURL;
            }
        });
    }
    if ($('#EnableTour').val() == 'true' || $('#EnableTour').val() == true || $('#EnableTour').val() == 'True') {

        if (screen.width > MobileScreenSize) {
            var tour = new Tour({
                steps: [
                    {
                        element: "#vchatmainboxsection",
                        title: "<img src='/images/v-chat.svg' width='40' class='d-block'> V.Chat",
                        content: " You’ll find V.Chat here. Have quick conversations with the key contacts for your vessels.",
                        placement: "left"
                    },
                    {
                        element: "#sidebarvchatsection",
                        title: "<img src='/images/v-chat.svg' width='40' class='d-block'> V.Chat",
                        content: "V.Chat can also be accessed<br> from this sidebar.",
                        placement: "left"
                    },
                    {
                        element: "#topmenubarvchatsection",
                        title: "<img src='/images/v-chat.svg' width='40' class='d-block'> V.Chat",
                        content: "And from this top menu.",
                        placement: "bottom"
                    },
                    {
                        element: "#approvalsection",
                        title: "<img src='/images/approve-leftmenu-green.svg' width='40' class='d-block'> Approvals",
                        content: "Do you have approvals for Purchase Orders or JSA? <br> You can easily approve direct from here…",
                        placement: "bottom"
                    },
                    {
                        element: "#vesselandfleetsectiondesktop",
                        title: "<img src='/images/vesseltopicon.svg' width='40' class='d-block'> Vessels and Fleets",
                        content: "You’ll have access to your vessels and fleets, but you can create favourites…",
                        placement: "bottom"
                    }
                ],
                backdrop: false,
                storage: false,
                keyboard: true,
                framework: 'bootstrap4',
                animate: true,
                showProgressBar: false,
                showProgressText: false,
            });
        }

        if (screen.width < MobileScreenSize) {
            var tourmobile = new Tour({
                steps: [
                    {
                        element: "#vchatmainboxsection",
                        title: "<img src='/images/v-chat.svg' width='40' class='d-block'> V.Chat",
                        content: " You’ll find V.Chat here. Have quick conversations with the key contacts for your vessels. <br> It’s also on the menu and sidebar.",
                        placement: "left"
                    },
                    {
                        element: "#approvalsection",
                        title: "<img src='/images/approve-leftmenu-green.svg' width='40' class='d-block'> Approvals",
                        content: "Do you have approvals for Purchase Orders or JSA? <br> You can easily approve direct from here…",
                        placement: "bottom"
                    },
                    {
                        element: "#vesselandfleetsectionmobile",
                        title: "<img src='/images/vesseltopicon.svg' width='40' class='d-block'> Vessels and Fleets",
                        content: "You’ll have access to your vessels and fleets, but you can create favourites…",
                        placement: "bottom"
                    }
                ],
                backdrop: false,
                storage: false,
                keyboard: true,
                framework: 'bootstrap4',
                animate: true,
                showProgressBar: false,
                showProgressText: false,
            });
        }

        //tour.init();
        //tour.start();

        $(document).on("click", "[data-tour]", function (e) {
            e.preventDefault();
            if ($(this).hasClass("disabled")) {
                return;
            }
            tour.restart();
            $("#toumodal").modal('hide');
        });
        $(document).on("click", "[data-tourmobile]", function (e) {
            e.preventDefault();
            if ($(this).hasClass("disabled")) {
                return;
            }
            tourmobile.restart();
            $("#toumodal").modal('hide');
        });

        $('#tourstart').click(function () {
            $("#toumodal").modal('show');
        });
    }
});

//$(window).on('load', function () {
//    $("#toumodal").modal('show');
//});


$(window).on('load', function () {
    setTimeout(function () {
        if (($(window).width() < MobileScreenSize)) {
            if ($('#tab-3').hasClass('active')) {
                var windowheightmap = $(window).height();
                var headertop = $(".app-header").outerHeight();
                var tabfooter = $(".mobile-tab-threedefault").outerHeight();
                $("#generalMap").css({
                    "height": windowheightmap - headertop - tabfooter
                });
            }
        }
    }, 1000);

});

$(document).on('click', '.notename', function () {
    let id = $(this).data('id');
    AddClassIfAbsent(".notes-sidebar-open", 'settings-open');
    if ($(".notes-sidebar-open").hasClass('settings-open')) {
        $('.addfixed').addClass('addfixedleft');
        $('.add-list').addClass('addfixedleft');
    }
    else {
        $('.addfixed').removeClass('addfixedleft');
        $('.add-list').removeClass('addfixedleft');
    }
    EditNote(id);
});

$(document).on('click', '.dismissReminder', function () {
    let id = $(this).data('id');
    ReminderDismissed(id);
});

$(document).on('click', '.navigateToDetails', function () {
    let encryptedNoteId = $(this).data('noteid');
    let encryptedVesselId = $(this).data('vesselid');
    NavigateToNoteRecordDetails(encryptedNoteId, encryptedVesselId)
});

$(document).on('click', '.showFromAgentDetails, .showToAgentDetails ', function () {
    let urlRequest = $(this).data('encryptedposid');
    let portname = decodeURIComponent($(this).data('portname'));
    CurrentVoyageAgentDetails(urlRequest, portname);
});

//Option 1 Load More Script for IPAD
$(window).on('resize', function () {
    if ($(window).width() > 1021 && $(window).width() < 1025) {
        $("div.Vessel-box").slice(0, 4).show();
        $("#loadMore").on('click', function (e) {
            e.preventDefault();
            $("div.Vessel-box:hidden").slice(0, 1).slideDown();
            if ($("div.Vessel-box:hidden").length == 0) {
                $("#loadMore").fadeOut('slow');
            }
            $('html,body').animate({
                scrollTop: $(this).offset().top
            }, 1500);
        });
    }
});


//Option 2 On Scroll Script for IPAD
$(window).on('resize', function () {
    if ($(window).width() > 1021 && $(window).width() < 1025) {
        $("div.Vessel-box").slice(0, 4).show();
        $("#loadMore2").on('click', function (e) {
            e.preventDefault();
            $("div.Vessel-box:hidden").slice(0, 1).slideDown();
            if ($("div.Vessel-box:hidden").length == 0) {
                $("#loadMore2").fadeOut('slow');
            }
            //$('html,body').animate({
            //	scrollTop: $(this).offset().top
            //}, 1500);
        });
    }
});

//Option 2 On Scroll Script
$(window).on('scroll', function () {
    if ($(window).scrollTop() >= $(document).height() - $(window).height()) {
        $("#loadMore2").click();
    }
}).scroll();

$(window).on('beforeunload', function () {
    $(window).scrollTop(0);
});

function SetPurchaseOrderScrollerHeight(parent) {
    var approvalOuter = $(".dashboard-section-2 .card-body .tab-pane").outerHeight();
    var approval = $(".dashboard-section-2 .card-body .tab-pane").height();
    var approvalOuterHeight = approvalOuter - approval;

    var height = $(parent).height() - ($("#carouselControlsdashboard").height() + $(".dashboard-section-2 .card-header").height() + approvalOuterHeight + 10);
    $(".dashboard-section-2-scroller").height(height);
}

function LoadMap() {
    map = new GMaps({
        el: "#generalMap",
        lat: 11.774276,
        lng: 73.207846,
        width: "100%",
        height: "320px",
        zoom: 3,
        markerClusterer: function (map) {
            return new MarkerClusterer(map);
        },
        styles: [
            {
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#8ec3b9"
                    }
                ]
            },
            {
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1a3646"
                    }
                ]
            },
            {
                "featureType": "administrative.country",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#4b6878"
                    }
                ]
            },
            {
                "featureType": "administrative.land_parcel",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#64779e"
                    }
                ]
            },
            {
                "featureType": "administrative.province",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#4b6878"
                    }
                ]
            },
            {
                "featureType": "landscape.man_made",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#334e87"
                    }
                ]
            },
            {
                "featureType": "landscape.natural",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#023e58"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#283d6a"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#6f9ba5"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#023e58"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#3C7680"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#304a7d"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#98a5be"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#2c6675"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#255763"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#b0d5ce"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#023e58"
                    }
                ]
            },
            {
                "featureType": "transit",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#98a5be"
                    }
                ]
            },
            {
                "featureType": "transit",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "featureType": "transit.line",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#283d6a"
                    }
                ]
            },
            {
                "featureType": "transit.station",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#3a4762"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#0e1626"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#4e6d70"
                    }
                ]
            }
        ]
    });

    var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';

    var features = [
        {
            position: new google.maps.LatLng(-9.889369, 81.645346),
            details: { id: "101", Name: "Sichem Amethyst", Status: "In Management", VesselType: "Chemical Tanker", Destination: "", Speed: "9.8 Knots", ETA: "", Heading: "270.3&deg;", ReplicationType: "Web Service", LastReported: "", LastReplicated: "" }
        },
        {
            position: new google.maps.LatLng(-14.866810, 75.317221),
            details: { id: "102", Name: "Sicehm Eagle" }
        },
        {
            position: new google.maps.LatLng(11.774276, 73.207846),
            details: { id: "103", Name: "Queen Esther" }
        },
        {
            position: new google.maps.LatLng(10.221350, 70.922690),
            details: { id: "104", Name: "Sichem Mumbai" }
        },
        {
            position: new google.maps.LatLng(9.528647, 74.262533),
            details: { id: "201", Name: "Sichem Etsher" }
        }
    ];

    var markers = [];
    features.forEach(function (feature) {

        var marker = map.addMarker({
            position: feature.position,
            details: feature.details,
            label: {
                color: '#ffffff',
                fontWeight: 'bold',
                text: feature.details.Name,
                scaledSize: new google.maps.Size(32, 38),
                labelOrigin: new google.maps.Point(9, 9)
            },
            icon: {
                labelOrigin: new google.maps.Point(11, 50),
                url: '/images/icons/marker_red.png',
                size: new google.maps.Size(22, 40),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(11, 40),
            },
            click: function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: ('<div class="card d-none d-sm-block"> <div class="map-info"> <div class="pl-2"> <div class="card-title"><a href="/Vessel/VesselPositionList" class="text-teal-hover-black">Sichem Amethyst</a></div> <span class="card-subtitle"> In Management <span class="ml-2 border-left pl-2">Chemical Tanker (IMO I & II)</span> </span> </div> <div class="card-body p-0"> <ul class="list-group list-group-flush"> <li class="p-2 list-group-item">  <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Destination </td> <td>  </td> <td class="font-weight-bold" width="150px"> Speed (Reported) </td> <td> 9.8 Knots </td> </tr> <tr> <td class="font-weight-bold"> ETA </td> <td>  </td> <td class="font-weight-bold"> Heading (Reported) </td> <td> 270.3&deg; </td> </tr> <tr> <td class="font-weight-bold"> Last Reported </td> <td> <span> 30 Jul 2020 13:50 <i class="fa fa-info-circle ml-2" title="Last known Position information provided by polestar" data-toggle="tooltip" data-placement="bottom"></i> </span> </td> </tr> </table>  </li> <li class="p-2 list-group-item"> <div class="row"> <div class="col-6"> <div id="accordionCharter" class="mt-2"> <div id="charter" data-toggle="collapse" data-target="#collapseCharter" aria-expanded="true" aria-controls="collapseOne" class="text-left cursor-pointer mb-2"> <span class="card-title"> <i class="fa fa-chevron-up mr-3"></i> Charter </span> </div> <div data-parent="#accordionCharter" id="collapseCharter" aria-labelledby="charter" class="collapse show"> <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Average Speed </td> <td width="150px"> 2.07 knots </td> <td> </td> </tr> <tr> <td class="font-weight-bold"> Charter Speed </td> <td> <span>13.5 knots</span> </td> </tr> <tr> <td class="font-weight-bold"> Charter Fo </td> <td> <span>15 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Charter Go </td> <td> <span>15 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Average FO </td> <td> <span>8.5 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Average GO </td> <td> <span>1.29 mt</span> </td> </tr> </table> </div> </div> </div> <div class="col-6 border-left"> <div id="accordionWeather" class="mt-2"> <div id="weather" data-toggle="collapse" data-target="#collapseWeather" aria-expanded="true" aria-controls="collapseOne" class="text-left cursor-pointer mb-2"> <span class="card-title"> <i class="fa fa-chevron-up mr-3"></i> Weather </span> </div> <div data-parent="#accordionWeather" id="collapseWeather" aria-labelledby="weather" class="collapse show"> <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Air Pressure </td> <td width="150px"> 1005 mbar </td> <td> </td> </tr> <tr> <td class="font-weight-bold"> Wind Direction </td> <td> <span>223&deg;</span> </td> </tr> <tr> <td class="font-weight-bold"> Wind Speed </td> <td> <span>17.3 knots</span> </td> </tr> <tr> <td class="font-weight-bold"> Wave Direction </td> <td> <span>202&deg;</span> </td> </tr> <tr> <td class="font-weight-bold"> Wave Height </td> <td> <span>3 m</span> </td> </tr> <tr> <td class="font-weight-bold"> Forecast Time </td> <td> <span>30 Jul 2020 05:30 PM</span> </td> </tr> </table> </div> </div> </div> </div> </li> </ul> </div> </div> </div>' + '<div class="d-block d-sm-none"> <b> ' + e.details.Name + ' </b> <br /> ' + e.position.lat() + ', ' + e.position.lng() + ' </div>')
                });
                infoWindow.open(map, e);
            }
        });
    });


    map.markerClusterer = new MarkerClusterer(map, markers, { imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m' });

}

function LoadMapdesktop() {
    map = new GMaps({
        el: "#generalMapdesktop",
        lat: 11.774276,
        lng: 73.207846,
        width: "100%",
        height: "320px",
        zoom: 3,
        markerClusterer: function (map) {
            return new MarkerClusterer(map);
        },
        styles: [
            {
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#8ec3b9"
                    }
                ]
            },
            {
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1a3646"
                    }
                ]
            },
            {
                "featureType": "administrative.country",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#4b6878"
                    }
                ]
            },
            {
                "featureType": "administrative.land_parcel",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#64779e"
                    }
                ]
            },
            {
                "featureType": "administrative.province",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#4b6878"
                    }
                ]
            },
            {
                "featureType": "landscape.man_made",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#334e87"
                    }
                ]
            },
            {
                "featureType": "landscape.natural",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#023e58"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#283d6a"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#6f9ba5"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#023e58"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#3C7680"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#304a7d"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#98a5be"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#2c6675"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "color": "#255763"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#b0d5ce"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#023e58"
                    }
                ]
            },
            {
                "featureType": "transit",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#98a5be"
                    }
                ]
            },
            {
                "featureType": "transit",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#1d2c4d"
                    }
                ]
            },
            {
                "featureType": "transit.line",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#283d6a"
                    }
                ]
            },
            {
                "featureType": "transit.station",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#3a4762"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#0e1626"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#4e6d70"
                    }
                ]
            }
        ]
    });

    var iconBase = 'https://maps.google.com/mapfiles/kml/shapes/';

    var features = [
        {
            position: new google.maps.LatLng(-9.889369, 81.645346),
            details: { id: "101", Name: "Sichem Amethyst", Status: "In Management", VesselType: "Chemical Tanker", Destination: "", Speed: "9.8 Knots", ETA: "", Heading: "270.3&deg;", ReplicationType: "Web Service", LastReported: "", LastReplicated: "" }
        },
        {
            position: new google.maps.LatLng(-14.866810, 75.317221),
            details: { id: "102", Name: "Sicehm Eagle" }
        },
        {
            position: new google.maps.LatLng(11.774276, 73.207846),
            details: { id: "103", Name: "Queen Esther" }
        },
        {
            position: new google.maps.LatLng(10.221350, 70.922690),
            details: { id: "104", Name: "Sichem Mumbai" }
        },
        {
            position: new google.maps.LatLng(9.528647, 74.262533),
            details: { id: "201", Name: "Sichem Etsher" }
        }
    ];

    var markers = [];
    features.forEach(function (feature) {

        var marker = map.addMarker({
            position: feature.position,
            details: feature.details,
            label: {
                color: '#ffffff',
                fontWeight: 'bold',
                text: feature.details.Name,
                scaledSize: new google.maps.Size(32, 38),
                labelOrigin: new google.maps.Point(9, 9)
            },
            icon: {
                labelOrigin: new google.maps.Point(11, 50),
                url: '/images/icons/marker_red.png',
                size: new google.maps.Size(22, 40),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(11, 40),
            },
            click: function (e) {
                var infoWindow = new google.maps.InfoWindow({
                    content: ('<div class="card d-none d-sm-block"> <div class="map-info"> <div class="pl-2"> <div class="card-title"><a href="/Vessel/VesselPositionList" class="text-teal-hover-black">Sichem Amethyst</a></div> <span class="card-subtitle"> In Management <span class="ml-2 border-left pl-2">Chemical Tanker (IMO I & II)</span> </span> </div> <div class="card-body p-0"> <ul class="list-group list-group-flush"> <li class="p-2 list-group-item">  <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Destination </td> <td>  </td> <td class="font-weight-bold" width="150px"> Speed (Reported) </td> <td> 9.8 Knots </td> </tr> <tr> <td class="font-weight-bold"> ETA </td> <td>  </td> <td class="font-weight-bold"> Heading (Reported) </td> <td> 270.3&deg; </td> </tr> <tr> <td class="font-weight-bold"> Last Reported </td> <td> <span> 30 Jul 2020 13:50 <i class="fa fa-info-circle ml-2" title="Last known Position information provided by polestar" data-toggle="tooltip" data-placement="bottom"></i> </span> </td> </tr> </table>  </li> <li class="p-2 list-group-item"> <div class="row"> <div class="col-6"> <div id="accordionCharter" class="mt-2"> <div id="charter" data-toggle="collapse" data-target="#collapseCharter" aria-expanded="true" aria-controls="collapseOne" class="text-left cursor-pointer mb-2"> <span class="card-title"> <i class="fa fa-chevron-up mr-3"></i> Charter </span> </div> <div data-parent="#accordionCharter" id="collapseCharter" aria-labelledby="charter" class="collapse show"> <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Average Speed </td> <td width="150px"> 2.07 knots </td> <td> </td> </tr> <tr> <td class="font-weight-bold"> Charter Speed </td> <td> <span>13.5 knots</span> </td> </tr> <tr> <td class="font-weight-bold"> Charter Fo </td> <td> <span>15 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Charter Go </td> <td> <span>15 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Average FO </td> <td> <span>8.5 mt</span> </td> </tr> <tr> <td class="font-weight-bold"> Average GO </td> <td> <span>1.29 mt</span> </td> </tr> </table> </div> </div> </div> <div class="col-6 border-left"> <div id="accordionWeather" class="mt-2"> <div id="weather" data-toggle="collapse" data-target="#collapseWeather" aria-expanded="true" aria-controls="collapseOne" class="text-left cursor-pointer mb-2"> <span class="card-title"> <i class="fa fa-chevron-up mr-3"></i> Weather </span> </div> <div data-parent="#accordionWeather" id="collapseWeather" aria-labelledby="weather" class="collapse show"> <table class="table table-borderless map-info-table"> <tr> <td class="font-weight-bold" width="150px"> Air Pressure </td> <td width="150px"> 1005 mbar </td> <td> </td> </tr> <tr> <td class="font-weight-bold"> Wind Direction </td> <td> <span>223&deg;</span> </td> </tr> <tr> <td class="font-weight-bold"> Wind Speed </td> <td> <span>17.3 knots</span> </td> </tr> <tr> <td class="font-weight-bold"> Wave Direction </td> <td> <span>202&deg;</span> </td> </tr> <tr> <td class="font-weight-bold"> Wave Height </td> <td> <span>3 m</span> </td> </tr> <tr> <td class="font-weight-bold"> Forecast Time </td> <td> <span>30 Jul 2020 05:30 PM</span> </td> </tr> </table> </div> </div> </div> </div> </li> </ul> </div> </div> </div>' + '<div class="d-block d-sm-none"> <b> ' + e.details.Name + ' </b> <br /> ' + e.position.lat() + ', ' + e.position.lng() + ' </div>')
                });
                infoWindow.open(map, e);
            }
        });
    });


    map.markerClusterer = new MarkerClusterer(map, markers, { imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m' });

}

function OnSideBarMenuSelection(menuItemType, id, isFirstTime) {
    if (isFirstTime == false) {
        $('#Overview-Menu').toggleClass("show");
    }
    var data = {
        'Identifier': id,
        'UserMenuItemType': menuItemType
    }
    $.ajax({
        url: "/PurchaseOrder/PostGetOpenOrdersByVesselIds",
        type: "POST",
        dataType: "JSON",
        data: data,
        success: function (data) {
            $('#InProcessCount').text(data.orderInProcessCount);
            $('#OrderedCount').text(data.orderedCount);
            $('#DeliveryOnTheWayCount').text(data.orderDeliveryOnTheWayCount);
            $('#AuthorisationCount').text(data.authorisationCount);
            $('#RecievedIn30DaysCount').text(data.recievedIn30DaysCount);
        }
    });
}

window.OnSideBarMenuSelection = OnSideBarMenuSelection;

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

//radial graph

var options = {
    series: [50, 60, 40],
    //data: [150, 160, 100],
    chart: {
        height: 220,
        width: '100%',
        type: 'radialBar',
    },
    plotOptions: {
        radialBar: {
            offsetY: -50,
            offsetX: -30,
            startAngle: -145,
            endAngle: 145,
            hollow: {
                margin: 0,
                size: '15%',
                background: 'transparent',
            },
            dataLabels: {
                name: {
                    show: true,
                },
                value: {
                    show: true,
                }
            }
        }
    },
    colors: ['#8283be', '#338dc9', '#54bce8'],
    labels: ['Cht. Speed', 'Cht. Loaded', 'Actual'],
    legend: {
        show: true,
        floating: false,
        fontSize: '10px',
        position: 'right',
        color: '#666666',
        horizontalAlign: 'top',
        fontFamily: 'Open Sans',
        offsetX: 0,
        offsetY: 0,
        labels: {
            useSeriesColors: false,
        },
        markers: {
            size: 0,
            width: 12,
            height: 12,
            fillColors: undefined,
            radius: 0
        },
        formatter: function (seriesName, opts) {
            return seriesName + ":  " + (opts.w.globals.series[opts.seriesIndex])
        },
        itemMargin: {
            vertical: 1,
            horizontal: 2
        }
    },
    title: {
        text: 'Speed',
        align: 'center',
        offsetX: 0,
        offsetY: 8,
        style: {
            fontSize: '0.688rem',
            fontWeight: '600',
            color: '#000000'
        },
    },
    responsive: [{
        breakpoint: 767,
        options: {
            plotOptions: {
                radialBar: {
                    offsetX: 0,
                    offsetY: -45,
                }
            },
            legend: {
                offsetY: -30,
                offsetX: 0,
                position: 'bottom',
            },
        }
    },
    {
        breakpoint: 769,
        options: {
            legend: {
                offsetY: 0,
                offsetX: -45,

            },
            plotOptions: {
                radialBar: {
                    offsetX: 20,
                }
            },
        }
    }]
};

var chart = new ApexCharts(document.querySelector("#charterorderspeed"), options);
chart.render();




// fuel consuption graph
//get the bar chart canvas
var ctx = $("#fuel-consuption");
//bar chart data
var data = {
    labels: ['FO', 'DO'],
    datasets: [
        {
            label: 'Cht. Loaded',
            data: [9.1, 9.1],
            backgroundColor: ["#57bbb1", "#57bbb1"],
            barPercentage: 0.5,
            barThickness: 20,
            maxBarThickness: 20,
            minBarLength: 2,
        },
        {
            label: "Cht. Actual",
            data: [13.9, 1.95],
            backgroundColor: ["#5a91cc", "#5a91cc"],
            barPercentage: 0.5,
            barThickness: 20,
            maxBarThickness: 20,
            minBarLength: 2,
        }
    ]
};
//options
var options = {
    tooltips: {
        enabled: false
    },
    layout: {
        padding: {
            left: 0,
            right: 0,
            top: 0,
            bottom: 0
        }
    },
    hover: {
        animationDuration: 1
    },
    animation: {
        duration: 1,
        onComplete: function () {
            var chartInstance = this.chart,
                ctx = chartInstance.ctx;
            ctx.textAlign = 'center';
            ctx.fillStyle = "rgba(0, 0, 0, 1)";
            ctx.textBaseline = 'bottom';
            this.data.datasets.forEach(function (dataset, i) {
                var meta = chartInstance.controller.getDatasetMeta(i);
                meta.data.forEach(function (bar, index) {
                    var data = dataset.data[index];
                    ctx.fillText(data, bar._model.x, bar._model.y - 5);
                });
            });
        }
    },
    responsive: true,
    maintainAspectRatio: false,
    title: {
        display: true,
        position: "top",
        text: "Fuel Consuption (Daily Avg)",
        useSeriesColors: true,
        align: 'center',
        offsetX: -10,
        offsetY: -10,
        style: {
            fontSize: '10px',
            fontWeight: 'normal',
            fontFamily: 'Open Sans',
            fontColor: '#000000',
        },
    },
    legend: {
        display: true,
        position: "right",
        labels: {
            fontColor: "#666666",
            fontSize: 12,
            useSeriesColors: true,
            fontFamily: 'Open Sans',
            horizontalAlign: 'top',
        },
        markers: {
            width: 12,
            height: 12,
            strokeWidth: 0,
            strokeColor: '#fff',
            fillColors: undefined,
            radius: 0,
            customHTML: undefined,
            onClick: undefined,
            offsetX: 0,
            offsetY: 0
        },
        itemMargin: {
            horizontal: 5,
            vertical: 0
        },
        onItemClick: {
            toggleDataSeries: true
        },
        onItemHover: {
            highlightDataSeries: true
        },
    },
    scales: {
        yAxes: [{
            ticks: {
                max: 20,
                min: 0,
                stepSize: 5
            }
        }]
    },
    responsive: [{
        breakpoint: 480,
        options: {
            legend: {
                offsetY: 0,
                offsetX: 0,
                position: 'bottom',
            },
            plotOptions: {
                radialBar: {
                    offsetX: 0,
                    offsetY: 0,
                }
            },
        }
    }],
    plugins: {
        datalabels: {
            display: true,
            align: 'center',
            anchor: 'center'
        }
    },
};
//create Chart class object
var chart = new Chart(ctx, {
    type: "bar",
    data: data,
    options: options
});

function BindVesselDetailsSummary(vesselid, parent, vesselIdentifier) {
    var request =
    {
        "VesselId": vesselid,
        "VesselIdentifier": vesselIdentifier,
        "IsFleetSelection": ($('#hdnIsFleetSelection').val() == 'true')
    }
    $.ajax({
        url: "/Dashboard/GetVesselDetails",
        type: "POST",
        dataType: "JSON",
        data: request,
        beforeSend: function (xhr) {
            $(parent).find('.vessel-summary').block({
                message: $(" " + loadercontent),
            });
        },

        success: function (data) {
            if (data != null) {
                //$(panelId+' .spanVesselName').text(data.name);
                //$(panelId +' .spanVesselFlag').text(data.flag);
                if (!IsNullOrEmpty(data.imo)) {
                    $(parent).find('.spanVesselImo').text(IMO_Prefix + ' ' + data.imo);
                }
                if (!IsNullOrEmpty(data.type)) {
                    $(parent).find('.spanVesselType').text(data.type);
                }
                if (!IsNullOrEmpty(data.vesselBuiltDate)) {
                    $(parent).find('.spanVesselBuiltDate').text(data.vesselBuiltDate);
                }
                if (!IsNullOrEmpty(data.vesselAge)) {
                    $(parent).find('.spanVesselAge').text(data.vesselAge);
                }
                if (data.image != null) {
                    $(parent).find(".imgvesselpicture").attr("src", "data:image/png;base64," + data.image + "");
                }
                
            }
        },
        complete: function () {
            $('.height-equal-vessel').matchHeight();
            $(parent).find('.vessel-summary').unblock();
        }
    });
}

function BindingVesselOfficerDetails(vesselId, parent) {
    $.ajax({
        url: "/Dashboard/GetVesselOfficeDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselId": vesselId
        },
        beforeSend: function (xhr) {
            $(parent).find('.officer-detail').block({
                message: $(" " + loadercontent),
            });
        },

        success: function (data) {
            if (data != null) {
                if (!IsNullOrEmpty(data.vesselChiefEnggName)) {
                    $(parent).find('.spanVesselChiefEngg').text(data.vesselChiefEnggName);
                }
                if (!IsNullOrEmpty(data.vesselMasterName)) {
                    $(parent).find('.spanVesselMaster').text(data.vesselMasterName);
                }
            }
        },
        complete: function () {
            $('.height-equal-vessel').matchHeight();
            $(parent).find('.officer-detail').unblock();
        }
    });
}


function BindCrewSummary(vesselId, parent) {
    $.ajax({
        url: "/Dashboard/GetCrewSummary",
        type: "POST",
        "data": {
            "input": vesselId,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            $(parent).find('.crew-panel').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {

            $(parent).find(".overDueCount").text(data.overdueCount);
            $(parent).find('.overdue-panel').addClass(colorMap.get(data.overduePriority).color);
            $(parent).find('.overDueCount').addClass(colorMap.get(data.overduePriority).textColor);


            $(parent).find(".unplannedBirthCount").text(data.unplannedBerthCount);
            $(parent).find('.unplanned-panel').addClass(colorMap.get(data.unplannedBerthPriority).color);
            $(parent).find('.unplannedBirthCount').addClass(colorMap.get(data.unplannedBerthPriority).textColor);


            $(parent).find(".medicalCount").text(data.medicalSignOffCount);
            $(parent).find('.medicalSignOff-panel').addClass(colorMap.get(data.medicalSignOffPriority).color);
            $(parent).find('.medicalCount').addClass(colorMap.get(data.medicalSignOffPriority).textColor);

            if (data.crewChangeCount != 'undefined' || data.crewChangeCount != null) {
                $(parent).find(".crewChangesCount").text(data.crewChangeCount);
            }
            $(parent).find('.crewChanges-panel').addClass(colorMap.get(data.crewChangePriority).color);
            $(parent).find('.crewChangesCount').addClass(colorMap.get(data.crewChangePriority).textColor);


            //settig up url			
            var viewMoreNav = "/Crew/List/?CrewList=" + data.viewMoreURL + "&VesselId=" + vesselId;
            $(parent).find('.crewViewMore').attr("href", viewMoreNav);

            var overDueNav = "/Crew/List/?CrewList=" + data.overdueURL + "&VesselId=" + vesselId;
            var unPlannedBerth = "/Crew/List/?CrewList=" + data.unplannedBerthURL + "&VesselId=" + vesselId;
            var crewChangeNav = "/Crew/List/?CrewList=" + data.crewChangeUrl + "&VesselId=" + vesselId;
            var medicalSignOffNav = "/Crew/MedicalSignOffList/?CrewList=" + data.medicalSignOffURL + "&VesselId=" + vesselId;

            $(parent).find(".overDueUrl").attr("href", overDueNav);
            $(parent).find(".unplannedBirthUrl").attr("href", unPlannedBerth);
            $(parent).find(".medicalUrl").attr("href", medicalSignOffNav);
            $(parent).find(".crewChangesUrl").attr("href", crewChangeNav);

        },
        complete: function () {
            $(parent).find('.crew-panel').unblock();
        }
    });
}

function BindDefectManager(vesselId, parent) {
    var date = new Date();
    var start = 1 + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
    var end = moment().format("DD MMM YYYY");

    var request =
    {
        "EncryptedVesselId": vesselId,
        "ToDate": end,
        "FromDate": start
    }
    $.ajax({
        url: "/Dashboard/GetDefectDashboardDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.defect-panel').block({
                message: $(" " + loadercontent),
            })
        },

        success: function (data) {
            if (data != null) {
                $(parent).find('.dueDefectsCount').text(data.openDefectCount);
                $(parent).find('.dueDefectsCount-panel').addClass(colorMap.get(data.openDefectPriority).color);
                $(parent).find('.dueDefectsCount').addClass(colorMap.get(data.openDefectPriority).textColor);

                $(parent).find('.overDueDefectsCount').text(data.overdueCount);
                $(parent).find('.overDueDefectsCount-panel').addClass(colorMap.get(data.overduePriority).color);
                $(parent).find('.overDueDefectsCount').addClass(colorMap.get(data.overduePriority).textColor);

                $(parent).find('.offHireRequiredCount').text(data.offHireCount);
                $(parent).find('.offHireRequiredCount-panel').addClass(colorMap.get(data.offHirePriority).color);
                $(parent).find('.offHireRequiredCount').addClass(colorMap.get(data.offHirePriority).textColor);

                $(parent).find('.awaitingSparesCount').text(data.awaitingSparesCount);
                $(parent).find('.awaitingSparesCount-panel').addClass(colorMap.get(data.awaitingSparesPriority).color);
                $(parent).find('.awaitingSparesCount').addClass(colorMap.get(data.awaitingSparesPriority).textColor);

                //setting up url
                var viewMoreNav = "/Defect/List/?DefectRequest=" + data.openDefectNavigation + "&VesselId=" + vesselId;
                $(parent).find('.defectViewMore').attr("href", viewMoreNav);

                var dueNavigation = "/Defect/List/?DefectRequest=" + data.openDefectNavigation + "&VesselId=" + vesselId;
                $(parent).find(".dueDefectsUrl").attr("href", dueNavigation);

                var overdueNavigation = "/Defect/List/?DefectRequest=" + data.overdueDefectNavigation + "&VesselId=" + vesselId;
                $(parent).find(".overDueDefectsUrl").attr("href", overdueNavigation);

                var offHireNavigation = "/Defect/List/?DefectRequest=" + data.offHireDefectNavigation + "&VesselId=" + vesselId;
                $(parent).find(".offHireRequiredUrl").attr("href", offHireNavigation);

                var awaitingSparesNavigation = "/Defect/List/?DefectRequest=" + data.awaitingSparesNavigation + "&VesselId=" + vesselId;
                $(parent).find(".awaitingSparesUrl").attr("href", awaitingSparesNavigation);

            }
        },
        complete: function () {
            $(parent).find('.defect-panel').unblock();
        }
    });
}

function BindPurchaseOrderSummary(vesselId, parent) {
    //date is not used in api	
    var postartDate = moment().subtract(6, "month");
    var poendDate = moment();
    var localStartDate = postartDate.format("DD MMM YYYY");
    var localEndDate = poendDate.format("DD MMM YYYY");

    var request =
    {
        "vesselId": vesselId,
        "orderToDate": localEndDate,
        "orderFromDate": localStartDate
    }
    $.ajax({
        url: "/Dashboard/GetOrderSummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.order-panel').block({
                message: $(" " + loadercontent),
            })
        },

        success: function (data) {
            $(parent).find('.requestedCount').text(data.requisitionCount);
            $(parent).find('.orderedCount').text(data.orderedCount);
            $(parent).find('.awaitingSnrCount').text(data.authRequiredCount);
            $(parent).find('.awaitingCount').text(data.awaitingAuthorisationCount);

            var requisitionsURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.requisitionsURL + "&VesselId=" + vesselId;
            $(parent).find('.requestedUrl').attr("href", requisitionsURL);

            var overViewURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.overViewURL + "&VesselId=" + vesselId;
            $(parent).find('.orderViewMore').attr("href", overViewURL);

            var OrderedURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.orderURL + "&VesselId=" + vesselId;
            $(parent).find(".orderedUrl").attr("href", OrderedURL);

            var authRequiredUrl = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.authRequiredURL + "&VesselId=" + vesselId;
            $(parent).find('.awaitingSnrUrl').attr("href", authRequiredUrl);

            var tenderAwaitingAuthURL = "/PurchaseOrder/List/?purchaseOrderRequest=" + data.tenderAwaitingAuthURL + "&VesselId=" + vesselId;
            $(parent).find('.awaitingUrl').attr("href", tenderAwaitingAuthURL);

            //color
            $(parent).find('.requested-panel').addClass(colorMap.get(data.requisitionPriority).color);
            $(parent).find('.requestedCount').addClass(colorMap.get(data.requisitionPriority).textColor);

            $(parent).find('.ordered-panel').addClass(colorMap.get(data.orderedPriority).color);
            $(parent).find('.orderedCount').addClass(colorMap.get(data.orderedPriority).textColor);

            $(parent).find('.awaitingSnrAuth-panel').addClass(colorMap.get(data.awaitingSnrAuthPriority).color);
            $(parent).find('.awaitingSnrCount').addClass(colorMap.get(data.awaitingSnrAuthPriority).textColor);

            $(parent).find('.tenderAwaitingAuth-panel').addClass(colorMap.get(data.tenderAwaitingAuthPriority).color);
            $(parent).find('.awaitingCount').addClass(colorMap.get(data.tenderAwaitingAuthPriority).textColor);
        },
        complete: function () {
            $(parent).find('.order-panel').unblock();
        }
    });
}

function BindOpexSummary(vesselId, parent) {
    var request =
    {
        "VesselId": vesselId,
    }

    $.ajax({
        url: "/Dashboard/GetOpexDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.Opex-panel').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {
            $(parent).find('.budgetYTDCount').text(data.budgetStr);
            $(parent).find('.budget-panel').addClass(colorMap.get(data.budgetKPIPriority).color);
            $(parent).find('.budgetYTDCount').addClass(colorMap.get(data.budgetKPIPriority).textColor);

            $(parent).find('.actualSpendCount').text(data.actualStr);
            $(parent).find('.actual-panel').addClass(colorMap.get(data.actualKPIPriority).color);
            $(parent).find('.actualSpendCount').addClass(colorMap.get(data.actualKPIPriority).textColor);

            $(parent).find('.accrualsCount').text(data.accuralsStr);
            $(parent).find('.accrual-panel').addClass(colorMap.get(data.accrualKPIPriority).color);
            $(parent).find('.accrualsCount').addClass(colorMap.get(data.accrualKPIPriority).textColor);

            $(parent).find('.varianceCount').text(data.variancePercent);
            $(parent).find('.variance-panel').addClass(colorMap.get(data.varianceKPIPriority).color);
            $(parent).find('.varianceCount').addClass(colorMap.get(data.varianceKPIPriority).textColor);

            var OpexURL = "/Finance/List/?OperationCostRequestUrl=" + data.opexDashboardUrl + "&VesselId=" + vesselId;
            $(parent).find('.financeViewMore').attr("href", OpexURL);
            $(parent).find('.varianceURL').attr("href", OpexURL);
            $(parent).find('.actualSpendUrl').attr("href", OpexURL);
            $(parent).find('.accrualsUrl').attr("href", OpexURL);
            $(parent).find('.budgetYTDUrl').attr("href", OpexURL);

        },
        complete: function () {
            $(parent).find('.Opex-panel').unblock();
        }
    });
}
function BindInspectionSummary(vesselId, parent) {
    var request =
    {
        "EncryptedVesselId": vesselId,

        "ToDate": moment().format("DD MMM YYYY"),
        "FromDate": moment().subtract(6, "month").format("DD MMM YYYY"),

        "DeficienciesPerPSCToDate": moment().format("DD MMM YYYY"),
        "DeficienciesPerPSCFromDate": moment().subtract(6, "month").format("DD MMM YYYY"),

        "PSCDetentionToDate": moment().format("DD MMM YYYY"),
        "PSCDetentionFromDate": moment().subtract(3, "month").format("DD MMM YYYY"),

        "PscDeficiencyToDate": moment().format("DD MMM YYYY"),
        "PscDeficiencyFromDate": moment().subtract(3, "month").format("DD MMM YYYY"),

        "OmvRejectionToDate": moment().format("DD MMM YYYY"),
        "OmvRejectionFromDate": moment().subtract(3, "month").format("DD MMM YYYY"),

        "DeficienciesPerPscPriorityHighLimit": 3,
        "DeficienciesPerPscPriorityMidLimit": 2.99,
        "DeficienciesPerPscPriorityLowLimit": 1,

        "DeficienciesPerOmvPriorityHighLimit": 3,
        "DeficienciesPerOmvPriorityMidLimit": 2.99,
        "DeficienciesPerOmvPriorityLowLimit": 1,

        "OverdueFindingsPriorityLimit": 0,
        "OverdueInspectionsPriorityLimit": 0,

        "IsFromDashboard": true
    }

    $.ajax({
        url: "/Dashboard/GetInspectionManagerSummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.inspection-panel').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {
            if (data != null) {

                $(parent).find(".deficienciesPerOmvCount").text(data.omvDefectRate);
                $(parent).find('.deficienciesPerOmv-panel').addClass(colorMap.get(data.deficienciesPerOMVPriority).color);
                $(parent).find('.deficienciesPerOmvCount').addClass(colorMap.get(data.deficienciesPerOMVPriority).textColor);

                $(parent).find(".deficienciesPerPscCount").text(data.pscDefectRate);
                $(parent).find('.deficienciesPerPsc-panel').addClass(colorMap.get(data.deficienciesPerPSCPriority).color);
                $(parent).find('.deficienciesPerPscCount').addClass(colorMap.get(data.deficienciesPerPSCPriority).textColor);

                $(parent).find(".overdueFindingsCount").text(data.totalOverdueFindingCount);
                $(parent).find('.overdueFindings-panel').addClass(colorMap.get(data.overdueFindingsPriority).color);
                $(parent).find('.overdueFindingsCount').addClass(colorMap.get(data.overdueFindingsPriority).textColor);

                $(parent).find(".overdueInspectionCount").text(data.inspectionOverdueCount);
                $(parent).find('.overdueInspection-panel').addClass(colorMap.get(data.overdueInspectionPriority).color);
                $(parent).find('.overdueInspectionCount').addClass(colorMap.get(data.overdueInspectionPriority).textColor);

                $(parent).find(".dashboardInspectionPlanningDetils").attr('data-vesselid', data.vesselId);
                $(parent).find(".dashboardInspectionPlanningDetils").attr('data-vesselname', encodeURIComponent(data.vesselName));

                $(parent).find(".pscDeficenCount").text(data.totalPSCFindingCount);
                $(parent).find('.pscDeficen-panel').addClass(colorMap.get(data.pscDeficenPriority).color);
                $(parent).find('.pscDeficenCount').addClass(colorMap.get(data.pscDeficenPriority).textColor);

                $(parent).find(".pscDetCount").text(data.pscDetaintionCount);
                $(parent).find('.pscDetCount').addClass(colorMap.get(data.pscDetentionPriority).textColor);

                $(parent).find(".omvRejCount").text(data.omvRejCount);
                $(parent).find('.omvRejCount').addClass(colorMap.get(data.omvRejPriority).textColor);

                if (data.omvRejPriority == 2 || data.pscDetentionPriority == 2)
                    $(parent).find('.pscomv-panel').addClass(colorMap.get(2).color); // Red
                else
                    $(parent).find('.pscomv-panel').addClass(colorMap.get(0).color); // Green

                //settig up url			
                var viewMoreNav = "/Inspection/List/?Inspection=" + data.overviewInspectionUrl + "&VesselId=" + vesselId + "&IsViewMore=true";
                var overdueFindingsNav = "/Inspection/List/?Inspection=" + data.overdueFindingsUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
                var pscDetNav = "/Inspection/List/?Inspection=" + data.pscDetentionUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
                var pscDeficiencyNav = "/Inspection/List/?Inspection=" + data.pscDeficiencyUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
                var omvRejectionNav = "/Inspection/List/?Inspection=" + data.omvRejectionUrl + "&VesselId=" + vesselId + "&IsViewMore=false";
                var deficienciesPerOmvNav = "/Inspection/List/?Inspection=" + data.deficienciesPerOmvURL + "&VesselId=" + vesselId + "&IsViewMore=false";
                var deficienciesPerPscNav = "/Inspection/List/?Inspection=" + data.deficienciesPerPscURL + "&VesselId=" + vesselId + "&IsViewMore=false";

                if (data.omvRejCount == 'N/A') {
                    $(parent).find('.omvRejCount').addClass('inspection-na');
                    $(parent).find('.omvRejUrl').attr("href", 'javascript: void(0);');
                }
                else {
                    $(parent).find('.omvRejCount').removeClass('inspection-na');
                    $(parent).find('.omvRejUrl').attr("href", omvRejectionNav);
                }

                if (data.omvDefectRate == 'N/A') {
                    $(parent).find('.deficienciesPerOmvCount').addClass('inspection-na');
                    $(parent).find('.deficienciesPerOmvUrl').attr("href", 'javascript: void(0);');
                }
                else {
                    $(parent).find('.deficienciesPerOmvCount').removeClass('inspection-na');
                    $(parent).find('.deficienciesPerOmvUrl').attr("href", deficienciesPerOmvNav);
                }

                $(parent).find('.overdueInspectionUrl').attr("href", 'javascript: void(0);');
                $(parent).find('.deficienciesPerPscUrl').attr("href", deficienciesPerPscNav);

                $(parent).find('.overdueFindingsUrl').attr("href", overdueFindingsNav);
                $(parent).find('.pscDetUrl').attr("href", pscDetNav);
                $(parent).find('.pscDeficenUrl').attr("href", pscDeficiencyNav);
                $(parent).find('.inspectionViewMore').attr("href", viewMoreNav);
            }
        },
        complete: function () {
            $(parent).find('.inspection-panel').unblock();
        }
    });
}

function BindCertificateSummary(vesselId, parent) {
    var request =
    {
        "vesselId": vesselId
    }
    $.ajax({
        url: "/Dashboard/GetCertificateDashboardDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "input": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.certificates-panel').block({
                message: $(" " + loadercontent),
            })
        },

        success: function (data) {
            if (data != null && data != 'undefined') {
                $(parent).find('.overdueCount').text(data.overDueCertificateCount);
                $(parent).find('.expiringWithin30Count').text(data.expires30DaysCertificateCount);
                $(parent).find('.withinSurveyRangeCount').text(data.surveyRangeCertificateCount);
                $(parent).find('.stopSailingExpiring30Count').text(data.stopSailingTradingExpiringIn30DaysCount);

                //color
                $(parent).find('.overdueCertificate-panel').addClass(colorMap.get(data.overDueCertificatePriority).color);
                $(parent).find('.overdueCount').addClass(colorMap.get(data.overDueCertificatePriority).textColor);

                $(parent).find('.expiringWithin-panel').addClass(colorMap.get(data.expiringXDaysCertificatePriority).color);
                $(parent).find('.expiringWithin30Count').addClass(colorMap.get(data.expiringXDaysCertificatePriority).textColor);

                $(parent).find('.withingSurvey-panel').addClass(colorMap.get(data.surveyRangeCertificatePriority).color);
                $(parent).find('.withinSurveyRangeCount').addClass(colorMap.get(data.surveyRangeCertificatePriority).textColor);

                $(parent).find('.stopSailingExpiring30-panel').addClass(colorMap.get(data.stopSailingTradingExpiringIn30DaysKPI).color);
                $(parent).find('.stopSailingExpiring30Count').addClass(colorMap.get(data.stopSailingTradingExpiringIn30DaysKPI).textColor);

                //navigation
                var certificateBaseURL = "/Certificate/List?VesselId=" + vesselId + "&CertificateRequestUrl=";

                $(parent).find(".overdueUrl").attr("href", certificateBaseURL + data.overDueCertificateCountURL + "&IsViewMore=false");

                $(parent).find(".expiringWithin30Url").attr("href", certificateBaseURL + data.expires30DaysCertificateCountURL + "&IsViewMore=false");

                $(parent).find(".withinSurveyRangeUrl").attr("href", certificateBaseURL + data.surveyRangeCertificateCountURL + "&IsViewMore=false");

                $(parent).find(".stopSailingExpiring30Url").attr("href", certificateBaseURL + data.stopSailingTradingExpiringIn30DaysUrl + "&IsViewMore=false");

                $(parent).find('.certificateViewMore').attr("href", certificateBaseURL + data.allActiveCertificateCountURL + "&IsViewMore=true");
            }
        },
        complete: function () {
            $(parent).find('.certificates-panel').unblock();
        }
    });
}

function BindPMSSummary(vesselId, parent) {

    var request =
    {
        "VesselId": vesselId
    }
    $.ajax({
        url: "/Dashboard/PMSDashboardSummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.pms-panel').block({
                message: $(" " + loadercontent),
            })
        },

        success: function (data) {
            if (data != null) {
                $(parent).find('.pmsCriticalOverdueCount').text(data.criticalOverdue);
                $(parent).find('.pms-critical-overdue-panel').addClass(colorMap.get(data.criticalOverduePriority).color);
                $(parent).find('.pmsCriticalOverdueCount').addClass(colorMap.get(data.criticalOverduePriority).textColor);

                $(parent).find('.pmsCriticalDueCount').text(data.criticalDue);
                $(parent).find('.pms-critical-due-panel').addClass(colorMap.get(data.criticalDuePriority).color);
                $(parent).find('.pmsCriticalDueCount').addClass(colorMap.get(data.criticalDuePriority).textColor);

                $(parent).find('.pmsOverdueCount').text(data.overdue);
                $(parent).find('.pms-overdue-panel').addClass(colorMap.get(data.overduePriority).color);
                $(parent).find('.pmsOverdueCount').addClass(colorMap.get(data.overduePriority).textColor);

                $(parent).find('.pmsDueCount').text(data.due);
                $(parent).find('.pms-due-panel').addClass(colorMap.get(data.duePriority).color);
                $(parent).find('.pmsDueCount').addClass(colorMap.get(data.duePriority).textColor);

                var viewMoreURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.allRequestURL + "&VesselId=" + vesselId;
                $(parent).find('.pmsViewMore').attr("href", viewMoreURL);

                var dueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.dueURL + "&VesselId=" + vesselId;
                $(parent).find('.pmsDueURL').attr("href", dueURL);

                var criticalDueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalDueURL + "&VesselId=" + vesselId;
                $(parent).find('.pmsCriticalDueURL').attr("href", criticalDueURL);

                var overdueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.overdueURL + "&VesselId=" + vesselId;
                $(parent).find('.pmsOverdueURL').attr("href", overdueURL);

                var criticalOverdueURL = "/PlannedMaintenance/List/?PlannedMaintenance=" + data.criticalOverdueURL + "&VesselId=" + vesselId;
                $(parent).find('.pmsCriticalOverdueURL').attr("href", criticalOverdueURL);

            }
        },
        complete: function () {
            $(parent).find('.pms-panel').unblock();
        }
    });
}

function BindHazoccSummary(vesselId, parent) {

    var request =
    {
        "VesselId": vesselId,
        "StartDate": moment().subtract(12, 'months').add(1, 'days').format('DD MMM YYYY'),
        "EndDate": moment().endOf('day').format('DD MMM YYYY HH:mm:ss')
    }
    $.ajax({
        url: "/Dashboard/HazOccSummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": request
        },
        beforeSend: function (xhr) {
            $(parent).find('.hazoc-panel').block({
                message: $(" " + loadercontent),
            })
        },

        success: function (data) {
            if (data != null) {
                $(parent).find('.seriousAccidentsCount').text(data.seriousAccidentCount);
                $(parent).find('.seriousAccidentsPanel').addClass(colorMap.get(data.seriousAccidentPriority).color);
                $(parent).find('.seriousAccidentsCount').addClass(colorMap.get(data.seriousAccidentPriority).textColor);

                $(parent).find('.seriousIncidentsCount').text(data.seriousIncidentCount);
                $(parent).find('.seiousIncidentsPanel').addClass(colorMap.get(data.seriousIncidentPriority).color);
                $(parent).find('.seriousIncidentsCount').addClass(colorMap.get(data.seriousIncidentPriority).textColor);

                $(parent).find('.ltiFreeDaysCount').text(data.ltiFreeDaysCount);
                $(parent).find('.ltiFreeDaysPanel').addClass(colorMap.get(data.ltiFreeDaysPriority).color);
                $(parent).find('.ltiFreeDaysCount').addClass(colorMap.get(data.ltiFreeDaysPriority).textColor);

                $(parent).find('.unsafeCount').text(ConvertDecimalNumberToString(data.unsafeRate, 2, 1, 2) );
                $(parent).find('.unsafePanel').addClass(colorMap.get(data.unsafePriority).color);
                $(parent).find('.unsafeCount').addClass(colorMap.get(data.unsafePriority).textColor);

                let viewMoreNav = '/HazOcc/List?request=' + data.hazOccListRequestUrl + '&VesselId=' + vesselId;
                $(parent).find('.hazocViewMore').attr("href", viewMoreNav);

                let seriousAccidentURL = '/HazOcc/List?request=' + data.seriousAccidentURL + '&VesselId=' + vesselId;
                $(parent).find('.seriousAccidentsUrl').attr("href", seriousAccidentURL);

                let seriousIncidentURL = '/HazOcc/List?request=' + data.seriousIncidentURL + '&VesselId=' + vesselId;
                $(parent).find('.seriousIncidentsUrl').attr("href", seriousIncidentURL);

                let uaucRateURL = '/HazOcc/List?request=' + data.unsafeActAndUnsafeConditionURL + '&VesselId=' + vesselId;
                $(parent).find('.unsafeUrl').attr("href", uaucRateURL);
            }
        },
        complete: function () {
            $(parent).find('.hazoc-panel').unblock();
        }
    });
}

function NumberToString(number, toFixedValue, state) {
    var numToString = "";
    if (number != null && number != '' && number != 'undefined') {
        numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
            {
                minimumFractionDigits: 0
            });
    }
    else {
        if (state == 1) {
            //when expected return is 0.00
            numToString = "0";
        }
        else if (state == 2) {
            //when blank space is expected
            numToString = "";
        }
    }
    return numToString;
}

function BindCommercialSummary(vesselId, parent) {
    $.ajax({
        url: "/Dashboard/GetCommercialSummary",
        type: "POST",
        "data": {
            "input": vesselId,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            $(parent).find('.commercial-panel').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {

            $(parent).find(".unplannedOffHireHrs").text(data.unplannedOffHireHrs);
            $(parent).find('.unplannedOffHireHrs-panel').addClass(colorMap.get(data.unplannedOffHireHrsPriority).color);
            $(parent).find('.unplannedOffHireHrs').addClass(colorMap.get(data.unplannedOffHireHrsPriority).textColor);

            $(parent).find(".fuelUnderPerformance").text(data.fuelUnderPerformance);
            $(parent).find('.fuelUnderPerformance-panel').addClass(colorMap.get(data.fuelUnderPerformancePriority).color);
            $(parent).find('.fuelUnderPerformance').addClass(colorMap.get(data.fuelUnderPerformancePriority).textColor);

            $(parent).find(".speedUnderPerformance").text(data.speedUnderPerformance);
            $(parent).find('.speedUnderPerformance-panel').addClass(colorMap.get(data.speedUnderPerformancePriority).color);
            $(parent).find('.speedUnderPerformance').addClass(colorMap.get(data.speedUnderPerformancePriority).textColor);

            $(parent).find(".predictedBadWeather").text(data.predictedBadWeather);
            $(parent).find('.predictedBadWeather-panel').addClass(colorMap.get(data.predictedBadWeatherPriority).color);
            $(parent).find('.predictedBadWeather').addClass(colorMap.get(data.predictedBadWeatherPriority).textColor);
            $(parent).find(".PredictedBadWeatherCls").attr('data-vesselid', data.vesselId);
            $(parent).find(".PredictedBadWeatherCls").attr('data-vesselname', encodeURIComponent(data.vesselName));
            $(parent).find(".PredictedBadWeatherCls").attr('data-predictedbadweather', data.predictedBadWeather);

            //settig up url			
            var viewMoreNav = "/VoyageReporting/VesselActivityList/?VoyageReportingRequestUrl=" + data.viewMoreURL + "&VesselId=" + vesselId + "&isFromDashboard=true";
            $(parent).find('.commercialViewMoreUrl').attr("href", viewMoreNav);
            $(parent).find(".unplannedOffHireHrsUrl").attr("href", viewMoreNav);
            $(parent).find(".fuelUnderPerformanceUrl").attr("href", viewMoreNav);
            $(parent).find(".speedUnderPerformanceUrl").attr("href", viewMoreNav);
        },
        complete: function () {
            $(parent).find('.commercial-panel').unblock();
        }
    });
}

function BindEnvironmentSummary(vesselId, parent) {

    var request =
    {
        "VesselId": vesselId,
        "StartDate": moment().subtract(30, "day").format("DD MMM YYYY"),
        "EndDate": moment().format("DD MMM YYYY"),
        "OilSpillStartDate": moment().subtract(3, "month").format("DD MMM YYYY"),
        "OilSpillEndDate": moment().format("DD MMM YYYY"),
        "BilgeStartDate": moment().subtract(1, "year").format("DD MMM YYYY"),
        "BilgeEndDate": moment().format("DD MMM YYYY"),
        "AEUtilisationStartDate": moment().subtract(1, "year").format("DD MMM YYYY"),
        "AEUtilisationEndDate": moment().format("DD MMM YYYY")
    }

    $.ajax({
        url: "/Dashboard/GetEnvironmentSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            $(parent).find('.environment-panel').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {
            $(parent).find(".EEOICount").text(data.eeoi);
            $(parent).find('.EEOI-panel').addClass(colorMap.get(0).color);
            $(parent).find('.EEOICount').addClass(colorMap.get(0).textColor);

            $(parent).find(".accidentalSpillsCount").text(data.accidentalOilSpillsCount);
            $(parent).find('.accidentalSpills-panel').addClass(colorMap.get(0).color);
            $(parent).find('.accidentalSpillsCount').addClass(colorMap.get(0).textColor);

            $(parent).find(".oilBilgeRetentionCount").text(data.oilBilgeRetention + "%");
            $(parent).find('.oilBilgeRetention-panel').addClass(colorMap.get(0).color);
            $(parent).find('.oilBilgeRetentionCount').addClass(colorMap.get(0).textColor);

            $(parent).find(".aeUtilisationCount").text(data.aeUtilisation);
            $(parent).find('.aeUtilisation-panel').addClass(colorMap.get(0).color);
            $(parent).find('.aeUtilisationCount').addClass(colorMap.get(0).textColor);
        },
        complete: function () {
            $(parent).find('.environment-panel').unblock();
        }
    });
}

function BindJSASummary(vesselId, parent) {
    $.ajax({
        url: "/Dashboard/GetJSASummary",
        type: "POST",
        dataType: "JSON",
        data: {
            "vesselId": vesselId
        },
        beforeSend: function (xhr) {
            $(parent).find('.jsa-panel').block({
                message: $(" " + loadercontent),
            })
        },

        success: function (data) {
            if (data != null) {

                //Navigation
                var viewMoreURL = "/JSA/List/?JsaRequest=" + data.totalUrl + "&VesselId=" + vesselId;
                $(parent).find('.jsaViewMore').attr("href", viewMoreURL);

                var highRiskUrl = "/JSA/List/?JsaRequest=" + data.midHighUrl + "&VesselId=" + vesselId;
                $(parent).find('.jsaMidHighRiskUrl').attr("href", highRiskUrl);


                $(parent).find('.jsaOpenReportURL').attr("href", viewMoreURL);

                var overdueUrl = "/JSA/List/?JsaRequest=" + data.overdueForClosureUrl + "&VesselId=" + vesselId;
                $(parent).find('.jsaOverdueForClosureUrl').attr("href", overdueUrl);

                var pendOffApprUrl = "/JSA/List/?JsaRequest=" + data.pendingOfficeApprovalUrl + "&VesselId=" + vesselId;
                $(parent).find('.jsaPendOffAppUrl').attr("href", pendOffApprUrl);

                //Count
                $(parent).find(".jsaMidHighRiskCount").text(data.residualRiskMediumAndHighCount);
                $(parent).find(".jsaOpenCount").text(data.totalCount);
                $(parent).find(".jsaOverdueClosureCount").text(data.overdueForClosureCount);
                $(parent).find(".jsaPendingOffAppCount").text(data.pendingOfficeApprovalCount);

                //Priority 
                $(parent).find('.jsaMedHighPanel').addClass(colorMap.get(data.residualRiskMediumAndHighPriority).color);
                $(parent).find('.jsaMidHighRiskCount').addClass(colorMap.get(data.residualRiskMediumAndHighPriority).textColor);

                $(parent).find('.jsaOpenPanel').addClass(colorMap.get(data.openPriority).color);
                $(parent).find('.jsaOpenCount').addClass(colorMap.get(data.openPriority).textColor);

                $(parent).find('.jsaOverduePanel').addClass(colorMap.get(data.overdueForClosurePriority).color);
                $(parent).find('.jsaOverdueClosureCount').addClass(colorMap.get(data.overdueForClosurePriority).textColor);

                $(parent).find('.jsaPendingOfficeApprovalPanel').addClass(colorMap.get(data.pendingOfficeApprovalPriority).color);
                $(parent).find('.jsaPendingOffAppCount').addClass(colorMap.get(data.pendingOfficeApprovalPriority).textColor);

            }
        },
        complete: function () {
            $(parent).find('.jsa-panel').unblock();
        }
    });

}

var dashboardFleetSummaryLoader = true;
function BlockFleetSummaryLoader() {
    if (dashboardFleetSummaryLoader) {
        $('.fleet-panel-loader').block({
            message: $(" " + loadercontent),
        })
        colorMap.forEach(function callbackFn(value, key, map) {
            $('.fleet-text').removeClass(map.get(key).textColor);
            $('.fleet-panel').removeClass(map.get(key).color);
        });
    }
    dashboardFleetSummaryLoader = false;
}

var crewSuccess, opexSuccess, inspectionSuccess, hazOccSuccess, commercialSuccess, rightshipSuccess, pmsSuccess
function UnBlockFleetSummaryLoader() {
    if (crewSuccess && opexSuccess && inspectionSuccess && hazOccSuccess && commercialSuccess && rightshipSuccess && pmsSuccess) {
        $('.fleet-panel-loader').unblock();
        if (screen.width > 767) {
            $('.fleet-panel').matchHeight({
                byRow: 0
            });
            $('.height-equal').matchHeight({
                byRow: 0
            });
        }
        SetPurchaseOrderScrollerHeight($('#dashboard-section-2'));
        crewSuccess = false; opexSuccess = false; inspectionSuccess = false; hazOccSuccess = false; commercialSuccess = false; rightshipSuccess = false; pmsSuccess = false;
    }
}

function BindCrewFleetSummary(fltId, typeMenu, vesId, title) {
    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,
    }

    $.ajax({
        url: "/Dashboard/GetCrewFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader()
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-experienceMatrixCount').text(data.experienceMatrixVesselCount);
                $('.fleet-experienceMatrixCount').addClass(colorMap.get(data.experienceMatrixPriority).textColor);
                $('.fleet-experienceMatrix-panel').addClass(colorMap.get(data.experienceMatrixPriority).color);
                $("#hdnExperienceMatrixCount").val(data.experienceMatrixVesselCount);
                $("#hdnExperienceMatrixPriority").val(data.experienceMatrixPriority);
                SetTooltip('#imgExperienceMatrixTooltip', data.experienceMatrixInfo);
            }
        },
        complete: function () {
            crewSuccess = true;
            UnBlockFleetSummaryLoader();
        }
    });
}

function BindOpexFleetSummary(fltId, typeMenu, vesId, title) {

    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,

        "OpexToDate": moment().subtract(2, "month").endOf('month').format("DD MMM YYYY"),
        "BudgetDays": 90,
        "BudgetPercentageHighLimit": 10.00,
        "BudgetPercentageLowLimit": 5.00,
    }

    $.ajax({
        url: "/Dashboard/GetOpexFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader();
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-overBudgetCount').text(data.opexOverBudgetPercentage);
                $('.fleet-overBudgetCount').addClass(colorMap.get(data.opexOverBudgetPriority).textColor);
                $('.fleet-overBudget-panel').addClass(colorMap.get(data.opexOverBudgetPriority).color);
                $("#hdnOverBudgetCount").val(data.opexOverBudgetPercentage);
                $("#hdnOverBudgetPriority").val(data.opexOverBudgetPriority);
                $("#hdnOverBudgetToDate").val(data.opexOverBudgetToDate);
                $("#hOpexHeader").text('Opex ' + data.opexOverBudgetToDate);
                SetTooltip('#imgOpexOverBudgetTooltip', data.opexOverBudgetInfo);
            }
        },
        complete: function () {
            opexSuccess = true;
            UnBlockFleetSummaryLoader()
        }
    });
}

function BindInspectionFleetSummary(fltId, typeMenu, vesId, title) {
    var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
    var endDate = moment().format("DD MMM YYYY");

    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,

        "PSCDetentionFromDate": startDate,
        "PSCDetentionToDate": endDate,
        "PSCDetentionPriorityLimit": 0,

        "PSCDeficiencyFromDate": startDate,
        "PSCDeficiencyToDate": endDate,
        "PSCDeficiencyPriorityLimit": 0.88,

        "OMVFindingsFromdate": moment().subtract(6, "month").format("DD MMM YYYY"),
        "OMVFindingsToDate": endDate,
        "OMVFindingsPriorityLowLimit": 2.7,
        "OMVFindingsPriorityHighLimit": 2.8,

        "OverdueInspectionsPriorityLimit": 0,
    }

    $.ajax({
        url: "/Dashboard/GetInspectionFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader();
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-pscDetentionsCount').text(data.pscDetentionCount);
                $('.fleet-pscDetentionsCount').addClass(colorMap.get(data.pscDetentionPriority).textColor);
                $('.fleet-pscDetentions-panel').addClass(colorMap.get(data.pscDetentionPriority).color).addClass('fleet-pscDetentions-panel');
                $("#pscDetentionCountId").val(data.pscDetentionCount);
                $("#hdnPscDetentionPriority").val(data.pscDetentionPriority);
                SetTooltip('#imgPscDetentionTooltip', data.pscDetentionInfo);

                $('.fleet-pscDeficienciesCount').text(data.pscDeficiencyRate);
                $('.fleet-pscDeficienciesCount').addClass(colorMap.get(data.pscDeficiencyPriority).textColor);
                $('.fleet-pscDeficiencies-panel').addClass(colorMap.get(data.pscDeficiencyPriority).color);
                $("#hdnpscDeficienciesCount").val(data.pscDeficiencyCount);
                $("#hdnPscDeficienciesPriority").val(data.pscDeficiencyPriority);
                $("#hdnPscDeficiencyRate").val(data.pscDeficiencyRate);
                $("#hdnPscDeficiencyInspectionCount").val(data.pscDeficiencyInspectionCount);
                SetTooltip('#imgPscDeficiencyTooltip', data.pscDeficiencyInfo);

                $('.fleet-omvfindingCount').text(data.omvFindingsRate);
                $('.fleet-omvfindingCount').addClass(colorMap.get(data.omvFindingsPriority).textColor);
                $('.fleet-omvfinding-panel').addClass(colorMap.get(data.omvFindingsPriority).color);
                $("#hdnOmvFindingRate").val(data.omvFindingsRate);
                $("#hdnOmvFindingsCount").val(data.omvFindingsCount);
                $("#hdnOmvInspectionsCount").val(data.omvInspectionsCount);
                $("#hdnOmvFindingPriority").val(data.omvFindingsPriority);
                SetTooltip('#imgOmvFindingsTooltip', data.omvFindingsInfo);

                $('.fleet-overdueInspectionCount').text(data.overdueInspectionCount);
                $('.fleet-overdueInspectionCount').addClass(colorMap.get(data.overdueInspectionPriority).textColor);
                $('.fleet-overdueInspection-panel').addClass(colorMap.get(data.overdueInspectionPriority).color);
                $("#hdnOverdueInspection").val(data.overdueInspectionCount);
                $("#hdnOverdueInspectionPriority").val(data.overdueInspectionPriority);
                SetTooltip('#imgOverdueInspectionsTooltip', data.overdueInspectionInfo);
            }

        },
        complete: function () {
            inspectionSuccess = true;
            UnBlockFleetSummaryLoader();
        }
    });
}

function BindHazOccFleetSummary(fltId, typeMenu, vesId, title) {
    var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
    var endDate = moment().format("DD MMM YYYY");
    var LTIStartDate = moment().subtract(12, "month").format("DD MMM YYYY");
    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,

        "IncidentStartDate": startDate,
        "IncidentEndDate": endDate,
        "SeriousIncidentsPriority": 0,

        "LtiFromDate": LTIStartDate,
        "LtiToDate": endDate,
        "LtifPriority": 7,

        "OilSpillFromDate": startDate,
        "OilSpillToDate": endDate,
        "OilSpillPriorityLimit": 0
    }

    $.ajax({
        url: "/Dashboard/GetHazOccFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader();
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-seriousIncidentsCount').text(data.seriousIncidentsCount);
                $('.fleet-seriousIncidentsCount').addClass(colorMap.get(data.seriousIncidentsPriority).textColor);
                $('.fleet-seriousIncidents-panel').addClass(colorMap.get(data.seriousIncidentsPriority).color);
                $("#seriousIncidentCountId").val(data.seriousIncidentsCount);
                $("#hdnSeriousIncidentPriority").val(data.seriousIncidentsPriority);
                SetTooltip('#imgSeriousIncidentTooltip', data.seriousIncidentsInfo);

                if (data.ltifCount > DaysInYear) {
                    $('.fleet-ltifCount').text(LTIFreeDaysExcessCondition);
                }
                else {
                    $('.fleet-ltifCount').text(data.ltifCount);
                }

                $('.fleet-ltifCount').addClass(colorMap.get(data.ltifPriority).textColor);
                $('.fleet-ltif-panel').addClass(colorMap.get(data.ltifPriority).color);
                SetTooltip('#divLtif', data.ltifInfo);

                $('.fleet-oilSpillsCount').text(data.oilSpillCount);
                $('.fleet-oilSpillsCount').addClass(colorMap.get(data.oilSpillPriority).textColor);
                $('.fleet-oilSpills-panel').addClass(colorMap.get(data.oilSpillPriority).color);
                $("#hdnOilSpillToWaterCount").val(data.oilSpillCount);
                $("#hdnOilSpillToWaterPriority").val(data.oilSpillPriority);
                SetTooltip('#imgOilSpillToWaterTooltip', data.oilSpillInfo);
            }

        },
        complete: function () {
            hazOccSuccess = true;
            UnBlockFleetSummaryLoader();
        }
    });
}

function BindCommercialFleetSummary(fltId, typeMenu, vesId, title) {
    var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
    var endDate = moment().format("DD MMM YYYY");

    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,

        "OffHireStartDate": startDate,
        "OffHireEndDate": endDate,
        "OffHirePriority": "00:17:26",

        "FuelEfficiencyFromDate": startDate,
        "FuelEfficiencyToDate": endDate,
        "FuelEfficiencyPriorityHighLimit": 5.00,
        "FuelEfficiencyPriorityLowLimit": 0.00
    }

    $.ajax({
        url: "/Dashboard/GetCommercialFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader();
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-offHire').text(data.offHireData == null ? "0" : data.offHireData);
                $('.fleet-offHire').addClass(colorMap.get(data.offHirePriority).textColor);
                $('.fleet-offHire-panel').addClass(colorMap.get(data.offHirePriority).color);
                $("#offHireCountId").val(data.offHireData == null ? "0" : data.offHireData);
                $("#hdnOffHirePriority").val(data.offHirePriority);
                SetTooltip('#imgOffHireTooltip', data.offHireInfo);

                $('.fleet-fuelEfficiencyCount').text(data.fuelEfficiencyPercentage);
                $('.fleet-fuelEfficiencyCount').addClass(colorMap.get(data.fuelEfficiencyPriority).textColor);
                $('.fleet-fuelEfficiency-panel').addClass(colorMap.get(data.fuelEfficiencyPriority).color);
                $("#hdnFuelEfficiencyCount").val(data.fuelEfficiencyPercentageValue);
                $("#hdnFuelEfficiencyPriority").val(data.fuelEfficiencyPriority);
                SetTooltip('#imgFuelEfficiencyTooltip', data.fuelEfficiencyInfo);
            }
        },
        complete: function () {
            commercialSuccess = true;
            UnBlockFleetSummaryLoader();
        }
    });
}

function BindRightshipFleetSummary(fltId, typeMenu, vesId, title) {

    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,
        "RightShipPriority": 3,
    }

    $.ajax({
        url: "/Dashboard/GetRightshipFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader();
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-rightShipCount').text(data.rightShipRate);
                $('.fleet-rightShipCount').addClass(colorMap.get(data.rightShipPriority).textColor);
                $('.fleet-rightShip-panel').addClass(colorMap.get(data.rightShipPriority).color);
                $("#hdnRightShipCount").val(data.rightShipRate);
                $("#hdnRightShipPriority").val(data.rightShipPriority);
                SetTooltip('#imgRightShipTooltip', data.rightShipInfo);
            }
        },
        complete: function () {
            rightshipSuccess = true;
            UnBlockFleetSummaryLoader();
        }
    });
}

function BindPMSFleetSummary(fltId, typeMenu, vesId, title) {

    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,

        "CriticalPmspriority": 0
    }

    $.ajax({
        url: "/Dashboard/GetPMSFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            BlockFleetSummaryLoader();
        },
        success: function (data) {
            if (data != null) {
                $('.fleet-criticalPmsCount').text(data.criticalPMSCount);
                $('.fleet-criticalPmsCount').addClass(colorMap.get(data.criticalPMSPriority).textColor);
                $('.fleet-criticalPms-panel').addClass(colorMap.get(data.criticalPMSPriority).color);
                $("#hdnCriticalPMSCountId").val(data.criticalPMSCount);
                $("#hdnCriticalPMSPriority").val(data.criticalPMSPriority);
                SetTooltip('#imgCriticalPMSTooltip', data.criticalPMSInfo);
            }
        },
        complete: function () {
            pmsSuccess = true;
            UnBlockFleetSummaryLoader();
        }
    });
}

function BindFleetSummary(fltId, typeMenu, vesId, title) {

    $("#fleetSelectionTitle").text(title);
    $("#fleetSelectionVesselTitle").text(title);
    $(".spanfleetSelectionTitle").text(title);
    var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
    var endDate = moment().format("DD MMM YYYY");

    var request =
    {
        "FleetId": fltId,
        "MenuType": typeMenu,
        "VesselId": vesId,
        "PSCDetentionFromDate": startDate,
        "PSCDetentionToDate": endDate,
        "PSCDetentionPriorityLimit": 0,

        "PSCDeficiencyFromDate": startDate,
        "PSCDeficiencyToDate": endDate,
        "PSCDeficiencyPriorityLimit": 0.88,

        "OMVFindingsFromdate": moment().subtract(6, "month").format("DD MMM YYYY"),
        "OMVFindingsToDate": endDate,
        "OMVFindingsPriorityLowLimit": 2.7,
        "OMVFindingsPriorityHighLimit": 2.8,

        "OverdueInspectionsPriorityLimit": 0,

        "IncidentStartDate": startDate,
        "IncidentEndDate": endDate,
        "SeriousIncidentsPriority": 0,

        "CriticalPmspriority": 0,

        "LtiFromDate": startDate,
        "LtiToDate": endDate,
        "LtifPriority": 7,

        "OffHireStartDate": startDate,
        "OffHireEndDate": endDate,
        "OffHirePriority": "00:17:26",

        "RightShipPriority": 3,

        "OilSpillFromDate": startDate,
        "OilSpillToDate": endDate,
        "OilSpillPriorityLimit": 0,

        "OpexToDate": moment().subtract(2, "month").endOf('month').format("DD MMM YYYY"),
        "BudgetDays": 90,
        "BudgetPercentageHighLimit": 10.00,
        "BudgetPercentageLowLimit": 5.00,

        "FuelEfficiencyFromDate": startDate,
        "FuelEfficiencyToDate": endDate,
        "FuelEfficiencyPriorityHighLimit": 5.00,
        "FuelEfficiencyPriorityLowLimit": 0.00,

    }

    $.ajax({
        url: "/Dashboard/GetFleetSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            $('.fleet-panel-loader').block({
                message: $(" " + loadercontent),
            })
            colorMap.forEach(function callbackFn(value, key, map) {
                $('.fleet-text').removeClass(map.get(key).textColor);
                $('.fleet-panel').removeClass(map.get(key).color);
            });
        },
        success: function (data) {
            $('.fleet-pscDetentionsCount').text(data.pscDetentionCount);
            $('.fleet-pscDetentionsCount').addClass(colorMap.get(data.pscDetentionPriority).textColor);
            $('.fleet-pscDetentions-panel').addClass(colorMap.get(data.pscDetentionPriority).color).addClass('fleet-pscDetentions-panel');
            $("#pscDetentionCountId").val(data.pscDetentionCount);
            $("#hdnPscDetentionPriority").val(data.pscDetentionPriority);

            $('.fleet-pscDeficienciesCount').text(data.pscDeficiencyRate);
            $('.fleet-pscDeficienciesCount').addClass(colorMap.get(data.pscDeficiencyPriority).textColor);
            $('.fleet-pscDeficiencies-panel').addClass(colorMap.get(data.pscDeficiencyPriority).color);
            $("#hdnpscDeficienciesCount").val(data.pscDeficiencyCount);
            $("#hdnPscDeficienciesPriority").val(data.pscDeficiencyPriority);
            $("#hdnPscDeficiencyRate").val(data.pscDeficiencyRate);
            $("#hdnPscDeficiencyInspectionCount").val(data.pscDeficiencyInspectionCount);

            $('.fleet-omvfindingCount').text(data.omvFindingsRate);
            $('.fleet-omvfindingCount').addClass(colorMap.get(data.omvFindingsPriority).textColor);
            $('.fleet-omvfinding-panel').addClass(colorMap.get(data.omvFindingsPriority).color);
            $("#hdnOmvFindingRate").val(data.omvFindingsRate);
            $("#hdnOmvFindingsCount").val(data.omvFindingsCount);
            $("#hdnOmvInspectionsCount").val(data.omvInspectionsCount);
            $("#hdnOmvFindingPriority").val(data.omvFindingsPriority);

            $('.fleet-overdueInspectionCount').text(data.overdueInspectionCount);
            $('.fleet-overdueInspectionCount').addClass(colorMap.get(data.overdueInspectionPriority).textColor);
            $('.fleet-overdueInspection-panel').addClass(colorMap.get(data.overdueInspectionPriority).color);
            $("#hdnOverdueInspection").val(data.overdueInspectionCount);
            $("#hdnOverdueInspectionPriority").val(data.overdueInspectionPriority);

            $('.fleet-seriousIncidentsCount').text(data.seriousIncidentsCount);
            $('.fleet-seriousIncidentsCount').addClass(colorMap.get(data.seriousIncidentsPriority).textColor);
            $('.fleet-seriousIncidents-panel').addClass(colorMap.get(data.seriousIncidentsPriority).color);
            $("#seriousIncidentCountId").val(data.seriousIncidentsCount);
            $("#hdnSeriousIncidentPriority").val(data.seriousIncidentsPriority);

            if (data.ltifCount > DaysInYear) {
                $('.fleet-ltifCount').text(LTIFreeDaysExcessCondition);
            }
            else {
                $('.fleet-ltifCount').text(data.ltifCount);
            }

            $('.fleet-ltifCount').addClass(colorMap.get(data.ltifPriority).textColor);
            $('.fleet-ltif-panel').addClass(colorMap.get(data.ltifPriority).color);

            $('.fleet-offHire').text(data.offHireData == null ? "0" : data.offHireData);
            $('.fleet-offHire').addClass(colorMap.get(data.offHirePriority).textColor);
            $('.fleet-offHire-panel').addClass(colorMap.get(data.offHirePriority).color);
            $("#offHireCountId").val(data.offHireData == null ? "0" : data.offHireData);
            $("#hdnOffHirePriority").val(data.offHirePriority);

            $('.fleet-rightShipCount').text(data.rightShipRate);
            $('.fleet-rightShipCount').addClass(colorMap.get(data.rightShipPriority).textColor);
            $('.fleet-rightShip-panel').addClass(colorMap.get(data.rightShipPriority).color);
            $("#hdnRightShipCount").val(data.rightShipRate);
            $("#hdnRightShipPriority").val(data.rightShipPriority);

            $('.fleet-criticalPmsCount').text(data.criticalPMSCount);
            $('.fleet-criticalPmsCount').addClass(colorMap.get(data.criticalPMSPriority).textColor);
            $('.fleet-criticalPms-panel').addClass(colorMap.get(data.criticalPMSPriority).color);
            $("#hdnCriticalPMSCountId").val(data.criticalPMSCount);
            $("#hdnCriticalPMSPriority").val(data.criticalPMSPriority);

            $('.fleet-overBudgetCount').text(data.opexOverBudgetPercentage);
            $('.fleet-overBudgetCount').addClass(colorMap.get(data.opexOverBudgetPriority).textColor);
            $('.fleet-overBudget-panel').addClass(colorMap.get(data.opexOverBudgetPriority).color);
            $("#hdnOverBudgetCount").val(data.opexOverBudgetPercentage);
            $("#hdnOverBudgetPriority").val(data.opexOverBudgetPriority);
            $("#hdnOverBudgetToDate").val(data.opexOverBudgetToDate);
            $("#hOpexHeader").text('Opex ' + data.opexOverBudgetToDate);

            $('.fleet-oilSpillsCount').text(data.oilSpillCount);
            $('.fleet-oilSpillsCount').addClass(colorMap.get(data.oilSpillPriority).textColor);
            $('.fleet-oilSpills-panel').addClass(colorMap.get(data.oilSpillPriority).color);
            $("#hdnOilSpillToWaterCount").val(data.oilSpillCount);
            $("#hdnOilSpillToWaterPriority").val(data.oilSpillPriority);

            $('.fleet-fuelEfficiencyCount').text(data.fuelEfficiencyPercentage);
            $('.fleet-fuelEfficiencyCount').addClass(colorMap.get(data.fuelEfficiencyPriority).textColor);
            $('.fleet-fuelEfficiency-panel').addClass(colorMap.get(data.fuelEfficiencyPriority).color);
            $("#hdnFuelEfficiencyCount").val(data.fuelEfficiencyPercentageValue);
            $("#hdnFuelEfficiencyPriority").val(data.fuelEfficiencyPriority);

            $('.fleet-experienceMatrixCount').text(data.experienceMatrixVesselCount);
            $('.fleet-experienceMatrixCount').addClass(colorMap.get(data.experienceMatrixPriority).textColor);
            $('.fleet-experienceMatrix-panel').addClass(colorMap.get(data.experienceMatrixPriority).color);
            $("#hdnExperienceMatrixCount").val(data.experienceMatrixVesselCount);
            $("#hdnExperienceMatrixPriority").val(data.experienceMatrixPriority);

            $('.fleet-panel-loader').unblock();
        },
        complete: function () {
            if (screen.width > 767) {
                $('.fleet-panel').matchHeight({
                    byRow: 0
                });
                $('.height-equal').matchHeight({
                    byRow: 0
                });
            }
            SetPurchaseOrderScrollerHeight($('#dashboard-section-2'));
        }
    });
}

function BindDashboardVesselList(fleetId, menuType, vesselId) {

    var request =
    {
        "FleetId": fleetId,
        "MenuType": menuType,
        "VesselIds": vesselId,
    }

    $.ajax({
        url: "/Dashboard/GetDashboardVesselList",
        type: "POST",
        "data": {
            "input": request,
        },
        "datatype": "html",
        beforeSend: function (xhr) {
            $('.dashboardvessel-panel-loader').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {
            if (data != null) {
                $('#divVesselResult').empty();
                $('#divVesselResult').html(data);

                SetOnVesselOnClick();

                if (parseInt($("#hdnTotalPages").val()) != 1) {
                    $('#btnLoadMoreVessels').toggle();
                }
                $(".expandIcon").click(ExpandVesselDetail);

                if ($('#vesselId').val() === '') {
                        $("#vesselCount").text("| " + $('#hdnTotalCount').val() + " Vessel(s)");

                } 

                BindVoyageReportingMobile();

                if ($('#hdnIsFleetSelection').val() == 'true') {
                    $('#hdnIsFleetSelection').val(false)
                    $('#hdnVesselIdentifier').val(null);
                }
                else {
                    if (screen.width < MobileScreenSize) {
                        if ($('#ActiveMobileTabClass').val() == "tab-2") {
                            var vesId = $('#hdnVesselIdentifier').val();
                            var vesselExpander = document.getElementById('Mobile_' + vesId);
                            if (vesselExpander != null && vesselExpander != undefined) {
                                vesselExpander.scrollIntoView({ behavior: 'smooth', block: 'center' });
                            }
                        }
                    }
                    else {
                        var vesId = $('#hdnVesselIdentifier').val();
                        var vesselExpander = document.getElementById(vesId);
                        if (vesselExpander != null && vesselExpander != undefined) {
                            vesselExpander.click();
                            vesselExpander.scrollIntoView(true);
                        }
                    }
                }

                allowedModules.CanShowCertificates = $("#CanShowCertificates").val();
                allowedModules.CanShowCommercial = $("#CanShowCommercial").val();
                allowedModules.CanShowCrewing = $("#CanShowCrewing").val();
                allowedModules.CanShowDefects = $("#CanShowDefects").val();
                allowedModules.CanShowEnvironment = $("#CanShowEnvironment").val();
                allowedModules.CanShowFinancials = $("#CanShowFinancials").val();
                allowedModules.CanShowHazOcc = $("#CanShowHazOcc").val();
                allowedModules.CanShowInspectionsAndRatings = $("#CanShowInspectionsAndRatings").val();
                allowedModules.CanShowPMS = $("#CanShowPMS").val();
                allowedModules.CanShowProcurement = $("#CanShowProcurement").val();

                if (allowedModules.CanShowDefects != "True") {
                    $("#defectHeading").hide();
                }

                if (allowedModules.CanShowCertificates != "True") {
                    $("#certificateHeading").hide();
                }
                if (allowedModules.CanShowCommercial != "True") {
                    $("#commercialHeading").hide();
                }
                if (allowedModules.CanShowCrewing != "True") {
                    $("#crewHeading").hide();
                }
                if (allowedModules.CanShowEnvironment != "True") {
                    $("#environmentHeading").hide();
                }

                if (allowedModules.CanShowFinancials != "True") {
                    $("#financialHeading").hide();
                }

                if (allowedModules.CanShowHazOcc != "True") {
                    $("#hazoccHeading").hide();
                }

                if (allowedModules.CanShowInspectionsAndRatings != "True") {
                    $("#inspectionHeading").hide();
                }

                if (allowedModules.CanShowPMS != "True") {
                    $("#pmsHeading").hide();
                }

                if (allowedModules.CanShowProcurement != "True") {
                    $("#procurementHeading").hide();
                }
            }
        },
        complete: function () {
            $('.height-equal-vessel').matchHeight();
            $('.equal-height-hazzoc-defect').matchHeight();
            $('.equal-height-pms-crew').matchHeight();
            $('.dashboardvessel-panel-loader').unblock();
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function SetOnVesselOnClick() {
    $(document).find(".customCollapseBtn").off();
    $(document).find(".customCollapseBtn").on('click', function () {
        event.preventDefault();
        let icon = $(this).parents('.vessel-name-list').find('.expandcollapse');
        let detailsDiv = $(this).parents('.vessel-name-list').next();
        $(icon).toggleClass('collapsed');
        if ($(detailsDiv).is(":visible")) {
            ReplaceClass($(detailsDiv), 'openedDetails', 'closedDetails');
        } else {
            ReplaceClass($(detailsDiv), 'closedDetails', 'openedDetails');
        }
        $(detailsDiv).slideToggle();
    });
}

function ExpandVesselDetail() {
    //$(this)[0].scrollIntoView();

    let panelId = $(this).attr('href');
    let parent = $(panelId);

    let vesselLink = $(this).data('url');

    let vesselId = vesselLink.split('VesselId=')[1];
    var vesselIdentifier = $(this).attr('id') + '';
    if (!dataLoad.has(panelId)) {
        $(parent).find('.vesselcommunicationlink').attr('data-id', vesselId);
        BindVesselDetailsSummary(vesselId, parent, vesselIdentifier);
        BindingVesselOfficerDetails(vesselId, parent);
        BindSentinelValue(vesselId, parent);
        if (allowedModules.CanShowDefects === "True") {
            BindDefectManager(vesselId, parent);
        }

        if (allowedModules.CanShowCertificates === "True") {
            BindCertificateSummary(vesselId, parent);
        }
        if (allowedModules.CanShowCommercial === "True") {
            BindCommercialSummary(vesselId, parent);
        }
        if (allowedModules.CanShowCrewing === "True") {
            BindCrewSummary(vesselId, parent);
        }
        if (allowedModules.CanShowEnvironment === "True") {
            BindEnvironmentSummary(vesselId, parent);
        }

        if (allowedModules.CanShowFinancials === "True") {
            BindOpexSummary(vesselId, parent);
        }

        if (allowedModules.CanShowHazOcc === "True") {
            BindHazoccSummary(vesselId, parent);
        }

        if (allowedModules.CanShowInspectionsAndRatings === "True") {
            BindInspectionSummary(vesselId, parent);
        }

        if (allowedModules.CanShowPMS === "True") {
            BindPMSSummary(vesselId, parent);
        }

        if (allowedModules.CanShowProcurement === "True") {
            BindPurchaseOrderSummary(vesselId, parent);
        }
        BindJSASummary(vesselId, parent);
        BindVoyageReporting(vesselId, parent);
        BindPerformanceSummary(vesselId, parent);
        BindVesselRightShip(vesselId, parent);
        dataLoad.add(panelId);
    }

}

function BindVoyageReporting(vesselId, parent) {
    $.ajax({
        url: "/Dashboard/GetVoyageLandingPageDetail",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselId": vesselId
        },
        beforeSend: function (xhr) {
            $(parent).find('.route-graph-loader').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {

            if (data != null) {
                if (data.isFromToVisible) {
                    $(parent).find(".clsActivityName").text(data.activityName);
                    $(parent).find(".voyage-divFromSection").show();
                    $(parent).find(".voyage-divToSection").show();

                    $(parent).find('.spanFoap').text(data.fromFAOPValue);
                    $(parent).find('.spanFromPortName').text(data.fromPortName);
                    $(parent).find('.spanSeaPassageFromDate').text(data.fromDate);

                    $(parent).find('.spanToEosp').text(data.toEOSPValue);
                    $(parent).find('.spanToPortName').text(data.toPortName);
                    $(parent).find('.spanSeaPassageToDate').text(data.toPortDate);

                    var viewMoreNav = "/VoyageReporting/VesselActivityList/?VoyageReportingRequestUrl=" + data.requestURL + "&VesselId=" + vesselId;
                    $(parent).find('.currentVoyageURL').attr("href", viewMoreNav);
                    $(parent).find('.chartername').text(data.charterName);
                    $(parent).find('.charternumber').text(data.charterNumber);

                    $(parent).find('.showFromAgentDetails').hide();
                    $(parent).find('.showToAgentDetails').hide();

                    if (!IsNullOrEmptyOrUndefinedLooseTyped(data.previousActivityId) || data.isAgentAvailable) {
                        $(parent).find('.showFromAgentDetails').show();
                        $(parent).find('.showFromAgentDetails').data('encryptedposid', data.fromAgentRequestURL);
                        $(parent).find('.showFromAgentDetails').data('portname', encodeURIComponent(data.fromPortName));
                    }

                    if (!IsNullOrEmptyOrUndefinedLooseTyped(data.nextActivityId)) {
                        $(parent).find('.showToAgentDetails').show();
                        $(parent).find('.showToAgentDetails').data('encryptedposid', data.toAgentRequestURL);
                        $(parent).find('.showToAgentDetails').data('portname', encodeURIComponent(data.toPortName));
                    }

                    $(parent).find('.fromAnchorPortAlertCls').addClass('d-none').removeClass('d-inline-block');
                    $(parent).find('.toAnchorPortAlertCls').addClass('d-none').removeClass('d-inline-block');

                    if (data.hasFromPortAlert) {
                        $(parent).find('.fromAnchorPortAlertCls').removeClass('d-none').addClass('d-inline-block');
                        $(parent).find('.fromAnchorPortAlertCls').data('url', data.fromRequestURL);
                    }

                    if (data.isToPortAlertVisible) {
                        $(parent).find('.toAnchorPortAlertCls').removeClass('d-none').addClass('d-inline-block');
                        $(parent).find('.toAnchorPortAlertCls').data('url', data.toRequestURL);
                    }

                    if (data.plA_ID != 'SP') {
                        $(parent).find(".HeadingPreformanceLeft").text("Latest Voyage Performance");
                        $(parent).find(".HeadingPreformanceRight").text("Previous Voyage Performance");
                    }
                    else {
                        $(parent).find(".HeadingPreformanceLeft").text("Current Voyage Performance");
                        $(parent).find(".HeadingPreformanceRight").text("Previous Voyage Performance");
                    }

                }
                else {
                    $(parent).find(".voyage-divFromSection").hide();
                    $(parent).find(".voyage-divToSection").hide();
                }

                LoadProgressBar(data, parent);
                LoadPortCallDetails(data, parent);
            }
        },
        complete: function () {
            $('.height-equal-vessel').matchHeight();
            $(parent).find('.route-graph-loader').unblock();
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function BindVoyageReportingMobileVesselList(vesselId, parent) {
    $.ajax({
        url: "/Dashboard/GetVoyageLandingPageDetail",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselId": vesselId
        },
        beforeSend: function (xhr) {
            $(parent).find('.route-graph-loader').block({
                message: $(" " + loadercontent),
            })
        },
        success: function (data) {
            if (data != null) {
                BindGraph(data, parent);
            }
        },
        complete: function () {
            $(parent).find('.route-graph-loader').unblock();
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function BindVoyageReportingsMobileVesselList(vesselIds) {
    $.ajax({
        url: "/Dashboard/GetVoyageDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "VesselIds": vesselIds
        },
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {
                    BindGraph(data[i], $('.mobile-voyage-vessel[data-id="' + data[i].encryptedVesselId + '"]')[0]);
                    $($('.mobile-voyage-vessel[data-id="' + data[i].encryptedVesselId + '"]')[0]).find('.route-graph-loader').unblock();
                }
            }
        },
        complete: function () {
            $('.route-graph-loader').unblock();
            $('[data-toggle="tooltip"]').tooltip();
        }
    });
}

function GetSeriousIncidents(request) {

    $('#modalSeriousIncident').block({
        message: $(" " + loadercontent),
    });

    $('#dtSeriousIncidents').DataTable().destroy();
    var dtSeriousIncidents = $('#dtSeriousIncidents').DataTable({
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
            "emptyTable": "No Serious incidents occurred.",
        },
        "ajax": {
            "url": "/Dashboard/GetSeriousIncidents",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "80px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-text-align",
                data: "shipRefNo",
                width: "45px",
                render: function (data, type, full, meta) {
                    var HazOccDetailsurl = "/HazOcc/Details/?HazOccDetails=" + full.hazOccDetailsUrlData + '&vesselId=' + full.encryptedVesselId;;
                    return GetCellData('Ship Ref No.', '<a href="' + HazOccDetailsurl + '"> ' + data + '</a>');
                }
            },
            {
                className: "data-datetime-align",
                data: "occurranceDate",
                width: "45px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Occurrence Date', data);
                }
            },
            {
                className: "data-text-align",
                data: "category",
                width: "80px",
                render: function (data, type, full, meta) { return GetCellData('Category', data); }
            },
            {
                className: "data-text-align",
                data: "classification",
                width: "110px",
                render: function (data, type, full, meta) { return GetCellData('Classification', data); }
            },
            {
                className: "data-text-align",
                data: "status",
                width: "60px",
                render: function (data, type, full, meta) { return GetCellData('Status', data); }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanSeriousIncidentsCount'
            var priorityValue = $("#hdnSeriousIncidentPriority").val()

            $(dataSpan).text($("#seriousIncidentCountId").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalSeriousIncident').unblock();
        }
    });
}

function GetPSCDetentionDetails(request) {
    $('#modalPscDetention').block({
        message: $(" " + loadercontent),
    })

    $('#dtPscDetention').DataTable().destroy();
    var dtPscDetention = $('#dtPscDetention').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "order": [],
        "language": {
            "emptyTable": "No psc detention.",
        },
        "ajax": {
            "url": "/Dashboard/GetPscDetentionDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "90px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-datetime-align",
                data: "detentionDate",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateWithFindingsURL(type, 'Date', data, full.encryptedFindingURL, full.encryptedVesselId);
                }
            },
            {
                className: "data-text-align",
                data: "port",
                width: "80px",
                render: function (data, type, full, meta) { return GetCellData('Port', data); }
            },
            {
                className: "data-text-align",
                data: "companyName",
                width: "200px",
                render: function (data, type, full, meta) { return GetCellData('Company', data); }
            },
            {
                className: "data-number-align",
                data: "daysDetained",
                width: "10px",
                render: function (data, type, full, meta) { return GetCellData('Days Detained', data); }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanPscDetention'
            var priorityValue = $("#hdnPscDetentionPriority").val()

            $(dataSpan).text($("#pscDetentionCountId").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalPscDetention').unblock();
        }
    });
}

function GetFormattedDateWithFindingsURL(type, label, data, encryptedFindingURL, encryptedVesselId) {
    var date = "";
    var formattedDate = "";
    if (data != null && data != '' && data != undefined) {
        date = new Date(data);
        formattedDate = moment(date).format("DD MMM YYYY");
    }
    if (type === "display") {
        return GetCellData(label, '<a href="/Inspection/Findings/?finding=' + encryptedFindingURL + '&vesselId=' + encryptedVesselId + '">' + formattedDate + '</a>');
    }
    return date;
}

function GetFormattedDateWithURL(type, label, data, target, hrefLink) {
    var date = "";
    var formattedDate = "";
    if (data != null && data != '' && data != undefined) {
        date = new Date(data);
        formattedDate = moment(date).format("DD MMM YYYY");
    }
    if (type === "display") {
        return GetCellData(label, '<a target="' + target + '" href="' + hrefLink + '">' + formattedDate + '</a>');
    }
    return date;
}


function GetOmvFindingsSummary(request) {

    $("#hideDeficiencyOMV").prop('checked', false);
    $('#modalOmvFindingsSummary').block({
        message: $(" " + loadercontent),
    })

    $('#dtOmvFindings').DataTable().destroy();

    var dtOmvFindings = $('#dtOmvFindings').DataTable({
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
            "url": "/Dashboard/GetOmvFindingsDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "120px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-datetime-align",
                data: "inspectionDate",
                width: "65px",
                render: function (data, type, full, meta) {
                    var hrefLink = '/Inspection/List/?Inspection=' + full.encryptedInspectionURL + '&vesselId=' + full.encryptedVesselId;
                    return GetFormattedDateWithURL(type, 'Insp. Date', data, "_self", hrefLink);
                }
            },
            {
                className: "data-text-align",
                data: "where",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Where', data); }
            },
            {
                className: "data-text-align",
                data: "companyName",
                width: "200px",
                render: function (data, type, full, meta) { return GetCellData('Company', data); }
            },
            {
                className: "data-text-align",
                data: "inspectorName",
                width: "120px",
                render: function (data, type, full, meta) { return GetCellData('Inspector', data); }
            },
            {
                className: "data-number-align",
                data: "findingCount",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'No. of Findings', data, 0, "0");
                }
            },
            {
                className: "data-datetime-align",
                data: "nextDueDate",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Next Due Date', data);
                }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanOmvRate'
            var priorityValue = $("#hdnOmvFindingPriority").val()

            $(dataSpan).text($("#hdnOmvFindingRate").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#spanInspectionCount').text($("#hdnOmvInspectionsCount").val());
            $('#spanFindingCount').text($("#hdnOmvFindingsCount").val());

            $('#modalOmvFindingsSummary').unblock();
        }
    });
}

function GetOverBudgetSummary(request) {
    $('#modalOverBudgetSummary').block({
        message: $(" " + loadercontent),
    })

    $('#dtOverBudget').DataTable().destroy();
    var dtOverBudget = $('#dtOverBudget').DataTable({
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
        "ajax": {
            "url": "/Dashboard/GetOverBudgetDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "90px",
                render: function (data, type, full, meta) {
                    return '<a href="/Finance/List?OperationCostRequestUrl=' + full.encryptedOpexURL + '&VesselId=' + full.encryptedVesselId + '">' + data + '</a>';
                },
            },
            {
                className: "data-number-align",
                "data": "total",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetFormattedDecimalKPI(type, 'Total', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetFormattedDecimal(type, 'Total', data, 2, '0.00');
                    }
                }
            },
            {
                className: "data-number-align",
                "data": "budget",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetFormattedDecimalKPI(type, 'Budget', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetFormattedDecimal(type, 'Budget', data, 2, '0.00');
                    }
                }
            },
            {
                className: "data-number-align",
                "data": "variance",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetFormattedDecimalKPI(type, 'Variance', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetFormattedDecimal(type, 'Variance', data, 2, '0.00');
                    }
                }
            },
            {
                className: "data-number-align",
                "data": "budgetPercenatge",
                width: "70px",
                render: function (data, type, full, meta) {
                    if (data < 0) {
                        return GetFormattedDecimalKPI(type, 'Variance %', data, 2, '0.00', 'txt-red');
                    }
                    else {
                        return GetFormattedDecimal(type, 'Variance %', data, 2, '0.00');
                    }
                }
            }
        ],
        "initComplete": function (settings) {
            $('#spanOpexDetailsHeader').text('Opex ' + $("#hdnOverBudgetToDate").val());
            var dataSpan = '#spanOverBudgetPercentage'
            var priorityValue = $("#hdnOverBudgetPriority").val()

            $(dataSpan).text($("#hdnOverBudgetCount").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalOverBudgetSummary').unblock();
        }
    });
}

function LoadPortCallDetails(data, parent) {

    $(parent).find(".portCountryCls").text(data.countryName);
    $(parent).find(".portLocCodeCls").text(data.unlocode);
    $(parent).find(".portKeyHubCls").text(data.isKeyHubPort);
    $(parent).find(".portLatitudeCls").text(data.fullLatitude);
    $(parent).find(".portLongitudeCls").text(data.fullLongitude);
    $(parent).find(".portEospDateHeaderCls").text(data.eospDateHeader);
    $(parent).find(".portEospDateCls").text(data.eospDate);
    $(parent).find(".portBerthDateHeaderCls").text(data.berthDateHeader);
    $(parent).find(".portBerthDateCls").text(data.berthDate);
    $(parent).find(".PortUnBerthDateHeaderCls").text(data.unBerthDateHeader);
    $(parent).find(".PortUnBerthDateCLs").text(data.unBerthDate);
    $(parent).find(".portFaopDateHeaderCls").text(data.faopDateHeader);
    $(parent).find(".portFaopDateCls").text(data.faopDate);
    $(parent).find(".portVoyageNoCls").text(data.voyageNumber);
    $(parent).find(".portLastReportedEventCls").text(data.lastUpdatedEventDate);
}

function LoadProgressBar(data, parent) {

    BindGraph(data, parent);

    if (data != null && data.badWeatherDetails != null && data.badWeatherDetails.length > 0) {
        var length = data.badWeatherDetails.length;
        var i = 0;
        for (i; i < length; i++) {

            //this is for bad weather
            //IsOnlyBadWeatherAlert || IsBreakAndBadWeatherAlert - BadWeatherDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyBadWeatherAlert == true || data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
                var badWeatherPosition = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

                if (data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
                    $(parent).find(".divProgressBar").append('<a class="badweather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
                        '<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                        'style="left:' + badWeatherPosition + '%;top: -23px;" data-html="true"></div></a>');
                }
                else {
                    $(parent).find(".divProgressBar").append('<a class="badweather" href="javascript: void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
                        '<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                        'style="left:' + badWeatherPosition + '%;" data-html="true"></div></a>');
                }
            }

            //this is for off hire
            //IsOnlyBreakAlert || IsBreakAndBadWeatherAlert uses - BreakDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyBreakAlert == true || data.badWeatherDetails[i].isBreakAndBadWeatherAlert == true) {
                var offHirePosition = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);
                $(parent).find(".divProgressBar").append('<a class="offhire" href="javascript:void(0);" id=' + data.badWeatherDetails[i].voyageReportingModalRequestURL + '>' +
                    '<div class="graph-position-default offhire-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                    'style="left:' + offHirePosition + '%;" data-html="true"></div></a>');
            }

            //IsOnlyPortBadWeatherAlert - uses PortBadWeathersDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyPortBadWeatherAlert == true) {
                var PortBadWeathersDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);
                $(parent).find(".divProgressBar").append('<a class="portBadWeather" href="javascript:void(0);" id=' + data.requestURL + '>' +
                    '<div class="graph-position-default weather-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                    'style="left:' + PortBadWeathersDetail + '%;" data-html="true"></div></a>');
            }

            //IsOnlyDelayAlert - use DelaysDetailsTemplate
            if (data.badWeatherDetails[i].isOnlyDelayAlert == true) {
                var DelaysDetail = ConvertValueToPercentage(data.badWeatherDetails[i].distance, data.totalDistance);

                $(parent).find(".divProgressBar").append('<a class="portDelay" href="javascript:void(0);" id=' + data.requestURL + '>' +
                    '<div class="graph-position-default delayed-location" role="progressbar" aria-valuenow="45" aria-valuemin="0" aria-valuemax="100"' +
                    'style="left:' + DelaysDetail + '%;" data-html="true"></div></a>');
            }
        }
    }
}

function BindGraph(data, parent) {
    let toalDistance = parseFloat(data.totalDistance);
    if (toalDistance > 0) {
        $(parent).find('.spanSPTotalDistance').text(ConvertDecimalNumberToString(data.totalDistance, 2, 1, 2) + " nm" + " total");
    }
    else
        $(parent).find('.spanSPTotalDistance').hide();

    let distanceTravelled = parseFloat(data.distanceTravelled);
    if (distanceTravelled > 0) {
        $(parent).find('.spanSPDistanceCovered').text(ConvertDecimalNumberToString(data.distanceTravelled, 2, 1, 2) + " nm" + " traveled");
    }
    else
        $(parent).find('.spanSPDistanceCovered').hide();

    if (data.isSeaPassageEvent == true) {
        var endPositionOfVessel = ConvertValueToPercentage(data.distanceTravelled, data.totalDistance);
        var progressBarEndContent = "AT SEA" + "<br/>" + data.lastEventPosition + "<br/>" + data.remainingValue + " nm remaining"; 
        $(parent).find('.divProgressBarFlow').css('width', endPositionOfVessel + '%');
        $(parent).find('.at-location').css('left', endPositionOfVessel + '%');
        $(parent).find('.at-location').attr('data-original-title',progressBarEndContent);
        $(parent).find('.graphtooltip').attr('data-original-title', progressBarEndContent);
        $(parent).find('[data-toggle="tooltip"]').tooltip({html:true});
        $(parent).find('.graphtooltip').tooltip({html:true});
    }
}

function BindPerformanceSummary(vesselId, parent) {

    var startDate = moment().subtract(3, "month").format("DD MMM YYYY");
    var endDate = moment().format("DD MMM YYYY");

    var request =
    {
        "VesselId": vesselId,
        "StartDate": null,
        "EndDate": null,
    }

    $.ajax({
        url: "/Dashboard/GetPerformanceSummary",
        type: "POST",
        "data": {
            "request": request,
        },
        "datatype": "JSON",
        beforeSend: function (xhr) {
            $(parent).find(".performance-section").block({
                message: $(" " + loadercontent),
            });
        },
        success: function (result) {
          
            for (let i = 0; i < result.length; i++) {
                if (result.length == 1) {
                    $(parent).find(".hidepreformanceright").hide();
                }
                else {
                    $(parent).find(".hidepreformanceright").show();
                }

                let obj = result[i];
                let selector = '';
                if (obj.rankId == 1) {
                    selector = ".hidepreformanceleft";
                }
                else {
                    selector = ".hidepreformanceright";
                }
                SetPerformanceSummary(parent, selector, obj);
            }
        },
        complete: function () {
            $('.height-equal-vessel').matchHeight();
            $(parent).find(".performance-section").unblock();
        }
    });
}

function SetPerformanceSummary(parent, selector, data) {

    $(parent).find(selector).find(".last24HourSpeed-text").text(data.last24HourSpeed);
    $(parent).find(selector).find(".last24HourSpeed").addClass(colorMap.get(data.last24HourSpeedPriority).textColor);
    $(parent).find(selector).find(".last24HourSpeed").addClass(colorMap.get(data.last24HourSpeedBackgroundPriority).color);

    $(parent).find(selector).find(".last24HourConsumption-text").text(data.last24HourConsumption);
    $(parent).find(selector).find(".last24HourConsumption").addClass(colorMap.get(data.last24HourConsumptionPriority).textColor);
    $(parent).find(selector).find(".last24HourConsumption").addClass(colorMap.get(data.last24HourConsumptionBackgroundPriority).color);

    $(parent).find(selector).find(".voyageAverageSpeed-text").text(data.voyageAverageSpeed);
    $(parent).find(selector).find(".voyageAverageSpeed").addClass(colorMap.get(data.voyageAverageSpeedPriority).textColor);
    $(parent).find(selector).find(".voyageAverageSpeed").addClass(colorMap.get(data.voyageAverageSpeedBackgroundPriority).color);

    $(parent).find(selector).find(".voyageAverageConsumption-text").text(data.voyageAverageConsumption);
    $(parent).find(selector).find(".voyageAverageConsumption").addClass(colorMap.get(data.voyageAverageConsumptionPriority).textColor);
    $(parent).find(selector).find(".voyageAverageConsumption").addClass(colorMap.get(data.voyageAverageConsumptionBackgroundPriority).color);

    $(parent).find(selector).find(".cpAdjustedSpeed-text").text(data.cpAdjustedSpeed);
    $(parent).find(selector).find(".cpAdjustedSpeed").addClass(colorMap.get(data.cpAdjustedSpeedPriority).textColor);
    $(parent).find(selector).find(".cpAdjustedSpeed").addClass(colorMap.get(data.cpAdjustedSpeedBackgroundPriority).color);

    $(parent).find(selector).find(".cpAdjustedConsumption-text").text(data.cpAdjustedConsumption);
    $(parent).find(selector).find(".cpAdjustedConsumption").addClass(colorMap.get(data.cpAdjustedConsumptionPriority).textColor);
    $(parent).find(selector).find(".cpAdjustedConsumption").addClass(colorMap.get(data.cpAdjustedConsumptionBackgroundPriority).color);

    $(parent).find(selector).find(".cpOrdersSpeed-text").text(data.cpOrdersSpeed);
    //$(parent).find(".cpOrdersSpeed").addClass(colorMap.get(data.cpOrdersSpeedPriority).textColor);
    //$(parent).find(".cpOrdersSpeed").addClass(colorMap.get(data.cpOrdersSpeedBackgroundPriority).color);

    $(parent).find(selector).find(".cpOrdersConsumption-text").text(data.cpOrdersConsumption);
            //$(parent).find(".cpOrdersConsumption").addClass(colorMap.get(data.cpOrdersConsumptionPriority).textColor);
            //$(parent).find(".cpOrdersConsumption").addClass(colorMap.get(data.cpOrdersConsumptionBackgroundPriority).color);     
}

function GetPSCDeficiencyDetails(request) {
    $("#hideDeficiencyPSC").prop('checked', false);

    $('#modalPscDeficiency').block({
        message: $(" " + loadercontent),
    });

    $('#dtPscDeficiency').DataTable().destroy();
    var dtPscDeficiency = $('#dtPscDeficiency').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "fixedHeader": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "order": [],
        "language": {
            "emptyTable": "No PSC Deficiencies.",
        },
        "ajax": {
            "url": "/Dashboard/GetPSCDeficiencies",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "110px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-datetime-align",
                data: "detentionDate",
                width: "55px",
                render: function (data, type, full, meta) {
                    var hrefLink = '/Inspection/List/?Inspection=' + full.encryptedInspectionURL + '&vesselId=' + full.encryptedVesselId;
                    return GetFormattedDateWithURL(type, 'Insp. Date', data, "_self", hrefLink);
                }
            },
            {
                className: "data-text-align",
                data: "whereLocation",
                width: "120px",
                render: function (data, type, full, meta) { return GetCellData('Where', data); }
            },

            {
                className: "data-text-align",
                data: "companyName",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Company', data); }
            },
            {
                className: "data-text-align",
                data: "inspectorName",
                width: "140px",
                render: function (data, type, full, meta) { return GetCellData('Inspector Name', data); }
            },
            {
                className: "data-number-align",
                data: "findingCount",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'No of Deficiencies', data, 0, 0);
                }
            },
            {
                className: "data-text-align",
                data: "isDetained",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetCellData('Detained (Y/N)', data);
                }
            },
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanPscDefRate'
            var priorityValue = $("#hdnPscDeficienciesPriority").val()

            $(dataSpan).text($("#hdnPscDeficiencyRate").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#spanPscDefInspectionCount').text($("#hdnPscDeficiencyInspectionCount").val());
            $('#spanPscDefFindingCount').text($("#hdnpscDeficienciesCount").val());

            $('#modalPscDeficiency').unblock();
        }
    });
}

function GetCriticalPMSDetails(request) {
    $('#modalPMSOverdue').block({
        message: $(" " + loadercontent),
    })

    $('#dtPMSOverdue').DataTable().destroy();
    var dtPMSOverdue = $('#dtPMSOverdue').DataTable({
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
            "emptyTable": "No critical pms.",
        },
        "ajax": {
            "url": "/Dashboard/GetCriticalPMS",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "55px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-datetime-align",
                data: "dueDate",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Due Date', data);
                }
            },
            {
                className: "data-number-align",
                width: "38px",
                render: function (data, type, full, meta) {
                    let todaysDate = moment().startOf('day');
                    let dueDate = moment(new Date(full.dueDate));
                    let diff = moment.duration(todaysDate.diff(dueDate)).asDays();
                    return GetCellData('No. Of Days Overdue', diff);
                }
            },
            {
                className: "data-text-align",
                data: "jobName",
                width: "125px",
                render: function (data, type, full, meta) {
                    return GetCellData('Job Name', '<a href = "/PlannedMaintenance/Detail/?PlannedMaintenanceDetails=' + full.plannedMaintenanceDetailsRequestURL + '&VesselId=' + full.encryptedVesselId + '"> ' + data + '</a > ');
                }
            },
            {
                className: "data-text-align",
                data: "componentName",
                width: "125px",
                render: function (data, type, full, meta) { return GetCellData('Component Name', data); }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanPmsOverdueCount'
            var priorityValue = $("#hdnCriticalPMSPriority").val()

            $(dataSpan).text($("#hdnCriticalPMSCountId").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalPMSOverdue').unblock();
        }
    });
}

function GetOverdueInspectionDetails(request) {
    $('#modalOverdueInspection').block({
        message: $(" " + loadercontent),
    });

    $('#dtOverdueInspection').DataTable().destroy();
    var dtOverdueInspection = $('#dtOverdueInspection').DataTable({
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
            "emptyTable": "No overdue inspection.",
        },
        "ajax": {
            "url": "/Dashboard/GetOverdueInspectionDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "90px",
                render: function (data, type, full, meta) {
                    return '<a href="/Inspection/List/?Inspection=' + full.encryptedInspectionURL + '&vesselId=' + full.encryptedVesselId + '">' + data + '</a>';
                }
            },
            {
                className: "data-text-align",
                data: "inspectionType",
                width: "100px",
                render: function (data, type, full, meta) {
                    return GetCellData('Inspection Type', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "lastDoneDate",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Last Done', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "nextDueDate",
                width: "50px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Due Date', data);
                }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanOverdueInspection'
            var priorityValue = $("#hdnOverdueInspectionPriority").val()

            $(dataSpan).text($("#hdnOverdueInspection").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalOverdueInspection').unblock();
        }
    });
}

function GetOilSpillsToWater(request) {
    $('#modalOilSpill').block({
        message: $(" " + loadercontent),
    })

    $('#dtOilSpillsToWater').DataTable().destroy();
    var dtOilSpillsToWater = $('#dtOilSpillsToWater').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": false,
        "paging": false,
        "scrollY": "150px",
        "order": [],
        "language": {
            "emptyTable": "No record for oil spills to water.",
        },
        "ajax": {
            "url": "/Dashboard/GetOilSpillsToWaterDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "90px",
                render: function (data, type, full, meta) { return data; }
            },
            {
                className: "data-text-align",
                data: "shipRefNo",
                width: "50px",
                render: function (data, type, full, meta) {
                    var HazOccDetailsurl = "/HazOcc/Details/?HazOccDetails=" + full.hazOccRequestURL;
                    return GetCellData('Ship Ref No.', '<a href="' + HazOccDetailsurl + '"> ' + data + '</a>');
                }
            },
            {
                className: "data-datetime-align",
                data: "incidentDate",
                width: "65px",
                render: function (data, type, full, meta) {
                    return GetFormattedDate(type, 'Incident Date', data);
                }
            },
            {
                className: "data-text-align",
                data: "incidentType",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Incident Type', data); }
            },
            {
                className: "data-text-align",
                data: "severity",
                width: "70px",
                render: function (data, type, full, meta) { return GetCellData('Severity', data); }
            },
            {
                className: "data-text-align",
                data: "locOfVessel",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Location Of Vessel', data); }
            },
            {
                className: "data-number-align",
                data: "quantitySpilled",
                width: "60px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Quantity Spilled (ltr)', data, 0, "0.00");
                }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanOilSplillsToWater'
            var priorityValue = $("#hdnOilSpillToWaterPriority").val()

            $(dataSpan).text($("#hdnOilSpillToWaterCount").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalOilSpill').unblock();
        }
    });
}

function GetRightShipDetils(request) {
    $('#modalRightShip').block({
        message: $(" " + loadercontent),
    })

    $('#dtRightShip').DataTable().destroy();
    var dtRightShip = $('#dtRightShip').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": true,
        "paging": false,
        "scrollY": "150px",
        "order": [[1, "desc"]],
        "language": {
            "emptyTable": "No rightship occurred.",
        },
        "ajax": {
            "url": "/Dashboard/GetRightShipDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "275px",
                render: function (data, type, full, meta) { return data }
            },
            {
                className: "data-number-align",
                data: "rightShipScore",
                width: "80px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Right Ship Score', data, 2, '0.00');
                }
            },
            {
                className: "data-text-align",
                data: "ghgRating",
                width: "45px",
                render: function (data, type, full, meta) {
                    if (data == null) {
                        data = '';
                    }
                    return GetCellData("GHG Rating", data);
                }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanRightshipScore'
            var priorityValue = $("#hdnRightShipPriority").val()

            $(dataSpan).text($("#hdnRightShipCount").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalRightShip').unblock();
        }
    });
}

function GetExperienceMatrixDetils(request) {
    $('#modalExperienceMatrix').block({
        message: $(" " + loadercontent),
    })

    $('#dtExperienceMatrix').DataTable().destroy();
    var dtExperienceMatrixList = $('#dtExperienceMatrix').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": true,
        "paging": false,
        "scrollY": "150px",
        "order": [[0, "asc"]],
        "language": {
            "emptyTable": "No experience matrix occurred.",
        },
        "ajax": {
            "url": "/Dashboard/GetExperienceMatrixDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "125px",
                render: function (data, type, full, meta) {
                    return '<a href="/Crew/List/?CrewList=' + full.encryptedCrewURL + '&VesselId=' + full.encryptedVesselId + '&IsViewMore=true">' + data + '</a>'
                }
            },
            {
                className: "data-text-align",
                data: "departmentName",
                width: "125px",
                render: function (data, type, full, meta) { return GetCellData('Department', data); }
            },
            {
                className: "data-text-align",
                data: "crewName",
                width: "250px",
                render: function (data, type, full, meta) { return GetCellData('Crew', data); }
            },
            {
                className: "data-text-align",
                data: "rankName",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Rank', data); }
            },
            {
                className: "data-number-align",
                data: "vmsExperienceInDays",
                width: "60px",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetCellData('VMS Exp', full.experienceInYearsAndMonths);
                    }
                    return data;
                }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanExperienceMatrix'
            var priorityValue = $("#hdnExperienceMatrixPriority").val()

            $(dataSpan).text($("#hdnExperienceMatrixCount").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalExperienceMatrix').unblock();
        }
    });
}


function GetOffHireSummary(request) {

    $('#modalOffHireSummary').block({
        message: $(" " + loadercontent),
    })

    $('#dtOffHireSummary').DataTable().destroy();
    var dtOffHireSummary = $('#dtOffHireSummary').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "info": false,
        "autoWidth": true,
        "paging": false,
        "scrollY": "150px",
        "language": {
            "emptyTable": "No offhire data present.",
        },
        "ajax": {
            "url": "/Dashboard/GetOffHireSummary",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "140px",
                render: function (data, type, full, meta) {
                    if (full.isSeaPassageEvent == true) {
                        return '<a href="/VoyageReporting/SeaPassageEvent?VoyageReportingRequestUrl=' + full.seaPassageURL + '&VesselId=' + full.encryptedVesselId + '">' + data + '</a>';
                    }
                    else {
                        return '<a href="/VoyageReporting/PortCallLocationEvent?VoyageReportingRequestUrl=' + full.portCallURL + '&VesselId=' + full.encryptedVesselId + '">' + data + '</a>';
                    }
                }
            },
            {
                className: "data-text-align",
                data: "reason",
                width: "160px",
                render: function (data, type, full, meta) { return GetCellData('Reason', data); }
            },
            {
                className: "data-number-align",
                data: "delayDuration",
                width: "40px",
                render: function (data, type, full, meta) {
                    return GetCellData('Delay (hh:mm:ss)', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "dateFrom",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateTime(type, 'From', data);
                }
            },
            {
                className: "data-datetime-align",
                data: "dateTo",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDateTime(type, 'To', data);
                }
            },
            {
                className: "data-text-align tdblock",
                data: "offHireType",
                width: "62px",
                render: function (data, type, full, meta) { return GetCellData('OffHire Type', data); }
            },
            {
                className: "data-text-align tdblock",
                data: "comment",
                width: "55px",
                render: function (data, type, full, meta) { return GetCellData('Comments', data == null ? "" : data); }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanOffHireValue'
            var priorityValue = $("#hdnRightShipPriority").val()

            $(dataSpan).text($("#offHireCountId").val());
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#spanOffHireCount').text(this.api().data().length);
            $('#modalOffHireSummary').unblock();
        }
    });
}

function OpenModalPredictedBadWeather(vesselId, vesselName) {

    $('#vesselNameBadWeatherModal').text(vesselName);

    $('#dtbadweather').DataTable().destroy();
    var dtbadweather = $('#dtbadweather').DataTable({
        "dom": '<<"row mb-3" <"detailsinfo"><"col-12 col-md-7 offset-md-1 col-lg-7 offset-lg-1 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
        "processing": false,
        "serverSide": false,
        "lengthChange": true,
        "searching": false,
        "scrollY": "235px",
        "scrollCollapse": true,
        "info": true,
        "autoWidth": false,
        "paging": false,
        "language": {
            "emptyTable": "No bad weather data present.",
        },
        "ajax": {
            "url": "/Dashboard/GetPredictedBadWeather",
            "type": "POST",
            "data": {
                "input": vesselId,
            },
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-datetime-align",
                data: "weatherDate",
                width: "60px",
                render: function (data, type, full, meta) { return GetFormattedDateTime(type, 'Date', data); }
            },
            {
                className: "data-text-align",
                data: "locationStr",
                width: "100px",
                render: function (data, type, full, meta) { return GetCellData('Location', data); }
            },
            {
                className: "data-text-align",
                data: "speedBeaufort",
                width: "40px",
                render: function (data, type, full, meta) {
                    return GetCellData('Beaufort', '<span style="color:' + full.beaufortColour + '">F' + data + '</span');
                }
            },
            {
                className: "data-number-align",
                data: "speedMS",
                width: "45px",
                render: function (data, type, full, meta) { return GetCellData('Speed m/s', data); }
            },
            {
                className: "data-number-align",
                data: "direction",
                width: "45px",
                render: function (data, type, full, meta) { return GetCellData('Direction', data == null ? "" : data); }
            }
        ]
    });
}

function GetFuelEfficiencySummary(request) {
    $('#modalFuelEfficiencySummary').block({
        message: $(" " + loadercontent),
    })

    $('#dtFuelEfficiency').DataTable().destroy();
    var dtFuelEfficiency = $('#dtFuelEfficiency').DataTable({
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
        "ajax": {
            "url": "/Dashboard/GetFuelEfficiencyDetails",
            "type": "POST",
            "data": request,
            "datatype": "json"
        },
        "columns": [
            {
                className: "data-text-align tdblock",
                data: "vesselName",
                width: "120px",
                render: function (data, type, full, meta) {
                    return '<a href="/VoyageReporting/VesselActivityList?isFromDashboard=true&VoyageReportingRequestUrl=' + full.encryptedFuelEfficiencyURL + '&VesselId=' + full.encryptedVesselId + '">' + data + '</a>';
                },
            },
            {
                className: "data-number-align",
                "data": "fuelEfficiencyRatio",
                width: "90px",
                render: function (data, type, full, meta) {
                    return GetFormattedDecimal(type, 'Fuel Efficiency Ratio', data, 2, '0.00');
                }
            }
        ],
        "initComplete": function (settings) {
            var dataSpan = '#spanFuelEfficiency'
            var priorityValue = $("#hdnFuelEfficiencyPriority").val()

            $(dataSpan).text($("#hdnFuelEfficiencyCount").val() + '%');
            removeDetailsSubHeaderClass(dataSpan);
            addDetailsSubHeaderClass(dataSpan, priorityValue)

            $('#modalFuelEfficiencySummary').unblock();
        }
    });
}

function removeDetailsSubHeaderClass(spanSubHeader) {

    if ($(spanSubHeader).hasClass('vessel-red-color')) {
        $(spanSubHeader).removeClass('vessel-red-color');
    }
    if ($(spanSubHeader).hasClass('vessel-amber-color')) {
        $(spanSubHeader).removeClass('vessel-amber-color');
    }
    if ($(spanSubHeader).hasClass('vessel-green-color')) {
        $(spanSubHeader).removeClass('vessel-green-color');
    }
}

function addDetailsSubHeaderClass(spanSubHeader, priority) {

    if (!$(spanSubHeader).hasClass('vessel-red-color') && priority == '2') {
        $(spanSubHeader).addClass('vessel-red-color');
    }
    if (!$(spanSubHeader).hasClass('vessel-orange-color') && priority == '1') {
        $(spanSubHeader).addClass('vessel-orange-color');
    }
    if (!$(spanSubHeader).hasClass('vessel-green-color') && priority == '0') {
        $(spanSubHeader).addClass('vessel-green-color');
    }
}

function BindVesselRightShip(vesselid, parent) {
    let input = {
        "VesselId": vesselid
    }
    $.ajax({
        url: "/Dashboard/GetRightShipGHGRatingDetails",
        type: "POST",
        dataType: "JSON",
        data: {
            "request": input
        },
        beforeSend: function (xhr) {
            $(parent).find('.rightship-details').block({
                message: $(" " + loadercontent),
            });
        },

        success: function (data) {
            if (data != null && data.data != null) {
                let rightShipDetails = data.data;
                if (!IsNullOrEmpty(rightShipDetails.rightShipScore)) {
                    let score = GetFormattedDecimal('', '', rightShipDetails.rightShipScore, 2, '0.00');
                    $(parent).find('.spanRightShipScore').text(score);
                }
                if (!IsNullOrEmpty(rightShipDetails.ghgRating)) {
                    $(parent).find('.spanRightShipRating').text(rightShipDetails.ghgRating);
                }
            }
        },
        complete: function () {
            $('.height-equal-vessel').matchHeight();
            $(parent).find('.rightship-details').unblock();
        }
    });
}

function BindVoyageReportingMobile() {
    if (screen.width < MobileScreenSize) {

        var vesselList = [];
        $(".mobile-voyage-vessel").each(function () {
            let vesselId = $(this).data("id");
            $(this).find('.route-graph-loader').block({
                message: $(" " + loadercontent),
            })
            vesselList.push(vesselId);
        });
        BindVoyageReportingsMobileVesselList(vesselList);
    }
}

function fetchClientLogo() {
    $.ajax({
        url: "/Dashboard/GetUserClientLogo",
        dataType: "JSON",
        type: "POST",
        success: function (data) {
            $(".divUserClientName").text(data.clientName);
            $(".divSubHeadUserClientName").text(data.clientName);

            if (data.image != null) {
                $("#imgClientLogo").attr("src", "data:image/png;base64," + data.image + "");
            }
            else {
                $("#imgClientLogo").attr("src", "/images/Vships-logo.png");
            }
        }
    })
}

function SetTooltip(element, content) {
    $(element).attr('title', "");
    $(element).attr('data-original-title', content);
    $('[data-toggle="tooltip"]').tooltip();
    $(element).tooltip();
}

function ShowFullMap() {
    $.ajax({
        url: "/Dashboard/GetDashboardFullMapUrl",
        dataType: "JSON",
        type: "POST",
        success: function (data) {
            var fullMapUrl = "/Dashboard/DashboardMapFullScreen/?mapurl=" + data;
            window.location.href = fullMapUrl;
        }
    })
}

function AddMessagingUserIfNotExists() {
    $.ajax({
        url: "/Dashboard/AddMessagingUserIfNotExists",
        type: "POST",
        dataType: "JSON",
        success: function (userDetails) {
        }
    });
}

function GetApprovalsList() {
    var input = {
        FleetId: $('#fleetId').val(),
        MenuType: $('#menuType').val(),
        VesselId: $('#vesselId').val(),
        Title: $('#selectedTitle').val()
    }
    $.ajax({
        url: "/Dashboard/GetApprovalSummary",
        type: "POST",
        data: input,
        dataType: "JSON",
        beforeSend: function (xhr) {
            $('.approval-list').empty();
            AddModelLoadingIndicator('#approval');
        },
        success: function (response) {
            if (!IsNullOrEmpty(response)) {
                var result = response.data;
                for (var i = 0; i < result.length; i++) {
                    var newRow = CreateApprovalTemplate(result[i], i);
                    $('.approval-list').append(newRow);
                }
            }
        },
        complete: function () {
            RemoveModelLoadingIndicator('#approval');
        }
    });
}

function CreateApprovalTemplate(item, recordNumber) {
    var element = '';
    element += '<div class="list">\
					<div class="row no-gutters">\
						<div class="col-12 col-md-12 col-lg-12 col-xl-12">\
							<div class="approvalhead">\
                            '+ item.headerNode + '\
                            </div>';

    for (var i = 0; i < item.children.length; i++) {

        if (recordNumber == 0 && i == 0) {
            //$('#approvalsListViewBtn').attr("href", "/Approval/List?ApprovalReuqest=" + item.children[0].approvalOverViewUrl);
            $('#approvalsListViewBtn').attr("href", "/Approval/List");
        }

        element += '<a class="row no-gutters text-decoration-none cursor-pointer" href="/Approval/List?ApprovalReuqest=' + item.children[i].approvalListUrl + '">\
						<span class="col-10 col-md-10 col-lg-10 col-xl-10">\
                            <div class="approvalsubhead"> \
                                ' + item.children[i].node + '\
                            </div >\
                        </span>\
						<div class="col-2 col-md-2 col-lg-2 col-xl-2 text-left approvalcount">\
							<span class="" >'+ item.children[i].count + '</span>\
						</div>\
					</a>';
    }
    element += '        </div>\
					</div>\
				</div>';
    return element;
}

function formatMobileVesselSearchResult(result) {
    if (result.loading) {
        return "Searching...";
    }

    var $result = result.vesselName;

    if (($(window).width() < MobileScreenSize) && result != undefined) {
        $result = $('<div class="row select2-row">' +
            //'<div class="col-sm-4"><b>Coy Id : </b>' + result.accountingCompanyId + '</div>' +
            '<div class="col-sm-8">' + result.vesselName + '</div>'
            + '</div>');
    }
    return $result;
}

function formatMobileVesselSearchRepoSelection(repo) {
    return repo.text;
}


function CurrentVoyageAgentDetails(urlRequest, portname) {
    $('#VoyageAgentContentsHtml').html("");
    $('#VoyageAgentCount').html("0");
    $('#VoyagePortName').text("");
    $('#ModalVoyageAgentDetails').modal("show");

    $('#ModalVoyageAgentDetails').block({
        message: $(" " + loadercontent),
    });

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
            $('#ModalVoyageAgentDetails').unblock();

            if (($(window).width() < MobileScreenSize)) {
                $('body').addClass('fixedmodalbodyagent');
                $('#ModalVoyageAgentDetails').addClass('fixedmodalbodyagent');
                var windowheightagent = $(window).height();
                var modalheaderagent = $('#ModalVoyageAgentDetails .modal-header').outerHeight();
                $("#ModalVoyageAgentDetails .scroller").css({
                    "max-height": windowheightagent - modalheaderagent - 60
                });

                $("#ModalVoyageAgentDetails").on('hidden.bs.modal', function (e) {
                    $('body').removeClass('fixedmodalbodyagent');
                    $('#ModalVoyageAgentDetails').removeClass('fixedmodalbodyagent');
                });
            }
            else {
                var windowheightagent = $(window).height();
                var modalheaderagent = $('#ModalVoyageAgentDetails .modal-header').outerHeight();
                $("#ModalVoyageAgentDetails .scroller").css({
                    "max-height": windowheightagent - modalheaderagent - 100
                });
            }
        }
    });
}

function AddCommunicationLoadingIndicator(selector) {
    $(selector).block({
        message: '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
            '<div class="loader  mx-auto">' +
            '<div class="ball-clip-rotate">' +
            '<div></div>' +
            '</div>' +
            '</div>' +
            '</div>',

        onBlock: function () {
            $(".blockElement").addClass("blockMsg");
            $(".blockElement").removeClass("undefined");
        }
    });
}

function DeleteChannelById(channelId, isSaveAsDraft) {
    $.ajax({
        url: "/Dashboard/DeleteChannelById",
        type: "GET",
        dataType: "JSON",
        data: {
            "channelId": channelId
        },
        success: function (data) {
            if (data != null) {

                if (isSaveAsDraft) {
                    ToastrAlert("success", "Draft deleted successfully.");
                }
                else {
                    ToastrAlert("success", "You left the discussion successfully.");
                }

                UpdateUnreadMessagesOnDashboard();
            }
            else {
                ToastrAlert("error", "Something went wrong!");
            }
        }
    });
}

function BindSentinelValue(vesselid,parent) {
    $.ajax({
        url: "/Dashboard/GetSentinelValue",
        type: "GET",
        dataType: "JSON",
        data: {
            "VesselId": vesselid
        },
        success: function (data) {
            if (data != null) {
                var state = 1;
                if (data.sentinelTotalValueColor == Amber) {
                    $(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-orange.png");
                }
                else if (data.sentinelTotalValueColor == Green) {
                    $(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-green.png");
                }
                else if (data.sentinelTotalValueColor == Red) {
                    $(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-red.png");
                }
                else {
                    $(parent).find('.imgSentinelShield').attr("src", "/images/Sentinel/shield-grey.png");
                    $(parent).find('.aSentinelValue').removeAttr("href");
                    state = 2;
                }
                $(parent).find('.pSentinelScore').text(ConvertDecimalNumberToString(data.sentinelTotalValue,1,state,1));
            }
        }
    });
}