using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test
{
	[XmlType("PathologistTestOrderItemList")]
	public class PathologistTestOrderItemList : ObservableCollection<PathologistTestOrderListItem>
	{
		public PathologistTestOrderItemList()
		{

		}

		public void Build(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.ClearItems();
            this.AddPanelSets(accessionOrder);
            if (accessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder surgicalPanelSetOrder = accessionOrder.PanelSetOrderCollection.GetSurgical();
                this.AddTestOrders(surgicalPanelSetOrder);
            }            
		}

        private void AddPanelSets(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = accessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);                
                PathologistTestOrderListItem item = new PathologistTestOrderListItem();
                if(orderTarget != null) item.AliquotDescription = orderTarget.GetDescription();
                item.TestName = panelSetOrder.PanelSetName;
                item.TestOrderDate = panelSetOrder.OrderDate.Value.ToShortDateString();
                this.Add(item);
            }
        }

        private void AddTestOrders(PanelSetOrder panelSetOrder)
        {
            foreach (PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
            {
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                {
                    if (testOrder.TestName != "H and E")
                    {						
                        PathologistTestOrderListItem item = new PathologistTestOrderListItem();
                        string description = testOrder.AliquotOrder.Display;
                        if (string.IsNullOrEmpty(description)) description = string.Empty;
                        item.AliquotDescription = description;
                        item.TestName = testOrder.TestName;
                        item.TestOrderDate = panelOrder.OrderDate.Value.ToShortDateString();
                        this.Add(item);                        
                    }
                }
            }	
        }       
	}
}
