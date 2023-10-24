using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class MLH1MethylationAnalysisResultCodeCollection : ObservableCollection<string>
    {
        public MLH1MethylationAnalysisResultCodeCollection()
        {
            MLH1MethylationAnalysisDetectedResult mlh1MethylationAnalysisDetectedResult = new MLH1MethylationAnalysisDetectedResult();
            MLH1MethylationAnalysisNotDetectedResult mlh1MethylationAnalysisNotDetectedResult = new MLH1MethylationAnalysisNotDetectedResult();
            MLH1MethylationAnalysisNotSetResult mlh1MethylationAnalysisNotSetResult = new MLH1MethylationAnalysisNotSetResult();

            this.Add(mlh1MethylationAnalysisDetectedResult.ResultCode);
            this.Add(mlh1MethylationAnalysisNotDetectedResult.ResultCode);
            this.Add(mlh1MethylationAnalysisNotSetResult.ResultCode);
        }
    }
}
