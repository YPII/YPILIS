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
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    /// <summary>
    /// Interaction logic for PatientLinkingPage.xaml
    /// </summary>
    public partial class PatientLinkingPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		private YellowstonePathology.Business.Patient.Model.PatientLinker m_PatientLinker;
        private bool m_ShowPageButtons;
        private Brush m_LinkingStatusColor;
        private string m_PageHeaderText = "Patient Linking";
        private YellowstonePathology.Business.Patient.Model.PatientLinkingListModeEnum m_Mode;

		private string m_DisplayName;
		private string m_PatientId;

        public PatientLinkingPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,             
            bool showPageButtons,
			YellowstonePathology.Business.Patient.Model.PatientLinkingListModeEnum mode,
			YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker)
        {
            this.m_AccessionOrder = accessionOrder;            
            this.m_ShowPageButtons = showPageButtons;
            this.m_Mode = mode;

			this.m_PatientId = this.m_AccessionOrder.PatientId;
			this.m_DisplayName = this.m_AccessionOrder.PatientDisplayName;
			this.m_PatientLinker = patientLinker;

			InitializeComponent();

            this.DataContext = this;
            Loaded += new RoutedEventHandler(PatientLinkingPage_Loaded);
            Unloaded += PatientLinkingPage_Unloaded;
        }

        private void PatientLinkingPage_Loaded(object sender, RoutedEventArgs e)
        {
             
			if (this.m_PatientLinker.IsOkToLink.IsValid == true)
			{
				this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
					new Action(delegate()
						{
							this.m_PatientLinker.GetLinkingList();
							this.m_PatientLinker.SetMatches();
							this.SetPatientLinkingListSelections();
							this.NotifyPropertyChanged("PatientLinkingList");
						}));
			}
			else
			{
				this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
						new Action(delegate() { MessageBox.Show("Unable to show the linking list because\r\n" + this.m_PatientLinker.IsOkToLink.Message, "Missing Information"); }));
			}

            this.SetLinkingStatusColor();
            if (!this.m_ShowPageButtons)
            {
                this.ButtonBack.Visibility = System.Windows.Visibility.Collapsed;
            }
            
            this.ButtonLink.IsEnabled = this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe;            
        }

        private void PatientLinkingPage_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        public void SetPatientLinkingListSelections()
		{
			foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item in this.m_PatientLinker.LinkingList)
			{
				if (item.IsSelected == true)
				{
					this.listViewLinkingList.SelectedItems.Add(item);
				}
			}
			NotifyPropertyChanged("PatientLinkingList");
		}

        public void SetLinkingStatusColor()
        {
            Brush brush = Brushes.LightGreen;

			if (string.IsNullOrEmpty(this.m_AccessionOrder.PatientId) == true || this.m_AccessionOrder.PatientId == "0")
			{
				brush = Brushes.PaleVioletRed;
			}
            this.LinkingStatusColor = brush;
        }

        public Brush LinkingStatusColor
        {
            get { return this.m_LinkingStatusColor; }
            set
            {
                this.m_LinkingStatusColor = value;
                this.NotifyPropertyChanged("LinkingStatusColor");
            }
        }

		public string DisplayName
		{
			get { return this.m_DisplayName; }
		}

		public string PatientId
		{
			get { return this.m_PatientId; }
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonCaseNotes_Click(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Command, FinalizeAccessionCommandTypeEnum.ShowCaseNotes);
            this.Return(this, args);
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public ObservableCollection<YellowstonePathology.Business.Patient.Model.PatientLinkingListItem> PatientLinkingList
        {
            get { return this.m_PatientLinker.LinkingList; }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public void ButtonLink_Click(object sender, RoutedEventArgs args)
        {
			this.m_PatientLinker.SetItemsToUnSelected();

            int selectedCount = 0;
            foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem item in this.listViewLinkingList.SelectedItems)
            {
                item.IsSelected = true;
                selectedCount++;
            }

            if (selectedCount == 0)
            {
                MessageBox.Show("No Items have been selected to link.  Select at least one item to link.");
                return;
            }
			this.m_PatientId = this.m_PatientLinker.Link();

			this.m_AccessionOrder.PatientId = this.m_PatientId;
			this.NotifyPropertyChanged("PatientLinkingList");
			this.NotifyPropertyChanged("PatientId");

            this.SetLinkingStatusColor();
        }

        public void ButtonPrint_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Reports.PatientLinkingListing rpt = new YellowstonePathology.Business.Reports.PatientLinkingListing();
            rpt.CreateReport(this.m_PatientLinker.LinkingList);
        }

        public void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
            this.Return(this, args);
        }

        public void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PatientId) == true || this.m_AccessionOrder.PatientId == "0")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("This patient is not linked, are you sure you want to continue.", "Patient Not Linked.", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                    this.Return(this, args);
                }
            }
            else
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
                this.Return(this, args);
            }
        }        
	}
}
