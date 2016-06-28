using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class OrderCommentFactory
	{
		public static YellowstonePathology.Business.Interface.IOrderComment GetOrderComment(YellowstonePathology.Business.Domain.OrderCommentEnum orderCommentEnum)
        {
			YellowstonePathology.Business.Interface.IOrderComment result = null;
			switch (orderCommentEnum)
            {
				case OrderCommentEnum.CytologyLogFaxSentRequestingInformation:  //5000
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(5000);
					break;
				case OrderCommentEnum.CytologyLogFaxSentRequestingABN:          //5001
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(5001);
                    break;
				case OrderCommentEnum.CytologyLogClientCall:          //5002
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(5002);
                    break;
				case OrderCommentEnum.SpecimenReceivedFromHistology:          //5005
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(5005);
					break;
				case OrderCommentEnum.CancelOrder:   //7000
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(7000);
                    break;
				case OrderCommentEnum.ClientOrderAccessioned:   //1010
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(1010);
                    break;
				case OrderCommentEnum.ClientOrderSpecimenAccessioned:   //1011
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(1011);
                    break;
				case OrderCommentEnum.PatientVerified: //6010
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(6010);
					break;
				case OrderCommentEnum.PatientVerificationError: //6020
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(6020);
                    break;
				case OrderCommentEnum.SpecimenVerified: //6011
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(6011);
					break;
				case OrderCommentEnum.SpecimenVerificationError: //6021
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(6021);
					break;
				case OrderCommentEnum.OrderIron:  //6030
					result = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetLabEventByEventId(6030);
					break;
				default:
					break;
            }
            return result;
        }
	}
}
