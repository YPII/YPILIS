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
	public class SurgicalAuditCollection : ObservableCollection<SurgicalAudit>
	{
		public const string PREFIXID = "SRA";

		public SurgicalAuditCollection()
        {

		}

        public bool Exists(string surgicalAuditId)
        {
            bool result = false;
            foreach(SurgicalAudit surgicalAudit in this)
            {
                if(surgicalAudit.SurgicalAuditId == surgicalAuditId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public SurgicalAudit Get(string surgicalAuditId)
        {
            SurgicalAudit result = null;
            foreach (SurgicalAudit surgicalAudit in this)
            {
                if (surgicalAudit.SurgicalAuditId == surgicalAuditId)
                {
                    result = surgicalAudit;
                    break;
                }
            }
            return result;
        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string surgicalAuditId = element.Element("SurgicalAuditId").Value;
                    if (this[i].SurgicalAuditId == surgicalAuditId)
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

        public SurgicalAudit GetNextItem(string amendmentId, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical, int pathologistId, int assignedToId)
		{
			string surgicalAuditId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			SurgicalAudit surgicalAudit = new Surgical.SurgicalAudit(surgicalAuditId, surgicalAuditId, amendmentId, panelSetOrderSurgical, pathologistId, assignedToId);
			return surgicalAudit;
		}

        public void SetAmendmentReference(YellowstonePathology.Business.Amendment.Model.Amendment amendment)
        {
            foreach (SurgicalAudit surgicalAudit in this)
            {
                if (surgicalAudit.AmendmentId == amendment.AmendmentId)
                {
                    surgicalAudit.Amendment = amendment;
                }
            }
        }
	}
}
