using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.Models.Report.Sentinel;
using PWAFeaturesRnd.ViewModels.Sentinel;
using PWAFeaturesRnd.ViewModels.VoyageReporting;
using PWAFeaturesRnd.ViewModels.Common;
using Microsoft.AspNetCore.Http;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// HSEQ Manager Dashboard Client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class HSEQManagerDashboardClient : BaseHttpClient
    {
        #region Private Properties

        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HSEQManagerDashboardClient"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="confirguration">The confirguration.</param>
        /// <param name="provider">The provider.</param>
        public HSEQManagerDashboardClient(HttpClient client, IConfiguration confirguration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.HSEQManagerDashboardApiUrl);
            _client = client;
            _configuration = confirguration;
            _provider = provider;
        }

        #endregion

        #region Method

        /// <summary>
        /// Gets the vessel sentinel value by identifier.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<VesselSentinelValueViewModel> GetVesselSentinelValueById(string vesselId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetVesselSentinelValueById/" + vesselId));
            VesselSentinelValue response = await GetAsync<VesselSentinelValue>(requestUrl);
            VesselSentinelValueViewModel result = new VesselSentinelValueViewModel();
            if (response != null)
            {
                result.SentinelTotalValue = response.SentinelTotalValue.HasValue ? Decimal.Round(response.SentinelTotalValue.Value, 1) : response.SentinelTotalValue;
                result.SentinelTotalValueColor = response.SentinelTotalValueColor;
            }
            return result;
        }

        /// <summary>
        /// Gets the sentinel dashboard detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<SentinelDashboardDetailViewModel> GetSentinelDashboardDetail(SentinelDashboardDetailRequestViewModel input)
        {
            string queryString = "VesselId=" + input.VesselId + "&GetCategoryGraphDetails=" + input.GetCategoryGraphDetails;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetSentinelDashboardDetail"), queryString);

            SentinelDashboardDetailResponse response = await GetAsync<SentinelDashboardDetailResponse>(requestUrl);
            SentinelDashboardDetailViewModel result = new SentinelDashboardDetailViewModel();

            if (response != null)
            {
                if (response.VesselCurrentVoyageDetail != null && response.VesselCurrentVoyageDetail.Any())
                {
                    SentinelDashboardVesselCurrentVoyageDetail obj = response.VesselCurrentVoyageDetail.FirstOrDefault();
                    if (obj != null)
                    {
                        VoyageReportingRequestViewModel voyageReportingRequestToPort = new VoyageReportingRequestViewModel();
                        VoyageReportingRequestViewModel voyageReportingRequestFromPort = new VoyageReportingRequestViewModel();

                        voyageReportingRequestFromPort.PortId = obj.FromPortId;
                        voyageReportingRequestFromPort.VesselId = obj.VesselId;

                        voyageReportingRequestToPort.PortId = obj.ToPortId ?? obj.NextPortId;
                        voyageReportingRequestToPort.VesselId = obj.VesselId;

                        result.VesselCurrentVoyageDetail = new VesselCurrentVoyageDetailViewModel()
                        {
                            ActivityName = obj.ActivityName,
                            ToPortIsAlertAdded = obj.ToPortIsAlertAdded,
                            FromPortIsAlertAdded = obj.FromPortIsAlertAdded,
                            NextPortIsAlertAdded = obj.NextPortIsAlertAdded,
                            VesselId = obj.VesselId,
                            Percentage = (int?)((obj.DistanceTravelled * 100) / obj.TotalDistance),
                            ToPortRequestUrl = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestToPort)),
                            FromPortRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestFromPort))
                        };

                        if (!string.IsNullOrWhiteSpace(obj.PlaId))
                        {
                            if (obj.PlaId == Constants.SP)
                            {
                                result.VesselCurrentVoyageDetail.ToPortCountry = string.IsNullOrWhiteSpace(obj.ToPortName) ? null : (obj.ToPortName + ", " + obj.ToCntCode);
                                result.VesselCurrentVoyageDetail.FromPortCountry = obj.FromPortName + ", " + obj.FromCntCode;
                                result.VesselCurrentVoyageDetail.PortDate = obj.ToDate != null ? obj.ToDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                result.VesselCurrentVoyageDetail.FromPortDate = obj.FromDate != null ? obj.FromDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                result.VesselCurrentVoyageDetail.FromDateHeader = Constants.ShortFAOP;
                                result.VesselCurrentVoyageDetail.DateHeader = GetDateHeaderText(obj.EospStatus);
                            }
                            else
                            {
                                result.VesselCurrentVoyageDetail.FromPortCountry = obj.FromPortName + ", " + obj.FromCntCode;
                                result.VesselCurrentVoyageDetail.ToPortCountry = string.IsNullOrWhiteSpace(obj.NextPortName) ? null : (obj.NextPortName + ", " + obj.NextCntCode);
                                result.VesselCurrentVoyageDetail.PortDate = obj.ToDate != null ? obj.ToDate.Value.ToString(Constants.DateTime24HrFormat) : obj.NextDate != null ? obj.NextDate.Value.ToString(Constants.DateTime24HrFormat) : "";

                                result.VesselCurrentVoyageDetail.FromPortDate = obj.FromDate != null ? obj.FromDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                result.VesselCurrentVoyageDetail.FromDateHeader = Constants.ShortEOSP;
                                result.VesselCurrentVoyageDetail.DateHeader = Constants.ShortEOSP;
                            }
                        }
                    }
                }

                if (response.VesselDetail != null && response.VesselDetail.Any())
                {
                    SentinelDashboardVesselDetail obj = response.VesselDetail.FirstOrDefault();
                    if (obj != null)
                    {
                        result.VesselDetail = new SentinelVesselDetailViewModel()
                        {
                            VesselId = obj.VesselId,
                            VesselModelTotalValue = obj.VesselModelTotalValue != null ? Math.Round(obj.VesselModelTotalValue.Value, 1) : (decimal?)null,
                            VesselName = obj.VesselName,
                            VesselFlag = obj.VesselFlag + Constants.Png,
                            DrydockPeriodEndDate = obj.DrydockPeriodEndDate != null ? obj.DrydockPeriodEndDate.Value.ToString(Constants.DateFormat) : ""
                        };
                        if (obj.VesselModelTotalValueColor == Constants.Amber)
                        {
                            result.VesselDetail.VesselModelTotalValueColor = "shield-orange.png";
                        }
                        else if (obj.VesselModelTotalValueColor == Constants.Green)
                        {
                            result.VesselDetail.VesselModelTotalValueColor = "shield-green.png";
                        }
                        else if (obj.VesselModelTotalValueColor == Constants.Red)
                        {
                            result.VesselDetail.VesselModelTotalValueColor = "shield-red.png";
                        }
                        else
                        {
                            result.VesselDetail.VesselModelTotalValueColor = "shield-grey.png";
                        }
                    }
                }
                if (response.ModelValueDetail != null && response.ModelValueDetail.Any())
                {
                    result.ModelValueDetail = new List<VesselModelValueDetailViewModel>();
                    response.ModelValueDetail.ForEach(x =>
                    {
                        VesselModelValueDetailViewModel modelValue = new VesselModelValueDetailViewModel();
                        modelValue.EncryptedVesselId = CommonUtil.GetEncryptedURL<string>(_provider, Constants.VesselId, x.VesselId);
                        modelValue.ModelDimensionId = x.ModelDimensionId;
                        modelValue.ModelDimensionName = x.ModelDimensionName;
                        modelValue.ModelValue = x.ModelValue.HasValue ? Decimal.Round(x.ModelValue.Value, 1) : x.ModelValue;
                        modelValue.ModelValueColor = String.IsNullOrWhiteSpace(x.ModelValueColor) ? "Grey" : x.ModelValueColor;
                        modelValue.HasActiveOverride = x.HasActiveOverride;
                        modelValue.ModelDimensionValueChangeStatus = x.ModelDimensionValueChangeStatus;
                        modelValue.ModelDimensionValueDifference = x.ModelDimensionValueDifference.HasValue ? Decimal.Round(x.ModelDimensionValueDifference.Value, 1) : x.ModelDimensionValueDifference;
                        modelValue.LatestHistoryDate = x.LatestHistoryDate;
                        modelValue.CategoryGraphDetail = new List<VesselSentinelValueViewModel>();
                        x.CategoryGraphDetail.ForEach(y =>
                        {
                            VesselSentinelValueViewModel sentinelValue = new VesselSentinelValueViewModel();
                            sentinelValue.SentinelTotalValue = y.SentinelTotalValue;
                            sentinelValue.SentinelTotalValueColor = y.SentinelTotalValueColor;
                            sentinelValue.StatDate = y.StatDate;
                            modelValue.CategoryGraphDetail.Add(sentinelValue);
                        }
                            );
                        result.ModelValueDetail.Add(modelValue);
                    });
                }

                if (response.VesselStaticFactorDetails != null && response.VesselStaticFactorDetails.Any())
                {
                    result.VesselStaticFactorDetails = new List<VesselModelFactorDetailViewModel>();
                    response.VesselStaticFactorDetails.ForEach(x =>
                    {
                        VesselModelFactorDetailViewModel factorDetails = new VesselModelFactorDetailViewModel();

                        if (x.FactorValueNumeric == null && x.FactorValueText == null)
                        {
                            goto _continue;
                        }
                        if (x.FactorName == Constants.Flag)
                        {
                            goto _continue;
                        }
                        factorDetails.FactorName = x.FactorName;
                        factorDetails.FactorValue = !string.IsNullOrWhiteSpace(x.FactorValueText) ? x.FactorValueText : x.FactorValueNumeric.HasValue ? Math.Round((decimal)x.FactorValueNumeric, 1).ToString() + " yrs" : x.FactorValueNumeric.ToString() + " yrs";

                        result.VesselStaticFactorDetails.Add(factorDetails);
                    _continue:;
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the sentinel dashboard office view.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<OfficeViewDetailResponseViewModel>> GetSentinelDashboardOfficeView(SentinelDashboardDetailRequestViewModel input)
        {
            string queryString = "MenuType=" + input.MenuType + "&UserId=" + input.UserId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetSentinelDashboardOfficeView"), queryString);

            SentinelDashboardOfficeViewDetailRespose response = await GetAsync<SentinelDashboardOfficeViewDetailRespose>(requestUrl);
            List<OfficeViewDetailResponseViewModel> result = new List<OfficeViewDetailResponseViewModel>();
            FleetVesselDetailRequestViewModel fleetVesselListRequest = new FleetVesselDetailRequestViewModel();

            if (response != null)
            {
                if (response.OfficeDetail != null && response.OfficeDetail.Any())
                {
                    response.OfficeDetail.ForEach(x =>
                    {
                        DashboardParameter fleetRequest = new DashboardParameter
                        {
                            MenuType = input.MenuType,
                            Title = x.OfficeName,
                            FleetId = x.OfficeId
                        };
                        result.Add(new OfficeViewDetailResponseViewModel
                        {
                            FleetRequest = CommonUtil.GetEncryptedFleetRequest(_provider, fleetRequest),
                            OfficeId = x.OfficeId,
                            OfficeName = x.OfficeName,
                            HasActiveOverride = x.ActiveOverrideCount != "-",
                            TotalVesselCount = x.TotalVesselCount.GetValueOrDefault()
                        });
                    });
                }

                if (response.ColorVesselCountDetail != null && response.ColorVesselCountDetail.Any())
                {
                    result.ForEach(x =>
                    {
                        response.ColorVesselCountDetail.ForEach(y =>
                        {
                            if (x.OfficeId.Equals(y.OfficeId))
                            {
                                if (y.Color.Equals(Constants.Red))
                                {
                                    x.RedVesselCount = y.VesselCount.GetValueOrDefault();
                                    fleetVesselListRequest.ColorStatus = y.Color;
                                    x.EncryptedVesselListRequestRed = EncryptVesselListRequest(fleetVesselListRequest);
                                }
                                else if (y.Color.Equals(Constants.Amber))
                                {
                                    x.AmberVesselCount = y.VesselCount.GetValueOrDefault();
                                    fleetVesselListRequest.ColorStatus = y.Color;
                                    x.EncryptedVesselListRequestAmber = EncryptVesselListRequest(fleetVesselListRequest);
                                }
                                else if (y.Color.Equals(Constants.Green))
                                {
                                    x.GreenVesselCount = y.VesselCount.GetValueOrDefault();
                                    fleetVesselListRequest.ColorStatus = y.Color;
                                    x.EncryptedFleetVesselListRequestGreen = EncryptVesselListRequest(fleetVesselListRequest);
                                }
                                else if (y.Color.Equals(""))
                                {
                                    x.GreyVesselCount = y.VesselCount.GetValueOrDefault();
                                }
                            }
                        });

                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the sentinel category and factor detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<VesselModelChildValueDetailResponseViewModel>> GetSentinelCategoryAndFactorDetail(CategoryOverrideDetailRequestViewModel input)
        {
            string queryString = "VesselId=" + input.VesselId + "&CategoryId=" + input.CategoryId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetSentinelCategoryAndFactorDetail?"), queryString);

            List<SentinelVesselModelChildValueDetailResponse> response = await GetAsync<List<SentinelVesselModelChildValueDetailResponse>>(requestUrl);
            List<VesselModelChildValueDetailResponseViewModel> result = new List<VesselModelChildValueDetailResponseViewModel>();
            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    VesselModelChildValueDetailResponseViewModel childValueDetail = new VesselModelChildValueDetailResponseViewModel();
                    childValueDetail.VesselId = x.VesselId;
                    childValueDetail.ModelDimensionId = x.ModelDimensionId;
                    childValueDetail.ModelDimensionName = x.ModelDimensionName;
                    childValueDetail.ModelCombinedValue = x.ModelCombinedValue.HasValue ? Decimal.Round(x.ModelCombinedValue.Value, 1) : x.ModelCombinedValue;
                    childValueDetail.ModelCombinedValueColor = x.ModelCombinedValueColor;
                    childValueDetail.ModelCurrentValue = x.ModelCurrentValue.HasValue ? Decimal.Round(x.ModelCurrentValue.Value, 1) : x.ModelCurrentValue;
                    childValueDetail.ModelCurrentValueColor = x.ModelCurrentValueColor;
                    childValueDetail.ModelLaggingValue = x.ModelLaggingValue.HasValue ? Decimal.Round(x.ModelLaggingValue.Value, 1) : x.ModelLaggingValue;
                    childValueDetail.ModelLaggingValueColor = x.ModelLaggingValueColor;
                    childValueDetail.ModelOverrideDimensionId = x.ModelOverrideDimensionId;
                    childValueDetail.ModelOverrideDimensionDescription = x.ModelOverrideDimensionDescription;
                    childValueDetail.ModelOverrideDimensionValue = x.ModelOverrideDimensionValue.HasValue ? Decimal.Round(x.ModelOverrideDimensionValue.Value, 1) : x.ModelOverrideDimensionValue;
                    childValueDetail.ModelOverrideDimensionValueColor = x.ModelOverrideDimensionValueColor;
                    childValueDetail.NavigationLinkName = x.NavigationLinkName;
                    childValueDetail.ModuleName = x.ModuleName;
                    childValueDetail.ViewName = x.ViewName;
                    childValueDetail.NavigationType = x.NavigationType;
                    childValueDetail.NavigationFilter = x.NavigationFilter;
                    childValueDetail.AdditionalInfo = x.AdditionalInfo;
                    childValueDetail.SortOrder = x.SortOrder;
                    childValueDetail.ModelType = x.ModelType;
                    childValueDetail.ModelTypeValue = x.ModelTypeValue;

                    childValueDetail.ModelMachineFactorDetail = new List<VesselModelFactorDetailResponseViewModel>();
                    if (x.ModelMachineFacorDetail != null && x.ModelMachineFacorDetail.Any())
                    {
                        x.ModelMachineFacorDetail.ForEach(y =>
                        {
                            VesselModelFactorDetailResponseViewModel vesselModelFactorDetail = new VesselModelFactorDetailResponseViewModel();
                            vesselModelFactorDetail.FactorValueNumeric = y.FactorValueNumeric.HasValue ? Decimal.Round(y.FactorValueNumeric.Value, 1) : y.FactorValueNumeric;
                            vesselModelFactorDetail.FactorName = y.FactorName;
                            vesselModelFactorDetail.AdditionalInfo = y.AdditionalInfo;
                            vesselModelFactorDetail.NavigationLinkName = y.NavigationLinkName;
                            childValueDetail.ModelMachineFactorDetail.Add(vesselModelFactorDetail);
                        });
                    }
                    childValueDetail.ModelExpertFactorDetail = new List<VesselModelExpertTypeFactorDetailResponseViewModel>();
                    if (x.ModelExpertFactorDetail != null && x.ModelExpertFactorDetail.Any())
                    {
                        x.ModelExpertFactorDetail.ForEach(y =>
                        {
                            VesselModelExpertTypeFactorDetailResponseViewModel vesselModelExpertTypeFactorDetail = new VesselModelExpertTypeFactorDetailResponseViewModel();
                            vesselModelExpertTypeFactorDetail.Indicator = y.Indicator;
                            vesselModelExpertTypeFactorDetail.IndicatorValue = y.IndicatorValue.HasValue ? Decimal.Round(y.IndicatorValue.Value, 1) : y.IndicatorValue;
                            vesselModelExpertTypeFactorDetail.IndicatorColor = y.IndicatorColor;
                            vesselModelExpertTypeFactorDetail.ThresholdValue = y.ThresholdValue.HasValue ? Decimal.Round(y.ThresholdValue.Value, 1) : y.ThresholdValue;
                            vesselModelExpertTypeFactorDetail.Weight = y.Weight.HasValue ? Decimal.Round(y.Weight.Value, 1) : y.Weight;
                            vesselModelExpertTypeFactorDetail.AdditionalInfo = y.AdditionalInfo;
                            childValueDetail.ModelExpertFactorDetail.Add(vesselModelExpertTypeFactorDetail);

                        });
                    }
                    childValueDetail.ModelOverrideDetail = new List<CategoryOverrideDetailResponseViewModel>();
                    if (x.ModelOverrideDetail != null && x.ModelOverrideDetail.Any())
                    {
                        x.ModelOverrideDetail.ForEach(y =>
                        {
                            CategoryOverrideDetailResponseViewModel categoryOverrideDetail = new CategoryOverrideDetailResponseViewModel();
                            categoryOverrideDetail.OverrideDimensionName = y.OverrideDimensionName;
                            categoryOverrideDetail.OverrideDimensionCurrentValue = y.OverrideDimensionCurrentValue.HasValue ? Decimal.Round(y.OverrideDimensionCurrentValue.Value, 1) : y.OverrideDimensionCurrentValue;
                            childValueDetail.ModelOverrideDetail.Add(categoryOverrideDetail);
                        });
                    }
                    result.Add(childValueDetail);
                });
            }
            return result;
        }

        /// <summary>
        /// Gets the sentinel fleet vessel detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<ModelDimensionVesselValueResponseViewModel>> GetSentinelFleetVesselDetail(ModelDimensionVesselValueRequestViewModel input)
        {
            string queryString = "MenuType=" + input.MenuType + "&FleetId=" + input.FleetId + "&UserId=" + input.UserId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetSentinelFleetVesselDetail"), queryString);

            List<SentinelModelDimensionVesselValueResponse> response = await GetAsync<List<SentinelModelDimensionVesselValueResponse>>(requestUrl);
            List<ModelDimensionVesselValueResponseViewModel> result = new List<ModelDimensionVesselValueResponseViewModel>();

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    ModelDimensionVesselValueResponseViewModel dimensionVesselValue = new ModelDimensionVesselValueResponseViewModel();

                    dimensionVesselValue.EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, x.VesselId, x.VesselName, "");
                    dimensionVesselValue.OfficeId = x.OfficeId;
                    dimensionVesselValue.OfficeName = x.OfficeName;
                    dimensionVesselValue.VesselName = x.VesselName;
                    dimensionVesselValue.HasActiveOverride = x.HasActiveOverride;
                    dimensionVesselValue.ModelDimensionValue = x.ModelDimensionValue != null ? Math.Round((decimal)x.ModelDimensionValue, 1) : (decimal?)null;
                    dimensionVesselValue.VesselModelCombinedColor = x.VesselModelCombinedColor;
                    dimensionVesselValue.ModelDimensionValueChangeStatus = x.ModelDimensionValueChangeStatus;
                    dimensionVesselValue.VesselModelCombinedValue = x.VesselModelCombinedValue != null ? Math.Round((decimal)x.VesselModelCombinedValue, 1) : (decimal?)null;

                    if (x.BiggestMoversValue == "-")
                    {
                        dimensionVesselValue.IsBiggestMoversUP = null;
                        dimensionVesselValue.BiggestMoversValue = x.BiggestMoversValue;
                    }
                    else if (x.BiggestMoversValue.Contains("-"))
                    {
                        dimensionVesselValue.IsBiggestMoversUP = false;
                        dimensionVesselValue.BiggestMoversValue = x.BiggestMoversValue.Substring(1);
                    }
                    else
                    {
                        dimensionVesselValue.IsBiggestMoversUP = true;
                        dimensionVesselValue.BiggestMoversValue = x.BiggestMoversValue;
                    }

                    if (x.ModelDimensionValueColor == Constants.Amber)
                    {
                        dimensionVesselValue.ModelDimensionValueColor = "shield-orange.png";
                    }
                    else if (x.ModelDimensionValueColor == Constants.Green)
                    {
                        dimensionVesselValue.ModelDimensionValueColor = "shield-green.png";
                    }
                    else if (x.ModelDimensionValueColor == Constants.Red)
                    {
                        dimensionVesselValue.ModelDimensionValueColor = "shield-red.png";
                    }
                    else
                    {
                        dimensionVesselValue.ModelDimensionValueColor = "shield-grey.png";
                    }

                    if (x.VesselCurrentVoyageDetail != null && x.VesselCurrentVoyageDetail.Any())
                    {
                        SentinelDashboardVesselCurrentVoyageDetail obj = x.VesselCurrentVoyageDetail.FirstOrDefault();
                        if (obj != null)
                        {
                            dimensionVesselValue.VesselCurrentVoyageDetail = new VesselCurrentVoyageDetailViewModel()
                            {
                                ActivityName = obj.ActivityName,
                                ToPortIsAlertAdded = obj.ToPortIsAlertAdded,
                                FromPortIsAlertAdded = obj.FromPortIsAlertAdded,
                                NextPortIsAlertAdded = obj.NextPortIsAlertAdded,
                                VesselId = obj.VesselId,
                                Percentage = (int?)((obj.DistanceTravelled * 100) / obj.TotalDistance)

                            };

                            if (!string.IsNullOrWhiteSpace(obj.PlaId))
                            {
                                if (obj.PlaId == Constants.SP)
                                {
                                    dimensionVesselValue.VesselCurrentVoyageDetail.ToPortCountry = string.IsNullOrWhiteSpace(obj.ToPortName) ? null : (obj.ToPortName + ", " + obj.ToCntCode);
                                    dimensionVesselValue.VesselCurrentVoyageDetail.FromPortCountry = obj.FromPortName + ", " + obj.FromCntCode;
                                    dimensionVesselValue.VesselCurrentVoyageDetail.PortDate = obj.ToDate != null ? obj.ToDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                    dimensionVesselValue.VesselCurrentVoyageDetail.FromPortDate = obj.FromDate != null ? obj.FromDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                    dimensionVesselValue.VesselCurrentVoyageDetail.FromDateHeader = Constants.ShortFAOP;
                                    dimensionVesselValue.VesselCurrentVoyageDetail.DateHeader = GetDateHeaderText(obj.EospStatus);
                                }
                                else
                                {
                                    dimensionVesselValue.VesselCurrentVoyageDetail.FromPortCountry = obj.FromPortName + ", " + obj.FromCntCode;
                                    dimensionVesselValue.VesselCurrentVoyageDetail.ToPortCountry = string.IsNullOrWhiteSpace(obj.NextPortName) ? null : (obj.NextPortName + ", " + obj.NextCntCode);
                                    dimensionVesselValue.VesselCurrentVoyageDetail.PortDate = obj.ToDate != null ? obj.ToDate.Value.ToString(Constants.DateTime24HrFormat) : obj.NextDate != null ? obj.NextDate.Value.ToString(Constants.DateTime24HrFormat) : "";

                                    dimensionVesselValue.VesselCurrentVoyageDetail.FromPortDate = obj.FromDate != null ? obj.FromDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                    dimensionVesselValue.VesselCurrentVoyageDetail.FromDateHeader = Constants.ShortEOSP;
                                    dimensionVesselValue.VesselCurrentVoyageDetail.DateHeader = Constants.ShortEOSP;
                                }
                            }
                        }
                    }
                    result.Add(dimensionVesselValue);

                });
            }

            return result;
        }
        /// <summary>
        /// Gets the vessel list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="pagedRequest">The paged request.</param>
        /// <returns></returns>
        public async Task<PagedResponse<List<FleetVesselDetailResponseViewModel>>> GetVesselList(FleetVesselDetailRequestViewModel input, PagedRequest pagedRequest)
        {
            FleetVesselDetailRequest request = new FleetVesselDetailRequest()
            {
                BiggestMoverRange = input.BiggestMoverRange,
                ColorStatus = input.ColorStatus,
                ConsiderOverrideScoreCategory = input.ConsiderOverrideScoreCategory,
                FleetId = input.FleetId,
                MenuType = input.MenuType,
                ModelDimensionId = input.ModelDimensionId,
                OfficeName = input.OfficeName,
                OverrideDimensionId = input.OverrideDimensionId,
                SortBy = input.SortBy > 0 ? input.SortBy : 1,
                UserId = input.UserId,
                VesselId = input.VesselId,
                VesselName = input.VesselName
            };

            var value = new Dictionary<string, object>()
            {
                { "fleetVesselDetailRequest", request },
                { "pageRequest", pagedRequest }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "v1/SentinelDashboard/GetFleetVesselDetail"));
            PagedResponse<List<FleetVesselDetailResponse>> response = await PostAsync<PagedResponse<List<FleetVesselDetailResponse>>>(requestUrl, CreateHttpContent(value));
            PagedResponse<List<FleetVesselDetailResponseViewModel>> result = new PagedResponse<List<FleetVesselDetailResponseViewModel>>();
            
            if (response != null && response.Result != null && response.Result.Any())
            {
                result.Result = new List<FleetVesselDetailResponseViewModel>();
                response.Result.ForEach(x =>
                {
                    FleetVesselDetailResponseViewModel obj = new FleetVesselDetailResponseViewModel();

                    obj.EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, x.VesselId, x.VesselName, "");
                    obj.VesselName = x.VesselName;
                    obj.OfficeId = x.OfficeId;
                    obj.OfficeName = x.OfficeName;

                    if (x.VesselModelCombinedColor == Constants.Amber)
                    {
                        obj.VesselModelCombinedColor = "shield-orange.png";
                    }
                    else if (x.VesselModelCombinedColor == Constants.Green)
                    {
                        obj.VesselModelCombinedColor = "shield-green.png";
                    }
                    else if (x.VesselModelCombinedColor == Constants.Red)
                    {
                        obj.VesselModelCombinedColor = "shield-red.png";
                    }
                    else
                    {
                        obj.VesselModelCombinedColor = "shield-grey.png";
                    }
                    obj.VesselModelCombinedValue = x.VesselModelCombinedValue != null ? Math.Round((decimal)x.VesselModelCombinedValue, 1) : (decimal?)null;
                    obj.HasActiveOverride = x.HasActiveOverride.GetValueOrDefault();

                    obj.ModelDimensionValueDifference = x.ModelDimensionValueDifference.HasValue ? Decimal.Round(x.ModelDimensionValueDifference.Value, 1) : x.ModelDimensionValueDifference;
                    obj.ModelCategoryName = x.ModelCategoryName.Equals(Constants.Total) ? Constants.OverallScore : x.ModelCategoryName;
                    obj.LatestHistoryDate = x.LatestHistoryDate;
                    obj.ModelCombinedValue = x.ModelCombinedValue.HasValue ? Decimal.Round(x.ModelCombinedValue.Value, 1) : x.ModelCombinedValue;
                    obj.ModelCombinedColor = x.ModelCombinedColor;

                    if (x.VesselCurrentVoyageDetail != null && x.VesselCurrentVoyageDetail.Any())
                    {
                        SentinelDashboardVesselCurrentVoyageDetail voyageObj = x.VesselCurrentVoyageDetail.FirstOrDefault();
                        if (voyageObj != null)
                        {
                            VoyageReportingRequestViewModel voyageReportingRequestToPort = new VoyageReportingRequestViewModel();
                            VoyageReportingRequestViewModel voyageReportingRequestFromPort = new VoyageReportingRequestViewModel();

                            voyageReportingRequestFromPort.PortId = voyageObj.FromPortId;
                            voyageReportingRequestFromPort.VesselId = voyageObj.VesselId;

                            voyageReportingRequestToPort.PortId = voyageObj.ToPortId ?? voyageObj.NextPortId;
                            voyageReportingRequestToPort.VesselId = voyageObj.VesselId;
                            obj.VesselCurrentVoyageDetail = new VesselCurrentVoyageDetailViewModel()
                            {
                                ActivityName = voyageObj.ActivityName,
                                ToPortIsAlertAdded = voyageObj.ToPortIsAlertAdded,
                                FromPortIsAlertAdded = voyageObj.FromPortIsAlertAdded,
                                NextPortIsAlertAdded = voyageObj.NextPortIsAlertAdded,
                                VesselId = voyageObj.VesselId,
                                Percentage = (int?)((voyageObj.DistanceTravelled * 100) / voyageObj.TotalDistance),
                                ToPortRequestUrl = CommonUtil.GetEncryptedURL(_provider,Constants.VoyageReportingURL,voyageReportingRequestToPort),
                                FromPortRequestURL = CommonUtil.GetEncryptedURL(_provider, Constants.VoyageReportingURL, voyageReportingRequestFromPort)
                            }; 
                            if (!string.IsNullOrWhiteSpace(voyageObj.PlaId))
                            {
                                if (voyageObj.PlaId == Constants.SP)
                                {
                                    obj.VesselCurrentVoyageDetail.ToPortCountry = string.IsNullOrWhiteSpace(voyageObj.ToPortName) ? null : (voyageObj.ToPortName + ", " + voyageObj.ToCntCode);
                                    obj.VesselCurrentVoyageDetail.FromPortCountry = voyageObj.FromPortName + ", " + voyageObj.FromCntCode;
                                    obj.VesselCurrentVoyageDetail.PortDate = voyageObj.ToDate != null ? voyageObj.ToDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                    obj.VesselCurrentVoyageDetail.FromPortDate = voyageObj.FromDate != null ? voyageObj.FromDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                    obj.VesselCurrentVoyageDetail.FromDateHeader = Constants.ShortFAOP;
                                    obj.VesselCurrentVoyageDetail.DateHeader = GetDateHeaderText(voyageObj.EospStatus);
                                }
                                else
                                {
                                    obj.VesselCurrentVoyageDetail.FromPortCountry = voyageObj.FromPortName + ", " + voyageObj.FromCntCode;
                                    obj.VesselCurrentVoyageDetail.ToPortCountry = string.IsNullOrWhiteSpace(voyageObj.NextPortName) ? null : (voyageObj.NextPortName + ", " + voyageObj.NextCntCode);
                                    obj.VesselCurrentVoyageDetail.PortDate = voyageObj.ToDate != null ? voyageObj.ToDate.Value.ToString(Constants.DateTime24HrFormat) : voyageObj.NextDate != null ? voyageObj.NextDate.Value.ToString(Constants.DateTime24HrFormat) : "";

                                    obj.VesselCurrentVoyageDetail.FromPortDate = voyageObj.FromDate != null ? voyageObj.FromDate.Value.ToString(Constants.DateTime24HrFormat) : "";
                                    obj.VesselCurrentVoyageDetail.FromDateHeader = Constants.ShortEOSP;
                                    obj.VesselCurrentVoyageDetail.DateHeader = Constants.ShortEOSP;
                                }
                            }
                        }
                    }
                    result.Result.Add(obj);

                });
            }

            result.TotalRecords = response.TotalRecords;
            result.PageSize = response.PageSize;
            result.PageNumber = response.PageNumber;
            result.TotalPages = response.TotalPages;

            return result;
        }

        /// <summary>
        /// Gets the sentinel fleet wise override vessel count.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<OverrideDimensionVesselResponseViewModel>> GetSentinelFleetWiseOverrideVesselCount(SentinelDashboardDetailRequestViewModel input)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, "v1/SentinelDashboard/GetSentinelFleetWiseOverrideVesselCount");

            SentinelDashboardDetailRequest request = new SentinelDashboardDetailRequest()
            {
                MenuType = input.MenuType,
                UserId = input.UserId,
                FleetId = input.FleetId
            };

            List<SentinelOverrideDimensionVesselResponse> response = await PostAsync<List<SentinelOverrideDimensionVesselResponse>>(requestUrl, CreateHttpContent(request));
            List<OverrideDimensionVesselResponseViewModel> result = new List<OverrideDimensionVesselResponseViewModel>();
            FleetVesselDetailRequestViewModel fleetVesselListRequest = new FleetVesselDetailRequestViewModel();
            response.ForEach(x =>
            {
                fleetVesselListRequest.ModelDimensionId = x.ModelDimensionId;
                fleetVesselListRequest.OverrideDimensionId = x.OverrideDimensionId;

                result.Add(new OverrideDimensionVesselResponseViewModel()
                {
                    ParentModelDimensionDescription = x.ParentModelDimensionDescription,
                    ModelDimensionDescription = x.ModelDimensionDescription,
                    OverrideDimension = x.OverrideDimension,
                    VesselCount = x.VesselCount,
                    EncryptedVesselListRequest = EncryptVesselListRequest(fleetVesselListRequest)
                });
            });
            return result;

        }

        /// <summary>
        /// Gets the value difference range.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<ValueDifferenceRangeResponseViewModel>> GetValueDifferenceRange(SentinelDashboardDetailRequestViewModel input)
        {
            SentinelDashboardDetailRequest request = new SentinelDashboardDetailRequest()
            {
                MenuType = input.MenuType,
                UserId = input.UserId,
                FleetId = input.FleetId,
                IsAllSelected = String.IsNullOrWhiteSpace(input.FleetId)
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, "v1/SentinelDashboard/GetValueDifferenceRange");
            List<SentinelValueDifferenceRangeResponse> response = await PostAsync<List<SentinelValueDifferenceRangeResponse>>(requestUrl, CreateHttpContent(request));
            List<ValueDifferenceRangeResponseViewModel> result = new List<ValueDifferenceRangeResponseViewModel>();
            FleetVesselDetailRequestViewModel fleetVesselListRequest = new FleetVesselDetailRequestViewModel();

            List<SentinelValueDifferenceRangeResponse> sortedResponse = response.OrderBy(x => x.SortOrder).ToList();
            foreach (var item in sortedResponse)
            {
                fleetVesselListRequest.BiggestMoverRange = item.RangeIndex;
                result.Add(new ValueDifferenceRangeResponseViewModel()
                {
                    DisplayRange = item.DisplayRange,
                    ComparisonType = item.ComparisonType,
                    VesselCount = item.VesselCount.GetValueOrDefault(),
                    EncryptedVesselListRequest = EncryptVesselListRequest(fleetVesselListRequest)
                });
            }

            return result;
        }

        /// <summary>
        /// Gets the sentinel model dimension.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<TreeViewModel<ModelDimensionResponseViewModel>>> GetSentinelModelDimension(ModelDimensionRequestViewModel input)
        {
            string queryString = "MenuType=" + input.MenuType + "&FleetId=" + input.FleetId + "&UserId=" + input.UserId + "&DimensionLevelString=" + input.DimensionLevelString;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetSentinelModelDimension"), queryString);

            List<SentinelModelDimensionResponse> response = await GetAsync<List<SentinelModelDimensionResponse>>(requestUrl);
            List<ModelDimensionResponseViewModel> result = new List<ModelDimensionResponseViewModel>();
            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    result.Add(new ModelDimensionResponseViewModel()
                    {
                        ModelDimensionId = x.ModelDimensionId,
                        ModelDimensionName = x.ModelDimensionName,
                        ModelDimensionParent = x.ModelDimensionParent,
                        ModelDimensionLevel = x.ModelDimensionLevel,
                        IsOverride = x.IsOverride,
                    });
                });
            }

            List<TreeViewModel<ModelDimensionResponseViewModel>> treeList = new List<TreeViewModel<ModelDimensionResponseViewModel>>();
            ModelDimensionResponseViewModel Overall = result.Where(x => x.ModelDimensionLevel == 0).FirstOrDefault();

            var nodeById = result.Select(item => new TreeViewModel<ModelDimensionResponseViewModel>
            {
                Key = item.ModelDimensionId,
                Title = item.ModelDimensionName,
                Tooltip = item.ModelDimensionName,
                Expanded = false,
                Checkbox = false,
                Lazy = false,
                Children = new List<TreeViewModel<ModelDimensionResponseViewModel>>(),
                AdditionalData = new ModelDimensionResponseViewModel()
                {
                    IsOverride = item.IsOverride,
                    ModelDimensionParent = item.ModelDimensionParent
                }
            }).ToDictionary(item => item.Key);

            foreach (var item in result)
            {
                if (item.ModelDimensionParent == Overall.ModelDimensionId)
                {
                    item.ModelDimensionParent = null;
                }
                var nodeList = String.IsNullOrEmpty(item.ModelDimensionParent) ? treeList : nodeById[item.ModelDimensionParent].Children;
                nodeList.Add(nodeById[item.ModelDimensionId]);
            }
            return treeList;
        }

        /// <summary>
        /// Gets the biggest mover range.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TreeViewModel<BiggestMoverRangeResponseViewModel>>> GetBiggestMoverRangeTree()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, "/v1/SentinelDashboard/GetBiggestMoverRange");

            List<BiggestMoverRangeResponse> response = await GetAsync<List<BiggestMoverRangeResponse>>(requestUrl);
            List<BiggestMoverRangeResponseViewModel> result = new List<BiggestMoverRangeResponseViewModel>();
            response.ForEach(x =>
            {
                result.Add(new BiggestMoverRangeResponseViewModel()
                {
                    ComparisonType = x.ComparisonType,
                    DisplayRange = x.DisplayRange,
                    RangeIndex = x.RangeIndex,
                });

            });
            List<TreeViewModel<BiggestMoverRangeResponseViewModel>> treeList = new List<TreeViewModel<BiggestMoverRangeResponseViewModel>>();

            List<TreeViewModel<BiggestMoverRangeResponseViewModel>> childItems = new List<TreeViewModel<BiggestMoverRangeResponseViewModel>>();
            childItems.AddRange(result.Where(x => x.ComparisonType.Equals(Constants.Increased)).Select(x =>
            new TreeViewModel<BiggestMoverRangeResponseViewModel>()
            {
                Title = x.DisplayRange,
                Expanded = true,
                Key = x.RangeIndex.ToString(),
                Checkbox = false,
                Lazy = false,
                Tooltip = x.DisplayRange,
            }));

            TreeViewModel<BiggestMoverRangeResponseViewModel> increasedNode = new TreeViewModel<BiggestMoverRangeResponseViewModel>()
            {
                Title = "Increased",
                Expanded = true,
                Key = "Increased",
                Checkbox = false,
                Lazy = false,
                Tooltip = "Increased",
                Children = childItems
            };
            treeList.Add(increasedNode);


            childItems = new List<TreeViewModel<BiggestMoverRangeResponseViewModel>>();
            childItems.AddRange(result.Where(x => x.ComparisonType.Equals(Constants.Decreased)).Select(x =>
            new TreeViewModel<BiggestMoverRangeResponseViewModel>()
            {
                Title = x.DisplayRange,
                Expanded = true,
                Key = x.RangeIndex.ToString(),
                Checkbox = false,
                Lazy = false,
                Tooltip = x.DisplayRange,
            }));

            TreeViewModel<BiggestMoverRangeResponseViewModel> decreasedNode = new TreeViewModel<BiggestMoverRangeResponseViewModel>()
            {
                Title = "Decreased",
                Expanded = true,
                Key = "Decreased",
                Checkbox = false,
                Lazy = false,
                Tooltip = "Decreased",
                Children = childItems
            };
            treeList.Add(decreasedNode);

            return treeList;
        }

        /// <summary>
        /// Gets the vessel sentinel score.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<VesselSentinelScoreResponseViewModel>> GetVesselSentinelScore(List<string> input)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, "v1/SentinelDashboard/GetVesselSentinelScore");
            List<VesselSentinelScoreResponse> response = await PostAsync<List<VesselSentinelScoreResponse>>(requestUrl, CreateHttpContent(input));
            List<VesselSentinelScoreResponseViewModel> result = new List<VesselSentinelScoreResponseViewModel>();

            response.ForEach(x =>
            {
                VesselSentinelScoreResponseViewModel obj = new VesselSentinelScoreResponseViewModel()
                {
                    SentinelTotalValue = x.SentinelTotalValue != null ? Math.Round(x.SentinelTotalValue.Value, 1) : (decimal?)null,
                    VesselId = x.VesselId
                };

                if (x.SentinelTotalValueColor == Constants.Amber)
                {
                    obj.SentinelTotalValueColor = "shield-orange.png";
                }
                else if (x.SentinelTotalValueColor == Constants.Green)
                {
                    obj.SentinelTotalValueColor = "shield-green.png";
                }
                else if (x.SentinelTotalValueColor == Constants.Red)
                {
                    obj.SentinelTotalValueColor = "shield-red.png";
                }
                else
                {
                    obj.SentinelTotalValueColor = "shield-grey.png";
                }

                result.Add(obj);
            });

            return result;
        }

        /// <summary>
        /// Gets the fleet list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<OfficeViewDetailResponseViewModel>> GetFleetList(OfficeFleetVesselCountRequestViewModel input)
        {
            OfficeFleetVesselCountRequest request = new OfficeFleetVesselCountRequest()
            {
                MenuType = input.MenuType,
                FleetId = input.FleetId,
                UserId = input.UserId,
                FleetName = input.FleetName,
                ColorStatus = input.ColorStatus,
                ConsiderOverrideScoreCategory = input.ConsiderOverrideScoreCategory,
                SortBy = input.SortBy,
                IsAllSelected = input.IsAllSelected,
                GetFleetList = true
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "v1/SentinelDashboard/GetOfficeFleetVesselCount"));
            SentinelDashboardOfficeViewDetailRespose response = await PostAsync<SentinelDashboardOfficeViewDetailRespose>(requestUrl, CreateHttpContent(request));
            List<OfficeViewDetailResponseViewModel> result = new List<OfficeViewDetailResponseViewModel>();
            if (response != null)
            {
                if (response.OfficeDetail != null && response.OfficeDetail.Any())
                {
                    response.OfficeDetail.ForEach(x =>
                    {
                        DashboardParameter fleetRequest = new DashboardParameter
                        {
                            MenuType = input.MenuType,
                            Title = x.OfficeName,
                            FleetId = x.OfficeId
                        };
                        result.Add(new OfficeViewDetailResponseViewModel
                        {
                            FleetRequest = CommonUtil.GetEncryptedFleetRequest(_provider, fleetRequest),
                            OfficeId = x.OfficeId,
                            OfficeName = x.OfficeName,
                            HasActiveOverride = x.ActiveOverrideCount != "-",
                            TotalVesselCount = x.TotalVesselCount.GetValueOrDefault()
                        });
                    });
                }

                if (response.ColorVesselCountDetail != null && response.ColorVesselCountDetail.Any())
                {
                    result.ForEach(x =>
                    {
                        response.ColorVesselCountDetail.ForEach(y =>
                        {
                            if (x.OfficeId.Equals(y.OfficeId))
                            {
                                if (y.Color.Equals(Constants.Red))
                                {
                                    x.RedVesselCount = y.VesselCount.GetValueOrDefault();
                                }
                                else if (y.Color.Equals(Constants.Amber))
                                {
                                    x.AmberVesselCount = y.VesselCount.GetValueOrDefault();
                                }
                                else if (y.Color.Equals(Constants.Green))
                                {
                                    x.GreenVesselCount = y.VesselCount.GetValueOrDefault();
                                }
                                else if (y.Color.Equals(""))
                                {
                                    x.GreyVesselCount = y.VesselCount.GetValueOrDefault();
                                }
                            }
                        });

                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the fleet vessel summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<FleetVesselSummaryViewModel> GetFleetVesselSummary(OfficeFleetVesselCountRequestViewModel input)
        {
            OfficeFleetVesselCountRequest request = new OfficeFleetVesselCountRequest()
            {
                MenuType = input.MenuType,
                FleetId = input.FleetId,
                UserId = input.UserId,
                IsAllSelected = String.IsNullOrWhiteSpace(input.FleetId) ? true : false
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "v1/SentinelDashboard/GetOfficeFleetVesselCount"));
            SentinelDashboardOfficeViewDetailRespose response = await PostAsync<SentinelDashboardOfficeViewDetailRespose>(requestUrl, CreateHttpContent(request));
            FleetVesselSummaryViewModel result = new FleetVesselSummaryViewModel();

            if (response != null)
            {
                if (response.OfficeDetail != null && response.OfficeDetail.Any())
                {
                    response.OfficeDetail.ForEach(x =>
                    {
                        result.TotalVesselCount = x.TotalVesselCount.GetValueOrDefault();
                        result.OverrideCount = x.OverrideCount.GetValueOrDefault();
                    });
                }
                if (response.ColorVesselCountDetail != null && response.ColorVesselCountDetail.Any())
                {
                    response.ColorVesselCountDetail.ForEach(x =>
                    {
                        if (x.Color.Equals(Constants.Red))
                        {
                            result.RedVesselCount = x.VesselCount.GetValueOrDefault();
                        }
                        else if (x.Color.Equals(Constants.Amber))
                        {
                            result.AmberVesselCount = x.VesselCount.GetValueOrDefault();
                        }
                        else if (x.Color.Equals(Constants.Green))
                        {
                            result.GreenVesselCount = x.VesselCount.GetValueOrDefault();
                        }
                        else if (x.Color.Equals(""))
                        {
                            result.GreyVesselCount = x.VesselCount.GetValueOrDefault();
                        }
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the sentinel vessel history graph detail.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselSentinelValueViewModel>> GetSentinelVesselHistoryGraphDetail(string vesselId)
        {
            string queryString = "VesselId=" + vesselId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "/v1/SentinelDashboard/GetSentinelVesselHistoryGraphDetail"), queryString);
            List<VesselSentinelValue> response = await GetAsync<List<VesselSentinelValue>>(requestUrl);

            List<VesselSentinelValueViewModel> result = new List<VesselSentinelValueViewModel>();

            if(response != null)
            {
                response.ForEach(x =>
                {
                    result.Add(new VesselSentinelValueViewModel()
                    {
                        SentinelTotalValue = x.SentinelTotalValue,
                        SentinelTotalValueColor = x.SentinelTotalValueColor,
                        StatDate = x.StatDate

                    });
                });
            }

            return result;
        }

            #endregion

        #region Private Methods

        /// <summary>
        /// Gets the date header text.
        /// </summary>
        /// <returns></returns>
        private string GetDateHeaderText(string EospStatus)
            {
                PositionListDateStatus? eospStauts = GetPositionListDateStatus(EospStatus);
                if (eospStauts.HasValue && eospStauts.Value == PositionListDateStatus.ACT)
                {
                    return Constants.ShortEOSP;
                }
                else if (!eospStauts.HasValue || eospStauts.Value == PositionListDateStatus.EST)
                {
                    return Constants.ETA;
                }
                return Constants.ShortEOSP;
            }

            /// <summary>
            /// Gets the position list date status.
            /// </summary>
            /// <param name="status">The status.</param>
            /// <returns></returns>
            private PositionListDateStatus? GetPositionListDateStatus(string status)
            {
                if (!string.IsNullOrWhiteSpace(status))
                {
                    return (PositionListDateStatus)Enum.Parse(typeof(PositionListDateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(PositionListDateStatus), status));
                }
                else
                {
                    return default(PositionListDateStatus?);
                }
            }

            /// <summary>
            /// Encrypts the vessel list request.
            /// </summary>
            /// <param name="fleetVesselList">The fleet vessel list.</param>
            /// <returns></returns>
            private string EncryptVesselListRequest(FleetVesselDetailRequestViewModel fleetVesselList)
            {
                string vesselListRequest = CommonUtil.GetEncryptedURL(_provider, Constants.VesselList, fleetVesselList);
                return vesselListRequest;
            }
            #endregion
    }
}