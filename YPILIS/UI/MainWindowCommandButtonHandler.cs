using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class MainWindowCommandButtonHandler
    {
        private readonly object m_EventLockObject = new object();

        public delegate void ShowCaseDocumentEventHandler(object sender, EventArgs e);
        public event ShowCaseDocumentEventHandler ShowCaseDocument;

        public delegate void ShowOrderFormEventHandler(object sender, EventArgs e);
        public event ShowOrderFormEventHandler ShowOrderForm;

        public delegate void AssignCaseEventHandler(object sender, EventArgs e);
        public event AssignCaseEventHandler AssignCase;

        public delegate void StartProviderDistributionPathEventHandler(object sender, EventArgs e);
        public event StartProviderDistributionPathEventHandler StartProviderDistributionPath;

        public delegate void ShowAmendmentDialogEventHandler(object sender, EventArgs e);
        public event ShowAmendmentDialogEventHandler ShowAmendmentDialog;

        public delegate void SaveEventHandler(object sender, EventArgs e);
        public event SaveEventHandler Save;

        public delegate void RemoveTabEventHandler(object sender, EventArgs e);
        public event RemoveTabEventHandler RemoveTab;

        public delegate void ShowMessagingDialogEventHandler(object sender, EventArgs e);
        public event ShowMessagingDialogEventHandler ShowMessagingDialog;

        public MainWindowCommandButtonHandler()
        {
            
        }

        public void OnSave()
        {
            if (this.Save != null) this.Save(this, EventArgs.Empty);
        }

        public void OnShowCaseDocument()
		{
            if (this.ShowCaseDocument != null) this.ShowCaseDocument(this, EventArgs.Empty);
        }

        public void OnRemoveTab()
        {
            if (this.RemoveTab != null) this.RemoveTab(this, EventArgs.Empty);
        }

        public void OnShowMessagingDialog()
        {
            if (this.ShowMessagingDialog != null) this.ShowMessagingDialog(this, EventArgs.Empty);
        }

        public void OnShowOrderForm()
		{
			if (this.ShowOrderForm != null) this.ShowOrderForm(this, EventArgs.Empty);
		}		

		public void OnAssignCase()
		{
			if (this.AssignCase != null) this.AssignCase(this, EventArgs.Empty);
		}		

        public void OnStartProviderDistributionPath()
        {
            if (this.StartProviderDistributionPath != null) this.StartProviderDistributionPath(this, EventArgs.Empty);
        }

        public void OnShowAmendmentDialog()
        {
            if (this.ShowAmendmentDialog != null) this.ShowAmendmentDialog(this, EventArgs.Empty);
        }
    }
}
