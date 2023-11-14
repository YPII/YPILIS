using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test.Model
{
	public class StainTestCollection : ObservableCollection<StainTest>
	{
		public StainTestCollection()
		{

		}

        public bool IsIHC(int testId)
        {
            bool result = false;
            if (this.Exists(testId) == true)
            {
                StainTest stainTest = this.GetByTestId(testId);
                if (stainTest.StainType.ToUpper() == "IMMUNOHISTOCHEMICAL")
                {
                    result = true;
                }
            }
            return result;
        }

        public bool Exists(int testId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.Model.StainTest stainTest in this)
            {
                if (stainTest.TestId == testId)
                {
                    result = true;
                }
            }
            return result;
        }

        public StainTest GetByTestId(int testId)
        {
            StainTest result = null;
            foreach (YellowstonePathology.Business.Test.Model.StainTest stainTest in this)
            {
                if (stainTest.TestId == testId)
                {
                    result = stainTest;
                }
            }
            return result;
        }
	}
}
