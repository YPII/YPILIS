using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Surgical
{
	public partial class IntraoperativeConsultationResult
	{
		private Dictionary<string, string> m_ValidationErrors;

		private string m_StartDateBinding;
		private string m_EndDateBinding;

		public void ValidateObject()
		{
			this.ValidateStartDateOnLeaving();
			this.ValidateEndDateOnLeaving();
		}

		public System.Windows.Media.SolidColorBrush StartDateBackgroundColorBinding
		{
			get
			{
				System.Windows.Media.SolidColorBrush brush = System.Windows.Media.Brushes.White;
				return brush;
			}
		}

		public string StartDateBinding
		{
			get
			{
				string result = null;

				if (this.m_ValidationErrors.ContainsKey("StartDateBinding") == false)
				{
					if (this.m_StartDate.HasValue == true)
					{
						this.m_StartDateBinding = this.m_StartDate.Value.ToString("MM/dd/yyy HH:mm");
						result = this.m_StartDateBinding;
					}
					else
					{
						this.m_StartDateBinding = null;
						result = this.m_StartDateBinding;
					}
				}
				else
				{
					return this.m_StartDateBinding;
				}
				return result;
			}
			set
			{
				string formattedString = YellowstonePathology.Business.Helper.DateTimeExtensions.AddSlashesSpacesAndColon(value);
				this.m_StartDateBinding = formattedString;

				DateTime startDate;
				bool isGoodDate = DateTime.TryParse(formattedString, out startDate);
				if (isGoodDate == true)
				{
					this.m_StartDate = startDate;
				}
				else
				{
					this.m_StartDateBinding = value;
				}

				this.ValidateStartDate();
			}
		}

		public void ValidateStartDate()
		{
			if (this.m_ValidationErrors.ContainsKey("StartDateBinding") == true) this.m_ValidationErrors.Remove("StartDateBinding");

			DateTime startDate;
			bool isGoodDate = DateTime.TryParse(this.m_StartDateBinding, out startDate);
			if (isGoodDate == false)
			{
				if (string.IsNullOrEmpty(this.m_StartDateBinding) == false)
				{
					this.m_ValidationErrors.Add("StartDateBinding", "The Start Date is invalid");
				}
			}

			this.NotifyPropertyChanged("StartDateBinding");
			this.NotifyPropertyChanged("StartDate");
			this.NotifyPropertyChanged("StartDateBackgroundColorBinding");
		}

		public void ValidateStartDateOnLeaving()
		{
			if (this.m_ValidationErrors.ContainsKey("StartDateBinding") == true) this.m_ValidationErrors.Remove("StartDateBinding");

			DateTime startDate;
			bool isGoodDate = DateTime.TryParse(this.m_StartDateBinding, out startDate);
			if (isGoodDate == false)
			{
				this.m_ValidationErrors.Add("StartDateBinding", "The Start Date is invalid");
			}

			this.NotifyPropertyChanged("StartDateBinding");
			this.NotifyPropertyChanged("StartDate");
			this.NotifyPropertyChanged("StartDateBackgroundColorBinding");
		}

		public System.Windows.Media.SolidColorBrush EndDateBackgroundColorBinding
		{
			get
			{
				System.Windows.Media.SolidColorBrush brush = System.Windows.Media.Brushes.White;
				return brush;
			}
		}

		public string EndDateBinding
		{
			get
			{
				string result = null;

				if (this.m_ValidationErrors.ContainsKey("EndDateBinding") == false)
				{
					if (this.m_EndDate.HasValue == true)
					{
						this.m_EndDateBinding = this.m_EndDate.Value.ToString("MM/dd/yyy HH:mm");
						result = this.m_EndDateBinding;
					}
					else
					{
						this.m_EndDateBinding = null;
						result = this.m_EndDateBinding;
					}
				}
				else
				{
					return this.m_EndDateBinding;
				}
				return result;
			}
			set
			{
				string formattedString = YellowstonePathology.Business.Helper.DateTimeExtensions.AddSlashesSpacesAndColon(value);
				this.m_EndDateBinding = formattedString;

				DateTime endDate;
				bool isGoodDate = DateTime.TryParse(formattedString, out endDate);
				if (isGoodDate == true)
				{
					this.m_EndDate = endDate;
				}
				else
				{
					this.m_EndDateBinding = value;
				}

				this.ValidateEndDate();
			}
		}

		public void ValidateEndDate()
		{
			if (this.m_ValidationErrors.ContainsKey("EndDateBinding") == true) this.m_ValidationErrors.Remove("EndDateBinding");

			DateTime endDate;
			bool isGoodDate = DateTime.TryParse(this.m_EndDateBinding, out endDate);
			if (isGoodDate == false)
			{
				if (string.IsNullOrEmpty(this.m_EndDateBinding) == false)
				{
					this.m_ValidationErrors.Add("EndDateBinding", "The End Date is invalid");
				}
			}

			this.NotifyPropertyChanged("EndDateBinding");
			this.NotifyPropertyChanged("EndDate");
			this.NotifyPropertyChanged("EndDateBackgroundColorBinding");
		}

		public void ValidateEndDateOnLeaving()
		{
			if (this.m_ValidationErrors.ContainsKey("EndDateBinding") == true) this.m_ValidationErrors.Remove("EndDateBinding");

			DateTime endDate;
			bool isGoodDate = DateTime.TryParse(this.m_EndDateBinding, out endDate);
			if (isGoodDate == false)
			{
				if (string.IsNullOrEmpty(this.m_EndDateBinding) == true)
				{
					this.m_ValidationErrors.Add("EndDateBinding", "The End Date should not be blank.");
				}
				else
				{
					this.m_ValidationErrors.Add("EndDateBinding", "The End Date is invalid");
				}
			}

			this.NotifyPropertyChanged("EndDateBinding");
			this.NotifyPropertyChanged("EndDate");
			this.NotifyPropertyChanged("EndDateBackgroundColorBinding");
		}

		public void ClearDateValidationErrors()
		{
			if (this.m_ValidationErrors.ContainsKey("StartDateBinding") == true) this.m_ValidationErrors.Remove("StartDateBinding");
			if (this.m_ValidationErrors.ContainsKey("EndDateBinding") == true) this.m_ValidationErrors.Remove("EndDateBinding");
		}

		public string this[string columnName]
		{
			get
			{
				string result = null;
				if (this.m_ValidationErrors.ContainsKey(columnName) == true)
				{
					result = this.m_ValidationErrors[columnName];
				}
				return result;
			}
		}

		public string Error
		{
			get
			{
				string result = null;
				if (this.m_ValidationErrors.Count > 0)
				{
					result = "There are errors that need to handled.";
				}
				return result;
			}
		}

		public void AddError(string proteryName, string message)
		{
			if (this.m_ValidationErrors.ContainsKey(proteryName) == false)
			{
				this.m_ValidationErrors.Add(proteryName, message);
			}
		}

		private void RemoveError(string propertyName)
		{
			if (this.m_ValidationErrors.ContainsKey(propertyName) == true)
			{
				this.m_ValidationErrors.Remove(propertyName);
			}
		}

		public Dictionary<string, string> ValidationErrors
		{
			get { return this.m_ValidationErrors; }
		}
	}
}
