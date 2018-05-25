using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Login.Extra
{
    class MyActionBar : ActionBarDrawerToggle
    {
        private AppCompatActivity host;
        private int openResource, closedResource;

        public MyActionBar(AppCompatActivity host, DrawerLayout drawerLayout, int openResource, int closedResource)
             : base(host, drawerLayout, openResource, closedResource)
        {
            this.host = host;
            this.openResource = openResource;
            this.closedResource = closedResource;
        }
        public override void OnDrawerClosed(View drawerView)
        {
            base.OnDrawerClosed(drawerView);
        }
        public override void OnDrawerOpened(View drawerView)
        {
            base.OnDrawerOpened(drawerView);
        }
        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            base.OnDrawerSlide(drawerView, slideOffset);
        }
    }
}