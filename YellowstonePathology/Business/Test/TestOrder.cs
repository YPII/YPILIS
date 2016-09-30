using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
	[PersistentClass(true, "tblTestOrder", "YPIDATA")]
	public class TestOrder : TestOrder_Base
	{		
        private YellowstonePathology.Business.Test.AliquotOrder_Base m_AliquotOrder_Base;
        private YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base m_SlideOrderCollection;		
		
		public TestOrder()
		{
            this.m_AliquotOrder_Base = new Business.Test.AliquotOrder_Base();
            this.m_SlideOrderCollection = new Business.Slide.Model.SlideOrderCollection();
		}

		public TestOrder(string testOrderId, string objectId, string panelOrderId, string aliquotOrderId, YellowstonePathology.Business.Test.Model.Test test, string comment)
		{
			this.m_TestOrderId = testOrderId;
			this.m_ObjectId = objectId;
			this.m_PanelOrderId = panelOrderId;
            this.m_AliquotOrderId = aliquotOrderId;
			this.m_TestId = test.TestId;
			this.m_TestName = test.TestName;
            this.m_TestAbbreviation = test.TestAbbreviation;
			this.m_Result = null;
            this.m_Comment = comment;

            this.m_SlideOrderCollection = new Business.Slide.Model.SlideOrderCollection();
		}		

        public YellowstonePathology.Business.Test.AliquotOrder_Base AliquotOrder
        {
            get { return this.m_AliquotOrder_Base; }
            set { this.m_AliquotOrder_Base = value; }
        }

        [PersistentCollection(true)]
        public YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base SlideOrderCollection
        {
            get { return this.m_SlideOrderCollection; }
            set { this.m_SlideOrderCollection = value; }
        }

        public override void PullOver(Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }

        public string DisplayString
        {
            get 
            {
                string result = this.AliquotOrder.Display + "  -  " + this.m_TestName;
                if (this.m_OrderedAsDual == true)
                {
                    result += ", Ordered As Dual";
                }                
                if (string.IsNullOrEmpty(Comment) == false)
                {
                    result += ", " + this.m_Comment;
                }
                return result;
            }
        }
	}
}
