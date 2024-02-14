using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class MolecularLabelZebra : Label
    {
        private string m_MasterAccessionNo;
        private string m_PatientFirstName;
        private string m_PatientLastName;
        private string m_SpecimenDescription;
        private YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;

        public MolecularLabelZebra(string masterAccessionNo, string patientFirstName, string patientLastName, string specimenDescription, YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {            
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_SpecimenDescription = specimenDescription;
            this.m_PanelSet = panelSet;
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            e.Graphics.DrawString(this.m_MasterAccessionNo, new System.Drawing.Font("Verdana", 2), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 5, y + 8));
            e.Graphics.DrawString(this.m_PatientLastName, new System.Drawing.Font("Verdana", 2), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 5, y + 18));            

            e.Graphics.DrawString(this.m_MasterAccessionNo, new System.Drawing.Font("Verdana", 4), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 55, y + 8));
            e.Graphics.DrawString(this.m_PatientLastName, new System.Drawing.Font("Verdana", 4), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 56, y + 18));
            e.Graphics.DrawString(this.m_SpecimenDescription, new System.Drawing.Font("Verdana", 4), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 56, y + 28));            
        }        
    }
}
