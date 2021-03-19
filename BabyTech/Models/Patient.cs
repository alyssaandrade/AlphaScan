using Amazon.DynamoDBv2.DataModel;
using System;
namespace BabyTech.Models
{

    [DynamoDBTable("Patients")]
    public class Patient
    {
        [DynamoDBHashKey]
        public string PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string HospitalName { get; set; }
        public string Observations { get; set; }
        public bool BirthComplications { get; set; }
        public bool GeneticDisorders { get; set; }
        public bool ChromosomalAbnormalities { get; set; }
        public bool SiblingMedicalIssues { get; set; }
        public bool FamilyMedicalIssues { get; set; }
        public bool SurgeryHistory { get; set; }
        public bool Allergies { get; set; }
        public bool MotherOver35 { get; set; }
        public bool FatherOver40 { get; set; }
        public bool PreviousMiscarraige { get; set; }
        public bool Exposure { get; set; }
        public bool DrugsOrAlcohol { get; set; }
        public bool Diabetes { get; set; }
        public bool AccutaneUse { get; set; }

        public Patient()
        {
        }
    }
}
