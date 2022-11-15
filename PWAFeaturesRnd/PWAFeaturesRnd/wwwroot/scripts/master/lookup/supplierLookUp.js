import { AjaxError, GetCookie, ToastrAlert } from "../../common/utilities.js"
require('bootstrap');

var SelectedCompanyId = '';
var SelectedCompanyName = '';
var IsClearedClick = false;
var PageNumber = 1;
var isSearchPreviouslyEnabled = false;

$(document).ready(function () {
    AjaxError();
    $('#divSelectedCompanyResultTemplate').hide();
    $("#searchCompany").attr("placeholder", "Type here to search supplier");
    $('.removeSelection').click(function () {
        ClearSelectedCompany();
    });

    $('#searchCompany').on('keyup', function () {
        if (this.value.length > 2) {
            IsClearedClick = false;
            SearchDetails();
        }
    });

    $('#divCompanyResult').on('scroll', function () {
        if (!IsClearedClick) {
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                PageNumber++;
                LoadCompanyList();
            }
        }
    })
});

$(document).on('click', '.rowTemplate', function () {
    SelectedCompanyId = $(this).attr('id');
    SelectedCompanyName = $(this).attr('data-company-name');
    var localCompany = $(this);
    $('#divCompanyResult').hide();
    $('#divSelectedCompanyResultTemplate').show();
    $("#divSelectedCompanyResult").empty();
    $('#divSelectedCompanyResult').append(localCompany);
    if (!$("#btnSearch").prop('disabled') && !isSearchPreviouslyEnabled) {
        isSearchPreviouslyEnabled = true;
    }
    $("#btnSearch").prop('disabled', false);
});

function SearchDetails() {
    $('#divCompanyResult').show();
    $('#divCompanyResult').empty();
    $('#divSelectedCompanyResultTemplate').hide();
    SelectedCompanyId = '';
    SelectedCompanyName = '';
    LoadCompanyList();
}

function LoadCompanyList() {
    $.ajax({
        url: "/PurchaseOrder/GetCompanyListPaged",
        type: "POST",
        dataType: "JSON",
        data: {
            "inputText": $('#searchCompany').val(),
            "page": PageNumber
        },
        success: function (data) {
            CreateTemplate(data.data);
        }
    });
}

function CreateTemplate(data) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let companyDetails = data[i];
            var newRow = RowCreated(companyDetails);
            $('#divCompanyResult').append(newRow);
        }
    }
}

function RowCreated(result) {
    var localRow = $('<div class="row rowTemplate mt-1" id="' + result.companyId + '" data-company-name="' + result.companyName + '">' +
        '<div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 pr-4"><b>' + result.companyName + '</b></div> <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"> <b>  Address : </b>' + result.address + '</div>' + '<div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"><b>Country : </b>' + result.country + '</div>' +
        '<div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"><b>Currency : </b>' + result.currency + '</div>' +
        '</div> <hr/>');

    return localRow;
}

export function GetSelectedCompanyDetails() {
    var selectedCompany = {
        id: SelectedCompanyId,
        name: SelectedCompanyName
    }
    return selectedCompany;
}

export function SetSelectedCompay(companyName, companyId) {
    $('#searchCompany').val(companyName);
    $.ajax({
        url: "/PurchaseOrder/GetSelectedCompany",
        type: "POST",
        dataType: "JSON",
        data: {
            "inputText": companyName,
            "companyId": companyId
        },
        success: function (data) {
            var rowElement = RowCreated(data);
            $('#divCompanyResult').hide();
            $('#divSelectedCompanyResultTemplate').show();
            $('#divSelectedCompanyResult').empty();
            $('#divSelectedCompanyResult').append(rowElement);
            SelectedCompanyName = companyName;
            SelectedCompanyId = companyId;
            $("#btnSearch").prop('disabled', false);
        }
    });
}

export function ClearSelectedCompany() {
    
    $('#searchCompany').val('');
    $('#divCompanyResult').empty();
    $('#divCompanyResult').show();
    $("#divSelectedCompanyResult").empty();
    $('#divSelectedCompanyResultTemplate').hide();
    SelectedCompanyId = '';
    SelectedCompanyName = '';
    PageNumber = 1;
    IsClearedClick = true;
    if (!isSearchPreviouslyEnabled) {
        $("#btnSearch").prop('disabled', true);
    }
    isSearchPreviouslyEnabled = false;
}