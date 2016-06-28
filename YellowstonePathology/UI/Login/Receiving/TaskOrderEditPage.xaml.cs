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
	/// Interaction logic for TaskOrderEditPage.xaml
	/// </summary>
	public partial class TaskOrderEditPage : UserControl 
	{
		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void CloseEventHandler(object sender, EventArgs e);
		public event CloseEventHandler Close;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Task.Model.TaskOrderDetail m_TaskOrderDetail;		
		private PageNavigationModeEnum m_PageNavigationMode;

		public TaskOrderEditPage(YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			PageNavigationModeEnum pageNavigationMode)
		{
			this.m_TaskOrderDetail = taskOrderDetail;
			this.m_AccessionOrder = accessionOrder;
			this.m_PageNavigationMode = pageNavigationMode;

			InitializeComponent();

			this.SetButtonVisibility();
			this.DataContext = this;

            Loaded += TaskOrderEditPage_Loaded;
            Unloaded += TaskOrderEditPage_Unloaded;
		}

        private void TaskOrderEditPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void TaskOrderEditPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void SetButtonVisibility()
		{
			switch (this.m_PageNavigationMode)
			{
				case PageNavigationModeEnum.Inline:
					this.ButtonBack.Visibility = System.Windows.Visibility.Visible;
					this.ButtonClose.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonNext.Visibility = System.Windows.Visibility.Visible;
					break;
				case PageNavigationModeEnum.Standalone:
					this.ButtonBack.Visibility = System.Windows.Visibility.Visible;
					this.ButtonClose.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonNext.Visibility = System.Windows.Visibility.Visible;
					break;
			}
		}

		public YellowstonePathology.Business.Task.Model.TaskOrderDetail TaskOrderDetail
		{
			get { return this.m_TaskOrderDetail; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}				

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.Next != null)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                this.Next(this, new EventArgs());
            }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			if (this.Close != null) this.Close(this, new EventArgs());
		}
	}
}
