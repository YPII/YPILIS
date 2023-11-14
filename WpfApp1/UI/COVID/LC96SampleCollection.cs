using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace YellowstonePathology.UI.COVID
{
    public class LC96SampleCollection : ObservableCollection<LC96Sample>
    {        
        public LC96SampleCollection()
        {
            
        }        

        public bool Exists(string position)
        {
            bool result = false;
            foreach (LC96Sample covidSample in this)
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
            foreach (LC96Sample covidSample in this)
            {
                if (covidSample.SampleName == sampleName)
                {
                    result = true;
                }
            }
            return result;
        }

        public LC96SampleCollection GetAllCOV2()
        {
            LC96SampleCollection result = new LC96SampleCollection();
            foreach(LC96Sample covidSample in this)
            {
                if(covidSample.GeneName == "SARS-CoV-2")
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
                LC96Sample covidSample = new LC96Sample();
                this.Add(covidSample);
            }            
        }

        public static LC96SampleCollection GetTemplate()
        {
            SampleMapRows rows = new SampleMapRows();
            LC96SampleCollection result = new LC96SampleCollection();            
            AddSamples(result, rows);
            SetControls(result);
            return result;
        }

        public LC96Sample GetSampleByPosition(string position)
        {
            LC96Sample result = null;
            foreach(LC96Sample sample in this)
            {
                if(sample.Position == position)
                {
                    result = sample;
                    break;
                }
            }
            return result;
        }

        public static void SetControls(LC96SampleCollection covidSampleCollection)
        {
            LC96Sample a1 = covidSampleCollection.GetSampleByPosition("A1");
            a1.GeneName = "SARS-CoV-2";
            a1.SampleName = "Control A1";
            a1.Control.GeneName = "IC RNaseP";
            a1.Control.SampleName = "Control A1";

            LC96Sample b1 = covidSampleCollection.GetSampleByPosition("B1");
            b1.GeneName = "SARS-CoV-2";
            b1.SampleName = "Control B1";
            b1.Control.GeneName = "IC RNaseP";
            b1.Control.SampleName = "Control B1";            
        }        

        public static void AddSamples(LC96SampleCollection covidSampleCollection, SampleMapRows rows)
        {
            int currentColumn = 1;
            int currentRow = 1;

            for (int i = 1; i < 97; i++)
            {                
                string row = rows[currentRow-1];
                LC96Sample sample = new LC96Sample()
                {
                    Color = "255;66;41;24",
                    Position = $"{row}{currentColumn.ToString()}",
                    GeneName = "SARS-CoV-2",
                    Dye = "FAM",
                    SampleType = "Nasal swab",
                    Number = i.ToString()
                };

                LC96Sample control = new LC96Sample()
                {
                    Color = "255;66;41;24",
                    Position = $"{row}{currentColumn.ToString()}",
                    GeneName = "IC RNaseP",
                    Dye = "Cy5",
                    SampleType = "Nasal swab",
                    Number = i.ToString()
                };

                sample.Control = control;
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
                        this[i].ConditionName = patientName;
                        this[i].Control.SampleName = reportNo;
                        this[i].Control.ConditionName = patientName;
                        break;
                    }
                }
            }            
        }

        public void ExportLC96Samples()
        {
            COVIDCaseCollection.HandleFolderStructure();
            string fileName = $"{COVIDCaseCollection.LC96A_SAMPLE_FILES_DIR}sample_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.csv";
            StringBuilder data = new StringBuilder();            

            List<string> propertyList = new List<string>();

            data.AppendLine(GetLC96Headers());
            foreach (LC96Sample covidSample in this)
            {
                if(string.IsNullOrEmpty(covidSample.SampleName) == false)
                {
                    data.AppendLine(covidSample.GetLC96CSV());
                    data.AppendLine(covidSample.GetControlCSV());
                }                
            }
            System.IO.File.WriteAllText(fileName, data.ToString());
        }

        public static LC96SampleCollection Import(string fileName)
        {
            LC96SampleCollection result = new LC96SampleCollection();

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
                    LC96Sample covidSample = new LC96Sample();
                    List<PropertyInfo> propertyList = LC96Sample.GetLC96PropertyList();
                    foreach (PropertyInfo property in propertyList)
                    {
                        Business.Persistence.MappingAttribute mappedProperty = (Business.Persistence.MappingAttribute)property.GetCustomAttributes(typeof(Business.Persistence.MappingAttribute), false)[0];
                        int index = Array.IndexOf(headers, mappedProperty.Name);
                        string propValue = commaSplit[index];
                        property.SetValue(covidSample, propValue.Trim());
                    }
                    result.Add(covidSample);
                }
            }
            return result;
        }

        public static void FillSampleCollection(LC96SampleCollection original, LC96SampleCollection imported)
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

        public static string GetLC96Headers()
        {
            StringBuilder result = new StringBuilder();
            List<PropertyInfo> propertyList = LC96Sample.GetLC96PropertyList();
            foreach (PropertyInfo property in propertyList)
            {
                Business.Persistence.MappingAttribute mappedProperty = (Business.Persistence.MappingAttribute)property.GetCustomAttributes(typeof(Business.Persistence.MappingAttribute), false)[0];
                result.Append(mappedProperty.Name + ",");
            }
            return result.ToString().Remove(result.Length - 1, 1);
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
