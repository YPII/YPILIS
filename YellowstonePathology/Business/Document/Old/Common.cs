using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace YellowstonePathology.Business.Document.Old
{
    public class Common
    {
        public const string reportTemplatePath = @"\\CFileServer\documents\ReportTemplates\";
        public const string printDistributionPath = @"\\CFileServer\documents\distribution\print\";
        public const string printDistributionDonePath = @"\\CFileServer\documents\distribution\print\done";
        public const string faxDistributionPath = @"\\CFileServer\documents\distribution\fax\";

        public enum FlowTestTypes
        {
            LLP = 1,
            AbCD4 = 15,
            TPP = 13,
            DNA = 14,
            HLA = 16,
            PAA = 4,
            RP = 5,
            FH = 7,
            SCE = 12
        }

        public enum roleDescription
        {
            Administrator = 1,
            SurgicalCaseFinal = 2,
            CytologyScreener = 3,
            FlowCaseFinal = 4,
            CaseLogEntry = 5,
            SurgicalCaseTyping = 6,
            MolecularCaseFinal = 7,
            MolecularCaseTech = 8,
            ReportDistributionMonitor = 9,
            MedTech = 10
        }

        public static void openWordDocWithWordViewer(string fileName)
        {
            Process p1 = new Process();
            p1.StartInfo = new ProcessStartInfo("wordview.exe", fileName);
            //p1.StartInfo.Verb = "Print";
            p1.Start();
        }

        public static void openWordDoc(string fileName)
        {
            Process p1 = new Process();
            p1.StartInfo = new ProcessStartInfo("winword.exe", fileName);
            p1.Start();
        }

        public static void printTifByAccessionNo(string reportNo)
        {
            string fileName = Common.getCaseFileNameTif(reportNo);
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            p.StartInfo = info;
            p.StartInfo.Verb = "print";
            p.Start();
            p.WaitForExit();
        }

        public static void printTifByFileName(string fileName)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(fileName);
            p.StartInfo = info;
            p.StartInfo.Verb = "print";
            p.Start();
            p.WaitForExit();
        }

        public static string getPatientAgeString(string accessionDate, string birthDate)
        {
            string ageString = "";
            string birthDateString = Common.getShortDateStringFromDate(birthDate);
            if (birthDate != "")
            {
                DateTime accessionDateObject = DateTime.Parse(accessionDate);
                DateTime birthDateObject = DateTime.Parse(birthDate);

                System.TimeSpan timeSpan = new TimeSpan(accessionDateObject.Ticks - birthDateObject.Ticks);
                long days = timeSpan.Days;
                long years = days / 365;
                if (years >= 1)
                {
                    ageString = years.ToString() + "YO";
                }
                else
                {
                    if (days <= 30)
                    {
                        ageString = days.ToString() + "DO";
                    }
                    else
                    {
                        long months = days / 30;
                        ageString = months.ToString() + "MO";
                    }
                }
            }
            return ageString;
        }

        public static string getShortDateStringFromDate(string date)
        {
            string returnDate = "";
            try
            {
                returnDate = DateTime.Parse(date).ToShortDateString();
            }
            catch (Exception)
            {
                returnDate = "";
            }
            return returnDate;
        }

        public static string getShortTimeStringFromTime(string time)
        {
            string returnTime = "";
            try
            {
                returnTime = DateTime.Parse(time).ToLongTimeString();
            }
            catch (Exception)
            {
                returnTime = "";
            }
            return returnTime;
        }

        public static string getShortDateTimeString(string dateTime)
        {
            string returnTime = string.Empty;
            try
            {
                returnTime = DateTime.Parse(dateTime).ToString("MM/dd/yyy HH:mm");
            }
            catch (Exception)
            {
                returnTime = string.Empty;
            }
            return returnTime;
        }

        public static void printDistributionReports()
        {
            string[] files = Directory.GetFiles(printDistributionPath, "*.doc");
            foreach (string file in files)
            {                
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo(file);
                info.Verb = "Print";                
                p.StartInfo = info;
                p.Start();              
                string doneFile = file.Replace(@"\\CFileServer\documents\Distribution\Print", @"\\CFileServer\documents\Distribution\Print\Done");
                File.Move(file, doneFile);
            }
            MessageBox.Show(files.Length.ToString() + " files have been printed.");
        }

        public static string formatDate(string date)
        {
            try
            {
                return DateTime.Parse(date).ToShortDateString();
            }
            catch (FormatException)
            {
                return "";
            }
        }

        public static bool validateDate(string date)
        {
            DateTime dateOut;
            if (DateTime.TryParse(date, out dateOut))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string [] getCaseFileList(string strAccessionNo)
        {
            return Directory.GetFiles(getCasePath(strAccessionNo));
        }

        public static string getNextFileName(string reportNo)
        {
            int fileNo = 1;
            string strPath = Common.getCasePath(reportNo);
            string destinationFile = strPath + reportNo + "." + fileNo + ".tif";
            while (true)
            {
                if (File.Exists(destinationFile))
                {
                    fileNo += 1;
                    destinationFile = strPath + reportNo + "." + fileNo + ".tif";
                    continue;
                }
                else
                {
                    break;
                }
            }
            return destinationFile;
        }

        public static string getCaseFileNameDoc(string reportNo)
        {
            string fileName = getCasePath(reportNo) + reportNo + ".doc";
            return fileName;
        }

        public static string getCaseFileNameTif(string reportNo)
        {
            string fileName = getCasePath(reportNo) + reportNo + ".tif";
            return fileName;
        }

		public static string getCasePath(string reportNo)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(reportNo);
			return YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
		}
    }
}
