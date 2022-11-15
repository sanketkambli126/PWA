import { Amber, HexAmber, HexGreen, HexRed, Green, Red } from "../../common/constants.js";
import { AjaxError, AddLoadingIndicator, RemoveLoadingIndicator, AddClassIfAbsent, RemoveClassIfPresent, IsNullOrEmptyOrUndefined } from "../../common/utilities.js";
import { OpenModalPortServices } from "../../master/voyageReportingModal.js";
require('bootstrap');

var dataLoad = new Set();

$(document).on('click', '.fromAnchorPortAlertCls , .toAnchorPortAlertCls', function () {
    var requestUrl = $(this).data('url');
    OpenModalPortServices(requestUrl);
})

$(document).ready(function () {
    AjaxError();
    AddLoadingIndicator();
    RemoveLoadingIndicator();

    if (($(window).width() < 993)) {
        $("#vesseldetailsshow").click(function () {
            $("#mobilevsdetails").toggleClass("showdetails");
            $("#vesseldetailsshow").toggleClass("arrowchange");
        });
    }

    BindVesselScoreGraph();

    $("input[name=hdnCategoryGraphDetail]").each(function () {
        var modelDimensionId = $(this).next().data().modeldimensionid;
        BindCategoryGraph(modelDimensionId);
    });
});

$('.sentinel-accordation .btn-link').on("click", function () {
    var categoryId = $(this).data().modeldimensionid;
    var vesselId = $(this).data().vesselid;
    if (!dataLoad.has(categoryId)) {
        $.ajax({
            url: "/Sentinel/GetCategoryDetails",
            type: "GET",
            "data": {
                "EncryptedVesselId": vesselId,
                "CategoryId": categoryId
            },
            success: function (data) {
                if (data != null) {
                    $('#card-body-' + categoryId).empty();
                    $('#card-body-' + categoryId).append(data);
                }
            },
            complete: function () {
                $('[data-toggle="tooltip"]').tooltip({
                    trigger: 'hover',
                    boundary: 'window'
                })
            }
        });
        dataLoad.add(categoryId);
    }
})

$('body').on('click', '.customAccordionHeader.categoryHeader', function (e) {
    $('.customAccordionContainer.categoryContainer').find('.customAccordionDetail.categoryDetail').stop().slideUp();
    $(this).closest('.customAccordionContainer.categoryContainer').find('.customAccordionDetail.categoryDetail').stop().slideToggle();
    var hasCollapsed = $(this).hasClass('collapsed');
    $('.sentinel-accordation .categoryContainer .btn-link').each(function () {
        $(this).addClass('collapsed');
    });
    if (hasCollapsed) {
        RemoveClassIfPresent($(this), 'collapsed')
    }
    else {
        AddClassIfAbsent($(this), 'collapsed')
    }
});

$('body').on('click', '.customAccordionHeader.subCategoryHeader', function (e) {
    $('.customAccordionContainer.subCategoryContainer').find('.customAccordionDetail.subCategoryDetail').stop().slideUp();
    $(this).closest('.customAccordionContainer.subCategoryContainer').find('.customAccordionDetail.subCategoryDetail').stop().slideToggle();
    var hasCollapsed = $(this).hasClass('collapsed');
    $('.detailsContainer .subCategoryContainer .btn-link').each(function () {
        $(this).addClass('collapsed');
    });
    if (hasCollapsed) {
        RemoveClassIfPresent($(this), 'collapsed')
    }
    else {
        AddClassIfAbsent($(this), 'collapsed')
    }
});

function BindCategoryGraph(modelDimensionId) {
    var data = $("#hdnCategoryGraphDetail-" + modelDimensionId).val();
    if (!IsNullOrEmptyOrUndefined(data)) {
        var parsedData = $.parseJSON(data);
        var sentinelTotalValues = [];
        var sentinelTotalValueColors = [];
        var statDates = [];
        parsedData.forEach(function (item) {
            if (!IsNullOrEmptyOrUndefined(item.SentinelTotalValue)) {
                var val = item.SentinelTotalValue;
                sentinelTotalValues.push(val);
            }

            if (!IsNullOrEmptyOrUndefined(item.SentinelTotalValueColor)) {
                if (item.SentinelTotalValueColor == Amber) {
                    val = HexAmber;
                }
                if (item.SentinelTotalValueColor == Red) {
                    val = HexRed;
                }
                if (item.SentinelTotalValueColor == Green) {
                    val = HexGreen;
                }
                sentinelTotalValueColors.push(val);
            }

            if (!IsNullOrEmptyOrUndefined(item.StatDate)) {
                val = item.StatDate.substring(0, 10);
                statDates.push(val);
            }
        });

        const ctx = document.getElementById('categoryGraph-' + modelDimensionId).getContext('2d');
        PlotGraph(ctx, sentinelTotalValues, sentinelTotalValueColors, statDates);
    }
}

function BindVesselScoreGraph() {
    var vesselId = $('#hdVesselId').val();
    $.ajax({
        url: "/Sentinel/GetVesselScoreGraph",
        type: "GET",
        "data": {
            "VesselId": vesselId,
        },
        success: function (data) {
            if (data != null) {
                var sentinelTotalValues = [];
                var sentinelTotalValueColors = [];
                var statDates = [];
                data.forEach(function (item) {
                    if (!IsNullOrEmptyOrUndefined(item.sentinelTotalValue)) {
                        var val = item.sentinelTotalValue;
                        sentinelTotalValues.push(val);
                    }

                    if (!IsNullOrEmptyOrUndefined(item.sentinelTotalValueColor)) {
                        if (item.sentinelTotalValueColor == Amber) {
                            val = HexAmber;
                        }
                        if (item.sentinelTotalValueColor == Red) {
                            val = HexRed;
                        }
                        if (item.sentinelTotalValueColor == Green) {
                            val = HexGreen;
                        }
                        sentinelTotalValueColors.push(val);
                    }

                    if (!IsNullOrEmptyOrUndefined(item.statDate)) {
                        val = item.statDate.substring(0, 10);
                        statDates.push(val);
                    }
                });
                const ctx = document.getElementById('vesselGraph').getContext('2d');
                PlotGraph(ctx, sentinelTotalValues, sentinelTotalValueColors, statDates);
            }
        }
    });
}

function PlotGraph(ctx, sentinelTotalValues, sentinelTotalValueColors, statDates) {
    const chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: statDates,
            datasets: [
                {
                    data: sentinelTotalValues,
                    fill: false,
                    tension: 0,
                    pointBackgroundColor: sentinelTotalValueColors
                }
            ]
        },
        options: {
            responsive: false,
            legend: {
                display: false
            },
            layout: {
                padding: {
                    left: 10,
                    right: 10,
                    top: 20,
                    bottom: 20,
                },
            },
            elements: {
                line: {
                    borderColor: '#124D67',
                    borderWidth: 1
                },
                point: {
                    radius: 3
                }
            },
            tooltips: {
                mode: "index",
                intersect: false,
                yAlign: 'center'
            },
            scales: {
                yAxes: [
                    {
                        display: false,
                        ticks: {
                            min: 0,
                            max: 5,
                            stepSize: 1
                        }
                    }
                ],
                xAxes: [
                    {
                        display: false
                    }
                ]
            }
        }
    });
}