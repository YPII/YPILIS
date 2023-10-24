using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace YellowstonePathology.Business
{
    public class SvhAuditList : ObservableCollection<SvhAuditItem>
    {
        public SvhAuditList()
        {

        }

        public static SvhAuditList FromJSON(JArray items)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            SvhAuditList result = new SvhAuditList();
            foreach (JObject item in items)
            {                                
                SvhAuditItem auditItem = new SvhAuditItem(item["lastName"].ToString(), item["firstName"].ToString(), item["svhNo"].ToString(), item["mrn"].ToString(), item["dob"].ToString(), item["dos"].ToString());
                result.Items.Add(auditItem);
            }
            return result;
        }
    }
}
