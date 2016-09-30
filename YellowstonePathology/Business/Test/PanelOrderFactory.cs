using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YellowstonePathology.Business.Test
{
	public class PanelOrderFactory
	{
        public static PanelOrder GetPanelOrder(YellowstonePathology.Business.Panel.Model.Panel panel)
        {            
            Type panelOrderType = Type.GetType(panel.PanelOrderClassName);
            return (PanelOrder)Activator.CreateInstance(panelOrderType);            
        }

        public static PanelOrder GetPanelOrder(string reportNo, string objectId, string panelOrderId, YellowstonePathology.Business.Panel.Model.Panel panel, int orderedById)
		{
            Type panelOrderType = Type.GetType(panel.PanelOrderClassName);
            return (PanelOrder)Activator.CreateInstance(panelOrderType, reportNo, objectId, panelOrderId, panel, orderedById);            
		}
	}
}
