using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Slide.Model
{
	[PersistentClass(true, "tblSlideOrder", "YPIDATA")]
	public class SlideOrder : SlideOrder_Base
	{        		        
        private YellowstonePathology.Business.Test.Model.TestOrder_Base m_TestOrder;        
		
		public SlideOrder()
		{            
            
		}

        public SlideOrder(string objectId, string slideOrderId, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder,
            YellowstonePathology.Business.Test.Model.TestOrder testOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            int slideNumber)
            : base(objectId, slideOrderId, aliquotOrder, testOrder, systemIdentity, slideNumber)
        {}

        public YellowstonePathology.Business.Test.Model.TestOrder_Base TestOrder
        {
            get { return this.m_TestOrder; }
            set { this.m_TestOrder = value; }
        }        		           
	}
}
