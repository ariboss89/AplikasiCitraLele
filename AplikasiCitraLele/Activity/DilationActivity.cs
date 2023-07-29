
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AplikasiCitraLele.StaticDetails;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "DilationActivity")]
    public class DilationActivity : AppCompatActivity
    {
        private ImageView imgUnggahan;
        private Button btnProses;
        Android.Graphics.Bitmap bitmap;
        Android.Content.Intent intent;
        byte[] imageArray;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DilationLayout);

            imgUnggahan = FindViewById<ImageView>(Resource.Id.imgUnggahan);
            btnProses = FindViewById<Button>(Resource.Id.btnProses);

            imageArray = SD_ImgProccess.ImgByte;

            bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imgUnggahan.SetImageBitmap(bitmap);

            btnProses.Text = "SIMPAN";

        }
    }
}

