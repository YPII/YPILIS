using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Common
{
	public class FieldEnabler : INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private bool m_IsProtectedEnabled;
		private bool m_IsUnprotectedEnabled;
		private bool m_SignatureButtonEnabled;
		private bool m_BillingAuditEnabled;

		public FieldEnabler()
		{
			this.m_IsProtectedEnabled = false;
			this.m_IsUnprotectedEnabled = false;
			this.m_SignatureButtonEnabled = false;
			this.m_BillingAuditEnabled = false;
		}

		public bool IsProtectedEnabled
		{
            get { return  this.m_IsProtectedEnabled; }
			set
			{
				this.m_IsProtectedEnabled = value;
				NotifyPropertyChanged("IsProtectedEnabled");                
			}
		}

		public bool IsUnprotectedEnabled
		{
			get { return m_IsUnprotectedEnabled; }
			set
			{
				m_IsUnprotectedEnabled = value;
				NotifyPropertyChanged("IsUnprotectedEnabled");                
			}
		}

		public bool IsSignatureButtonEnabled
		{
			get { return this.m_SignatureButtonEnabled; }

			set
			{
				this.m_SignatureButtonEnabled = value;
				NotifyPropertyChanged("IsSignatureButtonEnabled");
			}
		}

		public bool IsBillingAuditEnabled
		{
			get { return this.m_BillingAuditEnabled; }
			set
			{
				this.m_BillingAuditEnabled = value;
				NotifyPropertyChanged("IsBillingAuditEnabled");
			}
		}

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
