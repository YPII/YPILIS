using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ReferenceLabTestingWorkspace.xaml
    /// </summary>
    public partial class ReferenceLabTestingWorkspace : UserControl
    {
        private string m_DocumentPath = @"\\ypiiinterface2\ChannelData\Incoming\Neogenomics";
        private List<System.IO.FileInfo> m_FileInfoList;

        public ReferenceLabTestingWorkspace()
        {
            this.m_FileInfoList = new List<System.IO.FileInfo>();
            string[] files = System.IO.Directory.GetFiles(this.m_DocumentPath);
            foreach (string file in files)
                this.m_FileInfoList.Add(new System.IO.FileInfo(file));
            InitializeComponent();
            this.DataContext = this;                   
        }

        public List<System.IO.FileInfo> FileInfoList
        {
            get { return this.m_FileInfoList; }
        }
    }
}
