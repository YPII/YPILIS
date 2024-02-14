using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PanelSetCptCode
    {
        public event PropertyChangedEventHandler PropertyChanged;
                
        int m_Quantity;
        CptCode m_CptCode;

        public PanelSetCptCode()
        {

        }

        public PanelSetCptCode(CptCode cptCode, int quantity)
        {                    
            this.m_CptCode = cptCode;
            this.m_Quantity = quantity;
        }                

        public CptCode CptCode
        {
            get { return this.m_CptCode; }
            set 
            {
                if (this.m_CptCode != value)
                {
                    this.m_CptCode = value;
                    this.NotifyPropertyChanged("CptCode");
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
		
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }	
	}
}
