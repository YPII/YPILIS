using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterHavre : CassettePrinter
    {
        public CassettePrinterHavre() : base("Gross Havre")
        {
            this.Carousel.Columns.Add(new CarouselColumn("White", 1, 101, "White", HAVREPRINTERPATH));            
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
