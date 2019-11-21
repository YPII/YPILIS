using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology
{
    /// <summary>
    /// Interaction logic for AxisCameraDialog.xaml
    /// </summary>
    public partial class AxisCameraDialog : Window
    {
        public AxisCameraDialog()
        {
            InitializeComponent();
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("Please_Show_Gross_Camera_Dialog", "{}");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("Please_Hide_Gross_Camera_Dialog", "Thank You");
        }

        private string GetMessage(Business.Test.AccessionOrder accessionOrder)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(accessionOrder.MasterAccessionNo);
            string caseDocumentPath = Business.Document.CaseDocument.GetCaseDocumentFullPath(orderIdParser);

            StringBuilder result = new StringBuilder();
            result.Append("{");
            result.Append("\"Master AccessionNo\": ");
            result.Append("\"" + accessionOrder.MasterAccessionNo + "\"");
            result.Append(",\"Patient Name\": ");
            result.Append("\"" + accessionOrder.PatientDisplayName + "\"");
            result.Append(",\"Case Document Path\": " + caseDocumentPath);            
            result.Append("}");

            return result.ToString();
        }
    }
}
