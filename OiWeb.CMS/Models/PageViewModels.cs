using System;

namespace OiWeb.CMS.Models
{
    public class PageViewModel
    {
        public int idPage { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isCommon { get; set; }
        public bool isActive { get; set; }
        public DateTime dtCreate { get; set; }
    }
}