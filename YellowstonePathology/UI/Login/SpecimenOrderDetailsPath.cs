using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class SpecimenOrderDetailsPath
	{
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;		
		
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

		public SpecimenOrderDetailsPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			string containerId,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_AccessionOrder = accessionOrder;
            this.m_PageNavigator = pageNavigator;
            this.m_SpecimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(containerId);
		}

        public SpecimenOrderDetailsPath(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_PageNavigator = pageNavigator;
        }
        
		public void Start()
		{
            this.ShowAccessionedSpecimenPage();
        }				

		private void ShowAccessionedSpecimenPage()
		{
            SpecimenOrderDetailsPage specimenOrderDetailsPage = new SpecimenOrderDetailsPage(this.m_AccessionOrder, this.m_SpecimenOrder);
            specimenOrderDetailsPage.Next += new SpecimenOrderDetailsPage.NextEventHandler(SpecimenOrderDetailsPage_Finish);
            specimenOrderDetailsPage.Back += new SpecimenOrderDetailsPage.BackEventHandler(SpecimenOrderDetailsPage_Back);
            this.m_PageNavigator.Navigate(specimenOrderDetailsPage);
		}

        private void SpecimenOrderDetailsPage_Back(object sender, EventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }

        private void SpecimenOrderDetailsPage_Finish(object sender, EventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }		
	}
}
