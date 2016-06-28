using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrderCollection
	{
		public YellowstonePathology.Business.Validation.ValidationResult IsDomainValid()
		{
			YellowstonePathology.Business.Validation.ValidationResult validationResult = new Business.Validation.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
	}
}
