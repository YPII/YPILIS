using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business
{
	[XmlType("ReportNoCollection")]
	public class ReportNoCollection : ObservableCollection<ReportNo>
	{
		public ReportNoCollection()
		{

		}

        public bool Exists(string reportNoString)
        {
            bool result = false;
            foreach (ReportNo reportNo in this)
            {
                if (reportNo.Value == reportNoString)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
	}
}
