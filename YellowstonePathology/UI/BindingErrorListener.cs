using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace YellowstonePathology.UI
{
    public class BindingErrorListener : TraceListener
    {
        private Action<string> logAction;

        public static void Listen(Action<string> logAction)
        {
            PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorListener() { logAction = logAction });
        }

        public override void Write(string message) 
        {
            //throw new Exception(message);
        }

        public override void WriteLine(string message)
        {
            logAction(message);
        }
    }
}
