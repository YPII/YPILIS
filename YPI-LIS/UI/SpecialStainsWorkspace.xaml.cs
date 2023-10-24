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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SpecialStainsWorkspace.xaml
    /// </summary>

    /*public partial class SpecialStainsWorkspace : System.Windows.Controls.UserControl
    {
        public CommandBinding CommandBindingSave;
        private static UI.SpecialStainsWorkspace m_Instance; 
        YellowstonePathology.Business.SpecialStain.SpecialStains m_SpecialStains;

        private SpecialStainsWorkspace()
        {
            this.CommandBindingSave = new CommandBinding(ApplicationCommands.Save, SaveData, CanSaveData);
            this.m_SpecialStains = new YellowstonePathology.Business.SpecialStain.SpecialStains();

            this.DataContext = this.m_SpecialStains;

            InitializeComponent();            
        }

        public static UI.SpecialStainsWorkspace Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new UI.SpecialStainsWorkspace();
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
            this.m_SpecialStains.CurrentSpecialStain.Save();            
        }

        public void lsitViewSpecialStain_SelectionChanged(object sender, RoutedEventArgs args)
        {
            //YellowstonePathology.Business.SpecialStainsItem item = (YellowstonePathology.Business.SpecialStainsItem)this.listViewSpecialStains.SelectedItem;
            //MessageBox.Show(item.StainName);

            //YellowstonePathology.Business.SpecialStainsItem item = (YellowstonePathology.Business.SpecialStainsItem)this.m_SpecialStains.SpecialStainsView.CurrentItem;
            //MessageBox.Show(item.StainName);

            //BusinessObjects.SpecialStainItem item = (BusinessObjects.SpecialStainItem)listViewSpecialStains.SelectedItem;            
            //this.m_SpecialStains.CurrentSpecialStain = item;
        }

        public void ButtonAddGroup_Click(object sender, RoutedEventArgs args)
        {
            this.m_SpecialStains.InsertGroupName();            
        }

        public void ButtonRemoveGroup_Click(object sender, RoutedEventArgs args)
        {
            this.m_SpecialStains.CurrentSpecialStain.SpecialStainGroup.Delete(this.m_SpecialStains.CurrentSpecialStain.CurrentSpecialStainGroup);
            this.m_SpecialStains.CurrentSpecialStain.SpecialStainGroup.Fill();
        }
    }*/
}