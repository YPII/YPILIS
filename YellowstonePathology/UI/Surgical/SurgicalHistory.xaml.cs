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

namespace YellowstonePathology.UI.Surgical
{
	/// <summary>
	/// Interaction logic for SurgicalHistory.xaml
	/// </summary>
	public partial class SurgicalHistory : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private PathologistUI m_PathologistUI;
		private YellowstonePathology.UI.Common.CaseHistoryPage m_CaseHistoryPage;

		public SurgicalHistory(PathologistUI pathologistUI)
		{
			this.m_PathologistUI = pathologistUI;
			this.m_CaseHistoryPage = new Common.CaseHistoryPage(this.AccessionOrder);

			InitializeComponent();

			this.DataContext = this;
			this.HistoryControl.Content = this.m_CaseHistoryPage;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_PathologistUI.AccessionOrder; }
		}

		public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder PanelSetOrderSurgical
		{
			get { return (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_PathologistUI.PanelSetOrder; }
		}

		public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
		{
			get { return this.m_PathologistUI.FieldEnabler; }
			set { this.m_PathologistUI.FieldEnabler = value; }
		}
	}
}
