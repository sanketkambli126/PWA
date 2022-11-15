import "jquery.fancytree";
import { AddClassIfAbsent, RemoveClassIfPresent, AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, IsNullOrEmptyOrUndefined } from "../../common/utilities.js"
import { Increased, Decreased, All, Red, Amber, Green } from '../../common/constants.js'
import { OpenModalPortServices } from "../../master/voyageReportingModal.js";
require('bootstrap');
var filterCount = 0;
var fleetFilterCount = 0;
var isFleetTabSelected = false;
var isFleetListLoaded = false;

$(document).on('click', '.fromAnchorPortAlertCls , .toAnchorPortAlertCls', function () {
    var requestUrl = $(this).data('url');
    OpenModalPortServices(requestUrl);
})

$(document).ready(function () {
    var fleetRequest = $('#fleetRequest').val();
    LoadCategoryHierarchyTree(fleetRequest);
    LoadBiggestMoversTree();
    SetColorStatus();
    LoadFleetVesselSummary();

    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();

    HideFiltersOnTabSelect();

    if (($(window).width() < 767)) {
        var formheight = ($(window).height());
        var filterhead = ($(".sentinel-filter .filterpanel").outerHeight());
        var footerbtn = ($(".sentinel-filter .search-section-bottom").outerHeight());
        var total = formheight - filterhead - footerbtn - 60;

        $('.sentinel-filter .tab-content').attr('style', 'height:' + total + 'px !important');
    }

    $(window).on('resize', function () {
        if (($(window).width() < 767)) {
            var formheight = ($(window).height());
            var filterhead = ($(".sentinel-filter .filterpanel").outerHeight());
            var footerbtn = ($(".sentinel-filter .search-section-bottom").outerHeight());
            var total = formheight - filterhead - footerbtn - 60;

            $('.sentinel-filter .tab-content').attr('style', 'height:' + total + 'px !important');
        }
    });

});

$("#tabStatus .listrepeat").on('click', function () {
    RemoveClassIfPresent($('#tabStatus *'), 'activeelement')
    AddClassIfAbsent(this.children, 'activeelement');
})

$('#btnSearch').click(function () {
    if (isFleetTabSelected) {
        LoadFilterdFleetData(true);
    }
    else {
        LoadFilteredData(true, 1);
    }
});

$('#btnClear').click(function () {
    if (isFleetTabSelected) {
        clearFilterFleetTab()
    }
    else {
        ClearFilter(false);
    }
});

$(".searchtext img").on('click', function () {
    LoadDataAsPerSelectedTemplate();
});

$("#searchText").bind('input', function (e) {
    var value = $("#searchText").val().trim();
    if (e.keyCode == 13) {
        if (value.length > 1) {
            LoadFilteredData(false, 1);
        }
    }
    else if (value == "") {
        LoadFilteredData(false, 1);
    }
});

$("#fleetSearchText").bind('input', function (e) {
    var value = $("#fleetSearchText").val().trim();
    if (e.keyCode == 13) {
        if (value.length > 1) {
            LoadFilterdFleetData(false);
        }
    }
    else if (value == "") {
        LoadFilterdFleetData(false);
    }
});


$('#checkboxOverride').on('click', function () {
    LoadFilteredData(false, 1);
})

$("#divLoadMore").on('click', function () {
    var pageNumber = $("#hdnPageNumber").val();
    LoadFilteredData(false, ++pageNumber);
})

$("#sortdrpmenu .sortlabellink").on('click', function () {
    RemoveClassIfPresent($('#sortdrpmenu *'), 'activesort')
    AddClassIfAbsent(this, 'activesort');
    $('#sortdrpmenu').toggleClass("show");
    LoadFilteredData(false, 1);
})

$("#fleet-tab").on('click', function () {
    AddClassIfAbsent($(".vesselFilter"), 'd-none');
    RemoveClassIfPresent($(".fleetFilter"), 'd-none');
    AddClassIfAbsent($("#tabLinkFleetStatus"), 'active');
    AddClassIfAbsent($("#tabFleetStatus"), 'active show');
    if (isFleetListLoaded == false) {
        LoadFilterdFleetData(false);
        isFleetListLoaded = true;
    }
    isFleetTabSelected = true;
    SetFilterButtonCount();
});

$("#vessel-tab").on('click', function () {
    isFleetTabSelected = false;
    HideFiltersOnTabSelect();
    SetFilterButtonCount();
})

$("#fleetsortdrpmenu .fleetsortlabellink").on('click', function () {
    RemoveClassIfPresent($('#fleetsortdrpmenu *'), 'activesort')
    AddClassIfAbsent(this, 'activesort');
    $('#fleetsortdrpmenu').toggleClass("show");
    LoadFilterdFleetData(false);
});

$("#tabFleetStatus .listrepeat").on('click', function () {
    RemoveClassIfPresent($('#tabFleetStatus *'), 'activeelement')
    AddClassIfAbsent(this.children, 'activeelement');
});

$('#FleetCheckboxOverride').on('click', function () {
    LoadFilterdFleetData(false);
})

$("#vessel .redscore span").on("click", function () {
    ClearFilter(true);
    SetColorStatusActive(Red);
    LoadFilteredData(false, 1);
})

$("#vessel .orangescore span").on("click", function () {
    ClearFilter(true);
    SetColorStatusActive(Amber);
    LoadFilteredData(false, 1);
})

$("#vessel .greenscore span").on("click", function () {
    ClearFilter(true);
    SetColorStatusActive(Green);
    LoadFilteredData(false, 1);
});

function LoadCategoryHierarchyTree(fleetRequest) {

    $("#CategoryHierarchyTree").fancytree({
        checkbox: false,
        selectMode: 1,
        icon: function (event, data) {
            if (!$.isEmptyObject(data.node.data)) {
                let additionalData = data.node.data.additionalData;
                return !IsNullOrEmptyOrUndefined(additionalData) && additionalData.isOverride ? "/images/sentinelimages/s-cycle-grey.svg" : false;
            }
            else {
                return false;
            }
        },
        source:
            $.ajax({
                url: "/Sentinel/GetCategoryAndOverridesTree",
                dataType: "json",
                data: { fleetRequest: fleetRequest }
            }),
        init: function(event, data) {
            SetCategoryTree();
            $('span[title="Overall"]').parents().parents().addClass('toptreemenu');
        }
    });
}

function LoadBiggestMoversTree() {
    $("#BiggestMoversTree").fancytree({
        checkbox: false,
        selectMode: 1,
        icon: false,
        source:
            $.ajax({
                url: "/Sentinel/GetBiggestMoversTree",
                dataType: "json"
            }),
        beforeActivate: function (event, data) {
            if (data.node.key == Increased || data.node.key == Decreased) {
                return false;
            }
        },
        init: function (event, data) {
            SetBiggestMoversTree();
        }
    });
}



function LoadFilteredData(isSearchClicked,pageNumber) {

    var selectedCategoryNode = $.ui.fancytree.getTree("#CategoryHierarchyTree").getActiveNode();
    var additionalData = selectedCategoryNode != null ? selectedCategoryNode.data.additionalData : null;
    var selectedCategory;
    var selectedOverride;
    if (additionalData != null && additionalData.isOverride) {
        selectedOverride = selectedCategoryNode != null ? selectedCategoryNode.key : null;
        selectedCategory = additionalData.modelDimensionParent;
        $("#checkboxOverride").prop("checked",false);
        $("#checkboxOverride").attr("disabled", true);
    }
    else {
        selectedOverride = null;
        selectedCategory = selectedCategoryNode != null ? selectedCategoryNode.key : null;
        $("#checkboxOverride").attr("disabled", false);
    }

    var selectedBigggestMoversNode = $.ui.fancytree.getTree("#BiggestMoversTree").getActiveNode();
    var selectedBiggestMovers = selectedBigggestMoversNode != null ? selectedBigggestMoversNode.key : null;

    var selectedStatus = $("#tabStatus .activeelement").text();

    var searchText = $("#searchText").val();

    var considerOverrideScoreCategory = $("#checkboxOverride").prop('checked');

    var selectedSort = $("#sortdrpmenu .activesort").attr("data-index");

    if (isSearchClicked) {
        filterCount = 0;

        if (selectedCategory != null) {
            filterCount++;
        }

        if (selectedStatus != "") {
            filterCount++;
        }

        if (selectedBiggestMovers != null) {
            filterCount++;
        }

        SetFilterButtonCount();
    }

    var input = {
        "modelDimensionId": selectedCategory,
        "biggestMoverRange": selectedBiggestMovers != null ? parseInt(selectedBiggestMovers) : selectedBiggestMovers,
        "colorStatus": selectedStatus == All ? null : selectedStatus,
        "overrideDimensionId": selectedOverride,
        "vesselName": searchText,
        "officeName": $("#IsVLOfficeSearchAvailable").val() ? searchText : null ,
        "considerOverrideScoreCategory": considerOverrideScoreCategory,
        "pageNumber": pageNumber,
        "sortBy": selectedSort
    }

    GetFilteredResults(input);
}

function ClearFilter(isShieldCountClicked) {
    $("#hdnOverrideDimensionId").val('');
    $("#hdnBiggestMoverRange").val('');
    $("#hdnColorStatus").val('');

    var fleetRequest = $('#fleetRequest').val();
    LoadCategoryHierarchyTree(fleetRequest);
    LoadBiggestMoversTree();
    RemoveClassIfPresent($('#tabStatus *'), 'activeelement');
    RemoveClassIfPresent($(".btn-filters"), 'btn-filters-active');
    filterCount = 0;
    if (!isShieldCountClicked) {
        LoadFilteredData(false, 1);
        SetFilterButtonCount();
    }
    else {
        $("#searchText").val('');
        if ($('#checkboxOverride').is(':checked')) {
            $('#checkboxOverride').prop("checked", false);
        }
    }
}

function LoadDataAsPerSelectedTemplate() {
    if (isFleetTabSelected) {
        LoadFilterdFleetData(false);
    }
    else {
        LoadFilteredData(false, 1);
    }
}

function GetFilteredResults(input) {

    var fleetRequest = $('#fleetRequest').val();

    $.ajax({
        url: "/Sentinel/GetFilteredResults",
        type: "POST",
        data: {
            "fleetRequest": fleetRequest,
            "input": input
        },
        beforeSend: function (xhr) {
            if (input.pageNumber == 1) {
                $(".vessellisttemplate").empty();
            }
            $("#hdnPageNumber").remove();
            $("#hdnTotalPages").remove();
        },
        success: function (data) {
            if (data != null) {
                $(".vessellisttemplate").append(data);
            }
        },
        complete: function () {
            $('[data-toggle="tooltip"]').tooltip({
                trigger: 'hover',
                boundary: 'window'
            })
            var pageNumber = $("#hdnPageNumber").val();
            var totalPages = $("#hdnTotalPages").val();
            if (pageNumber == totalPages || totalPages == 0) {
                AddClassIfAbsent($("#divLoadMore"), 'd-none');
            }
            else {
                RemoveClassIfPresent($("#divLoadMore"), 'd-none');
            }
        }
    });
}


function GetFleetListResults(input) {
    var fleetRequest = $('#fleetRequest').val();
    $.ajax({
        url: "/Sentinel/GetFleetListResults",
        type: "POST",
        data: {
            "fleetRequest": fleetRequest,
            "input":input
        },
        beforeSend: function (xhr) {   
                $(".fleetListTemplate").empty();
        },
        success: function (data) {
            if (data != null) {
                $(".fleetListTemplate").append(data);
            }
        },
        complete: function () {
            $('[data-toggle="tooltip"]').tooltip({
                trigger: 'hover',
                boundary: 'window'
            })
        }
        
    })
}

function SetCategoryTree() {
    var overrideDimensionId = $("#hdnOverrideDimensionId").val();
    $("#CategoryHierarchyTree").fancytree("getTree").visit(function (node) {
        if (overrideDimensionId == node.key) {
            node.setActive();
            if (node.data.additionalData.isOverride == true) {
                $("#checkboxOverride").prop("checked", false);
                $("#checkboxOverride").attr("disabled", true);
            }
            filterCount++;
        }
    });

    SetFilterButtonCount(); 
}

function SetBiggestMoversTree() {
    var biggestMoverRange = $("#hdnBiggestMoverRange").val();
    $("#BiggestMoversTree").fancytree("getTree").visit(function (node) {
        if (biggestMoverRange == node.key) {
            node.setActive();
            filterCount++;
        }
    });
    SetFilterButtonCount();
}

function SetColorStatus() {
    SetColorStatusActive($("#hdnColorStatus").val());
    SetFilterButtonCount();
}

function SetFilterButtonCount() {
    var counter = isFleetTabSelected ? fleetFilterCount : filterCount;
    if (counter > 0) {
        AddClassIfAbsent($(".btn-filters"), 'btn-filters-active');
        $(".btn-filters").append("<div class='activecount'>" + counter + "</div>");
    }
    
    else {
        RemoveClassIfPresent($(".btn-filters"), 'btn-filters-active');
        $(".activecount").remove();
    }
}

function LoadFilterdFleetData(isSearchClicked) {
    var selectedStatus = $("#tabFleetStatus .activeelement").text();
    var searchText = $("#fleetSearchText").val();
    var considerOverrideScoreCategory = $("#FleetCheckboxOverride").prop('checked');
    var selectedSort = $("#fleetsortdrpmenu .activesort").attr("data-index");
    if (isSearchClicked) {
        fleetFilterCount = 0;
        if (selectedStatus != "") {
            fleetFilterCount++;
        }
        SetFilterButtonCount();
    }
    var input = {
        "fleetName": searchText,
        "colorStatus": selectedStatus == All ? null : selectedStatus,
        "considerOverrideScoreCategory": considerOverrideScoreCategory,
        "sortBy": selectedSort
    }
    GetFleetListResults(input);
}

function clearFilterFleetTab() {
    RemoveClassIfPresent($('#tabFleetStatus *'), 'activeelement');
    RemoveClassIfPresent($(".btn-filters"), 'btn-filters-active');
    fleetFilterCount = 0;
    SetFilterButtonCount();
    LoadFilterdFleetData(false);
}

function LoadFleetVesselSummary(){
    var fleetRequest = $('#fleetRequest').val();
    $.ajax({
        url: "/Sentinel/GetFleetVesselSummary",
        type: "POST",
        data: {
            "fleetRequest": fleetRequest,
        },
        success: function (data) {
            if (data != null) {
                $("#vessel .totalVessels").html(data.totalVesselCount);
                $("#vessel .lists .stxt-red").html(data.overrideCount);
                $("#vessel .redscore span").html(data.redVesselCount);
                $("#vessel .orangescore span").html(data.amberVesselCount);
                $("#vessel .greenscore span").html(data.greenVesselCount);
                $("#vessel .greyscore span").html(data.greyVesselCount);

                $("#fleet .totalVessels").html(data.totalVesselCount);
                $("#fleet .lists .stxt-red").html(data.overrideCount);
                $("#fleet .redscore span").html(data.redVesselCount);
                $("#fleet .orangescore span").html(data.amberVesselCount);
                $("#fleet .greenscore span").html(data.greenVesselCount);
                $("#fleet .greyscore span").html(data.greyVesselCount);
            }
        }
    });
}

function SetColorStatusActive(colorStatus) {
    var colorList = $('#tabStatus .alllist');
    RemoveClassIfPresent($('#tabStatus *'), 'activeelement')
    colorList.children().each(function () {
        var data = $(this).children().text();
        if (data == colorStatus) {
            AddClassIfAbsent($(this).children(), 'activeelement');
            filterCount++;
        }
    });
}
function HideFiltersOnTabSelect() {
    AddClassIfAbsent($(".fleetFilter"), 'd-none');
    RemoveClassIfPresent($(".vesselFilter"), 'd-none');
}
