using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure
    {
        protected List<string> m_DiagnosisWordCollection;
        protected List<string> m_SpecimenWordCollection;

        protected string m_Header;
        protected YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;
        protected YellowstonePathology.Business.Billing.Model.PQRSCodeCollection m_PQRSCodeCollection;
        protected PQRSAgeDefinitionEnum m_PQRSAgeDefinition;

        public PQRSMeasure()
        {
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.AllPatients;
            this.m_DiagnosisWordCollection = new List<string>();
            this.m_SpecimenWordCollection = new List<string>();
            this.m_CptCodeCollection = new Business.Billing.Model.CptCodeCollection();
            this.m_PQRSCodeCollection = new Business.Billing.Model.PQRSCodeCollection();
        }

		public virtual bool DoesMeasureApply(Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;            
            if (WordsExistIn(surgicalSpecimen.SpecimenOrder.Description, this.m_DiagnosisWordCollection) == true)				
			{
                YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollectionForThisSpecimen = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrder.SpecimenOrderId);
                if (panelSetOrderCPTCodeCollectionForThisSpecimen.DoesCollectionHaveCodes(this.m_CptCodeCollection) == true)
                {
                    result = true;                    
                }
            }            
            return result;
        }        

		public YellowstonePathology.Business.Billing.Model.CptCodeCollection CptCodeCollection
		{
			get { return this.m_CptCodeCollection; }		
		}

		public YellowstonePathology.Business.Billing.Model.PQRSCodeCollection PQRSCodeCollection
		{
			get { return this.m_PQRSCodeCollection; }
		}

		public List<string> DiagnosisWordCollection
		{
            get { return this.m_DiagnosisWordCollection; }
		}

        public List<string> SpecimenWordCollection
        {
            get { return this.m_SpecimenWordCollection; }
        }

        public string Header
		{
			get { return this.m_Header; }
		}

        public PQRSAgeDefinitionEnum PQRSAgeDefinition
        {
            get { return this.m_PQRSAgeDefinition; }
        }

        public static bool WordsExistIn(string text, List<string> stringList)
        {
            bool result = false;
            foreach (string keyWord in stringList)
            {
                if (string.IsNullOrEmpty(text) == false)
                {
                    if (text.ToUpper().Contains(keyWord.ToUpper()) == true)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
