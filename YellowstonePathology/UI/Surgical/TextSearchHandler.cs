using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Surgical
{
    public class TextSearchHandler
    {
        private string m_SearchText;        

        public TextSearchHandler(string searchText)
        {
            this.m_SearchText = searchText;            
        }

        public object GetSearchObject()
        {
            object result = null;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_SearchText);
			if(orderIdParser.IsValidReportNo == true)
            {
                result = new YellowstonePathology.Business.ReportNo(this.m_SearchText);                
			}
            else if(orderIdParser.IsValidMasterAccessionNo == true)
            {
                result = YellowstonePathology.Business.MasterAccessionNo.Parse(this.m_SearchText, true);
            }
            else
            {
                YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = null;
                if (YellowstonePathology.Business.MasterAccessionNo.TryParse(this.m_SearchText, false, out masterAccessionNo) == true)
                {
                    result = masterAccessionNo;
                }
                else
                {
                    YellowstonePathology.Business.PatientName patientName = null;
                    if (YellowstonePathology.Business.PatientName.TryParse(this.m_SearchText, out patientName) == true)
                    {
                        result = patientName;
                    }
                }
            }
            
            return result;
        }

        private bool IsInt(string text)
        {
            int parseResult = 0;
            bool result = int.TryParse(text, out parseResult);
            return result;
        }        
    }
}
