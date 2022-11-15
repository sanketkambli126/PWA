﻿using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// The record discussion request
    /// </summary>
    public class RecordDiscussionRequest
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the reference ids.
        /// </summary>
        /// <value>
        /// The reference ids.
        /// </value>
        public List<string> ReferenceIds { get; set; }
    }
}
