using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSMeasure
    {
        protected PQRIKeyWordCollection m_PQRIKeyWordCollection;
		protected string m_Header;
        protected YellowstonePathology.Business.Billing.Model.CptCodeCollection m_CptCodeCollection;
        protected YellowstonePathology.Business.Billing.Model.PQRSCodeCollection m_PQRSCodeCollection;
        protected PQRSAgeDefinitionEnum m_PQRSAgeDefinition;

        public PQRSMeasure()
        {
            this.m_PQRSAgeDefinition = PQRSAgeDefinitionEnum.AllPatients;
            this.m_PQRIKeyWordCollection = new PQRIKeyWordCollection();
            this.m_CptCodeCollection = new Business.Billing.Model.CptCodeCollection();
            this.m_PQRSCodeCollection = new Business.Billing.Model.PQRSCodeCollection();
        }

		public virtual bool DoesMeasureApply(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, int patientAge)
        {
            bool result = false;            
            if (this.m_PQRIKeyWordCollection.WordsExistIn(surgicalSpecimen.SpecimenOrder.Description) == true)				
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

		public PQRIKeyWordCollection PQRIKeyWordCollection
		{
            get { return this.m_PQRIKeyWordCollection; }
		}

		public string Header
		{
			get { return this.m_Header; }
		}

        public PQRSAgeDefinitionEnum PQRSAgeDefinition
        {
            get { return this.m_PQRSAgeDefinition; }
        }
    }
}
