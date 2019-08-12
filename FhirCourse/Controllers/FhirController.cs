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
        private readonly IFhirClient _fhirClient;
        private readonly IClient _client;

        public FhirController(IFhirServices fhirServices, IClient client)
        {
            _fhirServices = fhirServices;
            _client = client;
            _fhirClient =  new FhirClient("http://fhir.hl7fundamentals.org/r4/");
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
            var patient = _fhirClient.Get("Patient/50672");
            return Ok(patient.ToJson());
        }

        [HttpGet, Route("[Action]")]
        public ActionResult<string> UpdatePatient()
        {
            var patient = _fhirClient.Update(_fhirServices.CreatePatient());
            return Ok(patient.ToJson());
        }

        [HttpGet]
        [Route("[Action]")]
        public ActionResult<string> MakeTransaction([FromHeader] int minutes = 0)
        {
            FhirDateTime fhirDateTime = new FhirDateTime(DateTimeOffset.Now.AddMinutes(minutes));
            Bundle bundle = _fhirServices.CreateTransaction();
            return bundle.ToXml();
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<ActionResult<string>> GetPsPatient()
        {
            FhirClient fhirClient = await _client.ConfigureClient();
            Resource patient = await fhirClient.GetAsync("/Patient");
            return patient.ToJson();
        }
    }
}