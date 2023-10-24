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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for UnreachableResultPage.xaml
    /// </summary>
    public partial class UnreachableResultPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        private string m_PageHeaderText;
        private string m_Message;

        public UnreachableResultPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_PageHeaderText = "Result for: " + accessionOrder.PatientDisplayName;
            this.m_Message = "Results for " + panelSetOrder.PanelSetName + " are not available here.";

            InitializeComponent();

            DataContext = this;
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public string Message
        {
            get { return this.m_Message; }
        }
        

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }
    }
}
