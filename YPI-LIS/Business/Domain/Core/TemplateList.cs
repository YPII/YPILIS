using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Core
{
	public class TemplateList : List<Template>
	{
        private static YellowstonePathology.Business.Domain.Core.TemplateList m_Instance;

        private TemplateList()
        {
			this.Add(new Template() { TemplateId = 0, FileName = string.Empty, Retired = false, TemplateName = string.Empty});
			this.Add(new Template() { TemplateId = 15, FileName = "CFCarrierScreeningV2.xml", Retired = false, TemplateName = "CF Carrier Screening" });
            this.Add(new Template() { TemplateId = 16, FileName = "CFCarrierScreeningNegativeEthnicRiskKnownV2.xml", Retired = false, TemplateName = "CF Carrier Screening - Negative - Ethinic Risk Known" });                                
        }

        public static YellowstonePathology.Business.Domain.Core.TemplateList Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new YellowstonePathology.Business.Domain.Core.TemplateList();
                }
                return m_Instance;
            }
        }
	}
}
