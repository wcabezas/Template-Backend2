using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Template.Service.Tests.Helpers
{
    /// <summary>
    /// Mock response data to receive from the the Azure Function
    /// Needed in order to fake the FunctionContext (that's not avaible when running the test)
    /// </summary>
    public class MockHttpResponseData : HttpResponseData
    {
        public override HttpStatusCode StatusCode { get; set; }
        
        public override HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();
        
        public override Stream Body { get; set; } = new MemoryStream();
        
        public override HttpCookies Cookies { get; }

        public MockHttpResponseData(FunctionContext functionContext) : base(functionContext)
        {
            Cookies = new Mock<HttpCookies>().Object;
        }
    }


    /// <summary>
    /// Extention method to deserialize
    /// </summary>
    public static class HttpRequestDataExtensions
    {
        public async static Task<T> DeserializeAsync<T>(this HttpResponseData response)
        {
            response.Body.Position = 0;
            var reader = new StreamReader(response.Body);
            var responseBody = await reader.ReadToEndAsync();
            var content = JsonConvert.DeserializeObject<T>(responseBody);
            return content;
        }
    }


}
