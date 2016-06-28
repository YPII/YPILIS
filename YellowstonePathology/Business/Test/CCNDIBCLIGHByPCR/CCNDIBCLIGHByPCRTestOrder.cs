using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR
{
    [PersistentClass("tblCCNDIBCLIGHByPCRTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class CCNDIBCLIGHByPCRTestOrder : PanelSetOrder
    {
        private string m_Result;
        private string m_Interpretation;
        private string m_Method;
        private string m_References;
        private string m_ACR;

        public CCNDIBCLIGHByPCRTestOrder()
        {
        }

        public CCNDIBCLIGHByPCRTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ACR = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has not " +
                "been approved by the FDA.The FDA has determined such clearance or approval is not necessary.This laboratory is CLIA " +
                "certified to perform high complexity clinical testing.";
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
        public string ACR
        {
            get { return this.m_ACR; }
            set
            {
                if (this.m_ACR != value)
                {
                    this.m_ACR = value;
                    this.NotifyPropertyChanged("ACR");
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

