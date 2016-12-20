using System;
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
