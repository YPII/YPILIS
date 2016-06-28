using System;
using System.Collections;
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
using System.Threading;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using Microsoft.Win32;

namespace YellowstonePathology.UI
{    
    public partial class MainWindow : System.Windows.Window
    {
		//System.Timers.Timer m_Timer;
		System.Media.SoundPlayer m_WavPlayer;        

        TabItem m_TabItemFlow;                
        TabItem m_TabItemPathologist;      
        TabItem m_TabItemReportDistribution;        
        TabItem m_TabItemSearch;        
        TabItem m_TabItemLab;        
        TabItem m_TabItemAdministration;
        TabItem m_TabItemScanning;
        TabItem m_TabItemClient;
        TabItem m_TabItemBilling;
        TabItem m_TabItemCytology;        
		TabItem m_TabItemLogin;
        TabItem m_TabItemClientOrder;
        TabItem m_TabItemTask;

        TabItem m_TabItemTyping;		
        Surgical.TypingWorkspace m_TypingWorkspace;
        Surgical.PathologistWorkspace m_PathologistWorkspace;

        Flow.FlowWorkspace m_FlowWorkspace;                                
        ReportDistribution.ReportDistributionWorkspace m_ReportDistributionWorkspace; 

        Cytology.CytologyWorkspace m_CytologyWorkspace;

		Login.LoginWorkspace m_LoginWorkspace;
        ClientOrderWorkspace m_ClientOrderWorkspace;
        TaskWorkspace m_TaskWorkspace;

        SearchWorkspace m_SearchWorkspace;                
        Test.LabWorkspace m_LabWorkspace;        
        AdministrationWorkspace m_AdministrationWorkspace;                		

		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;

        public MainWindow()
        {
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;

            //BindingErrorListener.Listen(m => MessageBox.Show(m));            
            this.m_MainWindowCommandButtonHandler = new MainWindowCommandButtonHandler();

			this.m_WavPlayer = new System.Media.SoundPlayer();

            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.m_TabItemFlow = new TabItem();
            this.m_TabItemFlow.Header = SetHeader("Flow", "Flow.ico");
			this.m_TabItemFlow.Tag = "Flow";            

            this.m_TabItemPathologist = new TabItem();
            this.m_TabItemPathologist.Header = SetHeader("Pathologist", "Microscope.ico");
			this.m_TabItemPathologist.Tag = "Pathologist";

            this.m_TabItemReportDistribution = new TabItem();
			this.m_TabItemReportDistribution.Header = SetHeader("Report Distribution", "Distribution.ico");
			this.m_TabItemReportDistribution.Tag = "Report_Distribution";

			this.m_TabItemSearch = new TabItem();
            this.m_TabItemSearch.Header = SetHeader("Search", "Search.ico");
			this.m_TabItemSearch.Tag = "Search";            

            this.m_TabItemLab = new TabItem();
            this.m_TabItemLab.Header = SetHeader("Lab", "Lab.ico");
			this.m_TabItemLab.Tag = "Lab";
            this.m_TabItemLab.Name = "TabItemLab";
           
            this.m_TabItemAdministration = new TabItem();
            this.m_TabItemAdministration.Header = SetHeader("Administration", "Wand.ico");
			this.m_TabItemAdministration.Tag = "Administration";

            this.m_TabItemScanning = new TabItem();
            this.m_TabItemScanning.Header = SetHeader("Scan Processing", "Scan.ico");
			this.m_TabItemScanning.Tag = "Scan_Processing";

            this.m_TabItemClient = new TabItem();
            this.m_TabItemClient.Header = SetHeader("Client", "Client.ico");
			this.m_TabItemClient.Tag = "Client";

            this.m_TabItemBilling = new TabItem();
            this.m_TabItemBilling.Header = SetHeader("Billing", "Billing.ico");
			this.m_TabItemBilling.Tag = "Billing";            

            this.m_TabItemCytology = new TabItem();
			this.m_TabItemCytology.Header = SetHeader("Cytology", "Cytology.ico");
			this.m_TabItemCytology.Tag = "Cytology";
			
			this.m_TabItemTyping = new TabItem();
			this.m_TabItemTyping.Header = SetHeader("Typing", "Typing.ico");
			this.m_TabItemTyping.Tag = "Typing";

			this.m_TabItemLogin = new TabItem();
			this.m_TabItemLogin.Header = SetHeader("Login", "Login.ico");
			this.m_TabItemLogin.Tag = "Login";

            this.m_TabItemClientOrder = new TabItem();
            this.m_TabItemClientOrder.Header = SetHeader("Client Order", "Batch.ico");
            this.m_TabItemClientOrder.Tag = "Client_Order";

            this.m_TabItemTask = new TabItem();
            this.m_TabItemTask.Header = SetHeader("Tasks", "AcceptResults.ico");
            this.m_TabItemTask.Tag = "Tasks";

            InitializeComponent();
            
            this.AddHandler(UI.CustomControls.CloseableTabItem.CloseTabEvent, new RoutedEventHandler(this.CloseTab));            

			this.TabControlLeftWorkspace.SelectionChanged += new SelectionChangedEventHandler(TabControlLeftWorkspace_SelectionChanged);
			if (this.m_SystemIdentity.User.UserId != 5001 && this.m_SystemIdentity.User.UserId != 5051 && this.m_SystemIdentity.User.UserId != 5126)
			{                
                this.MenuItemReportDistribution.IsEnabled = false;
            }

			if (YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ActivateNotificationAlert == true)
            {
                TaskNotifier.Instance.Start();
            }
            
            this.DataContext = this;

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            this.Closing +=new System.ComponentModel.CancelEventHandler(MainWindow_Closing);            
        }        

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:              
                    while(this.TabControlLeftWorkspace.Items.Count > 0)
                    {
                        this.TabControlLeftWorkspace.Items.RemoveAt(0);                                                
                    }
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Flush();
                    break;
            }
        }        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {                        
            this.ShowStartupPage();
        }        

        public static void MoveKeyboardFocusNextThenBack()
        {
            IInputElement inputElement = Keyboard.FocusedElement;

            TraversalRequest traversalRequestNext = new TraversalRequest(FocusNavigationDirection.Next);
            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;

            if (keyboardFocus != null)
            {
                keyboardFocus.MoveFocus(traversalRequestNext);
                inputElement.Focus();
            }
        }

       public static void UpdateFocusedBindingSource(DependencyObject element)
        {
            object focusObj = FocusManager.GetFocusedElement(element);
            if (focusObj != null && focusObj is TextBox)
            {
                var binding = (focusObj as TextBox).GetBindingExpression(TextBox.TextProperty);
                if(binding != null)
                {
                    binding.UpdateSource();
                }                
            }
        }

        private void ShowStartupPage()
		{
			switch (YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.StartupPage)
            {
                case "Main Window":
                    break;
                case "Pathologist Workspace":
                    this.AddPathologistWorkspace();
                    break;
                case "Lab Workspace":
                    this.AddLabWorkspace();
                    break;
                case "Login Workspace":
                    this.ShowTaskWorkspace();
                    this.ShowClientOrderWorkspace();
                    this.ShowLoginWorkspace();
                    break;
                case "Flow Workspace":
                    this.AddFlowWorkspace();
                    break;
                case "Cutting Workspace":
                    break;
                case "Gross Workspace":
                    YellowstonePathology.UI.Gross.HistologyGrossPath histologyGrossPath = new Gross.HistologyGrossPath();
                    histologyGrossPath.Start();
                    break;
                case "Monitor Workspace":
                    YellowstonePathology.UI.Monitor.MonitorPath monitorPath = new Monitor.MonitorPath();
                    monitorPath.LoadAllPages();
                    monitorPath.Start();
                    break;                
                case "Cytology Workspace":
                    this.AddCytologyWorkspace();
                    break;
                case "Typing Workspace":                    
                    this.AddTypingWorkspace();                    
                    break;
            }               
		}

        private PageNavigationWindow ShowSecondMonitorWindowForTyping()
        {
            PageNavigationWindow pageNavigationWindow = null;

            if (System.Windows.Forms.Screen.AllScreens.Length == 2)
            {
                pageNavigationWindow = new PageNavigationWindow(this.m_SystemIdentity);

                System.Windows.Forms.Screen screen2 = System.Windows.Forms.Screen.AllScreens[1];
                System.Drawing.Rectangle screen2Rectangle = screen2.WorkingArea;

                pageNavigationWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                pageNavigationWindow.Width = 1500;
                pageNavigationWindow.Height = 800;
                pageNavigationWindow.Left = screen2Rectangle.Left + (screen2Rectangle.Width - pageNavigationWindow.Width) / 2;
                pageNavigationWindow.Top = screen2Rectangle.Top + (screen2Rectangle.Height - pageNavigationWindow.Height) / 2;
                pageNavigationWindow.Show();
            }

            return pageNavigationWindow;
        }

		private void SetupGross()
		{
			this.MenuItemAdmin.Visibility = Visibility.Collapsed;
			this.MenuItemBilling.Visibility = Visibility.Collapsed;
			this.MenuItemClient.Visibility = Visibility.Collapsed;
			this.MenuItemCytology.Visibility = Visibility.Collapsed;
			this.MenuItemDistribution.Visibility = Visibility.Collapsed;
			this.MenuItemFlow.Visibility = Visibility.Collapsed;
			this.MenuItemLab.Visibility = Visibility.Collapsed;
			this.MenuItemScanProcessing.Visibility = Visibility.Collapsed;
			this.MenuItemSurgical.Visibility = Visibility.Collapsed;

			this.ToolBarTrayMain.Visibility = Visibility.Collapsed;			
		}        

        public void MoveKeyboardInputToNext()
        {
            UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
            if (elementWithFocus != null)
            {
				if (elementWithFocus.GetType().Name == "TextBox" || elementWithFocus.GetType().Name == "ValidTextBox")
				{
					BindingExpression bindingExpression = ((TextBox)elementWithFocus).GetBindingExpression(TextBox.TextProperty);
                    if (bindingExpression != null)
                    {
                        bindingExpression.UpdateSource();
                    }
				}				
			}
        }        
        
        public YellowstonePathology.UI.Test.LabWorkspace LabWorkspace
        {
            get { return this.m_LabWorkspace; }
        }

		public YellowstonePathology.UI.Surgical.PathologistWorkspace PathologistWorkspace
		{
			get { return this.m_PathologistWorkspace; }
		}

		public YellowstonePathology.UI.Cytology.CytologyWorkspace CytologyWorkspace
		{
			get { return this.m_CytologyWorkspace; }
		}                

		private void TabControlLeftWorkspace_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0 && e.AddedItems[0] != null)
			{
                if(e.AddedItems[0].GetType().ToString() == "System.Windows.Controls.TabItem")
                {
				    if (((TabItem)e.AddedItems[0]).Tag == null)
				    {
					    return;
				    }
				    StackPanel s = (StackPanel)((TabItem)e.AddedItems[0]).Header;
				    s.Children[2].Visibility = Visibility.Visible;
				    if (e.RemovedItems.Count > 0 && ((TabItem)e.RemovedItems[0]).Tag != null)
				    {
					    StackPanel sr = (StackPanel)((TabItem)e.RemovedItems[0]).Header;
					    sr.Children[2].Visibility = Visibility.Hidden;
				    }
                }
			}
		}

		private StackPanel SetHeader(string title, string filename)
		{
			StackPanel stackPanel = new StackPanel();
			Uri uri = new Uri(@"pack://application:,,,/Resources/" + filename , UriKind.Absolute);
			Image image = new Image();
			image.Source = ((BitmapDecoder)(IconBitmapDecoder.Create(uri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default))).Frames[0];
			image.Margin = new Thickness(0, 0, 0, 2);
			TextBlock textBlock = new TextBlock();
			textBlock.Text = title;
			textBlock.Margin = new Thickness(2, 0, 0, 2);
			uri = new Uri(@"pack://application:,,,/Resources/Close.ico", UriKind.RelativeOrAbsolute);
			Image close = new Image();
			close.Source = ((BitmapDecoder)(IconBitmapDecoder.Create(uri, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default))).Frames[0];
			Button closeButton = new Button();
			closeButton.Content = close;
			closeButton.Margin = new Thickness(10, 0, 0, 0);
			title = title.Replace(' ', '_');
			closeButton.Name = title;
			closeButton.Click += new RoutedEventHandler(closeButton_Click);
			closeButton.Height = 12;
			closeButton.Width = 12;
			closeButton.ToolTip = "Close Tab";
			stackPanel.Orientation = Orientation.Horizontal;
			stackPanel.Children.Add(image);
			stackPanel.Children.Add(textBlock);            
		    stackPanel.Children.Add(closeButton);			

			return stackPanel;
		}

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			for (int idx = 0; idx < TabControlLeftWorkspace.Items.Count; idx++ )
			{
				if (((TabItem)TabControlLeftWorkspace.Items[idx]).Tag != null)
				{
					if (((TabItem)TabControlLeftWorkspace.Items[idx]).Tag.ToString() == ((Button)sender).Name)
					{
						((TabItem)TabControlLeftWorkspace.Items[idx]).Focus();
						this.m_MainWindowCommandButtonHandler.OnRemoveTab();
						TabControlLeftWorkspace.Items.RemoveAt(idx);
						break;
					}
				}
			}
		}

        public void CloseTab(object sender, RoutedEventArgs args)
        {
			this.m_MainWindowCommandButtonHandler.OnRemoveTab();            
            UI.CustomControls.CloseableTabItem tabItem = (UI.CustomControls.CloseableTabItem)args.OriginalSource;
            tabItem.Focus();
            if (tabItem != null)
            {
                TabControl tabControl = tabItem.Parent as TabControl;
                if (tabControl != null)
                {
                    tabControl.Items.Remove(tabItem);                    
                }
            }      
        }

        public void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs args)
        {
			App.Current.Shutdown();
        }
                
        public void AddAdministrationWorkspace()
        {            
            if (this.m_TabItemAdministration.Parent != null)
            {
                this.m_TabItemAdministration.Focus();
            }
            else
            {
                this.m_AdministrationWorkspace = UI.AdministrationWorkspace.Instance;                
                this.m_TabItemAdministration.Content = this.m_AdministrationWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemAdministration);
                this.m_TabItemAdministration.Focus();
            }         
        }                

        public void AddCytologyWorkspace()
        {            
            if (this.m_TabItemCytology.Parent == null)
            {
                this.m_CytologyWorkspace = new Cytology.CytologyWorkspace(this.m_MainWindowCommandButtonHandler, this.m_TabItemCytology);
                this.m_TabItemCytology.Content = this.m_CytologyWorkspace;
                
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemCytology);

                this.m_TabItemCytology.Focus();
            }         
            this.m_TabItemCytology.Focus();
       }                             

        public void AddPathologistWorkspace()
        {            
            if (this.m_TabItemPathologist.Parent != null)
            {
                this.m_TabItemPathologist.Focus();
            }
            else
            {
				this.m_PathologistWorkspace = new YellowstonePathology.UI.Surgical.PathologistWorkspace(this.m_MainWindowCommandButtonHandler, this.m_TabItemPathologist);
				this.m_TabItemPathologist.Content = this.m_PathologistWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemPathologist);
                this.m_TabItemPathologist.Focus();                                
			}            
        }

        public void AddTypingWorkspace()
        {
            PageNavigationWindow secondMonitorWindow = this.ShowSecondMonitorWindowForTyping();

            if (m_TabItemTyping.Parent != null)
            {
                m_TabItemTyping.Focus();
            }
            else
            {
                this.m_TypingWorkspace = new Surgical.TypingWorkspace(this.m_MainWindowCommandButtonHandler, secondMonitorWindow, this.m_TabItemTyping);
				this.m_TabItemTyping.Content = this.m_TypingWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemTyping);
                this.m_TabItemTyping.Focus();                
                this.m_TypingWorkspace.Loaded += new RoutedEventHandler(this.TypingWorkspace_Loaded);				
			}
        }        

        private void TypingWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_TypingWorkspace.SetFocus();
        }

        public void AddFlowWorkspace()
        {
            if (this.m_TabItemFlow.Parent != null)
            {
                this.m_TabItemFlow.Focus();
            }
            else
            {
                this.m_FlowWorkspace = new Flow.FlowWorkspace(this.m_MainWindowCommandButtonHandler, this.m_TabItemFlow);
                this.m_TabItemFlow.Content = this.m_FlowWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemFlow);
                this.m_TabItemFlow.Focus();                
                this.m_FlowWorkspace.Loaded += new RoutedEventHandler(this.FlowWorkspace_Loaded);				
			}
        }

        public void AddFlowWorkspace(string reportNo, string masterAccessionNo)
        {
            this.AddFlowWorkspace();
            this.m_FlowWorkspace.GetCase(reportNo, masterAccessionNo);
        }

        private void FlowWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_FlowWorkspace.TabItemGeneral.Focus();
        }        

        public void AddFlowMarkerPanelWorkspace()
        {
            
        }

        public void AddSearchWorkspace()
        {            
            if (this.m_TabItemSearch.Parent != null)
            {
                this.m_TabItemSearch.Focus();
            }
            else
            {
                this.m_SearchWorkspace = new SearchWorkspace(this.m_MainWindowCommandButtonHandler, this.m_TabItemSearch);
                this.m_TabItemSearch.Content = this.m_SearchWorkspace;                
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemSearch);
                this.m_TabItemSearch.Focus();
                this.m_SearchWorkspace.Loaded += new RoutedEventHandler(m_SearchWorkspace_Loaded);
            }
        }

        private void m_SearchWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_SearchWorkspace.SetFocus();
        }
        
        public void AddLabWorkspace()
        {            
            if (m_TabItemLab.Parent != null)
            {
                m_TabItemLab.Focus();
            }
            else
            {
                this.m_LabWorkspace = new Test.LabWorkspace(this.m_MainWindowCommandButtonHandler, this.m_TabItemLab);
                this.m_TabItemLab.Content = this.m_LabWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemLab);
                this.m_TabItemLab.Focus();
                //this.m_LabWorkspace.Loaded += new RoutedEventHandler(m_LabWorkspace_Loaded);
			}            
        }

        public void AddLabWorkspace(string masterAccessionNo, string reportNo)
        {
            this.AddLabWorkspace();
            this.m_LabWorkspace.GetCase(masterAccessionNo, reportNo);
        }

        /*private void m_LabWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_LabWorkspace.TabItemCaseList.Focus();
        }*/        

        private void ToolBarButtonSearchWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddSearchWorkspace();
        }		

        public void MenuItemCytologyWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddCytologyWorkspace();
        }

        public void MenuItemAdministrationWorkspace_Click(object sender, RoutedEventArgs args)
        {            
            this.AddAdministrationWorkspace();                     
        }        

        private void MenuItemReportDistributionWorkspace_Click(object sender, RoutedEventArgs e)
        {            
            this.m_ReportDistributionWorkspace = new ReportDistribution.ReportDistributionWorkspace();            
            this.m_TabItemReportDistribution.Content = this.m_ReportDistributionWorkspace;            
            this.TabControlLeftWorkspace.Items.Add(this.m_TabItemReportDistribution);
            this.m_TabItemReportDistribution.Focus();   
        }   

        public void onFileExit_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }        

        public void PathologistWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddPathologistWorkspace();            
        }

        public void FlowWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddFlowWorkspace();
        }        

        //public void ScanProcessingWorkspace_Click(object sender, RoutedEventArgs args)
        //{
        //    this.AddScanProcessingWorkspace();
        //}
        
        public void ToolBarButtonPathologistWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddPathologistWorkspace();
        }

        public void ToolBarButtonFlowWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddFlowWorkspace();
        }                
        
        public void ToolBarButtonLabWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddLabWorkspace();
        }

        private void ToolBarButtonCytologyWorkspace_Click(object sender, RoutedEventArgs e)
        {
            this.AddCytologyWorkspace();
        }		

        public void ToolBarButtonSave_Click(object sender, RoutedEventArgs args)
        {
            this.m_MainWindowCommandButtonHandler.OnSave();
        }

		public void ToolBarButtonViewDocument_Click(object sender, RoutedEventArgs args)
		{            
            this.m_MainWindowCommandButtonHandler.OnShowCaseDocument();
        }

        private void ToolBarButtonViewDocument_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // WHC This is here to prevent sending the click event twice which causes an IO exception:
            // "The process cannot access the file <filename> because it is being used by another process."
            e.Handled = true;
        }        

        public void ToolBarButtonOrderForm_Click(object sender, RoutedEventArgs args)
        {
			this.m_MainWindowCommandButtonHandler.OnShowOrderForm();
        }        

        public void ToolBarButtonAssign_Click(object sender, RoutedEventArgs args)
        {
			this.m_MainWindowCommandButtonHandler.OnAssignCase();
        }        
        
        public void MenuItemLabWorkspace_Click(object sender, RoutedEventArgs args)
        {
            this.AddLabWorkspace();
        }              
        
        public void MenuItemReportDistributionMonitor_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.UI.Monitor.MonitorPath monitorPath = new Monitor.MonitorPath();            
            monitorPath.Show(YellowstonePathology.UI.Monitor.MonitorPageLoadEnum.ReportDistributionMonitor);            
        }        

        public void MenuItemFlowMarkerPanels_Click(object sender, RoutedEventArgs args)
        {            
			this.AddFlowWorkspace();
		}                

        public void MenuItemScanProcessing_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.UI.Scanning.ProcessScannedDocumentsWindow documents = new Scanning.ProcessScannedDocumentsWindow();
            documents.ShowDialog();
        }                    

        public void MenuItemRuleBrowser_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.UI.RulesBrowser ruleBrowser = new RulesBrowser();
            ruleBrowser.ShowDialog();
        }

		public void ToolBarButtonTypingWorkspace_Click(object sender, RoutedEventArgs args)
		{
			this.AddTypingWorkspace();
		}        

		private void MenuItemMasterLog_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog report = new YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog();
			report.CreateReport(DateTime.Today);
            report.PrintReport();
		}		

        private void ToolBarButtonProviderDistribution_Click(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.OnStartProviderDistributionPath();
        }             

		private void MenuItemPreferences_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.UI.Common.UserPreferences dlg = new YellowstonePathology.UI.Common.UserPreferences();
			dlg.ShowDialog();
		}

		private void MenuItemDatabase_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.UI.Common.DatabaseSelection dlg = new YellowstonePathology.UI.Common.DatabaseSelection();
			dlg.ShowDialog();
		}

		private void MenuItemValidate_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.UI.Common.ScanTest dlg = new YellowstonePathology.UI.Common.ScanTest();
			dlg.ShowDialog();
		}

		public void Restart()
		{
			Close();
		}                    

        private void ToolBarButtonAddAmendment_Click(object sender, RoutedEventArgs e)
        {
			this.m_MainWindowCommandButtonHandler.OnShowAmendmentDialog();			
        }        		

		private void SurgicalRescreen_Click(object sender, RoutedEventArgs e)
		{
			UI.Surgical.SurgicalRescreenDialog surgicalRescreenDialog = new Surgical.SurgicalRescreenDialog();
			surgicalRescreenDialog.ShowDialog();
		}

		private void MenuItemLogin_Click(object sender, RoutedEventArgs e)
		{
            this.ShowTaskWorkspace();
            this.ShowClientOrderWorkspace();
            this.ShowLoginWorkspace();
		}

        private void ShowLoginWorkspace()
        {
            if (m_TabItemLogin.Parent != null)
            {
                m_TabItemLogin.Focus();
            }
            else
            {
                this.m_LoginWorkspace = new Login.LoginWorkspace(this.m_MainWindowCommandButtonHandler, m_TabItemLogin);
                this.m_TabItemLogin.Content = this.m_LoginWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemLogin);
                this.m_TabItemLogin.Focus();
            }
        }

        private void ShowClientOrderWorkspace()
        {
            if (m_TabItemClientOrder.Parent != null)
            {
                m_TabItemClientOrder.Focus();
            }
            else
            {
                this.m_ClientOrderWorkspace = new ClientOrderWorkspace(this.m_MainWindowCommandButtonHandler, m_TabItemClientOrder);
                this.m_TabItemClientOrder.Content = this.m_ClientOrderWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemClientOrder);
                this.m_TabItemClientOrder.Focus();
            }
        }

        private void ShowTaskWorkspace()
        {
            if (m_TabItemTask.Parent != null)
            {
                m_TabItemTask.Focus();
            }
            else
            {
                this.m_TaskWorkspace = new TaskWorkspace(this.m_MainWindowCommandButtonHandler, m_TabItemTask);
                this.m_TabItemTask.Content = this.m_TaskWorkspace;
                this.TabControlLeftWorkspace.Items.Add(this.m_TabItemTask);
                this.m_TabItemTask.Focus();
            }
        }

        private void MenuItemMaterialTracking_Click(object sender, RoutedEventArgs e)
        {			            
            YellowstonePathology.UI.MaterialTracking.MaterialTrackingPath caseCompilationPath = new MaterialTracking.MaterialTrackingPath();
            caseCompilationPath.Start();
        }

        private void MenuItemProviderLookup_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Client.ProviderLookupDialog providerLookupDialog = new Client.ProviderLookupDialog();
            providerLookupDialog.ShowDialog();
        }

        private void MenuItemSlideLookup_Click(object sender, RoutedEventArgs e)
        {
            MaterialTrackingReportNoDialog dialog = new MaterialTrackingReportNoDialog();
            dialog.ShowDialog();
        }

        private void MenuItemTumorRegistryDistribution_Click(object sender, RoutedEventArgs e)
        {
            TumerRegistryDistributionDialog tumorRegistryDistributionDialog = new TumerRegistryDistributionDialog();
            tumorRegistryDistributionDialog.ShowDialog();
        }        

        private void MenuItemBillingReports_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Billing.ClientBillingReportDialog clientBillingReportDialog = new Billing.ClientBillingReportDialog();
            clientBillingReportDialog.ShowDialog();
        }

        private void MenuItemPSATransder_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Billing.PSATransferDialog psaTransferDialog = new Billing.PSATransferDialog();
            psaTransferDialog.ShowDialog();
        }

        private void MenuItemValidationTesting_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.ValidationTestingDialog validationTestingDialog = new ValidationTestingDialog();
            validationTestingDialog.ShowDialog();
        }

        private void MenuItemSVHBillingDataImport_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Billing.SVHBillingDataImportDialog svhBillingDataImportDialog = new Billing.SVHBillingDataImportDialog();
            svhBillingDataImportDialog.ShowDialog();
        }

        private void MenuItemShowReportDistributionMonitor_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Monitor.MonitorPath monitorPath = new Monitor.MonitorPath();            
            monitorPath.Show(YellowstonePathology.UI.Monitor.MonitorPageLoadEnum.ReportDistributionMonitor);            
        }

        private void MenuItemShowCytologyScreeningMonitor_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Monitor.MonitorPath monitorPath = new Monitor.MonitorPath();            
            monitorPath.Show(YellowstonePathology.UI.Monitor.MonitorPageLoadEnum.CytologyScreeningMonitor);            
        }

        private void MenuItemShowPendingTestMonitor_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Monitor.MonitorPath monitorPath = new Monitor.MonitorPath();
            monitorPath.Show(YellowstonePathology.UI.Monitor.MonitorPageLoadEnum.PendingTestMonitor);            
        }

        private void MenuItemMongoMigration_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Mongo.MongoMigrationWindow mongoMigrationWindow = new Mongo.MongoMigrationWindow();
            mongoMigrationWindow.ShowDialog();
        }

        private void MenuItemClientSupplyOrders_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Client.ClientSupplyOrderListDialog clientSupplyOrderListDialog = new Client.ClientSupplyOrderListDialog();
            clientSupplyOrderListDialog.ShowDialog();
        }

        private void MenuItemCytologyUnsatLetterDialog_Click(object sender, RoutedEventArgs e)
        {
            Cytology.CytologyUnsatLetterDialog dialog = new Cytology.CytologyUnsatLetterDialog();
            dialog.ShowDialog();
        }

        private void MenuItemPantherOrders_Click(object sender, RoutedEventArgs e)
        {
            PantherOrdersDialog pantherOrdersDialog = new PantherOrdersDialog();
            pantherOrdersDialog.Show();
        }

        private void MenuItemPantherStorage_Click(object sender, RoutedEventArgs e)
        {
            PantherStorageDialog pantherStorageDialog = new PantherStorageDialog();
            pantherStorageDialog.ShowDialog();
        }

        private void MenuItemShowMissingInformationMonitor_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Monitor.MonitorPath monitorPath = new Monitor.MonitorPath();
            monitorPath.Show(YellowstonePathology.UI.Monitor.MonitorPageLoadEnum.MissingInformationMonitor);            
        }

        private void MenuItemAcidWashOrders_Click(object sender, RoutedEventArgs e)
        {
            Test.AcidWashOrdersDialog acidWashOrdersDialog = new Test.AcidWashOrdersDialog();
            acidWashOrdersDialog.ShowDialog();
        }

        private void MenuItemLockedCases_Click(object sender, RoutedEventArgs e)
        {
            UI.LockedCaseDialog lockedCaseDialog = new LockedCaseDialog();
            lockedCaseDialog.Show();
        }

        private void ToolBarButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.OnShowMessagingDialog();
        }

        private void MenuItemParsePSAData_Click(object sender, RoutedEventArgs e)
        {
            ParsePsaAccessionsWindow window = new ParsePsaAccessionsWindow();
            window.Show();
        }

        private void MenuItemUpdateSpellDictionary_Click(object sender, RoutedEventArgs e)
        {
            if(App.HandledictionarySetup() == true)
            {
                MessageBox.Show("The dictionary was successfully updated.");
            }
        }

        private void MenuItemEmbedding_Click(object sender, RoutedEventArgs e)
        {
            EmbeddingDialog embeddingDialog = new EmbeddingDialog();
            embeddingDialog.ShowDialog();
        }

        private void MenuItemTesting_Click(object sender, RoutedEventArgs e)
        {
            TestWindow window = new TestWindow();
            window.Show();
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            Login.DeleteAccessionPath deleteAccessionPath = new Login.DeleteAccessionPath(this.TabControlLeftWorkspace.Items.Count);
            deleteAccessionPath.CloseOpenTabs += DeleteAccessionPath_CloseOpenTabs;
            deleteAccessionPath.Start();
        }

        private bool DeleteAccessionPath_CloseOpenTabs(object sender, EventArgs e)
        {
            for (int idx = this.TabControlLeftWorkspace.Items.Count; idx > 0; idx--)
            {
                TabItem tabItem = (TabItem)this.TabControlLeftWorkspace.Items[idx - 1];
                tabItem.Focus();
                this.m_MainWindowCommandButtonHandler.OnRemoveTab();
                this.TabControlLeftWorkspace.Items.Remove(tabItem);
            }
            return true;
        }
    }
}
