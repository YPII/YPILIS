using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NHunspell;
using System.ComponentModel;
using System.Reflection;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for NHunspell.xaml
    /// </summary>
    public partial class NHunspell : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;
        
        private List<string> m_SuggestedWordList;
        private Hunspell m_Hunspell;

        private int m_CurrentSelectionStart;
        private int m_CurrentSelectionLength;
        private string m_Text;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private SpellCheckAccessionOrder m_SpellCheckAccessionOrder;

        public NHunspell(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SpellCheckAccessionOrder = new SpellCheckAccessionOrder(this.m_AccessionOrder);                    
            this.m_Hunspell = new Hunspell();
            //this.m_Hunspell.Load(@"C:\Program Files\Yellowstone Pathology Institute\en_US-custom.aff", @"C:\Program Files\Yellowstone Pathology Institute\en_US-custom.dic");
            //this.m_Hunspell.Load(@"C:\Program Files\Yellowstone Pathology Institute\en_med_glut.aff", @"C:\Program Files\Yellowstone Pathology Institute\en_med_glut.dic");

            this.m_Hunspell.Load(YellowstonePathology.Properties.Settings.Default.LocalAFFFile, YellowstonePathology.Properties.Settings.Default.LocalDICFile);
            this.m_SpellCheckAccessionOrder.SetErrorCounts(this.m_Hunspell);

            InitializeComponent();            
            this.DataContext = this;

            this.Loaded += NHunspell_Loaded;            
        }

        public SpellCheckAccessionOrder SpellCheckAccessionOrder
        {
            get { return this.m_SpellCheckAccessionOrder; }
        }

        private void NHunspell_Loaded(object sender, RoutedEventArgs e)
        {
            
            //this.ListViewProperties.SelectedIndex = 0;
            this.ListViewProperties.MouseLeftButtonUp += ListViewProperties_MouseLeftButtonUp;         
            //SpellCheckProperty spellCheckProperty = this.GetNextProperty();
            //this.CheckSpelling(spellCheckProperty);            
        }        

        public string Text
        {
            get { return this.m_Text; }
            set
            {
                if(this.m_Text != value)
                {
                    this.m_Text = value;
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        public List<string> SuggestedWordList
        {
            get { return this.m_SuggestedWordList; }
        }

        private void ButtonNextProperty_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_SpellCheckAccessionOrder.HasNextProperty() == true)
            {
                SpellCheckProperty spellCheckProperty = this.GetNextProperty();                
                this.CheckSpelling(spellCheckProperty);
                this.ListViewProperties.SelectedIndex = this.m_SpellCheckAccessionOrder.CurrentPropertyListIndex;
            }
        }

        private SpellCheckProperty GetNextProperty()
        {
            if (this.m_SpellCheckAccessionOrder.HasNextProperty() == true)
            {
                SpellCheckProperty spellCheckProperty = this.m_SpellCheckAccessionOrder.GetNextProperty();
                this.SetProperty(spellCheckProperty);
                return spellCheckProperty;
            }
            else
            {
                //MessageBox.Show("You have reached the end.");
                return null;
            }
        } 
        
        private void SetProperty(SpellCheckProperty spellCheckProperty)
        {
            this.m_Text = spellCheckProperty.GetText();
            this.Title = spellCheckProperty.Description;
            this.NotifyPropertyChanged("Text");
        }      

        private void CheckSpelling(SpellCheckProperty spellCheckProperty)
        {
            if(spellCheckProperty.HasNextMatch() == true)
            {                
                System.Text.RegularExpressions.Match match = spellCheckProperty.GetNextMatch();
                bool correct = this.m_Hunspell.Spell(match.Value);
                if (correct == false)
                {                    
                    this.m_SuggestedWordList = this.m_Hunspell.Suggest(match.Value);
                    this.NotifyPropertyChanged("SuggestedWordList");

                    this.TextBoxText.Focus();
                    this.TextBoxText.SelectionStart = match.Index;
                    this.TextBoxText.SelectionLength = match.Length;

                    this.m_CurrentSelectionStart = match.Index;
                    this.m_CurrentSelectionLength = match.Length;

                    return;
                }
                else
                {
                    this.CheckSpelling(spellCheckProperty);
                }
            }
            else
            {
                //MessageBox.Show("All done.");
            }            
        }

        private void ButtonSkip_Click(object sender, RoutedEventArgs e)
        {            
            if(this.m_SpellCheckAccessionOrder.CurrentPropertyListIndex > -1)
            {
                this.m_SuggestedWordList = new List<string>();
                this.NotifyPropertyChanged("SuggestedWordList");
                this.m_SpellCheckAccessionOrder.Skip();

                SpellCheckProperty spellCheckProperty = this.m_SpellCheckAccessionOrder.GetCurrentProperty();
                if (spellCheckProperty.HasNextMatch() == true)
                {
                    this.CheckSpelling(spellCheckProperty);
                }
            }            
        }            

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewSuggestedWordList.SelectedItems.Count != 0)
            {
                string selectedWord = (string)this.ListViewSuggestedWordList.SelectedItem;                
                this.m_Text = this.m_Text.Remove(this.m_CurrentSelectionStart, this.m_CurrentSelectionLength);
                this.m_Text = this.m_Text.Insert(this.m_CurrentSelectionStart, selectedWord);
                this.m_SuggestedWordList = new List<string>();

                this.NotifyPropertyChanged("Text");
                this.NotifyPropertyChanged("SuggestedWordList");

                SpellCheckProperty spellCheckProperty = this.m_SpellCheckAccessionOrder.GetCurrentProperty();
                spellCheckProperty.Reset(this.m_Text);
                this.CheckSpelling(spellCheckProperty);
            }            
        }        

        private void ListViewProperties_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewProperties.SelectedItem != null)
            {
                SpellCheckProperty spellCheckProperty = (SpellCheckProperty)this.ListViewProperties.SelectedItem;
                this.m_SpellCheckAccessionOrder.SetCurrentProperty(this.ListViewProperties.SelectedIndex);

                this.SetProperty(spellCheckProperty);
                this.CheckSpelling(spellCheckProperty);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();         
        }

        private void ExportDic()
        {
            using (System.IO.StreamWriter writefile = new System.IO.StreamWriter(@"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.aff"))
            {
                string line;
                System.IO.StreamReader readFile = new System.IO.StreamReader(@"C:\Program Files\Yellowstone Pathology Institute\ypi-custom.dic");
                while ((line = readFile.ReadLine()) != null)
                {
                    if (line.Contains("'") == true)
                    {
                        line = line.Replace("'", "''");
                    }

                    writefile.WriteLine("Insert tblWord (Word) values ('" + line + "')");
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

        private void ButtonTesting_Click(object sender, RoutedEventArgs e)
        {
            this.ReadSentanceFile();
        }

        private void ReadSentanceFile()
        {
            List<string> wordList = new List<string>();
            string line;
            System.IO.StreamReader readFile = new System.IO.StreamReader(@"C:\Program Files\Yellowstone Pathology Institute\alldiagnosis.txt");
            while ((line = readFile.ReadLine()) != null)
            {
                System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(@"\b\w+\b");
                foreach (System.Text.RegularExpressions.Match match in rx.Matches(line))
                {
                    if(wordList.Contains(match.Value) == false)
                    {
                        wordList.Add(match.Value);            
                    }                                              
                }                    
            }

            wordList.Sort();
            using (System.IO.StreamWriter writefile = new System.IO.StreamWriter(@"C:\Program Files\Yellowstone Pathology Institute\alldiagnosis.json"))
            {
                foreach(string word in wordList)
                {
                    writefile.WriteLine(word);
                }
            }
        }        
    }
}
