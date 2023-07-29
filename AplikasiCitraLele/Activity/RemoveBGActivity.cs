
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Java.Nio;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "RemoveBGActivity")]
    public class RemoveBGActivity : AppCompatActivity
    {
        private ImageView imgUnggah;
        private Button btnRemove;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RemoveBGLayout);

            imgUnggah = FindViewById<ImageView>(Resource.Id.imgUnggahan);
            btnRemove = FindViewById<Button>(Resource.Id.btnRemove);

            btnRemove.Click += BtnRemove_Click;

        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            var imgByte = SD_ImgProccess.ImgByte;

            //using (var client = new HttpClient())
            //using (var formData = new MultipartFormDataContent())
            //{
            //    formData.Headers.Add("X-Api-Key", "5EKcMA1NG9NZTwA7zPc7sK98");
            //    formData.Add(new ByteArrayContent(imgByte), "image_file", "file.jpg");
            //    //formData.Add(new StringContent("https://www.remove.bg/example.jpg"), "image_url");
            //    formData.Add(new StringContent("auto"), "size");
            //    var response = client.PostAsync("https://api.remove.bg/v1.0/removebg", formData).Result;

            //    if (response.IsSuccessStatusCode)
            //    {
            //        FileStream fileStream = new FileStream("no-bg.png", FileMode.Create, FileAccess.Write, FileShare.None);
            //        response.Content.CopyToAsync(fileStream).ContinueWith((copyTask) => { fileStream.Close(); });

            //    }
            //    else
            //    {
            //        Console.WriteLine("Error: " + response.Content.ReadAsStringAsync().Result);
            //    }
            //}
        }
    }
}

