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
            
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Produto", "fa-table", "Produto");

            var product = Business.Product.GetProduct(idProduct);    
            return View("/Views/Product/ProductView.cshtml", product);           
        }
    }
}