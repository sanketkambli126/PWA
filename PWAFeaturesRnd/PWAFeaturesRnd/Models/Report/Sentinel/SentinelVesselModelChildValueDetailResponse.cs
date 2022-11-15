using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Vessel Model Child Value Detail Response
    /// </summary>
    public class SentinelVesselModelChildValueDetailResponse
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the model dimension.
        /// </summary>
        /// <value>
        /// The name of the model dimension.
        /// </value>
        public string ModelDimensionName { get; set; }

        /// <summary>
        /// Gets or sets the model combined value.
        /// </summary>
        /// <value>
        /// The model combined value.
        /// </value>
        public decimal? ModelCombinedValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the model combined value.
        /// </summary>
        /// <value>
        /// The color of the model combined value.
        /// </value>
        public string ModelCombinedValueColor { get; set; }

        /// <summary>
        /// Gets or sets the model current value.
        /// </summary>
        /// <value>
        /// The model current value.
        /// </value>
        public decimal? ModelCurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the model current value.
        /// </summary>
        /// <value>
        /// The color of the model current value.
        /// </value>
        public string ModelCurrentValueColor { get; set; }

        /// <summary>
        /// Gets or sets the model lagging value.
        /// </summary>
        /// <value>
        /// The model lagging value.
        /// </value>
        public decimal? ModelLaggingValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the model lagging value.
        /// </summary>
        /// <value>
        /// The color of the model lagging value.
        /// </value>
        public string ModelLaggingValueColor { get; set; }

        /// <summary>
        /// Gets or sets the model override dimension identifier.
        /// </summary>
        /// <value>
        /// The model override dimension identifier.
        /// </value>
        public string ModelOverrideDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the model override dimension description.
        /// </summary>
        /// <value>
        /// The model override dimension description.
        /// </value>
        public string ModelOverrideDimensionDescription { get; set; }

        /// <summary>
        /// Gets or sets the model override dimension value.
        /// </summary>
        /// <value>
        /// The model override dimension value.
        /// </value>
        public decimal? ModelOverrideDimensionValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the model override dimension value.
        /// </summary>
        /// <value>
        /// The color of the model override dimension value.
        /// </value>
        public string ModelOverrideDimensionValueColor { get; set; }

        /// <summary>
        /// Gets or sets the name of the navigation link.
        /// </summary>
        /// <value>
        /// The name of the navigation link.
        /// </value>
        public string NavigationLinkName { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        /// <value>
        /// The name of the view.
        /// </value>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the type of the navigation.
        /// </summary>
        /// <value>
        /// The type of the navigation.
        /// </value>
        public string NavigationType { get; set; }

        /// <summary>
        /// Gets or sets the navigation filter.
        /// </summary>
        /// <value>
        /// The navigation filter.
        /// </value>
        public string NavigationFilter { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        public string ModelType { get; set; }

        /// <summary>
        /// Gets or sets the model type value.
        /// </summary>
        /// <value>
        /// The model type value.
        /// </value>
        public int? ModelTypeValue { get; set; }

        /// <summary>
        /// Gets or sets the model machine facor detail.
        /// </summary>
        /// <value>
        /// The model machine facor detail.
        /// </value>
        public List<SentinelVesselModelFactorDetailResponse> ModelMachineFacorDetail { get; set; }

        /// <summary>
        /// Gets or sets the model expert factor detail.
        /// </summary>
        /// <value>
        /// The model expert factor detail.
        /// </value>
        public List<SentinelVesselModelExpertTypeFactorDetailResponse> ModelExpertFactorDetail { get; set; }

        /// <summary>
        /// Gets or sets the model override detail.
        /// </summary>
        /// <value>
        /// The model override detail.
        /// </value>
        public List<SentinelCategoryOverrideDetailResponse> ModelOverrideDetail { get; set; }
    }
}
