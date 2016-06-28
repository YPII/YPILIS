using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Gross
{
	public class HistologyGrossPath
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ContainerId;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        
        private HistologyGrossDialog m_HistologyGrossDialog;
        private SecondaryWindow m_SecondaryWindow;                

		public HistologyGrossPath()
		{
            
		}        

		private void HistologyGrossDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
           
		}

		public void Start()
		{
            this.m_HistologyGrossDialog = new HistologyGrossDialog();            
            this.m_HistologyGrossDialog.Closing += new System.ComponentModel.CancelEventHandler(HistologyGrossDialog_Closing);
            this.m_HistologyGrossDialog.Show();
                                    
            if (this.m_HistologyGrossDialog.PageNavigator.HasDualMonitors() == true)
            {
                this.m_SecondaryWindow = new SecondaryWindow();
                this.m_HistologyGrossDialog.PageNavigator.ShowSecondMonitorWindow(this.m_SecondaryWindow);
                this.m_HistologyGrossDialog.PageNavigator.SecondMonitorWindow.WindowState = System.Windows.WindowState.Maximized;
                BlankPage blankPage = new BlankPage();
                this.m_SecondaryWindow.PageNavigator.Navigate(blankPage);
            }             

            this.ShowScanSecurityBadgePage();			
		}

		public void ShowScanSecurityBadgePage()
		{
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new YellowstonePathology.UI.Login.ScanSecurityBadgePage(System.Windows.Visibility.Visible);
            this.m_HistologyGrossDialog.PageNavigator.Navigate(scanSecurityBadgePage);
			scanSecurityBadgePage.AuthentificationSuccessful += new YellowstonePathology.UI.Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            scanSecurityBadgePage.Close += new Login.ScanSecurityBadgePage.CloseEventHandler(ScanSecurityBadgePage_Close);
		}

        private void ScanSecurityBadgePage_Close(object sender, EventArgs e)
        {
            System.Windows.Controls.UserControl scanSecurityBadgePage = (System.Windows.Controls.UserControl)sender;
            System.Windows.Window.GetWindow(scanSecurityBadgePage).Close();
        }

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, EventArgs e)
        {
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;			
			this.ShowScanContainerPage();
        }

		public void ShowScanContainerPage()
		{
			ScanContainerPage scanContainerPage = new ScanContainerPage(this.m_SystemIdentity, "Please Scan Container");
            scanContainerPage.UseThisContainer += new ScanContainerPage.UseThisContainerEventHandler(ScanContainerPage_UseThisContainer);
            scanContainerPage.PageTimedOut += new ScanContainerPage.PageTimedOutEventHandler(ScanContainerPage_PageTimedOut);
            scanContainerPage.BarcodeWontScan += new ScanContainerPage.BarcodeWontScanEventHandler(ScanContainerPage_BarcodeWontScan);
            scanContainerPage.SignOut += new ScanContainerPage.SignOutEventHandler(ScanContainerPage_SignOut);            

			this.m_HistologyGrossDialog.PageNavigator.Navigate(scanContainerPage);
		}        

		private void MaterialBatchSelectionPage_Back(object sender, EventArgs e)
		{
			this.ShowScanContainerPage();
		}

		private void MaterialBatchSelectionPage_Next(object sender, CustomEventArgs.MaterialTrackingBatchEventArgs e)
		{
			this.ShowScanContainerPage();
		}

        private void ScanContainerPage_SignOut(object sender, EventArgs e)
        {            
            this.ShowScanSecurityBadgePage();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this.m_HistologyGrossDialog);
        }

        private void ScanContainerPage_UseThisContainer(object sender, string containerId)
        {
            this.m_ContainerId = containerId;
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromContainerId(containerId);
            if(string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, m_HistologyGrossDialog);

                if (this.m_AccessionOrder == null)
                {
                    System.Windows.MessageBox.Show("The scanned container was not found.");
                    this.ShowScanContainerPage();
                }
                else
                {
                    if (this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
                    {
                        this.HandleLockAquiredByMe();
                    }
                    else
                    {
                        this.ShowCaseLockPage();
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("The scanned container was not found.");
                this.ShowScanContainerPage();

            }            
        } 
        
        private void HandleLockAquiredByMe()
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(this.m_ContainerId);
            this.AddMaterialTrackingLog(specimenOrder);

            if (this.m_HistologyGrossDialog.PageNavigator.HasDualMonitors() == true)
            {
                DictationTemplatePage dictationTemplatePage = new DictationTemplatePage(specimenOrder, this.m_AccessionOrder, this.m_SystemIdentity);
                this.m_SecondaryWindow.PageNavigator.Navigate(dictationTemplatePage);
            }

            if (string.IsNullOrEmpty(specimenOrder.ProcessorRunId) == true)
            {
                YellowstonePathology.Business.Surgical.ProcessorRunCollection processorRunCollection = YellowstonePathology.Business.Surgical.ProcessorRunCollection.GetAll(false);
                YellowstonePathology.Business.Surgical.ProcessorRun processorRun = processorRunCollection.Get(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference);
                if(processorRun != null)
                {
                    specimenOrder.SetProcessor(processorRun);
                    specimenOrder.SetFixationDuration();
                }                
            }

            if (this.m_AccessionOrder.PrintMateColumnNumber == 0 && this.m_AccessionOrder.PanelSetOrderCollection.HasTestBeenOrdered(48) == false)
            {
                this.ShowBlockColorSelectionPage(specimenOrder);
            }
            else
            {
                this.ShowPrintBlockPage(specimenOrder);
            }
        }       

        private void ShowCaseLockPage()
        {
            UI.Login.CaseLockedPage caseLockedPage = new Login.CaseLockedPage(this.m_AccessionOrder);
            caseLockedPage.Next += CaseLockedPage_Next;            
            caseLockedPage.AskForLock += CaseLockedPage_AskForLock;
            this.m_HistologyGrossDialog.PageNavigator.Navigate(caseLockedPage);
        }

        private void CaseLockedPage_AskForLock(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            UI.AppMessaging.MessagingPath.Instance.StartSendRequest(e.AccessionOrder, this.m_HistologyGrossDialog.PageNavigator);
            UI.AppMessaging.MessagingPath.Instance.LockWasReleased += MessageQueuePath_LockWasReleased;
            UI.AppMessaging.MessagingPath.Instance.HoldYourHorses += MessageQueuePath_HoldYourHorses;
        }

        private void MessageQueuePath_HoldYourHorses(object sender, EventArgs e)
        {
            this.ShowScanContainerPage();
        }

        private void MessageQueuePath_LockWasReleased(object sender, EventArgs e)
        {
            this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_HistologyGrossDialog);
            this.HandleLockAquiredByMe();
        }

        private void CaseLockedPage_Next(object sender, EventArgs e)
        {
            this.ShowScanContainerPage();
        }

        private void AddMaterialTrackingLog(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);            
            
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, specimenOrder.SpecimenOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
                thisLocation.LocationId, thisLocation.Description, "Container Scan", "Container Scanned At Gross", "Specimen", this.m_AccessionOrder.MasterAccessionNo, specimenOrder.Description, specimenOrder.ClientAccessioned);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, m_HistologyGrossDialog);            
        }

        private void ScanContainerPage_BarcodeWontScan(object sender, EventArgs e)
        {
            Login.Receiving.BarcodeManualEntryPage containerManualEntryPage = new Login.Receiving.BarcodeManualEntryPage();
            containerManualEntryPage.Return += new Login.Receiving.BarcodeManualEntryPage.ReturnEventHandler(BarcodeManualEntryPage_Return);
            containerManualEntryPage.Back += new Login.Receiving.BarcodeManualEntryPage.BackEventHandler(ContainerManualEntryPage_Back);
            this.m_HistologyGrossDialog.PageNavigator.Navigate(containerManualEntryPage);			
		}

        private void ContainerManualEntryPage_Back()
        {
            this.ShowScanContainerPage();
        }

        private void ScanContainerPage_PageTimedOut(object sender, EventArgs e)
        {
            this.ShowScanSecurityBadgePage();
        }
                
		private void BarcodeManualEntryPage_Return(object sender, string containerId)
		{
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromContainerId(containerId);
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, m_HistologyGrossDialog);

            if (this.m_AccessionOrder == null)
            {
                System.Windows.MessageBox.Show("The scanned container was not found.");
                this.ShowScanContainerPage();
                return;
            }			

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(containerId);
            this.ShowPrintBlockPage(specimenOrder);           
		}        

		public void ShowBlockOptionsPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
	    {
			BlockOptionsPage blockOptionsPage = new BlockOptionsPage(specimenOrder, aliquotOrder, this.m_AccessionOrder);
            blockOptionsPage.Next += new BlockOptionsPage.NextEventHandler(BlockOptionsPage_Next);
            blockOptionsPage.Back += new BlockOptionsPage.BackEventHandler(BlockOptionsPage_Back);
            blockOptionsPage.ShowBlockColorSelectionPage += new BlockOptionsPage.ShowBlockColorSelectionPageEventHandler(BlockOptionsPage_ShowBlockColorSelectionPage);
			this.m_HistologyGrossDialog.PageNavigator.Navigate(blockOptionsPage);
		}

        private void BlockOptionsPage_ShowBlockColorSelectionPage(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            this.ShowBlockColorSelectionPage(e.SpecimenOrder);    
        }

        private void BlockOptionsPage_Back(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            this.ShowPrintBlockPage(e.SpecimenOrder);			
        }        

		private void BlockOptionsPage_Next(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e)
		{			
            this.ShowPrintBlockPage(e.SpecimenOrder);			
		}

		public void ShowPrintBlockPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{            
			PrintBlockPage printBlockPage = new PrintBlockPage(this.m_SystemIdentity, this.m_AccessionOrder, specimenOrder);
            printBlockPage.Next += new PrintBlockPage.NextEventHandler(PrintBlockPage_Next);
            printBlockPage.ShowBlockOptions += new PrintBlockPage.ShowBlockOptionsEventHandler(PrintBlockPage_ShowBlockOptions);
            printBlockPage.ShowStainOrderPage += new PrintBlockPage.ShowStainOrderPageEventHandler(PrintBlockPage_ShowStainOrderPage);
            printBlockPage.ShowProcessorSelectionPage += new PrintBlockPage.ShowProcessorSelectionPageEventHandler(PrintBlockPage_ShowProcessorSelectionPage);
			this.m_HistologyGrossDialog.PageNavigator.Navigate(printBlockPage);
		}

        private void PrintBlockPage_ShowProcessorSelectionPage(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            ProcessorSelectionPage processorSelectionPage = new ProcessorSelectionPage(e.SpecimenOrder);            
            processorSelectionPage.Next += new ProcessorSelectionPage.NextEventHandler(ProcessorSelectionPage_Next);
            this.m_HistologyGrossDialog.PageNavigator.Navigate(processorSelectionPage);
        }

        private void ProcessorSelectionPage_Next(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            this.ShowPrintBlockPage(e.SpecimenOrder);
        }        

        private void PrintBlockPage_ShowStainOrderPage(object sender, YellowstonePathology.UI.CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs e)
        {
                        
            StainOrderPage stainOrderPage = new StainOrderPage(this.m_AccessionOrder, e.SpecimenOrder, e.AliquotOrder, this.m_SystemIdentity);
            stainOrderPage.Back += new StainOrderPage.BackEventHandler(StainOrderPage_Back);            
            this.m_HistologyGrossDialog.PageNavigator.Navigate(stainOrderPage);
        }        

        private void StainOrderPage_Back(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            this.ShowPrintBlockPage(e.SpecimenOrder);
        }

        private void PrintBlockPage_ShowBlockOptions(object sender, CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs e)
        {
            this.ShowBlockOptionsPage(e.SpecimenOrder, e.AliquotOrder);
        }

        private void PrintBlockPage_Next(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            if (this.m_HistologyGrossDialog.PageNavigator.HasDualMonitors() == true)
            {
                BlankPage blankPage = new BlankPage();
                this.m_SecondaryWindow.PageNavigator.Navigate(blankPage);
            }

            this.ShowScanContainerPage();
        }
		
		private void PrintBlockPage_Return_HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			List<object> objects = (List<Object>)e.Data;
			switch ((HistologyGrossCommandEnum)objects[0])
			{
				case HistologyGrossCommandEnum.BlockOptions:
					string specimenOrderId = objects[1].ToString();
					string aliquotOrderId = objects[2].ToString();
					
					break;
				case HistologyGrossCommandEnum.TimeOut:
					this.ShowScanSecurityBadgePage();					
					break;
			}
		}

		private void ShowBlockColorSelectionPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
			BlockColorSelectionPage blockColorSelectionPage = new BlockColorSelectionPage(specimenOrder, this.m_AccessionOrder);
            blockColorSelectionPage.Next += new BlockColorSelectionPage.NextEventHandler(BlockColorSelectionPage_Next);
			this.m_HistologyGrossDialog.PageNavigator.Navigate(blockColorSelectionPage);
		}

        private void BlockColorSelectionPage_Next(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            this.ShowPrintBlockPage(e.SpecimenOrder);
        }		
	}
}
