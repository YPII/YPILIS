﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class AuthorizationForVerbalTestRequestResultPath : ResultPath
    {
        private AuthorizationForVerbalTestRequestResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestTestOrder m_TestOrder;

        public AuthorizationForVerbalTestRequestResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = (YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest.AuthorizationForVerbalTestRequestTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new AuthorizationForVerbalTestRequestResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += ResultPage_Next;
            this.m_ResultPage.ShowDocument += ResultPage_ShowDocument;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_ShowDocument(object sender, EventArgs e)
        {
            PublishedDocumentResultPath documentPath = new Test.PublishedDocumentResultPath(this.m_TestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
            documentPath.Finish += DocumentPath_Finish;
            documentPath.Start();
        }

        private void DocumentPath_Finish(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
