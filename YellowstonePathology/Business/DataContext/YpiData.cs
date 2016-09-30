using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Reflection;
using System.Xml.Linq;

namespace YellowstonePathology.Business.DataContext
{
	public class YpiData : YpiDataBase
    {
		public YpiData()
			: base(@"Data Source=TestSql;Initial Catalog=YPIData;Integrated Security=True")
		{            
		
		}		
	}
}
