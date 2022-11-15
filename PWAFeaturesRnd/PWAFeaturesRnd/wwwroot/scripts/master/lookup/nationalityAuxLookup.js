import "select2/dist/js/select2.full.js";
require('bootstrap');

var nationalityHeaderIsAppend = false;
var nationalityIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        nationalityIsMobile = true;
    } else {
        nationalityIsMobile = false;
    }
});

export function LoadNationalityLookup() {
    nationalityHeaderIsAppend = false;
    $("#cboNationalityAuxLookup").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: nationalityIsMobile ? '' : 'aux-lookup-bigdrop',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetNationalityLookup',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term
                };
            },
        },
        templateResult: nationalityResult,
        templateSelection: nationalityRepoSelection
    });

    $('#cboNationalityAuxLookup').on('select2:open', function (e) {
        if (!nationalityIsMobile) {
            if (!nationalityHeaderIsAppend) {                             
                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Id</th>\
		                  <th width="50%">Description</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');

                nationalityHeaderIsAppend = true;
            }
        }
    });

    $('#cboNationalityAuxLookup').on('select2:opening', function (e) {
        if (!nationalityHeaderIsAppend) {
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
    
    function nationalityResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (nationalityIsMobile) {
            if (result != undefined) {
                $result = $('<div class="row select2-row">' +
                    '<div class="col-sm-12"><b>Id : </b>' + result.id + '<b> | Description : </b>' + result.description + '</div>'
                    + '</div>');
            }
        }
        else {
            if (result != undefined) {
                $result = $('<table class="table table-bordered p-0 m-0">\
								 <tbody>\
									<tr>\
                                        <td width="50%">' + result.id + '</td>\
										<td width="50%">' + result.description + '</td>\
									</tr>\
								</tbody>\
							</table>');
            }
        }
        return $result;
    }

    function nationalityRepoSelection(repo) {
        return repo.text;
    }
}

export function SetSelectedNationalityLookup(NationalityId, NationalityDescription) {
    if (NationalityId != null && NationalityId != '' && NationalityId != 'undefined' && NationalityDescription != null && NationalityDescription != '' && NationalityDescription != 'undefined') {
        var selectedNationalityLookup = new Option(NationalityDescription, NationalityId, true, true);
        $('#cboNationalityAuxLookup').append(selectedNationalityLookup).trigger('change');
    }
}