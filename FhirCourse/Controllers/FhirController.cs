using System;
using System.Threading.Tasks;
using FhirCourse.Extensions;
using FhirCourse.Services;
using FhirCourse.Services.FhirServices;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Mvc;

namespace FhirCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FhirController : ControllerBase
    {
        private readonly IFhirServices _fhirServices;
        private readonly IClient _client;

        public FhirController(IFhirServices fhirServices, IClient client)
        {
            _fhirServices = fhirServices;
            _client = client;
        }

        [HttpGet]
        [Route("[Action]")]
        public ActionResult<string> MakePatient()
        {
            Patient patient = _fhirServices.CreatePatient();
            return Ok(patient.ToJson());
        }

        [HttpGet, Route("[Action]")]
        public ActionResult<string> GetPatient()
        {
            FhirClient fhirClient = _client.ConfigureBasicClient("http://fhir.hl7fundamentals.org/r4/");
            Resource patientResource = fhirClient.Get("Patient/50672");
            return Ok(patientResource.ToJson());
        }

        [HttpGet, Route("[Action]")]
        public ActionResult<string> UpdatePatient()
        {
            FhirClient fhirClient = _client.ConfigureBasicClient("http://fhir.hl7fundamentals.org/r4/");
            Resource patientResource = fhirClient.Update(_fhirServices.CreatePatient());
            return Ok(patientResource.ToJson());
        }

        [HttpGet]
        [Route("[Action]")]
        public ActionResult<string> MakeTransaction([FromHeader] int minutes = 0)
        {
            Bundle bundle = _fhirServices.CreateTransaction();
            return bundle.ToXml();
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<string>> GetPsPatient()
        {
            FhirClient fhirClient = await _client.ConfigureClient();
            try
            {
                Resource patient = await fhirClient.GetAsync("/Patient");
                return patient.ToJson();
            }
            catch (Exception exception)
            {
                return OperationOutcome.ForException(exception,
                    OperationOutcome.IssueType.Processing,
                    OperationOutcome.IssueSeverity.Fatal).ToJson();
            }
        }

        [HttpGet]
        [Route("[Action]")]
        public ActionResult<string> GetBerk()
        {
            return Ok(_fhirServices.BerzerkistanPatient().ToXml());
        }
    }
}