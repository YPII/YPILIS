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

namespace YellowstonePathology.UI.Login
{	
	public partial class MissingInformationPage : UserControl
	{
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private string m_NewText;     

		public MissingInformationPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{            
            this.m_AccessionOrder = accessionOrder;                        
			InitializeComponent();
			DataContext = this;
		}         

        public string NewText
        {
            get { return this.m_NewText; }
            set { this.m_NewText = value; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}        

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
            this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            this.Next(this, new EventArgs());
		}		

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.m_NewText) == false)
            {
                string newDialog = YellowstonePathology.Business.User.SystemIdentity.Instance.User.DisplayName + ": " + DateTime.Now.ToString("MM/dd/yyyy HH:mm") + Environment.NewLine + this.m_NewText;
                if (string.IsNullOrEmpty(this.m_AccessionOrder.CaseDialog) == false)
                {
                    this.m_AccessionOrder.CaseDialog = newDialog + Environment.NewLine + Environment.NewLine + this.m_AccessionOrder.CaseDialog;
                }
                else
                {
                    this.m_AccessionOrder.CaseDialog = newDialog;
                }

                this.m_NewText = null;               
            }            
        }
    }
}
