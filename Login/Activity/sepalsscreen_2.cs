using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using System;
using Android.Content;
using Login.Activity;
using Android.Content.PM;

namespace Login
{
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar",ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.sepalsscreen);
          

        }
        protected override void OnResume()
        {
          
            base.OnResume();
            Task startupWork = new Task(async () => { await SimulateStartupAsync(); });
            startupWork.Start();
        }

        private async Task SimulateStartupAsync()
        {
            await Task.Delay(4000); // Simulate a bit of startup work.
           StartActivity(new Intent(this, typeof(Activity1)));
            Finish();
        }
    }
}

