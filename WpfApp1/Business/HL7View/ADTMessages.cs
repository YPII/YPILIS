using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

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

        public static ADTMessages GetPatientNameDOB(string firstName, string lastName, string dateOfBirth)
        {
            ADTMessages result = new ADTMessages();

            return result;
        }

        public static ObservableCollection<ADTMessage> GetADTByPatientNameDOB(string lastName, string firstName, DateTime dateOfBirth)
        {
            ObservableCollection<ADTMessage> result = new ObservableCollection<ADTMessage>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from tblADT where pLastName = @LastName and pFirstName = @FirstName and pBirthDate = @DateOfBirth;";
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ADTMessage adtMessage = new ADTMessage();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(adtMessage, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(adtMessage);
                    }
                }
            }
            return result;
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
            foreach(ADTMessage adtMessage in this.m_Messages)
            {
                //var result = this.m_Messages.OrderByDescending(t => t.DateReceived).First();
                if(string.IsNullOrEmpty(adtMessage.PIDSegment.Address.PAddress1) == false)
                {
                    accessionOrder.PAddress1 = adtMessage.PatientAddress.PAddress1;
                    accessionOrder.PAddress2 = adtMessage.PatientAddress.PAddress2;
                    accessionOrder.PCity = adtMessage.PatientAddress.PCity;
                    accessionOrder.PState = adtMessage.PatientAddress.PState;
                    accessionOrder.PZipCode = adtMessage.PatientAddress.PZipCode;
                    accessionOrder.PPhoneNumberHome = adtMessage.PHomePhone;
                    break;
                }
            }            
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

        public GT1 GetFirstGT1Segment()
        {
            GT1 result = null;
            foreach (ADTMessage adtMessage in this.m_Messages)
            {
                if(adtMessage.GT1Segment != null)
                {
                    result = adtMessage.GT1Segment;
                    break;
                }
            }
            return result;
        }
    }
}
