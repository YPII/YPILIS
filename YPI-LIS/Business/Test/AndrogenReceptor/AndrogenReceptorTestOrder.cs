using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.AndrogenReceptor
{
    [PersistentClass("tblAndrogenReceptorTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class AndrogenReceptorTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private bool m_ResultedOnSurgical;

        public AndrogenReceptorTestOrder()
        { }

        public AndrogenReceptorTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        { }

        [PersistentProperty()]
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
        public bool ResultedOnSurgical
        {
            get { return this.m_ResultedOnSurgical; }
            set
            {
                if (this.m_ResultedOnSurgical != value)
                {
                    this.m_ResultedOnSurgical = value;
                    this.NotifyPropertyChanged("ResultedOnSurgical");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            return result.ToString();
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
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
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToFinal;
                }
            }

            return result;
        }
    }
}
