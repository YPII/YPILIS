using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public partial class AccessionOrder
    {
		public YellowstonePathology.Business.Validation.ValidationResult IsMedicalRecordNoValid()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;

            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection westParkHospitalGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(36);
            if (westParkHospitalGroup.ClientIdExists(this.m_ClientId) == true)
            {
                if (string.IsNullOrEmpty(this.m_SvhMedicalRecord) == false)
                {
                    if (this.SvhMedicalRecord.Length != 8)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital medical record numbers must be 8 characters long.";
                    }
                    else if (this.SvhMedicalRecord.StartsWith("W") == false)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital medical record numbers must start with the letter W.";
                    }
                }
                else
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "West Park Hostpital medical record number must not be blank.";
                }
            }
            return validationResult;
        }

		public YellowstonePathology.Business.Validation.ValidationResult IsAccountNoValid()
        {
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
            validationResult.IsValid = true;

            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection westParkHospitalGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(36);
            if (westParkHospitalGroup.ClientIdExists(this.m_ClientId) == true)
            {
                if (string.IsNullOrEmpty(this.m_SvhAccount) == false)
                {
                    if (this.SvhAccount.Length != 9)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital account numbers must be 9 characters long.";
                    }
                    else if (this.SvhAccount.StartsWith("A") == false && this.SvhAccount.StartsWith("CC") == false)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital account numbers must start with the letter A or CC.";
                    }
                }
                else
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "The West Park Hostpital account number must not be blank.";
                }
            }
            return validationResult;
        }
    }
}
