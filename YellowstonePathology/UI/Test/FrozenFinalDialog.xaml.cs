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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for FrozenFinalDialog.xaml
	/// </summary>
	public partial class FrozenFinalDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult m_IntraoperativeConsultation;
        private YellowstonePathology.UI.TypingShortcutUserControl m_TypingShortcutUserControl;

        public FrozenFinalDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultation,
            YellowstonePathology.UI.TypingShortcutUserControl typingShortcutUserControl)
		{
			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);
			this.m_AccessionOrder = accessionOrder;
			this.m_IntraoperativeConsultation = intraoperativeConsultation;
            this.m_TypingShortcutUserControl = typingShortcutUserControl;

			YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)accessionOrder.PanelSetOrderCollection.GetSurgical();
			YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = (from ssr in panelSetOrderSurgical.SurgicalSpecimenCollection
																											where ssr.SurgicalSpecimenId == intraoperativeConsultation.SurgicalSpecimenId
																						   select ssr).Single<YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen>();
			this.m_SpecimenOrder = surgicalSpecimen.SpecimenOrder;
			InitializeComponent();

			this.DataContext = this;
			Closing += new System.ComponentModel.CancelEventHandler(FrozenFinalDialog_Closing);
		}

		private void FrozenFinalDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.m_IntraoperativeConsultation.ValidateObject();
			if (this.m_IntraoperativeConsultation.ValidationErrors.Count > 0)
			{
				MessageBoxResult result = MessageBox.Show("There are validation errors on this form." + Environment.NewLine + "If you continue you may lose data." +
					Environment.NewLine + Environment.NewLine + "Do you want to continue?", "Close?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
				if (result == MessageBoxResult.No)
				{
					e.Cancel = true;
				}
			}
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
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return this.m_SpecimenOrder; }
		}

		public YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult IntraoperativeConsultation
		{
			get { return this.m_IntraoperativeConsultation; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}

		private void HyperLinkSetTimes_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBoxResult.OK;
			if (this.m_IntraoperativeConsultation.StartDate.HasValue == true || this.m_IntraoperativeConsultation.EndDate.HasValue == true)
			{
				result = MessageBox.Show("Setting times will replace the current start and end times.", "Reset Times", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
			}

			if (result == MessageBoxResult.OK)
			{
				this.m_IntraoperativeConsultation.ClearDateValidationErrors();
				this.m_IntraoperativeConsultation.StartDate = this.m_SpecimenOrder.DateReceived;
				this.m_IntraoperativeConsultation.EndDate = DateTime.Now;
				this.NotifyPropertyChanged("");
			}
		}

		private void HyperLinkUnfinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_IntraoperativeConsultation.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
				this.m_IntraoperativeConsultation.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_IntraoperativeConsultation.IsOkToFinalize();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Audit.Model.IntraoperativeConsultationFinalAudit intraoperativeConsultationFinalAudit = new Business.Audit.Model.IntraoperativeConsultationFinalAudit(this.m_IntraoperativeConsultation);
				intraoperativeConsultationFinalAudit.Run();
				if (intraoperativeConsultationFinalAudit.ActionRequired == true)
				{
					MessageBox.Show(intraoperativeConsultationFinalAudit.Message.ToString(), "Missing Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
				else
				{
					this.m_IntraoperativeConsultation.SetFinal();
				}
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ComboBoxFinaledBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboBoxFinaledBy.SelectedItem != null)
			{
				this.m_IntraoperativeConsultation.FinaledBy = ((YellowstonePathology.Business.User.SystemUser)this.ComboBoxFinaledBy.SelectedItem).DisplayName;
			}
		}

        private void TextBox_KeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Space)
            {
                TextBox textBox = (TextBox)args.Source;
                this.m_TypingShortcutUserControl.SetShortcut(textBox);
            }
        }
    }
}
