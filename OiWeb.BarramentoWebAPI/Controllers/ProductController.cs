using System;
using System.Collections.Generic;
using System.Linq;
using AttributeRouting.Web.Mvc;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OiWeb.BarramentoWebAPI.Models;

namespace OiWeb.BarramentoWebAPI.Controllers
{
    public class ProductController : Controller
    {
        [GET("/Product/Catalog/{idPriceGroup}")]
        public string Catalog(int idPriceGroup)
        {
            var product = Business.Product.GetCatalogProduct(idPriceGroup);            
            var EntityRegulations = Business.PlanRegulation.GetPlanRegulations(idPriceGroup);
            //{"catalog":{"NameProduct":{"category":"CategoryProduct ","plans":[{ "SKU":{"name":"NamePlan","price":"PricePlan"} }]}}} 

            List<dynamic> plans = new List<dynamic>();

            var sufix = "MES";
            var IsBuyLink = true;
            if(product.idProduct == 7){
                sufix = "";
                IsBuyLink = false;
            }
            //Populando list de planos com dynamic para vincular o SKU ao Slug -- List de dynamic
            foreach (var plan in product.PlanProducts.Where(p => p.isVisible == true))
            {
                if (plan.Prices.Where(p => p.idPriceGroup == idPriceGroup).Count() > 0)
                {                  

                    var priceFid =
                        new
                        {
                            value = plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 1).FirstOrDefault().value,
                            sufix = "MES"
                        };

                    var priceNoFid =
                        new
                        {
                            value = plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 2).Count() == 0 ?
                                            plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 1).FirstOrDefault().value :
                                            plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 2).FirstOrDefault().value,
                            sufix = "MES"
                        };

                    var priceComboFid =
                        new
                        {
                            value = plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 1).FirstOrDefault().valueCombo,
                            sufix = "MES"
                        };

                    var priceComboNoFid =
                        new
                        {
                            value = plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 2).Count() == 0 ?
                                            plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 1).FirstOrDefault().valueCombo :
                                            plan.Prices.Where(a => a.idPriceGroup == idPriceGroup && a.idPriceLoyalty == 2).FirstOrDefault().valueCombo,
                            sufix = "MES"
                        };
                     
                    
                    plans.Add(
                        new Dictionary<int, dynamic>()
                        {
                            {
                                plan.idPlan,
                                new
                                {                                   
                                    name = plan.name,                                    
                                    priceFid,
                                    priceNoFid,
                                    priceComboFid,
                                    priceComboNoFid,
                                    buyLink = plan.linkEcommerce
                                }
                            }
                        }
                    ); 
                }

            };
              
            List<dynamic> lines = new List<dynamic>();   
            List<dynamic> regulation = new List<dynamic>();
            List<dynamic> summary = new List<dynamic>();
            List<dynamic> others = new List<dynamic>();
            List<dynamic> accession = new List<dynamic>();
            
            //Populando list de planos com dynamic para vincular o SKU ao Slug -- List de dynamic
            foreach (var regulationItem in EntityRegulations)
            {
                if (regulationItem.type == "regulation")
                {
                    regulation.Add(new
                    {
                        name = regulationItem.name,
                        link = regulationItem.link
                    });
                }
                if (regulationItem.type == "summary")
                {
                    summary.Add(new
                    {
                        name = regulationItem.name,
                        link = regulationItem.link
                    });
                }
                if (regulationItem.type == "others")
                {
                    others.Add(new
                    {
                        name = regulationItem.name,
                        link = regulationItem.link
                    });
                }
                if (regulationItem.type == "accession")
                {
                    accession.Add(new
                    {
                        name = regulationItem.name,
                        link = regulationItem.link
                    });
                }
            };

            if (accession.Count() > 0)
            {
                lines.Add(new
                {
                    title = "Contrato de adesão",
                    links = accession
                });
            }            

            lines.Add(new
            {
                title = "Regulamentos das ofertas",
                links = regulation
            });
            
            lines.Add(new
            {
                title = "Sumário das ofertas",
                links = summary
            });
            
            lines.Add(new
            {
                title = "Outros",
                links = others
            });

            //lines.Add(new Dictionary<string, dynamic>() 
            //{                
            //    { 
            //        "summary", 
            //        summary
            //    }                
            //});
            //lines.Add(new Dictionary<string, dynamic>() 
            //{                
            //    { 
            //        "others", 
            //        others
            //    }                
            //});

            var contract = new
            {
                lines = lines
            };

            var defaultSKU = product.PlanProducts.Where(a => a.defaultSKU == true).FirstOrDefault().idPlan;

            //Populando dynamic raiz
            var catalog = new Dictionary<string, dynamic>() 
            {                
                { 
                    "catalog",  new Dictionary<string, dynamic>() 
                    {                
                        { 
                            product.name, 
                            new { 
                                defaultSKU = defaultSKU,
                                category = product.ProductFamily.name,
                                plans = plans,
                                contract
                            } 
                        }                
                    }                    
                }                
            };            
            return Newtonsoft.Json.JsonConvert.SerializeObject(catalog);        
        }
    }
}
