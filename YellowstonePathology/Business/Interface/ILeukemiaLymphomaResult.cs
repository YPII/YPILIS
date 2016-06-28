using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
    public interface ILeukemiaLymphomaResult
    {		
		string GatingPopulationV2  {get; set; }
		int LymphocyteCount {get; set;}
		int MonocyteCount { get; set; }
		int MyeloidCount { get; set; }
		int DimCD45ModSSCount { get; set; }
		int OtherCount { get; set; }
		string OtherName { get; set; }
		string InterpretiveComment { get; set; }
		string Impression { get; set; }
		string CellPopulationOfInterest { get; set; }
		string TestResult { get; set; }
		string CellDescription { get; set; }
		string BTCellSelection { get; set; }
		string EGateCD34Percent { get; set; }
		string EGateCD117Percent { get; set; }
		double LymphocyteCountPercent { get; }
		double MonocyteCountPercent { get; }
		double MyeloidCountPercent { get; }
		double DimCD45ModSSCountPercent { get; }

    }
}
