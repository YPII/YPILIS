using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.StemCellCD34Enumeration
{
    [PersistentClass("tblStemCellCD34EnumerationTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class StemCellCD34EnumerationTestOrder : PanelSetOrder
    {

        private string m_CD34Percentage;
        private string m_CD34Absolute;
        private string m_WBCCount;
        private string m_WBCAbsolute;
        private string m_Method;
        private string m_CD34Viability;

        public StemCellCD34EnumerationTestOrder()
        {
        }

        public StemCellCD34EnumerationTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_Method = "Quantitative Flow Cytometry. Sensitivity for CD34 Absolute is 1 cell/uL. Sensitivity for CD34% is 0.02%.";
        }

        [PersistentProperty()]
        public string CD34Percentage
        {
            get { return this.m_CD34Percentage; }
            set
            {
                if (this.m_CD34Percentage != value)
                {
                    this.m_CD34Percentage = value;
                    NotifyPropertyChanged("CD34Percentage");
                }
            }
        }

        [PersistentProperty()]
        public string CD34Absolute
        {
            get { return this.m_CD34Absolute; }
            set
            {
                if (this.m_CD34Absolute != value)
                {
                    this.m_CD34Absolute = value;
                    NotifyPropertyChanged("CD34Absolute");
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
        public string WBCAbsolute
        {
            get { return this.m_WBCAbsolute; }
            set
            {
                if (this.m_WBCAbsolute != value)
                {
                    this.m_WBCAbsolute = value;
                    NotifyPropertyChanged("WBCAbsolute");
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

        [PersistentProperty()]
        public string CD34Viability
        {
            get { return this.m_CD34Viability; }
            set
            {
                if (this.m_CD34Viability != value)
                {
                    this.m_CD34Viability = value;
                    NotifyPropertyChanged("CD34Viability");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("CD34 Percentage: " + this.m_CD34Percentage);
            result.AppendLine();

            result.AppendLine("CD34 Absolute: " + this.m_CD34Absolute);
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
                if (string.IsNullOrEmpty(this.m_CD34Percentage) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "CD34 Percentage" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_CD34Absolute) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "CD34 Absolute" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_WBCCount) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "WBC Count" + Environment.NewLine;
                }

                if (result.Status == AuditStatusEnum.Failure)
                {
                    result.Message = "Unable to accept as the following result/s are not set: " + Environment.NewLine + result.Message;
                }
            }
            return result;
        }
    }
}
