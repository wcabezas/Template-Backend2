using Template.Common.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Template.Service.Extensions
{
   // <summary>
    /// HttpRequestData extentions methods
    /// to create the HTTP Response
    /// </summary>
    public static class HttpRequestDataExtensions
{

    /// <summary>
    /// Creates a http response based on the result
    /// </summary>    
    private static async Task<HttpResponseData> CreateResponseAsync(this HttpRequestData request, Result result)
    {
        var response = request.CreateResponse(result.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        await response.WriteAsJsonAsync(new Response(result.Success, result.Message)).ConfigureAwait(false);
        return response;
    }


    /// <summary>
    /// Creates a http response based on the result
    /// </summary>    
    private static async Task<HttpResponseData> CreateResponseAsync<TResult>(this HttpRequestData request, Result<TResult> result)
    {
        var response = request.CreateResponse(result.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        await response.WriteAsJsonAsync(new Response<TResult>(result.Success, result.Data, result.Message)).ConfigureAwait(false);
        return response;
    }



    /// <summary>
    /// Returns a HTTP response after executing a method with no parameters
    /// </summary>   
    public static async Task<HttpResponseData> CreateResponse<TResult>(this HttpRequestData request, Func<Task<Result<TResult>>> func)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func();
        return await CreateResponseAsync(request, result);
    }

    /// <summary>
    /// Returns a HTTP response after executing a method with 1 parameters
    /// </summary>   
    public static async Task<HttpResponseData> CreateResponse<T>(this HttpRequestData request, Func<T, Task<Result>> func, T param)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func(param);
        return await CreateResponseAsync(request, result);
    }



    /// <summary>
    /// Returns a HTTP response after executing a method with 1 parameters
    /// </summary>   
    public static async Task<HttpResponseData> CreateResponse<T, TResult>(this HttpRequestData request, Func<T, Task<Result<TResult>>> func, T param)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func(param);
        return await CreateResponseAsync(request, result);
    }


    /// <summary>
    /// Returns a HTTP response after executing a method with 2 parameters
    /// </summary>     
    public static async Task<HttpResponseData> CreateResponse<T1, T2, TResult>(this HttpRequestData request, Func<T1, T2, Task<Result<TResult>>> func, T1 param1, T2 param2)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func(param1, param2);
        return await CreateResponseAsync(request, result);
    }


    /// <summary>
    /// Deserializes the boby of the HttpRequestData
    /// </summary>
    public static T DeserializeBody<T>(this HttpRequestData request) where T : class
    {
        if (request.Body == null)
        {
            return null;
        }
        else
        {
            var body = new StreamReader(request.Body).ReadToEnd();
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
