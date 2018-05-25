using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using V4FragmentManager = Android.Support.V4.App.FragmentManager;
using Android.Views;
using Login.FragmentActivity;
using V4Fragment = Android.Support.V4.App.Fragment;
using Android.Content.PM;
using Android.Support.V7.Widget;
using Login.Adapter;
using Login.Service;
using Login.ModelClass;

namespace Login.Activity
{
    [Activity(Label = "RoundProcess", Theme = "@style/Theme.AppCompat.Light", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class RoundProcess : AppCompatActivity
    {
        string RoundId;
        List<RevieweeListForRound> RoundReviewee;
        RecyclerView DialogRecycler;
        RecyclerView RevieweeListRecycler;
        RecyclerView.LayoutManager mLayoutManager;
        AdpaterRevieweeList mRevieweeListAdapter;
        ProgressDialog progress;
        //     public CardView card1 { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.RoundRevieweeList);
            RoundId = Intent.GetStringExtra("RoundId");
            RevieweeListRecycler = FindViewById<RecyclerView>(Resource.Id.RevieweeListRecycler);
            SetupRevieweeList();
            // Get our button from the layout resource,
            // and attach an event to it


            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += (sender, e) =>
            {
                // Show a snackbar
                // Snackbar.Make(fab, "Sure ! You want to add employee ", Snackbar.LengthLong).SetAction("Add",
                // v => Console.WriteLine("Action handler")).Show();
                View anchor = sender as View;

                Snackbar.Make(anchor, "Sure! You want to add employee!", Snackbar.LengthLong)
                        .SetAction("ADD", v =>
                        {
                            //Do something here
                                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                                builder.SetTitle("ReviewableList");
                                LayoutInflater inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
                                View content = inflater.Inflate(Resource.Layout.CustomDialog,null);
                                builder.SetView(content);
                                DialogRecycler = content.FindViewById<RecyclerView>(Resource.Id.DialogRecyclerView);
                            //View content = inflater.Inflate();
                        })
                        .Show();

            };
        }

        public async void SetupRevieweeList()
        {
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Please wait...");
            progress.SetCancelable(false);
            progress.Show();
            RoundReviewee = await Constants.mAzureDataService.GetRevieweeListForReview(RoundId);
            progress.Dismiss();
            mLayoutManager = new LinearLayoutManager(this);
            mRevieweeListAdapter = new AdpaterRevieweeList(this, RoundReviewee);
            RevieweeListRecycler.SetLayoutManager(mLayoutManager);
            RevieweeListRecycler.SetAdapter(mRevieweeListAdapter);
        }

    }
    

    }