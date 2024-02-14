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
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for StainOrder.xaml
    /// </summary>
    public partial class StainOrder : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        List<CheckBox> m_StainsCheckBoxes;
        List<CheckBox> m_LiverPanelCheckBoxes;
        List<CheckBox> m_NonStainCheckBoxes;
        List<CheckBox> m_PanelCheckBoxes;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private string m_PanelOrderComment;
        private string m_RecutComment;

        private YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderView m_AliquotAndStainOrderView;
        private YellowstonePathology.Business.Visitor.StainAcknowledgementTaskOrderVisitor m_StainAcknowledgementTaskOrderVisitor;

        public StainOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;

            this.m_StainAcknowledgementTaskOrderVisitor = new Business.Visitor.StainAcknowledgementTaskOrderVisitor(this.m_PanelSetOrder);
            this.m_AliquotAndStainOrderView = new Login.FinalizeAccession.AliquotAndStainOrderView(this.m_AccessionOrder, this.m_PanelSetOrder);

            InitializeComponent();

            this.DataContext = this;
            this.Loaded += StainOrder_Loaded;            
        }        

        private void StainOrder_Loaded(object sender, RoutedEventArgs e)
        {     
            this.m_StainsCheckBoxes = new List<CheckBox>();
            this.InitializeCheckboxList("StainOrder", this.m_StainsCheckBoxes, this.MainGrid);

            this.m_LiverPanelCheckBoxes = new List<CheckBox>();
            this.m_LiverPanelCheckBoxes.Add(this.StainOrder122);
            this.m_LiverPanelCheckBoxes.Add(this.StainOrder123);
            this.m_LiverPanelCheckBoxes.Add(this.StainOrder124);
            this.m_LiverPanelCheckBoxes.Add(this.StainOrder125);
            this.m_LiverPanelCheckBoxes.Add(this.StainOrder126);

            this.m_NonStainCheckBoxes = new List<CheckBox>();
            this.m_NonStainCheckBoxes.Add(this.CheckBoxICF);
            this.m_NonStainCheckBoxes.Add(this.CheckBoxIC);
            this.m_NonStainCheckBoxes.Add(this.CheckBoxGrossOnly);
            this.m_NonStainCheckBoxes.Add(this.CheckBoxRecuts);

            this.m_PanelCheckBoxes = new List<CheckBox>();
            this.m_PanelCheckBoxes.Add(this.CheckBoxLowGradeLymphoma);
            this.m_PanelCheckBoxes.Add(this.CheckBoxLargeCellLymphoma);
            this.m_PanelCheckBoxes.Add(this.CheckBoxHodgkinLymphoma);
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderView AliquotAndStainOrderView
        {
            get { return this.m_AliquotAndStainOrderView; }
        }

        public string PanelOrderComment
        {
            get { return this.m_PanelOrderComment; }
            set
            {
                if (this.m_PanelOrderComment != value)
                {
                    this.m_PanelOrderComment = value;
                    this.NotifyPropertyChanged("PanelOrderComment");
                }
            }
        }

        public string RecutComment
        {
            get { return this.m_RecutComment; }
            set
            {
                if (this.m_RecutComment != value)
                {
                    this.m_RecutComment = value;
                    this.NotifyPropertyChanged("RecutComment");
                }
            }
        }

        private void InitializeCheckboxList(string name, List<CheckBox> list, DependencyObject depObj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if(child is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)child;
                    if(checkBox.Name.Contains(name))
                    {
                        list.Add(checkBox);
                    }
                }
                this.InitializeCheckboxList(name, list, child);
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            Business.Stain.Model.StainCollection stainsToOrder = this.GetStainsToOrder();            

            ((MainWindow)Application.Current.MainWindow).MoveKeyboardInputToNext();

            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            if (selectedAliquots.Count > 0)
            {
                YellowstonePathology.Business.Test.Model.TestCollection selectedTests = stainsToOrder.GetTestCollection();
                this.GetNonStainTestsToOrder(selectedTests);

                if (selectedTests.Count > 0)
                {
                    if(this.HandleRecutComment(selectedTests) == true)
                    {
                        this.HandleOrderingTests(selectedAliquots, selectedTests);

                        this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderDetailComment = this.m_PanelOrderComment;
                        this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);

                        this.ClearCheckBoxes();

                        this.m_AliquotAndStainOrderView.Refresh(false, this.m_PanelSetOrder);
                        this.NotifyPropertyChanged("AliquotAndStainOrderView");

                        this.m_PanelOrderComment = null;
                        this.NotifyPropertyChanged("PanelOrderComment");

                        this.m_RecutComment = null;
                        this.NotifyPropertyChanged("RecutComment");
                    }                    
                }
                else
                {
                    MessageBox.Show("There are no tests selected.");
                }
            }
            else
            {
                MessageBox.Show("There are no aliquots selected.");
            }
        }

        private bool HandleRecutComment(YellowstonePathology.Business.Test.Model.TestCollection selectedTests)
        {
            List<int> nmhClientList = new List<int>();
            nmhClientList.Add(587);
            nmhClientList.Add(588);
            nmhClientList.Add(1607);
            nmhClientList.Add(1604);
            nmhClientList.Add(1608);
            nmhClientList.Add(1699);                       

            bool result = true;            

            if(nmhClientList.Contains(this.m_AccessionOrder.ClientId))
            {
                if (selectedTests.Exists("150") == true)
                {
                    if (string.IsNullOrEmpty(this.m_PanelOrderComment) == true)
                    {
                        result = false;
                        MessageBox.Show("To better track the reason for recuts for NMH a comment is required. Click on the Recut Comment link to the right of the text box for canned comments.");
                    }
                }
            }            
            return result;
        }

        private void HandleOrderingTests(YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots, YellowstonePathology.Business.Test.Model.TestCollection selectedTests)
        {
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
                foreach (YellowstonePathology.Business.Test.Model.Test test in selectedTests)
                {
                    bool orderAsDual = false;
                    bool acknowledgeOnOrder = false;

                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitorTest = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, this.m_PanelOrderComment, orderAsDual, aliquotOrder, acknowledgeOnOrder, false, this.m_AccessionOrder.TaskOrderCollection);
                    this.m_AccessionOrder.TakeATrip(orderTestVisitorTest);
                    this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitorTest.TestOrder);
                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection selectedTestOrders = this.m_AliquotAndStainOrderView.GetSelectedTestOrders();

            if (selectedTestOrders.Count != 0)
            {
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in selectedTestOrders)
                {
                    if (this.m_AliquotAndStainOrderView.HaveSlidesBeenMadeForTestOrder(testOrder) == false)
                    {
                        YellowstonePathology.Business.Visitor.RemoveTestOrderVisitor removeTestOrderVisitor = new Business.Visitor.RemoveTestOrderVisitor(testOrder.TestOrderId);
                        this.m_AccessionOrder.TakeATrip(removeTestOrderVisitor);
                        this.m_StainAcknowledgementTaskOrderVisitor.RemoveTestOrder(testOrder);
                    }
                    else
                    {
                        MessageBox.Show("The test " + testOrder.TestName + " cannot be deleted because slides have been made.");
                    }
                }

                this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);
                this.m_AliquotAndStainOrderView.Refresh(false, this.m_PanelSetOrder);
                this.NotifyPropertyChanged("AliquotAndStainOrderView");
            }
            else
            {
                MessageBox.Show("No tests selected.");
            }
        }

        private void ClearCheckBoxes()
        {
            if(this.CheckBoxLiverPanel.IsChecked == true)
            {
                CheckBoxLiverPanel.IsChecked = false;
            }

            foreach (CheckBox checkBox in this.m_StainsCheckBoxes)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsChecked = false;
                }
            }

            foreach (CheckBox checkBox in this.m_NonStainCheckBoxes)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsChecked = false;
                }
            }

            foreach (CheckBox checkBox in this.m_PanelCheckBoxes)
            {
                if (checkBox.IsChecked == true)
                {
                    checkBox.IsChecked = false;
                }
            }
        }

        private Business.Stain.Model.StainCollection GetStainsToOrder()
        {
            Business.Stain.Model.StainCollection result = new Business.Stain.Model.StainCollection();
            foreach(CheckBox checkBox in this.m_StainsCheckBoxes)
            {
                if(checkBox.IsChecked == true)
                {
                    string stainId = (string)checkBox.Tag;
                    Business.Stain.Model.Stain stain = Business.Stain.Model.StainCollection.Instance.GetStain(stainId);
                    if (stain != null)
                    {
                        result.Add(stain);
                    }
                    else
                    {
                        throw new Exception("The stain was not found.");
                    }
                }                
            }

            this.AddTestsForSelectedPanels(result);
            return result;
        }

        private void AddTestsForSelectedPanels(Business.Stain.Model.StainCollection stainsToOrder)
        {
            Business.Stain.Model.StainCollection stains = Business.Stain.Model.StainCollection.Instance;
            if(this.CheckBoxLowGradeLymphoma.IsChecked == true)
            {

                stainsToOrder.Add(stains.GetStain("cd3"));
                stainsToOrder.Add(stains.GetStain("cd5"));
                stainsToOrder.Add(stains.GetStain("cd10"));
                stainsToOrder.Add(stains.GetStain("cd19"));
                stainsToOrder.Add(stains.GetStain("cd20"));
                stainsToOrder.Add(stains.GetStain("cd23"));
                stainsToOrder.Add(stains.GetStain("cd138"));
                stainsToOrder.Add(stains.GetStain("bcl2"));
                stainsToOrder.Add(stains.GetStain("bcl6"));
                stainsToOrder.Add(stains.GetStain("cyclnd1"));
                stainsToOrder.Add(stains.GetStain("px5"));
            }
            else if (this.CheckBoxLargeCellLymphoma.IsChecked == true)
            {
                stainsToOrder.Add(stains.GetStain("cd3"));
                stainsToOrder.Add(stains.GetStain("cd5"));
                stainsToOrder.Add(stains.GetStain("cd10"));
                stainsToOrder.Add(stains.GetStain("cd19"));
                stainsToOrder.Add(stains.GetStain("cd20"));
                stainsToOrder.Add(stains.GetStain("cd30"));
                stainsToOrder.Add(stains.GetStain("bcl2"));
                stainsToOrder.Add(stains.GetStain("bcl6"));
                stainsToOrder.Add(stains.GetStain("mm1"));
                stainsToOrder.Add(stains.GetStain("px5"));
                stainsToOrder.Add(stains.GetStain("cyclnd1"));
                stainsToOrder.Add(stains.GetStain("k67"));
                stainsToOrder.Add(stains.GetStain("cmyc"));
            }
            else if (this.CheckBoxHodgkinLymphoma.IsChecked == true)
            {
                stainsToOrder.Add(stains.GetStain("cd3"));
                stainsToOrder.Add(stains.GetStain("cd15"));
                stainsToOrder.Add(stains.GetStain("cd19"));
                stainsToOrder.Add(stains.GetStain("cd20"));
                stainsToOrder.Add(stains.GetStain("cd30"));
                stainsToOrder.Add(stains.GetStain("cd45"));
                stainsToOrder.Add(stains.GetStain("px5"));                
                stainsToOrder.Add(stains.GetStain("fscn"));                
            }
        }

        private void GetNonStainTestsToOrder(Business.Test.Model.TestCollection selectedTests)
        {
            foreach (CheckBox checkBox in this.m_NonStainCheckBoxes)
            {
                if (checkBox.IsChecked == true)
                {
                    string testId = (string)checkBox.Tag;
                    Business.Test.Model.Test test = Business.Test.Model.TestCollectionInstance.GetClone(testId);

                    if (test is Business.Test.Model.Recut) test.OrderComment = RecutComment;

                    selectedTests.Add(test);
                }
            }
        }        

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void CheckBoxLiverPanel_Checked(object sender, RoutedEventArgs e)
        {            
            foreach(CheckBox checkBox in this.m_LiverPanelCheckBoxes)
            {
                checkBox.IsChecked = true;
            }                            
        }

        private void CheckBoxLiverPanel_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (CheckBox checkBox in this.m_LiverPanelCheckBoxes)
            {
                checkBox.IsChecked = false;
            }
        }

        private void HyperLinkRecutComments_Click(object sender, RoutedEventArgs e)
        {
            UI.Surgical.RecutCommentsDialog recutCommentsDialog = new RecutCommentsDialog(this);
            recutCommentsDialog.ShowDialog();            
        }

        private void HandleProcessBlockComment(bool removeOnly)
        {
            string pattern = "Please reprocess block.+\\.";
            bool exists = (!string.IsNullOrEmpty(this.m_RecutComment) && Regex.IsMatch(this.m_RecutComment, pattern, RegexOptions.None));
            if (exists)
            {
                this.m_RecutComment = Regex.Replace(this.m_RecutComment, pattern, string.Empty, RegexOptions.None);
            }

            if (removeOnly == false)
            {
                Business.Test.AliquotOrderCollection selectedBlocks = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
                if (selectedBlocks.Count > 0)
                {
                    string blocks = string.Join(", ", selectedBlocks.Select(i => i.Label));
                    string reprocessComment = $"Please reprocess block(s) { blocks}.";
                    int lastCommaIndex = reprocessComment.LastIndexOf(",");
                    if(lastCommaIndex > 0)
                    {
                        reprocessComment = reprocessComment.Remove(lastCommaIndex, 1).Insert(lastCommaIndex, " and");
                    }                    
                    this.m_RecutComment = $"{((string.IsNullOrEmpty(this.m_RecutComment) == false) ? this.m_RecutComment + " " : "")}{reprocessComment}";
                }
            }
            this.NotifyPropertyChanged("RecutComment");
        }

        private void CheckboxReprocessBlock_Checked(object sender, RoutedEventArgs e)
        {
            this.HandleProcessBlockComment(false);               
        }

        private void CheckboxReporcessBlock_Unchecked(object sender, RoutedEventArgs e)
        {
            this.HandleProcessBlockComment(true);
        }
    }
}
