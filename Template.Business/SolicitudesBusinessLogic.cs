namespace Template.BusinessLogic
{
    using Template.Models;
    using System.Threading.Tasks;
    using Template.Common.BusinessLogic;
    using Template.Common.DataAccess;
    using Microsoft.Extensions.Logging;
    using System;
    using Template.Common.Models;
    using Template.Common.Providers;

    /// <summary>
    /// Items Business Logic methods
    /// </summary>
    public class SolicitudesBusinessLogic : ISolicitudesBusinessLogic
    {
        private readonly ILogger<SolicitudesBusinessLogic> logger;
        private readonly ISolicitudesDataAccess dataAccess;
        private readonly ISessionProvider sessionProvider;

        /// <summary>
        /// Gets by DI the dependeciees
        /// </summary>
        /// <param name="dataAccess"></param>
        public SolicitudesBusinessLogic(ISolicitudesDataAccess dataAccess, ISessionProvider sessionProvider, ILogger<SolicitudesBusinessLogic> logger)
        {
            this.logger = logger;
            this.dataAccess = dataAccess;
            this.sessionProvider = sessionProvider;
        }


        /// <inheritdoc/>   
        public async Task<Result<Solicitud>> AddSolicitudAsync(Solicitud request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing SolicitudesBusinessLogic.AddSolicitudAsync");
                request.FechaCreacion = DateTimeOffset.Now;
                var solicitud = await dataAccess.AddUpdateSolicitudAsync(request);
                return new Result<Solicitud>(solicitud);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Solicitud>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>   
        public async Task<Result<Solicitud>> UpdateSolicitudAsync(Solicitud request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing SolicitudesBusinessLogic.UpdateSolicitudAsync");
                var solicitud = await dataAccess.AddUpdateSolicitudAsync(request);
                return new Result<Solicitud>(solicitud);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Solicitud>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>     
        public async Task<Result<Solicitud[]>> LoadSolicitudesAsync()
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing SolicitudesBusinessLogic.LoadSolicitudesAsync");
                var solicitudes = await dataAccess.LoadSolicitudesAsync();
                return new Result<Solicitud[]>(solicitudes.ToArray());
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Solicitud[]>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }



        /// <inheritdoc/>     
        public async Task<Result<Solicitud>> LoadSolicitudAsync(Guid solicitudId)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing SolicitudesBusinessLogic.LoadSolicitudAsync");
                var solicitud = await dataAccess.LoadSolicitudAsync(solicitudId);
                return new Result<Solicitud>(solicitud);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Solicitud>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>     
        public async Task<Result> DeleteSolicitudAsync(Guid solicitudId)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing SolicitudesBusinessLogic.DeleteSolicitudAsync");
                var result = await dataAccess.DeleteSolicitudAsync(solicitudId);
                return new Result(result);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result(false, ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }
    }
}
