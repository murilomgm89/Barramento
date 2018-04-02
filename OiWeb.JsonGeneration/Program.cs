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
            Console.WriteLine("Qual categoria você deseja gerar?\n\nDigite:\n1. Cidades\n2. Catalogo");
            var option = Console.ReadKey();

            switch (option.KeyChar)
            {
                case '1':
                    {
                        Console.Clear();
                        EscolherCidades();
                        break;
                    }
                case '2':
                    {
                        Console.Clear();
                        GerarCatalogo();
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

        private static void EscolherCidades()
        {
            Console.WriteLine("Gerar Cities: \n\nDigite:\n1. Por Produto\n2. Paginas Genéricas");
            var option = Console.ReadKey();

            switch (option.KeyChar)
            {
                case '1':
                    {
                        Console.Clear();
                        EscolherProduto();
                        break;
                    }
                case '2':
                    {
                        Console.Clear();
                        GerarGenericPages();
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

        private static void EscolherProduto()
        {
            Console.WriteLine("Qual produto deseja gerar os cities?\n\nDigite:\n1. TV\n2. Fixo\n3. BandaLarga\n4. Combo\n5. Controle\n6. PosPago\n7. PrePago\n8. InternetMovel");
            var option = Console.ReadKey();

            switch (option.KeyChar)
            {
                case '1':
                    {
                        Console.Clear();
                        GerarCidades(1,"TV");
                        break;
                    }
                case '2':
                    {
                        Console.Clear();
                        GerarCidades(2, "Fixo");
                        break;
                    }
                case '3':
                    {
                        Console.Clear();
                        GerarCidades(3, "BandaLarga");
                        break;
                    }
                case '4':
                    {
                        Console.Clear();
                        GerarCidades(10, "ComboResidencial");
                        GerarCidades(11, "ComboConectado");
                        GerarCidades(12, "ComboSolucaoCompleta");
                        GerarCidades(13, "ComboTvFixo");
                        GerarCidades(14, "ComboPlay");
                        break;
                    }
                case '5':
                    {
                        Console.Clear();
                        GerarCidades(8, "Controle");
                        break;
                    }
                case '6':
                    {
                        Console.Clear();
                        GerarCidades(6, "PosPago");
                        break;
                    }
                case '7':
                    {
                        Console.Clear();
                        GerarCidades(7, "PrePago");
                        break;
                    }
                case '8':
                    {
                        Console.Clear();
                        GerarCidades(15, "InternetMovel");
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
        
        private static void GerarGenericPages()
        {
            Console.WriteLine("### GERAÇÃO CITIES-GenericPages INICIADA ### \n\n");
            Console.WriteLine(@"{0} - Os arquivos estarão no diretório C:\JsonGeneration\API\Cities\GenericPages", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            if (!Directory.Exists(@"C:\JsonGeneration"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration");
            }

            if (!Directory.Exists(@"C:\JsonGeneration\API"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API");
            }

            if (!Directory.Exists(@"C:\JsonGeneration\API\Cities"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API\Cities");
            }

            if (!Directory.Exists(@"C:\JsonGeneration\API\Cities\GenericPages"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API\Cities\GenericPages");
            }            

            Console.WriteLine("{0} - Buscando dados...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            var cities = new List<Entity.City>();

            cities.AddRange(Business.City.GetCities());

            Console.WriteLine("{0} - Construindo arquivos...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            Parallel.ForEach(cities, (city) =>
            {
                VMLtWebClient wc = new VMLtWebClient();
                wc.Encoding = Encoding.UTF8;

                string json = wc.DownloadString("http://back2.tempsite.ws/API/City/GenericPages/" + city.idCity).Replace(@"\u0027", "'").Replace(@"\u0026", "&");

                TextWriter sw = new StreamWriter(@"C:\JsonGeneration\API\Cities\GenericPages\" + city.idCity + ".json");
                sw.Write(json);
                sw.Close();
            });

            Console.WriteLine("{0} - Geração de Cities-GenericPages concluída.", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private static void GerarCidades(int idProduct, string name)
        {
            
            Console.WriteLine("### GERAÇÃO JSON CITIES-Product INICIADA ### \n\n");
            Console.WriteLine(@"{0} - Os arquivos estarão no diretório C:\JsonGeneration\API\Cities\" + name, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            if (!Directory.Exists(@"C:\JsonGeneration"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration");
            }

            if (!Directory.Exists(@"C:\JsonGeneration\API"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API");
            }

            if (!Directory.Exists(@"C:\JsonGeneration\API\Cities"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API\Cities");
            }
            
            if (!Directory.Exists(@"C:\JsonGeneration\API\Cities\" + name))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API\Cities\" + name);
            }

            Console.WriteLine("{0} - Buscando dados...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            var cities = new List<Entity.City>();

            cities.AddRange(Business.City.GetCities());

            Console.WriteLine("{0} - Construindo arquivos...", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            Parallel.ForEach(cities, (city) =>
            {
                VMLtWebClient wc = new VMLtWebClient();
                wc.Encoding = Encoding.UTF8;

                string json = wc.DownloadString("http://back2.tempsite.ws/API/City/CatalogPages/" + city.idCity + "/" + idProduct).Replace(@"\u0027", "'").Replace(@"\u0026", "&");
                
                switch (idProduct)
                {
                    case 1:
                        {
                            TextWriter sw1 = new StreamWriter(@"C:\JsonGeneration\API\Cities\TV\" + city.idCity + ".json");
                            sw1.Write(json);
                            sw1.Close();
                            break;
                        }
                    case 2:
                        {
                            TextWriter sw2 = new StreamWriter(@"C:\JsonGeneration\API\Cities\Fixo\" + city.idCity + ".json");
                            sw2.Write(json);
                            sw2.Close();
                            break;
                        }
                    case 3:
                        {
                            TextWriter sw3 = new StreamWriter(@"C:\JsonGeneration\API\Cities\BandaLarga\" + city.idCity + ".json");
                            sw3.Write(json);
                            sw3.Close();
                            break;
                        }
                    case 10:
                        {
                            TextWriter sw10 = new StreamWriter(@"C:\JsonGeneration\API\Cities\ComboResidencial\" + city.idCity + ".json");
                            sw10.Write(json);
                            sw10.Close();
                            break;
                        }
                    case 11:
                        {
                            TextWriter sw11 = new StreamWriter(@"C:\JsonGeneration\API\Cities\ComboConectado\" + city.idCity + ".json");
                            sw11.Write(json);
                            sw11.Close();
                            break;
                        }
                    case 12:
                        {
                            TextWriter sw12 = new StreamWriter(@"C:\JsonGeneration\API\Cities\ComboSolucaoCompleta\" + city.idCity + ".json");
                            sw12.Write(json);
                            sw12.Close();
                            break;
                        }
                    case 13:
                        {
                            TextWriter sw13 = new StreamWriter(@"C:\JsonGeneration\API\Cities\ComboTvFixo\" + city.idCity + ".json");
                            sw13.Write(json);
                            sw13.Close();
                            break;
                        }
                    case 14:
                        {
                            TextWriter sw14 = new StreamWriter(@"C:\JsonGeneration\API\Cities\ComboPlay\" + city.idCity + ".json");
                            sw14.Write(json);
                            sw14.Close();
                            break;
                        }                    
                    case 8:
                        {
                            TextWriter sw8 = new StreamWriter(@"C:\JsonGeneration\API\Cities\Controle\" + city.idCity + ".json");
                            sw8.Write(json);
                            sw8.Close();
                            break;
                        }
                    case 6:
                        {
                            TextWriter sw6 = new StreamWriter(@"C:\JsonGeneration\API\Cities\PosPago\" + city.idCity + ".json");
                            sw6.Write(json);
                            sw6.Close();
                            break;
                        }
                    case 7:
                        {
                            TextWriter sw7 = new StreamWriter(@"C:\JsonGeneration\API\Cities\PrePago\" + city.idCity + ".json");
                            sw7.Write(json);
                            sw7.Close();
                            break;
                        }
                    case 15:
                        {
                            TextWriter sw15 = new StreamWriter(@"C:\JsonGeneration\API\Cities\InternetMovel\" + city.idCity + ".json");
                            sw15.Write(json);
                            sw15.Close();
                            break;
                        }
                    default:
                        {
                            if (!Directory.Exists(@"C:\JsonGeneration\API\Cities\ProdNaoIndentificado" + name))
                            {
                                Directory.CreateDirectory(@"C:\JsonGeneration\API\Cities\ProdNaoIndentificado" + name);
                            }
                            TextWriter sw = new StreamWriter(@"C:\JsonGeneration\API\Cities\ProdNaoIndentificado\" + city.idCity + ".json");
                            sw.Write(json);
                            sw.Close();
                            return;
                        }
                } 
            });

            Console.WriteLine("{0} - Geração de Cities concluída.", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        private static void GerarCatalogo()
        {
            Console.WriteLine("### GERAÇÃO JSON CATALOGO ### \n\n");
            Console.WriteLine(@" Os arquivos estarão no diretório C:\JsonGeneration\API\Catalog");


            if (!Directory.Exists(@"C:\JsonGeneration"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration");
            }

            if (!Directory.Exists(@"C:\JsonGeneration\API"))
            {
                Directory.CreateDirectory(@"C:\JsonGeneration\API");
            }

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
