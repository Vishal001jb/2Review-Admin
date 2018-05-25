using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Login.Activity;
using Login.Adapter;
using Login.ModelClass;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.FragmentActivity
{
    public class ListEmployee : Android.Support.V4.App.Fragment
    {

        RecyclerView EmployeeRecyclerview;
        AdapterEmployee mAdapterEmployee;
        static List<EmpDesig> Datalist = new List<EmpDesig>();
        ProgressDialog progress;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)Context.GetSystemService(Context.ConnectivityService);
            Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;

            if (!isOnline)
                Toast.MakeText(Context, "No Internet Connection", ToastLength.Long).Show();
            else
            {
                SetAdapter();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.ListEmployee, null);
            EmployeeRecyclerview = root.FindViewById<RecyclerView>(Resource.Id.EmployeeRecyclerview);
            return root;
        }

        public void itemclicked(object sender, AdapterEmployeeClickEventArgs e)
        {
            //Toast.MakeText(Context, ""+ListEmployee.Datalist[e.Position].EmpId, ToastLength.Long).Show();
            //Toast.MakeText(Context, ""+ListEmployee.Datalist[e.Position].EmpDesiId, ToastLength.Long).Show();
            //Activity.StartActivity(typeof(AddEmployee));
            var intent = new Intent(Context, typeof(EmployeeDetail));
            intent.PutExtra("EmpId", Datalist[e.Position].EmpId); 
            intent.PutExtra("EmpDesiId", Datalist[e.Position].EmpDesiId); 
            StartActivity(intent);
        }
        public void Add()
        {
            Toast.MakeText(Context, "Add button clicked", ToastLength.Long).Show();
            Intent intent = new Intent(Activity, typeof(AddEmployee));
            Activity.StartActivityForResult(intent,1);
        }

        public async void SetAdapter()                                                                                                                                                                                                                                      
        {
            try
            {
                progress = new ProgressDialog(Activity);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading...");
                progress.SetCancelable(false);
                progress.Show();
                Datalist = await Constants.mAzureDataService.GetEmpDesig();
                progress.Dismiss();

                if (mAdapterEmployee == null)
                {
                    mAdapterEmployee = new AdapterEmployee(this, Datalist);
                    LinearLayoutManager manager = new LinearLayoutManager(Activity);
                    EmployeeRecyclerview.SetLayoutManager(manager);
                    EmployeeRecyclerview.SetAdapter(mAdapterEmployee);
                    mAdapterEmployee.ItemClick += itemclicked;
                }
                else
                {
                    mAdapterEmployee.mDataList = Datalist;
                    mAdapterEmployee.NotifyDataSetChanged();
                }
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