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

        public string Convert(PanelSet test)
        {
            string result = string.Empty;

            JsonSerializerSettings camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string jString = JsonConvert.SerializeObject(test, Newtonsoft.Json.Formatting.Indented, camelCaseFormatter);
            JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
            this.GetPanelSetType(jObject, test);
            this.GetReportNumberLetterInfo(jObject);
            this.GetUniversalServiceIdInfo(jObject);
            //this.GetAliquotToAddOnOrderId(jObject);
            this.GetOrderTargetTypeExclusionInfo(jObject);
            this.GetOrderTargetTypeRestrictionInfo(jObject);
            this.GetPanelCollectionInfo(jObject);
            this.GetFacilityInfo(jObject, "professionalComponentFacility", "professionalComponentFacilityId");
            this.GetFacilityInfo(jObject, "technicalComponentFacility", "technicalComponentFacilityId");
            this.GetFacilityInfo(jObject, "professionalComponentBillingFacility", "professionalComponentBillingFacilityId");
            this.GetFacilityInfo(jObject, "technicalComponentBillingFacility", "technicalComponentBillingFacilityId");
            //this.GetTaskInfo(jObject);
            this.GetPanelSetCptCodeInfo(jObject);
            result = JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented, camelCaseFormatter);
            return result;
        }

        private void GetPanelSetType(JObject jObject, PanelSet test)
        {
            string testType = "PanelSet";
            if (test is PanelSetMolecularTest)
            {
                testType = "PanelSetMolecularTest";
            }
            if (test is PanelSetFlowCytometry)
            {
                testType = "PanelSetFlowCytometry";
            }
            if (test is Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest)
            {
                testType = "ErPrSemiQuantitativeTest";
            }
            if (test is Test.MPNExtendedReflex.MPNExtendedReflexTest)
            {
                testType = "MPNExtendedReflexTest";
            }
            if (test is Test.MPNStandardReflex.MPNStandardReflexTest)
            {
                testType = "MPNStandardReflexTest";
            }
            if (test is Test.Surgical.SurgicalTest)
            {
                testType = "SurgicalTest";
            }

            jObject.Add("testType", testType);
        }

        private void GetReportNumberLetterInfo(JObject jObject)
        {
            JToken reportNo = jObject["reportNoLetter"];
            string letter = null;
            if (reportNo.HasValues == true)
            {
                letter = reportNo["letter"].ToString();
            }
            jObject.Add("reportLetter", letter);
            jObject.Remove("reportNoLetter");
        }

        private void GetUniversalServiceIdInfo(JObject jObject)
        {
            JArray universalServiceIds = new JArray();
            JArray universalServiceIdCollection = jObject["universalServiceIdCollection"] as JArray;
            foreach (JObject universalService in universalServiceIdCollection)
            {
                string id = universalService["universalServiceId"].ToString();
                universalServiceIds.Add(id);
            }
            jObject.Add("universalServiceIds", universalServiceIds);
            jObject.Remove("universalServiceIdCollection");
        }

        private void GetAliquotToAddOnOrderId(JObject jObject)
        {
            JToken aliquot = jObject["aliquotToAddOnOrder"];
            string aliquotType = null;
            if(aliquot.HasValues == true)
            {
                aliquotType = aliquot["aliquotType"].ToString();
            }
            JProperty aliquotIdToAdd = new JProperty("aliquotIdToAdd", aliquotType);
            jObject.Remove("aliquotToAddOnOrder");
            jObject.Add(aliquotIdToAdd);
        }

        private void GetOrderTargetTypeExclusionInfo(JObject jObject)
        {
            JArray ids = new JArray();
            JArray collection = jObject["orderTargetTypeCollectionExclusions"] as JArray;
            foreach (JObject jObj in collection)
            {
                string id = jObj["typeId"].ToString();
                ids.Add(id);
            }
            jObject.Add("orderTargetTypeExclusionIds", ids);
            jObject.Remove("orderTargetTypeCollectionExclusions");
        }

        private void GetOrderTargetTypeRestrictionInfo(JObject jObject)
        {
            JArray ids = new JArray();
            JArray collection = jObject["orderTargetTypeCollectionRestrictions"] as JArray;
            foreach (JObject jObj in collection)
            {
                string id = jObj["typeId"].ToString();
                ids.Add(id);
            }
            jObject.Add("orderTargetTypeRestrictionIds", ids);
            jObject.Remove("orderTargetTypeCollectionRestrictions");
        }

        private void GetPanelCollectionInfo(JObject jObject)
        {
            JArray ids = new JArray();
            JArray collection = jObject["panelCollection"] as JArray;
            foreach (JObject jObj in collection)
            {
                JToken id = jObj["panelId"];
                ids.Add(id);
            }
            jObject.Add("panelIds", ids);
            jObject.Remove("panelCollection");
        }

        private void GetFacilityInfo(JObject jObject, string removePropertyName, string addPropertyName)
        {
            JToken facility = jObject[removePropertyName];
            string facilityId = null;
            if(facility.HasValues == true)
            {
                facilityId = facility["facilityId"].ToString();
            }
            jObject.Add(addPropertyName, facilityId);
            jObject.Remove(removePropertyName);
        }

        private void GetTaskInfo(JObject jObject)
        {
            JArray tasks = new JArray();
            JArray collection = jObject["taskCollection"] as JArray;
            foreach (JObject jObj in collection)
            {
                string id = jObj["assignedTo"].ToString();
                string desc = jObj["description"].ToString();
                JObject task = new JObject();
                task.Add("assignTo", id);
                task.Add("description", desc);
                tasks.Add(task);
            }
            jObject.Add("tasks", tasks);
            jObject.Remove("taskCollection");
        }

        private void GetPanelSetCptCodeInfo(JObject jObject)
        {
            JArray ids = new JArray();
            JArray collection = jObject["panelSetCptCodeCollection"] as JArray;
            foreach (JObject jObj in collection)
            {
                JToken jCPT = jObj["cptCode"];
                string code = jCPT["code"].ToString();
                int qty = (int)jObj["quantity"];
                JToken mod = jObj["modifier"];
                string modifier = null;
                if (mod != null)
                {
                    modifier = mod.ToString();
                }
                JObject cpt = new JObject();
                cpt.Add("cptCode", code);
                cpt.Add("quantity", qty);
                cpt.Add("modifier", modifier);
                ids.Add(cpt);
            }
            jObject.Add("panelSetCptCodes", ids);
            jObject.Remove("panelSetCptCodeCollection");
        }
    }
}
