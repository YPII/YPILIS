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

namespace YellowstonePathology.UI
{
	/// <summary>
	/// Interaction logic for AmendmentEditPage.xaml
	/// </summary>
	public partial class AmendmentEditPage : Page
	{
		public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.User.SystemUserCollection m_AmendmentSigners;
		private AmendmentUI m_AmendmentUI;

		public AmendmentEditPage(AmendmentUI amendmentUI)
		{
			this.m_AmendmentUI = amendmentUI;
			this.AmendmentSigners = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.AmendmentSigner, true);

			InitializeComponent();

			DataContext = this;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public YellowstonePathology.Business.Amendment.Model.Amendment SelectedAmendment
		{
			get { return this.m_AmendmentUI.SelectedAmendment; }
			set
			{
				this.m_AmendmentUI.SelectedAmendment = value;
				NotifyPropertyChanged("SelectedAmendment");
			}
		}

		public YellowstonePathology.Business.User.SystemUserCollection AmendmentSigners
		{
			get { return this.m_AmendmentSigners; }
			set
			{
				this.m_AmendmentSigners = value;
				NotifyPropertyChanged("AmendmentSigners");
			}
		}

		public void GridSelectedAmendment_KeyUp(object sender, KeyEventArgs args)
		{
			if (args.Key == Key.F7)
			{
				this.CheckSpelling();
			}
		}

		public void ComboBoxAmendmentUsers_SelectionChanged(object sender, RoutedEventArgs args)
		{
			if (this.SelectedAmendment != null && this.comboBoxAmendmentUsers.SelectedItem != null)
			{
				YellowstonePathology.Business.User.SystemUser systemUserItem = (YellowstonePathology.Business.User.SystemUser)this.comboBoxAmendmentUsers.SelectedItem;
				this.SelectedAmendment.PathologistSignature = systemUserItem.Signature;
			}
		}

		public void UpdateSource()
		{
			TextBoxAmendmentDate.GetBindingExpression(TextBox.TextProperty).UpdateSource();
			TextBoxAmendmentTime.GetBindingExpression(TextBox.TextProperty).UpdateSource();
			TextBoxFinalDate.GetBindingExpression(TextBox.TextProperty).UpdateSource();
			TextBoxFinalTime.GetBindingExpression(TextBox.TextProperty).UpdateSource();
			TextBoxAmendment.GetBindingExpression(TextBox.TextProperty).UpdateSource();
		}

		private void CheckSpelling()
		{
			YellowstonePathology.Business.Common.SpellChecker spellCheck = new YellowstonePathology.Business.Common.SpellChecker();
			YellowstonePathology.Business.Common.AmendmentSpellCheckList amendmentSpellCheckList = new YellowstonePathology.Business.Common.AmendmentSpellCheckList(this.SelectedAmendment);
			spellCheck.CheckSpelling(amendmentSpellCheckList);
			MessageBox.Show("Spell check is complete.");
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.GoBack();
		}

		private void ButtonFinalize_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Test.OkToFinalizeResult okToFinalizeResult = this.SelectedAmendment.IsOkToFinalize(this.m_AmendmentUI.AccessionOrder);
            if (okToFinalizeResult.OK == true)
            {
                bool canFinal = true;
                if (okToFinalizeResult.ShowWarningMessage == true)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(okToFinalizeResult.Message, "Issue with the amendment", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.No)
                    {
                        canFinal = false;
                    }
                }
                if (canFinal == true)
                {
                    this.SelectedAmendment.Finish();
                }
            }
            else
            {
                MessageBox.Show(okToFinalizeResult.Message);
            }

            NotifyPropertyChanged("SelectedAmendment");
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            if(this.m_AmendmentUI.SelectedAmendment.AmendmentType == "Correction")
            {
                if(string.IsNullOrEmpty(this.m_AmendmentUI.SelectedAmendment.CorrectionType) == true)
                {
                    MessageBox.Show("You must choose the type of correction for this amendment.");
                    return;
                }
            }

			Window window = Window.GetWindow(this);
			window.Close();
		}
	}
}
