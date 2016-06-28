using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    [PersistentClass("tblRASRAFPanelTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class RASRAFPanelTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
    	
        private string m_KRASResult;
        private string m_NRASResult;
        private string m_HRASResult;
        private string m_BRAFResult;
        private string m_KRASMutationName;
        private string m_KRASAlternateNucleotideMutationName;
        private string m_KRASConsequence;
        private string m_KRASPredictedEffectOnProtein;
        private string m_NRASMutationName;
        private string m_NRASAlternateNucleotideMutationName;
        private string m_NRASConsequence;
        private string m_NRASPredictedEffectOnProtein;
        private string m_HRASMutationName;
        private string m_HRASAlternateNucleotideMutationName;
        private string m_HRASConsequence;
        private string m_HRASPredictedEffectOnProtein;
        private string m_BRAFMutationName;
        private string m_BRAFAlternateNucleotideMutationName;
        private string m_BRAFConsequence;
        private string m_BRAFPredictedEffectOnProtein;
        private string m_Method;
        private string m_References;
        private string m_ReportDisclaimer;
        private string m_Interpretation;
        private string m_Comment;

        public RASRAFPanelTestOrder()
        {

        }

        public RASRAFPanelTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
        	this.m_Method = RASRAFPanelResult.Method;
        	this.m_References = RASRAFPanelResult.References;
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

        [PersistentProperty()]
        public string KRASResult
        {
            get { return this.m_KRASResult; }
            set
            {
                if (this.m_KRASResult != value)
                {
                    this.m_KRASResult = value;
                    this.NotifyPropertyChanged("KRASResult");
                }
            }
        }

        [PersistentProperty()]
        public string NRASResult
        {
            get { return this.m_NRASResult; }
            set
            {
                if (this.m_NRASResult != value)
                {
                    this.m_NRASResult = value;
                    this.NotifyPropertyChanged("NRASResult");
                }
            }
        }

        [PersistentProperty()]
        public string HRASResult
        {
            get { return this.m_HRASResult; }
            set
            {
                if (this.m_HRASResult != value)
                {
                    this.m_HRASResult = value;
                    this.NotifyPropertyChanged("HRASResult");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFResult
        {
            get { return this.m_BRAFResult; }
            set
            {
                if (this.m_BRAFResult != value)
                {
                    this.m_BRAFResult = value;
                    this.NotifyPropertyChanged("BRAFResult");
                }
            }
        }

        [PersistentProperty()]
        public string KRASMutationName
        {
            get { return this.m_KRASMutationName; }
            set
            {
                if (this.m_KRASMutationName != value)
                {
                    this.m_KRASMutationName = value;
                    this.NotifyPropertyChanged("KRASMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string KRASAlternateNucleotideMutationName
        {
            get { return this.m_KRASAlternateNucleotideMutationName; }
            set
            {
                if (this.m_KRASAlternateNucleotideMutationName != value)
                {
                    this.m_KRASAlternateNucleotideMutationName = value;
                    this.NotifyPropertyChanged("KRASAlternateNucleotideMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string KRASConsequence
        {
            get { return this.m_KRASConsequence; }
            set
            {
                if (this.m_KRASConsequence != value)
                {
                    this.m_KRASConsequence = value;
                    this.NotifyPropertyChanged("KRASConsequence");
                }
            }
        }

        [PersistentProperty()]
        public string KRASPredictedEffectOnProtein
        {
            get { return this.m_KRASPredictedEffectOnProtein; }
            set
            {
                if (this.m_KRASPredictedEffectOnProtein != value)
                {
                    this.m_KRASPredictedEffectOnProtein = value;
                    this.NotifyPropertyChanged("KRASPredictedEffectOnProtein");
                }
            }
        }

        [PersistentProperty()]
        public string NRASMutationName
        {
            get { return this.m_NRASMutationName; }
            set
            {
                if (this.m_NRASMutationName != value)
                {
                    this.m_NRASMutationName = value;
                    this.NotifyPropertyChanged("NRASMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string NRASAlternateNucleotideMutationName
        {
            get { return this.m_NRASAlternateNucleotideMutationName; }
            set
            {
                if (this.m_NRASAlternateNucleotideMutationName != value)
                {
                    this.m_NRASAlternateNucleotideMutationName = value;
                    this.NotifyPropertyChanged("NRASAlternateNucleotideMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string NRASConsequence
        {
            get { return this.m_NRASConsequence; }
            set
            {
                if (this.m_NRASConsequence != value)
                {
                    this.m_NRASConsequence = value;
                    this.NotifyPropertyChanged("NRASConsequence");
                }
            }
        }

        [PersistentProperty()]
        public string NRASPredictedEffectOnProtein
        {
            get { return this.m_NRASPredictedEffectOnProtein; }
            set
            {
                if (this.m_NRASPredictedEffectOnProtein != value)
                {
                    this.m_NRASPredictedEffectOnProtein = value;
                    this.NotifyPropertyChanged("NRASPredictedEffectOnProtein");
                }
            }
        }

        [PersistentProperty()]
        public string HRASMutationName
        {
            get { return this.m_HRASMutationName; }
            set
            {
                if (this.m_HRASMutationName != value)
                {
                    this.m_HRASMutationName = value;
                    this.NotifyPropertyChanged("HRASMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string HRASAlternateNucleotideMutationName
        {
            get { return this.m_HRASAlternateNucleotideMutationName; }
            set
            {
                if (this.m_HRASAlternateNucleotideMutationName != value)
                {
                    this.m_HRASAlternateNucleotideMutationName = value;
                    this.NotifyPropertyChanged("HRASAlternateNucleotideMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string HRASConsequence
        {
            get { return this.m_HRASConsequence; }
            set
            {
                if (this.m_HRASConsequence != value)
                {
                    this.m_HRASConsequence = value;
                    this.NotifyPropertyChanged("HRASConsequence");
                }
            }
        }

        [PersistentProperty()]
        public string HRASPredictedEffectOnProtein
        {
            get { return this.m_HRASPredictedEffectOnProtein; }
            set
            {
                if (this.m_HRASPredictedEffectOnProtein != value)
                {
                    this.m_HRASPredictedEffectOnProtein = value;
                    this.NotifyPropertyChanged("HRASPredictedEffectOnProtein");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFMutationName
        {
            get { return this.m_BRAFMutationName; }
            set
            {
                if (this.m_BRAFMutationName != value)
                {
                    this.m_BRAFMutationName = value;
                    this.NotifyPropertyChanged("BRAFMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFAlternateNucleotideMutationName
        {
            get { return this.m_BRAFAlternateNucleotideMutationName; }
            set
            {
                if (this.m_BRAFAlternateNucleotideMutationName != value)
                {
                    this.m_BRAFAlternateNucleotideMutationName = value;
                    this.NotifyPropertyChanged("BRAFAlternateNucleotideMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFConsequence
        {
            get { return this.m_BRAFConsequence; }
            set
            {
                if (this.m_BRAFConsequence != value)
                {
                    this.m_BRAFConsequence = value;
                    this.NotifyPropertyChanged("BRAFConsequence");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFPredictedEffectOnProtein
        {
            get { return this.m_BRAFPredictedEffectOnProtein; }
            set
            {
                if (this.m_BRAFPredictedEffectOnProtein != value)
                {
                    this.m_BRAFPredictedEffectOnProtein = value;
                    this.NotifyPropertyChanged("BRAFPredictedEffectOnProtein");
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
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.Append("BRAF Result: ");
            result.AppendLine(this.m_BRAFResult);
            result.Append("KRAS Result: ");
            result.AppendLine(this.m_KRASResult);
            result.Append("NRAS Result: ");
            result.AppendLine(this.m_NRASResult);
            result.Append("HRAS Result: ");
            result.AppendLine(this.m_HRASResult);
            return result.ToString();
        }
        
        public YellowstonePathology.Business.Rules.MethodResult IsOkToSet()
        {
        	YellowstonePathology.Business.Rules.MethodResult methodResult = new YellowstonePathology.Business.Rules.MethodResult();
        	if(this.Accepted == true)
        	{
        		methodResult.Success = false;
        		methodResult.Message = "Unable to set results as the results have already been accepted.";
        	}
        		
        	return methodResult;
        }

        public void SetBrafSummaryResult(YellowstonePathology.Business.Test.LynchSyndrome.LSEResult lSEResult)
        {
            if (string.IsNullOrEmpty(this.m_BRAFResult) == false)
            {
                if (this.m_BRAFResult == "Not Detected")
                {
                    lSEResult.BrafResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Negative;
                }
                else if (this.m_BRAFResult == "Detected")
                {
                    lSEResult.BrafResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Positive;
                }
            }
        }

    }
}
