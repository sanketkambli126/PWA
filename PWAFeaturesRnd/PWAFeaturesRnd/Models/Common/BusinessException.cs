using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Common
{
	public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        public BusinessException()
        {
            IsDetailSpecified = true;
        }

        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessException(string message) : base(message)
        {
            IsDetailSpecified = false;
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public ExceptionCategory Category { get; set; }
        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        public BusinessExceptionDetail Detail { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is detail specified.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is detail specified; otherwise, <c>false</c>.
        /// </value>
        public bool IsDetailSpecified { get; set; }
    }
}
