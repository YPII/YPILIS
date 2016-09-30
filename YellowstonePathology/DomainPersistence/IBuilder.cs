using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence
{
	public interface IBuilder
	{
		void Build(XElement xElement);
	}
}
