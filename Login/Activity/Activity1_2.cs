using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Login.Activity
{
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity1 : AppCompatActivity
    {
        EditText editTextUsername, editTextPassword;
        TextInputLayout textInputLayoutUsername, textInputLayoutPassword;
        Button buttonLogin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Activity1);
            findAllView();

            buttonLogin.Click += check_validation;



        }

        private void findAllView()
        {
            buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);
            editTextUsername = FindViewById<EditText>(Resource.Id.editTextUsername);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            textInputLayoutPassword = FindViewById<TextInputLayout>(Resource.Id.textInputLayoutPassword);
            textInputLayoutUsername = FindViewById<TextInputLayout>(Resource.Id.textInputLayoutUserName);
        }
        private void check_validation(object sender, EventArgs e)
        {
            if (editTextUsername.Text.Contains("abc@1rivet.com"))
            {
                if (editTextPassword.Text.Equals("123456"))
                {

                    Toast.MakeText(this, "Login", ToastLength.Short).Show();
                    editTextUsername.Text = "";
                    editTextPassword.Text = "";

                    StartActivity(new Intent(this, typeof(Navigtion)));
                }
                else if (editTextUsername.Text.Length != 0 || editTextUsername.Text.Length < 6)
                {
                    //editTextUsername.SetError("Enter Valid Username", null);
                    Toast.MakeText(this, "Invalid Credential", ToastLength.Short).Show();
                }
                else if (editTextUsername.Text.Length == 0)
                {
                  //  editTextUsername.SetError("Invalid credential", null);
                }

            }
            else if (editTextUsername.Text.Length != 0 || editTextUsername.Text.Length < 12)
            {
                //editTextUsername.SetError("Invalid credential", null);
                Toast.MakeText(this, "Invalid Credential", ToastLength.Short).Show();
            }
            else if (editTextUsername.Text.Length == 0)
            {
                //editTextUsername.SetError("Invalid credential", null);
            }

            //     else
            //    {
            //          editTextUsername.SetError("Enter Valid Username", null);
            //       //  Toast.MakeText(this, "Enter The Valid Password", ToastLength.Short).Show();
            //    }
            // }
            //  else
            //{
            //    editTextPassword.SetError("Enter Valid Password", null);
            //    // Toast.MakeText(this, "Enter The Valid Username", ToastLength.Short).Show();
            //  }


            //    if (editTextUsername.Text.Length < 12)
            //    {
            //        if (editTextUsername.Text.Length == 0)
            //            editTextUsername.SetError("Username Required", null);

            //        else
            //            editTextUsername.SetError("Username must be of Minimum 12 character length", null);
            //    }
            //    else if (editTextPassword.Text.Length < 6)
            //    {
            //        if (editTextPassword.Text.Length == 0)
            //            editTextPassword.SetError("Password Required", null);
            //        else
            //            editTextPassword.SetError("password must be of Minimum 6 character length", null);



        }
    }

}