﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationRecount
{
    public class HER2AmplificationRecountTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public HER2AmplificationRecountTest()
        {
            this.m_PanelSetId = 314;
            this.m_PanelSetName = "HER2 Amplification Recount";
            this.m_CaseType = Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;
            this.IsBillable = false;
            this.NeverDistribute = true;
            this.m_SurgicalAmendmentRequired = false;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.DoNotPublishReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(4);

            YellowstonePathology.Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
