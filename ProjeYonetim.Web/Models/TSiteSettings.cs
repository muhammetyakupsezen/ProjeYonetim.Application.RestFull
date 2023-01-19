using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjeYonetim.Web.Models
{
    public static class TSiteSettings
    {
        public static string ApiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"].ToString();

    }
}