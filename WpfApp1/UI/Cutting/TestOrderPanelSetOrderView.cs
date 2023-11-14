using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Cutting
{
    public class TestOrderPanelSetOrderView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Business.Test.Model.TestOrder m_TestOrder;
        private Business.Test.PanelSetOrder m_PanelSetOrder;

        private string m_ReportNo;
        private string m_PanelSetName;
        private string m_TestAbbreviation;
        private string m_TestOrderId;
        private bool m_OrderedAsDual;
        private bool m_CuttingIsComplete;        

        public TestOrderPanelSetOrderView(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_TestOrder = testOrder;

            this.m_ReportNo = panelSetOrder.ReportNo;
            this.m_PanelSetName = panelSetOrder.PanelSetName;
            this.m_TestAbbreviation = testOrder.TestAbbreviation;
            this.m_TestOrderId = testOrder.TestOrderId;
            this.m_OrderedAsDual = testOrder.OrderedAsDual;

            this.Update();
        }      

        public void Update()
        {
            if (this.m_TestOrder.SlideOrderCollection.HasPrintedSlide() == true && this.m_TestOrder.SlideOrderCollection.HasUnprintedSlide() == false)
            {
                this.m_CuttingIsComplete = true;
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if(this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }                
            }
        }

        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if(this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
                }                
            }
        }

        public string TestAbbreviation
        {
            get { return this.m_TestAbbreviation; }
            set
            {
                if(this.m_TestAbbreviation != value)
                {
                    this.m_TestAbbreviation = value;
                    this.NotifyPropertyChanged("TestAbbreviation");
                }                
            }
        }

        public string TestOrderId
        {
            get { return this.m_TestOrderId; }
            set
            {
                if(this.m_TestOrderId != value)
                {
                    this.m_TestOrderId = value;
                    this.NotifyPropertyChanged("TestOrderId");
                }                
            }
        }

        public bool OrderedAsDual
        {
            get { return this.m_OrderedAsDual; }
            set
            {
                if(this.m_OrderedAsDual == true)
                {
                    this.m_OrderedAsDual = value;
                    this.NotifyPropertyChanged("OrderedAsDual");
                }                
            }
        }

        public bool CuttingIsComplete
        {
            get { return this.m_CuttingIsComplete; }
            set
            {
                if(this.m_CuttingIsComplete != value)
                {
                    this.m_CuttingIsComplete = value;
                    this.NotifyPropertyChanged("CuttingIsComplete");
                }                
            }
        }        

        public string DisplayString
        {
            get { return this.m_ReportNo + ": " + this.m_TestAbbreviation; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
