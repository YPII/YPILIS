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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{
	/// <summary>
	/// Interaction logic for ImmediateExamCorrelationDialog.xaml
	/// </summary>
	public partial class ImmediateExamCorrelationDialog : Window
	{
		private YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult m_IntraoperativeConsultationResult;
		private List<string> m_CorrelationTypes;
		private List<string> m_DescrepancyTypes;
		private List<string> m_patientCareEffects;

		public ImmediateExamCorrelationDialog(YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult)
		{
			this.m_IntraoperativeConsultationResult = intraoperativeConsultationResult;
			this.m_CorrelationTypes = new List<string>();
			this.m_CorrelationTypes.Add("Not correlated");
			this.m_CorrelationTypes.Add("Agree");
			this.m_CorrelationTypes.Add("Defer");
			this.m_CorrelationTypes.Add("Not applicable");
			this.m_CorrelationTypes.Add("Disagree");

			this.m_DescrepancyTypes = new List<string>();
			this.m_DescrepancyTypes.Add("Gross exam/microscopic exam discrepancy");
			this.m_DescrepancyTypes.Add("Misinterpretation");
			this.m_DescrepancyTypes.Add("Specimen sampling");
			this.m_DescrepancyTypes.Add("Tissue block sampling");
			this.m_DescrepancyTypes.Add("Technical (sectioning) inadequacy");
			this.m_DescrepancyTypes.Add("Inadequate clinical information");
			this.m_DescrepancyTypes.Add("Labeling error");

			this.m_patientCareEffects = new List<string>();
			this.m_patientCareEffects.Add("No impact on patient care");
			this.m_patientCareEffects.Add("Minor or questionable impact on patient care");
			this.m_patientCareEffects.Add("Major or potentially major impact on patient care");
			InitializeComponent();
			DataContext = this;
		}

		public YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult IntraoperativeConsultationResult
		{
			get { return this.m_IntraoperativeConsultationResult; }
		}

		public List<string> CorrelationTypes
		{
			get { return this.m_CorrelationTypes; }
		}

		public List<string> DescrepancyTypes
		{
			get { return this.m_DescrepancyTypes; }
		}

		public List<string> PatientCareEffects
		{
			get { return this.m_patientCareEffects; }
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ListBoxCorrelation_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ListBoxCorrelation.SelectedItem != null && this.ListBoxCorrelation.SelectedItem.ToString() != "Disagree")
			{
				this.m_IntraoperativeConsultationResult.CorrelationEffectOnPatientCare = string.Empty;
				this.m_IntraoperativeConsultationResult.CorrelationDiscrepancyType = string.Empty;
			}
		}
	}
}
