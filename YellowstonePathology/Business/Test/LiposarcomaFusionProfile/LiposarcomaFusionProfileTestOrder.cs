using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.LiposarcomaFusionProfile
{
    [PersistentClass("tblLiposarcomaFusionProfileTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class LiposarcomaFusionProfileTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        protected const string ReportMethod = "Method needs to be set.";
        protected const string References = "References need to be set.";
        protected const string TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics " +
            "Laboratories.  This test has not been approved by the FDA.  The FDA has determined such clearance or approval is not necessary. This laboratory is " +
            "CLIA certified to perform high complexity clinical testing.";

        private string m_Result;
        private string m_Interpretation;
        private string m_Method;
        private string m_ReportDisclaimer;

        public LiposarcomaFusionProfileTestOrder()
        {
        }

        public LiposarcomaFusionProfileTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Method = ReportMethod;
            this.m_ReportReferences = References;

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(this.GetLocationPerformedComment() + Environment.NewLine);
            disclaimer.AppendLine(TestDevelopment);
            this.m_ReportDisclaimer = disclaimer.ToString();
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        public string ReportDisclaimer
        {
            get { return this.m_ReportDisclaimer; }
            set
            {
                if (this.m_ReportDisclaimer != value)
                {
                    this.m_ReportDisclaimer = value;
                    this.NotifyPropertyChanged("ReportDisclaimer");
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
