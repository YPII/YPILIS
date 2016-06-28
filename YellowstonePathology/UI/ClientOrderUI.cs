using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    public class ClientOrderUI : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        private DateTime m_ClientOrderDate;

        private YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection m_OrderBrowserListItemCollection;
        private string m_SelectedItemCount;
        private object m_Writer;

        public ClientOrderUI(object writer)
        {
            this.m_Writer = writer;
            this.m_ClientOrderDate = DateTime.Today;

            this.GetClientOrderList();
        }

        public string SelectedItemCount
        {
            get { return this.m_SelectedItemCount; }
            set
            {
                if (this.m_SelectedItemCount != value)
                {
                    this.m_SelectedItemCount = value;
                    this.NotifyPropertyChanged("SelectedItemCount");
                }
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.OrderBrowserListItemCollection OrderBrowserListItemCollection
        {
            get { return this.m_OrderBrowserListItemCollection; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set
            {
                this.m_AccessionOrder = value;
                this.NotifyPropertyChanged("AccessionOrder");
            }
        }

        public DateTime ClientOrderDate
        {
            get { return this.m_ClientOrderDate; }
            set
            {
                this.m_ClientOrderDate = value;
                NotifyPropertyChanged("ClientOrderDate");
            }
        }

        public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return YellowstonePathology.Business.User.SystemIdentity.Instance; }
        }

        public void GetClientOrderList()
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByOrderDate(this.m_ClientOrderDate);
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
        }

        public void GetClientOrderListByMasterAccessionNo(string masterAccessionNo)
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByMasterAccessionNo(masterAccessionNo);
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
        }

        public void GetClientOrderListByPatientName(YellowstonePathology.Business.PatientName patientName)
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByPatientName(patientName.LastName, patientName.FirstName);
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
        }

        public void GetHoldList()
        {
            this.m_OrderBrowserListItemCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetOrderBrowserListItemsByHoldStatus();
            this.NotifyPropertyChanged("OrderBrowserListItemCollection");
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
