using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;

namespace OiWeb.CMS.Controllers
{
    public class CitiesController : Controller
    {
        [GET("/Cidades")]
        public ActionResult GetCities()
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Cidades";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Cidades";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var cities = Business.City.GetCities();

            return View("/Views/City/CitiesView.cshtml", cities);           
        }

        [GET("/Cidades/{idCity}")]
        public ActionResult GetCity(int idCity)
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Cidades";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Cidades";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var city = Business.City.GetCity(idCity);
            city.PriceGroupCities = city.PriceGroupCities.OrderBy(pg => pg.idPriceGroup).ToList();
            return View("/Views/City/CityDetailsView.cshtml", city);
        }       
    }
}