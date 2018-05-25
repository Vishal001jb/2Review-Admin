using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Login.Adapter;
using Login.ModelClass;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.FragmentActivity
{
    public class ListRound : Android.Support.V4.App.Fragment
    {
        List<Round> Datalist = new List<Round>();
        //List<data> Datalist1 = new List<data>();
        ProgressDialog progress;
        AdapterRound mAdapterRound;
        RecyclerView RoundRecyclerview;

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
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.ListRound, null);
            RoundRecyclerview = root.FindViewById<RecyclerView>(Resource.Id.RoundRecyclerView);
            return root;
        }
        public void Add()
        {
            View view = LayoutInflater.From(Activity).Inflate(Resource.Layout.AddRound, null);
            AlertDialog builder = null;
            EditText edtextRound = view.FindViewById<EditText>(Resource.Id.edtextRound);
            Button addRound = view.FindViewById<Button>(Resource.Id.addRound);
            Button cancelRound = view.FindViewById<Button>(Resource.Id.cancelRound);

            string time = DateTime.Now.ToString("HH:mm");
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            addRound.Click += async delegate
            {
                if (edtextRound.Text == "")
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
                        await Constants.mAzureDataService.AddRound(edtextRound.Text, date, time);
                        progress.Dismiss();
                        Toast.MakeText(Activity, "Successfully Inserted", ToastLength.Long).Show();
                        //Toast.MakeText(Activity, ""+date, ToastLength.Long).Show();
                        //Toast.MakeText(Activity, ""+time, ToastLength.Long).Show();
                        SetAdapter();
                    }
                }
            };

            cancelRound.Click += delegate
            {
                builder.Dismiss();
            };

            builder = new AlertDialog.Builder(Activity).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);
            builder.Show();

        }

        public async void SetAdapter()
        {
            try
            {
                progress = new ProgressDialog(Activity);
                progress.Indeterminate = true;
                progress.SetProgressStyle(ProgressDialogStyle.Spinner);
                progress.SetMessage("Please wait...");
                progress.SetCancelable(false);
                progress.Show();
                //Datalist = await dataService.GetRound();
                Datalist = await Constants.mAzureDataService.GetRound();
                progress.Dismiss();

                if (mAdapterRound == null)
                {
                    mAdapterRound = new AdapterRound(Activity, Datalist);
                    LinearLayoutManager manager  = new LinearLayoutManager(Activity);
                    RoundRecyclerview.SetLayoutManager(manager);
                    RoundRecyclerview.SetAdapter(mAdapterRound);
                }
                else
                {
                    mAdapterRound.mDataList = Datalist;
                    mAdapterRound.NotifyDataSetChanged();
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