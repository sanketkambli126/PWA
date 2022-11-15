import { AjaxError, GetCookie, ToastrAlert } from "../../common/utilities.js"
require('bootstrap');

var SelectedLookUpId = '';
var SelectedLookUpName = '';
var SelectedLookUpDescription = '';
var IsClearedClick = false;
var PageNumber = 1;

$(document).ready(function () {
    AjaxError();
    $('#divSelectedLookUpResultTemplate').hide();
    $("#searchLookUp").attr("placeholder", "Type account code / name");
    $('.removeSelection').click(function () {
        ClearSelectedLookUp();
    });

    $('#searchLookUp').on('keyup', function () {
        if (this.value.length > 2) {
            IsClearedClick = false;
            SearchDetails();
        }
    });

    $('#divLookUpResult').on('scroll', function () {
        if (!IsClearedClick) {
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                PageNumber++;
                LoadLookUpList();
            }
        }
    })
});

$(document).on('click', '.rowTemplate', function () {
    SelectedLookUpId = $(this).attr('id');
    SelectedLookUpName = $(this).attr('data-LookUp-name');
    SelectedLookUpDescription = $(this).attr('data-LookUp-description');

    var localLookUp = $(this);
    $('#divLookUpResult').hide();
    $('#divSelectedLookUpResultTemplate').show();
    $("#divSelectedLookUpResult").empty();
    $('#divSelectedLookUpResult').append(localLookUp);
});

function SearchDetails() {
    $('#divLookUpResult').show();
    $('#divLookUpResult').empty();
    $('#divSelectedLookUpResultTemplate').hide();
    SelectedLookUpId = '';
    SelectedLookUpName = '';
    SelectedLookUpDescription = '';
    LoadLookUpList();
}

function LoadLookUpList() {
    $.ajax({
        url: "/Finance/GetAccountCodesListPaged",
        type: "POST",
        dataType: "JSON",
        data: {
            "inputText": $('#searchLookUp').val(),
            "page": PageNumber,
            "ChhId": $('#ChhId').val()
        },
        success: function (data) {
            CreateTemplate(data.data);
        }
    });
}

function CreateTemplate(data) {
    if (data != null && data != '') {
        for (var i = 0; i < data.length; i++) {
            let LookUpDetails = data[i];
            var newRow = RowCreated(LookUpDetails);
            $('#divLookUpResult').append(newRow);
        }
    }
}

function RowCreated(result) {
    if (result != null) {
        var localRow = $('<div class="row rowTemplate mt-1" id="' + result.id + '" data-LookUp-name="' + result.text + '" data-LookUp-description="' + result.chartDescription + '">' +
            '<div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12"><b>' + result.text + '</b></div>' +
            '<div class= "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12" > <b> CUR : </b>' + result.chartCurrencyId + '</div > ' +
            '<div class= "col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12" > <b> Account Type : </b>' + result.chartType + '</div > ' +
            '</div> <hr/>');

        return localRow;
    }
}

export function GetSelectedLookUpDetails() {
    var selectedLookUp = {
        id: SelectedLookUpId,
        name: SelectedLookUpName,
        description: SelectedLookUpDescription
    }
    return selectedLookUp;
}

export function SetSelectedLookUp(inputText, accountId) {
    $('#searchLookUp').val(inputText);
    $.ajax({
        url: "/Finance/GetSelectedAccountCode",
        type: "POST",
        dataType: "JSON",
        data: {
            "inputText": inputText,
            "accountId": accountId,
            "ChhId": $('#ChhId').val()
        },
        success: function (data) {
            if (data != null) {
                var rowElement = RowCreated(data);
                $('#divLookUpResult').hide();
                $('#divSelectedLookUpResultTemplate').show();
                $('#divSelectedLookUpResult').empty();
                $('#divSelectedLookUpResult').append(rowElement);
            }
            SelectedLookUpName = accountId + " - " + inputText;;
            SelectedLookUpDescription = inputText;
            SelectedLookUpId = accountId;
        }
    });
}

export function ClearSelectedLookUp() {

    $('#searchLookUp').val('');
    $('#divLookUpResult').empty();
    $('#divLookUpResult').show();
    $("#divSelectedLookUpResult").empty();
    $('#divSelectedLookUpResultTemplate').hide();
    SelectedLookUpId = '';
    SelectedLookUpName = '';
    SelectedLookUpDescription = '';
    PageNumber = 1;
    IsClearedClick = true;
}