using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Our.Umbraco.MigrationsViewer.Core.Filters;
using Our.Umbraco.MigrationsViewer.Core.Models;
using Umbraco.Web.WebApi;

namespace Our.Umbraco.MigrationsViewer.Core.Controllers
{
    [OutgoingDateTimeFormat]
    [JsonCamelCaseFormatter]
    public class MigrationsViewerApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public IEnumerable<MigrationItem> Get(string productName)
        {
            var items = Services.MigrationEntryService.GetAll(productName);

            if (items.Any())
            {
                return items.Select(x => new MigrationItem()
                {
                    CreateDate = x.CreateDate,
                    Version = x.Version.ToString()
                });
            }

            return null;
        }
    }
}
