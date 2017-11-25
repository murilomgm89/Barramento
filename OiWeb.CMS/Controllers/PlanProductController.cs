using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Models;
using AttributeRouting.Web.Mvc;

namespace OiWeb.CMS.Controllers
{
    public class PlanProductController : Controller
    {
        [GET("/Produto/{idProduct}/Plano")]
        public ActionResult NewPlanProduct(int idProduct)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Cadastro de Plano", "fa-table", "Novo Plano"); 

            var product = Business.Product.GetProduct(idProduct);
            var plan = new Entity.PlanProduct {Product = product};
            return View("/Views/Plan/PlanDetailsView.cshtml", plan);           
        }

        [GET("/Produto/Plano/{idPlan}/Alterar")]
        public ActionResult UpdatePlan(int idPlan)
        {
            ViewBag.breadcrumbViewModel = new BreadcrumbViewModel("Editar Plano", "fa-table", "Editar Plano"); 

            var plan = Business.PlanProduct.GetPlan(idPlan);
            return View("/Views/Plan/PlanEditDetailsView.cshtml", plan);
        }

        
        [POST("/SavePlan")]
        public RedirectResult SaveNewPlanProduct(SavePlanViewModels plan)
        {
            var planEntity = new Entity.PlanProduct
            {
                idPlan = plan.SKU,
                idProduct = plan.idProduct,
                description = plan.description,
                isVisible = plan.isVisible,
                defaultSKU = plan.defaultSKU,
                name = plan.name,
                dtCreate = DateTime.Now
            };
            Business.PlanProduct.Create(planEntity);
            return Redirect("/Produto/" + planEntity.idProduct);
        }

        [POST("/SaveUpdatePlan")]
        public RedirectResult SaveUpdatePlan(SavePlanViewModels plan)
        {
            var planEntity = new Entity.PlanProduct
            {
                idPlan = plan.SKU,
                idProduct = plan.idProduct,
                description = plan.description,
                isVisible = plan.isVisible,
                defaultSKU = plan.defaultSKU,
                name = plan.name
            };
            Business.PlanProduct.Update(planEntity);
            return Redirect("/Produto/" + planEntity.idProduct);
        }

        [POST("/SaveUpdatePlanPrice")]
        public RedirectResult SaveUpdatePricePlan(SavePriceViewModels price)
        {
            var priceEntity = new Entity.Price
            {
                idPlan = price.idPlan,
                idPriceGroup = price.idPriceGroup,
                idPriceLoyalty = 1,
                idTypeClient = price.idTypeClient,
                idPaymentMethod = price.idPaymentMethod,
                value = price.valueFid,
                valueCombo = price.valueFidCombo
            };


            Business.Price.Update(priceEntity);
            
            priceEntity.idPriceLoyalty = 2;
            priceEntity.value = price.valueNoFid;
            priceEntity.valueCombo = price.valueNoFidCombo;
            Business.Price.Update(priceEntity);
            
            return Redirect("/Grupos/" + price.idPriceGroup);
        }
    }
}