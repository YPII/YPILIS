using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using YellowstonePathology.Business.Domain;

namespace YellowstonePathology.Business.Document
{
    public class DnaReport : YellowstonePathology.Business.Interface.ICaseDocument
	{
        XElement m_ReportTemplate;

        //private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        //private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;        
        private YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;

        public DnaReport(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)             
        {
            //this.m_NativeDocumentFormat = NativeDocumentFormatEnum.XPS;
            //this.m_AccessionOrder = new YellowstonePathology.Business.Test.AccessionOrder();
            //this.m_PanelSetOrderItem = new YellowstonePathology.Business.Test.PanelSetOrder();                 
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }

		public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }

		public virtual void Render()
        {
			/*
			System.Reflection.Assembly assembly = this.GetType().Assembly;
			XmlReader xmlReader = XmlReader.Create(assembly.GetManifestResourceStream("YellowstonePathology.Business.Document.DnaReport.xml"));
			this.m_ReportTemplate = XElement.Load(xmlReader, LoadOptions.None);

			this.m_AccessionOrder = Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);                        

			this.SetData("ReportNo", this.m_PanelSetOrderItem.ReportNo);
			this.SetData("MasterAccessionNo", this.m_AccessionOrder.MasterAccessionNo.ToString());
			this.SetData("PatientName", this.m_AccessionOrder.PatientDisplayName);
			this.SetData("PatientAgeString", "(DOB " + this.m_AccessionOrder.PBirthdate.Value.ToShortDateString() + ", " + this.m_AccessionOrder.PatientAccessionAge + ", " + this.m_AccessionOrder.PSex + ")");
			this.SetData("ClientName", this.m_AccessionOrder.ClientName);
			this.SetData("PhysicianName", this.m_AccessionOrder.PhysicianClientName);
			this.SetData("FinalDate", YellowstonePathology.Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(this.m_PanelSetOrderItem.FinalDate));
			this.SetData("AccessionDate", YellowstonePathology.Business.Helper.DateTimeExtensions.DateAndTimeStringModifiedFromNullable(this.m_AccessionOrder.AccessionDateTime.Value));
			this.SetData("CollectionDate", YellowstonePathology.Business.Helper.DateTimeExtensions.DateAndTimeStringModifiedFromNullable(this.m_AccessionOrder.CollectionDate));
			this.SetData("SpecimenDescription", this.m_AccessionOrder.SpecimenOrderCollection[0].Description);

			this.SetData("Pcan", this.m_AccessionOrder.PCAN);
			if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == false)
			{
				this.SetData("Pcan", this.m_AccessionOrder.SvhAccount + "/" + this.m_AccessionOrder.SvhMedicalRecord);
			}
			else
			{
				if (string.IsNullOrEmpty(this.m_AccessionOrder.PCAN) == false)
				{
					this.SetData("Pcan", this.m_AccessionOrder.PCAN);
				}
			}

			//string dnaResult = this.m_AccessionOrder.ExtensionDocument.Element("PanelSetOrders").Element("PanelSetOrder").Element("DnaResult").Element("Result").Value;
			//this.SetData("DnaResult", dnaResult);

			//string reportComment = this.m_AccessionOrder.ExtensionDocument.Element("PanelSetOrders").Element("PanelSetOrder").Element("DnaResult").Element("Comment").Value;
			//this.SetData("ReportComment", reportComment);

			//string interpretation = this.m_AccessionOrder.ExtensionDocument.Element("PanelSetOrders").Element("PanelSetOrder").Element("DnaResult").Element("Interpretation").Value;
			//this.SetData("ReportInterpretation", interpretation);

			//string signature = this.m_AccessionOrder.ExtensionDocument.Element("PanelSetOrders").Element("PanelSetOrder").Element("Signature").Value;
			//this.SetData("ReportSignature", signature);

			//string referenceId = this.m_AccessionOrder.ExtensionDocument.Element("PanelSetOrders").Element("PanelSetOrder").Element("DnaResult").Element("DnaReferenceId").Value;
			//if (string.IsNullOrEmpty(referenceId) == false)
			//{
				//YellowstonePathology.Business.Domain.Dna.DnaReference dnaReference = this.m_Repository.GetAllDnaReferenceById(Convert.ToInt32(referenceId));
				//YellowstonePathology.Business.Domain.Dna.DnaReference dnaReference = YellowstonePathology.Business.Gateway.LocalDataGateway.GetDnaReferenceById(Convert.ToInt32(referenceId));
				//this.SetData("ReportReferences", dnaReference.Reference);
			//}

			YellowstonePathology.Business.Patient.Model.PatientHistoryList patientHistoryList = new Patient.Model.PatientHistoryList();
			patientHistoryList.SetFillCommandByAccessionNo(this.m_PanelSetOrderItem.ReportNo);
			patientHistoryList.Fill();

			string otherReports = patientHistoryList.ToReportString(this.m_PanelSetOrderItem.ReportNo);
			this.SetData("OtherReports", otherReports);


			YellowstonePathology.DocumentCreator.Document document = new YellowstonePathology.DocumentCreator.Document();
			document.FromReportTemplate(this.m_ReportTemplate);
			SetHistogram(document.DocumentBody);
			SetDnaCycles(document.DocumentBody);            

			YellowstonePathology.Business.Document.ReportDistributionTable.ToDocumentBody(document.DocumentBody, this.m_AccessionOrder.ReportDistributionItemCollection);

			string filePath = string.Empty;
			switch (reportSaveEnum)
			{
				case YellowstonePathology.Business.Document.ReportSaveModeEnum.Draft:
					filePath = CaseDocument.GetCasePath(reportNo) + reportNo + ".draft.xps";
					break;
				case YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal:
					filePath = CaseDocument.GetCasePath(reportNo) + reportNo + ".xps";
					break;
				case YellowstonePathology.Business.Document.ReportSaveModeEnum.Test:
					filePath = @"c:\Testing\Test.xps";
					break;
			}  
            
			document.Paginate(filePath);
			 */
		}

        public void Publish()
        {
            //YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(this.m_PanelSetOrderItem.ReportNo);
            //YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrderItem.ReportNo, false);
        }
        public void Publish(XElement reportTemplate)
        {
            this.m_ReportTemplate = reportTemplate;

            this.SetData("ReportNo", "S10-123");
            this.SetData("PatientName", "Mouse, Mickey E");
            this.SetData("PatientDetails", "DOB");

			//YellowstonePathology.DocumentCreator.Document document = new YellowstonePathology.DocumentCreator.Document();
            //document.FromReportTemplate(this.m_ReportTemplate);
            //document.Paginate(@"C:\Program Files\Yellowstone Pathology Institute\Test.xps");
        }

        /*
		private void SetHistogram(YellowstonePathology.DocumentCreator.DocumentPart pageBody)
        {
			YellowstonePathology.Domain.OrderIdParser orderIdParser = new YellowstonePathology.Domain.OrderIdParser(this.m_PanelSetOrderItem.ReportNo);
			string histogramPath = YellowstonePathology.Business.Document.CaseDocument.GetFlowHistogramFileName(orderIdParser);
			foreach (YellowstonePathology.DocumentCreator.DocumentRow documentRow in pageBody.DocumentRowCollection)
            {
                if (documentRow.Name == "CommentRow")
                {
                    System.Windows.Thickness margin = new System.Windows.Thickness(485, 20, 0, 0);
                    documentRow.DocumentImageCollection[0].FromFile(histogramPath, 250, 200, 1, margin);
                }
            }
        }
        */

        /*
		private void SetDnaCycles(YellowstonePathology.DocumentCreator.DocumentPart pageBody)
        {
            
			foreach (YellowstonePathology.DocumentCreator.DocumentRow documentRow in pageBody.DocumentRowCollection)
            {
                if (documentRow.Name == "CommentRow")
                {
                    
                    IEnumerable<XElement> cycleElements = this.m_AccessionOrder.ExtensionDocument.Element("PanelSetOrders").Element("PanelSetOrder").Element("DnaResult").Element("Cycles").Elements("Cycle");
                    double left = 625;
                    double top = 230;

                    foreach (XElement xelement in cycleElements)
                    {
                        DnaCycleTable dnaCyleTable = new DnaCycleTable();
                        dnaCyleTable.FromCycleElement(xelement);
                        System.Windows.Controls.Canvas.SetLeft(dnaCyleTable, left);
                        System.Windows.Controls.Canvas.SetTop(dnaCyleTable, top);
                        documentRow.Canvas.Children.Add(dnaCyleTable);                        
                        left -= 130;
                    }
                }
            }
           
        }
        */

        private void SetData(string elementName, string data)
        {            
            IEnumerable<XElement> elements = this.m_ReportTemplate.Descendants("TextBlock");
            foreach (XElement element in elements)
            {
                if (element.Attribute("Name").Value == elementName)
                {
                    if (string.IsNullOrEmpty(data) == true)
                    {
                        data = string.Empty;
                    }
                    element.Value = data;
                }
            }
        }
	}
}
