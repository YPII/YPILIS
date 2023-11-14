using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class ClientOrderReceivingHandler
    {
        private bool m_AClientHasBeenFound;
        private bool m_AClientOrderHasBeenAcquired;
        private bool m_AClientOrderHasBeenConfirmed;
		private bool m_AnAccessionOrderHasBeenAquired;
		
		private YellowstonePathology.Business.Client.Model.Client m_Client;        
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
        private string m_ClientOrderId;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private OrderTypeEnum m_ExpectedOrderType;		
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_CurrentClientOrderDetail;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private object m_Writer;

		public ClientOrderReceivingHandler(object writer)
        {
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
            this.m_AClientHasBeenFound = false;
            this.m_AClientOrderHasBeenAcquired = false;
            this.m_AClientOrderHasBeenConfirmed = false;
			this.m_AnAccessionOrderHasBeenAquired = false;
            this.m_Writer = writer;
            this.m_ClientOrderCollection = new Business.ClientOrder.Model.ClientOrderCollection();
        }      		

		public YellowstonePathology.Business.Client.Model.Client Client
		{
			get { return this.m_Client; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrderCollection.GetClientOrder(this.m_ClientOrderId); }
		}

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail CurrentClientOrderDetail
		{
			get { return this.m_CurrentClientOrderDetail; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}		

		public OrderTypeEnum ExpectedOrderType
		{
			get { return this.m_ExpectedOrderType; }
		}

		public bool AClientHasBeenFound
		{
			get { return this.m_AClientHasBeenFound; }
		}

		public bool AClientOrderHasBeenAcquired
		{
			get { return this.m_AClientOrderHasBeenAcquired; }
		}

		public bool AClientOrderHasBeenConfirmed
		{
			get { return this.m_AClientOrderHasBeenConfirmed; }
		}

		public bool AnAccessionOrderHasBeenAquired
		{
			get { return this.m_AnAccessionOrderHasBeenAquired; }
		}

        public void LetsUseANewClientOrder()
        {
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = new Business.ClientOrder.Model.SurgicalClientOrder(objectId);
            clientOrder.SetDefaults(this.m_Client.ClientId, this.m_Client.ClientName);            
            clientOrder.OrderedById = this.m_SystemIdentity.User.UserId.ToString();
            clientOrder.OrderedBy = this.m_SystemIdentity.User.UserName;
            clientOrder.CollectionDate = DateTime.Today;

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI universalServiceYPI = new Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI();
            clientOrder.UniversalServiceId = universalServiceYPI.UniversalServiceId;

            this.m_ClientOrderCollection.Remove(this.ClientOrder);
            this.m_ClientOrderId = clientOrder.ClientOrderId;

            switch (this.m_ExpectedOrderType)
            {                
                default:
                    clientOrder.PanelSetId = 13;
                    break;
            }

            clientOrder.OrderType = "Routine Surgical Pathology";
            this.m_AClientOrderHasBeenAcquired = true;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(clientOrder, this.m_Writer);
            this.m_ClientOrderCollection.Add(clientOrder);
        }

        public void IFoundAClient(YellowstonePathology.Business.Client.Model.Client client, bool useRequsition)
        {
            this.m_Client = client;
            this.m_AClientHasBeenFound = true;
            if (useRequsition == false)
            {
                this.m_ExpectedOrderType = (OrderTypeEnum)Enum.Parse(typeof(OrderTypeEnum), client.OrderType, true);
            }
            else
            {
                this.m_ExpectedOrderType = OrderTypeEnum.REQUISITION;
            }
		}

        public void IFoundAClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {            
            if (this.m_AClientOrderHasBeenAcquired == true)
            {
                if (this.ClientOrder.ClientOrderId != clientOrder.ClientOrderId)
                {
                    this.m_ClientOrderCollection.Remove(this.ClientOrder);
                    this.m_ClientOrderCollection.Add(clientOrder);
                }
            }
            else
            {
                this.m_ClientOrderCollection.Remove(this.ClientOrder);
                this.m_ClientOrderCollection.Add(clientOrder);
            }

            this.m_ClientOrderId = clientOrder.ClientOrderId;
            this.m_AClientOrderHasBeenAcquired = true;
        }

        public void ResetClientOrder()
        {
            if(string.IsNullOrEmpty(this.m_ClientOrderId) == false && this.m_ClientOrderCollection.Exists(this.m_ClientOrderId) == true)
            {
                this.m_ClientOrderCollection.Remove(ClientOrder);
            }
            this.m_ClientOrderId = null;
            this.m_AClientOrderHasBeenAcquired = false;
            this.m_AClientOrderHasBeenConfirmed = false;
        }

		public void ConfirmTheClientOrder()
		{
			if (this.m_AClientOrderHasBeenAcquired == true)
			{
				this.m_AClientOrderHasBeenConfirmed = true;
			}
		}

		public IFoundAContainerResult IFoundAContainer(string containerId)
		{			
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = this.FindOrCreateNewClientOrderDetail(containerId);
            IFoundAContainerResult result = new IFoundAContainerResult(clientOrderDetail);

			if (string.IsNullOrEmpty(clientOrderDetail.ClientOrderId) == false)
			{
				if (this.m_ClientOrderId != clientOrderDetail.ClientOrderId)
				{
					result.OkToReceive = false;
					result.Message = "This container does not appear to belong to the current order.";
				}
				else
				{
					this.ReceiveClientOrderDetail(clientOrderDetail);
					result.OkToReceive = true;
				}
			}
			else
			{
				this.ReceiveClientOrderDetail(clientOrderDetail);
				clientOrderDetail.ClientOrderId = this.m_ClientOrderId;
				result.OkToReceive = true;
			}
			return result;
		}

        public void IFoundAClientOrderDetail(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
        {
            this.ClientOrder.ClientOrderDetailCollection.Add(clientOrderDetail);            
        }

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail FindOrCreateNewClientOrderDetail(string containerId)
		{
			YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail result = null;
            if (this.ClientOrder.ClientOrderDetailCollection.ExistsByContainerId(containerId) == false)
			{
				YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = Business.Gateway.ClientOrderGateway.GetClientOrderDetailByContainerId(containerId);
				if (clientOrderDetail == null)
				{
                    result = this.ClientOrder.ClientOrderDetailCollection.GetNextItem(containerId, this.ClientOrder.ClientOrderId, "SRGCL", "YPIILAB", "None.", 
                        null, "Ypii Lab", this.m_SystemIdentity.User.DisplayName, this.GetCollectionDateForNewClientOrderDetail());
                    this.ClientOrder.ClientOrderDetailCollection.Add(result);
				}
				else
				{
					result = clientOrderDetail;					
				}
			}
			else
			{
                result = this.ClientOrder.ClientOrderDetailCollection.GetByContainerId(containerId);
			}

			return result;
		}

        public DateTime GetCollectionDateForNewClientOrderDetail()
        {
            DateTime collectionDate = DateTime.Now;
            if (this.ClientOrder.ClientOrderDetailCollection.Count == 0)
            {
                if (this.ClientOrder.CollectionDate.HasValue == true)
                {
                    collectionDate = this.ClientOrder.CollectionDate.Value;
                }
            }
            else
            {
                collectionDate = this.ClientOrder.ClientOrderDetailCollection[this.ClientOrder.ClientOrderDetailCollection.Count - 1].CollectionDate.Value;
            }
            return collectionDate;
        }

		private void ReceiveClientOrderDetail(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{			
			this.m_CurrentClientOrderDetail = clientOrderDetail;
			this.ClientOrder.Receive();
			this.m_CurrentClientOrderDetail.Receive();            
        }		

		public void CreateNewAccessionOrder(YellowstonePathology.Business.Test.AccessionTypeEnum accessionType)
		{
            string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetNextMasterAccessionNo();            
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			switch (accessionType)
			{				
				case Business.Test.AccessionTypeEnum.ThinPrepPap:
					this.m_AccessionOrder = new YellowstonePathology.Business.Test.AccessionOrderThinPrepPap(masterAccessionNo, objectId);
					break;
				case Business.Test.AccessionTypeEnum.Surgical:
					this.m_AccessionOrder = new Business.Test.AccessionOrder(masterAccessionNo, objectId);
					break;
			}

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_AccessionOrder, this.m_Writer);
            string externalOrderId = this.GetExternalOrderIds();
			this.m_AccessionOrder.FromClientOrder(this.ClientOrder, this.m_SystemIdentity.User.UserId, externalOrderId);                                   			

			this.m_AnAccessionOrderHasBeenAquired = true;
		}

        public void AccessionClientOrders()
        {
            for(int idx = 0; idx < this.m_ClientOrderCollection.Count; idx++)
            {
                this.m_ClientOrderCollection[idx] = Business.Persistence.DocumentGateway.Instance.PullClientOrder(this.m_ClientOrderCollection[idx].ClientOrderId, this.m_Writer);
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = this.m_ClientOrderCollection[idx];
                clientOrder.Accession(this.m_AccessionOrder.MasterAccessionNo);

                if (this.m_ClientOrderId == clientOrder.ClientOrderId)
                {
                    this.m_AccessionOrder.AccessionSpecimen(clientOrder.ClientOrderDetailCollection);
                }

                this.SendStatusMessage(clientOrder);

                if (string.IsNullOrEmpty(this.m_AccessionOrder.SpecialInstructions) == true)
                {
                    this.m_AccessionOrder.SpecialInstructions = clientOrder.SpecialInstructions;
                }
                else if (this.m_AccessionOrder.SpecialInstructions.Contains(clientOrder.SpecialInstructions) == false)
                {
                    this.m_AccessionOrder.SpecialInstructions += Environment.NewLine + clientOrder.SpecialInstructions;
                }

                YellowstonePathology.Business.ClientOrder.Model.EPICClinicalHistoryExtractor epicClinicalHistoryConverter = new Business.ClientOrder.Model.EPICClinicalHistoryExtractor();
                string clinicalhistory = epicClinicalHistoryConverter.ExctractClinicalHistory(this.m_AccessionOrder.SpecialInstructions);
                if (string.IsNullOrEmpty(clinicalhistory) == false)
                {
                    if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == true)
                    {
                        this.m_AccessionOrder.ClinicalHistory = clinicalhistory;
                    }
                    else if (this.m_AccessionOrder.ClinicalHistory.Contains(clinicalhistory) == false)
                    {
                        this.m_AccessionOrder.ClinicalHistory = this.m_AccessionOrder.ClinicalHistory + " " + clinicalhistory;
                    }
                }

                this.m_AccessionOrder.PanelSetOrderCollection.FromClientOrder(clientOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            }

            if (this.m_ClientOrderCollection.PanelSetIdExists(15) == true)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder thinPrepClientOrder = this.m_ClientOrderCollection.GetClientOrderByPanelSetId(15);
                this.m_AccessionOrder.PanelSetOrderCollection.HandleReflexTestingFromClientOrder(thinPrepClientOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            }
        }

        private void SendStatusMessage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			if (clientOrder.SystemInitiatingOrder == "EPIC")
			{
				if (clientOrder.Acknowledged == false)
				{
                    YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
                    YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(clientOrder.UniversalServiceId);

					YellowstonePathology.Business.HL7View.EPIC.EPICStatusMessage statusMessage = new Business.HL7View.EPIC.EPICStatusMessage(clientOrder, YellowstonePathology.Business.HL7View.OrderStatusEnum.InProcess, universalService, "Yellowstone Pathology Institute: Order Is In Process.", "I", clientOrder.OrderDate.Value);
					statusMessage.Publish();
					
					clientOrder.Acknowledged = true;
					clientOrder.AcknowledgedById = this.m_SystemIdentity.User.UserId;
					clientOrder.AcknowledgedDate = DateTime.Now;					
				}
			}
		}

		public void UseThisMasterAccessionNoToGetTheAccessionOrder(string masterAccessionNo)
		{
			this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
            this.SetExternalOrderIds();
			this.m_AnAccessionOrderHasBeenAquired = true;
		}

		public void SetPatientAsVerified()
		{
			this.ClientOrder.Validated = true;
		}

		public bool IsTheProviderNPIValid()
		{
			bool result = true;

			if (this.ClientOrder.SystemInitiatingOrder == "EPIC")
			{
				YellowstonePathology.Business.Domain.Physician physician = Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.ClientOrder.ProviderId);
				if (physician == null)
				{
					result = false;
				}
			}

			return result;
		}		

		public void ResetTheSelectedClientOrderDetailToThisOne(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			this.m_CurrentClientOrderDetail = clientOrderDetail;
		}

        private void SetExternalOrderIds()
        {
            YellowstonePathology.Business.ClientOrder.Model.ExternalOrderIdsCollection externalOrderIdsCollection = Business.ClientOrder.Model.ExternalOrderIdsCollection.FromFormattedValue(this.m_AccessionOrder.ExternalOrderId);
            if (string.IsNullOrEmpty(this.ClientOrder.ExternalOrderId) == false && this.ClientOrder.PanelSetId.HasValue)
            {
                YellowstonePathology.Business.ClientOrder.Model.ExternalOrderIds externalOrderIds = new Business.ClientOrder.Model.ExternalOrderIds(this.ClientOrder);
                if (externalOrderIdsCollection.Exists(externalOrderIds.PanelSetId) == false)
                {
                    externalOrderIdsCollection.Add(externalOrderIds);
                }
            }

            if(externalOrderIdsCollection.Count > 0)
            {
                if (this.ClientOrder.SystemInitiatingOrder == "EPIC")
                {
                    this.m_AccessionOrder.ExternalOrderId = externalOrderIdsCollection.ToFormattedValue();
                }
            }
        }

        private string GetExternalOrderIds()
        {
            string result = null;
            YellowstonePathology.Business.ClientOrder.Model.ExternalOrderIdsCollection externalOrderIdsCollection = new Business.ClientOrder.Model.ExternalOrderIdsCollection(this.m_ClientOrderCollection);
            if (externalOrderIdsCollection.Count > 0)
            {
                result = externalOrderIdsCollection.ToFormattedValue();
            }
            return result;
        }

        public void AddClientOrders(YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders)
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in clientOrders)
            {
                if (this.m_ClientOrderCollection.Exists(clientOrder.ClientOrderId) == false)
                {
                    this.m_ClientOrderCollection.Add(clientOrder);
                }
            }
        }
    }
}
