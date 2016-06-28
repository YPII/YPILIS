using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	[PersistentClass("tblTrichomonasTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class TrichomonasTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private string m_Method;

        public TrichomonasTestOrder()
        {
            
        }

		public TrichomonasTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_TechnicalComponentInstrumentId = Instrument.HOLOGICPANTHERID;
            this.m_Method = TrichomonasResult.Method;
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
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

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
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

		public override string GetResultWithTestName()
		{
			StringBuilder result = new StringBuilder();
			result.Append(this.m_PanelSetName);
			result.Append(": ");
			result.Append(this.m_Result);
			return result.ToString();
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			return result.ToString();
		}

		public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
			if (result.Success == true)
			{
				if (this.m_ResultCode == null)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted until they are set.";
				}
			}
			return result;
		}
	}
}
