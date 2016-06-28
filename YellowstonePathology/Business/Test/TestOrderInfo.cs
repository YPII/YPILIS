using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class TestOrderInfo
	{
		private YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        private YellowstonePathology.Business.Interface.IOrderTarget m_OrderTarget;        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private bool m_Distribute;
        private bool m_OrderTargetIsKnown;        

		public TestOrderInfo()
		{
            
		}

        public TestOrderInfo(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, YellowstonePathology.Business.Interface.IOrderTarget orderTarget, bool distribute)
		{
            this.m_PanelSet = panelSet;
			this.m_OrderTarget = orderTarget;
            this.m_Distribute = distribute;
            this.m_OrderTargetIsKnown = true;
		}

		public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
		{
			get { return this.m_PanelSet; }
			set { this.m_PanelSet = value; }
		}

		public YellowstonePathology.Business.Interface.IOrderTarget OrderTarget
		{
			get { return this.m_OrderTarget; }
			set { this.m_OrderTarget = value; }
		}        

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
			set { this.m_PanelSetOrder = value; }
		}

		public bool Distribute
		{
			get { return this.m_Distribute; }
			set { this.m_Distribute = value; }
		}

        public bool OrderTargetIsKnown
        {
            get { return this.m_OrderTargetIsKnown; }
            set { this.m_OrderTargetIsKnown = value; }
        }

        public void AttemptSetOrderTarget(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection)
        {
            if (this.m_PanelSet.AttemptOrderTargetLookup == true)
            {
                if (specimenOrderCollection.Exists(this.m_PanelSet.OrderTargetTypeCollectionRestrictions) == true)
                {
                    this.m_OrderTarget = specimenOrderCollection.GetOrderTarget(this.m_PanelSet.OrderTargetTypeCollectionRestrictions);
                    this.m_OrderTargetIsKnown = true;
                }
            }
        }
	}
}
