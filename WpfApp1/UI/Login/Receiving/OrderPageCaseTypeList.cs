using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class OrderPageCaseTypeList : List<string>
    {
        public OrderPageCaseTypeList()
        {
            this.Add("All Orders");
            this.Add("ARUP");
            this.Add("Cytogenetics");
            this.Add("FISH");
            this.Add("Flow Cytometry");
            this.Add("Histology");            
            this.Add("Molecular Genetics");            
            this.Add("Neogenomics");
            this.Add("Pathologist");                        
			this.Add("Reflex Testing");
            
        }
    }
}
