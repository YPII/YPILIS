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
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private OrderTypeEnum m_ExpectedOrderType;		
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_CurrentClientOrderDetail;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private object m_Writer;

		public ClientOrderReceivingHandler(object writer)
        {
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            this.m_AClientHasBeenFound = false;
            this.m_AClientOrderHasBeenAcquired = false;
            this.m_AClientOrderHasBeenConfirmed = false;
			this.m_AnAccessionOrderHasBeenAquired = false;
            this.m_Writer = writer;
        }      		

		public YellowstonePathology.Business.Client.Model.Client Client
		{
			get { return this.m_Client; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrder; }
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
            this.m_ClientOrder = new Business.ClientOrder.Model.SurgicalClientOrder(objectId);
			this.m_ClientOrder.SetDefaults(this.m_Client.ClientId, this.m_Client.ClientName);
			this.m_ClientOrder.ClientLocation = this.m_Client.ClientLocationCollection.CurrentLocation;
            this.m_ClientOrder.OrderedById = this.m_SystemIdentity.User.UserId.ToString();
            this.m_ClientOrder.OrderedBy = this.m_SystemIdentity.User.UserName;
            this.m_ClientOrder.CollectionDate = DateTime.Today;

            YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI universalServiceYPI = new Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI();
            this.m_ClientOrder.UniversalServiceId = universalServiceYPI.UniversalServiceId;

            switch (this.m_ExpectedOrderType)
            {                
                default:
                    this.m_ClientOrder.PanelSetId = 13;
                    break;
            }
			
			this.m_ClientOrder.OrderType = "Routine Surgical Pathology";
            this.m_AClientOrderHasBeenAcquired = true;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_ClientOrder, this.m_Writer);			
        }

        public void IFoundAClient(YellowstonePathology.Business.Client.Model.Client client)
        {
            this.m_Client = client;
            this.m_AClientHasBeenFound = true;
            this.m_ExpectedOrderType = (OrderTypeEnum)Enum.Parse(typeof(OrderTypeEnum), client.ClientLocationCollection.CurrentLocation.OrderType, true);            
		}

        public void IFoundAClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {            
            if (this.m_AClientOrderHasBeenAcquired == true)
            {
                if (this.m_ClientOrder.ClientOrderId != clientOrder.ClientOrderId)
                {                    
                    this.m_ClientOrder = clientOrder;                    
                }
            }
            else
            {                
                this.m_ClientOrder = clientOrder;                
            }

            this.m_AClientOrderHasBeenAcquired = true;
        }

        public void ResetClientOrder()
        {
            this.m_ClientOrder = null;
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
				if (this.m_ClientOrder.ClientOrderId != clientOrderDetail.ClientOrderId)
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
				clientOrderDetail.ClientOrderId = this.m_ClientOrder.ClientOrderId;
				result.OkToReceive = true;
			}
			return result;
		}

        public void IFoundAClientOrderDetail(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
        {
            this.m_ClientOrder.ClientOrderDetailCollection.Add(clientOrderDetail);            
        }

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail FindOrCreateNewClientOrderDetail(string containerId)
		{
			YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail result = null;
            if (this.m_ClientOrder.ClientOrderDetailCollection.ExistsByContainerId(containerId) == false)
			{
				YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderDetailByContainerId(containerId);
				if (clientOrderDetail == null)
				{
                    result = this.m_ClientOrder.ClientOrderDetailCollection.GetNextItem(containerId, this.m_ClientOrder.ClientOrderId, "SRGCL", "YPIILAB", "None.", 
                        null, "Ypii Lab", this.m_SystemIdentity.User.DisplayName, this.GetCollectionDateForNewClientOrderDetail());
                    this.m_ClientOrder.ClientOrderDetailCollection.Add(result);
				}
				else
				{
					result = clientOrderDetail;					
				}
			}
			else
			{
                result = this.m_ClientOrder.ClientOrderDetailCollection.GetByContainerId(containerId);
			}

			return result;
		}

        public DateTime GetCollectionDateForNewClientOrderDetail()
        {
            DateTime collectionDate = DateTime.Now;
            if (this.m_ClientOrder.ClientOrderDetailCollection.Count == 0)
            {
                if (this.m_ClientOrder.CollectionDate.HasValue == true)
                {
                    collectionDate = this.m_ClientOrder.CollectionDate.Value;
                }
            }
            else
            {
                collectionDate = this.m_ClientOrder.ClientOrderDetailCollection[this.m_ClientOrder.ClientOrderDetailCollection.Count - 1].CollectionDate.Value;
            }
            return collectionDate;
        }

		private void ReceiveClientOrderDetail(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{			
			this.m_CurrentClientOrderDetail = clientOrderDetail;
			this.m_ClientOrder.Receive();
			this.m_CurrentClientOrderDetail.Receive();            
        }		

		public void CreateNewAccessionOrder(YellowstonePathology.Business.Test.AccessionTypeEnum accessionType)
		{
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNextMasterAccessionNo();            
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
			this.m_AccessionOrder.FromClientOrder(this.m_ClientOrder, this.m_SystemIdentity.User.UserId);                                   			

			this.m_AnAccessionOrderHasBeenAquired = true;
		}

		public void AccessionClientOrder()
		{
			this.m_ClientOrder.Accession(this.m_AccessionOrder.MasterAccessionNo);
			this.SendStatusMessage();            
			this.m_AccessionOrder.AccessionSpecimen(this.m_ClientOrder.ClientOrderDetailCollection);

            YellowstonePathology.Business.ClientOrder.Model.EPICClinicalHistoryExtractor epicClinicalHistoryConverter = new Business.ClientOrder.Model.EPICClinicalHistoryExtractor();
            string clinicalhistory = epicClinicalHistoryConverter.ExctractClinicalHistory(this.m_AccessionOrder.SpecialInstructions);
            if (string.IsNullOrEmpty(clinicalhistory) == false)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == true)
                {
                    this.m_AccessionOrder.ClinicalHistory = clinicalhistory;
                }
                else
                {
                    this.m_AccessionOrder.ClinicalHistory = this.m_AccessionOrder.ClinicalHistory + " " + clinicalhistory;
                }                
            }

            this.m_AccessionOrder.PanelSetOrderCollection.FromClientOrder(this.m_ClientOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_AccessionOrder.PanelSetOrderCollection.HandleReflexTestingFromClientOrder(this.m_ClientOrder, this.m_AccessionOrder, this.m_SystemIdentity);            
        }

		private void SendStatusMessage()
		{
			if (this.m_ClientOrder.SystemInitiatingOrder == "EPIC")
			{
				if (this.m_ClientOrder.Acknowledged == false)
				{
                    YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
                    YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(this.m_ClientOrder.UniversalServiceId);

					YellowstonePathology.Business.HL7View.EPIC.EPICStatusMessage statusMessage = new Business.HL7View.EPIC.EPICStatusMessage(this.m_ClientOrder, YellowstonePathology.Business.HL7View.OrderStatusEnum.InProcess, universalService);
					YellowstonePathology.Business.Rules.MethodResult result = statusMessage.Send();

					if (result.Success == false)
					{
                        YellowstonePathology.Business.Logging.EmailExceptionHandler.HandleException(result.Message);
					}
					else
					{
						this.m_ClientOrder.Acknowledged = true;
						this.m_ClientOrder.AcknowledgedById = this.m_SystemIdentity.User.UserId;
						this.m_ClientOrder.AcknowledgedDate = DateTime.Now;
					}
				}
			}
		}

		public void UseThisMasterAccessionNoToGetTheAccessionOrder(string masterAccessionNo)
		{
			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);			
			this.m_AnAccessionOrderHasBeenAquired = true;
		}

		public void SetPatientAsVerified()
		{
			this.m_ClientOrder.Validated = true;
		}

		public bool IsTheProviderNPIValid()
		{
			bool result = true;

			if (this.m_ClientOrder.SystemInitiatingOrder == "EPIC")
			{
				YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(this.m_ClientOrder.ProviderId);
				if (physician == null)
				{
					result = false;
				}
			}

			return result;
		}

		public void Save(bool releaseLock)
		{
			
        }

		public void ResetTheSelectedClientOrderDetailToThisOne(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			this.m_CurrentClientOrderDetail = clientOrderDetail;
		}
	}
}
