using System.Collections.Generic;

namespace Our.Umbraco.MigrationsViewer.Core.Services
{
    public interface IMigrationNamesService
    {
        IEnumerable<string> GetUniqueMigrationNames();
    }
}