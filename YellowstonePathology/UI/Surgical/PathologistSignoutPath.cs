using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Surgical
{
    public class PathologistSignoutPath
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;

        private List<Action> m_ActionList;
        private int m_ActionIndex;
        private List<string> m_AuditMessages;
        private List<string> m_ColonCancerMessages;

        private YellowstonePathology.Business.Audit.Model.PathologistSignoutAuditCollection m_PathologistSignoutAuditCollection;
        private YellowstonePathology.Business.Audit.Model.AuditResult m_AuditResult;
        private YellowstonePathology.Business.Audit.Model.AuditResult m_MessageAuditResult;
        private PathologistSignoutDialog m_PathologistSignoutDialog;
        private PQRSSignoutPage m_PQRSSignoutPage;

        private System.Windows.Visibility m_BackButtonVisibility;
        private System.Windows.Visibility m_NextButtonVisibility;


        public PathologistSignoutPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;

            this.m_PathologistSignoutAuditCollection = new Business.Audit.Model.PathologistSignoutAuditCollection(this.m_AccessionOrder);

            this.m_AuditResult = this.m_PathologistSignoutAuditCollection.Run2();
            if (this.m_AuditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.SetActionList();
            }
        }

        public void Start()
        {
            if (this.m_AuditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_PathologistSignoutDialog = new PathologistSignoutDialog();
                this.m_ActionIndex = 0;
                this.m_ActionList[0].Invoke();
                this.m_PathologistSignoutDialog.ShowDialog();
            }
        }

        public YellowstonePathology.Business.Audit.Model.AuditResult PathologistSignOutAudit
        {
            get { return this.m_AuditResult; }
        }

        public YellowstonePathology.Business.Audit.Model.AuditResult IsPathologistSignoutAuditSuccessful()
        {
            YellowstonePathology.Business.Audit.Model.PathologistSignoutIsHandledAuditCollection isPathologistSignoutHandledAuditCollection = new Business.Audit.Model.PathologistSignoutIsHandledAuditCollection(this.m_AccessionOrder);
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = isPathologistSignoutHandledAuditCollection.Run2();
            return auditResult;
        }

        private void SetActionList()
        {
            this.m_ActionList = new List<Action>();
            this.m_AuditMessages = new List<string>();
            this.m_ColonCancerMessages = new List<string>();
            this.m_MessageAuditResult = new Business.Audit.Model.AuditResult();
            this.m_MessageAuditResult.Status = Business.Audit.Model.AuditStatusEnum.OK;
            this.m_MessageAuditResult.Message = string.Empty;

            foreach (YellowstonePathology.Business.Audit.Model.Audit audit in this.m_PathologistSignoutAuditCollection)
            {
                audit.Run();
            }

            Business.Audit.Model.AuditCollection auditMessageCollection = this.m_PathologistSignoutAuditCollection.GetAuditMessageCollection();
            foreach (YellowstonePathology.Business.Audit.Model.Audit audit in auditMessageCollection)
            {
                if (audit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    this.m_AuditMessages.Add(audit.Message.ToString().Trim());
                    this.m_MessageAuditResult.Status = audit.Status;
                    if (this.m_ActionList.Contains(HandleAuditMessages) == false)
                    {
                        this.m_ActionList.Add(HandleAuditMessages);
                    }
                }
            }

            Business.Audit.Model.NonASCIICharacterAudit nonASCIICharacterAudit = this.m_PathologistSignoutAuditCollection.GetNonASCIICharacterAudit();
            if(nonASCIICharacterAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_ActionList.Add(HandleNonASCIICharacters);
            }
            Business.Audit.Model.PapCorrelationIsRequiredAudit papCorrelationAudit = this.m_PathologistSignoutAuditCollection.GetPapCorrelationAudit();
            if(papCorrelationAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_ActionList.Add(HandlePapCorrelation);
            }
            Business.Audit.Model.PQRSIsRequiredAudit pqrsIsRequiredAudit = this.m_PathologistSignoutAuditCollection.GetPQRSIsRequiredAudit();
            if(pqrsIsRequiredAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
            {
                this.m_ActionList.Add(HandlePQRS);
            }
            Business.Audit.Model.AuditCollection suggestedTestCollection = this.m_PathologistSignoutAuditCollection.GetSuggestedTestAuditCollection();
            foreach (YellowstonePathology.Business.Audit.Model.Audit audit in suggestedTestCollection)
            {
                if (audit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    this.m_ColonCancerMessages.Add(audit.Message.ToString().Trim());
                    if (this.m_ActionList.Contains(HandleColorectalCancer) == false)
                    {
                        this.m_ActionList.Add(HandleColorectalCancer);
                    }
                }
            }
        }

        private void MoveForward(object sender, EventArgs e)
        {
            this.m_ActionIndex++;
            this.m_ActionList[this.m_ActionIndex].Invoke();
        }

        private void MoveBack(object sender, EventArgs e)
        {
            this.m_ActionIndex--;
            this.m_ActionList[this.m_ActionIndex].Invoke();
        }

        private void CloseDialog(object sender, EventArgs e)
        {
            this.m_PathologistSignoutDialog.Close();
        }

        private void SetWindowButtonVisibility()
        {
            this.m_BackButtonVisibility = System.Windows.Visibility.Visible;
            this.m_NextButtonVisibility = System.Windows.Visibility.Visible;

            if (this.m_ActionIndex == 0)
            {
                this.m_BackButtonVisibility = System.Windows.Visibility.Hidden;
            }

            if(this.m_ActionIndex == this.m_ActionList.Count - 1)
            {
                this.m_NextButtonVisibility = System.Windows.Visibility.Hidden;
            }
        }

        private void HandlePapCorrelation()
        {
            this.SetWindowButtonVisibility();
            PapCorrelationPage papCorrelationPage = new PapCorrelationPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_BackButtonVisibility, this.m_NextButtonVisibility);
            papCorrelationPage.Next += this.MoveForward;
            papCorrelationPage.Back += this.MoveBack;
            papCorrelationPage.Close += this.CloseDialog;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(papCorrelationPage);
        }

        private void HandlePQRS()
        {
            this.SetWindowButtonVisibility();
            Business.Audit.Model.PQRSIsRequiredAudit pqrsIsRequiredAudit = this.m_PathologistSignoutAuditCollection.GetPQRSIsRequiredAudit();
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = pqrsIsRequiredAudit.SurgicalSpecimen;
            YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure = pqrsIsRequiredAudit.PQRSMeasure;
            if (this.m_PQRSSignoutPage == null)
            {
                this.m_PQRSSignoutPage = new PQRSSignoutPage(pqrsMeasure, surgicalSpecimen, this.m_SurgicalTestOrder, this.m_AccessionOrder, this.m_BackButtonVisibility, this.m_NextButtonVisibility);
                this.m_PQRSSignoutPage.Next += this.MoveForward;
                this.m_PQRSSignoutPage.Back +=this.MoveBack;
                this.m_PQRSSignoutPage.Close += this.CloseDialog;
            }
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(this.m_PQRSSignoutPage);
        }

        private void HandleColorectalCancer()
        {
            this.SetWindowButtonVisibility();
            SuggestedAdditionalTestingPage suggestedAdditionalTestingPage = new SuggestedAdditionalTestingPage(this.m_AccessionOrder, this.m_ColonCancerMessages, this.m_BackButtonVisibility, this.m_NextButtonVisibility);
            suggestedAdditionalTestingPage.Next += this.MoveForward;
            suggestedAdditionalTestingPage.Back += this.MoveBack;
            suggestedAdditionalTestingPage.Close += this.CloseDialog;
            suggestedAdditionalTestingPage.OrderTest += SuggestedAdditionalTestingPage_OrderTest;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(suggestedAdditionalTestingPage);
        }

        private void SuggestedAdditionalTestingPage_OrderTest(object sender, CustomEventArgs.PanelSetReturnEventArgs e)
        {
            this.StartReportOrderPath(e.PanelSet);
        }

        private void StartReportOrderPath(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo();
            testOrderInfo.PanelSet = panelSet;
            testOrderInfo.OrderTargetIsKnown = false;

            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PathologistSignoutDialog.PageNavigator, PageNavigationModeEnum.Inline, m_PathologistSignoutDialog);
            reportOrderPath.Finish += ReportOrderPath_Finish;
            reportOrderPath.Start(testOrderInfo);
        }

        private void ReportOrderPath_Finish(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
        {
            this.HandleColorectalCancer();
        }

        private void HandleAuditMessages()
        {
            this.SetWindowButtonVisibility();
            PathologistSignoutAuditMessagePage pathologistSignoutAuditMessagePage = new PathologistSignoutAuditMessagePage(this.m_AuditMessages, this.m_BackButtonVisibility, this.m_NextButtonVisibility);
            pathologistSignoutAuditMessagePage.Next += this.MoveForward;
            pathologistSignoutAuditMessagePage.Back += this.MoveBack;
            pathologistSignoutAuditMessagePage.Close += this.CloseDialog;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(pathologistSignoutAuditMessagePage);
        }

        private void HandleNonASCIICharacters()
        {
            this.SetWindowButtonVisibility();
            NonASCIICharacterCorrectionPage nonASCIICharacterCorrectionPage = new NonASCIICharacterCorrectionPage(this.m_AccessionOrder, this.m_SurgicalTestOrder, this.m_BackButtonVisibility, this.m_NextButtonVisibility);
            nonASCIICharacterCorrectionPage.Next += this.MoveForward;
            nonASCIICharacterCorrectionPage.Back += this.MoveBack;
            nonASCIICharacterCorrectionPage.Close += this.CloseDialog;
            this.m_PathologistSignoutDialog.PageNavigator.Navigate(nonASCIICharacterCorrectionPage);
        }
    }
}
