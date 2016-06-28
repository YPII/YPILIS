using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
    public class MPNExtendedReflexResult
    {
        public static string PendingResult = "Pending";
        public static string NotPerformedResult = "Not Performed";
        public static string NotClinicallyIndicated = "Not Clinically Indicated";
        public static string PleaseOrder = "Please Order";
        public static string UnknownState = "Unknown State";
        public static string NotOrdered = "Not Ordered";

        private static string JAK2DetectedInterpretation = "JAK2 V617F mutation activates a tyrosine kinase that leads to clonal proliferation.  " +
            "V617F is present in greater than 40% of essential thrombocythemia (ET) and primary myelofibrosis (MF) cases. MPL mutations are detected in up " +
            "to 5% of patients with JAK2-negative myeloproliferative neoplasms (MPN). CALR mutations are mutually exclusive with JAK2 and MPL mutations, and are " +
            "detected in 70-85% of ET and MF cases that are JAK2 and MPL negative.  These mutations have not been detected in normal patients.  Thus, the " +
            "detection of the JAK2 V617F, MPL or CALR mutations provides confirmation for the diagnosis of a MPN.   The molecular analysis detected the V617F mutation. " +
            "This result supports the diagnosis of an MPN.";

        private static string CALRDetectedInterpretation = "JAK2 V617F mutation activates a tyrosine kinase that leads to clonal proliferation.  V617F is present in " +
            "greater than 40% of essential thrombocythemia (ET) and primary myelofibrosis (MF) cases. MPL mutations are detected in up to 5% of patients with JAK2-negative " +
            "myeloproliferative neoplasms (MPN). CALR mutations are mutually exclusive with JAK2 and MPL mutations, and are detected in 70-85% of ET and MF cases that are " +
            "JAK2 and MPL negative.  These mutations have not been detected in normal patients.  Thus, the detection of the JAK2 V617F, MPL or CALR mutations provides " +
            "confirmation for the diagnosis of a MPN.   The molecular analysis detected a CALR mutation. V617F was negative. This result supports the diagnosis of an MPN.  " +
            "The presence of the CALR mutation indicates a better prognosis compared to the JAK2 mutation.";

        private static string MPLDetectedInterpretation = "JAK2 V617F mutation activates a tyrosine kinase that leads to clonal proliferation.  V617F is present in greater than 40% of " +
            "essential thrombocythemia (ET) and primary myelofibrosis (MF) cases. MPL mutations are detected in up to 5% of patients with JAK2-negative myeloproliferative " +
            "neoplasms (MPN). CALR mutations are mutually exclusive with JAK2 and MPL mutations, and are detected in 70-85% of ET and MF cases that are JAK2 and MPL negative.  " +
            "These mutations have not been detected in normal patients.  Thus, the detection of the JAK2 V617F, MPL or CALR mutations provides confirmation for the " +
            "diagnosis of a MPN.   The molecular analysis detected a MPL mutation.  JAK2 V617F and CALR were normal.  This result supports the diagnosis of an MPN.  ";

        private static string NotDetectedInterpretation = "JAK2 V617F mutation activates a tyrosine kinase that leads to clonal proliferation.  V617F is present in greater " +
            "than 40% of essential thrombocythemia (ET) and primary myelofibrosis (MF) cases. MPL mutations are detected in up to 5% of patients with JAK2-negative " +
            "myeloproliferative neoplasms (MPN). CALR mutations are mutually exclusive with JAK2 and MPL mutations, and are detected in 70-85% of ET and MF cases that are " +
            "JAK2 and MPL negative.  These mutations have not been detected in normal patients.  Thus, the detection of the JAK2 V617F, MPL or CALR mutations provides " +
            "confirmation for the diagnosis of a MPN.   The molecular analysis did not detect mutations in JAK2 V617F, CALR or MPL.  The results do not support the diagnosis " +
            "of a MPN, but do not rule it out. Recommend clinical and pathologic correlation.";

        private static string References = "1.  Tefferi A, Vardiman JW.  Classification and diagnosis of myeloproliferative neoplasms: The 2008 World Health Organization criteria and point-of-care diagnostic algorithms.  Leukemia (2008) 22, 14–22 " + Environment.NewLine +
            "2.  Levine RL, Gilliland DG.  Myeloproliferative disorders.  Blood. 2008 112: 2190-2198 " + Environment.NewLine +
            "3. Nangalia J, et al. Somatic CALR mutations in myeloproliferative neoplasms with nonmutated JAK2, N Engl J Med. 2013 Dec 19;369(25):2391-405.";

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        private YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest m_PanelSetJAK2V617F;
        private YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest m_PanelSetCalreticulinMutationAnalysis;
        private YellowstonePathology.Business.Test.MPL.MPLTest m_PanelSetMPL;

        private YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex m_PanelSetOrderMPNExtendedReflex;
        private YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder m_PanelSetOrderJAK2V617F;
        private YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder m_PanelSetOrderCalreticulinMutationAnalysis;
        private YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL m_PanelSetOrderMPL;

        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

        private bool m_HasCALR;
        private bool m_HasMPL;

        private MPNExtendedReflexJAK2Result m_JAK2V617FResult;
        private MPNExtendedReflexCALRResult m_CALRResult;
        private MPNExtendedReflexMPLResult m_MPLResult;

        private YellowstonePathology.Business.Audit.Model.AuditCollection m_AuditCollection;

        private string m_Comment;
        private string m_Interpretation;
        private string m_Method;
        private string m_References;

        public MPNExtendedReflexResult(AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

            YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest panelSetMPNExtendedReflex = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest();
            this.m_PanelSetOrderMPNExtendedReflex = (YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNExtendedReflex.PanelSetId);

            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrderMPNExtendedReflex.OrderedOn, this.m_PanelSetOrderMPNExtendedReflex.OrderedOnId);

            this.m_PanelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
            this.m_PanelSetCalreticulinMutationAnalysis = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
            this.m_PanelSetMPL = new YellowstonePathology.Business.Test.MPL.MPLTest();

            this.m_PanelSetOrderJAK2V617F = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetJAK2V617F.PanelSetId);

            this.m_HasCALR = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetCalreticulinMutationAnalysis.PanelSetId);
            if (this.m_HasCALR == true)
            {
                this.m_PanelSetOrderCalreticulinMutationAnalysis = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetCalreticulinMutationAnalysis.PanelSetId);
            }

            this.m_HasMPL = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetMPL.PanelSetId);
            if (this.m_HasMPL == true)
            {
                this.m_PanelSetOrderMPL = (YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetMPL.PanelSetId);
            }

            this.m_AuditCollection = new Audit.Model.AuditCollection();
            this.m_JAK2V617FResult = new MPNExtendedReflexJAK2Result(this.m_PanelSetOrderJAK2V617F);
            this.m_AuditCollection.Add(this.m_JAK2V617FResult);

            this.m_CALRResult = new MPNExtendedReflexCALRResult(this.m_AccessionOrder);
            this.m_AuditCollection.Add(this.m_CALRResult);

            this.m_MPLResult = new MPNExtendedReflexMPLResult(this.m_AccessionOrder);
            this.m_AuditCollection.Add(this.m_MPLResult);

            this.SetInterpretation();
            this.SetMethod();
            this.SetComment();
            this.m_References = References;
        }

        public YellowstonePathology.Business.Audit.Model.AuditCollection AuditCollection
        {
            get { return this.m_AuditCollection; }
        }

        private void SetInterpretation()
        {
            if (this.IsJAK2Detected() == true) this.m_Interpretation = JAK2DetectedInterpretation;
            if (this.m_HasCALR == true)
            {
                if (this.IsCALRDetected() == true)
                {
                    this.m_Interpretation = CALRDetectedInterpretation;
                }
            }

            if (this.m_HasMPL == true)
            {
                if (this.IsMPLDetected() == true)
                {
                    this.m_Interpretation = MPLDetectedInterpretation;
                }
            }

            if (this.m_HasCALR == true && this.m_HasMPL == true)
            {
                if (this.AreAllNotDetected() == true)
                {
                    this.m_Interpretation = NotDetectedInterpretation;
                }
            }
        }

        private bool IsJAK2Detected()
        {
            bool result = false;
            YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult jak2V617FDetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult();
            if (this.m_PanelSetOrderJAK2V617F.ResultCode == jak2V617FDetectedResult.ResultCode)
            {
                result = true;
            }
            return result;
        }

        private bool IsCALRDetected()
        {
            bool result = false;
            YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultDetected calreticulinMutationAnalysisResultDetected = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultDetected();
            if (this.m_PanelSetOrderCalreticulinMutationAnalysis.ResultCode == calreticulinMutationAnalysisResultDetected.ResultCode)
            {
                result = true;
            }
            return result;
        }

        private bool IsMPLDetected()
        {
            bool result = false;
            YellowstonePathology.Business.Test.MPL.MPLResultDetected mplResultDetected = new MPL.MPLResultDetected();
            if (this.m_PanelSetOrderMPL.ResultCode == mplResultDetected.ResultCode)
            {
                result = true;
            }
            return result;
        }

        private bool AreAllNotDetected()
        {
            bool result = false;

            YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FNotDetectedResult jak2V617FNotDetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FNotDetectedResult();
            YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultNotDetected calreticulinMutationAnalysisResultNotDetected = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultNotDetected();
            YellowstonePathology.Business.Test.MPL.MPLResultNotDetected mplResultNotDetected = new MPL.MPLResultNotDetected();

            if (this.m_PanelSetOrderJAK2V617F.ResultCode == jak2V617FNotDetectedResult.ResultCode)
            {
                if (this.m_PanelSetOrderCalreticulinMutationAnalysis.ResultCode == calreticulinMutationAnalysisResultNotDetected.ResultCode)
                {
                    if (this.m_PanelSetOrderMPL.ResultCode == mplResultNotDetected.ResultCode)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        private void SetMethod()
        {
            StringBuilder method = new StringBuilder();
            method.Append(this.m_PanelSetOrderJAK2V617F.PanelSetName + ": " + this.m_PanelSetOrderJAK2V617F.Method + Environment.NewLine + Environment.NewLine);
            if (this.m_HasCALR == true)
            {
                method.Append(this.m_PanelSetOrderCalreticulinMutationAnalysis.PanelSetName + ": " + this.m_PanelSetOrderCalreticulinMutationAnalysis.Method + Environment.NewLine + Environment.NewLine);
                if (this.m_HasMPL == true)
                {
                    method.Append(this.m_PanelSetOrderMPL.PanelSetName + ": " + this.m_PanelSetOrderMPL.Method + Environment.NewLine + Environment.NewLine);
                }
            }
            this.m_Method = method.ToString().Trim();
        }

        private void SetComment()
        {
            this.m_Comment = this.m_PanelSetOrderJAK2V617F.Comment;
        }

        public void SetResults()
        {
            this.m_PanelSetOrderMPNExtendedReflex.Comment = this.m_Comment;
            this.m_PanelSetOrderMPNExtendedReflex.Interpretation = this.m_Interpretation;
            this.m_PanelSetOrderMPNExtendedReflex.Method = this.m_Method;
            this.m_PanelSetOrderMPNExtendedReflex.References = this.m_References;
        }

        public MPNExtendedReflexJAK2Result JAK2V617FResult
        {
            get { return this.m_JAK2V617FResult; }
        }

        public MPNExtendedReflexCALRResult CALRResult
        {
            get { return this.m_CALRResult; }
        }

        public MPNExtendedReflexMPLResult MPLResult
        {
            get { return this.m_MPLResult; }
        }

        public YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex PanelSetOrderMPNExtendedReflex
        {
            get { return this.m_PanelSetOrderMPNExtendedReflex; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize()
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrderMPNExtendedReflex.IsOkToFinalize(this.m_AccessionOrder);
            if(result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if(this.m_JAK2V617FResult.Result == MPNExtendedReflexResult.PendingResult ||
                    this.m_CALRResult.Result == MPNExtendedReflexResult.PendingResult ||
                    this.m_MPLResult.Result == MPNExtendedReflexResult.PendingResult)
                {
                    result.Message = "This case can not be finaled as one or more dependency tests are not finaled.";
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                }
                else if(this.m_CALRResult.Result == MPNExtendedReflexResult.PleaseOrder ||
                    this.m_MPLResult.Result == MPNExtendedReflexResult.PleaseOrder)
                {
                    result.Message = "This case can not be finaled as one or more dependency tests are not ordered.";
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                }
            }

            return result;
        }
    }
}
