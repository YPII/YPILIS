using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class WomensHealthProfilePath : Test.ResultPath
    {
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.UI.Test.WomensHealthProfilePage m_WomensHealthProfilePage;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        private System.Windows.Visibility m_BackButtonVisibility;

        public WomensHealthProfilePath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window,
            System.Windows.Visibility backButtonVisibility) : base(pageNavigator, window)
        {            
            this.m_AccessionOrder = accessionOrder;
			this.m_ClientOrder = clientOrder;
            this.m_BackButtonVisibility = backButtonVisibility;
        }

        protected override void ShowResultPage()
        {
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
            this.m_WomensHealthProfilePage = new Test.WomensHealthProfilePage(womensHealthProfileTestOrder, this.m_AccessionOrder, this.m_ClientOrder, this.m_BackButtonVisibility);
			this.m_WomensHealthProfilePage.Finished += new Test.WomensHealthProfilePage.FinishedEventHandler(WomensHealthProfilePage_Finished);
            this.m_WomensHealthProfilePage.Back += new Test.WomensHealthProfilePage.BackEventHandler(WomensHealthProfilePage_Back);
            this.m_PageNavigator.Navigate(this.m_WomensHealthProfilePage);
        }

        private void WomensHealthProfilePage_Finished(object sender, EventArgs e)
        {
            this.Finished();
        }

        private void WomensHealthProfilePage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }
    }
}
