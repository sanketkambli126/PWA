using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Finance
{
	public class ChartAccountDetailResponseViewModel
	{
		/// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
		public string AccountId { get; set; }

		/// <summary>
		/// Gets or sets the chart description.
		/// </summary>
		/// <value>
		/// The chart description.
		/// </value>
		public string ChartDescription { get; set; }

		/// <summary>
		/// Gets or sets the type of the chart currency.
		/// </summary>
		/// <value>
		/// The type of the chart currency.
		/// </value>
		public ChartDetailCurrencyType? ChartCurrencyType { get; set; }

		/// <summary>
		/// Gets or sets the chart currency identifier.
		/// </summary>
		/// <value>
		/// The chart currency identifier.
		/// </value>
		public string ChartCurrencyId { get; set; }

		/// <summary>
		/// Gets or sets the chart posting.
		/// </summary>
		/// <value>
		/// The chart posting.
		/// </value>
		public string ChartPosting { get; set; }

		/// <summary>
		/// Gets or sets the type of the chart.
		/// </summary>
		/// <value>
		/// The type of the chart.
		/// </value>
		public string ChartType { get; set; }

		/// <summary>
		/// Gets or sets the chart header identifier.
		/// </summary>
		/// <value>
		/// The chart header identifier.
		/// </value>
		public string ChartHeaderId { get; set; }

		/// <summary>
		/// Gets or sets the chart detail parent identifier.
		/// </summary>
		/// <value>
		/// The chart detail parent identifier.
		/// </value>		
		public string ChartDetailParentId { get; set; }

		/// <summary>
		/// Gets or sets the chart detail identifier.
		/// </summary>
		/// <value>
		/// The chart detail identifier.
		/// </value>
		public string ChartDetailId { get; set; }

		/// <summary>
		/// Gets the chart account display.
		/// </summary>
		/// <value>
		/// The chart account display.
		/// </value>
		public string Text
		{
			get
			{
				if (!string.IsNullOrEmpty(AccountId) && !string.IsNullOrEmpty(ChartDescription))
				{
					return string.Join(" - ", AccountId, ChartDescription);
				}
				return null;
			}
		}

		public string Id
		{
			get
			{
				return AccountId;
			}
		}

		/// <summary>
		/// Gets or sets the is bank account.
		/// </summary>
		/// <value>
		/// The is bank account.
		/// </value>
		public bool IsBankAccount { get; set; }
	}
}
