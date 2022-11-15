namespace PWAFeaturesRnd.Common
{
    public class Constants
    {
        public const string UserIDClaimType = "http://VShip/ShipSure/UserID";
        public const string UserDisplayNameClaimType = "http://VShip/ShipSure/UserDisplayName";
        public const string SiteIDClaimType = "http://VShip/ShipSure/SiteID";
        public const string DepartmentIDClaimType = "http://VShip/ShipSure/DepartmentID";
        public const string RoleClaimType = "http://VShip/ShipSure/ShipSureRole";
        public const string ClientIdClaimType = "http://VShip/ShipSure/ClientID";
        public const string UserEmailClaimType = "http://VShip/ShipSure/UserEmail";
        public const string UserSiteTypeClaimType = "http://VShip/ShipSure/SiteType";
        public const string UserFleetIdClaimType = "http://VShip/ShipSure/FleetId";
        public const string UserCompanyClaimType = "http://VShip/ShipSure/Company";
        public const string UserLinkIdClaimType = "http://VShip/ShipSure/LinkId";
        public const string UserLinkIdTypeClaimType = "http://VShip/ShipSure/LinkIdType";
        public const string ApiUserIdClaimType = "http://VShip/ShipSure/ApiUserId";
        public const string ExternalUserIdClaimType = "http://VShip/ShipSure/ExternalUserId";
        public const string UserTypeClaimType = "http://VShip/ShipSure/UserType";

        public const string UserNameSessionKey = "UserName";
        public const string UserClientNameSessionKey = "UserClientName";
        public const string AccessModuleSessionKey = "AccessModule";
        public const string UserIdKey = "UserId";

        public const string TwoDecimal_NumberFormat = "{0:N2}";
        public const string OneDecimal_NumberFormat = "{0:N1}";
        public const string FourDecimal_NumberFormat = "{0:N4}";
        public const string TimeFormat = "{0:0#}";
        public const string DateFormat = "dd MMM yyyy";
        public const string DateTime24HrFormat = "dd MMM yyyy HH:mm";
        public const string DayDateFormat = "dddd, MMMM dd, yyyy";
        public const string ShortDateTimeFormat = "dd/M/yyyy";
        public const string NotificationChatDateTimeFormat = "dd MMM yyyy, HH:mm";
        public const string FullDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DateTimeFormat = "d MMM HH:mm";
        public const string LogFileDateTimeFormat = "dd.MM.yyyy HH.MM.ss.fffff";
        public const string LogFileStartEndTimeFormat = "hh.mm.sss.fffffff";

        public const string Separator = "¥";

        public const double SupplierBaseTotalMargin = 0.05d;
        public const int CertificateDueNowRange = 29;
        public const string OperationCostDrillDownDelimiter = "¬";
        public const string SeaPassageLoaded = "SEA PASSAGE LOADED";
        public const string SeaPassageBallast = "SEA PASSAGE BALLAST";
        public const string ETA = "ETA";
        public const string ETD = "ETD";
        public const string ETS = "ETS";
        public const string EOSP = "E.O.S.P.";
        public const string FAOP = "F.A.O.P.";
        public const string ETB = "ETB";
        public const string BTHD = "BTHD";
        public const string ETDBRTH = "ETD BRTH";
        public const string UNBTHD = "UNBTHD";
        public const int ReportedInLastNDays = 14;
        public const string HTMLStartString = "<!DOCTYPE html";
        public const string SpeedKts = "SPEED (Kts)";
        public const int CreatedInLastNMonths = 6;
        public static int InLastDays = 30;

        public const string CharterLoaded = "Cht.Loaded";
        public const string CharterBallast = "Cht. Ballast";
        public const string SpeedLabel = "SPEED";
        public const string FOLabel = "FO";
        public const string GOLabel = "GO";
        public const string LSFOLabel = "LSFO";
        public const string DOLabel = "DO";
        public const string LNGLabel = "LNG";
        public const string SwellLength = "Swell Length";
        public const string WindForce = "Wind Force";
        public const string FreightOrders = "Freight Order";
        public const int DaysToMonth = 30;
        public const int MonthToYears = 12;
        public const int DocumentAboutToExpireDays = 90;
        public const string RedColor = "red";
        public const string OrangeColor = "orange";
        public const string SupplierOrderStatusForQuoteAuthorizationIncorrect = "Order {0} - {1} cannot be authorised yet because the supplier order status should be 'Tender Awaiting Authorisation' or 'Tender Awaiting Review'.";
        public const string OrderStatusForQuoteAuthorizationIncorrect = "Order {0} - {1} cannot be authorised yet because the order status should be 'Tender Awaiting Authorisation' or 'Tender Awaiting Review'.";
        public const string VesselManagementEnd = "The management contract has ended, no further actions can be completed under this contract.";
        public const string OrderAuthoriseSuccessMessage = "Order authorised successfully.";
        public const string OrderAuthoriseFailedMessage = "Order authorization failed.";
        public const string OrderRejectSuccessMessage = "Order rejected successfully.";
        public const string OrderRejectFailMessage = "Order reject failed.";
        public const string VesselInspectionReportFileName = "Inspection Summary";
        public const string ZipExtension = ".zip";
        public const string OrderDetailsReportFileName = "Order Detail Report";
        public const string PurchaseOrderFilter = "PurchaseOrderFilter";
        public const string VesselIdFilter = "VesselIdFilter";
        public const string AccessoryVessel = "GLAS00000057";
        public const string AccessoryUserDefined = "GLAS00000058";
        public const string CrewListFilter = "CrewListFilter";
        public const string VesselDefectListFileName = "Defect List Summary";
        public const string DefectManagerFilter = "DefectManagerFilter";
        public const string OrderStatusColorCritical = "Critical";
        public const string OrderStatusColorGood = "Good";
        public const string OrderStatusColorPreWarning = "PreWarning";
        public const string OrderCancelled_ZZ = "ZZ";
        public const string OnHold_OH = "OH";
        public const string OrderCancelled = "Order Cancelled";
        public const string OnHold = "On Hold";
        public const string All = "All";
        public const string MonthYearFormat = "MMM yyyy";
        public const string MultiCurrency = "Multi";
        public const string MultiCurrencyType = "2";
        public const int CrewChangeLastXDays = -7;
        public const int CrewOverdueXDays = -30;
        public const int VarianceMonthsDuration = 3;
        public const int VariancePercentageFirstXMonthLimit = -10;
        public const int VariancecPercentageSecondXMonthLimit = -5;
        public const int CriticalOverduePriorityLimitPMS = 0;
        public const int OverduePriorityLimitPMS = 0;
        public const string None = "None";
        public const string WorkOrder = "Work Order";
        public const string JsaWithPermitRequiredTooltip = "JSA required with permit.";
        public const string JsaRequiredTooltip = "JSA required.";
        public const string JsaPermitRequiredTooltip = "Permit required.";
        public const string NotApplicable = "N/A";
        public const int HazOccAccidentNMonths = -3;
        public const int HazOccIncidentNMonths = -3;
        public const int HazOccAccidentPriority = 0;
        public const int HazOccIncidentPriority = 0;
        public const int HazOccUnsafeConditionPriority = 1;
        public const int SeriousIncidentFleetLevelNMonths = -3;
        public const int OverdueInspectionDefaultYearPeriod = -3;
        public const int PscDetentionFleetLevelNMonths = -3;
        public const string PSCInspectionTypeId = "10";
        public const int OMVFindingsFleetLevelNMonths = -6;
        public const int PscDeficienciesFleetLevelNMonths = -3;
        public const int OilSpillsToWaterFleetLevelNMonths = -3;
        public const int FuelEfficiencyFleetLevelNMonths = -3;
        public const string CanBeSetToCompleteWithoutOfficeReview = "Can be set to complete without office review";
        public const string NeedsToBeSubmittedForReviewAndCompletionInOffice = "Needs to be submitted for review and completion in office";
        public const int HazoccGraphMonthPeriod = 12;
        public const string Yes = "Yes";
        public const string No = "No";

        public const string HumanFactorsText = "Human Factors";
        public const string JobFactorsText = "Job Factors";
        public const string ManagementFailureText = "Management Failure";
        public const string StandardActText = "Substandard Acts";
        public const string StandardCondtionText = "Substandard Conditions";
        public const string PassengerVesselType = "Passenger Ship";
        public const string LocationOnboard = "Location Onboard";
        public const string LocationAshore = "Location Ashore";
        public const string DashForEmpty = "-";
        public const string DayLight = "DayLight";
        public const string Night = "Night";
        public const string Ashore = "Ashore";
        public const string OnBoard = "On Board";
        public const string VShipsClientName = "V. Ships";

        public const string YardNameReportLocationLabel = "Yard Name";
        public const string SpecifyReportLocationLabel = "Specify";
        public const string RivernameReportLocationLabel = "Rivername";
        public const string CanalNameReportLocationLabel = "Canal Name";
        public const string InstallationNameRefReportLocationLabel = "Installation Name/Ref";

        public const string FleetSavedFailed = "Fleet saved failed.";
        public const string FleetSavedSuccessfully = "Fleet saved successfully.";
        public const int UAUCRateNMonths = -12;
        public const string AccessController = "Access";
        public const string AuthCodeController = "AuthCode";
        public const string InspectionListPageKey = "InspectionFilter";
        public const string InspectionFindingPageKey = "InspectionFindings";
        public const string InspectionActionPageKey = "InspectionAction";
        public const string DefectListPageKey = "DefectListFilter";
        public const string DefectDetailsPageKey = "DefectDetailsFilter";
        public const string MobileVesselListPortActivityDateTimeFormat = "HH:mm, dd/MM/yy";
        public const string VesselEncryptionText = "Vessel";
        public const string HazOccDetailsEncryptionText = "HazOccDetails";
        public const string PMSList = "PMSList";
        public const string PlannedMaintenanceController = "PlannedMaintenance";
        public const string ListMethod = "List";
        public const string DashboardFilterKey = "DashboardFilter";
        public const string InspectionFindingMethod = "Findings";
        public const string InspectionEncryptionKey = "Inspection";
        public const string PurchaseOrderEncryptionText = "PurchaseOrder";
        public const string ViewQuoteHeaderEncryptionText = "ViewQuoteHeader";
        public const string NoteIdEncryptionText = "NoteId";
        public const string ReminderIdEncryptionText = "ReminderIdEncryptionText";
        public const string VesselIdEncryptionText = "VesselId";
        public const string CL = "CL";
        public const string CA = "CA";
        public const string SR = "SR";
        public const string CR = "CR";
        public const string Closed = "Closed";
        public const string CorrectiveAction = "Corrective Action";
        public const string SendForReview = "Send for Review";
        public const string Correction = "Correction";
        public const string DashboardVesselNavigation = "/Dashboard/?VesselId=";
        public const string Condition = "Condition";
        public const string PosActivityType_Medical_PLA_ID = "MD";
        public const string FullMapDetails = "FullMapDetails";
        public const string PreferencesSavedSuccess = "Preferences saved successfully.";
        public const string PreferencesSavedFailed = "Preferences not saved.";
        public const string EstimatedPeriodDays = " Days";
        public const string EstimatedPeriodMinutes = " Minutes";
        public const string EstimatedPeriodHours = " Hours";

        public const string PMSDue = "Due";
        public const string PMSCriticalDue = "Critical Due";
        public const string PMSOverdue = "Overdue (Prior Months)";
        public const string PMSCriticalOverdue = "Critical Overdue (Prior Months)";
        public const string PMSCritical = "Critical";
        public const string PMSCompleted= "Completed";
        public const string PMSPlannedFor = "Planned For";
        public const string PMSReqReschedule = "Req. Resch.";
        public const string PMSAll = "All";
        public const string PMSCertificates = "PMS Certificates";
        public const string PMSOverhaul = "Overhaul";
        public const string PMSRescheduled = "Rescheduled";
        public const string NotificationMobileChatDetailEncKey = "NotificationMobileChatDetailURL";
        public const string NotificationMobileInfoEncKey = "NotificationMobileInfoURL";
        public const string NotificationMobileDiscussionEncKey = "NotificationMobileDiscussionEncKey";
        public const string NotificationRecordDetailsEncKey = "NotificationRecordDetailsEncKey";
        public const string TimeZoneDiffSessionKey = "TimeZoneDifference";
        public const string CertificatesDetails = "CertificatesDetails";
        public const string VChatModalId = "VCM";
        public const int ReminderAlertDayDifference = 30;
        public const string Client = "C";
        public const string Responsibilities = "R";
        public const string JSAList = "JSAList";
        public const string JSADetails = "JSADetails";
        public const string JsaJobId = "JobId";
        public const string JSADetailsPageKey = "JSADetailsFilter";
        public const string AdditionalJobHazards = "Additional Job Hazards";
        public const string MeetingGuidelinesKey = "GLAS00000062";
        public const string ApprovalListPageKey = "ApprovalListFilter";
        public const string ToastrSuccess = "success";
        public const string ToastrError = "error";
        public const string ToastrValidate = "validate";
        public const string FleetRequestEncryptionText = "FleetRequest";
        public const string ApprovalEncryptionText = "ApprovalList";
        public const string RescheduleRequestEncryptionText = "RescheduleRequest";
        public const string InspectionIdEncryptionText = "InspectionKey";
        public const string DocumentUrlEncryptionText = "DocumentURL";
        public const string SP = "SP";
        public const string ShortEOSP = "EOSP";
        public const string ShortFAOP = "FAOP";
        public const string Png = ".png";
        public const string Amber = "Amber";
        public const string Green = "Green";
        public const string Red = "Red";
        public const string Flag = "Flag";
        public const string VesselId = "VesselId";
        public const string Increased = "Increased";
        public const string Decreased = "Decreased";
        public const string Total = "Total";
        public const string OverallScore = "Overall Score";
        public const string VesselList = "VesselList";
        public const string VoyageReportingURL = "VoyageReportingURL";

        //Notification Record Navigation Keys
        public const string NavigationUrlConstant = "URL";

        //to get values from the context parameters while navigating to the details page
        //Table: MessageCategory, Column: ContextPayloadTemplate
        // start
        public const string NotificationPoNavigationCoyId = "CoyId";
        public const string NotificationPoNavigationOrderNo = "OrderNo";
        public const string NotificationInspectionNavigationInspectionId = "InspectionId";
        public const string NotificationInspectionNavigationFindingId = "FindingId";
        public const string NotificationCertificateNavigationVesselCertificateId = "VesselCertificateId";
        public const string NotificationHazOccNavigationIncidentId = "IncidentId";
        public const string NotificationCrewNavigationCrewId = "CrewId";
        public const string NotificationVASeaPassageNavigationPositionListId = "PositionListId";
        public const string NotificationVAPortCallLocationEventNavigationPositionListId = "PositionListId";
        public const string NotificationDefectsNavigationDefectWorkOrderId = "DwoId";
        public const string NotificationPMSNavigationWorkOrderId = "PwoId";
        // end - Please add the non categorised constants in the above section only

        //Tabs Keys
        //start
        public const string Tab1 = "tab-1"; //Summary
        public const string Tab2 = "tab-2"; //List
        public const string Tab3 = "tab-3"; //Map

        //Tabs as dropdown in mobile view
        public const string DropdownTab1 = "dropdown-tab-1"; 
        public const string DropdownTab2 = "dropdown-tab-2";
        public const string DropdownTab3 = "dropdown-tab-3";
        public const string DropdownTab4 = "dropdown-tab-4";
        public const string DropdownTab5 = "dropdown-tab-5";
        //end

        //Noon Fuel ROB title
        public const string FO = "HFO (mt)";
        public const string LSFO = "LFO (mt)";
        public const string DO = "DO (mt)";
        public const string GO = "GO (mt)";
        public const string LNG = "LNG (mt)";
        public const string BunkerLNG = "LNG Bunker (mt)";
        public const string CargoLNG = "Cargo Boil Off (mt)";

        public const string Current = "Current";
        public const string Previous = "Previous";
        public const string Weather24HoursLabel = "Deck Log Weather Extract for last {0} Hours";

        //Shipsure Brush
        public const string ShipsureBrushGreen = "#52A829";
        public const string ShipsureBrushDarkBlue = "#00679C";
        public const string ShipsureBrushGraphLineCyan = "#15ACA2";
        public const string ShipsureBrushYellow = "#FFCA2B";
        public const string ShipsureBrushRed = "#FF2905";
        public const string ShipsureBrushOrange = "#FF8D1F";
        public const string ShipsureBrushPurple = "#9D4F9B";

        //Log File Name
        public const string LogFileName = "LogFileName";

        //Demo
        public const string DemoValue = "_Demo";
        public const string DemoMode = "DemoMode";
        public const string DemoControlId = "346F8CA4-AF18-4AF0-9454-5A949048EECB";

        //Sentinel Categories
        public const string Certificates = "Certificates";
        public const string CrewStrength = "Crew Strength";
        public const string Environmental = "Environmental";
        public const string HazOcc = "HazOcc";
        public const string Inspections = "Inspections";
        public const string PMSDefects = "PMS & Defects";
        public const string Management = "Management";
        public const string OPEXProcurement = "OPEX & Procurement";
        public const string SafetyCulture = "Safety Culture";
        public const string ShipSureUsage = "ShipSure Usage";
    }
};