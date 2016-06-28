using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class ContainerPaperLabel : Label
    {
		private YellowstonePathology.Business.BarcodeScanning.ContainerBarcode m_ContainerBarCode;        
        private string m_GUIDFirstLine;
        private string m_GUIDSecondLine;
        private string m_GUIDThirdLine;

		public ContainerPaperLabel(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.m_ContainerBarCode = containerBarcode;
            this.m_GUIDFirstLine = containerBarcode.ToString().Substring(0, 14);
            this.m_GUIDSecondLine = containerBarcode.ToString().Substring(14, 14);
            this.m_GUIDThirdLine = containerBarcode.ToString().Substring(28);
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("YPI", new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 26, y = 4));

            DataMatrix.DmtxImageEncoder encoder = new DataMatrix.DmtxImageEncoder();
            DataMatrix.DmtxImageEncoderOptions options = new DataMatrix.DmtxImageEncoderOptions();
            options.ModuleSize = 1;
            options.MarginSize = 3;
            options.BackColor = System.Drawing.Color.White;
            options.ForeColor = System.Drawing.Color.Black;            

            e.Graphics.DrawImage(encoder.EncodeImage(this.m_ContainerBarCode.ToString(), options), new System.Drawing.Point(x + 29, y + 17));
            e.Graphics.DrawString(this.m_GUIDFirstLine, new System.Drawing.Font("Tahoma", 5), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 18, y + 50));
            e.Graphics.DrawString(this.m_GUIDSecondLine, new System.Drawing.Font("Tahoma", 5), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 18, y + 60));
            e.Graphics.DrawString(this.m_GUIDThirdLine, new System.Drawing.Font("Tahoma", 7), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 12, y + 70));
        }        
    }
}
