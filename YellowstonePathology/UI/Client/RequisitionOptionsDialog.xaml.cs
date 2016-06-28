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

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for RequisitionOptionsDialog.xaml
	/// </summary>
	public partial class RequisitionOptionsDialog : Window
	{
		private int m_ClientId;
		private int m_Copies;
		private string m_ClientName;

		private YellowstonePathology.Business.User.UserPreference m_UserPreference;

		public RequisitionOptionsDialog(int clientId, string clientName)
		{
			this.m_ClientId = clientId;
			this.m_ClientName = clientName;
			this.m_Copies = 25;

			this.m_UserPreference = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference;

			InitializeComponent();

			DataContext = this;
		}

        public System.Drawing.Printing.PrinterSettings.StringCollection InstalledPrinters
        {
            get { return System.Drawing.Printing.PrinterSettings.InstalledPrinters; }
        }

		public YellowstonePathology.Business.User.UserPreference UserPreference
        {
            get { return this.m_UserPreference; }
        }

		public string ClientName
		{
			get { return this.m_ClientName; }
			set { this.m_ClientName = value; }
		}

		public int Copies
		{
			get { return this.m_Copies; }
			set { this.m_Copies = value; }
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ButtonPrint_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_Copies > 0)
			{
                if (this.ComboBoxForm.SelectedItem != null)
                {
                    if (this.ComboBoxPrinter.SelectedItem != null)
                    {
                        System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
						System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.RequisitionPrinter);

                        ComboBoxItem comboBoxItem = (ComboBoxItem)this.ComboBoxForm.SelectedItem;
                        switch (comboBoxItem.Content.ToString())
                        {
                            case "Standard":
                                StandardRequisition standardRequisition = new StandardRequisition(this.m_ClientId);
                                standardRequisition.Print(this.m_Copies, printQueue);
                                break;
                            case "Oncology":
                                OncologyRequisition oncologyRequisition = new OncologyRequisition(this.m_ClientId);
                                oncologyRequisition.Print(this.m_Copies, printQueue);
                                break;
                            case "IHC/Molecular":
                                IHCMolecularRequisition ihcMolecularRequisition = new IHCMolecularRequisition(this.m_ClientId);
                                ihcMolecularRequisition.Print(this.m_Copies, printQueue);
                                break;
                        }
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("You must select a printer.");
                    }
                }
                else
                {
                    MessageBox.Show("You must select a form type.");
                }
			}			
		}
	}
}
