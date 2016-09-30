using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderDNA : PanelSetOrder
	{
		
	}
}
