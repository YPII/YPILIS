using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Test.RetrospectiveReview
{
    public class RetrospectiveReviewTestOrderDetailCollection : ObservableCollection<RetrospectiveReviewTestOrderDetail>
    {
        public void Sync(DataTable dataTable, string reportNo)
        {
            this.RemoveDeleted(dataTable, reportNo);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string id = dataTableReader["RetrospectiveReviewTestOrderDetailId"].ToString();
                string detailReportNo = dataTableReader["ReportNo"].ToString();
                RetrospectiveReviewTestOrderDetail rsrd = null;

                if (this.Exists(id) == true)
                {
                    rsrd = this.Get(id);
                }
                else
                {
                    if (detailReportNo == reportNo)
                    {
                        rsrd = new RetrospectiveReviewTestOrderDetail();
                        this.Add(rsrd);
                    }
                }

                if (rsrd != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(rsrd, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public RetrospectiveReviewTestOrderDetail Get(string id)
        {
            foreach (RetrospectiveReviewTestOrderDetail item in this)
            {
                if (item.RetrospectiveReviewTestOrderDetailId == id)
                {
                    return item;
                }
            }
            return null;
        }

        public bool Exists(string retrospectiveReviewTestOrderDetailId)
        {
            bool result = false;
            foreach (RetrospectiveReviewTestOrderDetail item in this)
            {
                if (item.RetrospectiveReviewTestOrderDetailId == retrospectiveReviewTestOrderDetailId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void RemoveDeleted(DataTable dataTable, string reportNo)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                string detailReportNo = string.Empty;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    detailReportNo = dataTable.Rows[idx]["ReportNo"].ToString();
                    string id = dataTable.Rows[idx]["RetrospectiveReviewTestOrderDetailId"].ToString();
                    if (this[i].RetrospectiveReviewTestOrderDetailId == id && detailReportNo == reportNo)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false && detailReportNo == reportNo)
                {
                    this.RemoveItem(i);
                }
            }
        }

    }
}
