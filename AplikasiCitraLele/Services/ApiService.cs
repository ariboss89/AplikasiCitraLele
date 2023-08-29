using System;
using Android.App;
using AplikasiCitraLele.Helper;

namespace AplikasiCitraLele.Services
{
    public class ApiService
    {
        AppPreference app = new AppPreference(Application.Context);

        public string ApiUrl()
        {
            string apiUrl = $"http://{app.getAccessKey("ip")}/api/";

            return apiUrl;
        }

        public string GetAllCitra()
        {
            string manise = $"{ApiUrl()}Citra/GetAllCitra";
            return $"{ApiUrl()}Citra/GetAllCitra";
        }

        public string SaveCitra()
        {
            return $"{ApiUrl()}Citra/SaveCitra";
        }

        public string DeleteCitra(int Id)
        {
            return $"{ApiUrl()}Citra/DeleteCitra?Id="+Id;
        }
    }
}

