using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Rules;
using YellowstonePathology.Business.Audit.Model;

namespace YellowstonePathology.Business.Test.HER2AmplificationRecount
{
    [PersistentClass("tblHER2AmplificationRecountTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationRecountTestOrder : PanelSetOrder
    {
        private int m_CellsCounted;
        private int m_Chr17SignalsCounted;
        private int m_Her2SignalsCounted;

        public HER2AmplificationRecountTestOrder()
        {
        }

        public HER2AmplificationRecountTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_Distribute = false;
            this.NoCharge = true;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int CellsCounted
        {
            get { return this.m_CellsCounted; }
            set
            {
                if (this.m_CellsCounted != value)
                {
                    this.m_CellsCounted = value;
                    this.NotifyPropertyChanged("CellsCounted");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int Chr17SignalsCounted
        {
            get { return this.m_Chr17SignalsCounted; }
            set
            {
                if (this.m_Chr17SignalsCounted != value)
                {
                    this.m_Chr17SignalsCounted = value;
                    this.NotifyPropertyChanged("Chr17SignalsCounted");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int Her2SignalsCounted
        {
            get { return this.m_Her2SignalsCounted; }
            set
            {
                if (this.m_Her2SignalsCounted != value)
                {
                    this.m_Her2SignalsCounted = value;
                    this.NotifyPropertyChanged("Her2SignalsCounted");
                }
            }
        }

        public int CellsCountedUI
        {
            get { return this.m_CellsCounted; }
            set
            {
                if (this.m_CellsCounted != value)
                {
                    this.CellsCounted = value;
                    NotifyPropertyChanged("CellsCountedUI");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int Chr17SignalsCountedUI
        {
            get { return this.m_Chr17SignalsCounted; }
            set
            {
                if (this.m_Chr17SignalsCounted != value)
                {
                    this.Chr17SignalsCounted = value;
                    NotifyPropertyChanged("Chr17SignalsCountedUI");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public int Her2SignalsCountedUI
        {
            get { return this.m_Her2SignalsCounted; }
            set
            {
                if (this.m_Her2SignalsCounted != value)
                {
                    this.Her2SignalsCounted = value;
                    NotifyPropertyChanged("Her2SignalsCountedUI");
                    NotifyPropertyChanged("AverageHer2Chr17Signal");
                }
            }
        }

        public string AverageHer2Chr17Signal
        {
            get
            {
                string ratio = "Unable to calculate";
                Nullable<double> dratio = this.AverageHer2Chr17SignalAsDouble;
                if (dratio.HasValue)
                {
                    ratio = Convert.ToString(Math.Round((dratio.Value), 2));
                }
                return ratio;
            }
            set { }
        }

        public Nullable<double> AverageHer2Chr17SignalAsDouble
        {
            get
            {
                Nullable<double> dratio = null;
                if (Chr17SignalsCounted > 0 && Her2SignalsCounted > 0 && CellsCounted > 0)
                {
                    dratio = ((double)Her2SignalsCounted / (double)CellsCounted) / ((double)Chr17SignalsCounted / (double)CellsCounted);
                }
                return dratio;
            }
        }

        public Nullable<double> AverageHer2NeuSignal
        {
            get
            {
                Nullable<double> result = null;
                if (Her2SignalsCounted > 0 && CellsCounted > 0)
                {
                    double dratio = (double)Her2SignalsCounted / (double)CellsCounted;
                    result = Convert.ToDouble(Math.Round((dratio), 2));
                }
                return result;
            }
            set { }
        }

        public string AverageChr17Signal
        {
            get
            {
                string ratio = "Unable to calculate";
                if (Chr17SignalsCounted > 0 && CellsCounted > 0)
                {
                    double dratio = (double)Chr17SignalsCounted / (double)CellsCounted;
                    ratio = Convert.ToString(Math.Round((dratio), 2));
                }
                return ratio;
            }
            set { }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Cells Counted: " + this.m_CellsCounted.ToString());
            result.AppendLine();

            result.AppendLine("Chr!7 Signals Counted: " + this.m_Chr17SignalsCounted.ToString());
            result.AppendLine();

            result.AppendLine("Her2 Signals Counted: " + this.m_Her2SignalsCounted);
            result.AppendLine();

            return result.ToString();
        }

        public override MethodResult IsOkToAccept()
        {
            MethodResult result = base.IsOkToAccept();
            if(result.Success == true)
            {
                if(this.m_CellsCounted == 0)
                {
                    result.Success = false;
                    result.Message = "Cells Counted must be entered.";
                }
                if (this.m_Her2SignalsCounted == 0)
                {
                    result.Success = false;
                    result.Message += "Her2 Signals Counted must be entered.";
                }
                if (this.m_Chr17SignalsCounted == 0)
                {
                    result.Success = false;
                    result.Message += "Chr17 Signals Counted must be entered.";
                }
            }
            return result;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;
            Rules.MethodResult methodResult = base.IsOkToFinalize();
            if (methodResult.Success == false)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = methodResult.Message;
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (this.m_CellsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to final as Total Cells Counted is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (this.m_Her2SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to final as HER2 Signals Counted is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (this.m_Chr17SignalsCounted == 0)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to final as Chr17 Signals Counted is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                HER2AmplificationSummary.HER2AmplificationSummaryTest test = new HER2AmplificationSummary.HER2AmplificationSummaryTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_OrderedOnId, true) == false)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to finalize as a " + test.PanelSetName + " is required.";
                }
            }

            return result;
        }

        public override FinalizeTestResult Finish(AccessionOrder accessionOrder)
        {
            HER2AmplificationSummary.HER2AmplificationSummaryTest test = new HER2AmplificationSummary.HER2AmplificationSummaryTest();
            HER2AmplificationSummary.HER2AmplificationSummaryTestOrder testOrder = (HER2AmplificationSummary.HER2AmplificationSummaryTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId, this.m_OrderedOnId, true);
            testOrder.SetValues(accessionOrder);
            return base.Finish(accessionOrder);
        }
    }
}
