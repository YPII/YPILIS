using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class CancelTestEventArgs : System.EventArgs
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.UI.Test.IResultPage m_ResultPage;
        //private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private string m_ReasonForCancelation;

        public CancelTestEventArgs(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            string reasonForCancelation,
            YellowstonePathology.UI.Test.IResultPage resultPage //,
            //YellowstonePathology.Business.Persistence.ObjectTracker objectTracker
            )
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_ReasonForCancelation = reasonForCancelation;
            this.m_ResultPage = resultPage;
            //this.m_ObjectTracker = objectTracker;
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.UI.Test.IResultPage ResultPage
        {
            get { return this.m_ResultPage; }
        }

        public string ReasonForCancelation
        {
            get { return this.m_ReasonForCancelation; }
        }

        //public YellowstonePathology.Business.Persistence.ObjectTracker ObjectTracker
        //{
        //    get { return this.m_ObjectTracker; }
        //}
    }
}
