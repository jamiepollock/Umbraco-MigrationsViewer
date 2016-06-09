using System.Collections.Generic;
using Umbraco.Core.Persistence;

namespace Our.Umbraco.MigrationsViewer.Core.Services
{
    internal class DatabaseMigrationNamesService : IMigrationNamesService
    {
        private readonly Database _database;
        public DatabaseMigrationNamesService(Database database)
        {
            _database = database;
        }

        public IEnumerable<string> GetUniqueMigrationNames()
        {
            var sql = new Sql().Select("distinct name").From("umbracoMigration");

            return _database.Fetch<string>(sql);
        }
    }
}
