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
    
    public partial class ComponentPage
    {
        public int idComponentPage { get; set; }
        public int idPage { get; set; }
        public int idComponentType { get; set; }
    
        public virtual ComponentType ComponentType { get; set; }
        public virtual Page Page { get; set; }
    }
}
