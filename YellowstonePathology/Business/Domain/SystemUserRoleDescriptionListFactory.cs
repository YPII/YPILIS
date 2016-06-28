using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class SystemUserRoleDescriptionListFactory
	{
		public static YellowstonePathology.Business.User.SystemUserRoleDescriptionList GetFinalPanelSetRoleList(YellowstonePathology.Business.PanelSet.Model.PanelSetEnum panelSet)
		{
            YellowstonePathology.Business.User.SystemUserRoleDescriptionList roleList = new YellowstonePathology.Business.User.SystemUserRoleDescriptionList();
			switch (panelSet)
			{
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.CysticFibrosis:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.JAK2:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.KRAS:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.SurgicalPathology:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.Flow:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.FluorescentInSituHybridization:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.HER2AmplificationByFISH:
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist);
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Administrator);
					break;
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.HighRiskHPV:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.HPV:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.NGCT:
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.Extraction:
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist);
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Administrator);
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);
					break;
				case YellowstonePathology.Business.PanelSet.Model.PanelSetEnum.TechnicalOnly:
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.HistologyLog);
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist);
					roleList.Add(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Administrator);
					break;
			}
			return roleList;
		}
	}
}
