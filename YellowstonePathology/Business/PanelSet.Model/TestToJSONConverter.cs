using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class TestToJSONConverter
    {
        public TestToJSONConverter() { }

        public string Convert()
        {
            StringBuilder result = new StringBuilder();

            PanelSet test = PanelSetCollection.GetAll().GetPanelSet(1);

            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string jString = JsonConvert.SerializeObject(test, Newtonsoft.Json.Formatting.Indented, camelCaseFormatter);
            JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
            jObject.Remove("universalServiceIdCollection");
            jObject.Property("reportAsAdditionalTesting").AddAfterSelf(new JArray("universalServiceIds"));

            return result.ToString();
        }

        private string GetUniversalServiceIdInfo(YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServices)
        {
            string result = "  universalServiceIds: [";
            if(universalServices.Count > 0)
            {
                foreach(ClientOrder.Model.UniversalService universalService in universalServices)
                {
                    result = result + Environment.NewLine + "    \"universalServiceId\": \"" + universalService.UniversalServiceId + "\",";
                }
                result = result.Substring(0, result.Length - 1);
                result += Environment.NewLine + "  ";

            }
            result += "],";
            return result;
        }

        private void GetAliquotToAddOnOrderId(YellowstonePathology.Business.Specimen.Model.Aliquot aliquot)
        {

        }

        private void GetOrderTargetTypeExclusionInfo(YellowstonePathology.Business.OrderTargetTypeCollection orderTargetTypeExclusions)
        {

        }

        private void GetOrderTargetTypeRestrictionInfo(YellowstonePathology.Business.OrderTargetTypeCollection orderTargetTypenRestrictions)
        {

        }

        private void GetPanelCollectionInfo(YellowstonePathology.Business.Panel.Model.PanelCollection panelCollection)
        {

        }

        private void GetFacilityId(YellowstonePathology.Business.Facility.Model.Facility facility)
        {

        }

        private void GetTaskInfo(YellowstonePathology.Business.Task.Model.TaskCollection tasks)
        {

        }

        private void GetPanelSetCptCodeInfo(YellowstonePathology.Business.Billing.Model.PanelSetCptCodeCollection panelSetCptCodes)
        {

        }




/*        panelSetCptCodeCollection - id and quantity for each cptcode
technicalComponentFacility ids only
professionalComponentFacility - ids only
technicalComponentBillingFacility -  ids only
professionalComponentBillingFacility -  ids only

reportNoLetter - letter only

panelCollection - */

    }
}
