using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ProstateBiopsyKitCollection : ObservableCollection<GeneralDataCassetteProstate>
    {
        //string m_RightColor = "White";
        //string m_LeftColor = "Pink";        

        List<string> m_Descriptions;

        public ProstateBiopsyKitCollection()
        {
            this.m_Descriptions = new List<string>();
            this.m_Descriptions.Add("RT BASE");
            this.m_Descriptions.Add("RT LAT BASE");
            this.m_Descriptions.Add("RT MID");
            this.m_Descriptions.Add("RT LAT MID");
            this.m_Descriptions.Add("RT APEX");
            this.m_Descriptions.Add("RT LAT APEX");
            this.m_Descriptions.Add("LT BASE");
            this.m_Descriptions.Add("LT LAT BASE");
            this.m_Descriptions.Add("LT MID");
            this.m_Descriptions.Add("LT LAT MID");
            this.m_Descriptions.Add("LT APEX");
            this.m_Descriptions.Add("LT LAT APEX");

            foreach (string description in this.m_Descriptions)
            {
                string color = null;
                if(description.StartsWith("LT") == true)
                {
                    color = "Pink";
                } 
                else if(description.StartsWith("RT") == true)
                {
                    color = "Lilac";
                }
                this.Add(new GeneralDataCassetteProstate(color, description));
            }        
        }
    }
}
