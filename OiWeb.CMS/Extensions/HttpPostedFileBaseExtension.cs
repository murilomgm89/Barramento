using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using OiWeb.CMS.Handle;
using OiWeb.Entity;

namespace OiWeb.CMS.Extensions
{
    public static class HttpPostedFileBaseExtension
    {

        public static void InsertPriceGroupCitiesByExcelDdd(this HttpPostedFileBase file, string fileLocation, int idProduct, int idPriceGroup)
        {
            //Arquivo valido para tratamento
            if (file.FileName.Contains(".csv") || file.FileName.Contains(".xlsx"))
            {

                string columName = "City";
                var names = columName.Split(';');

                if (System.IO.File.Exists(fileLocation))
                    System.IO.File.Delete(fileLocation);

                var dt = new DataTable();
                file.SaveAs(fileLocation);

                //Caso for csv
                if (fileLocation.Contains(".csv"))
                    dt = DataTableHandle.GetDataTabletFromCsvFile(fileLocation);


                if (fileLocation.Contains(".xlsx"))
                    using (var reader = new ExcelDataReader(fileLocation))
                        dt.Load(reader);

                var lista = new List<PriceGroupCity>();
                foreach (var s in dt.AsEnumerable())
                {
                    PriceGroupCity data;
                    try
                    {
                        if(string.IsNullOrEmpty(s[names[0].Trim()].ToString()))
                            continue;

                        var ddd = Convert.ToInt32(s[names[0].Trim()].ToString());

                        if(ddd.Equals(0))
                            continue;

                        var cities = Business.City.GetCititesByDdd(ddd);
                        var listInt = cities.Select(x => x.idCity).ToList();

                        foreach (var item in listInt)
                        {

                            data = new Entity.PriceGroupCity
                            {
                                idCity = item,
                                idProduct = idProduct,
                                idPriceGroup = idPriceGroup
                            };
                            lista.Add(data);
                        }
                       
                    }
                    catch
                    {
                        //supressed
                        continue;
                    }
                    
                }

                //Atualiza as ordens

                Business.PriceGroupCities.Insert(lista);
            }
        }

        public static void InsertPriceGroupCitiesByExcelIdCity(this HttpPostedFileBase file, string fileLocation, int idProduct, int idPriceGroup)
        {
            //Arquivo valido para tratamento
            if (file.FileName.Contains(".csv") || file.FileName.Contains(".xlsx"))
            {
              

                string columName = "City";
                var names = columName.Split(';');

                if (System.IO.File.Exists(fileLocation))
                    System.IO.File.Delete(fileLocation);

                var dt = new DataTable();
                file.SaveAs(fileLocation);

                //Caso for csv
                if (fileLocation.Contains(".csv"))
                    dt = DataTableHandle.GetDataTabletFromCsvFile(fileLocation);


                if (fileLocation.Contains(".xlsx"))
                    using (var reader = new ExcelDataReader(fileLocation))
                        dt.Load(reader);

                var lista = new List<PriceGroupCity>();
                foreach (var s in dt.AsEnumerable())
                {
                    PriceGroupCity data;
                    try
                    {
                        data = new Entity.PriceGroupCity
                        {
                            idCity = Convert.ToInt32(s[names[0].Trim()].ToString()),
                            idProduct = idProduct,
                            idPriceGroup = idPriceGroup
                        };
                    }
                    catch
                    {
                        //supressed
                        continue;
                    }
                    lista.Add(data);
                }

                //Atualiza as ordens

                Business.PriceGroupCities.Insert(lista);


            }
        }

    }
}