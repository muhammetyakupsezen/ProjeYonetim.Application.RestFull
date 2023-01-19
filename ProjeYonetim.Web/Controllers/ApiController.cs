using Newtonsoft.Json;
using ProjeYonetim.Data;
using ProjeYonetim.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeYonetim.Web.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api

        [HttpPost]
        public ActionResult Login(string Username, string password)
        {

            TRestClient client = new TRestClient();
            string ResultData = client.GetToken(Username, password);

            if (ResultData != null)
            {
                if (ResultData != "")
                {
                    HttpCookie NameCookie = new HttpCookie("ProjeYonetim");
                    //  NameCookie.HttpOnly = true;
                    NameCookie.Values["Token"] = ResultData;
                    NameCookie.Expires = DateTime.Now.AddMinutes(20);
                    Response.Cookies.Add(NameCookie);
                    Session["Token"] = ResultData;
                    Session["Login"] = true;

                }

            }
            return Json(ResultData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetLoginUserDetails()
        {


            HttpCookie NameCookie = Request.Cookies["ProjeYonetim"];
            string CookieToken = NameCookie != null ? NameCookie.Value.Split('=')[1] : "undefined";
            string SessionToken = HttpContext.Session["Token"].ToString();
            TResult result = new TResult();
            VwKisiKullaniciIletisim PersonData = new VwKisiKullaniciIletisim();

            if (CookieToken == SessionToken)
            {
                TRestClient client = new TRestClient();

                 result = client.GetLoginUserDetail(HttpContext.Application["SecretKey"].ToString(), SessionToken);
                PersonData = JsonConvert.DeserializeObject<VwKisiKullaniciIletisim>(result.Data[0].ToString());
                PersonData.Sifre = "***";

            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }





    }
}