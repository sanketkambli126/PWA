import "select2/dist/js/select2.full.js";
require('bootstrap');

var vesselHeaderIsAppend = false;
var vesselAuxIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        vesselAuxIsMobile = true;
    } else {
        vesselAuxIsMobile = false;
    }
});

export function LoadVesselAuxLookup() {
    vesselHeaderIsAppend = false;
    $("#cboVesselAuxLookup").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: vesselAuxIsMobile ? '' : 'lookup-bigdrop-500',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetVesselAuxLookup',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term,
                    page: params.page
                };
            },
        },
        templateResult: vesselAuxResult,
        templateSelection: vesselAuxRepoSelection
    });

    $('#cboVesselAuxLookup').on('select2:open', function (e) {
        if (!vesselAuxIsMobile) {
            if (!vesselHeaderIsAppend) {
                
                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="25%">Short Code</th>\
		                  <th width="48%">Vessel Name</th>\
                          <th width="25%">Type</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');
                
                vesselHeaderIsAppend = true;
            }
        }
    });

    $('#cboVesselAuxLookup').on('select2:opening', function (e) {
        if (!vesselHeaderIsAppend) {
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

    function vesselAuxResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (vesselAuxIsMobile) {
            if (result != undefined) {
                $result = $('<div class="row select2-row">' +
                    '<div class="col-sm-12"><b>Short Code : </b>' + result.identifier + '<b> | Vessel Name : </b>' + result.description + '</div>' +
                    '<div class="col-sm-12"><b>Type : </b>' + result.type + '</div>'
                    + '</div>');
            }
        }
        else {
            if (result != undefined) {
                $result = $('<table class="table table-bordered p-0 m-0">\
								 <tbody>\
									<tr>\
                                        <td width="25%">' + result.identifier + '</td>\
										<td width="50%">' + result.description + '</td>\
                                        <td width="25%">' + result.type + '</td>\
									</tr>\
								</tbody>\
							</table>');
            }
        }
        return $result;
    }

    function vesselAuxRepoSelection(repo) {
        return repo.text;
    }
}

export function SetSelectedVesselAuxLookup(VesselManagementId, VesselName) {
    if (VesselManagementId != null && VesselManagementId != '' && VesselManagementId != 'undefined' && VesselName != null && VesselName != '' && VesselName != 'undefined') {
        var selectedVesselAuxLookup = new Option(VesselName, VesselManagementId, true, true);
        $('#cboVesselAuxLookup').append(selectedVesselAuxLookup).trigger('change');
    }
}