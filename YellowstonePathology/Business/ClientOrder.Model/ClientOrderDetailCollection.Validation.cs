using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrderDetailCollection
	{
		public YellowstonePathology.Business.Validation.ValidationResult IsContainerIdNotADuplicate(string clientOrderDetailId, string containerId)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (ExistsByContainerId(containerId) == true)
			{
				ClientOrderDetail matchClientOrderDetail = GetByContainerId(containerId);
				if (matchClientOrderDetail.ClientOrderDetailId != clientOrderDetailId)
				{
					validationResult.IsValid = false;
				}
			}

			if(validationResult.IsValid == false)
			{
				validationResult.Message = "The container scanned is part of an existing order and can't be used again. Are you sure you have scanned the correct container?";
			}
			return validationResult;
		}

		public static YellowstonePathology.Business.Validation.ValidationResult IsDataTypeValid(ClientOrderDetail clientOrderDetail, ClientOrderDetailCollection clientOrderDetailCollection)
		{
			Business.Validation.ValidationResult result = new Business.Validation.ValidationResult();
			result.IsValid = true;
			List<YellowstonePathology.Business.Validation.ValidationResult> validationResults = new List<Business.Validation.ValidationResult>();
			validationResults.Add(clientOrderDetailCollection.IsContainerIdNotADuplicate(clientOrderDetail.ClientOrderDetailId, clientOrderDetail.ContainerId));

			foreach (Business.Validation.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = validationResult;
					break;
				}
			}
			return result;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsDomainValid()
		{
			Business.Validation.ValidationResult result = new Business.Validation.ValidationResult();
			result.IsValid = true;

            /*
			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			foreach (ClientOrderDetail clientOrderDetail in this)
			{
				validationResults.Add(clientOrderDetail.IsDomainValid());
				validationResults.Add(this.IsContainerIdNotADuplicate(clientOrderDetail.ClientOrderDetailId, clientOrderDetail.ContainerId));
			}

			foreach (Shared.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = validationResult;
					break;
				}
			}
            */
			return result;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsDomainValid(ClientOrderDetail clientOrderDetail)
		{
			Business.Validation.ValidationResult result = new Business.Validation.ValidationResult();
			result.IsValid = true;
			List<YellowstonePathology.Business.Validation.ValidationResult> validationResults = new List<Business.Validation.ValidationResult>();
			validationResults.Add(this.IsContainerIdNotADuplicate(clientOrderDetail.ClientOrderDetailId, clientOrderDetail.ContainerId));

			foreach (Business.Validation.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = validationResult;
					break;
				}
			}
			return result;
		}
	}
}
