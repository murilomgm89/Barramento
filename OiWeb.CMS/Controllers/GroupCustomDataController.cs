using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;

namespace OiWeb.CMS.Controllers
{
    public class GroupCustomDataController : Controller
    {
        [GET("/Grupos/CustomData")]
        public ActionResult GetGroupsCustomData()
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Grupos Custom Data";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Grupos Custom Data";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var groups = Business.GroupCustomData.GetGroupCustomDatas();

            return View("/Views/Group/GroupCustomDataView.cshtml", groups);           
        }

        [GET("/Grupos/{idPriceGroup}")]
        public ActionResult GetGroup(int idPriceGroup)
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Grupo";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Grupo";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var group = Business.Groups.GetPriceGroup(idPriceGroup);
            var cities = Business.City.GetCities(idPriceGroup);

            CitiesViewModel citiesViewModel = new CitiesViewModel();
            citiesViewModel.group = group;
            citiesViewModel.cities = cities;
            citiesViewModel.plans = group.Product.PlanProducts;           

            return View("/Views/Group/GroupDetailsView.cshtml", citiesViewModel);
        }

        [GET("/Grupos/Cadastro/Novo")]
        public ActionResult GetCreateGroup()
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Novo Grupo";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Novo Grupo";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            return View("/Views/Group/GroupCreateView.cshtml");
        }

        [GET("/Grupos/{idPriceGroup}/{idPlan}/Precos")]
        public ActionResult GetUpdatePrices(int idPriceGroup, int idPlan)
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Alteração de Precificação";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Alteração de Precificação";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var prices = Business.Price.GetPrices(idPlan, idPriceGroup);
            var plan = new Entity.PlanProduct();
            plan.Prices = prices;
            plan.idPlan = idPlan;
            ViewBag.idPriceGroup = idPriceGroup;

            return View("/Views/Plan/PlanEditPriceView.cshtml", plan);
        }
        [GET("/Grupos/{idPriceGroup}/{idPlan}/Excluir")]
        public ActionResult GetDeletePrices(int idPriceGroup, int idPlan)
        {
            Business.Price.Delete(idPlan, idPriceGroup); 
            return Redirect("/Grupos/" + idPriceGroup);
        }


        [POST("/Grupos/SaveGroup")]
        public RedirectResult SaveNewGroup(Entity.PriceGroup group)
        {
            Business.Groups.Save(group);
            return Redirect("/Grupos");
        }
    }
}