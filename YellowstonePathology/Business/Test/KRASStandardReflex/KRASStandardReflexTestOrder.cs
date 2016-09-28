using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
	[PersistentClass("tblKRASStandardReflexTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class KRASStandardReflexTestOrder : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan, YellowstonePathology.Business.Interface.ISolidTumorTesting
	{		
		protected string m_Interpretation;
        protected string m_Comment;
        protected string m_TumorNucleiPercentage;
        protected string m_Indication;
        protected string m_IndicationComment;
        protected string m_Method;
        protected string m_ReportDisclaimer;        

        public KRASStandardReflexTestOrder()
        {
            
        }

		public KRASStandardReflexTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		public override void OrderInitialTests(Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
		{
            YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest krasStandardTest = new KRASStandard.KRASStandardTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(krasStandardTest.PanelSetId, orderTarget.GetId(), true) == false)
			{
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(krasStandardTest, orderTarget, true);
                YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                accessionOrder.TakeATrip(orderTestOrderVisitor);     				
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if (this.m_Interpretation != value)
				{
					this.m_Interpretation = value;
					this.NotifyPropertyChanged("Interpretation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string IndicationComment
        {
            get { return this.m_IndicationComment; }
            set
            {
                if (this.m_IndicationComment != value)
                {
                    this.m_IndicationComment = value;
                    this.NotifyPropertyChanged("IndicationComment");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ReportDisclaimer
		{
			get { return this.m_ReportDisclaimer; }
			set
			{
				if (this.m_ReportDisclaimer != value)
				{
					this.m_ReportDisclaimer = value;
					this.NotifyPropertyChanged("ReportDisclaimer");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            StringBuilder result = new StringBuilder();
            result.Append("Result: ");
            result.AppendLine(this.m_IndicationComment);
            result.Append("Indication: ");
            result.AppendLine(this.m_Interpretation);            
            result.Append("Tumor Nuclei Percent: ");
            result.AppendLine(this.m_TumorNucleiPercentage);
            return result.ToString();
        }

		public void SetSummaryResult(YellowstonePathology.Business.Test.LynchSyndrome.LSEResult lSEResult)
        {
			
        }

		public void UpdateFromChildren(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder testOrder)
		{
			this.Indication = testOrder.Indication;
			this.IndicationComment = testOrder.IndicationComment;
			this.TumorNucleiPercentage = testOrder.TumorNucleiPercentage;
			this.Comment = testOrder.Comment;

			YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new BRAFV600EK.BRAFV600EKTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder brafV600EKTestOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId);
				brafV600EKTestOrder.Indication = testOrder.Indication;
				brafV600EKTestOrder.IndicationComment = testOrder.IndicationComment;
				brafV600EKTestOrder.TumorNucleiPercentage = testOrder.TumorNucleiPercentage;
				brafV600EKTestOrder.Comment = testOrder.Comment;
			}
		}

		public void UpdateFromChildren(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder testOrder)
		{
			this.Indication = testOrder.Indication;
			this.IndicationComment = testOrder.IndicationComment;
			this.TumorNucleiPercentage = testOrder.TumorNucleiPercentage;
			this.Comment = testOrder.Comment;
		}
	}
}
