using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class HistologySlideLabelDocument
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;
		private Queue<YellowstonePathology.Business.BarcodeScanning.HistologySlide> m_HistologySlideQueue;        

        public HistologySlideLabelDocument(YellowstonePathology.Business.Slide.Model.SlideOrderCollection slideOrderCollection)
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
			this.m_HistologySlideQueue = new Queue<Business.BarcodeScanning.HistologySlide>();
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();         

            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in slideOrderCollection)
            {
				YellowstonePathology.Business.BarcodeScanning.HistologySlide histologySlide = new Business.BarcodeScanning.HistologySlide(slideOrder.SlideOrderId,
                    slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestName, slideOrder.Location);
                this.m_HistologySlideQueue.Enqueue(histologySlide);
            }            
        }

        public HistologySlideLabelDocument(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
			this.m_HistologySlideQueue = new Queue<Business.BarcodeScanning.HistologySlide>();

			YellowstonePathology.Business.BarcodeScanning.HistologySlide histologySlide = new Business.BarcodeScanning.HistologySlide(slideOrder.SlideOrderId,
                    slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestName, slideOrder.Location);
            this.m_HistologySlideQueue.Enqueue(histologySlide);
        }

		public HistologySlideLabelDocument(YellowstonePathology.Business.BarcodeScanning.HistologySlide histologySlide, int labelCount)
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
			this.m_HistologySlideQueue = new Queue<Business.BarcodeScanning.HistologySlide>();
            for (int i = 0; i < labelCount; i++)
            {
                this.m_HistologySlideQueue.Enqueue(histologySlide);
            }
        }

        public void Print(System.Printing.PrintQueue printQueue)
        {
            this.m_PrintDocument.PrinterSettings.PrinterName = printQueue.FullName;
            this.m_PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            this.m_PrintDocument.Print();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int count = 1;
            int x = 3;
            //int y = 8;            

            for(int i=0; i<4; i++)
            {
				YellowstonePathology.Business.BarcodeScanning.HistologySlide histologySlide = this.m_HistologySlideQueue.Dequeue();
                //histologySlide.DrawLabel(x, y, e);
                x = x + 106;
                count += 1;
                if (this.m_HistologySlideQueue.Count == 0) break;
            }

            if (this.m_HistologySlideQueue.Count == 0)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }       
    }
}
