using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace YellowstonePathology.UI.COVID
{
    public class COVIDResultCollection : ObservableCollection<COVIDResult>
    {        
        public COVIDResultCollection()
        {

        }

        public static void UpdateLC480Results(COVIDResultCollection covidResultCollection, System.Windows.Window context)
        {
            foreach (COVIDResult covidResult in covidResultCollection)
            {
                if(covidResult.Excluded == "Unchecked" && covidResult.CombinedResult != "Invalid")
                {
                    string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(covidResult.SampleName);
                    if (string.IsNullOrEmpty(masterAccessionNo) != true)
                    {
                        Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, context);
                        Business.Test.SARSCoV2.SARSCoV2TestOrder sarscov2TestOrder = (Business.Test.SARSCoV2.SARSCoV2TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(covidResult.SampleName);
                        if (sarscov2TestOrder.Final == false)
                        {
                            switch (covidResult.CombinedResult)
                            {
                                case "Negative":
                                    sarscov2TestOrder.Result = "NOT DETECTED";
                                    sarscov2TestOrder.Finish(accessionOrder);
                                    break;
                                case "Positive":
                                    sarscov2TestOrder.Result = "DETECTED";
                                    break;
                                default:
                                    sarscov2TestOrder.Result = covidResult.CombinedResult;
                                    break;
                            }
                        }
                        Business.Persistence.DocumentGateway.Instance.Push(context);
                    }
                }                
            }
        }

        public COVIDResultCollection GetAllCOV2()
        {
            COVIDResultCollection result = new COVIDResultCollection();
            foreach (COVIDResult covidResult in this)
            {
                if (covidResult.GeneName == "SARS-CoV-2")
                {
                    result.Add(covidResult);
                }
            }
            return result;
        }

        public static COVIDResultCollection Import(string fileName)
        {            
            string text = System.IO.File.ReadAllText(fileName);
            string[] newLine = new string[1] { "\r\n" };
            string[] lineSplit = text.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

            COVIDResultCollection result = new COVIDResultCollection();

            bool headersAssigned = false;
            string[] headers = null;

            foreach (string line in lineSplit)
            {
                string[] tabSplit = line.Split('\t');
                if (headersAssigned == false)
                {
                    headers = tabSplit;
                    headersAssigned = true;
                }
                else
                {
                    COVIDResult covidResult = new COVIDResult();
                    List<PropertyInfo> propertyList = covidResult.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.MappingAttribute))).ToList();
                    foreach (PropertyInfo property in propertyList)
                    {
                        Business.Persistence.MappingAttribute mappedProperty = (Business.Persistence.MappingAttribute)property.GetCustomAttributes(typeof(Business.Persistence.MappingAttribute), false)[0];
                        int index = Array.IndexOf(headers, mappedProperty.Name);
                        if(index < tabSplit.Count())
                        {
                            string propValue = tabSplit[index];
                            property.SetValue(covidResult, propValue);
                        }
                    }
                    result.Add(covidResult);
                }
            }
            return result;
        }
    }
}
