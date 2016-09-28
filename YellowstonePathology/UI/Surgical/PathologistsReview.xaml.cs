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
using System.ComponentModel;

namespace YellowstonePathology.UI.Surgical
{
	/// <summary>
	/// Interaction logic for PathologistsReview.xaml
	/// </summary>
	public partial class PathologistsReview : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private PathologistUI m_PathologistUI;
		private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;
		private YellowstonePathology.UI.Common.TreeViewWorkspace m_TreeViewWorkspace;		
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.UI.TypingShortcutUserControl m_TypingShortcutUserControl;
        private object m_ReviewContent;

        public PathologistsReview(PathologistUI pathologistUI, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PathologistUI = pathologistUI;
			this.m_SystemIdentity = systemIdentity;

			InitializeComponent();                      

			this.m_DocumentViewer = new DocumentWorkspace();
			this.TabItemDocumentWorkspace.Content = this.m_DocumentViewer;			
			this.m_TreeViewWorkspace = new YellowstonePathology.UI.Common.TreeViewWorkspace(this.m_PathologistUI.AccessionOrder, this.m_SystemIdentity);
			this.tabItemTreeView.Content = this.m_TreeViewWorkspace;

			this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.AccessionOrder, this.PanelSetOrder.ReportNo);

			this.DataContext = this;

			this.m_DocumentViewer.ClearContent();

            this.m_TypingShortcutUserControl = new TypingShortcutUserControl(this.m_SystemIdentity, this.m_PathologistUI.Writer);
            this.TabItemTypingShortCuts.Content = this.m_TypingShortcutUserControl;

			if (this.m_CaseDocumentCollection.Count != 0)
			{
				if (this.m_CaseDocumentCollection.GetFirstRequisition() != null)
				{
					this.m_DocumentViewer.ShowDocument(this.m_CaseDocumentCollection.GetFirstRequisition());
				}
			}

			if (this.PanelSetOrder != null)
			{
				this.m_PathologistUI.RunWorkspaceEnableRules();
				this.m_PathologistUI.RunPathologistEnableRules();

				this.SetReviewContent();
				if (this.PanelSetOrder.PanelSetId == 19)
				{
					if (this.PanelSetOrder.Accepted == true)
					{
						this.RightTabControl.SelectedItem = this.TabItemHistory;
					}
					else
					{
						this.RightTabControl.SelectedItem = this.TabItemReview;
					}
				}
                else if (this.PanelSetOrder.PanelSetId == 13 || this.PanelSetOrder.PanelSetId == 128)
				{
					if (this.PanelSetOrder.Accepted == true)
					{
						this.RightTabControl.SelectedItem = this.TabItemReview;
					}
					else
					{
						this.RightTabControl.SelectedItem = this.TabItemHistory;
					}
				}
				else
				{
					this.RightTabControl.SelectedIndex = this.m_PathologistUI.SelectedTabIndex;
				}
			}

			this.m_TreeViewWorkspace = new Common.TreeViewWorkspace(this.m_PathologistUI.AccessionOrder, this.m_SystemIdentity);
            this.m_TreeViewWorkspace.IsEnabled = this.m_PathologistUI.AccessionOrder.AccessionLock.IsLockAquiredByMe;
            this.tabItemTreeView.Content = this.m_TreeViewWorkspace;
            this.Unloaded += PathologistsReview_Unloaded;			
		}

        private void PathologistsReview_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.MoveKeyboardFocusNextThenBack();
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_PathologistUI.AccessionOrder; }
		}

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PathologistUI.PanelSetOrder; }
		}

		public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
		{
			get { return this.m_PathologistUI.FieldEnabler; }
			set { this.m_PathologistUI.FieldEnabler = value; }
		}

		public void SetReviewContent()
		{
			this.m_ReviewContent = null;
			object historyContent = null;
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(this.PanelSetOrder.PanelSetId);
            if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument ||
                panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.RetiredTestDocument)
			{
                this.m_ReviewContent = new PublishedDocumentReview(this.m_PathologistUI, this.m_SystemIdentity);
				historyContent = new CommonHistory(this.AccessionOrder);
			}
			else
			{
				switch (this.PanelSetOrder.PanelSetId)
				{
					case 28: //Fetal Hemoglobin
					case 29: //DNA Content and S-Phase Analysis
					case 56: //T-Cell Immunodeficiency Profile
                        this.m_ReviewContent = new PublishedDocumentReview(this.m_PathologistUI, this.m_SystemIdentity);
						historyContent = new CommonHistory(this.AccessionOrder);
						break;
					case 13:
					case 128:
                        this.m_ReviewContent = new SurgicalReview(this.m_TypingShortcutUserControl, this.m_PathologistUI);
						historyContent = new SurgicalHistory(this.m_PathologistUI);
						break;
					default:
                        this.m_ReviewContent = new ResultPathReview(this.m_PathologistUI, this.m_SystemIdentity);
						historyContent = new CommonHistory(this.AccessionOrder);
						break;
				}
			}
			this.ContentControlReview.Content = this.m_ReviewContent;
			this.ContentControlHistory.Content = historyContent;
		}

        public object ReviewContent
        {
            get { return this.m_ReviewContent; }
        }

		public string CaseStatusTextColor
		{
			get
			{
				string color = string.Empty;
				if (this.PanelSetOrder != null && this.PanelSetOrder.Final == true)
				{
					color = "Black";
				}
				else
				{
					if (this.m_PathologistUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
					{
						color = "Green";
					}
					else
					{
						color = "Red";
					}
				}
				return color;
			}
		}

		public string PatientAccessionAgeInfo
		{
			get
			{
				StringBuilder result = new StringBuilder();
				if (this.AccessionOrder.PBirthdate.HasValue)
				{
					result.Append(this.AccessionOrder.PBirthdate.Value.ToShortDateString() + "  ");
					result.Append(this.AccessionOrder.PatientAccessionAge + "  ");
				}
				if (string.IsNullOrEmpty(this.AccessionOrder.PSex) == false)
				{
					result.Append(this.AccessionOrder.PSex);
				}
				return result.ToString();
			}
		}

		private void RightTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			TabControl tabControl = (TabControl)sender;
			this.m_PathologistUI.SelectedTabIndex = tabControl.SelectedIndex;
		}

        public void TextBox_KeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Space)
            {
                TextBox textBox = (TextBox)args.Source;
                this.m_TypingShortcutUserControl.SetShortcut(textBox);
            }
        } 
	}
}
