using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using YellowstonePathology.Business.Persistence;
using System.Xml.Linq;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    [PersistentClass("tblSurgicalClientOrder", "tblClientOrder", "YPIDATA")]
    [DataContract]
    public partial class SurgicalClientOrder : ClientOrder
    {
		private string m_PreOpDiagnosis;
        private string m_PostOpDiagnosis;

        public SurgicalClientOrder()
        {

        }
		public SurgicalClientOrder(string objectId) : base(objectId)
		{
		}

        [PersistentProperty()]
        [DataMember]
        public string PreOpDiagnosis
        {
            get { return this.m_PreOpDiagnosis; }
            set
            {
                if (this.m_PreOpDiagnosis != value)
                {
                    this.m_PreOpDiagnosis = value;
                    this.NotifyPropertyChanged("PreOpDiagnosis");                    
                }
            }
        }

        [PersistentProperty()]
        [DataMember]
        public string PostOpDiagnosis
        {
            get { return this.m_PostOpDiagnosis; }
            set
            {
                if (this.m_PostOpDiagnosis != value)
                {
                    this.m_PostOpDiagnosis = value;
                    this.NotifyPropertyChanged("PostOpDiagnosis");                    
                }
            }
        }

        public override System.Xml.Linq.XElement ToXML(bool includeChildren)
        {
            return base.ToXML(includeChildren);
        }
    }
}
