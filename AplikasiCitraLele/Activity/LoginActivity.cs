
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AplikasiCitraLele.Helper;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "LoginActivity", MainLauncher =true)]
    public class LoginActivity : AppCompatActivity
    {
        private EditText edtIpAddress;
        private Button btnMasuk;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginLayout);

            edtIpAddress = FindViewById<EditText>(Resource.Id.edtIp);
            btnMasuk = FindViewById<Button>(Resource.Id.btnMasuk);

            btnMasuk.Click += BtnMasuk_Click;

        }

        private void BtnMasuk_Click(object sender, EventArgs e)
        {
            if (edtIpAddress.Text.Equals(""))
            {
                Toast.MakeText(this, "Silahkan input ip address terlebih dahulu !", ToastLength.Long).Show();
            }
            else
            {
                AppPreference ap = new AppPreference(Application.Context);

                var ipAddress = edtIpAddress.Text;

                ap.saveIP(ipAddress);

                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }
    }
}

