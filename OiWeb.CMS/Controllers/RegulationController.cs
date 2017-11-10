using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using OiWeb.CMS.Models;

namespace OiWeb.CMS.Controllers
{

    public class RegulationController : Controller
    {
        [GET("regulamentos")]
        public ActionResult Index()
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Regulamentos";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Regulamentos";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var regulations = Business.PlanRegulation.GetPlanRegulations();

            return View(regulations);
        }

        [GET("regulamentos/criar")]
        public ActionResult Create()
        {

            var groups = Business.Groups.GetPriceGroups();
            ViewBag.Groups = groups;

            return View();
        }

        [POST("regulamentos/salvar")]
        public RedirectResult Save(Entity.PlanRegulation entity)
        {

            if (entity.file != null)
            {
                var fileLocation = Server.MapPath("~/Content/Upload/Regulation/" + entity.file.FileName);
                entity.file.SaveAs(fileLocation);
            }

            entity.link = "/Content/Upload/Regulation/" + entity.file.FileName;
            Business.PlanRegulation.Insert(entity);
            
            return Redirect("/regulamentos");
        }

        [GET("regulamentos/{idPlanRegulation}/Excluir")]
        public RedirectResult Excluir(int idPlanRegulation)
        {
            if (!idPlanRegulation.Equals(0))
            {
                Business.PlanRegulation.Remove(idPlanRegulation);

            }

            return Redirect("~/Grupos/" + idPlanRegulation + "/regulamentos");
        }

        [GET("regulamentos/{idPlanRegulation}/alterar")]
        public ActionResult Alterar(int idPlanRegulation)
        {
            var data = Business.PlanRegulation.GetById(idPlanRegulation);
            
            var groups = Business.Groups.GetPriceGroups();
            ViewBag.Groups = groups;
            
            return View(data);
        }

        [POST("regulamentos/{idPlanRegulation}/alterar")]
        public RedirectResult Update(Entity.PlanRegulation entity)
        {
            if (!entity.idPlanRegulation.Equals(0))
            {
                Business.PlanRegulation.Update(entity);

            }
            return Redirect("~/Grupos/" + entity.idPlanRegulation + "/regulamentos");
        }
    }
}