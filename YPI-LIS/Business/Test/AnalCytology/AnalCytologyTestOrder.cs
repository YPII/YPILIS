using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.AnalCytology
{
    [PersistentClass("tblAnalCytologyTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class AnalCytologyTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_SpecimenAdequacy;
        private string m_ScreeningImpression;
        private string m_OtherConditions;
        private string m_ReportComment;
        private string m_Method;

        public AnalCytologyTestOrder()
        { }

        public AnalCytologyTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        [PersistentProperty()]
        public string SpecimenAdequacy
        {
            get { return this.m_SpecimenAdequacy; }
            set
            {
                if (this.m_SpecimenAdequacy != value)
                {
                    this.m_SpecimenAdequacy = value;
                    this.NotifyPropertyChanged("SpecimenAdequacy");
                }
            }
        }

        [PersistentProperty()]
        public string ScreeningImpression
        {
            get { return this.m_ScreeningImpression; }
            set
            {
                if (this.m_ScreeningImpression != value)
                {
                    this.m_ScreeningImpression = value;
                    this.NotifyPropertyChanged("ScreeningImpression");
                }
            }
        }

        [PersistentProperty()]
        public string OtherConditions
        {
            get { return this.m_OtherConditions; }
            set
            {
                if (this.m_OtherConditions != value)
                {
                    this.m_OtherConditions = value;
                    this.NotifyPropertyChanged("OtherConditions");
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
                    this.NotifyPropertyChanged("ReportComment");
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

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_ScreeningImpression);
            result.AppendLine();

            return result.ToString();
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_ScreeningImpression) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToAccept;
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_ScreeningImpression) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToFinal;
                }
            }

            return result;
        }
    }
}
