using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.HL7View
{
    public class ADTMessages
    {
        ObservableCollection<ADTMessage> m_Messages;         

        public ADTMessages()
        {
            this.m_Messages = new ObservableCollection<ADTMessage>();            
        }          
        
        public ObservableCollection<ADTMessage> Messages
        {
            get { return this.m_Messages; }
        }

        public string GetPrimaryInsurance()
        {
            string result = "Not Selected";
            foreach(ADTMessage message in this.m_Messages)
            {
                foreach(IN1 in1 in message.IN1Segments)
                {
                    if(in1.InsuranceName.ToUpper().Contains("MEDICARE"))
                    {
                        result = "Medicare";
                    }
                    else if (in1.InsuranceName.ToUpper().Contains("MEDICAID"))
                    {
                        result = "Medicaid";
                    }
                }
            }
            return result;
        }

        public string GetPrimaryInsuranceV2()
        {
            string result = null;
            List<IN1> in1List = this.GetUniqueIN1Segments();
            
            foreach (IN1 in1 in in1List)
            {
                if(string.IsNullOrEmpty(in1.InsuranceName) == false)
                {
                    result = in1.InsuranceName;
                    break;
                }
            }            
            return result;
        }

        public ObservableCollection<ADTMessage> TakeTop(int count)
        {
            ObservableCollection<ADTMessage> result = new ObservableCollection<ADTMessage>();
            for(int i=0; i<count; i++)
            {
                if(i<this.m_Messages.Count)
                {
                    result.Add(this.Messages[i]);
                }
                else
                {
                    break;
                }                
            }
            return result;
        }

        public void SetCurrentAddress(Business.Test.AccessionOrder accessionOrder)
        {
            var result = this.m_Messages.OrderByDescending(t => t.DateReceived).First();
            accessionOrder.PAddress1 = result.PatientAddress.PAddress1;
            accessionOrder.PAddress2 = result.PatientAddress.PAddress2;
            accessionOrder.PCity = result.PatientAddress.PCity;
            accessionOrder.PState = result.PatientAddress.PState;
            accessionOrder.PZipCode = result.PatientAddress.PZipCode;               
        }        

        public Business.Patient.Model.Address GetPatientAddress()
        {
            Business.Patient.Model.Address result = null;
            foreach(ADTMessage message in this.m_Messages)
            {
                if(string.IsNullOrEmpty(message.PatientAddress.PAddress1) == false)
                {
                    result = message.PatientAddress;
                    break;
                }
            }
            return result;
        }

        public List<IN1> GetUniqueIN1Segments()
        {
            List<IN1> result = new List<IN1>();
            foreach (ADTMessage adtMessage in this.m_Messages)
            {
                foreach(Business.HL7View.IN1 in1 in adtMessage.IN1Segments)
                {
                    if(string.IsNullOrEmpty(in1.InsuranceName) == false)
                    {
                        if(!result.Exists(item => item.InsuranceName == in1.InsuranceName))
                        {
                            result.Add(in1);
                        }
                    }
                }
            }
            return result;
        }        
    }
}
