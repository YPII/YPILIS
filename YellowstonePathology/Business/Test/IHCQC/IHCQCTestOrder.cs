using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.IHCQC
{
	[PersistentClass("tblIHCQCTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class IHCQCTestOrder : PanelSetOrder
	{
		private bool m_ControlsReactedAppropriately;		
		private string m_Comment;
		
		public IHCQCTestOrder()
        {

        }

        public IHCQCTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
        public bool ControlsReactedAppropriately
		{
            get { return this.m_ControlsReactedAppropriately; }
			set
			{
                if (this.m_ControlsReactedAppropriately != value)
				{
                    this.m_ControlsReactedAppropriately = value;
                    this.NotifyPropertyChanged("ControlsReactedAppropriately");
				}
			}
		}

		[PersistentProperty()]
		public string Comment
		{
            get { return this.m_Comment; }
			set
			{
                if (this.m_Comment != value)
				{
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
				}
			}
		}		

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

            result.AppendLine("Controls Reacted Appropriately: " + this.m_ControlsReactedAppropriately);
			result.AppendLine();

			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();			

			return result.ToString();
		}
	}
}
