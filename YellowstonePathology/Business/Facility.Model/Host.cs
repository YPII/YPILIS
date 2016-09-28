using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Facility.Model
{
    [PersistentClass("tblHost", "YPIDATA")]
    public class Host
    {
		private string m_ObjectId;
		private string m_HostName;
        private string m_FacilityId;
        private string m_LocationId;        

        public Host()
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
				}
			}
		}

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string HostName
        {
            get { return this.m_HostName; }
            set { this.m_HostName = value; }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set { this.m_FacilityId = value; }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string LocationId
        {
            get { return this.m_LocationId; }
            set { this.m_LocationId = value; }
        }        
    }
}
