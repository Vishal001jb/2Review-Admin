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
namespace Login.Adapter
{
    class AdapterDesignation : RecyclerView.Adapter
    {
        ListDesignation context;
        public List<Designation> mDataList;

        public AdapterDesignation(ListDesignation context, List<Designation> mData)
        {
            this.context = context;
            this.mDataList = mData;
        }
        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterDesignationViewHolder;
            if (dataitem != null)
            {
                viewholder.designation.Text = dataitem.DesignationName;
            }

            viewholder.DesignationDelete.Click += delegate
            {
                ((ListDesignation)context).Delete(dataitem);
            };
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Designation_Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterDesignationViewHolder madapterrecyclerviewholder = new AdapterDesignationViewHolder(itemView);
            return madapterrecyclerviewholder;
        }
    }

    class AdapterDesignationViewHolder : RecyclerView.ViewHolder
    {
        public TextView designation;
        public Button DesignationDelete;
        public Button DesignationEdit;
        public AdapterDesignationViewHolder(View itemview) : base(itemview)
        {
            designation = itemview.FindViewById<TextView>(Resource.Id.designation);
            DesignationDelete = itemview.FindViewById<Button>(Resource.Id.DesignationDelete);
            DesignationEdit = itemview.FindViewById<Button>(Resource.Id.DesignationEdit);
        }
    }
}