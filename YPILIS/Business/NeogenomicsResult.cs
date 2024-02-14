using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business
{
    //[PersistentClass("tblNeogenomicsResult", "YPIDATA")]
    public class NeogenomicsResult : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_NeogenomicsResultId;
        private DateTime m_DateResultReceived;
        private string m_Result;
        private string m_PFirstName;
        private string m_PLastName;
        private DateTime m_PBirthdate;
        private bool m_Acknowledged;
        private string m_AcknowledgedBy;
        private Nullable<DateTime> m_AcknowledgedDate;
        private bool m_Imported;
        private string m_ImportedBy;
        private Nullable<DateTime> m_ImportedDate;

        public NeogenomicsResult()
        {

        }

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

        [PersistentPrimaryKeyProperty(false)]
        public string NeogenomicsResultId
        {
            get { return this.m_NeogenomicsResultId; }
            set
            {
                if (this.m_NeogenomicsResultId != value)
                {
                    this.m_NeogenomicsResultId = value;
                    this.NotifyPropertyChanged("NeogenomicsResultId");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public DateTime DateResultReceived
        {
            get { return this.m_DateResultReceived; }
            set
            {
                if (this.m_DateResultReceived != value)
                {
                    this.m_DateResultReceived = value;
                    this.NotifyPropertyChanged("DateResultReceived");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public DateTime PBirthdate
        {
            get { return this.m_PBirthdate; }
            set
            {
                if (this.m_PBirthdate != value)
                {
                    this.m_PBirthdate = value;
                    this.NotifyPropertyChanged("PBirthdate");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public bool Acknowledged
        {
            get { return this.m_Acknowledged; }
            set
            {
                if (this.m_Acknowledged != value)
                {
                    this.m_Acknowledged = value;
                    this.NotifyPropertyChanged("Acknowledged");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public string AcknowledgedBy
        {
            get { return this.m_AcknowledgedBy; }
            set
            {
                if (this.m_AcknowledgedBy != value)
                {
                    this.m_AcknowledgedBy = value;
                    this.NotifyPropertyChanged("AcknowledgedBy");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public Nullable<DateTime> AcknowledgedDate
        {
            get { return this.m_AcknowledgedDate; }
            set
            {
                if (this.m_AcknowledgedDate != value)
                {
                    this.m_AcknowledgedDate = value;
                    this.NotifyPropertyChanged("AcknowledgedDate");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public bool Imported
        {
            get { return this.m_Imported; }
            set
            {
                if (this.m_Imported != value)
                {
                    this.m_Imported = value;
                    this.NotifyPropertyChanged("Imported");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public string ImportedBy
        {
            get { return this.m_ImportedBy; }
            set
            {
                if (this.m_ImportedBy != value)
                {
                    this.m_ImportedBy = value;
                    this.NotifyPropertyChanged("ImportedBy");
                }
            }
        }

        [Persistence.PersistentProperty()]
        public Nullable<DateTime> ImportedDate
        {
            get { return this.m_ImportedDate; }
            set
            {
                if (this.m_ImportedDate != value)
                {
                    this.m_ImportedDate = value;
                    this.NotifyPropertyChanged("ImportedDate");
                }
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            XElement resultDocument = XElement.Parse(this.m_Result);
            result.AppendLine("Last Name: " + resultDocument.Element("PID").Element("PID.5").Element("PID.5.1").Value);
            result.AppendLine("First Name: " + resultDocument.Element("PID").Element("PID.5").Element("PID.5.2").Value);

            result.AppendLine();

            IEnumerable<XElement> obxElements = resultDocument.Elements("OBX");
            foreach (XElement obxElement in obxElements)
            {
                result.AppendLine(obxElement.Element("OBX.3").Element("OBX.3.2").Value + ":");
                if (obxElement.Element("OBX.5").Element("OBX.5.1") != null)
                {
                    result.AppendLine(obxElement.Element("OBX.5").Element("OBX.5.1").Value);
                }
                result.AppendLine();
            }            

            return result.ToString();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
