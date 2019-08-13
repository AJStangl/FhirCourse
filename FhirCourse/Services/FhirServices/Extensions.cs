using System.Collections.Generic;
using Hl7.Fhir.Model;

namespace FhirCourse.Services.FhirServices
{
    public class PatientTaxSituation
    {
        public ValueSet PatientTaxSituationValueSet()
        {
            return new ValueSet
            {
                Text = new Narrative
                {
                    StatusElement = new Code<Narrative.NarrativeStatus>(Narrative.NarrativeStatus.Generated),
                },
                UrlElement = new FhirUri("http://fhir.hl7fundamentals.org/berzerkistan/ValueSet/PatientTaxSituation"),
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        SystemElement =
                            new FhirUri("http://fhir.hl7fundamentals.org/berzerkistan/identifiers/valuesets"),
                        ValueElement = new FhirString("PatientTaxSituation")
                    }
                },
                VersionElement = new FhirString("20151120"),
                NameElement = new FhirString("Berzerkistan PPH Tax Situation - 20151120"),
                StatusElement = new Code<PublicationStatus>(PublicationStatus.Draft),
                ExperimentalElement = new FhirBoolean(true),
                PublisherElement = new FhirString("Berzerkistan Healthcare Ministry"),
                Contact = new List<ContactDetail>
                {
                    new ContactDetail
                    {
                        NameElement = new FhirString("Professor Allan Karim Von Mannenheim"),
                        Telecom = new List<ContactPoint>
                        {
                            new ContactPoint
                            {
                                SystemElement =
                                    new Code<ContactPoint.ContactPointSystem>(ContactPoint.ContactPointSystem.Other),
                                ValueElement = new FhirString("http://bhm.org.bz/fhir")
                            }
                        }
                    }
                },
                Description = new Markdown("Berzerkistan PPH Tax Situation - 20151120"),
                // Extensible?
                Compose = new ValueSet.ComposeComponent
                {
                    Include = new List<ValueSet.ConceptSetComponent>
                    {
                        new ValueSet.ConceptSetComponent
                        {
                            SystemElement =
                                new FhirUri(
                                    "http://fhir.hl7fundamentals.org/berzerkistan/CodeSystem/PatientTaxSituation"),
                            Concept = new List<ValueSet.ConceptReferenceComponent>
                            {
                                new ValueSet.ConceptReferenceComponent
                                    {CodeElement = new Code("U"), Display = "UNCOVERED"},
                                new ValueSet.ConceptReferenceComponent
                                    {CodeElement = new Code("V"), Display = "VOLUNTARY"},
                                new ValueSet.ConceptReferenceComponent
                                    {CodeElement = new Code("M"), Display = "MANDATED"},
                            }
                        }
                    }
                }
            };
        }

        public CodeSystem PatientTaxSituationCodeSystem()
        {
            return new CodeSystem
            {
                NameElement = new FhirString("Berzerkistan PPH Tax Situation - 20151120"),
                Meta = new Meta
                {
                    ProfileElement = new List<Canonical>
                    {
                        new Canonical
                        {
                            Value = "http://hl7.org/fhir/StructureDefinition/shareablecodesystem"
                        }
                    }
                },
                UrlElement = new FhirUri("http://fhir.hl7fundamentals.org/berzerkistan/CodeSystem/PatientTaxSituation"),
                VersionElement = new FhirString("2"),
                StatusElement = new Code<PublicationStatus>(PublicationStatus.Draft),
                Contact = new List<ContactDetail>
                {
                    new ContactDetail
                    {
                        Telecom = new List<ContactPoint>
                        {
                            new ContactPoint
                            {
                                SystemElement =
                                    new Code<ContactPoint.ContactPointSystem>(ContactPoint.ContactPointSystem.Other),
                                ValueElement = new FhirString("http://bhm.org.bz/fhir")
                            }
                        },
                        NameElement = new FhirString("Professor Allan Karim Von Mannenheim")
                    }
                },
                Description = new Markdown("Code System PPH Table Berzerkistan PPH Patient Tax Situation"),
                CompositionalElement = new FhirBoolean(false),
                Content = CodeSystem.CodeSystemContentMode.Complete,
                Concept = new List<CodeSystem.ConceptDefinitionComponent>
                {
                    new CodeSystem.ConceptDefinitionComponent
                    {
                        CodeElement = new Code("U"),
                        DisplayElement = new FhirString("Uncovered"),
                        DefinitionElement = new FhirString("Patient pays 21%")
                    },
                    new CodeSystem.ConceptDefinitionComponent
                    {
                        CodeElement = new Code("V"),
                        DisplayElement = new FhirString("Voluntary"),
                        DefinitionElement = new FhirString("Patient pays 10.5%")
                    },
                    new CodeSystem.ConceptDefinitionComponent
                    {
                        CodeElement = new Code("M"),
                        DisplayElement = new FhirString("Mandated"),
                        DefinitionElement = new FhirString("Patient pays 0%")
                    }
                },
                Count = 4,
                Text = new Narrative
                {
                    StatusElement = new Code<Narrative.NarrativeStatus>(Narrative.NarrativeStatus.Generated),
                    Div =
                        "<div xmlns=\"http://www.w3.org/1999/xhtml\"><h2>Berzerkistan PPH Tax Situation - 20151120</h2><tt>http://fhir.hl7fundamentals.org/berzerkistan/CodeSystem/PatientTaxSituation</tt><p>Code System PPH Table Berzerkistan PPH Patient Tax Situation</p></div>"
                },
                ValueSet = "http://fhir.hl7fundamentals.org/berzerkistan/CodeSystem/PatientTaxSituation",
                CaseSensitiveElement = new FhirBoolean(false),
                VersionNeededElement = new FhirBoolean(false),
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        SystemElement =
                            new FhirUri("http://fhir.hl7fundamentals.org/berzerkistan/identifiers/codesystems"),
                        ValueElement = new FhirString("PatientTaxSituation")
                    }
                },
                DateElement = new FhirDateTime(2015, 11, 20),
                PublisherElement = new FhirString("Berzerkistan Healthcare Ministry"),
                ExperimentalElement = new FhirBoolean(true),
                IdElement = new Id("e5749167-db9a-4f5c-a8f2-4406312b3fa5")
            };
        }
    }
}