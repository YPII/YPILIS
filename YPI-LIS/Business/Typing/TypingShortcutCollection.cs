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
        
        public static string GetMicroText(string text)
        {
            string result = null;
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(text, @"(MICRO:)([A-za-z0-9\s\r\n]+)");
            if(matches.Count > 0)
            {
                if(matches[0].Groups.Count == 3)
                {
                    result = matches[0].Groups[1].Value;
                }
            }
            return result;
        }

        public string GetDxText(string text)
        {
            string result = null;
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(text, @"(MICRO:)([A-za-z0-9\s\r\n]+)");
            if (matches.Count > 0)
            {
                if (matches[0].Groups.Count == 3)
                {
                    result = matches[0].Groups[1].Value;
                }
            }
            return result;
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

        public TypingShortcut FindItem(string shortcut)
        {
            foreach (TypingShortcut typingShortcut in this)
            {
                if (typingShortcut.Shortcut.ToUpper().Trim() == shortcut.ToUpper().Trim())
                {
                    return typingShortcut;
                }
            }
            return null;
        }

        public bool Exists(string shortcut)
        {
            bool result = false;
            foreach (TypingShortcut typingShortcut in this)
            {
                if (typingShortcut.Shortcut.ToUpper().Trim() == shortcut.ToUpper().Trim())
                {
                    result = true;
                    break;
                }
            }
            return result;
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
        
        public TypingShortcutCollection GetWithMatchingText(string searchText)
        {
            TypingShortcutCollection result = new Typing.TypingShortcutCollection();

            string upper = searchText.ToUpper();
            foreach(TypingShortcut typingShortcut in this)
            {
                if(string.IsNullOrEmpty(typingShortcut.Text) == false)
                {
                    string matchUpper = typingShortcut.Text.ToUpper();
                    if (matchUpper.Contains(upper))
                    {
                        result.Add(typingShortcut);
                    }
                }                
            }
            return result;
        }
    }
}
