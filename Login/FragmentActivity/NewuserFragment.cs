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
    public class NewuserFragment : Fragment
    {

        public NewuserFragment()
        {

        }
        public static Fragment newInstance(Context context)
        {
            NewuserFragment busrouteFragment = new NewuserFragment();
            return busrouteFragment;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.NewuserFragment, null);


            //Intent i = new Intent(this, typeof(Addemployee));

            //buttonadd.Click += delegate
            //{
            //    StartActivity(i);
            //};
            //Image
            edittext_employeeimage = root.FindViewById<EditText>(Resource.Id.edittext_employeeimage);

            edittext_employeeimage.Click += delegate
            {
                GetImage(((b, p) =>
                {
                    edittext_employeeimage.Text = p;
                    //Toast.MakeText(this, "Found path: " + p, ToastLength.Long).Show();
                }));
            };

            //Joining Date
            edittext_joiningdate = root.FindViewById<EditText>(Resource.Id.edittext_joiningdate);

            edittext_joiningdate.Click += (sender, e) =>
            {
                DateTime today = DateTime.Today;
                DatePickerDialog dialog = new DatePickerDialog(this.Context, OnDateSet, today.Year, today.Month - 1, today.Day);
                dialog.DatePicker.MinDate = today.Millisecond;
                dialog.Show();
            };
            //spinner category
            spinner = root.FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this.Context, Resource.Array.category, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;


            return root;

        }
        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            edittext_joiningdate.Text = e.Date.ToLongDateString();
        }



        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            // string toast = string.Format("Selected car is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
        public delegate void OnImageResultHandler(bool success, string imagePath);

        protected OnImageResultHandler _imagePickerCallback;
        private EditText edittext_employeeimage;
        private EditText edittext_joiningdate;
        private Spinner spinner;
        private Intent Intent;


        public void GetImage(OnImageResultHandler callback)
        {
            if (callback == null)
            {
                throw new ArgumentException("OnImageResultHandler callback cannot be null.");
            }

            _imagePickerCallback = callback;
            InitializeMediaPicker();
        }

        public void InitializeMediaPicker()
        {
            Intent = new Android.Content.Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), 1000);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode != 1000) || (resultCode != Result.Ok) || (data == null))
            {
                return;
            }

            string imagePath = null;
            var uri = data.Data;
            try
            {
                imagePath = GetPathToImage(uri);
            }
            catch (Exception ex)
            {
                // Failed for some reason.
            }

            _imagePickerCallback(imagePath != null, imagePath);
            
        }


        private string GetPathToImage(Android.Net.Uri uri)
        {
            string doc_id = "";
            using (var c1 = Context.ContentResolver.Query(uri, null, null, null, null))
            {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = null;

            // The projection contains the columns we want to return in our query.
            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";

            using (var cursor = Activity.ManagedQuery(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null))
            {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }
            return path;

        }


    }

}

