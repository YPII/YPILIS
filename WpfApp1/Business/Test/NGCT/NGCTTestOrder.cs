using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.NGCT
{
	[PersistentClass("tblNGCTTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class NGCTTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_NeisseriaGonorrhoeaeResult;
		private string m_ChlamydiaTrachomatisResult;
		private string m_NGResultCode;
		private string m_CTResultCode;
		private string m_Comment;
		private string m_Method;
        private string m_TestInformation;

		public NGCTTestOrder()
		{

		}

		public NGCTTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Method = NGCTResult.Method;
            this.m_ReportReferences = NGCTResult.References;
            this.m_TestInformation = NGCTResult.TestInformation;
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
        }		

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if (string.IsNullOrEmpty(this.m_NGResultCode) == true)
				{
					result.Success = false;
					result.Message = "The NG result must be set before results may be accepted.";
				}
				else if (string.IsNullOrEmpty(this.m_CTResultCode) == true)
				{
					result.Success = false;
					result.Message = "The CT result must be set before results may be accepted.";
				}
			}
			return result;
		}

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append("Chlamydia trachomatis: ");
			result.Append(this.m_ChlamydiaTrachomatisResult);
			result.Append(" Neisseria gonorrhoeae: ");
			result.Append(this.m_NeisseriaGonorrhoeaeResult);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("Chlamydia trachomatis Result: ");
			result.AppendLine(this.m_ChlamydiaTrachomatisResult);
			result.Append("Neisseria gonorrhoeae Result: ");
			result.AppendLine(this.m_NeisseriaGonorrhoeaeResult);
			return result.ToString();
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string NeisseriaGonorrhoeaeResult
		{
			get { return this.m_NeisseriaGonorrhoeaeResult; }
			set
			{
				if (this.m_NeisseriaGonorrhoeaeResult != value)
				{
					this.m_NeisseriaGonorrhoeaeResult = value;
					this.NotifyPropertyChanged("NeisseriaGonorrhoeaeResult");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ChlamydiaTrachomatisResult
		{
			get { return this.m_ChlamydiaTrachomatisResult; }
			set
			{
				if (this.m_ChlamydiaTrachomatisResult != value)
				{
					this.m_ChlamydiaTrachomatisResult = value;
					this.NotifyPropertyChanged("ChlamydiaTrachomatisResult");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string NGResultCode
		{
			get { return this.m_NGResultCode; }
			set
			{
				if (this.m_NGResultCode != value)
				{
					this.m_NGResultCode = value;
					this.NotifyPropertyChanged("NGResultCode");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CTResultCode
		{
			get { return this.m_CTResultCode; }
			set
			{
				if (this.m_CTResultCode != value)
				{
					this.m_CTResultCode = value;
					this.NotifyPropertyChanged("CTResultCode");
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
        public string TestInformation
        {
            get { return this.m_TestInformation; }
            set
            {
                if (this.m_TestInformation != value)
                {
                    this.m_TestInformation = value;
                    this.NotifyPropertyChanged("TestInformation");
                }
            }
        }
    }
}

