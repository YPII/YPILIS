using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace YellowstonePathology.Business.Flow
{
	public partial class FlowTestTypeItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		public FlowTestTypeItem()
        {

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
