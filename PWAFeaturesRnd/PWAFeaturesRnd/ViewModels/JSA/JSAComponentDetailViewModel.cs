using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    public class JSAComponentDetailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }
        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; set; }
        /// <summary>
        /// Gets or sets the maker.
        /// </summary>
        /// <value>
        /// The maker.
        /// </value>
        public string Maker { get; set; }
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; set; }
        /// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>
        /// The class code.
        /// </value>
        public string ClassCode { get; set; }
        /// <summary>
        /// Gets or sets the work order status description.
        /// </summary>
        /// <value>
        /// The work order status description.
        /// </value>
        public string WorkOrderStatusDescription { get; set; }
        /// <summary>
        /// Gets or sets the report work done date.
        /// </summary>
        /// <value>
        /// The report work done date.
        /// </value>
        public DateTime? ReportWorkDoneDate { get; set; }
    }
}
