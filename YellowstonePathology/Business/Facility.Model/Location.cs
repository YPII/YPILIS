using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{    
    public class Location
    {
        protected string m_LocationId;        
		protected string m_Description;        

        public Location()
        {
            
        }

        public Location(string locationId, string description)
        {
            this.m_LocationId = locationId;
            this.m_Description = description;
        }
        
        public string LocationId
        {
            get { return this.m_LocationId; }
            set { this.m_LocationId = value; }
        }        

		public string Description
        {
			get { return this.m_Description; }
			set { this.m_Description = value; }
        }        
	}
}
