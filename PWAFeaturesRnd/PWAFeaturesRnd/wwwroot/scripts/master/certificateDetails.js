import "@chenfengyuan/datepicker";
import "bootstrap";
import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import "daterangepicker";
import * as JSZip from "jszip";
import moment from "moment";
import { GetCellData } from "../common/datatablefunctions.js"
import { AjaxError, ErrorLog, AddLoadingIndicator, RemoveLoadingIndicator, base64ToArrayBuffer, saveByteArray, GetCookie, ToastrAlert, BackButton, SetHeaderMargin, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, RemoveClassIfPresent, headerReadMore,  } from "../common/utilities.js";
import { CertificateDetailsPageKey } from "../common/constants.js"
import { RecordLevelNote } from "../common/notesUtilities.js"

window.JSZip = JSZip;
$(window).on('resize', SetHeaderMargin);

var gridChangelogsList;
var gridRenewalHistoryList;

$(document).on("click", ".close-popover", function () {
	$('.popover').popover('hide');
	$('body').removeClass('popover-design');
});

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

$('#mobileactiontoggle').click(function () {
	$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
});

$(document).click(function () {
	if ($("#mobileActiondropdown").hasClass('show')) {
		$("#mobileActiondropdown").removeClass('show');
	}
});

$(document).ready(function () {

	var val = $('#IsFromViewRecord').val();
	if (val == 'True' || val == 'true' || val == true) {
		if ($(window).width() > 767) {
			$('body').addClass("hideleftmenuheader");
		}
		else {
			$('.app-container .logo-src, .app-container .aBaseNotification, .app-container .mobile-toggle-header-nav, .mobile-header-back, .vesseldropdownmobile').hide();
			$('.app-header__mobile-menu .header-dots').css("visibility", "hidden");
			RemoveClassIfPresent('.backclose', 'd-none');
		}
	}

	if (screen.width < 760) {
		headerReadMore('common-sub-heading-black', 'header');
	}

	//Sidebar back
	BackButton(CertificateDetailsPageKey, false)
	$('.backclose').click(function () {
		window.close();
	});

	AddLoadingIndicator();
	RemoveLoadingIndicator();

	GetChangelogs();
	GetCertificateRenewalHistory();

	ConfigurePopover();
	ConfigureDownloadPopup();

	var messageDetailsJSON = $("#MessageDetailsJSON").val()
	RecordLevelMessage(messageDetailsJSON);
	RecordLevelNote(messageDetailsJSON);

	GetDiscussionNotesCount(messageDetailsJSON);

	InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);

	AjaxError();
});

function GetChangelogs() {
	gridChangelogsList = $('#dtChangelogs').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 col-lg-12 col-xl-12"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		//"scrollY": "130px",
		"scrollCollapse": true,
		"autoWidth": false,
		"paging": false,
		"pageLength": 10,
		"order": [[0, "desc"]],
		"language": {
			"emptyTable": "No change logs available.",
			"loadingRecords": "&nbsp;"
		},
		"ajax": {
			"url": "/Certificate/GetCertificateAuditLog",
			"type": "POST",
			"data": {
				"VesselCertificateId": $('#VesselCertificateId').val(),
				"VesselId": $('#VesselId').val(),
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-datetime-align top-margin-0",
				data: "logDateLocal",
				width: "10px",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Local Date', formattedDate);
					}
					return date;
				}
			},
			{
				className: "data-text-align  top-margin-0",
				data: "updatedByName",
				width: "40px",
				render: function (data, type, full, meta) {
					if (data != null) {
						return GetCellData('Updated By', data);
					}
					else {
						return GetCellData('Updated By', "");
					}
				}
			},
			{
				className: "data-text-align tdblock",
				data: "event",
				width: "40px",
				render: function (data, type, full, meta) {
					if (data != null) {
						return GetCellData('Action', data);
					} else {
						return GetCellData('Action', "");
					}
				}
			},
			{
				className: "data-text-align tdblock",
				data: "remarks",
				width: "250px",
				render: function (data, type, full, meta) {
					if (data != null) {
						return GetCellData('Remarks', data);
					}
					else {
						return GetCellData('Remarks', "");
					}
				}
			},
		]
	});
}

function GetCertificateRenewalHistory() {
	gridRenewalHistoryList = $('#dtRenewalHistory').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 col-lg-12 col-xl-12"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		//"scrollY": "150px",
		"scrollCollapse": true,
		"autoWidth": false,
		"paging": false,
		"pageLength": 10,
		"order": [[1, "asc"]],
		"language": {
			"emptyTable": "No renewal history available.",
			"loadingRecords": "&nbsp;"
		},
		"ajax": {
			"url": "/Certificate/GetCertificateRenewalHistory",
			"type": "POST",
			"data": {
				"VesselCertificateId": $('#VesselCertificateId').val()
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-icon-align mobile-popover-attachments tdblock",
				//data: "certificateLogId",
				width: "15px",
				orderable: false,
				render: function (data, type, full, meta) {
					if (!full.isDocumentsAvailable) {
						return '<a class="text-black" target="_blank"><img src="/images/Download-doc-inactive.png" class="m-0 align-top" width="18" title="Download"/></a>';
					}
					else {
						var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';
						var uniqueId = full.certificateLogId;
						var element = '';

						element = '<a class="text-black documentPopup renewal-history-doc-popup cursor-pointer universalIdentifier_' + uniqueId + '" target="_blank" ><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
						return element;
					}
				}
			},
			{
				className: "data-text-align tdblock top-margin-0 ",
				data: "issuedBy",
				width: "50px",
				render: function (data, type, full, meta) { return GetCellData('Issued By', data); }
			},
			{
				className: "data-datetime-align",
				data: "issueDate",
				width: "15px",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Issued', formattedDate);
					}
					return date;
				}
			},
			{
				className: "data-datetime-align",
				data: "expiryDate",
				width: "15px",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Expiry', formattedDate);
					}
					return date;
				}
			},
			{
				className: "data-text-align",
				data: "location",
				width: "50px",
				render: function (data, type, full, meta) {
					data = data == null ? "" : data;
					return GetCellData('Location', data);
				}
			},
			{
				className: "data-text-align",
				data: "updatedBy",
				width: "50px",
				render: function (data, type, full, meta) {
					data = data == null ? "" : data;
					return GetCellData('Updated By', data);
				}
			},
			{
				className: "data-datetime-align",
				data: "updatedOn",
				width: "15px",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Updated', formattedDate);
					}
					return date;
				}
			}
		]
	});
}

function GetFormattedOnlyDate(data) {
	if (data == null) return "";
	var date = new Date(data);
	return moment(date).format("D MMM YYYY");
}

function CertificateDocumentsPopover(uniqueClsSel, VesselCertificateLogIds) {
	var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
		'<div class="loader  mx-auto">' +
		'<div class="ball-clip-rotate">' +
		'<div></div>' +
		'</div>' +
		'</div>' +
		'</div>';

	$('.' + uniqueClsSel).attr('title', 'Attachments <a href = "#" class= "close-popover cursor-pointer pull-right"><img src="/images/popover-close.png" /></a>');
	$('.' + uniqueClsSel).attr('data-placement', 'bottom');
	$('.' + uniqueClsSel).attr('data-trigger', 'focus');
	$('.' + uniqueClsSel).attr('data-toggle', 'popover');
	$('.' + uniqueClsSel).attr('data-html', true);
	$('.' + uniqueClsSel).attr('data-content', '<div class="elementLoader p-2"></div>');

	$.ajax({
		url: "/Certificate/GetCertificateDocuments",
		type: "POST",
		dataType: "JSON",
		global: false,
		data: {
			"VesselCertificateLogIds": VesselCertificateLogIds
		},
		beforeSend: function (xhr) {
			$('.' + uniqueClsSel).popover('show');
			$(".elementLoader").block({
				message: $(" " + loadercontent),
			});
		},
		success: function (data) {
			var jsonArray = data.data;
			var attachCount = jsonArray.length;
			if (attachCount > 1 || (attachCount == 1 && jsonArray[0].isWebAddressEditable == true)) {
				var html_content = "<div class='elementLoader'><table class='table table-condensed table-borderless mb-0'><tbody>";
				for (var i = 0; i < attachCount; i++) {
					var iconPath = jsonArray[i].isWebAddressEditable == true ? '/images/Download-doc-inactive.png' : '/images/Download-doc-active.png';
					html_content += "<tr>";
					html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='" + iconPath + "' class='m-0' width='18' title='Download'/>";
					html_content += "<span class='documentName' > " + jsonArray[i].title + " </span >";
					html_content += "<span class='documentId d-none'> " + jsonArray[i].ettId + " </span >";
					html_content += "<span class='webAddress d-none'> " + jsonArray[i].webAddress + " </span >";
					html_content += "<span class='isWebAddressEditable d-none'> " + jsonArray[i].isWebAddressEditable + " </span >";
					html_content += "<span class='documentfileName d-none' > " + jsonArray[i].cloudFileName + " </span ></a></td > ";
					html_content += "<td class='data-datetime-align'> " + GetFormattedOnlyDate(jsonArray[i].createdOn) + "</td>";
					html_content += "</tr>";
				}
				html_content += "</tbody></table></div>";
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('title', 'Attachments (' + attachCount + ') <a href="#" class="close-popover cursor-pointer pull-right"><img src="/images/popover-close.png" /></a>');
				$('.' + uniqueClsSel).attr('data-content', html_content);
				$('.' + uniqueClsSel).attr('data-placement', 'bottom');
				$('.' + uniqueClsSel).attr('data-trigger', 'focus');
				$('.' + uniqueClsSel).attr('data-toggle', 'popover');
				$('.' + uniqueClsSel).attr('data-html', true);
				//$('.' + uniqueClsSel).attr('data-container', ''); popover({ 'container', ".modal" });
				$('.' + uniqueClsSel).popover('show');
				$('.' + uniqueClsSel).removeAttr('title');
				//$('.' + uniqueClsSel).popover({ container: ".renewal-history-doc-popup" });

			}
			else if (attachCount == 1) {
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('data-content', '');
				var certificatesDocMap = new Map();
				certificatesDocMap.set(0, {
					documentName: jsonArray[0].title,
					documentId: jsonArray[0].ettId,
					documentFileName: jsonArray[0].cloudFileName
				});
				DownloadSelectedAttachment(certificatesDocMap, false);
			}
		},
		complete: function () {
			$(".elementLoader").unblock();
		},
	});
}

function InitializePopoverConstructor() {
	$.fn.popover.Constructor.Default.whiteList.table = [];
	$.fn.popover.Constructor.Default.whiteList.tr = [];
	$.fn.popover.Constructor.Default.whiteList.td = [];
	$.fn.popover.Constructor.Default.whiteList.th = [];
	$.fn.popover.Constructor.Default.whiteList.div = [];
	$.fn.popover.Constructor.Default.whiteList.tbody = [];
	$.fn.popover.Constructor.Default.whiteList.thead = [];
	$.fn.popover.Constructor.Default.whiteList.a = [];
	$.fn.popover.Constructor.Default.whiteList.i = [];
	$.fn.popover.Constructor.Default.whiteList.span = [];
}

function ConfigurePopover() {
	//configuring popupover
	$('#dtCertificateList tbody').on('click', 'a.documentPopup', function () {
		InitializePopoverConstructor();
		$('body').addClass('popover-design');
		var data = gridCertificateList.row($(this).parents('tr')).data();
		var uniqueClsSel = 'universalIdentifier_' + data.vesselCertificateLogId;

		if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
			CertificateDocumentsPopover(uniqueClsSel, data.vesselCertificateLogId);
		}
		else {
			$('.' + uniqueClsSel).popover('show');
		}
	});

	$(document).on('click', 'a.renewal-history-doc-popup', function () {
		InitializePopoverConstructor();
		$('body').addClass('popover-design');
		var data = gridRenewalHistoryList.row($(this).parents('tr')).data();
		var uniqueClsSel = 'universalIdentifier_' + data.certificateLogId;

		if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
			CertificateDocumentsPopover(uniqueClsSel, data.certificateLogId);
		}
		else {
			$('.' + uniqueClsSel).popover('show');
		}

	});
}

function DownloadSelectedAttachment(certificatesDocMap, globalFlag) {

	var fileName = '';
	var nextAttach = 0;
	var totalAttachment = certificatesDocMap.size;

	DownloadNextAttachment();

	function DownloadNextAttachment() {

		var input = {
			"identifier": certificatesDocMap.get(nextAttach).documentId.trim(),
			"fileName": certificatesDocMap.get(nextAttach).documentFileName.trim()
		};
		fileName = certificatesDocMap.get(nextAttach).documentName.trim();

		$.ajax({
			url: "/Certificate/DownloadDocument",
			type: "POST",
			dataType: "JSON",
			global: globalFlag,
			data: {
				"input": JSON.stringify(input)
			},
			success: function (data) {
				if (data.bytes != null) {
					var array = base64ToArrayBuffer(data.bytes);
					saveByteArray(fileName, array, data.fileType);
				} else {
					ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
				}

				nextAttach++;
				// Not implemented in error due to avoid call for multipule time 
				if (totalAttachment > nextAttach) {
					DownloadNextAttachment();
				}
			}
		});
	}
}

function ConfigureDownloadPopup() {

	$(document).on('click', 'a.documentDownload', function () {

		var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
			'<div class="loader">' +
			'<div class="ball-clip-rotate">' +
			'<div></div>' +
			'</div>' +
			'</div>' +
			'</div>';

		var documentName = $(this).children('span.documentName').text();
		var documentId = $(this).children('span.documentId').text();
		var docfileName = $(this).children('span.documentfileName').text();
		var isWebAddressEditable = $(this).children('span.isWebAddressEditable').text();
		var webAddress = $(this).children('span.webAddress').text();

		if (isWebAddressEditable.trim().toLowerCase() == 'true') {
			window.open(webAddress, '_blank').focus();
		}
		else {
			var fileName = ''
			var input = {
				"identifier": documentId.trim(),
				"fileName": docfileName.trim()
			};
			fileName = documentName.trim();

			$.ajax({
				url: "/Certificate/DownloadDocument",
				type: "POST",
				dataType: "JSON",
				global: false,
				data: {
					"input": JSON.stringify(input)
				},
				beforeSend: function (xhr) {
					$(".popover").block({
						message: $(" " + loadercontent),
					});
				},
				complete: function () {
					$(".popover").unblock();
				},
				success: function (data) {
					if (data.bytes != null) {
						var array = base64ToArrayBuffer(data.bytes);
						saveByteArray(fileName, array, data.fileType);
					} else {
						ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");
					}
				}
			});
		}

	});

}