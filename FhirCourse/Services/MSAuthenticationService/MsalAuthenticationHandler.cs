using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace FhirCourse.Services.MSAuthenticationService
{
    public class MsalAuthenticationHandler : IMsalAuthenticator
    {
        private readonly IConfidentialClientApplication _confidentialClientApplication;
        private readonly string[] _scopes;

        public MsalAuthenticationHandler(IConfiguration config)
        {
            _confidentialClientApplication = ConfidentialClientApplicationBuilder.Create(config["AzureAd:ClientId"])
                .WithClientSecret(config["QuerySecret"])
                .WithAuthority(AzureCloudInstance.AzurePublic, config["AzureAd:TenantId"])
                .Build();
            _scopes = config["AzureFhirScopes"].Split(',');
        }

        public async Task<MsalNetAuthResult> GetTokenAsync()
        {
            IEnumerable<IAccount> accounts = await _confidentialClientApplication.GetAccountsAsync();
            MsalNetAuthResult authRes = new MsalNetAuthResult();

            try
            {
                authRes.Auth = await _confidentialClientApplication
                    .AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    authRes.Auth = await _confidentialClientApplication.AcquireTokenForClient(_scopes)
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