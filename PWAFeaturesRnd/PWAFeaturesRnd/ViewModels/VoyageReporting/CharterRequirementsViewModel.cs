using PWAFeaturesRnd.Common;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Charter Requirements View Model
	/// </summary>
	public class CharterRequirementsViewModel
	{
		/// <summary>
		/// Gets or sets the type of the fuel.
		/// </summary>
		/// <value>
		/// The type of the fuel.
		/// </value>
		public string FuelType { get; set; }

		/// <summary>
		/// Gets or sets the loaded value.
		/// </summary>
		/// <value>
		/// The loaded value.
		/// </value>
		public float? LoadedValue { get; set; }

		/// <summary>
		/// Gets or sets the ballast value.
		/// </summary>
		/// <value>
		/// The ballast value.
		/// </value>
		public float? BallastValue { get; set; }

		/// <summary>
		/// Gets or sets the actual value.
		/// </summary>
		/// <value>
		/// The actual value.
		/// </value>
		public float? ActualValue { get; set; }

		/// <summary>
		/// Gets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(FuelType) && FuelType.Contains(Constants.SpeedKts))
				{
					if (LoadedValue.HasValue)
					{
						return ActualValue.HasValue && LoadedValue.GetValueOrDefault() != 0 && ActualValue < LoadedValue;
					}
					if (BallastValue.HasValue)
					{
						return ActualValue.HasValue && BallastValue.GetValueOrDefault() != 0 && ActualValue < BallastValue;
					}
				}
				else
				{
					if (LoadedValue.HasValue)
					{
						return ActualValue.HasValue && LoadedValue.GetValueOrDefault() != 0 && ActualValue > LoadedValue;
					}
					if (BallastValue.HasValue)
					{
						return ActualValue.HasValue && BallastValue.GetValueOrDefault() != 0 && ActualValue > BallastValue;
					}
				}
				return false;
			}
		}

	}
}
