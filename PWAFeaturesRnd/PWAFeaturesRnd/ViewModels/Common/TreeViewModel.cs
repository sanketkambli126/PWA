using Newtonsoft.Json;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Common
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TreeViewModel<T>
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the tooltip.
		/// </summary>
		/// <value>
		/// The tooltip.
		/// </value>
		public string Tooltip { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TreeViewModel{T}"/> is expanded.
		/// </summary>
		/// <value>
		///   <c>true</c> if expanded; otherwise, <c>false</c>.
		/// </value>
		public bool Expanded { get; set; }

		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<TreeViewModel<T>>? Children { get; set; }

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public string Key { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TreeViewModel{T}"/> is lazy.
		/// </summary>
		/// <value>
		///   <c>true</c> if lazy; otherwise, <c>false</c>.
		/// </value>
		public bool Lazy { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="TreeViewModel{T}"/> is checkbox.
		/// </summary>
		/// <value>
		///   <c>true</c> if checkbox; otherwise, <c>false</c>.
		/// </value>
		public bool Checkbox { get; set; }

		/// <summary>
		/// Gets or sets the additional data.
		/// </summary>
		/// <value>
		/// The additional data.
		/// </value>
		public T AdditionalData { get; set; }
	}
}