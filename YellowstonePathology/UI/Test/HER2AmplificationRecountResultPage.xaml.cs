using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for HER2AmplificationRecountResultPage.xaml
    /// </summary>
    public partial class HER2AmplificationRecountResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void OrderTestEventHandler(object sender, CustomEventArgs.PanelSetReturnEventArgs e);
        public event OrderTestEventHandler OrderTest;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder m_PanelSetOrder;
        private string m_OrderedOnDescription;

        public HER2AmplificationRecountResultPage(YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrder, accessionOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = "Her2 Amplification Recount Result For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;

            InitializeComponent();

            DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnfinalize();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Accept();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }


        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Unaccept();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkOrderHER2Summary_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest test = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == false)
            {
                CustomEventArgs.PanelSetReturnEventArgs args = new CustomEventArgs.PanelSetReturnEventArgs(test);
                this.OrderTest(this, args);
            }
            else
            {
                MessageBox.Show("Unable to order a " + test.PanelSetName + " as one already exists");
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }
    }
}
