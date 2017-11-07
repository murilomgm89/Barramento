using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Handle;

namespace OiWeb.CMS.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveBond(HttpPostedFileBase file, string name)
        {
            //Arquivo valido para tratamento
            if (file.FileName.Contains(".csv") || file.FileName.Contains(".xlsx"))
            {
                
                return Redirect("~/Order/Index");
            }
            return Redirect("~/Order/Index");
        }
    }
}

