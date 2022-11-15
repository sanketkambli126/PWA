using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Common.DataAttributes
{
    /// <summary>
    /// Enum value data attribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class EnumValueDataAttribute : Attribute
    {
        /// <summary>
        /// The name
        /// </summary>
        private string _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The key value
        /// </summary>
        private string _keyValue;
        /// <summary>
        /// Gets or sets the key value.
        /// </summary>
        /// <value>
        /// The key value.
        /// </value>
        public string KeyValue
        {
            get { return _keyValue; }
            set { _keyValue = value; }
        }

        /// <summary>
        /// The live key value
        /// </summary>
        private string _liveKeyValue;
        /// <summary>
        /// Gets or sets the live key value.
        /// </summary>
        /// <value>
        /// The live key value.
        /// </value>
        public string LiveKeyValue
        {
            get { return _liveKeyValue; }
            set { _liveKeyValue = value; }
        }
    }

    /// <summary>
    /// Enum exception details attribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class EnumExceptionDataAttribute : Attribute
    {
        /// <summary>
        /// The message
        /// </summary>
        private string _message;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// The code
        /// </summary>
        private string _code;
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
    }

    /// <summary>
    /// Enum DataTable Export Button data attribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class EnumDataTableExportButtonAttribute : Attribute
    {
        /// <summary>
        /// The option name
        /// </summary>
        private string _optionName;
        /// <summary>
        /// Gets or sets export option name.
        /// </summary>
        /// <value>
        /// The export option name.
        /// </value>
        public string OptionName
        {
            get { return _optionName; }
            set { _optionName = value; }
        }

        /// <summary>
        /// The button name
        /// </summary>
        private string _name;
        /// <summary>
        /// Gets or sets the button name.
        /// </summary>
        /// <value>
        /// The button name.
        /// </value>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The button title
        /// </summary>
        private string _title;
        /// <summary>
        /// Gets or sets the button title.
        /// </summary>
        /// <value>
        /// The button title.
        /// </value>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// The button css Classes
        /// </summary>
        private string _faClasses;
        /// <summary>
        /// Gets or sets the button css Classes.
        /// </summary>
        /// <value>
        /// The button css Classes.
        /// </value>
        public string FaClasses
        {
            get { return _faClasses; }
            set { _faClasses = value; }
        }
    }

    /// <summary>
	/// Purchase status attribute
	/// </summary>
	/// <seealso cref="System.Attribute" />
	public class PurchaseStatusAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the valid status list.
        /// </summary>
        /// <value>
        /// The valid status list.
        /// </value>
        public PurchaseOrderStatus[] ValidStatusList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseStatusAttribute"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public PurchaseStatusAttribute(params PurchaseOrderStatus[] values)
        {
            this.ValidStatusList = values;
        }

    }

    /// <summary>
    /// Short Code Attribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class ShortCodeAttribute : Attribute
    {
        /// <summary>
        /// The short code
        /// </summary>
        private string _shortCode;

        /// <summary>
        /// Gets or sets the short code.
        /// </summary>
        /// <value>
        /// The short code.
        /// </value>
        public string ShortCode
        {
            get { return _shortCode; }
            set { _shortCode = value; }
        }
    }

    /// <summary>
    /// Recharge Recovery Type
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class RechargeRecoveryType : Attribute
    {

        /// <summary>
        /// The type code
        /// </summary>
        private string _typeCode;

        /// <summary>
        /// Gets or sets the type code.
        /// </summary>
        /// <value>
        /// The type code.
        /// </value>
        public string TypeCode
        {
            get { return _typeCode; }
            set { _typeCode = value; }
        }
    }

    /// <summary>
	/// Claoud Document attribute
	/// </summary>
	/// <seealso cref="System.Attribute" />
	public class CloudDocumentAttribute : Attribute
    {
        /// <summary>
        /// The prefix
        /// </summary>
        private string _prefix;
        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Prefix
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        /// <summary>
        /// The folder
        /// </summary>
        private CloudFolder _folder;
        /// <summary>
        /// Gets or sets the cloud folder.
        /// </summary>
        /// <value>
        /// The cloud folder.
        /// </value>
        public CloudFolder CloudFolder
        {
            get { return _folder; }
            set { _folder = value; }
        }
    }
}
