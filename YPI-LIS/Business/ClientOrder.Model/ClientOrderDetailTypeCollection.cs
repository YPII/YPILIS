using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class ClientOrderDetailTypeCollection : ObservableCollection<ClientOrderDetailType>
    {
        public ClientOrderDetailTypeCollection()
        {
            ClientOrderDetailType bpsy = new ClientOrderDetailType() { m_Description = "Biopsy/Excision", m_Code = "BPSY" };
            this.Add(bpsy);            
            ClientOrderDetailType srgcl = new ClientOrderDetailType() { m_Description = "Surgical Specimen", m_Code = "SRGCL" };
            this.Add(srgcl);
            ClientOrderDetailType fna = new ClientOrderDetailType() { m_Description = "Fine Needle Apsirate", m_Code = "FNA" };
            this.Add(fna);
            //ClientOrderDetailType tpp = new ClientOrderDetailType() { m_Description = "Thin Prep Pap", m_Code = "TPP" };
            //this.Add(tpp);
            ClientOrderDetailType poc = new ClientOrderDetailType() { m_Description = "Products of Conception", m_Code = "POC" };
            this.Add(poc);            
            ClientOrderDetailType plcnt = new ClientOrderDetailType() { m_Description = "Placenta", m_Code = "PLCNT" };
            this.Add(plcnt);            
            ClientOrderDetailType prphlbld = new ClientOrderDetailType() { m_Description = "Peripheral Blood", m_Code = "PRPHLBLD" };
            this.Add(prphlbld);            
        }
    }
}
