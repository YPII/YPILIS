using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BCellClonalityByPCR
{
	[PersistentClass("tblBCellClonalityByPCRTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class BCellClonalityByPCRTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_TumorNucleiPercent;
		private string m_Comment;
		private string m_Method;
		private string m_References;
		private string m_ASRComment;
		private string m_BCellFrameWork1;
		private string m_BCellFrameWork2;
		private string m_BCellFrameWork3;
		private string m_BCellFragmentSize;

		public BCellClonalityByPCRTestOrder() { }

		public BCellClonalityByPCRTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{

		}

		[PersistentProperty()]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}

		[PersistentProperty()]
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
		public string TumorNucleiPercent
		{
			get { return this.m_TumorNucleiPercent; }
			set
			{
				if (this.m_TumorNucleiPercent != value)
				{
                    this.m_TumorNucleiPercent = value;
					this.NotifyPropertyChanged("TumorNucleiPercent");
				}
			}
		}

		[PersistentProperty()]
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
		public string References
		{
			get { return this.m_References; }
			set
			{
				if (this.m_References != value)
				{
					this.m_References = value;
					this.NotifyPropertyChanged("References");
				}
			}
		}

		[PersistentProperty()]
		public string ASRComment
		{
			get { return this.m_ASRComment; }
			set
			{
				if (this.m_ASRComment != value)
				{
					this.m_ASRComment = value;
					this.NotifyPropertyChanged("ASRComment");
				}
			}
		}

		[PersistentProperty()]
		public string BCellFrameWork1
		{
			get { return this.m_BCellFrameWork1; }
			set
			{
				if (this.m_BCellFrameWork1 != value)
				{
					this.m_BCellFrameWork1 = value;
					this.NotifyPropertyChanged("BCellFrameWork1");
				}
			}
		}

		[PersistentProperty()]
		public string BCellFrameWork2
		{
			get { return this.m_BCellFrameWork2; }
			set
			{
				if (this.m_BCellFrameWork2 != value)
				{
					this.m_BCellFrameWork2 = value;
					this.NotifyPropertyChanged("BCellFrameWork2");
				}
			}
		}

		[PersistentProperty()]
		public string BCellFrameWork3
		{
			get { return this.m_BCellFrameWork3; }
			set
			{
				if (this.m_BCellFrameWork3 != value)
				{
					this.m_BCellFrameWork3 = value;
					this.NotifyPropertyChanged("BCellFrameWork3");
				}
			}
		}

		[PersistentProperty()]
		public string BCellFragmentSize
		{
			get { return this.m_BCellFragmentSize; }
			set
			{
				if (this.m_BCellFragmentSize != value)
				{
					this.m_BCellFragmentSize = value;
					this.NotifyPropertyChanged("BCellFragmentSize");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.Append("B-Cell FrameWork: ");
			result.AppendLine(this.m_Result);
			result.AppendLine();

			result.Append("Interpretation: ");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();

			result.Append("Tumor Nuclei Percent: ");
			result.AppendLine(this.m_TumorNucleiPercent);
			result.AppendLine();

			result.Append("Comment: ");
			result.AppendLine(this.m_Comment);
			result.AppendLine();

			return result.ToString();
		}

		public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			if (this.m_Accepted == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the results already have been accepted.";
			}
			else if (string.IsNullOrEmpty(this.m_BCellFrameWork1) == true ||
				string.IsNullOrEmpty(this.m_BCellFrameWork2) == true ||
				string.IsNullOrEmpty(this.m_BCellFrameWork3) == true)
			{
				result.Success = false;
				result.Message = "All B-Cell results must be set before the results can be set.";
			}

			return result;
		}
	}
}
