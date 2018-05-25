using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Login.Activity
{
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar",ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Addemployee : AppCompatActivity
    {
        Button MyButton, Form;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Addemployee);
            // Create your application here

            Form = (Button)FindViewById(Resource.Id.Form);
            // MyButton = (Button)FindViewById(Resource.Id.MyButton);


            Intent i;


            Form.Click += delegate
            {
                i = new Intent(this, typeof(newuser));
                StartActivity(i);
            };

            Button MyButton = FindViewById<Button>(Resource.Id.MyButton);
            MyButton.Click += delegate
            {
                LayoutInflater LayoutInflaterAndroid = LayoutInflater.From(this);
                View mView = LayoutInflaterAndroid.Inflate(Resource.Layout.userinput, null);
                Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertdialogbuilder.SetView(mView);


                alertdialogbuilder.SetCancelable(false)
                .SetPositiveButton("SENT", delegate
                {
                    Toast.MakeText(this, "upload file", ToastLength.Short).Show();
                })


                .SetNegativeButton("cancle", delegate
                {
                    alertdialogbuilder.Dispose();

                });
                Android.Support.V7.App.AlertDialog alertDialogandroid = alertdialogbuilder.Create();
                alertDialogandroid.Show();
            };

        }

    }

}