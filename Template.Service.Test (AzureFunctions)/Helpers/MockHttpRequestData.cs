using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace Template.Service.Tests.Helpers
{
    /// <summary>
    /// Mock request data to pass to the Azure Function
    /// Needed in order to fake the FunctionContext (that's not avaible when running the test)
    /// </summary>
    public class MockHttpRequestData : HttpRequestData
    {


        public override Stream Body { get; } = new MemoryStream();


        public override HttpHeadersCollection Headers { get; } = new HttpHeadersCollection();


        public override IReadOnlyCollection<IHttpCookie> Cookies { get; }


        public override Uri Url { get; }


        public override IEnumerable<ClaimsIdentity> Identities { get; }


        public override string Method { get; }


        public MockHttpRequestData(FunctionContext functionContext, Uri url, Stream body) : base(functionContext)
        {
            Url = url;
            Body = body ?? new MemoryStream();
            Method = string.Empty;
            Identities = new List<ClaimsIdentity>();
            Cookies = new List<HttpCookie>();
        }


        public override HttpResponseData CreateResponse()
        {
            return new MockHttpResponseData(FunctionContext);
        }


        /// </summary>
        /// <returns></returns>
        public static MockHttpRequestData Create()
        {
            var context = new Mock<FunctionContext>();
            var request = new MockHttpRequestData(
                            context.Object,
                            new Uri("https://localhost"),
                            new MemoryStream());

            return request;
        }



        /// <summary>
        /// Creates an http request with a typed body
        /// </summary>
        public static MockHttpRequestData Create<T>(T body)
        {
            var data = JsonConvert.SerializeObject(body);
            var context = new Mock<FunctionContext>();
            var request = new MockHttpRequestData(
                            context.Object,
                            new Uri("https://localhost"),
                            new MemoryStream(Encoding.ASCII.GetBytes(data)));
            return request;
        }

    }
}
