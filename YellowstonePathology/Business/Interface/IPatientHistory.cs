using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Interface
{
	public interface IPatientHistory
	{
		string PatientId { get; set; }
		int MasterAccessionNo { get; set; }
		string ReportNo { get; set; }
		string AccessionDate { get; set; }
		string Result { get; set; }
		Nullable<DateTime>FinalDate { get; set; }
	}
}
