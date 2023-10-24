using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.ReportDistribution
{
    /// <summary>
    /// Interaction logic for EPICResultDialog.xaml
    /// </summary>
    public partial class Hl7ResultDialog : Window
    {             
        XElement m_Document;        
        string m_DocumentText;

        public Hl7ResultDialog(XElement document)
        {            
            InitializeComponent();
                        
            this.m_Document = document;
            this.m_DocumentText = ParseDocument(this.m_Document);            

            this.DataContext = this;
        }

        private string ParseDocument(XElement document)
        {
            StringBuilder result = new StringBuilder();
            IEnumerable<XElement> obxSegments = document.Elements("OBX");
            foreach (XElement obxSegment in obxSegments)
            {
                XElement obx5 = obxSegment.Element("OBX.5");
                XElement obx51 = obx5.Element("OBX.5.1");
                if (obx51 != null)
                {
                    result.AppendLine(obx51.Value);
                }
                else
                {
                    result.AppendLine();
                }                
            }

            IEnumerable<XElement> nteSegments = document.Elements("NTE");
            foreach (XElement nteSegment in nteSegments)
            {
                XElement nte3 = nteSegment.Element("NTE.3");                                
                if (nte3 != null)
                {
                    XElement nte31 = nte3.Element("NTE.3.1");
                    result.AppendLine(nte31.Value);
                }
                else
                {
                    result.AppendLine();
                }
            }

            return result.ToString();
        }        

        public string DocumentText
        {
            get { return this.m_DocumentText; }
        }
    }
}
