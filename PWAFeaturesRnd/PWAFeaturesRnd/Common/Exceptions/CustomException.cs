using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Common.Exceptions
{
	public class CustomException : Exception
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomException" /> class.
		/// </summary>
		public CustomException()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public CustomException(string message) : base(message)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		public CustomException(string message, Exception? innerException)
			: base(message, innerException)
		{

		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>
		/// The category.
		/// </value>
		public ExceptionCategory Category { get; set; }

		/// <summary>
		/// Gets or sets the business exception detail.
		/// </summary>
		/// <value>
		/// The detail.
		/// </value>
		public BusinessExceptionDetail? BusinessExceptionDetail { get; set; }

		/// <summary>
		/// Gets or sets the detail messaage.
		/// </summary>
		/// <value>
		/// The detail messaage.
		/// </value>
		public string DetailMessaage { get; set; }

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>
		/// The name of the file.
		/// </value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the parameter details.
		/// </summary>
		/// <value>
		/// The parameter details.
		/// </value>
		public string MethodDetails { get; set; }

		#endregion
	}
}
