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

            if (this.config != null)
            {
                var connectionString = config.GetConnectionString("SqlConnectionString");              
                optionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                if (!optionsBuilder.IsConfigured)
                {
                    // This is used by EF Core when scalfolding
                    optionsBuilder.UseSqlServer(@"Server=.;Initial Catalog=.;Persist Security Info=False;User ID=.;Password=.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                }
            }
        }


        ///// <summary>
        ///// Seeds the BD
        ///// </summary>
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Use hasdata to pre-propulate the db     
        //}
    }
}
