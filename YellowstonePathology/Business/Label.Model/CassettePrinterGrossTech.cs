﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinterGrossTech : CassettePrinter
    {
        //\\10.1.1.84\Jobs\

        public CassettePrinterGrossTech() : base("Gross Tech")
        {
            this.Carousel.Columns.Add(new CarouselColumn("White", 1, 101, "White", TECHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Pink", 2, 102, "#FF33D4", TECHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Yellow", 3, 103, "Yellow", TECHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Lilac", 4, 104, "#b666d2", TECHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Blue", 5, 105, "#bfefff", TECHPRINTERPATH));
            this.Carousel.Columns.Add(new CarouselColumn("Pink", 6, 106, "#e5a3ad", TECHPRINTERPATH));
        }

        public override Cassette GetCassette()
        {
            return new GeneralDataCassette();
        }
    }
}
