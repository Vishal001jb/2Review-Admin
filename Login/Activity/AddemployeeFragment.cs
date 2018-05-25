using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Login.Activity;
using Login.ModelClass;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.FragmentActivity
{
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]

    public class AddemployeeFragment : AppCompatActivity
    {

        AzureDataService dataService = new AzureDataService();
        TextView textviewjoiningdate;
        EditText edittextuserfname, edittextusermname, edittextuserlname, edittextusernumber, edittextuseremail, edittextuseraddress, edittextusercity, edittextuserpincode, edittextuserstate, edittextusercountry, radiogroupgender, edittextusername, edittextuserpassword, edittextconfirmpassword;
        List<EmployeeCredential> dl = new List<EmployeeCredential>();
        Button Add;
        RadioButton male, female;
        Spinner spinner;
        ProgressDialog progress;
        string gender;
        DateTime today;
        string did;
        ListEmployee act = new ListEmployee();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Home);
            CurrentPlatform.Init();
            dataService.Initialize();
            UIReference();

        }

        public void UIReference()
        {
            Add = FindViewById<Button>(Resource.Id.buttonadd);
            edittextuserfname = FindViewById<EditText>(Resource.Id.edittextuserfname);
            edittextusermname = FindViewById<EditText>(Resource.Id.edittextusermname);
            edittextuserlname = FindViewById<EditText>(Resource.Id.edittextuserlname);
            edittextusernumber = FindViewById<EditText>(Resource.Id.edittextusernumber);
            edittextuseremail = FindViewById<EditText>(Resource.Id.edittextuseremail);
            edittextuseraddress = FindViewById<EditText>(Resource.Id.edittextuseraddress);
            edittextusercity = FindViewById<EditText>(Resource.Id.edittextusercity);
            edittextuserpincode = FindViewById<EditText>(Resource.Id.edittextuserpincode);
            edittextuserstate = FindViewById<EditText>(Resource.Id.edittextuserstate);
            edittextusercountry = FindViewById<EditText>(Resource.Id.edittextusercountry);
            male = FindViewById<RadioButton>(Resource.Id.male_radio_btn);
            female = FindViewById<RadioButton>(Resource.Id.female_radio_btn);
            textviewjoiningdate = FindViewById<TextView>(Resource.Id.textviewjoiningdate);
            edittextusername = FindViewById<EditText>(Resource.Id.edittextusername);
            edittextuserpassword = FindViewById<EditText>(Resource.Id.edittextuserpassword);
            edittextconfirmpassword = FindViewById<EditText>(Resource.Id.edittextconfirmpassword);

            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, Constants.lst1);
            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter1;

            //Joining date validation
            textviewjoiningdate.Click += delegate (object sender, EventArgs e)
            {
                today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };

            male.Click += delegate
            {
                gender = "Male";
            };

            female.Click += delegate
            {
                gender = "Female";
            };

            string image = "image1";

            string a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12;

            Add.Click += async delegate
            {

                bool invalid = false;

                //first name validation
                if (string.IsNullOrWhiteSpace(edittextuserfname.Text))
                {
                    edittextuserfname.Error = "First Name cannot be Blank";
                }
                else
                {
                    if (edittextuserfname.Text.Length < 3)
                    {
                        invalid = true;
                        edittextuserfname.Error = "First name can not be less then 3 characters";
                    }
                    else
                    {
                        edittextuserfname.Error = null;
                    }
                }
                //middle name validation
                if (string.IsNullOrWhiteSpace(edittextusermname.Text))
                {
                    edittextusermname.Error = "Middle Name cannot be Blank";
                }
                else
                {
                    if (edittextusermname.Text.Length < 3)
                    {
                        invalid = true;
                        edittextusermname.Error = "Middle name can not be less then 3 characters";
                    }
                    else
                    {
                        edittextusermname.Error = null;
                    }
                }

                //last name validation
                if (string.IsNullOrWhiteSpace(edittextuserlname.Text))
                {
                    edittextuserlname.Error = "Last Name cannot be Blank";
                }
                else
                {
                    if (edittextuserlname.Text.Length < 3)
                    {
                        invalid = true;
                        edittextuserlname.Error = "Last name can not be less then 3 characters";
                    }
                    else
                    {
                        edittextuserlname.Error = null;
                    }
                }

                //Number Validation
                string inputnumber = edittextusernumber.Text.ToString();
                var number = isValidMobile(inputnumber);
                if (string.IsNullOrWhiteSpace(inputnumber))
                {
                    edittextusernumber.Error = "Enter the Number.!";
                }
                else
                {
                    if (inputnumber.Length < 10)
                    {
                        invalid = true;
                        edittextusernumber.Error = "Please enter valid number";

                    }
                    else
                    {
                        edittextusernumber.Error = null;
                    }

                }

                //Email Validation

                string inputemail = edittextuseremail.Text.ToString();
                var emailvalidate = isValidEmail(inputemail);

                if (string.IsNullOrWhiteSpace(inputemail))
                {
                    edittextuseremail.Error = "Enter the Email.!";
                }
                else
                {
                    if (emailvalidate == false)
                    {
                        invalid = true;
                        edittextuseremail.Error = "Please enter valid Email";
                    }
                    else
                    {
                        edittextuseremail.Error = null;
                    }
                }

                //Address validation
                if (string.IsNullOrWhiteSpace(edittextuseraddress.Text))
                {
                    edittextuseraddress.Error = " Address cannot be Blank";
                }
                else
                {
                    if (edittextuseraddress.Text.Length < 3)
                    {
                        invalid = true;
                        edittextuseraddress.Error = "Address can not be less then 3 characters";
                    }
                    else
                    {
                        edittextuseraddress.Error = null;
                    }
                }

                //city validation
                if (string.IsNullOrWhiteSpace(edittextusercity.Text))
                {
                    invalid = true;
                    edittextusercity.Error = "City cannot be Blank";
                }
                else
                {
                    edittextusercity.Error = null;
                }

                //pincode validation 
                if (string.IsNullOrWhiteSpace(edittextuserpincode.Text))
                {
                    edittextuserpincode.Error = "Pincode cannot be Blank";
                }
                else
                {
                    if (edittextuserpincode.Text.Length == 6)
                    {
                        edittextuserpincode.Error = null;

                    }
                    else
                    {
                        invalid = true;
                        edittextuserpincode.Error = "Enter valid pincode";
                    }
                }

                //state validation
                if (string.IsNullOrWhiteSpace(edittextuserstate.Text))
                {
                    invalid = true;
                    edittextuserstate.Error = "State cannot be Blank";
                }
                else
                {
                    edittextuserstate.Error = null;
                }

                //country validation
                if (string.IsNullOrWhiteSpace(edittextusercountry.Text))
                {
                    invalid = true;
                    edittextusercountry.Error = "Country cannot be Blank";
                }
                else
                {
                    edittextusercountry.Error = null;
                }

                //joining date validation
                if (string.IsNullOrWhiteSpace(textviewjoiningdate.Text))
                {
                    invalid = true;
                    textviewjoiningdate.Error = "Joining date cannot be Blank";
                }
                else
                {
                    textviewjoiningdate.Error = null;
                }

                // username validation

                if (string.IsNullOrWhiteSpace(edittextusername.Text))
                {
                    invalid = true;
                    edittextusername.Error = "username cannot be Blank";
                }
                else
                {
                    edittextusername.Error = null;
                }

                //password validation
                if (string.IsNullOrWhiteSpace(edittextuserpassword.Text))
                {
                    invalid = true;
                    edittextuserpassword.Error = "password cannot be Blank";
                }
                else
                {
                    edittextuserpassword.Error = null;
                }

                //confirm password validation
                if (string.IsNullOrWhiteSpace(edittextconfirmpassword.Text))
                {
                    edittextconfirmpassword.Error = "Confirm password cannot be Blank";
                }
                else
                {
                    if (string.Compare(edittextuserpassword.Text, edittextconfirmpassword.Text) != 0)
                    {
                        invalid = true;
                        edittextconfirmpassword.Error = "password and confirm password doesn't match.!";
                    }
                    else
                    {
                        edittextconfirmpassword.Error = null;
                    }
                }

                if (invalid == false)
                {
                    a1 = edittextuserfname.Text;
                    a2 = edittextusermname.Text;
                    a3 = edittextuserlname.Text;
                    a4 = edittextusernumber.Text;
                    a5 = edittextuseremail.Text;
                    a6 = edittextuseraddress.Text;
                    a7 = edittextusercity.Text;
                    a8 = edittextuserpincode.Text;
                    a9 = edittextuserstate.Text;
                    a10 = edittextusercountry.Text;
                    a11 = edittextusername.Text;
                    a12 = edittextuserpassword.Text;

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
                        progress.SetMessage("Adding Please wait...");
                        progress.SetCancelable(false);
                        progress.Show();
                        string id = await Task.FromResult<string>(await dataService.AddEmployeeCredential(a11, a12));
                        string eid = await Task.FromResult<string>(await dataService.AddEmployee(id, a1, a2, a3, gender, a4, image, a5, a6, a7, a8, a9, a10, today));
                        await dataService.AddEmployeeDesignation(eid, did);
                        progress.Dismiss();
                        Toast.MakeText(this, "Successfully Inserted", ToastLength.Long).Show();
                        act.SetAdapter();
                        Finish();
                    }
                } 
            };
        }

        private bool isValidMobile(String edittextusernumber)
        {
            return Patterns.Phone.Matcher(edittextusernumber).Matches();
        }

        public bool isValidEmail(string edittextuseremail)
        {
            return Patterns.EmailAddress.Matcher(edittextuseremail).Matches();
        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            textviewjoiningdate.Text = e.Date.ToLongDateString();
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            did = Constants.lst[e.Position].DesignationId;
        }

    }
    }
