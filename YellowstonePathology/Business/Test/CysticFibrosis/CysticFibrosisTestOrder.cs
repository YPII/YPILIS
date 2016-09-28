using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	[PersistentClass("tblCysticFibrosisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class CysticFibrosisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
        private string m_EthnicGroupId;
		private string m_Interpretation;
		private string m_Comment;
		private string m_Method;
		private string m_MutationsTested;
		private string m_MutationsDetected;
		private string m_Result1898Plus1GtoA;
		private string m_ResultR117H;
		private string m_Result621Plus1GtoT;
		private string m_ResultG551D;
		private string m_ResultDeltaI507;
		private string m_Result711Plus1GtoT;
		private string m_ResultG85E;
		private string m_Result1717Minus1GtoA;
		private string m_ResultR560T;
		private string m_ResultR334W;
		private string m_Result3659delC;
		private string m_Result2184delA;
		private string m_Result2789Plus5GtoA;
		private string m_ResultW1282X;
		private string m_Result3120Plus1GtoA;
		private string m_ResultA455E;
		private string m_ResultDeltaF508;
		private string m_ResultR1162X;
		private string m_ResultR553X;
		private string m_Result3849Plus10KbCtoT;
		private string m_ResultR347P;
		private string m_ResultG542X;
		private string m_ResultN1303K;

		public CysticFibrosisTestOrder()
		{

		}

		public CysticFibrosisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{			
			this.SetResultsAsNotDetected();
		}		

		public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResultCollection cysticFibrosisResultCollection = CysticFibrosisResultCollection.GetAllResults();
            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResult cysticFibrosisResult = cysticFibrosisResultCollection.GetResult(this.m_ResultCode);

            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupCollection cysticFibrosisEthnicGroupCollection = new CysticFibrosisEthnicGroupCollection();
            YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup = cysticFibrosisEthnicGroupCollection.GetCysticFibrosisEthnicGroup(this.m_EthnicGroupId);

			if (this.Accepted == true)
			{
				result.Success = false;
				result.Message = "The results cannot be set because they have already been accepted";
			}			                            
            else if (cysticFibrosisResult is YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisNullResult)
            {
                result.Success = false;
                result.Message = "The results cannot be set because the result has not been selected.";
            }			
            else if(cysticFibrosisEthnicGroup is YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupNull)
			{
                result.Success = false;
                result.Message = "The results cannot be set because the ethnic group has not been selected.";
			}
			else if (this.DoesResultReflectFindings() == false)
			{
				result.Success = false;
				result.Message = "The results cannot be accepted because the individual mutation results are not consistent with the selected result.";
			}

			return result;
		}

		public override Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if (string.IsNullOrEmpty(this.ResultCode) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the results have not been set.";
				}
				else if (this.m_TemplateId == 0)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the patient's race has not been set.";
				}
				else if (this.DoesResultReflectFindings() == false)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the individual mutation results are not consistent with the selected result.";
				}
			}

			return result;
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string EthnicGroupId
        {
            get { return this.m_EthnicGroupId; }
            set
            {
                if (this.m_EthnicGroupId != value)
                {
                    this.m_EthnicGroupId = value;
                    this.NotifyPropertyChanged("EthnicGroupId");
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string MutationsTested
		{
			get { return this.m_MutationsTested; }
			set
			{
				if (this.m_MutationsTested != value)
				{
					this.m_MutationsTested = value;
					this.NotifyPropertyChanged("MutationsTested");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string MutationsDetected
		{
			get { return this.m_MutationsDetected; }
			set
			{
				if (this.m_MutationsDetected != value)
				{
					this.m_MutationsDetected = value;
					this.NotifyPropertyChanged("MutationsDetected");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result1898Plus1GtoA
		{
			get { return this.m_Result1898Plus1GtoA; }
			set
			{
				if (this.m_Result1898Plus1GtoA != value)
				{
					this.m_Result1898Plus1GtoA = value;
					this.NotifyPropertyChanged("Result1898Plus1GtoA");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultR117H
		{
			get { return this.m_ResultR117H; }
			set
			{
				if (this.m_ResultR117H != value)
				{
					this.m_ResultR117H = value;
					this.NotifyPropertyChanged("ResultR117H");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result621Plus1GtoT
		{
			get { return this.m_Result621Plus1GtoT; }
			set
			{
				if (this.m_Result621Plus1GtoT != value)
				{
					this.m_Result621Plus1GtoT = value;
					this.NotifyPropertyChanged("Result621Plus1GtoT");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultG551D
		{
			get { return this.m_ResultG551D; }
			set
			{
				if (this.m_ResultG551D != value)
				{
					this.m_ResultG551D = value;
					this.NotifyPropertyChanged("ResultG551D");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultDeltaI507
		{
			get { return this.m_ResultDeltaI507; }
			set
			{
				if (this.m_ResultDeltaI507 != value)
				{
					this.m_ResultDeltaI507 = value;
					this.NotifyPropertyChanged("ResultDeltaI507");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result711Plus1GtoT
		{
			get { return this.m_Result711Plus1GtoT; }
			set
			{
				if (this.m_Result711Plus1GtoT != value)
				{
					this.m_Result711Plus1GtoT = value;
					this.NotifyPropertyChanged("Result711Plus1GtoT");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultG85E
		{
			get { return this.m_ResultG85E; }
			set
			{
				if (this.m_ResultG85E != value)
				{
					this.m_ResultG85E = value;
					this.NotifyPropertyChanged("ResultG85E");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result1717Minus1GtoA
		{
			get { return this.m_Result1717Minus1GtoA; }
			set
			{
				if (this.m_Result1717Minus1GtoA != value)
				{
					this.m_Result1717Minus1GtoA = value;
					this.NotifyPropertyChanged("Result1717Minus1GtoA");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultR560T
		{
			get { return this.m_ResultR560T; }
			set
			{
				if (this.m_ResultR560T != value)
				{
					this.m_ResultR560T = value;
					this.NotifyPropertyChanged("ResultR560T");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultR334W
		{
			get { return this.m_ResultR334W; }
			set
			{
				if (this.m_ResultR334W != value)
				{
					this.m_ResultR334W = value;
					this.NotifyPropertyChanged("ResultR334W");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result3659delC
		{
			get { return this.m_Result3659delC; }
			set
			{
				if (this.m_Result3659delC != value)
				{
					this.m_Result3659delC = value;
					this.NotifyPropertyChanged("Result3659delC");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result2184delA
		{
			get { return this.m_Result2184delA; }
			set
			{
				if (this.m_Result2184delA != value)
				{
					this.m_Result2184delA = value;
					this.NotifyPropertyChanged("Result2184delA");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result2789Plus5GtoA
		{
			get { return this.m_Result2789Plus5GtoA; }
			set
			{
				if (this.m_Result2789Plus5GtoA != value)
				{
					this.m_Result2789Plus5GtoA = value;
					this.NotifyPropertyChanged("Result2789Plus5GtoA");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultW1282X
		{
			get { return this.m_ResultW1282X; }
			set
			{
				if (this.m_ResultW1282X != value)
				{
					this.m_ResultW1282X = value;
					this.NotifyPropertyChanged("ResultW1282X");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result3120Plus1GtoA
		{
			get { return this.m_Result3120Plus1GtoA; }
			set
			{
				if (this.m_Result3120Plus1GtoA != value)
				{
					this.m_Result3120Plus1GtoA = value;
					this.NotifyPropertyChanged("Result3120Plus1GtoA");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultA455E
		{
			get { return this.m_ResultA455E; }
			set
			{
				if (this.m_ResultA455E != value)
				{
					this.m_ResultA455E = value;
					this.NotifyPropertyChanged("ResultA455E");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultDeltaF508
		{
			get { return this.m_ResultDeltaF508; }
			set
			{
				if (this.m_ResultDeltaF508 != value)
				{
					this.m_ResultDeltaF508 = value;
					this.NotifyPropertyChanged("ResultDeltaF508");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultR1162X
		{
			get { return this.m_ResultR1162X; }
			set
			{
				if (this.m_ResultR1162X != value)
				{
					this.m_ResultR1162X = value;
					this.NotifyPropertyChanged("ResultR1162X");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultR553X
		{
			get { return this.m_ResultR553X; }
			set
			{
				if (this.m_ResultR553X != value)
				{
					this.m_ResultR553X = value;
					this.NotifyPropertyChanged("ResultR553X");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result3849Plus10KbCtoT
		{
			get { return this.m_Result3849Plus10KbCtoT; }
			set
			{
				if (this.m_Result3849Plus10KbCtoT != value)
				{
					this.m_Result3849Plus10KbCtoT = value;
					this.NotifyPropertyChanged("Result3849Plus10KbCtoT");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultR347P
		{
			get { return this.m_ResultR347P; }
			set
			{
				if (this.m_ResultR347P != value)
				{
					this.m_ResultR347P = value;
					this.NotifyPropertyChanged("ResultR347P");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultG542X
		{
			get { return this.m_ResultG542X; }
			set
			{
				if (this.m_ResultG542X != value)
				{
					this.m_ResultG542X = value;
					this.NotifyPropertyChanged("ResultG542X");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ResultN1303K
		{
			get { return this.m_ResultN1303K; }
			set
			{
				if (this.m_ResultN1303K != value)
				{
					this.m_ResultN1303K = value;
					this.NotifyPropertyChanged("ResultN1303K");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Mutations Detected:");
			result.AppendLine(this.m_MutationsDetected);
			result.AppendLine();

			result.AppendLine("Interpretation:");
			result.AppendLine(this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();

			return result.ToString();
		}		

		private void SetResultsAsNotDetected()
		{
            CysticFibrosisNotDetectedResult cysticFibrosisNotDetectedResult = new CysticFibrosisNotDetectedResult();
            string result = cysticFibrosisNotDetectedResult.Result;

			this.Result1898Plus1GtoA = result;
			this.ResultR117H = result;
			this.Result621Plus1GtoT = result;
			this.ResultG551D = result;
			this.ResultDeltaI507 = result;
			this.Result711Plus1GtoT = result;
			this.ResultG85E = result;
			this.Result1717Minus1GtoA = result;
			this.ResultR560T = result;
			this.ResultR334W = result;
			this.Result3659delC = result;
			this.Result2184delA = result;
			this.Result2789Plus5GtoA = result;
			this.ResultW1282X = result;
			this.Result3120Plus1GtoA = result;
			this.ResultA455E = result;
			this.ResultDeltaF508 = result;
			this.ResultR1162X = result;
			this.ResultR553X = result;
			this.Result3849Plus10KbCtoT = result;
			this.ResultR347P = result;
			this.ResultG542X = result;
			this.ResultN1303K = result;
		}

		private bool DoesResultReflectFindings()
		{
			bool result = false;
			CysticFibrosisDetectedResult cysticFibrosisDetectedResult = new CysticFibrosisDetectedResult();
			string compareString = cysticFibrosisDetectedResult.Result;
			CysticFibrosisResult compareResult = new CysticFibrosisNotDetectedResult();

			if (this.Result1898Plus1GtoA == compareString ||
			this.ResultR117H == compareString ||
			this.Result621Plus1GtoT == compareString ||
			this.ResultG551D == compareString ||
			this.ResultDeltaI507 == compareString ||
			this.Result711Plus1GtoT == compareString ||
			this.ResultG85E == compareString ||
			this.Result1717Minus1GtoA == compareString ||
			this.ResultR560T == compareString ||
			this.ResultR334W == compareString ||
			this.Result3659delC == compareString ||
			this.Result2184delA == compareString ||
			this.Result2789Plus5GtoA == compareString ||
			this.ResultW1282X == compareString ||
			this.Result3120Plus1GtoA == compareString ||
			this.ResultA455E == compareString ||
			this.ResultDeltaF508 == compareString ||
			this.ResultR1162X == compareString ||
			this.ResultR553X == compareString ||
			this.Result3849Plus10KbCtoT == compareString ||
			this.ResultR347P == compareString ||
			this.ResultG542X == compareString ||
			this.ResultN1303K == compareString)
			{
				compareResult = new CysticFibrosisDetectedResult();
			}
			if (compareResult.ResultCode == this.m_ResultCode) result = true;
			return result;

		}
	}
}
