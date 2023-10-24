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

namespace YellowstonePathology.UI.Surgical
{
	/// <summary>
	/// Interaction logic for CommonHistory.xaml
	/// </summary>
	public partial class CommonHistory : UserControl
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.UI.Common.CaseHistoryPage m_CaseHistoryPage;

		public CommonHistory(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_CaseHistoryPage = new Common.CaseHistoryPage(this.m_AccessionOrder);

			InitializeComponent();

			this.DataContext = this;
			this.HistoryControl.Content = this.m_CaseHistoryPage;
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}
	}
}
