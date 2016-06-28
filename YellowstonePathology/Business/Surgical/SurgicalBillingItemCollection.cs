using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Surgical
{
	public class SurgicalBillingItemCollection : ObservableCollection<SurgicalBillingItem>
	{
		public SurgicalBillingItemCollection()
		{
		}
	}
}
