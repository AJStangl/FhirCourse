using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace FhirCourse.Services
{
    public class FhirServices : IFhirServices
    {
        public Patient CreatePatient()
        {
            var patient = new Patient
            {
                Meta = new Meta
                {
                    LastUpdatedElement = new Instant(DateTimeOffset.Now)
                },
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        ElementId = "116",
                        SystemElement =
                            new FhirUri("https://courses.hl7fundamentals.org/campus/user/profile.php?id=116"),
                        Use = Identifier.IdentifierUse.Official,
                        ValueElement = new FhirString("Alfred.Stangl")
                    }
                },
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated
                },
                Name = new List<HumanName>
                {
                    new HumanName
                    {
                        FamilyElement = new FhirString("Stangl"),
                        GivenElement = new List<FhirString>
                        {
                            new FhirString("Alfred")
                        },
                        PrefixElement = new List<FhirString>
                        {
                            new FhirString("Mr.")
                        }
                    }
                },
                GenderElement = new Code<AdministrativeGender>(AdministrativeGender.Male),
                Telecom = new List<ContactPoint>
                {
                    new ContactPoint
                    {
                        Use = ContactPoint.ContactPointUse.Home,
                        ValueElement = new FhirString("(03) 5555 6789")
                    }
                },
                Address = new List<Address>
                {
                    new Address
                    {
                        CityElement = new FhirString("Ann Harbor"),
                        CountryElement = new FhirString("US"),
                        LineElement = new List<FhirString> {new FhirString("3300 Washtenaw")},
                        PostalCodeElement = new FhirString("48104"),
                        StateElement = new FhirString("MI"),
                        DistrictElement = new FhirString("Washtenaw")
                    }
                },
                ActiveElement = new FhirBoolean(true),
                BirthDateElement = new Date(1988, 03, 06)
            };
            return patient;
        }
    }
}