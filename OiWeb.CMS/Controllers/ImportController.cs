using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OiWeb.CMS.Handle;

namespace OiWeb.CMS.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveBond(HttpPostedFileBase file, string name)
        {
            //Arquivo valido para tratamento
            if (file.FileName.Contains(".csv") || file.FileName.Contains(".xlsx"))
            {
                string date = DateTime.Now.Day.ToString();
                date += DateTime.Now.Hour.ToString();
                date += DateTime.Now.Millisecond;
                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Upload/Excel/PriceCity"), name + "id_" + DateTime.Now.Day + "_" + date + ".csv");

                string columName = "Numero do pedido;Status Do Pedido;Ibm;Protocolo;Data da entrega";
                var names = columName.Split(';');

                if (System.IO.File.Exists(fileLocation))
                    System.IO.File.Delete(fileLocation);

                var dt = new DataTable();
                file.SaveAs(fileLocation);

                //Caso for csv
                if (file.FileName.Contains(".csv"))
                    dt = DataTableHandle.GetDataTabletFromCsvFile(fileLocation);


                if (file.FileName.Contains(".xlsx"))
                    using (var reader = new ExcelDataReader(fileLocation))
                        dt.Load(reader);

                //var lista = new List<Entity.Order>();
                //foreach (var s in dt.AsEnumerable())
                //{
                //    Entity.Order obj = new Order();
                //    try
                //    {
                //        obj = new Entity.Order();


                //        obj.IdOrder = Convert.ToInt32(s[names[0].Trim()].ToString());

                //        if (!string.IsNullOrEmpty(s[names[1]].ToString()))
                //            obj.IdStatusOrder = Convert.ToInt32(s[names[1]]);

                //        if (!string.IsNullOrEmpty(s[names[4].Trim()].ToString()))
                //        {
                //            string dt1 = s[names[4]].ToString().Split('/')[2] + "-" +
                //                         s[names[4]].ToString().Split('/')[1] + "-" +
                //                         s[names[4]].ToString().Split('/')[0];

                //            obj.DateDelivery = Convert.ToDateTime(dt1);
                //        }


                //    }
                //    catch (Exception ex)
                //    {
                //        //supressed
                //        continue;
                //    }
                //    lista.Add(obj);
                //}

                //Atualiza as ordens


                return Redirect("~/Order/Index");
            }
            return Redirect("~/Order/Index");
        }
    }
}

