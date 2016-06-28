using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Flow
{
    /// <summary>
    /// Interaction logic for MarkerPanelWorkspace.xaml
    /// </summary>

    public partial class MarkerPanelWorkspace : System.Windows.Controls.UserControl
    {
        private static UI.Flow.MarkerPanelWorkspace m_Instance;

        public CommandBinding CommandBindingSave;

        private MarkerPanelWorkspace()
        {
            this.CommandBindingSave = new CommandBinding(ApplicationCommands.Save, SaveData, CanSaveData);
            InitializeComponent();
        }

        public static UI.Flow.MarkerPanelWorkspace Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new MarkerPanelWorkspace();
                }
                return m_Instance;
            }
        }

        private void CanSaveData(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SaveData(object target, ExecutedRoutedEventArgs args)
        {

        }
    }
}