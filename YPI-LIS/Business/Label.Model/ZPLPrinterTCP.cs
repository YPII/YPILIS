using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class ZPLPrinterTCP
    {
        private int m_ErrorCount;
        private string m_IPAddress;
        private int m_Port;        

        public ZPLPrinterTCP(string ipAddress)
        {            
            this.m_IPAddress = ipAddress;
            this.m_Port = 9100;
        }

        public static string DecodeZPLFromBase64(string encodedString)
        {
            byte[] bytes = Convert.FromBase64String(encodedString);
            string result = System.Text.Encoding.Default.GetString(bytes);
            return result;
        }

        public void Print(string zplCommands)
        {
            this.m_ErrorCount = 0;
            this.TryPrint(zplCommands);
        }

        public void TryPrint(string zplCommands)
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
                this.m_ErrorCount += 1;
                if(this.m_ErrorCount < 10)
                {
                    System.Threading.Thread.Sleep(5000);
                    this.TryPrint(zplCommands);
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}
