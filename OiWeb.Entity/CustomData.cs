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
    
    public partial class CustomData
    {
        public int idData { get; set; }
        public int idVersion { get; set; }
        public int idComponentType { get; set; }
        public int idGroup { get; set; }
        public string value { get; set; }
    
        public virtual ComponentType ComponentType { get; set; }
        public virtual GroupCustomData GroupCustomData { get; set; }
        public virtual VersionAPI VersionAPI { get; set; }
    }
}