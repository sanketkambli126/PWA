import { AjaxError, GetCookie, ToastrAlert } from "../../common/utilities.js"
require('bootstrap');

var SelectedCompanyId = '';
var SelectedCompanyName = '';
var IsClearedClick = false;
var PageNumber = 1;

var rowsAppend = new Set();

$(document).ready(function () {
    AjaxError();
    $('#divSelectedCompanyResultTemplate').hide();

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
    });

    var searchtabheight = $('#search-tab-divContent').height();
    console.log($('#search-tab-divContent').height());
    var scrollerheight = $('.scroller-listbox').css({
        "height": searchtabheight - 40
    });
});

$(document).on('click', '.rowTemplate', function () {
    SelectedCompanyId = $(this).attr('id');
    SelectedCompanyName = $(this).attr('data-company-name');
    var localCompany = $(this);
    $('#divCompanyResult').hide();
    $('#divSelectedCompanyResultTemplate').show();
    $("#divSelectedCompanyResult").empty();
    $('#divSelectedCompanyResult').append(localCompany);
    $('#searchCompany').val(SelectedCompanyName);
});

function SearchDetails() {
    $('#divCompanyResult').show();
    $('#divCompanyResult').empty();
    rowsAppend.clear();
    $('#divSelectedCompanyResultTemplate').hide();
    SelectedCompanyId = '';
    SelectedCompanyName = '';
    LoadCompanyList();
}

function LoadCompanyList() {
    $.ajax({
        url: "/Inspection/GetCompanyListPaged",
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
    if (!rowsAppend.has(result.companyId)) {
        var localRow = $('<div class="row rowTemplate" id="' + result.companyId + '" data-company-name="' + result.companyName + '">' +
            '<div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"><div class="counters-heading">' + result.companyName + '</div></div><div class="col-4 col-md-3 col-lg-3 col-xl-3"><div class= "dashboard-counters-label" >Address</div></div><div class="col-8 col-md-9 col-lg-9 col-xl-9"><div class="dashboard-counters">' + result.address + '</div></div>' + '<div class="col-4 col-md-3 col-lg-3 col-xl-3"><div class="dashboard-counters-label">Country</div></div><div class="col-8 col-md-19 col-lg-9 col-xl-9"><div class="dashboard-counters">' + result.country + '</div></div>'
            + '</div> <hr/>');

        rowsAppend.add(result.companyId);
        return localRow;
    }
    else {
        return "";
    }
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
        url: "/Inspection/GetSelectedCompany",
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
    rowsAppend.clear();
}

