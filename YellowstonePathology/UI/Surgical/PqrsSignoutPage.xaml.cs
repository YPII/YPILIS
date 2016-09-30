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
    /// Interaction logic for PQRSSignoutPage.xaml
    /// </summary>
    public partial class PQRSSignoutPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        private YellowstonePathology.Business.Surgical.PQRSMeasure m_PQRSMeasure;
        private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private System.Windows.Visibility m_BackButtonVisibility;
        private System.Windows.Visibility m_NextButtonVisibility;

        public PQRSSignoutPage(YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            System.Windows.Visibility backButtonVisibility,
            System.Windows.Visibility nextButtonVisibility)
        {
            this.m_PQRSMeasure = pqrsMeasure;
            this.m_SurgicalSpecimen = surgicalSpecimen;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_BackButtonVisibility = backButtonVisibility;
            this.m_NextButtonVisibility = nextButtonVisibility;

            this.m_SurgicalTestOrder.PQRSIsIndicated = true;

            InitializeComponent();
            this.DataContext = this;
            this.Loaded += PQRSSignoutPage_Loaded;
            Unloaded += PQRSSignoutPage_Unloaded;
        }

        private void PQRSSignoutPage_Loaded(object sender, RoutedEventArgs e)
        {
             
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection = this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(this.m_SurgicalSpecimen.SpecimenOrderId);
            for(int idx = 0; idx < this.m_PQRSMeasure.PQRSCodeCollection.Count; idx++)
            {
                if (panelSetOrderCPTCodeCollection.Exists(this.m_PQRSMeasure.PQRSCodeCollection[idx].Code, 1) == true)
                {
                    this.RadioButtonList.SelectedIndex = idx;
                    break;
                }
            }
        }

        private void PQRSSignoutPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if(this.CanSave() == true)
            {
                this.Next(this, new EventArgs());
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                this.Close(this, new EventArgs());
            }
        }

        private bool CanSave()
        {
            bool result = false;
            if (this.CheckBoxNotApplicable.IsChecked.HasValue && this.CheckBoxNotApplicable.IsChecked.Value == true)
            {
                result = true;
            }
            else if (this.RadioButtonList.SelectedItem != null)
            {
                result = true;
                YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode = (YellowstonePathology.Business.Billing.Model.PQRSCode)this.RadioButtonList.SelectedItem;
                this.AddPQRSCode(pqrsCode);
            }
            else
            {
                MessageBox.Show("Please select an option from the list.");
            }

            return result;
        }        

        public YellowstonePathology.Business.Surgical.PQRSMeasure PQRSMeasure
        {
            get { return this.m_PQRSMeasure; }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder SurgicalTestOrder
        {
            get { return this.m_SurgicalTestOrder; }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
        {
            get { return this.m_SurgicalSpecimen; }
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

        private void CheckBoxNotApplicable_Checked(object sender, RoutedEventArgs e)
        {
            this.RadioButtonList.SelectedIndex = -1;
        }

        private void RadioButtonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.RadioButtonList.SelectedIndex != -1) this.CheckBoxNotApplicable.IsChecked = false;
        }

        private void AddPQRSCode(YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode)
        {
            if (this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Exists(pqrsCode.Code, 1) == false)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_SurgicalTestOrder.ReportNo);
                panelSetOrderCPTCode.Quantity = 1;
                panelSetOrderCPTCode.CPTCode = pqrsCode.Code;
                panelSetOrderCPTCode.Modifier = pqrsCode.Modifier;
                panelSetOrderCPTCode.CodeableDescription = "PQRS Code";
                panelSetOrderCPTCode.CodeableType = "PQRS";
                panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
                panelSetOrderCPTCode.SpecimenOrderId = this.m_SurgicalSpecimen.SpecimenOrderId;
                panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
                this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            }
        }
    }
}
