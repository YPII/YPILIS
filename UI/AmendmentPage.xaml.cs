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
    /// Interaction logic for AmendmentPage.xaml
    /// </summary>
    public partial class AmendmentPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

        private YellowstonePathology.Business.User.SystemUserCollection m_AmendmentSigners;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.Amendment.Model.Amendment m_Amendment;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_PageHeaderText;

        public AmendmentPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.Business.Amendment.Model.Amendment amendment,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;
            this.m_Amendment = amendment;
            this.m_SystemIdentity = systemIdentity;
            this.AmendmentSigners = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.AmendmentSigner, true);
            this.m_PageHeaderText = "Amendment For: " + this.m_AccessionOrder.PatientDisplayName + " (" + this.m_Amendment.ReportNo + ")";

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

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save()
        {
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
        }

        public void UpdateBindingSources()
        {

        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public YellowstonePathology.Business.Amendment.Model.Amendment Amendment
        {
            get { return this.m_Amendment; }
            set
            {
                this.m_Amendment = value;
                NotifyPropertyChanged("Amendment");
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

        private void CheckSpelling()
        {
            YellowstonePathology.Business.Common.SpellChecker spellCheck = new YellowstonePathology.Business.Common.SpellChecker();
            YellowstonePathology.Business.Common.AmendmentSpellCheckList amendmentSpellCheckList = new YellowstonePathology.Business.Common.AmendmentSpellCheckList(this.m_Amendment);
            spellCheck.CheckSpelling(amendmentSpellCheckList);
            MessageBox.Show("Spell check is complete.");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Finish(this, new EventArgs());
        }

        private void HyperLinkResetAmendmentText_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_Amendment.IsOkToResetText(this.m_AccessionOrder);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2AmplificationByISHTestOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_Amendment.ReferenceReportNo);
                this.m_Amendment.Text = YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSystemGeneratedAmendmentText.AmendmentText(her2AmplificationByISHTestOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkAccept_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_Amendment.IsOkToAccept();
            if (methodResult.Success == true)
            {
                this.m_Amendment.Accept(this.m_SystemIdentity.User);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
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
                    this.m_Amendment.Finalize(this.m_SystemIdentity.User);
                }
            }
            else
            {
                MessageBox.Show(okToFinalizeResult.Message);
            }
        }

        private void HyperLinkUnaccept_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_Amendment.IsOkToUnaccept();
            if(methodResult.Success == true)
            {
                this.m_Amendment.Unaccept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkUnfinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_Amendment.ReportNo);
            YellowstonePathology.Business.Test.OkToUnfinalizeResult okToUnfinalizeResult = this.m_Amendment.IsOkToUnfinalize(panelSetOrder);
            if (okToUnfinalizeResult.OK == true)
            {
                this.m_Amendment.Unfinalize();
            }
            else
            {
                MessageBox.Show(okToUnfinalizeResult.Message);
            }
        }
    }
}
