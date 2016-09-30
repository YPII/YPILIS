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
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{	
	public partial class SpecimenOrderDetailsPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private string m_PageHeaderText = "Specimen Details Page";
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private ObservableCollection<string> m_FixationTypeCollection;
        private YellowstonePathology.UI.Login.FinalizeAccession.SpecimenAdequacyTypes m_SpecimenAdequacyTypes;        
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;

        private YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;

        public SpecimenOrderDetailsPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_SpecimenOrder = specimenOrder;

            this.m_ProcessorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll(true);
            this.m_SpecimenCollection = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.GetAll();

            this.m_FixationTypeCollection = YellowstonePathology.Business.Specimen.Model.FixationType.GetFixationTypeCollection();
            this.m_SpecimenAdequacyTypes = new FinalizeAccession.SpecimenAdequacyTypes();

			InitializeComponent();
			DataContext = this;
            this.Loaded += new RoutedEventHandler(SpecimenOrderDetailsPage_Loaded);
            Unloaded += SpecimenOrderDetailsPage_Unloaded;
		}

        private void SpecimenOrderDetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
             
            this.TextBoxDescription.Focus();
            if(string.IsNullOrEmpty(this.m_SpecimenOrder.SpecimenId) == true)
            {
                this.ComboBoxSpecimenId.Focus();
            }

            if (string.IsNullOrEmpty(this.m_SpecimenOrder.Description) == false)
            {
                if (this.m_SpecimenOrder.Description.ToUpper().Contains("THIN PREP") == true)
                {
                    this.TextBoxSpecimenSource.Focus();
                }
            }

            this.ComboBoxSpecimenId.SelectionChanged += ComboBoxSpecimenId_SelectionChanged;
            this.ComboBoxProcessorRun.SelectionChanged += new SelectionChangedEventHandler(ComboBoxProcessorRun_SelectionChanged);
            this.ComboBoxReceivedIn.SelectionChanged += new SelectionChangedEventHandler(ComboBoxReceivedIn_SelectionChanged);
            this.CheckBoxClientAccessioned.Checked +=new RoutedEventHandler(CheckBoxClientAccessioned_Checked);
            this.CheckBoxClientAccessioned.Unchecked +=new RoutedEventHandler(CheckBoxClientAccessioned_Unchecked);
        }

        private void SpecimenOrderDetailsPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void ComboBoxReceivedIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_SpecimenOrder.SetFixationStartTime();
            this.m_SpecimenOrder.SetTimeToFixation();            
        }

        private void ComboBoxProcessorRun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxProcessorRun.SelectedItem != null)
            {
                YellowstonePathology.Business.Surgical.ProcessorRun processorRun = (YellowstonePathology.Business.Surgical.ProcessorRun)this.ComboBoxProcessorRun.SelectedItem;
                this.m_SpecimenOrder.SetProcessor(processorRun);                               
            }
        }

        public YellowstonePathology.Business.Surgical.ProcessorRunCollection ProcessorRunCollection
        {
            get { return this.m_ProcessorRunCollection; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return this.m_SpecimenCollection; }
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.Next != null)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                this.Next(this, new EventArgs());
            }
		}				

        public ObservableCollection<string> FixationTypeCollection
		{
			get { return this.m_FixationTypeCollection; }
		}

        public YellowstonePathology.UI.Login.FinalizeAccession.SpecimenAdequacyTypes SpecimenAdequacyTypes
        {
            get { return this.m_SpecimenAdequacyTypes; }
        }		

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
            get { return this.m_SpecimenOrder; }
		}		

        private void HyperLinkReceivedFresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Fresh;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_SpecimenOrder.SetFixationStartTime();            
            this.m_SpecimenOrder.SetTimeToFixation();            
        }

        private void HyperLinkReceivedInFormalin_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_SpecimenOrder.SetFixationStartTime();
            this.m_SpecimenOrder.SetTimeToFixation();            
        }

        private void HyperLinkReceivedInBPlus_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.BPlusFixative;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.m_SpecimenOrder.SetFixationStartTime();
        }

        private void HyperLinkReceivedInCytolyt_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Cytolyt;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Cytolyt;
            this.m_SpecimenOrder.SetFixationStartTime();
            this.m_SpecimenOrder.SetTimeToFixation();            
        }

        private void HyperLinkReceived95PercentIPA_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.NinetyFivePercentIPA;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.NinetyFivePercentIPA;
            this.m_SpecimenOrder.SetFixationStartTime();
            this.m_SpecimenOrder.SetTimeToFixation();            
        }

        private void HyperLinkReceivedInNotApplicable_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable;
            this.m_SpecimenOrder.SetFixationStartTime();
            this.m_SpecimenOrder.SetTimeToFixation();            
        }

        private void HyperLinkReceivedInPreservCyte_Click(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.PreservCyt;
            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.PreservCyt;
            this.m_SpecimenOrder.SetFixationStartTime();
            this.m_SpecimenOrder.SetTimeToFixation();
            this.m_SpecimenOrder.SetFixationDuration();

            YellowstonePathology.Business.Surgical.NullProcessor nullProcessor = new Business.Surgical.NullProcessor();
            YellowstonePathology.Business.Surgical.ProcessorRun processorRun = nullProcessor.ProcessorRunCollection[0];
            this.m_SpecimenOrder.SetProcessor(processorRun);            
        }

        private void TextBoxCollectionDate_LostFocus(object sender, RoutedEventArgs e)
        {
            this.m_SpecimenOrder.SetFixationStartTime();
        }        

        private void CheckBoxClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            this.TextBoxFixationStartTime.IsEnabled = false;
            this.m_SpecimenOrder.FixationStartTime = null;

            this.m_SpecimenOrder.ClientFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.ComboBoxReceivedIn.IsEnabled = false;

            this.m_SpecimenOrder.LabFixation = YellowstonePathology.Business.Specimen.Model.FixationType.Formalin;
            this.ComboBoxProcessedIn.IsEnabled = false;
        }

        private void CheckBoxClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            this.TextBoxFixationStartTime.IsEnabled = true;
            this.m_SpecimenOrder.SetFixationStartTime();
        }

        private void HyperlinkClearContainerId_Click(object sender, RoutedEventArgs e)
        {            
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the Container ID.", "Clear Container ID", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                this.m_SpecimenOrder.ContainerId = null;                
                this.m_SpecimenOrder.DateReceived = null;
                this.m_SpecimenOrder.FixationStartTime = null;
            }            
        }

        private void ComboBoxSpecimenId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxSpecimenId.SelectedItem != null && this.ComboBoxSpecimenId.SelectedItem.GetType() != typeof(YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.NullSpecimen))
            {
                if (string.IsNullOrEmpty(this.m_SpecimenOrder.Description) == true)
                {
                    YellowstonePathology.Business.Specimen.Model.Specimen specimen = (YellowstonePathology.Business.Specimen.Model.Specimen)this.ComboBoxSpecimenId.SelectedItem;
                    this.m_SpecimenOrder.Description = specimen.Description;
                    this.m_SpecimenOrder.LabFixation = specimen.LabFixation;
                    this.m_SpecimenOrder.ClientFixation = specimen.ClientFixation;
                    this.m_SpecimenOrder.RequiresGrossExamination = specimen.RequiresGrossExamination;

                    this.HandleTemplatedSpecimen();
                    this.NotifyPropertyChanged("");
                }
            }
        }

        private void HandleTemplatedSpecimen()
        {
            int positionOfFirstBracket = this.TextBoxDescription.Text.IndexOf('[');
            if (positionOfFirstBracket != -1)
            {
                int positionOfLastBracket = this.TextBoxDescription.Text.IndexOf(']', positionOfFirstBracket);
                if (positionOfLastBracket != -1)
                {
                    this.TextBoxDescription.Focus();
                    this.TextBoxDescription.SelectionStart = positionOfFirstBracket;
                    this.TextBoxDescription.SelectionLength = positionOfLastBracket - positionOfFirstBracket + 1;
                }
            }
        }

        private void HyperlinkHoldSpecimen_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Surgical.HoldProcessor holdProcessor = new Business.Surgical.HoldProcessor();            
            this.m_SpecimenOrder.ProcessorRun = holdProcessor.ProcessorRunCollection[0].Name;
            this.m_SpecimenOrder.ProcessorRunId = holdProcessor.ProcessorRunCollection[0].ProcessorRunId;
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
