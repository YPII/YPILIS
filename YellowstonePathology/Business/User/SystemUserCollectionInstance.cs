using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.User
{
	public class SystemUserCollectionInstance
	{
		private static SystemUserCollectionInstance instance;
		private SystemUserCollection m_SystemUserCollection;

		private SystemUserCollectionInstance()
		{            
			m_SystemUserCollection = YellowstonePathology.Business.User.SystemUserGateway.GetSystemUserCollection(); 
		}

		public static SystemUserCollectionInstance Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new SystemUserCollectionInstance();
				}
				return instance;
			}
		}

		public SystemUserCollection SystemUserCollection
		{
			get { return m_SystemUserCollection; }
		}
	}
}
