using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.StemCellEnumeration
{
    [PersistentClass("tblStemCellEnumerationTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class StemCellEnumerationTestOrder : PanelSetOrder
    {

        private string m_StemCellEnumeration;
        private string m_Viability;
        private string m_WBCCount;
        private string m_Method;

        public StemCellEnumerationTestOrder()
        {
        }

        public StemCellEnumerationTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Method = "Quantitative Flow Cytometry - Sensitivity for CD34 is 0.03 %";
        }

        [PersistentProperty()]
        public string StemCellEnumeration
        {
            get { return this.m_StemCellEnumeration; }
            set
            {
                if (this.m_StemCellEnumeration != value)
                {
                    this.m_StemCellEnumeration = value;
                    NotifyPropertyChanged("StemCellEnumeration");
                }
            }
        }

        [PersistentProperty()]
        public string Viability
        {
            get { return this.m_Viability; }
            set
            {
                if (this.m_Viability != value)
                {
                    this.m_Viability = value;
                    NotifyPropertyChanged("Viability");
                }
            }
        }

        [PersistentProperty()]
        public string WBCCount
        {
            get { return this.m_WBCCount; }
            set
            {
                if (this.m_WBCCount != value)
                {
                    this.m_WBCCount = value;
                    NotifyPropertyChanged("WBCCount");
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
                    NotifyPropertyChanged("Method");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("StemCellEnumeration: " + this.m_StemCellEnumeration);
            result.AppendLine();

            result.AppendLine("Viability: " + this.m_Viability);
            result.AppendLine();

            result.AppendLine("WBCCount: " + this.m_WBCCount);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if(string.IsNullOrEmpty(this.m_StemCellEnumeration) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Stem Cell Enumeration" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_Viability) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Viability" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_WBCCount) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "WBC Count" + Environment.NewLine;
                }

                if ( result.Status == AuditStatusEnum.Failure)
                {
                    result.Message = "Unable to accept as the following result/s are not set: " + Environment.NewLine + result.Message;
                }
            }
            return result;
        }
    }
}
