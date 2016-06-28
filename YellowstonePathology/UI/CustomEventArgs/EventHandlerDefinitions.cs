using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class EventHandlerDefinitions
    {
        public delegate void CancelTestEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e);
    }
}
