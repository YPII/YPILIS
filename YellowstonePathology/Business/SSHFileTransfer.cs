using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;

namespace YellowstonePathology.Business
{
    public class SSHFileTransfer
    {
        public delegate void StatusMessageEventHandler(object sender, string message, int count);
        public event StatusMessageEventHandler StatusMessage;        

        public delegate void FailedEventHandler(object sender, string message);
        public event FailedEventHandler Failed;

        private const string SSH_KEY_PATH = @"C:\Program Files\Yellowstone Pathology Institute\ssh.key";

        private string m_Host;
        private int m_Port;
        private string m_UserName;
        private string m_Password;
             
        public SSHFileTransfer(string host, int port, string userName, string password)
        {
            this.m_Host = host;
            this.m_Port = port;
            this.m_UserName = userName;
            this.m_Password = password;
        }

        public void UploadFilesToPSA(string[] files)
        {
            int count = 1;
            var keyFile = new PrivateKeyFile(SSH_KEY_PATH);
            if (File.Exists(SSH_KEY_PATH) == false)
            {                
                throw new Exception("SSH Key File Not Found.");
            }

            var keyFiles = new [] { keyFile };
            var username = this.m_UserName;

            var methods = new List<AuthenticationMethod>();
            methods.Add(new PasswordAuthenticationMethod(username, this.m_Password));
            methods.Add(new PrivateKeyAuthenticationMethod(username, keyFiles));

            var con = new ConnectionInfo(this.m_Host, this.m_Port, username, methods.ToArray());
            using (var client = new SftpClient(con))
            {                
                try
                {
                    this.SendStatusMessage("Connecting to PSA SSH.", count);                    
                    client.Connect();
                    foreach (string file in files)
                    {
                        using (var fs = new FileStream(file, FileMode.Open))
                        {
                            count = count + 1;
                            this.SendStatusMessage("Uploading File: " + file, count);
                            string fileNameOnly = Path.GetFileName(file);                            
                            client.UploadFile(fs, fileNameOnly);
                            fs.Close();
                        }
                    }

                    client.Disconnect();
                    this.SendStatusMessage("Disconnecting from PSA SSH.", count);
                    this.SendStatusMessage("Finished transferring files.", count);
                }   
                catch(Exception connectException)
                {
                    this.SendStatusMessage("Failed transferring files.", count);
                    this.Failed(this, connectException.Message);                    
                }                                             
            }
        }

        private void SendStatusMessage(string message, int count)
        {
            if(this.StatusMessage != null)
            {
                this.StatusMessage(this, message, count);
            }
        }
    }
}
