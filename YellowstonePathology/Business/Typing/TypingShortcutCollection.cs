using System;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Typing
{
    public class TypingShortcutCollection : ObservableCollection<TypingShortcut>
    {
        MySqlCommand m_Cmd;

        public TypingShortcutCollection()
        {
            this.m_Cmd = new MySqlCommand();
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
        
        public void UpdateItem(TypingShortcut typingShortcut)
        {
            foreach(TypingShortcut item in this)
            {
                if(item.ObjectId == typingShortcut.ObjectId)
                {
                    item.Update(typingShortcut);
                }
            }
        }                      
    }
}
