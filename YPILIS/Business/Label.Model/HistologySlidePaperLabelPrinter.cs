using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class HistologySlidePaperLabelPrinter : LabelPrinter
    {        
        public HistologySlidePaperLabelPrinter() 
            : base(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HistologySlideLabelPrinter)
        {
            this.m_ColumnCount = 4;
            this.m_ColumnWidth = 106;            
        }        
    }
}
