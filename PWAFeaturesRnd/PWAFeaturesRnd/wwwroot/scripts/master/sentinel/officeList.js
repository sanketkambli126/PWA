import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator } from "../../common/utilities.js";

var dataLoad = new Set();

$(document).ready(function () {
    $(".spanfleetSelectionTitle").html("<em>Search Vessel / Fleet</em>");

    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();
})

$('.sentinel-accordation .btn-link').on("click", function () {
    var fleetRequest = $(this).data().fleetrequest;
    var officeId = $(this).data().officeid;
    if (!dataLoad.has(officeId)) {
        BindActiveOverrides(fleetRequest, officeId);
        BindBiggestMovers(fleetRequest, officeId);
        dataLoad.add(officeId);
    }
})

function BindActiveOverrides(fleetRequest, officeId) {
    $.ajax({
        url: "/Sentinel/GetActiveOverrides",
        type: "GET",
        "data": {
            "fleetRequest": fleetRequest
        },
        success: function (data) {
            if (data != null) {
                $('#overridediv-' + officeId).empty()
                $('#overridediv-' + officeId).append(data);
            }
        }
    });
}

function BindBiggestMovers(fleetRequest, officeId) {
    $.ajax({
        url: "/Sentinel/GetBiggestMovers",
        type: "GET",
        "data": {
            "fleetRequest": fleetRequest
        },
        success: function (data) {
            if (data != null) {
                $('#inc-dec-info-' + officeId).empty();
                $('#inc-dec-info-' + officeId).append(data);
            }
        }
    });
}