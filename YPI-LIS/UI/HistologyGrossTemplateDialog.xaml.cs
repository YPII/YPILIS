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
    /// Interaction logic for HistologyGrossTemplateDialog.xaml
    /// </summary>
    public partial class HistologyGrossTemplateDialog : Window
    {
        public HistologyGrossTemplateDialog()
        {
            InitializeComponent();
            this.PwdGrammar();
        }

        private void PwdGrammar()
        {
            using (System.Speech.Recognition.SpeechRecognitionEngine recognizer = new System.Speech.Recognition.SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
            {
                //recognizer.LoadGrammar(new System.Speech.Recognition.DictationGrammar());

                System.Speech.Recognition.GrammarBuilder pwdBuilder = new System.Speech.Recognition.GrammarBuilder("My Password is");
                System.Speech.Recognition.GrammarBuilder wildcardBuilder = new System.Speech.Recognition.GrammarBuilder();
                wildcardBuilder.AppendWildcard();
                System.Speech.Recognition.SemanticResultKey pwd = new System.Speech.Recognition.SemanticResultKey("Password", wildcardBuilder);
                pwdBuilder += pwd;

                System.Speech.Recognition.Grammar grammar = new System.Speech.Recognition.Grammar(pwdBuilder);
                grammar.Name = "Password input";
                grammar.SpeechRecognized += new EventHandler<System.Speech.Recognition.SpeechRecognizedEventArgs>(Grammar_SpeechRecognized);

                grammar.Enabled = true;
                recognizer.LoadGrammar(grammar);
                //UpdateGrammarTree(_grammarTreeView, _recognizer);
            }
        }

        private void Grammar_SpeechRecognized(object sender, System.Speech.Recognition.SpeechRecognizedEventArgs e)
        {
            System.Speech.Recognition.SemanticValue semantics = e.Result.Semantics;
            System.Speech.Recognition.RecognitionResult result = e.Result;

            if (!semantics.ContainsKey("Password"))
            {
                MessageBox.Show("Hello");
            }
            else
            {
                System.Speech.Recognition.RecognizedAudio pwdAudio = result.GetAudioForWordRange(result.Words[3], result.Words[result.Words.Count - 1]);
                System.IO.MemoryStream pwdMemoryStream = new System.IO.MemoryStream();
                pwdAudio.WriteToAudioStream(pwdMemoryStream);
            }
        }
    }
}
