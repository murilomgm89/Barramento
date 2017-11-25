using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;

namespace OiWeb.CMS.Controllers
{
    public class HomeController : Controller
    {
        [GET("/Home")]
        public ActionResult Index()
        {
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Home", "", "");
            return View();
        }
    }
}