using System.Net;
using System.Threading.Tasks;
using FhirCourse.Services.MSAuthenticationService;
using Hl7.Fhir.Rest;
using Microsoft.Extensions.Options;

namespace FhirCourse.Services
{
    public interface IClient
    {
        Task<FhirClient> ConfigureClient();
    }

    public class Client : IClient
    {
        private readonly IMsalAuthenticator _auth;
        private readonly AzureAd _azureAd;

        public Client(IOptions<AzureAd> azureAd, IMsalAuthenticator msalAuthenticator)
        {
            _azureAd = azureAd.Value;
            _auth = msalAuthenticator;
        }

        public async Task<FhirClient> ConfigureClient()
        {
            FhirClient fhirClient = new FhirClient(_azureAd.FhirServerEndpoint)
            {
                PreferredFormat = ResourceFormat.Json,
                UseFormatParam = true
            };

            MsalNetAuthResult authResult = await _auth.GetTokenAsync();
            fhirClient.OnBeforeRequest += delegate(object sender, BeforeRequestEventArgs e)
            {
                e.RawRequest.Headers.Add(HttpRequestHeader.Accept, "*/*");
                e.RawRequest.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {authResult.Auth.AccessToken}");
            };

            return fhirClient;
        }
    }
}