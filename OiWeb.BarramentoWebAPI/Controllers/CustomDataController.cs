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
    public class CustomDataController : Controller
    {
        [GET("/CustomData/{nameComponent}/{idGroup}")]
        public ActionResult Custom(string nameComponent,int idGroup)
        {
            CustomDataViewModel mockupViewModel = new CustomDataViewModel();
            var customDataPage = Business.CustomData.GetCustomDataPage(nameComponent,1,idGroup);
            if (customDataPage == null)
                return View("~/Views/Json/Error.cshtml");
            mockupViewModel.identifierParent.nameComponent = nameComponent;
            mockupViewModel.identifierParent.identifiers = new List<CustomDataViewModel.Identifiers>();
            foreach (Entity.CustomData EntityIdentifier in customDataPage)
            { 
                CustomDataViewModel identifierModel = new CustomDataViewModel();
                identifierModel.identifiers.nameComponent = EntityIdentifier.ComponentType.identifier;
                identifierModel.identifiers.value = EntityIdentifier.value;                   
                mockupViewModel.identifierParent.identifiers.Add(identifierModel.identifiers);                    
            }
            
            return View("~/Views/Json/Mockup.cshtml", mockupViewModel);           
        }
    }
}
