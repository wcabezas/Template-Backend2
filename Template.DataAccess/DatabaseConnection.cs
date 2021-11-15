using Microsoft.Extensions.Configuration;
using Template.Common.DataAccess;

namespace Template.DataAccess
{
    /// <summary>
    /// Scoped service to handle the database connection for any layer
    /// </summary>
    public class DatabaseConnection : IDatabaseConnection<DatabaseContext>
    {
        /// <summary>
        /// DB Context
        /// </summary>
        public DatabaseContext DatabaseContext { get; private set; }


        /// <summary>
        /// Configuration (settings)
        /// </summary>
        protected IConfiguration Configuration { get; private set; }


        /// <summary>
        /// Receives by DI the service configuration (appsettings.json)
        /// </summary>
        /// <param name="configuration"></param>
        public DatabaseConnection(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }



        /// <summary>
        /// Opens the database
        /// </summary>
        public void OpenDatabase()
        {
            if (this.DatabaseContext == null)
            {
                this.DatabaseContext = new DatabaseContext(this.Configuration);
                this.DatabaseContext.Database.EnsureCreated();
            }
        }


        /// <summary>
        /// Cloees the databse
        /// </summary>
        public void CloseDatabase()
        {
            if (this.DatabaseContext != null)
            {
                this.DatabaseContext.Dispose();
                this.DatabaseContext = null;
            }
        }
    }
}
