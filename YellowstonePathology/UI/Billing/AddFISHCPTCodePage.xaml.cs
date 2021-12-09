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

namespace YellowstonePathology.UI.Billing
{    
    public partial class AddFISHCPTCodePage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Billing.Model.FISHCPTCodeList m_FISHCPTCodeList;
        private string m_PageHeaderText;
        private int m_ProbeSetCount;

        public AddFISHCPTCodePage(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;        
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            Business.PanelSet.Model.PanelSetCollection allTests = Business.PanelSet.Model.PanelSetCollection.GetAll();
            Business.PanelSet.Model.PanelSet panelSet = allTests.GetPanelSet(this.m_PanelSetOrder.PanelSetId);
            if(panelSet is Business.PanelSet.Model.FISHTest)
            {
                Business.PanelSet.Model.FISHTest fishTest = (Business.PanelSet.Model.FISHTest)panelSet;
                this.m_ProbeSetCount = fishTest.ProbeSetCount;
                this.m_FISHCPTCodeList = new Business.Billing.Model.FISHCPTCodeList(this.m_ProbeSetCount);
            }
            else
            {
                this.m_ProbeSetCount = 0;
                MessageBox.Show("This FISH test does not have the Probe Set Count defined.");
            }
            
            this.m_PageHeaderText = "CPT Codes For " + this.m_PanelSetOrder.ReportNo + ": " + this.m_AccessionOrder.PatientDisplayName + " - " + this.m_AccessionOrder.PBirthdate.Value.ToShortDateString();

            DataContext = this;

            InitializeComponent();
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

        public YellowstonePathology.Business.Billing.Model.FISHCPTCodeList FISHCPTCodeList
        {
            get { return this.m_FISHCPTCodeList; }
        }
        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        private void HyperLinkAddCodes_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Billing.Model.TypingCptCodeListItem item = (YellowstonePathology.Business.Billing.Model.TypingCptCodeListItem)hyperlink.Tag;
            if (item.Quantity > 0)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_PanelSetOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = item.Quantity;
                panelSetOrderCPTCode.CPTCode = item.CptCode.Code;
                panelSetOrderCPTCode.Modifier = item.CptCode.Modifier == null ? null : item.CptCode.Modifier;
                panelSetOrderCPTCode.CodeType = item.CptCode.CodeType.ToString();
                panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.m_PanelSetOrder.PanelSetName;
                panelSetOrderCPTCode.CodeableType = "Billable Test";
                panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
                panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                panelSetOrderCPTCode.MedicalRecord = this.m_AccessionOrder.SvhMedicalRecord;
                this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }
            else
            {
                MessageBox.Show("Unable to add CPT Code " + item.CptCode.Code + " as the quantity is 0.");
            }
        }

        private void MenuItemDeletePanelSetOrderCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSetOrderCPTCode.SelectedItems.Count != 0)
            {
                for (int i = this.ListViewPanelSetOrderCPTCode.SelectedItems.Count - 1; i >= 0; i--)
                {
                    YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = (YellowstonePathology.Business.Test.PanelSetOrderCPTCode)this.ListViewPanelSetOrderCPTCode.SelectedItems[i];
                    this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection.Remove(panelSetOrderCPTCode);
                }
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }
    }
}
