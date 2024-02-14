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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    public class PageControl : System.Windows.Controls.UserControl
    {
        public PageControl()
        {

        }

        public virtual void BeforeNavigatingAway()
        {
            //Not implemented here
        }
    }
}
