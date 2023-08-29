
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
using AplikasiCitraLele.Models;
using AplikasiCitraLele.Services;
using AplikasiCitraLele.StaticDetails;
using Java.Util;

namespace AplikasiCitraLele.Activity
{
    [Activity(Label = "HistoryActivity")]
    public class HistoryActivity : AppCompatActivity
    {
        ImageView imgArrow, imgCitra;
        Spinner spinCitra;
        TextView txtTotal, txtKet;

        List<tb_citra> listCitra = new List<tb_citra>();
        tb_citra tbc = new tb_citra();
        CitraService csh = new CitraService();
        List<string> listId = new List<string>();
        byte[] imageArray;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HistoryLayout);
            imgArrow = FindViewById<ImageView>(Resource.Id.imgArrow);
            imgCitra = FindViewById<ImageView>(Resource.Id.imgCitra);
            spinCitra = FindViewById<Spinner>(Resource.Id.spinCitra);
            txtTotal = FindViewById<TextView>(Resource.Id.txtTotal);
            txtKet = FindViewById<TextView>(Resource.Id.txtKet);

            listCitra = csh.ShowDataCitra();

            if (listCitra.Count != 0)
            {
                var DistinctItems = listCitra.GroupBy(x => x.Id).Select(y => y.First());

                foreach (var item in DistinctItems)
                {
                    listId.Add(item.Id.ToString());
                }

                ArrayAdapter<string> adapter = new ArrayAdapter<string>(Application.Context, Android.Resource.Layout.SimpleSpinnerDropDownItem, listId);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinCitra.Adapter = adapter;

            }

            spinCitra.ItemSelected += SpinCitra_ItemSelected;
            imgArrow.Click += ImgArrow_Click;
        }

        private void ImgArrow_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void SpinCitra_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int id = Convert.ToInt32(spinCitra.SelectedItem.ToString());

            Tampil(id);
        }

        private void Tampil(int Id)
        {
            listCitra = csh.ShowDataCitra().Where(x => x.Id == Id).ToList();

            if (listCitra.Count != 0)
            {

                imageArray = listCitra[0].gambar;

                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                imgCitra.SetImageBitmap(bitmap);

                txtTotal.Text = listCitra[0].total_pixel.ToString();
                txtKet.Text = listCitra[0].keterangan;
            }
        }
    }
}

