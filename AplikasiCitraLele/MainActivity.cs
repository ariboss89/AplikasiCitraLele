using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android;
using Android.Widget;
using Android.Content;
using AplikasiCitraLele.Activity;

namespace AplikasiCitraLele
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher =false)]
    public class MainActivity : AppCompatActivity
    {
        private LinearLayout linCitra, linPengguna, linRiwayat, linPengaturan, linLogout;
        private Intent intent;

        readonly string[] permissionGroup =
        {
            Manifest.Permission.Internet,
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.AccessNetworkState,
            Manifest.Permission.Camera
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.MainLayout);

            linCitra = FindViewById<LinearLayout>(Resource.Id.linCitra);
            //linPengguna = FindViewById<LinearLayout>(Resource.Id.linPengguna);
            linRiwayat = FindViewById<LinearLayout>(Resource.Id.linRiwayat);
            //linPengaturan = FindViewById<LinearLayout>(Resource.Id.linPengaturan);
            //linLogout = FindViewById<LinearLayout>(Resource.Id.linLogout);

            linCitra.Click += LinCitra_Click;
            linRiwayat.Click += LinRiwayat_Click;

            RequestPermissions(permissionGroup, 0);
            
        }

        private void LinRiwayat_Click(object sender, EventArgs e)
        {
            intent = new Android.Content.Intent(this, typeof(HistoryActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            
        }

        private void LinCitra_Click(object sender, EventArgs e)
        {
            intent = new Android.Content.Intent(this, typeof(CitraActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}

