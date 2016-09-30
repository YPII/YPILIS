using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class SpecimenMappingPage : UserControl
	{
		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Specimen Mapping Page";
		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private ObservableCollection<string> m_TimeToFixationTypeCollection;

        public SpecimenMappingPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;

            this.m_TimeToFixationTypeCollection = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.GetTimeToFixationTypeCollection();

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);			
			InitializeComponent();

			DataContext = this;           
		}        

        public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        public ObservableCollection<string> TimeToFixationTypeCollection
        {
            get { return this.m_TimeToFixationTypeCollection; }
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            this.HandleClientAccessionedStainResult();
            this.HandleClientAccessionedTestOrders();

            if (this.IsOkToGoNext() == true)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                this.Next(this, new EventArgs());
            }
		}

        private void HandleClientAccessionedTestOrders()
        {
            foreach(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                foreach(YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    foreach(YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        foreach(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in testOrder.SlideOrderCollection)
                        {
                            if (slideOrder.ClientAccessioned == true)
                            {
                                testOrder.NoCharge = true;
                            }
                        }
                    }
                }
            }
        }

        private void HandleClientAccessionedStainResult()
        {
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
                    {
                        if (this.m_AccessionOrder.SpecimenOrderCollection.SlideOrderExists(stainResult.TestOrderId) == true)
                        {
                            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSlideOrderByTestOrderId(stainResult.TestOrderId);
                            if (slideOrder.ClientAccessioned == true)
                            {
                                stainResult.ClientAccessioned = true;
                                stainResult.Billable = false;
                                stainResult.NoCharge = true;
                            }
                            else
                            {
                                stainResult.ClientAccessioned = false;
                                stainResult.Billable = true;
                                stainResult.NoCharge = false;
                            }
                        }
                    }
                }
            }
        }

        private bool IsOkToGoNext()
        {
            bool result = true;
            if (this.m_AccessionOrder.ClientAccessioned == true)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.ClientAccessionNo) == true)
                {
                    MessageBox.Show("The Client Accession No cannot be blank.");
                    result = false;
                }
                else
                {
                    foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                    {
                        if (specimenOrder.ClientAccessioned == true)
                        {
                            if (specimenOrder.ClientSpecimenNumber == 0)
                            {
                                MessageBox.Show("The Client Specimen Number cannot be zero.");
                                result = false;
                                break;
                            }
                            else
                            {
                                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                                {
                                    if (aliquotOrder.ClientAccessioned == true)
                                    {
                                        if (string.IsNullOrEmpty(aliquotOrder.ClientLabel) == true)
                                        {
                                            MessageBox.Show("The Client Label for: " + aliquotOrder.Label + " cannot be blank.");
                                            result = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }		

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            this.PrintClientAccessionedLabels();
        }

        private void PrintClientAccessionedLabels()
        {
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (specimenOrder.ClientAccessioned == true)
                {
                    foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                    {                        
                        if (aliquotOrder.ClientAccessioned == true)
                        {
                            YellowstonePathology.Business.Label.Model.BlockLabelPrinter blockLabelPrinter = new Business.Label.Model.BlockLabelPrinter(aliquotOrder.AliquotOrderId, aliquotOrder.Label, this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName);
                            blockLabelPrinter.Print();

                            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                            {
                                if (slideOrder.ClientAccessioned == true)
                                {
                                    YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel paperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId,
                                        this.m_AccessionOrder.MasterAccessionNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.Location);

                                    YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter printer = new Business.Label.Model.HistologySlidePaperLabelPrinter();
                                    printer.Queue.Enqueue(paperLabel);
                                    printer.Print();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CheckBoxSlideClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)checkBox.Tag;
            slideOrder.Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.ClientAccessioned.ToString();
        }

        private void CheckBoxSlideClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)checkBox.Tag;
            slideOrder.Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Created.ToString();
        }

        private void ButtonUpdateTrackingLog_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection materialTrackingLogCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingLogCollectionByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);            
            materialTrackingLogCollection.UpdateClientAccessioned(this.m_AccessionOrder.SpecimenOrderCollection);
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(materialTrackingLogCollection, false);            
        }
	}
}
