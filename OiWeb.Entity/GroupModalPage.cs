//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OiWeb.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupModalPage
    {
        public int idGroupModalPage { get; set; }
        public int idPage { get; set; }
        public int idGroupModal { get; set; }
        public int idModal { get; set; }
    
        public virtual GroupModal GroupModal { get; set; }
        public virtual Page Page { get; set; }
        public virtual Modal Modal { get; set; }
    }
}
