using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleEndoCervical2 : HPVRule
    {
        public HPVRuleEndoCervical2()
        {
            this.m_Description = "Absent";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrep = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrep.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrep.PanelSetId);
                if (panelSetOrderCytology.Final == true)
                {
                    if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNILM(panelSetOrderCytology.ResultCode) == true ||
                        YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeReactive(panelSetOrderCytology.ResultCode) == true)
                    {
                        if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeTZoneAbsent(panelSetOrderCytology.ResultCode) == true)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
    }
}
