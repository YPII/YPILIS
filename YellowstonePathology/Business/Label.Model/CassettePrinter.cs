using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinter
    {        
        public static string TECHPRINTERPATH = @"\\10.1.1.75\Jobs";
        public static string PATHPRINTERPATH = @"\\10.1.1.84\Jobs\";
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

        public void Print(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {                        
            List<Cassette> cassettes = new List<Cassette>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint == true)
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
