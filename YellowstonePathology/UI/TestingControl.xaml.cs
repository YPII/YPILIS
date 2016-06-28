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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TestingControl.xaml
    /// </summary>
    public partial class TestingControl : UserControl
    {        
        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TestingControl),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(TextChangedCallBack)));        

        public TestingControl()
        {
            InitializeComponent();
            //this.TextBoxTest.TextChanged += new TextChangedEventHandler(TextBoxTest_TextChanged);
        }

        void TextBoxTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            //this.Text = this.TextBoxTest.Text;
        }        

        static void TextChangedCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            TestingControl testingControl = (TestingControl)property;
            string newText = (string)args.NewValue;
            //if (testingControl.TextBoxTest.Text != newText)
            //{
            //    testingControl.TextBoxTest.Text = newText;
            //}
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }            
        }      
    }
}
