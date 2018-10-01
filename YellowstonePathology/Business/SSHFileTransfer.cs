﻿using System;
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
            var keyFile = new PrivateKeyFile(SSH_KEY_PATH);
            var keyFiles = new [] { keyFile };
            var username = this.m_UserName;

            var methods = new List<AuthenticationMethod>();
            methods.Add(new PasswordAuthenticationMethod(username, this.m_Password));
            methods.Add(new PrivateKeyAuthenticationMethod(username, keyFiles));

            var con = new ConnectionInfo(this.m_Host, this.m_Port, username, methods.ToArray());
            using (var client = new SftpClient(con))
            {
                client.Connect();                                                
                foreach(string file in files)
                {
                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        client.UploadFile(fs, file);
                        fs.Close();
                    }
                }                
                client.Disconnect();
            }
        }
    }
}
