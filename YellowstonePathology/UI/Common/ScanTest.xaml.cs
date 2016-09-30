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

namespace YellowstonePathology.UI.Common
{
	/// <summary>
	/// Interaction logic for ScanTest.xaml
	/// </summary>
	public partial class ScanTest : Window
	{        
		//private YellowstonePathology.Business.SpecimenTracking.TestScan m_TestScan;

		public ScanTest()
		{
			//m_TestScan = new YellowstonePathology.Business.SpecimenTracking.TestScan();
			InitializeComponent();
			//this.DataContext = m_TestScan;
		}

		private void ButtonTest_Click(object sender, RoutedEventArgs e)
		{
			//this.m_TestScan.NotifyPropertyChanged("Message");
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			//this.m_TestScan.ResetScanner();
			//Close();
		}                
    }
}
