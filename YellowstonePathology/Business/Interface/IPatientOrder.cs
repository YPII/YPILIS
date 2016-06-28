using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface IPatientOrder
    {
        string PatientId { get; set; }
        string PLastName { get; set; }
        string PFirstName { get; set; }
        string PMiddleInitial { get; set; }
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
        string PatientDisplayName { get; }
    }
}
