using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinter
    {
        public static string HAVREPRINTERPATH = @"\\10.88.88.50\Jobs";
        public static string TECHPRINTERPATH = @"\\10.1.1.84\Jobs";
        public static string PATHPRINTERPATH = @"\\10.1.1.75\Jobs\";
        public static string HOBBITPRINTERPATH = @"\\gross-hobbit\Jobs";
        public static string CODYPRINTERPATH = @"\\10.33.33.57\CassettePrinter\";

        private string m_Name;        
        private Carousel m_Carousel;

        public CassettePrinter(string name)
        {
            this.m_Name = name;            
            this.m_Carousel = new Carousel();
        }            

        public string Name
        {
            get { return this.m_Name; }
        }        

        public Carousel Carousel
        {
            get { return this.m_Carousel; }
        }

        public virtual Cassette GetCassette()
        {
            throw new Exception("Not Implemented Here.");
        }

        public void Print(ProstateBiopsyKitCollection prostateBiopsyKitCollection)
        {
            CarouselColumn firstColumn = this.m_Carousel.GetColumn(prostateBiopsyKitCollection[0].CassetteColor);
            //string fileName = System.IO.Path.Combine(firstColumn.PrinterPath, $"prostate_biopsy_kit_{MongoDB.Bson.ObjectId.GenerateNewId().ToString()}.{prostateBiopsyKitCollection[0].GetFileExtension()}");
            string fileName = System.IO.Path.Combine(@"c:\testing", $"prostate_biopsy_kit_{MongoDB.Bson.ObjectId.GenerateNewId().ToString()}{prostateBiopsyKitCollection[0].GetFileExtension()}");

            //try
            //{
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    foreach (GeneralDataCassette cassette in prostateBiopsyKitCollection)
                    {
                        CarouselColumn column = this.m_Carousel.GetColumn(cassette.CassetteColor);
                        string line = cassette.GetLine(column.PrinterCode);
                        file.Write(line + "\r\n");
                    }
                }
            //}
            //catch (Exception e)
            //{
                //System.Windows.MessageBox.Show(fileName + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            //}                        
        }

        public void Print(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {                        
            List<Cassette> cassettes = new List<Cassette>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == Business.Specimen.Model.AliquotLabelType.DirectPrint == true)
                    {
                        Cassette cassette = this.GetCassette();
                        cassette.FromAliquotOrder(aliquotOrder, accessionOrder);

                        CarouselColumn column = this.m_Carousel.GetColumn(accessionOrder.CassetteColor);
                        string line = cassette.GetLine(column.PrinterCode);                        
                        string fileName = System.IO.Path.Combine(column.PrinterPath, MongoDB.Bson.ObjectId.GenerateNewId().ToString() + "." + aliquotOrder.AliquotOrderId + cassette.GetFileExtension());

                        try
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                            {
                                file.Write(line + "\r\n");
                                aliquotOrder.Printed = true;
                            }
                        }
                        catch (Exception e)
                        {
                            System.Windows.MessageBox.Show(fileName + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
                        }                                          
                    }
                }
            }                       
        }        
    }
}
