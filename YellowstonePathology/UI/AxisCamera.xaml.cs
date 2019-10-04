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
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AxisCamera.xaml
    /// </summary>
    public partial class AxisCamera : Window
    {
        private static string AXISRootURL = "http://10.1.1.200/axis-cgi/com/";
        private int m_X = 0;
        private CredentialCache m_CrentialCache;

        public AxisCamera()
        {
            this.m_CrentialCache = new CredentialCache();
            this.m_CrentialCache.Add(new Uri(AXISRootURL), "Digest", new NetworkCredential("ypii", "ypii"));

            InitializeComponent();
        }

        public void SendCommand(string url)
        {
            using (var clientHander = new HttpClientHandler
            {
                Credentials = this.m_CrentialCache,
                PreAuthenticate = true
            })

            using (var httpClient = new HttpClient(clientHander))
            {                
                var responseTask = httpClient.GetAsync(url);
                responseTask.Result.EnsureSuccessStatusCode();                
            }
        }

        public int X
        {
            get { return this.m_X; }
            set { this.m_X = value; }
        }

        private void ButtonMove_Click(object sender, RoutedEventArgs e)
        {
            this.m_X += 10;
            string command = AXISRootURL + "ptz.cgi?pan=" + this.m_X;
            SendCommand(command);            
        }
    }
}
