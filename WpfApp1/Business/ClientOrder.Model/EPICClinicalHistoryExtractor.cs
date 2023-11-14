using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class EPICClinicalHistoryExtractor
    {
        private List<string> m_ClinicalHistoryList;

        public EPICClinicalHistoryExtractor()
        {
            this.m_ClinicalHistoryList = new List<string>();
            this.m_ClinicalHistoryList.Add("Last Menstrual Period");
            this.m_ClinicalHistoryList.Add("Date of previous PAP");
            this.m_ClinicalHistoryList.Add("Menstrual/Preg History");
            this.m_ClinicalHistoryList.Add("Contraceptive History");
            this.m_ClinicalHistoryList.Add("Cancer History");
            this.m_ClinicalHistoryList.Add("Treatment History");
            this.m_ClinicalHistoryList.Add("Clincial History");
        }

        public bool Exists(string text)
        {
            bool result = false;
            foreach (string str in this.m_ClinicalHistoryList)
            {
                if (text.Contains(str) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public string Normalize(string text)
        {
            string result = text;
            text = text.Replace("->", ": ");
            return text;
        }

        public string ExctractClinicalHistory(string inputText)
        {
            string result = null;
            if (string.IsNullOrEmpty(inputText) == false)
            {
                StringBuilder clinicalHistory = new StringBuilder();
                string[] lines = inputText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (this.Exists(line) == true)
                    {
                        string normalizedLine = this.Normalize(line);
                        clinicalHistory.AppendLine(normalizedLine);
                    }
                }
                result = clinicalHistory.ToString();
            }
            return result;
        }
    }
}
