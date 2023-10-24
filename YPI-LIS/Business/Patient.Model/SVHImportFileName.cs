using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Patient.Model
{
    public class SVHImportFileName
    {
        //\\dc1\FTPData\SVBBilling\Processed\YP-SVB-2014-05-01-05-04-45.txt

        private bool m_IsSVHImportFile;
        private DateTime m_FileDate;
        private string m_FileName;
        private string m_FullPath;
        private string [] m_FileNameParts;

        public SVHImportFileName(string fullPath)
        {
            this.m_FullPath = fullPath;
            this.m_FileName = System.IO.Path.GetFileName(fullPath);
            this.m_FileNameParts = this.m_FileName.Split('-');

            if (this.m_FileNameParts.Length == 8)
            {
                this.m_FileDate = DateTime.Parse(this.m_FileNameParts[3] + "/" + this.m_FileNameParts[4] + "/" + this.m_FileNameParts[2]);
                this.m_IsSVHImportFile = true;
            }
            
        }

        public DateTime FileDate
        {
            get { return this.m_FileDate; }
        }

        public bool IsSVHImportFile
        {
            get { return this.m_IsSVHImportFile; }
        }
      
        public string FullPath
        {
            get { return this.m_FullPath; }
        }
    }
}
