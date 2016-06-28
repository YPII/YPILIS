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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Surgical
{
	/// <summary>
	/// Interaction logic for ResultPathReview.xaml
	/// </summary>
	public partial class ResultPathReview : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private PathologistUI m_PathologistUI;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.UI.Test.ResultDialog m_ResultDialog;        

		public ResultPathReview(PathologistUI pathologistUI, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PathologistUI = pathologistUI;
			this.m_SystemIdentity = systemIdentity;

			InitializeComponent();
			this.DataContext = this;
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string ResultString
		{
			get
            {
                if (this.m_PathologistUI.AccessionOrder.PanelSetOrderCollection.Exists(this.PanelSetOrder.ReportNo) == true)
                {
                    return this.m_PathologistUI.AccessionOrder.ToResultString(this.PanelSetOrder.ReportNo);
                }
                else
                {
                    return string.Empty;
                }
            }
		}

		YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PathologistUI.PanelSetOrder; }
		}

		private void ButtonResults_Click(object sender, RoutedEventArgs e)
		{
			int panelSetid = this.PanelSetOrder.PanelSetId;
            this.m_ResultDialog = new Test.ResultDialog();            

			YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
            resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(ResultPathFactory_Finished);

			bool resultPathStarted = resultPathFactory.Start(this.m_PathologistUI.PanelSetOrder, this.m_PathologistUI.AccessionOrder, this.m_ResultDialog.PageNavigator, this.m_ResultDialog, System.Windows.Visibility.Collapsed);
            if (resultPathStarted == false)
            {
                string msg = "Results not yet implemented for " + this.PanelSetOrder.PanelSetName;
                MessageBox.Show(msg, "Under Construction");
            }
            else
            {
                this.m_ResultDialog.ShowDialog();
            }
		}

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();
            this.NotifyPropertyChanged("ResultString");
        }
    }
}
