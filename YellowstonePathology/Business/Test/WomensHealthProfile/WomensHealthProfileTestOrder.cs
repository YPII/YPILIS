using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
	[PersistentClass("tblWomensHealthProfileTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class WomensHealthProfileTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
        private bool m_OrderHPV;        
        private bool m_OrderNGCT;
        private bool m_OrderTrichomonas;
        private bool m_OrderHPV1618;                                       
        private string m_HPVStandingOrderCode;
        private string m_HPVReflexOrderCode;
        private string m_HPV1618StandingOrderCode;
        private string m_HPV1618ReflexOrderCode;
        private string m_Comment;

		public WomensHealthProfileTestOrder() 
        {

        }

		public WomensHealthProfileTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_AssignedToId = 5051;

            YellowstonePathology.Business.Client.Model.ReflexOrderNone reflexOrderNone = new YellowstonePathology.Business.Client.Model.ReflexOrderNone();
            this.m_HPVReflexOrderCode = reflexOrderNone.ReflexOrderCode;
            this.m_HPV1618ReflexOrderCode = reflexOrderNone.ReflexOrderCode;

            YellowstonePathology.Business.Client.Model.StandingOrderNotSet standingOrderNotSet = new YellowstonePathology.Business.Client.Model.StandingOrderNotSet();
            this.m_HPVStandingOrderCode = standingOrderNotSet.StandingOrderCode;
            this.m_HPV1618StandingOrderCode = standingOrderNotSet.StandingOrderCode;
		}        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool OrderHPV
        {
            get { return this.m_OrderHPV; }
            set
            {
                if (this.m_OrderHPV != value)
                {
                    this.m_OrderHPV = value;
                    this.NotifyPropertyChanged("OrderHPV");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool OrderNGCT
        {
            get { return this.m_OrderNGCT; }
            set
            {
                if (this.m_OrderNGCT != value)
                {
                    this.m_OrderNGCT = value;
                    this.NotifyPropertyChanged("OrderNGCT");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool OrderTrichomonas
        {
            get { return this.m_OrderTrichomonas; }
            set
            {
                if (this.m_OrderTrichomonas != value)
                {
                    this.m_OrderTrichomonas = value;
                    this.NotifyPropertyChanged("OrderTrichomonas");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool OrderHPV1618
        {
            get { return this.m_OrderHPV1618; }
            set
            {
                if (this.m_OrderHPV1618 != value)
                {
                    this.m_OrderHPV1618 = value;
                    this.NotifyPropertyChanged("OrderHPV1618");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string HPVReflexOrderCode
        {
            get { return this.m_HPVReflexOrderCode; }
            set
            {
                if (this.m_HPVReflexOrderCode != value)
                {
                    this.m_HPVReflexOrderCode = value;
                    this.NotifyPropertyChanged("HPVReflexOrderCode");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'STNDNONE'", "varchar")]
        public string HPVStandingOrderCode
        {
            get { return this.m_HPVStandingOrderCode; }
            set
            {
                if (this.m_HPVStandingOrderCode != value)
                {
                    this.m_HPVStandingOrderCode = value;
                    this.NotifyPropertyChanged("HPVStandingOrderCode");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string HPV1618ReflexOrderCode
        {
            get { return this.m_HPV1618ReflexOrderCode; }
            set
            {
                if (this.m_HPV1618ReflexOrderCode != value)
                {
                    this.m_HPV1618ReflexOrderCode = value;
                    this.NotifyPropertyChanged("HPV1618ReflexOrderCode");
                }
            }
        }
                       

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'STNDNONE'", "varchar")]
        public string HPV1618StandingOrderCode
        {
            get { return this.m_HPV1618StandingOrderCode; }
            set
            {
                if (this.m_HPV1618StandingOrderCode != value)
                {
                    this.m_HPV1618StandingOrderCode = value;
                    this.NotifyPropertyChanged("HPV1618StandingOrderCode");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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

        public void SetExptectedFinalTime(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Audit.Model.IsOKToAdjustWHPExpectedFinalTimeAuditCollection auditCollection = new Audit.Model.IsOKToAdjustWHPExpectedFinalTimeAuditCollection(accessionOrder);
            auditCollection.Run();

            if (auditCollection.ActionRequired == false)
            {
                this.m_ExpectedFinalTime = DateTime.Now;   
            }
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
			YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
			YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

			if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrepPap.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrepPap.PanelSetId);
				result.AppendLine(panelSetOrderCytology.ToResultString(accessionOrder));
				result.AppendLine();
			}

			if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
				result.AppendLine(hpvTestOrder.ToResultString(accessionOrder));
				result.AppendLine();
			}

			if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
				result.AppendLine(panelSetOrderHPV1618.ToResultString(accessionOrder));
				result.AppendLine();
			}

			if (accessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
				result.AppendLine(panelSetOrderNGCT.ToResultString(accessionOrder));
				result.AppendLine();
			}

			if (accessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
				result.AppendLine(reportOrderTrichomonas.ToResultString(accessionOrder));
				result.AppendLine();
			}

			return result.ToString();
		}
	}
}
