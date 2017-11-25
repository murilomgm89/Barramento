namespace OiWeb.CMS.Models
{
    public class BreadcrumbViewModel
    {
        public BreadcrumbViewModel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="h1">Nome do titulo da página</param>
        /// <param name="icon">nome do icone para esta pagina</param>
        /// <param name="session">Nome da sessão</param>
        public BreadcrumbViewModel(string h1, string icon, string session)
        {
            H1 = h1;
            Icon = icon;
            Session = session;
        }

        
        public string H1 { get; set; }
        public string Icon { get; set; }
        public string Session { get; set; }

        }
}