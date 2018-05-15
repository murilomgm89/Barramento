using System;
using System.Collections.Generic;
using System.Linq;
using AttributeRouting.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OiWeb.Entity;
using System.Web.Mvc;

namespace OiWeb.BarramentoWebAPI.Controllers
{
    public class CityController : Controller
    {
        [GET("/City/{idCity}")]
        public string GetCity(int idCity)
        {
            var city = Business.City.GetCity(idCity);
            if (city == null)
                return "City null";

            var groupCustomDataPages = Business.GroupCustomDataPage.GetGroupCustomDataPage(city.idCity);
            
            var Pages = new Dictionary<string, int>();
            var Common = new Dictionary<string, int>();
            foreach (var page in groupCustomDataPages)
            {
                if (page.Page.isCommon == true)
                {
                    Common.Add(page.Page.name, page.idGroup);
                }
                else
                {
                    Pages.Add(page.Page.name, page.idGroup);
                }
            }

            var Modal = new Dictionary<string, Dictionary<string, int>>();            
            var modaisPage = new Dictionary<string, int>();

            foreach (var page in groupCustomDataPages)
            {
                var groupModalPages = Business.GroupModal.GetGroupModalPage(page.idPage, idCity);
                if (groupModalPages.Count() > 0)
                {
                    foreach (var itemPage in groupModalPages)
                    {
                        modaisPage.Add(itemPage.Modal.name, itemPage.idGroupModal);
                    }
                    Modal.Add(page.Page.name, modaisPage);
                    modaisPage = new Dictionary<string, int>();
                }                
            }
           
            var Catalog = new Dictionary<string, int>();


            var priceGroupCity = Business.PriceGroupCities.GetPriceGroupCity(city.idCity);
            foreach (var catalog in priceGroupCity)
            {
                Catalog.Add(catalog.Product.valueGroupPrice, catalog.idPriceGroup);
            } 

            var OldRegion =
                        new
                        {
                            value = city.oldRegion
                        };

            //Populando dynamic raiz
            var cityRaiz = new Dictionary<string, dynamic>() 
            {                
                { 
                    "City",  new Dictionary<int, dynamic>() 
                    {                
                        { 
                            city.idCity,
                            new { 
                                Pages,
                                Common,
                                Modal,
                                Catalog,
                                OldRegion
                            } 
                        }                
                    }
                }                
            };
            //Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Newtonsoft.Json.JsonConvert.SerializeObject(cityRaiz); 
        }
    }
}
