using System;
using System.Windows;
using System.ComponentModel;
using System.Data;

using MySql.Data.MySqlClient;

namespace YellowstonePathology.UI.Surgical
{
	public partial class SurgicalRescreenDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Surgical.SurgicalRescreenItemCollection m_SurgicalRescreenItemCollection;
        private DateTime m_WorkDate;

		public SurgicalRescreenDialog()
		{
            this.m_WorkDate = DateTime.Today;
			this.m_SurgicalRescreenItemCollection = new Business.Surgical.SurgicalRescreenItemCollection();
			InitializeComponent();
			this.DataContext = this;
		}

		public DateTime WorkDate
		{
            get { return this.m_WorkDate; }
            set
			{
				this.m_WorkDate = value;
				this.NotifyPropertyChanged("WorkDate");
			}
		}

		public YellowstonePathology.Business.Surgical.SurgicalRescreenItemCollection SurgicalRescreenItemCollection
		{
			get { return this.m_SurgicalRescreenItemCollection; }
			set
			{
				this.m_SurgicalRescreenItemCollection = value;
				this.NotifyPropertyChanged("SurgicalRescreenItemCollection");
			}
		}

		private void SurgicalRescreenDialog_Loaded(object sender, RoutedEventArgs e)
		{
			this.GetSurgicalRescreenItemCollection();
		}

		private void GetSurgicalRescreenItemCollection()
		{
			SurgicalRescreenItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalRescreenItemCollectionByDate(this.m_WorkDate);
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            WorkDate = WorkDate.AddDays(-1);
			this.GetSurgicalRescreenItemCollection();
		}

		private void ButtonForward_Click(object sender, RoutedEventArgs e)
		{
			WorkDate = WorkDate.AddDays(1);
			this.GetSurgicalRescreenItemCollection();
		}

		private void ButtonRequired_Click(object sender, RoutedEventArgs e)
		{
			this.SetRescreenStatus("Required");
		}

		private void ButtonNotRequired_Click(object sender, RoutedEventArgs e)
		{
			this.SetRescreenStatus("Not Required");
		}

		private void ButtonComplete_Click(object sender, RoutedEventArgs e)
		{
			this.SetRescreenStatus("Complete");
		}

		private void SetRescreenStatus(string rescreenStatus)
		{
			if (this.listViewSurgicalRescreenCases.SelectedItem != null)
			{
				foreach (YellowstonePathology.Business.Surgical.SurgicalRescreenItem item in this.listViewSurgicalRescreenCases.SelectedItems)
				{
					string specimenOrderId = item.SpecimenOrderId;

					MySqlCommand cmd = new MySqlCommand();
					cmd.CommandText = "Update tblSurgicalSpecimen set RescreenStatus = @RescreenStatus where SpecimenOrderId = @SpecimenOrderId";
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add("@RescreenStatus", SqlDbType.VarChar).Value = rescreenStatus;
					cmd.Parameters.Add("@SpecimenOrderId", SqlDbType.VarChar).Value = specimenOrderId;
					using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
					{
						cn.Open();
						cmd.Connection = cn;
						cmd.ExecuteNonQuery();
					}
				}

				this.GetSurgicalRescreenItemCollection();
			}
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
