using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Helper;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Company details contract
	/// </summary>
	public class CompanyDetails
	{
		/// <summary>
		/// Gets or sets the company identifier.
		/// </summary>
		/// <value>
		/// The identifier of the company.
		/// </value>
		public string CompanyId { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>
		/// The address.
		/// </value>
		public string Address { get; set; }

		/// <summary>
		/// Gets or sets the town.
		/// </summary>
		/// <value>
		/// The town.
		/// </value>
		public string Town { get; set; }

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public string State { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		/// <value>
		/// The country.
		/// </value>
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the postal code.
		/// </summary>
		/// <value>
		/// The postal code.
		/// </value>
		public string PostalCode { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the telephone.
		/// </summary>
		/// <value>
		/// The telephone.
		/// </value>
		public string Telephone { get; set; }

		/// <summary>
		/// Gets or sets the fax.
		/// </summary>
		/// <value>
		/// The fax.
		/// </value>
		public string Fax { get; set; }

		/// <summary>
		/// Gets or sets the type enum.
		/// </summary>
		/// <value>
		/// The type enum.
		/// </value>
		public CompanyTypeEnum? CompanyType { get; set; }

		/// <summary>
		/// Gets or sets the type of company.
		/// </summary>
		/// <value>
		/// The type of company.
		/// </value>
		public string TypeOfCompany { get; set; }

		/// <summary>
		/// Sets the t ype of comapny.
		/// </summary>
		public void SetTYpeOfComapny()
		{
			if (CompanyType.HasValue)
			{
				TypeOfCompany = EnumsHelper.GetDescription(CompanyType.Value);
			}
		}

		/// <summary>
		/// Gets or sets the supplier rating.
		/// </summary>
		/// <value>
		/// The supplier rating.
		/// </value>
		public SupplierRating SupplierRating { get; set; }

		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>
		/// The rating.
		/// </value>
		public string Rating { get; set; }

		/// <summary>
		/// Sets the rating string.
		/// </summary>
		public void SetRatingString()
		{
			Rating = EnumsHelper.GetDescription(SupplierRating);
		}

		/// <summary>
		/// Gets or sets procurement type
		/// </summary>
		/// <value>
		/// The procurement type
		/// </value>
		public string ProcurementType { get; set; }

		/// <summary>
		/// Gets or sets is MARCAS supplier flag
		/// </summary>
		/// <value>
		/// The bool value IsMARCAS
		/// </value>
		public bool IsMARCAS { get; set; }

		/// <summary>
		/// Gets or sets is the company type is Group Company
		/// </summary>
		/// <value>
		/// The bool value IsGroupCompany
		/// </value>
		public bool IsGroupCompany { get; set; }

		/// <summary>
		/// Gets or sets the default currency (for supplier)
		/// </summary>
		/// <value>
		/// The string value for default currency
		/// </value>
		public string DefaultCurrency { get; set; }

		/// <summary>
		/// Gets or sets the top1 currency (for supplier)
		/// </summary>
		/// <value>
		/// The string value for top1 currency
		/// </value>
		public string Top1Currency { get; set; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the mobile.
		/// </summary>
		/// <value>
		/// The mobile.
		/// </value>
		public string Mobile { get; set; }

		/// <summary>
		/// Gets or sets the telex.
		/// </summary>
		/// <value>
		/// The telex.
		/// </value>
		public string Telex { get; set; }

		/// <summary>
		/// Gets or sets the website.
		/// </summary>
		/// <value>
		/// The website.
		/// </value>
		public string Website { get; set; }
	}
}
