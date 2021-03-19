using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using BabyTech.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyTech.Services
{
    public class DB
    {

        private DynamoDBContext context;

        public async Task CreatePatient(Patient newPatient)
        {
            await context.SaveAsync(newPatient);
        }

        public async Task<Patient> ReadPatient(string patientID)
        {
            try
            {
                Patient patient = await context.LoadAsync<Patient>(patientID);
                return patient;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task UpdatePatient(Patient updatedPatient)
        {
            try
            {
                Patient patient = await context.LoadAsync<Patient>(updatedPatient.PatientID);
                patient = updatedPatient;
                await context.SaveAsync(patient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task DeletePatient(string patientID)
        {
            try
            {
                Patient patient = await context.LoadAsync<Patient>(patientID);
                await context.DeleteAsync(patient);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DB()
        {
            // Connect to DynamoDB
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            AWS.IdentityPoolId, // Your identity pool ID
            Amazon.RegionEndpoint.USEast1 // Region
            );

            // Create a DynamoDB context
            try
            {
                var client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
                this.context = new DynamoDBContext(client);
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message);  }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

    }
}
