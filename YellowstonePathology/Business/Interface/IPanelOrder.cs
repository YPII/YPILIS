using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Interface
{
    public interface IPanelOrder
    {
        string ReportNo { get; set; }         
        int PanelId { get; set; } 
        string PanelName { get; set; } 
        int PanelOrderBatchId { get; set; }         
        bool Acknowledged { get; set; }
        int AcknowledgedById { get; set; } 
        Nullable<DateTime> AcknowledgedDate { get; set; } 
        Nullable<DateTime> AcknowledgedTime { get; set; }
        bool Accepted { get; set; } 
        int AcceptedById { get; set; }         
        Nullable<DateTime> AcceptedDate { get; set; } 
        Nullable<DateTime> AcceptedTime { get; set; } 
        int OrderedById { get; set; } 
        Nullable<DateTime> OrderDate { get; set; } 
        Nullable<DateTime> OrderTime { get; set; } 
        string Comment { get; set; }

		string PanelOrderId { get; set; }
		int AssignedToId { get; set; }

		//void AcceptResults(Rules.RuleExecutionStatus ruleExecutionStatus, Test.AccessionOrder accessionOrder, Business.User.SystemUser orderingUser);
        //XElement ToXml();
    }
}
