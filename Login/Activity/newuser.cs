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
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class newuser : AppCompatActivity
    {

        EditText First_Name, Middle_Name, Last_Name, Mobile_No, Email_Id, Address, City, Pincode, State, Country, Joining_Date;
        Button Add,Cancel;
        RadioButton male, female;
        string Gender;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.newuser);

            Add = FindViewById<Button>(Resource.Id.Add);
            Cancel = FindViewById<Button>(Resource.Id.Cancel);
            First_Name = FindViewById<EditText>(Resource.Id.First_Name);
            Middle_Name = FindViewById<EditText>(Resource.Id.Middle_Name);
            Last_Name = FindViewById<EditText>(Resource.Id.Last_Name);
            Mobile_No = FindViewById<EditText>(Resource.Id.Mobile_No);
            Email_Id = FindViewById<EditText>(Resource.Id.Email_Id);
            Address = FindViewById<EditText>(Resource.Id.Address);
            City = FindViewById<EditText>(Resource.Id.City);
            Pincode = FindViewById<EditText>(Resource.Id.Pincode);
            State = FindViewById<EditText>(Resource.Id.State);
            Country = FindViewById<EditText>(Resource.Id.Country);
            male = FindViewById<RadioButton>(Resource.Id.male_radio_btn);
            female = FindViewById<RadioButton>(Resource.Id.female_radio_btn);
            Joining_Date = FindViewById<EditText>(Resource.Id.Joining_Date);

            male.Click += delegate
            {
                Gender = "Male";
            };

            female.Click += delegate
            {
                Gender = "Female";
            };

            Joining_Date.Click += (sender, e) =>
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };

            Add.Click += delegate
            {
                //Toast.MakeText(this,"" + First_Name.Text,ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Middle_Name.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Last_Name.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Mobile_No.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Email_Id.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Address.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + City.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Pincode.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + State.Text, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Country.Text, ToastLength.Long).Show();
                Toast.MakeText(this, "" + Gender, ToastLength.Long).Show();
                //Toast.MakeText(this, "" + Joining_Date.Text, ToastLength.Long).Show();
            };

            Cancel.Click += delegate
            {
                Toast.MakeText(this, "Cancel button clicked", ToastLength.Long).Show();
            };
        }
        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            Joining_Date.Text = e.Date.ToLongDateString();
        }
    }
}