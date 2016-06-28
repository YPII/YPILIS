using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReflexTesting
{
    public class ReflexTestingPlanStepPanelSet : ReflexTestingPlanStep
    {        
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;

        public ReflexTestingPlanStepPanelSet(string stepId, string stepDescription, YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet) 
            : base(stepId, stepDescription)
        {            
            this.m_PanelSet = panelSet;            
        }        

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
        }
        
        public override void SetStatus(PanelSetOrderCollection panelSetOrderCollection)
        {
            if (panelSetOrderCollection.HasPanelSetBeenOrdered(this.PanelSet.PanelSetId) == false)
            {
                this.m_Ordered = false;
                this.m_StatusMessage = "Not Ordered";
            }
            else
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = panelSetOrderCollection.GetPanelSetOrder(this.PanelSet.PanelSetId);
                this.m_Ordered = true;
                this.m_OrderDate = panelSetOrder.OrderDate;

                if (panelSetOrder.Final == false)
                {
                    this.m_StatusMessage = "Not Finalized";
                }
                else
                {
                    this.m_StatusMessage = "Finaled";
                    this.m_ResultIsFinal = true;
                    this.m_ResultFinalDate = panelSetOrder.FinalDate;
                }
            } 
        }        
    }
}
