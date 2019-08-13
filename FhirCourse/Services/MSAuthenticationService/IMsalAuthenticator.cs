using System.Threading.Tasks;

namespace FhirCourse.Services.MSAuthenticationService
{
    public interface IMsalAuthenticator
    {
        /// <summary>
        /// Asynchronously attempts to retrieve an authentication token
        /// </summary>
        /// <returns>An MsalNetAuthResult, containing either the AuthenticationResult with the retrieved token or the error message from a failure.</returns>
        Task<MsalNetAuthResult> GetTokenAsync();
    }
}