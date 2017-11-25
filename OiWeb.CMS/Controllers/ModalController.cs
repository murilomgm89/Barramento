using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using OiWeb.CMS.Extensions;
using OiWeb.CMS.Models;

namespace OiWeb.CMS.Controllers
{
    public class ModalController : Controller
    {
        [GET("/Modal")]
        public ActionResult Index()
        {

            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Modal", "fa-table", "Modal");

            var datas = Business.GroupModal.GetAll();

            return View("~/Views/Modal/ModalDetailsView.cshtml", datas);
        }

        [GET("/Modal/criar")]
        public ActionResult Create()
        {

            var data = Business.GroupModal.GetAllGroupModal();
            ViewBag.GroupModals = data;

            var entities = Business.Page.GetPages().ToList();
            ViewBag.Pages = entities;


            return View("~/Views/Modal/ModalCreateView.cshtml");
        }

        [GET("/Modal/{idModal}/Alterar")]
        public ActionResult Alterar(int idModal)
        {
            var data = Business.GroupModal.GetById(idModal);
            return View(data);
        }

        [GET("/Modal/{idModal}")]
        public ActionResult Detalhes(int idModal)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Detalhes do Modal", "fa-table", "Detalhes do Modal");

            var data = Business.GroupModal.GetById(idModal);

          
            return View(data);
        }

        [GET("/Grupos/Modal/{idGroupModal}")]
        public ActionResult DetalhesGroup(int idGroupModal)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Detalhes do Grupo Modal", "fa-table", "Detalhes do Grupo Modal");

            var data = Business.GroupModal.GetByIdGroupModal(idGroupModal);


            return View("~/Views/GroupModal/DetalhesGroup.cshtml", data);
        }

        [GET("/Modal/{idModal}/Excluir")]
        public ActionResult Excluir(int idModal)
        {
            Business.GroupModal.ExcludeModal(idModal);

            return Redirect("~/Modal");
        }


        [POST("/Modal/Alterar")]
        public RedirectResult PostAlterar(Entity.Modal data)
        {
            Business.GroupModal.UpdateModal(data);

            return Redirect("~/Modal");
        }


        [POST("/Modal/salvar")]
        public RedirectResult PostCreate(Entity.Modal entity)
        {

            if (string.IsNullOrEmpty(entity.name))
                throw new Exception("Nome deve ser preenchido");


            Business.GroupModal.Insert(entity);

            return Redirect("~/Modal");
        }

        [GET("/Grupos/Modal")]
        public ActionResult IndexGrupos()
        {

            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Grupos de Modal", "fa-table", "Grupos de Modal");

            var datas = Business.GroupModal.GetAllGroupModal();

            return View("~/Views/GroupModal/GroupModalDetailsView.cshtml", datas);
        }

        [GET("/Grupos/Modal/criar")]
        public ActionResult PostGroupCreate()
        {

            var entities = Business.Page.GetPages().ToList();
            ViewBag.Pages = entities;


            return View("~/Views/GroupModal/GroupModalCreateView.cshtml");
        }

        [GET("/Grupos/Modal/{idGroupModal}/Alterar")]
        public ActionResult AlterarGroupModal(int idGroupModal)
        {
            var data = Business.GroupModal.GetByIdGroupModal(idGroupModal);

            return View("~/Views/GroupModal/Alterar.cshtml", data);
        }

        [POST("/Grupos/Modal/Alterar")]
        public RedirectResult PostAlterarGroupModal(Entity.GroupModal entity)
        {
            //Função de Alterar
            Business.GroupModal.UpdateGroupModal(entity);

            return Redirect("~/Grupos/Modal");
        }

        [GET("/Grupos/Modal/{idGroupModal}/excluir")]
        public RedirectResult Exclude(int idGroupModal)
        {
            Business.GroupModal.ExcludeGroupModal(idGroupModal);

            return Redirect("~/Grupos/Modal");
        }


        [POST("/Grupos/Modal/salvar")]
        public RedirectResult PostGroupCreate(Entity.GroupModal entity)
        {

            Business.GroupModal.InsertGroupModal(entity);

            //Quer dizer que tem excel para realizer a inserção
            if (entity.file != null)
            {
                //verifica se é por dd ou por idcity
                //se for por idcity insere o range
                var datas = new List<int>();
                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Content/Upload/Excel/GroupModal"), "GroupModal" + "id_" + entity.idGroupModal + DateTime.Now.Day + ".csv");
                if (entity.isByCity)
                    datas = entity.file.GetIdCityByExcel(fileLocation);
                else
                    datas = entity.file.GetIdCityByDddInExcel(fileLocation);

                if (datas.Any())
                {
                    var entities = datas.Select(s => new Entity.GroupModalCity()
                    {
                        idCity = s,
                        idGroupModal = entity.idGroupModal

                    }).ToList();

                    Business.GroupModalCities.Insert(entities);
                }


            }

            return Redirect("~/Grupos/Modal");
        }

        [GET("/Modal/vincular/grupos")]
        public ActionResult VincularModal()
        {
            var data = Business.GroupModal.GetAllGroupModal();
            ViewBag.GroupModals = data;

            var entities = Business.Page.GetPages().ToList();
            ViewBag.Pages = entities;

            var modals = Business.GroupModal.GetAll();
            ViewBag.Modals = modals;

            return View();
        }

        [POST("/Modal/vincular/grupos/salvar")]
        public RedirectResult PostVincularModal(Entity.GroupModalPage entity)
        {
            Business.GroupModalPage.Insert(entity);
            return Redirect("~/Modal");
        }
    }
}