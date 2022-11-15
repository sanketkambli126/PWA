import "select2/dist/js/select2.full.js";
require('bootstrap');

var general1AuxHeaderIsAppend = false;
var general1AuxIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        general1AuxIsMobile = true;
    } else {
        general1AuxIsMobile = false;
    }
});

export function LoadGeneral1AuxLookup() {
    general1AuxHeaderIsAppend = false;
    $("#cboGeneral1AuxLookup").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: general1AuxIsMobile ? '' : 'aux-lookup-bigdrop',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetGeneralAuxListByTypePaged',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term,
                    page: params.page,
                    auxType: 'General 1'
                };
            },
        },
        templateResult: generalAuxResult,
        templateSelection: generalAuxRepoSelection
    });

    $('#cboGeneral1AuxLookup').on('select2:open', function (e) {
        if (!general1AuxIsMobile) {
            if (!general1AuxHeaderIsAppend) {

                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Short Code</th>\
		                  <th width="50%">Description</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');

                general1AuxHeaderIsAppend = true;
            }
        }
    });

    $('#cboGeneral1AuxLookup').on('select2:opening', function (e) {
        if (!general1AuxHeaderIsAppend) {
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

        if (general1AuxIsMobile) {
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

export function SetSelectedGeneral1AuxLookup(General1Id, General1Description) {
    if (General1Id != null && General1Id != '' && General1Id != 'undefined' && General1Description != null && General1Description != '' && General1Description != 'undefined') {
        var selectedGeneralAux = new Option(General1Description, General1Id, true, true);
        $('#cboGeneral1AuxLookup').append(selectedGeneralAux).trigger('change');
    }
}