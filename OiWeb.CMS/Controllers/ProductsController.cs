using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;

namespace OiWeb.CMS.Controllers
{
    public class ProductsController : Controller
    {
        [GET("/Produto/{idProduct}")]
        public ActionResult GetProducts(int idProduct)
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Produto";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Produto";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var product = Business.Product.GetProduct(idProduct);    
            return View("/Views/Product/ProductView.cshtml", product);           
        }
    }
}