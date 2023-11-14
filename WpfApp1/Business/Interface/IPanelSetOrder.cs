using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Interface
{
    public interface IPanelSetOrder
    {        
        string MasterAccessionNo { get; set; }        
        string ReportNo { get; set; }        
        int PanelSetId { get; set; }        
        Nullable<DateTime> FinalDate { get; set; }        
        Nullable<DateTime> FinalTime { get; set; }        
        bool Final { get; set; }        
        Nullable<DateTime> OrderDate { get; set; }        
        Nullable<DateTime> OrderTime { get; set; }        
        int TemplateId { get; set; }                
        string PanelSetName { get; set; }        
        int FinaledById { get; set; }        
        int OrderedById { get; set; }                
        string Signature { get; set; }        
        int AssignedToId { get; set; }		
		bool Audited { get; set; }		
    }
}
