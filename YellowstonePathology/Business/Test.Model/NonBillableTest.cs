using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class NonBillableTest : Test
    {
        public NonBillableTest()
        {
            this.m_IsBillable = false;
        }
    }
}
