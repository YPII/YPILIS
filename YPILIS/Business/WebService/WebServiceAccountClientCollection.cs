using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.WebService
{
    public class WebServiceAccountClientCollection : ObservableCollection<WebServiceAccountClient>
    {
        public WebServiceAccountClientCollection()
        { }

        public WebServiceAccountClient Get(int webServiceAccountClientId)
        {
            WebServiceAccountClient result = this.FirstOrDefault(w => w.WebServiceAccountClientId == webServiceAccountClientId);
            return result;
        }

        public bool Exists(int clientId)
        {
            WebServiceAccountClient result = this.FirstOrDefault(w => w.ClientId == clientId);
            return result == null ? false : true;
        }
    }
}
