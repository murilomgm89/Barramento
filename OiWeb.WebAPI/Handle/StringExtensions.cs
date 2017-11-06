using System.Linq;

namespace OiWeb.WebAPI.Handle
{
    public static class StringExtensions
    {
        public static string ToString(this string from, string mask)
        {
            if (string.IsNullOrEmpty(from))
                return string.Empty;

            if (mask.Count(m => m == '#') > from.Length)
                return from;

            string novoValor = string.Empty;
            int posicao = 0;
            for (int i = 0; mask.Length > i; i++)
            {
                if (mask[i] == '#')
                {
                    if (from.Length > posicao)
                    {
                        novoValor = novoValor + from[posicao];
                        posicao++;
                    }
                    else
                        break;
                }
                else
                {
                    if (from.Length > posicao)
                        novoValor = novoValor + mask[i];
                    else
                        break;
                }
            }

            return novoValor;
        }    
    }

    public class StringFunctions
    {
        public static string[] GetDayAndMonthName(string dateBr)
        {
            string[] parts = new string[2];
            parts[0] = dateBr.Split('/')[0];

            switch (dateBr.Split('/')[1])
            {
                case "01":
                {
                    parts[1] = "Janeiro";
                    break;
                }
                case "02":
                {
                    parts[1] = "Fevereiro";
                    break;
                }
                case "03":
                {
                    parts[1] = "Março";
                    break;
                }
                case "04":
                {
                    parts[1] = "Abril";
                    break;
                }
                case "05":
                {
                    parts[1] = "Maio";
                    break;
                }
                case "06":
                {
                    parts[1] = "Junho";
                    break;
                }
                case "07":
                {
                    parts[1] = "Julho";
                    break;
                }
                case "08":
                {
                    parts[1] = "Agosto";
                    break;
                }
                case "09":
                {
                    parts[1] = "Setembro";
                    break;
                }
                case "10":
                {
                    parts[1] = "Outubro";
                    break;
                }
                case "11":
                {
                    parts[1] = "Novembro";
                    break;
                }
                case "12":
                {
                    parts[1] = "Dezembro";
                    break;
                }
            }

            return parts;
        }
    }
}