using System;

namespace YellowstonePathology.Business.Twain
{
    public class ScanningCompleteEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public ScanningCompleteEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
