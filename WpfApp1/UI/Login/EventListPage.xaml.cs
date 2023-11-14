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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for EventListPage.xaml
	/// </summary>
	public partial class EventListPage : Page
	{
		private YellowstonePathology.Business.Domain.OrderCommentLogCollection m_OrderCommentLogCollection;

		public EventListPage(YellowstonePathology.Business.Domain.OrderCommentLogCollection orderCommentLogCollection)
		{
			this.m_OrderCommentLogCollection = orderCommentLogCollection;

			InitializeComponent();

			this.DataContext = this;            
		}

		public YellowstonePathology.Business.Domain.OrderCommentLogCollection OrderCommentLogCollection
		{
			get { return this.m_OrderCommentLogCollection; }
		}

        private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
        {

        }
	}
}
