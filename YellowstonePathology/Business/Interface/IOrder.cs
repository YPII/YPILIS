using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Interface
{
    public interface IOrder
	{        	        
        string MasterAccessionNo { get; set; }
        int SpecimenLogId { get; set; }
        int LoggedById { get; set; }
        int AccessionedById { get; set; }
        bool Accessioned { get; set; }
        Nullable<DateTime> CollectionDate { get; set; }                        
        Nullable<DateTime> CollectionTime { get; set; }                
        Nullable<DateTime> AccessionDate { get; set; }                
        Nullable<DateTime> AccessionTime { get; set; }

        int ClientId { get; set; }
        string ClientName { get; set; }
		int PhysicianId { get; set; }
        string PhysicianName { get; set; }
		string PatientType { get; set; }                                    
		string PrimaryInsurance { get; set; }                                    
		string SecondaryInsurance { get; set; }                                    
		string PatientId { get; set; }        
        string PLastName { get; set; }
        string PFirstName { get; set; }        
        string PMiddleInitial { get; set; }
        string PatientName { get; }        
        Nullable<DateTime> PBirthdate { get; set; }
        string PAddress1 { get; set; }
        string PAddress2 { get; set; }
        string PCity { get; set; }
        string PState { get; set; }
        string PZipCode { get; set; }
        string PPhoneNumberHome { get; set; }
        string PPhoneNumberBusiness { get; set; }
        string PMaritalStatus { get; set; }
        string PRace { get; set; }
        string PSex { get; set; }
        string PSSN { get; set; }
        string PCAN { get; set; }
        string PSuffix { get; set; }        
        string SvhMedicalRecord { get; set;}
        string SvhAccount { get; set;}
		string ClientOrderId { get; set; }		
		bool RequisitionVerified { get; set; }
        bool OrderCancelled { get; set; }
        string ExternalOrderId { get; set; }        

		string PatientAccessionAge { get; }
		string PatientDisplayName { get; }		

		Nullable<DateTime> AccessionDateTime { get; set; }        

        void NotifyPropertyChanged(string property);
        void SubmitChanges(YellowstonePathology.Business.DataContext.YpiDataBase dataContext);
	}
}
