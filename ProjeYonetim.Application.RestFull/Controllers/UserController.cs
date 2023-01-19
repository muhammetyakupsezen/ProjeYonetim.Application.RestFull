using ProjeYonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ProjeYonetim.Application.RestFull.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public HttpResponseMessage Get()
        {

            IEnumerable<string> SecretKeyValues;
            IEnumerable<string> TokenValues;
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Unauthorized;


            bool GetSecretKey = Request.Headers.TryGetValues("SecretKey", out SecretKeyValues);
            bool GetToken = Request.Headers.TryGetValues("Token", out TokenValues);

            if (GetSecretKey && GetToken)
            {
                string SecretKey = SecretKeyValues.First();
                string Token = TokenValues.First();

                if (HttpContext.Current.Application["SecretKey"].ToString() == SecretKey)  //secret key her uygulamaya bir tane verilir, databasede kontrol edilmeden önce clienttan gelen secret keyin application startta confige yazılan secret keyle aynı olup olmadığı kontrol edilir,aynıysa işlemler devam eder
                {

                    Business.KullaniciIslemleri kullaniciIslemleri = new Business.KullaniciIslemleri();
                    TResult result = kullaniciIslemleri.GetPersonDetail(Token, SecretKey);

                    if (!result.Success)
                    {
                        response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        response = Request.CreateResponse<TResult>(HttpStatusCode.OK, result);
                    }
                }



            }

            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(TKullaniciKisiIletisim KisiBilgileri)
        {
            Business.KullaniciIslemleri kullanici = new Business.KullaniciIslemleri();
             TResult result =   kullanici.Register(KisiBilgileri);
            var Response = Request.CreateResponse<TResult>(HttpStatusCode.OK, result);

            return Response;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}