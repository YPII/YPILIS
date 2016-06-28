using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for IntraoperativeConsultationSelection.xaml
	/// </summary>
	public partial class IntraoperativeConsultationSelection : Window
	{
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderCollectiion;

		public IntraoperativeConsultationSelection(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection)
		{
			m_SpecimenOrderCollectiion = specimenOrderCollection;
			InitializeComponent();
			this.DataContext = this.m_SpecimenOrderCollectiion;
		}

		private void ButtonAccept_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			Close();
		}
	}
}
