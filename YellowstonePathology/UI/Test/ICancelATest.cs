using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public interface IResultPage
    {        
        event YellowstonePathology.UI.CustomEventArgs.EventHandlerDefinitions.CancelTestEventHandler CancelTest;        
    }    
}
