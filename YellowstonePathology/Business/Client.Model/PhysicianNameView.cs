using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	public class PhysicianNameView
	{
		private int m_PhysicianId;
		private string m_ProviderId;
		private string m_FirstName;
		private string m_LastName;        
        private string m_HomeBasePhone;
        private string m_HomeBaseFax;

        public PhysicianNameView()
		{

		}		

		[PersistentProperty]
		public int PhysicianId
		{
			get { return this.m_PhysicianId; }
			set { this.m_PhysicianId = value; }
		}

		[PersistentProperty]
		public string ProviderId
		{
			get { return this.m_ProviderId; }
			set { this.m_ProviderId = value; }
		}

		[PersistentProperty]
		public string FirstName
		{
			get { return this.m_FirstName; }
			set { this.m_FirstName = value; }
		}

		[PersistentProperty]
		public string LastName
		{
			get { return this.m_LastName; }
			set { this.m_LastName = value; }
		}

        [PersistentProperty]
        public string HomeBasePhone
        {
            get { return this.m_HomeBasePhone; }
            set { this.m_HomeBasePhone = value; }
        }

        [PersistentProperty]
        public string HomeBaseFax
        {
            get { return this.m_HomeBaseFax; }
            set { this.m_HomeBaseFax = value; }
        }						
	}
}
