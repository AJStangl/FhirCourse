using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace FhirCourse.Services.FhirServices
{
    public class FhirServices : IFhirServices
    {
//        private const string PatientId = "50671";
        private const string PatientId = "Alfred.Stangl";
        private const string PatientName = "Alfred";

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
                        ValueElement = new FhirString(PatientId)
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

        public Bundle CreateTransaction()
        {
            Bundle bundle = new Bundle();
            bundle.Type = Bundle.BundleType.Transaction;
            // Add Organization Resource
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.PUT,
                    UrlElement = new FhirUri($"Organization?identifier=www.nationalorgidentifier.gov/ids|UWEARME_{PatientId}")
                },
                Resource = GetOrganization(),
                FullUrlElement = new FhirUri("urn:uuid:17C7D86E-664F-4FE2-91D7-AF9A8E47311E")
            });
            // Add Patient Resource
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.PUT,
                    UrlElement = new FhirUri($"Patient?identifier=www.mypatientidentifier.com/ids|{PatientId}")
                },
                Resource = GetPatient(),
                FullUrlElement = new FhirUri("urn:uuid:0fc374a1-a226-4552-9683-55dd510e67c9")
            });
            // Add Device
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.PUT,
                    UrlElement = new FhirUri($"Device?identifier=www.uwearme.com/dev|{PatientId}")
                },
                Resource = GetDevice(),
                FullUrlElement = new FhirUri("urn:uuid:01ba878a-e49d-4bac-b629-9de3fcb7e83a")
            });

            AddObservations(bundle, GetDate());
            AddObservations(bundle, GetDate(15));
            AddObservations(bundle, GetDate(30));
            return bundle;
        }

        private void AddObservations(Bundle bundle, FhirDateTime fhirDateTime)
        {
            // Add Observation Systolic BP
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.POST,
                    UrlElement = new FhirUri("Observation")
                },
                Resource = GetObservation("Systolic BP", 120, "mmHg", fhirDateTime, "8480-6", PatientId)
            });
            // Add Observation Diastolic BP
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.POST,
                    UrlElement = new FhirUri("Observation")
                },
                Resource = GetObservation("Diastolic BP", 80, "mmHg", fhirDateTime, "8462-4", PatientId)
            });
            // Add Observation Heart Rate
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.POST,
                    UrlElement = new FhirUri("Observation")
                },
                Resource = GetObservation("Heart Rate", 60, "/min", fhirDateTime, "8867-4", PatientId)
            });
            // Add Observation Respiratory Rate
            bundle.Entry.Add(new Bundle.EntryComponent
            {
                Request = new Bundle.RequestComponent
                {
                    Method = Bundle.HTTPVerb.POST,
                    UrlElement = new FhirUri("Observation")
                },
                Resource = GetObservation("Respiratory Rate", 20, "/min", fhirDateTime, "9279-1", PatientId)
            });
        }

        private Organization GetOrganization()
        {
            return new Organization
            {
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div =
                        "<div xmlns=\"http://www.w3.org/1999/xhtml\"> UWEARME - A DIVISION OF HEALTH GIZMOS CORP </div>"
                },
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        SystemElement = new FhirUri("www.nationalorgidentifier.gov/ids"),
                        ValueElement = new FhirString($"UWEARME_{PatientId}")
                    }
                },
                NameElement = new FhirString("UWEARME"),
                Address = new List<Address>
                {
                    new Address
                    {
                        LineElement = new List<FhirString> {new FhirString("2000 WEARABLE DRIVE")},
                        CityElement = new FhirString("ANN ARBOR"),
                        StateElement = new FhirString("MI"),
                        CountryElement = new FhirString("US")
                    }
                }
            };
        }

        private Patient GetPatient()
        {
            return new Patient
            {
                Text = new Narrative
                {
                    Div = $"<div xmlns=\"http://www.w3.org/1999/xhtml\">{PatientName} Id # {PatientId} </div>"
                },
                Identifier = new List<Identifier> {new Identifier
                {
                    SystemElement = new FhirUri("www.mypatientidentifier.com/ids"),
                    ValueElement = new FhirString(PatientId)
                }},
                Name = new List<HumanName>
                {
                    new HumanName
                    {
                        FamilyElement = new FhirString("Stangl"),
                        GivenElement = new List<FhirString>
                        {
                            new FhirString("Alfred")
                        }
                    }
                },
                Address = new List<Address>
                {
                    new Address
                    {
                        LineElement = new List<FhirString> {new FhirString("3300 Washtenaw")},
                        CityElement = new FhirString("Ann Harbor"),
                        StateElement = new FhirString("MI"),
                        CountryElement = new FhirString("US")
                    }
                },
                ManagingOrganization = new ResourceReference("urn:uuid:17C7D86E-664F-4FE2-91D7-AF9A8E47311E")
            };
        }

        private Device GetDevice()
        {
            return new Device
            {
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns=\"http://www.w3.org/1999/xhtml\"> UW Device {PatientId}</div>"
                },
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        SystemElement = new FhirUri("www.uwearme.com/dev"),
                        ValueElement = new FhirString(PatientId)
                    }
                },
                ExpirationDateElement = new FhirDateTime("2020-10-10"),
                LotNumberElement = new FhirString("22222"),
                ModelNumberElement = new FhirString("u888800-1"),
                Type = new CodeableConcept
                {
                    Coding = new List<Coding>
                    {
                        new Coding
                        {
                            SystemElement = new FhirUri("http://snomed.info/sct"),
                            CodeElement = new Code("33894003"),
                            DisplayElement = new FhirString("Experimental Device")
                        }
                    }
                },
                Patient = new ResourceReference("urn:uuid:0fc374a1-a226-4552-9683-55dd510e67c9"),
                Owner = new ResourceReference("urn:uuid:17C7D86E-664F-4FE2-91D7-AF9A8E47311E")
            };
        }

        private Observation GetObservation(string description, decimal value, string unit, FhirDateTime date, string lonicCode, string patientId)
        {
            return new Observation
            {
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns=\"http://www.w3.org/1999/xhtml\"> UW Device for Pat # {patientId}\n{description}: {value} {unit} on {date}</div>"
                },
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        SystemElement = new FhirUri("www.uwearme.com/measures"),
                        ValueElement = new FhirString($"{patientId}-{date}-{lonicCode}")
                    }
                },
                Status = ObservationStatus.Final,
                Code = new CodeableConcept
                {
                    Coding = new List<Coding>
                    {
                        new Coding
                        {
                            SystemElement = new FhirUri("http://loinc.org"),
                            CodeElement = new Code($"{lonicCode}"),
                            DisplayElement = new FhirString($"{description}")
                        }
                    }
                },
                Subject = new ResourceReference("urn:uuid:0fc374a1-a226-4552-9683-55dd510e67c9"),
                Effective = date,
                Value = new SimpleQuantity
                {
                    ValueElement = new FhirDecimal(value),
                    UnitElement = new FhirString(unit),
                    SystemElement = new FhirUri("http://unitsofmeasure.org")
                },
                Device = new ResourceReference("urn:uuid:01ba878a-e49d-4bac-b629-9de3fcb7e83a")
            };
        }

        private FhirDateTime GetDate(int minutes = 0)
        {
            return new FhirDateTime(DateTimeOffset.Now.AddMinutes(minutes));
        } 
    }
}