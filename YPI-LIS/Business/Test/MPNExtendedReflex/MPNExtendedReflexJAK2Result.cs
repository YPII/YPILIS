using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
    public class MPNExtendedReflexJAK2Result : YellowstonePathology.Business.Audit.Model.Audit
    {
        public MPNExtendedReflexJAK2Result(YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder panelSetOrderJAK2V617F)
        {
            if (panelSetOrderJAK2V617F.Final == true)
            {
                this.m_Message = new StringBuilder(panelSetOrderJAK2V617F.Result);
            }
            else
            {
                this.m_Message = new StringBuilder(MPNExtendedReflexResult.PendingResult);
            }
        }

        public string Result
        {
            get { return this.m_Message.ToString(); }
        }
    }
}
