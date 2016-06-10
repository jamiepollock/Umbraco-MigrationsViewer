using System;
using System.Globalization;
using System.Net.Http.Formatting;
using Our.Umbraco.MigrationsViewer.Core.Services;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;

namespace Our.Umbraco.MigrationsViewer.Core.Controllers
{
    [UmbracoApplicationAuthorize(Constants.Applications.Developer)]
    [PluginController(PluginConstants.Name)]
    [Tree(Constants.Applications.Developer, PluginConstants.Name, PluginConstants.Title)]
    public class MigrationsViewerTreeController : TreeController
    {
        private const string MigrationNameTreeNodeIcon = "icon-tactics";
        private readonly IMigrationNamesService _service = new DatabaseMigrationNamesService(ApplicationContext.Current.DatabaseContext.Database);

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            if (IsRoot(id) == false)
            {
                throw new NotSupportedException();
            }

            var migrationNames = _service.GetUniqueMigrationNames();

            foreach (var name in migrationNames)
            {
                var node = CreateTreeNode(name, id, null, name, MigrationNameTreeNodeIcon, false);
                nodes.Add(node);
            }
            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (IsRoot(id) == false) return menu;

            var text = ApplicationContext.Services.TextService.Localize(ActionRefresh.Instance.Alias,
                CultureInfo.CurrentUICulture);
            menu.Items.Add<RefreshNode, ActionRefresh>(text);

            return menu;
        }

        private static bool IsRoot(string id)
        {
            return string.Equals(id, Constants.System.Root.ToString());
        }
    }
}