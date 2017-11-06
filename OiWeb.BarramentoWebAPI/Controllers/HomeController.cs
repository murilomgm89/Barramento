using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OiWeb.BarramentoWebAPI.Models;


namespace OiWeb.BarramentoWebAPI.Controllers
{
    public class HomeController : Controller
    {        
        public string Index()
        {
            var product = Business.Product.GetCatalogProduct(6);
            //{"catalog":{"NameProduct":{"category":"CategoryProduct ","plans":[{ "SKU":{"name":"NamePlan","price":"PricePlan"} }]}}} 

            dynamic[] plans = new dynamic[product.PlanProducts.Count()];
            dynamic[] ArrayPlans = new dynamic[product.PlanProducts.Count()];
            int indexArray = 0;
            //Populando array de planos com dynamic para vincular o SKU ao Slug -- Array de dynamic
            foreach (var plan in product.PlanProducts)
            {
                plans[indexArray] = new Dictionary<int, dynamic>()
                {
                    {
                        plan.idPlan,
                        new
                        {
                            name = plan.name,
                            price = plan.Prices.Where(a => a.idPriceGroup == 6).FirstOrDefault().value
                        }
                    }
                };
                
            }; 

            //Populando dynamic raiz
            var catalog = new Dictionary<string, dynamic>() 
            {                
                { 
                    "catalog",  new Dictionary<string, dynamic>() 
                    {                
                        { 
                            product.name, 
                            new { 
                                category = product.ProductFamily.name,
                                plans = plans
                            } 
                        }                
                    }
                }                
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(catalog);             
        }
    }
}
