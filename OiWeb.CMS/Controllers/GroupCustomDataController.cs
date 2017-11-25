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
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Grupos Custom Data", "fa-table", "Grupos Custom Data");
            
            var groups = Business.GroupCustomData.GetGroupCustomDatas().ToList();

            return View("/Views/GroupCustomData/GroupCustomDataView.cshtml", groups);           
        }

        [GET("/Grupos/CustomData/Editar/{idGroup}")]
        public ActionResult GetGroup(int idGroup)
        {
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Editar Grupo", "fa-table", "Editar Grupo"); 

            var group = Business.GroupCustomData.GetGroupCustomData(idGroup);
            var cities = Business.City.GetCitiesCustomData(idGroup);
            var pages = Business.Page.GetPagesInGroupCustomData(idGroup);

            CitiesViewModel citiesViewModel = new CitiesViewModel();
            citiesViewModel.groupCustomData = group;
            citiesViewModel.cities = cities;
            citiesViewModel.pages = pages;
       
            return View("/Views/GroupCustomData/GroupCustomDataDetailsView.cshtml", citiesViewModel);
        }

        [GET("/Grupos/CustomData/Cadastro/Novo")]
        public ActionResult GetCreateGroup()
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Novo Grupo", "fa-table", "Novo Grupo"); 

            return View("/Views/GroupCustomData/GroupCustomDataCreateView.cshtml");
        }

        [POST("/Grupos/CustomData/Cadastro/Novo")]
        public ActionResult CreateGroup()
        {   
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Novo Grupo", "fa-table", "Novo Grupo");

            return View("/Views/Group/GroupCreateView.cshtml");
        }

        [GET("/Grupos/CustomData/Paginas/isActive/{isActive}/{idGroup}")]
        public ActionResult IsActiveCustomData(bool isActive, int idGroup)
        {
            var customData = Business.GroupCustomData.GetGroupCustomData(idGroup);
            customData.isActive = isActive;
            Business.GroupCustomData.Update(customData);
            return Redirect("/Grupos/CustomData");
        }

        [GET("/Grupos/CustomData/Paginas/Desvincular/{idPage}/{idGroup}")]
        public ActionResult DesvincularPageGroupCustomData(int idPage, int idGroup)
        {
            var customData = Business.GroupCustomData.GetGroupCustomData(idGroup);           
            Business.GroupCustomData.Update(customData);
            return Redirect("/Grupos/CustomData");
        }


        [POST("/Grupos/SaveGroup")]
        public RedirectResult SaveNewGroup(Entity.PriceGroup group)
        {
            Business.Groups.Save(group);
            return Redirect("/Grupos");
        }
    }
}