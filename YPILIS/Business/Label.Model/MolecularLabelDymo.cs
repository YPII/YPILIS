using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace YellowstonePathology.Business.Label.Model
{
    public class MolecularLabelDymo : Label
    {        
        private string m_MasterAccessionNo;
        private string m_PatientFirstName;
        private string m_PatientLastName;
        private string m_SpecimenDescription;        
        private YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        private bool m_IncludeTestAbbreviation;

        public MolecularLabelDymo(string masterAccessionNo, string patientFirstName, string patientLastName, string specimenDescription, 
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, bool includeTestAbbreviation)
        {            
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_SpecimenDescription = specimenDescription;
            this.m_PanelSet = panelSet;
            this.m_IncludeTestAbbreviation = includeTestAbbreviation;
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string largeRectangleString = this.TruncateString(this.m_SpecimenDescription, 150) + Environment.NewLine;

            if (this.m_PanelSet != null)
            {
                largeRectangleString = this.TruncateString(this.m_PanelSet.Abbreviation, 150) + Environment.NewLine +
                    this.TruncateString(this.m_PatientLastName + ", " + this.m_PatientFirstName.Substring(0, 1), 150) + Environment.NewLine +
                    this.TruncateString(this.m_MasterAccessionNo, 150);
            }
            else
            {
                largeRectangleString = this.TruncateString(this.m_PatientLastName + ", " + this.m_PatientFirstName.Substring(0, 1), 150) + Environment.NewLine +
                    this.TruncateString(this.m_MasterAccessionNo, 150);
            }

            StringFormat stringFormatLargeRectangle = new StringFormat();
            stringFormatLargeRectangle.Alignment = StringAlignment.Far;
            stringFormatLargeRectangle.LineAlignment = StringAlignment.Center;
            stringFormatLargeRectangle.FormatFlags = StringFormatFlags.DirectionVertical;

            Rectangle leftRectangle = new Rectangle(33, 0, 45, 150);                        

            using (Font font = new Font("Verdana", 6, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(largeRectangleString, font, Brushes.Black, leftRectangle, stringFormatLargeRectangle);                
            }

            StringFormat stringFormatCircle = new StringFormat();
            stringFormatCircle.Alignment = StringAlignment.Center;
            stringFormatCircle.LineAlignment = StringAlignment.Center;
            stringFormatCircle.FormatFlags = StringFormatFlags.DirectionVertical;

            Rectangle circleRectangle = new Rectangle(35, 163, 40, 40);

            string circleText = string.Empty;

            if (this.m_IncludeTestAbbreviation == true)
            {
                circleText = this.m_PanelSet.Abbreviation + Environment.NewLine;
            }

            circleText += this.m_MasterAccessionNo + Environment.NewLine +
                this.TruncateString(this.m_PatientLastName, 8);
                

            using (Font font = new Font("Verdana", 4, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(circleText, font, Brushes.Black, circleRectangle, stringFormatCircle);                
            }            
        }        
    }
}
