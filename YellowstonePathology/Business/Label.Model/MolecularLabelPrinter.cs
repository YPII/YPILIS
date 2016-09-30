using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class MolecularLabelPrinter : LabelPrinter
    {                
        public MolecularLabelPrinter() 
            : base(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelPrinter)
        {
            this.m_ColumnCount = 1;
        }        
    }
}
