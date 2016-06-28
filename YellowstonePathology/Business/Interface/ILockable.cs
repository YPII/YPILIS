using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface ILockable
	{
		void GetKeyLock(YellowstonePathology.Business.Domain.KeyLock keyLock);		
	}
}
