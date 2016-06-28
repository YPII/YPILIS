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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for MaterialTrackingDialog.xaml
    /// </summary>
    public partial class MaterialTrackingDialog : Window, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		YellowstonePathology.Business.View.AccessionSlideOrderViewCollection m_AccessionSlideOrderViewCollection;
		YellowstonePathology.Business.Domain.MaterialLocationCollection m_MaterialLocationCollection;
		YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection m_MaterialTrackingBatchCollection;
		YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection m_SlideTrackingLogCollection;
        int m_SlideCount;
		int m_LocationId;

        public MaterialTrackingDialog()
        {
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;

			this.m_AccessionSlideOrderViewCollection = new Business.View.AccessionSlideOrderViewCollection();
			this.m_MaterialLocationCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialLocationCollection();
			this.m_MaterialTrackingBatchCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingBatchCollection();

			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;            
            this.m_BarcodeScanPort.HistologySlideScanReceived += HistologySlideScanReceived;
			this.m_SlideTrackingLogCollection = new YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogCollection();

			this.Closing += new CancelEventHandler(MaterialTrackingDialog_Closing);
            InitializeComponent();
			DataContext = this;
        }

        public int SlideCount
        {
            get { return this.m_SlideCount; }
            set
            {
                this.m_SlideCount = value;
                this.NotifyPropertyChanged("SlideCount");
            }
        }

		public YellowstonePathology.Business.View.AccessionSlideOrderViewCollection AccessionSlideOrderViewCollection
		{
			get { return this.m_AccessionSlideOrderViewCollection; }
			set
			{
				this.m_AccessionSlideOrderViewCollection = value;
				NotifyPropertyChanged("AccessionSlideOrderViewCollection");
			}
		}

		public YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatchCollection MaterialTrackingBatchCollection
		{
			get { return this.m_MaterialTrackingBatchCollection; }
		}

		private void HistologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
				this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
					new Action(
						delegate()
						{
							if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
							{
                                if (this.m_AccessionSlideOrderViewCollection.Exists(barcode.ID) == false)
                                {
                                    YellowstonePathology.Business.View.AccessionSlideOrderView accessionSlideOrderView = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetAccessionSlideOrderViewBySlideOrderId(barcode.ID);
									if (accessionSlideOrderView != null)
									{
										this.m_AccessionSlideOrderViewCollection.Add(accessionSlideOrderView);
										this.SlideCount = this.m_AccessionSlideOrderViewCollection.Count;
										this.CreateSlideTrackingLogEntry(accessionSlideOrderView);
									}
									else
									{
										MessageBox.Show("The scan did not identify the slide.");
									}
                                }
                                else
                                {
                                    this.SetSelectedSlide(barcode.ID);                              
                                }
							}
						}));
        }

		private void SetSelectedSlide(string specimenOrderId)
        {
            foreach (YellowstonePathology.Business.View.AccessionSlideOrderView view in this.ListBoxSlideTracking.Items)
            {
				if (view.SlideOrder.SlideOrderId == specimenOrderId)
                {
                    this.ListBoxSlideTracking.SelectedItem = view;
                }
            }
        }

		private void CreateSlideTrackingLogEntry(YellowstonePathology.Business.View.AccessionSlideOrderView accessionSlideOrderView)
		{
            throw new Exception("Needs work");
			//YellowstonePathology.Business.Domain.MaterialTrackingBatch materialTrackingBatch = (YellowstonePathology.Business.Domain.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem;
			////////int locationId = this.GetLocationId(materialTrackingBatch.Name);
			//this.m_SlideTrackingLogCollection.Add(accessionSlideOrderView.SlideOrder.SlideOrderId, materialTrackingBatch.MaterialTrackingBatchId, this.m_LocationId, materialTrackingBatch.Name, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.UserName, materialTrackingBatch.Action);
		}

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void MaterialTrackingDialog_Closing(object sender, CancelEventArgs e)
		{
			this.SaveSlideTrackingLog();
		}

		private void ButtonCreateBatch_Click(object sender, RoutedEventArgs e)
		{
			MaterialTrackingBatchDialog materialTrackingBatchDialog = new MaterialTrackingBatchDialog();
			bool? result = materialTrackingBatchDialog.ShowDialog();
			if (result.HasValue && result.Value == true)
			{
				this.m_MaterialTrackingBatchCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialTrackingBatchCollection();
				NotifyPropertyChanged("MaterialTrackingBatchCollection");
			}
		}

		private void ButtonPrintBatch_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
			{
                throw new Exception("Needs work");


				//YellowstonePathology.Business.Domain.MaterialTrackingBatch materialTrackingBatch = (YellowstonePathology.Business.Domain.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem;
				//Login.CaseCompilation.SlideDistributionDetailData slideDistributionDetailData = new Login.CaseCompilation.SlideDistributionDetailData(materialTrackingBatch.Name, materialTrackingBatch.BatchDate, this.m_AccessionSlideOrderViewCollection);
				//Login.CaseCompilation.SlideDistributionDetail slideDistributionDetail = new Login.CaseCompilation.SlideDistributionDetail(slideDistributionDetailData);

				//XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
				//xpsDocumentViewer.LoadDocument(slideDistributionDetail.FixedDocument);
				//xpsDocumentViewer.ShowDialog();


				/////YellowstonePathology.Business.Document.MaterialBatchListing materialBatchListing = new Business.Document.MaterialBatchListing(materialTrackingBatch.Name, materialTrackingBatch.BatchDate, this.m_AccessionSlideOrderViewCollection);
				/////materialBatchListing.Print();
			}
		}

		private void ButtonDeleteEmptyBatch_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
			{
				if (this.m_AccessionSlideOrderViewCollection.Count == 0)
				{
					foreach (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch in this.m_MaterialTrackingBatchCollection)
					{
						if (materialTrackingBatch.MaterialTrackingBatchId == ((YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem).MaterialTrackingBatchId)
						{
                            throw new Exception("needs work");
							//this.m_MaterialTrackingBatchCollection.Remove(materialTrackingBatch);
							//YellowstonePathology.Business.Gateway.AccessionOrderGateway gateway = new Business.Gateway.AccessionOrderGateway();
							//gateway.SubmitChanges(this.m_MaterialTrackingBatchCollection);
							//this.m_MaterialTrackingBatchCollection.Reset(YellowstonePathology.Business.Domain.Persistence.CollectionTrackingResetModeEnum.KeyPropertyDataNotPresentAfterInsert);
							//break;
						}
					}

				}
			}
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		private void ListBoxMaterialTrackingBatch_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.SaveSlideTrackingLog();
			this.m_AccessionSlideOrderViewCollection.Clear();
			this.m_SlideTrackingLogCollection.Clear();

			if (this.ListBoxMaterialTrackingBatch.SelectedItem != null)
			{
				YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = (YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch)this.ListBoxMaterialTrackingBatch.SelectedItem;
				this.m_AccessionSlideOrderViewCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetAccessionSlideOrderViewCollectionByBatchId(materialTrackingBatch.MaterialTrackingBatchId);
                this.SlideCount = this.m_AccessionSlideOrderViewCollection.Count;
				this.m_LocationId = this.GetLocationId(materialTrackingBatch.Description);
				NotifyPropertyChanged("AccessionSlideOrderViewCollection");
			}
		}

		private void SaveSlideTrackingLog()
		{
            throw new Exception("needs work");
			//YellowstonePathology.Business.Gateway.AccessionOrderGateway gateway = new Business.Gateway.AccessionOrderGateway();
			//YellowstonePathology.Business.Gateway.SimpleSubmitter<YellowstonePathology.Business.Slide.ModelTrackingLog> submitter = new Business.Gateway.SimpleSubmitter<YellowstonePathology.Business.Slide.ModelTrackingLog>(this.m_SlideTrackingLogCollection);
			//submitter.SubmitChanges(gateway);
		}

		private int GetLocationId(string materialTrackingBatchName)
		{
			int result = 0;
			foreach (YellowstonePathology.Business.Domain.MaterialLocation materialLocation in this.m_MaterialLocationCollection)
			{
				if (materialLocation.Name == materialTrackingBatchName)
				{
					result = materialLocation.MaterialLocationId;
					break;
				}
			}
			return result;
		}
	}
}
