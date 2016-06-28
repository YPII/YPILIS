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
	public partial class FNAResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;        

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder m_FNAAdequacyAssessmentResult;

        public FNAResultPage(YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder fnaAdequacyAssessmentResult,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(fnaAdequacyAssessmentResult, accessionOrder)
		{            
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

			this.m_FNAAdequacyAssessmentResult = fnaAdequacyAssessmentResult;

            this.m_PageHeaderText = "FNA Time Entry: " + this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();            

			this.DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder FNAAdequacyAssessmentResult
        {
            get { return this.m_FNAAdequacyAssessmentResult; }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}				        

        private bool ValidationIsHandled()
        {
            bool result = false;
            YellowstonePathology.Business.Validation.ValidationResult dataTypeValidationResult = this.ValidatDataTypesAndUpdateBindingSources();
            if (dataTypeValidationResult.IsValid == true)
            {
                YellowstonePathology.Business.Validation.ValidationResultCollection domainValidationResult = this.ValidateDomain();
                if (domainValidationResult.IsValid() == true)
                {
                    result = true;
                }
                else
                {
                    result = false;
                    MessageBox.Show(domainValidationResult.GetMessage());
                }
            }
            else
            {
                result = false;
                MessageBox.Show(dataTypeValidationResult.Message);
            }
            return result;
        }

        private YellowstonePathology.Business.Validation.ValidationResult ValidatDataTypesAndUpdateBindingSources()
        {            
            YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;
            StringBuilder resultMessage = new StringBuilder();

            BindingExpression startDateBindingExpression = this.TextBoxStartDate.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Business.Validation.ValidationResult startDateDataTypeValidationResult = YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder.IsDateDataTypeValid(this.TextBoxStartDate.Text);
            if (startDateDataTypeValidationResult.IsValid == true)
            {
                startDateBindingExpression.UpdateSource();                
            }
            else
            {
                resultMessage.AppendLine(startDateDataTypeValidationResult.Message);
                validationResult.IsValid = false;
            }

            BindingExpression endDateBindingExpression = this.TextBoxEndDate.GetBindingExpression(TextBox.TextProperty);
			YellowstonePathology.Business.Validation.ValidationResult endDateDataTypeValidationResult = YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder.IsDateDataTypeValid(this.TextBoxEndDate.Text);
            if (endDateDataTypeValidationResult.IsValid == true)
            {
                endDateBindingExpression.UpdateSource();
            }
            else
            {
                resultMessage.AppendLine(endDateDataTypeValidationResult.Message);
                validationResult.IsValid = false;
            }

            validationResult.Message = resultMessage.ToString();
            return validationResult;
        }

        private YellowstonePathology.Business.Validation.ValidationResultCollection ValidateDomain()
        {
            YellowstonePathology.Business.Validation.ValidationResultCollection validationResultCollection = new Business.Validation.ValidationResultCollection();
            validationResultCollection.Add(this.m_FNAAdequacyAssessmentResult.IsEndDateGreaterThanStartDate());
            validationResultCollection.Add(this.m_FNAAdequacyAssessmentResult.IsEndDateBlank());
            validationResultCollection.Add(this.m_FNAAdequacyAssessmentResult.IsStartDateBlank());
            validationResultCollection.Add(this.m_FNAAdequacyAssessmentResult.DoesStartDateHaveATime());
            validationResultCollection.Add(this.m_FNAAdequacyAssessmentResult.DoesEndDateHaveATime());
            return validationResultCollection;
        }

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.ValidationIsHandled() == true)
			{
				if (this.Next != null) this.Next(this, new EventArgs());
			}
		}

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_FNAAdequacyAssessmentResult.Final == false)
			{
				if (this.ValidationIsHandled() == true)
				{
					this.m_FNAAdequacyAssessmentResult.Finish(this.m_AccessionOrder);
				}
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_FNAAdequacyAssessmentResult.Final == true)
			{
				this.m_FNAAdequacyAssessmentResult.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}
	}
}
