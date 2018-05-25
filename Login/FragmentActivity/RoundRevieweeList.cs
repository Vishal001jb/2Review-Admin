using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Login.Service;
using Login.ModelClass;

namespace Login.FragmentActivity
{
    public class RoundRevieweeList : Android.Support.V4.App.Fragment
    {
        RecyclerView mRevieweeListRecycler;
        List<RevieweeListForRound> EmployeeList;
        List<RevieweeListForRound> RevieweeList;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            EmployeeList = new List<RevieweeListForRound>();
            RevieweeList = new List<RevieweeListForRound>();
            var v = inflater.Inflate(Resource.Layout.RoundRevieweeList, container, false);
            mRevieweeListRecycler = v.FindViewById<RecyclerView>(Resource.Id.RevieweeListRecycler);
            GetRevieweeList();

            return v;
        }

        public async void GetRevieweeList()
        {
            EmployeeList = await Constants.mAzureDataService.GetRevieweeListForReview("");
        }
    }
}