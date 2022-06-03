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
    public class PruebasBusinessLogic : IPruebasBusinessLogic
    {
        private readonly ILogger<PruebasBusinessLogic> logger;
        private readonly IPruebasDataAccess dataAccess;
        private readonly ISessionProvider sessionProvider;

        /// <summary>
        /// Gets by DI the dependeciees
        /// </summary>
        /// <param name="dataAccess"></param>
        public PruebasBusinessLogic(IPruebasDataAccess dataAccess,   ISessionProvider sessionProvider, ILogger<PruebasBusinessLogic> logger)
        {
            this.logger = logger;
            this.dataAccess = dataAccess;
            this.sessionProvider = sessionProvider;
        }


        /// <inheritdoc/>   
        public async Task<Result<Prueba>> AddPruebaAsync(Prueba request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing PruebasBusinessLogic.AddPruebaAsync");                
                request.Fecha = DateTimeOffset.Now;
                var prueba = await dataAccess.AddUpdatePruebaAsync(request);
                return new Result<Prueba>(prueba);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Prueba>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>   
        public async Task<Result<Prueba>> UpdatePruebaAsync(Prueba request)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing PruebasBusinessLogic.UpdatePruebaAsync");
                var prueba = await dataAccess.AddUpdatePruebaAsync(request);
                return new Result<Prueba>(prueba);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Prueba>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>     
        public async Task<Result<Prueba[]>> LoadPruebasAsync()
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing PruebasBusinessLogic.LoadPruebasAsync");
                var pruebas = await dataAccess.LoadPruebasAsync();
                return new Result<Prueba[]>(pruebas.ToArray());
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Prueba[]>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }



        /// <inheritdoc/>     
        public async Task<Result<Prueba>> LoadPruebaAsync(Guid pruebaId)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing PruebasBusinessLogic.LoadPruebaAsync");
                var prueba = await dataAccess.LoadPruebaAsync(pruebaId);
                return new Result<Prueba>(prueba);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.Message);
                return new Result<Prueba>(ex.Message);
            }
            finally
            {
                this.dataAccess.CloseDatabase();
            }
        }


        /// <inheritdoc/>     
        public async Task<Result> DeletePruebaAsync(Guid pruebaId)
        {
            try
            {
                this.dataAccess.OpenDatabase();
                this.logger?.LogInformation("Executing PruebasBusinessLogic.DeletePruebaAsync");
                var result = await dataAccess.DeletePruebaAsync(pruebaId);
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
