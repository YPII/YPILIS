using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CDC
{
	public class MTDohObxView
	{		
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		string m_ReportNo;
        int m_ObxCount;

        public MTDohObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
			this.m_ObxCount = obxCount;
		}

		public int ObxCount
		{
			get { return this.m_ObxCount; }
		}

		public void ToXml(XElement document)
		{
			YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			int observationSubId = 1;
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				XElement natureOfSpecimenObx3 = this.CreateObx3Segement("22633-2", "Nature of Specimen", "LN");
				string natureOfSpecimen = this.StripWhiteSpace(surgicalSpecimen.SpecimenOrder.Description);
				this.AddNextObxElement(document, natureOfSpecimenObx3, observationSubId, natureOfSpecimen, "F");

				XElement finalDiagnosisObx3 = this.CreateObx3Segement("22637-3", "Final Diagnosis", "LN");
				string finalDiagnosis = this.StripWhiteSpace(surgicalSpecimen.Diagnosis);
				this.AddNextObxElement(document, finalDiagnosisObx3, observationSubId, finalDiagnosis, "F");

				observationSubId += 1;
			}

			observationSubId += 1;
			XElement grossObx03Element = this.CreateObx3Segement("22634-0", "Gross Pathology", "LN");
			string grossDescription = this.StripWhiteSpace(panelSetOrderSurgical.GrossX);
			this.AddNextObxElement(document, grossObx03Element, observationSubId, grossDescription, "F");

			observationSubId += 1;
			XElement microObx03Element = this.CreateObx3Segement("22635-7", "Micro Pathology", "LN");
			string microDescription = this.StripWhiteSpace(panelSetOrderSurgical.MicroscopicX);
			this.AddNextObxElement(document, microObx03Element, observationSubId, microDescription, "F");

			observationSubId += 1;
			XElement clinicalInfoObx03Element = this.CreateObx3Segement("22636-5", "Clinical History", "LN");
			string clinicalInfo = this.StripWhiteSpace(this.m_AccessionOrder.ClinicalHistory);
			this.AddNextObxElement(document, clinicalInfoObx03Element, observationSubId, clinicalInfo, "F");

			observationSubId += 1;
			XElement commentObx03Element = this.CreateObx3Segement("22638-1", "Comment Section", "LN");
			string comment = this.StripWhiteSpace(panelSetOrderSurgical.Comment);
			this.AddNextObxElement(document, commentObx03Element, observationSubId, comment, "F");

			observationSubId += 1;
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrderSurgical.AmendmentCollection)
			{
				XElement supplemental03Element = this.CreateObx3Segement("22639-9", "SupplementalReports/Addendum", "LN");
				string supplemental = this.StripWhiteSpace(amendment.Text);
				this.AddNextObxElement(document, supplemental03Element, observationSubId, supplemental, "F");
			}
		}

		private XElement CreateObx3Segement(string identifier, string identifierName, string codingSystem)
		{
			XElement obx03Element = new XElement("OBX.3");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.1", identifier, obx03Element);
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.2", identifierName, obx03Element);
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.3.3", codingSystem, obx03Element);
			return obx03Element;
		}

		private void AddNextObxElement(XElement document, XElement obx3Element, int observationSubId, string value, string observationResultStatus)
		{
			XElement obxElement = new XElement("OBX");
			document.Add(obxElement);

			XElement obx01Element = new XElement("OBX.1");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.1.1", this.m_ObxCount.ToString(), obx01Element);
			obxElement.Add(obx01Element);

			XElement obx02Element = new XElement("OBX.2");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.2.1", "TX", obx02Element);
			obxElement.Add(obx02Element);

			obxElement.Add(obx3Element);

			XElement obx04Element = new XElement("OBX.4");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.4.1", observationSubId.ToString(), obx04Element);
			obxElement.Add(obx04Element);

			XElement obx05Element = new XElement("OBX.5");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.5.1", value, obx05Element);
			obxElement.Add(obx05Element);

			XElement obx11Element = new XElement("OBX.11");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.11.1", observationResultStatus, obx11Element);
			obxElement.Add(obx11Element);

			XElement obx15Element = new XElement("OBX.15");
			YellowstonePathology.Business.Helper.XmlDocumentHelper.AddElement("OBX.15.1", MshView.CLIANUMBER, obx15Element);
			obxElement.Add(obx15Element);

			this.m_ObxCount += 1;
		}


		public string StripWhiteSpace(string str)
		{
			StringBuilder sb = new StringBuilder();
			if (string.IsNullOrEmpty(str) == false)
			{
				for (int i = 0; i < str.Length; i++)
				{
					char c = str[i];
					switch (c)
					{
						case '\r':
						case '\n':
						case '\t':
							continue;
						default:
							sb.Append(c);
							break;
					}
				}
			}
			return sb.ToString();
		}

		private void HandleLongString(XElement document, XElement obx3Element, int observationSubId, string value, string observationResultStatus)
		{
			if (string.IsNullOrEmpty(value) == false)
			{
				string[] textSplit = value.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in textSplit)
				{
					this.AddNextObxElement(document, obx3Element, observationSubId, text, observationResultStatus);
				}
			}
		}
	}
}
