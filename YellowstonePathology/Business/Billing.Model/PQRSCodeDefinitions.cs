using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model.PQRSCodeDefinitions
{
    public class PQRSG9431 : PQRSCode
    {
        public PQRSG9431()
        {
            this.m_Code = "G9431";
            this.m_Description = null;
            this.m_ReportingDefinition = "Code for performance not met";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9425 : PQRSCode
    {
        public PQRSG9425()
        {
            this.m_Code = "G9425";
            this.m_Description = "Code for performance not met";
            this.m_ReportingDefinition = "For Primary Lung Carcinoma, the pT category, pN category not document, reason not given and for Non Small Cell Lung Cancer, Histologic " +
                "Type (Squamous Cell Carcinoma, Adenocarcinoma) OR classified as NSCLC-NOS with an explanation is not documented, reason not given.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9430 : PQRSCode
    {
        public PQRSG9430()
        {
            this.m_Code = "G9430";
            this.m_Description = null;
            this.m_ReportingDefinition = "Specimen site other than anatomic cutaneous location.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9429 : PQRSCode
    {
        public PQRSG9429()
        {
            this.m_Code = "G9429";
            this.m_Description = null;
            this.m_ReportingDefinition = "Code for medical exclusions";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9428 : PQRSCode
    {
        public PQRSG9428()
        {
            this.m_Code = "G9428";
            this.m_Description = null;
            this.m_ReportingDefinition = "Pathology report includes the pT Category and a statement on thickness and ulceration and for pT1, mitotic rate";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9421 : PQRSCode
    {        
        public PQRSG9421()
        {
            this.m_Code = "G9421";
            this.m_Description = "Lung Cancer Reporting - Performance Not Met";
            this.m_ReportingDefinition = "Primary non-small cell lung cancer biopsy and cytology specimen report does not document classification into specific histologic type " +
                "OR classified as NSCLC-NOS with an explanation.";
            this.m_FeeSchedule = FeeScheduleEnum.None;            
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9420 : PQRSCode
    {
        public PQRSG9420()
        {
            this.m_Code = "G9420";
            this.m_Description = "Lung Cancer Reporting - Other Exclusions";
            this.m_ReportingDefinition = "Specimen site other than anatomic location of lung or is not classified as primay non-small cell lung cancer.";
            this.m_FeeSchedule = FeeScheduleEnum.None;            
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9419 : PQRSCode
    {
        public PQRSG9419()
        {
            this.m_Code = "G9419";
            this.m_Description = "Lung Cancer Reporting - Medical Exclusions";
            this.m_ReportingDefinition = "Documentation of medical reason(s) for not reporting the histological type OR NSCLC-NOS classification with an explanation (eg, " +
            "biopsy taken for other purposes in a patient with a history of primay non-small cell lung cancer or other documented medical reasons).";
            this.m_FeeSchedule = FeeScheduleEnum.None;            
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9422 : PQRSCode
    {
        public PQRSG9422()
        {
            this.m_Code = "G9422";
            this.m_Description = "Lung Cancer Reporting - Performance Met";
            this.m_ReportingDefinition = "Primary lung carcinoma that include the pT category, pN category and for Non-Small Cell Lung Cancer, Histologic Type (Squamous Cell " +
                "Carcinomona, Adenocarcinoma) OR classified as NSCLC-NOS with an explanation.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9423 : PQRSCode
    {
        public PQRSG9423()
        {
            this.m_Code = "G9423";
            this.m_Description = "Lung Cancer Reporting - Medical Exclusions";
            this.m_ReportingDefinition = "Primary Lung Carcinoma where the pT category, pN category and for Non Small Cell Lung Cancer, Histologic Type (Squamous Cell Carcinoma, " +
                "Adenocarcinoma) OR NSCLC-NOS classification with an explanation (eg, a solitary fibrous tumor in a person with a history of non-small cell carcinoma or other " +
                "documented medical reasons) not documented for medical reasons.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }

    public class PQRSG9424 : PQRSCode
    {
        public PQRSG9424()
        {
            this.m_Code = "G9424";
            this.m_Description = "Lung Cancer Reporting - Other Exclusions";
            this.m_ReportingDefinition = "specimen site other than anatomic location of lung, is not classified as non-small cell ung cancer or classified as NSCLC-NOS";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
        }
    }    

    public class PQRSG9418 : PQRSCode
    {
        public PQRSG9418()
        {
            this.m_Code = "G9418";
            this.m_Description = "Lung Cancer Reporting - Performance met";
            this.m_ReportingDefinition = "Biopsy and/or cytology specimen reports with a diagnosis of primary non-small cell lung cancer classified into specific hitologic type " +
                "(squamous cell carcinoma, adenocarcinoma) OR classified as NSCLC-NOS with an explanation included in the pathology report.";
            this.m_FeeSchedule = FeeScheduleEnum.None;            
            this.m_IsBillable = false;
        }
    }

    public class PQRS3395F : PQRSCode
    {
        public PQRS3395F()
        {
            this.m_Code = "3395F";
            this.m_ReportingDefinition = "";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3394F : PQRSCode
    {
        public PQRS3394F()
        {
            this.m_Code = "3394F";
            this.m_ReportingDefinition = "";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3394F8P : PQRSCode
    {
        public PQRS3394F8P()
        {
            this.m_Code = "3394F";            
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.Modifier = "8P";
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3125F : PQRSCode
    {
        public PQRS3125F()
        {
            this.m_Code = "3125F";
			this.m_ReportingDefinition = "The esophageal biopsy report documents the presence of Barrett's mucosa and includes a statement about dysplasia.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }        
    }

    public class PQRS3126F : PQRSCode
    {
        public PQRS3126F()
        {
            this.m_Code = "3126F";
            this.m_ReportingDefinition = "The esophageal biopsy reports with the histological finding of Barrett's mucosa that contains a statement about dysplasia. " +
                "(present, absent, or indefinite and if peresent, contains appropriate grading)";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3126F1P : PQRSCode
    {
        public PQRS3126F1P()
        {
            this.m_Code = "3126F";
            this.m_Modifier = "1P";
            this.m_ReportingDefinition = "The esophageal biopsy reports with the histological finding of Barrett's mucosa that contains a statement about dysplasia. " +
                "(present, absent, or indefinite) not performed for medical reasons (eg, malignant neoplasm or absence of intestinal metaplasia)";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }


   

    public class PQRS3126F8P : PQRSCode
    {
        public PQRS3126F8P()
        {
            this.m_Code = "3126F";
            this.m_Modifier = "8P";
            this.m_ReportingDefinition = "The esophageal biopsy reports with the histological finding of Barrett's mucosa that does noot contain a statement about dysplasia. " +
                "(present, absent, or indefinite and if present, contains appropriate grading), reason not otherwise specified.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }



	public class PQRS3125F1P : PQRSCode
	{
		public PQRS3125F1P()
		{
			this.m_Code = "3125F";
			this.m_Modifier = "1P";
			this.m_ReportingDefinition = "The esophageal biopsy reports with histological finding of Barrett's that contains a statement about dysplasia (present, past, or indefinite) not performed for medical reasons.  Documentation of medical reason for not reporting the histological finding of Barrett's (ie, malignant neoplasm or absence of intestinal metaplasia)";
			this.m_FeeSchedule = FeeScheduleEnum.None;
			this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
		}
	}

	public class PQRS3125F8P : PQRSCode
	{
		public PQRS3125F8P()
		{
			this.m_Code = "3125F";
			this.m_Modifier = "8P";
			this.m_ReportingDefinition = "Esophageal biopsy reports with histological finding of Barrett's that does not contain a statement about sysplasia(present, past, or indefinite), reason not specified.";
			this.m_FeeSchedule = FeeScheduleEnum.None;
			this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
		}
	}

	public class PQRS3250F : PQRSCode
    {
        public PQRS3250F()
        {
            this.m_Code = "3250F";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3260 : PQRSCode
    {
        public PQRS3260()
        {
            this.m_Code = "3260";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3260F : PQRSCode
    {
        public PQRS3260F()
        {
            this.m_Code = "3260F";
            this.m_ReportingDefinition = "pT category, pN category and histologic grade have been documented in pathology report.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3260F1P : PQRSCode
    {
        public PQRS3260F1P()
        {
            this.m_Code = "3260F";
            this.m_Modifier = "1P";
            this.m_ReportingDefinition = "The medical reason(s) for not including pT category, pN category, and histologic grade are included the the pathology report.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3260F8P : PQRSCode
    {
        public PQRS3260F8P()
        {
            this.m_Code = "3260F";
            this.m_Modifier = "8P";
            this.m_ReportingDefinition = "pT category, pN category, and histologic grade are not documented in the pathology report, reason not otherwise specified.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRS3267F : PQRSCode
    {
        public PQRS3267F()
        {
            this.m_Code = "3267F";
			this.m_ReportingDefinition = "Pathology report includes pT category, pN category, Gleason score, and statement about margins.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

	public class PQRS3267F1P : PQRSCode
	{
		public PQRS3267F1P()
		{
			this.m_Code = "3267F";
			this.m_Modifier = "1P";
			this.m_ReportingDefinition = "Documentation of medical reason(s) for not including pT category, pN category, Gleason score and statement about margin status (ie, specimen originated from other malignant neoplasms, transurethral resections of the prostate, or secondary site prostatic carcinomas)";
			this.m_FeeSchedule = FeeScheduleEnum.None;
			this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
		}
	}

	public class PQRS3267F8P : PQRSCode
	{
		public PQRS3267F8P()
		{
			this.m_Code = "3267F";
			this.m_Modifier = "8P";
			this.m_ReportingDefinition = "pT category, pN category, Gleason score and statement about margin status were not documented in pathology report, reason not otherwise specified.";
			this.m_FeeSchedule = FeeScheduleEnum.None;
			this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
		}
	}

    public class PQRSG8721 : PQRSCode
    {
        public PQRSG8721()
        {
            this.m_Code = "G8721";
			this.m_ReportingDefinition = "pT category, pN category and histologic grade have been documented in pathology report.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRSG8722 : PQRSCode
    {
        public PQRSG8722()
        {
            this.m_Code = "G8722";
			this.m_ReportingDefinition = "The medical reason(s) for not including pT category, pN category, and histologic grade are included the the pathology report.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRSG8723 : PQRSCode
    {
        public PQRSG8723()
        {
            this.m_Code = "G8723";
			this.m_ReportingDefinition = "pT category, pN category, and histologic grade are not documented in the pathology report, reason not otherwise specified.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRSG8797 : PQRSCode
    {
        public PQRSG8797()
        {
            this.m_Code = "G8797";
            this.m_ReportingDefinition = "Patient is not eligible for this measure because specimen(s) are not of esophageal origin (e.g. stomach) report: " +
                "Specimen site other than anatomic location of esophagus";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class PQRSG8798 : PQRSCode
    {
        public PQRSG8798()
        {
            this.m_Code = "G8798";
			this.m_ReportingDefinition = "Specimen Site other than anatomic location of prostate.";
            this.m_FeeSchedule = FeeScheduleEnum.None;
            this.m_IsBillable = false;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }
}
