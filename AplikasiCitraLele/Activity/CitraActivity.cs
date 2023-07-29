
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using AndroidX.AppCompat.App;
using AplikasiCitraLele.Models;
using AplikasiCitraLele.StaticDetails;
using Java.IO;
using Plugin.Media;
using static System.Net.WebRequestMethods;
using static Xamarin.Essentials.Platform;
using File = System.IO.File;
using Intent = Android.Content.Intent;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "CitraActivity", MainLauncher = false)]
    public class CitraActivity : AppCompatActivity
    {
        private Button btnKamera, btnUnggah, btnProses;
        private ProgressBar progressFoto;
        private ImageView imgUnggahan;
        tb_citra tbc = new tb_citra();
        Intent intent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CitraLayout);

            btnKamera = FindViewById<Button>(Resource.Id.btnKamera);
            btnUnggah = FindViewById<Button>(Resource.Id.btnUnggah);
            btnProses = FindViewById<Button>(Resource.Id.btnProses);
            //progressFoto = FindViewById<ProgressBar>(Resource.Id.btnProses);
            imgUnggahan = FindViewById<ImageView>(Resource.Id.imgUnggahan);

            btnKamera.Click += BtnKamera_Click;
            btnUnggah.Click += BtnUnggah_Click;
            btnProses.Click += BtnProses_Click;

        }

        private void BtnProses_Click(object sender, EventArgs e)
        {
            if (tbc.gambar != null)
            {
                BitmapDrawable drawable = (BitmapDrawable)imgUnggahan.Drawable;
                Bitmap bmp = drawable.Bitmap;

                try
                {
                    int height = bmp.Height;
                    int width = bmp.Width;

                    var mutableBitmap = Bitmap.CreateBitmap(width, height, bmp.GetConfig());

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            var p = new Color(bmp.GetPixel(x, y));

                            int a = p.A;
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            int avg = (r + g + b) / 3;

                            mutableBitmap.SetPixel(x, y, Color.Argb(a, avg, avg, avg));

                        }
                    }
                    
                    using(var stream = new MemoryStream()) {
                        mutableBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                        byte[] bitmapData = stream.ToArray();

                        SD_ImgProccess.ImgByte = bitmapData;
                    }

                    intent = new Android.Content.Intent(this, typeof(GrayscaleActivity));
                    //intent = new Android.Content.Intent(this, typeof(RemoveBGActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);


                }
                catch (Exception x)
                {
                    Toast.MakeText(this, x.ToString(), ToastLength.Long).Show();
                }
            }
        }

        private async void BtnUnggah_Click(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    Toast.MakeText(this, "Upload not supported on this device", ToastLength.Short).Show();
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    CompressionQuality = 40
                });

                MemoryStream ms = new MemoryStream();
                file.GetStream().CopyTo(ms);
                byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                Android.Graphics.Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                imgUnggahan.SetImageBitmap(bitmap);

                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Headers.Add("X-Api-Key", "5EKcMA1NG9NZTwA7zPc7sK98");
                    formData.Add(new ByteArrayContent(imageArray), "image_file", "file.jpg");
                    formData.Add(new StringContent("auto"), "size");
                    var response = client.PostAsync("https://api.remove.bg/v1.0/removebg", formData).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        //string destFullName = file.Path;
                        //FileStream fileStream = new FileStream("file.jpg", FileMode.Create, FileAccess.Write, FileShare.None);
                        //response.Content.CopyToAsync(fileStream).ContinueWith((copyTask) => { fileStream.Close(); });
                        //response.Content.CopyToAsync(fileStrea

                        //bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                        //imgUnggahan.SetImageBitmap(bitmap);


                    }
                    else
                    {
                        //Console.WriteLine("Error: " + response.Content.ReadAsStringAsync().Result);
                    }
                }


                tbc.gambar = imageArray;

            }
            catch (Exception x)
            {
                Toast.MakeText(this, x.ToString(), ToastLength.Long).Show();
            }
        }

        private void BtnKamera_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();

            }catch(Exception)
            {

            }
        }
    }
}

