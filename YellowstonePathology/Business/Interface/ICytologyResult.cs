using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Interface
{
	public interface ICytologyResult
	{		
		int ScreenedById { get; set; }		
		string ScreenedByName { get; set; }		
		int ResultCode { get; set; }	
		string SpecimenAdequacy { get; set; }
		string ScreeningImpression { get; set; }
		string ScreeningImpressionComment { get; set; }
		string OtherConditions { get; set; }
		string ReportComment { get; set; }
		string InternalComment { get; set; }
		string ScreeningType { get; set; }
		bool QC { get; set; }		
        bool ImagerError { get; set; }
        bool NoCharge { get; set; }
        bool PhysicianInterpretation { get; set; }
        int SlideCount { get; set; }
        bool ECCCheckPerformed { get; set; }
        bool ScreeningError { get; set; }
	}
}
