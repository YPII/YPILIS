using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YellowstonePathology.Business.Test
{
	public class PanelSetOrderFactory
	{
		public static PanelSetOrder CreatePanelSetOrder(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
		{
			Type panelSetType = Type.GetType(panelSet.PanelSetOrderClassName);
			PanelSetOrder panelSetOrder = (PanelSetOrder)Activator.CreateInstance(panelSetType);
			return panelSetOrder;
		}       

		public static PanelSetOrder CreatePanelSetOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)			
        {
            Type panelSetType = Type.GetType(panelSet.PanelSetOrderClassName);
            PanelSetOrder panelSetOrder = (PanelSetOrder)Activator.CreateInstance(panelSetType, masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute);
            return panelSetOrder;
        }        

        public static PanelSetOrder CreatePanelSetOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,            
            bool distribute)
        {
            Type panelSetType = Type.GetType(panelSet.PanelSetOrderClassName);
            PanelSetOrder panelSetOrder = (PanelSetOrder)Activator.CreateInstance(panelSetType, masterAccessionNo, reportNo, objectId, panelSet, distribute);
            return panelSetOrder;
        }		
	}
}