using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{	
	public partial class StandingOrderPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        
        private List<YellowstonePathology.Business.Test.AccessionOrder> m_AccessionOrderList;
        private YellowstonePathology.Business.Domain.Physician m_Physician;
        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_StandingOrderCollection;
        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPVStandingOrderCollection;
        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPV1618StandingOrderCollection;
		private YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder m_WomensHealthProfileTestOrder;
		private YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest m_WomensHealthProfileTest;
		private System.Windows.Visibility m_WomensHealthProfileVisibility;

        public StandingOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_AccessionOrder = accessionOrder;            

			this.m_WomensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			this.m_Physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);
            this.m_StandingOrderCollection = this.m_Physician.GetStandingOrderCollection();

            this.m_HPVStandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
            this.m_HPV1618StandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPV1618StandingOrders();

			if (accessionOrder.PanelSetOrderCollection.Exists(this.m_WomensHealthProfileTest.PanelSetId) == true)
            {
				this.m_WomensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_WomensHealthProfileTest.PanelSetId);
				this.m_WomensHealthProfileVisibility = System.Windows.Visibility.Visible;
                this.UpdateWomensHealthProfile();
            }
            else
            {
				this.m_WomensHealthProfileVisibility = System.Windows.Visibility.Collapsed;
            }

            this.m_AccessionOrderList = new List<Business.Test.AccessionOrder>();
            this.m_AccessionOrderList.Add(this.m_AccessionOrder);

			InitializeComponent();

			DataContext = this;
        }

        public YellowstonePathology.Business.Domain.Physician Physician
        {
            get { return this.m_Physician; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public System.Windows.Visibility WomensHealthProfileVisibility
        {
			get { return this.m_WomensHealthProfileVisibility; }
        }

		public YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder WomensHealthProfileTestOrder
        {
			get { return this.m_WomensHealthProfileTestOrder; }
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderCollection StandingOrderCollection
        {
            get { return this.m_StandingOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPVStandingOrderCollection
        {
            get { return this.m_HPVStandingOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPV1618StandingOrderCollection
        {
            get { return this.m_HPV1618StandingOrderCollection; }
        }

        public List<YellowstonePathology.Business.Test.AccessionOrder> AccessionOrderList
        {
            get { return this.m_AccessionOrderList; }
        }						        

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOkToContinue() == true)
            {
                if (this.Next != null)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.Next(this, new EventArgs());
                }
            }
        }

        public bool IsOkToContinue()
        {
            bool result = true;

            YellowstonePathology.Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection();
            auditCollection.Add(new Business.Audit.Model.WHPIsRequiredByStandingOrderAudit(this.m_AccessionOrder));
            auditCollection.Add(new Business.Audit.Model.WHPStandingOrderNotSetAudit(this.m_AccessionOrder));
            auditCollection.Add(new Business.Audit.Model.WHPStandingOrderMismatchAudit(this.m_AccessionOrder));
            auditCollection.Run();

            if (auditCollection.ActionRequired == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditCollection.Message + " Are you sure you want to continue?", "Continue", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    result = false;
                }
            }

            return result;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonAddWHP_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_WomensHealthProfileTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = (YellowstonePathology.Business.Interface.IOrderTarget)this.m_AccessionOrder.SpecimenOrderCollection.GetThinPrep();
				string reportNo = YellowstonePathology.Business.OrderIdParser.GetNextReportNo(this.m_AccessionOrder.PanelSetOrderCollection, this.m_WomensHealthProfileTest, this.m_AccessionOrder.MasterAccessionNo);
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				string masterAccessionNo = this.m_AccessionOrder.MasterAccessionNo;
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)YellowstonePathology.Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(masterAccessionNo, reportNo, objectId, this.m_WomensHealthProfileTest, orderTarget, false);
				womensHealthProfileTestOrder.AssignedToId = 5051;
				this.m_AccessionOrder.PanelSetOrderCollection.Add(womensHealthProfileTestOrder);
				this.m_WomensHealthProfileTestOrder = womensHealthProfileTestOrder;
				this.m_WomensHealthProfileVisibility = System.Windows.Visibility.Visible;
                this.UpdateWomensHealthProfile();
                //this.Save(false);
                this.NotifyPropertyChanged(string.Empty);
            }
            else
            {
				MessageBox.Show("A " + this.m_WomensHealthProfileTest.PanelSetName + " already exists.");
            }
        }

        private void ButtonUpdateWHP_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_WomensHealthProfileTest.PanelSetId) == true)
            {
                this.UpdateWomensHealthProfile();
            }
        }

		private void UpdateWomensHealthProfile()
        {
			this.m_WomensHealthProfileTestOrder.HPVStandingOrderCode = this.m_Physician.HPVStandingOrderCode;
			this.m_WomensHealthProfileTestOrder.HPV1618StandingOrderCode = this.m_Physician.HPV1618StandingOrderCode;
        }
	}
}
