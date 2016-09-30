using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Cutting
{
    public class CuttingStationPath
    {
        private CuttingWorkspaceWindow m_CuttingWorkspaceWindow;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_AliquotOrderId;   
        private CuttingPage m_CuttingPage;

        private YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter m_HistologySlidePaperLabelPrinter;

        public CuttingStationPath()
        {
            this.m_HistologySlidePaperLabelPrinter = new Business.Label.Model.HistologySlidePaperLabelPrinter();
        }

        public void Start()
        {            
            this.m_CuttingWorkspaceWindow = new CuttingWorkspaceWindow();
            this.ShowScanSecurityBadgePage();
            this.m_CuttingWorkspaceWindow.ShowDialog();
        }        

        private void ShowScanSecurityBadgePage()
        {
            this.m_CuttingWorkspaceWindow.MenuFile.Visibility = System.Windows.Visibility.Visible;
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new Login.ScanSecurityBadgePage(System.Windows.Visibility.Visible);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(scanSecurityBadgePage);
            scanSecurityBadgePage.AuthentificationSuccessful += new Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            scanSecurityBadgePage.Close += new Login.ScanSecurityBadgePage.CloseEventHandler(ScanSecurityBadgePage_Close);
        }

        private void ScanSecurityBadgePage_Close(object sender, EventArgs e)
        {
            this.m_CuttingWorkspaceWindow.Close();
        }

        private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, EventArgs e)
        {
            this.m_CuttingWorkspaceWindow.MenuFile.Visibility = System.Windows.Visibility.Collapsed;            
            this.ShowScanAliquotPage(null);
        }

        private void ShowScanAliquotPage(string lastMasterAccessionNo)
        {            
            ScanAliquotPage scanAliquotPage = new ScanAliquotPage(this.m_AccessionOrder, lastMasterAccessionNo);
            scanAliquotPage.AliquotOrderSelected += new ScanAliquotPage.AliquotOrderSelectedEventHandler(ScanAliquotPage_AliquotOrderSelected);
            scanAliquotPage.SignOut += new ScanAliquotPage.SignOutEventHandler(ScanAliquotPage_SignOut);
            scanAliquotPage.ShowMasterAccessionNoEntryPage += new ScanAliquotPage.ShowMasterAccessionNoEntryPageEventHandler(ScanAliquotPage_ShowMasterAccessionNoEntryPage);
            scanAliquotPage.UseLastMasterAccessionNo += new ScanAliquotPage.UseLastMasterAccessionNoEventHandler(ScanAliquotPage_UseLastMasterAccessionNo);
            //scanAliquotPage.PageTimedOut += new ScanAliquotPage.PageTimedOutEventHandler(PageTimedOut);
            scanAliquotPage.PrintImmunos += ScanAliquotPage_PrintImmunos;            
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(scanAliquotPage);
        }

        private void ScanAliquotPage_ShowCaseLockedPage(object sender, CustomEventArgs.AccessionOrderReturnEventArgs eventArgs)
        {
            this.ShowCaseLockedPage(eventArgs.AccessionOrder);   
        }

        private void ShowCaseLockedPage(Business.Test.AccessionOrder accessionOrder)
        {
            UI.Login.CaseLockedPage caseLockedPage = new Login.CaseLockedPage(accessionOrder);
            caseLockedPage.Next += CaseLockedPage_Next;
            caseLockedPage.AskForLock += CaseLockedPage_AskForLock;
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(caseLockedPage);
        }

        private void CaseLockedPage_AskForLock(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {            
            UI.AppMessaging.MessagingPath.Instance.StartSendRequest(e.AccessionOrder, this.m_CuttingWorkspaceWindow.PageNavigator);
            UI.AppMessaging.MessagingPath.Instance.LockWasReleased += MessageQueuePath_LockWasReleased;
            UI.AppMessaging.MessagingPath.Instance.HoldYourHorses += Instance_HoldYourHorses;
            UI.AppMessaging.MessagingPath.Instance.Nevermind += MessageQueuePath_Nevermind;
        }

        private void Instance_HoldYourHorses(object sender, EventArgs e)
        {
            this.ShowScanAliquotPage(this.m_AccessionOrder.MasterAccessionNo);
        }

        private void MessageQueuePath_Nevermind(object sender, EventArgs e)
        {
            this.ShowScanAliquotPage(null);
        }

        private void MessageQueuePath_LockWasReleased(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_CuttingWorkspaceWindow);
            this.HandleLockAquiredByMe();
        }

        private void CaseLockedPage_Next(object sender, UI.CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            this.ShowScanAliquotPage(e.AccessionOrder.MasterAccessionNo);
        }

        private void ScanAliquotPage_PrintImmunos(object sender, EventArgs eventArgs)
        {            
            if (this.m_HistologySlidePaperLabelPrinter.Queue.Count != 0)
            {                
                this.m_HistologySlidePaperLabelPrinter.Print();
            }
            else
            {
                System.Windows.MessageBox.Show("There are not any labels waiting to be printed.");
            }
        }

        private void ScanAliquotPage_UseLastMasterAccessionNo(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.HandleMasterAccessionNoFound(eventArgs.MasterAccessionNo);
        }

        private void ScanAliquotPage_ShowMasterAccessionNoEntryPage(object sender, EventArgs eventArgs)
        {
            YellowstonePathology.UI.Cutting.MasterAccessionNoEntryPage masterAccessionNoEntryPage = new MasterAccessionNoEntryPage();
            masterAccessionNoEntryPage.UseThisMasterAccessionNo += new MasterAccessionNoEntryPage.UseThisMasterAccessionNoEventHandler(MasterAccessionNoEntryPage_UseThisMasterAccessionNo);
            masterAccessionNoEntryPage.Close += new MasterAccessionNoEntryPage.CloseEventHandler(MasterAccessionNoEntryPage_Close);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(masterAccessionNoEntryPage);
        }

        private void MasterAccessionNoEntryPage_Close(object sender, EventArgs eventArgs)
        {
            this.ShowScanAliquotPage(null);
        }

        private void MasterAccessionNoEntryPage_UseThisMasterAccessionNo(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.HandleMasterAccessionNoFound(eventArgs.MasterAccessionNo);
        }

        private void HandleMasterAccessionNoFound(string masterAccessionNo)
        {
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, m_CuttingWorkspaceWindow);			

            if(this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
            {
                this.ShowAliquotOrderSelectionPage();
            }
            else
            {
                this.ShowCaseLockedPageManualMA(this.m_AccessionOrder);
            }
        }

        private void ShowCaseLockedPageManualMA(Business.Test.AccessionOrder accessionOrder)
        {
            UI.Login.CaseLockedPage caseLockedPage = new Login.CaseLockedPage(accessionOrder);
            caseLockedPage.Next += CaseLockedPage_Next;
            caseLockedPage.AskForLock += CaseLockedPage_AskForLockManualMA;
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(caseLockedPage);
        }

        private void CaseLockedPage_AskForLockManualMA(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {            
            UI.AppMessaging.MessagingPath.Instance.StartSendRequest(e.AccessionOrder, this.m_CuttingWorkspaceWindow.PageNavigator);
            //AppMessaging.MessagingPath.Instance.LockWasReleased += MessageQueuePath_LockWasReleasedManualMA;
            //AppMessaging.MessagingPath.Instance.HoldYourHorses += Instance_HoldYourHorses;
            UI.AppMessaging.MessagingPath.Instance.Nevermind += MessageQueuePath_Nevermind;
        }

        private void MessageQueuePath_LockWasReleasedManualMA(object sender, EventArgs e)
        {
            this.ShowAliquotOrderSelectionPage();
        }

        private void ShowAliquotOrderSelectionPage()
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrdersThatHaveTestOrders();
            AliquotOrderSelectionPage aliquotOrderSelectionPage = new AliquotOrderSelectionPage(aliquotOrderCollection, this.m_AccessionOrder);
            aliquotOrderSelectionPage.AliquotOrderSelected += new AliquotOrderSelectionPage.AliquotOrderSelectedEventHandler(AliquotOrderSelectionPage_AliquotOrderSelected);
            aliquotOrderSelectionPage.Back += new AliquotOrderSelectionPage.BackEventHandler(AliquotOrderSelectionPage_Back);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(aliquotOrderSelectionPage);
        }

        private void AliquotOrderSelectionPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {            
            this.ShowScanAliquotPage(eventArgs.MasterAccessionNo);
        }

        private void AliquotOrderSelectionPage_AliquotOrderSelected(object sender, CustomEventArgs.AliquotOrderReturnEventArgs eventArgs)
        {
            this.HandleAliquotOrderFound(eventArgs.AliquotOrder);
        }

        private void ScanAliquotPage_AliquotOrderSelected(object sender, CustomEventArgs.BarcodeReturnEventArgs eventArgs)
        {            
            this.m_AliquotOrderId = eventArgs.Barcode.ID;
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromAliquotOrderId(this.m_AliquotOrderId);

            if (string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_CuttingWorkspaceWindow);

                if (this.m_AccessionOrder != null)
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
                else
                {
                    System.Windows.MessageBox.Show("The block scanned could not be found.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("The block scanned could not be found.");
            }
        }

        private void HandleLockAquiredByMe()
        {
            Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_AliquotOrderId);
            this.AddMaterialTrackingLog(aliquotOrder);
            this.HandleAliquotOrderFound(aliquotOrder);
        }

        private void ShowCaseLockPage()
        {
            UI.Login.CaseLockedPage caseLockedPage = new Login.CaseLockedPage(this.m_AccessionOrder);
            caseLockedPage.Next += CaseLockedPage_Next;
            caseLockedPage.AskForLock += CaseLockedPage_AskForLock;
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(caseLockedPage);
        }

        private void AddMaterialTrackingLog(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
            YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, aliquotOrder.AliquotOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
                thisLocation.LocationId, thisLocation.Description, "Block Scanned", "Block Scanned At Cutting", "Aliquot", this.m_AccessionOrder.MasterAccessionNo, aliquotOrder.Label, aliquotOrder.ClientAccessioned);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, this.m_CuttingWorkspaceWindow);
        }

        private void HandleAliquotOrderFound(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            if (aliquotOrder.TestOrderCollection.OnlyHasOneHAndE() == true)
            {
                YellowstonePathology.Business.Test.Model.TestOrder_Base testOrderBase = aliquotOrder.TestOrderCollection.GetTestOrderBase(49);
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(testOrderBase.TestOrderId);
                this.ShowCuttingPage(aliquotOrder, testOrder);
            }
            else
            {
                this.ShowTestOrderSelectionPage(aliquotOrder);
            }
        }

        private void ShowTestOrderSelectionPage(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            TestOrderPanelSetOrderViewCollection testOrderPanelSetOrderViewCollection = new TestOrderPanelSetOrderViewCollection(aliquotOrder.TestOrderCollection, this.m_AccessionOrder);
            TestOrderSelectionPage testOrderSelectionPage = new TestOrderSelectionPage(testOrderPanelSetOrderViewCollection, aliquotOrder, this.m_AccessionOrder);
            testOrderSelectionPage.TestOrderSelected += new TestOrderSelectionPage.TestOrderSelectedEventHandler(TestOrderSelectionPage_TestOrderSelected);
            testOrderSelectionPage.Back += new TestOrderSelectionPage.BackEventHandler(TestOrderSelectionPage_Back);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(testOrderSelectionPage);
        }

        private void TestOrderSelectionPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.ShowScanAliquotPage(eventArgs.MasterAccessionNo);
        }

        private void TestOrderSelectionPage_TestOrderSelected(object sender, CustomEventArgs.TestOrderAliquotOrderReturnEventArgs eventArgs)
        {
            this.ShowCuttingPage(eventArgs.AliquotOrder, eventArgs.TestOrder);
        }

        private void ScanAliquotPage_SignOut(object sender, EventArgs e)
        {            
            this.ShowScanSecurityBadgePage();
        }        

        private void ShowCuttingPage(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {            
            this.m_CuttingPage = new CuttingPage(aliquotOrder, testOrder, this.m_AccessionOrder, this.m_HistologySlidePaperLabelPrinter, this.m_CuttingWorkspaceWindow.PageNavigator);
            this.m_CuttingPage.Finished += new CuttingPage.FinishedEventHandler(CuttingPage_Finished);            
            this.m_CuttingPage.ShowTestOrderSelectionPage += new CuttingPage.ShowTestOrderSelectionPageEventHandler(CuttingPage_ShowTestOrderSelectionPage);
            //this.m_CuttingPage.PageTimedOut += new CuttingPage.PageTimedOutEventHandler(PageTimedOut);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(this.m_CuttingPage);
        }

        private void PageTimedOut(object sender, EventArgs eventArgs)
        {
            this.ShowScanSecurityBadgePage();
        }

        private void CuttingPage_ShowTestOrderSelectionPage(object sender, CustomEventArgs.AliquotOrderReturnEventArgs eventArgs)
        {
            this.ShowTestOrderSelectionPage(eventArgs.AliquotOrder);
        }                

        private void CuttingPage_Finished(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.ShowScanAliquotPage(eventArgs.MasterAccessionNo);
            this.m_CuttingPage = null;
        }       
    }
}
