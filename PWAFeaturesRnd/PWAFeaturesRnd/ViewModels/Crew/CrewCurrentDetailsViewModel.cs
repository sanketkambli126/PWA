using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    ///   <br />
    /// </summary>
    public class CrewCurrentDetailsViewModel
    {
        /// <summary>Gets or sets the rank.</summary>
        /// <value>The rank.</value>
        public string Rank { get; set; }

        /// <summary>Gets or sets the vessel.</summary>
        /// <value>The vessel.</value>
        public string Vessel { get; set; }

        /// <summary>Gets or sets the service start.</summary>
        /// <value>The service start.</value>
        public DateTime ServiceStart { get; set; }

        /// <summary>Gets or sets the service end.</summary>
        /// <value>The service end.</value>
        public DateTime ServiceEnd { get; set; }

        /// <summary>Gets or sets the client.</summary>
        /// <value>The client.</value>
        public string Client { get; set; }

        /// <summary>Gets or sets the days rem.</summary>
        /// <value>The days rem.</value>
        public int DaysRem { get; set; }

        /// <summary>Gets or sets the length of the contract.</summary>
        /// <value>The length of the contract.</value>
        public int ContractLength { get; set; }

        /// <summary>Gets or sets the contract length unit.</summary>
        /// <value>The contract length unit.</value>
        public string ContractLengthUnit { get; set; }

    }
}
