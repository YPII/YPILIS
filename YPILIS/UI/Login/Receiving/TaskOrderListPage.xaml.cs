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

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for TaskOrderListPage.xaml
	/// </summary>
	public partial class TaskOrderListPage : UserControl 
	{

		public delegate void NextEventHandler(object sender, CustomEventArgs.TaskOrderReturnEventArgs e);
		public event NextEventHandler Next;

		public delegate void CloseEventHandler(object sender, EventArgs e);
		public event CloseEventHandler Close;
		
		private string m_ReportNo;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;
		private PageNavigationModeEnum m_PageNavigationMode;

		public TaskOrderListPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			string reportNo,
			PageNavigationModeEnum pageNavigationMode)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
			this.m_PageNavigationMode = pageNavigationMode;
            this.m_TaskOrderCollection = accessionOrder.TaskOrderCollection;
			
			InitializeComponent();

			this.SetButtonVisibility();
			DataContext = this;
		}

		private void SetButtonVisibility()
		{
			switch (this.m_PageNavigationMode)
			{
				case PageNavigationModeEnum.Inline:
					this.ButtonClose.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonNext.Visibility = System.Windows.Visibility.Collapsed;
					break;
				case PageNavigationModeEnum.Standalone:
					this.ButtonClose.Visibility = System.Windows.Visibility.Visible;
					this.ButtonNext.Visibility = System.Windows.Visibility.Visible;
					break;
			}
		}

		public YellowstonePathology.Business.Task.Model.TaskOrderCollection TaskOrderCollection
		{
			get { return this.m_TaskOrderCollection; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save(bool releaseLock)
		{
		}

		public void UpdateBindingSources()
		{
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewTaskOrder.SelectedItem != null)
			{
				YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = (YellowstonePathology.Business.Task.Model.TaskOrder)this.ListViewTaskOrder.SelectedItem;
				CustomEventArgs.TaskOrderReturnEventArgs args = new CustomEventArgs.TaskOrderReturnEventArgs(taskOrder);
				this.Next(this, args);
			}
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			if (this.Close != null) this.Close(this, new EventArgs());
		}
	}
}
