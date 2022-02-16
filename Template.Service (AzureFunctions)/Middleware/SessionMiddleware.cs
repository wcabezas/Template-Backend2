using Template.Common.Providers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Service.Middleware
{
    /// <summary>
    /// Middleware to capture and store session inforamtion (like authentication headers)
    /// so all layer can access the sesion information through the SessionProvider
    /// </summary>
    public class SessionMiddleware : IFunctionsWorkerMiddleware
    {
        /// <summary>
        /// Called when the middleware is used.
        /// The scoped service is injected into Invoke
        /// </summary>
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {

            try
            {
                if (context.BindingContext.BindingData is IReadOnlyDictionary<string, object> bindingData && bindingData.ContainsKey("headers"))
                {
                    // Takes the username form the header or uses a default one
                    // On a real project the username should be mandatory and if not present return a BadRequest error
                    var defaultUsername = "admin";
                    var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(bindingData["headers"].ToString());
                    var sessionProvider = context.InstanceServices.GetRequiredService<ISessionProvider>();
                    sessionProvider.Setup(headers.ContainsKey("Username") ? headers["Username"].ToString() : defaultUsername);
                }
                await next(context);
            }
            catch (Exception ex)
            {
                var logger = context.GetLogger<SessionMiddleware>();
                logger?.LogError(ex, ex.Message);
            }
        }
    }
}
