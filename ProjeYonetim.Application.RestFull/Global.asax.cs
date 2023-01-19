﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ProjeYonetim.Application.RestFull
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Application["SecretKey"] = System.Configuration.ConfigurationManager.AppSettings["SecretKey"].ToString();

        }
    }
}
