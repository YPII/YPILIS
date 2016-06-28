using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
    public class InvasiveBreastPanelResult
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		private YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel m_InvasiveBreastPanel;
		private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_PanelSetOrderHer2ByIsh;
        private YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder m_PanelSetOrderErPrSemiQuantitative;
		//private YellowstonePathology.Business.Test.PanelSetOrderHer2AmplificationByFishRetired3 m_PanelSetOrderHer2AmplificationByFishRetired3;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;

        private bool m_HasSurgical;
        private YellowstonePathology.Business.SpecialStain.StainResultItem m_StainResultER;
        private YellowstonePathology.Business.SpecialStain.StainResultItem m_StainResultPR;

        private string m_ERResultString;
        private string m_PRResultString;
        private string m_HER2ResultString;
		private string m_HER2ByFISHResultString;
		private bool m_IsHER2ByFISHRequired;
		private bool m_HER2ByFISHHasBeenOrdered;

        public InvasiveBreastPanelResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

			YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest panelSetInvasiveBreastPanel = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest();
			this.m_InvasiveBreastPanel = (YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetInvasiveBreastPanel.PanelSetId);
            this.m_InvasiveBreastPanel.SetStatus(this.m_AccessionOrder.PanelSetOrderCollection);

            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_InvasiveBreastPanel.OrderedOnId);

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(46) == true)
            {
                this.m_PanelSetOrderHer2ByIsh = (YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(46);
                this.m_HER2ResultString = this.m_PanelSetOrderHer2ByIsh.Result;
            }

			YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest erPrSemiQuantitativeTest = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest();            
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true && this.m_AccessionOrder.PanelSetOrderCollection.Exists(erPrSemiQuantitativeTest.PanelSetId) == false)
            {
                this.m_HasSurgical = true;
				YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetSurgical.PanelSetId);
				this.m_SurgicalSpecimen = panelSetOrderSurgical.SurgicalSpecimenCollection.GetBySpecimenOrderId(this.m_SpecimenOrder.SpecimenOrderId);

                YellowstonePathology.Business.Test.Model.TestOrder testOrderER = this.m_AccessionOrder.PanelSetOrderCollection.GetTestOrderByTestId(99);
				this.m_StainResultER = panelSetOrderSurgical.GetStainResult(testOrderER.TestOrderId);
                this.m_ERResultString = this.m_StainResultER.Result;

                YellowstonePathology.Business.Test.Model.TestOrder testOrderPR = this.m_AccessionOrder.PanelSetOrderCollection.GetTestOrderByTestId(145);
				this.m_StainResultPR = panelSetOrderSurgical.GetStainResult(testOrderPR.TestOrderId);
                this.m_PRResultString = this.m_StainResultPR.Result;

            }
			else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(erPrSemiQuantitativeTest.PanelSetId) == true)
            {
				this.m_PanelSetOrderErPrSemiQuantitative = (YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(erPrSemiQuantitativeTest.PanelSetId);
                this.m_ERResultString = this.m_PanelSetOrderErPrSemiQuantitative.ErResult;
                this.m_PRResultString = this.m_PanelSetOrderErPrSemiQuantitative.PrResult;
            }

			if (this.m_HER2ResultString == YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder.EquivocalResult) this.m_IsHER2ByFISHRequired = true;

			YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired3 panelSetHer2AmplificationByFishRetired3 = new YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired3();
			YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest panelSetHer2AmplificationByFish = new YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHer2AmplificationByFishRetired3.PanelSetId) == true)
			{
				this.m_HER2ByFISHHasBeenOrdered = true;
				YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHer2AmplificationByFishRetired3.PanelSetId);
				this.m_HER2ByFISHResultString = "Result in Report " + panelSetOrder.ReportNo;
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHer2AmplificationByFish.PanelSetId) == true)
			{
				this.m_HER2ByFISHHasBeenOrdered = true;
				YellowstonePathology.Business.Test.Her2AmplificationByFish.PanelSetOrderHer2AmplificationByFish panelSetOrderHer2AmplificationByFish = (YellowstonePathology.Business.Test.Her2AmplificationByFish.PanelSetOrderHer2AmplificationByFish)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHer2AmplificationByFish.PanelSetId);
				this.m_HER2ByFISHResultString = panelSetOrderHer2AmplificationByFish.Result;
			}
			else this.m_HER2ByFISHResultString = "Not Required.";
        }

        public bool HasSurgical
        {
            get { return this.m_HasSurgical; }
        }

        public string ERResultString
        {
            get { return this.m_ERResultString; }
        }

        public string PRResultString
        {
            get { return this.m_PRResultString; }
        }

        public string HER2ResultString
        {
            get { return this.m_HER2ResultString; }
        }

		public string HER2ByFISHResultString
		{
			get { return this.m_HER2ByFISHResultString; }
		}

		public bool IsHER2ByFISHRequired
		{
			get { return this.m_IsHER2ByFISHRequired; }
		}

		public bool HER2ByFISHHasBeenOrdered
		{
			get { return this.m_HER2ByFISHHasBeenOrdered; }
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

		public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
		{
			get { return this.m_SurgicalSpecimen; }
		}
	}
}
