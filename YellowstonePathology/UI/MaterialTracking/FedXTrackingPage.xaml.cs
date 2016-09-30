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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net;
using System.Xml;

namespace YellowstonePathology.UI.MaterialTracking
{
	public partial class FedXTrackingPage : UserControl, INotifyPropertyChanged
	{
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public event PropertyChangedEventHandler PropertyChanged;
        private string m_TrackingResult;
        
		public FedXTrackingPage(string trackingNumber)
		{
            this.GetTrackingInfo(trackingNumber);
			InitializeComponent();
            this.DataContext = this;
            this.Loaded += MaterialTrackingStartPage_Loaded;
		}        
        
        private void MaterialTrackingStartPage_Loaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(Window.GetWindow(this));
        } 
        
        public string TrackingResult
        {
            get { return this.m_TrackingResult; }
            set { this.m_TrackingResult = value; }
        }                           
		
		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.Next != null) this.Next(this, new EventArgs());
        }

        private void GetTrackingInfo(string trackingNumber)
        {
            // authorization key: 9AKQLFGu15YA91pZ
            // meter number: 109758384
            // production password: sDY7lb5HCpaNvU2B98PZ6JoUY
            // production url https://ws.fedex.com:443/web-services

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ws.fedex.com:443/web-services");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var resourceName = "YellowstonePathology.UI.MaterialTracking.FedXTrackingRequest.json";

            using (System.IO.Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                json = json.Replace("#TRACKINGNUMBER#", trackingNumber);
                using (var streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }            

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string result = null;
            using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                this.NotifyPropertyChanged("TrackingResult");
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            StringBuilder sb = new StringBuilder();
            System.IO.TextWriter tr = new System.IO.StringWriter(sb);
            XmlTextWriter wr = new XmlTextWriter(tr);
            wr.Formatting = Formatting.Indented;
            doc.Save(wr);
            wr.Close();



            XmlNodeList nodeListActualDeliveryTimestamp = doc.GetElementsByTagName("ActualDeliveryTimestamp");
            if(nodeListActualDeliveryTimestamp.Count != 0)
            {
                this.m_TrackingResult = "Delivered: " + nodeListActualDeliveryTimestamp[0].InnerText + Environment.NewLine + Environment.NewLine;
            }

            XmlNodeList nodeListMessage = doc.GetElementsByTagName("Message");
            if (nodeListMessage.Count != 0)
            {
                this.m_TrackingResult += "Message: " + nodeListMessage[2].InnerText + Environment.NewLine + Environment.NewLine;
            }

            this.m_TrackingResult += sb.ToString();
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
