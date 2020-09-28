using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.FetalHemoglobinV2
{
    [PersistentClass("tblFetalHemoglobinV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class FetalHemoglobinV2TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_HbFPercent;
        private string m_HbFReferenceRange;
        private string m_MothersHeightFeet;
        private string m_MothersHeightInches;
        private string m_MothersHeightCM;
        private string m_MothersWeight;
        private string m_MothersWeightKG;
        private string m_MothersBloodVolume;
        private bool m_HeightWeightNotProvided;
        private string m_FetalBleed;
        private string m_FetalBleedReferenceRange;
        private string m_RhImmuneGlobulin;
        private string m_ReportComment;
        private string m_ASRComment;
        private string m_Method;
        private string m_NotificationComment;

        public FetalHemoglobinV2TestOrder()
        { }

        public FetalHemoglobinV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_HbFReferenceRange = "Less than or equal to 0.04%";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR’s) were developed and performance characteristics determined " +
                "by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The " +
                "FDA has determined that such clearance or approval is not necessary.  ASR’s may be used for clinical purposes and should not " +
                "be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement " +
                "Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
        }

        [PersistentProperty()]
        public string HbFPercent
        {
            get { return this.m_HbFPercent; }
            set
            {
                if (this.m_HbFPercent != value)
                {
                    this.m_HbFPercent = value;
                    NotifyPropertyChanged("HbFPercent");
                }
            }
        }

        [PersistentProperty()]
        public string ReportComment
        {
            get { return this.m_ReportComment; }
            set
            {
                if (this.m_ReportComment != value)
                {
                    this.m_ReportComment = value;
                    NotifyPropertyChanged("ReportComment");
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
        public string HbFReferenceRange
        {
            get { return this.m_HbFReferenceRange; }
            set
            {
                if (this.m_HbFReferenceRange != value)
                {
                    this.m_HbFReferenceRange = value;
                    NotifyPropertyChanged("HbFReferenceRange");
                }
            }
        }

        [PersistentProperty()]
        public string FetalBleedReferenceRange
        {
            get { return this.m_FetalBleedReferenceRange; }
            set
            {
                if (this.m_FetalBleedReferenceRange != value)
                {
                    this.m_FetalBleedReferenceRange = value;
                    NotifyPropertyChanged("FetalBleedReferenceRange");
                }
            }
        }

        [PersistentProperty()]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    NotifyPropertyChanged("ASRComment");
                }
            }
        }

        [PersistentProperty()]
        public string MothersHeightFeet
        {
            get { return this.m_MothersHeightFeet; }
            set
            {
                if (this.m_MothersHeightFeet != value)
                {
                    this.m_MothersHeightFeet = value;
                    NotifyPropertyChanged("MothersHeightFeet");
                }
            }
        }

        [PersistentProperty()]
        public string MothersHeightInches
        {
            get { return this.m_MothersHeightInches; }
            set
            {
                if (this.m_MothersHeightInches != value)
                {
                    this.m_MothersHeightInches = value;
                    NotifyPropertyChanged("MothersHeightInches");
                }
            }
        }

        [PersistentProperty()]
        public string MothersHeightCM
        {
            get { return this.m_MothersHeightCM; }
            set
            {
                if (this.m_MothersHeightCM != value)
                {
                    this.m_MothersHeightCM = value;
                    NotifyPropertyChanged("MothersHeightCM");
                }
            }
        }

        [PersistentProperty()]
        public string MothersWeight
        {
            get { return this.m_MothersWeight; }
            set
            {
                if (this.m_MothersWeight != value)
                {
                    this.m_MothersWeight = value;
                    NotifyPropertyChanged("MothersWeight");
                }
            }
        }

        [PersistentProperty()]
        public string MothersWeightKG
        {
            get { return this.m_MothersWeightKG; }
            set
            {
                if (this.m_MothersWeightKG != value)
                {
                    this.m_MothersWeightKG = value;
                    NotifyPropertyChanged("MothersWeightKG");
                }
            }
        }

        [PersistentProperty()]
        public bool HeightWeightNotProvided
        {
            get { return this.m_HeightWeightNotProvided; }
            set
            {
                if (this.m_HeightWeightNotProvided != value)
                {
                    this.m_HeightWeightNotProvided = value;
                    NotifyPropertyChanged("HeightWeightNotProvided");
                }
            }
        }

        [PersistentProperty()]
        public string MothersBloodVolume
        {
            get { return this.m_MothersBloodVolume; }
            set
            {
                if (this.m_MothersBloodVolume != value)
                {
                    this.m_MothersBloodVolume = value;
                    NotifyPropertyChanged("MothersBloodVolume");
                }
            }
        }

        [PersistentProperty()]
        public string FetalBleed
        {
            get { return this.m_FetalBleed; }
            set
            {
                if (this.m_FetalBleed != value)
                {
                    this.m_FetalBleed = value;
                    NotifyPropertyChanged("FetalBleed");
                }
            }
        }

        [PersistentProperty()]
        public string RhImmuneGlobulin
        {
            get { return this.m_RhImmuneGlobulin; }
            set
            {
                if (this.m_RhImmuneGlobulin != value)
                {
                    this.m_RhImmuneGlobulin = value;
                    NotifyPropertyChanged("RhImmuneGlobulin");
                }
            }
        }

        [PersistentProperty()]
        public string NotificationComment
        {
            get { return this.m_NotificationComment; }
            set
            {
                if (this.m_NotificationComment != value)
                {
                    this.m_NotificationComment = value;
                    NotifyPropertyChanged("NotificationComment");
                }
            }
        }

        public void SetMothersBloodVolume()
        {
            if (string.IsNullOrEmpty(this.m_MothersWeight) == true || string.IsNullOrEmpty(this.m_MothersHeightFeet) == true)
            {
                this.MothersBloodVolume = "5000";
            }
            else
            {
                if (string.IsNullOrEmpty(this.m_MothersWeight) == false &&
                    string.IsNullOrEmpty(this.m_MothersHeightFeet) == false &&
                    string.IsNullOrEmpty(this.m_MothersHeightInches) == false)
                {
                    double heightFeet = float.Parse(this.m_MothersHeightFeet);
                    double heightInches = float.Parse(this.m_MothersHeightInches);
                    double totalInches = heightFeet * 12 + heightInches;
                    double weightInLbs = float.Parse(this.m_MothersWeight);

                    double bloodVolume = 2370.0 * Math.Pow(((totalInches * weightInLbs) / 3131.0), .5);
                    
                    this.m_MothersBloodVolume = Math.Round(bloodVolume).ToString();
                    this.NotifyPropertyChanged("MothersBloodVolume");
                }
            }
        }

        public void SetFetalBleed()
        {
            if (this.m_HeightWeightNotProvided == true)
            {
                this.m_MothersBloodVolume = "5000";
                this.NotifyPropertyChanged("MothersBloodVolume");
            }

            if (string.IsNullOrEmpty(this.m_HbFPercent) == false)
            {               
                if (string.IsNullOrEmpty(this.m_HbFPercent) == false && string.IsNullOrEmpty(this.m_MothersBloodVolume) == false)
                {
                    double percentFetalCells = double.Parse(this.m_HbFPercent);
                    double mothersBloodVolume = double.Parse(this.m_MothersBloodVolume);
                    double fetalBleed = percentFetalCells * mothersBloodVolume * 0.01;
                    this.FetalBleed = Math.Round(fetalBleed, 2).ToString();
                }
            }
            else
            {
                this.FetalBleed = null;
            }
        }

        public void HandleHeightConverstion()
        {
            // 1 in = 2.54 cm

            if (this.m_HeightWeightNotProvided == false)
            {
                if (string.IsNullOrEmpty(this.m_MothersHeightFeet) == true)
                {
                    if (string.IsNullOrEmpty(this.m_MothersHeightCM) == false)
                    {
                        Double totalInches = Convert.ToDouble(this.m_MothersHeightCM) / 2.54;
                        double feet = Math.Floor(totalInches / 12);
                        double inches = Convert.ToInt32(totalInches % 12);
                        this.m_MothersHeightFeet = feet.ToString();
                        this.m_MothersHeightInches = inches.ToString();
                        this.NotifyPropertyChanged("MothersHeightFeet");
                        this.NotifyPropertyChanged("MothersHeightInches");
                    }
                }
                else if (string.IsNullOrEmpty(this.m_MothersHeightCM) == true)
                {
                    if(string.IsNullOrEmpty(this.m_MothersHeightFeet) == false)
                    {
                        double totalCM = (Convert.ToDouble(this.m_MothersHeightFeet) * 12 + Convert.ToDouble(this.m_MothersHeightInches)) * 2.54;
                        this.m_MothersHeightCM = totalCM.ToString();
                        this.NotifyPropertyChanged("MothersHeightCM");
                    }
                }
            }
            else
            {
                this.m_MothersHeightFeet = null;
                this.m_MothersHeightInches = null;
                this.m_MothersHeightCM = null;
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        public void HandleWeightConversion()
        {
            if (this.m_HeightWeightNotProvided == false)
            {
                if (string.IsNullOrEmpty(this.m_MothersWeight) == true)
                {
                    if (string.IsNullOrEmpty(this.m_MothersWeightKG) == false)
                    {
                        double lbs = Convert.ToDouble(this.m_MothersWeightKG) * 2.20462;
                        this.m_MothersWeight = lbs.ToString();
                        this.NotifyPropertyChanged("MothersWeight");
                    }
                }
                else if (string.IsNullOrEmpty(this.m_MothersWeightKG) == true)
                {
                    if (string.IsNullOrEmpty(this.m_MothersWeight) == false)
                    {
                        double kg = Convert.ToDouble(this.m_MothersWeight) / 2.20462;
                        this.m_MothersWeightKG = Math.Round(kg, 2).ToString();
                        this.NotifyPropertyChanged("MothersWeightKG");
                    }
                }
            }
            else
            {
                this.m_MothersWeightKG = null;
                this.m_MothersWeight = null;
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        public void SetRecommendedNumberOfVials()
        {
            if (string.IsNullOrEmpty(this.m_FetalBleed) == false)
            {
                double fetalBleed = Convert.ToDouble(this.m_FetalBleed);                                                    
                double vials = fetalBleed / 30;
                double partialValue = vials - Math.Truncate(vials);
                int additionalVials = partialValue >= 0.5 ? 2 : 1;
                int recommendedNumberOfVials = (int)Math.Truncate(vials) + additionalVials;
                this.RhImmuneGlobulin = recommendedNumberOfVials.ToString();                
            }
            else
            {
                this.RhImmuneGlobulin = null;
            }
        }

        public void SetMethod()
        {
            string method = "Quantitative Flow Cytometry" + Environment.NewLine + "Sensitivity for Hb - F is <= 0.02 %." + Environment.NewLine;
            string comment = "";

            if(this.m_HeightWeightNotProvided == false)
            {
                method += "Patient blood volume, Fetal - Maternal Bleed quantity and Rh Immune Globulin(RhIg) dosing are calculated from patient height and weight data submitted with the test request.  If height and weight data are not received, then a default patient blood volume of 5000 mL is used for the Fetal-Maternal Bleed quantity and RhIg dose. " + Environment.NewLine +
                    "Patient Height: [HEIGHTCM] cm " + Environment.NewLine +
                    "Patient Weight: [WEIGHTKG] kg " + Environment.NewLine +
                    "Patient calculated blood volume: [BLOODVOLUME] mL";
                method = method.Replace("[HEIGHTCM]", this.m_MothersHeightCM);
                method = method.Replace("[WEIGHTKG]", this.m_MothersWeightKG);
                method = method.Replace("[BLOODVOLUME]", this.MothersBloodVolume);

                comment = "Fetal-Maternal Bleed quantity and RhIg dose recommendation are based on the following patient data provided with the test request:  Height [HEIGHTCM] cm, Weight [WEIGHTKG] kg.  Patient calculated blood volume is [BLOODVOLUME] mL.";
                comment = comment.Replace("[HEIGHTCM]", this.m_MothersHeightCM);
                comment = comment.Replace("[WEIGHTKG]", this.m_MothersWeightKG);
                comment = comment.Replace("[BLOODVOLUME]", this.m_MothersBloodVolume);
            }
            else
            {
                method += "Patient blood volume, Fetal-Maternal Bleed quantity and Rh Immune Globulin (RhIg) dosing are calculated from patient height and weight data submitted with the test request.  If height and weight data are not received, then a default patient blood volume of 5000 mL is used for the Fetal-Maternal Bleed quantity and RhIg dose. " + Environment.NewLine +
                    "Patient Height: None submitted" + Environment.NewLine +
                    "Patient Weight: None submitted" + Environment.NewLine +
                    "Patient default blood volume: 5000 mL";
                comment = "Specific patient height and weight information was not provided for this patient, and therefore the patient’s specific blood volume cannot be calculated.  A default blood volume of 5000 mL was used as the basis of the RhIg dose recommendation.  Please complete a patient specific calculation for more precise dosing, if necessary.";
            }

            this.m_Method = method;
            this.m_ReportComment = comment;

            this.NotifyPropertyChanged("Method");
            this.NotifyPropertyChanged("ReportComment");
        }        

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Hb-F percent: " + this.m_HbFPercent);
            result.AppendLine();

            result.AppendLine("Fetal Bleed: " + this.m_FetalBleed);
            result.AppendLine();

            result.AppendLine("Rh Immune Globulin: " + this.m_RhImmuneGlobulin);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_HbFPercent) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Hb-F percent" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_FetalBleed) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Fetal Bleed" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_RhImmuneGlobulin) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Rh Immune Globulin";
                }
            }

            if (result.Status == AuditStatusEnum.Failure)
            {
                result.Message = "Unable to accept as the following result/s are not set: " + Environment.NewLine + result.Message;
            }

            return result;
        }
    }
}
