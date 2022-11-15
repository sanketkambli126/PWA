import "select2/dist/js/select2.full.js";
require('bootstrap');

var insuranceClaimHeaderIsAppend = false;
var insuranceClaimIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        insuranceClaimIsMobile = true;
    } else {
        insuranceClaimIsMobile = false;
    }
});

export function LoadInsuranceclaimLookup(accountCompanyId) {
    insuranceClaimHeaderIsAppend = false;
    $("#cboInsuaranceClaimAux").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: insuranceClaimIsMobile ? '' : 'aux-lookup-bigdrop',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetInsuranceClaimLookup',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term,
                    accountCompanyId: accountCompanyId,
                    page: params.page
                };
            },
        },
        templateResult: insuranceClaimResult,
        templateSelection: insuranceClaimRepoSelection
    });

    $('#cboInsuaranceClaimAux').on('select2:open', function (e) {
        if (!insuranceClaimIsMobile) {
            if (!insuranceClaimHeaderIsAppend) {

                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Short Code</th>\
		                  <th width="50%">Description</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');

                insuranceClaimHeaderIsAppend = true;
            }
        }
    });
    
    $('#cboInsuaranceClaimAux').on('select2:opening', function (e) {       
        if (!insuranceClaimHeaderIsAppend) {
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


    function insuranceClaimResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (insuranceClaimIsMobile) {
            if (result != undefined) {
                $result = $('<div class="row select2-row">' +
                    '<div class="col-sm-12"><b>Short Code : </b>' + result.shortCode + '<b> | Description : </b>' + result.description + '</div>'
                    + '</div>');
            }
        }
        else {
            if (result != undefined) {
                $result = $('<table class="table table-bordered p-0 m-0">\
								 <tbody>\
									<tr>\
                                        <td width="50%">' + result.shortCode + '</td>\
										<td width="50%">' + result.description + '</td>\
									</tr>\
								</tbody>\
							</table>');
            }
        }
        return $result;
    }

    function insuranceClaimRepoSelection(repo) {
        if (repo.shortCodeDesc != '' && repo.shortCodeDesc != 'undefined' && repo.shortCodeDesc != null) {
            return repo.shortCodeDesc;
        }
        else {
            return repo.text;
        }
    }
}

export function SetSelectedInsuranceClaim(ClaimsId, ClaimsDescription, ClaimsShortCode) {
    var ShortCodeDesc = "";

    if (ClaimsShortCode != null && ClaimsShortCode != '' && ClaimsShortCode != 'undefined') {
        if (ClaimsDescription != null && ClaimsDescription != '' && ClaimsDescription != 'undefined') {
            ShortCodeDesc = ClaimsShortCode + " - " + ClaimsDescription;
        }
        else {
            ShortCodeDesc = ClaimsShortCode;
        }
    }
    else {
        if (ClaimsDescription != null && ClaimsDescription != '' && ClaimsDescription != 'undefined') {
            ShortCodeDesc = ClaimsId + " - " + ClaimsDescription;
        }
        else {
            ShortCodeDesc = ClaimsId;
        }
    }

    if (ClaimsId != null && ClaimsId != '' && ClaimsId != 'undefined' && ClaimsDescription != null && ClaimsDescription != '' && ClaimsDescription != 'undefined') {
        var selectedInsuranceClaim = new Option(ShortCodeDesc, ClaimsId, true, true);
        $('#cboInsuaranceClaimAux').append(selectedInsuranceClaim).trigger('change');
    }   
}