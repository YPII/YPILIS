using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Linq;
using System.Xml.Linq;


namespace YellowstonePathology.Business.Test.Model
{
	public class TestOrderCollection_Base : ObservableCollection<TestOrder_Base>
	{
        public TestOrderCollection_Base()
		{
            
		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string testOrderId = element.Element("TestOrderId").Value;
                    if (this[i].TestOrderId == testOrderId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public TestOrder_Base GetTestOrderBase(int testId)
        {
            foreach (TestOrder item in this)
            {
                if (item.TestId == testId)
                {
                    return item;
                }
            }
            return null;
        }

        public TestOrder_Base GetBase(string testOrderId)
        {
            foreach (TestOrder item in this)
            {
                if (item.TestOrderId == testOrderId)
                {
                    return item;
                }
            }
            return null;
        }	                  

        public void Remove(string testOrderId)
        {
            foreach (TestOrder testOrder in this)
            {
                if (testOrder.TestOrderId == testOrderId)
                {
                    this.Remove(testOrder);
                    break;
                }
            }
        }

        public bool OnlyHasOneHAndE()
        {
            bool result = false;
            if (this.Count == 1)
            {
                foreach (TestOrder testOrder in this)
                {
                    if (testOrder.TestId == 49)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public int GetUniqueTestCount()
        {
            TestOrderCollection testOrderCollection = new TestOrderCollection();
            foreach (TestOrder testOrder in this)
            {
                if (testOrderCollection.Exists(testOrder.TestId) == false)
                {
                    testOrderCollection.Add(testOrder);
                }
            }
            return testOrderCollection.Count;
        }

        public int GetBillableCytochemicalStainCount()
        {
            int result = 0;
            YellowstonePathology.Business.Test.Model.TestCollection cytochemicalTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetCytochemicalTests();
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this)
            {
                if (testOrder.NoCharge == false)
                {
                    if (cytochemicalTestCollection.Exists(testOrder.TestId) == true)
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }

        public int GetCytochemicalForMicroorganismsStainCount()
        {
            int result = 0;
            YellowstonePathology.Business.Test.Model.TestCollection cytochemicalForMicroorganismsTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetCytochemicalForMicroorganismsTests();
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this)
            {
                if (testOrder.NoCharge == false)
                {
                    if (cytochemicalForMicroorganismsTestCollection.Exists(testOrder.TestId) == true)
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }

        public int GetBillableGradeStainCount(bool includeOrderedAsDual)
        {
            int result = 0;
            YellowstonePathology.Business.Test.Model.TestCollection gradedTestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetGradedTests();

            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this)
            {
                if (testOrder.NoCharge == false)
                {
                    if (gradedTestCollection.Exists(testOrder.TestId) == true)
                    {
                        if (includeOrderedAsDual == true)
                        {
                            result += 1;
                        }
                        else if (includeOrderedAsDual == false)
                        {
                            if (testOrder.OrderedAsDual == false)
                            {
                                result += 1;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public int GetBillableHANDECount()
        {
            int result = 0;
            YellowstonePathology.Business.Test.Model.HandE hande = new HandE();

            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this)
            {
                if (testOrder.NoCharge == false)
                {
                    if (hande.TestId == testOrder.TestId)
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
            foreach (TestOrder_Base item in this)
            {
                if (item.TestId == testId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string testOrderId)
        {
            bool result = false;
            foreach (TestOrder_Base testOrder in this)
            {
                if (testOrder.TestOrderId == testOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }                

        public bool HasTestBeenOrdered(int testId)
        {
            bool result = false;
            foreach (TestOrder_Base item in this)
            {
                if (item.TestId == testId)
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
