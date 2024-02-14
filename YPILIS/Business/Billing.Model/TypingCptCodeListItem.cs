using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing.Model
{
	public class TypingCptCodeListItem : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;        
        int m_Quantity;

		public TypingCptCodeListItem(YellowstonePathology.Business.Billing.Model.CptCode cptCode)
        {
            this.m_CptCode = cptCode;            
            this.m_Quantity = 1;
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Billing.Model.CptCode CptCode
        {
            get { return this.m_CptCode; }
        }        

        public int Quantity
        {
            get { return this.m_Quantity; }
            set
            {
                if (value != this.m_Quantity)
                {
                    this.m_Quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }
            }
        }
    }
}
