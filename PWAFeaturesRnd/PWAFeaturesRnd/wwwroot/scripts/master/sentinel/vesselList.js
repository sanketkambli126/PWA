import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator } from "../../common/utilities.js";

$(document).ready(function () {
    $(".spanfleetSelectionTitle").html("<em>Search Vessel / Fleet</em>");

    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();
})
