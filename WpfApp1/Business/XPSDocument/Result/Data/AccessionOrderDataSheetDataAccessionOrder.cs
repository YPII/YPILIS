using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class AccessionOrderDataSheetDataAccessionOrder
    {
        private string m_PatientDisplayName;
        private string m_SSN;
        private string m_ClientName;
        private string m_PhysicianName;
        private string m_MasterAccessionNo;
        private string m_AccessionTime;
        private string m_SvhMedicalRecord;
        private string m_SvhAccount;
        private string m_ClinicalHistory;
        private string m_PreOpDiagnosis;
        private string m_SpecialInstructions;

        public AccessionOrderDataSheetDataAccessionOrder(Test.AccessionOrder accessionOrder, ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            this.m_PatientDisplayName = string.Empty;
            this.m_SSN = string.Empty;
            this.m_ClientName = string.Empty;
            this.m_PhysicianName = string.Empty;
            this.m_MasterAccessionNo = string.Empty;
            this.m_AccessionTime = string.Empty;
            this.m_SvhMedicalRecord = string.Empty;
            this.m_SvhAccount = string.Empty;
            this.m_ClinicalHistory = string.Empty;
            this.m_PreOpDiagnosis = string.Empty;
            this.m_SpecialInstructions = string.Empty;

            this.SetPatientDisplayName(accessionOrder);
            this.SetAccessionData(accessionOrder);
            this.SetClientOrderData(clientOrderCollection);
        }

        public string PatientDisplayName
        {
            get { return this.m_PatientDisplayName; }
        }

        public string SSN
        {
            get { return this.m_SSN; }
        }

        public string ClientName
        {
            get { return this.m_ClientName; }
        }

        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
        }

        public string AccessionTime
        {
            get { return this.m_AccessionTime; }
        }

        public string SvhMedicalRecord
        {
            get { return this.m_SvhMedicalRecord; }
        }

        public string SvhAccount
        {
            get { return this.m_SvhAccount; }
        }

        public string ClinicalHistory
        {
            get { return this.m_ClinicalHistory; }
        }

        public string PreOpDiagnosis
        {
            get { return this.m_PreOpDiagnosis; }
        }

        public string SpecialInstructions
        {
            get { return this.m_SpecialInstructions; }
        }


        private void SetPatientDisplayName(Test.AccessionOrder accessionOrder)
        {
            this.m_PatientDisplayName = accessionOrder.PatientDisplayName;
            string pAge = accessionOrder.PatientAccessionAge;
            if (string.IsNullOrEmpty(pAge) == false)
            {
                this.m_PatientDisplayName += "(" + accessionOrder.PBirthdate.Value.ToShortDateString() + ", " + pAge + ", " + accessionOrder.PSex + ")";
            }
        }

        private void SetAccessionData(Test.AccessionOrder accessionOrder)
        {
            if (string.IsNullOrEmpty(accessionOrder.PSSN) == false) this.m_SSN = accessionOrder.PSSN;
            if (string.IsNullOrEmpty(accessionOrder.ClientName) == false) this.m_ClientName = accessionOrder.ClientName;
            if (string.IsNullOrEmpty(accessionOrder.PhysicianName) == false) this.m_PhysicianName = accessionOrder.PhysicianName;
            if (string.IsNullOrEmpty(accessionOrder.MasterAccessionNo) == false) this.m_MasterAccessionNo = accessionOrder.MasterAccessionNo;
            if (accessionOrder.AccessionTime.HasValue) this.m_AccessionTime = accessionOrder.AccessionTime.Value.ToString("MM/dd/yyyy HH:mm");
            if (string.IsNullOrEmpty(accessionOrder.SvhMedicalRecord) == false) this.m_SvhMedicalRecord = accessionOrder.SvhMedicalRecord;
            if (string.IsNullOrEmpty(accessionOrder.SvhAccount) == false) this.m_SvhAccount = accessionOrder.SvhAccount;
        }

        private void SetClientOrderData(ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            string clinicalHistory = string.Empty;
            string preOpDiagnosis = string.Empty;
            string coSpecialInstructions = string.Empty;

            foreach (Business.ClientOrder.Model.ClientOrder clientOrder in clientOrderCollection)
            {
                string clientClinicalHistory = clientOrder.ClinicalHistory;

                string clientPreOpDiagnosis = string.Empty;
                if (clientOrder.PanelSetId == 13)
                {
                    ClientOrder.Model.SurgicalClientOrder surgicalClientOrder = (ClientOrder.Model.SurgicalClientOrder)clientOrder;
                    if (string.IsNullOrEmpty(surgicalClientOrder.PreOpDiagnosis) == false) clientPreOpDiagnosis = surgicalClientOrder.PreOpDiagnosis;
                }

                if (string.IsNullOrEmpty(clinicalHistory) == true)
                {
                    if (string.IsNullOrEmpty(clientClinicalHistory) == false)
                    {
                        clinicalHistory = clientClinicalHistory;
                    }
                }

                if (string.IsNullOrEmpty(preOpDiagnosis) == true)
                {
                    if (string.IsNullOrEmpty(clientPreOpDiagnosis) == false)
                    {
                        preOpDiagnosis = clientPreOpDiagnosis;
                    }
                }

                if (string.IsNullOrEmpty(clientOrder.SpecialInstructions) == false) coSpecialInstructions += clientOrder.SpecialInstructions;
            }

            this.m_ClinicalHistory = clinicalHistory;
            this.m_PreOpDiagnosis = preOpDiagnosis;
            this.m_SpecialInstructions = coSpecialInstructions;
        }
    }
}
