using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Template.SqlDataAccess.Entities;

namespace Template.SqlDataAccess
{
    /// <summary>
    /// SQL Data context
    /// </summary>
    public class DatabaseContext : DbContext
    {
        private IConfiguration config;
       
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<PruebaEntity> Pruebas { get; set; }
        public DbSet<SolicitudEntity> Solicitudes { get; set; }


        /// <summary>
        /// EF Core constructor
        /// </summary>
        public DatabaseContext()
        {

        }


        /// <summary>
        /// EF Core constructor
        /// </summary>
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }


        /// <summary>
        /// Contrustor used when a configuration is passed
        /// </summary>
        public DatabaseContext(IConfiguration config)
        {
            this.config = config;
        }


        /// <summary>
        /// Configures the data context
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null) return;
            base.OnConfiguring(optionsBuilder);
            var connectionString = string.Empty;
            if (this.config != null)
            {
                // Setup sqlite (only for testing, not to be used ina project)
                connectionString = config.GetConnectionString("SqLiteDatabase");             
                optionsBuilder.UseSqlite(connectionString);

                // For a real project, remove the SQlite support and configure Azure SQL or CosmosDB
                // connectionString = config.GetConnectionString("SqlDatabase");
                // optionsBuilder.UseSqlServer(connectionString);             
            }


            // This can be used by EF Core when scalfolding SQL
            else
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("connectionString");
                }
            }
        }


        ///// <summary>
        ///// Seeds the BD
        ///// </summary>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Use hasdata to pre-propulate the database     
        //}
    }
}
