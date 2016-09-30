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
	public class SurgicalSpecimenAuditCollection : ObservableCollection<SurgicalSpecimenAudit>
	{
		public const string PREFIXID = "SSRA";

		public SurgicalSpecimenAuditCollection()
        {

        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string surgicalSpecimenAuditId = element.Element("SurgicalSpecimenAuditId").Value;
                    if (this[i].SurgicalSpecimenAuditId == surgicalSpecimenAuditId)
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

        public SurgicalSpecimenAudit GetNextItem(string surgicalAuditId, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, string amendmentId)
		{
			string surgicalSpecimenAuditId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			SurgicalSpecimenAudit surgicalSpecimenAudit = new SurgicalSpecimenAudit(surgicalSpecimenAuditId, surgicalSpecimenAuditId, surgicalAuditId, surgicalSpecimen, amendmentId);

			return surgicalSpecimenAudit;
		}

        public bool Exists(string surgicalSpecimenAuditId)
        {
            bool result = false;
            foreach(SurgicalSpecimenAudit surgicalSpecimenAudit in this)
            {
                if(surgicalSpecimenAudit.SurgicalSpecimenAuditId == surgicalSpecimenAuditId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public SurgicalSpecimenAudit Get(string surgicalSpecimenAuditId)
        {
            SurgicalSpecimenAudit result = null;
            foreach (SurgicalSpecimenAudit surgicalSpecimenAudit in this)
            {
                if (surgicalSpecimenAudit.SurgicalSpecimenAuditId == surgicalSpecimenAuditId)
                {
                    result = surgicalSpecimenAudit;
                    break;
                }
            }
            return result;
        }

        public void Sync(DataTable dataTable, string surgicalAuditId)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string surgicalSpecimenAuditId = dataTableReader["SurgicalSpecimenAuditId"].ToString();
                string specimenSurgicalAuditId = dataTableReader["SurgicalAuditId"].ToString();

                SurgicalSpecimenAudit surgicalSpecimenAudit = null;

                if (this.Exists(surgicalSpecimenAuditId) == true)
                {
                    surgicalSpecimenAudit = this.Get(surgicalSpecimenAuditId);
                }
                else if (surgicalAuditId == specimenSurgicalAuditId)
                {
                    surgicalSpecimenAudit = new SurgicalSpecimenAudit();
                    this.Add(surgicalSpecimenAudit);
                }

                if (surgicalSpecimenAudit != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(surgicalSpecimenAudit, dataTableReader);
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
                    string surgicalSpecimenAuditId = dataTable.Rows[idx]["SurgicalSpecimenAuditId"].ToString();
                    if (this[i].SurgicalSpecimenAuditId == surgicalSpecimenAuditId)
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
