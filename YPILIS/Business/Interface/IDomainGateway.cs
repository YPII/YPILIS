using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IDomainGateway
	{
        void Insert(object o);
        void Update(object o);
        void Delete(object o);        
	}
}
