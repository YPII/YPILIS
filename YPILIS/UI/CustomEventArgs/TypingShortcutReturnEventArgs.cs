using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TypingShortcutReturnEventArgs : System.EventArgs
    {
		YellowstonePathology.Business.Typing.TypingShortcut m_TypingShortcut;

		public TypingShortcutReturnEventArgs(YellowstonePathology.Business.Typing.TypingShortcut typingShortcut)
        {
            this.m_TypingShortcut = typingShortcut;
        }

		public YellowstonePathology.Business.Typing.TypingShortcut TypingShortcut
        {
            get { return this.m_TypingShortcut; }
        }
    }
}
