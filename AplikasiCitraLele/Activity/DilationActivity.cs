
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AplikasiCitraLele.Models;
using AplikasiCitraLele.Services;
using AplikasiCitraLele.StaticDetails;
using Emgu.CV.Structure;
using Newtonsoft.Json;
using static Emgu.CV.VideoCapture;
using static Java.Util.Jar.Attributes;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "DilationActivity")]
    public class DilationActivity : AppCompatActivity
    {
        private ImageView imgUnggahan;
        private Button btnProses;
        private TextView txtTotal, txtKet;

        Android.Graphics.Bitmap bitmap;
        Android.Content.Intent intent;
        byte[] imageArray;
        tb_citra ctr = new tb_citra();
        CitraService csh = new CitraService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DilationLayout);

            imgUnggahan = FindViewById<ImageView>(Resource.Id.imgUnggahan);
            btnProses = FindViewById<Button>(Resource.Id.btnSimpan);
            txtTotal = FindViewById<TextView>(Resource.Id.txtTotal);
            txtKet = FindViewById<TextView>(Resource.Id.txtKet);

            imageArray = SD_ImgProccess.ImgByte;

            bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imgUnggahan.SetImageBitmap(bitmap);
            btnProses.Text = "SIMPAN";
            txtTotal.Text = SD_ImgProccess.total_pixel.ToString();
            txtKet.Text = SD_ImgProccess.keterangan;

            btnProses.Click += BtnProses_Click;

        }

        private void BtnProses_Click(object sender, EventArgs e)
        {
            SimpanCitra();
        }

        private void SimpanCitra()
        {
            try
            {
                tb_citra ctr = new tb_citra()
                {
                    gambar = SD_ImgProccess.ImgByte,
                    keterangan = txtKet.Text,
                    total_pixel = Convert.ToInt32(txtTotal.Text)
                };

                csh.SaveCitra(ctr);

                Toast.MakeText(this, "Data citra berhasil di tambahkan !!", ToastLength.Long).Show();

                Intent intent = new Intent(this, typeof(MainActivity));
                intent.SetFlags(ActivityFlags.NewTask);
                StartActivity(intent);

            }
            catch (Exception x)
            {
                Toast.MakeText(Application.Context, x.ToString(), ToastLength.Short).Show();
            }
        }

    }
}

