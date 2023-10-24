using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossPath : CassettePrinter
    {
        public CassettePrinterGrossPath() : base("Gross Path")
        {
            this.Carousel.Columns.Add(new CarouselColumn("White", 1, 101, "White", PATHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Orange", 2, 102, "#FFFFC175", PATHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Yellow", 3, 103, "Yellow", PATHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Lilac", 4, 104, "#b666d2", PATHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Blue", 5, 105, "#bfefff", PATHPRINTERPATH));
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
