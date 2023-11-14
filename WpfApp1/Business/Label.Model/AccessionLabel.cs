using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace YellowstonePathology.Business.Label.Model
{
    public class AccessionLabel : Label, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_PatientFirstName;
        private string m_PatientLastName;
        private string m_MasterAccessionNo;

        public AccessionLabel()
        {
        
        }

        public AccessionLabel(string masterAccessionNo)
        {            
            this.m_MasterAccessionNo = masterAccessionNo;
        }

        public AccessionLabel(string patientFirstName, string patientLastName, string masterAccessionNo)
        {                        
            this.m_PatientFirstName = patientFirstName;
            this.m_PatientLastName = patientLastName;
            this.m_MasterAccessionNo = masterAccessionNo;
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set 
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        public string PatientLastName
        {
            get { return this.m_PatientLastName; }
            set 
            {
                if (this.m_PatientLastName != value)
                {
                    this.m_PatientLastName = value;
                    this.NotifyPropertyChanged("PatientLastName");
                }
            }
        }

        public string PatientFirstName
        {
            get { return this.m_PatientFirstName; }
            set 
            {
                if (this.m_PatientFirstName != value)
                {
                    this.m_PatientFirstName = value;
                    this.NotifyPropertyChanged("PatientFirstName");
                }
            }
        }

        public override void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat masterAccessionNoStringtFormat = new StringFormat();
            masterAccessionNoStringtFormat.Alignment = StringAlignment.Near;
            masterAccessionNoStringtFormat.LineAlignment = StringAlignment.Center;

            Rectangle masterAccessionNoRectangle = new Rectangle(x + 10, y + 10, 80, 15);

            using (Font masterAccessionNofont = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(this.m_MasterAccessionNo, masterAccessionNofont, Brushes.Black, masterAccessionNoRectangle, masterAccessionNoStringtFormat);
            }            

            string patientNameText = this.TruncateString(this.m_PatientFirstName, 8)  +  Environment.NewLine + this.TruncateString(this.m_PatientLastName, 8);

            StringFormat patientNameStringtFormat = new StringFormat();
            patientNameStringtFormat.Alignment = StringAlignment.Near;
            patientNameStringtFormat.LineAlignment = StringAlignment.Center;

            Rectangle patientNameRectangle = new Rectangle(x + 10, y + 35, 80, 30);

            using (Font patientNamefont = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(patientNameText, patientNamefont, Brushes.Black, patientNameRectangle, patientNameStringtFormat);
            }

            string locationText = "YPI Blgs";

            StringFormat locationStringtFormat = new StringFormat();
            locationStringtFormat.Alignment = StringAlignment.Near;
            locationStringtFormat.LineAlignment = StringAlignment.Center;

            Rectangle locationRectangle = new Rectangle(x + 10, y + 75, 80, 15);

            using (Font locationfont = new Font("Verdana", 8, FontStyle.Regular, GraphicsUnit.Point))
            {
                e.Graphics.DrawString(locationText, locationfont, Brushes.Black, locationRectangle, locationStringtFormat);
            }            
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
