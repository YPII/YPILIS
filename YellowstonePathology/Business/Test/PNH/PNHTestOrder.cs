using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.PNH
{
	[PersistentClass("tblPNHTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class PNHTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Comment;
		private string m_TypeIRedBloodCells;
		private string m_TypeIIRedBloodCells;
		private string m_TypeIIIRedBloodCells;
		private string m_TypeIIMonocytes;
		private string m_TypeIIIMonocytes;
		private string m_TypeIIGranulocytes;
		private string m_TypeIIIGranulocytes;
		private string m_Method;
		private string m_References;
		private string m_ASRComment;
		
		public PNHTestOrder()
        {
            
        }

		public PNHTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			if (this.m_Accepted == true)
			{
				result.Success = false;
				result.Message = "Results may not be set because the results already have been accepted.";
			}
			else if (string.IsNullOrEmpty(this.TypeIIGranulocytes) == true ||
				string.IsNullOrEmpty(this.TypeIIIGranulocytes) == true ||
				string.IsNullOrEmpty(this.TypeIIMonocytes) == true ||
				string.IsNullOrEmpty(this.TypeIIIMonocytes) == true ||
				string.IsNullOrEmpty(this.TypeIRedBloodCells) == true ||
				string.IsNullOrEmpty(this.TypeIIRedBloodCells) == true ||
				string.IsNullOrEmpty(this.TypeIIIRedBloodCells) == true)
			{
				result.Success = false;
				result.Message = "All cell counts must be set before the results can be set.";
			}

			return result;
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("Result: ");
			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.Append("Comment: ");
			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();

			return result.ToString();
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
		public string TypeIRedBloodCells
		{
			get { return this.m_TypeIRedBloodCells; }
			set
			{
				if (this.m_TypeIRedBloodCells != value)
				{
					this.m_TypeIRedBloodCells = value;
					this.NotifyPropertyChanged("TypeIRedBloodCells");
				}
			}
		}

		[PersistentProperty()]
		public string TypeIIRedBloodCells
		{
			get { return this.m_TypeIIRedBloodCells; }
			set
			{
				if (this.m_TypeIIRedBloodCells != value)
				{
					this.m_TypeIIRedBloodCells = value;
					this.NotifyPropertyChanged("TypeIIRedBloodCells");
				}
			}
		}

		[PersistentProperty()]
		public string TypeIIIRedBloodCells
		{
			get { return this.m_TypeIIIRedBloodCells; }
			set
			{
				if (this.m_TypeIIIRedBloodCells != value)
				{
					this.m_TypeIIIRedBloodCells = value;
					this.NotifyPropertyChanged("mypeIIIRedBloodCells");
				}
			}
		}

		[PersistentProperty()]
		public string TypeIIMonocytes
		{
			get { return this.m_TypeIIMonocytes; }
			set
			{
				if (this.m_TypeIIMonocytes != value)
				{
					this.m_TypeIIMonocytes = value;
					this.NotifyPropertyChanged("TypeIIMonocytes");
				}
			}
		}

		[PersistentProperty()]
		public string TypeIIIMonocytes
		{
			get { return this.m_TypeIIIMonocytes; }
			set
			{
				if (this.m_TypeIIIMonocytes != value)
				{
					this.m_TypeIIIMonocytes = value;
					this.NotifyPropertyChanged("TypeIIIMonocytes");
				}
			}
		}

		[PersistentProperty()]
		public string TypeIIGranulocytes
		{
			get { return this.m_TypeIIGranulocytes; }
			set
			{
				if (this.m_TypeIIGranulocytes != value)
				{
					this.m_TypeIIGranulocytes = value;
					this.NotifyPropertyChanged("TypeIIGranulocytes");
				}
			}
		}

		[PersistentProperty()]
		public string TypeIIIGranulocytes
		{
			get { return this.m_TypeIIIGranulocytes; }
			set
			{
				if (this.m_TypeIIIGranulocytes != value)
				{
					this.m_TypeIIIGranulocytes = value;
					this.NotifyPropertyChanged("TypeIIIGranulocytes");
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
	}
}
