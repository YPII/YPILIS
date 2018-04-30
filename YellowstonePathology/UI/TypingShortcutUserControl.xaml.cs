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

            YellowstonePathology.UI.TypingShorcutDialog typingShortcutDialog = new TypingShorcutDialog(typingShortcut.ObjectId, true);
            typingShortcutDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            typingShortcutDialog.Finished += TypingShortcutDialogAdd_Finished;
            typingShortcutDialog.ShowDialog();            

            if(typingShortcutDialog.DialogResult == true)
            {
                this.m_TypingShortcutCollection.Add(typingShortcut);
            }
            
            this.NotifyPropertyChanged("");
        }

        private void TypingShortcutDialogAdd_Finished(object sender, CustomEventArgs.TypingShortcutReturnEventArgs e)
        {
            this.m_TypingShortcutCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTypingShortcutCollectionByUser(this.m_SystemIdentity.User.UserId);
        }

        public void ContextMenuTypingShortcutEdit_Click(object sender, RoutedEventArgs args)
        {            
            YellowstonePathology.Business.Typing.TypingShortcut typingShortcut = (YellowstonePathology.Business.Typing.TypingShortcut)this.ListViewTypingShortcut.SelectedItem;            
            YellowstonePathology.UI.TypingShorcutDialog typingShortcutDialog = new TypingShorcutDialog(typingShortcut.ObjectId, false);
            typingShortcutDialog.Finished += TypingShortcutDialog_Finished;
            typingShortcutDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            typingShortcutDialog.ShowDialog();            
        }

        private void TypingShortcutDialog_Finished(object sender, CustomEventArgs.TypingShortcutReturnEventArgs e)
        {
            this.m_TypingShortcutCollection.UpdateItem(e.TypingShortcut);
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

        public void SetShortcut(TextBox microscopix, Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(microscopix.Text.Substring(0, microscopix.SelectionStart), @"(@[A-za-z]+)([0-9]+) ");
            if(matches.Count > 0)
            {
                string shortcutName = matches[0].Groups[1].Value;
                string specimenNumber = matches[0].Groups[2].Value;
                if (this.m_TypingShortcutCollection.Exists(shortcutName) == true)
                {
                    string shortcutText = this.m_TypingShortcutCollection.Find(shortcutName);                    
                    System.Text.RegularExpressions.MatchCollection shortcutMatches = System.Text.RegularExpressions.Regex.Matches(shortcutText, @"(MICRO:)([\s\S]+)(DX:)([\s\S]+)");
                    if (shortcutMatches.Count > 0)
                    {
                        if (matches[0].Groups.Count == 3)
                        {
                            microscopix.Text = microscopix.Text.Replace(matches[0].Value, "Specimen " + specimenNumber + " - ");
                            microscopix.Text += shortcutMatches[0].Groups[2].Value.Trim();
                            Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = surgicalTestOrder.SurgicalSpecimenCollection.GetBySpecimenNumber(specimenNumber);
                            surgicalSpecimen.Diagnosis += shortcutMatches[0].Groups[4].Value;
                            microscopix.SelectionStart = microscopix.Text.Length;
                        }
                    }
                }
                else
                {
                    this.SetShortcut(microscopix);
                }
            }
            else
            {
                this.SetShortcut(microscopix);
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