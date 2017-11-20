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

            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Grupos", "fa-table", "Grupos"); ;

            var groups = Business.Groups.GetPriceGroups();

            return View("/Views/Group/GroupView.cshtml", groups);           
        }

        [GET("/Grupos/{idPriceGroup}")]
        public ActionResult GetGroup(int idPriceGroup)
        {
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Grupo", "fa-table", "Grupo");

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
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Regulamentos", "fa-table", "Regulamento"); ;

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
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Novo Grupo", "fa-table", "Novo Grupo"); 

            return View("/Views/Group/GroupCreateView.cshtml");
        }

        [GET("/Grupos/{idPriceGroup}/{idPlan}/Precos")]
        public ActionResult GetUpdatePrices(int idPriceGroup, int idPlan)
        {
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Alteração de Precificação", "fa-table", "Alteração de Precificação"); 

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