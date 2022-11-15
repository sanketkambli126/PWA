using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection contract class
    /// </summary>
    public class SaveInspectionVisitViewModel
    {

        /// <summary>
        /// Converts to portname.
        /// </summary>
        /// <value>
        /// The name of to port.
        /// </value>
        public string ToPortName { get; set; }

        /// <summary>
        /// Converts to portid.
        /// </summary>
        /// <value>
        /// To port identifier.
        /// </value>
        public string ToPortId { get; set; }


        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the where.
        /// </summary>
        /// <value>
        /// The where.
        /// </value>
        public string Where { get; set; }


        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the port identifier.
        /// </summary>
        /// <value>
        /// The port identifier.
        /// </value>
        public string PortId { get; set; }


        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }


        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public string DepartmentId { get; set; }


        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }


        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }


        /// <summary>
        /// Gets or sets the inspector title.
        /// </summary>
        /// <value>
        /// The inspector title.
        /// </value>
        public string InspectorTitle { get; set; }

        /// <summary>
        /// Gets or sets the inspector surname.
        /// </summary>
        /// <value>
        /// The inspector surname.
        /// </value>
        public string InspectorSurname { get; set; }


        /// <summary>
        /// Gets or sets the inspector forename.
        /// </summary>
        /// <value>
        /// The inspector forename.
        /// </value>
        public string InspectorForename { get; set; }
        /// <summary>
        /// Gets or sets the office reviewer detail.
        /// </summary>
        /// <value>
        /// The office reviewer detail.
        /// </value>
        public List<VesselInspectionOfficeReviewerDetail> OfficeReviewerDetail { get; set; }
        /// <summary>
        /// Gets or sets the schedule details.
        /// </summary>
        /// <value>
        /// The schedule details.
        /// </value>
        public List<InspectionScheduleDetail> ScheduleDetails { get; set; }
        /// <summary>
        /// Gets or sets the mapped questions.
        /// </summary>
        /// <value>
        /// The mapped questions.
        /// </value>
        public List<MarineQuestionAnswerDetailResponse> MappedQuestions { get; set; }
    }

}

       
