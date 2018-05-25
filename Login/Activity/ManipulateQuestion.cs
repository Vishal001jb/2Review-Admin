using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.Activity
{
    [Activity(Label = "ManipulateQuestion", Theme = "@style/Theme.AppCompat.Light")]
    public class ManipulateQuestion : AppCompatActivity
    {
        AzureDataService dataService = new AzureDataService();
        QueDesig Datalist = new QueDesig();
        ProgressDialog progress;
        string QID, DID;
        TextView Question, Designation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ManipulateQuestion);
            Question = FindViewById<TextView>(Resource.Id.Question);
            Designation = FindViewById<TextView>(Resource.Id.Designation);
            QID = Intent.GetStringExtra("qId");
            DID = Intent.GetStringExtra("dId");

            //Question.Text = "Follows company rules and regulations ?";
            //Designation.Text = "Project Lead";

            //Toast.MakeText(this, "" + QID, ToastLength.Long).Show();
            //Toast.MakeText(this, "" + DID, ToastLength.Long).Show();

            Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)GetSystemService(ConnectivityService);
            Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;

            if (!isOnline)

                Toast.MakeText(this, "No Internet Connection", ToastLength.Long).Show();

            else
            {
                CurrentPlatform.Init();
                dataService.Initialize();
                SetAdapter();
            }
        }
        public async void SetAdapter()
        {
            try
            {
                progress = new ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage("Please wait...");
                progress.SetCancelable(false);
                progress.Show();
                //Datalist = await dataService.GetRound();
                Datalist = await dataService.GetSelectedQuestionDesignation(QID, DID);

                if (Datalist != null)
                {
                    Question.Text = Datalist.Question_Name;
                    Designation.Text = Datalist.Designation_Name;
                    //Toast.MakeText(this, "" + Question.Text, ToastLength.Long).Show();
                    //Toast.MakeText(this, "" + Designation.Text, ToastLength.Long).Show();
                }
                progress.Dismiss();
            }
            catch (Exception e)
            {

            }
            finally
            {

            }
        }
    }
}