﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AmplificationSummaryTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Interpretation;
        private string m_HER2CEP17Ratio;
        private string m_AverageHER2CopyNo;
        private string m_HER2ByIHCScore;
        private bool m_HER2ByIHCRequired;
        private bool m_HER2ByIHCIsOrdered;
        private bool m_HER2ByIHCIsAccepted;
        private bool m_RequiresBlindedObserver;

        public HER2AmplificationSummaryTestOrder()
        { }

        public HER2AmplificationSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

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
        public string HER2CEP17Ratio
        {
            get { return m_HER2CEP17Ratio; }
            set
            {
                if (this.m_HER2CEP17Ratio != value)
                {
                    this.m_HER2CEP17Ratio = value;
                    this.NotifyPropertyChanged("HER2CEP17Ratio");
                }
            }
        }

        [PersistentProperty()]
        public string AverageHER2CopyNo
        {
            get { return m_AverageHER2CopyNo; }
            set
            {
                if (this.m_AverageHER2CopyNo != value)
                {
                    this.m_AverageHER2CopyNo = value;
                    this.NotifyPropertyChanged("AverageHER2CopyNo");
                }
            }
        }

        [PersistentProperty()]
        public string HER2ByIHCScore
        {
            get { return m_HER2ByIHCScore; }
            set
            {
                if (this.m_HER2ByIHCScore != value)
                {
                    this.m_HER2ByIHCScore = value;
                    this.NotifyPropertyChanged("HER2ByIHCScore");
                }
            }
        }

        [PersistentProperty()]
        public bool HER2ByIHCRequired
        {
            get { return m_HER2ByIHCRequired; }
            set
            {
                if (this.m_HER2ByIHCRequired != value)
                {
                    this.m_HER2ByIHCRequired = value;
                    this.NotifyPropertyChanged("HER2ByIHCRequired");
                }
            }
        }

        [PersistentProperty()]
        public bool HER2ByIHCIsOrdered
        {
            get { return m_HER2ByIHCIsOrdered; }
            set
            {
                if (this.m_HER2ByIHCIsOrdered != value)
                {
                    this.m_HER2ByIHCIsOrdered = value;
                    this.NotifyPropertyChanged("HER2ByIHCIsOrdered");
                }
            }
        }

        [PersistentProperty()]
        public bool HER2ByIHCIsAccepted
        {
            get { return m_HER2ByIHCIsAccepted; }
            set
            {
                if (this.m_HER2ByIHCIsAccepted != value)
                {
                    this.m_HER2ByIHCIsAccepted = value;
                    this.NotifyPropertyChanged("HER2ByIHCIsAccepted");
                }
            }
        }

        [PersistentProperty()]
        public bool RequiresBlindedObserver
        {
            get { return m_RequiresBlindedObserver; }
            set
            {
                if (this.m_RequiresBlindedObserver != value)
                {
                    this.m_RequiresBlindedObserver = value;
                    this.NotifyPropertyChanged("RequiresBlindedObserver");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string result = "HER2 Amplification Summary: " + "Under construction";
            return result;
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResults()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            if (this.m_Accepted == true)
            {
                result.Success = false;
                result.Message = "The results may not be set because they have already been accepted.";
            }
            return result;
        }

        public void SetResults()
        {

        }
    }
}
