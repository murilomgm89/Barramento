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
        [GET("/City/CatalogPages/{idCity}/{idProduct}")]
        public string GetCity(int idCity, int? idProduct)
        {
            Entity.Product product = new Entity.Product();
            if (idProduct != null && idProduct != 15)
            {
                product = Business.Product.GetProduct(idProduct);
            }
            if(idProduct == 15){
                product.name = "InternetMovel";
            }
           
            var city = Business.City.GetCity(idCity);
            if (city == null)
                return "City null";

            var groupCustomDataPages = Business.GroupCustomDataPage.GetGroupCustomDataPage(city.idCity, idProduct);
            
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


            var priceGroupCity = Business.PriceGroupCities.GetPriceGroupCity(city.idCity, idProduct);
            foreach (var catalog in priceGroupCity)
            {
                Catalog.Add(catalog.Product.valueGroupPrice, catalog.idPriceGroup);
            } 

            var OldRegion =
                        new
                        {
                            value = city.oldRegion
                        };

            var cityRaiz = new Dictionary<string, dynamic>() 
            {                
                { 
                    "City",  new Dictionary<int, dynamic>() 
                    {                
                        { 
                            city.idCity, new Dictionary<string, dynamic>() 
                            {                
                                { 
                                    product.name,
                                    new { 
                                        Catalog,
                                        Pages,
                                        Common,
                                        Modal,
                                    } 
                                }                
                            }
                        }                
                    }
                }
            };            
            return Newtonsoft.Json.JsonConvert.SerializeObject(cityRaiz); 
        }

        [GET("/City/GenericPages/{idCity}")]
        public string GetCityGenericPage(int idCity)
        {
            var city = Business.City.GetCity(idCity);
            if (city == null)
                return "City null";

            var groupCustomDataPages = Business.GroupCustomDataPage.GetGroupCustomDataPage(city.idCity, null);

            var GenericPages = new Dictionary<string, dynamic>();            
            var Modal = new Dictionary<string, int>();
            var Common = new Dictionary<string, int>();            

            foreach (var page in groupCustomDataPages)
            {
                if (page.Page.isCommon != true)
                {   
                    var groupModalPages = Business.GroupModal.GetGroupModalPage(page.idPage, idCity);
                    if (groupModalPages.Count() > 0)
                    {
                        foreach (var itemPage in groupModalPages)
                        {
                            Modal.Add(itemPage.Modal.name, itemPage.idGroupModal);
                        }                         
                    }
                    if (Modal.Count == 0)
                    {
                        GenericPages.Add(page.Page.name, new
                        {
                            page.idGroup                           
                        });
                    }
                    else
                    {
                        GenericPages.Add(page.Page.name, new
                        {
                            page.idGroup,
                            Modal
                        });
                    }
                   
                    Modal = new Dictionary<string, int>();
                }
                else
                {
                    Common.Add(page.Page.name, page.idGroup);
                }
            }
            
            var OldRegion =
                        new
                        {
                            value = city.oldRegion
                        };

            var cityRaiz = new Dictionary<string, dynamic>() 
            {                
                { 
                    "City",  new Dictionary<int, dynamic>() 
                    {                
                        { 
                            city.idCity, new {                                
                                Common,
                                GenericPages
                            }
                        }                
                    }
                }
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(cityRaiz);
        }

        //[GET("/CityMap")]
        //public string GetCityMap()
        //{
        //    var cities = Business.City.GetCities();
        //    if (cities == null)
        //        return "City null";

        //    List<Array> data = new List<Array>();

        //    foreach (Entity.City city in cities)
        //    {
        //        string[] data;   


        //    }
            
        //    //Response.AppendHeader("Access-Control-Allow-Origin", "*");
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(cityRaiz);
        //}
    }
}
