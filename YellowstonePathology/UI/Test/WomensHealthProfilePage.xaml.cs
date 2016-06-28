using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{    
    public partial class WomensHealthProfilePage : ResultControl, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;        

        public delegate void FinishedEventHandler(object sender, EventArgs e);
        public event FinishedEventHandler Finished;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Domain.Physician m_Physician;
		private DateTime? m_DateOfLastHPV;

		private YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;
		private YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder m_WomensHealthProfileTestOrder;                       

        private YellowstonePathology.Business.Client.Model.ReflexOrderCollection m_HPV1618ReflexOrderCollection;
        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPV1618StandingOrderCollection;
        private YellowstonePathology.Business.Client.Model.ReflexOrderCollection m_HPVReflexOrderCollection;
        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPVStandingOrderCollection;        
        private YellowstonePathology.Business.Audit.Model.IsWHPAllDoneAuditCollection m_AuditCollection;

        private string m_HPVStandingOrderDescription;
        private System.Windows.Visibility m_BackButtonVisibility;
        private Window m_ParentWindow;

        public WomensHealthProfilePage(YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, 
            System.Windows.Visibility backButtonVisibility): base(womensHealthProfileTestOrder, accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            
            this.m_HPVReflexOrderCollection = YellowstonePathology.Business.Client.Model.ReflexOrderCollection.GetHPVRequisitionReflexOrders();
            this.m_HPVStandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
            this.m_HPV1618ReflexOrderCollection = YellowstonePathology.Business.Client.Model.ReflexOrderCollection.GetHPV1618ReflexOrders();
            this.m_HPV1618StandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPV1618StandingOrders();
            
            this.m_ClientOrder = clientOrder;
			//this.m_WomensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
            this.m_WomensHealthProfileTestOrder = womensHealthProfileTestOrder;
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
            this.m_BackButtonVisibility = backButtonVisibility;

			this.m_Physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);

            this.m_AuditCollection = new Business.Audit.Model.IsWHPAllDoneAuditCollection(this.m_AccessionOrder); 
            this.m_AuditCollection.Run();

            if (string.IsNullOrEmpty(accessionOrder.PatientId) == false)
            {
				YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(accessionOrder.PatientId);
                this.m_DateOfLastHPV = patientHistory.GetDateOfPreviousHpv(this.m_AccessionOrder.AccessionDate.Value);
            }

            YellowstonePathology.Business.Client.Model.StandingOrder standingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.m_WomensHealthProfileTestOrder.HPVStandingOrderCode);
            this.m_HPVStandingOrderDescription = standingOrder.ToString();

            InitializeComponent();

            this.DataContext = this;

            this.m_ParentWindow = Window.GetWindow(this);

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonClose);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalize);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockNext);
        }

        public string HPVStandingOrderDescription
        {
            get { return this.m_HPVStandingOrderDescription; }
            set { this.m_HPVStandingOrderDescription = value; }
        }

        public YellowstonePathology.Business.Audit.Model.AuditCollection AuditCollection
        {
            get { return this.m_AuditCollection; }
        }

        public YellowstonePathology.Business.Client.Model.ReflexOrderCollection HPVReflexOrderCollection
        {
            get { return this.m_HPVReflexOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPVStandingOrderCollection
        {
            get { return this.m_HPVStandingOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.ReflexOrderCollection HPV1618ReflexOrderCollection
        {
            get { return this.m_HPV1618ReflexOrderCollection; }
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPV1618StandingOrderCollection
        {
            get { return this.m_HPV1618StandingOrderCollection; }
        }

		public YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder WomensHealthProfileTestOrder
        {
            get { return this.m_WomensHealthProfileTestOrder; }
        }

		public YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology PanelSetOrderCytology
        {
            get { return this.m_PanelSetOrderCytology; }
        }

        public YellowstonePathology.Business.Domain.Physician Physician
        {
            get { return this.m_Physician; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public string PageHeaderText
        {
			get { return "Womens Health Profile For: " + this.m_AccessionOrder.PatientDisplayName; }
        }

        public string SpecialInstructions
        {
            get
            {
                string result = string.Empty;
                if (this.m_ClientOrder != null)
                {
                    result = this.m_ClientOrder.SpecialInstructions;
                }
                return result;
            }
        }

		public DateTime? DateOfLastHPV
		{
			get { return this.m_DateOfLastHPV; }
		}

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        private void HyperLinkOrderHPV_Click(object sender, RoutedEventArgs e)
        {
            int hpvPanelSetId = 14;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasPanelSetBeenOrdered(hpvPanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_WomensHealthProfileTestOrder.OrderedOnId);
				YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new Business.Test.HPV.HPVTest();
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(panelSetHPV, orderTarget, true);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);
                this.m_AuditCollection.Run();

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                this.NotifyPropertyChanged(string.Empty);
            }
            else
            {
                MessageBox.Show("An HPV has already been ordered.");
            }
        }

        private void HyperLinkOrderNGCT_Click(object sender, RoutedEventArgs e)
        {
            int ngctPanelSetId = 3;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasPanelSetBeenOrdered(ngctPanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_WomensHealthProfileTestOrder.OrderedOnId);
                YellowstonePathology.Business.Test.NGCT.NGCTTest ngctTest = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(ngctTest, orderTarget, true);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                this.m_AuditCollection.Run();
                this.NotifyPropertyChanged(string.Empty);
            }
            else
            {
                MessageBox.Show("NG/CT has already been ordered.");
            }
        }

        private void HyperLinkOrderTrich_Click(object sender, RoutedEventArgs e)
        {
            int trhichomonasPanelSetId = 61;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasPanelSetBeenOrdered(trhichomonasPanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_WomensHealthProfileTestOrder.OrderedOnId);
                YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest trichomonasTest = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(trichomonasTest, orderTarget, true);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                this.m_AuditCollection.Run();
                this.NotifyPropertyChanged(string.Empty);
            }
            else
            {
                MessageBox.Show("Trhichomonas Vaginalis has already been ordered.");
            }
        }

        private void HyperLinkOrderHPV1618_Click(object sender, RoutedEventArgs e)
        {
            int HPV1618PanelSetId = 62;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasPanelSetBeenOrdered(HPV1618PanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_WomensHealthProfileTestOrder.OrderedOnId);
                YellowstonePathology.Business.Test.HPV1618.HPV1618Test hpv1618Test = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(hpv1618Test, orderTarget, true);                                

                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                this.m_AuditCollection.Run();
                this.NotifyPropertyChanged(string.Empty);
            }
            else
            {
                MessageBox.Show("HPV 16/18 has already been ordered.");
            }
        }

        private void HyperLinkOrderThinPrepPap_Click(object sender, RoutedEventArgs e)
        {
            int ThinPrepPapPanelSetId = 15;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasPanelSetBeenOrdered(ThinPrepPapPanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_WomensHealthProfileTestOrder.OrderedOnId);
                YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(thinPrepPapTest, orderTarget, true);                
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                this.m_AuditCollection.Run();
                this.NotifyPropertyChanged(string.Empty);                
            }
            else
            {
                MessageBox.Show("Thin Prep Pap has already been ordered.");
            }
        }

        private void HyperLinkDeleteSelectedOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxPanelSetOrders.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrder pso = (YellowstonePathology.Business.Test.PanelSetOrder)this.ListBoxPanelSetOrders.SelectedItem;
                if (pso.PanelSetId != 15)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this report.", "Delete Report", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (pso.Final == false)
                        {
                            if (pso.PanelSetId == 14)
                            {
                                this.m_AccessionOrder.PanelSetOrderCollection.Remove(pso);
                                this.m_AuditCollection.Run();
                                this.NotifyPropertyChanged(string.Empty);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cannot delete this order because its final");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The PAP cannot be deleted from this window.");
                }
            }
        }

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWordDocument report = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWordDocument(this.m_AccessionOrder, this.m_WomensHealthProfileTestOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_WomensHealthProfileTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.GoNext();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void GoNext()
        {
            this.m_AuditCollection.Run();
            if (this.m_AuditCollection.ActionRequired == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(this.m_AuditCollection.Message + " Are you sure you want to continue.", "Continue?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (this.Finished != null) this.Finished(this, new EventArgs());
                }
            }
            else
            {
                if (this.Finished != null) this.Finished(this, new EventArgs());
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
            multiTestDistributionHandler.Set();

            YellowstonePathology.Business.Audit.Model.CanWomensHealthProfileBeFinaledAudit canWomensHealthProfileBeFinaledAudit = new Business.Audit.Model.CanWomensHealthProfileBeFinaledAudit(this.m_AccessionOrder);
            canWomensHealthProfileBeFinaledAudit.Run();

            if (canWomensHealthProfileBeFinaledAudit.ActionRequired == true)
            {
                MessageBox.Show(canWomensHealthProfileBeFinaledAudit.Message.ToString());
            }
            else
            {
                this.m_WomensHealthProfileTestOrder.Finish(this.m_AccessionOrder);
                this.m_AuditCollection.Run();
                this.NotifyPropertyChanged("");
            }                        
        }        

        private void HyperLinkUnfinalize_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_WomensHealthProfileTestOrder.TestOrderReportDistributionCollection.HasDistributedItems() == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("This case has been distributed.  Are you sure you want to unfinal it?", "Case has distributed.", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_WomensHealthProfileTestOrder.Unfinalize();
                    this.m_AuditCollection.Run();
                    this.NotifyPropertyChanged("");
                }
            }
            else
            {
                this.m_WomensHealthProfileTestOrder.Unfinalize();
                this.m_AuditCollection.Run();
                this.NotifyPropertyChanged("");
            }
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_WomensHealthProfileTestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_WomensHealthProfileTestOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_WomensHealthProfileTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_WomensHealthProfileTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private void ComboBoxHPV1618ReflexOrderCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_AuditCollection.Run();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ComboBoxHPVReflexOrderCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_AuditCollection.Run();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void HyperLinkNext_Click(object sender, RoutedEventArgs e)
        {
            this.GoNext();
        }        
    }
}
