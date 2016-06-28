using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Patient.Model
{
    public class SVHImportFolder
    {
        private const string FolderPath = @"\\ypiiinterface1\FTPData\SVBBilling";
        private const string ProcessedFolderPath = @"\\ypiiinterface1\FTPData\SVBBilling\Processed";
		private List<string> m_FileList;

        public SVHImportFolder()
        {
            this.m_FileList = new List<string>();

            string[] files = System.IO.Directory.GetFiles(FolderPath);
            foreach (string file in files)
            {
                if (System.IO.Path.GetExtension(file).ToUpper() == ".TXT")
                {
                    this.m_FileList.Add(file);
                }
            }
        }

        public List<string> FileList
        {
            get { return this.m_FileList; }
        }

        public void Process(DateTime importDate, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string[] files = System.IO.Directory.GetFiles(FolderPath);
            foreach (string file in files)
            {
                SVHImportFileName svhImportFilename = new SVHImportFileName(file);
                if (svhImportFilename.IsSVHImportFile == true)
                {
					if (svhImportFilename.FileDate == importDate)
					{
						SVHImportFile svhImportFile = new SVHImportFile(svhImportFilename, systemIdentity);
						svhImportFile.ParseAndPersist();
						string destinationFileName = Path.Combine(ProcessedFolderPath, Path.GetFileName(file));
						if (File.Exists(destinationFileName) == false) File.Move(svhImportFilename.FullPath, destinationFileName);
					}
                }
            }
        }        
    }
}
