using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PanelSetFactory
    {
        public PanelSetFactory() { }


        public static PanelSet FromJson(string jString)
        {
            PanelSet result = null;

            JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
            string panelSetType = jObject["testType"].ToString();
            switch(panelSetType)
            {
                case "PanelSetMolecularTest":
                    PanelSetMolecularTest molecular = JsonConvert.DeserializeObject<Business.PanelSet.Model.PanelSetMolecularTest>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    result = molecular;
                    break;
                case "PanelSetFlowCytometry":
                    PanelSetFlowCytometry flow = JsonConvert.DeserializeObject<Business.PanelSet.Model.PanelSetFlowCytometry>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    result = flow;
                    break;
                case "ErPrSemiQuantitativeTest":
                    Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest semiQuant = JsonConvert.DeserializeObject<Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    result = semiQuant;
                    break;
                case "MPNExtendedReflexTest":
                    Test.MPNExtendedReflex.MPNExtendedReflexTest mpnExtended = JsonConvert.DeserializeObject<Test.MPNExtendedReflex.MPNExtendedReflexTest>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    result = mpnExtended;
                    break;
                case "MPNStandardReflexTest":
                    Test.MPNStandardReflex.MPNStandardReflexTest mpnStandard = JsonConvert.DeserializeObject<Test.MPNStandardReflex.MPNStandardReflexTest>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    result = mpnStandard;
                    break;
                case "SurgicalTest":
                    Test.Surgical.SurgicalTest surgical = JsonConvert.DeserializeObject<Test.Surgical.SurgicalTest>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    result = surgical;
                    break;
                default:
                    result = JsonConvert.DeserializeObject<PanelSet>(jString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    });
                    break;
            }

            HandleInternalObjects(result, jObject);

            return result;
        }

        private static void HandleInternalObjects(PanelSet test, JObject jObject)
        {
            SetReportNumberLetterInfo(test, jObject);
            SetUniversalServiceIdInfo(test, jObject);
            SetOrderTargetTypeExclusionInfo(test, jObject);
            SetOrderTargetTypeRestrictionInfo(test, jObject);

            SetPanelSetCptCodeInfo(test, jObject);
        }

        private static void SetReportNumberLetterInfo(PanelSet test, JObject jObject)
        {
            JToken reportNo = jObject["reportLetter"];
            string reportLetter = jObject["reportLetter"].ToString();
            Business.ReportNoLetter reportNoLetter = ReportNoLetterCollection.Instance.GetByLetter(reportLetter);
            test.ReportNoLetter = reportNoLetter;
        }

        private static void SetUniversalServiceIdInfo(PanelSet test, JObject jObject)
        {
            ClientOrder.Model.UniversalServiceCollection universalServiceCollection = ClientOrder.Model.UniversalServiceCollection.GetAll();
            JArray universalServiceIds = jObject["universalServiceIds"] as JArray;
            foreach (JObject universalServiceId in universalServiceIds)
            {
                string id = universalServiceId.ToString();
                ClientOrder.Model.UniversalService universalService = universalServiceCollection.GetByUniversalServiceId(id);
                test.UniversalServiceIdCollection.Add(universalService);
            }
        }

        /*private static void SetAliquotToAddOnOrderId(PanelSet test, JObject jObject)
        {
            JToken aliquot = jObject["aliquotIdToAdd"];
            if (aliquot.HasValues == true)
            {
                string aliquotType = null;
                aliquotType = aliquot.ToString();
                switch(aliquotType)
                {
                    case "Block":
                        test.AliquotToAddOnOrder = new Specimen.Model.Block();
                        break;
                    case "CellBlock":
                        test.AliquotToAddOnOrder = new Specimen.Model.CellBlock();
                        break;
                    case "CESlide":
                        test.AliquotToAddOnOrder = new Specimen.Model.CESlide();
                        break;
                    case "FNASlide":
                        test.AliquotToAddOnOrder = new Specimen.Model.FNASlide();
                        break;
                    case "FrozenBlock":
                        test.AliquotToAddOnOrder = new Specimen.Model.FrozenBlock();
                        break;
                    case "NGYNSlide":
                        test.AliquotToAddOnOrder = new Specimen.Model.NGYNSlide();
                        break;
                    case "PantherAliquot":
                        test.AliquotToAddOnOrder = new Specimen.Model.PantherAliquot();
                        break;
                    case "Slide":
                        test.AliquotToAddOnOrder = new Specimen.Model.Slide();
                        break;
                    case "SpecimenAliquot":
                        test.AliquotToAddOnOrder = new Specimen.Model.SpecimenAliquot();
                        break;
                    case "ThinPrepSlide":
                        test.AliquotToAddOnOrder = new Specimen.Model.ThinPrepSlide();
                        break;
                    case "WashAliquot":
                        test.AliquotToAddOnOrder = new Specimen.Model.WashAliquot();
                        break;
                }
            }
        }*/

        private static void SetOrderTargetTypeExclusionInfo(PanelSet test, JObject jObject)
        {
            JArray collection = jObject["orderTargetTypeExclusionIds"] as JArray;
            foreach (JObject jObj in collection)
            {
                string id = jObj["typeId"].ToString();
                Specimen.Model.Specimen specimen = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen(id);
                test.OrderTargetTypeCollectionExclusions.Add(specimen);
            }
        }

        private static void SetOrderTargetTypeRestrictionInfo(PanelSet test, JObject jObject)
        {
            JArray collection = jObject["orderTargetTypeRestrictionIds"] as JArray;
            foreach (JObject jObj in collection)
            {
                string id = jObj["typeId"].ToString();
                Specimen.Model.Specimen specimen = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen(id);
                test.OrderTargetTypeCollectionRestrictions.Add(specimen);
            }
        }

        private static void SetPanelCollectionInfo(PanelSet test, JObject jObject)
        {
            JArray collection = jObject["panelIds"] as JArray;
            foreach (JObject jObj in collection)
            {
                int id = (int)jObj["panelId"];
                Panel.Model.Panel panel = Panel.Model.PanelCollection.GetAll().GetPanel(id);
            }
        }

        private static void GetFacilityInfo(PanelSet test, JObject jObject, string removePropertyName, string addPropertyName)
        {
            JToken facility = jObject[removePropertyName];
            string facilityId = null;
            if (facility.HasValues == true)
            {
                facilityId = facility["facilityId"].ToString();
            }
            jObject.Add(addPropertyName, facilityId);
            jObject.Remove(removePropertyName);
        }

        private static void GetTaskInfo(PanelSet test, JObject jObject)
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

        private static void SetPanelSetCptCodeInfo(PanelSet test, JObject jObject)
        {
            JArray collection = jObject["panelSetCptCodes"] as JArray;
            foreach (JObject jObj in collection)
            {
                JToken jCPT = jObj["cptCode"];
                string code = jCPT["code"].ToString();
                int quantity = (int)jObj["quantity"];
                string modifier = jObj["modifier"].ToString();
                Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone(code, modifier), quantity);
                test.PanelSetCptCodeCollection.Add(panelSetCptCode);
            }
        }
    }
}
