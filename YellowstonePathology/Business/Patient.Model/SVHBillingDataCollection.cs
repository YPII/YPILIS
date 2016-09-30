using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Patient.Model
{
	public class SVHBillingDataCollection : ObservableCollection<SVHBillingData>
	{
		public SVHBillingDataCollection()
		{

		}

        public YellowstonePathology.Business.Patient.Model.SVHBillingData GetMostRecent()
        {
            SVHBillingData result = null;
            DateTime workingDate = DateTime.Parse("1/1/1900");
            foreach (SVHBillingData svhBillingData in this)
            {
                if (svhBillingData.FileDate > workingDate)
                {
                    workingDate = svhBillingData.FileDate;
                    result = svhBillingData;
                }
            }
            return result;
        }

		public void AddOnlyMostRecent(SVHBillingData itemToAdd)
		{
			if (string.IsNullOrEmpty(itemToAdd.MRN) == false)
			{
				SVHBillingData existingItem = null;
				DateTime? itemToAddAdmitDate = null;
				DateTime? existingItemAdmitDate = null;

				foreach (SVHBillingData currentItem in this)
				{
					if (itemToAdd.MRN == currentItem.MRN)
					{
						existingItem = currentItem;
						DateTime tmp;
						bool hasDate = DateTime.TryParse(itemToAdd.AdmitDate, out tmp);
						if (hasDate == true) itemToAddAdmitDate = tmp;

						hasDate = DateTime.TryParse(existingItem.AdmitDate, out tmp);
						if (hasDate == true) existingItemAdmitDate = tmp;
						break;
					}
				}

				if (existingItem != null)
				{
					if (itemToAddAdmitDate != null && existingItemAdmitDate == null)
					{
						this.Remove(existingItem);
						this.Add(itemToAdd);
					}
					else if (itemToAddAdmitDate != null && existingItemAdmitDate != null)
					{
						if (itemToAddAdmitDate.Value > existingItemAdmitDate.Value)
						{
							this.Remove(existingItem);
							this.Add(itemToAdd);
						}
					}
				}
				else
				{
					this.Add(itemToAdd);
				}

			}
		}
	}
}
