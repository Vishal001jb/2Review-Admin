using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Login.Activity
{
    [Activity(Label = "UpdateQuestion")]
    public class UpdateQuestion : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UpdateQuestion);

            String value = Intent.GetStringExtra("UpdateID");

            Toast.MakeText(this,"The id is " + value,ToastLength.Long).Show();
        }
    }
}