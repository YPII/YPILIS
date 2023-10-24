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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.SlidePrinting
{
	public partial class PrintSlidePage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        string m_ContainerId;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;        

        public PrintSlidePage(string containerId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
            this.m_ContainerId = containerId;
            this.m_AccessionOrder = accessionOrder;
            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(this.m_ContainerId);			            

			InitializeComponent();
			DataContext = this;            
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null ) this.Back(this, new EventArgs());
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {            
            if (this.Next != null) this.Next(this, new EventArgs());            
        }        		  

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperLinkAddSlide_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void HyperLinkPrintSlide_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewAliquotOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.ListViewAliquotOrders.SelectedItem;
                YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode = Business.BarcodeScanning.CytycBarcode.Parse(this.m_AccessionOrder.MasterAccessionNo);
                YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode = new Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.PSLD, aliquotOrder.AliquotOrderId);
                YellowstonePathology.Business.Label.Model.ThinPrepSlideDirectPrintLabel thinPrepSlideDirectPrintLabel = new Business.Label.Model.ThinPrepSlideDirectPrintLabel(this.m_SpecimenOrder.MasterAccessionNo, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName, cytycBarcode, barcode);
                YellowstonePathology.Business.Label.Model.ThermoFisherPAPSlidePrinter thermoFisherPAPSlidePrinter = new Business.Label.Model.ThermoFisherPAPSlidePrinter();
                thermoFisherPAPSlidePrinter.Queue.Add(thinPrepSlideDirectPrintLabel);
                thermoFisherPAPSlidePrinter.Print();
            }
        }       
	}
}
