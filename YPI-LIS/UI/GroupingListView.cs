using System;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI
{
	public class GroupingListView : ListView
	{
		public override void EndInit()
		{
			base.EndInit();

			if (this.GroupStyle.Count == 0)
			{
				ResourceDictionary r = new ResourceDictionary();
				r.Source = new Uri("pack://application:,,,/UserInterface;component/UI/GroupingListViewStyle.xaml", UriKind.RelativeOrAbsolute);
				this.Resources.MergedDictionaries.Add(r);
				this.GroupStyle.Add(this.Resources["ListViewGroupStyle"] as GroupStyle);				
			}
		}		
	}
}
