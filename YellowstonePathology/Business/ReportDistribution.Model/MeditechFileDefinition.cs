using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MeditechFileDefinition
    {
        string m_MasterAccessionNo;
        string m_ReportNo;

        string m_ApplicationId; //30 required
        string m_AccountNumber; //12 required -- Starts with A
        string m_MedicalRecordNumber; //10 optional -- Starts with W
        string m_FormId; //15 required
        string m_DeleteFlag; //1 Y optional
        string m_NumberOfPages; //required if 2+ pages, should be 5 intergers, zero fill from left
        string m_DateOfBirth; //optional 8 intergers yyyymmdd
        string m_Sex; // options 1 (M F U)
        string m_CreateDate; //yyyymmdd - optional 8 integers, date file was created
        string m_CreateTime; //hhmm - optional 4 integers

        public MeditechFileDefinition(int numberOfPages, string accountNumber, string medicalRecordNumber, string masterAccessionNo, string reportNo, DateTime birthDate, string sex, DateTime finalDate)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ReportNo = reportNo;
            
            this.m_ApplicationId = ("YPII" + this.m_MasterAccessionNo + this.m_ReportNo).PadRight(30, ' ');
            this.m_AccountNumber = accountNumber.PadRight(12, ' ');
            this.m_MedicalRecordNumber = medicalRecordNumber.PadRight(10, ' ');
            this.m_FormId = ("PATH").PadRight(15, ' ');
            this.m_DeleteFlag = string.Empty.PadRight(1, ' ');
            this.m_NumberOfPages = numberOfPages.ToString().PadLeft(5, '0');
            this.m_DateOfBirth = birthDate.ToString("yyyyMMdd");
            this.m_Sex = sex;

            System.Diagnostics.Trace.Assert(finalDate != null, "The final date cannot be null.");
            this.m_CreateDate = finalDate.ToString("yyyyMMdd"); //DateTime.Now.ToString("yyyyMMdd");
            this.m_CreateTime = finalDate.ToString("HHmm"); //DateTime.Now.ToString("HHmm");
        }

        public string GetFileName()
        {
            System.Diagnostics.Debug.Assert(this.m_ApplicationId.Length == 30, "The length of the Application ID must be 30.");
            System.Diagnostics.Debug.Assert(this.m_AccountNumber.Length == 12, "The length of the Account Number must be 12.");
            System.Diagnostics.Debug.Assert(this.m_MedicalRecordNumber.Length == 10, "The length of the Medical Record Number must be 10.");
            System.Diagnostics.Debug.Assert(this.m_FormId.Length == 15, "The length of the Form Id must be 15.");
            System.Diagnostics.Debug.Assert(this.m_DeleteFlag.Length == 1, "The length of the Delete Flag must be 1.");
            System.Diagnostics.Debug.Assert(this.m_NumberOfPages.Length == 5, "The length of the Number Of Pages  must be 5.");
            System.Diagnostics.Debug.Assert(this.m_DateOfBirth.Length == 8, "The length of the Date Of Birth  must be 8.");
            System.Diagnostics.Debug.Assert(this.m_Sex.Length == 1, "The length of the Sex  must be 1.");
            System.Diagnostics.Debug.Assert(this.m_CreateDate.Length == 8, "The length of the Create Date  must be 8.");
            System.Diagnostics.Debug.Assert(this.m_CreateTime.Length == 4, "The length of the Create Time  must be 4.");

            StringBuilder result = new StringBuilder();
            result.Append(this.m_ApplicationId);
            result.Append(this.m_AccountNumber);
            result.Append(this.m_MedicalRecordNumber);
            result.Append(this.m_FormId);
            result.Append(this.m_DeleteFlag);
            result.Append(this.m_NumberOfPages);
            result.Append(this.m_DateOfBirth);
            result.Append(this.m_Sex);
            result.Append(this.m_CreateDate);
            result.Append(this.m_CreateTime);

            System.Diagnostics.Debug.Assert(result.Length == 94);            
            return result.ToString();
        }
    }
}
