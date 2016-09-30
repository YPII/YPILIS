using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for RulesBrowser.xaml
    /// </summary>
    public partial class RulesBrowser : Window
    {
        YellowstonePathology.Business.Rules.RuleBrowserUI m_RuleBrowserUI;

        public RulesBrowser()
        {
            this.m_RuleBrowserUI = new YellowstonePathology.Business.Rules.RuleBrowserUI();            

            InitializeComponent();

            this.DataContext = this.m_RuleBrowserUI;            
        }

        public void ComboBoxFolderName_SelectionChanged(object sender, RoutedEventArgs args)
        {            
            YellowstonePathology.Business.Rules.RulesNameSpaceItem rulesNameSpaceItem = (YellowstonePathology.Business.Rules.RulesNameSpaceItem)this.ComboBoxFolderName.SelectedItem;
            this.m_RuleBrowserUI.RulesClassList.Fill(rulesNameSpaceItem.NameSpace);
        }

        public void MenuItemEditRule_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Rules.RulesClassItem rulesClassItem = (YellowstonePathology.Business.Rules.RulesClassItem)this.listViewRules.SelectedItem;
            YellowstonePathology.Business.Rules.BaseRules baseRules = YellowstonePathology.Business.Rules.BaseRules.CreateRuleSetInstance(rulesClassItem.ClassType);
            baseRules.EditRuleSet();
        }

        public void MenuItemCreateRulesFile_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Rules.RulesClassItem rulesClassItem = (YellowstonePathology.Business.Rules.RulesClassItem)this.listViewRules.SelectedItem;
            YellowstonePathology.Business.Rules.BaseRules.CreateRuleSetFile(rulesClassItem.ClassType);
            System.Windows.MessageBox.Show("The xml Rule File has been created.");
        }
    }
}
