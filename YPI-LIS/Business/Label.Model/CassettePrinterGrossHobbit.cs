using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossHobbit : CassettePrinter
    {
        public CassettePrinterGrossHobbit() : base("Gross Hobbit")
        {
            this.Carousel.Columns.Add(new CarouselColumn("White", 1, 101, "White", HOBBITPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Orange", 2, 102, "#FFFFC175", HOBBITPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Yellow", 3, 103, "Yellow", HOBBITPRINTERPATH));                       
            this.Carousel.Columns.Add(new CarouselColumn("Lilac", 4, 104, "#b666d2", HOBBITPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Blue", 5, 105, "#bfefff", HOBBITPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Pink", 6, 106, "#e5a3ad", HOBBITPRINTERPATH));            
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
