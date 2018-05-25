using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Login.FragmentActivity;
using Login.Service;

namespace Login.Adapter
{
    class AdapterEmployee : RecyclerView.Adapter
    {
        public event EventHandler<AdapterEmployeeClickEventArgs> ItemClick;
        ListEmployee context;
        public List<EmpDesig> mDataList;

        public AdapterEmployee(ListEmployee context, List<EmpDesig> mData)
        {
            this.context = context;
            this.mDataList = mData;
        }
        public override int ItemCount => mDataList.Count;
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var dataitem = mDataList[position];
            var viewholder = holder as AdapterEmployeeViewHolder;
            if (dataitem != null)
            {
                viewholder.employee.Text = dataitem.EmpType;
                viewholder.employeedesignation.Text = dataitem.EmpDesi;
            }

            //viewholder.DesignationDelete.Click += delegate
            //{
            //    //((ListDesignation)context).Delete(dataitem);
            //};
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var id = Resource.Layout.Employee_Row_Item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            AdapterEmployeeViewHolder madapterrecyclerviewholder = new AdapterEmployeeViewHolder(itemView, OnClick);
            return madapterrecyclerviewholder;
        }

        void OnClick(AdapterEmployeeClickEventArgs args) => ItemClick?.Invoke(this, args);
    }

    class AdapterEmployeeViewHolder : RecyclerView.ViewHolder
    {
        public TextView employee,employeedesignation;
        public AdapterEmployeeViewHolder(View itemview, Action<AdapterEmployeeClickEventArgs> clickListener) : base(itemview)
        {
            employee = itemview.FindViewById<TextView>(Resource.Id.Employee);
            employeedesignation = itemview.FindViewById<TextView>(Resource.Id.EmployeeDesignation);
            itemview.Click += (sender, e) => clickListener(new AdapterEmployeeClickEventArgs { View = itemview, Position = AdapterPosition, itemtype = ItemViewType});

        }
    }
    public class AdapterEmployeeClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public int itemtype { get; set; }
    }

}
