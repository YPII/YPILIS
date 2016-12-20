using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Business.SpecialStain
{
	public class StainResultItemCollection : ObservableCollection<StainResultItem>
	{
		public const string PREFIXID = "STR";

        public StainResultItemCollection()
        {

        }

        public void RemoveDeleted(List<string> stainResultIdList)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (string id in stainResultIdList)
                {
                    if (this[i].StainResultId == id)
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

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string stainResultId = element.Element("StainResultId").Value;
                    if (this[i].StainResultId == stainResultId)
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

        public StainResultItem GetNextItem(string surgicalSpecimenId)
		{
			string stainResultId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			string objectId = stainResultId;
			StainResultItem stainResultItem = new StainResultItem(objectId, stainResultId, surgicalSpecimenId);
			return stainResultItem;
		}

		public StainResultItem GetCurrent()
		{
			return this.Count > 0 ? this[0] : null;
		}

        public void RemoveGradedStains(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
        {
            foreach (StainResultItem stainResultItem in this)
            {
                if (stainResultItem.IsGraded == true)
                {
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = testOrderCollection.Get(stainResultItem.TestOrderId);
                    testOrderCollection.Remove(testOrder);
                }
            }
        }

		public StainResultItem Get(string stainResultId)
		{
			foreach (StainResultItem item in this)
			{
				if (item.StainResultId == stainResultId)
				{
					return item;
				}
			}
			return null;
		}

        public StainResultItem GetCurrent(string stainResultId)
        {
            foreach (StainResultItem item in this)
            {
                if (item.StainResultId == stainResultId)
                {
                    return item;
                }
            }
            return null;
        }

        public bool HasDuplicates()
        {
            bool result = false;
            var duplicates = this
                .GroupBy(i => i.ProcedureName)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
            if (duplicates.Count() != 0)
            {
                result = true;
            }
            return result;
        }

		public List<string> GetIdsForProcedure(string procedureName)
		{
			List<string> result = new List<string>();
			foreach (StainResultItem stainresult in this)
			{
				if (stainresult.ProcedureName == procedureName) result.Add(stainresult.StainResultId);
			}
			return result;
		}

		public StainResultItemCollection GetCytochemicalStains(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
		{
			StainResultItemCollection result = new StainResultItemCollection();
			foreach (StainResultItem stainresult in this)
			{
                if (stainresult.StainType == "Cytochemical" && stainresult.ClientAccessioned == false)
                {
                    result.Add(stainresult);
                }
			}
			return result;
		}

		public StainResultItemCollection GetSingleplexStains(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
		{
			StainResultItemCollection result = new StainResultItemCollection();
			foreach (StainResultItem stainresult in this)
			{
				if (stainresult.StainType == "Immunohistochemical" && stainresult.ClientAccessioned == false)
				{
					YellowstonePathology.Business.Test.Model.TestOrder testOrder = testOrderCollection.Get(stainresult.TestOrderId);
					if (testOrder.OrderedAsDual == false)
					{
						result.Add(stainresult);
					}
				}
			}
			return result;
		}

		public StainResultItemCollection GetMultiplexStains(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
		{
			StainResultItemCollection result = new StainResultItemCollection();
			foreach (StainResultItem stainresult in this)
			{
                if (stainresult.StainType == "Immunohistochemical" && stainresult.ClientAccessioned == false)
				{
					YellowstonePathology.Business.Test.Model.TestOrder testOrder = testOrderCollection.Get(stainresult.TestOrderId);
					if (testOrder.OrderedAsDual == true)
					{
						result.Add(stainresult);
					}
				}
			}
			return result;
		}

		public StainResultItemCollection GetGradedStains(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
		{
			StainResultItemCollection result = new StainResultItemCollection();
			YellowstonePathology.Business.Test.Model.TestCollection allTests = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
			foreach (StainResultItem stainresult in this)
			{
				YellowstonePathology.Business.Test.Model.TestOrder testOrder = testOrderCollection.Get(stainresult.TestOrderId);
				YellowstonePathology.Business.Test.Model.GradedTest gradedTest = allTests.GetTest(testOrder.TestId) as YellowstonePathology.Business.Test.Model.GradedTest;
				if (gradedTest != null) result.Add(stainresult);
			}
			return result;
		}

        public StainResultItem GetStainResult(string testOrderId)
        {
            StainResultItem result = null;            
            foreach (StainResultItem stainresult in this)
            {
                if (stainresult.TestOrderId == testOrderId)
                {
                    result = stainresult;
                    break;
                }
            }
            return result;
        }

        public int GetGradedStainCount()
        {
            int result = 0;
            foreach (StainResultItem stainResultItem in this)
            {
                if (stainResultItem.IsGraded == true)
                {
                    result += 1;
                }
            }
            return result;
        }        

        public bool TestOrderExists(string testOrderId)
        {
            bool result = false;            
            foreach (StainResultItem stainresult in this)
            {
                if (stainresult.TestOrderId == testOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string stainResultId)
        {
            bool result = false;
            foreach (StainResultItem stainresult in this)
            {
                if (stainresult.StainResultId == stainResultId)
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

        public void Sync(DataTable dataTable, string surgicalSpecimenId)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string stainResultId = dataTableReader["StainResultId"].ToString();
                string stainResultSurgicalSpecimenId = dataTableReader["SurgicalSpecimenId"].ToString();

                StainResultItem stainResult = null;

                if (this.Exists(stainResultId) == true)
                {
                    stainResult = this.Get(stainResultId);
                }
                else if (surgicalSpecimenId == stainResultSurgicalSpecimenId)
                {
                    stainResult = new StainResultItem();
                    this.Add(stainResult);
                }

                if (stainResult != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(stainResult, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string stainResultId = dataTable.Rows[idx]["StainResultId"].ToString();
                    if (this[i].StainResultId == stainResultId)
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
    }
}
