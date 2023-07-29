
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Hardware.Lights;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AplikasiCitraLele.StaticDetails;
using Java.Util;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "ThresholdTestingActivity")]
    public class ThresholdTestingActivity : AppCompatActivity
    {
        private ImageView imgUnggahan;
        private Button btnProses;
        Android.Graphics.Bitmap bitmap;
        byte[] imageArray;
        Android.Content.Intent intent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ThresholdLayout);

            imgUnggahan = FindViewById<ImageView>(Resource.Id.imgUnggahan);
            btnProses = FindViewById<Button>(Resource.Id.btnProses);

            imageArray = SD_ImgProccess.ImgByte;

            bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imgUnggahan.SetImageBitmap(bitmap);

            btnProses.Click += BtnProses_Click;
        }

        private void BtnProses_Click(object sender, EventArgs e)
        {
            BitmapDrawable drawable = (BitmapDrawable)imgUnggahan.Drawable;
            Bitmap bmp = drawable.Bitmap;

            var mutableBitmap = Bitmap.CreateBitmap(bmp.Width, bmp.Height, bmp.GetConfig());

            var value = SD_ImgProccess.imgBiner;

            var value2 = SD_ImgProccess.imgBiner;

            var manise = SD_ImgProccess.ImgByte;

            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(manise, 0, imageArray.Length);

            var mutableBitmap2 = Bitmap.CreateBitmap(bitmap.Width, bitmap.Height, bitmap.GetConfig());

            int x = bmp.Width;
            int y = bmp.Height;

            int a, r, g, b;

            //looping dilasi
            int width = bmp.Width;
            int height = bmp.Height;

            int hitungPixel = 0;

            int totalPixel = width * height;
            int hasilAkhir = 0;
            int hitungPutih = 0;

            for (int l = 1; l < height - 1; l ++)
            {
                for (int k = 1; k < width - 1; k++)
                {
                    var val = value[k, l];
                        
                    if (val != 0)
                    {
                        var p = new Color(bmp.GetPixel(k, l));
                        a = p.A;
                        r = p.R;
                        g = p.G;
                        b = p.B;

                        int avg = (r + g + b) / 3;

                        int atas = k - 1;
                        mutableBitmap2.SetPixel(atas, l, Color.Rgb(avg, avg, avg));
                        //hitungPixel += 1;

                        int kanan = l + 1;
                        mutableBitmap2.SetPixel(k, kanan, Color.Rgb(avg, avg, avg));
                        //hitungPixel += 1;

                        int bawah = k + 1;
                        mutableBitmap2.SetPixel(bawah, l, Color.Rgb(avg, avg, avg));
                        //hitungPixel += 1;

                        int kiri = l - 1;
                        mutableBitmap2.SetPixel(k, kiri, Color.Rgb(avg, avg, avg));

                        //hitungPixel += 1;

                    }
                    else
                    {
                        var p = new Color(bmp.GetPixel(k, l));
                        a = p.A;
                        r = p.R;
                        g = p.G;
                        b = p.B;

                        a = 255;
                        r = 255;
                        g = 255;
                        b = 255;

                        int avg = (r + g + b) / 3;

                        mutableBitmap2.SetPixel(k, l, Color.Rgb(avg, avg, avg));

                        //hitungPutih += 1;
                    }
                }
            }

            //hitung yang mutablebitmap2

            totalPixel = 0;
            List<List<int>> myList;


            int[,] arr = new int[width,height];

            for (int l = 1; l < height - 1; l++)
            {
                for (int k = 1; k < width - 1; k++)
                {

                    var p = new Color(mutableBitmap2.GetPixel(k, l));
                    a = p.A;
                    r = p.R;
                    g = p.G;
                    b = p.B;

                    int avg = (r + g + b) / 3;

                    if (avg == 0)
                    {
                        arr[k, l] = 100;
                        hitungPixel += 1;

                    }
                    else
                    {
                        hitungPutih += 1;
                    }
                }
            }

            int widthManise = arr.GetLength(0);
            int heightManise = arr.GetLength(1);

            totalPixel = height * width;
            hasilAkhir = totalPixel - hitungPixel;

            using (var stream = new MemoryStream())
            {
                mutableBitmap2.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();
                SD_ImgProccess.ImgByte = bitmapData;
            }

            var axx = hasilAkhir;

            intent = new Android.Content.Intent(this, typeof(DilationActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

    }
}

