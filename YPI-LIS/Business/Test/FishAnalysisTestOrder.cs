using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.FishAnalysis
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class FishAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{        
        private string m_TestName;
        private string m_Result;
        private string m_Interpretation;        
        private string m_References;
        private string m_NucleiScored;        

        public FishAnalysisTestOrder()
        {

        }

		public FishAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		public string TestName
        {
            get { return this.m_TestName; }
            set
            {
                if (this.m_TestName != value)
                {
                    this.m_TestName = value;
                    this.NotifyPropertyChanged("TestName");
                }
            }
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
		public string NucleiScored
        {
            get { return this.m_NucleiScored; }
            set
            {
                if (this.m_NucleiScored != value)
                {
                    this.m_NucleiScored = value;
                    this.NotifyPropertyChanged("NucleiScored");
                }
            }
        }           		

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			result.AppendLine("Nuclei Scored: " + this.m_NucleiScored);
			result.AppendLine();

			return result.ToString();
		}
	}
}
