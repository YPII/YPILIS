using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
	public class OrderComment : INotifyPropertyChanged, Interface.IOrderComment
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_OrderCommentId;
		private string m_Category;
		private string m_Action;
		private string m_Description;
		private bool m_RequiresResponse;
		private bool m_RequiresNotification;
		private string m_Response;
		private string m_NotificationAddress;

		public OrderComment()
        {

        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentProperty()]
		public int OrderCommentId
		{
			get { return this.m_OrderCommentId; }
			set
			{
				if(this.m_OrderCommentId != value)
				{
					this.m_OrderCommentId = value;
					this.NotifyPropertyChanged("OrderCommentId");
				}
			}
		}

		[PersistentProperty()]
		public string Category
		{
			get { return this.m_Category; }
			set
			{
				if(this.m_Category != value)
				{
					this.m_Category = value;
					this.NotifyPropertyChanged("Category");
				}
			}
		}

		[PersistentProperty()]
		public string Action
		{
			get { return this.m_Action; }
			set
			{
				if(this.m_Action != value)
				{
					this.m_Action = value;
					this.NotifyPropertyChanged("Action");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if(this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[PersistentProperty()]
		public bool RequiresResponse
		{
			get { return this.m_RequiresResponse; }
			set
			{
				if(this.m_RequiresResponse != value)
				{
					this.m_RequiresResponse = value;
					this.NotifyPropertyChanged("RequiresResponse");
				}
			}
		}

		[PersistentProperty()]
		public bool RequiresNotification
		{
			get { return this.m_RequiresNotification; }
			set
			{
				if(this.m_RequiresNotification != value)
				{
					this.m_RequiresNotification = value;
					this.NotifyPropertyChanged("RequiresNotification");
				}
			}
		}

		[PersistentProperty()]
		public string Response
		{
			get { return this.m_Response; }
			set
			{
				if(this.m_Response != value)
				{
					this.m_Response = value;
					this.NotifyPropertyChanged("Response");
				}
			}
		}

		[PersistentProperty()]
		public string NotificationAddress
		{
			get { return this.m_NotificationAddress; }
			set
			{
				if(this.m_NotificationAddress != value)
				{
					this.m_NotificationAddress = value;
					this.NotifyPropertyChanged("NotificationAddress");
				}
			}
		}
	}
}
