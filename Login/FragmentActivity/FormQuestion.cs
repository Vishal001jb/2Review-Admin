using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace _2ReviewEmployeeSideHomeScreen.ModelClasses
{
    public class Form_Question
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Form_Id { get; set; }
        public string Question_Id { get; set; }

    }
}