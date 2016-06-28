using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class DocumentBuilder
    {
        
        public virtual object BuildNew()
        {
            throw new Exception("Not implemented here.");
        }

        public virtual void Refresh(object o)
        {
            throw new Exception("Not implemented here.");
        }
    }
}
