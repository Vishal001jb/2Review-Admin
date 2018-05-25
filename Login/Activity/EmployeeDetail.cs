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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Login.Adapter;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.Activity
{
    [Activity(Label = "EmployeeDetail",Theme = "@style/Theme.AppCompat.Light", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class EmployeeDetail : AppCompatActivity
    {
        RecyclerView roundProgressRecyclerView;
        AdapterEmployeePerformance mAdapterEmployeePerformance;
        AzureDataService dataService = new AzureDataService();
        List<string> Datalist = new List<string>();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EmployeeDetail);

            roundProgressRecyclerView = FindViewById<RecyclerView>(Resource.Id.roundProgressRecyclerView);

            string EmpId = Intent.GetStringExtra("EmpId");
            string EmpDesiId = Intent.GetStringExtra("EmpDesiId");

            Toast.MakeText(this, "" + EmpId, ToastLength.Long).Show();
            Toast.MakeText(this, "" + EmpDesiId, ToastLength.Long).Show();

            CurrentPlatform.Init();
            dataService.Initialize();
            SetAdapter();
        }
        public async void SetAdapter()
        {
            try
            {
                Datalist = await dataService.GetEmpRoundWisePerformance();

                if (mAdapterEmployeePerformance == null)
                {
                    mAdapterEmployeePerformance = new AdapterEmployeePerformance(this, Datalist);
                    LinearLayoutManager manager = new LinearLayoutManager(this);
                    roundProgressRecyclerView.SetLayoutManager(manager);
                    roundProgressRecyclerView.SetAdapter(mAdapterEmployeePerformance);
                }
                else
                {
                    mAdapterEmployeePerformance.mDataList = Datalist;
                    mAdapterEmployeePerformance.NotifyDataSetChanged();
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