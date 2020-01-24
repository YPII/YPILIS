using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.View
{
    public class PanelSetOrderAmendmentView
    {
        private Test.AccessionOrder m_AccessionOrder;
        private Test.PanelSetOrder m_PanelSetOrder;
        //private Amendment.Model.AmendmentCollection m_AmendmentCollection;

        public PanelSetOrderAmendmentView(Test.AccessionOrder accessionOrder, Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
        }

        public Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public Amendment.Model.AmendmentCollection AmendmentCollection
        {
            get
            {
                return this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_PanelSetOrder.ReportNo);
            }
        }
    }
}
