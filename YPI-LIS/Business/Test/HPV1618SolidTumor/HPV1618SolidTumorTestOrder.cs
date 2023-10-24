using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	[PersistentClass("tblHPV1618SolidTumorTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class HPV1618SolidTumorTestOrder : PanelSetOrder
	{
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string DetectedResult = "Detected";
        public static string NotDetectedResult = "Not Detected";
        public static string IndeterminateResult = "Indeterminate";

        private string m_Indication;
        private string m_HPV6Result;
        private string m_HPV16Result;
		private string m_HPV18Result;
        private string m_HPV31Result;
        private string m_HPV33Result;
        private string m_HPV45Result;
        private string m_HPV58Result;
        private string m_HPVDNAResult;
        private string m_Method;		
        private string m_Interpretation;
        private string m_Comment;

        public HPV1618SolidTumorTestOrder()
		{

		}

		public HPV1618SolidTumorTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
            this.m_Method = m_Method = "HPV DNA Tissue testing utilizes type-specific primers for early protein genes (E5-E7). Six high-risk (HR) " +
            "types, 16, 18, 31, 33, 45, 58, and one low risk (LR) type, 6/11, are detected by fragment analysis, which covers 95% of " +
            "cancer-related strains.  This test has a limit of detection of 5-10% for detecting HPV subtypes out of total DNA.";
            this.m_ReportReferences = "1. A. Molijn et al. Molecular diagnosis of human papillomavirus (HPV) infections. Journal of Clinical " +
            "Virology 32S (2005) S43–S51" + Environment.NewLine +
            "2. P. Boscolo-Rizzo et al. New insights into human papillomavirus-associated head and neck squamous cell carcinoma head and neck " +
            "squamous cell carcinoma. Acta Otorhinolaryngol Ital 2013;33:77-87";
        }
        
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

        [PersistentProperty()]
        public string HPV6Result
        {
            get { return this.m_HPV6Result; }
            set
            {
                if (this.m_HPV6Result != value)
                {
                    this.m_HPV6Result = value;
                    this.NotifyPropertyChanged("HPV6Result");
                }
            }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string HPV16Result
		{
			get { return this.m_HPV16Result; }
			set
			{
				if (this.m_HPV16Result != value)
				{
					this.m_HPV16Result = value;
					this.NotifyPropertyChanged("HPV16Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string HPV18Result
		{
			get { return this.m_HPV18Result; }
			set
			{
				if (this.m_HPV18Result != value)
				{
					this.m_HPV18Result = value;
					this.NotifyPropertyChanged("HPV18Result");
				}
			}
		}

        [PersistentProperty()]
        public string HPV31Result
        {
            get { return this.m_HPV31Result; }
            set
            {
                if (this.m_HPV31Result != value)
                {
                    this.m_HPV31Result = value;
                    this.NotifyPropertyChanged("HPV31Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPV33Result
        {
            get { return this.m_HPV33Result; }
            set
            {
                if (this.m_HPV33Result != value)
                {
                    this.m_HPV33Result = value;
                    this.NotifyPropertyChanged("HPV33Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPV45Result
        {
            get { return this.m_HPV45Result; }
            set
            {
                if (this.m_HPV45Result != value)
                {
                    this.m_HPV45Result = value;
                    this.NotifyPropertyChanged("HPV45Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPV58Result
        {
            get { return this.m_HPV58Result; }
            set
            {
                if (this.m_HPV58Result != value)
                {
                    this.m_HPV58Result = value;
                    this.NotifyPropertyChanged("HPV58Result");
                }
            }
        }

        [PersistentProperty()]
        public string HPVDNAResult
        {
            get { return this.m_HPVDNAResult; }
            set
            {
                if (this.m_HPVDNAResult != value)
                {
                    this.m_HPVDNAResult = value;
                    this.NotifyPropertyChanged("HPVDNAResult");
                }
            }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public override bool IsOkToAddTasks()
        {
            bool result = false;
            if (this.m_OrderedOn == "Aliquot") result = true;
            return result;
        }

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append("HPV16: ");
			result.Append(this.m_HPV16Result);
			result.Append(" HPV18: ");
			result.Append(this.m_HPV18Result);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("HPV DNA: ");
			result.AppendLine(this.m_HPVDNAResult);
			result.Append("HPV-6/11: ");
			result.AppendLine(this.m_HPV6Result);
            result.Append("HPV-16: ");
            result.AppendLine(this.m_HPV16Result);
            result.Append("HPV-18: ");
            result.AppendLine(this.m_HPV18Result);
            result.Append("HPV-31: ");
            result.AppendLine(this.m_HPV31Result);
            result.Append("HPV-33: ");
            result.AppendLine(this.m_HPV33Result);
            result.Append("HPV-45: ");
            result.AppendLine(this.m_HPV45Result);
            result.Append("HPV58: ");
            result.AppendLine(this.m_HPV58Result);
            return result.ToString();
		}

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();

            if (string.IsNullOrEmpty(this.m_Indication) == true)
            {
                result.Success = false;
                result.Message = "You must choose an indication first.";
            }
            else if (this.m_Accepted == true)
            {
                result.Success = false;
                result.Message = "The results cannot be set because they have already been accepted.";
            }
            return result;
        }

        public void SetNotDetectedResult()
        {
            this.m_HPV6Result = NotDetectedResult;
            this.m_HPV16Result = NotDetectedResult;
            this.m_HPV18Result = NotDetectedResult;
            this.m_HPV31Result = NotDetectedResult;
            this.m_HPV33Result = NotDetectedResult;
            this.m_HPV45Result = NotDetectedResult;
            this.m_HPV58Result = NotDetectedResult;
            this.m_HPVDNAResult = NotDetectedResult;
        }

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();

			if (result.Success == true)
            {
                string message = string.Empty;
                if (string.IsNullOrEmpty(this.m_HPVDNAResult) == true)
                {
                    result.Success = false;
                    message = "HPV DNA, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV6Result) == true)
                {
                    result.Success = false;
                    message += "HPV-6, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV16Result) == true)
                {
                    result.Success = false;
                    message += "HPV-16, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV18Result) == true)
                {
                    result.Success = false;
                    message += "HPV-18, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV31Result) == true)
                {
                    result.Success = false;
                    message += "HPV-31, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV33Result) == true)
                {
                    result.Success = false;
                    message += "HPV-33, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV45Result) == true)
                {
                    result.Success = false;
                    message += "HPV-45, ";
                }
                if (string.IsNullOrEmpty(this.m_HPV58Result) == true)
                {
                    result.Success = false;
                    message += "HPV-58, ";
                }

                if(message.Length > 0)
                {
                    message = message.Substring(0, message.Length - 2);
                    result.Message = "The results cannot be accepted because the " + message + " result/s need a value." + Environment.NewLine +
                        "Not Performed, Detected or Not Detected are acceptable values.";
                }
            }

            return result;
		}

        public void AcceptResults()
        {
            YellowstonePathology.Business.Test.PanelOrder panelOrder = this.PanelOrderCollection.GetUnacceptedPanelOrder();
            panelOrder.AcceptResults();
            this.Accept();
        }
    }
}
