using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	[PersistentClass("tblCalreticulinMutationAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class CalreticulinMutationAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
        private string m_Result;
        private string m_Percentage;
        private string m_Mutations;
        private string m_Interpretation;
        private string m_Method;

        public CalreticulinMutationAnalysisTestOrder()
        {

        }

		public CalreticulinMutationAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {

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
        public string Percentage
        {
            get { return this.m_Percentage; }
            set
            {
                if (this.m_Percentage != value)
                {
                    this.m_Percentage = value;
                    this.NotifyPropertyChanged("Percentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Mutations
        {
            get { return this.m_Mutations; }
            set
            {
                if (this.m_Mutations != value)
                {
                    this.m_Mutations = value;
                    this.NotifyPropertyChanged("Mutations");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}
	}
}
