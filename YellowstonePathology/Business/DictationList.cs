using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business
{
    public class DictationList : FileList
    {
        DictationLocationEnum m_DictationLocation;

        public DictationList(DictationLocationEnum dictationLocation)
        {
            this.m_DictationLocation = dictationLocation;
            this.LoadDirectory();
        }        

        public override void Refresh()
        {
            this.LoadDirectory();
        }

        private void LoadDirectory()
        {
            this.ClearItems();
            switch (this.m_DictationLocation)
            {
                case DictationLocationEnum.Local:
                    this.LoadDirectory(YellowstonePathology.Properties.Settings.Default.LocalDictationFolder);
                    break;
                case DictationLocationEnum.Server:
                    foreach (DirectoryInfo d in new DirectoryInfo(YellowstonePathology.Properties.Settings.Default.ServerDictationFolder).GetDirectories())
                    {
                        base.LoadDirectory(d.FullName);
                    }
                    break;
            }            
        }       

        public void MoveServerFileToLocal(FileListItem item)
        {
            string [] slashSplit = item.FullPath.Split('\\');
            string copyDestination = item.FullPath.Replace(slashSplit[slashSplit.Length - 1], @"Done\" + Guid.NewGuid() + ".dct");
            if (File.Exists(item.FullPath) == true && File.Exists(copyDestination) == false)
            {
                File.Copy(item.FullPath, copyDestination);
            }
            string moveDestination = YellowstonePathology.Properties.Settings.Default.LocalDictationFolder + slashSplit[slashSplit.Length - 1];
			if (File.Exists(item.FullPath) == true && File.Exists(moveDestination) == false)
            {
                File.Move(item.FullPath, moveDestination);
            }
        }

        public void MoveLocalFileToDone(FileListItem item)
        {
			string[] slashSplit = item.FullPath.Split('\\');
			string copyDestination = item.FullPath.Replace(slashSplit[slashSplit.Length - 1], @"Done\" + Guid.NewGuid() + ".dct");
			File.Move(item.FullPath, copyDestination);
        }        
    }    
}
