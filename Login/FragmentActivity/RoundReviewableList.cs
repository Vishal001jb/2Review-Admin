using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Login.ModelClass;

namespace Login.FragmentActivity
{
    public class RoundReviewableList : Android.Support.V4.App.Fragment
    {

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private RecyclerView.Adapter mAdapter;
        private List<RevieweeList> mRevieweeLists;
        private string RoundId;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var v = inflater.Inflate(Resource.Layout.RoundReviewableList, container, false);
            RoundId = Arguments.GetString("RoundId");
            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.recyclerview2);
            mRevieweeLists = new List<RevieweeList>();

            mLayoutManager = new LinearLayoutManager(v.Context);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new RecyclerAdapter(mRevieweeLists);
            mRecyclerView.SetAdapter(mAdapter);


            return v;
        }



        public class RecyclerAdapter : RecyclerView.Adapter
        {
            View temp;
            private List<RevieweeList> mRevieweeLists;

            public RecyclerAdapter(List<RevieweeList> revieweelist)
            {
                mRevieweeLists = revieweelist;
            }
            public class MyView : RecyclerView.ViewHolder
            {
                public View mMainView { get; set; }
                public ImageView mImage { get; set; }
                public TextView mName { get; set; }
                public TextView mDesignation { get; set; }
                public TextView mTotal { get; set; }
                public TextView mPending { get; set; }
                public CardView card1 { get; set; }
                public MyView(View view) : base(view)
                {
                    mMainView = view;
                }
            }
            View RevieweeData;

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                RevieweeData = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.RevieweeData, parent, false);
                ImageView imageView3 = RevieweeData.FindViewById<ImageView>(Resource.Id.imageView3);
                TextView txtname = RevieweeData.FindViewById<TextView>(Resource.Id.txtname);
                TextView txtdesig = RevieweeData.FindViewById<TextView>(Resource.Id.txtdesig);
                TextView txttotal = RevieweeData.FindViewById<TextView>(Resource.Id.txttotal);
                TextView txtpending = RevieweeData.FindViewById<TextView>(Resource.Id.txtpending);


                CardView Card_Click1 = RevieweeData.FindViewById<CardView>(Resource.Id.Card_Click1);

                MyView view = new MyView(RevieweeData) { card1 = Card_Click1, mImage = imageView3, mName = txtname, mDesignation = txtdesig, mTotal = txttotal, mPending = txtpending };
                temp = RevieweeData;
                return view;


            }
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                MyView myHolder = holder as MyView;
                myHolder.mImage.SetImageBitmap(BitmapFactory.DecodeFile(mRevieweeLists[position].Image));

                myHolder.mName.Text = mRevieweeLists[position].Name;
                myHolder.mDesignation.Text = mRevieweeLists[position].Designation;
                myHolder.mTotal.Text = mRevieweeLists[position].Total.ToString();
                myHolder.mPending.Text = mRevieweeLists[position].Pending.ToString();

                myHolder.card1.Click += delegate
                {

                    //Toast.MakeText(row.Context,"card view called!", ToastLength.Short).Show();
                };
            }



            public override int ItemCount
            {
                get { return mRevieweeLists.Count; }

            }
        }
    }
}