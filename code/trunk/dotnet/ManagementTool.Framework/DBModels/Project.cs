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
    
    public partial class Project
    {
        public int ProjectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ProjectManager { get; set; }
        public System.DateTime DueDate { get; set; }
        public int CompanyId { get; set; }
        public string TeamMembers { get; set; }
    }
}
