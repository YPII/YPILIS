using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Rules.Amendment
{
	public class PendingStudies
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		bool m_IsSurgical;

		public PendingStudies()
		{
			m_IsSurgical = false;
			this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
			this.m_Rule.ActionList.Add(this.IsSurgical);
			this.m_Rule.ActionList.Add(this.HasPendingAncillaryStudies);
		}

		public void IsSurgical()
		{
			if (m_PanelSetOrderItem.PanelSetId == 13)
			{
				m_IsSurgical = true;
			}
		}

		private void HasPendingAncillaryStudies()
		{
			if (m_IsSurgical)
			{
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_PanelSetOrderItem;
				if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
				{
					string message = string.Empty;
					foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in panelSetOrderSurgical.TypingStainCollection)
					{

						if (string.IsNullOrEmpty(stainResultItem.Result) == true)
						{
							message = "There are pending ancillary studies.";
							break;
						}
						if (string.IsNullOrEmpty(stainResultItem.ProcedureComment) == true && (string.IsNullOrEmpty(stainResultItem.Result) || stainResultItem.Result != "Pending"))
						{
							message = "There are pending ancillary studies.";
							break;
						}

						if (string.IsNullOrEmpty(message) == false)
						{
							System.Windows.MessageBox.Show(message, "Pending Ancillary Studies", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
						}
					}
				}
			}
		}

		public void Execute(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			this.m_PanelSetOrderItem = panelSetOrder;
			this.m_ExecutionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			this.m_Rule.Execute(this.m_ExecutionStatus);
		}
	}
}
