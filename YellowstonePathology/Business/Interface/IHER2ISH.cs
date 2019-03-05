using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Interface
{
    public interface IHER2ISH
    {
        string ReportNo { get; set; }
        string Result { get; set; }
        bool RecountRequired { get; set; }
        bool HER2ByIHCRequired { get; set; }
        string GeneticHeterogeneity { get; set; }
        string Indicator { get; set; }
        string Her2Chr17ClusterRatio { get; set; }
        string ResultComment { get; set; }
        string InterpretiveComment { get; set; }
        string ResultDescription { get; set; }
        string CommentLabel { get; set; }
        string ReportReference { get; set; }
        bool NoCharge { get; set; }
        double? AverageHer2Chr17SignalAsDouble { get; }
        double? AverageHer2NeuSignal { get; }
        double? Her2Chr17Ratio { get; }
        int CellCountToUse { get; }
    }
}
