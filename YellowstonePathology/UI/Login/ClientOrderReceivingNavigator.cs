using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class ClientOrderReceivingNavigator
    {
        private static System.Windows.Window m_MainWindow;

        public static System.Windows.Window MainWindow
        {
            get { return m_MainWindow; }
            set { m_MainWindow = value; }
        }
    }
}
