import "smartwizard";
import "select2/dist/js/select2.full.js";
import toastr from "toastr";
import moment from "moment";
import "bootstrap";

import * as JSZip from "jszip";
window.JSZip = JSZip;

import "datatables.net-buttons";
import "datatables.net-buttons/js/buttons.html5.js";
import "datatables.net-buttons/js/buttons.print.js";
import { MobileScreenSize, ProcurementDetailPageKey } from "../common/constants.js"
import { CustomizedExcelHeader, GetFormattedDecimal, GetFormattedDate } from "../common/datatablefunctions.js"
import { AjaxError, ErrorLog, base64ToArrayBuffer, saveByteArray, AddLoadingIndicator, RemoveLoadingIndicator, GetRoleRightsAsync, ConvertDecimalNumberToString, headerReadMore, SetHeaderMargin, BackButton, RemoveClassIfPresent, GetCookie, ToastrAlert, RecordLevelMessage, GetDiscussionNotesCount, InitializeDiscussionAndNoteClickEvents, AddClassIfAbsent, RegisterTabSelectionEvent } from "../common/utilities.js"
import { LoadInsuranceclaimLookup, SetSelectedInsuranceClaim } from "../master/lookup/insuranceClaimAuxLookup.js"
import { LoadSeasonalLookup, SetSelectedSeasonalLookup } from "../master/lookup/seasonalAuxLookup.js"
import { LoadNationalityLookup, SetSelectedNationalityLookup } from "../master/lookup/nationalityAuxLookup"
import { LoadCrewRankLookup, SetSelectedCrewRankLookup } from "../master/lookup/crewRankAuxLookup"
import { LoadVesselAuxLookup, SetSelectedVesselAuxLookup } from "../master/lookup/vesselAuxLookup"
import { LoadGeneral1AuxLookup, SetSelectedGeneral1AuxLookup } from "../master/lookup/general1AuxLookup"
import { LoadGeneral3AuxLookup, SetSelectedGeneral3AuxLookup } from "../master/lookup/general3AuxLookup"
import { RecordLevelNote } from "../common/notesUtilities.js"

var gridOrderLines, dtSuppliersTab, dtSuppliersList, accountingCompanyId, orderNumber, orderStage, baseLabel, supplierListBaseLabel, gridSupplierOrderLines, gridBudhetTabList, accountCode, orderStatus, gridSummaryComponentList, orderType, orderPriority, dtNonQuotedOrderLines;

var columnIndex_Rec = 10, columnIndex_ORD = 9

var FreightAccrualInPoVesselCurrencyDecimal, QuoteAmountInPoVesselCurrencyDecimal;
var headerIsAppend = false;
var IsMobile = false;

var AccountCodeIdentifier = "", AccountCodeDescription = "";
var AccountingCompanyIdForVesselBudget = "", OrderNumberForVesselBudget = "", AccountIdForVesselBudget;
var IsBudgetExceed;
var SupplierOrderId;
var TotalAccruals;
var IsSupplierNotesGiven, IsFeedbackRequired;
var FeedbackSupplierOrderId;
var IsAuxillaryApplicable = false, IsInsuaranceClaimAuxApplicable = false, IsSeasonalAuxApplicable = false, IsNationalityAuxApplicable = false,
	IsCrewRankAuxApplicable = false, IsVesselAuxApplicable = false, IsGeneral1AuxApplicable = false, IsGeneral3AuxApplicable = false;

var InsuaranceClaimAuxSelectedValue = "", CrewRankAuxSelectedValue = "", General1AuxSelectedValue = "", GeneralAux3SelectedValue = "", NationalityAuxSelectedValue = "", SeasonalAuxSelectedValue = "", VesselAuxSelectedValue = "";
var SaveJustificationComment = "", SaveJustificationReasonId = "", SaveFeedbackComment = "", SaveFeedbackReasonText = "";
var AuthoriseQuoteControlId = 'd5ad7449-3ecf-43bb-9657-addce93c38d1';
var ClientAuthoriseControlId = "F788B692-B22E-4EF1-A509-4025E88B999B";

var IsUserAllowedToAuthorize;
var IsClientAuthorize;
var CanAuthorise = false;
var CanClientAuthorise = false;
var CanClientReject = false;
var noOfEnabledCheckboxes;

const AUTHORISED_ENQUIRY = "Authorised Enquiry";
const PURCHASE_ORDER = "Purchase Order";
const ORDER_CANCELLED = "ZZ";
const ONHOLD = "OH";

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

$(document).on('click', '.supplierRatingToggler', function () {
	$('.rating-popup').hide();
	var supplierCompanyId = $(this).data('compid');
	var supplierName = $(this).data('suppname');
	SupplierRatingBreakdown(supplierCompanyId, supplierName);
});

$(document).on('click', '.closeRating', function () {
	$('.rating-popup').hide();
	$('.dropdown-overflow .table-responsive').css("overflow", "auto");
});

$(document).on("click", ".close-popover", function () {
	$('.popover').popover('hide');
	$('body').removeClass('popover-design');
	$('body').removeAttr('class');
});


$(document).ready(function () {
	AjaxError();
	ConfigurePopover();
	ConfigureDownloadPopup();

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
	BackButton(ProcurementDetailPageKey, false);
	$('.backclose').click(function () {
		window.close();
	});
	if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
		IsMobile = true;
	} else {
		IsMobile = false;
	}

	$('#tab-orderDetails-Suppliers .table-responsive').on('show.bs.dropdown', function () { $('#tab-orderDetails-Suppliers .table-responsive').css("overflow", "inherit"); }); $('#tab-orderDetails-Suppliers .table-responsive').on('hide.bs.dropdown', function () { $('#tab-orderDetails-Suppliers .table-responsive').css("overflow", "auto"); })

	accountingCompanyId = $('#hdnAccountingCompanyId').val();
	orderNumber = $('#hdnOrderNumber').val();
	orderStage = $('#hdnOrderStage').val();
	accountCode = $('#hdnAccountCode').val();
	orderStatus = $('#hdnOrderStatus').val();
	orderType = $('#hdnOrderType').val();
	orderPriority = $('#hdnOrderPriority').val();

	AddLoadingIndicator();
	RemoveLoadingIndicator();

	//LoadSummary();
	$('.height-equal').matchHeight();

	ClientAuthorizationPending();

	$("#rejectionComment").on('input', function () {
		if ($("#rejectionComment").val() == "") {
			$("#rejectionComment").css('border-color', 'red');
			$("#clientRejectSaveBtn").prop('disabled', true);
		} else {
			$("#rejectionComment").css('border-color', '');
			$("#clientRejectSaveBtn").prop('disabled', false);
		}
	})

	$('.dropdown-tab-1').click(function () {
		LoadSummary();
	});

	$('.dropdown-tab-2').click(function () {
		LoadOrderLinesGrid();
	});

	$('.dropdown-tab-3').click(function () {
		LoadSuppliersGridWithRoleRights();
	});

	$('.dropdown-tab-5').click(function () {
		LoadBudgetTabGrid();
	});

	$('.dropdown-tab-4').click(function () {
		LoadDeliveryTab();
	});

	RegisterTabSelectionEvent('.mobileTabClick', ProcurementDetailPageKey);
	var MobilTabCls = $("#ActiveMobileTabClass").val();
	$('.' + MobilTabCls)[0].click();
	//dropdown tab element
	if (($(window).width() < MobileScreenSize)) {
		var selectedTabElement = $('#dropdowntablist .' + MobilTabCls)[0]
		if (!($(selectedTabElement).hasClass('modal-click'))) {
			$(".dropdowntabtitle").html($(selectedTabElement).text());
			$('#dropdowntablist li').removeClass("active");
			$(selectedTabElement).parent().addClass('active')
		}
	}

	$("#authoriseButton").click(function () {
		AuthoriseOrder();
	});

	$("#clientAuthoriseButton").click(function () {
		ClientAuthoriseOrder(undefined, true);
	});

	$("#clientRejectButton").click(function () {
		$("#rejectionComment").val('');
		$("#rejectionComment").css('border-color', '');
	})

	$("#clientRejectSaveBtn").click(function () {
		let comment = $("#rejectionComment").val();
		if (comment != null && comment != "") {
			ClientAuthoriseOrder(comment, false);
		}
	});

	$('#mobileactiontoggle').click(function () {
		$('.dropdown.mobile-dropdown-title .dropdown-menu').toggleClass('show');
	});

	$(document).click(function () {
		if ($("#mobileActiondropdown").hasClass('show')) {
			$("#mobileActiondropdown").removeClass('show');
		}
	});

	//$('.open-modal-btn').click(function () {
	//    if ($('#authorizeQuoteBackdrop').hasClass('d-none')) {
	//        $('#authorizeQuoteBackdrop').removeClass('d-none');
	//    }
	//});

	//$('.close-modal-btn').click(function () {
	//    if (!$('#authorizeQuoteBackdrop').hasClass('d-none')) {
	//        $('#authorizeQuoteBackdrop').addClass('d-none');
	//    }
	//});

	//$("#modalSupplier").on('hidden.bs.modal', function (e) {
	//    alert('closed');
	//    if (!$('#authorizeQuoteBackdrop').hasClass('d-none')) {
	//        $('#authorizeQuoteBackdrop').addClass('d-none');
	//    }
	//})

	$('#btnExportBudgetList').click(() => {
		var searchValue = gridBudhetTabList.search();
		gridBudhetTabList.search("").draw();

		$('#dtBudgetTabList.cardview thead').addClass("export-grid-show");
		$('#dtBudgetTabList').DataTable().buttons(0, 2).trigger();
		$('#dtBudgetTabList.cardview thead').removeClass("export-grid-show");

		gridBudhetTabList.search(searchValue).draw();
	});

	//resolved issue for modal on modal
	$('body').on('hidden.bs.modal', function () {
		if ($('.modal.show').length > 0) {
			$('body').addClass('modal-open');
		}
	});

	//accounut code on change in wizard setup
	$('#cboVessAccountCode').on("select2:select", function (e) {
		var selectedVeeselAccCode = $('#cboVessAccountCode').select2('data');
		AccountCodeIdentifier = selectedVeeselAccCode[0].id;
		AccountCodeDescription = selectedVeeselAccCode[0].description;

		//have to call method here for account code change: LoadVesselBudget, LoadApplicableAuxFlags
		LoadVesselBudget(AccountingCompanyIdForVesselBudget, OrderNumberForVesselBudget, selectedVeeselAccCode[0].id);

		LoadApplicableAuxFlags(AccountingCompanyIdForVesselBudget, selectedVeeselAccCode[0].id);

		//Calling order details on account code change 
		GetOrderDetailsForQuoteAuth(accountingCompanyId, orderNumber)
	});

	//checkbox changed in Wizard setup
	$('#supplierNoteCheck').change(function () {
		if (this.checked) {
			//checkbox selection manupulation here:
			if ($('#next-btnAuthorizeQuote').hasClass('disabled')) {
				$('#next-btnAuthorizeQuote').removeClass('disabled');
			}
		}
		else {
			if (!$('#next-btnAuthorizeQuote').hasClass('disabled')) {
				$('#next-btnAuthorizeQuote').addClass('disabled');
			}
		}
	});

	//finish button in wizard setup   
	$('#finish-btnAuthorizeQuote').click(() => {
		WizardFinishStep();
	});

	$("#selectReasons").change(function () {
		$("#selectReasons option[value='']").remove();
		var JustificationReasonId = $('#selectReasons').val();
		if (JustificationReasonId != null && JustificationReasonId != '' && JustificationReasonId != 'undefined') {
			$('#selectReasons').css({ "border-color": '' });
		}
		else {
			$('#selectReasons').css({ "border-color": 'red' });
		}
	});

	$("#selectFeedbackReason").change(function () {
		$("#selectFeedbackReason option[value='']").remove();
		var SaveFeedbackReasonText = $("#selectFeedbackReason option:selected").text();
		if (SaveFeedbackReasonText != null && SaveFeedbackReasonText != '' && SaveFeedbackReasonText != 'undefined') {
			$('#selectFeedbackReason').css({ "border-color": '' });
		}
		else {
			$('#selectFeedbackReason').css({ "border-color": 'red' });
		}
	});

	// External Button Events
	$("#reset-btnAuthorizeQuote").on("click", function () {
		// Reset wizard
		$("#smartwizardAuthorizeQuote").smartWizard("reset");
		return true;
	});

	$("#prev-btnAuthorizeQuote").on("click", function () {
		// Navigate previous
		$("#smartwizardAuthorizeQuote").smartWizard("prev");
		return true;
	});

	$("#next-btnAuthorizeQuote").on("click", function () {
		//Navigate next
		$("#smartwizardAuthorizeQuote").smartWizard("next");
		return true;
	});

	//adding all step wise conditions here
	$("#smartwizardAuthorizeQuote").on("leaveStep", function (e, anchorObject, currentStepIndex, nextStepIndex, stepDirection) {

		if (currentStepIndex == 0) {
			//checkbox check here
			var isCheckedsupplierNote = $('#supplierNoteCheck').is(':checked');
			if (isCheckedsupplierNote) {
				return true;
			}
			else {
				return false;
			}
		}

		if (currentStepIndex == 1) {
			//if account code, reason and comments are not entered do not forward it to step 3 when it is available
			// if aux code is applicable need to add that validation here also

			if (IsFeedbackRequired) {
				var IsAccountCodeEntered, IsJustificationDescriptionEntered, IsJustificationReasonEntered;
				var JustificationReasonId = "";
				if (AccountCodeIdentifier != null && AccountCodeIdentifier != '' && AccountCodeIdentifier != 'undefined') {
					IsAccountCodeEntered = true;
				}
				else {
					IsAccountCodeEntered = false;
				}				

				JustificationReasonId = $('#selectReasons').val();
				if (IsBudgetExceed) {
					if (JustificationReasonId != null && JustificationReasonId != '' && JustificationReasonId != 'undefined') {
						$('#selectReasons').css({ "border-color": '' });
						IsJustificationReasonEntered = true;
					}
					else {
						$('#selectReasons').css({ "border-color": 'red' });
						IsJustificationReasonEntered = false;
					}

					if (($('#txtAreaDescription').val().trim().length > 0)) {
						$('#txtAreaDescription').css({ "border-color": '' });
						IsJustificationDescriptionEntered = true;
					}
					else {
						$('#txtAreaDescription').css({ "border-color": 'red' });
						IsJustificationDescriptionEntered = false;
					}
				}
				else {
					IsJustificationDescriptionEntered = true;
					IsJustificationReasonEntered = true;
                }

				//check if only data.isAuxillaryApplicable
				var IsAllAuxCompletedStepValidation = false;
				if (IsAuxillaryApplicable) {
					IsAllAuxCompletedStepValidation = ValidationCheckForAux();
				}
				else {
					IsAllAuxCompletedStepValidation = true;
				}

				if (IsJustificationReasonEntered && IsAccountCodeEntered && IsJustificationDescriptionEntered && IsAllAuxCompletedStepValidation) {
					return true;
				}
				else {
					ToastrAlert("validate", 'Mandatory details missing.');
					return false;
				}
			} 
		}

		return true;
	});

	$("#smartwizardAuthorizeQuote").on("showStep", function (e, anchorObject, stepNumber, stepDirection, stepPosition) {

		if (stepPosition === 'first') {
			//notes
			if (!$('#prev-btnAuthorizeQuote').hasClass('disabled')) {
				$('#prev-btnAuthorizeQuote').addClass('disabled');
			}
			$("#next-btnAuthorizeQuote").show();
			$("#finish-btnAuthorizeQuote").hide();

		} else if (stepPosition == 'middle') {
			//Details

			if (IsSupplierNotesGiven) {
				if ($('#prev-btnAuthorizeQuote').hasClass('disabled')) {
					$('#prev-btnAuthorizeQuote').removeClass('disabled');
				}
			}
			else {
				if (!$('#prev-btnAuthorizeQuote').hasClass('disabled')) {
					$('#prev-btnAuthorizeQuote').addClass('disabled');
				}
			}

			//if feedback is available
			if (IsFeedbackRequired) {
				$("#next-btnAuthorizeQuote").show();
				$("#finish-btnAuthorizeQuote").hide();
			}
			else {
				$("#next-btnAuthorizeQuote").hide();
				$("#finish-btnAuthorizeQuote").show();
			}

		}
		else if (stepPosition === 'final') {
			//feedback

			if ($('#prev-btnAuthorizeQuote').hasClass('disabled')) {
				$('#prev-btnAuthorizeQuote').removeClass('disabled');
			}

			$("#next-btnAuthorizeQuote").hide();
			$("#finish-btnAuthorizeQuote").show();
		}
	});

	$('#btnConfirmSave').click(function () {
		AuthQuoteConfirmedSave();
	});

	$('.btnExportOrderDetails').click(function () {
		ExportOrderDetails();
	});
	if (screen.width < 760) {
		headerReadMore('common-sub-heading-black', 'header');
		SetHeaderMargin();
	}

	var messageDetailsJSON = $("#MessageDetailsJSON").val()
	RecordLevelMessage(messageDetailsJSON);
	RecordLevelNote(messageDetailsJSON);

	GetDiscussionNotesCount(messageDetailsJSON);

	InitializeDiscussionAndNoteClickEvents(messageDetailsJSON);

	const txtAreaDescription = document.getElementById("txtAreaDescription");
	txtAreaDescription.addEventListener("input", (e) => {
		if (($('#txtAreaDescription').val().trim().length > 0)) {
			$('#txtAreaDescription').css({ "border-color": '' });
		}
		else {
			$('#txtAreaDescription').css({ "border-color": 'red' });
		}
	});

	const txtAreaFeedbackComments = document.getElementById("txtAreaFeedbackComments");
	txtAreaFeedbackComments.addEventListener("input", (e) => {
		if (($('#txtAreaFeedbackComments').val().trim().length > 0)) {
			$('#txtAreaFeedbackComments').css({ "border-color": '' });
		}
		else {
			$('#txtAreaFeedbackComments').css({ "border-color": 'red' });
		}
	});

	$('#cboCrewRankAuxLookup').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboCrewRankAuxLookup').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboCrewRankAuxLookup'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboCrewRankAuxLookup'), 'error-select2');
		}
	});

	$('#cboSeasonalAux').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboSeasonalAux').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboSeasonalAux'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboSeasonalAux'), 'error-select2');
		}
	});

	$('#cboNationalityAuxLookup').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboNationalityAuxLookup').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboNationalityAuxLookup'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboNationalityAuxLookup'), 'error-select2');
		}
	});

	$('#cboGeneral1AuxLookup').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboGeneral1AuxLookup').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboGeneral1AuxLookup'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboGeneral1AuxLookup'), 'error-select2');
		}
	});

	$('#cboGeneral3AuxLookup').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboGeneral3AuxLookup').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboGeneral3AuxLookup'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboGeneral3AuxLookup'), 'error-select2');
		}
	});

	$('#cboInsuaranceClaimAux').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboInsuaranceClaimAux').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboInsuaranceClaimAux'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboInsuaranceClaimAux'), 'error-select2');
		}
	});

	$('#cboVesselAuxLookup').on('select2:select', function (e) {
		var selectedCrewRank = $('#cboVesselAuxLookup').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboVesselAuxLookup'), 'error-select2');
		}
		else {
			AddClassIfAbsent($('#cboVesselAuxLookup'), 'error-select2');
		}
	});

	$('.btnAdvDownload').on('click', function () {
		showAdvDowloadGrid();
		$('.popover').popover('hide');
	});

	$('#btnAdvCancel').on('click', function () {
		hideAdvDowloadGrid();
	});

	$('#btnDownloadSelection').on('click', function () {
		fetchAttachments();
	});

	$('#AdvDownSelectAll').on('click', function (e) {
		if (this.checked) {
			$('input[type="checkbox"].select-checkbox-all:not(:checked)').trigger('click');
		} else {
			$('input[type="checkbox"].select-checkbox-all').trigger('click');
		}
	});

	$('#dtSuppliersTab').on('change', '.select-checkbox, .select-checkbox-all', function () {
		var rows_selected = dtSuppliersTab.column(0).checkboxes.selected();
		if (rows_selected.length == 0) {
			$("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
		}
		else {
			$("#btnDownloadSelection").removeClass('disabled').removeAttr('aria-disabled');
		}
		if (rows_selected.length > 0) {
			let rows = dtSuppliersTab.rows().nodes();
			noOfEnabledCheckboxes = $('input[type="checkbox"].select-checkbox', rows).not(":disabled").length;

			if (noOfEnabledCheckboxes == rows_selected.length) {
				$("#AdvDownSelectAll").prop({
					checked: true,
				});
			} else {
				$("#AdvDownSelectAll").prop({
					indeterminate: true,
				});
			}
		} else {
			$("#AdvDownSelectAll").prop({
				indeterminate: false,
				checked: false
			});
		}

		$('#AdvDocSelected').text(rows_selected.length);
	});

	$('#modalAuthorizeQuote').on('hidden.bs.modal', function (e) {
		$('#selectFeedbackReason').css({ "border-color": '' });
		$('#txtAreaFeedbackComments').css({ "border-color": '' });
		$('#selectReasons').css({ "border-color": '' });
		$('#txtAreaDescription').css({ "border-color": '' });
	});
	$('#modalAuthorizeQuote').on('shown.bs.modal', function (e) {
		var windowheight = $(window).height();
		var modalheader = $('.scrollerheightmodal .modal-header').outerHeight();
		var modalfooter = $('.scrollerheightmodal .modal-footer').outerHeight();
		$(".scrollerheightmodal .modal-body .scroller").css({
			"max-height": windowheight - modalheader - modalfooter - 75
		});
	});

	$('#modalConfirmAuthoriseQuote').on('hidden.bs.modal', function () {
		$('#modalAuthorizeQuote').css('z-index', 1050);
	});
	

});

//To load Order Details for quote auth - on account code change
function GetOrderDetailsForQuoteAuth(_accountingCompanyId, _orderNumber) {

	$.ajax({
		url: "/PurchaseOrder/PostGetOrderDetailsForQuoteAuth",
		type: "POST",
		"data": {
			"accountingCompanyId": _accountingCompanyId,
			"orderNumber": _orderNumber
		},
		success: function (data) {
			if (data != null) {
				//Insurance Lookup                
				SetSelectedInsuranceClaim(data.auxiliaries.claimsId, data.auxiliaries.claimsDescription, data.auxiliaries.claimsShortCode);

				//Seasonal Lookup
				SetSelectedSeasonalLookup(data.auxiliaries.seasonalId, data.auxiliaries.seasonalDescription);

				//Nationality Lookup
				SetSelectedNationalityLookup(data.auxiliaries.nationalityId, data.auxiliaries.nationalityDescription);

				//Crew Rank Lookup
				SetSelectedCrewRankLookup(data.auxiliaries.crewRankId, data.auxiliaries.crewRankDescription);

				//Vessel Lookup
				SetSelectedVesselAuxLookup(data.auxiliaries.vesselManagementId, data.auxiliaries.vesselName);

				//General 1 Aux Lookup
				SetSelectedGeneral1AuxLookup(data.auxiliaries.general1Id, data.auxiliaries.general1Description);

				//General 3 Aux Lookup
				SetSelectedGeneral3AuxLookup(data.auxiliaries.general3Id, data.auxiliaries.general3Description);
			}
		}
	});
}

//on click of wizard's finish step
function WizardFinishStep() {
	var IsJustificationReasonEntered = true;
	var IsJustificationDescriptionEntered = true;
	var IsAccountCodeEntered = true;
	var IsFeedbackCommentEntered = true;
	var IsFeedbackReasonTextEntered = true;
	SaveJustificationComment = "", SaveJustificationReasonId = "", SaveFeedbackComment = "", SaveFeedbackReasonText = "";

	if (IsBudgetExceed) {
		if (($('#txtAreaDescription').val().trim().length > 0)) {
			$('#txtAreaDescription').css({ "border-color": '' });
			SaveJustificationComment = $('#txtAreaDescription').val().trim();
			IsJustificationDescriptionEntered = true;
		}
		else {
			$('#txtAreaDescription').css({ "border-color": 'red' });
			IsJustificationDescriptionEntered = false;
		}

		SaveJustificationReasonId = $('#selectReasons').val();
		if (SaveJustificationReasonId != null && SaveJustificationReasonId != '' && SaveJustificationReasonId != 'undefined') {
			$('#selectReasons').css({ "border-color": '' });
			IsJustificationReasonEntered = true;
		}
		else {
			$('#selectReasons').css({ "border-color": 'red' });
			IsJustificationReasonEntered = false;
		}
	}
	else {
		SaveJustificationComment = "";
		SaveJustificationReasonId = "";
	}

	if (AccountCodeIdentifier != null && AccountCodeIdentifier != '' && AccountCodeIdentifier != 'undefined') {
		IsAccountCodeEntered = true;
	}
	else {
		IsAccountCodeEntered = false;
	}

	//check if only data.isAuxillaryApplicable
	var IsAllAuxCompleted = false;
	if (IsAuxillaryApplicable) {
		IsAllAuxCompleted = ValidationCheckForAux();
	}
	else {
		IsAllAuxCompleted = true;
	}

	//validation condition for feedback step:
	//for drop down validation & text entered
	if (IsFeedbackRequired) {
		SaveFeedbackReasonText = $("#selectFeedbackReason option:selected").text();
		if (SaveFeedbackReasonText != null && SaveFeedbackReasonText != '' && SaveFeedbackReasonText != 'undefined') {
			$('#selectFeedbackReason').css({ "border-color": '' });
			IsFeedbackReasonTextEntered = true;
		}
		else {
			$('#selectFeedbackReason').css({ "border-color": 'red' });
			IsFeedbackReasonTextEntered = false;
		}

		if (($('#txtAreaFeedbackComments').val().trim().length > 0)) {
			SaveFeedbackComment = $('#txtAreaFeedbackComments').val().trim();
			$('#txtAreaFeedbackComments').css({ "border-color": '' });
			IsFeedbackCommentEntered = true;
		}
		else {
			$('#txtAreaFeedbackComments').css({ "border-color": 'red' });
			IsFeedbackCommentEntered = false;
		}
	}
	else {
		SaveFeedbackReasonText = "";
		IsFeedbackReasonTextEntered = false;
		SaveFeedbackComment = "";
		IsFeedbackCommentEntered = false;
	}

	if (IsJustificationReasonEntered && IsAccountCodeEntered && IsJustificationDescriptionEntered && IsAllAuxCompleted && (!IsFeedbackRequired || (IsFeedbackCommentEntered && IsFeedbackReasonTextEntered))) {
		if (IsAuxillaryApplicable) {
			GetSelectedValuesOfAux();
		}

		$('#modalConfirmAuthoriseQuote').modal("show");
		$('#modalAuthorizeQuote').css('z-index', 1039);
	}
	else {
		ToastrAlert("validate", 'Mandatory details missing.');
	}
}

function AuthQuoteConfirmedSave() {
	var auxiliariesObj = { claimsId: InsuaranceClaimAuxSelectedValue, crewRankId: CrewRankAuxSelectedValue, general1Id: General1AuxSelectedValue, general3Id: GeneralAux3SelectedValue, nationalityId: NationalityAuxSelectedValue, seasonalId: SeasonalAuxSelectedValue, vesselManagementId: VesselAuxSelectedValue };
	var input = {
		"OrderId": $('#OrderId').val(),
		"SupplierOrderId": SupplierOrderId,
		"JustificationComment": SaveJustificationComment,
		"AccountId": AccountCodeIdentifier,
		"JustificationReasonId": SaveJustificationReasonId,
		"Accruals": TotalAccruals,
		"IsFeedbackRequired": IsFeedbackRequired,
		"FeedbackComments": SaveFeedbackComment,
		"FeedbackReasonDescription": SaveFeedbackReasonText,
		"FeedbackSupplierOrderId": FeedbackSupplierOrderId,
		"Auxiliaries": auxiliariesObj
	}

	$.ajax({
		url: "/PurchaseOrder/SaveAuthQuote",
		type: "POST",
		"data": {
			request: input
		},
		"datatype": "JSON",
		success: function (data) {
			if (data) {
				$('#modalAuthorizeQuote').modal('hide');
				$('#lblStatusMsg').text('Quote authorised successfully');
				$("#modalAuthoriseOrderSuccess").modal('show');
				$('#btnAuthoriseOrderSuccessOK').off();
				$('#btnAuthoriseOrderSuccessOK').on('click', function () {
					$("#modalAuthoriseOrderSuccess").modal('hide');
					AuthorizeQuoteSuccess(ProcurementDetailPageKey);
				});
			}
			else {
				ToastrAlert("validate", 'Unexpected error occurred.');
			}
		}
	});
}

function GetSelectedValuesOfAux() {

	if (IsCrewRankAuxApplicable) {
		var selectedCrewRank = $('#cboCrewRankAuxLookup').select2('data');
		CrewRankAuxSelectedValue = selectedCrewRank[0].id
	}
	else {
		CrewRankAuxSelectedValue = "";
	}

	if (IsSeasonalAuxApplicable) {
		var selectedSeasonalAux = $('#cboSeasonalAux').select2('data');
		SeasonalAuxSelectedValue = selectedSeasonalAux[0].id
	}
	else {
		SeasonalAuxSelectedValue = "";
	}

	if (IsNationalityAuxApplicable) {
		var selectedNationality = $('#cboNationalityAuxLookup').select2('data');
		NationalityAuxSelectedValue = selectedNationality[0].id
	}
	else {
		NationalityAuxSelectedValue = "";
	}

	if (IsGeneral1AuxApplicable) {
		var selectedGeneral1Aux = $('#cboGeneral1AuxLookup').select2('data');
		General1AuxSelectedValue = selectedGeneral1Aux[0].id
	}
	else {
		General1AuxSelectedValue = "";
	}

	if (IsGeneral3AuxApplicable) {
		var selectedGeneral3Aux = $('#cboGeneral3AuxLookup').select2('data');
		GeneralAux3SelectedValue = selectedGeneral3Aux[0].id
	}
	else {
		GeneralAux3SelectedValue = "";
	}

	if (IsInsuaranceClaimAuxApplicable) {
		var selectedInsuaranceAux = $('#cboInsuaranceClaimAux').select2('data');
		InsuaranceClaimAuxSelectedValue = selectedInsuaranceAux[0].id
	}
	else {
		InsuaranceClaimAuxSelectedValue = "";
	}

	if (IsVesselAuxApplicable) {
		var selectedVesselAux = $('#cboVesselAuxLookup').select2('data');
		VesselAuxSelectedValue = selectedVesselAux[0].id
	}
	else {
		VesselAuxSelectedValue = "";
	}
}

function ValidationCheckForAux() {
	//7 bool flag -> show/hide
	//show-> condition apply 

	var IsCrewRankAuxSelected = true, IsSeasonalAuxSelected = true, IsNationalityAuxSelected = true, IsGeneral1AuxSelected = true, IsGeneralAux3Selected = true, IsInsuaranceAuxSelected = true, IsVesselAuxSelected = true;

	if (IsCrewRankAuxApplicable) {
		var selectedCrewRank = $('#cboCrewRankAuxLookup').select2('data');
		if (selectedCrewRank[0].id != null && selectedCrewRank[0].id != '' && selectedCrewRank[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboCrewRankAuxLookup'), 'error-select2');
			IsCrewRankAuxSelected = true;
		}
		else {
			AddClassIfAbsent($('#cboCrewRankAuxLookup'), 'error-select2');
			IsCrewRankAuxSelected = false;
		}
	}

	if (IsSeasonalAuxApplicable) {
		var selectedSeasonalAux = $('#cboSeasonalAux').select2('data');
		if (selectedSeasonalAux[0].id != null && selectedSeasonalAux[0].id != '' && selectedSeasonalAux[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboSeasonalAux'), 'error-select2');
			IsSeasonalAuxSelected = true;
		}
		else {
			AddClassIfAbsent($('#cboSeasonalAux'), 'error-select2');
			IsSeasonalAuxSelected = false;
		}
	}

	if (IsNationalityAuxApplicable) {
		var selectedNationality = $('#cboNationalityAuxLookup').select2('data');
		if (selectedNationality[0].id != null && selectedNationality[0].id != '' && selectedNationality[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboNationalityAuxLookup'), 'error-select2');
			IsNationalityAuxSelected = true;
		}
		else {
			AddClassIfAbsent($('#cboNationalityAuxLookup'), 'error-select2');
			IsNationalityAuxSelected = false;
		}
	}

	if (IsGeneral1AuxApplicable) {
		var selectedGeneral1Aux = $('#cboGeneral1AuxLookup').select2('data');
		if (selectedGeneral1Aux[0].id != null && selectedGeneral1Aux[0].id != '' && selectedGeneral1Aux[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboGeneral1AuxLookup'), 'error-select2');
			IsGeneral1AuxSelected = true;
		}
		else {
			AddClassIfAbsent($('#cboGeneral1AuxLookup'), 'error-select2');
			IsGeneral1AuxSelected = false;
		}
	}

	if (IsGeneral3AuxApplicable) {
		var selectedGeneral3Aux = $('#cboGeneral3AuxLookup').select2('data');
		if (selectedGeneral3Aux[0].id != null && selectedGeneral3Aux[0].id != '' && selectedGeneral3Aux[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboGeneral3AuxLookup'), 'error-select2');
			IsGeneralAux3Selected = true;
		}
		else {
			AddClassIfAbsent($('#cboGeneral3AuxLookup'), 'error-select2');
			IsGeneralAux3Selected = false;
		}
	}

	if (IsInsuaranceClaimAuxApplicable) {
		var selectedInsuaranceAux = $('#cboInsuaranceClaimAux').select2('data');
		if (selectedInsuaranceAux[0].id != null && selectedInsuaranceAux[0].id != '' && selectedInsuaranceAux[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboInsuaranceClaimAux'), 'error-select2');
			IsInsuaranceAuxSelected = true;
		}
		else {
			AddClassIfAbsent($('#cboInsuaranceClaimAux'), 'error-select2');
			IsInsuaranceAuxSelected = false;
		}
	}

	if (IsVesselAuxApplicable) {
		var selectedVesselAux = $('#cboVesselAuxLookup').select2('data');
		if (selectedVesselAux[0].id != null && selectedVesselAux[0].id != '' && selectedVesselAux[0].id != 'undefined') {
			RemoveClassIfPresent($('#cboVesselAuxLookup'), 'error-select2');
			IsVesselAuxSelected = true;
		}
		else {
			AddClassIfAbsent($('#cboVesselAuxLookup'), 'error-select2');
			IsVesselAuxSelected = false;
		}
	}

	return IsCrewRankAuxSelected && IsSeasonalAuxSelected && IsNationalityAuxSelected && IsGeneral1AuxSelected && IsGeneralAux3Selected && IsInsuaranceAuxSelected && IsVesselAuxSelected;
}

function LoadWizardSetup(isReadNotesStepVisible, isFeedbackStepVisible) {
	$("#smartwizardAuthorizeQuote").smartWizard("reset");

	//reset all elements here - 2 textarea
	ClearTextArea();
	//unchecked checkbox here
	$('#supplierNoteCheck').prop('checked', false);
	if (isReadNotesStepVisible) {
		//selected step is 0
		$("#smartwizardAuthorizeQuote").smartWizard({
			//selected: 0,
			transitionEffect: "slide",
			toolbarSettings: {
				toolbarPosition: "none",
			},
		});
		$("#smartwizardAuthorizeQuote").smartWizard('goToStep', 0);
		$('#smartwizardAuthorizeQuote').smartWizard("stepState", [0], "show");
		$("#finish-btnAuthorizeQuote").hide();

	}
	else {
		//selected step is 1
		$("#smartwizardAuthorizeQuote").smartWizard({
			//selected: 1,
			transitionEffect: "slide",
			toolbarSettings: {
				toolbarPosition: "none",
			},
		});
		$("#smartwizardAuthorizeQuote").smartWizard('goToStep', 1);
		$('#smartwizardAuthorizeQuote').smartWizard("stepState", [0], "hide");
		$('#smartwizardAuthorizeQuote').smartWizard("stepState", [0], "disable");

		//show/hide inside else because handling where to show next/finish
		if (isFeedbackStepVisible) {
			if ($('#next-btnAuthorizeQuote').hasClass('disabled')) {
				$('#next-btnAuthorizeQuote').removeClass('disabled');
			}
			$("#finish-btnAuthorizeQuote").hide();
			$("#next-btnAuthorizeQuote").show();
		}
		else {
			$("#finish-btnAuthorizeQuote").show();
			$("#next-btnAuthorizeQuote").hide();
		}
	}

	//step 3 enable/disable
	//May need to check stepstate[number] -> what if readnotes is disabled? scenario
	if (isFeedbackStepVisible) {
		$('#smartwizardAuthorizeQuote').smartWizard("stepState", [2], "show");
	}
	else {
		$('#smartwizardAuthorizeQuote').smartWizard("stepState", [2], "hide");
		$('#smartwizardAuthorizeQuote').smartWizard("stepState", [2], "disable");
	}

	if (!$('#prev-btnAuthorizeQuote').hasClass('disabled')) {
		$('#prev-btnAuthorizeQuote').addClass('disabled');
	}
}

function ClearTextArea() {
	$('#txtAreaFeedbackComments').val('');
	$('#txtAreaDescription').val('');
}

function GetCellData(label, data) {
	return '<label>' + label + '</label><br />' + GetActualCellData(data);
}

function GetActualCellData(data) {
	return '<span class="export-Data">' + data + '</span>';
}

function LoadSummary() {
	LoadSuppliersList();
	LoadSuppliersComponentDetails();
	LoadSupplierOrderDetails();
	LoadAuthorisedSupplierDetails();
	LoadCostDetails();
	LoadInvoiceList();
	CheckOrderAuthorisationPending(function (data) {
		LoadAuthoriseAdditionalInfo();
		LoadAuthoriseByGrid();
		GetButtonVisiblityConditions(data);
	});
}
function AuthoriseOrder() {
	if (CanAuthorise) {
		$.ajax({
			url: "/PurchaseOrder/PostAuthoriseOrder",
			type: "POST",
			dataType: "JSON",
			data: {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber,
				"vesselId": $("#PurchaseOrderVesselId").val()
			},
			success: function (data) {
				if (data.response == true) {
					$("#modalAuthoriseOrderSuccess").modal('show');
					$('#btnAuthoriseOrderSuccessOK').off();
					$('#btnAuthoriseOrderSuccessOK').on('click', function () {
						$("#modalAuthoriseOrderSuccess").modal('hide');
						AuthorizeOrderSuccess(ProcurementDetailPageKey);
					});
				}
				else {
					ToastrAlert("validate", data.message);
					CheckOrderAuthorisationPending(function (data) {
						LoadAuthoriseByGrid();
						GetButtonVisiblityConditions(data);
					});
				}
			}
		});
	}
}

function AuthorizeOrderSuccess(keyName) {
	$.ajax({
		url: "/PurchaseOrder/GetAuthoriseOrderSuccesUrl",
		type: "POST",
		dataType: "JSON",
		data: {
			"pageKey": keyName
		},
		success: function (data) {
			if (data != null) {
				window.location.replace(data);
			}
		}
	});
}

function AuthorizeQuoteSuccess(keyName) {
	$.ajax({
		url: "/PurchaseOrder/GetAuthoriseQuoteSuccesUrl",
		type: "POST",
		dataType: "JSON",
		data: {
			"pageKey": keyName
		},
		success: function (data) {
			if (data != null) {
				window.location.replace(data);
			}
		}
	});
}



function ClientAuthoriseOrder(comment, isApprove) {
	if (CanClientAuthorise || CanClientReject) {
		$.ajax({
			url: "/PurchaseOrder/PostClientAuthoriseOrder",
			type: "POST",
			dataType: "JSON",
			data: {
				"accountingCompanyId": accountingCompanyId,
				"vesselId": $("#PurchaseOrderVesselId").val(),
				"comment": comment,
				"orderId": $("#OrderId").val(),
				"isApprove": isApprove
			},
			success: function (data) {
				if (data.response == true) {
					$("#modalAuthoriseOrderSuccess").modal('show');
					if (!isApprove) {
						$('#lblStatusMsg').text('Order rejected successfully');
					}

					$('#btnAuthoriseOrderSuccessOK').off();
					$('#btnAuthoriseOrderSuccessOK').on('click', function () {
						$("#modalAuthoriseOrderSuccess").modal('hide');
						AuthorizeOrderSuccess(ProcurementDetailPageKey);
					});
				}
				else {
					ToastrAlert("validate", data.message);

					CheckOrderAuthorisationPending(function (data) {
						LoadAuthoriseByGrid();
						GetButtonVisiblityConditions(data);
					});
				}
			}
		})
	}
}

function GetButtonVisiblityConditions(authPend) {

	LoadAuthorisationRights(function (authResult) {

		LoadCurrentUser(function (currUser) {

			GetRoleRightsAsync([ClientAuthoriseControlId], function (rolerights) {

				var authoriseQuoteAccess = rolerights.find(x => x.controlId.toLowerCase() === ClientAuthoriseControlId.toLowerCase());

				let orderCondition = orderStage == AUTHORISED_ENQUIRY &&
					orderStatus != ONHOLD &&
					orderStatus != ORDER_CANCELLED;

				IsUserAllowedToAuthorize = orderCondition &&
					currUser != null && currUser.roles && currUser.roles.length > 0 &&
					doesRightsExists(currUser.roles, authResult);

				IsClientAuthorize = orderCondition && authoriseQuoteAccess.permission;

				if (IsUserAllowedToAuthorize || IsClientAuthorize) {
					$("#authoriseButtonSection").removeClass("d-none");

					if (IsUserAllowedToAuthorize) {
						$("#authoriseButton").removeClass("d-none");
					} else {
						$("#authoriseButton").remove();
					}

					if (IsClientAuthorize) {
						$("#clientAuthoriseButton").removeClass("d-none");
						$("#clientRejectButton").removeClass("d-none");
					} else {
						$("#clientAuthoriseButton").remove();
						$("#clientRejectButton").remove();
					}

					CanAuthorise = (IsUserAllowedToAuthorize && authPend != null && authPend.isFurtherOrderAuthorisationRequired && authPend.isCurrentUserAuthorisationRequired);
					if (!CanAuthorise && $("#authoriseButton").length) {
						$("#authoriseButton").prop('disabled', true);
					}

					CanClientAuthorise = (IsClientAuthorize && authPend != null && authPend.isClientAuthorisationRequired);
					if (!CanClientAuthorise && $("#clientAuthoriseButton").length) {
						$("#clientAuthoriseButton").prop('disabled', true);
					}

					CanClientReject = (IsClientAuthorize && authPend != null && authPend.isClientAuthorisationRequired);
					if (!CanClientReject && $("#clientRejectButton").length) {
						$("#clientRejectButton").prop('disabled', true);
					}
					if (($(window).width() < MobileScreenSize)) {
						$(".content-pane").css("margin-top", $(".app-page-title").outerHeight(true));
					}
				} else {
					RemoveAuthButtons();
				}
			})
		});
	});
}
function doesRightsExists(arr1, arr2) {
	for (let el1 of arr1) {
		for (let el2 of arr2) {
			if (el1.roleId == el2.roleIdentifier) {
				return true;
				break;
			}
		}
	}
	return false;
}


function LoadAuthorisationRights(resolve) {
	$.ajax({
		url: "/PurchaseOrder/PostGetAuthorizationRightsForPurchasingAndOrderAuth",
		dataType: "JSON",
		type: "POST",
		success: function (authResult) {
			resolve(authResult);
		}
	});
}
function LoadCurrentUser(resolve) {
	$.ajax({
		url: "/PurchaseOrder/PostGetCurrentUser",
		dataType: "JSON",
		type: "POST",
		success: function (currUser) {
			resolve(currUser);
		}
	});
}

function LoadOrderLinesGrid() {
	$('#dtOrderLines').DataTable().destroy();
	gridOrderLines = $('#dtOrderLines').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-7 col-lg-8 col-xl-8"i><"col-12 col-md-5 col-lg-4 col-xl-4"f><"col-12 col-md-5 position-absolute-mobile">><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		"order": [],
		'columnDefs': [
			{ 'visible': false, 'targets': [columnIndex_ORD, columnIndex_Rec] }
		],
		"language": {
			"emptyTable": "No data available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/PurchaseOrder/GetOrderLines",
			"type": "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber
			},
			"datatype": "json"
		},

		"columns": [
			{
				className: "d-none d-sm-table-cell data-icon-align",
				name: "Notes",
				data: "notes",
				width: "30px",
				orderable: false,
				render: function (data, type, full, meta) {
					if (data) {
						return '<img src="/images/notes.svg" class="orderLinesNotes cursor-pointer" width="12" aria-hidden="true" data-toggle="modal" data-target="#orderLinesNotesModal"/>';
					}
					else {
						return "";
					}
				}
			},
			{
				className: "d-none d-sm-table-cell data-number-align",
				width: "10px",
				data: "itemNo",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('#', full.itemNo);
				}
			},
			{
				className: "tdblock td-row-header data-text-align",
				name: "PartName",
				data: "partName",
				width: "340px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						if (full.notes) {
							return '<img src="/images/notes.svg" class="orderLinesNotes cursor-pointer d-inline-block d-sm-none mr-1" aria-hidden="true" width="12" data-toggle="modal" data-target="#orderLinesNotesModal"/>' + '<span class="d-inline-block d-sm-none">' + full.itemNo + '&nbsp;' + '</span>' + full.partName;
						}
						else {
							return '<span class="d-inline-block d-sm-none">' + full.itemNo + '&nbsp;' + '</span>' + full.partName;
						}
					}
					return data; // This is a unformatted data  
				}
			},
			{
				className: "tdblock data-text-align",
				name: "MakersReference",
				data: "makersReference",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetCellData('Makers Reference', full.makersReference);
				}
			},
			{
				className: "tdblock data-text-align",
				name: "DrawingPosition",
				data: "drawingPosition",
				width: "100px",
				render: function (data, type, full, meta) {
					return GetCellData('Drawing Position', full.drawingPosition);
				}
			},
			{
				className: "data-text-align width-auto",
				name: "UOM",
				data: "uom",
				width: "30px",
				render: function (data, type, full, meta) {
					return GetCellData('UOM', full.uom);
				}
			},
			{
				className: "data-number-align width-auto",
				name: "ROB",
				data: "rob",
				type: "html-num",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('ROB', full.rob);
					}
					return data; // This is a unformatted data  
				}
			},
			{
				className: "data-number-align width-auto",
				name: "REQ",
				data: "req",
				type: "html-num",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('REQ', full.req);
					}
					return data; // This is a unformatted data  
				}
			},
			{
				className: "data-number-align width-auto",
				name: "ENQ",
				data: "enq",
				type: "html-num",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('ENQ', full.enq);
					}
					return data;
				}
			},
			{
				className: "data-number-align width-auto",
				name: "ORD",
				data: "ord",
				type: "html-num",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('ORD', full.ord);
					}
					return data;
				}
			},
			{
				className: "data-number-align width-auto",
				name: "REC",
				data: "rec",
				type: "html-num",
				width: "30px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('REC', full.rec);
					}
					return data;
				}
			},
		],
	});

	if (orderStage == "Purchase Order") {
		gridOrderLines.columns([columnIndex_ORD, columnIndex_Rec]).visible(true);
	}

	$('#dtOrderLines tbody').on('click', 'img.orderLinesNotes', function () {
		var data = gridOrderLines.row($(this).parents('tr')).data();
		$('#spanOrderLineNotes').text(data.notes);
	});
}

function LoadAuthoriseAdditionalInfo() {

	$.ajax({
		url: "/PurchaseOrder/PostGetAuthorisationAdditionalInfo",
		type: "POST",
		dataType: "JSON",
		data: {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber
		},
		success: function (data) {

			if (data.isAboveAuthLevel1Limit || data.isAboveClientLimit || data.isAboveOfficeLimit || data.isAboveVesselLimit) {
				$("#limitSection").removeClass("d-none");
				$(".limitCurrency").text(data.limitCurrency);
			}

			if (!data.isContractedAccount) {
				$("#isContractedAccount").removeClass("d-none");
			}

			if (data.isAboveAuthLevel1Limit) {
				$("#authLevelDesc").removeClass("d-none");
				$("#authLevelAmount").text(ConvertDecimalNumberToString(data.authLevel1Limit, 2, 1, 2));
			}
			if (data.isAboveClientLimit) {
				$("#clientLimitDesc").removeClass("d-none");
				$("#clientLimitAmount").text(ConvertDecimalNumberToString(data.clientLimit, 2, 1, 2));
			}
			if (data.isAboveOfficeLimit) {
				$("#officeLimitDesc").removeClass("d-none");
				$("#officeLimitAmount").text(ConvertDecimalNumberToString(data.officeLimit, 2, 1, 2));
			}
			if (data.isAboveVesselLimit) {
				$("#vesselLimitDesc").removeClass("d-none");
				$("#vesselLimitAmount").text(ConvertDecimalNumberToString(data.vesselLimit, 2, 1, 2));
			}
		}
	})
}

function LoadAuthoriseByGrid() {

	$('#dtOrderAuthorisers').DataTable().destroy();
	dtSuppliersTab = $('#dtOrderAuthorisers').DataTable({
		"dom": '<<"row mb-0" <"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-4 col-xl-7 offset-xl-2 dt-infomation"i><"col-12 col-md-5"f>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"pageLength": 10,
		"order": [[0, 'asc'], [1, 'asc']],
		"language": {
			"emptyTable": "No authorisers available.",
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetOrderAuthorisers",
			"type": "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber
			},
			"datatype": "json"
		},
		"columns": [
			{
				className: "data-text-align top-margin-0",
				orderable: false,
				data: "roleName",
				width: "30px",
				render: function (data, type, full, meta) {
					return GetCellData('Role', data);
				}
			},
			{
				className: "data-text-align tdblock",
				orderable: false,
				data: "userName",
				width: "140px",
				render: function (data, type, full, meta) {
					return GetCellData('User Name', data);
				}
			},
			{
				className: "data-datetime-align",
				orderable: false,
				type: "date",
				width: "20px",
				data: "authorisationDate",
				render: function (data, type, full, meta) {
					return GetFormattedDate(type, 'Date', data);
				}
			},
		],
	});
}

function CheckOrderAuthorisationPending(loadAuthorisedDetails) {

	if ((orderStage == AUTHORISED_ENQUIRY || orderStage == PURCHASE_ORDER) && orderStatus != ORDER_CANCELLED) {
		$.ajax({
			url: "/PurchaseOrder/PostCheckOrderAuthorisationPending",
			type: "POST",
			dataType: "JSON",
			data: {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber
			},
			success: function (authPend) {
				if ((orderStage == AUTHORISED_ENQUIRY || orderStage == PURCHASE_ORDER) &&
					authPend != null &&
					(authPend.isOrderAuthorisationProcessed || authPend.isClientAuthorisationRequired || authPend.isClientAuthorisationProcessed)) {

					$(".authoriseSection").removeClass("d-none");
					loadAuthorisedDetails(authPend);
				} else {
					RemoveAuthButtons();
				}
			}
		});
	} else {
		RemoveAuthButtons();
	}
}

function RemoveAuthButtons() {
	$("#authoriseButtonSection").remove();
}

function LoadSuppliersGridWithRoleRights() {
	GetRoleRightsAsync([AuthoriseQuoteControlId], function (rolerights) {
		LoadSuppliersGrid(rolerights);
	});
}

function LoadSuppliersGrid(rolerights) {

	var masterCheckBox = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
	masterCheckBox += '<input type = "checkbox" class="select-checkbox-all custom-control-input" id="masterCheckboxAll">';
	masterCheckBox += '<label class="custom-control-label d-block" for="masterCheckboxAll"></label></div >';

	var authoriseQuoteAccess = rolerights.find(x => x.controlId.toLowerCase() === AuthoriseQuoteControlId.toLowerCase());

	var purchaseOrderRequestURL = $('#PurchaseOrderRequest').val();
	$('#dtSuppliersTab').DataTable().destroy();
	dtSuppliersTab = $('#dtSuppliersTab').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-7"i><"col-12 col-md-5 position-absolute-mobile" <"row"f>>><rt><"clearfix"<"float-left"l><""p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		"order": [],
		"language": {
			"emptyTable": "No orders available.",
		},
		'columnDefs': [
			{
				'orderable': false,
				'targets': 0,
				'visible': false,
				'render': function (data, type, row, meta) {
					if (row.documentCount == 0) {
						var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
						uielement += '<input type = "checkbox" class="select-checkbox custom-control-input dt-checkboxes" id= "' + row.supplierOrderId + '" disabled="disabled">';
						uielement += '<label class="custom-control-label d-block" for= "' + row.supplierOrderId + '"></label></div >';
						data = uielement;
						return data;
					} else {
						var uielement = '<div class="custom-checkbox custom-control custom-control-inline mr-0">';
						uielement += '<input type = "checkbox" class="select-checkbox custom-control-input dt-checkboxes" id= "' + row.supplierOrderId + '">';
						uielement += '<label class="custom-control-label d-block" for= "' + row.supplierOrderId + '"></label></div >';
						data = uielement;
						return data;
					}
				},
				'createdCell': function (td, cellData, rowData, row, col) {
					if (rowData.documentCount == 0) {
						let child = $(td).children()[0];
						$(child).addClass('invisible');
						$(td).removeClass('dt-checkboxes-cell');
						this.api().cell(td).checkboxes.disable();
					}
				},
				'checkboxes': {
					'selectRow': true,
					'selectAllRender': masterCheckBox
				}
			},
		],
		'select': {
			'style': 'multi'
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetSuppliers",
			"type": "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber,
				"fetchSupplierOrderLineTotalCost": true,
				"purchaseOrderRequestURL": purchaseOrderRequestURL
			},
			"datatype": "json"
		},
		"initComplete": function (settings, json) {
			//$(dtSuppliersTab.column(11).header()).text(baseLabel);
			$('.supplierToggler').dropdown();
		},
		"columns": [
			{
				"data": "supplierOrderId",
				orderable: false,
				className: "mobile-popover-attachments tdblock data-icon-align checkbox-design-table",
				width: "40px",
			},
			{
				className: "d-sm-table-cell mobile-popover-attachments tdblock data-icon-align",
				orderable: false,
				width: "40px",
				render: function (data, type, full, meta) {
					if (full.documentCount == 0) {
						return '';
					}
					else {
						var count = '<sup style="padding-left:5px" class="txt-green-attachments font-weight-600">' + full.documentCount + '</sup>';
						var uniqueId = full.supplierOrderId;
						var element = '';

						element = '<a class="text-black documentPopup cursor-pointer universalIdentifier_' + uniqueId + '" target="_blank" ><img src="/images/Download-doc-active.png" class="m-0 align-top" width="18" title="Download"/>' + count + '</a>';
						return element;
					}
				}
			},
			{
				className: "d-none d-sm-table-cell data-icon-align",
				orderable: false,
				data: null,
				width: "35px",
				render: function (data, type, full, meta) {
					if (authoriseQuoteAccess != null && authoriseQuoteAccess != 'undefined' && authoriseQuoteAccess != '') {
						if (full.isValidForEnquiry && authoriseQuoteAccess.permission) {
							return '<div class="btn-group"><button type="button" class="btn dropdown-toggle supplierToggler" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fas fa-th-large" aria-hidden="true"></i></button> <ul class="dropdown-menu supplier-dropdown" role="menu"><li><button class="btn btn-icon"><a class="authorizeQuote text-black"> <i class="fa fa-check-circle cursor-pointer" title="Authorize Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">Authorize Quote</span></i></a></button></li><li><button class="btn btn-icon"> <a class="text-black" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '"><i class="fa fa-eye" title="View Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">View Quote</span></i></a> </button></li></ul></div>';
						}
						else {
							return '<div class="btn-group"><button type="button" class="btn dropdown-toggle supplierToggler" data-toggle="dropdown"><i class="fas fa-th-large" aria-hidden="true"></i></button> <ul class="dropdown-menu supplier-dropdown" role="menu"><li><button class="btn btn-icon"> <a class="text-black" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '"><i class="fa fa-eye" title="View Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">View Quote</span></i></a> </button></li></ul></div>';
						}
					}
					else {
						return '<div class="btn-group"><button type="button" class="btn dropdown-toggle supplierToggler" data-toggle="dropdown"><i class="fas fa-th-large" aria-hidden="true"></i></button> <ul class="dropdown-menu supplier-dropdown" role="menu"><li><button class="btn btn-icon"> <a class="text-black" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '"><i class="fa fa-eye" title="View Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">View Quote</span></i></a></button></li></ul></div>';
					}

				}
			},
			{
				className: "d-none d-sm-table-cell data-icon-align",
				name: "Notes",
				data: "hasNotes",
				orderable: false,
				width: "35px",
				render: function (data, type, full, meta) {
					return '<img src="/images/notes.svg" class="supplierNotes cursor-pointer" width="12" data-toggle="modal" data-target="#modalSupplierNotes"/>';
				}
			},
			{
				className: "data-text-align td-row-header width-85",
				name: "SupplierName",
				width: "245px",
				data: "supplierName",
				render: function (data, type, full, meta) {
					var supplierName = full.supplierName;
					if (type === "display") {
						var status = "";
						if (full.supplierOrderStatus != null) {
							if (full.orderStage == 'Purchase Order') {
								if (full.isOrderAuthorised) {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
								}
								else {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
								}
							}
							else if (full.orderStatus == 'EP' || full.orderStatus == 'EO') {
								if (full.supplierOrderStatus == 'TI' || full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
								}
								else if (full.supplierOrderStatus == 'TX') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-danger">' + full.supplierOrderStatus + '</span>';
								}
								else if (full.supplierOrderStatus == 'TH') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-amber">' + full.supplierOrderStatus + '</span>';
								}
								else {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
								}
							}
							else if (full.orderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								if (full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
								}
								else if (full.supplierOrderStatus == 'TX') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-danger">' + full.supplierOrderStatus + '</span>';
								}
								else if (full.supplierOrderStatus == 'TH') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-amber">' + full.supplierOrderStatus + '</span>';
								}
								else {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
								}
							}
							else if (full.orderStatus == 'TR') {
								if (full.supplierOrderStatus == 'TR' || full.supplierOrderStatus == 'O') {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
								}
								else {
									status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
								}
							}
							else {
								status = '<span class="d-inline-block d-sm-none mr-1 badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
							}
							var statusWithTooltip = '<span data-toggle="tooltip" title="' + full.supplierOrderStatusName + '" data-placement="bottom">' + status + '</span>';
							return '<img src="/images/notes.svg" class="supplierNotes d-inline-block d-sm-none mr-2 cursor-pointer" aria-hidden="true"  data-toggle="modal" data-target="#modalSupplierNotes" width="14"/>' + statusWithTooltip +
								'<span class="powidth"><a href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '">' + full.supplierName + '</i></a></span>';
						}
						else {
							return '<img src="/images/notes.svg" width="14" class="supplierNotes d-inline-block d-sm-none mr-2 cursor-pointer" aria-hidden="true"  data-toggle="modal" data-target="#modalSupplierNotes">' +
								'<span class="powidth"><a target="_blank" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '">' + full.supplierName + '</i></a></span>';
						}
					}
					return data;
				}
			},
			{
				className: "d-sm-none data-icon-align width-auto tooglebtn",
				orderable: false,
				data: null,
				render: function (data, type, full, meta) {
					if (authoriseQuoteAccess != null && authoriseQuoteAccess != 'undefined' && authoriseQuoteAccess != '') {
						if (full.isValidForEnquiry && authoriseQuoteAccess.permission) {
							return '<div class="btn-group"><button type="button" class="btn dropdown-toggle supplierToggler" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fas fa-th-large txt-green-common-text font-size-lg mt-1" aria-hidden="true"></i></button> <ul class="dropdown-menu supplier-dropdown" role="menu"><li><button class="btn btn-icon"><a class="authorizeQuote text-black"> <i class="fa fa-check-circle cursor-pointer" title="Authorize Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">Authorize Quote</span></i></a></button></li><li><button class="btn btn-icon"> <a class="text-black" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '"><i class="fa fa-eye" title="View Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">View Quote</span></i></a> </button></li></ul></div>';
						}
						else {
							return '<div class="btn-group"><button type="button" class="btn dropdown-toggle supplierToggler" data-toggle="dropdown"><i class="fas fa-th-large txt-green-common-text font-size-lg mt-1" aria-hidden="true"></i></button> <ul class="dropdown-menu supplier-dropdown" role="menu"><li><button class="btn btn-icon"> <a class="text-black" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '"><i class="fa fa-eye" title="View Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">View Quote</span></i></a> </button></li></ul></div>';
						}
					}
					else {
						return '<div class="btn-group"><button type="button" class="btn dropdown-toggle supplierToggler" data-toggle="dropdown"><i class="fas fa-th-large txt-green-common-text font-size-lg mt-1" aria-hidden="true"></i></button> <ul class="dropdown-menu supplier-dropdown" role="menu"><li><button class="btn btn-icon"> <a class="text-black" href="/PurchaseOrder/ViewQuote?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '&supplierOrderId=' + full.protectedSupplierOrderId + '"><i class="fa fa-eye" title="View Quote" data-placement="bottom" aria-hidden="true"><span class="ml-1">View Quote</span></i></a></button></li></ul></div>';
					}
				}
			},
			{
				className: "tdblock data-icon-align",
				name: "Rating",
				data: "rating",
				orderable: false,
				width: "110px",
				render: function (data, type, full, meta) {

					var htnmlrating = '<div class="btn-group ratingdesign">';
					htnmlrating += '<button type="button" class="p-0 btn btn-icon supplierRatingToggler" data-suppname="' + full.supplierName + '" data-compid="' + full.supplierCompanyId + '">';
					htnmlrating += GetRatingdesign(full);
					htnmlrating += '</button><div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-shadow dropdown-menu rating-popup" id="Rating_' + full.supplierCompanyId + '"></div></div>';

					return htnmlrating;
				}
			},
			{
				className: "d-none d-sm-table-cell data-icon-align",
				"data": "status",
				name: "Status",
				width: "35px",
				orderable: false,
				render: function (data, type, full, meta) {
					var status = "";
					if (full.supplierOrderStatus != null) {
						if (full.orderStage == 'Purchase Order') {
							if (full.isOrderAuthorised == true) {
								status = '<span class="badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'EP' || full.orderStatus == 'EO') {
							if (full.supplierOrderStatus == 'TI' || full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								status = '<span class="badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TX') {
								status = '<span class="badge badge-pill status-badge badge-danger">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TH') {
								status = '<span class="badge badge-pill status-badge badge-amber">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
							if (full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								status = '<span class="badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TX') {
								status = '<span class="badge badge-pill status-badge badge-danger">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TH') {
								status = '<span class="badge badge-pill status-badge badge-amber">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'TR') {
							if (full.supplierOrderStatus == 'TR' || full.supplierOrderStatus == 'O') {
								status = '<span class="badge badge-pill status-badge badge-success">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
							}
						}
						else {
							status = '<span class="badge badge-pill status-badge badge-warning">' + full.supplierOrderStatus + '</span>';
						}
					}
					var statusWithTooltip = '<span data-toggle="tooltip" title="' + full.supplierOrderStatusName + '" data-placement="bottom">' + status + '</span>';
					return GetCellData('Status', statusWithTooltip);
				}
			},
			{
				className: "data-text-align td-w-27",
				name: "Country",
				data: "country",
				width: "55px",
				render: function (data, type, full, meta) {
					return GetCellData('Country', full.country);
				}
			},
			{
				className: "data-icon-align td-w-36",
				"data": "isMarcas",
				name: "Type",
				width: "50px",
				orderable: false,
				render: function (data, type, full, meta) {
					var type = "";
					if (full.isMarcas == true) {
						type = '<span class="badge badge-pill status-badge badge-low mr-1" data-toggle="tooltip" data-placement="bottom" title="Marcus">M</span>';
					}
					if (full.isPreferred == true) {
						type = type + '<span class="badge badge-pill status-badge badge-salmonPink mr-1" title="Preferred" data-toggle="tooltip" data-placement="bottom">P</span>';
					}
					if (full.isContracted == true) {
						type = type + '<span class="badge badge-pill status-badge badge-low mr-1" data-toggle="tooltip" data-placement="bottom" title="Contract">C</span>';
					}
					return GetCellData('Type', type);
				}
			},
			{
				className: "data-icon-align td-w-36",
				"data": "isCompleteQuote",
				name: "Complete",
				width: "35px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var complete = "";
						if (full.isCompleteQuote == true) {
							complete = '<i class="fa fa-check text-green" aria-hidden="true"></i>';
						}
						else {
							complete = '<i class="fa fa-times text-low" aria-hidden="true"></i>';
						}
						return GetCellData('Full Quote', complete);
					}
					return data; // This is a unformatted data  
				}
			},
			{
				className: "data-number-align",
				name: "MaxExWorksDays",
				data: "maxExWorksDays",
				width: "56px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('Max Ex-Works Days', full.maxExWorksDays);
					}
					return data; // This is a unformatted data  
				}
			},
			{
				className: "data-datetime-align",
				name: "QuoteReceivedDate",
				data: "quoteReceivedDate",
				width: "65px",
				type: "date",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("DD MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Quote Received', formattedDate);
					}
					return date;
				}
			},
			{
				className: "data-number-align width-auto td-min-w-32",
				name: "Total",
				data: "total",
				width: "60px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('Total', full.total);
					}
					return data;
				}
			},
			{
				className: "data-text-align td-w-16",
				name: "Cur",
				data: "cur",
				width: "30px",
				render: function (data, type, full, meta) {
					return GetCellData('CUR', full.cur);
				}
			},
			{
				className: "data-number-align width-auto basewidth",
				name: "Base",
				data: "base",
				width: "65px",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var base = data;
						if (full.baseCurrency != null) {

							baseLabel = 'Base (' + full.baseCurrency + ')';
						}
						else {
							baseLabel = 'Base';
						}
						if (full.isBaseTotalNotMatched == true) {
							base = '<i class="fa fa-exclamation-triangle text-danger float-left py-1 mr-1" aria-hidden="true" title="Supplier order lines total cost value does not match with supplier order value. Please check supplier notes and quantities quoted." data-toggle="tooltip" data-placement="bottom"></i>' + full.base;
						}
						else {
							base = full.base;
						}
						return GetCellData(baseLabel, base);
					}
					return data;
				}
			},
		],
	});

	$('#dtSuppliersTab tbody').on('click', 'img.supplierNotes', function () {
		var data = dtSuppliersTab.row($(this).parents('tr')).data();
		OpenModalSuppierNote(data);
	});

	$("#dtSuppliersTab tbody").unbind('click');
	$('#dtSuppliersTab tbody').on('click', 'a.authorizeQuote', function () {
		var data = dtSuppliersTab.row($(this).parents('tr')).data();
		AuthorizeQuote(data);
	});

	$('#dtSuppliersTab').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip();
	});

}

//Open wizard
function AuthorizeQuote(selectedItem) {
	//storinig SupplierOrderId
	SupplierOrderId = selectedItem.supplierOrderId;
	$.ajax({
		url: "/PurchaseOrder/GetQuoteAuthorizeWizard",
		type: "POST",
		"data": {
			request: selectedItem.authorizeQuoteRequestUrl
		},
		"datatype": "JSON",
		success: function (data) {
			if (data.response == true) {
				if (data.supplierDetails.isSupplierGivenNotes) {
					$('#textReadNotes').val('');
					$('#textReadNotes').val(data.supplierDetails.supplierNotes);
				}
				else {
					$('#textReadNotes').val('');
				}
				//setting global value 
				IsSupplierNotesGiven = data.supplierDetails.isSupplierGivenNotes;
				IsFeedbackRequired = data.supplierDetails.isFeedbackRequired;
				FeedbackSupplierOrderId = data.supplierDetails.feedbackSupplierOrderId;
				LoadWizardSetup(data.supplierDetails.isSupplierGivenNotes, data.supplierDetails.isFeedbackRequired);

				//Load Details Data here
				if (data.supplierDetails.isStatusTR) {
					$('#spanOrderStatusImportantMsg').text(data.supplierDetails.supplierOrderStatusWarning);
				}
				else {
					$('#divSupplierOrderStatus').hide();
				}

				$('#spanSupplierOrderName').text(data.orderDetails.orderTitle);
				$('#spanSupplierOrderSupplierName').text(data.supplierDetails.supplierName);
				$('#spanSupplierPortName').text(data.supplierDetails.portName);
				$('#spanSupplierExpectedCountry').text(data.supplierDetails.expectedWorkCountry);
				$('#spanSupplierExpectedWorks').text(data.supplierDetails.expectedWorkDays);
				$('#spanSupplierHazGoods').text(data.supplierDetails.isHazardousGoods);
				$('#spanSupplierSpareParts').text(data.supplierDetails.sparePartTypeDetail);
				$('#spanSupplierItemsQuoted').text(data.supplierDetails.fullItemsQuoted);
				if (data.supplierDetails.isFullItemsQuoted) {
					$('#spanSupplierItemsQuoted').addClass('text-success')
				}
				else {
					$('#spanSupplierItemsQuoted').addClass('text-danger')
				}

				if (data.supplierDetails.isItemsNotQuoted) {
					$('#divItemsNotQuoted').show();
					$('#spanSupplierItemsNotQuoted').text(data.supplierDetails.itemsNotQuotedCount);
				}
				else {
					$('#divItemsNotQuoted').hide();
				}

				$('#spanSupplierProformaReq').text(data.supplierDetails.isProformaRequested);

				//binding of supplier authorize validation message
				if (data.supplierDetails.isAuthLevelAuthorizationRequired || data.supplierDetails.isClientAuthRequired) {
					$('#divAdditionalApprovals').show();
				}
				else {
					$('#divAdditionalApprovals').hide();
				}

				//binding supplier details pop up here
				$('#aSupplierDetails').click(function () {
					LoadSupplierAdditionalInfo(data.supplierDetails.encryptedSupplierCompanyId);
				});

				//Quote Section
				$('#spanSupplierQuotedAmount').text(data.supplierDetails.quotedAmount);

				//Loadvesselbudget details and assign value there only because it will be called on onchange of accound code also
				QuoteAmountInPoVesselCurrencyDecimal = data.supplierDetails.quoteAmountInPoVesselCurrencyDecimal;
				FreightAccrualInPoVesselCurrencyDecimal = data.supplierDetails.freightAccrualInPoVesselCurrencyDecimal;
				AccountingCompanyIdForVesselBudget = data.orderDetails.accountingCompanyId;

				OrderNumberForVesselBudget = data.orderDetails.orderNumber;
				AccountIdForVesselBudget = data.orderDetails.accountId;
				LoadVesselBudget(data.orderDetails.accountingCompanyId, data.orderDetails.orderNumber, data.orderDetails.accountId);

				LoadApplicableAuxFlags(data.orderDetails.accountingCompanyId, data.orderDetails.accountId);

				//Based on applicable aux flag neload default lookup and initialise them

				//Insurance Lookup
				LoadInsuranceclaimLookup(data.orderDetails.accountingCompanyId);
				SetSelectedInsuranceClaim(data.orderDetails.auxiliaries.claimsId, data.orderDetails.auxiliaries.claimsDescription, data.orderDetails.auxiliaries.claimsShortCode);

				//Seasonal Lookup
				LoadSeasonalLookup();
				SetSelectedSeasonalLookup(data.orderDetails.auxiliaries.seasonalId, data.orderDetails.auxiliaries.seasonalDescription);

				//Nationality Lookup
				LoadNationalityLookup();
				SetSelectedNationalityLookup(data.orderDetails.auxiliaries.nationalityId, data.orderDetails.auxiliaries.nationalityDescription);

				//Crew Rank Lookup
				LoadCrewRankLookup();
				SetSelectedCrewRankLookup(data.orderDetails.auxiliaries.crewRankId, data.orderDetails.auxiliaries.crewRankDescription);

				//Vessel Lookup
				LoadVesselAuxLookup();
				SetSelectedVesselAuxLookup(data.orderDetails.auxiliaries.vesselManagementId, data.orderDetails.auxiliaries.vesselName);

				//General 1 Aux Lookup
				LoadGeneral1AuxLookup();
				SetSelectedGeneral1AuxLookup(data.orderDetails.auxiliaries.general1Id, data.orderDetails.auxiliaries.general1Description);

				//General 3 Aux Lookup
				LoadGeneral3AuxLookup();
				SetSelectedGeneral3AuxLookup(data.orderDetails.auxiliaries.general3Id, data.orderDetails.auxiliaries.general3Description);

				var vesselCurrencyId = '(' + data.orderDetails.vesselCurrencyId + ')';
				$('#spanSupplierBudgetCurrency').text(vesselCurrencyId);
				$('#spanSupplierActualCurrency').text(vesselCurrencyId);

				//Vessel Office Limit Pop up opens here:
				$('#aLimitLevels').click(function () {
					LoadVesselOfficeLimit(selectedItem.authorizeQuoteRequestUrl);
				});

				//accrual section here:                
				$('#spanPOINVCurrency').text(vesselCurrencyId);
				$('#spanOrderAccrual').text(data.supplierDetails.quoteAmountInPoVesselCurrency);
				$('#spanOrderAccrualVesselCurrency').text(vesselCurrencyId);
				$('#spanFrieghtAccrual').text(data.supplierDetails.freightAccrualInPoVesselCurrency);
				$('#spanFrieghtAccrualVesselCurrency').text(vesselCurrencyId);
				$('#spanProjectAccrualVesselCurrency').text(vesselCurrencyId);

				//Load vessel account code
				AccountCodeIdentifier = data.orderDetails.accountId;
				AccountCodeDescription = data.orderDetails.accountDescription;
				LoadAccountVesselCode(data.orderDetails.vesselId, data.orderDetails.accountingCompanyId, data.isVesselInManagement);
				LoadSelectedValueInSelect2();
				if (data.supplierDetails.isSupplierGivenNotes) {
					if (!$('#next-btnAuthorizeQuote').hasClass('disabled')) {
						$('#next-btnAuthorizeQuote').addClass('disabled');
					}
				}
				else if (data.supplierDetails.isFeedbackRequired) {
					if ($('#next-btnAuthorizeQuote').hasClass('disabled')) {
						$('#next-btnAuthorizeQuote').removeClass('disabled');
					}
				}

				//--------- STEP 3
				if (data.supplierDetails.isFeedbackRequired) {
					LoadFeedbackReason();
				}

				//show wizard setup popup				
				$('#modalAuthorizeQuote').modal("show");
			}
			else if (data.response == false) {
				ToastrAlert("validate", data.message);
				//$('#modalAuthoriseAlert').modal("show");
				//$('#pAlertMessage').text(data.message);
			}

		}
	});
	
	$("#spanSupplierItemsNotQuoted").off();
	$("#spanSupplierItemsNotQuoted").click(function () {
		LoadUnquotedItems(SupplierOrderId);
	});
}

//To check if aux should be loaded or not
//Adding conditional check - IsAuxillaryApplicable -> show/hide of aux columns
//Adding indivisual aux check
function LoadApplicableAuxFlags(accountingCompanyId, accountId) {
	$.ajax({
		url: "/PurchaseOrder/GetAuxByAccCodeAndCoyId",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"accountId": accountId
		},
		success: function (data) {
			if (data != null) {
				IsAuxillaryApplicable = data.isAuxillaryApplicable;
				if (data.isAuxillaryApplicable) {
					$('#divAuxLookup').show();

					if ($('#divAuthQuoteDetails').hasClass('col-lg-12')) {
						$('#divAuthQuoteDetails').removeClass('col-lg-12');
						$('#divAuthQuoteDetails').addClass('col-lg-6');
					}

					if ($('#divAuthQuoteDetails').hasClass('col-xl-12')) {
						$('#divAuthQuoteDetails').removeClass('col-xl-12');
						$('#divAuthQuoteDetails').addClass('col-xl-6');
					}

					IsInsuaranceClaimAuxApplicable = data.auxClaims;
					if (data.auxClaims) {
						$('#divInsuaranceClaimAux').show();
					}
					else {
						$('#divInsuaranceClaimAux').hide();
					}

					IsSeasonalAuxApplicable = data.auxSeasonal;
					if (data.auxSeasonal) {
						$('#divSeasonalAux').show();
					}
					else {
						$('#divSeasonalAux').hide();
					}

					IsNationalityAuxApplicable = data.auxNationality;
					if (data.auxNationality) {
						$('#divNationalityAux').show();
					}
					else {
						$('#divNationalityAux').hide();
					}

					IsCrewRankAuxApplicable = data.auxRank;
					if (data.auxRank) {
						$('#divCrewRankAux').show();
					}
					else {
						$('#divCrewRankAux').hide();
					}

					IsVesselAuxApplicable = data.auxVessel
					if (data.auxVessel) {
						$('#divVesselAux').show();
					}
					else {
						$('#divVesselAux').hide();
					}

					IsGeneral1AuxApplicable = data.auxGeneral1;
					if (data.auxGeneral1) {
						$('#divGeneral1Aux').show();
					}
					else {
						$('#divGeneral1Aux').hide();
					}

					IsGeneral3AuxApplicable = data.auxGeneral3;
					if (data.auxGeneral1) {
						$('#divGeneral3Aux').show();
					}
					else {
						$('#divGeneral3Aux').hide();
					}
				}
				else {
					$('#divAuxLookup').hide();

					if ($('#divAuthQuoteDetails').hasClass('col-xl-6')) {
						$('#divAuthQuoteDetails').removeClass('col-xl-6');
						$('#divAuthQuoteDetails').addClass('col-xl-12');
					}
					if ($('#divAuthQuoteDetails').hasClass('col-lg-6')) {
						$('#divAuthQuoteDetails').removeClass('col-lg-6');
						$('#divAuthQuoteDetails').addClass('col-lg-12');
					}
				}
			}
		}
	});
}

function LoadUnquotedItems(supplierOrderId) {
	$('#dtNonQuotedOrderLines').DataTable().destroy();
	dtNonQuotedOrderLines = $('#dtNonQuotedOrderLines').DataTable({
		"dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"scrollY": "300px",
		"pageLength": 10,
		"order": [],
		"language": {
			"emptyTable": "No data available.",
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetSupplierQuoteOrderLinesUnQuoted",
			"type": "POST",
			"data": {
				"supplierOrderId": supplierOrderId
			}
		},
		"columns": [
			{
				className: "d-none d-sm-table-cell text-right",
				width: "20px",
				data: "itemNo",
				name: "ItemNo",
				render: function (data, type, full, meta) {
					return GetCellData('#', full.itemNo);
				}
			},
			{
				className: "tdblock td-row-header text-left",
				data: "partName",
				name: "PartName",
				render: function (data, type, full, meta) {
					var ibutton = "";
					if (full.vesselPartId != null && full.vesselPartId != "") {
						ibutton = '&nbsp' + '<i class="fa fa-info-circle cursor-pointer open-modal-btn partNameIbutton"></i></a>'
					}
					return '<span class="d-inline-block d-sm-none">' + full.itemNo + '&nbsp;' + '</span>' + full.partName + ibutton;
				}
			},
			{
				className: "text-left",
				data: "makersReference",
				name: "MakersReference",
				render: function (data, type, full, meta) {
					return GetCellData('Makers Reference', data);
				}
			},
			{
				className: "text-right width-auto",
				width: "65px",
				name: "QtyEnq",
				"data": "qtyEnq",
				render: function (data, type, full, meta) {
					return GetCellData('Qty Enq', full.qtyEnq);
				}
			},
		],
		"initComplete": function () {
			$('#unQuoteCountDt').text(this.api().data().length)
		}
	})

	$('#dtNonQuotedOrderLines tbody').on('click', 'i.partNameIbutton', function () {
		var data = dtNonQuotedOrderLines.row($(this).parents('tr')).data();
		ShowVesselPartDetails(data.vesselPartId);
	});
}

function ShowVesselPartDetails(vesselPartId) {

	$.ajax({
		url: "/PurchaseOrder/PostGetVesselPartDetail",
		type: "POST",
		data: {
			"vesselPartId": vesselPartId
		},
		success: function (data) {
			$('#partNameModal').text(data.partName);
			$('#spanUOM').text(data.unitOfMeasurement);
			$('#spanMakerReferenceNumber').text(data.makerReferenceNumber);
			$('#spanPlateSheetNumber').text(data.plateSheetNumber);
			$('#spanDrawingPosition').text(data.drawingPosition);
			$('#spanHarNumber').text(data.harmonizedNumber);
			$('#spanHarWeight').text(data.harmonizedWeight);
			$('#spanStockItem').text(data.isStockItem);
			$('#spanROB').text(data.recommendedROB);
			$('#spanTechMinStock').text(data.technicalMinStock);
			$('#spanOperationalMinStock').text(data.operationalMinStock);
			$('#spanCertificateReq').text(data.isCertificateRequired);
			$('#spanCertificateName').text(data.certificateName);
			$('#spanDangerousGoods').text(data.isDangerousGoods);
			$('#spanDescription').text(data.dangerousGoodsDescription);
			if (data.weight != null && data.weight != "") {
				$('#spanWeight').text(GetBlankOrValue(data.weight) + ' ' + GetBlankOrValue(data.weightUnit));
			}
			if (data.volume != null && data.volume != "") {
				$('#spanVolume').text(GetBlankOrValue(data.volume) + ' ' + GetBlankOrValue(data.volumeUnit));
			}
			if (data.shelfLifeDuration != null && data.shelfLifeDuration != "") {
				$('#spanShelfLifeDuration').text(GetBlankOrValue(data.shelfLifeDuration) + ' ' + GetBlankOrValue(data.shelfLifeDurationUnit));
			}
			$("#vesselPartDetailModal").modal("show");
		}
	})
}
function GetBlankOrValue(data) {
	if (data == null || data == "") {
		return "";
	}
	return data;
}

//vessel office limit pop up
function LoadVesselOfficeLimit(authorizeQuoteRequestUrl) {
	$.ajax({
		url: "/PurchaseOrder/GetVesselAndOfficeLimit",
		type: "POST",
		"data": {
			request: authorizeQuoteRequestUrl
		},
		success: function (data) {
			$('#spanVOVesselLimit').text(data.vesselLimit);
			$('#spanVOOfficeLimit').text(data.officeLimit);
			$('#spanVOLevel1Limit').text(data.level1Limit);
			$('#spanVOLevel2Limit').text(data.level2Limit);
			$('#spanVOLevel3Limit').text(data.level3Limit);
			$('#spanVOClientLimit').text(data.clientLimit);
			$('#modalVesselOfficeLimit').modal("show");
		}
	});
}

//vessel budget info
function LoadVesselBudget(accountingCompanyId, orderNumber, accountId) {
	$.ajax({
		url: "/PurchaseOrder/GetVesselAccountBudget",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber,
			"accountId": accountId
		},
		success: function (data) {
			$('#spanSupplierBudget').text(data.budgetAmountAllocated);
			$('#spanSupplierActual').text(data.budgetActualSpend);
			$('#spanPOINVAccrual').text(data.totalAccruals);
			TotalAccruals = data.totalAccrualsDecimal
			var ProjectedAccrual = data.totalAccrualsDecimal + FreightAccrualInPoVesselCurrencyDecimal + QuoteAmountInPoVesselCurrencyDecimal;
			var totalExpenses = data.budgetActualSpendDecimal + ProjectedAccrual;
			IsBudgetExceed = (totalExpenses > data.budgetAmountAllocatedDecimal);
			if (IsBudgetExceed) {
				$('#divProjectAccrualExceeds').show();
				$('#divJustification').show();
				$('#divProjectAccruals').addClass('text-danger');
				var ProjectedAccrualText = "= " + GetFormattedDecimal('', '', ProjectedAccrual, 2, '0.00');
				$('#spanProjectAccrual').text(ProjectedAccrualText);

				//Justicfication reason drop down binding
				LoadJustificationReasons();

			}
			else {
				$('#divProjectAccrualExceeds').hide();
				$('#divJustification').hide();
				$('#divProjectAccruals').addClass('text-success');
				var ProjectedAccrualText = "= " + GetFormattedDecimal('', '', ProjectedAccrual, 2, '0.00');
				$('#spanProjectAccrual').text(ProjectedAccrualText);
			}
		}
	});
}

//vessel account code lookup
function LoadAccountVesselCode(vesselId, accountCompanyId, isVesselInManagement) {
	headerIsAppend = false;
	$("#cboVessAccountCode").select2({
		theme: "bootstrap4",
		placeholder: "Enter Account code or name",
		dropdownCssClass: IsMobile ? '' : 'aux-lookup-bigdrop',
		minimumInputLength: 1,
		ajax: {
			url: '/PurchaseOrder/GetVesselAccountCode',
			delay: 2000,
			dataType: 'json',
			data: function (params) {
				return {
					term: params.term,
					vesselId: vesselId,
					accountCompanyId: accountCompanyId,
					isVesselInPurchasingManagement: isVesselInManagement
				};
			},
		},
		templateResult: formatResult,
		templateSelection: formatRepoSelection
	});

	$('#cboVessAccountCode').on('select2:opening', function (e) {
		if (!headerIsAppend) {
			try {
				var selectedDropdown = $('.select2-search--dropdown input').attr("aria-controls");
				if (selectedDropdown != null && selectedDropdown != '' && selectedDropdown != 'undefined') {
					var arr = selectedDropdown.split("-");
					var createdId = '#' + arr[1];
					$(createdId).select2('close');
				}
			} catch (ex) {
				console.log('error ' + ex);
			}
		}
	});

	$('#cboVessAccountCode').on('select2:open', function (e) {
		if (!IsMobile) {
			if (!headerIsAppend) {
				$('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Account Code</th>\
		                  <th width="50%">Account Name</th>\
                        </tr>\
		              </thead>\
		             </table>');
				$('.select2-results').addClass('stock');

				headerIsAppend = true;
			}
		}
	});

	function formatResult(result) {
		if (result.loading)
			return "Searching...";

		var $result;

		if (IsMobile) {
			if (result != undefined) {
				$result = $('<div class="row select2-row">' +
					'<div class="col-sm-6"><b>Account Code : </b>' + result.identifier + '<b> | Account Name : </b>' + result.description + '</div>');
			}
		}
		else {
			if (result != undefined) {
				$result = $('<table class="table table-bordered p-0 m-0">\
								 <tbody>\
									<tr>\
                                        <td width="50%">' + result.identifier + '</td>\
										<td width="50%">' + result.description + '</td>\
                                    </tr>\
								</tbody>\
							</table>');
			}
		}
		return $result;
	}

	function formatRepoSelection(repo) {
		return repo.id + " - " + repo.text;
	}

}

//Load selected value in select2
function LoadSelectedValueInSelect2() {
	if (AccountCodeIdentifier != null && AccountCodeIdentifier != '' && AccountCodeIdentifier != 'undefined' && AccountCodeDescription != null && AccountCodeDescription != '' && AccountCodeDescription != 'undefined') {
		var selectedAccountCode = new Option(AccountCodeDescription, AccountCodeIdentifier, true, true);
		$('#cboVessAccountCode').append(selectedAccountCode).trigger('change');
	}
}

//justification reason drop down - BudgetJustification
function LoadJustificationReasons() {
	var selectReasons = document.getElementById("selectReasons");
	var selectReasonsLength = selectReasons.options.length;
	for (var i = selectReasonsLength - 1; i >= 0; i--) {
		selectReasons.options[i] = null;
	}

	$.ajax({
		url: "/PurchaseOrder/GetCompanyRejectionReasons",
		type: "POST",
		"data": {
			"rejectionReasonType": "B"
		},
		"datatype": "JSON",
		success: function (data) {
			var select = document.getElementById('selectReasons');

			var option = document.createElement('option');
			option.selected = true;
			option.value = "";
			option.innerHTML = "";
			select.appendChild(option);

			for (var i = 0; i < data.length; i++) {
				var obj = data[i];
				var opt = document.createElement('option');
				if ($('#SelectedJustificationReason').val() == obj.identifier) {
					opt.selected = true;
				}
				else {
					opt.selected = false;
				}
				opt.value = obj.identifier;
				opt.innerHTML = obj.description;
				opt.setAttribute('data-select2-id', obj.identifier);
				select.appendChild(opt);
			}
		}
	});
}

//feedback reason drop down - Rejection
function LoadFeedbackReason() {
	var selectFeedbackReason = document.getElementById("selectFeedbackReason");
	var selectFeedbackReasonLength = selectFeedbackReason.options.length;
	for (var i = selectFeedbackReasonLength - 1; i >= 0; i--) {
		selectFeedbackReason.options[i] = null;
	}

	$.ajax({
		url: "/PurchaseOrder/GetCompanyRejectionReasons",
		type: "POST",
		"data": {
			"rejectionReasonType": "R"
		},
		"datatype": "JSON",
		success: function (data) {
			var select = document.getElementById('selectFeedbackReason');

			var option = document.createElement('option');
			option.selected = true;
			option.value = "";
			option.innerHTML = "";
			select.appendChild(option);

			for (var i = 0; i < data.length; i++) {
				var obj = data[i];
				var opt = document.createElement('option');
				opt.value = obj.identifier;
				opt.innerHTML = obj.description;
				opt.setAttribute('data-select2-id', obj.identifier);
				select.appendChild(opt);
			}
		}
	});
}

//supplier pop up
function LoadSupplierAdditionalInfo(encryptedSupplierCompanyId) {
	$.ajax({
		url: "/PurchaseOrder/GetAddressDetails",
		type: "POST",
		"data": {
			"selectedStatus": 'Supplier',
			"encryptedId": encryptedSupplierCompanyId
		},
		success: function (data) {

			$('#modalHeader').text('Supplier Details');

			if (data.isCompanyAddressVisible) {
				$("#divCompanyAddress").removeClass('d-none');
				$('#spanCompanyAddress').text(data.companyAddress);
			}

			$('#spanLocalAddres').text(data.companyLocalAddress);

			if (data.isCountryVisible) {
				$("#divCountry").removeClass('d-none');
				$('#spanCountryName').text(data.companyCountryDesc);
				$("#imgCountryFlag").attr("src", "/images/Flags/" + data.countryCode + ".png");
			}

			if (data.isTelephoneVisible) {
				$("#divPOTelephone").removeClass('d-none');
				$('#spanSupplierTelephone').text(data.telephoneNumber);
			}

			if (data.isMobileVisible) {
				$("#divPOMobile").removeClass('d-none');
				$('#spanMobile').text(data.mobileNumber);
			}

			if (data.isFaxVisible) {
				$("#divPOFax").removeClass('d-none');
				$('#spanSupplierFax').text(data.faxNumber);
			}

			if (data.isEmailVisible) {
				$("#divPOEmail").removeClass('d-none');
				$('#spanSupplierEmail').text(data.email);
				$("#aEmail").attr("href", 'mailto:' + data.email);
			}

			if (data.isWebAddressVisible) {
				$("#divPOWeb").removeClass('d-none');
				$('#spanWeb').text(data.webAddress);
				$("#aWebAddr").attr("href", 'http://' + data.webAddress);
				$("#aWebAddr").attr('target', '_blank')
			}

			if (data.isProcurmentVisible) {
				$("#divPOProcurment").removeClass('d-none');
				$('#spanProcurment').text(data.procurmentType);
			}

			$('#spanIsCompanyCertified').text(data.isCompanyCertified);

		}
	});
	$('#supplierModal').modal("show");
}

function LoadSuppliersList() {
	var purchaseOrderRequestURL = $('#PurchaseOrderRequest').val();
	$('#dtSuppliersList').DataTable().destroy();
	dtSuppliersList = $('#dtSuppliersList').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		"order": [],
		"language": {
			"emptyTable": "No supplier available.",
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetSuppliers",
			"type": "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber,
				"fetchSupplierOrderLineTotalCost": true,
				"purchaseOrderRequestURL": purchaseOrderRequestURL
			},
			"datatype": "json"
		},
		"initComplete": function (settings, json) {
			$(dtSuppliersList.column(6).header()).text(supplierListBaseLabel);
		},
		"columns": [
			{
				className: "d-none d-sm-table-cell data-icon-align",
				"data": "status",
				name: "Status",
				width: "30px",
				render: function (data, type, full, meta) {
					var status = "";
					if (full.supplierOrderStatus != null) {
						if (full.orderStage == 'Purchase Order') {
							if (full.isOrderAuthorised == true) {
								status = '<span class="badge badge-pill status-badge badge-success" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'EP' || full.orderStatus == 'EO') {
							if (full.supplierOrderStatus == 'TI' || full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								status = '<span class="badge badge-pill status-badge badge-success" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TX') {
								status = '<span class="badge badge-pill status-badge badge-danger" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TH') {
								status = '<span class="badge badge-pill status-badge badge-amber" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
							if (full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								status = '<span class="badge badge-pill status-badge badge-success" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TX') {
								status = '<span class="badge badge-pill status-badge badge-danger" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TH') {
								status = '<span class="badge badge-pill status-badge badge-amber" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'TR') {
							if (full.supplierOrderStatus == 'TR' || full.supplierOrderStatus == 'O') {
								status = '<span class="badge badge-pill status-badge badge-success" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
							}
						}
						else {
							status = '<span class="badge badge-pill status-badge badge-warning" title="' + full.supplierOrderStatusName + '" data-toggle="tooltip" data-placement="bottom">' + full.supplierOrderStatus + '</span>';
						}
					}
					return GetCellData('Status', status);
				}
			},
			{
				className: "tdblock td-row-header data-text-align",
				name: "SupplierName",
				data: "supplierName",
				width: "500px",
				orderable: false,
				render: function (data, type, full, meta) {
					var status = "";
					if (full.supplierOrderStatus != null) {
						if (full.orderStage == 'Purchase Order') {
							if (full.isOrderAuthorised) {
								status = '<span class="badge badge-pill status-badge badge-success d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'EP' || full.orderStatus == 'EO') {
							if (full.supplierOrderStatus == 'TI' || full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								status = '<span class="badge badge-pill status-badge badge-success d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TX') {
								status = '<span class="badge badge-pill status-badge badge-danger d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TH') {
								status = '<span class="badge badge-pill status-badge badge-amber d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
							if (full.supplierOrderStatus == 'TA' || full.supplierOrderStatus == 'TQ') {
								status = '<span class="badge badge-pill status-badge badge-success d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TX') {
								status = '<span class="badge badge-pill status-badge badge-danger d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else if (full.supplierOrderStatus == 'TH') {
								status = '<span class="badge badge-pill status-badge badge-amber d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
						}
						else if (full.orderStatus == 'TR') {
							if (full.supplierOrderStatus == 'TR' || full.supplierOrderStatus == 'O') {
								status = '<span class="badge badge-pill status-badge badge-success d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
							else {
								status = '<span class="badge badge-pill status-badge badge-warning d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
							}
						}
						else {
							status = '<span class="badge badge-pill status-badge badge-warning d-inline-block d-sm-none mr-2">' + full.supplierOrderStatus + '</span>';
						}
					}
					if (full.supplierOrderStatus) {
						return status + full.supplierName;
					}
					else {
						return full.supplierName;
					}
				}
			},
			{
				className: "data-number-align",
				name: "GoodsCost",
				data: "goodsCost",
				width: "100px",
				render: function (data, type, full, meta) {
					var goodsCost = "";
					if (full.isDiscountApplied == true) {
						goodsCost = '<i class="fa fa-exclamation-triangle text-danger float-left py-1 mr-2" aria-hidden="true" title="Discount applied" data-toggle="tooltip" data-placement="bottom"></i>' + full.goodsCost;
					}
					else {
						goodsCost = full.goodsCost;
					}
					return GetCellData('Goods', goodsCost);
				}
			},
			{
				className: "data-number-align",
				name: "FreightCost",
				data: "freightCost",
				width: "70px",
				render: function (data, type, full, meta) {
					return GetCellData('Freight', full.freightCost);
				}
			},
			{
				className: "data-number-align",
				name: "Total",
				data: "total",
				width: "70px",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Total', full.total);
				}
			},
			{
				className: "data-text-align",
				name: "Cur",
				data: "cur",
				width: "40px",
				render: function (data, type, full, meta) {
					return GetCellData('CUR', full.cur);
				}
			},
			{
				className: "data-number-align",
				name: "Base",
				data: "base",
				width: "70px",
				orderable: false,
				render: function (data, type, full, meta) {
					if (full.baseCurrency != null) {

						supplierListBaseLabel = 'Base (' + full.baseCurrency + ')';
					}
					else {
						supplierListBaseLabel = 'Base';
					}
					return GetCellData(supplierListBaseLabel, full.base);
				}
			},
		],
	});

	$('#dtSuppliersList').on('draw.dt', function () {
		$('[data-toggle="tooltip"]').tooltip(
			{
				container: 'body'
			}
		);
	});
}

function OpenModalSuppierNote(result) {
	LoadSupplierOrderLine(result.protectedSupplierOrderId);
	$('#spanSupplierName').text(result.supplierName);
	$('#spanCurrency').text(result.cur);
	$('#spanPortNameSupplierNotes').text(result.portName);
	$('#txtAreaNote').text(result.supplierNotes);
}

function LoadSupplierOrderLine(supplierOrderId) {
	$('#dtOrderLineDetails').DataTable().destroy();
	gridSupplierOrderLines = $('#dtOrderLineDetails').DataTable({
		"dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"scrollY": "180px",
		"scrollCollapse": true,
		"processing": false,
		"serverSide": false,
		"lengthChange": true,
		"searching": true,
		"info": true,
		"autoWidth": false,
		"paging": false,
		"pageLength": 25,
		"order": [],
		"language": {
			"emptyTable": "No supplier order available.",
			"search": "_INPUT_",
			"searchPlaceholder": "Search",
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetSupplierQuoteOrderLines",
			"type": "POST",
			"data": {
				"supplierOrderId": supplierOrderId
			}
		},
		"columns": [
			{
				className: "d-none d-sm-table-cell text-left",
				width: "20px",
				data: "itemNo",
				name: "ItemNo",
				render: function (data, type, full, meta) {
					return GetCellData('#', full.itemNo);
				}
			},
			{
				className: "tdblock td-row-header",
				data: "partName",
				name: "PartName",
				render: function (data, type, full, meta) {
					return '<span class="d-inline-block d-sm-none">' + full.itemNo + '&nbsp;' + '</span>' + full.partName;
				}
			},
			{
				className: "text-right width-auto",
				width: "65px",
				name: "QtyEnq",
				"data": "qtyEnq",
				render: function (data, type, full, meta) {
					return GetCellData('Qty Enq', full.qtyEnq);
				}
			},
			{
				className: "text-right width-auto",
				name: "SupplierQty",
				"data": "supplierQty",
				width: "65px",
				render: function (data, type, full, meta) {
					var type = "";
					if (full.isSupplierQtyMismatch == true) {
						type = '<span class="text-danger">' + full.supplierQty + '</span>'
						return GetCellData('Supplier Qty', type);
					}
					else {
						return GetCellData('Supplier Qty', full.supplierQty);
					}

				}
			},
			{
				className: "text-right width-auto",
				name: "UnitPrice",
				"data": "unitPrice",
				render: function (data, type, full, meta) {
					return GetCellData('Unit Price', full.unitPrice);
				}
			},
			{
				className: "tdblock",
				name: "Notes",
				"data": "notes",
				render: function (data, type, full, meta) {
					return GetCellData('Supplier Notes', full.notes);
				}
			},
		],
	});
}

function LoadSuppliersComponentDetails() {
	$('#dtComponentList').DataTable().destroy();
	gridSummaryComponentList = $('#dtComponentList').DataTable({
		"dom": '<<"row mb-0"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": false,
		"autoWidth": false,
		"paging": false,
		"pageLength": 25,
		"order": [[0, "asc"]],
		"language": {
			"emptyTable": "No supplier commponent available.",
		},
		"ajax": {
			"url": "/PurchaseOrder/GetSummaryComponent",
			"type": "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber
			}
		},
		"columns": [
			{
				className: "top-margin-0 tdblock data-text-align",
				data: "componentName",
				render: function (data, type, full, meta) {
					return GetCellData('Component', full.componentName);
				}
			},
			{
				className: "data-text-align",
				data: "maker",
				render: function (data, type, full, meta) {
					return GetCellData('Maker', full.maker);
				}
			},
			{
				className: "data-text-align",
				"data": "serial",
				render: function (data, type, full, meta) {
					return GetCellData('Serial', full.serial);
				}
			},
			{
				className: "data-text-align",
				"data": "type",
				render: function (data, type, full, meta) {
					return GetCellData('Type', full.type);
				}
			},
			{
				className: "data-text-align",
				"data": "designer",
				render: function (data, type, full, meta) {
					return GetCellData('Designer', full.designer);
				}
			},
			{
				className: "data-icon-align",
				width: "50px",
				"data": "criticalComponent",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var criticalStatus = "";
						if (full.criticalComponent == true) {
							criticalStatus = '<i class="fa fa-check text-success" aria-hidden="true"></i>';
						}
						else if (full.criticalComponent == false) {
							criticalStatus = '<i class="fa fa-times text-low" aria-hidden="true"> </i>';
						}
						return GetCellData('Critical', criticalStatus);
					}
					return data;
				}
			}
		]
	});
}

function LoadSupplierOrderDetails() {
	$.ajax({
		url: "/PurchaseOrder/GetSupplierOrderDetails",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber
		},
		"datatype": "JSON",
		success: function (data) {
			$('#orderDetailsType').text(orderType);
			$('#orderDetailsPriority').text(orderPriority);
			if (orderPriority == 'Normal') {
				$('#orderDetailsPriority').addClass('txt-green-common-text');
			}
			else if (orderPriority == 'Urgent') {
				$('#orderDetailsPriority').addClass('txt-red');
			}

			$('#orderDetailsLines').text(data.itemsCount);
			$('#orderDetailsExpectedDeliveryDate').text(data.expectedDate);
			$('#orderDetailsExpectedPort').text(data.portName);
			$('#orderDetailsExpectedCountry').text(data.portCountry);
		}
	});
}

function LoadAuthorisedSupplierDetails() {
	$.ajax({
		url: "/PurchaseOrder/GetAuthorisedSupplierDetails",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber
		},
		"datatype": "JSON",
		success: function (data) {
			if (data.supplierReference == undefined || data.supplierReference == "" || data.supplierReference == null) {
				$('#summaryAuthorisedSupplierDetails').text('Authorised Supplier');
			}
			else {
				$('#summaryAuthorisedSupplierDetails').text('Authorised Supplier (Sup Ref: ' + data.supplierReference + ')');
			}

			$('#authorisedSupDateAuthorised').text(data.dateAuthorised);
			$('#authorisedSupAuthorisedBy').text(data.authorisedBy);
			$('#authorisedSupPOConfirmedDate').text(data.confirmationDate);
			$('#authorisedSupPOChasedDate').text(data.purchaseOrderChasedDate);
			$('#authorisedSupInvoiceChasedDate').text(data.invoiceChasedDate);

			if (data.companyDetails != undefined && data.companyDetails != null) {
				$('#authSupCompanyName').text(data.companyDetails.companyName);

				$('#authSupAddress').text(data.companyDetails.address);
				var townDetails = data.companyDetails.town + ' ' + data.companyDetails.state + ' ' + data.companyDetails.postalCode
				$('#authSupTownDetails').text(townDetails);

				$('#authSupCountry').text(data.companyDetails.country);

				if (data.companyDetails.telephone != null && data.companyDetails.telephone != '' && data.companyDetails.telephoe != 'undefined') {
					$('#divAuthSupTelephone').removeClass('d-none');
					$('#authSupTelephone').text(data.companyDetails.telephone);
				}

				if (data.companyDetails.fax != null && data.companyDetails.fax != '' && data.companyDetails.fax != 'undefined') {
					$('#divAuthSupFax').removeClass('d-none');
					$('#authSupFax').text(data.companyDetails.fax);
				}

				if (data.companyDetails.email != null && data.companyDetails.email != '' && data.companyDetails.email != 'undefined') {
					$('#divAuthSupEmail').removeClass('d-none');
					$('#authSupEmail').text(data.companyDetails.email);
					$("#authSupDeliveryEmail").attr("href", 'mailto:' + data.companyDetails.email);
				}
			}
			$('.height-equal').matchHeight();
		}
	});

}

function LoadCostDetails() {
	var requestObject = {
		"AccountingCompanyId": accountingCompanyId,
		"OrderNumber": orderNumber,
		"OrderStatus": orderStatus,
		"OrderStage": orderStage
	}

	$.ajax({
		url: "/PurchaseOrder/PostGetSummaryPoCost",
		type: "POST",
		"data": requestObject,
		"datatype": "JSON",
		success: function (data) {
			var costHeaderBuilder = "";
			if (data.currency != null && data.currency != 'undefined' && data.currency != "" && data.costList != null) {
				costHeaderBuilder = 'Cost (' + data.currency + ')';
			}
			else {
				costHeaderBuilder = 'Cost'
			}

			$('#spanCostHeader').text(costHeaderBuilder);

			if (data.lastPOCurrencyChangeLog != 'undefined' && data.lastPOCurrencyChangeLog != null && data.lastPOCurrencyChangeLog != "") {
				$('#costWarningMessageLabel').removeClass('d-none');
				$('#spanCostWarningMessage').text(data.lastPOCurrencyChangeLog);
			}
			LoadCostList(data.costList, data.showProposed);

		}
	});
}

function LoadCostList(costList, showProposed) {
	$('#dtSummaryCostList').DataTable().destroy();

	var gridSummaryCostList = $('#dtSummaryCostList').DataTable({
		"dom": '<<"row mb-0"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"autoWidth": false,
		"pageLength": 25,
		"order": [],
		'columnDefs': [
			{ 'visible': false, 'targets': [3] }
		],
		"language": {
			"emptyTable": "No cost summary available.",
		},
		"searching": false,
		"paging": false,
		"info": false,
		data: costList,
		"columns": [
			{
				className: "tdblock td-row-header data-text-align",
				"data": "labelHeader",
				width: "170px",
				render: function (data, type, full, meta) {
					return full.labelHeader
				}
			},
			{
				className: "data-number-align",
				width: "90px",
				data: "amount",
				type: "html-num",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var amount = "";
						if (full.labelHeader == "Total PO") {
							amount = '<span class="font-weight-bold">' + full.amount + '</span>'
							return GetCellData('Amount', amount);
						}
						else {
							return GetCellData('Amount', full.amount);
						}
					}
					else {
						return data; // This is a unformatted data 
					}
				}
			},
			{
				className: "data-number-align",
				width: "90px",
				"data": "outstanding",
				type: "html-num",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var outStanding = "";
						if (full.outstanding == 0) {
							if (full.labelHeader == "Total PO") {
								outStanding = '<span class="font-weight-bold">' + full.outstanding + '</span>'
								return GetCellData('Outstanding', outStanding);
							}
							else {
								return GetCellData('Outstanding', full.outstanding);
							}
						}
						else {
							if (full.labelHeader == "Total PO") {
								outStanding = '<span class="text-amber font-weight-bold">' + full.outstanding + '</span>'
								return GetCellData('Outstanding', outStanding);
							}
							else {
								return GetCellData('Outstanding', full.outstanding);
							}
						}
					}
					else {
						return data; // This is a unformatted data 
					}

				}
			},
			{
				className: "data-number-align",
				"data": "proposed",
				width: "90px",
				type: "html-num",
				render: function (data, type, full, meta) {
					if (type === "display") {
						var proposed = "";
						if (full.labelHeader == "Total PO") {
							proposed = '<span class="txt-red font-weight-bold">' + full.proposed + '</span>'
							return GetCellData('Proposed', proposed);
						}
						else {
							proposed = '<span class="txt-red">' + full.proposed + '</span>'
							return GetCellData('Proposed', proposed);
						}
					}
					else {
						return data; // This is a unformatted data 
					}
				}
			},
			{
				className: "d-none d-xl-none",
				"defaultContent": '',
				orderable: false,
			}
		]
	});

	if (showProposed) {
		gridSummaryCostList.columns([3]).visible(true);
	}
}

function LoadInvoiceList() {
	$('#dtSummaryInvoiceList').DataTable().destroy();
	var gridInvoiceList = $('#dtSummaryInvoiceList').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-12 offset-md-0 col-lg-7 offset-lg-2 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		"order": [],
		"language": {
			"emptyTable": "No invoice summary available.",
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetSummaryPoInvoicing",
			"type": "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber
			},
			"datatype": "JSON",
		},
		"columns": [
			{
				className: "tdblock mobile-popover-attachments data-icon-align",
				width: "35px",
				orderable: false,
				render: function (data, type, full, meta) {
					//return '<a class="text-black" target="_blank"><i class="fa fa-paperclip documentDownload cursor-pointer" aria-hidden="true"></i></a>';
					return '<a class="" target="_blank"><img src="/images/Download-doc-active.png" class="documentDownload m-0 align-top cursor-pointer" width="18" title="Download"/></a>';
				}
			},
			{
				className: "tdblock td-row-header data-text-align",
				"data": "reference",
				render: function (data, type, full, meta) {
					//return '<i class="fa fa-paperclip d-inline-block d-sm-none mr-1 cursor-pointer" aria-hidden="true"></i>' + full.reference
					return full.reference
				}
			},
			{
				className: "data-text-align",
				width: "240px",
				"data": "status",
				orderable: false,
				render: function (data, type, full, meta) {
					var kpi = "";
					if (full.StatusCategory == "Disputed") {
						kpi = '<span class="txt-red">' + full.status + '</span>'
						return GetCellData('Status', kpi);
					}
					else {
						kpi = '<span class="txt-green">' + full.status + '</span>'
						return GetCellData('Status', kpi);
					}
				}
			},
			{
				className: "data-number-align",
				width: "45px",
				"data": "amount",
				orderable: false,
				render: function (data, type, full, meta) {
					return GetCellData('Amount', full.amount)
				}
			},
			{
				className: "data-text-align",
				width: "35px",
				"data": "currency",
				render: function (data, type, full, meta) {
					return GetCellData('Currency', full.currency)
				}
			},
			{
				className: "data-datetime-align",
				width: "50px",
				type: "date",
				"data": "invoiceDate",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Invoice Date', formattedDate)
					}
					return date;
				}
			},
			{
				className: "data-datetime-align",
				width: "35px",
				"data": "invoicePaidDate",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Paid Date', formattedDate)
					}
					return date;
				}
			},
			{
				className: "d-none d-xl-none",
				"defaultContent": '',
				orderable: false,
			}
		],
	});

	$('#dtSummaryInvoiceList tbody').on('click', 'img.documentDownload', function () {
		var data = gridInvoiceList.row($(this).parents('tr')).data();
		$.ajax({
			url: '/PurchaseOrder/DownloadInvoice',
			type: 'POST',
			dataType: "JSON",
			data: {
				"identifier": data.invoiceDocumentId
			},
			success: function (data) {
				if (data.bytes != null) {
					var array = base64ToArrayBuffer(data.bytes)
					saveByteArray(data.filename, array, data.fileType);
				} else {
					ToastrAlert("error", "File Not Found");
				}
			}
		})
	})
}

function LoadBudgetTabGrid() {
	LoadBudgetTabDetails();
}

function LoadBudgetTabDetails() {
	if (accountCode == null || accountCode == 'undefined' || accountCode == "") {
		$('#modalBudgetAlert').modal("show");
	}
	else {
		$.ajax({
			url: "/PurchaseOrder/PostGetBudgetTabHeaderDetails",
			type: "POST",
			"data": {
				"accountingCompanyId": accountingCompanyId,
				"orderNumber": orderNumber,
				"accountCode": accountCode
			},
			"datatype": "JSON",
			success: function (data) {
				$('#budgetFromDate').text(data.budgetStartDate);
				$('#budgetToDate').text(data.budgetStartEnd);
				$('#budgetYearAllocation').text(data.budgetAmountAllocated);
				$('#budgetAccural').text(data.totalAccruals);
				$('#budgetActual').text(data.budgetActualSpend);
				$('#budgeTotal').text(data.totalBudget);
				$('#budgetVariance').text(data.varianceAmount);
				$('#budgetCurrency').text(data.budgetCurrencyId);
				if (!isNaN(data.varianceAmountDecimal)) {
					if (data.varianceAmountDecimal < 0) {
						$('#budgetVariance').addClass('text-danger');
					}

				}

				LoadBudgetTabList(data.endDate, data.startDate, data.vesselId, data.budgetAmountAllocated, data.totalAccruals, data.budgetActualSpend, data.totalBudget, data.varianceAmount, data.budgetCurrencyId);
			}
		});
	}
}

function LoadBudgetTabList(endDate, startDate, vesselId, budgetAmountAllocated, totalAccruals, budgetActualSpend, totalBudget, varianceAmount, budgetCurrencyId) {
	var purchaseOrderRequestURL = $('#PurchaseOrderRequest').val();

	var requestObject = {
		"StartDate": startDate,
		"EndDate": endDate,
		"VesselId": vesselId,
		"AccountingCompanyId": accountingCompanyId,
		"AccountId": accountCode,
		"PurchaseOrderRequestUrl": purchaseOrderRequestURL
	}

	$('#dtBudgetTabList').DataTable().destroy();
	gridBudhetTabList = $('#dtBudgetTabList').DataTable({
		"dom": '<<"row mb-3"<"col-12 col-md-6 offset-md-0 col-lg-7 offset-lg-1 col-xl-7 offset-xl-1 dt-infomation"i><"col-12 col-md-6 col-lg-4 col-xl-4 position-absolute-mobile"<"row"f<"data-grid-right ExportToExcel">>>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
		"processing": true,
		"serverSide": false,
		"lengthChange": true,
		"searching": false,
		"info": true,
		"autoWidth": false,
		"paging": true,
		"pageLength": 25,
		"order": [[0, "asc"]],
		"language": {
			"emptyTable": "No budget details available.",
		},
		"ajax": {
			"url": "/PurchaseOrder/PostGetBudgetDetailsList",
			"type": "POST",
			"data": requestObject,
			"datatype": "JSON",
		},
		buttons: [
			'copy', 'csv',
			{
				extend: 'excel',
				exportOptions: {
					columns: ':visible',
					format: {
						body: function (data, row, column, node) {
							var child = node.querySelectorAll('.export-Data');

							if (child != undefined && child.length > 0) {
								var actualData = child[0];
								if (actualData != undefined) {
									return actualData.innerText;
								}
							}
						}
					}
				},
				customize: function (xlsx) {
					CustomizedExcelHeader(xlsx, 3);
				},
				title: "Budget Details Report",
				messageTop: function () {
					return 'Order No. : ' + $('#AccountingCompanyId').val() + ' - ' + $('#OrderNumber').val() + ' | Title :  ' + $('#Title').val() + '\nFrom : ' + moment(startDate).format("D MMM YYYY") + ' | To : ' + moment(endDate).format("D MMM YYYY") + ' | Year Allocation : ' + budgetAmountAllocated + '\nAccrual : ' + totalAccruals + ' | Actual : ' + budgetActualSpend + ' | Total : ' + totalBudget + ' | Variance : ' + varianceAmount + ' | Currency : ' + budgetCurrencyId
				}
			},
			'pdf', 'print'
		],
		"columns": [
			{
				className: "tdblock data-text-align",
				width: "30px",
				render: function (data, type, full, meta) {
					return '<a href = "/PurchaseOrder/Detail/?PurchaseOrderRequest=' + full.purchaseOrderRequestUrl + '&VesselId=' + full.purchaseOrderRequestVesselId + '"> ' + GetActualCellData(full.accountingCompanyId + ' - ' + full.orderNumber) + '</a > ';
				}
			},
			{
				className: "tdblock data-text-align",
				width: "490px",
				data: "orderTitle",
				render: function (data, type, full, meta) {
					return GetCellData('Order Name', full.orderTitle)
				}
			},
			{
				className: "data-datetime-align",
				width: "40px",
				name: "orderDate",
				"data": "orderDate",
				render: function (data, type, full, meta) {
					var date = "";
					var formattedDate = "";
					if (data != null) {
						date = new Date(data);
						formattedDate = moment(date).format("D MMM YYYY");
					}
					if (type === "display") {
						return GetCellData('Date', formattedDate);
					}
					return date;
				}
			},
			{
				className: "data-number-align",
				"data": "localCost",
				width: "40px",
				type: "html-num",
				render: function (data, type, full, meta) {
					if (type === "display") {
						return GetCellData('Local Cost', full.localCost);
					}
					return data;
				}
			},
			{
				className: "data-text-align",
				"data": "currencyId",
				width: "45px",
				render: function (data, type, full, meta) {
					return GetCellData('Currency', full.currencyId);
				}
			}
		],
	});

	//$("div.ExportToExcel").html('<button data-toggle="tooltip" id="btnExportBudgetList" title="Export to excel" data-placement="bottom" class="btn btn-dark btn-shadow btn-actions hover-blue"><i class="fa fa-fw" aria-hidden="true"></i></button>');

}

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

function LoadDeliveryTab() {
	LoadDeliveryTimes();
	LoadOrderTracker();
	LoadDeliveryAddressDetails();
}

function LoadDeliveryTimes() {
	$.ajax({
		url: "/PurchaseOrder/PostGetDeliveryTimes",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber
		},
		"datatype": "JSON",
		success: function (data) {
			$('#deliveryDays').text(data.deliveryDays);
			$('#daysRemaining').text(data.remainingDeliveryDays);
			$('#deliveryDate').text(data.deliveryDate);
		}
	});
}

function LoadOrderTracker() {
	$.ajax({
		url: "/PurchaseOrder/PostGetOrderTrackerDetails",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber,
			"orderStatus": orderStatus
		},
		"datatype": "JSON",
		success: function (data) {
			if (data != null) {
				if (data[0].orderStageName == 'Created') {
					$('#divCreatedHeader').removeClass('d-none');
					$('#spanCreatedDate').text(data[0].orderStageDate);
					if (data[0].orderStage == 2) {
						$('#divCreatedHeader').addClass('dot-teal');
					}
					else if (data[0].orderStage == 1) {
						$('#divCreatedHeader').addClass('dot-teal');
						$('#spanCreatedSubHeader').addClass('vertical-timeline-element-bg-teal');
						$('#spanCreatedDate').addClass('text-success');
					}
				}

				if (data[1].orderStageName == 'Requested') {
					$('#divRequestedHeader').removeClass('d-none');
					$('#spanRequestedDate').text(data[1].orderStageDate);

					if (data[1].orderStage == 2) {
						$('#divRequestedHeader').addClass('dot-teal');
					}
					if (data[1].orderStage == 1) {
						$('#divRequestedHeader').addClass('dot-teal');
						$('#spanRequestedSubHeader').addClass('vertical-timeline-element-bg-teal');
						$('#spanRequestedDate').addClass('text-success');
					}
				}

				if (data[2].orderStageName == 'Authorised') {
					$('#divAuthorisedHeader').removeClass('d-none');
					$('#spanAuthorisedDate').text(data[2].orderStageDate);

					if (data[2].orderStage == 2) {
						$('#divAuthorisedHeader').addClass('dot-teal');
					}
					if (data[2].orderStage == 1) {
						$('#divAuthorisedHeader').addClass('dot-teal');
						$('#spanAuthorisedSubHeader').addClass('vertical-timeline-element-bg-teal');
						$('#spanAuthorisedDate').addClass('text-success');
					}
				}

				if (data[3].orderStageName == 'Ordered') {
					$('#divOrderedHeader').removeClass('d-none');
					$('#spanOrderedDated').text(data[3].orderStageDate);

					if (data[3].orderStage == 2) {
						$('#divOrderedHeader').addClass('dot-teal');
					}
					if (data[3].orderStage == 1) {
						$('#divOrderedHeader').addClass('dot-teal');
						$('#spanOrderedSubHeader').addClass('vertical-timeline-element-bg-teal');
						$('#spanOrderedDated').addClass('text-success');
					}
				}

				if (data[4].orderStageName == 'ExpectedDelivery') {
					$('#divExpectedDeliveryHeader').removeClass('d-none');
					$('#spanExpectedDeliveryDate').text(data[4].orderStageDate);

					if (data[4].orderStage == 2) {
						$('#divExpectedDeliveryHeader').addClass('dot-teal');
					}
					if (data[4].orderStage == 1) {
						$('#divExpectedDeliveryHeader').addClass('dot-teal');
						$('#spanExpectedDeliverySubHeader').addClass('vertical-timeline-element-bg-teal');
						$('#spanExpectedDeliveryDate').addClass('text-success');
					}
				}

				if (data[5].orderStageName == 'PartiallyReceived') {
					$('#divPartiallyClosedHeader').removeClass('d-none');
					$('#spanPartiallyRecievedDate').text(data[5].orderStageDate);
					if (data[5].orderStage == 1) {
						$('#divPartiallyClosedHeader').addClass('dot-amber');
						$('#spanPartiallyRecievedDate').addClass('text-amber');
					}
				}
				else if (data[5].orderStageName == 'Closed') {
					$('#divClosedHeader').removeClass('d-none');
					$('#spanRecievedDate').text(data[5].orderStageDate);
					if (data[5].orderStage == 1) {
						$('#divClosedHeader').addClass('dot-teal');
						//$('#spanClosedSubHeader').addClass('vertical-timeline-element-bg-teal');
						$('#spanRecievedDate').addClass('text-success');
					}
				}
			}
			$('.height-equal').matchHeight();
		}
	});
}

function LoadDeliveryAddressDetails() {
	$.ajax({
		url: "/PurchaseOrder/GetDeliveryAddressDetails",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber
		},
		"datatype": "JSON",
		success: function (data) {
			if (data != null) {
				$('#spanPortName').text(data.port);

				if (data.isExpectedOrderStatus) {
					$('#divDeliveryHeader').text('Delivery To');
				}
				else {
					$('#divDeliveryHeader').text('Received At');
				}

				$('#spanCompanyType').text(data.companyType);
				$('#spanCompanyName').text(data.companyName);
				$('#spanAddress').text(data.address);
				var townDetails = data.town + ' ' + data.state + ' ' + data.postalCode;
				$('#spanTownDetails').text(townDetails);
				$('#spanCountry').text(data.country);

				if (data.telephone != null && data.telephone != '' && data.telephoe != 'undefined') {
					$('#divDeliveryTelephone').removeClass('d-none');
					$('#spanTelephone').text(data.telephone);
				}

				if (data.fax != null && data.fax != '' && data.fax != 'undefined') {
					$('#divDeliveryFax').removeClass('d-none');
					$('#spanFax').text(data.fax);
				}

				if (data.email != null && data.email != '' && data.email != 'undefined') {
					$('#divDeliveryEmail').removeClass('d-none');
					$('#spanEmail').text(data.email);
					$("#deliveryEmail").attr("href", 'mailto:' + data.email);
				}
			}
		}
	});
}

function ExportOrderDetails() {

	$.ajax({
		url: "/PurchaseOrder/ExportToExcelOrderDetailsReport",
		type: "POST",
		"data": {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber,
			//"vesselId": $("#PurchaseOrderVesselId").val()
		},
		success: function (data) {
			if (data.success) {
				ToastrAlert("success", data.message);
			}
			else {
				ErrorLog(xhr, status, error);
			}
		}
	});
}

function ClientAuthorizationPending() {
	$.ajax({
		url: "/PurchaseOrder/PostCheckOrderAuthorisationPending",
		type: "POST",
		dataType: "JSON",
		data: {
			"accountingCompanyId": accountingCompanyId,
			"orderNumber": orderNumber
		},
		success: function (data) {
			if (data != null && data.isFurtherOrderAuthorisationRequired) {
				if ($('#spanOrderAuthPending').hasClass('d-none')) {
					$('#spanOrderAuthPending').removeClass('d-none');
				}
			}
			else {
				if (!$('#spanOrderAuthPending').hasClass('d-none')) {
					$('#spanOrderAuthPending').addClass('d-none');
				}

			}
		}
	});
}

function ConfigurePopover() {
	$('#dtSuppliersTab').on('click', 'a.documentPopup', function () {
		InitializePopoverConstructor();
		$('body').addClass('popover-design');
		var data = dtSuppliersTab.row($(this).parents('tr')).data();
		var uniqueClsSel = 'universalIdentifier_' + data.supplierOrderId;
		if ($('.' + uniqueClsSel).attr('data-content') == undefined || $('.' + uniqueClsSel).attr('data-content') == "") {
			DocumentsPopover(uniqueClsSel, data.supplierOrderId);
		}
		else {
			$('.' + uniqueClsSel).popover('show');
		}
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

function GetFormattedOnlyDate(data) {
	if (data == null) return "";
	var date = new Date(data);
	return moment(date).format("D MMM YYYY");
}

function DocumentsPopover(uniqueClsSel, supplierOrderId) {
	var loadercontent = '<div class="loader-wrapper d-flex justify-content-center align-items-center">' +
		'<div class="loader  mx-auto">' +
		'<div class="ball-clip-rotate">' +
		'<div></div>' +
		'</div>' +
		'</div>' +
		'</div>';

	$('.' + uniqueClsSel).attr('title', 'Attachments <a href = "#" class= "close close-popover cursor-pointer"><img src="/images/popover-close.png" /></a>');
	$('.' + uniqueClsSel).attr('data-placement', 'bottom');
	$('.' + uniqueClsSel).attr('data-trigger', 'focus');
	$('.' + uniqueClsSel).attr('data-toggle', 'popover');
	$('.' + uniqueClsSel).attr('data-html', true);
	$('.' + uniqueClsSel).attr('data-content', '<div class="elementLoader p-2"></div>');

	$.ajax({
		url: "/PurchaseOrder/GetSupplierDocuments",
		type: "POST",
		dataType: "JSON",
		global: false,
		data: {
			"supplierOrderIds": supplierOrderId
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
				var html_content = "<div class='elementLoader scroller'><table class='table table-condensed table-borderless mb-0'><tbody>";
				for (var i = 0; i < attachCount; i++) {
					html_content += "<tr>";
					if (jsonArray[i].isWebAddressEditable) {
						html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='/images/AttachmentLinkIcon.png' class='mt-2' width='18' title='View link'/>";
					}
					else {
						html_content += "<td class='tdblock'><a href='' class='documentDownload cursor-pointer' id='document_" + i + "'><img src='/images/Download-doc-active.png' class='m-0' width='18' title='Download'/>";
					}
					html_content += "<span class='documentName' > " + jsonArray[i].title + " </span >";
					html_content += "<span class='documentId d-none'> " + jsonArray[i].ettId + " </span >";
					html_content += "<span class='webAddress d-none'> " + jsonArray[i].webAddress + " </span >";
					html_content += "<span class='documentCategory d-none'> " + jsonArray[i].documentCategory + " </span >";
					html_content += "<span class='isWebAddressEditable d-none'> " + jsonArray[i].isWebAddressEditable + " </span >";
					html_content += "<span class='documentfileName d-none' > " + jsonArray[i].cloudFileName + " </span ></a></td > ";
					html_content += "<td class='data-datetime-align'> " + GetFormattedOnlyDate(jsonArray[i].createdOn) + "</td>";
					html_content += "</tr>";
				}
				html_content += "</tbody></table></div>";
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('title', 'Attachments (' + attachCount + ') <a href="#" class="close close-popover"><img src="/images/popover-close.png" /></a>');
				$('.' + uniqueClsSel).attr('data-content', html_content);
				$('.' + uniqueClsSel).attr('data-placement', 'bottom');
				$('.' + uniqueClsSel).attr('data-trigger', 'focus');
				$('.' + uniqueClsSel).attr('data-toggle', 'popover');
				$('.' + uniqueClsSel).attr('data-html', true);
				$('.' + uniqueClsSel).popover('show');
				$('.' + uniqueClsSel).removeAttr('title');
			}
			else if (attachCount == 1) {
				$('.' + uniqueClsSel).popover('dispose');
				$('.' + uniqueClsSel).attr('data-content', '');
				var defectDocMap = new Map();
				defectDocMap.set(0, {
					documentName: jsonArray[0].title,
					documentId: jsonArray[0].ettId,
					documentFileName: jsonArray[0].cloudFileName,
					documentCategory: jsonArray[0].documentCategory
				});
				DownloadSelectedAttachment(defectDocMap, false);
			}
		},
		complete: function () {
			$(".elementLoader").unblock();
		},
	});
}

function DownloadSelectedAttachment(defectDocMap, globalFlag) {

	var fileName = '';
	var nextAttach = 0;
	var totalAttachment = defectDocMap.size;
	DownloadNextAttachment();

	function DownloadNextAttachment() {
		var documentId = (defectDocMap.get(nextAttach).documentId != null && defectDocMap.get(nextAttach).documentId != 'undefined') ? defectDocMap.get(nextAttach).documentId.trim() : '';
		var documentFileName = (defectDocMap.get(nextAttach).documentFileName != null && defectDocMap.get(nextAttach).documentFileName != 'undefined') ? defectDocMap.get(nextAttach).documentFileName.trim() : '';
		var documentCategory = (defectDocMap.get(nextAttach).documentCategory != null && defectDocMap.get(nextAttach).documentCategory != 'undefined') ? defectDocMap.get(nextAttach).documentCategory : '';
		var input = {
			"identifier": documentId,
			"fileName": documentFileName,
			"documentCategory": documentCategory
		};
		fileName = defectDocMap.get(nextAttach).documentName.trim();

		$.ajax({
			url: "/Defect/DownloadDocument",
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
		var documentCategory = $(this).children('span.documentCategory').text();

		if (isWebAddressEditable != null && isWebAddressEditable != 'undefined' && isWebAddressEditable.trim().toLowerCase() == 'true') {
			window.open(webAddress, '_blank').focus();
		}
		else {
			var fileName = ''
			var input = {
				"identifier": documentId != null && documentId != 'undefined' ? documentId.trim() : '',
				"fileName": docfileName != null && docfileName != 'undefined' ? docfileName.trim() : '',
				"documentCategory": documentCategory != null && documentCategory != 'undefined' ? documentCategory.trim() : ''
			};
			fileName = documentName != null && documentName != 'undefined' ? documentName.trim() : '';

			$.ajax({
				url: "/Defect/DownloadDocument",
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

function fetchAttachments(defectWorkOrderId) {
	var defectWorkOrderIds = [];
	var rows_selected = dtSuppliersTab.column(0).checkboxes.selected();

	if (rows_selected.length > 0) {
		$.each(rows_selected, function (index, rowId) {
			defectWorkOrderIds.push(rowId);
		});
	}
	else {
		defectWorkOrderIds = defectWorkOrderId;
	}

	if (defectWorkOrderIds.length > 0) {
		var defectDocMap = new Map();
		$.ajax({
			url: "/Defect/GetDefectDocuments",
			type: "POST",
			dataType: "JSON",
			data: {
				"DefectWorkOrderIds": defectWorkOrderIds
			},
			success: function (data) {
				var jsonArray = data.data;
				var counter = 0;
				for (var i = 0; i < jsonArray.length; i++) {
					if (!jsonArray[i].isWebAddressEditable) {
						defectDocMap.set(counter++, {
							documentName: jsonArray[i].title,
							documentId: jsonArray[i].ettId,
							documentFileName: jsonArray[i].cloudFileName,
							documentCategory: jsonArray[i].documentCategory
						});
					}
				}
				DownloadSelectedAttachment(defectDocMap, true);
			}
		});
	}
}

function SupplierRatingBreakdown(supplierCompanyId, supplierName) {

	var data = {
		SupplierCompanyId: supplierCompanyId,
		SupplierName: supplierName
	};

	$.ajax({
		url: "/PurchaseOrder/GetSupplierRatingBreakdown",
		type: "POST",
		"datatype": "html",
		data: data,
		success: function (data) {
			if (data != null) {
				$('#Rating_' + supplierCompanyId).html(data);
				$('#Rating_' + supplierCompanyId).show();
				$('.dropdown-overflow .table-responsive').css("overflow", "inherit");
			}
		}
	});
}

function hideAdvDowloadGrid() {
	if (($(window).width() < MobileScreenSize)) {
		$("#btnAdvDownloadMobile").show();
		$("#btnAdvDownloadDesktop").hide();
	} else {
		$("#btnAdvDownloadDesktop").show();
	}

	$(".grid-action-panel").hide();
	$('.app-main__outer .background-padding').removeClass('download-attachment-margin');
	//Get the column API object
	$('.select-checkbox-all').prop("checked", false);
	$("#AdvDownSelectAll").prop("checked", false);

	var ChkBoxColumn = dtSuppliersTab.column(0);
	var IconColumn = dtSuppliersTab.column(1);
	ChkBoxColumn.visible(false);
	IconColumn.visible(true);
	dtSuppliersTab.column(0).checkboxes.deselectAll();

	$('#AdvDocSelected').text(0);
	$('.isSelectAllDocument').hide();
}

function showAdvDowloadGrid() {
	$(".btnAdvDownload").hide();
	if ($("#mobileActiondropdown").hasClass('show')) {
		$("#mobileActiondropdown").removeClass('show');
	}
	$(".grid-action-panel").show();
	$('.app-main__outer .background-padding').addClass('download-attachment-margin');
	//Get the column API object
	var ChkBoxColumn = dtSuppliersTab.column(0);
	var IconColumn = dtSuppliersTab.column(1);
	$('.select-checkbox-all').prop({
		indeterminate: false,
		checked: false
	});
	$('#AdvDownSelectAll').prop({
		indeterminate: false,
		checked: false
	});
	ChkBoxColumn.visible(true);
	IconColumn.visible(false);
	$('.isSelectAllDocument').show();
	$("#btnDownloadSelection").addClass('disabled').attr('aria-disabled', 'true');
}


function GetRatingdesign(full) {
	var htmlrating = "";
	if (full.supplierRating == "1") {
		htmlrating = '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
	}
	else if (full.supplierRating == "2") {
		htmlrating = '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
	}
	else if (full.supplierRating == "3") {
		htmlrating = '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
	}
	else if (full.supplierRating == "4") {
		htmlrating = '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
	}
	else if (full.supplierRating == "5") {
		htmlrating = '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw checkedrate" aria-hidden="true" ></i>';
	}
	else {
		htmlrating = '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
		htmlrating += '<i class="fa fa-fw uncheckedrate" aria-hidden="true" ></i>';
	}

	return htmlrating;
}