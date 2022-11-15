import "select2/dist/js/select2.full.js";
require('bootstrap');

var seasonalHeaderIsAppend = false;
var seasonalIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        seasonalIsMobile = true;
    } else {
        seasonalIsMobile = false;
    }
});

export function LoadSeasonalLookup() {
    seasonalHeaderIsAppend = false;
    $("#cboSeasonalAux").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: seasonalIsMobile ? '' : 'aux-lookup-bigdrop',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetGeneralAuxListByTypePaged',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term,
                    page: params.page,
                    auxType: 'Seasonal'
                };
            },
        },
        templateResult: seasoanlResult,
        templateSelection: seasonalRepoSelection
    });

    $('#cboSeasonalAux').on('select2:open', function (e) {
        if (!seasonalIsMobile) {
            if (!seasonalHeaderIsAppend) {               
                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Short Code</th>\
		                  <th width="50%">Description</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');
                
                seasonalHeaderIsAppend = true;
            }
        }
    });

    $('#cboSeasonalAux').on('select2:opening', function (e) {
        if (!seasonalHeaderIsAppend) {
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

    function seasoanlResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (seasonalIsMobile) {
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

    function seasonalRepoSelection(repo) {
        return repo.text;
    }
}

export function SetSelectedSeasonalLookup(SeasonalId, SeasonalDescription) {
    if (SeasonalId != null && SeasonalId != '' && SeasonalId != 'undefined' && SeasonalDescription != null && SeasonalDescription != '' && SeasonalDescription != 'undefined') {
        var selectedSeasonalLookup = new Option(SeasonalDescription, SeasonalId, true, true);
        $('#cboSeasonalAux').append(selectedSeasonalLookup).trigger('change');
    }
}