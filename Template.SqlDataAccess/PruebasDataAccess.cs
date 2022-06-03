namespace Template.SqlDataAccess
{
    using Template.Models;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Template.Common.DataAccess;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Template.SqlDataAccess.Entities;
    using Template.Common.Providers;

    /// <summary>
    /// Data access implementation for Azure Storage
    /// </summary>
    public class PruebasDataAccess :  BaseDataAccess, IPruebasDataAccess
    {
        private readonly ILogger<PruebasDataAccess> logger;
        private readonly ISessionProvider sessionProvider;


        /// <summary>
        /// Gets the connection string from the configuration
        /// </summary>
        /// <param name="configuration"></param>
        public PruebasDataAccess(IConfiguration configuration, IDatabaseConnection<DatabaseContext> connection, 
            ISessionProvider sessionProvider, ILogger<PruebasDataAccess> logger) : base(connection)
        {
            this.logger = logger;
            this.sessionProvider = sessionProvider;
        }


        /// <inheritdoc/>  
        public async Task<Prueba> AddUpdatePruebaAsync(Prueba prueba)
        {
            this.logger.LogInformation("Executing PruebasDataAccess.AddUpdatePruebaAsync");
            var entity = PruebaEntity.FromModel(prueba);
            entity.Fecha = DateTimeOffset.Now;
            await this.DatabaseContext.Pruebas.Upsert(entity).RunAsync();
            return prueba;
        }


        /// <inheritdoc/>   
        public async Task<List<Prueba>> LoadPruebasAsync()
        {
            this.logger.LogInformation("Executing PruebasDataAccess.LoadPruebasAsync");
            var pruebas = await this.DatabaseContext.Pruebas.Where(x=>!x.Deleted).Select(x => x.ToModel()).ToListAsync();
            return pruebas;
        }


        /// <inheritdoc/>   
        public async Task<Prueba> LoadPruebaAsync(Guid pruebaId)
        {
            this.logger.LogInformation("Executing PruebasDataAccess.LoadPruebaAsync");
            var prueba = await this.DatabaseContext.Pruebas.FirstOrDefaultAsync(x=>x.PruebaId == pruebaId);
            return prueba?.ToModel();
        }


        /// <inheritdoc/>   
        public async Task<bool> DeletePruebaAsync(Guid pruebaId)
        {
            this.logger.LogInformation("Executing PruebasDataAccess.DeletePruebaAsync");
            var prueba = await this.DatabaseContext.Pruebas.FirstOrDefaultAsync(x => x.PruebaId == pruebaId);
            if (prueba != null)
            {
                prueba.Fecha = DateTimeOffset.Now;
                prueba.Deleted = true;
                await this.DatabaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
