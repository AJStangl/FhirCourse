using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace FhirCourse.Services
{
    public class MsalNetAuthResult
    {
        /// <summary>
        /// A successfully retrieved authentication token
        /// </summary>
        public AuthenticationResult Auth { get; set; }
        /// <summary>
        /// An error message, so that the caller can see what's gone wrong
        /// </summary>
        public string Error { get; set; }
    }
    public interface IMsalAuthenticator
    {
        /// <summary>
        /// Asynchronously attempts to retrieve an authentication token
        /// </summary>
        /// <returns>An MsalNetAuthResult, containing either the AuthenticationResult with the retrieved token or the error message from a failure.</returns>
        Task<MsalNetAuthResult> GetTokenAsync();
    }
}