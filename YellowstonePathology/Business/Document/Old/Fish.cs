using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Document.Old
{
    public class Fish : BaseReport, YellowstonePathology.Business.Interface.ICaseDocument
    {
        const string m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Fish.4.xml";
        private YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;                

        public Fish(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_NativeDocumentFormat = NativeDocumentFormatEnum.Word;
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }

		public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
			return YellowstonePathology.Business.Document.CaseDocument.DeleteCaseFiles(orderIdParser);
        }

		public void Render()
		{                        
			base.OpenTemplate(m_TemplateName);

			this.m_SqlStatements.Add("select Top(1) a.* from tblAccessionOrder a JOIN tblPanelSetOrder pso ON a.[MasterAccessionNo] = pso.[MasterAccessionNo] where pso.[ReportNo] = '" + this.m_PanelSetOrder.ReportNo + "'");
			this.m_SqlStatements.Add("select * from tblFishResult where ReportNo = '" + this.m_PanelSetOrder.ReportNo + "'");

			this.m_TableNames.Add("tblAccessionOrder");
            this.m_TableNames.Add("tblFishResult");

            this.GetDataSet();
			this.SetDemographics("tblAccessionOrder");
            this.SetReportDistribution();
            this.SetCaseHistory();

            string surgicalAccessionNo = this.m_ReportData.Tables[0].Rows[0]["surgicalaccessionno"].ToString();
            this.SetXmlNodeData("s_accessionno", surgicalAccessionNo);

            string result = this.m_ReportData.Tables["tblFishResult"].Rows[0]["result"].ToString();
            this.SetXmlNodeData("test_result", result);

            string ratio = this.m_ReportData.Tables["tblFishResult"].Rows[0]["resultdescription"].ToString();
            this.SetXmlNodeData("test_ratio", ratio);

			string signature = this.m_ReportData.Tables["tblAccessionOrder"].Rows[0]["pathologistsignature"].ToString();
            this.SetXmlNodeData("pathologist_signature", signature);

            string reportComment = this.m_ReportData.Tables["tblFishResult"].Rows[0]["reportcomment"].ToString();
            this.SetXmlNodeData("interpretation_comment", reportComment);

			string finalDate = string.Empty;
			if (this.m_PanelSetOrder.FinalDate.HasValue)
			{
				finalDate = this.m_PanelSetOrder.FinalDate.Value.ToShortDateString();
			}			
			this.SetXmlNodeData("final_date", finalDate);

            string fixationComment = this.m_ReportData.Tables["tblFishResult"].Rows[0]["fixationcomment"].ToString();
            this.SetXmlNodeData("fixation_comment", fixationComment);

            string timeOfFixation = "Time of fixation: " + this.m_ReportData.Tables["tblFishResult"].Rows[0]["TimeOfFixation"].ToString();
            this.SetXmlNodeData("time_of_fixation", timeOfFixation);

            string resultComment = this.m_ReportData.Tables["tblFishResult"].Rows[0]["resultcomment"].ToString();
            if (resultComment == "")
            {
                this.SetXmlNodeData("comment_label", "");
            }
            else
            {
                this.SetXmlNodeData("comment_label", "Comment:");
            }

            this.SetXmlNodeData("result_comment", resultComment);			
            this.SaveReport();
        }

        public void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }        
    }
}
