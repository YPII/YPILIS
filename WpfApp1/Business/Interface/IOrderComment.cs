using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IOrderComment
	{
		int OrderCommentId { get; set; }
        string Category { get; set; }
        string Action { get; set; }
        string Description { get; set; }
		bool RequiresNotification { get; set; }
		bool RequiresResponse { get; set; }
		string NotificationAddress { get; set; }
	}
}
