using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Container
    {
        string m_ContainerId;
        Nullable<DateTime> m_Shipped;
        Nullable<DateTime> m_Received;        

        public Container(string ethResult)
        {
            ethResult = ethResult.Remove(0, 2);

            string hexShipped = ethResult.Substring(64, 64);
            int seconds = int.Parse(hexShipped, System.Globalization.NumberStyles.HexNumber);
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            this.m_Shipped = dtDateTime.AddSeconds(seconds).ToLocalTime();

            string hexContainerId = ethResult.Substring(128);
            string[] split = this.GetSplit(hexContainerId);
            this.m_ContainerId = FromHex(split).Substring(64, 40);
        }            
        
        private string FromHex(string [] split)
        {
            StringBuilder result = new StringBuilder();
            foreach (string hex in split)
            {                
                int value = Convert.ToInt32(hex, 16);             
                result.Append(Char.ConvertFromUtf32(value));                
            }            
            return result.ToString().Trim();
        }

        private string [] GetSplit(string hexString)
        {
            StringBuilder spaced = new StringBuilder();
            for(int i=0; i<hexString.Length; i++)
            {
                if(i != 0 && i % 2 == 0)
                {
                    spaced.Append(' ');
                }
                spaced.Append(hexString[i]);
            }            
            return spaced.ToString().Split(' ');
        }    

        public string ContainerId
        {
            get { return this.m_ContainerId; }
            set { this.m_ContainerId = value; }
        }

        public Nullable<DateTime> Shipped
        {
            get { return this.m_Shipped; }
            set { this.m_Shipped = value; }
        }

        public Nullable<DateTime> Received
        {
            get { return this.m_Received; }
            set { this.m_Received = value; }
        }        
    }
}
