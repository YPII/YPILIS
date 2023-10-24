using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{    
    public class MasterAccessionNo
    {
        private string m_Value;
        private string m_Year;
        private string m_Number;

        public MasterAccessionNo()
        {
            
        }

        public string Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }

        public string Year
        {
            get { return this.m_Year; }
            set { this.m_Year = value; }
        }

        public string Number
        {
            get { return this.m_Number; }
            set { this.m_Number = value; }
        }

        public static MasterAccessionNo Parse(string masterAccessionNo, bool yearIncluded)
        {
            MasterAccessionNo result = new MasterAccessionNo();

            if (yearIncluded == true)
            {
                result.Value = masterAccessionNo;
            }
            else
            {
                result.Value = DateTime.Today.ToString("yy") + "-" + masterAccessionNo;
            }

            string[] dashSplit = masterAccessionNo.Split('-');
            if (dashSplit.Length == 2)
            {
                result.m_Year = dashSplit[0];
                result.m_Number = dashSplit[1];
            }
            
            return result;            
        }

        public static bool TryParse(string inputString, bool yearIncluded, out MasterAccessionNo masterAccessionNo)
        {
            bool result = false;

            MasterAccessionNo output = new MasterAccessionNo();

            if (yearIncluded == true)
            {
                output.Value = inputString;
                result = true;
            }
            else
            {
                int man = 0;
                if (int.TryParse(inputString, out man) == true)
                {
                    output.Value = DateTime.Today.ToString("yy") + "-" + inputString;
                    result = true;
                }                
            }

            if (result == true)
            {
                string[] dashSplit = inputString.Split('-');
                if (dashSplit.Length == 2)
                {
                    output.m_Year = dashSplit[0];
                    output.m_Number = dashSplit[1];
                }                
            }

            masterAccessionNo = output;
            return result;
        }
    }
}
