
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : AppCompatActivity
    {
        private EditText edtIpAddress, edtUsername, edtPassword;
        private Button btnLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginLayout);

            edtIpAddress = FindViewById<EditText>(Resource.Id.edtIp);
            edtUsername = FindViewById<EditText>(Resource.Id.edtUsername);
            edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);



        }
    }
}

