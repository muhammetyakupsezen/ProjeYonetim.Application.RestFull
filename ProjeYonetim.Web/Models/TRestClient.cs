using Newtonsoft.Json;
using ProjeYonetim.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeYonetim.Web.Models
{
    public class TRestClient
    {
        private RestClient client;

        // private string Url = "https://localhost:44308/Api/";

        public TRestClient()
        {

        }


        public string Test()
        {
            client = new RestClient(TSiteSettings.ApiUrl + "//Token/12");

            var request = new RestRequest();

            var response = client.Get(request);


            return response.Content;

        }


        public string GetToken(string Username, string Password)
        {
            string SecretKey = HttpContext.Current.Application["SecretKey"].ToString();
            string result = "";
            ApiUser apiUser = new ApiUser();
            apiUser.Username = Username;
            apiUser.Password = Password;

            client = new RestClient(TSiteSettings.ApiUrl + "//Token");
            RestRequest restRequest = new RestRequest();
            restRequest.AddHeader("SecretKey", SecretKey);
            restRequest.AddJsonBody<ApiUser>(apiUser);


            var response = client.Post(restRequest);
            var t = response.Content;

            TResult ResultData = JsonConvert.DeserializeObject<TResult>(response.Content);
            if (ResultData.Success)
            {
                result = ResultData.Data[0].ToString();
            }

            return result;
        }



        public TResult Register(TKullaniciKisiIletisim KisiBilgileri)
        {
            TResult result = new TResult();

            //  string JsonKisiBilgileri = JsonConvert.SerializeObject(KisiBilgileri);
            client = new RestClient(TSiteSettings.ApiUrl + "//User");
            var request = new RestRequest();
            request.AddJsonBody(KisiBilgileri);
            //  request.Method = Method.Post;
            var response = client.Post(request);
            result = JsonConvert.DeserializeObject<TResult>(response.Content);

            return result;
        }


        public TResult GetLoginUserDetail(string SecretKey, string Token)
        {
            TResult result = new TResult();


            RestClient restClient = new RestClient(TSiteSettings.ApiUrl + "//User//" ); 

            var request = new RestRequest();
            request.AddHeader("SecretKey", SecretKey);
            request.AddHeader("Token", Token);

            var response = restClient.Get(request);

            result = JsonConvert.DeserializeObject<TResult>(response.Content);

            return result;
        }



    }
}