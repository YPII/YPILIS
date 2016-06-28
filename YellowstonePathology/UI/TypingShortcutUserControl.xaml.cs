using System;
using System.Collections.Generic;
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

    public partial class TypingShortcutUserControl : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;                
        private YellowstonePathology.Business.Typing.TypingShortcutCollection m_TypingShortcutCollection;
        private object m_Writer;

		public TypingShortcutUserControl(YellowstonePathology.Business.User.SystemIdentity systemIdentity, object writer)
        {            
            this.m_SystemIdentity = systemIdentity;            
			this.m_TypingShortcutCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTypingShortcutCollectionByUser(this.m_SystemIdentity.User.UserId);
            this.m_Writer = writer;
            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Business.Typing.TypingShortcutCollection TypingShortcutCollection
        {
            get { return this.m_TypingShortcutCollection; }
        }        

        public void ContextMenuTypingShortcutDelete_Click(object sender, RoutedEventArgs args)
        {            
            if (this.ListViewTypingShortcut.SelectedItem != null)
            {
                YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = (YellowstonePathology.Business.Typing.TypingShortcut)this.ListViewTypingShortcut.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Delete this item?", "Delete", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.m_TypingShortcutCollection.Remove(typingShortcut);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(typingShortcut, this.m_Writer);
                }
            }
        }

        public void ContextMenuTypingShortcutAdd_Click(object sender, RoutedEventArgs args)
        {
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = new YellowstonePathology.Business.Typing.TypingShortcut(objectId);            
			typingShortcut.ObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();            

            YellowstonePathology.UI.TypingShorcutDialog typingShorcutDialog = new TypingShorcutDialog(typingShortcut, true);
            typingShorcutDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            typingShorcutDialog.ShowDialog();            

            if(typingShorcutDialog.DialogResult == true)
            {
                this.m_TypingShortcutCollection.Add(typingShortcut);
            }
            
            this.NotifyPropertyChanged("");
        }

        public void ContextMenuTypingShortcutEdit_Click(object sender, RoutedEventArgs args)
        {            
            YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = (YellowstonePathology.Business.Typing.TypingShortcut)this.ListViewTypingShortcut.SelectedItem;            
            YellowstonePathology.UI.TypingShorcutDialog typingShortcutDialog = new TypingShorcutDialog(typingShortcut, false);
            typingShortcutDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            typingShortcutDialog.ShowDialog();            
        }

        public void SetShortcut(TextBox textBox)
        {
            int carretPosition = textBox.CaretIndex;
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(textBox.Text.Substring(0, textBox.SelectionStart), @"\S*\s");
            if (matches.Count > 0)
            {
                string lastWord = matches[matches.Count - 1].Value.ToString();
                string returnText = this.m_TypingShortcutCollection.Find(lastWord);
                if (returnText != string.Empty)
                {
                    StringBuilder sb = new StringBuilder(textBox.Text);
                    sb.Replace(lastWord, returnText, matches[matches.Count - 1].Index, matches[matches.Count - 1].Length);
                    textBox.Text = sb.ToString();
                    textBox.SelectionStart = matches[matches.Count - 1].Index + returnText.Length;
                }
            }  
        }
        
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }               
    }
}