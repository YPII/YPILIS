using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.TestCancelled
{
	[PersistentClass("tblTestCancelledTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class TestCancelledTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Comment;
        private int m_CancelledTestId;
        private string m_CancelledTestName;

        public TestCancelledTestOrder()
        {

        }

        public TestCancelledTestOrder(string masterAccessionNo, string reportNo)
        {
            this.MasterAccessionNo = masterAccessionNo;
            this.ReportNo = reportNo;            
        }

		public TestCancelledTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,            
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
			this.m_NoCharge = true;
		}       

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public int CancelledTestId
        {
            get { return this.m_CancelledTestId; }
            set { this.m_CancelledTestId = value; }
        }

        [YellowstonePathology.Business.Persistence.PersistentProperty()]
        public string CancelledTestName
        {
            get { return this.m_CancelledTestName; }
            set { this.m_CancelledTestName = value; }
        }

		public override string ToResultString(Business.Test.AccessionOrder accessionOrder)
		{
			return "No results for a cancelled test.";
		}
    }
}
