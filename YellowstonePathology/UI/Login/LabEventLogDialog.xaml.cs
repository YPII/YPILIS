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

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for ElectronicOrders.xaml
    /// </summary>
    public partial class LabEventLogDialog : Window
    {
		YellowstonePathology.Business.Domain.OrderCommentLogCollection m_OrderCommentLogCollection;

        public LabEventLogDialog(int specimenLogId)
        {
			m_OrderCommentLogCollection = YellowstonePathology.Business.Gateway.OrderCommentGateway.GetOrderCommentsForSpecimenLogId(specimenLogId);
            InitializeComponent();
            
            this.DataContext = this;
        }

        //public List<YellowstonePathology.Business.Domain.LabEventLog> OrderEventLogList
        //{
        //    get { return this.m_LabEventLogList; }
        //    set { this.m_LabEventLogList = value; }
        //}

		public YellowstonePathology.Business.Domain.OrderCommentLogCollection OrderCommentLogCollection
		{
			get { return this.m_OrderCommentLogCollection; }
			set { this.m_OrderCommentLogCollection = value; }
		}

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }        

        private void ListViewLogList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {            
            this.DialogResult = true;
            this.Close();            
        }        
    }
}
