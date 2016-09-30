using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface IClient
    {
        int m_ClientId { get; set; }
        string m_ClientName { get; set; }
        string m_abbreviation { get; set; }
        string m_Address { get; set; }
        string m_City { get; set; }                
        string m_ContactName { get; set; }
        string m_Fax { get; set; }
        bool m_FlowClient { get; set; }
        bool m_Inactive { get; set; }
        bool m_LongDistance { get; set; }
        string m_State { get; set; }
        string m_Telephone { get; set; }
        int m_Zip { get; set; }
    }
}
