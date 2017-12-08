using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OiWeb.Entity;
using OiWeb.Business;
using System.IO;
using System.Net;
using System.Timers;

namespace BotJsonGeneration
{
    class Program
    {
        public static System.Timers.Timer _timer;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Processo iniciado");
            _timer = new System.Timers.Timer();
            _timer.AutoReset = false;
            _timer.Interval = 300000; // Intervalo em milésimos
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(GerarPacote);
            _timer.Enabled = true;
            Console.ReadKey();
        }

        private static void GerarPacote(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            Console.Clear();
            GerarCidades();
            GerarCatalogo();
        }

        static void GerarCidades()
        { 
            Console.WriteLine("### GERAÇÃO JSON CITIES INICIADA - " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " ### \n\n");
            Console.WriteLine(@" Os arquivos estarão no diretório C:\JsonGeneration\API\Cities");

            if (!Directory.Exists(@"C:\JsonGeneration\API\Cities"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API\Cities");
            }

            var cities = new List<OiWeb.Entity.City>();

            cities.AddRange(OiWeb.Business.City.GetCities());

            Console.WriteLine("{0} - Construindo arquivos...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            Parallel.ForEach(cities, (city) =>
            {
                VMLtWebClient wc = new VMLtWebClient();
                wc.Encoding = Encoding.UTF8;

                string json = wc.DownloadString("http://back2.tempsite.ws/API/City/" + city.idCity).Replace(@"\u0027", "'").Replace(@"\u0026", "&");

                TextWriter sw = new StreamWriter(@"C:\JsonGeneration\API\Cities\" + city.idCity + ".json");
                sw.Write(json);
                sw.Close();
            });

            Console.WriteLine("{0} - Geração de Cities concluída.", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));            
        }

        private static void GerarCatalogo()
        {
            Console.WriteLine("### GERAÇÃO JSON CATALOGO ### \n\n");
            Console.WriteLine(@" Os arquivos estarão no diretório C:\JsonGeneration\API\Catalog");

            if (!Directory.Exists(@"C:\JsonGeneration\API\Catalog"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API\Catalog");
            }

            var prideGroups = new List<OiWeb.Entity.PriceGroup>();
          
            prideGroups.AddRange(OiWeb.Business.Groups.GetPriceGroups());

            Console.WriteLine("{0} - Construindo arquivos...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
           
            Parallel.ForEach(prideGroups, (prideGroup) =>
            {
                VMLtWebClient wc = new VMLtWebClient();
                wc.Encoding = Encoding.UTF8;

                string json = wc.DownloadString("http://back2.tempsite.ws/API/Product/Catalog/" + prideGroup.idPriceGroup).Replace(@"\u0027", "'").Replace(@"\u0026", "&");

                if (!Directory.Exists(@"C:\JsonGeneration\API\Catalog\" + prideGroup.Product.name))
                {
                    Directory.CreateDirectory(@"C:\JsonGeneration\API\Catalog\" + prideGroup.Product.name);
                }

                if (!Directory.Exists(@"C:\JsonGeneration\API\Catalog\" + prideGroup.Product.name + "\\" + prideGroup.idPriceGroup))
                {
                    Directory.CreateDirectory(@"C:\JsonGeneration\API\Catalog\" + prideGroup.Product.name + "\\" + prideGroup.idPriceGroup);
                }

                TextWriter sw = new StreamWriter(@"C:\JsonGeneration\API\Catalog\" + prideGroup.Product.name + "\\" + prideGroup.idPriceGroup + "\\" + prideGroup.Product.name + ".json");
                sw.Write(json);
                sw.Close();
            });
            Console.WriteLine("{0} - Geração de Catalogo concluída.", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            _timer.Enabled = true;
        }
    }

    public class VMLtWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 20 * 60 * 1000;
            return w;
        }
    }
}
