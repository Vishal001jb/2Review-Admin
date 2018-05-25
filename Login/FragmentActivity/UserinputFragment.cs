using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Login.FragmentActivity
{
    public class UserinputFragment : Fragment
    {

        public UserinputFragment()
        {

        }
        public static Fragment newInstance(Context context)
        {
            UserinputFragment busrouteFragment = new UserinputFragment();
            return busrouteFragment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.UserinputFragment, null);
            return root;
        }
    }
}