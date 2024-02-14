using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface IPhysician
    {
        int m_PhysicianID { get; set; }
        bool m_Active { get; set; }
        string m_Address { get; set; }
        string m_City { get; set; }
        string m_DisplayName { get; set; }
        string m_FullName { get; set; }        
        int m_HomeBaseClientId { get; set; }
        int m_HPVInstructionID { get; set; }
        int m_HPVStandingOrderCode { get; set; }
        string m_FirstName { get; set; }
        string m_LastName { get; set; }
        bool m_OutsideConsult { get; set; }        
        int m_ReportDeliveryMethod { get; set; }        
    }
}
