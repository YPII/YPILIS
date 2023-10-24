using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHResultCollection : ObservableCollection<ALKForNSCLCByFISHResult>
    {
        public ALKForNSCLCByFISHResultCollection()
        {
            this.Add(new ALKForNSCLCByFISHNoResult());            
            this.Add(new ALKForNSCLCByFISHNegativeResult());
            this.Add(new ALKForNSCLCByFISHNegativeWithPolysomyResult());
            this.Add(new ALKForNSCLCByFISHNegativeWithMonosomyResult());
            this.Add(new ALKForNSCLCByFISHPositiveResult());
            this.Add(new ALKForNSCLCByFISHNotInterpretableResult());
            this.Add(new ALKForNSCLCByFISHQNSResult());
        }

        public ALKForNSCLCByFISHResult GetByResultCode(string resultCode)
        {
            ALKForNSCLCByFISHResult result = null;
            foreach(ALKForNSCLCByFISHResult alkForNSCLCByFISHResult in this)
            {
                if (alkForNSCLCByFISHResult.ResultCode == resultCode)
                {
                    result = alkForNSCLCByFISHResult;
                    break;
                }
            }
            return result;
        }
    }
}
