//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManagementTool.Framework.DBModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Requirement
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int ProjectId { get; set; }
        public int RequirementId { get; set; }
        public string Category { get; set; }
    }
}
