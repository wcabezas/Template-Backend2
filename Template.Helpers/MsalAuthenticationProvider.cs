using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace Template.Helpers.Helpers
{

    /// <summary>
    /// Encapsulates the details of getting a token from MSAL and exposes it via the 
    /// IAuthenticationProvider interface so that GraphServiceClient or AuthHandler can use it.    
    /// </summary>
    public class MsalAuthenticationProvider : IAuthenticationProvider
    {
        private string[] scopes;
        private IConfidentialClientApplication clientApplication;

        public MsalAuthenticationProvider(IConfidentialClientApplication clientApplication, string[] scopes)
        {
            this.clientApplication = clientApplication;
            this.scopes = scopes;
        }


        /// <summary>
        /// Update HttpRequestMessage with credentials
        /// </summary>
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            var token = await GetTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        /// <summary>
        /// Acquire Token 
        /// </summary>
        public async Task<string> GetTokenAsync()
        {
            try
            {
                AuthenticationResult? authResult = null;
                authResult = await clientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                return authResult.AccessToken;
            }
            catch
            {
                throw;
            }
        }
    }
}
