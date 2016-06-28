using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEResultStatusCollection : ObservableCollection<LSEResultStatus>
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        

        public LSEResultStatusCollection(YellowstonePathology.Business.Test.LynchSyndrome.LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string lseType, string orderedOnId)
        {
            this.m_AccessionOrder = accessionOrder;

			if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET)
			{
				this.Add(new LSEResultStatusNotSet(lseResult, this.m_AccessionOrder, orderedOnId));
			}
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GYN)
            {
                this.Add(new LSEResultStatusMethylationAnalysisRequiredGYN(lseResult, this.m_AccessionOrder, orderedOnId));                
            }
            else
            {
                //The order of these entries is critical                
                this.Add(new LSEResultStatusMethylationAnalysisRequired(lseResult, this.m_AccessionOrder, orderedOnId));
                this.Add(new LSEResultStatusBRAFRequired(lseResult, this.m_AccessionOrder, orderedOnId));
                this.Add(new LSEResultStatusCompleteAfterMSI(lseResult, this.m_AccessionOrder, orderedOnId));                
            }            
        }

        public YellowstonePathology.Business.Test.LynchSyndrome.LSEResultStatus GetMatch()
        {
            YellowstonePathology.Business.Test.LynchSyndrome.LSEResultStatus result = null;
            foreach (YellowstonePathology.Business.Test.LynchSyndrome.LSEResultStatus lseResultStatus in this)
            {
                if (lseResultStatus.IsMatch() == true)
                {
                    result = lseResultStatus;
                    break;
                }
            }
            return result;
        }
    }
}
