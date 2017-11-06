namespace OiWeb.WebAPI.Handle
{
    public static class DecimalExtension
    {
        public static string ToCurrencyString(this decimal value)
        {
            string strValue = value.ToString();
            string[] split = strValue.Replace(',', '.').Split('.');

            if (split.Length == 1)
            {
                return split[0] + ",00";
            }
            else
            {
                return split[0] + "," + (split[1].Length == 0 ? "00" : (split[1].Length == 1 ? split[1] + "0" : (split[1].Length == 2 ? split[1] : split[1].Substring(0, 2))));
            }
        }
    }
}