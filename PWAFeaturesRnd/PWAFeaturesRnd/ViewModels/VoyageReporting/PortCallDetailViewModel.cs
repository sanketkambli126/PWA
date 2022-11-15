using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Port Call Detail View Model
	/// </summary>
	public class PortCallDetailViewModel
	{
		/// <summary>
		/// Gets or sets the name of the charter.
		/// </summary>
		/// <value>
		/// The name of the charter.
		/// </value>
		public string CharterName { get; set; }

		/// <summary>
		/// Gets or sets the voyage number.
		/// </summary>
		/// <value>
		/// The voyage number.
		/// </value>
		public string VoyageNumber { get; set; }

		/// <summary>
		/// Gets or sets the charter number.
		/// </summary>
		/// <value>
		/// The charter number.
		/// </value>
		public string CharterNumber { get; set; }

		/// <summary>
		/// Gets or sets the trade.
		/// </summary>
		/// <value>
		/// The trade.
		/// </value>
		public string Trade { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>
		/// The port.
		/// </value>
		public string Port { get; set; }

		/// <summary>
		/// Gets or sets the CHT company code.
		/// </summary>
		/// <value>
		/// The CHT company code.
		/// </value>
		public string ChtCompanyCode { get; set; }

		/// <summary>
		/// Gets or sets the eosp date header.
		/// </summary>
		/// <value>
		/// The eosp date header.
		/// </value>
		public string EospDateHeader { get; set; }

		/// <summary>
		/// Gets or sets the faop date header.
		/// </summary>
		/// <value>
		/// The faop date header.
		/// </value>
		public string FaopDateHeader { get; set; }

		/// <summary>
		/// Gets or sets the berth date header.
		/// </summary>
		/// <value>
		/// The berth date header.
		/// </value>
		public string BerthDateHeader { get; set; }

		/// <summary>
		/// Gets or sets the un berth date header.
		/// </summary>
		/// <value>
		/// The un berth date header.
		/// </value>
		public string UnBerthDateHeader { get; set; }

		/// <summary>
		/// Gets or sets the cargo operation time.
		/// </summary>
		/// <value>
		/// The cargo operation time.
		/// </value>
		public string CargoOperationTime { get; set; }

		/// <summary>
		/// Gets or sets the outofservice time.
		/// </summary>
		/// <value>
		/// The outofservice time.
		/// </value>
		public string OutofserviceTime { get; set; }

		/// <summary>
		/// Gets or sets the berth time.
		/// </summary>
		/// <value>
		/// The berth time.
		/// </value>
		public string BerthTime { get; set; }

		/// <summary>
		/// Gets or sets the total time.
		/// </summary>
		/// <value>
		/// The total time.
		/// </value>
		public string TotalTime { get; set; }

		/// <summary>
		/// Gets or sets the eosp.
		/// </summary>
		/// <value>
		/// The eosp.
		/// </value>
		public string EOSP { get; set; }

		/// <summary>
		/// Gets or sets the berthed.
		/// </summary>
		/// <value>
		/// The berthed.
		/// </value>
		public string Berthed { get; set; }

		/// <summary>
		/// Gets or sets the un berthed.
		/// </summary>
		/// <value>
		/// The un berthed.
		/// </value>
		public string UnBerthed { get; set; }

		/// <summary>
		/// Gets or sets the faop.
		/// </summary>
		/// <value>
		/// The faop.
		/// </value>
		public string Faop { get; set; }

		/// <summary>
		/// Gets or sets the eosp.
		/// </summary>
		/// <value>
		/// The eosp.
		/// </value>
		public DateTime? EOSPDate { get; set; }

		/// <summary>
		/// Gets or sets the berthed date.
		/// </summary>
		/// <value>
		/// The berthed date.
		/// </value>
		public DateTime? BerthedDate { get; set; }

		/// <summary>
		/// Gets or sets the un berthed date.
		/// </summary>
		/// <value>
		/// The un berthed date.
		/// </value>
		public DateTime? UnBerthedDate { get; set; }

		/// <summary>
		/// Gets or sets the faop date.
		/// </summary>
		/// <value>
		/// The faop date.
		/// </value>
		public DateTime? FaopDate { get; set; }

	}
}
