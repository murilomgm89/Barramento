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
            breadcrumbViewModel.H1 = "Grupos";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Grupos";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var groups = Business.Groups.GetPriceGroups();

            return View(groups);
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