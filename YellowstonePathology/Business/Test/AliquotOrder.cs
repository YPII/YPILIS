using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    [PersistentClass("tblAliquotOrder", "YPIDATA")]
    public class AliquotOrder : AliquotOrder_Base
	{		
		private YellowstonePathology.Business.Test.Model.TestOrderCollection_Base m_TestOrderCollection;		
        private YellowstonePathology.Business.Slide.Model.SlideOrderCollection m_SlideOrderCollection;        

        public AliquotOrder()
        {
            this.m_TestOrderCollection = new YellowstonePathology.Business.Test.Model.TestOrderCollection_Base();            
            this.m_SlideOrderCollection = new YellowstonePathology.Business.Slide.Model.SlideOrderCollection();
        }        

		public AliquotOrder(string aliquotOrderId, string objectId, string specimenOrderId)
		{
			this.m_AliquotOrderId = aliquotOrderId;
			this.m_ObjectId = objectId;
			this.m_SpecimenOrderId = specimenOrderId;

            this.m_TestOrderCollection = new YellowstonePathology.Business.Test.Model.TestOrderCollection_Base();			
			this.m_SlideOrderCollection = new YellowstonePathology.Business.Slide.Model.SlideOrderCollection();
		}

        public YellowstonePathology.Business.Test.Model.TestOrderCollection_Base TestOrderCollection
        {
            get { return this.m_TestOrderCollection; }
        }

        [PersistentCollection()]
        public YellowstonePathology.Business.Slide.Model.SlideOrderCollection SlideOrderCollection
        {
            get { return this.m_SlideOrderCollection; }
            set { this.m_SlideOrderCollection = value; }
        }

        public bool DoesSlideBelongToThisAliquotOrder(string slideOrderId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this.SlideOrderCollection)
            {
                if (slideOrder.SlideOrderId == slideOrderId)
                {                    
                    result = true;
                    break;
                }
            }
            return result;
        }
        
        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }        
	}
}
