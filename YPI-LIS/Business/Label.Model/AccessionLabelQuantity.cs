using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Label.Model
{
    public class AccessionLabelQuantity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_IsValid;
        private int m_Quantity;
        private AccessionLabel m_AccessionLabel;

        public AccessionLabelQuantity(int quantity, AccessionLabel accessionLabel, bool isValid)
        {
            this.m_Quantity = quantity;
            this.m_AccessionLabel = accessionLabel;
            this.m_IsValid = isValid;
        }

        public bool IsValid
        {
            get { return this.m_IsValid; }
            set 
            {
                if (this.m_IsValid != value)
                {
                    this.m_IsValid = value;
                    this.NotifyPropertyChanged("IsValid");
                }
            }
        }

        public int Quantity
        {
            get { return this.m_Quantity; }
            set 
            {
                if (this.m_Quantity != value)
                {
                    this.m_Quantity = value;
                    this.NotifyPropertyChanged("Quantity");
                }                
            }
        }

        public AccessionLabel AccessionLabel
        {
            get { return this.m_AccessionLabel; }
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
