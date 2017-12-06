using System.Web;

namespace OiWeb.Entity
{
    public partial class GroupCustomDataPage
    {
        public HttpPostedFileBase file { get; set; }
        /// <summary>
        /// This property just works to contain data during the POST from the FORM
        /// </summary>
        public bool isByCity { get; set; }
    }
}