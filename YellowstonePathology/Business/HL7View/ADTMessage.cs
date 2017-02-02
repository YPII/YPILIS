using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.HL7View
{
    public class ADTMessage
    {
        List<Business.HL7View.IN1> m_IN1Segments;
        List<Business.HL7View.IN2> m_IN2Segments;

        Business.HL7View.GT1 m_Gt1Segment;
        Business.HL7View.PV1 m_PV1Segment;
        Business.HL7View.PID m_PIDSegment;

        protected string m_MessageId;
        protected DateTime m_DateReceived;
        protected string m_Message;
        protected string m_PLastName;
        protected string m_PFirstName;
        protected DateTime m_PBirthdate;
        protected string m_AccountNo;
        protected string m_MedicalRecordNo;
        protected string m_MessageType;
        protected Business.Patient.Model.Address m_Address;

        public ADTMessage()
        {            
            this.m_IN1Segments = new List<HL7View.IN1>();
            this.m_IN2Segments = new List<HL7View.IN2>();

            this.m_Gt1Segment = new HL7View.GT1();
            this.m_PV1Segment = new PV1();
            this.m_PIDSegment = new PID();
        }

        public PID PIDSegment
        {
            get { return this.m_PIDSegment; }
        }

        public Business.Patient.Model.Address PatientAddress
        {
            get { return this.m_Address; }
        }

        public List<Business.HL7View.IN1> IN1Segments
        {
            get { return this.m_IN1Segments; }
        }

        public List<Business.HL7View.IN2> IN2Segments
        {
            get { return this.m_IN2Segments; }
        }

        public void ParseHL7()
        {            
            string[] lines = this.m_Message.Split('\r');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split('|');
                if (fields[0] == "IN1")
                {
                    Business.HL7View.IN1 in1 = new HL7View.IN1();
                    in1.FromHl7(lines[i], this.m_DateReceived);
                    this.m_IN1Segments.Add(in1);
                }

                if (fields[0] == "IN2")
                {
                    Business.HL7View.IN2 in2 = new HL7View.IN2();
                    in2.FromHl7(lines[i], this.m_DateReceived);
                    this.m_IN2Segments.Add(in2);
                }

                if (fields[0] == "GT1")
                {
                    this.m_Gt1Segment.FromHL7(lines[i]);                    
                }

                if (fields[0] == "PV1")
                {
                    this.m_PV1Segment.FromHL7(lines[i]);
                }

                if (fields[0] == "PID")
                {
                    this.m_PIDSegment.FromHL7(lines[i]);
                    this.m_Address = this.m_PIDSegment.Address;
                }                
            }            
        }        

        [PersistentProperty()]
        public string MessageId
        {
            get { return this.m_MessageId; }
            set { this.m_MessageId = value; }
        }

        [PersistentProperty()]
        public DateTime DateReceived
        {
            get { return this.m_DateReceived; }
            set { this.m_DateReceived = value; }
        }

        [PersistentProperty()]
        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public DateTime PBirthdate
        {
            get { return this.m_PBirthdate; }
            set { this.m_PBirthdate = value; }
        }

        [PersistentProperty()]
        public string AccountNo
        {
            get { return this.m_AccountNo; }
            set { this.m_AccountNo = value; }
        }

        [PersistentProperty()]
        public string MedicalRecordNo
        {
            get { return this.m_MedicalRecordNo; }
            set { this.m_MedicalRecordNo = value; }
        }

        [PersistentProperty()]
        public string MessageType
        {
            get { return this.m_MessageType; }
            set { this.m_MessageType = value; }
        }

        public string MessageTypeComment
        {
            get
            {
                string result = null;
                switch (this.MessageType)
                {
                    case "ADT-A01":
                        result = "Patient Admit";
                        break;
                    case "ADT-A02":
                        result = "Patient Transfer";
                        break;
                    case "ADT-A03":
                        result = "Patient Discharge";
                        break;
                    case "ADT-A04":
                        result = "Patient Registration";
                        break;
                    case "ADT-A08":
                        result = "Patient Pre-admisssion";
                        break;
                    case "ADT-A11":
                        result = "Patient Information Update";
                        break;
                    case "ADT-A12":
                        result = "Cancel Patient Admit";
                        break;
                    case "ADT-A13":
                        result = "Cancel Patient Transfer";
                        break;
                }
                return result;              
            }
        }
    }
}
