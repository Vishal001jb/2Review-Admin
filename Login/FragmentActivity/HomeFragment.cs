using Android.OS;
using Android.Views;
using Android.Widget;
using Login.FragmentActivity;
using Login.Activity;

namespace Login
{
    class HomeFragment : Android.Support.V4.App.Fragment
    {

        Android.Support.V4.App.Fragment fragment;
        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.Home, null);
            Button b1 = root.FindViewById<Button>(Resource.Id.addemployee);
            Button b2 = root.FindViewById<Button>(Resource.Id.adddesignation);
            Button b3 = root.FindViewById<Button>(Resource.Id.managequestion);
            Button b4 = root.FindViewById<Button>(Resource.Id.scheduleround);

            b1.Click += delegate
            {
                fragment = new ListEmployee();
                ((Navigation)Activity).FragementSelection(fragment, "ListEmployee");
            };
            b2.Click += delegate
            {
                fragment = new ListDesignation();
                ((Navigation)Activity).FragementSelection(fragment, "ListDesignation");
            };
            b3.Click += delegate
            {
                fragment = new ListQuestion();
                ((Navigation)Activity).FragementSelection(fragment, "ListQuestion");
            };
            b4.Click += delegate
            {
                fragment = new ListRound();
                ((Navigation)Activity).FragementSelection(fragment, "ListRound");
            };

            return root;
        }
    }
}
