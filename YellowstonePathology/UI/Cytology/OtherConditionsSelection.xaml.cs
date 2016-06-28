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
    public partial class OtherConditionsSelection : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(OtherConditionsSelection), new UIPropertyMetadata(string.Empty, TextChangedCallback));

		public static readonly DependencyProperty CytologyPanelOrderProperty = DependencyProperty.Register("CytologyPanelOrder",
			typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology), typeof(OtherConditionsSelection), new UIPropertyMetadata(null, CytologyPanelOrderChangedCallback));

		public static readonly DependencyProperty OtherConditionsVisibilityProperty = DependencyProperty.Register("OtherConditionsVisibility",
            typeof(Visibility), typeof(OtherConditionsSelection), new UIPropertyMetadata(System.Windows.Visibility.Collapsed, VisibilityChangedCallback));

		YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection m_OtherConditionCollection;
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_CytologyPanelOrder;

        public OtherConditionsSelection()
        {
			this.m_OtherConditionCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetOtherConditions();

            InitializeComponent();

			this.ListViewOtherConditions.DataContext = this.m_OtherConditionCollection;
        }

        public Visibility OtherConditionsVisibility
        {
            get { return (Visibility)GetValue(OtherConditionsVisibilityProperty); }
            set
            {
                this.SetValue(OtherConditionsVisibilityProperty, value);
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
            YellowstonePathology.Business.Rules.Cytology.SetOtherCondition setOtherCondition = new YellowstonePathology.Business.Rules.Cytology.SetOtherCondition();
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

            foreach (YellowstonePathology.Business.Domain.Cytology.OtherCondition otherCondition in this.ListViewOtherConditions.SelectedItems)
            {
                setOtherCondition.Execute(otherCondition.OtherConditionText, this.m_CytologyPanelOrder, executionStatus);
            }

            if (executionStatus.Halted == true)
            {
                System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
            }
            else
            {
                this.ListViewOtherConditions.SelectedItems.Clear();
                this.StackPanelListAndButtons.Visibility = System.Windows.Visibility.Collapsed;
                this.OtherConditionsVisibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.ListViewOtherConditions.SelectedItems.Clear();
            this.StackPanelListAndButtons.Visibility = System.Windows.Visibility.Collapsed;
            this.OtherConditionsVisibility = System.Windows.Visibility.Collapsed;
        }

        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OtherConditionsSelection otherConditionsSelection = (OtherConditionsSelection)d;
            otherConditionsSelection.TextBoxOtherConditions.Text = e.NewValue as string;
        }

        private static void VisibilityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OtherConditionsSelection otherConditionsSelection = (OtherConditionsSelection)d;
            otherConditionsSelection.StackPanelListAndButtons.Visibility = (Visibility)e.NewValue;
        }

        private static void CytologyPanelOrderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OtherConditionsSelection otherConditionsSelection = (OtherConditionsSelection)d;
            otherConditionsSelection.m_CytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)e.NewValue;
        }
    }
}
