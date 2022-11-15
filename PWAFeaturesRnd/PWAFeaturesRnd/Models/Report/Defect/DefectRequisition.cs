using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectRequisition
    {
        /// <summary>
        /// Gets or sets the DRQ identifier.
        /// </summary>
        /// <value>
        /// The DRQ identifier.
        /// </value>
        public string DrqId { get; set; }

        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the PGR identifier.
        /// </summary>
        /// <value>
        /// The PGR identifier.
        /// </value>
        public string PgrId { get; set; }

        /// <summary>
        /// Gets or sets the PTR identifier.
        /// </summary>
        /// <value>
        /// The PTR identifier.
        /// </value>
        public string PtrId { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        /// <value>
        /// The name of the order.
        /// </value>
        public string OrderName { get; set; }

        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// The type of the order.
        /// </value>
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets the department short code.
        /// </summary>
        /// <value>
        /// The department short code.
        /// </value>
        public string DepartmentShortCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the account code description.
        /// </summary>
        /// <value>
        /// The account code description.
        /// </value>
        public string AccountCodeDescription { get; set; }

        /// <summary>
        /// Gets or sets the requested date.
        /// </summary>
        /// <value>
        /// The requested date.
        /// </value>
        public DateTime? RequestedDate { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the expected port.
        /// </summary>
        /// <value>
        /// The expected port.
        /// </value>
        public string ExpectedPort { get; set; }

        /// <summary>
        /// Gets or sets the expected delivery date.
        /// </summary>
        /// <value>
        /// The expected delivery date.
        /// </value>
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>
        /// The received date.
        /// </value>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the linked defect.
        /// </summary>
        /// <value>
        /// The linked defect.
        /// </value>
        public int? LinkedDefect { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
