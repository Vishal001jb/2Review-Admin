using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Login.Activity;

namespace Login.Adapter
{
    class AdapterEmployeePerformance : RecyclerView.Adapter
    {
        EmployeeDetail context;
        public List<string> mDataList;

        public AdapterEmployeePerformance(EmployeeDetail context, List<string> mData)
        {
            this.context = context;
            this.mDataList = mData;
        }
        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterEmployeePerformanceViewHolder;
            if (dataitem != null)
            {
                viewholder.mRoundName.Text = mDataList[position];
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Ratinglist;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterEmployeePerformanceViewHolder madapterrecyclerviewholder = new AdapterEmployeePerformanceViewHolder(itemView);
            return madapterrecyclerviewholder;
        }
    }

    class AdapterEmployeePerformanceViewHolder : RecyclerView.ViewHolder
    {
        public TextView mRoundName, mRoundDate, mRateCount;
        public ProgressBar mProgress1;
        public AdapterEmployeePerformanceViewHolder(View itemview) : base(itemview)
        {
            mRoundName = itemview.FindViewById<TextView>(Resource.Id.txtRoundName);
            mRoundDate = itemview.FindViewById<TextView>(Resource.Id.txtDate);
            mRateCount = itemview.FindViewById<TextView>(Resource.Id.txtRateCount);
            mProgress1 = itemview.FindViewById<ProgressBar>(Resource.Id.progressrate);
        }
    }
}