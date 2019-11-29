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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for PanelSetOrderCPTCodeBillEditDialog.xaml
    /// </summary>
    public partial class PanelSetOrderCPTCodeBillEditDialog : Window
    {
        private YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill m_PanelSetOrderCPTCodeBill;
        private string m_ClientName;
        private Dictionary<string, string> m_Modifiers;
        private Dictionary<YellowstonePathology.Business.Billing.Model.CPTCodeTypeEnum, string> m_CodeTypes;


        public PanelSetOrderCPTCodeBillEditDialog(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill, string clientName)
        {
            this.m_PanelSetOrderCPTCodeBill = panelSetOrderCPTCodeBill;
            this.m_ClientName = clientName;

            this.m_Modifiers = new Dictionary<string, string>();
            this.m_Modifiers.Add("None", null);
            this.m_Modifiers.Add(YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TwentySix, YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TwentySix);
            this.m_Modifiers.Add(YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TechnicalComponent, YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TechnicalComponent);

            this.m_CodeTypes = new Dictionary<Business.Billing.Model.CPTCodeTypeEnum, string>();
            this.m_CodeTypes.Add(Business.Billing.Model.CPTCodeTypeEnum.Global, Business.Billing.Model.CPTCodeTypeEnum.Global.ToString());
            this.m_CodeTypes.Add(Business.Billing.Model.CPTCodeTypeEnum.ProfessionalOnly, Business.Billing.Model.CPTCodeTypeEnum.ProfessionalOnly.ToString());
            this.m_CodeTypes.Add(Business.Billing.Model.CPTCodeTypeEnum.TechnicalOnly, Business.Billing.Model.CPTCodeTypeEnum.TechnicalOnly.ToString());
            this.m_CodeTypes.Add(Business.Billing.Model.CPTCodeTypeEnum.PQRS, Business.Billing.Model.CPTCodeTypeEnum.PQRS.ToString());

            DataContext = this;

            InitializeComponent();
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill PanelSetOrderCPTCodeBill
        {
            get { return this.m_PanelSetOrderCPTCodeBill; }
        }
        public string ClientName
        {
            get { return this.m_ClientName; }
        }

        public Dictionary<string, string> Modifiers
        {
            get { return this.m_Modifiers; }
        }

        public Dictionary<YellowstonePathology.Business.Billing.Model.CPTCodeTypeEnum, string> CodeTypes
        {
            get { return this.m_CodeTypes; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
