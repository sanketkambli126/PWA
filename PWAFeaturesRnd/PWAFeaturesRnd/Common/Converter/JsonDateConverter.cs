using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Common.Converter
{
    /// <summary>
    /// used for converting date for viewmodels 
    /// </summary>
    /// <seealso cref="System.Text.Json.Serialization.JsonConverter{System.DateTime}" />
    public class JsonDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
       => DateTime.ParseExact(reader.GetString(),
                    "DD MMM YYYY", CultureInfo.InvariantCulture);


        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
       => writer.WriteStringValue(value.ToString(
                    "dd-MM-yyyy", CultureInfo.InvariantCulture));
    }
}
