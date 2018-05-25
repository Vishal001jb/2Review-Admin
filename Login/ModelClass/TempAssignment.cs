using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Login.ModelClass
{
    public class TempAssignment
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Reviewable_Id { get; set; }
        public string Reviewee_Id { get; set; }
        public string Form_Id { get; set; }

    }
}