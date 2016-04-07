﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CMMC
{
	class CMMCLynchSyndromeEvaluationNteView : CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public CMMCLynchSyndromeEvaluationNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrder = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(106);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("Lynch Syndrome Evaluation", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("Interpretation: ", document);
			this.HandleLongString(panelSetOrder.Interpretation, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Comment: ", document);
			this.HandleLongString(panelSetOrder.Comment, document);
			this.AddBlankNteElement(document);

			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(102);
			if (panelSetOrderLynchSyndromeIHC != null)
			{
				this.AddNextNteElement("Mismatch Repair Protein Expression by Immunohistochemistry: ", document);
				this.AddNextNteElement("MLH1: " + panelSetOrderLynchSyndromeIHC.MLH1Result, document);
				this.AddNextNteElement("MSH2: " + panelSetOrderLynchSyndromeIHC.MSH2Result, document);
				this.AddNextNteElement("MSH6: " + panelSetOrderLynchSyndromeIHC.MSH6Result, document);
				this.AddNextNteElement("PMS2: " + panelSetOrderLynchSyndromeIHC.PMS2Result, document);
				this.AddBlankNteElement(document);
			}

            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
            YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (((this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true ||
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true) &&
                panelSetOrder.BRAFIsIndicated == true) ||
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
            {
				this.AddNextNteElement("Molecular Analysis", document);
				this.AddBlankNteElement(document);
            }

            if (panelSetOrder.BRAFIsIndicated == true)
			{
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
				{
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrder.OrderedOnId, true);

					this.AddNextNteElement("BRAF V600E Mutation by PCR: " + panelSetOrderBraf.ReportNo, document);
					this.AddNextNteElement("Result: " + panelSetOrderBraf.Result, document);
					this.AddBlankNteElement(document);
				}
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrder.OrderedOnId, true);

                    this.AddNextNteElement("BRAF V600E Mutation by PCR: " + panelSetOrderRASRAF.ReportNo, document);
                    this.AddNextNteElement("Result: " + panelSetOrderRASRAF.BRAFResult, document);
                    this.AddBlankNteElement(document);
                }
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrder.OrderedOnId, true);
                this.AddNextNteElement("MLH1 Methylation Analysis: " + panelSetOrderMLH1MethylationAnalysis.ReportNo, document);
                this.AddNextNteElement("Result: " + panelSetOrderMLH1MethylationAnalysis.Result, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Method: ", document);
			this.HandleLongString(panelSetOrder.Method, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("References: ", document);
			this.HandleLongString(panelSetOrder.References, document);
			this.AddBlankNteElement(document);
		}
	}
}
