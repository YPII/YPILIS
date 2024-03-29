﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.MUM1ByIHC
{
    class MUM1ByIHCTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public MUM1ByIHCTest()
        {
            this.m_PanelSetId = 236;
            this.m_PanelSetName = "MUM1 By IHC";
            this.m_CaseType = Business.CaseType.IHC;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = true;
            this.m_NeverDistribute = true;
            this.m_HasNoOrderTarget = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(8);
            this.m_IsClientAccessioned = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.NothingToPublishReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
        }
    }
}
