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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	/// <summary>
	/// Interaction logic for CytologyClinicalHistoryPage.xaml
	/// </summary>
	public partial class CytologyClinicalHistoryPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder m_CytologyClientOrder;
		private string m_PageHeaderText = "Cytology Clinical History";

		public CytologyClinicalHistoryPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_CytologyClientOrder = (YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder)
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(this.m_AccessionOrder.ClientOrderId, Window.GetWindow(this));

			InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(CytologyClinicalHistoryPage_Loaded);
            Unloaded += CytologyClinicalHistoryPage_Unloaded;
		}

        private void CytologyClinicalHistoryPage_Loaded(object sender, RoutedEventArgs e)
		{
             
        }

        private void CytologyClinicalHistoryPage_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.ClientOrder.Model.CytologyClientOrder CytologyClientOrder
		{
			get { return this.m_CytologyClientOrder; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
		}

		private void ButtonClinicalHistory_Click(object sender, RoutedEventArgs e)
		{
			this.TextBoxClinicalHistory.Text = this.BuildClinicalHistory();
		}

		private string BuildClinicalHistory()
		{
			StringBuilder result = new StringBuilder();
			if (string.IsNullOrEmpty(this.m_CytologyClientOrder.LMP) == false)
			{
				result.Append("LMP " + this.m_CytologyClientOrder.LMP + "; ");
			}
			if (this.m_CytologyClientOrder.Hysterectomy == true)
			{
				result.Append("Hysterectomy; ");
			}
			if (this.m_CytologyClientOrder.CervixPresent == true)
			{
				result.Append("Cervix Present; ");
			}
			if (this.m_CytologyClientOrder.AbnormalBleeding == true)
			{
				result.Append("Abnormal Bleeding; ");
			}
			if (this.m_CytologyClientOrder.BirthControl == true)
			{
				result.Append("Birth Control; ");
			}
			if (this.m_CytologyClientOrder.HormoneTherapy == true)
			{
				result.Append("Hormone Therapy; ");
			}
			if (this.m_CytologyClientOrder.PreviousNormalPap == true)
			{
				result.Append("Previous Normal Pap");
				if (string.IsNullOrEmpty(this.m_CytologyClientOrder.PreviousNormalPapDate) == false)
				{
					result.Append(" - Date " + this.m_CytologyClientOrder.PreviousNormalPapDate + "; ");
				}
				result.Append("; ");
			}
			else if (string.IsNullOrEmpty(this.m_CytologyClientOrder.PreviousNormalPapDate) == false)
			{
				result.Append("Previous Normal Pap Date " + this.m_CytologyClientOrder.PreviousNormalPapDate + "; ");
			}
			if (this.m_CytologyClientOrder.PreviousAbnormalPap == true)
			{
				result.Append("Previous Abnormal Pap");
				if (string.IsNullOrEmpty(this.m_CytologyClientOrder.PreviousAbnormalPapDate) == false)
				{
					result.Append(" - Date " + this.m_CytologyClientOrder.PreviousAbnormalPapDate);
				}
				result.Append("; ");
			}
			else if (string.IsNullOrEmpty(this.m_CytologyClientOrder.PreviousAbnormalPapDate) == false)
			{
					result.Append("Previous Abnormal Pap Date " + this.m_CytologyClientOrder.PreviousAbnormalPapDate + "; ");
			}
			if (this.m_CytologyClientOrder.PreviousBiopsy == true)
			{
				result.Append("Previous Biopsy");
				if (string.IsNullOrEmpty(this.m_CytologyClientOrder.PreviousBiopsyDate) == false)
				{
					result.Append(" - Date " + this.m_CytologyClientOrder.PreviousBiopsyDate);
				}
				result.Append("; ");
			}
			else if (string.IsNullOrEmpty(this.m_CytologyClientOrder.PreviousBiopsyDate) == false)
			{
				result.Append("Previous Biopsy Date " + this.m_CytologyClientOrder.PreviousBiopsyDate + "; ");
			}
			if (this.m_CytologyClientOrder.Prenatal == true)
			{
				result.Append("Prenatal; ");
			}
			if (this.m_CytologyClientOrder.Postpartum == true)
			{
				result.Append("Postpartum; ");
			}
			if (this.m_CytologyClientOrder.Postmenopausal == true)
			{
				result.Append("Post Menopausal; ");
			}

			if (result.Length > 2)
			{
				result.Remove(result.Length - 2, 2);
			}

			return result.ToString();
		}		
	}
}
