using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HoldForFlow
{
    public class DirectHarvestForFISHTest : HoldForFlowTest
    {
        public DirectHarvestForFISHTest()
        {
            this.m_PanelSetName = "Direct Harvest For FISH";
            this.m_Abbreviation = "Direct Harvest For FISH";
            this.m_HasTechnicalComponent = true;            
            this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = true;
            this.m_ExpectedDuration = new TimeSpan(5, 0, 0, 0);

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube and 2x5 mL in EDTA tube or " +
                "Bone marrow: 1-2 mL in sodium heparin tube and 2 mL in EDTA tube) and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
        }
    }
}
