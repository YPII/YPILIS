using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class SpecimenOrderViewCollection : ObservableCollection<SpecimenOrderView>
	{        
        bool m_ShowUnverifiedSpecimen;

		public SpecimenOrderViewCollection(bool showUnverifiedSpecimen)
		{            
            this.m_ShowUnverifiedSpecimen = showUnverifiedSpecimen;
		}

        public void Load(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.Clear();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
            {
                SpecimenOrderView specimenOrderView = new SpecimenOrderView(specimenOrder, accessionOrder);
                specimenOrderView.IsVisible = false;
                specimenOrderView.WaitingForScan = true;

                if (this.m_ShowUnverifiedSpecimen == false)
                {                
                    specimenOrderView.IsVisible = specimenOrder.Verified;                
                }                                
                this.Add(specimenOrderView);
            }
            this.SortAscending();
        }		

		public void ShowAll()
		{
			foreach (SpecimenOrderView specimenOrderView in this)
			{
				specimenOrderView.IsVisible = true;
			}
		}

		public bool IsVerified()
		{
            bool result = true;
			foreach (SpecimenOrderView specimenOrderView in this)
			{
				if (specimenOrderView.SpecimenOrder.Verified == false)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		public SpecimenOrderView GetSpecimenOrderViewBySpecimenOrderId(string specimenOrderId)
		{
			SpecimenOrderView result = null;
			foreach (SpecimenOrderView item in this)
			{
				if (item.SpecimenOrder.SpecimenOrderId == specimenOrderId)
				{
					result = item;
					break;
				}
			}
			return result;
		}

		public bool CanVerifySpecimen(string specimenOrderId)
		{
			bool result = false;
			foreach (SpecimenOrderView view in this)
			{
				if (view.SpecimenOrder.SpecimenOrderId == specimenOrderId)
				{
					result = true;
					break;
				}
				else if (view.WaitingForScan)
				{
					break;
				}
			}
			return result;
		}        

		public void SortAscending()
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				for (int j = 1; j <= i; j++)
				{
					object o1 = this[j - 1];
					object o2 = this[j];
					if (((IComparable)o1).CompareTo(o2) > 0)
					{
						((IList)this).Remove(o1);
						((IList)this).Insert(j, o1);
					}
				}
			}
		}
	}
}
