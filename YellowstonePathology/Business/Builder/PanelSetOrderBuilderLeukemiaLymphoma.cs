using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Builder
{
	public class PanelSetOrderBuilderLeukemiaLymphoma : PanelSetOrderBuilder
	{
		public override void Build(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, System.Xml.Linq.XElement panelSetOrderElement)
		{

            Test.LLP.PanelSetOrderLeukemiaLymphoma llpPanelSetOrder = (Test.LLP.PanelSetOrderLeukemiaLymphoma)panelSetOrder;

            List<XElement> markerElements = (from item in panelSetOrderElement.Elements("FlowMarkerCollection")
											 select item).ToList<XElement>();

            llpPanelSetOrder.FlowMarkerCollection.RemoveDeleted(markerElements.Elements("FlowMarker"));
            
            Flow.FlowMarkerItem flowMarker = null;
            foreach (XElement markerElement in markerElements.Elements("FlowMarker"))
			{
                string flowMarkerId = markerElement.Element("FlowMarkerId").Value;

                if (llpPanelSetOrder.FlowMarkerCollection.Exists(flowMarkerId) == true)
                {
                    flowMarker = llpPanelSetOrder.FlowMarkerCollection.Get(flowMarkerId);
                }
                else
                {
                    flowMarker = new Flow.FlowMarkerItem();
                    llpPanelSetOrder.FlowMarkerCollection.Add(flowMarker);
                }

                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(markerElement, flowMarker);
				xmlPropertyWriter.Write();				
			}
		}
	}
}
