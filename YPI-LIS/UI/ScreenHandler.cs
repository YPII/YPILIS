using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI
{
    public static class ScreenHandler
    {        
        public static System.Windows.Forms.Screen GetOtherScreen()
        {
            System.Windows.Forms.Screen result = null;

            if (System.Windows.Forms.Screen.AllScreens.Count() >= 2)
            {
                System.Windows.Window window = App.Current.MainWindow;
                var parentArea = new System.Drawing.Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
                System.Windows.Forms.Screen mainScreen = System.Windows.Forms.Screen.FromRectangle(parentArea);

                if (mainScreen.DeviceName.Contains("DISPLAY1"))
                {
                    result = FindScreen("DISPLAY2");
                }
                else if (mainScreen.DeviceName.Contains("DISPLAY2"))
                {
                    result = FindScreen("DISPLAY1");
                }                
            }
            else
            {
                result = System.Windows.Forms.Screen.AllScreens[0];
            }            
            return result;
        }

        public static System.Windows.Forms.Screen FindScreen(string deviceName)
        {
            System.Windows.Forms.Screen result = null;
            foreach(System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                if(screen.DeviceName.Contains(deviceName))
                {
                    result = screen;
                    break;
                }
            }
            return result;
        }
    }
}
