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

namespace Login.Activity
{
    
    [Activity(Label = "2Review",  Theme = "@style/Theme.AppCompat.Light.NoActionBar", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Navigtion : AppCompatActivity
    {
    
        Android.Support.V7.Widget.Toolbar Toolbar;
        MyActionBar drawertoggle;
        DrawerLayout drawerlayout;
        NavigationView nav_view;
        FloatingActionButton fab;
        Android.Support.V4.App.Fragment fragment;
        Android.Support.V4.App.FragmentTransaction ft;
        Android.Support.V4.App.FragmentManager manager; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            manager = SupportFragmentManager;
            ft = manager.BeginTransaction();
            
            // Create your application here
            SetContentView(Resource.Layout.Navigation);
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Hide();

            fragment = new HomeFragment();
            ft.Replace(Resource.Id.frame, fragment).Commit();
            
          // ft.Show(fragment);
            drawerlayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            nav_view = FindViewById<NavigationView>(Resource.Id.nav_view);

            Toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(Toolbar);
            drawertoggle = new MyActionBar(this, drawerlayout, Resource.String.open, Resource.String.closed);
            drawerlayout.AddDrawerListener(drawertoggle);
            nav_view.NavigationItemSelected += NavigationView_NavigationItemSelected;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            drawertoggle.SyncState();
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {

            Android.Support.V4.App.Fragment fragment = null;


            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.home:
                    //fragment = new HomeFragment();
                    //ft.Replace(Resource.Id.frame, fragment).Commit();
                    
                    break; 
                case Resource.Id.addemployee:
                    fragment = new AddemployeeFragment();
                  //  ft.Replace(Resource.Id.frame, fragment).Commit();

                    break;
                case Resource.Id.adddesignation:
                    break;
                case Resource.Id.managequestion:
                    break;
                case Resource.Id.scheduleround:
                    break;

            }
            if(fragment !=null){
                SupportFragmentManager.BeginTransaction().Replace(Resource.Id.frame, fragment).Commit();
            }
           

            drawerlayout.CloseDrawers();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            drawertoggle.OnOptionsItemSelected(item);



            return base.OnOptionsItemSelected(item);
        }


    }
}