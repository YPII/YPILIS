using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
	public class SystemIdentityReturnEventArgs
	{
		Business.User.SystemIdentity m_SystemIdentity;

		public SystemIdentityReturnEventArgs(Business.User.SystemIdentity systemIdentity)
        {
			this.m_SystemIdentity = systemIdentity;
        }

		public Business.User.SystemIdentity SystemIdentity
        {
			get { return this.m_SystemIdentity; }
        }
	}
}
