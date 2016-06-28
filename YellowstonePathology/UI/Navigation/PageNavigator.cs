using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Navigation
{
    public class PageNavigator
    {        
        private ContentControl m_ContentControl;
        private UserControl m_CurrentPage;

        private System.Windows.Window m_PrimaryMonitorWindow;
        private System.Windows.Window m_SecondMonitorWindow;
        

        public PageNavigator(ContentControl contentControl)
        {
            this.m_ContentControl = contentControl;            
        }        

        public UserControl CurrentPage
        {
            get { return this.m_CurrentPage; }
        }

        public void ClearCurrentPage()
        {
            this.m_CurrentPage = null;
            this.m_ContentControl.Content = null;
        }

        public System.Windows.Window SecondMonitorWindow
        {
            get { return this.m_SecondMonitorWindow; }
        }

        public System.Windows.Window PrimaryMonitorWindow
        {
            get
            {
                if (this.m_PrimaryMonitorWindow == null) throw new Exception("Primary Monitor window is null.");
                return this.m_PrimaryMonitorWindow;
            }
            set { this.m_PrimaryMonitorWindow = value; }
        }

        public void Navigate(UserControl page)
        {            

            if(this.m_CurrentPage is UI.PageControl)
            {
                ((UI.PageControl)this.m_CurrentPage).BeforeNavigatingAway();                
            }           

            YellowstonePathology.UI.Navigation.NavigatingEventArgs eventArgs = new NavigatingEventArgs(this.m_CurrentPage, page);			

            this.m_CurrentPage = page;
            this.m_ContentControl.Content = page;            
        }

        public void ShowSecondMonitorWindow(System.Windows.Window window)
        {            
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
            {
                if (this.m_SecondMonitorWindow != null) this.m_SecondMonitorWindow.Close();                

                System.Windows.Forms.Screen screen2 = System.Windows.Forms.Screen.AllScreens[1];                                             
                System.Drawing.Rectangle screen2Rectangle = screen2.WorkingArea;

                this.m_SecondMonitorWindow = window;
                this.m_SecondMonitorWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                this.m_SecondMonitorWindow.Width = 1500;
                this.m_SecondMonitorWindow.Height = 800;
                this.m_SecondMonitorWindow.Left = screen2Rectangle.Left + (screen2Rectangle.Width - this.m_SecondMonitorWindow.Width) / 2;
                this.m_SecondMonitorWindow.Top = screen2Rectangle.Top + (screen2Rectangle.Height - this.m_SecondMonitorWindow.Height) / 2;             
                this.m_SecondMonitorWindow.Show();                
            }	            
        }

        public bool HasDualMonitors()
        {
            bool result = false;
            if (System.Windows.Forms.Screen.AllScreens.Length == 2)
            {
                result = true;
            }
            return result;
        }

        public void Close()
        {			
            if (this.m_SecondMonitorWindow != null)
            {
                this.m_SecondMonitorWindow.Close();
            }
        }       
    }
}
