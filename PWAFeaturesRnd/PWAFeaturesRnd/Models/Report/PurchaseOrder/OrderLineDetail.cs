namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Order Line Detail
	/// </summary>
	public class OrderLineDetail
	{
		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }
		/// <summary>
		/// Gets or sets the name of the part.
		/// </summary>
		/// <value>
		/// The name of the part.
		/// </value>
		public string PartName { get; set; }
		/// <summary>
		/// Gets or sets the makers reference.
		/// </summary>
		/// <value>
		/// The makers reference.
		/// </value>
		public string MakersReference { get; set; }
		/// <summary>
		/// Gets or sets the drawing position.
		/// </summary>
		/// <value>
		/// The drawing position.
		/// </value>
		public string DrawingPosition { get; set; }
		/// <summary>
		/// Gets or sets the uom.
		/// </summary>
		/// <value>
		/// The uom.
		/// </value>
		public string UOM { get; set; }
		/// <summary>
		/// Gets or sets the rob.
		/// </summary>
		/// <value>
		/// The rob.
		/// </value>
		public int? ROB { get; set; }
		/// <summary>
		/// Gets or sets the req.
		/// </summary>
		/// <value>
		/// The req.
		/// </value>
		public int? REQ { get; set; }
		/// <summary>
		/// Gets or sets the enq.
		/// </summary>
		/// <value>
		/// The enq.
		/// </value>
		public int? ENQ { get; set; }
		/// <summary>
		/// Gets or sets the ord.
		/// </summary>
		/// <value>
		/// The ord.
		/// </value>
		public int? ORD { get; set; }
		/// <summary>
		/// Gets or sets the record.
		/// </summary>
		/// <value>
		/// The record.
		/// </value>
		public int? REC { get; set; }

		/// <summary>
		/// Gets or sets the item no.
		/// </summary>
		/// <value>
		/// The item no.
		/// </value>
		public int ItemNo { get; set; }
	}
}
