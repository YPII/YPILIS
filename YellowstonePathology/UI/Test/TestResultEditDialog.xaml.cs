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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for TestResultEditDialog.xaml
	/// </summary>
	public partial class TestResultEditDialog : Window
	{
		private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;

		public TestResultEditDialog(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
		{
			this.m_TestOrder = testOrder;

			InitializeComponent();

			DataContext = this.m_TestOrder;
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
