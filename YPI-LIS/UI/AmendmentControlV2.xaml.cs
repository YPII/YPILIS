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
	public partial class AmendmentControlV2 : System.Windows.Controls.UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_Parent;

        private YellowstonePathology.Business.View.PanelSetOrderAmendmentViewCollection m_PanelSetOrderAmendmentViewCollection;
        private Visibility m_ContextMenuPSOVisibility;
        private Visibility m_ContextMenuAmendmentVisibility;

        public AmendmentControlV2(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderAmendmentViewCollection = new Business.View.PanelSetOrderAmendmentViewCollection();
            if (this.m_AccessionOrder != null) this.m_PanelSetOrderAmendmentViewCollection = new Business.View.PanelSetOrderAmendmentViewCollection(this.m_AccessionOrder);

            this.m_ContextMenuPSOVisibility = Visibility.Collapsed;
            this.m_ContextMenuAmendmentVisibility = Visibility.Collapsed;

            InitializeComponent();
            this.DataContext = this;

            if (this.m_AccessionOrder != null)
            {
                if (this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                {
                    this.TreeViewAmendment.ContextMenu.IsEnabled = false;
                }
            }
        }

        public YellowstonePathology.Business.User.SystemUser CurrentUser
        {
            get { return YellowstonePathology.Business.User.SystemIdentity.Instance.User; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            private set
            {
                this.m_AccessionOrder = value;
                this.NotifyPropertyChanged("AccessionOrder");
            }
        }

        public YellowstonePathology.Business.View.PanelSetOrderAmendmentViewCollection PanelSetOrderAmendmentViewCollection
        {
            get { return this.m_PanelSetOrderAmendmentViewCollection; }
        }

        public Visibility ContextMenuPSOVisibility
        {
            get { return this.m_ContextMenuPSOVisibility; }
            set
            {
                if (this.m_ContextMenuPSOVisibility != value)
                {
                    this.m_ContextMenuPSOVisibility = value;
                    NotifyPropertyChanged("ContextMenuPSOVisibility");
                }
            }
        }

        public Visibility ContextMenuAmendmentVisibility
        {
            get { return this.m_ContextMenuAmendmentVisibility; }
            set
            {
                if (this.m_ContextMenuAmendmentVisibility != value)
                {
                    this.m_ContextMenuAmendmentVisibility = value;
                    NotifyPropertyChanged("ContextMenuAmendmentVisibility");
                }
            }
        }

        public void ContextMenuAddAmendment_Click(object sender, RoutedEventArgs args)
        {
            if (this.TreeViewAmendment.SelectedItem != null)
            {
                if (this.m_AccessionOrder != null)
                {
                    string reportNo = ((Business.View.PanelSetOrderAmendmentView)this.TreeViewAmendment.SelectedItem).PanelSetOrder.ReportNo;
                    this.m_AccessionOrder.AddAmendment(reportNo);
                    this.m_PanelSetOrderAmendmentViewCollection.Refresh(this.m_AccessionOrder);
                }
            }
        }

        public void ContextMenuEditAmendment_Click(object sender, RoutedEventArgs args)
        {
            if (this.TreeViewAmendment.SelectedItem != null)
            {
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.TreeViewAmendment.SelectedItem;
                YellowstonePathology.UI.AmendmentV2 amendmentV2 = new AmendmentV2(amendment, this.m_AccessionOrder, this.m_Parent);
                amendmentV2.ShowDialog();
                this.m_PanelSetOrderAmendmentViewCollection.Refresh(this.m_AccessionOrder);
            }
        }

        public void ContextMenuDeleteAmendment_Click(object sedner, RoutedEventArgs args)
        {
            if (this.TreeViewAmendment.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Delete the selected item?", "Delete.", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.m_AccessionOrder.DeleteAmendment(((YellowstonePathology.Business.Amendment.Model.Amendment)this.TreeViewAmendment.SelectedItem).AmendmentId);
                    this.m_PanelSetOrderAmendmentViewCollection.Refresh(this.m_AccessionOrder);
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

        private void TreeViewAmendment_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.TreeViewAmendment.SelectedItem != null)
            {
                if (this.TreeViewAmendment.SelectedItem is YellowstonePathology.Business.Amendment.Model.Amendment)
                {
                    this.ContextMenuAmendmentVisibility = Visibility.Visible;
                    this.ContextMenuPSOVisibility = Visibility.Collapsed;
                }
                else
                {
                    this.ContextMenuAmendmentVisibility = Visibility.Collapsed;
                    this.ContextMenuPSOVisibility = Visibility.Visible;
                }
            }
            else
            {
                this.ContextMenuAmendmentVisibility = Visibility.Collapsed;
                this.ContextMenuPSOVisibility = Visibility.Collapsed;
            }
            e.Handled = true;
        }

        private T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            if (parent is TreeViewItem)
            {
                TreeViewItem item = parent as TreeViewItem;
                YellowstonePathology.Business.View.PanelSetOrderAmendmentView view = item.DataContext as YellowstonePathology.Business.View.PanelSetOrderAmendmentView;
                this.m_Parent = view.PanelSetOrder;
                return null;
            }
            return parentT ?? FindParent<T>(parent);
        }

        private void TreeViewAmendment_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = item.DataContext as YellowstonePathology.Business.Amendment.Model.Amendment;
            if (amendment != null)
            {
                TreeViewItem parent = FindParent<TreeViewItem>(item);
            }
        }
    }
}