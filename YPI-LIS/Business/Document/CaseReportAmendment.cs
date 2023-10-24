using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace YellowstonePathology.Business.Document
{
	public class CaseReportAmendment
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		bool m_HasSignature;
		FontSizeConverter m_FontSizeConverter;

		Table m_MoneyBoxTable;
        YellowstonePathology.Business.Amendment.Model.AmendmentCollection m_AmendmentCollection;

		public CaseReportAmendment()
		{
			m_FontSizeConverter = new FontSizeConverter();
			m_Rule = new YellowstonePathology.Business.Rules.Rule();

			this.m_Rule.ActionList.Add(this.SetAmendments);
		}

        public void SetData(Table moneyBoxTable, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection, bool hasSignature, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			this.m_MoneyBoxTable = moneyBoxTable;
			this.m_AmendmentCollection = amendmentCollection;
			m_HasSignature = hasSignature;
			m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(m_ExecutionStatus);
		}

		private void SetAmendments()
		{
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendmentItem in m_AmendmentCollection)
			{
				if (amendmentItem.Final)
				{
					this.AddAmendmentLine1(YellowstonePathology.Business.Helper.DateTimeExtensions.ReportDateTimeFormat(amendmentItem.FinalDate));
					this.AddAmendmentLine2(amendmentItem.Text);
					if(m_HasSignature)
					{
						if (amendmentItem.RequirePathologistSignature)
						{
							this.AddAmendmentLine3(amendmentItem.PathologistSignature);
							this.AddAmendmentLine4("*** E-Signed " + YellowstonePathology.Business.Helper.DateTimeExtensions.ReportDateTimeFormat(amendmentItem.FinalTime) + " ***");
						}
					}
				}
			}
		}

		public void AddAmendmentLine1(string textValue)
		{
			Paragraph paragraph = new Paragraph(new Run(textValue));
			paragraph.Margin = new Thickness(10, 0, 0, 0);
			paragraph.FontSize = (double)m_FontSizeConverter.ConvertFromString("8pt");
			paragraph.FontFamily = new FontFamily("Verdana");
			paragraph.TextAlignment = TextAlignment.Left;

			Section section = new Section(paragraph);
			section.Margin = new Thickness(0, 1, 0, 0);
			section.BorderBrush = Brushes.Black;
			section.BorderThickness = new Thickness(0, 1, 0, 0);

			TableCell cell = new TableCell(section);
			cell.ColumnSpan = 3;
			cell.BorderBrush = Brushes.Black;
			cell.BorderThickness = new Thickness(0, 2, 0, 0);

			TableRow row = new TableRow();
			row.Cells.Add(cell);

			this.m_MoneyBoxTable.RowGroups[0].Rows.Add(row);
		}

		public void AddAmendmentLine2(string textValue)
		{
			Paragraph paragraph = new Paragraph(new Run(textValue));
			paragraph.Margin = new Thickness(10, 0, 0, 0);
			paragraph.FontSize = (double)m_FontSizeConverter.ConvertFromString("8pt");
			paragraph.FontFamily = new FontFamily("Verdana");
			paragraph.TextAlignment = TextAlignment.Left;

			TableCell cell = new TableCell(paragraph);
			cell.ColumnSpan = 3;

			TableRow row = new TableRow();
			row.Cells.Add(cell);

			this.m_MoneyBoxTable.RowGroups[0].Rows.Add(row);
		}

		public void AddAmendmentLine3(string textValue)
		{
			Paragraph paragraph = new Paragraph(new Run(textValue));
			paragraph.Margin = new Thickness(10, 0, 0, 0);
			paragraph.FontSize = (double)m_FontSizeConverter.ConvertFromString("8pt");
			paragraph.FontFamily = new FontFamily("Verdana");
			paragraph.TextAlignment = TextAlignment.Center;

			TableCell cell = new TableCell(paragraph);

			TableCell spacerCell = new TableCell();
			spacerCell.ColumnSpan = 2;

			TableRow row = new TableRow();
			row.Cells.Add(spacerCell);
			row.Cells.Add(cell);

			this.m_MoneyBoxTable.RowGroups[0].Rows.Add(row);
		}

		public void AddAmendmentLine4(string textValue)
		{
			Paragraph paragraph = new Paragraph(new Run(textValue));
			paragraph.FontSize = (double)m_FontSizeConverter.ConvertFromString("6pt");
			paragraph.FontFamily = new FontFamily("Verdana");
			paragraph.TextAlignment = TextAlignment.Center;

			TableCell cell = new TableCell(paragraph);

			TableCell spacerCell = new TableCell();
			spacerCell.ColumnSpan = 2;

			TableRow row = new TableRow();
			row.Cells.Add(spacerCell);
			row.Cells.Add(cell);

			this.m_MoneyBoxTable.RowGroups[0].Rows.Add(row);
		}
	}
}
