using System.Web;

namespace OiWeb.CMS.Models
{
    public class GroupCustomDataPageViewModel
    {
        public HttpPostedFileBase file { get; set; }
        /// <summary>
        /// This property just works to contain data during the POST from the FORM
        /// </summary>
        public bool isByCity { get; set; }

        public int idPage { get; set; }
        public int idGroup { get; set; }
    }
}