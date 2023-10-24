using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class PAPSlideLabelPrinter : LabelPrinter
    {
        public PAPSlideLabelPrinter() 
            : base(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.CytologySlideLabelPrinter)
        {
            this.m_ColumnCount = 2;
            this.m_ColumnWidth = 106;
        }        
    }
}
