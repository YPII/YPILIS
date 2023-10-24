using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MolecularAnalysis
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class MolecularAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{        
        private string m_TestName;
        private string m_Result;
        private string m_ClinicalSignificance;
        private string m_Method;
        private string m_References;        

        public MolecularAnalysisTestOrder()
        {

        }

		public MolecularAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
		public string ClinicalSignificance
        {
            get { return this.m_ClinicalSignificance; }
            set
            {
                if (this.m_ClinicalSignificance != value)
                {
                    this.m_ClinicalSignificance = value;
                    this.NotifyPropertyChanged("ClinicalSignificance");
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
	}
}
