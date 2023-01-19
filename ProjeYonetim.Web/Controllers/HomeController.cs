using ProjeYonetim.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeYonetim.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
           // TRestClient client = new TRestClient();
           // ApiUser user = new ApiUser();

           // user.Username = "yakup sezen";
           // user.Password = "1234";

           //var Token =  client.GetToken(user.Username, user.Password);


            return View();
        }


        [HttpGet]
        public ActionResult GetTest()
        {
            TRestClient client = new TRestClient();
               string ResultData =  client.Test();

            return Json(ResultData, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult PostTest(string name, string surname)
        {


            return Json(name + " " + surname , JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult PostTest()
        {
            TRestClient restClient = new TRestClient();
          //  restClient.Test();
            string ResultData = restClient.Test();

            return Json(ResultData,JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public ActionResult Register()
        {
            TRestClient client = new TRestClient();
          

            return Json(JsonRequestBehavior.AllowGet);

        }




    }
}