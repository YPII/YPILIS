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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for EventEntryPage.xaml
	/// </summary>
    public partial class EventEntryPage : Page, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.Domain.OrderCommentLog m_OrderCommentLog;
        List<string> m_Comments;

		public EventEntryPage(YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog)
		{
			this.m_OrderCommentLog = orderCommentLog;

            this.m_Comments = new List<string>();
            this.m_Comments.Add("Typographical error during accessioning.");
            this.m_Comments.Add("Typographical error during verification.");
            this.m_Comments.Add("The cow jumped over the moon.");
            this.m_Comments.Add("The pencil fell out of my hand.");
            this.m_Comments.Add("It's Halloween and I went crazy.");

			InitializeComponent();

            this.DataContext = this;
            this.NotifyPropertyChanged("");
		}

        public List<string> Comments
        {
            get { return this.m_Comments; }
        }

		public YellowstonePathology.Business.Domain.OrderCommentLog OrderCommentLog
        {
			get { return this.m_OrderCommentLog; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListBoxComments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxComments.SelectedItems.Count != 0)
            {
                string comment = (string)this.ListBoxComments.SelectedItem;
				this.m_OrderCommentLog.Response = comment;
            }
        }
	}
}
