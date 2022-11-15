using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Common
{
	public class UserMenuItem
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public string DisplayText { get; set; }

        /// <summary>
        /// Gets or sets the type of the user menu item.
        /// </summary>
        /// <value>
        /// The type of the user menu item.
        /// </value>
        public UserMenuItemType UserMenuItemType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildren { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public List<UserMenuItem> Children { get; set; }

        /// <summary>
        /// Gets or sets the other identifiers.
        /// </summary>
        /// <value>
        /// The other identifiers.
        /// </value>
        public Dictionary<string, object> OtherIdentifiers { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is ves forward planning applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ves forward planning applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesForwardPlanningApplicable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optimiser enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is optimiser enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsOptimiserEnabled { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel gen.
        /// </summary>
        /// <value>
        /// The type of the vessel gen.
        /// </value> 
        public string VesselGenType { get; set; }

        /// Note : This property is used only for ClientNode.
        /// <summary>
        /// Gets or sets the is vessel with no coy identifier.
        /// </summary>
        /// <value>
        /// The is vessel with no coy identifier.
        /// </value>
        /// Default 0 : VesselName with CoyId Will be displayed.
        /// Value   1 : Distinct VesselName will be displayed. (CoyId doesn'.t matter)
        public int IsVesselWithNoCoyId { get; set; }
    }
}
