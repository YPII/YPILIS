using System;
using System.Collections.Generic;
using System.Linq;
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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for NonASCIICharacterCorrectionPage.xaml
    /// </summary>
    public partial class NonASCIICharacterCorrectionPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private System.Windows.Visibility m_BackButtonVisibility;
        private System.Windows.Visibility m_NextButtonVisibility;

        public NonASCIICharacterCorrectionPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
        System.Windows.Visibility backButtonVisibility,
            System.Windows.Visibility nextButtonVisibility)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_BackButtonVisibility = backButtonVisibility;
            this.m_NextButtonVisibility = nextButtonVisibility;

            InitializeComponent();
            DataContext = this;           
        }                

        public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder SurgicalTestOrder
        {
            get { return this.m_SurgicalTestOrder; }
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public System.Windows.Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public System.Windows.Visibility CloseButtonVisibility
        {
            get
            {
                if (this.m_NextButtonVisibility == System.Windows.Visibility.Hidden)
                {
                    return System.Windows.Visibility.Visible;
                }
                return System.Windows.Visibility.Hidden;
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        private void HyperCorrectNonASCIICharacters_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder initialNonASCIICharacters = this.FindNonASCIICharacters();
            this.ReplaceNonASCIICharacters();
            StringBuilder subsequentNonASCIICharacters = this.FindNonASCIICharacters();
            if (subsequentNonASCIICharacters.Length != 0)
            {
                MessageBox.Show("The following non-ASCII characters were found but not replaced: " + Environment.NewLine + subsequentNonASCIICharacters.ToString());
            }
            else
            {
                MessageBox.Show("The following non-ASCII characters have been replaced: " + Environment.NewLine + initialNonASCIICharacters.ToString());
            }
        }

        private void ReplaceNonASCIICharacters()
        {
            StringBuilder result = new StringBuilder();
            string text = this.m_SurgicalTestOrder.CancerSummary;

            Dictionary<int, string> mapDictionary = new Dictionary<int, string>();
            mapDictionary.Add(8804, "<=");
            mapDictionary.Add(8805, ">=");
            mapDictionary.Add(8220, "\"");
            mapDictionary.Add(8221, "\"");

            foreach (KeyValuePair<int, string> map in mapDictionary)
            {
                char nonASCIIChar = (char)map.Key;
                text = text.Replace(nonASCIIChar.ToString(), map.Value);
            }

            this.m_SurgicalTestOrder.CancerSummary = text;
        }

        private StringBuilder FindNonASCIICharacters()
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(this.m_SurgicalTestOrder.CancerSummary) == false)
            {
                string text = this.m_SurgicalTestOrder.CancerSummary;
                for (int i = 0; i < text.Length; ++i)
                {
                    char c = text[i];
                    if (((int)c) > 127)
                    {
                        result.AppendLine("Character: " + text[i].ToString() + ", Code: " + ((int)c).ToString());
                    }
                }
            }
            return result;
        }
    }
}
