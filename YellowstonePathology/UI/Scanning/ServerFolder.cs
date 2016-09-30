using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Scanning
{
    public class ServerFolder
    {
        private string m_Path;
        private string m_Description;

        public ServerFolder(string path, string description)
        {
            this.m_Path = path;
            this.m_Description = description;
        }

        public string Path
        {
            get { return this.m_Path; }
            set { this.m_Path = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}
