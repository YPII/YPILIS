using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class FileNameReturnEventArgs : System.EventArgs
    {
        private string m_FileName;

        public FileNameReturnEventArgs(string fileName)
        {
            this.m_FileName = fileName;
        }

        public string FileName
        {
            get { return this.m_FileName; }
        }
    }
}
