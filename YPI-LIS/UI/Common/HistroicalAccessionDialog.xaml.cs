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

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for HistroicalAccessionDialog.xaml
	/// </summary>
	public partial class HistroicalAccessionDialog : Window
	{
		private YellowstonePathology.Business.Patient.Model.PatientHistoryList m_PatientHistoryList;
		private string m_CorrelatedAccessionNo;

		public HistroicalAccessionDialog(string reportNo)
		{

			m_PatientHistoryList = new YellowstonePathology.Business.Patient.Model.PatientHistoryList();
			this.DataContext = m_PatientHistoryList;
			InitializeComponent();

			m_PatientHistoryList.SetFillCommandByAccessionNo(reportNo);
			this.m_PatientHistoryList.Fill();
			m_CorrelatedAccessionNo = string.Empty;
		}

		public string CorrelatedAccessionNo
		{
			get { return m_CorrelatedAccessionNo; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			Close();
		}

		private void listViewAccessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listViewAccessions.SelectedItem != null)
			{
				m_CorrelatedAccessionNo = ((YellowstonePathology.Business.Patient.Model.PatientHistoryListItem)listViewAccessions.SelectedItem).ReportNo;
			}
		}
	}
}
