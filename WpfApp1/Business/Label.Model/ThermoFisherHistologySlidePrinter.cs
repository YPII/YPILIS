using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThermoFisherHistologySlidePrinter : LabelPrinter
    {
        public ThermoFisherHistologySlidePrinter()
            : base(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ThermoFisherSlidePrinter)
        {
            this.m_ColumnCount = 1;
            this.m_ColumnWidth = 106;
        }
    }
}