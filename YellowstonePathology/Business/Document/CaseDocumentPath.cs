using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Document
{
    public class CaseDocumentPath
    {
		private static string GetLegacyPath(string reportNo)
        {
            string strPath = @"\\CFileServer\documents";
            string strCaseType = reportNo.Substring(0, 1);
            switch (strCaseType)
            {
                case "A":
                    strPath += @"\Autopsy";
                    break;
                case "S":
                    strPath += @"\surgical";
                    break;
                case "P":
                    strPath += @"\cytology";
                    break;
                case "F":
                    switch (reportNo.Substring(0, 2))
                    {
                        case "FM":
                            strPath += @"\ForensicKHM";
                            break;
                        case "FB":
                            strPath += @"\ForensicTLB";
                            break;
                        default:
                            strPath += @"\flowcytometry";
                            break;
                    }
                    break;
                case "M":
                    if (reportNo.Substring(0, 2) == "ME")
                    {
                        strPath += @"\AutopsyMueller";
                    }
                    else
                    {
                        strPath += @"\MolecularTesting";
                    }
                    break;
                case "T":
                case "B":
                    strPath += @"\TechnicalOnly";
                    break;
                case "R":
                    strPath += @"\ReferenceLab";
                    break;
                case "Y":
                    strPath += @"\Summary";
                    break;
            }
            int dashPosition = reportNo.IndexOf('-');
            string strYear = reportNo.Substring(dashPosition - 2, 2);
            switch (strYear)
            {
                case "99":
                    strPath += @"\19" + strYear;
                    break;
                default:
                    strPath += @"\20" + strYear;
                    break;
            }
            string[] splitAtDash = reportNo.Split('-');
            string number = splitAtDash[1];
            int length = number.Length;

            switch (length)
            {
                case 5:
                    strPath += @"\" + number.Substring(0, 2) + "000-" + number.Substring(0, 2) + "999";
                    break;
                case 4:
                    strPath += @"\0" + number.Substring(0, 1) + "000-0" + number.Substring(0, 1) + "999";
                    break;
                default:
                    strPath += @"\00001-00999";
                    break;
            }
            strPath += @"\" + reportNo + @"\";
            return strPath;
        }

        public static string GetPath(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            if (orderIdParser.IsLegacyReportNo == true) 
                return GetLegacyPath(orderIdParser.ReportNo);

            string strPath = @"\\CFileServer\AccessionDocuments\20" + orderIdParser.MasterAccessionNoYear.Value.ToString();
            string number = orderIdParser.MasterAccessionNoNumber.Value.ToString();
            int length = number.Length;

            switch (length)
            {
                case 5:
                    strPath += @"\" + number.Substring(0, 2) + "000-" + number.Substring(0, 2) + @"999\";
                    break;
                case 4:
                    strPath += @"\0" + number.Substring(0, 1) + "000-0" + number.Substring(0, 1) + @"999\";
                    break;
                default:
                    strPath += @"\00001-00999\";
                    break;
            }
            strPath += orderIdParser.MasterAccessionNo + @"\";
            return strPath;
        }
    }
}
