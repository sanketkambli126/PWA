import "select2/dist/js/select2.full.js";
require('bootstrap');

var general3AuxHeaderIsAppend = false;
var general3AuxIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        general3AuxIsMobile = true;
    } else {
        general3AuxIsMobile = false;
    }
});

export function LoadGeneral3AuxLookup() {
    general3AuxHeaderIsAppend = false;
    $("#cboGeneral3AuxLookup").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: general3AuxIsMobile ? '' : 'aux-lookup-bigdrop',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetGeneralAuxListByTypePaged',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term,
                    page: params.page,
                    auxType: 'General 3'
                };
            },
        },
        templateResult: generalAuxResult,
        templateSelection: generalAuxRepoSelection
    });

    $('#cboGeneral3AuxLookup').on('select2:open', function (e) {
        if (!general3AuxIsMobile) {
            if (!general3AuxHeaderIsAppend) {

                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Short Code</th>\
		                  <th width="50%">Description</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');

                general3AuxHeaderIsAppend = true;
            }
        }
    });

    $('#cboGeneral3AuxLookup').on('select2:opening', function (e) {
        if (!general3AuxHeaderIsAppend) {
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

    function generalAuxResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (general3AuxIsMobile) {
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

    function generalAuxRepoSelection(repo) {
        return repo.text;
    }
}

export function SetSelectedGeneral3AuxLookup(General3Id, General3Description) {
    if (General3Id != null && General3Id != '' && General3Id != 'undefined' && General3Description != null && General3Description != '' && General3Description != 'undefined') {
        var selectedGeneralAux = new Option(General3Description, General3Id, true, true);
        $('#cboGeneral3AuxLookup').append(selectedGeneralAux).trigger('change');
    }
}