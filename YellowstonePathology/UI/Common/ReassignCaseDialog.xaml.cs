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
using System.ComponentModel;

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for ReassignCaseDialog.xaml
	/// </summary>
	public partial class ReassignCaseDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private Visibility m_ShowMessage;
		private bool m_CreateAmendment;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public ReassignCaseDialog(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PanelSetOrder = panelSetOrder;
            this.m_SystemIdentity = systemIdentity;

			ShowMessage = m_PanelSetOrder.Final == true ? Visibility.Visible : Visibility.Collapsed;
			CreateAmendment = false;
			InitializeComponent();
			this.DataContext = this;
		}

		public Visibility ShowMessage
		{
			get { return this.m_ShowMessage; }
			set
			{
				this.m_ShowMessage = value;
				NotifyPropertyChanged("ShowMessage");
			}
		}

		public bool CreateAmendment
		{
			get { return this.m_CreateAmendment; }
			set
			{
				this.m_CreateAmendment = value;
				NotifyPropertyChanged("CreateAmendment");
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.PanelSetOrder.ReassignCase reassignCase = new YellowstonePathology.Business.Rules.PanelSetOrder.ReassignCase();
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			reassignCase.Execute(executionStatus, this.m_PanelSetOrder, CreateAmendment, this.m_SystemIdentity);

			if (executionStatus.Halted)
			{
				YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
				ruleExecutionStatus.PopulateFromLinqExecutionStatus(executionStatus);
				RuleExecutionStatusDialog ruleExecutionStatusDialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
				ruleExecutionStatusDialog.ShowDialog();
				return;
			}
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
