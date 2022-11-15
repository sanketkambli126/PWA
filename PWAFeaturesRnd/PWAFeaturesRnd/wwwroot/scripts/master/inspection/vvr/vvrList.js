import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator } from "../../../common/utilities.js" 
import { LoadAddInspectionVisitModal } from "../vvr/addInspectionModal.js";

$(document).on('click', '.openvvr' , function () {
    LoadAddInspectionVisitModal();
})

$(document).ready(function () {
    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();
    
 });
