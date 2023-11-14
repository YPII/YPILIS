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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for OrderCommentDialog.xaml
	/// </summary>
	public partial class OrderCommentDialog : Window
	{
		XElement m_Orders;

		public OrderCommentDialog(XElement orders)
		{
			this.m_Orders = orders;
			// use xml in constuctor as data context
			InitializeComponent();
			this.ListBoxOrderComments.DataContext = this.m_Orders.Element("Specimens").Elements();
		}

		private void ButtonOrder_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
