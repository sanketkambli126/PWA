using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.InspectionManager;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Data contract for incident event details.
    /// </summary>
    public class IncidentEventDetail
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>

        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>

        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the idi identifier.
        /// </summary>
        /// <value>
        /// The idi identifier.
        /// </value>

        public string IdiId { get; set; }

        /// <summary>
        /// Gets or sets the ship operation identifier.
        /// </summary>
        /// <value>
        /// The ship operation identifier.
        /// </value>

        public string ShipOperationId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [damage to vessel or equip].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [damage to vessel or equip]; otherwise, <c>false</c>.
        /// </value>

        public bool DamageToVesselOrEquip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is drug or alcohol factor.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is drug or alcohol factor; otherwise, <c>false</c>.
        /// </value>

        public bool IsDrugOrAlcoholFactor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is protest note issued.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is protest note issued; otherwise, <c>false</c>.
        /// </value>

        public bool IsProtestNoteIssued { get; set; }

        /// <summary>
        /// Gets or sets the pollution deck identifier.
        /// </summary>
        /// <value>
        /// The pollution deck identifier.
        /// </value>

        public string PollutionDeckId { get; set; }

        /// <summary>
        /// Gets or sets the oil LTRS deck.
        /// </summary>
        /// <value>
        /// The oil LTRS deck.
        /// </value>

        public float? OilLtrsDeck { get; set; }

        /// <summary>
        /// Gets or sets the deck details.
        /// </summary>
        /// <value>
        /// The deck details.
        /// </value>

        public string DeckDetails { get; set; }

        /// <summary>
        /// Gets or sets the pollution sea identifier.
        /// </summary>
        /// <value>
        /// The pollution sea identifier.
        /// </value>

        public string PollutionSeaId { get; set; }

        /// <summary>
        /// Gets or sets the oil LTRS sea.
        /// </summary>
        /// <value>
        /// The oil LTRS sea.
        /// </value>

        public float? OilLtrsSea { get; set; }

        /// <summary>
        /// Gets or sets the sea details.
        /// </summary>
        /// <value>
        /// The sea details.
        /// </value>

        public string SeaDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [damage to third party].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [damage to third party]; otherwise, <c>false</c>.
        /// </value>

        public bool DamageToThirdParty { get; set; }

        /// <summary>
        /// Gets or sets the damage to third party desc.
        /// </summary>
        /// <value>
        /// The damage to third party desc.
        /// </value>

        public string DamageToThirdPartyDesc { get; set; }

        /// <summary>
        /// Gets or sets the immediate action taken.
        /// </summary>
        /// <value>
        /// The immediate action taken.
        /// </value>

        public string ImmediateActionTaken { get; set; }

        /// <summary>
        /// Gets or sets the damage description.
        /// </summary>
        /// <value>
        /// The damage description.
        /// </value>

        public string DamageDescription { get; set; }

        /// <summary>
        /// Gets or sets the hierarchy explorer mapping.
        /// </summary>
        /// <value>
        /// The hierarchy explorer mapping.
        /// </value>

        public List<HierarchyExplorerMappingDetail> HierarchyExplorerMapping { get; set; }
    }
}
