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
    public class Round
    {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Status { get; set; }

        public string RoundDate { get; set; }

        public string RoundTime { get; set; }

        public string Round_Name { get; set; }

        public int Reviewee { get; set; }

        public int Reviewable { get; set; }

        public int Progress { get; set; }

    }
}