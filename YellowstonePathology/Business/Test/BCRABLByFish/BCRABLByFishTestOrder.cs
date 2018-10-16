using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BCRABLByFish
{
	[PersistentClass("tblBCRABLByFishTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class BCRABLByFishTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Interpretation;
		private string m_ProbeSetDetail;
		private string m_NucleiScored;

        public BCRABLByFishTestOrder()
        {
        }

		public BCRABLByFishTestOrder(string masterAccessionNo, string reportNo, string objectId,
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
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
        public string ProbeSetDetail
		{
			get { return this.m_ProbeSetDetail; }
			set
			{
				if (this.m_ProbeSetDetail != value)
				{
					this.m_ProbeSetDetail = value;
					this.NotifyPropertyChanged("ProbeSetDetail");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

            result.AppendLine("Probeset Detail: " + this.m_ProbeSetDetail);
            result.AppendLine();

            result.AppendLine("Nuclei Scored: " + this.m_NucleiScored);
            result.AppendLine();

            return result.ToString();
        }

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            Business.Test.BCRABLByFish.BCRABLByFishTestOrder panelSetOrder = (Business.Test.BCRABLByFish.BCRABLByFishTestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.ProbeSetDetail = this.ProbeSetDetail;
            panelSetOrder.NucleiScored = this.NucleiScored;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Interpretation = null;
            this.m_ProbeSetDetail = null;
            this.m_NucleiScored = null;
            base.ClearPreviousResults();
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToAccept + Environment.NewLine;
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToFinal + Environment.NewLine;
                }
            }
            return result;
        }
    }
}
