import { DashboardFullMapPageKey } from '../common/constants.js';
import { GetCookie, ToastrAlert, BackButton, FleetTrackerCloseButton } from "../common/utilities.js";

$(document).ready(function () {
    BackButton(DashboardFullMapPageKey, false);
    FleetTrackerCloseButton(DashboardFullMapPageKey, false);

    //height
    var divHeight = $('.mapheight').height();
    $('.mapfullscreen').css('min-height', divHeight - 20);
    var divHeight2 = $('.mapfullscreen').height();
    $('.mapfullscreen iframe').css('min-height', divHeight2);

    if (($(window).width() > 200) & ($(window).width() < 991)) {
        $('body').addClass('block-divs-ipad');
        var divHeight = $('.mapheight').height();
        $('.mapfullscreen').css('min-height', divHeight - 20);
        var divHeight2 = $('.mapfullscreen').height();
        $('.mapfullscreen iframe').css('min-height', divHeight2);
    }
});