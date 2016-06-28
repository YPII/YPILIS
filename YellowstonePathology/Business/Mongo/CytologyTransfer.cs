using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Mongo
{
    [YellowstonePathology.Business.Persistence.PersistentDocumentCollectionName("Transfer")]
    public class CytologyTransfer : Transfer
    {
        public override void ExtendDocuments()
        {            
            YellowstonePathology.Business.Mongo.Gateway.ExtendCytologyDocuments();            
        }
    }
}
