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
    /// Interaction logic for SuggestedAdditionalTestingPge.xaml
    /// </summary>
    public partial class SuggestedAdditionalTestingPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        public delegate void OrderTestEventHandler(object sender, CustomEventArgs.PanelSetReturnEventArgs e);
        public event OrderTestEventHandler OrderTest;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private List<string> m_Messages;
        private System.Windows.Visibility m_BackButtonVisibility;
        private System.Windows.Visibility m_NextButtonVisibility;

        public SuggestedAdditionalTestingPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            List<string> messages,
            System.Windows.Visibility backButtonVisibility,
            System.Windows.Visibility nextButtonVisibility)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_Messages = messages;
            this.m_BackButtonVisibility = backButtonVisibility;
            this.m_NextButtonVisibility = nextButtonVisibility;

            InitializeComponent();
            DataContext = this;
        }        

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public List<string> Messages
        {
            get { return this.m_Messages; }
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public System.Windows.Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public System.Windows.Visibility CloseButtonVisibility
        {
            get
            {
                if (this.m_NextButtonVisibility == System.Windows.Visibility.Hidden)
                {
                    return System.Windows.Visibility.Visible;
                }
                return System.Windows.Visibility.Hidden;
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        private void HyperLinkLynchSyndrome_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(lynchSyndromeEvaluationTest));
            }
        }

        private void HyperLinkComprehensiveColonCancerProfile_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest comprehensiveColonCancerProfileTest = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(comprehensiveColonCancerProfileTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(comprehensiveColonCancerProfileTest));
            }
        }

        private void HyperLinkBRAFV600E_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new Business.Test.BRAFV600EK.BRAFV600EKTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(brafV600EKTest));
            }
        }

        private void HyperLinkHPV1618_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test hpv1618Test = new Business.Test.HPV1618.HPV1618Test();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(hpv1618Test.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(hpv1618Test));
            }
        }

        private void HyperLinkRASRAFPanel_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new Business.Test.RASRAFPanel.RASRAFPanelTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(rasRAFPanelTest));
            }
        }

        private void HyperLinkPNH_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PNH.PNHTest pnhTest = new Business.Test.PNH.PNHTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(pnhTest.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(pnhTest));
            }
        }

        private void HyperLinkPDL1_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PDL1.PDL1Test pdl1Test = new Business.Test.PDL1.PDL1Test();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(pdl1Test.PanelSetId) == false)
            {
                this.OrderTest(this, new CustomEventArgs.PanelSetReturnEventArgs(pdl1Test));
            }
        }
    }
}
