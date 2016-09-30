using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class AmendmentUI
    {
        YellowstonePathology.Business.Amendment.Model.Amendment m_SelectedAmendment;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public AmendmentUI(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
        }        

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection AmendmentCollection
        {
            get { return this.PanelSetOrder.AmendmentCollection; }
        }

        public YellowstonePathology.Business.Amendment.Model.Amendment SelectedAmendment
        {
            get { return this.m_SelectedAmendment; }
            set
            {
                this.m_SelectedAmendment = value;
            }
        }

        public string ReportNo
        {
            get { return this.m_PanelSetOrder.ReportNo; }
        }

        public int AssignedToId
        {
            get { return this.m_PanelSetOrder.AssignedToId; }
        }

		public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return YellowstonePathology.Business.User.SystemIdentity.Instance; }
        }
    }
}
