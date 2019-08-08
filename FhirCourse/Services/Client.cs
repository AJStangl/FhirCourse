using System;
using System.Collections.Generic;
using System.Net.Http;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace FhirCourse.Services
{
    public interface IClient
    {
        System.Threading.Tasks.Task<Task> GetPatient();
    }

    public class Client : IClient
    {
        private readonly string _fhirServerBaseAddr;
        private readonly IMsalAuthenticator _auth;
        public Client(IConfiguration config, IMsalAuthenticator msalAuthenticator)
        {
            _fhirServerBaseAddr = config["FhirServerEndpoint"];
            _auth = msalAuthenticator;
        }


        public async System.Threading.Tasks.Task<Task> GetPatient()
        {
            FhirClient fhirClient = new FhirClient(_fhirServerBaseAddr);
            fhirClient.PreferredFormat = ResourceFormat.Json;
            fhirClient.UseFormatParam = true;
            MsalNetAuthResult authResult = await _auth.GetTokenAsync();
            fhirClient.OnBeforeRequest += (object sender, BeforeRequestEventArgs e) =>
            {
                e.RawRequest.Headers.Add("Accept", "*/*");
                e.RawRequest.Headers.Add("Authorization", $"Bearer {authResult.Auth.AccessToken}");
            };
            var foo = fhirClient.Get("/Patient");
            return null;
        }
        private HttpRequestMessage CreateRequestMessage(AuthenticationResult token)
        {
            var msg = new HttpRequestMessage();
            msg.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
            msg.RequestUri = new Uri($"{_fhirServerBaseAddr}Patient");
            return msg;
        }
    }
}