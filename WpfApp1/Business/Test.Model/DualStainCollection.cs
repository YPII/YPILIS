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

        public bool Exists(string testId)
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

        public DualStain Get(string testId)
        {
            DualStain result = null;
            foreach (DualStain dualStain in this)
            {
                if (dualStain.TestId == testId)
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

            stainList.Add((DualStain)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("CD30CD20")); // CD3CD20DualStain());
            stainList.Add((DualStain)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("PAX5CD5")); // PAX5CD5DualStain());
			stainList.Add((DualStain)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("CK34P504RM")); // Cytokeratin34P504sRacemaseDualStain());
			stainList.Add((DualStain)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("KI67MA")); // Ki67MelanADualStain());
            stainList.Add((DualStain)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("OSCRSMM")); // OSCARSmoothMuscleMyosinDualStain());			
            stainList.Add((DualStain)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("TTFNPSNA")); // TTF1NapsinADualStain());

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
                if (testOrderCollection.ExistsByTestId(ds.FirstTest.TestId) == true && testOrderCollection.ExistsByTestId(ds.SecondTest.TestId) == true)
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
