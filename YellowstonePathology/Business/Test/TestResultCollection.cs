using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class TestResultCollection : ObservableCollection<TestResult>
	{
        public TestResultCollection()
		{

		}

        public TestResult GetResult(PanelSetOrder panelSetOrder)
		{
            TestResult result = null;
            foreach (TestResult testResult in this)
            {
                if (testResult.ResultCode == panelSetOrder.ResultCode)
                {
                    result = testResult;
                    break;
                }
            }
			return result;
		}        
	}
}
