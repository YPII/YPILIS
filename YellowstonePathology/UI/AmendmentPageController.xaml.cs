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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AmendmentPageController.xaml
    /// </summary>
    public partial class AmendmentPageController : Window
    {
        AmendmentUI m_AmendmentUI;        

        public AmendmentPageController(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {

            this.m_AmendmentUI = new AmendmentUI(accessionOrder, panelSetOrder);
            InitializeComponent();

            this.DataContext = this;
            Closing += new System.ComponentModel.CancelEventHandler(AmendmentPageController_Closing);
            this.Title = "Amendments for " + panelSetOrder.ReportNo + "   " + accessionOrder.PatientName;

            AmendmentListPage amendmentListPage = new AmendmentListPage(this.m_AmendmentUI);
            this.NavigationFrame.NavigationService.Navigate(amendmentListPage);
        }

        private void AmendmentPageController_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.NavigationFrame.Content.GetType() == typeof(AmendmentEditPage))
            {
                AmendmentEditPage amendmentEditPage = (AmendmentEditPage)this.NavigationFrame.Content;
                amendmentEditPage.TextBoxAmendment.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            //this.m_AmendmentUI.Save(true);            
        }
    }
}
