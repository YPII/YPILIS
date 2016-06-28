using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Typing
{
    public class TypingShortcutCollection : ObservableCollection<TypingShortcut>
    {
        SqlCommand m_Cmd;

        public TypingShortcutCollection()
        {
            this.m_Cmd = new SqlCommand();
        }        

        public string Find(string shortcut)
        {
            foreach (TypingShortcut typingShortcut in this)
            {
                if (typingShortcut.Shortcut.ToUpper().Trim() == shortcut.ToUpper().Trim())
                {
                    return typingShortcut.Text;
                }
            }
            return string.Empty;
        }                             
    }
}
