using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class IntraoperativeConsultationResultCollection : ObservableCollection<IntraoperativeConsultationResult>
	{
		public const string PREFIXID = "IC";

		public IntraoperativeConsultationResultCollection()
        {

        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string intraoperativeConsultationResultId = element.Element("IntraoperativeConsultationResultId").Value;
                    if (this[i].IntraoperativeConsultationResultId == intraoperativeConsultationResultId)
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

        public IntraoperativeConsultationResult GetNextItem(string surgicalSpecimenId)
		{
			string intraoperativeConsultationResultId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			IntraoperativeConsultationResult intraoperativeConsultationResult = new IntraoperativeConsultationResult(intraoperativeConsultationResultId, intraoperativeConsultationResultId, surgicalSpecimenId);

			return intraoperativeConsultationResult;
		}

		public IntraoperativeConsultationResult GetCurrent()
		{
			return this.Count > 0 ? this[0] : null;
		}

		public IntraoperativeConsultationResult GetCurrent(string intraoperativeConsultationResultId)
		{
			foreach (IntraoperativeConsultationResult item in this)
			{
				if (item.IntraoperativeConsultationResultId == intraoperativeConsultationResultId)
				{
					return item;
				}
			}
			return null;
		}

        public IntraoperativeConsultationResult Get(string intraoperativeConsultationResultId)
        {
            foreach (IntraoperativeConsultationResult item in this)
            {
                if (item.IntraoperativeConsultationResultId == intraoperativeConsultationResultId)
                {
                    return item;
                }
            }
            return null;
        }

        public bool TestOrderIdExists(string testOrderId)
        {
            bool result = false;
            foreach (IntraoperativeConsultationResult icr in this)
            {
                if (icr.TestOrderId == testOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string intraoperativeConsultationResultId)
        {
            bool result = false;
            foreach (IntraoperativeConsultationResult icr in this)
            {
                if (icr.IntraoperativeConsultationResultId == intraoperativeConsultationResultId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public IntraoperativeConsultationResult GetIntraoperativeConsultationResult(string testOrderId)
        {
            IntraoperativeConsultationResult result = null;
            foreach (IntraoperativeConsultationResult icr in this)
            {
                if (icr.TestOrderId == testOrderId)
                {
                    result = icr;
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
                string icd9BillingId = dataTableReader["IntraoperativeConsultationResultId"].ToString();
                string icSurgicalSpecimenId = dataTableReader["SurgicalSpecimenId"].ToString();

                IntraoperativeConsultationResult intraoperativeConsultationResult = null;

                if (this.Exists(icd9BillingId) == true)
                {
                    intraoperativeConsultationResult = this.Get(icd9BillingId);
                }
                else if (surgicalSpecimenId == icSurgicalSpecimenId)
                {
                    intraoperativeConsultationResult = new IntraoperativeConsultationResult();
                    this.Add(intraoperativeConsultationResult);
                }

                if (intraoperativeConsultationResult != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(intraoperativeConsultationResult, dataTableReader);
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
                    string intraoperativeConsultationResultId = dataTable.Rows[idx]["IntraoperativeConsultationResultId"].ToString();
                    if (this[i].IntraoperativeConsultationResultId == intraoperativeConsultationResultId)
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
