using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Autopsy
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class AutopsyTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		public AutopsyTestOrder()
		{

		}

		public AutopsyTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}
	}
}
