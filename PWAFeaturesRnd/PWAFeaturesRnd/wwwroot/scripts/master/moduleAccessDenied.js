import { MobileScreenSize } from '../common/constants.js'

$(document).ready(function () {

	var isMobileScreen = false
	if ($(window).width() < MobileScreenSize) {
		isMobileScreen = true
	}

	$('.back').click(function () {
		if (isMobileScreen) {
			window.location.replace("/Dashboard/VesselDetailsMobile/");
		}
		else {
			window.location.replace("/Dashboard");
		}
	});
})