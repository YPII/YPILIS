using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.AbsoluteCD4Count
{    
    public class AbsoluteCD4CountTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{        
        private int m_ReportOrderAbsoluteCD4CountId;        
        private string m_CD3Result;
        private string m_CD4Result;
        private string m_CD8Result;
        private string m_CD4CD8Ratio;
        private string m_Interpretation;        

        public AbsoluteCD4CountTestOrder()
        {

        }

		public AbsoluteCD4CountTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTest panelSet,
			YellowstonePathology.Business.Test.AliquotOrder block,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
		}

        public int ReportOrderAbsoluteCD4CountId
        {
            get { return this.m_ReportOrderAbsoluteCD4CountId; }
            set
            {
                if (this.m_ReportOrderAbsoluteCD4CountId != value)
                {
                    this.m_ReportOrderAbsoluteCD4CountId = value;
                    this.NotifyPropertyChanged("ReportOrderAbsoluteCD4CountId");
                }
            }
        }        

        public string CD3Result
        {
            get { return this.m_CD3Result; }
            set
            {
                if (this.m_CD3Result != value)
                {
                    this.m_CD3Result = value;
                    this.NotifyPropertyChanged("CD3Result");
                }
            }
        }

        public string CD4Result
        {
            get { return this.m_CD4Result; }
            set
            {
                if (this.m_CD4Result != value)
                {
                    this.m_CD4Result = value;
                    this.NotifyPropertyChanged("CD4Result");
                }
            }
        }

        public string CD8Result
        {
            get { return this.m_CD8Result; }
            set
            {
                if (this.m_CD8Result != value)
                {
                    this.m_CD8Result = value;
                    this.NotifyPropertyChanged("CD8Result");
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
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation  = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }       		

		public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			result.Append("CD3: ");
			result.AppendLine(m_CD3Result);
			result.AppendLine();

			result.Append("CD4: ");
			result.AppendLine(m_CD4Result);
			result.AppendLine();

			result.Append("CD8: ");
			result.AppendLine(m_CD8Result);
			result.AppendLine();

			result.Append("CD4 CD8 Ratio: ");
			result.AppendLine(m_CD4CD8Ratio);
			result.AppendLine();

			result.Append("Interpretation: ");
			result.AppendLine(m_Interpretation);

			return result.ToString();
		}
	}
}
