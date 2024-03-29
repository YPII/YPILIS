﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.API2MALT1ByPCR
{
    public class API2MALT1ByPCRCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public API2MALT1ByPCRCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            API2MALT1ByPCRTestOrder panelSetOrder = (API2MALT1ByPCRTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSet.PanelSetName, document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Result: " + panelSetOrder.Result, document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen: " + specimenOrder.Description, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Probe Set Details:", document);
            this.HandleLongString(panelSetOrder.ProbeSetDetail, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Nuclei Scored:", document);
            this.HandleLongString(panelSetOrder.NucleiScored, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("References:", document);
            this.HandleLongString(panelSetOrder.ReportReferences, document);
        }
    }
}
