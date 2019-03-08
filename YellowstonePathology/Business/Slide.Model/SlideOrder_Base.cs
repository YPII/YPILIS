using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Slide.Model
{
    [PersistentClass("tblSlideOrder", "YPIDATA")]
    public class SlideOrder_Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_ObjectId;
        protected string m_SlideOrderId;
        protected string m_AliquotOrderId;
        protected bool m_ClientAccessioned;
        protected DateTime m_OrderDate;
        protected string m_Description;
        protected string m_AliquotType;
        protected string m_Label;
        protected string m_ClientLabel;

        protected int m_OrderedById;
        protected string m_OrderedBy;
        protected string m_OrderedFrom;
        protected bool m_Validated;
        protected string m_ValidationStation;
        protected int m_ValidatedById;
        protected string m_ValidatedBy;
        protected Nullable<DateTime> m_ValidationDate;

        protected bool m_Printed;
        protected string m_PrintStation;
        protected int m_PrintedById;
        protected string m_PrintedBy;
        protected Nullable<DateTime> m_PrintDate;

        protected string m_Status;
        protected string m_TestId;
        protected string m_TestName;
        protected string m_TestAbbreviation;
        protected string m_TestOrderId;
        protected string m_PatientLastName;
        protected string m_PatientFirstName;
        protected string m_Location;
        protected string m_ReportNo;
        protected string m_LabelType;
        protected bool m_OrderedAsDual;
        protected bool m_UseWetProtocol;
        protected bool m_OrderSentToVentana;
        protected bool m_PerformedByHand;

        private DateTime? m_LocationTime;
        private string m_FacilityId;
        private string m_AccessioningFacility;

        private bool m_Combined;

        public SlideOrder_Base()
        {
            this.m_Combined = false;
        }

        public SlideOrder_Base(string objectId, string slideOrderId, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity, int slideNumber)
        {
            this.m_ObjectId = objectId;
            this.m_SlideOrderId = slideOrderId;
            this.m_AliquotOrderId = aliquotOrder.AliquotOrderId;
            this.m_OrderDate = DateTime.Now;
            this.m_AliquotType = "Slide";
            this.m_Description = "Histology Slide";
            this.m_Status = SlideStatusEnum.Created.ToString();
            this.m_Label = SlideOrder_Base.GetSlideLabel(slideNumber, aliquotOrder.Label, aliquotOrder.AliquotType);
            this.m_OrderedBy = systemIdentity.User.UserName;
            this.m_OrderedById = systemIdentity.User.UserId;
            this.m_OrderedFrom = Environment.MachineName;
            this.m_OrderedAsDual = testOrder.OrderedAsDual;
            this.m_FacilityId = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId).FacilityId;
            this.m_Location = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;
            this.m_LocationTime = DateTime.Now;
        }

        public void Validate(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_ValidationStation = System.Environment.MachineName;
            this.m_ValidatedBy = systemIdentity.User.UserName;
            this.m_ValidatedById = systemIdentity.User.UserId;
            this.m_ValidationDate = DateTime.Now;
            this.m_Validated = true;
            this.m_Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Validated.ToString();
            this.NotifyPropertyChanged(string.Empty);
        }

        public void SetAsPrinted(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            if (this.m_Status != YellowstonePathology.Business.Slide.Model.SlideStatusEnum.ClientAccessioned.ToString())
            {
                this.m_PrintStation = System.Environment.MachineName;
                this.m_PrintedBy = systemIdentity.User.UserName;
                this.m_PrintedById = systemIdentity.User.UserId;
                this.m_PrintDate = DateTime.Now;
                this.m_Printed = true;

                if (this.m_Validated == false)
                {
                    this.m_Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed.ToString();
                }
            }

            this.NotifyPropertyChanged(string.Empty);
        }

        [PersistentDocumentIdProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string SlideOrderId
        {
            get { return this.m_SlideOrderId; }
            set
            {
                if ((this.m_SlideOrderId != value))
                {
                    this.m_SlideOrderId = value;
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ClientAccessioned
        {
            get { return this.m_ClientAccessioned; }
            set
            {
                if (this.m_ClientAccessioned != value)
                {
                    this.m_ClientAccessioned = value;
                    this.NotifyPropertyChanged("ClientAccessioned");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "50", "0", "varchar")]
        public string AliquotOrderId
        {
            get
            {
                return this.m_AliquotOrderId;
            }
            set
            {
                if (this.m_AliquotOrderId != value)
                {
                    this.m_AliquotOrderId = value;
                    this.NotifyPropertyChanged("AliquotOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "3", "null", "datetime")]
        public DateTime OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                if (this.m_OrderDate != value)
                {
                    this.m_OrderDate = value;
                    this.NotifyPropertyChanged("OrderDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ValidationStation
        {
            get
            {
                return this.m_ValidationStation;
            }
            set
            {
                if (this.m_ValidationStation != value)
                {
                    this.m_ValidationStation = value;
                    this.NotifyPropertyChanged("ValidationStation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Validated
        {
            get { return this.m_Validated; }
            set
            {
                if (this.m_Validated != value)
                {
                    this.m_Validated = value;
                    this.NotifyPropertyChanged("Validated");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int ValidatedById
        {
            get
            {
                return this.m_ValidatedById;
            }
            set
            {
                if (this.m_ValidatedById != value)
                {
                    this.m_ValidatedById = value;
                    this.NotifyPropertyChanged("ValidatedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ValidatedBy
        {
            get
            {
                return this.m_ValidatedBy;
            }
            set
            {
                if (this.m_ValidatedBy != value)
                {
                    this.m_ValidatedBy = value;
                    this.NotifyPropertyChanged("ValidatedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> ValidationDate
        {
            get { return this.m_ValidationDate; }
            set
            {
                if (this.m_ValidationDate != value)
                {
                    this.m_ValidationDate = value;
                    this.NotifyPropertyChanged("ValidationDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string PrintStation
        {
            get
            {
                return this.m_PrintStation;
            }
            set
            {
                if (this.m_PrintStation != value)
                {
                    this.m_PrintStation = value;
                    this.NotifyPropertyChanged("PrintStation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Printed
        {
            get { return this.m_Printed; }
            set
            {
                if (this.m_Printed != value)
                {
                    this.m_Printed = value;
                    this.NotifyPropertyChanged("Printed");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int PrintedById
        {
            get
            {
                return this.m_PrintedById;
            }
            set
            {
                if (this.m_PrintedById != value)
                {
                    this.m_PrintedById = value;
                    this.NotifyPropertyChanged("PrintedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string PrintedBy
        {
            get
            {
                return this.m_PrintedBy;
            }
            set
            {
                if (this.m_PrintedBy != value)
                {
                    this.m_PrintedBy = value;
                    this.NotifyPropertyChanged("PrintedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> PrintDate
        {
            get { return this.m_PrintDate; }
            set
            {
                if (this.m_PrintDate != value)
                {
                    this.m_PrintDate = value;
                    this.NotifyPropertyChanged("PrintDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Description
        {
            get
            {
                return this.m_Description;
            }
            set
            {
                if (this.m_Description != value)
                {
                    this.m_Description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string AliquotType
        {
            get
            {
                return this.m_AliquotType;
            }
            set
            {

                if (this.m_AliquotType != value)
                {
                    this.m_AliquotType = value;
                    this.NotifyPropertyChanged("AliquotType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Label
        {
            get
            {
                return this.m_Label;
            }
            set
            {
                if (this.m_Label != value)
                {
                    this.m_Label = value;
                    this.NotifyPropertyChanged("Label");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ClientLabel
        {
            get
            {
                return this.m_ClientLabel;
            }
            set
            {
                if (this.m_ClientLabel != value)
                {
                    this.m_ClientLabel = value;
                    this.NotifyPropertyChanged("ClientLabel");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int OrderedById
        {
            get
            {
                return this.m_OrderedById;
            }
            set
            {
                if (this.m_OrderedById != value)
                {
                    this.m_OrderedById = value;
                    this.NotifyPropertyChanged("OrderedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string OrderedBy
        {
            get
            {
                return this.m_OrderedBy;
            }
            set
            {
                if (this.m_OrderedBy != value)
                {
                    this.m_OrderedBy = value;
                    this.NotifyPropertyChanged("OrderedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string OrderedFrom
        {
            get
            {
                return this.m_OrderedFrom;
            }
            set
            {
                if (this.m_OrderedFrom != value)
                {
                    this.m_OrderedFrom = value;
                    this.NotifyPropertyChanged("OrderedFrom");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'Created'", "varchar")]
        public string Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                if (this.m_Status != value)
                {
                    this.m_Status = value;
                    this.NotifyPropertyChanged("Status");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public string TestId
        {
            get
            {
                return this.m_TestId;
            }
            set
            {
                if (this.m_TestId != value)
                {
                    this.m_TestId = value;
                    this.NotifyPropertyChanged("TestId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string TestName
        {
            get
            {
                return this.m_TestName;
            }
            set
            {
                if (this.m_TestName != value)
                {
                    this.m_TestName = value;
                    this.NotifyPropertyChanged("TestName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string TestAbbreviation
        {
            get
            {
                return this.m_TestAbbreviation;
            }
            set
            {
                if (this.m_TestAbbreviation != value)
                {
                    this.m_TestAbbreviation = value;
                    this.NotifyPropertyChanged("TestAbbreviation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TestOrderId
        {
            get
            {
                return this.m_TestOrderId;
            }
            set
            {
                if (this.m_TestOrderId != value)
                {
                    this.m_TestOrderId = value;
                    this.NotifyPropertyChanged("TestOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string PatientLastName
        {
            get
            {
                return this.m_PatientLastName;
            }
            set
            {
                if (this.m_PatientLastName != value)
                {
                    this.m_PatientLastName = value;
                    this.NotifyPropertyChanged("PatientLastName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string PatientFirstName
        {
            get
            {
                return this.m_PatientFirstName;
            }
            set
            {
                if (this.m_PatientFirstName != value)
                {
                    this.m_PatientFirstName = value;
                    this.NotifyPropertyChanged("PatientFirstName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Location
        {
            get
            {
                return this.m_Location;
            }
            set
            {
                if (this.m_Location != value)
                {
                    this.m_Location = value;
                    this.NotifyPropertyChanged("Location");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ReportNo
        {
            get
            {
                return this.m_ReportNo;
            }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string LabelType
        {
            get
            {
                return this.m_LabelType;
            }
            set
            {
                if (this.m_LabelType != value)
                {
                    this.m_LabelType = value;
                    this.NotifyPropertyChanged("LabelType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool OrderedAsDual
        {
            get
            {
                return this.m_OrderedAsDual;
            }
            set
            {
                if (this.m_OrderedAsDual != value)
                {
                    this.m_OrderedAsDual = value;
                    this.NotifyPropertyChanged("OrderedAsDual");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "3", "null", "datetime")]
        public DateTime? LocationTime
        {
            get { return this.m_LocationTime; }
            set
            {
                if (this.m_LocationTime != value)
                {
                    this.m_LocationTime = value;
                    this.NotifyPropertyChanged("LocationTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set
            {
                if (this.m_FacilityId != value)
                {
                    this.m_FacilityId = value;
                    this.NotifyPropertyChanged("FacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Combined
        {
            get { return this.m_Combined; }
            set
            {
                if (this.m_Combined != value)
                {
                    this.m_Combined = value;
                    this.NotifyPropertyChanged("Combined");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool UseWetProtocol
        {
            get { return this.m_UseWetProtocol; }
            set
            {
                if (this.m_UseWetProtocol != value)
                {
                    this.m_UseWetProtocol = value;
                    this.NotifyPropertyChanged("UseWetProtocol");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool OrderSentToVentana
        {
            get { return this.m_OrderSentToVentana; }
            set
            {
                if (this.m_OrderSentToVentana != value)
                {
                    this.m_OrderSentToVentana = value;
                    this.NotifyPropertyChanged("OrderSentToVentana");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool PerformedByHand
        {
            get { return this.m_PerformedByHand; }
            set
            {
                if (this.m_PerformedByHand != value)
                {
                    this.m_PerformedByHand = value;
                    this.NotifyPropertyChanged("PerformedByHand");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string AccessioningFacility
        {
            get
            {
                return this.m_AccessioningFacility;
            }
            set
            {
                if (this.m_AccessioningFacility != value)
                {
                    this.m_AccessioningFacility = value;
                    this.NotifyPropertyChanged("AccessioningFacility");
                }
            }
        }

        public static string GetSlideLabel(int slideNumber, string blockLabel, string aliquotType)
        {
            StringBuilder slideOrderLabel = new StringBuilder();
            switch (aliquotType)
            {
                case "FrozenBlock":
                    slideOrderLabel.Append("FS" + blockLabel + slideNumber.ToString());
                    break;
                case "CellBlock":
                    slideOrderLabel.Append("CB" + blockLabel + slideNumber.ToString());
                    break;
                case "Wash":
                case "Slide":
                    slideOrderLabel.Append(blockLabel);
                    break;
                default:
                    slideOrderLabel.Append(blockLabel + slideNumber.ToString());
                    break;
            }
            return slideOrderLabel.ToString();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void SetLocation(YellowstonePathology.Business.Facility.Model.Facility facility, string location)
        {
            this.m_FacilityId = facility.FacilityId;
            this.m_LocationTime = DateTime.Now;
            this.m_Location = location;
        }
    }
}
