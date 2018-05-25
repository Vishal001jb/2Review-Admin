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
    class AdapterRound : RecyclerView.Adapter
    {
        Context context;
        public List<Round> mDataList;

        public AdapterRound(Context context, List<Round> round)
        {
            this.context = context;
            this.mDataList = round;
        }

        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterRoundViewHolder;
            if (dataitem != null)
            {
                viewholder.txtRoundName.Text = dataitem.Round_Name;
                viewholder.txtCountReviewee.Text = dataitem.Reviewee.ToString();
                viewholder.txtCountReviewable.Text = dataitem.Reviewable.ToString();
                viewholder.txtStatus.Text = dataitem.Status;
                viewholder.txtDate.Text = dataitem.RoundDate;
                viewholder.txtTime.Text = dataitem.RoundTime;
                viewholder.progressBar.Progress = dataitem.Progress;
            }

            viewholder.Card_Click.Click += delegate
            {
                Intent i = new Intent(context,typeof(RoundProcess));
                i.PutExtra("RoundId",mDataList[position].Id);
                context.StartActivity(i);
                //Toast.MakeText(context, "card view called!", ToastLength.Short).Show();
            };

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Round_Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterRoundViewHolder madapterrecyclerviewholder = new AdapterRoundViewHolder(itemView);
            return madapterrecyclerviewholder;
        }

    }

    class AdapterRoundViewHolder : RecyclerView.ViewHolder
    {
        public CardView Card_Click;
        public ProgressBar progressBar;
        public TextView txtRoundName, txtCountReviewee, txtCountReviewable, txtStatus, txtDate, txtTime;

        public AdapterRoundViewHolder(View itemview) : base(itemview)
        {
            txtRoundName = itemview.FindViewById<TextView>(Resource.Id.txtRoundName);
            txtCountReviewee = itemview.FindViewById<TextView>(Resource.Id.txtCountReviewee);
            txtCountReviewable = itemview.FindViewById<TextView>(Resource.Id.txtCountReviewable);
            txtStatus = itemview.FindViewById<TextView>(Resource.Id.txtStatus);
            txtDate = itemview.FindViewById<TextView>(Resource.Id.txtDate);
            txtTime = itemview.FindViewById<TextView>(Resource.Id.txtTime);
            Card_Click = itemview.FindViewById<CardView>(Resource.Id.Card_Click);
            progressBar = itemview.FindViewById<ProgressBar>(Resource.Id.progressBar1);
        }
    }
}