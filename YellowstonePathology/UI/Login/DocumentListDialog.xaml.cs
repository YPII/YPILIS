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
using System.Diagnostics;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for DocumentListDialog.xaml
	/// </summary>
    public partial class DocumentListDialog : Window, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_ReportNo;

		public DocumentListDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
			
			InitializeComponent();

			DataContext = this;
            this.Loaded += new RoutedEventHandler(DocumentListDialog_Loaded);
		}

        private void DocumentListDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetSelectedReportNo(this.m_ReportNo);
        }

        private void SetSelectedReportNo(string reportNo)
        {
            int selectedIndex = 0;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.ListBoxReportOrders.Items)
            {
                if (panelSetOrder.ReportNo == reportNo)
                {
                    this.ListBoxReportOrders.SelectedIndex = selectedIndex;
                }
                selectedIndex += 1;
            }
        }

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		private void ShowDocument(YellowstonePathology.Business.Document.CaseDocument caseDocument)
		{
			switch(caseDocument.Extension.ToUpper())
			{
				case "XPS":
					if (caseDocument.CaseDocumentType == "AccessionOrderDataSheet")
					{
						this.ShowAccessionDataSheet();
					}
					else
					{
						XpsDocumentViewer viewer = new XpsDocumentViewer();
						viewer.ViewDocument(caseDocument.FullFileName);
						viewer.ShowDialog();
					}
					break;
				case "DOC":
					YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(caseDocument.FullFileName);
					break;
				case "PDF":
					YellowstonePathology.Business.Document.CaseDocument.OpenPDF(caseDocument.FullFileName);
					break;
				case "TIF":
					YellowstonePathology.Business.Document.CaseDocument.OpenTiff(caseDocument.FullFileName);
					break;
			}
		}

		private void ShowAccessionDataSheet()
		{
			XpsDocumentViewer viewer = new XpsDocumentViewer();

			YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData accessionOrderDataSheetData = YellowstonePathology.Business.Gateway.XmlGateway.GetAccessionOrderDataSheetData(this.m_AccessionOrder.MasterAccessionNo);
			YellowstonePathology.Document.Result.Xps.AccessionOrderDataSheet accessionOrderDataSheet = new Document.Result.Xps.AccessionOrderDataSheet(accessionOrderDataSheetData);
			viewer.LoadDocument(accessionOrderDataSheet.FixedDocument);
			viewer.ShowDialog();
		}

		private void ButtonViewDocument_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxDocumentList.SelectedItem != null)
			{
                YellowstonePathology.Business.Document.CaseDocument caseDocument = (YellowstonePathology.Business.Document.CaseDocument)this.ListBoxDocumentList.SelectedItem;
				this.ShowDocument(caseDocument);
			}
		}

        private void ListBoxDocumentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
            if (this.ListBoxDocumentList.SelectedItem != null)
			{
                YellowstonePathology.Business.Document.CaseDocument caseDocument = (YellowstonePathology.Business.Document.CaseDocument)this.ListBoxDocumentList.SelectedItem;
				this.ShowDocument(caseDocument);
			}
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

        private void ListBoxReportOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxReportOrders.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Test.PanelSetOrder pso = (YellowstonePathology.Business.Test.PanelSetOrder)this.ListBoxReportOrders.SelectedItem;
                this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_AccessionOrder, pso.ReportNo);                
                this.HandleContextMenu(pso);
                this.NotifyPropertyChanged("CaseDocumentCollection");
            }
        }

        private void HandleContextMenu(YellowstonePathology.Business.Test.PanelSetOrder selectedPanelSetOrder)
        {
            for (int i = 0; i < this.ContextMenuDocumentList.Items.Count; i++)
            {
                MenuItem menuItem = (MenuItem)this.ContextMenuDocumentList.Items[i];
                if (menuItem.Tag != null)
                {
                    this.ContextMenuDocumentList.Items.Remove(menuItem);
                }
            }
           
            foreach (YellowstonePathology.Business.Test.PanelSetOrder pso in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                if (pso.ReportNo != selectedPanelSetOrder.ReportNo)
                {
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = "Copy To: " + pso.ReportNo;
                    menuItem.Tag = pso;
                    menuItem.Click += new RoutedEventHandler(ListBoxDocumentListCopyTo_Click);
                    this.ContextMenuDocumentList.Items.Add(menuItem);
                }
            }
        }

        private void ListBoxDocumentListCopyTo_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            YellowstonePathology.Business.Test.PanelSetOrder copyToPso = (YellowstonePathology.Business.Test.PanelSetOrder)menuItem.Tag;
            YellowstonePathology.Business.Test.PanelSetOrder copyFromPso = (YellowstonePathology.Business.Test.PanelSetOrder)this.ListBoxReportOrders.SelectedItem;

            YellowstonePathology.Business.Document.CaseDocumentCollection copyToCaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(copyToPso.ReportNo);
                        
            foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this.ListBoxDocumentList.SelectedItems)
            {
                if (caseDocument.IsRequisition() == true)
                {
                    string copyFromFileName = System.IO.Path.GetFileName(caseDocument.FullFileName);                    
                    YellowstonePathology.Business.Document.CaseDocument newCaseDocument = copyToCaseDocumentCollection.GetNextRequisition();                    
                    copyToCaseDocumentCollection.Add(newCaseDocument);                    
                    System.IO.File.Copy(caseDocument.FullFileName, newCaseDocument.FullFileName);
                }
            }

            YellowstonePathology.Business.Test.PanelSetOrder pso = (YellowstonePathology.Business.Test.PanelSetOrder)this.ListBoxReportOrders.SelectedItem;
            this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_AccessionOrder, pso.ReportNo);                
            MessageBox.Show("The selected file(s) have been copied.");
        }
       
        private void MenuItemListBoxDocumentListDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxDocumentList.SelectedItems.Count != 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the selected file(s).", "Delete Files?", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    string resultMessage = "The selected file(s) have been deleted.";

                    for (int i = this.ListBoxDocumentList.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        if (System.IO.File.Exists(this.m_CaseDocumentCollection[i].FullFileName) == true)
                        {
                            try
                            {
                                System.IO.File.Delete(this.m_CaseDocumentCollection[i].FullFileName);
                                this.m_CaseDocumentCollection.Remove(this.m_CaseDocumentCollection[i]);
                            }
                            catch (Exception)
                            {
                                resultMessage = "There was a problem deleting: " + this.m_CaseDocumentCollection[i].FullFileName;
                                break;
                            }
                        }
                    }
                    
                    this.NotifyPropertyChanged("CaseDocumentCollection");
                    MessageBox.Show(resultMessage);
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void OnListBoxItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {            
            e.Handled = true;
        }

        private void HyperLinkOpenFolder_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ReportNo);
			string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
            p.StartInfo = info;
            p.Start();            
        }       
	}
}
