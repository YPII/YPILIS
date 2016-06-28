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
	/// Interaction logic for CodeSelectionV2.xaml
	/// </summary>
	public partial class CodeSelectionV2 : System.Windows.Window
	{		
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
		YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
		YellowstonePathology.Business.Billing.Model.TypingCptCodeList m_TypingCptCodeList;

		public CodeSelectionV2(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
			this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalSpecimen = surgicalSpecimen;
			this.m_SurgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
			this.m_TypingCptCodeList = new YellowstonePathology.Business.Billing.Model.TypingCptCodeList();

            InitializeComponent();

            this.ListViewCptCodes.ItemsSource = this.m_TypingCptCodeList;
            this.Loaded += new RoutedEventHandler(CptCodeSelection_Loaded);
        }

        private void CptCodeSelection_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListViewCptCodes.SelectedItem = null;
            this.TextBoxIcd9Code.Focus();
        }

        public void Grid_KeyUP(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Enter)
            {
                this.SetCodes();
                this.DialogResult = true;
                this.Close();
            }
            if (args.Key == Key.Escape)
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        public void Grid_KeyDown(object sender, KeyEventArgs args)
        {
            if (args.Key != Key.Enter | args.Key != Key.Escape)
            {
                this.TextBoxIcd9Code.Focus();
                this.TextBoxIcd9Code.SelectionStart = 999;
            }            
        }                

        public void ButtonOk_Click(object sender, RoutedEventArgs args)
        {
            this.SetCodes();
            this.DialogResult = true;
            this.Close();
        }

        public void ButtonCancel_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = false;
            this.Close();
        }

        public void SetCodes()
        {
			if (this.ListViewCptCodes.SelectedItems.Count > 0)
			{
				foreach (YellowstonePathology.Business.Billing.Model.TypingCptCodeListItem cptItem in this.ListViewCptCodes.SelectedItems)
				{
					if (this.CheckBoxAddToAllSpecimen.IsChecked == true)
					{
						foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen specimenCptItem in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
						{
							YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderById(specimenCptItem.SpecimenOrderId);
							this.AddCode(specimenOrder, cptItem);
						}
					}
					else
					{
						YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderById(this.m_SurgicalSpecimen.SpecimenOrderId);
						this.AddCode(specimenOrder, cptItem);
					}
				}
			}

			if (this.TextBoxIcd9Code.Text.Length > 0)
			{
				string[] spaceSplit = this.TextBoxIcd9Code.Text.Split(' ');
				foreach (string icd9Code in spaceSplit)
				{
					if (this.CheckBoxAddToAllSpecimen.IsChecked == true)
					{
						foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
						{
							this.AddICDCode(icd9Code, null, surgicalSpecimen);
						}
					}
					else
					{
						this.AddICDCode(icd9Code, null, this.m_SurgicalSpecimen);
					}
				}
			}
		}

		private void AddCode(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Billing.Model.TypingCptCodeListItem cptItem)
		{
			YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.GetNextItem(this.m_SurgicalTestOrder.ReportNo);
			panelSetOrderCPTCode.Quantity = cptItem.Quantity;
			panelSetOrderCPTCode.CPTCode = cptItem.CptCode.Code;
			panelSetOrderCPTCode.Modifier = null;
			panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.m_SurgicalTestOrder.PanelSetName;
			panelSetOrderCPTCode.CodeableType = "Surgical Diagnosis";
			panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
			panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            panelSetOrderCPTCode.CodeType = cptItem.CptCode.CodeType.ToString();
			panelSetOrderCPTCode.ClientId = this.m_AccessionOrder.ClientId;
			this.m_SurgicalTestOrder.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
		}

		public void AddICDCode(string icd9Code, string icd10Code, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
		{
			YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.m_AccessionOrder.ICD9BillingCodeCollection.GetNextItem(this.m_SurgicalTestOrder.ReportNo,
							this.m_AccessionOrder.MasterAccessionNo, surgicalSpecimen.SpecimenOrderId, icd9Code, icd10Code, 1);
			icd9BillingCode.SurgicalSpecimenId = surgicalSpecimen.SurgicalSpecimenId;
			this.m_AccessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
		}
	}
}
