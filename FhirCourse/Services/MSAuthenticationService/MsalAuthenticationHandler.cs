using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace FhirCourse.Services.MSAuthenticationService
{
    public class MsalAuthenticationHandler : IMsalAuthenticator
    {
        private const string DummyString = "DummyString";
        private static readonly string DummyGuid = Guid.Empty.ToString();
        private readonly IConfidentialClientApplication _confidentialClientApplication;
        private readonly AzureAd _azureAd;

        public MsalAuthenticationHandler(IOptions<AzureAd> azureAd)
        {
            _azureAd = azureAd?.Value;
            try
            {
                _confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(_azureAd?.ClientId ?? DummyString)
                    .WithClientSecret(_azureAd?.ClientSecret ?? DummyString)
                    .WithAuthority(AzureCloudInstance.AzurePublic, _azureAd?.TenantId ?? DummyGuid)
                    .Build();
            }
            catch (Exception exception)
            {
                _confidentialClientApplication = null;
            }
        }

        public async Task<MsalNetAuthResult> GetTokenAsync()
        {
            IEnumerable<IAccount> accounts = await _confidentialClientApplication.GetAccountsAsync();
            MsalNetAuthResult msalNetAuthResult = new MsalNetAuthResult();

            try
            {
                msalNetAuthResult.Auth = await _confidentialClientApplication
                    .AcquireTokenSilent(_azureAd.Scopes, accounts?.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    msalNetAuthResult.Auth = await _confidentialClientApplication
                        .AcquireTokenForClient(_azureAd.Scopes)
                        .ExecuteAsync();
                }
                catch (Exception exception) when (exception is MsalUiRequiredException ||
                                                  exception is MsalServiceException)
                {
                    msalNetAuthResult.Error = exception.Message;
                }
            }
            catch (Exception exception)
            {
                msalNetAuthResult.Error = exception.Message;
            }

            return msalNetAuthResult;
        }
    }
}