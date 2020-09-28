using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CheckSuggestedOrdersOnFlowFinalAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma m_PanelSetOrder;
        private bool m_HasIndication;
        private bool m_HasCLLByFish;
        private bool m_HasIGVH;
        private bool m_HasNeotypeCLLPrognosticPanel;
        private bool m_IsNewDiagnosis;
        private bool m_UseFCCRule;
        private bool m_UseTCIRule;
        private bool m_UseNMHRule;

        private List<int> m_FCCClients;
        private List<int> m_TCIClients;
        private List<int> m_NMHClients;

        public CheckSuggestedOrdersOnFlowFinalAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;

            this.m_HasIndication = false;
            if(string.IsNullOrEmpty(this.m_PanelSetOrder.Impression) == false)
            {
                this.m_HasIndication = this.m_PanelSetOrder.Impression.Contains("chronic lymphocytic leukemia");
            }                                   

            this.m_FCCClients = new List<int>();
            this.m_FCCClients.Add(67);

            this.m_TCIClients = new List<int>();
            this.m_TCIClients.Add(658);
            this.m_TCIClients.Add(879);
            this.m_TCIClients.Add(936);
            this.m_TCIClients.Add(1123);
            this.m_TCIClients.Add(1132);
            this.m_TCIClients.Add(1201);
            this.m_TCIClients.Add(1311);
            this.m_TCIClients.Add(1316);
            this.m_TCIClients.Add(1457);
            this.m_TCIClients.Add(1478);
            this.m_TCIClients.Add(1552);
            this.m_TCIClients.Add(1558);
            this.m_TCIClients.Add(1615);

            this.m_NMHClients = new List<int>();
            this.m_NMHClients.Add(587);
            this.m_NMHClients.Add(588);
            this.m_NMHClients.Add(1607);
            this.m_NMHClients.Add(1604);
            this.m_NMHClients.Add(1608);
            this.m_NMHClients.Add(1699);
        }

        public override void Run()
        {
            this.HasIndication();
            this.CaseForClient();
            this.HasOrder();
            this.CreateResult();
        }

        private void HasIndication()
        {
            this.m_HasIndication = false;
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.Impression) == false)
            {
                if (this.m_PanelSetOrder.Impression.Contains("chronic lymphocytic leukemia") == true ||
                this.m_PanelSetOrder.InterpretiveComment.Contains("chronic lymphocytic leukemia") == true)
                {
                    this.m_HasIndication = true;
                }
            }            
        }

        private void CaseForClient()
        {
            if (this.m_HasIndication == true)
            {
                if (this.m_FCCClients.IndexOf(this.m_AccessionOrder.ClientId) > -1) this.m_UseFCCRule = true;
                else if (this.m_TCIClients.IndexOf(this.m_AccessionOrder.ClientId) > -1) this.m_UseTCIRule = true;
                else if (this.m_NMHClients.IndexOf(this.m_AccessionOrder.ClientId) > -1) this.m_UseNMHRule = true;
            }
        }

        private void HasOrder()
        {
            if (this.m_HasIndication == true)
            {
                Test.CLLByFish.CLLByFishTest cllByFishTest = new Test.CLLByFish.CLLByFishTest();
                PanelSet.Model.PanelSetIGVH panelSetIGVH = new PanelSet.Model.PanelSetIGVH();
                foreach (Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
                {
                    if (panelSetOrder.PanelSetId == cllByFishTest.PanelSetId) this.m_HasCLLByFish = true;
                    if (panelSetOrder.PanelSetId == panelSetIGVH.PanelSetId) this.m_HasIGVH = true;
                }
            }
        }

        private void CreateResult()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if (this.m_UseFCCRule == true && this.m_HasIndication == true) CreateFCCResult();
            else if (this.m_UseTCIRule == true && this.m_HasIndication == true) CreateTetonCancerResult();
            else if (this.m_UseNMHRule == true&& this.m_HasIndication == true) CreateNMHResult();
        }

        private void CreateFCCResult()
        {
            this.IsNewDiagnosis();
            if (this.m_IsNewDiagnosis == true)
            {
                if (this.m_HasCLLByFish == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The client has requested that all new diagnoses of CLL have a CLL Panel by FISH reflexively ordered.");
                    this.m_Message.AppendLine();
                    this.m_Message.AppendLine("Please order CLL Panel by FISH, and document in the interpretive comment in the flow report.");
                }
            }
        }

        private void CreateTetonCancerResult()
        {
            this.IsNewDiagnosis();
            if (this.m_IsNewDiagnosis == true)
            {
                if(this.m_HasCLLByFish == false && this.m_HasIGVH == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The client has requested that all new diagnoses of CLL have a CLL Panel by FISH and IgVH mutation analysis reflexively ordered.");
                    this.m_Message.AppendLine();
                    this.Message.AppendLine("Please order CLL Panel by FISH, IgVH mutation analysis, and document in the interpretive comment in the flow report.");
                }
                else if(this.m_HasCLLByFish == false && this.m_HasIGVH == true)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The client has requested that all new diagnoses of CLL have a CLL Panel by FISH and IgVH mutation analysis reflexively ordered.");
                    this.m_Message.AppendLine();
                    this.Message.AppendLine("Please order CLL Panel by FISH as there is an IgVH mutation analysis, and document in the interpretive comment in the flow report.");
                }
                else if (this.m_HasCLLByFish == true && this.m_HasIGVH == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The client has requested that all new diagnoses of CLL have a CLL Panel by FISH and IgVH mutation analysis reflexively ordered.");
                    this.m_Message.AppendLine();
                    this.Message.AppendLine("Please order IgVH mutation analysis as here is a CLL Panel by FISH, and document in the interpretive comment in the flow report.");
                }
            }
        }

        private void CreateNMHResult()
        {                           
            this.m_Status = AuditStatusEnum.Failure;
            this.m_Message.AppendLine("The client has requested that all new diagnoses of CLL have a Neotype CLL Prognostic Panel reflexively ordered.");
            this.m_Message.AppendLine();
            this.m_Message.AppendLine("Please order Neotype CLL Prognostic Panel, and document in the interpretive comment in the flow report.");                
        }

        private void IsNewDiagnosis()
        {
            string patientId = this.m_AccessionOrder.PatientId;
            int previousCases = Gateway.FlowGateway.PreviousFlowCasesAbnormalCLL(this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.PatientId);
            if (previousCases == 0) this.m_IsNewDiagnosis = true;
        }
    }
}
