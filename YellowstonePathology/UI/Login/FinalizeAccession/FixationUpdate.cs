using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class FixationUpdate
    {
        private bool m_ClientAccessioned;
        private Nullable<DateTime> m_TimeSpecimenCollected;
        private Nullable<DateTime> m_DateSpecimenCollected;
        private Nullable<DateTime> m_TimeSpecimenReceived;
        private string m_ReceivedIn;
        private string m_TimeToFixation;        
        private Nullable<int> m_FixationDuration;
        private bool m_SpecimenAccessioningAssumption;
        private YellowstonePathology.Business.Surgical.ProcessorRun m_ProcessorRun;
        
        public FixationUpdate(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Surgical.ProcessorRunCollection processorRunCollection)
        {
            this.m_ClientAccessioned = specimenOrder.ClientAccessioned;
            this.m_TimeSpecimenCollected = specimenOrder.CollectionTime;
            this.m_DateSpecimenCollected = specimenOrder.CollectionDate;
            this.m_TimeSpecimenReceived = specimenOrder.DateReceived;
            this.m_ReceivedIn = specimenOrder.ClientFixation;
            //this.m_TimeToFixation = specimenOrder.TimeToFixation;
            //this.m_FixationDuration = specimenOrder.FixationDuration;
            this.m_SpecimenAccessioningAssumption = true;

            if (string.IsNullOrEmpty(specimenOrder.ProcessorRunId) == false)
            {                
                this.m_ProcessorRun = processorRunCollection.Get(specimenOrder.ProcessorRunId);
            }
        }

        public void Update(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            throw new Exception("This is not valid anymore.");

            /*
            if (specimenOrder.ClientAccessioned == false)
            {
                specimenOrder.CollectionTime = this.m_TimeSpecimenCollected;
                specimenOrder.CollectionDate = this.m_DateSpecimenCollected;
                specimenOrder.ClientFixation = this.m_ReceivedIn;
                specimenOrder.TimeToFixation = this.m_TimeToFixation;            
                //this.m_ProcessorRun.Update(specimenOrder, this.m_SpecimenAccessioningAssumption);
            }                        
            this.SetFixationComment(specimenOrder);
            */
        }

        
        public void SetFixationComment(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
            if (specimenOrder.FixationDuration == 0)
			{
                specimenOrder.FixationComment = "Fixation time for this specimen was not provided or is unknown.  Probe signal strength was satisfactory for evaluation, and results are valid.";
			}
			else
			{
                if (specimenOrder.FixationDuration < 6)
				{
                    specimenOrder.FixationComment = "Fixation time provided for this specimen is less than the optimal range (6-72 hours), which may affect results.  " +
						"However, for this specimen probe signal strength was satisfactory for evaluation, and results are valid.";
				}
                else if (specimenOrder.FixationDuration >= 6 && specimenOrder.FixationDuration <= 72)
				{
                    specimenOrder.FixationComment = string.Empty;
				}
                else if (specimenOrder.FixationDuration > 72)
				{
                    specimenOrder.FixationComment = "Fixation time provided for this specimen is more than the optimal range (6-72 hours), which may affect results.  " +
						"However, for this specimen probe signal strength was satisfactory for evaluation, and results are valid.";
				}
			}
		}     

        public bool ClientAccessioned
        {
            get { return this.m_ClientAccessioned; }
            set { this.m_ClientAccessioned = value; }
        }

        public Nullable<DateTime> TimeSpecimenCollected
        {
            get { return this.m_TimeSpecimenCollected; }
            set { this.m_TimeSpecimenCollected = value; }
        }

        public Nullable<DateTime> DateSpecimenCollected
        {
            get { return this.m_DateSpecimenCollected; }
            set { this.m_DateSpecimenCollected = value; }
        }

        public Nullable<DateTime> TimeSpecimenReceived
        {
            get { return this.m_TimeSpecimenReceived; }
            set { this.m_TimeSpecimenReceived = value; }
        }

        public string ReceivedIn
        {
            get { return this.m_ReceivedIn; }
            set { this.m_ReceivedIn = value; }
        }

        public string TimeToFixation
        {
            get { return this.m_TimeToFixation; }
            set { this.m_TimeToFixation = value; }
        }        

        public Nullable<int> FixationDuration
        {
            get { return this.m_FixationDuration; }
            set { this.m_FixationDuration = value; }
        }

        public YellowstonePathology.Business.Surgical.ProcessorRun ProcessorRun
        {
            get { return this.m_ProcessorRun; }
            set { this.m_ProcessorRun = value; }
        }

        public bool SpecimenAccessioningAssumption
        {
            get { return this.m_SpecimenAccessioningAssumption; }
            set { this.m_SpecimenAccessioningAssumption = value; }
        }
    }    

}
