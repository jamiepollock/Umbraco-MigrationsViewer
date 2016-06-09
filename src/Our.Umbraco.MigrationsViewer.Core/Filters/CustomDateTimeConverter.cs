using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Umbraco.Core;

namespace Our.Umbraco.MigrationsViewer.Core.Filters
{
    internal class CustomDateTimeConverter : IsoDateTimeConverter
    {
        private readonly string _dateTimeFormat;

        public CustomDateTimeConverter(string dateTimeFormat)
        {
            Mandate.ParameterNotNullOrEmpty(dateTimeFormat, "dateTimeFormat");
            _dateTimeFormat = dateTimeFormat;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(_dateTimeFormat));
        }
    }
}
