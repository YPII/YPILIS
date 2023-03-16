using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Audit.Model;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    [PersistentClass("tblHER2AmplificationSummaryTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class HER2AnalysisSummaryTestOrder : PanelSetOrder
    {
        private string m_Result;
        private string m_ResultComment;
        private string m_InterpretiveComment;
        private string m_ReportReference;

        public HER2AnalysisSummaryTestOrder()
        { }

        public HER2AnalysisSummaryTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
        }

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
        public string ResultComment
        {
            get { return this.m_ResultComment; }
            set
            {
                if (this.m_ResultComment != value)
                {
                    this.m_ResultComment = value;
                    this.NotifyPropertyChanged("ResultComment");
                }
            }
        }

        [PersistentProperty()]
        public string InterpretiveComment
        {
            get { return this.m_InterpretiveComment; }
            set
            {
                if (this.m_InterpretiveComment != value)
                {
                    this.m_InterpretiveComment = value;
                    this.NotifyPropertyChanged("InterpretiveComment");
                }
            }
        }

        [PersistentProperty()]
        public string ReportReference
        {
            get { return this.m_ReportReference; }
            set
            {
                if (this.m_ReportReference != value)
                {
                    this.m_ReportReference = value;
                    this.NotifyPropertyChanged("ReportReference");
                }
            }
        }

        public void SetValues()
        {
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string result = "HER2 Amplification Summary: " + this.Result;
            return result;
        }

        public AuditResult IsOkToSetResults(AccessionOrder accessionOrder)
        {
            AuditResult result = new AuditResult();
            result.Status = AuditStatusEnum.OK;

            HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();
            
            if(accessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId) == true)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTestOrder ishTestOrder = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_OrderedOnId, true);
                if(ishTestOrder.Indicator == "Breast Metastatic")
                {
                    result.Status = AuditStatusEnum.OK;
                }
                else
                {
                    if (ishTestOrder.Final == false)
                    {
                        result.Status = AuditStatusEnum.Failure;
                        result.Message += "The " + ishTest.PanelSetName + " must be final before results can be set." + Environment.NewLine;
                    }
                    else
                    {
                        if (accessionOrder.PanelSetOrderCollection.Exists(ihcTest.PanelSetId, this.OrderedOnId, true) == true)
                        {
                            Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC testOrder = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.OrderedOnId, true);
                            if (testOrder.Final == false)
                            {
                                result.Status = AuditStatusEnum.Failure;
                                result.Message += "The " + ihcTest.PanelSetName + " must be final before results can be set." + Environment.NewLine;
                            }
                            else
                            {
                                if (testOrder.Score == "2+")
                                {
                                    if (accessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.OrderedOnId, true) == true)
                                    {
                                        HER2AmplificationRecount.HER2AmplificationRecountTestOrder recountTestOrder = (HER2AmplificationRecount.HER2AmplificationRecountTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.OrderedOnId, true);
                                        if (recountTestOrder.Final == false)
                                        {
                                            result.Status = AuditStatusEnum.Failure;
                                            result.Message += "The " + recountTest.PanelSetName + " must be final before results can be set." + Environment.NewLine;
                                        }
                                    }
                                    else
                                    {
                                        result.Status = AuditStatusEnum.Failure;
                                        result.Message += "A " + recountTest.PanelSetName + " must be ordered and final before results can be set." + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else
                        {
                            result.Status = AuditStatusEnum.Failure;
                            result.Message += "A " + ihcTest.PanelSetName + " must be ordered and final before results can be set." + Environment.NewLine;
                        }

                        if (result.Status == AuditStatusEnum.OK)
                        {
                            if (this.m_Accepted == true)
                            {
                                result.Status = AuditStatusEnum.Failure;
                                result.Message = "The results may not be set because they have already been accepted." + Environment.NewLine;
                            }
                        }
                    }
                }                
            }
            else
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message += "A " + ishTest.PanelSetName + " must be ordered and final before results can be set." + Environment.NewLine;
            }

            return result;
        }

        public void SetResults(AccessionOrder accessionOrder)
        {
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "The result may not be accepted because the result is not set.";
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
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to final as the result is not set.";
                }
            }

            return result;
        }

    }
}
