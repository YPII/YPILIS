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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TaskWorkspace.xaml
    /// </summary>
    public partial class TaskWorkspace : UserControl
    {
        private TaskUI m_TaskUI;
        private bool m_LoadedHasRun;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public TaskWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_LoadedHasRun = false;
            this.m_Writer = writer;

            this.m_TaskUI = new TaskUI(this.m_Writer);

            InitializeComponent();

            this.DataContext = this.m_TaskUI;
            this.DatePickerDailyLog.SelectedDate = DateTime.Today;

            this.Loaded += new RoutedEventHandler(LoginWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(LoginWorkspace_Unloaded);
        }

        private void LoginWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_LoadedHasRun == false)
            {
                this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
                this.m_MainWindowCommandButtonHandler.RemoveTab += MainWindowCommandButtonHandler_RemoveTab;
            }


            this.m_LoadedHasRun = true;
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            if (this.m_TaskUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_TaskUI.AccessionOrder, this.m_Writer);
            }
        }

        private void LoginWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_LoadedHasRun = false;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.RemoveTab -= MainWindowCommandButtonHandler_RemoveTab;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        private void ButtonViewDailyLog_Click(object sender, RoutedEventArgs e)
        {
            if (!this.DatePickerDailyLog.SelectedDate.HasValue)
            {
                MessageBox.Show("Select a log date to display.", "No date selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            this.m_TaskUI.ViewLabOrderLog(this.DatePickerDailyLog.SelectedDate.Value);
        }

        private void ButtonTaskOrderRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_TaskUI.GetTaskOrderCollection();
        }

        private void ButtonTaskOrderPrint_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTaskOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = (YellowstonePathology.Business.Task.Model.TaskOrder)this.ListViewTaskOrders.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(taskOrder.MasterAccessionNo, this.m_Writer);
                Login.Receiving.TaskOrderDataSheet taskOrderDataSheet = new Login.Receiving.TaskOrderDataSheet(taskOrder, accessionOrder);

                System.Printing.PrintQueue printQueue = new System.Printing.LocalPrintServer().DefaultPrintQueue;
                System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
                printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
                printDialog.PrintQueue = printQueue;
                printDialog.PrintDocument(taskOrderDataSheet.FixedDocument.DocumentPaginator, "Task Order Data Sheet");
            }
        }

        private void ListViewTaskOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewTaskOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Task.Model.TaskOrder selectedTaskOrder = (YellowstonePathology.Business.Task.Model.TaskOrder)this.ListViewTaskOrders.SelectedItem;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(selectedTaskOrder.MasterAccessionNo, this.m_Writer);
                YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = accessionOrder.TaskOrderCollection.GetTaskOrder(selectedTaskOrder.TaskOrderId);

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();

                YellowstonePathology.UI.Login.Receiving.TaskOrderPath taskOrderPath = new Login.Receiving.TaskOrderPath(accessionOrder, taskOrder, this.m_LoginPageWindow.PageNavigator, PageNavigationModeEnum.Standalone);
                taskOrderPath.Close += new Login.Receiving.TaskOrderPath.CloseEventHandler(TaskOrderPath_Close);
                taskOrderPath.Start();

                this.m_LoginPageWindow.ShowDialog();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this.m_Writer);
            }
        }

        private void TaskOrderPath_Close(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ButtonTasksNotAcknowledged_Click(object sender, RoutedEventArgs e)
        {
            string acknowledgeTasksFor = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.AcknowledgeTasksFor;
            if (string.IsNullOrEmpty(acknowledgeTasksFor) == false)
            {
                this.m_TaskUI.GetTasksNotAcknowledged();
            }
            else
            {
                MessageBox.Show("You must select the department to acknowledge for in preferences to complete this action.");
            }
        }

        private void ButtonDailyTaskOrderRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_TaskUI.GetDailyTaskOrderCollection();
        }

        private void ButtonDailyTaskAcknowledge_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
            if (this.ListViewDailyTaskOrders.SelectedItems.Count > 0)
            {
                foreach (YellowstonePathology.Business.Task.Model.TaskOrder listTaskOrder in this.ListViewDailyTaskOrders.SelectedItems)
                {
                    YellowstonePathology.Business.Task.Model.TaskOrder pulledTaskOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullTaskOrder(listTaskOrder.TaskOrderId, this.m_Writer);
                    if (pulledTaskOrder.Acknowledged == false)
                    {
                        listTaskOrder.Acknowledged = true;
                        listTaskOrder.AcknowledgedDate = DateTime.Now;
                        listTaskOrder.AcknowledgedById = systemIdentity.User.UserId;
                        listTaskOrder.AcknowledgedByInitials = systemIdentity.User.Initials;

                        pulledTaskOrder.Acknowledged = listTaskOrder.Acknowledged;
                        pulledTaskOrder.AcknowledgedDate = listTaskOrder.AcknowledgedDate;
                        pulledTaskOrder.AcknowledgedById = listTaskOrder.AcknowledgedById;
                        pulledTaskOrder.AcknowledgedByInitials = listTaskOrder.AcknowledgedByInitials;
                    }
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(pulledTaskOrder, this.m_Writer);
                }
            }
            else
            {
                MessageBox.Show("Select a task to acknowledge.");
            }
        }

        private void ButtonDailyTaskOrderPrint_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewDailyTaskOrders.SelectedItems.Count > 0)
            {
                YellowstonePathology.Business.Task.Model.TaskCytologySlideDisposal taskCytologySlideDisposal = new Business.Task.Model.TaskCytologySlideDisposal();
                YellowstonePathology.Business.Task.Model.TaskSurgicalSpecimenDisposal taskSurgicalSpecimenDisposal = new Business.Task.Model.TaskSurgicalSpecimenDisposal();
                YellowstonePathology.Business.Task.Model.TaskPOCReport taskPOCReport = new Business.Task.Model.TaskPOCReport();

                foreach (YellowstonePathology.Business.Task.Model.TaskOrder taskOrder in this.ListViewDailyTaskOrders.SelectedItems)
                {
                    if (taskOrder.TaskId == taskCytologySlideDisposal.TaskId)
                    {
                        YellowstonePathology.Business.Reports.CytologySlideDisposalReport report1 = new YellowstonePathology.Business.Reports.CytologySlideDisposalReport(taskOrder.TaskDate.Value);
                        System.Windows.Controls.PrintDialog printDialog1 = new System.Windows.Controls.PrintDialog();

                        printDialog1.ShowDialog();
                        printDialog1.PrintDocument(report1.DocumentPaginator, "Cytology Slide Disposal");
                    }
                    else if (taskOrder.TaskId == taskSurgicalSpecimenDisposal.TaskId)
                    {
                        YellowstonePathology.Business.Reports.SurgicalSpecimenDisposalReport report2 = new YellowstonePathology.Business.Reports.SurgicalSpecimenDisposalReport(taskOrder.TaskDate.Value);
                        System.Windows.Controls.PrintDialog printDialog2 = new System.Windows.Controls.PrintDialog();
                        printDialog2.ShowDialog();
                        printDialog2.PrintDocument(report2.DocumentPaginator, "Surgical Specimen Disposal Report for: ");
                    }
                    else if (taskOrder.TaskId == taskPOCReport.TaskId)
                    {
                        YellowstonePathology.Business.Reports.POCRetensionReport report = new YellowstonePathology.Business.Reports.POCRetensionReport(taskOrder.TaskDate.Value.AddDays(-6), taskOrder.TaskDate.Value);
                        System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
                        printDialog.ShowDialog();
                        printDialog.PrintDocument(report.DocumentPaginator, "POC ");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a task to print.");
            }
        }

        private void ButtonDailyTaskOrderHistory_Click(object sender, RoutedEventArgs e)
        {
            this.m_TaskUI.GetDailyTaskOrderHistoryCollection();
        }

        private void ButtonDailyTaskOrderAddDays_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            YellowstonePathology.Business.Rules.MethodResult result = YellowstonePathology.Business.Task.Model.TaskOrderCollection.AddDailyTaskOrderCytologySlideDisposal(30);
            message.AppendLine(result.Message);

            result = YellowstonePathology.Business.Task.Model.TaskOrderCollection.AddDailyTaskOrderSurgicalSpecimenDisposal(30);
            message.AppendLine(result.Message);

            MessageBox.Show(message.ToString());
        }

        private void MenuItemDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewTaskOrders.SelectedItems.Count != 0)
            {
                foreach (YellowstonePathology.Business.Task.Model.TaskOrder taskOrder in this.ListViewTaskOrders.SelectedItems)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(taskOrder, this.m_Writer);
                }
                this.m_TaskUI.GetTaskOrderCollection();
            }
        }
    }
}
