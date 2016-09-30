using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class IsOKToAdjustWHPExpectedFinalTimeAuditCollection : AuditCollection
    {
        public IsOKToAdjustWHPExpectedFinalTimeAuditCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.Add(new YellowstonePathology.Business.Audit.Model.HPVIsRequiredAudit(accessionOrder));
            this.Add(new YellowstonePathology.Business.Audit.Model.HPV1618IsRequiredAudit(accessionOrder));
            this.Add(new YellowstonePathology.Business.Audit.Model.NGCTIsRequiredAudit(accessionOrder));
            this.Add(new YellowstonePathology.Business.Audit.Model.TrichomonasIsRequiredAudit(accessionOrder));
            this.Add(new YellowstonePathology.Business.Audit.Model.AreAllWHPTestsFinalAudit(accessionOrder));            
        }
    }
}
