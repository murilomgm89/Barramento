﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;
using OiWeb.CMS.Extensions;

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

        [GET("/Grupos/CustomData/Detalhes/{idGroupCustomData}")]
        public ActionResult GetGroupsCustomDataInfo(int idGroupCustomData)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Detalhes Grupo Custom Data", "fa-table", "Detalhes Grupo Custom Data");
            var group = Business.GroupCustomData.GetGroupCustomData(idGroupCustomData);
            return View("/Views/GroupCustomData/GroupCustomDataInfoView.cshtml", group);
        }

        [GET("/Grupos/CustomData/Editar/{idGroup}")]
        public ActionResult GetGroup(int idGroup)
        {
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Editar Grupo", "fa-table", "Editar Grupo"); 
            
            var entities = Business.Page.GetPages().ToList();
            ViewBag.Pages = entities;

            var groups = Business.GroupCustomData.GetGroupCustomDatas().ToList();
            ViewBag.GroupCustomDatas = groups;
       
            return View("~/Views/GroupCustomData/VincularCustom.cshtml");
        }

        [GET("/Grupos/CustomData/Excluir/{idGroupCustomData}")]
        public ActionResult DeleteGroupCustomData(int idGroupCustomData)
        {
            Business.GroupCustomData.ExcludeCustomData(idGroupCustomData);
            return Redirect("/Grupos/CustomData");
        }

        [GET("/Grupos/CustomData/Cadastro/Editar/{idGroup}")]
        public ActionResult UpdateDetailsGroup(int idGroup)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Editar Grupo", "fa-table", "Editar Grupo");

            return View("/Views/GroupCustomData/GroupCustomDataEditView.cshtml", Business.GroupCustomData.GetGroupCustomData(idGroup));
        }

        [POST("/Grupos/CustomData/Cadastro/Editar")]
        public ActionResult UpdateDetailsGroup(GroupCustomDataViewModel group)
        {
            Entity.GroupCustomData entity = new Entity.GroupCustomData();
            entity.description = group.description;
            entity.name = group.name;
            entity.idGroup = group.idGroup;
            Business.GroupCustomData.Update(entity);
            return Redirect("/Grupos/CustomData");
        }

        [GET("/Grupos/CustomData/Cadastro/Novo")]
        public ActionResult GetCreateGroup()
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Novo Grupo", "fa-table", "Novo Grupo"); 

            return View("/Views/GroupCustomData/GroupCustomDataCreateView.cshtml");
        }

        [POST("/Grupos/CustomData/Cadastro/Novo")]
        public RedirectResult CreateGroup(Entity.GroupCustomData entity)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Novo Grupo", "fa-table", "Novo Grupo");
            Business.GroupCustomData.Create(entity);

            return Redirect("/Grupos/CustomData");
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
            Business.GroupCustomData.DesvinculaGroupPage(idGroup, idPage);
            return Redirect("/Paginas/detalhes/" + idPage);
        }
        
        //[POST("/Grupos/SaveGroup")]
        //public RedirectResult SaveNewGroup(Entity.PriceGroup group)
        //{
        //    Business.Groups.Save(group);
        //    return Redirect("/Grupos");
        //}

        [GET("/Custom/vincular/cidades")]
        public ActionResult VincularCustom()
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Vincular Cidade", "fa-table", "Vincular Cidade");

            var entities = Business.Page.GetPages().ToList();
            ViewBag.Pages = entities;

            var groups = Business.GroupCustomData.GetGroupCustomDatas().ToList();
            ViewBag.GroupCustomDatas = groups;

            return View();
        }

        [POST("/Custom/vincular/cidades/salvar")]
        public RedirectResult PostVincularCustom(GroupCustomDataPageViewModel entity)
        {
            //Quer dizer que tem excel para realizer a inserção
            if (entity.file != null)
            {
                //Deleta os pricesgroups existentes
                Business.GroupCustomDataPage.RemoveByIdGroupAndPage(entity.idGroup, entity.idPage);

                //verifica se é por dd ou por idcity
                //se for por idcity insere o range
                var datas = new List<int>();
                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Content/Upload/Excel/CustomDataPage"), "customDataPages" + "id_" + entity.idGroup + DateTime.Now.Day + ".csv");
                datas = entity.isByCity ? entity.file.GetIdCityByExcel(fileLocation) : entity.file.GetIdCityByDddInExcel(fileLocation);


                if (datas.Any())
                {
                    var entities = datas.Select(s => new Entity.GroupCustomDataPage()
                    {
                        idCity = s,
                        idPage = entity.idPage,
                        idGroup = entity.idGroup

                    }).ToList();

                    Business.GroupCustomDataPage.Insert(entities);
                }
            }
            
            return Redirect("~/Grupos/CustomData");
        }

    }
}