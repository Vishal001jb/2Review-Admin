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

namespace Login
{
    public class ListDesignation : Android.Support.V4.App.Fragment
    {
        RecyclerView DesignationRecyclerView;
        AdapterDesignation mAdapterDesignation;
        AzureDataService dataService = new AzureDataService();
        List<Designation> Datalist = new List<Designation>();
        Android.App.ProgressDialog progress;

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
                CurrentPlatform.Init();
                dataService.Initialize();
                SetAdapter();
            }

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.ListDesignation, null);
            DesignationRecyclerView = root.FindViewById<RecyclerView>(Resource.Id.DesignationRecyclerview);
            return root;
        }

        public void Add()
        {
            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.AddDesignation, null);
            AlertDialog builder = null;
            EditText addesi = view.FindViewById<EditText>(Resource.Id.addesi);
            Button DesbtnAdd = view.FindViewById<Button>(Resource.Id.DesbtnAdd);
            Button DesbtnCancel = view.FindViewById<Button>(Resource.Id.DesbtnCancel);

            DesbtnAdd.Click += async delegate
            {
                if (addesi.Text == "")
                {
                    Toast.MakeText(Activity, "field cannot be left blank", ToastLength.Long).Show();

                }
                else
                {
                    Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)Context.GetSystemService(Context.ConnectivityService);
                    Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
                    bool isOnline = (activeConnection != null) && activeConnection.IsConnected;

                    if (!isOnline)
                    {
                        Toast.MakeText(Context, "No Internet Connection", ToastLength.Long).Show();
                    }
                    else
                    {
                        builder.Dismiss();
                        progress = new ProgressDialog(Activity);
                        progress.Indeterminate = true;
                        progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                        progress.SetMessage("Adding Please wait...");
                        progress.SetCancelable(false);
                        progress.Show();
                        await dataService.AddDesignation(addesi.Text);
                        progress.Dismiss();
                        dataset();
                        Toast.MakeText(Activity, "Successfully Inserted", ToastLength.Long).Show();
                        SetAdapter();
                    }
                }
            };

            DesbtnCancel.Click += delegate
            {
                builder.Dismiss();
            };

            builder = new AlertDialog.Builder(Activity).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);
            builder.Show();

        }
        public async void Delete(Designation Id)
        {

            Android.Net.ConnectivityManager connectivityManager = (Android.Net.ConnectivityManager)Context.GetSystemService(Context.ConnectivityService);
            Android.Net.NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
            bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
            if (!isOnline)
                Toast.MakeText(Context, "No Internet Connection", ToastLength.Long).Show();
            else
            {
                if (Datalist != null && Datalist.Count != 0)
                {
                    progress = new Android.App.ProgressDialog(Activity);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Deleting...");
                    progress.SetCancelable(false);
                    progress.Show();
                    await dataService.DeleteDesignation(Id);
                    progress.Dismiss();
                    SetAdapter();
                }
            }
        }

        public async void SetAdapter()
        {
            try
            {
                progress = new Android.App.ProgressDialog(Activity);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Loading...");
                progress.SetCancelable(false);
                progress.Show();
                Datalist = await dataService.GetDesignation();
                progress.Dismiss();

                if (mAdapterDesignation == null)
                {
                    mAdapterDesignation = new AdapterDesignation(this, Datalist);
                    LinearLayoutManager manager = new LinearLayoutManager(Activity);
                    DesignationRecyclerView.SetLayoutManager(manager);
                    DesignationRecyclerView.SetAdapter(mAdapterDesignation);
                }
                else
                {
                    mAdapterDesignation.mDataList = Datalist;
                    mAdapterDesignation.NotifyDataSetChanged();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
            }
        }

        public static implicit operator Fragment(ListDesignation v)
        {
            throw new NotImplementedException();
        }
        public async void dataset()
        {
            var temp1 = await Constants.mAzureDataService.GetDesignation();
            foreach (var temp in temp1)
            {
                var t2 = temp.DesignationName;
                Constants.lst1.Add(t2);
                var t1 = temp.Id;
                Designation t3 = new Designation(t1, t2);
                Constants.lst.Add(t3);
            }
        }
    }
}