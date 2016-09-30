using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public enum OrderCommentEnum
	{
        CytologyLogFaxSentRequestingInformation,
        CytologyLogFaxSentRequestingABN,
        CytologyLogClientCall,
		SpecimenReceivedFromHistology,
        CancelOrder,
		ClientOrderAccessioned,
		ClientOrderSpecimenAccessioned,
		PatientVerified,
        PatientVerificationError,
 		SpecimenVerified,
		SpecimenVerificationError,
		OrderIron,
        UserComment
	}
}
