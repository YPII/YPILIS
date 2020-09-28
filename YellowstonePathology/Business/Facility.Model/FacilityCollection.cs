using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Facility.Model
{	
	public class FacilityCollection : ObservableCollection<Facility>
	{
        private static FacilityCollection instance;


        public static FacilityCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetAllFacilities();
                    instance.Insert(0, new Facility());
                }
                return instance;
            }
        }

        public static FacilityCollection Refresh()
        {
            instance = null;
            return Instance;
        }

        public FacilityCollection()
		{
            
		}

        protected override void InsertItem(int index, Facility item)
        {
            base.InsertItem(index, item);
        }

        private static FacilityCollection Sort(FacilityCollection facilityCollection)
        {
            FacilityCollection result = new FacilityCollection();
            IOrderedEnumerable<Facility> orderedResult = facilityCollection.OrderBy(i => i.FacilityName);
            foreach (Facility facility in orderedResult)
            {
                result.Add(facility);
            }
            return result;
        }

        public static FacilityCollection GetPathGroupFacilities()
        {
            FacilityCollection result = new FacilityCollection();
            foreach (Facility facility in FacilityCollection.Instance)
            {
                if( facility.FacilityId == null ||
                    facility.FacilityId == "YPBLGS" ||       //YellowstonePathologistBillings
                    facility.FacilityId == "YPBZMN" ||      //YellowstonePathologistBozeman
                    facility.FacilityId == "BTTPTHLGY" ||   //ButtePathology
                    facility.FacilityId == "PAOIF" ||       //PathologyAssociatesOfIdahoFalls
                    facility.FacilityId == "PCOWM" ||       //PathologyConsultantsOfWesternMontana
                    facility.FacilityId == "PPWY" ||        //ProfessionalPathologyOfWyoming
                    facility.FacilityId == "SHPTHASS")      //SheridanPathologyAssociates
                {
                    result.Add(facility);
                }
            }
            return result;
        }

        public Facility GetByFacilityId(string facilityId)
        {
            Facility result = null;
            foreach (Facility facility in this)
            {
                if (facility.FacilityId == facilityId)
                {
                    result = facility;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string facilityId)
        {
            bool result = false;
            foreach (Facility facility in this)
            {
                if (facility.FacilityId == facilityId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static FacilityCollection GetAllYPFacilities()
        {
            FacilityCollection result = new FacilityCollection();
            foreach(Facility facility in FacilityCollection.Instance)
            {
                if(facility.FacilityId == "YPBLGS" ||   //YellowstonePathologistBillings
                    facility.FacilityId == "YPIBLGS" || //YellowstonePathologyInstituteBillings
                    facility.FacilityId == "YPCDY" ||   //YellowstonePathologistCody
                    facility.FacilityId == "YPICDY" ||  //YellowstonePathologyInstituteCody
                    facility.FacilityId == "YPBZMN" ||   //YellowstonePathologistBozeman
                    facility.FacilityId == "YPIHVR" )   //YellowstonePathologyHavre
                {
                    result.Add(facility);
                }
            }

            return result;
        }

        public static bool IsAYellowstonePathologyFacility(Facility facility)
        {
            bool result = false;

            if (facility.FacilityId == "YPBLGS" ||   //YellowstonePathologistBillings
                facility.FacilityId == "YPIBLGS" || //YellowstonePathologyInstituteBillings
                facility.FacilityId == "YPCDY" ||   //YellowstonePathologistCody
                facility.FacilityId == "YPICDY" ||  //YellowstonePathologyInstituteCody
                facility.FacilityId == "YPBZMN" ||
                facility.FacilityId == "YPIHVR")   //YellowstonePathologistBozeman
            {
                result = true;
            }

            return result;
        }        

        public static string GetBillBy(string professionalComponentBillingFacilityId, string technicalComponentBillingFacilityId, string billingComponent, string billTo)
        {
            string result = null;

            Facility technicalFacility = FacilityCollection.Instance.GetByFacilityId(technicalComponentBillingFacilityId);
            Facility professionalFacility = FacilityCollection.Instance.GetByFacilityId(professionalComponentBillingFacilityId);
            Facility ypiFacility = FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            Facility ypFacility = FacilityCollection.Instance.GetByFacilityId("YPBLGS");

            if (FacilityCollection.IsAYellowstonePathologyFacility(technicalFacility) == true && FacilityCollection.IsAYellowstonePathologyFacility(professionalFacility) == true)
            {
                if (billingComponent == "Global" && billTo == "Client")
                {
                    result = ypFacility.FacilityId;
                }
                else
                {
                    result = ypiFacility.FacilityId;
                }
            }
            else
            {
                if (billingComponent == "Professional")
                {
                    result = professionalComponentBillingFacilityId;
                }
                else if (billingComponent == "Technical")
                {
                    result = technicalComponentBillingFacilityId;
                }
                else if (billingComponent == "Global")
                {
                    result = technicalComponentBillingFacilityId;
                }
            }

            return result;            
        }

        public Facility GetByClientId(int clientId)
        {
            Facility result = this.FirstOrDefault(f => f.ClientId == clientId);
            return result;
        }
	}
}
