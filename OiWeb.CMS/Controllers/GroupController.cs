using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;
using OiWeb.CMS.Extensions;

namespace OiWeb.CMS.Controllers
{
    public class GroupsController : Controller
    {
        [GET("/Grupos")]
        public ActionResult GetGroups()
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Grupos";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Grupos";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var groups = Business.Groups.GetPriceGroups();

            return View("/Views/Group/GroupView.cshtml", groups);           
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
        [GET("/Grupos/{idPriceGroup}/regulamentos")]
        public ActionResult GetRegulationGroup(int idPriceGroup)
        {
            var breadcrumbViewModel = new BreadcrumbViewModel
            {
                H1 = "Regulamentos",
                Icon = "fa-table",
                Session = "Regulamento"
            };
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var group = Business.Groups.GetPriceGroup(idPriceGroup);
            var cities = Business.City.GetCities(idPriceGroup);
            var planregulations = Business.PlanRegulation.GetPlanRegulations(idPriceGroup);

            CitiesViewModel citiesViewModel = new CitiesViewModel
            {
                @group = @group,
                cities = cities,
                plans = @group.Product.PlanProducts
            };

            return View("/Views/Regulation/RegulationDetailsView.cshtml", planregulations.ToList());
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
            group.isActive = true;
            Business.Groups.Save(group);

            //Quer dizer que tem excel para realizer a inserção
            if (group.file != null)
            {
                //verifica se é por dd ou por idcity
                //se for por idcity insere o range

                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Content/Upload/Excel/PriceCity"), "pricegroupcities" + "id_" + group.idPriceGroup + DateTime.Now.Day + ".csv");
                if (group.isByCity)
                    group.file.InsertPriceGroupCitiesByExcelIdCity(fileLocation, group.idProduct, group.idPriceGroup);
                else
                    group.file.InsertPriceGroupCitiesByExcelDdd(fileLocation, group.idProduct, group.idPriceGroup);

            }

            return Redirect("/Grupos");
        }
        [POST("/Grupos/Cidades")]
        public RedirectResult SaveNewCities(Entity.PriceGroup group)
        {
            //Quer dizer que tem excel para realizer a inserção
            if (group.file != null)
            {
                //Deleta os pricesgroups existentes
                Business.PriceGroupCities.RemoveByIdPriceGroup(group.idPriceGroup);

                //verifica se é por dd ou por idcity
                //se for por idcity insere o range

                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Content/Upload/Excel/PriceCity"), "pricegroupcities" + "id_" + group.idPriceGroup + DateTime.Now.Day + ".csv");
                if (group.isByCity)
                    group.file.InsertPriceGroupCitiesByExcelIdCity(fileLocation, group.idProduct, group.idPriceGroup);
                else
                    group.file.InsertPriceGroupCitiesByExcelDdd(fileLocation, group.idProduct, group.idPriceGroup);

            }

            return Redirect("/Grupos/" + group.idPriceGroup);
        }
    }
}