using System;
using Android.Widget;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using AplikasiCitraLele.Models;
using Newtonsoft.Json;
using Android.App;

namespace AplikasiCitraLele.Services
{
	public class CitraService
	{
        ApiService api = new ApiService();
        List<tb_citra> listAlternatif = new List<tb_citra>();
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response;
        tb_citra tba = new tb_citra();

        public List<tb_citra> ShowDataCitra()
        {
            listAlternatif = new List<tb_citra>();

            try
            {
                httpClient = new HttpClient();
                response = httpClient.GetAsync(api.GetAllCitra()).GetAwaiter().GetResult();
                string result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                listAlternatif = JsonConvert.DeserializeObject<List<tb_citra>>(result);

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long).Show();
            }

            return listAlternatif;
        }

        public async void SaveCitra(tb_citra ctr)
        {
            try
            {
                tba = new tb_citra();
                httpClient = new HttpClient();
                string url = api.SaveCitra();
                var uri = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(ctr);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(uri, content);
                var message = response.Content.ReadAsStringAsync();

                var msg = message.Result.ToString();

                if (msg.Contains("Success"))
                {
                    var ari = "aaa";
                }
                else
                {
                    var manise = "aaaa";
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, "Save Citra Failed ! Due "+ex, ToastLength.Short).Show();
            }
        }

        public async void DeleteAlternatif(int Id)
        {
            try
            {
                HttpClient myClient = new HttpClient();
                string url = api.DeleteCitra(Id);
                var uri = new Uri(url);
                myClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = await myClient.DeleteAsync(uri);

                Toast.MakeText(Application.Context, "Data Citra Deleted", ToastLength.Long).Show();

            }
            catch (Exception)
            {
                Toast.MakeText(Application.Context, "Delete Citra Failed !", ToastLength.Short).Show();
            }
        }
    }
}

