using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for Amendment.xaml
    /// </summary>

    public partial class AmendmentV2 : System.Windows.Window
    {
        YellowstonePathology.Business.Amendment.Model.Amendment m_Amendment;
		YellowstonePathology.Business.User.SystemUserCollection m_Users;

		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public AmendmentV2(YellowstonePathology.Business.Amendment.Model.Amendment amendment, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_Amendment = amendment;
			this.m_AccessionOrder = accessionOrder;

			this.m_Users = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.AmendmentSigner, true);
			InitializeComponent();

			this.DataContext = this.m_Amendment;
			this.comboBoxAmendmentUsers.ItemsSource = this.m_Users;

            Loaded += AmendmentV2_Loaded;
            Unloaded += AmendmentV2_Unloaded;
		}

        private void AmendmentV2_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void AmendmentV2_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void ComboBoxAmendmentUsers_SelectionChanged(object sender, RoutedEventArgs args)
        {
			YellowstonePathology.Business.User.SystemUser systemUserItem = (YellowstonePathology.Business.User.SystemUser)this.comboBoxAmendmentUsers.SelectedItem;
            this.m_Amendment.PathologistSignature = systemUserItem.Signature;
        }

        public void GridAmendment_KeyUp(object sender, KeyEventArgs args)
        {            
            if (args.Key == Key.F7)
            {
                this.CheckSpelling();
            }            
        }

        public void CheckSpelling()
        {         
            this.ButtonOk.Focus();            
            YellowstonePathology.Business.Common.SpellChecker spellCheck = new YellowstonePathology.Business.Common.SpellChecker();
            YellowstonePathology.Business.Common.AmendmentSpellCheckList amendmentSpellCheckList = new YellowstonePathology.Business.Common.AmendmentSpellCheckList(this.m_Amendment);
            spellCheck.CheckSpelling(amendmentSpellCheckList);
            MessageBox.Show("Spell check is complete.");
        }

        public void ButtonOk_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = true;
            this.Save(false);            
            this.Close();
        }

        public void ButtonCancel_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Save(bool releaseLock)
        {
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_AccessionOrder, true);
        }

        public void ButtonFinalize_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.User.SystemUser systemUser = (YellowstonePathology.Business.User.SystemUser)this.comboBoxAmendmentUsers.SelectedItem;
            if (systemUser.UserId > 0)
            {
                YellowstonePathology.Business.Test.OkToFinalizeResult okToFinalizeResult = this.m_Amendment.IsOkToFinalize(this.m_AccessionOrder);
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
                        this.m_Amendment.Finish();
                        this.Save(false);
                    }
                }
                else
                {
                    MessageBox.Show(okToFinalizeResult.Message);
                }
            }
            else
            {
                MessageBox.Show("Select a signer from the Amended By choices.", "Amendment signer not selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}