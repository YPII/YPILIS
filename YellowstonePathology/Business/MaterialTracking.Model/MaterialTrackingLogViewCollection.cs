using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class MaterialTrackingLogViewCollection : ObservableCollection<MaterialTrackingLogView>
    {
        public MaterialTrackingLogViewCollection()
        {

        }

        public List<string> GetDistinctMaterialTypes()
        {
            List<string> result = new List<string>();
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {                
                if (result.Contains(materialTrackingLogView.MaterialType) == false)
                {
                    result.Add(materialTrackingLogView.MaterialType);
                }             
            }
            return result;
        }

        public XElement ToSummaryXML()
        {
            XElement result = new XElement("MaterialTrackingLogSummary");

            return result;
        }

        public List<string> GetMasterAccessionNoList()
        {
            List<string> result = new List<string>();
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                if (result.Contains(materialTrackingLogView.MasterAccessionNo) == false)
                {
                    result.Add(materialTrackingLogView.MasterAccessionNo);
                }
            }
            return result;
        }

        public string GetMasterAccessionNoString()
        {
            StringBuilder result = new StringBuilder();
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                string masterAccessionNoString = "'" + materialTrackingLogView.MasterAccessionNo.ToString() + "'";
                if (!result.ToString().Contains(masterAccessionNoString))
                {
                    result.Append(masterAccessionNoString + ",");
                }
            }
            if (result.Length > 1)
            {
                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }

        public bool Exists(string materialTrackingLogId)
        {
            bool result = false;
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                if (materialTrackingLogView.MaterialTrackingLogId == materialTrackingLogId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool MaterialIdExists(string materialId)
        {
            bool result = false;
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                if (materialTrackingLogView.MaterialId == materialId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public int GetAliquotCount(string masterAccessionNo)
        {
            int result = 0;
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                if (materialTrackingLogView.MasterAccessionNo == masterAccessionNo)
                {
                    if (materialTrackingLogView.MaterialType == "Aliquot")
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }

        public int GetMaterialCount(string masterAccessionNo, string materialType)
        {
            int result = 0;
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                if (materialTrackingLogView.MasterAccessionNo == masterAccessionNo)
                {
                    if (materialTrackingLogView.MaterialType == materialType)
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }

        public int GetSlideCount(string masterAccessionNo)
        {
            int result = 0;
            foreach (MaterialTrackingLogView materialTrackingLogView in this)
            {
                if (materialTrackingLogView.MasterAccessionNo == masterAccessionNo)
                {
                    if (materialTrackingLogView.MaterialType == "Slide")
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }
    }
}
