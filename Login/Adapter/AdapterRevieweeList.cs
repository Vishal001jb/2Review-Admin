using System;
using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Login.Activity;
using Login.FragmentActivity;
using Login.ModelClass;
using Login.Service;

namespace Login.Adapter
{
    class AdpaterRevieweeList : RecyclerView.Adapter
    {
        Context context;
        public List<RevieweeListForRound> mDataList;

        public AdpaterRevieweeList(Context context, List<RevieweeListForRound> reviewlist)
        {
            this.context = context;
            mDataList = reviewlist;
        }

        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterRevieweeListViewHolder;
            if (dataitem != null)
            {
                viewholder.txtName.Text = dataitem.RevieweeName;
                viewholder.txtDesig.Text = dataitem.Designation;
                viewholder.txtTotal.Text = dataitem.Total.ToString();
            }

            viewholder.Card_Click.Click += delegate
            {
                //Intent i = new Intent(context, typeof(RoundProcess));
                //i.PutExtra("RoundId", mDataList[position].Id);
                //context.StartActivity(typeof(RoundProcess));
                //Toast.MakeText(context, "card view called!", ToastLength.Short).Show();
            };

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.RevieweeData;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterRevieweeListViewHolder madapterrecyclerviewholder = new AdapterRevieweeListViewHolder(itemView);
            return madapterrecyclerviewholder;
        }

    }

    class AdapterRevieweeListViewHolder : RecyclerView.ViewHolder
    {
        public CardView Card_Click;
        public TextView txtName, txtDesig, txtTotal;

        public AdapterRevieweeListViewHolder(View itemview) : base(itemview)
        {
            txtName = itemview.FindViewById<TextView>(Resource.Id.txtname);
            txtDesig = itemview.FindViewById<TextView>(Resource.Id.txtdesig);
            txtTotal = itemview.FindViewById<TextView>(Resource.Id.txttotal);
            Card_Click = itemview.FindViewById<CardView>(Resource.Id.Card_Click1);
        }
    }
}