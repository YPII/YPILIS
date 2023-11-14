using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class LabelFormatCollection : ObservableCollection<string>
    {
        public static LabelFormatCollection GetMolecularLabelCollection()
        {
            LabelFormatCollection result = new LabelFormatCollection();
            result.Add(LabelFormatEnum.DYMO.ToString());
            result.Add(LabelFormatEnum.ZEBRA.ToString());
            return result;
        }
    }
}
