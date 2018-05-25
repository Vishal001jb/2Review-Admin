using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace Login.ModelClass
{
    public class Reviewee
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Employee_Id { get; set; }
        public string Designation_Id { get; set; }
        public string Round_Id { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }

    }
}