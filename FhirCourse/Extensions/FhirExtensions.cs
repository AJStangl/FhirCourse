using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace FhirCourse.Extensions
{
    public static class FhirExtensions
    {
        private static FhirJsonSerializer _fhirJsonSerializer = new FhirJsonSerializer {Settings = {Pretty = true}};
        private static FhirXmlSerializer _fhirXmlSerializer = new FhirXmlSerializer {Settings = {Pretty = true}};

        public static string ToJson<T>(this T genericFhirResource)
        {
            try
            {
                return _fhirJsonSerializer.SerializeToString(genericFhirResource as Base);
            }
            catch (Exception e)
            {
                return _fhirJsonSerializer.SerializeToString(
                    instance: OperationOutcome.ForException(e, OperationOutcome.IssueType.Processing));
            }
        }

        public static string ToXml<T>(this T genericFhirResource)
        {
            try
            {
                return _fhirXmlSerializer.SerializeToString(genericFhirResource as Base);
            }
            catch (Exception e)
            {
                return _fhirXmlSerializer.SerializeToString(
                    instance: OperationOutcome.ForException(e, OperationOutcome.IssueType.Processing));
            }
        }
    }
}