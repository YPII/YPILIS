using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    [PersistentClass("tblAliquotOrder", "YPIDATA")]
    public class AliquotOrder_Base : INotifyPropertyChanged, YellowstonePathology.Business.Interface.IOrderTarget
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool m_Order = false;
        protected string m_ObjectId;
        protected string m_AliquotOrderId;
        protected string m_SpecimenOrderId;
        protected bool m_ClientAccessioned;        
        protected string m_AliquotType;
        protected string m_Description;
        protected string m_Label;
        protected string m_ClientLabel;
        protected string m_LabelPrefix;
        protected bool m_GrossVerified;
        protected Nullable<int> m_GrossVerifiedById;
        protected string m_GrossVerifiedBy;
        protected Nullable<DateTime> m_GrossVerifiedDate;
        protected bool m_EmbeddingVerified;
        protected Nullable<int> m_EmbeddingVerifiedById;
        protected string m_EmbeddingVerifiedBy;
        protected Nullable<DateTime> m_EmbeddingVerifiedDate;
        protected bool m_Printed;
        protected string m_LabelType;
        private string m_LocationId;
        private string m_FacilityId;
        private string m_FacilityName;
        protected bool m_Validated;
        protected string m_ValidationStation;
        protected int? m_ValidatedById;
        protected string m_ValidatedBy;
        protected Nullable<DateTime> m_ValidationDate;
        protected string m_Status;
        protected string m_EmbeddingInstructions;    

        public AliquotOrder_Base()
        {

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
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
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
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string SpecimenOrderId
        {
            get { return this.m_SpecimenOrderId; }
            set
            {
                if (this.m_SpecimenOrderId != value)
                {
                    this.m_SpecimenOrderId = value;
                    this.NotifyPropertyChanged("SpecimenOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
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
        [PersistentDataColumnProperty(true, "50", "''", "varchar")]
        public string AliquotType
        {
            get { return this.m_AliquotType; }
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
        public string Description
        {
            get { return this.m_Description; }
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Label
        {
            get { return this.m_Label; }
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
            get { return this.m_ClientLabel; }
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string LabelPrefix
        {
            get { return this.m_LabelPrefix; }
            set
            {
                if (this.m_LabelPrefix != value)
                {
                    this.m_LabelPrefix = value;
                    this.NotifyPropertyChanged("LabelPrefix");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool GrossVerified
        {
            get { return this.m_GrossVerified; }
            set
            {
                if (this.m_GrossVerified != value)
                {
                    this.m_GrossVerified = value;
                    this.NotifyPropertyChanged("GrossVerified");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public Nullable<int> GrossVerifiedById
        {
            get { return this.m_GrossVerifiedById; }
            set
            {
                if (this.m_GrossVerifiedById != value)
                {
                    this.m_GrossVerifiedById = value;
                    this.NotifyPropertyChanged("GrossVerifiedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string GrossVerifiedBy
        {
            get { return this.m_GrossVerifiedBy; }
            set
            {
                if (this.m_GrossVerifiedBy != value)
                {
                    this.m_GrossVerifiedBy = value;
                    this.NotifyPropertyChanged("GrossVerifiedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> GrossVerifiedDate
        {
            get { return this.m_GrossVerifiedDate; }
            set
            {
                if (this.m_GrossVerifiedDate != value)
                {
                    this.m_GrossVerifiedDate = value;
                    this.NotifyPropertyChanged("GrossVerifiedDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string LabelType
        {
            get { return this.m_LabelType; }
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string LocationId
        {
            get { return this.m_LocationId; }
            set
            {
                if (this.m_LocationId != value)
                {
                    this.m_LocationId = value;
                    this.NotifyPropertyChanged("LocationId");
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string FacilityName
        {
            get { return this.m_FacilityName; }
            set
            {
                if (this.m_FacilityName != value)
                {
                    this.m_FacilityName = value;
                    this.NotifyPropertyChanged("FacilityName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
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
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int? ValidatedById
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string EmbeddingInstructions
        {
            get
            {
                return this.m_EmbeddingInstructions;
            }
            set
            {
                if (this.m_EmbeddingInstructions != value)
                {
                    this.m_EmbeddingInstructions = value;
                    this.NotifyPropertyChanged("EmbeddingInstructions");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool EmbeddingVerified
        {
            get { return this.m_EmbeddingVerified; }
            set
            {
                if (this.m_EmbeddingVerified != value)
                {
                    this.m_EmbeddingVerified = value;
                    this.NotifyPropertyChanged("EmbeddingVerified");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public Nullable<int> EmbeddingVerifiedById
        {
            get { return this.m_EmbeddingVerifiedById; }
            set
            {
                if (this.m_EmbeddingVerifiedById != value)
                {
                    this.m_EmbeddingVerifiedById = value;
                    this.NotifyPropertyChanged("EmbeddingVerifiedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string EmbeddingVerifiedBy
        {
            get { return this.m_EmbeddingVerifiedBy; }
            set
            {
                if (this.m_EmbeddingVerifiedBy != value)
                {
                    this.m_EmbeddingVerifiedBy = value;
                    this.NotifyPropertyChanged("EmbeddingVerifiedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> EmbeddingVerifiedDate
        {
            get { return this.m_EmbeddingVerifiedDate; }
            set
            {
                if (this.m_EmbeddingVerifiedDate != value)
                {
                    this.m_EmbeddingVerifiedDate = value;
                    this.NotifyPropertyChanged("EmbeddingVerifiedDate");
                }
            }
        }

        public bool IsNotIntraoperative
        {
            get
            {
                if (LabelPrefix == "FS")
                {
                    return false;
                }
                return true;
            }

            set
            {
                if (value)
                {
                    if (LabelPrefix != "CB")
                    {
                        // see if a frozen needs to be removed
                        LabelPrefix = string.Empty;
                    }
                }
                else
                {
                    LabelPrefix = "FS";
                    // see if a frozen needs to be added
                }
            }
        }

        public string Display
        {
            get
            {
                if (LabelPrefix == "CB" || LabelPrefix == "FS")
                {
                    return LabelPrefix + Label;
                }
                if (!string.IsNullOrEmpty(Label))
                {
                    return Label;
                }
                return Description;
            }
            set { }
        }

        public bool Order
        {
            get { return m_Order; }
            set
            {
                m_Order = value;
                NotifyPropertyChanged("Order");
            }
        }

        public YellowstonePathology.Business.Interface.IOrderTargetType GetTargetType()
        {
            throw new Exception("Needs to be impemented");
        }

        public string PrintLabel
        {
            get
            {
                StringBuilder blockTitle = new StringBuilder();
                if (this.AliquotType == "Block" || this.AliquotType == "FrozenBlock" || this.AliquotType == "CellBlock")
                {
                    if (!string.IsNullOrEmpty(this.LabelPrefix))
                    {
                        blockTitle.Append(this.LabelPrefix);
                    }
                    blockTitle.Append(this.Label);
                }
                return blockTitle.ToString();
            }
        }

        /// <summary>
        /// Called when adding or removing a testOrder to set the label prefix.
        /// </summary>
        /// <param name="testOrder"></param>
        /// <param name="adding"></param>
        public void SetLabelPrefix(YellowstonePathology.Business.Test.Model.TestOrder testOrder, bool adding)
        {
            if (testOrder.TestId == 45)
            {
                if (adding)
                {
                    this.LabelPrefix = "FS";
                }
                else
                {
                    this.LabelPrefix = string.Empty;
                }
            }
            else if (testOrder.TestId == 195)
            {
                if (adding)
                {
                    this.LabelPrefix = "CB";
                }
                else
                {
                    this.LabelPrefix = string.Empty;
                }
            }
        }

        public static string GetPrefix(string aliquotType)
        {
            string result = string.Empty;

            if (aliquotType == "FrozenBlock")
            {
                result = "FS";
            }
            else if (aliquotType == "CellBlock")
            {
                result = "CB";
            }

            return result;
        }

        public void Validate()
        {
            if (this.m_Validated == false)
            {
                this.m_ValidationStation = System.Environment.MachineName;
                this.m_ValidatedBy = Business.User.SystemIdentity.Instance.User.UserName;
                this.m_ValidatedById = Business.User.SystemIdentity.Instance.User.UserId;
                this.m_ValidationDate = DateTime.Now;
                this.m_Validated = true;
                this.m_Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Validated.ToString();
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        public void GrossVerify(YellowstonePathology.Business.User.SystemUser systemUser)
        {
            if (this.GrossVerified == false)
            {
                this.GrossVerified = true;
                this.GrossVerifiedById = systemUser.UserId;
                this.GrossVerifiedDate = DateTime.Now;
                this.GrossVerifiedBy = systemUser.UserName;
            }
        }

        public void EmbeddingVerify(YellowstonePathology.Business.User.SystemUser systemUser)
        {
            if (this.EmbeddingVerified == false)
            {
                this.EmbeddingVerified = true;
                this.EmbeddingVerifiedById = systemUser.UserId;
                this.EmbeddingVerifiedDate = DateTime.Now;
                this.EmbeddingVerifiedBy = systemUser.UserName;
            }
        }

        public YellowstonePathology.Business.Slide.Model.SlideStatusEnum StatusDepricated
        {
            get
            {
                YellowstonePathology.Business.Slide.Model.SlideStatusEnum result = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Created;
                if (this.GrossVerified == true)
                {
                    result = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Validated;
                }
                else if (this.Printed == true)
                {
                    result = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed;
                }
                return result;
            }
        }

        public bool IsBlock()
        {
            bool result = false;
            if (this.m_AliquotType == "Block" || this.m_AliquotType == "CellBlock" || this.m_AliquotType == "FrozenBlock")
            {
                result = true;
            }
            return result;
        }

        public bool IsSlide()
        {
            bool result = false;
            if (this.m_AliquotType == "Slide" || this.m_AliquotType == "FNASLD" || this.m_AliquotType == "NGYNSLD")
            {
                result = true;
            }
            return result;
        }

        public bool IsSpecimen()
        {
            bool result = false;
            if (this.m_AliquotType == "Specimen")
            {
                result = true;
            }
            return result;
        }

        public string GetId()
        {
            return this.m_AliquotOrderId;
        }

        public string GetOrderedOnType()
        {
            return YellowstonePathology.Business.OrderedOn.Aliquot;
        }

        public string GetDescription()
        {
            string result = this.m_Description;
            if (string.IsNullOrEmpty(result) == true)
            {
                if (string.IsNullOrEmpty(this.m_LabelPrefix) == true)
                {
                    result = this.m_Label;
                }
                else
                {
                    result = this.m_LabelPrefix + this.m_Label;
                }
            }
            return result;
        }	
        
        public ParseSpecimenOrderIdResult ParseSpecimenOrderIdFromBlock()
        {
            ParseSpecimenOrderIdResult result = new ParseSpecimenOrderIdResult();
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d\d-\d+\.\d+");
            System.Text.RegularExpressions.Match match = regex.Match(this.m_AliquotOrderId);

            if (match.Captures.Count != 0)
            {
                result.ParsedSuccessfully = true;
                result.SpecimenOrderId = match.Value;
            }
            else
            {
                result.ParsedSuccessfully = false;
                result.SpecimenOrderId = null;
            }

            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void HandleAddFrozen(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {
            if (testOrder.TestId == 45)
            {
                this.AliquotType = "FrozenBlock";
            }
        }
    }
}
