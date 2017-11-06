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
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Cadastro de Plano";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Novo Plano";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var product = Business.Product.GetProduct(idProduct);
            var plan = new Entity.PlanProduct();
            plan.Product = product;
            return View("/Views/Plan/PlanDetailsView.cshtml", plan);           
        }

        [GET("/Produto/Plano/{idPlan}/Alterar")]
        public ActionResult UpdatePlan(int idPlan)
        {
            BreadcrumbViewModel breadcrumbViewModel = new BreadcrumbViewModel();
            breadcrumbViewModel.H1 = "Editar Plano";
            breadcrumbViewModel.Icon = "fa-table";
            breadcrumbViewModel.Session = "Editar Plano";
            ViewBag.breadcrumbViewModel = breadcrumbViewModel;

            var plan = Business.PlanProduct.GetPlan(idPlan);
            return View("/Views/Plan/PlanEditDetailsView.cshtml", plan);
        }

        
        [POST("/SavePlan")]
        public RedirectResult SaveNewPlanProduct(SavePlanViewModels plan)
        {           
            var planEntity = new Entity.PlanProduct();
            planEntity.idPlan = plan.SKU;
            planEntity.idProduct = plan.idProduct;
            planEntity.description = plan.description;
            planEntity.isVisible = plan.isVisible;
            planEntity.defaultSKU = plan.defaultSKU;
            planEntity.name = plan.name;
            planEntity.dtCreate = DateTime.Now;
            Business.PlanProduct.Create(planEntity);
            return Redirect("/Produto/" + planEntity.idProduct);
        }

        [POST("/SaveUpdatePlan")]
        public RedirectResult SaveUpdatePlan(SavePlanViewModels plan)
        {
            var planEntity = new Entity.PlanProduct();
            planEntity.idPlan = plan.SKU;
            planEntity.idProduct = plan.idProduct;
            planEntity.description = plan.description;
            planEntity.isVisible = plan.isVisible;
            planEntity.defaultSKU = plan.defaultSKU;
            planEntity.name = plan.name;
            Business.PlanProduct.Update(planEntity);
            return Redirect("/Produto/" + planEntity.idProduct);
        }

        [POST("/SaveUpdatePlanPrice")]
        public RedirectResult SaveUpdatePricePlan(SavePriceViewModels price)
        {
            var priceEntity = new Entity.Price();
            priceEntity.idPlan = price.idPlan;            
            priceEntity.idPriceGroup = price.idPriceGroup;
            priceEntity.idPriceLoyalty = 1;
            priceEntity.idTypeClient = price.idTypeClient;
            priceEntity.idPaymentMethod = price.idPaymentMethod;
           
            priceEntity.value = price.valueFid;
            priceEntity.valueCombo = price.valueFidCombo;
            Business.Price.Update(priceEntity);
            
            priceEntity.idPriceLoyalty = 2;
            priceEntity.value = price.valueNoFid;
            priceEntity.valueCombo = price.valueNoFidCombo;
            Business.Price.Update(priceEntity);
            
            return Redirect("/Grupos/" + price.idPriceGroup);
        }
    }
}