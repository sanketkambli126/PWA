namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class RunningBalanceResponseViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the type of the currency.
        /// </summary>
        /// <value>
        /// The type of the currency.
        /// </value>
        public int CurrencyType { get; set; }

        /// <summary>
        /// Gets or sets the distinct currencies.
        /// </summary>
        /// <value>
        /// The distinct currencies.
        /// </value>
        public int DistinctCurrencies { get; set; }

        /// <summary>
        /// Gets or sets the running balance base.
        /// </summary>
        /// <value>
        /// The running balance base.
        /// </value>
        public decimal RunningBalanceBase { get; set; }

        /// <summary>
        /// Gets or sets the running balance original.
        /// </summary>
        /// <value>
        /// The running balance original.
        /// </value>
        public decimal RunningBalanceOrig { get; set; }

        #endregion
    }
}
