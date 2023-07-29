
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
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Loader.Content;
using AplikasiCitraLele.StaticDetails;
using Java.Security.Cert;
using static Android.Hardware.Camera;
using static Android.InputMethodServices.Keyboard;
using static Xamarin.Essentials.Platform;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "ThresholdActivity")]
    public class ThresholdActivity : AppCompatActivity
    {
        private ImageView imgUnggahan;
        private Button btnProses;
        Android.Graphics.Bitmap bitmap;
        byte[] imageArray;
        byte[,] arrBinary;
        byte[,] arrBinary2;
        byte[,] newArrBinary;
        Android.Content.Intent intent;

        List<byte> ListOfBytes = new List<byte>();

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

            var arrImg = imageArray.ToArray();

            arrBinary = SD_ImgProccess.imgBiner;

            arrBinary2 = SD_ImgProccess.imgBiner2;

            //StringBuilder SBuilder = new StringBuilder();

            //byte b1 = Convert.ToByte(SD_ImgProccess.ImgByte);
            
            //string s1 = string.Format("%8s", Convert.ToString(b1 & 0xFF)).Replace(' ', '0');

            var manise = "";

            for (int i = 0; i < imageArray.Length; i++)
            {
                manise += ""+ imageArray[i];
            }

            var kk = manise;

            var ii = "";

            //mencoba fungsi dilasi manise morfologi
            int[,] array2D = new int[10,10];
            array2D[0, 0] = 0; array2D[1, 0] = 0; array2D[2, 0] = 0; array2D[3, 0] = 0; array2D[4, 0] = 0;
            array2D[0, 1] = 0; array2D[1, 1] = 0; array2D[2, 1] = 0; array2D[3, 1] = 0; array2D[4, 1] = 0;
            array2D[0, 2] = 0; array2D[1, 2] = 0; array2D[2, 2] = 1; array2D[3, 2] = 1; array2D[4, 2] = 1;
            array2D[0, 3] = 0; array2D[1, 3] = 0; array2D[2, 3] = 0; array2D[3, 3] = 0; array2D[4, 3] = 0;
            array2D[0, 4] = 0; array2D[1, 4] = 0; array2D[2, 4] = 0; array2D[3, 4] = 1; array2D[4, 4] = 1;
            array2D[0, 5] = 0; array2D[1, 5] = 0; array2D[2, 5] = 0; array2D[3, 5] = 0; array2D[4, 5] = 0;
            array2D[0, 6] = 0; array2D[1, 6] = 0; array2D[2, 6] = 1; array2D[3, 6] = 1; array2D[4, 6] = 1;
            array2D[0, 7] = 0; array2D[1, 7] = 0; array2D[2, 7] = 0; array2D[3, 7] = 0; array2D[4, 7] = 0;
            array2D[0, 8] = 0; array2D[1, 8] = 0; array2D[2, 8] = 0; array2D[3, 8] = 0; array2D[4, 8] = 0;
            array2D[0, 9] = 0; array2D[1, 9] = 0; array2D[2, 9] = 0; array2D[3, 9] = 0; array2D[4, 9] = 0;

            array2D[5, 0] = 0; array2D[6, 0] = 0; array2D[7, 0] = 0; array2D[8, 0] = 0; array2D[9, 0] = 0;
            array2D[5, 1] = 0; array2D[6, 1] = 0; array2D[7, 1] = 0; array2D[8, 1] = 0; array2D[9, 1] = 0;
            array2D[5, 2] = 1; array2D[6, 2] = 0; array2D[7, 2] = 1; array2D[8, 2] = 0; array2D[9, 2] = 0;
            array2D[5, 3] = 0; array2D[6, 3] = 1; array2D[7, 3] = 0; array2D[8, 3] = 0; array2D[9, 3] = 0;
            array2D[5, 4] = 1; array2D[6, 4] = 0; array2D[7, 4] = 0; array2D[8, 4] = 0; array2D[9, 4] = 0;
            array2D[5, 5] = 0; array2D[6, 5] = 1; array2D[7, 5] = 0; array2D[8, 5] = 0; array2D[9, 5] = 0;
            array2D[5, 6] = 1; array2D[6, 6] = 0; array2D[7, 6] = 0; array2D[8, 6] = 0; array2D[9, 6] = 0;
            array2D[5, 7] = 0; array2D[6, 7] = 0; array2D[7, 7] = 0; array2D[8, 7] = 0; array2D[9, 7] = 0;
            array2D[5, 8] = 0; array2D[6, 8] = 0; array2D[7, 8] = 0; array2D[8, 8] = 0; array2D[9, 8] = 0;
            array2D[5, 9] = 0; array2D[6, 9] = 0; array2D[7, 9] = 0; array2D[8, 9] = 0; array2D[9, 9] = 0;


            //array tandingan
            int[,] array2D2 = new int[10, 10];
            array2D[0, 0] = 0; array2D[1, 0] = 0; array2D[2, 0] = 0; array2D[3, 0] = 0; array2D[4, 0] = 0;
            array2D[0, 1] = 0; array2D[1, 1] = 0; array2D[2, 1] = 0; array2D[3, 1] = 0; array2D[4, 1] = 0;
            array2D[0, 2] = 0; array2D[1, 2] = 0; array2D[2, 2] = 1; array2D[3, 2] = 1; array2D[4, 2] = 1;
            array2D[0, 3] = 0; array2D[1, 3] = 0; array2D[2, 3] = 0; array2D[3, 3] = 0; array2D[4, 3] = 0;
            array2D[0, 4] = 0; array2D[1, 4] = 0; array2D[2, 4] = 0; array2D[3, 4] = 1; array2D[4, 4] = 1;
            array2D[0, 5] = 0; array2D[1, 5] = 0; array2D[2, 5] = 0; array2D[3, 5] = 0; array2D[4, 5] = 0;
            array2D[0, 6] = 0; array2D[1, 6] = 0; array2D[2, 6] = 1; array2D[3, 6] = 1; array2D[4, 6] = 1;
            array2D[0, 7] = 0; array2D[1, 7] = 0; array2D[2, 7] = 0; array2D[3, 7] = 0; array2D[4, 7] = 0;
            array2D[0, 8] = 0; array2D[1, 8] = 0; array2D[2, 8] = 0; array2D[3, 8] = 0; array2D[4, 8] = 0;
            array2D[0, 9] = 0; array2D[1, 9] = 0; array2D[2, 9] = 0; array2D[3, 9] = 0; array2D[4, 9] = 0;

            array2D[5, 0] = 0; array2D[6, 0] = 0; array2D[7, 0] = 0; array2D[8, 0] = 0; array2D[9, 0] = 0;
            array2D[5, 1] = 0; array2D[6, 1] = 0; array2D[7, 1] = 0; array2D[8, 1] = 0; array2D[9, 1] = 0;
            array2D[5, 2] = 1; array2D[6, 2] = 0; array2D[7, 2] = 0; array2D[8, 2] = 0; array2D[9, 2] = 0;
            array2D[5, 3] = 0; array2D[6, 3] = 1; array2D[7, 3] = 0; array2D[8, 3] = 0; array2D[9, 3] = 0;
            array2D[5, 4] = 1; array2D[6, 4] = 0; array2D[7, 4] = 0; array2D[8, 4] = 0; array2D[9, 4] = 0;
            array2D[5, 5] = 0; array2D[6, 5] = 1; array2D[7, 5] = 0; array2D[8, 5] = 0; array2D[9, 5] = 0;
            array2D[5, 6] = 1; array2D[6, 6] = 0; array2D[7, 6] = 0; array2D[8, 6] = 0; array2D[9, 6] = 0;
            array2D[5, 7] = 0; array2D[6, 7] = 0; array2D[7, 7] = 0; array2D[8, 7] = 0; array2D[9, 7] = 0;
            array2D[5, 8] = 0; array2D[6, 8] = 0; array2D[7, 8] = 0; array2D[8, 8] = 0; array2D[9, 8] = 0;
            array2D[5, 9] = 0; array2D[6, 9] = 0; array2D[7, 9] = 0; array2D[8, 9] = 0; array2D[9, 9] = 0;

            //looping dilasi
            //int arrWidth = array2D.GetLength(0);
            //int arrHeight = array2D.GetLength(1);
            //int awal = arrWidth + 2;

            //int patok = (arrWidth * arrHeight)-(arrWidth+1);
            //int akhir = patok;
            ////var manis = new int[arrHeight,arrWidth];
            //var manis = new int[arrWidth, arrHeight];

            //for (int b = 1; b < arrHeight-1; b++)
            //{
            //    for (int a = 1; a < arrWidth-1; a++)
            //    {
            //        manis = array2D2;

            //        int value = array2D[a, b];

            //        if (value == 1)
            //        {
            //            int atas = a - 1;
            //            manis[atas, b] = 1;
            //            int kanan = b + 1;
            //            manis[a, kanan] = 1;
            //            int bawah = a + 1;
            //            manis[bawah, b] = 1;
            //            int kiri = b - 1;
            //            manis[a, kiri] = 1;
            //            manis[a, b] = value;
            //        }
            //    }
            //}

            //looping dilasi gambar asli ne
            int width = arrBinary.GetLength(0);
            int height = arrBinary.GetLength(1);
            newArrBinary = new byte[width, height];

            for (int b = 1; b < height - 1; b++)
            {
                for (int a = 1; a < width - 1; a++)
                {
                    newArrBinary = arrBinary2;

                    byte value = arrBinary[a, b];

                    if (value == 1)
                    {
                        int atas = a - 1;
                        newArrBinary[atas, b] = 1;
                        int kanan = b + 1;
                        newArrBinary[a, kanan] = 1;
                        int bawah = a + 1;
                        newArrBinary[bawah, b] = 1;
                        int kiri = b - 1;
                        newArrBinary[a, kiri] = 1;
                        newArrBinary[a, b] = value;
                    }
                }
            }

            var aaa = newArrBinary;
            string bbb = "aaaa";

            string gg = "";

            foreach(var a in imageArray)
            {
                var valueee = a.ToString();

                gg = valueee;
            }

            string aaax = gg;

        }

        private void BtnProses_Click(object sender, EventArgs e)
        {
            BitmapDrawable drawable = (BitmapDrawable)imgUnggahan.Drawable;
            Bitmap bmp = drawable.Bitmap;
            var mutableBitmap = Bitmap.CreateBitmap(bmp.Width, bmp.Height, bmp.GetConfig());

            int x = newArrBinary.GetLength(0);
            int y = newArrBinary.GetLength(1);
            int a, r, g, b;

            for (int j = 0; j < y; j++)
            {
                for (int i = 1; i < x; i++)
                {

                    //var q = new Color(bmp.GetPixel(i, j));

                    var value = newArrBinary[i, j];

                    a = 0; r = 0; g = 0; b = 0;

                    int avg = 0;

                    //if (value == 0)
                    //{
                    //    a = 255;
                    //    r = 255;
                    //    g = 255;
                    //    b = 255;

                    //    avg = (r + g + b) / 3;
                    //}
                    //else
                    //{
                    //    avg = 0;
                    //}

                    if (value == 1)
                    {
                        var p = new Color(bmp.GetPixel(i, j));
                        a = p.A;
                        r = p.R;
                        g = p.G;
                        b = p.B;

                        //a = 255;
                        //r = 255;
                        //g = 255;
                        //b = 255;

                        avg = (r + g + b) / 3;

                        mutableBitmap.SetPixel(i, j, Color.White);
                    }
                    else
                    {
                        var p = new Color(bmp.GetPixel(i, j));
                        a = p.A;
                        r = p.R;
                        g = p.G;
                        b = p.B;

                        avg = ((r + g + b) / 3);

                        //avg = 0;

                        mutableBitmap.SetPixel(i, j, Color.Black);
                    }


                    //mutableBitmap.SetPixel(i, j, Color.Rgb(avg, avg, avg));

                }

                var aaa = SD_ImgProccess.ImgByte;

                byte[] maniseeee;

                using (var stream = new MemoryStream())
                {
                    mutableBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    byte[] bitmapData = stream.ToArray();

                    maniseeee = bitmapData;
                }

                if (aaa != maniseeee)
                {
                    SD_ImgProccess.ImgByte = maniseeee;

                    intent = new Android.Content.Intent(this, typeof(DilationActivity));
                    intent.SetFlags(ActivityFlags.NewTask);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "APACIEH", ToastLength.Long).Show();
                }
            }

            //var img = bitmap;
            //String texto = "";

            //int arrWidth = arrBinary.GetLength(0);
            //int arrHeight = arrBinary.GetLength(1);

            //for (int i = 0; i < arrHeight; i++)
            //{
            //    for (int j = 0; j < arrWidth; j++)
            //    {

            //        var value = arrBinary[i, j];

            //        var pxl = img.GetPixel(i, j);

            //        if (img.GetPixel(j, i).ToString() == "255" && img.GetPixel(j, i).ToString() == "255" && img.GetPixel(j, i).ToString() == "255" && img.GetPixel(j, i).ToString() == "255")
            //        {
            //            texto = texto + "0";
            //        }
            //        else
            //        {
            //            texto = texto + "1";
            //        }
            //    }
            //    texto = texto + "\r\n"; // this is to make the enter between lines

            //}

            //string manise = texto;

            //------------------------------------------

            //var manise = Test(arrBinary);

            //bitmap = BitmapFactory.DecodeByteArray(manise, 0, manise.Length);
            //imgUnggahan.SetImageBitmap(bitmap);

        }

        private byte[] Test(int[,] arrBinary)
        {
            int arrWidth = arrBinary.GetLength(0);
            int arrHeight = arrBinary.GetLength(1);
            int avg = 0;
            byte[] bitmapData;

            var mutableBitmap = Bitmap.CreateBitmap(arrWidth, arrHeight, bitmap.GetConfig());

            for (int j = 0; j < arrHeight; j++)
            {
                for (int i = 0; i < arrWidth; i++)
                {

                    var valueArr = arrBinary[i, j];

                    if (valueArr == 1)
                    {
                        avg = 255;

                        mutableBitmap.SetPixel(i, j, Color.Rgb(avg, avg, avg));

                    }
                }
            }

            using (var stream = new MemoryStream())
            {
                mutableBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
                
            }

            var manise = bitmapData;

            return bitmapData;

        }
    }
}

