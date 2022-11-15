using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Certificate;
using PWAFeaturesRnd.Models.Report.Crew;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.Models.Report.Finance;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.ViewModels.Approval;
using PWAFeaturesRnd.ViewModels.Certificate;
using PWAFeaturesRnd.ViewModels.Crew;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Defect;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.PurchaseOrder;
using PWAFeaturesRnd.ViewModels.Shared;
using PWAFeaturesRnd.ViewModels.VoyageReporting;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// Shared client
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class SharedClient : BaseHttpClient
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasingClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public SharedClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.SharedWebApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        /// <summary>
        /// Gets the navigation tree top level.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<List<NavigationTreeViewModel>> GetNavigationTreeTopLevel(NavigationTreeViewModel navigationTreeViewModel)
        {
            List<NavigationTreeViewModel> response = new List<NavigationTreeViewModel>();
            var input = new Dictionary<string, object>()
            {
                { "preloadUserFleet", navigationTreeViewModel.PreloadUserFleet },
                { "exclusions", navigationTreeViewModel.Exclusion }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/TopLevelNavigationTree"));
            List<UserMenuItem> result = await PostAsync<List<UserMenuItem>>(requestUrl, CreateHttpContent(input));

            if (navigationTreeViewModel.TreeType == TreeType.FleetOverviewTree)
            {
                response.Add(new NavigationTreeViewModel()
                {
                    Title = EnumsHelper.GetDescription(UserMenuItemType.AllVessels),
                    Tooltip = EnumsHelper.GetDescription(UserMenuItemType.AllVessels),
                    Key = "",
                    Lazy = false,
                    Children = null,
                    UserMenuItemType = UserMenuItemType.AllVessels,
                    Checkbox = false,
                    Expanded = false,
                    IsVessel = false,
                    AllowFleetSelection = navigationTreeViewModel.AllowFleetSelection
                });
            }

            if (result != null && result.Any())
            {
                result.ForEach(x =>
                {
                    NavigationTreeViewModel data = new NavigationTreeViewModel();

                    if (x.UserMenuItemType == UserMenuItemType.MyClients && navigationTreeViewModel.UserType == UserType.Client)
                    {
                        data.Title = EnumsHelper.GetDescription(UserMenuItemType.MyFleets);
                        data.Tooltip = EnumsHelper.GetDescription(UserMenuItemType.MyFleets);
                    }
                    else
                    {
                        data.Title = x.DisplayText;
                        data.Tooltip = x.DisplayText;
                    }

                    data.Key = x.Identifier;
                    data.Lazy = x.Children != null && x.Children.Any() ? false : true;
                    data.UserMenuItemType = x.UserMenuItemType;
                    data.Checkbox = false;
                    data.Expanded = false;
                    data.IsVessel = x.UserMenuItemType == UserMenuItemType.Vessel ? true : false;
                    data.UserMenuTypeShortCode = GetVesselMenuType(x.UserMenuItemType);
                    data.Identifier = x.Identifier;
                    data.AllowFleetSelection = navigationTreeViewModel.AllowFleetSelection;

                    if (x.Children != null && x.Children.Any())
                    {
                        data.Children = new List<NavigationTreeViewModel>();
                        x.Children.ForEach(x =>
                        {
                            data.Children.Add(new NavigationTreeViewModel()
                            {
                                Title = x.DisplayText,
                                Tooltip = x.DisplayText,
                                Key = GetVesselTreeKey(x),
                                Lazy = x.Children != null && x.Children.Any() ? false : true,
                                Children = x.UserMenuItemType == UserMenuItemType.Vessel ? new List<NavigationTreeViewModel>() : null,
                                UserMenuItemType = x.UserMenuItemType,
                                Checkbox = false,
                                Expanded = false,
                                IsVessel = x.UserMenuItemType == UserMenuItemType.Vessel ? true : false,
                                UserMenuTypeShortCode = string.IsNullOrEmpty(data.UserMenuTypeShortCode) ? GetVesselMenuType(x.UserMenuItemType) : data.UserMenuTypeShortCode,
                                Identifier = x.Identifier,
                                AllowFleetSelection = navigationTreeViewModel.AllowFleetSelection
                            });
                        });
                    }
                    response.Add(data);
                });
            }

            return response;
        }

        /// <summary>
        /// Gets the vessel tree key.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <returns></returns>
        private string GetVesselTreeKey(UserMenuItem menuItem)
        {
            string Key = "";
            if (menuItem.UserMenuItemType == UserMenuItemType.Vessel)
            {
                if (menuItem.OtherIdentifiers.ContainsKey("CoyId"))
                {
                    var coyId = menuItem.OtherIdentifiers["CoyId"];
                    if (!string.IsNullOrWhiteSpace(coyId.ToString()))
                    {
                        Key = _provider.CreateProtector("Vessel").Protect(menuItem.Identifier + Constants.Separator + menuItem.DisplayText + Constants.Separator + coyId);
                    }
                }
            }
            else
            {
                Key = menuItem.Identifier;
            }
            return Key;
        }

        /// <summary>
        /// Gets the navigation tree top level without vessel.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<List<NavigationTreeViewModel>> GetNavigationTreeTopLevelWithoutVessel(NavigationTreeViewModel navigationTreeViewModel)
        {
            List<NavigationTreeViewModel> response = new List<NavigationTreeViewModel>();
            var input = new Dictionary<string, object>()
            {
                { "preloadUserFleet", navigationTreeViewModel.PreloadUserFleet },
                { "exclusions", navigationTreeViewModel.Exclusion }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/TopLevelNavigationTree"));
            List<UserMenuItem> result = await PostAsync<List<UserMenuItem>>(requestUrl, CreateHttpContent(input));

            if (navigationTreeViewModel.TreeType == TreeType.FleetOverviewTree)
            {
                response.Add(new NavigationTreeViewModel()
                {
                    Title = EnumsHelper.GetDescription(UserMenuItemType.AllVessels),
                    Tooltip = EnumsHelper.GetDescription(UserMenuItemType.AllVessels),
                    Key = "",
                    Lazy = false,
                    Children = null,
                    UserMenuItemType = UserMenuItemType.AllVessels,
                    Checkbox = false,
                    Expanded = false,
                    IsVessel = false
                });
            }

            result.ForEach(x =>
            {
                if (x.UserMenuItemType != UserMenuItemType.Vessel)
                {
                    NavigationTreeViewModel data = new NavigationTreeViewModel();

                    if (x.UserMenuItemType == UserMenuItemType.MyClients && navigationTreeViewModel.UserType == UserType.Client)
                    {
                        data.Title = EnumsHelper.GetDescription(UserMenuItemType.MyFleets);
                        data.Tooltip = EnumsHelper.GetDescription(UserMenuItemType.MyFleets);
                    }
                    else
                    {
                        data.Title = x.DisplayText;
                        data.Tooltip = x.DisplayText;
                    }

                    data.Key = x.Identifier;
                    data.Lazy = x.Children != null && x.Children.Any() ? false : true;
                    data.UserMenuItemType = x.UserMenuItemType;
                    data.Checkbox = false;
                    data.Expanded = false;
                    data.IsVessel = false;
                    data.UserMenuTypeShortCode = GetVesselMenuType(x.UserMenuItemType);
                    data.Identifier = x.Identifier;
                    data.AllowFleetSelection = navigationTreeViewModel.AllowFleetSelection;

                    if (x.Children != null && x.Children.Any())
                    {
                        data.Children = new List<NavigationTreeViewModel>();
                        x.Children.ForEach(x =>
                        {
                            if (x.UserMenuItemType != UserMenuItemType.Vessel)
                            {
                                data.Children.Add(new NavigationTreeViewModel()
                                {
                                    Title = x.DisplayText,
                                    Tooltip = x.DisplayText,
                                    Key = x.Identifier,
                                    Lazy = x.Children != null && x.Children.Any() ? false : true,
                                    Children = null,
                                    UserMenuItemType = x.UserMenuItemType,
                                    Checkbox = false,
                                    Expanded = false,
                                    IsVessel = false,
                                    UserMenuTypeShortCode = string.IsNullOrEmpty(data.UserMenuTypeShortCode) ? GetVesselMenuType(x.UserMenuItemType) : data.UserMenuTypeShortCode,
                                    Identifier = x.Identifier,
                                    AllowFleetSelection = navigationTreeViewModel.AllowFleetSelection
                                });
                            }
                        });
                    }
                    response.Add(data);
                }

            });

            return response;
        }

        /// <summary>
        /// Gets the navigation tree level.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<List<NavigationTreeViewModel>> GetNavigationTreeLevel(NavigationTreeViewModel navigationTreeViewModel)
        {
            List<NavigationTreeViewModel> response = new List<NavigationTreeViewModel>();

            UserMenuItem userMenuItem = new UserMenuItem();
            userMenuItem.Identifier = navigationTreeViewModel.Key;
            userMenuItem.DisplayText = navigationTreeViewModel.Title;
            userMenuItem.UserMenuItemType = navigationTreeViewModel.UserMenuItemType;

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/NavigationTree"));
            UserMenuItem result = await PostAsync<UserMenuItem>(requestUrl, CreateHttpContent(userMenuItem));

            if (result.Children != null && result.Children.Any())
            {
                result.Children.ForEach(x =>
                {
                    NavigationTreeViewModel data = new NavigationTreeViewModel();

                    if (x.UserMenuItemType == UserMenuItemType.Vessel)
                    {
                        if (x.OtherIdentifiers.ContainsKey("CoyId"))
                        {
                            var coyId = x.OtherIdentifiers["CoyId"];
                            if (!string.IsNullOrWhiteSpace(coyId.ToString()))
                            {
                                data.Key = _provider.CreateProtector("Vessel").Protect(x.Identifier + Constants.Separator + x.DisplayText + Constants.Separator + coyId);
                            }
                        }
                        
                    }
                    else
                    {
                        data.Key = (x.UserMenuItemType == UserMenuItemType.Vessel ? _provider.CreateProtector("Vessel").Protect(x.Identifier + Constants.Separator + x.DisplayText) : x.Identifier);
                    }
                    data.Title = x.DisplayText;
                    data.Identifier = x.Identifier;
                    data.Lazy = true;
                    data.Tooltip = x.DisplayText;
                    data.UserMenuItemType = x.UserMenuItemType;
                    data.Checkbox = false;
                    data.Expanded = false;
                    data.Children = x.UserMenuItemType == UserMenuItemType.Vessel ? new List<NavigationTreeViewModel>() : null;
                    data.IsVessel = x.UserMenuItemType == UserMenuItemType.Vessel ? true : false;
                    if (!string.IsNullOrEmpty(navigationTreeViewModel.ParentUserMenuTypeShortCode))
                    {
                        data.UserMenuTypeShortCode = navigationTreeViewModel.ParentUserMenuTypeShortCode;
                    }
                    else
                    {
                        data.UserMenuTypeShortCode = GetVesselMenuType(x.UserMenuItemType);
                    }
                    data.AllowFleetSelection = navigationTreeViewModel.AllowFleetSelection;
                    response.Add(data);
                });
            }
            return response;
        }

        /// <summary>
        /// Gets the navigation tree level without vessel.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<List<NavigationTreeViewModel>> GetNavigationTreeLevelWithoutVessel(NavigationTreeViewModel navigationTreeViewModel)
        {
            List<NavigationTreeViewModel> response = new List<NavigationTreeViewModel>();

            UserMenuItem userMenuItem = new UserMenuItem();
            userMenuItem.Identifier = navigationTreeViewModel.Key;
            userMenuItem.DisplayText = navigationTreeViewModel.Title;
            userMenuItem.UserMenuItemType = navigationTreeViewModel.UserMenuItemType;

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/NavigationTree"));
            UserMenuItem result = await PostAsync<UserMenuItem>(requestUrl, CreateHttpContent(userMenuItem));

            if (result.Children != null && result.Children.Any())
            {

                result.Children.ForEach(x =>
                {
                    if (x.UserMenuItemType != UserMenuItemType.Vessel)
                    {
                        NavigationTreeViewModel data = new NavigationTreeViewModel();

                        data.Key = x.Identifier;
                        data.Identifier = x.Identifier;
                        data.Title = x.DisplayText;
                        data.Lazy = true;
                        data.Tooltip = x.DisplayText;
                        data.UserMenuItemType = x.UserMenuItemType;
                        data.Checkbox = false;
                        data.Expanded = false;
                        data.Children = null;
                        data.IsVessel = false;
                        if (!string.IsNullOrEmpty(navigationTreeViewModel.ParentUserMenuTypeShortCode))
                        {
                            data.UserMenuTypeShortCode = navigationTreeViewModel.ParentUserMenuTypeShortCode;
                        }
                        else
                        {
                            data.UserMenuTypeShortCode = GetVesselMenuType(x.UserMenuItemType);
                        }
                        response.Add(data);
                    }
                });
            }
            return response;
        }

        /// <summary>
        /// Gets the type of the vessel menu.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private string GetVesselMenuType(UserMenuItemType type)
        {
            switch (type)
            {
                case UserMenuItemType.MyClients:
                case UserMenuItemType.Client:
                    return "C";
                case UserMenuItemType.MyOffices:
                case UserMenuItemType.Office:
                    return "O";
                case UserMenuItemType.MyFleets:
                    return "F";
                case UserMenuItemType.MyFavourites:
                    return "U";
                case UserMenuItemType.MyResponsibilities:
                    return "R";
                case UserMenuItemType.LeavingManagement:
                    return "L";
                case UserMenuItemType.EnteringManagement:
                    return "EM";
                case UserMenuItemType.ResponsibleEnteringManagement:
                    return "RE";
                case UserMenuItemType.LeftManagement:
                    return "LM";
                case UserMenuItemType.ResponsibleLeftManagement:
                    return "RL";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Posts the get company detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<CompanyDetails> PostGetCompanyDetail(string input)
        {
            CompanyDetails result = new CompanyDetails();

            string companyId = _provider.CreateProtector("Company").Unprotect(input);

            string queryString = "companyId=" + companyId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/Company"), queryString);
            CompanyDetail response = await PostAsync<CompanyDetail>(requestUrl, CreateHttpContent(companyId));

            if (response != null)
            {
                result.CompanyName = response.CmpName;
                result.Notes = response.CmpNote;
                result.Address = string.IsNullOrWhiteSpace(response.CmpAddr) ? null : response.CmpAddr;
                result.Town = string.IsNullOrWhiteSpace(response.CmpTown) ? null : response.CmpTown;
                result.State = string.IsNullOrWhiteSpace(response.CmpState) ? null : response.CmpState;
                result.PostalCode = string.IsNullOrWhiteSpace(response.CmpPostCode) ? null : response.CmpPostCode;
                result.Fax = string.IsNullOrWhiteSpace(response.CmpFax) ? null : response.CmpFax;
                result.Mobile = string.IsNullOrWhiteSpace(response.CmpMobile) ? null : response.CmpMobile;
                result.Telephone = string.IsNullOrWhiteSpace(response.CmpTelephone) ? null : response.CmpTelephone;
                result.Telex = string.IsNullOrWhiteSpace(response.CmpTelex) ? null : response.CmpTelex;
                result.Email = string.IsNullOrWhiteSpace(response.CmpEmail) ? null : response.CmpEmail;
                result.Website = string.IsNullOrWhiteSpace(response.CmpWww) ? null : response.CmpWww;

                if (response.Country != null)
                {
                    result.Country = response.Country.CntDesc;
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get document details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<DocumentDetail>> PostGetDocumentDetails(string input)
        {
            List<DocumentDetailViewModel> result = new List<DocumentDetailViewModel>();

            DocumentDetailRequest documentRequest = new DocumentDetailRequest();
            string data = _provider.CreateProtector("DocumentURL").Unprotect(input);
            documentRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentDetailRequest>(data);

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/DocumentDetails"));
            List<DocumentDetail> response = await PostAsync<List<DocumentDetail>>(requestUrl, CreateHttpContent(documentRequest));

            return response;
        }

        /// <summary>
        /// Posts the get management vessel lookup.
        /// </summary>
        /// <param name="vesselFilter">The vessel filter.</param>
        /// <returns></returns>
        public async Task<List<ManagementVesselDetailViewModel>> PostGetManagementVesselLookup(ManagementVesselFilter vesselFilter)
        {
            List<ManagementVesselDetailViewModel> VesselLookup = new List<ManagementVesselDetailViewModel>();

            List<ManagementVesselDetail> result = null;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/VesselLookup"));

            result = await PostAsync<List<ManagementVesselDetail>>(requestUrl, CreateHttpContent(vesselFilter));


            if (result != null && result.Any())
            {
                foreach (ManagementVesselDetail item in result)
                {
                    ManagementVesselDetailViewModel vesselLookupViewModel = new ManagementVesselDetailViewModel();
                    vesselLookupViewModel.AccountingCompanyId = item.AccountingCompanyId;
                    vesselLookupViewModel.VesselId = item.VesselId;
                    vesselLookupViewModel.VesselName = item.VesselName;
                    vesselLookupViewModel.VesselURL = _provider.CreateProtector("Vessel").Protect(item.VesselId + Constants.Separator + item.VesselName + " - " + item.AccountingCompanyId + Constants.Separator + item.AccountingCompanyId);
                    VesselLookup.Add(vesselLookupViewModel);
                }
            }

            return VesselLookup;
        }

        /// <summary>
        /// Posts the get supplier details.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<CompanySearchResponseViewModel>>> PostGetSupplierDetails(DataTablePageRequest<string> pageRequest, CompanySearchRequest request)
        {
            DataTablePageResponse<List<CompanySearchResponseViewModel>> result = new DataTablePageResponse<List<CompanySearchResponseViewModel>>();
            result.Data = new List<CompanySearchResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<CompanySearchResponse>> response = null;
            var value = new Dictionary<string, object>()
            {
                { "request", request },
                { "pageRequest", pagedRequest }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/Companies"));
            response = await PostAsync<PagedResponse<List<CompanySearchResponse>>>(requestUrl, CreateHttpContent(value));

            if (response.Result != null)
            {
                foreach (CompanySearchResponse item in response.Result)
                {
                    CompanySearchResponseViewModel supplierVM = new CompanySearchResponseViewModel();
                    supplierVM.CompanyName = item.CompanyName;
                    supplierVM.CompanyId = item.CompanyId;
                    supplierVM.Address = item.Address ?? "";
                    supplierVM.Currency = item.Currency ?? "";
                    supplierVM.Country = item.Country ?? "";
                    result.Data.Add(supplierVM);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;

        }

        /// <summary>
        /// Posts the get covid list asynchronous.
        /// </summary>
        /// <param name="tableList">The table list.</param>
        /// <returns></returns>
        public async Task<List<TableAttribute>> PostGetCovidListAsync(List<string> tableList)
        {
            var covidRequestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/TableAttributesForTableList"));
            List<TableAttribute> covidResponse = await PostAsync<List<TableAttribute>>(covidRequestUrl, CreateHttpContent(tableList));
            return covidResponse;
        }

        /// <summary>
        /// Posts the get department list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DepartmentViewModel>> PostGetDepartmentList()
        {
            List<DepartmentViewModel> result = new List<DepartmentViewModel>();
            List<Lookup> response = new List<Lookup>();
            DepartmentFilter filter = new DepartmentFilter();
            filter.IsActive = true;
            filter.IsVessel = true;
            var input = new Dictionary<string, object>()
            {
                { "filter",filter },
                { "isSortBySequenceNumber", true }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/Departments"));
            response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(input));

            if (response != null && response.Any())
            {
                foreach (Lookup item in response)
                {
                    DepartmentViewModel department = new DepartmentViewModel();
                    department.Id = item.Identifier;
                    department.Text = item.Description;
                    department.DepartmentId = item.Identifier;
                    department.DepartmentName = item.Description;
                    result.Add(department);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the company address details.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns></returns>
        public async Task<CompanyDetailViewModel> GetCompanyAddressDetails(string companyId)
        {
            CompanyDetailViewModel companyDetails = new CompanyDetailViewModel();
            CompanyDetail response = new CompanyDetail();

            string urlRequest = "companyId=" + companyId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CompanyWithPaths"), urlRequest);
            response = await PostAsync<CompanyDetail>(requestUrl, CreateHttpContent(companyId));

            if (response != null)
            {
                companyDetails.CompanyAddress = response.CmpAddr ?? "";
                companyDetails.CompanyLocalAddress = (response.CmpTown ?? "") + " " + (response.CmpState ?? "") + " " + (response.CmpPostCode ?? "");
                companyDetails.CompanyCountryDesc = response.Country != null ? (response.Country.CntDesc ?? "") : "";
                companyDetails.CountryCode = response.Country != null ? response.Country.CntId : "";
                companyDetails.TelephoneNumber = response.CmpTelephone ?? "";
                companyDetails.MobileNumber = response.CmpMobile ?? "";
                companyDetails.FaxNumber = response.CmpFax ?? "";
                companyDetails.Email = response.CmpEmail ?? "";
                companyDetails.WebAddress = response.CmpWww ?? "";
                companyDetails.ProcurmentType = response.ProcurementType ?? "";
                companyDetails.IsAuditCompleted = response.IsSupplierAuditCompleted ? "Yes" : "No";
                companyDetails.AuditCompletedDate = response.SupplierAuditStartDate.HasValue ? response.SupplierAuditStartDate.Value.ToString(Constants.DateFormat) : "";
                companyDetails.IsCompanyCertified = response.IsCompanyCertified ? "Yes" : "No";
                companyDetails.IsCompanyAddressVisible = !string.IsNullOrWhiteSpace(response.CmpAddr) ? true : false;
                companyDetails.IsCountryVisible = response.Country != null && !string.IsNullOrWhiteSpace(response.Country.CntDesc) ? true : false;
                companyDetails.IsTelephoneVisible = !string.IsNullOrWhiteSpace(response.CmpTelephone) ? true : false;
                companyDetails.IsMobileVisible = !string.IsNullOrWhiteSpace(response.CmpMobile) ? true : false;
                companyDetails.IsFaxVisible = !string.IsNullOrWhiteSpace(response.CmpFax) ? true : false;
                companyDetails.IsEmailVisible = !string.IsNullOrWhiteSpace(response.CmpEmail) ? true : false;
                companyDetails.IsWebAddressVisible = !string.IsNullOrWhiteSpace(response.CmpWww) ? true : false;
                companyDetails.IsProcurmentVisible = !string.IsNullOrWhiteSpace(response.ProcurementType) ? true : false;
                companyDetails.IsAuditDateVisible = response.IsSupplierAuditCompleted;
                companyDetails.CompanyName = response.CmpName;
            }

            return companyDetails;
        }

        /// <summary>
        /// Posts the check vessel is in purchasing management.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<bool> PostCheckVesselIsInPurchasingManagement(AuthorizeQuoteRequestViewModel input)
        {
            bool isVesselInManagement = false;
            var request = new Dictionary<string, object>()
            {
                { "accountingCompanyId",input.AccountingCompanyId },
                { "vesselId", input.VesselId },
                { "checkPrefundingFlag", input.CheckPrefundingFlag}
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CheckVesselIsInPurchasingManagement"));
            isVesselInManagement = await PostAsync<bool>(requestUrl, CreateHttpContent(request));

            return isVesselInManagement;
        }

        /// <summary>
        /// Posts the get company rejection reasons.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> PostGetCompanyRejectionReasons(ReasonType reason)
        {
            List<Lookup> rejectionResasons = new List<Lookup>();

            string urlRequest = "reasonType=" + reason;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CompanyRejectionReasons"), urlRequest);
            rejectionResasons = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(reason));
            return rejectionResasons;
        }

        /// <summary>
        /// Posts the get control permissions.
        /// </summary>
        /// <param name="controlIds">The control ids.</param>
        /// <returns></returns>
        public async Task<List<ControlPermission>> PostGetControlPermissions(List<string> controlIds)
        {
            List<ControlPermission> response = new List<ControlPermission>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetControlPermissions"));
            response = await PostAsync<List<ControlPermission>>(requestUrl, CreateHttpContent(controlIds));
            return response;
        }

        /// <summary>
        /// Posts the get insurance claims by coy identifier paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="accountingCompanyId">The accounting company identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<AuxiliaryResponseViewModel>>> PostGetInsuranceClaimsByCoyIdPaged(DataTablePageRequest<string> pageRequest, string accountingCompanyId, AuxiliarySearchRequestForLookUp searchRequest)
        {
            DataTablePageResponse<List<AuxiliaryResponseViewModel>> result = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();
            result.Data = new List<AuxiliaryResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<AuxiliaryResponse>> response = null;
            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "accountingCompanyId",accountingCompanyId},
                { "searchRequest", searchRequest }

            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/InsuranceClaims"));
            response = await PostAsync<PagedResponse<List<AuxiliaryResponse>>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Result != null && response.Result.Any())
            {
                foreach (var item in response.Result)
                {
                    AuxiliaryResponseViewModel insuranceClaim = new AuxiliaryResponseViewModel();
                    insuranceClaim.CoyID = item.CoyID ?? "";
                    insuranceClaim.Description = item.Description ?? "";
                    insuranceClaim.Id = item.Identifier ?? "";
                    insuranceClaim.Identifier = item.Identifier ?? "";
                    insuranceClaim.ShortCode = item.ShortCode ?? "";
                    insuranceClaim.Type = item.Type ?? "";
                    if (!string.IsNullOrEmpty(item.ShortCode))
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            insuranceClaim.ShortCodeDesc = item.ShortCode + " - " + item.Description;
                            insuranceClaim.Text = item.ShortCode + " - " + item.Description;
                        }
                        else
                        {
                            insuranceClaim.ShortCodeDesc = item.ShortCode;
                            insuranceClaim.Text = item.ShortCode;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            insuranceClaim.ShortCodeDesc = item.Identifier + " - " + item.Description;
                            insuranceClaim.Text = item.Identifier + " - " + item.Description;
                        }
                        else
                        {
                            insuranceClaim.ShortCodeDesc = item.Identifier;
                            insuranceClaim.Text = item.Identifier;
                        }

                    }

                    result.Data.Add(insuranceClaim);
                }
            }
            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;
            return result;
        }

        /// <summary>
        /// Posts the get crew rank list for aux paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<AuxiliaryResponseViewModel>>> PostGetCrewRankListForAuxPaged(DataTablePageRequest<string> pageRequest, string searchText)
        {
            DataTablePageResponse<List<AuxiliaryResponseViewModel>> result = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();
            result.Data = new List<AuxiliaryResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<AuxiliaryResponse>> response = null;
            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "searchText",searchText},
                { "shortCode", searchText },
                { "isQuickSearch", true }

            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CrewRankListForAux"));
            response = await PostAsync<PagedResponse<List<AuxiliaryResponse>>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Result != null && response.Result.Any())
            {
                foreach (var item in response.Result)
                {
                    AuxiliaryResponseViewModel crewRank = new AuxiliaryResponseViewModel();
                    crewRank.CoyID = item.CoyID ?? "";
                    crewRank.Description = item.Description ?? "";
                    crewRank.Id = item.Identifier ?? "";
                    crewRank.Identifier = item.Identifier ?? "";
                    crewRank.ShortCode = item.ShortCode ?? "";
                    crewRank.Type = item.Type ?? "";
                    crewRank.Text = item.Description ?? "";
                    if (!string.IsNullOrEmpty(item.ShortCode))
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            crewRank.ShortCodeDesc = item.ShortCode + " - " + item.Description;
                        }
                        else
                        {
                            crewRank.ShortCodeDesc = item.ShortCode;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            crewRank.ShortCodeDesc = item.Identifier + " - " + item.Description;
                        }
                        else
                        {
                            crewRank.ShortCodeDesc = item.Identifier;
                        }

                    }

                    result.Data.Add(crewRank);
                }
            }
            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;
            return result;
        }

        /// <summary>
        /// Posts the get vessel aux list paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<AuxiliaryResponseViewModel>>> PostGetVesselAuxListPaged(DataTablePageRequest<string> pageRequest, AuxiliarySearchRequestForLookUp searchRequest)
        {
            DataTablePageResponse<List<AuxiliaryResponseViewModel>> result = new DataTablePageResponse<List<AuxiliaryResponseViewModel>>();
            result.Data = new List<AuxiliaryResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<AuxiliaryResponse>> response = null;
            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "searchRequest", searchRequest }

            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/VesselAuxList"));
            response = await PostAsync<PagedResponse<List<AuxiliaryResponse>>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Result != null && response.Result.Any())
            {
                foreach (var item in response.Result)
                {
                    AuxiliaryResponseViewModel vesselAux = new AuxiliaryResponseViewModel();
                    vesselAux.CoyID = item.CoyID;
                    vesselAux.Description = item.Description ?? "";
                    vesselAux.Id = item.Identifier ?? "";
                    vesselAux.Identifier = item.Identifier ?? "";
                    vesselAux.ShortCode = item.ShortCode ?? "";
                    vesselAux.Type = item.Type ?? "";
                    vesselAux.Text = item.Description ?? "";
                    if (!string.IsNullOrEmpty(item.ShortCode))
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            vesselAux.ShortCodeDesc = item.ShortCode + " - " + item.Description;
                        }
                        else
                        {
                            vesselAux.ShortCodeDesc = item.ShortCode;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            vesselAux.ShortCodeDesc = item.Identifier + " - " + item.Description;
                        }
                        else
                        {
                            vesselAux.ShortCodeDesc = item.Identifier;
                        }

                    }

                    result.Data.Add(vesselAux);
                }
            }
            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;
            return result;
        }

        /// <summary>
        /// Posts the get authorization rights.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<AuthorizationRightsViewModel>> PostGetAuthorizationRights(Dictionary<string, object> input)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/AuthorizationRights"));
            List<AuthorizationRightsResponse> response = await PostAsync<List<AuthorizationRightsResponse>>(requestUrl, CreateHttpContent(input));
            List<AuthorizationRightsViewModel> result = new List<AuthorizationRightsViewModel>();
            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    AuthorizationRightsViewModel authorizationRights = new AuthorizationRightsViewModel();
                    authorizationRights.Priority = x.Priority;
                    authorizationRights.RoleIdentifier = x.RoleIdentifier;
                    result.Add(authorizationRights);
                });
            }
            return result;
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>
        /// UserViewModel
        /// </returns>
        public async Task<UserViewModel> GetCurrentUser()
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CurrentUser"));
            User response = await GetAsync<User>(requestUrl);

            UserViewModel result = new UserViewModel();
            if (response != null)
            {
                result.Roles = response.Roles;
                result.UserDisplayName = response.UserDisplayName;
                result.UserId = response.UserId;
                result.UserLoginLogId = response.UserLoginLogId;
                result.Username = response.Username;
            }
            return result;
        }

        /// <summary>
        /// Gets the report light by filename.
        /// </summary>
        /// <param name="reportFilename">The report filename.</param>
        /// <returns></returns>
        public async Task<ReportLight> GetReportLightByFilename(string reportFilename)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetReportLightByFilename/" + reportFilename));
            ReportLight response = await GetAsync<ReportLight>(requestUrl);
            return response;
        }

        /// <summary>
        /// Initiates the report request.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<string> InitiateReportRequest(ReportLight input)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/ReportExecutor"));
            var response = await PostAsync<string>(requestUrl, CreateHttpContent(input));
            return response;
        }

        /// <summary>
        /// Vessels the certificate audit log.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<VesselCertificateAuditLogDetail>> VesselCertificateAuditLog(Dictionary<string, object> input)
        {

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/VesselCertificateAuditLog"));
            var response = await PostAsync<List<VesselCertificateAuditLogDetail>>(requestUrl, CreateHttpContent(input));
            return response;
        }

        /// <summary>
        /// Gets the company list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<CompanySearchResponseViewModel>> GetCompanyList(CompanySearchRequest request)
        {
            List<CompanySearchResponseViewModel> result = new List<CompanySearchResponseViewModel>();
            List<CompanySearchResponse> response = null;
            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/Companies"));
            response = await PostAsyncAutoPaged<CompanySearchResponse>(requestUrl, value, 100);

            if (response != null && response.Any())
            {
                foreach (CompanySearchResponse item in response)
                {
                    CompanySearchResponseViewModel supplierVM = new CompanySearchResponseViewModel();
                    supplierVM.CompanyName = item.CompanyName;
                    supplierVM.CompanyId = item.CompanyId;
                    supplierVM.Address = item.Address ?? "";
                    supplierVM.Currency = item.Currency ?? "";
                    supplierVM.Country = item.Country ?? "";
                    result.Add(supplierVM);
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the cloud document search.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<CloudDocumentSearchResponseViewModel>> PostCloudDocumentSearch(CloudDocumentSearchRequest request)
        {
            List<CloudDocumentSearchResponseViewModel> result = new List<CloudDocumentSearchResponseViewModel>();
            List<CloudDocumentSearchResponse> response = new List<CloudDocumentSearchResponse>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CloudDocumentSearch"));

            response = await PostAsync<List<CloudDocumentSearchResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (CloudDocumentSearchResponse item in response)
                {
                    CloudDocumentSearchResponseViewModel document = new CloudDocumentSearchResponseViewModel();
                    document.CoyId = item.CoyId;
                    document.CloudDocumentIdentifier = item.CloudDocumentIdentifier;
                    document.DocumentCategory = item.DocumentCategory;
                    document.DocumentFilename = item.DocumentFilename;
                    document.FileType = item.FileType;
                    document.MatchedId = item.MatchedId;
                    document.DocumentCateogoryId = item.DocumentCateogoryId;
                    document.DocumentDescription = item.DocumentDescription;
                    document.DocumentSizeInBytes = item.DocumentSizeInBytes;
                    result.Add(document);
                }
            }
            return result;
        }


        /// <summary>
        /// Posts the get vessel dashboard list paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<VesselDashboardViewModel>>> PostGetVesselDashboardListPaged(DataTablePageRequest<string> pageRequest, VesselDashboardRequest searchRequest)
        {
            DataTablePageResponse<List<VesselDashboardViewModel>> result = new DataTablePageResponse<List<VesselDashboardViewModel>>();
            result.Data = new List<VesselDashboardViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<VesselDashboardResponse>> response = null;
            var value = new Dictionary<string, object>()
            {
                { "searchRequest", searchRequest },
                { "pageRequest", pagedRequest }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/DashboardVesselList"));
            response = await PostAsync<PagedResponse<List<VesselDashboardResponse>>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Result != null && response.Result.Any())
            {
                foreach (VesselDashboardResponse x in response.Result)
                {
                    string vesselID = _provider.CreateProtector("Vessel").Protect(x.VesselId + Constants.Separator + x.VesselName + " - " + x.CoyId + Constants.Separator + x.CoyId);
                    result.Data.Add(new VesselDashboardViewModel()
                    {
                        VesselIdentifier = x.VesselId,
                        VesselId = vesselID,
                        VesselName = x.VesselName,
                        VesselUrl = "/Dashboard/?VesselId=" + vesselID,
                        VesselMobileUrl = "/Dashboard/VesselDetailsMobile/?VesselId=" + vesselID,
                        DropDownId = "#vessel" + vesselID,
                        ManageStartDate = x.ManageStartDate,
                        CertificateColor = GetCertificateColorImagePath(x.Certificate),
                        CommercialColor = GetCommercialColorImagePath(x.Commercial),
                        CrewColor = GetCrewColorImagePath(x.Crew),
                        DefectsColor = GetDefectColorImagePath(x.Defects),
                        DrillAndCampaignColor = GetDrillAndCampaignColorImagePath(x.DrillAndCampaign),
                        EnvironmentColor = GetEnvironmentColorImagePath(x.Environment),
                        HazOccColor = GetHazOccColorImagePath(x.HazOcc),
                        InspectionColor = GetInspectionColorImagePath(x.Inspection),
                        JsaColor = GetJSAAndMOCColorImagePath(x.JSAAndMOC),
                        OpexColor = GetOpexColorImagePath(x.Opex),
                        PMSColor = GetPMSColorImagePath(x.PMS),
                        PurchaseOrderColor = GetPurchaseOrderColorImagePath(x.PurchaseOrder),
                        DefectsURL = GetDefectManagerURL(vesselID),
                        PurchaseOrderURL = SetUpPurchaseOrderUrl(vesselID),
                        CrewURL = CrewNavigation(vesselID),
                        InspectionURL = SetInspectionURL(vesselID, x.VesselId),
                        CertificateURL = SetCertificateURL(vesselID),
                        OpexURL = SetOpexURL(vesselID, x.CoyId),
                        CommercialURL = GetCommercialURL(vesselID, x.VesselId),
                        OpexOverSpend = x.OpexOverSpend + "%",
                        Location = x.Location,
                        DestinationPortAddress = x.DestinationPortAddress,
                        Longitude = CreateLongitude(x.LongDegree, x.LongMin, x.LongIndicator),
                        Lattitude = CreateLatitude(x.LatDegree, x.LatMin, x.LatIndicator),
                        EstimatedDate = x.EstimatedDate.HasValue ? x.EstimatedDate.Value.ToString(Constants.DateTime24HrFormat) : "",
                        IsSeaPassage = x.IsSeaPassage,
                        FromCountryCode = x.FromCountryCode,
                        ToCountryCode = x.ToCountryCode,
                        FromPortId = x.FromPortId,
                        NextPortId = x.NextPortId,
                        FromPortIsAlertAdded = x.FromPortIsAlertAdded,
                        NextPortIsAlertAdded = x.NextPortIsAlertAdded,
                        FromPortRequestURL = GetFromPortAlertUrl(x.FromPortId, vesselID),
                        TotalCount = response.TotalRecords,
                        PortFromDate = x.PortFromDate.HasValue ? x.PortFromDate.Value.ToString(Constants.DateTime24HrFormat) : "",
                        TotalPages = response.TotalPages,
                        ImoNumber = x.ImoNumber,
                        CanShowCertificates = x.CanShowCertificates,
                        CanShowCommercial = x.CanShowCommercial,
                        CanShowCrewing = x.CanShowCrewing,
                        CanShowDefects = x.CanShowDefects,
                        CanShowEnvironment = x.CanShowEnvironment,
                        CanShowFinancials = x.CanShowFinancials,
                        CanShowHazOcc = x.CanShowHazOcc,
                        CanShowInspectionsAndRatings = x.CanShowInspectionsAndRatings,
                        CanShowPMS = x.CanShowPMS,
                        CanShowProcurement = x.CanShowProcurement,
                        CanShowJSA = true,
                        VesselChiefEnggName = !string.IsNullOrWhiteSpace(x.VesselChiefEnggName) ? x.VesselChiefEnggName : "-",
                        VesselMasterName = !string.IsNullOrWhiteSpace(x.VesselMasterName) ? x.VesselMasterName : "-",
                        VesselType = !string.IsNullOrWhiteSpace(x.VesselType) ? x.VesselType : "-"
                    });
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Creates the longitude.
        /// </summary>
        /// <param name="LongDegree">The long degree.</param>
        /// <param name="LongMin">The long minimum.</param>
        /// <param name="LongIndicator">The long indicator.</param>
        /// <returns></returns>
        private string CreateLongitude(decimal? LongDegree, decimal? LongMin, string LongIndicator)
        {
            string longitude = string.Empty;
            if (LongDegree.HasValue)
            {
                longitude += LongDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                longitude += "°, ";
            }
            if (LongMin.HasValue)
            {
                longitude += LongMin.Value.ToString("0.00");
                longitude += "' ";
                longitude += LongIndicator;
            }
            return longitude;
        }

        /// <summary>
        /// Creates the latitude.
        /// </summary>
        /// <param name="LatDegree">The lat degree.</param>
        /// <param name="LatMin">The lat minimum.</param>
        /// <param name="LatIndicator">The lat indicator.</param>
        /// <returns></returns>
        private string CreateLatitude(decimal? LatDegree, decimal? LatMin, string LatIndicator)
        {
            string latitude = string.Empty;
            if (LatDegree.HasValue)
            {
                latitude += LatDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                latitude += "°, ";
            }
            if (LatMin.HasValue)
            {
                latitude += LatMin.Value.ToString("0.00");
                latitude += "' ";
                latitude += LatIndicator;
            }
            return latitude;
        }

        /// <summary>
        /// Gets the user detail.
        /// </summary>
        /// <returns></returns>
        public async Task<UserViewModel> GetUserDetail()
        {
            UserViewModel result = null;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/UserDetail"));
            UserDetail response = await GetAsync<UserDetail>(requestUrl);

            if (response != null)
            {
                result = new UserViewModel
                {
                    ClientId = response.CmpId,
                    UserDisplayName = response.UsrDisplayName,
                    UserForeName = response.UsrForename,
                    UserSurName = response.UsrSurname,
                    UserTitle = response.UsrTitle
                };

                if (response.Company != null)
                {
                    result.ClientName = response.Company.CmpName;
                    result.IsClientLogoAvailable = response.Company.IsLogoAvailable;
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the task messages.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskMessageDetailViewModel>> GetTaskMessages()
        {
            List<TaskMessageDetailViewModel> result = new List<TaskMessageDetailViewModel>();
            List<TaskMessageDetail> response = null;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/TaskMessage"));

            response = await PostAsyncAutoPaged<TaskMessageDetail>(requestUrl, null, 20);

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    result.Add(new TaskMessageDetailViewModel
                    {
                        MessageDate = x.MessageDate,
                        Success = x.Success,
                        MessageContent = x.MessageContent,
                        IsFile = IsFile(x),
                        DownloadDocumentUrl = _provider.CreateProtector("DownloadDocument").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(x))
                    });
                });
            }
            return result;
        }

        /// <summary>
        /// Determines whether the specified entity is file.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity is file; otherwise, <c>false</c>.
        /// </returns>
        private bool IsFile(TaskMessageDetail entity)
        {
            string fileName = !string.IsNullOrWhiteSpace(entity.GeneratedFilename) ? entity.GeneratedFilename : entity.MessageContent;
            if (!string.IsNullOrWhiteSpace(fileName)
                && !fileName.Contains("Scheduled Report")
                && !fileName.Contains("Cancel Order")
                && (fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Word))
                || fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSX))
                || fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSM))
                || fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.PDF))
                || fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Excel))
                || fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.CSV))
                || fileName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Text))
                || fileName.EndsWith(Constants.ZipExtension, System.StringComparison.OrdinalIgnoreCase))
                )

            {
                return true;
            }
            return false;
        }

        #region Image Paths

        /// <summary>
        /// Gets the certificate color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetCertificateColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/certificate-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/certificate-amber-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/certificate-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the commercial color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetCommercialColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/commercial-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/commercial-amber-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/commercial-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the crew color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetCrewColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/crew-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/crew-amber-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/crew-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the defect color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetDefectColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/defects-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/defects-amber-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/defects-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the drill and campaign color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetDrillAndCampaignColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/drill-red-vessel.png";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/drill-orange-vessel.png";
            }
            else
            {
                return "/images/DashboardIcons/drill-green-vessel.png";
            }
        }

        /// <summary>
        /// Gets the environment color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetEnvironmentColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/enviornment-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                //TODO: Navita will provide image
                //No image is available
                return "/images/DashboardIcons/environment-amber-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/enviornment-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the haz occ color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetHazOccColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/hazocc-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/hazzoc-orange-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/hazzoc-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the inspection color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetInspectionColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/inspection-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/inspection-orange-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/inspection-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the jsa and moc color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetJSAAndMOCColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/jsa-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/jsa-amber-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/jsa-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the PMS color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetPMSColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/pms-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/pms-orange-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/pms-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the purchase order color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetPurchaseOrderColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "/images/DashboardIcons/orders-red-vessel.svg";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "/images/DashboardIcons/orders-orange-vessel.svg";
            }
            else
            {
                return "/images/DashboardIcons/orders-green-vessel.svg";
            }
        }

        /// <summary>
        /// Gets the opex color image path.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private string GetOpexColorImagePath(int count)
        {
            if (EnumsHelper.GetKeyValue(KPIPriority.Red) == count.ToString())
            {
                return "opex-red-vessel";
            }
            else if (EnumsHelper.GetKeyValue(KPIPriority.Amber) == count.ToString())
            {
                return "opex-orange-vessel";
            }
            else
            {
                return "opex-green-vessel";
            }
        }

        #endregion

        #region Header URL

        /// <summary>
        /// Gets the defect manager URL.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        private string GetDefectManagerURL(string encryptedVesselId)
        {
            DefectListViewModel request = new DefectListViewModel();
            request.FromDate = null;
            request.ToDate = null;
            request.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.OpenDefect);
            string defectURL = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            var navigation = "/Defect/List/?DefectRequest=" + defectURL + "&VesselId=" + encryptedVesselId + "&IsViewMore=true";
            return navigation;
        }

        /// <summary>
        /// Sets up purchase order URL.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        private string SetUpPurchaseOrderUrl(string encryptedVesselId)
        {
            PurchaseOrderRequestViewModel poRequest = new PurchaseOrderRequestViewModel();
            poRequest.IsSearchClicked = false;
            poRequest.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.InProcess);
            string purchaseOrderURL = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(poRequest));
            var navigation = "/PurchaseOrder/List/?purchaseOrderRequest=" + purchaseOrderURL + "&VesselId=" + encryptedVesselId + "&IsViewMore=true";
            return navigation;
        }
        /// <summary>
        /// Crews the navigation.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        private string CrewNavigation(string encryptedVesselId)
        {
            CrewListViewModel viewMore = new CrewListViewModel();
            viewMore.ToDate = DateTime.Now;
            viewMore.FromDate = DateTime.Now;
            viewMore.SelectedFilter = CrewStageFilter.Onboard;
            viewMore.SelectedStageFilter = EnumsHelper.GetKeyValue(CrewStageFilter.Onboard);
            string crewRequestURL = _provider.CreateProtector("CrewList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(viewMore));
            var navigation = "/Crew/List/?CrewList=" + crewRequestURL + "&VesselId=" + encryptedVesselId + "&IsViewMore=true";
            return navigation;
        }

        /// <summary>
        /// Sets the inspection URL.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        private string SetInspectionURL(string encryptedVesselId, string vesselId)
        {
            InspectionRequestViewModel inspection = new InspectionRequestViewModel();
            inspection.InspectionType = InspectionDashboardType.AllInspection;
            inspection.FromDate = DateTime.Now.AddMonths(-6);
            inspection.ToDate = DateTime.Now;
            inspection.VesselId = vesselId;
            inspection.IsSummaryClicked = true;
            string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(inspection));
            var navigation = "/Inspection/List/?Inspection=" + inspectionURL + "&VesselId=" + encryptedVesselId + "&IsViewMore=true";
            return navigation;
        }

        /// <summary>
        /// Sets the certificate URL.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        private string SetCertificateURL(string vesselId)
        {
            CertificateRequestViewModel input = new CertificateRequestViewModel();
            input.StageName = EnumsHelper.GetKeyValue(VesselCertificates.TotalActive);
            string certificateURL = _provider.CreateProtector("CertificateURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            var navigation = "/Certificate/List?CertificateRequestUrl=" + certificateURL + "&VesselId=" + vesselId + "&IsViewMore=true";
            return navigation;
        }

        /// <summary>
        /// Sets the opex URL.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="coyId">The coy identifier.</param>
        /// <returns></returns>
        private string SetOpexURL(string vesselId, string coyId)
        {
            OperatingCostBarChartRequest request = new OperatingCostBarChartRequest();
            request.AccountId = null;
            request.AccountLevel = -1;
            request.CoyId = coyId;
            request.ReportDefinitionType = ReportDefinitionType.S;

            var CurrentDate = DateTime.Now;
            var previousDate = CurrentDate.AddDays(-1);
            var nextMonth = previousDate.AddMonths(1);
            var NextMonthDate = new DateTime(nextMonth.Year, nextMonth.Month, 1);
            var requiredDate = NextMonthDate.AddDays(-1);
            request.ToDate = requiredDate;
            string operationCostURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            var navigation = "/Finance/List/?OperationCostRequestUrl=" + operationCostURL + "&VesselId=" + vesselId + "&IsViewMore=true";
            return navigation;
        }

        /// <summary>
        /// Gets the commercial URL.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        private string GetCommercialURL(string encryptedVesselId, string vesselId)
        {
            VoyageReportingRequestViewModel voyageReportingRequest = new VoyageReportingRequestViewModel();
            voyageReportingRequest.PositionListId = "";
            voyageReportingRequest.VesselId = vesselId;
            voyageReportingRequest.MenuType = UserMenuItemType.Vessel;
            voyageReportingRequest.FromDate = DateTime.Now.Date;
            voyageReportingRequest.ToDate = DateTime.Now.Date;
            string RequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequest));
            string Navigation = "/VoyageReporting/VesselActivityList/?VoyageReportingRequestUrl=" + RequestURL + "&VesselId=" + encryptedVesselId + "&isFromDashboard=" + true;
            return Navigation;
        }

        /// <summary>
        /// Gets from port alert URL.
        /// </summary>
        /// <param name="fromPortId">From port identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        private string GetFromPortAlertUrl(string fromPortId, string vesselId)
        {
            VoyageReportingRequestViewModel voyageFromReportingRequest = new VoyageReportingRequestViewModel();
            voyageFromReportingRequest.VesselId = vesselId;//decreptedString.Split(Constants.Separator)[0];
            voyageFromReportingRequest.MenuType = UserMenuItemType.Vessel;
            voyageFromReportingRequest.PortId = fromPortId;

            return _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageFromReportingRequest));
        }

        #endregion

        #region Dashboard - Fleet Summary
        /// <summary>
        /// Gets the fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<FleetSummaryResponseViewModel> GetFleetSummary(FleetSummaryRequestViewModel input)
        {
            FleetSummaryRequest request = new FleetSummaryRequest();
            FleetSummaryResponse response = new FleetSummaryResponse();
            FleetSummaryResponseViewModel fleetSummary = new FleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            request.PSCDetentionFromDate = input.PSCDetentionFromDate;
            request.PSCDetentionToDate = input.PSCDetentionToDate;
            request.PSCDetentionPriorityLimit = input.PSCDetentionPriorityLimit;

            request.PSCDeficiencyFromDate = input.PSCDeficiencyFromDate;
            request.PSCDeficiencyToDate = input.PSCDeficiencyToDate;
            request.PSCDeficiencyPriorityLimit = input.PSCDeficiencyPriorityLimit;

            request.OMVFindingsFromdate = input.OMVFindingsFromdate;
            request.OMVFindingsToDate = input.OMVFindingsToDate;
            request.OMVFindingsPriorityLowLimit = input.OMVFindingsPriorityLowLimit;
            request.OMVFindingsPriorityHighLimit = input.OMVFindingsPriorityHighLimit;

            request.OverdueInspectionsPriorityLimit = input.OverdueInspectionsPriorityLimit;

            request.IncidentStartDate = input.IncidentStartDate;
            request.IncidentEndDate = input.IncidentEndDate;
            request.SeriousIncidentsPriority = input.SeriousIncidentsPriority;

            request.CriticalPmspriority = input.CriticalPmspriority;

            request.LtiFromDate = input.LtiFromDate;
            request.LtiToDate = input.LtiToDate;
            request.LtifPriority = input.LtifPriority;

            request.OffHireStartDate = input.OffHireStartDate;
            request.OffHireEndDate = input.OffHireEndDate;
            request.OffHirePriority = input.OffHirePriority;

            request.RightShipPriority = input.RightShipPriority;

            request.OilSpillFromDate = input.OilSpillFromDate;
            request.OilSpillToDate = input.OilSpillToDate;
            request.OilSpillPriorityLimit = input.OilSpillPriorityLimit;

            request.OpexToDate = input.OpexToDate;
            request.BudgetDays = input.BudgetDays;
            request.BudgetPercentageHighLimit = input.BudgetPercentageHighLimit;
            request.BudgetPercentageLowLimit = input.BudgetPercentageLowLimit;

            request.FuelEfficiencyFromDate = input.FuelEfficiencyFromDate;
            request.FuelEfficiencyToDate = input.FuelEfficiencyToDate;
            request.FuelEfficiencyPriorityHighLimit = input.FuelEfficiencyPriorityHighLimit;
            request.FuelEfficiencyPriorityLowLimit = input.FuelEfficiencyPriorityLowLimit;
            request.VesselId = GetVesselId(input.VesselId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/FleetSummary"));
            response = await PostAsync<FleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.PSCDetentionCount = response.PSCDetentionCount;
                fleetSummary.PSCDetentionPriority = response.PSCDetentionPriority;

                fleetSummary.PSCDeficiencyCount = response.PSCDeficiencyCount;
                fleetSummary.PscDeficiencyRate = string.Format(Constants.TwoDecimal_NumberFormat, response.PscDeficiencyRate);
                fleetSummary.PscDeficiencyInspectionCount = response.PscDeficiencyInspectionCount;
                fleetSummary.PSCDeficiencyPriority = response.PSCDeficiencyPriority;

                fleetSummary.OMVFindingsRate = string.Format(Constants.TwoDecimal_NumberFormat, response.OMVFindingsRate);
                fleetSummary.OMVFindingsPriority = response.OMVFindingsPriority;
                fleetSummary.OMVFindingsCount = response.OMVFindingsCount;
                fleetSummary.OMVInspectionsCount = response.OMVInspectionsCount;

                fleetSummary.OverdueInspectionCount = response.OverdueInspectionCount;
                fleetSummary.OverdueInspectionPriority = response.OverdueInspectionPriority;

                fleetSummary.CriticalPMSCount = response.CriticalPMSCount;
                fleetSummary.CriticalPMSPriority = response.CriticalPMSPriority;

                fleetSummary.SeriousIncidentsCount = response.SeriousIncidentsCount;
                fleetSummary.SeriousIncidentsPriority = response.SeriousIncidentsPriority;

                fleetSummary.LtifCount = response.LtifCount;
                fleetSummary.LtifPriority = response.LtifPriority;

                fleetSummary.OffHireData = response.OffHireData;
                fleetSummary.OffHirePriority = response.OffHirePriority;

                fleetSummary.RightShipRate = string.Format(Constants.TwoDecimal_NumberFormat, response.RightShipRate);
                fleetSummary.RightShipPriority = response.RightShipPriority;

                fleetSummary.OilSpillCount = response.OilSpillCount;
                fleetSummary.OilSpillPriority = response.OilSpillPriority;

                fleetSummary.OpexOverBudgetPercentage = response.OpexOverBudgetPercentage == null
                                                        ? Constants.NotApplicable
                                                        : string.Format(Constants.TwoDecimal_NumberFormat, response.OpexOverBudgetPercentage) + "%";
                fleetSummary.OpexOverBudgetPriority = response.OpexOverBudgetPriority;
                fleetSummary.OpexOverBudgetToDate = string.Format("{0:MMM}-{0:yyyy}", request.OpexToDate);

                fleetSummary.ExperienceMatrixVesselCount = response.ExperienceMatrixVesselCount;
                fleetSummary.ExperienceMatrixPriority = response.ExperienceMatrixPriority;

                fleetSummary.FuelEfficiencyPercentage = response.FuelEfficiencyPercentage == null
                                                        ? Constants.NotApplicable
                                                        : string.Format(Constants.TwoDecimal_NumberFormat, response.FuelEfficiencyPercentage) + "%";
                fleetSummary.FuelEfficiencyPriority = response.FuelEfficiencyPriority;
                fleetSummary.FuelEfficiencyPercentageValue = response.FuelEfficiencyPercentage.GetValueOrDefault();
            }
            return fleetSummary;
        }

        /// <summary>
        /// Gets the fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<PerformanceSummaryResponseViewModel>> GetPerformanceSummary(PerformanceSummaryRequestViewModel input)
        {
            PerformanceSummaryRequest request = new PerformanceSummaryRequest();
            List<PerformanceSummaryResponseViewModel> responseSummaryList = new List<PerformanceSummaryResponseViewModel>();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
            string vesselId = decreptedString.Split(Constants.Separator)[0];

            request.VesselId = vesselId;
            request.StartDate = input.StartDate;
            request.EndDate = input.EndDate;
            request.PositionId = input.PosId;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/PerformanceSummary"));
            List<PerformanceSummaryResponse> response = await PostAsync<List<PerformanceSummaryResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    PerformanceSummaryResponseViewModel responseSummary = new PerformanceSummaryResponseViewModel();
                    responseSummary.Last24HourSpeed = item.Last24HourSpeed.HasValue ? item.Last24HourSpeed.ToString() : string.Empty;
                    responseSummary.Last24HourSpeedPriority = item.Last24HourSpeedPriority;
                    responseSummary.Last24HourSpeedBackgroundPriority = item.Last24HourSpeedBackgroundPriority;

                    responseSummary.Last24HourConsumption = item.Last24HourConsumption.HasValue ? item.Last24HourConsumption.ToString() : string.Empty;
                    responseSummary.Last24HourConsumptionPriority = item.Last24HourConsumptionPriority;
                    responseSummary.Last24HourConsumptionBackgroundPriority = item.Last24HourConsumptionBackgroundPriority;

                    responseSummary.VoyageAverageSpeed = item.VoyageAverageSpeed.HasValue ? item.VoyageAverageSpeed.ToString() : string.Empty;
                    responseSummary.VoyageAverageSpeedPriority = item.VoyageAverageSpeedPriority;
                    responseSummary.VoyageAverageSpeedBackgroundPriority = item.VoyageAverageSpeedBackgroundPriority;

                    responseSummary.VoyageAverageConsumption = item.VoyageAverageConsumption.HasValue ? item.VoyageAverageConsumption.ToString() : string.Empty;
                    responseSummary.VoyageAverageConsumptionPriority = item.VoyageAverageConsumptionPriority;
                    responseSummary.VoyageAverageConsumptionBackgroundPriority = item.VoyageAverageConsumptionBackgroundPriority;

                    responseSummary.CPAdjustedSpeed = item.CPAdjustedSpeed.HasValue ? item.CPAdjustedSpeed.ToString() : string.Empty;
                    responseSummary.CPAdjustedSpeedPriority = item.CPAdjustedSpeedPriority;
                    responseSummary.CPAdjustedSpeedBackgroundPriority = item.CPAdjustedSpeedBackgroundPriority;

                    responseSummary.CPAdjustedConsumption = item.CPAdjustedConsumption.HasValue ? item.CPAdjustedConsumption.ToString() : string.Empty;
                    responseSummary.CPAdjustedConsumptionPriority = item.CPAdjustedConsumptionPriority;
                    responseSummary.CPAdjustedConsumptionBackgroundPriority = item.CPAdjustedConsumptionBackgroundPriority;

                    responseSummary.CPOrdersSpeed = item.CPOrdersSpeed.HasValue ? item.CPOrdersSpeed.ToString() : string.Empty;
                    responseSummary.CPOrdersSpeedPriority = item.CPOrdersSpeedPriority;
                    responseSummary.CPOrdersSpeedBackgroundPriority = item.CPOrdersSpeedBackgroundPriority;

                    responseSummary.CPOrdersConsumption = item.CPOrdersConsumption.HasValue ? item.CPOrdersConsumption.ToString() : string.Empty;
                    responseSummary.CPOrdersConsumptionPriority = item.CPOrdersConsumptionPriority;
                    responseSummary.CPOrdersConsumptionBackgroundPriority = item.CPOrdersConsumptionBackgroundPriority;
                    responseSummary.RankId = item.RankId;
                    responseSummary.PosId = item.PosId;
                    responseSummaryList.Add(responseSummary);
                }
            }
            return responseSummaryList;
        }

        /// <summary>
        /// Gets the vessel identifier.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        private string GetVesselId(string encryptedVesselDetail)
        {
            if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
            {
                string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
                return decryptedString.Split(Constants.Separator)[0];
            }
            return null;
        }

        /// <summary>
        /// Posts the get user fleets.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<FleetDetailViewModel>> PostGetUserFleets(UserFleetsRequestViewModel input)
        {
            List<FleetDetailViewModel> result = new List<FleetDetailViewModel>();
            var request = new Dictionary<string, object>()
            {
                { "fleetName", input.FleetName },
                { "userId", input.UserId },
                { "isActive", input.IsActive }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/UserFleets"));
            List<FleetDetail> response = await PostAsync<List<FleetDetail>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    FleetDetailViewModel objFleet = new FleetDetailViewModel();
                    objFleet.FleetId = item.FleetIdentifier;
                    objFleet.FleetName = item.FleetName;
                    objFleet.IsActive = item.IsActive ? Constants.Yes : Constants.No;
                    objFleet.IsFleetActive = item.IsActive;
                    result.Add(objFleet);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get fleet detail by identifier.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<FleetDetailViewModel> PostGetFleetDetailById(string fleetId)
        {
            FleetDetailViewModel result = new FleetDetailViewModel();

            string urlRequest = "fleetId=" + fleetId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/FleetDetailById"), urlRequest);
            FleetDetail response = await PostAsync<FleetDetail>(requestUrl, CreateHttpContent(fleetId));

            if (response != null)
            {
                result.FleetId = response.FleetIdentifier;
                result.FleetName = response.FleetName;
                result.IsActive = response.IsActive ? Constants.Yes : Constants.No;
                result.IsFleetActive = response.IsActive;
            }
            return result;
        }

        /// <summary>
        /// Posts the get mapped vessel for user fleet.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<VesselManagementDetailForUserFleetViewModel>>> PostGetMappedVesselForUserFleet(DataTablePageRequest<string> pageRequest, string fleetId)
        {
            int counter = 1;
            DataTablePageResponse<List<VesselManagementDetailForUserFleetViewModel>> result = new DataTablePageResponse<List<VesselManagementDetailForUserFleetViewModel>>();
            result.Data = new List<VesselManagementDetailForUserFleetViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<VesselManagementDetailForUserFleet>> response = null;
            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "fleetId", fleetId }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/MappedVesselForUserFleet"));
            response = await PostAsync<PagedResponse<List<VesselManagementDetailForUserFleet>>>(requestUrl, CreateHttpContent(value));

            if (response.Result != null)
            {
                foreach (var item in response.Result)
                {
                    VesselManagementDetailForUserFleetViewModel vesselManage = new VesselManagementDetailForUserFleetViewModel();
                    vesselManage.FleetVesselId = item.FleetVesselId;
                    vesselManage.VesselId = item.VesselId;
                    vesselManage.VesselManagementId = item.VesselManagementId;
                    vesselManage.RowId = "rowNo" + counter++;
                    vesselManage.VesselName = item.VesselName;
                    result.Data.Add(vesselManage);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Posts the get mapped vessel for user fleet.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselManagementDetailForUserFleetViewModel>> PostGetMappedVesselForUserFleet(string fleetId)
        {
            List<VesselManagementDetailForUserFleetViewModel> result = new List<VesselManagementDetailForUserFleetViewModel>();
            var value = new Dictionary<string, object>()
            {
                { "fleetId", fleetId }
            };
            int counter = 1;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/MappedVesselForUserFleet"));
            List<VesselManagementDetailForUserFleet> response = await PostAsyncAutoPaged<VesselManagementDetailForUserFleet>(requestUrl, value, 500);

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    VesselManagementDetailForUserFleetViewModel vesselManage = new VesselManagementDetailForUserFleetViewModel();
                    vesselManage.FleetVesselId = item.FleetVesselId;
                    vesselManage.VesselId = item.VesselId;
                    vesselManage.VesselManagementId = item.VesselManagementId;
                    vesselManage.RowId = "rowNo" + counter++;
                    vesselManage.VesselName = item.VesselName;
                    result.Add(vesselManage);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get get un mapped vessel for user fleet.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselManagementDetailForUserFleetViewModel>> PostGetGetUnMappedVesselForUserFleet(string fleetId)
        {
            List<VesselManagementDetailForUserFleet> response = null;
            List<VesselManagementDetailForUserFleetViewModel> result = new List<VesselManagementDetailForUserFleetViewModel>();
            var value = new Dictionary<string, object>()
            {
                { "fleetId", fleetId }
            };
            int counter = 1;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetUnMappedVesselForUserFleet"));
            response = await PostAsyncAutoPaged<VesselManagementDetailForUserFleet>(requestUrl, value, 500);
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    VesselManagementDetailForUserFleetViewModel vesselManage = new VesselManagementDetailForUserFleetViewModel();
                    vesselManage.FleetVesselId = item.FleetVesselId;
                    vesselManage.VesselId = item.VesselId;
                    vesselManage.VesselManagementId = item.VesselManagementId;
                    vesselManage.RowId = "rowNo" + counter++;
                    vesselManage.VesselName = item.VesselName;
                    result.Add(vesselManage);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets user management vessel.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselManagementDetailForUserFleetViewModel>> GetUserManagementVessel(string fleetId)
        {
            List<VesselManagementDetailForUserFleet> response = null;
            List<VesselManagementDetailForUserFleetViewModel> result = new List<VesselManagementDetailForUserFleetViewModel>();
            var value = new Dictionary<string, object>()
            {
                { "fleetId", fleetId }
            };

            int counter = 1;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetUserManagementVessel"));
            response = await PostAsyncAutoPaged<VesselManagementDetailForUserFleet>(requestUrl, value, 500);
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    VesselManagementDetailForUserFleetViewModel vesselManage = new VesselManagementDetailForUserFleetViewModel();
                    vesselManage.FleetVesselId = item.FleetVesselId;
                    vesselManage.VesselId = item.VesselId;
                    vesselManage.VesselManagementId = item.VesselManagementId;
                    vesselManage.RowId = "rowNo" + counter++;
                    vesselManage.VesselName = item.VesselName;
                    result.Add(vesselManage);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the check can create user fleet.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<bool> PostCheckCanCreateUserFleet(FleetDetailViewModel input)
        {
            bool response = false;
            if (input != null)
            {
                FleetDetail objFleet = new FleetDetail();
                objFleet.FleetIdentifier = input.FleetId;
                objFleet.FleetName = input.FleetName;
                objFleet.IsActive = input.IsFleetActive;
                objFleet.UserId = input.UserId;
                objFleet.FleetType = FleetType.User;

                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CheckCanCreateUserFleet"));
                response = await PostAsync<bool>(requestUrl, CreateHttpContent(objFleet));
            }
            return response;
        }

        /// <summary>
        /// Posts the check can create user fleet pwa.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<CheckUserFleetResponse> PostCheckCanCreateUserFleetPWA(FleetDetailViewModel input)
        {
            CheckUserFleetResponse response = new CheckUserFleetResponse();
            if (input != null)
            {
                FleetDetail objFleet = new FleetDetail();
                objFleet.FleetIdentifier = input.FleetId;
                objFleet.FleetName = input.FleetName;
                objFleet.IsActive = input.IsFleetActive;
                objFleet.UserId = input.UserId;
                objFleet.FleetType = FleetType.User;

                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/CheckCanCreateUserFleetPWA"));
                response = await PostAsync<CheckUserFleetResponse>(requestUrl, CreateHttpContent(objFleet));
            }
            return response;
        }

        /// <summary>
        /// Posts the save user fleet.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="vesselManagementIds">The vessel management ids.</param>
        /// <returns></returns>
        public async Task<bool> PostSaveUserFleet(FleetDetailViewModel input, List<string> vesselManagementIds)
        {
            bool response = false;
            if (input != null)
            {
                FleetDetail objFleet = new FleetDetail();
                objFleet.FleetIdentifier = input.FleetId;
                objFleet.FleetName = input.FleetName;
                objFleet.IsActive = input.IsFleetActive;
                objFleet.UserId = input.UserId;
                objFleet.FleetType = FleetType.User;

                var request = new Dictionary<string, object>()
                {
                    { "fleetDetail", objFleet},
                    { "vesselManagementIds", vesselManagementIds },
                };

                var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/SaveUserFleet"));
                response = await PostAsync<bool>(requestUrl, CreateHttpContent(request));
            }
            return response;
        }

        #endregion

        /// <summary>
        /// Gets the participants.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="userSearchName">Name of the user search.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="userId">The Logged In User identifier.</param>
        /// <param name="tempSelectedUsers">The Temporary Selected Users List.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<ParticipantsDetailsViewModel>>> GetParticipants(DataTablePageRequest<string> pageRequest, string userSearchName, string vesselId, string userId, List<string> tempSelectedUsers)
        {
            DataTablePageResponse<List<ParticipantsDetailsViewModel>> result = new DataTablePageResponse<List<ParticipantsDetailsViewModel>>();
            result.Data = new List<ParticipantsDetailsViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            PagedResponse<List<UserSearchResponse>> response = null;

            UserSearchRequest input = new UserSearchRequest();
            input.UserNameSearch = userSearchName;
            //input.VesselId = vesselId;

            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "searchRequest", input }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetUserForParticipant"));
            response = await PostAsync<PagedResponse<List<UserSearchResponse>>>(requestUrl, CreateHttpContent(value));

            if (response != null && response.Result != null && response.Result.Any())
            {
                foreach (UserSearchResponse item in response.Result.Where(x => x.UserId != userId && !(tempSelectedUsers.Contains(x.UserId))).ToList())
                {
                    ParticipantsDetailsViewModel subscriber = new ParticipantsDetailsViewModel();
                    subscriber.Id = item.UserId;
                    string FullName = item.ForeName + " " + item.SurName;
                    subscriber.Text = FullName;
                    subscriber.UserShortName = GetUserShortName(FullName);
                    subscriber.Description = item.RoleName ?? string.Empty;
                    result.Data.Add(subscriber);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;
            return result;
        }

        /// <summary>
        /// Gets the short name of the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private string GetUserShortName(string userName)
        {
            string result = "";
            if (!string.IsNullOrWhiteSpace(userName))
            {
                userName = userName.Trim();
                var splitName = userName.Split(" ");
                result += splitName[0].Substring(0, 1);
                result += splitName[1].Substring(0, 1);
            }
            return result;
        }

        /// <summary>
        /// Gets the user preferences.
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserPreferenceDetailViewModel>> GetUserPreferences()
        {
            List<UserPreferenceDetail> response = new List<UserPreferenceDetail>();
            List<UserPreferenceDetailViewModel> result = new List<UserPreferenceDetailViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/UserPreferences"));
            response = await GetAsync<List<UserPreferenceDetail>>(requestUrl);
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    result.Add(new UserPreferenceDetailViewModel
                    {
                        IsPreferred = item.IsPreferred,
                        MappingId = item.MappingId,
                        PreferenceId = item.PreferenceId,
                        PreferenceKey = item.PreferenceKey
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Saves the user preference.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<bool> SaveUserPreference(List<UserPreferenceDetailViewModel> input)
        {
            bool response = false;
            List<UserPreferenceDetail> request = new List<UserPreferenceDetail>();
            foreach (var item in input)
            {
                request.Add(new UserPreferenceDetail
                {
                    IsPreferred = item.IsPreferred,
                    MappingId = item.MappingId,
                    PreferenceId = item.PreferenceId,
                    PreferenceKey = item.PreferenceKey,
                });
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/SaveUserPreferences"));
            response = await PostAsync<bool>(requestUrl, CreateHttpContent(request));

            return response;
        }

        /// <summary>
        /// Gets the name of the users primary role.
        /// </summary>
        /// <param name="userIds">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<UserRoleDetailViewModel>> GetUsersPrimaryRoleName(List<string> userIds)
        {
            List<UserRoleDetailViewModel> UserList = new List<UserRoleDetailViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetUsersPrimaryRoleName"));
            var response = await PostAsync<List<UserRoleDetail>>(requestUrl, CreateHttpContent(userIds));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    UserRoleDetailViewModel userRole = new UserRoleDetailViewModel();
                    userRole.RoleId = item.RoleId;
                    userRole.RoleName = item.RoleName;
                    userRole.UserId = item.UserId;
                    UserList.Add(userRole);
                }

            }

            return UserList;
        }

        /// <summary>
        /// Gets the name of the vessels.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselInfoViewModel>> GetVesselsName(List<string> vesselId)
        {
            List<VesselInfoViewModel> VesselList = new List<VesselInfoViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetVesselsName"));
            var response = await PostAsync<List<VesselInfo>>(requestUrl, CreateHttpContent(vesselId));

            if (response != null && response.Any())
            {
                foreach (VesselInfo item in response)
                {
                    VesselInfoViewModel vessel = new VesselInfoViewModel();
                    vessel.VesselId = item.VesselId;
                    vessel.VesselName = item.VesselName ?? string.Empty;
                    vessel.IMONumber = item.IMOnumber ?? string.Empty;
                    vessel.CoyId = item.CoyId ?? string.Empty;
                    VesselList.Add(vessel);
                }
            }

            return VesselList;
        }

        /// <summary>
        /// Gets the vessel responsibilities.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="userId">The Logged In User identifier.</param>
        /// <returns></returns>
        public async Task<List<ParticipantsDetailsViewModel>> GetVesselResponsibilities(string vesselId, string userId)
        {
            List<UserSearchResponse> response = null;
            List<ParticipantsDetailsViewModel> participants = new List<ParticipantsDetailsViewModel>();
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/VesselResponisibilities_PWA/" + vesselId));
            response = await GetAsync<List<UserSearchResponse>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (UserSearchResponse item in response.Where(x => x.UserId != userId).ToList())
                {
                    ParticipantsDetailsViewModel user = new ParticipantsDetailsViewModel();
                    user.Id = item.UserId;
                    string FullName = item.ForeName + " " + item.SurName;
                    user.Text = FullName;
                    user.UserShortName = GetUserShortName(FullName);
                    user.Description = item.RoleName ?? string.Empty;
                    participants.Add(user);
                }
            }

            return participants;
        }

        /// <summary>
        /// Gets the supplier rating breakdown.
        /// </summary>
        /// <param name="SupplierCompanyId">The supplier company identifier.</param>
        /// <returns></returns>
        public async Task<SupplierRatingBreakdownViewModel> GetSupplierRatingBreakdown(string SupplierCompanyId)
        {
            SupplierRatingBreakdownViewModel viewModel = new SupplierRatingBreakdownViewModel();

            SupplierRatingBreakdown RatingsBreakdown = null;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetSupplierRatingBreakdown/" + SupplierCompanyId));
            RatingsBreakdown = await GetAsync<SupplierRatingBreakdown>(requestUrl);
            if (RatingsBreakdown != null)
            {
                double scoretotal = RatingsBreakdown.OneStar + (RatingsBreakdown.TwoStar * 2) + (RatingsBreakdown.ThreeStar * 3) + (RatingsBreakdown.FourStar * 4);

                double responsetotal = RatingsBreakdown.OneStar + RatingsBreakdown.TwoStar + RatingsBreakdown.ThreeStar + RatingsBreakdown.FourStar;

                viewModel.AverageRating = responsetotal > 0 ? Math.Round(scoretotal / responsetotal, 1) : 0;
                viewModel.OneStar = RatingsBreakdown.OneStar;
                viewModel.TwoStar = RatingsBreakdown.TwoStar;
                viewModel.ThreeStar = RatingsBreakdown.ThreeStar;
                viewModel.FourStar = RatingsBreakdown.FourStar;

                viewModel.OneStarPercent = RatingsBreakdown.TotalOrders > 0 ? (RatingsBreakdown.OneStar * 100) / RatingsBreakdown.TotalOrders : 0;
                viewModel.TwoStarPercent = RatingsBreakdown.TotalOrders > 0 ? (RatingsBreakdown.TwoStar * 100) / RatingsBreakdown.TotalOrders : 0;
                viewModel.ThreeStarPercent = RatingsBreakdown.TotalOrders > 0 ? (RatingsBreakdown.ThreeStar * 100) / RatingsBreakdown.TotalOrders : 0;
                viewModel.FourStarPercent = RatingsBreakdown.TotalOrders > 0 ? (RatingsBreakdown.FourStar * 100) / RatingsBreakdown.TotalOrders : 0;

                viewModel.TotalOrders = RatingsBreakdown.TotalOrders;
            }

            return viewModel;
        }

        /// <summary>
        /// Saves the error log.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<bool> SaveErrorLog(ErrorLogRequest request)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/SaveErrorLogPWA"));
            var response = await PostAsync<bool>(requestUrl, CreateHttpContent(request));
            return response;
        }

        #region Approval

        /// <summary>
        /// The Get Approval Summary method
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ApprovalSummaryResponseViewModel>> GetApprovalSummary(ApprovalSummaryRequestViewModel request)
        {
            List<ApprovalSummaryResponse> response = new List<ApprovalSummaryResponse>();
            List<ApprovalSummaryResponseViewModel> approvalSummary = new List<ApprovalSummaryResponseViewModel>();
            ApprovalSummaryRequest input = new ApprovalSummaryRequest();
            input.FleetId = request.FleetId;
            input.MenuType = request.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, request.VesselId);
            input.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/ApprovalSummary"));
            response = await PostAsync<List<ApprovalSummaryResponse>>(requestUrl, CreateHttpContent(input));
            if (response != null && response.Any())
            {
                var headerNodes = response.Select(x => x.HeaderNodeShortCode).Distinct();
                DashboardParameter parameter = new DashboardParameter()
                {
                    FleetId = request.FleetId,
                    MenuType = request.MenuType,
                    VesselId = request.VesselId,
                    Title = request.Title
                };

                foreach (var header in headerNodes)
                {
                    ApprovalSummaryResponse responseObject = response.FirstOrDefault(x => x.HeaderNodeShortCode.Equals(header));
                    ApprovalSummaryResponseViewModel approvalVM = TransformToViewModel(responseObject, parameter);
                    approvalVM.Children = response.Where(x => x.HeaderNodeShortCode.Equals(header)).Select(x => TransformToViewModel(x, parameter)).ToList();
                    approvalSummary.Add(approvalVM);
                }
            }
            return approvalSummary;
        }

        /// <summary>
        /// Transforms to view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private ApprovalSummaryResponseViewModel TransformToViewModel(ApprovalSummaryResponse item, DashboardParameter parameter)
        {
            ApprovalListViewModel approvalListObject = new ApprovalListViewModel
            {
                HeaderNodeShortCode = item.HeaderNodeShortCode,
                NodeShortCode = item.NodeShortCode,
                DashboardParameter = parameter
            };


            ApprovalSummaryResponseViewModel approvalVM = new ApprovalSummaryResponseViewModel();
            approvalVM.HeaderNodeShortCode = item.HeaderNodeShortCode;
            approvalVM.HeaderNode = item.HeaderNode;
            approvalVM.NodeShortCode = item.NodeShortCode;
            approvalVM.Node = item.Node;
            approvalVM.Count = item.Count;
            approvalListObject.ActiveMobileTabClass = Constants.Tab1;
            approvalVM.ApprovalOverViewUrl = CommonUtil.GetEncryptedURL(_provider, Constants.ApprovalEncryptionText, approvalListObject);
            approvalListObject.ActiveMobileTabClass = Constants.Tab2;
            approvalVM.ApprovalListUrl = CommonUtil.GetEncryptedURL(_provider, Constants.ApprovalEncryptionText, approvalListObject);
            approvalVM.Children = new List<ApprovalSummaryResponseViewModel>();
            return approvalVM;
        }

        #endregion

        /// <summary>
        /// Get Vessel Communications
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselCommunicationDetailViewModel>> GetVesselCommunications(string vesselId, string commTypeCategoryId)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            string VesselId = decreptedString.Split(Constants.Separator)[0];

            var value = new Dictionary<string, object>()
            {
                { "vesselId", VesselId },
                { "typeCatId", commTypeCategoryId }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Shared/GetVesselCommunicationsByCategory"));
            List<VesselCommunicationDetail> result = await PostAsync<List<VesselCommunicationDetail>>(requestUrl, CreateHttpContent(value));

            List<VesselCommunicationDetailViewModel> response = new List<VesselCommunicationDetailViewModel>();

            if (result != null && result.Any())
            {
                foreach (VesselCommunicationDetail item in result)
                {
                    response.Add(new VesselCommunicationDetailViewModel()
                    {
                        ComId = item.ComId,
                        ComDeleted = item.ComDeleted,
                        ComDesc = !string.IsNullOrWhiteSpace(item.ComDesc) ? item.ComDesc : "",
                        ComExpiryDate = item.ComExpiryDate,
                        ComNumber = item.ComNumber,
                        ComPrimaryContact = item.ComPrimaryContact,
                        PrimaryContact = item.ComPrimaryContact.GetValueOrDefault() ? "Primary" : "",
                        ComProvider = item.ComProvider,
                        ComStartDate = item.ComStartDate,
                        CtyId = item.CtyId,
                        VesId = item.VesId,
                        CmpName = !string.IsNullOrWhiteSpace(item.CmpName) ? item.CmpName : "",
                        CtyName = !string.IsNullOrWhiteSpace(item.CtyName) ? item.CtyName : "",
                        IsEmail = !string.IsNullOrWhiteSpace(item.ComNumber) && item.ComNumber.Contains("@"),
                    });
                }
            }
            return response.OrderByDescending(x => x.PrimaryContact).ToList();
        }
    }

}