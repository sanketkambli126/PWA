import { AjaxError, GetCookie, ToastrAlert, IsNullOrEmptyOrUndefined } from "../common/utilities.js";


$(document).ready(function () {
	AjaxError();
    $("#btnSend").on('click', function () {
        //call the api
		let message = $("#messageText").val();
		$("#messageText").val('');
		
		if (!IsNullOrEmptyOrUndefined(message)) {
			GetChannelMessageTest(message);
		}
	});

	$(document).on('keypress', function (e) {
		if (e.which == 13) {
			e.preventDefault();
			if ($("#messageText").is(":focus")) {
				if ($.trim($("#messageText").val()) != "") {
					let message = $("#messageText").val();
					$("#messageText").val('');
					GetChannelMessageTest(message);
				}
			}
		}
	});

});

function GetChannelMessageTest(message) {
	$.ajax({
		url: "/Notification/GetChannelMessageTest",
		dataType: "JSON",
		data: {
			"message": message
		},
		success: function (data) {
			console.log("message sent status: " + data);
		}
	});
}