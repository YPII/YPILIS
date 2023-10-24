using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class CytologyClientOrder
	{
		#region ValidateReflexHPV
		public static YellowstonePathology.Business.Validation.ValidationResult IsReflexHPVDataTypeValid(bool orderReflexHPV)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsReflexHPVDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateRoutineHPVTesting
		public static YellowstonePathology.Business.Validation.ValidationResult IsRoutineHPVTestingDataTypeValid(bool orderRoutineHPVTesting)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsRoutineHPVTestingDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateNGCTTesting
		public static YellowstonePathology.Business.Validation.ValidationResult IsNGCTTestingDataTypeValid(bool orderNGCTTesting)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsNGCTTestingDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateScreeningType
		public static YellowstonePathology.Business.Validation.ValidationResult IsScreeningTypeDataTypeValid(string screeningType)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsScreeningTypeDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (string.IsNullOrEmpty(this.ScreeningType) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "You must select a screening type before you can continue.";
			}
			return validationResult;
		}
		#endregion

		#region ValidateLMP
		public static YellowstonePathology.Business.Validation.ValidationResult IsLMPBindingDataTypeValid(string lMP)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsLMPDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateHysterectomy
		public static YellowstonePathology.Business.Validation.ValidationResult IsHysterectomyDataTypeValid(bool hysterectomy)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsHysterectomyDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateCervixPresent
		public static YellowstonePathology.Business.Validation.ValidationResult IsCervixPresentDataTypeValid(bool cervixPresent)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsCervixPresentDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateAbnormalBleeding
		public static YellowstonePathology.Business.Validation.ValidationResult IsAbnormalBleedingDataTypeValid(bool abnormalBleeding)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsAbnormalBleedingDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateBirthControl
		public static YellowstonePathology.Business.Validation.ValidationResult IsBirthControlDataTypeValid(bool birthControl)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsBirthControlDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateHormoneTherapy
		public static YellowstonePathology.Business.Validation.ValidationResult IsHormoneTherapyDataTypeValid(bool hormoneTherapy)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsHormoneTherapyDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousNormalPap
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreviousNormalPapDataTypeValid(bool previousNormalPap)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreviousNormalPapDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousNormalPapDate
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreviousNormalPapDateDataTypeValid(string previousNormalPapDate)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreviousNormalPapDateDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousAbnormalPap
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreviousAbnormalPapDataTypeValid(bool previousAbnormalPap)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreviousAbnormalPapDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousAbnormalPapDate
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreviousAbnormalPapDateDataTypeValid(string previousAbnormalPapDate)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreviousAbnormalPapDateDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousBiopsy
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreviousBiopsyDataTypeValid(bool previousBiopsy)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreviousBiopsyDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousBiopsyDate
		public static YellowstonePathology.Business.Validation.ValidationResult IsPreviousBiopsyDateDataTypeValid(string previousBiopsyDate)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPreviousBiopsyDateDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePrenatal
		public static YellowstonePathology.Business.Validation.ValidationResult IsPrenatalDataTypeValid(bool prenatal)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPrenatalDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePostpartum
		public static YellowstonePathology.Business.Validation.ValidationResult IsPostpartumDataTypeValid(bool postpartum)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPostpartumDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePostmenopausal
		public static YellowstonePathology.Business.Validation.ValidationResult IsPostmenopausalDataTypeValid(bool postmenopausal)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsPostmenopausalDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateIcd9Code
		public static YellowstonePathology.Business.Validation.ValidationResult IsIcd9CodeDataTypeValid(string icd9Code)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsIcd9CodeDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			if (string.IsNullOrEmpty(this.m_Icd9Code) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "You must enter an ICD9 code before you can continue.";
			}
			return validationResult;
		}
		#endregion

		#region ValidateTrichomonasVaginalis
		public static YellowstonePathology.Business.Validation.ValidationResult IsTrichomonasVaginalisDataTypeValid(bool trichomonasVaginalis)
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Business.Validation.ValidationResult IsTrichomonasVaginalisDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion
	}
}
