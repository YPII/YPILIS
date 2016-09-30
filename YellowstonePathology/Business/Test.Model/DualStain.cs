using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DualStain
    {
        protected string m_TestId;
        protected string m_TestName;
		protected Test m_FirstTest;
		protected Test m_SecondTest;
		protected int m_DepricatedFirstTestId;
		protected int m_DepricatedSecondTestId;
        private string m_OrderComment;

        public DualStain()
        {
            
        }

        public string TestId
        {
            get { return this.m_TestId; }
        }

        public string TestName
        {
            get { return this.m_TestName; }
        }

		public Test FirstTest
        {
            get { return this.m_FirstTest; }            
        }

		public Test SecondTest
        {
            get { return this.m_SecondTest; }
        }        

		public int DepricatedFirstTestId
		{
			get { return this.m_DepricatedFirstTestId; }
		}

		public int DepricatedSecondTestId
		{
			get { return this.m_DepricatedSecondTestId; }
		}

        public string OrderComment
        {
            get { return this.m_OrderComment; }
            set { this.m_OrderComment = value; }
        }

        public YellowstonePathology.Business.Test.Model.TestOrder GetTestOrder(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection, int stainNumber)
        {
            YellowstonePathology.Business.Test.Model.TestOrder result = null;
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in testOrderCollection)
            {
                if (stainNumber == 1)
                {
                    if (testOrder.TestId == this.m_FirstTest.TestId || testOrder.TestId == this.m_DepricatedFirstTestId)
                    {
                        result = testOrder;
                    }
                }
                else if (stainNumber == 2)
                {
                    if (testOrder.TestId == this.m_SecondTest.TestId || testOrder.TestId == this.m_DepricatedSecondTestId)
                    {
                        result = testOrder;
                    }
                }
            }
            return result;
        }

		public static int CompareByTestName(DualStain x, DualStain y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				else
				{
					return -1;
				}
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				else
				{
					int retval = x.m_TestName.CompareTo(y.m_TestName);
					if (retval != 0)
					{
						return retval;
					}
					else
					{
						return x.m_TestName.CompareTo(y.m_TestName);
					}
				}
			}
		}
	}
}
