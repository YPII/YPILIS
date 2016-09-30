using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Scanning
{
    public class ServerFolderCollection : Collection<ServerFolder>
    {
        public ServerFolderCollection()
        {
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Cody", "Cody"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder1", "Folder1"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder2", "Folder2"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder3", "Folder3"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder4", "Folder4"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder5", "Folder5"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder6", "Folder6"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Folder7", "Folder7"));
            this.Add(new ServerFolder(@"\\cfileserver\documents\Scanning\Histo", "Histo"));
        }
    }
}
