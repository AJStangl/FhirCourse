using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace FhirCourse.Services
{
    public class MsalAuthenticationHandler : IMsalAuthenticator
    {
        private readonly IConfidentialClientApplication _conApp;
        private readonly string[] _scopes;

        public MsalAuthenticationHandler(IConfiguration config)
        {
            _conApp = ConfidentialClientApplicationBuilder.Create(config["AzureAd:ClientId"])
                .WithClientSecret(config["QuerySecret"])
                .WithAuthority(AzureCloudInstance.AzurePublic, config["AzureAd:TenantId"])
                .Build();
            _scopes = config["AzureFhirScopes"].Split(',');
        }
        public async Task<MsalNetAuthResult> GetTokenAsync()
        {
            var accounts = await _conApp.GetAccountsAsync();
            var authRes = new MsalNetAuthResult();

            try
            {
                authRes.Auth = await _conApp.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    authRes.Auth = await _conApp.AcquireTokenForClient(_scopes)
                        .ExecuteAsync();
                }
                catch (Exception ex) when (ex is MsalUiRequiredException || ex is MsalServiceException)
                {
                    authRes.Error = ex.Message;
                }
            }
            catch (Exception ex)
            {
                authRes.Error = ex.Message;
            }

            return authRes;
        }
    }
}