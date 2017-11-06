using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OiWeb.Entity;
using OiWeb.Business;



namespace OiWeb.JsonGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Qual categoria você deseja gerar?\n\nDigite:\n1. Cidades\n2. CustomData\n3. Catalogo");
            var option = Console.ReadKey();

            switch (option.KeyChar)
            {
                case '1':
                    {
                        Console.Clear();
                        GerarCidades();
                        break;
                    }
                case '2':
                    {
                        Console.Clear();
                        Console.WriteLine("escolheu 1");
                        break;
                    }
                case '3':
                    {
                        Console.Clear();
                        Console.WriteLine("escolheu 1");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Opção inválida!");

                        return;
                    }
            }

            Console.WriteLine("\nTecle algo para SAIR...");
            Console.ReadKey();
        }

        private static void GerarCidades()
        {
            Console.WriteLine("### GERAÇÃO DE ARQUIVOS JSON DAS CIDADES ### \n\n");
            Console.WriteLine(@"{0} - Os arquivos estarão no diretório VML SSD:\OiWebJson\json\city\", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            if (!Directory.Exists(@"C:\OiWebJson"))
            {
                Directory.CreateDirectory(@"C:\OiWebJson");
            }

            if (!Directory.Exists(@"C:\OiWebJson\json"))
            {
                Directory.CreateDirectory(@"C:\OiWebJson\json");
            }

            if (!Directory.Exists(@"C:\OiWebJson\json\city"))
            {
                Directory.CreateDirectory(@"C:\OiWebJson\json\city");
            }

            Console.WriteLine("{0} - Buscando dados...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            var cities = new List<Entity.City>();

            cities.AddRange(Business.City.GetCities());

            Console.WriteLine("{0} - Construindo arquivos...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            Parallel.ForEach(cities, (city) =>
            {
                VMLtWebClient wc = new VMLtWebClient();
                wc.Encoding = Encoding.UTF8;

                string json = wc.DownloadString("http://back2.tempsite.ws/API/City/" + city.idCity).Replace(@"\u0027", "'").Replace(@"\u0026", "&");

                TextWriter sw = new StreamWriter(@"C:\OiWebJson\json\city\" + city.idCity + ".json");
                sw.Write(json);
                sw.Close();
            });

            Console.WriteLine("{0} - Geração concluída.", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
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
