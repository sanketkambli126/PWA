using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.DataAttributes;

namespace PWAFeaturesRnd.Helper
{
	public static class EnumsHelper
	{
        public static string GetEnumItemFromKeyValue(Type enumType, string keyValue)
        {

            if (!enumType.IsEnum)
            {
                throw new ApplicationException("GetKeyValue does not support non-enum types");
            }

            var fields = enumType.GetFields();

            for (int x = 0; x < fields.Count(); x++)
            {
                var attributes = (EnumValueDataAttribute[])fields[x].GetCustomAttributes(typeof(EnumValueDataAttribute), false);
                if (attributes.Length > 0 && attributes[0].KeyValue == keyValue)
                {
                    return fields[x].Name;
                }
            }

            return null;

            //if (returnValue == null)
            //{
            //    throw new ApplicationException("Value not found for enumeration item " + keyValue + " , within Enum: " + enumType.Name);
            //}

        }

        public static string GetEnumNameFromKeyValue(Type enumType, string keyValue)
        {

            if (!enumType.IsEnum)
            {
                throw new ApplicationException("GetKeyValue does not support non-enum types");
            }

            var fields = enumType.GetFields();

            for (int x = 0; x < fields.Count(); x++)
            {
                var attributes = (EnumValueDataAttribute[])fields[x].GetCustomAttributes(typeof(EnumValueDataAttribute), false);
                if (attributes.Length > 0 && attributes[0].KeyValue == keyValue)
                {
                    return attributes[0].Name;
                }
            }

            return null;

            //if (returnValue == null)
            //{
            //    throw new ApplicationException("Value not found for enumeration item " + keyValue + " , within Enum: " + enumType.Name);
            //}

        }


        public static string GetEnumItemFromDescription(Type enumType, string description)
        {
            string returnValue = null;

            if (!enumType.IsEnum)
            {
                throw new ApplicationException("GetKeyValue does not support non-enum types");
            }

            var fields = enumType.GetFields();

            for (int x = 0; x < fields.Count(); x++)
            {
                var attributes = (EnumValueDataAttribute[])fields[x].GetCustomAttributes(typeof(EnumValueDataAttribute), false);
                if (attributes.Length > 0 && attributes[0].Name == description)
                {
                    return fields[x].Name;
                }
            }


            if (returnValue == null)
            {
                throw new ApplicationException("Value not found for enumeration item " + description + " , within Enum: " + enumType.Name);
            }

            return returnValue;

        }

        /// <summary>
        /// Gets the descriptions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Collection of KeyValuePair objects</returns>
        public static IEnumerable<KeyValuePair<T, string>> GetEnumDataSource<T>()
            where T : struct, IConvertible
        {
            Array enumValues = Enum.GetValues(typeof(T));
            List<KeyValuePair<T, string>> descriptions = new List<KeyValuePair<T, string>>(enumValues.Length);
            var e = enumValues.GetEnumerator();
            while (e.MoveNext())
            {
                T enumItem = (T)(e.Current);
                descriptions.Add(new KeyValuePair<T, string>(enumItem, GetEnumMetadata<T, EnumValueDataAttribute>(enumItem).Name));
            }

            return descriptions;
        }



        /// <summary>
        /// Returns the first instance of metadata asociated to an enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <typeparam name="TMetadata">The metadata type to find.</typeparam>
        /// <param name="value">The enum value.</param>
        /// <returns>The metadata associated to the enum.</returns>
        public static TMetadata GetEnumMetadata<TEnum, TMetadata>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            return fi.GetCustomAttributes(typeof(TMetadata), false).Cast<TMetadata>().FirstOrDefault();
        }

        public static string GetKeyValue<T>(T enumItemValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var metaData = GetEnumMetadata<T, EnumValueDataAttribute>(enumItemValue);

            return metaData.KeyValue;

        }


        public static string GetDescription<T>(T enumItemValue) where T : struct, IConvertible
        {

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var metaData = GetEnumMetadata<T, EnumValueDataAttribute>(enumItemValue);

            return metaData.Name;

        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }


        public static string GetErrorCodeValue<T>(T enumItemValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var metaData = GetEnumMetadata<T, EnumExceptionDataAttribute>(enumItemValue);

            return metaData.Code;

        }


        public static string GetErrorMessageFromValue<T>(T enumItemValue) where T : struct, IConvertible
        {

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var metaData = GetEnumMetadata<T, EnumExceptionDataAttribute>(enumItemValue);

            return metaData.Message;

        }

        public static bool HasValidEnumById<T>(string id) where T : struct, IConvertible
        {
            return !string.IsNullOrWhiteSpace(id) && GetEnumItemFromKeyValue(typeof(T), id) != null;
        }

        public static T GetEnumById<T>(string id) where T : struct, IConvertible
        {
            try
            {
                return (T)Enum.Parse(typeof(T), EnumsHelper.GetEnumItemFromKeyValue(typeof(T), id));
            }
            catch (Exception e)
            {
                throw new ApplicationException("GetKeyValue does not support non-enum types");
            }
        }
    }
}
