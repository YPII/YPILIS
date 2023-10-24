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

namespace YellowstonePathology.Business.Rules
{
    /// <summary>
    /// Interaction logic for ExecutionStatusDialog.xaml
    /// </summary>
    public partial class ExecutionStatusDialog : Window
    {
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        public ExecutionStatusDialog(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            InitializeComponent();
            this.DataContext = this.m_ExecutionStatus;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        
    }
}
