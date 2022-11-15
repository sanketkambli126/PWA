using System;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// 
    /// </summary>
    public class JSATaskBreakdownDetailViewModel
    {
        /// <summary>
        /// Gets or sets the seq no.
        /// </summary>
        /// <value>
        /// The seq no.
        /// </value>
        public int SeqNo { get; set; }

        /// <summary>
        /// Gets or sets the step description.
        /// </summary>
        /// <value>
        /// The step description.
        /// </value>
        public string StepDescription { get; set; }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>
        /// The task description.
        /// </value>
        public string TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the estimate completion date time.
        /// </summary>
        /// <value>
        /// The estimate completion date time.
        /// </value>
        public DateTime? EstCompletionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the responsible person.
        /// </summary>
        /// <value>
        /// The responsible person.
        /// </value>
        public string ResponsiblePerson { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }
    }
}
