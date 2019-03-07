using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForMolecular
{
    public class ExtractAndHoldForMolecularTestRetired : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ExtractAndHoldForMolecularTestRetired()
        {
            this.m_PanelSetId = 230;
            this.m_PanelSetName = "Extract And Hold For Molecular - Retired";
            this.m_Abbreviation = "Extract And Hold For Molecular";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = false;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = new TimeSpan(2, 0, 0, 0);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ExtractAndHoldForMolecular.ExtractAndHoldForMolecularTestOrder).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = false;
            this.m_AllowMultiplePerAccession = false;
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
