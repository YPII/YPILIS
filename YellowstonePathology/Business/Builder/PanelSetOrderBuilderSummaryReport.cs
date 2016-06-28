using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Builder
{
    public class PanelSetOrderBuilderSummaryReport : PanelSetOrderBuilder
    {
		public override void Build(Test.PanelSetOrder panelSetOrder, XElement panelSetOrderElement)
        {
            List<XElement> reportOrderSummaryDetailElements = (from item in panelSetOrderElement.Elements("ReportOrderSummaryDetailCollection")
                                             select item).ToList<XElement>();

            /*YellowstonePathology.Business.Test.SummaryReport.ReportOrderSummary reportOrderSummary = (YellowstonePathology.Business.Test.SummaryReport.ReportOrderSummary)panelSetOrder;
            foreach (XElement reportOrderSummaryDetailElement in reportOrderSummaryDetailElements.Elements("ReportOrderSummaryDetail"))
            {

                int panelSetId = Convert.ToInt32(reportOrderSummaryDetailElement.Element("PanelSetId").Value);
                YellowstonePathology.Business.Test.SummaryReport.ReportOrderSummaryDetail reportOrderSummaryDetail = null;
                switch (panelSetId)
                {
                    case 1:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailJAK2();
                        break;
                    case 13:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailSurgical();              
                        break;
                    case 18:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailBRAF();
                        break;
                    case 20:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailLeukemiaLymphoma();
                        break;
                    case 30:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailKRASBRAFReflex();
                        break;
                    case 46:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailHER2DISH();
                        break;
                    case 102:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetailLynchSyndromeIHC();
                        break;
                    default:
                        reportOrderSummaryDetail = new Test.SummaryReport.ReportOrderSummaryDetail();                
                        break;
                }
                
                YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new Persistence.XmlPropertyWriter(reportOrderSummaryDetailElement, reportOrderSummaryDetail);
                xmlPropertyWriter.Write();
                
                reportOrderSummary.ReportOrderSummaryDetailCollection.Add(reportOrderSummaryDetail);
            }*/
        }
    }
}
