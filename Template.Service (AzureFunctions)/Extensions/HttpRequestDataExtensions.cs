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
    private static async Task<HttpResponseData> CreateResponseAsync(this HttpRequestData request, Result result, Action<Response> setResponseLinks)
    {
        var responseData = request.CreateResponse(result.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        var response = new Response(result.Success, result.Message);
        if (setResponseLinks != null) setResponseLinks(response);
        await responseData.WriteAsJsonAsync(response).ConfigureAwait(false);
        return responseData;
    }


    /// <summary>
    /// Creates a http response based on the result
    /// </summary>    
    private static async Task<HttpResponseData> CreateResponseAsync<TResult>(this HttpRequestData request, Result<TResult> result, Action<Response<TResult>> setResponseLinks)
    {
        var responseData = request.CreateResponse(result.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        var response = new Response<TResult>(result.Success, result.Data, result.Message);
        if (setResponseLinks != null) setResponseLinks(response);
        await responseData.WriteAsJsonAsync(response).ConfigureAwait(false);
        return responseData;
    }



    /// <summary>
    /// Returns a HTTP response after executing a method with no parameters
    /// </summary>   
    public static async Task<HttpResponseData> CreateResponse<TResult>(this HttpRequestData request, Func<Task<Result<TResult>>> func, Action<Response<TResult>> setResponseLinks = null)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func();
        return await CreateResponseAsync(request, result, setResponseLinks);
    }


    /// <summary>
    /// Returns a HTTP response after executing a method with 1 parameters
    /// </summary>   
    public static async Task<HttpResponseData> CreateResponse<T>(this HttpRequestData request,  Func<T, Task<Result>> func, T param, Action<Response> setResponseLinks = null)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func(param);
        return await CreateResponseAsync(request, result, setResponseLinks);
    }



    /// <summary>
    /// Returns a HTTP response after executing a method with 1 parameters
    /// </summary>   
    public static async Task<HttpResponseData> CreateResponse<T, TResult>(this HttpRequestData request, Func<T, Task<Result<TResult>>> func, T param, Action<Response<TResult>> setResponseLinks = null)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func(param);
        return await CreateResponseAsync(request, result, setResponseLinks);
    }


    /// <summary>
    /// Returns a HTTP response after executing a method with 2 parameters
    /// </summary>     
    public static async Task<HttpResponseData> CreateResponse<T1, T2, TResult>(this HttpRequestData request,  Func<T1, T2, Task<Result<TResult>>> func, T1 param1, T2 param2, Action<Response<TResult>> setResponseLinks = null)
    {
        var logger = request.FunctionContext.GetLogger(request.FunctionContext.FunctionDefinition.Name);
        logger?.LogInformation($"Executing {request.FunctionContext.FunctionDefinition.Name}");
        var result = await func(param1, param2);
        return await CreateResponseAsync(request, result, setResponseLinks);
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
