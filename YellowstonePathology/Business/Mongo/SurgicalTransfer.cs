using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Mongo
{
    [YellowstonePathology.Business.Persistence.PersistentDocumentCollectionName("Transfer")]
    public class SurgicalTransfer : Transfer
    {
        public override void ExtendDocuments()
        {
            
        }
    }
}
