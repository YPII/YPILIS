using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterCollection : ObservableCollection<CassettePrinter>
    {
        public CassettePrinterCollection()
        {
            this.Add(new CassettePrinterGrossTech());
            this.Add(new CassettePrinterGrossPath());
            this.Add(new CassettePrinterGrossHobbit());
        }

        public CassettePrinter GetPrinter(string name)
        {
            CassettePrinter result = null;
            foreach(CassettePrinter printer in this)
            {
                if(printer.Name == name)
                {
                    result = printer;
                    break;
                }
            }
            return result;
        }
    }
}
