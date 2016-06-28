using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ClientBillingFacility
    {        
        private Facility m_PerformingFacility;
        private Facility m_DefaultBillingFacility;
        private Business.Client.Model.ClientGroupClientCollection m_ClientGroupClientCollection;
        private string m_FacilityComponent;

        public ClientBillingFacility(Facility performaingFacility, Facility defaultBillingFacility, Business.Client.Model.ClientGroupClientCollection clientGroupClientCollection, string facilityComponent)
        {
            this.m_PerformingFacility = performaingFacility;
            this.m_DefaultBillingFacility = defaultBillingFacility;
            this.m_ClientGroupClientCollection = clientGroupClientCollection;
            this.m_FacilityComponent = facilityComponent;
        }        

        public bool IsMatch(string performingFacilityId, int clientId, string facilityComponent)
        {
            bool result = false;
            if (this.m_PerformingFacility.FacilityId == performingFacilityId && this.m_FacilityComponent == facilityComponent)
            {
                if (this.m_ClientGroupClientCollection.ClientIdExists(clientId) == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public Business.Client.Model.ClientGroupClientCollection ClientGroupClientCollection
        {
            get { return this.m_ClientGroupClientCollection; }
        }

        public Facility PerformingFacility
        {
            get { return this.m_PerformingFacility; }
        }

        public Facility DefaultBillingFacility
        {
            get { return this.m_DefaultBillingFacility; }
        }

        public string FacilityComponent
        {
            get { return this.m_FacilityComponent; }
        }
    }
}
