using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YellowstonePathology.Business.Twain.TwainNative
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct Event
    {
        public IntPtr EventPtr;
        public Message Message;
    }
}
