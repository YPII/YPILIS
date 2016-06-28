using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DualStainCollection : List<DualStain>
    {
        public DualStainCollection()
        {
			
		}

        public bool Exists(DualStain dualStain)
        {
            bool result = false;
            foreach (DualStain ds in this)
            {
                if (ds.TestId == dualStain.TestId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public int StainCount(bool includeDualsWithGradedStains)
        {
            int result = 0;
            if (includeDualsWithGradedStains == true)
            {
                result = this.Count();
            }
            else
            {
                foreach (DualStain dualStain in this)
                {
                    if (dualStain.FirstTest is GradedTest == false)
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }

        public bool Exists(int testId)
        {
            bool result = false;
            foreach (DualStain dualStain in this)
            {
                if (dualStain.FirstTest.TestId == testId || dualStain.SecondTest.TestId == testId ||
					dualStain.DepricatedFirstTestId == testId || dualStain.DepricatedSecondTestId == testId)
                {
                    result = true;
                }                
            }
            return result;
        }

        public DualStain Get(int testId)
        {
            DualStain result = null;
            foreach (DualStain dualStain in this)
            {
                if (dualStain.FirstTest.TestId == testId || dualStain.SecondTest.TestId == testId ||
                    dualStain.DepricatedFirstTestId == testId || dualStain.DepricatedSecondTestId == testId)
                {
                    result = dualStain;
                    break;
                }
            }
            return result;
        }

        public static DualStainCollection GetAll()
        {
            DualStainCollection result = new DualStainCollection();
			List<DualStain> stainList = new List<DualStain>();

			stainList.Add(new CD3CD20DualStain());
			stainList.Add(new CDX2VillinDualStain());
			stainList.Add(new Cytokeratin34P504sRacemaseDualStain());
			stainList.Add(new IgKappaIgLambdaDualStain());
			stainList.Add(new Ki67MelanADualStain());
			stainList.Add(new OSCARSmoothMuscleMyosinDualStain());
			stainList.Add(new PAX5Zap70DualStain());
			stainList.Add(new TTF1NapsinADualStain());

			stainList.Sort(DualStain.CompareByTestName);
			foreach (DualStain stain in stainList)
			{
				result.Add(stain);
			}
            return result;
        }

        public static ObservableCollection<object> GetAllAsObjectCollection()
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            DualStainCollection dualStainCollection = GetAll();
            foreach (DualStain dualStain in dualStainCollection)
            {
                result.Add(dualStain);
            }
            return result;
        }

        public static DualStainCollection GetCollection(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
        {
            DualStainCollection result = new DualStainCollection();
            DualStainCollection allDualStains = DualStainCollection.GetAll();

            foreach (DualStain ds in allDualStains)
            {
                if (testOrderCollection.Exists(ds.FirstTest.TestId) == true && testOrderCollection.Exists(ds.SecondTest.TestId) == true)
                {
                    TestOrder firstTestOrder = testOrderCollection.GetTestOrder(ds.FirstTest.TestId);
                    TestOrder secondTestOrder = testOrderCollection.GetTestOrder(ds.SecondTest.TestId);

                    if (firstTestOrder.NoCharge == false && secondTestOrder.NoCharge == false)
                    {
                        if (firstTestOrder.OrderedAsDual == true && secondTestOrder.OrderedAsDual == true)
                        {
                            if (result.Exists(ds) == false)
                            {
                                result.Add(ds);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
