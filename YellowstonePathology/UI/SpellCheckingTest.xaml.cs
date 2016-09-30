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
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SpellCheckingTest.xaml
    /// </summary>
    public partial class SpellCheckingTest : Window
    {
        public SpellCheckingTest()
        {
            InitializeComponent();
        }

        private void ButtonCheckSpelling_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void DoIt()
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            app.Visible = false;

            int errors = 0;
            if (this.TextBoxSpell.Text.Length > 0)
            {
                object template = Type.Missing;
                object newTemplate = Type.Missing;
                object documentType = Type.Missing;
                object visible = true;

                Microsoft.Office.Interop.Word._Document doc1 = app.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);
                doc1.Words.First.InsertBefore(this.TextBoxSpell.Text);
                Microsoft.Office.Interop.Word.ProofreadingErrors spellErrorsColl = doc1.SpellingErrors;
                errors = spellErrorsColl.Count;

                object optional = Type.Missing;

                app.ActiveWindow.Activate();

                doc1.CheckSpelling(
                    ref optional, ref optional, ref optional, ref optional, ref optional, ref optional,
                    ref optional, ref optional, ref optional, ref optional, ref optional, ref optional);

                object first = 0;
                object last = doc1.Characters.Count - 1;
                this.TextBoxSpell.Text = doc1.Range(ref first, ref last).Text;
            }

            object saveChanges = false;
            object originalFormat = Type.Missing;
            object routeDocument = Type.Missing;

            app.Quit(ref saveChanges, ref originalFormat, ref routeDocument);

        }
    }
}
