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

namespace YellowstonePathology.UI.Cytology
{
    /// <summary>
    /// Interaction logic for CytologyOtherConditions.xaml
    /// </summary>
    public partial class ReportCommentSelection : UserControl
    {                
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(ReportCommentSelection), new UIPropertyMetadata(string.Empty, TextChangedCallback));

        public static readonly DependencyProperty CytologyPanelOrderProperty = DependencyProperty.Register("CytologyPanelOrder",
            typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology), typeof(ReportCommentSelection), new UIPropertyMetadata(null, CytologyPanelOrderChangedCallback));

        public static readonly DependencyProperty ReportCommentVisibilityProperty = DependencyProperty.Register("ReportCommentVisibility",
            typeof(Visibility), typeof(ReportCommentSelection), new UIPropertyMetadata(System.Windows.Visibility.Collapsed, VisibilityChangedCallback));

		YellowstonePathology.Business.Domain.Cytology.CytologyReportCommentCollection m_CytologyReportCommentCollection;
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_CytologyPanelOrder;

        public ReportCommentSelection()
        {
			this.m_CytologyReportCommentCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportComments();

            InitializeComponent();

			this.ListViewReportComment.DataContext = this.m_CytologyReportCommentCollection;
        }

        public Visibility ReportCommentVisibility
        {
            get { return (Visibility)GetValue(ReportCommentVisibilityProperty); }
            set 
            {
                this.SetValue(ReportCommentVisibilityProperty, value); 
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology CytologyPanelOrder
        {
            get { return (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)GetValue(CytologyPanelOrderProperty); }
            set
            {
                this.SetValue(CytologyPanelOrderProperty, value);
            }
        }


        private void TextBoxText_TextChanged(object sender, TextChangedEventArgs e)
        {            
            this.SetValue(TextProperty, this.TextBoxOtherConditions.Text);            
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.Cytology.SetReportComment setReportComment = new YellowstonePathology.Business.Rules.Cytology.SetReportComment();
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

            foreach (YellowstonePathology.Business.Domain.Cytology.CytologyReportComment reportComment in this.ListViewReportComment.SelectedItems)
            {
                setReportComment.Execute(reportComment.Comment, this.m_CytologyPanelOrder, executionStatus);
            }

            if (executionStatus.Halted == true)
            {
                System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
            }
            else
            {
                this.ListViewReportComment.SelectedItems.Clear();
                this.StackPanelListAndButtons.Visibility = System.Windows.Visibility.Collapsed;
                this.ReportCommentVisibility = System.Windows.Visibility.Collapsed;                
            }            
        }        

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.ListViewReportComment.SelectedItems.Clear();
            this.StackPanelListAndButtons.Visibility = System.Windows.Visibility.Collapsed;
            this.ReportCommentVisibility = System.Windows.Visibility.Collapsed;
            
        }        

        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportCommentSelection reportCommentSelection = (ReportCommentSelection)d;
            reportCommentSelection.TextBoxOtherConditions.Text = e.NewValue as string;
        }

        private static void VisibilityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportCommentSelection reportCommentSelection = (ReportCommentSelection)d;
            reportCommentSelection.StackPanelListAndButtons.Visibility = (Visibility)e.NewValue;
        }

        private static void CytologyPanelOrderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ReportCommentSelection reportCommentSelection = (ReportCommentSelection)d;
            reportCommentSelection.m_CytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)e.NewValue;
        }        
    }
}
