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
using System.Windows.Shapes;

namespace YellowstonePathology.Policy
{
    /// <summary>
    /// Interaction logic for EditDirectoryDialog.xaml
    /// </summary>
    public partial class EditDirectoryDialog : Window
    {
        public delegate void FinishedEventHandler(object sender, EventArgs e);
        public event FinishedEventHandler Finished;

        private Directory m_Directory;
        private Directory m_ParentDirectory;
        private bool m_IsNewDirectory;

        public EditDirectoryDialog(Directory directory, Directory parentDirectory, bool isNewDirectory)
        {
            this.m_Directory = directory;
            this.m_ParentDirectory = parentDirectory;
            this.m_IsNewDirectory = isNewDirectory;

            InitializeComponent();
            this.DataContext = this;
        }

        public Directory Directory
        {
            get { return this.m_Directory; }
        }        

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_IsNewDirectory == true)
            {
                this.m_ParentDirectory.Subdirectories.Add(this.m_Directory);
                this.Finished(this, new EventArgs());
                this.Close();
            }
        }
    }
}
