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
    /// Interaction logic for RuleExecutionDialog.xaml
    /// </summary>
    public partial class RuleExecutionStatusDialog : Window
    {
        YellowstonePathology.Business.Rules.RuleExecutionStatus m_RuleExecutionStatus;

        public RuleExecutionStatusDialog(YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus)
        {
            this.m_RuleExecutionStatus = ruleExecutionStatus;
            InitializeComponent();
            this.DataContext = this.m_RuleExecutionStatus;
        }        
    }
}
