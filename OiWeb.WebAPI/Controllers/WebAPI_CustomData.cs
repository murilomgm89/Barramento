using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OiWeb.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OiWeb.WebAPI.Controllers
{
    public class WebAPI_CustomData : Controller
    {

        public string GetJson()
        {
           var re =  Business.CustomData.GetCustomDataParent(1, "1", 1);
           return "OK";

        }
    }
}
