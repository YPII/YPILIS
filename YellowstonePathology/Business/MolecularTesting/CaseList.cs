using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.MolecularTesting
{
    public class CaseList
    {
        public CaseList()
        {

        }

        public void Print(YellowstonePathology.Business.Search.ReportSearchList caseList, string description, DateTime printDate)
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.Visible = false;

            Microsoft.Office.Interop.Excel.Workbook wb = xlApp.Workbooks.Add(@"\\CFileServer\documents\ReportTemplates\MolecularTesting\CaseList.xlt");
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

            ws.Cells[3, 1] = "Batch: " + description + " - " + printDate.ToShortDateString();

            int rowPosition = 6;

            for (int i = caseList.Count - 1; i > -1; i--)
            {
                ws.Cells[rowPosition, 1] = caseList[i].ReportNo;
                ws.Cells[rowPosition, 2] = caseList[i].PanelSetName;
                ws.Cells[rowPosition, 3] = caseList[i].PatientName;
                ws.Cells[rowPosition, 4] = caseList[i].PhysicianName + " - " + caseList[i].ClientName;
                ws.Cells[rowPosition, 5] = caseList[i].OrderedBy;
                rowPosition++;
            }

            Object oMissing = Type.Missing;
            Object oFalse = false;

            ws.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close(oFalse, oMissing, oMissing);
            xlApp.Quit();
        }
    }
}
