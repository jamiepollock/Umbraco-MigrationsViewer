using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using Umbraco.Core;

namespace Our.Umbraco.MigrationsViewer.Core.Filters
{
    /// <summary>
    /// Exact replica of an UmbracoCms.Core internal attribute class found here (https://github.com/umbraco/Umbraco-CMS/blob/75c2b07ad3a093b5b65b6ebd45697687c062f62a/src/Umbraco.Web/WebApi/Filters/OutgoingDateTimeFormatAttribute.cs)
    /// </summary>
    internal sealed class OutgoingDateTimeFormatAttribute : Attribute, IControllerConfiguration
    {
        private readonly string _format = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Specify a custom format
        /// </summary>
        /// <param name="format"></param>
        public OutgoingDateTimeFormatAttribute(string format)
        {
            Mandate.ParameterNotNullOrEmpty(format, "format");
            _format = format;
        }

        /// <summary>
        /// Will use the standard ISO format
        /// </summary>
        public OutgoingDateTimeFormatAttribute()
        {

        }

        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            var jsonFormatter = controllerSettings.Formatters.OfType<JsonMediaTypeFormatter>();
            foreach (var r in jsonFormatter)
            {
                r.SerializerSettings.Converters.Add(new CustomDateTimeConverter(_format));
            }
        }
    }
}