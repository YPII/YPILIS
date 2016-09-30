using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Media;

namespace YellowstonePathology.Business.View
{
	public class GrossBlockManagementView : XElement
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private XElement m_CaseNotesDocument;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public GrossBlockManagementView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, XElement caseNotesDocument, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
			: base("GrossBlockManagementView")
		{            
			this.m_AccessionOrder = accessionOrder;			
			this.m_CaseNotesDocument = caseNotesDocument;
            this.m_SpecimenOrder = specimenOrder;
    
			this.BuildAccessionElement();
			this.BuildCaseNotesElement();
			this.BuildSpecimenElement();
		}        

		private void BuildAccessionElement()
		{                        
			this.Add(new XElement("Accession",
				new XElement("MasterAccessionNo", this.m_AccessionOrder.MasterAccessionNo.ToString()),				
				new XElement("PFirstName", this.m_AccessionOrder.PFirstName),
				new XElement("PLastName", this.m_AccessionOrder.PLastName),
				new XElement("DisplayName", this.m_AccessionOrder.PatientDisplayName)));
		}

		private void BuildCaseNotesElement()
		{
			XElement caseNotesElement = new XElement("CaseNotesCollection");
			foreach (XElement caseNoteElement in this.m_CaseNotesDocument.Elements())
			{
				XElement noteElement = caseNoteElement;
				string logDate = noteElement.Element("LogDate").Value;
				DateTime lDate;
				if (DateTime.TryParse(logDate, out lDate) == true)
				{
					logDate = lDate.ToShortDateString() + " " + lDate.ToShortTimeString();
				}

				noteElement.Element("LogDate").Value = logDate;
				caseNotesElement.Add(noteElement);
			}
			this.Add(caseNotesElement);
		}

		private void BuildSpecimenElement()
		{            
			XElement specimenOrderElement = new XElement("SpecimenOrder",
				new XElement("ContainerId", this.m_SpecimenOrder.ContainerId),
				new XElement("SpecimenOrderId", this.m_SpecimenOrder.SpecimenOrderId),
                new XElement("ProcessorRun", this.m_SpecimenOrder.ProcessorRun),
                new XElement("ProcessorRunId", this.m_SpecimenOrder.ProcessorRunId),
				new XElement("SpecimenNumber", this.m_SpecimenOrder.SpecimenNumber),
                new XElement("FixationDurationString", this.m_SpecimenOrder.FixationDurationString),
				new XElement("Description", this.m_SpecimenOrder.Description));

			XElement aliquotCollectionElement = new XElement("AliquotOrderCollection");

			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.m_SpecimenOrder.AliquotOrderCollection)
			{
				if (aliquotOrder.AliquotType == "Block" ||
					aliquotOrder.AliquotType == "FrozenBlock" ||
					aliquotOrder.AliquotType == "CellBlock")
				{
					XElement aliquotOrderElement = this.BuildAliquotOrderElement(aliquotOrder);
					aliquotCollectionElement.Add(aliquotOrderElement);
				}
			}

			specimenOrderElement.Add(aliquotCollectionElement);
			this.Add(specimenOrderElement);
		}		

		private XElement BuildAliquotOrderElement(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
		{
            YellowstonePathology.Business.Common.PrintMate printMate = new Common.PrintMate();
            YellowstonePathology.Business.Common.PrintMateColumn printMateColumn = printMate.Carousel.GetColumn(this.m_AccessionOrder.PrintMateColumnNumber);            
			
			string status = "Created";
			if(aliquotOrder.StatusDepricated == YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed) status = "Printed";
            if (aliquotOrder.StatusDepricated == YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Validated) status = "Validated";
			XElement result = new XElement("AliquotOrder",
					new XElement("AliquotType",aliquotOrder.AliquotType),
					new XElement("AliquotOrderId",aliquotOrder.AliquotOrderId),
					new XElement("Description", aliquotOrder.Description),
					new XElement("Label", aliquotOrder.PrintLabel),
					new XElement("GrossVerified", aliquotOrder.GrossVerified.ToString()),
					new XElement("BlockColor", printMateColumn.ColorCode),
                    new XElement("EmbeddingInstructions", aliquotOrder.EmbeddingInstructions),
                    new XElement("StatusDepricated", status));

			XElement testCollectionElement = new XElement("TestOrderCollection");

			foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in aliquotOrder.TestOrderCollection)
			{                
                XElement testOrderElement = this.BuildTestOrderElement(testOrder);
                testCollectionElement.Add(testOrderElement);                
			}

			result.Add(testCollectionElement);
			return result;
		}		

		private XElement BuildTestOrderElement(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
		{
			XElement result = new XElement("TestOrder",
				new XElement("TestOrderId", testOrder.TestOrderId),
				new XElement("TestName",testOrder.TestName),
				new XElement("TestId", testOrder.TestId.ToString()));

			return result;
		}
	}
}
