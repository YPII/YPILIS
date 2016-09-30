 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	[DataContract]
	[PersistentClass("tblSurgicalClientOrderDetail", "tblClientOrderDetail", "YPIDATA")]
	public partial class SurgicalClientOrderDetail : ClientOrderDetail
	{
		private Nullable<bool> m_OrderImmediateExam;
		private Nullable<bool> m_OrderFrozenSection;

		public SurgicalClientOrderDetail()
        {

        }

		public SurgicalClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode) 
            : base(persistenceMode)
		{

		}

		public SurgicalClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode, string objectId)
			: base(persistenceMode, objectId)
		{

		}

        public string ImmediateExamInstructions
        {
            get
            {
                string result = null;
                if (this.OrderImmediateExam == true)
                {
                    if (this.OrderFrozenSection == true)
                    {
                        result = "Please perform an immediate exam with frozen section on this specimen.";
                    }
                    else
                    {
                        result = "Please perform an immediate exam on this specimen.";
                    }                    
                }
                return result;
            }
        }

		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in ClientOrderDetailSurgicalProperty");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in ClientOrderDetailSurgicalProperty");
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public Nullable<bool> OrderImmediateExam
		{
			get { return this.m_OrderImmediateExam; }
			set
			{
				if (this.m_OrderImmediateExam != value)
				{
					this.m_OrderImmediateExam = value;
					this.NotifyPropertyChanged("OrderImmediateExam");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public Nullable<bool> OrderFrozenSection
		{
			get { return this.m_OrderFrozenSection; }
			set
			{
				if (this.m_OrderFrozenSection != value)
				{
					this.m_OrderFrozenSection = value;
					this.NotifyPropertyChanged("OrderFrozenSection");
				}
			}
		}
	}
}
