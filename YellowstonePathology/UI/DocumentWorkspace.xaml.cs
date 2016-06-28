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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.UI
{  
    public partial class DocumentWorkspace : System.Windows.Controls.UserControl
    {        
		YellowstonePathology.Business.Document.CaseDocument m_CaseDocument;
        Nullable<bool> m_SyncDocuments = true;

        public DocumentWorkspace()
        {                        
            InitializeComponent();                        
        }

		public void ClearContent()
        {
            this.ContentControlDocument.Content = null;            
        }

        public void ContextMenuItemPrint_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_CaseDocument != null)
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo(this.m_CaseDocument.FullFileName);
                p.StartInfo = info;
                p.StartInfo.Verb = "print";
                p.Start();                
            }
        }

        public void ContextMenuItemEdit_Click(object sender, RoutedEventArgs args)
        {
			if (this.m_CaseDocument != null)
            {
                Process p = new Process();
				ProcessStartInfo info = new ProcessStartInfo(m_CaseDocument.FullFileName);
                p.StartInfo = info;                
                p.Start();                
            }
        }       

        public Nullable<bool> SyncDocuments
        {
            get { return this.m_SyncDocuments; }
            set { this.m_SyncDocuments = value; }
        }

        public void ShowDocument(YellowstonePathology.Business.Document.CaseDocument caseDocument)
        {
            if (this.m_CaseDocument != null) this.m_CaseDocument.Close();
            this.m_CaseDocument = caseDocument;
			this.ClearContent();

			if (this.m_CaseDocument != null)
			{
				caseDocument.Show(this.ContentControlDocument, Window.GetWindow(this));
			}
       }		
    }
}