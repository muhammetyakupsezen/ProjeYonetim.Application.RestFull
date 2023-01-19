using ProjeYonetim.Data;
using ProjeYonetim.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeYonetim.Web.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        public ActionResult Index()
        {
            return View();
        } 
        
        public ActionResult IndexPanel()
        {
            HttpCookie NameCookie = Request.Cookies["ProjeYonetim"];
            string CookieToken = NameCookie != null ? NameCookie.Value.Split('=')[1] : "undefined";
            string SessionToken = HttpContext.Session["Token"].ToString();

            if (CookieToken != SessionToken)
            {
                Session["Token"] = "";
                Session["Login"] = false;
                Response.Redirect("~/", true);
            }
            //else
            //{
            //    TRestClient restClient = new TRestClient();
            // TResult result =  restClient.GetLoginUserDetail(HttpContext.Application["SecretKey"].ToString(),SessionToken);


            //}


            return View();
        }
    }
}