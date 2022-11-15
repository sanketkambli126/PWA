import "select2/dist/js/select2.full.js";
require('bootstrap');

var crewRankHeaderIsAppend = false;
var crewRankIsMobile = false;

$(document).ready(function () {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        crewRankIsMobile = true;
    } else {
        crewRankIsMobile = false;
    }
});

export function LoadCrewRankLookup() {
    crewRankHeaderIsAppend = false;
    $("#cboCrewRankAuxLookup").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search",
        dropdownCssClass: crewRankIsMobile ? '' : 'aux-lookup-bigdrop',
        minimumInputLength: 1,
        ajax: {
            url: '/PurchaseOrder/GetCrewRankLookup',
            delay: 2000,
            dataType: 'json',
            data: function (params) {
                return {
                    term: params.term,
                    page: params.page
                };
            },
        },
        templateResult: crewRankResult,
        templateSelection: crewRankRepoSelection
    });

    $('#cboCrewRankAuxLookup').on('select2:open', function (e) {
        if (!crewRankIsMobile) {
            if (!crewRankHeaderIsAppend) {                
                $('.select2-search').append('<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <thead>\
                        <tr>\
		                  <th width="50%">Short Code</th>\
		                  <th width="50%">Description</th>\
		                </tr>\
		              </thead>\
		             </table>');
                $('.select2-results').addClass('stock');
                
                crewRankHeaderIsAppend = true;
            }
        }
    });

    $('#cboCrewRankAuxLookup').on('select2:opening', function (e) {
        if (!crewRankHeaderIsAppend) {
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

    function crewRankResult(result) {
        if (result.loading)
            return "Searching...";

        var $result;

        if (crewRankIsMobile) {
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

    function crewRankRepoSelection(repo) {
        return repo.text;
    }
}

export function SetSelectedCrewRankLookup(CrewRankId, CrewRankDescription) {
    if (CrewRankId != null && CrewRankId != '' && CrewRankId != 'undefined' && CrewRankDescription != null && CrewRankDescription != '' && CrewRankDescription != 'undefined') {
        var selectedCrewRankLookup = new Option(CrewRankDescription, CrewRankId, true, true);
        $('#cboCrewRankAuxLookup').append(selectedCrewRankLookup).trigger('change');
    }
}