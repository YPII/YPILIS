﻿using System;
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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for PrintCytologyLabelsPage.xaml
	/// </summary>
	public partial class PrintCytologyLabelsPage : UserControl
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void FinishEventHandler(object sender, EventArgs e);
		public event FinishEventHandler Finish;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		public PrintCytologyLabelsPage(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PageHeaderText = "Print labels for " + this.m_AccessionOrder.PFirstName + " " + this.m_AccessionOrder.PLastName + ": " + reportNo;

			InitializeComponent();
			DataContext = this;            
		}        

        public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}			

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.Back(this, new EventArgs());
		}

		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{
			if(this.Finish != null) this.Finish(this, new EventArgs());
		}

		private void ButtonPrintCytologyLabels_Click(object sender, RoutedEventArgs e)
		{              
            if (this.m_AccessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
            {
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetThinPrep();
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = null;
                if (specimenOrder.AliquotOrderCollection.HasThinPrepSlide()== false)
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.AddThinPrepSlide(specimenOrder, this.m_AccessionOrder.AccessionDate.Value);                    
                }
                else
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.GetThinPrepSlide();
                }                
                
                YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.PSLD, aliquotOrder.AliquotOrderId);
                YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode = Business.BarcodeScanning.CytycBarcode.Parse(this.m_AccessionOrder.MasterAccessionNo);
                YellowstonePathology.Business.Label.Model.PAPSlideLabel papSlideLabel = new Business.Label.Model.PAPSlideLabel(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, barcode, cytycBarcode);
                YellowstonePathology.Business.Label.Model.PAPSlideLabelPrinter papSlideLabelPrinter = new Business.Label.Model.PAPSlideLabelPrinter();
                for (int i = 0; i < 2; i++)
                {
                    papSlideLabelPrinter.Queue.Enqueue(papSlideLabel);
                }
                papSlideLabelPrinter.Print();
            }
            else
            {
                MessageBox.Show("Unable to find a Thin Prep Specimen");
            }            
		}		

        private void ButtonPrintCytologyDummyLabel_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPAP();
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);
            string normalMA = orderIdParser.MasterAccessionNo;            
            string dummyMA = normalMA.Replace("23-", "73-");

            if (this.m_AccessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetThinPrep();
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetThinPrepSlide();

                YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.PSLD, aliquotOrder.AliquotOrderId);
                YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode = Business.BarcodeScanning.CytycBarcode.Parse(dummyMA);
                Business.Label.Model.HologicSlideLabel hologicSlideLabel = new Business.Label.Model.HologicSlideLabel(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, barcode, cytycBarcode);
                Business.Label.Model.HologicSlideLabelPrinter.Print(hologicSlideLabel, Business.User.UserPreferenceInstance.Instance.UserPreference.CytologySlideLabelPrinter, 2);
            }
            else
            {
                MessageBox.Show("Unable to find a Thin Prep Specimen");
            }
        }

        private void ButtonPrintMolecularLabels_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelFormat) == false)
            {
                YellowstonePathology.Business.Label.Model.LabelFormatEnum labelFormat = (YellowstonePathology.Business.Label.Model.LabelFormatEnum)Enum.Parse(typeof(YellowstonePathology.Business.Label.Model.LabelFormatEnum), YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelFormat);            
                YellowstonePathology.Business.Specimen.Model.Specimen thinPrepFluid = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNTHNPRPFLD"); // Definition.ThinPrepFluid();
                YellowstonePathology.Business.Label.Model.MolecularLabelPrinter molecularLabelPrinter = new Business.Label.Model.MolecularLabelPrinter();
                YellowstonePathology.Business.Label.Model.Label label = Business.Label.Model.LabelFactory.GetMolecularLabel(labelFormat, this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, thinPrepFluid.Description, null, false);
                molecularLabelPrinter.Queue.Enqueue(label);
                molecularLabelPrinter.Print();
            }
            else
            {
                MessageBox.Show("The label format must first be selected in User Preferences.");
            }
        }

        private void ButtonPrintCytologyNewLabel_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_AccessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetThinPrep();
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = null;
                if (specimenOrder.AliquotOrderCollection.HasThinPrepSlide() == false)
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.AddThinPrepSlide(specimenOrder, this.m_AccessionOrder.AccessionDate.Value);
                }
                else
                {
                    aliquotOrder = specimenOrder.AliquotOrderCollection.GetThinPrepSlide();
                }

                YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.PSLD, aliquotOrder.AliquotOrderId);
                YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode = Business.BarcodeScanning.CytycBarcode.Parse(this.m_AccessionOrder.MasterAccessionNo);
                Business.Label.Model.HologicSlideLabel hologicSlideLabel = new Business.Label.Model.HologicSlideLabel(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, barcode, cytycBarcode);
                Business.Label.Model.HologicSlideLabelPrinter.Print(hologicSlideLabel, Business.User.UserPreferenceInstance.Instance.UserPreference.CytologySlideLabelPrinter, 2);
            }
            else
            {
                MessageBox.Show("Unable to find a Thin Prep Specimen");
            }
        }
    }
}
