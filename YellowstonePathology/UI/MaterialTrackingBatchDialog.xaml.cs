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
	public partial class MaterialTrackingBatchDialog : Window
	{
		YellowstonePathology.Business.Domain.MaterialLocationCollection m_MaterialLocationCollection;

		public MaterialTrackingBatchDialog()
		{
			this.m_MaterialLocationCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetMaterialLocationCollection();
			InitializeComponent();
			this.DataContext = this;
		}

		public YellowstonePathology.Business.Domain.MaterialLocationCollection MaterialLocationCollection
		{
			get { return this.m_MaterialLocationCollection; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			if (this.ComboBoxLocation.SelectedItem != null)
			{
                throw new Exception("needs work");

				//ComboBoxItem comboBoxItem = (ComboBoxItem)this.ComboBoxAction.SelectedItem;
				//string action = comboBoxItem.Content.ToString();

				//YellowstonePathology.Business.Domain.MaterialLocation materialLocation = (YellowstonePathology.Business.Domain.MaterialLocation)this.ComboBoxLocation.SelectedItem;
				//YellowstonePathology.Business.Domain.MaterialTrackingBatchCollection materialTrackingBatchCollection = new Business.Domain.MaterialTrackingBatchCollection();
				//materialTrackingBatchCollection.Add(materialLocation.Name, DateTime.Now, action, materialLocation.MaterialLocationId);
                
				//YellowstonePathology.Business.Gateway.AccessionOrderGateway gateway = new Business.Gateway.AccessionOrderGateway();
				//YellowstonePathology.Business.Gateway.SimpleSubmitter<YellowstonePathology.Business.Domain.MaterialTrackingBatch> submitter = new Business.Gateway.SimpleSubmitter<Business.Domain.MaterialTrackingBatch>(materialTrackingBatchCollection);
				//submitter.SubmitChanges(gateway);
				//this.DialogResult = true;
				//Close();
			}
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
