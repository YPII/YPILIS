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
	/// Interaction logic for PatientEdit.xaml
	/// </summary>
	public partial class PatientEditDialog : Window
	{
		private YellowstonePathology.Business.Interface.IPatientOrder m_PatientOrder;

		public PatientEditDialog(YellowstonePathology.Business.Interface.IPatientOrder patientOrder)
		{
			m_PatientOrder = patientOrder;
			this.DataContext = m_PatientOrder;
			InitializeComponent();
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			IInputElement focusedElement = Keyboard.FocusedElement;
			FrameworkElement element = (FrameworkElement)focusedElement;
			element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			Close();
		}
	}
}
