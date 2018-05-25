using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;

using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Login.Extra;
using Login.FragmentActivity;
using Login.ModelClass;
using Login.Service;
using Microsoft.WindowsAzure.MobileServices;

namespace Login.Activity
{

    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Navigation : AppCompatActivity
    {
        int i = 0;
        IMenu menu;
        MyActionBar drawertoggle;
        DrawerLayout drawerlayout;
        NavigationView nav_view;
        Android.Support.V4.App.Fragment fragment;
        Android.Support.V4.App.FragmentTransaction ft;
        Android.Support.V4.App.FragmentManager manager;
        //Android.App.ProgressDialog progress;
        //ListQuestion lq = new ListQuestion();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Navigation);

            manager = SupportFragmentManager;
            ft = manager.BeginTransaction();

            fragment = new HomeFragment();
            ft.Add(Resource.Id.frame, fragment, "HomeFragment");
            ft.AddToBackStack(null).Commit();

            ft.Show(fragment);
            drawerlayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            nav_view = FindViewById<NavigationView>(Resource.Id.nav_view);

            var tool = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(tool);
            drawertoggle = new MyActionBar(this, drawerlayout, Resource.String.open, Resource.String.closed);
            drawerlayout.AddDrawerListener(drawertoggle);
            nav_view.NavigationItemSelected += NavigationView_NavigationItemSelected;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            drawertoggle.SyncState();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            this.menu = menu;
            MenuInflater.Inflate(Resource.Menu.Top_Menu, menu);
            IMenuItem it = menu.FindItem(Resource.Id.menu_edit);
            it.SetVisible(false);
            return base.OnCreateOptionsMenu(menu);

        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            Android.Support.V4.App.Fragment fragment = null;
            Intent intent;

            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.home:
                    fragment = new HomeFragment();
                    FragementSelection(fragment, "HomeFragment");

                    break;
                case Resource.Id.addemployee:
                    fragment = new ListEmployee();
                    FragementSelection(fragment, "ListEmployee");

                    break;
                case Resource.Id.adddesignation:
                    fragment = new ListDesignation();
                    FragementSelection(fragment, "ListDesignation");
                    break;
                case Resource.Id.manage_Question:
                    fragment = new ListQuestion();
                    FragementSelection(fragment, "ListQuestion");
                    i = 4;
                    break;
                case Resource.Id.scheduleround:
                    fragment = new ListRound();
                    FragementSelection(fragment, "ListRound");
                    i = 5;
                    break;
                case Resource.Id.logout:
                    intent = new Intent(this, typeof(Activity1));
                    StartActivity(intent);
                    Finish();
                    break;

            }
            if (fragment != null)
            {
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragment).Commit();
            }


            drawerlayout.CloseDrawers();

        }

        public void FragementSelection(Android.Support.V4.App.Fragment fragment, string Tag)
        {
            if (fragment != null)
            {
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragment, Tag).Commit();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            drawertoggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        { 
            if (fragment != null)
            {
                Android.Support.V4.App.Fragment f = manager.FindFragmentByTag("HomeFragment");
                if (f.IsVisible)
                {
                    ft.Remove(f);
                    Finish();
                }
                else
                {
                    FragementSelection(fragment, "HomeFragment");
                }
            }
        }

    }
}