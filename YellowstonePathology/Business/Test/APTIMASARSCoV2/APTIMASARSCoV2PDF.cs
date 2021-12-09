using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace YellowstonePathology.Business.Test.APTIMASARSCoV2
{
	public class APTIMASARSCoV2PDF
	{
		MigraDoc.DocumentObjectModel.Document m_Document;
		Business.Test.AccessionOrder m_AccessionOrder;
		Business.Test.SARSCoV2.SARSCoV2TestOrder m_PanelSetOrder;
		Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public APTIMASARSCoV2PDF(Business.Test.AccessionOrder accessionOrder, Business.Test.SARSCoV2.SARSCoV2TestOrder panelSetOrder)
		{
			this.m_Document = new MigraDoc.DocumentObjectModel.Document();
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;
			this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
		}

		public void Render()
		{
			this.CreateDocument();
			this.DefineStyes();
			this.CreatePage();
			MigraDoc.Rendering.PdfDocumentRenderer pdfRenderer = new MigraDoc.Rendering.PdfDocumentRenderer();
			pdfRenderer.Document = this.m_Document;
			pdfRenderer.RenderDocument();
			pdfRenderer.PdfDocument.Save(@"c:\ghost\yes.pdf");
		}

		public void CreateDocument()
		{
			this.m_Document = new MigraDoc.DocumentObjectModel.Document();
			this.m_Document.Info.Title = this.m_PanelSetOrder.PanelSetName;			
			this.m_Document.Info.Subject = this.m_PanelSetOrder.ReportNo;
			this.m_Document.Info.Author = "YPI";
		}

		private void DefineStyes()
		{			
			Style style = this.m_Document.Styles["Normal"];			
			style.Font.Name = "Verdana";			
			style = this.m_Document.Styles[StyleNames.Header];
			style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);			
			style = this.m_Document.Styles[StyleNames.Footer];
			style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
			style = this.m_Document.Styles.AddStyle("Table", "Normal");			
			style.Font.Name = "Verdana";			
			style.Font.Name = "Times New Roman";			
			style.Font.Size = 9;			
			style = this.m_Document.Styles.AddStyle("Reference", "Normal");			
			style.ParagraphFormat.SpaceBefore = "5mm";			
			style.ParagraphFormat.SpaceAfter = "5mm";			
			style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
		}

		private void CreatePage()
		{
			Section demoSection = this.m_Document.AddSection();			
			MigraDoc.DocumentObjectModel.Tables.Table demoTable = demoSection.AddTable();			
			demoTable.Style = "Table";
			demoTable.Borders.Color = Color.FromCmyk(100, 0, 0, 0);
			demoTable.Borders.Width = 0.25;
			demoTable.Borders.Left.Width = 0.5;
			demoTable.Borders.Right.Width = 0.5;
			demoTable.Rows.LeftIndent = 0;

			Column column1 = demoTable.AddColumn("3cm");			
			column1.Format.Alignment = ParagraphAlignment.Center;			

			Column column2 = demoTable.AddColumn("2.5cm");
			column2.Format.Alignment = ParagraphAlignment.Center;

			Row row = demoTable.AddRow();			
			row.HeadingFormat = true;			
			row.Format.Alignment = ParagraphAlignment.Center;			
			row.Format.Font.Bold = true;			
			row.Shading.Color = Color.FromCmyk(100, 0, 0, 0);

			Cell cell = new Cell();
			cell.AddParagraph("hello world");
			//cell.MergeRight = 2;
			row.Cells.Add(cell);			
		}

		private void FillContent()
		{

		}

        //public void Render()
		//{
			//PdfDocument document = new PdfDocument();			
			//document.Info.Title = this.m_PanelSetOrder.PanelSetName;			
			//PdfPage page = document.AddPage();			
			//XGraphics gfx = XGraphics.FromPdfPage(page);			
			//XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

			//gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopLeft);
			//document.Save(@"c:\ghost\blooper.pdf");

			//Aspose.Words.Document document = new Aspose.Words.Document(@"\\CFileServer\Documents\ReportTemplates\XmlTemplates\SARSCoV2.3.docx");

			//string description = this.m_SpecimenOrder.Description;
			//document.Range.Replace("specimen_description", description, new FindReplaceOptions(FindReplaceDirection.Forward));			

			/*
			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(this.m_SpecimenOrder.CollectionDate, this.m_SpecimenOrder.CollectionTime);
			document.ReplaceText("date_time_collected", collectionDateTimeString);

			string result = null;
			if (this.m_PanelSetOrder.Result == "DETECTED")
			{
				result = "POSITIVE (DETECTED)";
				document.ReplaceText("test_result_p", result);
				document.ReplaceText("test_result_n", string.Empty);
			}

			if (this.m_PanelSetOrder.Result == "NOT DETECTED")
			{
				result = "Negative (Not Detected)";
				document.ReplaceText("test_result_n", result);
				document.ReplaceText("test_result_p", string.Empty);
			}

			document.ReplaceText("report_method", this.m_PanelSetOrder.Method);
			document.ReplaceText("report_result", this.m_PanelSetOrder.Result);
			document.ReplaceText("report_references", this.m_PanelSetOrder.ReportReferences);
			document.ReplaceText("asr_comment", this.m_PanelSetOrder.ASRComment);
			document.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			*/

			//document.Save(@"c:\ghost\hellllo.docx");
			//document.Save(@"c:\ghost\hellllo.xps", SaveFormat.Xps);
			//document.Save(@"c:\ghost\hellllo.tif", SaveFormat.Tiff);
			//document.Save(@"c:\ghost\hellllo.pdf", SaveFormat.Pdf);

			/*
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\SARSCoV2.3.xml";			

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(this.m_PanelSetOrder.OrderedOnId);
			string description = specimenOrder.Description;
            base.ReplaceText("specimen_description", description);            

			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

			string result = null;
			if (panelSetOrder.Result == "DETECTED")
			{
				result = "POSITIVE (DETECTED)";
				base.ReplaceText("test_result_p", result);
				base.ReplaceText("test_result_n", string.Empty);
			}

			if (panelSetOrder.Result == "NOT DETECTED")
			{
				result = "Negative (Not Detected)";
				base.ReplaceText("test_result_n", result);
				base.ReplaceText("test_result_p", string.Empty);
			}
			
			base.ReplaceText("report_method", panelSetOrder.Method);
			base.ReplaceText("report_result", panelSetOrder.Result);                                    
            base.ReplaceText("report_references", panelSetOrder.ReportReferences);
			base.ReplaceText("asr_comment", panelSetOrder.ASRComment);
			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));			

			this.SetReportDistribution();
			this.SetCaseHistory();

			this.SaveReport();
			*/
		//}		
	}
}
