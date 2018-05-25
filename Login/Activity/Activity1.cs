using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Login.ModelClass;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.Activity
{
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity1 : AppCompatActivity
    {
        EditText editTextUsername, editTextPassword;
        TextInputLayout textInputLayoutUsername, textInputLayoutPassword;
        Button buttonLogin;
        Android.App.ProgressDialog progress;

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
        private async void check_validation(object sender, EventArgs e)
        {

            if (editTextUsername.Text.Length == 0 || editTextPassword.Text.Length == 0)
            {
                Toast.MakeText(this, "Invalid Login", ToastLength.Short).Show();
            }
            else
            {

                Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)GetSystemService(ConnectivityService);
                Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
                bool isOnline = (activeConnection != null) && activeConnection.IsConnected;


                if (!isOnline)

                    Toast.MakeText(this, "No Internet Connection", ToastLength.Long).Show();

                else
                {
                    progress = new ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                    progress.SetMessage("Authenticating");
                    progress.SetCancelable(false);
                    progress.Show();

                    int cl = await Constants.mAzureDataService.CheckCredential(editTextUsername.Text, editTextPassword.Text);

                    if (cl == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("Cl in if : {0}",cl);
                        progress.Dismiss();
                        Toast.MakeText(this, "Invalid Credential", ToastLength.Long).Show();
                    }
                    else
                    {
                        progress.Dismiss();
                        StartActivity(typeof(Navigation));
                        Toast.MakeText(this, "Valid Credential", ToastLength.Long).Show();
                        Finish();
                    }
                }

            }
        }
    }
}