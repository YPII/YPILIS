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

namespace YellowstonePathology.UI.Mongo
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        YellowstonePathology.Business.Mongo.Transfer m_Transfer;

        public TransferWindow(YellowstonePathology.Business.Mongo.Transfer transfer)
        {
            this.m_Transfer = transfer;
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Mongo.Transfer Transfer
        {
            get { return this.m_Transfer; }
        }
    }
}
