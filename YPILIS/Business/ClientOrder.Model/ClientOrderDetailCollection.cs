using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{        
    public partial class ClientOrderDetailCollection : ObservableCollection<YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail>
    {        
        public ClientOrderDetailCollection()
        {

        }

        public bool HasItemsWithNoContainerId()
        {
            bool result = false;
            foreach (ClientOrderDetail clientOrderDetail in this)
            {
                if (string.IsNullOrEmpty(clientOrderDetail.ContainerId) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void AddReceivedItems(ClientOrderDetailCollection clientOrderDetailCollection)
        {
            foreach (ClientOrderDetail clientOrderDetail in clientOrderDetailCollection)
            {
                if (clientOrderDetail.Received == true)
                {
                    this.Add(clientOrderDetail);
                }
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail GetByContainerId(string containerId)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail result = null;
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this)
            {
                if (clientOrderDetail.ContainerId.ToUpper() == containerId.ToUpper())
                {
                    result = clientOrderDetail;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail GetByClientOrderDetailId(string clientOrderDetailId)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail result = null;
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this)
            {
                if (clientOrderDetail.ClientOrderDetailId == clientOrderDetailId)
                {
                    result = clientOrderDetail;
                    break;
                }
            }
            return result;
        }

        public void MarkAsSubmitted()
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this)
            {
                if (clientOrderDetail.Submitted == false)
                {
                    clientOrderDetail.Submitted = true;
                }
            }
        }

        public void SetClientOrderIds(string clientOrderId)
        {
            foreach(ClientOrderDetail cod in this)
            {
                if (string.IsNullOrEmpty(cod.ClientOrderId) == true)
                {
                    cod.ClientOrderId = clientOrderId;
                }
                else
                {
                    if (clientOrderId != cod.ClientOrderId)
                    {
                        throw new Exception("The client order ID should match.");
                    }
                }
            }
        }

        public int GetNextSpecimenNumber()
        {
            return this.Count + 1;
        }

        public void RenumberSpecimens()
        {
            int specimenNumber = 1;
            foreach (ClientOrderDetail clientOrderDetail in this)
            {
                clientOrderDetail.SpecimenNumber = specimenNumber;
                specimenNumber += 1;
            }
        }

        public void ClientRequestDeleteSpecimen(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail,
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, 
            YellowstonePathology.Business.Rules.ExecutionMessage executionMessage)
        {
			if (clientOrderDetail.Received == true)
			{
				executionMessage.Halted = true;
				executionMessage.Message = "This specimen cannot be deleted because it has been recieved at YPI";
			}
			else
			{				
				clientOrder.ClientOrderDetailCollection.Remove(clientOrderDetail);
				clientOrder.ClientOrderDetailCollection.RenumberSpecimens();
				
				if(clientOrder.ClientOrderDetailCollection.Count > 0)
				{
			        executionMessage.Message = "The selected specimen was successfully deleted.  The remaining specimen have been renumbered.";
				}
			}
		}		

		public void SetAccessioned()
		{
			foreach (ClientOrderDetail clientOrderDetail in this)
			{
				if (clientOrderDetail.Received)
				{
					clientOrderDetail.Accessioned = true;
				}
			}
		}

		public bool ExistsByContainerId(string ContainerId)
		{
			bool result = false;
			foreach (ClientOrderDetail clientOrderDetail in this)
			{
                if (string.IsNullOrEmpty(clientOrderDetail.ContainerId) == false)
                {
                    if (clientOrderDetail.ContainerId.ToUpper() == ContainerId.ToUpper())
                    {
                        result = true;
                        break;
                    }
                }
			}
			return result;
		}

        public bool ExistsByClientOrderDetailId(string clientOrderDetailId)
        {
            bool result = false;

            foreach (ClientOrderDetail clientOrderDetail in this)
            {
                if (clientOrderDetail.ClientOrderDetailId == clientOrderDetailId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }		

		public void LoadMedia(YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaCollection clientOrderMediaCollection)
		{
			foreach (ClientOrderDetail clientOrderDetail in this)
			{				
                ClientOrderMedia clientOrderMedia = new ClientOrderMedia(clientOrderDetail.ContainerId);
                clientOrderMedia.ClientOrderDetailId = clientOrderDetail.ClientOrderDetailId;
				clientOrderMedia.CollectionDate = clientOrderDetail.CollectionDate;
				clientOrderMedia.ClientFixation = clientOrderDetail.ClientFixation;
				clientOrderMedia.Description = clientOrderDetail.Description;
				clientOrderMedia.DescriptionToAccession = clientOrderDetail.DescriptionToAccession;
				clientOrderMedia.SpecimenNumber = clientOrderDetail.SpecimenNumber.ToString();
				clientOrderMedia.LabFixation = clientOrderDetail.LabFixation;				
                clientOrderMedia.Received = clientOrderDetail.Received;
				clientOrderMediaCollection.Add(clientOrderMedia);				
			}
		}

        public void AddIfNotExist(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
        {
            if (this.ExistsByContainerId(clientOrderDetail.ContainerId) == false)
            {
                this.Add(clientOrderDetail);
            }
        }

        public bool DoesClientOrderIdExist(string clientOrderId)
        {
            bool result = false;
            foreach (ClientOrderDetail clientOrderDetail in this)
            {
                if (clientOrderDetail.ClientOrderId == clientOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public string GetContainerIdString()
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			foreach (ClientOrderDetail clientOrderDetail in this)
			{
				result.Append(clientOrderDetail.ContainerId + ",");
			}
			if (result.Length > 1)
			{
				result.Remove(result.Length - 1, 1);
			}
			return result.ToString();
		}		

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail GetNextItem(string containerId, string clientOrderId, string orderTypeCode, 
            string systemInitiatingOrder, string orderDescription, string accessionDescription, string specimenTrackingInitiated, string orderedBy, Nullable<DateTime> collectionDate)
		{
			YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail result = ClientOrderDetailFactory.GetClientOrderDetail(orderTypeCode, YellowstonePathology.Business.Persistence.PersistenceModeEnum.AddNewObject);
			result.ClientOrderDetailId = Guid.NewGuid().ToString();
			result.ObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			result.OrderTypeCode = orderTypeCode;
			result.ClientOrderId = clientOrderId;
			result.ContainerId = containerId;
			result.OrderedBy = orderedBy;
			result.OrderDate = DateTime.Today;
			result.OrderTime = DateTime.Now;
			result.CollectionDate = collectionDate;            
			result.SystemInitiatingOrder = systemInitiatingOrder;
			result.Description = orderDescription;
            result.DescriptionToAccession = accessionDescription;
			result.SpecimenTrackingInitiated = specimenTrackingInitiated;
            result.SpecimenNumber = this.GetNextSpecimenNumber();
            result.OrderedBy = orderedBy;
            return result;
		}        

		public void Join(string clientOrderId)
		{
			foreach (ClientOrderDetail clientOrderDetail in this)
			{
				clientOrderDetail.ClientOrderId = clientOrderId;
			}
		}
	}
}