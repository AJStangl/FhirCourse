using FhirCourse.Extensions;
using FhirCourse.Services;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;

namespace FhirCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FhirController : ControllerBase
    {
        private readonly IFhirServices _fhirServices;

        public FhirController(IFhirServices fhirServices)
        {
            _fhirServices = fhirServices;
        }

        [HttpGet]
        [Route("patient")]
        public ActionResult<string> GetPatient()
        {
            Patient patient = _fhirServices.CreatePatient();
            return patient.ToJson();
        }
    }
}