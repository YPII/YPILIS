using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientCollection : ObservableCollection<PhysicianClient>
    {
        public PhysicianClientCollection()
        {     
            
        }        
    }    
}
