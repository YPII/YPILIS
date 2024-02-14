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

            string expectedFixationDuration = this.m_SpecimenOrder.GetExpectedFixationDuration();
			XElement specimenOrderElement = new XElement("SpecimenOrder",
				new XElement("ContainerId", this.m_SpecimenOrder.ContainerId),
				new XElement("SpecimenOrderId", this.m_SpecimenOrder.SpecimenOrderId),                
				new XElement("SpecimenNumber", this.m_SpecimenOrder.SpecimenNumber),
                new XElement("FixationDurationString", expectedFixationDuration),
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
            Business.Label.Model.CassettePrinterCollection printers = new Label.Model.CassettePrinterCollection();
            Business.Label.Model.CassettePrinter printer = printers.GetPrinter(Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter);            
            Business.Label.Model.CarouselColumn column = printer.Carousel.GetColumn(this.m_AccessionOrder.CassetteColor);

            string blockColor = null;
            if (aliquotOrder.Status == "Hold")
            {
                blockColor = "#f42a56";
            }
            else
            {
                blockColor = column.ColorCode;
            }
            
			string status = "Created";
			if(aliquotOrder.StatusDepricated == Business.Slide.Model.SlideStatusEnum.Printed) status = "Printed";
            if(aliquotOrder.StatusDepricated == Business.Slide.Model.SlideStatusEnum.Validated) status = "Validated";

            string decal = null;
            if (aliquotOrder.Decal == true) decal = "Decal";

			XElement result = new XElement("AliquotOrder",
					new XElement("AliquotType",aliquotOrder.AliquotType),
					new XElement("AliquotOrderId",aliquotOrder.AliquotOrderId),
					new XElement("Description", aliquotOrder.Description),
					new XElement("Label", aliquotOrder.PrintLabel),
                    new XElement("Decal", decal),
                    new XElement("GrossVerified", aliquotOrder.GrossVerified.ToString()),
					new XElement("BlockColor", blockColor),
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
