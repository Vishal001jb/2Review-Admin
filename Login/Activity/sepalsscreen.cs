    using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using Android.Content;
using Login.Activity;
using Android.Content.PM;
using Microsoft.WindowsAzure.MobileServices;
using Login.Service;

namespace Login
{
    [Activity(Label = "2Review", Theme = "@style/Theme.AppCompat.Light.NoActionBar",MainLauncher = true, Icon = "@drawable/ICON", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        ISharedPreferences sharedPreferences;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.sepalsscreen);
            CurrentPlatform.Init();
            sharedPreferences = GetSharedPreferences("Login", FileCreationMode.Private);
            Constants.mAzureDataService = new AzureDataService();
            Constants.mAzureDataService.Initialize();
        }
        protected override void OnResume()
        {
          
            base.OnResume();
            Task startupWork = new Task(async () => { await SimulateStartupAsync(); });
            startupWork.Start();
        }

        private async Task SimulateStartupAsync()
        {
            await Task.Delay(1000); // Simulate a bit of startup work.
            if (!sharedPreferences.Contains("Username"))
                StartActivity(new Intent(this, typeof(Activity1)));
            else
                StartActivity(new Intent(this, typeof(Navigation)));

            Finish();
        }
    }
}

