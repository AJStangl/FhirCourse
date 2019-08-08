using Hl7.Fhir.Model;

namespace FhirCourse.Services.FhirServices
{
    public interface IFhirServices
    {
        Patient CreatePatient();
        Bundle CreateTransaction();
    }
}