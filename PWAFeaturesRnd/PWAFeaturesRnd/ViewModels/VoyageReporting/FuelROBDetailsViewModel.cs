namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    public class FuelROBDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the tank capacity.
        /// </summary>
        /// <value>
        /// The tank capacity.
        /// </value>
        public float? TankCapacity { get; set; }

        /// <summary>
        /// Gets or sets the previous rob.
        /// </summary>
        /// <value>
        /// The previous rob.
        /// </value>
        public float? PreviousROB { get; set; }

        /// <summary>
        /// Gets or sets the sulphur.
        /// </summary>
        /// <value>
        /// The sulphur.
        /// </value>
        public string Sulphur { get; set; }
        /// <summary>
        /// Gets or sets the boiler.
        /// </summary>
        /// <value>
        /// The boiler.
        /// </value>
        public string Boiler { get; set; }

        /// <summary>
        /// Gets or sets the break consumption.
        /// </summary>
        /// <value>
        /// The break consumption.
        /// </value>
        public string BreakConsumption { get; set; }

        /// <summary>
        /// Gets or sets the cargo.
        /// </summary>
        /// <value>
        /// The cargo.
        /// </value>
        public string Cargo { get; set; }

        /// <summary>
        /// Gets or sets the dg consumption.
        /// </summary>
        /// <value>
        /// The dg consumption.
        /// </value>
        public string DGConsumption { get; set; }

        /// <summary>
        /// Gets or sets me consumption.
        /// </summary>
        /// <value>
        /// Me consumption.
        /// </value>
        public string MEConsumption { get; set; }

        /// <summary>
        /// Gets or sets the de ballast.
        /// </summary>
        /// <value>
        /// The de ballast.
        /// </value>
        public string DeBallast { get; set; }

        /// <summary>
        /// Gets or sets the igs.
        /// </summary>
        /// <value>
        /// The igs.
        /// </value>
        public string IGS { get; set; }

        /// <summary>
        /// Gets or sets the tank cleaning.
        /// </summary>
        /// <value>
        /// The tank cleaning.
        /// </value>
        public string TankCleaning { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>
        /// The other.
        /// </value>
        public string Other { get; set; }

        /// <summary>
        /// Gets or sets the is sulphur read only.
        /// </summary>
        /// <value>
        /// The is sulphur read only.
        /// </value>
        public bool IsSulphurReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the total rob.
        /// </summary>
        /// <value>
        /// The total rob.
        /// </value>
        public float? TotalROB { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public double Percentage { get; set; }

        /// <summary>
        /// Gets or sets the free capacity.
        /// </summary>
        /// <value>
        /// The free capacity.
        /// </value>
        public float? FreeCapacity { get; set; }

        /// <summary>
        /// Gets or sets the total consumption.
        /// </summary>
        /// <value>
        /// The total consumption.
        /// </value>
        public float? TotalConsumption { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is type collapsed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is type collapsed; otherwise, <c>false</c>.
        /// </value>
        public bool IsTypeCollapsed { get; set; }
    }
}
