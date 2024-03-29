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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Linq.SqlClient;
using YellowstonePathology.Business.ClientOrder.Model;

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class ClientOrderDetailsPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;        
		
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;		

		private string m_PageHeaderText = "Specimen Details Page";
        private ObservableCollection<string> m_FixationTypeCollection;
        //private string m_SpecialInstructions;        

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private ObservableCollection<string> m_TimeToFixationTypeCollection;
        private Business.ClientOrder.Model.ClientOrder m_ClientOrder;

		public ClientOrderDetailsPage(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, 
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail,
            Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_PageNavigator = pageNavigator;
            this.m_ClientOrderDetail = clientOrderDetail;
            //this.m_SpecialInstructions = clientOrderDetail.SpecialInstructions;
            this.m_ClientOrder = clientOrder;        

            this.m_TimeToFixationTypeCollection = Business.Specimen.Model.TimeToFixationType.GetTimeToFixationTypeCollection();
            this.m_FixationTypeCollection = Business.Specimen.Model.FixationType.GetFixationTypeCollection();
			this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;

            this.DataContext = this;
			InitializeComponent();
            
            this.Loaded +=new RoutedEventHandler(ClientOrderDetailsPage_Loaded);
            this.Unloaded += new RoutedEventHandler(ClientOrderDetailsPage_Unloaded);            
		}

        private void ClientOrderDetailsPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
        }

        private void ClientOrderDetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;                              
            this.FixTimes();

            this.TextBoxAccessionAs.Focus();

            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.SpecimenId) == true)
            {
                this.ComboBoxSpecimenId.Focus();
            }

            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == false)
            {
                if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("THIN PREP") == true)
                {
                    this.TextBoxSpecimenSource.Focus();
                }
            }

            TextBox textBox = (TextBox)this.ComboBoxSpecimenId.Template.FindName("PART_EditableTextBox", this.ComboBoxSpecimenId);
            if (textBox != null)
            {
                textBox.LostFocus += TextBoxInComboBox_LostFocus;
            }

            this.ComboBoxReceivedIn.SelectionChanged += new SelectionChangedEventHandler(ComboBoxReceivedIn_SelectionChanged);
            this.CheckBoxClientAccessioned.Checked +=new RoutedEventHandler(CheckBoxClientAccessioned_Checked);
            this.CheckBoxClientAccessioned.Unchecked +=new RoutedEventHandler(CheckBoxClientAccessioned_Unchecked);

            //HandleSpecimenDescription(false, this.m_ClientOrderDetail, this.m_ClientOrder);
        }

        private void FixTimes()
        {
           this.m_ClientOrderDetail.CollectionDate = Business.Helper.DateTimeExtensions.RemoveSeconds(this.m_ClientOrderDetail.CollectionDate);
           this.m_ClientOrderDetail.FixationStartTime = Business.Helper.DateTimeExtensions.RemoveSeconds(this.m_ClientOrderDetail.FixationStartTime);
        }


        private void ComboBoxReceivedIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_ClientOrderDetail.SetFixationStartTime();
            this.NotifyPropertyChanged(string.Empty);
        }

		private void ContainerScanReceived(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                if (string.IsNullOrEmpty(this.m_ClientOrderDetail.ContainerId) == true)
                {
                    this.m_ClientOrderDetail.ContainerIdBinding = containerBarcode.ToString();
                    this.m_ClientOrderDetail.SetFixationStartTime();
                    this.m_ClientOrderDetail.Receive();
                }
                else
                {
                    MessageBox.Show("Unable to set the Container ID because it is already set.");
                }                
            }
            ));
        }

        public ObservableCollection<string> TimeToFixationTypeCollection
        {
            get { return this.m_TimeToFixationTypeCollection; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetActive(); }
        }

        /*
        public string SpecialInstructions
        {
            get { return this.m_SpecialInstructions; }
            set { this.m_SpecialInstructions = value; }
        }
        */

        public ObservableCollection<string> FixationTypeCollection
        {
            get { return this.m_FixationTypeCollection; }
        }			

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}
        
		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
		{
			get { return this.m_ClientOrderDetail; }
		}

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
            if(this.Back != null) this.Back(this, new EventArgs());
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.ClientOrderDetail.ClientAccessioned == false)
            {
                if (this.HandleBreastFixationTime() == true)
                {
                    this.m_ClientOrderDetail.ValidateObject();

                    if (this.m_ClientOrderDetail.ValidationErrors.Count > 0)
                    {
                        this.CheckFixationStartTimeOrCollectionTimeValidationErrors();
                        MessageBoxResult messageBoxResult = MessageBox.Show("There are validation errors on this form.  Are you sure you want to continue?", "Validation Errors", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
                            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                            this.Next(this, new EventArgs());
                        }
                    }
                    else
                    {
                        this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                        this.Next(this, new EventArgs());
                    }
                }
            }
            else
            {
                this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                this.Next(this, new EventArgs());
            }
        }

        private bool HandleBreastFixationTime()
        {
            bool result = true;
            if(string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == false)
            {
                if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("BREAST") == true)
                {
                    if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_ClientOrderDetail.CollectionDate) == false)
                    {
                        MessageBoxResult collectionDateResult = MessageBox.Show("This case appears to be a breast case and there is no Collection Time. Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo);
                        if (collectionDateResult == MessageBoxResult.No) result = false;
                    }
                    else
                    {
                        if(this.m_ClientOrderDetail.FixationStartTime.HasValue == true)
                        {
                            DateTime todayAt500 = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + "T17:00");
                            Business.Surgical.ProcessorRun run = new Business.Surgical.ProcessorRun("This Afternoon", todayAt500, new TimeSpan(2, 30, 0));
                            DateTime fixationEndTime = run.GetFixationEndTime(this.m_ClientOrderDetail.FixationStartTime.Value);
                            TimeSpan fixationDuration = fixationEndTime.Subtract(this.m_ClientOrderDetail.FixationStartTime.Value);
                            if (fixationDuration.TotalHours < 6)
                            {
                                MessageBox.Show("Warning! Fixation duration will be under 6 hours unless this specimen is held.");                                    
                            }
                            else if (fixationDuration.TotalHours > 72)
                            {
                                MessageBox.Show("Warning! Fixation duration will be over 72 hours if processed normally.");                                    
                            }                            
                        }                        
                    }                    
                }
            }
            return result;
        }

        private void CheckFixationStartTimeOrCollectionTimeValidationErrors()
        {
            DateTime? fixedFixationStartTime = Business.Helper.DateTimeExtensions.RemoveSeconds(this.m_ClientOrderDetail.FixationStartTime);
            DateTime? fixedCollectionDate = Business.Helper.DateTimeExtensions.RemoveSeconds(this.m_ClientOrderDetail.CollectionDate);
            if (this.m_ClientOrderDetail.ValidationErrors.ContainsKey("CollectionDateBinding") == true)
            {
                if(this.m_ClientOrderDetail.ValidationErrors["CollectionDateBinding"] == "The Collection Time cannot be after the Fixation Start Time.")
                {
                    if(fixedFixationStartTime.Value == fixedCollectionDate.Value)
                    {
                        MessageBox.Show("Please contact IT before continuing!  IT is trying to determine how this happens." + Environment.NewLine + "The message is 'The Collection Time cannot be after the Fixation Start Time.'.");
                    }
                }
            }
            else if (this.m_ClientOrderDetail.ValidationErrors.ContainsKey("FixationStartTimeBinding") == true)
            {
                if (this.m_ClientOrderDetail.ValidationErrors["FixationStartTimeBinding"] == "The Fixation Start Time cannot be before the Collection Date.")
                {
                    if (fixedFixationStartTime.Value == fixedCollectionDate.Value)
                    {
                        MessageBox.Show("Please contact IT before continuing!  IT is trying to determine how this happens." + Environment.NewLine + "The message is 'The Fixation Start Time cannot be before the Collection Date.'.");
                    }
                }
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult IsOkToNavigate()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = true;
                        
            if (this.m_ClientOrderDetail.FixationStartTimeManuallyEntered == true && string.IsNullOrEmpty(this.m_ClientOrderDetail.FixationComment) == true)
            {
                result.Success = false;
                result.Message = "The Fixation Start Time has been manually set so you must provide a comment before continuing.";
            }            

            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccession) == false)
            {
                if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("PROSTATE") == true)
                {
                    if (string.IsNullOrEmpty(this.m_ClientOrderDetail.SpecimenId) == true)
                    {
                        result.Success = false;
                        result.Message = "You must select the specimen id for prostate specimens.";
                    }
                }
                else if (this.m_ClientOrderDetail.DescriptionToAccession.ToUpper().Contains("APPENDIX, EXCISION") == true)
                {
                    if (string.IsNullOrEmpty(this.m_ClientOrderDetail.SpecimenId) == true)
                    {
                        result.Success = false;
                        result.Message = "You must select the specimen id for Appendix, excision specimens.";
                    }
                }
            }            

            return result;
        }        

        private bool HandleContainerIdValidation()
        {
            bool result = false;

            if (string.IsNullOrEmpty(this.m_ClientOrderDetail.ContainerId) == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("The Container ID is blank. Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }

            return result;
        }      		

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }              

        private void TextBoxAccessionAs_GotFocus(object sender, RoutedEventArgs e)
        {
            int positionOfFirstBracket = this.TextBoxAccessionAs.Text.IndexOf('[');
            if (positionOfFirstBracket != -1)
            {
                int positionOfLastBracket = this.TextBoxAccessionAs.Text.IndexOf(']', positionOfFirstBracket);
                if (positionOfLastBracket != -1)
                {
                    this.TextBoxAccessionAs.Focus();
                    this.TextBoxAccessionAs.SelectionStart = positionOfFirstBracket;
                    this.TextBoxAccessionAs.SelectionLength = positionOfLastBracket - positionOfFirstBracket + 1;
                }
            }
        }        

        private void HyperLinkReceivedFresh_Click(object sender, RoutedEventArgs e)
        {            
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.Fresh;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.Formalin;            
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInFormalin_Click(object sender, RoutedEventArgs e)
        {            
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInBPlus_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.BPlusFixative;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.Formalin;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInCytolyt_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.Cytolyt;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.PreservCyt;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceived95PercentIPA_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.NinetyFivePercentIPA;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.NinetyFivePercentIPA;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInNotApplicable_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.NotApplicable;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.NotApplicable;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperLinkReceivedInPreservCyt_Click(object sender, RoutedEventArgs e)
        {
            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.PreservCyt;
            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.PreservCyt;
            this.m_ClientOrderDetail.SetFixationStartTime();
        }   

        private void CheckBoxClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            this.TextBoxFixationStartTime.IsEnabled = false;
            this.m_ClientOrderDetail.FixationStartTimeBinding = null;

            this.m_ClientOrderDetail.ClientFixationBinding = Business.Specimen.Model.FixationType.Formalin;
            this.ComboBoxReceivedIn.IsEnabled = false;

            this.m_ClientOrderDetail.LabFixationBinding = Business.Specimen.Model.FixationType.Formalin;
            this.ComboBoxProcessedIn.IsEnabled = false;            
        }

        private void CheckBoxClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {            
            this.m_ClientOrderDetail.SetFixationStartTime();
        }

        private void HyperlinkClearContainerId_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ClientOrderDetail.Accessioned == false)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to clear the Container ID.", "Clear Container ID", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    this.m_ClientOrderDetail.ContainerId = null;
                    this.m_ClientOrderDetail.Received = false;
                    this.m_ClientOrderDetail.DateReceived = null;
                    this.m_ClientOrderDetail.FixationStartTime = null;
                    this.NotifyPropertyChanged(string.Empty);
                }
            }
            else
            {
                MessageBox.Show("The Container Id cannot be cleared because the specimen has been accessioned.");
            }
        }

        private void TextBoxInComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.ComboBoxSpecimenId.SelectedItem != null)
            {
                YellowstonePathology.Business.Specimen.Model.Specimen specimen = (YellowstonePathology.Business.Specimen.Model.Specimen)this.ComboBoxSpecimenId.SelectedItem;
                if (string.IsNullOrEmpty(this.m_ClientOrderDetail.DescriptionToAccessionBinding) == true)
                {
                    this.m_ClientOrderDetail.DescriptionToAccessionBinding = specimen.Description;
                }
                this.m_ClientOrderDetail.LabFixationBinding = specimen.LabFixation;
                this.m_ClientOrderDetail.ClientFixationBinding = specimen.ClientFixation;
                this.m_ClientOrderDetail.RequiresGrossExamination = specimen.RequiresGrossExamination;
                this.NotifyPropertyChanged("");
            }
        }

        private void ButtonSetReceiveDate_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_ClientOrder.ClientOrderDetailCollection.Count > 1)
            {
                if(this.m_ClientOrder.ClientOrderDetailCollection.IndexOf(this.m_ClientOrderDetail) != 0)
                {
                    Business.ClientOrder.Model.ClientOrderDetail firstClientOrderDetail = this.m_ClientOrder.ClientOrderDetailCollection[0];
                    if(firstClientOrderDetail.DateReceived.HasValue == true)
                    {
                        this.m_ClientOrderDetail.DateReceived = firstClientOrderDetail.DateReceived;
                        this.m_ClientOrderDetail.SetFixationStartTime();
                    }
                }
            }
        }

        private void HyperlinkImportSpecimenDescription_Click(object sender, RoutedEventArgs e)
        {
            //HandleSpecimenDescription(true, this.m_ClientOrderDetail, this.m_ClientOrder);
        }

        public static string GetSanitizedDescription(int specimenNumber, string clientOrderSpecialInstructions)
        {
            string result = null;
                
            string specimenLetter = GetSpecimenLetter(specimenNumber).ToString();
            string[] lines = clientOrderSpecialInstructions.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {                    
                if (lines[i].Contains($"{specimenLetter})") == true && i != 0)
                {
                    string endString = lines[i - 1];
                    if (endString.Contains("path")) endString = "biopsy";

                    string description = $"Skin, {lines[i]}, {endString}";
                    description = description.Replace("bx", "biopsy");
                    description = description.Replace($"Specimen Source: {specimenLetter}) ", "");
                    description = description.Replace("Specimen Label: ", "");                    
                    result = description;             
                }
            }
            return result;
        }

        static char GetSpecimenLetter(int specimenNumber)
        {
            if (specimenNumber >= 1 && specimenNumber < 26)
            {
                // Add 'A' to the index to get the corresponding letter.                
                return (char)('A' + (specimenNumber - 1));
            }
            else
            {
                // Return null character '\0' to indicate an error.
                return '\0';
            }
        }
    }
}
