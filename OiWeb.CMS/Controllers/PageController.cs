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
    public class PageController : Controller
    {
        [GET("/Paginas")]
        public ActionResult GetPages()
        {   
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Páginas", "fa-table", "Páginas"); 
            var pages = Business.Page.GetPages();
            return View("/Views/Page/PagesView.cshtml", pages);           
        }

        [GET("/Paginas/Cadastro/Novo")]
        public ActionResult GetCreatePage()
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Criar Página", "fa-table", "Criar Página");
            return View("/Views/Page/PageCreateView.cshtml");
        }

        [POST("/Paginas/Cadastro/Novo")]
        public ActionResult CreatePage(PageViewModel page)
        {
            Entity.Page pageEntity = new Entity.Page
            {
                name = page.name,
                description = page.description,
                isCommon = page.isCommon,
                isActive = true,
                dtCreate = DateTime.Now
            };

            Business.Page.Create(pageEntity);
            return Redirect("/Paginas");
        }

        [GET("/Paginas/Editar/{idPage}")]
        public ActionResult GetEditPage(int idPage)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Editar Página", "fa-table", "Editar Página");
            
            var page = Business.Page.GetPage(idPage);
            return View("/Views/Page/PageEditView.cshtml", page);
        }

        [GET("/Paginas/Detalhes/{idPage}")]
        public ActionResult GetDetailsPage(int idPage)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Detalhes da Página", "fa-table", "Detalhes da Página");
            var page = Business.Page.GetGroupsPage(idPage);
            List<Entity.GroupCustomData> groupsList = new List<Entity.GroupCustomData>();
            foreach( var got in page.GroupCustomDataPages.Select(g => g.idGroup).Distinct()){
                groupsList.Add(Business.GroupCustomData.GetGroupCustomData(got));
            }

            PageViewModel pageViewModel = new PageViewModel();
            pageViewModel.page = page;
            pageViewModel.groups = groupsList;
            return View("/Views/Page/PageDetailsView.cshtml", pageViewModel);
        }

        [GET("/Paginas/Deletar/{idPage}")]
        public ActionResult DeletePage(int idPage)
        {
            Business.Page.Delete(idPage);
            return Redirect("/Paginas");
        }
        
        [POST("/Paginas/Editar")]
        public ActionResult EditPage(PageViewModel page)
        {            
            Entity.Page pageEntity = Business.Page.GetPage(page.idPage);
            pageEntity.name = page.name;
            pageEntity.description = page.description;
            pageEntity.isCommon = page.isCommon;
            pageEntity.isActive = page.isActive;
            Business.Page.Update(pageEntity);
            return Redirect("/Paginas");
        }

        [GET("/Paginas/isActive/{isActive}/{idPage}")]
        public ActionResult IsActivePage(bool isActive, int idPage)
        {
            var page = Business.Page.GetPage(idPage);
            page.isActive = isActive;
            Business.Page.Update(page);
            return Redirect("/Paginas");
        }        
    }
}