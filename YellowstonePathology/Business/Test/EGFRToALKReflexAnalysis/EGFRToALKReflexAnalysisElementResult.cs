using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisElementResult
    {
        protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;

        protected string m_TestAbbreviation;
        protected bool m_Ordered;
        protected bool m_Final;
        protected string m_ResultAbbreviation;
        protected EGFRToALKReflexAnalysisElementStatusEnum m_Status;
        

        public EGFRToALKReflexAnalysisElementResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, int panelSetId)
        {
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            this.m_PanelSet = panelSetCollection.GetPanelSet(panelSetId);

            this.m_TestAbbreviation = this.m_PanelSet.Abbreviation;
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetId) == true)
            {                
                this.m_Ordered = true;
                this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.Ordered;

                this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetId);
                this.m_Final = this.m_PanelSetOrder.Final;

                if (this.m_PanelSetOrder.Accepted == true) this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.Accepted;
                if (this.m_PanelSetOrder.Final == true) this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.Final;
            }
            else
            {
                this.m_Ordered = false;
                this.m_Final = false;
                this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.NotOrdered;
            }
        }

        public string TestAbbreviation
        {
            get { return this.m_TestAbbreviation; }
        }

        public bool Ordered
        {
            get { return this.m_Ordered; }
        }

        public bool Final
        {
            get { return this.m_Final; }
        }

        public string ResultAbbreviation
        {
            get { return this.m_ResultAbbreviation; }
        }

        public EGFRToALKReflexAnalysisElementStatusEnum Status
        {
            get { return this.m_Status; }
        }
    }
}
