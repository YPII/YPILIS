using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThinPrepSlidePrinter : LabelPrinter
    {
        public ThinPrepSlidePrinter() 
            : base(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.CytologySlidePrinter)
        {
            this.m_ColumnCount = 1;
            this.m_ColumnWidth = 106;
        }        
    }
}
