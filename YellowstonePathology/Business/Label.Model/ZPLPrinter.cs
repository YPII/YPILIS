using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ZPLPrinter
    {
        private string m_IPAddress;
        private int m_Port;        

        public ZPLPrinter(string ipAddress)
        {
            this.m_IPAddress = ipAddress;
            this.m_Port = 9100;
        }        

        public void Print(string zplCommands)
        {            
            try
            {
                // Open connection
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client.Connect(this.m_IPAddress, this.m_Port);

                // Write ZPL String to connection
                System.IO.StreamWriter writer = new System.IO.StreamWriter(client.GetStream());
                writer.Write(zplCommands);
                writer.Flush();

                // Close Connection
                writer.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
