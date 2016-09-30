using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Cutting
{
    public class TestOrderPanelSetOrderView
    {
        private string m_ReportNo;
        private string m_PanelSetName;
        private string m_TestAbbreviation;
        private string m_TestOrderId;
        private bool m_OrderedAsDual;
        private bool m_CuttingIsComplete;

        public TestOrderPanelSetOrderView(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {            
            this.m_ReportNo = panelSetOrder.ReportNo;
            this.m_PanelSetName = panelSetOrder.PanelSetName;
            this.m_TestAbbreviation = testOrder.TestAbbreviation;
            this.m_TestOrderId = testOrder.TestOrderId;
            this.m_OrderedAsDual = testOrder.OrderedAsDual;            

            if(testOrder.SlideOrderCollection.HasPrintedSlide() == true && testOrder.SlideOrderCollection.HasUnprintedSlide() == false)
            {
                this.m_CuttingIsComplete = true;   
            }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
        }

        public string TestAbbreviation
        {
            get { return this.m_TestAbbreviation; }
            set { this.m_TestAbbreviation = value; }
        }

        public string TestOrderId
        {
            get { return this.m_TestOrderId; }
            set { this.m_TestOrderId = value; }
        }

        public bool OrderedAsDual
        {
            get { return this.m_OrderedAsDual; }
            set { this.m_OrderedAsDual = value; }
        }

        public bool CuttingIsComplete
        {
            get { return this.m_CuttingIsComplete; }
            set { this.m_CuttingIsComplete = value; }
        }

        public string DisplayString
        {
            get { return this.m_ReportNo + ": " + this.m_TestAbbreviation; }
        }
    }
}
