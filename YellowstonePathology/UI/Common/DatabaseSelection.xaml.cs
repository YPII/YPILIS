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
using System.Threading;

namespace YellowstonePathology.UI.Common
{
	public partial class DatabaseSelection : Window
	{
		private YellowstonePathology.Business.Common.DatabaseSelectionModel m_DatabaseSelectionModel;

		public DatabaseSelection()
		{
			m_DatabaseSelectionModel = new YellowstonePathology.Business.Common.DatabaseSelectionModel();
			InitializeComponent();
			this.DataContext = m_DatabaseSelectionModel;
			this.TextBoxPassword.Focus();
		}

		private void RadioButton_Click(object sender, RoutedEventArgs e)
		{
			m_DatabaseSelectionModel.ConnectionString = ((RadioButton)sender).Tag.ToString();
			App.Current.Shutdown();
		}

		private void TextBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
		{
			m_DatabaseSelectionModel.CheckPassword(this.TextBoxPassword.Text);
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
