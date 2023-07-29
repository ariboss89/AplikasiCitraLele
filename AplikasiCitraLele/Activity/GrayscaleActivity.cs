
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Hardware.Lights;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Lifecycle;
using AplikasiCitraLele.StaticDetails;
using static Android.Content.ClipData;
using static Android.Webkit.WebStorage;
using static Xamarin.Essentials.Platform;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "GrayscaleActivity")]
    public class GrayscaleActivity : AppCompatActivity
    {
        private Button btnProccess;
        private ImageView imgGrayScale, imgThreshold;
        Android.Content.Intent intent;
        List<byte> listByte = new List<byte>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GrayScaleLayout);

            imgGrayScale = FindViewById<ImageView>(Resource.Id.imgGrayScale);
            btnProccess = FindViewById<Button>(Resource.Id.btnProses);

            byte[] imageArray = SD_ImgProccess.ImgByte;
            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            imgGrayScale.SetImageBitmap(bitmap);

            btnProccess.Click += BtnProccess_Click;

        }

        private void BtnProccess_Click(object sender, EventArgs e)
        {
            byte[] imageArray = SD_ImgProccess.ImgByte;

            Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);

            BitmapDrawable drawable = (BitmapDrawable)imgGrayScale.Drawable;
            Bitmap bmp = drawable.Bitmap;

            var mutableBitmap = Bitmap.CreateBitmap(bmp.Width, bmp.Height, bmp.GetConfig());

            // Variable for image brightness
            double avgBright = 0;
            for (int m = 0; m < bmp.Height; m++)
            {
                for (int l = 0; l < bmp.Width; l++)
                {
                    var p = new Color(bmp.GetPixel(l, m));

                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    var bright = GetBrightness(r, g, b);

                    // Get the brightness of this pixel
                    avgBright += bright;
                }
            }

            // Get the average brightness and limit it's min / max
            avgBright = avgBright / (bmp.Width * bmp.Height);
            avgBright = avgBright < .3 ? .3 : avgBright;
            avgBright = avgBright > .7 ? .7 : avgBright;

            // Convert image to black and white based on average brightness

            int x = 0, y = 0;

            x = bmp.Width;
            y = bmp.Height;

            List<int> listPixel = new List<int>();

            try
            {

                for (int j = 0; j < y; j++)
                {
                    for (int i = 0; i < x; i++)
                    {

                        var p = new Color(bmp.GetPixel(i, j));
                        int a = p.A;
                        int r = p.R;
                        int g = p.G;
                        int b = p.B;

                        int avg = (r + g + b) / 3;

                        listPixel.Add(avg);

                    }
                }
            }catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }

            int max = 0, min = 0, threshold = 0;

            max = listPixel.Max();
            min = listPixel.Min();

            threshold = (min+max) / 2;

            string texto = "";

            //int[,] arrBiner =new int[bmp.Width,bmp.Height];

            //test ubah ke byte
            byte[,] arrBiner =new byte[bmp.Width,bmp.Height];

            for (int j = 0; j < y; j++)
            {
                for (int i = 0; i < x; i++)
                {

                    var q = new Color(bmp.GetPixel(i, j));

                    int a = q.A;

                    if(a == 0)
                    {
                        var aaa = a;
                    }

                    int r = q.R;
                    int g = q.G;
                    int b = q.B;

                    int avg = (r + g + b) / 3;

                    //var bright = GetBrightness(r, g, b);

                    // Set this pixel to black or white based on threshold
                    // befor avgBright, now i try avg

                    //if (bright > avg) mutableBitmap.SetPixel(x, y, Color.White);
                    //if (bright>avgBright) mutableBitmap.SetPixel(x, y, Color.White);
                    //if (avg < 196) mutableBitmap.SetPixel(x, y, Color.Black);
                    //if (avg > bright) mutableBitmap.SetPixel(x, y, Color.Black);
                    //else mutableBitmap.SetPixel(x, y, Color.White);

                    if (avg < threshold)
                    {
                        avg = 0;

                        //mutableBitmap.SetPixel(i, j, Color.Black);

                    }
                    else
                    {
                        avg = 255;
                        //mutableBitmap.SetPixel(i, j, Color.White);
                    }

                    if(avg == 255)
                    {
                        arrBiner[i,j] = 0;

                    }
                    else
                    {
                        arrBiner[i, j] = 1;
                       
                    }

                    mutableBitmap.SetPixel(i, j, Color.Rgb(avg, avg, avg));
                }
            }

            SD_ImgProccess.imgBiner = new byte[x,y];

            SD_ImgProccess.imgBiner = arrBiner;

            SD_ImgProccess.imgBiner2 = arrBiner;

            SD_ImgProccess.ImgByte = null;

            byte[] ok = new byte[5];

            using (var stream = new MemoryStream())
            {
                mutableBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();
                SD_ImgProccess.ImgByte = bitmapData;
            }

            intent = new Android.Content.Intent(this, typeof(ThresholdTestingActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);

        }


        public double GetBrightness(float R, float G, float B)
        {
            float r = (float)R / 255.0f;
            float g = (float)G / 255.0f;
            float b = (float)B / 255.0f;

            float max, min;

            max = r; min = r;

            if (g > max) max = g;
            if (b > max) max = b;

            if (g < min) min = g;
            if (b < min) min = b;

            return (max + min) / 2;

            //return (0.2126 * R + 0.7152 * G + 0.0722 * B);
        }
    }
}

