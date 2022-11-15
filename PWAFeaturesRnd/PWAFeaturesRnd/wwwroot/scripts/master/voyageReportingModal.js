import { AjaxError, GetCookie, ToastrAlert, base64ToArrayBuffer, saveByteArray } from "../common/utilities.js"
import { GetCellData, GetFormattedDateTime, GetFormattedDate } from "../common/datatablefunctions.js"

var agent1Details, agent2Details, agent3Details;

var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
	'<div class="loader  mx-auto">' +
	'<div class="ball-clip-rotate">' +
	'<div></div>' +
	'</div>' +
	'</div>' +
	'</div>';


$(document).on('click', '#btnAgentDetailsCopy1', function () {
	copyToClipboard(agent1Details);
})

$(document).on('click', '#btnAgentDetailsCopy2', function () {
	copyToClipboard(agent2Details);
})

$(document).on('click', '#btnAgentDetailsCopy3', function () {
	copyToClipboard(agent3Details);
})

$(document).on('click', '.badweather', function () {
	var urlRequest = $(this).attr('id');
	LoadBadWeather(urlRequest);
});

$(document).on('click', '.offhire', function () {
	var urlRequest = $(this).attr('id');
	LoadOffHireDetails(urlRequest);
});

$(document).on('click', '.portBadWeather', function () {
	var urlRequest = $(this).attr('id');
	LoadPortBadWeather(urlRequest);
});

$(document).on('click', '.portDelay', function () {
	var urlRequest = $(this).attr('id');
	LoadPortDelay(urlRequest);
});

$(document).ready(function () {

	AjaxError();
	$('.agentDetails').click(function () {
		var requestURL = $('#ListURL').val();
		var vesselURL = $('#EncryptedVesselDetail').val();
		LoadAgentDetails(requestURL, vesselURL);
	});

	$('#PortAgentDetails').click(function () {
		var requestURL = $('#VoyageReportingRequestUrl').val();
		CurrentVoyageAgentDetails(requestURL);
	});

	$('#FromAnchorPortAlert').click(function () {
		var requestURL = $(this).attr('url-data');
		LoadPortServiceDetails(requestURL);
	});

	$('#ToAnchorPortAlert').click(function () {
		var requestURL = $(this).attr('url-data');
		LoadPortServiceDetails(requestURL);
	});

	$('#modalAttachmentsDescription').on('hidden.bs.modal', function () {
		$('body').addClass('modal-open');
	});

});

export function OpenModalAgentDetails(requestURL, vesselURL) {
	LoadAgentDetails(requestURL, vesselURL);
}

export function OpenModalPortServices(requestURL) {
	LoadPortServiceDetails(requestURL);
}

function LoadAgentDetails(requestURL, vesselURL) {
	LoadVesselPreview(vesselURL);
	LoadPortAgentList(requestURL);
}

function LoadVesselPreview(vesselURL) {

	$('#spanAgentDetailsVesselName').text('');
	$('#spanAgentDetailsImoNumber').text('');

	$(document).find('.modelagentdetailspopup').block({
		message: $(" " + loadercontent),
	});
	$.ajax({
		url: "/VoyageReporting/GetVesselPreview",
		type: "POST",
		dataType: "JSON",
		data: {
			"encryptedVesselDetail": vesselURL
		},
		success: function (data) {
			if (data != null) {
				$('#spanAgentDetailsVesselName').text(data.name);
				$('#spanAgentDetailsImoNumber').text(data.imo);
				var vesselToolTip = 'Type: ' + data.type + '<br/>Build Date: ' + data.vesselBuiltDate + '<br/>Age: ' + data.vesselAge;
				$('#infoAgentDetailsVesselDetails').attr("data-original-title", vesselToolTip);
			}
		},
		complete: function () {
			$(document).find('.modelagentdetailspopup').unblock();
		}
	});
}

function LoadPortAgentList(requestURL) {

	$('#ulAgentTabList').empty();
	$('#divAgentTabContent').empty();

	$.ajax({
		url: "/VoyageReporting/GetPortAgentList",
		type: "POST",
		dataType: "JSON",
		data: {
			"input": requestURL
		},
		success: function (result) {
			if (result != null) {
				var data = result.data;
				var tabHtml = '';
				for (var i = 0; i < data.length; i++) {
					var index = i + 1;
					var tabContentHtml = '';
					if (index == 1) {
						tabHtml += "<li class='nav-item'><a data-toggle='tab' href='#tab-agentDetails-agent" + index + "' id='Agent" + index + "' class='nav-link nav-link-teal active'>AGENT " + index + " </a></li>";
						tabContentHtml += "<div class='tab-pane active' id='tab-agentDetails-agent" + index + "' role='tabpanel'></div>";
					}
					else {
						tabHtml += "<li class='nav-item'><a data-toggle='tab' href='#tab-agentDetails-agent" + index + "' id='Agent" + index + "' class='nav-link nav-link-teal'>AGENT " + index + " </a></li>";
						tabContentHtml += "<div class='tab-pane' id='tab-agentDetails-agent" + index + "' role='tabpanel'></div>";
					}
					$('#divAgentTabContent').append(tabContentHtml);
					LoadAgentTab(index, data[i].encryptedAgentId, data[i].agentType);
				}
				$('#ulAgentTabList').append(tabHtml);
			}
		},
		complete: function () {
			$(document).find('.modelagentdetailspopup').unblock();
		}
	});
}

function LoadAgentTab(index, encryptedAgentId, agentType) {

	agent1Details = '';
	agent2Details = '';
	agent3Details = '';

	$.ajax({
		url: "/VoyageReporting/GetPortAgentDetail",
		type: "POST",
		dataType: "JSON",
		data: {
			"input": encryptedAgentId
		},
		success: function (result) {
			if (result != null) {
				console.log(result.data);
				var obj = result.data;
				var tabContentsHtml = '';
				var copyAgentDetails = '';

				//cmpname + agent type
				tabContentsHtml += "<div class='font-weight-bold'> <span class='float-right'><a href='javascript: void (0);' class='ml-1 text-teal'><i class='fa fa-copy' id='btnAgentDetailsCopy" + index + "'></i></a></span> <span id='spanCmpName" + index + "'>" + obj.companyName + "</span> (<span id='spanAgentType" + index + "'>" + agentType + "</span>) </div>";
				copyAgentDetails += obj.companyName + " (" + agentType + ")";

				//detailssection -- start
				tabContentsHtml += "<div class='row'>";

				//detailscolumn -- start
				tabContentsHtml += "<div class='mt-2 col-12 col-md-7'>";

				//visibility sections -- start
				//" + index + "
				//" + obj. + "

				//CmpAddr
				if (obj.address != null && obj.address != '') {
					tabContentsHtml += "<div class='row'> <div class='col-12 col-md-12 py-1'> <span id='spanCmpAddr" + index + "'>" + obj.address + "</span> </div> </div>";
					copyAgentDetails += "\n" + obj.address;
				}

				//CmpTown + CmpState + CmpPostCode
				if ((obj.town != null && obj.town != '') || (obj.state != null && obj.state != '') || (obj.postalCode != null && obj.postalCode != '')) {

					tabContentsHtml += "<div class='row'> <div class='col-12 col-md-12 py-1'>";
					copyAgentDetails += "\n";

					//CmpTown
					if (obj.town != null && obj.town != '') {
						tabContentsHtml += "<span id='spanCmpTown" + index + "'> " + obj.town + "</span>";
						copyAgentDetails += obj.town;
					}

					//CmpState
					if (obj.state != null && obj.state != '') {
						tabContentsHtml += "<span id='spanCmpState" + index + "'> " + obj.state + " </span>";
						copyAgentDetails += " " + obj.state;
					}

					//CmpPostCode
					if (obj.postalCode != null && obj.postalCode != '') {
						tabContentsHtml += "<span id='spanCmpPostCode" + index + "'> " + obj.postalCode + "</span>";
						copyAgentDetails += " " + obj.postalCode;
					}

					tabContentsHtml += "</div> </div>";
				}

				//Country.CntDesc
				if (obj.country != null && obj.country != '') {
					tabContentsHtml += "<div class='row'> <div class='col-12 col-md-12 py-1'> <span id='spanCntDesc" + index + "'>" + obj.country + "</span> </div> </div>";
					copyAgentDetails += "\n" + obj.country;
				}

				//CmpFax
				if (obj.fax != null && obj.fax != '') {
					tabContentsHtml += "<div class='row'> <div class='col-3 col-md-3 py-1'> <label class='custom-label'>Fax</label> </div> <div class='col-9 col-md-9 py-1'> <span id='spanCmpFax" + index + "'> " + obj.fax + "</span> </div> </div>";
					copyAgentDetails += "\n" + "Fax " + obj.fax;
				}

				//CmpMobile
				if (obj.mobile != null && obj.mobile != '') {
					tabContentsHtml += "<div class='row'> <div class='col-3 col-md-3 py-1'> <label class='custom-label'>Mobile</label> </div> <div class='col-9 col-md-9 py-1'> <span id='spanCmpMobile" + index + "'> " + obj.mobile + "</span> </div> </div>";
					copyAgentDetails += "\n" + "Mobile " + obj.mobile;
				}

				//CmpTelephone
				if (obj.telephone != null && obj.telephone != '') {
					tabContentsHtml += "<div class='row'> <div class='col-3 col-md-3 py-1'> <label class='custom-label'>Telephone</label> </div> <div class='col-9 col-md-9 py-1'> <span id='spanCmpTelephone" + index + "'> " + obj.telephone + "</span> </div> </div>";
					copyAgentDetails += "\n" + "Telephone " + obj.telephone;
				}

				//CmpTelex
				if (obj.telex != null && obj.telex != '') {
					tabContentsHtml += "<div class='row'> <div class='col-3 col-md-3 py-1'> <label class='custom-label'>Telex</label> </div> <div class='col-9 col-md-9 py-1'> <span id='spanCmpTelex" + index + "'> " + obj.telex + "</span> </div> </div>";
					copyAgentDetails += "\n" + "Telex " + obj.telex;
				}

				//CmpEmail
				if (obj.email != null && obj.email != '') {
					tabContentsHtml += "<div class='row'> <div class='col-3 col-md-3 py-1'> <label class='custom-label'> Email </label> </div> <div class='col-9 col-md-9 py-1'> <a id='aCmpEmail" + index + "' href='mailto:" + obj.email + "'>" + obj.email + "</a> </div> </div>";
					copyAgentDetails += "\n" + "Email " + obj.email;
				}

				//CmpWww
				if (obj.website != null && obj.website != '') {
					tabContentsHtml += "<div class='row'> <div class='col-3 col-md-3 py-1'> <label class='custom-label'> Website </label> </div> <div class='col-9 col-md-9 py-1'> <a id='aCmpWww" + index + "' href='https://" + obj.website + "'> " + obj.website + " </a> </div> </div>";
					copyAgentDetails += "\n" + "Website " + obj.website;
				}
				//visibility sections -- end

				//detailscolumn -- end
				tabContentsHtml += "</div>";

				//notes column -- start
				tabContentsHtml += "<div class='mt-2 col-12 col-md-5'> <div class='font-weight-bold'>Notes</div> <div> <textarea id='textareaAgentNotes" + index + "' class='form-control form-control-sm mt-2' rows='9' readonly>" + obj.notes + "</textarea> </div> </div>";
				if (obj.notes != null && obj.notes != '') {
					copyAgentDetails += "\n" + "Notes " + obj.notes;
				}
				//notes column -- end

				//detailssection -- end
				tabContentsHtml += "</div>";

				$('#tab-agentDetails-agent' + index).append(tabContentsHtml);

				if (index == 1) {
					agent1Details = copyAgentDetails;
				}
				else if (index == 2) {
					agent2Details = copyAgentDetails;
				}
				else if (index == 3) {
					agent3Details = copyAgentDetails;
				}
			}
		},
		complete: function () {
			$(document).find('.modelagentdetailspopup').unblock();
		}
	});
}

function copyToClipboard(text) {
	var input = document.querySelector("textarea.copyfrom");
	input.value = text;
	input.select();
	document.execCommand("copy");
}

export function LoadBadWeather(urlRequest) {
	$('#modalBadweather').modal("show");
	$.ajax({
		url: "/VoyageReporting/GetCurrentVoyageBadWeatherDetails",
		type: "POST",
		global:false,
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(document).find(".modalBadWeatherPopup").block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			$('#spanBadWeatherSpaDate').text(data.spaDate);
			$('#dtbadweatherlist').DataTable().destroy();
			var badweatherlist = $('#dtbadweatherlist').DataTable({
				"processing": false,
				"serverSide": false,
				"lengthChange": true,
				"searching": false,
				"info": false,
				"autoWidth": true,
				"paging": false,
				"data": data.breakAndBadWeatherList,
				"columns": [
					{
						className: "data-text-align tdblock",
						data: "badWeatherDetailDescription",
						"orderable": false,
					},
					{
						className: "data-text-align",
						data: "charterValue",
						render: function (data, type, full, meta) { return GetCellData('Charter', data); }
					},
					{
						className: "data-text-align",
						data: "maxValue",
						render: function (data, type, full, meta) { return GetCellData('Max', data); }
					}
				]
			});

			
		},
		complete: function () {
			$(document).find(".modalBadWeatherPopup").unblock();
		}
	});

}

export function LoadOffHireDetails(urlRequest) {
	$('#modalOffHire').modal("show");
	$.ajax({
		url: "/VoyageReporting/GetCurrentVoyageOffHireDetails",
		type: "POST",
		global: false,
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(document).find(".modalOffHirePopup").block({
				message: $(" " + loadercontent),
			})
		},
		success: function (data) {
			$('#dtOffHireList').DataTable().destroy();
			var offhirelist = $('#dtOffHireList').DataTable({
				"processing": false,
				"serverSide": false,
				"lengthChange": true,
				"searching": false,
				"info": false,
				"autoWidth": false,
				"paging": false,
				"data": data.listOfBreaks,
				"columns": [
					{
						className: "data-text-align top-margin-0",
						data: "activityDescription",
						width: "100px",
						render: function (data, type, full, meta) { return GetCellData('Reason', data); }
					},
					{
						className: "data-datetime-align top-margin-0",
						data: "delayDuration",
						width: "35px",
						render: function (data, type, full, meta) { return GetCellData('Delay', data); }
					},
					{
						className: "data-text-align",
						data: "isOffHire",
						width: "50px",
						render: function (data, type, full, meta) { return GetCellData('Off - Hire', data); }
					},
					{
						className: "data-datetime-align",
						data: "dateFrom",
						width: "100px",
						render: function (data, type, full, meta) { return GetCellData('From', data); }
					},
					{
						className: "data-datetime-align",
						data: "dateTo",
						width: "100px",
						render: function (data, type, full, meta) { return GetCellData('To', data); }
					},
					{
						className: "data-text-align text-break",
						data: "offHireType",
						width: "120px",
						render: function (data, type, full, meta) { return GetCellData('Off Hire Type', data); }
					},
					{
						className: "data-text-align tdblock",
						data: "comments",
						width: "180px",
						render: function (data, type, full, meta) { return GetCellData('Comments', data); }
					}

				]
			});

		},
		complete: function () {
			$(document).find(".modalOffHirePopup").unblock();
		}
	});
}

export function LoadPortBadWeather(urlRequest) {
	$("#modalPortWeatherDetails").modal("show");
	$('#spanSwellLength').text('');
	$('#spanWindForce').text('');
	$('#dtWeatherList').DataTable().destroy();
	
	$.ajax({
		url: "/VoyageReporting/GetBadWeatherDetail",
		type: "POST",
		global: false,
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(document).find(".modalPortWeatherDetailsPopup").block({
				message: $(" " + loadercontent),
			})
		},
		success: function (result) {
			var data = result.data;
			$('#spanSwellLength').text(data.charterSwellLength);
			$('#spanWindForce').text(data.charterWindForce);
			LoadWeatherDetailsList(data.badWeatherList);

			
		},
		complete: function () {
			$(document).find(".modalPortWeatherDetailsPopup").unblock();
		}
	});
}

function LoadWeatherDetailsList(weatherdata) {

	var weatherDetailsGrid = $('#dtWeatherList').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"order": [],
		"data": weatherdata,
		"language": {
			"emptyTable": "No weather details available.",
		},
		"columns": [
			{
				className: "text-left",
				data: "eventName",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Event Name', data); }
			},
			{
				className: "text-center",
				data: "eventDate",
				orderable: false,
				type: "date",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetFormattedDateTime(type, 'Date', data);
					}
					return data;
				}
			},
			{
				className: "text-left",
				data: "maxSwellLengthDscription",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Swell Length', data); }
			},
			{
				className: "text-left",
				data: "maxWindForce",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Wind Force', data); }
			},
		]
	});
}

function CurrentVoyageAgentDetails(urlRequest) {

	$.ajax({
		url: "/Dashboard/GetAgentDetails",
		type: "POST",
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		success: function (result) {
			LoadCurrentVoyageAgentDetails(result);
			$('#modalAgentDetail').modal("show");
		}
	});
}

function LoadCurrentVoyageAgentDetails(data) {
	$('#dtPortAgentDetails').DataTable().destroy();
	var AgentDetails = $('#dtPortAgentDetails').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": true,
		"paging": false,
		"order": [],
		"data": data,
		"language": {
			"emptyTable": "No current agent available.",
		},
		"columns": [
			{
				className: "tdblock",
				data: "cmpName",
				orderable: false,
				render: function (data, type, full, meta) { return GetCellData('Agent', data); }
			},
			{
				className: "text-left",
				data: "cmpAddr",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Delay Duration', data);
				}
			},
			{
				className: "text-left",
				data: "cmpTelephone",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Telephone', data);
				}
			}
		]
	});
}

export function LoadPortDelay(urlRequest) {
	$('#modalDelayDetails').modal("show");
	$.ajax({
		url: "/VoyageReporting/GetPortDelayAlert",
		type: "POST",
		global: false,
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {
			$(document).find(".modalDelayDetailsPopup").block({
				message: $(" " + loadercontent),
			})
		},
		success: function (result) {
			LoadDataTablePortDelay(result);
			
		},
		complete: function () {
			$(document).find(".modalDelayDetailsPopup").unblock();
		}
	});
}

function LoadDataTablePortDelay(data) {
	$('#dtDelayList').DataTable().destroy();
	var dtDelayList = $('#dtDelayList').DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"order": [],
		"data": data,
		"language": {
			"emptyTable": "No delay available.",
		},
		"columns": [
			{
				className: "data-text-align tdblock",
				data: "activityDescription",
				orderable: false,
				width: "300px",
				render: function (data, type, full, meta) { return GetCellData('Activity', data); }
			},
			{
				className: "data-datetime-align",
				data: "delayDuration",
				orderable: false,
				width: "42px",
				render: function (data, type, full, meta) {
					return GetCellData('Delay Duration', data);
				}
			},
			{
				className: "data-text-align",
				data: "isOffHire",
				orderable: false,
				width: "62px",
				render: function (data, type, full, meta) {
					return GetCellData('Off-Hire', data);
				}
			},
			{
				className: "data-datetime-align",
				data: "dateFrom",
				orderable: false,
				width: "120px",
				type: "date",
				render: function (data, type, full, meta) {
					return GetCellData('From', data);
				}
			},
			{
				className: "data-datetime-align tdblock",
				data: "dateTo",
				orderable: false,
				width: "120px",
				type: "date",
				render: function (data, type, full, meta) {
					return GetCellData('To', data);
				}
			},
		]
	});
}

function LoadPortServiceDetails(urlRequest) {
	$.ajax({
		url: "/VoyageReporting/GetPortServiceDetails",
		type: "POST",
		global:false,
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		beforeSend: function (xhr) {

			$(document).find('#modalPortdetails').block({
				message: $(" " + loadercontent),
			});
		},
		success: function (data) {
			if (data != null) {
				$("#spanPortFullName").text(data.portFullName);
				$("#imgCountryFlag").attr("src", "/images/Flags/" + data.countryCode + ".png");
				$("#spanCountryName").text(data.countryName);
				$("#spanFullLongitude").text(data.fullLongitude);
				$("#spanFullLatitude").text(data.fullLatitude);
				$("#spanUnlocode").text(data.unlocode);
				$("#spanIsKeyHubPort").text(data.isKeyHubPort);
				$('#modalPortdetails').modal("show");
				LoadPortAlert(urlRequest);
			}
		}
	});
}

function LoadPortAlert(urlRequest) {

	$('#accordionAlert').empty();

	$.ajax({
		url: "/VoyageReporting/GetPortAlert",
		type: "POST",
		global:false,
		"data": {
			"input": urlRequest,
		},
		"datatype": "JSON",
		success: function (data) {
			if (data != null) {
				console.log(data);
				$('#spanAlertCount').text(data.length);

				for (var i = 0; i < data.length; i++) {
					var index = i + 1;
					var obj = data[i];
					var contentHtml = '';
					var html = $.parseHTML(obj.description);

					contentHtml += "<div class='card'>";

					//heading -- start
					contentHtml += "<div id='heading" + index + "' class='card-header'> <button type='button' data-toggle='collapse' data-target='#collapse" + index + "' aria-expanded='false' aria-controls='collapse" + index + "' class='text-left m-0 p-0 btn btn-link btn-block w-100 collapsed'> <h6 class='m-0 p-0'>";

					contentHtml += "<span class='titleport'>" + obj.title + "</span>";

					if (obj.isAcknowledged) {
						contentHtml += "<i class='fa fa-check-circle text-green float-right'></i>";
					}
					else {
						contentHtml += "<i class='fa fa-exclamation-circle text-red float-right'></i>";
					}

					if (obj.prtId == null) {
						contentHtml += "<i class='fa fa-globe float-right text-black'></i>";
					}
					else {
						contentHtml += "<i class='fa fa-anchor float-right icon-red'></i>";
					}

					if (obj.prtId == null) {
						contentHtml += "<span class='text-red float-right indication'>Country Wide Alert</span>";
					}
					else {
						contentHtml += "<span class='text-red float-right indication'>Port Alert</span>";
					}

					contentHtml += "</h6> </button> </div>";
					//heading -- end


					//content -- start
					contentHtml += "<div data-parent='#accordionAlert' id='collapse" + index + "' class='collapse'> <div class='card-body'>"

					//acknowledge -- start
					if (obj.isAcknowledged) {
						contentHtml += "<div class='approverdiv clearfix'><div class='float-left mr-common'><div class='clearfix'> <div class='p-head'>Acknowledged By</div><div class='p-value'>" + obj.acknowledgeUserName + "</div>  </div>";

						contentHtml += "<div class='clearfix'> <div class='p-head'>Acknowledged Date</div> <div class='p-value'> " + obj.acknowledgeDate + "</div></div></div>";

						contentHtml += "<div class='float-left mr-common'><div class='clearfix'> <div class='p-head w-auto role'>Role</div><div class='p-value'>" + obj.acknowledgeUserRank +"</div></div></div></div>";
					}
					//acknowledge -- end

					//details -- start
					contentHtml += "<div class='row no-gutters description'> <div class='col-5 col-md-4 col-lg-6'><div class='counters-heading'>Description</div></div><div class='col-7 col-md-8 col-lg-6'>";

					



					contentHtml += "</div> <div class='col-12 col-md-12'> <div class='mt-1 scroller pl-0 mb-1'>";

					//contentHtml += "<iframe id='iframeDescription" + index + "' style='width:100%;' frameborder='0'></iframe>";
					contentHtml += "<div id='iframeDescription" + index + "'>" + obj.description +"</div>";

					contentHtml += "</div></div> ";

					


					contentHtml += "</div>";
					//details -- end
					

					//attachments & links -- start
					contentHtml += "<div class='counters-heading border-none mb-1'>Attachments & Links (<span id='spanAttachmentsAndLinksCount" + index + "'>0</span>)</div>";

					contentHtml += "<div class='table-common-design icon-padding'><div class='table-responsive compact-table'> <table style='width: 100%;' id='dtportservicelist" + index + "' class='dtAttachments table table-hover table-bordered cardview row-sm'><thead><tr><th>&nbsp;</th><th>Created Date</th><th>Title</th> <th>Description</th></tr></thead></table></div></div>";
					//contentHtml += ;
					//attachments & links -- end

					contentHtml += "</div> </div>";
					//content -- end

					contentHtml += "</div>";

					//LoadAgentTab(index, data[i].encryptedAgentId, data[i].agentType);
					$('#accordionAlert').append(contentHtml);

					//var html = $.parseHTML(obj.description);
					//$('#iframeDescription' + index).contents().find('body').append(html);
					//$('#iframeDescription' + index).contents().find('body').css('margin', '0px');
					//$('#iframeDescription' + index).contents().find('body').css('padding-right', '15px');
					LoadAttachmentsAndLinks(obj.documentRequestUrl, index);
				}

			}
		},
		complete: function () {
			$(document).find('#modalPortdetails').unblock();
			if (($(window).width() < 767)) {
				$('body').addClass('fixedmodalbodyport');
				$('#modalPortdetails').addClass('fixedmodalbodyport');
				$('#modalPortdetails .body-layout').addClass('scroller pl-0 mb-0');
				var windowheightport = $(window).height();
				var modalheaderport = $('#modalPortdetails .modal-header').outerHeight();
				$("#modalPortdetails .body-layout.scroller").css({
					"max-height": windowheightport - modalheaderport - 50,
					"height": windowheightport - modalheaderport - 50
				});

				$("#modalPortdetails").on('hidden.bs.modal', function (e) {
					$('body').removeClass('fixedmodalbodyport');
					$('#modalPortdetails').removeClass('fixedmodalbodyport');
				});
			}
			else {
				var windowheightport = $(window).height();
				var modalheaderport = $('#modalPortdetails .modal-header').outerHeight();
				$('#modalPortdetails .body-layout').addClass('scroller pl-0 mb-0');
				$("#modalPortdetails .body-layout.scroller").css({
					"max-height": windowheightport - modalheaderport - 100
				});
			}
		}
	});
}

function LoadAttachmentsAndLinks(documentRequestUrl, index) {

	var dtportservicelist = $('#dtportservicelist' + index).DataTable({
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"order": [],
		"language": {
			"emptyTable": "No attachments available.",
		},
		"ajax": {
			"url": "/VoyageReporting/GetDocumentDetails",
			"type": "POST",
			"data":
			{
				"input": documentRequestUrl
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-icon-align",
				orderable: false,
				width:'25px',
				render: function (data, type, full, meta) {
					if (full.isWebAddressEditable == true) {
						return GetCellData('&nbsp', "<a href='https://" + full.webAddress + "'> <i class='fa fa-link' aria-hidden='true'></i> </a>");
					}
					else {
						return GetCellData('', '<i class="fa fa-paperclip documentDownload cursor-pointer" aria-hidden="true"></i>');
					}
					
				}
			},
			{
				className: "data-datetime-align",
				data: "createdOn",
				width: '100px',
				render: function (data, type, full, meta) { return GetFormattedDate(type, 'Created Date', data); }
			},
			{
				className: "data-text-align tdblock",
				data: "title",
				render: function (data, type, full, meta) {
					if (type === "display") {
						if (full.isWebAddressEditable == true) {
							return GetCellData('Title', "<a href='https://" + full.webAddress + "'> " + data + " </a>");
						}
						else {
							return GetCellData('Title', data);
						}
					}
					return data;
					
				}
			},
			{
				className: "data-icon-align",
				data: "description",
				width: '25px',
				orderable: false,
				render: function (data, type, full, meta) {
					if (type === "display") {
						if (data) {
							return GetCellData('Description', "<img src='/images/notes.svg' width='12' class='docsDescription cursor-pointer' data-toggle='modal' data-target='#modalAttachmentsDescription'>");
						}
						else {
							return GetCellData('Description', "");
						}
					}
					return data;
				}
			}
		],
		"initComplete": function (settings, json) {
			$('#spanAttachmentsAndLinksCount' + index).text(dtportservicelist.data().count());
		},
	});

	$('.dtAttachments tbody').on('click', 'i.docsDescription', function () {
		var data = dtportservicelist.row($(this).parents('tr')).data();
		if (data.isWebAddressEditable == true) {
			$('#spanAttachmentsLink').append("<a href='https://" + data.webAddress + "'> " + data.webAddress + " </a>");
		}
		else {
			document.getElementById('divWebLink').style.display = "none";
		}
		$('#spanAttachmentsDescription').text(data.description);
	});
	$('.dtAttachments tbody').on('click', 'i.documentDownload', function () {
		var data = dtportservicelist.row($(this).parents('tr')).data();
		DownloadSelectedAttachment(data);
	});

}

function DownloadSelectedAttachment(selectedItem) {

	var documentId = (selectedItem.ettId != null && selectedItem.ettId != 'undefined') ? selectedItem.ettId.trim() : '';
	var documentFileName = (selectedItem.cloudFileName != null && selectedItem.cloudFileName != 'undefined') ? selectedItem.cloudFileName.trim() : '';
	var documentCategory = (selectedItem.documentCategory != null && selectedItem.documentCategory != 'undefined') ? selectedItem.documentCategory : '';
	var input = {
		"identifier": documentId,
		"fileName": documentFileName,
		"documentCategory": documentCategory
	};
	var fileName = selectedItem.title.trim();

	$.ajax({
		url: "/Common/DownloadDocument",
		type: "POST",
		dataType: "JSON",
		global: false,
		data: {
			"input": JSON.stringify(input)
		},
		success: function (data) {
			if (data.bytes != null) {
				var array = base64ToArrayBuffer(data.bytes);
				saveByteArray(data.filename, array, data.fileType);
			} else {
				ToastrAlert("validate", "File Not Found for \"" + fileName + "\"");

			}
		}
	});
}