using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Login.ModelClass;
using Login.Activity;
using Android.Support.V7.Widget;
using Login.FragmentActivity;
using Login.Service;

namespace Login.Adapter
{
    class AdapterQuestion : RecyclerView.Adapter
    {
        public event EventHandler<AdapterQuestionClickEventArgs> ItemClick;
        ListQuestion context;
        public List<QueDesig> mDataList;

        public AdapterQuestion(ListQuestion context, List<QueDesig> mData)
        {
            this.context = context;
            this.mDataList = mData;
        }

        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterQuestionViewHolder;
            if (dataitem != null)
            {
                viewholder.Question.Text = dataitem.Question_Name;
                viewholder.Designation.Text = dataitem.Designation_Name;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Question_Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterQuestionViewHolder madapterrecyclerviewholder = new AdapterQuestionViewHolder(itemView, OnClick);
            return madapterrecyclerviewholder;
        }
        void OnClick(AdapterQuestionClickEventArgs args) => ItemClick?.Invoke(this, args);

    }

    class AdapterQuestionViewHolder : RecyclerView.ViewHolder
    {
        public TextView Question,Designation;

        public AdapterQuestionViewHolder(View itemview, Action<AdapterQuestionClickEventArgs> clickListener) : base(itemview)
        {
            Question = itemview.FindViewById<TextView>(Resource.Id.question);
            Designation = itemview.FindViewById<TextView>(Resource.Id.designation);
            itemview.Click += (sender, e) => clickListener(new AdapterQuestionClickEventArgs { View = itemview, Position = AdapterPosition, itemtype = ItemViewType });
        }
    }
    public class AdapterQuestionClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public int itemtype { get; set; }
    }
}