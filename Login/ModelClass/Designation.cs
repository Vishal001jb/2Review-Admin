using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Login.ModelClass
{
    public class Designation
    {
        public string DesignationName { get; set; }
        public string DesignationId { get; set; }
        public Designation()
        {


        }
        public Designation(string t1)
        {
            DesignationName = t1;
            
        }
        public Designation(string t1, string t2)
        {
            DesignationId = t1;
            DesignationName = t2;
        }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        
    }
}