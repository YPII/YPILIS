using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ContainerPaperLabelPrinter : LabelPrinter
    {        
        public ContainerPaperLabelPrinter() 
            : base(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ContainerLabelPrinter)
        {
            this.m_ColumnCount = 4;
            this.m_ColumnWidth = 106;            
        }        
    }
}
