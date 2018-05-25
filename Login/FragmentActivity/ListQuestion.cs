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
using Login.Activity;
using Login.Adapter;
using Login.ModelClass;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login
{
    public class ListQuestion : Android.Support.V4.App.Fragment
    {
        RecyclerView QuestionRecyclerView;
        AdapterQuestion mAdapterQuestion;
        AzureDataService dataService = new AzureDataService();
        static List<QueDesig> Datalist = new List<QueDesig>();
        Android.App.ProgressDialog progress;
        Spinner spinner;
        string did;

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
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.ListQuestion, null);
            QuestionRecyclerView = root.FindViewById<RecyclerView>(Resource.Id.QuestionRecyclerview);
            
            return root;
        }

        public void itemclicked(object sender, AdapterQuestionClickEventArgs e)
        {
            //Toast.MakeText(Context, ""+ListEmployee.Datalist[e.Position].EmpId, ToastLength.Long).Show();
            //Toast.MakeText(Context, ""+ListEmployee.Datalist[e.Position].EmpDesiId, ToastLength.Long).Show();
            //Activity.StartActivity(typeof(AddEmployee));
            var intent = new Intent(Context, typeof(ManipulateQuestion));
            intent.PutExtra("qId", ListQuestion.Datalist[e.Position].Q_Id);
            intent.PutExtra("dId", ListQuestion.Datalist[e.Position].D_Id);
            StartActivity(intent);
        }

        public void Add()
        {
            View  view = LayoutInflater.From(Activity).Inflate(Resource.Layout.AddQuestion, null);
            AlertDialog builder = null;
            EditText adque = view.FindViewById<EditText>(Resource.Id.adque);
            Button QuebtnAdd = view.FindViewById<Button>(Resource.Id.QuebtnAdd);
            Button QuebtnCancel = view.FindViewById<Button>(Resource.Id.QuebtnCancel);
            spinner = view.FindViewById<Spinner>(Resource.Id.spinnerQue);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter1 = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleSpinnerItem,Constants.lst1);
            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter1;

            //string did = spinner.SelectedItemId.ToString();

            QuebtnAdd.Click += async delegate
                {
                    if (adque.Text == "")
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
                            string id = await Task.FromResult<string>(await dataService.AddQuestion(adque.Text));
                            await dataService.AddQuestionDesignation(id,did);
                            progress.Dismiss();
                            Toast.MakeText(Activity, "Successfully Inserted", ToastLength.Long).Show();
                            SetAdapter();
                        }
                    }
                };

            QuebtnCancel.Click += delegate
                {
                    builder.Dismiss();
                };

            builder = new AlertDialog.Builder(Activity).Create();
            builder.SetView(view);
            builder.SetCanceledOnTouchOutside(false);
            builder.Show();

        }
        public async void Delete(Question Id)
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
                    //await dataService.DeleteQuestion(Id);
                    progress.Dismiss();
                    SetAdapter();
                }
            }
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
                Datalist = await dataService.GetQuestionDesignation();
                progress.Dismiss();

                if (mAdapterQuestion == null)
                {
                    mAdapterQuestion = new AdapterQuestion(this, Datalist);
                    LinearLayoutManager manager = new LinearLayoutManager(Activity);
                    QuestionRecyclerView.SetLayoutManager(manager);
                    QuestionRecyclerView.SetAdapter(mAdapterQuestion);
                    mAdapterQuestion.ItemClick += itemclicked;
                }
                else
                {
                    mAdapterQuestion.mDataList = Datalist;
                    mAdapterQuestion.NotifyDataSetChanged();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
            }
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            did = Constants.lst[e.Position].DesignationId;
        }

        public static implicit operator Fragment(ListQuestion v)
        {
            throw new NotImplementedException();
        }
    }
}
