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
    public class SolicitudesDataAccess : BaseDataAccess, ISolicitudesDataAccess
    {
        private readonly ILogger<SolicitudesDataAccess> logger;
        private readonly ISessionProvider sessionProvider;


        /// <summary>
        /// Gets the connection string from the configuration
        /// </summary>
        /// <param name="configuration"></param>
        public SolicitudesDataAccess(IConfiguration configuration, IDatabaseConnection<DatabaseContext> connection,
            ISessionProvider sessionProvider, ILogger<SolicitudesDataAccess> logger) : base(connection)
        {
            this.logger = logger;
            this.sessionProvider = sessionProvider;
        }


        /// <inheritdoc/>  
        public async Task<Solicitud> AddUpdateSolicitudAsync(Solicitud solicitud)
        {
            this.logger.LogInformation("Executing SolicitudesDataAccess.AddUpdateSolicitudAsync");
            var entity = SolicitudEntity.FromModel(solicitud);
            entity.FechaModificacion = DateTimeOffset.Now;
            await this.DatabaseContext.Solicitudes.Upsert(entity).RunAsync();
            return solicitud;
        }


        /// <inheritdoc/>   
        public async Task<List<Solicitud>> LoadSolicitudesAsync()
        {
            this.logger.LogInformation("Executing SolicitudesDataAccess.LoadSolicitudesAsync");
            var solicitudes = await this.DatabaseContext.Solicitudes.Where(x => !x.Desactivado).Select(x => x.ToModel()).ToListAsync();
            return solicitudes;
        }


        /// <inheritdoc/>   
        public async Task<Solicitud> LoadSolicitudAsync(Guid solicitudId)
        {
            this.logger.LogInformation("Executing SolicitudesDataAccess.LoadSolicitudAsync");
            var solicitud = await this.DatabaseContext.Solicitudes.FirstOrDefaultAsync(x => x.IdSolicitud == solicitudId);
            return solicitud?.ToModel();
        }


        /// <inheritdoc/>   
        public async Task<bool> DeleteSolicitudAsync(Guid solicitudId)
        {
            this.logger.LogInformation("Executing SolicitudesDataAccess.DeleteSolicitudAsync");
            var solicitud = await this.DatabaseContext.Solicitudes.FirstOrDefaultAsync(x => x.IdSolicitud == solicitudId);
            if (solicitud != null)
            {
                solicitud.FechaModificacion = DateTimeOffset.Now;
                solicitud.Desactivado = true;
                await this.DatabaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
