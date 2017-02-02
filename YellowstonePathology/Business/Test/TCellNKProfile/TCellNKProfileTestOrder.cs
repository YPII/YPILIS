using System;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.TCellNKProfile
{
    [PersistentClass("tblTCellNKProfileTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class TCellNKProfileTestOrder : PanelSetOrder
    {
        private int? m_WBC;
        private double? m_LymphocytePercentage;
        private double? m_CD3TPercent;
        private double? m_CD4TPercent;
        private double? m_CD8TPercent;
        private double? m_CD16CD56NKPercent;
        private string m_Method;
        private string m_ASRComment;

        private string m_CD4CD8Ratio;
        private string m_CD3TCount;
        private string m_CD4TCount;
        private string m_CD8TCount;
        private string m_CD16CD56NKCount;

        private double? m_CD3Percent;
        private double? m_CD4Percent;
        private double? m_CD8Percent;
        private double? m_CD16Percent;
        private double? m_CD19Percent;
        private double? m_CD45Percent;
        private double? m_CD56Percent;

        public TCellNKProfileTestOrder()
        {

        }

        public TCellNKProfileTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_Method = "Quantitative Flow Cytometry.";
            this.m_ReportReferences = "1.  Stewart CC, Stewart SJ. Immunological monitoring utilizing novel probes. Annual of the New York " +
                "Annals of New York Academy of Science.  1993; 95: 816-823.";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by " +
                "Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has " +
                "determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as " +
                "investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) " +
                "as qualified to perform high complexity clinical laboratory testing.";
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public int? WBC
        {
            get { return this.m_WBC; }
            set
            {
                if (this.m_WBC != value)
                {
                    this.m_WBC = value;
                    this.NotifyPropertyChanged("WBC");
                    this.CD3TCount = this.CalculateAbsoluteCount(m_CD3TPercent);
                    this.CD4TCount = this.CalculateAbsoluteCount(m_CD4TPercent);
                    this.CD8TCount = this.CalculateAbsoluteCount(m_CD8TPercent);
                    this.CD16CD56NKCount = this.CalculateAbsoluteCount(m_CD16CD56NKPercent);
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? LymphocytePercentage
        {
            get { return this.m_LymphocytePercentage; }
            set
            {
                if (this.m_LymphocytePercentage != value)
                {
                    this.m_LymphocytePercentage = value;
                    this.NotifyPropertyChanged("LymphocytePercentage");
                    this.CD3TCount = this.CalculateAbsoluteCount(m_CD3TPercent);
                    this.CD4TCount = this.CalculateAbsoluteCount(m_CD4TPercent);
                    this.CD8TCount = this.CalculateAbsoluteCount(m_CD8TPercent);
                    this.CD16CD56NKCount = this.CalculateAbsoluteCount(m_CD16CD56NKPercent);
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD3TPercent
        {
            get { return this.m_CD3TPercent; }
            set
            {
                if (this.m_CD3TPercent != value)
                {
                    this.m_CD3TPercent = value;
                    this.NotifyPropertyChanged("CD3TPercent");
                    this.CD3TCount = this.CalculateAbsoluteCount(m_CD3TPercent);
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD4TPercent
        {
            get { return this.m_CD4TPercent; }
            set
            {
                if (this.m_CD4TPercent != value)
                {
                    this.m_CD4TPercent = value;
                    this.NotifyPropertyChanged("CD4TPercent");
                    this.CD4TCount = this.CalculateAbsoluteCount(m_CD4TPercent);
                    this.SetCD4CD8Ratio();
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD8TPercent
        {
            get { return this.m_CD8TPercent; }
            set
            {
                if (this.m_CD8TPercent != value)
                {
                    this.m_CD8TPercent = value;
                    this.NotifyPropertyChanged("CD8TPercent");
                    this.CD8TCount = this.CalculateAbsoluteCount(m_CD8TPercent);
                    this.SetCD4CD8Ratio();
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD16CD56NKPercent
        {
            get { return this.m_CD16CD56NKPercent; }
            set
            {
                if (this.m_CD16CD56NKPercent != value)
                {
                    this.m_CD16CD56NKPercent = value;
                    this.NotifyPropertyChanged("CD16CD56NKPercent");
                    this.CD16CD56NKCount = this.CalculateAbsoluteCount(m_CD16CD56NKPercent);
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        
        public string CD3TCount
        {
            get { return this.m_CD3TCount; }
            set
            {
                if (this.m_CD3TCount != value)
                {
                    this.m_CD3TCount = value;
                    this.NotifyPropertyChanged("CD3TCount");
                }
            }
        }

        public string CD4TCount
        {
            get { return this.m_CD4TCount; }
            set
            {
                if (this.m_CD4TCount != value)
                {
                    this.m_CD4TCount = value;
                    this.NotifyPropertyChanged("CD4TCount");
                }
            }
        }
        
        public string CD8TCount
        {
            get { return this.m_CD8TCount; }
            set
            {
                if (this.m_CD8TCount != value)
                {
                    this.m_CD8TCount = value;
                    this.NotifyPropertyChanged("CD8TCount");
                }
            }
        }
        
        public string CD16CD56NKCount
        {
            get { return this.m_CD16CD56NKCount; }
            set
            {
                if (this.m_CD16CD56NKCount != value)
                {
                    this.m_CD16CD56NKCount = value;
                    this.NotifyPropertyChanged("CD16CD56NKCount");
                }
            }
        }

        public string CD4CD8Ratio
        {
            get { return this.m_CD4CD8Ratio; }
            set
            {
                if (this.m_CD4CD8Ratio != value)
                {
                    this.m_CD4CD8Ratio = value;
                    this.NotifyPropertyChanged("CD4CD8Ratio");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD3Percent
        {
            get { return this.m_CD3Percent; }
            set
            {
                if (this.m_CD3Percent != value)
                {
                    this.m_CD3Percent = value;
                    this.NotifyPropertyChanged("CD3Percent");                    
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD4Percent
        {
            get { return this.m_CD4Percent; }
            set
            {
                if (this.m_CD4Percent != value)
                {
                    this.m_CD4Percent = value;
                    this.NotifyPropertyChanged("CD4Percent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD8Percent
        {
            get { return this.m_CD8Percent; }
            set
            {
                if (this.m_CD8Percent != value)
                {
                    this.m_CD8Percent = value;
                    this.NotifyPropertyChanged("CD8Percent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD16Percent
        {
            get { return this.m_CD16Percent; }
            set
            {
                if (this.m_CD16Percent != value)
                {
                    this.m_CD16Percent = value;
                    this.NotifyPropertyChanged("CD16Percent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD19Percent
        {
            get { return this.m_CD19Percent; }
            set
            {
                if (this.m_CD19Percent != value)
                {
                    this.m_CD19Percent = value;
                    this.NotifyPropertyChanged("CD19Percent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD45Percent
        {
            get { return this.m_CD45Percent; }
            set
            {
                if (this.m_CD45Percent != value)
                {
                    this.m_CD45Percent = value;
                    this.NotifyPropertyChanged("CD45Percent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "null", "float")]
        public double? CD56Percent
        {
            get { return this.m_CD56Percent; }
            set
            {
                if (this.m_CD56Percent != value)
                {
                    this.m_CD56Percent = value;
                    this.NotifyPropertyChanged("CD56Percent");
                }
            }
        }

        private string CalculateAbsoluteCount(double? valueToSet)
        {
            string result = "0";
            if (this.m_WBC.HasValue && this.m_LymphocytePercentage.HasValue && valueToSet.HasValue)
            {
                double doubleValue = Math.Round(this.m_WBC.Value * this.m_LymphocytePercentage.Value * valueToSet.Value / 10000, 2);
                int resultValue = Convert.ToInt32(doubleValue);
                result = resultValue.ToString();
            }
            return result;
        }

        private void SetCD4CD8Ratio()
        {
            double? result = null;
            if (this.m_CD4TPercent.HasValue && this.m_CD8TPercent.HasValue)
            {
                result = Math.Round(this.m_CD4TPercent.Value / this.m_CD8TPercent.Value, 2);
            }
            this.CD4CD8Ratio = result.ToString();
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("WBC: " + this.m_CD3TPercent.ToString().StringAsPercent());
            result.AppendLine("Lymphocyte Precentage: " + this.m_CD3TCount);
            result.AppendLine("T-Cell % of Lymphocytes: " + this.m_CD3TPercent.ToString().StringAsPercent());
            result.AppendLine("T-Cell Absolute Count: " + this.m_CD3TCount);
            result.AppendLine("CD4 + T-helper % of Lymphocytes: " + this.m_CD4TPercent.ToString().StringAsPercent());
            result.AppendLine("CD4 + T-helper Absolute Count: " + this.m_CD4TCount);
            result.AppendLine("CD8 + T-helper % of Lymphocytes: " + this.m_CD8TPercent.ToString().StringAsPercent());
            result.AppendLine("CD8 + T-helper Absolute Count: " + this.m_CD8TCount);
            result.AppendLine("NK cells % of Lymphocytes: " + this.m_CD16CD56NKPercent.ToString().StringAsPercent());
            result.AppendLine("NK cells Absolute Count: " + this.m_CD16CD56NKCount);
            result.AppendLine("CD4/CD8 Ratio: " + this.m_CD4CD8Ratio);
            result.AppendLine();

            return result.ToString();
        }
    }
}
