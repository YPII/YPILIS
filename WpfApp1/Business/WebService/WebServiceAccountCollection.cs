using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.WebService
{
    public class WebServiceAccountCollection : ObservableCollection<WebServiceAccount>
    {
        public WebServiceAccountCollection()
        { }

        public bool ExistsByUserName(string userName)
        {
            WebServiceAccount webServiceAccount = this.FirstOrDefault(w => w.UserName == userName);
            return webServiceAccount != null ? true : false;
        }
    }
}
