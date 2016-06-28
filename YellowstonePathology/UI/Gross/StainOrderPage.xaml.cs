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

namespace YellowstonePathology.UI.Gross
{
    public partial class StainOrderPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event BackEventHandler Back;       

        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.Model.TestOrderCollection m_TestOrderCollection;

		public StainOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, 
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{            
            this.m_AccessionOrder = accessionOrder;
            this.m_AliquotOrder = aliquotOrder;
            this.m_SpecimenOrder = specimenOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            this.m_TestOrderCollection = this.m_PanelSetOrder.GetTestOrderCollection(this.m_AliquotOrder.AliquotOrderId);

			InitializeComponent();
			DataContext = this;
		}

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

        public YellowstonePathology.Business.Test.Model.TestOrderCollection TestOrderCollection
        {
            get { return this.m_TestOrderCollection; }
        }		        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {            
            if(this.Back != null) this.Back(this, new UI.CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder));
        }

        private void ButtonOrderHPylori_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.HelicobacterPylori helicobacterPylori = new YellowstonePathology.Business.Test.Model.HelicobacterPylori();
            YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, helicobacterPylori, helicobacterPylori.OrderComment, null, false, this.m_AliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
            this.m_AccessionOrder.TakeATrip(orderTestVisitor);

            this.m_TestOrderCollection = this.m_PanelSetOrder.GetTestOrderCollection(this.m_AliquotOrder.AliquotOrderId);
            this.NotifyPropertyChanged("TestOrderCollection");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonOrderPancytokeratin_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.Pancytokeratin test = new YellowstonePathology.Business.Test.Model.Pancytokeratin();
			YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, null, false, this.m_AliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
            this.m_AccessionOrder.TakeATrip(orderTestVisitor);

            this.m_TestOrderCollection = this.m_PanelSetOrder.GetTestOrderCollection(this.m_AliquotOrder.AliquotOrderId);
            this.NotifyPropertyChanged("TestOrderCollection");
        }

        private void ButtonOrderHMB45_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.HMB45 test = new YellowstonePathology.Business.Test.Model.HMB45();
			YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, null, false, this.m_AliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
            this.m_AccessionOrder.TakeATrip(orderTestVisitor);

            this.m_TestOrderCollection = this.m_PanelSetOrder.GetTestOrderCollection(this.m_AliquotOrder.AliquotOrderId);
            this.NotifyPropertyChanged("TestOrderCollection");
        }

        private void ButtonOrderMelanA_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.MelanA test = new YellowstonePathology.Business.Test.Model.MelanA();
			YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, null, false, this.m_AliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
            this.m_AccessionOrder.TakeATrip(orderTestVisitor);

            this.m_TestOrderCollection = this.m_PanelSetOrder.GetTestOrderCollection(this.m_AliquotOrder.AliquotOrderId);
            this.NotifyPropertyChanged("TestOrderCollection");
        }

        private void ButtonOrderMITF_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.MITF test = new YellowstonePathology.Business.Test.Model.MITF();
			YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, null, false, this.m_AliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
            this.m_AccessionOrder.TakeATrip(orderTestVisitor);

            this.m_TestOrderCollection = this.m_PanelSetOrder.GetTestOrderCollection(this.m_AliquotOrder.AliquotOrderId);
            this.NotifyPropertyChanged("TestOrderCollection");
        }
	}
}
