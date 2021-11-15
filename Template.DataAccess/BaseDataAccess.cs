using Microsoft.Extensions.Configuration;
using Template.Common.DataAccess;

namespace Template.DataAccess
{
    /// <summary>
    /// Base class for all database classes
    /// </summary>
    public class BaseDataAccess
    {
        private IDatabaseConnection<DatabaseContext> databaseConnection;

        /// <summary>
        /// DB Context
        /// </summary>
        protected DatabaseContext DatabaseContext => databaseConnection.DatabaseContext;


        /// <summary>
        /// Receives by DI the service configuration (appsettings.json)
        /// </summary>
        /// <param name="configuration"></param>
        public BaseDataAccess(IDatabaseConnection<DatabaseContext> databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }


        /// <summary>
        /// Opens the database
        /// </summary>
        public void OpenDatabase()
        {
            this.databaseConnection.OpenDatabase();
        }


        /// <summary>
        /// Cloees the databse
        /// </summary>
        public void CloseDatabase()
        {
            this.databaseConnection.CloseDatabase();
        }
    }
}
