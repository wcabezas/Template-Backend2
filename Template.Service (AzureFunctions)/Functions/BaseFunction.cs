namespace Template.Service.Functions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Template.Models;

    /// <summary>
    /// Base function 
    /// </summary>
    public class BaseFunction
    {
        protected ILogger logger;


        /// <summary>
        /// Gets by DI the depedencies
        /// </summary>
        /// <param name="logger"></param>
        public BaseFunction(ILogger logger)
        {
            this.logger = logger;
        }


        /// <summary>
        /// Execute a business logic method with no parameters
        /// </summary>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="func">Method to execute</param>
        /// <returns>OkObjectResult with a Response<TResult></returns>
        public async Task<IActionResult> Execute<TResult>(Func<Task<Result<TResult>>> func)
        {
            try
            {
                var result = await func();
                if (result.Success)
                {
                    return new OkObjectResult(new Response<TResult>(true, result.Data, result.Message));
                }
                else
                {
                    return new BadRequestObjectResult(new Response<TResult>(false, result.Data, result.Message));
                }
            }
            catch (Exception ex)
            {
                return InternalServiceError(ex);
            }
        }



        /// <summary>
        /// Execute a business logic method that requires parameters and return a simple result (non typed)
        /// </summary>
        /// <typeparam name="TRequest">Request type</typeparam>
        /// <param name="request">Request data</param>
        /// <param name="func">Method to execute</param>
        /// <returns>OkObjectResult with a Response</returns>
        public async Task<IActionResult> Execute<TRequest>(Func<TRequest, Task<Result>> func, TRequest request) 
        {
            try
            {
                var result = await func(request);
                if (result.Success)
                {
                    return new OkObjectResult(new Response(true, result.Message));
                }
                else
                {
                    return new BadRequestObjectResult(new Response(false, result.Message));
                }
            }
            catch (Exception ex)
            {
                return InternalServiceError(ex);
            }
        }


        /// <summary>
        /// Execute a business logic method that requieres parameters and return a typed result
        /// </summary>
        /// <typeparam name="TRequest">Request type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="request">Request data</param>
        /// <param name="func">Method to execute</param>
        /// <returns>OkObjectResult with a Response<TResult></returns>
        public async Task<IActionResult> Execute<TRequest, TResult>(Func<TRequest, Task<Result<TResult>>> func, TRequest request) 
        {
            try
            {
                var result = await func(request);
                if (result.Success)
                {
                    return new OkObjectResult(new Response<TResult>(result.Data));
                }
                else
                {
                    return new BadRequestObjectResult(new Response<TResult>(false, result.Data, result.Message));
                }
            }
            catch (Exception ex)
            {
                return InternalServiceError(ex);
            }
        }


        /// <summary>
        /// Execute a business logic method that doens't requieres parameters and return a simple result
        /// </summary>
        /// <typeparam name="TResult">Result class</typeparam>
        /// <param name="func">Method to execute</param>
        /// <returns>OkObjectResult with a Response</returns>
        public async Task<IActionResult> Execute<TResult>(Func<Task<Result>> func)
        {
            try
            {
                var result = await func();
                if (result.Success)
                {
                    return new OkObjectResult(new Response(true, result.Message));
                }
                else
                {
                    return new BadRequestObjectResult(new Response(false, result.Message));
                }
            }
            catch (Exception ex)
            {
                return InternalServiceError(ex);
            }
        }


        /// <summary>
        /// Returns and Internal Server error response
        /// </summary>
        private IActionResult InternalServiceError(Exception ex)
        {
            this.logger?.LogError(ex.Message, ex);
            var response = new ObjectResult(ex.Message)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            return response;
        }


        /// <summary>
        /// Strem base constructor
        /// </summary>
        /// <param name="stream"></param>
        public T DeserializeBody<T>(Stream stream) where T : class
        {
            if (stream == null)
            {
                return null;
            }
            else
            {
                var body = new StreamReader(stream).ReadToEnd();
                if (!string.IsNullOrEmpty(body))
                {
                    if (typeof(T) == typeof(string))
                    {
                        return body as T;
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<T>(body);
                    }
                }
            }
            return null;
        }       

    }
}