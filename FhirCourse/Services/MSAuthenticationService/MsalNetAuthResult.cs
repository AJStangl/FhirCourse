using Microsoft.Identity.Client;

namespace FhirCourse.Services.MSAuthenticationService
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
}