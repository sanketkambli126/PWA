require('bootstrap');
import "select2/dist/js/select2.full.js";


import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, ToastrAlert, IsNullOrEmptyOrUndefined, AddClassIfAbsent, RemoveClassIfPresent, IsNullOrEmptyOrUndefinedLooseTyped } from "../../../common/utilities.js"

var selectedVesselId = "";
require('bootstrap');

$(document).ready(function () {
    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();

    $("#addfindingsmodal").click(function () {
        $('#modalfindingslist').modal('show');
        $('#modalInspectionVesselVisit').modal('hide');
        setTimeout(function () {
            $('body').addClass('modal-open');
        }, 1000);
    });

    $('#modalInspectionVesselVisit').on('shown.bs.modal', function () {
        setTimeout(function () {
            $('body').addClass('modal-open');
        }, 1000);
    });
   
    $('#modalfindingslist').on('hidden.bs.modal', function () {
        $('.modal-backdrop').remove();
    });
    
    $('#insecpectionVesselSearch').on('select2:open', function (e) {
        $('.select2-results').addClass('vessel-drop');
    });
    $('#cbWhere').on('select2:open', function (e) {
        $('.select2-results').addClass('vessel-drop');
    });
    $('#cbCompany').on('select2:open', function (e) {
        $('.select2-results').addClass('vessel-drop');
    });
    $('#cbFromPort').on('select2:open', function (e) {
        $('.select2-results').addClass('vessel-drop');
    });
    $('#cbToPort').on('select2:open', function (e) {
        $('.select2-results').addClass('vessel-drop');
    });


    $("#operationList").select2({
        theme: "bootstrap4",
        placeholder: "Select Operating",
    });
    $('#operationList').on('select2:open', function (e) {
        $('.select2-results').addClass('stock');
        $('.select2-results').addClass('vessel-drop');
        $('.select2-results').addClass('hidesearch');
        $('.select2-search__field').hide();
    });

    $("#activityTypeList").select2({
        theme: "bootstrap4",
        placeholder: "Select Activity",
    });
    $('#activityTypeList').on('select2:open', function (e) {
        $('.select2-results').addClass('stock');
        $('.select2-results').addClass('vessel-drop');
        $('.select2-results').addClass('hidesearch');
        $('.select2-search__field').hide();
    });

    $("#department").select2({
        theme: "bootstrap4",
        placeholder: "Select Activity",
    });
    $('#department').on('select2:open', function (e) {
        $('.select2-results').addClass('stock');
        $('.select2-results').addClass('vessel-drop');
        $('.select2-results').addClass('hidesearch');
        $('.select2-search__field').hide();
    });

});

export function LoadAddInspectionVisitModal() {

    BindInspectionVisitDetails();

    BindVesselLookUp();

    BindWhereLookUp();

    BindFromPortLookUp();

    BindToPortLookUp();

    BindCompanyLookUp();

    OnLocationSelection();

    OnInspectionEntityTypeSelection();
    
    OnOperationSelection();

    $('#btnSaveVisit').click(function () {
        SaveVisit();
    });

}

function BindInspectionVisitDetails() {

    $.ajax({
        url: "/Inspection/BindInspectionVisitDetails",
        dataType: "JSON",
        success: function (data) {
            if (data != null) {
                if (!IsNullOrEmptyOrUndefined(data.departmentList)) {
                    $('#department').empty();
                    $.each(data.departmentList, function () {
                        $('#department').append($("<option></option>").val(this['identifier']).html(this['description']));
                    });
                }
                if (!IsNullOrEmptyOrUndefined(data.operatingList)) {
                    $('#operationList').empty();
                    $.each(data.operatingList, function () {
                        $('#operationList').append($("<option></option>").val(this['identifier']).html(this['description']));
                    });
                }
                if (!IsNullOrEmptyOrUndefined(data.activityTypesList)) {
                    $('#activityTypeList').empty();
                    $.each(data.activityTypesList, function () {
                        $('#activityTypeList').append($("<option></option>").val(this['id']).html(this['description']));
                    });
                }
                if (!IsNullOrEmptyOrUndefined(data.userForeName)) {
                    $('#txtInspectorForeName').val(data.userForeName);
                }
                if (!IsNullOrEmptyOrUndefined(data.userSurName)) {
                    $('#txtInspectorSurName').val(data.userSurName);

                }
                
            }
        },
        complete: function () {

        }
    });
}

function formatInspectionVisitVesselResult(result) {
    if (result.loading)
        return "Searching...";

    var $result;

    if (result != undefined) {
        $result = $('<div class="select2-row">' +
            '<div>' + result.vesselName + '</div>'
            + '</div>');
        return $result;
    }
}


function formatInspectionVisitVesselRepoSelection(repo) {
    selectedVesselId = repo.vesselId;
    return repo.text;
}

function formatInspectionVisitWhereResult(result) {
    if (result.loading)
        return "Searching...";

    var $result;
    if (result != undefined) {
        $result = $('<div class="select2-row">' +
            '<div>' + result.portName + '</div>'
            + '</div>');
    }
    return $result;
}

function formatInspectionVisitWhereRepoSelection(repo) {
    return repo.text;
}

function formatInspectionVisitCompanyResult(result) {
    if (result.loading)
        return "Searching...";

    var $result;
    if (result != undefined) {
        $result = $('<div class="select2-row">' +
            '<div>' + result.companyName + '</div>'
            + '</div>');
    }
    return $result;
}

function formatInspectionVisitCompanyRepoSelection(repo) {
    return repo.text;
}

function SaveVisit() {
    
    var SelectedInspectionVisitVesselId = selectedVesselId;
    var dtStartDate = $('#dtStartDate').val();
    var dtEndDate = $('#dtEndDate').val();
    var selectedLocation = $('input[type=radio][name=Location]:checked').val();
    var cbWhere = $('#cbWhere').val();
    var operationList = $('#operationList').val();
    var activityTypeList = $('#activityTypeList').val();
    var InspectorEntityType = $('input[name="InspectorEntityType"]:checked').val();
    var cbCompany = $('#cbCompany').val();
    var cbToPort = $('#cbToPort').val();
    var cbFromPort = $('#cbFromPort').val();
    var txtInspectorTitle = $('#txtInspectorTitle').val();
    var txtInspectorForeName = $('#txtInspectorForeName').val();
    var txtInspectorSurName = $('#txtInspectorSurName').val();
    var department = $('#department').val();
    var isValid = ValidateControls();
    if (isValid) {
        if (selectedLocation == "Port" && operationList == 1 && InspectorEntityType == "Office") {
            var request =
            {
                "VesselId": SelectedInspectionVisitVesselId,
                "FromDate": dtStartDate,
                "EndDate": dtEndDate,
                "Where": cbWhere,
                "ActivityType": activityTypeList,
                "CompanyId": cbCompany,
                "InspectorTitle": txtInspectorTitle,
                "InspectorForename": txtInspectorForeName,
                "InspectorSurname": txtInspectorSurName,
                "DepartmentId": department
            };
            SaveInspectionVisit(request);

        }
        else if (selectedLocation == "Sailing" && operationList == 1 && InspectorEntityType == "Office") {
            var request =
            {
                "VesselId": SelectedInspectionVisitVesselId,
                "FromDate": dtStartDate,
                "EndDate": dtEndDate,
                "Where": cbFromPort,
                "ToPortId": cbToPort,
                "CompanyId": cbCompany,
                "InspectorTitle": txtInspectorTitle,
                "InspectorForename": txtInspectorForeName,
                "InspectorSurname": txtInspectorSurName,
                "DepartmentId": department
            };
            SaveInspectionVisit(request);
        }
    }
}

function ValidateControls() {
    var selectedVesselId = $('#insecpectionVesselSearch').val();
    var dtStartDate = $('#dtStartDate').val();
    var dtEndDate = $('#dtEndDate').val();
    var selectedLocation = $('input[type=radio][name=Location]:checked').val();
    var cbWhere = $('#cbWhere').val();
    var cbToPort = $('#cbToPort').val();
    var cbFromPort = $('#cbFromPort').val();
    var operationList = $('#operationList').val();
    var activityTypeList = $('#activityTypeList').val();
    var InspectorEntityType = $('input[type=radio][name=InspectorEntityType]:checked').val();
    var cbCompany = $('#cbCompany').val();
    var txtInspectorTitle = $('#txtInspectorTitle').val();
    var txtInspectorForeName = $('#txtInspectorForeName').val();
    var txtInspectorSurName = $('#txtInspectorSurName').val();
    var department = $('#department').val();
    if (IsNullOrEmptyOrUndefinedLooseTyped(selectedLocation)) {
        ToastrAlert("validate", "Please select Location");
        return false;
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(operationList)) {
        ToastrAlert("validate", "Please select Operation.");
        $("#operationList").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#operationList").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(InspectorEntityType)) {
        ToastrAlert("validate", "Please select Entity Type.");
        return false;
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(selectedVesselId)) {
        ToastrAlert("validate", "Please select Vessel.");
        $("#insecpectionVesselSearch").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#insecpectionVesselSearch").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(dtStartDate)) {
        ToastrAlert("validate", "Please select Start Date.");
        $("#dtStartDate").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#dtStartDate").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(dtEndDate)) {
        ToastrAlert("validate", "Please select End Date.");
        $("#dtEndDate").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#dtEndDate").css({ "border-color": '' });
    }
    var currentDate = new Date();
    if (new Date(dtStartDate) > currentDate) {
        ToastrAlert("validate", "StartDate cannot be in the future");
        return false;
    }
    if (new Date(dtEndDate) > currentDate) {
        ToastrAlert("validate", "EndDate cannot be in the future");
        return false;
    }
    if (dtEndDate < dtStartDate) {
        ToastrAlert("validate", "EndDate cannot be before StartDate");
        return false;

    }
    if (dtStartDate > dtEndDate) {
        ToastrAlert("validate", "StartDate cannot be after EndDate");
        return false;

    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(cbCompany)) {
        ToastrAlert("validate", "Please select company");
        $("#cbCompany").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#cbCompany").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(txtInspectorTitle)) {
        ToastrAlert("validate", "Please enter Title");
        $("#txtInspectorTitle").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#txtInspectorTitle").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(txtInspectorForeName)) {
        ToastrAlert("validate", "Please enter ForeName");
        $("#txtInspectorForeName").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#txtInspectorForeName").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(txtInspectorSurName)) {
        ToastrAlert("validate", "Please enter SurName");
        $("#txtInspectorSurName").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#txtInspectorSurName").css({ "border-color": '' });
    }
    if (IsNullOrEmptyOrUndefinedLooseTyped(department)) {
        ToastrAlert("validate", "Please enter depatment");
        $("#department").css({ "border-color": 'red' });
        return false;
    }
    else {
        $("#department").css({ "border-color": '' });
    }

    if (selectedLocation == "Port" && operationList == 1 && InspectorEntityType == "Office") {

        if (IsNullOrEmptyOrUndefinedLooseTyped(cbWhere)) {
            ToastrAlert("validate", "Please select where");
            $("#cbWhere").css({ "border-color": 'red' });
            return false;
        }
        else {
            $("#cbWhere").css({ "border-color": '' });
        }
        if (IsNullOrEmptyOrUndefinedLooseTyped(activityTypeList)) {
            ToastrAlert("validate", "Please select activity");
            $("#activityTypeList").css({ "border-color": 'red' });
            return false;
        }
        else {
            $("#activityTypeList").css({ "border-color": '' });
        }
    }
    if (selectedLocation == "Sailing" && operationList == 1 && InspectorEntityType == "Office") {
        if (IsNullOrEmptyOrUndefinedLooseTyped(cbFromPort)) {
            ToastrAlert("validate", "Please select from port");
            $("#cbFromPort").css({ "border-color": 'red' });
            return false;
        }
        else {
            $("#cbFromPort").css({ "border-color": '' });
        }
        if (IsNullOrEmptyOrUndefinedLooseTyped(cbToPort)) {
            ToastrAlert("validate", "Please select to port");
            $("#cbToPort").css({ "border-color": 'red' });
            return false;
        }
        else {
            $("#cbToPort").css({ "border-color": '' });
        }
    }
    return true;
}

function ClearDepartmentOptions() {
    $('#department')
        .find('option')
        .remove()
        .end();

}

function BindInspectionEntityType() {
    $.ajax({
        url: "/Inspection/GetDepartmentDetailsByInspectorType",
        type: "POST",
        dataType: "JSON",
        data: {
            "EncryptedVesselId": $('#VesselId').val(),
            "inspectorEntity": this.value
        },
        success: function (data) {
            $.each(data.data, function () {
                $('#department').append($("<option></option>").val(this['identifier']).html(this['description']));
            });
        }
    });
}

function SaveInspectionVisit(request) {
    $.ajax({
        url: "/Inspection/SaveInspectionVisitDetails",
        dataType: "JSON",
        data: request,
        success: function (data) {
            var modal = $("#modalInspectionVesselVisit");
            HideModal(modal);
            $("#successModalMsg").text("Inspection Saved successfully");
            HandleSuccessModal();
        },
        error: function () {
            ToastrAlert("error", "Failed to Save Visit");
        }
    });
}

function BindVesselLookUp() {
    $("#insecpectionVesselSearch").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search vessel",
        minimumInputLength: 3,
        ajax: {
            url: '/Dashboard/GetVesselLookup',
            dataType: 'json'
        },
        templateResult: formatInspectionVisitVesselResult,
        templateSelection: formatInspectionVisitVesselRepoSelection
    });

    $('#insecpectionVesselSearch').on('select2:open', function (e) {
        var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		            <tbody>\
		            </tbody>\
		            </table>';
        $('.select2-search').append(html);
        $('.select2-results').addClass('stock');
    });

}

function BindWhereLookUp() {
    $("#cbWhere").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search port",
        minimumInputLength: 3,
        ajax: {
            url: '/Inspection/GetPortsLookup',
            dataType: 'json'
        },
        templateResult: formatInspectionVisitWhereResult,
        templateSelection: formatInspectionVisitWhereRepoSelection
    });

    $('#cbWhere').on('select2:open', function (e) {
        var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <tbody>\
		              </tbody>\
		              </table>';
        $('.select2-search').append(html);
        $('.select2-results').addClass('stock');
    });
}

function BindFromPortLookUp() {
    $("#cbFromPort").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search port",
        minimumInputLength: 3,
        ajax: {
            url: '/Inspection/GetPortsLookup',
            dataType: 'json'
        },
        templateResult: formatInspectionVisitWhereResult,
        templateSelection: formatInspectionVisitWhereRepoSelection
    });

    $('#cbFromPort').on('select2:open', function (e) {
        var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <tbody>\
		              </tbody>\
		              </table>';
        $('.select2-search').append(html);
        $('.select2-results').addClass('stock');
    });
}

function BindToPortLookUp() {
    $("#cbToPort").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search port",
        minimumInputLength: 3,
        ajax: {
            url: '/Inspection/GetPortsLookup',
            dataType: 'json'
        },
        templateResult: formatInspectionVisitWhereResult,
        templateSelection: formatInspectionVisitWhereRepoSelection
    });

    $('#cbToPort').on('select2:open', function (e) {
        var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <tbody>\
		              </tbody>\
		              </table>';
        $('.select2-search').append(html);
        $('.select2-results').addClass('stock');
    });
}

function BindCompanyLookUp() {
    $("#cbCompany").select2({
        theme: "bootstrap4",
        placeholder: "Type here to search company",
        minimumInputLength: 3,
        ajax: {
            url: '/Inspection/GetCompanySearchLookup',
            dataType: 'json'
        },
        templateResult: formatInspectionVisitCompanyResult,
        templateSelection: formatInspectionVisitCompanyRepoSelection
    });

    $('#cbCompany').on('select2:open', function (e) {
        var html = '<table class="table table-bordered" style="margin-top: 5px;margin-bottom: 0px; width: 98%">\
		              <tbody>\
		              </tbody>\
		              </table>';
        $('.select2-search').append(html);
        $('.select2-results').addClass('stock');
    });
}

function OnLocationSelection() {
    $('input[type=radio][name=Location]').change(function () {
        if (this.value == 'Sailing') {
            AddClassIfAbsent('#whereSection', 'd-none');
            AddClassIfAbsent('#whereHeadingSection', 'd-none');
            RemoveClassIfPresent('#fromPortSection', 'd-none');
            RemoveClassIfPresent('#fromPortHeadingSection', 'd-none');
            AddClassIfAbsent('#operatingSection', 'd-none');
            AddClassIfAbsent('#operatingHeadingSection', 'd-none');
            RemoveClassIfPresent('#toPortSection', 'd-none');
            RemoveClassIfPresent('#toPortHeadingSection', 'd-none');
            AddClassIfAbsent('#operatingactivitySection', 'd-none');
        }
        else {
            AddClassIfAbsent('#toPortSection', 'd-none');
            AddClassIfAbsent('#toPortHeadingSection', 'd-none');
            AddClassIfAbsent('#fromPortSection', 'd-none');
            AddClassIfAbsent('#fromPortHeadingSection', 'd-none');
            RemoveClassIfPresent('#whereSection', 'd-none');
            RemoveClassIfPresent('#whereHeadingSection', 'd-none');
            RemoveClassIfPresent('#operatingSection', 'd-none');
            RemoveClassIfPresent('#operatingHeadingSection', 'd-none');
            RemoveClassIfPresent('#operatingactivitySection', 'd-none');
        }
    });
}

function OnInspectionEntityTypeSelection() {
    $('input[type=radio][name=InspectorEntityType]').change(function () {
        ClearDepartmentOptions();
        BindInspectionEntityType();
    });
}

function OnOperationSelection() {
    $('#operationList').change(function () {
        if ($(this).val() == 1) {
            $('#activityTypeList').removeAttr("disabled");
            BindInspectionVisitDetails();
        }
        else {
            $('#activityTypeList').prop("disabled", "disabled");
            $('#activityTypeList').empty();
        }
    });
}

function HideModal(modal) {
    modal.removeClass("in");
    $(".modal-backdrop").remove();
    modal.hide();
}

function HandleSuccessModal() {
    $("#addInspectionSuccessModal").modal('show');
    $('#btnSuccessOk').off();
    $('#btnSuccessOk').on('click', function () {
        $("#addInspectionSuccessModal").modal('hide');
        ApprovalSuccess();
    });
}

function ApprovalSuccess() {
    AddLoadingIndicator();
    window.location.reload();
}