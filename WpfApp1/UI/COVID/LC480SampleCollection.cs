using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace YellowstonePathology.UI.COVID
{
    public class LC480SampleCollection : ObservableCollection<LC480Sample>
    {        
        public LC480SampleCollection()
        {
            
        }        

        public bool Exists(string position)
        {
            bool result = false;
            foreach (LC480Sample covidSample in this)
            {
                if (covidSample.Position == position)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool SampleNameExists(string sampleName)
        {
            bool result = false;
            foreach (LC480Sample covidSample in this)
            {
                if (covidSample.SampleName == sampleName)
                {
                    result = true;
                }
            }
            return result;
        }

        public LC480SampleCollection GetAllCOV2()
        {
            LC480SampleCollection result = new LC480SampleCollection();
            foreach(LC480Sample covidSample in this)
            {
                if(covidSample.FiltComb == "483-533")
                {
                    result.Add(covidSample);
                }
            }
            return result;
        }

        public void Padd()
        {            
            for(int i=this.Count; i<96; i++)
            {
                LC480Sample covidSample = new LC480Sample();
                this.Add(covidSample);
            }            
        }

        public static LC480SampleCollection GetTemplate()
        {
            SampleMapRows rows = new SampleMapRows();
            LC480SampleCollection result = new LC480SampleCollection();            
            AddSamples(result, rows);
            SetControls(result);
            return result;
        }

        public LC480Sample GetSampleByPosition(string position)
        {
            LC480Sample result = null;
            foreach(LC480Sample sample in this)
            {
                if(sample.Position == position)
                {
                    result = sample;
                    break;
                }
            }
            return result;
        }

        public static void SetControls(LC480SampleCollection covidSampleCollection)
        {
            LC480Sample a1 = covidSampleCollection.GetSampleByPosition("A1");
            a1.SampleId = "Control A1";
            a1.SampleName = "Control A1";
            a1.Position = "A1";

            LC480Sample b1 = covidSampleCollection.GetSampleByPosition("B1");
            b1.SampleId = "Control B1";
            b1.SampleName = "Control B1";
            b1.Position = "B1";
        }        

        public static void AddSamples(LC480SampleCollection covidSampleCollection, SampleMapRows rows)
        {
            int currentColumn = 1;
            int currentRow = 1;

            for (int i = 1; i < 97; i++)
            {                
                string row = rows[currentRow-1];
                LC480Sample sample = new LC480Sample()
                {
                    FiltComb = "483-533",
                    Position = $"{row}{currentColumn.ToString()}"
                };

                LC480Sample control = new LC480Sample()
                {
                    FiltComb = "483-533",
                    Position = $"{row}{currentColumn.ToString()}"                    
                };
                
                covidSampleCollection.Add(sample);

                currentRow += 1;
                if(currentRow == 9)
                {
                    currentColumn += 1;
                    currentRow = 1;
                }                
            }
        }

        public void SetNextSampleName(string reportNo, string patientName)
        {
            if(this.SampleNameExists(reportNo) == false)
            {
                for (int i = 8; i < 96; i++)
                {
                    if (string.IsNullOrEmpty(this[i].SampleName) == true)
                    {
                        this[i].SampleName = reportNo;
                        this[i].SampleId = patientName;                        
                        break;
                    }
                }
            }            
        }

        public void ExportSamples()
        {
            COVIDCaseCollection.HandleFolderStructure();
            string fileName = $"{COVIDCaseCollection.LC480A_SAMPLE_FILES_DIR}sample_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.csv";
            StringBuilder data = new StringBuilder();            

            List<string> propertyList = new List<string>();

            data.AppendLine(GetHeaders());
            foreach (LC480Sample covidSample in this)
            {
                if(string.IsNullOrEmpty(covidSample.SampleName) == false)
                {
                    data.AppendLine(covidSample.GetCSV());                    
                }                
            }
            System.IO.File.WriteAllText(fileName, data.ToString());
        }

        public static LC480SampleCollection Import(string fileName)
        {
            LC480SampleCollection result = new LC480SampleCollection();

            string text = System.IO.File.ReadAllText(fileName);
            string[] newLine = new string[1] { "\r\n" };
            string[] lineSplit = text.Split(newLine, StringSplitOptions.RemoveEmptyEntries);            

            bool headersAssigned = false;
            string[] headers = null;

            foreach (string line in lineSplit)
            {
                string[] commaSplit = line.Split(',');
                if (headersAssigned == false)
                {
                    TrimStringArray(commaSplit);
                    headers = commaSplit;
                    headersAssigned = true;
                }
                else
                {
                    LC480Sample covidSample = new LC480Sample();
                    List<PropertyInfo> propertyList = LC480Sample.GetPropertyList();
                    foreach (PropertyInfo property in propertyList)
                    {
                        Business.Persistence.MappingAttribute mappedProperty = (Business.Persistence.MappingAttribute)property.GetCustomAttributes(typeof(Business.Persistence.MappingAttribute), false)[0];
                        int index = Array.IndexOf(headers, $"\"{mappedProperty.Name}\"");
                        string propValue = commaSplit[index];
                        propValue = propValue.Replace("\"", "");
                        property.SetValue(covidSample, propValue.Trim());
                    }
                    result.Add(covidSample);
                }
            }
            return result;
        }

        public static void FillSampleCollection(LC480SampleCollection original, LC480SampleCollection imported)
        {
            for(int i=0; i<original.Count; i++)
            {
                if (imported.Exists(original[i].Position) == true)
                {
                    original[i] = imported.GetSampleByPosition(original[i].Position);
                }
            }
        }

        public static void TrimStringArray(string [] strings)
        {
            for(int i=0; i<strings.Length; i++)
            {
                strings[i] = strings[i].Trim();
            }
        }

        public static string GetHeaders()
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = LC480Sample.GetPropertyList();
            foreach (PropertyInfo property in propertyList)
            {
                Business.Persistence.MappingAttribute mappedProperty = (Business.Persistence.MappingAttribute)property.GetCustomAttributes(typeof(Business.Persistence.MappingAttribute), false)[0];
                result.Append($"\"{mappedProperty.Name}\"\t");
            }
            return result.ToString().Trim();
        }       

        public static LC96SampleCollection Import()
        {
            string fileName = @"c:\temp\lightsample.csv";
            string text = System.IO.File.ReadAllText(fileName);
            string[] newLine = new string[1] { "\r\n" };
            string[] lineSplit = text.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

            LC96SampleCollection result = new LC96SampleCollection();

            bool headersAssigned = false;
            string[] headers = null;

            foreach (string line in lineSplit)
            {
                string[] tabSplit = line.Split(',');
                if (headersAssigned == false)
                {
                    headers = tabSplit;
                    headersAssigned = true;
                }
                else
                {
                    LC96Sample covidSample = new LC96Sample();
                    List<PropertyInfo> propertyList = covidSample.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.MappingAttribute))).ToList();
                    foreach (PropertyInfo property in propertyList)
                    {
                        Business.Persistence.MappingAttribute mappedProperty = (Business.Persistence.MappingAttribute)property.GetCustomAttributes(typeof(Business.Persistence.MappingAttribute), false)[0];
                        int index = Array.IndexOf(headers, mappedProperty.Name);
                        string propValue = tabSplit[index];
                        property.SetValue(covidSample, propValue);
                    }
                    result.Add(covidSample);
                }
            }
            return result;
        }
    }
}
