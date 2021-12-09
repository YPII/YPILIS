using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ProstateBiopsyKitCollection : ObservableCollection<GeneralDataCassette>
    {
        string m_RightColor = "White";
        string m_LeftColor = "Orange";

        List<string> m_Descriptions;

        public ProstateBiopsyKitCollection()
        {
            this.m_Descriptions = new List<string>();
            this.m_Descriptions.Add("Right Base");
            this.m_Descriptions.Add("Right Lateral Base");
            this.m_Descriptions.Add("Right Mid");
            this.m_Descriptions.Add("Right Lateral Mid");
            this.m_Descriptions.Add("Right Apex");
            this.m_Descriptions.Add("Right Lateral Apex");
            this.m_Descriptions.Add("Left Base");
            this.m_Descriptions.Add("Left Lateral Base");
            this.m_Descriptions.Add("Left Mid");
            this.m_Descriptions.Add("Left Lateral Mid");
            this.m_Descriptions.Add("Left Apex");
            this.m_Descriptions.Add("Left Lateral Apex");

            foreach (string description in this.m_Descriptions)
            {
                string color = null;
                if(description.StartsWith("Left") == true)
                {
                    color = "White";
                } 
                else if(description.StartsWith("Right") == true)
                {
                    color = "Orange";
                }
                this.Add(new GeneralDataCassette(color, description));
            }        
        }
    }
}
