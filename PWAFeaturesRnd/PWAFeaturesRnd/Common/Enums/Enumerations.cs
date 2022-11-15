using PWAFeaturesRnd.Common.DataAttributes;

namespace PWAFeaturesRnd.Common.Enums
{
    /// <summary>
    /// Enumeration for purchase order status
    /// </summary>
    public enum PurchaseOrderStatus
    {
        /// <summary>
        /// The delivery to agent
        /// </summary>
        [EnumValueData(Name = "Delivery to Agent", KeyValue = "DA")]
        DeliveryToAgent,

        /// <summary>
        /// The delivery to vessel
        /// </summary>
        [EnumValueData(Name = "Delivery to Vessel", KeyValue = "DV")]
        DeliveryToVessel,

        /// <summary>
        /// The delivery to warehouse
        /// </summary>
        [EnumValueData(Name = "Delivery to Warehouse", KeyValue = "DW")]
        DeliveryToWarehouse,

        /// <summary>
        /// The enquiry outstanding
        /// </summary>
        [EnumValueData(Name = "Enquiry Outstanding", KeyValue = "EO")]
        EnquiryOutstanding,

        /// <summary>
        /// The enquiry in progress
        /// </summary>
        [EnumValueData(Name = "Enquiry in progress", KeyValue = "EP")]
        EnquiryInProgress,

        /// <summary>
        /// The freight order
        /// </summary>
        [EnumValueData(Name = "Freight Order", KeyValue = "FO")]
        FreightOrder,

        /// <summary>
        /// The not received
        /// </summary>
        [EnumValueData(Name = "Not Received", KeyValue = "NR")]
        NotReceived,

        /// <summary>
        /// The order issued
        /// </summary>
        [EnumValueData(Name = "Order Issued", KeyValue = "O")]
        OrderIssued,

        /// <summary>
        /// The on hold
        /// </summary>
        [EnumValueData(Name = "On Hold", KeyValue = "OH")]
        OnHold,

        /// <summary>
        /// The order ready ex_ works
        /// </summary>
        [EnumValueData(Name = "Order Ready Ex-Works", KeyValue = "OX")]
        OrderReadyEx_Works,

        /// <summary>
        /// The part order received by vessel
        /// </summary>
        [EnumValueData(Name = "Part Order Received by Vessel", KeyValue = "PD")]
        PartOrderReceivedByVessel,

        /// <summary>
        /// The pro forma invoice received
        /// </summary>
        [EnumValueData(Name = "Pro-Forma Invoice Received", KeyValue = "PF")]
        ProFormaInvoiceReceived,

        /// <summary>
        /// The draft of requisition
        /// </summary>
        [EnumValueData(Name = "Draft of Requisition", KeyValue = "PL")]
        DraftOfRequisition,

        /// <summary>
        /// The received by agent
        /// </summary>
        [EnumValueData(Name = "Received by Agent", KeyValue = "RA")]
        ReceivedByAgent,

        /// <summary>
        /// The requisition
        /// </summary>
        [EnumValueData(Name = "Requisition", KeyValue = "RE")]
        Requisition,

        /// <summary>
        /// The rejected
        /// </summary>
        [EnumValueData(Name = "Rejected", KeyValue = "RJ")]
        Rejected,

        /// <summary>
        /// The received in transit
        /// </summary>
        [EnumValueData(Name = "Received in Transit", KeyValue = "RT")]
        ReceivedInTransit,

        /// <summary>
        /// The received by vessel
        /// </summary>
        [EnumValueData(Name = "Received by Vessel", KeyValue = "RV")]
        ReceivedByVessel,

        /// <summary>
        /// The stored at warehouse
        /// </summary>
        [EnumValueData(Name = "Stored at Warehouse", KeyValue = "SW")]
        StoredAtWarehouse,

        /// <summary>
        /// The tender awaiting authorisation
        /// </summary>
        [EnumValueData(Name = "Tender Awaiting Authorisation", KeyValue = "TA")]
        TenderAwaitingAuthorisation,

        /// <summary>
        /// The tender awaiting technical information
        /// </summary>
        [EnumValueData(Name = "Tender Awaiting Technical Information", KeyValue = "TH")]
        TenderAwaitingTechnicalInformation,

        /// <summary>
        /// The tender issued
        /// </summary>
        [EnumValueData(Name = "Tender Issued", KeyValue = "TI")]
        TenderIssued,

        /// <summary>
        /// The tender ready for order
        /// </summary>
        [EnumValueData(Name = "Tender Ready for Order", KeyValue = "TR")]
        TenderReadyForOrder,

        /// <summary>
        /// The unable to quote
        /// </summary>
        [EnumValueData(Name = "Tender Declined", KeyValue = "TX")]
        UnableToQuote,

        /// <summary>
        /// The order cancelled
        /// </summary>
        [EnumValueData(Name = "Order Cancelled", KeyValue = "ZZ")]
        OrderCancelled,

        /// <summary>
        /// The tender awaiting review
        /// </summary>
        [EnumValueData(Name = "Tender Awaiting Review", KeyValue = "TQ")]
        TenderAwaitingReview,

        /// <summary>
        /// The default
        /// </summary>
        [EnumValueData(Name = "NULL", KeyValue = "NULL")]
        Default
    }

    /// <summary>
    /// Enumeration for purchase order types
    /// </summary>
    public enum PurchaseOrderType
    {
        /// <summary>
        /// The materials
        /// </summary>
        [EnumValueData(Name = "Materials", KeyValue = "Materials")]
        Materials,

        /// <summary>
        /// The spares
        /// </summary>
        [EnumValueData(Name = "Spares", KeyValue = "Spares")]
        Spares,

        /// <summary>
        /// The consumables
        /// </summary>
        [EnumValueData(Name = "Consumables", KeyValue = "Consumables")]
        Consumables,

        /// <summary>
        /// The services
        /// </summary>
        [EnumValueData(Name = "Services", KeyValue = "Services")]
        Services
    }

    /// <summary>
    /// Enumeration for purchase order priority
    /// </summary>
    public enum PurchaseOrderPriority
    {
        /// <summary>
        /// The fast track
        /// </summary>
        [EnumValueData(Name = "Fast Track", KeyValue = "Fast Track")]
        FastTrack,
        /// <summary>
        /// The local
        /// </summary>
        [EnumValueData(Name = "Local", KeyValue = "Local")]
        Local,
        /// <summary>
        /// The normal
        /// </summary>
        [EnumValueData(Name = "Normal", KeyValue = "Normal")]
        Normal,
        /// <summary>
        /// The urgent
        /// </summary>
        [EnumValueData(Name = "Urgent", KeyValue = "Urgent")]
        Urgent

    }

    /// <summary>
    /// Enumeration for user menu item type
    /// </summary>
    public enum UserMenuItemType
    {

        /// <summary>
        /// All vessels
        /// </summary>
        [EnumValueData(Name = "All Vessels", KeyValue = "1")]
        AllVessels,

        /// <summary>
        /// The offices
        /// </summary>
        [EnumValueData(Name = "My Offices", KeyValue = "2")]
        MyOffices,

        /// <summary>
        /// The groups
        /// </summary>
        [EnumValueData(Name = "My Groups", KeyValue = "3")]
        MyGroups,

        /// <summary>
        /// The clients
        /// </summary>
        [EnumValueData(Name = "My Clients", KeyValue = "4")]
        MyClients,

        /// <summary>
        /// The vessel type
        /// </summary>
        [EnumValueData(Name = "All Vessel Types", KeyValue = "5")]
        AllVesselTypes,

        /// <summary>
        /// The vessel status
        /// </summary>
        [EnumValueData(Name = "All Vessel Status", KeyValue = "6")]
        AllVesselStatus,

        /// <summary>
        /// Your fleet
        /// </summary>
        [EnumValueData(Name = "My Fleets", KeyValue = "7")]
        MyFleets,

        /// <summary>
        /// The vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "8")]
        Vessel,

        /// <summary>
        /// The office
        /// </summary>
        [EnumValueData(Name = "Office", KeyValue = "9")]
        Office,

        /// <summary>
        /// The group
        /// </summary>
        [EnumValueData(Name = "Group", KeyValue = "10")]
        Group,

        /// <summary>
        /// The client
        /// </summary>
        [EnumValueData(Name = "Client", KeyValue = "11")]
        Client,

        /// <summary>
        /// The vessel type
        /// </summary>
        [EnumValueData(Name = "Vessel Type", KeyValue = "12")]
        VesselType,

        /// <summary>
        /// The vessel status
        /// </summary>
        [EnumValueData(Name = "Vessel Status", KeyValue = "14")]
        VesselStatus,

        /// <summary>
        /// The fleet
        /// </summary>
        [EnumValueData(Name = "Fleet", KeyValue = "15")]
        Fleet,

        /// <summary>
        /// The my vessel
        /// </summary>
        [EnumValueData(Name = "My Vessel", KeyValue = "16")]
        MyVessel,

        /// <summary>
        /// Your fleet
        /// </summary>
        [EnumValueData(Name = "My Favourites", KeyValue = "17")]
        MyFavourites,

        /// <summary>
        /// Responsibilites
        /// </summary>
        [EnumValueData(Name = "My Responsibilities", KeyValue = "18")]
        MyResponsibilities,

        /// <summary>
        /// Leaving Management
        /// </summary>
        [EnumValueData(Name = "Leaving Management", KeyValue = "19")]
        LeavingManagement,

        /// <summary>
        /// The left management
        /// </summary>
        [EnumValueData(Name = "Left Management", KeyValue = "20")]
        LeftManagement,

        /// <summary>
        /// The My Planning Cells
        /// </summary>
        [EnumValueData(Name = "My Planning Cells", KeyValue = "21")]
        MyPlanningCells,

        /// <summary>
        /// Planning Cell.
        /// </summary>
        [EnumValueData(Name = "Planning Cell", KeyValue = "22")]
        PlanningCell,

        /// <summary>
        /// Budget
        /// </summary>
        [EnumValueData(Name = "Budget", KeyValue = "23")]
        Budget,

        /// <summary>
        /// The inactive
        /// </summary>
        [EnumValueData(Name = "InActive", KeyValue = "24")]
        InActive,

        /// <summary>
        /// My flags
        /// </summary>
        [EnumValueData(Name = "Flag", KeyValue = "25")]
        MyFlags,
        /// <summary>
        /// The flag
        /// </summary>
        [EnumValueData(Name = "Flag", KeyValue = "25")]
        Flag,

        /// <summary>
        /// The moto moco
        /// </summary>
        [EnumValueData(Name = "Moto Moco", KeyValue = "26")]
        MotoMoco,

        ///// <summary>
        ///// All user vessel
        ///// </summary>
        //[EnumValueData(Name = "All", KeyValue = "27")]
        //UserAllVessel,

        /// <summary>
        /// The responsible left management
        /// </summary>
        [EnumValueData(Name = "Responsible Left Management", KeyValue = "27")]
        ResponsibleLeftManagement,

        /// <summary>
        /// My entity responsibilities
        /// </summary>
        [EnumValueData(Name = "My Entity Responsibilities", KeyValue = "28")]
        MyEntityResponsibilities,

        /// <summary>
        /// The entity company
        /// </summary>
        [EnumValueData(Name = "Entity Company", KeyValue = "29")]
        EntityCompany,

        /// <summary>
        /// The entering management
        /// </summary>
        [EnumValueData(Name = "Entering Management", KeyValue = "30")]
        EnteringManagement,

        /// <summary>
        /// The responsible entering management
        /// </summary>
        [EnumValueData(Name = "Responsible Entering Management", KeyValue = "31")]
        ResponsibleEnteringManagement,

        /// <summary>
        /// The entering purchasing management.
        /// </summary>
        [EnumValueData(Name = "Entering Management", KeyValue = "32")]
        EnteringPurchasingManagement,

        /// <summary>
        /// My purchasing responsibilities.
        /// </summary>
        [EnumValueData(Name = "Responsibilities", KeyValue = "33")]
        MyPurchasingResponsibilities
    }

    /// <summary>
    /// Purchasing Filter Order Number Type
    /// </summary>
    public enum PurchasingFilterOrderNumberType
    {
        /// <summary>
        /// The ship sure order number
        /// </summary>
        [EnumValueData(Name = "ShipSure Order No", KeyValue = "ShipSureOrderNumber")]
        ShipSureOrderNumber,

        /// <summary>
        /// The external order number
        /// </summary>
        [EnumValueData(Name = "Externa lOrder No", KeyValue = "ExternalOrderNumber")]
        ExternalOrderNumber,

        /// <summary>
        /// The requisition number
        /// </summary>
        [EnumValueData(Name = "RequisitionNo", KeyValue = "RequisitionNumber")]
        RequisitionNumber
    }

    /// <summary>
    /// Enumeration for purchase order stage
    /// </summary>
    public enum PurchaseOrderStage
    {
        //[EnumValueData(Name = "Not Specified", KeyValue = "Not Specified")]
        //NotSpecified,
        /// <summary>
        /// The requisition
        /// </summary>
        [EnumValueData(Name = "Requisition", KeyValue = "Requisition")]
        [PurchaseStatusAttribute(new PurchaseOrderStatus[] { PurchaseOrderStatus.DraftOfRequisition, PurchaseOrderStatus.Requisition, PurchaseOrderStatus.OnHold, PurchaseOrderStatus.OrderCancelled })]
        [ShortCode(ShortCode = "REQ")]
        Requisition,
        /// <summary>
        /// The enquiry
        /// </summary>
        [EnumValueData(Name = "Enquiry", KeyValue = "Enquiry")]
        [PurchaseStatusAttribute(new PurchaseOrderStatus[] { PurchaseOrderStatus.EnquiryOutstanding, PurchaseOrderStatus.EnquiryInProgress, PurchaseOrderStatus.TenderAwaitingAuthorisation, PurchaseOrderStatus.TenderAwaitingReview, PurchaseOrderStatus.OnHold, PurchaseOrderStatus.OrderCancelled })]
        [ShortCode(ShortCode = "ENQ")]
        Enquiry,
        /// <summary>
        /// The authorised enquiry
        /// </summary>
        [EnumValueData(Name = "Authorised Enquiry", KeyValue = "Authorised Enquiry")]
        [PurchaseStatusAttribute(new PurchaseOrderStatus[] { PurchaseOrderStatus.TenderReadyForOrder, PurchaseOrderStatus.OnHold, PurchaseOrderStatus.OrderCancelled })]
        [ShortCode(ShortCode = "Auth ENQ")]
        AuthorisedEnquiry,
        /// <summary>
        /// The purchase order
        /// </summary>
        [EnumValueData(Name = "Purchase Order", KeyValue = "Purchase Order")]
        [PurchaseStatusAttribute(new PurchaseOrderStatus[] { PurchaseOrderStatus.OrderIssued, PurchaseOrderStatus.OrderReadyEx_Works, PurchaseOrderStatus.DeliveryToAgent, PurchaseOrderStatus.DeliveryToVessel, PurchaseOrderStatus.DeliveryToWarehouse, PurchaseOrderStatus.StoredAtWarehouse, PurchaseOrderStatus.ReceivedByAgent, PurchaseOrderStatus.ReceivedInTransit, PurchaseOrderStatus.ReceivedByVessel, PurchaseOrderStatus.ProFormaInvoiceReceived, PurchaseOrderStatus.PartOrderReceivedByVessel, PurchaseOrderStatus.OnHold, PurchaseOrderStatus.OrderCancelled })]
        [ShortCode(ShortCode = "PO")]
        PurchaseOrder,

        //[EnumValueData(Name = "ANY", KeyValue = "ANY")]
        //[PurchaseStatusAttribute(new PurchaseOrderStatus[] { PurchaseOrderStatus.OnHold, PurchaseOrderStatus.OrderCancelled })]
        //ANY,
        /// <summary>
        /// The freight order
        /// </summary>
        [EnumValueData(Name = "Freight Order", KeyValue = "Freight Order")]
        [PurchaseStatusAttribute(new PurchaseOrderStatus[] { PurchaseOrderStatus.FreightOrder, PurchaseOrderStatus.OnHold, PurchaseOrderStatus.OrderCancelled })]
        [ShortCode(ShortCode = "FO")]
        FreightOrder
    }

    /// <summary>
    /// Sort direction
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// The ascending
        /// </summary>
        [EnumValueData(Name = "Ascending", KeyValue = "ASC")]
        Ascending,

        /// <summary>
        /// The descending
        /// </summary>
        [EnumValueData(Name = "Descending", KeyValue = "DESC")]
        Descending
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ExceptionCategory
    {
        /// <summary>
        /// The unexpected error
        /// </summary>
        [EnumExceptionData(Message = "An unexpected error has occurred.", Code = "1000")]
        UnexpectedError,

        /// <summary>
        /// The security error. Not used anywhere as of 30 Apr 2018.
        /// </summary>
        [EnumExceptionData(Message = "A security error was detected.", Code = "2000")]
        SecurityError,

        /// <summary>
        /// The input format error
        /// </summary>
        [EnumExceptionData(Message = "An input format error was detected.", Code = "3000")]
        InputFormatError,

        /// <summary>
        /// The database error. Used only in baseService for handling SQL Exception.
        /// </summary>
        [EnumExceptionData(Message = "A database exception has occurred.", Code = "4000")]
        DatabaseError,

        /// <summary>
        /// The validation error. Used only in baseService for handling ORM Entity Validation Exception.
        /// </summary>
        [EnumExceptionData(Message = "A ORM validation exception was detected.", Code = "5000")]
        ValidationError,

        /// <summary>
        /// The business error
        /// </summary>
        [EnumExceptionData(Message = "A Business Level error was detected.", Code = "6000")]
        BusinessError,

        /// <summary>
        /// The amazon s3 exception. Used for handling Amazon Cloud Service Exception.
        /// </summary>
        [EnumExceptionData(Message = "Cloud storage error was detected.", Code = "7000")]
        AmazonS3Exception,

        /// <summary>
        /// The amazon s3 file not found
        /// </summary>
        [EnumExceptionData(Message = "File not found.", Code = "8000")]
        AmazonS3FileNotFound,

        /// <summary>
        /// The file not found
        /// </summary>
        [EnumExceptionData(Message = "File not found.", Code = "9000")]
        FileNotFound,

        /// <summary>
        /// The directory not found
        /// </summary>
        [EnumExceptionData(Message = "Directory not found.", Code = "10000")]
        DirectoryNotFound
    }

    /// <summary>
    /// 
    /// </summary>
    public enum BusinessExceptionDetail
    {
        /// <summary>
        /// The invalid input field
        /// </summary>
        [EnumExceptionData(Message = "Invalid Value for field.", Code = "3001")]
        InvalidInputField,

        /// <summary>
        /// The missing input field
        /// </summary>
        [EnumExceptionData(Message = "Field not specified.", Code = "3002")]
        MissingInputField,
    }

    /// <summary>
    /// This enum is used for inspection dashboard counts.
    /// </summary>
    public enum InspectionDashboardType
    {
        /// <summary>
        /// The inspection PSC type
        /// </summary>
        [EnumValueData(Name = "Inspections & Audits - PSC Det.", KeyValue = "InspectionPSCType")]
        InspectionPSCType,

        /// <summary>
        /// The inspection omv type
        /// </summary>
        [EnumValueData(Name = "InspectionOMVType", KeyValue = "InspectionOMVType")]
        InspectionOMVType,

        /// <summary>
        /// The detention type
        /// </summary>
        [EnumValueData(Name = "DetentionType", KeyValue = "DetentionType")]
        DetentionType,

        /// <summary>
        /// The inspection due type
        /// </summary>
        [EnumValueData(Name = "InspectionDueType", KeyValue = "InspectionDueType")]
        InspectionDueType,

        /// <summary>
        /// The inspection overdue type
        /// </summary>
        [EnumValueData(Name = "InspectionOverdueType", KeyValue = "InspectionOverdueType")]
        InspectionOverdueType,

        /// <summary>
        /// The inspection never done type
        /// </summary>
        [EnumValueData(Name = "InspectionNeverDoneType", KeyValue = "InspectionNeverDoneType")]
        InspectionNeverDoneType,

        /// <summary>
        /// The inspection finding outstanding type
        /// </summary>
        [EnumValueData(Name = "Inspections - Open Findings", KeyValue = "InspectionFindingOutstandingType")]
        InspectionFindingOutstandingType,

        /// <summary>
        /// The inspection finding overdue type
        /// </summary>
        [EnumValueData(Name = "Inspections - Overdue Findings", KeyValue = "InspectionFindingOverdueType")]
        InspectionFindingOverdueType,

        /// <summary>
        /// The pending closure by office type
        /// </summary>
        [EnumValueData(Name = "Inspections - Pending Closure", KeyValue = "PendingClosureByOfficeType")]
        PendingClosureByOfficeType,

        /// <summary>
        /// The audit inspection finding outstanding type
        /// </summary>
        [EnumValueData(Name = "Audits - Open Findings", KeyValue = "AuditInspectionFindingOutstandingType")]
        AuditInspectionFindingOutstandingType,

        /// <summary>
        /// The audit inspection finding overdue type
        /// </summary>
        [EnumValueData(Name = "Audits - Overdue Findings", KeyValue = "AuditInspectionFindingOverdueType")]
        AuditInspectionFindingOverdueType,

        /// <summary>
        /// The audit pending closure by office type
        /// </summary>
        [EnumValueData(Name = "Audits - Pending Closure", KeyValue = "AuditPendingClosureByOfficeType")]
        AuditPendingClosureByOfficeType,

        /// <summary>
        /// The audit pending closure by office type
        /// </summary>
        [EnumValueData(Name = "Inspections & Audits", KeyValue = "AllInspection")]
        AllInspection,

        /// <summary>
        /// The findings open type
        /// </summary>
        [EnumValueData(Name = "Inspections & Audits - Open Findings", KeyValue = "FindingsOutstandingType")]
        FindingsOutstandingType,

        /// <summary>
        /// The findings overdue type
        /// </summary>
        [EnumValueData(Name = "Inspections & Audits - Overdue Findings", KeyValue = "FindingsOverdueType")]
        FindingsOverdueType,

        /// <summary>
        /// The PSC deficiency type
        /// </summary>
        [EnumValueData(Name = "Inspections & Audits - PSC Deficen. Last 3 Months", KeyValue = "PSCDeficiencyType")]
        PSCDeficiencyType,

        /// <summary>
        /// Vessel Inspection Report
        /// </summary>
        [EnumValueData(Name = "Inspection Type - Vessel Inspection Report", KeyValue = "VesselInspectionReport")]
        VesselInspectionReport,

        /// <summary>
        /// The omv findings type
        /// </summary>
        [EnumValueData(Name = "Inspection Type - OMV", KeyValue = "OMVFindingsType")]
        OMVFindingsType,

        /// <summary>
        /// The open omv findings type
        /// </summary>
        [EnumValueData(Name = "Inspection Type - OMV", KeyValue = "OpenOMVFindingsType")]
        OpenOMVFindingsType,

        /// <summary>
        /// The PSC deficiency type
        /// </summary>
        [EnumValueData(Name = "Inspection Type - PSC", KeyValue = "InspectionPscDeficiencyType")]
        InspectionPscDeficiencyType,

        /// <summary>
        /// The OMV Rejection Type
        /// </summary>
        [EnumValueData(Name = "Inspection Type - OMV Rejection", KeyValue = "OMVRejectionType")]
        OMVRejectionType
    }

    /// <summary>
    /// This enum is for Inspection Type
    /// </summary>
    public enum InspectionType
    {
        /// <summary>
        /// The oil major vetting
        /// </summary>
        [EnumValueData(Name = "Oil Major Vetting", KeyValue = "17")]
        OilMajorVetting,

        /// <summary>
        /// The port state control
        /// </summary>
        [EnumValueData(Name = "Port State Control", KeyValue = "10")]
        PortStateControl,

        /// <summary>
        /// The vessel inspection report
        /// </summary>
        [EnumValueData(Name = "Vessel Inspection Report", KeyValue = "43")]
        VesselInspectionReport,

    }

    /// <summary>
    /// 
    /// </summary>
    public enum InspectionFindingFilter
    {
        /// <summary>
        /// All findings
        /// </summary>
        [EnumValueData(Name = "All Findings", KeyValue = "AllFindings")]
        AllFindings,

        /// <summary>
        /// The cleared
        /// </summary>
        [EnumValueData(Name = "Cleared", KeyValue = "Cleared")]
        Cleared,

        /// <summary>
        /// The outstanding
        /// </summary>
        [EnumValueData(Name = "Outstanding", KeyValue = "Outstanding")]
        Outstanding,

        /// <summary>
        /// The overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue
    }

    /// <summary>
    /// Order Line Quote Type
    /// </summary>
    public enum OrderLineQuoteType
    {
        /// <summary>
        /// All
        /// </summary>
        All,

        /// <summary>
        /// The quoted
        /// </summary>
        Quoted,

        /// <summary>
        /// The un quoted
        /// </summary>
        UnQuoted
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// The client
        /// </summary>
        [EnumValueData(Name = "Client", KeyValue = "Client")]
        Client,

        /// <summary>
        /// The internal
        /// </summary>
        [EnumValueData(Name = "Internal", KeyValue = "Internal")]
        Internal
    }

    /// <summary>
    /// Enum is use for order tracking detail
    /// </summary>
    public enum OrderTracker
    {
        /// <summary>
        /// The created
        /// </summary>
        [EnumValueData(Name = "Created", KeyValue = "Created")]
        Created,

        /// <summary>
        /// The requested
        /// </summary>
        [EnumValueData(Name = "Requested", KeyValue = "Requested")]
        Requested,

        /// <summary>
        /// The authorised
        /// </summary>
        [EnumValueData(Name = "Authorised", KeyValue = "Authorised")]
        Authorised,

        /// <summary>
        /// The ordered
        /// </summary>
        [EnumValueData(Name = "Ordered", KeyValue = "Ordered")]
        Ordered,

        /// <summary>
        /// The expected delivery
        /// </summary>
        [EnumValueData(Name = "Expected Delivery", KeyValue = "ExpectedDelivery")]
        ExpectedDelivery,

        /// <summary>
        /// The partially received
        /// </summary>
        [EnumValueData(Name = "Partially Received", KeyValue = "PartiallyReceived")]
        PartiallyReceived,

        /// <summary>
        /// The closed
        /// </summary>
        [EnumValueData(Name = "Closed", KeyValue = "Closed")]
        Closed
    }

    /// <summary>
    /// Tracker status enum
    /// </summary>
    public enum TrackerStatus
    {
        /// <summary>
        /// The completed
        /// </summary>
        [EnumValueData(Name = "Completed", KeyValue = "1")]
        Completed,

        /// <summary>
        /// The in progress
        /// </summary>
        [EnumValueData(Name = "In Progress", KeyValue = "2")]
        InProgress,

        /// <summary>
        /// The not completed
        /// </summary>
        [EnumValueData(Name = "Not Completed", KeyValue = "0")]
        NotCompleted
    }

    /// <summary>
    /// Supplier Rating enum
    /// </summary>
    public enum SupplierRating
    {
        /// <summary>
        /// The default, default value in case no rating is specified
        /// </summary>
        [EnumValueData(Name = "", KeyValue = null)]
        Default,

        /// <summary>
        /// The star1. Corresponds to E rating
        /// </summary>
        [EnumValueData(Name = "1", KeyValue = "E")]
        Star1,
        /// <summary>
        /// 2 star. Corresponds to D rating
        /// </summary>
        [EnumValueData(Name = "2", KeyValue = "D")]
        Star2,
        /// <summary>
        /// 3 star. Corresponds to C rating
        /// </summary>
        [EnumValueData(Name = "3", KeyValue = "C")]
        Star3,
        /// <summary>
        /// 4 star. Corresponds to B rating
        /// </summary>
        [EnumValueData(Name = "4", KeyValue = "B")]
        Star4,
        /// <summary>
        /// 5 star. Corresponds to A rating
        /// </summary>
        [EnumValueData(Name = "5", KeyValue = "A")]
        Star5
    }

    /// <summary>
    /// Company type enum
    /// </summary>
    public enum CompanyTypeEnum
    {
        /// <summary>
        /// The default. Will be used when no company type is specified
        /// </summary>
        [EnumValueData(Name = "All Companies", KeyValue = null)]
        Default,
        /// <summary>
        /// The supplier
        /// </summary>
        [EnumValueData(Name = "Supplier", KeyValue = "SUPP")]
        Supplier,
        /// <summary>
        /// The agent
        /// </summary>
        [EnumValueData(Name = "Agent", KeyValue = "AGEN")]
        Agent,
        /// <summary>
        /// The warehouse
        /// </summary>
        [EnumValueData(Name = "Warehouse", KeyValue = "WARE")]
        Warehouse,

        /// <summary>
        /// Group Company
        /// </summary>
        [EnumValueData(Name = "Group Company", KeyValue = "EGROUP")]
        [RechargeRecoveryType(TypeCode = "E")]
        GroupCompany,

        /// <summary>
        /// Purchase Supplier
        /// </summary>
        [EnumValueData(Name = "Purchase Supplier", KeyValue = "PSUPP")]
        PurchaseSupplier,

        /// <summary>
        /// Freight Supplier
        /// </summary>
        [EnumValueData(Name = "Freight Supplier", KeyValue = "FREIT")]
        FreightSupplier,

        /// <summary>
        /// The P and I club
        /// </summary>
        [EnumValueData(Name = "P & I Club", KeyValue = "PICL")]
        PANDIClub,

        /// <summary>
        /// Classification Society
        /// </summary>
        [EnumValueData(Name = "Classification Society", KeyValue = "CLSO")]
        ClassificationSociety,

        /// <summary>
        /// Yard
        /// </summary>
        [EnumValueData(Name = "YARDS", KeyValue = "YARD")]
        YARDS,

        /// <summary>
        /// Broker
        /// </summary>
        [EnumValueData(Name = "Broker", KeyValue = "BRKR")]
        Broker,

        /// <summary>
        /// The H and M lead underwriter
        /// </summary>
        [EnumValueData(Name = "H & M Lead Underwriter", KeyValue = "VINSC")]
        HMLeadUnderwriter,

        /// <summary>
        /// The Crew Management Offices
        /// </summary>
        [EnumValueData(Name = "Crew Management Offices", KeyValue = "CMO")]
        CrewManagementOffices,

        /// <summary>
        /// The Management Office
        /// </summary>
        [EnumValueData(Name = "Management Office", KeyValue = "MANO")]
        ManagementOffice,

        /// <summary>
        /// The Ship Management Offices
        /// </summary>
        [EnumValueData(Name = "Ship Management Offices", KeyValue = "SMO")]
        ShipManagementOffices,

        /// <summary>
        /// The Ship Owner
        /// </summary>
        [EnumValueData(Name = "Ship Owner", KeyValue = "OWNR")]
        ShipOwner,

        /// <summary>
        /// The Beneficial Owner
        /// </summary>
        [EnumValueData(Name = "Beneficial Owner", KeyValue = "BENE")]
        BeneficialOwner,

        /// <summary>
        /// The Registered Owner
        /// </summary>
        [EnumValueData(Name = "Registered Owner", KeyValue = "REG")]
        RegisteredOwner,

        /// <summary>
        /// The All Agent
        /// </summary>
        [EnumValueData(Name = "Position List", KeyValue = null)]
        PositionList,

        /// <summary>
        /// The Third Party Agent
        /// </summary>
        [EnumValueData(Name = "Third Party Agent", KeyValue = "3rdCRW")]
        ThirdPartyAgent,

        /// <summary>
        /// The Direct Employment
        /// </summary>
        [EnumValueData(Name = "Direct Employment", KeyValue = "DIRECT")]
        DirectEmployment,

        /// <summary>
        /// The marcas
        /// </summary>
        [EnumValueData(Name = "Marcas", KeyValue = "MCASUP")]
        Marcas,

        /// <summary>
        /// The vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "EVESS")]
        [RechargeRecoveryType(TypeCode = "V")]
        Vessel,

        /// <summary>
        /// The internal vessel
        /// </summary>
        [EnumValueData(Name = "Internal Vessel", KeyValue = "EVESS")]
        InternalVessel,

        /// <summary>
        /// The internal group company
        /// </summary>
        [EnumValueData(Name = "Internal Group Company", KeyValue = "EGROUP")]
        InternalGroupCompany,

        /// <summary>
        /// The internal group company
        /// </summary>
        [EnumValueData(Name = "Charterer", KeyValue = "CHRT")]
        Charterer,

        /// <summary>
        /// The client
        /// </summary>
        [EnumValueData(Name = "Client", KeyValue = "ECUST")]
        [RechargeRecoveryType(TypeCode = "C")]
        Client,

        /// <summary>
        /// The v.ships offices
        /// </summary>
        [EnumValueData(Name = "V.Ships Offices", KeyValue = "VSIGN")]
        VShipsOffices,

        /// <summary>
        /// The oil analysis.
        /// </summary>
        [EnumValueData(Name = "Oil Analysis", KeyValue = "OLAL")]
        OilAnalysis,

        /// <summary>
        /// The Oil Major Vetting Company.
        /// </summary>
        [EnumValueData(Name = "Oil Major Vetting Company", KeyValue = "OMVC")]
        OilMajorVettingCompany,

        /// <summary>
        /// The contracting company
        /// </summary>
        [EnumValueData(Name = "Ship Management Contract Company", KeyValue = "SMDCON")]
        ContractingCompany,

        /// <summary>
        /// The entity supplier
        /// </summary>
        [EnumValueData(Name = "Entity Supplier", KeyValue = "ESUPP")]
        EntitySupplier,

        /// <summary>
        /// The VShips Account Agent
        /// </summary>
        [EnumValueData(Name = "V.Ships Account Agent", KeyValue = "VAGENT")]
        VShipsAccountAgent,

        /// <summary>
        /// The third party inspector
        /// </summary>
        [EnumValueData(Name = "Third Party Inspector", KeyValue = "TPI")]
        ThirdPartyInspector,
        /// <summary>
        /// The vessel customer
        /// </summary>
        [EnumValueData(Name = "Vessel Customer", KeyValue = "VCUST")]
        VesselCustomer,

        /// <summary>
        /// The effective group client.
        /// </summary>
        [EnumValueData(Name = "Effective Group Client", KeyValue = "EGC")]
        EffectiveGroupClient,

        /// <summary>
        /// The deleted
        /// </summary>
        [EnumValueData(Name = "Deleted", KeyValue = "ZZZ")]
        Deleted,

        /// <summary>
        /// The deleted agent
        /// </summary>
        [EnumValueData(Name = "Deleted Agent", KeyValue = "ZZA")]
        DeletedAgent,
    }


    /// <summary>
    /// 
    /// </summary>
    public enum TreeType
    {
        /// <summary>
        /// The client
        /// </summary>
        [EnumValueData(Name = "VesselTree", KeyValue = "VesselTree")]
        VesselTree,

        /// <summary>
        /// The internal
        /// </summary>
        [EnumValueData(Name = "FleetOverviewTree", KeyValue = "FleetOverviewTree")]
        FleetOverviewTree
    }

    /// <summary>
    /// Filters for the inspection
    /// </summary>
    public enum InspectionsFilter
    {
        /// <summary>
        /// All inspections
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "AllInspections")]
        AllInspections,

        /// <summary>
        /// Oustanding inspections.
        /// </summary>
        [EnumValueData(Name = "Inspection With Finding Outstanding", KeyValue = "Outstanding")]
        Outstanding,

        /// <summary>
        /// Overdue inspections.
        /// </summary>
        [EnumValueData(Name = "Inspection With Finding Overdue", KeyValue = "Overdue")]
        Overdue,

        /// <summary>
        /// The Complete
        /// </summary>
        [EnumValueData(Name = "Pending Closure", KeyValue = "Complete")]
        Complete,

        /// <summary>
        /// The Closed
        /// </summary>
        [EnumValueData(Name = "Closed", KeyValue = "Closed")]
        Closed
    }

    /// <summary>
    /// Inspection finding types
    /// </summary>
    public enum InspectionFindingType
    {
        /// <summary>
        /// The deficiency
        /// </summary>
        [EnumValueData(Name = "Deficiency", KeyValue = "DEF")]
        Deficiency,

        /// <summary>
        /// The non conformance report
        /// </summary>
        [EnumValueData(Name = "Non Conform. Report", KeyValue = "NCR")]
        NonConformanceReport,

        /// <summary>
        /// The observation
        /// </summary>
        [EnumValueData(Name = "Observation", KeyValue = "OBS")]
        Observation,

        /// <summary>
        /// The recommendation
        /// </summary>
        [EnumValueData(Name = "Recommendation", KeyValue = "REC")]
        Recommendation
    }

    /// <summary>
    /// PO Stage Filter
    /// </summary>
    public enum PoStagesFilter
    {
        /// <summary>
        /// The in process
        /// </summary>
        [EnumValueData(Name = "In Progress", KeyValue = "InProcess")]
        InProcess,

        /// <summary>
        /// The ordered
        /// </summary>
        [EnumValueData(Name = "Ordered", KeyValue = "Ordered")]
        Ordered,

        /// <summary>
        /// The dispatched
        /// </summary>
        [EnumValueData(Name = "Dispatched", KeyValue = "Dispatched")]
        Dispatched,

        /// <summary>
        /// The authentication required
        /// </summary>
        [EnumValueData(Name = "Auth Enq", KeyValue = "AuthenticationRequired")]
        AuthenticationRequired,

        /// <summary>
        /// The received30 days
        /// </summary>
        [EnumValueData(Name = "Received (30 Days)", KeyValue = "Received30Days")]
        Received30Days,

        /// <summary>
        /// The authorisation required
        /// </summary>
        [EnumValueData(Name = "Auth Required", KeyValue = "AuthorisationRequired")]
        AuthorisationRequired,

        /// <summary>
        /// The tender awaiting authorization
        /// </summary>
        [EnumValueData(Name = "Tender Awaiting Authorization", KeyValue = "TenderAwaitingAuthorization")]
        TenderAwaitingAuthorization,

        /// <summary>
        /// The requisitions
        /// </summary>
        [EnumValueData(Name = "Requisitions", KeyValue = "Requisitions")]
        Requisitions,

        /// <summary>
        /// The enquiries
        /// </summary>
        [EnumValueData(Name = "Enquiries", KeyValue = "Enquiries")]
        Enquiries,

        /// <summary>
        /// The enquiries
        /// </summary>
        [EnumValueData(Name = "OnHold", KeyValue = "OnHold")]
        OnHold,
    }

    /// <summary>
    /// Vessel certificate - Staistics - on right top in UI
    /// </summary>
    public enum VesselCertificateStatistic
    {
        /// <summary>
        /// The total active
        /// </summary>
        [EnumValueData(Name = "Total Active", KeyValue = null)]
        TotalActive,

        /// <summary>
        /// The overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = null)]
        Overdue,

        /// <summary>
        /// The expiring in30 days
        /// </summary>
        [EnumValueData(Name = "Expiring in 30 Days", KeyValue = "29")]
        ExpiringIn30Days,

        /// <summary>
        /// The within survey range
        /// </summary>
        [EnumValueData(Name = "Within Survey Range", KeyValue = null)]
        WithinSurveyRange,

        /// <summary>
        /// The within survey range
        /// </summary>
        [EnumValueData(Name = "Stop sailing and trading Expiring in 30days", KeyValue = "StopSailingAndTradingExpiring30Days")]
        StopSailingAndTradingExpiring30Days
    }

    /// <summary>
    /// 
    /// </summary>
    public enum VesselCertificates
    {
        /// <summary>
        /// The total active
        /// </summary>
        [EnumValueData(Name = "Total Active", KeyValue = "All")]
        TotalActive,

        /// <summary>
        /// The overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue,

        /// <summary>
        /// The expiring in30 days
        /// </summary>
        [EnumValueData(Name = "Expiring in 30 Days", KeyValue = "Expire30Days")]
        ExpiringIn30Days,

        /// <summary>
        /// The within survey range
        /// </summary>
        [EnumValueData(Name = "Within Survey Range", KeyValue = "SurveyRange")]
        WithinSurveyRange,

        /// <summary>
        /// The within survey range
        /// </summary>
        [EnumValueData(Name = "Stop Sailing / Trading Expiring in 30 days in Window", KeyValue = "StopSailingAndTradingExpiring30Days")]
        StopSailingAndTradingExpiring30Days
    }

    /// <summary>
    /// Certificate Range Type
    /// </summary>
    public enum CertificateRangeType
    {
        /// <summary>
        /// The over due
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        OverDue,

        /// <summary>
        /// The is due
        /// </summary>
        [EnumValueData(Name = "Due", KeyValue = "Due")]
        IsDue,

        /// <summary>
        /// The is in range
        /// </summary>
        [EnumValueData(Name = "In Range", KeyValue = "InRange")]
        IsInRange,

        /// <summary>
        /// All due
        /// </summary>
        [EnumValueData(Name = "All Due", KeyValue = "AllDue")]
        AllDue,

        /// <summary>
        /// The in range and overdue
        /// </summary>
        [EnumValueData(Name = "In Range & Overdue", KeyValue = "InRangeAndOverdue")]
        InRangeAndOverdue,

        /// <summary>
        /// The due now
        /// </summary>
        [EnumValueData(Name = "Due Now", KeyValue = "DueNow")]
        DueNow,

        /// <summary>
        /// The within survey range
        /// </summary>
        [EnumValueData(Name = "Within Survey Range", KeyValue = "WithinSurveyRange")]
        WithinSurveyRange,

        /// <summary>
        /// Total.
        /// </summary>
        [EnumValueData(Name = "Total", KeyValue = "Total")]
        Total,

        /// <summary>
        /// Unsynced.
        /// </summary>
        [EnumValueData(Name = "Unsynced", KeyValue = "Unsynced")]
        Unsynced,
    }

    /// <summary>
    /// Enumeration for Certificate Impact
    /// </summary>
    public enum CertificateImpact
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,

        /// <summary>
        /// The stop sailing
        /// </summary>
        [EnumValueData(Name = "Stop Sailing", KeyValue = "GLAS00000001")]
        StopSailing,

        /// <summary>
        /// The stop trading
        /// </summary>
        [EnumValueData(Name = "Stop Trading", KeyValue = "GLAS00000002")]
        StopTrading,

        /// <summary>
        /// The no impact
        /// </summary>
        [EnumValueData(Name = "No Impact", KeyValue = null)]
        NoImpact
    }

    /// <summary>
    /// Certificate Type
    /// </summary>
    public enum CertificateType
    {
        /// <summary>
        /// The default. Will be used when no certificate type is specified
        /// </summary>
        [EnumValueData(Name = "", KeyValue = null)]
        Default,

        /// <summary>
        /// The Client
        /// </summary>
        [EnumValueData(Name = "Permanent", KeyValue = "Permanent")]
        Permanent,

        /// <summary>
        /// The Manager
        /// </summary>
        [EnumValueData(Name = "Renewal", KeyValue = "Renewal")]
        Renewal
    }

    /// <summary>
    /// The Report Chart Type
    /// </summary>
    public enum ReportDefinitionType
    {
        /// <summary>
        /// The c
        /// </summary>
        [EnumValueData(Name = "Client", KeyValue = "C")]
        C,

        /// <summary>
        /// The s
        /// </summary>
        [EnumValueData(Name = "Std.", KeyValue = "S")]
        S
    }

    /// <summary>
    /// Enumeration to hold Transaction Status
    /// </summary>
    public enum TransactionState
    {
        /// <summary>
        /// The close
        /// </summary>
        [EnumValueData(Name = "Closed", KeyValue = "0")]
        Close = 0,
        /// <summary>
        /// The open
        /// </summary>
        [EnumValueData(Name = "Opened", KeyValue = "1")]
        Open = 1,
        /// <summary>
        /// All i.e Open And Close
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "2")]
        All = 2
    }

    /// <summary>
    /// Enumeration to hold Auxiliary Fields
    /// </summary>
    public enum Auxiliary
    {
        /// <summary>
        /// The claims
        /// </summary>
        [EnumValueData(Name = "Claims", KeyValue = "Claims")]
        Claims,
        /// <summary>
        /// The seasonal
        /// </summary>
        [EnumValueData(Name = "Seasonal", KeyValue = "Seasonal")]
        Seasonal,
        /// <summary>
        /// The nationality
        /// </summary>
        [EnumValueData(Name = "Nationality", KeyValue = "Nationality")]
        Nationality,
        /// <summary>
        /// The rank
        /// </summary>
        [EnumValueData(Name = "Rank", KeyValue = "Rank")]
        Rank,
        /// <summary>
        /// The vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "Vessel")]
        Vessel,
        /// <summary>
        /// The general1
        /// </summary>
        [EnumValueData(Name = "General 1", KeyValue = "General1")]
        General1,
        /// <summary>
        /// The general3
        /// </summary>
        [EnumValueData(Name = "General 3", KeyValue = "General3")]
        General3,
        /// <summary>
        /// The entity vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "EntityVessel")]
        EntityVessel,
        /// <summary>
        /// The department
        /// </summary>
        [EnumValueData(Name = "Department", KeyValue = "Department")]
        Department,
        /// <summary>
        /// The employee
        /// </summary>
        [EnumValueData(Name = "Employee", KeyValue = "Employee")]
        Employee,
        /// <summary>
        /// The project
        /// </summary>
        [EnumValueData(Name = "Project", KeyValue = "Project")]
        Project,
        /// <summary>
        /// The group
        /// </summary>
        [EnumValueData(Name = "Group", KeyValue = "Group")]
        Group,
        /// <summary>
        /// The expense
        /// </summary>
        [EnumValueData(Name = "Expense", KeyValue = "Expense")]
        Expense,
        /// <summary>
        /// The aux7
        /// </summary>
        [EnumValueData(Name = "Aux7", KeyValue = "Aux7")]
        Aux7,
        /// <summary>
        /// The aux8
        /// </summary>
        [EnumValueData(Name = "Aux8", KeyValue = "Aux8")]
        Aux8,
        /// <summary>
        /// The aux9
        /// </summary>
        [EnumValueData(Name = "Aux9", KeyValue = "Aux9")]
        Aux9
    }

    /// <summary>
    /// Planned manitenance stages
    /// </summary>
    public enum PlannedMaintenanceStages
    {

        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,

        /// <summary>
        /// The due
        /// </summary>
        [EnumValueData(Name = "Due", KeyValue = "Due")]
        Due,

        /// <summary>
        /// The over due current month
        /// </summary>
        [EnumValueData(Name = "OverDueCurrentMonth", KeyValue = "OverDueCurrentMonth")]
        OverDueCurrentMonth,

        /// <summary>
        /// The over due previous month
        /// </summary>
        [EnumValueData(Name = "OverDuePreviousMonth", KeyValue = "OverDuePreviousMonth")]
        OverDuePreviousMonth,

        /// <summary>
        /// The done
        /// </summary>
        [EnumValueData(Name = "Done", KeyValue = "Done")]
        Done
    }

    /// <summary>
    /// Work basket - Job status
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// The scheduled job
        /// </summary>
        [EnumValueData(Name = "SJ/ST", KeyValue = "GLAS00000001")]
        ScheduledJob,
        /// <summary>
        /// The work order
        /// </summary>
        [EnumValueData(Name = "WO", KeyValue = "GLAS00000002")]
        WorkOrder,

        /// <summary>
        /// The reported work order
        /// </summary>
        [EnumValueData(Name = "RP", KeyValue = "GLAS00000003")]
        ReportedWorkOrder,

        /// <summary>
        /// The completed work order
        /// </summary>
        [EnumValueData(Name = "CP", KeyValue = "GLAS00000004")]
        CompletedWorkOrder,
        /// <summary>
        /// The closed
        /// </summary>
        [EnumValueData(Name = "CLS", KeyValue = "GLAS00000005")]
        Closed,
        /// <summary>
        /// The deferredfrom ship
        /// </summary>
        [EnumValueData(Name = "SDEF", KeyValue = "GLAS00000006")]
        DeferredfromShip,
        /// <summary>
        /// The deferred approved
        /// </summary>
        [EnumValueData(Name = "ADEF", KeyValue = "GLAS00000007")]
        DeferredApproved,
        /// <summary>
        /// The unplanned work order
        /// </summary>
        [EnumValueData(Name = "SWO", KeyValue = "GLAS00000008")]
        ShipsWorkOrder,
        /// <summary>
        /// The unplanned task shore staff
        /// </summary>
        [EnumValueData(Name = "UShor", KeyValue = "GLAS00000009")]
        UnplannedTaskShoreStaff,
        /// <summary>
        /// The moveto layup request
        /// </summary>
        [EnumValueData(Name = "RLP", KeyValue = "GLAS00000010")]
        MovetoLayupRequest,
        /// <summary>
        /// The approved layup
        /// </summary>
        [EnumValueData(Name = "ALP", KeyValue = "GLAS00000011")]
        ApprovedLayup,
        /// <summary>
        /// The moveto drydock request
        /// </summary>
        [EnumValueData(Name = "RDD", KeyValue = "GLAS00000012")]
        MovetoDrydockRequest,
        /// <summary>
        /// The approved drydock
        /// </summary>
        [EnumValueData(Name = "ADD", KeyValue = "GLAS00000013")]
        ApprovedDrydock,
        /// <summary>
        /// The cancelled
        /// </summary>
        [EnumValueData(Name = "CAN", KeyValue = "GLAS00000014")]
        Cancelled,

        /// <summary>
        /// The re opened work order
        /// </summary>
        [EnumValueData(Name = "RO", KeyValue = "GLAS00000015")]
        ReOpenedWorkOrder,

        /// <summary>
        /// The reschedule requested
        /// </summary>
        [EnumValueData(Name = "RS", KeyValue = "GLAS00000016")]
        RescheduleRequested,

        /// <summary>
        /// The defect work order
        /// </summary>
        [EnumValueData(Name = "DWO", KeyValue = "GLAS00000017")]
        DefectWorkOrder
    }
    /// <summary>
    /// Enum used to list the interval type in job description tab
    /// </summary>
    public enum ScheduleJobIntervalType
    {
        /// <summary>
        /// The counter based
        /// </summary>
        [EnumValueData(Name = "Counter Based", KeyValue = "GLAS00000011")]
        CounterBased = 0,

        /// <summary>
        /// The calendar based
        /// </summary>
        [EnumValueData(Name = "Calender Based", KeyValue = "GLAS00000012")]
        CalendarBased = 1,

        /// <summary>
        /// The calendar or single counter
        /// </summary>
        [EnumValueData(Name = "Calendar or Single Counter", KeyValue = "GLAS00000013")]
        CalendarOrSingleCounter = 2,

        /// <summary>
        /// The calendar or multiple counters
        /// </summary>
        [EnumValueData(Name = "Calendar or Multiple Counters", KeyValue = "GLAS00000015")]
        CalendarOrMultipleCounters = 3,

        /// <summary>
        /// The multiple counters
        /// </summary>
        [EnumValueData(Name = "Multiple Counters", KeyValue = "GLAS00000016")]
        MultipleCounters = 4,

        /// <summary>
        /// The continuous monitoring
        /// </summary>
        [EnumValueData(Name = "Continuous Monitoring", KeyValue = "GLAS00000017")]
        ContinuousMonitoring = 5,

        /// <summary>
        /// The running hours range
        /// </summary>
        [EnumValueData(Name = "Running Hours Range", KeyValue = "GLAS00000018")]
        RunningHoursRange = 6,

        /// <summary>
        /// The trigger based
        /// </summary>
        [EnumValueData(Name = "Standing", KeyValue = "GLAS00000023")]
        TriggerBased = 7,

        /// <summary>
        /// The calendar range.
        /// </summary>
        [EnumValueData(Name = "Calendar Range", KeyValue = "GLAS00000024")]
        CalendarRange = 8
    }

    /// <summary>
    /// Job interval type
    /// </summary>
    public enum JobIntervalType
    {
        /// <summary>
        /// The month
        /// </summary>
        [EnumValueData(Name = "Month", KeyValue = "SYST00000001")]
        Month,
        /// <summary>
        /// The week
        /// </summary>
        [EnumValueData(Name = "Week", KeyValue = "SYST00000002")]
        Week,
        /// <summary>
        /// The year
        /// </summary>
        [EnumValueData(Name = "Year", KeyValue = "SYST00000003")]
        Year,
        /// <summary>
        /// The running hours
        /// </summary>
        [EnumValueData(Name = "Hours", KeyValue = "SYST00000004")]
        RunningHours,
        /// <summary>
        /// The revolutions
        /// </summary>
        [EnumValueData(Name = "Revolution", KeyValue = "SYST00000005")]
        Revolutions,
        /// <summary>
        /// The events
        /// </summary>
        [EnumValueData(Name = "Event(s)", KeyValue = "SYST00000006")]
        Events
    }

    /// <summary>
    /// Enumeration to get the work order reschedule status
    /// </summary>
    public enum WorkOrderRescheduleStatus
    {
        /// <summary>
        /// The pending
        /// </summary>
        [EnumValueData(Name = "Pending", KeyValue = "SYST00000007")]
        Pending,

        /// <summary>
        /// The approved
        /// </summary>
        [EnumValueData(Name = "Approved", KeyValue = "SYST00000008")]
        Approved,

        /// <summary>
        /// The rejected
        /// </summary>
        [EnumValueData(Name = "Rejected", KeyValue = "SYST00000009")]
        Rejected,

        /// <summary>
        /// The draft.
        /// </summary>
        [EnumValueData(Name = "Draft", KeyValue = "SYST00000159")]
        Draft,

        /// <summary>
        /// The revised.
        /// </summary>
        [EnumValueData(Name = "Revised", KeyValue = "SYST00000160")]
        Revised,

        /// <summary>
        /// The acknowledged.
        /// </summary>
        [EnumValueData(Name = "Acknowledged", KeyValue = "SYST00000161")]
        Acknowledged,

        /// <summary>
        /// The cancelled.
        /// </summary>
        [EnumValueData(Name = "Cancelled", KeyValue = "SYST00000162")]
        Cancelled
    }

    /// <summary>
    /// A generic way to indicate Key Performance Indicators.
    /// </summary>
    public enum KPI
    {
        /// <summary>
        /// Normal performance.
        /// </summary>
        Normal,

        /// <summary>
        /// Good performance.
        /// </summary>
        Good,

        /// <summary>
        /// Better performance
        /// </summary>
        Better,

        /// <summary>
        /// Best performance
        /// </summary>
        Best,

        /// <summary>
        /// Excellent performance
        /// </summary>
        Excellent,

        /// <summary>
        /// Pre warning.
        /// </summary>
        PreWarning,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning,

        /// <summary>
        /// Critical.
        /// </summary>
        Critical,

        /// <summary>
        /// Defines Poor state
        /// </summary>
        Poor
    }

    /// <summary>
    /// Describes the status of an entry in the position list
    /// </summary>
    public enum PositionListEventStatus
    {
        /// <summary>
        /// The voyage arrived
        /// </summary>
        VoyageArrived,
        /// <summary>
        /// Vessel has arrived at the port berth.
        /// </summary>
        BerthArrived,
        /// <summary>
        /// Vessel has departed the port berth.
        /// </summary>
        BerthDeparted,
        /// <summary>
        /// Vessel has vacated the port.  Also used to indicate sea passage departure.
        /// </summary>
        VoyageDeparted
    }

    /// <summary>
    /// Enumeration to hold Chart Detail Currency Type
    /// </summary>
    public enum ChartDetailCurrencyType
    {
        /// <summary>
        /// The base currency
        /// </summary>
        [EnumValueData(Name = "Base currency", KeyValue = "0")]
        BaseCurrency,

        /// <summary>
        /// The single currency
        /// </summary>
        [EnumValueData(Name = "Single currency", KeyValue = "1")]
        SingleCurrency,

        /// <summary>
        /// The multi currency
        /// </summary>
        [EnumValueData(Name = "Multi currency", KeyValue = "2")]
        MultiCurrency
    }

    /// <summary>
    /// Enumeration to hold Chart Detail Account Type
    /// </summary>
    public enum ChartDetailAccountType
    {
        /// <summary>
        /// The asset
        /// </summary>
        [EnumValueData(Name = "Asset", KeyValue = "1")]
        Asset = 1,
        /// <summary>
        /// The liability
        /// </summary>
        [EnumValueData(Name = "Liability", KeyValue = "2")]
        Liability,
        /// <summary>
        /// The income
        /// </summary>
        [EnumValueData(Name = "Income", KeyValue = "3")]
        Income,
        /// <summary>
        /// The expense
        /// </summary>
        [EnumValueData(Name = "Expense", KeyValue = "4")]
        Expense
    }

    /// <summary>
    /// Position list  Date Status
    /// </summary>
    public enum PositionListDateStatus
    {
        /// <summary>
        /// ACT
        /// </summary>
        [EnumValueData(Name = "ACT", KeyValue = "ACT")]
        ACT,

        /// <summary>
        /// EST
        /// </summary>
        [EnumValueData(Name = "EST", KeyValue = "EST")]
        EST
    }

    /// <summary>
    /// Enum for work order indication type.
    /// </summary>
    public enum WorkOrderIndicationType
    {
        /// <summary>
        /// The CBM triggered.
        /// </summary>
        [EnumValueData(Name = "CBM Triggered", KeyValue = "SYST00000035")]
        CBMTriggered,

        /// <summary>
        /// The defect.
        /// </summary>
        [EnumValueData(Name = "Defect", KeyValue = "SYST00000036")]
        Defect,

        /// <summary>
        /// The round.
        /// </summary>
        [EnumValueData(Name = "Round", KeyValue = "SYST00000037")]
        Round,

        /// <summary>
        /// The standing work order.
        /// </summary>
        [EnumValueData(Name = "Standing Work Order", KeyValue = "SYST00000038")]
        StandingWorkOrder,

        /// <summary>
        /// The certificate.
        /// </summary>
        [EnumValueData(Name = "Certificate Work Order", KeyValue = "SYST00000047")]
        Certificate
    }

    /// <summary>
    /// The work order symptom
    /// </summary>
    public enum WorkOrderSymptom
    {
        /// <summary>
        /// The other
        /// </summary>
        [EnumValueData(Name = "Other", KeyValue = "GLAS00000011")]
        Other
    }

    /// <summary>
    /// Defect stages
    /// </summary>
    public enum DefectStages
    {

        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,

        /// <summary>
        /// The due
        /// </summary>
        [EnumValueData(Name = "Due", KeyValue = "Due")]
        Due,

        /// <summary>
        /// The over due
        /// </summary>
        [EnumValueData(Name = "OverDue", KeyValue = "OverDue")]
        OverDue,

        /// <summary>
        /// The open tech defect
        /// </summary>
        [EnumValueData(Name = "OpenTechDefect", KeyValue = "OpenTechDefect")]
        OpenTechDefect,

        /// <summary>
        /// The closed tech defect
        /// </summary>
        [EnumValueData(Name = "ClosedTechDefect", KeyValue = "ClosedTechDefect")]
        ClosedTechDefect,

        /// <summary>
        /// The off hire required
        /// </summary>
        [EnumValueData(Name = "OffHireRequired", KeyValue = "OffHireRequired")]
        OffHireRequired,

        /// <summary>
        /// The defect with open orders
        /// </summary>
        [EnumValueData(Name = "DefectWithOpenOrders", KeyValue = "DefectWithOpenOrders")]
        DefectWithOpenOrders
    }

    /// <summary>
    /// 
    /// </summary>
    public enum DefectManagerStages
    {
        /// <summary>
        /// ClosedDefect
        /// </summary>
        [EnumValueData(Name = "Closed (Last 12M)", KeyValue = "ClosedDefect")]
        ClosedDefect,

        /// <summary>
        /// OpenDefect
        /// </summary>
        [EnumValueData(Name = "Open", KeyValue = "OpenDefect")]
        OpenDefect,

        /// <summary>
        /// Overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue,

        /// <summary>
        /// OffHire
        /// </summary>
        [EnumValueData(Name = "Off Hire", KeyValue = "OffHire")]
        OffHire,

        /// <summary>
        /// Layover
        /// </summary>
        [EnumValueData(Name = "Layover", KeyValue = "Layover")]
        Layover,

        /// <summary>
        /// Drydock
        /// </summary>
        [EnumValueData(Name = "Drydock", KeyValue = "Drydock")]
        Drydock,

        /// <summary>
        /// TechnicalDefect
        /// </summary>
        [EnumValueData(Name = "Technical", KeyValue = "TechnicalDefect")]
        TechnicalDefect,

        /// <summary>
        /// GuaranteeClaim
        /// </summary>
        [EnumValueData(Name = "Guarantee Claim", KeyValue = "GuaranteeClaim")]
        GuaranteeClaim,

        /// <summary>
        /// The awaiting spares
        /// </summary>
        [EnumValueData(Name = "Awaiting Spares", KeyValue = "AwaitingSpares")]
        AwaitingSpares,

        /// <summary>
        /// The awaiting spares
        /// </summary>
        [EnumValueData(Name = "Completed", KeyValue = "Completed")]
        Completed,
    }

    /// <summary>
    /// Enum for defect work order status
    /// </summary>
    public enum DefectWorkOrderStatus
    {
        /// <summary>
        /// The work order
        /// </summary>
        [EnumValueData(Name = "DWO", KeyValue = "GLAS00000029")]
        DefectWorkOrder,

        /// <summary>
        /// The completed
        /// </summary>
        [EnumValueData(Name = "CP", KeyValue = "GLAS00000030")]
        Completed,

        /// <summary>
        /// The close
        /// </summary>
        [EnumValueData(Name = "CLS", KeyValue = "GLAS00000031")]
        Close,

        /// <summary>
        /// The reopen
        /// </summary>
        [EnumValueData(Name = "RO", KeyValue = "GLAS00000032")]
        Reopen,

        /// <summary>
        /// The reschedule
        /// </summary>
        [EnumValueData(Name = "RS", KeyValue = "GLAS00000033")]
        Reschedule,

        /// <summary>
        /// The rejected
        /// </summary>
        [EnumValueData(Name = "RJ", KeyValue = "GLAS00000045")]
        Rejected,

        /// <summary>
        /// The claim accepted
        /// </summary>
        [EnumValueData(Name = "CA", KeyValue = "GLAS00000046")]
        ClaimAccepted
    }

    /// <summary>
    /// This enum is used for inspection manager overview filter.
    /// </summary>
    public enum InspectionManagerOverviewFilter
    {
        /// <summary>
        /// The inspection manager due in days
        /// </summary>
        [EnumValueData(Name = "Inspection Manager Due In Days Filter", KeyValue = "GLAS00000140")]
        InspectionManagerDueInDays
    }

    /// <summary>
    /// Port Event Type
    /// </summary>
    public enum PortEventType
    {
        /// <summary>
        /// The eosp
        /// </summary>
        [EnumValueData(Name = "End of Sea Passage (EOSP)", KeyValue = "001")]
        EOSP,
        /// <summary>
        /// The faop
        /// </summary>
        [EnumValueData(Name = "Full Away Of Passage (FAOP)", KeyValue = "033")]
        FAOP,

        /// <summary>
        /// The berthed
        /// </summary>
        [EnumValueData(Name = "Berthed All Fast", KeyValue = "005")]
        BERTHED,

        /// <summary>
        /// The unberthed
        /// </summary>
        [EnumValueData(Name = "Left Berth", KeyValue = "027")]
        UNBERTHED,

        /// <summary>
        /// The tugarrival
        /// </summary>
        [EnumValueData(Name = "Tugs In On Arrival", KeyValue = "021")]
        TUGARRIVAL,

        /// <summary>
        /// The tugdeparture
        /// </summary>
        [EnumValueData(Name = "Tugs Out On Departure", KeyValue = "034")]
        TUGDEPARTURE,

        /// <summary>
        /// The load cargo commenced
        /// </summary>
        [EnumValueData(Name = "Loading Commenced/Completed", KeyValue = "016")]
        LoadCargoCommenced,

        /// <summary>
        /// The discharge cargo commenced
        /// </summary>
        [EnumValueData(Name = "Discharging Commenced/Completed", KeyValue = "017")]
        DischargeCargoCommenced,

        /// <summary>
        /// The bunker commenced
        /// </summary>
        [EnumValueData(Name = "Bunkering commenced/completed", KeyValue = "073")]
        BunkerCommenced,

        /// <summary>
        /// The port noon report
        /// </summary>
        [EnumValueData(Name = "Port Noon Report", KeyValue = "077")]
        PortNoonReport,

        /// <summary>
        /// The finished with engines
        /// </summary>
        [EnumValueData(Name = "Finished with Engine", KeyValue = "078")]
        FinishedWithEngines,

        /// <summary>
        /// The stand by engines
        /// </summary>
        [EnumValueData(Name = "Stand by Engine", KeyValue = "079")]
        StandByEngines,

        /// <summary>
        /// The stand by engines
        /// </summary>
        [EnumValueData(Name = "Midnight Report", KeyValue = "074")]
        MidnightReport,

        /// <summary>
        /// The anchorage commenced or completed
        /// </summary>
        [EnumValueData(Name = "Anchorage Commenced/Completed", KeyValue = "056")]
        AnchorageCommencedOrCompleted,

        /// <summary>
        /// The river passage commenced or completed
        /// </summary>
        [EnumValueData(Name = "River Passage Commenced/Completed", KeyValue = "060")]
        RiverPassageCommencedOrCompleted,

        /// <summary>
        /// The canal passage commenced or completed
        /// </summary>
        [EnumValueData(Name = "Canal Passage Commenced/Completed", KeyValue = "061")]
        CanalPassageCommencedOrCompleted,

        /// <summary>
        /// The STS
        /// </summary>
        [EnumValueData(Name = "Ship to Ship Transfer (STS)", KeyValue = "071")]
        STS,

        /// <summary>
        /// The maneouvring
        /// </summary>
        [EnumValueData(Name = "Maneouvring Commenced/Completed", KeyValue = "00M")]
        Maneouvring
    }

    /// <summary>
    /// Enumeration for Vessel Agent Type
    /// </summary>
    public enum VesselAgentType
    {
        /// <summary>
        /// The default. Will be used when no vessel agent type is specified
        /// </summary>
        [EnumValueData(Name = "", KeyValue = null)]
        Default,

        /// <summary>
        /// The charterer
        /// </summary>
        [EnumValueData(Name = "Charterer", KeyValue = "C")]
        Charterer,

        /// <summary>
        /// The owner
        /// </summary>
        [EnumValueData(Name = "Owner", KeyValue = "O")]
        Owner,

        /// <summary>
        /// The blank
        /// </summary>
        [EnumValueData(Name = "", KeyValue = "")]
        Blank
    }

    /// <summary>
    /// Submodules used for documents
    /// </summary>
    public enum SubModule
    {
        /// <summary>
        /// The charter
        /// </summary>
        [EnumValueData(Name = "Charter", KeyValue = "GLAS00000001")]
        Charter,
        /// <summary>
        /// The port event
        /// </summary>
        [EnumValueData(Name = "PortEvent", KeyValue = "GLAS00000002")]
        PortEvent,
        /// <summary>
        /// The bunker
        /// </summary>
        [EnumValueData(Name = "Bunker", KeyValue = "GLAS00000003")]
        Bunker,
        /// <summary>
        /// The position list activity
        /// </summary>
        [EnumValueData(Name = "PositionListActivity", KeyValue = "GLAS00000004")]
        PositionListActivity,
        /// <summary>
        /// The vessel general arrangement
        /// </summary>
        [EnumValueData(Name = "VesselGeneralArrangement", KeyValue = "GLAS00000005")]
        VesselGeneralArrangement,
        /// <summary>
        /// The vessel certificate
        /// </summary>
        [EnumValueData(Name = "VesselCertificate", KeyValue = "GLAS00000006")]
        VesselCertificate,
        /// <summary>
        /// The garbage
        /// </summary>
        [EnumValueData(Name = "Garbage", KeyValue = "GLAS00000007")]
        Garbage,
        /// <summary>
        /// The sewage
        /// </summary>
        [EnumValueData(Name = "Sewage", KeyValue = "GLAS00000008")]
        Sewage,
        /// <summary>
        /// The ozone
        /// </summary>
        [EnumValueData(Name = "Ozone", KeyValue = "GLAS00000009")]
        Ozone,
        /// <summary>
        /// The oil
        /// </summary>
        [EnumValueData(Name = "Oil", KeyValue = "GLAS00000010")]
        Oil,
        /// <summary>
        /// The job
        /// </summary>
        [EnumValueData(Name = "Job", KeyValue = "GLAS00000011")]
        Job,
        /// <summary>
        /// The schedule task
        /// </summary>
        [EnumValueData(Name = "ScheduleTaskComponentDetail", KeyValue = "GLAS00000012")]
        ScheduleTaskComponentDetail,
        /// <summary>
        /// The work order history
        /// </summary>
        [EnumValueData(Name = "WorkOrderHistory", KeyValue = "GLAS00000013")]
        WorkOrderHistory,
        /// <summary>
        /// The component
        /// </summary>
        [EnumValueData(Name = "Component", KeyValue = "GLAS00000014")]
        Component,
        /// <summary>
        /// The spare
        /// </summary>
        [EnumValueData(Name = "Spare", KeyValue = "GLAS00000015")]
        Spare,
        /// <summary>
        /// The defect manager
        /// </summary>
        [EnumValueData(Name = "DefectWorkOrder", KeyValue = "GLAS00000016")]
        DefectWorkOrder,
        /// <summary>
        /// The management of change
        /// </summary>
        [EnumValueData(Name = "ManagementOfChange", KeyValue = "GLAS00000017")]
        ManagementOfChange,
        /// <summary>
        /// The job safety analysis
        /// </summary>
        [EnumValueData(Name = "JobSafetyAnalysis", KeyValue = "GLAS00000018")]
        JobSafetyAnalysis,
        /// <summary>
        /// The defect report work order
        /// </summary>
        [EnumValueData(Name = "DefectReportWorkOrder", KeyValue = "GLAS00000019")]
        DefectReportWorkOrder,
        /// <summary>
        /// The schedule task measurement standards
        /// </summary>
        [EnumValueData(Name = "ScheduleTaskMeasurementStandards", KeyValue = "GLAS00000020")]
        ScheduleTaskMeasurementStandards,
        /// <summary>
        /// The work order history component detail
        /// </summary>
        [EnumValueData(Name = "WorkOrderHistoryComponentDetail", KeyValue = "GLAS00000021")]
        WorkOrderHistoryComponentDetail,
        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "GLAS00000022")]
        HazOcc,
        /// <summary>
        /// The vessel Shared
        /// </summary>
        [EnumValueData(Name = "VesselShared", KeyValue = "GLAS00000023")]
        VesselShared,
        /// <summary>
        /// The vessel create requisition
        /// </summary>
        [EnumValueData(Name = "VesselCreateRequisition", KeyValue = "GLAS00000024")]
        VesselCreateRequisition,
        /// <summary>
        /// The office Shared
        /// </summary>
        [EnumValueData(Name = "OfficeShared", KeyValue = "GLAS00000025")]
        OfficeShared,
        /// <summary>
        /// The onboard crewing
        /// </summary>
        [EnumValueData(Name = "OnboardCrewing", KeyValue = "GLAS00000026")]
        OnboardCrewing,
        /// <summary>
        /// The inspection
        /// </summary>
        [EnumValueData(Name = "Inspection", KeyValue = "GLAS00000027")]
        Inspection,

        /// <summary>
        /// The insurance
        /// </summary>
        [EnumValueData(Name = "Insurance", KeyValue = "GLAS00000028")]
        Insurance,

        /// <summary>
        /// The hotel defect manager
        /// </summary>
        [EnumValueData(Name = "HotelDefectManager", KeyValue = "GLAS00000031")]
        HotelDefectManager,

        /// <summary>
        /// The vessel technical document
        /// </summary>
        [EnumValueData(Name = "VesselTechnicalDocument", KeyValue = "GLAS00000032")]
        VesselTechnicalDocument,

        /// <summary>
        /// The weekly minutes
        /// </summary>
        [EnumValueData(Name = "WeeklyMinutes", KeyValue = "GLAS00000033")]
        WeeklyMinutes,

        /// <summary>
        /// The drills and campaigns maintainer
        /// </summary>
        [EnumValueData(Name = "Drills And Campaigns Maintainer", KeyValue = "GLAS00000035")]
        DrillsAndCampaignsMaintainer,

        /// <summary>
        /// The vessel drills
        /// </summary>
        [EnumValueData(Name = "Vessel Drills", KeyValue = "GLAS00000036")]
        VesselDrills,

        /// <summary>
        /// The report drills and campaigns
        /// </summary>
        [EnumValueData(Name = "Report Drills And Campaigns", KeyValue = "GLAS00000037")]
        ReportDrillsAndCampaigns,

        /// <summary>
        /// The catalogue item
        /// </summary>
        [EnumValueData(Name = "CatalogueItem", KeyValue = "GLAS00000034")]
        CatalogueItem,

        /// <summary>
        /// The hotel defect job.
        /// </summary>
        [EnumValueData(Name = "HotelDefectJob", KeyValue = "GLAS00000038")]
        HotelDefectJob,

        /// <summary>
        /// The hotel defect report work order
        /// </summary>
        [EnumValueData(Name = "HotelDefectReportWorkOrder", KeyValue = "GLAS00000039")]
        HotelDefectReportWorkOrder,

        /// <summary>
        /// The performance appraisal
        /// </summary>
        [EnumValueData(Name = "PerformanceAppraisal", KeyValue = "GLAS00000040")]
        PerformanceAppraisal,

        /// <summary>
        /// The catalog section
        /// </summary>
        [EnumValueData(Name = "CatalogSection", KeyValue = "GLAS00000041")]
        CatalogSection,

        /// <summary>
        /// The reschedule work order.
        /// </summary>
        [EnumValueData(Name = "RescheduleWorkOrder", KeyValue = "GLAS00000042")]
        RescheduleWorkOrder,

        /// <summary>
        /// The temporary crew document
        /// </summary>
        [EnumValueData(Name = "TempCrewDoc", KeyValue = "GLAS00000043")]
        TempCrewDoc,

        /// <summary>
        /// The moc guidelines.
        /// </summary>
        [EnumValueData(Name = "MOCGuidelines", KeyValue = "GLAS00000044")]
        MOCGuidelines,

        /// <summary>
        /// The Company
        /// </summary>
        [EnumValueData(Name = "Company", KeyValue = "GLAS00000045")]
        Company,

        /// <summary>
        /// The company compliance
        /// </summary>
        [EnumValueData(Name = "CompanyCompliance", KeyValue = "GLAS00000046")]
        CompanyCompliance,

        /// <summary>
        /// The company compliance
        /// </summary>
        [EnumValueData(Name = "PortPlanningActivity", KeyValue = "GLAS00000047")]
        PortPlanningActivity,

        /// <summary>
        /// The vessel inspectio section image
        /// </summary>
        [EnumValueData(Name = "VesselInspectioSectionImage", KeyValue = "GLAS00000048")]
        VesselInspectioSectionImage,

        /// <summary>
        /// The work order
        /// </summary>
        [EnumValueData(Name = "WorkOrder", KeyValue = "GLAS00000049")]
        WorkOrder,

        /// <summary>
        /// The vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "GLAS00000050")]
        Vessel,

        /// <summary>
        /// The port alert
        /// </summary>
        [EnumValueData(Name = "PortAlert", KeyValue = "GLAS00000051")]
        PortAlert,

        /// <summary>
        /// The vessel supplier quote
        /// </summary>
        [EnumValueData(Name = "VesselSupplierQuote", KeyValue = "GLAS00000057")]
        VesselSupplierQuote,
    }

    /// <summary>
    /// Sea Passage Activity type
    /// </summary>
    public enum SeaPassageActivityType
    {
        /// <summary>
        /// The faop
        /// </summary>
        [EnumValueData(Name = "F.A.O.P", KeyValue = "FAP")]
        FAOP,
        /// <summary>
        /// The noon
        /// </summary>
        [EnumValueData(Name = "NOON", KeyValue = "NN")]
        NOON,
        /// <summary>
        /// The eosp
        /// </summary>
        [EnumValueData(Name = "E.O.S.P", KeyValue = "ESP")]
        EOSP,

        /// <summary>
        /// The changedestination
        /// </summary>
        [EnumValueData(Name = "CHANGE DESTINATION", KeyValue = "CD")]
        CHANGEDESTINATION
    }

    /// <summary>
    /// On board crew ranks
    /// </summary>
    public enum OnBoardCrewRank
    {
        /// <summary>
        /// The master
        /// </summary>
        [EnumValueData(Name = "Master", KeyValue = "VSHP00000002")]
        Master,

        /// <summary>
        /// The chief engineer
        /// </summary>
        [EnumValueData(Name = "Chief Engineer", KeyValue = "VSHP00000068")]
        ChiefEngineer,

        /// <summary>
        /// The Chief Officer
        /// </summary>
        [EnumValueData(Name = "Chief Officer", KeyValue = "VSHP00000008")]
        ChiefOfficer,

        /// <summary>
        /// The 2nd Engineer
        /// </summary>
        [EnumValueData(Name = "2nd Engineer", KeyValue = "VSHP00000072")]
        SecondEngineer,

        /// <summary>
        /// The 4th Engineer
        /// </summary>
        [EnumValueData(Name = "4th Engineer", KeyValue = "VSHP00000079")]
        FourthEngineer
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HazoccDashboardType
    {
        /// <summary>
        /// The open items
        /// </summary>
        [EnumValueData(Name = "Open Items", KeyValue = "OpenItems")]
        OpenItems,

        /// <summary>
        /// The office rev
        /// </summary>
        [EnumValueData(Name = "Office Rev", KeyValue = "OfficeRev")]
        OfficeRev,

        /// <summary>
        /// The incident very serious
        /// </summary>
        [EnumValueData(Name = "IncidentVerySerious", KeyValue = "IncidentVerySerious")]
        IncidentVerySerious,

        /// <summary>
        /// The incident serious
        /// </summary>
        [EnumValueData(Name = "IncidentSerious", KeyValue = "IncidentSerious")]
        IncidentSerious,

        /// <summary>
        /// The incident moderate
        /// </summary>
        [EnumValueData(Name = "IncidentModerate", KeyValue = "IncidentModerate")]
        IncidentModerate,

        /// <summary>
        /// The incident minor
        /// </summary>
        [EnumValueData(Name = "IncidentMinor", KeyValue = "IncidentMinor")]
        IncidentMinor,

        /// <summary>
        /// The crew accident fatal
        /// </summary>
        [EnumValueData(Name = "CrewAccidentFatal", KeyValue = "CrewAccidentFatal")]
        CrewAccidentFatal,

        /// <summary>
        /// The crew accident LWC
        /// </summary>
        [EnumValueData(Name = "CrewAccidentLWC", KeyValue = "CrewAccidentLWC")]
        CrewAccidentLWC,

        /// <summary>
        /// The crew accident RWC
        /// </summary>
        [EnumValueData(Name = "CrewAccidentRWC", KeyValue = "CrewAccidentRWC")]
        CrewAccidentRWC,

        /// <summary>
        /// The crew accident MTC
        /// </summary>
        [EnumValueData(Name = "CrewAccidentMTC", KeyValue = "CrewAccidentMTC")]
        CrewAccidentMTC,

        /// <summary>
        /// The crew accident fac
        /// </summary>
        [EnumValueData(Name = "CrewAccidentFAC", KeyValue = "CrewAccidentFAC")]
        CrewAccidentFAC,

        /// <summary>
        /// The statistics lti
        /// </summary>
        [EnumValueData(Name = "StatisticsLTI", KeyValue = "StatisticsLTI")]
        StatisticsLTI,

        /// <summary>
        /// The statistics TRC
        /// </summary>
        [EnumValueData(Name = "StatisticsTRC", KeyValue = "StatisticsTRC")]
        StatisticsTRC,

        /// <summary>
        /// The statistics mexphs
        /// </summary>
        [EnumValueData(Name = "StatisticsMEXPHS", KeyValue = "StatisticsMEXPHS")]
        StatisticsMEXPHS,

        /// <summary>
        /// The near miss safe acts
        /// </summary>
        [EnumValueData(Name = "NearMissSafeActs", KeyValue = "NearMissSafeActs")]
        NearMissSafeActs,

        /// <summary>
        /// The near miss count
        /// </summary>
        [EnumValueData(Name = "NearMissCount", KeyValue = "NearMissCount")]
        NearMissCount,

        /// <summary>
        /// The near miss unsafe acts
        /// </summary>
        [EnumValueData(Name = "NearMissUnsafeActs", KeyValue = "NearMissUnsafeActs")]
        NearMissUnsafeActs,

        /// <summary>
        /// The near miss unsafe cond
        /// </summary>
        [EnumValueData(Name = "NearMissUnsafeCond", KeyValue = "NearMissUnsafeCond")]
        NearMissUnsafeCond,

        /// <summary>
        /// The passenger fatal
        /// </summary>
        [EnumValueData(Name = "PassengerFatal", KeyValue = "PassengerFatal")]
        PassengerFatal,

        /// <summary>
        /// The passenger MTC
        /// </summary>
        [EnumValueData(Name = "PassengerMTC", KeyValue = "PassengerMTC")]
        PassengerMTC,

        /// <summary>
        /// The passenger fac
        /// </summary>
        [EnumValueData(Name = "PassengerFAC", KeyValue = "PassengerFAC")]
        PassengerFAC
    }

    /// <summary>
    /// HazOcc Severity
    /// </summary>
    public enum HazOccReportSeverity
    {
        /// <summary>
        /// v Serious
        /// </summary>
        [EnumValueData(Name = "Major", KeyValue = "INCMAN000003")]
        VS = 1,

        /// <summary>
        /// Serious
        /// </summary>
        [EnumValueData(Name = "Severe", KeyValue = "INCMAN000004")]
        SR = 2,

        /// <summary>
        /// Moderate
        /// </summary>
        [EnumValueData(Name = "Moderate", KeyValue = "INCMAN000005")]
        MS = 3,

        /// <summary>
        /// Minor
        /// </summary>
        [EnumValueData(Name = "Minor", KeyValue = "INCMAN000006")]
        MN = 4,

        /// <summary>
        /// Disastrous
        /// </summary>
        [EnumValueData(Name = "Disastrous", KeyValue = "INCMAN000001")]
        DS = 5,

        /// <summary>
        /// Critical
        /// </summary>
        [EnumValueData(Name = "Critical", KeyValue = "INCMAN000002")]
        CR = 6,

        /// <summary>
        /// LOW
        /// </summary>
        [EnumValueData(Name = "Low", KeyValue = "INCMAN000009")]
        LW = 7,

        /// <summary>
        /// MEDIUM
        /// </summary>
        [EnumValueData(Name = "Medium", KeyValue = "INCMAN000008")]
        MD = 8,

        /// <summary>
        /// HIGH
        /// </summary>
        [EnumValueData(Name = "High", KeyValue = "INCMAN000007")]
        HI = 9
    }

    /// <summary>
    /// HazOcc Classification Types
    /// </summary>
    public enum HazOccClassCodes
    {
        /// <summary>
        /// First Aid.
        /// </summary>
        [EnumValueData(Name = "First Aid Case", KeyValue = "INCMAN000004")]
        FA = 1,

        /// <summary>
        /// Lost Time Injury.
        /// </summary>
        [EnumValueData(Name = "Lost Time Injury", KeyValue = "INCMAN000002")]
        LTI = 2,

        /// <summary>
        /// Fatality.
        /// </summary>
        [EnumValueData(Name = "Fatality", KeyValue = "INCMAN000001")]
        FT = 3,

        /// <summary>
        /// Medical Treatment Case.
        /// </summary>
        [EnumValueData(Name = "Medical Treatment Case", KeyValue = "INCMAN000003")]
        MT = 4,

        /// <summary>
        /// Security.
        /// </summary>
        [EnumValueData(Name = "Security", KeyValue = "INCMAN000024")]
        SC = 5,

        /// <summary>
        /// Unsafe Act.
        /// </summary>
        [EnumValueData(Name = "Unsafe Act", KeyValue = "INCMAN000088")]
        UA = 6,

        /// <summary>
        /// Unsafe Condition.
        /// </summary>
        [EnumValueData(Name = "Unsafe Condition", KeyValue = "INCMAN000089")]
        UC = 7,

        /// <summary>
        /// Lost WorkTime Case.
        /// </summary>
        [EnumValueData(Name = "Lost Workday Case", KeyValue = "INCMAN000021")]
        LW = 8,

        /// <summary>
        /// Permanent Total Disability.
        /// </summary>
        [EnumValueData(Name = "Permanent Total Disability", KeyValue = "INCMAN000009")]
        PT = 9,

        /// <summary>
        /// Permanent Partial Disability.
        /// </summary>
        [EnumValueData(Name = "Permanent Partial Disability", KeyValue = "INCMAN000010")]
        PP = 10,

        /// <summary>
        /// Safe Act.
        /// </summary>
        [EnumValueData(Name = "Safe Act", KeyValue = "INCMAN000091")]
        SA = 11,

        /// <summary>
        /// Safe Condition.
        /// </summary>
        [EnumValueData(Name = "Safe Condition", KeyValue = "INCMAN000092")]
        SD = 12,

        /// <summary>
        /// Restricted Work Case.
        /// </summary>
        [EnumValueData(Name = "Restricted Work Case", KeyValue = "INCMAN000005")]
        RC = 13,

        /// <summary>
        /// Medical Illness.
        /// </summary>
        [EnumValueData(Name = "Medical Illness", KeyValue = "INCMAN000095")]
        MI = 14,

        /// <summary>
        /// The mfat
        /// </summary>
        [EnumValueData(Name = "Medical Fatality", KeyValue = "INCMAN000097")]
        MFAT = 15,

        /// <summary>
        /// The fatal
        /// </summary>
        [EnumValueData(Name = "Fatality (medical)", KeyValue = "INCMAN000023")]
        Fatal = 16,
    }

    /// <summary>
    /// HazoccType
    /// </summary>
    public enum HazoccType
    {
        /// <summary>
        /// The near miss
        /// </summary>
        [EnumValueData(Name = "Near Miss", KeyValue = "NearMiss")]
        NearMiss,

        /// <summary>
        /// The safety acts
        /// </summary>
        [EnumValueData(Name = "Safety Acts", KeyValue = "SafetyActs")]
        SafetyActs,

        /// <summary>
        /// The un safety acts
        /// </summary>
        [EnumValueData(Name = "UnSafety Acts", KeyValue = "UnSafetyActs")]
        UnSafetyActs,

        /// <summary>
        /// The un safety conditions
        /// </summary>
        [EnumValueData(Name = "UnSafety Conditions", KeyValue = "UnSafetyConditions")]
        UnSafetyConditions,

        /// <summary>
        /// The m exp hs
        /// </summary>
        [EnumValueData(Name = "MExpHs", KeyValue = "MExpHs")]
        MExpHs,

        /// <summary>
        /// The open accidents
        /// </summary>
        [EnumValueData(Name = "OpenAccidents", KeyValue = "OpenAccidents")]
        OpenAccidents,

        /// <summary>
        /// The open incidents
        /// </summary>
        [EnumValueData(Name = "OpenIncidents", KeyValue = "OpenIncidents")]
        OpenIncidents,

        /// <summary>
        /// The open incidents
        /// </summary>
        [EnumValueData(Name = "Open Items", KeyValue = "OpenItems")]
        OpenItems,

        /// <summary>
        /// The open incidents
        /// </summary>
        [EnumValueData(Name = "LTI", KeyValue = "LTI")]
        LTI,

        /// <summary>
        /// The open incidents
        /// </summary>
        [EnumValueData(Name = "TRC", KeyValue = "TRC")]
        TRC,

        /// <summary>
        /// The crew medical fatality.
        /// </summary>
        [EnumValueData(Name = "Crew Medical Fatality", KeyValue = "Crew Medical Fatality")]
        CrewMedicalFatality,

        /// <summary>
        /// The third party accidents.
        /// </summary>
        [EnumValueData(Name = "ThirdPartyAccidents", KeyValue = "ThirdPartyAccidents")]
        ThirdPartyAccidents,

        /// <summary>
        /// The passenger accidents.
        /// </summary>
        [EnumValueData(Name = "PassengerAccidents", KeyValue = "PassengerAccidents")]
        PassengerAccidents,

        /// <summary>
        /// The illness.
        /// </summary>
        [EnumValueData(Name = "Illness", KeyValue = "Illness")]
        Illness
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PMSDashboardType
    {
        /// <summary>
        /// The critical done
        /// </summary>
        [EnumValueData(Name = "CriticalDone", KeyValue = "CriticalDone")]
        CriticalDone,

        /// <summary>
        /// The critical due
        /// </summary>
        [EnumValueData(Name = "CriticalDue", KeyValue = "CriticalDue")]
        CriticalDue,

        /// <summary>
        /// The critical over due
        /// </summary>
        [EnumValueData(Name = "CriticalOverDue", KeyValue = "CriticalOverDue")]
        CriticalOverDue,

        /// <summary>
        /// The critical over due prior
        /// </summary>
        [EnumValueData(Name = "CriticalOverDuePrior", KeyValue = "CriticalOverDuePrior")]
        CriticalOverDuePrior,

        /// <summary>
        /// The non critical done
        /// </summary>
        [EnumValueData(Name = "NonCriticalDone", KeyValue = "NonCriticalDone")]
        NonCriticalDone,

        /// <summary>
        /// The non critical due
        /// </summary>
        [EnumValueData(Name = "NonCriticalDue", KeyValue = "NonCriticalDue")]
        NonCriticalDue,

        /// <summary>
        /// The non critical over due
        /// </summary>
        [EnumValueData(Name = "NonCriticalOverDue", KeyValue = "NonCriticalOverDue")]
        NonCriticalOverDue,

        /// <summary>
        /// The non critical over due prior
        /// </summary>
        [EnumValueData(Name = "NonCriticalOverDuePrior", KeyValue = "NonCriticalOverDuePrior")]
        NonCriticalOverDuePrior,

        /// <summary>
        /// The ships wo planned
        /// </summary>
        [EnumValueData(Name = "ShipsWOPlanned", KeyValue = "ShipsWOPlanned")]
        ShipsWOPlanned,

        /// <summary>
        /// The ships wo done
        /// </summary>
        [EnumValueData(Name = "ShipsWODone", KeyValue = "ShipsWODone")]
        ShipsWODone,

        /// <summary>
        /// The resc wo approved
        /// </summary>
        [EnumValueData(Name = "RescWOApproved", KeyValue = "RescWOApproved")]
        RescWOApproved,

        /// <summary>
        /// The spares below tech minimum
        /// </summary>
        [EnumValueData(Name = "SparesBelowTechMin", KeyValue = "SparesBelowTechMin")]
        SparesBelowTechMin,

        /// <summary>
        /// The spares below opr minimum
        /// </summary>
        [EnumValueData(Name = "SparesBelowOprMin", KeyValue = "SparesBelowOprMin")]
        SparesBelowOprMin
    }

    /// <summary>
    /// Categories for Documents
    /// </summary>
    public enum DocumentCategoryType
    {
        /// <summary>
        /// The certificates
        /// </summary>
        [EnumValueData(Name = "Certificates", KeyValue = "GLAS00000001")]
        Certificates,
        /// <summary>
        /// The general arrangement
        /// </summary>
        [EnumValueData(Name = "General Arrangement", KeyValue = "GLAS00000002")]
        GeneralArrangement,
        /// <summary>
        /// The lop
        /// </summary>
        [EnumValueData(Name = "LOP", KeyValue = "GLAS00000003")]
        LOP,
        /// <summary>
        /// The BDN
        /// </summary>
        [EnumValueData(Name = "BDN", KeyValue = "GLAS00000004")]
        BDN,
        /// <summary>
        /// The general documentation
        /// </summary>
        [EnumValueData(Name = "General Documentation", KeyValue = "GLAS00000005")]
        GeneralDocumentation,
        /// <summary>
        /// The work order
        /// </summary>
        [EnumValueData(Name = "Work Order", KeyValue = "GLAS00000006")]
        WorkOrder,
        /// <summary>
        /// The report work done
        /// </summary>
        [EnumValueData(Name = "Report Work Done", KeyValue = "GLAS00000007")]
        ReportWorkDone,
        /// <summary>
        /// The specification
        /// </summary>
        [EnumValueData(Name = "Specification", KeyValue = "GLAS00000008")]
        Specification,
        /// <summary>
        /// The manual
        /// </summary>
        [EnumValueData(Name = "Manual/Drawing/Procedure Document", KeyValue = "GLAS00000009")]
        Manual,
        /// <summary>
        /// The training document
        /// </summary>
        [EnumValueData(Name = "Training Document", KeyValue = "GLAS00000010")]
        TrainingDocument,
        /// <summary>
        /// The other
        /// </summary>
        [EnumValueData(Name = "Other", KeyValue = "GLAS00000011")]
        Other,
        /// <summary>
        /// The permits
        /// </summary>
        [EnumValueData(Name = "Permits", KeyValue = "GLAS00000012")]
        Permits,
        /// <summary>
        /// The component image
        /// </summary>
        [EnumValueData(Name = "Component Image", KeyValue = "GLAS00000013")]
        ComponentImage,
        /// <summary>
        /// The task document
        /// </summary>
        [EnumValueData(Name = "Task Document", KeyValue = "GLAS00000014")]
        TaskDocument,
        /// <summary>
        /// The service letter
        /// </summary>
        [EnumValueData(Name = "Service Letter", KeyValue = "GLAS00000015")]
        ServiceLetter,
        /// <summary>
        /// The maker manual
        /// </summary>
        [EnumValueData(Name = "Maker Manual", KeyValue = "GLAS00000016")]
        MakerManual,
        /// <summary>
        /// The CBM
        /// </summary>
        [EnumValueData(Name = "CBM", KeyValue = "GLAS00000017")]
        CBM,
        /// <summary>
        /// The work done image
        /// </summary>
        [EnumValueData(Name = "Work Done Image", KeyValue = "GLAS00000018")]
        WorkDoneImage,
        /// <summary>
        /// The makers form
        /// </summary>
        [EnumValueData(Name = "Maker's Form", KeyValue = "GLAS00000019")]
        MakersForm,
        /// <summary>
        /// The service note
        /// </summary>
        [EnumValueData(Name = "Service Note", KeyValue = "GLAS00000020")]
        ServiceNote,
        /// <summary>
        /// The delivery note
        /// </summary>
        [EnumValueData(Name = "Delivery Note", KeyValue = "GLAS00000021")]
        DeliveryNote,
        /// <summary>
        /// The service report
        /// </summary>
        [EnumValueData(Name = "Service Report", KeyValue = "GLAS00000022")]
        ServiceReport,
        /// <summary>
        /// The freight order
        /// </summary>
        [EnumValueData(Name = "Freight Order", KeyValue = "GLAS00000023")]
        FreightOrder,
        /// <summary>
        /// The purch order
        /// </summary>
        [EnumValueData(Name = "Purch Order", KeyValue = "GLAS00000024")]
        PurchOrder,

        /// <summary>
        /// The reschedule moc guidelines.
        /// </summary>
        [EnumValueData(Name = "Reschedule MOC Guidelines", KeyValue = "GLAS00000040")]
        RescheduleMOCGuidelines,

        /// <summary>
        /// The material declaration
        /// </summary>
        [EnumValueData(Name = "Material Declaration", KeyValue = "GLAS00000041")]
        MaterialDeclaration,

        /// <summary>
        /// The supplier document of compliance
        /// </summary>
        [EnumValueData(Name = "Supplier Document of Compliance", KeyValue = "GLAS00000042")]
        SupplierDocumentofCompliance,

        /// <summary>
        /// The vessel image
        /// </summary>
        [EnumValueData(Name = "Vessel Image", KeyValue = "GLAS00000043")]
        VesselImage,

        /// <summary>
        /// The cover image
        /// </summary>
        [EnumValueData(Name = "Cover Image", KeyValue = "GLAS00000044")]
        CoverImage
    }

    /// <summary>
    /// This enum is used for document type
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// The document
        /// </summary>
        [EnumValueData(Name = "Document", KeyValue = "1")]
        Document,

        /// <summary>
        /// The web address
        /// </summary>
        [EnumValueData(Name = "Web Address", KeyValue = "2")]
        WebAddress
    }

    /// <summary>
    /// Enum for cloud folder
    /// </summary>
    public enum CloudFolder
    {
        /// <summary>
        /// The crew
        /// </summary>
        [EnumValueData(Name = "Crew", KeyValue = "Crew", LiveKeyValue = "crew")]
        Crew,
        /// <summary>
        /// The entity
        /// </summary>
        [EnumValueData(Name = "ENTITY", KeyValue = "ENTITY", LiveKeyValue = "entity")]
        Entity,
        /// <summary>
        /// The v entity
        /// </summary>
        [EnumValueData(Name = "ventity", KeyValue = "ventity", LiveKeyValue = "Ventity")]
        VEntity,
        /// <summary>
        /// The finance invoice
        /// </summary>
        [EnumValueData(Name = "FinanceInvoice", KeyValue = "FinanceInvoice", LiveKeyValue = "financeinvoice")]
        FinanceInvoice,
        /// <summary>
        /// The supplierdocs
        /// </summary>
        [EnumValueData(Name = "supplierdocs", KeyValue = "supplierdocs", LiveKeyValue = "SupplierDocs")]
        supplierdocs,

        /// <summary>
        /// The crew finance docs - payslip scans etc
        /// </summary>
        [EnumValueData(Name = "CrewFinance", KeyValue = "crewfinancedocstest", LiveKeyValue = "crewfinancedocs")]
        CrewFinanceDocuments,

        /// <summary>
        /// The crew optimiser
        /// </summary>
        [EnumValueData(Name = "CrewOptimiser", KeyValue = "crewoptimiseruat", LiveKeyValue = "crewoptimiserlive")]
        CrewOptimiser,

        /// <summary>
        /// The crew payroll
        /// </summary>
        [EnumValueData(Name = "crewpayroll", KeyValue = "crewpayroll", LiveKeyValue = "crewpayroll-live")]
        CrewPayroll
    }



    /// <summary>The category of document</summary>
    public enum DocumentCategory
    {
        /// <summary>
        /// The medical drug alcohol test
        /// </summary>
        [EnumValueData(Name = "MedicalDrugAlcoholTest", KeyValue = "CRWMedicalDrugAlcoholTest")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        MedicalDrugAlcoholTest,
        /// <summary>
        /// The vessel inspection
        /// </summary>
        [EnumValueData(Name = "VesselInspection", KeyValue = "VESVISIT")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselInspection,

        ///// <summary>
        ///// The vessel certificate
        ///// </summary>
        //[EnumValueData(Name = "VesselCertificate", KeyValue = "VESCERTIFICATE")]
        //[CloudDocumentAttribute(Prefix = "VLCERT_", CloudFolder = CloudFolder.VEntity)]
        //VesselCertificate,

        /// <summary>
        /// The vessel hazardous occurrences
        /// </summary>
        [EnumValueData(Name = "VesselHazardousOccurrences", KeyValue = "INCMAN_REPORTS")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselHazardousOccurrences,
        /// <summary>
        /// The acc company
        /// </summary>
        [EnumValueData(Name = "AccountingCompany", KeyValue = "AccCompany")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        AccCompany,
        /// <summary>
        /// The tech document
        /// </summary>
        [EnumValueData(Name = "TechnicalDocument", KeyValue = "TechDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        TechDocument,
        /// <summary>
        /// Crew's Next Of Kin Attachment Category
        /// </summary>
        [EnumValueData(Name = "CrewNextOfKinAttachment", KeyValue = "CRWNextOfKin")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWNextOfKin,
        /// <summary>
        /// The crew document category
        /// </summary>
        [EnumValueData(Name = "CrewDocument", KeyValue = "CRWDocs")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewDocument,
        /// <summary>
        /// The mob checklist header
        /// </summary>
        [EnumValueData(Name = "MobChecklistHeader", KeyValue = "CRWMobilisationCheckListHeader")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        MobChecklistHeader,
        /// <summary>
        /// The crew document archived category
        /// </summary>
        [EnumValueData(Name = "CrewDocumentArchived", KeyValue = "archive_CRWDocs")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewDocumentArchived,
        /// <summary>
        /// The crew service detail category
        /// </summary>
        [EnumValueData(Name = "CrewServiceDetail", KeyValue = "CRWSrvDetail")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewServiceDetail,
        /// <summary>
        /// The crew previous experience category
        /// </summary>
        [EnumValueData(Name = "CrewPreviousExperience", KeyValue = "CRWPreviousExperience")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewPreviousExperience,

        /// <summary>
        /// Remittncne Advice, linked with unique PayRun Number for multiple Invoices
        /// </summary>
        [EnumValueData(Name = "RemittanceAdvice", KeyValue = "RemittanceAdvice")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        RemittanceAdvice,

        /// <summary>
        /// The CRW document scanned
        /// </summary>
        [EnumValueData(Name = "CRWDocScanned", KeyValue = "CRWDocScanned")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWDocScanned,

        /// <summary>
        /// The PPM job
        /// </summary>
        [EnumValueData(Name = "PPMJob", KeyValue = "PPMJob")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PPMJob,

        /// <summary>
        /// The Crew Line up category
        /// </summary>
        [EnumValueData(Name = "CrewLineups", KeyValue = "CRWLineups")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWLineups,

        /// <summary>
        /// The ves inventory
        /// </summary>
        [EnumValueData(Name = "VesInventory", KeyValue = "VesInventory")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesInventory,

        /// <summary>
        /// The General Ledger Transaction transaction
        /// </summary>
        [EnumValueData(Name = "GlTransaction", KeyValue = "GlTransaction")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        GeneralLedgerTransaction,

        /// <summary>
        /// The crew personal note category
        /// </summary>
        [EnumValueData(Name = "CrewPersonalNote", KeyValue = "CRWPersonalNotes")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewPersonalNote,


        /// <summary>
        /// The PPM work order history
        /// </summary>
        [EnumValueData(Name = "PPMWorkOrderHistoryDocument", KeyValue = "PPMWorkOrderHistoryDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PPMWorkOrderHistory,

        /// <summary>
        /// The ra vessel assessment
        /// </summary>
        [EnumValueData(Name = "RA_VesselAss", KeyValue = "RA_VesselAss")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        RA_VesselAss,

        /// <summary>
        /// The Schedule Task Document
        /// </summary>
        [EnumValueData(Name = "PPMScheduleTaskDocument", KeyValue = "PPMScheduleTaskDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PPMScheduleTask,

        /// <summary>
        /// The Company
        /// </summary>
        [EnumValueData(Name = "Company", KeyValue = "Company")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Company,

        /// <summary>
        /// The Company
        /// </summary>
        [EnumValueData(Name = "CompanyVettingDocuments", KeyValue = "CompanyVettingDocuments")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        CompanyVettingDocuments,

        /// <summary>
        /// The Company
        /// </summary>
        [EnumValueData(Name = "ViXDocuments", KeyValue = "ViXDocuments")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ViXDocuments,

        /// <summary>
        /// The sea passage
        /// </summary>
        [EnumValueData(Name = "VESENVMAN_Document", KeyValue = "VESENVMAN_Document")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesEnvManDocument,

        /// <summary>
        /// The sea passage
        /// </summary>
        [EnumValueData(Name = "PosSeaPassage", KeyValue = "PosSeaPassage")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        SeaPassage,

        /// <summary>
        /// The Crew image.
        /// </summary>
        [EnumValueData(Name = "CRWPICS", KeyValue = "CRWPICS")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewPics,

        /// <summary>
        /// The Job Safety Analysis - Job Detail
        /// </summary>
        [EnumValueData(Name = "JSA_JobDetail", KeyValue = "JSA_JobDetail")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        JSA_JobDetail,

        /// <summary>
        /// The defect work order document
        /// </summary>
        [EnumValueData(Name = "DefectWorkOrderDocument", KeyValue = "DefectWorkOrderDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        DefectWorkOrderDocument,

        /// <summary>
        /// The ves particular
        /// </summary>
        [EnumValueData(Name = "VESPARTICULAR", KeyValue = "VESPARTICULAR")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesParticular,

        /// <summary>
        /// The budget supporting docs
        /// </summary>
        [EnumValueData(Name = "BUDGET_SupportingDocs", KeyValue = "BUDGET_SupportingDocs")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        BUDGET_SupportingDocs,

        /// <summary>
        /// The vesmoc document
        /// </summary>
        [EnumValueData(Name = "VESMOC_Document", KeyValue = "VESMOC_Document")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VESMOCDocument,

        /// <summary>
        /// The CrewMisconduct.
        /// </summary>
        [EnumValueData(Name = "CRWMisconduct", KeyValue = "CRWMisconduct")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewMisconduct,

        /// <summary>
        /// The CrewContractTemplates.
        /// </summary>
        [EnumValueData(Name = "CRWContractTemplates", KeyValue = "CRWContractTemplates")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewContractTemplates,

        /// <summary>
        /// The vessel document.
        /// </summary>
        [EnumValueData(Name = "VesselDocument", KeyValue = "VesselDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselDocument,

        /// <summary>
        /// The insurance claim document.
        /// </summary>
        [EnumValueData(Name = "VL_InsuranceClaims", KeyValue = "VL_InsuranceClaims")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VL_InsuranceClaims,

        /// <summary>
        /// The insurance claim document.
        /// </summary>
        [EnumValueData(Name = "CrewPaySlips", KeyValue = "CrewPaySlips")]
        [CloudDocumentAttribute(Prefix = "PS_", CloudFolder = CloudFolder.CrewFinanceDocuments)]
        CrewPaySlips,

        /// <summary>
        /// The insurance claim document.
        /// </summary>
        [EnumValueData(Name = "INVDocument", KeyValue = "INVDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        FinanceInvoice,

        /// <summary>
        /// The finance invoice old
        /// </summary>
        [EnumValueData(Name = "INVDocument", KeyValue = "INVDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.FinanceInvoice)]
        FinanceInvoiceOld,

        /// <summary>
        /// The JSA Document
        /// </summary>
        [EnumValueData(Name = "JSA_Document", KeyValue = "JSA_Document")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        JSADocument,

        /// <summary>
        /// The Crew Contract Details.
        /// </summary>
        [EnumValueData(Name = "CRWContractDetails", KeyValue = "CRWContractDetails")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewContractDetails,

        /// <summary>
        /// The charter
        /// </summary>
        [EnumValueData(Name = "Charter", KeyValue = "Charter")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Charter,

        /// <summary>
        /// The port event
        /// </summary>
        [EnumValueData(Name = "PortEvent", KeyValue = "PortEvent")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PortEvent,

        /// <summary>
        /// The bunker
        /// </summary>
        [EnumValueData(Name = "Bunker", KeyValue = "Bunker")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Bunker,

        /// <summary>
        /// The position list activity
        /// </summary>
        [EnumValueData(Name = "PositionListActivity", KeyValue = "PositionListActivity")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PositionListActivity,

        /// <summary>
        /// The vessel general arrangement
        /// </summary>
        [EnumValueData(Name = "VesselGeneralArrangement", KeyValue = "VesselGeneralArrangement")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselGeneralArrangement,

        /// <summary>
        /// The vessel certificate
        /// </summary>
        [EnumValueData(Name = "VesselCertificate", KeyValue = "VesselCertificate")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselCertificate,

        /// <summary>
        /// The garbage
        /// </summary>
        [EnumValueData(Name = "Garbage", KeyValue = "Garbage")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Garbage,

        /// <summary>
        /// The sewage
        /// </summary>
        [EnumValueData(Name = "Sewage", KeyValue = "Sewage")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Sewage,

        /// <summary>
        /// The ozone
        /// </summary>
        [EnumValueData(Name = "Ozone", KeyValue = "Ozone")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Ozone,

        /// <summary>
        /// The oil
        /// </summary>
        [EnumValueData(Name = "Oil", KeyValue = "Oil")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Oil,

        /// <summary>
        /// The job
        /// </summary>
        [EnumValueData(Name = "Job", KeyValue = "Job")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Job,

        /// <summary>
        /// The schedule task component detail
        /// </summary>
        [EnumValueData(Name = "ScheduleTaskComponentDetail", KeyValue = "ScheduleTaskComponentDetail")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ScheduleTaskComponentDetail,

        /// <summary>
        /// The work order history
        /// </summary>
        [EnumValueData(Name = "WorkOrderHistory", KeyValue = "WorkOrderHistory")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        WorkOrderHistory,

        /// <summary>
        /// The component
        /// </summary>
        [EnumValueData(Name = "Component", KeyValue = "Component")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Component,

        /// <summary>
        /// The spare
        /// </summary>
        [EnumValueData(Name = "Spare", KeyValue = "Spare")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Spare,

        /// <summary>
        /// The defect work order
        /// </summary>
        [EnumValueData(Name = "DefectWorkOrder", KeyValue = "DefectWorkOrder")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        DefectWorkOrder,

        /// <summary>
        /// The management of change
        /// </summary>
        [EnumValueData(Name = "ManagementOfChange", KeyValue = "ManagementOfChange")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ManagementOfChange,

        /// <summary>
        /// The job safety analysis
        /// </summary>
        [EnumValueData(Name = "JobSafetyAnalysis", KeyValue = "JobSafetyAnalysis")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        JobSafetyAnalysis,

        /// <summary>
        /// The defect report work order
        /// </summary>
        [EnumValueData(Name = "DefectReportWorkOrder", KeyValue = "DefectReportWorkOrder")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        DefectReportWorkOrder,

        /// <summary>
        /// The schedule task measurement standards
        /// </summary>
        [EnumValueData(Name = "ScheduleTaskMeasurementStandards", KeyValue = "ScheduleTaskMeasurementStandards")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ScheduleTaskMeasurementStandards,

        /// <summary>
        /// The work order history component detail
        /// </summary>
        [EnumValueData(Name = "WorkOrderHistoryComponentDetail", KeyValue = "WorkOrderHistoryComponentDetail")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        WorkOrderHistoryComponentDetail,

        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "HazOcc")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        HazOcc,

        /// <summary>
        /// The insurance
        /// </summary>
        [EnumValueData(Name = "Insurance", KeyValue = "Insurance")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Insurance,

        /// <summary>
        /// The crew personal details
        /// </summary>
        [EnumValueData(Name = "CRWPersonalDetails", KeyValue = "CRWPersonalDetails")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewPersonalDetails,

        /// <summary>
        /// The onboard crewing
        /// </summary>
        [EnumValueData(Name = "OnboardCrewing", KeyValue = "OnboardCrewing")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        OnboardCrewing,

        /// <summary>
        /// The vessel technical document
        /// </summary>
        [EnumValueData(Name = "VesselTechnicalDocument", KeyValue = "VesselTechnicalDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselTechnicalDocument,

        /// <summary>
        /// The hotel defect manager
        /// </summary>
        [EnumValueData(Name = "HotelDefectManager", KeyValue = "HotelDefectManager")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        HotelDefectManager,

        /// <summary>
        /// The weekly minutes
        /// </summary>
        [EnumValueData(Name = "WeeklyMinutes", KeyValue = "WeeklyMinutes")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        WeeklyMinutes,

        /// <summary>
        /// The drills and campaigns maintainer
        /// </summary>
        [EnumValueData(Name = "DrillsAndCampaignsMaintainer", KeyValue = "DrillsAndCampaignsMaintainer")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        DrillsAndCampaignsMaintainer,

        /// <summary>
        /// The vessel drills
        /// </summary>
        [EnumValueData(Name = "VesselDrills", KeyValue = "VesselDrills")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselDrills,

        /// <summary>
        /// The report drills and campaigns
        /// </summary>
        [EnumValueData(Name = "ReportDrillsAndCampaigns", KeyValue = "ReportDrillsAndCampaigns")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ReportDrillsAndCampaigns,

        /// <summary>
        /// The catalogue item
        /// </summary>
        [EnumValueData(Name = "CatalogueItem", KeyValue = "CatalogueItem")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        CatalogueItem,

        /// <summary>
        /// The hotel defect job
        /// </summary>
        [EnumValueData(Name = "HotelDefectJob", KeyValue = "HotelDefectJob")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        HotelDefectJob,

        /// <summary>
        /// The hotel defect report work order
        /// </summary>
        [EnumValueData(Name = "HotelDefectReportWorkOrder", KeyValue = "HotelDefectReportWorkOrder")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        HotelDefectReportWorkOrder,

        /// <summary>
        /// The crew documents others
        /// </summary>
        [EnumValueData(Name = "CRWDocsOthers", KeyValue = "CRWDocsOthers")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWDocsOthers,

        /// <summary>
        /// The crew recruiting
        /// </summary>
        [EnumValueData(Name = "CRWRequirement", KeyValue = "CRWRequirement")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWRequirement,

        /// <summary>
        /// The crew medical
        /// </summary>
        [EnumValueData(Name = "CRWMedicalHistory", KeyValue = "CRWMedicalHistory")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWMedicalHistory,

        /// <summary>
        /// The crew appraisals
        /// </summary>
        [EnumValueData(Name = "CRWAppraisal", KeyValue = "CRWAppraisal")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWAppraisal,

        /// <summary>
        /// The performance appraisal
        /// </summary>
        [EnumValueData(Name = "PerformanceAppraisal", KeyValue = "PerformanceAppraisal")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PerformanceAppraisal,

        /// <summary>
        /// The PRL period
        /// </summary>
        [EnumValueData(Name = "PRLPeriod", KeyValue = "PRLPeriod")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewPayroll)]
        PRLPeriod,

        /// <summary>
        /// The PRL period tran header
        /// </summary>
        [EnumValueData(Name = "PRLPeriodTranHeader", KeyValue = "PRLPeriodTranHeader")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewPayroll)]
        PRLPeriodTranHeader,

        /// <summary>
        /// The aps crew allotment.
        /// </summary>
        [EnumValueData(Name = "APS_Crew_Allotment", KeyValue = "APS_Crew_Allotment")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewPayroll)]
        APS_Crew_Allotment,

        /// <summary>
        /// The APS vessel period.
        /// </summary>
        [EnumValueData(Name = "APS_Vessel_Period", KeyValue = "APS_Vessel_Period")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewPayroll)]
        APS_Vessel_Period,

        /// <summary>
        /// The catalog section
        /// </summary>
        [EnumValueData(Name = "CatalogSection", KeyValue = "CatalogSection")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        CatalogSection,

        /// <summary>
        /// The crew optimiser request
        /// </summary>
        [EnumValueData(Name = "CRWOptimiserRequest", KeyValue = "CRWOptimiserRequest")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewOptimiser)]
        CRWOptimiserRequest,

        /// <summary>
        /// The crew optimiser response
        /// </summary>
        [EnumValueData(Name = "CRWOptimiserResponse", KeyValue = "CRWOptimiserResponse")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewOptimiser)]
        CRWOptimiserResponse,

        /// <summary>
        /// The DocManCloudFile section
        /// </summary>
        [EnumValueData(Name = "DocManCloudFile", KeyValue = "DocManCloudFile")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        DocManCloudFile,

        /// <summary>
        /// The PRL period portage
        /// </summary>
        [EnumValueData(Name = "PRLPeriodPortage", KeyValue = "PRLPeriodPortage")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        PRLPeriodPortage,

        /// <summary>
        /// The PRL Bank File.
        /// </summary>
        [EnumValueData(Name = "PRLBankFile", KeyValue = "PRLBankFile")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        PRLBankFile,

        /// <summary>
        /// The reschedule work order
        /// </summary>
        [EnumValueData(Name = "RescheduleWorkOrder", KeyValue = "RescheduleWorkOrder")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        RescheduleWorkOrder,

        /// <summary>
        /// The CRW temporary docs
        /// </summary>
        [EnumValueData(Name = "CRWTempDocs", KeyValue = "CRWTempDocs")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        CRWTempDocs,

        /// <summary>
        /// For vessel sales invoice header
        /// </summary>
        [EnumValueData(Name = "VESINVHEADER", KeyValue = "VESINVHEADER")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VESINVHEADER,

        /// <summary>
        /// The CRW waiver
        /// </summary>
        [EnumValueData(Name = "CRWWaiver", KeyValue = "CRWWaiver")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWWaiver,

        /// <summary>
        /// The moc guidelines.
        /// </summary>
        [EnumValueData(Name = "MOCGuidelines", KeyValue = "MOCGuidelines")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        MOCGuidelines,

        /// <summary>
        /// The company compliance
        /// </summary>
        [EnumValueData(Name = "CompanyCompliance", KeyValue = "CompanyCompliance")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        CompanyCompliance,

        /// <summary>
        /// The Port Planning Activity
        /// </summary>
        [EnumValueData(Name = "PortPlanningActivity", KeyValue = "PortPlanningActivity")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PortPlanningActivity,

        /// <summary>
        /// The vessel inspectio section image
        /// </summary>
        [EnumValueData(Name = "VesselInspectioSectionImage", KeyValue = "VesselInspectioSectionImage")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselInspectioSectionImage,

        /// <summary>
        /// The vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "Vessel")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Vessel,

        /// <summary>
        /// The port alert
        /// </summary>
        [EnumValueData(Name = "PortAlert", KeyValue = "PortAlert")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        PortAlert,

        /// <summary>
        /// The inspection
        /// </summary>
        [EnumValueData(Name = "Inspection", KeyValue = "Inspection")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        Inspection,

        /// <summary>
        /// The inspection
        /// </summary>
        [EnumValueData(Name = "CRWDebriefing", KeyValue = "CRWDebriefing")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWDebriefing,

        /// <summary>
        /// The Crew Allotment
        /// </summary>
        [EnumValueData(Name = "CRWAllotments2", KeyValue = "CRWAllotments2")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CRWAllotments2,

        /// <summary>
        /// The Crew salary transfer letter details
        /// </summary>
        [EnumValueData(Name = "Crwsalarytransferletterdetail", KeyValue = "Crwsalarytransferletterdetail")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        Crwsalarytransferletterdetail,

        /// <summary>
        /// The PRL export template tracking
        /// </summary>
        [EnumValueData(Name = "PRLExportTemplateTracking", KeyValue = "PRLExportTemplateTracking")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        PRLExportTemplateTracking,

        /// <summary>
        /// The moc task break down
        /// </summary>
        [EnumValueData(Name = "MOCTaskBreakDown", KeyValue = "MOCTaskBreakDown")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        MOCTaskBreakDown,

        /// <summary>
        /// The moc task permit
        /// </summary>
        [EnumValueData(Name = "MOCTaskPermit", KeyValue = "MOCTaskPermit")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        MOCTaskPermit,

        /// <summary>
        /// The PRL period header
        /// </summary>
        [EnumValueData(Name = "PRLPeriodHeader", KeyValue = "PRLPeriodHeader")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.CrewPayroll)]
        PRLPeriodHeader,

        /// <summary>
        /// The invheader
        /// </summary>
        [EnumValueData(Name = "INVHEADER", KeyValue = "INVHEADER")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        INVHEADER,

        /// <summary>
        /// Crew Interview Details
        /// </summary>
        [EnumValueData(Name = "CRW_InterviewDetails", KeyValue = "CRW_InterviewDetails")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CrewInterviewDetails,

        /// <summary>
        /// The ent tech document
        /// </summary>
        [EnumValueData(Name = "ENTTechDocument", KeyValue = "ENTTechDocument")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ENTTechDocument,

        /// <summary>
        /// The order line
        /// </summary>
        [EnumValueData(Name = "OrderLine", KeyValue = "OrderLine")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        OrderLine,

        /// <summary>
        /// The vessel supplier quote
        /// </summary>
        [EnumValueData(Name = "VesselSupplierQuote", KeyValue = "VesselSupplierQuote")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselSupplierQuote,

        /// <summary>
        /// The spare serial no
        /// </summary>
        [EnumValueData(Name = "SpareSerialNo", KeyValue = "SpareSerialNo")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        SpareSerialNo,

        /// <summary>
        /// Office Safety Assessment documents
        /// </summary>
        [EnumValueData(Name = "JSA_JobDetailExtension", KeyValue = "JSA_JobDetailExtension")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        OfficeSafetyAssessment,

        /// <summary>
        /// The entinv comparator
        /// </summary>
        [EnumValueData(Name = "ENTINVComparator", KeyValue = "ENTINVComparator")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        ENTINVComparator,

        /// <summary>
        /// The inventory landing form.
        /// </summary>
        [EnumValueData(Name = "InventoryLandingForm", KeyValue = "InventoryLandingForm")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        InventoryLandingForm,

        /// <summary>
        /// The inspection maintainer.
        /// </summary>
        [EnumValueData(Name = "InspectionMaintainer", KeyValue = "InspectionMaintainer")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        InspectionMaintainer,

        /// <summary>
        /// The inventory landing form item.
        /// </summary>
        [EnumValueData(Name = "InventoryLandingFormItem", KeyValue = "InventoryLandingFormItem")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        InventoryLandingFormItem,

        /// <summary>
        /// The environment orb part a
        /// </summary>
        [EnumValueData(Name = "EnvironmentORBPartA", KeyValue = "EnvironmentORBPartA")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EnvironmentORBPartA,

        /// <summary>
        /// The environment orb part b
        /// </summary>
        [EnumValueData(Name = "EnvironmentORBPartB", KeyValue = "EnvironmentORBPartB")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EnvironmentORBPartB,

        /// <summary>
        /// The environment garbage record book
        /// </summary>
        [EnumValueData(Name = "EnvironmentGarbageRecordBook", KeyValue = "EnvironmentGarbageRecordBook")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EnvironmentGarbageRecordBook,

        /// <summary>
        /// The environment sewage record book
        /// </summary>
        [EnumValueData(Name = "EnvironmentSewageRecordBook", KeyValue = "EnvironmentSewageRecordBook")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EnvironmentSewageRecordBook,

        /// <summary>
        /// The environment ods record book annexure ii
        /// </summary>
        [EnumValueData(Name = "EnvironmentODSRecordBookAnnexureII", KeyValue = "EnvironmentODSRecordBookAnnexureII")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EnvironmentODSRecordBookAnnexureII,

        /// <summary>
        /// The environment ods record book annexure iii
        /// </summary>
        [EnumValueData(Name = "EnvironmentODSRecordBookAnnexureIII", KeyValue = "EnvironmentODSRecordBookAnnexureIII")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EnvironmentODSRecordBookAnnexureIII,

        /// <summary>
        /// The entity invoice depreciation history
        /// </summary>
        [EnumValueData(Name = "EntityInvoiceDepreciationHistory", KeyValue = "EntityInvoiceDepreciationHistory")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityInvoiceDepreciationHistory,

        /// <summary>
        /// The entity invoice accrual history
        /// </summary>
        [EnumValueData(Name = "EntityInvoiceAccrualHistory", KeyValue = "EntityInvoiceAccrualHistory")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityInvoiceAccrualHistory,

        /// <summary>
        /// The entity stock
        /// </summary>
        [EnumValueData(Name = "EntityStock", KeyValue = "EntityStock")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityStock,

        /// <summary>
        /// The entity stock
        /// </summary>
        [EnumValueData(Name = "EntityStockImage", KeyValue = "EntityStockImage")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityStockImage,

        /// <summary>
        /// The entity order line.
        /// </summary>
        [EnumValueData(Name = "EntityOrderLine", KeyValue = "EntityOrderLine")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityOrderLine,

        /// <summary>
        /// The entity roll and clear history.
        /// </summary>
        [EnumValueData(Name = "EntityRollAndClearHistory", KeyValue = "EntityRollAndClearHistory")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityRollAndClearHistory,


        /// <summary>
        /// The inter company settlement request
        /// </summary>
        [EnumValueData(Name = "InterCompanySettlementRequest", KeyValue = "InterCompanySettlementRequest")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        InterCompanySettlementRequest,

        /// <summary>
        /// The entity catalogue item
        /// </summary>
        [EnumValueData(Name = "EntityCatalogueItem", KeyValue = "EntityCatalogueItem")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityCatalogueItem,


        /// <summary>
        /// The entity catalogue section
        /// </summary>
        [EnumValueData(Name = "EntityCatalogueSection", KeyValue = "EntityCatalogueSection")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntityCatalogueSection,

        /// <summary>
        /// The centralised requests
        /// </summary>
        [EnumValueData(Name = "CentralisedRequests", KeyValue = "CRWCentralisedRequest")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        CentralisedRequests,

        /// <summary>
        /// The engine logbook daily report
        /// </summary>
        [EnumValueData(Name = "EngineLogbookDailyReport", KeyValue = "EngineLogbookDailyReport")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EngineLogbookDailyReport,

        /// <summary>
        /// The vessel contract supplier
        /// </summary>
        [EnumValueData(Name = "VesselContractSupplier", KeyValue = "VesselContractSupplier")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        VesselContractSupplier,

        /// <summary>
        /// The ent purch unmatched invoice
        /// </summary>
        [EnumValueData(Name = "EntPurchUnmatchedInvoice", KeyValue = "EntPurchUnmatchedInvoice")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.VEntity)]
        EntPurchUnmatchedInvoice,

        /// <summary>
        /// The promotion management
        /// </summary>
        [EnumValueData(Name = "PromotionManagement", KeyValue = "CRWPromotionHeader")]
        [CloudDocumentAttribute(Prefix = "", CloudFolder = CloudFolder.Crew)]
        PromotionManagement
    }

    /// <summary>
    /// Enum for document file type
    /// </summary>
    public enum DocumentFileType
    {
        /// <summary>
        /// The PDF
        /// </summary>
        [EnumValueData(Name = "application/pdf", KeyValue = ".pdf")]
        Pdf,
        /// <summary>
        /// The bitmap
        /// </summary>
        [EnumValueData(Name = "image/bmp", KeyValue = ".bmp")]
        Bitmap,
        /// <summary>
        /// The CSV
        /// </summary>
        [EnumValueData(Name = "text/csv", KeyValue = ".csv")]
        Csv,
        /// <summary>
        /// The GIF
        /// </summary>
        [EnumValueData(Name = "image/gif", KeyValue = ".gif")]
        Gif,
        /// <summary>
        /// The JPEG
        /// </summary>
        [EnumValueData(Name = "image/jpeg", KeyValue = ".jpeg")]
        Jpeg,
        /// <summary>
        /// The JPG
        /// </summary>
        [EnumValueData(Name = "image/jpeg", KeyValue = ".jpg")]
        Jpg,
        /// <summary>
        /// The PNG
        /// </summary>
        [EnumValueData(Name = "image/png", KeyValue = ".png")]
        Png,

        /// <summary>
        /// The tif
        /// </summary>
        [EnumValueData(Name = "image/tif", KeyValue = ".tif")]
        Tif,

        /// <summary>
        /// The XLSX
        /// </summary>
        [EnumValueData(Name = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", KeyValue = ".xlsx")]
        Xlsx,
        /// <summary>
        /// The docx
        /// </summary>
        [EnumValueData(Name = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", KeyValue = ".docx")]
        Docx,
        /// <summary>
        /// The document
        /// </summary>
        [EnumValueData(Name = "application/msword", KeyValue = ".doc")]
        Doc,
        /// <summary>
        /// The XLS
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-excel", KeyValue = ".xls")]
        Xls,
        /// <summary>
        /// The text
        /// </summary>
        [EnumValueData(Name = "text/plain", KeyValue = ".txt")]
        Txt,

        /// <summary>
        /// The XLSM
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-excel.sheet.macroEnabled.12", KeyValue = ".xlsm")]
        xlsm,

        /// <summary>
        /// The MSG
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-outlook", KeyValue = ".msg")]
        msg,

        /// <summary>
        /// The RTF
        /// </summary>
        [EnumValueData(Name = "application/rtf", KeyValue = ".rtf")]
        rtf,

        /// <summary>
        /// The zip
        /// </summary>
        [EnumValueData(Name = "application/zip", KeyValue = ".zip")]
        zip,

        /// <summary>
        /// The XPS
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-xpsdocument", KeyValue = ".xps")]
        xps,

        /// <summary>
        /// The PTX
        /// </summary>
        [EnumValueData(Name = "application/rtf", KeyValue = ".ptx")]
        ptx,

        /// <summary>
        /// The odt
        /// </summary>
        [EnumValueData(Name = "application/vnd.oasis.opendocument", KeyValue = ".odt")]
        odt,

        /// <summary>
        /// The Gzip
        /// </summary>
        [EnumValueData(Name = "application/gzip", KeyValue = ".gz")]
        gzip,

        /// <summary>
        /// The rar
        /// </summary>
        [EnumValueData(Name = "application/x-rar-compressed", KeyValue = ".rar")]
        rar,
        /// <summary>
        /// The sevenz
        /// </summary>
        [EnumValueData(Name = "application/x-7z-compressed", KeyValue = ".7z")]
        sevenz,
        /// <summary>
        /// The PPT
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-powerpoint", KeyValue = ".ppt")]
        ppt,
        /// <summary>
        /// The PPS
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-powerpoint", KeyValue = ".pps")]
        pps,
        /// <summary>
        /// The PPTX
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-powerpoint", KeyValue = ".pptx")]
        pptx,
        /// <summary>
        /// The PPSX
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-powerpoint", KeyValue = ".ppsx")]
        ppsx,
        /// <summary>
        /// The pot
        /// </summary>
        [EnumValueData(Name = "application/vnd.ms-powerpoint", KeyValue = ".pot")]
        pot
    }

    /// <summary>
    /// Enum for cloud upload status
    /// </summary>
    public enum CloudUploadStatus
    {
        /// <summary>
        /// The local file not found
        /// </summary>
        [EnumValueData(Name = "LocalFileNotFound", KeyValue = "5")]
        LocalFileNotFound,
        /// <summary>
        /// The failed
        /// </summary>
        [EnumValueData(Name = "Failed", KeyValue = "0")]
        Failed,
        /// <summary>
        /// The ready for processing
        /// </summary>
        [EnumValueData(Name = "ReadyForProcessing", KeyValue = "-1")]
        ReadyForProcessing,
        /// <summary>
        /// The processing
        /// </summary>
        [EnumValueData(Name = "Processing", KeyValue = "-2")]
        Processing,
        /// <summary>
        /// The in cloud
        /// </summary>
        [EnumValueData(Name = "InCloud", KeyValue = "1")]
        InCloud,

        /// <summary>
        /// The in file system
        /// </summary>
        [EnumValueData(Name = "InFileSystem", KeyValue = "3")]
        InFileSystem,

        /// <summary>
        /// The deleted
        /// </summary>
        [EnumValueData(Name = "Deleted", KeyValue = "4")]
        Deleted
    }

    /// <summary>
    /// Enumeration to hold contract type for supplier
    /// </summary>
    public enum SupplierContractType
    {
        /// <summary>
        /// The marcas
        /// </summary>
        [EnumValueData(Name = "Marcas", KeyValue = "M")]
        Marcas,
        /// <summary>
        /// The preferred
        /// </summary>
        [EnumValueData(Name = "Preferred", KeyValue = "P")]
        Preferred,
        /// <summary>
        /// The contract
        /// </summary>
        [EnumValueData(Name = "Contract", KeyValue = "C")]
        Contract
    }

    /// <summary>
    /// Enumeration for type of Crew Status
    /// </summary>
    public enum CrewStatus
    {
        /// <summary>
        /// The onboard
        /// </summary>
        [EnumValueData(Name = "ONBOARD", KeyValue = "OB")]
        Onboard,
        /// <summary>
        /// The overlap
        /// </summary>
        [EnumValueData(Name = "OVERLAP", KeyValue = "OV")]
        Overlap,
        /// <summary>
        /// The NTBR
        /// </summary>
        [EnumValueData(Name = "UNDESIRABLE", KeyValue = "XX")]
        NTBR,
        /// <summary>
        /// The Retired
        /// </summary>
        [EnumValueData(Name = "LC: Retired", KeyValue = "RE")]
        Retired,
        /// <summary>
        /// The Left Company
        /// </summary>
        [EnumValueData(Name = "Left Company", KeyValue = "LL")]
        LeftCompany,
        /// <summary>
        /// The Died
        /// </summary>
        [EnumValueData(Name = "DECEASED", KeyValue = "DE")]
        Died,

        /// <summary>
        /// The Applicant
        /// </summary>
        [EnumValueData(Name = "APPLICANT", KeyValue = "AP")]
        Applicant,

        /// <summary>
        /// The Vacation
        /// </summary>
        [EnumValueData(Name = "VACATION", KeyValue = "LE")]
        Vacation,
        /// <summary>
        /// The In Transit
        /// </summary>
        [EnumValueData(Name = "Transit", KeyValue = "IT")]
        InTransit,

        /// <summary>
        /// The Training
        /// </summary>
        [EnumValueData(Name = "Training", KeyValue = "TR")]
        Training,

        /// <summary>
        /// The Expired
        /// </summary>
        [EnumValueData(Name = "Expired", KeyValue = "EX")]
        Expired,

        /// <summary>
        /// The assigned
        /// </summary>
        [EnumValueData(Name = "Assigned", KeyValue = "AS")]
        Assigned
    }

    /// <summary>
    /// The crew planning status
    /// </summary>
    public enum CrewPlanningStatus
    {
        /// <summary>
        /// The planning
        /// </summary>
        [EnumValueData(Name = "PL", KeyValue = "VSHP00000011")]
        Planned,

        /// <summary>
        /// The proposed
        /// </summary>
        [EnumValueData(Name = "PR", KeyValue = "VSHP00000001")]
        Proposed,

        /// <summary>
        /// The approved
        /// </summary>
        [EnumValueData(Name = "A", KeyValue = "VSHP00000002")]
        Approved,

        /// <summary>
        /// The ready
        /// </summary>
        [EnumValueData(Name = "RD", KeyValue = "VSHP00000004")]
        Ready,

        /// <summary>
        /// The released
        /// </summary>
        [EnumValueData(Name = "RL", KeyValue = "VSHP00000008")]
        Released,

        /// <summary>
        /// The rejected
        /// </summary>
        [EnumValueData(Name = "RJ", KeyValue = "VSHP00000003")]
        Rejected,

        /// <summary>
        /// The joined
        /// </summary>
        [EnumValueData(Name = "J", KeyValue = "VSHP00000007")]
        Joined,

        /// <summary>
        /// The cancelled
        /// </summary>
        [EnumValueData(Name = "C", KeyValue = "VSHP00000012")]
        Cancelled,

        /// <summary>
        /// The plan proposed
        /// </summary>
        [EnumValueData(Name = "PP", KeyValue = "VSHP00000013")]
        PlanProposed,

        /// <summary>
        /// The To Be Assigned
        /// </summary>
        [EnumValueData(Name = "TBA", KeyValue = "VSHP00000014")]
        ToBeAssigned
    }

    /// <summary>
    /// Crew Service Status Filter
    /// </summary>
    public enum CrewServiceStatusFilter
    {
        /// <summary>
        /// The company experience
        /// </summary>
        [EnumValueData(Name = "CompanyExperience", KeyValue = "CompanyExperience")]
        CompanyExperience = 1,

        /// <summary>
        /// The previous experience
        /// </summary>
        [EnumValueData(Name = "PreviousExperience", KeyValue = "PreviousExperience")]
        PreviousExperience = 2,

        /// <summary>
        /// The onshore
        /// </summary>
        [EnumValueData(Name = "Onshore", KeyValue = "Onshore")]
        Onshore = 3,
    }

    /// <summary>
    /// Crew Flight Type
    /// </summary>
    public enum CrewADNOCFlightType
    {
        /// <summary>
        /// The inbound
        /// </summary>
        [EnumValueData(Name = "Inbound / FDS", KeyValue = "0")]
        Inbound = 0,

        /// <summary>
        /// The outbound
        /// </summary>
        [EnumValueData(Name = "Outbound / LDS", KeyValue = "1")]
        Outbound = 1

    }

    /// <summary>
    /// Experience Type
    /// </summary>
    public enum ExperienceType
    {
        /// <summary>
        /// The rank
        /// </summary>
        [EnumValueData(Name = "Rank", KeyValue = "Rank")]
        Rank,

        /// <summary>
        /// The vessel type group
        /// </summary>
        [EnumValueData(Name = "VesselTypeGroup", KeyValue = "VesselTypeGroup")]
        VesselTypeGroup,

        /// <summary>
        /// The group
        /// </summary>
        [EnumValueData(Name = "Group", KeyValue = "Group")]
        Group,

        /// <summary>
        /// The owner
        /// </summary>
        [EnumValueData(Name = "Owner", KeyValue = "Owner")]
        Owner,

        /// <summary>
        /// The Vships Exp (C)
        /// </summary>
        [EnumValueData(Name = "CompanyExperience", KeyValue = "CompanyExperience")]
        CompanyExperience,
    }

    /// <summary>
    /// The Crew Service Active Status
    /// </summary>
    public enum CrewServiceActiveStatus
    {
        /// <summary>
        /// The cancelled
        /// </summary>
        [EnumValueData(Name = "Cancelled", KeyValue = "0")]
        Cancelled,

        /// <summary>
        /// The actual active
        /// </summary>
        [EnumValueData(Name = "Actual Active", KeyValue = "1")]
        ActualActive,

        /// <summary>
        /// The actual historical
        /// </summary>
        [EnumValueData(Name = "Actual Historical", KeyValue = "2")]
        ActualHistorical,

        /// <summary>
        /// The future
        /// </summary>
        [EnumValueData(Name = "Future", KeyValue = "3")]
        Future
    }

    /// <summary>
    /// Crew Master Data Tables
    /// </summary>
    public enum CrewMasterDataTables
    {
        /// <summary>
        /// The ContractLengthUnits
        /// </summary>
        [EnumValueData(Name = "Contract Length Units", KeyValue = "ContractLengthUnits")]
        ContractLengthUnits,

        /// <summary>
        /// The Personal_Note_Type
        /// </summary>
        [EnumValueData(Name = "Personal Note Type", KeyValue = "Personal_Note_Type")]
        PersonalNoteType,

        /// <summary>
        /// The SRV_Event_Types
        /// </summary>
        [EnumValueData(Name = "Service Event Types", KeyValue = "SRV_Event_Types")]
        ServiceEventTypes,

        /// <summary>
        /// The ServiceNoteType
        /// </summary>
        [EnumValueData(Name = "Service Note Type", KeyValue = "ServiceNoteType")]
        ServiceNoteType,

        /// <summary>
        /// The nationality type
        /// </summary>
        [EnumValueData(Name = "Nationality Type", KeyValue = "NATIONALITY_TYPE")]
        NationalityType,

        /// <summary>
        /// The plan resrticted type
        /// </summary>
        [EnumValueData(Name = "Plan Restrict Type", KeyValue = "PLAN_RESTRICT_TYPE")]
        PLAN_RESRTICTED_TYPE,

        /// <summary>
        /// The document reject reason
        /// </summary>
        [EnumValueData(Name = "Document Reject Reason", KeyValue = "DocumentRejectReason")]
        Document_Reject_Reason,

        /// <summary>
        /// The crew planned released subStatus
        /// </summary>
        [EnumValueData(Name = "CRW Planned Released SubStatus", KeyValue = "CRWPlannedReleasedSubStatus")]
        CRW_Planned_Released_SubStatus,

        /// <summary>
        /// The crew pool team member role
        /// </summary>
        [EnumValueData(Name = "Crew Pool Team Member Role", KeyValue = "CrewCell")]
        CrewPoolTeamMemberRole,

        /// <summary>
        /// The task type group
        /// </summary>
        [EnumValueData(Name = "Task Type Group", KeyValue = "TASK_TYPE_GROUP")]
        TASK_TYPE_GROUP,

        /// <summary>
        /// SRM
        /// </summary>
        [EnumValueData(Name = "SRM", KeyValue = "SRM")]
        SRM,

        /// <summary>
        /// Crew Appraisal
        /// </summary>
        [EnumValueData(Name = "CRWAppraisal", KeyValue = "CRWAppraisal")]
        CRWAppraisal,

        /// <summary>
        /// The Medical Disability
        /// </summary>
        [EnumValueData(Name = "Medical Disability", KeyValue = "MedicalDisability")]
        MedicalDisability,

        /// <summary>
        /// The Medical Case Status
        /// </summary>
        [EnumValueData(Name = "Medical Case Status", KeyValue = "MedicalCaseStatus")]
        MedicalCaseStatus,

        /// <summary>
        /// The medical status
        /// </summary>
        [EnumValueData(Name = "Medical Status", KeyValue = "MedicalStatus")]
        MedicalStatus,

        /// <summary>
        /// The medical classification
        /// </summary>
        [EnumValueData(Name = "Medical Classification", KeyValue = "MedicalClassification")]
        MedicalClassification,

        /// <summary>
        /// The CRW test purpose
        /// </summary>
        [EnumValueData(Name = "Crewing Test Purpose", KeyValue = "CRWTestPurpose")]
        CRWTestPurpose,

        /// <summary>
        /// The CRW chart work
        /// </summary>
        [EnumValueData(Name = "Crewing Chart Purpose", KeyValue = "CRWChartWork")]
        CRWChartWork,

        /// <summary>
        /// The CRW mand question
        /// </summary>
        [EnumValueData(Name = "Crewing Mandatory Questions", KeyValue = "CRWMandQuestion")]
        CRWMandQuestion,

        /// <summary>
        /// The crew requirement status
        /// </summary>
        [EnumValueData(Name = "Crew Requirement Status", KeyValue = "CrewRequirementStatus")]
        CrewRequirementStatus,

        /// <summary>
        /// The Months for Reliever not Required
        /// </summary>
        [EnumValueData(Name = "Months for Reliever not Required", KeyValue = "MonthsForRelieverNotRequired")]
        CRWCLVesCrewList,

        /// <summary>
        /// The dispensation status
        /// </summary>
        [EnumValueData(Name = "Statuses for Dispensations", KeyValue = "DispensationStatus")]
        DispensationStatus,

        /// <summary>
        /// The dispensation type
        /// </summary>
        [EnumValueData(Name = "Types for Dispensations", KeyValue = "DispensationType")]
        DispensationType,

        /// <summary>
        /// The ves wage scale req type
        /// </summary>
        [EnumValueData(Name = "Vessel Wage Scale Required Types", KeyValue = "VesWageScaleReqType")]
        VesWageScaleReqType,

        /// <summary>
        /// The crew requirement rejection reason
        /// </summary>
        [EnumValueData(Name = "Crew Requirement Rejection Reason", KeyValue = "CrewRequirementRejectionReason")]
        CrewRequirementRejectionReason,

        /// <summary>
        /// The crew requirement contract type
        /// </summary>
        [EnumValueData(Name = "Crew Requirement Contract Type", KeyValue = "CrewRequirementContractType")]
        CrewRequirementContractType,

        /// <summary>
        /// The SFC type
        /// </summary>
        [EnumValueData(Name = "SFC TYPE", KeyValue = "SFC_TYPE")]
        SFCType,

        /// <summary>
        /// The Crew Cell Type
        /// </summary>
        [EnumValueData(Name = "Cell Type", KeyValue = "CREW_CELL_TYPE")]
        CrewCellType,

        /// <summary>
        /// Vessel Training System
        /// </summary>
        [EnumValueData(Name = "Vessel Training System", KeyValue = "VesselTrainingSystem")]
        VesselTrainingSystem,

        /// <summary>
        /// The Applicable Seniority
        /// </summary>
        [EnumValueData(Name = "Applicable Seniority", KeyValue = "ApplicableSeniority")]
        ApplicableSeniority,

        /// <summary>
        /// The covid vaccine type
        /// </summary>
        [EnumValueData(Name = "Covid Vaccine Type", KeyValue = "CovidVaccineType")]
        CovidVaccineType,

        /// <summary>
        /// The covid vaccine provider
        /// </summary>
        [EnumValueData(Name = "Covid Vaccine Provider", KeyValue = "CovidVaccineProvider")]
        CovidVaccineProvider,

        /// <summary>
        /// The covid document type
        /// </summary>
        [EnumValueData(Name = "Covid Document Type", KeyValue = "CovidDocumentType")]
        CovidDocumentType
    }

    /// <summary>
    /// Crew Stages
    /// </summary>
    public enum CrewStageFilter
    {
        /// <summary>
        /// Onboard
        /// </summary>
        [EnumValueData(Name = "Onboard", KeyValue = "Onboard")]
        Onboard,

        /// <summary>
        /// The overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue,

        /// <summary>
        /// The unplanned berth
        /// </summary>
        [EnumValueData(Name = "Unplanned Berth", KeyValue = "UnplannedBerth")]
        UnplannedBerth,

        /// <summary>
        /// The change last x days
        /// </summary>
        [EnumValueData(Name = "Crew Changes Last 7 Days", KeyValue = "ChangeLastXDays")]
        ChangeLastXDays
    }

    /// <summary>
    /// 
    /// </summary>
    public enum POTemplateSelection
    {
        /// <summary>
        /// The supplier
        /// </summary>
        [EnumValueData(Name = "Supplier", KeyValue = "Supplier")]
        Supplier,

        /// <summary>
        /// The agent
        /// </summary>
        [EnumValueData(Name = "Agent", KeyValue = "Agent")]
        Agent,

        /// <summary>
        /// The warehouse
        /// </summary>
        [EnumValueData(Name = "Warehouse", KeyValue = "Warehouse")]
        Warehouse
    }

    /// <summary>
    /// Inspector Entity Selections
    /// </summary>
    public enum InspectorEntity
    {
        /// <summary>
        /// The ship staff
        /// </summary>
        [EnumValueData(Name = "Ship Staff", KeyValue = "Ship Staff")]
        ShipStaff,

        /// <summary>
        /// The third party
        /// </summary>
        [EnumValueData(Name = "Third Party", KeyValue = "Third Party")]
        ThirdParty,

        /// <summary>
        /// The office
        /// </summary>
        [EnumValueData(Name = "Office", KeyValue = "Office")]
        Office
    }

    /// <summary>
    /// Location values for Inspection Detail
    /// </summary>
    public enum InspectionLocation
    {
        /// <summary>
        /// The in port
        /// </summary>
        [EnumValueData(Name = "In Port", KeyValue = "In Port")]
        InPort,

        /// <summary>
        /// The sailing
        /// </summary>
        [EnumValueData(Name = "Sailing", KeyValue = "Sailing")]
        Sailing
    }

    /// <summary>
    /// ReasonType for Quote Authorization
    /// </summary>
    public enum ReasonType
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "", KeyValue = null)]
        All,

        /// <summary>
        /// The budget justification
        /// </summary>
        [EnumValueData(Name = "BudgetJustification", KeyValue = "B")]
        BudgetJustification,

        /// <summary>
        /// The rejection
        /// </summary>
        [EnumValueData(Name = "Rejection", KeyValue = "R")]
        Rejection
    }


    /// <summary>
    /// AuxCodeType
    /// </summary>
    public enum AuxCodeType
    {
        /// <summary>
        /// The seasonal
        /// </summary>
        [EnumValueData(Name = "Seasonal", KeyValue = "6")]
        Seasonal,
        /// <summary>
        /// The general1
        /// </summary>
        [EnumValueData(Name = "General 1", KeyValue = "7")]
        General1,
        /// <summary>
        /// The general3
        /// </summary>
        [EnumValueData(Name = "General 3", KeyValue = "10")]
        General3,
        /// <summary>
        /// The department
        /// </summary>
        [EnumValueData(Name = "Department", KeyValue = "2")]
        Department,
        /// <summary>
        /// The employee
        /// </summary>
        [EnumValueData(Name = "Employee", KeyValue = "3")]
        Employee,
        /// <summary>
        /// The project
        /// </summary>
        [EnumValueData(Name = "Project", KeyValue = "4")]
        Project,
        /// <summary>
        /// The group
        /// </summary>
        [EnumValueData(Name = "Group", KeyValue = "5")]
        Group,
        /// <summary>
        /// The expense
        /// </summary>
        [EnumValueData(Name = "Expense", KeyValue = "6")]
        Expense,
        /// <summary>
        /// The aux7
        /// </summary>
        [EnumValueData(Name = "Aux7", KeyValue = "7")]
        Aux7,
        /// <summary>
        /// The aux8
        /// </summary>
        [EnumValueData(Name = "Aux8", KeyValue = "8")]
        Aux8,
        /// <summary>
        /// The aux9
        /// </summary>
        [EnumValueData(Name = "Aux9", KeyValue = "9")]
        Aux9
    }

    /// <summary>
    /// Module Enum
    /// </summary>
    public enum ModuleEnum
    {
        /// <summary>
        /// The purchasing
        /// </summary>
        [EnumValueData(Name = "Purchasing", KeyValue = "GLAS00000014")]
        Purchasing,

        /// <summary>
        /// The invoicing
        /// </summary>
        [EnumValueData(Name = "Invoicing", KeyValue = "GLAS00000006")]
        Invoicing,

        /// <summary>
        /// The job safety analysis
        /// </summary>
        [EnumValueData(Name = "JobSafetyAnalysis", KeyValue = "GLAS00000086")]
        JobSafetyAnalysis,

        /// <summary>
        /// The weekly minutes
        /// </summary>
        [EnumValueData(Name = "WeeklyMinutes", KeyValue = "GLAS00000121")]
        WeeklyMinutes,

        /// <summary>
        /// The environmental management
        /// </summary>
        [EnumValueData(Name = "EnvironmentalManagement", KeyValue = "GLAS00000060")]
        EnvironmentalManagement,

        /// <summary>
        /// The management of change
        /// </summary>
        [EnumValueData(Name = "ManagementOfChange", KeyValue = "GLAS00000079")]
        ManagementOfChange,

        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "GLAS00000017")]
        HazOcc,

        /// <summary>
        /// The voyage reporting.
        /// </summary>
        [EnumValueData(Name = "Voyage Reporting", KeyValue = "GLAS00000021")]
        VoyageReporting
    }

    /// <summary>
    /// WorkFlowFunctionalityEnum
    /// </summary>
    public enum WorkFlowFunctionalityEnum
    {
        /// <summary>
        /// Change Accruals
        /// </summary>
        [EnumValueData(Name = "Change Accruals", KeyValue = "Change Accruals")]
        ChangeAccruals,

        /// <summary>
        /// Comparator Authorisation
        /// </summary>
        [EnumValueData(Name = "Comparator Authorisation", KeyValue = "Comparator Authorisation")]
        ComparatorAuthorisation,

        /// <summary>
        /// Order Authorization
        /// </summary>
        [EnumValueData(Name = "Order Authorization", KeyValue = "Order Authorization")]
        OrderAuthorization,

        /// <summary>
        /// The non po invoice authorization
        /// </summary>
        [EnumValueData(Name = "Non PO Invoice Authorization", KeyValue = "Non PO Invoice Authorization")]
        NonPOInvoiceAuthorization,

        /// <summary>
        /// The sludge discharge
        /// </summary>
        [EnumValueData(Name = "Sludge Discharge", KeyValue = "Sludge Discharge")]
        SludgeDischarge,

        /// <summary>
        /// The bilge waste
        /// </summary>
        [EnumValueData(Name = "Bilge Waste", KeyValue = "Bilge Waste")]
        BilgeWaste,

        /// <summary>
        /// The cargo discharge
        /// </summary>
        [EnumValueData(Name = "Cargo Discharge", KeyValue = "Cargo Discharge")]
        CargoDischarge,

        /// <summary>
        /// The garbage discharge
        /// </summary>
        [EnumValueData(Name = "Garbage Discharge", KeyValue = "Garbage Discharge")]
        GarbageDischarge,

        /// <summary>
        /// The sewage discharge
        /// </summary>
        [EnumValueData(Name = "Sewage Discharge", KeyValue = "Sewage Discharge")]
        SewageDischarge,

        /// <summary>
        /// The ozone depleting substances
        /// </summary>
        [EnumValueData(Name = "Ozone Depleting Substances", KeyValue = "Ozone Depleting Substances")]
        OzoneDepletingSubstances,

        /// <summary>
        /// The inadequacy
        /// </summary>
        [EnumValueData(Name = "Inadequacy", KeyValue = "Inadequacy")]
        Inadequacy,

        /// <summary>
        /// The advance notification
        /// </summary>
        [EnumValueData(Name = "Advance Notification", KeyValue = "Advance Notification")]
        AdvanceNotification,

        /// <summary>
        /// The dispute comparator invoice
        /// </summary>
        [EnumValueData(Name = "Dispute Comparator Invoice", KeyValue = "Dispute Comparator Invoice")]
        DisputeComparatorInvoice,

        /// <summary>
        /// The moc stages
        /// </summary>
        [EnumValueData(Name = "MOC Stages", KeyValue = "MOC Stages")]
        MOCStages,

        /// <summary>
        /// The moc task detail
        /// </summary>
        [EnumValueData(Name = "MOC Task Detail", KeyValue = "MOC Task Detail")]
        MOCTaskDetail,

        /// <summary>
        /// The failure of monitoring control
        /// </summary>
        [EnumValueData(Name = "Failure of Monitoring/Control", KeyValue = "Failure of Monitoring/Control")]
        FailureOfMonitoringControl,

        /// <summary>
        /// The voyage weekly rob report
        /// </summary>
        [EnumValueData(Name = "Voyage/Weekly ROB Report", KeyValue = "Voyage/Weekly ROB Report")]
        VoyageWeeklyROBReport,

        /// <summary>
        /// The service of monitoring control
        /// </summary>
        [EnumValueData(Name = "Service of Monitoring/Control", KeyValue = "Service of Monitoring/Control")]
        ServiceOfMonitoringControl,

        /// <summary>
        /// The ballasting cleaning fuel tanks
        /// </summary>
        [EnumValueData(Name = "Ballasting/Cleaning Fuel Tanks", KeyValue = "Ballasting/Cleaning Fuel Tanks")]
        BallastingCleaningFuelTanks,

        /// <summary>
        /// The discharge of dirty ballast
        /// </summary>
        [EnumValueData(Name = "Discharge of Dirty Ballast", KeyValue = "Discharge of Dirty Ballast")]
        DischargeOfDirtyBallast,

        /// <summary>
        /// The posbunker
        /// </summary>
        [EnumValueData(Name = "POSBUNKER", KeyValue = "POSBUNKER")]
        POSBUNKER,

        /// <summary>
        /// The additional operational procedures and general remarks
        /// </summary>
        [EnumValueData(Name = "Additional Operational Procedures And General Remarks", KeyValue = "Additional Operational Procedures And General Remarks")]
        AdditionalOperationalProceduresAndGeneralRemarks
    }

    /// <summary>
    /// DefectCriticalStatus
    /// </summary>
    public enum DefectCriticalStatus
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,
        /// <summary>
        /// Only Critical
        /// </summary>
        [EnumValueData(Name = "Only Critical", KeyValue = "OnlyCritical")]
        OnlyCritical
    }

    /// <summary>
    /// DefectDueStatus
    /// </summary>
    public enum DefectDueStatus
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,
        /// <summary>
        /// Due
        /// </summary>
        [EnumValueData(Name = "Due", KeyValue = "Due")]
        Due,
        /// <summary>
        /// Overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue
    }

    /// <summary>
    /// Enum for defect attribute
    /// </summary>
    public enum DefectAttribute
    {
        /// <summary>
        /// The accessory work
        /// </summary>
        [EnumValueData(Name = "AccessoryWork", KeyValue = "AccessoryWork")]
        AccessoryWork = 0,

        /// <summary>
        /// The site type
        /// </summary>
        [EnumValueData(Name = "SiteType", KeyValue = "SiteType")]
        SiteType = 1,

        /// <summary>
        /// The staff type
        /// </summary>
        [EnumValueData(Name = "StaffType", KeyValue = "StaffType")]
        StaffType = 2,

        /// <summary>
        /// The work order category
        /// </summary>
        [EnumValueData(Name = "WorkOrderCategory", KeyValue = "WorkOrderCategory")]
        WorkOrderCategory = 3,

        /// <summary>
        /// The work order priority
        /// </summary>
        [EnumValueData(Name = "WorkOrderPriority", KeyValue = "WorkOrderPriority")]
        WorkOrderPriority = 4,

        /// <summary>
        /// The work order status
        /// </summary>
        [EnumValueData(Name = "WorkOrderStatus", KeyValue = "WorkOrderStatus")]
        WorkOrderStatus = 5,

        /// <summary>
        /// The defect impact
        /// </summary>
        [EnumValueData(Name = "DefectImpact", KeyValue = "DefectImpact")]
        DefectImpact = 6,

        /// <summary>
        /// The off hire period
        /// </summary>
        [EnumValueData(Name = "OffHirePeriod", KeyValue = "OffHirePeriod")]
        OffHirePeriod = 7,

        /// <summary>
        /// The specific
        /// </summary>
        [EnumValueData(Name = "Specific", KeyValue = "Specific")]
        Specific = 8
    }

    /// <summary>
    /// Enum for VesselCertificateStatus
    /// </summary>
    public enum VesselCertificateStatus
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,

        /// <summary>
        /// The Active
        /// </summary>
        [EnumValueData(Name = "Active", KeyValue = "Active")]
        Active,

        /// <summary>
        /// The inactive
        /// </summary>
        [EnumValueData(Name = "Inactive", KeyValue = "Inactive")]
        Inactive,

        /// <summary>
        /// The deleted
        /// </summary>
        [EnumValueData(Name = "Deleted", KeyValue = "Deleted")]
        Deleted
    }

    /// <summary>
    /// Report List
    /// </summary>
    public enum ReportMaster
    {
        /// <summary>
        /// The purchasing invoice comparator report
        /// </summary>
        [EnumValueData(Name = "Purchasing Invoice Comparator Report", KeyValue = "PurchasingInvoiceComparatorReport.rpt")]
        PurchasingInvoiceComparatorReport,

        /// <summary>
        /// The finance accounting company audit log report
        /// </summary>
        [EnumValueData(Name = "Finance Accounting Company Audit Log Report", KeyValue = "FinanceAccountCompanyAuditLogsReport.rpt")]
        FinanceAccountingCompanyAuditlogReport,

        /// <summary>
        /// The finance accounting company memo report
        /// </summary>
        [EnumValueData(Name = "Finance Accounting Company Memo Report", KeyValue = "FinanceAccountCompanyMemoPrintReport.rpt")]
        FinanceAccountingCompanyMemoReport,

        /// <summary>
        /// The purchasing outstanding order by account report
        /// </summary>
        [EnumValueData(Name = "Purchasing Outstanding Order By Account Report", KeyValue = "Purchasing_OutstandingOrderByAccountReport.rpt")]
        PurchasingOutstandingOrderByAccountReport,

        /// <summary>
        /// The purchasing outstanding delivery information report
        /// </summary>
        [EnumValueData(Name = "Purchasing Outstanding Delivery Information Report", KeyValue = "Purchasing_OutstandingDeliveryInformationReport.rpt")]
        PurchasingOutstandingDeliveryInformationReport,

        /// <summary>
        /// The purchasing vessel stored orders report
        /// </summary>
        [EnumValueData(Name = "Purchasing Vessel Stored Orders Report", KeyValue = "Purchasing_VesselStoredOrdersReport.rpt")]
        PurchasingVesselStoredOrdersReport,

        /// <summary>
        /// The purchasing order detail report
        /// </summary>
        [EnumValueData(Name = "Purchasing Order Detail Report", KeyValue = "Purchasing_OrderDetailReport.rpt")]
        PurchasingOrderDetailReport,
        /// <summary>
        /// The purchasing order detail report
        /// </summary>
        [EnumValueData(Name = "Purchasing Order Detail Excel Report", KeyValue = "PurchasingOrderDetailExcelReport.rpt")]
        PurchasingOrderDetailExcelReport,
        /// <summary>
        /// The purchasing vessel order status report
        /// </summary>
        [EnumValueData(Name = "Purchasing Vessel Order Status Report", KeyValue = "Purchasing_VesselOrderStatusReport.rpt")]
        PurchasingVesselOrderStatusReport,

        /// <summary>
        /// The purchasing supplier purchase orders report
        /// </summary>
        [EnumValueData(Name = "Purchasing Supplier Purchase Orders Report", KeyValue = "Purchasing_SupplierPurchaseOrdersReport.rpt")]
        PurchasingSupplierPurchaseOrdersReport,

        /// <summary>
        /// The purchasing port manifest report
        /// </summary>
        [EnumValueData(Name = "Purchasing Port Manifest Report", KeyValue = "Purchasing_PortManifestReport.rpt")]
        PurchasingPortManifestReport,

        /// <summary>
        /// The purchasing issue flight itinerary report
        /// </summary>
        [EnumValueData(Name = "Purchasing Issue Flight Itinerary Report", KeyValue = "Purchasing_IssueFlightItineraryReport.rpt")]
        PurchasingIssueFlightItineraryReport,

        /// <summary>
        /// The purchasing order accruals report
        /// </summary>
        [EnumValueData(Name = "Purchasing Order Accruals Report", KeyValue = "Purchasing_OrderAccrualsReport.rpt")]
        PurchasingOrderAccrualsReport,

        /// <summary>
        /// The purchasing request for quotation report
        /// </summary>
        [EnumValueData(Name = "Purchasing Request For Quotation Report", KeyValue = "Purchasing_RequestForQuotationReport.rpt")]
        PurchasingRequestForQuotationReport,

        /// <summary>
        /// The purchasing entity request for quotation report
        /// </summary>
        [EnumValueData(Name = "Purchasing Entity Request For Quotation Report", KeyValue = "Purchasing_Entity_RequestForQuotationReport.rpt")]
        PurchasingEntityRequestForQuotationReport,

        /// <summary>
        /// The purchasing issue purchase order report
        /// </summary>
        [EnumValueData(Name = "Purchasing Issue Purchase Order Report", KeyValue = "Purchasing_IssuePurchaseOrderReport.rpt")]
        PurchasingIssuePurchaseOrderReport,

        /// <summary>
        /// The purchasing issue freight order report
        /// </summary>
        [EnumValueData(Name = "Purchasing Issue Freight Order Report", KeyValue = "Purchasing_IssueFreightOrderReport.rpt")]
        PurchasingIssueFreightOrderReport,

        /// <summary>
        /// The purchasing notification report
        /// </summary>
        [EnumValueData(Name = "Purchasing Notification Report", KeyValue = "Purchasing_NotificationReport.rpt")]
        PurchasingNotificationReport,

        /// <summary>
        /// The purchasing memo report
        /// </summary>
        [EnumValueData(Name = "Purchasing Memo Report", KeyValue = "Purchasing_MemoReport.rpt")]
        PurchasingMemoReport,

        /// <summary>
        /// The purchasing audit log report
        /// </summary>
        [EnumValueData(Name = "Purchasing Audit Log Report", KeyValue = "Purchasing_AuditLogReport.rpt")]
        PurchasingAuditLogReport,

        /// <summary>
        /// The purchasing export analysis report
        /// </summary>
        [EnumValueData(Name = "Purchasing Export Analysis Report", KeyValue = "PurchasingExportAnalysisTabReport.rpt")]
        PurchasingExportAnalysisReport,

        /// <summary>
        /// The purchasing disputed invoices report
        /// </summary>
        [EnumValueData(Name = "Purchasing Disputed Invoices Report", KeyValue = "PurchasingDisputedInvoicesReport.rpt")]
        PurchasingDisputedInvoicesReport,

        /// <summary>
        /// The purchasing comparator exception report
        /// </summary>
        [EnumValueData(Name = "Purchasing Comparator Exception Report", KeyValue = "PurchasingComparatorExceptionReport.rpt")]
        PurchasingComparatorExceptionReport,

        /// <summary>
        /// The finance general ledger audit log report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Audit Log Report", KeyValue = "FinanceGeneralLedgerAuditLogReport.rpt")]
        FinanceGeneralLedgerAuditLogReport,

        /// <summary>
        /// The finance general ledger memo report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Memo Report", KeyValue = "FinanceGeneralLedgerMemoReport.rpt")]
        FinanceGeneralLedgerMemoReport,

        /// <summary>
        /// The finance general ledger transaction report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Transaction Report", KeyValue = "FinanceGeneralLedgerReport.rpt")]
        FinanceGeneralLedgerTransactionReport,

        /// <summary>
        /// The finance general ledger transaction report export
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Transaction Report Export", KeyValue = "FinanceGeneralLedgerReportExport.rpt")]
        FinanceGeneralLedgerTransactionReportExport,

        /// <summary>
        /// The finance invoice selected for payment report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Selected For Payment Report", KeyValue = "FinanceInvSelectedForPaymentReport.rpt")]
        FinanceInvoiceSelectedForPaymentReport,

        /// <summary>
        /// The finance inv selected for payment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Selected For Payment Excel Report", KeyValue = "FinanceInvSelectedForPaymentExcelReport.rpt")]
        FinanceInvSelectedForPaymentExcelReport,

        /// <summary>
        /// The finance invoice confirm payment report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Confirm Payment Report", KeyValue = "FinanceInvConfirmedForPaymentReport.rpt")]
        FinanceInvoiceConfirmPaymentReport,

        /// <summary>
        /// The finance invoice finalise payment report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Finalise Payment Report", KeyValue = "FinanceInvoiceFinalisePaymentReport.rpt")]
        FinanceInvoiceFinalisePaymentReport,

        /// <summary>
        /// The finance sales invoice report
        /// </summary>
        [EnumValueData(Name = "Finance Sales Invoice Report", KeyValue = "FinanceSalesInvoiceReport.rpt")]
        FinanceSalesInvoiceReport,

        /// <summary>
        /// The finance chart detail report
        /// </summary>
        [EnumValueData(Name = "Finance Chart Detail Report", KeyValue = "FinanceChartDetailReport.rpt")]
        FinanceChartDetailReport,

        /// <summary>
        /// The finance operating chart audit log report
        /// </summary>
        [EnumValueData(Name = "Finance Operating Chart Audit Log Report", KeyValue = "FinanceOperatingChartAuditLogReport.rpt")]
        FinanceOperatingChartAuditLogReport,

        /// <summary>
        /// The finance client chart audit log report
        /// </summary>
        [EnumValueData(Name = "Finance Client Chart Audit Log Report", KeyValue = "FinanceClientChartAuditLogReport.rpt")]
        FinanceClientChartAuditLogReport,

        /// <summary>
        /// The finance company bank audit log report
        /// </summary>
        [EnumValueData(Name = "Finance Company Bank Audit Log Report", KeyValue = "FinanceCompanyBankAuditLogReport.rpt")]
        FinanceCompanyBankAuditLogReport,

        /// <summary>
        /// The user role details report
        /// </summary>
        [EnumValueData(Name = "User Role Details Report", KeyValue = "UserRoleDetailsReport.rpt")]
        UserRoleDetailsReport,

        /// <summary>
        /// The finance commitment details report
        /// </summary>
        [EnumValueData(Name = "Finance Commitment Details Report", KeyValue = "FinanceCommitmentDetailsReport.rpt")]
        FinanceCommitmentDetailsReport,

        /// <summary>
        /// The finance latest currency rate report
        /// </summary>
        [EnumValueData(Name = "Finance Latest Currency Rate Report", KeyValue = "FinanceLatestCurrencyRateReport.rpt")]
        FinanceLatestCurrencyRateReport,

        /// <summary>
        /// The finance latest currency rate excel report
        /// </summary>
        [EnumValueData(Name = "Finance Latest Currency Rate Excel Report", KeyValue = "FinanceLatestCurrencyRateExcelReport.rpt")]
        FinanceLatestCurrencyRateExcelReport,

        /// <summary>
        /// The finance account left management with AP active report
        /// </summary>
        [EnumValueData(Name = "Finance Account Left Management With AP Active Report", KeyValue = "FinanceAccountLeftManagementWithAPActiveReport.rpt")]
        FinanceAccountLeftManagementWithAPActiveReport,

        /// <summary>
        /// The finance account fixed asset list report
        /// </summary>
        [EnumValueData(Name = "Finance Account Fixed Asset List Report", KeyValue = "FinanceAccountFixedAssetListReport.rpt")]
        FinanceAccountFixedAssetListReport,

        /// <summary>
        /// The control permissions Report report
        /// </summary>
        [EnumValueData(Name = "Control Permissions Report", KeyValue = "ControlPermissionsReport.rpt")]
        ControlPermissionsReport,

        /// <summary>
        /// The finance account controller audit log report
        /// </summary>
        [EnumValueData(Name = "Finance Account Controller Audit Log Report", KeyValue = "FinanceAccountControllerAuditLogReport.rpt")]
        FinanceAccountControllerAuditLogReport,

        /// <summary>
        /// The finance client chart mapping report
        /// </summary>
        [EnumValueData(Name = "Finance Client Chart Mapping Report", KeyValue = "FinanceClientChartMappingReport.rpt")]
        FinanceClientChartMappingReport,


        /// <summary>
        /// The finance entity roll and clear report
        /// </summary>
        [EnumValueData(Name = "Finance Entity Roll And Clear Report", KeyValue = "FinanceEntityRollAndClearReport.rpt")]
        FinanceEntityRollAndClearReport,

        /// <summary>
        /// The finance saga data extract report
        /// </summary>
        [EnumValueData(Name = "Finance Saga Data Extract Report", KeyValue = "FinanceSagaDataExtractReport.rpt")]
        FinanceSagaDataExtractReport,

        /// <summary>
        /// The finance vessel roll and clear report
        /// </summary>
        [EnumValueData(Name = "Finance Vessel Roll And Clear Report", KeyValue = "FinanceVesselRollAndClearReport.rpt")]
        FinanceVesselRollAndClearReport,

        /// <summary>
        /// The finance invoice header sales reval report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Header Sales Reval Report", KeyValue = "FinanceInvoiceHeaderSalesRevalReport.rpt")]
        FinanceInvoiceHeaderSalesRevalReport,

        /// <summary>
        /// The finance invoice header acc payable reval report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Header AccPayable Reval Report", KeyValue = "FinanceInvoiceHeaderAccPayableRevalReport.rpt")]
        FinanceInvoiceHeaderAccPayableRevalReport,

        /// <summary>
        /// The finance single currency reval admin report
        /// </summary>
        [EnumValueData(Name = "Finance Single Currency Reval Admin Report", KeyValue = "FinanceSingleCurrencyRevalAdminReport.rpt")]
        FinanceSingleCurrencyRevalAdminReport,


        /// <summary>
        /// The finance inter company settlement GL report
        /// </summary>
        [EnumValueData(Name = "Finance Inter Company Settlement GL Report", KeyValue = "FinanceInterCompanySettlementGLReport.rpt")]
        FinanceInterCompanySettlementGLReport,

        /// <summary>
        /// The finance inter company settlement report
        /// </summary>
        [EnumValueData(Name = "Finance Inter Company Settlement Report", KeyValue = "FinanceInterCompanySettlementReport.rpt")]
        FinanceInterCompanySettlementReport,


        /// <summary>
        /// The finance order accrual report
        /// </summary>
        [EnumValueData(Name = "Finance Order Accrual Report", KeyValue = "FinanceOrderAccrualReport.rpt")]
        FinanceOrderAccrualReport,


        /// <summary>
        /// The finance operational cost drill down internal report
        /// </summary>
        [EnumValueData(Name = "Finance Operational Cost Drill Down Internal Report", KeyValue = "FinanceOperationalCostDrillDownInternalReport.rpt")]
        FinanceOperationalCostDrillDownInternalReport,

        /// <summary>
        /// The finance operational cost drill down period report
        /// </summary>
        [EnumValueData(Name = "Finance Operational Cost Drill Down Period Report", KeyValue = "FinanceOperationalCostDrillDownPeriodReport.rpt")]
        FinanceOperationalCostDrillDownPeriodReport,

        /// <summary>
        /// The finance order accrual report export
        /// </summary>
        [EnumValueData(Name = "Finance Order Accrual Report Export", KeyValue = "FinanceOrderAccrualReportExport.rpt")]
        FinanceOrderAccrualReportExport,


        /// <summary>
        /// The finance invoice accrual report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Accrual Report", KeyValue = "FinanceInvoiceAccrualReport.rpt")]
        FinanceInvoiceAccrualReport,


        /// <summary>
        /// The finance invoice accrual report export
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Accrual Report export", KeyValue = "FinanceInvoiceAccrualReportExport.rpt")]
        FinanceInvoiceAccrualReportExport,

        /// <summary>
        /// The purchasing cancel order report
        /// </summary>
        [EnumValueData(Name = "Purchasing Cancel Order Report", KeyValue = "PurchasingCancelOrderReport.rpt")]
        PurchasingCancelOrderReport,

        /// <summary>
        /// The marine engine log book details
        /// </summary>
        [EnumValueData(Name = "Marine Engine Log Book Details", KeyValue = "MarineEngineLogBookDetailsReport.rpt")]
        MarineEngineLogBookDetails,

        /// <summary>
        /// The marine running hours list report
        /// </summary>
        /// Replace MarineCounterReadingsReport with MarineRunningHoursListReport
        [EnumValueData(Name = "Marine Running Hours List Report", KeyValue = "MarineRunningHoursListReport.rpt")]
        MarineRunningHoursListReport,

        /// <summary>
        /// The marine vessel certificate audit logs report
        /// </summary>
        [EnumValueData(Name = "Marine Vessel Certificate Audit Logs Report", KeyValue = "MarineVesselCertificateAuditLogsReport.rpt")]
        MarineVesselCertificateAuditLogsReport,

        /// <summary>
        /// The marine sire certificate report
        /// </summary>
        [EnumValueData(Name = "Marine SIRE Certificate Report", KeyValue = "MarineSIRECertificateReport.rpt")]
        MarineSIRECertificateReport,

        /// <summary>
        /// The finance remittance advice report
        /// </summary>
        [EnumValueData(Name = "Finance Remittance Advice Report", KeyValue = "FinanceRemittanceAdviceReport.rpt")]
        FinanceRemittanceAdviceReport,

        /// <summary>
        /// The marine vessel certificate report
        /// </summary>
        [EnumValueData(Name = "Marine Vessel Certificate Report", KeyValue = "MarineVesselCertificateReport.rpt")]
        MarineVesselCertificateReport,

        /// <summary>
        /// The PMS certificate report
        /// </summary>
        [EnumValueData(Name = "PMS Certificate Report", KeyValue = "MarinePMSCertificateReport.rpt")]
        PMSCertificateReport,

        /// <summary>
        /// The crewing CV report of type BGI.
        /// </summary>
        [EnumValueData(Name = "BGI", KeyValue = "CrewingCurriculumVitaeBGIReport.rpt")]
        CrewingCurriculumVitaeBGIReport,

        /// <summary>
        /// The crewing CV report of type ITM.
        /// </summary>
        [EnumValueData(Name = "ITM", KeyValue = "CrewingCurriculumVitaeITMReport.rpt")]
        CrewingCurriculumVitaeITMReport,

        /// <summary>
        /// The crewing CV report of type VShips.
        /// </summary>
        [EnumValueData(Name = "VShips", KeyValue = "CrewingCurriculumVitaeVShipsReport.rpt")]
        CrewingCurriculumVitaeVShipsReport,

        /// <summary>
        /// The marine work and rest variance report
        /// </summary>
        [EnumValueData(Name = "Marine Work And Rest Variance Report", KeyValue = "MarineWKRVarianceReport.rpt")]
        MarineWorkAndRestVarianceReport,

        /// <summary>
        /// The marine work and rest monthly report
        /// </summary>
        [EnumValueData(Name = "Marine WKR Crew Monthly Hours Report", KeyValue = "MarineWKRCrewMonthlyHoursReport.rpt")]
        MarineWorkAndRestMonthlyReport,

        /// <summary>
        /// The marine work and rest daily report
        /// </summary>
        [EnumValueData(Name = "Marine Work And Rest Crew Daily Hours Report", KeyValue = "MarineWKRCrewDailyHoursReport.rpt")]
        MarineWorkAndRestDailyReport,

        /// <summary>
        /// The marine work and rest admin report
        /// </summary>
        [EnumValueData(Name = "Marine Work And Rest Admin Report", KeyValue = "MarineWKRAdminReport.rpt")]
        MarineWorkAndRestAdminReport,

        /// <summary>
        /// The marine work and rest crew monthly blank template report
        /// </summary>
        [EnumValueData(Name = "Marine Work And Rest Crew Monthly Blank Template Report", KeyValue = "MarineWKRCrewMonthlyBlankTemplateReport.rpt")]
        MarineWorkAndRestCrewMonthlyBlankTemplateReport,

        /// <summary>
        /// The purchasing invoice comparison report
        /// </summary>
        [EnumValueData(Name = "Invoice comparison Report", KeyValue = "PurchasingInvoiceComparisonReport.rpt")]
        PurchasingInvoiceComparisonReport,

        /// <summary>
        /// The marine work and rest crew monthly summary report
        /// </summary>
        [EnumValueData(Name = "Marine Work And Rest Crew Monthly Summary Report", KeyValue = "MarineWKRCrewMonthlySummaryReport.rpt")]
        MarineWorkAndRestCrewMonthlySummaryReport,

        /// <summary>
        /// The marine ra vessel listing report
        /// </summary>
        [EnumValueData(Name = "Marine Risk Assessment Vessel Listing Report", KeyValue = "MarineRAVesselListingReport.rpt")]
        MarineRAVesselListingReport,

        /// <summary>
        /// The marine ra vessel listing details report
        /// </summary>
        [EnumValueData(Name = "Marine Risk Assessment Vessel Listing Details Report", KeyValue = "MarineRAVesselListingDetailsReport.rpt")]
        MarineRAVesselListingDetailsReport,

        /// <summary>
        /// The finance invoice audit logs report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Audit Logs Report", KeyValue = "FinanceInvoiceAuditLogsReport.rpt")]
        FinanceInvoiceAuditLogsReport,

        /// <summary>
        /// The marine ra generic listing report
        /// </summary>
        [EnumValueData(Name = "Marine Risk Assessment Generic Listing Report", KeyValue = "MarineRAGenericListingReport.rpt")]
        MarineRAGenericListingReport,

        /// <summary>
        /// The marine ra generic listing detail report
        /// </summary>
        [EnumValueData(Name = "Marine Risk Assessment Generic Listing Detail Report", KeyValue = "MarineRAGenericListingDetailReport.rpt")]
        MarineRAGenericListingDetailReport,

        /// <summary>
        /// The finance invoice memo print report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Memo Print Report", KeyValue = "FinanceInvoiceMemoPrintReport.rpt")]
        FinanceInvoiceMemoPrintReport,

        /// <summary>
        /// The marine inventory update by location report
        /// </summary>
        [EnumValueData(Name = "Marine Inventory Update By Location Report", KeyValue = "MarineInventoryUpdateByLocationReport.rpt")]
        MarineInventoryUpdateByLocationReport,

        /// <summary>
        /// The marine component listing report
        /// </summary>
        [EnumValueData(Name = "Marine Component Listing Report", KeyValue = "MarineComponentListingReport.rpt")]
        MarineComponentListingReport,

        /// <summary>
        /// The marine work basket listing report
        /// </summary>
        [EnumValueData(Name = "Marine Work Basket Listing Report", KeyValue = "MarineWorkBasketListingReport.rpt")]
        MarineWorkBasketListingReport,
        /// <summary>
        /// The marine work basket list detail report
        /// </summary>
        [EnumValueData(Name = "Marine Work Basket List Detail Report", KeyValue = "MarineWorkBasketListDetailReport.rpt")]
        MarineWorkBasketListDetailReport,
        /// <summary>
        /// The marine inventory update by component report
        /// </summary>
        [EnumValueData(Name = "Marine Inventory Update By Component Report", KeyValue = "MarineInventoryUpdateByComponentReport.rpt")]
        MarineInventoryUpdateByComponentReport,

        /// <summary>
        /// The marine maintenance history list report
        /// </summary>
        [EnumValueData(Name = "Marine Maintenance History List Report", KeyValue = "MarineMaintenanceHistoryListReport.rpt")]
        MarineMaintenanceHistoryListReport,

        /// <summary>
        /// The marine vessel waste summary report
        /// </summary>
        [EnumValueData(Name = "Marine Vessel Waste Emission Summary Report", KeyValue = "MarineVesselWasteSummaryReport.rpt")]
        MarineVesselWasteEmissionSummaryReport,

        /// <summary>
        /// The marine em discharge summary excel report
        /// </summary>
        [EnumValueData(Name = "Marine EM Discharge Summary Excel Report", KeyValue = "MarineEMDischargeSummaryExcelReport.rpt")]
        MarineEMDischargeSummaryExcelReport,

        /// <summary>
        /// The marine ozone depleting substances report
        /// </summary>
        [EnumValueData(Name = "Marine Ozone Depleting Substances Report", KeyValue = "MarineOzoneDepletingSubstancesReport.rpt")]
        MarineOzoneDepletingSubstancesReport,

        /// <summary>
        /// The marine component details report
        /// </summary>
        [EnumValueData(Name = "Marine Component Details Report", KeyValue = "MarineComponentDetailsReport.rpt")]
        MarineComponentDetailsReport,

        /// <summary>
        /// The marine CO2 emission report
        /// </summary>
        [EnumValueData(Name = "Marine Vessel Waste Emission Listing Report", KeyValue = "MarineCO2EmissionReport.rpt")]
        MarineVesselWasteEmissionListingReport,

        /// <summary>
        /// The marine garbage waste details report
        /// </summary>
        [EnumValueData(Name = "Marine Garbage Waste Details Report", KeyValue = "MarineGarbageWasteDetails.rpt")]
        MarineGarbageWasteDetailsReport,

        /// <summary>
        /// The marine oil record book for cargo waste report
        /// </summary>
        [EnumValueData(Name = "Cargo Discharge Report", KeyValue = "MarineOilRecordBookForCargoWasteReport.rpt")]
        MarineOilRecordBookForCargoWasteReport,

        /// <summary>
        /// The marine oil record book for sludge waste report
        /// </summary>
        [EnumValueData(Name = "Sludge Waste Discharge Report", KeyValue = "MarineOilRecordBookForSludgeWasteReport.rpt")]
        MarineOilRecordBookForSludgeWasteReport,

        /// <summary>
        /// The marine oil record book for oil water report
        /// </summary>
        [EnumValueData(Name = "Oily Water Waste Discharge Report", KeyValue = "MarineOilRecordBookForOilWaterReport.rpt")]
        MarineOilRecordBookForOilWaterReport,

        /// <summary>
        /// The Oil Major Crew Experience Matrix Report
        /// </summary>
        [EnumValueData(Name = "Oil Major Crew Experience Matrix Report", KeyValue = "CrewingExperienceCertificateMatrixReport.rpt")]
        CrewOilMajorExperienceMatrixReport,

        /// <summary>
        /// The marine work basket detail report
        /// </summary>
        [EnumValueData(Name = "Marine Work Basket Detail Report", KeyValue = "MarineWorkBasketDetailReport.rpt")]
        MarineWorkBasketDetailReport,

        /// <summary>
        /// The marine job safety analysis report
        /// </summary>
        [EnumValueData(Name = "Marine Job Safety Analysis Report", KeyValue = "MarineJobSafetyAnalysisReport.rpt")]
        MarineJobSafetyAnalysisReport,

        /// <summary>
        /// The marine spare listing report
        /// </summary>
        [EnumValueData(Name = "Marine Spare Listing Report", KeyValue = "MarineSpareListingReport.rpt")]
        MarineSpareListingReport,

        /// <summary>
        /// The marine defect manager listing report
        /// </summary>
        [EnumValueData(Name = "Marine Defect Manager Listing Report", KeyValue = "MarineDefectManagerListingReport.rpt")]
        MarineDefectManagerListingReport,

        /// <summary>
        /// The marine HazOcc Vessel Incident report
        /// </summary>
        [EnumValueData(Name = "Marine HazOcc Vessel Incident Report", KeyValue = "MarineVesIncidentReport.rpt")]
        MarineHazOccVesselIncReport,

        /// <summary>
        /// The marine HazOcc Vessel investigation report
        /// </summary>
        [EnumValueData(Name = "Marine HazOcc Investigation Report", KeyValue = "MarineIncidentInvestigationReport.rpt")]
        MarineHazOccInvReport,

        /// <summary>
        /// The marine HazOcc Witnessn report
        /// </summary>
        [EnumValueData(Name = "Marine HazOcc Witness Report", KeyValue = "MarineIncidentBlkWitnessReport.rpt")]
        MarineHazOccWtnReport,

        /// <summary>
        /// The marine maintenance history details report
        /// </summary>
        [EnumValueData(Name = "Marine Maintenance History Details Report", KeyValue = "MarineMaintenanceHistoryDetailsReport.rpt")]
        MarineMaintenanceHistoryDetailsReport,

        /// <summary>
        /// The marine crew ilo report
        /// </summary>
        [EnumValueData(Name = "Marine Crew ILO Report", KeyValue = "MarineCrewILOReport.rpt")]
        MarineCrewILOReport,

        /// <summary>
        /// The marine critical spare listing tech 18b report
        /// </summary>
        [EnumValueData(Name = "Marine Critical Spare Listing Tech 18B Report", KeyValue = "MarineCriticalSpareListingTech18BReport.rpt")]
        MarineCriticalSpareListingTech18BReport,

        /// <summary>
        /// The marine critical component listing tech18 a report
        /// </summary>
        [EnumValueData(Name = "Marine Critical Component Listing Tech 18A Report", KeyValue = "MarineCriticalComponentListingTech18AReport.rpt")]
        MarineCriticalComponentListingTech18AReport,

        /// <summary>
        /// The marine component class listing report
        /// </summary>
        [EnumValueData(Name = "Marine Component Class Listing Report", KeyValue = "MarineComponentClassListingReport.rpt")]
        MarineComponentClassListingReport,

        /// <summary>
        /// The marine voyage sea passage report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Sea Passage Report", KeyValue = "MarineVoyageSeaPassageReport.rpt")]
        MarineVoyageSeaPassageReport,

        /// <summary>
        /// The marine voyage port call report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Port Call Report", KeyValue = "MarineVoyagePortCallReport.rpt")]
        MarineVoyagePortCallReport,

        /// <summary>
        /// The marine voyage rob report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage ROB Report", KeyValue = "MarineVoyageROBReport.rpt")]
        MarineVoyageROBReport,

        /// <summary>
        /// The marine voyage noon report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Noon Report", KeyValue = "MarineVoyageNoonReport.rpt")]
        MarineVoyageNoonReport,

        /// <summary>
        /// The marine defect manager details report
        /// </summary>
        [EnumValueData(Name = "Marine Defect Manager Details Report", KeyValue = "MarineDefectManagerDetailsReport.rpt")]
        MarineDefectManagerDetailsReport,

        /// <summary>
        /// The marine voyage activity report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Activity Report", KeyValue = "MarineVoyageActivityReport.rpt")]
        MarineVoyageActivityReport,

        /// <summary>
        /// The marine voyage cargo tab details report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Cargo Tab Details Report", KeyValue = "MarineVoyageCargoTabDetailsReport.rpt")]
        MarineVoyageCargoTabDetailsReport,

        /// <summary>
        /// The marine voyage cargo performance report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Cargo Performance Report", KeyValue = "MarineVoyageCargoPerformanceReport.rpt")]
        MarineVoyageCargoPerformanceReport,

        /// <summary>
        /// The marine spare forecast report
        /// </summary>
        [EnumValueData(Name = "Marine Spare Forecast Report", KeyValue = "MarineSpareForecastReport.rpt")]
        MarineSpareForecastReport,

        /// <summary>
        /// The marine voyage delays and deviations report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Delays And Deviations Report", KeyValue = "MarineVoyageDelaysAndDeviationsReport.rpt")]
        MarineVoyageDelaysAndDeviationsReport,

        /// <summary>
        /// The marine inspection ship security assessment report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Ship Security Assessment Report", KeyValue = "MarineInspectionShipSecurityAssessmentReport.rpt")]
        MarineInspectionShipSecurityAssessmentReport,

        /// <summary>
        /// The marine inspection ship security assessment report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Ship Security Assessment Excel Report", KeyValue = "MarineInspectionShipSecurityAssessmentExcelReport.rpt")]
        MarineInspectionShipSecurityAssessmentExcelReport,

        /// <summary>
        /// The marine inspection tmsa report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection TMSA Report", KeyValue = "MarineInspectionTMSAReport.rpt")]
        MarineInspectionTMSAReport,

        /// <summary>
        /// The marine inspection manager overview vessel excel report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Manager Overview Vessel Excel Report", KeyValue = "MarineInspectionManagerOverviewVesselExcelReport.rpt")]
        MarineInspectionManagerOverviewVesselExcelReport,

        /// <summary>
        /// The marine inspection manager overview excel report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Manager Overview Excel Report", KeyValue = "MarineInspectionManagerOverviewExcelReport.rpt")]
        MarineInspectionManagerOverviewExcelReport,

        /// <summary>
        /// The marine inspection summary report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Summary Report", KeyValue = "MarineInspectionSummaryReport.rpt")]
        MarineInspectionSummaryReport,

        /// <summary>
        /// The marine inventory value fifo report
        /// </summary>
        [EnumValueData(Name = "Marine Inventory Value FIFO Report", KeyValue = "MarineInventoryValueFIFOReport.rpt")]
        MarineInventoryValueFIFOReport,

        /// <summary>
        /// The marine inventory value fifo Excel report
        /// </summary>
        [EnumValueData(Name = "Marine Inventory Value FIFO Report", KeyValue = "MarineInventoryValueFIFOExcelReport.rpt")]
        MarineInventoryValueFIFOExcelReport,

        /// <summary>
        /// The marine environment manager inadequacy report
        /// </summary>
        [EnumValueData(Name = "Marine Environment Manager Inadequacy Report", KeyValue = "MarineEnvironmentManagerInadequacyReport.rpt")]
        MarineEnvironmentManagerInadequacyReport,

        /// <summary>
        /// The marine running hours detail report
        /// </summary>
        [EnumValueData(Name = "Marine Running Hours Detail Report", KeyValue = "MarineRunningHoursDetailReport.rpt")]
        MarineRunningHoursDetailReport,

        /// <summary>
        /// The marine work cost report
        /// </summary>
        [EnumValueData(Name = "Marine Work Cost Report", KeyValue = "MarineWorkCostReport.rpt")]
        MarineWorkCostReport,

        /// <summary>
        /// The marine work cost summary report
        /// </summary>
        [EnumValueData(Name = "Marine Work Cost Summary Report", KeyValue = "MarineWorkCostSummaryReport.rpt")]
        MarineWorkCostSummaryReport,

        /// <summary>
        /// The marine voyage charter details report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Charter Details Report", KeyValue = "MarineVoyageCharterDetailsReport.rpt")]
        MarineVoyageCharterDetailsReport,

        /// <summary>
        /// The marine voyage charter details excel report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Charter Details Excel Report", KeyValue = "MarineVoyageCharterDetailsExcelReport.rpt")]
        MarineVoyageCharterDetailsExcelReport,

        /// <summary>
        /// The marine environmental management SEEMP report
        /// </summary>
        [EnumValueData(Name = "Marine Environmental Management SEEMP Report", KeyValue = "MarineEMSEEMPReport.rpt")]
        MarineEMSEEMPReport,

        /// <summary>
        /// The marine job safety analysis overview report
        /// </summary>
        [EnumValueData(Name = "Marine Job Safety Analysis Overview Report", KeyValue = "MarineJobSafetyAnalysisOverviewReport.rpt")]
        MarineJobSafetyAnalysisOverviewReport,

        /// <summary>
        /// The marine environmental management advance notification report
        /// </summary>
        [EnumValueData(Name = "Marine Environmental Management Advanced Notification Report", KeyValue = "MarineEMAdvanceNotificationReport.rpt")]
        MarineEMAdvanceNotificationReport,

        /// <summary>
        /// The marine environment log report
        /// </summary>
        [EnumValueData(Name = "Marine Environmental Log Report", KeyValue = "MarineEnvironmentLogReport.rpt")]
        MarineEnvironmentLogReport,

        /// <summary>
        /// The marine insurance claims details report
        /// </summary>
        [EnumValueData(Name = "Marine Insurance Claims Details Report", KeyValue = "MarineInsuranceClaimsDetailsReport.rpt")]
        MarineInsuranceClaimsDetailsReport,

        /// <summary>
        /// The marine insurance claims listing report
        /// </summary>
        [EnumValueData(Name = "Marine Insurance Claims Listing Report", KeyValue = "MarineInsuranceClaimsListingReport.rpt")]
        MarineInsuranceClaimsListingReport,

        /// <summary>
        /// The marine environmental management EUMRV emission report
        /// </summary>
        [EnumValueData(Name = "Marine Environmental Management EUMRV Emission Report", KeyValue = "MarineEMEUMRVEmissionReport.rpt")]
        MarineEMEUMRVEmissionReport,

        /// <summary>
        /// The finance balance sheet report
        /// </summary>
        [EnumValueData(Name = "Finance Balance Sheet Report", KeyValue = "FinanceBalanceSheetReport.rpt")]
        FinanceBalanceSheetReport,

        /// <summary>
        /// The finance profit and loss report
        /// </summary>
        [EnumValueData(Name = "Finance Profit And Loss Report", KeyValue = "FinanceProfitAndLossReport.rpt")]
        FinanceProfitAndLossReport,

        /// <summary>
        /// The marine inspection ship security assessment causes report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Ship Security Assessment Causes Report", KeyValue = "MarineInspectionShipSecurityAssessmentCausesReport.rpt")]
        MarineInspectionShipSecurityAssessmentCausesReport,

        /// <summary>
        /// The marine dry dock report
        /// </summary>
        [EnumValueData(Name = "Marine Dry Dock Report", KeyValue = "MarineDryDockReport.rpt")]
        MarineDryDockReport,

        /// <summary>
        /// The marine dry dock excel report
        /// </summary>
        [EnumValueData(Name = "Marine Dry Dock Excel Report", KeyValue = "MarineDryDockExcelReport.rpt")]
        MarineDryDockExcelReport,

        /// <summary>
        /// The marine crew onboard summary report
        /// </summary>
        [EnumValueData(Name = "Marine Crew Onboard Summary Report", KeyValue = "MarineCrewOnboardSummaryReport.rpt")]
        MarineCrewOnboardSummaryReport,

        /// <summary>
        /// The finance client account ledger report
        /// </summary>
        [EnumValueData(Name = "Finance Client Account Ledger Report", KeyValue = "FinanceClientAccountLedgerReport.rpt")]
        FinanceClientAccountLedgerReport,


        /// <summary>
        /// The finance outstanding invoices by vessel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Vessel Report", KeyValue = "FinanceOutstandingInvoicesByVesselReport.rpt")]
        FinanceOutstandingInvoicesByVesselReport,

        /// <summary>
        /// The finance outstanding invoices by vessel curr report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Vessel Curr Report", KeyValue = "FinanceOutstandingInvoicesByVesselCurrReport.rpt")]
        FinanceOutstandingInvoicesByVesselCurrReport,

        /// <summary>
        /// The finance outstanding invoices by supplier report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Supplier Report", KeyValue = "FinanceOutstandingInvoicesBySupplierReport.rpt")]
        FinanceOutstandingInvoicesBySupplierReport,

        /// <summary>
        /// The finance outstanding invoices by supplier curr report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Supplier Curr Report", KeyValue = "FinanceOutstandingInvoicesBySupplierCurrReport.rpt")]
        FinanceOutstandingInvoicesBySupplierCurrReport,

        /// <summary>
        /// The finance outstanding invoices by vessel45 summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Vessel 45 Summary Excel Report", KeyValue = "FinanceOutstandingInvoicesByVessel45SummaryExcelReport.rpt")]
        FinanceOutstandingInvoicesByVessel45SummaryExcelReport,

        /// <summary>
        /// The finance outstanding invoices by supplier45 excel summary report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Supplier 45 Excel Summary Report", KeyValue = "FinanceOutstandingInvoicesBySupplier45ExcelSummaryReport.rpt")]
        FinanceOutstandingInvoicesBySupplier45ExcelSummaryReport,

        /// <summary>
        /// The finance outstanding invoices by vessel15 summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Vessel 15 Summary Excel Report", KeyValue = "FinanceOutstandingInvoicesByVessel15SummaryExcelReport.rpt")]
        FinanceOutstandingInvoicesByVessel15SummaryExcelReport,

        /// <summary>
        /// The finance outstanding invoices by supplier15 excel summary report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Supplier 15 Excel Summary Report", KeyValue = "FinanceOutstandingInvoicesBySupplier15ExcelSummaryReport.rpt")]
        FinanceOutstandingInvoicesBySupplier15ExcelSummaryReport,

        /// <summary>
        /// The finance outstanding invoices by vessel45 excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Vessel 45 Excel Report", KeyValue = "FinanceOutstandingInvoicesByVessel45ExcelReport.rpt")]
        FinanceOutstandingInvoicesByVessel45ExcelReport,

        /// <summary>
        /// The finance outstanding invoices by supplier45 excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Supplier45 Excel Report", KeyValue = "FinanceOutstandingInvoicesBySupplier45ExcelReport.rpt")]
        FinanceOutstandingInvoicesBySupplier45ExcelReport,

        /// <summary>
        /// The finance outstanding invoices by vessel15 excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Vessel 15 Excel Report", KeyValue = "FinanceOutstandingInvoicesByVessel15ExcelReport.rpt")]
        FinanceOutstandingInvoicesByVessel15ExcelReport,

        /// <summary>
        /// The finance outstanding invoices by supplier15 excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoices By Supplier 15 Excel Report", KeyValue = "FinanceOutstandingInvoicesBySupplier15ExcelReport.rpt")]
        FinanceOutstandingInvoicesBySupplier15ExcelReport,

        /// <summary>
        /// The Crewing Appraisal Report
        /// </summary>
        [EnumValueData(Name = "Crewing Appraisal Report", KeyValue = "CrewingAppraisalReport.rpt")]
        CrewingAppraisalReport,

        /// <summary>
        /// The finance open transactions report
        /// </summary>
        [EnumValueData(Name = "Finance Open Transactions Report", KeyValue = "FinanceOpenTransactionsReport.rpt")]
        FinanceOpenTransactionsReport,

        /// <summary>
        /// The finance open transactions excel report
        /// </summary>
        [EnumValueData(Name = "Finance Open Transactions Excel Report", KeyValue = "FinanceOpenTransactionsExcelReport.rpt")]
        FinanceOpenTransactionsExcelReport,

        /// <summary>
        /// The finance auxiliary cost analysis report
        /// </summary>
        [EnumValueData(Name = "Finance Auxiliary Cost Analysis Report", KeyValue = "FinanceAuxiliaryCostAnalysisReport.rpt")]
        FinanceAuxiliaryCostAnalysisReport,

        /// <summary>
        /// The finance auxiliary cost balance report
        /// </summary>
        [EnumValueData(Name = "Finance Auxiliary Cost Balance Report", KeyValue = "FinanceAuxiliaryCostBalanceReport.rpt")]
        FinanceAuxiliaryCostBalanceReport,

        /// <summary>
        /// The finance auxiliary cost analysis canada report
        /// </summary>
        [EnumValueData(Name = "Finance Auxiliary Cost Analysis Canada Report", KeyValue = "FinanceAuxiliaryCostAnalysisCanadaReport.rpt")]
        FinanceAuxiliaryCostAnalysisCanadaReport,

        /// <summary>
        /// The finance commitment acc sequence report
        /// </summary>
        [EnumValueData(Name = "Finance Commitment Account Sequence Report", KeyValue = "FinanceCommitmentAccSequenceReport.rpt")]
        FinanceCommitmentAccSequenceReport,

        /// <summary>
        /// The finance commitment listing report
        /// </summary>
        [EnumValueData(Name = "Finance Commitment Listing Report", KeyValue = "FinanceCommitmentListingReport.rpt")]
        FinanceCommitmentListingReport,

        /// <summary>
        /// The finance voyage results across fleet report
        /// </summary>
        [EnumValueData(Name = "Finance Voyage Results Across Fleet Report", KeyValue = "FinanceVoyageResultsAcrossFleetReport.rpt")]
        FinanceVoyageResultsAcrossFleetReport,

        /// <summary>
        /// The finance voyage results across fleet summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Voyage Results Across Fleet Summary Excel Report", KeyValue = "FinanceVoyageResultsAcrossFleetSummaryExcelReport.rpt")]
        FinanceVoyageResultsAcrossFleetSummaryExcelReport,

        /// <summary>
        /// The finance voyage results across fleet detail excel report
        /// </summary>
        [EnumValueData(Name = "Finance Voyage Results Across Fleet Detail Excel Report", KeyValue = "FinanceVoyageResultsAcrossFleetDetailExcelReport.rpt")]
        FinanceVoyageResultsAcrossFleetDetailExcelReport,

        /// <summary>
        /// The Crewing Compliance Report
        /// </summary>
        [EnumValueData(Name = "Crewing Compliance Report", KeyValue = "CrewingComplianceReport.rpt")]
        CrewingComplianceReport,

        /// <summary>
        /// The finance balance sheet fleet report
        /// </summary>
        [EnumValueData(Name = "Finance Balance Sheet Fleet Report", KeyValue = "FinanceBalanceSheetFleetReport.rpt")]
        FinanceBalanceSheetFleetReport,

        /// <summary>
        /// The finance profit and loss for fleet report
        /// </summary>
        [EnumValueData(Name = "Finance Profit And Loss For Fleet Report", KeyValue = "FinanceProfitAndLossForFleetReport.rpt")]
        FinanceProfitAndLossForFleetReport,

        /// <summary>
        /// The finance trial balance client report
        /// </summary>
        [EnumValueData(Name = "Finance Trial Balance Client Report", KeyValue = "FinanceTrialBalanceClientReport.rpt")]
        FinanceTrialBalanceClientReport,

        /// <summary>
        /// The finance trial balance report
        /// </summary>
        [EnumValueData(Name = "Finance Trial Balance Report", KeyValue = "FinanceTrialBalanceReport.rpt")]
        FinanceTrialBalanceReport,

        /// <summary>
        /// The finance trial balance excel report
        /// </summary>
        [EnumValueData(Name = "Finance Trial Balance Excel Report", KeyValue = "FinanceTrialBalanceExcelReport.rpt")]
        FinanceTrialBalanceExcelReport,

        /// <summary>
        /// The finance trial balance for fleet excel report
        /// </summary>
        [EnumValueData(Name = "Finance Trial Balance For Fleet Excel Report", KeyValue = "FinanceTrialBalanceForFleetExcelReport.rpt")]
        FinanceTrialBalanceForFleetExcelReport,

        /// <summary>
        /// The finance sales invoice memo report
        /// </summary>
        [EnumValueData(Name = "Finance Sales Invoice Memo Report", KeyValue = "FinanceSalesInvoiceMemoReport.rpt")]
        FinanceSalesInvoiceMemoReport,

        /// <summary>
        /// The finance sales invoice audit logs report
        /// </summary>
        [EnumValueData(Name = "Finance Sales Invoice Audit Logs Report", KeyValue = "FinanceSalesInvoiceAuditLogsReport.rpt")]
        FinanceSalesInvoiceAuditLogsReport,


        /// <summary>
        /// The Crewing Vessel Details Report
        /// </summary>
        [EnumValueData(Name = "Crewing Vessel Details Report", KeyValue = "CrewingVesselDetailsReport.rpt")]
        CrewVesselDetailsReport,

        /// <summary>
        /// The Crewing Crew Report
        /// </summary>
        [EnumValueData(Name = "Crewing Crew Report", KeyValue = "CrewingCrewListReport.rpt")]
        CrewingCrewReport,

        /// <summary>
        /// The finance creditor analysis report
        /// </summary>
        [EnumValueData(Name = "Finance Creditor Analysis Report", KeyValue = "FinanceCreditorAnalysisReport.rpt")]
        FinanceCreditorAnalysisReport,

        /// <summary>
        /// The finance creditor analysis excel report
        /// </summary>
        [EnumValueData(Name = "Finance Creditor Analysis Excel Report", KeyValue = "FinanceCreditorAnalysisExcelReport.rpt")]
        FinanceCreditorAnalysisExcelReport,

        /// <summary>
        /// The finance trial balance for fleet report
        /// </summary>
        [EnumValueData(Name = "Finance Trial Balance For Fleet Report", KeyValue = "FinanceTrialBalanceForFleetReport.rpt")]
        FinanceTrialBalanceForFleetReport,

        /// <summary>
        /// The Crewing Generic Appraisal Report
        /// </summary>
        [EnumValueData(Name = "Crewing Generic Appraisal Report", KeyValue = "CrewingGenericAppraisalReport.rpt")]
        CrewingGenericAppraisalReport,

        /// <summary>
        /// The Crewing Reliever Report
        /// </summary>
        [EnumValueData(Name = "Crewing Reliever Report", KeyValue = "CrewingRelieverReport.rpt")]
        CrewingRelieverReport,

        /// <summary>
        /// The marine voyage faop report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage FAOP Report", KeyValue = "MarineVoyageFAOPReport.rpt")]
        MarineVoyageFAOPReport,

        /// <summary>
        /// The marine ships work order report
        /// </summary>
        [EnumValueData(Name = "Marine Ships Work Order Report", KeyValue = "MarineShipsWorkOrderReport.rpt")]
        MarineShipsWorkOrderReport,

        /// <summary>
        /// The marine voyage bunker performance report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Bunker Performance Report", KeyValue = "MarineVoyageBunkerPerformanceReport.rpt")]
        MarineVoyageBunkerPerformanceReport,

        /// <summary>
        /// The marine onboard earnings report
        /// </summary>
        [EnumValueData(Name = "Marine Onboard Earnings Report", KeyValue = "MarineOnboardEarningsReport.rpt")]
        MarineOnboardEarningsReport,

        /// <summary>
        /// The marine onboard deductions
        /// </summary>
        [EnumValueData(Name = "Marine Onboard Deductions", KeyValue = "MarineOnboardDeductions.rpt")]
        MarineOnboardDeductions,

        /// <summary>
        /// The finance expenditure statement details report
        /// </summary>
        [EnumValueData(Name = "Finance Expenditure Statement Details Report", KeyValue = "FinanceExpenditureStatementDetailsReport.rpt")]
        FinanceExpenditureStatementDetailsReport,

        /// <summary>
        /// The finance expenditure statement summary report
        /// </summary>
        [EnumValueData(Name = "Finance Expenditure Statement Summary Report", KeyValue = "FinanceExpenditureStatementSummaryReport.rpt")]
        FinanceExpenditureStatementSummaryReport,

        /// <summary>
        /// The finance voyage results across coy report
        /// </summary>
        [EnumValueData(Name = "Finance Voyage Results Across COY Report", KeyValue = "FinanceVoyageResultsAcrossCOYReport.rpt")]
        FinanceVoyageResultsAcrossCOYReport,

        /// <summary>
        /// The finance clipper data extract report
        /// </summary>
        [EnumValueData(Name = "Finance Clipper Data Extract Report", KeyValue = "FinanceClipperDataExtractReport.rpt")]
        FinanceClipperDataExtractReport,

        /// <summary>
        /// The finance voyage results across coy summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Voyage Results Across COY Summary Excel Report", KeyValue = "FinanceVoyageResultsAcrossCOYSummaryExcelReport.rpt")]
        FinanceVoyageResultsAcrossCOYSummaryExcelReport,

        /// <summary>
        /// The finance voyage results across coy details excel report
        /// </summary>
        [EnumValueData(Name = "Finance Voyage Results Across COY Details Excel Report", KeyValue = "FinanceVoyageResultsAcrossCOYDetailsExcelReport.rpt")]
        FinanceVoyageResultsAcrossCOYDetailsExcelReport,

        /// <summary>
        /// The marine voyage noon multi vessel report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Noon Multi Vessel Report", KeyValue = "MarineVoyageNoonMultiVesselReport.rpt")]
        MarineVoyageNoonMultiVesselReport,

        /// <summary>
        /// The marine haz occ passenger accident report
        /// </summary>
        [EnumValueData(Name = "Marine HazOcc Passenger Accident Report", KeyValue = "MarineHazOccPassengerAccidentReport.rpt")]
        MarineHazOccPassengerAccidentReport,

        /// <summary>
        /// The finance group invoice paid report
        /// </summary>
        [EnumValueData(Name = "Finance Group Invoice Paid Report", KeyValue = "FinanceGroupInvoicePaidReport.rpt")]
        FinanceGroupInvoicePaidReport,

        /// <summary>
        /// The marine agent list report
        /// </summary>
        [EnumValueData(Name = "Marine Agent List Report", KeyValue = "MarineAgentListReport.rpt")]
        MarineAgentListReport,

        /// <summary>
        /// The marine weekly minutes for period report
        /// </summary>
        [EnumValueData(Name = "Marine Weekly Minutes For Period Report", KeyValue = "MarineWeeklyMinutesForPeriodReport.rpt")]
        MarineWeeklyMinutesForPeriodReport,

        /// <summary>
        /// The marine ves perf analysis main engine shaft report
        /// </summary>
        [EnumValueData(Name = "Marine Vessel Performace Analysis Main Engine Shaft Report", KeyValue = "MarineVesPerfAnalysisMainEngineShaftReport.rpt")]
        MarineVesPerfAnalysisMainEngineShaftReport,

        /// <summary>
        /// The finance account ledger BC1 fleet report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Fleet Report", KeyValue = "FinanceAccountLedgerBC1FleetReport.rpt")]
        FinanceAccountLedgerBC1FleetReport,

        /// <summary>
        /// The finance account ledger BC1 vessel details report
        /// </summary>
        [EnumValueData(Name = "FinanceAccountLedgerBC1COYDetailsReport", KeyValue = "FinanceAccountLedgerBC1COYDetailsReport.rpt")]
        FinanceAccountLedgerBC1COYDetailsReport,

        /// <summary>
        /// The finance account ledger BC1 vessel summary report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 COY Summary Report", KeyValue = "FinanceAccountLedgerBC1COYSummaryReport.rpt")]
        FinanceAccountLedgerBC1COYSummaryReport,

        /// <summary>
        /// The finance account ledger summary report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger Summary Report", KeyValue = "FinanceAccountLedgerSummaryReport.rpt")]
        FinanceAccountLedgerSummaryReport,

        /// <summary>
        /// The marine inspection summary list of outstanding deficiencies
        /// </summary>
        [EnumValueData(Name = "Marine Inspection Summary List Of Outstanding Deficiencies", KeyValue = "MarineInspectionSummaryListOfOutstandingDeficiencies.rpt")]
        MarineInspectionSummaryListOfOutstandingDeficiencies,

        /// <summary>
        /// The marine voyage port call for fleet report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Port Call For Fleet Report", KeyValue = "MarineVoyagePortCallForFleetReport.rpt")]
        MarineVoyagePortCallForFleetReport,

        /// <summary>
        /// The marine ECA report
        /// </summary>
        [EnumValueData(Name = "Marine ECA Report", KeyValue = "MarineECAReport.rpt")]
        MarineECAReport,

        /// <summary>
        /// The marine hotel defect manager details report
        /// </summary>
        [EnumValueData(Name = "Marine Hotel Defect Manager Details Report", KeyValue = "MarineHotelDefectManagerDetailsReport.rpt")]
        MarineHotelDefectManagerDetailsReport,

        /// <summary>
        /// The marine spares barcode report
        /// </summary>
        [EnumValueData(Name = "Marine Spares Barcode Report", KeyValue = "MarineSparesBarcodeReport.rpt")]
        MarineSparesBarcodeReport,

        /// <summary>
        /// The marine port alert report
        /// </summary>
        [EnumValueData(Name = "Marine Port Alert Report", KeyValue = "MarinePortAlertReport.rpt")]
        MarinePortAlertReport,

        /// <summary>
        /// The marine spare parts label report
        /// </summary>
        [EnumValueData(Name = "Marine Spare Parts Label Report", KeyValue = "MarineSparePartsLabelReport.rpt")]
        MarineSparePartsLabelReport,

        /// <summary>
        /// The finance chembulk extract report
        /// </summary>
        [EnumValueData(Name = "Finance Chembulk Extract Report", KeyValue = "FinanceChemBulkExtractReport.rpt")]
        FinanceChemBulkExtractReport,

        /// <summary>
        /// The finance CSL europe extract report
        /// </summary>
        [EnumValueData(Name = "FinanceCSLEuropeExtractReport", KeyValue = "FinanceCSLEuropeExtractReport.rpt")]
        FinanceCSLEuropeExtractReport,

        /// <summary>
        /// The Crewing Crew LineUp Report
        /// </summary>
        [EnumValueData(Name = "Crewing Crew LineUp Report", KeyValue = "CrewingCrewLineUpReport.rpt")]
        CrewingCrewLineUpReport,

        /// <summary>
        /// The finance funding position summary report
        /// </summary>
        [EnumValueData(Name = "Finance Funding Position Summary Report", KeyValue = "FinanceFundingPositionSummaryReport.rpt")]
        FinanceFundingPositionSummaryReport,

        /// <summary>
        /// The finance fleet funding position summary report
        /// </summary>
        [EnumValueData(Name = "Finance Fleet Funding Position Summary Report", KeyValue = "FinanceFleetFundingPositionSummaryReport.rpt")]
        FinanceFleetFundingPositionSummaryReport,

        /// <summary>
        /// The finance invoice reconciliation report
        /// </summary>
        [EnumValueData(Name = "Finance Invoice Reconciliation Report", KeyValue = "FinanceInvoiceReconciliationReport.rpt")]
        FinanceInvoiceReconciliationReport,

        /// <summary>
        /// The finance funding position summary bsa report
        /// </summary>
        [EnumValueData(Name = "Finance Funding Position Summary BSA Report", KeyValue = "FinanceFundingPositionSummaryBSAReport.rpt")]
        FinanceFundingPositionSummaryBSAReport,

        /// <summary>
        /// The finance CMM data extract report
        /// </summary>
        [EnumValueData(Name = "Finance CMM Data Extract Report", KeyValue = "FinanceCMMDataExtractReport.rpt")]
        FinanceCMMDataExtractReport,

        /// <summary>
        /// The finance CSL data extract australia boston report
        /// </summary>
        [EnumValueData(Name = "Finance CSL Data Extract Australia Boston Report", KeyValue = "FinanceCSLDataExtractAustraliaBostonReport.rpt")]
        FinanceCSLDataExtractAustraliaBostonReport,

        /// <summary>
        /// The finance outstanding invoice by supplier office all comp report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoice By Supplier Office All Company Report", KeyValue = "FinanceOutstandingInvoiceBySupplierOfficeAllCompReport.rpt")]
        FinanceOutstandingInvoiceBySupplierOfficeAllCompReport,

        /// <summary>
        /// The finance outstanding invoice by vessel office all comp report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Invoice By Vessel Office All Company Report", KeyValue = "FinanceOutstandingInvoiceByVesselOfficeAllCompReport.rpt")]
        FinanceOutstandingInvoiceByVesselOfficeAllCompReport,

        /// <summary>
        /// The marine weekly minutes managers operational report
        /// </summary>
        [EnumValueData(Name = "Marine Weekly Minutes Managers Operational Report", KeyValue = "MarineWeeklyMinutesManagersOperationalReport.rpt")]
        MarineWeeklyMinutesManagersOperationalReport,

        /// <summary>
        /// The finance running cost analysis for fleet report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Analysis For Fleet Report", KeyValue = "FinanceRunningCostAnalysisForFleetReport.rpt")]
        FinanceRunningCostAnalysisForFleetReport,

        /// <summary>
        /// The finance general ledger analysis net totals report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Analysis Net Totals Report", KeyValue = "FinanceGeneralLedgerAnalysisNetTotalsReport.rpt")]
        FinanceGeneralLedgerAnalysisNetTotalsReport,

        /// <summary>
        /// The finance general ledger analysis net totals excel report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Analysis Net Totals Excel Report", KeyValue = "FinanceGeneralLedgerAnalysisNetTotalsExcelReport.rpt")]
        FinanceGeneralLedgerAnalysisNetTotalsExcelReport,


        /// <summary>
        /// The marine on board crew objective details report
        /// </summary>
        [EnumValueData(Name = "Marine OnBoard Crew Objective Details Report", KeyValue = "MarineOnBoardCrewObjectiveDetailsReport.rpt")]
        MarineOnBoardCrewObjectiveDetailsReport,

        /// <summary>
        /// The crewing crew objectives report
        /// </summary>
        [EnumValueData(Name = "Crew Objective Details Report", KeyValue = "CrewingCrewObjectivesReport.rpt")]
        CrewingCrewObjectivesReport,

        /// <summary>
        /// The marine on board crew joining briefing report
        /// </summary>
        [EnumValueData(Name = "Marine OnBoard Crew Joining Briefing Report", KeyValue = "MarineOnBoardCrewJoiningBriefingReport.rpt")]
        MarineOnBoardCrewJoiningBriefingReport,

        /// <summary>
        /// The marine crew on board appraisal details report
        /// </summary>
        [EnumValueData(Name = "Marine Crew OnBoard Appraisal Details Report", KeyValue = "MarineCrewOnBoardAppraisalDetailsReport.rpt")]
        MarineCrewOnBoardAppraisalDetailsReport,

        /// <summary>
        /// The crewing crew list enhanced relievers report
        /// </summary>
        [EnumValueData(Name = "Crewing Crew List Enhanced Relievers Report", KeyValue = "CrewingCrewListEnhancedRelieversReport.rpt")]
        CrewingCrewListEnhancedRelieversReport,

        /// <summary>
        /// The crewing crew list enhanced on board report
        /// </summary>
        [EnumValueData(Name = "Crewing Crew List Enhanced OnBoard Report", KeyValue = "CrewingCrewListEnhancedOnBoardReport.rpt")]
        CrewingCrewListEnhancedOnBoardReport,

        /// <summary>
        /// The finance account ledger BC1 currency totals report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Currency Totals Report", KeyValue = "FinanceAccountLedgerBC1CurrencyTotalsReport.rpt")]
        FinanceAccountLedgerBC1CurrencyTotalsReport,

        /// <summary>
        /// The crewing crew list enhanced on board relievers report
        /// </summary>
        [EnumValueData(Name = "Crewing Crew List Onboard and Relievers Report", KeyValue = "CrewingCrewListEnhancedOnBoardRelieversReport.rpt")]
        CrewingCrewListEnhancedOnBoardRelieversReport,

        /// <summary>
        /// The finance auxiliary cost analysis canada excel report
        /// </summary>
        [EnumValueData(Name = "Finance Auxiliary Cost Analysis Canada Excel Report", KeyValue = "FinanceAuxiliaryCostAnalysisCanadaExcelReport.rpt")]
        FinanceAuxiliaryCostAnalysisCanadaExcelReport,

        /// <summary>
        /// The finance auxiliary cost analysis excel report
        /// </summary>
        [EnumValueData(Name = "Finance Auxiliary Cost Analysis Excel Report", KeyValue = "FinanceAuxiliaryCostAnalysisExcelReport.rpt")]
        FinanceAuxiliaryCostAnalysisExcelReport,

        /// <summary>
        /// The finance auxiliary cost balance excel report
        /// </summary>
        [EnumValueData(Name = "Finance Auxiliary Cost Balance Excel Report", KeyValue = "FinanceAuxiliaryCostBalanceExcelReport.rpt")]
        FinanceAuxiliaryCostBalanceExcelReport,

        /// <summary>
        /// The finance trial balance client excel report
        /// </summary>
        [EnumValueData(Name = "Finance Trial Balance Client Excel Report", KeyValue = "FinanceTrialBalanceClientExcelReport.rpt")]
        FinanceTrialBalanceClientExcelReport,

        /// <summary>
        /// The payroll monthly wage acc report
        /// </summary>
        [EnumValueData(Name = "Payroll Monthly Wage AccReport", KeyValue = "PayrollMonthlyWageAccReport.rpt")]
        PayrollMonthlyWageAccReport,

        /// <summary>
        /// The marine crew on board cash advance request for multiple crew report
        /// </summary>
        [EnumValueData(Name = "Marine Crew OnBoard Cash Advance Request For Multiple Crew Report", KeyValue = "MarineCrewOnBoardCashAdvanceRequestForMultipleCrewReport.rpt")]
        MarineCrewOnBoardCashAdvanceRequestForMultipleCrewReport,

        /// <summary>
        /// The finance balance sheet excel report
        /// </summary>
        [EnumValueData(Name = "Finance Balance Sheet Excel Report", KeyValue = "FinanceBalanceSheetExcelReport.rpt")]
        FinanceBalanceSheetExcelReport,

        /// <summary>
        /// The finance balance sheet fleet excel report
        /// </summary>
        [EnumValueData(Name = "Finance Balance Sheet Fleet Excel Report", KeyValue = "FinanceBalanceSheetFleetExcelReport.rpt")]
        FinanceBalanceSheetFleetExcelReport,

        /// <summary>
        /// The finance profit and loss excel report
        /// </summary>
        [EnumValueData(Name = "Finance Profit And Loss Excel Report", KeyValue = "FinanceProfitAndLossExcelReport.rpt")]
        FinanceProfitAndLossExcelReport,

        /// <summary>
        /// The finance profit and loss for fleet excel report
        /// </summary>
        [EnumValueData(Name = "Finance Profit And Loss For Fleet Excel Report", KeyValue = "FinanceProfitAndLossForFleetExcelReport.rpt")]
        FinanceProfitAndLossForFleetExcelReport,

        /// <summary>
        /// The finance client account ledger excel report
        /// </summary>
        [EnumValueData(Name = "Finance Client Account Ledger Excel Report", KeyValue = "FinanceClientAccountLedgerExcelReport.rpt")]
        FinanceClientAccountLedgerExcelReport,

        /// <summary>
        /// The finance running cost currency month report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Currency Month Report", KeyValue = "FinanceRunningCostCurrMonthReport.rpt")]
        FinanceRunningCostCurrMonthReport,

        /// <summary>
        /// The finance running cost expenses report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Expenses Report", KeyValue = "FinanceRunningCostExpensesReport.rpt")]
        FinanceRunningCostExpensesReport,

        /// <summary>
        /// The finance running cost currency month msi report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Currency Month MSI Report", KeyValue = "FinanceRunningCostCurrMonthMSIReport.rpt")]
        FinanceRunningCostCurrMonthMSIReport,

        /// <summary>
        /// The finance running cost quarterly summary report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Quarterly Summary Report", KeyValue = "FinanceRunningCostQtrSummaryReport.rpt")]
        FinanceRunningCostQtrSummaryReport,

        /// <summary>
        /// The finance running cost quarterly forecast report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Quarterly Forecast Report", KeyValue = "FinanceRunningCostQtrForecastReport.rpt")]
        FinanceRunningCostQtrForecastReport,

        /// <summary>
        /// The finance running cost currency month excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Currency Month Excel Report", KeyValue = "FinanceRunningCostCurrMonthExcelReport.rpt")]
        FinanceRunningCostCurrMonthExcelReport,

        /// <summary>
        /// The finance running cost expenses excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Expenses Excel Report", KeyValue = "FinanceRunningCostExpensesExcelReport.rpt")]
        FinanceRunningCostExpensesExcelReport,

        /// <summary>
        /// The finance running cost QTR forecast excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost quarterly Forecast Excel Report", KeyValue = "FinanceRunningCostQtrForecastExcelReport.rpt")]
        FinanceRunningCostQtrForecastExcelReport,

        /// <summary>
        /// The finance running cost currency month MSI excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Currency Month MSI Excel Report", KeyValue = "FinanceRunningCostCurrMonthMSIExcelReport.rpt")]
        FinanceRunningCostCurrMonthMSIExcelReport,

        /// <summary>
        /// The finance running cost operational results report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Operational Results Report", KeyValue = "FinanceRunningCostOperationalResultsReport.rpt")]
        FinanceRunningCostOperationalResultsReport,

        /// <summary>
        /// The finance running cost operational results excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Operational Results Excel Report", KeyValue = "FinanceRunningCostOperationalResultsExcelReport.rpt")]
        FinanceRunningCostOperationalResultsExcelReport,

        /// <summary>
        /// The finance running cost forecast comment report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Forecast Comment Report", KeyValue = "FinanceRunningCostForecastCommentReport.rpt")]
        FinanceRunningCostForecastCommentReport,

        /// <summary>
        /// The finance running cost total average report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Total Average Report", KeyValue = "FinanceRunningCostTotalAverageReport.rpt")]
        FinanceRunningCostTotalAverageReport,

        /// <summary>
        /// The finance running cost operational report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Operational Report", KeyValue = "FinanceRunningCostOperationalReport.rpt")]
        FinanceRunningCostOperationalReport,

        /// <summary>
        /// The finance running cost operational excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Operational Excel Report", KeyValue = "FinanceRunningCostOperationalExcelReport.rpt")]
        FinanceRunningCostOperationalExcelReport,

        /// <summary>
        /// The finance running cost quarterly summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Quarterly Summary Excel Report", KeyValue = "FinanceRunningCostQtrSummaryExcelReport.rpt")]
        FinanceRunningCostQtrSummaryExcelReport,

        /// <summary>
        /// The finance running cost total average excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Total Average Excel Report", KeyValue = "FinanceRunningCostTotalAverageExcelReport.rpt")]
        FinanceRunningCostTotalAverageExcelReport,

        /// <summary>
        /// The finance account ledger BC1 exclude accruals
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Exclude Accruals", KeyValue = "FinanceAccountLedgerBC1ExcludeAccruals.rpt")]
        FinanceAccountLedgerBC1ExcludeAccruals,

        /// <summary>
        /// The finance account ledger summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger Summary Excel Report", KeyValue = "FinanceAccountLedgerSummaryExcelReport.rpt")]
        FinanceAccountLedgerSummaryExcelReport,

        /// <summary>
        /// The finance account ledger b c1 fleet excel report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Fleet Excel Report", KeyValue = "FinanceAccountLedgerBC1FleetExcelReport.rpt")]
        FinanceAccountLedgerBC1FleetExcelReport,

        /// <summary>
        /// The finance account ledger b c1 exclude accruals excel
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Exclude Accruals Excel", KeyValue = "FinanceAccountLedgerBC1ExcludeAccrualsExcel.rpt")]
        FinanceAccountLedgerBC1ExcludeAccrualsExcel,

        /// <summary>
        /// The finance account ledger b c1 currency totals excel report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Currency Totals Excel Report", KeyValue = "FinanceAccountLedgerBC1CurrencyTotalsExcelReport.rpt")]
        FinanceAccountLedgerBC1CurrencyTotalsExcelReport,

        /// <summary>
        /// The finance account ledger b c1 currency totals summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 Currency Totals Summary Excel Report", KeyValue = "FinanceAccountLedgerBC1CurrencyTotalsSummaryExcelReport.rpt")]
        FinanceAccountLedgerBC1CurrencyTotalsSummaryExcelReport,

        /// <summary>
        /// The finance account ledger b c1 coy summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 COY Summary Excel Report", KeyValue = "FinanceAccountLedgerBC1COYSummaryExcelReport.rpt")]
        FinanceAccountLedgerBC1COYSummaryExcelReport,

        /// <summary>
        /// The finance account ledger b c1 coy details excel report
        /// </summary>
        [EnumValueData(Name = "Finance Account Ledger BC1 COY Details Excel Report", KeyValue = "FinanceAccountLedgerBC1COYDetailsExcelReport.rpt")]
        FinanceAccountLedgerBC1COYDetailsExcelReport,

        /// <summary>
        /// The finance general ledger owner export report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Owner Export Report", KeyValue = "FinanceGeneralLedgerOwnerExportReport.rpt")]
        FinanceGeneralLedgerOwnerExportReport,

        /// <summary>
        /// The finance general ledger nrs report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger NRS Report", KeyValue = "FinanceGeneralLedgerNRSReport.rpt")]
        FinanceGeneralLedgerNRSReport,


        /// <summary>
        /// The marine crew on board cash advance request for single crew report
        /// </summary>
        [EnumValueData(Name = "Marine Crew OnBoard Cash Advance Request For Single Crew Report", KeyValue = "MarineCrewOnBoardCashAdvanceRequestForSingleCrewReport.rpt")]
        MarineCrewOnBoardCashAdvanceRequestForSingleCrewReport,

        /// <summary>
        /// The marine voyage off hire report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage OffHire Report", KeyValue = "MarineVoyageOffHireReport.rpt")]
        MarineVoyageOffHireReport,

        /// <summary>
        /// The marine voyage bunker performance fleet report
        /// </summary>
        [EnumValueData(Name = "Marine Voyage Bunker Performance Fleet Report", KeyValue = "MarineVoyageBunkerPerformanceFleetReport.rpt")]
        MarineVoyageBunkerPerformanceFleetReport,

        /// <summary>
        /// The finance expenditure statement summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Expenditure Statement Summary Excel Report", KeyValue = "FinanceExpenditureStatementSummaryExcelReport.rpt")]
        FinanceExpenditureStatementSummaryExcelReport,

        /// <summary>
        /// The finance expenditure statement details export report
        /// </summary>
        [EnumValueData(Name = "Finance Expenditure Statement Details Export Report", KeyValue = "FinanceExpenditureStatementDetailsExportReport.rpt")]
        FinanceExpenditureStatementDetailsExportReport,

        /// <summary>
        /// The finance commitment listing excel report
        /// </summary>
        [EnumValueData(Name = "Finance Commitment Listing Excel Report", KeyValue = "FinanceCommitmentListingExcelReport.rpt")]
        FinanceCommitmentListingExcelReport,

        /// <summary>
        /// The finance commitment acc sequence excel report
        /// </summary>
        [EnumValueData(Name = "Finance Commitment Account Sequence Excel Report", KeyValue = "FinanceCommitmentAccSequenceExcelReport.rpt")]
        FinanceCommitmentAccSequenceExcelReport,

        /// <summary>
        /// The marine crew on board allotment report
        /// </summary>
        [EnumValueData(Name = "Marine Crew OnBoard Allotment Report", KeyValue = "MarineCrewOnBoardAllotmentReport.rpt")]
        MarineCrewOnBoardAllotmentReport,

        /// <summary>
        /// The finance dania internal extract report
        /// </summary>
        [EnumValueData(Name = "Finance Dania Internal Extract Report", KeyValue = "FinanceDaniaInternalExtractReport.rpt")]
        FinanceDaniaInternalExtractReport,

        /// <summary>
        /// The finance dania data extract report
        /// </summary>
        [EnumValueData(Name = "Finance Dania Data Extract Report", KeyValue = "FinanceDaniaDataExtractReport.rpt")]
        FinanceDaniaDataExtractReport,

        /// <summary>
        /// The finance selandia data extract report
        /// </summary>
        [EnumValueData(Name = "Finance Selandia Data Extract Report", KeyValue = "FinanceSelandiaDataExtractReport.rpt")]
        FinanceSelandiaDataExtractReport,

        /// <summary>
        /// The finance north sea data extract report
        /// </summary>
        [EnumValueData(Name = "Finance North Sea Data Extract Report", KeyValue = "FinanceNorthSeaDataExtractReport.rpt")]
        FinanceNorthSeaDataExtractReport,

        /// <summary>
        /// The finance osg data extract report
        /// </summary>
        [EnumValueData(Name = "Finance OSG Data Extract Report", KeyValue = "FinanceOSGDataExtractReport.rpt")]
        FinanceOSGDataExtractReport,

        /// <summary>
        /// The finance client chart month transaction excel report
        /// </summary>
        [EnumValueData(Name = "Finance Client Chart Month Transaction Excel Report", KeyValue = "FinanceClientChartMonthTransactionExcelReport.rpt")]
        FinanceClientChartMonthTransactionExcelReport,

        /// <summary>
        /// The finance union maritime extract report
        /// </summary>
        [EnumValueData(Name = "Finance Union Sea Maritime Extract Report", KeyValue = "FinanceUnionMaritimeExtractReport.rpt")]
        FinanceUnionMaritimeExtractReport,

        /// <summary>
        /// The finance transpetro data extract report
        /// </summary>
        [EnumValueData(Name = "Finance Transpetro Data Extract Report", KeyValue = "FinanceTranspetroDataExtractReport.rpt")]
        FinanceTranspetroDataExtractReport,

        /// <summary>
        /// The finance CLS data extract report
        /// </summary>
        [EnumValueData(Name = "Finance CLS Data Extract Report", KeyValue = "FinanceCLSDataExtractReport.rpt")]
        FinanceCLSDataExtractReport,

        /// <summary>
        /// A payroll summary of earning report enum value.
        /// </summary>
        [EnumValueData(Name = "Payroll Summary of Earnings Report", KeyValue = "PayrollSummaryOfEarningsReport.rpt")]
        PayrollSummaryOfEarningsReport,

        /// <summary>
        /// A payroll summary of earning excel report enum value.
        /// </summary>
        [EnumValueData(Name = "Payroll Summary of Earnings Excel Report", KeyValue = "PayRollSummaryOfEarningsExcelReport.rpt")]
        PayRollSummaryOfEarningsExcelReport,

        /// <summary>
        /// A payroll pay item summary report.
        /// </summary>
        [EnumValueData(Name = "Payroll Pay Item Summary Report", KeyValue = "PayrollPayItemSummaryReport.rpt")]
        PayrollPayItemSummaryReport,

        /// <summary>
        /// A payroll pay item summary excel report.
        /// </summary>
        [EnumValueData(Name = "Payroll Pay Item Summary Excel Report", KeyValue = "PayRollPayItemSummaryExcelReport.rpt")]
        PayRollPayItemSummaryExcelReport,

        /// <summary>
        /// A payroll monthly analysis report.
        /// </summary>
        [EnumValueData(Name = "Payroll Monthly Analysis Report", KeyValue = "PayrollMonthlyAnalysisReport.rpt")]
        PayrollMonthlyAnalysisReport,

        /// <summary>
        /// A payroll monthly analysis excel report.
        /// </summary>
        [EnumValueData(Name = "Payroll Monthly Analysis Excel Report", KeyValue = "PayrollMonthlyAnalysisExcelReport.rpt")]
        PayrollMonthlyAnalysisExcelReport,

        /// <summary>
        /// The payroll process summary report
        /// </summary>
        [EnumValueData(Name = "Payroll Process Summary Report", KeyValue = "PayrollProcessSummaryReport.rpt")]
        PayrollProcessSummaryReport,

        /// <summary>
        /// The crewing on board appraisal details report
        /// </summary>
        [EnumValueData(Name = "Crewing OnBoard Appraisal Report", KeyValue = "CrewingOnBoardAppraisalDetailsReport.rpt")]
        CrewingOnBoardAppraisalDetailsReport,

        /// <summary>
        /// The crewing curriculum vitae atlas report
        /// </summary>
        [EnumValueData(Name = "Crewing Curriculum Vitae Atlas Report", KeyValue = "CMS - Atlas_CV_new.rpt")]
        CrewingCurriculumVitaeAtlasReport,

        /// <summary>
        /// The purchasing vessel stored orders excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Vessel Stored Orders Excel Report", KeyValue = "Purchasing_VesselStoredOrdersExcelReport.rpt")]
        PurchasingVesselStoredOrdersExcelReport,

        /// <summary>
        /// The purchasing outstanding order by account excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Outstanding Order By Account Excel Report", KeyValue = "Purchasing_OutstandingOrderByAccountExcelReport.rpt")]
        PurchasingOutstandingOrderByAccountExcelReport,

        /// <summary>
        /// The purchasing vessel order status excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Vessel Order Status Excel Report", KeyValue = "Purchasing_VesselOrderStatusExcelReport.rpt")]
        PurchasingVesselOrderStatusExcelReport,

        /// <summary>
        /// The purchasing order accruals excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Order Accruals Excel Report", KeyValue = "Purchasing_OrderAccrualsExcelReport.rpt")]
        PurchasingOrderAccrualsExcelReport,

        /// <summary>
        /// The purchasing outstanding delivery information excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Outstanding Delivery Information Excel Report", KeyValue = "Purchasing_OutstandingDeliveryInformationExcelReport.rpt")]
        PurchasingOutstandingDeliveryInformationExcelReport,

        /// <summary>
        /// The purchasing supplier purchase orders excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Supplier Purchase Orders Excel Report", KeyValue = "Purchasing_SupplierPurchaseOrdersExcelReport.rpt")]
        PurchasingSupplierPurchaseOrdersExcelReport,

        /// <summary>
        /// The purchasing invoice comparison excel Excel report
        /// </summary>
        [EnumValueData(Name = "Invoice comparison Report", KeyValue = "PurchasingInvoiceComparisonExcelReport.rpt")]
        PurchasingInvoiceComparisonExcelReport,

        /// <summary>
        /// The purchasing disputed invoices excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Disputed Invoices Excel Report", KeyValue = "PurchasingDisputedInvoicesExcelReport.rpt")]
        PurchasingDisputedInvoicesExcelReport,

        /// <summary>
        /// The purchasing invoice comparator excel report
        /// </summary>
        [EnumValueData(Name = "Purchasing Invoice Comparator Excel Report", KeyValue = "PurchasingInvoiceComparatorExcelReport.rpt")]
        PurchasingInvoiceComparatorExcelReport,

        /// <summary>
        /// The marine crew imo list report
        /// </summary>
        [EnumValueData(Name = "Marine Crew IMO List Report", KeyValue = "MarineCrewIMOListReport.rpt")]
        MarineCrewIMOListReport,

        /// <summary>
        /// The finance vessel invoice sales template directly report
        /// </summary>
        [EnumValueData(Name = "Finance Vessel Invoice Sales Template Directly Report", KeyValue = "FinanceVesselInvoiceSalesTemplateDirectlyReport.rpt")]
        FinanceVesselInvoiceSalesTemplateDirectlyReport,

        /// <summary>
        /// The finance vessel invoice sales template on behalf report
        /// </summary>
        [EnumValueData(Name = "Finance Vessel Invoice Sales Template On Behalf Report", KeyValue = "FinanceVesselInvoiceSalesTemplateOnBehalfReport.rpt")]
        FinanceVesselInvoiceSalesTemplateOnBehalfReport,

        /// <summary>
        /// Marine Drill and Campaign Maintainer Details Report
        /// </summary>
        [EnumValueData(Name = "Marine Drill and Campaign Maintainer Details Report", KeyValue = "MarineDrillandCampaignMaintainerDetailsReport.rpt")]
        MarineDrillandCampaignMaintainerDetailsReport,

        /// <summary>
        /// The marine drilland campaign maintainer details excel report
        /// </summary>
        [EnumValueData(Name = "Marine Drill and Campaign Maintainer Details Excel Report", KeyValue = "MarineDrillandCampaignMaintainerDetailsExcelReport.rpt")]
        MarineDrillandCampaignMaintainerDetailsExcelReport,

        /// <summary>
        /// The marine drilland campaign vessel details excel report
        /// </summary>
        [EnumValueData(Name = "Marine Drill and Campaign vessel Details Excel Report", KeyValue = "MarineDrillandCampaignVesselDetailsExcelReport.rpt")]
        MarineDrillandCampaignVesselDetailsExcelReport,
        /// <summary>
        /// Marine Drill and Campaign Maintainer List Report
        /// </summary>
        [EnumValueData(Name = "Marine Drill and Campaign Maintainer List Report", KeyValue = "MarineDrillandCampaignMaintainerListReport.rpt")]
        MarineDrillandCampaignMaintainerListReport,

        /// <summary>
        /// Marine Drill and Campaign Vessel Details Report
        /// </summary>
        [EnumValueData(Name = "Marine Drill and Campaign Vessel Details Report", KeyValue = "MarineDrillandCampaignVesselDetailsReport.rpt")]
        MarineDrillandCampaignVesselDetailsReport,

        /// <summary>
        /// Marine Drill and Campaign Vessel List Report
        /// </summary>
        [EnumValueData(Name = "Marine Drill and Campaign Vessel List Report", KeyValue = "MarineDrillandCampaignVesselListReport.rpt")]
        MarineDrillandCampaignVesselListReport,

        /// <summary>
        /// Vessel in Management Report.
        /// </summary>
        [EnumValueData(Name = "Vessels in Management Report", KeyValue = "Analytics_And_Dashboard_Vessels_In_Management_Report")]
        AnalyticsAndDashboardVesselsInManagementReport,

        /// <summary>
        /// The marine crew on board expense report
        /// </summary>
        [EnumValueData(Name = "Crew OnBoard Expense Report", KeyValue = "MarineCrewOnBoardExpenseReport.rpt")]
        MarineCrewOnBoardExpenseReport,

        /// <summary>
        /// The marine inventory dangerous goods report.
        /// </summary>
        [EnumValueData(Name = "Marine Inventory Dangerous Goods Report", KeyValue = "MarineInventoryDangerousGoodsReport.rpt")]
        MarineInventoryDangerousGoodsReport,

        /// <summary>
        /// The marine haz occ dashboard vessel excel report
        /// </summary>
        [EnumValueData(Name = "Marine HazOcc Dashboard Vessel Excel Report", KeyValue = "MarineHazOccDashboardVesselExcelReport.rpt")]
        MarineHazOccDashboardVesselExcelReport,

        /// <summary>
        /// The marine hazocc dashboard excel report
        /// </summary>
        [EnumValueData(Name = "Marine Hazocc Dashboard Excel Report", KeyValue = "MarineHazoccDashboardExcelReport.rpt")]
        MarineHazoccDashboardExcelReport,

        /// <summary>
        /// The marine position list summary report
        /// </summary>
        [EnumValueData(Name = "Marine Position List Summary Report", KeyValue = "MarinePositionListSummaryReport.rpt")]
        MarinePositionListSummaryReport,

        /// <summary>
        /// The marine position list summary vessel report
        /// </summary>
        [EnumValueData(Name = "Marine Position List Summary Vessel Report", KeyValue = "MarinePositionListSummaryVesselReport.rpt")]
        MarinePositionListSummaryVesselReport,

        /// <summary>
        /// The marine pc inventory report
        /// </summary>
        [EnumValueData(Name = "Marine PC Inventory Report", KeyValue = "MarinePCInventoryReport.rpt")]
        MarinePCInventoryReport,

        /// <summary>
        /// The marine pc inventory fleet yearly audit report
        /// </summary>
        [EnumValueData(Name = "Marine PC Inventory Fleet Yearly Audit Report", KeyValue = "MarinePCInventoryFleetYearlyAuditReport.rpt")]
        MarinePCInventoryFleetYearlyAuditReport,

        /// <summary>
        /// The marine pc inventory yearly audit excel report
        /// </summary>
        [EnumValueData(Name = "Marine PC Inventory Yearly Audit Excel Report", KeyValue = "MarinePCInventoryYearlyAuditExcelReport.rpt")]
        MarinePCInventoryYearlyAuditExcelReport,
        /// <summary>
        /// The marine pc inventory fleet report
        /// </summary>
        [EnumValueData(Name = "Marine PC Inventory Fleet Report", KeyValue = "MarinePCInventoryFleetReport.rpt")]
        MarinePCInventoryFleetReport,
        /// <summary>
        /// The marine position list summary vessel report
        /// </summary>
        [EnumValueData(Name = "Marine PC Inventory Excel Report", KeyValue = "MarinePCInventoryExcelReport.rpt")]
        MarinePCInventoryExcelReport,

        /// <summary>
        /// The marine job detail report
        /// </summary>
        [EnumValueData(Name = "Marine Job Detail Report", KeyValue = "MarineJobDetailReport.rpt")]
        MarineJobDetailReport,

        /// <summary>
        /// The finance funding position summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Funding Position Summary Excel Report", KeyValue = "FinanceFundingPositionSummaryExcelReport.rpt")]
        FinanceFundingPositionSummaryExcelReport,

        /// <summary>
        /// The finance fleet funding position summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Fleet Funding Position Summary Excel Report", KeyValue = "FinanceFleetFundingPositionSummaryExcelReport.rpt")]
        FinanceFleetFundingPositionSummaryExcelReport,

        /// <summary>
        /// The finance funding position summary bsa excel report
        /// </summary>
        [EnumValueData(Name = "Finance Funding Position Summary BSA Excel Report", KeyValue = "FinanceFundingPositionSummaryBSAExcelReport.rpt")]
        FinanceFundingPositionSummaryBSAExcelReport,

        /// <summary>
        /// The purchasing office buyer volume report
        /// </summary>
        [EnumValueData(Name = "Purchasing Office Buyer Volume Report", KeyValue = "PurchasingOfficeBuyerVolumeReport.rpt")]
        PurchasingOfficeBuyerVolumeReport,

        /// <summary>
        /// The purchasing timeline of requisition report
        /// </summary>
        [EnumValueData(Name = "Purchasing Timeline Of Requisition Report", KeyValue = "PurchasingTimelineOfRequisitionReport.rpt")]
        PurchasingTimelineOfRequisitionReport,

        /// <summary>
        /// The Finance general ledger text report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Text Report", KeyValue = "FinanceGeneralLedgerTextReport.rpt")]
        FinanceGeneralLedgerTextReport,

        /// <summary>
        /// The finance comment report
        /// </summary>
        [EnumValueData(Name = "Finance Comment Report", KeyValue = "FinanceCommentReport.rpt")]
        FinanceCommentReport,

        /// <summary>
        /// The purchasing vessel spend analysis report
        /// </summary>
        [EnumValueData(Name = "Purchasing Vessel Spend Analysis Report", KeyValue = "PurchasingVesselSpendAnalysisReport.rpt")]
        PurchasingVesselSpendAnalysisReport,

        /// <summary>
        /// The purchasing purchase order created for invoice report
        /// </summary>
        [EnumValueData(Name = "Purchasing Purchase Order Created For Invoice Report", KeyValue = "PurchasingPurchaseOrderCreatedForInvoiceReport.rpt")]
        PurchasingPurchaseOrderCreatedForInvoiceReport,

        /// <summary>
        /// The purchasing quotation monitoring detail report
        /// </summary>
        [EnumValueData(Name = "Purchasing Quotation Monitoring Detail Report", KeyValue = "PurchasingQuotationMonitoringDetailReport.rpt")]
        PurchasingQuotationMonitoringDetailReport,

        /// <summary>
        /// The finance purchase ledger all offices report export
        /// </summary>
        [EnumValueData(Name = "Finance Purchase Ledger All Offices Report Export", KeyValue = "FinancePurchaseLedgerAllOfficesReportExport.rpt")]
        FinancePurchaseLedgerAllOfficesReportExport,

        /// <summary>
        /// The finance operational cost drill down internal excel report
        /// </summary>
        [EnumValueData(Name = "Finance Operational Cost DrillDown Internal Excel Report", KeyValue = "FinanceOperationalCostDrillDownInternalExcelReport.rpt")]
        FinanceOperationalCostDrillDownInternalExcelReport,

        /// <summary>
        /// The finance operational cost drill down period excel report
        /// </summary>
        [EnumValueData(Name = "Finance Operational Cost DrillDown Period Excel Report", KeyValue = "FinanceOperationalCostDrillDownPeriodExcelReport.rpt")]
        FinanceOperationalCostDrillDownPeriodExcelReport,

        /// <summary>
        /// The finance master chart accounts report
        /// </summary>
        [EnumValueData(Name = "Finance Master Chart Accounts Report", KeyValue = "FinanceMasterChartAccountsReport.rpt")]
        FinanceMasterChartAccountsReport,

        /// <summary>
        /// The finance crew budget variance detail report
        /// </summary>
        [EnumValueData(Name = "Finance Crew Budget VarianceDetailReport", KeyValue = "FinanceCrewBudgetVarianceDetailReport.rpt")]
        FinanceCrewBudgetVarianceDetailReport,

        /// <summary>
        /// The marine job listing excel report
        /// </summary>
        [EnumValueData(Name = "Marine Job Listing Excel Report", KeyValue = "MarineJobListingExcelReport.rpt")]
        MarineJobListingExcelReport,

        /// <summary>
        /// The marine job listing report
        /// </summary>
        [EnumValueData(Name = "Marine Job Listing Report", KeyValue = "MarineJobListingReport.rpt")]
        MarineJobListingReport,

        /// <summary>
        /// The finance master chart audit log report
        /// </summary>
        [EnumValueData(Name = "Finance Master Chart Audit Log Report", KeyValue = "FinanceMasterChartAuditLogReport.rpt")]
        FinanceMasterChartAuditLogReport,

        /// <summary>
        /// The finance general ledger audit list report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Audit List Report", KeyValue = "FinanceGeneralLedgerAuditListReport.rpt")]
        FinanceGeneralLedgerAuditListReport,

        /// <summary>
        /// The finance general ledger owner report
        /// </summary>
        [EnumValueData(Name = "Finance General Ledger Owner Report", KeyValue = "FinanceGeneralLedgerOwnerReport.rpt")]
        FinanceGeneralLedgerOwnerReport,

        /// <summary>
        /// The finance pending disputed invoice excel report
        /// </summary>
        [EnumValueData(Name = "Finance Pending Disputed Invoice Excel Report", KeyValue = "FinancePendingDisputedInvoiceExcelReport.rpt")]
        FinancePendingDisputedInvoiceExcelReport,

        /// <summary>
        /// The finance outstanding sales invoice report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Sales Invoice Report", KeyValue = "FinanceOutstandingSalesInvoiceReport.rpt")]
        FinanceOutstandingSalesInvoiceReport,

        /// <summary>
        /// The finance outstanding sales invoice excel report
        /// </summary>
        [EnumValueData(Name = "Finance Outstanding Sales Invoice Excel Report", KeyValue = "FinanceOutstandingSalesInvoiceExcelReport.rpt")]
        FinanceOutstandingSalesInvoiceExcelReport,

        /// <summary>
        /// The salary transfer letter report
        /// </summary>
        [EnumValueData(Name = "Crew Salary Transfer Letter", KeyValue = "CRWSalaryTransferLetter.rpt")]
        SalaryTransferLetterReport,

        /// <summary>
        /// The finance office funding position summary report
        /// </summary>
        [EnumValueData(Name = "Finance Office Funding Position Summary Report", KeyValue = "FinanceOfficeFundingPositionSummaryReport.rpt")]
        FinanceOfficeFundingPositionSummaryReport,

        /// <summary>
        /// The finance office funding position summary excel report
        /// </summary>
        [EnumValueData(Name = "Finance Office Funding Position Summary Excel Report", KeyValue = "FinanceOfficeFundingPositionSummaryExcelReport.rpt")]
        FinanceOfficeFundingPositionSummaryExcelReport,

        /// <summary>
        /// The finance running cost curr month msi comment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Curr Month MSI Comment Excel Report", KeyValue = "FinanceRunningCostCurrMonthMSICommentExcelReport.rpt")]
        FinanceRunningCostCurrMonthMSICommentExcelReport,

        /// <summary>
        /// The finance running cost operational comment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Operational Comment Excel Report", KeyValue = "FinanceRunningCostOperationalCommentExcelReport.rpt")]
        FinanceRunningCostOperationalCommentExcelReport,

        /// <summary>
        /// The finance running cost operational results comment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Operational Results Comment Excel Report", KeyValue = "FinanceRunningCostOperationalResultsCommentExcelReport.rpt")]
        FinanceRunningCostOperationalResultsCommentExcelReport,

        /// <summary>
        /// The finance running cost QTR forecast comment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Qtr Forecast Comment Excel Report", KeyValue = "FinanceRunningCostQtrForecastCommentExcelReport.rpt")]
        FinanceRunningCostQtrForecastCommentExcelReport,

        /// <summary>
        /// The finance running cost QTR summary comment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Qtr Summary Comment Excel Report", KeyValue = "FinanceRunningCostQtrSummaryCommentExcelReport.rpt")]
        FinanceRunningCostQtrSummaryCommentExcelReport,

        /// <summary>
        /// The finance running cost total average comment excel report
        /// </summary>
        [EnumValueData(Name = "Finance Running Cost Total Average Comment Excel Report", KeyValue = "FinanceRunningCostTotalAverageCommentExcelReport.rpt")]
        FinanceRunningCostTotalAverageCommentExcelReport,

        /// <summary>
        /// The dashboard overall fleet performance report
        /// </summary>
        [EnumValueData(Name = "Dashboard Overall Fleet Performance Report", KeyValue = "DashboardOverallFleetPerformanceReport.rpt")]
        DashboardOverallFleetPerformanceReport,

        /// <summary>
        /// The Dashboard Overall Office Performance Report
        /// </summary>
        [EnumValueData(Name = "Dashboard Overall Office Performance Report", KeyValue = "DashboardOverallOfficePerformanceReport.rpt")]
        DashboardOverallOfficePerformanceReport,

        /// <summary>
        /// The dashboard group kp is report
        /// </summary>
        [EnumValueData(Name = "Dashboard Group KPIs Report", KeyValue = "DashboardGroupKPIsReport.rpt")]
        DashboardGroupKPIsReport,

        /// <summary>
        /// The dashboard office kp is report
        /// </summary>
        [EnumValueData(Name = "Dashboard Office KPIs Report", KeyValue = "DashboardOfficeKPIsReport.rpt")]
        DashboardOfficeKPIsReport,

        /// <summary>
        /// The marine garbage record book report
        /// </summary>
        [EnumValueData(Name = "Marine Garbage Record Book Report", KeyValue = "MarineGarbageRecordBookReport.rpt")]
        MarineGarbageRecordBookReport,

        /// <summary>
        /// The finance operational cost period report
        /// </summary>
        [EnumValueData(Name = "Finance Operational Cost Period Excel Report", KeyValue = "FinanceOperationalCostPeriodExcelReport.rpt")]
        FinanceOperationalCostPeriodExcelReport,

        /// <summary>
        /// The finance operational cost period report
        /// </summary>
        [EnumValueData(Name = "Finance Operational Cost Period Report", KeyValue = "FinanceOperationalCostPeriodReport.rpt")]
        FinanceOperationalCostPeriodReport,

        /// <summary>
        /// The finance master chart running cost curr month report
        /// </summary>
        [EnumValueData(Name = "Finance Master Chart Running Cost Curr Month Report", KeyValue = "FinanceMasterChartRunningCostCurrMonthReport.rpt")]
        FinanceMasterChartRunningCostCurrMonthReport,

        /// <summary>
        /// The finance master chart running cost curr month excel report
        /// </summary>
        [EnumValueData(Name = "Finance Master Chart Running Cost Curr Month Excel Report", KeyValue = "FinanceMasterChartRunningCostCurrMonthExcelReport.rpt")]
        FinanceMasterChartRunningCostCurrMonthExcelReport,

        /// <summary>
        /// The purchasing leaving management dashboard report
        /// </summary>
        [EnumValueData(Name = "Purchasing Leaving Management Dashboard Report", KeyValue = "Purchasing_LeavingManagementDashboardReport.rpt")]
        Purchasing_LeavingManagementDashboardReport,

        /// <summary>
        /// The purchasing entity export analysis tab report
        /// </summary>
        [EnumValueData(Name = "Purchasing Entity Export Analysis Report", KeyValue = "PurchasingEntityExportAnalysisTabReport.rpt")]
        PurchasingEntityExportAnalysisTabReport,

        /// <summary>
        /// The purchasing entity audit log report
        /// </summary>
        [EnumValueData(Name = "Purchasing Entity A Report", KeyValue = "Purchasing_Entity_AuditLogReport.rpt")]
        PurchasingEntityAuditLogReport,

        /// <summary>
        /// The purchasing entity memo report
        /// </summary>
        [EnumValueData(Name = "Purchasing Entity MemoReport", KeyValue = "Purchasing_Entity_MemoReport.rpt")]
        PurchasingEntityMemoReport,

        /// <summary>
        /// The marine inspection question answer report
        /// </summary>
        [EnumValueData(Name = "Marine Inspection QA Report", KeyValue = "MarineInspectionQuestionAnswerReport.rpt")]
        MarineInspectionQuestionAnswerReport,
    }

    /// <summary>
    /// The exports types for a report.
    /// </summary>
    public enum ReportExportTypes
    {
        /// <summary>
        /// PDF format.
        /// </summary>
        [EnumValueData(Name = "PDF", KeyValue = ".pdf")]
        PDF,
        /// <summary>
        /// XML format.
        /// </summary>
        [EnumValueData(Name = "XML", KeyValue = ".xml")]
        XML,
        /// <summary>
        /// RichText format.
        /// </summary>
        [EnumValueData(Name = "RichText", KeyValue = ".rtf")]
        RichText,
        /// <summary>
        /// Excel format.
        /// </summary>
        [EnumValueData(Name = "Excel", KeyValue = ".xls")]
        Excel,

        /// <summary>
        /// Excel with open XML format
        /// </summary>
        [EnumValueData(Name = "ExcelXLSX", KeyValue = ".xlsx")]
        ExcelXLSX,

        /// <summary>
        /// The excel XLSM Macro Enabled Worksheet
        /// </summary>
        [EnumValueData(Name = "ExcelXLSM", KeyValue = ".xlsm")]
        ExcelXLSM,

        /// <summary>
        /// Word Format.
        /// </summary>
        [EnumValueData(Name = "Word", KeyValue = ".doc")]
        Word,

        /// <summary>
        /// CSV Format.
        /// </summary>
        [EnumValueData(Name = "CSV", KeyValue = ".csv")]
        CSV,

        /// <summary>
        /// Text Format.
        /// </summary>
        [EnumValueData(Name = "Text", KeyValue = ".txt")]
        Text
    }

    /// <summary>
    /// Defect work order reschedule status
    /// </summary>
    public enum DefectWorkOrderRescheduleStatus
    {
        /// <summary>
        /// The pending
        /// </summary>
        [EnumValueData(Name = "Pending", KeyValue = "GLAS00000042")]
        Pending,

        /// <summary>
        /// The approved
        /// </summary>
        [EnumValueData(Name = "Approved", KeyValue = "GLAS00000043")]
        Approved,

        /// <summary>
        /// The rejected
        /// </summary>
        [EnumValueData(Name = "Rejected", KeyValue = "GLAS00000044")]
        Rejected
    }

    /// <summary>
    /// CloudDocumentField
    /// </summary>
    public enum CloudDocumentField
    {
        /// <summary>
        /// The coy identifier
        /// </summary>
        [EnumValueData(Name = "CoyId", KeyValue = "CoyId")]
        CoyId
    }

    /// <summary>
    /// Crew Detail Source
    /// </summary>
    public enum CrewDetailSource
    {
        /// <summary>
        /// The crew list
        /// </summary>
        [EnumValueData(Name = "Crew List", KeyValue = "1")]
        CrewList,

        /// <summary>
        /// The medical sign off list
        /// </summary>
        [EnumValueData(Name = "Medical Sign Off List", KeyValue = "2")]
        MedicalSignOffList,
    }
    /// <summary>
    /// VesselEntityType
    /// </summary>
    public enum VesselEntityType
    {
        /// <summary>
        /// The entity
        /// </summary>
        [EnumValueData(Name = "Entity", KeyValue = "A")]
        Entity,
        /// <summary>
        /// The vessel
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "V")]
        Vessel
    }


    /// <summary>
    /// AccountType
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// The posting
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "P")]
        Posting,

        /// <summary>
        /// The summary
        /// </summary>
        [EnumValueData(Name = "Vessel", KeyValue = "S")]
        Summary
    }

    /// <summary>
    /// KPIPriority
    /// </summary>
    public enum KPIPriority
    {
        /// <summary>
        /// The green
        /// </summary>
        [EnumValueData(Name = "#11C256", KeyValue = "0")]
        Green,

        /// <summary>
        /// The amber
        /// </summary>
        [EnumValueData(Name = "#FF803D", KeyValue = "1")]
        Amber,

        /// <summary>
        /// The red
        /// </summary>
        [EnumValueData(Name = "#FF544A", KeyValue = "2")]
        Red
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PMSDashboardStage
    {
        /// <summary>
        /// Due
        /// </summary>
        [EnumValueData(Name = "Due", KeyValue = "Due")]
        Due,

        /// <summary>
        /// Critical Due
        /// </summary>
        [EnumValueData(Name = "CriticalDue", KeyValue = "CriticalDue")]
        CriticalDue,

        /// <summary>
        /// Overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue,

        /// <summary>
        /// Critical Overdue
        /// </summary>
        [EnumValueData(Name = "CriticalOverdue", KeyValue = "CriticalOverdue")]
        CriticalOverdue,

        /// <summary>
        /// PlannedFor
        /// </summary>
        [EnumValueData(Name = "PlannedFor", KeyValue = "PlannedFor")]
        PlannedFor,

        /// <summary>
        /// ReqReschedule
        /// </summary>
        [EnumValueData(Name = "ReqReschedule", KeyValue = "ReqReschedule")]
        ReqReschedule,

        /// <summary>
        /// Critical
        /// </summary>
        [EnumValueData(Name = "Critical", KeyValue = "Critical")]
        Critical,

        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,

        /// <summary>
        /// The PMS managed certificates
        /// The navigation for this node is from Certificates
        /// </summary>
        [EnumValueData(Name = "PMS Managed Certificates", KeyValue = "PMSManagedCertificates")]
        PMSManagedCertificates,

        /// <summary>
        /// Critical
        /// </summary>
        [EnumValueData(Name = "Completed", KeyValue = "Completed")]
        Completed,

    }

    /// <summary>
    /// Priority for work basket
    /// </summary>
    public enum WorkBasketPriority
    {
        /// <summary>
        /// The non critical
        /// </summary>
        [EnumValueData(Name = "Non Critical", KeyValue = "NC")]
        NonCritical,

        /// <summary>
        /// The critical
        /// </summary>
        [EnumValueData(Name = "Critical", KeyValue = "C")]
        Critical,
    }

    /// <summary>
    /// Enum for the tree filter drop down.
    /// </summary>
    public enum TreeFilterNodeType
    {
        /// <summary>
        /// The department
        /// </summary>
        [EnumValueData(Name = "Department", KeyValue = "Department")]
        Department,

        /// <summary>
        /// The responsibility
        /// </summary>
        [EnumValueData(Name = "Responsibility", KeyValue = "Responsibility")]
        Responsibility,

        /// <summary>
        /// The order stage
        /// </summary>
        [EnumValueData(Name = "OrderStage", KeyValue = "OrderStage")]
        OrderStage,

        /// <summary>
        /// The order status
        /// </summary>
        [EnumValueData(Name = "OrderStatus", KeyValue = "OrderStatus")]
        OrderStatus,

        /// <summary>
        /// The group .
        /// </summary>
        [EnumValueData(Name = "Group", KeyValue = "Group")]
        Group
    }

    /// <summary>
    /// The maintenance attribute lookup code
    /// </summary>
    public enum MaintenanceAttributeLookupCode
    {
        /// <summary>
        /// The alternative number type
        /// </summary>
        [EnumValueData(Name = "AlternativeNumberType", KeyValue = "AlternativeNumberType")]
        AlternativeNumberType,


        /// <summary>
        /// The inventory location condition
        /// </summary>
        [EnumValueData(Name = "InventoryLocationCondition", KeyValue = "InventoryLocationCondition")]
        InventoryLocationCondition,


        /// <summary>
        /// The work order reschedule status
        /// </summary>
        [EnumValueData(Name = "WorkOrderRescheduleStatus", KeyValue = "WorkOrderRescheduleStatus")]
        WorkOrderRescheduleStatus,


        /// <summary>
        /// The vessel condition
        /// </summary>
        [EnumValueData(Name = "VesselCondition", KeyValue = "VesselCondition")]
        VesselCondition,


        /// <summary>
        /// The engine room condition
        /// </summary>
        [EnumValueData(Name = "EngineRoomCondition", KeyValue = "EngineRoomCondition")]
        EngineRoomCondition,


        /// <summary>
        /// The acoustic noise unit of measure
        /// </summary>
        [EnumValueData(Name = "AcousticNoiseUnitOfMeasure", KeyValue = "AcousticNoiseUnitOfMeasure")]
        AcousticNoiseUnitOfMeasure,


        /// <summary>
        /// The fuel oil source
        /// </summary>
        [EnumValueData(Name = "FuelOilSource", KeyValue = "FuelOilSource")]
        FuelOilSource,


        /// <summary>
        /// The lube oil source.
        /// </summary>
        [EnumValueData(Name = "LubeOilSource", KeyValue = "LubeOilSource")]
        LubeOilSource,


        /// <summary>
        /// The vessel accessory and essentials
        /// </summary>
        [EnumValueData(Name = "VesselAccessoryAndEssentials", KeyValue = "VesselAccessoryAndEssentials")]
        VesselAccessoryAndEssentials,


        /// <summary>
        /// The procurement catalogue status
        /// </summary>
        [EnumValueData(Name = "ProcurementCatalogueStatus", KeyValue = "ProcurementCatalogueStatus")]
        ProcurementCatalogueStatus,


        /// <summary>
        /// The add work order request status.
        /// </summary>
        [EnumValueData(Name = "AddWorkOrderRequestStatus", KeyValue = "AddWorkOrderRequestStatus")]
        AddWorkOrderRequestStatus,


        /// <summary>
        /// The PMS request type.
        /// </summary>
        [EnumValueData(Name = "PMSRequestType", KeyValue = "PMSRequestType")]
        PMSRequestType,


        /// <summary>
        /// The edit wo status.
        /// </summary>
        [EnumValueData(Name = "EditWOStatus", KeyValue = "EditWOStatus")]
        EditWOStatus,


        /// <summary>
        /// The job parameter.
        /// </summary>
        [EnumValueData(Name = "JobParameter", KeyValue = "JobParameter")]
        JobParameter,


        /// <summary>
        /// The shelf life duration unit
        /// </summary>
        [EnumValueData(Name = "ShelfLifeDurationUnit", KeyValue = "ShelfLifeDurationUnit")]
        ShelfLifeDurationUnit,


        /// <summary>
        /// The inventory attribute.
        /// </summary>
        [EnumValueData(Name = "InventoryAttribute", KeyValue = "InventoryAttribute")]
        InventoryAttribute,


        /// <summary>
        /// The round job parameter type.
        /// </summary>
        [EnumValueData(Name = "RoundJobParameterType", KeyValue = "RoundJobParameterType")]
        RoundJobParameterType,


        /// <summary>
        /// The reschedule type.
        /// </summary>
        [EnumValueData(Name = "RescheduleType", KeyValue = "RescheduleType")]
        RescheduleType,


        /// <summary>
        /// The stock comment.
        /// </summary>
        [EnumValueData(Name = "StockComment", KeyValue = "StockComment")]
        StockComment,


        /// <summary>
        /// The spare serail no UI source
        /// </summary>
        [EnumValueData(Name = "SpareSerailNoUISource", KeyValue = "SpareSerailNoUISource")]
        SpareSerailNoUISource,


        /// <summary>
        /// The spare serial no status
        /// </summary>
        [EnumValueData(Name = "SpareSerialNoStatus", KeyValue = "SpareSerialNoStatus")]
        SpareSerialNoStatus,


        /// <summary>
        /// The parts used source.
        /// </summary>
        [EnumValueData(Name = "PartsUsedSource", KeyValue = "PartsUsedSource")]
        PartsUsedSource,


        /// <summary>
        /// The required spare source.
        /// </summary>
        [EnumValueData(Name = "RequiredSpareSource", KeyValue = "RequiredSpareSource")]
        RequiredSpareSource,


        /// <summary>
        /// The part type.
        /// </summary>
        [EnumValueData(Name = "PartType", KeyValue = "PartType")]
        PartType,


        /// <summary>
        /// The landing form status.
        /// </summary>
        [EnumValueData(Name = "LandingFormStatus", KeyValue = "LandingFormStatus")]
        LandingFormStatus,


        /// <summary>
        /// The landing item status.
        /// </summary>
        [EnumValueData(Name = "LandingItemStatus", KeyValue = "LandingItemStatus")]
        LandingItemStatus,


        /// <summary>
        /// The landing item source.
        /// </summary>
        [EnumValueData(Name = "LandingItemSource", KeyValue = "LandingItemSource")]
        LandingItemSource
    }

    /// <summary>
    /// PPMAttributeLookup
    /// </summary>
    public enum PPMAttributeLookup
    {
        /// <summary>
        /// Planned for
        /// </summary>
        [EnumValueData(Name = "Planned For", KeyValue = "SYST00000154")]
        RescheduleTypePlannedFor
    }

    /// <summary>
    /// MaintenanceType
    /// </summary>
    public enum MaintenanceType
    {
        /// <summary>
        /// The scheduled work order
        /// </summary>
        [EnumValueData(Name = "Scheduled Work Order", KeyValue = "ScheduledWorkOrder")]
        ScheduledWorkOrder,

        /// <summary>
        /// The ships work order
        /// </summary>
        [EnumValueData(Name = "Ships Work Order", KeyValue = "ShipsWorkOrder")]
        ShipsWorkOrder,

        /// <summary>
        /// The hotel maintenance manager
        /// </summary>
        [EnumValueData(Name = "Hotel Maintenance Manager", KeyValue = "HotelMaintenanceManager")]
        HotelMaintenanceManager
    }

    /// <summary>
    /// Other Filters for PMS
    /// </summary>
    public enum PMSOtherFilter
    {

        /// <summary>
        /// The overdue current month
        /// </summary>
        [EnumValueData(Name = "Overdue (Current Month)", KeyValue = "Overdue(CurrentMonth)")]
        OverdueCurrentMonth,

        /// <summary>
        /// The overdue prior month
        /// </summary>
        [EnumValueData(Name = "Overdue (Prior Month)", KeyValue = "Overdue(PriorMonth)")]
        OverduePriorMonth,

        /// <summary>
        /// The due
        /// </summary>
        [EnumValueData(Name = "Due", KeyValue = "Due")]
        Due
    }

    /// <summary>
    /// Functional Type for System Area
    /// </summary>
    public enum SystemAreaFunctionalType
    {
        /// <summary>
        /// The generic system areas
        /// </summary>
        [EnumValueData(Name = "Generic System Areas", KeyValue = "1")]
        GenericSystemAreas,

        /// <summary>
        /// The consumables
        /// </summary>
        [EnumValueData(Name = "Consumables", KeyValue = "2")]
        Consumables,

        /// <summary>
        /// The hotel defect manager
        /// </summary>
        [EnumValueData(Name = "Hotel Defect Manager", KeyValue = "4")]
        HotelDefectManager
    }

    /// <summary>
    /// The closed workorder history summary
    /// </summary>
    public enum ClosedWorkOrderHistoryStage
    {
        /// <summary>
        /// The overhaul count
        /// </summary>
        [EnumValueData(Name = "OverhaulCount", KeyValue = "OverhaulCount")]
        OverhaulCount,

        /// <summary>
        /// The rescheduled count
        /// </summary>
        [EnumValueData(Name = "RescheduledCount", KeyValue = "RescheduledCount")]
        RescheduledCount,
    }

    /// <summary>
    /// Job class type
    /// </summary>
    public enum JobClassType
    {
        /// <summary>
        /// The overhaul
        /// </summary>
        [EnumValueData(Name = "Overhaul", KeyValue = "SYST00000021")]
        Overhaul,
    }

    /// <summary>
    /// Enum for Reschedule type.
    /// </summary>
    public enum RescheduleType
    {
        /// <summary>
        /// The reschedule.
        /// </summary>
        [EnumValueData(Name = "Reschedule", KeyValue = "SYST00000153")]
        Reschedule,
    }

    /// <summary>
    /// HazOcc Report Types
    /// </summary>
    public enum HazOccTypeCodes
    {
        /// <summary>
        /// The ac
        /// </summary>
        [EnumValueData(Name = "Accident", KeyValue = "INCMAN000021")]
        AC = 1,

        /// <summary>
        /// The ill
        /// </summary>
        [EnumValueData(Name = "Illness", KeyValue = "INCMAN000037")]
        ILL = 3,

        /// <summary>
        /// The near miss
        /// </summary>
        [EnumValueData(Name = "Near Miss", KeyValue = "INCMAN000003")]
        NM = 4,

        /// <summary>
        /// The en
        /// </summary>
        [EnumValueData(Name = "Enforcement Notice", KeyValue = "INCMAN000027")]
        EN = 5,

        /// <summary>
        /// The incident(Unclassified)
        /// </summary>
        [EnumValueData(Name = "Incident", KeyValue = "INCMAN000036")]
        IP = 6,

        /// <summary>
        /// The incident (fire)
        /// </summary>
        [EnumValueData(Name = "Incident (Fire)", KeyValue = "INCMAN000029")]
        IF = 7,

        /// <summary>
        /// The incident (Grounding)
        /// </summary>
        [EnumValueData(Name = "Incident (Grounding/Collision/Contact)", KeyValue = "INCMAN000030")]
        IC = 8,

        /// <summary>
        /// The incident (Equip Failure)
        /// </summary>
        [EnumValueData(Name = "Incident (Equipment Failure)", KeyValue = "INCMAN000031")]
        IE = 9,

        /// <summary>
        /// The incident (Power Loss)
        /// </summary>
        [EnumValueData(Name = "Incident (Process Loss / Failure)", KeyValue = "INCMAN000032")]
        IL = 10,

        /// <summary>
        /// The incident (Env)
        /// </summary>
        [EnumValueData(Name = "Incident (Environmental)", KeyValue = "INCMAN000033")]
        IV = 11,

        /// <summary>
        /// The incident (security)
        /// </summary>
        [EnumValueData(Name = "Incident (Security)", KeyValue = "INCMAN000034")]
        IS = 12,

        /// <summary>
        /// The en
        /// </summary>
        [EnumValueData(Name = "Safe Act/Condition", KeyValue = "INCMAN000024")]
        SA = 13,

        /// <summary>
        /// The en
        /// </summary>
        [EnumValueData(Name = "Unsafe Act/Condition", KeyValue = "INCMAN000020")]
        OB = 14,

        /// <summary>
        /// Crew Accident
        /// </summary>
        [EnumValueData(Name = "Crew Accident", KeyValue = "INCMAN000001")]
        CA = 15,

        /// <summary>
        /// Pasenger Accident
        /// </summary>
        [EnumValueData(Name = "Passenger Accident", KeyValue = "INCMAN000006")]
        PA = 16,

        /// <summary>
        /// Third Party Accidet
        /// </summary>
        [EnumValueData(Name = "Third Party Accident", KeyValue = "INCMAN000018")]
        TA = 17,

        /// <summary>
        /// Safety Obs
        /// </summary>
        [EnumValueData(Name = "Observation", KeyValue = "INCMAN000035")]
        SO = 18,

        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "AllTypes")]
        ALL = 19,

        // (old) Incident Type
        /// <summary>
        /// The ii
        /// </summary>
        [EnumValueData(Name = "Incident(DEL)", KeyValue = "INCMAN000002")]
        II = 20,

        /// <summary>
        /// The ci.
        /// </summary>
        [EnumValueData(Name = "Crew Illness", KeyValue = "INCMAN000038")]
        CI = 21,

        /// <summary>
        /// The pi.
        /// </summary>
        [EnumValueData(Name = "Passenger Illness", KeyValue = "INCMAN000039")]
        PI = 22,

        /// <summary>
        /// The ti.
        /// </summary>
        [EnumValueData(Name = "Third Party Illness", KeyValue = "INCMAN000040")]
        TI = 23,

        /// <summary>
        /// The Allision
        /// </summary>
        [EnumValueData(Name = "Allision", KeyValue = "INCMAN000042")]
        IA = 24,

        /// <summary>
        /// The Collision
        /// </summary>
        [EnumValueData(Name = "Collision", KeyValue = "INCMAN000041")]
        IO = 25,

        /// <summary>
        /// The Heavy Weather Damage
        /// </summary>
        [EnumValueData(Name = "Heavy Weather Damage", KeyValue = "INCMAN000044")]
        IW = 26,

        /// <summary>
        /// The Heavy Flooding
        /// </summary>
        [EnumValueData(Name = "Heavy Flooding", KeyValue = "INCMAN000045")]
        ID = 27,
    }

    /// <summary>
    /// Vessel Specific Attribute type
    /// </summary>
    public enum VesselSpecificAttributeType
    {
        /// <summary>
        /// The HDM hierarchy explorer
        /// </summary>
        [EnumValueData(Name = "HDM Hierarchy Explorer", KeyValue = "GLAS00000027")]
        HDMHierarchyExplorer,

        /// <summary>
        /// The accounts is automatic approval
        /// </summary>
        [EnumValueData(Name = "Auto Approval for Accounts", KeyValue = "GLAS00000028")]
        Accounts_IsAutoApproval,

        /// <summary>
        /// The historic nan no of days
        /// </summary>
        [EnumValueData(Name = "No Of Days for NAN History", KeyValue = "GLAS00000029")]
        HistoricNAN_NoOfDays,

        /// <summary>
        /// The cash account currency
        /// </summary>
        [EnumValueData(Name = "Cash Account Currency", KeyValue = "GLAS00000030")]
        CashAccountCurrency,

        /// <summary>
        /// The welfare account currency
        /// </summary>
        [EnumValueData(Name = "Welfare Account Currency", KeyValue = "GLAS00000031")]
        WelfareAccountCurrency,

        /// <summary>
        /// The slopchest account currency
        /// </summary>
        [EnumValueData(Name = "Slopchest Account Currency", KeyValue = "GLAS00000032")]
        SlopchestAccountCurrency,

        /// <summary>
        /// The is crew changed on vessel
        /// </summary>
        [EnumValueData(Name = "Crew Changed on Vessel", KeyValue = "GLAS00000033")]
        IsCrewChangedOnVessel,

        /// <summary>
        /// The vesselhas accessto performance MGMT
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Performance Management", KeyValue = "GLAS00000034")]
        VesselhasAccesstoPerformanceMgmt,

        /// <summary>
        /// The crew change performed by
        /// </summary>
        [EnumValueData(Name = "Crew Change functionality can be performed by", KeyValue = "GLAS00000035")]
        CrewChangePerformedBy,

        /// <summary>
        /// The vesselhas accessto pending availability request
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Pending Availability Request", KeyValue = "GLAS00000036")]
        VesselhasAccesstoPendingAvailabilityRequest,

        /// <summary>
        /// The vesselhas accessto nan manager
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to NAN Manager", KeyValue = "GLAS00000037")]
        VesselhasAccesstoNANManager,

        /// <summary>
        /// The vesselcan manage crew certificate
        /// </summary>
        [EnumValueData(Name = "Vessel can Manage Crew Certificate", KeyValue = "GLAS00000038")]
        VesselcanManageCrewCertificate,

        /// <summary>
        /// The vessel has access to medical manager.
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Medical Manager", KeyValue = "GLAS00000039")]
        VesselhasAccesstoMedicalManager,

        /// <summary>
        /// The vesselhas accessto allotments
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Allotments", KeyValue = "GLAS00000040")]
        VesselhasAccesstoAllotments,

        /// <summary>
        /// The vesselhas accessto crew management
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Crew Management", KeyValue = "GLAS00000041")]
        VesselhasAccesstoCrewManagement,

        /// <summary>
        /// The vesselhas accessto crew ob accounts
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Crew Onboard Accounts", KeyValue = "GLAS00000042")]
        VesselhasAccesstoCrewOBAccounts,

        /// <summary>
        /// The joining briefing overdue days
        /// </summary>
        [EnumValueData(Name = "Joining briefing overdue days", KeyValue = "GLAS00000043")]
        JoiningBriefingOverdueDays,

        /// <summary>
        /// The appraisal next review span
        /// </summary>
        [EnumValueData(Name = "Appraisal next review span", KeyValue = "GLAS00000044")]
        AppraisalNextReviewSpan,

        /// <summary>
        /// The hotel defect manager menu enable
        /// </summary>
        [EnumValueData(Name = "Vessel has Access to Hotel Defect Manager", KeyValue = "GLAS00000045")]
        HotelDefectManagerMenuEnable,

        /// <summary>
        /// The HTM l5 profile instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Profile Instruction", KeyValue = "GLAS00000046")]
        HTML5ProfileInstruction,

        /// <summary>
        /// The HTM l5 nan instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 NAN Instruction", KeyValue = "GLAS00000047")]
        HTML5NANInstruction,

        /// <summary>
        /// The HTM l5 availability request instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Availability Request Instruction", KeyValue = "GLAS00000048")]
        HTML5AvailabilityRequestInstruction,

        /// <summary>
        /// The HTM l5 banking instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Banking Instruction", KeyValue = "GLAS00000049")]
        HTML5BankingInstruction,

        /// <summary>
        /// The HTM l5 allotment instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Allotment Instruction", KeyValue = "GLAS00000050")]
        HTML5AllotmentInstruction,

        /// <summary>
        /// The HTM l5 earning deduction instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Earning Deduction Instruction", KeyValue = "GLAS00000051")]
        HTML5EarningDeductionInstruction,

        /// <summary>
        /// The HTM l5 cash advance instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Cash Advance Instruction", KeyValue = "GLAS00000052")]
        HTML5CashAdvanceInstruction,

        /// <summary>
        /// The HTM l5 unsafe moment instruction
        /// </summary>
        [EnumValueData(Name = "HTML5 Unsafe Moment Instruction", KeyValue = "GLAS00000053")]
        HTML5UnsafeMomentInstruction,

        /// <summary>
        /// The calendar work order planned for maximum days.
        /// </summary>
        [EnumValueData(Name = "Calendar Work order Planned for maximum days", KeyValue = "GLAS00000064")]
        CalendarWOPlannedForMaxDays,

        /// <summary>
        /// The counter work order planned for maximum days
        /// </summary>
        [EnumValueData(Name = "Counter Work order Planned for maximum days", KeyValue = "GLAS00000065")]
        CounterWOPlannedForMaxDays,

        /// <summary>
        /// The pmwo reschedule maximum limit.
        /// </summary>
        [EnumValueData(Name = "Planned calendar work order Reschedule Max Limit", KeyValue = "GLAS00000078")]
        PMWORescheduleMaxLimit,

        /// <summary>
        /// The counter work order reschedule maximum limit.
        /// </summary>
        [EnumValueData(Name = "Planned counter work order Reschedule Max Limit", KeyValue = "GLAS00000087")]
        CounterWorkOrderRescheduleMaxLimit,

        /// <summary>
        /// The planned for maximum limit.
        /// </summary>
        [EnumValueData(Name = "Planned For Max Limit", KeyValue = "GLAS00000088")]
        PlannedForMaxLimit,

        /// <summary>
        /// The pmwo reschedule maximum interval value months.
        /// </summary>
        [EnumValueData(Name = "Reschedule Max Interval value in Months", KeyValue = "GLAS00000080")]
        PMWORescheduleMaxIntervalValueMonths,

        /// <summary>
        /// The swo reschedule maximum interval value month.
        /// </summary>
        [EnumValueData(Name = "Ships Work Order Reschedule Max Interval value in Months", KeyValue = "GLAS00000072")]
        SWORescheduleMaxIntervalValueMonth,

        /// <summary>
        /// The integration vessel mapping.
        /// </summary>
        [EnumValueData(Name = "Integration Vessel Mapping", KeyValue = "GLAS00000144")]
        IntegrationVesselMapping,

        /// <summary>
        /// The voyage integration configuration.
        /// </summary>
        [EnumValueData(Name = "Voyage Integration Configuration", KeyValue = "GLAS00000145")]
        VoyageIntegrationConfiguration,

        /// <summary>
        /// The integration success log.
        /// </summary>
        [EnumValueData(Name = "Integration Success Log", KeyValue = "GLAS00000146")]
        IntegrationSuccessLog,

        /// <summary>
        /// The counter reading actual average add slab in percentage.
        /// </summary>
        [EnumValueData(Name = "Counter Reading Actual Average Add Slab In Percentage", KeyValue = "GLAS00000156")]
        CounterReadingActualAverageAddSlabInPercentage,

        /// <summary>
        /// The counter reading previous actual average multiple limit.
        /// </summary>
        [EnumValueData(Name = "Counter Reading Previous Actual Average Multiple Limit", KeyValue = "GLAS00000157")]
        CounterReadingPrevActualAverageMultipleLimit,

        /// <summary>
        /// The vessel applicable for crew rejoin.
        /// </summary>
        [EnumValueData(Name = "Vessel Applicable For Crew Rejoin", KeyValue = "GLAS00000163")]
        VesselApplicableForCrewRejoin,

        /// <summary>
        /// The alternate tree template.
        /// </summary>
        [EnumValueData(Name = "Alternate Tree Template", KeyValue = "GLAS00000165")]
        AlternateTreeTemplate,

        /// <summary>
        /// The trigger work order maximum allowed days.
        /// </summary>
        [EnumValueData(Name = "Trigger Work Order Max Allowed Days", KeyValue = "GLAS00000167")]
        TriggerWorkOrderMaxAllowedDays,

        /// <summary>
        /// The env man electronic logbook start date
        /// </summary>
        [EnumValueData(Name = "Environmental Manager Electronic Logbook Start Date", KeyValue = "GLAS00000169")]
        EnvManElectronicLogbookStartDate,

        /// <summary>
        /// The environment sign off verification rank
        /// </summary>
        [EnumValueData(Name = "Environment Sign Off Verification Rank", KeyValue = "GLAS00000170")]
        EnvironmentSignOffVerificationRank,

        /// <summary>
        /// The env man orb part a logbook report path
        /// </summary>
        [EnumValueData(Name = "EnvMan ORB Part A Logbook Report Path", KeyValue = "GLAS00000171")]
        EnvManORBPartALogbookReportPath,

        /// <summary>
        /// The env man garbage logbook report path
        /// </summary>
        [EnumValueData(Name = "EnvMan Garbage Logbook Report Path", KeyValue = "GLAS00000172")]
        EnvManGarbageLogbookReportPath,

        /// <summary>
        /// The env man ods annexure ii logbook report path
        /// </summary>
        [EnumValueData(Name = "EnvMan ODS Annexure II Logbook Report Path", KeyValue = "GLAS00000179")]
        EnvManODSAnnexureIILogbookReportPath,

        /// <summary>
        /// The env man ods annexure iii logbook report path
        /// </summary>
        [EnumValueData(Name = "EnvMan ODS Annexure III Logbook Report Path", KeyValue = "GLAS00000180")]
        EnvManODSAnnexureIIILogbookReportPath,

        /// <summary>
        /// The environment sewage treatment plant
        /// </summary>
        [EnumValueData(Name = "Environment Sewage Treatment Plant", KeyValue = "GLAS00000181")]
        EnvironmentSewageTreatmentPlant,

        /// <summary>
        /// The env man sewage logbook report path
        /// </summary>
        [EnumValueData(Name = "EnvMan Sewage Logbook Report Path", KeyValue = "GLAS00000185")]
        EnvManSewageLogbookReportPath,

        /// <summary>
        /// The env man orb part b logbook report path
        /// </summary>
        [EnumValueData(Name = "EnvMan ORB Part B Logbook Report Path", KeyValue = "GLAS00000186")]
        EnvManORBPartBLogbookReportPath
    }
    /// <summary>
    /// HazOcc Report Status
    /// </summary>
    public enum HazOccReportStatus
    {
        /// <summary>
        /// Drafts
        /// </summary>
        [EnumValueData(Name = "Draft", KeyValue = "INCMAN000001")]
        DR = 1,

        /// <summary>
        /// In Review
        /// </summary>
        [EnumValueData(Name = "In Review", KeyValue = "INCMAN000003")]
        IR = 2,

        /// <summary>
        /// Actions In Progress
        /// </summary>
        [EnumValueData(Name = "Actions In Progress", KeyValue = "INCMAN000002")]
        AIP = 3,

        /// <summary>
        /// Actions Signed Off
        /// </summary>
        [EnumValueData(Name = "Actions Signed Off", KeyValue = "INCMAN000005")]
        ASO = 4,

        /// <summary>
        /// Closed
        /// </summary>
        [EnumValueData(Name = "Closed", KeyValue = "INCMAN000004")]
        Cl = 5
    }

    /// <summary>
    /// HazOccListStageFilter
    /// </summary>
    public enum HazOccListStageFilter
    {
        /// <summary>
        /// The passenger accidents
        /// </summary>
        [EnumValueData(Name = "Passenger Accidents", KeyValue = "PassengerAccidents")]
        PassengerAccidents,

        /// <summary>
        /// The crew accidents
        /// </summary>
        [EnumValueData(Name = "Crew Accidents", KeyValue = "CrewAccidents")]
        CrewAccidents,

        /// <summary>
        /// The incidents
        /// </summary>
        [EnumValueData(Name = "Incidents", KeyValue = "Incidents")]
        Incidents,

        /// <summary>
        /// The near miss safety observe
        /// </summary>
        [EnumValueData(Name = "Near Miss & Safety Observ.", KeyValue = "NearMissSafetyObserve")]
        NearMissSafetyObserve,

        /// <summary>
        /// The fatality
        /// </summary>
        [EnumValueData(Name = "Fatality", KeyValue = "Fatality")]
        Fatality,

        /// <summary>
        /// The very serious
        /// </summary>
        [EnumValueData(Name = "Very Serious", KeyValue = "VerySerious")]
        VerySerious,

        /// <summary>
        /// Serious Accidents
        /// </summary>
        [EnumValueData(Name = "Serious Accidents", KeyValue = "SeriousAccidents")]
        SeriousAccidents,

        /// <summary>
        /// Serious Incidents
        /// </summary>
        [EnumValueData(Name = "Serious Incidents", KeyValue = "SeriousIncidents")]
        SeriousIncidents,

        /// <summary>
        /// Unsafe Acts And Unsafe Codition
        /// </summary>
        [EnumValueData(Name = "Unsafe Acts And Unsafe Condition", KeyValue = "UAUCRate")]
        UAUCRate,

        /// <summary>
        /// The total
        /// </summary>
        [EnumValueData(Name = "Total Reports", KeyValue = "Total")]
        Total,

        /// <summary>
        /// The lti
        /// </summary>
        [EnumValueData(Name = "LTI", KeyValue = "LTI")]
        LTI,

        /// <summary>
        /// The TRC
        /// </summary>
        [EnumValueData(Name = "TRC", KeyValue = "TRC")]
        TRC,

        /// <summary>
        /// The third party accident
        /// </summary>
        [EnumValueData(Name = "Third Party Accidents", KeyValue = "ThirdPartyAccident")]
        ThirdPartyAccident,

        /// <summary>
        /// The illness
        /// </summary>
        [EnumValueData(Name = "Illness", KeyValue = "Illness")]
        Illness,
    }

    /// <summary>
    /// Enum for Hazocc main report types.
    /// </summary>
    public enum HazOccReportCodes
    {
        /// <summary>
        /// The accident.
        /// </summary>
        [EnumValueData(Name = "Accident", KeyValue = "INCMAN000021")]
        Accident = 1,

        /// <summary>
        /// The incident.
        /// </summary>
        [EnumValueData(Name = "Incident", KeyValue = "INCMAN000028")]
        Incident = 2,

        /// <summary>
        /// The near miss.
        /// </summary>
        [EnumValueData(Name = "Near Miss", KeyValue = "INCMAN000003")]
        NearMiss = 3,

        /// <summary>
        /// The observation.
        /// </summary>
        [EnumValueData(Name = "Observation", KeyValue = "INCMAN000035")]
        Observation = 4,

        /// <summary>
        /// The illness.
        /// </summary>
        [EnumValueData(Name = "Illness", KeyValue = "INCMAN000037")]
        Illness = 5
    }

    /// <summary>
    /// HazOcc Stat Anal Devision Codes
    /// </summary>
    public enum HazOccStatCodes
    {
        /// <summary>
        /// Maneuvering
        /// </summary>
        [EnumValueData(Name = "Maneuvering", KeyValue = "MANB")]
        MN = 1,

        /// <summary>
        /// Ship's Location
        /// </summary>
        [EnumValueData(Name = "ShipLocation", KeyValue = "SHLC")]
        SL = 2,

        /// <summary>
        /// Ahsore Locations
        /// </summary>
        [EnumValueData(Name = "AshoreLocation", KeyValue = "ASHL")]
        AL = 3,

        /// <summary>
        /// onBoard Locations
        /// </summary>
        [EnumValueData(Name = "OnboardLocation", KeyValue = "ONBL")]
        OL = 4,

        /// <summary>
        /// types of accident
        /// </summary>
        [EnumValueData(Name = "AccidentType", KeyValue = "TpcAcc")]
        TA = 5,

        /// <summary>
        /// place of accident
        /// </summary>
        [EnumValueData(Name = "AccidentPlace", KeyValue = "PLCINJ")]
        PA = 6,

        /// <summary>
        /// PAX Duty
        /// </summary>
        [EnumValueData(Name = "DutyStatus", KeyValue = "DUT")]
        DS = 7,

        /// <summary>
        /// Dept
        /// </summary>
        [EnumValueData(Name = "Department", KeyValue = "DEP")]
        DP = 8,

        /// <summary>
        /// Inj Type
        /// </summary>
        [EnumValueData(Name = "InjuryType", KeyValue = "TypInj")]
        IT = 9,

        /// <summary>
        /// Accident Cause
        /// </summary>
        [EnumValueData(Name = "AccidentCause", KeyValue = "CscAcc")]
        CA = 10,

        /// <summary>
        /// Ships Location - other
        /// </summary>
        [EnumValueData(Name = "ShipLocationOther", KeyValue = "INCMAN003236")]
        LO = 11,

        /// <summary>
        /// body Areas
        /// </summary>
        [EnumValueData(Name = "BodyAreas", KeyValue = "BDAR")]
        BA = 12,

        /// <summary>
        /// PPE
        /// </summary>
        [EnumValueData(Name = "PPE", KeyValue = "PPE")]
        PE = 13,

        /// <summary>
        /// Planning
        /// </summary>
        [EnumValueData(Name = "Planning", KeyValue = "PLPR")]
        PL = 14,

        /// <summary>
        /// Tools and EQuipment
        /// </summary>
        [EnumValueData(Name = "Tools", KeyValue = "VEEQ")]
        TL = 15,

        /// <summary>
        /// Jobs
        /// </summary>
        [EnumValueData(Name = "Jobs", KeyValue = "JFTS")]
        JB = 16,

        /// <summary>
        /// personal factors
        /// </summary>
        [EnumValueData(Name = "Factors", KeyValue = "PEFA")]
        FT = 17,

        /// <summary>
        /// Training and Supervision
        /// </summary>
        [EnumValueData(Name = "Training", KeyValue = "TRSK")]
        TR = 18,

        /// <summary>
        /// Passenger Ahsore Locations
        /// </summary>
        [EnumValueData(Name = "PAXAshoreLocation", KeyValue = "ASHLP")]
        PS = 19,

        /// <summary>
        /// Passenger onboard Locations
        /// </summary>
        [EnumValueData(Name = "PAXOnboardLocation", KeyValue = "ONBLP")]
        PB = 20,

        /// <summary>
        /// Ship's Location (PAX)
        /// </summary>
        [EnumValueData(Name = "ShipLocation", KeyValue = "SHLCP")]
        PSL = 21,

        /// <summary>
        /// Ships Location - other(PAX)
        /// </summary>
        [EnumValueData(Name = "ShipLocationOther", KeyValue = "INCMAN003263")]
        PLO = 22,

        /// <summary>
        /// Ships Ops
        /// </summary>
        [EnumValueData(Name = "ShipOperation", KeyValue = "SHOP")]
        SO = 23,

        /// <summary>
        /// Prim Equip
        /// </summary>
        [EnumValueData(Name = "PrimaryEquipment", KeyValue = "PED")]
        PD = 24,

        /// <summary>
        /// Pollution Category
        /// </summary>
        [EnumValueData(Name = "PollutionCategory", KeyValue = "POLUT")]
        PT = 25,

        /// <summary>
        /// Operation
        /// </summary>
        [EnumValueData(Name = "Operation", KeyValue = "NMOP")]
        NM = 26,

        /// <summary>
        /// Possible Consequences
        /// </summary>
        [EnumValueData(Name = "PossConsequences", KeyValue = "NMPC")]
        PC = 27,

        /// <summary>
        /// Acts
        /// </summary>
        [EnumValueData(Name = "Acts", KeyValue = "DCSA")]
        AT = 28,

        /// <summary>
        /// Conditions
        /// </summary>
        [EnumValueData(Name = "Conditions", KeyValue = "DASC")]
        CT = 29,

        /// <summary>
        /// Weather
        /// </summary>
        [EnumValueData(Name = "Weather", KeyValue = "ENWE")]
        WE = 30,

        /// <summary>
        /// Ships motion
        /// </summary>
        [EnumValueData(Name = "Motion", KeyValue = "ENSM")]
        MT = 31,

        /// <summary>
        /// Lighting and Noise
        /// </summary>
        [EnumValueData(Name = "Lighting", KeyValue = "ENLN")]
        LN = 32,

        /// <summary>
        /// Deck Surface
        /// </summary>
        [EnumValueData(Name = "DeckSurface", KeyValue = "ENDS")]
        DK = 33,

        /// <summary>
        /// Access
        /// </summary>
        [EnumValueData(Name = "Access", KeyValue = "ENAC")]
        CS = 34,

        /// <summary>
        /// ventilation
        /// </summary>
        [EnumValueData(Name = "Ventilation", KeyValue = "ENVE")]
        VT = 35,

        /// <summary>
        /// Substandard Acts
        /// </summary>
        [EnumValueData(Name = "Substandard Acts", KeyValue = "SBACT")]
        SSA = 36,

        /// <summary>
        /// Substandard Conditions
        /// </summary>
        [EnumValueData(Name = "Substandard Conditions", KeyValue = "SBCON")]
        SSC = 37,

        /// <summary>
        /// Human Factors
        /// </summary>
        [EnumValueData(Name = "Human Factors", KeyValue = "RCHUM")]
        RCH = 38,

        /// <summary>
        /// Job Factors
        /// </summary>
        [EnumValueData(Name = "Job Factors", KeyValue = "RCJOB")]
        RCJ = 39,

        /// <summary>
        /// Management Failure
        /// </summary>
        [EnumValueData(Name = "Management Failure", KeyValue = "RCMAN")]
        RCM = 40,

        /// <summary>
        /// HouseKeeping
        /// </summary>
        [EnumValueData(Name = "House Keeping", KeyValue = "HSKP")]
        HSK = 41,

        /// <summary>
        /// Pitch and Roll
        /// </summary>
        [EnumValueData(Name = "Pitch & Roll", KeyValue = "PTRL")]
        PTR = 42,

        /// <summary>
        /// Job Familiarity
        /// </summary>
        [EnumValueData(Name = "Familiarity", KeyValue = "JFFA")]
        JFF = 43,

        /// <summary>
        /// System Areas
        /// </summary>
        [EnumValueData(Name = "SystemAreas", KeyValue = "SYSA")]
        SYS = 44,

        /// <summary>
        /// Equipment Types
        /// </summary>
        [EnumValueData(Name = "EquipmentTypes", KeyValue = "NAVG")]
        EQP = 45,

        /// <summary>
        /// Action Types
        /// </summary>
        [EnumValueData(Name = "ActionTypes", KeyValue = "ACTT")]
        ACT = 46,

        /// <summary>
        /// Create hazOcc Quests
        /// </summary>
        [EnumValueData(Name = "HazOccCreateQst", KeyValue = "HAZQ")]
        HZQ = 47,

        /// <summary>
        /// The MMVT.
        /// </summary>
        [EnumValueData(Name = "VisitType", KeyValue = "MMVT")]
        MMVT = 48,

        /// <summary>
        /// The MMD.
        /// </summary>
        [EnumValueData(Name = "Limitation", KeyValue = "MMD1")]
        MMD = 49,

        /// <summary>
        /// The med ill.
        /// </summary>
        [EnumValueData(Name = "Medical Illness", KeyValue = "MedIll")]
        MedIll = 50,

        /// <summary>
        /// The ship ashore other location.
        /// </summary>
        [EnumValueData(Name = "ShipAshoreOtherLocation", KeyValue = "GLAS00003271")]
        SHOT = 51
    }

    /// <summary>
    /// HazOccAccidentClassifications
    /// </summary>
    public enum HazOccAccidentClassifications
    {

        /// <summary>
        /// The fatal
        /// </summary>
        [EnumValueData(Name = "FATAL", KeyValue = "FATAL")]
        FATAL,

        /// <summary>
        /// The LWC
        /// </summary>
        [EnumValueData(Name = "LWC", KeyValue = "LWC")]
        LWC,

        /// <summary>
        /// The RWC
        /// </summary>
        [EnumValueData(Name = "RWC", KeyValue = "RWC")]
        RWC,

        /// <summary>
        /// The MTC
        /// </summary>
        [EnumValueData(Name = "MTC", KeyValue = "MTC")]
        MTC,

        /// <summary>
        /// The fac
        /// </summary>
        [EnumValueData(Name = "FAC", KeyValue = "FAC")]
        FAC,

    }

    /// <summary>
    /// This enum is used for change location.
    /// </summary>
    public enum ChangeLocationEnum
    {
        /// <summary>
        /// The onboard
        /// </summary>
        [EnumValueData(Name = "Onboard", KeyValue = "Onboard")]
        Onboard,

        /// <summary>
        /// The ashore
        /// </summary>
        [EnumValueData(Name = "Ashore", KeyValue = "Ashore")]
        Ashore
    }

    /// <summary>
    /// HazOcc Statuses
    /// </summary>
    public enum HazOccReportLocationAbr
    {
        /// <summary>
        /// Ashore - At Port
        /// </summary>
        [EnumValueData(Name = "At Port", KeyValue = "INCMAN003205")]
        AP = 1,

        /// <summary>
        /// Ashore - At Yard
        /// </summary>
        [EnumValueData(Name = "At Yard", KeyValue = "INCMAN003206")]
        AY = 2,

        /// <summary>
        /// Ashore - Other
        /// </summary>
        [EnumValueData(Name = "Other", KeyValue = "INCMAN003207")]
        AO = 3,

        /// <summary>
        /// onBoard - At Sea
        /// </summary>
        [EnumValueData(Name = "At Sea", KeyValue = "INCMAN003247")]
        OS = 4,

        /// <summary>
        /// onBoard -At Port
        /// </summary>
        [EnumValueData(Name = "At Port", KeyValue = "INCMAN003209")]
        OP = 5,

        /// <summary>
        /// onBoard - river passage
        /// </summary>
        [EnumValueData(Name = "River Passage", KeyValue = "INCMAN003210")]
        OR = 6,

        /// <summary>
        /// onBoard - Canal Passage
        /// </summary>
        [EnumValueData(Name = "Canal Passage", KeyValue = "INCMAN003211")]
        OC = 7,

        /// <summary>
        /// onBoard - At Yard
        /// </summary>
        [EnumValueData(Name = "At Yard", KeyValue = "INCMAN003212")]
        OY = 8,

        /// <summary>
        /// onBoard - 500m of installation
        /// </summary>
        [EnumValueData(Name = "Within 500m", KeyValue = "INCMAN003213")]
        OI = 9,

        /// <summary>
        /// At Anchorage
        /// </summary>
        [EnumValueData(Name = "At Anchorage", KeyValue = "INCMAN003222")]
        OA = 10,

        /// <summary>
        /// onBoard - other
        /// </summary>
        [EnumValueData(Name = "Other", KeyValue = "INCMAN003214")]
        OO = 11,

        /// <summary>
        /// Ashore - At Port (PAX)
        /// </summary>
        [EnumValueData(Name = "At Port(PAX)", KeyValue = "INCMAN003245")]
        PAP = 12,

        /// <summary>
        /// Ashore - Other (PAX)
        /// </summary>
        [EnumValueData(Name = "Other(PAX)", KeyValue = "INCMAN003246")]
        PAO = 13,

        /// <summary>
        /// onBoard - At Sea(PAX)
        /// </summary>
        [EnumValueData(Name = "At Sea", KeyValue = "INCMAN003248")]
        POS = 14,

        /// <summary>
        /// onBoard -At Port(PAX)
        /// </summary>
        [EnumValueData(Name = "At Port", KeyValue = "INCMAN003249")]
        POP = 15,

        /// <summary>
        /// onBoard - river passage(PAX)
        /// </summary>
        [EnumValueData(Name = "River Passage", KeyValue = "INCMAN003250")]
        POR = 16,

        /// <summary>
        /// onBoard - Canal Passage(PAX)
        /// </summary>
        [EnumValueData(Name = "Canal Passage", KeyValue = "INCMAN003251")]
        POC = 17,

        /// <summary>
        /// At Anchorage(PAX)
        /// </summary>
        [EnumValueData(Name = "At Anchorage", KeyValue = "INCMAN003255")]
        POA = 18,

        /// <summary>
        /// onBoard - other(PAX)
        /// </summary>
        [EnumValueData(Name = "Other", KeyValue = "INCMAN003254")]
        POO = 19
    }


    /// <summary>
    /// This is FleetType
    /// </summary>
    public enum FleetType
    {
        /// <summary>
        /// The office
        /// </summary>
        [EnumValueData(Name = "Office", KeyValue = "O")]
        Office,
        /// <summary>
        /// The client
        /// </summary>
        [EnumValueData(Name = "Client", KeyValue = "C")]
        Client,

        /// <summary>
        /// The user
        /// </summary>
        [EnumValueData(Name = "User", KeyValue = "U")]
        User,
        /// <summary>
        /// The fleet
        /// </summary>
        [EnumValueData(Name = "Fleet", KeyValue = "F")]
        Fleet,

        /// <summary>
        /// The planning
        /// </summary>
        [EnumValueData(Name = "Planning", KeyValue = "P")]
        Planning,

        /// <summary>
        /// The group
        /// </summary>
        [EnumValueData(Name = "Group", KeyValue = "G")]
        Group,

        /// <summary>
        /// The payroll fleet
        /// </summary>
        [EnumValueData(Name = "Payroll Fleet", KeyValue = "R")]
        PayrollFleet,

        /// <summary>
        /// The account fleet
        /// </summary>
        [EnumValueData(Name = "Account Fleet", KeyValue = "A")]
        AccountFleet,

        /// <summary>
        /// The moto moco
        /// </summary>
        [EnumValueData(Name = "Moto Moco Fleet", KeyValue = "M")]
        MotoMoco
    }

    /// <summary>
    /// HazOcc Severity Status
    /// </summary>
    public enum HazOccSeverityStatus
    {
        // Referes to table - IncMan_Severity
        /// <summary>
        /// Serious
        /// </summary>
        [EnumValueData(KeyValue = "INCMAN000004", Name = "Serious")]
        Serious,

        /// <summary>
        /// Very Serious
        /// </summary>
        [EnumValueData(KeyValue = "INCMAN000002", Name = "Very Serious")]
        VerySerious
    }

    /// <summary>
    /// Navigation page key
    /// </summary>
    public enum NavigationPageKey
    {
        /// <summary>
        /// The inspection list page key
        /// </summary>
        [EnumValueData(KeyValue = "InspectionFilter", Name = "InspectionListPageKey")]
        InspectionListPageKey,

        /// <summary>
        /// The inspection finding page key
        /// </summary>
        [EnumValueData(KeyValue = "InspectionFindings", Name = "InspectionFindingPageKey")]
        InspectionFindingPageKey,

        /// <summary>
        /// The inspection action page key
        /// </summary>
        [EnumValueData(KeyValue = "InspectionAction", Name = "InspectionActionPageKey")]
        InspectionActionPageKey,

        /// <summary>
        /// The Defect List Page Key
        /// </summary>
        [EnumValueData(KeyValue = "DefectListFilter", Name = "DefectListPageKey")]
        DefectListPageKey,

        /// <summary>
        /// The Defect Details Page Key
        /// </summary>
        [EnumValueData(KeyValue = "DefectDetailsFilter", Name = "DefectDetailsPageKey")]
        DefectDetailsPageKey,

        /// <summary>
        /// The planned maintenance list page key
        /// </summary>
        [EnumValueData(KeyValue = "PlannedMaintenanceListFilter", Name = "PlannedMaintenanceListPageKey")]
        PlannedMaintenanceListPageKey,

        /// <summary>
        /// The planned maintenance details page key
        /// </summary>
        [EnumValueData(KeyValue = "PlannedMaintenanceDetailsFilter", Name = "PlannedMaintenanceDetailsPageKey")]
        PlannedMaintenanceDetailsPageKey,

        /// <summary>
        /// The crew list page key
        /// </summary>
        [EnumValueData(KeyValue = "CrewListFilter", Name = "CrewListPageKey")]
        CrewListPageKey,

        /// <summary>
        /// The medical sign off list page key
        /// </summary>
        [EnumValueData(KeyValue = "MedicalSignOffListFilter", Name = "MedicalSignOffListPageKey")]
        MedicalSignOffListPageKey,

        /// <summary>
        /// The crew details page key
        /// </summary>
        [EnumValueData(KeyValue = "CrewDetailsFilter", Name = "CrewDetailsPageKey")]
        CrewDetailsPageKey,

        /// <summary>
        /// The planned maintenance history list page key
        /// </summary>
        [EnumValueData(KeyValue = "MaintenanceHistoryListFilter", Name = "MaintenanceHistoryListPageKey")]
        MaintenanceHistoryListPageKey,

        /// <summary>
        /// The planned maintenance history details page key
        /// </summary>
        [EnumValueData(KeyValue = "MaintenanceHistoryDetailsFilter", Name = "MaintenanceHistoryDetailsPageKey")]
        MaintenanceHistoryDetailsPageKey,

        /// <summary>
        /// The haz occ list page key
        /// </summary>
        [EnumValueData(KeyValue = "HazOccListPage", Name = "HazOccListPageKey")]
        HazOccListPageKey,

        /// <summary>
        /// The haz occ details page key
        /// </summary>
        [EnumValueData(KeyValue = "HazOccDetailsPage", Name = "HazOccDetailsPageKey")]
        HazOccDetailsPageKey,

        /// <summary>
        /// The Voyage Reporting List Page Key
        /// </summary>
        [EnumValueData(KeyValue = "VoyageReportingListFilter", Name = "VoyageReportingListPageKey")]
        VoyageReportingListPageKey,

        /// <summary>
        /// The Sea passage event Page Key
        /// </summary>
        [EnumValueData(KeyValue = "SeaPassageEventFilter", Name = "SeaPassageEventPageKey")]
        SeaPassageEventPageKey,

        /// <summary>
        /// The port call event Page Key
        /// </summary>
        [EnumValueData(KeyValue = "PortCallEventFilter", Name = "PortCallEventPageKey")]
        PortCallEventPageKey,

        /// <summary>
        /// The cartificate list page key
        /// </summary>
        [EnumValueData(KeyValue = "CertificateListFilter", Name = "CertificateListPageKey")]
        CertificateListPageKey,

        /// <summary>
        /// The FinanceListPageKey
        /// </summary>
        [EnumValueData(KeyValue = "FinanceListFilter", Name = "FinanceListPageKey")]
        FinanceListPageKey,

        /// <summary>
        /// The FinanceGeneralLedgerPageKey
        /// </summary>
        [EnumValueData(KeyValue = "FinanceGeneralLedgerFilter", Name = "FinanceGeneralLedgerPageKey")]
        FinanceGeneralLedgerPageKey,

        /// <summary>
        /// The FinanceGeneralLedgerTransactionPageKey
        /// </summary>
        [EnumValueData(KeyValue = "FinanceGeneralLedgerTransactionFilter", Name = "FinanceGeneralLedgerTransactionPageKey")]
        FinanceGeneralLedgerTransactionPageKey,


        /// <summary>
        /// The finance transaction page key
        /// </summary>
        [EnumValueData(KeyValue = "FinanceTransactionFilter", Name = "FinanceTransactionPageKey")]
        FinanceTransactionPageKey,

        /// <summary>
        /// The procurement list page key
        /// </summary>
        [EnumValueData(KeyValue = "ProcurementListFilter", Name = "ProcurementListPageKey")]
        ProcurementListPageKey,

        /// <summary>
        /// The procurement details page key
        /// </summary>
        [EnumValueData(KeyValue = "ProcurementDetailFilter", Name = "ProcurementDetailPageKey")]
        ProcurementDetailPageKey,

        /// <summary>
        /// The procurement view quote page key
        /// </summary>
        [EnumValueData(KeyValue = "ProcurementViewQuoteFilter", Name = "ProcurementViewQuotePageKey")]
        ProcurementViewQuotePageKey,


        /// <summary>
        /// The finance list stack key
        /// </summary>
        [EnumValueData(KeyValue = "FinanceStackKey", Name = "FinanceStackKey")]
        FinanceStackKey,

        /// <summary>
        /// The dashboard full map page key
        /// </summary>
        [EnumValueData(KeyValue = "DashboardFullMapPageFilter", Name = "DashboardFullMapPageKey")]
        DashboardFullMapPageKey,

        /// <summary>
        /// The notification page key
        /// </summary>
        [EnumValueData(KeyValue = "NotificationPageFilter", Name = "NotificationPageKey")]
        NotificationPageKey,

        /// <summary>
        /// The notification mobile chat detail key
        /// </summary>
        [EnumValueData(KeyValue = "NotificationMobileChatDetailFilter", Name = "NotificationMobileChatDetailKey")]
        NotificationMobileChatDetailKey,

        /// <summary>
        /// The notification mobile information key
        /// </summary>
        [EnumValueData(KeyValue = "NotificationMobileInfoFilter", Name = "NotificationMobileInfoKey")]
        NotificationMobileInfoKey,

        /// <summary>
        /// The notification mobile discussion key
        /// </summary>
        [EnumValueData(KeyValue = "NotificationMobileDiscussionFilter", Name = "NotificationMobileDiscussionKey")]
        NotificationMobileDiscussionKey,

        /// <summary>
        /// The certificate details key
        /// </summary>
        [EnumValueData(KeyValue = "CertificateDetailsFilter", Name = "CertificateDetailsPageKey")]
        CertificateDetailsPageKey,

        /// <summary>
        /// The notification chat page key
        /// </summary>
        [EnumValueData(KeyValue = "NotificationChatFilter", Name = "NotificationChatPageKey")]
        NotificationChatPageKey,

        /// <summary>
        /// The jsa list page key
        /// </summary>
        [EnumValueData(KeyValue = "JSAListPageKey", Name = "JSAListPageKey")]
        JSAListPageKey,

        /// <summary>
        /// The jsa list page key
        /// </summary>
        [EnumValueData(KeyValue = "JSADetailsFilter", Name = "JSADetailsPageKey")]
        JSADetailsPageKey,

		/// <summary>
		/// The approval list page key
		/// </summary>
		[EnumValueData(KeyValue = "ApprovalListFilter", Name = "ApprovalListPageKey")]
        ApprovalListPageKey,
    }

    /// <summary>
    /// The Berth types.
    /// </summary>
    public enum BerthType
    {
        /// <summary>
        /// The budgeted berth
        /// </summary>
        [EnumValueData(Name = "Budgeted Berth", KeyValue = "BG")]
        BudgetedBerth,

        /// <summary>
        /// The extra berth tech
        /// </summary>
        [EnumValueData(Name = "Extra Berth Tech", KeyValue = "ET")]
        ExtraBerthTech,

        /// <summary>
        /// The extra berth owner
        /// </summary>
        [EnumValueData(Name = "Extra Berth Owner", KeyValue = "EO")]
        ExtraBerthOwner,

        /// <summary>
        /// The training berth
        /// </summary>
        [EnumValueData(Name = "Training Berth", KeyValue = "TG")]
        TrainingBerth,

        /// <summary>
        /// The expired berth
        /// </summary>
        [EnumValueData(Name = "Expired Berth", KeyValue = "EX")]
        ExpiredBerth,

        /// <summary>
        /// The overlap berth
        /// </summary>
        [EnumValueData(Name = "Overlap Berth", KeyValue = "OV")]
        OverlapBerth,
    }

    /// <summary>
    /// Job type enum.
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// The adjust.
        /// </summary>
        [EnumValueData(Name = "Adjust", KeyValue = "GLAS00000002")]
        Adjust,

        /// <summary>
        /// The analysis.
        /// </summary>
        [EnumValueData(Name = "Analysis", KeyValue = "GLAS00000001")]
        Analysis,

        /// <summary>
        /// The check.
        /// </summary>
        [EnumValueData(Name = "Check", KeyValue = "SYST00000009")]
        Check,

        /// <summary>
        /// The class code only.
        /// </summary>
        [EnumValueData(Name = "Class Code Only", KeyValue = "CLASCODE0001")]
        ClassCodeOnly,

        /// <summary>
        /// The condition monitoring task.
        /// </summary>
        [EnumValueData(Name = "Condition Monitoring Task", KeyValue = "SYST00000038")]
        ConditionMonitoringTask,

        /// <summary>
        /// The defect.
        /// </summary>
        [EnumValueData(Name = "Defect", KeyValue = "SYST00000039")]
        Defect,

        /// <summary>
        /// The drydock.
        /// </summary>
        [EnumValueData(Name = "Drydock", KeyValue = "SYST00000010")]
        Drydock,

        /// <summary>
        /// The electrical.
        /// </summary>
        [EnumValueData(Name = "Electrical", KeyValue = "SYST00000011")]
        Electrical,

        /// <summary>
        /// The inspection.
        /// </summary>
        [EnumValueData(Name = "Inspection", KeyValue = "SYST00000015")]
        Inspection,

        /// <summary>
        /// The lubricate.
        /// </summary>
        [EnumValueData(Name = "Lubricate", KeyValue = "SYST00000018")]
        Lubricate,

        /// <summary>
        /// The measurements.
        /// </summary>
        [EnumValueData(Name = "Measurements", KeyValue = "SYST00000037")]
        Measurements,

        /// <summary>
        /// The megger test.
        /// </summary>
        [EnumValueData(Name = "Megger Test", KeyValue = "SYST00000019")]
        MeggerTest,

        /// <summary>
        /// The oil change.
        /// </summary>
        [EnumValueData(Name = "Oil Change", KeyValue = "SYST00000035")]
        OilChange,

        /// <summary>
        /// The overhaul.
        /// </summary>
        [EnumValueData(Name = "Overhaul", KeyValue = "SYST00000021")]
        Overhaul,

        /// <summary>
        /// The paint.
        /// </summary>
        [EnumValueData(Name = "Paint", KeyValue = "SYST00000022")]
        Paint,

        /// <summary>
        /// The renew.
        /// </summary>
        [EnumValueData(Name = "Renew", KeyValue = "GLAS00000006")]
        Renew,

        /// <summary>
        /// The repair.
        /// </summary>
        [EnumValueData(Name = "Repair", KeyValue = "GLAS00000007")]
        Repair,

        /// <summary>
        /// The service.
        /// </summary>
        [EnumValueData(Name = "Service", KeyValue = "GLAS00000004")]
        Service,

        /// <summary>
        /// The survey.
        /// </summary>
        [EnumValueData(Name = "Survey", KeyValue = "SYST00000024")]
        Survey,

        /// <summary>
        /// The test.
        /// </summary>
        [EnumValueData(Name = "Test", KeyValue = "GLAS00000003")]
        Test,

        /// <summary>
        /// The tightening.
        /// </summary>
        [EnumValueData(Name = "Tightening", KeyValue = "SYST00000034")]
        Tightening,

        /// <summary>
        /// The washing cleaning.
        /// </summary>
        [EnumValueData(Name = "Washing/Cleaning", KeyValue = "SYST00000036")]
        WashingCleaning,

        /// <summary>
        /// The winter works.
        /// </summary>
        [EnumValueData(Name = "Winter Works", KeyValue = "GLAS00000008")]
        WinterWorks,

        /// <summary>
        /// The certificate.
        /// </summary>
        [EnumValueData(Name = "Certificate", KeyValue = "SYST00000040")]
        Certificate
    }

    /// <summary>
    /// Enum for file format types
    /// </summary>
    public enum FileFormatTypes
    {
        /// <summary>
        /// The format type excel
        /// </summary>
        [EnumValueData(Name = "Excel", KeyValue = "Excel(*.xls)|*.xls")]
        FormatTypeExcel = 1,

        /// <summary>
        /// The format type ms word
        /// </summary>
        [EnumValueData(Name = "MS-Word", KeyValue = "Document(*.doc)|*.doc")]
        FormatTypeMSWord = 2,

        /// <summary>
        /// The format PDF
        /// </summary>
        [EnumValueData(Name = "PDF", KeyValue = "PDF(*.pdf)|*.pdf")]
        FormatPDF = 3,
    }

    /// <summary>
    /// Enum for FormatType List
    /// </summary>
    public enum FormatTypes
    {
        /// <summary>
        /// The format type BGI
        /// </summary>
        [EnumValueData(Name = "BGI", KeyValue = "D33FE07F-69F2-414A-A05A-9097CCCEA555")]
        FormatTypeBGI = 1,

        /// <summary>
        /// The format type atlas
        /// </summary>
        [EnumValueData(Name = "Atlas", KeyValue = "Atlas")]
        FormatTypeAtlas = 2,

        /// <summary>
        /// The format type ITM
        /// </summary>
        [EnumValueData(Name = "ITM", KeyValue = "E708880E-9F71-478B-B0A2-319B99118223")]
        FormatTypeITM = 3,

        /// <summary>
        /// The format type VShips
        /// </summary>
        [EnumValueData(Name = "VShips", KeyValue = "219D903B-6A4F-4CA0-82B2-E5713E49A968")]
        FormatTypeVShips = 4,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HazOccEvalCodes
    {
        /// <summary>
        /// Action
        /// </summary>
        [EnumValueData(Name = "Action", KeyValue = "ACT")]
        AC = 1,

        /// <summary>
        /// Evaluation
        /// </summary>
        [EnumValueData(Name = "Evaluation", KeyValue = "EVA")]
        EV = 2,

        /// <summary>
        /// Correction
        /// </summary>
        [EnumValueData(Name = "COR", KeyValue = "INCMAN003717")]
        CR = 3,

        /// <summary>
        /// Corrective Action
        /// </summary>
        [EnumValueData(Name = "CRA", KeyValue = "INCMAN003718")]
        CA = 4,

        /// <summary>
        /// Send for Review
        /// </summary>
        [EnumValueData(Name = "SDR", KeyValue = "INCMAN003719")]
        SR = 5
    }

    /// <summary>
    /// Mapping of Hierarchy Explorer
    /// </summary>
    public enum HierarchyExplorerMappingSource
    {
        /// <summary>
        /// The haz ocs
        /// </summary>
        [EnumValueData(Name = "HazOcs", KeyValue = "GLAS00000020")]
        HazOcs,

        /// <summary>
        /// The inspection
        /// </summary>
        [EnumValueData(Name = "Inspection", KeyValue = "GLAS00000021")]
        Inspection,

        /// <summary>
        /// The job safety analysis
        /// </summary>
        [EnumValueData(Name = "Job Safety Analysis", KeyValue = "GLAS00000022")]
        JobSafetyAnalysis,

        /// <summary>
        /// The management of change.
        /// </summary>
        [EnumValueData(Name = "Management of Change", KeyValue = "GLAS00000158")]
        ManagementOfChange,

        /// <summary>
        /// The environmental management
        /// </summary>
        [EnumValueData(Name = "Environmental Management", KeyValue = "GLAS00000166")]
        EnvironmentalManagement
    }
    /// <summary>
    /// Inspection Findings Report Type
    /// </summary>
    public enum InspectionFindingsReportType
    {
        /// <summary>
        /// The summary
        /// </summary>
        [EnumValueData(Name = "Summary", KeyValue = "Summary")]
        Summary,

        /// <summary>
        /// The detail
        /// </summary>
        [EnumValueData(Name = "Detail", KeyValue = "Detail")]
        Detail,

        /// <summary>
        /// The qa detail
        /// </summary>
        [EnumValueData(Name = "QaDetail", KeyValue = "QaDetail")]
        QaDetail,
    }

    /// <summary>
    /// Break In Passage type
    /// </summary>
    public enum BreakInPassageType
    {
        /// <summary>
        /// The break in passage
        /// </summary>
        [EnumValueData(Name = "Stoppage", KeyValue = "GLAS00000022")]
        BreakInPassage,
        /// <summary>
        /// The slow steam event
        /// </summary>
        [EnumValueData(Name = "Slow Steam", KeyValue = "GLAS00000023")]
        SlowSteamEvent,
        /// <summary>
        /// The delay deviation
        /// </summary>
        [EnumValueData(Name = "Deviation Delay", KeyValue = "GLAS00000024")]
        DelayDeviation
    }

    /// <summary>
    /// Module Area
    /// </summary>
    public enum ModuleArea
    {
        /// <summary>
        /// The commercial
        /// </summary>
        [EnumValueData(Name = "Commercial", KeyValue = "Commercial")]
        Commercial,
        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "HazOcc")]
        HazOcc,
        /// <summary>
        /// The crewing
        /// </summary>
        [EnumValueData(Name = "Crewing", KeyValue = "Crewing")]
        Crewing,
        /// <summary>
        /// The procurement
        /// </summary>
        [EnumValueData(Name = "Procurement", KeyValue = "Procurement")]
        Procurement,
        /// <summary>
        /// The environment
        /// </summary>
        [EnumValueData(Name = "Environment", KeyValue = "Environment")]
        Environment,
        /// <summary>
        /// The financials
        /// </summary>
        [EnumValueData(Name = "Financials", KeyValue = "Financials")]
        Financials,
        /// <summary>
        /// The certificates
        /// </summary>
        [EnumValueData(Name = "Certificates", KeyValue = "Certificates")]
        Certificates,
        /// <summary>
        /// The defects
        /// </summary>
        [EnumValueData(Name = "Defects", KeyValue = "Defects")]
        Defects,
        /// <summary>
        /// The PMS
        /// </summary>
        [EnumValueData(Name = "PMS", KeyValue = "PMS")]
        PMS,
        /// <summary>
        /// The inspections
        /// </summary>
        [EnumValueData(Name = "Inspections & Ratings", KeyValue = "Inspections")]
        Inspections,

    }

    /// <summary>
    /// UserPreferences
    /// </summary>
    /// refers to ClientPortalUserPreference in shipsure DB
    /// Name refers to PreferenceKey and KevValue refers to PreferenceId
    public enum UserPreferences
    {

        /// <summary>
        /// The commercial
        /// </summary>
        [EnumValueData(Name = "Commercial", KeyValue = "GLAS00000001")]
        Commercial,
        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "GLAS00000002")]
        HazOcc,
        /// <summary>
        /// The crewing
        /// </summary>
        [EnumValueData(Name = "Crewing", KeyValue = "GLAS00000003")]
        Crewing,
        /// <summary>
        /// The procurement
        /// </summary>
        [EnumValueData(Name = "Procurement", KeyValue = "GLAS00000009")]
        Procurement,
        /// <summary>
        /// The environment
        /// </summary>
        [EnumValueData(Name = "Environment", KeyValue = "GLAS00000004")]
        Environment,
        /// <summary>
        /// The financials
        /// </summary>
        [EnumValueData(Name = "Financials", KeyValue = "GLAS00000005")]
        Financials,
        /// <summary>
        /// The certificates
        /// </summary>
        [EnumValueData(Name = "Certificates", KeyValue = "GLAS00000006")]
        Certificates,
        /// <summary>
        /// The defects
        /// </summary>
        [EnumValueData(Name = "Defects", KeyValue = "GLAS00000007")]
        Defects,
        /// <summary>
        /// The PMS
        /// </summary>
        [EnumValueData(Name = "PMS", KeyValue = "GLAS00000008")]
        PMS,
        /// <summary>
        /// The inspections
        /// </summary>
        [EnumValueData(Name = "InspectionsAndRatings", KeyValue = "GLAS00000010")]
        Inspections,

    }

    /// <summary>
    /// Modules
    /// </summary>
    public enum Modules
    {
        /// <summary>
        /// The crewing
        /// </summary>
        [EnumValueData(Name = "Crewing", KeyValue = "Crewing")]
        Crewing,

        /// <summary>
        /// The PMS
        /// </summary>
        [EnumValueData(Name = "PMS", KeyValue = "PMS")]
        PMS,
    }

    //update in constants.js for MessageCateory_[Category]
    /// <summary>
    /// Message Category Enum
    /// </summary>
    public enum MessageCategoryEnum
    {
        /// <summary>
        /// The general
        /// </summary>
        [EnumValueData(Name = "General", KeyValue = "1")]
        General,

        /// <summary>
        /// The po approval
        /// </summary>
        [EnumValueData(Name = "Purchase Order", KeyValue = "2")]
        PurchaseOrder,

        /// <summary>
        /// The inspection
        /// </summary>
        [EnumValueData(Name = "Inspection", KeyValue = "3")]
        Inspection,

        /// <summary>
        /// The inspection finding
        /// </summary>
        [EnumValueData(Name = "Inspection Finding", KeyValue = "4")]
        InspectionFinding,

        /// <summary>
        /// The certificate
        /// </summary>
        [EnumValueData(Name = "Certificate", KeyValue = "5")]
        Certificate,

        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "6")]
        HazOcc,

        /// <summary>
        /// The sea passage
        /// </summary>
        [EnumValueData(Name = "Sea Passage", KeyValue = "7")]
        SeaPassage,

        /// <summary>
        /// The port call location event
        /// </summary>
        [EnumValueData(Name = "Port Call/ Location Event", KeyValue = "8")]
        PortCallLocationEvent,

        /// <summary>
        /// The Crew
        /// </summary>
        [EnumValueData(Name = "Crew", KeyValue = "9")]
        Crew,

        /// <summary>
        /// The Crew
        /// </summary>
        [EnumValueData(Name = "Defect Work Order", KeyValue = "10")]
        DefectWorkOrder,

        /// <summary>
        /// The Crew
        /// </summary>
        [EnumValueData(Name = "Planned Maintenance", KeyValue = "11")]
        PlannedMaintenance,

        /// <summary>
        /// The Crew
        /// </summary>
        [EnumValueData(Name = "JSA", KeyValue = "12")]
        JSA,
    }

    /// <summary>
    /// Enum for Risk Assesment
    /// </summary>
    public enum RiskAssessment
    {
        /// <summary>
        /// The total
        /// </summary>
        [EnumValueData(Name = "Total", KeyValue = "Total")]
        Total,

        /// <summary>
        /// The medium or higher
        /// </summary>
        [EnumValueData(Name = "MediumOrHigher", KeyValue = "MediumOrHigher")]
        MediumOrHigher,

        /// <summary>
        /// Medium Or Higher No Control
        /// </summary>
        [EnumValueData(Name = "MediumOrHigherNoControl", KeyValue = "MediumOrHigherNoControl")]
        MediumOrHigherNoControl,

        /// <summary>
        /// The average
        /// </summary>
        [EnumValueData(Name = "Average", KeyValue = "Average")]
        Average,

        /// <summary>
        /// The overdue
        /// </summary>
        [EnumValueData(Name = "Overdue", KeyValue = "Overdue")]
        Overdue,

        /// <summary>
        /// The void
        /// </summary>
        [EnumValueData(Name = "Void", KeyValue = "Void")]
        Void,

        /// <summary>
        /// The Updated In Last 30 Days
        /// </summary>
        [EnumValueData(Name = "UpdatedInLast30Days", KeyValue = "UpdatedInLast30Days")]
        UpdatedInLast30Days
    }

    /// <summary>
    /// JSA Status
    /// </summary>
    public enum JSAStatus
    {
        /// <summary>
        /// The planned
        /// </summary>
        [EnumValueData(Name = "Planned", KeyValue = "GLAS00000018")]
        Planned,

        /// <summary>
        /// The approval pending
        /// </summary>
        [EnumValueData(Name = "Approval Pending", KeyValue = "GLAS00000019")]
        ApprovalPending,

        /// <summary>
        /// The complete
        /// </summary>
        [EnumValueData(Name = "Completed", KeyValue = "GLAS00000022")]
        Completed,

        /// <summary>
        /// The cancel
        /// </summary>
        [EnumValueData(Name = "Cancelled", KeyValue = "GLAS00000023")]
        Cancelled,

        /// <summary>
        /// The reject
        /// </summary>
        [EnumValueData(Name = "Rejected", KeyValue = "GLAS00000024")]
        Rejected,

        /// <summary>
        /// The approved
        /// </summary>
        [EnumValueData(Name = "Approved", KeyValue = "GLAS00000025")]
        Approved,

        /// <summary>
        /// The office approval pending
        /// </summary>
        [EnumValueData(Name = "Office Approval Pending", KeyValue = "GLAS00000026")]
        OfficeApprovalPending,

        /// <summary>
        /// The reopen
        /// </summary>
        [EnumValueData(Name = "Reopened", KeyValue = "GLAS00000027")]
        Reopened,

        /// <summary>
        /// The deleted
        /// </summary>
        [EnumValueData(Name = "Deleted", KeyValue = "GLAS00000028")]
        Deleted
    }

    /// <summary>
    /// 
    /// </summary>
    public enum JSAStage
    {

        /// <summary>
        /// The total
        /// </summary>
        [EnumValueData(Name = "Open", KeyValue = "Total")]
        Total,

        /// <summary>
        /// The low
        /// </summary>
        [EnumValueData(Name = "Low", KeyValue = "Low")]
        Low,

        /// <summary>
        /// The mid high
        /// </summary>
        [EnumValueData(Name = "Mid & High", KeyValue = "MidHigh")]
        MidHigh,

        /// <summary>
        /// The completed
        /// </summary>
        [EnumValueData(Name = "Completed", KeyValue = "Completed")]
        Completed,

        /// <summary>
        /// The overdue for closure
        /// </summary>
        [EnumValueData(Name = "Overdue For Closure", KeyValue = "OverdueForClosure")]
        OverdueForClosure,

        /// <summary>
        /// The pending office approval
        /// </summary>
        [EnumValueData(Name = "Pending Office Approval", KeyValue = "PendingOfficeApproval")]
        PendingOfficeApproval,

        /// <summary>
        /// The high risk
        /// </summary>
        [EnumValueData(Name = "High Risk", KeyValue = "HighRisk")]
        HighRisk,
    }

    /// <summary>
    /// JSA Attribute Lookup Type
    /// </summary>
    public enum JSAAttributeLookupType
    {
        /// <summary>
        /// The safety precautions
        /// </summary>
        [EnumValueData(Name = "SafetyPrecaution", KeyValue = "SafetyPrecaution")]
        SafetyPrecaution,

        /// <summary>
        /// The permit type
        /// </summary>
        [EnumValueData(Name = "PermitType", KeyValue = "PermitType")]
        PermitType,

        /// <summary>
        /// The status
        /// </summary>
        [EnumValueData(Name = "Status", KeyValue = "Status")]
        Status,

        /// <summary>
        /// The osa status
        /// </summary>
        [EnumValueData(Name = "OsaStatus", KeyValue = "OsaStatus")]
        OsaStatus,

        /// <summary>
        /// The communication protocol.
        /// </summary>
        [EnumValueData(Name = "CommunicationProtocol", KeyValue = "CommunicationProtocol")]
        CommunicationProtocol,

        /// <summary>
        /// The crew meeting guidelines.
        /// </summary>
        [EnumValueData(Name = "CrewMeetingGuidelines", KeyValue = "CrewMeetingGuidelines")]
        CrewMeetingGuidelines
    }

    /// <summary>
    /// Pos Attribute Lookup Code
    /// </summary>
    public enum PosAttributeLookupCode
    {
        /// <summary>
        /// The fuel type
        /// </summary>
        [EnumValueData(Name = "FuelType", KeyValue = "FuelType")]
        FuelType,

        /// <summary>
        /// The fresh water type
        /// </summary>
        [EnumValueData(Name = "FreshWaterType", KeyValue = "FreshWaterType")]
        FreshWaterType,

        /// <summary>
        /// The lub oil type
        /// </summary>
        [EnumValueData(Name = "LubOilType", KeyValue = "LubOilType")]
        LubOilType,

        /// <summary>
        /// The waste type
        /// </summary>
        [EnumValueData(Name = "WasteType", KeyValue = "WasteType")]
        WasteType,

        /// <summary>
        /// Waste ROB Break Down
        /// </summary>
        [EnumValueData(Name = "WasteROBBreakDown", KeyValue = "WasteROBBreakDown")]
        WasteROBBreakDown,

        /// <summary>
        /// Fresh Water Rob Break Down
        /// </summary>
        [EnumValueData(Name = "FreshWaterROBBreakDown", KeyValue = "FreshWaterROBBreakDown")]
        FreshWaterROBBreakDown,

        /// <summary>
        /// Lub oil Break Rob Down 
        /// </summary>
        [EnumValueData(Name = "LubOilROBBreakDown", KeyValue = "LubOilROBBreakDown")]
        LubOilROBBreakDown,

        /// <summary>
        /// Tank Capacity
        /// </summary>
        [EnumValueData(Name = "TankCapacity", KeyValue = "TankCapacity")]
        TankCapacity,

        /// <summary>
        /// Previous Rob
        /// </summary>
        [EnumValueData(Name = "PreviousROB", KeyValue = "PreviousROB")]
        PreviousROB,

        /// <summary>
        /// The ballast water type
        /// </summary>
        [EnumValueData(Name = "BallastWaterType", KeyValue = "BallastWaterType")]
        BallastWaterType,

        /// <summary>
        /// The ballast break down
        /// </summary>
        [EnumValueData(Name = "BallastBreakDown", KeyValue = "BallastBreakDown")]
        BallastBreakDown,

        /// <summary>
        /// The previous events data
        /// </summary>
        [EnumValueData(Name = "PreviousEventsData", KeyValue = "PreviousEventsData")]
        PreviousEventsData,

        /// <summary>
        /// The break in passage type
        /// </summary>
        [EnumValueData(Name = "BreakInPassageType", KeyValue = "BreakInPassageType")]
        BreakInPassageType,

        /// <summary>
        /// The consumption category
        /// </summary>
        [EnumValueData(Name = "ConsumptionCategory", KeyValue = "ConsumptionCategory")]
        ConsumptionCategory,

        /// <summary>
        /// The noon synopsis type
        /// </summary>
        [EnumValueData(Name = "NoonSynopsisType", KeyValue = "NoonSynopsisType")]
        NoonSynopsisType,

        /// <summary>
        /// The significant current
        /// </summary>
        [EnumValueData(Name = "SignificantCurrent", KeyValue = "SignificantCurrent")]
        SignificantCurrent,

        /// <summary>
        /// The off hire applicable
        /// </summary>
        [EnumValueData(Name = "OffHireApplicable", KeyValue = "OffHireApplicable")]
        OffHireApplicable,

        /// <summary>
        /// The port activity category
        /// </summary>
        [EnumValueData(Name = "PortActivityCategory", KeyValue = "PortActivityCategory")]
        PortActivityCategory,

        /// <summary>
        /// The port planning status
        /// </summary>
        [EnumValueData(Name = "PortPlanningStatus", KeyValue = "PortPlanningStatus")]
        PortPlanningStatus,

        /// <summary>
        /// The UTC mapping.
        /// </summary>
        [EnumValueData(Name = "UTCMapping", KeyValue = "UTCMapping")]
        UTCMapping,

        /// <summary>
        /// The UTC mapping.
        /// </summary>
        [EnumValueData(Name = "RouteChangeReason", KeyValue = "RouteChangeReason")]
        RouteChangeReason,

        /// <summary>
        /// The specifics
        /// </summary>
        [EnumValueData(Name = "Specifics", KeyValue = "Specifics")]
        Specifics,

        /// <summary>
        /// The reason for difference
        /// </summary>
        [EnumValueData(Name = "ReasonForDifference", KeyValue = "ReasonForDifference")]
        ReasonForDifference,

        /// <summary>
        /// The charter order type
        /// </summary>
        [EnumValueData(Name = "CharterOrderType", KeyValue = "CharterOrderType")]
        CharterOrderType,

        /// <summary>
        /// The master order reason
        /// </summary>
        [EnumValueData(Name = "MasterOrderReason", KeyValue = "MasterOrderReason")]
        MasterOrderReason,
    }

    /// <summary>
    /// Approval Header Nodes
    /// </summary>
    public enum ApprovalHeaderNodes
	{
		/// <summary>
		/// The purchase order
		/// </summary>
		[EnumValueData(Name = "Purchase Order", KeyValue = "PurchaseOrder")]
        PurchaseOrder,

		/// <summary>
		/// The jsa
		/// </summary>
		[EnumValueData(Name = "JSA", KeyValue = "JSA")]
        JSA,

		/// <summary>
		/// The defect
		/// </summary>
		[EnumValueData(Name = "Defect", KeyValue = "Defect")]
        Defect,

		/// <summary>
		/// The PMS
		/// </summary>
		[EnumValueData(Name = "PMS", KeyValue = "PMS")]
        PMS,

        /// <summary>
        /// Inspections & Audits
        /// </summary>
        [EnumValueData(Name = "Inspections & Audits", KeyValue = "InspectionsAndAudits")]
        InspectionsAndAudits
    }

    /// <summary>
    /// Approval Purchase Order Nodes
    /// </summary>
    public enum ApprovalPurchaseOrderNodes
    {
		/// <summary>
		/// The pending approval
		/// </summary>
		[EnumValueData(Name = "Awaiting Snr. / Client Auth.", KeyValue = "AwaitingSnrOrClientAuth")]
        PendingApproval,

		/// <summary>
		/// The tender awaiting authorization
		/// </summary>
		[EnumValueData(Name = "Tender Awaiting Authorization", KeyValue = "TenderAwaitingAuthorization")]
        TenderAwaitingAuthorization,
    }

    /// <summary>
    /// Approval JSA Nodes 
    /// </summary>
    public enum ApprovalJSANodes
    {
        /// <summary>
        /// The pending approval
        /// </summary>
        [EnumValueData(Name = "Pending Approval", KeyValue = "PendingApproval")]
        PendingApproval,

    }

    /// <summary>
    /// Approval Defect Nodes
    /// </summary>
    public enum ApprovalDefectNodes
    {
        /// <summary>
        /// The pending closure
        /// </summary>
        [EnumValueData(Name = "Pending Closure", KeyValue = "PendingClosure")]
        PendingClosure,
    }

    /// <summary>
    /// Approval Inspection Audit Nodes
    /// </summary>
    public enum ApprovalInspectionAuditNodes
    {
        /// <summary>
        /// The Inspections pending closure
        /// </summary>
        [EnumValueData(Name = "Inspections Pending Closure", KeyValue = "InspectionPendingClosure")]
        InspectionPendingClosure,

        /// <summary>
        /// The Audits Pending Closure
        /// </summary>
        [EnumValueData(Name = "Audits Pending Closure", KeyValue = "AuditPendingClosure")]
        AuditPendingClosure,

    }

    /// <summary>
    /// Apprioval PMS Nodes
    /// </summary>
    public enum ApprovalPMSNodes
    {
        /// <summary>
        /// The pending closure
        /// </summary>
        [EnumValueData(Name = "Pending Closure", KeyValue = "PendingClosure")]
        PendingClosure,

        /// <summary>
        /// The pending reschedule request
        /// </summary>
        [EnumValueData(Name = "Pending Reschedule Requests", KeyValue = "PendingRescheduleRequests")]
        PendingRescheduleRequests
    }

    /// <summary>
    /// Enum for Voyage Attribute Lookup Code
    /// </summary>
    public enum VoyageAttributeLookupCode
    {
        /// <summary>
        /// The noon comment category
        /// </summary>
        [EnumValueData(Name = "NoonCommentCategory", KeyValue = "NoonCommentCategory")]
        NoonCommentCategory,
    }

    //Need to delete this at the time of development
    /// <summary>
    /// Inspection Type enumeration.
    /// </summary>
    public enum InspectionTypes
    {
        /// <summary>
        /// The dry dock report
        /// </summary>
        [EnumValueData(Name = "DryDock Report", KeyValue = "08")]
        DryDockReport = 0,

        /// <summary>
        /// The public health inspection external
        /// </summary>
        [EnumValueData(Name = "Public Health Inspection External", KeyValue = "35")]
        PublicHealthInspectionExternal,

        /// <summary>
        /// The external ism audit
        /// </summary>
        [EnumValueData(Name = "External ISM Audit", KeyValue = "16")]
        ExternalISMAudit,

        /// <summary>
        /// The external is o14001 audit
        /// </summary>
        [EnumValueData(Name = "External ISO 14001 Audit", KeyValue = "39")]
        ExternalISO14001Audit,

        /// <summary>
        /// The external is o9001 audit
        /// </summary>
        [EnumValueData(Name = "External ISO 9001 Audit", KeyValue = "38")]
        ExternalISO9001Audit,

        /// <summary>
        /// The external isps audit
        /// </summary>
        [EnumValueData(Name = "External ISPS Audit", KeyValue = "20")]
        ExternalISPSAudit,

        /// <summary>
        /// The external ohsa S18001 audit
        /// </summary>
        [EnumValueData(Name = "External OHSAS 18001 Audit", KeyValue = "64")]
        ExternalOHSAS18001Audit,

        /// <summary>
        /// The flag state
        /// </summary>
        [EnumValueData(Name = "Flag State", KeyValue = "11")]
        FlagState,

        /// <summary>
        /// The internal ism audit
        /// </summary>
        [EnumValueData(Name = "Internal ISM Audit", KeyValue = "13")]
        InternalISMAudit,

        /// <summary>
        /// The internal is o14001 audit
        /// </summary>
        [EnumValueData(Name = "Internal ISO 14001 Audit", KeyValue = "40")]
        InternalISO14001Audit,

        /// <summary>
        /// The internal is o50001 audit
        /// </summary>
        [EnumValueData(Name = "Internal ISO 50001 Audit", KeyValue = "63")]
        InternalISO50001Audit,

        /// <summary>
        /// The internal is o9001 audit
        /// </summary>
        [EnumValueData(Name = "Internal ISO 9001 Audit", KeyValue = "55")]
        InternalISO9001Audit,

        /// <summary>
        /// The internal isps audit
        /// </summary>
        [EnumValueData(Name = "Internal ISPS Audit", KeyValue = "19")]
        InternalISPSAudit,

        /// <summary>
        /// The internal ohsa S18001 audit
        /// </summary>
        [EnumValueData(Name = "Internal OHSAS 18001 Audit", KeyValue = "64")]
        InternalOHSAS18001Audit,

        /// <summary>
        /// The marine supt safety inspection
        /// </summary>
        [EnumValueData(Name = "Marine Supt Safety Inspection", KeyValue = "14")]
        MarineSuptSafetyInspection,

        /// <summary>
        /// The MLC self assessment ad M33
        /// </summary>
        [EnumValueData(Name = "MLC Self Assessment (ADM33)", KeyValue = "61")]
        MLCSelfAssessmentADM33,

        /// <summary>
        /// The moco crewing office audit
        /// </summary>
        [EnumValueData(Name = "MOCO Crewing Office Audit", KeyValue = "59")]
        MOCOCrewingOfficeAudit,

        /// <summary>
        /// The moto office audit
        /// </summary>
        [EnumValueData(Name = "MOTO Office Audit", KeyValue = "MOTOOfficeAudit")]
        MOTOOfficeAudit,

        /// <summary>
        /// The navigation self assessment audit na V15
        /// </summary>
        [EnumValueData(Name = "Navigation Self Assessment Audit (NAV15)", KeyValue = "46")]
        NavigationSelfAssessmentAuditNAV15,

        /// <summary>
        /// The office or ship emergency drill record
        /// </summary>
        [EnumValueData(Name = "Office/Ship Emergency Drill Record", KeyValue = "37")]
        OfficeOrShipEmergencyDrillRecord,

        /// <summary>
        /// The onboard training visit
        /// </summary>
        [EnumValueData(Name = "Onboard Training Visit", KeyValue = "45")]
        OnboardTrainingVisit,

        /// <summary>
        /// The port state control
        /// </summary>
        [EnumValueData(Name = "Port State Control", KeyValue = "10")]
        PortStateControl,

        /// <summary>
        /// The public health inspection internal
        /// </summary>
        [EnumValueData(Name = "Public Health Inspection Internal", KeyValue = "36")]
        PublicHealthInspectionInternal,

        /// <summary>
        /// The safety self assessment
        /// </summary>
        [EnumValueData(Name = "Safety Self Assessment", KeyValue = "12")]
        SafetySelfAssessment,

        /// <summary>
        /// The uscgicve or cve
        /// </summary>
        [EnumValueData(Name = "USCG ICVE/CVE", KeyValue = "30")]
        USCGICVEOrCVE,

        /// <summary>
        /// The vessel inspection report
        /// </summary>
        [EnumValueData(Name = "Vessel Inspection Report", KeyValue = "43")]
        VesselInspectionReport,

        /// <summary>
        /// The VGP annual inspection
        /// </summary>
        [EnumValueData(Name = "VGP Annual Inspection", KeyValue = "50")]
        VGPAnnualInspection,

        /// <summary>
        /// The VGP dry dock inspection
        /// </summary>
        [EnumValueData(Name = "VGP Dry-Dock Inspection", KeyValue = "53")]
        VGPDryDockInspection,

        /// <summary>
        /// The VGP quarterly inspection
        /// </summary>
        [EnumValueData(Name = "VGP Quarterly Inspection", KeyValue = "52")]
        VGPQuarterlyInspection,

        /// <summary>
        /// The office staff visit
        /// </summary>
        [EnumValueData(Name = "Office Staff Visit", KeyValue = "48")]
        OfficeStaffVisit,

        /// <summary>
        /// The senior managers or ceo visits
        /// </summary>
        [EnumValueData(Name = "Senior Managers/CEO Visits", KeyValue = "67")]
        SeniorManagersOrCEOVisits,

        /// <summary>
        /// The oil major vetting
        /// </summary>
        [EnumValueData(Name = "Oil Major Vetting", KeyValue = "17")]
        OilMajorVetting,

        /// <summary>
        /// The Envir. Compliance report A+B
        /// </summary>
        [EnumValueData(Name = "Envir. Compliance report A+B", KeyValue = "86")]
        EnvironmentComplianceReportAB,

        /// <summary>
        /// The Environmental Self Assessment
        /// </summary>
        [EnumValueData(Name = "Environmental Self Assessment", KeyValue = "32")]
        EnvironmentalSelfAssessment,

        /// <summary>
        /// The condition of class
        /// </summary>
        [EnumValueData(Name = "Conditions of Class", KeyValue = "18")]
        ConditionsOfClass,

        /// <summary>
        /// The shipboard ISM audit
        /// </summary>
        [EnumValueData(Name = "Shipboard ISM Audit", KeyValue = "13")]
        ShipboardISMAudit,

        /// <summary>
        /// The Cyber Security Audit
        /// </summary>
        [EnumValueData(Name = "Cyber Security Audit", KeyValue = "84")]
        CyberSecurityAudit,

        /// <summary>
        /// The Pre-vetting Inspection
        /// </summary>
        [EnumValueData(Name = "Pre-vetting Inspection", KeyValue = "47")]
        PreVettingInspection,

        /// <summary>
        /// The Marine Supt. Annual Inspection
        /// </summary>
        [EnumValueData(Name = "Marine Supt. Annual Inspection", KeyValue = "14")]
        MarineSuptAnnualInspection,


        /// <summary>
        /// The Vsl Inspection Report (At Sea)
        /// </summary>
        [EnumValueData(Name = "Vsl Inspection Report (At Sea)", KeyValue = "79")]
        VslInspectionReport
    }

    /// <summary>
    /// This enum is used for inspection status.
    /// </summary>
    public enum InspectionScreeningStatus
    {
        /// <summary>
        /// The pending
        /// </summary>
        [EnumValueData(Name = "Pending", KeyValue = "SYST00000015")]
        Pending,

        /// <summary>
        /// The accepted
        /// </summary>
        [EnumValueData(Name = "Accepted", KeyValue = "SYST00000002")]
        Accepted,

        /// <summary>
        /// The rejected
        /// </summary>
        [EnumValueData(Name = "Rejected", KeyValue = "SYST00000011")]
        Rejected
    }

    /// <summary>
    /// this enum is used for inspection manager report status
    /// </summary>
    public enum InspectionManagerReportStatus
    {
        /// <summary>
        /// The original report
        /// </summary>
        [EnumValueData(Name = "Original Report", KeyValue = "GLAS00000045")]
        OriginalReport,

        /// <summary>
        /// The report and review office
        /// </summary>
        [EnumValueData(Name = "Report And Review Office", KeyValue = "GLAS00000046")]
        ReportAndReviewOffice,

        /// <summary>
        /// The report complete
        /// </summary>
        [EnumValueData(Name = "Report Complete", KeyValue = "GLAS00000047")]
        ReportComplete,

        /// <summary>
        /// The reopen
        /// </summary>
        [EnumValueData(Name = "Reopen", KeyValue = "GLAS00000048")]
        Reopen
    }

    public enum QuestionAnswerType
    {
        /// <summary>
        /// The text.
        /// </summary>
        [EnumValueData(Name = "Text", KeyValue = "GLAS00000003")]
        Text,

        /// <summary>
        /// The yes no.
        /// </summary>
        [EnumValueData(Name = "Yes/No", KeyValue = "GLAS00000004")]
        YesNo,

        /// <summary>
        /// The date.
        /// </summary>
        [EnumValueData(Name = "Date", KeyValue = "GLAS00000005")]
        Date,

        /// <summary>
        /// The numeric.
        /// </summary>
        [EnumValueData(Name = "Numeric", KeyValue = "GLAS00000019")]
        Numeric,

        /// <summary>
        /// The scale.
        /// </summary>
        [EnumValueData(Name = "Scale", KeyValue = "GLAS00000020")]
        Scale,

        /// <summary>
        /// The user defined list.
        /// </summary>
        [EnumValueData(Name = "User Defined List", KeyValue = "GLAS00000051")]
        UserDefinedList
    }

    /// <summary>
    /// POS Activity Type Lookup code
    /// </summary>
    public enum PosActivityTypeLookupCode
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "")]
        All,
        /// <summary>
        /// The port
        /// </summary>
        [EnumValueData(Name = "Port", KeyValue = "Port")]
        Port,
        /// <summary>
        /// The posl
        /// </summary>
        [EnumValueData(Name = "Posl", KeyValue = "Posl")]
        Posl,
        /// <summary>
        /// The sea
        /// </summary>
        [EnumValueData(Name = "Sea", KeyValue = "Sea")]
        Sea,
        /// <summary>
        /// The sea b
        /// </summary>
        [EnumValueData(Name = "SeaB", KeyValue = "SeaB")]
        SeaB,
        /// <summary>
        /// The sea m
        /// </summary>
        [EnumValueData(Name = "SeaM", KeyValue = "SeaM")]
        SeaM,
        /// <summary>
        /// The type
        /// </summary>
        [EnumValueData(Name = "Type", KeyValue = "Type")]
        Type,
        /// <summary>
        /// The type
        /// </summary>
        [EnumValueData(Name = "Sailing", KeyValue = "Sailing")]
        Sailing
    }

    /// <summary>
    /// The ReportType
    /// </summary>
    public enum ReportType
    {
        [EnumValueData(Name = "FPD Dashboard", KeyValue = "1")]
        FPDDashboard,

        [EnumValueData(Name = "RightShip Maintainer", KeyValue = "2")]
        RightShipMaintainer,

        [EnumValueData(Name = "KPI Reports", KeyValue = "3")]
        KPIReports,

        [EnumValueData(Name = "GHG Rating Report", KeyValue = "4")]
        GHGRatingReport
    }

    /// <summary>
    /// Consumption Category Type Attribute
    /// </summary>
    public enum ConsumptionCategoryAttribute
    {
        /// <summary>
        /// The tank capacity
        /// </summary>
        [EnumValueData(Name = "Tank Capacity", KeyValue = "GLAS00000004")]
        TankCapacity = 1,
        /// <summary>
        /// The previous rob
        /// </summary>
        [EnumValueData(Name = "Previous ROB", KeyValue = "GLAS00000005")]
        PreviousROB = 2,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events consumption", KeyValue = "GLAS00000050")]
        PreviousEventsConsumption = 3,

        /// <summary>
        /// The sulphur percent
        /// </summary>
        [EnumValueData(Name = "Sulphur Percent", KeyValue = "GLAS00000006")]
        SulphurPercent = 4,

        /// <summary>
        /// Me consumption
        /// </summary>
        [EnumValueData(Name = "ME Consumption", KeyValue = "GLAS00000007")]
        MEConsumption = 5,

        /// <summary>
        /// The cargo heat cool
        /// </summary>
        [EnumValueData(Name = "Cargo heat/ cool", KeyValue = "GLAS00000008")]
        CargoHeatCool = 6,

        /// <summary>
        /// The boiler
        /// </summary>
        [EnumValueData(Name = "Boiler", KeyValue = "GLAS00000009")]
        Boiler = 7,

        /// <summary>
        /// The dg consumption
        /// </summary>
        [EnumValueData(Name = "DG Consumption", KeyValue = "GLAS00000010")]
        DGConsumption = 8,

        /// <summary>
        /// The other
        /// </summary>
        [EnumValueData(Name = "Other", KeyValue = "GLAS00000011")]
        Other = 9,

        /// <summary>
        /// The total consumption
        /// </summary>
        [EnumValueData(Name = "Total (Consumption)", KeyValue = "GLAS00000012")]
        TotalConsumption = 10,

        /// <summary>
        /// The received fuel
        /// </summary>
        [EnumValueData(Name = "Received Fuel", KeyValue = "GLAS00000013")]
        ReceivedFuel = 11,

        /// <summary>
        /// The discharged fuel
        /// </summary>
        [EnumValueData(Name = "Discharged Fuel", KeyValue = "GLAS00000014")]
        DischargedFuel = 12,

        /// <summary>
        /// The total rob
        /// </summary>
        [EnumValueData(Name = "ROB", KeyValue = "GLAS00000015")]
        TotalROB = 13,

        /// <summary>
        /// The free capacity
        /// </summary>
        [EnumValueData(Name = "Free Capacity", KeyValue = "GLAS00000016")]
        FreeCapacity = 14,
        /// <summary>
        /// The discharging
        /// </summary>
        [EnumValueData(Name = "Discharging", KeyValue = "GLAS00000067")]
        Discharging = 15,
        /// <summary>
        /// The loading
        /// </summary>
        [EnumValueData(Name = "Loading", KeyValue = "GLAS00000068")]
        Loading = 16,
        /// <summary>
        /// The deballast
        /// </summary>
        [EnumValueData(Name = "Deballast", KeyValue = "GLAS00000069")]
        Deballast = 17,
        /// <summary>
        /// The igs
        /// </summary>
        [EnumValueData(Name = "IGS", KeyValue = "GLAS00000072")]
        IGS = 18,
        /// <summary>
        /// The tankcleaning
        /// </summary>
        [EnumValueData(Name = "Tank cleaning", KeyValue = "GLAS00000073")]
        Tankcleaning = 19,

        /// <summary>
        /// The before survey
        /// </summary>
        [EnumValueData(Name = "Before Survey", KeyValue = "GLAS00000265")]
        BeforeSurvey,

        /// <summary>
        /// The after survey
        /// </summary>
        [EnumValueData(Name = "After Survey", KeyValue = "GLAS00000266")]
        AfterSurvey,

        /// <summary>
        /// The eosp
        /// </summary>
        [EnumValueData(Name = "EOSP", KeyValue = "GLAS00000272")]
        EOSP,
    }

    /// <summary>
    /// The lub oil rob break down
    /// </summary>
    public enum LubOilROBBreakDown
    {
        /// <summary>
        /// The tank capacity
        /// </summary>
        [EnumValueData(Name = "Tank Capacity", KeyValue = "GLAS00000004")]
        TankCapacity = 1,

        /// <summary>
        /// The previous rob
        /// </summary>
        [EnumValueData(Name = "Previous ROB", KeyValue = "GLAS00000005")]
        PreviousROB = 2,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events prod.", KeyValue = "GLAS00000051")]
        PreviousEventsProduction = 3,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events consumption", KeyValue = "GLAS00000050")]
        PreviousEventsConsumption = 4,

        /// <summary>
        /// The production qty
        /// </summary>
        [EnumValueData(Name = "Supplied Qty.", KeyValue = "GLAS00000040")]
        ProductionQty = 5,

        /// <summary>
        /// The consumption qty
        /// </summary>
        [EnumValueData(Name = "Consumption Qty.", KeyValue = "GLAS00000041")]
        ConsumptionQty = 6,

        /// <summary>
        /// The current rob
        /// </summary>
        [EnumValueData(Name = "ROB", KeyValue = "GLAS00000042")]
        CurrentRob = 7,
    }


    /// <summary>
    /// Fuel Type Attribute
    /// </summary>
    public enum FuelType
    {
        /// <summary>
        /// The fo
        /// </summary>
        [EnumValueData(Name = "HFO (mt)", KeyValue = "GLAS00000017")]
        FO,

        /// <summary>
        /// The lsfo
        /// </summary>
        [EnumValueData(Name = "LFO (mt)", KeyValue = "GLAS00000018")]
        LSFO,

        /// <summary>
        /// The do
        /// </summary>
        [EnumValueData(Name = "DO (mt)", KeyValue = "GLAS00000019")]
        DO,

        /// <summary>
        /// The go
        /// </summary>
        [EnumValueData(Name = "GO (mt)", KeyValue = "GLAS00000020")]
        GO,

        /// <summary>
        /// The LNG
        /// </summary>
        [EnumValueData(Name = "LNG (mt)", KeyValue = "GLAS00000021")]
        LNG,

        /// <summary>
        /// The LNG Cargo
        /// </summary>
        [EnumValueData(Name = "LNG Cargo(mt)", KeyValue = "GLAS00000263")]
        LNGCargo,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LubOilType
    {
        /// <summary>
        /// The clo
        /// </summary>
        [EnumValueData(Name = "CLO (m3)", KeyValue = "GLAS00000027")]
        CLO = 1,

        /// <summary>
        /// The aux
        /// </summary>
        [EnumValueData(Name = "Aux (m3)", KeyValue = "GLAS00000029")]
        Aux = 2,

        /// <summary>
        /// The crank case
        /// </summary>
        [EnumValueData(Name = "Crank Case (m3)", KeyValue = "GLAS00000028")]
        CrankCase = 3,
        /// <summary>
        /// The crank case
        /// </summary>
        [EnumValueData(Name = "General (m3)", KeyValue = "GLAS00000063")]
        General = 4
    }

    /// <summary>
    /// Oil grade type specifications
    /// </summary>
    public enum OillGradeTypeSpecificationForVoyage
    {
        /// <summary>
        /// The fuel oil
        /// </summary>
        [EnumValueData(Name = "HFO", KeyValue = "F")]
        FO = 0,


        /// <summary>
        /// The fuel oil
        /// </summary>
        [EnumValueData(Name = "LFO", KeyValue = "F")]
        LSFO = 2,

        /// <summary>
        /// The diesel oil
        /// </summary>
        [EnumValueData(Name = "DO", KeyValue = "D")]
        DO = 3,

        /// <summary>
        /// The gas oil
        /// </summary>
        [EnumValueData(Name = "GO", KeyValue = "G")]
        GO = 4,

        /// <summary>
        /// The lube oil
        /// </summary>

        [EnumValueData(Name = "LNG", KeyValue = "N")]
        LNG = 5,

        /// <summary>
        /// The lub oil
        /// </summary>
        [EnumValueData(Name = "Lube Oil", KeyValue = "O")]
        LubOil = 6,

    }

    /// <summary>
    /// FreshWaterROBBreakDown
    /// </summary>
    public enum FreshWaterROBBreakDown
    {

        /// <summary>
        /// The tank capacity
        /// </summary>
        [EnumValueData(Name = "Tank Capacity", KeyValue = "GLAS00000004")]
        TankCapacity = 1,

        /// <summary>
        /// The previous rob
        /// </summary>
        [EnumValueData(Name = "Previous ROB", KeyValue = "GLAS00000005")]
        PreviousROB = 2,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events prod.", KeyValue = "GLAS00000051")]
        PreviousEventsProduction = 3,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events consumption", KeyValue = "GLAS00000050")]
        PreviousEventsConsumption = 4,

        /// <summary>
        /// The production qty
        /// </summary>
        [EnumValueData(Name = "Prod. Qty.", KeyValue = "GLAS00000037")]
        ProductionQty = 5,

        /// <summary>
        /// The consumption qty
        /// </summary>
        [EnumValueData(Name = "Consumption Qty.", KeyValue = "GLAS00000038")]
        ConsumptionQty = 6,

        /// <summary>
        /// The current rob
        /// </summary>
        [EnumValueData(Name = "ROB", KeyValue = "GLAS00000039")]
        CurrentRob = 7,
    }

    /// <summary>
    /// WasteROBBreakDown
    /// </summary>
    public enum WasteROBBreakDown
    {
        /// <summary>
        /// The tank capacity
        /// </summary>
        [EnumValueData(Name = "Tank Capacity", KeyValue = "GLAS00000004")]
        TankCapacity = 1,

        /// <summary>
        /// The previous rob
        /// </summary>
        [EnumValueData(Name = "Previous ROB", KeyValue = "GLAS00000005")]
        PreviousROB = 2,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events prod.", KeyValue = "GLAS00000051")]
        PreviousEventsProduction = 3,

        /// <summary>
        /// The previous events consumption
        /// </summary>
        [EnumValueData(Name = "Previous events discharge", KeyValue = "GLAS00000050")]
        PreviousEventsConsumption = 4,

        /// <summary>
        /// The production qty
        /// </summary>
        [EnumValueData(Name = "Prod. Qty.", KeyValue = "GLAS00000034")]
        ProductionQty = 5,

        /// <summary>
        /// The discharge quantity
        /// </summary>
        [EnumValueData(Name = "Dis. Qty.", KeyValue = "GLAS00000035")]
        DischargeQuantity = 6,

        /// <summary>
        /// The current rob
        /// </summary>
        [EnumValueData(Name = "ROB", KeyValue = "GLAS00000036")]
        CurrentRob = 7,


    }

    /// <summary>
    /// Ballast Type
    /// </summary>
    public enum BallastType
    {
        /// <summary>
        /// The clean
        /// </summary>
        [EnumValueData(Name = "Clean", KeyValue = "GLAS00000043")]
        Clean = 1,
        /// <summary>
        /// The dirty
        /// </summary>
        [EnumValueData(Name = "Dirty", KeyValue = "GLAS00000044")]
        Dirty = 2

    }

    /// <summary>
    /// Ballast Break Down Attribute
    /// </summary>
    public enum BallastBreakDownAttribute
    {
        /// <summary>
        /// The initial qty
        /// </summary>
        [EnumValueData(Name = "Initial Qty (mt)", KeyValue = "GLAS00000045")]
        InitialQty = 1,
        /// <summary>
        /// The final qty
        /// </summary>
        [EnumValueData(Name = "Final Qty (mt)", KeyValue = "GLAS00000046")]
        FinalQty = 2,
        /// <summary>
        /// The tank capacity
        /// </summary>
        [EnumValueData(Name = "Tank Capacity", KeyValue = "GLAS00000004")]
        TankCapacity = 3,
    }

    /// <summary>
    /// Enum for Risk Assessment source.
    /// </summary>
    public enum RiskAssessmentSource
    {
        /// <summary>
        /// The reschedule work order.
        /// </summary>
        [EnumValueData(Name = "Reschedule Work Order", KeyValue = "SYST00000143")]
        RescheduleWorkOrder
    }

    /// <summary>
    /// Module Enum
    /// </summary>
    public enum SecModule
    {
        /// <summary>
        /// The purchasing
        /// </summary>
        [EnumValueData(Name = "Purchasing", KeyValue = "GLAS00000014")]
        Purchasing,

        /// <summary>
        /// The invoicing
        /// </summary>
        [EnumValueData(Name = "Invoicing", KeyValue = "GLAS00000006")]
        Invoicing,

        /// <summary>
        /// The job safety analysis
        /// </summary>
        [EnumValueData(Name = "JobSafetyAnalysis", KeyValue = "GLAS00000086")]
        JobSafetyAnalysis,

        /// <summary>
        /// The weekly minutes
        /// </summary>
        [EnumValueData(Name = "WeeklyMinutes", KeyValue = "GLAS00000121")]
        WeeklyMinutes,

        /// <summary>
        /// The environmental management
        /// </summary>
        [EnumValueData(Name = "EnvironmentalManagement", KeyValue = "GLAS00000060")]
        EnvironmentalManagement,

        /// <summary>
        /// The management of change
        /// </summary>
        [EnumValueData(Name = "ManagementOfChange", KeyValue = "GLAS00000079")]
        ManagementOfChange,

        /// <summary>
        /// The haz occ
        /// </summary>
        [EnumValueData(Name = "HazOcc", KeyValue = "GLAS00000017")]
        HazOcc,

        /// <summary>
        /// The voyage reporting.
        /// </summary>
        [EnumValueData(Name = "Voyage Reporting", KeyValue = "GLAS00000021")]
        VoyageReporting,

        /// <summary>
        /// The Sales Invoicing module.
        /// </summary>
        [EnumValueData(Name = "SalesInvoicing", KeyValue = "GLAS00000072")]
        SalesInvoicing,

        /// <summary>
        /// The Ledger module.
        /// </summary>
        [EnumValueData(Name = "Ledger", KeyValue = "GLAS00000008")]
        Ledger,

        /// <summary>
        /// The office moc
        /// </summary>
        [EnumValueData(Name = "OfficeMOC", KeyValue = "MRMA00000005")]
        OfficeMOC,

        /// <summary>
        /// The seemp
        /// </summary>
        [EnumValueData(Name = "Seemp", KeyValue = "GLAS00000060")]
        Seemp
    }

    /// <summary>
    /// Model Types
    /// </summary>
    public enum ModelTypes
    {
        /// <summary>
        /// The machine
        /// </summary>
        [EnumValueData(Name = "Machine", KeyValue = "Machine")]
        Machine,

        /// <summary>
        /// The Expert
        /// </summary>
        [EnumValueData(Name = "Expert", KeyValue = "Expert")]
        Expert,
    }

    /// <summary>
    /// Status
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// All
        /// </summary>
        [EnumValueData(Name = "All", KeyValue = "All")]
        All,

        /// <summary>
        /// Red
        /// </summary>
        [EnumValueData(Name = "Red", KeyValue = "Red")]
        Red,

        /// <summary>
        /// Amber
        /// </summary>
        [EnumValueData(Name = "Amber", KeyValue = "Amber")]
        Amber,

        /// <summary>
        /// Green
        /// </summary>
        [EnumValueData(Name = "Green", KeyValue = "Green")]
        Green
    }

    /// <summary>
    /// BiggestMoversRange
    /// </summary>
    public enum BiggestMoversRange
    {
        /// <summary>
        /// IncreasedUpto05
        /// </summary>
        [EnumValueData(Name = "Upto 0.5", KeyValue = "1")]
        IncreasedUpto05,

        /// <summary>
        /// Increased05to15
        /// </summary>
        [EnumValueData(Name = "0.5 - 1.5", KeyValue = "2")]
        Increased05to15,

        /// <summary>
        /// IncreasedAbove15
        /// </summary>
        [EnumValueData(Name = "Above 1.5", KeyValue = "3")]
        IncreasedAbove15,

        /// <summary>
        /// DecreasedUpto05
        /// </summary>
        [EnumValueData(Name = "Upto 0.5", KeyValue = "4")]
        DecreasedUpto05,

        /// <summary>
        /// Decreased05to15
        /// </summary>
        [EnumValueData(Name = "0.5 - 1.5", KeyValue = "5")]
        Decreased05to15,

        /// <summary>
        /// DecreasedAbove15
        /// </summary>
        [EnumValueData(Name = "Above 1.5", KeyValue = "6")]
        DecreasedAbove15,
    }

}