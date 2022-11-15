import "block-ui";
import toastr from "toastr";
import moment from "moment";
import { MobileScreenSize } from '../common/constants.js'
import { MessageCateory_General } from "../common/constants.js"
import { UpdateNotesTab } from "../common/notesUtilities";

export function AddLoadingIndicator() {
	$(document).ajaxStart(function () {
		$.blockUI({
			message: $(".loading-indicator"),
			onBlock: function () {
				$(".blockMsg").addClass("custom-blockUI");
				$(".blockMsg").addClass("blockUI-z-index");
				$(".blockOverlay").addClass("blockUIOverlay-z-index");

			}
		});
	});
}

export function AjaxError() {
	$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
		if (jqxhr.status === 401) {
			console.log(jqxhr.status);
		}
		else if (jqxhr.status !== 0) {
			try {
				var data = JSON.parse(jqxhr.responseText);
				ErrorLog(xhr, status, error);
			}
			catch (e) {
				//This is temprory solution.
				let findValue = "<title>Login Portal</title>";
				var incStr = jqxhr.responseText.includes(findValue, 0);
				if (incStr == true) {
					window.location.reload();
				}
			}
		}
	});
}


export function RemoveLoadingIndicator() {
	$(document).ajaxStop($.unblockUI);
}

export function GetCookie(name) {
	var cookieArr = document.cookie.split(";");

	for (var i = 0; i < cookieArr.length; i++) {
		var cookiePair = cookieArr[i].split("=");

		if (name == cookiePair[0].trim()) {
			return decodeURIComponent(cookiePair[1]);
		}
	}
	return null;
}


export function SetCookie(key, value) {
	console.log("document.cookie ", document.cookie)
	var cookie = document.cookie;
	console.log("key and value ", key + "=" + value + ";")
	cookie = cookie + key + "=" + value + ";"
	console.log("cookie ", cookie)
	document.cookie = cookie;
	console.log("document.cookie ", document.cookie)
}

export function ToastrAlert(type, message) {
	if (type == "success") {

		toastr.success(message);
	}
	else if (type == "validate") {
		toastr.options = {
			"closeButton": true,
			"timeOut": "0",
			"extendedTimeOut": "0"
		};
		toastr.error(message);
	}
	else if (type == "error") {
		//toastr.options = {
		//    "closeButton": true,
		//    "timeOut": "0",
		//    "extendedTimeOut": "0"
		//};
		//toastr.error(message);
		console.log(message);
	}
}


export function ConvertDecimalNumberToString(number, toFixedValue, state, minFractionDigit) {
	var numToString = "";
	if (number != null && number != '' && number != 'undefined') {
		numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
			{
				minimumFractionDigits: minFractionDigit
			});
	}
	else {
		if (state == 1) {
			//when expected return is 0.00
			numToString = "0.00";
		}
		else if (state == 2) {
			//when blank space is expected
			numToString = "";
		}
	}
	return numToString;
}

export function ConvertValueToPercentage(value, max) {
	return parseFloat((value * 100) / max).toFixed(2);
}

export function base64ToArrayBuffer(base64) {
	var binaryString = window.atob(base64);
	var binaryLen = binaryString.length;
	var bytes = new Uint8Array(binaryLen);
	for (var i = 0; i < binaryLen; i++) {
		var ascii = binaryString.charCodeAt(i);
		bytes[i] = ascii;
	}
	return bytes;
}

export function saveByteArray(reportName, byte, fileType) {
	var blob = new Blob([byte], { type: fileType });
	var link = document.createElement('a');
	link.href = window.URL.createObjectURL(blob);
	var fileName = reportName;
	link.download = fileName;
	link.click();
	URL.revokeObjectURL(link.href);
};


export function saveOpenByteArray(reportName, byte, fileType) {

	var extensions = ['pdf', 'bmp', 'csv', 'gif', 'jpeg', 'jpg', 'png'];

	var blob = new Blob([byte], { type: fileType });
	var link = document.createElement('a');
	var fileName = reportName;
	var ext = fileName.substr((fileName.lastIndexOf('.') + 1));
	link.href = window.URL.createObjectURL(blob);

	console.log('ext' + ext);
	console.log('ext' + link.href);

	//if ($.inArray(ext.toLowerCase(), extensions) >= 0)
	//{
	//	window.open(link.href, '_blank');
	//}

	link.download = fileName;
	link.click();
	URL.revokeObjectURL(link.href);
};

export function FormatDate(data) {
	if (data == null) return "";
	var date = new Date(data);
	return moment(date).format("D MMM YYYY");
};

export function txtReadMore(clsSelector, settings) {

	var config = {
		showChars: 100,
		ellipsesText: "...",
		moreText: "Show More",
		lessText: "Show Less"
	};

	if (settings) {
		$.extend(config, settings);
	}

	$('.' + clsSelector).each(function () {
		var content = $(this).html();

		if (content.length > config.showChars) {

			var c = content.substr(0, config.showChars);
			var h = content.substr(config.showChars - 1, content.length - config.showChars);

			var html = c + '<span class="moreellipses">' + config.ellipsesText + '&nbsp;</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="readmorelink">' + config.moreText + '</a></span>';

			$(this).html(html);
			$(".morecontent span").hide();
		}

	});

	$(".readmorelink").on("click", function () {
		if ($(this).hasClass("less")) {
			$(this).removeClass("less");
			$(this).html(config.moreText);
		} else {
			$(this).addClass("less");
			$(this).html(config.lessText);
		}
		$(this).parent().prev().toggle();
		$(this).prev().toggle();
		return false;
	});
}

export function headerReadMore(clsSelector, headerSelector, characterLimit) {
	var config = {
		showChars: 55,
		ellipsesText: "...",
		moreText: "Show More",
		lessText: "Show Less"
	};
	if (!IsNullOrEmpty(characterLimit)) {
		config.showChars = characterLimit;
	}

	let childs = $('.' + clsSelector).children();

	let headerReached = false;
	let headerInfo = [];
	let contentInfo = [];
	for (var i = 0; i < childs.length; i++) {
		if (headerReached) {
			contentInfo.push(childs[i]);
		} else {
			headerInfo.push(childs[i]);
		}
		if ($(childs[i]).hasClass(headerSelector)) {
			headerReached = true;
		}
	}

	$('.' + clsSelector).empty();

	let headerTexts = 0;

	for (var i = 0; i < headerInfo.length; i++) {
		headerTexts += $(headerInfo[i]).text().trim();
	}
	if (config.showChars < headerTexts.length) {
		let extra = headerTexts.length - config.showChars;
		let lastHeader = $(headerInfo[headerInfo.length - 1]).text();
		let extraText = lastHeader.substring(lastHeader.length - extra, lastHeader.length);

		let lastHeaderFinaltext = lastHeader.substring(0, lastHeader.length - extra);
		let extraTextSpan = '<span class="extraText12" style="display: none;">' + extraText + '</span>';
		$(headerInfo[headerInfo.length - 1]).text(lastHeaderFinaltext);
		$(headerInfo[headerInfo.length - 1]).append(extraTextSpan);
	}

	for (var i = 0; i < headerInfo.length; i++) {
		$('.' + clsSelector).append(headerInfo[i]);
	}

	$('.' + clsSelector).append('<span class="moreellipses">' + config.ellipsesText + '</span>');


	for (var i = 0; i < contentInfo.length; i++) {
		$('.' + clsSelector).append('<div class="content">' + $(contentInfo[i]).html() + '</div>');
	}
	$('.' + clsSelector).append('<a href="" class="morelink d-block">' + config.moreText + '</a>');

	$('.content').hide();

	$(".morelink").on("click", function () {
		if ($(this).hasClass("less")) {
			$(this).removeClass("less");
			$(this).html(config.moreText);
		} else {
			$(this).addClass("less");
			$(this).html(config.lessText);
		}
		$(".moreellipses").toggle();
		$(".extraText12").toggle();
		$(this).parent().prev().toggle();
		$(this).prev().toggle();
		return false;
	});
}

export function headerReadMoreFindings(clsSelector, headerSelector, characterLimit) {
	var config = {
		showChars: 55,
		ellipsesText: "...",
		moreText: "Show More",
		lessText: "Show Less"
	};

	if (!IsNullOrEmpty(characterLimit)) {
		config.showChars = characterLimit;
	}

	let childs = $('.' + clsSelector).children();

	let headerReached = false;
	let headerInfo = [];
	let contentInfo = [];
	for (var i = 0; i < childs.length; i++) {
		if (headerReached) {
			contentInfo.push(childs[i]);
		} else {
			headerInfo.push(childs[i]);
		}
		if ($(childs[i]).hasClass(headerSelector)) {
			headerReached = true;
		}
	}

	$('.' + clsSelector).empty();

	let headerTexts = '';
	let headerChildren = $(headerInfo[0]).children();

	for (var i = 0; i < headerChildren.length; i++) {
		headerTexts += $(headerChildren[i]).text().trim();
	}
	if (config.showChars < headerTexts.length) {
		let extra = headerTexts.length - config.showChars;
		let lastHeader = $(headerChildren[headerChildren.length - 1]).text();
		let extraText = lastHeader.substring(lastHeader.length - extra, lastHeader.length);

		let lastHeaderFinaltext = lastHeader.substring(0, lastHeader.length - extra);
		let extraTextSpan = '<span class="extraText12" style="display: none;">' + extraText + '</span>';
		$(headerChildren[headerChildren.length - 1]).text(lastHeaderFinaltext);
		$(headerChildren[headerChildren.length - 1]).append(extraTextSpan);
	}

	for (var i = 0; i < headerChildren.length; i++) {
		$('.' + clsSelector).append(headerChildren[i]);
	}

	$('.' + clsSelector).append('<span class="moreellipses">' + config.ellipsesText + '</span>');


	for (var i = 0; i < contentInfo.length; i++) {
		$('.' + clsSelector).append('<div class="content">' + $(contentInfo[i]).html() + '</div>');
	}
	$('.' + clsSelector).append('<a href="" class="morelink d-block">' + config.moreText + '</a>');

	$('.content').hide();

	$(".morelink").on("click", function () {
		if ($(this).hasClass("less")) {
			$(this).removeClass("less");
			$(this).html(config.moreText);
		} else {
			$(this).addClass("less");
			$(this).html(config.lessText);
		}
		$(".moreellipses").toggle();
		$(".extraText12").toggle();
		$(this).parent().prev().toggle();
		$(this).prev().toggle();
		return false;
	});
}

export function headerReadMoreMulti(clsSelector, headerSelector, characterLimit) {
	if (!readMoreExists(clsSelector)) {

		var config = {
			showChars: 55,
			ellipsesText: "...",
			moreText: "Show More",
			lessText: "Show Less"
		};
		if (!IsNullOrEmpty(characterLimit)) {
			config.showChars = characterLimit;
		}

		let childs = $('.' + clsSelector).children();

		let headerReached = false;
		let headerInfo = [];
		let contentInfo = [];
		for (var i = 0; i < childs.length; i++) {
			if (headerReached) {
				contentInfo.push(childs[i]);
			} else {
				headerInfo.push(childs[i]);
			}
			if ($(childs[i]).hasClass(headerSelector)) {
				headerReached = true;
			}
		}

		$('.' + clsSelector).empty();

		let headerTexts = 0;

		for (var i = 0; i < headerInfo.length; i++) {
			headerTexts += $(headerInfo[i]).text().trim();
		}
		if (config.showChars < headerTexts.length) {
			let extra = headerTexts.length - config.showChars;
			let lastHeader = $(headerInfo[headerInfo.length - 1]).text();
			let extraText = lastHeader.substring(lastHeader.length - extra, lastHeader.length);

			let lastHeaderFinaltext = lastHeader.substring(0, lastHeader.length - extra);
			let extraTextSpan = '<span class="extraText12" style="display: none;">' + extraText + '</span>';
			$(headerInfo[headerInfo.length - 1]).text(lastHeaderFinaltext);
			$(headerInfo[headerInfo.length - 1]).append(extraTextSpan);
		}

		for (var i = 0; i < headerInfo.length; i++) {
			$('.' + clsSelector).append(headerInfo[i]);
		}

		$('.' + clsSelector).append('<span class="moreellipses">' + config.ellipsesText + '</span>');


		for (var i = 0; i < contentInfo.length; i++) {
			$('.' + clsSelector).append('<div class="content">' + $(contentInfo[i]).html() + '</div>');
		}
		$('.' + clsSelector).append('<a href="" class="morelink' + headerSelector + ' d-block morelink">' + config.moreText + '</a>');

		$('.content').hide();

		$(".morelink" + headerSelector).on("click", function (e) {
			if ($(this).hasClass("less")) {
				$(this).removeClass("less");
				$(this).html(config.moreText);
			} else {
				$(this).addClass("less");
				$(this).html(config.lessText);
			}
			$(".moreellipses").toggle();
			$(".extraText12").toggle();
			$(this).parent().prev().toggle();
			$(this).prev().toggle();
			return false;
		});
	}
}

function readMoreExists(headerClass) {
	let childs = $('.' + headerClass).children();
	let minimiseHeader = false;
	for (var i = 0; i < childs.length; i++) {
		if ($(childs[i]).hasClass('moreellipses')) {
			minimiseHeader = true;
		}
	}
	return minimiseHeader;
}

export function GetRoleRights(data) {
    var roleRights;
    $.ajax({
        url: "/Permission/PostGetRoleRights",
        type: "POST",
        "data": {
            controlIds: data
        },
        "datatype": "JSON",
        async: false, //Required for execution to wait for role rights data. If it does not wait, it will cause problem when removing elements for which user does not have permission.
        success: function (data) {
            roleRights = data;
        }
    });
    return roleRights;
};

export function GetRoleRightsAsync(data, resolve) {
    $.ajax({
        url: "/Permission/PostGetRoleRights",
        type: "POST",
        "data": {
            controlIds: data
        },
        "datatype": "JSON",
        success: function (data) {
            if (resolve != null && resolve != 'undefined') {
                resolve(data);
            }
        }
    });
};

export function MobileTab_Overview() {
	$(".tab-box-1").hide();
	$(".tab-box-2").show();
	$(".tab-1").removeClass("active");
	$(".tab-2").addClass("active");
	$(window).scrollTop(0);
}

export function MobileTab_List() {
	$(".tab-box-1").show();
	$(".tab-box-2").hide();
	$(".tab-2").removeClass("active");
	$(".tab-1").addClass("active");
	$(window).scrollTop(0);
}

export function Mobile_Tabs() {
	if ($(".tab-1").hasClass('active')) {
		$('#divDatePickermobilehide').removeClass('d-inline-block');
		$('#divDatePickermobilehide').addClass('d-none');
		$('#divfilterhide').removeClass('d-inline-block');
		$('#divfilterhide').addClass('d-none');
		$('#divlegendhide').removeClass('d-inline-block');
		$('#divlegendhide').addClass('d-none');
		$("#search-mobile").removeClass('search-filter-top');
	}
	else {
		$('#divDatePickermobilehide').removeClass('d-none');
		$('#divDatePickermobilehide').addClass('d-inline-block');
		$('#divfilterhide').removeClass('d-none');
		$('#divfilterhide').addClass('d-inline-block');
		$('#divlegendhide').removeClass('d-none');
		$('#divlegendhide').addClass('d-inline-block');

		$("#search-mobile").addClass('search-filter-top');
	}
}

export function SetHeaderMargin() {
	$(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));
}

export function createFlatRadioButton(id, name, value, description) {

	var FlatRadioButton = '<div class="custom-radio custom-control">' +
		'<input class="custom-control-input" id="' + id + '" name="' + name + '" type="radio" value="' + value + '" data-description="' + description + '">' +
		'<label class="custom-control-label" for="' + id + '">' + description + '</label></div>';

	return FlatRadioButton;
}

export function GetDashboardColorMap() {
	var colorMap = new Map();
	colorMap.set(0, { color: "green-panel", textColor: "green-panel-color" });
	colorMap.set(1, { color: "orange-panel", textColor: "orange-panel-color" });
	colorMap.set(2, { color: "red-panel", textColor: "red-panel-color" });
	colorMap.set(3, { color: "blue-panel", textColor: "blue-panel-color" });
	colorMap.set(4, { color: "purple-panel", textColor: "purple-panel-color" });
	colorMap.set(5, { color: "gray-panel", textColor: "gray-panel-color" });
	return colorMap;
}

export function GetStringNullOrWhiteSpace(data) {
	if (data !== null && data !== '') {
		return data;
	}
	return '';
}

//intended to use with api calls
export function IsNullOrEmpty(data) {
	return data === null || data === '';
}

//intended to use within javascript
export function IsNullOrEmptyOrUndefined(data) {
	return IsNullOrEmpty(data) || data === 'undefined';
}

//can use with api values 
//param jqueryObject can be either a string value or the jqueryObject
export function SetValueElseDefault(jqueryObject, value, defaultValue) {
	let localDefault = '-';
	if (!IsNullOrEmptyOrUndefined(defaultValue)) {
		localDefault = defaultValue;
	}
	if (IsNullOrEmptyOrUndefined(value)) {
		$(jqueryObject).text(localDefault);
	} else {
		$(jqueryObject).text(value);
	}
}

export function BackButton(keyName, isFromList, beforeCompleteCallback, afterCompleteCallback) {
	var isMobileScreen = false
	if ($(window).width() < MobileScreenSize) {
		isMobileScreen = true
	}
	$('.back').click(function () {
		$.ajax({
			url: "/Base/GetSourceURL",
			type: "POST",
			dataType: "JSON",
			data: {
				"pageKey": keyName
			},
			beforeSend: function (xhr) {
				if (beforeCompleteCallback != null && beforeCompleteCallback != 'undefined') {
					beforeCompleteCallback();
				}
			},
			success: function (data) {
				if (data != null) {
					if (isFromList) {
						if (isMobileScreen) {
							window.location.replace("/Dashboard/VesselDetailsMobile/");
						}
						else {
							window.location.replace("/Dashboard");
						}
					}
					else {
						window.location.replace(data);
					}
				}
			},
			complete: function () {
				if (afterCompleteCallback != null && afterCompleteCallback != 'undefined') {
					afterCompleteCallback();
				}
			}
		});
	});
}

export function FleetTrackerCloseButton(keyName, isFromList) {
	var isMobileScreen = false
	if ($(window).width() < MobileScreenSize) {
		isMobileScreen = true
	}
	$('.fleetTrackerClose').click(function () {
		$.ajax({
			url: "/Base/GetSourceURL",
			type: "POST",
			dataType: "JSON",
			data: {
				"pageKey": keyName
			},
			success: function (data) {
				if (data != null) {
					if (isFromList) {
						if (isMobileScreen) {
							window.location.replace("/Dashboard/VesselDetailsMobile/");
						}
						else {
							window.location.replace("/Dashboard");
						}
					}
					else {
						window.location.replace(data);
					}
				}
			}
		});
	});
}
export function AddClassIfAbsent(jqueryObject, className) {
	if (!$(jqueryObject).hasClass(className)) {
		$(jqueryObject).addClass(className);
	}
}

export function RemoveClassIfPresent(jqueryObject, className) {
	if ($(jqueryObject).hasClass(className)) {
		$(jqueryObject).removeClass(className);
	}
}

export function ReplaceClass(jqueryObject, removeClass, addClass) {
	RemoveClassIfPresent(jqueryObject, removeClass);
	AddClassIfAbsent(jqueryObject, addClass);
}

export function AddModelLoadingIndicator(selector) {
	var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
		'<div class="loader  mx-auto">' +
		'<div class="ball-clip-rotate">' +
		'<div></div>' +
		'</div>' +
		'</div>' +
		'</div>';

	$(selector).block({
		message: $(" " + loadercontent),
		onBlock: function () {
			$(".blockElement").addClass("blockMsg");
			$(".blockElement").removeClass("undefined");
		}
	});
}

export function RemoveModelLoadingIndicator(selector) {
	$(selector).unblock();
}

export function BlockLinkAccess(selector) {
	$(selector).removeAttr("href");
	AddClassIfAbsent(selector, 'blockLink');
}

export function GetUserModule(successCallback) {
    $.ajax({
        url: "/Base/GetUserModulesFromSession",
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            if (!IsNullOrEmptyOrUndefined(successCallback)) {
                successCallback(data);
            }
        }
    });
}

export function IsModuleAccessible(moduleName, ifModuleNotAccessible) {
	GetUserModule(function (modules) {
		var isAccessible = modules.includes(moduleName);
		if (!IsNullOrEmptyOrUndefined(ifModuleNotAccessible) && !isAccessible) {
			ifModuleNotAccessible();
		}
	});
}

export function SetSessionDetailsForTimeZone() {
	var d = new Date()
	var timezoneOffset = d.getTimezoneOffset();
	$.ajax({
		url: "/Dashboard/SetTimeZoneSession",
		type: "POST",
		dataType: "html",
		data: {
			"timeZoneOffSet": timezoneOffset
		}
	});
}

export function IsNullOrEmptyOrUndefinedLooseTyped(data) {
	return data == null || data == '' || data == 'undefined';
}

export function RecordLevelMessage(messageDetailsJSON) {
	if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
		$('#hdnmessageDetailsJSON').val(messageDetailsJSON);

		RemoveClassIfPresent("#aBaseRecordLevelMessage", "d-none");

	}
}

export function encryptMessage(message, key) {
	var encryptedMessage = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(message), key);
	return encryptedMessage.toString();
}

export function decryptMessage(encryptedMessage, key) {
	var decryptedBytes = CryptoJS.AES.decrypt(encryptedMessage, key);
	var decryptedMessage = decryptedBytes.toString(CryptoJS.enc.Utf8);
	return decryptedMessage;
}

export function encryptObject(obj, key) {
	var stringifiedObj = JSON.stringify(obj);
	return encryptMessage(stringifiedObj, key);
}

export function decryptObject(encryptedObj, key) {
	var decryptedStringifiedObject = decryptMessage(encryptedObj, key);
	return JSON.parse(decryptedStringifiedObject);
}

export function GetLastReferrer() {
	let pathname = window.location.pathname
	let refererWithoutParameterRequest = pathname.split("?")[0];
	let referers = refererWithoutParameterRequest.split("/");
	let lastReferer = '';
	if (referers.length > 0) {
		lastReferer = referers[referers.length - 1];
		if (lastReferer === '') {
			lastReferer = referers[referers.length - 2];
		}
	}
	return lastReferer;
}

export function GetRecordDiscussionAndNotesCount(requestObject, successCallback, completeCallback) {
    var messageDetailsObj = JSON.parse(requestObject);
    $.ajax({
        url: "/Dashboard/GetRecordLevelDiscussionAndNotesCounts",
        type: "POST",
        "datatype": "JSON",
        data: messageDetailsObj,
        success: function (data) {
            if (!IsNullOrEmptyOrUndefinedLooseTyped(successCallback)) {
                successCallback(data);
            }
        },
        complete: function () {
            if (!IsNullOrEmptyOrUndefinedLooseTyped(completeCallback)) {
                completeCallback();
            }
        }
    });
}

export function InitializeDiscussionAndNoteClickEvents(messageDetailsJSON) {
	$(".noteAnchorOnClick").on('click', function () {
		$("#hdnNotesMessageDetails").val(messageDetailsJSON);
		UpdateNotesTab();
		$(".notes-sidebar-open").toggleClass("settings-open");
		$("#tabs a[href='#tabs1']")[0].click();
	});

	$(".discussionAnchorOnClick").on('click', function () {
		NavigateToNotification(messageDetailsJSON);
	});

	document.addEventListener("updateCount", function () {
		let messageDetailsJSON = $("#MessageDetailsJSON").val();
		if (!IsNullOrEmptyOrUndefinedLooseTyped(messageDetailsJSON)) {
			GetDiscussionNotesCount(messageDetailsJSON);
		}
	});
}

export function NavigateToNotification(messageDetailsJSON, channelId = 0) {

    $.ajax({
        url: "/Common/GetJwtAccessToken",
        type: "POST",
        "datatype": "json",
        success: function (token) {
            if (token != null) {
                if (screen.width < MobileScreenSize) {
                    // mobile

					var obj = { "ChannelId": channelId, "MessageDetailsJSON": messageDetailsJSON, "NotificationJwtToken": token, "UserId": GetCookie("UserId"), "UserEmailId": GetCookie("EmailId"), "ApplicationId": GetCookie("ApplicationId"), "NavigationURL": GetCookie("ClientPortalPWAURL") }

					var redirectURL = window.location.protocol + "//" + window.location.host + '/Dashboard/NotificationChatView/';

                    var form = $("<form id='formNotificationChatView' action='" + redirectURL + "' method='post'><input type='text' name='searchRequest' value='"
                        + JSON.stringify(obj) + "' /></form>");
                    $('body').append(form);
                    form.submit();
                }
                else {
					var obj = { "ChannelId": channelId, "MessageDetailsJSON": messageDetailsJSON, "NotificationJwtToken": token, "UserId": GetCookie("UserId"), "UserEmailId": GetCookie("EmailId"), "ApplicationId": GetCookie("ApplicationId"), "NavigationURL": GetCookie("ClientPortalPWAURL") }

                    var redirectURL = window.location.protocol + "//" + window.location.host + '/Dashboard/NotificationChatView/';

                    var form = $("<form id='formNotificationChatView' action='" + redirectURL + "' method='post'><input type='text' name='searchRequest' value='"
                        + JSON.stringify(obj) + "' /></form>");
                    $('body').append(form);
                    form.submit();
                }               
                
            }
        }
    });    

}

export function NavigateToNotificationDashboard(messageDetailsJSON, channelId = 0, isDraft) {
    $.ajax({
        url: "/Common/GetJwtAccessToken",
        type: "POST",
        "datatype": "json",
        success: function (token) {
            if (token != null) {
                if (screen.width < MobileScreenSize) {
                    // mobile

                    //mobile
					var obj = { "ChannelId": channelId, "MessageDetailsJSON": messageDetailsJSON, "NotificationJwtToken": token, "UserId": GetCookie("UserId"), "UserEmailId": GetCookie("EmailId"), "ApplicationId": GetCookie("ApplicationId"), "NavigationURL": GetCookie("ClientPortalPWAURL") }

                    var redirectURL = window.location.protocol + "//" + window.location.host + '/Dashboard/';
                    if (isDraft == true || isDraft == 'true') {
                        redirectURL = redirectURL + 'NotificationChatDiscussionView/'
                    }
                    else {
                        redirectURL = redirectURL + 'NotificationChatDetailView/'
                    }

                    var form = $("<form id='formNotificationChatView' action='" + redirectURL + "' method='post'><input type='text' name='searchRequest' value='"
                        + JSON.stringify(obj) + "' /></form>");
                    $('body').append(form);
                    form.submit();
                }
                else {
					var obj = { "ChannelId": channelId, "MessageDetailsJSON": messageDetailsJSON, "NotificationJwtToken": token, "UserId": GetCookie("UserId"), "UserEmailId": GetCookie("EmailId"), "ApplicationId": GetCookie("ApplicationId"), "NavigationURL": GetCookie("ClientPortalPWAURL") }

                    var redirectURL = window.location.protocol + "//" + window.location.host + '/Dashboard/NotificationChatView/';

                    var form = $("<form id='formNotificationChatView' action='" + redirectURL + "' method='post'><input type='text' name='searchRequest' value='"
                        + JSON.stringify(obj) + "' /></form>");
                    $('body').append(form);
                    form.submit();
                }

            }
        }
    });    

}


export function GetDiscussionNotesCount(messageDetailsJSON, callback) {
	GetRecordDiscussionAndNotesCount(messageDetailsJSON, function (data) {
		$(".baseDiscussionCountCommon").text(data.channelCount);
		$(".baseNotesCountCommon").text(data.notesCount);
		ShowCountButtonBasedOnCount(parseInt(data.channelCount), parseInt(data.notesCount), callback);
	});
}

function ShowCountButtonBasedOnCount(channelCount, notesCount, callBack) {
	if (channelCount > 0 || notesCount > 0) {
		RemoveClassIfPresent('#baseCountButton', 'd-none');
		AddClassIfAbsent('#baseCountButton', 'd-md-inline-block d-none');

		if (notesCount > 0) {
			//count inside the button
			let div = $("#baseNotesCount").parents('div')[0];
			RemoveClassIfPresent(div, 'd-none');

			//dropdown
			let anchorDropdown = $("#baseNotesCountDropdown").parents('a')[0];
			RemoveClassIfPresent(anchorDropdown, 'd-none');

			//mobile
			let mobileElement = $("#baseNotesMobile");
			RemoveClassIfPresent(mobileElement, 'd-none');
		}

		if (channelCount > 0) {
			let div = $("#baseChannelCount").parents('div')[0];
			RemoveClassIfPresent(div, 'd-none');

			let anchorDropdown = $("#baseChannelCountDropdown").parents('a')[0];
			RemoveClassIfPresent(anchorDropdown, 'd-none');

			let mobileElement = $("#baseDiscussionMobile");
			RemoveClassIfPresent(mobileElement, 'd-none');
		}

		if (!IsNullOrEmptyOrUndefinedLooseTyped(callBack)) {
			callBack();
		}
	}

	let mobileParentElement = $("#baseDiscussionMobile").parents('.dropdown');
	let children = mobileParentElement.find('li');
	if (children.length <= 2) {
		if (notesCount == 0 && channelCount == 0) {
			RemoveClassIfPresent(mobileParentElement, 'd-block');
			AddClassIfAbsent(mobileParentElement, 'd-none');
		} else {
			AddClassIfAbsent(mobileParentElement, 'd-block');
			RemoveClassIfPresent(mobileParentElement, 'd-none');
		}
	}
}

export function GetAttachmentViewUtility(obj) {
	let ettId = obj.ettId;
	let fileName = obj.description + obj.fileExtension;
	let row = '<div class="attached-files attachmentClick" data-seq="' + obj.sequence + '" data-id="' + ettId + '" data-desc="' + obj.description + '" data-type="' + obj.fileExtension + '">' + GetAttachmentIconUtility(obj.fileExtension) + '<div class="attachment-text">' + fileName + '</div></div >';
	return row;
}

export function GetAttachmentIconUtility(extension) {
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

export function AttachmentTemplateUtility(attachemtIcon, fileName, sequence, attachedTo, id) {
	var $row;
	$row = $('<div class="attached-files" data-ettid="' + id + '" data-sequence="' + sequence + '" data-attachedTo="' + attachedTo + '">'
		+ attachemtIcon +
		'<div class="attachment-text">' + fileName + '</div >\
				<div class="attachment-close">×</div>\
			</div >');

	return $row;
}

function GetUnreadMessages() {
    $.ajax({
        url: "/Dashboard/GetUnreadMessages",
        type: "GET",
        dataType: "JSON",
        beforeSend: function (xhr) {
            $('.message-list').empty();
            $("#unreadMessageCountDashboard").text(0);
            AddModelLoadingIndicator('#messages');
        },
        success: function (data) {
            let lst = data.data;

			if (lst != null && lst.length > 0) {
				for (var i = 0; i < lst.length; i++) {
					let msg = lst[i];
					var newRow = CreateUnreadMessageTemplate(msg);
					$('.message-list').append(newRow);
					$('[data-toggle="tooltip"]').tooltip();
				}
			}
		},
		complete: function () {
			RemoveModelLoadingIndicator('#messages');
			$('[data-toggle="tooltip"]').tooltip();
		},
	});
}

export function GetChatNotesBaseButtons(id, chatCount, notesCount, messageDetailsJSON) {
	var chatDisplay = chatCount > 0 ? '' : 'd-none';
	var notesDisplay = notesCount > 0 ? '' : 'd-none';
	var messageDetailsJSONstr = encodeURIComponent(JSON.stringify(messageDetailsJSON));

	var baseButtons = '<div id = "baseCountButton' + id + '" class="discussion-dropdown">' +
		'<div class="dropdown">' +
		'<button class="btn dropdown-toggle p-1" type="button" id="dropdowndiscussion' + id + '" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
		'<div class="discussion-drop ' + chatDisplay + '"><img style="height:16px;" src="/images/discussion-drop.png" /><span id="baseChannelCount' + id + '" class="baseDiscussionCountCommon">' + chatCount + '</span></div>' +
		'<div class="notes-drop ' + notesDisplay + '"><img style="height:15px;" src="/images/notes-drop.png" /><span id="baseNotesCount' + id + '" class="baseNotesCountCommon">' + notesCount + '</span></div>' +
		'</button>' +
		'<div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdowndiscussion' + id + '">' +
		'<a class="dropdown-item discuss-design  discussionListAnchorOnClick ' + chatDisplay + '" data-messagejson="' + messageDetailsJSONstr + '" href = "javascript:void(0);" > <img src="/images/discussion-drop-green.png" />Discussions (<span id="baseChannelCountDropdown' + id + '" class="baseDiscussionCountCommon">' + chatCount + '</span>)</a > ' +
		'<a class="dropdown-item notess-design  noteListAnchorOnClick ' + notesDisplay + '" data-messagejson="' + messageDetailsJSONstr + '" href="javascript:void(0);"><img src="/images/notes-drop-green.png" />My Notes (<span id="baseNotesCountDropdown' + id + '" class="baseNotesCountCommon">' + notesCount + '</span>)</a>' +
		'</div>' +
		'</div>' +
		'</div>';

	return baseButtons;
}

export function GetChatNotesBaseIcons(id, chatCount, notesCount, messageDetailsJSON) {
	var chatDisplay = chatCount > 0 ? '' : 'd-none';
	var notesDisplay = notesCount > 0 ? '' : 'd-none';
	var messageDetailsJSONstr = encodeURIComponent(JSON.stringify(messageDetailsJSON));

	var baseButtons = '<div class="table-discussionnotes" ><span class="discussion-drop discussionListAnchorOnClick cursor-pointer ' + chatDisplay + '" data-messagejson="' + messageDetailsJSONstr + '" data-toggle="tooltip" data-placement="top" title="Discussion">\
						<img style="height:25px;" src="/images/chat-blue.svg" />\
						<span id = "baseChannelCount' + id + '" class="baseDiscussionCountCommon" > ' + chatCount + '</span >\
					</span> ' +
		'<span class="notes-drop noteListAnchorOnClick cursor-pointer ' + notesDisplay + '" data-messagejson="' + messageDetailsJSONstr + '" data-toggle="tooltip" data-placement="top" title="Notes"><img style="height:25px;" src="/images/notes-blue.svg" /><span id="baseNotesCount' + id + '" class="baseNotesCountCommon">' + notesCount + '</span></span></div>';

	return baseButtons;
}

export function InitializeListDiscussionAndNoteClickEvents(code) {
	$(document).off('click', '.noteListAnchorOnClick');
	$(document).on('click', '.noteListAnchorOnClick', function () {
		let messageDetailsJSONstr = $(this).data("messagejson");
		var messageDetailsJSON = JSON.parse(decodeURIComponent(messageDetailsJSONstr));

		$("#hdnNotesMessageDetails").val(messageDetailsJSON);
		UpdateNotesTab();
		$(".notes-sidebar-open").toggleClass("settings-open");
		$("#tabs a[href='#tabs1']")[0].click();
	});

	$(document).off('click', '.discussionListAnchorOnClick');
	$(document).on('click', '.discussionListAnchorOnClick', function () {
		let messageDetailsJSONstr = $(this).data("messagejson");
		let messageDetailsJSON = JSON.parse(decodeURIComponent(messageDetailsJSONstr));
		NavigateToNotification(messageDetailsJSON);
	});
}

function CreateUnreadMessageTemplate(result) {
	if (result != null) {
		let vesselName = result.vesselName;
		let channelName = IsNullOrEmptyOrUndefinedLooseTyped(result.channelName) ? '' : result.channelName;
		let createDate = result.createdLocalDate;
		let vesselVisiblity = IsNullOrEmptyOrUndefinedLooseTyped(vesselName) ? 'd-none' : '';
		let isUnread = result.unreadMessageCount > 0 ? 'list-unread' : '';
		let unreadMessageCountUIElement = result.unreadMessageCount > 0 ? '<span>' + result.unreadMessageCount + '</span>' : '';
		let lastSender = IsNullOrEmptyOrUndefinedLooseTyped(result.lastSender) ? '' : result.lastSender.split(" ")[0];
		let completeMessage = IsNullOrEmptyOrUndefinedLooseTyped(result.lastMessageDescription) ? '' : result.lastMessageDescription;
		let lastMessageDescription = IsNullOrEmptyOrUndefinedLooseTyped(result.lastMessageDescription) ? '' : result.lastMessageDescription.length > 22 ? result.lastMessageDescription.substring(0, 19) + '...' : result.lastMessageDescription;
		let lastMessageVisibility = result.isDraft == true ? ' d-none' : '';
		let isTouchEnable = isTouchEnabled();
		let tooltipText = isTouchEnable ? '' : 'data-toggle="tooltip" data-placement="bottom" title="' + channelName + '"';
		let tooltipParticipantsNameText = isTouchEnable ? '' : 'data-toggle="tooltip" data-placement="bottom" title="' + result.participantsNames + '"';
		let participantsInitialsVisibility = IsNullOrEmptyOrUndefinedLooseTyped(result.participantsInitials) ? 'd-none' : 'd-block';
		let element = '<div class="messagerow"><a href="javascript:void(0)" class="messageChannelOnClick" data-chid="' + result.channelId + '" >\
						<div class="list ' + isUnread + '" data-chid="' + result.channelId + '">\
							<div class="row no-gutters">\
								<div class="col-9 col-md-9 col-lg-9 col-xl-9">\
									<div class="subject mb-1" ' + tooltipText + '><input type="hidden" id="hdnisdraft_' + result.channelId + '" value="' + result.isDraft + '">\
										'+ channelName + '\
									</div>\
									<h1 class="message-head vesselhead '+ vesselVisiblity + '">\
                                        <img class="mr-1" src="/images/vesseltopicon.svg" style="height:14px;" />\
                                        <span class="align-bottom">' + vesselName + '</span>\
                                    </h1>\
                                    <h1 class="message-head initialshead '+ participantsInitialsVisibility + '">\
                                        <img class="" src="/images/initialsuser.svg" ' + tooltipParticipantsNameText +' />\
                                        <span class="align-bottom" ' + tooltipParticipantsNameText +'>'+ result.participantsInitials + '</span>\
                                    </h1>\
								</div>\
								<div class="col-3 col-md-3 col-lg-3 col-xl-3 message-right-align">\
                                <div class="messagecount">'+ unreadMessageCountUIElement + '</div>\
								</div>\
							</div>\
                      <div class="message-time">' + createDate + '</div>\
                               '+ checkDraft(lastSender, lastMessageDescription, lastMessageVisibility, completeMessage, result.messageHasAttachment) + '\
						</div></a>\
                    '+ '<button type="button" class="float-right deleteChannelDashboard btn p-0" data-channelid="' + result.channelId + '" data-isdraft="' + result.isDraft + '">'
			+ '<i class="fa fa-trash-alt color-delete-grey"></i>'
			+ '</button></div>';
		return element;
	} else {
		return '';
	}
}

function checkDraft(strlastSender, strlastMessageDescription, strlastMessageVisibility, strcompleteMessage, isAttachment) {
	let isTouchEnable = isTouchEnabled();
	let tooltipTextDraft = isTouchEnable ? '' : 'data-toggle="tooltip" data-placement="bottom"';
	let tooltipTextMessage = isTouchEnable ? '' : 'data-toggle="tooltip" data-placement="bottom" title="' + strcompleteMessage + '"';
	if (strlastMessageVisibility == ' d-none') {
		return '<h1 class="message-head msghead msg" ' + tooltipTextDraft + ' ><div class="vessel-name"> <i>Draft</i></div></h1>';
	}
	else {

		var value = ' <h1 class="message-head msghead msg' + strlastMessageVisibility + '" ' + tooltipTextMessage + '">\ <img src="/images/messagetabs.svg" />\ <span class="">' + strlastSender + ' : ';
		if (isAttachment) {
			value = value + '<i class="fa fa-paperclip mr-1 text-teal"></i>'
		}

		value = value + strlastMessageDescription + '</span></h1>';

		return value;
	}
}

export function UpdateUnreadMessagesOnDashboard() {
	if ($('div.message-list').length > 0) {
		GetUnreadMessages();
	}
}

export function GetChatBaseIcons(id, chatCount, messageDetailsJSON) {
	var chatDisplay = chatCount > 0 ? '' : 'd-none';
	var messageDetailsJSONstr = encodeURIComponent(JSON.stringify(messageDetailsJSON));

	var baseButtons = '<a href="javascript:void(0)" class="discussion-drop discussionListAnchorOnClick ' + chatDisplay + '" data-messagejson="' + messageDetailsJSONstr + '" data-toggle="tooltip" data-placement="top" title="Discussion">\
						<span id = "baseChannelCount' + id + '" class="baseDiscussionCountCommon" > ' + chatCount + '</span >\
					</a> '

	return baseButtons;
}

export function GetNotesBaseIcons(id, notesCount, messageDetailsJSON) {
	var notesDisplay = notesCount > 0 ? '' : 'd-none';
	var messageDetailsJSONstr = encodeURIComponent(JSON.stringify(messageDetailsJSON));

	var baseButtons = '<a href="javascript:void(0)" class="notes-drop noteListAnchorOnClick ' + notesDisplay + '" data-messagejson="' + messageDetailsJSONstr + '" data-toggle="tooltip" data-placement="top" title="Notes"><span id="baseNotesCount' + id + '" class="baseNotesCountCommon">' + notesCount + '</span></a>';

	return baseButtons;
}
export function ConvertByteToMegaBytes(bytes) {
	if (bytes == 0) {
		return 0;
	}
	var megaBytes = parseFloat((bytes / (1024 * 1024)));
	return megaBytes;
}

export function GetNoticationUnreadChannelCountDashboard() {
    $.ajax({
        url: "/Dashboard/GetUnreadChannelCount",
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

export function GetValueOrDefaultDT(value) {
	let defaultValue = '<span class="d-md-none d-lg-none d-xl-none">-</span>';
	return IsNullOrEmptyOrUndefinedLooseTyped(value) ? defaultValue : value;
}

export function ErrorLog(xhr, status, error) {
	var data = JSON.parse(xhr.responseText);
	ToastrAlert("error", data);
	console.log(xhr)
	console.log(status)
	console.log(error)
}

export function IsJsonString(str) {
	try {
		JSON.parse(str);
	} catch (e) {
		return false;
	}
	return true;
}

export function GlobalRequestAuthorisation(xhr) {
	xhr.setRequestHeader('Authorization', 'Bearer ' + GetCookie('ClientWebToken'));
}

export function GlobalNotificationRequestAuthorisation(xhr) {
	xhr.setRequestHeader('Authorization', 'Bearer ' + GetCookie('NotificationWebToken'));
}

export function GlobalAjaxCall(url, requestType, contentType, dataType, data, beforeSendCallbackFunction, successCallbackFunction, completeCallbackFunction, errorCallBackFunction) {

    $.ajax({
        url: url,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {
            //GlobalRequestAuthorisation(jqXHR);
            if (typeof beforeSendCallbackFunction === "function") {
                beforeSendCallbackFunction(jqXHR);
            }
        },
        success: function (data, textStatus, jqXHR) {
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (typeof errorCallBackFunction === "function") {
                errorCallBackFunction(jqXHR, textStatus, errorThrown);
            }
        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}

export function GlobalAjaxNotificationCall(url, requestType, contentType, dataType, data, beforeSendCallbackFunction, successCallbackFunction, completeCallbackFunction, errorCallBackFunction) {

    $.ajax({
        url: url,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {
            //GlobalNotificationRequestAuthorisation(jqXHR);
            if (typeof beforeSendCallbackFunction === "function") {
                beforeSendCallbackFunction(jqXHR);
            }
        },
        success: function (data, textStatus, jqXHR) {
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (typeof errorCallBackFunction === "function") {
                errorCallBackFunction(jqXHR, textStatus, errorThrown);
            }
        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}

function SetSelectedTab(pageKey, selectedTab) {
    $.ajax({
        url: "/Common/UpdateSelectedTab",
        type: "post",
        datatype: "json",
        data: {
            pageKey: pageKey,
            selectedTab: selectedTab
        },
        success: function (data) {

		},
	});
}

export function RegisterTabSelectionEvent(selector, pageKey) {
	$(selector).click(function () {
		var id = $(this)[0].id;
		SetSelectedTab(pageKey, id);
	});
}

function isTouchEnabled() {
	return ('ontouchstart' in window) ||
		(navigator.maxTouchPoints > 0) ||
		(navigator.msMaxTouchPoints > 0);
}

export function datepickerheightinmobile() {
	//if (screen.width < 767) {
	//   /* alert('screenwidth');*/
	//    var fullheightdatepicker = $(window).height();
	//    var headerdatepicker = $('.app-header').outerHeight();
	//    var subheaderdatepicker = $('.app-page-title').outerHeight();
	//    $(".daterangepicker").css({
	//        "height": fullheightdatepicker - headerdatepicker - subheaderdatepicker + 30,
	//        "top": headerdatepicker + subheaderdatepicker - 30
	//    });
	//}

	//console.log('bodyheight', fullheightdatepicker);
	//console.log(headerdatepicker);
	//console.log(subheaderdatepicker);
}

export function SetTooltip(element, content) {
	$(element).attr('title', "");
	$(element).attr('data-original-title', content);
	$('[data-toggle="tooltip"]').tooltip();
	$(element).tooltip();
}

export function SummaryColorCode(span, data, colorClass) {
	if (data == 0) {
		$(span).addClass("txt-color-gray");
	}
	else {
		$(span).addClass(colorClass);
	}
}

export function FormatDateCustom(data, dateFormat) {
	if (data == null) return "";
	var date = new Date(data);
	return moment(date).format(dateFormat);
};
