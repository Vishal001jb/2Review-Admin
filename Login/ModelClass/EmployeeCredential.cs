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
    public class EmployeeCredential
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}